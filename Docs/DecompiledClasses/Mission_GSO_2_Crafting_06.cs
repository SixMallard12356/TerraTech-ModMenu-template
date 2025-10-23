using System;
using BehaviorDesigner.Runtime;
using UnityEngine;

[Serializable]
[FriendlyName("Untitled", "")]
[NodePath("Graphs")]
public class Mission_GSO_2_Crafting_06 : uScriptLogic
{
	private GameObject parentGameObject;

	private uScript_GUI thisScriptsOnGuiListener;

	private bool m_RegisteredForEvents;

	[Multiline(3)]
	public string basePosition = "";

	public SpawnBlockData[] blockSpawnData = new SpawnBlockData[0];

	public SpawnBlockData[] blockSpawnDataAutoMiner = new SpawnBlockData[0];

	public float clearSceneryRadius;

	public float distBaseFound;

	private Tank local_209_Tank;

	private float local_215_System_Single;

	private TankBlock local_AutoMinerBlock_TankBlock;

	private Vector3 local_AutoMinerPos_UnityEngine_Vector3 = new Vector3(0f, 0f, 0f);

	private bool local_AutoMinerSpawned_System_Boolean;

	private Tank local_CraftingBaseTech_Tank;

	private bool local_MiningInProgress_System_Boolean;

	private bool local_msgAutoMinerSpawnedShown_System_Boolean;

	private bool local_msgBaseFoundShown_System_Boolean;

	private bool local_msgIntroShown_System_Boolean;

	private bool local_msgSeamUncoveredShown_System_Boolean;

	private bool local_msgUncoverSeamShown_System_Boolean;

	private bool local_NearBase_System_Boolean;

	private Tank local_NPCTech_Tank;

	private ResourceDispenser local_ResourceDispenser_ResourceDispenser;

	private string local_ResourceSeamID_System_String = "ResourceSeam";

	private Vector3 local_ResourceSeamPos_UnityEngine_Vector3 = new Vector3(0f, 0f, 0f);

	private bool local_ScenerySpawned_System_Boolean;

	private bool local_SeamUncoveredEarly_System_Boolean;

	private int local_Stage_System_Int32 = 1;

	public uScript_AddMessage.MessageSpeaker messageSpeaker;

	[Multiline(3)]
	public string messageTag = "";

	public uScript_AddMessage.MessageData msg01Intro;

	public uScript_AddMessage.MessageData msg02BaseFound;

	public uScript_AddMessage.MessageData msg03UncoverSeam;

	public uScript_AddMessage.MessageData msg04aSeamUncoveredEarly;

	public uScript_AddMessage.MessageData msg04SeamUncovered;

	public uScript_AddMessage.MessageData msg05AutoMinerSpawned;

	public uScript_AddMessage.MessageData msg06AnchorAutoMiner;

	public uScript_AddMessage.MessageData msg07Complete;

	public uScript_AddMessage.MessageData msgBlockOutsideArea;

	public uScript_AddMessage.MessageData msgLeavingMissionArea;

	public Transform NPCDespawnParticleEffect;

	public ExternalBehaviorTree NPCFlyAwayAI;

	public SpawnTechData[] NPCSpawnData = new SpawnTechData[0];

	public int numSceneryObjectsToSpawn;

	public TerrainObject resourceSeamPrefab;

	public TerrainObject sceneryPrefab;

	public float scenerySpawnDistanceMax;

	public float scenerySpawnDistanceMin;

	private GameObject owner_Connection_1;

	private GameObject owner_Connection_5;

	private GameObject owner_Connection_35;

	private GameObject owner_Connection_38;

	private GameObject owner_Connection_103;

	private GameObject owner_Connection_159;

	private GameObject owner_Connection_163;

	private GameObject owner_Connection_174;

	private GameObject owner_Connection_180;

	private GameObject owner_Connection_221;

	private uScript_IsPlayerInRangeOfTech logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_2 = new uScript_IsPlayerInRangeOfTech();

	private Tank logic_uScript_IsPlayerInRangeOfTech_tech_2;

	private float logic_uScript_IsPlayerInRangeOfTech_range_2;

	private Tank[] logic_uScript_IsPlayerInRangeOfTech_techs_2 = new Tank[0];

	private bool logic_uScript_IsPlayerInRangeOfTech_Out_2 = true;

	private bool logic_uScript_IsPlayerInRangeOfTech_InRange_2 = true;

	private bool logic_uScript_IsPlayerInRangeOfTech_OutOfRange_2 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_8 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_8;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_8 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_8 = "MiningInProgress";

	private uScript_LockTechStacks logic_uScript_LockTechStacks_uScript_LockTechStacks_10 = new uScript_LockTechStacks();

	private Tank logic_uScript_LockTechStacks_tech_10;

	private bool logic_uScript_LockTechStacks_Out_10 = true;

	private uScript_LockTechInteraction logic_uScript_LockTechInteraction_uScript_LockTechInteraction_11 = new uScript_LockTechInteraction();

	private Tank logic_uScript_LockTechInteraction_tech_11;

	private BlockTypes[] logic_uScript_LockTechInteraction_excludedBlocks_11 = new BlockTypes[0];

	private bool logic_uScript_LockTechInteraction_Out_11 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_13 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_13;

	private bool logic_uScriptCon_CompareBool_True_13 = true;

	private bool logic_uScriptCon_CompareBool_False_13 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_15 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_15;

	private bool logic_uScriptAct_SetBool_Out_15 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_15 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_15 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_18 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_18;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_18 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_18 = "msgBaseFoundShown";

	private uScript_PausePopulation logic_uScript_PausePopulation_uScript_PausePopulation_19 = new uScript_PausePopulation();

	private bool logic_uScript_PausePopulation_Out_19 = true;

	private uScript_PausePopulation logic_uScript_PausePopulation_uScript_PausePopulation_21 = new uScript_PausePopulation();

	private bool logic_uScript_PausePopulation_Out_21 = true;

	private uScript_HideArrow logic_uScript_HideArrow_uScript_HideArrow_22 = new uScript_HideArrow();

	private bool logic_uScript_HideArrow_Out_22 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_23 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_23;

	private bool logic_uScriptAct_SetBool_Out_23 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_23 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_23 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_24 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_24 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_24 = true;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_24 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_26 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_26;

	private bool logic_uScriptAct_SetBool_Out_26 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_26 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_26 = true;

	private uScript_IsPlayerInRangeOfTech logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_27 = new uScript_IsPlayerInRangeOfTech();

	private Tank logic_uScript_IsPlayerInRangeOfTech_tech_27;

	private float logic_uScript_IsPlayerInRangeOfTech_range_27 = 100f;

	private Tank[] logic_uScript_IsPlayerInRangeOfTech_techs_27 = new Tank[0];

	private bool logic_uScript_IsPlayerInRangeOfTech_Out_27 = true;

	private bool logic_uScript_IsPlayerInRangeOfTech_InRange_27 = true;

	private bool logic_uScript_IsPlayerInRangeOfTech_OutOfRange_27 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_29 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_29;

	private bool logic_uScriptCon_CompareBool_True_29 = true;

	private bool logic_uScriptCon_CompareBool_False_29 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_31 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_31;

	private bool logic_uScriptAct_SetBool_Out_31 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_31 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_31 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_33 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_33;

	private bool logic_uScriptCon_CompareBool_True_33 = true;

	private bool logic_uScriptCon_CompareBool_False_33 = true;

	private uScript_SpawnBlocksFromData logic_uScript_SpawnBlocksFromData_uScript_SpawnBlocksFromData_34 = new uScript_SpawnBlocksFromData();

	private SpawnBlockData[] logic_uScript_SpawnBlocksFromData_blockData_34 = new SpawnBlockData[0];

	private GameObject logic_uScript_SpawnBlocksFromData_ownerNode_34;

	private bool logic_uScript_SpawnBlocksFromData_Out_34 = true;

	private uScript_GetAndCheckBlocks logic_uScript_GetAndCheckBlocks_uScript_GetAndCheckBlocks_39 = new uScript_GetAndCheckBlocks();

	private SpawnBlockData[] logic_uScript_GetAndCheckBlocks_blockData_39 = new SpawnBlockData[0];

	private GameObject logic_uScript_GetAndCheckBlocks_ownerNode_39;

	private TankBlock[] logic_uScript_GetAndCheckBlocks_blocks_39 = new TankBlock[0];

	private bool logic_uScript_GetAndCheckBlocks_AllAlive_39 = true;

	private bool logic_uScript_GetAndCheckBlocks_SomeAlive_39 = true;

	private bool logic_uScript_GetAndCheckBlocks_AllDead_39 = true;

	private bool logic_uScript_GetAndCheckBlocks_WaitingToSpawn_39 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_42 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_42;

	private bool logic_uScriptCon_CompareBool_True_42 = true;

	private bool logic_uScriptCon_CompareBool_False_42 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_43 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_43;

	private bool logic_uScriptAct_SetBool_Out_43 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_43 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_43 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_44 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_44;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_44 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_44 = "msgIntroShown";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_47 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_47;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_47 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_47 = "AutoMinerSpawned";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_49 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_49;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_49 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_49 = "msgAutoMinerSpawnedShown";

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_51 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_51;

	private bool logic_uScriptAct_SetBool_Out_51 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_51 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_51 = true;

	private SubGraph_Crafting_Tutorial_ManageBlock logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_53 = new SubGraph_Crafting_Tutorial_ManageBlock();

	private SpawnBlockData[] logic_SubGraph_Crafting_Tutorial_ManageBlock_blockSpawnData_53 = new SpawnBlockData[0];

	private int logic_SubGraph_Crafting_Tutorial_ManageBlock_blockIndex_53;

	private TankBlock logic_SubGraph_Crafting_Tutorial_ManageBlock_block_53;

	private bool logic_SubGraph_Crafting_Tutorial_ManageBlock_allowAnchoring_53 = true;

	private uScript_AddMessage.MessageData logic_SubGraph_Crafting_Tutorial_ManageBlock_msgBlockOutsideArea_53;

	private uScript_AddMessage.MessageSpeaker logic_SubGraph_Crafting_Tutorial_ManageBlock_messageSpeaker_53;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_54 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_54;

	private bool logic_uScriptCon_CompareBool_True_54 = true;

	private bool logic_uScriptCon_CompareBool_False_54 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_57 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_57 = true;

	private uScriptCon_ManualSwitch logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_59 = new uScriptCon_ManualSwitch();

	private int logic_uScriptCon_ManualSwitch_CurrentOutput_59;

	private SubGraph_SaveLoadInt logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_60 = new SubGraph_SaveLoadInt();

	private int logic_SubGraph_SaveLoadInt_restartValue_60 = 1;

	private int logic_SubGraph_SaveLoadInt_integer_60;

	private object logic_SubGraph_SaveLoadInt_intAsVariable_60 = "";

	private string logic_SubGraph_SaveLoadInt_uniqueID_60 = "Stage";

	private SubGraph_LoadObjectiveStates logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_62 = new SubGraph_LoadObjectiveStates();

	private int logic_SubGraph_LoadObjectiveStates_currentObjective_62;

	private SubGraph_CompleteObjectiveStage logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_68 = new SubGraph_CompleteObjectiveStage();

	private int logic_SubGraph_CompleteObjectiveStage_objectiveStage_68;

	private bool logic_SubGraph_CompleteObjectiveStage_isFinalObjective_68;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_70 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_70;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_70;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_70;

	private bool logic_uScript_AddMessage_Out_70 = true;

	private bool logic_uScript_AddMessage_Shown_70 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_73 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_73;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_73;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_73;

	private bool logic_uScript_AddMessage_Out_73 = true;

	private bool logic_uScript_AddMessage_Shown_73 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_77 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_77;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_77;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_77;

	private bool logic_uScript_AddMessage_Out_77 = true;

	private bool logic_uScript_AddMessage_Shown_77 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_80 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_80;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_80;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_80;

	private bool logic_uScript_AddMessage_Out_80 = true;

	private bool logic_uScript_AddMessage_Shown_80 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_84 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_84;

	private bool logic_uScriptCon_CompareBool_True_84 = true;

	private bool logic_uScriptCon_CompareBool_False_84 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_85 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_85;

	private bool logic_uScriptAct_SetBool_Out_85 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_85 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_85 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_89 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_89;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_89;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_89;

	private bool logic_uScript_AddMessage_Out_89 = true;

	private bool logic_uScript_AddMessage_Shown_89 = true;

	private SubGraph_Crafting_Tutorial_Finish logic_SubGraph_Crafting_Tutorial_Finish_SubGraph_Crafting_Tutorial_Finish_90 = new SubGraph_Crafting_Tutorial_Finish();

	private Tank logic_SubGraph_Crafting_Tutorial_Finish_craftingBaseTech_90;

	private Tank logic_SubGraph_Crafting_Tutorial_Finish_NPCTech_90;

	private ExternalBehaviorTree logic_SubGraph_Crafting_Tutorial_Finish_NPCFlyAwayAI_90;

	private Transform logic_SubGraph_Crafting_Tutorial_Finish_NPCDespawnParticleEffect_90;

	private SubGraph_Crafting_Tutorial_Init logic_SubGraph_Crafting_Tutorial_Init_SubGraph_Crafting_Tutorial_Init_99 = new SubGraph_Crafting_Tutorial_Init();

	private SpawnTechData[] logic_SubGraph_Crafting_Tutorial_Init_baseSpawnData_99 = new SpawnTechData[0];

	private SpawnBlockData[] logic_SubGraph_Crafting_Tutorial_Init_blockSpawnData_99 = new SpawnBlockData[0];

	private SpawnTechData[] logic_SubGraph_Crafting_Tutorial_Init_NPCSpawnData_99 = new SpawnTechData[0];

	private TankPreset logic_SubGraph_Crafting_Tutorial_Init_completedBasePreset_99;

	private bool logic_SubGraph_Crafting_Tutorial_Init_useNPCAsTutorialTech_99;

	private bool logic_SubGraph_Crafting_Tutorial_Init_spawnBase_99;

	private string logic_SubGraph_Crafting_Tutorial_Init_basePosition_99 = "";

	private float logic_SubGraph_Crafting_Tutorial_Init_clearSceneryRadius_99;

	private Tank logic_SubGraph_Crafting_Tutorial_Init_CraftingBaseTech_99;

	private Tank logic_SubGraph_Crafting_Tutorial_Init_NPCTech_99;

	private uScript_SetEncounterTarget logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_104 = new uScript_SetEncounterTarget();

	private GameObject logic_uScript_SetEncounterTarget_owner_104;

	private object logic_uScript_SetEncounterTarget_visibleObject_104 = "";

	private bool logic_uScript_SetEncounterTarget_Out_104 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_107 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_107;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_107;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_107;

	private bool logic_uScript_AddMessage_Out_107 = true;

	private bool logic_uScript_AddMessage_Shown_107 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_108 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_108 = true;

	private uScript_IsResourceReservoir logic_uScript_IsResourceReservoir_uScript_IsResourceReservoir_109 = new uScript_IsResourceReservoir();

	private ResourceDispenser logic_uScript_IsResourceReservoir_resourceDispenser_109;

	private bool logic_uScript_IsResourceReservoir_True_109 = true;

	private bool logic_uScript_IsResourceReservoir_False_109 = true;

	private SubGraph_CompleteObjectiveStage logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_111 = new SubGraph_CompleteObjectiveStage();

	private int logic_SubGraph_CompleteObjectiveStage_objectiveStage_111;

	private bool logic_SubGraph_CompleteObjectiveStage_isFinalObjective_111;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_114 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_114;

	private bool logic_uScriptAct_SetBool_Out_114 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_114 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_114 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_117 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_117;

	private bool logic_uScriptCon_CompareBool_True_117 = true;

	private bool logic_uScriptCon_CompareBool_False_117 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_119 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_119;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_119;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_119;

	private bool logic_uScript_AddMessage_Out_119 = true;

	private bool logic_uScript_AddMessage_Shown_119 = true;

	private uScript_IsBlockMining logic_uScript_IsBlockMining_uScript_IsBlockMining_121 = new uScript_IsBlockMining();

	private TankBlock logic_uScript_IsBlockMining_block_121;

	private bool logic_uScript_IsBlockMining_True_121 = true;

	private bool logic_uScript_IsBlockMining_False_121 = true;

	private uScript_IsPlayerInteractingWithBlock logic_uScript_IsPlayerInteractingWithBlock_uScript_IsPlayerInteractingWithBlock_123 = new uScript_IsPlayerInteractingWithBlock();

	private TankBlock logic_uScript_IsPlayerInteractingWithBlock_block_123;

	private bool logic_uScript_IsPlayerInteractingWithBlock_Out_123 = true;

	private bool logic_uScript_IsPlayerInteractingWithBlock_Interacted_123 = true;

	private bool logic_uScript_IsPlayerInteractingWithBlock_NotInteracted_123 = true;

	private bool logic_uScript_IsPlayerInteractingWithBlock_Dragging_123 = true;

	private bool logic_uScript_IsPlayerInteractingWithBlock_NotDragging_123 = true;

	private uScript_PointArrowAtBlock logic_uScript_PointArrowAtBlock_uScript_PointArrowAtBlock_125 = new uScript_PointArrowAtBlock();

	private TankBlock logic_uScript_PointArrowAtBlock_block_125;

	private float logic_uScript_PointArrowAtBlock_timeToShowFor_125 = -1f;

	private Vector3 logic_uScript_PointArrowAtBlock_offset_125 = new Vector3(0f, 0f, 0f);

	private bool logic_uScript_PointArrowAtBlock_Out_125 = true;

	private uScript_PointArrowAtVisible logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_126 = new uScript_PointArrowAtVisible();

	private object logic_uScript_PointArrowAtVisible_targetObject_126 = "";

	private float logic_uScript_PointArrowAtVisible_timeToShowFor_126 = -1f;

	private Vector3 logic_uScript_PointArrowAtVisible_offset_126 = new Vector3(0f, 0f, 0f);

	private bool logic_uScript_PointArrowAtVisible_Out_126 = true;

	private uScript_HideArrow logic_uScript_HideArrow_uScript_HideArrow_129 = new uScript_HideArrow();

	private bool logic_uScript_HideArrow_Out_129 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_131 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_131;

	private bool logic_uScriptCon_CompareBool_True_131 = true;

	private bool logic_uScriptCon_CompareBool_False_131 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_132 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_132;

	private bool logic_uScriptAct_SetBool_Out_132 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_132 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_132 = true;

	private uScript_PointArrowAtVisible logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_135 = new uScript_PointArrowAtVisible();

	private object logic_uScript_PointArrowAtVisible_targetObject_135 = "";

	private float logic_uScript_PointArrowAtVisible_timeToShowFor_135 = 4f;

	private Vector3 logic_uScript_PointArrowAtVisible_offset_135 = new Vector3(0f, 0f, 0f);

	private bool logic_uScript_PointArrowAtVisible_Out_135 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_137 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_137;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_137 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_137 = "msgSeamUncoveredShown";

	private uScript_IsResourceReservoir logic_uScript_IsResourceReservoir_uScript_IsResourceReservoir_140 = new uScript_IsResourceReservoir();

	private ResourceDispenser logic_uScript_IsResourceReservoir_resourceDispenser_140;

	private bool logic_uScript_IsResourceReservoir_True_140 = true;

	private bool logic_uScript_IsResourceReservoir_False_140 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_141 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_141;

	private bool logic_uScriptAct_SetBool_Out_141 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_141 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_141 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_143 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_143;

	private bool logic_uScriptCon_CompareBool_True_143 = true;

	private bool logic_uScriptCon_CompareBool_False_143 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_145 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_145;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_145;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_145;

	private bool logic_uScript_AddMessage_Out_145 = true;

	private bool logic_uScript_AddMessage_Shown_145 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_148 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_148;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_148 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_148 = "SeamUncoveredEarly";

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_149 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_149;

	private bool logic_uScriptCon_CompareBool_True_149 = true;

	private bool logic_uScriptCon_CompareBool_False_149 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_152 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_152;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_152;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_152;

	private bool logic_uScript_AddMessage_Out_152 = true;

	private bool logic_uScript_AddMessage_Shown_152 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_154 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_154;

	private bool logic_uScriptAct_SetBool_Out_154 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_154 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_154 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_155 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_155 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_156 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_156 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_158 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_158;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_158 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_158 = "msgUncoverSeamShown";

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_167 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_167;

	private bool logic_uScriptCon_CompareBool_True_167 = true;

	private bool logic_uScriptCon_CompareBool_False_167 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_168 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_168;

	private bool logic_uScriptAct_SetBool_Out_168 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_168 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_168 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_171 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_171;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_171 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_171 = "ScenerySpawned";

	private uScript_GetSpawnedScenery logic_uScript_GetSpawnedScenery_uScript_GetSpawnedScenery_173 = new uScript_GetSpawnedScenery();

	private GameObject logic_uScript_GetSpawnedScenery_ownerNode_173;

	private string logic_uScript_GetSpawnedScenery_uniqueSceneryName_173 = "";

	private ResourceDispenser logic_uScript_GetSpawnedScenery_Return_173;

	private bool logic_uScript_GetSpawnedScenery_Out_173 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_178 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_178 = true;

	private uScript_FinishEncounter logic_uScript_FinishEncounter_uScript_FinishEncounter_179 = new uScript_FinishEncounter();

	private GameObject logic_uScript_FinishEncounter_owner_179;

	private bool logic_uScript_FinishEncounter_Out_179 = true;

	private uScript_FlyTechUpAndAway logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_181 = new uScript_FlyTechUpAndAway();

	private Tank logic_uScript_FlyTechUpAndAway_tech_181;

	private float logic_uScript_FlyTechUpAndAway_maxLifetime_181 = 5f;

	private float logic_uScript_FlyTechUpAndAway_targetHeight_181 = 150f;

	private ExternalBehaviorTree logic_uScript_FlyTechUpAndAway_aiTree_181;

	private Transform logic_uScript_FlyTechUpAndAway_removalParticles_181;

	private bool logic_uScript_FlyTechUpAndAway_Out_181 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_186 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_186 = "";

	private bool logic_uScript_EnableGlow_enable_186 = true;

	private bool logic_uScript_EnableGlow_Out_186 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_188 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_188 = "";

	private bool logic_uScript_EnableGlow_enable_188;

	private bool logic_uScript_EnableGlow_Out_188 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_190 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_190;

	private bool logic_uScriptCon_CompareBool_True_190 = true;

	private bool logic_uScriptCon_CompareBool_False_190 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_193 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_193 = "";

	private bool logic_uScript_EnableGlow_enable_193;

	private bool logic_uScript_EnableGlow_Out_193 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_194 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_194 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_195 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_195 = "";

	private bool logic_uScript_EnableGlow_enable_195 = true;

	private bool logic_uScript_EnableGlow_Out_195 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_197 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_197 = "";

	private bool logic_uScript_EnableGlow_enable_197;

	private bool logic_uScript_EnableGlow_Out_197 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_200 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_200 = "";

	private bool logic_uScript_EnableGlow_enable_200;

	private bool logic_uScript_EnableGlow_Out_200 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_201 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_201 = "";

	private bool logic_uScript_EnableGlow_enable_201;

	private bool logic_uScript_EnableGlow_Out_201 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_204 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_204 = "";

	private bool logic_uScript_EnableGlow_enable_204;

	private bool logic_uScript_EnableGlow_Out_204 = true;

	private uScript_SendAnaliticsEvent logic_uScript_SendAnaliticsEvent_uScript_SendAnaliticsEvent_205 = new uScript_SendAnaliticsEvent();

	private string logic_uScript_SendAnaliticsEvent_analiticsEvent_205 = "tutorial_complete";

	private string logic_uScript_SendAnaliticsEvent_parameterName_205 = "crafting_tutorial";

	private object logic_uScript_SendAnaliticsEvent_parameter_205 = "6";

	private bool logic_uScript_SendAnaliticsEvent_Out_205 = true;

	private uScript_SendAnaliticsEvent logic_uScript_SendAnaliticsEvent_uScript_SendAnaliticsEvent_206 = new uScript_SendAnaliticsEvent();

	private string logic_uScript_SendAnaliticsEvent_analiticsEvent_206 = "tutorial_start";

	private string logic_uScript_SendAnaliticsEvent_parameterName_206 = "crafting_tutorial";

	private object logic_uScript_SendAnaliticsEvent_parameter_206 = "6";

	private bool logic_uScript_SendAnaliticsEvent_Out_206 = true;

	private uScript_GetPlayerTankWithBlock logic_uScript_GetPlayerTankWithBlock_uScript_GetPlayerTankWithBlock_207 = new uScript_GetPlayerTankWithBlock();

	private BlockTypes logic_uScript_GetPlayerTankWithBlock_block_207;

	private TankBlock logic_uScript_GetPlayerTankWithBlock_tankBlock_207;

	private bool logic_uScript_GetPlayerTankWithBlock_useBlockType_207;

	private Tank logic_uScript_GetPlayerTankWithBlock_Return_207;

	private bool logic_uScript_GetPlayerTankWithBlock_Returned_207 = true;

	private bool logic_uScript_GetPlayerTankWithBlock_NotReturned_207 = true;

	private bool logic_uScript_GetPlayerTankWithBlock_Out_207 = true;

	private uScript_GetPositionOfTech logic_uScript_GetPositionOfTech_uScript_GetPositionOfTech_210 = new uScript_GetPositionOfTech();

	private Tank logic_uScript_GetPositionOfTech_tech_210;

	private Vector3 logic_uScript_GetPositionOfTech_Return_210;

	private bool logic_uScript_GetPositionOfTech_Out_210 = true;

	private bool logic_uScript_GetPositionOfTech_TechValid_210 = true;

	private bool logic_uScript_GetPositionOfTech_TechNull_210 = true;

	private uScriptAct_GetVector3Distance logic_uScriptAct_GetVector3Distance_uScriptAct_GetVector3Distance_211 = new uScriptAct_GetVector3Distance();

	private Vector3 logic_uScriptAct_GetVector3Distance_A_211;

	private Vector3 logic_uScriptAct_GetVector3Distance_B_211;

	private float logic_uScriptAct_GetVector3Distance_Distance_211;

	private bool logic_uScriptAct_GetVector3Distance_Out_211 = true;

	private uScriptCon_CompareFloat logic_uScriptCon_CompareFloat_uScriptCon_CompareFloat_214 = new uScriptCon_CompareFloat();

	private float logic_uScriptCon_CompareFloat_A_214;

	private float logic_uScriptCon_CompareFloat_B_214 = 5f;

	private bool logic_uScriptCon_CompareFloat_GreaterThan_214 = true;

	private bool logic_uScriptCon_CompareFloat_GreaterThanOrEqualTo_214 = true;

	private bool logic_uScriptCon_CompareFloat_EqualTo_214 = true;

	private bool logic_uScriptCon_CompareFloat_NotEqualTo_214 = true;

	private bool logic_uScriptCon_CompareFloat_LessThanOrEqualTo_214 = true;

	private bool logic_uScriptCon_CompareFloat_LessThan_214 = true;

	private uScript_SpawnScenery logic_uScript_SpawnScenery_uScript_SpawnScenery_218 = new uScript_SpawnScenery();

	private GameObject logic_uScript_SpawnScenery_ownerNode_218;

	private TerrainObject logic_uScript_SpawnScenery_sceneryPrefab_218;

	private string logic_uScript_SpawnScenery_posName_218 = "Origin";

	private string logic_uScript_SpawnScenery_uniqueName_218 = "";

	private Vector3[] logic_uScript_SpawnScenery_spawnPositions_218;

	private int logic_uScript_SpawnScenery_spawnNum_218 = 1;

	private float logic_uScript_SpawnScenery_maxSpawnRange_218;

	private float logic_uScript_SpawnScenery_minSpawnRange_218;

	private bool logic_uScript_SpawnScenery_Out_218 = true;

	private uScript_SpawnScenery logic_uScript_SpawnScenery_uScript_SpawnScenery_219 = new uScript_SpawnScenery();

	private GameObject logic_uScript_SpawnScenery_ownerNode_219;

	private TerrainObject logic_uScript_SpawnScenery_sceneryPrefab_219;

	private string logic_uScript_SpawnScenery_posName_219 = "Origin";

	private string logic_uScript_SpawnScenery_uniqueName_219 = "Scenery";

	private Vector3[] logic_uScript_SpawnScenery_spawnPositions_219;

	private int logic_uScript_SpawnScenery_spawnNum_219;

	private float logic_uScript_SpawnScenery_maxSpawnRange_219;

	private float logic_uScript_SpawnScenery_minSpawnRange_219;

	private bool logic_uScript_SpawnScenery_Out_219 = true;

	private uScript_GetSpawnedScenery logic_uScript_GetSpawnedScenery_uScript_GetSpawnedScenery_223 = new uScript_GetSpawnedScenery();

	private GameObject logic_uScript_GetSpawnedScenery_ownerNode_223;

	private string logic_uScript_GetSpawnedScenery_uniqueSceneryName_223 = "";

	private ResourceDispenser logic_uScript_GetSpawnedScenery_Return_223;

	private bool logic_uScript_GetSpawnedScenery_Out_223 = true;

	private uScript_GetPositionOfVisibleObject logic_uScript_GetPositionOfVisibleObject_uScript_GetPositionOfVisibleObject_489 = new uScript_GetPositionOfVisibleObject();

	private object logic_uScript_GetPositionOfVisibleObject_visibleObject_489 = "";

	private Vector3 logic_uScript_GetPositionOfVisibleObject_Return_489;

	private bool logic_uScript_GetPositionOfVisibleObject_Out_489 = true;

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
		if (null == owner_Connection_5 || !m_RegisteredForEvents)
		{
			owner_Connection_5 = parentGameObject;
			if (null != owner_Connection_5)
			{
				uScript_SaveLoad uScript_SaveLoad2 = owner_Connection_5.GetComponent<uScript_SaveLoad>();
				if (null == uScript_SaveLoad2)
				{
					uScript_SaveLoad2 = owner_Connection_5.AddComponent<uScript_SaveLoad>();
				}
				if (null != uScript_SaveLoad2)
				{
					uScript_SaveLoad2.SaveEvent += Instance_SaveEvent_4;
					uScript_SaveLoad2.LoadEvent += Instance_LoadEvent_4;
					uScript_SaveLoad2.RestartEvent += Instance_RestartEvent_4;
				}
			}
		}
		if (null == owner_Connection_35 || !m_RegisteredForEvents)
		{
			owner_Connection_35 = parentGameObject;
		}
		if (null == owner_Connection_38 || !m_RegisteredForEvents)
		{
			owner_Connection_38 = parentGameObject;
		}
		if (null == owner_Connection_103 || !m_RegisteredForEvents)
		{
			owner_Connection_103 = parentGameObject;
		}
		if (null == owner_Connection_159 || !m_RegisteredForEvents)
		{
			owner_Connection_159 = parentGameObject;
		}
		if (null == owner_Connection_163 || !m_RegisteredForEvents)
		{
			owner_Connection_163 = parentGameObject;
		}
		if (null == owner_Connection_174 || !m_RegisteredForEvents)
		{
			owner_Connection_174 = parentGameObject;
		}
		if (null == owner_Connection_180 || !m_RegisteredForEvents)
		{
			owner_Connection_180 = parentGameObject;
		}
		if (null == owner_Connection_221 || !m_RegisteredForEvents)
		{
			owner_Connection_221 = parentGameObject;
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
		if (!m_RegisteredForEvents && null != owner_Connection_5)
		{
			uScript_SaveLoad uScript_SaveLoad2 = owner_Connection_5.GetComponent<uScript_SaveLoad>();
			if (null == uScript_SaveLoad2)
			{
				uScript_SaveLoad2 = owner_Connection_5.AddComponent<uScript_SaveLoad>();
			}
			if (null != uScript_SaveLoad2)
			{
				uScript_SaveLoad2.SaveEvent += Instance_SaveEvent_4;
				uScript_SaveLoad2.LoadEvent += Instance_LoadEvent_4;
				uScript_SaveLoad2.RestartEvent += Instance_RestartEvent_4;
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
		if (null != owner_Connection_5)
		{
			uScript_SaveLoad component2 = owner_Connection_5.GetComponent<uScript_SaveLoad>();
			if (null != component2)
			{
				component2.SaveEvent -= Instance_SaveEvent_4;
				component2.LoadEvent -= Instance_LoadEvent_4;
				component2.RestartEvent -= Instance_RestartEvent_4;
			}
		}
	}

	public override void SetParent(GameObject g)
	{
		parentGameObject = g;
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_2.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_8.SetParent(g);
		logic_uScript_LockTechStacks_uScript_LockTechStacks_10.SetParent(g);
		logic_uScript_LockTechInteraction_uScript_LockTechInteraction_11.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_13.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_15.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_18.SetParent(g);
		logic_uScript_PausePopulation_uScript_PausePopulation_19.SetParent(g);
		logic_uScript_PausePopulation_uScript_PausePopulation_21.SetParent(g);
		logic_uScript_HideArrow_uScript_HideArrow_22.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_23.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_24.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_26.SetParent(g);
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_27.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_29.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_31.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_33.SetParent(g);
		logic_uScript_SpawnBlocksFromData_uScript_SpawnBlocksFromData_34.SetParent(g);
		logic_uScript_GetAndCheckBlocks_uScript_GetAndCheckBlocks_39.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_42.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_43.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_44.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_47.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_49.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_51.SetParent(g);
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_53.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_54.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_57.SetParent(g);
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_59.SetParent(g);
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_60.SetParent(g);
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_62.SetParent(g);
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_68.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_70.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_73.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_77.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_80.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_84.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_85.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_89.SetParent(g);
		logic_SubGraph_Crafting_Tutorial_Finish_SubGraph_Crafting_Tutorial_Finish_90.SetParent(g);
		logic_SubGraph_Crafting_Tutorial_Init_SubGraph_Crafting_Tutorial_Init_99.SetParent(g);
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_104.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_107.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_108.SetParent(g);
		logic_uScript_IsResourceReservoir_uScript_IsResourceReservoir_109.SetParent(g);
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_111.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_114.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_117.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_119.SetParent(g);
		logic_uScript_IsBlockMining_uScript_IsBlockMining_121.SetParent(g);
		logic_uScript_IsPlayerInteractingWithBlock_uScript_IsPlayerInteractingWithBlock_123.SetParent(g);
		logic_uScript_PointArrowAtBlock_uScript_PointArrowAtBlock_125.SetParent(g);
		logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_126.SetParent(g);
		logic_uScript_HideArrow_uScript_HideArrow_129.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_131.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_132.SetParent(g);
		logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_135.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_137.SetParent(g);
		logic_uScript_IsResourceReservoir_uScript_IsResourceReservoir_140.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_141.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_143.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_145.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_148.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_149.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_152.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_154.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_155.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_156.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_158.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_167.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_168.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_171.SetParent(g);
		logic_uScript_GetSpawnedScenery_uScript_GetSpawnedScenery_173.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_178.SetParent(g);
		logic_uScript_FinishEncounter_uScript_FinishEncounter_179.SetParent(g);
		logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_181.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_186.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_188.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_190.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_193.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_194.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_195.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_197.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_200.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_201.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_204.SetParent(g);
		logic_uScript_SendAnaliticsEvent_uScript_SendAnaliticsEvent_205.SetParent(g);
		logic_uScript_SendAnaliticsEvent_uScript_SendAnaliticsEvent_206.SetParent(g);
		logic_uScript_GetPlayerTankWithBlock_uScript_GetPlayerTankWithBlock_207.SetParent(g);
		logic_uScript_GetPositionOfTech_uScript_GetPositionOfTech_210.SetParent(g);
		logic_uScriptAct_GetVector3Distance_uScriptAct_GetVector3Distance_211.SetParent(g);
		logic_uScriptCon_CompareFloat_uScriptCon_CompareFloat_214.SetParent(g);
		logic_uScript_SpawnScenery_uScript_SpawnScenery_218.SetParent(g);
		logic_uScript_SpawnScenery_uScript_SpawnScenery_219.SetParent(g);
		logic_uScript_GetSpawnedScenery_uScript_GetSpawnedScenery_223.SetParent(g);
		logic_uScript_GetPositionOfVisibleObject_uScript_GetPositionOfVisibleObject_489.SetParent(g);
		owner_Connection_1 = parentGameObject;
		owner_Connection_5 = parentGameObject;
		owner_Connection_35 = parentGameObject;
		owner_Connection_38 = parentGameObject;
		owner_Connection_103 = parentGameObject;
		owner_Connection_159 = parentGameObject;
		owner_Connection_163 = parentGameObject;
		owner_Connection_174 = parentGameObject;
		owner_Connection_180 = parentGameObject;
		owner_Connection_221 = parentGameObject;
	}

	public void Awake()
	{
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_8.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_18.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_44.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_47.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_49.Awake();
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_53.Awake();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_60.Awake();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_62.Awake();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_68.Awake();
		logic_SubGraph_Crafting_Tutorial_Finish_SubGraph_Crafting_Tutorial_Finish_90.Awake();
		logic_SubGraph_Crafting_Tutorial_Init_SubGraph_Crafting_Tutorial_Init_99.Awake();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_111.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_137.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_148.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_158.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_171.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_8.Save_Out += SubGraph_SaveLoadBool_Save_Out_8;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_8.Load_Out += SubGraph_SaveLoadBool_Load_Out_8;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_8.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_8;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_18.Save_Out += SubGraph_SaveLoadBool_Save_Out_18;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_18.Load_Out += SubGraph_SaveLoadBool_Load_Out_18;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_18.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_18;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_44.Save_Out += SubGraph_SaveLoadBool_Save_Out_44;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_44.Load_Out += SubGraph_SaveLoadBool_Load_Out_44;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_44.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_44;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_47.Save_Out += SubGraph_SaveLoadBool_Save_Out_47;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_47.Load_Out += SubGraph_SaveLoadBool_Load_Out_47;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_47.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_47;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_49.Save_Out += SubGraph_SaveLoadBool_Save_Out_49;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_49.Load_Out += SubGraph_SaveLoadBool_Load_Out_49;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_49.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_49;
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_53.Out += SubGraph_Crafting_Tutorial_ManageBlock_Out_53;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_59.Output1 += uScriptCon_ManualSwitch_Output1_59;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_59.Output2 += uScriptCon_ManualSwitch_Output2_59;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_59.Output3 += uScriptCon_ManualSwitch_Output3_59;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_59.Output4 += uScriptCon_ManualSwitch_Output4_59;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_59.Output5 += uScriptCon_ManualSwitch_Output5_59;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_59.Output6 += uScriptCon_ManualSwitch_Output6_59;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_59.Output7 += uScriptCon_ManualSwitch_Output7_59;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_59.Output8 += uScriptCon_ManualSwitch_Output8_59;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_60.Save_Out += SubGraph_SaveLoadInt_Save_Out_60;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_60.Load_Out += SubGraph_SaveLoadInt_Load_Out_60;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_60.Restart_Out += SubGraph_SaveLoadInt_Restart_Out_60;
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_62.Out += SubGraph_LoadObjectiveStates_Out_62;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_68.Out += SubGraph_CompleteObjectiveStage_Out_68;
		logic_SubGraph_Crafting_Tutorial_Finish_SubGraph_Crafting_Tutorial_Finish_90.Out += SubGraph_Crafting_Tutorial_Finish_Out_90;
		logic_SubGraph_Crafting_Tutorial_Init_SubGraph_Crafting_Tutorial_Init_99.Out += SubGraph_Crafting_Tutorial_Init_Out_99;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_111.Out += SubGraph_CompleteObjectiveStage_Out_111;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_137.Save_Out += SubGraph_SaveLoadBool_Save_Out_137;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_137.Load_Out += SubGraph_SaveLoadBool_Load_Out_137;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_137.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_137;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_148.Save_Out += SubGraph_SaveLoadBool_Save_Out_148;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_148.Load_Out += SubGraph_SaveLoadBool_Load_Out_148;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_148.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_148;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_158.Save_Out += SubGraph_SaveLoadBool_Save_Out_158;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_158.Load_Out += SubGraph_SaveLoadBool_Load_Out_158;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_158.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_158;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_171.Save_Out += SubGraph_SaveLoadBool_Save_Out_171;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_171.Load_Out += SubGraph_SaveLoadBool_Load_Out_171;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_171.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_171;
	}

	public void Start()
	{
		SyncUnityHooks();
		m_RegisteredForEvents = true;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_8.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_18.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_44.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_47.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_49.Start();
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_53.Start();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_60.Start();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_62.Start();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_68.Start();
		logic_SubGraph_Crafting_Tutorial_Finish_SubGraph_Crafting_Tutorial_Finish_90.Start();
		logic_SubGraph_Crafting_Tutorial_Init_SubGraph_Crafting_Tutorial_Init_99.Start();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_111.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_137.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_148.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_158.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_171.Start();
	}

	public void OnEnable()
	{
		RegisterForUnityHooks();
		m_RegisteredForEvents = true;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_8.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_18.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_44.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_47.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_49.OnEnable();
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_53.OnEnable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_60.OnEnable();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_62.OnEnable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_68.OnEnable();
		logic_SubGraph_Crafting_Tutorial_Finish_SubGraph_Crafting_Tutorial_Finish_90.OnEnable();
		logic_SubGraph_Crafting_Tutorial_Init_SubGraph_Crafting_Tutorial_Init_99.OnEnable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_111.OnEnable();
		logic_uScript_IsBlockMining_uScript_IsBlockMining_121.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_137.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_148.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_158.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_171.OnEnable();
	}

	public void OnDisable()
	{
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_2.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_8.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_18.OnDisable();
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_27.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_44.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_47.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_49.OnDisable();
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_53.OnDisable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_60.OnDisable();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_62.OnDisable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_68.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_70.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_73.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_77.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_80.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_89.OnDisable();
		logic_SubGraph_Crafting_Tutorial_Finish_SubGraph_Crafting_Tutorial_Finish_90.OnDisable();
		logic_SubGraph_Crafting_Tutorial_Init_SubGraph_Crafting_Tutorial_Init_99.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_107.OnDisable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_111.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_119.OnDisable();
		logic_uScript_IsPlayerInteractingWithBlock_uScript_IsPlayerInteractingWithBlock_123.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_137.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_145.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_148.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_152.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_158.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_171.OnDisable();
		logic_uScript_GetSpawnedScenery_uScript_GetSpawnedScenery_173.OnDisable();
		logic_uScript_GetPositionOfTech_uScript_GetPositionOfTech_210.OnDisable();
		logic_uScript_SpawnScenery_uScript_SpawnScenery_218.OnDisable();
		logic_uScript_SpawnScenery_uScript_SpawnScenery_219.OnDisable();
		logic_uScript_GetSpawnedScenery_uScript_GetSpawnedScenery_223.OnDisable();
		UnregisterEventListeners();
		m_RegisteredForEvents = false;
	}

	public void Update()
	{
		SyncEventListeners();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_8.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_18.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_44.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_47.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_49.Update();
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_53.Update();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_60.Update();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_62.Update();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_68.Update();
		logic_SubGraph_Crafting_Tutorial_Finish_SubGraph_Crafting_Tutorial_Finish_90.Update();
		logic_SubGraph_Crafting_Tutorial_Init_SubGraph_Crafting_Tutorial_Init_99.Update();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_111.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_137.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_148.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_158.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_171.Update();
	}

	public void OnDestroy()
	{
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_8.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_18.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_44.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_47.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_49.OnDestroy();
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_53.OnDestroy();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_60.OnDestroy();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_62.OnDestroy();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_68.OnDestroy();
		logic_SubGraph_Crafting_Tutorial_Finish_SubGraph_Crafting_Tutorial_Finish_90.OnDestroy();
		logic_SubGraph_Crafting_Tutorial_Init_SubGraph_Crafting_Tutorial_Init_99.OnDestroy();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_111.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_137.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_148.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_158.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_171.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_8.Save_Out -= SubGraph_SaveLoadBool_Save_Out_8;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_8.Load_Out -= SubGraph_SaveLoadBool_Load_Out_8;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_8.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_8;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_18.Save_Out -= SubGraph_SaveLoadBool_Save_Out_18;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_18.Load_Out -= SubGraph_SaveLoadBool_Load_Out_18;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_18.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_18;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_44.Save_Out -= SubGraph_SaveLoadBool_Save_Out_44;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_44.Load_Out -= SubGraph_SaveLoadBool_Load_Out_44;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_44.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_44;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_47.Save_Out -= SubGraph_SaveLoadBool_Save_Out_47;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_47.Load_Out -= SubGraph_SaveLoadBool_Load_Out_47;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_47.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_47;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_49.Save_Out -= SubGraph_SaveLoadBool_Save_Out_49;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_49.Load_Out -= SubGraph_SaveLoadBool_Load_Out_49;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_49.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_49;
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_53.Out -= SubGraph_Crafting_Tutorial_ManageBlock_Out_53;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_59.Output1 -= uScriptCon_ManualSwitch_Output1_59;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_59.Output2 -= uScriptCon_ManualSwitch_Output2_59;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_59.Output3 -= uScriptCon_ManualSwitch_Output3_59;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_59.Output4 -= uScriptCon_ManualSwitch_Output4_59;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_59.Output5 -= uScriptCon_ManualSwitch_Output5_59;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_59.Output6 -= uScriptCon_ManualSwitch_Output6_59;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_59.Output7 -= uScriptCon_ManualSwitch_Output7_59;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_59.Output8 -= uScriptCon_ManualSwitch_Output8_59;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_60.Save_Out -= SubGraph_SaveLoadInt_Save_Out_60;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_60.Load_Out -= SubGraph_SaveLoadInt_Load_Out_60;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_60.Restart_Out -= SubGraph_SaveLoadInt_Restart_Out_60;
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_62.Out -= SubGraph_LoadObjectiveStates_Out_62;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_68.Out -= SubGraph_CompleteObjectiveStage_Out_68;
		logic_SubGraph_Crafting_Tutorial_Finish_SubGraph_Crafting_Tutorial_Finish_90.Out -= SubGraph_Crafting_Tutorial_Finish_Out_90;
		logic_SubGraph_Crafting_Tutorial_Init_SubGraph_Crafting_Tutorial_Init_99.Out -= SubGraph_Crafting_Tutorial_Init_Out_99;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_111.Out -= SubGraph_CompleteObjectiveStage_Out_111;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_137.Save_Out -= SubGraph_SaveLoadBool_Save_Out_137;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_137.Load_Out -= SubGraph_SaveLoadBool_Load_Out_137;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_137.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_137;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_148.Save_Out -= SubGraph_SaveLoadBool_Save_Out_148;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_148.Load_Out -= SubGraph_SaveLoadBool_Load_Out_148;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_148.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_148;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_158.Save_Out -= SubGraph_SaveLoadBool_Save_Out_158;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_158.Load_Out -= SubGraph_SaveLoadBool_Load_Out_158;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_158.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_158;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_171.Save_Out -= SubGraph_SaveLoadBool_Save_Out_171;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_171.Load_Out -= SubGraph_SaveLoadBool_Load_Out_171;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_171.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_171;
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

	private void SubGraph_SaveLoadBool_Save_Out_8(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_8 = e.boolean;
		local_MiningInProgress_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_8;
		Relay_Save_Out_8();
	}

	private void SubGraph_SaveLoadBool_Load_Out_8(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_8 = e.boolean;
		local_MiningInProgress_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_8;
		Relay_Load_Out_8();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_8(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_8 = e.boolean;
		local_MiningInProgress_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_8;
		Relay_Restart_Out_8();
	}

	private void SubGraph_SaveLoadBool_Save_Out_18(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_18 = e.boolean;
		local_msgBaseFoundShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_18;
		Relay_Save_Out_18();
	}

	private void SubGraph_SaveLoadBool_Load_Out_18(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_18 = e.boolean;
		local_msgBaseFoundShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_18;
		Relay_Load_Out_18();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_18(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_18 = e.boolean;
		local_msgBaseFoundShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_18;
		Relay_Restart_Out_18();
	}

	private void SubGraph_SaveLoadBool_Save_Out_44(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_44 = e.boolean;
		local_msgIntroShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_44;
		Relay_Save_Out_44();
	}

	private void SubGraph_SaveLoadBool_Load_Out_44(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_44 = e.boolean;
		local_msgIntroShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_44;
		Relay_Load_Out_44();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_44(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_44 = e.boolean;
		local_msgIntroShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_44;
		Relay_Restart_Out_44();
	}

	private void SubGraph_SaveLoadBool_Save_Out_47(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_47 = e.boolean;
		local_AutoMinerSpawned_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_47;
		Relay_Save_Out_47();
	}

	private void SubGraph_SaveLoadBool_Load_Out_47(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_47 = e.boolean;
		local_AutoMinerSpawned_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_47;
		Relay_Load_Out_47();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_47(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_47 = e.boolean;
		local_AutoMinerSpawned_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_47;
		Relay_Restart_Out_47();
	}

	private void SubGraph_SaveLoadBool_Save_Out_49(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_49 = e.boolean;
		local_msgAutoMinerSpawnedShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_49;
		Relay_Save_Out_49();
	}

	private void SubGraph_SaveLoadBool_Load_Out_49(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_49 = e.boolean;
		local_msgAutoMinerSpawnedShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_49;
		Relay_Load_Out_49();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_49(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_49 = e.boolean;
		local_msgAutoMinerSpawnedShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_49;
		Relay_Restart_Out_49();
	}

	private void SubGraph_Crafting_Tutorial_ManageBlock_Out_53(object o, SubGraph_Crafting_Tutorial_ManageBlock.LogicEventArgs e)
	{
		logic_SubGraph_Crafting_Tutorial_ManageBlock_block_53 = e.block;
		blockSpawnDataAutoMiner = logic_SubGraph_Crafting_Tutorial_ManageBlock_blockSpawnData_53;
		local_AutoMinerBlock_TankBlock = logic_SubGraph_Crafting_Tutorial_ManageBlock_block_53;
		Relay_Out_53();
	}

	private void uScriptCon_ManualSwitch_Output1_59(object o, EventArgs e)
	{
		Relay_Output1_59();
	}

	private void uScriptCon_ManualSwitch_Output2_59(object o, EventArgs e)
	{
		Relay_Output2_59();
	}

	private void uScriptCon_ManualSwitch_Output3_59(object o, EventArgs e)
	{
		Relay_Output3_59();
	}

	private void uScriptCon_ManualSwitch_Output4_59(object o, EventArgs e)
	{
		Relay_Output4_59();
	}

	private void uScriptCon_ManualSwitch_Output5_59(object o, EventArgs e)
	{
		Relay_Output5_59();
	}

	private void uScriptCon_ManualSwitch_Output6_59(object o, EventArgs e)
	{
		Relay_Output6_59();
	}

	private void uScriptCon_ManualSwitch_Output7_59(object o, EventArgs e)
	{
		Relay_Output7_59();
	}

	private void uScriptCon_ManualSwitch_Output8_59(object o, EventArgs e)
	{
		Relay_Output8_59();
	}

	private void SubGraph_SaveLoadInt_Save_Out_60(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_60 = e.integer;
		local_Stage_System_Int32 = logic_SubGraph_SaveLoadInt_integer_60;
		Relay_Save_Out_60();
	}

	private void SubGraph_SaveLoadInt_Load_Out_60(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_60 = e.integer;
		local_Stage_System_Int32 = logic_SubGraph_SaveLoadInt_integer_60;
		Relay_Load_Out_60();
	}

	private void SubGraph_SaveLoadInt_Restart_Out_60(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_60 = e.integer;
		local_Stage_System_Int32 = logic_SubGraph_SaveLoadInt_integer_60;
		Relay_Restart_Out_60();
	}

	private void SubGraph_LoadObjectiveStates_Out_62(object o, SubGraph_LoadObjectiveStates.LogicEventArgs e)
	{
		Relay_Out_62();
	}

	private void SubGraph_CompleteObjectiveStage_Out_68(object o, SubGraph_CompleteObjectiveStage.LogicEventArgs e)
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_68 = e.objectiveStage;
		local_Stage_System_Int32 = logic_SubGraph_CompleteObjectiveStage_objectiveStage_68;
		Relay_Out_68();
	}

	private void SubGraph_Crafting_Tutorial_Finish_Out_90(object o, SubGraph_Crafting_Tutorial_Finish.LogicEventArgs e)
	{
		Relay_Out_90();
	}

	private void SubGraph_Crafting_Tutorial_Init_Out_99(object o, SubGraph_Crafting_Tutorial_Init.LogicEventArgs e)
	{
		logic_SubGraph_Crafting_Tutorial_Init_CraftingBaseTech_99 = e.CraftingBaseTech;
		logic_SubGraph_Crafting_Tutorial_Init_NPCTech_99 = e.NPCTech;
		local_NPCTech_Tank = logic_SubGraph_Crafting_Tutorial_Init_NPCTech_99;
		Relay_Out_99();
	}

	private void SubGraph_CompleteObjectiveStage_Out_111(object o, SubGraph_CompleteObjectiveStage.LogicEventArgs e)
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_111 = e.objectiveStage;
		local_Stage_System_Int32 = logic_SubGraph_CompleteObjectiveStage_objectiveStage_111;
		Relay_Out_111();
	}

	private void SubGraph_SaveLoadBool_Save_Out_137(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_137 = e.boolean;
		local_msgSeamUncoveredShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_137;
		Relay_Save_Out_137();
	}

	private void SubGraph_SaveLoadBool_Load_Out_137(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_137 = e.boolean;
		local_msgSeamUncoveredShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_137;
		Relay_Load_Out_137();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_137(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_137 = e.boolean;
		local_msgSeamUncoveredShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_137;
		Relay_Restart_Out_137();
	}

	private void SubGraph_SaveLoadBool_Save_Out_148(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_148 = e.boolean;
		local_SeamUncoveredEarly_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_148;
		Relay_Save_Out_148();
	}

	private void SubGraph_SaveLoadBool_Load_Out_148(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_148 = e.boolean;
		local_SeamUncoveredEarly_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_148;
		Relay_Load_Out_148();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_148(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_148 = e.boolean;
		local_SeamUncoveredEarly_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_148;
		Relay_Restart_Out_148();
	}

	private void SubGraph_SaveLoadBool_Save_Out_158(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_158 = e.boolean;
		local_msgUncoverSeamShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_158;
		Relay_Save_Out_158();
	}

	private void SubGraph_SaveLoadBool_Load_Out_158(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_158 = e.boolean;
		local_msgUncoverSeamShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_158;
		Relay_Load_Out_158();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_158(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_158 = e.boolean;
		local_msgUncoverSeamShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_158;
		Relay_Restart_Out_158();
	}

	private void SubGraph_SaveLoadBool_Save_Out_171(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_171 = e.boolean;
		local_ScenerySpawned_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_171;
		Relay_Save_Out_171();
	}

	private void SubGraph_SaveLoadBool_Load_Out_171(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_171 = e.boolean;
		local_ScenerySpawned_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_171;
		Relay_Load_Out_171();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_171(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_171 = e.boolean;
		local_ScenerySpawned_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_171;
		Relay_Restart_Out_171();
	}

	private void Relay_OnUpdate_0()
	{
		Relay_In_99();
	}

	private void Relay_OnSuspend_0()
	{
	}

	private void Relay_OnResume_0()
	{
	}

	private void Relay_In_2()
	{
		logic_uScript_IsPlayerInRangeOfTech_tech_2 = local_NPCTech_Tank;
		logic_uScript_IsPlayerInRangeOfTech_range_2 = distBaseFound;
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_2.In(logic_uScript_IsPlayerInRangeOfTech_tech_2, logic_uScript_IsPlayerInRangeOfTech_range_2, logic_uScript_IsPlayerInRangeOfTech_techs_2);
		bool inRange = logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_2.InRange;
		bool outOfRange = logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_2.OutOfRange;
		if (inRange)
		{
			Relay_Pause_19();
		}
		if (outOfRange)
		{
			Relay_UnPause_21();
		}
	}

	private void Relay_SaveEvent_4()
	{
		Relay_Save_60();
	}

	private void Relay_LoadEvent_4()
	{
		Relay_Load_60();
	}

	private void Relay_RestartEvent_4()
	{
		Relay_Restart_60();
	}

	private void Relay_Save_Out_8()
	{
		Relay_Save_44();
	}

	private void Relay_Load_Out_8()
	{
		Relay_Load_44();
	}

	private void Relay_Restart_Out_8()
	{
		Relay_Set_False_44();
	}

	private void Relay_Save_8()
	{
		logic_SubGraph_SaveLoadBool_boolean_8 = local_MiningInProgress_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_8 = local_MiningInProgress_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_8.Save(ref logic_SubGraph_SaveLoadBool_boolean_8, logic_SubGraph_SaveLoadBool_boolAsVariable_8, logic_SubGraph_SaveLoadBool_uniqueID_8);
	}

	private void Relay_Load_8()
	{
		logic_SubGraph_SaveLoadBool_boolean_8 = local_MiningInProgress_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_8 = local_MiningInProgress_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_8.Load(ref logic_SubGraph_SaveLoadBool_boolean_8, logic_SubGraph_SaveLoadBool_boolAsVariable_8, logic_SubGraph_SaveLoadBool_uniqueID_8);
	}

	private void Relay_Set_True_8()
	{
		logic_SubGraph_SaveLoadBool_boolean_8 = local_MiningInProgress_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_8 = local_MiningInProgress_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_8.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_8, logic_SubGraph_SaveLoadBool_boolAsVariable_8, logic_SubGraph_SaveLoadBool_uniqueID_8);
	}

	private void Relay_Set_False_8()
	{
		logic_SubGraph_SaveLoadBool_boolean_8 = local_MiningInProgress_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_8 = local_MiningInProgress_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_8.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_8, logic_SubGraph_SaveLoadBool_boolAsVariable_8, logic_SubGraph_SaveLoadBool_uniqueID_8);
	}

	private void Relay_In_10()
	{
		logic_uScript_LockTechStacks_tech_10 = local_CraftingBaseTech_Tank;
		logic_uScript_LockTechStacks_uScript_LockTechStacks_10.In(logic_uScript_LockTechStacks_tech_10);
		if (logic_uScript_LockTechStacks_uScript_LockTechStacks_10.Out)
		{
			Relay_In_104();
		}
	}

	private void Relay_In_11()
	{
		logic_uScript_LockTechInteraction_tech_11 = local_CraftingBaseTech_Tank;
		logic_uScript_LockTechInteraction_uScript_LockTechInteraction_11.In(logic_uScript_LockTechInteraction_tech_11, logic_uScript_LockTechInteraction_excludedBlocks_11);
		if (logic_uScript_LockTechInteraction_uScript_LockTechInteraction_11.Out)
		{
			Relay_In_10();
		}
	}

	private void Relay_In_13()
	{
		logic_uScriptCon_CompareBool_Bool_13 = local_msgSeamUncoveredShown_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_13.In(logic_uScriptCon_CompareBool_Bool_13);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_13.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_13.False;
		if (num)
		{
			Relay_In_33();
		}
		if (flag)
		{
			Relay_In_143();
		}
	}

	private void Relay_True_15()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_15.True(out logic_uScriptAct_SetBool_Target_15);
		local_msgSeamUncoveredShown_System_Boolean = logic_uScriptAct_SetBool_Target_15;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_15.Out)
		{
			Relay_In_33();
		}
	}

	private void Relay_False_15()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_15.False(out logic_uScriptAct_SetBool_Target_15);
		local_msgSeamUncoveredShown_System_Boolean = logic_uScriptAct_SetBool_Target_15;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_15.Out)
		{
			Relay_In_33();
		}
	}

	private void Relay_Save_Out_18()
	{
		Relay_Save_158();
	}

	private void Relay_Load_Out_18()
	{
		Relay_Load_158();
	}

	private void Relay_Restart_Out_18()
	{
		Relay_Set_False_158();
	}

	private void Relay_Save_18()
	{
		logic_SubGraph_SaveLoadBool_boolean_18 = local_msgBaseFoundShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_18 = local_msgBaseFoundShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_18.Save(ref logic_SubGraph_SaveLoadBool_boolean_18, logic_SubGraph_SaveLoadBool_boolAsVariable_18, logic_SubGraph_SaveLoadBool_uniqueID_18);
	}

	private void Relay_Load_18()
	{
		logic_SubGraph_SaveLoadBool_boolean_18 = local_msgBaseFoundShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_18 = local_msgBaseFoundShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_18.Load(ref logic_SubGraph_SaveLoadBool_boolean_18, logic_SubGraph_SaveLoadBool_boolAsVariable_18, logic_SubGraph_SaveLoadBool_uniqueID_18);
	}

	private void Relay_Set_True_18()
	{
		logic_SubGraph_SaveLoadBool_boolean_18 = local_msgBaseFoundShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_18 = local_msgBaseFoundShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_18.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_18, logic_SubGraph_SaveLoadBool_boolAsVariable_18, logic_SubGraph_SaveLoadBool_uniqueID_18);
	}

	private void Relay_Set_False_18()
	{
		logic_SubGraph_SaveLoadBool_boolean_18 = local_msgBaseFoundShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_18 = local_msgBaseFoundShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_18.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_18, logic_SubGraph_SaveLoadBool_boolAsVariable_18, logic_SubGraph_SaveLoadBool_uniqueID_18);
	}

	private void Relay_Pause_19()
	{
		logic_uScript_PausePopulation_uScript_PausePopulation_19.Pause();
		if (logic_uScript_PausePopulation_uScript_PausePopulation_19.Out)
		{
			Relay_True_23();
		}
	}

	private void Relay_UnPause_19()
	{
		logic_uScript_PausePopulation_uScript_PausePopulation_19.UnPause();
		if (logic_uScript_PausePopulation_uScript_PausePopulation_19.Out)
		{
			Relay_True_23();
		}
	}

	private void Relay_Pause_21()
	{
		logic_uScript_PausePopulation_uScript_PausePopulation_21.Pause();
		if (logic_uScript_PausePopulation_uScript_PausePopulation_21.Out)
		{
			Relay_In_24();
		}
	}

	private void Relay_UnPause_21()
	{
		logic_uScript_PausePopulation_uScript_PausePopulation_21.UnPause();
		if (logic_uScript_PausePopulation_uScript_PausePopulation_21.Out)
		{
			Relay_In_24();
		}
	}

	private void Relay_In_22()
	{
		logic_uScript_HideArrow_uScript_HideArrow_22.In();
		if (logic_uScript_HideArrow_uScript_HideArrow_22.Out)
		{
			Relay_In_190();
		}
	}

	private void Relay_True_23()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_23.True(out logic_uScriptAct_SetBool_Target_23);
		local_NearBase_System_Boolean = logic_uScriptAct_SetBool_Target_23;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_23.Out)
		{
			Relay_In_59();
		}
	}

	private void Relay_False_23()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_23.False(out logic_uScriptAct_SetBool_Target_23);
		local_NearBase_System_Boolean = logic_uScriptAct_SetBool_Target_23;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_23.Out)
		{
			Relay_In_59();
		}
	}

	private void Relay_In_24()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_24 = messageTag;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_24.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_24, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_24);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_24.Out)
		{
			Relay_In_22();
		}
	}

	private void Relay_True_26()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_26.True(out logic_uScriptAct_SetBool_Target_26);
		local_msgIntroShown_System_Boolean = logic_uScriptAct_SetBool_Target_26;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_26.Out)
		{
			Relay_In_70();
		}
	}

	private void Relay_False_26()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_26.False(out logic_uScriptAct_SetBool_Target_26);
		local_msgIntroShown_System_Boolean = logic_uScriptAct_SetBool_Target_26;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_26.Out)
		{
			Relay_In_70();
		}
	}

	private void Relay_In_27()
	{
		logic_uScript_IsPlayerInRangeOfTech_tech_27 = local_NPCTech_Tank;
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_27.In(logic_uScript_IsPlayerInRangeOfTech_tech_27, logic_uScript_IsPlayerInRangeOfTech_range_27, logic_uScript_IsPlayerInRangeOfTech_techs_27);
		if (logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_27.InRange)
		{
			Relay_In_29();
		}
	}

	private void Relay_In_29()
	{
		logic_uScriptCon_CompareBool_Bool_29 = local_msgIntroShown_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_29.In(logic_uScriptCon_CompareBool_Bool_29);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_29.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_29.False;
		if (num)
		{
			Relay_In_2();
		}
		if (flag)
		{
			Relay_True_26();
		}
	}

	private void Relay_True_31()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_31.True(out logic_uScriptAct_SetBool_Target_31);
		local_AutoMinerSpawned_System_Boolean = logic_uScriptAct_SetBool_Target_31;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_31.Out)
		{
			Relay_In_34();
		}
	}

	private void Relay_False_31()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_31.False(out logic_uScriptAct_SetBool_Target_31);
		local_AutoMinerSpawned_System_Boolean = logic_uScriptAct_SetBool_Target_31;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_31.Out)
		{
			Relay_In_34();
		}
	}

	private void Relay_In_33()
	{
		logic_uScriptCon_CompareBool_Bool_33 = local_AutoMinerSpawned_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_33.In(logic_uScriptCon_CompareBool_Bool_33);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_33.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_33.False;
		if (num)
		{
			Relay_In_39();
		}
		if (flag)
		{
			Relay_True_31();
		}
	}

	private void Relay_In_34()
	{
		int num = 0;
		Array array = blockSpawnDataAutoMiner;
		if (logic_uScript_SpawnBlocksFromData_blockData_34.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SpawnBlocksFromData_blockData_34, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SpawnBlocksFromData_blockData_34, num, array.Length);
		num += array.Length;
		logic_uScript_SpawnBlocksFromData_ownerNode_34 = owner_Connection_35;
		logic_uScript_SpawnBlocksFromData_uScript_SpawnBlocksFromData_34.In(logic_uScript_SpawnBlocksFromData_blockData_34, logic_uScript_SpawnBlocksFromData_ownerNode_34);
		if (logic_uScript_SpawnBlocksFromData_uScript_SpawnBlocksFromData_34.Out)
		{
			Relay_In_39();
		}
	}

	private void Relay_In_39()
	{
		int num = 0;
		Array array = blockSpawnDataAutoMiner;
		if (logic_uScript_GetAndCheckBlocks_blockData_39.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckBlocks_blockData_39, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckBlocks_blockData_39, num, array.Length);
		num += array.Length;
		logic_uScript_GetAndCheckBlocks_ownerNode_39 = owner_Connection_38;
		logic_uScript_GetAndCheckBlocks_uScript_GetAndCheckBlocks_39.In(logic_uScript_GetAndCheckBlocks_blockData_39, logic_uScript_GetAndCheckBlocks_ownerNode_39, ref logic_uScript_GetAndCheckBlocks_blocks_39);
		bool allAlive = logic_uScript_GetAndCheckBlocks_uScript_GetAndCheckBlocks_39.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckBlocks_uScript_GetAndCheckBlocks_39.SomeAlive;
		if (allAlive)
		{
			Relay_In_42();
		}
		if (someAlive)
		{
			Relay_In_42();
		}
	}

	private void Relay_In_42()
	{
		logic_uScriptCon_CompareBool_Bool_42 = local_msgAutoMinerSpawnedShown_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_42.In(logic_uScriptCon_CompareBool_Bool_42);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_42.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_42.False;
		if (num)
		{
			Relay_In_80();
		}
		if (flag)
		{
			Relay_In_77();
		}
	}

	private void Relay_True_43()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_43.True(out logic_uScriptAct_SetBool_Target_43);
		local_msgAutoMinerSpawnedShown_System_Boolean = logic_uScriptAct_SetBool_Target_43;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_43.Out)
		{
			Relay_In_123();
		}
	}

	private void Relay_False_43()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_43.False(out logic_uScriptAct_SetBool_Target_43);
		local_msgAutoMinerSpawnedShown_System_Boolean = logic_uScriptAct_SetBool_Target_43;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_43.Out)
		{
			Relay_In_123();
		}
	}

	private void Relay_Save_Out_44()
	{
		Relay_Save_18();
	}

	private void Relay_Load_Out_44()
	{
		Relay_Load_18();
	}

	private void Relay_Restart_Out_44()
	{
		Relay_Set_False_18();
	}

	private void Relay_Save_44()
	{
		logic_SubGraph_SaveLoadBool_boolean_44 = local_msgIntroShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_44 = local_msgIntroShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_44.Save(ref logic_SubGraph_SaveLoadBool_boolean_44, logic_SubGraph_SaveLoadBool_boolAsVariable_44, logic_SubGraph_SaveLoadBool_uniqueID_44);
	}

	private void Relay_Load_44()
	{
		logic_SubGraph_SaveLoadBool_boolean_44 = local_msgIntroShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_44 = local_msgIntroShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_44.Load(ref logic_SubGraph_SaveLoadBool_boolean_44, logic_SubGraph_SaveLoadBool_boolAsVariable_44, logic_SubGraph_SaveLoadBool_uniqueID_44);
	}

	private void Relay_Set_True_44()
	{
		logic_SubGraph_SaveLoadBool_boolean_44 = local_msgIntroShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_44 = local_msgIntroShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_44.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_44, logic_SubGraph_SaveLoadBool_boolAsVariable_44, logic_SubGraph_SaveLoadBool_uniqueID_44);
	}

	private void Relay_Set_False_44()
	{
		logic_SubGraph_SaveLoadBool_boolean_44 = local_msgIntroShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_44 = local_msgIntroShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_44.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_44, logic_SubGraph_SaveLoadBool_boolAsVariable_44, logic_SubGraph_SaveLoadBool_uniqueID_44);
	}

	private void Relay_Save_Out_47()
	{
		Relay_Save_8();
	}

	private void Relay_Load_Out_47()
	{
		Relay_Load_8();
	}

	private void Relay_Restart_Out_47()
	{
		Relay_Set_False_8();
	}

	private void Relay_Save_47()
	{
		logic_SubGraph_SaveLoadBool_boolean_47 = local_AutoMinerSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_47 = local_AutoMinerSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_47.Save(ref logic_SubGraph_SaveLoadBool_boolean_47, logic_SubGraph_SaveLoadBool_boolAsVariable_47, logic_SubGraph_SaveLoadBool_uniqueID_47);
	}

	private void Relay_Load_47()
	{
		logic_SubGraph_SaveLoadBool_boolean_47 = local_AutoMinerSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_47 = local_AutoMinerSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_47.Load(ref logic_SubGraph_SaveLoadBool_boolean_47, logic_SubGraph_SaveLoadBool_boolAsVariable_47, logic_SubGraph_SaveLoadBool_uniqueID_47);
	}

	private void Relay_Set_True_47()
	{
		logic_SubGraph_SaveLoadBool_boolean_47 = local_AutoMinerSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_47 = local_AutoMinerSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_47.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_47, logic_SubGraph_SaveLoadBool_boolAsVariable_47, logic_SubGraph_SaveLoadBool_uniqueID_47);
	}

	private void Relay_Set_False_47()
	{
		logic_SubGraph_SaveLoadBool_boolean_47 = local_AutoMinerSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_47 = local_AutoMinerSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_47.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_47, logic_SubGraph_SaveLoadBool_boolAsVariable_47, logic_SubGraph_SaveLoadBool_uniqueID_47);
	}

	private void Relay_Save_Out_49()
	{
	}

	private void Relay_Load_Out_49()
	{
		Relay_In_62();
	}

	private void Relay_Restart_Out_49()
	{
		Relay_False_51();
	}

	private void Relay_Save_49()
	{
		logic_SubGraph_SaveLoadBool_boolean_49 = local_msgAutoMinerSpawnedShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_49 = local_msgAutoMinerSpawnedShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_49.Save(ref logic_SubGraph_SaveLoadBool_boolean_49, logic_SubGraph_SaveLoadBool_boolAsVariable_49, logic_SubGraph_SaveLoadBool_uniqueID_49);
	}

	private void Relay_Load_49()
	{
		logic_SubGraph_SaveLoadBool_boolean_49 = local_msgAutoMinerSpawnedShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_49 = local_msgAutoMinerSpawnedShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_49.Load(ref logic_SubGraph_SaveLoadBool_boolean_49, logic_SubGraph_SaveLoadBool_boolAsVariable_49, logic_SubGraph_SaveLoadBool_uniqueID_49);
	}

	private void Relay_Set_True_49()
	{
		logic_SubGraph_SaveLoadBool_boolean_49 = local_msgAutoMinerSpawnedShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_49 = local_msgAutoMinerSpawnedShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_49.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_49, logic_SubGraph_SaveLoadBool_boolAsVariable_49, logic_SubGraph_SaveLoadBool_uniqueID_49);
	}

	private void Relay_Set_False_49()
	{
		logic_SubGraph_SaveLoadBool_boolean_49 = local_msgAutoMinerSpawnedShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_49 = local_msgAutoMinerSpawnedShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_49.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_49, logic_SubGraph_SaveLoadBool_boolAsVariable_49, logic_SubGraph_SaveLoadBool_uniqueID_49);
	}

	private void Relay_True_51()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_51.True(out logic_uScriptAct_SetBool_Target_51);
		local_NearBase_System_Boolean = logic_uScriptAct_SetBool_Target_51;
	}

	private void Relay_False_51()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_51.False(out logic_uScriptAct_SetBool_Target_51);
		local_NearBase_System_Boolean = logic_uScriptAct_SetBool_Target_51;
	}

	private void Relay_Out_53()
	{
		Relay_In_178();
	}

	private void Relay_In_53()
	{
		int num = 0;
		Array array = blockSpawnDataAutoMiner;
		if (logic_SubGraph_Crafting_Tutorial_ManageBlock_blockSpawnData_53.Length != num + array.Length)
		{
			Array.Resize(ref logic_SubGraph_Crafting_Tutorial_ManageBlock_blockSpawnData_53, num + array.Length);
		}
		Array.Copy(array, 0, logic_SubGraph_Crafting_Tutorial_ManageBlock_blockSpawnData_53, num, array.Length);
		num += array.Length;
		logic_SubGraph_Crafting_Tutorial_ManageBlock_block_53 = local_AutoMinerBlock_TankBlock;
		logic_SubGraph_Crafting_Tutorial_ManageBlock_msgBlockOutsideArea_53 = msgBlockOutsideArea;
		logic_SubGraph_Crafting_Tutorial_ManageBlock_messageSpeaker_53 = messageSpeaker;
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_53.In(ref logic_SubGraph_Crafting_Tutorial_ManageBlock_blockSpawnData_53, logic_SubGraph_Crafting_Tutorial_ManageBlock_blockIndex_53, ref logic_SubGraph_Crafting_Tutorial_ManageBlock_block_53, logic_SubGraph_Crafting_Tutorial_ManageBlock_allowAnchoring_53, logic_SubGraph_Crafting_Tutorial_ManageBlock_msgBlockOutsideArea_53, logic_SubGraph_Crafting_Tutorial_ManageBlock_messageSpeaker_53);
	}

	private void Relay_In_54()
	{
		logic_uScriptCon_CompareBool_Bool_54 = local_AutoMinerSpawned_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_54.In(logic_uScriptCon_CompareBool_Bool_54);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_54.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_54.False;
		if (num)
		{
			Relay_In_53();
		}
		if (flag)
		{
			Relay_In_57();
		}
	}

	private void Relay_In_57()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_57.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_57.Out)
		{
			Relay_In_108();
		}
	}

	private void Relay_Output1_59()
	{
		Relay_In_68();
	}

	private void Relay_Output2_59()
	{
		Relay_In_173();
	}

	private void Relay_Output3_59()
	{
		Relay_In_131();
	}

	private void Relay_Output4_59()
	{
	}

	private void Relay_Output5_59()
	{
	}

	private void Relay_Output6_59()
	{
	}

	private void Relay_Output7_59()
	{
	}

	private void Relay_Output8_59()
	{
	}

	private void Relay_In_59()
	{
		logic_uScriptCon_ManualSwitch_CurrentOutput_59 = local_Stage_System_Int32;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_59.In(logic_uScriptCon_ManualSwitch_CurrentOutput_59);
	}

	private void Relay_Save_Out_60()
	{
		Relay_Save_148();
	}

	private void Relay_Load_Out_60()
	{
		Relay_Load_148();
	}

	private void Relay_Restart_Out_60()
	{
		Relay_Set_False_148();
	}

	private void Relay_Save_60()
	{
		logic_SubGraph_SaveLoadInt_integer_60 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_60 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_60.Save(logic_SubGraph_SaveLoadInt_restartValue_60, ref logic_SubGraph_SaveLoadInt_integer_60, logic_SubGraph_SaveLoadInt_intAsVariable_60, logic_SubGraph_SaveLoadInt_uniqueID_60);
	}

	private void Relay_Load_60()
	{
		logic_SubGraph_SaveLoadInt_integer_60 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_60 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_60.Load(logic_SubGraph_SaveLoadInt_restartValue_60, ref logic_SubGraph_SaveLoadInt_integer_60, logic_SubGraph_SaveLoadInt_intAsVariable_60, logic_SubGraph_SaveLoadInt_uniqueID_60);
	}

	private void Relay_Restart_60()
	{
		logic_SubGraph_SaveLoadInt_integer_60 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_60 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_60.Restart(logic_SubGraph_SaveLoadInt_restartValue_60, ref logic_SubGraph_SaveLoadInt_integer_60, logic_SubGraph_SaveLoadInt_intAsVariable_60, logic_SubGraph_SaveLoadInt_uniqueID_60);
	}

	private void Relay_Out_62()
	{
		Relay_In_223();
	}

	private void Relay_In_62()
	{
		logic_SubGraph_LoadObjectiveStates_currentObjective_62 = local_Stage_System_Int32;
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_62.In(logic_SubGraph_LoadObjectiveStates_currentObjective_62);
	}

	private void Relay_Out_68()
	{
		Relay_In_206();
	}

	private void Relay_In_68()
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_68 = local_Stage_System_Int32;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_68.In(ref logic_SubGraph_CompleteObjectiveStage_objectiveStage_68, logic_SubGraph_CompleteObjectiveStage_isFinalObjective_68);
	}

	private void Relay_In_70()
	{
		logic_uScript_AddMessage_messageData_70 = msg01Intro;
		logic_uScript_AddMessage_speaker_70 = messageSpeaker;
		logic_uScript_AddMessage_Return_70 = logic_uScript_AddMessage_uScript_AddMessage_70.In(logic_uScript_AddMessage_messageData_70, logic_uScript_AddMessage_speaker_70);
		if (logic_uScript_AddMessage_uScript_AddMessage_70.Out)
		{
			Relay_In_2();
		}
	}

	private void Relay_In_73()
	{
		logic_uScript_AddMessage_messageData_73 = msg04aSeamUncoveredEarly;
		logic_uScript_AddMessage_speaker_73 = messageSpeaker;
		logic_uScript_AddMessage_Return_73 = logic_uScript_AddMessage_uScript_AddMessage_73.In(logic_uScript_AddMessage_messageData_73, logic_uScript_AddMessage_speaker_73);
		if (logic_uScript_AddMessage_uScript_AddMessage_73.Shown)
		{
			Relay_True_15();
		}
	}

	private void Relay_In_77()
	{
		logic_uScript_AddMessage_messageData_77 = msg05AutoMinerSpawned;
		logic_uScript_AddMessage_speaker_77 = messageSpeaker;
		logic_uScript_AddMessage_Return_77 = logic_uScript_AddMessage_uScript_AddMessage_77.In(logic_uScript_AddMessage_messageData_77, logic_uScript_AddMessage_speaker_77);
		if (logic_uScript_AddMessage_uScript_AddMessage_77.Shown)
		{
			Relay_True_43();
		}
	}

	private void Relay_In_80()
	{
		logic_uScript_AddMessage_messageData_80 = msg06AnchorAutoMiner;
		logic_uScript_AddMessage_speaker_80 = messageSpeaker;
		logic_uScript_AddMessage_Return_80 = logic_uScript_AddMessage_uScript_AddMessage_80.In(logic_uScript_AddMessage_messageData_80, logic_uScript_AddMessage_speaker_80);
		if (logic_uScript_AddMessage_uScript_AddMessage_80.Out)
		{
			Relay_In_123();
		}
	}

	private void Relay_In_84()
	{
		logic_uScriptCon_CompareBool_Bool_84 = local_NearBase_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_84.In(logic_uScriptCon_CompareBool_Bool_84);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_84.True)
		{
			Relay_In_89();
		}
	}

	private void Relay_True_85()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_85.True(out logic_uScriptAct_SetBool_Target_85);
		local_NearBase_System_Boolean = logic_uScriptAct_SetBool_Target_85;
	}

	private void Relay_False_85()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_85.False(out logic_uScriptAct_SetBool_Target_85);
		local_NearBase_System_Boolean = logic_uScriptAct_SetBool_Target_85;
	}

	private void Relay_In_89()
	{
		logic_uScript_AddMessage_messageData_89 = msgLeavingMissionArea;
		logic_uScript_AddMessage_speaker_89 = messageSpeaker;
		logic_uScript_AddMessage_Return_89 = logic_uScript_AddMessage_uScript_AddMessage_89.In(logic_uScript_AddMessage_messageData_89, logic_uScript_AddMessage_speaker_89);
		if (logic_uScript_AddMessage_uScript_AddMessage_89.Out)
		{
			Relay_False_85();
		}
	}

	private void Relay_Out_90()
	{
	}

	private void Relay_In_90()
	{
		logic_SubGraph_Crafting_Tutorial_Finish_craftingBaseTech_90 = local_CraftingBaseTech_Tank;
		logic_SubGraph_Crafting_Tutorial_Finish_NPCTech_90 = local_NPCTech_Tank;
		logic_SubGraph_Crafting_Tutorial_Finish_NPCFlyAwayAI_90 = NPCFlyAwayAI;
		logic_SubGraph_Crafting_Tutorial_Finish_NPCDespawnParticleEffect_90 = NPCDespawnParticleEffect;
		logic_SubGraph_Crafting_Tutorial_Finish_SubGraph_Crafting_Tutorial_Finish_90.In(logic_SubGraph_Crafting_Tutorial_Finish_craftingBaseTech_90, logic_SubGraph_Crafting_Tutorial_Finish_NPCTech_90, logic_SubGraph_Crafting_Tutorial_Finish_NPCFlyAwayAI_90, logic_SubGraph_Crafting_Tutorial_Finish_NPCDespawnParticleEffect_90);
	}

	private void Relay_Out_99()
	{
		Relay_In_167();
	}

	private void Relay_In_99()
	{
		int num = 0;
		Array array = blockSpawnData;
		if (logic_SubGraph_Crafting_Tutorial_Init_blockSpawnData_99.Length != num + array.Length)
		{
			Array.Resize(ref logic_SubGraph_Crafting_Tutorial_Init_blockSpawnData_99, num + array.Length);
		}
		Array.Copy(array, 0, logic_SubGraph_Crafting_Tutorial_Init_blockSpawnData_99, num, array.Length);
		num += array.Length;
		int num2 = 0;
		Array nPCSpawnData = NPCSpawnData;
		if (logic_SubGraph_Crafting_Tutorial_Init_NPCSpawnData_99.Length != num2 + nPCSpawnData.Length)
		{
			Array.Resize(ref logic_SubGraph_Crafting_Tutorial_Init_NPCSpawnData_99, num2 + nPCSpawnData.Length);
		}
		Array.Copy(nPCSpawnData, 0, logic_SubGraph_Crafting_Tutorial_Init_NPCSpawnData_99, num2, nPCSpawnData.Length);
		num2 += nPCSpawnData.Length;
		logic_SubGraph_Crafting_Tutorial_Init_basePosition_99 = basePosition;
		logic_SubGraph_Crafting_Tutorial_Init_clearSceneryRadius_99 = clearSceneryRadius;
		logic_SubGraph_Crafting_Tutorial_Init_NPCTech_99 = local_NPCTech_Tank;
		logic_SubGraph_Crafting_Tutorial_Init_SubGraph_Crafting_Tutorial_Init_99.In(logic_SubGraph_Crafting_Tutorial_Init_baseSpawnData_99, logic_SubGraph_Crafting_Tutorial_Init_blockSpawnData_99, logic_SubGraph_Crafting_Tutorial_Init_NPCSpawnData_99, logic_SubGraph_Crafting_Tutorial_Init_completedBasePreset_99, logic_SubGraph_Crafting_Tutorial_Init_useNPCAsTutorialTech_99, logic_SubGraph_Crafting_Tutorial_Init_spawnBase_99, logic_SubGraph_Crafting_Tutorial_Init_basePosition_99, logic_SubGraph_Crafting_Tutorial_Init_clearSceneryRadius_99, ref logic_SubGraph_Crafting_Tutorial_Init_CraftingBaseTech_99, ref logic_SubGraph_Crafting_Tutorial_Init_NPCTech_99);
	}

	private void Relay_In_104()
	{
		logic_uScript_SetEncounterTarget_owner_104 = owner_Connection_103;
		logic_uScript_SetEncounterTarget_visibleObject_104 = local_NPCTech_Tank;
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_104.In(logic_uScript_SetEncounterTarget_owner_104, logic_uScript_SetEncounterTarget_visibleObject_104);
		if (logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_104.Out)
		{
			Relay_In_27();
		}
	}

	private void Relay_In_107()
	{
		logic_uScript_AddMessage_messageData_107 = msg07Complete;
		logic_uScript_AddMessage_speaker_107 = messageSpeaker;
		logic_uScript_AddMessage_Return_107 = logic_uScript_AddMessage_uScript_AddMessage_107.In(logic_uScript_AddMessage_messageData_107, logic_uScript_AddMessage_speaker_107);
		if (logic_uScript_AddMessage_uScript_AddMessage_107.Shown)
		{
			Relay_In_181();
		}
	}

	private void Relay_In_108()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_108.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_108.Out)
		{
			Relay_In_104();
		}
	}

	private void Relay_In_109()
	{
		logic_uScript_IsResourceReservoir_resourceDispenser_109 = local_ResourceDispenser_ResourceDispenser;
		logic_uScript_IsResourceReservoir_uScript_IsResourceReservoir_109.In(logic_uScript_IsResourceReservoir_resourceDispenser_109);
		if (logic_uScript_IsResourceReservoir_uScript_IsResourceReservoir_109.True)
		{
			Relay_In_135();
		}
	}

	private void Relay_Out_111()
	{
	}

	private void Relay_In_111()
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_111 = local_Stage_System_Int32;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_111.In(ref logic_SubGraph_CompleteObjectiveStage_objectiveStage_111, logic_SubGraph_CompleteObjectiveStage_isFinalObjective_111);
	}

	private void Relay_True_114()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_114.True(out logic_uScriptAct_SetBool_Target_114);
		local_msgBaseFoundShown_System_Boolean = logic_uScriptAct_SetBool_Target_114;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_114.Out)
		{
			Relay_In_140();
		}
	}

	private void Relay_False_114()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_114.False(out logic_uScriptAct_SetBool_Target_114);
		local_msgBaseFoundShown_System_Boolean = logic_uScriptAct_SetBool_Target_114;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_114.Out)
		{
			Relay_In_140();
		}
	}

	private void Relay_In_117()
	{
		logic_uScriptCon_CompareBool_Bool_117 = local_msgBaseFoundShown_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_117.In(logic_uScriptCon_CompareBool_Bool_117);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_117.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_117.False;
		if (num)
		{
			Relay_In_149();
		}
		if (flag)
		{
			Relay_In_119();
		}
	}

	private void Relay_In_119()
	{
		logic_uScript_AddMessage_messageData_119 = msg02BaseFound;
		logic_uScript_AddMessage_speaker_119 = messageSpeaker;
		logic_uScript_AddMessage_Return_119 = logic_uScript_AddMessage_uScript_AddMessage_119.In(logic_uScript_AddMessage_messageData_119, logic_uScript_AddMessage_speaker_119);
		if (logic_uScript_AddMessage_uScript_AddMessage_119.Shown)
		{
			Relay_True_114();
		}
	}

	private void Relay_In_121()
	{
		logic_uScript_IsBlockMining_block_121 = local_AutoMinerBlock_TankBlock;
		logic_uScript_IsBlockMining_uScript_IsBlockMining_121.In(logic_uScript_IsBlockMining_block_121);
		if (logic_uScript_IsBlockMining_uScript_IsBlockMining_121.True)
		{
			Relay_In_207();
		}
	}

	private void Relay_In_123()
	{
		logic_uScript_IsPlayerInteractingWithBlock_block_123 = local_AutoMinerBlock_TankBlock;
		logic_uScript_IsPlayerInteractingWithBlock_uScript_IsPlayerInteractingWithBlock_123.In(logic_uScript_IsPlayerInteractingWithBlock_block_123);
		bool dragging = logic_uScript_IsPlayerInteractingWithBlock_uScript_IsPlayerInteractingWithBlock_123.Dragging;
		bool notDragging = logic_uScript_IsPlayerInteractingWithBlock_uScript_IsPlayerInteractingWithBlock_123.NotDragging;
		if (dragging)
		{
			Relay_In_126();
		}
		if (notDragging)
		{
			Relay_In_125();
		}
	}

	private void Relay_In_125()
	{
		logic_uScript_PointArrowAtBlock_block_125 = local_AutoMinerBlock_TankBlock;
		logic_uScript_PointArrowAtBlock_uScript_PointArrowAtBlock_125.In(logic_uScript_PointArrowAtBlock_block_125, logic_uScript_PointArrowAtBlock_timeToShowFor_125, logic_uScript_PointArrowAtBlock_offset_125);
		if (logic_uScript_PointArrowAtBlock_uScript_PointArrowAtBlock_125.Out)
		{
			Relay_In_186();
		}
	}

	private void Relay_In_126()
	{
		logic_uScript_PointArrowAtVisible_targetObject_126 = local_ResourceDispenser_ResourceDispenser;
		logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_126.In(logic_uScript_PointArrowAtVisible_targetObject_126, logic_uScript_PointArrowAtVisible_timeToShowFor_126, logic_uScript_PointArrowAtVisible_offset_126);
		if (logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_126.Out)
		{
			Relay_In_195();
		}
	}

	private void Relay_In_129()
	{
		logic_uScript_HideArrow_uScript_HideArrow_129.In();
		if (logic_uScript_HideArrow_uScript_HideArrow_129.Out)
		{
			Relay_In_188();
		}
	}

	private void Relay_In_131()
	{
		logic_uScriptCon_CompareBool_Bool_131 = local_MiningInProgress_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_131.In(logic_uScriptCon_CompareBool_Bool_131);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_131.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_131.False;
		if (num)
		{
			Relay_In_107();
		}
		if (flag)
		{
			Relay_In_13();
		}
	}

	private void Relay_True_132()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_132.True(out logic_uScriptAct_SetBool_Target_132);
		local_MiningInProgress_System_Boolean = logic_uScriptAct_SetBool_Target_132;
	}

	private void Relay_False_132()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_132.False(out logic_uScriptAct_SetBool_Target_132);
		local_MiningInProgress_System_Boolean = logic_uScriptAct_SetBool_Target_132;
	}

	private void Relay_In_135()
	{
		logic_uScript_PointArrowAtVisible_targetObject_135 = local_ResourceDispenser_ResourceDispenser;
		logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_135.In(logic_uScript_PointArrowAtVisible_targetObject_135, logic_uScript_PointArrowAtVisible_timeToShowFor_135, logic_uScript_PointArrowAtVisible_offset_135);
		if (logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_135.Out)
		{
			Relay_In_111();
		}
	}

	private void Relay_Save_Out_137()
	{
		Relay_Save_49();
	}

	private void Relay_Load_Out_137()
	{
		Relay_Load_49();
	}

	private void Relay_Restart_Out_137()
	{
		Relay_Set_False_49();
	}

	private void Relay_Save_137()
	{
		logic_SubGraph_SaveLoadBool_boolean_137 = local_msgSeamUncoveredShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_137 = local_msgSeamUncoveredShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_137.Save(ref logic_SubGraph_SaveLoadBool_boolean_137, logic_SubGraph_SaveLoadBool_boolAsVariable_137, logic_SubGraph_SaveLoadBool_uniqueID_137);
	}

	private void Relay_Load_137()
	{
		logic_SubGraph_SaveLoadBool_boolean_137 = local_msgSeamUncoveredShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_137 = local_msgSeamUncoveredShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_137.Load(ref logic_SubGraph_SaveLoadBool_boolean_137, logic_SubGraph_SaveLoadBool_boolAsVariable_137, logic_SubGraph_SaveLoadBool_uniqueID_137);
	}

	private void Relay_Set_True_137()
	{
		logic_SubGraph_SaveLoadBool_boolean_137 = local_msgSeamUncoveredShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_137 = local_msgSeamUncoveredShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_137.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_137, logic_SubGraph_SaveLoadBool_boolAsVariable_137, logic_SubGraph_SaveLoadBool_uniqueID_137);
	}

	private void Relay_Set_False_137()
	{
		logic_SubGraph_SaveLoadBool_boolean_137 = local_msgSeamUncoveredShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_137 = local_msgSeamUncoveredShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_137.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_137, logic_SubGraph_SaveLoadBool_boolAsVariable_137, logic_SubGraph_SaveLoadBool_uniqueID_137);
	}

	private void Relay_In_140()
	{
		logic_uScript_IsResourceReservoir_resourceDispenser_140 = local_ResourceDispenser_ResourceDispenser;
		logic_uScript_IsResourceReservoir_uScript_IsResourceReservoir_140.In(logic_uScript_IsResourceReservoir_resourceDispenser_140);
		bool num = logic_uScript_IsResourceReservoir_uScript_IsResourceReservoir_140.True;
		bool flag = logic_uScript_IsResourceReservoir_uScript_IsResourceReservoir_140.False;
		if (num)
		{
			Relay_True_141();
		}
		if (flag)
		{
			Relay_In_149();
		}
	}

	private void Relay_True_141()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_141.True(out logic_uScriptAct_SetBool_Target_141);
		local_SeamUncoveredEarly_System_Boolean = logic_uScriptAct_SetBool_Target_141;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_141.Out)
		{
			Relay_In_155();
		}
	}

	private void Relay_False_141()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_141.False(out logic_uScriptAct_SetBool_Target_141);
		local_SeamUncoveredEarly_System_Boolean = logic_uScriptAct_SetBool_Target_141;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_141.Out)
		{
			Relay_In_155();
		}
	}

	private void Relay_In_143()
	{
		logic_uScriptCon_CompareBool_Bool_143 = local_SeamUncoveredEarly_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_143.In(logic_uScriptCon_CompareBool_Bool_143);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_143.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_143.False;
		if (num)
		{
			Relay_In_73();
		}
		if (flag)
		{
			Relay_In_145();
		}
	}

	private void Relay_In_145()
	{
		logic_uScript_AddMessage_messageData_145 = msg04SeamUncovered;
		logic_uScript_AddMessage_speaker_145 = messageSpeaker;
		logic_uScript_AddMessage_Return_145 = logic_uScript_AddMessage_uScript_AddMessage_145.In(logic_uScript_AddMessage_messageData_145, logic_uScript_AddMessage_speaker_145);
		if (logic_uScript_AddMessage_uScript_AddMessage_145.Shown)
		{
			Relay_True_15();
		}
	}

	private void Relay_Save_Out_148()
	{
		Relay_Save_171();
	}

	private void Relay_Load_Out_148()
	{
		Relay_Load_171();
	}

	private void Relay_Restart_Out_148()
	{
		Relay_Set_False_171();
	}

	private void Relay_Save_148()
	{
		logic_SubGraph_SaveLoadBool_boolean_148 = local_SeamUncoveredEarly_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_148 = local_SeamUncoveredEarly_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_148.Save(ref logic_SubGraph_SaveLoadBool_boolean_148, logic_SubGraph_SaveLoadBool_boolAsVariable_148, logic_SubGraph_SaveLoadBool_uniqueID_148);
	}

	private void Relay_Load_148()
	{
		logic_SubGraph_SaveLoadBool_boolean_148 = local_SeamUncoveredEarly_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_148 = local_SeamUncoveredEarly_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_148.Load(ref logic_SubGraph_SaveLoadBool_boolean_148, logic_SubGraph_SaveLoadBool_boolAsVariable_148, logic_SubGraph_SaveLoadBool_uniqueID_148);
	}

	private void Relay_Set_True_148()
	{
		logic_SubGraph_SaveLoadBool_boolean_148 = local_SeamUncoveredEarly_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_148 = local_SeamUncoveredEarly_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_148.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_148, logic_SubGraph_SaveLoadBool_boolAsVariable_148, logic_SubGraph_SaveLoadBool_uniqueID_148);
	}

	private void Relay_Set_False_148()
	{
		logic_SubGraph_SaveLoadBool_boolean_148 = local_SeamUncoveredEarly_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_148 = local_SeamUncoveredEarly_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_148.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_148, logic_SubGraph_SaveLoadBool_boolAsVariable_148, logic_SubGraph_SaveLoadBool_uniqueID_148);
	}

	private void Relay_In_149()
	{
		logic_uScriptCon_CompareBool_Bool_149 = local_msgUncoverSeamShown_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_149.In(logic_uScriptCon_CompareBool_Bool_149);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_149.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_149.False;
		if (num)
		{
			Relay_In_109();
		}
		if (flag)
		{
			Relay_True_154();
		}
	}

	private void Relay_In_152()
	{
		logic_uScript_AddMessage_messageData_152 = msg03UncoverSeam;
		logic_uScript_AddMessage_speaker_152 = messageSpeaker;
		logic_uScript_AddMessage_Return_152 = logic_uScript_AddMessage_uScript_AddMessage_152.In(logic_uScript_AddMessage_messageData_152, logic_uScript_AddMessage_speaker_152);
		if (logic_uScript_AddMessage_uScript_AddMessage_152.Out)
		{
			Relay_In_109();
		}
	}

	private void Relay_True_154()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_154.True(out logic_uScriptAct_SetBool_Target_154);
		local_msgUncoverSeamShown_System_Boolean = logic_uScriptAct_SetBool_Target_154;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_154.Out)
		{
			Relay_In_152();
		}
	}

	private void Relay_False_154()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_154.False(out logic_uScriptAct_SetBool_Target_154);
		local_msgUncoverSeamShown_System_Boolean = logic_uScriptAct_SetBool_Target_154;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_154.Out)
		{
			Relay_In_152();
		}
	}

	private void Relay_In_155()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_155.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_155.Out)
		{
			Relay_In_156();
		}
	}

	private void Relay_In_156()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_156.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_156.Out)
		{
			Relay_In_111();
		}
	}

	private void Relay_Save_Out_158()
	{
		Relay_Save_137();
	}

	private void Relay_Load_Out_158()
	{
		Relay_Load_137();
	}

	private void Relay_Restart_Out_158()
	{
		Relay_Set_False_137();
	}

	private void Relay_Save_158()
	{
		logic_SubGraph_SaveLoadBool_boolean_158 = local_msgUncoverSeamShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_158 = local_msgUncoverSeamShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_158.Save(ref logic_SubGraph_SaveLoadBool_boolean_158, logic_SubGraph_SaveLoadBool_boolAsVariable_158, logic_SubGraph_SaveLoadBool_uniqueID_158);
	}

	private void Relay_Load_158()
	{
		logic_SubGraph_SaveLoadBool_boolean_158 = local_msgUncoverSeamShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_158 = local_msgUncoverSeamShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_158.Load(ref logic_SubGraph_SaveLoadBool_boolean_158, logic_SubGraph_SaveLoadBool_boolAsVariable_158, logic_SubGraph_SaveLoadBool_uniqueID_158);
	}

	private void Relay_Set_True_158()
	{
		logic_SubGraph_SaveLoadBool_boolean_158 = local_msgUncoverSeamShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_158 = local_msgUncoverSeamShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_158.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_158, logic_SubGraph_SaveLoadBool_boolAsVariable_158, logic_SubGraph_SaveLoadBool_uniqueID_158);
	}

	private void Relay_Set_False_158()
	{
		logic_SubGraph_SaveLoadBool_boolean_158 = local_msgUncoverSeamShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_158 = local_msgUncoverSeamShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_158.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_158, logic_SubGraph_SaveLoadBool_boolAsVariable_158, logic_SubGraph_SaveLoadBool_uniqueID_158);
	}

	private void Relay_In_167()
	{
		logic_uScriptCon_CompareBool_Bool_167 = local_ScenerySpawned_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_167.In(logic_uScriptCon_CompareBool_Bool_167);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_167.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_167.False;
		if (num)
		{
			Relay_In_54();
		}
		if (flag)
		{
			Relay_True_168();
		}
	}

	private void Relay_True_168()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_168.True(out logic_uScriptAct_SetBool_Target_168);
		local_ScenerySpawned_System_Boolean = logic_uScriptAct_SetBool_Target_168;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_168.Out)
		{
			Relay_In_218();
		}
	}

	private void Relay_False_168()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_168.False(out logic_uScriptAct_SetBool_Target_168);
		local_ScenerySpawned_System_Boolean = logic_uScriptAct_SetBool_Target_168;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_168.Out)
		{
			Relay_In_218();
		}
	}

	private void Relay_Save_Out_171()
	{
		Relay_Save_47();
	}

	private void Relay_Load_Out_171()
	{
		Relay_Load_47();
	}

	private void Relay_Restart_Out_171()
	{
		Relay_Set_False_47();
	}

	private void Relay_Save_171()
	{
		logic_SubGraph_SaveLoadBool_boolean_171 = local_ScenerySpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_171 = local_ScenerySpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_171.Save(ref logic_SubGraph_SaveLoadBool_boolean_171, logic_SubGraph_SaveLoadBool_boolAsVariable_171, logic_SubGraph_SaveLoadBool_uniqueID_171);
	}

	private void Relay_Load_171()
	{
		logic_SubGraph_SaveLoadBool_boolean_171 = local_ScenerySpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_171 = local_ScenerySpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_171.Load(ref logic_SubGraph_SaveLoadBool_boolean_171, logic_SubGraph_SaveLoadBool_boolAsVariable_171, logic_SubGraph_SaveLoadBool_uniqueID_171);
	}

	private void Relay_Set_True_171()
	{
		logic_SubGraph_SaveLoadBool_boolean_171 = local_ScenerySpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_171 = local_ScenerySpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_171.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_171, logic_SubGraph_SaveLoadBool_boolAsVariable_171, logic_SubGraph_SaveLoadBool_uniqueID_171);
	}

	private void Relay_Set_False_171()
	{
		logic_SubGraph_SaveLoadBool_boolean_171 = local_ScenerySpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_171 = local_ScenerySpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_171.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_171, logic_SubGraph_SaveLoadBool_boolAsVariable_171, logic_SubGraph_SaveLoadBool_uniqueID_171);
	}

	private void Relay_In_173()
	{
		logic_uScript_GetSpawnedScenery_ownerNode_173 = owner_Connection_174;
		logic_uScript_GetSpawnedScenery_uniqueSceneryName_173 = local_ResourceSeamID_System_String;
		logic_uScript_GetSpawnedScenery_Return_173 = logic_uScript_GetSpawnedScenery_uScript_GetSpawnedScenery_173.In(logic_uScript_GetSpawnedScenery_ownerNode_173, logic_uScript_GetSpawnedScenery_uniqueSceneryName_173);
		local_ResourceDispenser_ResourceDispenser = logic_uScript_GetSpawnedScenery_Return_173;
		if (logic_uScript_GetSpawnedScenery_uScript_GetSpawnedScenery_173.Out)
		{
			Relay_In_117();
		}
	}

	private void Relay_In_178()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_178.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_178.Out)
		{
			Relay_In_104();
		}
	}

	private void Relay_Succeed_179()
	{
		logic_uScript_FinishEncounter_owner_179 = owner_Connection_180;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_179.Succeed(logic_uScript_FinishEncounter_owner_179);
		if (logic_uScript_FinishEncounter_uScript_FinishEncounter_179.Out)
		{
			Relay_In_205();
		}
	}

	private void Relay_Fail_179()
	{
		logic_uScript_FinishEncounter_owner_179 = owner_Connection_180;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_179.Fail(logic_uScript_FinishEncounter_owner_179);
		if (logic_uScript_FinishEncounter_uScript_FinishEncounter_179.Out)
		{
			Relay_In_205();
		}
	}

	private void Relay_In_181()
	{
		logic_uScript_FlyTechUpAndAway_tech_181 = local_NPCTech_Tank;
		logic_uScript_FlyTechUpAndAway_aiTree_181 = NPCFlyAwayAI;
		logic_uScript_FlyTechUpAndAway_removalParticles_181 = NPCDespawnParticleEffect;
		logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_181.In(logic_uScript_FlyTechUpAndAway_tech_181, logic_uScript_FlyTechUpAndAway_maxLifetime_181, logic_uScript_FlyTechUpAndAway_targetHeight_181, logic_uScript_FlyTechUpAndAway_aiTree_181, logic_uScript_FlyTechUpAndAway_removalParticles_181);
		if (logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_181.Out)
		{
			Relay_Succeed_179();
		}
	}

	private void Relay_In_186()
	{
		logic_uScript_EnableGlow_targetObject_186 = local_AutoMinerBlock_TankBlock;
		logic_uScript_EnableGlow_uScript_EnableGlow_186.In(logic_uScript_EnableGlow_targetObject_186, logic_uScript_EnableGlow_enable_186);
		if (logic_uScript_EnableGlow_uScript_EnableGlow_186.Out)
		{
			Relay_In_200();
		}
	}

	private void Relay_In_188()
	{
		logic_uScript_EnableGlow_targetObject_188 = local_AutoMinerBlock_TankBlock;
		logic_uScript_EnableGlow_uScript_EnableGlow_188.In(logic_uScript_EnableGlow_targetObject_188, logic_uScript_EnableGlow_enable_188);
		if (logic_uScript_EnableGlow_uScript_EnableGlow_188.Out)
		{
			Relay_In_201();
		}
	}

	private void Relay_In_190()
	{
		logic_uScriptCon_CompareBool_Bool_190 = local_AutoMinerSpawned_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_190.In(logic_uScriptCon_CompareBool_Bool_190);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_190.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_190.False;
		if (num)
		{
			Relay_In_193();
		}
		if (flag)
		{
			Relay_In_194();
		}
	}

	private void Relay_In_193()
	{
		logic_uScript_EnableGlow_targetObject_193 = local_AutoMinerBlock_TankBlock;
		logic_uScript_EnableGlow_uScript_EnableGlow_193.In(logic_uScript_EnableGlow_targetObject_193, logic_uScript_EnableGlow_enable_193);
		if (logic_uScript_EnableGlow_uScript_EnableGlow_193.Out)
		{
			Relay_In_204();
		}
	}

	private void Relay_In_194()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_194.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_194.Out)
		{
			Relay_In_84();
		}
	}

	private void Relay_In_195()
	{
		logic_uScript_EnableGlow_targetObject_195 = local_ResourceDispenser_ResourceDispenser;
		logic_uScript_EnableGlow_uScript_EnableGlow_195.In(logic_uScript_EnableGlow_targetObject_195, logic_uScript_EnableGlow_enable_195);
		if (logic_uScript_EnableGlow_uScript_EnableGlow_195.Out)
		{
			Relay_In_197();
		}
	}

	private void Relay_In_197()
	{
		logic_uScript_EnableGlow_targetObject_197 = local_AutoMinerBlock_TankBlock;
		logic_uScript_EnableGlow_uScript_EnableGlow_197.In(logic_uScript_EnableGlow_targetObject_197, logic_uScript_EnableGlow_enable_197);
		if (logic_uScript_EnableGlow_uScript_EnableGlow_197.Out)
		{
			Relay_In_121();
		}
	}

	private void Relay_In_200()
	{
		logic_uScript_EnableGlow_targetObject_200 = local_ResourceDispenser_ResourceDispenser;
		logic_uScript_EnableGlow_uScript_EnableGlow_200.In(logic_uScript_EnableGlow_targetObject_200, logic_uScript_EnableGlow_enable_200);
		if (logic_uScript_EnableGlow_uScript_EnableGlow_200.Out)
		{
			Relay_In_121();
		}
	}

	private void Relay_In_201()
	{
		logic_uScript_EnableGlow_targetObject_201 = local_ResourceDispenser_ResourceDispenser;
		logic_uScript_EnableGlow_uScript_EnableGlow_201.In(logic_uScript_EnableGlow_targetObject_201, logic_uScript_EnableGlow_enable_201);
		if (logic_uScript_EnableGlow_uScript_EnableGlow_201.Out)
		{
			Relay_True_132();
		}
	}

	private void Relay_In_204()
	{
		logic_uScript_EnableGlow_targetObject_204 = local_ResourceDispenser_ResourceDispenser;
		logic_uScript_EnableGlow_uScript_EnableGlow_204.In(logic_uScript_EnableGlow_targetObject_204, logic_uScript_EnableGlow_enable_204);
		if (logic_uScript_EnableGlow_uScript_EnableGlow_204.Out)
		{
			Relay_In_84();
		}
	}

	private void Relay_In_205()
	{
		logic_uScript_SendAnaliticsEvent_uScript_SendAnaliticsEvent_205.In(logic_uScript_SendAnaliticsEvent_analiticsEvent_205, logic_uScript_SendAnaliticsEvent_parameterName_205, logic_uScript_SendAnaliticsEvent_parameter_205);
	}

	private void Relay_In_206()
	{
		logic_uScript_SendAnaliticsEvent_uScript_SendAnaliticsEvent_206.In(logic_uScript_SendAnaliticsEvent_analiticsEvent_206, logic_uScript_SendAnaliticsEvent_parameterName_206, logic_uScript_SendAnaliticsEvent_parameter_206);
	}

	private void Relay_In_207()
	{
		logic_uScript_GetPlayerTankWithBlock_tankBlock_207 = local_AutoMinerBlock_TankBlock;
		logic_uScript_GetPlayerTankWithBlock_Return_207 = logic_uScript_GetPlayerTankWithBlock_uScript_GetPlayerTankWithBlock_207.In(logic_uScript_GetPlayerTankWithBlock_block_207, logic_uScript_GetPlayerTankWithBlock_tankBlock_207, logic_uScript_GetPlayerTankWithBlock_useBlockType_207);
		local_209_Tank = logic_uScript_GetPlayerTankWithBlock_Return_207;
		if (logic_uScript_GetPlayerTankWithBlock_uScript_GetPlayerTankWithBlock_207.Returned)
		{
			Relay_In_210();
		}
	}

	private void Relay_In_210()
	{
		logic_uScript_GetPositionOfTech_tech_210 = local_209_Tank;
		logic_uScript_GetPositionOfTech_Return_210 = logic_uScript_GetPositionOfTech_uScript_GetPositionOfTech_210.In(logic_uScript_GetPositionOfTech_tech_210);
		local_AutoMinerPos_UnityEngine_Vector3 = logic_uScript_GetPositionOfTech_Return_210;
		if (logic_uScript_GetPositionOfTech_uScript_GetPositionOfTech_210.TechValid)
		{
			Relay_In_489();
		}
	}

	private void Relay_In_211()
	{
		logic_uScriptAct_GetVector3Distance_A_211 = local_AutoMinerPos_UnityEngine_Vector3;
		logic_uScriptAct_GetVector3Distance_B_211 = local_ResourceSeamPos_UnityEngine_Vector3;
		logic_uScriptAct_GetVector3Distance_uScriptAct_GetVector3Distance_211.In(logic_uScriptAct_GetVector3Distance_A_211, logic_uScriptAct_GetVector3Distance_B_211, out logic_uScriptAct_GetVector3Distance_Distance_211);
		local_215_System_Single = logic_uScriptAct_GetVector3Distance_Distance_211;
		if (logic_uScriptAct_GetVector3Distance_uScriptAct_GetVector3Distance_211.Out)
		{
			Relay_In_214();
		}
	}

	private void Relay_In_214()
	{
		logic_uScriptCon_CompareFloat_A_214 = local_215_System_Single;
		logic_uScriptCon_CompareFloat_uScriptCon_CompareFloat_214.In(logic_uScriptCon_CompareFloat_A_214, logic_uScriptCon_CompareFloat_B_214);
		if (logic_uScriptCon_CompareFloat_uScriptCon_CompareFloat_214.LessThanOrEqualTo)
		{
			Relay_In_129();
		}
	}

	private void Relay_In_218()
	{
		logic_uScript_SpawnScenery_ownerNode_218 = owner_Connection_159;
		logic_uScript_SpawnScenery_sceneryPrefab_218 = resourceSeamPrefab;
		logic_uScript_SpawnScenery_uniqueName_218 = local_ResourceSeamID_System_String;
		logic_uScript_SpawnScenery_maxSpawnRange_218 = scenerySpawnDistanceMax;
		logic_uScript_SpawnScenery_minSpawnRange_218 = scenerySpawnDistanceMin;
		logic_uScript_SpawnScenery_uScript_SpawnScenery_218.In(logic_uScript_SpawnScenery_ownerNode_218, logic_uScript_SpawnScenery_sceneryPrefab_218, logic_uScript_SpawnScenery_posName_218, logic_uScript_SpawnScenery_uniqueName_218, out logic_uScript_SpawnScenery_spawnPositions_218, logic_uScript_SpawnScenery_spawnNum_218, logic_uScript_SpawnScenery_maxSpawnRange_218, logic_uScript_SpawnScenery_minSpawnRange_218);
		if (logic_uScript_SpawnScenery_uScript_SpawnScenery_218.Out)
		{
			Relay_In_219();
		}
	}

	private void Relay_In_219()
	{
		logic_uScript_SpawnScenery_ownerNode_219 = owner_Connection_163;
		logic_uScript_SpawnScenery_sceneryPrefab_219 = sceneryPrefab;
		logic_uScript_SpawnScenery_spawnNum_219 = numSceneryObjectsToSpawn;
		logic_uScript_SpawnScenery_maxSpawnRange_219 = scenerySpawnDistanceMax;
		logic_uScript_SpawnScenery_minSpawnRange_219 = scenerySpawnDistanceMin;
		logic_uScript_SpawnScenery_uScript_SpawnScenery_219.In(logic_uScript_SpawnScenery_ownerNode_219, logic_uScript_SpawnScenery_sceneryPrefab_219, logic_uScript_SpawnScenery_posName_219, logic_uScript_SpawnScenery_uniqueName_219, out logic_uScript_SpawnScenery_spawnPositions_219, logic_uScript_SpawnScenery_spawnNum_219, logic_uScript_SpawnScenery_maxSpawnRange_219, logic_uScript_SpawnScenery_minSpawnRange_219);
		if (logic_uScript_SpawnScenery_uScript_SpawnScenery_219.Out)
		{
			Relay_In_54();
		}
	}

	private void Relay_In_223()
	{
		logic_uScript_GetSpawnedScenery_ownerNode_223 = owner_Connection_221;
		logic_uScript_GetSpawnedScenery_uniqueSceneryName_223 = local_ResourceSeamID_System_String;
		logic_uScript_GetSpawnedScenery_Return_223 = logic_uScript_GetSpawnedScenery_uScript_GetSpawnedScenery_223.In(logic_uScript_GetSpawnedScenery_ownerNode_223, logic_uScript_GetSpawnedScenery_uniqueSceneryName_223);
		local_ResourceDispenser_ResourceDispenser = logic_uScript_GetSpawnedScenery_Return_223;
	}

	private void Relay_In_489()
	{
		logic_uScript_GetPositionOfVisibleObject_visibleObject_489 = local_ResourceDispenser_ResourceDispenser;
		logic_uScript_GetPositionOfVisibleObject_Return_489 = logic_uScript_GetPositionOfVisibleObject_uScript_GetPositionOfVisibleObject_489.In(logic_uScript_GetPositionOfVisibleObject_visibleObject_489);
		local_ResourceSeamPos_UnityEngine_Vector3 = logic_uScript_GetPositionOfVisibleObject_Return_489;
		if (logic_uScript_GetPositionOfVisibleObject_uScript_GetPositionOfVisibleObject_489.Out)
		{
			Relay_In_211();
		}
	}
}
