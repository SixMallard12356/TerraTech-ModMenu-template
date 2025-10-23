using System;
using BehaviorDesigner.Runtime;
using UnityEngine;

[Serializable]
[FriendlyName("Untitled", "")]
[NodePath("Graphs")]
public class Mission_GSO_2_Crafting_01 : uScriptLogic
{
	private GameObject parentGameObject;

	private uScript_GUI thisScriptsOnGuiListener;

	private bool m_RegisteredForEvents;

	[Multiline(3)]
	public string basePosition = "";

	public SpawnTechData[] baseSpawnData = new SpawnTechData[0];

	public SpawnBlockData[] blockSpawnData = new SpawnBlockData[0];

	public SpawnBlockData[] blockSpawnDataRefinery = new SpawnBlockData[0];

	public BlockTypes blockTypeConveyor;

	public float clearSceneryRadius;

	public TankPreset completedBasePreset;

	public float distBaseFound;

	public GhostBlockSpawnData[] ghostBlockConveyor01 = new GhostBlockSpawnData[0];

	public GhostBlockSpawnData[] ghostBlockConveyor02 = new GhostBlockSpawnData[0];

	public GhostBlockSpawnData[] ghostBlockConveyor03 = new GhostBlockSpawnData[0];

	public GhostBlockSpawnData[] ghostBlockConveyor04 = new GhostBlockSpawnData[0];

	public GhostBlockSpawnData[] ghostBlockReceiver = new GhostBlockSpawnData[0];

	public GhostBlockSpawnData[] ghostBlockRefinery = new GhostBlockSpawnData[0];

	private TankBlock[] local_109_TankBlockArray = new TankBlock[0];

	private TankBlock[] local_115_TankBlockArray = new TankBlock[0];

	private ChunkTypes local_20_ChunkTypes;

	private TankBlock[] local_83_TankBlockArray = new TankBlock[0];

	private TankBlock[] local_97_TankBlockArray = new TankBlock[0];

	private TankBlock local_ConveyorBlock01_TankBlock;

	private TankBlock local_ConveyorBlock02_TankBlock;

	private TankBlock local_ConveyorBlock03_TankBlock;

	private TankBlock local_ConveyorBlock04_TankBlock;

	private Tank local_CraftingBaseTech_Tank;

	private bool local_DraggingConveyor_System_Boolean;

	private TankBlock local_GhostBlockConveyor01_TankBlock;

	private bool local_GhostBlockConveyor01Spawned_System_Boolean;

	private TankBlock local_GhostBlockConveyor02_TankBlock;

	private bool local_GhostBlockConveyor02Spawned_System_Boolean;

	private TankBlock local_GhostBlockConveyor03_TankBlock;

	private bool local_GhostBlockConveyor03Spawned_System_Boolean;

	private TankBlock local_GhostBlockConveyor04_TankBlock;

	private bool local_GhostBlockConveyor04Spawned_System_Boolean;

	private bool local_msgBaseFoundShown_System_Boolean;

	private bool local_msgConveyorsAttachedShown_System_Boolean;

	private bool local_msgIntroShown_System_Boolean;

	private bool local_msgReceiverAttachedShown_System_Boolean;

	private bool local_msgRefinerySpawnedShown_System_Boolean;

	private bool local_msgUnrefinedChunksSoldShown_System_Boolean;

	private bool local_NearBase_System_Boolean;

	private Tank local_NPCTech_Tank;

	private int local_NumConveyorsAttached_System_Int32;

	private TankBlock local_ReceiverBlock_TankBlock;

	private TankBlock local_RefineryBlock_TankBlock;

	private bool local_RefinerySpawned_System_Boolean;

	private bool local_Resources01Sold_System_Boolean;

	private bool local_Resources01Spawned_System_Boolean;

	private bool local_Resources02Sold_System_Boolean;

	private bool local_Resources02Spawned_System_Boolean;

	private int local_Stage_System_Int32 = 1;

	public uScript_AddMessage.MessageSpeaker messageSpeaker;

	[Multiline(3)]
	public string messageTag = "";

	public uScript_AddMessage.MessageData msg01Intro;

	public uScript_AddMessage.MessageData msg02BaseFound;

	public uScript_AddMessage.MessageData msg03AttachConveyors;

	public uScript_AddMessage.MessageData msg04ConveyorsAttached;

	public uScript_AddMessage.MessageData msg05AttachReceiver;

	public uScript_AddMessage.MessageData msg06ReceiverAttached;

	public uScript_AddMessage.MessageData msg07UnrefinedChunksSold;

	public uScript_AddMessage.MessageData msg08RefinerySpawned;

	public uScript_AddMessage.MessageData msg09AttachRefinery;

	public uScript_AddMessage.MessageData msg10RefineryAttached;

	public uScript_AddMessage.MessageData msg11Complete;

	public uScript_AddMessage.MessageData msgBlockOutsideArea;

	public uScript_AddMessage.MessageData msgLeavingMissionArea;

	public Transform NPCDespawnParticleEffect;

	public ExternalBehaviorTree NPCFlyAwayAI;

	public SpawnTechData[] NPCSpawnData = new SpawnTechData[0];

	public ChunkTypes[] resourceList = new ChunkTypes[0];

	public ChunkTypes resourceTypeRefined;

	public float timeWaitBeforeResourceSold;

	private GameObject owner_Connection_1;

	private GameObject owner_Connection_10;

	private GameObject owner_Connection_17;

	private GameObject owner_Connection_85;

	private GameObject owner_Connection_99;

	private GameObject owner_Connection_106;

	private GameObject owner_Connection_117;

	private GameObject owner_Connection_203;

	private GameObject owner_Connection_239;

	private GameObject owner_Connection_343;

	private uScript_IsPlayerInRangeOfTech logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_2 = new uScript_IsPlayerInRangeOfTech();

	private Tank logic_uScript_IsPlayerInRangeOfTech_tech_2;

	private float logic_uScript_IsPlayerInRangeOfTech_range_2;

	private Tank[] logic_uScript_IsPlayerInRangeOfTech_techs_2 = new Tank[0];

	private bool logic_uScript_IsPlayerInRangeOfTech_Out_2 = true;

	private bool logic_uScript_IsPlayerInRangeOfTech_InRange_2 = true;

	private bool logic_uScript_IsPlayerInRangeOfTech_OutOfRange_2 = true;

	private uScript_PausePopulation logic_uScript_PausePopulation_uScript_PausePopulation_3 = new uScript_PausePopulation();

	private bool logic_uScript_PausePopulation_Out_3 = true;

	private uScript_PausePopulation logic_uScript_PausePopulation_uScript_PausePopulation_4 = new uScript_PausePopulation();

	private bool logic_uScript_PausePopulation_Out_4 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_7 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_7;

	private bool logic_uScriptAct_SetBool_Out_7 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_7 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_7 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_11 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_11;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_11 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_11 = "Resources01Spawned";

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_15 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_15 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_15 = true;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_15 = true;

	private uScript_CompareChunkTypes logic_uScript_CompareChunkTypes_uScript_CompareChunkTypes_18 = new uScript_CompareChunkTypes();

	private ChunkTypes logic_uScript_CompareChunkTypes_A_18;

	private ChunkTypes logic_uScript_CompareChunkTypes_B_18;

	private bool logic_uScript_CompareChunkTypes_EqualTo_18 = true;

	private bool logic_uScript_CompareChunkTypes_NotEqualTo_18 = true;

	private SubGraph_Crafting_Tutorial_Init logic_SubGraph_Crafting_Tutorial_Init_SubGraph_Crafting_Tutorial_Init_21 = new SubGraph_Crafting_Tutorial_Init();

	private SpawnTechData[] logic_SubGraph_Crafting_Tutorial_Init_baseSpawnData_21 = new SpawnTechData[0];

	private SpawnBlockData[] logic_SubGraph_Crafting_Tutorial_Init_blockSpawnData_21 = new SpawnBlockData[0];

	private SpawnTechData[] logic_SubGraph_Crafting_Tutorial_Init_NPCSpawnData_21 = new SpawnTechData[0];

	private TankPreset logic_SubGraph_Crafting_Tutorial_Init_completedBasePreset_21;

	private bool logic_SubGraph_Crafting_Tutorial_Init_useNPCAsTutorialTech_21;

	private bool logic_SubGraph_Crafting_Tutorial_Init_spawnBase_21 = true;

	private string logic_SubGraph_Crafting_Tutorial_Init_basePosition_21 = "";

	private float logic_SubGraph_Crafting_Tutorial_Init_clearSceneryRadius_21;

	private Tank logic_SubGraph_Crafting_Tutorial_Init_CraftingBaseTech_21;

	private Tank logic_SubGraph_Crafting_Tutorial_Init_NPCTech_21;

	private uScript_LockTechStacks logic_uScript_LockTechStacks_uScript_LockTechStacks_28 = new uScript_LockTechStacks();

	private Tank logic_uScript_LockTechStacks_tech_28;

	private bool logic_uScript_LockTechStacks_Out_28 = true;

	private SubGraph_Crafting_Tutorial_AttachBlockToBase logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_SubGraph_Crafting_Tutorial_AttachBlockToBase_30 = new SubGraph_Crafting_Tutorial_AttachBlockToBase();

	private TankBlock logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_Block_30;

	private Tank logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_CraftingBaseTech_30;

	private GhostBlockSpawnData[] logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_ghostBlockData_30 = new GhostBlockSpawnData[0];

	private BlockTypes logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_BlockType_30 = BlockTypes.GSORefinery_222;

	private Vector3 logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_BlockPosition_30 = new Vector3(-3f, 0f, 1f);

	private uScript_LockTechInteraction logic_uScript_LockTechInteraction_uScript_LockTechInteraction_34 = new uScript_LockTechInteraction();

	private Tank logic_uScript_LockTechInteraction_tech_34;

	private BlockTypes[] logic_uScript_LockTechInteraction_excludedBlocks_34 = new BlockTypes[0];

	private TankBlock[] logic_uScript_LockTechInteraction_excludedUniqueBlocks_34 = new TankBlock[0];

	private bool logic_uScript_LockTechInteraction_Out_34 = true;

	private SubGraph_Crafting_Tutorial_ManageBlock logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_38 = new SubGraph_Crafting_Tutorial_ManageBlock();

	private SpawnBlockData[] logic_SubGraph_Crafting_Tutorial_ManageBlock_blockSpawnData_38 = new SpawnBlockData[0];

	private int logic_SubGraph_Crafting_Tutorial_ManageBlock_blockIndex_38;

	private TankBlock logic_SubGraph_Crafting_Tutorial_ManageBlock_block_38;

	private bool logic_SubGraph_Crafting_Tutorial_ManageBlock_allowAnchoring_38;

	private uScript_AddMessage.MessageData logic_SubGraph_Crafting_Tutorial_ManageBlock_msgBlockOutsideArea_38;

	private uScript_AddMessage.MessageSpeaker logic_SubGraph_Crafting_Tutorial_ManageBlock_messageSpeaker_38;

	private SubGraph_Crafting_Tutorial_ManageBlock logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_41 = new SubGraph_Crafting_Tutorial_ManageBlock();

	private SpawnBlockData[] logic_SubGraph_Crafting_Tutorial_ManageBlock_blockSpawnData_41 = new SpawnBlockData[0];

	private int logic_SubGraph_Crafting_Tutorial_ManageBlock_blockIndex_41 = 1;

	private TankBlock logic_SubGraph_Crafting_Tutorial_ManageBlock_block_41;

	private bool logic_SubGraph_Crafting_Tutorial_ManageBlock_allowAnchoring_41;

	private uScript_AddMessage.MessageData logic_SubGraph_Crafting_Tutorial_ManageBlock_msgBlockOutsideArea_41;

	private uScript_AddMessage.MessageSpeaker logic_SubGraph_Crafting_Tutorial_ManageBlock_messageSpeaker_41;

	private SubGraph_Crafting_Tutorial_ManageBlock logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_44 = new SubGraph_Crafting_Tutorial_ManageBlock();

	private SpawnBlockData[] logic_SubGraph_Crafting_Tutorial_ManageBlock_blockSpawnData_44 = new SpawnBlockData[0];

	private int logic_SubGraph_Crafting_Tutorial_ManageBlock_blockIndex_44 = 2;

	private TankBlock logic_SubGraph_Crafting_Tutorial_ManageBlock_block_44;

	private bool logic_SubGraph_Crafting_Tutorial_ManageBlock_allowAnchoring_44;

	private uScript_AddMessage.MessageData logic_SubGraph_Crafting_Tutorial_ManageBlock_msgBlockOutsideArea_44;

	private uScript_AddMessage.MessageSpeaker logic_SubGraph_Crafting_Tutorial_ManageBlock_messageSpeaker_44;

	private SubGraph_Crafting_Tutorial_ManageBlock logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_48 = new SubGraph_Crafting_Tutorial_ManageBlock();

	private SpawnBlockData[] logic_SubGraph_Crafting_Tutorial_ManageBlock_blockSpawnData_48 = new SpawnBlockData[0];

	private int logic_SubGraph_Crafting_Tutorial_ManageBlock_blockIndex_48 = 3;

	private TankBlock logic_SubGraph_Crafting_Tutorial_ManageBlock_block_48;

	private bool logic_SubGraph_Crafting_Tutorial_ManageBlock_allowAnchoring_48;

	private uScript_AddMessage.MessageData logic_SubGraph_Crafting_Tutorial_ManageBlock_msgBlockOutsideArea_48;

	private uScript_AddMessage.MessageSpeaker logic_SubGraph_Crafting_Tutorial_ManageBlock_messageSpeaker_48;

	private SubGraph_Crafting_Tutorial_ManageBlock logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_50 = new SubGraph_Crafting_Tutorial_ManageBlock();

	private SpawnBlockData[] logic_SubGraph_Crafting_Tutorial_ManageBlock_blockSpawnData_50 = new SpawnBlockData[0];

	private int logic_SubGraph_Crafting_Tutorial_ManageBlock_blockIndex_50 = 4;

	private TankBlock logic_SubGraph_Crafting_Tutorial_ManageBlock_block_50;

	private bool logic_SubGraph_Crafting_Tutorial_ManageBlock_allowAnchoring_50;

	private uScript_AddMessage.MessageData logic_SubGraph_Crafting_Tutorial_ManageBlock_msgBlockOutsideArea_50;

	private uScript_AddMessage.MessageSpeaker logic_SubGraph_Crafting_Tutorial_ManageBlock_messageSpeaker_50;

	private uScript_PointArrowAtVisible logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_52 = new uScript_PointArrowAtVisible();

	private object logic_uScript_PointArrowAtVisible_targetObject_52 = "";

	private float logic_uScript_PointArrowAtVisible_timeToShowFor_52 = -1f;

	private Vector3 logic_uScript_PointArrowAtVisible_offset_52 = new Vector3(0f, 0f, 0f);

	private bool logic_uScript_PointArrowAtVisible_Out_52 = true;

	private uScript_BlockAttachedToTech logic_uScript_BlockAttachedToTech_uScript_BlockAttachedToTech_53 = new uScript_BlockAttachedToTech();

	private Tank logic_uScript_BlockAttachedToTech_tech_53;

	private TankBlock logic_uScript_BlockAttachedToTech_block_53;

	private bool logic_uScript_BlockAttachedToTech_True_53 = true;

	private bool logic_uScript_BlockAttachedToTech_False_53 = true;

	private uScript_PointArrowAtVisible logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_55 = new uScript_PointArrowAtVisible();

	private object logic_uScript_PointArrowAtVisible_targetObject_55 = "";

	private float logic_uScript_PointArrowAtVisible_timeToShowFor_55 = -1f;

	private Vector3 logic_uScript_PointArrowAtVisible_offset_55 = new Vector3(0f, 0f, 0f);

	private bool logic_uScript_PointArrowAtVisible_Out_55 = true;

	private uScript_PointArrowAtVisible logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_57 = new uScript_PointArrowAtVisible();

	private object logic_uScript_PointArrowAtVisible_targetObject_57 = "";

	private float logic_uScript_PointArrowAtVisible_timeToShowFor_57 = -1f;

	private Vector3 logic_uScript_PointArrowAtVisible_offset_57 = new Vector3(0f, 0f, 0f);

	private bool logic_uScript_PointArrowAtVisible_Out_57 = true;

	private uScript_BlockAttachedToTech logic_uScript_BlockAttachedToTech_uScript_BlockAttachedToTech_58 = new uScript_BlockAttachedToTech();

	private Tank logic_uScript_BlockAttachedToTech_tech_58;

	private TankBlock logic_uScript_BlockAttachedToTech_block_58;

	private bool logic_uScript_BlockAttachedToTech_True_58 = true;

	private bool logic_uScript_BlockAttachedToTech_False_58 = true;

	private uScript_BlockAttachedToTech logic_uScript_BlockAttachedToTech_uScript_BlockAttachedToTech_61 = new uScript_BlockAttachedToTech();

	private Tank logic_uScript_BlockAttachedToTech_tech_61;

	private TankBlock logic_uScript_BlockAttachedToTech_block_61;

	private bool logic_uScript_BlockAttachedToTech_True_61 = true;

	private bool logic_uScript_BlockAttachedToTech_False_61 = true;

	private uScript_PointArrowAtVisible logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_64 = new uScript_PointArrowAtVisible();

	private object logic_uScript_PointArrowAtVisible_targetObject_64 = "";

	private float logic_uScript_PointArrowAtVisible_timeToShowFor_64 = -1f;

	private Vector3 logic_uScript_PointArrowAtVisible_offset_64 = new Vector3(0f, 0f, 0f);

	private bool logic_uScript_PointArrowAtVisible_Out_64 = true;

	private uScript_BlockAttachedToTech logic_uScript_BlockAttachedToTech_uScript_BlockAttachedToTech_65 = new uScript_BlockAttachedToTech();

	private Tank logic_uScript_BlockAttachedToTech_tech_65;

	private TankBlock logic_uScript_BlockAttachedToTech_block_65;

	private bool logic_uScript_BlockAttachedToTech_True_65 = true;

	private bool logic_uScript_BlockAttachedToTech_False_65 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_69 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_69 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_71 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_71 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_72 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_72 = true;

	private uScript_IsPlayerInteractingWithBlock logic_uScript_IsPlayerInteractingWithBlock_uScript_IsPlayerInteractingWithBlock_73 = new uScript_IsPlayerInteractingWithBlock();

	private TankBlock logic_uScript_IsPlayerInteractingWithBlock_block_73;

	private bool logic_uScript_IsPlayerInteractingWithBlock_Out_73 = true;

	private bool logic_uScript_IsPlayerInteractingWithBlock_Interacted_73 = true;

	private bool logic_uScript_IsPlayerInteractingWithBlock_NotInteracted_73 = true;

	private bool logic_uScript_IsPlayerInteractingWithBlock_Dragging_73 = true;

	private bool logic_uScript_IsPlayerInteractingWithBlock_NotDragging_73 = true;

	private uScript_IsPlayerInteractingWithBlock logic_uScript_IsPlayerInteractingWithBlock_uScript_IsPlayerInteractingWithBlock_74 = new uScript_IsPlayerInteractingWithBlock();

	private TankBlock logic_uScript_IsPlayerInteractingWithBlock_block_74;

	private bool logic_uScript_IsPlayerInteractingWithBlock_Out_74 = true;

	private bool logic_uScript_IsPlayerInteractingWithBlock_Interacted_74 = true;

	private bool logic_uScript_IsPlayerInteractingWithBlock_NotInteracted_74 = true;

	private bool logic_uScript_IsPlayerInteractingWithBlock_Dragging_74 = true;

	private bool logic_uScript_IsPlayerInteractingWithBlock_NotDragging_74 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_75 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_75;

	private bool logic_uScriptAct_SetBool_Out_75 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_75 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_75 = true;

	private uScript_IsPlayerInteractingWithBlock logic_uScript_IsPlayerInteractingWithBlock_uScript_IsPlayerInteractingWithBlock_77 = new uScript_IsPlayerInteractingWithBlock();

	private TankBlock logic_uScript_IsPlayerInteractingWithBlock_block_77;

	private bool logic_uScript_IsPlayerInteractingWithBlock_Out_77 = true;

	private bool logic_uScript_IsPlayerInteractingWithBlock_Interacted_77 = true;

	private bool logic_uScript_IsPlayerInteractingWithBlock_NotInteracted_77 = true;

	private bool logic_uScript_IsPlayerInteractingWithBlock_Dragging_77 = true;

	private bool logic_uScript_IsPlayerInteractingWithBlock_NotDragging_77 = true;

	private uScript_IsPlayerInteractingWithBlock logic_uScript_IsPlayerInteractingWithBlock_uScript_IsPlayerInteractingWithBlock_79 = new uScript_IsPlayerInteractingWithBlock();

	private TankBlock logic_uScript_IsPlayerInteractingWithBlock_block_79;

	private bool logic_uScript_IsPlayerInteractingWithBlock_Out_79 = true;

	private bool logic_uScript_IsPlayerInteractingWithBlock_Interacted_79 = true;

	private bool logic_uScript_IsPlayerInteractingWithBlock_NotInteracted_79 = true;

	private bool logic_uScript_IsPlayerInteractingWithBlock_Dragging_79 = true;

	private bool logic_uScript_IsPlayerInteractingWithBlock_NotDragging_79 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_81 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_81;

	private bool logic_uScriptAct_SetBool_Out_81 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_81 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_81 = true;

	private uScript_SpawnGhostBlocks logic_uScript_SpawnGhostBlocks_uScript_SpawnGhostBlocks_84 = new uScript_SpawnGhostBlocks();

	private GhostBlockSpawnData[] logic_uScript_SpawnGhostBlocks_ghostBlockData_84 = new GhostBlockSpawnData[0];

	private GameObject logic_uScript_SpawnGhostBlocks_ownerNode_84;

	private Tank logic_uScript_SpawnGhostBlocks_targetTech_84;

	private TankBlock[] logic_uScript_SpawnGhostBlocks_Return_84;

	private bool logic_uScript_SpawnGhostBlocks_OnAlreadySpawned_84 = true;

	private bool logic_uScript_SpawnGhostBlocks_OnSpawned_84 = true;

	private uScript_AccessListBlock logic_uScript_AccessListBlock_uScript_AccessListBlock_87 = new uScript_AccessListBlock();

	private TankBlock[] logic_uScript_AccessListBlock_blockList_87 = new TankBlock[0];

	private int logic_uScript_AccessListBlock_index_87;

	private TankBlock logic_uScript_AccessListBlock_value_87;

	private bool logic_uScript_AccessListBlock_Out_87 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_88 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_88;

	private bool logic_uScriptCon_CompareBool_True_88 = true;

	private bool logic_uScriptCon_CompareBool_False_88 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_90 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_90;

	private bool logic_uScriptCon_CompareBool_True_90 = true;

	private bool logic_uScriptCon_CompareBool_False_90 = true;

	private uScript_AccessListBlock logic_uScript_AccessListBlock_uScript_AccessListBlock_92 = new uScript_AccessListBlock();

	private TankBlock[] logic_uScript_AccessListBlock_blockList_92 = new TankBlock[0];

	private int logic_uScript_AccessListBlock_index_92;

	private TankBlock logic_uScript_AccessListBlock_value_92;

	private bool logic_uScript_AccessListBlock_Out_92 = true;

	private uScript_SpawnGhostBlocks logic_uScript_SpawnGhostBlocks_uScript_SpawnGhostBlocks_93 = new uScript_SpawnGhostBlocks();

	private GhostBlockSpawnData[] logic_uScript_SpawnGhostBlocks_ghostBlockData_93 = new GhostBlockSpawnData[0];

	private GameObject logic_uScript_SpawnGhostBlocks_ownerNode_93;

	private Tank logic_uScript_SpawnGhostBlocks_targetTech_93;

	private TankBlock[] logic_uScript_SpawnGhostBlocks_Return_93;

	private bool logic_uScript_SpawnGhostBlocks_OnAlreadySpawned_93 = true;

	private bool logic_uScript_SpawnGhostBlocks_OnSpawned_93 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_94 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_94;

	private bool logic_uScriptAct_SetBool_Out_94 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_94 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_94 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_100 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_100;

	private bool logic_uScriptCon_CompareBool_True_100 = true;

	private bool logic_uScriptCon_CompareBool_False_100 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_102 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_102;

	private bool logic_uScriptCon_CompareBool_True_102 = true;

	private bool logic_uScriptCon_CompareBool_False_102 = true;

	private uScript_AccessListBlock logic_uScript_AccessListBlock_uScript_AccessListBlock_104 = new uScript_AccessListBlock();

	private TankBlock[] logic_uScript_AccessListBlock_blockList_104 = new TankBlock[0];

	private int logic_uScript_AccessListBlock_index_104;

	private TankBlock logic_uScript_AccessListBlock_value_104;

	private bool logic_uScript_AccessListBlock_Out_104 = true;

	private uScript_SpawnGhostBlocks logic_uScript_SpawnGhostBlocks_uScript_SpawnGhostBlocks_108 = new uScript_SpawnGhostBlocks();

	private GhostBlockSpawnData[] logic_uScript_SpawnGhostBlocks_ghostBlockData_108 = new GhostBlockSpawnData[0];

	private GameObject logic_uScript_SpawnGhostBlocks_ownerNode_108;

	private Tank logic_uScript_SpawnGhostBlocks_targetTech_108;

	private TankBlock[] logic_uScript_SpawnGhostBlocks_Return_108;

	private bool logic_uScript_SpawnGhostBlocks_OnAlreadySpawned_108 = true;

	private bool logic_uScript_SpawnGhostBlocks_OnSpawned_108 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_110 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_110;

	private bool logic_uScriptAct_SetBool_Out_110 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_110 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_110 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_112 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_112;

	private bool logic_uScriptAct_SetBool_Out_112 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_112 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_112 = true;

	private uScript_AccessListBlock logic_uScript_AccessListBlock_uScript_AccessListBlock_118 = new uScript_AccessListBlock();

	private TankBlock[] logic_uScript_AccessListBlock_blockList_118 = new TankBlock[0];

	private int logic_uScript_AccessListBlock_index_118;

	private TankBlock logic_uScript_AccessListBlock_value_118;

	private bool logic_uScript_AccessListBlock_Out_118 = true;

	private uScript_SpawnGhostBlocks logic_uScript_SpawnGhostBlocks_uScript_SpawnGhostBlocks_119 = new uScript_SpawnGhostBlocks();

	private GhostBlockSpawnData[] logic_uScript_SpawnGhostBlocks_ghostBlockData_119 = new GhostBlockSpawnData[0];

	private GameObject logic_uScript_SpawnGhostBlocks_ownerNode_119;

	private Tank logic_uScript_SpawnGhostBlocks_targetTech_119;

	private TankBlock[] logic_uScript_SpawnGhostBlocks_Return_119;

	private bool logic_uScript_SpawnGhostBlocks_OnAlreadySpawned_119 = true;

	private bool logic_uScript_SpawnGhostBlocks_OnSpawned_119 = true;

	private uScript_PointArrowAtVisible logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_120 = new uScript_PointArrowAtVisible();

	private object logic_uScript_PointArrowAtVisible_targetObject_120 = "";

	private float logic_uScript_PointArrowAtVisible_timeToShowFor_120 = -1f;

	private Vector3 logic_uScript_PointArrowAtVisible_offset_120 = new Vector3(0f, 0f, 0f);

	private bool logic_uScript_PointArrowAtVisible_Out_120 = true;

	private uScript_PointArrowAtVisible logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_121 = new uScript_PointArrowAtVisible();

	private object logic_uScript_PointArrowAtVisible_targetObject_121 = "";

	private float logic_uScript_PointArrowAtVisible_timeToShowFor_121 = -1f;

	private Vector3 logic_uScript_PointArrowAtVisible_offset_121 = new Vector3(0f, 0f, 0f);

	private bool logic_uScript_PointArrowAtVisible_Out_121 = true;

	private uScript_PointArrowAtVisible logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_122 = new uScript_PointArrowAtVisible();

	private object logic_uScript_PointArrowAtVisible_targetObject_122 = "";

	private float logic_uScript_PointArrowAtVisible_timeToShowFor_122 = -1f;

	private Vector3 logic_uScript_PointArrowAtVisible_offset_122 = new Vector3(0f, 0f, 0f);

	private bool logic_uScript_PointArrowAtVisible_Out_122 = true;

	private uScript_PointArrowAtVisible logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_123 = new uScript_PointArrowAtVisible();

	private object logic_uScript_PointArrowAtVisible_targetObject_123 = "";

	private float logic_uScript_PointArrowAtVisible_timeToShowFor_123 = -1f;

	private Vector3 logic_uScript_PointArrowAtVisible_offset_123 = new Vector3(0f, 0f, 0f);

	private bool logic_uScript_PointArrowAtVisible_Out_123 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_125 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_125;

	private bool logic_uScriptAct_SetBool_Out_125 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_125 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_125 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_127 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_127;

	private bool logic_uScriptCon_CompareBool_True_127 = true;

	private bool logic_uScriptCon_CompareBool_False_127 = true;

	private SubGraph_Crafting_Tutorial_AttachBlockToBase logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_SubGraph_Crafting_Tutorial_AttachBlockToBase_129 = new SubGraph_Crafting_Tutorial_AttachBlockToBase();

	private TankBlock logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_Block_129;

	private Tank logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_CraftingBaseTech_129;

	private GhostBlockSpawnData[] logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_ghostBlockData_129 = new GhostBlockSpawnData[0];

	private BlockTypes logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_BlockType_129 = BlockTypes.GSOReceiver_111;

	private Vector3 logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_BlockPosition_129 = new Vector3(-5f, 0f, 0f);

	private uScriptCon_ManualSwitch logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_132 = new uScriptCon_ManualSwitch();

	private int logic_uScriptCon_ManualSwitch_CurrentOutput_132;

	private uScriptAct_AddInt_v2 logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_134 = new uScriptAct_AddInt_v2();

	private int logic_uScriptAct_AddInt_v2_A_134;

	private int logic_uScriptAct_AddInt_v2_B_134 = 1;

	private int logic_uScriptAct_AddInt_v2_IntResult_134;

	private float logic_uScriptAct_AddInt_v2_FloatResult_134;

	private bool logic_uScriptAct_AddInt_v2_Out_134 = true;

	private uScriptCon_CompareInt logic_uScriptCon_CompareInt_uScriptCon_CompareInt_136 = new uScriptCon_CompareInt();

	private int logic_uScriptCon_CompareInt_A_136;

	private int logic_uScriptCon_CompareInt_B_136 = 4;

	private bool logic_uScriptCon_CompareInt_GreaterThan_136 = true;

	private bool logic_uScriptCon_CompareInt_GreaterThanOrEqualTo_136 = true;

	private bool logic_uScriptCon_CompareInt_EqualTo_136 = true;

	private bool logic_uScriptCon_CompareInt_NotEqualTo_136 = true;

	private bool logic_uScriptCon_CompareInt_LessThanOrEqualTo_136 = true;

	private bool logic_uScriptCon_CompareInt_LessThan_136 = true;

	private uScriptCon_CompareInt logic_uScriptCon_CompareInt_uScriptCon_CompareInt_138 = new uScriptCon_CompareInt();

	private int logic_uScriptCon_CompareInt_A_138;

	private int logic_uScriptCon_CompareInt_B_138;

	private bool logic_uScriptCon_CompareInt_GreaterThan_138 = true;

	private bool logic_uScriptCon_CompareInt_GreaterThanOrEqualTo_138 = true;

	private bool logic_uScriptCon_CompareInt_EqualTo_138 = true;

	private bool logic_uScriptCon_CompareInt_NotEqualTo_138 = true;

	private bool logic_uScriptCon_CompareInt_LessThanOrEqualTo_138 = true;

	private bool logic_uScriptCon_CompareInt_LessThan_138 = true;

	private uScriptCon_CompareInt logic_uScriptCon_CompareInt_uScriptCon_CompareInt_140 = new uScriptCon_CompareInt();

	private int logic_uScriptCon_CompareInt_A_140;

	private int logic_uScriptCon_CompareInt_B_140;

	private bool logic_uScriptCon_CompareInt_GreaterThan_140 = true;

	private bool logic_uScriptCon_CompareInt_GreaterThanOrEqualTo_140 = true;

	private bool logic_uScriptCon_CompareInt_EqualTo_140 = true;

	private bool logic_uScriptCon_CompareInt_NotEqualTo_140 = true;

	private bool logic_uScriptCon_CompareInt_LessThanOrEqualTo_140 = true;

	private bool logic_uScriptCon_CompareInt_LessThan_140 = true;

	private uScriptCon_ManualSwitch logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_142 = new uScriptCon_ManualSwitch();

	private int logic_uScriptCon_ManualSwitch_CurrentOutput_142;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_144 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_144 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_145 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_145 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_146 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_146 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_147 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_147 = true;

	private uScript_DoesTechHaveBlockAtPosition logic_uScript_DoesTechHaveBlockAtPosition_uScript_DoesTechHaveBlockAtPosition_149 = new uScript_DoesTechHaveBlockAtPosition();

	private Tank logic_uScript_DoesTechHaveBlockAtPosition_tech_149;

	private BlockTypes logic_uScript_DoesTechHaveBlockAtPosition_blockType_149;

	private Vector3 logic_uScript_DoesTechHaveBlockAtPosition_localPosition_149 = new Vector3(-4f, 0f, 0f);

	private bool logic_uScript_DoesTechHaveBlockAtPosition_True_149 = true;

	private bool logic_uScript_DoesTechHaveBlockAtPosition_False_149 = true;

	private uScript_DoesTechHaveBlockAtPosition logic_uScript_DoesTechHaveBlockAtPosition_uScript_DoesTechHaveBlockAtPosition_151 = new uScript_DoesTechHaveBlockAtPosition();

	private Tank logic_uScript_DoesTechHaveBlockAtPosition_tech_151;

	private BlockTypes logic_uScript_DoesTechHaveBlockAtPosition_blockType_151;

	private Vector3 logic_uScript_DoesTechHaveBlockAtPosition_localPosition_151 = new Vector3(-1f, 0f, 0f);

	private bool logic_uScript_DoesTechHaveBlockAtPosition_True_151 = true;

	private bool logic_uScript_DoesTechHaveBlockAtPosition_False_151 = true;

	private uScript_DoesTechHaveBlockAtPosition logic_uScript_DoesTechHaveBlockAtPosition_uScript_DoesTechHaveBlockAtPosition_153 = new uScript_DoesTechHaveBlockAtPosition();

	private Tank logic_uScript_DoesTechHaveBlockAtPosition_tech_153;

	private BlockTypes logic_uScript_DoesTechHaveBlockAtPosition_blockType_153;

	private Vector3 logic_uScript_DoesTechHaveBlockAtPosition_localPosition_153 = new Vector3(-2f, 0f, 0f);

	private bool logic_uScript_DoesTechHaveBlockAtPosition_True_153 = true;

	private bool logic_uScript_DoesTechHaveBlockAtPosition_False_153 = true;

	private uScript_DoesTechHaveBlockAtPosition logic_uScript_DoesTechHaveBlockAtPosition_uScript_DoesTechHaveBlockAtPosition_157 = new uScript_DoesTechHaveBlockAtPosition();

	private Tank logic_uScript_DoesTechHaveBlockAtPosition_tech_157;

	private BlockTypes logic_uScript_DoesTechHaveBlockAtPosition_blockType_157;

	private Vector3 logic_uScript_DoesTechHaveBlockAtPosition_localPosition_157 = new Vector3(-3f, 0f, 0f);

	private bool logic_uScript_DoesTechHaveBlockAtPosition_True_157 = true;

	private bool logic_uScript_DoesTechHaveBlockAtPosition_False_157 = true;

	private uScript_RemoveAllGhostBlocksOnTech logic_uScript_RemoveAllGhostBlocksOnTech_uScript_RemoveAllGhostBlocksOnTech_161 = new uScript_RemoveAllGhostBlocksOnTech();

	private Tank logic_uScript_RemoveAllGhostBlocksOnTech_targetTech_161;

	private bool logic_uScript_RemoveAllGhostBlocksOnTech_Out_161 = true;

	private uScript_HideArrow logic_uScript_HideArrow_uScript_HideArrow_164 = new uScript_HideArrow();

	private bool logic_uScript_HideArrow_Out_164 = true;

	private uScript_HideArrow logic_uScript_HideArrow_uScript_HideArrow_165 = new uScript_HideArrow();

	private bool logic_uScript_HideArrow_Out_165 = true;

	private uScript_RemoveAllGhostBlocksOnTech logic_uScript_RemoveAllGhostBlocksOnTech_uScript_RemoveAllGhostBlocksOnTech_167 = new uScript_RemoveAllGhostBlocksOnTech();

	private Tank logic_uScript_RemoveAllGhostBlocksOnTech_targetTech_167;

	private bool logic_uScript_RemoveAllGhostBlocksOnTech_Out_167 = true;

	private uScript_RemoveAllGhostBlocksOnTech logic_uScript_RemoveAllGhostBlocksOnTech_uScript_RemoveAllGhostBlocksOnTech_169 = new uScript_RemoveAllGhostBlocksOnTech();

	private Tank logic_uScript_RemoveAllGhostBlocksOnTech_targetTech_169;

	private bool logic_uScript_RemoveAllGhostBlocksOnTech_Out_169 = true;

	private uScript_HideArrow logic_uScript_HideArrow_uScript_HideArrow_170 = new uScript_HideArrow();

	private bool logic_uScript_HideArrow_Out_170 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_172 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_172;

	private bool logic_uScriptCon_CompareBool_True_172 = true;

	private bool logic_uScriptCon_CompareBool_False_172 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_174 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_174;

	private bool logic_uScriptAct_SetBool_Out_174 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_174 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_174 = true;

	private SubGraph_SaveLoadInt logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_177 = new SubGraph_SaveLoadInt();

	private int logic_SubGraph_SaveLoadInt_restartValue_177;

	private int logic_SubGraph_SaveLoadInt_integer_177;

	private object logic_SubGraph_SaveLoadInt_intAsVariable_177 = "";

	private string logic_SubGraph_SaveLoadInt_uniqueID_177 = "NumConveyorsAttached";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_178 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_178;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_178 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_178 = "Resources02Spawned";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_180 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_180;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_180 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_180 = "msgIntroShown";

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_181 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_181;

	private bool logic_uScriptCon_CompareBool_True_181 = true;

	private bool logic_uScriptCon_CompareBool_False_181 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_183 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_183;

	private bool logic_uScriptAct_SetBool_Out_183 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_183 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_183 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_185 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_185;

	private bool logic_uScriptCon_CompareBool_True_185 = true;

	private bool logic_uScriptCon_CompareBool_False_185 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_188 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_188;

	private bool logic_uScriptAct_SetBool_Out_188 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_188 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_188 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_189 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_189;

	private bool logic_uScriptCon_CompareBool_True_189 = true;

	private bool logic_uScriptCon_CompareBool_False_189 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_191 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_191;

	private bool logic_uScriptAct_SetBool_Out_191 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_191 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_191 = true;

	private uScript_IsPlayerInRangeOfTech logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_194 = new uScript_IsPlayerInRangeOfTech();

	private Tank logic_uScript_IsPlayerInRangeOfTech_tech_194;

	private float logic_uScript_IsPlayerInRangeOfTech_range_194 = 100f;

	private Tank[] logic_uScript_IsPlayerInRangeOfTech_techs_194 = new Tank[0];

	private bool logic_uScript_IsPlayerInRangeOfTech_Out_194 = true;

	private bool logic_uScript_IsPlayerInRangeOfTech_InRange_194 = true;

	private bool logic_uScript_IsPlayerInRangeOfTech_OutOfRange_194 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_196 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_196;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_196 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_196 = "msgConveyorsAttachedShown";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_198 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_198;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_198 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_198 = "msgUnrefinedChunksSoldShown";

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_199 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_199;

	private bool logic_uScriptAct_SetBool_Out_199 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_199 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_199 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_201 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_201;

	private bool logic_uScriptCon_CompareBool_True_201 = true;

	private bool logic_uScriptCon_CompareBool_False_201 = true;

	private uScript_SpawnBlocksFromData logic_uScript_SpawnBlocksFromData_uScript_SpawnBlocksFromData_202 = new uScript_SpawnBlocksFromData();

	private SpawnBlockData[] logic_uScript_SpawnBlocksFromData_blockData_202 = new SpawnBlockData[0];

	private GameObject logic_uScript_SpawnBlocksFromData_ownerNode_202;

	private bool logic_uScript_SpawnBlocksFromData_Out_202 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_205 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_205;

	private bool logic_uScriptCon_CompareBool_True_205 = true;

	private bool logic_uScriptCon_CompareBool_False_205 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_208 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_208 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_208 = true;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_208 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_210 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_210;

	private bool logic_uScriptAct_SetBool_Out_210 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_210 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_210 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_211 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_211;

	private bool logic_uScriptCon_CompareBool_True_211 = true;

	private bool logic_uScriptCon_CompareBool_False_211 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_213 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_213;

	private bool logic_uScriptAct_SetBool_Out_213 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_213 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_213 = true;

	private uScript_HideArrow logic_uScript_HideArrow_uScript_HideArrow_216 = new uScript_HideArrow();

	private bool logic_uScript_HideArrow_Out_216 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_218 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_218;

	private bool logic_uScriptCon_CompareBool_True_218 = true;

	private bool logic_uScriptCon_CompareBool_False_218 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_219 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_219;

	private bool logic_uScriptAct_SetBool_Out_219 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_219 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_219 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_221 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_221;

	private bool logic_uScriptAct_SetBool_Out_221 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_221 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_221 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_222 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_222;

	private bool logic_uScriptCon_CompareBool_True_222 = true;

	private bool logic_uScriptCon_CompareBool_False_222 = true;

	private uScript_Wait logic_uScript_Wait_uScript_Wait_225 = new uScript_Wait();

	private float logic_uScript_Wait_seconds_225;

	private bool logic_uScript_Wait_repeat_225;

	private bool logic_uScript_Wait_Waited_225 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_228 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_228;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_228 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_228 = "Resources01Sold";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_230 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_230;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_230 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_230 = "msgRefinerySpawnedShown";

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_232 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_232;

	private bool logic_uScriptCon_CompareBool_True_232 = true;

	private bool logic_uScriptCon_CompareBool_False_232 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_233 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_233;

	private bool logic_uScriptAct_SetBool_Out_233 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_233 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_233 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_236 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_236;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_236 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_236 = "msgReceiverAttachedShown";

	private uScript_GetAndCheckBlocks logic_uScript_GetAndCheckBlocks_uScript_GetAndCheckBlocks_237 = new uScript_GetAndCheckBlocks();

	private SpawnBlockData[] logic_uScript_GetAndCheckBlocks_blockData_237 = new SpawnBlockData[0];

	private GameObject logic_uScript_GetAndCheckBlocks_ownerNode_237;

	private TankBlock[] logic_uScript_GetAndCheckBlocks_blocks_237 = new TankBlock[0];

	private bool logic_uScript_GetAndCheckBlocks_AllAlive_237 = true;

	private bool logic_uScript_GetAndCheckBlocks_SomeAlive_237 = true;

	private bool logic_uScript_GetAndCheckBlocks_AllDead_237 = true;

	private bool logic_uScript_GetAndCheckBlocks_WaitingToSpawn_237 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_240 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_240;

	private bool logic_uScriptAct_SetBool_Out_240 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_240 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_240 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_243 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_243;

	private bool logic_uScriptAct_SetBool_Out_243 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_243 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_243 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_246 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_246;

	private bool logic_uScriptAct_SetBool_Out_246 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_246 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_246 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_248 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_248;

	private bool logic_uScriptAct_SetBool_Out_248 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_248 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_248 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_250 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_250;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_250 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_250 = "msgBaseFoundShown";

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_252 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_252;

	private bool logic_uScriptCon_CompareBool_True_252 = true;

	private bool logic_uScriptCon_CompareBool_False_252 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_253 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_253;

	private bool logic_uScriptAct_SetBool_Out_253 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_253 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_253 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_255 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_255;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_255 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_255 = "Resources02Sold";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_256 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_256;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_256 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_256 = "RefinerySpawned";

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_258 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_258;

	private bool logic_uScriptAct_SetBool_Out_258 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_258 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_258 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_260 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_260;

	private bool logic_uScriptCon_CompareBool_True_260 = true;

	private bool logic_uScriptCon_CompareBool_False_260 = true;

	private SubGraph_Crafting_Tutorial_ManageBlock logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_262 = new SubGraph_Crafting_Tutorial_ManageBlock();

	private SpawnBlockData[] logic_SubGraph_Crafting_Tutorial_ManageBlock_blockSpawnData_262 = new SpawnBlockData[0];

	private int logic_SubGraph_Crafting_Tutorial_ManageBlock_blockIndex_262;

	private TankBlock logic_SubGraph_Crafting_Tutorial_ManageBlock_block_262;

	private bool logic_SubGraph_Crafting_Tutorial_ManageBlock_allowAnchoring_262;

	private uScript_AddMessage.MessageData logic_SubGraph_Crafting_Tutorial_ManageBlock_msgBlockOutsideArea_262;

	private uScript_AddMessage.MessageSpeaker logic_SubGraph_Crafting_Tutorial_ManageBlock_messageSpeaker_262;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_265 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_265 = true;

	private uScriptCon_ManualSwitch logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_266 = new uScriptCon_ManualSwitch();

	private int logic_uScriptCon_ManualSwitch_CurrentOutput_266;

	private SubGraph_SaveLoadInt logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_269 = new SubGraph_SaveLoadInt();

	private int logic_SubGraph_SaveLoadInt_restartValue_269 = 1;

	private int logic_SubGraph_SaveLoadInt_integer_269;

	private object logic_SubGraph_SaveLoadInt_intAsVariable_269 = "";

	private string logic_SubGraph_SaveLoadInt_uniqueID_269 = "Stage";

	private SubGraph_LoadObjectiveStates logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_270 = new SubGraph_LoadObjectiveStates();

	private int logic_SubGraph_LoadObjectiveStates_currentObjective_270;

	private SubGraph_CompleteObjectiveStage logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_279 = new SubGraph_CompleteObjectiveStage();

	private int logic_SubGraph_CompleteObjectiveStage_objectiveStage_279;

	private bool logic_SubGraph_CompleteObjectiveStage_isFinalObjective_279;

	private SubGraph_CompleteObjectiveStage logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_282 = new SubGraph_CompleteObjectiveStage();

	private int logic_SubGraph_CompleteObjectiveStage_objectiveStage_282;

	private bool logic_SubGraph_CompleteObjectiveStage_isFinalObjective_282;

	private SubGraph_CompleteObjectiveStage logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_284 = new SubGraph_CompleteObjectiveStage();

	private int logic_SubGraph_CompleteObjectiveStage_objectiveStage_284;

	private bool logic_SubGraph_CompleteObjectiveStage_isFinalObjective_284;

	private SubGraph_CompleteObjectiveStage logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_286 = new SubGraph_CompleteObjectiveStage();

	private int logic_SubGraph_CompleteObjectiveStage_objectiveStage_286;

	private bool logic_SubGraph_CompleteObjectiveStage_isFinalObjective_286;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_287 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_287;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_287;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_287;

	private bool logic_uScript_AddMessage_Out_287 = true;

	private bool logic_uScript_AddMessage_Shown_287 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_292 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_292;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_292;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_292;

	private bool logic_uScript_AddMessage_Out_292 = true;

	private bool logic_uScript_AddMessage_Shown_292 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_293 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_293;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_293;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_293;

	private bool logic_uScript_AddMessage_Out_293 = true;

	private bool logic_uScript_AddMessage_Shown_293 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_297 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_297;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_297;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_297;

	private bool logic_uScript_AddMessage_Out_297 = true;

	private bool logic_uScript_AddMessage_Shown_297 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_299 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_299;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_299;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_299;

	private bool logic_uScript_AddMessage_Out_299 = true;

	private bool logic_uScript_AddMessage_Shown_299 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_304 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_304;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_304;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_304;

	private bool logic_uScript_AddMessage_Out_304 = true;

	private bool logic_uScript_AddMessage_Shown_304 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_307 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_307;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_307;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_307;

	private bool logic_uScript_AddMessage_Out_307 = true;

	private bool logic_uScript_AddMessage_Shown_307 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_308 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_308;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_308;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_308;

	private bool logic_uScript_AddMessage_Out_308 = true;

	private bool logic_uScript_AddMessage_Shown_308 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_311 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_311;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_311;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_311;

	private bool logic_uScript_AddMessage_Out_311 = true;

	private bool logic_uScript_AddMessage_Shown_311 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_315 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_315;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_315;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_315;

	private bool logic_uScript_AddMessage_Out_315 = true;

	private bool logic_uScript_AddMessage_Shown_315 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_319 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_319;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_319;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_319;

	private bool logic_uScript_AddMessage_Out_319 = true;

	private bool logic_uScript_AddMessage_Shown_319 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_321 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_321;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_321;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_321;

	private bool logic_uScript_AddMessage_Out_321 = true;

	private bool logic_uScript_AddMessage_Shown_321 = true;

	private SubGraph_Crafting_Tutorial_Finish logic_SubGraph_Crafting_Tutorial_Finish_SubGraph_Crafting_Tutorial_Finish_335 = new SubGraph_Crafting_Tutorial_Finish();

	private Tank logic_SubGraph_Crafting_Tutorial_Finish_craftingBaseTech_335;

	private Tank logic_SubGraph_Crafting_Tutorial_Finish_NPCTech_335;

	private ExternalBehaviorTree logic_SubGraph_Crafting_Tutorial_Finish_NPCFlyAwayAI_335;

	private Transform logic_SubGraph_Crafting_Tutorial_Finish_NPCDespawnParticleEffect_335;

	private uScript_SetEncounterTarget logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_342 = new uScript_SetEncounterTarget();

	private GameObject logic_uScript_SetEncounterTarget_owner_342;

	private object logic_uScript_SetEncounterTarget_visibleObject_342 = "";

	private bool logic_uScript_SetEncounterTarget_Out_342 = true;

	private uScript_SpawnResourceListOnHolder logic_uScript_SpawnResourceListOnHolder_uScript_SpawnResourceListOnHolder_345 = new uScript_SpawnResourceListOnHolder();

	private Tank logic_uScript_SpawnResourceListOnHolder_tech_345;

	private ChunkTypes[] logic_uScript_SpawnResourceListOnHolder_chunks_345 = new ChunkTypes[0];

	private BlockTypes logic_uScript_SpawnResourceListOnHolder_blockType_345 = BlockTypes.GSOReceiver_111;

	private bool logic_uScript_SpawnResourceListOnHolder_Out_345 = true;

	private uScript_SpawnResourceListOnHolder logic_uScript_SpawnResourceListOnHolder_uScript_SpawnResourceListOnHolder_348 = new uScript_SpawnResourceListOnHolder();

	private Tank logic_uScript_SpawnResourceListOnHolder_tech_348;

	private ChunkTypes[] logic_uScript_SpawnResourceListOnHolder_chunks_348 = new ChunkTypes[0];

	private BlockTypes logic_uScript_SpawnResourceListOnHolder_blockType_348 = BlockTypes.GSOReceiver_111;

	private bool logic_uScript_SpawnResourceListOnHolder_Out_348 = true;

	private uScript_ShowHint logic_uScript_ShowHint_uScript_ShowHint_351 = new uScript_ShowHint();

	private GameHints.HintID logic_uScript_ShowHint_hintId_351 = GameHints.HintID.ConveyorDirection;

	private bool logic_uScript_ShowHint_Out_351 = true;

	private uScript_RestrictItemPickup logic_uScript_RestrictItemPickup_uScript_RestrictItemPickup_352 = new uScript_RestrictItemPickup();

	private Tank logic_uScript_RestrictItemPickup_tech_352;

	private ChunkTypes[] logic_uScript_RestrictItemPickup_typesToAccept_352 = new ChunkTypes[0];

	private bool logic_uScript_RestrictItemPickup_Out_352 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_354 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_354 = "";

	private bool logic_uScript_EnableGlow_enable_354 = true;

	private bool logic_uScript_EnableGlow_Out_354 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_357 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_357 = "";

	private bool logic_uScript_EnableGlow_enable_357 = true;

	private bool logic_uScript_EnableGlow_Out_357 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_359 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_359 = "";

	private bool logic_uScript_EnableGlow_enable_359 = true;

	private bool logic_uScript_EnableGlow_Out_359 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_360 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_360 = "";

	private bool logic_uScript_EnableGlow_enable_360 = true;

	private bool logic_uScript_EnableGlow_Out_360 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_363 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_363 = "";

	private bool logic_uScript_EnableGlow_enable_363;

	private bool logic_uScript_EnableGlow_Out_363 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_364 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_364 = "";

	private bool logic_uScript_EnableGlow_enable_364;

	private bool logic_uScript_EnableGlow_Out_364 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_366 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_366 = "";

	private bool logic_uScript_EnableGlow_enable_366;

	private bool logic_uScript_EnableGlow_Out_366 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_367 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_367 = "";

	private bool logic_uScript_EnableGlow_enable_367;

	private bool logic_uScript_EnableGlow_Out_367 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_370 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_370 = "";

	private bool logic_uScript_EnableGlow_enable_370 = true;

	private bool logic_uScript_EnableGlow_Out_370 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_372 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_372 = "";

	private bool logic_uScript_EnableGlow_enable_372 = true;

	private bool logic_uScript_EnableGlow_Out_372 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_374 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_374 = "";

	private bool logic_uScript_EnableGlow_enable_374 = true;

	private bool logic_uScript_EnableGlow_Out_374 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_376 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_376 = "";

	private bool logic_uScript_EnableGlow_enable_376 = true;

	private bool logic_uScript_EnableGlow_Out_376 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_378 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_378 = "";

	private bool logic_uScript_EnableGlow_enable_378;

	private bool logic_uScript_EnableGlow_Out_378 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_381 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_381;

	private bool logic_uScriptCon_CompareBool_True_381 = true;

	private bool logic_uScriptCon_CompareBool_False_381 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_382 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_382 = "";

	private bool logic_uScript_EnableGlow_enable_382;

	private bool logic_uScript_EnableGlow_Out_382 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_384 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_384;

	private bool logic_uScriptCon_CompareBool_True_384 = true;

	private bool logic_uScriptCon_CompareBool_False_384 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_386 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_386 = "";

	private bool logic_uScript_EnableGlow_enable_386;

	private bool logic_uScript_EnableGlow_Out_386 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_388 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_388;

	private bool logic_uScriptCon_CompareBool_True_388 = true;

	private bool logic_uScriptCon_CompareBool_False_388 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_390 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_390 = "";

	private bool logic_uScript_EnableGlow_enable_390;

	private bool logic_uScript_EnableGlow_Out_390 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_392 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_392;

	private bool logic_uScriptCon_CompareBool_True_392 = true;

	private bool logic_uScriptCon_CompareBool_False_392 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_394 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_394 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_395 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_395 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_396 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_396 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_397 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_397 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_398 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_398 = "";

	private bool logic_uScript_EnableGlow_enable_398;

	private bool logic_uScript_EnableGlow_Out_398 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_399 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_399 = "";

	private bool logic_uScript_EnableGlow_enable_399;

	private bool logic_uScript_EnableGlow_Out_399 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_401 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_401 = "";

	private bool logic_uScript_EnableGlow_enable_401;

	private bool logic_uScript_EnableGlow_Out_401 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_403 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_403 = "";

	private bool logic_uScript_EnableGlow_enable_403;

	private bool logic_uScript_EnableGlow_Out_403 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_407 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_407 = "";

	private bool logic_uScript_EnableGlow_enable_407;

	private bool logic_uScript_EnableGlow_Out_407 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_409 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_409 = "";

	private bool logic_uScript_EnableGlow_enable_409;

	private bool logic_uScript_EnableGlow_Out_409 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_410 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_410 = "";

	private bool logic_uScript_EnableGlow_enable_410;

	private bool logic_uScript_EnableGlow_Out_410 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_413 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_413 = "";

	private bool logic_uScript_EnableGlow_enable_413;

	private bool logic_uScript_EnableGlow_Out_413 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_416 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_416 = "";

	private bool logic_uScript_EnableGlow_enable_416;

	private bool logic_uScript_EnableGlow_Out_416 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_417 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_417 = "";

	private bool logic_uScript_EnableGlow_enable_417;

	private bool logic_uScript_EnableGlow_Out_417 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_419 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_419 = "";

	private bool logic_uScript_EnableGlow_enable_419;

	private bool logic_uScript_EnableGlow_Out_419 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_421 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_421 = "";

	private bool logic_uScript_EnableGlow_enable_421;

	private bool logic_uScript_EnableGlow_Out_421 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_424 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_424 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_426 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_426 = "";

	private bool logic_uScript_EnableGlow_enable_426;

	private bool logic_uScript_EnableGlow_Out_426 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_428 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_428 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_429 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_429 = "";

	private bool logic_uScript_EnableGlow_enable_429;

	private bool logic_uScript_EnableGlow_Out_429 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_430 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_430 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_431 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_431 = "";

	private bool logic_uScript_EnableGlow_enable_431;

	private bool logic_uScript_EnableGlow_Out_431 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_432 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_432;

	private bool logic_uScriptCon_CompareBool_True_432 = true;

	private bool logic_uScriptCon_CompareBool_False_432 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_436 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_436 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_439 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_439;

	private bool logic_uScriptCon_CompareBool_True_439 = true;

	private bool logic_uScriptCon_CompareBool_False_439 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_442 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_442;

	private bool logic_uScriptCon_CompareBool_True_442 = true;

	private bool logic_uScriptCon_CompareBool_False_442 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_443 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_443;

	private bool logic_uScriptCon_CompareBool_True_443 = true;

	private bool logic_uScriptCon_CompareBool_False_443 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_444 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_444 = "";

	private bool logic_uScript_EnableGlow_enable_444;

	private bool logic_uScript_EnableGlow_Out_444 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_446 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_446;

	private bool logic_uScriptCon_CompareBool_True_446 = true;

	private bool logic_uScriptCon_CompareBool_False_446 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_449 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_449 = "";

	private bool logic_uScript_EnableGlow_enable_449;

	private bool logic_uScript_EnableGlow_Out_449 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_450 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_450 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_452 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_452 = "";

	private bool logic_uScript_EnableGlow_enable_452;

	private bool logic_uScript_EnableGlow_Out_452 = true;

	private uScript_SendAnaliticsEvent logic_uScript_SendAnaliticsEvent_uScript_SendAnaliticsEvent_453 = new uScript_SendAnaliticsEvent();

	private string logic_uScript_SendAnaliticsEvent_analiticsEvent_453 = "tutorial_start";

	private string logic_uScript_SendAnaliticsEvent_parameterName_453 = "crafting_tutorial";

	private object logic_uScript_SendAnaliticsEvent_parameter_453 = "1";

	private bool logic_uScript_SendAnaliticsEvent_Out_453 = true;

	private uScript_SendAnaliticsEvent logic_uScript_SendAnaliticsEvent_uScript_SendAnaliticsEvent_454 = new uScript_SendAnaliticsEvent();

	private string logic_uScript_SendAnaliticsEvent_analiticsEvent_454 = "tutorial_complete";

	private string logic_uScript_SendAnaliticsEvent_parameterName_454 = "crafting_tutorial";

	private object logic_uScript_SendAnaliticsEvent_parameter_454 = "1";

	private bool logic_uScript_SendAnaliticsEvent_Out_454 = true;

	private ChunkTypes event_UnityEngine_GameObject_ResourceType_16;

	private int event_UnityEngine_GameObject_ResourceTypeTotal_16;

	private int event_UnityEngine_GameObject_SoldTotal_16;

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
		if (null == owner_Connection_10 || !m_RegisteredForEvents)
		{
			owner_Connection_10 = parentGameObject;
			if (null != owner_Connection_10)
			{
				uScript_SaveLoad uScript_SaveLoad2 = owner_Connection_10.GetComponent<uScript_SaveLoad>();
				if (null == uScript_SaveLoad2)
				{
					uScript_SaveLoad2 = owner_Connection_10.AddComponent<uScript_SaveLoad>();
				}
				if (null != uScript_SaveLoad2)
				{
					uScript_SaveLoad2.SaveEvent += Instance_SaveEvent_9;
					uScript_SaveLoad2.LoadEvent += Instance_LoadEvent_9;
					uScript_SaveLoad2.RestartEvent += Instance_RestartEvent_9;
				}
			}
		}
		if (null == owner_Connection_17 || !m_RegisteredForEvents)
		{
			owner_Connection_17 = parentGameObject;
			if (null != owner_Connection_17)
			{
				uScript_ResourceSoldEvent uScript_ResourceSoldEvent2 = owner_Connection_17.GetComponent<uScript_ResourceSoldEvent>();
				if (null == uScript_ResourceSoldEvent2)
				{
					uScript_ResourceSoldEvent2 = owner_Connection_17.AddComponent<uScript_ResourceSoldEvent>();
				}
				if (null != uScript_ResourceSoldEvent2)
				{
					uScript_ResourceSoldEvent2.ResourceSoldEvent += Instance_ResourceSoldEvent_16;
				}
			}
		}
		if (null == owner_Connection_85 || !m_RegisteredForEvents)
		{
			owner_Connection_85 = parentGameObject;
		}
		if (null == owner_Connection_99 || !m_RegisteredForEvents)
		{
			owner_Connection_99 = parentGameObject;
		}
		if (null == owner_Connection_106 || !m_RegisteredForEvents)
		{
			owner_Connection_106 = parentGameObject;
		}
		if (null == owner_Connection_117 || !m_RegisteredForEvents)
		{
			owner_Connection_117 = parentGameObject;
		}
		if (null == owner_Connection_203 || !m_RegisteredForEvents)
		{
			owner_Connection_203 = parentGameObject;
		}
		if (null == owner_Connection_239 || !m_RegisteredForEvents)
		{
			owner_Connection_239 = parentGameObject;
		}
		if (null == owner_Connection_343 || !m_RegisteredForEvents)
		{
			owner_Connection_343 = parentGameObject;
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
		if (!m_RegisteredForEvents && null != owner_Connection_10)
		{
			uScript_SaveLoad uScript_SaveLoad2 = owner_Connection_10.GetComponent<uScript_SaveLoad>();
			if (null == uScript_SaveLoad2)
			{
				uScript_SaveLoad2 = owner_Connection_10.AddComponent<uScript_SaveLoad>();
			}
			if (null != uScript_SaveLoad2)
			{
				uScript_SaveLoad2.SaveEvent += Instance_SaveEvent_9;
				uScript_SaveLoad2.LoadEvent += Instance_LoadEvent_9;
				uScript_SaveLoad2.RestartEvent += Instance_RestartEvent_9;
			}
		}
		if (!m_RegisteredForEvents && null != owner_Connection_17)
		{
			uScript_ResourceSoldEvent uScript_ResourceSoldEvent2 = owner_Connection_17.GetComponent<uScript_ResourceSoldEvent>();
			if (null == uScript_ResourceSoldEvent2)
			{
				uScript_ResourceSoldEvent2 = owner_Connection_17.AddComponent<uScript_ResourceSoldEvent>();
			}
			if (null != uScript_ResourceSoldEvent2)
			{
				uScript_ResourceSoldEvent2.ResourceSoldEvent += Instance_ResourceSoldEvent_16;
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
		if (null != owner_Connection_10)
		{
			uScript_SaveLoad component2 = owner_Connection_10.GetComponent<uScript_SaveLoad>();
			if (null != component2)
			{
				component2.SaveEvent -= Instance_SaveEvent_9;
				component2.LoadEvent -= Instance_LoadEvent_9;
				component2.RestartEvent -= Instance_RestartEvent_9;
			}
		}
		if (null != owner_Connection_17)
		{
			uScript_ResourceSoldEvent component3 = owner_Connection_17.GetComponent<uScript_ResourceSoldEvent>();
			if (null != component3)
			{
				component3.ResourceSoldEvent -= Instance_ResourceSoldEvent_16;
			}
		}
	}

	public override void SetParent(GameObject g)
	{
		parentGameObject = g;
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_2.SetParent(g);
		logic_uScript_PausePopulation_uScript_PausePopulation_3.SetParent(g);
		logic_uScript_PausePopulation_uScript_PausePopulation_4.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_7.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_11.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_15.SetParent(g);
		logic_uScript_CompareChunkTypes_uScript_CompareChunkTypes_18.SetParent(g);
		logic_SubGraph_Crafting_Tutorial_Init_SubGraph_Crafting_Tutorial_Init_21.SetParent(g);
		logic_uScript_LockTechStacks_uScript_LockTechStacks_28.SetParent(g);
		logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_SubGraph_Crafting_Tutorial_AttachBlockToBase_30.SetParent(g);
		logic_uScript_LockTechInteraction_uScript_LockTechInteraction_34.SetParent(g);
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_38.SetParent(g);
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_41.SetParent(g);
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_44.SetParent(g);
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_48.SetParent(g);
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_50.SetParent(g);
		logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_52.SetParent(g);
		logic_uScript_BlockAttachedToTech_uScript_BlockAttachedToTech_53.SetParent(g);
		logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_55.SetParent(g);
		logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_57.SetParent(g);
		logic_uScript_BlockAttachedToTech_uScript_BlockAttachedToTech_58.SetParent(g);
		logic_uScript_BlockAttachedToTech_uScript_BlockAttachedToTech_61.SetParent(g);
		logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_64.SetParent(g);
		logic_uScript_BlockAttachedToTech_uScript_BlockAttachedToTech_65.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_69.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_71.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_72.SetParent(g);
		logic_uScript_IsPlayerInteractingWithBlock_uScript_IsPlayerInteractingWithBlock_73.SetParent(g);
		logic_uScript_IsPlayerInteractingWithBlock_uScript_IsPlayerInteractingWithBlock_74.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_75.SetParent(g);
		logic_uScript_IsPlayerInteractingWithBlock_uScript_IsPlayerInteractingWithBlock_77.SetParent(g);
		logic_uScript_IsPlayerInteractingWithBlock_uScript_IsPlayerInteractingWithBlock_79.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_81.SetParent(g);
		logic_uScript_SpawnGhostBlocks_uScript_SpawnGhostBlocks_84.SetParent(g);
		logic_uScript_AccessListBlock_uScript_AccessListBlock_87.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_88.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_90.SetParent(g);
		logic_uScript_AccessListBlock_uScript_AccessListBlock_92.SetParent(g);
		logic_uScript_SpawnGhostBlocks_uScript_SpawnGhostBlocks_93.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_94.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_100.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_102.SetParent(g);
		logic_uScript_AccessListBlock_uScript_AccessListBlock_104.SetParent(g);
		logic_uScript_SpawnGhostBlocks_uScript_SpawnGhostBlocks_108.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_110.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_112.SetParent(g);
		logic_uScript_AccessListBlock_uScript_AccessListBlock_118.SetParent(g);
		logic_uScript_SpawnGhostBlocks_uScript_SpawnGhostBlocks_119.SetParent(g);
		logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_120.SetParent(g);
		logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_121.SetParent(g);
		logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_122.SetParent(g);
		logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_123.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_125.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_127.SetParent(g);
		logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_SubGraph_Crafting_Tutorial_AttachBlockToBase_129.SetParent(g);
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_132.SetParent(g);
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_134.SetParent(g);
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_136.SetParent(g);
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_138.SetParent(g);
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_140.SetParent(g);
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_142.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_144.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_145.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_146.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_147.SetParent(g);
		logic_uScript_DoesTechHaveBlockAtPosition_uScript_DoesTechHaveBlockAtPosition_149.SetParent(g);
		logic_uScript_DoesTechHaveBlockAtPosition_uScript_DoesTechHaveBlockAtPosition_151.SetParent(g);
		logic_uScript_DoesTechHaveBlockAtPosition_uScript_DoesTechHaveBlockAtPosition_153.SetParent(g);
		logic_uScript_DoesTechHaveBlockAtPosition_uScript_DoesTechHaveBlockAtPosition_157.SetParent(g);
		logic_uScript_RemoveAllGhostBlocksOnTech_uScript_RemoveAllGhostBlocksOnTech_161.SetParent(g);
		logic_uScript_HideArrow_uScript_HideArrow_164.SetParent(g);
		logic_uScript_HideArrow_uScript_HideArrow_165.SetParent(g);
		logic_uScript_RemoveAllGhostBlocksOnTech_uScript_RemoveAllGhostBlocksOnTech_167.SetParent(g);
		logic_uScript_RemoveAllGhostBlocksOnTech_uScript_RemoveAllGhostBlocksOnTech_169.SetParent(g);
		logic_uScript_HideArrow_uScript_HideArrow_170.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_172.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_174.SetParent(g);
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_177.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_178.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_180.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_181.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_183.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_185.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_188.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_189.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_191.SetParent(g);
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_194.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_196.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_198.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_199.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_201.SetParent(g);
		logic_uScript_SpawnBlocksFromData_uScript_SpawnBlocksFromData_202.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_205.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_208.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_210.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_211.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_213.SetParent(g);
		logic_uScript_HideArrow_uScript_HideArrow_216.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_218.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_219.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_221.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_222.SetParent(g);
		logic_uScript_Wait_uScript_Wait_225.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_228.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_230.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_232.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_233.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_236.SetParent(g);
		logic_uScript_GetAndCheckBlocks_uScript_GetAndCheckBlocks_237.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_240.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_243.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_246.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_248.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_250.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_252.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_253.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_255.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_256.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_258.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_260.SetParent(g);
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_262.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_265.SetParent(g);
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_266.SetParent(g);
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_269.SetParent(g);
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_270.SetParent(g);
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_279.SetParent(g);
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_282.SetParent(g);
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_284.SetParent(g);
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_286.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_287.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_292.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_293.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_297.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_299.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_304.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_307.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_308.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_311.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_315.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_319.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_321.SetParent(g);
		logic_SubGraph_Crafting_Tutorial_Finish_SubGraph_Crafting_Tutorial_Finish_335.SetParent(g);
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_342.SetParent(g);
		logic_uScript_SpawnResourceListOnHolder_uScript_SpawnResourceListOnHolder_345.SetParent(g);
		logic_uScript_SpawnResourceListOnHolder_uScript_SpawnResourceListOnHolder_348.SetParent(g);
		logic_uScript_ShowHint_uScript_ShowHint_351.SetParent(g);
		logic_uScript_RestrictItemPickup_uScript_RestrictItemPickup_352.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_354.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_357.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_359.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_360.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_363.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_364.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_366.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_367.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_370.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_372.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_374.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_376.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_378.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_381.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_382.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_384.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_386.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_388.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_390.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_392.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_394.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_395.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_396.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_397.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_398.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_399.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_401.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_403.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_407.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_409.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_410.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_413.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_416.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_417.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_419.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_421.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_424.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_426.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_428.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_429.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_430.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_431.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_432.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_436.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_439.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_442.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_443.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_444.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_446.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_449.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_450.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_452.SetParent(g);
		logic_uScript_SendAnaliticsEvent_uScript_SendAnaliticsEvent_453.SetParent(g);
		logic_uScript_SendAnaliticsEvent_uScript_SendAnaliticsEvent_454.SetParent(g);
		owner_Connection_1 = parentGameObject;
		owner_Connection_10 = parentGameObject;
		owner_Connection_17 = parentGameObject;
		owner_Connection_85 = parentGameObject;
		owner_Connection_99 = parentGameObject;
		owner_Connection_106 = parentGameObject;
		owner_Connection_117 = parentGameObject;
		owner_Connection_203 = parentGameObject;
		owner_Connection_239 = parentGameObject;
		owner_Connection_343 = parentGameObject;
	}

	public void Awake()
	{
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_11.Awake();
		logic_SubGraph_Crafting_Tutorial_Init_SubGraph_Crafting_Tutorial_Init_21.Awake();
		logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_SubGraph_Crafting_Tutorial_AttachBlockToBase_30.Awake();
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_38.Awake();
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_41.Awake();
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_44.Awake();
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_48.Awake();
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_50.Awake();
		logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_SubGraph_Crafting_Tutorial_AttachBlockToBase_129.Awake();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_177.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_178.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_180.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_196.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_198.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_228.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_230.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_236.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_250.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_255.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_256.Awake();
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_262.Awake();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_269.Awake();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_270.Awake();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_279.Awake();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_282.Awake();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_284.Awake();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_286.Awake();
		logic_SubGraph_Crafting_Tutorial_Finish_SubGraph_Crafting_Tutorial_Finish_335.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_11.Save_Out += SubGraph_SaveLoadBool_Save_Out_11;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_11.Load_Out += SubGraph_SaveLoadBool_Load_Out_11;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_11.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_11;
		logic_SubGraph_Crafting_Tutorial_Init_SubGraph_Crafting_Tutorial_Init_21.Out += SubGraph_Crafting_Tutorial_Init_Out_21;
		logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_SubGraph_Crafting_Tutorial_AttachBlockToBase_30.Block_Attached += SubGraph_Crafting_Tutorial_AttachBlockToBase_Block_Attached_30;
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_38.Out += SubGraph_Crafting_Tutorial_ManageBlock_Out_38;
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_41.Out += SubGraph_Crafting_Tutorial_ManageBlock_Out_41;
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_44.Out += SubGraph_Crafting_Tutorial_ManageBlock_Out_44;
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_48.Out += SubGraph_Crafting_Tutorial_ManageBlock_Out_48;
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_50.Out += SubGraph_Crafting_Tutorial_ManageBlock_Out_50;
		logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_SubGraph_Crafting_Tutorial_AttachBlockToBase_129.Block_Attached += SubGraph_Crafting_Tutorial_AttachBlockToBase_Block_Attached_129;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_132.Output1 += uScriptCon_ManualSwitch_Output1_132;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_132.Output2 += uScriptCon_ManualSwitch_Output2_132;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_132.Output3 += uScriptCon_ManualSwitch_Output3_132;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_132.Output4 += uScriptCon_ManualSwitch_Output4_132;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_132.Output5 += uScriptCon_ManualSwitch_Output5_132;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_132.Output6 += uScriptCon_ManualSwitch_Output6_132;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_132.Output7 += uScriptCon_ManualSwitch_Output7_132;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_132.Output8 += uScriptCon_ManualSwitch_Output8_132;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_142.Output1 += uScriptCon_ManualSwitch_Output1_142;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_142.Output2 += uScriptCon_ManualSwitch_Output2_142;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_142.Output3 += uScriptCon_ManualSwitch_Output3_142;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_142.Output4 += uScriptCon_ManualSwitch_Output4_142;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_142.Output5 += uScriptCon_ManualSwitch_Output5_142;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_142.Output6 += uScriptCon_ManualSwitch_Output6_142;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_142.Output7 += uScriptCon_ManualSwitch_Output7_142;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_142.Output8 += uScriptCon_ManualSwitch_Output8_142;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_177.Save_Out += SubGraph_SaveLoadInt_Save_Out_177;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_177.Load_Out += SubGraph_SaveLoadInt_Load_Out_177;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_177.Restart_Out += SubGraph_SaveLoadInt_Restart_Out_177;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_178.Save_Out += SubGraph_SaveLoadBool_Save_Out_178;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_178.Load_Out += SubGraph_SaveLoadBool_Load_Out_178;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_178.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_178;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_180.Save_Out += SubGraph_SaveLoadBool_Save_Out_180;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_180.Load_Out += SubGraph_SaveLoadBool_Load_Out_180;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_180.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_180;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_196.Save_Out += SubGraph_SaveLoadBool_Save_Out_196;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_196.Load_Out += SubGraph_SaveLoadBool_Load_Out_196;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_196.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_196;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_198.Save_Out += SubGraph_SaveLoadBool_Save_Out_198;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_198.Load_Out += SubGraph_SaveLoadBool_Load_Out_198;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_198.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_198;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_228.Save_Out += SubGraph_SaveLoadBool_Save_Out_228;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_228.Load_Out += SubGraph_SaveLoadBool_Load_Out_228;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_228.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_228;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_230.Save_Out += SubGraph_SaveLoadBool_Save_Out_230;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_230.Load_Out += SubGraph_SaveLoadBool_Load_Out_230;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_230.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_230;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_236.Save_Out += SubGraph_SaveLoadBool_Save_Out_236;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_236.Load_Out += SubGraph_SaveLoadBool_Load_Out_236;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_236.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_236;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_250.Save_Out += SubGraph_SaveLoadBool_Save_Out_250;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_250.Load_Out += SubGraph_SaveLoadBool_Load_Out_250;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_250.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_250;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_255.Save_Out += SubGraph_SaveLoadBool_Save_Out_255;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_255.Load_Out += SubGraph_SaveLoadBool_Load_Out_255;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_255.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_255;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_256.Save_Out += SubGraph_SaveLoadBool_Save_Out_256;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_256.Load_Out += SubGraph_SaveLoadBool_Load_Out_256;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_256.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_256;
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_262.Out += SubGraph_Crafting_Tutorial_ManageBlock_Out_262;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_266.Output1 += uScriptCon_ManualSwitch_Output1_266;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_266.Output2 += uScriptCon_ManualSwitch_Output2_266;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_266.Output3 += uScriptCon_ManualSwitch_Output3_266;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_266.Output4 += uScriptCon_ManualSwitch_Output4_266;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_266.Output5 += uScriptCon_ManualSwitch_Output5_266;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_266.Output6 += uScriptCon_ManualSwitch_Output6_266;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_266.Output7 += uScriptCon_ManualSwitch_Output7_266;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_266.Output8 += uScriptCon_ManualSwitch_Output8_266;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_269.Save_Out += SubGraph_SaveLoadInt_Save_Out_269;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_269.Load_Out += SubGraph_SaveLoadInt_Load_Out_269;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_269.Restart_Out += SubGraph_SaveLoadInt_Restart_Out_269;
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_270.Out += SubGraph_LoadObjectiveStates_Out_270;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_279.Out += SubGraph_CompleteObjectiveStage_Out_279;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_282.Out += SubGraph_CompleteObjectiveStage_Out_282;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_284.Out += SubGraph_CompleteObjectiveStage_Out_284;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_286.Out += SubGraph_CompleteObjectiveStage_Out_286;
		logic_SubGraph_Crafting_Tutorial_Finish_SubGraph_Crafting_Tutorial_Finish_335.Out += SubGraph_Crafting_Tutorial_Finish_Out_335;
	}

	public void Start()
	{
		SyncUnityHooks();
		m_RegisteredForEvents = true;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_11.Start();
		logic_SubGraph_Crafting_Tutorial_Init_SubGraph_Crafting_Tutorial_Init_21.Start();
		logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_SubGraph_Crafting_Tutorial_AttachBlockToBase_30.Start();
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_38.Start();
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_41.Start();
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_44.Start();
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_48.Start();
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_50.Start();
		logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_SubGraph_Crafting_Tutorial_AttachBlockToBase_129.Start();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_177.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_178.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_180.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_196.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_198.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_228.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_230.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_236.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_250.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_255.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_256.Start();
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_262.Start();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_269.Start();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_270.Start();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_279.Start();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_282.Start();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_284.Start();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_286.Start();
		logic_SubGraph_Crafting_Tutorial_Finish_SubGraph_Crafting_Tutorial_Finish_335.Start();
	}

	public void OnEnable()
	{
		RegisterForUnityHooks();
		m_RegisteredForEvents = true;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_11.OnEnable();
		logic_SubGraph_Crafting_Tutorial_Init_SubGraph_Crafting_Tutorial_Init_21.OnEnable();
		logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_SubGraph_Crafting_Tutorial_AttachBlockToBase_30.OnEnable();
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_38.OnEnable();
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_41.OnEnable();
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_44.OnEnable();
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_48.OnEnable();
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_50.OnEnable();
		logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_SubGraph_Crafting_Tutorial_AttachBlockToBase_129.OnEnable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_177.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_178.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_180.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_196.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_198.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_228.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_230.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_236.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_250.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_255.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_256.OnEnable();
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_262.OnEnable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_269.OnEnable();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_270.OnEnable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_279.OnEnable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_282.OnEnable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_284.OnEnable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_286.OnEnable();
		logic_SubGraph_Crafting_Tutorial_Finish_SubGraph_Crafting_Tutorial_Finish_335.OnEnable();
	}

	public void OnDisable()
	{
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_2.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_11.OnDisable();
		logic_SubGraph_Crafting_Tutorial_Init_SubGraph_Crafting_Tutorial_Init_21.OnDisable();
		logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_SubGraph_Crafting_Tutorial_AttachBlockToBase_30.OnDisable();
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_38.OnDisable();
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_41.OnDisable();
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_44.OnDisable();
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_48.OnDisable();
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_50.OnDisable();
		logic_uScript_IsPlayerInteractingWithBlock_uScript_IsPlayerInteractingWithBlock_73.OnDisable();
		logic_uScript_IsPlayerInteractingWithBlock_uScript_IsPlayerInteractingWithBlock_74.OnDisable();
		logic_uScript_IsPlayerInteractingWithBlock_uScript_IsPlayerInteractingWithBlock_77.OnDisable();
		logic_uScript_IsPlayerInteractingWithBlock_uScript_IsPlayerInteractingWithBlock_79.OnDisable();
		logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_SubGraph_Crafting_Tutorial_AttachBlockToBase_129.OnDisable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_177.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_178.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_180.OnDisable();
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_194.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_196.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_198.OnDisable();
		logic_uScript_Wait_uScript_Wait_225.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_228.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_230.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_236.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_250.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_255.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_256.OnDisable();
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_262.OnDisable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_269.OnDisable();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_270.OnDisable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_279.OnDisable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_282.OnDisable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_284.OnDisable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_286.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_287.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_292.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_293.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_297.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_299.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_304.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_307.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_308.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_311.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_315.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_319.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_321.OnDisable();
		logic_SubGraph_Crafting_Tutorial_Finish_SubGraph_Crafting_Tutorial_Finish_335.OnDisable();
		UnregisterEventListeners();
		m_RegisteredForEvents = false;
	}

	public void Update()
	{
		SyncEventListeners();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_11.Update();
		logic_SubGraph_Crafting_Tutorial_Init_SubGraph_Crafting_Tutorial_Init_21.Update();
		logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_SubGraph_Crafting_Tutorial_AttachBlockToBase_30.Update();
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_38.Update();
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_41.Update();
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_44.Update();
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_48.Update();
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_50.Update();
		logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_SubGraph_Crafting_Tutorial_AttachBlockToBase_129.Update();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_177.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_178.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_180.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_196.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_198.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_228.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_230.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_236.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_250.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_255.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_256.Update();
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_262.Update();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_269.Update();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_270.Update();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_279.Update();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_282.Update();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_284.Update();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_286.Update();
		logic_SubGraph_Crafting_Tutorial_Finish_SubGraph_Crafting_Tutorial_Finish_335.Update();
	}

	public void OnDestroy()
	{
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_11.OnDestroy();
		logic_SubGraph_Crafting_Tutorial_Init_SubGraph_Crafting_Tutorial_Init_21.OnDestroy();
		logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_SubGraph_Crafting_Tutorial_AttachBlockToBase_30.OnDestroy();
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_38.OnDestroy();
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_41.OnDestroy();
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_44.OnDestroy();
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_48.OnDestroy();
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_50.OnDestroy();
		logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_SubGraph_Crafting_Tutorial_AttachBlockToBase_129.OnDestroy();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_177.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_178.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_180.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_196.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_198.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_228.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_230.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_236.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_250.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_255.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_256.OnDestroy();
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_262.OnDestroy();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_269.OnDestroy();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_270.OnDestroy();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_279.OnDestroy();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_282.OnDestroy();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_284.OnDestroy();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_286.OnDestroy();
		logic_SubGraph_Crafting_Tutorial_Finish_SubGraph_Crafting_Tutorial_Finish_335.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_11.Save_Out -= SubGraph_SaveLoadBool_Save_Out_11;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_11.Load_Out -= SubGraph_SaveLoadBool_Load_Out_11;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_11.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_11;
		logic_SubGraph_Crafting_Tutorial_Init_SubGraph_Crafting_Tutorial_Init_21.Out -= SubGraph_Crafting_Tutorial_Init_Out_21;
		logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_SubGraph_Crafting_Tutorial_AttachBlockToBase_30.Block_Attached -= SubGraph_Crafting_Tutorial_AttachBlockToBase_Block_Attached_30;
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_38.Out -= SubGraph_Crafting_Tutorial_ManageBlock_Out_38;
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_41.Out -= SubGraph_Crafting_Tutorial_ManageBlock_Out_41;
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_44.Out -= SubGraph_Crafting_Tutorial_ManageBlock_Out_44;
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_48.Out -= SubGraph_Crafting_Tutorial_ManageBlock_Out_48;
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_50.Out -= SubGraph_Crafting_Tutorial_ManageBlock_Out_50;
		logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_SubGraph_Crafting_Tutorial_AttachBlockToBase_129.Block_Attached -= SubGraph_Crafting_Tutorial_AttachBlockToBase_Block_Attached_129;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_132.Output1 -= uScriptCon_ManualSwitch_Output1_132;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_132.Output2 -= uScriptCon_ManualSwitch_Output2_132;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_132.Output3 -= uScriptCon_ManualSwitch_Output3_132;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_132.Output4 -= uScriptCon_ManualSwitch_Output4_132;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_132.Output5 -= uScriptCon_ManualSwitch_Output5_132;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_132.Output6 -= uScriptCon_ManualSwitch_Output6_132;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_132.Output7 -= uScriptCon_ManualSwitch_Output7_132;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_132.Output8 -= uScriptCon_ManualSwitch_Output8_132;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_142.Output1 -= uScriptCon_ManualSwitch_Output1_142;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_142.Output2 -= uScriptCon_ManualSwitch_Output2_142;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_142.Output3 -= uScriptCon_ManualSwitch_Output3_142;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_142.Output4 -= uScriptCon_ManualSwitch_Output4_142;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_142.Output5 -= uScriptCon_ManualSwitch_Output5_142;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_142.Output6 -= uScriptCon_ManualSwitch_Output6_142;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_142.Output7 -= uScriptCon_ManualSwitch_Output7_142;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_142.Output8 -= uScriptCon_ManualSwitch_Output8_142;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_177.Save_Out -= SubGraph_SaveLoadInt_Save_Out_177;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_177.Load_Out -= SubGraph_SaveLoadInt_Load_Out_177;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_177.Restart_Out -= SubGraph_SaveLoadInt_Restart_Out_177;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_178.Save_Out -= SubGraph_SaveLoadBool_Save_Out_178;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_178.Load_Out -= SubGraph_SaveLoadBool_Load_Out_178;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_178.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_178;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_180.Save_Out -= SubGraph_SaveLoadBool_Save_Out_180;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_180.Load_Out -= SubGraph_SaveLoadBool_Load_Out_180;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_180.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_180;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_196.Save_Out -= SubGraph_SaveLoadBool_Save_Out_196;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_196.Load_Out -= SubGraph_SaveLoadBool_Load_Out_196;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_196.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_196;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_198.Save_Out -= SubGraph_SaveLoadBool_Save_Out_198;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_198.Load_Out -= SubGraph_SaveLoadBool_Load_Out_198;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_198.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_198;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_228.Save_Out -= SubGraph_SaveLoadBool_Save_Out_228;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_228.Load_Out -= SubGraph_SaveLoadBool_Load_Out_228;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_228.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_228;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_230.Save_Out -= SubGraph_SaveLoadBool_Save_Out_230;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_230.Load_Out -= SubGraph_SaveLoadBool_Load_Out_230;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_230.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_230;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_236.Save_Out -= SubGraph_SaveLoadBool_Save_Out_236;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_236.Load_Out -= SubGraph_SaveLoadBool_Load_Out_236;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_236.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_236;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_250.Save_Out -= SubGraph_SaveLoadBool_Save_Out_250;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_250.Load_Out -= SubGraph_SaveLoadBool_Load_Out_250;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_250.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_250;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_255.Save_Out -= SubGraph_SaveLoadBool_Save_Out_255;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_255.Load_Out -= SubGraph_SaveLoadBool_Load_Out_255;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_255.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_255;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_256.Save_Out -= SubGraph_SaveLoadBool_Save_Out_256;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_256.Load_Out -= SubGraph_SaveLoadBool_Load_Out_256;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_256.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_256;
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_262.Out -= SubGraph_Crafting_Tutorial_ManageBlock_Out_262;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_266.Output1 -= uScriptCon_ManualSwitch_Output1_266;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_266.Output2 -= uScriptCon_ManualSwitch_Output2_266;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_266.Output3 -= uScriptCon_ManualSwitch_Output3_266;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_266.Output4 -= uScriptCon_ManualSwitch_Output4_266;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_266.Output5 -= uScriptCon_ManualSwitch_Output5_266;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_266.Output6 -= uScriptCon_ManualSwitch_Output6_266;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_266.Output7 -= uScriptCon_ManualSwitch_Output7_266;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_266.Output8 -= uScriptCon_ManualSwitch_Output8_266;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_269.Save_Out -= SubGraph_SaveLoadInt_Save_Out_269;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_269.Load_Out -= SubGraph_SaveLoadInt_Load_Out_269;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_269.Restart_Out -= SubGraph_SaveLoadInt_Restart_Out_269;
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_270.Out -= SubGraph_LoadObjectiveStates_Out_270;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_279.Out -= SubGraph_CompleteObjectiveStage_Out_279;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_282.Out -= SubGraph_CompleteObjectiveStage_Out_282;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_284.Out -= SubGraph_CompleteObjectiveStage_Out_284;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_286.Out -= SubGraph_CompleteObjectiveStage_Out_286;
		logic_SubGraph_Crafting_Tutorial_Finish_SubGraph_Crafting_Tutorial_Finish_335.Out -= SubGraph_Crafting_Tutorial_Finish_Out_335;
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

	private void Instance_ResourceSoldEvent_16(object o, uScript_ResourceSoldEvent.ResourceSoldEventArgs e)
	{
		event_UnityEngine_GameObject_ResourceType_16 = e.ResourceType;
		event_UnityEngine_GameObject_ResourceTypeTotal_16 = e.ResourceTypeTotal;
		event_UnityEngine_GameObject_SoldTotal_16 = e.SoldTotal;
		Relay_ResourceSoldEvent_16();
	}

	private void SubGraph_SaveLoadBool_Save_Out_11(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_11 = e.boolean;
		local_Resources01Spawned_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_11;
		Relay_Save_Out_11();
	}

	private void SubGraph_SaveLoadBool_Load_Out_11(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_11 = e.boolean;
		local_Resources01Spawned_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_11;
		Relay_Load_Out_11();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_11(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_11 = e.boolean;
		local_Resources01Spawned_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_11;
		Relay_Restart_Out_11();
	}

	private void SubGraph_Crafting_Tutorial_Init_Out_21(object o, SubGraph_Crafting_Tutorial_Init.LogicEventArgs e)
	{
		logic_SubGraph_Crafting_Tutorial_Init_CraftingBaseTech_21 = e.CraftingBaseTech;
		logic_SubGraph_Crafting_Tutorial_Init_NPCTech_21 = e.NPCTech;
		local_CraftingBaseTech_Tank = logic_SubGraph_Crafting_Tutorial_Init_CraftingBaseTech_21;
		local_NPCTech_Tank = logic_SubGraph_Crafting_Tutorial_Init_NPCTech_21;
		Relay_Out_21();
	}

	private void SubGraph_Crafting_Tutorial_AttachBlockToBase_Block_Attached_30(object o, SubGraph_Crafting_Tutorial_AttachBlockToBase.LogicEventArgs e)
	{
		Relay_Block_Attached_30();
	}

	private void SubGraph_Crafting_Tutorial_ManageBlock_Out_38(object o, SubGraph_Crafting_Tutorial_ManageBlock.LogicEventArgs e)
	{
		logic_SubGraph_Crafting_Tutorial_ManageBlock_block_38 = e.block;
		blockSpawnData = logic_SubGraph_Crafting_Tutorial_ManageBlock_blockSpawnData_38;
		local_ReceiverBlock_TankBlock = logic_SubGraph_Crafting_Tutorial_ManageBlock_block_38;
		Relay_Out_38();
	}

	private void SubGraph_Crafting_Tutorial_ManageBlock_Out_41(object o, SubGraph_Crafting_Tutorial_ManageBlock.LogicEventArgs e)
	{
		logic_SubGraph_Crafting_Tutorial_ManageBlock_block_41 = e.block;
		blockSpawnData = logic_SubGraph_Crafting_Tutorial_ManageBlock_blockSpawnData_41;
		local_ConveyorBlock01_TankBlock = logic_SubGraph_Crafting_Tutorial_ManageBlock_block_41;
		Relay_Out_41();
	}

	private void SubGraph_Crafting_Tutorial_ManageBlock_Out_44(object o, SubGraph_Crafting_Tutorial_ManageBlock.LogicEventArgs e)
	{
		logic_SubGraph_Crafting_Tutorial_ManageBlock_block_44 = e.block;
		blockSpawnData = logic_SubGraph_Crafting_Tutorial_ManageBlock_blockSpawnData_44;
		local_ConveyorBlock02_TankBlock = logic_SubGraph_Crafting_Tutorial_ManageBlock_block_44;
		Relay_Out_44();
	}

	private void SubGraph_Crafting_Tutorial_ManageBlock_Out_48(object o, SubGraph_Crafting_Tutorial_ManageBlock.LogicEventArgs e)
	{
		logic_SubGraph_Crafting_Tutorial_ManageBlock_block_48 = e.block;
		blockSpawnData = logic_SubGraph_Crafting_Tutorial_ManageBlock_blockSpawnData_48;
		local_ConveyorBlock03_TankBlock = logic_SubGraph_Crafting_Tutorial_ManageBlock_block_48;
		Relay_Out_48();
	}

	private void SubGraph_Crafting_Tutorial_ManageBlock_Out_50(object o, SubGraph_Crafting_Tutorial_ManageBlock.LogicEventArgs e)
	{
		logic_SubGraph_Crafting_Tutorial_ManageBlock_block_50 = e.block;
		blockSpawnData = logic_SubGraph_Crafting_Tutorial_ManageBlock_blockSpawnData_50;
		local_ConveyorBlock04_TankBlock = logic_SubGraph_Crafting_Tutorial_ManageBlock_block_50;
		Relay_Out_50();
	}

	private void SubGraph_Crafting_Tutorial_AttachBlockToBase_Block_Attached_129(object o, SubGraph_Crafting_Tutorial_AttachBlockToBase.LogicEventArgs e)
	{
		Relay_Block_Attached_129();
	}

	private void uScriptCon_ManualSwitch_Output1_132(object o, EventArgs e)
	{
		Relay_Output1_132();
	}

	private void uScriptCon_ManualSwitch_Output2_132(object o, EventArgs e)
	{
		Relay_Output2_132();
	}

	private void uScriptCon_ManualSwitch_Output3_132(object o, EventArgs e)
	{
		Relay_Output3_132();
	}

	private void uScriptCon_ManualSwitch_Output4_132(object o, EventArgs e)
	{
		Relay_Output4_132();
	}

	private void uScriptCon_ManualSwitch_Output5_132(object o, EventArgs e)
	{
		Relay_Output5_132();
	}

	private void uScriptCon_ManualSwitch_Output6_132(object o, EventArgs e)
	{
		Relay_Output6_132();
	}

	private void uScriptCon_ManualSwitch_Output7_132(object o, EventArgs e)
	{
		Relay_Output7_132();
	}

	private void uScriptCon_ManualSwitch_Output8_132(object o, EventArgs e)
	{
		Relay_Output8_132();
	}

	private void uScriptCon_ManualSwitch_Output1_142(object o, EventArgs e)
	{
		Relay_Output1_142();
	}

	private void uScriptCon_ManualSwitch_Output2_142(object o, EventArgs e)
	{
		Relay_Output2_142();
	}

	private void uScriptCon_ManualSwitch_Output3_142(object o, EventArgs e)
	{
		Relay_Output3_142();
	}

	private void uScriptCon_ManualSwitch_Output4_142(object o, EventArgs e)
	{
		Relay_Output4_142();
	}

	private void uScriptCon_ManualSwitch_Output5_142(object o, EventArgs e)
	{
		Relay_Output5_142();
	}

	private void uScriptCon_ManualSwitch_Output6_142(object o, EventArgs e)
	{
		Relay_Output6_142();
	}

	private void uScriptCon_ManualSwitch_Output7_142(object o, EventArgs e)
	{
		Relay_Output7_142();
	}

	private void uScriptCon_ManualSwitch_Output8_142(object o, EventArgs e)
	{
		Relay_Output8_142();
	}

	private void SubGraph_SaveLoadInt_Save_Out_177(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_177 = e.integer;
		local_NumConveyorsAttached_System_Int32 = logic_SubGraph_SaveLoadInt_integer_177;
		Relay_Save_Out_177();
	}

	private void SubGraph_SaveLoadInt_Load_Out_177(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_177 = e.integer;
		local_NumConveyorsAttached_System_Int32 = logic_SubGraph_SaveLoadInt_integer_177;
		Relay_Load_Out_177();
	}

	private void SubGraph_SaveLoadInt_Restart_Out_177(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_177 = e.integer;
		local_NumConveyorsAttached_System_Int32 = logic_SubGraph_SaveLoadInt_integer_177;
		Relay_Restart_Out_177();
	}

	private void SubGraph_SaveLoadBool_Save_Out_178(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_178 = e.boolean;
		local_Resources02Spawned_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_178;
		Relay_Save_Out_178();
	}

	private void SubGraph_SaveLoadBool_Load_Out_178(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_178 = e.boolean;
		local_Resources02Spawned_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_178;
		Relay_Load_Out_178();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_178(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_178 = e.boolean;
		local_Resources02Spawned_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_178;
		Relay_Restart_Out_178();
	}

	private void SubGraph_SaveLoadBool_Save_Out_180(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_180 = e.boolean;
		local_msgIntroShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_180;
		Relay_Save_Out_180();
	}

	private void SubGraph_SaveLoadBool_Load_Out_180(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_180 = e.boolean;
		local_msgIntroShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_180;
		Relay_Load_Out_180();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_180(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_180 = e.boolean;
		local_msgIntroShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_180;
		Relay_Restart_Out_180();
	}

	private void SubGraph_SaveLoadBool_Save_Out_196(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_196 = e.boolean;
		local_msgConveyorsAttachedShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_196;
		Relay_Save_Out_196();
	}

	private void SubGraph_SaveLoadBool_Load_Out_196(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_196 = e.boolean;
		local_msgConveyorsAttachedShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_196;
		Relay_Load_Out_196();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_196(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_196 = e.boolean;
		local_msgConveyorsAttachedShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_196;
		Relay_Restart_Out_196();
	}

	private void SubGraph_SaveLoadBool_Save_Out_198(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_198 = e.boolean;
		local_msgUnrefinedChunksSoldShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_198;
		Relay_Save_Out_198();
	}

	private void SubGraph_SaveLoadBool_Load_Out_198(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_198 = e.boolean;
		local_msgUnrefinedChunksSoldShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_198;
		Relay_Load_Out_198();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_198(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_198 = e.boolean;
		local_msgUnrefinedChunksSoldShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_198;
		Relay_Restart_Out_198();
	}

	private void SubGraph_SaveLoadBool_Save_Out_228(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_228 = e.boolean;
		local_Resources01Sold_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_228;
		Relay_Save_Out_228();
	}

	private void SubGraph_SaveLoadBool_Load_Out_228(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_228 = e.boolean;
		local_Resources01Sold_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_228;
		Relay_Load_Out_228();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_228(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_228 = e.boolean;
		local_Resources01Sold_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_228;
		Relay_Restart_Out_228();
	}

	private void SubGraph_SaveLoadBool_Save_Out_230(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_230 = e.boolean;
		local_msgRefinerySpawnedShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_230;
		Relay_Save_Out_230();
	}

	private void SubGraph_SaveLoadBool_Load_Out_230(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_230 = e.boolean;
		local_msgRefinerySpawnedShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_230;
		Relay_Load_Out_230();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_230(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_230 = e.boolean;
		local_msgRefinerySpawnedShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_230;
		Relay_Restart_Out_230();
	}

	private void SubGraph_SaveLoadBool_Save_Out_236(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_236 = e.boolean;
		local_msgReceiverAttachedShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_236;
		Relay_Save_Out_236();
	}

	private void SubGraph_SaveLoadBool_Load_Out_236(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_236 = e.boolean;
		local_msgReceiverAttachedShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_236;
		Relay_Load_Out_236();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_236(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_236 = e.boolean;
		local_msgReceiverAttachedShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_236;
		Relay_Restart_Out_236();
	}

	private void SubGraph_SaveLoadBool_Save_Out_250(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_250 = e.boolean;
		local_msgBaseFoundShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_250;
		Relay_Save_Out_250();
	}

	private void SubGraph_SaveLoadBool_Load_Out_250(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_250 = e.boolean;
		local_msgBaseFoundShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_250;
		Relay_Load_Out_250();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_250(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_250 = e.boolean;
		local_msgBaseFoundShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_250;
		Relay_Restart_Out_250();
	}

	private void SubGraph_SaveLoadBool_Save_Out_255(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_255 = e.boolean;
		local_Resources02Sold_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_255;
		Relay_Save_Out_255();
	}

	private void SubGraph_SaveLoadBool_Load_Out_255(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_255 = e.boolean;
		local_Resources02Sold_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_255;
		Relay_Load_Out_255();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_255(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_255 = e.boolean;
		local_Resources02Sold_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_255;
		Relay_Restart_Out_255();
	}

	private void SubGraph_SaveLoadBool_Save_Out_256(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_256 = e.boolean;
		local_RefinerySpawned_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_256;
		Relay_Save_Out_256();
	}

	private void SubGraph_SaveLoadBool_Load_Out_256(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_256 = e.boolean;
		local_RefinerySpawned_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_256;
		Relay_Load_Out_256();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_256(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_256 = e.boolean;
		local_RefinerySpawned_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_256;
		Relay_Restart_Out_256();
	}

	private void SubGraph_Crafting_Tutorial_ManageBlock_Out_262(object o, SubGraph_Crafting_Tutorial_ManageBlock.LogicEventArgs e)
	{
		logic_SubGraph_Crafting_Tutorial_ManageBlock_block_262 = e.block;
		blockSpawnDataRefinery = logic_SubGraph_Crafting_Tutorial_ManageBlock_blockSpawnData_262;
		local_RefineryBlock_TankBlock = logic_SubGraph_Crafting_Tutorial_ManageBlock_block_262;
		Relay_Out_262();
	}

	private void uScriptCon_ManualSwitch_Output1_266(object o, EventArgs e)
	{
		Relay_Output1_266();
	}

	private void uScriptCon_ManualSwitch_Output2_266(object o, EventArgs e)
	{
		Relay_Output2_266();
	}

	private void uScriptCon_ManualSwitch_Output3_266(object o, EventArgs e)
	{
		Relay_Output3_266();
	}

	private void uScriptCon_ManualSwitch_Output4_266(object o, EventArgs e)
	{
		Relay_Output4_266();
	}

	private void uScriptCon_ManualSwitch_Output5_266(object o, EventArgs e)
	{
		Relay_Output5_266();
	}

	private void uScriptCon_ManualSwitch_Output6_266(object o, EventArgs e)
	{
		Relay_Output6_266();
	}

	private void uScriptCon_ManualSwitch_Output7_266(object o, EventArgs e)
	{
		Relay_Output7_266();
	}

	private void uScriptCon_ManualSwitch_Output8_266(object o, EventArgs e)
	{
		Relay_Output8_266();
	}

	private void SubGraph_SaveLoadInt_Save_Out_269(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_269 = e.integer;
		local_Stage_System_Int32 = logic_SubGraph_SaveLoadInt_integer_269;
		Relay_Save_Out_269();
	}

	private void SubGraph_SaveLoadInt_Load_Out_269(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_269 = e.integer;
		local_Stage_System_Int32 = logic_SubGraph_SaveLoadInt_integer_269;
		Relay_Load_Out_269();
	}

	private void SubGraph_SaveLoadInt_Restart_Out_269(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_269 = e.integer;
		local_Stage_System_Int32 = logic_SubGraph_SaveLoadInt_integer_269;
		Relay_Restart_Out_269();
	}

	private void SubGraph_LoadObjectiveStates_Out_270(object o, SubGraph_LoadObjectiveStates.LogicEventArgs e)
	{
		Relay_Out_270();
	}

	private void SubGraph_CompleteObjectiveStage_Out_279(object o, SubGraph_CompleteObjectiveStage.LogicEventArgs e)
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_279 = e.objectiveStage;
		local_Stage_System_Int32 = logic_SubGraph_CompleteObjectiveStage_objectiveStage_279;
		Relay_Out_279();
	}

	private void SubGraph_CompleteObjectiveStage_Out_282(object o, SubGraph_CompleteObjectiveStage.LogicEventArgs e)
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_282 = e.objectiveStage;
		local_Stage_System_Int32 = logic_SubGraph_CompleteObjectiveStage_objectiveStage_282;
		Relay_Out_282();
	}

	private void SubGraph_CompleteObjectiveStage_Out_284(object o, SubGraph_CompleteObjectiveStage.LogicEventArgs e)
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_284 = e.objectiveStage;
		local_Stage_System_Int32 = logic_SubGraph_CompleteObjectiveStage_objectiveStage_284;
		Relay_Out_284();
	}

	private void SubGraph_CompleteObjectiveStage_Out_286(object o, SubGraph_CompleteObjectiveStage.LogicEventArgs e)
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_286 = e.objectiveStage;
		local_Stage_System_Int32 = logic_SubGraph_CompleteObjectiveStage_objectiveStage_286;
		Relay_Out_286();
	}

	private void SubGraph_Crafting_Tutorial_Finish_Out_335(object o, SubGraph_Crafting_Tutorial_Finish.LogicEventArgs e)
	{
		Relay_Out_335();
	}

	private void Relay_OnUpdate_0()
	{
		Relay_In_21();
	}

	private void Relay_OnSuspend_0()
	{
	}

	private void Relay_OnResume_0()
	{
	}

	private void Relay_In_2()
	{
		logic_uScript_IsPlayerInRangeOfTech_tech_2 = local_CraftingBaseTech_Tank;
		logic_uScript_IsPlayerInRangeOfTech_range_2 = distBaseFound;
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_2.In(logic_uScript_IsPlayerInRangeOfTech_tech_2, logic_uScript_IsPlayerInRangeOfTech_range_2, logic_uScript_IsPlayerInRangeOfTech_techs_2);
		bool inRange = logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_2.InRange;
		bool outOfRange = logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_2.OutOfRange;
		if (inRange)
		{
			Relay_Pause_3();
		}
		if (outOfRange)
		{
			Relay_UnPause_4();
		}
	}

	private void Relay_Pause_3()
	{
		logic_uScript_PausePopulation_uScript_PausePopulation_3.Pause();
		if (logic_uScript_PausePopulation_uScript_PausePopulation_3.Out)
		{
			Relay_True_210();
		}
	}

	private void Relay_UnPause_3()
	{
		logic_uScript_PausePopulation_uScript_PausePopulation_3.UnPause();
		if (logic_uScript_PausePopulation_uScript_PausePopulation_3.Out)
		{
			Relay_True_210();
		}
	}

	private void Relay_Pause_4()
	{
		logic_uScript_PausePopulation_uScript_PausePopulation_4.Pause();
		if (logic_uScript_PausePopulation_uScript_PausePopulation_4.Out)
		{
			Relay_In_15();
		}
	}

	private void Relay_UnPause_4()
	{
		logic_uScript_PausePopulation_uScript_PausePopulation_4.UnPause();
		if (logic_uScript_PausePopulation_uScript_PausePopulation_4.Out)
		{
			Relay_In_15();
		}
	}

	private void Relay_True_7()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_7.True(out logic_uScriptAct_SetBool_Target_7);
		local_Resources02Spawned_System_Boolean = logic_uScriptAct_SetBool_Target_7;
	}

	private void Relay_False_7()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_7.False(out logic_uScriptAct_SetBool_Target_7);
		local_Resources02Spawned_System_Boolean = logic_uScriptAct_SetBool_Target_7;
	}

	private void Relay_SaveEvent_9()
	{
		Relay_Save_177();
	}

	private void Relay_LoadEvent_9()
	{
		Relay_Load_177();
	}

	private void Relay_RestartEvent_9()
	{
		Relay_Restart_177();
	}

	private void Relay_Save_Out_11()
	{
		Relay_Save_178();
	}

	private void Relay_Load_Out_11()
	{
		Relay_Load_178();
	}

	private void Relay_Restart_Out_11()
	{
		Relay_Set_False_178();
	}

	private void Relay_Save_11()
	{
		logic_SubGraph_SaveLoadBool_boolean_11 = local_Resources01Spawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_11 = local_Resources01Spawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_11.Save(ref logic_SubGraph_SaveLoadBool_boolean_11, logic_SubGraph_SaveLoadBool_boolAsVariable_11, logic_SubGraph_SaveLoadBool_uniqueID_11);
	}

	private void Relay_Load_11()
	{
		logic_SubGraph_SaveLoadBool_boolean_11 = local_Resources01Spawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_11 = local_Resources01Spawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_11.Load(ref logic_SubGraph_SaveLoadBool_boolean_11, logic_SubGraph_SaveLoadBool_boolAsVariable_11, logic_SubGraph_SaveLoadBool_uniqueID_11);
	}

	private void Relay_Set_True_11()
	{
		logic_SubGraph_SaveLoadBool_boolean_11 = local_Resources01Spawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_11 = local_Resources01Spawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_11.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_11, logic_SubGraph_SaveLoadBool_boolAsVariable_11, logic_SubGraph_SaveLoadBool_uniqueID_11);
	}

	private void Relay_Set_False_11()
	{
		logic_SubGraph_SaveLoadBool_boolean_11 = local_Resources01Spawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_11 = local_Resources01Spawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_11.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_11, logic_SubGraph_SaveLoadBool_boolAsVariable_11, logic_SubGraph_SaveLoadBool_uniqueID_11);
	}

	private void Relay_In_15()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_15 = messageTag;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_15.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_15, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_15);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_15.Out)
		{
			Relay_In_216();
		}
	}

	private void Relay_ResourceSoldEvent_16()
	{
		local_20_ChunkTypes = event_UnityEngine_GameObject_ResourceType_16;
		Relay_In_18();
	}

	private void Relay_In_18()
	{
		logic_uScript_CompareChunkTypes_A_18 = local_20_ChunkTypes;
		logic_uScript_CompareChunkTypes_B_18 = resourceTypeRefined;
		logic_uScript_CompareChunkTypes_uScript_CompareChunkTypes_18.In(logic_uScript_CompareChunkTypes_A_18, logic_uScript_CompareChunkTypes_B_18);
		if (logic_uScript_CompareChunkTypes_uScript_CompareChunkTypes_18.EqualTo)
		{
			Relay_True_253();
		}
	}

	private void Relay_Out_21()
	{
		Relay_In_38();
	}

	private void Relay_In_21()
	{
		int num = 0;
		Array array = baseSpawnData;
		if (logic_SubGraph_Crafting_Tutorial_Init_baseSpawnData_21.Length != num + array.Length)
		{
			Array.Resize(ref logic_SubGraph_Crafting_Tutorial_Init_baseSpawnData_21, num + array.Length);
		}
		Array.Copy(array, 0, logic_SubGraph_Crafting_Tutorial_Init_baseSpawnData_21, num, array.Length);
		num += array.Length;
		int num2 = 0;
		Array array2 = blockSpawnData;
		if (logic_SubGraph_Crafting_Tutorial_Init_blockSpawnData_21.Length != num2 + array2.Length)
		{
			Array.Resize(ref logic_SubGraph_Crafting_Tutorial_Init_blockSpawnData_21, num2 + array2.Length);
		}
		Array.Copy(array2, 0, logic_SubGraph_Crafting_Tutorial_Init_blockSpawnData_21, num2, array2.Length);
		num2 += array2.Length;
		int num3 = 0;
		Array nPCSpawnData = NPCSpawnData;
		if (logic_SubGraph_Crafting_Tutorial_Init_NPCSpawnData_21.Length != num3 + nPCSpawnData.Length)
		{
			Array.Resize(ref logic_SubGraph_Crafting_Tutorial_Init_NPCSpawnData_21, num3 + nPCSpawnData.Length);
		}
		Array.Copy(nPCSpawnData, 0, logic_SubGraph_Crafting_Tutorial_Init_NPCSpawnData_21, num3, nPCSpawnData.Length);
		num3 += nPCSpawnData.Length;
		logic_SubGraph_Crafting_Tutorial_Init_completedBasePreset_21 = completedBasePreset;
		logic_SubGraph_Crafting_Tutorial_Init_basePosition_21 = basePosition;
		logic_SubGraph_Crafting_Tutorial_Init_clearSceneryRadius_21 = clearSceneryRadius;
		logic_SubGraph_Crafting_Tutorial_Init_CraftingBaseTech_21 = local_CraftingBaseTech_Tank;
		logic_SubGraph_Crafting_Tutorial_Init_NPCTech_21 = local_NPCTech_Tank;
		logic_SubGraph_Crafting_Tutorial_Init_SubGraph_Crafting_Tutorial_Init_21.In(logic_SubGraph_Crafting_Tutorial_Init_baseSpawnData_21, logic_SubGraph_Crafting_Tutorial_Init_blockSpawnData_21, logic_SubGraph_Crafting_Tutorial_Init_NPCSpawnData_21, logic_SubGraph_Crafting_Tutorial_Init_completedBasePreset_21, logic_SubGraph_Crafting_Tutorial_Init_useNPCAsTutorialTech_21, logic_SubGraph_Crafting_Tutorial_Init_spawnBase_21, logic_SubGraph_Crafting_Tutorial_Init_basePosition_21, logic_SubGraph_Crafting_Tutorial_Init_clearSceneryRadius_21, ref logic_SubGraph_Crafting_Tutorial_Init_CraftingBaseTech_21, ref logic_SubGraph_Crafting_Tutorial_Init_NPCTech_21);
	}

	private void Relay_In_28()
	{
		logic_uScript_LockTechStacks_tech_28 = local_CraftingBaseTech_Tank;
		logic_uScript_LockTechStacks_uScript_LockTechStacks_28.In(logic_uScript_LockTechStacks_tech_28);
		if (logic_uScript_LockTechStacks_uScript_LockTechStacks_28.Out)
		{
			Relay_In_352();
		}
	}

	private void Relay_Block_Attached_30()
	{
		Relay_In_169();
	}

	private void Relay_In_30()
	{
		logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_Block_30 = local_RefineryBlock_TankBlock;
		logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_CraftingBaseTech_30 = local_CraftingBaseTech_Tank;
		int num = 0;
		Array array = ghostBlockRefinery;
		if (logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_ghostBlockData_30.Length != num + array.Length)
		{
			Array.Resize(ref logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_ghostBlockData_30, num + array.Length);
		}
		Array.Copy(array, 0, logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_ghostBlockData_30, num, array.Length);
		num += array.Length;
		logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_SubGraph_Crafting_Tutorial_AttachBlockToBase_30.In(logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_Block_30, logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_CraftingBaseTech_30, logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_ghostBlockData_30, logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_BlockType_30, logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_BlockPosition_30);
	}

	private void Relay_In_34()
	{
		logic_uScript_LockTechInteraction_tech_34 = local_CraftingBaseTech_Tank;
		logic_uScript_LockTechInteraction_uScript_LockTechInteraction_34.In(logic_uScript_LockTechInteraction_tech_34, logic_uScript_LockTechInteraction_excludedBlocks_34, logic_uScript_LockTechInteraction_excludedUniqueBlocks_34);
		if (logic_uScript_LockTechInteraction_uScript_LockTechInteraction_34.Out)
		{
			Relay_In_28();
		}
	}

	private void Relay_Out_38()
	{
		Relay_In_41();
	}

	private void Relay_In_38()
	{
		int num = 0;
		Array array = blockSpawnData;
		if (logic_SubGraph_Crafting_Tutorial_ManageBlock_blockSpawnData_38.Length != num + array.Length)
		{
			Array.Resize(ref logic_SubGraph_Crafting_Tutorial_ManageBlock_blockSpawnData_38, num + array.Length);
		}
		Array.Copy(array, 0, logic_SubGraph_Crafting_Tutorial_ManageBlock_blockSpawnData_38, num, array.Length);
		num += array.Length;
		logic_SubGraph_Crafting_Tutorial_ManageBlock_block_38 = local_ReceiverBlock_TankBlock;
		logic_SubGraph_Crafting_Tutorial_ManageBlock_msgBlockOutsideArea_38 = msgBlockOutsideArea;
		logic_SubGraph_Crafting_Tutorial_ManageBlock_messageSpeaker_38 = messageSpeaker;
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_38.In(ref logic_SubGraph_Crafting_Tutorial_ManageBlock_blockSpawnData_38, logic_SubGraph_Crafting_Tutorial_ManageBlock_blockIndex_38, ref logic_SubGraph_Crafting_Tutorial_ManageBlock_block_38, logic_SubGraph_Crafting_Tutorial_ManageBlock_allowAnchoring_38, logic_SubGraph_Crafting_Tutorial_ManageBlock_msgBlockOutsideArea_38, logic_SubGraph_Crafting_Tutorial_ManageBlock_messageSpeaker_38);
	}

	private void Relay_Out_41()
	{
		Relay_In_44();
	}

	private void Relay_In_41()
	{
		int num = 0;
		Array array = blockSpawnData;
		if (logic_SubGraph_Crafting_Tutorial_ManageBlock_blockSpawnData_41.Length != num + array.Length)
		{
			Array.Resize(ref logic_SubGraph_Crafting_Tutorial_ManageBlock_blockSpawnData_41, num + array.Length);
		}
		Array.Copy(array, 0, logic_SubGraph_Crafting_Tutorial_ManageBlock_blockSpawnData_41, num, array.Length);
		num += array.Length;
		logic_SubGraph_Crafting_Tutorial_ManageBlock_block_41 = local_ConveyorBlock01_TankBlock;
		logic_SubGraph_Crafting_Tutorial_ManageBlock_msgBlockOutsideArea_41 = msgBlockOutsideArea;
		logic_SubGraph_Crafting_Tutorial_ManageBlock_messageSpeaker_41 = messageSpeaker;
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_41.In(ref logic_SubGraph_Crafting_Tutorial_ManageBlock_blockSpawnData_41, logic_SubGraph_Crafting_Tutorial_ManageBlock_blockIndex_41, ref logic_SubGraph_Crafting_Tutorial_ManageBlock_block_41, logic_SubGraph_Crafting_Tutorial_ManageBlock_allowAnchoring_41, logic_SubGraph_Crafting_Tutorial_ManageBlock_msgBlockOutsideArea_41, logic_SubGraph_Crafting_Tutorial_ManageBlock_messageSpeaker_41);
	}

	private void Relay_Out_44()
	{
		Relay_In_48();
	}

	private void Relay_In_44()
	{
		int num = 0;
		Array array = blockSpawnData;
		if (logic_SubGraph_Crafting_Tutorial_ManageBlock_blockSpawnData_44.Length != num + array.Length)
		{
			Array.Resize(ref logic_SubGraph_Crafting_Tutorial_ManageBlock_blockSpawnData_44, num + array.Length);
		}
		Array.Copy(array, 0, logic_SubGraph_Crafting_Tutorial_ManageBlock_blockSpawnData_44, num, array.Length);
		num += array.Length;
		logic_SubGraph_Crafting_Tutorial_ManageBlock_block_44 = local_ConveyorBlock02_TankBlock;
		logic_SubGraph_Crafting_Tutorial_ManageBlock_msgBlockOutsideArea_44 = msgBlockOutsideArea;
		logic_SubGraph_Crafting_Tutorial_ManageBlock_messageSpeaker_44 = messageSpeaker;
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_44.In(ref logic_SubGraph_Crafting_Tutorial_ManageBlock_blockSpawnData_44, logic_SubGraph_Crafting_Tutorial_ManageBlock_blockIndex_44, ref logic_SubGraph_Crafting_Tutorial_ManageBlock_block_44, logic_SubGraph_Crafting_Tutorial_ManageBlock_allowAnchoring_44, logic_SubGraph_Crafting_Tutorial_ManageBlock_msgBlockOutsideArea_44, logic_SubGraph_Crafting_Tutorial_ManageBlock_messageSpeaker_44);
	}

	private void Relay_Out_48()
	{
		Relay_In_50();
	}

	private void Relay_In_48()
	{
		int num = 0;
		Array array = blockSpawnData;
		if (logic_SubGraph_Crafting_Tutorial_ManageBlock_blockSpawnData_48.Length != num + array.Length)
		{
			Array.Resize(ref logic_SubGraph_Crafting_Tutorial_ManageBlock_blockSpawnData_48, num + array.Length);
		}
		Array.Copy(array, 0, logic_SubGraph_Crafting_Tutorial_ManageBlock_blockSpawnData_48, num, array.Length);
		num += array.Length;
		logic_SubGraph_Crafting_Tutorial_ManageBlock_block_48 = local_ConveyorBlock03_TankBlock;
		logic_SubGraph_Crafting_Tutorial_ManageBlock_msgBlockOutsideArea_48 = msgBlockOutsideArea;
		logic_SubGraph_Crafting_Tutorial_ManageBlock_messageSpeaker_48 = messageSpeaker;
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_48.In(ref logic_SubGraph_Crafting_Tutorial_ManageBlock_blockSpawnData_48, logic_SubGraph_Crafting_Tutorial_ManageBlock_blockIndex_48, ref logic_SubGraph_Crafting_Tutorial_ManageBlock_block_48, logic_SubGraph_Crafting_Tutorial_ManageBlock_allowAnchoring_48, logic_SubGraph_Crafting_Tutorial_ManageBlock_msgBlockOutsideArea_48, logic_SubGraph_Crafting_Tutorial_ManageBlock_messageSpeaker_48);
	}

	private void Relay_Out_50()
	{
		Relay_In_260();
	}

	private void Relay_In_50()
	{
		int num = 0;
		Array array = blockSpawnData;
		if (logic_SubGraph_Crafting_Tutorial_ManageBlock_blockSpawnData_50.Length != num + array.Length)
		{
			Array.Resize(ref logic_SubGraph_Crafting_Tutorial_ManageBlock_blockSpawnData_50, num + array.Length);
		}
		Array.Copy(array, 0, logic_SubGraph_Crafting_Tutorial_ManageBlock_blockSpawnData_50, num, array.Length);
		num += array.Length;
		logic_SubGraph_Crafting_Tutorial_ManageBlock_block_50 = local_ConveyorBlock04_TankBlock;
		logic_SubGraph_Crafting_Tutorial_ManageBlock_msgBlockOutsideArea_50 = msgBlockOutsideArea;
		logic_SubGraph_Crafting_Tutorial_ManageBlock_messageSpeaker_50 = messageSpeaker;
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_50.In(ref logic_SubGraph_Crafting_Tutorial_ManageBlock_blockSpawnData_50, logic_SubGraph_Crafting_Tutorial_ManageBlock_blockIndex_50, ref logic_SubGraph_Crafting_Tutorial_ManageBlock_block_50, logic_SubGraph_Crafting_Tutorial_ManageBlock_allowAnchoring_50, logic_SubGraph_Crafting_Tutorial_ManageBlock_msgBlockOutsideArea_50, logic_SubGraph_Crafting_Tutorial_ManageBlock_messageSpeaker_50);
	}

	private void Relay_In_52()
	{
		logic_uScript_PointArrowAtVisible_targetObject_52 = local_ConveyorBlock01_TankBlock;
		logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_52.In(logic_uScript_PointArrowAtVisible_targetObject_52, logic_uScript_PointArrowAtVisible_timeToShowFor_52, logic_uScript_PointArrowAtVisible_offset_52);
		if (logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_52.Out)
		{
			Relay_In_354();
		}
	}

	private void Relay_In_53()
	{
		logic_uScript_BlockAttachedToTech_tech_53 = local_CraftingBaseTech_Tank;
		logic_uScript_BlockAttachedToTech_block_53 = local_ConveyorBlock02_TankBlock;
		logic_uScript_BlockAttachedToTech_uScript_BlockAttachedToTech_53.In(logic_uScript_BlockAttachedToTech_tech_53, logic_uScript_BlockAttachedToTech_block_53);
		bool num = logic_uScript_BlockAttachedToTech_uScript_BlockAttachedToTech_53.True;
		bool flag = logic_uScript_BlockAttachedToTech_uScript_BlockAttachedToTech_53.False;
		if (num)
		{
			Relay_In_409();
		}
		if (flag)
		{
			Relay_In_55();
		}
	}

	private void Relay_In_55()
	{
		logic_uScript_PointArrowAtVisible_targetObject_55 = local_ConveyorBlock02_TankBlock;
		logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_55.In(logic_uScript_PointArrowAtVisible_targetObject_55, logic_uScript_PointArrowAtVisible_timeToShowFor_55, logic_uScript_PointArrowAtVisible_offset_55);
		if (logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_55.Out)
		{
			Relay_In_357();
		}
	}

	private void Relay_In_57()
	{
		logic_uScript_PointArrowAtVisible_targetObject_57 = local_ConveyorBlock03_TankBlock;
		logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_57.In(logic_uScript_PointArrowAtVisible_targetObject_57, logic_uScript_PointArrowAtVisible_timeToShowFor_57, logic_uScript_PointArrowAtVisible_offset_57);
		if (logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_57.Out)
		{
			Relay_In_359();
		}
	}

	private void Relay_In_58()
	{
		logic_uScript_BlockAttachedToTech_tech_58 = local_CraftingBaseTech_Tank;
		logic_uScript_BlockAttachedToTech_block_58 = local_ConveyorBlock03_TankBlock;
		logic_uScript_BlockAttachedToTech_uScript_BlockAttachedToTech_58.In(logic_uScript_BlockAttachedToTech_tech_58, logic_uScript_BlockAttachedToTech_block_58);
		bool num = logic_uScript_BlockAttachedToTech_uScript_BlockAttachedToTech_58.True;
		bool flag = logic_uScript_BlockAttachedToTech_uScript_BlockAttachedToTech_58.False;
		if (num)
		{
			Relay_In_410();
		}
		if (flag)
		{
			Relay_In_57();
		}
	}

	private void Relay_In_61()
	{
		logic_uScript_BlockAttachedToTech_tech_61 = local_CraftingBaseTech_Tank;
		logic_uScript_BlockAttachedToTech_block_61 = local_ConveyorBlock04_TankBlock;
		logic_uScript_BlockAttachedToTech_uScript_BlockAttachedToTech_61.In(logic_uScript_BlockAttachedToTech_tech_61, logic_uScript_BlockAttachedToTech_block_61);
		bool num = logic_uScript_BlockAttachedToTech_uScript_BlockAttachedToTech_61.True;
		bool flag = logic_uScript_BlockAttachedToTech_uScript_BlockAttachedToTech_61.False;
		if (num)
		{
			Relay_In_413();
		}
		if (flag)
		{
			Relay_In_64();
		}
	}

	private void Relay_In_64()
	{
		logic_uScript_PointArrowAtVisible_targetObject_64 = local_ConveyorBlock04_TankBlock;
		logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_64.In(logic_uScript_PointArrowAtVisible_targetObject_64, logic_uScript_PointArrowAtVisible_timeToShowFor_64, logic_uScript_PointArrowAtVisible_offset_64);
		if (logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_64.Out)
		{
			Relay_In_360();
		}
	}

	private void Relay_In_65()
	{
		logic_uScript_BlockAttachedToTech_tech_65 = local_CraftingBaseTech_Tank;
		logic_uScript_BlockAttachedToTech_block_65 = local_ConveyorBlock01_TankBlock;
		logic_uScript_BlockAttachedToTech_uScript_BlockAttachedToTech_65.In(logic_uScript_BlockAttachedToTech_tech_65, logic_uScript_BlockAttachedToTech_block_65);
		bool num = logic_uScript_BlockAttachedToTech_uScript_BlockAttachedToTech_65.True;
		bool flag = logic_uScript_BlockAttachedToTech_uScript_BlockAttachedToTech_65.False;
		if (num)
		{
			Relay_In_407();
		}
		if (flag)
		{
			Relay_In_52();
		}
	}

	private void Relay_In_69()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_69.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_69.Out)
		{
			Relay_In_71();
		}
	}

	private void Relay_In_71()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_71.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_71.Out)
		{
			Relay_True_75();
		}
	}

	private void Relay_In_72()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_72.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_72.Out)
		{
			Relay_In_69();
		}
	}

	private void Relay_In_73()
	{
		logic_uScript_IsPlayerInteractingWithBlock_block_73 = local_ConveyorBlock01_TankBlock;
		logic_uScript_IsPlayerInteractingWithBlock_uScript_IsPlayerInteractingWithBlock_73.In(logic_uScript_IsPlayerInteractingWithBlock_block_73);
		bool dragging = logic_uScript_IsPlayerInteractingWithBlock_uScript_IsPlayerInteractingWithBlock_73.Dragging;
		bool notDragging = logic_uScript_IsPlayerInteractingWithBlock_uScript_IsPlayerInteractingWithBlock_73.NotDragging;
		if (dragging)
		{
			Relay_In_72();
		}
		if (notDragging)
		{
			Relay_In_79();
		}
	}

	private void Relay_In_74()
	{
		logic_uScript_IsPlayerInteractingWithBlock_block_74 = local_ConveyorBlock04_TankBlock;
		logic_uScript_IsPlayerInteractingWithBlock_uScript_IsPlayerInteractingWithBlock_74.In(logic_uScript_IsPlayerInteractingWithBlock_block_74);
		bool dragging = logic_uScript_IsPlayerInteractingWithBlock_uScript_IsPlayerInteractingWithBlock_74.Dragging;
		bool notDragging = logic_uScript_IsPlayerInteractingWithBlock_uScript_IsPlayerInteractingWithBlock_74.NotDragging;
		if (dragging)
		{
			Relay_True_75();
		}
		if (notDragging)
		{
			Relay_False_75();
		}
	}

	private void Relay_True_75()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_75.True(out logic_uScriptAct_SetBool_Target_75);
		local_DraggingConveyor_System_Boolean = logic_uScriptAct_SetBool_Target_75;
		bool setTrue = logic_uScriptAct_SetBool_uScriptAct_SetBool_75.SetTrue;
		bool setFalse = logic_uScriptAct_SetBool_uScriptAct_SetBool_75.SetFalse;
		if (setTrue)
		{
			Relay_In_367();
		}
		if (setFalse)
		{
			Relay_In_381();
		}
	}

	private void Relay_False_75()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_75.False(out logic_uScriptAct_SetBool_Target_75);
		local_DraggingConveyor_System_Boolean = logic_uScriptAct_SetBool_Target_75;
		bool setTrue = logic_uScriptAct_SetBool_uScriptAct_SetBool_75.SetTrue;
		bool setFalse = logic_uScriptAct_SetBool_uScriptAct_SetBool_75.SetFalse;
		if (setTrue)
		{
			Relay_In_367();
		}
		if (setFalse)
		{
			Relay_In_381();
		}
	}

	private void Relay_In_77()
	{
		logic_uScript_IsPlayerInteractingWithBlock_block_77 = local_ConveyorBlock03_TankBlock;
		logic_uScript_IsPlayerInteractingWithBlock_uScript_IsPlayerInteractingWithBlock_77.In(logic_uScript_IsPlayerInteractingWithBlock_block_77);
		bool dragging = logic_uScript_IsPlayerInteractingWithBlock_uScript_IsPlayerInteractingWithBlock_77.Dragging;
		bool notDragging = logic_uScript_IsPlayerInteractingWithBlock_uScript_IsPlayerInteractingWithBlock_77.NotDragging;
		if (dragging)
		{
			Relay_In_71();
		}
		if (notDragging)
		{
			Relay_In_74();
		}
	}

	private void Relay_In_79()
	{
		logic_uScript_IsPlayerInteractingWithBlock_block_79 = local_ConveyorBlock02_TankBlock;
		logic_uScript_IsPlayerInteractingWithBlock_uScript_IsPlayerInteractingWithBlock_79.In(logic_uScript_IsPlayerInteractingWithBlock_block_79);
		bool dragging = logic_uScript_IsPlayerInteractingWithBlock_uScript_IsPlayerInteractingWithBlock_79.Dragging;
		bool notDragging = logic_uScript_IsPlayerInteractingWithBlock_uScript_IsPlayerInteractingWithBlock_79.NotDragging;
		if (dragging)
		{
			Relay_In_69();
		}
		if (notDragging)
		{
			Relay_In_77();
		}
	}

	private void Relay_True_81()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_81.True(out logic_uScriptAct_SetBool_Target_81);
		local_GhostBlockConveyor01Spawned_System_Boolean = logic_uScriptAct_SetBool_Target_81;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_81.Out)
		{
			Relay_TrySpawnOnTech_84();
		}
	}

	private void Relay_False_81()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_81.False(out logic_uScriptAct_SetBool_Target_81);
		local_GhostBlockConveyor01Spawned_System_Boolean = logic_uScriptAct_SetBool_Target_81;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_81.Out)
		{
			Relay_TrySpawnOnTech_84();
		}
	}

	private void Relay_TrySpawnOnTech_84()
	{
		int num = 0;
		Array array = ghostBlockConveyor01;
		if (logic_uScript_SpawnGhostBlocks_ghostBlockData_84.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SpawnGhostBlocks_ghostBlockData_84, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SpawnGhostBlocks_ghostBlockData_84, num, array.Length);
		num += array.Length;
		logic_uScript_SpawnGhostBlocks_ownerNode_84 = owner_Connection_85;
		logic_uScript_SpawnGhostBlocks_targetTech_84 = local_CraftingBaseTech_Tank;
		logic_uScript_SpawnGhostBlocks_Return_84 = logic_uScript_SpawnGhostBlocks_uScript_SpawnGhostBlocks_84.TrySpawnOnTech(logic_uScript_SpawnGhostBlocks_ghostBlockData_84, logic_uScript_SpawnGhostBlocks_ownerNode_84, logic_uScript_SpawnGhostBlocks_targetTech_84);
		local_83_TankBlockArray = logic_uScript_SpawnGhostBlocks_Return_84;
		if (logic_uScript_SpawnGhostBlocks_uScript_SpawnGhostBlocks_84.OnAlreadySpawned)
		{
			Relay_AtIndex_87();
		}
	}

	private void Relay_AtIndex_87()
	{
		int num = 0;
		Array array = local_83_TankBlockArray;
		if (logic_uScript_AccessListBlock_blockList_87.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListBlock_blockList_87, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListBlock_blockList_87, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListBlock_uScript_AccessListBlock_87.AtIndex(ref logic_uScript_AccessListBlock_blockList_87, logic_uScript_AccessListBlock_index_87, out logic_uScript_AccessListBlock_value_87);
		local_83_TankBlockArray = logic_uScript_AccessListBlock_blockList_87;
		local_GhostBlockConveyor01_TankBlock = logic_uScript_AccessListBlock_value_87;
		if (logic_uScript_AccessListBlock_uScript_AccessListBlock_87.Out)
		{
			Relay_In_123();
		}
	}

	private void Relay_In_88()
	{
		logic_uScriptCon_CompareBool_Bool_88 = local_GhostBlockConveyor01Spawned_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_88.In(logic_uScriptCon_CompareBool_Bool_88);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_88.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_88.False;
		if (num)
		{
			Relay_AtIndex_87();
		}
		if (flag)
		{
			Relay_True_81();
		}
	}

	private void Relay_In_90()
	{
		logic_uScriptCon_CompareBool_Bool_90 = local_GhostBlockConveyor02Spawned_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_90.In(logic_uScriptCon_CompareBool_Bool_90);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_90.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_90.False;
		if (num)
		{
			Relay_AtIndex_92();
		}
		if (flag)
		{
			Relay_True_94();
		}
	}

	private void Relay_AtIndex_92()
	{
		int num = 0;
		Array array = local_97_TankBlockArray;
		if (logic_uScript_AccessListBlock_blockList_92.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListBlock_blockList_92, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListBlock_blockList_92, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListBlock_uScript_AccessListBlock_92.AtIndex(ref logic_uScript_AccessListBlock_blockList_92, logic_uScript_AccessListBlock_index_92, out logic_uScript_AccessListBlock_value_92);
		local_97_TankBlockArray = logic_uScript_AccessListBlock_blockList_92;
		local_GhostBlockConveyor02_TankBlock = logic_uScript_AccessListBlock_value_92;
		if (logic_uScript_AccessListBlock_uScript_AccessListBlock_92.Out)
		{
			Relay_In_120();
		}
	}

	private void Relay_TrySpawnOnTech_93()
	{
		int num = 0;
		Array array = ghostBlockConveyor02;
		if (logic_uScript_SpawnGhostBlocks_ghostBlockData_93.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SpawnGhostBlocks_ghostBlockData_93, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SpawnGhostBlocks_ghostBlockData_93, num, array.Length);
		num += array.Length;
		logic_uScript_SpawnGhostBlocks_ownerNode_93 = owner_Connection_99;
		logic_uScript_SpawnGhostBlocks_targetTech_93 = local_CraftingBaseTech_Tank;
		logic_uScript_SpawnGhostBlocks_Return_93 = logic_uScript_SpawnGhostBlocks_uScript_SpawnGhostBlocks_93.TrySpawnOnTech(logic_uScript_SpawnGhostBlocks_ghostBlockData_93, logic_uScript_SpawnGhostBlocks_ownerNode_93, logic_uScript_SpawnGhostBlocks_targetTech_93);
		local_97_TankBlockArray = logic_uScript_SpawnGhostBlocks_Return_93;
		if (logic_uScript_SpawnGhostBlocks_uScript_SpawnGhostBlocks_93.OnAlreadySpawned)
		{
			Relay_AtIndex_92();
		}
	}

	private void Relay_True_94()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_94.True(out logic_uScriptAct_SetBool_Target_94);
		local_GhostBlockConveyor02Spawned_System_Boolean = logic_uScriptAct_SetBool_Target_94;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_94.Out)
		{
			Relay_TrySpawnOnTech_93();
		}
	}

	private void Relay_False_94()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_94.False(out logic_uScriptAct_SetBool_Target_94);
		local_GhostBlockConveyor02Spawned_System_Boolean = logic_uScriptAct_SetBool_Target_94;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_94.Out)
		{
			Relay_TrySpawnOnTech_93();
		}
	}

	private void Relay_In_100()
	{
		logic_uScriptCon_CompareBool_Bool_100 = local_GhostBlockConveyor03Spawned_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_100.In(logic_uScriptCon_CompareBool_Bool_100);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_100.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_100.False;
		if (num)
		{
			Relay_AtIndex_104();
		}
		if (flag)
		{
			Relay_True_110();
		}
	}

	private void Relay_In_102()
	{
		logic_uScriptCon_CompareBool_Bool_102 = local_GhostBlockConveyor04Spawned_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_102.In(logic_uScriptCon_CompareBool_Bool_102);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_102.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_102.False;
		if (num)
		{
			Relay_AtIndex_118();
		}
		if (flag)
		{
			Relay_True_112();
		}
	}

	private void Relay_AtIndex_104()
	{
		int num = 0;
		Array array = local_115_TankBlockArray;
		if (logic_uScript_AccessListBlock_blockList_104.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListBlock_blockList_104, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListBlock_blockList_104, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListBlock_uScript_AccessListBlock_104.AtIndex(ref logic_uScript_AccessListBlock_blockList_104, logic_uScript_AccessListBlock_index_104, out logic_uScript_AccessListBlock_value_104);
		local_115_TankBlockArray = logic_uScript_AccessListBlock_blockList_104;
		local_GhostBlockConveyor03_TankBlock = logic_uScript_AccessListBlock_value_104;
		if (logic_uScript_AccessListBlock_uScript_AccessListBlock_104.Out)
		{
			Relay_In_122();
		}
	}

	private void Relay_TrySpawnOnTech_108()
	{
		int num = 0;
		Array array = ghostBlockConveyor03;
		if (logic_uScript_SpawnGhostBlocks_ghostBlockData_108.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SpawnGhostBlocks_ghostBlockData_108, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SpawnGhostBlocks_ghostBlockData_108, num, array.Length);
		num += array.Length;
		logic_uScript_SpawnGhostBlocks_ownerNode_108 = owner_Connection_117;
		logic_uScript_SpawnGhostBlocks_targetTech_108 = local_CraftingBaseTech_Tank;
		logic_uScript_SpawnGhostBlocks_Return_108 = logic_uScript_SpawnGhostBlocks_uScript_SpawnGhostBlocks_108.TrySpawnOnTech(logic_uScript_SpawnGhostBlocks_ghostBlockData_108, logic_uScript_SpawnGhostBlocks_ownerNode_108, logic_uScript_SpawnGhostBlocks_targetTech_108);
		local_115_TankBlockArray = logic_uScript_SpawnGhostBlocks_Return_108;
		if (logic_uScript_SpawnGhostBlocks_uScript_SpawnGhostBlocks_108.OnAlreadySpawned)
		{
			Relay_AtIndex_104();
		}
	}

	private void Relay_True_110()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_110.True(out logic_uScriptAct_SetBool_Target_110);
		local_GhostBlockConveyor03Spawned_System_Boolean = logic_uScriptAct_SetBool_Target_110;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_110.Out)
		{
			Relay_TrySpawnOnTech_108();
		}
	}

	private void Relay_False_110()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_110.False(out logic_uScriptAct_SetBool_Target_110);
		local_GhostBlockConveyor03Spawned_System_Boolean = logic_uScriptAct_SetBool_Target_110;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_110.Out)
		{
			Relay_TrySpawnOnTech_108();
		}
	}

	private void Relay_True_112()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_112.True(out logic_uScriptAct_SetBool_Target_112);
		local_GhostBlockConveyor04Spawned_System_Boolean = logic_uScriptAct_SetBool_Target_112;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_112.Out)
		{
			Relay_TrySpawnOnTech_119();
		}
	}

	private void Relay_False_112()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_112.False(out logic_uScriptAct_SetBool_Target_112);
		local_GhostBlockConveyor04Spawned_System_Boolean = logic_uScriptAct_SetBool_Target_112;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_112.Out)
		{
			Relay_TrySpawnOnTech_119();
		}
	}

	private void Relay_AtIndex_118()
	{
		int num = 0;
		Array array = local_109_TankBlockArray;
		if (logic_uScript_AccessListBlock_blockList_118.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListBlock_blockList_118, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListBlock_blockList_118, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListBlock_uScript_AccessListBlock_118.AtIndex(ref logic_uScript_AccessListBlock_blockList_118, logic_uScript_AccessListBlock_index_118, out logic_uScript_AccessListBlock_value_118);
		local_109_TankBlockArray = logic_uScript_AccessListBlock_blockList_118;
		local_GhostBlockConveyor04_TankBlock = logic_uScript_AccessListBlock_value_118;
		if (logic_uScript_AccessListBlock_uScript_AccessListBlock_118.Out)
		{
			Relay_In_121();
		}
	}

	private void Relay_TrySpawnOnTech_119()
	{
		int num = 0;
		Array array = ghostBlockConveyor04;
		if (logic_uScript_SpawnGhostBlocks_ghostBlockData_119.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SpawnGhostBlocks_ghostBlockData_119, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SpawnGhostBlocks_ghostBlockData_119, num, array.Length);
		num += array.Length;
		logic_uScript_SpawnGhostBlocks_ownerNode_119 = owner_Connection_106;
		logic_uScript_SpawnGhostBlocks_targetTech_119 = local_CraftingBaseTech_Tank;
		logic_uScript_SpawnGhostBlocks_Return_119 = logic_uScript_SpawnGhostBlocks_uScript_SpawnGhostBlocks_119.TrySpawnOnTech(logic_uScript_SpawnGhostBlocks_ghostBlockData_119, logic_uScript_SpawnGhostBlocks_ownerNode_119, logic_uScript_SpawnGhostBlocks_targetTech_119);
		local_109_TankBlockArray = logic_uScript_SpawnGhostBlocks_Return_119;
		if (logic_uScript_SpawnGhostBlocks_uScript_SpawnGhostBlocks_119.OnAlreadySpawned)
		{
			Relay_AtIndex_118();
		}
	}

	private void Relay_In_120()
	{
		logic_uScript_PointArrowAtVisible_targetObject_120 = local_GhostBlockConveyor02_TankBlock;
		logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_120.In(logic_uScript_PointArrowAtVisible_targetObject_120, logic_uScript_PointArrowAtVisible_timeToShowFor_120, logic_uScript_PointArrowAtVisible_offset_120);
		if (logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_120.Out)
		{
			Relay_In_372();
		}
	}

	private void Relay_In_121()
	{
		logic_uScript_PointArrowAtVisible_targetObject_121 = local_GhostBlockConveyor04_TankBlock;
		logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_121.In(logic_uScript_PointArrowAtVisible_targetObject_121, logic_uScript_PointArrowAtVisible_timeToShowFor_121, logic_uScript_PointArrowAtVisible_offset_121);
		if (logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_121.Out)
		{
			Relay_In_376();
		}
	}

	private void Relay_In_122()
	{
		logic_uScript_PointArrowAtVisible_targetObject_122 = local_GhostBlockConveyor03_TankBlock;
		logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_122.In(logic_uScript_PointArrowAtVisible_targetObject_122, logic_uScript_PointArrowAtVisible_timeToShowFor_122, logic_uScript_PointArrowAtVisible_offset_122);
		if (logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_122.Out)
		{
			Relay_In_374();
		}
	}

	private void Relay_In_123()
	{
		logic_uScript_PointArrowAtVisible_targetObject_123 = local_GhostBlockConveyor01_TankBlock;
		logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_123.In(logic_uScript_PointArrowAtVisible_targetObject_123, logic_uScript_PointArrowAtVisible_timeToShowFor_123, logic_uScript_PointArrowAtVisible_offset_123);
		if (logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_123.Out)
		{
			Relay_In_370();
		}
	}

	private void Relay_True_125()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_125.True(out logic_uScriptAct_SetBool_Target_125);
		local_Resources01Spawned_System_Boolean = logic_uScriptAct_SetBool_Target_125;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_125.Out)
		{
			Relay_In_345();
		}
	}

	private void Relay_False_125()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_125.False(out logic_uScriptAct_SetBool_Target_125);
		local_Resources01Spawned_System_Boolean = logic_uScriptAct_SetBool_Target_125;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_125.Out)
		{
			Relay_In_345();
		}
	}

	private void Relay_In_127()
	{
		logic_uScriptCon_CompareBool_Bool_127 = local_Resources01Spawned_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_127.In(logic_uScriptCon_CompareBool_Bool_127);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_127.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_127.False;
		if (num)
		{
			Relay_In_225();
		}
		if (flag)
		{
			Relay_True_125();
		}
	}

	private void Relay_Block_Attached_129()
	{
		Relay_In_167();
	}

	private void Relay_In_129()
	{
		logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_Block_129 = local_ReceiverBlock_TankBlock;
		logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_CraftingBaseTech_129 = local_CraftingBaseTech_Tank;
		int num = 0;
		Array array = ghostBlockReceiver;
		if (logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_ghostBlockData_129.Length != num + array.Length)
		{
			Array.Resize(ref logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_ghostBlockData_129, num + array.Length);
		}
		Array.Copy(array, 0, logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_ghostBlockData_129, num, array.Length);
		num += array.Length;
		logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_SubGraph_Crafting_Tutorial_AttachBlockToBase_129.In(logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_Block_129, logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_CraftingBaseTech_129, logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_ghostBlockData_129, logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_BlockType_129, logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_BlockPosition_129);
	}

	private void Relay_Output1_132()
	{
		Relay_In_90();
	}

	private void Relay_Output2_132()
	{
		Relay_In_100();
	}

	private void Relay_Output3_132()
	{
		Relay_In_102();
	}

	private void Relay_Output4_132()
	{
	}

	private void Relay_Output5_132()
	{
	}

	private void Relay_Output6_132()
	{
	}

	private void Relay_Output7_132()
	{
	}

	private void Relay_Output8_132()
	{
	}

	private void Relay_In_132()
	{
		logic_uScriptCon_ManualSwitch_CurrentOutput_132 = local_NumConveyorsAttached_System_Int32;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_132.In(logic_uScriptCon_ManualSwitch_CurrentOutput_132);
	}

	private void Relay_In_134()
	{
		logic_uScriptAct_AddInt_v2_A_134 = local_NumConveyorsAttached_System_Int32;
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_134.In(logic_uScriptAct_AddInt_v2_A_134, logic_uScriptAct_AddInt_v2_B_134, out logic_uScriptAct_AddInt_v2_IntResult_134, out logic_uScriptAct_AddInt_v2_FloatResult_134);
		local_NumConveyorsAttached_System_Int32 = logic_uScriptAct_AddInt_v2_IntResult_134;
	}

	private void Relay_In_136()
	{
		logic_uScriptCon_CompareInt_A_136 = local_NumConveyorsAttached_System_Int32;
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_136.In(logic_uScriptCon_CompareInt_A_136, logic_uScriptCon_CompareInt_B_136);
		bool equalTo = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_136.EqualTo;
		bool notEqualTo = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_136.NotEqualTo;
		if (equalTo)
		{
			Relay_In_282();
		}
		if (notEqualTo)
		{
			Relay_In_181();
		}
	}

	private void Relay_In_138()
	{
		logic_uScriptCon_CompareInt_A_138 = local_NumConveyorsAttached_System_Int32;
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_138.In(logic_uScriptCon_CompareInt_A_138, logic_uScriptCon_CompareInt_B_138);
		bool equalTo = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_138.EqualTo;
		bool notEqualTo = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_138.NotEqualTo;
		if (equalTo)
		{
			Relay_In_88();
		}
		if (notEqualTo)
		{
			Relay_In_132();
		}
	}

	private void Relay_In_140()
	{
		logic_uScriptCon_CompareInt_A_140 = local_NumConveyorsAttached_System_Int32;
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_140.In(logic_uScriptCon_CompareInt_A_140, logic_uScriptCon_CompareInt_B_140);
		bool equalTo = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_140.EqualTo;
		bool notEqualTo = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_140.NotEqualTo;
		if (equalTo)
		{
			Relay_In_151();
		}
		if (notEqualTo)
		{
			Relay_In_142();
		}
	}

	private void Relay_Output1_142()
	{
		Relay_In_153();
	}

	private void Relay_Output2_142()
	{
		Relay_In_157();
	}

	private void Relay_Output3_142()
	{
		Relay_In_149();
	}

	private void Relay_Output4_142()
	{
	}

	private void Relay_Output5_142()
	{
	}

	private void Relay_Output6_142()
	{
	}

	private void Relay_Output7_142()
	{
	}

	private void Relay_Output8_142()
	{
	}

	private void Relay_In_142()
	{
		logic_uScriptCon_ManualSwitch_CurrentOutput_142 = local_NumConveyorsAttached_System_Int32;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_142.In(logic_uScriptCon_ManualSwitch_CurrentOutput_142);
	}

	private void Relay_In_144()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_144.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_144.Out)
		{
			Relay_In_145();
		}
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
			Relay_In_147();
		}
	}

	private void Relay_In_147()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_147.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_147.Out)
		{
			Relay_In_140();
		}
	}

	private void Relay_In_149()
	{
		logic_uScript_DoesTechHaveBlockAtPosition_tech_149 = local_CraftingBaseTech_Tank;
		logic_uScript_DoesTechHaveBlockAtPosition_blockType_149 = blockTypeConveyor;
		logic_uScript_DoesTechHaveBlockAtPosition_uScript_DoesTechHaveBlockAtPosition_149.In(logic_uScript_DoesTechHaveBlockAtPosition_tech_149, logic_uScript_DoesTechHaveBlockAtPosition_blockType_149, logic_uScript_DoesTechHaveBlockAtPosition_localPosition_149);
		if (logic_uScript_DoesTechHaveBlockAtPosition_uScript_DoesTechHaveBlockAtPosition_149.True)
		{
			Relay_In_161();
		}
	}

	private void Relay_In_151()
	{
		logic_uScript_DoesTechHaveBlockAtPosition_tech_151 = local_CraftingBaseTech_Tank;
		logic_uScript_DoesTechHaveBlockAtPosition_blockType_151 = blockTypeConveyor;
		logic_uScript_DoesTechHaveBlockAtPosition_uScript_DoesTechHaveBlockAtPosition_151.In(logic_uScript_DoesTechHaveBlockAtPosition_tech_151, logic_uScript_DoesTechHaveBlockAtPosition_blockType_151, logic_uScript_DoesTechHaveBlockAtPosition_localPosition_151);
		if (logic_uScript_DoesTechHaveBlockAtPosition_uScript_DoesTechHaveBlockAtPosition_151.True)
		{
			Relay_In_161();
		}
	}

	private void Relay_In_153()
	{
		logic_uScript_DoesTechHaveBlockAtPosition_tech_153 = local_CraftingBaseTech_Tank;
		logic_uScript_DoesTechHaveBlockAtPosition_blockType_153 = blockTypeConveyor;
		logic_uScript_DoesTechHaveBlockAtPosition_uScript_DoesTechHaveBlockAtPosition_153.In(logic_uScript_DoesTechHaveBlockAtPosition_tech_153, logic_uScript_DoesTechHaveBlockAtPosition_blockType_153, logic_uScript_DoesTechHaveBlockAtPosition_localPosition_153);
		if (logic_uScript_DoesTechHaveBlockAtPosition_uScript_DoesTechHaveBlockAtPosition_153.True)
		{
			Relay_In_161();
		}
	}

	private void Relay_In_157()
	{
		logic_uScript_DoesTechHaveBlockAtPosition_tech_157 = local_CraftingBaseTech_Tank;
		logic_uScript_DoesTechHaveBlockAtPosition_blockType_157 = blockTypeConveyor;
		logic_uScript_DoesTechHaveBlockAtPosition_uScript_DoesTechHaveBlockAtPosition_157.In(logic_uScript_DoesTechHaveBlockAtPosition_tech_157, logic_uScript_DoesTechHaveBlockAtPosition_blockType_157, logic_uScript_DoesTechHaveBlockAtPosition_localPosition_157);
		if (logic_uScript_DoesTechHaveBlockAtPosition_uScript_DoesTechHaveBlockAtPosition_157.True)
		{
			Relay_In_161();
		}
	}

	private void Relay_In_161()
	{
		logic_uScript_RemoveAllGhostBlocksOnTech_targetTech_161 = local_CraftingBaseTech_Tank;
		logic_uScript_RemoveAllGhostBlocksOnTech_uScript_RemoveAllGhostBlocksOnTech_161.In(logic_uScript_RemoveAllGhostBlocksOnTech_targetTech_161);
		if (logic_uScript_RemoveAllGhostBlocksOnTech_uScript_RemoveAllGhostBlocksOnTech_161.Out)
		{
			Relay_In_164();
		}
	}

	private void Relay_In_164()
	{
		logic_uScript_HideArrow_uScript_HideArrow_164.In();
		if (logic_uScript_HideArrow_uScript_HideArrow_164.Out)
		{
			Relay_In_134();
		}
	}

	private void Relay_In_165()
	{
		logic_uScript_HideArrow_uScript_HideArrow_165.In();
		if (logic_uScript_HideArrow_uScript_HideArrow_165.Out)
		{
			Relay_In_284();
		}
	}

	private void Relay_In_167()
	{
		logic_uScript_RemoveAllGhostBlocksOnTech_targetTech_167 = local_CraftingBaseTech_Tank;
		logic_uScript_RemoveAllGhostBlocksOnTech_uScript_RemoveAllGhostBlocksOnTech_167.In(logic_uScript_RemoveAllGhostBlocksOnTech_targetTech_167);
		if (logic_uScript_RemoveAllGhostBlocksOnTech_uScript_RemoveAllGhostBlocksOnTech_167.Out)
		{
			Relay_In_165();
		}
	}

	private void Relay_In_169()
	{
		logic_uScript_RemoveAllGhostBlocksOnTech_targetTech_169 = local_CraftingBaseTech_Tank;
		logic_uScript_RemoveAllGhostBlocksOnTech_uScript_RemoveAllGhostBlocksOnTech_169.In(logic_uScript_RemoveAllGhostBlocksOnTech_targetTech_169);
		if (logic_uScript_RemoveAllGhostBlocksOnTech_uScript_RemoveAllGhostBlocksOnTech_169.Out)
		{
			Relay_In_170();
		}
	}

	private void Relay_In_170()
	{
		logic_uScript_HideArrow_uScript_HideArrow_170.In();
		if (logic_uScript_HideArrow_uScript_HideArrow_170.Out)
		{
			Relay_In_286();
		}
	}

	private void Relay_In_172()
	{
		logic_uScriptCon_CompareBool_Bool_172 = local_msgIntroShown_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_172.In(logic_uScriptCon_CompareBool_Bool_172);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_172.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_172.False;
		if (num)
		{
			Relay_In_2();
		}
		if (flag)
		{
			Relay_True_174();
		}
	}

	private void Relay_True_174()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_174.True(out logic_uScriptAct_SetBool_Target_174);
		local_msgIntroShown_System_Boolean = logic_uScriptAct_SetBool_Target_174;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_174.Out)
		{
			Relay_In_287();
		}
	}

	private void Relay_False_174()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_174.False(out logic_uScriptAct_SetBool_Target_174);
		local_msgIntroShown_System_Boolean = logic_uScriptAct_SetBool_Target_174;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_174.Out)
		{
			Relay_In_287();
		}
	}

	private void Relay_Save_Out_177()
	{
		Relay_Save_269();
	}

	private void Relay_Load_Out_177()
	{
		Relay_Load_269();
	}

	private void Relay_Restart_Out_177()
	{
		Relay_Restart_269();
	}

	private void Relay_Save_177()
	{
		logic_SubGraph_SaveLoadInt_integer_177 = local_NumConveyorsAttached_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_177 = local_NumConveyorsAttached_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_177.Save(logic_SubGraph_SaveLoadInt_restartValue_177, ref logic_SubGraph_SaveLoadInt_integer_177, logic_SubGraph_SaveLoadInt_intAsVariable_177, logic_SubGraph_SaveLoadInt_uniqueID_177);
	}

	private void Relay_Load_177()
	{
		logic_SubGraph_SaveLoadInt_integer_177 = local_NumConveyorsAttached_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_177 = local_NumConveyorsAttached_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_177.Load(logic_SubGraph_SaveLoadInt_restartValue_177, ref logic_SubGraph_SaveLoadInt_integer_177, logic_SubGraph_SaveLoadInt_intAsVariable_177, logic_SubGraph_SaveLoadInt_uniqueID_177);
	}

	private void Relay_Restart_177()
	{
		logic_SubGraph_SaveLoadInt_integer_177 = local_NumConveyorsAttached_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_177 = local_NumConveyorsAttached_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_177.Restart(logic_SubGraph_SaveLoadInt_restartValue_177, ref logic_SubGraph_SaveLoadInt_integer_177, logic_SubGraph_SaveLoadInt_intAsVariable_177, logic_SubGraph_SaveLoadInt_uniqueID_177);
	}

	private void Relay_Save_Out_178()
	{
		Relay_Save_180();
	}

	private void Relay_Load_Out_178()
	{
		Relay_Load_180();
	}

	private void Relay_Restart_Out_178()
	{
		Relay_Set_False_180();
	}

	private void Relay_Save_178()
	{
		logic_SubGraph_SaveLoadBool_boolean_178 = local_Resources02Spawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_178 = local_Resources02Spawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_178.Save(ref logic_SubGraph_SaveLoadBool_boolean_178, logic_SubGraph_SaveLoadBool_boolAsVariable_178, logic_SubGraph_SaveLoadBool_uniqueID_178);
	}

	private void Relay_Load_178()
	{
		logic_SubGraph_SaveLoadBool_boolean_178 = local_Resources02Spawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_178 = local_Resources02Spawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_178.Load(ref logic_SubGraph_SaveLoadBool_boolean_178, logic_SubGraph_SaveLoadBool_boolAsVariable_178, logic_SubGraph_SaveLoadBool_uniqueID_178);
	}

	private void Relay_Set_True_178()
	{
		logic_SubGraph_SaveLoadBool_boolean_178 = local_Resources02Spawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_178 = local_Resources02Spawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_178.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_178, logic_SubGraph_SaveLoadBool_boolAsVariable_178, logic_SubGraph_SaveLoadBool_uniqueID_178);
	}

	private void Relay_Set_False_178()
	{
		logic_SubGraph_SaveLoadBool_boolean_178 = local_Resources02Spawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_178 = local_Resources02Spawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_178.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_178, logic_SubGraph_SaveLoadBool_boolAsVariable_178, logic_SubGraph_SaveLoadBool_uniqueID_178);
	}

	private void Relay_Save_Out_180()
	{
		Relay_Save_196();
	}

	private void Relay_Load_Out_180()
	{
		Relay_Load_196();
	}

	private void Relay_Restart_Out_180()
	{
		Relay_Set_False_196();
	}

	private void Relay_Save_180()
	{
		logic_SubGraph_SaveLoadBool_boolean_180 = local_msgIntroShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_180 = local_msgIntroShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_180.Save(ref logic_SubGraph_SaveLoadBool_boolean_180, logic_SubGraph_SaveLoadBool_boolAsVariable_180, logic_SubGraph_SaveLoadBool_uniqueID_180);
	}

	private void Relay_Load_180()
	{
		logic_SubGraph_SaveLoadBool_boolean_180 = local_msgIntroShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_180 = local_msgIntroShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_180.Load(ref logic_SubGraph_SaveLoadBool_boolean_180, logic_SubGraph_SaveLoadBool_boolAsVariable_180, logic_SubGraph_SaveLoadBool_uniqueID_180);
	}

	private void Relay_Set_True_180()
	{
		logic_SubGraph_SaveLoadBool_boolean_180 = local_msgIntroShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_180 = local_msgIntroShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_180.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_180, logic_SubGraph_SaveLoadBool_boolAsVariable_180, logic_SubGraph_SaveLoadBool_uniqueID_180);
	}

	private void Relay_Set_False_180()
	{
		logic_SubGraph_SaveLoadBool_boolean_180 = local_msgIntroShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_180 = local_msgIntroShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_180.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_180, logic_SubGraph_SaveLoadBool_boolAsVariable_180, logic_SubGraph_SaveLoadBool_uniqueID_180);
	}

	private void Relay_In_181()
	{
		logic_uScriptCon_CompareBool_Bool_181 = local_msgBaseFoundShown_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_181.In(logic_uScriptCon_CompareBool_Bool_181);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_181.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_181.False;
		if (num)
		{
			Relay_In_293();
		}
		if (flag)
		{
			Relay_In_292();
		}
	}

	private void Relay_True_183()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_183.True(out logic_uScriptAct_SetBool_Target_183);
		local_msgBaseFoundShown_System_Boolean = logic_uScriptAct_SetBool_Target_183;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_183.Out)
		{
			Relay_In_73();
		}
	}

	private void Relay_False_183()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_183.False(out logic_uScriptAct_SetBool_Target_183);
		local_msgBaseFoundShown_System_Boolean = logic_uScriptAct_SetBool_Target_183;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_183.Out)
		{
			Relay_In_73();
		}
	}

	private void Relay_In_185()
	{
		logic_uScriptCon_CompareBool_Bool_185 = local_msgConveyorsAttachedShown_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_185.In(logic_uScriptCon_CompareBool_Bool_185);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_185.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_185.False;
		if (num)
		{
			Relay_In_299();
		}
		if (flag)
		{
			Relay_In_297();
		}
	}

	private void Relay_True_188()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_188.True(out logic_uScriptAct_SetBool_Target_188);
		local_msgConveyorsAttachedShown_System_Boolean = logic_uScriptAct_SetBool_Target_188;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_188.Out)
		{
			Relay_In_129();
		}
	}

	private void Relay_False_188()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_188.False(out logic_uScriptAct_SetBool_Target_188);
		local_msgConveyorsAttachedShown_System_Boolean = logic_uScriptAct_SetBool_Target_188;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_188.Out)
		{
			Relay_In_129();
		}
	}

	private void Relay_In_189()
	{
		logic_uScriptCon_CompareBool_Bool_189 = local_msgUnrefinedChunksSoldShown_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_189.In(logic_uScriptCon_CompareBool_Bool_189);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_189.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_189.False;
		if (num)
		{
			Relay_In_201();
		}
		if (flag)
		{
			Relay_In_307();
		}
	}

	private void Relay_True_191()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_191.True(out logic_uScriptAct_SetBool_Target_191);
		local_msgUnrefinedChunksSoldShown_System_Boolean = logic_uScriptAct_SetBool_Target_191;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_191.Out)
		{
			Relay_In_201();
		}
	}

	private void Relay_False_191()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_191.False(out logic_uScriptAct_SetBool_Target_191);
		local_msgUnrefinedChunksSoldShown_System_Boolean = logic_uScriptAct_SetBool_Target_191;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_191.Out)
		{
			Relay_In_201();
		}
	}

	private void Relay_In_194()
	{
		logic_uScript_IsPlayerInRangeOfTech_tech_194 = local_CraftingBaseTech_Tank;
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_194.In(logic_uScript_IsPlayerInRangeOfTech_tech_194, logic_uScript_IsPlayerInRangeOfTech_range_194, logic_uScript_IsPlayerInRangeOfTech_techs_194);
		if (logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_194.InRange)
		{
			Relay_In_172();
		}
	}

	private void Relay_Save_Out_196()
	{
		Relay_Save_198();
	}

	private void Relay_Load_Out_196()
	{
		Relay_Load_198();
	}

	private void Relay_Restart_Out_196()
	{
		Relay_Set_False_198();
	}

	private void Relay_Save_196()
	{
		logic_SubGraph_SaveLoadBool_boolean_196 = local_msgConveyorsAttachedShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_196 = local_msgConveyorsAttachedShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_196.Save(ref logic_SubGraph_SaveLoadBool_boolean_196, logic_SubGraph_SaveLoadBool_boolAsVariable_196, logic_SubGraph_SaveLoadBool_uniqueID_196);
	}

	private void Relay_Load_196()
	{
		logic_SubGraph_SaveLoadBool_boolean_196 = local_msgConveyorsAttachedShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_196 = local_msgConveyorsAttachedShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_196.Load(ref logic_SubGraph_SaveLoadBool_boolean_196, logic_SubGraph_SaveLoadBool_boolAsVariable_196, logic_SubGraph_SaveLoadBool_uniqueID_196);
	}

	private void Relay_Set_True_196()
	{
		logic_SubGraph_SaveLoadBool_boolean_196 = local_msgConveyorsAttachedShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_196 = local_msgConveyorsAttachedShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_196.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_196, logic_SubGraph_SaveLoadBool_boolAsVariable_196, logic_SubGraph_SaveLoadBool_uniqueID_196);
	}

	private void Relay_Set_False_196()
	{
		logic_SubGraph_SaveLoadBool_boolean_196 = local_msgConveyorsAttachedShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_196 = local_msgConveyorsAttachedShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_196.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_196, logic_SubGraph_SaveLoadBool_boolAsVariable_196, logic_SubGraph_SaveLoadBool_uniqueID_196);
	}

	private void Relay_Save_Out_198()
	{
		Relay_Save_228();
	}

	private void Relay_Load_Out_198()
	{
		Relay_Load_228();
	}

	private void Relay_Restart_Out_198()
	{
		Relay_Set_False_228();
	}

	private void Relay_Save_198()
	{
		logic_SubGraph_SaveLoadBool_boolean_198 = local_msgUnrefinedChunksSoldShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_198 = local_msgUnrefinedChunksSoldShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_198.Save(ref logic_SubGraph_SaveLoadBool_boolean_198, logic_SubGraph_SaveLoadBool_boolAsVariable_198, logic_SubGraph_SaveLoadBool_uniqueID_198);
	}

	private void Relay_Load_198()
	{
		logic_SubGraph_SaveLoadBool_boolean_198 = local_msgUnrefinedChunksSoldShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_198 = local_msgUnrefinedChunksSoldShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_198.Load(ref logic_SubGraph_SaveLoadBool_boolean_198, logic_SubGraph_SaveLoadBool_boolAsVariable_198, logic_SubGraph_SaveLoadBool_uniqueID_198);
	}

	private void Relay_Set_True_198()
	{
		logic_SubGraph_SaveLoadBool_boolean_198 = local_msgUnrefinedChunksSoldShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_198 = local_msgUnrefinedChunksSoldShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_198.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_198, logic_SubGraph_SaveLoadBool_boolAsVariable_198, logic_SubGraph_SaveLoadBool_uniqueID_198);
	}

	private void Relay_Set_False_198()
	{
		logic_SubGraph_SaveLoadBool_boolean_198 = local_msgUnrefinedChunksSoldShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_198 = local_msgUnrefinedChunksSoldShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_198.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_198, logic_SubGraph_SaveLoadBool_boolAsVariable_198, logic_SubGraph_SaveLoadBool_uniqueID_198);
	}

	private void Relay_True_199()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_199.True(out logic_uScriptAct_SetBool_Target_199);
		local_RefinerySpawned_System_Boolean = logic_uScriptAct_SetBool_Target_199;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_199.Out)
		{
			Relay_In_202();
		}
	}

	private void Relay_False_199()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_199.False(out logic_uScriptAct_SetBool_Target_199);
		local_RefinerySpawned_System_Boolean = logic_uScriptAct_SetBool_Target_199;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_199.Out)
		{
			Relay_In_202();
		}
	}

	private void Relay_In_201()
	{
		logic_uScriptCon_CompareBool_Bool_201 = local_RefinerySpawned_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_201.In(logic_uScriptCon_CompareBool_Bool_201);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_201.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_201.False;
		if (num)
		{
			Relay_In_237();
		}
		if (flag)
		{
			Relay_True_199();
		}
	}

	private void Relay_In_202()
	{
		int num = 0;
		Array array = blockSpawnDataRefinery;
		if (logic_uScript_SpawnBlocksFromData_blockData_202.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SpawnBlocksFromData_blockData_202, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SpawnBlocksFromData_blockData_202, num, array.Length);
		num += array.Length;
		logic_uScript_SpawnBlocksFromData_ownerNode_202 = owner_Connection_203;
		logic_uScript_SpawnBlocksFromData_uScript_SpawnBlocksFromData_202.In(logic_uScript_SpawnBlocksFromData_blockData_202, logic_uScript_SpawnBlocksFromData_ownerNode_202);
		if (logic_uScript_SpawnBlocksFromData_uScript_SpawnBlocksFromData_202.Out)
		{
			Relay_In_237();
		}
	}

	private void Relay_In_205()
	{
		logic_uScriptCon_CompareBool_Bool_205 = local_Resources02Spawned_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_205.In(logic_uScriptCon_CompareBool_Bool_205);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_205.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_205.False;
		if (num)
		{
			Relay_In_252();
		}
		if (flag)
		{
			Relay_In_315();
		}
	}

	private void Relay_In_208()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_208 = messageTag;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_208.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_208, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_208);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_208.Out)
		{
			Relay_True_7();
		}
	}

	private void Relay_True_210()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_210.True(out logic_uScriptAct_SetBool_Target_210);
		local_NearBase_System_Boolean = logic_uScriptAct_SetBool_Target_210;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_210.Out)
		{
			Relay_In_266();
		}
	}

	private void Relay_False_210()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_210.False(out logic_uScriptAct_SetBool_Target_210);
		local_NearBase_System_Boolean = logic_uScriptAct_SetBool_Target_210;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_210.Out)
		{
			Relay_In_266();
		}
	}

	private void Relay_In_211()
	{
		logic_uScriptCon_CompareBool_Bool_211 = local_NearBase_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_211.In(logic_uScriptCon_CompareBool_Bool_211);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_211.True)
		{
			Relay_In_321();
		}
	}

	private void Relay_True_213()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_213.True(out logic_uScriptAct_SetBool_Target_213);
		local_NearBase_System_Boolean = logic_uScriptAct_SetBool_Target_213;
	}

	private void Relay_False_213()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_213.False(out logic_uScriptAct_SetBool_Target_213);
		local_NearBase_System_Boolean = logic_uScriptAct_SetBool_Target_213;
	}

	private void Relay_In_216()
	{
		logic_uScript_HideArrow_uScript_HideArrow_216.In();
		if (logic_uScript_HideArrow_uScript_HideArrow_216.Out)
		{
			Relay_In_416();
		}
	}

	private void Relay_In_218()
	{
		logic_uScriptCon_CompareBool_Bool_218 = local_msgRefinerySpawnedShown_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_218.In(logic_uScriptCon_CompareBool_Bool_218);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_218.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_218.False;
		if (num)
		{
			Relay_In_311();
		}
		if (flag)
		{
			Relay_In_308();
		}
	}

	private void Relay_True_219()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_219.True(out logic_uScriptAct_SetBool_Target_219);
		local_msgRefinerySpawnedShown_System_Boolean = logic_uScriptAct_SetBool_Target_219;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_219.Out)
		{
			Relay_In_30();
		}
	}

	private void Relay_False_219()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_219.False(out logic_uScriptAct_SetBool_Target_219);
		local_msgRefinerySpawnedShown_System_Boolean = logic_uScriptAct_SetBool_Target_219;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_219.Out)
		{
			Relay_In_30();
		}
	}

	private void Relay_True_221()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_221.True(out logic_uScriptAct_SetBool_Target_221);
		local_Resources01Sold_System_Boolean = logic_uScriptAct_SetBool_Target_221;
	}

	private void Relay_False_221()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_221.False(out logic_uScriptAct_SetBool_Target_221);
		local_Resources01Sold_System_Boolean = logic_uScriptAct_SetBool_Target_221;
	}

	private void Relay_In_222()
	{
		logic_uScriptCon_CompareBool_Bool_222 = local_Resources01Sold_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_222.In(logic_uScriptCon_CompareBool_Bool_222);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_222.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_222.False;
		if (num)
		{
			Relay_In_189();
		}
		if (flag)
		{
			Relay_In_232();
		}
	}

	private void Relay_In_225()
	{
		logic_uScript_Wait_seconds_225 = timeWaitBeforeResourceSold;
		logic_uScript_Wait_uScript_Wait_225.In(logic_uScript_Wait_seconds_225, logic_uScript_Wait_repeat_225);
		if (logic_uScript_Wait_uScript_Wait_225.Waited)
		{
			Relay_True_221();
		}
	}

	private void Relay_Save_Out_228()
	{
		Relay_Save_255();
	}

	private void Relay_Load_Out_228()
	{
		Relay_Load_255();
	}

	private void Relay_Restart_Out_228()
	{
		Relay_Set_False_255();
	}

	private void Relay_Save_228()
	{
		logic_SubGraph_SaveLoadBool_boolean_228 = local_Resources01Sold_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_228 = local_Resources01Sold_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_228.Save(ref logic_SubGraph_SaveLoadBool_boolean_228, logic_SubGraph_SaveLoadBool_boolAsVariable_228, logic_SubGraph_SaveLoadBool_uniqueID_228);
	}

	private void Relay_Load_228()
	{
		logic_SubGraph_SaveLoadBool_boolean_228 = local_Resources01Sold_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_228 = local_Resources01Sold_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_228.Load(ref logic_SubGraph_SaveLoadBool_boolean_228, logic_SubGraph_SaveLoadBool_boolAsVariable_228, logic_SubGraph_SaveLoadBool_uniqueID_228);
	}

	private void Relay_Set_True_228()
	{
		logic_SubGraph_SaveLoadBool_boolean_228 = local_Resources01Sold_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_228 = local_Resources01Sold_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_228.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_228, logic_SubGraph_SaveLoadBool_boolAsVariable_228, logic_SubGraph_SaveLoadBool_uniqueID_228);
	}

	private void Relay_Set_False_228()
	{
		logic_SubGraph_SaveLoadBool_boolean_228 = local_Resources01Sold_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_228 = local_Resources01Sold_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_228.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_228, logic_SubGraph_SaveLoadBool_boolAsVariable_228, logic_SubGraph_SaveLoadBool_uniqueID_228);
	}

	private void Relay_Save_Out_230()
	{
		Relay_Save_250();
	}

	private void Relay_Load_Out_230()
	{
		Relay_Load_250();
	}

	private void Relay_Restart_Out_230()
	{
		Relay_Set_False_250();
	}

	private void Relay_Save_230()
	{
		logic_SubGraph_SaveLoadBool_boolean_230 = local_msgRefinerySpawnedShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_230 = local_msgRefinerySpawnedShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_230.Save(ref logic_SubGraph_SaveLoadBool_boolean_230, logic_SubGraph_SaveLoadBool_boolAsVariable_230, logic_SubGraph_SaveLoadBool_uniqueID_230);
	}

	private void Relay_Load_230()
	{
		logic_SubGraph_SaveLoadBool_boolean_230 = local_msgRefinerySpawnedShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_230 = local_msgRefinerySpawnedShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_230.Load(ref logic_SubGraph_SaveLoadBool_boolean_230, logic_SubGraph_SaveLoadBool_boolAsVariable_230, logic_SubGraph_SaveLoadBool_uniqueID_230);
	}

	private void Relay_Set_True_230()
	{
		logic_SubGraph_SaveLoadBool_boolean_230 = local_msgRefinerySpawnedShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_230 = local_msgRefinerySpawnedShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_230.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_230, logic_SubGraph_SaveLoadBool_boolAsVariable_230, logic_SubGraph_SaveLoadBool_uniqueID_230);
	}

	private void Relay_Set_False_230()
	{
		logic_SubGraph_SaveLoadBool_boolean_230 = local_msgRefinerySpawnedShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_230 = local_msgRefinerySpawnedShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_230.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_230, logic_SubGraph_SaveLoadBool_boolAsVariable_230, logic_SubGraph_SaveLoadBool_uniqueID_230);
	}

	private void Relay_In_232()
	{
		logic_uScriptCon_CompareBool_Bool_232 = local_msgReceiverAttachedShown_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_232.In(logic_uScriptCon_CompareBool_Bool_232);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_232.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_232.False;
		if (num)
		{
			Relay_In_127();
		}
		if (flag)
		{
			Relay_In_304();
		}
	}

	private void Relay_True_233()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_233.True(out logic_uScriptAct_SetBool_Target_233);
		local_msgReceiverAttachedShown_System_Boolean = logic_uScriptAct_SetBool_Target_233;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_233.Out)
		{
			Relay_In_127();
		}
	}

	private void Relay_False_233()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_233.False(out logic_uScriptAct_SetBool_Target_233);
		local_msgReceiverAttachedShown_System_Boolean = logic_uScriptAct_SetBool_Target_233;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_233.Out)
		{
			Relay_In_127();
		}
	}

	private void Relay_Save_Out_236()
	{
	}

	private void Relay_Load_Out_236()
	{
		Relay_In_270();
	}

	private void Relay_Restart_Out_236()
	{
		Relay_False_240();
	}

	private void Relay_Save_236()
	{
		logic_SubGraph_SaveLoadBool_boolean_236 = local_msgReceiverAttachedShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_236 = local_msgReceiverAttachedShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_236.Save(ref logic_SubGraph_SaveLoadBool_boolean_236, logic_SubGraph_SaveLoadBool_boolAsVariable_236, logic_SubGraph_SaveLoadBool_uniqueID_236);
	}

	private void Relay_Load_236()
	{
		logic_SubGraph_SaveLoadBool_boolean_236 = local_msgReceiverAttachedShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_236 = local_msgReceiverAttachedShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_236.Load(ref logic_SubGraph_SaveLoadBool_boolean_236, logic_SubGraph_SaveLoadBool_boolAsVariable_236, logic_SubGraph_SaveLoadBool_uniqueID_236);
	}

	private void Relay_Set_True_236()
	{
		logic_SubGraph_SaveLoadBool_boolean_236 = local_msgReceiverAttachedShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_236 = local_msgReceiverAttachedShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_236.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_236, logic_SubGraph_SaveLoadBool_boolAsVariable_236, logic_SubGraph_SaveLoadBool_uniqueID_236);
	}

	private void Relay_Set_False_236()
	{
		logic_SubGraph_SaveLoadBool_boolean_236 = local_msgReceiverAttachedShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_236 = local_msgReceiverAttachedShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_236.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_236, logic_SubGraph_SaveLoadBool_boolAsVariable_236, logic_SubGraph_SaveLoadBool_uniqueID_236);
	}

	private void Relay_In_237()
	{
		int num = 0;
		Array array = blockSpawnDataRefinery;
		if (logic_uScript_GetAndCheckBlocks_blockData_237.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckBlocks_blockData_237, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckBlocks_blockData_237, num, array.Length);
		num += array.Length;
		logic_uScript_GetAndCheckBlocks_ownerNode_237 = owner_Connection_239;
		logic_uScript_GetAndCheckBlocks_uScript_GetAndCheckBlocks_237.In(logic_uScript_GetAndCheckBlocks_blockData_237, logic_uScript_GetAndCheckBlocks_ownerNode_237, ref logic_uScript_GetAndCheckBlocks_blocks_237);
		bool allAlive = logic_uScript_GetAndCheckBlocks_uScript_GetAndCheckBlocks_237.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckBlocks_uScript_GetAndCheckBlocks_237.SomeAlive;
		if (allAlive)
		{
			Relay_In_218();
		}
		if (someAlive)
		{
			Relay_In_218();
		}
	}

	private void Relay_True_240()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_240.True(out logic_uScriptAct_SetBool_Target_240);
		local_GhostBlockConveyor01Spawned_System_Boolean = logic_uScriptAct_SetBool_Target_240;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_240.Out)
		{
			Relay_False_243();
		}
	}

	private void Relay_False_240()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_240.False(out logic_uScriptAct_SetBool_Target_240);
		local_GhostBlockConveyor01Spawned_System_Boolean = logic_uScriptAct_SetBool_Target_240;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_240.Out)
		{
			Relay_False_243();
		}
	}

	private void Relay_True_243()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_243.True(out logic_uScriptAct_SetBool_Target_243);
		local_GhostBlockConveyor02Spawned_System_Boolean = logic_uScriptAct_SetBool_Target_243;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_243.Out)
		{
			Relay_False_246();
		}
	}

	private void Relay_False_243()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_243.False(out logic_uScriptAct_SetBool_Target_243);
		local_GhostBlockConveyor02Spawned_System_Boolean = logic_uScriptAct_SetBool_Target_243;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_243.Out)
		{
			Relay_False_246();
		}
	}

	private void Relay_True_246()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_246.True(out logic_uScriptAct_SetBool_Target_246);
		local_GhostBlockConveyor03Spawned_System_Boolean = logic_uScriptAct_SetBool_Target_246;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_246.Out)
		{
			Relay_False_248();
		}
	}

	private void Relay_False_246()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_246.False(out logic_uScriptAct_SetBool_Target_246);
		local_GhostBlockConveyor03Spawned_System_Boolean = logic_uScriptAct_SetBool_Target_246;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_246.Out)
		{
			Relay_False_248();
		}
	}

	private void Relay_True_248()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_248.True(out logic_uScriptAct_SetBool_Target_248);
		local_GhostBlockConveyor04Spawned_System_Boolean = logic_uScriptAct_SetBool_Target_248;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_248.Out)
		{
			Relay_False_258();
		}
	}

	private void Relay_False_248()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_248.False(out logic_uScriptAct_SetBool_Target_248);
		local_GhostBlockConveyor04Spawned_System_Boolean = logic_uScriptAct_SetBool_Target_248;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_248.Out)
		{
			Relay_False_258();
		}
	}

	private void Relay_Save_Out_250()
	{
		Relay_Save_236();
	}

	private void Relay_Load_Out_250()
	{
		Relay_Load_236();
	}

	private void Relay_Restart_Out_250()
	{
		Relay_Set_False_236();
	}

	private void Relay_Save_250()
	{
		logic_SubGraph_SaveLoadBool_boolean_250 = local_msgBaseFoundShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_250 = local_msgBaseFoundShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_250.Save(ref logic_SubGraph_SaveLoadBool_boolean_250, logic_SubGraph_SaveLoadBool_boolAsVariable_250, logic_SubGraph_SaveLoadBool_uniqueID_250);
	}

	private void Relay_Load_250()
	{
		logic_SubGraph_SaveLoadBool_boolean_250 = local_msgBaseFoundShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_250 = local_msgBaseFoundShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_250.Load(ref logic_SubGraph_SaveLoadBool_boolean_250, logic_SubGraph_SaveLoadBool_boolAsVariable_250, logic_SubGraph_SaveLoadBool_uniqueID_250);
	}

	private void Relay_Set_True_250()
	{
		logic_SubGraph_SaveLoadBool_boolean_250 = local_msgBaseFoundShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_250 = local_msgBaseFoundShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_250.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_250, logic_SubGraph_SaveLoadBool_boolAsVariable_250, logic_SubGraph_SaveLoadBool_uniqueID_250);
	}

	private void Relay_Set_False_250()
	{
		logic_SubGraph_SaveLoadBool_boolean_250 = local_msgBaseFoundShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_250 = local_msgBaseFoundShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_250.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_250, logic_SubGraph_SaveLoadBool_boolAsVariable_250, logic_SubGraph_SaveLoadBool_uniqueID_250);
	}

	private void Relay_In_252()
	{
		logic_uScriptCon_CompareBool_Bool_252 = local_Resources02Sold_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_252.In(logic_uScriptCon_CompareBool_Bool_252);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_252.True)
		{
			Relay_In_319();
		}
	}

	private void Relay_True_253()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_253.True(out logic_uScriptAct_SetBool_Target_253);
		local_Resources02Sold_System_Boolean = logic_uScriptAct_SetBool_Target_253;
	}

	private void Relay_False_253()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_253.False(out logic_uScriptAct_SetBool_Target_253);
		local_Resources02Sold_System_Boolean = logic_uScriptAct_SetBool_Target_253;
	}

	private void Relay_Save_Out_255()
	{
		Relay_Save_256();
	}

	private void Relay_Load_Out_255()
	{
		Relay_Load_256();
	}

	private void Relay_Restart_Out_255()
	{
		Relay_Set_False_256();
	}

	private void Relay_Save_255()
	{
		logic_SubGraph_SaveLoadBool_boolean_255 = local_Resources02Sold_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_255 = local_Resources02Sold_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_255.Save(ref logic_SubGraph_SaveLoadBool_boolean_255, logic_SubGraph_SaveLoadBool_boolAsVariable_255, logic_SubGraph_SaveLoadBool_uniqueID_255);
	}

	private void Relay_Load_255()
	{
		logic_SubGraph_SaveLoadBool_boolean_255 = local_Resources02Sold_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_255 = local_Resources02Sold_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_255.Load(ref logic_SubGraph_SaveLoadBool_boolean_255, logic_SubGraph_SaveLoadBool_boolAsVariable_255, logic_SubGraph_SaveLoadBool_uniqueID_255);
	}

	private void Relay_Set_True_255()
	{
		logic_SubGraph_SaveLoadBool_boolean_255 = local_Resources02Sold_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_255 = local_Resources02Sold_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_255.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_255, logic_SubGraph_SaveLoadBool_boolAsVariable_255, logic_SubGraph_SaveLoadBool_uniqueID_255);
	}

	private void Relay_Set_False_255()
	{
		logic_SubGraph_SaveLoadBool_boolean_255 = local_Resources02Sold_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_255 = local_Resources02Sold_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_255.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_255, logic_SubGraph_SaveLoadBool_boolAsVariable_255, logic_SubGraph_SaveLoadBool_uniqueID_255);
	}

	private void Relay_Save_Out_256()
	{
		Relay_Save_230();
	}

	private void Relay_Load_Out_256()
	{
		Relay_Load_230();
	}

	private void Relay_Restart_Out_256()
	{
		Relay_Set_False_230();
	}

	private void Relay_Save_256()
	{
		logic_SubGraph_SaveLoadBool_boolean_256 = local_RefinerySpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_256 = local_RefinerySpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_256.Save(ref logic_SubGraph_SaveLoadBool_boolean_256, logic_SubGraph_SaveLoadBool_boolAsVariable_256, logic_SubGraph_SaveLoadBool_uniqueID_256);
	}

	private void Relay_Load_256()
	{
		logic_SubGraph_SaveLoadBool_boolean_256 = local_RefinerySpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_256 = local_RefinerySpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_256.Load(ref logic_SubGraph_SaveLoadBool_boolean_256, logic_SubGraph_SaveLoadBool_boolAsVariable_256, logic_SubGraph_SaveLoadBool_uniqueID_256);
	}

	private void Relay_Set_True_256()
	{
		logic_SubGraph_SaveLoadBool_boolean_256 = local_RefinerySpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_256 = local_RefinerySpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_256.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_256, logic_SubGraph_SaveLoadBool_boolAsVariable_256, logic_SubGraph_SaveLoadBool_uniqueID_256);
	}

	private void Relay_Set_False_256()
	{
		logic_SubGraph_SaveLoadBool_boolean_256 = local_RefinerySpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_256 = local_RefinerySpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_256.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_256, logic_SubGraph_SaveLoadBool_boolAsVariable_256, logic_SubGraph_SaveLoadBool_uniqueID_256);
	}

	private void Relay_True_258()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_258.True(out logic_uScriptAct_SetBool_Target_258);
		local_NearBase_System_Boolean = logic_uScriptAct_SetBool_Target_258;
	}

	private void Relay_False_258()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_258.False(out logic_uScriptAct_SetBool_Target_258);
		local_NearBase_System_Boolean = logic_uScriptAct_SetBool_Target_258;
	}

	private void Relay_In_260()
	{
		logic_uScriptCon_CompareBool_Bool_260 = local_RefinerySpawned_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_260.In(logic_uScriptCon_CompareBool_Bool_260);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_260.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_260.False;
		if (num)
		{
			Relay_In_262();
		}
		if (flag)
		{
			Relay_In_265();
		}
	}

	private void Relay_Out_262()
	{
		Relay_In_34();
	}

	private void Relay_In_262()
	{
		int num = 0;
		Array array = blockSpawnDataRefinery;
		if (logic_SubGraph_Crafting_Tutorial_ManageBlock_blockSpawnData_262.Length != num + array.Length)
		{
			Array.Resize(ref logic_SubGraph_Crafting_Tutorial_ManageBlock_blockSpawnData_262, num + array.Length);
		}
		Array.Copy(array, 0, logic_SubGraph_Crafting_Tutorial_ManageBlock_blockSpawnData_262, num, array.Length);
		num += array.Length;
		logic_SubGraph_Crafting_Tutorial_ManageBlock_block_262 = local_RefineryBlock_TankBlock;
		logic_SubGraph_Crafting_Tutorial_ManageBlock_msgBlockOutsideArea_262 = msgBlockOutsideArea;
		logic_SubGraph_Crafting_Tutorial_ManageBlock_messageSpeaker_262 = messageSpeaker;
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_262.In(ref logic_SubGraph_Crafting_Tutorial_ManageBlock_blockSpawnData_262, logic_SubGraph_Crafting_Tutorial_ManageBlock_blockIndex_262, ref logic_SubGraph_Crafting_Tutorial_ManageBlock_block_262, logic_SubGraph_Crafting_Tutorial_ManageBlock_allowAnchoring_262, logic_SubGraph_Crafting_Tutorial_ManageBlock_msgBlockOutsideArea_262, logic_SubGraph_Crafting_Tutorial_ManageBlock_messageSpeaker_262);
	}

	private void Relay_In_265()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_265.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_265.Out)
		{
			Relay_In_34();
		}
	}

	private void Relay_Output1_266()
	{
		Relay_In_279();
	}

	private void Relay_Output2_266()
	{
		Relay_In_136();
	}

	private void Relay_Output3_266()
	{
		Relay_In_185();
	}

	private void Relay_Output4_266()
	{
		Relay_In_222();
	}

	private void Relay_Output5_266()
	{
		Relay_In_205();
	}

	private void Relay_Output6_266()
	{
	}

	private void Relay_Output7_266()
	{
	}

	private void Relay_Output8_266()
	{
	}

	private void Relay_In_266()
	{
		logic_uScriptCon_ManualSwitch_CurrentOutput_266 = local_Stage_System_Int32;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_266.In(logic_uScriptCon_ManualSwitch_CurrentOutput_266);
	}

	private void Relay_Save_Out_269()
	{
		Relay_Save_11();
	}

	private void Relay_Load_Out_269()
	{
		Relay_Load_11();
	}

	private void Relay_Restart_Out_269()
	{
		Relay_Set_False_11();
	}

	private void Relay_Save_269()
	{
		logic_SubGraph_SaveLoadInt_integer_269 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_269 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_269.Save(logic_SubGraph_SaveLoadInt_restartValue_269, ref logic_SubGraph_SaveLoadInt_integer_269, logic_SubGraph_SaveLoadInt_intAsVariable_269, logic_SubGraph_SaveLoadInt_uniqueID_269);
	}

	private void Relay_Load_269()
	{
		logic_SubGraph_SaveLoadInt_integer_269 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_269 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_269.Load(logic_SubGraph_SaveLoadInt_restartValue_269, ref logic_SubGraph_SaveLoadInt_integer_269, logic_SubGraph_SaveLoadInt_intAsVariable_269, logic_SubGraph_SaveLoadInt_uniqueID_269);
	}

	private void Relay_Restart_269()
	{
		logic_SubGraph_SaveLoadInt_integer_269 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_269 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_269.Restart(logic_SubGraph_SaveLoadInt_restartValue_269, ref logic_SubGraph_SaveLoadInt_integer_269, logic_SubGraph_SaveLoadInt_intAsVariable_269, logic_SubGraph_SaveLoadInt_uniqueID_269);
	}

	private void Relay_Out_270()
	{
	}

	private void Relay_In_270()
	{
		logic_SubGraph_LoadObjectiveStates_currentObjective_270 = local_Stage_System_Int32;
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_270.In(logic_SubGraph_LoadObjectiveStates_currentObjective_270);
	}

	private void Relay_Out_279()
	{
		Relay_In_453();
	}

	private void Relay_In_279()
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_279 = local_Stage_System_Int32;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_279.In(ref logic_SubGraph_CompleteObjectiveStage_objectiveStage_279, logic_SubGraph_CompleteObjectiveStage_isFinalObjective_279);
	}

	private void Relay_Out_282()
	{
		Relay_In_398();
	}

	private void Relay_In_282()
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_282 = local_Stage_System_Int32;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_282.In(ref logic_SubGraph_CompleteObjectiveStage_objectiveStage_282, logic_SubGraph_CompleteObjectiveStage_isFinalObjective_282);
	}

	private void Relay_Out_284()
	{
	}

	private void Relay_In_284()
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_284 = local_Stage_System_Int32;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_284.In(ref logic_SubGraph_CompleteObjectiveStage_objectiveStage_284, logic_SubGraph_CompleteObjectiveStage_isFinalObjective_284);
	}

	private void Relay_Out_286()
	{
	}

	private void Relay_In_286()
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_286 = local_Stage_System_Int32;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_286.In(ref logic_SubGraph_CompleteObjectiveStage_objectiveStage_286, logic_SubGraph_CompleteObjectiveStage_isFinalObjective_286);
	}

	private void Relay_In_287()
	{
		logic_uScript_AddMessage_messageData_287 = msg01Intro;
		logic_uScript_AddMessage_speaker_287 = messageSpeaker;
		logic_uScript_AddMessage_Return_287 = logic_uScript_AddMessage_uScript_AddMessage_287.In(logic_uScript_AddMessage_messageData_287, logic_uScript_AddMessage_speaker_287);
		if (logic_uScript_AddMessage_uScript_AddMessage_287.Out)
		{
			Relay_In_2();
		}
	}

	private void Relay_In_292()
	{
		logic_uScript_AddMessage_messageData_292 = msg02BaseFound;
		logic_uScript_AddMessage_speaker_292 = messageSpeaker;
		logic_uScript_AddMessage_Return_292 = logic_uScript_AddMessage_uScript_AddMessage_292.In(logic_uScript_AddMessage_messageData_292, logic_uScript_AddMessage_speaker_292);
		if (logic_uScript_AddMessage_uScript_AddMessage_292.Shown)
		{
			Relay_True_183();
		}
	}

	private void Relay_In_293()
	{
		logic_uScript_AddMessage_messageData_293 = msg03AttachConveyors;
		logic_uScript_AddMessage_speaker_293 = messageSpeaker;
		logic_uScript_AddMessage_Return_293 = logic_uScript_AddMessage_uScript_AddMessage_293.In(logic_uScript_AddMessage_messageData_293, logic_uScript_AddMessage_speaker_293);
		if (logic_uScript_AddMessage_uScript_AddMessage_293.Out)
		{
			Relay_In_73();
		}
	}

	private void Relay_In_297()
	{
		logic_uScript_AddMessage_messageData_297 = msg04ConveyorsAttached;
		logic_uScript_AddMessage_speaker_297 = messageSpeaker;
		logic_uScript_AddMessage_Return_297 = logic_uScript_AddMessage_uScript_AddMessage_297.In(logic_uScript_AddMessage_messageData_297, logic_uScript_AddMessage_speaker_297);
		if (logic_uScript_AddMessage_uScript_AddMessage_297.Shown)
		{
			Relay_True_188();
		}
	}

	private void Relay_In_299()
	{
		logic_uScript_AddMessage_messageData_299 = msg05AttachReceiver;
		logic_uScript_AddMessage_speaker_299 = messageSpeaker;
		logic_uScript_AddMessage_Return_299 = logic_uScript_AddMessage_uScript_AddMessage_299.In(logic_uScript_AddMessage_messageData_299, logic_uScript_AddMessage_speaker_299);
		if (logic_uScript_AddMessage_uScript_AddMessage_299.Out)
		{
			Relay_In_129();
		}
	}

	private void Relay_In_304()
	{
		logic_uScript_AddMessage_messageData_304 = msg06ReceiverAttached;
		logic_uScript_AddMessage_speaker_304 = messageSpeaker;
		logic_uScript_AddMessage_Return_304 = logic_uScript_AddMessage_uScript_AddMessage_304.In(logic_uScript_AddMessage_messageData_304, logic_uScript_AddMessage_speaker_304);
		if (logic_uScript_AddMessage_uScript_AddMessage_304.Shown)
		{
			Relay_True_233();
		}
	}

	private void Relay_In_307()
	{
		logic_uScript_AddMessage_messageData_307 = msg07UnrefinedChunksSold;
		logic_uScript_AddMessage_speaker_307 = messageSpeaker;
		logic_uScript_AddMessage_Return_307 = logic_uScript_AddMessage_uScript_AddMessage_307.In(logic_uScript_AddMessage_messageData_307, logic_uScript_AddMessage_speaker_307);
		if (logic_uScript_AddMessage_uScript_AddMessage_307.Shown)
		{
			Relay_True_191();
		}
	}

	private void Relay_In_308()
	{
		logic_uScript_AddMessage_messageData_308 = msg08RefinerySpawned;
		logic_uScript_AddMessage_speaker_308 = messageSpeaker;
		logic_uScript_AddMessage_Return_308 = logic_uScript_AddMessage_uScript_AddMessage_308.In(logic_uScript_AddMessage_messageData_308, logic_uScript_AddMessage_speaker_308);
		if (logic_uScript_AddMessage_uScript_AddMessage_308.Shown)
		{
			Relay_True_219();
		}
	}

	private void Relay_In_311()
	{
		logic_uScript_AddMessage_messageData_311 = msg09AttachRefinery;
		logic_uScript_AddMessage_speaker_311 = messageSpeaker;
		logic_uScript_AddMessage_Return_311 = logic_uScript_AddMessage_uScript_AddMessage_311.In(logic_uScript_AddMessage_messageData_311, logic_uScript_AddMessage_speaker_311);
		if (logic_uScript_AddMessage_uScript_AddMessage_311.Out)
		{
			Relay_In_30();
		}
	}

	private void Relay_In_315()
	{
		logic_uScript_AddMessage_messageData_315 = msg10RefineryAttached;
		logic_uScript_AddMessage_speaker_315 = messageSpeaker;
		logic_uScript_AddMessage_Return_315 = logic_uScript_AddMessage_uScript_AddMessage_315.In(logic_uScript_AddMessage_messageData_315, logic_uScript_AddMessage_speaker_315);
		if (logic_uScript_AddMessage_uScript_AddMessage_315.Shown)
		{
			Relay_In_348();
		}
	}

	private void Relay_In_319()
	{
		logic_uScript_AddMessage_messageData_319 = msg11Complete;
		logic_uScript_AddMessage_speaker_319 = messageSpeaker;
		logic_uScript_AddMessage_Return_319 = logic_uScript_AddMessage_uScript_AddMessage_319.In(logic_uScript_AddMessage_messageData_319, logic_uScript_AddMessage_speaker_319);
		if (logic_uScript_AddMessage_uScript_AddMessage_319.Shown)
		{
			Relay_In_335();
		}
	}

	private void Relay_In_321()
	{
		logic_uScript_AddMessage_messageData_321 = msgLeavingMissionArea;
		logic_uScript_AddMessage_speaker_321 = messageSpeaker;
		logic_uScript_AddMessage_Return_321 = logic_uScript_AddMessage_uScript_AddMessage_321.In(logic_uScript_AddMessage_messageData_321, logic_uScript_AddMessage_speaker_321);
		if (logic_uScript_AddMessage_uScript_AddMessage_321.Out)
		{
			Relay_False_213();
		}
	}

	private void Relay_Out_335()
	{
		Relay_In_351();
	}

	private void Relay_In_335()
	{
		logic_SubGraph_Crafting_Tutorial_Finish_craftingBaseTech_335 = local_CraftingBaseTech_Tank;
		logic_SubGraph_Crafting_Tutorial_Finish_NPCTech_335 = local_NPCTech_Tank;
		logic_SubGraph_Crafting_Tutorial_Finish_NPCFlyAwayAI_335 = NPCFlyAwayAI;
		logic_SubGraph_Crafting_Tutorial_Finish_NPCDespawnParticleEffect_335 = NPCDespawnParticleEffect;
		logic_SubGraph_Crafting_Tutorial_Finish_SubGraph_Crafting_Tutorial_Finish_335.In(logic_SubGraph_Crafting_Tutorial_Finish_craftingBaseTech_335, logic_SubGraph_Crafting_Tutorial_Finish_NPCTech_335, logic_SubGraph_Crafting_Tutorial_Finish_NPCFlyAwayAI_335, logic_SubGraph_Crafting_Tutorial_Finish_NPCDespawnParticleEffect_335);
	}

	private void Relay_In_342()
	{
		logic_uScript_SetEncounterTarget_owner_342 = owner_Connection_343;
		logic_uScript_SetEncounterTarget_visibleObject_342 = local_NPCTech_Tank;
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_342.In(logic_uScript_SetEncounterTarget_owner_342, logic_uScript_SetEncounterTarget_visibleObject_342);
		if (logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_342.Out)
		{
			Relay_In_194();
		}
	}

	private void Relay_In_345()
	{
		logic_uScript_SpawnResourceListOnHolder_tech_345 = local_CraftingBaseTech_Tank;
		int num = 0;
		Array array = resourceList;
		if (logic_uScript_SpawnResourceListOnHolder_chunks_345.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SpawnResourceListOnHolder_chunks_345, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SpawnResourceListOnHolder_chunks_345, num, array.Length);
		num += array.Length;
		logic_uScript_SpawnResourceListOnHolder_uScript_SpawnResourceListOnHolder_345.In(logic_uScript_SpawnResourceListOnHolder_tech_345, logic_uScript_SpawnResourceListOnHolder_chunks_345, logic_uScript_SpawnResourceListOnHolder_blockType_345);
		if (logic_uScript_SpawnResourceListOnHolder_uScript_SpawnResourceListOnHolder_345.Out)
		{
			Relay_In_225();
		}
	}

	private void Relay_In_348()
	{
		logic_uScript_SpawnResourceListOnHolder_tech_348 = local_CraftingBaseTech_Tank;
		int num = 0;
		Array array = resourceList;
		if (logic_uScript_SpawnResourceListOnHolder_chunks_348.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SpawnResourceListOnHolder_chunks_348, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SpawnResourceListOnHolder_chunks_348, num, array.Length);
		num += array.Length;
		logic_uScript_SpawnResourceListOnHolder_uScript_SpawnResourceListOnHolder_348.In(logic_uScript_SpawnResourceListOnHolder_tech_348, logic_uScript_SpawnResourceListOnHolder_chunks_348, logic_uScript_SpawnResourceListOnHolder_blockType_348);
		if (logic_uScript_SpawnResourceListOnHolder_uScript_SpawnResourceListOnHolder_348.Out)
		{
			Relay_In_208();
		}
	}

	private void Relay_In_351()
	{
		logic_uScript_ShowHint_uScript_ShowHint_351.In(logic_uScript_ShowHint_hintId_351);
		if (logic_uScript_ShowHint_uScript_ShowHint_351.Out)
		{
			Relay_In_454();
		}
	}

	private void Relay_In_352()
	{
		logic_uScript_RestrictItemPickup_tech_352 = local_CraftingBaseTech_Tank;
		logic_uScript_RestrictItemPickup_uScript_RestrictItemPickup_352.In(logic_uScript_RestrictItemPickup_tech_352, logic_uScript_RestrictItemPickup_typesToAccept_352);
		if (logic_uScript_RestrictItemPickup_uScript_RestrictItemPickup_352.Out)
		{
			Relay_In_342();
		}
	}

	private void Relay_In_354()
	{
		logic_uScript_EnableGlow_targetObject_354 = local_ConveyorBlock01_TankBlock;
		logic_uScript_EnableGlow_uScript_EnableGlow_354.In(logic_uScript_EnableGlow_targetObject_354, logic_uScript_EnableGlow_enable_354);
		if (logic_uScript_EnableGlow_uScript_EnableGlow_354.Out)
		{
			Relay_In_144();
		}
	}

	private void Relay_In_357()
	{
		logic_uScript_EnableGlow_targetObject_357 = local_ConveyorBlock02_TankBlock;
		logic_uScript_EnableGlow_uScript_EnableGlow_357.In(logic_uScript_EnableGlow_targetObject_357, logic_uScript_EnableGlow_enable_357);
		if (logic_uScript_EnableGlow_uScript_EnableGlow_357.Out)
		{
			Relay_In_145();
		}
	}

	private void Relay_In_359()
	{
		logic_uScript_EnableGlow_targetObject_359 = local_ConveyorBlock03_TankBlock;
		logic_uScript_EnableGlow_uScript_EnableGlow_359.In(logic_uScript_EnableGlow_targetObject_359, logic_uScript_EnableGlow_enable_359);
		if (logic_uScript_EnableGlow_uScript_EnableGlow_359.Out)
		{
			Relay_In_146();
		}
	}

	private void Relay_In_360()
	{
		logic_uScript_EnableGlow_targetObject_360 = local_ConveyorBlock04_TankBlock;
		logic_uScript_EnableGlow_uScript_EnableGlow_360.In(logic_uScript_EnableGlow_targetObject_360, logic_uScript_EnableGlow_enable_360);
		if (logic_uScript_EnableGlow_uScript_EnableGlow_360.Out)
		{
			Relay_In_140();
		}
	}

	private void Relay_In_363()
	{
		logic_uScript_EnableGlow_targetObject_363 = local_ConveyorBlock04_TankBlock;
		logic_uScript_EnableGlow_uScript_EnableGlow_363.In(logic_uScript_EnableGlow_targetObject_363, logic_uScript_EnableGlow_enable_363);
		if (logic_uScript_EnableGlow_uScript_EnableGlow_363.Out)
		{
			Relay_In_138();
		}
	}

	private void Relay_In_364()
	{
		logic_uScript_EnableGlow_targetObject_364 = local_ConveyorBlock03_TankBlock;
		logic_uScript_EnableGlow_uScript_EnableGlow_364.In(logic_uScript_EnableGlow_targetObject_364, logic_uScript_EnableGlow_enable_364);
		if (logic_uScript_EnableGlow_uScript_EnableGlow_364.Out)
		{
			Relay_In_363();
		}
	}

	private void Relay_In_366()
	{
		logic_uScript_EnableGlow_targetObject_366 = local_ConveyorBlock02_TankBlock;
		logic_uScript_EnableGlow_uScript_EnableGlow_366.In(logic_uScript_EnableGlow_targetObject_366, logic_uScript_EnableGlow_enable_366);
		if (logic_uScript_EnableGlow_uScript_EnableGlow_366.Out)
		{
			Relay_In_364();
		}
	}

	private void Relay_In_367()
	{
		logic_uScript_EnableGlow_targetObject_367 = local_ConveyorBlock01_TankBlock;
		logic_uScript_EnableGlow_uScript_EnableGlow_367.In(logic_uScript_EnableGlow_targetObject_367, logic_uScript_EnableGlow_enable_367);
		if (logic_uScript_EnableGlow_uScript_EnableGlow_367.Out)
		{
			Relay_In_366();
		}
	}

	private void Relay_In_370()
	{
		logic_uScript_EnableGlow_targetObject_370 = local_GhostBlockConveyor01_TankBlock;
		logic_uScript_EnableGlow_uScript_EnableGlow_370.In(logic_uScript_EnableGlow_targetObject_370, logic_uScript_EnableGlow_enable_370);
	}

	private void Relay_In_372()
	{
		logic_uScript_EnableGlow_targetObject_372 = local_GhostBlockConveyor02_TankBlock;
		logic_uScript_EnableGlow_uScript_EnableGlow_372.In(logic_uScript_EnableGlow_targetObject_372, logic_uScript_EnableGlow_enable_372);
	}

	private void Relay_In_374()
	{
		logic_uScript_EnableGlow_targetObject_374 = local_GhostBlockConveyor03_TankBlock;
		logic_uScript_EnableGlow_uScript_EnableGlow_374.In(logic_uScript_EnableGlow_targetObject_374, logic_uScript_EnableGlow_enable_374);
	}

	private void Relay_In_376()
	{
		logic_uScript_EnableGlow_targetObject_376 = local_GhostBlockConveyor04_TankBlock;
		logic_uScript_EnableGlow_uScript_EnableGlow_376.In(logic_uScript_EnableGlow_targetObject_376, logic_uScript_EnableGlow_enable_376);
	}

	private void Relay_In_378()
	{
		logic_uScript_EnableGlow_targetObject_378 = local_GhostBlockConveyor01_TankBlock;
		logic_uScript_EnableGlow_uScript_EnableGlow_378.In(logic_uScript_EnableGlow_targetObject_378, logic_uScript_EnableGlow_enable_378);
		if (logic_uScript_EnableGlow_uScript_EnableGlow_378.Out)
		{
			Relay_In_384();
		}
	}

	private void Relay_In_381()
	{
		logic_uScriptCon_CompareBool_Bool_381 = local_GhostBlockConveyor01Spawned_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_381.In(logic_uScriptCon_CompareBool_Bool_381);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_381.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_381.False;
		if (num)
		{
			Relay_In_378();
		}
		if (flag)
		{
			Relay_In_394();
		}
	}

	private void Relay_In_382()
	{
		logic_uScript_EnableGlow_targetObject_382 = local_GhostBlockConveyor02_TankBlock;
		logic_uScript_EnableGlow_uScript_EnableGlow_382.In(logic_uScript_EnableGlow_targetObject_382, logic_uScript_EnableGlow_enable_382);
		if (logic_uScript_EnableGlow_uScript_EnableGlow_382.Out)
		{
			Relay_In_388();
		}
	}

	private void Relay_In_384()
	{
		logic_uScriptCon_CompareBool_Bool_384 = local_GhostBlockConveyor02Spawned_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_384.In(logic_uScriptCon_CompareBool_Bool_384);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_384.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_384.False;
		if (num)
		{
			Relay_In_382();
		}
		if (flag)
		{
			Relay_In_395();
		}
	}

	private void Relay_In_386()
	{
		logic_uScript_EnableGlow_targetObject_386 = local_GhostBlockConveyor03_TankBlock;
		logic_uScript_EnableGlow_uScript_EnableGlow_386.In(logic_uScript_EnableGlow_targetObject_386, logic_uScript_EnableGlow_enable_386);
		if (logic_uScript_EnableGlow_uScript_EnableGlow_386.Out)
		{
			Relay_In_392();
		}
	}

	private void Relay_In_388()
	{
		logic_uScriptCon_CompareBool_Bool_388 = local_GhostBlockConveyor03Spawned_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_388.In(logic_uScriptCon_CompareBool_Bool_388);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_388.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_388.False;
		if (num)
		{
			Relay_In_386();
		}
		if (flag)
		{
			Relay_In_397();
		}
	}

	private void Relay_In_390()
	{
		logic_uScript_EnableGlow_targetObject_390 = local_GhostBlockConveyor04_TankBlock;
		logic_uScript_EnableGlow_uScript_EnableGlow_390.In(logic_uScript_EnableGlow_targetObject_390, logic_uScript_EnableGlow_enable_390);
		if (logic_uScript_EnableGlow_uScript_EnableGlow_390.Out)
		{
			Relay_In_65();
		}
	}

	private void Relay_In_392()
	{
		logic_uScriptCon_CompareBool_Bool_392 = local_GhostBlockConveyor04Spawned_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_392.In(logic_uScriptCon_CompareBool_Bool_392);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_392.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_392.False;
		if (num)
		{
			Relay_In_390();
		}
		if (flag)
		{
			Relay_In_396();
		}
	}

	private void Relay_In_394()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_394.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_394.Out)
		{
			Relay_In_384();
		}
	}

	private void Relay_In_395()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_395.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_395.Out)
		{
			Relay_In_388();
		}
	}

	private void Relay_In_396()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_396.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_396.Out)
		{
			Relay_In_65();
		}
	}

	private void Relay_In_397()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_397.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_397.Out)
		{
			Relay_In_392();
		}
	}

	private void Relay_In_398()
	{
		logic_uScript_EnableGlow_targetObject_398 = local_ConveyorBlock01_TankBlock;
		logic_uScript_EnableGlow_uScript_EnableGlow_398.In(logic_uScript_EnableGlow_targetObject_398, logic_uScript_EnableGlow_enable_398);
		if (logic_uScript_EnableGlow_uScript_EnableGlow_398.Out)
		{
			Relay_In_401();
		}
	}

	private void Relay_In_399()
	{
		logic_uScript_EnableGlow_targetObject_399 = local_ConveyorBlock04_TankBlock;
		logic_uScript_EnableGlow_uScript_EnableGlow_399.In(logic_uScript_EnableGlow_targetObject_399, logic_uScript_EnableGlow_enable_399);
	}

	private void Relay_In_401()
	{
		logic_uScript_EnableGlow_targetObject_401 = local_ConveyorBlock02_TankBlock;
		logic_uScript_EnableGlow_uScript_EnableGlow_401.In(logic_uScript_EnableGlow_targetObject_401, logic_uScript_EnableGlow_enable_401);
		if (logic_uScript_EnableGlow_uScript_EnableGlow_401.Out)
		{
			Relay_In_403();
		}
	}

	private void Relay_In_403()
	{
		logic_uScript_EnableGlow_targetObject_403 = local_ConveyorBlock03_TankBlock;
		logic_uScript_EnableGlow_uScript_EnableGlow_403.In(logic_uScript_EnableGlow_targetObject_403, logic_uScript_EnableGlow_enable_403);
		if (logic_uScript_EnableGlow_uScript_EnableGlow_403.Out)
		{
			Relay_In_399();
		}
	}

	private void Relay_In_407()
	{
		logic_uScript_EnableGlow_targetObject_407 = local_ConveyorBlock01_TankBlock;
		logic_uScript_EnableGlow_uScript_EnableGlow_407.In(logic_uScript_EnableGlow_targetObject_407, logic_uScript_EnableGlow_enable_407);
		if (logic_uScript_EnableGlow_uScript_EnableGlow_407.Out)
		{
			Relay_In_53();
		}
	}

	private void Relay_In_409()
	{
		logic_uScript_EnableGlow_targetObject_409 = local_ConveyorBlock02_TankBlock;
		logic_uScript_EnableGlow_uScript_EnableGlow_409.In(logic_uScript_EnableGlow_targetObject_409, logic_uScript_EnableGlow_enable_409);
		if (logic_uScript_EnableGlow_uScript_EnableGlow_409.Out)
		{
			Relay_In_58();
		}
	}

	private void Relay_In_410()
	{
		logic_uScript_EnableGlow_targetObject_410 = local_ConveyorBlock03_TankBlock;
		logic_uScript_EnableGlow_uScript_EnableGlow_410.In(logic_uScript_EnableGlow_targetObject_410, logic_uScript_EnableGlow_enable_410);
		if (logic_uScript_EnableGlow_uScript_EnableGlow_410.Out)
		{
			Relay_In_61();
		}
	}

	private void Relay_In_413()
	{
		logic_uScript_EnableGlow_targetObject_413 = local_ConveyorBlock04_TankBlock;
		logic_uScript_EnableGlow_uScript_EnableGlow_413.In(logic_uScript_EnableGlow_targetObject_413, logic_uScript_EnableGlow_enable_413);
		if (logic_uScript_EnableGlow_uScript_EnableGlow_413.Out)
		{
			Relay_In_140();
		}
	}

	private void Relay_In_416()
	{
		logic_uScript_EnableGlow_targetObject_416 = local_ConveyorBlock01_TankBlock;
		logic_uScript_EnableGlow_uScript_EnableGlow_416.In(logic_uScript_EnableGlow_targetObject_416, logic_uScript_EnableGlow_enable_416);
		if (logic_uScript_EnableGlow_uScript_EnableGlow_416.Out)
		{
			Relay_In_419();
		}
	}

	private void Relay_In_417()
	{
		logic_uScript_EnableGlow_targetObject_417 = local_ConveyorBlock04_TankBlock;
		logic_uScript_EnableGlow_uScript_EnableGlow_417.In(logic_uScript_EnableGlow_targetObject_417, logic_uScript_EnableGlow_enable_417);
		if (logic_uScript_EnableGlow_uScript_EnableGlow_417.Out)
		{
			Relay_In_452();
		}
	}

	private void Relay_In_419()
	{
		logic_uScript_EnableGlow_targetObject_419 = local_ConveyorBlock02_TankBlock;
		logic_uScript_EnableGlow_uScript_EnableGlow_419.In(logic_uScript_EnableGlow_targetObject_419, logic_uScript_EnableGlow_enable_419);
		if (logic_uScript_EnableGlow_uScript_EnableGlow_419.Out)
		{
			Relay_In_421();
		}
	}

	private void Relay_In_421()
	{
		logic_uScript_EnableGlow_targetObject_421 = local_ConveyorBlock03_TankBlock;
		logic_uScript_EnableGlow_uScript_EnableGlow_421.In(logic_uScript_EnableGlow_targetObject_421, logic_uScript_EnableGlow_enable_421);
		if (logic_uScript_EnableGlow_uScript_EnableGlow_421.Out)
		{
			Relay_In_417();
		}
	}

	private void Relay_In_424()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_424.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_424.Out)
		{
			Relay_In_432();
		}
	}

	private void Relay_In_426()
	{
		logic_uScript_EnableGlow_targetObject_426 = local_GhostBlockConveyor02_TankBlock;
		logic_uScript_EnableGlow_uScript_EnableGlow_426.In(logic_uScript_EnableGlow_targetObject_426, logic_uScript_EnableGlow_enable_426);
		if (logic_uScript_EnableGlow_uScript_EnableGlow_426.Out)
		{
			Relay_In_442();
		}
	}

	private void Relay_In_428()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_428.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_428.Out)
		{
			Relay_In_442();
		}
	}

	private void Relay_In_429()
	{
		logic_uScript_EnableGlow_targetObject_429 = local_GhostBlockConveyor01_TankBlock;
		logic_uScript_EnableGlow_uScript_EnableGlow_429.In(logic_uScript_EnableGlow_targetObject_429, logic_uScript_EnableGlow_enable_429);
		if (logic_uScript_EnableGlow_uScript_EnableGlow_429.Out)
		{
			Relay_In_443();
		}
	}

	private void Relay_In_430()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_430.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_430.Out)
		{
			Relay_In_446();
		}
	}

	private void Relay_In_431()
	{
		logic_uScript_EnableGlow_targetObject_431 = local_GhostBlockConveyor04_TankBlock;
		logic_uScript_EnableGlow_uScript_EnableGlow_431.In(logic_uScript_EnableGlow_targetObject_431, logic_uScript_EnableGlow_enable_431);
		if (logic_uScript_EnableGlow_uScript_EnableGlow_431.Out)
		{
			Relay_In_446();
		}
	}

	private void Relay_In_432()
	{
		logic_uScriptCon_CompareBool_Bool_432 = local_GhostBlockConveyor04Spawned_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_432.In(logic_uScriptCon_CompareBool_Bool_432);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_432.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_432.False;
		if (num)
		{
			Relay_In_431();
		}
		if (flag)
		{
			Relay_In_430();
		}
	}

	private void Relay_In_436()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_436.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_436.Out)
		{
			Relay_In_443();
		}
	}

	private void Relay_In_439()
	{
		logic_uScriptCon_CompareBool_Bool_439 = local_GhostBlockConveyor01Spawned_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_439.In(logic_uScriptCon_CompareBool_Bool_439);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_439.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_439.False;
		if (num)
		{
			Relay_In_429();
		}
		if (flag)
		{
			Relay_In_436();
		}
	}

	private void Relay_In_442()
	{
		logic_uScriptCon_CompareBool_Bool_442 = local_GhostBlockConveyor03Spawned_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_442.In(logic_uScriptCon_CompareBool_Bool_442);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_442.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_442.False;
		if (num)
		{
			Relay_In_444();
		}
		if (flag)
		{
			Relay_In_424();
		}
	}

	private void Relay_In_443()
	{
		logic_uScriptCon_CompareBool_Bool_443 = local_GhostBlockConveyor02Spawned_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_443.In(logic_uScriptCon_CompareBool_Bool_443);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_443.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_443.False;
		if (num)
		{
			Relay_In_426();
		}
		if (flag)
		{
			Relay_In_428();
		}
	}

	private void Relay_In_444()
	{
		logic_uScript_EnableGlow_targetObject_444 = local_GhostBlockConveyor03_TankBlock;
		logic_uScript_EnableGlow_uScript_EnableGlow_444.In(logic_uScript_EnableGlow_targetObject_444, logic_uScript_EnableGlow_enable_444);
		if (logic_uScript_EnableGlow_uScript_EnableGlow_444.Out)
		{
			Relay_In_432();
		}
	}

	private void Relay_In_446()
	{
		logic_uScriptCon_CompareBool_Bool_446 = local_RefinerySpawned_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_446.In(logic_uScriptCon_CompareBool_Bool_446);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_446.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_446.False;
		if (num)
		{
			Relay_In_449();
		}
		if (flag)
		{
			Relay_In_450();
		}
	}

	private void Relay_In_449()
	{
		logic_uScript_EnableGlow_targetObject_449 = local_RefineryBlock_TankBlock;
		logic_uScript_EnableGlow_uScript_EnableGlow_449.In(logic_uScript_EnableGlow_targetObject_449, logic_uScript_EnableGlow_enable_449);
		if (logic_uScript_EnableGlow_uScript_EnableGlow_449.Out)
		{
			Relay_In_211();
		}
	}

	private void Relay_In_450()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_450.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_450.Out)
		{
			Relay_In_211();
		}
	}

	private void Relay_In_452()
	{
		logic_uScript_EnableGlow_targetObject_452 = local_ReceiverBlock_TankBlock;
		logic_uScript_EnableGlow_uScript_EnableGlow_452.In(logic_uScript_EnableGlow_targetObject_452, logic_uScript_EnableGlow_enable_452);
		if (logic_uScript_EnableGlow_uScript_EnableGlow_452.Out)
		{
			Relay_In_439();
		}
	}

	private void Relay_In_453()
	{
		logic_uScript_SendAnaliticsEvent_uScript_SendAnaliticsEvent_453.In(logic_uScript_SendAnaliticsEvent_analiticsEvent_453, logic_uScript_SendAnaliticsEvent_parameterName_453, logic_uScript_SendAnaliticsEvent_parameter_453);
	}

	private void Relay_In_454()
	{
		logic_uScript_SendAnaliticsEvent_uScript_SendAnaliticsEvent_454.In(logic_uScript_SendAnaliticsEvent_analiticsEvent_454, logic_uScript_SendAnaliticsEvent_parameterName_454, logic_uScript_SendAnaliticsEvent_parameter_454);
	}
}
