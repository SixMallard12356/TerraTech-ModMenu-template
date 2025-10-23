using System;
using UnityEngine;

[Serializable]
[NodePath("Graphs")]
[FriendlyName("Untitled", "")]
public class Mission_GSO_1_S_Battery : uScriptLogic
{
	private GameObject parentGameObject;

	private uScript_GUI thisScriptsOnGuiListener;

	private bool m_RegisteredForEvents;

	public Transform batteryTreePrefab;

	[Multiline(3)]
	public string clearSceneryPos = "";

	public float clearSceneryRadius;

	private GameHints.HintID local_122_GameHints_HintID = GameHints.HintID.BatteryCharge;

	private GameHints.HintID local_124_GameHints_HintID = GameHints.HintID.BatteryPower;

	private GameHints.HintID local_128_GameHints_HintID = GameHints.HintID.PowerStorage;

	private TankBlock local_BatteryBlock_TankBlock;

	private BlockTypes local_BatteryBlockType_BlockTypes = BlockTypes.GSOBattery_111;

	private Tank local_BatteryTech_Tank;

	private string local_BlockBattery_System_String = "BlockBattery";

	private Vector3 local_EncounterPos_UnityEngine_Vector3 = new Vector3(0f, 0f, 0f);

	private bool local_EnteredMissionArea_System_Boolean;

	private bool local_MissionComplete_System_Boolean;

	private ManOnScreenMessages.OnScreenMessage local_Msg1_ManOnScreenMessages_OnScreenMessage;

	private bool local_QuestLogShown_System_Boolean;

	private bool local_SceneryCleared_System_Boolean;

	private int local_Stage_System_Int32 = 1;

	private bool local_TreeDestroyed_System_Boolean;

	public uScript_AddMessage.MessageSpeaker messageSpeaker;

	[Multiline(3)]
	public string messageTag = "";

	public uScript_AddMessage.MessageData msg01FindBattery;

	public uScript_AddMessage.MessageData msg02BatteryFound;

	public uScript_AddMessage.MessageData msg03DestroyTree;

	public uScript_AddMessage.MessageData msg04PickUpBattery;

	public uScript_AddMessage.MessageData msg05AttachBattery;

	public uScript_AddMessage.MessageData msg06BatteryDropped;

	public uScript_AddMessage.MessageData msg07BatteryDestroyed;

	public uScript_AddMessage.MessageData msg08BatteryAttached;

	public float NearTreeDistance;

	public Transform Particles;

	[Multiline(3)]
	public string PositionID = "";

	private GameObject owner_Connection_0;

	private GameObject owner_Connection_2;

	private GameObject owner_Connection_11;

	private GameObject owner_Connection_25;

	private GameObject owner_Connection_31;

	private GameObject owner_Connection_39;

	private GameObject owner_Connection_42;

	private GameObject owner_Connection_47;

	private GameObject owner_Connection_50;

	private GameObject owner_Connection_58;

	private GameObject owner_Connection_117;

	private GameObject owner_Connection_131;

	private uScript_SpawnEmbeddedDeliveryCannon logic_uScript_SpawnEmbeddedDeliveryCannon_uScript_SpawnEmbeddedDeliveryCannon_4 = new uScript_SpawnEmbeddedDeliveryCannon();

	private Transform logic_uScript_SpawnEmbeddedDeliveryCannon_prefab_4;

	private Vector3 logic_uScript_SpawnEmbeddedDeliveryCannon_position_4;

	private string logic_uScript_SpawnEmbeddedDeliveryCannon_uniqueName_4 = "";

	private GameObject logic_uScript_SpawnEmbeddedDeliveryCannon_owner_4;

	private TankBlock logic_uScript_SpawnEmbeddedDeliveryCannon_Return_4;

	private bool logic_uScript_SpawnEmbeddedDeliveryCannon_Out_4 = true;

	private bool logic_uScript_SpawnEmbeddedDeliveryCannon_ResourceDestroyed_4 = true;

	private uScript_FireParticlesTowardsGround logic_uScript_FireParticlesTowardsGround_uScript_FireParticlesTowardsGround_8 = new uScript_FireParticlesTowardsGround();

	private Vector3 logic_uScript_FireParticlesTowardsGround_groundPos_8;

	private Transform logic_uScript_FireParticlesTowardsGround_particleEffect_8;

	private GameObject logic_uScript_FireParticlesTowardsGround_owner_8;

	private string logic_uScript_FireParticlesTowardsGround_uniqueName_8 = "fireRain";

	private Transform logic_uScript_FireParticlesTowardsGround_Return_8;

	private bool logic_uScript_FireParticlesTowardsGround_Delivered_8 = true;

	private bool logic_uScript_FireParticlesTowardsGround_Out_8 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_10 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_10;

	private bool logic_uScriptAct_SetBool_Out_10 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_10 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_10 = true;

	private uScript_GetPositionInEncounter logic_uScript_GetPositionInEncounter_uScript_GetPositionInEncounter_14 = new uScript_GetPositionInEncounter();

	private GameObject logic_uScript_GetPositionInEncounter_ownerNode_14;

	private string logic_uScript_GetPositionInEncounter_posName_14 = "";

	private Vector3 logic_uScript_GetPositionInEncounter_Return_14;

	private bool logic_uScript_GetPositionInEncounter_Out_14 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_17 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_17 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_17 = true;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_17 = true;

	private uScriptCon_ManualSwitch logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_20 = new uScriptCon_ManualSwitch();

	private int logic_uScriptCon_ManualSwitch_CurrentOutput_20;

	private uScript_IsPlayerInteractingWithBlock logic_uScript_IsPlayerInteractingWithBlock_uScript_IsPlayerInteractingWithBlock_23 = new uScript_IsPlayerInteractingWithBlock();

	private TankBlock logic_uScript_IsPlayerInteractingWithBlock_block_23;

	private bool logic_uScript_IsPlayerInteractingWithBlock_Out_23 = true;

	private bool logic_uScript_IsPlayerInteractingWithBlock_Interacted_23 = true;

	private bool logic_uScript_IsPlayerInteractingWithBlock_NotInteracted_23 = true;

	private bool logic_uScript_IsPlayerInteractingWithBlock_Dragging_23 = true;

	private bool logic_uScript_IsPlayerInteractingWithBlock_NotDragging_23 = true;

	private uScript_GetNamedBlock logic_uScript_GetNamedBlock_uScript_GetNamedBlock_26 = new uScript_GetNamedBlock();

	private string logic_uScript_GetNamedBlock_name_26 = "";

	private GameObject logic_uScript_GetNamedBlock_owner_26;

	private TankBlock logic_uScript_GetNamedBlock_Return_26;

	private bool logic_uScript_GetNamedBlock_Out_26 = true;

	private bool logic_uScript_GetNamedBlock_Destroyed_26 = true;

	private bool logic_uScript_GetNamedBlock_BlockExists_26 = true;

	private bool logic_uScript_GetNamedBlock_WaitingForBlock_26 = true;

	private bool logic_uScript_GetNamedBlock_NoBlock_26 = true;

	private uScript_DiscoverBlock logic_uScript_DiscoverBlock_uScript_DiscoverBlock_28 = new uScript_DiscoverBlock();

	private BlockTypes logic_uScript_DiscoverBlock_blockType_28;

	private bool logic_uScript_DiscoverBlock_Out_28 = true;

	private uScript_GetPlayerTankWithBlock logic_uScript_GetPlayerTankWithBlock_uScript_GetPlayerTankWithBlock_29 = new uScript_GetPlayerTankWithBlock();

	private BlockTypes logic_uScript_GetPlayerTankWithBlock_block_29;

	private TankBlock logic_uScript_GetPlayerTankWithBlock_tankBlock_29;

	private bool logic_uScript_GetPlayerTankWithBlock_useBlockType_29 = true;

	private Tank logic_uScript_GetPlayerTankWithBlock_Return_29;

	private bool logic_uScript_GetPlayerTankWithBlock_Returned_29 = true;

	private bool logic_uScript_GetPlayerTankWithBlock_NotReturned_29 = true;

	private bool logic_uScript_GetPlayerTankWithBlock_Out_29 = true;

	private uScript_PlayerInRangeOfCurrentEncounter logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_40 = new uScript_PlayerInRangeOfCurrentEncounter();

	private GameObject logic_uScript_PlayerInRangeOfCurrentEncounter_ownerNode_40;

	private bool logic_uScript_PlayerInRangeOfCurrentEncounter_Out_40 = true;

	private bool logic_uScript_PlayerInRangeOfCurrentEncounter_True_40 = true;

	private bool logic_uScript_PlayerInRangeOfCurrentEncounter_False_40 = true;

	private uScript_MoveEncounterWithVisible logic_uScript_MoveEncounterWithVisible_uScript_MoveEncounterWithVisible_43 = new uScript_MoveEncounterWithVisible();

	private GameObject logic_uScript_MoveEncounterWithVisible_ownerNode_43;

	private object logic_uScript_MoveEncounterWithVisible_visibleObject_43 = "";

	private bool logic_uScript_MoveEncounterWithVisible_Out_43 = true;

	private SubGraph_BaseBomb_ShowQuestLog logic_SubGraph_BaseBomb_ShowQuestLog_SubGraph_BaseBomb_ShowQuestLog_44 = new SubGraph_BaseBomb_ShowQuestLog();

	private bool logic_SubGraph_BaseBomb_ShowQuestLog_Flag_QuestLogShown_44;

	private uScript_SetEncounterTarget logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_49 = new uScript_SetEncounterTarget();

	private GameObject logic_uScript_SetEncounterTarget_owner_49;

	private object logic_uScript_SetEncounterTarget_visibleObject_49 = "";

	private bool logic_uScript_SetEncounterTarget_Out_49 = true;

	private uScript_SetBatteryChargeAmount logic_uScript_SetBatteryChargeAmount_uScript_SetBatteryChargeAmount_52 = new uScript_SetBatteryChargeAmount();

	private Tank logic_uScript_SetBatteryChargeAmount_tech_52;

	private float logic_uScript_SetBatteryChargeAmount_chargeAmount_52 = 1f;

	private bool logic_uScript_SetBatteryChargeAmount_Out_52 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_54 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_54;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_54 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_54 = "AddedQuestLog";

	private SubGraph_SaveLoadInt logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_55 = new SubGraph_SaveLoadInt();

	private int logic_SubGraph_SaveLoadInt_restartValue_55 = 1;

	private int logic_SubGraph_SaveLoadInt_integer_55;

	private object logic_SubGraph_SaveLoadInt_intAsVariable_55 = "";

	private string logic_SubGraph_SaveLoadInt_uniqueID_55 = "Stage";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_61 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_61;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_61 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_61 = "TreeDestroyed";

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_62 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_62;

	private bool logic_uScriptCon_CompareBool_True_62 = true;

	private bool logic_uScriptCon_CompareBool_False_62 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_65 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_65;

	private bool logic_uScriptAct_SetBool_Out_65 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_65 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_65 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_66 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_66;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_66 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_66 = "MissionComplete";

	private uScript_FinishEncounter logic_uScript_FinishEncounter_uScript_FinishEncounter_68 = new uScript_FinishEncounter();

	private GameObject logic_uScript_FinishEncounter_owner_68;

	private bool logic_uScript_FinishEncounter_Out_68 = true;

	private uScript_KeepBlockInvulnerable logic_uScript_KeepBlockInvulnerable_uScript_KeepBlockInvulnerable_70 = new uScript_KeepBlockInvulnerable();

	private TankBlock logic_uScript_KeepBlockInvulnerable_block_70;

	private bool logic_uScript_KeepBlockInvulnerable_Out_70 = true;

	private SubGraph_LoadObjectiveStates logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_72 = new SubGraph_LoadObjectiveStates();

	private int logic_SubGraph_LoadObjectiveStates_currentObjective_72;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_74 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_74;

	private bool logic_uScriptCon_CompareBool_True_74 = true;

	private bool logic_uScriptCon_CompareBool_False_74 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_76 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_76;

	private bool logic_uScriptAct_SetBool_Out_76 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_76 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_76 = true;

	private uScript_PlayerInRange logic_uScript_PlayerInRange_uScript_PlayerInRange_79 = new uScript_PlayerInRange();

	private Vector3 logic_uScript_PlayerInRange_position_79;

	private float logic_uScript_PlayerInRange_range_79;

	private bool logic_uScript_PlayerInRange_True_79 = true;

	private bool logic_uScript_PlayerInRange_False_79 = true;

	private bool logic_uScript_PlayerInRange_Out_79 = true;

	private uScript_RemoveOnScreenMessage logic_uScript_RemoveOnScreenMessage_uScript_RemoveOnScreenMessage_80 = new uScript_RemoveOnScreenMessage();

	private ManOnScreenMessages.OnScreenMessage logic_uScript_RemoveOnScreenMessage_onScreenMessage_80;

	private bool logic_uScript_RemoveOnScreenMessage_instant_80;

	private bool logic_uScript_RemoveOnScreenMessage_Out_80 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_83 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_83;

	private bool logic_uScriptCon_CompareBool_True_83 = true;

	private bool logic_uScriptCon_CompareBool_False_83 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_85 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_85;

	private bool logic_uScriptCon_CompareBool_True_85 = true;

	private bool logic_uScriptCon_CompareBool_False_85 = true;

	private SubGraph_CompleteObjectiveStage logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_87 = new SubGraph_CompleteObjectiveStage();

	private int logic_SubGraph_CompleteObjectiveStage_objectiveStage_87;

	private bool logic_SubGraph_CompleteObjectiveStage_isFinalObjective_87;

	private SubGraph_CompleteObjectiveStage logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_88 = new SubGraph_CompleteObjectiveStage();

	private int logic_SubGraph_CompleteObjectiveStage_objectiveStage_88;

	private bool logic_SubGraph_CompleteObjectiveStage_isFinalObjective_88;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_90 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_90;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_90;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_90;

	private bool logic_uScript_AddMessage_Out_90 = true;

	private bool logic_uScript_AddMessage_Shown_90 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_95 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_95;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_95;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_95;

	private bool logic_uScript_AddMessage_Out_95 = true;

	private bool logic_uScript_AddMessage_Shown_95 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_97 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_97;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_97;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_97;

	private bool logic_uScript_AddMessage_Out_97 = true;

	private bool logic_uScript_AddMessage_Shown_97 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_99 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_99;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_99;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_99;

	private bool logic_uScript_AddMessage_Out_99 = true;

	private bool logic_uScript_AddMessage_Shown_99 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_104 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_104;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_104;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_104;

	private bool logic_uScript_AddMessage_Out_104 = true;

	private bool logic_uScript_AddMessage_Shown_104 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_105 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_105;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_105;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_105;

	private bool logic_uScript_AddMessage_Out_105 = true;

	private bool logic_uScript_AddMessage_Shown_105 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_108 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_108;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_108;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_108;

	private bool logic_uScript_AddMessage_Out_108 = true;

	private bool logic_uScript_AddMessage_Shown_108 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_113 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_113;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_113;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_113;

	private bool logic_uScript_AddMessage_Out_113 = true;

	private bool logic_uScript_AddMessage_Shown_113 = true;

	private uScript_PointArrowAtBlock logic_uScript_PointArrowAtBlock_uScript_PointArrowAtBlock_114 = new uScript_PointArrowAtBlock();

	private TankBlock logic_uScript_PointArrowAtBlock_block_114;

	private float logic_uScript_PointArrowAtBlock_timeToShowFor_114 = 0.1f;

	private Vector3 logic_uScript_PointArrowAtBlock_offset_114 = new Vector3(0f, 0f, 0f);

	private bool logic_uScript_PointArrowAtBlock_Out_114 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_116 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_116 = true;

	private uScript_ShowHint logic_uScript_ShowHint_uScript_ShowHint_118 = new uScript_ShowHint();

	private GameHints.HintID logic_uScript_ShowHint_hintId_118;

	private bool logic_uScript_ShowHint_Out_118 = true;

	private uScript_FinishEncounter logic_uScript_FinishEncounter_uScript_FinishEncounter_120 = new uScript_FinishEncounter();

	private GameObject logic_uScript_FinishEncounter_owner_120;

	private bool logic_uScript_FinishEncounter_Out_120 = true;

	private uScript_HasHintBeenShownBefore logic_uScript_HasHintBeenShownBefore_uScript_HasHintBeenShownBefore_121 = new uScript_HasHintBeenShownBefore();

	private GameHints.HintID logic_uScript_HasHintBeenShownBefore_hintID_121;

	private bool logic_uScript_HasHintBeenShownBefore_Shown_121 = true;

	private bool logic_uScript_HasHintBeenShownBefore_NotShown_121 = true;

	private uScript_ShowHint logic_uScript_ShowHint_uScript_ShowHint_123 = new uScript_ShowHint();

	private GameHints.HintID logic_uScript_ShowHint_hintId_123;

	private bool logic_uScript_ShowHint_Out_123 = true;

	private uScript_HasHintBeenShownBefore logic_uScript_HasHintBeenShownBefore_uScript_HasHintBeenShownBefore_125 = new uScript_HasHintBeenShownBefore();

	private GameHints.HintID logic_uScript_HasHintBeenShownBefore_hintID_125;

	private bool logic_uScript_HasHintBeenShownBefore_Shown_125 = true;

	private bool logic_uScript_HasHintBeenShownBefore_NotShown_125 = true;

	private uScript_ShowHint logic_uScript_ShowHint_uScript_ShowHint_127 = new uScript_ShowHint();

	private GameHints.HintID logic_uScript_ShowHint_hintId_127;

	private bool logic_uScript_ShowHint_Out_127 = true;

	private uScript_HasHintBeenShownBefore logic_uScript_HasHintBeenShownBefore_uScript_HasHintBeenShownBefore_129 = new uScript_HasHintBeenShownBefore();

	private GameHints.HintID logic_uScript_HasHintBeenShownBefore_hintID_129;

	private bool logic_uScript_HasHintBeenShownBefore_Shown_129 = true;

	private bool logic_uScript_HasHintBeenShownBefore_NotShown_129 = true;

	private uScript_RemoveScenery logic_uScript_RemoveScenery_uScript_RemoveScenery_130 = new uScript_RemoveScenery();

	private GameObject logic_uScript_RemoveScenery_ownerNode_130;

	private string logic_uScript_RemoveScenery_positionName_130 = "";

	private float logic_uScript_RemoveScenery_radius_130;

	private bool logic_uScript_RemoveScenery_preventChunksSpawning_130 = true;

	private bool logic_uScript_RemoveScenery_Out_130 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_135 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_135;

	private bool logic_uScriptCon_CompareBool_True_135 = true;

	private bool logic_uScriptCon_CompareBool_False_135 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_136 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_136;

	private bool logic_uScriptAct_SetBool_Out_136 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_136 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_136 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_138 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_138;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_138 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_138 = "SceneryCleared";

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_139 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_139 = "";

	private bool logic_uScript_EnableGlow_enable_139 = true;

	private bool logic_uScript_EnableGlow_Out_139 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_142 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_142 = "";

	private bool logic_uScript_EnableGlow_enable_142;

	private bool logic_uScript_EnableGlow_Out_142 = true;

	private uScript_IsCoreEncounterCompleted logic_uScript_IsCoreEncounterCompleted_uScript_IsCoreEncounterCompleted_143 = new uScript_IsCoreEncounterCompleted();

	private FactionSubTypes logic_uScript_IsCoreEncounterCompleted_corp_143 = FactionSubTypes.GSO;

	private int logic_uScript_IsCoreEncounterCompleted_grade_143 = 1;

	private string logic_uScript_IsCoreEncounterCompleted_encounterName_143 = "1-S Radar";

	private bool logic_uScript_IsCoreEncounterCompleted_True_143 = true;

	private bool logic_uScript_IsCoreEncounterCompleted_False_143 = true;

	private void SyncUnityHooks()
	{
		SyncEventListeners();
		if (null == owner_Connection_0 || !m_RegisteredForEvents)
		{
			owner_Connection_0 = parentGameObject;
		}
		if (null == owner_Connection_2 || !m_RegisteredForEvents)
		{
			owner_Connection_2 = parentGameObject;
		}
		if (null == owner_Connection_11 || !m_RegisteredForEvents)
		{
			owner_Connection_11 = parentGameObject;
		}
		if (null == owner_Connection_25 || !m_RegisteredForEvents)
		{
			owner_Connection_25 = parentGameObject;
		}
		if (null == owner_Connection_31 || !m_RegisteredForEvents)
		{
			owner_Connection_31 = parentGameObject;
		}
		if (null == owner_Connection_39 || !m_RegisteredForEvents)
		{
			owner_Connection_39 = parentGameObject;
		}
		if (null == owner_Connection_42 || !m_RegisteredForEvents)
		{
			owner_Connection_42 = parentGameObject;
		}
		if (null == owner_Connection_47 || !m_RegisteredForEvents)
		{
			owner_Connection_47 = parentGameObject;
			if (null != owner_Connection_47)
			{
				uScript_EncounterUpdate uScript_EncounterUpdate2 = owner_Connection_47.GetComponent<uScript_EncounterUpdate>();
				if (null == uScript_EncounterUpdate2)
				{
					uScript_EncounterUpdate2 = owner_Connection_47.AddComponent<uScript_EncounterUpdate>();
				}
				if (null != uScript_EncounterUpdate2)
				{
					uScript_EncounterUpdate2.OnUpdate += Instance_OnUpdate_48;
					uScript_EncounterUpdate2.OnSuspend += Instance_OnSuspend_48;
					uScript_EncounterUpdate2.OnResume += Instance_OnResume_48;
				}
			}
		}
		if (null == owner_Connection_50 || !m_RegisteredForEvents)
		{
			owner_Connection_50 = parentGameObject;
		}
		if (null == owner_Connection_58 || !m_RegisteredForEvents)
		{
			owner_Connection_58 = parentGameObject;
			if (null != owner_Connection_58)
			{
				uScript_SaveLoad uScript_SaveLoad2 = owner_Connection_58.GetComponent<uScript_SaveLoad>();
				if (null == uScript_SaveLoad2)
				{
					uScript_SaveLoad2 = owner_Connection_58.AddComponent<uScript_SaveLoad>();
				}
				if (null != uScript_SaveLoad2)
				{
					uScript_SaveLoad2.SaveEvent += Instance_SaveEvent_57;
					uScript_SaveLoad2.LoadEvent += Instance_LoadEvent_57;
					uScript_SaveLoad2.RestartEvent += Instance_RestartEvent_57;
				}
			}
		}
		if (null == owner_Connection_117 || !m_RegisteredForEvents)
		{
			owner_Connection_117 = parentGameObject;
		}
		if (null == owner_Connection_131 || !m_RegisteredForEvents)
		{
			owner_Connection_131 = parentGameObject;
		}
	}

	private void RegisterForUnityHooks()
	{
		SyncEventListeners();
		if (!m_RegisteredForEvents && null != owner_Connection_47)
		{
			uScript_EncounterUpdate uScript_EncounterUpdate2 = owner_Connection_47.GetComponent<uScript_EncounterUpdate>();
			if (null == uScript_EncounterUpdate2)
			{
				uScript_EncounterUpdate2 = owner_Connection_47.AddComponent<uScript_EncounterUpdate>();
			}
			if (null != uScript_EncounterUpdate2)
			{
				uScript_EncounterUpdate2.OnUpdate += Instance_OnUpdate_48;
				uScript_EncounterUpdate2.OnSuspend += Instance_OnSuspend_48;
				uScript_EncounterUpdate2.OnResume += Instance_OnResume_48;
			}
		}
		if (!m_RegisteredForEvents && null != owner_Connection_58)
		{
			uScript_SaveLoad uScript_SaveLoad2 = owner_Connection_58.GetComponent<uScript_SaveLoad>();
			if (null == uScript_SaveLoad2)
			{
				uScript_SaveLoad2 = owner_Connection_58.AddComponent<uScript_SaveLoad>();
			}
			if (null != uScript_SaveLoad2)
			{
				uScript_SaveLoad2.SaveEvent += Instance_SaveEvent_57;
				uScript_SaveLoad2.LoadEvent += Instance_LoadEvent_57;
				uScript_SaveLoad2.RestartEvent += Instance_RestartEvent_57;
			}
		}
	}

	private void SyncEventListeners()
	{
	}

	private void UnregisterEventListeners()
	{
		if (null != owner_Connection_47)
		{
			uScript_EncounterUpdate component = owner_Connection_47.GetComponent<uScript_EncounterUpdate>();
			if (null != component)
			{
				component.OnUpdate -= Instance_OnUpdate_48;
				component.OnSuspend -= Instance_OnSuspend_48;
				component.OnResume -= Instance_OnResume_48;
			}
		}
		if (null != owner_Connection_58)
		{
			uScript_SaveLoad component2 = owner_Connection_58.GetComponent<uScript_SaveLoad>();
			if (null != component2)
			{
				component2.SaveEvent -= Instance_SaveEvent_57;
				component2.LoadEvent -= Instance_LoadEvent_57;
				component2.RestartEvent -= Instance_RestartEvent_57;
			}
		}
	}

	public override void SetParent(GameObject g)
	{
		parentGameObject = g;
		logic_uScript_SpawnEmbeddedDeliveryCannon_uScript_SpawnEmbeddedDeliveryCannon_4.SetParent(g);
		logic_uScript_FireParticlesTowardsGround_uScript_FireParticlesTowardsGround_8.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_10.SetParent(g);
		logic_uScript_GetPositionInEncounter_uScript_GetPositionInEncounter_14.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_17.SetParent(g);
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_20.SetParent(g);
		logic_uScript_IsPlayerInteractingWithBlock_uScript_IsPlayerInteractingWithBlock_23.SetParent(g);
		logic_uScript_GetNamedBlock_uScript_GetNamedBlock_26.SetParent(g);
		logic_uScript_DiscoverBlock_uScript_DiscoverBlock_28.SetParent(g);
		logic_uScript_GetPlayerTankWithBlock_uScript_GetPlayerTankWithBlock_29.SetParent(g);
		logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_40.SetParent(g);
		logic_uScript_MoveEncounterWithVisible_uScript_MoveEncounterWithVisible_43.SetParent(g);
		logic_SubGraph_BaseBomb_ShowQuestLog_SubGraph_BaseBomb_ShowQuestLog_44.SetParent(g);
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_49.SetParent(g);
		logic_uScript_SetBatteryChargeAmount_uScript_SetBatteryChargeAmount_52.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_54.SetParent(g);
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_55.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_61.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_62.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_65.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_66.SetParent(g);
		logic_uScript_FinishEncounter_uScript_FinishEncounter_68.SetParent(g);
		logic_uScript_KeepBlockInvulnerable_uScript_KeepBlockInvulnerable_70.SetParent(g);
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_72.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_74.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_76.SetParent(g);
		logic_uScript_PlayerInRange_uScript_PlayerInRange_79.SetParent(g);
		logic_uScript_RemoveOnScreenMessage_uScript_RemoveOnScreenMessage_80.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_83.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_85.SetParent(g);
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_87.SetParent(g);
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_88.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_90.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_95.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_97.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_99.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_104.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_105.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_108.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_113.SetParent(g);
		logic_uScript_PointArrowAtBlock_uScript_PointArrowAtBlock_114.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_116.SetParent(g);
		logic_uScript_ShowHint_uScript_ShowHint_118.SetParent(g);
		logic_uScript_FinishEncounter_uScript_FinishEncounter_120.SetParent(g);
		logic_uScript_HasHintBeenShownBefore_uScript_HasHintBeenShownBefore_121.SetParent(g);
		logic_uScript_ShowHint_uScript_ShowHint_123.SetParent(g);
		logic_uScript_HasHintBeenShownBefore_uScript_HasHintBeenShownBefore_125.SetParent(g);
		logic_uScript_ShowHint_uScript_ShowHint_127.SetParent(g);
		logic_uScript_HasHintBeenShownBefore_uScript_HasHintBeenShownBefore_129.SetParent(g);
		logic_uScript_RemoveScenery_uScript_RemoveScenery_130.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_135.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_136.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_138.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_139.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_142.SetParent(g);
		logic_uScript_IsCoreEncounterCompleted_uScript_IsCoreEncounterCompleted_143.SetParent(g);
		owner_Connection_0 = parentGameObject;
		owner_Connection_2 = parentGameObject;
		owner_Connection_11 = parentGameObject;
		owner_Connection_25 = parentGameObject;
		owner_Connection_31 = parentGameObject;
		owner_Connection_39 = parentGameObject;
		owner_Connection_42 = parentGameObject;
		owner_Connection_47 = parentGameObject;
		owner_Connection_50 = parentGameObject;
		owner_Connection_58 = parentGameObject;
		owner_Connection_117 = parentGameObject;
		owner_Connection_131 = parentGameObject;
	}

	public void Awake()
	{
		logic_SubGraph_BaseBomb_ShowQuestLog_SubGraph_BaseBomb_ShowQuestLog_44.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_54.Awake();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_55.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_61.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_66.Awake();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_72.Awake();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_87.Awake();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_88.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_138.Awake();
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_20.Output1 += uScriptCon_ManualSwitch_Output1_20;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_20.Output2 += uScriptCon_ManualSwitch_Output2_20;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_20.Output3 += uScriptCon_ManualSwitch_Output3_20;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_20.Output4 += uScriptCon_ManualSwitch_Output4_20;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_20.Output5 += uScriptCon_ManualSwitch_Output5_20;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_20.Output6 += uScriptCon_ManualSwitch_Output6_20;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_20.Output7 += uScriptCon_ManualSwitch_Output7_20;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_20.Output8 += uScriptCon_ManualSwitch_Output8_20;
		logic_SubGraph_BaseBomb_ShowQuestLog_SubGraph_BaseBomb_ShowQuestLog_44.Out += SubGraph_BaseBomb_ShowQuestLog_Out_44;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_54.Save_Out += SubGraph_SaveLoadBool_Save_Out_54;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_54.Load_Out += SubGraph_SaveLoadBool_Load_Out_54;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_54.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_54;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_55.Save_Out += SubGraph_SaveLoadInt_Save_Out_55;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_55.Load_Out += SubGraph_SaveLoadInt_Load_Out_55;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_55.Restart_Out += SubGraph_SaveLoadInt_Restart_Out_55;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_61.Save_Out += SubGraph_SaveLoadBool_Save_Out_61;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_61.Load_Out += SubGraph_SaveLoadBool_Load_Out_61;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_61.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_61;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_66.Save_Out += SubGraph_SaveLoadBool_Save_Out_66;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_66.Load_Out += SubGraph_SaveLoadBool_Load_Out_66;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_66.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_66;
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_72.Out += SubGraph_LoadObjectiveStates_Out_72;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_87.Out += SubGraph_CompleteObjectiveStage_Out_87;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_88.Out += SubGraph_CompleteObjectiveStage_Out_88;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_138.Save_Out += SubGraph_SaveLoadBool_Save_Out_138;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_138.Load_Out += SubGraph_SaveLoadBool_Load_Out_138;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_138.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_138;
	}

	public void Start()
	{
		SyncUnityHooks();
		m_RegisteredForEvents = true;
		logic_SubGraph_BaseBomb_ShowQuestLog_SubGraph_BaseBomb_ShowQuestLog_44.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_54.Start();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_55.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_61.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_66.Start();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_72.Start();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_87.Start();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_88.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_138.Start();
	}

	public void OnEnable()
	{
		RegisterForUnityHooks();
		m_RegisteredForEvents = true;
		logic_SubGraph_BaseBomb_ShowQuestLog_SubGraph_BaseBomb_ShowQuestLog_44.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_54.OnEnable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_55.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_61.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_66.OnEnable();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_72.OnEnable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_87.OnEnable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_88.OnEnable();
		logic_uScript_HasHintBeenShownBefore_uScript_HasHintBeenShownBefore_121.OnEnable();
		logic_uScript_HasHintBeenShownBefore_uScript_HasHintBeenShownBefore_125.OnEnable();
		logic_uScript_HasHintBeenShownBefore_uScript_HasHintBeenShownBefore_129.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_138.OnEnable();
		logic_uScript_IsCoreEncounterCompleted_uScript_IsCoreEncounterCompleted_143.OnEnable();
	}

	public void OnDisable()
	{
		logic_uScript_SpawnEmbeddedDeliveryCannon_uScript_SpawnEmbeddedDeliveryCannon_4.OnDisable();
		logic_uScript_FireParticlesTowardsGround_uScript_FireParticlesTowardsGround_8.OnDisable();
		logic_uScript_IsPlayerInteractingWithBlock_uScript_IsPlayerInteractingWithBlock_23.OnDisable();
		logic_uScript_GetNamedBlock_uScript_GetNamedBlock_26.OnDisable();
		logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_40.OnDisable();
		logic_SubGraph_BaseBomb_ShowQuestLog_SubGraph_BaseBomb_ShowQuestLog_44.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_54.OnDisable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_55.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_61.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_66.OnDisable();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_72.OnDisable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_87.OnDisable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_88.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_90.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_95.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_97.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_99.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_104.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_105.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_108.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_113.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_138.OnDisable();
		UnregisterEventListeners();
		m_RegisteredForEvents = false;
	}

	public void Update()
	{
		SyncEventListeners();
		logic_SubGraph_BaseBomb_ShowQuestLog_SubGraph_BaseBomb_ShowQuestLog_44.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_54.Update();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_55.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_61.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_66.Update();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_72.Update();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_87.Update();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_88.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_138.Update();
	}

	public void OnDestroy()
	{
		logic_SubGraph_BaseBomb_ShowQuestLog_SubGraph_BaseBomb_ShowQuestLog_44.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_54.OnDestroy();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_55.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_61.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_66.OnDestroy();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_72.OnDestroy();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_87.OnDestroy();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_88.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_138.OnDestroy();
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_20.Output1 -= uScriptCon_ManualSwitch_Output1_20;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_20.Output2 -= uScriptCon_ManualSwitch_Output2_20;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_20.Output3 -= uScriptCon_ManualSwitch_Output3_20;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_20.Output4 -= uScriptCon_ManualSwitch_Output4_20;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_20.Output5 -= uScriptCon_ManualSwitch_Output5_20;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_20.Output6 -= uScriptCon_ManualSwitch_Output6_20;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_20.Output7 -= uScriptCon_ManualSwitch_Output7_20;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_20.Output8 -= uScriptCon_ManualSwitch_Output8_20;
		logic_SubGraph_BaseBomb_ShowQuestLog_SubGraph_BaseBomb_ShowQuestLog_44.Out -= SubGraph_BaseBomb_ShowQuestLog_Out_44;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_54.Save_Out -= SubGraph_SaveLoadBool_Save_Out_54;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_54.Load_Out -= SubGraph_SaveLoadBool_Load_Out_54;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_54.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_54;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_55.Save_Out -= SubGraph_SaveLoadInt_Save_Out_55;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_55.Load_Out -= SubGraph_SaveLoadInt_Load_Out_55;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_55.Restart_Out -= SubGraph_SaveLoadInt_Restart_Out_55;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_61.Save_Out -= SubGraph_SaveLoadBool_Save_Out_61;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_61.Load_Out -= SubGraph_SaveLoadBool_Load_Out_61;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_61.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_61;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_66.Save_Out -= SubGraph_SaveLoadBool_Save_Out_66;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_66.Load_Out -= SubGraph_SaveLoadBool_Load_Out_66;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_66.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_66;
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_72.Out -= SubGraph_LoadObjectiveStates_Out_72;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_87.Out -= SubGraph_CompleteObjectiveStage_Out_87;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_88.Out -= SubGraph_CompleteObjectiveStage_Out_88;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_138.Save_Out -= SubGraph_SaveLoadBool_Save_Out_138;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_138.Load_Out -= SubGraph_SaveLoadBool_Load_Out_138;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_138.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_138;
	}

	private void Instance_OnUpdate_48(object o, EventArgs e)
	{
		Relay_OnUpdate_48();
	}

	private void Instance_OnSuspend_48(object o, EventArgs e)
	{
		Relay_OnSuspend_48();
	}

	private void Instance_OnResume_48(object o, EventArgs e)
	{
		Relay_OnResume_48();
	}

	private void Instance_SaveEvent_57(object o, EventArgs e)
	{
		Relay_SaveEvent_57();
	}

	private void Instance_LoadEvent_57(object o, EventArgs e)
	{
		Relay_LoadEvent_57();
	}

	private void Instance_RestartEvent_57(object o, EventArgs e)
	{
		Relay_RestartEvent_57();
	}

	private void uScriptCon_ManualSwitch_Output1_20(object o, EventArgs e)
	{
		Relay_Output1_20();
	}

	private void uScriptCon_ManualSwitch_Output2_20(object o, EventArgs e)
	{
		Relay_Output2_20();
	}

	private void uScriptCon_ManualSwitch_Output3_20(object o, EventArgs e)
	{
		Relay_Output3_20();
	}

	private void uScriptCon_ManualSwitch_Output4_20(object o, EventArgs e)
	{
		Relay_Output4_20();
	}

	private void uScriptCon_ManualSwitch_Output5_20(object o, EventArgs e)
	{
		Relay_Output5_20();
	}

	private void uScriptCon_ManualSwitch_Output6_20(object o, EventArgs e)
	{
		Relay_Output6_20();
	}

	private void uScriptCon_ManualSwitch_Output7_20(object o, EventArgs e)
	{
		Relay_Output7_20();
	}

	private void uScriptCon_ManualSwitch_Output8_20(object o, EventArgs e)
	{
		Relay_Output8_20();
	}

	private void SubGraph_BaseBomb_ShowQuestLog_Out_44(object o, SubGraph_BaseBomb_ShowQuestLog.LogicEventArgs e)
	{
		logic_SubGraph_BaseBomb_ShowQuestLog_Flag_QuestLogShown_44 = e.Flag_QuestLogShown;
		local_QuestLogShown_System_Boolean = logic_SubGraph_BaseBomb_ShowQuestLog_Flag_QuestLogShown_44;
		Relay_Out_44();
	}

	private void SubGraph_SaveLoadBool_Save_Out_54(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_54 = e.boolean;
		local_QuestLogShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_54;
		Relay_Save_Out_54();
	}

	private void SubGraph_SaveLoadBool_Load_Out_54(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_54 = e.boolean;
		local_QuestLogShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_54;
		Relay_Load_Out_54();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_54(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_54 = e.boolean;
		local_QuestLogShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_54;
		Relay_Restart_Out_54();
	}

	private void SubGraph_SaveLoadInt_Save_Out_55(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_55 = e.integer;
		local_Stage_System_Int32 = logic_SubGraph_SaveLoadInt_integer_55;
		Relay_Save_Out_55();
	}

	private void SubGraph_SaveLoadInt_Load_Out_55(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_55 = e.integer;
		local_Stage_System_Int32 = logic_SubGraph_SaveLoadInt_integer_55;
		Relay_Load_Out_55();
	}

	private void SubGraph_SaveLoadInt_Restart_Out_55(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_55 = e.integer;
		local_Stage_System_Int32 = logic_SubGraph_SaveLoadInt_integer_55;
		Relay_Restart_Out_55();
	}

	private void SubGraph_SaveLoadBool_Save_Out_61(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_61 = e.boolean;
		local_TreeDestroyed_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_61;
		Relay_Save_Out_61();
	}

	private void SubGraph_SaveLoadBool_Load_Out_61(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_61 = e.boolean;
		local_TreeDestroyed_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_61;
		Relay_Load_Out_61();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_61(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_61 = e.boolean;
		local_TreeDestroyed_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_61;
		Relay_Restart_Out_61();
	}

	private void SubGraph_SaveLoadBool_Save_Out_66(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_66 = e.boolean;
		local_MissionComplete_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_66;
		Relay_Save_Out_66();
	}

	private void SubGraph_SaveLoadBool_Load_Out_66(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_66 = e.boolean;
		local_MissionComplete_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_66;
		Relay_Load_Out_66();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_66(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_66 = e.boolean;
		local_MissionComplete_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_66;
		Relay_Restart_Out_66();
	}

	private void SubGraph_LoadObjectiveStates_Out_72(object o, SubGraph_LoadObjectiveStates.LogicEventArgs e)
	{
		Relay_Out_72();
	}

	private void SubGraph_CompleteObjectiveStage_Out_87(object o, SubGraph_CompleteObjectiveStage.LogicEventArgs e)
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_87 = e.objectiveStage;
		local_Stage_System_Int32 = logic_SubGraph_CompleteObjectiveStage_objectiveStage_87;
		Relay_Out_87();
	}

	private void SubGraph_CompleteObjectiveStage_Out_88(object o, SubGraph_CompleteObjectiveStage.LogicEventArgs e)
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_88 = e.objectiveStage;
		local_Stage_System_Int32 = logic_SubGraph_CompleteObjectiveStage_objectiveStage_88;
		Relay_Out_88();
	}

	private void SubGraph_SaveLoadBool_Save_Out_138(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_138 = e.boolean;
		local_SceneryCleared_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_138;
		Relay_Save_Out_138();
	}

	private void SubGraph_SaveLoadBool_Load_Out_138(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_138 = e.boolean;
		local_SceneryCleared_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_138;
		Relay_Load_Out_138();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_138(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_138 = e.boolean;
		local_SceneryCleared_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_138;
		Relay_Restart_Out_138();
	}

	private void Relay_In_4()
	{
		logic_uScript_SpawnEmbeddedDeliveryCannon_prefab_4 = batteryTreePrefab;
		logic_uScript_SpawnEmbeddedDeliveryCannon_position_4 = local_EncounterPos_UnityEngine_Vector3;
		logic_uScript_SpawnEmbeddedDeliveryCannon_uniqueName_4 = local_BlockBattery_System_String;
		logic_uScript_SpawnEmbeddedDeliveryCannon_owner_4 = owner_Connection_0;
		logic_uScript_SpawnEmbeddedDeliveryCannon_Return_4 = logic_uScript_SpawnEmbeddedDeliveryCannon_uScript_SpawnEmbeddedDeliveryCannon_4.In(logic_uScript_SpawnEmbeddedDeliveryCannon_prefab_4, logic_uScript_SpawnEmbeddedDeliveryCannon_position_4, logic_uScript_SpawnEmbeddedDeliveryCannon_uniqueName_4, logic_uScript_SpawnEmbeddedDeliveryCannon_owner_4);
		local_BatteryBlock_TankBlock = logic_uScript_SpawnEmbeddedDeliveryCannon_Return_4;
		bool num = logic_uScript_SpawnEmbeddedDeliveryCannon_uScript_SpawnEmbeddedDeliveryCannon_4.Out;
		bool resourceDestroyed = logic_uScript_SpawnEmbeddedDeliveryCannon_uScript_SpawnEmbeddedDeliveryCannon_4.ResourceDestroyed;
		if (num)
		{
			Relay_In_44();
		}
		if (resourceDestroyed)
		{
			Relay_True_10();
		}
	}

	private void Relay_In_8()
	{
		logic_uScript_FireParticlesTowardsGround_groundPos_8 = local_EncounterPos_UnityEngine_Vector3;
		logic_uScript_FireParticlesTowardsGround_particleEffect_8 = Particles;
		logic_uScript_FireParticlesTowardsGround_owner_8 = owner_Connection_2;
		logic_uScript_FireParticlesTowardsGround_Return_8 = logic_uScript_FireParticlesTowardsGround_uScript_FireParticlesTowardsGround_8.In(logic_uScript_FireParticlesTowardsGround_groundPos_8, logic_uScript_FireParticlesTowardsGround_particleEffect_8, logic_uScript_FireParticlesTowardsGround_owner_8, logic_uScript_FireParticlesTowardsGround_uniqueName_8);
		if (logic_uScript_FireParticlesTowardsGround_uScript_FireParticlesTowardsGround_8.Delivered)
		{
			Relay_In_135();
		}
	}

	private void Relay_True_10()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_10.True(out logic_uScriptAct_SetBool_Target_10);
		local_TreeDestroyed_System_Boolean = logic_uScriptAct_SetBool_Target_10;
	}

	private void Relay_False_10()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_10.False(out logic_uScriptAct_SetBool_Target_10);
		local_TreeDestroyed_System_Boolean = logic_uScriptAct_SetBool_Target_10;
	}

	private void Relay_In_14()
	{
		logic_uScript_GetPositionInEncounter_ownerNode_14 = owner_Connection_11;
		logic_uScript_GetPositionInEncounter_posName_14 = PositionID;
		logic_uScript_GetPositionInEncounter_Return_14 = logic_uScript_GetPositionInEncounter_uScript_GetPositionInEncounter_14.In(logic_uScript_GetPositionInEncounter_ownerNode_14, logic_uScript_GetPositionInEncounter_posName_14);
		local_EncounterPos_UnityEngine_Vector3 = logic_uScript_GetPositionInEncounter_Return_14;
		if (logic_uScript_GetPositionInEncounter_uScript_GetPositionInEncounter_14.Out)
		{
			Relay_In_8();
		}
	}

	private void Relay_In_17()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_17 = messageTag;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_17.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_17, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_17);
	}

	private void Relay_Output1_20()
	{
		Relay_In_74();
	}

	private void Relay_Output2_20()
	{
		Relay_In_97();
	}

	private void Relay_Output3_20()
	{
		Relay_In_26();
	}

	private void Relay_Output4_20()
	{
	}

	private void Relay_Output5_20()
	{
	}

	private void Relay_Output6_20()
	{
	}

	private void Relay_Output7_20()
	{
	}

	private void Relay_Output8_20()
	{
	}

	private void Relay_In_20()
	{
		logic_uScriptCon_ManualSwitch_CurrentOutput_20 = local_Stage_System_Int32;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_20.In(logic_uScriptCon_ManualSwitch_CurrentOutput_20);
	}

	private void Relay_In_23()
	{
		logic_uScript_IsPlayerInteractingWithBlock_block_23 = local_BatteryBlock_TankBlock;
		logic_uScript_IsPlayerInteractingWithBlock_uScript_IsPlayerInteractingWithBlock_23.In(logic_uScript_IsPlayerInteractingWithBlock_block_23);
		bool interacted = logic_uScript_IsPlayerInteractingWithBlock_uScript_IsPlayerInteractingWithBlock_23.Interacted;
		bool notInteracted = logic_uScript_IsPlayerInteractingWithBlock_uScript_IsPlayerInteractingWithBlock_23.NotInteracted;
		bool dragging = logic_uScript_IsPlayerInteractingWithBlock_uScript_IsPlayerInteractingWithBlock_23.Dragging;
		if (interacted)
		{
			Relay_In_108();
		}
		if (notInteracted)
		{
			Relay_In_104();
		}
		if (dragging)
		{
			Relay_In_105();
		}
	}

	private void Relay_In_26()
	{
		logic_uScript_GetNamedBlock_name_26 = local_BlockBattery_System_String;
		logic_uScript_GetNamedBlock_owner_26 = owner_Connection_25;
		logic_uScript_GetNamedBlock_Return_26 = logic_uScript_GetNamedBlock_uScript_GetNamedBlock_26.In(logic_uScript_GetNamedBlock_name_26, logic_uScript_GetNamedBlock_owner_26);
		local_BatteryBlock_TankBlock = logic_uScript_GetNamedBlock_Return_26;
		bool destroyed = logic_uScript_GetNamedBlock_uScript_GetNamedBlock_26.Destroyed;
		bool blockExists = logic_uScript_GetNamedBlock_uScript_GetNamedBlock_26.BlockExists;
		bool waitingForBlock = logic_uScript_GetNamedBlock_uScript_GetNamedBlock_26.WaitingForBlock;
		if (destroyed)
		{
			Relay_In_99();
		}
		if (blockExists)
		{
			Relay_In_114();
		}
		if (waitingForBlock)
		{
			Relay_In_116();
		}
	}

	private void Relay_In_28()
	{
		logic_uScript_DiscoverBlock_blockType_28 = local_BatteryBlockType_BlockTypes;
		logic_uScript_DiscoverBlock_uScript_DiscoverBlock_28.In(logic_uScript_DiscoverBlock_blockType_28);
		if (logic_uScript_DiscoverBlock_uScript_DiscoverBlock_28.Out)
		{
			Relay_Succeed_68();
		}
	}

	private void Relay_In_29()
	{
		logic_uScript_GetPlayerTankWithBlock_block_29 = local_BatteryBlockType_BlockTypes;
		logic_uScript_GetPlayerTankWithBlock_Return_29 = logic_uScript_GetPlayerTankWithBlock_uScript_GetPlayerTankWithBlock_29.In(logic_uScript_GetPlayerTankWithBlock_block_29, logic_uScript_GetPlayerTankWithBlock_tankBlock_29, logic_uScript_GetPlayerTankWithBlock_useBlockType_29);
		local_BatteryTech_Tank = logic_uScript_GetPlayerTankWithBlock_Return_29;
		bool returned = logic_uScript_GetPlayerTankWithBlock_uScript_GetPlayerTankWithBlock_29.Returned;
		bool notReturned = logic_uScript_GetPlayerTankWithBlock_uScript_GetPlayerTankWithBlock_29.NotReturned;
		if (returned)
		{
			Relay_In_52();
		}
		if (notReturned)
		{
			Relay_In_49();
		}
	}

	private void Relay_In_40()
	{
		logic_uScript_PlayerInRangeOfCurrentEncounter_ownerNode_40 = owner_Connection_39;
		logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_40.In(logic_uScript_PlayerInRangeOfCurrentEncounter_ownerNode_40);
		bool num = logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_40.True;
		bool flag = logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_40.False;
		if (num)
		{
			Relay_In_20();
		}
		if (flag)
		{
			Relay_In_17();
		}
	}

	private void Relay_In_43()
	{
		logic_uScript_MoveEncounterWithVisible_ownerNode_43 = owner_Connection_42;
		logic_uScript_MoveEncounterWithVisible_visibleObject_43 = local_BatteryBlock_TankBlock;
		logic_uScript_MoveEncounterWithVisible_uScript_MoveEncounterWithVisible_43.In(logic_uScript_MoveEncounterWithVisible_ownerNode_43, logic_uScript_MoveEncounterWithVisible_visibleObject_43);
		if (logic_uScript_MoveEncounterWithVisible_uScript_MoveEncounterWithVisible_43.Out)
		{
			Relay_In_70();
		}
	}

	private void Relay_Out_44()
	{
		Relay_In_29();
	}

	private void Relay_In_44()
	{
		logic_SubGraph_BaseBomb_ShowQuestLog_Flag_QuestLogShown_44 = local_QuestLogShown_System_Boolean;
		logic_SubGraph_BaseBomb_ShowQuestLog_SubGraph_BaseBomb_ShowQuestLog_44.In(ref logic_SubGraph_BaseBomb_ShowQuestLog_Flag_QuestLogShown_44);
	}

	private void Relay_OnUpdate_48()
	{
		Relay_In_62();
	}

	private void Relay_OnSuspend_48()
	{
	}

	private void Relay_OnResume_48()
	{
	}

	private void Relay_In_49()
	{
		logic_uScript_SetEncounterTarget_owner_49 = owner_Connection_50;
		logic_uScript_SetEncounterTarget_visibleObject_49 = local_BatteryBlock_TankBlock;
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_49.In(logic_uScript_SetEncounterTarget_owner_49, logic_uScript_SetEncounterTarget_visibleObject_49);
		if (logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_49.Out)
		{
			Relay_In_43();
		}
	}

	private void Relay_In_52()
	{
		logic_uScript_SetBatteryChargeAmount_tech_52 = local_BatteryTech_Tank;
		logic_uScript_SetBatteryChargeAmount_uScript_SetBatteryChargeAmount_52.In(logic_uScript_SetBatteryChargeAmount_tech_52, logic_uScript_SetBatteryChargeAmount_chargeAmount_52);
		if (logic_uScript_SetBatteryChargeAmount_uScript_SetBatteryChargeAmount_52.Out)
		{
			Relay_True_65();
		}
	}

	private void Relay_Save_Out_54()
	{
		Relay_Save_61();
	}

	private void Relay_Load_Out_54()
	{
		Relay_Load_61();
	}

	private void Relay_Restart_Out_54()
	{
		Relay_Set_False_61();
	}

	private void Relay_Save_54()
	{
		logic_SubGraph_SaveLoadBool_boolean_54 = local_QuestLogShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_54 = local_QuestLogShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_54.Save(ref logic_SubGraph_SaveLoadBool_boolean_54, logic_SubGraph_SaveLoadBool_boolAsVariable_54, logic_SubGraph_SaveLoadBool_uniqueID_54);
	}

	private void Relay_Load_54()
	{
		logic_SubGraph_SaveLoadBool_boolean_54 = local_QuestLogShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_54 = local_QuestLogShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_54.Load(ref logic_SubGraph_SaveLoadBool_boolean_54, logic_SubGraph_SaveLoadBool_boolAsVariable_54, logic_SubGraph_SaveLoadBool_uniqueID_54);
	}

	private void Relay_Set_True_54()
	{
		logic_SubGraph_SaveLoadBool_boolean_54 = local_QuestLogShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_54 = local_QuestLogShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_54.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_54, logic_SubGraph_SaveLoadBool_boolAsVariable_54, logic_SubGraph_SaveLoadBool_uniqueID_54);
	}

	private void Relay_Set_False_54()
	{
		logic_SubGraph_SaveLoadBool_boolean_54 = local_QuestLogShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_54 = local_QuestLogShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_54.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_54, logic_SubGraph_SaveLoadBool_boolAsVariable_54, logic_SubGraph_SaveLoadBool_uniqueID_54);
	}

	private void Relay_Save_Out_55()
	{
		Relay_Save_138();
	}

	private void Relay_Load_Out_55()
	{
		Relay_Load_138();
	}

	private void Relay_Restart_Out_55()
	{
		Relay_Set_False_138();
	}

	private void Relay_Save_55()
	{
		logic_SubGraph_SaveLoadInt_integer_55 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_55 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_55.Save(logic_SubGraph_SaveLoadInt_restartValue_55, ref logic_SubGraph_SaveLoadInt_integer_55, logic_SubGraph_SaveLoadInt_intAsVariable_55, logic_SubGraph_SaveLoadInt_uniqueID_55);
	}

	private void Relay_Load_55()
	{
		logic_SubGraph_SaveLoadInt_integer_55 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_55 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_55.Load(logic_SubGraph_SaveLoadInt_restartValue_55, ref logic_SubGraph_SaveLoadInt_integer_55, logic_SubGraph_SaveLoadInt_intAsVariable_55, logic_SubGraph_SaveLoadInt_uniqueID_55);
	}

	private void Relay_Restart_55()
	{
		logic_SubGraph_SaveLoadInt_integer_55 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_55 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_55.Restart(logic_SubGraph_SaveLoadInt_restartValue_55, ref logic_SubGraph_SaveLoadInt_integer_55, logic_SubGraph_SaveLoadInt_intAsVariable_55, logic_SubGraph_SaveLoadInt_uniqueID_55);
	}

	private void Relay_SaveEvent_57()
	{
		Relay_Save_55();
	}

	private void Relay_LoadEvent_57()
	{
		Relay_Load_55();
	}

	private void Relay_RestartEvent_57()
	{
		Relay_Restart_55();
	}

	private void Relay_Save_Out_61()
	{
		Relay_Save_66();
	}

	private void Relay_Load_Out_61()
	{
		Relay_Load_66();
	}

	private void Relay_Restart_Out_61()
	{
		Relay_Set_False_66();
	}

	private void Relay_Save_61()
	{
		logic_SubGraph_SaveLoadBool_boolean_61 = local_TreeDestroyed_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_61 = local_TreeDestroyed_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_61.Save(ref logic_SubGraph_SaveLoadBool_boolean_61, logic_SubGraph_SaveLoadBool_boolAsVariable_61, logic_SubGraph_SaveLoadBool_uniqueID_61);
	}

	private void Relay_Load_61()
	{
		logic_SubGraph_SaveLoadBool_boolean_61 = local_TreeDestroyed_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_61 = local_TreeDestroyed_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_61.Load(ref logic_SubGraph_SaveLoadBool_boolean_61, logic_SubGraph_SaveLoadBool_boolAsVariable_61, logic_SubGraph_SaveLoadBool_uniqueID_61);
	}

	private void Relay_Set_True_61()
	{
		logic_SubGraph_SaveLoadBool_boolean_61 = local_TreeDestroyed_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_61 = local_TreeDestroyed_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_61.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_61, logic_SubGraph_SaveLoadBool_boolAsVariable_61, logic_SubGraph_SaveLoadBool_uniqueID_61);
	}

	private void Relay_Set_False_61()
	{
		logic_SubGraph_SaveLoadBool_boolean_61 = local_TreeDestroyed_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_61 = local_TreeDestroyed_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_61.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_61, logic_SubGraph_SaveLoadBool_boolAsVariable_61, logic_SubGraph_SaveLoadBool_uniqueID_61);
	}

	private void Relay_In_62()
	{
		logic_uScriptCon_CompareBool_Bool_62 = local_MissionComplete_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_62.In(logic_uScriptCon_CompareBool_Bool_62);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_62.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_62.False;
		if (num)
		{
			Relay_In_113();
		}
		if (flag)
		{
			Relay_In_14();
		}
	}

	private void Relay_True_65()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_65.True(out logic_uScriptAct_SetBool_Target_65);
		local_MissionComplete_System_Boolean = logic_uScriptAct_SetBool_Target_65;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_65.Out)
		{
			Relay_In_142();
		}
	}

	private void Relay_False_65()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_65.False(out logic_uScriptAct_SetBool_Target_65);
		local_MissionComplete_System_Boolean = logic_uScriptAct_SetBool_Target_65;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_65.Out)
		{
			Relay_In_142();
		}
	}

	private void Relay_Save_Out_66()
	{
	}

	private void Relay_Load_Out_66()
	{
		Relay_In_72();
	}

	private void Relay_Restart_Out_66()
	{
	}

	private void Relay_Save_66()
	{
		logic_SubGraph_SaveLoadBool_boolean_66 = local_MissionComplete_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_66 = local_MissionComplete_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_66.Save(ref logic_SubGraph_SaveLoadBool_boolean_66, logic_SubGraph_SaveLoadBool_boolAsVariable_66, logic_SubGraph_SaveLoadBool_uniqueID_66);
	}

	private void Relay_Load_66()
	{
		logic_SubGraph_SaveLoadBool_boolean_66 = local_MissionComplete_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_66 = local_MissionComplete_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_66.Load(ref logic_SubGraph_SaveLoadBool_boolean_66, logic_SubGraph_SaveLoadBool_boolAsVariable_66, logic_SubGraph_SaveLoadBool_uniqueID_66);
	}

	private void Relay_Set_True_66()
	{
		logic_SubGraph_SaveLoadBool_boolean_66 = local_MissionComplete_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_66 = local_MissionComplete_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_66.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_66, logic_SubGraph_SaveLoadBool_boolAsVariable_66, logic_SubGraph_SaveLoadBool_uniqueID_66);
	}

	private void Relay_Set_False_66()
	{
		logic_SubGraph_SaveLoadBool_boolean_66 = local_MissionComplete_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_66 = local_MissionComplete_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_66.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_66, logic_SubGraph_SaveLoadBool_boolAsVariable_66, logic_SubGraph_SaveLoadBool_uniqueID_66);
	}

	private void Relay_Succeed_68()
	{
		logic_uScript_FinishEncounter_owner_68 = owner_Connection_31;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_68.Succeed(logic_uScript_FinishEncounter_owner_68);
	}

	private void Relay_Fail_68()
	{
		logic_uScript_FinishEncounter_owner_68 = owner_Connection_31;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_68.Fail(logic_uScript_FinishEncounter_owner_68);
	}

	private void Relay_In_70()
	{
		logic_uScript_KeepBlockInvulnerable_block_70 = local_BatteryBlock_TankBlock;
		logic_uScript_KeepBlockInvulnerable_uScript_KeepBlockInvulnerable_70.In(logic_uScript_KeepBlockInvulnerable_block_70);
		if (logic_uScript_KeepBlockInvulnerable_uScript_KeepBlockInvulnerable_70.Out)
		{
			Relay_In_40();
		}
	}

	private void Relay_Out_72()
	{
	}

	private void Relay_In_72()
	{
		logic_SubGraph_LoadObjectiveStates_currentObjective_72 = local_Stage_System_Int32;
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_72.In(logic_SubGraph_LoadObjectiveStates_currentObjective_72);
	}

	private void Relay_In_74()
	{
		logic_uScriptCon_CompareBool_Bool_74 = local_EnteredMissionArea_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_74.In(logic_uScriptCon_CompareBool_Bool_74);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_74.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_74.False;
		if (num)
		{
			Relay_In_79();
		}
		if (flag)
		{
			Relay_True_76();
		}
	}

	private void Relay_True_76()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_76.True(out logic_uScriptAct_SetBool_Target_76);
		local_EnteredMissionArea_System_Boolean = logic_uScriptAct_SetBool_Target_76;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_76.Out)
		{
			Relay_In_90();
		}
	}

	private void Relay_False_76()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_76.False(out logic_uScriptAct_SetBool_Target_76);
		local_EnteredMissionArea_System_Boolean = logic_uScriptAct_SetBool_Target_76;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_76.Out)
		{
			Relay_In_90();
		}
	}

	private void Relay_In_79()
	{
		logic_uScript_PlayerInRange_position_79 = local_EncounterPos_UnityEngine_Vector3;
		logic_uScript_PlayerInRange_range_79 = NearTreeDistance;
		logic_uScript_PlayerInRange_uScript_PlayerInRange_79.In(logic_uScript_PlayerInRange_position_79, logic_uScript_PlayerInRange_range_79);
		bool num = logic_uScript_PlayerInRange_uScript_PlayerInRange_79.True;
		bool flag = logic_uScript_PlayerInRange_uScript_PlayerInRange_79.False;
		if (num)
		{
			Relay_In_80();
		}
		if (flag)
		{
			Relay_In_85();
		}
	}

	private void Relay_In_80()
	{
		logic_uScript_RemoveOnScreenMessage_onScreenMessage_80 = local_Msg1_ManOnScreenMessages_OnScreenMessage;
		logic_uScript_RemoveOnScreenMessage_uScript_RemoveOnScreenMessage_80.In(logic_uScript_RemoveOnScreenMessage_onScreenMessage_80, logic_uScript_RemoveOnScreenMessage_instant_80);
		if (logic_uScript_RemoveOnScreenMessage_uScript_RemoveOnScreenMessage_80.Out)
		{
			Relay_In_95();
		}
	}

	private void Relay_In_83()
	{
		logic_uScriptCon_CompareBool_Bool_83 = local_TreeDestroyed_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_83.In(logic_uScriptCon_CompareBool_Bool_83);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_83.True)
		{
			Relay_In_88();
		}
	}

	private void Relay_In_85()
	{
		logic_uScriptCon_CompareBool_Bool_85 = local_TreeDestroyed_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_85.In(logic_uScriptCon_CompareBool_Bool_85);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_85.True)
		{
			Relay_In_87();
		}
	}

	private void Relay_Out_87()
	{
	}

	private void Relay_In_87()
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_87 = local_Stage_System_Int32;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_87.In(ref logic_SubGraph_CompleteObjectiveStage_objectiveStage_87, logic_SubGraph_CompleteObjectiveStage_isFinalObjective_87);
	}

	private void Relay_Out_88()
	{
	}

	private void Relay_In_88()
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_88 = local_Stage_System_Int32;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_88.In(ref logic_SubGraph_CompleteObjectiveStage_objectiveStage_88, logic_SubGraph_CompleteObjectiveStage_isFinalObjective_88);
	}

	private void Relay_In_90()
	{
		logic_uScript_AddMessage_messageData_90 = msg01FindBattery;
		logic_uScript_AddMessage_speaker_90 = messageSpeaker;
		logic_uScript_AddMessage_Return_90 = logic_uScript_AddMessage_uScript_AddMessage_90.In(logic_uScript_AddMessage_messageData_90, logic_uScript_AddMessage_speaker_90);
		if (logic_uScript_AddMessage_uScript_AddMessage_90.Out)
		{
			Relay_In_79();
		}
	}

	private void Relay_In_95()
	{
		logic_uScript_AddMessage_messageData_95 = msg02BatteryFound;
		logic_uScript_AddMessage_speaker_95 = messageSpeaker;
		logic_uScript_AddMessage_Return_95 = logic_uScript_AddMessage_uScript_AddMessage_95.In(logic_uScript_AddMessage_messageData_95, logic_uScript_AddMessage_speaker_95);
		if (logic_uScript_AddMessage_uScript_AddMessage_95.Shown)
		{
			Relay_In_87();
		}
	}

	private void Relay_In_97()
	{
		logic_uScript_AddMessage_messageData_97 = msg03DestroyTree;
		logic_uScript_AddMessage_speaker_97 = messageSpeaker;
		logic_uScript_AddMessage_Return_97 = logic_uScript_AddMessage_uScript_AddMessage_97.In(logic_uScript_AddMessage_messageData_97, logic_uScript_AddMessage_speaker_97);
		if (logic_uScript_AddMessage_uScript_AddMessage_97.Out)
		{
			Relay_In_83();
		}
	}

	private void Relay_In_99()
	{
		logic_uScript_AddMessage_messageData_99 = msg07BatteryDestroyed;
		logic_uScript_AddMessage_speaker_99 = messageSpeaker;
		logic_uScript_AddMessage_Return_99 = logic_uScript_AddMessage_uScript_AddMessage_99.In(logic_uScript_AddMessage_messageData_99, logic_uScript_AddMessage_speaker_99);
		if (logic_uScript_AddMessage_uScript_AddMessage_99.Out)
		{
			Relay_In_28();
		}
	}

	private void Relay_In_104()
	{
		logic_uScript_AddMessage_messageData_104 = msg04PickUpBattery;
		logic_uScript_AddMessage_speaker_104 = messageSpeaker;
		logic_uScript_AddMessage_Return_104 = logic_uScript_AddMessage_uScript_AddMessage_104.In(logic_uScript_AddMessage_messageData_104, logic_uScript_AddMessage_speaker_104);
	}

	private void Relay_In_105()
	{
		logic_uScript_AddMessage_messageData_105 = msg05AttachBattery;
		logic_uScript_AddMessage_speaker_105 = messageSpeaker;
		logic_uScript_AddMessage_Return_105 = logic_uScript_AddMessage_uScript_AddMessage_105.In(logic_uScript_AddMessage_messageData_105, logic_uScript_AddMessage_speaker_105);
	}

	private void Relay_In_108()
	{
		logic_uScript_AddMessage_messageData_108 = msg06BatteryDropped;
		logic_uScript_AddMessage_speaker_108 = messageSpeaker;
		logic_uScript_AddMessage_Return_108 = logic_uScript_AddMessage_uScript_AddMessage_108.In(logic_uScript_AddMessage_messageData_108, logic_uScript_AddMessage_speaker_108);
	}

	private void Relay_In_113()
	{
		logic_uScript_AddMessage_messageData_113 = msg08BatteryAttached;
		logic_uScript_AddMessage_speaker_113 = messageSpeaker;
		logic_uScript_AddMessage_Return_113 = logic_uScript_AddMessage_uScript_AddMessage_113.In(logic_uScript_AddMessage_messageData_113, logic_uScript_AddMessage_speaker_113);
		if (logic_uScript_AddMessage_uScript_AddMessage_113.Shown)
		{
			Relay_In_143();
		}
	}

	private void Relay_In_114()
	{
		logic_uScript_PointArrowAtBlock_block_114 = local_BatteryBlock_TankBlock;
		logic_uScript_PointArrowAtBlock_uScript_PointArrowAtBlock_114.In(logic_uScript_PointArrowAtBlock_block_114, logic_uScript_PointArrowAtBlock_timeToShowFor_114, logic_uScript_PointArrowAtBlock_offset_114);
		if (logic_uScript_PointArrowAtBlock_uScript_PointArrowAtBlock_114.Out)
		{
			Relay_In_139();
		}
	}

	private void Relay_In_116()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_116.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_116.Out)
		{
			Relay_In_23();
		}
	}

	private void Relay_In_118()
	{
		logic_uScript_ShowHint_hintId_118 = local_122_GameHints_HintID;
		logic_uScript_ShowHint_uScript_ShowHint_118.In(logic_uScript_ShowHint_hintId_118);
		if (logic_uScript_ShowHint_uScript_ShowHint_118.Out)
		{
			Relay_In_121();
		}
	}

	private void Relay_Succeed_120()
	{
		logic_uScript_FinishEncounter_owner_120 = owner_Connection_117;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_120.Succeed(logic_uScript_FinishEncounter_owner_120);
	}

	private void Relay_Fail_120()
	{
		logic_uScript_FinishEncounter_owner_120 = owner_Connection_117;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_120.Fail(logic_uScript_FinishEncounter_owner_120);
	}

	private void Relay_In_121()
	{
		logic_uScript_HasHintBeenShownBefore_hintID_121 = local_124_GameHints_HintID;
		logic_uScript_HasHintBeenShownBefore_uScript_HasHintBeenShownBefore_121.In(logic_uScript_HasHintBeenShownBefore_hintID_121);
		bool shown = logic_uScript_HasHintBeenShownBefore_uScript_HasHintBeenShownBefore_121.Shown;
		bool notShown = logic_uScript_HasHintBeenShownBefore_uScript_HasHintBeenShownBefore_121.NotShown;
		if (shown)
		{
			Relay_Succeed_120();
		}
		if (notShown)
		{
			Relay_In_123();
		}
	}

	private void Relay_In_123()
	{
		logic_uScript_ShowHint_hintId_123 = local_124_GameHints_HintID;
		logic_uScript_ShowHint_uScript_ShowHint_123.In(logic_uScript_ShowHint_hintId_123);
		if (logic_uScript_ShowHint_uScript_ShowHint_123.Out)
		{
			Relay_Succeed_120();
		}
	}

	private void Relay_In_125()
	{
		logic_uScript_HasHintBeenShownBefore_hintID_125 = local_122_GameHints_HintID;
		logic_uScript_HasHintBeenShownBefore_uScript_HasHintBeenShownBefore_125.In(logic_uScript_HasHintBeenShownBefore_hintID_125);
		bool shown = logic_uScript_HasHintBeenShownBefore_uScript_HasHintBeenShownBefore_125.Shown;
		bool notShown = logic_uScript_HasHintBeenShownBefore_uScript_HasHintBeenShownBefore_125.NotShown;
		if (shown)
		{
			Relay_In_121();
		}
		if (notShown)
		{
			Relay_In_118();
		}
	}

	private void Relay_In_127()
	{
		logic_uScript_ShowHint_hintId_127 = local_128_GameHints_HintID;
		logic_uScript_ShowHint_uScript_ShowHint_127.In(logic_uScript_ShowHint_hintId_127);
		if (logic_uScript_ShowHint_uScript_ShowHint_127.Out)
		{
			Relay_In_125();
		}
	}

	private void Relay_In_129()
	{
		logic_uScript_HasHintBeenShownBefore_hintID_129 = local_128_GameHints_HintID;
		logic_uScript_HasHintBeenShownBefore_uScript_HasHintBeenShownBefore_129.In(logic_uScript_HasHintBeenShownBefore_hintID_129);
		bool shown = logic_uScript_HasHintBeenShownBefore_uScript_HasHintBeenShownBefore_129.Shown;
		bool notShown = logic_uScript_HasHintBeenShownBefore_uScript_HasHintBeenShownBefore_129.NotShown;
		if (shown)
		{
			Relay_In_125();
		}
		if (notShown)
		{
			Relay_In_127();
		}
	}

	private void Relay_In_130()
	{
		logic_uScript_RemoveScenery_ownerNode_130 = owner_Connection_131;
		logic_uScript_RemoveScenery_positionName_130 = clearSceneryPos;
		logic_uScript_RemoveScenery_radius_130 = clearSceneryRadius;
		logic_uScript_RemoveScenery_uScript_RemoveScenery_130.In(logic_uScript_RemoveScenery_ownerNode_130, logic_uScript_RemoveScenery_positionName_130, logic_uScript_RemoveScenery_radius_130, logic_uScript_RemoveScenery_preventChunksSpawning_130);
		if (logic_uScript_RemoveScenery_uScript_RemoveScenery_130.Out)
		{
			Relay_In_4();
		}
	}

	private void Relay_In_135()
	{
		logic_uScriptCon_CompareBool_Bool_135 = local_SceneryCleared_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_135.In(logic_uScriptCon_CompareBool_Bool_135);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_135.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_135.False;
		if (num)
		{
			Relay_In_4();
		}
		if (flag)
		{
			Relay_True_136();
		}
	}

	private void Relay_True_136()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_136.True(out logic_uScriptAct_SetBool_Target_136);
		local_SceneryCleared_System_Boolean = logic_uScriptAct_SetBool_Target_136;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_136.Out)
		{
			Relay_In_130();
		}
	}

	private void Relay_False_136()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_136.False(out logic_uScriptAct_SetBool_Target_136);
		local_SceneryCleared_System_Boolean = logic_uScriptAct_SetBool_Target_136;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_136.Out)
		{
			Relay_In_130();
		}
	}

	private void Relay_Save_Out_138()
	{
		Relay_Save_54();
	}

	private void Relay_Load_Out_138()
	{
		Relay_Load_54();
	}

	private void Relay_Restart_Out_138()
	{
		Relay_Set_False_54();
	}

	private void Relay_Save_138()
	{
		logic_SubGraph_SaveLoadBool_boolean_138 = local_SceneryCleared_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_138 = local_SceneryCleared_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_138.Save(ref logic_SubGraph_SaveLoadBool_boolean_138, logic_SubGraph_SaveLoadBool_boolAsVariable_138, logic_SubGraph_SaveLoadBool_uniqueID_138);
	}

	private void Relay_Load_138()
	{
		logic_SubGraph_SaveLoadBool_boolean_138 = local_SceneryCleared_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_138 = local_SceneryCleared_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_138.Load(ref logic_SubGraph_SaveLoadBool_boolean_138, logic_SubGraph_SaveLoadBool_boolAsVariable_138, logic_SubGraph_SaveLoadBool_uniqueID_138);
	}

	private void Relay_Set_True_138()
	{
		logic_SubGraph_SaveLoadBool_boolean_138 = local_SceneryCleared_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_138 = local_SceneryCleared_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_138.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_138, logic_SubGraph_SaveLoadBool_boolAsVariable_138, logic_SubGraph_SaveLoadBool_uniqueID_138);
	}

	private void Relay_Set_False_138()
	{
		logic_SubGraph_SaveLoadBool_boolean_138 = local_SceneryCleared_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_138 = local_SceneryCleared_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_138.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_138, logic_SubGraph_SaveLoadBool_boolAsVariable_138, logic_SubGraph_SaveLoadBool_uniqueID_138);
	}

	private void Relay_In_139()
	{
		logic_uScript_EnableGlow_targetObject_139 = local_BatteryBlock_TankBlock;
		logic_uScript_EnableGlow_uScript_EnableGlow_139.In(logic_uScript_EnableGlow_targetObject_139, logic_uScript_EnableGlow_enable_139);
		if (logic_uScript_EnableGlow_uScript_EnableGlow_139.Out)
		{
			Relay_In_23();
		}
	}

	private void Relay_In_142()
	{
		logic_uScript_EnableGlow_targetObject_142 = local_BatteryBlock_TankBlock;
		logic_uScript_EnableGlow_uScript_EnableGlow_142.In(logic_uScript_EnableGlow_targetObject_142, logic_uScript_EnableGlow_enable_142);
	}

	private void Relay_In_143()
	{
		logic_uScript_IsCoreEncounterCompleted_uScript_IsCoreEncounterCompleted_143.In(logic_uScript_IsCoreEncounterCompleted_corp_143, logic_uScript_IsCoreEncounterCompleted_grade_143, logic_uScript_IsCoreEncounterCompleted_encounterName_143);
		bool num = logic_uScript_IsCoreEncounterCompleted_uScript_IsCoreEncounterCompleted_143.True;
		bool flag = logic_uScript_IsCoreEncounterCompleted_uScript_IsCoreEncounterCompleted_143.False;
		if (num)
		{
			Relay_Succeed_120();
		}
		if (flag)
		{
			Relay_In_129();
		}
	}
}
