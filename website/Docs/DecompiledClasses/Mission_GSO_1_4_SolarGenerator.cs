using System;
using UnityEngine;

[Serializable]
[FriendlyName("Untitled", "")]
[NodePath("Graphs")]
public class Mission_GSO_1_4_SolarGenerator : uScriptLogic
{
	private GameObject parentGameObject;

	private uScript_GUI thisScriptsOnGuiListener;

	private bool m_RegisteredForEvents;

	public SpawnBlockData[] blockDataSolarGen = new SpawnBlockData[0];

	public float clearSceneryRadius;

	public TankPreset completedSolGenRegenPreset;

	public GhostBlockSpawnData[] ghostBlockRegen = new GhostBlockSpawnData[0];

	private TankBlock[] local_120_TankBlockArray = new TankBlock[0];

	private TankBlock[] local_152_TankBlockArray = new TankBlock[0];

	private GameHints.HintID local_237_GameHints_HintID = GameHints.HintID.MissionLog;

	private GameHints.HintID local_240_GameHints_HintID = GameHints.HintID.ObjectiveMarker;

	private bool local_AddedQuestLog_System_Boolean;

	private bool local_DismantleBaseMsgShown_System_Boolean;

	private Vector3 local_EncounterPos_UnityEngine_Vector3 = new Vector3(0f, 0f, 0f);

	private TankBlock local_GhostBlockRegen_TankBlock;

	private bool local_GhostBlockRegenSpawned_System_Boolean;

	private bool local_MissionComplete_System_Boolean;

	private ManOnScreenMessages.OnScreenMessage local_Msg03AnchorSolarGenerator_Pad1_ManOnScreenMessages_OnScreenMessage;

	private ManOnScreenMessages.OnScreenMessage local_Msg03AnchorSolarGenerator_Pad2_ManOnScreenMessages_OnScreenMessage;

	private ManOnScreenMessages.OnScreenMessage local_Msg05RepairBubbleSpawned_ManOnScreenMessages_OnScreenMessage;

	private ManOnScreenMessages.OnScreenMessage local_Msg06PickUpRepairBubble_ManOnScreenMessages_OnScreenMessage;

	private ManOnScreenMessages.OnScreenMessage local_Msg07AttachRepairBubble_ManOnScreenMessages_OnScreenMessage;

	private ManOnScreenMessages.OnScreenMessage local_Msg07AttachRepairBubble_Pad_ManOnScreenMessages_OnScreenMessage;

	private ManOnScreenMessages.OnScreenMessage local_Msg08ReturnRepairBubble_ManOnScreenMessages_OnScreenMessage;

	private ManOnScreenMessages.OnScreenMessage local_Msg10MoveInsideRepairBubble_ManOnScreenMessages_OnScreenMessage;

	private ManOnScreenMessages.OnScreenMessage local_Msg13UnanchorSolarGenerator_ManOnScreenMessages_OnScreenMessage;

	private ManOnScreenMessages.OnScreenMessage local_Msg13UnanchorSolarGenerator_Pad1_ManOnScreenMessages_OnScreenMessage;

	private ManOnScreenMessages.OnScreenMessage local_Msg13UnanchorSolarGenerator_Pad2_ManOnScreenMessages_OnScreenMessage;

	private ManOnScreenMessages.OnScreenMessage local_Msg13UnanchorSolarGenerator_Pad3_ManOnScreenMessages_OnScreenMessage;

	private bool local_PlayerHealed_System_Boolean;

	private Tank local_PlayerTank_Tank;

	private bool local_RegenSpawnedMsgShown_System_Boolean;

	private TankBlock local_RepairBubbleBlock_TankBlock;

	private Tank local_RepairTech_Tank;

	private TankBlock local_Solar_Generator_Block_TankBlock;

	private Tank local_SolarGenTech_Tank;

	private bool local_SolGenFoundMsgShown_System_Boolean;

	private bool local_SolGenRegenPresetSet_System_Boolean;

	private bool local_SolGenSpawned_System_Boolean;

	private int local_Stage_System_Int32 = 1;

	public uScript_AddMessage.MessageSpeaker messageSpeaker;

	[Multiline(3)]
	public string messageTag = "";

	public uScript_AddMessage.MessageData msg01FindSolarGenerator;

	public uScript_AddMessage.MessageData msg02SolarGeneratorFound;

	public uScript_AddMessage.MessageData msg03AnchorSolarGenerator;

	public uScript_AddMessage.MessageData msg03AnchorSolarGenerator_Pad1;

	public uScript_AddMessage.MessageData msg03AnchorSolarGenerator_Pad2;

	public uScript_AddMessage.MessageData msg04SolarGeneratorAnchored;

	public uScript_AddMessage.MessageData msg05RepairBubbleSpawned;

	public uScript_AddMessage.MessageData msg06PickUpRepairBubble;

	public uScript_AddMessage.MessageData msg07AttachRepairBubble;

	public uScript_AddMessage.MessageData msg07AttachRepairBubble_Pad;

	public uScript_AddMessage.MessageData msg08ReturnRepairBubble;

	public uScript_AddMessage.MessageData msg09RepairBubbleAttached;

	public uScript_AddMessage.MessageData msg10MoveInsideRepairBubble;

	public uScript_AddMessage.MessageData msg11PlayerHealed;

	public uScript_AddMessage.MessageData msg12DismantleBase;

	public uScript_AddMessage.MessageData msg13UnanchorSolarGenerator;

	public uScript_AddMessage.MessageData msg13UnanchorSolarGenerator_Pad1;

	public uScript_AddMessage.MessageData msg13UnanchorSolarGenerator_Pad2;

	public uScript_AddMessage.MessageData msg13UnanchorSolarGenerator_Pad3;

	public uScript_AddMessage.MessageData msg14FindOtherParts;

	public Transform particles;

	[Multiline(3)]
	public string PositionId = "";

	private GameObject owner_Connection_5;

	private GameObject owner_Connection_6;

	private GameObject owner_Connection_8;

	private GameObject owner_Connection_9;

	private GameObject owner_Connection_17;

	private GameObject owner_Connection_27;

	private GameObject owner_Connection_43;

	private GameObject owner_Connection_50;

	private GameObject owner_Connection_53;

	private GameObject owner_Connection_68;

	private GameObject owner_Connection_74;

	private GameObject owner_Connection_85;

	private GameObject owner_Connection_116;

	private GameObject owner_Connection_121;

	private GameObject owner_Connection_142;

	private GameObject owner_Connection_149;

	private GameObject owner_Connection_159;

	private GameObject owner_Connection_164;

	private GameObject owner_Connection_397;

	private GameObject owner_Connection_399;

	private GameObject owner_Connection_401;

	private GameObject owner_Connection_406;

	private uScriptCon_ManualSwitch logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_0 = new uScriptCon_ManualSwitch();

	private int logic_uScriptCon_ManualSwitch_CurrentOutput_0;

	private uScript_FireParticlesTowardsGround logic_uScript_FireParticlesTowardsGround_uScript_FireParticlesTowardsGround_4 = new uScript_FireParticlesTowardsGround();

	private Vector3 logic_uScript_FireParticlesTowardsGround_groundPos_4;

	private Transform logic_uScript_FireParticlesTowardsGround_particleEffect_4;

	private GameObject logic_uScript_FireParticlesTowardsGround_owner_4;

	private string logic_uScript_FireParticlesTowardsGround_uniqueName_4 = "fireRain";

	private Transform logic_uScript_FireParticlesTowardsGround_Return_4;

	private bool logic_uScript_FireParticlesTowardsGround_Delivered_4 = true;

	private bool logic_uScript_FireParticlesTowardsGround_Out_4 = true;

	private uScript_SpawnBlockAbovePlayer logic_uScript_SpawnBlockAbovePlayer_uScript_SpawnBlockAbovePlayer_10 = new uScript_SpawnBlockAbovePlayer();

	private BlockTypes logic_uScript_SpawnBlockAbovePlayer_blockType_10 = BlockTypes.GSORegen_111;

	private string logic_uScript_SpawnBlockAbovePlayer_uniqueName_10 = "regen";

	private GameObject logic_uScript_SpawnBlockAbovePlayer_owner_10;

	private TankBlock logic_uScript_SpawnBlockAbovePlayer_Return_10;

	private bool logic_uScript_SpawnBlockAbovePlayer_Out_10 = true;

	private uScript_PausePopulation logic_uScript_PausePopulation_uScript_PausePopulation_12 = new uScript_PausePopulation();

	private bool logic_uScript_PausePopulation_Out_12 = true;

	private uScript_KeepBlockInvulnerable logic_uScript_KeepBlockInvulnerable_uScript_KeepBlockInvulnerable_13 = new uScript_KeepBlockInvulnerable();

	private TankBlock logic_uScript_KeepBlockInvulnerable_block_13;

	private bool logic_uScript_KeepBlockInvulnerable_Out_13 = true;

	private uScript_GetPositionInEncounter logic_uScript_GetPositionInEncounter_uScript_GetPositionInEncounter_15 = new uScript_GetPositionInEncounter();

	private GameObject logic_uScript_GetPositionInEncounter_ownerNode_15;

	private string logic_uScript_GetPositionInEncounter_posName_15 = "";

	private Vector3 logic_uScript_GetPositionInEncounter_Return_15;

	private bool logic_uScript_GetPositionInEncounter_Out_15 = true;

	private uScript_IsBlockAnchored logic_uScript_IsBlockAnchored_uScript_IsBlockAnchored_25 = new uScript_IsBlockAnchored();

	private TankBlock logic_uScript_IsBlockAnchored_block_25;

	private bool logic_uScript_IsBlockAnchored_Out_25 = true;

	private bool logic_uScript_IsBlockAnchored_True_25 = true;

	private bool logic_uScript_IsBlockAnchored_False_25 = true;

	private uScript_UpdateEncounterPosFromBlock logic_uScript_UpdateEncounterPosFromBlock_uScript_UpdateEncounterPosFromBlock_28 = new uScript_UpdateEncounterPosFromBlock();

	private GameObject logic_uScript_UpdateEncounterPosFromBlock_ownerNode_28;

	private TankBlock logic_uScript_UpdateEncounterPosFromBlock_block_28;

	private bool logic_uScript_UpdateEncounterPosFromBlock_Out_28 = true;

	private uScript_IsPlayerInteractingWithBlock logic_uScript_IsPlayerInteractingWithBlock_uScript_IsPlayerInteractingWithBlock_29 = new uScript_IsPlayerInteractingWithBlock();

	private TankBlock logic_uScript_IsPlayerInteractingWithBlock_block_29;

	private bool logic_uScript_IsPlayerInteractingWithBlock_Out_29 = true;

	private bool logic_uScript_IsPlayerInteractingWithBlock_Interacted_29 = true;

	private bool logic_uScript_IsPlayerInteractingWithBlock_NotInteracted_29 = true;

	private bool logic_uScript_IsPlayerInteractingWithBlock_Dragging_29 = true;

	private bool logic_uScript_IsPlayerInteractingWithBlock_NotDragging_29 = true;

	private uScript_GetPlayerTankWithBlock logic_uScript_GetPlayerTankWithBlock_uScript_GetPlayerTankWithBlock_30 = new uScript_GetPlayerTankWithBlock();

	private BlockTypes logic_uScript_GetPlayerTankWithBlock_block_30;

	private TankBlock logic_uScript_GetPlayerTankWithBlock_tankBlock_30;

	private bool logic_uScript_GetPlayerTankWithBlock_useBlockType_30;

	private Tank logic_uScript_GetPlayerTankWithBlock_Return_30;

	private bool logic_uScript_GetPlayerTankWithBlock_Returned_30 = true;

	private bool logic_uScript_GetPlayerTankWithBlock_NotReturned_30 = true;

	private bool logic_uScript_GetPlayerTankWithBlock_Out_30 = true;

	private uScript_DoesTankHave logic_uScript_DoesTankHave_uScript_DoesTankHave_32 = new uScript_DoesTankHave();

	private Tank logic_uScript_DoesTankHave_tank_32;

	private int logic_uScript_DoesTankHave_amountOfPieces_32 = 1;

	private BlockTypes logic_uScript_DoesTankHave_block_32 = BlockTypes.GSORegen_111;

	private bool logic_uScript_DoesTankHave_checkForAmountOnly_32;

	private bool logic_uScript_DoesTankHave_Out_32 = true;

	private bool logic_uScript_DoesTankHave_True_32 = true;

	private bool logic_uScript_DoesTankHave_False_32 = true;

	private uScript_GetPlayerTankWithBlock logic_uScript_GetPlayerTankWithBlock_uScript_GetPlayerTankWithBlock_38 = new uScript_GetPlayerTankWithBlock();

	private BlockTypes logic_uScript_GetPlayerTankWithBlock_block_38 = BlockTypes.GSORegen_111;

	private TankBlock logic_uScript_GetPlayerTankWithBlock_tankBlock_38;

	private bool logic_uScript_GetPlayerTankWithBlock_useBlockType_38;

	private Tank logic_uScript_GetPlayerTankWithBlock_Return_38;

	private bool logic_uScript_GetPlayerTankWithBlock_Returned_38 = true;

	private bool logic_uScript_GetPlayerTankWithBlock_NotReturned_38 = true;

	private bool logic_uScript_GetPlayerTankWithBlock_Out_38 = true;

	private uScript_IsPlayerInRangeOfBlock logic_uScript_IsPlayerInRangeOfBlock_uScript_IsPlayerInRangeOfBlock_39 = new uScript_IsPlayerInRangeOfBlock();

	private TankBlock logic_uScript_IsPlayerInRangeOfBlock_block_39;

	private float logic_uScript_IsPlayerInRangeOfBlock_range_39 = 5f;

	private bool logic_uScript_IsPlayerInRangeOfBlock_Out_39 = true;

	private bool logic_uScript_IsPlayerInRangeOfBlock_InRange_39 = true;

	private bool logic_uScript_IsPlayerInRangeOfBlock_OutOfRange_39 = true;

	private uScript_MoveEncounterWithVisible logic_uScript_MoveEncounterWithVisible_uScript_MoveEncounterWithVisible_44 = new uScript_MoveEncounterWithVisible();

	private GameObject logic_uScript_MoveEncounterWithVisible_ownerNode_44;

	private object logic_uScript_MoveEncounterWithVisible_visibleObject_44 = "";

	private bool logic_uScript_MoveEncounterWithVisible_Out_44 = true;

	private uScript_IsPlayerInRangeOfBlock logic_uScript_IsPlayerInRangeOfBlock_uScript_IsPlayerInRangeOfBlock_45 = new uScript_IsPlayerInRangeOfBlock();

	private TankBlock logic_uScript_IsPlayerInRangeOfBlock_block_45;

	private float logic_uScript_IsPlayerInRangeOfBlock_range_45 = 50f;

	private bool logic_uScript_IsPlayerInRangeOfBlock_Out_45 = true;

	private bool logic_uScript_IsPlayerInRangeOfBlock_InRange_45 = true;

	private bool logic_uScript_IsPlayerInRangeOfBlock_OutOfRange_45 = true;

	private uScriptCon_CompareInt logic_uScriptCon_CompareInt_uScriptCon_CompareInt_47 = new uScriptCon_CompareInt();

	private int logic_uScriptCon_CompareInt_A_47;

	private int logic_uScriptCon_CompareInt_B_47 = 3;

	private bool logic_uScriptCon_CompareInt_GreaterThan_47 = true;

	private bool logic_uScriptCon_CompareInt_GreaterThanOrEqualTo_47 = true;

	private bool logic_uScriptCon_CompareInt_EqualTo_47 = true;

	private bool logic_uScriptCon_CompareInt_NotEqualTo_47 = true;

	private bool logic_uScriptCon_CompareInt_LessThanOrEqualTo_47 = true;

	private bool logic_uScriptCon_CompareInt_LessThan_47 = true;

	private uScript_MoveEncounterWithVisible logic_uScript_MoveEncounterWithVisible_uScript_MoveEncounterWithVisible_49 = new uScript_MoveEncounterWithVisible();

	private GameObject logic_uScript_MoveEncounterWithVisible_ownerNode_49;

	private object logic_uScript_MoveEncounterWithVisible_visibleObject_49 = "";

	private bool logic_uScript_MoveEncounterWithVisible_Out_49 = true;

	private uScript_SpawnBlockAbovePlayer logic_uScript_SpawnBlockAbovePlayer_uScript_SpawnBlockAbovePlayer_52 = new uScript_SpawnBlockAbovePlayer();

	private BlockTypes logic_uScript_SpawnBlockAbovePlayer_blockType_52 = BlockTypes.GSORegen_111;

	private string logic_uScript_SpawnBlockAbovePlayer_uniqueName_52 = "regen";

	private GameObject logic_uScript_SpawnBlockAbovePlayer_owner_52;

	private TankBlock logic_uScript_SpawnBlockAbovePlayer_Return_52;

	private bool logic_uScript_SpawnBlockAbovePlayer_Out_52 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_55 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_55 = 3;

	private int logic_uScriptAct_SetInt_Target_55;

	private bool logic_uScriptAct_SetInt_Out_55 = true;

	private uScript_GetPlayerTank logic_uScript_GetPlayerTank_uScript_GetPlayerTank_58 = new uScript_GetPlayerTank();

	private Tank logic_uScript_GetPlayerTank_Return_58;

	private bool logic_uScript_GetPlayerTank_Returned_58 = true;

	private bool logic_uScript_GetPlayerTank_NotReturned_58 = true;

	private uScript_IsShieldBlockPowered logic_uScript_IsShieldBlockPowered_uScript_IsShieldBlockPowered_59 = new uScript_IsShieldBlockPowered();

	private TankBlock logic_uScript_IsShieldBlockPowered_shieldBlock_59;

	private bool logic_uScript_IsShieldBlockPowered_True_59 = true;

	private bool logic_uScript_IsShieldBlockPowered_False_59 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_62 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_62 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_62 = true;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_62 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_64 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_64;

	private bool logic_uScriptCon_CompareBool_True_64 = true;

	private bool logic_uScriptCon_CompareBool_False_64 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_65 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_65;

	private bool logic_uScriptAct_SetBool_Out_65 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_65 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_65 = true;

	private uScript_PlayerInRangeOfCurrentEncounter logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_66 = new uScript_PlayerInRangeOfCurrentEncounter();

	private GameObject logic_uScript_PlayerInRangeOfCurrentEncounter_ownerNode_66;

	private bool logic_uScript_PlayerInRangeOfCurrentEncounter_Out_66 = true;

	private bool logic_uScript_PlayerInRangeOfCurrentEncounter_True_66 = true;

	private bool logic_uScript_PlayerInRangeOfCurrentEncounter_False_66 = true;

	private uScript_Wait logic_uScript_Wait_uScript_Wait_67 = new uScript_Wait();

	private float logic_uScript_Wait_seconds_67 = 2f;

	private bool logic_uScript_Wait_repeat_67;

	private bool logic_uScript_Wait_Waited_67 = true;

	private uScript_SetQuestLogVisible logic_uScript_SetQuestLogVisible_uScript_SetQuestLogVisible_73 = new uScript_SetQuestLogVisible();

	private bool logic_uScript_SetQuestLogVisible_visible_73 = true;

	private bool logic_uScript_SetQuestLogVisible_Out_73 = true;

	private uScript_SetTechBlocksHealth logic_uScript_SetTechBlocksHealth_uScript_SetTechBlocksHealth_76 = new uScript_SetTechBlocksHealth();

	private Tank logic_uScript_SetTechBlocksHealth_tech_76;

	private float logic_uScript_SetTechBlocksHealth_healthPercentage_76 = 60f;

	private bool logic_uScript_SetTechBlocksHealth_Out_76 = true;

	private uScript_IsCoreEncounterCompleted logic_uScript_IsCoreEncounterCompleted_uScript_IsCoreEncounterCompleted_78 = new uScript_IsCoreEncounterCompleted();

	private FactionSubTypes logic_uScript_IsCoreEncounterCompleted_corp_78 = FactionSubTypes.GSO;

	private int logic_uScript_IsCoreEncounterCompleted_grade_78 = 1;

	private string logic_uScript_IsCoreEncounterCompleted_encounterName_78 = "1-3 First Fight";

	private bool logic_uScript_IsCoreEncounterCompleted_True_78 = true;

	private bool logic_uScript_IsCoreEncounterCompleted_False_78 = true;

	private uScript_KeepBlockInvulnerable logic_uScript_KeepBlockInvulnerable_uScript_KeepBlockInvulnerable_80 = new uScript_KeepBlockInvulnerable();

	private TankBlock logic_uScript_KeepBlockInvulnerable_block_80;

	private bool logic_uScript_KeepBlockInvulnerable_Out_80 = true;

	private uScript_SetMissionsVisibleInHud logic_uScript_SetMissionsVisibleInHud_uScript_SetMissionsVisibleInHud_81 = new uScript_SetMissionsVisibleInHud();

	private bool logic_uScript_SetMissionsVisibleInHud_visible_81 = true;

	private bool logic_uScript_SetMissionsVisibleInHud_Out_81 = true;

	private SubGraph_SaveLoadInt logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_82 = new SubGraph_SaveLoadInt();

	private int logic_SubGraph_SaveLoadInt_restartValue_82 = 1;

	private int logic_SubGraph_SaveLoadInt_integer_82;

	private object logic_SubGraph_SaveLoadInt_intAsVariable_82 = "";

	private string logic_SubGraph_SaveLoadInt_uniqueID_82 = "Stage";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_83 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_83;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_83 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_83 = "AddedQuestLog";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_89 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_89;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_89 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_89 = "SolGenFoundMsgShown";

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_90 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_90;

	private bool logic_uScriptAct_SetBool_Out_90 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_90 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_90 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_92 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_92;

	private bool logic_uScriptCon_CompareBool_True_92 = true;

	private bool logic_uScriptCon_CompareBool_False_92 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_95 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_95;

	private bool logic_uScriptCon_CompareBool_True_95 = true;

	private bool logic_uScriptCon_CompareBool_False_95 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_96 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_96;

	private bool logic_uScriptAct_SetBool_Out_96 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_96 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_96 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_99 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_99;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_99 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_99 = "RegenSpawnedMsgShown";

	private uScript_PausePopulation logic_uScript_PausePopulation_uScript_PausePopulation_100 = new uScript_PausePopulation();

	private bool logic_uScript_PausePopulation_Out_100 = true;

	private uScript_LockTech logic_uScript_LockTech_uScript_LockTech_102 = new uScript_LockTech();

	private Tank logic_uScript_LockTech_tech_102;

	private uScript_LockTech.TechLockType logic_uScript_LockTech_lockType_102 = uScript_LockTech.TechLockType.LockDetachAndInteraction;

	private bool logic_uScript_LockTech_Out_102 = true;

	private uScript_GetPlayerTankWithBlock logic_uScript_GetPlayerTankWithBlock_uScript_GetPlayerTankWithBlock_103 = new uScript_GetPlayerTankWithBlock();

	private BlockTypes logic_uScript_GetPlayerTankWithBlock_block_103;

	private TankBlock logic_uScript_GetPlayerTankWithBlock_tankBlock_103;

	private bool logic_uScript_GetPlayerTankWithBlock_useBlockType_103;

	private Tank logic_uScript_GetPlayerTankWithBlock_Return_103;

	private bool logic_uScript_GetPlayerTankWithBlock_Returned_103 = true;

	private bool logic_uScript_GetPlayerTankWithBlock_NotReturned_103 = true;

	private bool logic_uScript_GetPlayerTankWithBlock_Out_103 = true;

	private uScript_GetTankBlock logic_uScript_GetTankBlock_uScript_GetTankBlock_106 = new uScript_GetTankBlock();

	private Tank logic_uScript_GetTankBlock_tank_106;

	private BlockTypes logic_uScript_GetTankBlock_blockType_106 = BlockTypes.GSORegen_111;

	private TankBlock logic_uScript_GetTankBlock_Return_106;

	private bool logic_uScript_GetTankBlock_Out_106 = true;

	private bool logic_uScript_GetTankBlock_Returned_106 = true;

	private bool logic_uScript_GetTankBlock_NotFound_106 = true;

	private uScript_GetNamedBlock logic_uScript_GetNamedBlock_uScript_GetNamedBlock_109 = new uScript_GetNamedBlock();

	private string logic_uScript_GetNamedBlock_name_109 = "regen";

	private GameObject logic_uScript_GetNamedBlock_owner_109;

	private TankBlock logic_uScript_GetNamedBlock_Return_109;

	private bool logic_uScript_GetNamedBlock_Out_109 = true;

	private bool logic_uScript_GetNamedBlock_Destroyed_109 = true;

	private bool logic_uScript_GetNamedBlock_BlockExists_109 = true;

	private bool logic_uScript_GetNamedBlock_WaitingForBlock_109 = true;

	private bool logic_uScript_GetNamedBlock_NoBlock_109 = true;

	private uScript_PointArrowAtVisible logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_110 = new uScript_PointArrowAtVisible();

	private object logic_uScript_PointArrowAtVisible_targetObject_110 = "";

	private float logic_uScript_PointArrowAtVisible_timeToShowFor_110 = 0.1f;

	private Vector3 logic_uScript_PointArrowAtVisible_offset_110 = new Vector3(0f, 0f, 0f);

	private bool logic_uScript_PointArrowAtVisible_Out_110 = true;

	private uScript_PointArrowAtVisible logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_112 = new uScript_PointArrowAtVisible();

	private object logic_uScript_PointArrowAtVisible_targetObject_112 = "";

	private float logic_uScript_PointArrowAtVisible_timeToShowFor_112 = 0.1f;

	private Vector3 logic_uScript_PointArrowAtVisible_offset_112 = new Vector3(0f, 0f, 0f);

	private bool logic_uScript_PointArrowAtVisible_Out_112 = true;

	private uScript_HideArrow logic_uScript_HideArrow_uScript_HideArrow_114 = new uScript_HideArrow();

	private bool logic_uScript_HideArrow_Out_114 = true;

	private uScript_DirectEnemiesOutOfEncounter logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_115 = new uScript_DirectEnemiesOutOfEncounter();

	private GameObject logic_uScript_DirectEnemiesOutOfEncounter_ownerNode_115;

	private bool logic_uScript_DirectEnemiesOutOfEncounter_Out_115 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_118 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_118;

	private bool logic_uScriptCon_CompareBool_True_118 = true;

	private bool logic_uScriptCon_CompareBool_False_118 = true;

	private uScript_AccessListBlock logic_uScript_AccessListBlock_uScript_AccessListBlock_119 = new uScript_AccessListBlock();

	private TankBlock[] logic_uScript_AccessListBlock_blockList_119 = new TankBlock[0];

	private int logic_uScript_AccessListBlock_index_119;

	private TankBlock logic_uScript_AccessListBlock_value_119;

	private bool logic_uScript_AccessListBlock_Out_119 = true;

	private uScript_PointArrowAtVisible logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_122 = new uScript_PointArrowAtVisible();

	private object logic_uScript_PointArrowAtVisible_targetObject_122 = "";

	private float logic_uScript_PointArrowAtVisible_timeToShowFor_122 = 0.1f;

	private Vector3 logic_uScript_PointArrowAtVisible_offset_122 = new Vector3(0f, 0f, 0f);

	private bool logic_uScript_PointArrowAtVisible_Out_122 = true;

	private uScript_SpawnGhostBlocks logic_uScript_SpawnGhostBlocks_uScript_SpawnGhostBlocks_124 = new uScript_SpawnGhostBlocks();

	private GhostBlockSpawnData[] logic_uScript_SpawnGhostBlocks_ghostBlockData_124 = new GhostBlockSpawnData[0];

	private GameObject logic_uScript_SpawnGhostBlocks_ownerNode_124;

	private Tank logic_uScript_SpawnGhostBlocks_targetTech_124;

	private TankBlock[] logic_uScript_SpawnGhostBlocks_Return_124;

	private bool logic_uScript_SpawnGhostBlocks_OnAlreadySpawned_124 = true;

	private bool logic_uScript_SpawnGhostBlocks_OnSpawned_124 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_125 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_125;

	private bool logic_uScriptAct_SetBool_Out_125 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_125 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_125 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_128 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_128;

	private bool logic_uScriptAct_SetBool_Out_128 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_128 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_128 = true;

	private uScript_RemoveAllGhostBlocksOnTech logic_uScript_RemoveAllGhostBlocksOnTech_uScript_RemoveAllGhostBlocksOnTech_130 = new uScript_RemoveAllGhostBlocksOnTech();

	private Tank logic_uScript_RemoveAllGhostBlocksOnTech_targetTech_130;

	private bool logic_uScript_RemoveAllGhostBlocksOnTech_Out_130 = true;

	private uScript_LockBlockAttach logic_uScript_LockBlockAttach_uScript_LockBlockAttach_132 = new uScript_LockBlockAttach();

	private TankBlock logic_uScript_LockBlockAttach_block_132;

	private bool logic_uScript_LockBlockAttach_Out_132 = true;

	private uScript_LockTutorialBlockAttach logic_uScript_LockTutorialBlockAttach_uScript_LockTutorialBlockAttach_133 = new uScript_LockTutorialBlockAttach();

	private TankBlock logic_uScript_LockTutorialBlockAttach_block_133;

	private bool logic_uScript_LockTutorialBlockAttach_Out_133 = true;

	private uScript_ChangeBuildingOptions logic_uScript_ChangeBuildingOptions_uScript_ChangeBuildingOptions_135 = new uScript_ChangeBuildingOptions();

	private uScript_ChangeBuildingOptions.BuildingOptions logic_uScript_ChangeBuildingOptions_change_135;

	private bool logic_uScript_ChangeBuildingOptions_allow_135;

	private bool logic_uScript_ChangeBuildingOptions_Out_135 = true;

	private uScript_ChangeBuildingOptions logic_uScript_ChangeBuildingOptions_uScript_ChangeBuildingOptions_136 = new uScript_ChangeBuildingOptions();

	private uScript_ChangeBuildingOptions.BuildingOptions logic_uScript_ChangeBuildingOptions_change_136;

	private bool logic_uScript_ChangeBuildingOptions_allow_136 = true;

	private bool logic_uScript_ChangeBuildingOptions_Out_136 = true;

	private uScript_ChangeBuildingOptions logic_uScript_ChangeBuildingOptions_uScript_ChangeBuildingOptions_137 = new uScript_ChangeBuildingOptions();

	private uScript_ChangeBuildingOptions.BuildingOptions logic_uScript_ChangeBuildingOptions_change_137;

	private bool logic_uScript_ChangeBuildingOptions_allow_137;

	private bool logic_uScript_ChangeBuildingOptions_Out_137 = true;

	private uScript_ChangeBuildingOptions logic_uScript_ChangeBuildingOptions_uScript_ChangeBuildingOptions_138 = new uScript_ChangeBuildingOptions();

	private uScript_ChangeBuildingOptions.BuildingOptions logic_uScript_ChangeBuildingOptions_change_138;

	private bool logic_uScript_ChangeBuildingOptions_allow_138 = true;

	private bool logic_uScript_ChangeBuildingOptions_Out_138 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_139 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_139 = true;

	private uScript_SpawnBlocksFromData logic_uScript_SpawnBlocksFromData_uScript_SpawnBlocksFromData_140 = new uScript_SpawnBlocksFromData();

	private SpawnBlockData[] logic_uScript_SpawnBlocksFromData_blockData_140 = new SpawnBlockData[0];

	private GameObject logic_uScript_SpawnBlocksFromData_ownerNode_140;

	private bool logic_uScript_SpawnBlocksFromData_Out_140 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_143 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_143;

	private bool logic_uScriptCon_CompareBool_True_143 = true;

	private bool logic_uScriptCon_CompareBool_False_143 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_144 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_144;

	private bool logic_uScriptAct_SetBool_Out_144 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_144 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_144 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_147 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_147;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_147 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_147 = "SolGenSpawned";

	private uScript_GetAndCheckBlocks logic_uScript_GetAndCheckBlocks_uScript_GetAndCheckBlocks_148 = new uScript_GetAndCheckBlocks();

	private SpawnBlockData[] logic_uScript_GetAndCheckBlocks_blockData_148 = new SpawnBlockData[0];

	private GameObject logic_uScript_GetAndCheckBlocks_ownerNode_148;

	private TankBlock[] logic_uScript_GetAndCheckBlocks_blocks_148 = new TankBlock[0];

	private bool logic_uScript_GetAndCheckBlocks_AllAlive_148 = true;

	private bool logic_uScript_GetAndCheckBlocks_SomeAlive_148 = true;

	private bool logic_uScript_GetAndCheckBlocks_AllDead_148 = true;

	private bool logic_uScript_GetAndCheckBlocks_WaitingToSpawn_148 = true;

	private uScript_AccessListBlock logic_uScript_AccessListBlock_uScript_AccessListBlock_151 = new uScript_AccessListBlock();

	private TankBlock[] logic_uScript_AccessListBlock_blockList_151 = new TankBlock[0];

	private int logic_uScript_AccessListBlock_index_151;

	private TankBlock logic_uScript_AccessListBlock_value_151;

	private bool logic_uScript_AccessListBlock_Out_151 = true;

	private uScript_GetPlayerTankWithBlock logic_uScript_GetPlayerTankWithBlock_uScript_GetPlayerTankWithBlock_155 = new uScript_GetPlayerTankWithBlock();

	private BlockTypes logic_uScript_GetPlayerTankWithBlock_block_155;

	private TankBlock logic_uScript_GetPlayerTankWithBlock_tankBlock_155;

	private bool logic_uScript_GetPlayerTankWithBlock_useBlockType_155;

	private Tank logic_uScript_GetPlayerTankWithBlock_Return_155;

	private bool logic_uScript_GetPlayerTankWithBlock_Returned_155 = true;

	private bool logic_uScript_GetPlayerTankWithBlock_NotReturned_155 = true;

	private bool logic_uScript_GetPlayerTankWithBlock_Out_155 = true;

	private uScript_LockTech logic_uScript_LockTech_uScript_LockTech_156 = new uScript_LockTech();

	private Tank logic_uScript_LockTech_tech_156;

	private uScript_LockTech.TechLockType logic_uScript_LockTech_lockType_156 = uScript_LockTech.TechLockType.LockDetachAndInteraction;

	private bool logic_uScript_LockTech_Out_156 = true;

	private uScript_RemoveScenery logic_uScript_RemoveScenery_uScript_RemoveScenery_158 = new uScript_RemoveScenery();

	private GameObject logic_uScript_RemoveScenery_ownerNode_158;

	private string logic_uScript_RemoveScenery_positionName_158 = "";

	private float logic_uScript_RemoveScenery_radius_158;

	private bool logic_uScript_RemoveScenery_preventChunksSpawning_158 = true;

	private bool logic_uScript_RemoveScenery_Out_158 = true;

	private uScript_ClearTutorialTechToBuild logic_uScript_ClearTutorialTechToBuild_uScript_ClearTutorialTechToBuild_162 = new uScript_ClearTutorialTechToBuild();

	private bool logic_uScript_ClearTutorialTechToBuild_Out_162 = true;

	private uScript_ShowQuestLog logic_uScript_ShowQuestLog_uScript_ShowQuestLog_163 = new uScript_ShowQuestLog();

	private GameObject logic_uScript_ShowQuestLog_owner_163;

	private bool logic_uScript_ShowQuestLog_Out_163 = true;

	private uScript_FinishEncounter logic_uScript_FinishEncounter_uScript_FinishEncounter_165 = new uScript_FinishEncounter();

	private GameObject logic_uScript_FinishEncounter_owner_165;

	private bool logic_uScript_FinishEncounter_Out_165 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_167 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_167 = true;

	private SubGraph_LoadObjectiveStates logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_169 = new SubGraph_LoadObjectiveStates();

	private int logic_SubGraph_LoadObjectiveStates_currentObjective_169;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_172 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_172;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_172 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_172 = "MissionComplete";

	private SubGraph_CompleteObjectiveStage logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_174 = new SubGraph_CompleteObjectiveStage();

	private int logic_SubGraph_CompleteObjectiveStage_objectiveStage_174;

	private bool logic_SubGraph_CompleteObjectiveStage_isFinalObjective_174;

	private SubGraph_CompleteObjectiveStage logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_175 = new SubGraph_CompleteObjectiveStage();

	private int logic_SubGraph_CompleteObjectiveStage_objectiveStage_175;

	private bool logic_SubGraph_CompleteObjectiveStage_isFinalObjective_175;

	private SubGraph_CompleteObjectiveStage logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_177 = new SubGraph_CompleteObjectiveStage();

	private int logic_SubGraph_CompleteObjectiveStage_objectiveStage_177;

	private bool logic_SubGraph_CompleteObjectiveStage_isFinalObjective_177;

	private uScript_ChangeBuildingOptions logic_uScript_ChangeBuildingOptions_uScript_ChangeBuildingOptions_179 = new uScript_ChangeBuildingOptions();

	private uScript_ChangeBuildingOptions.BuildingOptions logic_uScript_ChangeBuildingOptions_change_179;

	private bool logic_uScript_ChangeBuildingOptions_allow_179 = true;

	private bool logic_uScript_ChangeBuildingOptions_Out_179 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_180 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_180;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_180;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_180;

	private bool logic_uScript_AddMessage_Out_180 = true;

	private bool logic_uScript_AddMessage_Shown_180 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_183 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_183 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_184 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_184 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_186 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_186;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_186;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_186;

	private bool logic_uScript_AddMessage_Out_186 = true;

	private bool logic_uScript_AddMessage_Shown_186 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_190 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_190;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_190;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_190;

	private bool logic_uScript_AddMessage_Out_190 = true;

	private bool logic_uScript_AddMessage_Shown_190 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_193 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_193;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_193;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_193;

	private bool logic_uScript_AddMessage_Out_193 = true;

	private bool logic_uScript_AddMessage_Shown_193 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_194 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_194;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_194;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_194;

	private bool logic_uScript_AddMessage_Out_194 = true;

	private bool logic_uScript_AddMessage_Shown_194 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_197 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_197;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_197;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_197;

	private bool logic_uScript_AddMessage_Out_197 = true;

	private bool logic_uScript_AddMessage_Shown_197 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_202 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_202;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_202;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_202;

	private bool logic_uScript_AddMessage_Out_202 = true;

	private bool logic_uScript_AddMessage_Shown_202 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_205 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_205;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_205;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_205;

	private bool logic_uScript_AddMessage_Out_205 = true;

	private bool logic_uScript_AddMessage_Shown_205 = true;

	private uScript_IsCoreEncounterCompleted logic_uScript_IsCoreEncounterCompleted_uScript_IsCoreEncounterCompleted_206 = new uScript_IsCoreEncounterCompleted();

	private FactionSubTypes logic_uScript_IsCoreEncounterCompleted_corp_206 = FactionSubTypes.GSO;

	private int logic_uScript_IsCoreEncounterCompleted_grade_206 = 1;

	private string logic_uScript_IsCoreEncounterCompleted_encounterName_206 = "1-S Radar";

	private bool logic_uScript_IsCoreEncounterCompleted_True_206 = true;

	private bool logic_uScript_IsCoreEncounterCompleted_False_206 = true;

	private uScript_IsCoreEncounterCompleted logic_uScript_IsCoreEncounterCompleted_uScript_IsCoreEncounterCompleted_207 = new uScript_IsCoreEncounterCompleted();

	private FactionSubTypes logic_uScript_IsCoreEncounterCompleted_corp_207 = FactionSubTypes.GSO;

	private int logic_uScript_IsCoreEncounterCompleted_grade_207 = 1;

	private string logic_uScript_IsCoreEncounterCompleted_encounterName_207 = "1-S Battery";

	private bool logic_uScript_IsCoreEncounterCompleted_True_207 = true;

	private bool logic_uScript_IsCoreEncounterCompleted_False_207 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_210 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_210;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_210;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_210;

	private bool logic_uScript_AddMessage_Out_210 = true;

	private bool logic_uScript_AddMessage_Shown_210 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_211 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_211 = true;

	private uScript_SetTutorialTechToBuild logic_uScript_SetTutorialTechToBuild_uScript_SetTutorialTechToBuild_212 = new uScript_SetTutorialTechToBuild();

	private TankPreset logic_uScript_SetTutorialTechToBuild_completedTechPreset_212;

	private Tank logic_uScript_SetTutorialTechToBuild_tutorialBuildTech_212;

	private bool logic_uScript_SetTutorialTechToBuild_Out_212 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_214 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_214;

	private bool logic_uScriptAct_SetBool_Out_214 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_214 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_214 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_215 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_215;

	private bool logic_uScriptCon_CompareBool_True_215 = true;

	private bool logic_uScriptCon_CompareBool_False_215 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_218 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_218;

	private bool logic_uScriptAct_SetBool_Out_218 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_218 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_218 = true;

	private uScript_SetTutorialTechToBuild logic_uScript_SetTutorialTechToBuild_uScript_SetTutorialTechToBuild_221 = new uScript_SetTutorialTechToBuild();

	private TankPreset logic_uScript_SetTutorialTechToBuild_completedTechPreset_221;

	private Tank logic_uScript_SetTutorialTechToBuild_tutorialBuildTech_221;

	private bool logic_uScript_SetTutorialTechToBuild_Out_221 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_223 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_223;

	private bool logic_uScriptAct_SetBool_Out_223 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_223 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_223 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_225 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_225;

	private bool logic_uScriptCon_CompareBool_True_225 = true;

	private bool logic_uScriptCon_CompareBool_False_225 = true;

	private uScriptCon_CompareInt logic_uScriptCon_CompareInt_uScriptCon_CompareInt_226 = new uScriptCon_CompareInt();

	private int logic_uScriptCon_CompareInt_A_226;

	private int logic_uScriptCon_CompareInt_B_226 = 3;

	private bool logic_uScriptCon_CompareInt_GreaterThan_226 = true;

	private bool logic_uScriptCon_CompareInt_GreaterThanOrEqualTo_226 = true;

	private bool logic_uScriptCon_CompareInt_EqualTo_226 = true;

	private bool logic_uScriptCon_CompareInt_NotEqualTo_226 = true;

	private bool logic_uScriptCon_CompareInt_LessThanOrEqualTo_226 = true;

	private bool logic_uScriptCon_CompareInt_LessThan_226 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_228 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_228 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_229 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_229 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_232 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_232 = true;

	private uScript_HasHintBeenShownBefore logic_uScript_HasHintBeenShownBefore_uScript_HasHintBeenShownBefore_235 = new uScript_HasHintBeenShownBefore();

	private GameHints.HintID logic_uScript_HasHintBeenShownBefore_hintID_235;

	private bool logic_uScript_HasHintBeenShownBefore_Shown_235 = true;

	private bool logic_uScript_HasHintBeenShownBefore_NotShown_235 = true;

	private uScript_ShowHint logic_uScript_ShowHint_uScript_ShowHint_236 = new uScript_ShowHint();

	private GameHints.HintID logic_uScript_ShowHint_hintId_236;

	private bool logic_uScript_ShowHint_Out_236 = true;

	private uScript_ShowHint logic_uScript_ShowHint_uScript_ShowHint_238 = new uScript_ShowHint();

	private GameHints.HintID logic_uScript_ShowHint_hintId_238;

	private bool logic_uScript_ShowHint_Out_238 = true;

	private uScript_HasHintBeenShownBefore logic_uScript_HasHintBeenShownBefore_uScript_HasHintBeenShownBefore_239 = new uScript_HasHintBeenShownBefore();

	private GameHints.HintID logic_uScript_HasHintBeenShownBefore_hintID_239;

	private bool logic_uScript_HasHintBeenShownBefore_Shown_239 = true;

	private bool logic_uScript_HasHintBeenShownBefore_NotShown_239 = true;

	private uScript_Wait logic_uScript_Wait_uScript_Wait_242 = new uScript_Wait();

	private float logic_uScript_Wait_seconds_242 = 9f;

	private bool logic_uScript_Wait_repeat_242;

	private bool logic_uScript_Wait_Waited_242 = true;

	private uScript_IsCoreEncounterCompleted logic_uScript_IsCoreEncounterCompleted_uScript_IsCoreEncounterCompleted_243 = new uScript_IsCoreEncounterCompleted();

	private FactionSubTypes logic_uScript_IsCoreEncounterCompleted_corp_243 = FactionSubTypes.GSO;

	private int logic_uScript_IsCoreEncounterCompleted_grade_243 = 1;

	private string logic_uScript_IsCoreEncounterCompleted_encounterName_243 = "1-3 First Fight";

	private bool logic_uScript_IsCoreEncounterCompleted_True_243 = true;

	private bool logic_uScript_IsCoreEncounterCompleted_False_243 = true;

	private uScript_HideHint logic_uScript_HideHint_uScript_HideHint_244 = new uScript_HideHint();

	private GameHints.HintID logic_uScript_HideHint_hintId_244 = GameHints.HintID.ObjectiveMarker;

	private bool logic_uScript_HideHint_Out_244 = true;

	private uScript_IsHUDElementVisible logic_uScript_IsHUDElementVisible_uScript_IsHUDElementVisible_245 = new uScript_IsHUDElementVisible();

	private ManHUD.HUDElementType logic_uScript_IsHUDElementVisible_hudElement_245 = ManHUD.HUDElementType.MissionLog;

	private bool logic_uScript_IsHUDElementVisible_True_245 = true;

	private bool logic_uScript_IsHUDElementVisible_False_245 = true;

	private uScript_HideHint logic_uScript_HideHint_uScript_HideHint_246 = new uScript_HideHint();

	private GameHints.HintID logic_uScript_HideHint_hintId_246 = GameHints.HintID.MissionLog;

	private bool logic_uScript_HideHint_Out_246 = true;

	private uScript_IsShieldBlockPowered logic_uScript_IsShieldBlockPowered_uScript_IsShieldBlockPowered_248 = new uScript_IsShieldBlockPowered();

	private TankBlock logic_uScript_IsShieldBlockPowered_shieldBlock_248;

	private bool logic_uScript_IsShieldBlockPowered_True_248 = true;

	private bool logic_uScript_IsShieldBlockPowered_False_248 = true;

	private SubGraph_AddMessageWithPadSupport logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_249 = new SubGraph_AddMessageWithPadSupport();

	private uScript_AddMessage.MessageData logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboard_249;

	private uScript_AddMessage.MessageData logic_SubGraph_AddMessageWithPadSupport_messageControlPad_249;

	private uScript_AddMessage.MessageSpeaker logic_SubGraph_AddMessageWithPadSupport_speaker_249;

	private ManOnScreenMessages.OnScreenMessage logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboardReturn_249;

	private ManOnScreenMessages.OnScreenMessage logic_SubGraph_AddMessageWithPadSupport_messageControlPadReturn_249;

	private uScript_GetJoypadControlMode logic_uScript_GetJoypadControlMode_uScript_GetJoypadControlMode_253 = new uScript_GetJoypadControlMode();

	private bool logic_uScript_GetJoypadControlMode_Joypad_253 = true;

	private bool logic_uScript_GetJoypadControlMode_MouseAndKeyboard_253 = true;

	private uScript_GetJoypadControlMode logic_uScript_GetJoypadControlMode_uScript_GetJoypadControlMode_254 = new uScript_GetJoypadControlMode();

	private bool logic_uScript_GetJoypadControlMode_Joypad_254 = true;

	private bool logic_uScript_GetJoypadControlMode_MouseAndKeyboard_254 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_257 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_257;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_257;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_257;

	private bool logic_uScript_AddMessage_Out_257 = true;

	private bool logic_uScript_AddMessage_Shown_257 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_260 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_260;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_260;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_260;

	private bool logic_uScript_AddMessage_Out_260 = true;

	private bool logic_uScript_AddMessage_Shown_260 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_263 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_263;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_263;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_263;

	private bool logic_uScript_AddMessage_Out_263 = true;

	private bool logic_uScript_AddMessage_Shown_263 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_264 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_264 = true;

	private uScript_RemoveOnScreenMessage logic_uScript_RemoveOnScreenMessage_uScript_RemoveOnScreenMessage_268 = new uScript_RemoveOnScreenMessage();

	private ManOnScreenMessages.OnScreenMessage logic_uScript_RemoveOnScreenMessage_onScreenMessage_268;

	private bool logic_uScript_RemoveOnScreenMessage_instant_268;

	private bool logic_uScript_RemoveOnScreenMessage_Out_268 = true;

	private uScript_RemoveOnScreenMessage logic_uScript_RemoveOnScreenMessage_uScript_RemoveOnScreenMessage_271 = new uScript_RemoveOnScreenMessage();

	private ManOnScreenMessages.OnScreenMessage logic_uScript_RemoveOnScreenMessage_onScreenMessage_271;

	private bool logic_uScript_RemoveOnScreenMessage_instant_271;

	private bool logic_uScript_RemoveOnScreenMessage_Out_271 = true;

	private uScript_RemoveOnScreenMessage logic_uScript_RemoveOnScreenMessage_uScript_RemoveOnScreenMessage_273 = new uScript_RemoveOnScreenMessage();

	private ManOnScreenMessages.OnScreenMessage logic_uScript_RemoveOnScreenMessage_onScreenMessage_273;

	private bool logic_uScript_RemoveOnScreenMessage_instant_273;

	private bool logic_uScript_RemoveOnScreenMessage_Out_273 = true;

	private uScript_RemoveOnScreenMessage logic_uScript_RemoveOnScreenMessage_uScript_RemoveOnScreenMessage_277 = new uScript_RemoveOnScreenMessage();

	private ManOnScreenMessages.OnScreenMessage logic_uScript_RemoveOnScreenMessage_onScreenMessage_277;

	private bool logic_uScript_RemoveOnScreenMessage_instant_277;

	private bool logic_uScript_RemoveOnScreenMessage_Out_277 = true;

	private uScript_RemoveOnScreenMessage logic_uScript_RemoveOnScreenMessage_uScript_RemoveOnScreenMessage_281 = new uScript_RemoveOnScreenMessage();

	private ManOnScreenMessages.OnScreenMessage logic_uScript_RemoveOnScreenMessage_onScreenMessage_281;

	private bool logic_uScript_RemoveOnScreenMessage_instant_281;

	private bool logic_uScript_RemoveOnScreenMessage_Out_281 = true;

	private uScript_RemoveOnScreenMessage logic_uScript_RemoveOnScreenMessage_uScript_RemoveOnScreenMessage_285 = new uScript_RemoveOnScreenMessage();

	private ManOnScreenMessages.OnScreenMessage logic_uScript_RemoveOnScreenMessage_onScreenMessage_285;

	private bool logic_uScript_RemoveOnScreenMessage_instant_285;

	private bool logic_uScript_RemoveOnScreenMessage_Out_285 = true;

	private uScript_RemoveOnScreenMessage logic_uScript_RemoveOnScreenMessage_uScript_RemoveOnScreenMessage_288 = new uScript_RemoveOnScreenMessage();

	private ManOnScreenMessages.OnScreenMessage logic_uScript_RemoveOnScreenMessage_onScreenMessage_288;

	private bool logic_uScript_RemoveOnScreenMessage_instant_288;

	private bool logic_uScript_RemoveOnScreenMessage_Out_288 = true;

	private uScript_RemoveOnScreenMessage logic_uScript_RemoveOnScreenMessage_uScript_RemoveOnScreenMessage_290 = new uScript_RemoveOnScreenMessage();

	private ManOnScreenMessages.OnScreenMessage logic_uScript_RemoveOnScreenMessage_onScreenMessage_290;

	private bool logic_uScript_RemoveOnScreenMessage_instant_290;

	private bool logic_uScript_RemoveOnScreenMessage_Out_290 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_293 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_293;

	private bool logic_uScriptAct_SetBool_Out_293 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_293 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_293 = true;

	private uScript_RemoveOnScreenMessage logic_uScript_RemoveOnScreenMessage_uScript_RemoveOnScreenMessage_295 = new uScript_RemoveOnScreenMessage();

	private ManOnScreenMessages.OnScreenMessage logic_uScript_RemoveOnScreenMessage_onScreenMessage_295;

	private bool logic_uScript_RemoveOnScreenMessage_instant_295;

	private bool logic_uScript_RemoveOnScreenMessage_Out_295 = true;

	private uScript_RemoveOnScreenMessage logic_uScript_RemoveOnScreenMessage_uScript_RemoveOnScreenMessage_298 = new uScript_RemoveOnScreenMessage();

	private ManOnScreenMessages.OnScreenMessage logic_uScript_RemoveOnScreenMessage_onScreenMessage_298;

	private bool logic_uScript_RemoveOnScreenMessage_instant_298;

	private bool logic_uScript_RemoveOnScreenMessage_Out_298 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_299 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_299 = "";

	private bool logic_uScript_EnableGlow_enable_299 = true;

	private bool logic_uScript_EnableGlow_Out_299 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_300 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_300 = "";

	private bool logic_uScript_EnableGlow_enable_300 = true;

	private bool logic_uScript_EnableGlow_Out_300 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_302 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_302;

	private bool logic_uScriptCon_CompareBool_True_302 = true;

	private bool logic_uScriptCon_CompareBool_False_302 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_304 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_304 = "";

	private bool logic_uScript_EnableGlow_enable_304;

	private bool logic_uScript_EnableGlow_Out_304 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_306 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_306 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_308 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_308 = "";

	private bool logic_uScript_EnableGlow_enable_308;

	private bool logic_uScript_EnableGlow_Out_308 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_310 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_310 = "";

	private bool logic_uScript_EnableGlow_enable_310 = true;

	private bool logic_uScript_EnableGlow_Out_310 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_313 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_313 = "";

	private bool logic_uScript_EnableGlow_enable_313;

	private bool logic_uScript_EnableGlow_Out_313 = true;

	private uScript_GetJoypadControlMode logic_uScript_GetJoypadControlMode_uScript_GetJoypadControlMode_315 = new uScript_GetJoypadControlMode();

	private bool logic_uScript_GetJoypadControlMode_Joypad_315 = true;

	private bool logic_uScript_GetJoypadControlMode_MouseAndKeyboard_315 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_318 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_318;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_318;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_318;

	private bool logic_uScript_AddMessage_Out_318 = true;

	private bool logic_uScript_AddMessage_Shown_318 = true;

	private uScript_IsHUDElementVisible logic_uScript_IsHUDElementVisible_uScript_IsHUDElementVisible_319 = new uScript_IsHUDElementVisible();

	private ManHUD.HUDElementType logic_uScript_IsHUDElementVisible_hudElement_319 = ManHUD.HUDElementType.InteractionMode;

	private bool logic_uScript_IsHUDElementVisible_True_319 = true;

	private bool logic_uScript_IsHUDElementVisible_False_319 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_320 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_320;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_320;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_320;

	private bool logic_uScript_AddMessage_Out_320 = true;

	private bool logic_uScript_AddMessage_Shown_320 = true;

	private uScript_IsPlayerInteractingWithBlock logic_uScript_IsPlayerInteractingWithBlock_uScript_IsPlayerInteractingWithBlock_324 = new uScript_IsPlayerInteractingWithBlock();

	private TankBlock logic_uScript_IsPlayerInteractingWithBlock_block_324;

	private bool logic_uScript_IsPlayerInteractingWithBlock_Out_324 = true;

	private bool logic_uScript_IsPlayerInteractingWithBlock_Interacted_324 = true;

	private bool logic_uScript_IsPlayerInteractingWithBlock_NotInteracted_324 = true;

	private bool logic_uScript_IsPlayerInteractingWithBlock_Dragging_324 = true;

	private bool logic_uScript_IsPlayerInteractingWithBlock_NotDragging_324 = true;

	private uScript_IsPlayerInteractingWithBlock logic_uScript_IsPlayerInteractingWithBlock_uScript_IsPlayerInteractingWithBlock_325 = new uScript_IsPlayerInteractingWithBlock();

	private TankBlock logic_uScript_IsPlayerInteractingWithBlock_block_325;

	private bool logic_uScript_IsPlayerInteractingWithBlock_Out_325 = true;

	private bool logic_uScript_IsPlayerInteractingWithBlock_Interacted_325 = true;

	private bool logic_uScript_IsPlayerInteractingWithBlock_NotInteracted_325 = true;

	private bool logic_uScript_IsPlayerInteractingWithBlock_Dragging_325 = true;

	private bool logic_uScript_IsPlayerInteractingWithBlock_NotDragging_325 = true;

	private SubGraph_CompleteObjectiveStage logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_328 = new SubGraph_CompleteObjectiveStage();

	private int logic_SubGraph_CompleteObjectiveStage_objectiveStage_328;

	private bool logic_SubGraph_CompleteObjectiveStage_isFinalObjective_328;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_331 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_331;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_331;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_331;

	private bool logic_uScript_AddMessage_Out_331 = true;

	private bool logic_uScript_AddMessage_Shown_331 = true;

	private uScriptCon_CompareInt logic_uScriptCon_CompareInt_uScriptCon_CompareInt_333 = new uScriptCon_CompareInt();

	private int logic_uScriptCon_CompareInt_A_333;

	private int logic_uScriptCon_CompareInt_B_333 = 4;

	private bool logic_uScriptCon_CompareInt_GreaterThan_333 = true;

	private bool logic_uScriptCon_CompareInt_GreaterThanOrEqualTo_333 = true;

	private bool logic_uScriptCon_CompareInt_EqualTo_333 = true;

	private bool logic_uScriptCon_CompareInt_NotEqualTo_333 = true;

	private bool logic_uScriptCon_CompareInt_LessThanOrEqualTo_333 = true;

	private bool logic_uScriptCon_CompareInt_LessThan_333 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_336 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_336 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_336 = true;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_336 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_337 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_337;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_337;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_337;

	private bool logic_uScript_AddMessage_Out_337 = true;

	private bool logic_uScript_AddMessage_Shown_337 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_342 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_342;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_342;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_342;

	private bool logic_uScript_AddMessage_Out_342 = true;

	private bool logic_uScript_AddMessage_Shown_342 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_343 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_343;

	private bool logic_uScriptCon_CompareBool_True_343 = true;

	private bool logic_uScriptCon_CompareBool_False_343 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_344 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_344;

	private bool logic_uScriptAct_SetBool_Out_344 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_344 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_344 = true;

	private uScript_DoesPlayerTankHaveBlock logic_uScript_DoesPlayerTankHaveBlock_uScript_DoesPlayerTankHaveBlock_347 = new uScript_DoesPlayerTankHaveBlock();

	private BlockTypes logic_uScript_DoesPlayerTankHaveBlock_blockType_347 = BlockTypes.GSOGeneratorSolar_141;

	private int logic_uScript_DoesPlayerTankHaveBlock_amount_347 = 1;

	private bool logic_uScript_DoesPlayerTankHaveBlock_True_347 = true;

	private bool logic_uScript_DoesPlayerTankHaveBlock_False_347 = true;

	private uScript_DoesPlayerTankHaveBlock logic_uScript_DoesPlayerTankHaveBlock_uScript_DoesPlayerTankHaveBlock_348 = new uScript_DoesPlayerTankHaveBlock();

	private BlockTypes logic_uScript_DoesPlayerTankHaveBlock_blockType_348 = BlockTypes.GSORegen_111;

	private int logic_uScript_DoesPlayerTankHaveBlock_amount_348 = 1;

	private bool logic_uScript_DoesPlayerTankHaveBlock_True_348 = true;

	private bool logic_uScript_DoesPlayerTankHaveBlock_False_348 = true;

	private uScript_IsPlayerInteractingWithBlock logic_uScript_IsPlayerInteractingWithBlock_uScript_IsPlayerInteractingWithBlock_349 = new uScript_IsPlayerInteractingWithBlock();

	private TankBlock logic_uScript_IsPlayerInteractingWithBlock_block_349;

	private bool logic_uScript_IsPlayerInteractingWithBlock_Out_349 = true;

	private bool logic_uScript_IsPlayerInteractingWithBlock_Interacted_349 = true;

	private bool logic_uScript_IsPlayerInteractingWithBlock_NotInteracted_349 = true;

	private bool logic_uScript_IsPlayerInteractingWithBlock_Dragging_349 = true;

	private bool logic_uScript_IsPlayerInteractingWithBlock_NotDragging_349 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_353 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_353;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_353;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_353;

	private bool logic_uScript_AddMessage_Out_353 = true;

	private bool logic_uScript_AddMessage_Shown_353 = true;

	private uScript_DoesPlayerTankHaveBlock logic_uScript_DoesPlayerTankHaveBlock_uScript_DoesPlayerTankHaveBlock_354 = new uScript_DoesPlayerTankHaveBlock();

	private BlockTypes logic_uScript_DoesPlayerTankHaveBlock_blockType_354 = BlockTypes.GSORegen_111;

	private int logic_uScript_DoesPlayerTankHaveBlock_amount_354 = 1;

	private bool logic_uScript_DoesPlayerTankHaveBlock_True_354 = true;

	private bool logic_uScript_DoesPlayerTankHaveBlock_False_354 = true;

	private uScript_DoesPlayerTankHaveBlock logic_uScript_DoesPlayerTankHaveBlock_uScript_DoesPlayerTankHaveBlock_355 = new uScript_DoesPlayerTankHaveBlock();

	private BlockTypes logic_uScript_DoesPlayerTankHaveBlock_blockType_355 = BlockTypes.GSOGeneratorSolar_141;

	private int logic_uScript_DoesPlayerTankHaveBlock_amount_355 = 1;

	private bool logic_uScript_DoesPlayerTankHaveBlock_True_355 = true;

	private bool logic_uScript_DoesPlayerTankHaveBlock_False_355 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_356 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_356 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_357 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_357 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_358 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_358 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_360 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_360 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_360 = true;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_360 = true;

	private uScript_RemoveOnScreenMessage logic_uScript_RemoveOnScreenMessage_uScript_RemoveOnScreenMessage_364 = new uScript_RemoveOnScreenMessage();

	private ManOnScreenMessages.OnScreenMessage logic_uScript_RemoveOnScreenMessage_onScreenMessage_364;

	private bool logic_uScript_RemoveOnScreenMessage_instant_364;

	private bool logic_uScript_RemoveOnScreenMessage_Out_364 = true;

	private uScript_RemoveOnScreenMessage logic_uScript_RemoveOnScreenMessage_uScript_RemoveOnScreenMessage_367 = new uScript_RemoveOnScreenMessage();

	private ManOnScreenMessages.OnScreenMessage logic_uScript_RemoveOnScreenMessage_onScreenMessage_367;

	private bool logic_uScript_RemoveOnScreenMessage_instant_367;

	private bool logic_uScript_RemoveOnScreenMessage_Out_367 = true;

	private uScript_RemoveOnScreenMessage logic_uScript_RemoveOnScreenMessage_uScript_RemoveOnScreenMessage_369 = new uScript_RemoveOnScreenMessage();

	private ManOnScreenMessages.OnScreenMessage logic_uScript_RemoveOnScreenMessage_onScreenMessage_369;

	private bool logic_uScript_RemoveOnScreenMessage_instant_369;

	private bool logic_uScript_RemoveOnScreenMessage_Out_369 = true;

	private uScript_RemoveOnScreenMessage logic_uScript_RemoveOnScreenMessage_uScript_RemoveOnScreenMessage_371 = new uScript_RemoveOnScreenMessage();

	private ManOnScreenMessages.OnScreenMessage logic_uScript_RemoveOnScreenMessage_onScreenMessage_371;

	private bool logic_uScript_RemoveOnScreenMessage_instant_371;

	private bool logic_uScript_RemoveOnScreenMessage_Out_371 = true;

	private uScript_RemoveOnScreenMessage logic_uScript_RemoveOnScreenMessage_uScript_RemoveOnScreenMessage_372 = new uScript_RemoveOnScreenMessage();

	private ManOnScreenMessages.OnScreenMessage logic_uScript_RemoveOnScreenMessage_onScreenMessage_372;

	private bool logic_uScript_RemoveOnScreenMessage_instant_372;

	private bool logic_uScript_RemoveOnScreenMessage_Out_372 = true;

	private uScript_RemoveOnScreenMessage logic_uScript_RemoveOnScreenMessage_uScript_RemoveOnScreenMessage_374 = new uScript_RemoveOnScreenMessage();

	private ManOnScreenMessages.OnScreenMessage logic_uScript_RemoveOnScreenMessage_onScreenMessage_374;

	private bool logic_uScript_RemoveOnScreenMessage_instant_374;

	private bool logic_uScript_RemoveOnScreenMessage_Out_374 = true;

	private uScript_RemoveOnScreenMessage logic_uScript_RemoveOnScreenMessage_uScript_RemoveOnScreenMessage_379 = new uScript_RemoveOnScreenMessage();

	private ManOnScreenMessages.OnScreenMessage logic_uScript_RemoveOnScreenMessage_onScreenMessage_379;

	private bool logic_uScript_RemoveOnScreenMessage_instant_379;

	private bool logic_uScript_RemoveOnScreenMessage_Out_379 = true;

	private uScript_RemoveOnScreenMessage logic_uScript_RemoveOnScreenMessage_uScript_RemoveOnScreenMessage_381 = new uScript_RemoveOnScreenMessage();

	private ManOnScreenMessages.OnScreenMessage logic_uScript_RemoveOnScreenMessage_onScreenMessage_381;

	private bool logic_uScript_RemoveOnScreenMessage_instant_381;

	private bool logic_uScript_RemoveOnScreenMessage_Out_381 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_382 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_382;

	private bool logic_uScriptCon_CompareBool_True_382 = true;

	private bool logic_uScriptCon_CompareBool_False_382 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_384 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_384;

	private bool logic_uScriptAct_SetBool_Out_384 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_384 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_384 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_387 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_387;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_387 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_387 = "DismantleBaseMsgShown";

	private uScript_Wait logic_uScript_Wait_uScript_Wait_388 = new uScript_Wait();

	private float logic_uScript_Wait_seconds_388 = 1f;

	private bool logic_uScript_Wait_repeat_388;

	private bool logic_uScript_Wait_Waited_388 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_389 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_389;

	private bool logic_uScriptCon_CompareBool_True_389 = true;

	private bool logic_uScriptCon_CompareBool_False_389 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_392 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_392;

	private bool logic_uScriptAct_SetBool_Out_392 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_392 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_392 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_393 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_393;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_393 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_393 = "PlayerHealed";

	private uScript_MoveEncounterWithVisible logic_uScript_MoveEncounterWithVisible_uScript_MoveEncounterWithVisible_396 = new uScript_MoveEncounterWithVisible();

	private GameObject logic_uScript_MoveEncounterWithVisible_ownerNode_396;

	private object logic_uScript_MoveEncounterWithVisible_visibleObject_396 = "";

	private bool logic_uScript_MoveEncounterWithVisible_Out_396 = true;

	private uScript_PlayerInRangeOfCurrentEncounter logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_398 = new uScript_PlayerInRangeOfCurrentEncounter();

	private GameObject logic_uScript_PlayerInRangeOfCurrentEncounter_ownerNode_398;

	private bool logic_uScript_PlayerInRangeOfCurrentEncounter_Out_398 = true;

	private bool logic_uScript_PlayerInRangeOfCurrentEncounter_True_398 = true;

	private bool logic_uScript_PlayerInRangeOfCurrentEncounter_False_398 = true;

	private uScript_MoveEncounterWithVisible logic_uScript_MoveEncounterWithVisible_uScript_MoveEncounterWithVisible_400 = new uScript_MoveEncounterWithVisible();

	private GameObject logic_uScript_MoveEncounterWithVisible_ownerNode_400;

	private object logic_uScript_MoveEncounterWithVisible_visibleObject_400 = "";

	private bool logic_uScript_MoveEncounterWithVisible_Out_400 = true;

	private uScriptCon_CompareInt logic_uScriptCon_CompareInt_uScriptCon_CompareInt_403 = new uScriptCon_CompareInt();

	private int logic_uScriptCon_CompareInt_A_403;

	private int logic_uScriptCon_CompareInt_B_403 = 5;

	private bool logic_uScriptCon_CompareInt_GreaterThan_403 = true;

	private bool logic_uScriptCon_CompareInt_GreaterThanOrEqualTo_403 = true;

	private bool logic_uScriptCon_CompareInt_EqualTo_403 = true;

	private bool logic_uScriptCon_CompareInt_NotEqualTo_403 = true;

	private bool logic_uScriptCon_CompareInt_LessThanOrEqualTo_403 = true;

	private bool logic_uScriptCon_CompareInt_LessThan_403 = true;

	private uScript_MoveEncounterWithVisible logic_uScript_MoveEncounterWithVisible_uScript_MoveEncounterWithVisible_405 = new uScript_MoveEncounterWithVisible();

	private GameObject logic_uScript_MoveEncounterWithVisible_ownerNode_405;

	private object logic_uScript_MoveEncounterWithVisible_visibleObject_405 = "";

	private bool logic_uScript_MoveEncounterWithVisible_Out_405 = true;

	private uScript_PointArrowAtVisible logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_408 = new uScript_PointArrowAtVisible();

	private object logic_uScript_PointArrowAtVisible_targetObject_408 = "";

	private float logic_uScript_PointArrowAtVisible_timeToShowFor_408 = 0.1f;

	private Vector3 logic_uScript_PointArrowAtVisible_offset_408 = new Vector3(0f, 0f, 0f);

	private bool logic_uScript_PointArrowAtVisible_Out_408 = true;

	private uScript_PointArrowAtVisible logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_411 = new uScript_PointArrowAtVisible();

	private object logic_uScript_PointArrowAtVisible_targetObject_411 = "";

	private float logic_uScript_PointArrowAtVisible_timeToShowFor_411 = 0.1f;

	private Vector3 logic_uScript_PointArrowAtVisible_offset_411 = new Vector3(0f, 0f, 0f);

	private bool logic_uScript_PointArrowAtVisible_Out_411 = true;

	private uScript_PointArrowAtVisible logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_413 = new uScript_PointArrowAtVisible();

	private object logic_uScript_PointArrowAtVisible_targetObject_413 = "";

	private float logic_uScript_PointArrowAtVisible_timeToShowFor_413 = 0.1f;

	private Vector3 logic_uScript_PointArrowAtVisible_offset_413 = new Vector3(0f, 0f, 0f);

	private bool logic_uScript_PointArrowAtVisible_Out_413 = true;

	private uScript_PointArrowAtVisible logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_415 = new uScript_PointArrowAtVisible();

	private object logic_uScript_PointArrowAtVisible_targetObject_415 = "";

	private float logic_uScript_PointArrowAtVisible_timeToShowFor_415 = 0.1f;

	private Vector3 logic_uScript_PointArrowAtVisible_offset_415 = new Vector3(0f, 0f, 0f);

	private bool logic_uScript_PointArrowAtVisible_Out_415 = true;

	private uScript_SendAnaliticsEvent logic_uScript_SendAnaliticsEvent_uScript_SendAnaliticsEvent_416 = new uScript_SendAnaliticsEvent();

	private string logic_uScript_SendAnaliticsEvent_analiticsEvent_416 = "tutorial_stage";

	private string logic_uScript_SendAnaliticsEvent_parameterName_416 = "stage_complete";

	private object logic_uScript_SendAnaliticsEvent_parameter_416 = "solar_generator";

	private bool logic_uScript_SendAnaliticsEvent_Out_416 = true;

	private void SyncUnityHooks()
	{
		SyncEventListeners();
		if (null == owner_Connection_5 || !m_RegisteredForEvents)
		{
			owner_Connection_5 = parentGameObject;
		}
		if (null == owner_Connection_6 || !m_RegisteredForEvents)
		{
			owner_Connection_6 = parentGameObject;
		}
		if (null == owner_Connection_8 || !m_RegisteredForEvents)
		{
			owner_Connection_8 = parentGameObject;
		}
		if (null == owner_Connection_9 || !m_RegisteredForEvents)
		{
			owner_Connection_9 = parentGameObject;
		}
		if (null == owner_Connection_17 || !m_RegisteredForEvents)
		{
			owner_Connection_17 = parentGameObject;
		}
		if (null == owner_Connection_27 || !m_RegisteredForEvents)
		{
			owner_Connection_27 = parentGameObject;
		}
		if (null == owner_Connection_43 || !m_RegisteredForEvents)
		{
			owner_Connection_43 = parentGameObject;
		}
		if (null == owner_Connection_50 || !m_RegisteredForEvents)
		{
			owner_Connection_50 = parentGameObject;
		}
		if (null == owner_Connection_53 || !m_RegisteredForEvents)
		{
			owner_Connection_53 = parentGameObject;
		}
		if (null == owner_Connection_68 || !m_RegisteredForEvents)
		{
			owner_Connection_68 = parentGameObject;
		}
		if (null == owner_Connection_74 || !m_RegisteredForEvents)
		{
			owner_Connection_74 = parentGameObject;
			if (null != owner_Connection_74)
			{
				uScript_EncounterUpdate uScript_EncounterUpdate2 = owner_Connection_74.GetComponent<uScript_EncounterUpdate>();
				if (null == uScript_EncounterUpdate2)
				{
					uScript_EncounterUpdate2 = owner_Connection_74.AddComponent<uScript_EncounterUpdate>();
				}
				if (null != uScript_EncounterUpdate2)
				{
					uScript_EncounterUpdate2.OnUpdate += Instance_OnUpdate_75;
					uScript_EncounterUpdate2.OnSuspend += Instance_OnSuspend_75;
					uScript_EncounterUpdate2.OnResume += Instance_OnResume_75;
				}
			}
		}
		if (null == owner_Connection_85 || !m_RegisteredForEvents)
		{
			owner_Connection_85 = parentGameObject;
			if (null != owner_Connection_85)
			{
				uScript_SaveLoad uScript_SaveLoad2 = owner_Connection_85.GetComponent<uScript_SaveLoad>();
				if (null == uScript_SaveLoad2)
				{
					uScript_SaveLoad2 = owner_Connection_85.AddComponent<uScript_SaveLoad>();
				}
				if (null != uScript_SaveLoad2)
				{
					uScript_SaveLoad2.SaveEvent += Instance_SaveEvent_84;
					uScript_SaveLoad2.LoadEvent += Instance_LoadEvent_84;
					uScript_SaveLoad2.RestartEvent += Instance_RestartEvent_84;
				}
			}
		}
		if (null == owner_Connection_116 || !m_RegisteredForEvents)
		{
			owner_Connection_116 = parentGameObject;
		}
		if (null == owner_Connection_121 || !m_RegisteredForEvents)
		{
			owner_Connection_121 = parentGameObject;
		}
		if (null == owner_Connection_142 || !m_RegisteredForEvents)
		{
			owner_Connection_142 = parentGameObject;
		}
		if (null == owner_Connection_149 || !m_RegisteredForEvents)
		{
			owner_Connection_149 = parentGameObject;
		}
		if (null == owner_Connection_159 || !m_RegisteredForEvents)
		{
			owner_Connection_159 = parentGameObject;
		}
		if (null == owner_Connection_164 || !m_RegisteredForEvents)
		{
			owner_Connection_164 = parentGameObject;
		}
		if (null == owner_Connection_397 || !m_RegisteredForEvents)
		{
			owner_Connection_397 = parentGameObject;
		}
		if (null == owner_Connection_399 || !m_RegisteredForEvents)
		{
			owner_Connection_399 = parentGameObject;
		}
		if (null == owner_Connection_401 || !m_RegisteredForEvents)
		{
			owner_Connection_401 = parentGameObject;
		}
		if (null == owner_Connection_406 || !m_RegisteredForEvents)
		{
			owner_Connection_406 = parentGameObject;
		}
	}

	private void RegisterForUnityHooks()
	{
		SyncEventListeners();
		if (!m_RegisteredForEvents && null != owner_Connection_74)
		{
			uScript_EncounterUpdate uScript_EncounterUpdate2 = owner_Connection_74.GetComponent<uScript_EncounterUpdate>();
			if (null == uScript_EncounterUpdate2)
			{
				uScript_EncounterUpdate2 = owner_Connection_74.AddComponent<uScript_EncounterUpdate>();
			}
			if (null != uScript_EncounterUpdate2)
			{
				uScript_EncounterUpdate2.OnUpdate += Instance_OnUpdate_75;
				uScript_EncounterUpdate2.OnSuspend += Instance_OnSuspend_75;
				uScript_EncounterUpdate2.OnResume += Instance_OnResume_75;
			}
		}
		if (!m_RegisteredForEvents && null != owner_Connection_85)
		{
			uScript_SaveLoad uScript_SaveLoad2 = owner_Connection_85.GetComponent<uScript_SaveLoad>();
			if (null == uScript_SaveLoad2)
			{
				uScript_SaveLoad2 = owner_Connection_85.AddComponent<uScript_SaveLoad>();
			}
			if (null != uScript_SaveLoad2)
			{
				uScript_SaveLoad2.SaveEvent += Instance_SaveEvent_84;
				uScript_SaveLoad2.LoadEvent += Instance_LoadEvent_84;
				uScript_SaveLoad2.RestartEvent += Instance_RestartEvent_84;
			}
		}
	}

	private void SyncEventListeners()
	{
	}

	private void UnregisterEventListeners()
	{
		if (null != owner_Connection_74)
		{
			uScript_EncounterUpdate component = owner_Connection_74.GetComponent<uScript_EncounterUpdate>();
			if (null != component)
			{
				component.OnUpdate -= Instance_OnUpdate_75;
				component.OnSuspend -= Instance_OnSuspend_75;
				component.OnResume -= Instance_OnResume_75;
			}
		}
		if (null != owner_Connection_85)
		{
			uScript_SaveLoad component2 = owner_Connection_85.GetComponent<uScript_SaveLoad>();
			if (null != component2)
			{
				component2.SaveEvent -= Instance_SaveEvent_84;
				component2.LoadEvent -= Instance_LoadEvent_84;
				component2.RestartEvent -= Instance_RestartEvent_84;
			}
		}
	}

	public override void SetParent(GameObject g)
	{
		parentGameObject = g;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_0.SetParent(g);
		logic_uScript_FireParticlesTowardsGround_uScript_FireParticlesTowardsGround_4.SetParent(g);
		logic_uScript_SpawnBlockAbovePlayer_uScript_SpawnBlockAbovePlayer_10.SetParent(g);
		logic_uScript_PausePopulation_uScript_PausePopulation_12.SetParent(g);
		logic_uScript_KeepBlockInvulnerable_uScript_KeepBlockInvulnerable_13.SetParent(g);
		logic_uScript_GetPositionInEncounter_uScript_GetPositionInEncounter_15.SetParent(g);
		logic_uScript_IsBlockAnchored_uScript_IsBlockAnchored_25.SetParent(g);
		logic_uScript_UpdateEncounterPosFromBlock_uScript_UpdateEncounterPosFromBlock_28.SetParent(g);
		logic_uScript_IsPlayerInteractingWithBlock_uScript_IsPlayerInteractingWithBlock_29.SetParent(g);
		logic_uScript_GetPlayerTankWithBlock_uScript_GetPlayerTankWithBlock_30.SetParent(g);
		logic_uScript_DoesTankHave_uScript_DoesTankHave_32.SetParent(g);
		logic_uScript_GetPlayerTankWithBlock_uScript_GetPlayerTankWithBlock_38.SetParent(g);
		logic_uScript_IsPlayerInRangeOfBlock_uScript_IsPlayerInRangeOfBlock_39.SetParent(g);
		logic_uScript_MoveEncounterWithVisible_uScript_MoveEncounterWithVisible_44.SetParent(g);
		logic_uScript_IsPlayerInRangeOfBlock_uScript_IsPlayerInRangeOfBlock_45.SetParent(g);
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_47.SetParent(g);
		logic_uScript_MoveEncounterWithVisible_uScript_MoveEncounterWithVisible_49.SetParent(g);
		logic_uScript_SpawnBlockAbovePlayer_uScript_SpawnBlockAbovePlayer_52.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_55.SetParent(g);
		logic_uScript_GetPlayerTank_uScript_GetPlayerTank_58.SetParent(g);
		logic_uScript_IsShieldBlockPowered_uScript_IsShieldBlockPowered_59.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_62.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_64.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_65.SetParent(g);
		logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_66.SetParent(g);
		logic_uScript_Wait_uScript_Wait_67.SetParent(g);
		logic_uScript_SetQuestLogVisible_uScript_SetQuestLogVisible_73.SetParent(g);
		logic_uScript_SetTechBlocksHealth_uScript_SetTechBlocksHealth_76.SetParent(g);
		logic_uScript_IsCoreEncounterCompleted_uScript_IsCoreEncounterCompleted_78.SetParent(g);
		logic_uScript_KeepBlockInvulnerable_uScript_KeepBlockInvulnerable_80.SetParent(g);
		logic_uScript_SetMissionsVisibleInHud_uScript_SetMissionsVisibleInHud_81.SetParent(g);
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_82.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_83.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_89.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_90.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_92.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_95.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_96.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_99.SetParent(g);
		logic_uScript_PausePopulation_uScript_PausePopulation_100.SetParent(g);
		logic_uScript_LockTech_uScript_LockTech_102.SetParent(g);
		logic_uScript_GetPlayerTankWithBlock_uScript_GetPlayerTankWithBlock_103.SetParent(g);
		logic_uScript_GetTankBlock_uScript_GetTankBlock_106.SetParent(g);
		logic_uScript_GetNamedBlock_uScript_GetNamedBlock_109.SetParent(g);
		logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_110.SetParent(g);
		logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_112.SetParent(g);
		logic_uScript_HideArrow_uScript_HideArrow_114.SetParent(g);
		logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_115.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_118.SetParent(g);
		logic_uScript_AccessListBlock_uScript_AccessListBlock_119.SetParent(g);
		logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_122.SetParent(g);
		logic_uScript_SpawnGhostBlocks_uScript_SpawnGhostBlocks_124.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_125.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_128.SetParent(g);
		logic_uScript_RemoveAllGhostBlocksOnTech_uScript_RemoveAllGhostBlocksOnTech_130.SetParent(g);
		logic_uScript_LockBlockAttach_uScript_LockBlockAttach_132.SetParent(g);
		logic_uScript_LockTutorialBlockAttach_uScript_LockTutorialBlockAttach_133.SetParent(g);
		logic_uScript_ChangeBuildingOptions_uScript_ChangeBuildingOptions_135.SetParent(g);
		logic_uScript_ChangeBuildingOptions_uScript_ChangeBuildingOptions_136.SetParent(g);
		logic_uScript_ChangeBuildingOptions_uScript_ChangeBuildingOptions_137.SetParent(g);
		logic_uScript_ChangeBuildingOptions_uScript_ChangeBuildingOptions_138.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_139.SetParent(g);
		logic_uScript_SpawnBlocksFromData_uScript_SpawnBlocksFromData_140.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_143.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_144.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_147.SetParent(g);
		logic_uScript_GetAndCheckBlocks_uScript_GetAndCheckBlocks_148.SetParent(g);
		logic_uScript_AccessListBlock_uScript_AccessListBlock_151.SetParent(g);
		logic_uScript_GetPlayerTankWithBlock_uScript_GetPlayerTankWithBlock_155.SetParent(g);
		logic_uScript_LockTech_uScript_LockTech_156.SetParent(g);
		logic_uScript_RemoveScenery_uScript_RemoveScenery_158.SetParent(g);
		logic_uScript_ClearTutorialTechToBuild_uScript_ClearTutorialTechToBuild_162.SetParent(g);
		logic_uScript_ShowQuestLog_uScript_ShowQuestLog_163.SetParent(g);
		logic_uScript_FinishEncounter_uScript_FinishEncounter_165.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_167.SetParent(g);
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_169.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_172.SetParent(g);
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_174.SetParent(g);
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_175.SetParent(g);
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_177.SetParent(g);
		logic_uScript_ChangeBuildingOptions_uScript_ChangeBuildingOptions_179.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_180.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_183.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_184.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_186.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_190.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_193.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_194.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_197.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_202.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_205.SetParent(g);
		logic_uScript_IsCoreEncounterCompleted_uScript_IsCoreEncounterCompleted_206.SetParent(g);
		logic_uScript_IsCoreEncounterCompleted_uScript_IsCoreEncounterCompleted_207.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_210.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_211.SetParent(g);
		logic_uScript_SetTutorialTechToBuild_uScript_SetTutorialTechToBuild_212.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_214.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_215.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_218.SetParent(g);
		logic_uScript_SetTutorialTechToBuild_uScript_SetTutorialTechToBuild_221.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_223.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_225.SetParent(g);
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_226.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_228.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_229.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_232.SetParent(g);
		logic_uScript_HasHintBeenShownBefore_uScript_HasHintBeenShownBefore_235.SetParent(g);
		logic_uScript_ShowHint_uScript_ShowHint_236.SetParent(g);
		logic_uScript_ShowHint_uScript_ShowHint_238.SetParent(g);
		logic_uScript_HasHintBeenShownBefore_uScript_HasHintBeenShownBefore_239.SetParent(g);
		logic_uScript_Wait_uScript_Wait_242.SetParent(g);
		logic_uScript_IsCoreEncounterCompleted_uScript_IsCoreEncounterCompleted_243.SetParent(g);
		logic_uScript_HideHint_uScript_HideHint_244.SetParent(g);
		logic_uScript_IsHUDElementVisible_uScript_IsHUDElementVisible_245.SetParent(g);
		logic_uScript_HideHint_uScript_HideHint_246.SetParent(g);
		logic_uScript_IsShieldBlockPowered_uScript_IsShieldBlockPowered_248.SetParent(g);
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_249.SetParent(g);
		logic_uScript_GetJoypadControlMode_uScript_GetJoypadControlMode_253.SetParent(g);
		logic_uScript_GetJoypadControlMode_uScript_GetJoypadControlMode_254.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_257.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_260.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_263.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_264.SetParent(g);
		logic_uScript_RemoveOnScreenMessage_uScript_RemoveOnScreenMessage_268.SetParent(g);
		logic_uScript_RemoveOnScreenMessage_uScript_RemoveOnScreenMessage_271.SetParent(g);
		logic_uScript_RemoveOnScreenMessage_uScript_RemoveOnScreenMessage_273.SetParent(g);
		logic_uScript_RemoveOnScreenMessage_uScript_RemoveOnScreenMessage_277.SetParent(g);
		logic_uScript_RemoveOnScreenMessage_uScript_RemoveOnScreenMessage_281.SetParent(g);
		logic_uScript_RemoveOnScreenMessage_uScript_RemoveOnScreenMessage_285.SetParent(g);
		logic_uScript_RemoveOnScreenMessage_uScript_RemoveOnScreenMessage_288.SetParent(g);
		logic_uScript_RemoveOnScreenMessage_uScript_RemoveOnScreenMessage_290.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_293.SetParent(g);
		logic_uScript_RemoveOnScreenMessage_uScript_RemoveOnScreenMessage_295.SetParent(g);
		logic_uScript_RemoveOnScreenMessage_uScript_RemoveOnScreenMessage_298.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_299.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_300.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_302.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_304.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_306.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_308.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_310.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_313.SetParent(g);
		logic_uScript_GetJoypadControlMode_uScript_GetJoypadControlMode_315.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_318.SetParent(g);
		logic_uScript_IsHUDElementVisible_uScript_IsHUDElementVisible_319.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_320.SetParent(g);
		logic_uScript_IsPlayerInteractingWithBlock_uScript_IsPlayerInteractingWithBlock_324.SetParent(g);
		logic_uScript_IsPlayerInteractingWithBlock_uScript_IsPlayerInteractingWithBlock_325.SetParent(g);
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_328.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_331.SetParent(g);
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_333.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_336.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_337.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_342.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_343.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_344.SetParent(g);
		logic_uScript_DoesPlayerTankHaveBlock_uScript_DoesPlayerTankHaveBlock_347.SetParent(g);
		logic_uScript_DoesPlayerTankHaveBlock_uScript_DoesPlayerTankHaveBlock_348.SetParent(g);
		logic_uScript_IsPlayerInteractingWithBlock_uScript_IsPlayerInteractingWithBlock_349.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_353.SetParent(g);
		logic_uScript_DoesPlayerTankHaveBlock_uScript_DoesPlayerTankHaveBlock_354.SetParent(g);
		logic_uScript_DoesPlayerTankHaveBlock_uScript_DoesPlayerTankHaveBlock_355.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_356.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_357.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_358.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_360.SetParent(g);
		logic_uScript_RemoveOnScreenMessage_uScript_RemoveOnScreenMessage_364.SetParent(g);
		logic_uScript_RemoveOnScreenMessage_uScript_RemoveOnScreenMessage_367.SetParent(g);
		logic_uScript_RemoveOnScreenMessage_uScript_RemoveOnScreenMessage_369.SetParent(g);
		logic_uScript_RemoveOnScreenMessage_uScript_RemoveOnScreenMessage_371.SetParent(g);
		logic_uScript_RemoveOnScreenMessage_uScript_RemoveOnScreenMessage_372.SetParent(g);
		logic_uScript_RemoveOnScreenMessage_uScript_RemoveOnScreenMessage_374.SetParent(g);
		logic_uScript_RemoveOnScreenMessage_uScript_RemoveOnScreenMessage_379.SetParent(g);
		logic_uScript_RemoveOnScreenMessage_uScript_RemoveOnScreenMessage_381.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_382.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_384.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_387.SetParent(g);
		logic_uScript_Wait_uScript_Wait_388.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_389.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_392.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_393.SetParent(g);
		logic_uScript_MoveEncounterWithVisible_uScript_MoveEncounterWithVisible_396.SetParent(g);
		logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_398.SetParent(g);
		logic_uScript_MoveEncounterWithVisible_uScript_MoveEncounterWithVisible_400.SetParent(g);
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_403.SetParent(g);
		logic_uScript_MoveEncounterWithVisible_uScript_MoveEncounterWithVisible_405.SetParent(g);
		logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_408.SetParent(g);
		logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_411.SetParent(g);
		logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_413.SetParent(g);
		logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_415.SetParent(g);
		logic_uScript_SendAnaliticsEvent_uScript_SendAnaliticsEvent_416.SetParent(g);
		owner_Connection_5 = parentGameObject;
		owner_Connection_6 = parentGameObject;
		owner_Connection_8 = parentGameObject;
		owner_Connection_9 = parentGameObject;
		owner_Connection_17 = parentGameObject;
		owner_Connection_27 = parentGameObject;
		owner_Connection_43 = parentGameObject;
		owner_Connection_50 = parentGameObject;
		owner_Connection_53 = parentGameObject;
		owner_Connection_68 = parentGameObject;
		owner_Connection_74 = parentGameObject;
		owner_Connection_85 = parentGameObject;
		owner_Connection_116 = parentGameObject;
		owner_Connection_121 = parentGameObject;
		owner_Connection_142 = parentGameObject;
		owner_Connection_149 = parentGameObject;
		owner_Connection_159 = parentGameObject;
		owner_Connection_164 = parentGameObject;
		owner_Connection_397 = parentGameObject;
		owner_Connection_399 = parentGameObject;
		owner_Connection_401 = parentGameObject;
		owner_Connection_406 = parentGameObject;
	}

	public void Awake()
	{
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_82.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_83.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_89.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_99.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_147.Awake();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_169.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_172.Awake();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_174.Awake();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_175.Awake();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_177.Awake();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_249.Awake();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_328.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_387.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_393.Awake();
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_0.Output1 += uScriptCon_ManualSwitch_Output1_0;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_0.Output2 += uScriptCon_ManualSwitch_Output2_0;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_0.Output3 += uScriptCon_ManualSwitch_Output3_0;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_0.Output4 += uScriptCon_ManualSwitch_Output4_0;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_0.Output5 += uScriptCon_ManualSwitch_Output5_0;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_0.Output6 += uScriptCon_ManualSwitch_Output6_0;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_0.Output7 += uScriptCon_ManualSwitch_Output7_0;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_0.Output8 += uScriptCon_ManualSwitch_Output8_0;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_82.Save_Out += SubGraph_SaveLoadInt_Save_Out_82;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_82.Load_Out += SubGraph_SaveLoadInt_Load_Out_82;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_82.Restart_Out += SubGraph_SaveLoadInt_Restart_Out_82;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_83.Save_Out += SubGraph_SaveLoadBool_Save_Out_83;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_83.Load_Out += SubGraph_SaveLoadBool_Load_Out_83;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_83.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_83;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_89.Save_Out += SubGraph_SaveLoadBool_Save_Out_89;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_89.Load_Out += SubGraph_SaveLoadBool_Load_Out_89;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_89.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_89;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_99.Save_Out += SubGraph_SaveLoadBool_Save_Out_99;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_99.Load_Out += SubGraph_SaveLoadBool_Load_Out_99;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_99.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_99;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_147.Save_Out += SubGraph_SaveLoadBool_Save_Out_147;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_147.Load_Out += SubGraph_SaveLoadBool_Load_Out_147;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_147.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_147;
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_169.Out += SubGraph_LoadObjectiveStates_Out_169;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_172.Save_Out += SubGraph_SaveLoadBool_Save_Out_172;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_172.Load_Out += SubGraph_SaveLoadBool_Load_Out_172;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_172.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_172;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_174.Out += SubGraph_CompleteObjectiveStage_Out_174;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_175.Out += SubGraph_CompleteObjectiveStage_Out_175;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_177.Out += SubGraph_CompleteObjectiveStage_Out_177;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_249.Out += SubGraph_AddMessageWithPadSupport_Out_249;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_249.Shown += SubGraph_AddMessageWithPadSupport_Shown_249;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_328.Out += SubGraph_CompleteObjectiveStage_Out_328;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_387.Save_Out += SubGraph_SaveLoadBool_Save_Out_387;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_387.Load_Out += SubGraph_SaveLoadBool_Load_Out_387;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_387.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_387;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_393.Save_Out += SubGraph_SaveLoadBool_Save_Out_393;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_393.Load_Out += SubGraph_SaveLoadBool_Load_Out_393;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_393.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_393;
	}

	public void Start()
	{
		SyncUnityHooks();
		m_RegisteredForEvents = true;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_82.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_83.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_89.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_99.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_147.Start();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_169.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_172.Start();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_174.Start();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_175.Start();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_177.Start();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_249.Start();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_328.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_387.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_393.Start();
	}

	public void OnEnable()
	{
		RegisterForUnityHooks();
		m_RegisteredForEvents = true;
		logic_uScript_IsCoreEncounterCompleted_uScript_IsCoreEncounterCompleted_78.OnEnable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_82.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_83.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_89.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_99.OnEnable();
		logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_115.OnEnable();
		logic_uScript_LockTutorialBlockAttach_uScript_LockTutorialBlockAttach_133.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_147.OnEnable();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_169.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_172.OnEnable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_174.OnEnable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_175.OnEnable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_177.OnEnable();
		logic_uScript_IsCoreEncounterCompleted_uScript_IsCoreEncounterCompleted_206.OnEnable();
		logic_uScript_IsCoreEncounterCompleted_uScript_IsCoreEncounterCompleted_207.OnEnable();
		logic_uScript_HasHintBeenShownBefore_uScript_HasHintBeenShownBefore_235.OnEnable();
		logic_uScript_HasHintBeenShownBefore_uScript_HasHintBeenShownBefore_239.OnEnable();
		logic_uScript_IsCoreEncounterCompleted_uScript_IsCoreEncounterCompleted_243.OnEnable();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_249.OnEnable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_328.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_387.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_393.OnEnable();
	}

	public void OnDisable()
	{
		logic_uScript_FireParticlesTowardsGround_uScript_FireParticlesTowardsGround_4.OnDisable();
		logic_uScript_SpawnBlockAbovePlayer_uScript_SpawnBlockAbovePlayer_10.OnDisable();
		logic_uScript_IsBlockAnchored_uScript_IsBlockAnchored_25.OnDisable();
		logic_uScript_UpdateEncounterPosFromBlock_uScript_UpdateEncounterPosFromBlock_28.OnDisable();
		logic_uScript_IsPlayerInteractingWithBlock_uScript_IsPlayerInteractingWithBlock_29.OnDisable();
		logic_uScript_IsPlayerInRangeOfBlock_uScript_IsPlayerInRangeOfBlock_39.OnDisable();
		logic_uScript_IsPlayerInRangeOfBlock_uScript_IsPlayerInRangeOfBlock_45.OnDisable();
		logic_uScript_SpawnBlockAbovePlayer_uScript_SpawnBlockAbovePlayer_52.OnDisable();
		logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_66.OnDisable();
		logic_uScript_Wait_uScript_Wait_67.OnDisable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_82.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_83.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_89.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_99.OnDisable();
		logic_uScript_GetTankBlock_uScript_GetTankBlock_106.OnDisable();
		logic_uScript_GetNamedBlock_uScript_GetNamedBlock_109.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_147.OnDisable();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_169.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_172.OnDisable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_174.OnDisable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_175.OnDisable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_177.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_180.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_186.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_190.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_193.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_194.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_197.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_202.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_205.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_210.OnDisable();
		logic_uScript_Wait_uScript_Wait_242.OnDisable();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_249.OnDisable();
		logic_uScript_GetJoypadControlMode_uScript_GetJoypadControlMode_253.OnDisable();
		logic_uScript_GetJoypadControlMode_uScript_GetJoypadControlMode_254.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_257.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_260.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_263.OnDisable();
		logic_uScript_GetJoypadControlMode_uScript_GetJoypadControlMode_315.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_318.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_320.OnDisable();
		logic_uScript_IsPlayerInteractingWithBlock_uScript_IsPlayerInteractingWithBlock_324.OnDisable();
		logic_uScript_IsPlayerInteractingWithBlock_uScript_IsPlayerInteractingWithBlock_325.OnDisable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_328.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_331.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_337.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_342.OnDisable();
		logic_uScript_DoesPlayerTankHaveBlock_uScript_DoesPlayerTankHaveBlock_347.OnDisable();
		logic_uScript_DoesPlayerTankHaveBlock_uScript_DoesPlayerTankHaveBlock_348.OnDisable();
		logic_uScript_IsPlayerInteractingWithBlock_uScript_IsPlayerInteractingWithBlock_349.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_353.OnDisable();
		logic_uScript_DoesPlayerTankHaveBlock_uScript_DoesPlayerTankHaveBlock_354.OnDisable();
		logic_uScript_DoesPlayerTankHaveBlock_uScript_DoesPlayerTankHaveBlock_355.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_387.OnDisable();
		logic_uScript_Wait_uScript_Wait_388.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_393.OnDisable();
		logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_398.OnDisable();
		UnregisterEventListeners();
		m_RegisteredForEvents = false;
	}

	public void Update()
	{
		SyncEventListeners();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_82.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_83.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_89.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_99.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_147.Update();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_169.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_172.Update();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_174.Update();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_175.Update();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_177.Update();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_249.Update();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_328.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_387.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_393.Update();
	}

	public void OnDestroy()
	{
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_82.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_83.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_89.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_99.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_147.OnDestroy();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_169.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_172.OnDestroy();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_174.OnDestroy();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_175.OnDestroy();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_177.OnDestroy();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_249.OnDestroy();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_328.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_387.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_393.OnDestroy();
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_0.Output1 -= uScriptCon_ManualSwitch_Output1_0;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_0.Output2 -= uScriptCon_ManualSwitch_Output2_0;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_0.Output3 -= uScriptCon_ManualSwitch_Output3_0;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_0.Output4 -= uScriptCon_ManualSwitch_Output4_0;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_0.Output5 -= uScriptCon_ManualSwitch_Output5_0;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_0.Output6 -= uScriptCon_ManualSwitch_Output6_0;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_0.Output7 -= uScriptCon_ManualSwitch_Output7_0;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_0.Output8 -= uScriptCon_ManualSwitch_Output8_0;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_82.Save_Out -= SubGraph_SaveLoadInt_Save_Out_82;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_82.Load_Out -= SubGraph_SaveLoadInt_Load_Out_82;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_82.Restart_Out -= SubGraph_SaveLoadInt_Restart_Out_82;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_83.Save_Out -= SubGraph_SaveLoadBool_Save_Out_83;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_83.Load_Out -= SubGraph_SaveLoadBool_Load_Out_83;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_83.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_83;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_89.Save_Out -= SubGraph_SaveLoadBool_Save_Out_89;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_89.Load_Out -= SubGraph_SaveLoadBool_Load_Out_89;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_89.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_89;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_99.Save_Out -= SubGraph_SaveLoadBool_Save_Out_99;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_99.Load_Out -= SubGraph_SaveLoadBool_Load_Out_99;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_99.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_99;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_147.Save_Out -= SubGraph_SaveLoadBool_Save_Out_147;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_147.Load_Out -= SubGraph_SaveLoadBool_Load_Out_147;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_147.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_147;
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_169.Out -= SubGraph_LoadObjectiveStates_Out_169;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_172.Save_Out -= SubGraph_SaveLoadBool_Save_Out_172;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_172.Load_Out -= SubGraph_SaveLoadBool_Load_Out_172;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_172.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_172;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_174.Out -= SubGraph_CompleteObjectiveStage_Out_174;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_175.Out -= SubGraph_CompleteObjectiveStage_Out_175;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_177.Out -= SubGraph_CompleteObjectiveStage_Out_177;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_249.Out -= SubGraph_AddMessageWithPadSupport_Out_249;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_249.Shown -= SubGraph_AddMessageWithPadSupport_Shown_249;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_328.Out -= SubGraph_CompleteObjectiveStage_Out_328;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_387.Save_Out -= SubGraph_SaveLoadBool_Save_Out_387;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_387.Load_Out -= SubGraph_SaveLoadBool_Load_Out_387;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_387.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_387;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_393.Save_Out -= SubGraph_SaveLoadBool_Save_Out_393;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_393.Load_Out -= SubGraph_SaveLoadBool_Load_Out_393;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_393.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_393;
	}

	private void Instance_OnUpdate_75(object o, EventArgs e)
	{
		Relay_OnUpdate_75();
	}

	private void Instance_OnSuspend_75(object o, EventArgs e)
	{
		Relay_OnSuspend_75();
	}

	private void Instance_OnResume_75(object o, EventArgs e)
	{
		Relay_OnResume_75();
	}

	private void Instance_SaveEvent_84(object o, EventArgs e)
	{
		Relay_SaveEvent_84();
	}

	private void Instance_LoadEvent_84(object o, EventArgs e)
	{
		Relay_LoadEvent_84();
	}

	private void Instance_RestartEvent_84(object o, EventArgs e)
	{
		Relay_RestartEvent_84();
	}

	private void uScriptCon_ManualSwitch_Output1_0(object o, EventArgs e)
	{
		Relay_Output1_0();
	}

	private void uScriptCon_ManualSwitch_Output2_0(object o, EventArgs e)
	{
		Relay_Output2_0();
	}

	private void uScriptCon_ManualSwitch_Output3_0(object o, EventArgs e)
	{
		Relay_Output3_0();
	}

	private void uScriptCon_ManualSwitch_Output4_0(object o, EventArgs e)
	{
		Relay_Output4_0();
	}

	private void uScriptCon_ManualSwitch_Output5_0(object o, EventArgs e)
	{
		Relay_Output5_0();
	}

	private void uScriptCon_ManualSwitch_Output6_0(object o, EventArgs e)
	{
		Relay_Output6_0();
	}

	private void uScriptCon_ManualSwitch_Output7_0(object o, EventArgs e)
	{
		Relay_Output7_0();
	}

	private void uScriptCon_ManualSwitch_Output8_0(object o, EventArgs e)
	{
		Relay_Output8_0();
	}

	private void SubGraph_SaveLoadInt_Save_Out_82(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_82 = e.integer;
		local_Stage_System_Int32 = logic_SubGraph_SaveLoadInt_integer_82;
		Relay_Save_Out_82();
	}

	private void SubGraph_SaveLoadInt_Load_Out_82(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_82 = e.integer;
		local_Stage_System_Int32 = logic_SubGraph_SaveLoadInt_integer_82;
		Relay_Load_Out_82();
	}

	private void SubGraph_SaveLoadInt_Restart_Out_82(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_82 = e.integer;
		local_Stage_System_Int32 = logic_SubGraph_SaveLoadInt_integer_82;
		Relay_Restart_Out_82();
	}

	private void SubGraph_SaveLoadBool_Save_Out_83(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_83 = e.boolean;
		local_AddedQuestLog_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_83;
		Relay_Save_Out_83();
	}

	private void SubGraph_SaveLoadBool_Load_Out_83(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_83 = e.boolean;
		local_AddedQuestLog_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_83;
		Relay_Load_Out_83();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_83(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_83 = e.boolean;
		local_AddedQuestLog_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_83;
		Relay_Restart_Out_83();
	}

	private void SubGraph_SaveLoadBool_Save_Out_89(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_89 = e.boolean;
		local_SolGenFoundMsgShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_89;
		Relay_Save_Out_89();
	}

	private void SubGraph_SaveLoadBool_Load_Out_89(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_89 = e.boolean;
		local_SolGenFoundMsgShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_89;
		Relay_Load_Out_89();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_89(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_89 = e.boolean;
		local_SolGenFoundMsgShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_89;
		Relay_Restart_Out_89();
	}

	private void SubGraph_SaveLoadBool_Save_Out_99(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_99 = e.boolean;
		local_RegenSpawnedMsgShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_99;
		Relay_Save_Out_99();
	}

	private void SubGraph_SaveLoadBool_Load_Out_99(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_99 = e.boolean;
		local_RegenSpawnedMsgShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_99;
		Relay_Load_Out_99();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_99(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_99 = e.boolean;
		local_RegenSpawnedMsgShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_99;
		Relay_Restart_Out_99();
	}

	private void SubGraph_SaveLoadBool_Save_Out_147(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_147 = e.boolean;
		local_SolGenSpawned_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_147;
		Relay_Save_Out_147();
	}

	private void SubGraph_SaveLoadBool_Load_Out_147(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_147 = e.boolean;
		local_SolGenSpawned_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_147;
		Relay_Load_Out_147();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_147(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_147 = e.boolean;
		local_SolGenSpawned_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_147;
		Relay_Restart_Out_147();
	}

	private void SubGraph_LoadObjectiveStates_Out_169(object o, SubGraph_LoadObjectiveStates.LogicEventArgs e)
	{
		Relay_Out_169();
	}

	private void SubGraph_SaveLoadBool_Save_Out_172(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_172 = e.boolean;
		local_MissionComplete_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_172;
		Relay_Save_Out_172();
	}

	private void SubGraph_SaveLoadBool_Load_Out_172(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_172 = e.boolean;
		local_MissionComplete_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_172;
		Relay_Load_Out_172();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_172(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_172 = e.boolean;
		local_MissionComplete_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_172;
		Relay_Restart_Out_172();
	}

	private void SubGraph_CompleteObjectiveStage_Out_174(object o, SubGraph_CompleteObjectiveStage.LogicEventArgs e)
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_174 = e.objectiveStage;
		local_Stage_System_Int32 = logic_SubGraph_CompleteObjectiveStage_objectiveStage_174;
		Relay_Out_174();
	}

	private void SubGraph_CompleteObjectiveStage_Out_175(object o, SubGraph_CompleteObjectiveStage.LogicEventArgs e)
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_175 = e.objectiveStage;
		local_Stage_System_Int32 = logic_SubGraph_CompleteObjectiveStage_objectiveStage_175;
		Relay_Out_175();
	}

	private void SubGraph_CompleteObjectiveStage_Out_177(object o, SubGraph_CompleteObjectiveStage.LogicEventArgs e)
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_177 = e.objectiveStage;
		local_Stage_System_Int32 = logic_SubGraph_CompleteObjectiveStage_objectiveStage_177;
		Relay_Out_177();
	}

	private void SubGraph_AddMessageWithPadSupport_Out_249(object o, SubGraph_AddMessageWithPadSupport.LogicEventArgs e)
	{
		logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboardReturn_249 = e.messageMouseKeyboardReturn;
		logic_SubGraph_AddMessageWithPadSupport_messageControlPadReturn_249 = e.messageControlPadReturn;
		local_Msg07AttachRepairBubble_ManOnScreenMessages_OnScreenMessage = logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboardReturn_249;
		local_Msg07AttachRepairBubble_Pad_ManOnScreenMessages_OnScreenMessage = logic_SubGraph_AddMessageWithPadSupport_messageControlPadReturn_249;
		Relay_Out_249();
	}

	private void SubGraph_AddMessageWithPadSupport_Shown_249(object o, SubGraph_AddMessageWithPadSupport.LogicEventArgs e)
	{
		logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboardReturn_249 = e.messageMouseKeyboardReturn;
		logic_SubGraph_AddMessageWithPadSupport_messageControlPadReturn_249 = e.messageControlPadReturn;
		local_Msg07AttachRepairBubble_ManOnScreenMessages_OnScreenMessage = logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboardReturn_249;
		local_Msg07AttachRepairBubble_Pad_ManOnScreenMessages_OnScreenMessage = logic_SubGraph_AddMessageWithPadSupport_messageControlPadReturn_249;
		Relay_Shown_249();
	}

	private void SubGraph_CompleteObjectiveStage_Out_328(object o, SubGraph_CompleteObjectiveStage.LogicEventArgs e)
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_328 = e.objectiveStage;
		local_Stage_System_Int32 = logic_SubGraph_CompleteObjectiveStage_objectiveStage_328;
		Relay_Out_328();
	}

	private void SubGraph_SaveLoadBool_Save_Out_387(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_387 = e.boolean;
		local_DismantleBaseMsgShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_387;
		Relay_Save_Out_387();
	}

	private void SubGraph_SaveLoadBool_Load_Out_387(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_387 = e.boolean;
		local_DismantleBaseMsgShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_387;
		Relay_Load_Out_387();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_387(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_387 = e.boolean;
		local_DismantleBaseMsgShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_387;
		Relay_Restart_Out_387();
	}

	private void SubGraph_SaveLoadBool_Save_Out_393(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_393 = e.boolean;
		local_PlayerHealed_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_393;
		Relay_Save_Out_393();
	}

	private void SubGraph_SaveLoadBool_Load_Out_393(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_393 = e.boolean;
		local_PlayerHealed_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_393;
		Relay_Load_Out_393();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_393(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_393 = e.boolean;
		local_PlayerHealed_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_393;
		Relay_Restart_Out_393();
	}

	private void Relay_Output1_0()
	{
		Relay_In_174();
	}

	private void Relay_Output2_0()
	{
		Relay_In_25();
	}

	private void Relay_Output3_0()
	{
		Relay_In_30();
	}

	private void Relay_Output4_0()
	{
		Relay_In_106();
	}

	private void Relay_Output5_0()
	{
		Relay_In_382();
	}

	private void Relay_Output6_0()
	{
	}

	private void Relay_Output7_0()
	{
	}

	private void Relay_Output8_0()
	{
	}

	private void Relay_In_0()
	{
		logic_uScriptCon_ManualSwitch_CurrentOutput_0 = local_Stage_System_Int32;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_0.In(logic_uScriptCon_ManualSwitch_CurrentOutput_0);
	}

	private void Relay_In_4()
	{
		logic_uScript_FireParticlesTowardsGround_groundPos_4 = local_EncounterPos_UnityEngine_Vector3;
		logic_uScript_FireParticlesTowardsGround_particleEffect_4 = particles;
		logic_uScript_FireParticlesTowardsGround_owner_4 = owner_Connection_5;
		logic_uScript_FireParticlesTowardsGround_Return_4 = logic_uScript_FireParticlesTowardsGround_uScript_FireParticlesTowardsGround_4.In(logic_uScript_FireParticlesTowardsGround_groundPos_4, logic_uScript_FireParticlesTowardsGround_particleEffect_4, logic_uScript_FireParticlesTowardsGround_owner_4, logic_uScript_FireParticlesTowardsGround_uniqueName_4);
		if (logic_uScript_FireParticlesTowardsGround_uScript_FireParticlesTowardsGround_4.Delivered)
		{
			Relay_In_143();
		}
	}

	private void Relay_In_10()
	{
		logic_uScript_SpawnBlockAbovePlayer_owner_10 = owner_Connection_9;
		logic_uScript_SpawnBlockAbovePlayer_Return_10 = logic_uScript_SpawnBlockAbovePlayer_uScript_SpawnBlockAbovePlayer_10.In(logic_uScript_SpawnBlockAbovePlayer_blockType_10, logic_uScript_SpawnBlockAbovePlayer_uniqueName_10, logic_uScript_SpawnBlockAbovePlayer_owner_10);
		local_RepairBubbleBlock_TankBlock = logic_uScript_SpawnBlockAbovePlayer_Return_10;
	}

	private void Relay_Pause_12()
	{
		logic_uScript_PausePopulation_uScript_PausePopulation_12.Pause();
		if (logic_uScript_PausePopulation_uScript_PausePopulation_12.Out)
		{
			Relay_In_115();
		}
	}

	private void Relay_UnPause_12()
	{
		logic_uScript_PausePopulation_uScript_PausePopulation_12.UnPause();
		if (logic_uScript_PausePopulation_uScript_PausePopulation_12.Out)
		{
			Relay_In_115();
		}
	}

	private void Relay_In_13()
	{
		logic_uScript_KeepBlockInvulnerable_block_13 = local_Solar_Generator_Block_TankBlock;
		logic_uScript_KeepBlockInvulnerable_uScript_KeepBlockInvulnerable_13.In(logic_uScript_KeepBlockInvulnerable_block_13);
		if (logic_uScript_KeepBlockInvulnerable_uScript_KeepBlockInvulnerable_13.Out)
		{
			Relay_In_80();
		}
	}

	private void Relay_In_15()
	{
		logic_uScript_GetPositionInEncounter_ownerNode_15 = owner_Connection_17;
		logic_uScript_GetPositionInEncounter_posName_15 = PositionId;
		logic_uScript_GetPositionInEncounter_Return_15 = logic_uScript_GetPositionInEncounter_uScript_GetPositionInEncounter_15.In(logic_uScript_GetPositionInEncounter_ownerNode_15, logic_uScript_GetPositionInEncounter_posName_15);
		local_EncounterPos_UnityEngine_Vector3 = logic_uScript_GetPositionInEncounter_Return_15;
		if (logic_uScript_GetPositionInEncounter_uScript_GetPositionInEncounter_15.Out)
		{
			Relay_In_4();
		}
	}

	private void Relay_In_25()
	{
		logic_uScript_IsBlockAnchored_block_25 = local_Solar_Generator_Block_TankBlock;
		logic_uScript_IsBlockAnchored_uScript_IsBlockAnchored_25.In(logic_uScript_IsBlockAnchored_block_25);
		bool num = logic_uScript_IsBlockAnchored_uScript_IsBlockAnchored_25.True;
		bool flag = logic_uScript_IsBlockAnchored_uScript_IsBlockAnchored_25.False;
		if (num)
		{
			Relay_In_155();
		}
		if (flag)
		{
			Relay_In_110();
		}
	}

	private void Relay_In_28()
	{
		logic_uScript_UpdateEncounterPosFromBlock_ownerNode_28 = owner_Connection_27;
		logic_uScript_UpdateEncounterPosFromBlock_block_28 = local_Solar_Generator_Block_TankBlock;
		logic_uScript_UpdateEncounterPosFromBlock_uScript_UpdateEncounterPosFromBlock_28.In(logic_uScript_UpdateEncounterPosFromBlock_ownerNode_28, logic_uScript_UpdateEncounterPosFromBlock_block_28);
		if (logic_uScript_UpdateEncounterPosFromBlock_uScript_UpdateEncounterPosFromBlock_28.Out)
		{
			Relay_In_197();
		}
	}

	private void Relay_In_29()
	{
		logic_uScript_IsPlayerInteractingWithBlock_block_29 = local_RepairBubbleBlock_TankBlock;
		logic_uScript_IsPlayerInteractingWithBlock_uScript_IsPlayerInteractingWithBlock_29.In(logic_uScript_IsPlayerInteractingWithBlock_block_29);
		bool dragging = logic_uScript_IsPlayerInteractingWithBlock_uScript_IsPlayerInteractingWithBlock_29.Dragging;
		bool notDragging = logic_uScript_IsPlayerInteractingWithBlock_uScript_IsPlayerInteractingWithBlock_29.NotDragging;
		if (dragging)
		{
			Relay_In_133();
		}
		if (notDragging)
		{
			Relay_In_136();
		}
	}

	private void Relay_In_30()
	{
		logic_uScript_GetPlayerTankWithBlock_tankBlock_30 = local_Solar_Generator_Block_TankBlock;
		logic_uScript_GetPlayerTankWithBlock_Return_30 = logic_uScript_GetPlayerTankWithBlock_uScript_GetPlayerTankWithBlock_30.In(logic_uScript_GetPlayerTankWithBlock_block_30, logic_uScript_GetPlayerTankWithBlock_tankBlock_30, logic_uScript_GetPlayerTankWithBlock_useBlockType_30);
		local_SolarGenTech_Tank = logic_uScript_GetPlayerTankWithBlock_Return_30;
		if (logic_uScript_GetPlayerTankWithBlock_uScript_GetPlayerTankWithBlock_30.Returned)
		{
			Relay_In_32();
		}
	}

	private void Relay_In_32()
	{
		logic_uScript_DoesTankHave_tank_32 = local_SolarGenTech_Tank;
		logic_uScript_DoesTankHave_uScript_DoesTankHave_32.In(logic_uScript_DoesTankHave_tank_32, logic_uScript_DoesTankHave_amountOfPieces_32, logic_uScript_DoesTankHave_block_32, logic_uScript_DoesTankHave_checkForAmountOnly_32);
		bool num = logic_uScript_DoesTankHave_uScript_DoesTankHave_32.True;
		bool flag = logic_uScript_DoesTankHave_uScript_DoesTankHave_32.False;
		if (num)
		{
			Relay_In_28();
		}
		if (flag)
		{
			Relay_In_112();
		}
	}

	private void Relay_In_38()
	{
		logic_uScript_GetPlayerTankWithBlock_tankBlock_38 = local_RepairBubbleBlock_TankBlock;
		logic_uScript_GetPlayerTankWithBlock_Return_38 = logic_uScript_GetPlayerTankWithBlock_uScript_GetPlayerTankWithBlock_38.In(logic_uScript_GetPlayerTankWithBlock_block_38, logic_uScript_GetPlayerTankWithBlock_tankBlock_38, logic_uScript_GetPlayerTankWithBlock_useBlockType_38);
		local_RepairTech_Tank = logic_uScript_GetPlayerTankWithBlock_Return_38;
		bool returned = logic_uScript_GetPlayerTankWithBlock_uScript_GetPlayerTankWithBlock_38.Returned;
		bool notReturned = logic_uScript_GetPlayerTankWithBlock_uScript_GetPlayerTankWithBlock_38.NotReturned;
		if (returned)
		{
			Relay_In_59();
		}
		if (notReturned)
		{
			Relay_In_55();
		}
	}

	private void Relay_In_39()
	{
		logic_uScript_IsPlayerInRangeOfBlock_block_39 = local_RepairBubbleBlock_TankBlock;
		logic_uScript_IsPlayerInRangeOfBlock_uScript_IsPlayerInRangeOfBlock_39.In(logic_uScript_IsPlayerInRangeOfBlock_block_39, logic_uScript_IsPlayerInRangeOfBlock_range_39);
		bool inRange = logic_uScript_IsPlayerInRangeOfBlock_uScript_IsPlayerInRangeOfBlock_39.InRange;
		bool outOfRange = logic_uScript_IsPlayerInRangeOfBlock_uScript_IsPlayerInRangeOfBlock_39.OutOfRange;
		if (inRange)
		{
			Relay_In_76();
		}
		if (outOfRange)
		{
			Relay_In_205();
		}
	}

	private void Relay_In_44()
	{
		logic_uScript_MoveEncounterWithVisible_ownerNode_44 = owner_Connection_43;
		logic_uScript_MoveEncounterWithVisible_visibleObject_44 = local_Solar_Generator_Block_TankBlock;
		logic_uScript_MoveEncounterWithVisible_uScript_MoveEncounterWithVisible_44.In(logic_uScript_MoveEncounterWithVisible_ownerNode_44, logic_uScript_MoveEncounterWithVisible_visibleObject_44);
		if (logic_uScript_MoveEncounterWithVisible_uScript_MoveEncounterWithVisible_44.Out)
		{
			Relay_In_398();
		}
	}

	private void Relay_In_45()
	{
		logic_uScript_IsPlayerInRangeOfBlock_block_45 = local_Solar_Generator_Block_TankBlock;
		logic_uScript_IsPlayerInRangeOfBlock_uScript_IsPlayerInRangeOfBlock_45.In(logic_uScript_IsPlayerInRangeOfBlock_block_45, logic_uScript_IsPlayerInRangeOfBlock_range_45);
		bool inRange = logic_uScript_IsPlayerInRangeOfBlock_uScript_IsPlayerInRangeOfBlock_45.InRange;
		bool outOfRange = logic_uScript_IsPlayerInRangeOfBlock_uScript_IsPlayerInRangeOfBlock_45.OutOfRange;
		if (inRange)
		{
			Relay_In_249();
		}
		if (outOfRange)
		{
			Relay_In_194();
		}
	}

	private void Relay_In_47()
	{
		logic_uScriptCon_CompareInt_A_47 = local_Stage_System_Int32;
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_47.In(logic_uScriptCon_CompareInt_A_47, logic_uScriptCon_CompareInt_B_47);
		bool equalTo = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_47.EqualTo;
		bool notEqualTo = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_47.NotEqualTo;
		if (equalTo)
		{
			Relay_In_52();
		}
		if (notEqualTo)
		{
			Relay_In_403();
		}
	}

	private void Relay_In_49()
	{
		logic_uScript_MoveEncounterWithVisible_ownerNode_49 = owner_Connection_50;
		logic_uScript_MoveEncounterWithVisible_visibleObject_49 = local_RepairBubbleBlock_TankBlock;
		logic_uScript_MoveEncounterWithVisible_uScript_MoveEncounterWithVisible_49.In(logic_uScript_MoveEncounterWithVisible_ownerNode_49, logic_uScript_MoveEncounterWithVisible_visibleObject_49);
		if (logic_uScript_MoveEncounterWithVisible_uScript_MoveEncounterWithVisible_49.Out)
		{
			Relay_In_398();
		}
	}

	private void Relay_In_52()
	{
		logic_uScript_SpawnBlockAbovePlayer_owner_52 = owner_Connection_53;
		logic_uScript_SpawnBlockAbovePlayer_Return_52 = logic_uScript_SpawnBlockAbovePlayer_uScript_SpawnBlockAbovePlayer_52.In(logic_uScript_SpawnBlockAbovePlayer_blockType_52, logic_uScript_SpawnBlockAbovePlayer_uniqueName_52, logic_uScript_SpawnBlockAbovePlayer_owner_52);
		local_RepairBubbleBlock_TankBlock = logic_uScript_SpawnBlockAbovePlayer_Return_52;
		if (logic_uScript_SpawnBlockAbovePlayer_uScript_SpawnBlockAbovePlayer_52.Out)
		{
			Relay_In_49();
		}
	}

	private void Relay_In_55()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_55.In(logic_uScriptAct_SetInt_Value_55, out logic_uScriptAct_SetInt_Target_55);
		local_Stage_System_Int32 = logic_uScriptAct_SetInt_Target_55;
	}

	private void Relay_In_58()
	{
		logic_uScript_GetPlayerTank_Return_58 = logic_uScript_GetPlayerTank_uScript_GetPlayerTank_58.In();
		local_PlayerTank_Tank = logic_uScript_GetPlayerTank_Return_58;
		if (logic_uScript_GetPlayerTank_uScript_GetPlayerTank_58.Returned)
		{
			Relay_In_245();
		}
	}

	private void Relay_In_59()
	{
		logic_uScript_IsShieldBlockPowered_shieldBlock_59 = local_RepairBubbleBlock_TankBlock;
		logic_uScript_IsShieldBlockPowered_uScript_IsShieldBlockPowered_59.In(logic_uScript_IsShieldBlockPowered_shieldBlock_59);
		if (logic_uScript_IsShieldBlockPowered_uScript_IsShieldBlockPowered_59.True)
		{
			Relay_In_389();
		}
	}

	private void Relay_In_62()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_62 = messageTag;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_62.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_62, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_62);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_62.Out)
		{
			Relay_In_243();
		}
	}

	private void Relay_In_64()
	{
		logic_uScriptCon_CompareBool_Bool_64 = local_AddedQuestLog_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_64.In(logic_uScriptCon_CompareBool_Bool_64);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_64.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_64.False;
		if (num)
		{
			Relay_In_58();
		}
		if (flag)
		{
			Relay_In_66();
		}
	}

	private void Relay_True_65()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_65.True(out logic_uScriptAct_SetBool_Target_65);
		local_AddedQuestLog_System_Boolean = logic_uScriptAct_SetBool_Target_65;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_65.Out)
		{
			Relay_In_81();
		}
	}

	private void Relay_False_65()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_65.False(out logic_uScriptAct_SetBool_Target_65);
		local_AddedQuestLog_System_Boolean = logic_uScriptAct_SetBool_Target_65;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_65.Out)
		{
			Relay_In_81();
		}
	}

	private void Relay_In_66()
	{
		logic_uScript_PlayerInRangeOfCurrentEncounter_ownerNode_66 = owner_Connection_68;
		logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_66.In(logic_uScript_PlayerInRangeOfCurrentEncounter_ownerNode_66);
		bool num = logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_66.True;
		bool flag = logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_66.False;
		if (num)
		{
			Relay_True_65();
		}
		if (flag)
		{
			Relay_In_78();
		}
	}

	private void Relay_In_67()
	{
		logic_uScript_Wait_uScript_Wait_67.In(logic_uScript_Wait_seconds_67, logic_uScript_Wait_repeat_67);
		if (logic_uScript_Wait_uScript_Wait_67.Waited)
		{
			Relay_In_180();
		}
	}

	private void Relay_In_73()
	{
		logic_uScript_SetQuestLogVisible_uScript_SetQuestLogVisible_73.In(logic_uScript_SetQuestLogVisible_visible_73);
		if (logic_uScript_SetQuestLogVisible_uScript_SetQuestLogVisible_73.Out)
		{
			Relay_In_163();
		}
	}

	private void Relay_OnUpdate_75()
	{
		Relay_In_15();
	}

	private void Relay_OnSuspend_75()
	{
	}

	private void Relay_OnResume_75()
	{
	}

	private void Relay_In_76()
	{
		logic_uScript_SetTechBlocksHealth_tech_76 = local_PlayerTank_Tank;
		logic_uScript_SetTechBlocksHealth_uScript_SetTechBlocksHealth_76.In(logic_uScript_SetTechBlocksHealth_tech_76, logic_uScript_SetTechBlocksHealth_healthPercentage_76);
		if (logic_uScript_SetTechBlocksHealth_uScript_SetTechBlocksHealth_76.Out)
		{
			Relay_In_162();
		}
	}

	private void Relay_In_78()
	{
		logic_uScript_IsCoreEncounterCompleted_uScript_IsCoreEncounterCompleted_78.In(logic_uScript_IsCoreEncounterCompleted_corp_78, logic_uScript_IsCoreEncounterCompleted_grade_78, logic_uScript_IsCoreEncounterCompleted_encounterName_78);
		bool num = logic_uScript_IsCoreEncounterCompleted_uScript_IsCoreEncounterCompleted_78.True;
		bool flag = logic_uScript_IsCoreEncounterCompleted_uScript_IsCoreEncounterCompleted_78.False;
		if (num)
		{
			Relay_In_67();
		}
		if (flag)
		{
			Relay_In_183();
		}
	}

	private void Relay_In_80()
	{
		logic_uScript_KeepBlockInvulnerable_block_80 = local_RepairBubbleBlock_TankBlock;
		logic_uScript_KeepBlockInvulnerable_uScript_KeepBlockInvulnerable_80.In(logic_uScript_KeepBlockInvulnerable_block_80);
		if (logic_uScript_KeepBlockInvulnerable_uScript_KeepBlockInvulnerable_80.Out)
		{
			Relay_In_64();
		}
	}

	private void Relay_In_81()
	{
		logic_uScript_SetMissionsVisibleInHud_uScript_SetMissionsVisibleInHud_81.In(logic_uScript_SetMissionsVisibleInHud_visible_81);
		if (logic_uScript_SetMissionsVisibleInHud_uScript_SetMissionsVisibleInHud_81.Out)
		{
			Relay_In_73();
		}
	}

	private void Relay_Save_Out_82()
	{
		Relay_Save_147();
	}

	private void Relay_Load_Out_82()
	{
		Relay_Load_147();
	}

	private void Relay_Restart_Out_82()
	{
		Relay_Set_False_147();
	}

	private void Relay_Save_82()
	{
		logic_SubGraph_SaveLoadInt_integer_82 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_82 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_82.Save(logic_SubGraph_SaveLoadInt_restartValue_82, ref logic_SubGraph_SaveLoadInt_integer_82, logic_SubGraph_SaveLoadInt_intAsVariable_82, logic_SubGraph_SaveLoadInt_uniqueID_82);
	}

	private void Relay_Load_82()
	{
		logic_SubGraph_SaveLoadInt_integer_82 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_82 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_82.Load(logic_SubGraph_SaveLoadInt_restartValue_82, ref logic_SubGraph_SaveLoadInt_integer_82, logic_SubGraph_SaveLoadInt_intAsVariable_82, logic_SubGraph_SaveLoadInt_uniqueID_82);
	}

	private void Relay_Restart_82()
	{
		logic_SubGraph_SaveLoadInt_integer_82 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_82 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_82.Restart(logic_SubGraph_SaveLoadInt_restartValue_82, ref logic_SubGraph_SaveLoadInt_integer_82, logic_SubGraph_SaveLoadInt_intAsVariable_82, logic_SubGraph_SaveLoadInt_uniqueID_82);
	}

	private void Relay_Save_Out_83()
	{
		Relay_Save_89();
	}

	private void Relay_Load_Out_83()
	{
		Relay_Load_89();
	}

	private void Relay_Restart_Out_83()
	{
		Relay_Set_False_89();
	}

	private void Relay_Save_83()
	{
		logic_SubGraph_SaveLoadBool_boolean_83 = local_AddedQuestLog_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_83 = local_AddedQuestLog_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_83.Save(ref logic_SubGraph_SaveLoadBool_boolean_83, logic_SubGraph_SaveLoadBool_boolAsVariable_83, logic_SubGraph_SaveLoadBool_uniqueID_83);
	}

	private void Relay_Load_83()
	{
		logic_SubGraph_SaveLoadBool_boolean_83 = local_AddedQuestLog_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_83 = local_AddedQuestLog_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_83.Load(ref logic_SubGraph_SaveLoadBool_boolean_83, logic_SubGraph_SaveLoadBool_boolAsVariable_83, logic_SubGraph_SaveLoadBool_uniqueID_83);
	}

	private void Relay_Set_True_83()
	{
		logic_SubGraph_SaveLoadBool_boolean_83 = local_AddedQuestLog_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_83 = local_AddedQuestLog_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_83.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_83, logic_SubGraph_SaveLoadBool_boolAsVariable_83, logic_SubGraph_SaveLoadBool_uniqueID_83);
	}

	private void Relay_Set_False_83()
	{
		logic_SubGraph_SaveLoadBool_boolean_83 = local_AddedQuestLog_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_83 = local_AddedQuestLog_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_83.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_83, logic_SubGraph_SaveLoadBool_boolAsVariable_83, logic_SubGraph_SaveLoadBool_uniqueID_83);
	}

	private void Relay_SaveEvent_84()
	{
		Relay_Save_82();
	}

	private void Relay_LoadEvent_84()
	{
		Relay_Load_82();
	}

	private void Relay_RestartEvent_84()
	{
		Relay_Restart_82();
	}

	private void Relay_Save_Out_89()
	{
		Relay_Save_99();
	}

	private void Relay_Load_Out_89()
	{
		Relay_Load_99();
	}

	private void Relay_Restart_Out_89()
	{
		Relay_Set_False_99();
	}

	private void Relay_Save_89()
	{
		logic_SubGraph_SaveLoadBool_boolean_89 = local_SolGenFoundMsgShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_89 = local_SolGenFoundMsgShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_89.Save(ref logic_SubGraph_SaveLoadBool_boolean_89, logic_SubGraph_SaveLoadBool_boolAsVariable_89, logic_SubGraph_SaveLoadBool_uniqueID_89);
	}

	private void Relay_Load_89()
	{
		logic_SubGraph_SaveLoadBool_boolean_89 = local_SolGenFoundMsgShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_89 = local_SolGenFoundMsgShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_89.Load(ref logic_SubGraph_SaveLoadBool_boolean_89, logic_SubGraph_SaveLoadBool_boolAsVariable_89, logic_SubGraph_SaveLoadBool_uniqueID_89);
	}

	private void Relay_Set_True_89()
	{
		logic_SubGraph_SaveLoadBool_boolean_89 = local_SolGenFoundMsgShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_89 = local_SolGenFoundMsgShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_89.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_89, logic_SubGraph_SaveLoadBool_boolAsVariable_89, logic_SubGraph_SaveLoadBool_uniqueID_89);
	}

	private void Relay_Set_False_89()
	{
		logic_SubGraph_SaveLoadBool_boolean_89 = local_SolGenFoundMsgShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_89 = local_SolGenFoundMsgShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_89.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_89, logic_SubGraph_SaveLoadBool_boolAsVariable_89, logic_SubGraph_SaveLoadBool_uniqueID_89);
	}

	private void Relay_True_90()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_90.True(out logic_uScriptAct_SetBool_Target_90);
		local_SolGenFoundMsgShown_System_Boolean = logic_uScriptAct_SetBool_Target_90;
	}

	private void Relay_False_90()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_90.False(out logic_uScriptAct_SetBool_Target_90);
		local_SolGenFoundMsgShown_System_Boolean = logic_uScriptAct_SetBool_Target_90;
	}

	private void Relay_In_92()
	{
		logic_uScriptCon_CompareBool_Bool_92 = local_SolGenFoundMsgShown_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_92.In(logic_uScriptCon_CompareBool_Bool_92);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_92.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_92.False;
		if (num)
		{
			Relay_In_324();
		}
		if (flag)
		{
			Relay_In_186();
		}
	}

	private void Relay_In_95()
	{
		logic_uScriptCon_CompareBool_Bool_95 = local_RegenSpawnedMsgShown_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_95.In(logic_uScriptCon_CompareBool_Bool_95);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_95.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_95.False;
		if (num)
		{
			Relay_In_193();
		}
		if (flag)
		{
			Relay_In_190();
		}
	}

	private void Relay_True_96()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_96.True(out logic_uScriptAct_SetBool_Target_96);
		local_RegenSpawnedMsgShown_System_Boolean = logic_uScriptAct_SetBool_Target_96;
	}

	private void Relay_False_96()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_96.False(out logic_uScriptAct_SetBool_Target_96);
		local_RegenSpawnedMsgShown_System_Boolean = logic_uScriptAct_SetBool_Target_96;
	}

	private void Relay_Save_Out_99()
	{
		Relay_Save_393();
	}

	private void Relay_Load_Out_99()
	{
		Relay_Load_393();
	}

	private void Relay_Restart_Out_99()
	{
		Relay_Set_False_393();
	}

	private void Relay_Save_99()
	{
		logic_SubGraph_SaveLoadBool_boolean_99 = local_RegenSpawnedMsgShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_99 = local_RegenSpawnedMsgShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_99.Save(ref logic_SubGraph_SaveLoadBool_boolean_99, logic_SubGraph_SaveLoadBool_boolAsVariable_99, logic_SubGraph_SaveLoadBool_uniqueID_99);
	}

	private void Relay_Load_99()
	{
		logic_SubGraph_SaveLoadBool_boolean_99 = local_RegenSpawnedMsgShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_99 = local_RegenSpawnedMsgShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_99.Load(ref logic_SubGraph_SaveLoadBool_boolean_99, logic_SubGraph_SaveLoadBool_boolAsVariable_99, logic_SubGraph_SaveLoadBool_uniqueID_99);
	}

	private void Relay_Set_True_99()
	{
		logic_SubGraph_SaveLoadBool_boolean_99 = local_RegenSpawnedMsgShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_99 = local_RegenSpawnedMsgShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_99.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_99, logic_SubGraph_SaveLoadBool_boolAsVariable_99, logic_SubGraph_SaveLoadBool_uniqueID_99);
	}

	private void Relay_Set_False_99()
	{
		logic_SubGraph_SaveLoadBool_boolean_99 = local_RegenSpawnedMsgShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_99 = local_RegenSpawnedMsgShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_99.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_99, logic_SubGraph_SaveLoadBool_boolAsVariable_99, logic_SubGraph_SaveLoadBool_uniqueID_99);
	}

	private void Relay_Pause_100()
	{
		logic_uScript_PausePopulation_uScript_PausePopulation_100.Pause();
		if (logic_uScript_PausePopulation_uScript_PausePopulation_100.Out)
		{
			Relay_In_416();
		}
	}

	private void Relay_UnPause_100()
	{
		logic_uScript_PausePopulation_uScript_PausePopulation_100.UnPause();
		if (logic_uScript_PausePopulation_uScript_PausePopulation_100.Out)
		{
			Relay_In_416();
		}
	}

	private void Relay_In_102()
	{
		logic_uScript_LockTech_tech_102 = local_SolarGenTech_Tank;
		logic_uScript_LockTech_uScript_LockTech_102.In(logic_uScript_LockTech_tech_102, logic_uScript_LockTech_lockType_102);
		if (logic_uScript_LockTech_uScript_LockTech_102.Out)
		{
			Relay_In_225();
		}
	}

	private void Relay_In_103()
	{
		logic_uScript_GetPlayerTankWithBlock_tankBlock_103 = local_Solar_Generator_Block_TankBlock;
		logic_uScript_GetPlayerTankWithBlock_Return_103 = logic_uScript_GetPlayerTankWithBlock_uScript_GetPlayerTankWithBlock_103.In(logic_uScript_GetPlayerTankWithBlock_block_103, logic_uScript_GetPlayerTankWithBlock_tankBlock_103, logic_uScript_GetPlayerTankWithBlock_useBlockType_103);
		local_SolarGenTech_Tank = logic_uScript_GetPlayerTankWithBlock_Return_103;
		if (logic_uScript_GetPlayerTankWithBlock_uScript_GetPlayerTankWithBlock_103.Returned)
		{
			Relay_In_102();
		}
	}

	private void Relay_In_106()
	{
		logic_uScript_GetTankBlock_tank_106 = local_SolarGenTech_Tank;
		logic_uScript_GetTankBlock_Return_106 = logic_uScript_GetTankBlock_uScript_GetTankBlock_106.In(logic_uScript_GetTankBlock_tank_106, logic_uScript_GetTankBlock_blockType_106);
		local_RepairBubbleBlock_TankBlock = logic_uScript_GetTankBlock_Return_106;
		bool returned = logic_uScript_GetTankBlock_uScript_GetTankBlock_106.Returned;
		bool notFound = logic_uScript_GetTankBlock_uScript_GetTankBlock_106.NotFound;
		if (returned)
		{
			Relay_In_139();
		}
		if (notFound)
		{
			Relay_In_109();
		}
	}

	private void Relay_In_109()
	{
		logic_uScript_GetNamedBlock_owner_109 = owner_Connection_8;
		logic_uScript_GetNamedBlock_Return_109 = logic_uScript_GetNamedBlock_uScript_GetNamedBlock_109.In(logic_uScript_GetNamedBlock_name_109, logic_uScript_GetNamedBlock_owner_109);
		local_RepairBubbleBlock_TankBlock = logic_uScript_GetNamedBlock_Return_109;
		bool destroyed = logic_uScript_GetNamedBlock_uScript_GetNamedBlock_109.Destroyed;
		bool blockExists = logic_uScript_GetNamedBlock_uScript_GetNamedBlock_109.BlockExists;
		if (destroyed)
		{
			Relay_In_10();
		}
		if (blockExists)
		{
			Relay_In_167();
		}
	}

	private void Relay_In_110()
	{
		logic_uScript_PointArrowAtVisible_targetObject_110 = local_Solar_Generator_Block_TankBlock;
		logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_110.In(logic_uScript_PointArrowAtVisible_targetObject_110, logic_uScript_PointArrowAtVisible_timeToShowFor_110, logic_uScript_PointArrowAtVisible_offset_110);
		if (logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_110.Out)
		{
			Relay_In_299();
		}
	}

	private void Relay_In_112()
	{
		logic_uScript_PointArrowAtVisible_targetObject_112 = local_RepairBubbleBlock_TankBlock;
		logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_112.In(logic_uScript_PointArrowAtVisible_targetObject_112, logic_uScript_PointArrowAtVisible_timeToShowFor_112, logic_uScript_PointArrowAtVisible_offset_112);
		if (logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_112.Out)
		{
			Relay_In_29();
		}
	}

	private void Relay_In_114()
	{
		logic_uScript_HideArrow_uScript_HideArrow_114.In();
		if (logic_uScript_HideArrow_uScript_HideArrow_114.Out)
		{
			Relay_In_130();
		}
	}

	private void Relay_In_115()
	{
		logic_uScript_DirectEnemiesOutOfEncounter_ownerNode_115 = owner_Connection_116;
		logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_115.In(logic_uScript_DirectEnemiesOutOfEncounter_ownerNode_115);
		if (logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_115.Out)
		{
			Relay_In_244();
		}
	}

	private void Relay_In_118()
	{
		logic_uScriptCon_CompareBool_Bool_118 = local_GhostBlockRegenSpawned_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_118.In(logic_uScriptCon_CompareBool_Bool_118);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_118.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_118.False;
		if (num)
		{
			Relay_In_122();
		}
		if (flag)
		{
			Relay_True_125();
		}
	}

	private void Relay_AtIndex_119()
	{
		int num = 0;
		Array array = local_120_TankBlockArray;
		if (logic_uScript_AccessListBlock_blockList_119.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListBlock_blockList_119, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListBlock_blockList_119, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListBlock_uScript_AccessListBlock_119.AtIndex(ref logic_uScript_AccessListBlock_blockList_119, logic_uScript_AccessListBlock_index_119, out logic_uScript_AccessListBlock_value_119);
		local_120_TankBlockArray = logic_uScript_AccessListBlock_blockList_119;
		local_GhostBlockRegen_TankBlock = logic_uScript_AccessListBlock_value_119;
		if (logic_uScript_AccessListBlock_uScript_AccessListBlock_119.Out)
		{
			Relay_In_122();
		}
	}

	private void Relay_In_122()
	{
		logic_uScript_PointArrowAtVisible_targetObject_122 = local_GhostBlockRegen_TankBlock;
		logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_122.In(logic_uScript_PointArrowAtVisible_targetObject_122, logic_uScript_PointArrowAtVisible_timeToShowFor_122, logic_uScript_PointArrowAtVisible_offset_122);
		if (logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_122.Out)
		{
			Relay_In_308();
		}
	}

	private void Relay_TrySpawnOnTech_124()
	{
		int num = 0;
		Array array = ghostBlockRegen;
		if (logic_uScript_SpawnGhostBlocks_ghostBlockData_124.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SpawnGhostBlocks_ghostBlockData_124, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SpawnGhostBlocks_ghostBlockData_124, num, array.Length);
		num += array.Length;
		logic_uScript_SpawnGhostBlocks_ownerNode_124 = owner_Connection_121;
		logic_uScript_SpawnGhostBlocks_targetTech_124 = local_SolarGenTech_Tank;
		logic_uScript_SpawnGhostBlocks_Return_124 = logic_uScript_SpawnGhostBlocks_uScript_SpawnGhostBlocks_124.TrySpawnOnTech(logic_uScript_SpawnGhostBlocks_ghostBlockData_124, logic_uScript_SpawnGhostBlocks_ownerNode_124, logic_uScript_SpawnGhostBlocks_targetTech_124);
		local_120_TankBlockArray = logic_uScript_SpawnGhostBlocks_Return_124;
		if (logic_uScript_SpawnGhostBlocks_uScript_SpawnGhostBlocks_124.OnAlreadySpawned)
		{
			Relay_AtIndex_119();
		}
	}

	private void Relay_True_125()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_125.True(out logic_uScriptAct_SetBool_Target_125);
		local_GhostBlockRegenSpawned_System_Boolean = logic_uScriptAct_SetBool_Target_125;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_125.Out)
		{
			Relay_TrySpawnOnTech_124();
		}
	}

	private void Relay_False_125()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_125.False(out logic_uScriptAct_SetBool_Target_125);
		local_GhostBlockRegenSpawned_System_Boolean = logic_uScriptAct_SetBool_Target_125;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_125.Out)
		{
			Relay_TrySpawnOnTech_124();
		}
	}

	private void Relay_True_128()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_128.True(out logic_uScriptAct_SetBool_Target_128);
		local_GhostBlockRegenSpawned_System_Boolean = logic_uScriptAct_SetBool_Target_128;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_128.Out)
		{
			Relay_False_218();
		}
	}

	private void Relay_False_128()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_128.False(out logic_uScriptAct_SetBool_Target_128);
		local_GhostBlockRegenSpawned_System_Boolean = logic_uScriptAct_SetBool_Target_128;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_128.Out)
		{
			Relay_False_218();
		}
	}

	private void Relay_In_130()
	{
		logic_uScript_RemoveAllGhostBlocksOnTech_targetTech_130 = local_SolarGenTech_Tank;
		logic_uScript_RemoveAllGhostBlocksOnTech_uScript_RemoveAllGhostBlocksOnTech_130.In(logic_uScript_RemoveAllGhostBlocksOnTech_targetTech_130);
		if (logic_uScript_RemoveAllGhostBlocksOnTech_uScript_RemoveAllGhostBlocksOnTech_130.Out)
		{
			Relay_In_248();
		}
	}

	private void Relay_In_132()
	{
		logic_uScript_LockBlockAttach_block_132 = local_Solar_Generator_Block_TankBlock;
		logic_uScript_LockBlockAttach_uScript_LockBlockAttach_132.In(logic_uScript_LockBlockAttach_block_132);
		if (logic_uScript_LockBlockAttach_uScript_LockBlockAttach_132.Out)
		{
			Relay_In_92();
		}
	}

	private void Relay_In_133()
	{
		logic_uScript_LockTutorialBlockAttach_block_133 = local_RepairBubbleBlock_TankBlock;
		logic_uScript_LockTutorialBlockAttach_uScript_LockTutorialBlockAttach_133.In(logic_uScript_LockTutorialBlockAttach_block_133);
		if (logic_uScript_LockTutorialBlockAttach_uScript_LockTutorialBlockAttach_133.Out)
		{
			Relay_In_135();
		}
	}

	private void Relay_In_135()
	{
		logic_uScript_ChangeBuildingOptions_uScript_ChangeBuildingOptions_135.In(logic_uScript_ChangeBuildingOptions_change_135, logic_uScript_ChangeBuildingOptions_allow_135);
		if (logic_uScript_ChangeBuildingOptions_uScript_ChangeBuildingOptions_135.Out)
		{
			Relay_In_118();
		}
	}

	private void Relay_In_136()
	{
		logic_uScript_ChangeBuildingOptions_uScript_ChangeBuildingOptions_136.In(logic_uScript_ChangeBuildingOptions_change_136, logic_uScript_ChangeBuildingOptions_allow_136);
		if (logic_uScript_ChangeBuildingOptions_uScript_ChangeBuildingOptions_136.Out)
		{
			Relay_In_300();
		}
	}

	private void Relay_In_137()
	{
		logic_uScript_ChangeBuildingOptions_uScript_ChangeBuildingOptions_137.In(logic_uScript_ChangeBuildingOptions_change_137, logic_uScript_ChangeBuildingOptions_allow_137);
		if (logic_uScript_ChangeBuildingOptions_uScript_ChangeBuildingOptions_137.Out)
		{
			Relay_In_254();
		}
	}

	private void Relay_In_138()
	{
		logic_uScript_ChangeBuildingOptions_uScript_ChangeBuildingOptions_138.In(logic_uScript_ChangeBuildingOptions_change_138, logic_uScript_ChangeBuildingOptions_allow_138);
		if (logic_uScript_ChangeBuildingOptions_uScript_ChangeBuildingOptions_138.Out)
		{
			Relay_In_253();
		}
	}

	private void Relay_In_139()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_139.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_139.Out)
		{
			Relay_In_38();
		}
	}

	private void Relay_In_140()
	{
		int num = 0;
		Array array = blockDataSolarGen;
		if (logic_uScript_SpawnBlocksFromData_blockData_140.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SpawnBlocksFromData_blockData_140, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SpawnBlocksFromData_blockData_140, num, array.Length);
		num += array.Length;
		logic_uScript_SpawnBlocksFromData_ownerNode_140 = owner_Connection_142;
		logic_uScript_SpawnBlocksFromData_uScript_SpawnBlocksFromData_140.In(logic_uScript_SpawnBlocksFromData_blockData_140, logic_uScript_SpawnBlocksFromData_ownerNode_140);
		if (logic_uScript_SpawnBlocksFromData_uScript_SpawnBlocksFromData_140.Out)
		{
			Relay_In_158();
		}
	}

	private void Relay_In_143()
	{
		logic_uScriptCon_CompareBool_Bool_143 = local_SolGenSpawned_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_143.In(logic_uScriptCon_CompareBool_Bool_143);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_143.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_143.False;
		if (num)
		{
			Relay_In_148();
		}
		if (flag)
		{
			Relay_True_144();
		}
	}

	private void Relay_True_144()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_144.True(out logic_uScriptAct_SetBool_Target_144);
		local_SolGenSpawned_System_Boolean = logic_uScriptAct_SetBool_Target_144;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_144.Out)
		{
			Relay_In_140();
		}
	}

	private void Relay_False_144()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_144.False(out logic_uScriptAct_SetBool_Target_144);
		local_SolGenSpawned_System_Boolean = logic_uScriptAct_SetBool_Target_144;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_144.Out)
		{
			Relay_In_140();
		}
	}

	private void Relay_Save_Out_147()
	{
		Relay_Save_83();
	}

	private void Relay_Load_Out_147()
	{
		Relay_Load_83();
	}

	private void Relay_Restart_Out_147()
	{
		Relay_Set_False_83();
	}

	private void Relay_Save_147()
	{
		logic_SubGraph_SaveLoadBool_boolean_147 = local_SolGenSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_147 = local_SolGenSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_147.Save(ref logic_SubGraph_SaveLoadBool_boolean_147, logic_SubGraph_SaveLoadBool_boolAsVariable_147, logic_SubGraph_SaveLoadBool_uniqueID_147);
	}

	private void Relay_Load_147()
	{
		logic_SubGraph_SaveLoadBool_boolean_147 = local_SolGenSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_147 = local_SolGenSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_147.Load(ref logic_SubGraph_SaveLoadBool_boolean_147, logic_SubGraph_SaveLoadBool_boolAsVariable_147, logic_SubGraph_SaveLoadBool_uniqueID_147);
	}

	private void Relay_Set_True_147()
	{
		logic_SubGraph_SaveLoadBool_boolean_147 = local_SolGenSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_147 = local_SolGenSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_147.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_147, logic_SubGraph_SaveLoadBool_boolAsVariable_147, logic_SubGraph_SaveLoadBool_uniqueID_147);
	}

	private void Relay_Set_False_147()
	{
		logic_SubGraph_SaveLoadBool_boolean_147 = local_SolGenSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_147 = local_SolGenSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_147.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_147, logic_SubGraph_SaveLoadBool_boolAsVariable_147, logic_SubGraph_SaveLoadBool_uniqueID_147);
	}

	private void Relay_In_148()
	{
		int num = 0;
		Array array = blockDataSolarGen;
		if (logic_uScript_GetAndCheckBlocks_blockData_148.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckBlocks_blockData_148, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckBlocks_blockData_148, num, array.Length);
		num += array.Length;
		logic_uScript_GetAndCheckBlocks_ownerNode_148 = owner_Connection_149;
		int num2 = 0;
		Array array2 = local_152_TankBlockArray;
		if (logic_uScript_GetAndCheckBlocks_blocks_148.Length != num2 + array2.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckBlocks_blocks_148, num2 + array2.Length);
		}
		Array.Copy(array2, 0, logic_uScript_GetAndCheckBlocks_blocks_148, num2, array2.Length);
		num2 += array2.Length;
		logic_uScript_GetAndCheckBlocks_uScript_GetAndCheckBlocks_148.In(logic_uScript_GetAndCheckBlocks_blockData_148, logic_uScript_GetAndCheckBlocks_ownerNode_148, ref logic_uScript_GetAndCheckBlocks_blocks_148);
		local_152_TankBlockArray = logic_uScript_GetAndCheckBlocks_blocks_148;
		bool allAlive = logic_uScript_GetAndCheckBlocks_uScript_GetAndCheckBlocks_148.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckBlocks_uScript_GetAndCheckBlocks_148.SomeAlive;
		if (allAlive)
		{
			Relay_AtIndex_151();
		}
		if (someAlive)
		{
			Relay_AtIndex_151();
		}
	}

	private void Relay_AtIndex_151()
	{
		int num = 0;
		Array array = local_152_TankBlockArray;
		if (logic_uScript_AccessListBlock_blockList_151.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListBlock_blockList_151, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListBlock_blockList_151, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListBlock_uScript_AccessListBlock_151.AtIndex(ref logic_uScript_AccessListBlock_blockList_151, logic_uScript_AccessListBlock_index_151, out logic_uScript_AccessListBlock_value_151);
		local_152_TankBlockArray = logic_uScript_AccessListBlock_blockList_151;
		local_Solar_Generator_Block_TankBlock = logic_uScript_AccessListBlock_value_151;
		if (logic_uScript_AccessListBlock_uScript_AccessListBlock_151.Out)
		{
			Relay_In_13();
		}
	}

	private void Relay_In_155()
	{
		logic_uScript_GetPlayerTankWithBlock_tankBlock_155 = local_Solar_Generator_Block_TankBlock;
		logic_uScript_GetPlayerTankWithBlock_Return_155 = logic_uScript_GetPlayerTankWithBlock_uScript_GetPlayerTankWithBlock_155.In(logic_uScript_GetPlayerTankWithBlock_block_155, logic_uScript_GetPlayerTankWithBlock_tankBlock_155, logic_uScript_GetPlayerTankWithBlock_useBlockType_155);
		local_SolarGenTech_Tank = logic_uScript_GetPlayerTankWithBlock_Return_155;
		if (logic_uScript_GetPlayerTankWithBlock_uScript_GetPlayerTankWithBlock_155.Returned)
		{
			Relay_In_156();
		}
	}

	private void Relay_In_156()
	{
		logic_uScript_LockTech_tech_156 = local_SolarGenTech_Tank;
		logic_uScript_LockTech_uScript_LockTech_156.In(logic_uScript_LockTech_tech_156, logic_uScript_LockTech_lockType_156);
		if (logic_uScript_LockTech_uScript_LockTech_156.Out)
		{
			Relay_In_313();
		}
	}

	private void Relay_In_158()
	{
		logic_uScript_RemoveScenery_ownerNode_158 = owner_Connection_159;
		logic_uScript_RemoveScenery_positionName_158 = PositionId;
		logic_uScript_RemoveScenery_radius_158 = clearSceneryRadius;
		logic_uScript_RemoveScenery_uScript_RemoveScenery_158.In(logic_uScript_RemoveScenery_ownerNode_158, logic_uScript_RemoveScenery_positionName_158, logic_uScript_RemoveScenery_radius_158, logic_uScript_RemoveScenery_preventChunksSpawning_158);
		if (logic_uScript_RemoveScenery_uScript_RemoveScenery_158.Out)
		{
			Relay_In_148();
		}
	}

	private void Relay_In_162()
	{
		logic_uScript_ClearTutorialTechToBuild_uScript_ClearTutorialTechToBuild_162.In();
		if (logic_uScript_ClearTutorialTechToBuild_uScript_ClearTutorialTechToBuild_162.Out)
		{
			Relay_True_392();
		}
	}

	private void Relay_In_163()
	{
		logic_uScript_ShowQuestLog_owner_163 = owner_Connection_164;
		logic_uScript_ShowQuestLog_uScript_ShowQuestLog_163.In(logic_uScript_ShowQuestLog_owner_163);
		if (logic_uScript_ShowQuestLog_uScript_ShowQuestLog_163.Out)
		{
			Relay_In_58();
		}
	}

	private void Relay_Succeed_165()
	{
		logic_uScript_FinishEncounter_owner_165 = owner_Connection_6;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_165.Succeed(logic_uScript_FinishEncounter_owner_165);
	}

	private void Relay_Fail_165()
	{
		logic_uScript_FinishEncounter_owner_165 = owner_Connection_6;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_165.Fail(logic_uScript_FinishEncounter_owner_165);
	}

	private void Relay_In_167()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_167.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_167.Out)
		{
			Relay_In_38();
		}
	}

	private void Relay_Out_169()
	{
	}

	private void Relay_In_169()
	{
		logic_SubGraph_LoadObjectiveStates_currentObjective_169 = local_Stage_System_Int32;
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_169.In(logic_SubGraph_LoadObjectiveStates_currentObjective_169);
	}

	private void Relay_Save_Out_172()
	{
	}

	private void Relay_Load_Out_172()
	{
		Relay_In_169();
	}

	private void Relay_Restart_Out_172()
	{
		Relay_False_128();
	}

	private void Relay_Save_172()
	{
		logic_SubGraph_SaveLoadBool_boolean_172 = local_MissionComplete_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_172 = local_MissionComplete_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_172.Save(ref logic_SubGraph_SaveLoadBool_boolean_172, logic_SubGraph_SaveLoadBool_boolAsVariable_172, logic_SubGraph_SaveLoadBool_uniqueID_172);
	}

	private void Relay_Load_172()
	{
		logic_SubGraph_SaveLoadBool_boolean_172 = local_MissionComplete_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_172 = local_MissionComplete_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_172.Load(ref logic_SubGraph_SaveLoadBool_boolean_172, logic_SubGraph_SaveLoadBool_boolAsVariable_172, logic_SubGraph_SaveLoadBool_uniqueID_172);
	}

	private void Relay_Set_True_172()
	{
		logic_SubGraph_SaveLoadBool_boolean_172 = local_MissionComplete_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_172 = local_MissionComplete_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_172.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_172, logic_SubGraph_SaveLoadBool_boolAsVariable_172, logic_SubGraph_SaveLoadBool_uniqueID_172);
	}

	private void Relay_Set_False_172()
	{
		logic_SubGraph_SaveLoadBool_boolean_172 = local_MissionComplete_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_172 = local_MissionComplete_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_172.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_172, logic_SubGraph_SaveLoadBool_boolAsVariable_172, logic_SubGraph_SaveLoadBool_uniqueID_172);
	}

	private void Relay_Out_174()
	{
	}

	private void Relay_In_174()
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_174 = local_Stage_System_Int32;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_174.In(ref logic_SubGraph_CompleteObjectiveStage_objectiveStage_174, logic_SubGraph_CompleteObjectiveStage_isFinalObjective_174);
	}

	private void Relay_Out_175()
	{
	}

	private void Relay_In_175()
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_175 = local_Stage_System_Int32;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_175.In(ref logic_SubGraph_CompleteObjectiveStage_objectiveStage_175, logic_SubGraph_CompleteObjectiveStage_isFinalObjective_175);
	}

	private void Relay_Out_177()
	{
	}

	private void Relay_In_177()
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_177 = local_Stage_System_Int32;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_177.In(ref logic_SubGraph_CompleteObjectiveStage_objectiveStage_177, logic_SubGraph_CompleteObjectiveStage_isFinalObjective_177);
	}

	private void Relay_In_179()
	{
		logic_uScript_ChangeBuildingOptions_uScript_ChangeBuildingOptions_179.In(logic_uScript_ChangeBuildingOptions_change_179, logic_uScript_ChangeBuildingOptions_allow_179);
		if (logic_uScript_ChangeBuildingOptions_uScript_ChangeBuildingOptions_179.Out)
		{
			Relay_In_177();
		}
	}

	private void Relay_In_180()
	{
		logic_uScript_AddMessage_messageData_180 = msg01FindSolarGenerator;
		logic_uScript_AddMessage_speaker_180 = messageSpeaker;
		logic_uScript_AddMessage_Return_180 = logic_uScript_AddMessage_uScript_AddMessage_180.In(logic_uScript_AddMessage_messageData_180, logic_uScript_AddMessage_speaker_180);
		if (logic_uScript_AddMessage_uScript_AddMessage_180.Out)
		{
			Relay_True_65();
		}
	}

	private void Relay_In_183()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_183.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_183.Out)
		{
			Relay_In_184();
		}
	}

	private void Relay_In_184()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_184.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_184.Out)
		{
			Relay_In_58();
		}
	}

	private void Relay_In_186()
	{
		logic_uScript_AddMessage_messageData_186 = msg02SolarGeneratorFound;
		logic_uScript_AddMessage_speaker_186 = messageSpeaker;
		logic_uScript_AddMessage_Return_186 = logic_uScript_AddMessage_uScript_AddMessage_186.In(logic_uScript_AddMessage_messageData_186, logic_uScript_AddMessage_speaker_186);
		bool num = logic_uScript_AddMessage_uScript_AddMessage_186.Out;
		bool shown = logic_uScript_AddMessage_uScript_AddMessage_186.Shown;
		if (num)
		{
			Relay_In_324();
		}
		if (shown)
		{
			Relay_True_90();
		}
	}

	private void Relay_In_190()
	{
		logic_uScript_AddMessage_messageData_190 = msg05RepairBubbleSpawned;
		logic_uScript_AddMessage_speaker_190 = messageSpeaker;
		logic_uScript_AddMessage_Return_190 = logic_uScript_AddMessage_uScript_AddMessage_190.In(logic_uScript_AddMessage_messageData_190, logic_uScript_AddMessage_speaker_190);
		local_Msg05RepairBubbleSpawned_ManOnScreenMessages_OnScreenMessage = logic_uScript_AddMessage_Return_190;
		if (logic_uScript_AddMessage_uScript_AddMessage_190.Shown)
		{
			Relay_True_96();
		}
	}

	private void Relay_In_193()
	{
		logic_uScript_AddMessage_messageData_193 = msg06PickUpRepairBubble;
		logic_uScript_AddMessage_speaker_193 = messageSpeaker;
		logic_uScript_AddMessage_Return_193 = logic_uScript_AddMessage_uScript_AddMessage_193.In(logic_uScript_AddMessage_messageData_193, logic_uScript_AddMessage_speaker_193);
		local_Msg06PickUpRepairBubble_ManOnScreenMessages_OnScreenMessage = logic_uScript_AddMessage_Return_193;
		if (logic_uScript_AddMessage_uScript_AddMessage_193.Out)
		{
			Relay_In_268();
		}
	}

	private void Relay_In_194()
	{
		logic_uScript_AddMessage_messageData_194 = msg08ReturnRepairBubble;
		logic_uScript_AddMessage_speaker_194 = messageSpeaker;
		logic_uScript_AddMessage_Return_194 = logic_uScript_AddMessage_uScript_AddMessage_194.In(logic_uScript_AddMessage_messageData_194, logic_uScript_AddMessage_speaker_194);
		local_Msg08ReturnRepairBubble_ManOnScreenMessages_OnScreenMessage = logic_uScript_AddMessage_Return_194;
		if (logic_uScript_AddMessage_uScript_AddMessage_194.Out)
		{
			Relay_In_295();
		}
	}

	private void Relay_In_197()
	{
		logic_uScript_AddMessage_messageData_197 = msg09RepairBubbleAttached;
		logic_uScript_AddMessage_speaker_197 = messageSpeaker;
		logic_uScript_AddMessage_Return_197 = logic_uScript_AddMessage_uScript_AddMessage_197.In(logic_uScript_AddMessage_messageData_197, logic_uScript_AddMessage_speaker_197);
		if (logic_uScript_AddMessage_uScript_AddMessage_197.Out)
		{
			Relay_In_114();
		}
	}

	private void Relay_In_202()
	{
		logic_uScript_AddMessage_messageData_202 = msg04SolarGeneratorAnchored;
		logic_uScript_AddMessage_speaker_202 = messageSpeaker;
		logic_uScript_AddMessage_Return_202 = logic_uScript_AddMessage_uScript_AddMessage_202.In(logic_uScript_AddMessage_messageData_202, logic_uScript_AddMessage_speaker_202);
		if (logic_uScript_AddMessage_uScript_AddMessage_202.Shown)
		{
			Relay_In_175();
		}
	}

	private void Relay_In_205()
	{
		logic_uScript_AddMessage_messageData_205 = msg10MoveInsideRepairBubble;
		logic_uScript_AddMessage_speaker_205 = messageSpeaker;
		logic_uScript_AddMessage_Return_205 = logic_uScript_AddMessage_uScript_AddMessage_205.In(logic_uScript_AddMessage_messageData_205, logic_uScript_AddMessage_speaker_205);
		local_Msg10MoveInsideRepairBubble_ManOnScreenMessages_OnScreenMessage = logic_uScript_AddMessage_Return_205;
		if (logic_uScript_AddMessage_uScript_AddMessage_205.Out)
		{
			Relay_In_413();
		}
	}

	private void Relay_In_206()
	{
		logic_uScript_IsCoreEncounterCompleted_uScript_IsCoreEncounterCompleted_206.In(logic_uScript_IsCoreEncounterCompleted_corp_206, logic_uScript_IsCoreEncounterCompleted_grade_206, logic_uScript_IsCoreEncounterCompleted_encounterName_206);
		bool num = logic_uScript_IsCoreEncounterCompleted_uScript_IsCoreEncounterCompleted_206.True;
		bool flag = logic_uScript_IsCoreEncounterCompleted_uScript_IsCoreEncounterCompleted_206.False;
		if (num)
		{
			Relay_In_207();
		}
		if (flag)
		{
			Relay_In_211();
		}
	}

	private void Relay_In_207()
	{
		logic_uScript_IsCoreEncounterCompleted_uScript_IsCoreEncounterCompleted_207.In(logic_uScript_IsCoreEncounterCompleted_corp_207, logic_uScript_IsCoreEncounterCompleted_grade_207, logic_uScript_IsCoreEncounterCompleted_encounterName_207);
		bool num = logic_uScript_IsCoreEncounterCompleted_uScript_IsCoreEncounterCompleted_207.True;
		bool flag = logic_uScript_IsCoreEncounterCompleted_uScript_IsCoreEncounterCompleted_207.False;
		if (num)
		{
			Relay_UnPause_100();
		}
		if (flag)
		{
			Relay_In_210();
		}
	}

	private void Relay_In_210()
	{
		logic_uScript_AddMessage_messageData_210 = msg14FindOtherParts;
		logic_uScript_AddMessage_speaker_210 = messageSpeaker;
		logic_uScript_AddMessage_Return_210 = logic_uScript_AddMessage_uScript_AddMessage_210.In(logic_uScript_AddMessage_messageData_210, logic_uScript_AddMessage_speaker_210);
		if (logic_uScript_AddMessage_uScript_AddMessage_210.Out)
		{
			Relay_In_232();
		}
	}

	private void Relay_In_211()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_211.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_211.Out)
		{
			Relay_In_210();
		}
	}

	private void Relay_In_212()
	{
		logic_uScript_SetTutorialTechToBuild_completedTechPreset_212 = completedSolGenRegenPreset;
		logic_uScript_SetTutorialTechToBuild_tutorialBuildTech_212 = local_SolarGenTech_Tank;
		logic_uScript_SetTutorialTechToBuild_uScript_SetTutorialTechToBuild_212.In(logic_uScript_SetTutorialTechToBuild_completedTechPreset_212, logic_uScript_SetTutorialTechToBuild_tutorialBuildTech_212);
		if (logic_uScript_SetTutorialTechToBuild_uScript_SetTutorialTechToBuild_212.Out)
		{
			Relay_In_202();
		}
	}

	private void Relay_True_214()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_214.True(out logic_uScriptAct_SetBool_Target_214);
		local_SolGenRegenPresetSet_System_Boolean = logic_uScriptAct_SetBool_Target_214;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_214.Out)
		{
			Relay_In_212();
		}
	}

	private void Relay_False_214()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_214.False(out logic_uScriptAct_SetBool_Target_214);
		local_SolGenRegenPresetSet_System_Boolean = logic_uScriptAct_SetBool_Target_214;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_214.Out)
		{
			Relay_In_212();
		}
	}

	private void Relay_In_215()
	{
		logic_uScriptCon_CompareBool_Bool_215 = local_SolGenRegenPresetSet_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_215.In(logic_uScriptCon_CompareBool_Bool_215);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_215.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_215.False;
		if (num)
		{
			Relay_In_202();
		}
		if (flag)
		{
			Relay_True_214();
		}
	}

	private void Relay_True_218()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_218.True(out logic_uScriptAct_SetBool_Target_218);
		local_SolGenRegenPresetSet_System_Boolean = logic_uScriptAct_SetBool_Target_218;
	}

	private void Relay_False_218()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_218.False(out logic_uScriptAct_SetBool_Target_218);
		local_SolGenRegenPresetSet_System_Boolean = logic_uScriptAct_SetBool_Target_218;
	}

	private void Relay_In_221()
	{
		logic_uScript_SetTutorialTechToBuild_completedTechPreset_221 = completedSolGenRegenPreset;
		logic_uScript_SetTutorialTechToBuild_tutorialBuildTech_221 = local_SolarGenTech_Tank;
		logic_uScript_SetTutorialTechToBuild_uScript_SetTutorialTechToBuild_221.In(logic_uScript_SetTutorialTechToBuild_completedTechPreset_221, logic_uScript_SetTutorialTechToBuild_tutorialBuildTech_221);
		if (logic_uScript_SetTutorialTechToBuild_uScript_SetTutorialTechToBuild_221.Out)
		{
			Relay_In_47();
		}
	}

	private void Relay_True_223()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_223.True(out logic_uScriptAct_SetBool_Target_223);
		local_SolGenRegenPresetSet_System_Boolean = logic_uScriptAct_SetBool_Target_223;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_223.Out)
		{
			Relay_In_221();
		}
	}

	private void Relay_False_223()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_223.False(out logic_uScriptAct_SetBool_Target_223);
		local_SolGenRegenPresetSet_System_Boolean = logic_uScriptAct_SetBool_Target_223;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_223.Out)
		{
			Relay_In_221();
		}
	}

	private void Relay_In_225()
	{
		logic_uScriptCon_CompareBool_Bool_225 = local_SolGenRegenPresetSet_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_225.In(logic_uScriptCon_CompareBool_Bool_225);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_225.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_225.False;
		if (num)
		{
			Relay_In_47();
		}
		if (flag)
		{
			Relay_True_223();
		}
	}

	private void Relay_In_226()
	{
		logic_uScriptCon_CompareInt_A_226 = local_Stage_System_Int32;
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_226.In(logic_uScriptCon_CompareInt_A_226, logic_uScriptCon_CompareInt_B_226);
		bool equalTo = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_226.EqualTo;
		bool notEqualTo = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_226.NotEqualTo;
		if (equalTo)
		{
			Relay_In_103();
		}
		if (notEqualTo)
		{
			Relay_In_333();
		}
	}

	private void Relay_In_228()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_228.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_228.Out)
		{
			Relay_In_47();
		}
	}

	private void Relay_In_229()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_229.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_229.Out)
		{
			Relay_In_228();
		}
	}

	private void Relay_In_232()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_232.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_232.Out)
		{
			Relay_UnPause_100();
		}
	}

	private void Relay_In_235()
	{
		logic_uScript_HasHintBeenShownBefore_hintID_235 = local_237_GameHints_HintID;
		logic_uScript_HasHintBeenShownBefore_uScript_HasHintBeenShownBefore_235.In(logic_uScript_HasHintBeenShownBefore_hintID_235);
		bool shown = logic_uScript_HasHintBeenShownBefore_uScript_HasHintBeenShownBefore_235.Shown;
		bool notShown = logic_uScript_HasHintBeenShownBefore_uScript_HasHintBeenShownBefore_235.NotShown;
		if (shown)
		{
			Relay_In_239();
		}
		if (notShown)
		{
			Relay_In_236();
		}
	}

	private void Relay_In_236()
	{
		logic_uScript_ShowHint_hintId_236 = local_237_GameHints_HintID;
		logic_uScript_ShowHint_uScript_ShowHint_236.In(logic_uScript_ShowHint_hintId_236);
		if (logic_uScript_ShowHint_uScript_ShowHint_236.Out)
		{
			Relay_In_239();
		}
	}

	private void Relay_In_238()
	{
		logic_uScript_ShowHint_hintId_238 = local_240_GameHints_HintID;
		logic_uScript_ShowHint_uScript_ShowHint_238.In(logic_uScript_ShowHint_hintId_238);
	}

	private void Relay_In_239()
	{
		logic_uScript_HasHintBeenShownBefore_hintID_239 = local_240_GameHints_HintID;
		logic_uScript_HasHintBeenShownBefore_uScript_HasHintBeenShownBefore_239.In(logic_uScript_HasHintBeenShownBefore_hintID_239);
		if (logic_uScript_HasHintBeenShownBefore_uScript_HasHintBeenShownBefore_239.NotShown)
		{
			Relay_In_238();
		}
	}

	private void Relay_In_242()
	{
		logic_uScript_Wait_uScript_Wait_242.In(logic_uScript_Wait_seconds_242, logic_uScript_Wait_repeat_242);
		if (logic_uScript_Wait_uScript_Wait_242.Waited)
		{
			Relay_In_235();
		}
	}

	private void Relay_In_243()
	{
		logic_uScript_IsCoreEncounterCompleted_uScript_IsCoreEncounterCompleted_243.In(logic_uScript_IsCoreEncounterCompleted_corp_243, logic_uScript_IsCoreEncounterCompleted_grade_243, logic_uScript_IsCoreEncounterCompleted_encounterName_243);
		if (logic_uScript_IsCoreEncounterCompleted_uScript_IsCoreEncounterCompleted_243.True)
		{
			Relay_In_242();
		}
	}

	private void Relay_In_244()
	{
		logic_uScript_HideHint_uScript_HideHint_244.In(logic_uScript_HideHint_hintId_244);
		if (logic_uScript_HideHint_uScript_HideHint_244.Out)
		{
			Relay_In_0();
		}
	}

	private void Relay_In_245()
	{
		logic_uScript_IsHUDElementVisible_uScript_IsHUDElementVisible_245.In(logic_uScript_IsHUDElementVisible_hudElement_245);
		bool num = logic_uScript_IsHUDElementVisible_uScript_IsHUDElementVisible_245.True;
		bool flag = logic_uScript_IsHUDElementVisible_uScript_IsHUDElementVisible_245.False;
		if (num)
		{
			Relay_In_246();
		}
		if (flag)
		{
			Relay_In_226();
		}
	}

	private void Relay_In_246()
	{
		logic_uScript_HideHint_uScript_HideHint_246.In(logic_uScript_HideHint_hintId_246);
		if (logic_uScript_HideHint_uScript_HideHint_246.Out)
		{
			Relay_In_226();
		}
	}

	private void Relay_In_248()
	{
		logic_uScript_IsShieldBlockPowered_shieldBlock_248 = local_RepairBubbleBlock_TankBlock;
		logic_uScript_IsShieldBlockPowered_uScript_IsShieldBlockPowered_248.In(logic_uScript_IsShieldBlockPowered_shieldBlock_248);
		if (logic_uScript_IsShieldBlockPowered_uScript_IsShieldBlockPowered_248.True)
		{
			Relay_In_179();
		}
	}

	private void Relay_Out_249()
	{
		Relay_In_285();
	}

	private void Relay_Shown_249()
	{
	}

	private void Relay_In_249()
	{
		logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboard_249 = msg07AttachRepairBubble;
		logic_SubGraph_AddMessageWithPadSupport_messageControlPad_249 = msg07AttachRepairBubble_Pad;
		logic_SubGraph_AddMessageWithPadSupport_speaker_249 = messageSpeaker;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_249.In(logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboard_249, logic_SubGraph_AddMessageWithPadSupport_messageControlPad_249, logic_SubGraph_AddMessageWithPadSupport_speaker_249);
	}

	private void Relay_In_253()
	{
		logic_uScript_GetJoypadControlMode_uScript_GetJoypadControlMode_253.In();
		bool joypad = logic_uScript_GetJoypadControlMode_uScript_GetJoypadControlMode_253.Joypad;
		bool mouseAndKeyboard = logic_uScript_GetJoypadControlMode_uScript_GetJoypadControlMode_253.MouseAndKeyboard;
		if (joypad)
		{
			Relay_In_257();
		}
		if (mouseAndKeyboard)
		{
			Relay_In_264();
		}
	}

	private void Relay_In_254()
	{
		logic_uScript_GetJoypadControlMode_uScript_GetJoypadControlMode_254.In();
		bool joypad = logic_uScript_GetJoypadControlMode_uScript_GetJoypadControlMode_254.Joypad;
		bool mouseAndKeyboard = logic_uScript_GetJoypadControlMode_uScript_GetJoypadControlMode_254.MouseAndKeyboard;
		if (joypad)
		{
			Relay_In_260();
		}
		if (mouseAndKeyboard)
		{
			Relay_In_263();
		}
	}

	private void Relay_In_257()
	{
		logic_uScript_AddMessage_messageData_257 = msg03AnchorSolarGenerator_Pad1;
		logic_uScript_AddMessage_speaker_257 = messageSpeaker;
		logic_uScript_AddMessage_Return_257 = logic_uScript_AddMessage_uScript_AddMessage_257.In(logic_uScript_AddMessage_messageData_257, logic_uScript_AddMessage_speaker_257);
		local_Msg03AnchorSolarGenerator_Pad1_ManOnScreenMessages_OnScreenMessage = logic_uScript_AddMessage_Return_257;
		if (logic_uScript_AddMessage_uScript_AddMessage_257.Out)
		{
			Relay_In_277();
		}
	}

	private void Relay_In_260()
	{
		logic_uScript_AddMessage_messageData_260 = msg03AnchorSolarGenerator_Pad2;
		logic_uScript_AddMessage_speaker_260 = messageSpeaker;
		logic_uScript_AddMessage_Return_260 = logic_uScript_AddMessage_uScript_AddMessage_260.In(logic_uScript_AddMessage_messageData_260, logic_uScript_AddMessage_speaker_260);
		local_Msg03AnchorSolarGenerator_Pad2_ManOnScreenMessages_OnScreenMessage = logic_uScript_AddMessage_Return_260;
		if (logic_uScript_AddMessage_uScript_AddMessage_260.Out)
		{
			Relay_In_281();
		}
	}

	private void Relay_In_263()
	{
		logic_uScript_AddMessage_messageData_263 = msg03AnchorSolarGenerator;
		logic_uScript_AddMessage_speaker_263 = messageSpeaker;
		logic_uScript_AddMessage_Return_263 = logic_uScript_AddMessage_uScript_AddMessage_263.In(logic_uScript_AddMessage_messageData_263, logic_uScript_AddMessage_speaker_263);
	}

	private void Relay_In_264()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_264.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_264.Out)
		{
			Relay_In_263();
		}
	}

	private void Relay_In_268()
	{
		logic_uScript_RemoveOnScreenMessage_onScreenMessage_268 = local_Msg08ReturnRepairBubble_ManOnScreenMessages_OnScreenMessage;
		logic_uScript_RemoveOnScreenMessage_uScript_RemoveOnScreenMessage_268.In(logic_uScript_RemoveOnScreenMessage_onScreenMessage_268, logic_uScript_RemoveOnScreenMessage_instant_268);
		if (logic_uScript_RemoveOnScreenMessage_uScript_RemoveOnScreenMessage_268.Out)
		{
			Relay_In_271();
		}
	}

	private void Relay_In_271()
	{
		logic_uScript_RemoveOnScreenMessage_onScreenMessage_271 = local_Msg07AttachRepairBubble_ManOnScreenMessages_OnScreenMessage;
		logic_uScript_RemoveOnScreenMessage_uScript_RemoveOnScreenMessage_271.In(logic_uScript_RemoveOnScreenMessage_onScreenMessage_271, logic_uScript_RemoveOnScreenMessage_instant_271);
		if (logic_uScript_RemoveOnScreenMessage_uScript_RemoveOnScreenMessage_271.Out)
		{
			Relay_In_273();
		}
	}

	private void Relay_In_273()
	{
		logic_uScript_RemoveOnScreenMessage_onScreenMessage_273 = local_Msg07AttachRepairBubble_Pad_ManOnScreenMessages_OnScreenMessage;
		logic_uScript_RemoveOnScreenMessage_uScript_RemoveOnScreenMessage_273.In(logic_uScript_RemoveOnScreenMessage_onScreenMessage_273, logic_uScript_RemoveOnScreenMessage_instant_273);
	}

	private void Relay_In_277()
	{
		logic_uScript_RemoveOnScreenMessage_onScreenMessage_277 = local_Msg03AnchorSolarGenerator_Pad2_ManOnScreenMessages_OnScreenMessage;
		logic_uScript_RemoveOnScreenMessage_uScript_RemoveOnScreenMessage_277.In(logic_uScript_RemoveOnScreenMessage_onScreenMessage_277, logic_uScript_RemoveOnScreenMessage_instant_277);
	}

	private void Relay_In_281()
	{
		logic_uScript_RemoveOnScreenMessage_onScreenMessage_281 = local_Msg03AnchorSolarGenerator_Pad1_ManOnScreenMessages_OnScreenMessage;
		logic_uScript_RemoveOnScreenMessage_uScript_RemoveOnScreenMessage_281.In(logic_uScript_RemoveOnScreenMessage_onScreenMessage_281, logic_uScript_RemoveOnScreenMessage_instant_281);
	}

	private void Relay_In_285()
	{
		logic_uScript_RemoveOnScreenMessage_onScreenMessage_285 = local_Msg05RepairBubbleSpawned_ManOnScreenMessages_OnScreenMessage;
		logic_uScript_RemoveOnScreenMessage_uScript_RemoveOnScreenMessage_285.In(logic_uScript_RemoveOnScreenMessage_onScreenMessage_285, logic_uScript_RemoveOnScreenMessage_instant_285);
		if (logic_uScript_RemoveOnScreenMessage_uScript_RemoveOnScreenMessage_285.Out)
		{
			Relay_In_298();
		}
	}

	private void Relay_In_288()
	{
		logic_uScript_RemoveOnScreenMessage_onScreenMessage_288 = local_Msg07AttachRepairBubble_Pad_ManOnScreenMessages_OnScreenMessage;
		logic_uScript_RemoveOnScreenMessage_uScript_RemoveOnScreenMessage_288.In(logic_uScript_RemoveOnScreenMessage_onScreenMessage_288, logic_uScript_RemoveOnScreenMessage_instant_288);
	}

	private void Relay_In_290()
	{
		logic_uScript_RemoveOnScreenMessage_onScreenMessage_290 = local_Msg07AttachRepairBubble_ManOnScreenMessages_OnScreenMessage;
		logic_uScript_RemoveOnScreenMessage_uScript_RemoveOnScreenMessage_290.In(logic_uScript_RemoveOnScreenMessage_onScreenMessage_290, logic_uScript_RemoveOnScreenMessage_instant_290);
		if (logic_uScript_RemoveOnScreenMessage_uScript_RemoveOnScreenMessage_290.Out)
		{
			Relay_In_288();
		}
	}

	private void Relay_True_293()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_293.True(out logic_uScriptAct_SetBool_Target_293);
		local_RegenSpawnedMsgShown_System_Boolean = logic_uScriptAct_SetBool_Target_293;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_293.Out)
		{
			Relay_In_45();
		}
	}

	private void Relay_False_293()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_293.False(out logic_uScriptAct_SetBool_Target_293);
		local_RegenSpawnedMsgShown_System_Boolean = logic_uScriptAct_SetBool_Target_293;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_293.Out)
		{
			Relay_In_45();
		}
	}

	private void Relay_In_295()
	{
		logic_uScript_RemoveOnScreenMessage_onScreenMessage_295 = local_Msg06PickUpRepairBubble_ManOnScreenMessages_OnScreenMessage;
		logic_uScript_RemoveOnScreenMessage_uScript_RemoveOnScreenMessage_295.In(logic_uScript_RemoveOnScreenMessage_onScreenMessage_295, logic_uScript_RemoveOnScreenMessage_instant_295);
		if (logic_uScript_RemoveOnScreenMessage_uScript_RemoveOnScreenMessage_295.Out)
		{
			Relay_In_290();
		}
	}

	private void Relay_In_298()
	{
		logic_uScript_RemoveOnScreenMessage_onScreenMessage_298 = local_Msg06PickUpRepairBubble_ManOnScreenMessages_OnScreenMessage;
		logic_uScript_RemoveOnScreenMessage_uScript_RemoveOnScreenMessage_298.In(logic_uScript_RemoveOnScreenMessage_onScreenMessage_298, logic_uScript_RemoveOnScreenMessage_instant_298);
	}

	private void Relay_In_299()
	{
		logic_uScript_EnableGlow_targetObject_299 = local_Solar_Generator_Block_TankBlock;
		logic_uScript_EnableGlow_uScript_EnableGlow_299.In(logic_uScript_EnableGlow_targetObject_299, logic_uScript_EnableGlow_enable_299);
		if (logic_uScript_EnableGlow_uScript_EnableGlow_299.Out)
		{
			Relay_In_132();
		}
	}

	private void Relay_In_300()
	{
		logic_uScript_EnableGlow_targetObject_300 = local_RepairBubbleBlock_TankBlock;
		logic_uScript_EnableGlow_uScript_EnableGlow_300.In(logic_uScript_EnableGlow_targetObject_300, logic_uScript_EnableGlow_enable_300);
		if (logic_uScript_EnableGlow_uScript_EnableGlow_300.Out)
		{
			Relay_In_302();
		}
	}

	private void Relay_In_302()
	{
		logic_uScriptCon_CompareBool_Bool_302 = local_GhostBlockRegenSpawned_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_302.In(logic_uScriptCon_CompareBool_Bool_302);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_302.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_302.False;
		if (num)
		{
			Relay_In_304();
		}
		if (flag)
		{
			Relay_In_306();
		}
	}

	private void Relay_In_304()
	{
		logic_uScript_EnableGlow_targetObject_304 = local_GhostBlockRegen_TankBlock;
		logic_uScript_EnableGlow_uScript_EnableGlow_304.In(logic_uScript_EnableGlow_targetObject_304, logic_uScript_EnableGlow_enable_304);
		if (logic_uScript_EnableGlow_uScript_EnableGlow_304.Out)
		{
			Relay_In_95();
		}
	}

	private void Relay_In_306()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_306.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_306.Out)
		{
			Relay_In_95();
		}
	}

	private void Relay_In_308()
	{
		logic_uScript_EnableGlow_targetObject_308 = local_RepairBubbleBlock_TankBlock;
		logic_uScript_EnableGlow_uScript_EnableGlow_308.In(logic_uScript_EnableGlow_targetObject_308, logic_uScript_EnableGlow_enable_308);
		if (logic_uScript_EnableGlow_uScript_EnableGlow_308.Out)
		{
			Relay_In_310();
		}
	}

	private void Relay_In_310()
	{
		logic_uScript_EnableGlow_targetObject_310 = local_GhostBlockRegen_TankBlock;
		logic_uScript_EnableGlow_uScript_EnableGlow_310.In(logic_uScript_EnableGlow_targetObject_310, logic_uScript_EnableGlow_enable_310);
		if (logic_uScript_EnableGlow_uScript_EnableGlow_310.Out)
		{
			Relay_True_293();
		}
	}

	private void Relay_In_313()
	{
		logic_uScript_EnableGlow_targetObject_313 = local_Solar_Generator_Block_TankBlock;
		logic_uScript_EnableGlow_uScript_EnableGlow_313.In(logic_uScript_EnableGlow_targetObject_313, logic_uScript_EnableGlow_enable_313);
		if (logic_uScript_EnableGlow_uScript_EnableGlow_313.Out)
		{
			Relay_In_215();
		}
	}

	private void Relay_In_315()
	{
		logic_uScript_GetJoypadControlMode_uScript_GetJoypadControlMode_315.In();
		bool joypad = logic_uScript_GetJoypadControlMode_uScript_GetJoypadControlMode_315.Joypad;
		bool mouseAndKeyboard = logic_uScript_GetJoypadControlMode_uScript_GetJoypadControlMode_315.MouseAndKeyboard;
		if (joypad)
		{
			Relay_In_355();
		}
		if (mouseAndKeyboard)
		{
			Relay_In_347();
		}
	}

	private void Relay_In_318()
	{
		logic_uScript_AddMessage_messageData_318 = msg13UnanchorSolarGenerator_Pad1;
		logic_uScript_AddMessage_speaker_318 = messageSpeaker;
		logic_uScript_AddMessage_Return_318 = logic_uScript_AddMessage_uScript_AddMessage_318.In(logic_uScript_AddMessage_messageData_318, logic_uScript_AddMessage_speaker_318);
		local_Msg13UnanchorSolarGenerator_Pad1_ManOnScreenMessages_OnScreenMessage = logic_uScript_AddMessage_Return_318;
	}

	private void Relay_In_319()
	{
		logic_uScript_IsHUDElementVisible_uScript_IsHUDElementVisible_319.In(logic_uScript_IsHUDElementVisible_hudElement_319);
		bool num = logic_uScript_IsHUDElementVisible_uScript_IsHUDElementVisible_319.True;
		bool flag = logic_uScript_IsHUDElementVisible_uScript_IsHUDElementVisible_319.False;
		if (num)
		{
			Relay_In_371();
		}
		if (flag)
		{
			Relay_In_372();
		}
	}

	private void Relay_In_320()
	{
		logic_uScript_AddMessage_messageData_320 = msg13UnanchorSolarGenerator_Pad2;
		logic_uScript_AddMessage_speaker_320 = messageSpeaker;
		logic_uScript_AddMessage_Return_320 = logic_uScript_AddMessage_uScript_AddMessage_320.In(logic_uScript_AddMessage_messageData_320, logic_uScript_AddMessage_speaker_320);
		local_Msg13UnanchorSolarGenerator_Pad2_ManOnScreenMessages_OnScreenMessage = logic_uScript_AddMessage_Return_320;
	}

	private void Relay_In_324()
	{
		logic_uScript_IsPlayerInteractingWithBlock_block_324 = local_Solar_Generator_Block_TankBlock;
		logic_uScript_IsPlayerInteractingWithBlock_uScript_IsPlayerInteractingWithBlock_324.In(logic_uScript_IsPlayerInteractingWithBlock_block_324);
		bool dragging = logic_uScript_IsPlayerInteractingWithBlock_uScript_IsPlayerInteractingWithBlock_324.Dragging;
		bool notDragging = logic_uScript_IsPlayerInteractingWithBlock_uScript_IsPlayerInteractingWithBlock_324.NotDragging;
		if (dragging)
		{
			Relay_In_137();
		}
		if (notDragging)
		{
			Relay_In_138();
		}
	}

	private void Relay_In_325()
	{
		logic_uScript_IsPlayerInteractingWithBlock_block_325 = local_Solar_Generator_Block_TankBlock;
		logic_uScript_IsPlayerInteractingWithBlock_uScript_IsPlayerInteractingWithBlock_325.In(logic_uScript_IsPlayerInteractingWithBlock_block_325);
		bool dragging = logic_uScript_IsPlayerInteractingWithBlock_uScript_IsPlayerInteractingWithBlock_325.Dragging;
		bool notDragging = logic_uScript_IsPlayerInteractingWithBlock_uScript_IsPlayerInteractingWithBlock_325.NotDragging;
		if (dragging)
		{
			Relay_In_357();
		}
		if (notDragging)
		{
			Relay_In_349();
		}
	}

	private void Relay_Out_328()
	{
	}

	private void Relay_In_328()
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_328 = local_Stage_System_Int32;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_328.In(ref logic_SubGraph_CompleteObjectiveStage_objectiveStage_328, logic_SubGraph_CompleteObjectiveStage_isFinalObjective_328);
	}

	private void Relay_In_331()
	{
		logic_uScript_AddMessage_messageData_331 = msg11PlayerHealed;
		logic_uScript_AddMessage_speaker_331 = messageSpeaker;
		logic_uScript_AddMessage_Return_331 = logic_uScript_AddMessage_uScript_AddMessage_331.In(logic_uScript_AddMessage_messageData_331, logic_uScript_AddMessage_speaker_331);
		if (logic_uScript_AddMessage_uScript_AddMessage_331.Shown)
		{
			Relay_In_336();
		}
	}

	private void Relay_In_333()
	{
		logic_uScriptCon_CompareInt_A_333 = local_Stage_System_Int32;
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_333.In(logic_uScriptCon_CompareInt_A_333, logic_uScriptCon_CompareInt_B_333);
		bool equalTo = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_333.EqualTo;
		bool notEqualTo = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_333.NotEqualTo;
		if (equalTo)
		{
			Relay_In_103();
		}
		if (notEqualTo)
		{
			Relay_In_229();
		}
	}

	private void Relay_In_336()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_336 = messageTag;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_336.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_336, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_336);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_336.Out)
		{
			Relay_In_328();
		}
	}

	private void Relay_In_337()
	{
		logic_uScript_AddMessage_messageData_337 = msg13UnanchorSolarGenerator;
		logic_uScript_AddMessage_speaker_337 = messageSpeaker;
		logic_uScript_AddMessage_Return_337 = logic_uScript_AddMessage_uScript_AddMessage_337.In(logic_uScript_AddMessage_messageData_337, logic_uScript_AddMessage_speaker_337);
		local_Msg13UnanchorSolarGenerator_ManOnScreenMessages_OnScreenMessage = logic_uScript_AddMessage_Return_337;
	}

	private void Relay_In_342()
	{
		logic_uScript_AddMessage_messageData_342 = msg12DismantleBase;
		logic_uScript_AddMessage_speaker_342 = messageSpeaker;
		logic_uScript_AddMessage_Return_342 = logic_uScript_AddMessage_uScript_AddMessage_342.In(logic_uScript_AddMessage_messageData_342, logic_uScript_AddMessage_speaker_342);
		if (logic_uScript_AddMessage_uScript_AddMessage_342.Shown)
		{
			Relay_True_344();
		}
	}

	private void Relay_In_343()
	{
		logic_uScriptCon_CompareBool_Bool_343 = local_DismantleBaseMsgShown_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_343.In(logic_uScriptCon_CompareBool_Bool_343);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_343.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_343.False;
		if (num)
		{
			Relay_In_315();
		}
		if (flag)
		{
			Relay_In_342();
		}
	}

	private void Relay_True_344()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_344.True(out logic_uScriptAct_SetBool_Target_344);
		local_DismantleBaseMsgShown_System_Boolean = logic_uScriptAct_SetBool_Target_344;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_344.Out)
		{
			Relay_In_315();
		}
	}

	private void Relay_False_344()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_344.False(out logic_uScriptAct_SetBool_Target_344);
		local_DismantleBaseMsgShown_System_Boolean = logic_uScriptAct_SetBool_Target_344;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_344.Out)
		{
			Relay_In_315();
		}
	}

	private void Relay_In_347()
	{
		logic_uScript_DoesPlayerTankHaveBlock_uScript_DoesPlayerTankHaveBlock_347.In(logic_uScript_DoesPlayerTankHaveBlock_blockType_347, logic_uScript_DoesPlayerTankHaveBlock_amount_347);
		bool num = logic_uScript_DoesPlayerTankHaveBlock_uScript_DoesPlayerTankHaveBlock_347.True;
		bool flag = logic_uScript_DoesPlayerTankHaveBlock_uScript_DoesPlayerTankHaveBlock_347.False;
		if (num)
		{
			Relay_In_348();
		}
		if (flag)
		{
			Relay_In_400();
		}
	}

	private void Relay_In_348()
	{
		logic_uScript_DoesPlayerTankHaveBlock_uScript_DoesPlayerTankHaveBlock_348.In(logic_uScript_DoesPlayerTankHaveBlock_blockType_348, logic_uScript_DoesPlayerTankHaveBlock_amount_348);
		bool num = logic_uScript_DoesPlayerTankHaveBlock_uScript_DoesPlayerTankHaveBlock_348.True;
		bool flag = logic_uScript_DoesPlayerTankHaveBlock_uScript_DoesPlayerTankHaveBlock_348.False;
		if (num)
		{
			Relay_In_381();
		}
		if (flag)
		{
			Relay_In_396();
		}
	}

	private void Relay_In_349()
	{
		logic_uScript_IsPlayerInteractingWithBlock_block_349 = local_RepairBubbleBlock_TankBlock;
		logic_uScript_IsPlayerInteractingWithBlock_uScript_IsPlayerInteractingWithBlock_349.In(logic_uScript_IsPlayerInteractingWithBlock_block_349);
		bool dragging = logic_uScript_IsPlayerInteractingWithBlock_uScript_IsPlayerInteractingWithBlock_349.Dragging;
		bool notDragging = logic_uScript_IsPlayerInteractingWithBlock_uScript_IsPlayerInteractingWithBlock_349.NotDragging;
		if (dragging)
		{
			Relay_In_364();
		}
		if (notDragging)
		{
			Relay_In_319();
		}
	}

	private void Relay_In_353()
	{
		logic_uScript_AddMessage_messageData_353 = msg13UnanchorSolarGenerator_Pad3;
		logic_uScript_AddMessage_speaker_353 = messageSpeaker;
		logic_uScript_AddMessage_Return_353 = logic_uScript_AddMessage_uScript_AddMessage_353.In(logic_uScript_AddMessage_messageData_353, logic_uScript_AddMessage_speaker_353);
		local_Msg13UnanchorSolarGenerator_Pad3_ManOnScreenMessages_OnScreenMessage = logic_uScript_AddMessage_Return_353;
	}

	private void Relay_In_354()
	{
		logic_uScript_DoesPlayerTankHaveBlock_uScript_DoesPlayerTankHaveBlock_354.In(logic_uScript_DoesPlayerTankHaveBlock_blockType_354, logic_uScript_DoesPlayerTankHaveBlock_amount_354);
		bool num = logic_uScript_DoesPlayerTankHaveBlock_uScript_DoesPlayerTankHaveBlock_354.True;
		bool flag = logic_uScript_DoesPlayerTankHaveBlock_uScript_DoesPlayerTankHaveBlock_354.False;
		if (num)
		{
			Relay_In_356();
		}
		if (flag)
		{
			Relay_In_405();
		}
	}

	private void Relay_In_355()
	{
		logic_uScript_DoesPlayerTankHaveBlock_uScript_DoesPlayerTankHaveBlock_355.In(logic_uScript_DoesPlayerTankHaveBlock_blockType_355, logic_uScript_DoesPlayerTankHaveBlock_amount_355);
		bool num = logic_uScript_DoesPlayerTankHaveBlock_uScript_DoesPlayerTankHaveBlock_355.True;
		bool flag = logic_uScript_DoesPlayerTankHaveBlock_uScript_DoesPlayerTankHaveBlock_355.False;
		if (num)
		{
			Relay_In_356();
		}
		if (flag)
		{
			Relay_In_354();
		}
	}

	private void Relay_In_356()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_356.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_356.Out)
		{
			Relay_In_379();
		}
	}

	private void Relay_In_357()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_357.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_357.Out)
		{
			Relay_In_364();
		}
	}

	private void Relay_In_358()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_358.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_358.Out)
		{
			Relay_In_337();
		}
	}

	private void Relay_In_360()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_360 = messageTag;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_360.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_360, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_360);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_360.Out)
		{
			Relay_True_384();
		}
	}

	private void Relay_In_364()
	{
		logic_uScript_RemoveOnScreenMessage_onScreenMessage_364 = local_Msg13UnanchorSolarGenerator_Pad1_ManOnScreenMessages_OnScreenMessage;
		logic_uScript_RemoveOnScreenMessage_uScript_RemoveOnScreenMessage_364.In(logic_uScript_RemoveOnScreenMessage_onScreenMessage_364, logic_uScript_RemoveOnScreenMessage_instant_364);
		if (logic_uScript_RemoveOnScreenMessage_uScript_RemoveOnScreenMessage_364.Out)
		{
			Relay_In_367();
		}
	}

	private void Relay_In_367()
	{
		logic_uScript_RemoveOnScreenMessage_onScreenMessage_367 = local_Msg13UnanchorSolarGenerator_Pad2_ManOnScreenMessages_OnScreenMessage;
		logic_uScript_RemoveOnScreenMessage_uScript_RemoveOnScreenMessage_367.In(logic_uScript_RemoveOnScreenMessage_onScreenMessage_367, logic_uScript_RemoveOnScreenMessage_instant_367);
		if (logic_uScript_RemoveOnScreenMessage_uScript_RemoveOnScreenMessage_367.Out)
		{
			Relay_In_353();
		}
	}

	private void Relay_In_369()
	{
		logic_uScript_RemoveOnScreenMessage_onScreenMessage_369 = local_Msg13UnanchorSolarGenerator_Pad3_ManOnScreenMessages_OnScreenMessage;
		logic_uScript_RemoveOnScreenMessage_uScript_RemoveOnScreenMessage_369.In(logic_uScript_RemoveOnScreenMessage_onScreenMessage_369, logic_uScript_RemoveOnScreenMessage_instant_369);
		if (logic_uScript_RemoveOnScreenMessage_uScript_RemoveOnScreenMessage_369.Out)
		{
			Relay_In_320();
		}
	}

	private void Relay_In_371()
	{
		logic_uScript_RemoveOnScreenMessage_onScreenMessage_371 = local_Msg13UnanchorSolarGenerator_Pad1_ManOnScreenMessages_OnScreenMessage;
		logic_uScript_RemoveOnScreenMessage_uScript_RemoveOnScreenMessage_371.In(logic_uScript_RemoveOnScreenMessage_onScreenMessage_371, logic_uScript_RemoveOnScreenMessage_instant_371);
		if (logic_uScript_RemoveOnScreenMessage_uScript_RemoveOnScreenMessage_371.Out)
		{
			Relay_In_369();
		}
	}

	private void Relay_In_372()
	{
		logic_uScript_RemoveOnScreenMessage_onScreenMessage_372 = local_Msg13UnanchorSolarGenerator_Pad2_ManOnScreenMessages_OnScreenMessage;
		logic_uScript_RemoveOnScreenMessage_uScript_RemoveOnScreenMessage_372.In(logic_uScript_RemoveOnScreenMessage_onScreenMessage_372, logic_uScript_RemoveOnScreenMessage_instant_372);
		if (logic_uScript_RemoveOnScreenMessage_uScript_RemoveOnScreenMessage_372.Out)
		{
			Relay_In_374();
		}
	}

	private void Relay_In_374()
	{
		logic_uScript_RemoveOnScreenMessage_onScreenMessage_374 = local_Msg13UnanchorSolarGenerator_Pad3_ManOnScreenMessages_OnScreenMessage;
		logic_uScript_RemoveOnScreenMessage_uScript_RemoveOnScreenMessage_374.In(logic_uScript_RemoveOnScreenMessage_onScreenMessage_374, logic_uScript_RemoveOnScreenMessage_instant_374);
		if (logic_uScript_RemoveOnScreenMessage_uScript_RemoveOnScreenMessage_374.Out)
		{
			Relay_In_318();
		}
	}

	private void Relay_In_379()
	{
		logic_uScript_RemoveOnScreenMessage_onScreenMessage_379 = local_Msg13UnanchorSolarGenerator_Pad3_ManOnScreenMessages_OnScreenMessage;
		logic_uScript_RemoveOnScreenMessage_uScript_RemoveOnScreenMessage_379.In(logic_uScript_RemoveOnScreenMessage_onScreenMessage_379, logic_uScript_RemoveOnScreenMessage_instant_379);
		if (logic_uScript_RemoveOnScreenMessage_uScript_RemoveOnScreenMessage_379.Out)
		{
			Relay_In_360();
		}
	}

	private void Relay_In_381()
	{
		logic_uScript_RemoveOnScreenMessage_onScreenMessage_381 = local_Msg13UnanchorSolarGenerator_ManOnScreenMessages_OnScreenMessage;
		logic_uScript_RemoveOnScreenMessage_uScript_RemoveOnScreenMessage_381.In(logic_uScript_RemoveOnScreenMessage_onScreenMessage_381, logic_uScript_RemoveOnScreenMessage_instant_381);
		if (logic_uScript_RemoveOnScreenMessage_uScript_RemoveOnScreenMessage_381.Out)
		{
			Relay_In_360();
		}
	}

	private void Relay_In_382()
	{
		logic_uScriptCon_CompareBool_Bool_382 = local_MissionComplete_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_382.In(logic_uScriptCon_CompareBool_Bool_382);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_382.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_382.False;
		if (num)
		{
			Relay_In_388();
		}
		if (flag)
		{
			Relay_In_343();
		}
	}

	private void Relay_True_384()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_384.True(out logic_uScriptAct_SetBool_Target_384);
		local_MissionComplete_System_Boolean = logic_uScriptAct_SetBool_Target_384;
	}

	private void Relay_False_384()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_384.False(out logic_uScriptAct_SetBool_Target_384);
		local_MissionComplete_System_Boolean = logic_uScriptAct_SetBool_Target_384;
	}

	private void Relay_Save_Out_387()
	{
		Relay_Save_172();
	}

	private void Relay_Load_Out_387()
	{
		Relay_Load_172();
	}

	private void Relay_Restart_Out_387()
	{
		Relay_Set_False_172();
	}

	private void Relay_Save_387()
	{
		logic_SubGraph_SaveLoadBool_boolean_387 = local_DismantleBaseMsgShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_387 = local_DismantleBaseMsgShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_387.Save(ref logic_SubGraph_SaveLoadBool_boolean_387, logic_SubGraph_SaveLoadBool_boolAsVariable_387, logic_SubGraph_SaveLoadBool_uniqueID_387);
	}

	private void Relay_Load_387()
	{
		logic_SubGraph_SaveLoadBool_boolean_387 = local_DismantleBaseMsgShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_387 = local_DismantleBaseMsgShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_387.Load(ref logic_SubGraph_SaveLoadBool_boolean_387, logic_SubGraph_SaveLoadBool_boolAsVariable_387, logic_SubGraph_SaveLoadBool_uniqueID_387);
	}

	private void Relay_Set_True_387()
	{
		logic_SubGraph_SaveLoadBool_boolean_387 = local_DismantleBaseMsgShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_387 = local_DismantleBaseMsgShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_387.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_387, logic_SubGraph_SaveLoadBool_boolAsVariable_387, logic_SubGraph_SaveLoadBool_uniqueID_387);
	}

	private void Relay_Set_False_387()
	{
		logic_SubGraph_SaveLoadBool_boolean_387 = local_DismantleBaseMsgShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_387 = local_DismantleBaseMsgShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_387.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_387, logic_SubGraph_SaveLoadBool_boolAsVariable_387, logic_SubGraph_SaveLoadBool_uniqueID_387);
	}

	private void Relay_In_388()
	{
		logic_uScript_Wait_uScript_Wait_388.In(logic_uScript_Wait_seconds_388, logic_uScript_Wait_repeat_388);
		if (logic_uScript_Wait_uScript_Wait_388.Waited)
		{
			Relay_In_206();
		}
	}

	private void Relay_In_389()
	{
		logic_uScriptCon_CompareBool_Bool_389 = local_PlayerHealed_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_389.In(logic_uScriptCon_CompareBool_Bool_389);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_389.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_389.False;
		if (num)
		{
			Relay_In_331();
		}
		if (flag)
		{
			Relay_In_39();
		}
	}

	private void Relay_True_392()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_392.True(out logic_uScriptAct_SetBool_Target_392);
		local_PlayerHealed_System_Boolean = logic_uScriptAct_SetBool_Target_392;
	}

	private void Relay_False_392()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_392.False(out logic_uScriptAct_SetBool_Target_392);
		local_PlayerHealed_System_Boolean = logic_uScriptAct_SetBool_Target_392;
	}

	private void Relay_Save_Out_393()
	{
		Relay_Save_387();
	}

	private void Relay_Load_Out_393()
	{
		Relay_Load_387();
	}

	private void Relay_Restart_Out_393()
	{
		Relay_Set_False_387();
	}

	private void Relay_Save_393()
	{
		logic_SubGraph_SaveLoadBool_boolean_393 = local_PlayerHealed_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_393 = local_PlayerHealed_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_393.Save(ref logic_SubGraph_SaveLoadBool_boolean_393, logic_SubGraph_SaveLoadBool_boolAsVariable_393, logic_SubGraph_SaveLoadBool_uniqueID_393);
	}

	private void Relay_Load_393()
	{
		logic_SubGraph_SaveLoadBool_boolean_393 = local_PlayerHealed_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_393 = local_PlayerHealed_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_393.Load(ref logic_SubGraph_SaveLoadBool_boolean_393, logic_SubGraph_SaveLoadBool_boolAsVariable_393, logic_SubGraph_SaveLoadBool_uniqueID_393);
	}

	private void Relay_Set_True_393()
	{
		logic_SubGraph_SaveLoadBool_boolean_393 = local_PlayerHealed_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_393 = local_PlayerHealed_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_393.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_393, logic_SubGraph_SaveLoadBool_boolAsVariable_393, logic_SubGraph_SaveLoadBool_uniqueID_393);
	}

	private void Relay_Set_False_393()
	{
		logic_SubGraph_SaveLoadBool_boolean_393 = local_PlayerHealed_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_393 = local_PlayerHealed_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_393.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_393, logic_SubGraph_SaveLoadBool_boolAsVariable_393, logic_SubGraph_SaveLoadBool_uniqueID_393);
	}

	private void Relay_In_396()
	{
		logic_uScript_MoveEncounterWithVisible_ownerNode_396 = owner_Connection_397;
		logic_uScript_MoveEncounterWithVisible_visibleObject_396 = local_RepairBubbleBlock_TankBlock;
		logic_uScript_MoveEncounterWithVisible_uScript_MoveEncounterWithVisible_396.In(logic_uScript_MoveEncounterWithVisible_ownerNode_396, logic_uScript_MoveEncounterWithVisible_visibleObject_396);
		if (logic_uScript_MoveEncounterWithVisible_uScript_MoveEncounterWithVisible_396.Out)
		{
			Relay_In_408();
		}
	}

	private void Relay_In_398()
	{
		logic_uScript_PlayerInRangeOfCurrentEncounter_ownerNode_398 = owner_Connection_399;
		logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_398.In(logic_uScript_PlayerInRangeOfCurrentEncounter_ownerNode_398);
		bool num = logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_398.True;
		bool flag = logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_398.False;
		if (num)
		{
			Relay_Pause_12();
		}
		if (flag)
		{
			Relay_In_62();
		}
	}

	private void Relay_In_400()
	{
		logic_uScript_MoveEncounterWithVisible_ownerNode_400 = owner_Connection_401;
		logic_uScript_MoveEncounterWithVisible_visibleObject_400 = local_Solar_Generator_Block_TankBlock;
		logic_uScript_MoveEncounterWithVisible_uScript_MoveEncounterWithVisible_400.In(logic_uScript_MoveEncounterWithVisible_ownerNode_400, logic_uScript_MoveEncounterWithVisible_visibleObject_400);
		if (logic_uScript_MoveEncounterWithVisible_uScript_MoveEncounterWithVisible_400.Out)
		{
			Relay_In_411();
		}
	}

	private void Relay_In_403()
	{
		logic_uScriptCon_CompareInt_A_403 = local_Stage_System_Int32;
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_403.In(logic_uScriptCon_CompareInt_A_403, logic_uScriptCon_CompareInt_B_403);
		bool equalTo = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_403.EqualTo;
		bool notEqualTo = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_403.NotEqualTo;
		if (equalTo)
		{
			Relay_In_398();
		}
		if (notEqualTo)
		{
			Relay_In_44();
		}
	}

	private void Relay_In_405()
	{
		logic_uScript_MoveEncounterWithVisible_ownerNode_405 = owner_Connection_406;
		logic_uScript_MoveEncounterWithVisible_visibleObject_405 = local_Solar_Generator_Block_TankBlock;
		logic_uScript_MoveEncounterWithVisible_uScript_MoveEncounterWithVisible_405.In(logic_uScript_MoveEncounterWithVisible_ownerNode_405, logic_uScript_MoveEncounterWithVisible_visibleObject_405);
		if (logic_uScript_MoveEncounterWithVisible_uScript_MoveEncounterWithVisible_405.Out)
		{
			Relay_In_415();
		}
	}

	private void Relay_In_408()
	{
		logic_uScript_PointArrowAtVisible_targetObject_408 = local_RepairBubbleBlock_TankBlock;
		logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_408.In(logic_uScript_PointArrowAtVisible_targetObject_408, logic_uScript_PointArrowAtVisible_timeToShowFor_408, logic_uScript_PointArrowAtVisible_offset_408);
		if (logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_408.Out)
		{
			Relay_In_337();
		}
	}

	private void Relay_In_411()
	{
		logic_uScript_PointArrowAtVisible_targetObject_411 = local_Solar_Generator_Block_TankBlock;
		logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_411.In(logic_uScript_PointArrowAtVisible_targetObject_411, logic_uScript_PointArrowAtVisible_timeToShowFor_411, logic_uScript_PointArrowAtVisible_offset_411);
		if (logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_411.Out)
		{
			Relay_In_358();
		}
	}

	private void Relay_In_413()
	{
		logic_uScript_PointArrowAtVisible_targetObject_413 = local_RepairBubbleBlock_TankBlock;
		logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_413.In(logic_uScript_PointArrowAtVisible_targetObject_413, logic_uScript_PointArrowAtVisible_timeToShowFor_413, logic_uScript_PointArrowAtVisible_offset_413);
	}

	private void Relay_In_415()
	{
		logic_uScript_PointArrowAtVisible_targetObject_415 = local_Solar_Generator_Block_TankBlock;
		logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_415.In(logic_uScript_PointArrowAtVisible_targetObject_415, logic_uScript_PointArrowAtVisible_timeToShowFor_415, logic_uScript_PointArrowAtVisible_offset_415);
		if (logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_415.Out)
		{
			Relay_In_325();
		}
	}

	private void Relay_In_416()
	{
		logic_uScript_SendAnaliticsEvent_uScript_SendAnaliticsEvent_416.In(logic_uScript_SendAnaliticsEvent_analiticsEvent_416, logic_uScript_SendAnaliticsEvent_parameterName_416, logic_uScript_SendAnaliticsEvent_parameter_416);
		if (logic_uScript_SendAnaliticsEvent_uScript_SendAnaliticsEvent_416.Out)
		{
			Relay_Succeed_165();
		}
	}
}
