using System;
using BehaviorDesigner.Runtime;
using UnityEngine;

[Serializable]
[NodePath("Graphs")]
[FriendlyName("Untitled", "")]
public class Mission_GSO_2_Crafting_04 : uScriptLogic
{
	private GameObject parentGameObject;

	private uScript_GUI thisScriptsOnGuiListener;

	private bool m_RegisteredForEvents;

	[Multiline(3)]
	public string basePosition = "";

	public SpawnTechData[] baseSpawnData = new SpawnTechData[0];

	public SpawnBlockData[] blockSpawnData = new SpawnBlockData[0];

	public SpawnBlockData[] blockSpawnDataScrapper = new SpawnBlockData[0];

	public float clearSceneryRadius;

	public TankPreset completedBasePreset;

	public float distBaseFound;

	public GhostBlockSpawnData[] ghostBlockScrapper = new GhostBlockSpawnData[0];

	private bool local_BlockInScrapper_System_Boolean;

	private Tank local_CraftingBaseTech_Tank;

	private bool local_msgBaseFoundShown_System_Boolean;

	private bool local_msgIntroShown_System_Boolean;

	private bool local_msgScrapperAttachedShown_System_Boolean;

	private bool local_msgScrapperSpawnedShown_System_Boolean;

	private bool local_NearBase_System_Boolean;

	private Tank local_NPCTech_Tank;

	private TankBlock local_ScrapperBlock_TankBlock;

	private bool local_ScrapperSpawned_System_Boolean;

	private bool local_ScrappingInProgress_System_Boolean;

	private int local_Stage_System_Int32 = 1;

	public uScript_AddMessage.MessageSpeaker messageSpeaker;

	[Multiline(3)]
	public string messageTag = "";

	public uScript_AddMessage.MessageData msg01Intro;

	public uScript_AddMessage.MessageData msg02BaseFound;

	public uScript_AddMessage.MessageData msg03ScrapperSpawned;

	public uScript_AddMessage.MessageData msg04AttachScrapper;

	public uScript_AddMessage.MessageData msg05ScrapperAttached;

	public uScript_AddMessage.MessageData msg06PutBlockInScrapper;

	public uScript_AddMessage.MessageData msg07ScrappingInProgress;

	public uScript_AddMessage.MessageData msg08Complete;

	public uScript_AddMessage.MessageData msgBlockOutsideArea;

	public uScript_AddMessage.MessageData msgLeavingMissionArea;

	public Transform NPCDespawnParticleEffect;

	public ExternalBehaviorTree NPCFlyAwayAI;

	public SpawnTechData[] NPCSpawnData = new SpawnTechData[0];

	private GameObject owner_Connection_1;

	private GameObject owner_Connection_5;

	private GameObject owner_Connection_55;

	private GameObject owner_Connection_58;

	private GameObject owner_Connection_148;

	private uScript_IsPlayerInRangeOfTech logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_2 = new uScript_IsPlayerInRangeOfTech();

	private Tank logic_uScript_IsPlayerInRangeOfTech_tech_2;

	private float logic_uScript_IsPlayerInRangeOfTech_range_2;

	private Tank[] logic_uScript_IsPlayerInRangeOfTech_techs_2 = new Tank[0];

	private bool logic_uScript_IsPlayerInRangeOfTech_Out_2 = true;

	private bool logic_uScript_IsPlayerInRangeOfTech_InRange_2 = true;

	private bool logic_uScript_IsPlayerInRangeOfTech_OutOfRange_2 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_7 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_7;

	private bool logic_uScriptCon_CompareBool_True_7 = true;

	private bool logic_uScriptCon_CompareBool_False_7 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_9 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_9;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_9 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_9 = "ScrappingInProgress";

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_10 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_10;

	private bool logic_uScriptAct_SetBool_Out_10 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_10 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_10 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_13 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_13;

	private bool logic_uScriptCon_CompareBool_True_13 = true;

	private bool logic_uScriptCon_CompareBool_False_13 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_16 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_16;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_16 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_16 = "ScrappingInProgress";

	private uScript_IsCraftingBlockProducingItem logic_uScript_IsCraftingBlockProducingItem_uScript_IsCraftingBlockProducingItem_17 = new uScript_IsCraftingBlockProducingItem();

	private TankBlock logic_uScript_IsCraftingBlockProducingItem_craftingBlock_17;

	private bool logic_uScript_IsCraftingBlockProducingItem_True_17 = true;

	private bool logic_uScript_IsCraftingBlockProducingItem_False_17 = true;

	private uScript_LockTechStacks logic_uScript_LockTechStacks_uScript_LockTechStacks_20 = new uScript_LockTechStacks();

	private Tank logic_uScript_LockTechStacks_tech_20;

	private bool logic_uScript_LockTechStacks_Out_20 = true;

	private SubGraph_Crafting_Tutorial_AttachBlockToBase logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_SubGraph_Crafting_Tutorial_AttachBlockToBase_21 = new SubGraph_Crafting_Tutorial_AttachBlockToBase();

	private TankBlock logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_Block_21;

	private Tank logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_CraftingBaseTech_21;

	private GhostBlockSpawnData[] logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_ghostBlockData_21 = new GhostBlockSpawnData[0];

	private BlockTypes logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_BlockType_21 = BlockTypes.GSOScrapper_322;

	private Vector3 logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_BlockPosition_21 = new Vector3(4f, 0f, -3f);

	private uScript_LockTechInteraction logic_uScript_LockTechInteraction_uScript_LockTechInteraction_25 = new uScript_LockTechInteraction();

	private Tank logic_uScript_LockTechInteraction_tech_25;

	private BlockTypes[] logic_uScript_LockTechInteraction_excludedBlocks_25 = new BlockTypes[0];

	private bool logic_uScript_LockTechInteraction_Out_25 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_28 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_28;

	private bool logic_uScriptCon_CompareBool_True_28 = true;

	private bool logic_uScriptCon_CompareBool_False_28 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_29 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_29;

	private bool logic_uScriptAct_SetBool_Out_29 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_29 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_29 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_32 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_32;

	private bool logic_uScriptCon_CompareBool_True_32 = true;

	private bool logic_uScriptCon_CompareBool_False_32 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_34 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_34;

	private bool logic_uScriptAct_SetBool_Out_34 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_34 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_34 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_37 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_37;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_37 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_37 = "msgScrapperAttachedShown";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_38 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_38;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_38 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_38 = "msgBaseFoundShown";

	private uScript_PausePopulation logic_uScript_PausePopulation_uScript_PausePopulation_39 = new uScript_PausePopulation();

	private bool logic_uScript_PausePopulation_Out_39 = true;

	private uScript_PausePopulation logic_uScript_PausePopulation_uScript_PausePopulation_41 = new uScript_PausePopulation();

	private bool logic_uScript_PausePopulation_Out_41 = true;

	private uScript_HideArrow logic_uScript_HideArrow_uScript_HideArrow_42 = new uScript_HideArrow();

	private bool logic_uScript_HideArrow_Out_42 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_43 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_43;

	private bool logic_uScriptAct_SetBool_Out_43 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_43 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_43 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_44 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_44 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_44 = true;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_44 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_46 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_46;

	private bool logic_uScriptAct_SetBool_Out_46 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_46 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_46 = true;

	private uScript_IsPlayerInRangeOfTech logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_47 = new uScript_IsPlayerInRangeOfTech();

	private Tank logic_uScript_IsPlayerInRangeOfTech_tech_47;

	private float logic_uScript_IsPlayerInRangeOfTech_range_47 = 100f;

	private Tank[] logic_uScript_IsPlayerInRangeOfTech_techs_47 = new Tank[0];

	private bool logic_uScript_IsPlayerInRangeOfTech_Out_47 = true;

	private bool logic_uScript_IsPlayerInRangeOfTech_InRange_47 = true;

	private bool logic_uScript_IsPlayerInRangeOfTech_OutOfRange_47 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_49 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_49;

	private bool logic_uScriptCon_CompareBool_True_49 = true;

	private bool logic_uScriptCon_CompareBool_False_49 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_51 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_51;

	private bool logic_uScriptAct_SetBool_Out_51 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_51 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_51 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_53 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_53;

	private bool logic_uScriptCon_CompareBool_True_53 = true;

	private bool logic_uScriptCon_CompareBool_False_53 = true;

	private uScript_SpawnBlocksFromData logic_uScript_SpawnBlocksFromData_uScript_SpawnBlocksFromData_54 = new uScript_SpawnBlocksFromData();

	private SpawnBlockData[] logic_uScript_SpawnBlocksFromData_blockData_54 = new SpawnBlockData[0];

	private GameObject logic_uScript_SpawnBlocksFromData_ownerNode_54;

	private bool logic_uScript_SpawnBlocksFromData_Out_54 = true;

	private uScript_GetAndCheckBlocks logic_uScript_GetAndCheckBlocks_uScript_GetAndCheckBlocks_59 = new uScript_GetAndCheckBlocks();

	private SpawnBlockData[] logic_uScript_GetAndCheckBlocks_blockData_59 = new SpawnBlockData[0];

	private GameObject logic_uScript_GetAndCheckBlocks_ownerNode_59;

	private TankBlock[] logic_uScript_GetAndCheckBlocks_blocks_59 = new TankBlock[0];

	private bool logic_uScript_GetAndCheckBlocks_AllAlive_59 = true;

	private bool logic_uScript_GetAndCheckBlocks_SomeAlive_59 = true;

	private bool logic_uScript_GetAndCheckBlocks_AllDead_59 = true;

	private bool logic_uScript_GetAndCheckBlocks_WaitingToSpawn_59 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_62 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_62;

	private bool logic_uScriptCon_CompareBool_True_62 = true;

	private bool logic_uScriptCon_CompareBool_False_62 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_63 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_63;

	private bool logic_uScriptAct_SetBool_Out_63 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_63 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_63 = true;

	private uScript_IsCraftingBlockInOperation logic_uScript_IsCraftingBlockInOperation_uScript_IsCraftingBlockInOperation_64 = new uScript_IsCraftingBlockInOperation();

	private TankBlock logic_uScript_IsCraftingBlockInOperation_craftingBlock_64;

	private bool logic_uScript_IsCraftingBlockInOperation_True_64 = true;

	private bool logic_uScript_IsCraftingBlockInOperation_False_64 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_66 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_66;

	private bool logic_uScriptAct_SetBool_Out_66 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_66 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_66 = true;

	private uScript_HideArrow logic_uScript_HideArrow_uScript_HideArrow_68 = new uScript_HideArrow();

	private bool logic_uScript_HideArrow_Out_68 = true;

	private uScript_PointArrowAtVisible logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_69 = new uScript_PointArrowAtVisible();

	private object logic_uScript_PointArrowAtVisible_targetObject_69 = "";

	private float logic_uScript_PointArrowAtVisible_timeToShowFor_69 = -1f;

	private Vector3 logic_uScript_PointArrowAtVisible_offset_69 = new Vector3(0f, 0f, 0f);

	private bool logic_uScript_PointArrowAtVisible_Out_69 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_71 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_71;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_71 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_71 = "msgIntroShown";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_74 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_74;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_74 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_74 = "ScrapperSpawned";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_76 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_76;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_76 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_76 = "msgScrapperSpawnedShown";

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_78 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_78;

	private bool logic_uScriptAct_SetBool_Out_78 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_78 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_78 = true;

	private SubGraph_Crafting_Tutorial_ManageBlock logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_80 = new SubGraph_Crafting_Tutorial_ManageBlock();

	private SpawnBlockData[] logic_SubGraph_Crafting_Tutorial_ManageBlock_blockSpawnData_80 = new SpawnBlockData[0];

	private int logic_SubGraph_Crafting_Tutorial_ManageBlock_blockIndex_80;

	private TankBlock logic_SubGraph_Crafting_Tutorial_ManageBlock_block_80;

	private bool logic_SubGraph_Crafting_Tutorial_ManageBlock_allowAnchoring_80;

	private uScript_AddMessage.MessageData logic_SubGraph_Crafting_Tutorial_ManageBlock_msgBlockOutsideArea_80;

	private uScript_AddMessage.MessageSpeaker logic_SubGraph_Crafting_Tutorial_ManageBlock_messageSpeaker_80;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_81 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_81;

	private bool logic_uScriptCon_CompareBool_True_81 = true;

	private bool logic_uScriptCon_CompareBool_False_81 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_84 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_84 = true;

	private uScriptCon_ManualSwitch logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_86 = new uScriptCon_ManualSwitch();

	private int logic_uScriptCon_ManualSwitch_CurrentOutput_86;

	private SubGraph_SaveLoadInt logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_87 = new SubGraph_SaveLoadInt();

	private int logic_SubGraph_SaveLoadInt_restartValue_87 = 1;

	private int logic_SubGraph_SaveLoadInt_integer_87;

	private object logic_SubGraph_SaveLoadInt_intAsVariable_87 = "";

	private string logic_SubGraph_SaveLoadInt_uniqueID_87 = "Stage";

	private SubGraph_LoadObjectiveStates logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_89 = new SubGraph_LoadObjectiveStates();

	private int logic_SubGraph_LoadObjectiveStates_currentObjective_89;

	private SubGraph_CompleteObjectiveStage logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_96 = new SubGraph_CompleteObjectiveStage();

	private int logic_SubGraph_CompleteObjectiveStage_objectiveStage_96;

	private bool logic_SubGraph_CompleteObjectiveStage_isFinalObjective_96;

	private SubGraph_CompleteObjectiveStage logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_98 = new SubGraph_CompleteObjectiveStage();

	private int logic_SubGraph_CompleteObjectiveStage_objectiveStage_98;

	private bool logic_SubGraph_CompleteObjectiveStage_isFinalObjective_98;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_100 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_100;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_100;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_100;

	private bool logic_uScript_AddMessage_Out_100 = true;

	private bool logic_uScript_AddMessage_Shown_100 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_103 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_103;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_103;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_103;

	private bool logic_uScript_AddMessage_Out_103 = true;

	private bool logic_uScript_AddMessage_Shown_103 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_107 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_107;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_107;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_107;

	private bool logic_uScript_AddMessage_Out_107 = true;

	private bool logic_uScript_AddMessage_Shown_107 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_110 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_110;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_110;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_110;

	private bool logic_uScript_AddMessage_Out_110 = true;

	private bool logic_uScript_AddMessage_Shown_110 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_113 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_113;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_113;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_113;

	private bool logic_uScript_AddMessage_Out_113 = true;

	private bool logic_uScript_AddMessage_Shown_113 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_116 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_116;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_116;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_116;

	private bool logic_uScript_AddMessage_Out_116 = true;

	private bool logic_uScript_AddMessage_Shown_116 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_119 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_119;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_119;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_119;

	private bool logic_uScript_AddMessage_Out_119 = true;

	private bool logic_uScript_AddMessage_Shown_119 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_120 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_120;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_120;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_120;

	private bool logic_uScript_AddMessage_Out_120 = true;

	private bool logic_uScript_AddMessage_Shown_120 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_126 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_126;

	private bool logic_uScriptCon_CompareBool_True_126 = true;

	private bool logic_uScriptCon_CompareBool_False_126 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_127 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_127;

	private bool logic_uScriptAct_SetBool_Out_127 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_127 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_127 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_131 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_131;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_131;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_131;

	private bool logic_uScript_AddMessage_Out_131 = true;

	private bool logic_uScript_AddMessage_Shown_131 = true;

	private SubGraph_Crafting_Tutorial_Finish logic_SubGraph_Crafting_Tutorial_Finish_SubGraph_Crafting_Tutorial_Finish_132 = new SubGraph_Crafting_Tutorial_Finish();

	private Tank logic_SubGraph_Crafting_Tutorial_Finish_craftingBaseTech_132;

	private Tank logic_SubGraph_Crafting_Tutorial_Finish_NPCTech_132;

	private ExternalBehaviorTree logic_SubGraph_Crafting_Tutorial_Finish_NPCFlyAwayAI_132;

	private Transform logic_SubGraph_Crafting_Tutorial_Finish_NPCDespawnParticleEffect_132;

	private SubGraph_Crafting_Tutorial_Init logic_SubGraph_Crafting_Tutorial_Init_SubGraph_Crafting_Tutorial_Init_143 = new SubGraph_Crafting_Tutorial_Init();

	private SpawnTechData[] logic_SubGraph_Crafting_Tutorial_Init_baseSpawnData_143 = new SpawnTechData[0];

	private SpawnBlockData[] logic_SubGraph_Crafting_Tutorial_Init_blockSpawnData_143 = new SpawnBlockData[0];

	private SpawnTechData[] logic_SubGraph_Crafting_Tutorial_Init_NPCSpawnData_143 = new SpawnTechData[0];

	private TankPreset logic_SubGraph_Crafting_Tutorial_Init_completedBasePreset_143;

	private bool logic_SubGraph_Crafting_Tutorial_Init_useNPCAsTutorialTech_143;

	private bool logic_SubGraph_Crafting_Tutorial_Init_spawnBase_143 = true;

	private string logic_SubGraph_Crafting_Tutorial_Init_basePosition_143 = "";

	private float logic_SubGraph_Crafting_Tutorial_Init_clearSceneryRadius_143;

	private Tank logic_SubGraph_Crafting_Tutorial_Init_CraftingBaseTech_143;

	private Tank logic_SubGraph_Crafting_Tutorial_Init_NPCTech_143;

	private uScript_SetEncounterTarget logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_149 = new uScript_SetEncounterTarget();

	private GameObject logic_uScript_SetEncounterTarget_owner_149;

	private object logic_uScript_SetEncounterTarget_visibleObject_149 = "";

	private bool logic_uScript_SetEncounterTarget_Out_149 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_150 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_150 = "";

	private bool logic_uScript_EnableGlow_enable_150 = true;

	private bool logic_uScript_EnableGlow_Out_150 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_153 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_153 = "";

	private bool logic_uScript_EnableGlow_enable_153;

	private bool logic_uScript_EnableGlow_Out_153 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_154 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_154;

	private bool logic_uScriptCon_CompareBool_True_154 = true;

	private bool logic_uScriptCon_CompareBool_False_154 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_156 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_156 = "";

	private bool logic_uScript_EnableGlow_enable_156;

	private bool logic_uScript_EnableGlow_Out_156 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_158 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_158 = true;

	private uScript_SendAnaliticsEvent logic_uScript_SendAnaliticsEvent_uScript_SendAnaliticsEvent_159 = new uScript_SendAnaliticsEvent();

	private string logic_uScript_SendAnaliticsEvent_analiticsEvent_159 = "tutorial_complete";

	private string logic_uScript_SendAnaliticsEvent_parameterName_159 = "crafting_tutorial";

	private object logic_uScript_SendAnaliticsEvent_parameter_159 = "4";

	private bool logic_uScript_SendAnaliticsEvent_Out_159 = true;

	private uScript_SendAnaliticsEvent logic_uScript_SendAnaliticsEvent_uScript_SendAnaliticsEvent_160 = new uScript_SendAnaliticsEvent();

	private string logic_uScript_SendAnaliticsEvent_analiticsEvent_160 = "tutorial_start";

	private string logic_uScript_SendAnaliticsEvent_parameterName_160 = "crafting_tutorial";

	private object logic_uScript_SendAnaliticsEvent_parameter_160 = "4";

	private bool logic_uScript_SendAnaliticsEvent_Out_160 = true;

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
		if (null == owner_Connection_55 || !m_RegisteredForEvents)
		{
			owner_Connection_55 = parentGameObject;
		}
		if (null == owner_Connection_58 || !m_RegisteredForEvents)
		{
			owner_Connection_58 = parentGameObject;
		}
		if (null == owner_Connection_148 || !m_RegisteredForEvents)
		{
			owner_Connection_148 = parentGameObject;
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
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_7.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_9.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_10.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_13.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_16.SetParent(g);
		logic_uScript_IsCraftingBlockProducingItem_uScript_IsCraftingBlockProducingItem_17.SetParent(g);
		logic_uScript_LockTechStacks_uScript_LockTechStacks_20.SetParent(g);
		logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_SubGraph_Crafting_Tutorial_AttachBlockToBase_21.SetParent(g);
		logic_uScript_LockTechInteraction_uScript_LockTechInteraction_25.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_28.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_29.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_32.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_34.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_37.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_38.SetParent(g);
		logic_uScript_PausePopulation_uScript_PausePopulation_39.SetParent(g);
		logic_uScript_PausePopulation_uScript_PausePopulation_41.SetParent(g);
		logic_uScript_HideArrow_uScript_HideArrow_42.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_43.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_44.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_46.SetParent(g);
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_47.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_49.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_51.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_53.SetParent(g);
		logic_uScript_SpawnBlocksFromData_uScript_SpawnBlocksFromData_54.SetParent(g);
		logic_uScript_GetAndCheckBlocks_uScript_GetAndCheckBlocks_59.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_62.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_63.SetParent(g);
		logic_uScript_IsCraftingBlockInOperation_uScript_IsCraftingBlockInOperation_64.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_66.SetParent(g);
		logic_uScript_HideArrow_uScript_HideArrow_68.SetParent(g);
		logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_69.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_71.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_74.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_76.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_78.SetParent(g);
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_80.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_81.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_84.SetParent(g);
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_86.SetParent(g);
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_87.SetParent(g);
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_89.SetParent(g);
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_96.SetParent(g);
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_98.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_100.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_103.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_107.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_110.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_113.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_116.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_119.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_120.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_126.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_127.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_131.SetParent(g);
		logic_SubGraph_Crafting_Tutorial_Finish_SubGraph_Crafting_Tutorial_Finish_132.SetParent(g);
		logic_SubGraph_Crafting_Tutorial_Init_SubGraph_Crafting_Tutorial_Init_143.SetParent(g);
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_149.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_150.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_153.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_154.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_156.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_158.SetParent(g);
		logic_uScript_SendAnaliticsEvent_uScript_SendAnaliticsEvent_159.SetParent(g);
		logic_uScript_SendAnaliticsEvent_uScript_SendAnaliticsEvent_160.SetParent(g);
		owner_Connection_1 = parentGameObject;
		owner_Connection_5 = parentGameObject;
		owner_Connection_55 = parentGameObject;
		owner_Connection_58 = parentGameObject;
		owner_Connection_148 = parentGameObject;
	}

	public void Awake()
	{
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_9.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_16.Awake();
		logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_SubGraph_Crafting_Tutorial_AttachBlockToBase_21.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_37.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_38.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_71.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_74.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_76.Awake();
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_80.Awake();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_87.Awake();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_89.Awake();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_96.Awake();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_98.Awake();
		logic_SubGraph_Crafting_Tutorial_Finish_SubGraph_Crafting_Tutorial_Finish_132.Awake();
		logic_SubGraph_Crafting_Tutorial_Init_SubGraph_Crafting_Tutorial_Init_143.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_9.Save_Out += SubGraph_SaveLoadBool_Save_Out_9;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_9.Load_Out += SubGraph_SaveLoadBool_Load_Out_9;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_9.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_9;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_16.Save_Out += SubGraph_SaveLoadBool_Save_Out_16;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_16.Load_Out += SubGraph_SaveLoadBool_Load_Out_16;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_16.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_16;
		logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_SubGraph_Crafting_Tutorial_AttachBlockToBase_21.Block_Attached += SubGraph_Crafting_Tutorial_AttachBlockToBase_Block_Attached_21;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_37.Save_Out += SubGraph_SaveLoadBool_Save_Out_37;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_37.Load_Out += SubGraph_SaveLoadBool_Load_Out_37;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_37.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_37;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_38.Save_Out += SubGraph_SaveLoadBool_Save_Out_38;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_38.Load_Out += SubGraph_SaveLoadBool_Load_Out_38;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_38.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_38;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_71.Save_Out += SubGraph_SaveLoadBool_Save_Out_71;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_71.Load_Out += SubGraph_SaveLoadBool_Load_Out_71;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_71.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_71;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_74.Save_Out += SubGraph_SaveLoadBool_Save_Out_74;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_74.Load_Out += SubGraph_SaveLoadBool_Load_Out_74;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_74.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_74;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_76.Save_Out += SubGraph_SaveLoadBool_Save_Out_76;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_76.Load_Out += SubGraph_SaveLoadBool_Load_Out_76;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_76.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_76;
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_80.Out += SubGraph_Crafting_Tutorial_ManageBlock_Out_80;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_86.Output1 += uScriptCon_ManualSwitch_Output1_86;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_86.Output2 += uScriptCon_ManualSwitch_Output2_86;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_86.Output3 += uScriptCon_ManualSwitch_Output3_86;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_86.Output4 += uScriptCon_ManualSwitch_Output4_86;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_86.Output5 += uScriptCon_ManualSwitch_Output5_86;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_86.Output6 += uScriptCon_ManualSwitch_Output6_86;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_86.Output7 += uScriptCon_ManualSwitch_Output7_86;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_86.Output8 += uScriptCon_ManualSwitch_Output8_86;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_87.Save_Out += SubGraph_SaveLoadInt_Save_Out_87;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_87.Load_Out += SubGraph_SaveLoadInt_Load_Out_87;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_87.Restart_Out += SubGraph_SaveLoadInt_Restart_Out_87;
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_89.Out += SubGraph_LoadObjectiveStates_Out_89;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_96.Out += SubGraph_CompleteObjectiveStage_Out_96;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_98.Out += SubGraph_CompleteObjectiveStage_Out_98;
		logic_SubGraph_Crafting_Tutorial_Finish_SubGraph_Crafting_Tutorial_Finish_132.Out += SubGraph_Crafting_Tutorial_Finish_Out_132;
		logic_SubGraph_Crafting_Tutorial_Init_SubGraph_Crafting_Tutorial_Init_143.Out += SubGraph_Crafting_Tutorial_Init_Out_143;
	}

	public void Start()
	{
		SyncUnityHooks();
		m_RegisteredForEvents = true;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_9.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_16.Start();
		logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_SubGraph_Crafting_Tutorial_AttachBlockToBase_21.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_37.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_38.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_71.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_74.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_76.Start();
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_80.Start();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_87.Start();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_89.Start();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_96.Start();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_98.Start();
		logic_SubGraph_Crafting_Tutorial_Finish_SubGraph_Crafting_Tutorial_Finish_132.Start();
		logic_SubGraph_Crafting_Tutorial_Init_SubGraph_Crafting_Tutorial_Init_143.Start();
	}

	public void OnEnable()
	{
		RegisterForUnityHooks();
		m_RegisteredForEvents = true;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_9.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_16.OnEnable();
		logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_SubGraph_Crafting_Tutorial_AttachBlockToBase_21.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_37.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_38.OnEnable();
		logic_uScript_IsCraftingBlockInOperation_uScript_IsCraftingBlockInOperation_64.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_71.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_74.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_76.OnEnable();
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_80.OnEnable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_87.OnEnable();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_89.OnEnable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_96.OnEnable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_98.OnEnable();
		logic_SubGraph_Crafting_Tutorial_Finish_SubGraph_Crafting_Tutorial_Finish_132.OnEnable();
		logic_SubGraph_Crafting_Tutorial_Init_SubGraph_Crafting_Tutorial_Init_143.OnEnable();
	}

	public void OnDisable()
	{
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_2.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_9.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_16.OnDisable();
		logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_SubGraph_Crafting_Tutorial_AttachBlockToBase_21.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_37.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_38.OnDisable();
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_47.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_71.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_74.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_76.OnDisable();
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_80.OnDisable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_87.OnDisable();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_89.OnDisable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_96.OnDisable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_98.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_100.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_103.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_107.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_110.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_113.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_116.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_119.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_120.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_131.OnDisable();
		logic_SubGraph_Crafting_Tutorial_Finish_SubGraph_Crafting_Tutorial_Finish_132.OnDisable();
		logic_SubGraph_Crafting_Tutorial_Init_SubGraph_Crafting_Tutorial_Init_143.OnDisable();
		UnregisterEventListeners();
		m_RegisteredForEvents = false;
	}

	public void Update()
	{
		SyncEventListeners();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_9.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_16.Update();
		logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_SubGraph_Crafting_Tutorial_AttachBlockToBase_21.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_37.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_38.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_71.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_74.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_76.Update();
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_80.Update();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_87.Update();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_89.Update();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_96.Update();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_98.Update();
		logic_SubGraph_Crafting_Tutorial_Finish_SubGraph_Crafting_Tutorial_Finish_132.Update();
		logic_SubGraph_Crafting_Tutorial_Init_SubGraph_Crafting_Tutorial_Init_143.Update();
	}

	public void OnDestroy()
	{
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_9.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_16.OnDestroy();
		logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_SubGraph_Crafting_Tutorial_AttachBlockToBase_21.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_37.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_38.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_71.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_74.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_76.OnDestroy();
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_80.OnDestroy();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_87.OnDestroy();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_89.OnDestroy();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_96.OnDestroy();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_98.OnDestroy();
		logic_SubGraph_Crafting_Tutorial_Finish_SubGraph_Crafting_Tutorial_Finish_132.OnDestroy();
		logic_SubGraph_Crafting_Tutorial_Init_SubGraph_Crafting_Tutorial_Init_143.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_9.Save_Out -= SubGraph_SaveLoadBool_Save_Out_9;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_9.Load_Out -= SubGraph_SaveLoadBool_Load_Out_9;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_9.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_9;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_16.Save_Out -= SubGraph_SaveLoadBool_Save_Out_16;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_16.Load_Out -= SubGraph_SaveLoadBool_Load_Out_16;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_16.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_16;
		logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_SubGraph_Crafting_Tutorial_AttachBlockToBase_21.Block_Attached -= SubGraph_Crafting_Tutorial_AttachBlockToBase_Block_Attached_21;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_37.Save_Out -= SubGraph_SaveLoadBool_Save_Out_37;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_37.Load_Out -= SubGraph_SaveLoadBool_Load_Out_37;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_37.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_37;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_38.Save_Out -= SubGraph_SaveLoadBool_Save_Out_38;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_38.Load_Out -= SubGraph_SaveLoadBool_Load_Out_38;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_38.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_38;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_71.Save_Out -= SubGraph_SaveLoadBool_Save_Out_71;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_71.Load_Out -= SubGraph_SaveLoadBool_Load_Out_71;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_71.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_71;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_74.Save_Out -= SubGraph_SaveLoadBool_Save_Out_74;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_74.Load_Out -= SubGraph_SaveLoadBool_Load_Out_74;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_74.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_74;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_76.Save_Out -= SubGraph_SaveLoadBool_Save_Out_76;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_76.Load_Out -= SubGraph_SaveLoadBool_Load_Out_76;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_76.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_76;
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_80.Out -= SubGraph_Crafting_Tutorial_ManageBlock_Out_80;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_86.Output1 -= uScriptCon_ManualSwitch_Output1_86;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_86.Output2 -= uScriptCon_ManualSwitch_Output2_86;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_86.Output3 -= uScriptCon_ManualSwitch_Output3_86;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_86.Output4 -= uScriptCon_ManualSwitch_Output4_86;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_86.Output5 -= uScriptCon_ManualSwitch_Output5_86;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_86.Output6 -= uScriptCon_ManualSwitch_Output6_86;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_86.Output7 -= uScriptCon_ManualSwitch_Output7_86;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_86.Output8 -= uScriptCon_ManualSwitch_Output8_86;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_87.Save_Out -= SubGraph_SaveLoadInt_Save_Out_87;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_87.Load_Out -= SubGraph_SaveLoadInt_Load_Out_87;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_87.Restart_Out -= SubGraph_SaveLoadInt_Restart_Out_87;
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_89.Out -= SubGraph_LoadObjectiveStates_Out_89;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_96.Out -= SubGraph_CompleteObjectiveStage_Out_96;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_98.Out -= SubGraph_CompleteObjectiveStage_Out_98;
		logic_SubGraph_Crafting_Tutorial_Finish_SubGraph_Crafting_Tutorial_Finish_132.Out -= SubGraph_Crafting_Tutorial_Finish_Out_132;
		logic_SubGraph_Crafting_Tutorial_Init_SubGraph_Crafting_Tutorial_Init_143.Out -= SubGraph_Crafting_Tutorial_Init_Out_143;
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

	private void SubGraph_SaveLoadBool_Save_Out_9(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_9 = e.boolean;
		local_BlockInScrapper_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_9;
		Relay_Save_Out_9();
	}

	private void SubGraph_SaveLoadBool_Load_Out_9(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_9 = e.boolean;
		local_BlockInScrapper_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_9;
		Relay_Load_Out_9();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_9(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_9 = e.boolean;
		local_BlockInScrapper_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_9;
		Relay_Restart_Out_9();
	}

	private void SubGraph_SaveLoadBool_Save_Out_16(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_16 = e.boolean;
		local_ScrappingInProgress_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_16;
		Relay_Save_Out_16();
	}

	private void SubGraph_SaveLoadBool_Load_Out_16(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_16 = e.boolean;
		local_ScrappingInProgress_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_16;
		Relay_Load_Out_16();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_16(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_16 = e.boolean;
		local_ScrappingInProgress_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_16;
		Relay_Restart_Out_16();
	}

	private void SubGraph_Crafting_Tutorial_AttachBlockToBase_Block_Attached_21(object o, SubGraph_Crafting_Tutorial_AttachBlockToBase.LogicEventArgs e)
	{
		Relay_Block_Attached_21();
	}

	private void SubGraph_SaveLoadBool_Save_Out_37(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_37 = e.boolean;
		local_msgScrapperAttachedShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_37;
		Relay_Save_Out_37();
	}

	private void SubGraph_SaveLoadBool_Load_Out_37(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_37 = e.boolean;
		local_msgScrapperAttachedShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_37;
		Relay_Load_Out_37();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_37(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_37 = e.boolean;
		local_msgScrapperAttachedShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_37;
		Relay_Restart_Out_37();
	}

	private void SubGraph_SaveLoadBool_Save_Out_38(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_38 = e.boolean;
		local_msgBaseFoundShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_38;
		Relay_Save_Out_38();
	}

	private void SubGraph_SaveLoadBool_Load_Out_38(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_38 = e.boolean;
		local_msgBaseFoundShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_38;
		Relay_Load_Out_38();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_38(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_38 = e.boolean;
		local_msgBaseFoundShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_38;
		Relay_Restart_Out_38();
	}

	private void SubGraph_SaveLoadBool_Save_Out_71(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_71 = e.boolean;
		local_msgIntroShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_71;
		Relay_Save_Out_71();
	}

	private void SubGraph_SaveLoadBool_Load_Out_71(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_71 = e.boolean;
		local_msgIntroShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_71;
		Relay_Load_Out_71();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_71(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_71 = e.boolean;
		local_msgIntroShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_71;
		Relay_Restart_Out_71();
	}

	private void SubGraph_SaveLoadBool_Save_Out_74(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_74 = e.boolean;
		local_ScrapperSpawned_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_74;
		Relay_Save_Out_74();
	}

	private void SubGraph_SaveLoadBool_Load_Out_74(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_74 = e.boolean;
		local_ScrapperSpawned_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_74;
		Relay_Load_Out_74();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_74(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_74 = e.boolean;
		local_ScrapperSpawned_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_74;
		Relay_Restart_Out_74();
	}

	private void SubGraph_SaveLoadBool_Save_Out_76(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_76 = e.boolean;
		local_msgScrapperSpawnedShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_76;
		Relay_Save_Out_76();
	}

	private void SubGraph_SaveLoadBool_Load_Out_76(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_76 = e.boolean;
		local_msgScrapperSpawnedShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_76;
		Relay_Load_Out_76();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_76(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_76 = e.boolean;
		local_msgScrapperSpawnedShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_76;
		Relay_Restart_Out_76();
	}

	private void SubGraph_Crafting_Tutorial_ManageBlock_Out_80(object o, SubGraph_Crafting_Tutorial_ManageBlock.LogicEventArgs e)
	{
		logic_SubGraph_Crafting_Tutorial_ManageBlock_block_80 = e.block;
		blockSpawnDataScrapper = logic_SubGraph_Crafting_Tutorial_ManageBlock_blockSpawnData_80;
		local_ScrapperBlock_TankBlock = logic_SubGraph_Crafting_Tutorial_ManageBlock_block_80;
		Relay_Out_80();
	}

	private void uScriptCon_ManualSwitch_Output1_86(object o, EventArgs e)
	{
		Relay_Output1_86();
	}

	private void uScriptCon_ManualSwitch_Output2_86(object o, EventArgs e)
	{
		Relay_Output2_86();
	}

	private void uScriptCon_ManualSwitch_Output3_86(object o, EventArgs e)
	{
		Relay_Output3_86();
	}

	private void uScriptCon_ManualSwitch_Output4_86(object o, EventArgs e)
	{
		Relay_Output4_86();
	}

	private void uScriptCon_ManualSwitch_Output5_86(object o, EventArgs e)
	{
		Relay_Output5_86();
	}

	private void uScriptCon_ManualSwitch_Output6_86(object o, EventArgs e)
	{
		Relay_Output6_86();
	}

	private void uScriptCon_ManualSwitch_Output7_86(object o, EventArgs e)
	{
		Relay_Output7_86();
	}

	private void uScriptCon_ManualSwitch_Output8_86(object o, EventArgs e)
	{
		Relay_Output8_86();
	}

	private void SubGraph_SaveLoadInt_Save_Out_87(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_87 = e.integer;
		local_Stage_System_Int32 = logic_SubGraph_SaveLoadInt_integer_87;
		Relay_Save_Out_87();
	}

	private void SubGraph_SaveLoadInt_Load_Out_87(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_87 = e.integer;
		local_Stage_System_Int32 = logic_SubGraph_SaveLoadInt_integer_87;
		Relay_Load_Out_87();
	}

	private void SubGraph_SaveLoadInt_Restart_Out_87(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_87 = e.integer;
		local_Stage_System_Int32 = logic_SubGraph_SaveLoadInt_integer_87;
		Relay_Restart_Out_87();
	}

	private void SubGraph_LoadObjectiveStates_Out_89(object o, SubGraph_LoadObjectiveStates.LogicEventArgs e)
	{
		Relay_Out_89();
	}

	private void SubGraph_CompleteObjectiveStage_Out_96(object o, SubGraph_CompleteObjectiveStage.LogicEventArgs e)
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_96 = e.objectiveStage;
		local_Stage_System_Int32 = logic_SubGraph_CompleteObjectiveStage_objectiveStage_96;
		Relay_Out_96();
	}

	private void SubGraph_CompleteObjectiveStage_Out_98(object o, SubGraph_CompleteObjectiveStage.LogicEventArgs e)
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_98 = e.objectiveStage;
		local_Stage_System_Int32 = logic_SubGraph_CompleteObjectiveStage_objectiveStage_98;
		Relay_Out_98();
	}

	private void SubGraph_Crafting_Tutorial_Finish_Out_132(object o, SubGraph_Crafting_Tutorial_Finish.LogicEventArgs e)
	{
		Relay_Out_132();
	}

	private void SubGraph_Crafting_Tutorial_Init_Out_143(object o, SubGraph_Crafting_Tutorial_Init.LogicEventArgs e)
	{
		logic_SubGraph_Crafting_Tutorial_Init_CraftingBaseTech_143 = e.CraftingBaseTech;
		logic_SubGraph_Crafting_Tutorial_Init_NPCTech_143 = e.NPCTech;
		local_CraftingBaseTech_Tank = logic_SubGraph_Crafting_Tutorial_Init_CraftingBaseTech_143;
		local_NPCTech_Tank = logic_SubGraph_Crafting_Tutorial_Init_NPCTech_143;
		Relay_Out_143();
	}

	private void Relay_OnUpdate_0()
	{
		Relay_In_143();
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
			Relay_Pause_39();
		}
		if (outOfRange)
		{
			Relay_UnPause_41();
		}
	}

	private void Relay_SaveEvent_4()
	{
		Relay_Save_87();
	}

	private void Relay_LoadEvent_4()
	{
		Relay_Load_87();
	}

	private void Relay_RestartEvent_4()
	{
		Relay_Restart_87();
	}

	private void Relay_In_7()
	{
		logic_uScriptCon_CompareBool_Bool_7 = local_BlockInScrapper_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_7.In(logic_uScriptCon_CompareBool_Bool_7);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_7.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_7.False;
		if (num)
		{
			Relay_In_13();
		}
		if (flag)
		{
			Relay_In_28();
		}
	}

	private void Relay_Save_Out_9()
	{
		Relay_Save_16();
	}

	private void Relay_Load_Out_9()
	{
		Relay_Load_16();
	}

	private void Relay_Restart_Out_9()
	{
		Relay_Set_False_16();
	}

	private void Relay_Save_9()
	{
		logic_SubGraph_SaveLoadBool_boolean_9 = local_BlockInScrapper_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_9 = local_BlockInScrapper_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_9.Save(ref logic_SubGraph_SaveLoadBool_boolean_9, logic_SubGraph_SaveLoadBool_boolAsVariable_9, logic_SubGraph_SaveLoadBool_uniqueID_9);
	}

	private void Relay_Load_9()
	{
		logic_SubGraph_SaveLoadBool_boolean_9 = local_BlockInScrapper_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_9 = local_BlockInScrapper_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_9.Load(ref logic_SubGraph_SaveLoadBool_boolean_9, logic_SubGraph_SaveLoadBool_boolAsVariable_9, logic_SubGraph_SaveLoadBool_uniqueID_9);
	}

	private void Relay_Set_True_9()
	{
		logic_SubGraph_SaveLoadBool_boolean_9 = local_BlockInScrapper_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_9 = local_BlockInScrapper_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_9.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_9, logic_SubGraph_SaveLoadBool_boolAsVariable_9, logic_SubGraph_SaveLoadBool_uniqueID_9);
	}

	private void Relay_Set_False_9()
	{
		logic_SubGraph_SaveLoadBool_boolean_9 = local_BlockInScrapper_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_9 = local_BlockInScrapper_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_9.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_9, logic_SubGraph_SaveLoadBool_boolAsVariable_9, logic_SubGraph_SaveLoadBool_uniqueID_9);
	}

	private void Relay_True_10()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_10.True(out logic_uScriptAct_SetBool_Target_10);
		local_ScrappingInProgress_System_Boolean = logic_uScriptAct_SetBool_Target_10;
	}

	private void Relay_False_10()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_10.False(out logic_uScriptAct_SetBool_Target_10);
		local_ScrappingInProgress_System_Boolean = logic_uScriptAct_SetBool_Target_10;
	}

	private void Relay_In_13()
	{
		logic_uScriptCon_CompareBool_Bool_13 = local_ScrappingInProgress_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_13.In(logic_uScriptCon_CompareBool_Bool_13);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_13.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_13.False;
		if (num)
		{
			Relay_In_120();
		}
		if (flag)
		{
			Relay_In_119();
		}
	}

	private void Relay_Save_Out_16()
	{
		Relay_Save_37();
	}

	private void Relay_Load_Out_16()
	{
		Relay_Load_37();
	}

	private void Relay_Restart_Out_16()
	{
		Relay_Set_False_37();
	}

	private void Relay_Save_16()
	{
		logic_SubGraph_SaveLoadBool_boolean_16 = local_ScrappingInProgress_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_16 = local_ScrappingInProgress_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_16.Save(ref logic_SubGraph_SaveLoadBool_boolean_16, logic_SubGraph_SaveLoadBool_boolAsVariable_16, logic_SubGraph_SaveLoadBool_uniqueID_16);
	}

	private void Relay_Load_16()
	{
		logic_SubGraph_SaveLoadBool_boolean_16 = local_ScrappingInProgress_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_16 = local_ScrappingInProgress_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_16.Load(ref logic_SubGraph_SaveLoadBool_boolean_16, logic_SubGraph_SaveLoadBool_boolAsVariable_16, logic_SubGraph_SaveLoadBool_uniqueID_16);
	}

	private void Relay_Set_True_16()
	{
		logic_SubGraph_SaveLoadBool_boolean_16 = local_ScrappingInProgress_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_16 = local_ScrappingInProgress_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_16.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_16, logic_SubGraph_SaveLoadBool_boolAsVariable_16, logic_SubGraph_SaveLoadBool_uniqueID_16);
	}

	private void Relay_Set_False_16()
	{
		logic_SubGraph_SaveLoadBool_boolean_16 = local_ScrappingInProgress_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_16 = local_ScrappingInProgress_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_16.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_16, logic_SubGraph_SaveLoadBool_boolAsVariable_16, logic_SubGraph_SaveLoadBool_uniqueID_16);
	}

	private void Relay_In_17()
	{
		logic_uScript_IsCraftingBlockProducingItem_craftingBlock_17 = local_ScrapperBlock_TankBlock;
		logic_uScript_IsCraftingBlockProducingItem_uScript_IsCraftingBlockProducingItem_17.In(logic_uScript_IsCraftingBlockProducingItem_craftingBlock_17);
		if (logic_uScript_IsCraftingBlockProducingItem_uScript_IsCraftingBlockProducingItem_17.True)
		{
			Relay_True_10();
		}
	}

	private void Relay_In_20()
	{
		logic_uScript_LockTechStacks_tech_20 = local_CraftingBaseTech_Tank;
		logic_uScript_LockTechStacks_uScript_LockTechStacks_20.In(logic_uScript_LockTechStacks_tech_20);
		if (logic_uScript_LockTechStacks_uScript_LockTechStacks_20.Out)
		{
			Relay_In_149();
		}
	}

	private void Relay_Block_Attached_21()
	{
		Relay_In_98();
	}

	private void Relay_In_21()
	{
		logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_Block_21 = local_ScrapperBlock_TankBlock;
		logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_CraftingBaseTech_21 = local_CraftingBaseTech_Tank;
		int num = 0;
		Array array = ghostBlockScrapper;
		if (logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_ghostBlockData_21.Length != num + array.Length)
		{
			Array.Resize(ref logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_ghostBlockData_21, num + array.Length);
		}
		Array.Copy(array, 0, logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_ghostBlockData_21, num, array.Length);
		num += array.Length;
		logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_SubGraph_Crafting_Tutorial_AttachBlockToBase_21.In(logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_Block_21, logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_CraftingBaseTech_21, logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_ghostBlockData_21, logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_BlockType_21, logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_BlockPosition_21);
	}

	private void Relay_In_25()
	{
		logic_uScript_LockTechInteraction_tech_25 = local_CraftingBaseTech_Tank;
		logic_uScript_LockTechInteraction_uScript_LockTechInteraction_25.In(logic_uScript_LockTechInteraction_tech_25, logic_uScript_LockTechInteraction_excludedBlocks_25);
		if (logic_uScript_LockTechInteraction_uScript_LockTechInteraction_25.Out)
		{
			Relay_In_20();
		}
	}

	private void Relay_In_28()
	{
		logic_uScriptCon_CompareBool_Bool_28 = local_msgScrapperAttachedShown_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_28.In(logic_uScriptCon_CompareBool_Bool_28);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_28.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_28.False;
		if (num)
		{
			Relay_In_116();
		}
		if (flag)
		{
			Relay_In_113();
		}
	}

	private void Relay_True_29()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_29.True(out logic_uScriptAct_SetBool_Target_29);
		local_msgScrapperAttachedShown_System_Boolean = logic_uScriptAct_SetBool_Target_29;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_29.Out)
		{
			Relay_In_69();
		}
	}

	private void Relay_False_29()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_29.False(out logic_uScriptAct_SetBool_Target_29);
		local_msgScrapperAttachedShown_System_Boolean = logic_uScriptAct_SetBool_Target_29;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_29.Out)
		{
			Relay_In_69();
		}
	}

	private void Relay_In_32()
	{
		logic_uScriptCon_CompareBool_Bool_32 = local_msgBaseFoundShown_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_32.In(logic_uScriptCon_CompareBool_Bool_32);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_32.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_32.False;
		if (num)
		{
			Relay_In_53();
		}
		if (flag)
		{
			Relay_In_103();
		}
	}

	private void Relay_True_34()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_34.True(out logic_uScriptAct_SetBool_Target_34);
		local_msgBaseFoundShown_System_Boolean = logic_uScriptAct_SetBool_Target_34;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_34.Out)
		{
			Relay_In_53();
		}
	}

	private void Relay_False_34()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_34.False(out logic_uScriptAct_SetBool_Target_34);
		local_msgBaseFoundShown_System_Boolean = logic_uScriptAct_SetBool_Target_34;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_34.Out)
		{
			Relay_In_53();
		}
	}

	private void Relay_Save_Out_37()
	{
		Relay_Save_38();
	}

	private void Relay_Load_Out_37()
	{
		Relay_Load_38();
	}

	private void Relay_Restart_Out_37()
	{
		Relay_Set_False_38();
	}

	private void Relay_Save_37()
	{
		logic_SubGraph_SaveLoadBool_boolean_37 = local_msgScrapperAttachedShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_37 = local_msgScrapperAttachedShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_37.Save(ref logic_SubGraph_SaveLoadBool_boolean_37, logic_SubGraph_SaveLoadBool_boolAsVariable_37, logic_SubGraph_SaveLoadBool_uniqueID_37);
	}

	private void Relay_Load_37()
	{
		logic_SubGraph_SaveLoadBool_boolean_37 = local_msgScrapperAttachedShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_37 = local_msgScrapperAttachedShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_37.Load(ref logic_SubGraph_SaveLoadBool_boolean_37, logic_SubGraph_SaveLoadBool_boolAsVariable_37, logic_SubGraph_SaveLoadBool_uniqueID_37);
	}

	private void Relay_Set_True_37()
	{
		logic_SubGraph_SaveLoadBool_boolean_37 = local_msgScrapperAttachedShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_37 = local_msgScrapperAttachedShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_37.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_37, logic_SubGraph_SaveLoadBool_boolAsVariable_37, logic_SubGraph_SaveLoadBool_uniqueID_37);
	}

	private void Relay_Set_False_37()
	{
		logic_SubGraph_SaveLoadBool_boolean_37 = local_msgScrapperAttachedShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_37 = local_msgScrapperAttachedShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_37.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_37, logic_SubGraph_SaveLoadBool_boolAsVariable_37, logic_SubGraph_SaveLoadBool_uniqueID_37);
	}

	private void Relay_Save_Out_38()
	{
		Relay_Save_71();
	}

	private void Relay_Load_Out_38()
	{
		Relay_Load_71();
	}

	private void Relay_Restart_Out_38()
	{
		Relay_Set_False_71();
	}

	private void Relay_Save_38()
	{
		logic_SubGraph_SaveLoadBool_boolean_38 = local_msgBaseFoundShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_38 = local_msgBaseFoundShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_38.Save(ref logic_SubGraph_SaveLoadBool_boolean_38, logic_SubGraph_SaveLoadBool_boolAsVariable_38, logic_SubGraph_SaveLoadBool_uniqueID_38);
	}

	private void Relay_Load_38()
	{
		logic_SubGraph_SaveLoadBool_boolean_38 = local_msgBaseFoundShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_38 = local_msgBaseFoundShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_38.Load(ref logic_SubGraph_SaveLoadBool_boolean_38, logic_SubGraph_SaveLoadBool_boolAsVariable_38, logic_SubGraph_SaveLoadBool_uniqueID_38);
	}

	private void Relay_Set_True_38()
	{
		logic_SubGraph_SaveLoadBool_boolean_38 = local_msgBaseFoundShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_38 = local_msgBaseFoundShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_38.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_38, logic_SubGraph_SaveLoadBool_boolAsVariable_38, logic_SubGraph_SaveLoadBool_uniqueID_38);
	}

	private void Relay_Set_False_38()
	{
		logic_SubGraph_SaveLoadBool_boolean_38 = local_msgBaseFoundShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_38 = local_msgBaseFoundShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_38.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_38, logic_SubGraph_SaveLoadBool_boolAsVariable_38, logic_SubGraph_SaveLoadBool_uniqueID_38);
	}

	private void Relay_Pause_39()
	{
		logic_uScript_PausePopulation_uScript_PausePopulation_39.Pause();
		if (logic_uScript_PausePopulation_uScript_PausePopulation_39.Out)
		{
			Relay_True_43();
		}
	}

	private void Relay_UnPause_39()
	{
		logic_uScript_PausePopulation_uScript_PausePopulation_39.UnPause();
		if (logic_uScript_PausePopulation_uScript_PausePopulation_39.Out)
		{
			Relay_True_43();
		}
	}

	private void Relay_Pause_41()
	{
		logic_uScript_PausePopulation_uScript_PausePopulation_41.Pause();
		if (logic_uScript_PausePopulation_uScript_PausePopulation_41.Out)
		{
			Relay_In_44();
		}
	}

	private void Relay_UnPause_41()
	{
		logic_uScript_PausePopulation_uScript_PausePopulation_41.UnPause();
		if (logic_uScript_PausePopulation_uScript_PausePopulation_41.Out)
		{
			Relay_In_44();
		}
	}

	private void Relay_In_42()
	{
		logic_uScript_HideArrow_uScript_HideArrow_42.In();
		if (logic_uScript_HideArrow_uScript_HideArrow_42.Out)
		{
			Relay_In_154();
		}
	}

	private void Relay_True_43()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_43.True(out logic_uScriptAct_SetBool_Target_43);
		local_NearBase_System_Boolean = logic_uScriptAct_SetBool_Target_43;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_43.Out)
		{
			Relay_In_86();
		}
	}

	private void Relay_False_43()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_43.False(out logic_uScriptAct_SetBool_Target_43);
		local_NearBase_System_Boolean = logic_uScriptAct_SetBool_Target_43;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_43.Out)
		{
			Relay_In_86();
		}
	}

	private void Relay_In_44()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_44 = messageTag;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_44.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_44, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_44);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_44.Out)
		{
			Relay_In_42();
		}
	}

	private void Relay_True_46()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_46.True(out logic_uScriptAct_SetBool_Target_46);
		local_msgIntroShown_System_Boolean = logic_uScriptAct_SetBool_Target_46;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_46.Out)
		{
			Relay_In_100();
		}
	}

	private void Relay_False_46()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_46.False(out logic_uScriptAct_SetBool_Target_46);
		local_msgIntroShown_System_Boolean = logic_uScriptAct_SetBool_Target_46;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_46.Out)
		{
			Relay_In_100();
		}
	}

	private void Relay_In_47()
	{
		logic_uScript_IsPlayerInRangeOfTech_tech_47 = local_CraftingBaseTech_Tank;
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_47.In(logic_uScript_IsPlayerInRangeOfTech_tech_47, logic_uScript_IsPlayerInRangeOfTech_range_47, logic_uScript_IsPlayerInRangeOfTech_techs_47);
		if (logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_47.InRange)
		{
			Relay_In_49();
		}
	}

	private void Relay_In_49()
	{
		logic_uScriptCon_CompareBool_Bool_49 = local_msgIntroShown_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_49.In(logic_uScriptCon_CompareBool_Bool_49);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_49.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_49.False;
		if (num)
		{
			Relay_In_2();
		}
		if (flag)
		{
			Relay_True_46();
		}
	}

	private void Relay_True_51()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_51.True(out logic_uScriptAct_SetBool_Target_51);
		local_ScrapperSpawned_System_Boolean = logic_uScriptAct_SetBool_Target_51;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_51.Out)
		{
			Relay_In_54();
		}
	}

	private void Relay_False_51()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_51.False(out logic_uScriptAct_SetBool_Target_51);
		local_ScrapperSpawned_System_Boolean = logic_uScriptAct_SetBool_Target_51;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_51.Out)
		{
			Relay_In_54();
		}
	}

	private void Relay_In_53()
	{
		logic_uScriptCon_CompareBool_Bool_53 = local_ScrapperSpawned_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_53.In(logic_uScriptCon_CompareBool_Bool_53);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_53.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_53.False;
		if (num)
		{
			Relay_In_59();
		}
		if (flag)
		{
			Relay_True_51();
		}
	}

	private void Relay_In_54()
	{
		int num = 0;
		Array array = blockSpawnDataScrapper;
		if (logic_uScript_SpawnBlocksFromData_blockData_54.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SpawnBlocksFromData_blockData_54, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SpawnBlocksFromData_blockData_54, num, array.Length);
		num += array.Length;
		logic_uScript_SpawnBlocksFromData_ownerNode_54 = owner_Connection_55;
		logic_uScript_SpawnBlocksFromData_uScript_SpawnBlocksFromData_54.In(logic_uScript_SpawnBlocksFromData_blockData_54, logic_uScript_SpawnBlocksFromData_ownerNode_54);
		if (logic_uScript_SpawnBlocksFromData_uScript_SpawnBlocksFromData_54.Out)
		{
			Relay_In_59();
		}
	}

	private void Relay_In_59()
	{
		int num = 0;
		Array array = blockSpawnDataScrapper;
		if (logic_uScript_GetAndCheckBlocks_blockData_59.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckBlocks_blockData_59, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckBlocks_blockData_59, num, array.Length);
		num += array.Length;
		logic_uScript_GetAndCheckBlocks_ownerNode_59 = owner_Connection_58;
		logic_uScript_GetAndCheckBlocks_uScript_GetAndCheckBlocks_59.In(logic_uScript_GetAndCheckBlocks_blockData_59, logic_uScript_GetAndCheckBlocks_ownerNode_59, ref logic_uScript_GetAndCheckBlocks_blocks_59);
		bool allAlive = logic_uScript_GetAndCheckBlocks_uScript_GetAndCheckBlocks_59.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckBlocks_uScript_GetAndCheckBlocks_59.SomeAlive;
		if (allAlive)
		{
			Relay_In_62();
		}
		if (someAlive)
		{
			Relay_In_62();
		}
	}

	private void Relay_In_62()
	{
		logic_uScriptCon_CompareBool_Bool_62 = local_msgScrapperSpawnedShown_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_62.In(logic_uScriptCon_CompareBool_Bool_62);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_62.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_62.False;
		if (num)
		{
			Relay_In_110();
		}
		if (flag)
		{
			Relay_In_107();
		}
	}

	private void Relay_True_63()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_63.True(out logic_uScriptAct_SetBool_Target_63);
		local_msgScrapperSpawnedShown_System_Boolean = logic_uScriptAct_SetBool_Target_63;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_63.Out)
		{
			Relay_In_21();
		}
	}

	private void Relay_False_63()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_63.False(out logic_uScriptAct_SetBool_Target_63);
		local_msgScrapperSpawnedShown_System_Boolean = logic_uScriptAct_SetBool_Target_63;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_63.Out)
		{
			Relay_In_21();
		}
	}

	private void Relay_In_64()
	{
		logic_uScript_IsCraftingBlockInOperation_craftingBlock_64 = local_ScrapperBlock_TankBlock;
		logic_uScript_IsCraftingBlockInOperation_uScript_IsCraftingBlockInOperation_64.In(logic_uScript_IsCraftingBlockInOperation_craftingBlock_64);
		if (logic_uScript_IsCraftingBlockInOperation_uScript_IsCraftingBlockInOperation_64.True)
		{
			Relay_In_68();
		}
	}

	private void Relay_True_66()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_66.True(out logic_uScriptAct_SetBool_Target_66);
		local_BlockInScrapper_System_Boolean = logic_uScriptAct_SetBool_Target_66;
	}

	private void Relay_False_66()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_66.False(out logic_uScriptAct_SetBool_Target_66);
		local_BlockInScrapper_System_Boolean = logic_uScriptAct_SetBool_Target_66;
	}

	private void Relay_In_68()
	{
		logic_uScript_HideArrow_uScript_HideArrow_68.In();
		if (logic_uScript_HideArrow_uScript_HideArrow_68.Out)
		{
			Relay_In_153();
		}
	}

	private void Relay_In_69()
	{
		logic_uScript_PointArrowAtVisible_targetObject_69 = local_ScrapperBlock_TankBlock;
		logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_69.In(logic_uScript_PointArrowAtVisible_targetObject_69, logic_uScript_PointArrowAtVisible_timeToShowFor_69, logic_uScript_PointArrowAtVisible_offset_69);
		if (logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_69.Out)
		{
			Relay_In_150();
		}
	}

	private void Relay_Save_Out_71()
	{
		Relay_Save_74();
	}

	private void Relay_Load_Out_71()
	{
		Relay_Load_74();
	}

	private void Relay_Restart_Out_71()
	{
		Relay_Set_False_74();
	}

	private void Relay_Save_71()
	{
		logic_SubGraph_SaveLoadBool_boolean_71 = local_msgIntroShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_71 = local_msgIntroShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_71.Save(ref logic_SubGraph_SaveLoadBool_boolean_71, logic_SubGraph_SaveLoadBool_boolAsVariable_71, logic_SubGraph_SaveLoadBool_uniqueID_71);
	}

	private void Relay_Load_71()
	{
		logic_SubGraph_SaveLoadBool_boolean_71 = local_msgIntroShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_71 = local_msgIntroShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_71.Load(ref logic_SubGraph_SaveLoadBool_boolean_71, logic_SubGraph_SaveLoadBool_boolAsVariable_71, logic_SubGraph_SaveLoadBool_uniqueID_71);
	}

	private void Relay_Set_True_71()
	{
		logic_SubGraph_SaveLoadBool_boolean_71 = local_msgIntroShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_71 = local_msgIntroShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_71.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_71, logic_SubGraph_SaveLoadBool_boolAsVariable_71, logic_SubGraph_SaveLoadBool_uniqueID_71);
	}

	private void Relay_Set_False_71()
	{
		logic_SubGraph_SaveLoadBool_boolean_71 = local_msgIntroShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_71 = local_msgIntroShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_71.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_71, logic_SubGraph_SaveLoadBool_boolAsVariable_71, logic_SubGraph_SaveLoadBool_uniqueID_71);
	}

	private void Relay_Save_Out_74()
	{
		Relay_Save_76();
	}

	private void Relay_Load_Out_74()
	{
		Relay_Load_76();
	}

	private void Relay_Restart_Out_74()
	{
		Relay_Set_False_76();
	}

	private void Relay_Save_74()
	{
		logic_SubGraph_SaveLoadBool_boolean_74 = local_ScrapperSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_74 = local_ScrapperSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_74.Save(ref logic_SubGraph_SaveLoadBool_boolean_74, logic_SubGraph_SaveLoadBool_boolAsVariable_74, logic_SubGraph_SaveLoadBool_uniqueID_74);
	}

	private void Relay_Load_74()
	{
		logic_SubGraph_SaveLoadBool_boolean_74 = local_ScrapperSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_74 = local_ScrapperSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_74.Load(ref logic_SubGraph_SaveLoadBool_boolean_74, logic_SubGraph_SaveLoadBool_boolAsVariable_74, logic_SubGraph_SaveLoadBool_uniqueID_74);
	}

	private void Relay_Set_True_74()
	{
		logic_SubGraph_SaveLoadBool_boolean_74 = local_ScrapperSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_74 = local_ScrapperSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_74.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_74, logic_SubGraph_SaveLoadBool_boolAsVariable_74, logic_SubGraph_SaveLoadBool_uniqueID_74);
	}

	private void Relay_Set_False_74()
	{
		logic_SubGraph_SaveLoadBool_boolean_74 = local_ScrapperSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_74 = local_ScrapperSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_74.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_74, logic_SubGraph_SaveLoadBool_boolAsVariable_74, logic_SubGraph_SaveLoadBool_uniqueID_74);
	}

	private void Relay_Save_Out_76()
	{
	}

	private void Relay_Load_Out_76()
	{
		Relay_In_89();
	}

	private void Relay_Restart_Out_76()
	{
		Relay_False_78();
	}

	private void Relay_Save_76()
	{
		logic_SubGraph_SaveLoadBool_boolean_76 = local_msgScrapperSpawnedShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_76 = local_msgScrapperSpawnedShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_76.Save(ref logic_SubGraph_SaveLoadBool_boolean_76, logic_SubGraph_SaveLoadBool_boolAsVariable_76, logic_SubGraph_SaveLoadBool_uniqueID_76);
	}

	private void Relay_Load_76()
	{
		logic_SubGraph_SaveLoadBool_boolean_76 = local_msgScrapperSpawnedShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_76 = local_msgScrapperSpawnedShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_76.Load(ref logic_SubGraph_SaveLoadBool_boolean_76, logic_SubGraph_SaveLoadBool_boolAsVariable_76, logic_SubGraph_SaveLoadBool_uniqueID_76);
	}

	private void Relay_Set_True_76()
	{
		logic_SubGraph_SaveLoadBool_boolean_76 = local_msgScrapperSpawnedShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_76 = local_msgScrapperSpawnedShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_76.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_76, logic_SubGraph_SaveLoadBool_boolAsVariable_76, logic_SubGraph_SaveLoadBool_uniqueID_76);
	}

	private void Relay_Set_False_76()
	{
		logic_SubGraph_SaveLoadBool_boolean_76 = local_msgScrapperSpawnedShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_76 = local_msgScrapperSpawnedShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_76.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_76, logic_SubGraph_SaveLoadBool_boolAsVariable_76, logic_SubGraph_SaveLoadBool_uniqueID_76);
	}

	private void Relay_True_78()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_78.True(out logic_uScriptAct_SetBool_Target_78);
		local_NearBase_System_Boolean = logic_uScriptAct_SetBool_Target_78;
	}

	private void Relay_False_78()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_78.False(out logic_uScriptAct_SetBool_Target_78);
		local_NearBase_System_Boolean = logic_uScriptAct_SetBool_Target_78;
	}

	private void Relay_Out_80()
	{
		Relay_In_25();
	}

	private void Relay_In_80()
	{
		int num = 0;
		Array array = blockSpawnDataScrapper;
		if (logic_SubGraph_Crafting_Tutorial_ManageBlock_blockSpawnData_80.Length != num + array.Length)
		{
			Array.Resize(ref logic_SubGraph_Crafting_Tutorial_ManageBlock_blockSpawnData_80, num + array.Length);
		}
		Array.Copy(array, 0, logic_SubGraph_Crafting_Tutorial_ManageBlock_blockSpawnData_80, num, array.Length);
		num += array.Length;
		logic_SubGraph_Crafting_Tutorial_ManageBlock_block_80 = local_ScrapperBlock_TankBlock;
		logic_SubGraph_Crafting_Tutorial_ManageBlock_msgBlockOutsideArea_80 = msgBlockOutsideArea;
		logic_SubGraph_Crafting_Tutorial_ManageBlock_messageSpeaker_80 = messageSpeaker;
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_80.In(ref logic_SubGraph_Crafting_Tutorial_ManageBlock_blockSpawnData_80, logic_SubGraph_Crafting_Tutorial_ManageBlock_blockIndex_80, ref logic_SubGraph_Crafting_Tutorial_ManageBlock_block_80, logic_SubGraph_Crafting_Tutorial_ManageBlock_allowAnchoring_80, logic_SubGraph_Crafting_Tutorial_ManageBlock_msgBlockOutsideArea_80, logic_SubGraph_Crafting_Tutorial_ManageBlock_messageSpeaker_80);
	}

	private void Relay_In_81()
	{
		logic_uScriptCon_CompareBool_Bool_81 = local_ScrapperSpawned_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_81.In(logic_uScriptCon_CompareBool_Bool_81);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_81.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_81.False;
		if (num)
		{
			Relay_In_80();
		}
		if (flag)
		{
			Relay_In_84();
		}
	}

	private void Relay_In_84()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_84.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_84.Out)
		{
			Relay_In_25();
		}
	}

	private void Relay_Output1_86()
	{
		Relay_In_96();
	}

	private void Relay_Output2_86()
	{
		Relay_In_32();
	}

	private void Relay_Output3_86()
	{
		Relay_In_7();
	}

	private void Relay_Output4_86()
	{
	}

	private void Relay_Output5_86()
	{
	}

	private void Relay_Output6_86()
	{
	}

	private void Relay_Output7_86()
	{
	}

	private void Relay_Output8_86()
	{
	}

	private void Relay_In_86()
	{
		logic_uScriptCon_ManualSwitch_CurrentOutput_86 = local_Stage_System_Int32;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_86.In(logic_uScriptCon_ManualSwitch_CurrentOutput_86);
	}

	private void Relay_Save_Out_87()
	{
		Relay_Save_9();
	}

	private void Relay_Load_Out_87()
	{
		Relay_Load_9();
	}

	private void Relay_Restart_Out_87()
	{
		Relay_Set_False_9();
	}

	private void Relay_Save_87()
	{
		logic_SubGraph_SaveLoadInt_integer_87 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_87 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_87.Save(logic_SubGraph_SaveLoadInt_restartValue_87, ref logic_SubGraph_SaveLoadInt_integer_87, logic_SubGraph_SaveLoadInt_intAsVariable_87, logic_SubGraph_SaveLoadInt_uniqueID_87);
	}

	private void Relay_Load_87()
	{
		logic_SubGraph_SaveLoadInt_integer_87 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_87 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_87.Load(logic_SubGraph_SaveLoadInt_restartValue_87, ref logic_SubGraph_SaveLoadInt_integer_87, logic_SubGraph_SaveLoadInt_intAsVariable_87, logic_SubGraph_SaveLoadInt_uniqueID_87);
	}

	private void Relay_Restart_87()
	{
		logic_SubGraph_SaveLoadInt_integer_87 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_87 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_87.Restart(logic_SubGraph_SaveLoadInt_restartValue_87, ref logic_SubGraph_SaveLoadInt_integer_87, logic_SubGraph_SaveLoadInt_intAsVariable_87, logic_SubGraph_SaveLoadInt_uniqueID_87);
	}

	private void Relay_Out_89()
	{
	}

	private void Relay_In_89()
	{
		logic_SubGraph_LoadObjectiveStates_currentObjective_89 = local_Stage_System_Int32;
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_89.In(logic_SubGraph_LoadObjectiveStates_currentObjective_89);
	}

	private void Relay_Out_96()
	{
		Relay_In_160();
	}

	private void Relay_In_96()
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_96 = local_Stage_System_Int32;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_96.In(ref logic_SubGraph_CompleteObjectiveStage_objectiveStage_96, logic_SubGraph_CompleteObjectiveStage_isFinalObjective_96);
	}

	private void Relay_Out_98()
	{
	}

	private void Relay_In_98()
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_98 = local_Stage_System_Int32;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_98.In(ref logic_SubGraph_CompleteObjectiveStage_objectiveStage_98, logic_SubGraph_CompleteObjectiveStage_isFinalObjective_98);
	}

	private void Relay_In_100()
	{
		logic_uScript_AddMessage_messageData_100 = msg01Intro;
		logic_uScript_AddMessage_speaker_100 = messageSpeaker;
		logic_uScript_AddMessage_Return_100 = logic_uScript_AddMessage_uScript_AddMessage_100.In(logic_uScript_AddMessage_messageData_100, logic_uScript_AddMessage_speaker_100);
		if (logic_uScript_AddMessage_uScript_AddMessage_100.Out)
		{
			Relay_In_2();
		}
	}

	private void Relay_In_103()
	{
		logic_uScript_AddMessage_messageData_103 = msg02BaseFound;
		logic_uScript_AddMessage_speaker_103 = messageSpeaker;
		logic_uScript_AddMessage_Return_103 = logic_uScript_AddMessage_uScript_AddMessage_103.In(logic_uScript_AddMessage_messageData_103, logic_uScript_AddMessage_speaker_103);
		if (logic_uScript_AddMessage_uScript_AddMessage_103.Shown)
		{
			Relay_True_34();
		}
	}

	private void Relay_In_107()
	{
		logic_uScript_AddMessage_messageData_107 = msg03ScrapperSpawned;
		logic_uScript_AddMessage_speaker_107 = messageSpeaker;
		logic_uScript_AddMessage_Return_107 = logic_uScript_AddMessage_uScript_AddMessage_107.In(logic_uScript_AddMessage_messageData_107, logic_uScript_AddMessage_speaker_107);
		if (logic_uScript_AddMessage_uScript_AddMessage_107.Shown)
		{
			Relay_True_63();
		}
	}

	private void Relay_In_110()
	{
		logic_uScript_AddMessage_messageData_110 = msg04AttachScrapper;
		logic_uScript_AddMessage_speaker_110 = messageSpeaker;
		logic_uScript_AddMessage_Return_110 = logic_uScript_AddMessage_uScript_AddMessage_110.In(logic_uScript_AddMessage_messageData_110, logic_uScript_AddMessage_speaker_110);
		if (logic_uScript_AddMessage_uScript_AddMessage_110.Out)
		{
			Relay_In_21();
		}
	}

	private void Relay_In_113()
	{
		logic_uScript_AddMessage_messageData_113 = msg05ScrapperAttached;
		logic_uScript_AddMessage_speaker_113 = messageSpeaker;
		logic_uScript_AddMessage_Return_113 = logic_uScript_AddMessage_uScript_AddMessage_113.In(logic_uScript_AddMessage_messageData_113, logic_uScript_AddMessage_speaker_113);
		if (logic_uScript_AddMessage_uScript_AddMessage_113.Shown)
		{
			Relay_True_29();
		}
	}

	private void Relay_In_116()
	{
		logic_uScript_AddMessage_messageData_116 = msg06PutBlockInScrapper;
		logic_uScript_AddMessage_speaker_116 = messageSpeaker;
		logic_uScript_AddMessage_Return_116 = logic_uScript_AddMessage_uScript_AddMessage_116.In(logic_uScript_AddMessage_messageData_116, logic_uScript_AddMessage_speaker_116);
		if (logic_uScript_AddMessage_uScript_AddMessage_116.Out)
		{
			Relay_In_69();
		}
	}

	private void Relay_In_119()
	{
		logic_uScript_AddMessage_messageData_119 = msg07ScrappingInProgress;
		logic_uScript_AddMessage_speaker_119 = messageSpeaker;
		logic_uScript_AddMessage_Return_119 = logic_uScript_AddMessage_uScript_AddMessage_119.In(logic_uScript_AddMessage_messageData_119, logic_uScript_AddMessage_speaker_119);
		if (logic_uScript_AddMessage_uScript_AddMessage_119.Out)
		{
			Relay_In_17();
		}
	}

	private void Relay_In_120()
	{
		logic_uScript_AddMessage_messageData_120 = msg08Complete;
		logic_uScript_AddMessage_speaker_120 = messageSpeaker;
		logic_uScript_AddMessage_Return_120 = logic_uScript_AddMessage_uScript_AddMessage_120.In(logic_uScript_AddMessage_messageData_120, logic_uScript_AddMessage_speaker_120);
		if (logic_uScript_AddMessage_uScript_AddMessage_120.Shown)
		{
			Relay_In_132();
		}
	}

	private void Relay_In_126()
	{
		logic_uScriptCon_CompareBool_Bool_126 = local_NearBase_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_126.In(logic_uScriptCon_CompareBool_Bool_126);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_126.True)
		{
			Relay_In_131();
		}
	}

	private void Relay_True_127()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_127.True(out logic_uScriptAct_SetBool_Target_127);
		local_NearBase_System_Boolean = logic_uScriptAct_SetBool_Target_127;
	}

	private void Relay_False_127()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_127.False(out logic_uScriptAct_SetBool_Target_127);
		local_NearBase_System_Boolean = logic_uScriptAct_SetBool_Target_127;
	}

	private void Relay_In_131()
	{
		logic_uScript_AddMessage_messageData_131 = msgLeavingMissionArea;
		logic_uScript_AddMessage_speaker_131 = messageSpeaker;
		logic_uScript_AddMessage_Return_131 = logic_uScript_AddMessage_uScript_AddMessage_131.In(logic_uScript_AddMessage_messageData_131, logic_uScript_AddMessage_speaker_131);
		if (logic_uScript_AddMessage_uScript_AddMessage_131.Out)
		{
			Relay_False_127();
		}
	}

	private void Relay_Out_132()
	{
		Relay_In_159();
	}

	private void Relay_In_132()
	{
		logic_SubGraph_Crafting_Tutorial_Finish_craftingBaseTech_132 = local_CraftingBaseTech_Tank;
		logic_SubGraph_Crafting_Tutorial_Finish_NPCTech_132 = local_NPCTech_Tank;
		logic_SubGraph_Crafting_Tutorial_Finish_NPCFlyAwayAI_132 = NPCFlyAwayAI;
		logic_SubGraph_Crafting_Tutorial_Finish_NPCDespawnParticleEffect_132 = NPCDespawnParticleEffect;
		logic_SubGraph_Crafting_Tutorial_Finish_SubGraph_Crafting_Tutorial_Finish_132.In(logic_SubGraph_Crafting_Tutorial_Finish_craftingBaseTech_132, logic_SubGraph_Crafting_Tutorial_Finish_NPCTech_132, logic_SubGraph_Crafting_Tutorial_Finish_NPCFlyAwayAI_132, logic_SubGraph_Crafting_Tutorial_Finish_NPCDespawnParticleEffect_132);
	}

	private void Relay_Out_143()
	{
		Relay_In_81();
	}

	private void Relay_In_143()
	{
		int num = 0;
		Array array = baseSpawnData;
		if (logic_SubGraph_Crafting_Tutorial_Init_baseSpawnData_143.Length != num + array.Length)
		{
			Array.Resize(ref logic_SubGraph_Crafting_Tutorial_Init_baseSpawnData_143, num + array.Length);
		}
		Array.Copy(array, 0, logic_SubGraph_Crafting_Tutorial_Init_baseSpawnData_143, num, array.Length);
		num += array.Length;
		int num2 = 0;
		Array array2 = blockSpawnData;
		if (logic_SubGraph_Crafting_Tutorial_Init_blockSpawnData_143.Length != num2 + array2.Length)
		{
			Array.Resize(ref logic_SubGraph_Crafting_Tutorial_Init_blockSpawnData_143, num2 + array2.Length);
		}
		Array.Copy(array2, 0, logic_SubGraph_Crafting_Tutorial_Init_blockSpawnData_143, num2, array2.Length);
		num2 += array2.Length;
		int num3 = 0;
		Array nPCSpawnData = NPCSpawnData;
		if (logic_SubGraph_Crafting_Tutorial_Init_NPCSpawnData_143.Length != num3 + nPCSpawnData.Length)
		{
			Array.Resize(ref logic_SubGraph_Crafting_Tutorial_Init_NPCSpawnData_143, num3 + nPCSpawnData.Length);
		}
		Array.Copy(nPCSpawnData, 0, logic_SubGraph_Crafting_Tutorial_Init_NPCSpawnData_143, num3, nPCSpawnData.Length);
		num3 += nPCSpawnData.Length;
		logic_SubGraph_Crafting_Tutorial_Init_completedBasePreset_143 = completedBasePreset;
		logic_SubGraph_Crafting_Tutorial_Init_basePosition_143 = basePosition;
		logic_SubGraph_Crafting_Tutorial_Init_clearSceneryRadius_143 = clearSceneryRadius;
		logic_SubGraph_Crafting_Tutorial_Init_CraftingBaseTech_143 = local_CraftingBaseTech_Tank;
		logic_SubGraph_Crafting_Tutorial_Init_NPCTech_143 = local_NPCTech_Tank;
		logic_SubGraph_Crafting_Tutorial_Init_SubGraph_Crafting_Tutorial_Init_143.In(logic_SubGraph_Crafting_Tutorial_Init_baseSpawnData_143, logic_SubGraph_Crafting_Tutorial_Init_blockSpawnData_143, logic_SubGraph_Crafting_Tutorial_Init_NPCSpawnData_143, logic_SubGraph_Crafting_Tutorial_Init_completedBasePreset_143, logic_SubGraph_Crafting_Tutorial_Init_useNPCAsTutorialTech_143, logic_SubGraph_Crafting_Tutorial_Init_spawnBase_143, logic_SubGraph_Crafting_Tutorial_Init_basePosition_143, logic_SubGraph_Crafting_Tutorial_Init_clearSceneryRadius_143, ref logic_SubGraph_Crafting_Tutorial_Init_CraftingBaseTech_143, ref logic_SubGraph_Crafting_Tutorial_Init_NPCTech_143);
	}

	private void Relay_In_149()
	{
		logic_uScript_SetEncounterTarget_owner_149 = owner_Connection_148;
		logic_uScript_SetEncounterTarget_visibleObject_149 = local_NPCTech_Tank;
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_149.In(logic_uScript_SetEncounterTarget_owner_149, logic_uScript_SetEncounterTarget_visibleObject_149);
		if (logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_149.Out)
		{
			Relay_In_47();
		}
	}

	private void Relay_In_150()
	{
		logic_uScript_EnableGlow_targetObject_150 = local_ScrapperBlock_TankBlock;
		logic_uScript_EnableGlow_uScript_EnableGlow_150.In(logic_uScript_EnableGlow_targetObject_150, logic_uScript_EnableGlow_enable_150);
		if (logic_uScript_EnableGlow_uScript_EnableGlow_150.Out)
		{
			Relay_In_64();
		}
	}

	private void Relay_In_153()
	{
		logic_uScript_EnableGlow_targetObject_153 = local_ScrapperBlock_TankBlock;
		logic_uScript_EnableGlow_uScript_EnableGlow_153.In(logic_uScript_EnableGlow_targetObject_153, logic_uScript_EnableGlow_enable_153);
		if (logic_uScript_EnableGlow_uScript_EnableGlow_153.Out)
		{
			Relay_True_66();
		}
	}

	private void Relay_In_154()
	{
		logic_uScriptCon_CompareBool_Bool_154 = local_ScrapperSpawned_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_154.In(logic_uScriptCon_CompareBool_Bool_154);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_154.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_154.False;
		if (num)
		{
			Relay_In_156();
		}
		if (flag)
		{
			Relay_In_158();
		}
	}

	private void Relay_In_156()
	{
		logic_uScript_EnableGlow_targetObject_156 = local_ScrapperBlock_TankBlock;
		logic_uScript_EnableGlow_uScript_EnableGlow_156.In(logic_uScript_EnableGlow_targetObject_156, logic_uScript_EnableGlow_enable_156);
		if (logic_uScript_EnableGlow_uScript_EnableGlow_156.Out)
		{
			Relay_In_126();
		}
	}

	private void Relay_In_158()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_158.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_158.Out)
		{
			Relay_In_126();
		}
	}

	private void Relay_In_159()
	{
		logic_uScript_SendAnaliticsEvent_uScript_SendAnaliticsEvent_159.In(logic_uScript_SendAnaliticsEvent_analiticsEvent_159, logic_uScript_SendAnaliticsEvent_parameterName_159, logic_uScript_SendAnaliticsEvent_parameter_159);
	}

	private void Relay_In_160()
	{
		logic_uScript_SendAnaliticsEvent_uScript_SendAnaliticsEvent_160.In(logic_uScript_SendAnaliticsEvent_analiticsEvent_160, logic_uScript_SendAnaliticsEvent_parameterName_160, logic_uScript_SendAnaliticsEvent_parameter_160);
	}
}
