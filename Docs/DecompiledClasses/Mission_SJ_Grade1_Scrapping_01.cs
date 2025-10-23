using System;
using BehaviorDesigner.Runtime;
using UnityEngine;

[Serializable]
[NodePath("Graphs")]
[FriendlyName("Untitled", "")]
public class Mission_SJ_Grade1_Scrapping_01 : uScriptLogic
{
	private GameObject parentGameObject;

	private uScript_GUI thisScriptsOnGuiListener;

	private bool m_RegisteredForEvents;

	[Multiline(3)]
	public string basePosition = "";

	public SpawnTechData[] baseSpawnData = new SpawnTechData[0];

	public SpawnBlockData[] blockSpawnData = new SpawnBlockData[0];

	public SpawnBlockData[] blockSpawnDataScrapper = new SpawnBlockData[0];

	public BlockTypes BlockTypeSJScrapper;

	public float clearSceneryRadius;

	public TankPreset completedBasePreset;

	public float distBaseFound;

	public GhostBlockSpawnData[] ghostBlockScrapper = new GhostBlockSpawnData[0];

	private string local_202_System_String = "";

	private string local_203_System_String = "Scrapped Non-SJ Block Too Early: ";

	private TankBlock local_215_TankBlock;

	private TankBlock local_224_TankBlock;

	private FactionSubTypes local_250_FactionSubTypes = FactionSubTypes.SJ;

	private FactionSubTypes local_272_FactionSubTypes = FactionSubTypes.SJ;

	private bool local_CanGrabResources_System_Boolean;

	private bool local_CanSuckUpScrapBlock1_System_Boolean;

	private bool local_CanSuckUpScrapBlock2_System_Boolean;

	private Tank local_CraftingBaseTech_Tank;

	private bool local_msgBaseFoundShown_System_Boolean;

	private bool local_msgBlockScrappedShown_System_Boolean;

	private bool local_msgIntroShown_System_Boolean;

	private bool local_msgScrapperAttachedShown_System_Boolean;

	private bool local_msgScrapperSpawnedShown_System_Boolean;

	private bool local_NearBase_System_Boolean;

	private Tank local_NPCTech_Tank;

	private bool local_OtherCorpBlockInScrapper_System_Boolean;

	private string local_ScrapBlock1Name_System_String = "ScrapBlock1";

	private string local_ScrapBlock2Name_System_String = "ScrapBlock2";

	private bool local_ScrappedOtherCorpBlockEarly_System_Boolean;

	private TankBlock local_ScrapperBlock_TankBlock;

	private bool local_ScrapperSpawned_System_Boolean;

	private bool local_ScrappingInProgress_System_Boolean;

	private bool local_ScrappingOtherCorpBlockInProgress_System_Boolean;

	private bool local_SJBlockInScrapper_System_Boolean;

	private int local_Stage_System_Int32;

	private int local_Stage2_System_Int32;

	public uScript_AddMessage.MessageSpeaker messageSpeaker;

	[Multiline(3)]
	public string messageTag = "";

	public uScript_AddMessage.MessageData msg01Intro;

	public uScript_AddMessage.MessageData msg02BaseFound;

	public uScript_AddMessage.MessageData msg03ScrapperSpawned;

	public uScript_AddMessage.MessageData msg04AttachScrapper;

	public uScript_AddMessage.MessageData msg05ScrapperAttached;

	public uScript_AddMessage.MessageData msg06PutBlock1InScrapper;

	public uScript_AddMessage.MessageData msg07ScrappingBlock1_InProgress;

	public uScript_AddMessage.MessageData msg08ScrappedBlock1;

	public uScript_AddMessage.MessageData msg09Block1Scrapped;

	public uScript_AddMessage.MessageData msg10PutBlock2InScrapper;

	public uScript_AddMessage.MessageData msg11ScrappingBlock2_InProgress;

	public uScript_AddMessage.MessageData msg13Complete;

	public uScript_AddMessage.MessageData msg14AltComplete;

	public uScript_AddMessage.MessageData msgBlockOutsideArea;

	public uScript_AddMessage.MessageData msgLeavingMissionArea;

	public Transform NPCDespawnParticleEffect;

	public ExternalBehaviorTree NPCFlyAwayAI;

	public SpawnTechData[] NPCSpawnData = new SpawnTechData[0];

	private GameObject owner_Connection_27;

	private GameObject owner_Connection_52;

	private GameObject owner_Connection_103;

	private GameObject owner_Connection_108;

	private GameObject owner_Connection_129;

	private GameObject owner_Connection_218;

	private GameObject owner_Connection_220;

	private GameObject owner_Connection_631;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_0 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_0;

	private bool logic_uScriptAct_SetBool_Out_0 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_0 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_0 = true;

	private uScript_SetEncounterTarget logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_4 = new uScript_SetEncounterTarget();

	private GameObject logic_uScript_SetEncounterTarget_owner_4;

	private object logic_uScript_SetEncounterTarget_visibleObject_4 = "";

	private bool logic_uScript_SetEncounterTarget_Out_4 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_6 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_6;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_6;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_6;

	private bool logic_uScript_AddMessage_Out_6 = true;

	private bool logic_uScript_AddMessage_Shown_6 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_7 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_7 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_7 = true;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_7 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_9 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_9;

	private bool logic_uScriptCon_CompareBool_True_9 = true;

	private bool logic_uScriptCon_CompareBool_False_9 = true;

	private uScript_GetAndCheckBlocks logic_uScript_GetAndCheckBlocks_uScript_GetAndCheckBlocks_11 = new uScript_GetAndCheckBlocks();

	private SpawnBlockData[] logic_uScript_GetAndCheckBlocks_blockData_11 = new SpawnBlockData[0];

	private GameObject logic_uScript_GetAndCheckBlocks_ownerNode_11;

	private TankBlock[] logic_uScript_GetAndCheckBlocks_blocks_11 = new TankBlock[0];

	private bool logic_uScript_GetAndCheckBlocks_AllAlive_11 = true;

	private bool logic_uScript_GetAndCheckBlocks_SomeAlive_11 = true;

	private bool logic_uScript_GetAndCheckBlocks_AllDead_11 = true;

	private bool logic_uScript_GetAndCheckBlocks_WaitingToSpawn_11 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_13 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_13;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_13 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_13 = "msgIntroShown";

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_14 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_14;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_14;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_14;

	private bool logic_uScript_AddMessage_Out_14 = true;

	private bool logic_uScript_AddMessage_Shown_14 = true;

	private uScript_HideArrow logic_uScript_HideArrow_uScript_HideArrow_16 = new uScript_HideArrow();

	private bool logic_uScript_HideArrow_Out_16 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_17 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_17;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_17 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_17 = "SJBlockInScrapper";

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_18 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_18;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_18;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_18;

	private bool logic_uScript_AddMessage_Out_18 = true;

	private bool logic_uScript_AddMessage_Shown_18 = true;

	private SubGraph_CompleteObjectiveStage logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_24 = new SubGraph_CompleteObjectiveStage();

	private int logic_SubGraph_CompleteObjectiveStage_objectiveStage_24;

	private bool logic_SubGraph_CompleteObjectiveStage_isFinalObjective_24;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_30 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_30;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_30 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_30 = "msgScrapperAttachedShown";

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_32 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_32;

	private bool logic_uScriptCon_CompareBool_True_32 = true;

	private bool logic_uScriptCon_CompareBool_False_32 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_33 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_33;

	private bool logic_uScriptAct_SetBool_Out_33 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_33 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_33 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_34 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_34;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_34;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_34;

	private bool logic_uScript_AddMessage_Out_34 = true;

	private bool logic_uScript_AddMessage_Shown_34 = true;

	private SubGraph_SaveLoadInt logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_38 = new SubGraph_SaveLoadInt();

	private int logic_SubGraph_SaveLoadInt_restartValue_38 = 1;

	private int logic_SubGraph_SaveLoadInt_integer_38;

	private object logic_SubGraph_SaveLoadInt_intAsVariable_38 = "";

	private string logic_SubGraph_SaveLoadInt_uniqueID_38 = "Stage";

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_39 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_39;

	private bool logic_uScriptCon_CompareBool_True_39 = true;

	private bool logic_uScriptCon_CompareBool_False_39 = true;

	private uScript_HideArrow logic_uScript_HideArrow_uScript_HideArrow_42 = new uScript_HideArrow();

	private bool logic_uScript_HideArrow_Out_42 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_43 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_43;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_43;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_43;

	private bool logic_uScript_AddMessage_Out_43 = true;

	private bool logic_uScript_AddMessage_Shown_43 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_44 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_44;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_44;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_44;

	private bool logic_uScript_AddMessage_Out_44 = true;

	private bool logic_uScript_AddMessage_Shown_44 = true;

	private uScript_PointArrowAtVisible logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_50 = new uScript_PointArrowAtVisible();

	private object logic_uScript_PointArrowAtVisible_targetObject_50 = "";

	private float logic_uScript_PointArrowAtVisible_timeToShowFor_50 = -1f;

	private Vector3 logic_uScript_PointArrowAtVisible_offset_50 = new Vector3(0f, 0f, 0f);

	private bool logic_uScript_PointArrowAtVisible_Out_50 = true;

	private uScript_PausePopulation logic_uScript_PausePopulation_uScript_PausePopulation_51 = new uScript_PausePopulation();

	private bool logic_uScript_PausePopulation_Out_51 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_53 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_53 = "";

	private bool logic_uScript_EnableGlow_enable_53;

	private bool logic_uScript_EnableGlow_Out_53 = true;

	private SubGraph_Crafting_Tutorial_AttachBlockToBase logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_SubGraph_Crafting_Tutorial_AttachBlockToBase_57 = new SubGraph_Crafting_Tutorial_AttachBlockToBase();

	private TankBlock logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_Block_57;

	private Tank logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_CraftingBaseTech_57;

	private GhostBlockSpawnData[] logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_ghostBlockData_57 = new GhostBlockSpawnData[0];

	private BlockTypes logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_BlockType_57;

	private Vector3 logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_BlockPosition_57 = new Vector3(4f, 0f, -3f);

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_58 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_58 = "";

	private bool logic_uScript_EnableGlow_enable_58;

	private bool logic_uScript_EnableGlow_Out_58 = true;

	private uScriptCon_ManualSwitch logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_60 = new uScriptCon_ManualSwitch();

	private int logic_uScriptCon_ManualSwitch_CurrentOutput_60;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_71 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_71;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_71 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_71 = "msgBaseFoundShown";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_75 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_75;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_75 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_75 = "ScrappingInProgress";

	private SubGraph_Crafting_Tutorial_ManageBlock logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_76 = new SubGraph_Crafting_Tutorial_ManageBlock();

	private SpawnBlockData[] logic_SubGraph_Crafting_Tutorial_ManageBlock_blockSpawnData_76 = new SpawnBlockData[0];

	private int logic_SubGraph_Crafting_Tutorial_ManageBlock_blockIndex_76;

	private TankBlock logic_SubGraph_Crafting_Tutorial_ManageBlock_block_76;

	private bool logic_SubGraph_Crafting_Tutorial_ManageBlock_allowAnchoring_76;

	private uScript_AddMessage.MessageData logic_SubGraph_Crafting_Tutorial_ManageBlock_msgBlockOutsideArea_76;

	private uScript_AddMessage.MessageSpeaker logic_SubGraph_Crafting_Tutorial_ManageBlock_messageSpeaker_76;

	private uScript_IsPlayerInRangeOfTech logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_78 = new uScript_IsPlayerInRangeOfTech();

	private Tank logic_uScript_IsPlayerInRangeOfTech_tech_78;

	private float logic_uScript_IsPlayerInRangeOfTech_range_78 = 100f;

	private Tank[] logic_uScript_IsPlayerInRangeOfTech_techs_78 = new Tank[0];

	private bool logic_uScript_IsPlayerInRangeOfTech_Out_78 = true;

	private bool logic_uScript_IsPlayerInRangeOfTech_InRange_78 = true;

	private bool logic_uScript_IsPlayerInRangeOfTech_OutOfRange_78 = true;

	private uScript_SpawnBlocksFromData logic_uScript_SpawnBlocksFromData_uScript_SpawnBlocksFromData_80 = new uScript_SpawnBlocksFromData();

	private SpawnBlockData[] logic_uScript_SpawnBlocksFromData_blockData_80 = new SpawnBlockData[0];

	private GameObject logic_uScript_SpawnBlocksFromData_ownerNode_80;

	private bool logic_uScript_SpawnBlocksFromData_Out_80 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_81 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_81;

	private bool logic_uScriptCon_CompareBool_True_81 = true;

	private bool logic_uScriptCon_CompareBool_False_81 = true;

	private uScript_PausePopulation logic_uScript_PausePopulation_uScript_PausePopulation_82 = new uScript_PausePopulation();

	private bool logic_uScript_PausePopulation_Out_82 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_83 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_83;

	private bool logic_uScriptAct_SetBool_Out_83 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_83 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_83 = true;

	private uScript_IsPlayerInRangeOfTech logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_84 = new uScript_IsPlayerInRangeOfTech();

	private Tank logic_uScript_IsPlayerInRangeOfTech_tech_84;

	private float logic_uScript_IsPlayerInRangeOfTech_range_84;

	private Tank[] logic_uScript_IsPlayerInRangeOfTech_techs_84 = new Tank[0];

	private bool logic_uScript_IsPlayerInRangeOfTech_Out_84 = true;

	private bool logic_uScript_IsPlayerInRangeOfTech_InRange_84 = true;

	private bool logic_uScript_IsPlayerInRangeOfTech_OutOfRange_84 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_86 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_86;

	private bool logic_uScriptAct_SetBool_Out_86 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_86 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_86 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_87 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_87;

	private bool logic_uScriptCon_CompareBool_True_87 = true;

	private bool logic_uScriptCon_CompareBool_False_87 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_88 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_88;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_88 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_88 = "msgScrapperSpawnedShown";

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_90 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_90;

	private bool logic_uScriptCon_CompareBool_True_90 = true;

	private bool logic_uScriptCon_CompareBool_False_90 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_95 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_95;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_95 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_95 = "ScrapperSpawned";

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_96 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_96;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_96;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_96;

	private bool logic_uScript_AddMessage_Out_96 = true;

	private bool logic_uScript_AddMessage_Shown_96 = true;

	private SubGraph_CompleteObjectiveStage logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_97 = new SubGraph_CompleteObjectiveStage();

	private int logic_SubGraph_CompleteObjectiveStage_objectiveStage_97;

	private bool logic_SubGraph_CompleteObjectiveStage_isFinalObjective_97;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_104 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_104;

	private bool logic_uScriptAct_SetBool_Out_104 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_104 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_104 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_105 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_105 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_107 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_107 = "";

	private bool logic_uScript_EnableGlow_enable_107 = true;

	private bool logic_uScript_EnableGlow_Out_107 = true;

	private uScript_LockTechInteraction logic_uScript_LockTechInteraction_uScript_LockTechInteraction_112 = new uScript_LockTechInteraction();

	private Tank logic_uScript_LockTechInteraction_tech_112;

	private BlockTypes[] logic_uScript_LockTechInteraction_excludedBlocks_112 = new BlockTypes[0];

	private TankBlock[] logic_uScript_LockTechInteraction_excludedUniqueBlocks_112 = new TankBlock[0];

	private bool logic_uScript_LockTechInteraction_Out_112 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_117 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_117;

	private bool logic_uScriptAct_SetBool_Out_117 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_117 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_117 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_118 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_118;

	private bool logic_uScriptAct_SetBool_Out_118 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_118 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_118 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_119 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_119;

	private bool logic_uScriptCon_CompareBool_True_119 = true;

	private bool logic_uScriptCon_CompareBool_False_119 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_121 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_121 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_122 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_122;

	private bool logic_uScriptAct_SetBool_Out_122 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_122 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_122 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_124 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_124;

	private bool logic_uScriptAct_SetBool_Out_124 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_124 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_124 = true;

	private uScript_LockTechStacks logic_uScript_LockTechStacks_uScript_LockTechStacks_125 = new uScript_LockTechStacks();

	private Tank logic_uScript_LockTechStacks_tech_125;

	private bool logic_uScript_LockTechStacks_Out_125 = true;

	private SubGraph_Crafting_Tutorial_Init logic_SubGraph_Crafting_Tutorial_Init_SubGraph_Crafting_Tutorial_Init_126 = new SubGraph_Crafting_Tutorial_Init();

	private SpawnTechData[] logic_SubGraph_Crafting_Tutorial_Init_baseSpawnData_126 = new SpawnTechData[0];

	private SpawnBlockData[] logic_SubGraph_Crafting_Tutorial_Init_blockSpawnData_126 = new SpawnBlockData[0];

	private SpawnTechData[] logic_SubGraph_Crafting_Tutorial_Init_NPCSpawnData_126 = new SpawnTechData[0];

	private TankPreset logic_SubGraph_Crafting_Tutorial_Init_completedBasePreset_126;

	private bool logic_SubGraph_Crafting_Tutorial_Init_useNPCAsTutorialTech_126;

	private bool logic_SubGraph_Crafting_Tutorial_Init_spawnBase_126 = true;

	private string logic_SubGraph_Crafting_Tutorial_Init_basePosition_126 = "";

	private float logic_SubGraph_Crafting_Tutorial_Init_clearSceneryRadius_126;

	private Tank logic_SubGraph_Crafting_Tutorial_Init_CraftingBaseTech_126;

	private Tank logic_SubGraph_Crafting_Tutorial_Init_NPCTech_126;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_132 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_132;

	private bool logic_uScriptAct_SetBool_Out_132 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_132 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_132 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_136 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_136;

	private bool logic_uScriptCon_CompareBool_True_136 = true;

	private bool logic_uScriptCon_CompareBool_False_136 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_141 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_141;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_141;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_141;

	private bool logic_uScript_AddMessage_Out_141 = true;

	private bool logic_uScript_AddMessage_Shown_141 = true;

	private SubGraph_LoadObjectiveStates logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_144 = new SubGraph_LoadObjectiveStates();

	private int logic_SubGraph_LoadObjectiveStates_currentObjective_144;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_145 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_145;

	private bool logic_uScriptCon_CompareBool_True_145 = true;

	private bool logic_uScriptCon_CompareBool_False_145 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_147 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_147;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_147;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_147;

	private bool logic_uScript_AddMessage_Out_147 = true;

	private bool logic_uScript_AddMessage_Shown_147 = true;

	private uScript_IsCraftingBlockProducingItem logic_uScript_IsCraftingBlockProducingItem_uScript_IsCraftingBlockProducingItem_148 = new uScript_IsCraftingBlockProducingItem();

	private TankBlock logic_uScript_IsCraftingBlockProducingItem_craftingBlock_148;

	private bool logic_uScript_IsCraftingBlockProducingItem_True_148 = true;

	private bool logic_uScript_IsCraftingBlockProducingItem_False_148 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_149 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_149;

	private bool logic_uScriptCon_CompareBool_True_149 = true;

	private bool logic_uScriptCon_CompareBool_False_149 = true;

	private uScript_IsCraftingBlockProducingItem logic_uScript_IsCraftingBlockProducingItem_uScript_IsCraftingBlockProducingItem_156 = new uScript_IsCraftingBlockProducingItem();

	private TankBlock logic_uScript_IsCraftingBlockProducingItem_craftingBlock_156;

	private bool logic_uScript_IsCraftingBlockProducingItem_True_156 = true;

	private bool logic_uScript_IsCraftingBlockProducingItem_False_156 = true;

	private SubGraph_Crafting_Tutorial_Finish logic_SubGraph_Crafting_Tutorial_Finish_SubGraph_Crafting_Tutorial_Finish_157 = new SubGraph_Crafting_Tutorial_Finish();

	private Tank logic_SubGraph_Crafting_Tutorial_Finish_craftingBaseTech_157;

	private Tank logic_SubGraph_Crafting_Tutorial_Finish_NPCTech_157;

	private ExternalBehaviorTree logic_SubGraph_Crafting_Tutorial_Finish_NPCFlyAwayAI_157;

	private Transform logic_SubGraph_Crafting_Tutorial_Finish_NPCDespawnParticleEffect_157;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_158 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_158;

	private bool logic_uScriptCon_CompareBool_True_158 = true;

	private bool logic_uScriptCon_CompareBool_False_158 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_159 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_159;

	private bool logic_uScriptAct_SetBool_Out_159 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_159 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_159 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_162 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_162;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_162;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_162;

	private bool logic_uScript_AddMessage_Out_162 = true;

	private bool logic_uScript_AddMessage_Shown_162 = true;

	private SubGraph_CompleteObjectiveStage logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_165 = new SubGraph_CompleteObjectiveStage();

	private int logic_SubGraph_CompleteObjectiveStage_objectiveStage_165;

	private bool logic_SubGraph_CompleteObjectiveStage_isFinalObjective_165;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_166 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_166;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_166 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_166 = "OtherCorpBlockInScrapper";

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_168 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_168;

	private bool logic_uScriptCon_CompareBool_True_168 = true;

	private bool logic_uScriptCon_CompareBool_False_168 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_170 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_170;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_170 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_170 = "ScrappingInProgress";

	private uScript_PointArrowAtVisible logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_173 = new uScript_PointArrowAtVisible();

	private object logic_uScript_PointArrowAtVisible_targetObject_173 = "";

	private float logic_uScript_PointArrowAtVisible_timeToShowFor_173 = -1f;

	private Vector3 logic_uScript_PointArrowAtVisible_offset_173 = new Vector3(0f, 0f, 0f);

	private bool logic_uScript_PointArrowAtVisible_Out_173 = true;

	private uScript_IsCraftingBlockInOperation logic_uScript_IsCraftingBlockInOperation_uScript_IsCraftingBlockInOperation_176 = new uScript_IsCraftingBlockInOperation();

	private TankBlock logic_uScript_IsCraftingBlockInOperation_craftingBlock_176;

	private bool logic_uScript_IsCraftingBlockInOperation_True_176 = true;

	private bool logic_uScript_IsCraftingBlockInOperation_False_176 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_182 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_182;

	private bool logic_uScriptAct_SetBool_Out_182 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_182 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_182 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_183 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_183 = "";

	private bool logic_uScript_EnableGlow_enable_183 = true;

	private bool logic_uScript_EnableGlow_Out_183 = true;

	private uScript_HideArrow logic_uScript_HideArrow_uScript_HideArrow_184 = new uScript_HideArrow();

	private bool logic_uScript_HideArrow_Out_184 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_186 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_186;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_186;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_186;

	private bool logic_uScript_AddMessage_Out_186 = true;

	private bool logic_uScript_AddMessage_Shown_186 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_188 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_188 = "";

	private bool logic_uScript_EnableGlow_enable_188;

	private bool logic_uScript_EnableGlow_Out_188 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_189 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_189;

	private bool logic_uScriptAct_SetBool_Out_189 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_189 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_189 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_190 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_190;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_190;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_190;

	private bool logic_uScript_AddMessage_Out_190 = true;

	private bool logic_uScript_AddMessage_Shown_190 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_191 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_191;

	private bool logic_uScriptCon_CompareBool_True_191 = true;

	private bool logic_uScriptCon_CompareBool_False_191 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_197 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_197;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_197 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_197 = "msgBlockScrappedShown";

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_198 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_198;

	private bool logic_uScriptCon_CompareBool_True_198 = true;

	private bool logic_uScriptCon_CompareBool_False_198 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_200 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_200 = true;

	private uScriptAct_Concatenate logic_uScriptAct_Concatenate_uScriptAct_Concatenate_201 = new uScriptAct_Concatenate();

	private object[] logic_uScriptAct_Concatenate_A_201 = new object[0];

	private object[] logic_uScriptAct_Concatenate_B_201 = new object[0];

	private string logic_uScriptAct_Concatenate_Separator_201 = "";

	private string logic_uScriptAct_Concatenate_Result_201;

	private bool logic_uScriptAct_Concatenate_Out_201 = true;

	private uScriptAct_PrintText logic_uScriptAct_PrintText_uScriptAct_PrintText_205 = new uScriptAct_PrintText();

	private string logic_uScriptAct_PrintText_Text_205 = "";

	private int logic_uScriptAct_PrintText_FontSize_205 = 16;

	private FontStyle logic_uScriptAct_PrintText_FontStyle_205;

	private Color logic_uScriptAct_PrintText_FontColor_205 = new Color(0f, 0f, 0f, 1f);

	private TextAnchor logic_uScriptAct_PrintText_textAnchor_205;

	private int logic_uScriptAct_PrintText_EdgePadding_205 = 8;

	private float logic_uScriptAct_PrintText_time_205;

	private bool logic_uScriptAct_PrintText_Out_205 = true;

	private uScript_LockBlock logic_uScript_LockBlock_uScript_LockBlock_206 = new uScript_LockBlock();

	private TankBlock logic_uScript_LockBlock_block_206;

	private Visible.LockTimerTypes logic_uScript_LockBlock_functionalityToLock_206 = Visible.LockTimerTypes.ItemCollection;

	private bool logic_uScript_LockBlock_Out_206 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_208 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_208 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_209 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_209 = true;

	private uScript_GetNamedBlock logic_uScript_GetNamedBlock_uScript_GetNamedBlock_211 = new uScript_GetNamedBlock();

	private string logic_uScript_GetNamedBlock_name_211 = "";

	private GameObject logic_uScript_GetNamedBlock_owner_211;

	private TankBlock logic_uScript_GetNamedBlock_Return_211;

	private bool logic_uScript_GetNamedBlock_Out_211 = true;

	private bool logic_uScript_GetNamedBlock_Destroyed_211 = true;

	private bool logic_uScript_GetNamedBlock_BlockExists_211 = true;

	private bool logic_uScript_GetNamedBlock_WaitingForBlock_211 = true;

	private bool logic_uScript_GetNamedBlock_NoBlock_211 = true;

	private uScript_LockBlock logic_uScript_LockBlock_uScript_LockBlock_213 = new uScript_LockBlock();

	private TankBlock logic_uScript_LockBlock_block_213;

	private Visible.LockTimerTypes logic_uScript_LockBlock_functionalityToLock_213 = Visible.LockTimerTypes.BlocksAttachable;

	private bool logic_uScript_LockBlock_Out_213 = true;

	private uScript_LockBlock logic_uScript_LockBlock_uScript_LockBlock_214 = new uScript_LockBlock();

	private TankBlock logic_uScript_LockBlock_block_214;

	private Visible.LockTimerTypes logic_uScript_LockBlock_functionalityToLock_214 = Visible.LockTimerTypes.SendToSCU;

	private bool logic_uScript_LockBlock_Out_214 = true;

	private uScript_LockBlock logic_uScript_LockBlock_uScript_LockBlock_216 = new uScript_LockBlock();

	private TankBlock logic_uScript_LockBlock_block_216;

	private Visible.LockTimerTypes logic_uScript_LockBlock_functionalityToLock_216 = Visible.LockTimerTypes.BlocksAttachable;

	private bool logic_uScript_LockBlock_Out_216 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_217 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_217 = true;

	private uScript_LockBlock logic_uScript_LockBlock_uScript_LockBlock_219 = new uScript_LockBlock();

	private TankBlock logic_uScript_LockBlock_block_219;

	private Visible.LockTimerTypes logic_uScript_LockBlock_functionalityToLock_219 = Visible.LockTimerTypes.StackAccept;

	private bool logic_uScript_LockBlock_Out_219 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_221 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_221;

	private bool logic_uScriptCon_CompareBool_True_221 = true;

	private bool logic_uScriptCon_CompareBool_False_221 = true;

	private uScript_LockBlock logic_uScript_LockBlock_uScript_LockBlock_222 = new uScript_LockBlock();

	private TankBlock logic_uScript_LockBlock_block_222;

	private Visible.LockTimerTypes logic_uScript_LockBlock_functionalityToLock_222 = Visible.LockTimerTypes.ItemCollection;

	private bool logic_uScript_LockBlock_Out_222 = true;

	private uScript_LockBlock logic_uScript_LockBlock_uScript_LockBlock_223 = new uScript_LockBlock();

	private TankBlock logic_uScript_LockBlock_block_223;

	private Visible.LockTimerTypes logic_uScript_LockBlock_functionalityToLock_223 = Visible.LockTimerTypes.StackAccept;

	private bool logic_uScript_LockBlock_Out_223 = true;

	private uScript_LockBlock logic_uScript_LockBlock_uScript_LockBlock_225 = new uScript_LockBlock();

	private TankBlock logic_uScript_LockBlock_block_225;

	private Visible.LockTimerTypes logic_uScript_LockBlock_functionalityToLock_225 = Visible.LockTimerTypes.SendToSCU;

	private bool logic_uScript_LockBlock_Out_225 = true;

	private uScript_LockBlock logic_uScript_LockBlock_uScript_LockBlock_226 = new uScript_LockBlock();

	private TankBlock logic_uScript_LockBlock_block_226;

	private Visible.LockTimerTypes logic_uScript_LockBlock_functionalityToLock_226 = Visible.LockTimerTypes.Grabbable;

	private bool logic_uScript_LockBlock_Out_226 = true;

	private uScript_LockBlock logic_uScript_LockBlock_uScript_LockBlock_228 = new uScript_LockBlock();

	private TankBlock logic_uScript_LockBlock_block_228;

	private Visible.LockTimerTypes logic_uScript_LockBlock_functionalityToLock_228 = Visible.LockTimerTypes.ItemCollection;

	private bool logic_uScript_LockBlock_Out_228 = true;

	private uScript_LockBlock logic_uScript_LockBlock_uScript_LockBlock_229 = new uScript_LockBlock();

	private TankBlock logic_uScript_LockBlock_block_229;

	private Visible.LockTimerTypes logic_uScript_LockBlock_functionalityToLock_229 = Visible.LockTimerTypes.Grabbable;

	private bool logic_uScript_LockBlock_Out_229 = true;

	private uScript_GetNamedBlock logic_uScript_GetNamedBlock_uScript_GetNamedBlock_230 = new uScript_GetNamedBlock();

	private string logic_uScript_GetNamedBlock_name_230 = "";

	private GameObject logic_uScript_GetNamedBlock_owner_230;

	private TankBlock logic_uScript_GetNamedBlock_Return_230;

	private bool logic_uScript_GetNamedBlock_Out_230 = true;

	private bool logic_uScript_GetNamedBlock_Destroyed_230 = true;

	private bool logic_uScript_GetNamedBlock_BlockExists_230 = true;

	private bool logic_uScript_GetNamedBlock_WaitingForBlock_230 = true;

	private bool logic_uScript_GetNamedBlock_NoBlock_230 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_231 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_231;

	private bool logic_uScriptCon_CompareBool_True_231 = true;

	private bool logic_uScriptCon_CompareBool_False_231 = true;

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

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_237 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_237;

	private bool logic_uScriptAct_SetBool_Out_237 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_237 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_237 = true;

	private uScriptCon_ManualSwitch logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_239 = new uScriptCon_ManualSwitch();

	private int logic_uScriptCon_ManualSwitch_CurrentOutput_239;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_241 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_241;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_241;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_241;

	private bool logic_uScript_AddMessage_Out_241 = true;

	private bool logic_uScript_AddMessage_Shown_241 = true;

	private uScriptAct_AddInt_v2 logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_245 = new uScriptAct_AddInt_v2();

	private int logic_uScriptAct_AddInt_v2_A_245;

	private int logic_uScriptAct_AddInt_v2_B_245 = 1;

	private int logic_uScriptAct_AddInt_v2_IntResult_245;

	private float logic_uScriptAct_AddInt_v2_FloatResult_245;

	private bool logic_uScriptAct_AddInt_v2_Out_245 = true;

	private SubGraph_SaveLoadInt logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_246 = new SubGraph_SaveLoadInt();

	private int logic_SubGraph_SaveLoadInt_restartValue_246 = 1;

	private int logic_SubGraph_SaveLoadInt_integer_246;

	private object logic_SubGraph_SaveLoadInt_intAsVariable_246 = "";

	private string logic_SubGraph_SaveLoadInt_uniqueID_246 = "Stage";

	private uScript_GetScrapperBlockBeingScrapped logic_uScript_GetScrapperBlockBeingScrapped_uScript_GetScrapperBlockBeingScrapped_248 = new uScript_GetScrapperBlockBeingScrapped();

	private TankBlock logic_uScript_GetScrapperBlockBeingScrapped_craftingBlock_248;

	private FactionSubTypes logic_uScript_GetScrapperBlockBeingScrapped_blockFaction_248;

	private BlockTypes logic_uScript_GetScrapperBlockBeingScrapped_Return_248;

	private bool logic_uScript_GetScrapperBlockBeingScrapped_Out_248 = true;

	private bool logic_uScript_GetScrapperBlockBeingScrapped_IsOperating_248 = true;

	private uScript_CompareFactionSubTypes logic_uScript_CompareFactionSubTypes_uScript_CompareFactionSubTypes_251 = new uScript_CompareFactionSubTypes();

	private FactionSubTypes logic_uScript_CompareFactionSubTypes_A_251;

	private FactionSubTypes logic_uScript_CompareFactionSubTypes_B_251 = FactionSubTypes.SJ;

	private bool logic_uScript_CompareFactionSubTypes_EqualTo_251 = true;

	private bool logic_uScript_CompareFactionSubTypes_NotEqualTo_251 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_252 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_252;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_252 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_252 = "ScrappedOtherCorpBlockEarly";

	private uScript_HideArrow logic_uScript_HideArrow_uScript_HideArrow_254 = new uScript_HideArrow();

	private bool logic_uScript_HideArrow_Out_254 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_256 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_256 = "";

	private bool logic_uScript_EnableGlow_enable_256;

	private bool logic_uScript_EnableGlow_Out_256 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_257 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_257 = 5;

	private int logic_uScriptAct_SetInt_Target_257;

	private bool logic_uScriptAct_SetInt_Out_257 = true;

	private SubGraph_Crafting_Tutorial_Finish logic_SubGraph_Crafting_Tutorial_Finish_SubGraph_Crafting_Tutorial_Finish_264 = new SubGraph_Crafting_Tutorial_Finish();

	private Tank logic_SubGraph_Crafting_Tutorial_Finish_craftingBaseTech_264;

	private Tank logic_SubGraph_Crafting_Tutorial_Finish_NPCTech_264;

	private ExternalBehaviorTree logic_SubGraph_Crafting_Tutorial_Finish_NPCFlyAwayAI_264;

	private Transform logic_SubGraph_Crafting_Tutorial_Finish_NPCDespawnParticleEffect_264;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_265 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_265;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_265;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_265;

	private bool logic_uScript_AddMessage_Out_265 = true;

	private bool logic_uScript_AddMessage_Shown_265 = true;

	private uScript_GetScrapperBlockBeingScrapped logic_uScript_GetScrapperBlockBeingScrapped_uScript_GetScrapperBlockBeingScrapped_270 = new uScript_GetScrapperBlockBeingScrapped();

	private TankBlock logic_uScript_GetScrapperBlockBeingScrapped_craftingBlock_270;

	private FactionSubTypes logic_uScript_GetScrapperBlockBeingScrapped_blockFaction_270;

	private BlockTypes logic_uScript_GetScrapperBlockBeingScrapped_Return_270;

	private bool logic_uScript_GetScrapperBlockBeingScrapped_Out_270 = true;

	private bool logic_uScript_GetScrapperBlockBeingScrapped_IsOperating_270 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_271 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_271 = "";

	private bool logic_uScript_EnableGlow_enable_271;

	private bool logic_uScript_EnableGlow_Out_271 = true;

	private uScript_HideArrow logic_uScript_HideArrow_uScript_HideArrow_274 = new uScript_HideArrow();

	private bool logic_uScript_HideArrow_Out_274 = true;

	private uScript_CompareFactionSubTypes logic_uScript_CompareFactionSubTypes_uScript_CompareFactionSubTypes_276 = new uScript_CompareFactionSubTypes();

	private FactionSubTypes logic_uScript_CompareFactionSubTypes_A_276;

	private FactionSubTypes logic_uScript_CompareFactionSubTypes_B_276 = FactionSubTypes.SJ;

	private bool logic_uScript_CompareFactionSubTypes_EqualTo_276 = true;

	private bool logic_uScript_CompareFactionSubTypes_NotEqualTo_276 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_278 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_278;

	private bool logic_uScriptAct_SetBool_Out_278 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_278 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_278 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_280 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_280 = "";

	private bool logic_uScript_EnableGlow_enable_280;

	private bool logic_uScript_EnableGlow_Out_280 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_281 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_281 = 5;

	private int logic_uScriptAct_SetInt_Target_281;

	private bool logic_uScriptAct_SetInt_Out_281 = true;

	private uScript_HideArrow logic_uScript_HideArrow_uScript_HideArrow_283 = new uScript_HideArrow();

	private bool logic_uScript_HideArrow_Out_283 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_284 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_284;

	private bool logic_uScriptCon_CompareBool_True_284 = true;

	private bool logic_uScriptCon_CompareBool_False_284 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_285 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_285;

	private bool logic_uScriptAct_SetBool_Out_285 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_285 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_285 = true;

	private uScript_DirectEnemiesOutOfEncounter logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_630 = new uScript_DirectEnemiesOutOfEncounter();

	private GameObject logic_uScript_DirectEnemiesOutOfEncounter_ownerNode_630;

	private bool logic_uScript_DirectEnemiesOutOfEncounter_Out_630 = true;

	private void SyncUnityHooks()
	{
		SyncEventListeners();
		if (null == owner_Connection_27 || !m_RegisteredForEvents)
		{
			owner_Connection_27 = parentGameObject;
			if (null != owner_Connection_27)
			{
				uScript_SaveLoad uScript_SaveLoad2 = owner_Connection_27.GetComponent<uScript_SaveLoad>();
				if (null == uScript_SaveLoad2)
				{
					uScript_SaveLoad2 = owner_Connection_27.AddComponent<uScript_SaveLoad>();
				}
				if (null != uScript_SaveLoad2)
				{
					uScript_SaveLoad2.SaveEvent += Instance_SaveEvent_62;
					uScript_SaveLoad2.LoadEvent += Instance_LoadEvent_62;
					uScript_SaveLoad2.RestartEvent += Instance_RestartEvent_62;
				}
			}
		}
		if (null == owner_Connection_52 || !m_RegisteredForEvents)
		{
			owner_Connection_52 = parentGameObject;
		}
		if (null == owner_Connection_103 || !m_RegisteredForEvents)
		{
			owner_Connection_103 = parentGameObject;
		}
		if (null == owner_Connection_108 || !m_RegisteredForEvents)
		{
			owner_Connection_108 = parentGameObject;
			if (null != owner_Connection_108)
			{
				uScript_EncounterUpdate uScript_EncounterUpdate2 = owner_Connection_108.GetComponent<uScript_EncounterUpdate>();
				if (null == uScript_EncounterUpdate2)
				{
					uScript_EncounterUpdate2 = owner_Connection_108.AddComponent<uScript_EncounterUpdate>();
				}
				if (null != uScript_EncounterUpdate2)
				{
					uScript_EncounterUpdate2.OnUpdate += Instance_OnUpdate_140;
					uScript_EncounterUpdate2.OnSuspend += Instance_OnSuspend_140;
					uScript_EncounterUpdate2.OnResume += Instance_OnResume_140;
				}
			}
		}
		if (null == owner_Connection_129 || !m_RegisteredForEvents)
		{
			owner_Connection_129 = parentGameObject;
		}
		if (null == owner_Connection_218 || !m_RegisteredForEvents)
		{
			owner_Connection_218 = parentGameObject;
		}
		if (null == owner_Connection_220 || !m_RegisteredForEvents)
		{
			owner_Connection_220 = parentGameObject;
		}
		if (null == owner_Connection_631 || !m_RegisteredForEvents)
		{
			owner_Connection_631 = parentGameObject;
		}
	}

	private void RegisterForUnityHooks()
	{
		SyncEventListeners();
		if (!m_RegisteredForEvents && null != owner_Connection_27)
		{
			uScript_SaveLoad uScript_SaveLoad2 = owner_Connection_27.GetComponent<uScript_SaveLoad>();
			if (null == uScript_SaveLoad2)
			{
				uScript_SaveLoad2 = owner_Connection_27.AddComponent<uScript_SaveLoad>();
			}
			if (null != uScript_SaveLoad2)
			{
				uScript_SaveLoad2.SaveEvent += Instance_SaveEvent_62;
				uScript_SaveLoad2.LoadEvent += Instance_LoadEvent_62;
				uScript_SaveLoad2.RestartEvent += Instance_RestartEvent_62;
			}
		}
		if (!m_RegisteredForEvents && null != owner_Connection_108)
		{
			uScript_EncounterUpdate uScript_EncounterUpdate2 = owner_Connection_108.GetComponent<uScript_EncounterUpdate>();
			if (null == uScript_EncounterUpdate2)
			{
				uScript_EncounterUpdate2 = owner_Connection_108.AddComponent<uScript_EncounterUpdate>();
			}
			if (null != uScript_EncounterUpdate2)
			{
				uScript_EncounterUpdate2.OnUpdate += Instance_OnUpdate_140;
				uScript_EncounterUpdate2.OnSuspend += Instance_OnSuspend_140;
				uScript_EncounterUpdate2.OnResume += Instance_OnResume_140;
			}
		}
	}

	private void SyncEventListeners()
	{
	}

	private void UnregisterEventListeners()
	{
		if (null != owner_Connection_27)
		{
			uScript_SaveLoad component = owner_Connection_27.GetComponent<uScript_SaveLoad>();
			if (null != component)
			{
				component.SaveEvent -= Instance_SaveEvent_62;
				component.LoadEvent -= Instance_LoadEvent_62;
				component.RestartEvent -= Instance_RestartEvent_62;
			}
		}
		if (null != owner_Connection_108)
		{
			uScript_EncounterUpdate component2 = owner_Connection_108.GetComponent<uScript_EncounterUpdate>();
			if (null != component2)
			{
				component2.OnUpdate -= Instance_OnUpdate_140;
				component2.OnSuspend -= Instance_OnSuspend_140;
				component2.OnResume -= Instance_OnResume_140;
			}
		}
	}

	public override void SetParent(GameObject g)
	{
		parentGameObject = g;
		logic_uScriptAct_SetBool_uScriptAct_SetBool_0.SetParent(g);
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_4.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_6.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_7.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_9.SetParent(g);
		logic_uScript_GetAndCheckBlocks_uScript_GetAndCheckBlocks_11.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_13.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_14.SetParent(g);
		logic_uScript_HideArrow_uScript_HideArrow_16.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_17.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_18.SetParent(g);
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_24.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_30.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_32.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_33.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_34.SetParent(g);
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_38.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_39.SetParent(g);
		logic_uScript_HideArrow_uScript_HideArrow_42.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_43.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_44.SetParent(g);
		logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_50.SetParent(g);
		logic_uScript_PausePopulation_uScript_PausePopulation_51.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_53.SetParent(g);
		logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_SubGraph_Crafting_Tutorial_AttachBlockToBase_57.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_58.SetParent(g);
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_60.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_71.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_75.SetParent(g);
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_76.SetParent(g);
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_78.SetParent(g);
		logic_uScript_SpawnBlocksFromData_uScript_SpawnBlocksFromData_80.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_81.SetParent(g);
		logic_uScript_PausePopulation_uScript_PausePopulation_82.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_83.SetParent(g);
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_84.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_86.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_87.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_88.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_90.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_95.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_96.SetParent(g);
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_97.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_104.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_105.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_107.SetParent(g);
		logic_uScript_LockTechInteraction_uScript_LockTechInteraction_112.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_117.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_118.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_119.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_121.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_122.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_124.SetParent(g);
		logic_uScript_LockTechStacks_uScript_LockTechStacks_125.SetParent(g);
		logic_SubGraph_Crafting_Tutorial_Init_SubGraph_Crafting_Tutorial_Init_126.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_132.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_136.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_141.SetParent(g);
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_144.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_145.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_147.SetParent(g);
		logic_uScript_IsCraftingBlockProducingItem_uScript_IsCraftingBlockProducingItem_148.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_149.SetParent(g);
		logic_uScript_IsCraftingBlockProducingItem_uScript_IsCraftingBlockProducingItem_156.SetParent(g);
		logic_SubGraph_Crafting_Tutorial_Finish_SubGraph_Crafting_Tutorial_Finish_157.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_158.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_159.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_162.SetParent(g);
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_165.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_166.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_168.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_170.SetParent(g);
		logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_173.SetParent(g);
		logic_uScript_IsCraftingBlockInOperation_uScript_IsCraftingBlockInOperation_176.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_182.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_183.SetParent(g);
		logic_uScript_HideArrow_uScript_HideArrow_184.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_186.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_188.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_189.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_190.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_191.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_197.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_198.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_200.SetParent(g);
		logic_uScriptAct_Concatenate_uScriptAct_Concatenate_201.SetParent(g);
		logic_uScriptAct_PrintText_uScriptAct_PrintText_205.SetParent(g);
		logic_uScript_LockBlock_uScript_LockBlock_206.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_208.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_209.SetParent(g);
		logic_uScript_GetNamedBlock_uScript_GetNamedBlock_211.SetParent(g);
		logic_uScript_LockBlock_uScript_LockBlock_213.SetParent(g);
		logic_uScript_LockBlock_uScript_LockBlock_214.SetParent(g);
		logic_uScript_LockBlock_uScript_LockBlock_216.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_217.SetParent(g);
		logic_uScript_LockBlock_uScript_LockBlock_219.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_221.SetParent(g);
		logic_uScript_LockBlock_uScript_LockBlock_222.SetParent(g);
		logic_uScript_LockBlock_uScript_LockBlock_223.SetParent(g);
		logic_uScript_LockBlock_uScript_LockBlock_225.SetParent(g);
		logic_uScript_LockBlock_uScript_LockBlock_226.SetParent(g);
		logic_uScript_LockBlock_uScript_LockBlock_228.SetParent(g);
		logic_uScript_LockBlock_uScript_LockBlock_229.SetParent(g);
		logic_uScript_GetNamedBlock_uScript_GetNamedBlock_230.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_231.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_234.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_235.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_237.SetParent(g);
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_239.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_241.SetParent(g);
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_245.SetParent(g);
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_246.SetParent(g);
		logic_uScript_GetScrapperBlockBeingScrapped_uScript_GetScrapperBlockBeingScrapped_248.SetParent(g);
		logic_uScript_CompareFactionSubTypes_uScript_CompareFactionSubTypes_251.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_252.SetParent(g);
		logic_uScript_HideArrow_uScript_HideArrow_254.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_256.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_257.SetParent(g);
		logic_SubGraph_Crafting_Tutorial_Finish_SubGraph_Crafting_Tutorial_Finish_264.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_265.SetParent(g);
		logic_uScript_GetScrapperBlockBeingScrapped_uScript_GetScrapperBlockBeingScrapped_270.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_271.SetParent(g);
		logic_uScript_HideArrow_uScript_HideArrow_274.SetParent(g);
		logic_uScript_CompareFactionSubTypes_uScript_CompareFactionSubTypes_276.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_278.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_280.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_281.SetParent(g);
		logic_uScript_HideArrow_uScript_HideArrow_283.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_284.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_285.SetParent(g);
		logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_630.SetParent(g);
		owner_Connection_27 = parentGameObject;
		owner_Connection_52 = parentGameObject;
		owner_Connection_103 = parentGameObject;
		owner_Connection_108 = parentGameObject;
		owner_Connection_129 = parentGameObject;
		owner_Connection_218 = parentGameObject;
		owner_Connection_220 = parentGameObject;
		owner_Connection_631 = parentGameObject;
	}

	public void Awake()
	{
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_13.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_17.Awake();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_24.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_30.Awake();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_38.Awake();
		logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_SubGraph_Crafting_Tutorial_AttachBlockToBase_57.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_71.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_75.Awake();
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_76.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_88.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_95.Awake();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_97.Awake();
		logic_SubGraph_Crafting_Tutorial_Init_SubGraph_Crafting_Tutorial_Init_126.Awake();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_144.Awake();
		logic_SubGraph_Crafting_Tutorial_Finish_SubGraph_Crafting_Tutorial_Finish_157.Awake();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_165.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_166.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_170.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_197.Awake();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_246.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_252.Awake();
		logic_SubGraph_Crafting_Tutorial_Finish_SubGraph_Crafting_Tutorial_Finish_264.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_13.Save_Out += SubGraph_SaveLoadBool_Save_Out_13;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_13.Load_Out += SubGraph_SaveLoadBool_Load_Out_13;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_13.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_13;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_17.Save_Out += SubGraph_SaveLoadBool_Save_Out_17;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_17.Load_Out += SubGraph_SaveLoadBool_Load_Out_17;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_17.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_17;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_24.Out += SubGraph_CompleteObjectiveStage_Out_24;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_30.Save_Out += SubGraph_SaveLoadBool_Save_Out_30;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_30.Load_Out += SubGraph_SaveLoadBool_Load_Out_30;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_30.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_30;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_38.Save_Out += SubGraph_SaveLoadInt_Save_Out_38;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_38.Load_Out += SubGraph_SaveLoadInt_Load_Out_38;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_38.Restart_Out += SubGraph_SaveLoadInt_Restart_Out_38;
		logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_SubGraph_Crafting_Tutorial_AttachBlockToBase_57.Block_Attached += SubGraph_Crafting_Tutorial_AttachBlockToBase_Block_Attached_57;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_60.Output1 += uScriptCon_ManualSwitch_Output1_60;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_60.Output2 += uScriptCon_ManualSwitch_Output2_60;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_60.Output3 += uScriptCon_ManualSwitch_Output3_60;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_60.Output4 += uScriptCon_ManualSwitch_Output4_60;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_60.Output5 += uScriptCon_ManualSwitch_Output5_60;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_60.Output6 += uScriptCon_ManualSwitch_Output6_60;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_60.Output7 += uScriptCon_ManualSwitch_Output7_60;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_60.Output8 += uScriptCon_ManualSwitch_Output8_60;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_71.Save_Out += SubGraph_SaveLoadBool_Save_Out_71;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_71.Load_Out += SubGraph_SaveLoadBool_Load_Out_71;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_71.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_71;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_75.Save_Out += SubGraph_SaveLoadBool_Save_Out_75;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_75.Load_Out += SubGraph_SaveLoadBool_Load_Out_75;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_75.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_75;
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_76.Out += SubGraph_Crafting_Tutorial_ManageBlock_Out_76;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_88.Save_Out += SubGraph_SaveLoadBool_Save_Out_88;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_88.Load_Out += SubGraph_SaveLoadBool_Load_Out_88;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_88.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_88;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_95.Save_Out += SubGraph_SaveLoadBool_Save_Out_95;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_95.Load_Out += SubGraph_SaveLoadBool_Load_Out_95;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_95.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_95;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_97.Out += SubGraph_CompleteObjectiveStage_Out_97;
		logic_SubGraph_Crafting_Tutorial_Init_SubGraph_Crafting_Tutorial_Init_126.Out += SubGraph_Crafting_Tutorial_Init_Out_126;
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_144.Out += SubGraph_LoadObjectiveStates_Out_144;
		logic_SubGraph_Crafting_Tutorial_Finish_SubGraph_Crafting_Tutorial_Finish_157.Out += SubGraph_Crafting_Tutorial_Finish_Out_157;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_165.Out += SubGraph_CompleteObjectiveStage_Out_165;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_166.Save_Out += SubGraph_SaveLoadBool_Save_Out_166;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_166.Load_Out += SubGraph_SaveLoadBool_Load_Out_166;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_166.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_166;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_170.Save_Out += SubGraph_SaveLoadBool_Save_Out_170;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_170.Load_Out += SubGraph_SaveLoadBool_Load_Out_170;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_170.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_170;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_197.Save_Out += SubGraph_SaveLoadBool_Save_Out_197;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_197.Load_Out += SubGraph_SaveLoadBool_Load_Out_197;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_197.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_197;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_239.Output1 += uScriptCon_ManualSwitch_Output1_239;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_239.Output2 += uScriptCon_ManualSwitch_Output2_239;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_239.Output3 += uScriptCon_ManualSwitch_Output3_239;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_239.Output4 += uScriptCon_ManualSwitch_Output4_239;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_239.Output5 += uScriptCon_ManualSwitch_Output5_239;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_239.Output6 += uScriptCon_ManualSwitch_Output6_239;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_239.Output7 += uScriptCon_ManualSwitch_Output7_239;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_239.Output8 += uScriptCon_ManualSwitch_Output8_239;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_246.Save_Out += SubGraph_SaveLoadInt_Save_Out_246;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_246.Load_Out += SubGraph_SaveLoadInt_Load_Out_246;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_246.Restart_Out += SubGraph_SaveLoadInt_Restart_Out_246;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_252.Save_Out += SubGraph_SaveLoadBool_Save_Out_252;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_252.Load_Out += SubGraph_SaveLoadBool_Load_Out_252;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_252.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_252;
		logic_SubGraph_Crafting_Tutorial_Finish_SubGraph_Crafting_Tutorial_Finish_264.Out += SubGraph_Crafting_Tutorial_Finish_Out_264;
	}

	public void Start()
	{
		SyncUnityHooks();
		m_RegisteredForEvents = true;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_13.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_17.Start();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_24.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_30.Start();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_38.Start();
		logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_SubGraph_Crafting_Tutorial_AttachBlockToBase_57.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_71.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_75.Start();
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_76.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_88.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_95.Start();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_97.Start();
		logic_SubGraph_Crafting_Tutorial_Init_SubGraph_Crafting_Tutorial_Init_126.Start();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_144.Start();
		logic_SubGraph_Crafting_Tutorial_Finish_SubGraph_Crafting_Tutorial_Finish_157.Start();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_165.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_166.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_170.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_197.Start();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_246.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_252.Start();
		logic_SubGraph_Crafting_Tutorial_Finish_SubGraph_Crafting_Tutorial_Finish_264.Start();
	}

	public void OnEnable()
	{
		RegisterForUnityHooks();
		m_RegisteredForEvents = true;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_13.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_17.OnEnable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_24.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_30.OnEnable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_38.OnEnable();
		logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_SubGraph_Crafting_Tutorial_AttachBlockToBase_57.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_71.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_75.OnEnable();
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_76.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_88.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_95.OnEnable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_97.OnEnable();
		logic_SubGraph_Crafting_Tutorial_Init_SubGraph_Crafting_Tutorial_Init_126.OnEnable();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_144.OnEnable();
		logic_SubGraph_Crafting_Tutorial_Finish_SubGraph_Crafting_Tutorial_Finish_157.OnEnable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_165.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_166.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_170.OnEnable();
		logic_uScript_IsCraftingBlockInOperation_uScript_IsCraftingBlockInOperation_176.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_197.OnEnable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_246.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_252.OnEnable();
		logic_SubGraph_Crafting_Tutorial_Finish_SubGraph_Crafting_Tutorial_Finish_264.OnEnable();
		logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_630.OnEnable();
	}

	public void OnDisable()
	{
		logic_uScript_AddMessage_uScript_AddMessage_6.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_13.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_14.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_17.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_18.OnDisable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_24.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_30.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_34.OnDisable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_38.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_43.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_44.OnDisable();
		logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_SubGraph_Crafting_Tutorial_AttachBlockToBase_57.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_71.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_75.OnDisable();
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_76.OnDisable();
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_78.OnDisable();
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_84.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_88.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_95.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_96.OnDisable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_97.OnDisable();
		logic_SubGraph_Crafting_Tutorial_Init_SubGraph_Crafting_Tutorial_Init_126.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_141.OnDisable();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_144.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_147.OnDisable();
		logic_SubGraph_Crafting_Tutorial_Finish_SubGraph_Crafting_Tutorial_Finish_157.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_162.OnDisable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_165.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_166.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_170.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_186.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_190.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_197.OnDisable();
		logic_uScript_GetNamedBlock_uScript_GetNamedBlock_211.OnDisable();
		logic_uScript_GetNamedBlock_uScript_GetNamedBlock_230.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_241.OnDisable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_246.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_252.OnDisable();
		logic_SubGraph_Crafting_Tutorial_Finish_SubGraph_Crafting_Tutorial_Finish_264.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_265.OnDisable();
		UnregisterEventListeners();
		m_RegisteredForEvents = false;
	}

	public void Update()
	{
		SyncEventListeners();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_13.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_17.Update();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_24.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_30.Update();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_38.Update();
		logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_SubGraph_Crafting_Tutorial_AttachBlockToBase_57.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_71.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_75.Update();
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_76.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_88.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_95.Update();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_97.Update();
		logic_SubGraph_Crafting_Tutorial_Init_SubGraph_Crafting_Tutorial_Init_126.Update();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_144.Update();
		logic_SubGraph_Crafting_Tutorial_Finish_SubGraph_Crafting_Tutorial_Finish_157.Update();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_165.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_166.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_170.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_197.Update();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_246.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_252.Update();
		logic_SubGraph_Crafting_Tutorial_Finish_SubGraph_Crafting_Tutorial_Finish_264.Update();
	}

	public void OnDestroy()
	{
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_13.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_17.OnDestroy();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_24.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_30.OnDestroy();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_38.OnDestroy();
		logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_SubGraph_Crafting_Tutorial_AttachBlockToBase_57.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_71.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_75.OnDestroy();
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_76.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_88.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_95.OnDestroy();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_97.OnDestroy();
		logic_SubGraph_Crafting_Tutorial_Init_SubGraph_Crafting_Tutorial_Init_126.OnDestroy();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_144.OnDestroy();
		logic_SubGraph_Crafting_Tutorial_Finish_SubGraph_Crafting_Tutorial_Finish_157.OnDestroy();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_165.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_166.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_170.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_197.OnDestroy();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_246.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_252.OnDestroy();
		logic_SubGraph_Crafting_Tutorial_Finish_SubGraph_Crafting_Tutorial_Finish_264.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_13.Save_Out -= SubGraph_SaveLoadBool_Save_Out_13;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_13.Load_Out -= SubGraph_SaveLoadBool_Load_Out_13;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_13.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_13;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_17.Save_Out -= SubGraph_SaveLoadBool_Save_Out_17;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_17.Load_Out -= SubGraph_SaveLoadBool_Load_Out_17;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_17.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_17;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_24.Out -= SubGraph_CompleteObjectiveStage_Out_24;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_30.Save_Out -= SubGraph_SaveLoadBool_Save_Out_30;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_30.Load_Out -= SubGraph_SaveLoadBool_Load_Out_30;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_30.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_30;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_38.Save_Out -= SubGraph_SaveLoadInt_Save_Out_38;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_38.Load_Out -= SubGraph_SaveLoadInt_Load_Out_38;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_38.Restart_Out -= SubGraph_SaveLoadInt_Restart_Out_38;
		logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_SubGraph_Crafting_Tutorial_AttachBlockToBase_57.Block_Attached -= SubGraph_Crafting_Tutorial_AttachBlockToBase_Block_Attached_57;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_60.Output1 -= uScriptCon_ManualSwitch_Output1_60;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_60.Output2 -= uScriptCon_ManualSwitch_Output2_60;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_60.Output3 -= uScriptCon_ManualSwitch_Output3_60;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_60.Output4 -= uScriptCon_ManualSwitch_Output4_60;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_60.Output5 -= uScriptCon_ManualSwitch_Output5_60;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_60.Output6 -= uScriptCon_ManualSwitch_Output6_60;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_60.Output7 -= uScriptCon_ManualSwitch_Output7_60;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_60.Output8 -= uScriptCon_ManualSwitch_Output8_60;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_71.Save_Out -= SubGraph_SaveLoadBool_Save_Out_71;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_71.Load_Out -= SubGraph_SaveLoadBool_Load_Out_71;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_71.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_71;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_75.Save_Out -= SubGraph_SaveLoadBool_Save_Out_75;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_75.Load_Out -= SubGraph_SaveLoadBool_Load_Out_75;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_75.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_75;
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_76.Out -= SubGraph_Crafting_Tutorial_ManageBlock_Out_76;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_88.Save_Out -= SubGraph_SaveLoadBool_Save_Out_88;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_88.Load_Out -= SubGraph_SaveLoadBool_Load_Out_88;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_88.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_88;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_95.Save_Out -= SubGraph_SaveLoadBool_Save_Out_95;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_95.Load_Out -= SubGraph_SaveLoadBool_Load_Out_95;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_95.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_95;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_97.Out -= SubGraph_CompleteObjectiveStage_Out_97;
		logic_SubGraph_Crafting_Tutorial_Init_SubGraph_Crafting_Tutorial_Init_126.Out -= SubGraph_Crafting_Tutorial_Init_Out_126;
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_144.Out -= SubGraph_LoadObjectiveStates_Out_144;
		logic_SubGraph_Crafting_Tutorial_Finish_SubGraph_Crafting_Tutorial_Finish_157.Out -= SubGraph_Crafting_Tutorial_Finish_Out_157;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_165.Out -= SubGraph_CompleteObjectiveStage_Out_165;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_166.Save_Out -= SubGraph_SaveLoadBool_Save_Out_166;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_166.Load_Out -= SubGraph_SaveLoadBool_Load_Out_166;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_166.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_166;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_170.Save_Out -= SubGraph_SaveLoadBool_Save_Out_170;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_170.Load_Out -= SubGraph_SaveLoadBool_Load_Out_170;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_170.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_170;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_197.Save_Out -= SubGraph_SaveLoadBool_Save_Out_197;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_197.Load_Out -= SubGraph_SaveLoadBool_Load_Out_197;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_197.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_197;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_239.Output1 -= uScriptCon_ManualSwitch_Output1_239;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_239.Output2 -= uScriptCon_ManualSwitch_Output2_239;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_239.Output3 -= uScriptCon_ManualSwitch_Output3_239;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_239.Output4 -= uScriptCon_ManualSwitch_Output4_239;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_239.Output5 -= uScriptCon_ManualSwitch_Output5_239;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_239.Output6 -= uScriptCon_ManualSwitch_Output6_239;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_239.Output7 -= uScriptCon_ManualSwitch_Output7_239;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_239.Output8 -= uScriptCon_ManualSwitch_Output8_239;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_246.Save_Out -= SubGraph_SaveLoadInt_Save_Out_246;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_246.Load_Out -= SubGraph_SaveLoadInt_Load_Out_246;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_246.Restart_Out -= SubGraph_SaveLoadInt_Restart_Out_246;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_252.Save_Out -= SubGraph_SaveLoadBool_Save_Out_252;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_252.Load_Out -= SubGraph_SaveLoadBool_Load_Out_252;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_252.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_252;
		logic_SubGraph_Crafting_Tutorial_Finish_SubGraph_Crafting_Tutorial_Finish_264.Out -= SubGraph_Crafting_Tutorial_Finish_Out_264;
	}

	public void OnGUI()
	{
		logic_uScriptAct_PrintText_uScriptAct_PrintText_205.OnGUI();
	}

	private void Instance_SaveEvent_62(object o, EventArgs e)
	{
		Relay_SaveEvent_62();
	}

	private void Instance_LoadEvent_62(object o, EventArgs e)
	{
		Relay_LoadEvent_62();
	}

	private void Instance_RestartEvent_62(object o, EventArgs e)
	{
		Relay_RestartEvent_62();
	}

	private void Instance_OnUpdate_140(object o, EventArgs e)
	{
		Relay_OnUpdate_140();
	}

	private void Instance_OnSuspend_140(object o, EventArgs e)
	{
		Relay_OnSuspend_140();
	}

	private void Instance_OnResume_140(object o, EventArgs e)
	{
		Relay_OnResume_140();
	}

	private void SubGraph_SaveLoadBool_Save_Out_13(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_13 = e.boolean;
		local_msgIntroShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_13;
		Relay_Save_Out_13();
	}

	private void SubGraph_SaveLoadBool_Load_Out_13(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_13 = e.boolean;
		local_msgIntroShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_13;
		Relay_Load_Out_13();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_13(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_13 = e.boolean;
		local_msgIntroShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_13;
		Relay_Restart_Out_13();
	}

	private void SubGraph_SaveLoadBool_Save_Out_17(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_17 = e.boolean;
		local_SJBlockInScrapper_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_17;
		Relay_Save_Out_17();
	}

	private void SubGraph_SaveLoadBool_Load_Out_17(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_17 = e.boolean;
		local_SJBlockInScrapper_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_17;
		Relay_Load_Out_17();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_17(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_17 = e.boolean;
		local_SJBlockInScrapper_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_17;
		Relay_Restart_Out_17();
	}

	private void SubGraph_CompleteObjectiveStage_Out_24(object o, SubGraph_CompleteObjectiveStage.LogicEventArgs e)
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_24 = e.objectiveStage;
		local_Stage_System_Int32 = logic_SubGraph_CompleteObjectiveStage_objectiveStage_24;
		Relay_Out_24();
	}

	private void SubGraph_SaveLoadBool_Save_Out_30(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_30 = e.boolean;
		local_msgScrapperAttachedShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_30;
		Relay_Save_Out_30();
	}

	private void SubGraph_SaveLoadBool_Load_Out_30(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_30 = e.boolean;
		local_msgScrapperAttachedShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_30;
		Relay_Load_Out_30();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_30(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_30 = e.boolean;
		local_msgScrapperAttachedShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_30;
		Relay_Restart_Out_30();
	}

	private void SubGraph_SaveLoadInt_Save_Out_38(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_38 = e.integer;
		local_Stage_System_Int32 = logic_SubGraph_SaveLoadInt_integer_38;
		Relay_Save_Out_38();
	}

	private void SubGraph_SaveLoadInt_Load_Out_38(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_38 = e.integer;
		local_Stage_System_Int32 = logic_SubGraph_SaveLoadInt_integer_38;
		Relay_Load_Out_38();
	}

	private void SubGraph_SaveLoadInt_Restart_Out_38(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_38 = e.integer;
		local_Stage_System_Int32 = logic_SubGraph_SaveLoadInt_integer_38;
		Relay_Restart_Out_38();
	}

	private void SubGraph_Crafting_Tutorial_AttachBlockToBase_Block_Attached_57(object o, SubGraph_Crafting_Tutorial_AttachBlockToBase.LogicEventArgs e)
	{
		Relay_Block_Attached_57();
	}

	private void uScriptCon_ManualSwitch_Output1_60(object o, EventArgs e)
	{
		Relay_Output1_60();
	}

	private void uScriptCon_ManualSwitch_Output2_60(object o, EventArgs e)
	{
		Relay_Output2_60();
	}

	private void uScriptCon_ManualSwitch_Output3_60(object o, EventArgs e)
	{
		Relay_Output3_60();
	}

	private void uScriptCon_ManualSwitch_Output4_60(object o, EventArgs e)
	{
		Relay_Output4_60();
	}

	private void uScriptCon_ManualSwitch_Output5_60(object o, EventArgs e)
	{
		Relay_Output5_60();
	}

	private void uScriptCon_ManualSwitch_Output6_60(object o, EventArgs e)
	{
		Relay_Output6_60();
	}

	private void uScriptCon_ManualSwitch_Output7_60(object o, EventArgs e)
	{
		Relay_Output7_60();
	}

	private void uScriptCon_ManualSwitch_Output8_60(object o, EventArgs e)
	{
		Relay_Output8_60();
	}

	private void SubGraph_SaveLoadBool_Save_Out_71(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_71 = e.boolean;
		local_msgBaseFoundShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_71;
		Relay_Save_Out_71();
	}

	private void SubGraph_SaveLoadBool_Load_Out_71(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_71 = e.boolean;
		local_msgBaseFoundShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_71;
		Relay_Load_Out_71();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_71(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_71 = e.boolean;
		local_msgBaseFoundShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_71;
		Relay_Restart_Out_71();
	}

	private void SubGraph_SaveLoadBool_Save_Out_75(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_75 = e.boolean;
		local_ScrappingInProgress_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_75;
		Relay_Save_Out_75();
	}

	private void SubGraph_SaveLoadBool_Load_Out_75(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_75 = e.boolean;
		local_ScrappingInProgress_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_75;
		Relay_Load_Out_75();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_75(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_75 = e.boolean;
		local_ScrappingInProgress_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_75;
		Relay_Restart_Out_75();
	}

	private void SubGraph_Crafting_Tutorial_ManageBlock_Out_76(object o, SubGraph_Crafting_Tutorial_ManageBlock.LogicEventArgs e)
	{
		logic_SubGraph_Crafting_Tutorial_ManageBlock_block_76 = e.block;
		blockSpawnDataScrapper = logic_SubGraph_Crafting_Tutorial_ManageBlock_blockSpawnData_76;
		local_ScrapperBlock_TankBlock = logic_SubGraph_Crafting_Tutorial_ManageBlock_block_76;
		Relay_Out_76();
	}

	private void SubGraph_SaveLoadBool_Save_Out_88(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_88 = e.boolean;
		local_msgScrapperSpawnedShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_88;
		Relay_Save_Out_88();
	}

	private void SubGraph_SaveLoadBool_Load_Out_88(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_88 = e.boolean;
		local_msgScrapperSpawnedShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_88;
		Relay_Load_Out_88();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_88(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_88 = e.boolean;
		local_msgScrapperSpawnedShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_88;
		Relay_Restart_Out_88();
	}

	private void SubGraph_SaveLoadBool_Save_Out_95(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_95 = e.boolean;
		local_ScrapperSpawned_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_95;
		Relay_Save_Out_95();
	}

	private void SubGraph_SaveLoadBool_Load_Out_95(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_95 = e.boolean;
		local_ScrapperSpawned_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_95;
		Relay_Load_Out_95();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_95(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_95 = e.boolean;
		local_ScrapperSpawned_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_95;
		Relay_Restart_Out_95();
	}

	private void SubGraph_CompleteObjectiveStage_Out_97(object o, SubGraph_CompleteObjectiveStage.LogicEventArgs e)
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_97 = e.objectiveStage;
		local_Stage_System_Int32 = logic_SubGraph_CompleteObjectiveStage_objectiveStage_97;
		Relay_Out_97();
	}

	private void SubGraph_Crafting_Tutorial_Init_Out_126(object o, SubGraph_Crafting_Tutorial_Init.LogicEventArgs e)
	{
		logic_SubGraph_Crafting_Tutorial_Init_CraftingBaseTech_126 = e.CraftingBaseTech;
		logic_SubGraph_Crafting_Tutorial_Init_NPCTech_126 = e.NPCTech;
		local_CraftingBaseTech_Tank = logic_SubGraph_Crafting_Tutorial_Init_CraftingBaseTech_126;
		local_NPCTech_Tank = logic_SubGraph_Crafting_Tutorial_Init_NPCTech_126;
		Relay_Out_126();
	}

	private void SubGraph_LoadObjectiveStates_Out_144(object o, SubGraph_LoadObjectiveStates.LogicEventArgs e)
	{
		Relay_Out_144();
	}

	private void SubGraph_Crafting_Tutorial_Finish_Out_157(object o, SubGraph_Crafting_Tutorial_Finish.LogicEventArgs e)
	{
		Relay_Out_157();
	}

	private void SubGraph_CompleteObjectiveStage_Out_165(object o, SubGraph_CompleteObjectiveStage.LogicEventArgs e)
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_165 = e.objectiveStage;
		local_Stage_System_Int32 = logic_SubGraph_CompleteObjectiveStage_objectiveStage_165;
		Relay_Out_165();
	}

	private void SubGraph_SaveLoadBool_Save_Out_166(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_166 = e.boolean;
		local_OtherCorpBlockInScrapper_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_166;
		Relay_Save_Out_166();
	}

	private void SubGraph_SaveLoadBool_Load_Out_166(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_166 = e.boolean;
		local_OtherCorpBlockInScrapper_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_166;
		Relay_Load_Out_166();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_166(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_166 = e.boolean;
		local_OtherCorpBlockInScrapper_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_166;
		Relay_Restart_Out_166();
	}

	private void SubGraph_SaveLoadBool_Save_Out_170(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_170 = e.boolean;
		local_ScrappingOtherCorpBlockInProgress_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_170;
		Relay_Save_Out_170();
	}

	private void SubGraph_SaveLoadBool_Load_Out_170(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_170 = e.boolean;
		local_ScrappingOtherCorpBlockInProgress_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_170;
		Relay_Load_Out_170();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_170(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_170 = e.boolean;
		local_ScrappingOtherCorpBlockInProgress_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_170;
		Relay_Restart_Out_170();
	}

	private void SubGraph_SaveLoadBool_Save_Out_197(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_197 = e.boolean;
		local_msgBlockScrappedShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_197;
		Relay_Save_Out_197();
	}

	private void SubGraph_SaveLoadBool_Load_Out_197(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_197 = e.boolean;
		local_msgBlockScrappedShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_197;
		Relay_Load_Out_197();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_197(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_197 = e.boolean;
		local_msgBlockScrappedShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_197;
		Relay_Restart_Out_197();
	}

	private void uScriptCon_ManualSwitch_Output1_239(object o, EventArgs e)
	{
		Relay_Output1_239();
	}

	private void uScriptCon_ManualSwitch_Output2_239(object o, EventArgs e)
	{
		Relay_Output2_239();
	}

	private void uScriptCon_ManualSwitch_Output3_239(object o, EventArgs e)
	{
		Relay_Output3_239();
	}

	private void uScriptCon_ManualSwitch_Output4_239(object o, EventArgs e)
	{
		Relay_Output4_239();
	}

	private void uScriptCon_ManualSwitch_Output5_239(object o, EventArgs e)
	{
		Relay_Output5_239();
	}

	private void uScriptCon_ManualSwitch_Output6_239(object o, EventArgs e)
	{
		Relay_Output6_239();
	}

	private void uScriptCon_ManualSwitch_Output7_239(object o, EventArgs e)
	{
		Relay_Output7_239();
	}

	private void uScriptCon_ManualSwitch_Output8_239(object o, EventArgs e)
	{
		Relay_Output8_239();
	}

	private void SubGraph_SaveLoadInt_Save_Out_246(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_246 = e.integer;
		local_Stage2_System_Int32 = logic_SubGraph_SaveLoadInt_integer_246;
		Relay_Save_Out_246();
	}

	private void SubGraph_SaveLoadInt_Load_Out_246(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_246 = e.integer;
		local_Stage2_System_Int32 = logic_SubGraph_SaveLoadInt_integer_246;
		Relay_Load_Out_246();
	}

	private void SubGraph_SaveLoadInt_Restart_Out_246(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_246 = e.integer;
		local_Stage2_System_Int32 = logic_SubGraph_SaveLoadInt_integer_246;
		Relay_Restart_Out_246();
	}

	private void SubGraph_SaveLoadBool_Save_Out_252(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_252 = e.boolean;
		local_ScrappedOtherCorpBlockEarly_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_252;
		Relay_Save_Out_252();
	}

	private void SubGraph_SaveLoadBool_Load_Out_252(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_252 = e.boolean;
		local_ScrappedOtherCorpBlockEarly_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_252;
		Relay_Load_Out_252();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_252(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_252 = e.boolean;
		local_ScrappedOtherCorpBlockEarly_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_252;
		Relay_Restart_Out_252();
	}

	private void SubGraph_Crafting_Tutorial_Finish_Out_264(object o, SubGraph_Crafting_Tutorial_Finish.LogicEventArgs e)
	{
		Relay_Out_264();
	}

	private void Relay_True_0()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_0.True(out logic_uScriptAct_SetBool_Target_0);
		local_NearBase_System_Boolean = logic_uScriptAct_SetBool_Target_0;
	}

	private void Relay_False_0()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_0.False(out logic_uScriptAct_SetBool_Target_0);
		local_NearBase_System_Boolean = logic_uScriptAct_SetBool_Target_0;
	}

	private void Relay_In_4()
	{
		logic_uScript_SetEncounterTarget_owner_4 = owner_Connection_52;
		logic_uScript_SetEncounterTarget_visibleObject_4 = local_NPCTech_Tank;
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_4.In(logic_uScript_SetEncounterTarget_owner_4, logic_uScript_SetEncounterTarget_visibleObject_4);
		if (logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_4.Out)
		{
			Relay_In_78();
		}
	}

	private void Relay_In_6()
	{
		logic_uScript_AddMessage_messageData_6 = msgLeavingMissionArea;
		logic_uScript_AddMessage_speaker_6 = messageSpeaker;
		logic_uScript_AddMessage_Return_6 = logic_uScript_AddMessage_uScript_AddMessage_6.In(logic_uScript_AddMessage_messageData_6, logic_uScript_AddMessage_speaker_6);
		if (logic_uScript_AddMessage_uScript_AddMessage_6.Out)
		{
			Relay_False_0();
		}
	}

	private void Relay_In_7()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_7 = messageTag;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_7.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_7, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_7);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_7.Out)
		{
			Relay_In_42();
		}
	}

	private void Relay_In_9()
	{
		logic_uScriptCon_CompareBool_Bool_9 = local_msgScrapperSpawnedShown_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_9.In(logic_uScriptCon_CompareBool_Bool_9);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_9.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_9.False;
		if (num)
		{
			Relay_In_44();
		}
		if (flag)
		{
			Relay_In_18();
		}
	}

	private void Relay_In_11()
	{
		int num = 0;
		Array array = blockSpawnDataScrapper;
		if (logic_uScript_GetAndCheckBlocks_blockData_11.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckBlocks_blockData_11, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckBlocks_blockData_11, num, array.Length);
		num += array.Length;
		logic_uScript_GetAndCheckBlocks_ownerNode_11 = owner_Connection_129;
		logic_uScript_GetAndCheckBlocks_uScript_GetAndCheckBlocks_11.In(logic_uScript_GetAndCheckBlocks_blockData_11, logic_uScript_GetAndCheckBlocks_ownerNode_11, ref logic_uScript_GetAndCheckBlocks_blocks_11);
		bool allAlive = logic_uScript_GetAndCheckBlocks_uScript_GetAndCheckBlocks_11.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckBlocks_uScript_GetAndCheckBlocks_11.SomeAlive;
		if (allAlive)
		{
			Relay_In_9();
		}
		if (someAlive)
		{
			Relay_In_9();
		}
	}

	private void Relay_Save_Out_13()
	{
		Relay_Save_95();
	}

	private void Relay_Load_Out_13()
	{
		Relay_Load_95();
	}

	private void Relay_Restart_Out_13()
	{
		Relay_Set_False_95();
	}

	private void Relay_Save_13()
	{
		logic_SubGraph_SaveLoadBool_boolean_13 = local_msgIntroShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_13 = local_msgIntroShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_13.Save(ref logic_SubGraph_SaveLoadBool_boolean_13, logic_SubGraph_SaveLoadBool_boolAsVariable_13, logic_SubGraph_SaveLoadBool_uniqueID_13);
	}

	private void Relay_Load_13()
	{
		logic_SubGraph_SaveLoadBool_boolean_13 = local_msgIntroShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_13 = local_msgIntroShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_13.Load(ref logic_SubGraph_SaveLoadBool_boolean_13, logic_SubGraph_SaveLoadBool_boolAsVariable_13, logic_SubGraph_SaveLoadBool_uniqueID_13);
	}

	private void Relay_Set_True_13()
	{
		logic_SubGraph_SaveLoadBool_boolean_13 = local_msgIntroShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_13 = local_msgIntroShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_13.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_13, logic_SubGraph_SaveLoadBool_boolAsVariable_13, logic_SubGraph_SaveLoadBool_uniqueID_13);
	}

	private void Relay_Set_False_13()
	{
		logic_SubGraph_SaveLoadBool_boolean_13 = local_msgIntroShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_13 = local_msgIntroShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_13.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_13, logic_SubGraph_SaveLoadBool_boolAsVariable_13, logic_SubGraph_SaveLoadBool_uniqueID_13);
	}

	private void Relay_In_14()
	{
		logic_uScript_AddMessage_messageData_14 = msg07ScrappingBlock1_InProgress;
		logic_uScript_AddMessage_speaker_14 = messageSpeaker;
		logic_uScript_AddMessage_Return_14 = logic_uScript_AddMessage_uScript_AddMessage_14.In(logic_uScript_AddMessage_messageData_14, logic_uScript_AddMessage_speaker_14);
		if (logic_uScript_AddMessage_uScript_AddMessage_14.Shown)
		{
			Relay_In_148();
		}
	}

	private void Relay_In_16()
	{
		logic_uScript_HideArrow_uScript_HideArrow_16.In();
		if (logic_uScript_HideArrow_uScript_HideArrow_16.Out)
		{
			Relay_In_58();
		}
	}

	private void Relay_Save_Out_17()
	{
		Relay_Save_166();
	}

	private void Relay_Load_Out_17()
	{
		Relay_Load_166();
	}

	private void Relay_Restart_Out_17()
	{
		Relay_Set_False_166();
	}

	private void Relay_Save_17()
	{
		logic_SubGraph_SaveLoadBool_boolean_17 = local_SJBlockInScrapper_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_17 = local_SJBlockInScrapper_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_17.Save(ref logic_SubGraph_SaveLoadBool_boolean_17, logic_SubGraph_SaveLoadBool_boolAsVariable_17, logic_SubGraph_SaveLoadBool_uniqueID_17);
	}

	private void Relay_Load_17()
	{
		logic_SubGraph_SaveLoadBool_boolean_17 = local_SJBlockInScrapper_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_17 = local_SJBlockInScrapper_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_17.Load(ref logic_SubGraph_SaveLoadBool_boolean_17, logic_SubGraph_SaveLoadBool_boolAsVariable_17, logic_SubGraph_SaveLoadBool_uniqueID_17);
	}

	private void Relay_Set_True_17()
	{
		logic_SubGraph_SaveLoadBool_boolean_17 = local_SJBlockInScrapper_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_17 = local_SJBlockInScrapper_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_17.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_17, logic_SubGraph_SaveLoadBool_boolAsVariable_17, logic_SubGraph_SaveLoadBool_uniqueID_17);
	}

	private void Relay_Set_False_17()
	{
		logic_SubGraph_SaveLoadBool_boolean_17 = local_SJBlockInScrapper_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_17 = local_SJBlockInScrapper_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_17.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_17, logic_SubGraph_SaveLoadBool_boolAsVariable_17, logic_SubGraph_SaveLoadBool_uniqueID_17);
	}

	private void Relay_In_18()
	{
		logic_uScript_AddMessage_messageData_18 = msg03ScrapperSpawned;
		logic_uScript_AddMessage_speaker_18 = messageSpeaker;
		logic_uScript_AddMessage_Return_18 = logic_uScript_AddMessage_uScript_AddMessage_18.In(logic_uScript_AddMessage_messageData_18, logic_uScript_AddMessage_speaker_18);
		if (logic_uScript_AddMessage_uScript_AddMessage_18.Shown)
		{
			Relay_True_124();
		}
	}

	private void Relay_Out_24()
	{
	}

	private void Relay_In_24()
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_24 = local_Stage_System_Int32;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_24.In(ref logic_SubGraph_CompleteObjectiveStage_objectiveStage_24, logic_SubGraph_CompleteObjectiveStage_isFinalObjective_24);
	}

	private void Relay_Save_Out_30()
	{
		Relay_Save_71();
	}

	private void Relay_Load_Out_30()
	{
		Relay_Load_71();
	}

	private void Relay_Restart_Out_30()
	{
		Relay_Set_False_71();
	}

	private void Relay_Save_30()
	{
		logic_SubGraph_SaveLoadBool_boolean_30 = local_msgScrapperAttachedShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_30 = local_msgScrapperAttachedShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_30.Save(ref logic_SubGraph_SaveLoadBool_boolean_30, logic_SubGraph_SaveLoadBool_boolAsVariable_30, logic_SubGraph_SaveLoadBool_uniqueID_30);
	}

	private void Relay_Load_30()
	{
		logic_SubGraph_SaveLoadBool_boolean_30 = local_msgScrapperAttachedShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_30 = local_msgScrapperAttachedShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_30.Load(ref logic_SubGraph_SaveLoadBool_boolean_30, logic_SubGraph_SaveLoadBool_boolAsVariable_30, logic_SubGraph_SaveLoadBool_uniqueID_30);
	}

	private void Relay_Set_True_30()
	{
		logic_SubGraph_SaveLoadBool_boolean_30 = local_msgScrapperAttachedShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_30 = local_msgScrapperAttachedShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_30.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_30, logic_SubGraph_SaveLoadBool_boolAsVariable_30, logic_SubGraph_SaveLoadBool_uniqueID_30);
	}

	private void Relay_Set_False_30()
	{
		logic_SubGraph_SaveLoadBool_boolean_30 = local_msgScrapperAttachedShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_30 = local_msgScrapperAttachedShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_30.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_30, logic_SubGraph_SaveLoadBool_boolAsVariable_30, logic_SubGraph_SaveLoadBool_uniqueID_30);
	}

	private void Relay_In_32()
	{
		logic_uScriptCon_CompareBool_Bool_32 = local_ScrapperSpawned_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_32.In(logic_uScriptCon_CompareBool_Bool_32);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_32.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_32.False;
		if (num)
		{
			Relay_In_53();
		}
		if (flag)
		{
			Relay_In_121();
		}
	}

	private void Relay_True_33()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_33.True(out logic_uScriptAct_SetBool_Target_33);
		local_ScrappingInProgress_System_Boolean = logic_uScriptAct_SetBool_Target_33;
	}

	private void Relay_False_33()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_33.False(out logic_uScriptAct_SetBool_Target_33);
		local_ScrappingInProgress_System_Boolean = logic_uScriptAct_SetBool_Target_33;
	}

	private void Relay_In_34()
	{
		logic_uScript_AddMessage_messageData_34 = msg06PutBlock1InScrapper;
		logic_uScript_AddMessage_speaker_34 = messageSpeaker;
		logic_uScript_AddMessage_Return_34 = logic_uScript_AddMessage_uScript_AddMessage_34.In(logic_uScript_AddMessage_messageData_34, logic_uScript_AddMessage_speaker_34);
		if (logic_uScript_AddMessage_uScript_AddMessage_34.Out)
		{
			Relay_In_50();
		}
	}

	private void Relay_Save_Out_38()
	{
		Relay_Save_246();
	}

	private void Relay_Load_Out_38()
	{
		Relay_Load_246();
	}

	private void Relay_Restart_Out_38()
	{
		Relay_Restart_246();
	}

	private void Relay_Save_38()
	{
		logic_SubGraph_SaveLoadInt_integer_38 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_38 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_38.Save(logic_SubGraph_SaveLoadInt_restartValue_38, ref logic_SubGraph_SaveLoadInt_integer_38, logic_SubGraph_SaveLoadInt_intAsVariable_38, logic_SubGraph_SaveLoadInt_uniqueID_38);
	}

	private void Relay_Load_38()
	{
		logic_SubGraph_SaveLoadInt_integer_38 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_38 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_38.Load(logic_SubGraph_SaveLoadInt_restartValue_38, ref logic_SubGraph_SaveLoadInt_integer_38, logic_SubGraph_SaveLoadInt_intAsVariable_38, logic_SubGraph_SaveLoadInt_uniqueID_38);
	}

	private void Relay_Restart_38()
	{
		logic_SubGraph_SaveLoadInt_integer_38 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_38 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_38.Restart(logic_SubGraph_SaveLoadInt_restartValue_38, ref logic_SubGraph_SaveLoadInt_integer_38, logic_SubGraph_SaveLoadInt_intAsVariable_38, logic_SubGraph_SaveLoadInt_uniqueID_38);
	}

	private void Relay_In_39()
	{
		logic_uScriptCon_CompareBool_Bool_39 = local_ScrappingInProgress_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_39.In(logic_uScriptCon_CompareBool_Bool_39);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_39.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_39.False;
		if (num)
		{
			Relay_In_43();
		}
		if (flag)
		{
			Relay_In_14();
		}
	}

	private void Relay_In_42()
	{
		logic_uScript_HideArrow_uScript_HideArrow_42.In();
		if (logic_uScript_HideArrow_uScript_HideArrow_42.Out)
		{
			Relay_In_32();
		}
	}

	private void Relay_In_43()
	{
		logic_uScript_AddMessage_messageData_43 = msg08ScrappedBlock1;
		logic_uScript_AddMessage_speaker_43 = messageSpeaker;
		logic_uScript_AddMessage_Return_43 = logic_uScript_AddMessage_uScript_AddMessage_43.In(logic_uScript_AddMessage_messageData_43, logic_uScript_AddMessage_speaker_43);
		if (logic_uScript_AddMessage_uScript_AddMessage_43.Shown)
		{
			Relay_In_165();
		}
	}

	private void Relay_In_44()
	{
		logic_uScript_AddMessage_messageData_44 = msg04AttachScrapper;
		logic_uScript_AddMessage_speaker_44 = messageSpeaker;
		logic_uScript_AddMessage_Return_44 = logic_uScript_AddMessage_uScript_AddMessage_44.In(logic_uScript_AddMessage_messageData_44, logic_uScript_AddMessage_speaker_44);
		if (logic_uScript_AddMessage_uScript_AddMessage_44.Out)
		{
			Relay_In_57();
		}
	}

	private void Relay_In_50()
	{
		logic_uScript_PointArrowAtVisible_targetObject_50 = local_ScrapperBlock_TankBlock;
		logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_50.In(logic_uScript_PointArrowAtVisible_targetObject_50, logic_uScript_PointArrowAtVisible_timeToShowFor_50, logic_uScript_PointArrowAtVisible_offset_50);
		if (logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_50.Out)
		{
			Relay_In_107();
		}
	}

	private void Relay_Pause_51()
	{
		logic_uScript_PausePopulation_uScript_PausePopulation_51.Pause();
		if (logic_uScript_PausePopulation_uScript_PausePopulation_51.Out)
		{
			Relay_In_7();
		}
	}

	private void Relay_UnPause_51()
	{
		logic_uScript_PausePopulation_uScript_PausePopulation_51.UnPause();
		if (logic_uScript_PausePopulation_uScript_PausePopulation_51.Out)
		{
			Relay_In_7();
		}
	}

	private void Relay_In_53()
	{
		logic_uScript_EnableGlow_targetObject_53 = local_ScrapperBlock_TankBlock;
		logic_uScript_EnableGlow_uScript_EnableGlow_53.In(logic_uScript_EnableGlow_targetObject_53, logic_uScript_EnableGlow_enable_53);
		if (logic_uScript_EnableGlow_uScript_EnableGlow_53.Out)
		{
			Relay_In_87();
		}
	}

	private void Relay_Block_Attached_57()
	{
		Relay_In_24();
	}

	private void Relay_In_57()
	{
		logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_Block_57 = local_ScrapperBlock_TankBlock;
		logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_CraftingBaseTech_57 = local_CraftingBaseTech_Tank;
		int num = 0;
		Array array = ghostBlockScrapper;
		if (logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_ghostBlockData_57.Length != num + array.Length)
		{
			Array.Resize(ref logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_ghostBlockData_57, num + array.Length);
		}
		Array.Copy(array, 0, logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_ghostBlockData_57, num, array.Length);
		num += array.Length;
		logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_BlockType_57 = BlockTypeSJScrapper;
		logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_SubGraph_Crafting_Tutorial_AttachBlockToBase_57.In(logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_Block_57, logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_CraftingBaseTech_57, logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_ghostBlockData_57, logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_BlockType_57, logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_BlockPosition_57);
	}

	private void Relay_In_58()
	{
		logic_uScript_EnableGlow_targetObject_58 = local_ScrapperBlock_TankBlock;
		logic_uScript_EnableGlow_uScript_EnableGlow_58.In(logic_uScript_EnableGlow_targetObject_58, logic_uScript_EnableGlow_enable_58);
		if (logic_uScript_EnableGlow_uScript_EnableGlow_58.Out)
		{
			Relay_True_122();
		}
	}

	private void Relay_Output1_60()
	{
		Relay_In_97();
	}

	private void Relay_Output2_60()
	{
		Relay_In_149();
	}

	private void Relay_Output3_60()
	{
		Relay_In_119();
	}

	private void Relay_Output4_60()
	{
		Relay_In_168();
	}

	private void Relay_Output5_60()
	{
		Relay_In_284();
	}

	private void Relay_Output6_60()
	{
	}

	private void Relay_Output7_60()
	{
	}

	private void Relay_Output8_60()
	{
	}

	private void Relay_In_60()
	{
		logic_uScriptCon_ManualSwitch_CurrentOutput_60 = local_Stage_System_Int32;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_60.In(logic_uScriptCon_ManualSwitch_CurrentOutput_60);
	}

	private void Relay_SaveEvent_62()
	{
		Relay_Save_38();
	}

	private void Relay_LoadEvent_62()
	{
		Relay_Load_38();
	}

	private void Relay_RestartEvent_62()
	{
		Relay_Restart_38();
	}

	private void Relay_Save_Out_71()
	{
		Relay_Save_13();
	}

	private void Relay_Load_Out_71()
	{
		Relay_Load_13();
	}

	private void Relay_Restart_Out_71()
	{
		Relay_Set_False_13();
	}

	private void Relay_Save_71()
	{
		logic_SubGraph_SaveLoadBool_boolean_71 = local_msgBaseFoundShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_71 = local_msgBaseFoundShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_71.Save(ref logic_SubGraph_SaveLoadBool_boolean_71, logic_SubGraph_SaveLoadBool_boolAsVariable_71, logic_SubGraph_SaveLoadBool_uniqueID_71);
	}

	private void Relay_Load_71()
	{
		logic_SubGraph_SaveLoadBool_boolean_71 = local_msgBaseFoundShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_71 = local_msgBaseFoundShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_71.Load(ref logic_SubGraph_SaveLoadBool_boolean_71, logic_SubGraph_SaveLoadBool_boolAsVariable_71, logic_SubGraph_SaveLoadBool_uniqueID_71);
	}

	private void Relay_Set_True_71()
	{
		logic_SubGraph_SaveLoadBool_boolean_71 = local_msgBaseFoundShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_71 = local_msgBaseFoundShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_71.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_71, logic_SubGraph_SaveLoadBool_boolAsVariable_71, logic_SubGraph_SaveLoadBool_uniqueID_71);
	}

	private void Relay_Set_False_71()
	{
		logic_SubGraph_SaveLoadBool_boolean_71 = local_msgBaseFoundShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_71 = local_msgBaseFoundShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_71.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_71, logic_SubGraph_SaveLoadBool_boolAsVariable_71, logic_SubGraph_SaveLoadBool_uniqueID_71);
	}

	private void Relay_Save_Out_75()
	{
		Relay_Save_252();
	}

	private void Relay_Load_Out_75()
	{
		Relay_Load_252();
	}

	private void Relay_Restart_Out_75()
	{
		Relay_Set_False_252();
	}

	private void Relay_Save_75()
	{
		logic_SubGraph_SaveLoadBool_boolean_75 = local_ScrappingInProgress_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_75 = local_ScrappingInProgress_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_75.Save(ref logic_SubGraph_SaveLoadBool_boolean_75, logic_SubGraph_SaveLoadBool_boolAsVariable_75, logic_SubGraph_SaveLoadBool_uniqueID_75);
	}

	private void Relay_Load_75()
	{
		logic_SubGraph_SaveLoadBool_boolean_75 = local_ScrappingInProgress_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_75 = local_ScrappingInProgress_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_75.Load(ref logic_SubGraph_SaveLoadBool_boolean_75, logic_SubGraph_SaveLoadBool_boolAsVariable_75, logic_SubGraph_SaveLoadBool_uniqueID_75);
	}

	private void Relay_Set_True_75()
	{
		logic_SubGraph_SaveLoadBool_boolean_75 = local_ScrappingInProgress_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_75 = local_ScrappingInProgress_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_75.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_75, logic_SubGraph_SaveLoadBool_boolAsVariable_75, logic_SubGraph_SaveLoadBool_uniqueID_75);
	}

	private void Relay_Set_False_75()
	{
		logic_SubGraph_SaveLoadBool_boolean_75 = local_ScrappingInProgress_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_75 = local_ScrappingInProgress_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_75.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_75, logic_SubGraph_SaveLoadBool_boolAsVariable_75, logic_SubGraph_SaveLoadBool_uniqueID_75);
	}

	private void Relay_Out_76()
	{
		Relay_In_112();
	}

	private void Relay_In_76()
	{
		int num = 0;
		Array array = blockSpawnDataScrapper;
		if (logic_SubGraph_Crafting_Tutorial_ManageBlock_blockSpawnData_76.Length != num + array.Length)
		{
			Array.Resize(ref logic_SubGraph_Crafting_Tutorial_ManageBlock_blockSpawnData_76, num + array.Length);
		}
		Array.Copy(array, 0, logic_SubGraph_Crafting_Tutorial_ManageBlock_blockSpawnData_76, num, array.Length);
		num += array.Length;
		logic_SubGraph_Crafting_Tutorial_ManageBlock_block_76 = local_ScrapperBlock_TankBlock;
		logic_SubGraph_Crafting_Tutorial_ManageBlock_msgBlockOutsideArea_76 = msgBlockOutsideArea;
		logic_SubGraph_Crafting_Tutorial_ManageBlock_messageSpeaker_76 = messageSpeaker;
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_76.In(ref logic_SubGraph_Crafting_Tutorial_ManageBlock_blockSpawnData_76, logic_SubGraph_Crafting_Tutorial_ManageBlock_blockIndex_76, ref logic_SubGraph_Crafting_Tutorial_ManageBlock_block_76, logic_SubGraph_Crafting_Tutorial_ManageBlock_allowAnchoring_76, logic_SubGraph_Crafting_Tutorial_ManageBlock_msgBlockOutsideArea_76, logic_SubGraph_Crafting_Tutorial_ManageBlock_messageSpeaker_76);
	}

	private void Relay_In_78()
	{
		logic_uScript_IsPlayerInRangeOfTech_tech_78 = local_CraftingBaseTech_Tank;
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_78.In(logic_uScript_IsPlayerInRangeOfTech_tech_78, logic_uScript_IsPlayerInRangeOfTech_range_78, logic_uScript_IsPlayerInRangeOfTech_techs_78);
		if (logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_78.InRange)
		{
			Relay_In_90();
		}
	}

	private void Relay_In_80()
	{
		int num = 0;
		Array array = blockSpawnDataScrapper;
		if (logic_uScript_SpawnBlocksFromData_blockData_80.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SpawnBlocksFromData_blockData_80, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SpawnBlocksFromData_blockData_80, num, array.Length);
		num += array.Length;
		logic_uScript_SpawnBlocksFromData_ownerNode_80 = owner_Connection_103;
		logic_uScript_SpawnBlocksFromData_uScript_SpawnBlocksFromData_80.In(logic_uScript_SpawnBlocksFromData_blockData_80, logic_uScript_SpawnBlocksFromData_ownerNode_80);
		if (logic_uScript_SpawnBlocksFromData_uScript_SpawnBlocksFromData_80.Out)
		{
			Relay_In_11();
		}
	}

	private void Relay_In_81()
	{
		logic_uScriptCon_CompareBool_Bool_81 = local_msgScrapperAttachedShown_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_81.In(logic_uScriptCon_CompareBool_Bool_81);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_81.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_81.False;
		if (num)
		{
			Relay_In_34();
		}
		if (flag)
		{
			Relay_In_147();
		}
	}

	private void Relay_Pause_82()
	{
		logic_uScript_PausePopulation_uScript_PausePopulation_82.Pause();
		if (logic_uScript_PausePopulation_uScript_PausePopulation_82.Out)
		{
			Relay_True_86();
		}
	}

	private void Relay_UnPause_82()
	{
		logic_uScript_PausePopulation_uScript_PausePopulation_82.UnPause();
		if (logic_uScript_PausePopulation_uScript_PausePopulation_82.Out)
		{
			Relay_True_86();
		}
	}

	private void Relay_True_83()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_83.True(out logic_uScriptAct_SetBool_Target_83);
		local_msgScrapperAttachedShown_System_Boolean = logic_uScriptAct_SetBool_Target_83;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_83.Out)
		{
			Relay_In_50();
		}
	}

	private void Relay_False_83()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_83.False(out logic_uScriptAct_SetBool_Target_83);
		local_msgScrapperAttachedShown_System_Boolean = logic_uScriptAct_SetBool_Target_83;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_83.Out)
		{
			Relay_In_50();
		}
	}

	private void Relay_In_84()
	{
		logic_uScript_IsPlayerInRangeOfTech_tech_84 = local_CraftingBaseTech_Tank;
		logic_uScript_IsPlayerInRangeOfTech_range_84 = distBaseFound;
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_84.In(logic_uScript_IsPlayerInRangeOfTech_tech_84, logic_uScript_IsPlayerInRangeOfTech_range_84, logic_uScript_IsPlayerInRangeOfTech_techs_84);
		bool inRange = logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_84.InRange;
		bool outOfRange = logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_84.OutOfRange;
		if (inRange)
		{
			Relay_Pause_82();
		}
		if (outOfRange)
		{
			Relay_UnPause_51();
		}
	}

	private void Relay_True_86()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_86.True(out logic_uScriptAct_SetBool_Target_86);
		local_NearBase_System_Boolean = logic_uScriptAct_SetBool_Target_86;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_86.Out)
		{
			Relay_In_60();
		}
	}

	private void Relay_False_86()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_86.False(out logic_uScriptAct_SetBool_Target_86);
		local_NearBase_System_Boolean = logic_uScriptAct_SetBool_Target_86;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_86.Out)
		{
			Relay_In_60();
		}
	}

	private void Relay_In_87()
	{
		logic_uScriptCon_CompareBool_Bool_87 = local_NearBase_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_87.In(logic_uScriptCon_CompareBool_Bool_87);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_87.True)
		{
			Relay_In_6();
		}
	}

	private void Relay_Save_Out_88()
	{
		Relay_Save_197();
	}

	private void Relay_Load_Out_88()
	{
		Relay_Load_197();
	}

	private void Relay_Restart_Out_88()
	{
		Relay_Set_False_197();
	}

	private void Relay_Save_88()
	{
		logic_SubGraph_SaveLoadBool_boolean_88 = local_msgScrapperSpawnedShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_88 = local_msgScrapperSpawnedShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_88.Save(ref logic_SubGraph_SaveLoadBool_boolean_88, logic_SubGraph_SaveLoadBool_boolAsVariable_88, logic_SubGraph_SaveLoadBool_uniqueID_88);
	}

	private void Relay_Load_88()
	{
		logic_SubGraph_SaveLoadBool_boolean_88 = local_msgScrapperSpawnedShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_88 = local_msgScrapperSpawnedShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_88.Load(ref logic_SubGraph_SaveLoadBool_boolean_88, logic_SubGraph_SaveLoadBool_boolAsVariable_88, logic_SubGraph_SaveLoadBool_uniqueID_88);
	}

	private void Relay_Set_True_88()
	{
		logic_SubGraph_SaveLoadBool_boolean_88 = local_msgScrapperSpawnedShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_88 = local_msgScrapperSpawnedShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_88.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_88, logic_SubGraph_SaveLoadBool_boolAsVariable_88, logic_SubGraph_SaveLoadBool_uniqueID_88);
	}

	private void Relay_Set_False_88()
	{
		logic_SubGraph_SaveLoadBool_boolean_88 = local_msgScrapperSpawnedShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_88 = local_msgScrapperSpawnedShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_88.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_88, logic_SubGraph_SaveLoadBool_boolAsVariable_88, logic_SubGraph_SaveLoadBool_uniqueID_88);
	}

	private void Relay_In_90()
	{
		logic_uScriptCon_CompareBool_Bool_90 = local_msgIntroShown_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_90.In(logic_uScriptCon_CompareBool_Bool_90);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_90.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_90.False;
		if (num)
		{
			Relay_In_84();
		}
		if (flag)
		{
			Relay_True_117();
		}
	}

	private void Relay_Save_Out_95()
	{
		Relay_Save_88();
	}

	private void Relay_Load_Out_95()
	{
		Relay_Load_88();
	}

	private void Relay_Restart_Out_95()
	{
		Relay_Set_False_88();
	}

	private void Relay_Save_95()
	{
		logic_SubGraph_SaveLoadBool_boolean_95 = local_ScrapperSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_95 = local_ScrapperSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_95.Save(ref logic_SubGraph_SaveLoadBool_boolean_95, logic_SubGraph_SaveLoadBool_boolAsVariable_95, logic_SubGraph_SaveLoadBool_uniqueID_95);
	}

	private void Relay_Load_95()
	{
		logic_SubGraph_SaveLoadBool_boolean_95 = local_ScrapperSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_95 = local_ScrapperSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_95.Load(ref logic_SubGraph_SaveLoadBool_boolean_95, logic_SubGraph_SaveLoadBool_boolAsVariable_95, logic_SubGraph_SaveLoadBool_uniqueID_95);
	}

	private void Relay_Set_True_95()
	{
		logic_SubGraph_SaveLoadBool_boolean_95 = local_ScrapperSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_95 = local_ScrapperSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_95.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_95, logic_SubGraph_SaveLoadBool_boolAsVariable_95, logic_SubGraph_SaveLoadBool_uniqueID_95);
	}

	private void Relay_Set_False_95()
	{
		logic_SubGraph_SaveLoadBool_boolean_95 = local_ScrapperSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_95 = local_ScrapperSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_95.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_95, logic_SubGraph_SaveLoadBool_boolAsVariable_95, logic_SubGraph_SaveLoadBool_uniqueID_95);
	}

	private void Relay_In_96()
	{
		logic_uScript_AddMessage_messageData_96 = msg02BaseFound;
		logic_uScript_AddMessage_speaker_96 = messageSpeaker;
		logic_uScript_AddMessage_Return_96 = logic_uScript_AddMessage_uScript_AddMessage_96.In(logic_uScript_AddMessage_messageData_96, logic_uScript_AddMessage_speaker_96);
		if (logic_uScript_AddMessage_uScript_AddMessage_96.Shown)
		{
			Relay_True_132();
		}
	}

	private void Relay_Out_97()
	{
	}

	private void Relay_In_97()
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_97 = local_Stage_System_Int32;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_97.In(ref logic_SubGraph_CompleteObjectiveStage_objectiveStage_97, logic_SubGraph_CompleteObjectiveStage_isFinalObjective_97);
	}

	private void Relay_True_104()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_104.True(out logic_uScriptAct_SetBool_Target_104);
		local_NearBase_System_Boolean = logic_uScriptAct_SetBool_Target_104;
	}

	private void Relay_False_104()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_104.False(out logic_uScriptAct_SetBool_Target_104);
		local_NearBase_System_Boolean = logic_uScriptAct_SetBool_Target_104;
	}

	private void Relay_In_105()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_105.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_105.Out)
		{
			Relay_In_112();
		}
	}

	private void Relay_In_107()
	{
		logic_uScript_EnableGlow_targetObject_107 = local_ScrapperBlock_TankBlock;
		logic_uScript_EnableGlow_uScript_EnableGlow_107.In(logic_uScript_EnableGlow_targetObject_107, logic_uScript_EnableGlow_enable_107);
		if (logic_uScript_EnableGlow_uScript_EnableGlow_107.Out)
		{
			Relay_True_234();
		}
	}

	private void Relay_In_112()
	{
		logic_uScript_LockTechInteraction_tech_112 = local_CraftingBaseTech_Tank;
		logic_uScript_LockTechInteraction_uScript_LockTechInteraction_112.In(logic_uScript_LockTechInteraction_tech_112, logic_uScript_LockTechInteraction_excludedBlocks_112, logic_uScript_LockTechInteraction_excludedUniqueBlocks_112);
		if (logic_uScript_LockTechInteraction_uScript_LockTechInteraction_112.Out)
		{
			Relay_In_198();
		}
	}

	private void Relay_True_117()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_117.True(out logic_uScriptAct_SetBool_Target_117);
		local_msgIntroShown_System_Boolean = logic_uScriptAct_SetBool_Target_117;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_117.Out)
		{
			Relay_In_141();
		}
	}

	private void Relay_False_117()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_117.False(out logic_uScriptAct_SetBool_Target_117);
		local_msgIntroShown_System_Boolean = logic_uScriptAct_SetBool_Target_117;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_117.Out)
		{
			Relay_In_141();
		}
	}

	private void Relay_True_118()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_118.True(out logic_uScriptAct_SetBool_Target_118);
		local_ScrapperSpawned_System_Boolean = logic_uScriptAct_SetBool_Target_118;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_118.Out)
		{
			Relay_In_80();
		}
	}

	private void Relay_False_118()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_118.False(out logic_uScriptAct_SetBool_Target_118);
		local_ScrapperSpawned_System_Boolean = logic_uScriptAct_SetBool_Target_118;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_118.Out)
		{
			Relay_In_80();
		}
	}

	private void Relay_In_119()
	{
		logic_uScriptCon_CompareBool_Bool_119 = local_SJBlockInScrapper_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_119.In(logic_uScriptCon_CompareBool_Bool_119);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_119.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_119.False;
		if (num)
		{
			Relay_In_39();
		}
		if (flag)
		{
			Relay_In_81();
		}
	}

	private void Relay_In_121()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_121.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_121.Out)
		{
			Relay_In_87();
		}
	}

	private void Relay_True_122()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_122.True(out logic_uScriptAct_SetBool_Target_122);
		local_SJBlockInScrapper_System_Boolean = logic_uScriptAct_SetBool_Target_122;
	}

	private void Relay_False_122()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_122.False(out logic_uScriptAct_SetBool_Target_122);
		local_SJBlockInScrapper_System_Boolean = logic_uScriptAct_SetBool_Target_122;
	}

	private void Relay_True_124()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_124.True(out logic_uScriptAct_SetBool_Target_124);
		local_msgScrapperSpawnedShown_System_Boolean = logic_uScriptAct_SetBool_Target_124;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_124.Out)
		{
			Relay_In_57();
		}
	}

	private void Relay_False_124()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_124.False(out logic_uScriptAct_SetBool_Target_124);
		local_msgScrapperSpawnedShown_System_Boolean = logic_uScriptAct_SetBool_Target_124;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_124.Out)
		{
			Relay_In_57();
		}
	}

	private void Relay_In_125()
	{
		logic_uScript_LockTechStacks_tech_125 = local_CraftingBaseTech_Tank;
		logic_uScript_LockTechStacks_uScript_LockTechStacks_125.In(logic_uScript_LockTechStacks_tech_125);
		if (logic_uScript_LockTechStacks_uScript_LockTechStacks_125.Out)
		{
			Relay_In_630();
		}
	}

	private void Relay_Out_126()
	{
		Relay_In_221();
	}

	private void Relay_In_126()
	{
		int num = 0;
		Array array = baseSpawnData;
		if (logic_SubGraph_Crafting_Tutorial_Init_baseSpawnData_126.Length != num + array.Length)
		{
			Array.Resize(ref logic_SubGraph_Crafting_Tutorial_Init_baseSpawnData_126, num + array.Length);
		}
		Array.Copy(array, 0, logic_SubGraph_Crafting_Tutorial_Init_baseSpawnData_126, num, array.Length);
		num += array.Length;
		int num2 = 0;
		Array array2 = blockSpawnData;
		if (logic_SubGraph_Crafting_Tutorial_Init_blockSpawnData_126.Length != num2 + array2.Length)
		{
			Array.Resize(ref logic_SubGraph_Crafting_Tutorial_Init_blockSpawnData_126, num2 + array2.Length);
		}
		Array.Copy(array2, 0, logic_SubGraph_Crafting_Tutorial_Init_blockSpawnData_126, num2, array2.Length);
		num2 += array2.Length;
		int num3 = 0;
		Array nPCSpawnData = NPCSpawnData;
		if (logic_SubGraph_Crafting_Tutorial_Init_NPCSpawnData_126.Length != num3 + nPCSpawnData.Length)
		{
			Array.Resize(ref logic_SubGraph_Crafting_Tutorial_Init_NPCSpawnData_126, num3 + nPCSpawnData.Length);
		}
		Array.Copy(nPCSpawnData, 0, logic_SubGraph_Crafting_Tutorial_Init_NPCSpawnData_126, num3, nPCSpawnData.Length);
		num3 += nPCSpawnData.Length;
		logic_SubGraph_Crafting_Tutorial_Init_completedBasePreset_126 = completedBasePreset;
		logic_SubGraph_Crafting_Tutorial_Init_basePosition_126 = basePosition;
		logic_SubGraph_Crafting_Tutorial_Init_clearSceneryRadius_126 = clearSceneryRadius;
		logic_SubGraph_Crafting_Tutorial_Init_CraftingBaseTech_126 = local_CraftingBaseTech_Tank;
		logic_SubGraph_Crafting_Tutorial_Init_NPCTech_126 = local_NPCTech_Tank;
		logic_SubGraph_Crafting_Tutorial_Init_SubGraph_Crafting_Tutorial_Init_126.In(logic_SubGraph_Crafting_Tutorial_Init_baseSpawnData_126, logic_SubGraph_Crafting_Tutorial_Init_blockSpawnData_126, logic_SubGraph_Crafting_Tutorial_Init_NPCSpawnData_126, logic_SubGraph_Crafting_Tutorial_Init_completedBasePreset_126, logic_SubGraph_Crafting_Tutorial_Init_useNPCAsTutorialTech_126, logic_SubGraph_Crafting_Tutorial_Init_spawnBase_126, logic_SubGraph_Crafting_Tutorial_Init_basePosition_126, logic_SubGraph_Crafting_Tutorial_Init_clearSceneryRadius_126, ref logic_SubGraph_Crafting_Tutorial_Init_CraftingBaseTech_126, ref logic_SubGraph_Crafting_Tutorial_Init_NPCTech_126);
	}

	private void Relay_True_132()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_132.True(out logic_uScriptAct_SetBool_Target_132);
		local_msgBaseFoundShown_System_Boolean = logic_uScriptAct_SetBool_Target_132;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_132.Out)
		{
			Relay_In_145();
		}
	}

	private void Relay_False_132()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_132.False(out logic_uScriptAct_SetBool_Target_132);
		local_msgBaseFoundShown_System_Boolean = logic_uScriptAct_SetBool_Target_132;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_132.Out)
		{
			Relay_In_145();
		}
	}

	private void Relay_In_136()
	{
		logic_uScriptCon_CompareBool_Bool_136 = local_ScrapperSpawned_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_136.In(logic_uScriptCon_CompareBool_Bool_136);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_136.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_136.False;
		if (num)
		{
			Relay_In_76();
		}
		if (flag)
		{
			Relay_In_105();
		}
	}

	private void Relay_OnUpdate_140()
	{
		Relay_In_126();
	}

	private void Relay_OnSuspend_140()
	{
	}

	private void Relay_OnResume_140()
	{
	}

	private void Relay_In_141()
	{
		logic_uScript_AddMessage_messageData_141 = msg01Intro;
		logic_uScript_AddMessage_speaker_141 = messageSpeaker;
		logic_uScript_AddMessage_Return_141 = logic_uScript_AddMessage_uScript_AddMessage_141.In(logic_uScript_AddMessage_messageData_141, logic_uScript_AddMessage_speaker_141);
		if (logic_uScript_AddMessage_uScript_AddMessage_141.Out)
		{
			Relay_In_84();
		}
	}

	private void Relay_Out_144()
	{
	}

	private void Relay_In_144()
	{
		logic_SubGraph_LoadObjectiveStates_currentObjective_144 = local_Stage_System_Int32;
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_144.In(logic_SubGraph_LoadObjectiveStates_currentObjective_144);
	}

	private void Relay_In_145()
	{
		logic_uScriptCon_CompareBool_Bool_145 = local_ScrapperSpawned_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_145.In(logic_uScriptCon_CompareBool_Bool_145);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_145.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_145.False;
		if (num)
		{
			Relay_In_11();
		}
		if (flag)
		{
			Relay_True_118();
		}
	}

	private void Relay_In_147()
	{
		logic_uScript_AddMessage_messageData_147 = msg05ScrapperAttached;
		logic_uScript_AddMessage_speaker_147 = messageSpeaker;
		logic_uScript_AddMessage_Return_147 = logic_uScript_AddMessage_uScript_AddMessage_147.In(logic_uScript_AddMessage_messageData_147, logic_uScript_AddMessage_speaker_147);
		if (logic_uScript_AddMessage_uScript_AddMessage_147.Shown)
		{
			Relay_True_83();
		}
	}

	private void Relay_In_148()
	{
		logic_uScript_IsCraftingBlockProducingItem_craftingBlock_148 = local_ScrapperBlock_TankBlock;
		logic_uScript_IsCraftingBlockProducingItem_uScript_IsCraftingBlockProducingItem_148.In(logic_uScript_IsCraftingBlockProducingItem_craftingBlock_148);
		if (logic_uScript_IsCraftingBlockProducingItem_uScript_IsCraftingBlockProducingItem_148.True)
		{
			Relay_True_33();
		}
	}

	private void Relay_In_149()
	{
		logic_uScriptCon_CompareBool_Bool_149 = local_msgBaseFoundShown_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_149.In(logic_uScriptCon_CompareBool_Bool_149);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_149.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_149.False;
		if (num)
		{
			Relay_In_145();
		}
		if (flag)
		{
			Relay_In_96();
		}
	}

	private void Relay_In_156()
	{
		logic_uScript_IsCraftingBlockProducingItem_craftingBlock_156 = local_ScrapperBlock_TankBlock;
		logic_uScript_IsCraftingBlockProducingItem_uScript_IsCraftingBlockProducingItem_156.In(logic_uScript_IsCraftingBlockProducingItem_craftingBlock_156);
		if (logic_uScript_IsCraftingBlockProducingItem_uScript_IsCraftingBlockProducingItem_156.True)
		{
			Relay_True_159();
		}
	}

	private void Relay_Out_157()
	{
	}

	private void Relay_In_157()
	{
		logic_SubGraph_Crafting_Tutorial_Finish_craftingBaseTech_157 = local_CraftingBaseTech_Tank;
		logic_SubGraph_Crafting_Tutorial_Finish_NPCTech_157 = local_NPCTech_Tank;
		logic_SubGraph_Crafting_Tutorial_Finish_NPCFlyAwayAI_157 = NPCFlyAwayAI;
		logic_SubGraph_Crafting_Tutorial_Finish_NPCDespawnParticleEffect_157 = NPCDespawnParticleEffect;
		logic_SubGraph_Crafting_Tutorial_Finish_SubGraph_Crafting_Tutorial_Finish_157.In(logic_SubGraph_Crafting_Tutorial_Finish_craftingBaseTech_157, logic_SubGraph_Crafting_Tutorial_Finish_NPCTech_157, logic_SubGraph_Crafting_Tutorial_Finish_NPCFlyAwayAI_157, logic_SubGraph_Crafting_Tutorial_Finish_NPCDespawnParticleEffect_157);
	}

	private void Relay_In_158()
	{
		logic_uScriptCon_CompareBool_Bool_158 = local_ScrappingOtherCorpBlockInProgress_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_158.In(logic_uScriptCon_CompareBool_Bool_158);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_158.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_158.False;
		if (num)
		{
			Relay_In_239();
		}
		if (flag)
		{
			Relay_In_156();
		}
	}

	private void Relay_True_159()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_159.True(out logic_uScriptAct_SetBool_Target_159);
		local_ScrappingOtherCorpBlockInProgress_System_Boolean = logic_uScriptAct_SetBool_Target_159;
	}

	private void Relay_False_159()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_159.False(out logic_uScriptAct_SetBool_Target_159);
		local_ScrappingOtherCorpBlockInProgress_System_Boolean = logic_uScriptAct_SetBool_Target_159;
	}

	private void Relay_In_162()
	{
		logic_uScript_AddMessage_messageData_162 = msg13Complete;
		logic_uScript_AddMessage_speaker_162 = messageSpeaker;
		logic_uScript_AddMessage_Return_162 = logic_uScript_AddMessage_uScript_AddMessage_162.In(logic_uScript_AddMessage_messageData_162, logic_uScript_AddMessage_speaker_162);
		if (logic_uScript_AddMessage_uScript_AddMessage_162.Shown)
		{
			Relay_In_157();
		}
	}

	private void Relay_Out_165()
	{
	}

	private void Relay_In_165()
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_165 = local_Stage_System_Int32;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_165.In(ref logic_SubGraph_CompleteObjectiveStage_objectiveStage_165, logic_SubGraph_CompleteObjectiveStage_isFinalObjective_165);
	}

	private void Relay_Save_Out_166()
	{
		Relay_Save_75();
	}

	private void Relay_Load_Out_166()
	{
		Relay_Load_75();
	}

	private void Relay_Restart_Out_166()
	{
		Relay_Set_False_75();
	}

	private void Relay_Save_166()
	{
		logic_SubGraph_SaveLoadBool_boolean_166 = local_OtherCorpBlockInScrapper_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_166 = local_OtherCorpBlockInScrapper_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_166.Save(ref logic_SubGraph_SaveLoadBool_boolean_166, logic_SubGraph_SaveLoadBool_boolAsVariable_166, logic_SubGraph_SaveLoadBool_uniqueID_166);
	}

	private void Relay_Load_166()
	{
		logic_SubGraph_SaveLoadBool_boolean_166 = local_OtherCorpBlockInScrapper_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_166 = local_OtherCorpBlockInScrapper_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_166.Load(ref logic_SubGraph_SaveLoadBool_boolean_166, logic_SubGraph_SaveLoadBool_boolAsVariable_166, logic_SubGraph_SaveLoadBool_uniqueID_166);
	}

	private void Relay_Set_True_166()
	{
		logic_SubGraph_SaveLoadBool_boolean_166 = local_OtherCorpBlockInScrapper_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_166 = local_OtherCorpBlockInScrapper_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_166.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_166, logic_SubGraph_SaveLoadBool_boolAsVariable_166, logic_SubGraph_SaveLoadBool_uniqueID_166);
	}

	private void Relay_Set_False_166()
	{
		logic_SubGraph_SaveLoadBool_boolean_166 = local_OtherCorpBlockInScrapper_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_166 = local_OtherCorpBlockInScrapper_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_166.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_166, logic_SubGraph_SaveLoadBool_boolAsVariable_166, logic_SubGraph_SaveLoadBool_uniqueID_166);
	}

	private void Relay_In_168()
	{
		logic_uScriptCon_CompareBool_Bool_168 = local_OtherCorpBlockInScrapper_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_168.In(logic_uScriptCon_CompareBool_Bool_168);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_168.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_168.False;
		if (num)
		{
			Relay_In_158();
		}
		if (flag)
		{
			Relay_In_191();
		}
	}

	private void Relay_Save_Out_170()
	{
		Relay_Save_30();
	}

	private void Relay_Load_Out_170()
	{
		Relay_Load_30();
	}

	private void Relay_Restart_Out_170()
	{
		Relay_Set_False_30();
	}

	private void Relay_Save_170()
	{
		logic_SubGraph_SaveLoadBool_boolean_170 = local_ScrappingOtherCorpBlockInProgress_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_170 = local_ScrappingOtherCorpBlockInProgress_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_170.Save(ref logic_SubGraph_SaveLoadBool_boolean_170, logic_SubGraph_SaveLoadBool_boolAsVariable_170, logic_SubGraph_SaveLoadBool_uniqueID_170);
	}

	private void Relay_Load_170()
	{
		logic_SubGraph_SaveLoadBool_boolean_170 = local_ScrappingOtherCorpBlockInProgress_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_170 = local_ScrappingOtherCorpBlockInProgress_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_170.Load(ref logic_SubGraph_SaveLoadBool_boolean_170, logic_SubGraph_SaveLoadBool_boolAsVariable_170, logic_SubGraph_SaveLoadBool_uniqueID_170);
	}

	private void Relay_Set_True_170()
	{
		logic_SubGraph_SaveLoadBool_boolean_170 = local_ScrappingOtherCorpBlockInProgress_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_170 = local_ScrappingOtherCorpBlockInProgress_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_170.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_170, logic_SubGraph_SaveLoadBool_boolAsVariable_170, logic_SubGraph_SaveLoadBool_uniqueID_170);
	}

	private void Relay_Set_False_170()
	{
		logic_SubGraph_SaveLoadBool_boolean_170 = local_ScrappingOtherCorpBlockInProgress_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_170 = local_ScrappingOtherCorpBlockInProgress_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_170.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_170, logic_SubGraph_SaveLoadBool_boolAsVariable_170, logic_SubGraph_SaveLoadBool_uniqueID_170);
	}

	private void Relay_In_173()
	{
		logic_uScript_PointArrowAtVisible_targetObject_173 = local_ScrapperBlock_TankBlock;
		logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_173.In(logic_uScript_PointArrowAtVisible_targetObject_173, logic_uScript_PointArrowAtVisible_timeToShowFor_173, logic_uScript_PointArrowAtVisible_offset_173);
		if (logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_173.Out)
		{
			Relay_In_183();
		}
	}

	private void Relay_In_176()
	{
		logic_uScript_IsCraftingBlockInOperation_craftingBlock_176 = local_ScrapperBlock_TankBlock;
		logic_uScript_IsCraftingBlockInOperation_uScript_IsCraftingBlockInOperation_176.In(logic_uScript_IsCraftingBlockInOperation_craftingBlock_176);
		if (logic_uScript_IsCraftingBlockInOperation_uScript_IsCraftingBlockInOperation_176.True)
		{
			Relay_In_184();
		}
	}

	private void Relay_True_182()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_182.True(out logic_uScriptAct_SetBool_Target_182);
		local_msgBlockScrappedShown_System_Boolean = logic_uScriptAct_SetBool_Target_182;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_182.Out)
		{
			Relay_In_173();
		}
	}

	private void Relay_False_182()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_182.False(out logic_uScriptAct_SetBool_Target_182);
		local_msgBlockScrappedShown_System_Boolean = logic_uScriptAct_SetBool_Target_182;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_182.Out)
		{
			Relay_In_173();
		}
	}

	private void Relay_In_183()
	{
		logic_uScript_EnableGlow_targetObject_183 = local_ScrapperBlock_TankBlock;
		logic_uScript_EnableGlow_uScript_EnableGlow_183.In(logic_uScript_EnableGlow_targetObject_183, logic_uScript_EnableGlow_enable_183);
		if (logic_uScript_EnableGlow_uScript_EnableGlow_183.Out)
		{
			Relay_True_235();
		}
	}

	private void Relay_In_184()
	{
		logic_uScript_HideArrow_uScript_HideArrow_184.In();
		if (logic_uScript_HideArrow_uScript_HideArrow_184.Out)
		{
			Relay_In_188();
		}
	}

	private void Relay_In_186()
	{
		logic_uScript_AddMessage_messageData_186 = msg09Block1Scrapped;
		logic_uScript_AddMessage_speaker_186 = messageSpeaker;
		logic_uScript_AddMessage_Return_186 = logic_uScript_AddMessage_uScript_AddMessage_186.In(logic_uScript_AddMessage_messageData_186, logic_uScript_AddMessage_speaker_186);
		if (logic_uScript_AddMessage_uScript_AddMessage_186.Shown)
		{
			Relay_True_182();
		}
	}

	private void Relay_In_188()
	{
		logic_uScript_EnableGlow_targetObject_188 = local_ScrapperBlock_TankBlock;
		logic_uScript_EnableGlow_uScript_EnableGlow_188.In(logic_uScript_EnableGlow_targetObject_188, logic_uScript_EnableGlow_enable_188);
		if (logic_uScript_EnableGlow_uScript_EnableGlow_188.Out)
		{
			Relay_True_189();
		}
	}

	private void Relay_True_189()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_189.True(out logic_uScriptAct_SetBool_Target_189);
		local_OtherCorpBlockInScrapper_System_Boolean = logic_uScriptAct_SetBool_Target_189;
	}

	private void Relay_False_189()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_189.False(out logic_uScriptAct_SetBool_Target_189);
		local_OtherCorpBlockInScrapper_System_Boolean = logic_uScriptAct_SetBool_Target_189;
	}

	private void Relay_In_190()
	{
		logic_uScript_AddMessage_messageData_190 = msg10PutBlock2InScrapper;
		logic_uScript_AddMessage_speaker_190 = messageSpeaker;
		logic_uScript_AddMessage_Return_190 = logic_uScript_AddMessage_uScript_AddMessage_190.In(logic_uScript_AddMessage_messageData_190, logic_uScript_AddMessage_speaker_190);
		if (logic_uScript_AddMessage_uScript_AddMessage_190.Out)
		{
			Relay_In_173();
		}
	}

	private void Relay_In_191()
	{
		logic_uScriptCon_CompareBool_Bool_191 = local_msgBlockScrappedShown_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_191.In(logic_uScriptCon_CompareBool_Bool_191);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_191.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_191.False;
		if (num)
		{
			Relay_In_190();
		}
		if (flag)
		{
			Relay_In_186();
		}
	}

	private void Relay_Save_Out_197()
	{
	}

	private void Relay_Load_Out_197()
	{
		Relay_In_144();
	}

	private void Relay_Restart_Out_197()
	{
		Relay_False_104();
	}

	private void Relay_Save_197()
	{
		logic_SubGraph_SaveLoadBool_boolean_197 = local_msgBlockScrappedShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_197 = local_msgBlockScrappedShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_197.Save(ref logic_SubGraph_SaveLoadBool_boolean_197, logic_SubGraph_SaveLoadBool_boolAsVariable_197, logic_SubGraph_SaveLoadBool_uniqueID_197);
	}

	private void Relay_Load_197()
	{
		logic_SubGraph_SaveLoadBool_boolean_197 = local_msgBlockScrappedShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_197 = local_msgBlockScrappedShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_197.Load(ref logic_SubGraph_SaveLoadBool_boolean_197, logic_SubGraph_SaveLoadBool_boolAsVariable_197, logic_SubGraph_SaveLoadBool_uniqueID_197);
	}

	private void Relay_Set_True_197()
	{
		logic_SubGraph_SaveLoadBool_boolean_197 = local_msgBlockScrappedShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_197 = local_msgBlockScrappedShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_197.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_197, logic_SubGraph_SaveLoadBool_boolAsVariable_197, logic_SubGraph_SaveLoadBool_uniqueID_197);
	}

	private void Relay_Set_False_197()
	{
		logic_SubGraph_SaveLoadBool_boolean_197 = local_msgBlockScrappedShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_197 = local_msgBlockScrappedShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_197.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_197, logic_SubGraph_SaveLoadBool_boolAsVariable_197, logic_SubGraph_SaveLoadBool_uniqueID_197);
	}

	private void Relay_In_198()
	{
		logic_uScriptCon_CompareBool_Bool_198 = local_CanGrabResources_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_198.In(logic_uScriptCon_CompareBool_Bool_198);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_198.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_198.False;
		if (num)
		{
			Relay_In_206();
		}
		if (flag)
		{
			Relay_In_125();
		}
	}

	private void Relay_In_200()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_200.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_200.Out)
		{
			Relay_In_630();
		}
	}

	private void Relay_In_201()
	{
		int num = 0;
		if (logic_uScriptAct_Concatenate_A_201.Length <= num)
		{
			Array.Resize(ref logic_uScriptAct_Concatenate_A_201, num + 1);
		}
		logic_uScriptAct_Concatenate_A_201[num++] = local_203_System_String;
		int num2 = 0;
		if (logic_uScriptAct_Concatenate_B_201.Length <= num2)
		{
			Array.Resize(ref logic_uScriptAct_Concatenate_B_201, num2 + 1);
		}
		logic_uScriptAct_Concatenate_B_201[num2++] = local_ScrappedOtherCorpBlockEarly_System_Boolean;
		logic_uScriptAct_Concatenate_uScriptAct_Concatenate_201.In(logic_uScriptAct_Concatenate_A_201, logic_uScriptAct_Concatenate_B_201, logic_uScriptAct_Concatenate_Separator_201, out logic_uScriptAct_Concatenate_Result_201);
		local_202_System_String = logic_uScriptAct_Concatenate_Result_201;
		if (logic_uScriptAct_Concatenate_uScriptAct_Concatenate_201.Out)
		{
			Relay_ShowLabel_205();
		}
	}

	private void Relay_ShowLabel_205()
	{
		logic_uScriptAct_PrintText_Text_205 = local_202_System_String;
		logic_uScriptAct_PrintText_uScriptAct_PrintText_205.ShowLabel(logic_uScriptAct_PrintText_Text_205, logic_uScriptAct_PrintText_FontSize_205, logic_uScriptAct_PrintText_FontStyle_205, logic_uScriptAct_PrintText_FontColor_205, logic_uScriptAct_PrintText_textAnchor_205, logic_uScriptAct_PrintText_EdgePadding_205, logic_uScriptAct_PrintText_time_205);
	}

	private void Relay_HideLabel_205()
	{
		logic_uScriptAct_PrintText_Text_205 = local_202_System_String;
		logic_uScriptAct_PrintText_uScriptAct_PrintText_205.HideLabel(logic_uScriptAct_PrintText_Text_205, logic_uScriptAct_PrintText_FontSize_205, logic_uScriptAct_PrintText_FontStyle_205, logic_uScriptAct_PrintText_FontColor_205, logic_uScriptAct_PrintText_textAnchor_205, logic_uScriptAct_PrintText_EdgePadding_205, logic_uScriptAct_PrintText_time_205);
	}

	private void Relay_In_206()
	{
		logic_uScript_LockBlock_block_206 = local_ScrapperBlock_TankBlock;
		logic_uScript_LockBlock_uScript_LockBlock_206.In(logic_uScript_LockBlock_block_206, logic_uScript_LockBlock_functionalityToLock_206);
		if (logic_uScript_LockBlock_uScript_LockBlock_206.Out)
		{
			Relay_In_200();
		}
	}

	private void Relay_In_208()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_208.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_208.Out)
		{
			Relay_In_136();
		}
	}

	private void Relay_In_209()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_209.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_209.Out)
		{
			Relay_In_231();
		}
	}

	private void Relay_In_211()
	{
		logic_uScript_GetNamedBlock_name_211 = local_ScrapBlock1Name_System_String;
		logic_uScript_GetNamedBlock_owner_211 = owner_Connection_220;
		logic_uScript_GetNamedBlock_Return_211 = logic_uScript_GetNamedBlock_uScript_GetNamedBlock_211.In(logic_uScript_GetNamedBlock_name_211, logic_uScript_GetNamedBlock_owner_211);
		local_215_TankBlock = logic_uScript_GetNamedBlock_Return_211;
		if (logic_uScript_GetNamedBlock_uScript_GetNamedBlock_211.Out)
		{
			Relay_In_229();
		}
	}

	private void Relay_In_213()
	{
		logic_uScript_LockBlock_block_213 = local_224_TankBlock;
		logic_uScript_LockBlock_uScript_LockBlock_213.In(logic_uScript_LockBlock_block_213, logic_uScript_LockBlock_functionalityToLock_213);
		if (logic_uScript_LockBlock_uScript_LockBlock_213.Out)
		{
			Relay_In_228();
		}
	}

	private void Relay_In_214()
	{
		logic_uScript_LockBlock_block_214 = local_215_TankBlock;
		logic_uScript_LockBlock_uScript_LockBlock_214.In(logic_uScript_LockBlock_block_214, logic_uScript_LockBlock_functionalityToLock_214);
		if (logic_uScript_LockBlock_uScript_LockBlock_214.Out)
		{
			Relay_In_231();
		}
	}

	private void Relay_In_216()
	{
		logic_uScript_LockBlock_block_216 = local_215_TankBlock;
		logic_uScript_LockBlock_uScript_LockBlock_216.In(logic_uScript_LockBlock_block_216, logic_uScript_LockBlock_functionalityToLock_216);
		if (logic_uScript_LockBlock_uScript_LockBlock_216.Out)
		{
			Relay_In_222();
		}
	}

	private void Relay_In_217()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_217.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_217.Out)
		{
			Relay_In_208();
		}
	}

	private void Relay_In_219()
	{
		logic_uScript_LockBlock_block_219 = local_215_TankBlock;
		logic_uScript_LockBlock_uScript_LockBlock_219.In(logic_uScript_LockBlock_block_219, logic_uScript_LockBlock_functionalityToLock_219);
		if (logic_uScript_LockBlock_uScript_LockBlock_219.Out)
		{
			Relay_In_216();
		}
	}

	private void Relay_In_221()
	{
		logic_uScriptCon_CompareBool_Bool_221 = local_CanSuckUpScrapBlock1_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_221.In(logic_uScriptCon_CompareBool_Bool_221);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_221.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_221.False;
		if (num)
		{
			Relay_In_209();
		}
		if (flag)
		{
			Relay_In_211();
		}
	}

	private void Relay_In_222()
	{
		logic_uScript_LockBlock_block_222 = local_215_TankBlock;
		logic_uScript_LockBlock_uScript_LockBlock_222.In(logic_uScript_LockBlock_block_222, logic_uScript_LockBlock_functionalityToLock_222);
		if (logic_uScript_LockBlock_uScript_LockBlock_222.Out)
		{
			Relay_In_214();
		}
	}

	private void Relay_In_223()
	{
		logic_uScript_LockBlock_block_223 = local_224_TankBlock;
		logic_uScript_LockBlock_uScript_LockBlock_223.In(logic_uScript_LockBlock_block_223, logic_uScript_LockBlock_functionalityToLock_223);
		if (logic_uScript_LockBlock_uScript_LockBlock_223.Out)
		{
			Relay_In_213();
		}
	}

	private void Relay_In_225()
	{
		logic_uScript_LockBlock_block_225 = local_224_TankBlock;
		logic_uScript_LockBlock_uScript_LockBlock_225.In(logic_uScript_LockBlock_block_225, logic_uScript_LockBlock_functionalityToLock_225);
		if (logic_uScript_LockBlock_uScript_LockBlock_225.Out)
		{
			Relay_In_208();
		}
	}

	private void Relay_In_226()
	{
		logic_uScript_LockBlock_block_226 = local_224_TankBlock;
		logic_uScript_LockBlock_uScript_LockBlock_226.In(logic_uScript_LockBlock_block_226, logic_uScript_LockBlock_functionalityToLock_226);
		if (logic_uScript_LockBlock_uScript_LockBlock_226.Out)
		{
			Relay_In_223();
		}
	}

	private void Relay_In_228()
	{
		logic_uScript_LockBlock_block_228 = local_224_TankBlock;
		logic_uScript_LockBlock_uScript_LockBlock_228.In(logic_uScript_LockBlock_block_228, logic_uScript_LockBlock_functionalityToLock_228);
		if (logic_uScript_LockBlock_uScript_LockBlock_228.Out)
		{
			Relay_In_225();
		}
	}

	private void Relay_In_229()
	{
		logic_uScript_LockBlock_block_229 = local_215_TankBlock;
		logic_uScript_LockBlock_uScript_LockBlock_229.In(logic_uScript_LockBlock_block_229, logic_uScript_LockBlock_functionalityToLock_229);
		if (logic_uScript_LockBlock_uScript_LockBlock_229.Out)
		{
			Relay_In_219();
		}
	}

	private void Relay_In_230()
	{
		logic_uScript_GetNamedBlock_name_230 = local_ScrapBlock2Name_System_String;
		logic_uScript_GetNamedBlock_owner_230 = owner_Connection_218;
		logic_uScript_GetNamedBlock_Return_230 = logic_uScript_GetNamedBlock_uScript_GetNamedBlock_230.In(logic_uScript_GetNamedBlock_name_230, logic_uScript_GetNamedBlock_owner_230);
		local_224_TankBlock = logic_uScript_GetNamedBlock_Return_230;
		if (logic_uScript_GetNamedBlock_uScript_GetNamedBlock_230.Out)
		{
			Relay_In_226();
		}
	}

	private void Relay_In_231()
	{
		logic_uScriptCon_CompareBool_Bool_231 = local_CanSuckUpScrapBlock2_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_231.In(logic_uScriptCon_CompareBool_Bool_231);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_231.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_231.False;
		if (num)
		{
			Relay_In_217();
		}
		if (flag)
		{
			Relay_In_230();
		}
	}

	private void Relay_True_234()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_234.True(out logic_uScriptAct_SetBool_Target_234);
		local_CanSuckUpScrapBlock1_System_Boolean = logic_uScriptAct_SetBool_Target_234;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_234.Out)
		{
			Relay_In_248();
		}
	}

	private void Relay_False_234()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_234.False(out logic_uScriptAct_SetBool_Target_234);
		local_CanSuckUpScrapBlock1_System_Boolean = logic_uScriptAct_SetBool_Target_234;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_234.Out)
		{
			Relay_In_248();
		}
	}

	private void Relay_True_235()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_235.True(out logic_uScriptAct_SetBool_Target_235);
		local_CanSuckUpScrapBlock2_System_Boolean = logic_uScriptAct_SetBool_Target_235;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_235.Out)
		{
			Relay_In_176();
			Relay_In_270();
		}
	}

	private void Relay_False_235()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_235.False(out logic_uScriptAct_SetBool_Target_235);
		local_CanSuckUpScrapBlock2_System_Boolean = logic_uScriptAct_SetBool_Target_235;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_235.Out)
		{
			Relay_In_176();
			Relay_In_270();
		}
	}

	private void Relay_True_237()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_237.True(out logic_uScriptAct_SetBool_Target_237);
		local_CanSuckUpScrapBlock2_System_Boolean = logic_uScriptAct_SetBool_Target_237;
	}

	private void Relay_False_237()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_237.False(out logic_uScriptAct_SetBool_Target_237);
		local_CanSuckUpScrapBlock2_System_Boolean = logic_uScriptAct_SetBool_Target_237;
	}

	private void Relay_Output1_239()
	{
		Relay_In_241();
	}

	private void Relay_Output2_239()
	{
		Relay_In_162();
	}

	private void Relay_Output3_239()
	{
	}

	private void Relay_Output4_239()
	{
	}

	private void Relay_Output5_239()
	{
	}

	private void Relay_Output6_239()
	{
	}

	private void Relay_Output7_239()
	{
	}

	private void Relay_Output8_239()
	{
	}

	private void Relay_In_239()
	{
		logic_uScriptCon_ManualSwitch_CurrentOutput_239 = local_Stage2_System_Int32;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_239.In(logic_uScriptCon_ManualSwitch_CurrentOutput_239);
	}

	private void Relay_In_241()
	{
		logic_uScript_AddMessage_messageData_241 = msg11ScrappingBlock2_InProgress;
		logic_uScript_AddMessage_speaker_241 = messageSpeaker;
		logic_uScript_AddMessage_Return_241 = logic_uScript_AddMessage_uScript_AddMessage_241.In(logic_uScript_AddMessage_messageData_241, logic_uScript_AddMessage_speaker_241);
		if (logic_uScript_AddMessage_uScript_AddMessage_241.Shown)
		{
			Relay_In_245();
		}
	}

	private void Relay_In_245()
	{
		logic_uScriptAct_AddInt_v2_A_245 = local_Stage2_System_Int32;
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_245.In(logic_uScriptAct_AddInt_v2_A_245, logic_uScriptAct_AddInt_v2_B_245, out logic_uScriptAct_AddInt_v2_IntResult_245, out logic_uScriptAct_AddInt_v2_FloatResult_245);
		local_Stage2_System_Int32 = logic_uScriptAct_AddInt_v2_IntResult_245;
	}

	private void Relay_Save_Out_246()
	{
		Relay_Save_17();
	}

	private void Relay_Load_Out_246()
	{
		Relay_Load_17();
	}

	private void Relay_Restart_Out_246()
	{
		Relay_Set_False_17();
	}

	private void Relay_Save_246()
	{
		logic_SubGraph_SaveLoadInt_integer_246 = local_Stage2_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_246 = local_Stage2_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_246.Save(logic_SubGraph_SaveLoadInt_restartValue_246, ref logic_SubGraph_SaveLoadInt_integer_246, logic_SubGraph_SaveLoadInt_intAsVariable_246, logic_SubGraph_SaveLoadInt_uniqueID_246);
	}

	private void Relay_Load_246()
	{
		logic_SubGraph_SaveLoadInt_integer_246 = local_Stage2_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_246 = local_Stage2_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_246.Load(logic_SubGraph_SaveLoadInt_restartValue_246, ref logic_SubGraph_SaveLoadInt_integer_246, logic_SubGraph_SaveLoadInt_intAsVariable_246, logic_SubGraph_SaveLoadInt_uniqueID_246);
	}

	private void Relay_Restart_246()
	{
		logic_SubGraph_SaveLoadInt_integer_246 = local_Stage2_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_246 = local_Stage2_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_246.Restart(logic_SubGraph_SaveLoadInt_restartValue_246, ref logic_SubGraph_SaveLoadInt_integer_246, logic_SubGraph_SaveLoadInt_intAsVariable_246, logic_SubGraph_SaveLoadInt_uniqueID_246);
	}

	private void Relay_In_248()
	{
		logic_uScript_GetScrapperBlockBeingScrapped_craftingBlock_248 = local_ScrapperBlock_TankBlock;
		logic_uScript_GetScrapperBlockBeingScrapped_blockFaction_248 = local_250_FactionSubTypes;
		logic_uScript_GetScrapperBlockBeingScrapped_Return_248 = logic_uScript_GetScrapperBlockBeingScrapped_uScript_GetScrapperBlockBeingScrapped_248.In(logic_uScript_GetScrapperBlockBeingScrapped_craftingBlock_248, ref logic_uScript_GetScrapperBlockBeingScrapped_blockFaction_248);
		local_250_FactionSubTypes = logic_uScript_GetScrapperBlockBeingScrapped_blockFaction_248;
		if (logic_uScript_GetScrapperBlockBeingScrapped_uScript_GetScrapperBlockBeingScrapped_248.IsOperating)
		{
			Relay_In_251();
		}
	}

	private void Relay_In_251()
	{
		logic_uScript_CompareFactionSubTypes_A_251 = local_250_FactionSubTypes;
		logic_uScript_CompareFactionSubTypes_uScript_CompareFactionSubTypes_251.In(logic_uScript_CompareFactionSubTypes_A_251, logic_uScript_CompareFactionSubTypes_B_251);
		bool equalTo = logic_uScript_CompareFactionSubTypes_uScript_CompareFactionSubTypes_251.EqualTo;
		bool notEqualTo = logic_uScript_CompareFactionSubTypes_uScript_CompareFactionSubTypes_251.NotEqualTo;
		if (equalTo)
		{
			Relay_In_16();
		}
		if (notEqualTo)
		{
			Relay_In_254();
		}
	}

	private void Relay_Save_Out_252()
	{
		Relay_Save_170();
	}

	private void Relay_Load_Out_252()
	{
		Relay_Load_170();
	}

	private void Relay_Restart_Out_252()
	{
		Relay_Set_False_170();
	}

	private void Relay_Save_252()
	{
		logic_SubGraph_SaveLoadBool_boolean_252 = local_ScrappedOtherCorpBlockEarly_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_252 = local_ScrappedOtherCorpBlockEarly_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_252.Save(ref logic_SubGraph_SaveLoadBool_boolean_252, logic_SubGraph_SaveLoadBool_boolAsVariable_252, logic_SubGraph_SaveLoadBool_uniqueID_252);
	}

	private void Relay_Load_252()
	{
		logic_SubGraph_SaveLoadBool_boolean_252 = local_ScrappedOtherCorpBlockEarly_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_252 = local_ScrappedOtherCorpBlockEarly_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_252.Load(ref logic_SubGraph_SaveLoadBool_boolean_252, logic_SubGraph_SaveLoadBool_boolAsVariable_252, logic_SubGraph_SaveLoadBool_uniqueID_252);
	}

	private void Relay_Set_True_252()
	{
		logic_SubGraph_SaveLoadBool_boolean_252 = local_ScrappedOtherCorpBlockEarly_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_252 = local_ScrappedOtherCorpBlockEarly_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_252.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_252, logic_SubGraph_SaveLoadBool_boolAsVariable_252, logic_SubGraph_SaveLoadBool_uniqueID_252);
	}

	private void Relay_Set_False_252()
	{
		logic_SubGraph_SaveLoadBool_boolean_252 = local_ScrappedOtherCorpBlockEarly_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_252 = local_ScrappedOtherCorpBlockEarly_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_252.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_252, logic_SubGraph_SaveLoadBool_boolAsVariable_252, logic_SubGraph_SaveLoadBool_uniqueID_252);
	}

	private void Relay_In_254()
	{
		logic_uScript_HideArrow_uScript_HideArrow_254.In();
		if (logic_uScript_HideArrow_uScript_HideArrow_254.Out)
		{
			Relay_In_256();
		}
	}

	private void Relay_In_256()
	{
		logic_uScript_EnableGlow_targetObject_256 = local_ScrapperBlock_TankBlock;
		logic_uScript_EnableGlow_uScript_EnableGlow_256.In(logic_uScript_EnableGlow_targetObject_256, logic_uScript_EnableGlow_enable_256);
		if (logic_uScript_EnableGlow_uScript_EnableGlow_256.Out)
		{
			Relay_In_257();
		}
	}

	private void Relay_In_257()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_257.In(logic_uScriptAct_SetInt_Value_257, out logic_uScriptAct_SetInt_Target_257);
		local_Stage_System_Int32 = logic_uScriptAct_SetInt_Target_257;
	}

	private void Relay_Out_264()
	{
	}

	private void Relay_In_264()
	{
		logic_SubGraph_Crafting_Tutorial_Finish_craftingBaseTech_264 = local_CraftingBaseTech_Tank;
		logic_SubGraph_Crafting_Tutorial_Finish_NPCTech_264 = local_NPCTech_Tank;
		logic_SubGraph_Crafting_Tutorial_Finish_NPCFlyAwayAI_264 = NPCFlyAwayAI;
		logic_SubGraph_Crafting_Tutorial_Finish_NPCDespawnParticleEffect_264 = NPCDespawnParticleEffect;
		logic_SubGraph_Crafting_Tutorial_Finish_SubGraph_Crafting_Tutorial_Finish_264.In(logic_SubGraph_Crafting_Tutorial_Finish_craftingBaseTech_264, logic_SubGraph_Crafting_Tutorial_Finish_NPCTech_264, logic_SubGraph_Crafting_Tutorial_Finish_NPCFlyAwayAI_264, logic_SubGraph_Crafting_Tutorial_Finish_NPCDespawnParticleEffect_264);
	}

	private void Relay_In_265()
	{
		logic_uScript_AddMessage_messageData_265 = msg14AltComplete;
		logic_uScript_AddMessage_speaker_265 = messageSpeaker;
		logic_uScript_AddMessage_Return_265 = logic_uScript_AddMessage_uScript_AddMessage_265.In(logic_uScript_AddMessage_messageData_265, logic_uScript_AddMessage_speaker_265);
		if (logic_uScript_AddMessage_uScript_AddMessage_265.Shown)
		{
			Relay_In_264();
		}
	}

	private void Relay_In_270()
	{
		logic_uScript_GetScrapperBlockBeingScrapped_craftingBlock_270 = local_ScrapperBlock_TankBlock;
		logic_uScript_GetScrapperBlockBeingScrapped_blockFaction_270 = local_272_FactionSubTypes;
		logic_uScript_GetScrapperBlockBeingScrapped_Return_270 = logic_uScript_GetScrapperBlockBeingScrapped_uScript_GetScrapperBlockBeingScrapped_270.In(logic_uScript_GetScrapperBlockBeingScrapped_craftingBlock_270, ref logic_uScript_GetScrapperBlockBeingScrapped_blockFaction_270);
		local_272_FactionSubTypes = logic_uScript_GetScrapperBlockBeingScrapped_blockFaction_270;
		if (logic_uScript_GetScrapperBlockBeingScrapped_uScript_GetScrapperBlockBeingScrapped_270.IsOperating)
		{
			Relay_In_276();
		}
	}

	private void Relay_In_271()
	{
		logic_uScript_EnableGlow_targetObject_271 = local_ScrapperBlock_TankBlock;
		logic_uScript_EnableGlow_uScript_EnableGlow_271.In(logic_uScript_EnableGlow_targetObject_271, logic_uScript_EnableGlow_enable_271);
		if (logic_uScript_EnableGlow_uScript_EnableGlow_271.Out)
		{
			Relay_True_278();
		}
	}

	private void Relay_In_274()
	{
		logic_uScript_HideArrow_uScript_HideArrow_274.In();
		if (logic_uScript_HideArrow_uScript_HideArrow_274.Out)
		{
			Relay_In_271();
		}
	}

	private void Relay_In_276()
	{
		logic_uScript_CompareFactionSubTypes_A_276 = local_272_FactionSubTypes;
		logic_uScript_CompareFactionSubTypes_uScript_CompareFactionSubTypes_276.In(logic_uScript_CompareFactionSubTypes_A_276, logic_uScript_CompareFactionSubTypes_B_276);
		bool equalTo = logic_uScript_CompareFactionSubTypes_uScript_CompareFactionSubTypes_276.EqualTo;
		bool notEqualTo = logic_uScript_CompareFactionSubTypes_uScript_CompareFactionSubTypes_276.NotEqualTo;
		if (equalTo)
		{
			Relay_In_283();
		}
		if (notEqualTo)
		{
			Relay_In_274();
		}
	}

	private void Relay_True_278()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_278.True(out logic_uScriptAct_SetBool_Target_278);
		local_OtherCorpBlockInScrapper_System_Boolean = logic_uScriptAct_SetBool_Target_278;
	}

	private void Relay_False_278()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_278.False(out logic_uScriptAct_SetBool_Target_278);
		local_OtherCorpBlockInScrapper_System_Boolean = logic_uScriptAct_SetBool_Target_278;
	}

	private void Relay_In_280()
	{
		logic_uScript_EnableGlow_targetObject_280 = local_ScrapperBlock_TankBlock;
		logic_uScript_EnableGlow_uScript_EnableGlow_280.In(logic_uScript_EnableGlow_targetObject_280, logic_uScript_EnableGlow_enable_280);
		if (logic_uScript_EnableGlow_uScript_EnableGlow_280.Out)
		{
			Relay_In_281();
		}
	}

	private void Relay_In_281()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_281.In(logic_uScriptAct_SetInt_Value_281, out logic_uScriptAct_SetInt_Target_281);
		local_Stage_System_Int32 = logic_uScriptAct_SetInt_Target_281;
	}

	private void Relay_In_283()
	{
		logic_uScript_HideArrow_uScript_HideArrow_283.In();
		if (logic_uScript_HideArrow_uScript_HideArrow_283.Out)
		{
			Relay_In_280();
		}
	}

	private void Relay_In_284()
	{
		logic_uScriptCon_CompareBool_Bool_284 = local_ScrappedOtherCorpBlockEarly_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_284.In(logic_uScriptCon_CompareBool_Bool_284);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_284.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_284.False;
		if (num)
		{
			Relay_In_265();
		}
		if (flag)
		{
			Relay_True_285();
		}
	}

	private void Relay_True_285()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_285.True(out logic_uScriptAct_SetBool_Target_285);
		local_ScrappedOtherCorpBlockEarly_System_Boolean = logic_uScriptAct_SetBool_Target_285;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_285.Out)
		{
			Relay_In_265();
		}
	}

	private void Relay_False_285()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_285.False(out logic_uScriptAct_SetBool_Target_285);
		local_ScrappedOtherCorpBlockEarly_System_Boolean = logic_uScriptAct_SetBool_Target_285;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_285.Out)
		{
			Relay_In_265();
		}
	}

	private void Relay_In_630()
	{
		logic_uScript_DirectEnemiesOutOfEncounter_ownerNode_630 = owner_Connection_631;
		logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_630.In(logic_uScript_DirectEnemiesOutOfEncounter_ownerNode_630);
		if (logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_630.Out)
		{
			Relay_In_4();
		}
	}
}
