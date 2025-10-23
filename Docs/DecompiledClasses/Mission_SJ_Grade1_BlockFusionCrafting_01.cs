using System;
using BehaviorDesigner.Runtime;
using UnityEngine;

[Serializable]
[NodePath("Graphs")]
[FriendlyName("Untitled", "")]
public class Mission_SJ_Grade1_BlockFusionCrafting_01 : uScriptLogic
{
	private GameObject parentGameObject;

	private uScript_GUI thisScriptsOnGuiListener;

	private bool m_RegisteredForEvents;

	[Multiline(3)]
	public string basePosition = "";

	public SpawnTechData[] baseSpawnData = new SpawnTechData[0];

	public SpawnBlockData[] blockSpawnData = new SpawnBlockData[0];

	public SpawnBlockData[] blockSpawnDataFusionMachine = new SpawnBlockData[0];

	public BlockTypes BlockTypeFusionMachine;

	public ItemTypeInfo blockTypeToHighlight;

	public float clearSceneryRadius;

	public TankPreset completedBasePreset;

	public float distBaseFound;

	public GhostBlockSpawnData[] ghostBlockFusionMachine = new GhostBlockSpawnData[0];

	private string local_124_System_String = "";

	private string local_127_System_String = "Open: ";

	private TankBlock local_151_TankBlock;

	private TankBlock local_160_TankBlock;

	private SpawnBlockData local_174_SpawnBlockData;

	private TankBlock[] local_176_TankBlockArray = new TankBlock[0];

	private string local_189_System_String = "";

	private SpawnBlockData local_208_SpawnBlockData;

	private string local_212_System_String = "";

	private TankBlock[] local_229_TankBlockArray = new TankBlock[0];

	private bool local_allowAnchoring_System_Boolean;

	private bool local_BlockCrafted_System_Boolean;

	private bool local_CanInteractWithFusionMachine_System_Boolean;

	private bool local_CanSuckUpBlocks_System_Boolean;

	private bool local_CraftingInProgress_System_Boolean;

	private ManHUD.HUDElementType local_CraftingMenu_ManHUD_HUDElementType = ManHUD.HUDElementType.BlockRecipeSelect;

	private bool local_CraftingMenuOpened_System_Boolean;

	private TankBlock local_FusionBlock1_TankBlock;

	private string local_FusionBlock1Name_System_String = "FusionBlock1";

	private TankBlock local_FusionBlock2_TankBlock;

	private string local_FusionBlock2Name_System_String = "FusionBlock2";

	private Tank local_FusionCraftingBaseTech_Tank;

	private TankBlock local_FusionMachineBlock_TankBlock;

	private bool local_FusionMachineSpawned_System_Boolean;

	private bool local_IsFusionMachineAttached_System_Boolean;

	private bool local_msgBaseFoundShown_System_Boolean;

	private bool local_msgFusionMachineSpawnedShown_System_Boolean;

	private bool local_msgIntroShown_System_Boolean;

	private bool local_NearBase_System_Boolean;

	private Tank local_NPCTech_Tank;

	private int local_Stage_System_Int32 = 1;

	private int local_Stage2_System_Int32 = 1;

	private int local_Stage3_System_Int32 = 1;

	public uScript_AddMessage.MessageSpeaker messageSpeaker;

	[Multiline(3)]
	public string messageTag = "";

	public uScript_AddMessage.MessageData msg01Intro;

	public uScript_AddMessage.MessageData msg02BaseFound;

	public uScript_AddMessage.MessageData msg08FusionMachineSpawned;

	public uScript_AddMessage.MessageData msg09AttachFusionMachine;

	public uScript_AddMessage.MessageData msg10OpenMenu;

	public uScript_AddMessage.MessageData msg10OpenMenu_Pad;

	public uScript_AddMessage.MessageData msg11CraftBlock;

	public uScript_AddMessage.MessageData msg11CraftBlock_Pad;

	public uScript_AddMessage.MessageData msg13CraftingInProgress;

	public uScript_AddMessage.MessageData msg14Complete;

	public uScript_AddMessage.MessageData msgBlockOutsideArea;

	public uScript_AddMessage.MessageData msgLeavingMissionArea;

	public Transform NPCDespawnParticleEffect;

	public ExternalBehaviorTree NPCFlyAwayAI;

	public SpawnTechData[] NPCSpawnData = new SpawnTechData[0];

	private GameObject owner_Connection_11;

	private GameObject owner_Connection_21;

	private GameObject owner_Connection_54;

	private GameObject owner_Connection_58;

	private GameObject owner_Connection_70;

	private GameObject owner_Connection_150;

	private GameObject owner_Connection_163;

	private GameObject owner_Connection_177;

	private GameObject owner_Connection_179;

	private GameObject owner_Connection_203;

	private GameObject owner_Connection_225;

	private GameObject owner_Connection_309;

	private GameObject owner_Connection_321;

	private uScript_SetEncounterTarget logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_1 = new uScript_SetEncounterTarget();

	private GameObject logic_uScript_SetEncounterTarget_owner_1;

	private object logic_uScript_SetEncounterTarget_visibleObject_1 = "";

	private bool logic_uScript_SetEncounterTarget_Out_1 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_3 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_3 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_3 = true;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_3 = true;

	private uScript_GetAndCheckBlocks logic_uScript_GetAndCheckBlocks_uScript_GetAndCheckBlocks_4 = new uScript_GetAndCheckBlocks();

	private SpawnBlockData[] logic_uScript_GetAndCheckBlocks_blockData_4 = new SpawnBlockData[0];

	private GameObject logic_uScript_GetAndCheckBlocks_ownerNode_4;

	private TankBlock[] logic_uScript_GetAndCheckBlocks_blocks_4 = new TankBlock[0];

	private bool logic_uScript_GetAndCheckBlocks_AllAlive_4 = true;

	private bool logic_uScript_GetAndCheckBlocks_SomeAlive_4 = true;

	private bool logic_uScript_GetAndCheckBlocks_AllDead_4 = true;

	private bool logic_uScript_GetAndCheckBlocks_WaitingToSpawn_4 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_6 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_6;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_6 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_6 = "msgIntroShown";

	private SubGraph_CompleteObjectiveStage logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_9 = new SubGraph_CompleteObjectiveStage();

	private int logic_SubGraph_CompleteObjectiveStage_objectiveStage_9;

	private bool logic_SubGraph_CompleteObjectiveStage_isFinalObjective_9;

	private SubGraph_SaveLoadInt logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_16 = new SubGraph_SaveLoadInt();

	private int logic_SubGraph_SaveLoadInt_restartValue_16 = 1;

	private int logic_SubGraph_SaveLoadInt_integer_16;

	private object logic_SubGraph_SaveLoadInt_intAsVariable_16 = "";

	private string logic_SubGraph_SaveLoadInt_uniqueID_16 = "Stage";

	private uScript_HideArrow logic_uScript_HideArrow_uScript_HideArrow_17 = new uScript_HideArrow();

	private bool logic_uScript_HideArrow_Out_17 = true;

	private uScript_PausePopulation logic_uScript_PausePopulation_uScript_PausePopulation_20 = new uScript_PausePopulation();

	private bool logic_uScript_PausePopulation_Out_20 = true;

	private SubGraph_Crafting_Tutorial_AttachBlockToBase logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_SubGraph_Crafting_Tutorial_AttachBlockToBase_24 = new SubGraph_Crafting_Tutorial_AttachBlockToBase();

	private TankBlock logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_Block_24;

	private Tank logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_CraftingBaseTech_24;

	private GhostBlockSpawnData[] logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_ghostBlockData_24 = new GhostBlockSpawnData[0];

	private BlockTypes logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_BlockType_24;

	private Vector3 logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_BlockPosition_24 = new Vector3(1f, 1f, -2f);

	private uScriptCon_ManualSwitch logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_25 = new uScriptCon_ManualSwitch();

	private int logic_uScriptCon_ManualSwitch_CurrentOutput_25;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_32 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_32;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_32 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_32 = "msgBaseFoundShown";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_35 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_35;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_35 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_35 = "FusionMachineIsAttached";

	private SubGraph_Crafting_Tutorial_ManageBlock logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_36 = new SubGraph_Crafting_Tutorial_ManageBlock();

	private SpawnBlockData[] logic_SubGraph_Crafting_Tutorial_ManageBlock_blockSpawnData_36 = new SpawnBlockData[0];

	private int logic_SubGraph_Crafting_Tutorial_ManageBlock_blockIndex_36;

	private TankBlock logic_SubGraph_Crafting_Tutorial_ManageBlock_block_36;

	private bool logic_SubGraph_Crafting_Tutorial_ManageBlock_allowAnchoring_36;

	private uScript_AddMessage.MessageData logic_SubGraph_Crafting_Tutorial_ManageBlock_msgBlockOutsideArea_36;

	private uScript_AddMessage.MessageSpeaker logic_SubGraph_Crafting_Tutorial_ManageBlock_messageSpeaker_36;

	private uScript_IsPlayerInRangeOfTech logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_37 = new uScript_IsPlayerInRangeOfTech();

	private Tank logic_uScript_IsPlayerInRangeOfTech_tech_37;

	private float logic_uScript_IsPlayerInRangeOfTech_range_37 = 100f;

	private Tank[] logic_uScript_IsPlayerInRangeOfTech_techs_37 = new Tank[0];

	private bool logic_uScript_IsPlayerInRangeOfTech_Out_37 = true;

	private bool logic_uScript_IsPlayerInRangeOfTech_InRange_37 = true;

	private bool logic_uScript_IsPlayerInRangeOfTech_OutOfRange_37 = true;

	private uScript_SpawnBlocksFromData logic_uScript_SpawnBlocksFromData_uScript_SpawnBlocksFromData_39 = new uScript_SpawnBlocksFromData();

	private SpawnBlockData[] logic_uScript_SpawnBlocksFromData_blockData_39 = new SpawnBlockData[0];

	private GameObject logic_uScript_SpawnBlocksFromData_ownerNode_39;

	private bool logic_uScript_SpawnBlocksFromData_Out_39 = true;

	private uScript_PausePopulation logic_uScript_PausePopulation_uScript_PausePopulation_40 = new uScript_PausePopulation();

	private bool logic_uScript_PausePopulation_Out_40 = true;

	private uScript_IsPlayerInRangeOfTech logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_41 = new uScript_IsPlayerInRangeOfTech();

	private Tank logic_uScript_IsPlayerInRangeOfTech_tech_41;

	private float logic_uScript_IsPlayerInRangeOfTech_range_41;

	private Tank[] logic_uScript_IsPlayerInRangeOfTech_techs_41 = new Tank[0];

	private bool logic_uScript_IsPlayerInRangeOfTech_Out_41 = true;

	private bool logic_uScript_IsPlayerInRangeOfTech_InRange_41 = true;

	private bool logic_uScript_IsPlayerInRangeOfTech_OutOfRange_41 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_43 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_43;

	private bool logic_uScriptAct_SetBool_Out_43 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_43 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_43 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_44 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_44;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_44 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_44 = "msgFusionMachineSpawnedShown";

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_45 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_45;

	private bool logic_uScriptCon_CompareBool_True_45 = true;

	private bool logic_uScriptCon_CompareBool_False_45 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_49 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_49;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_49 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_49 = "FusionMachineSpawned";

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_50 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_50;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_50;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_50;

	private bool logic_uScript_AddMessage_Out_50 = true;

	private bool logic_uScript_AddMessage_Shown_50 = true;

	private SubGraph_CompleteObjectiveStage logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_51 = new SubGraph_CompleteObjectiveStage();

	private int logic_SubGraph_CompleteObjectiveStage_objectiveStage_51;

	private bool logic_SubGraph_CompleteObjectiveStage_isFinalObjective_51;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_55 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_55;

	private bool logic_uScriptAct_SetBool_Out_55 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_55 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_55 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_56 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_56 = true;

	private uScript_LockTechInteraction logic_uScript_LockTechInteraction_uScript_LockTechInteraction_60 = new uScript_LockTechInteraction();

	private Tank logic_uScript_LockTechInteraction_tech_60;

	private BlockTypes[] logic_uScript_LockTechInteraction_excludedBlocks_60 = new BlockTypes[0];

	private TankBlock[] logic_uScript_LockTechInteraction_excludedUniqueBlocks_60 = new TankBlock[0];

	private bool logic_uScript_LockTechInteraction_Out_60 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_64 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_64;

	private bool logic_uScriptAct_SetBool_Out_64 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_64 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_64 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_65 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_65;

	private bool logic_uScriptAct_SetBool_Out_65 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_65 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_65 = true;

	private uScript_LockTechStacks logic_uScript_LockTechStacks_uScript_LockTechStacks_67 = new uScript_LockTechStacks();

	private Tank logic_uScript_LockTechStacks_tech_67;

	private bool logic_uScript_LockTechStacks_Out_67 = true;

	private SubGraph_Crafting_Tutorial_Init logic_SubGraph_Crafting_Tutorial_Init_SubGraph_Crafting_Tutorial_Init_68 = new SubGraph_Crafting_Tutorial_Init();

	private SpawnTechData[] logic_SubGraph_Crafting_Tutorial_Init_baseSpawnData_68 = new SpawnTechData[0];

	private SpawnBlockData[] logic_SubGraph_Crafting_Tutorial_Init_blockSpawnData_68 = new SpawnBlockData[0];

	private SpawnTechData[] logic_SubGraph_Crafting_Tutorial_Init_NPCSpawnData_68 = new SpawnTechData[0];

	private TankPreset logic_SubGraph_Crafting_Tutorial_Init_completedBasePreset_68;

	private bool logic_SubGraph_Crafting_Tutorial_Init_useNPCAsTutorialTech_68;

	private bool logic_SubGraph_Crafting_Tutorial_Init_spawnBase_68 = true;

	private string logic_SubGraph_Crafting_Tutorial_Init_basePosition_68 = "";

	private float logic_SubGraph_Crafting_Tutorial_Init_clearSceneryRadius_68;

	private Tank logic_SubGraph_Crafting_Tutorial_Init_CraftingBaseTech_68;

	private Tank logic_SubGraph_Crafting_Tutorial_Init_NPCTech_68;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_71 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_71;

	private bool logic_uScriptAct_SetBool_Out_71 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_71 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_71 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_72 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_72;

	private bool logic_uScriptCon_CompareBool_True_72 = true;

	private bool logic_uScriptCon_CompareBool_False_72 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_77 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_77;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_77;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_77;

	private bool logic_uScript_AddMessage_Out_77 = true;

	private bool logic_uScript_AddMessage_Shown_77 = true;

	private SubGraph_LoadObjectiveStates logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_80 = new SubGraph_LoadObjectiveStates();

	private int logic_SubGraph_LoadObjectiveStates_currentObjective_80;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_81 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_81;

	private bool logic_uScriptCon_CompareBool_True_81 = true;

	private bool logic_uScriptCon_CompareBool_False_81 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_83 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_83;

	private bool logic_uScriptCon_CompareBool_True_83 = true;

	private bool logic_uScriptCon_CompareBool_False_83 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_90 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_90;

	private bool logic_uScriptAct_SetBool_Out_90 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_90 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_90 = true;

	private uScript_HideHUDElement logic_uScript_HideHUDElement_uScript_HideHUDElement_94 = new uScript_HideHUDElement();

	private ManHUD.HUDElementType logic_uScript_HideHUDElement_hudElement_94;

	private bool logic_uScript_HideHUDElement_Out_94 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_95 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_95;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_95 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_95 = "CraftingMenuOpened";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_96 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_96;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_96 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_96 = "BlockCrafted";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_97 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_97;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_97 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_97 = "CraftingInProgress";

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_101 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_101;

	private bool logic_uScriptCon_CompareBool_True_101 = true;

	private bool logic_uScriptCon_CompareBool_False_101 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_102 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_102;

	private bool logic_uScriptAct_SetBool_Out_102 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_102 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_102 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_103 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_103;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_103;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_103;

	private bool logic_uScript_AddMessage_Out_103 = true;

	private bool logic_uScript_AddMessage_Shown_103 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_104 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_104 = true;

	private uScript_DisableClosingCraftingUIWhenTooFarAway logic_uScript_DisableClosingCraftingUIWhenTooFarAway_uScript_DisableClosingCraftingUIWhenTooFarAway_106 = new uScript_DisableClosingCraftingUIWhenTooFarAway();

	private TankBlock logic_uScript_DisableClosingCraftingUIWhenTooFarAway_craftingBlock_106;

	private bool logic_uScript_DisableClosingCraftingUIWhenTooFarAway_Out_106 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_108 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_108 = "";

	private bool logic_uScript_EnableGlow_enable_108;

	private bool logic_uScript_EnableGlow_Out_108 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_109 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_109;

	private bool logic_uScriptCon_CompareBool_True_109 = true;

	private bool logic_uScriptCon_CompareBool_False_109 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_110 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_110;

	private bool logic_uScriptCon_CompareBool_True_110 = true;

	private bool logic_uScriptCon_CompareBool_False_110 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_112 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_112;

	private bool logic_uScriptAct_SetBool_Out_112 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_112 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_112 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_118 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_118;

	private bool logic_uScriptCon_CompareBool_True_118 = true;

	private bool logic_uScriptCon_CompareBool_False_118 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_119 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_119 = true;

	private uScriptAct_Concatenate logic_uScriptAct_Concatenate_uScriptAct_Concatenate_123 = new uScriptAct_Concatenate();

	private object[] logic_uScriptAct_Concatenate_A_123 = new object[0];

	private object[] logic_uScriptAct_Concatenate_B_123 = new object[0];

	private string logic_uScriptAct_Concatenate_Separator_123 = "";

	private string logic_uScriptAct_Concatenate_Result_123;

	private bool logic_uScriptAct_Concatenate_Out_123 = true;

	private uScriptAct_PrintText logic_uScriptAct_PrintText_uScriptAct_PrintText_125 = new uScriptAct_PrintText();

	private string logic_uScriptAct_PrintText_Text_125 = "";

	private int logic_uScriptAct_PrintText_FontSize_125 = 16;

	private FontStyle logic_uScriptAct_PrintText_FontStyle_125;

	private Color logic_uScriptAct_PrintText_FontColor_125 = new Color(0f, 0f, 0f, 1f);

	private TextAnchor logic_uScriptAct_PrintText_textAnchor_125;

	private int logic_uScriptAct_PrintText_EdgePadding_125 = 8;

	private float logic_uScriptAct_PrintText_time_125;

	private bool logic_uScriptAct_PrintText_Out_125 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_130 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_130;

	private bool logic_uScriptCon_CompareBool_True_130 = true;

	private bool logic_uScriptCon_CompareBool_False_130 = true;

	private uScript_LockTechInteraction logic_uScript_LockTechInteraction_uScript_LockTechInteraction_131 = new uScript_LockTechInteraction();

	private Tank logic_uScript_LockTechInteraction_tech_131;

	private BlockTypes[] logic_uScript_LockTechInteraction_excludedBlocks_131 = new BlockTypes[0];

	private TankBlock[] logic_uScript_LockTechInteraction_excludedUniqueBlocks_131 = new TankBlock[0];

	private bool logic_uScript_LockTechInteraction_Out_131 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_138 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_138;

	private bool logic_uScriptAct_SetBool_Out_138 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_138 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_138 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_139 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_139;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_139;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_139;

	private bool logic_uScript_AddMessage_Out_139 = true;

	private bool logic_uScript_AddMessage_Shown_139 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_141 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_141;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_141;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_141;

	private bool logic_uScript_AddMessage_Out_141 = true;

	private bool logic_uScript_AddMessage_Shown_141 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_143 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_143;

	private bool logic_uScriptCon_CompareBool_True_143 = true;

	private bool logic_uScriptCon_CompareBool_False_143 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_147 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_147;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_147 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_147 = "CanInteractWithFusionMachine";

	private uScript_GetNamedBlock logic_uScript_GetNamedBlock_uScript_GetNamedBlock_149 = new uScript_GetNamedBlock();

	private string logic_uScript_GetNamedBlock_name_149 = "";

	private GameObject logic_uScript_GetNamedBlock_owner_149;

	private TankBlock logic_uScript_GetNamedBlock_Return_149;

	private bool logic_uScript_GetNamedBlock_Out_149 = true;

	private bool logic_uScript_GetNamedBlock_Destroyed_149 = true;

	private bool logic_uScript_GetNamedBlock_BlockExists_149 = true;

	private bool logic_uScript_GetNamedBlock_WaitingForBlock_149 = true;

	private bool logic_uScript_GetNamedBlock_NoBlock_149 = true;

	private uScript_LockBlock logic_uScript_LockBlock_uScript_LockBlock_152 = new uScript_LockBlock();

	private TankBlock logic_uScript_LockBlock_block_152;

	private Visible.LockTimerTypes logic_uScript_LockBlock_functionalityToLock_152 = Visible.LockTimerTypes.Grabbable;

	private bool logic_uScript_LockBlock_Out_152 = true;

	private uScript_LockBlock logic_uScript_LockBlock_uScript_LockBlock_154 = new uScript_LockBlock();

	private TankBlock logic_uScript_LockBlock_block_154;

	private Visible.LockTimerTypes logic_uScript_LockBlock_functionalityToLock_154 = Visible.LockTimerTypes.StackAccept;

	private bool logic_uScript_LockBlock_Out_154 = true;

	private uScript_LockBlock logic_uScript_LockBlock_uScript_LockBlock_155 = new uScript_LockBlock();

	private TankBlock logic_uScript_LockBlock_block_155;

	private Visible.LockTimerTypes logic_uScript_LockBlock_functionalityToLock_155 = Visible.LockTimerTypes.BlocksAttachable;

	private bool logic_uScript_LockBlock_Out_155 = true;

	private uScript_LockBlock logic_uScript_LockBlock_uScript_LockBlock_156 = new uScript_LockBlock();

	private TankBlock logic_uScript_LockBlock_block_156;

	private Visible.LockTimerTypes logic_uScript_LockBlock_functionalityToLock_156 = Visible.LockTimerTypes.ItemCollection;

	private bool logic_uScript_LockBlock_Out_156 = true;

	private uScript_LockBlock logic_uScript_LockBlock_uScript_LockBlock_157 = new uScript_LockBlock();

	private TankBlock logic_uScript_LockBlock_block_157;

	private Visible.LockTimerTypes logic_uScript_LockBlock_functionalityToLock_157 = Visible.LockTimerTypes.SendToSCU;

	private bool logic_uScript_LockBlock_Out_157 = true;

	private uScript_GetNamedBlock logic_uScript_GetNamedBlock_uScript_GetNamedBlock_158 = new uScript_GetNamedBlock();

	private string logic_uScript_GetNamedBlock_name_158 = "";

	private GameObject logic_uScript_GetNamedBlock_owner_158;

	private TankBlock logic_uScript_GetNamedBlock_Return_158;

	private bool logic_uScript_GetNamedBlock_Out_158 = true;

	private bool logic_uScript_GetNamedBlock_Destroyed_158 = true;

	private bool logic_uScript_GetNamedBlock_BlockExists_158 = true;

	private bool logic_uScript_GetNamedBlock_WaitingForBlock_158 = true;

	private bool logic_uScript_GetNamedBlock_NoBlock_158 = true;

	private uScript_LockBlock logic_uScript_LockBlock_uScript_LockBlock_161 = new uScript_LockBlock();

	private TankBlock logic_uScript_LockBlock_block_161;

	private Visible.LockTimerTypes logic_uScript_LockBlock_functionalityToLock_161 = Visible.LockTimerTypes.ItemCollection;

	private bool logic_uScript_LockBlock_Out_161 = true;

	private uScript_LockBlock logic_uScript_LockBlock_uScript_LockBlock_162 = new uScript_LockBlock();

	private TankBlock logic_uScript_LockBlock_block_162;

	private Visible.LockTimerTypes logic_uScript_LockBlock_functionalityToLock_162 = Visible.LockTimerTypes.BlocksAttachable;

	private bool logic_uScript_LockBlock_Out_162 = true;

	private uScript_LockBlock logic_uScript_LockBlock_uScript_LockBlock_164 = new uScript_LockBlock();

	private TankBlock logic_uScript_LockBlock_block_164;

	private Visible.LockTimerTypes logic_uScript_LockBlock_functionalityToLock_164 = Visible.LockTimerTypes.StackAccept;

	private bool logic_uScript_LockBlock_Out_164 = true;

	private uScript_LockBlock logic_uScript_LockBlock_uScript_LockBlock_165 = new uScript_LockBlock();

	private TankBlock logic_uScript_LockBlock_block_165;

	private Visible.LockTimerTypes logic_uScript_LockBlock_functionalityToLock_165 = Visible.LockTimerTypes.Grabbable;

	private bool logic_uScript_LockBlock_Out_165 = true;

	private uScript_LockBlock logic_uScript_LockBlock_uScript_LockBlock_166 = new uScript_LockBlock();

	private TankBlock logic_uScript_LockBlock_block_166;

	private Visible.LockTimerTypes logic_uScript_LockBlock_functionalityToLock_166 = Visible.LockTimerTypes.SendToSCU;

	private bool logic_uScript_LockBlock_Out_166 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_167 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_167 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_168 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_168 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_169 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_169 = true;

	private SubGraph_SaveLoadInt logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_170 = new SubGraph_SaveLoadInt();

	private int logic_SubGraph_SaveLoadInt_restartValue_170 = 1;

	private int logic_SubGraph_SaveLoadInt_integer_170;

	private object logic_SubGraph_SaveLoadInt_intAsVariable_170 = "";

	private string logic_SubGraph_SaveLoadInt_uniqueID_170 = "Stage2";

	private uScript_GetBlockSpawnDataPositionName logic_uScript_GetBlockSpawnDataPositionName_uScript_GetBlockSpawnDataPositionName_172 = new uScript_GetBlockSpawnDataPositionName();

	private SpawnBlockData logic_uScript_GetBlockSpawnDataPositionName_blockData_172;

	private string logic_uScript_GetBlockSpawnDataPositionName_positionName_172;

	private bool logic_uScript_GetBlockSpawnDataPositionName_Out_172 = true;

	private uScript_LockTutorialBlockAttach logic_uScript_LockTutorialBlockAttach_uScript_LockTutorialBlockAttach_173 = new uScript_LockTutorialBlockAttach();

	private TankBlock logic_uScript_LockTutorialBlockAttach_block_173;

	private bool logic_uScript_LockTutorialBlockAttach_Out_173 = true;

	private uScript_KeepVisibleInEncounterArea logic_uScript_KeepVisibleInEncounterArea_uScript_KeepVisibleInEncounterArea_178 = new uScript_KeepVisibleInEncounterArea();

	private GameObject logic_uScript_KeepVisibleInEncounterArea_ownerNode_178;

	private object logic_uScript_KeepVisibleInEncounterArea_objectToEnclose_178 = "";

	private string logic_uScript_KeepVisibleInEncounterArea_resetPosName_178 = "";

	private Vector3 logic_uScript_KeepVisibleInEncounterArea_positionBeforeReset_178;

	private bool logic_uScript_KeepVisibleInEncounterArea_Out_178 = true;

	private bool logic_uScript_KeepVisibleInEncounterArea_InsideArea_178 = true;

	private bool logic_uScript_KeepVisibleInEncounterArea_ResetFromOutsideArea_178 = true;

	private uScript_AccessListBlock logic_uScript_AccessListBlock_uScript_AccessListBlock_181 = new uScript_AccessListBlock();

	private TankBlock[] logic_uScript_AccessListBlock_blockList_181 = new TankBlock[0];

	private int logic_uScript_AccessListBlock_index_181;

	private TankBlock logic_uScript_AccessListBlock_value_181;

	private bool logic_uScript_AccessListBlock_Out_181 = true;

	private uScript_GetAndCheckBlocks logic_uScript_GetAndCheckBlocks_uScript_GetAndCheckBlocks_185 = new uScript_GetAndCheckBlocks();

	private SpawnBlockData[] logic_uScript_GetAndCheckBlocks_blockData_185 = new SpawnBlockData[0];

	private GameObject logic_uScript_GetAndCheckBlocks_ownerNode_185;

	private TankBlock[] logic_uScript_GetAndCheckBlocks_blocks_185 = new TankBlock[0];

	private bool logic_uScript_GetAndCheckBlocks_AllAlive_185 = true;

	private bool logic_uScript_GetAndCheckBlocks_SomeAlive_185 = true;

	private bool logic_uScript_GetAndCheckBlocks_AllDead_185 = true;

	private bool logic_uScript_GetAndCheckBlocks_WaitingToSpawn_185 = true;

	private uScript_LockBlockAttach logic_uScript_LockBlockAttach_uScript_LockBlockAttach_186 = new uScript_LockBlockAttach();

	private TankBlock logic_uScript_LockBlockAttach_block_186;

	private bool logic_uScript_LockBlockAttach_Out_186 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_190 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_190;

	private bool logic_uScriptCon_CompareBool_True_190 = true;

	private bool logic_uScriptCon_CompareBool_False_190 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_191 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_191 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_192 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_192 = true;

	private uScript_AccessListBlockSpawnData logic_uScript_AccessListBlockSpawnData_uScript_AccessListBlockSpawnData_193 = new uScript_AccessListBlockSpawnData();

	private SpawnBlockData[] logic_uScript_AccessListBlockSpawnData_dataList_193 = new SpawnBlockData[0];

	private int logic_uScript_AccessListBlockSpawnData_index_193;

	private SpawnBlockData logic_uScript_AccessListBlockSpawnData_value_193;

	private bool logic_uScript_AccessListBlockSpawnData_Out_193 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_194 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_194;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_194;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_194;

	private bool logic_uScript_AddMessage_Out_194 = true;

	private bool logic_uScript_AddMessage_Shown_194 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_195 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_195 = true;

	private uScript_CastBlock logic_uScript_CastBlock_uScript_CastBlock_197 = new uScript_CastBlock();

	private TankBlock logic_uScript_CastBlock_block_197;

	private TankBlock logic_uScript_CastBlock_outBlock_197;

	private bool logic_uScript_CastBlock_Out_197 = true;

	private uScript_CastBlock logic_uScript_CastBlock_uScript_CastBlock_200 = new uScript_CastBlock();

	private TankBlock logic_uScript_CastBlock_block_200;

	private TankBlock logic_uScript_CastBlock_outBlock_200;

	private bool logic_uScript_CastBlock_Out_200 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_202 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_202 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_204 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_204;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_204;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_204;

	private bool logic_uScript_AddMessage_Out_204 = true;

	private bool logic_uScript_AddMessage_Shown_204 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_205 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_205;

	private bool logic_uScriptCon_CompareBool_True_205 = true;

	private bool logic_uScriptCon_CompareBool_False_205 = true;

	private uScript_GetAndCheckBlocks logic_uScript_GetAndCheckBlocks_uScript_GetAndCheckBlocks_209 = new uScript_GetAndCheckBlocks();

	private SpawnBlockData[] logic_uScript_GetAndCheckBlocks_blockData_209 = new SpawnBlockData[0];

	private GameObject logic_uScript_GetAndCheckBlocks_ownerNode_209;

	private TankBlock[] logic_uScript_GetAndCheckBlocks_blocks_209 = new TankBlock[0];

	private bool logic_uScript_GetAndCheckBlocks_AllAlive_209 = true;

	private bool logic_uScript_GetAndCheckBlocks_SomeAlive_209 = true;

	private bool logic_uScript_GetAndCheckBlocks_AllDead_209 = true;

	private bool logic_uScript_GetAndCheckBlocks_WaitingToSpawn_209 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_210 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_210 = true;

	private uScript_KeepBlockInvulnerable logic_uScript_KeepBlockInvulnerable_uScript_KeepBlockInvulnerable_213 = new uScript_KeepBlockInvulnerable();

	private TankBlock logic_uScript_KeepBlockInvulnerable_block_213;

	private bool logic_uScript_KeepBlockInvulnerable_Out_213 = true;

	private uScript_KeepVisibleInEncounterArea logic_uScript_KeepVisibleInEncounterArea_uScript_KeepVisibleInEncounterArea_215 = new uScript_KeepVisibleInEncounterArea();

	private GameObject logic_uScript_KeepVisibleInEncounterArea_ownerNode_215;

	private object logic_uScript_KeepVisibleInEncounterArea_objectToEnclose_215 = "";

	private string logic_uScript_KeepVisibleInEncounterArea_resetPosName_215 = "";

	private Vector3 logic_uScript_KeepVisibleInEncounterArea_positionBeforeReset_215;

	private bool logic_uScript_KeepVisibleInEncounterArea_Out_215 = true;

	private bool logic_uScript_KeepVisibleInEncounterArea_InsideArea_215 = true;

	private bool logic_uScript_KeepVisibleInEncounterArea_ResetFromOutsideArea_215 = true;

	private uScript_GetBlockSpawnDataPositionName logic_uScript_GetBlockSpawnDataPositionName_uScript_GetBlockSpawnDataPositionName_216 = new uScript_GetBlockSpawnDataPositionName();

	private SpawnBlockData logic_uScript_GetBlockSpawnDataPositionName_blockData_216;

	private string logic_uScript_GetBlockSpawnDataPositionName_positionName_216;

	private bool logic_uScript_GetBlockSpawnDataPositionName_Out_216 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_218 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_218 = true;

	private uScript_LockTutorialBlockAttach logic_uScript_LockTutorialBlockAttach_uScript_LockTutorialBlockAttach_220 = new uScript_LockTutorialBlockAttach();

	private TankBlock logic_uScript_LockTutorialBlockAttach_block_220;

	private bool logic_uScript_LockTutorialBlockAttach_Out_220 = true;

	private uScript_LockBlockAttach logic_uScript_LockBlockAttach_uScript_LockBlockAttach_222 = new uScript_LockBlockAttach();

	private TankBlock logic_uScript_LockBlockAttach_block_222;

	private bool logic_uScript_LockBlockAttach_Out_222 = true;

	private uScript_AccessListBlockSpawnData logic_uScript_AccessListBlockSpawnData_uScript_AccessListBlockSpawnData_223 = new uScript_AccessListBlockSpawnData();

	private SpawnBlockData[] logic_uScript_AccessListBlockSpawnData_dataList_223 = new SpawnBlockData[0];

	private int logic_uScript_AccessListBlockSpawnData_index_223 = 1;

	private SpawnBlockData logic_uScript_AccessListBlockSpawnData_value_223;

	private bool logic_uScript_AccessListBlockSpawnData_Out_223 = true;

	private uScript_AccessListBlock logic_uScript_AccessListBlock_uScript_AccessListBlock_227 = new uScript_AccessListBlock();

	private TankBlock[] logic_uScript_AccessListBlock_blockList_227 = new TankBlock[0];

	private int logic_uScript_AccessListBlock_index_227 = 1;

	private TankBlock logic_uScript_AccessListBlock_value_227;

	private bool logic_uScript_AccessListBlock_Out_227 = true;

	private uScript_KeepBlockInvulnerable logic_uScript_KeepBlockInvulnerable_uScript_KeepBlockInvulnerable_230 = new uScript_KeepBlockInvulnerable();

	private TankBlock logic_uScript_KeepBlockInvulnerable_block_230;

	private bool logic_uScript_KeepBlockInvulnerable_Out_230 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_233 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_233;

	private bool logic_uScriptCon_CompareBool_True_233 = true;

	private bool logic_uScriptCon_CompareBool_False_233 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_234 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_234 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_235 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_235 = true;

	private uScriptCon_ManualSwitch logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_237 = new uScriptCon_ManualSwitch();

	private int logic_uScriptCon_ManualSwitch_CurrentOutput_237;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_238 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_238;

	private bool logic_uScriptAct_SetBool_Out_238 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_238 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_238 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_240 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_240;

	private bool logic_uScriptCon_CompareBool_True_240 = true;

	private bool logic_uScriptCon_CompareBool_False_240 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_242 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_242;

	private bool logic_uScriptCon_CompareBool_True_242 = true;

	private bool logic_uScriptCon_CompareBool_False_242 = true;

	private uScript_DisableClosingCraftingUIWhenTooFarAway logic_uScript_DisableClosingCraftingUIWhenTooFarAway_uScript_DisableClosingCraftingUIWhenTooFarAway_244 = new uScript_DisableClosingCraftingUIWhenTooFarAway();

	private TankBlock logic_uScript_DisableClosingCraftingUIWhenTooFarAway_craftingBlock_244;

	private bool logic_uScript_DisableClosingCraftingUIWhenTooFarAway_Out_244 = true;

	private uScript_PointArrowAtVisible logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_245 = new uScript_PointArrowAtVisible();

	private object logic_uScript_PointArrowAtVisible_targetObject_245 = "";

	private float logic_uScript_PointArrowAtVisible_timeToShowFor_245 = -1f;

	private Vector3 logic_uScript_PointArrowAtVisible_offset_245 = new Vector3(0f, 0f, 0f);

	private bool logic_uScript_PointArrowAtVisible_Out_245 = true;

	private uScript_IsHUDElementLinkedToBlock logic_uScript_IsHUDElementLinkedToBlock_uScript_IsHUDElementLinkedToBlock_248 = new uScript_IsHUDElementLinkedToBlock();

	private ManHUD.HUDElementType logic_uScript_IsHUDElementLinkedToBlock_hudElement_248;

	private TankBlock logic_uScript_IsHUDElementLinkedToBlock_targetBlock_248;

	private bool logic_uScript_IsHUDElementLinkedToBlock_True_248 = true;

	private bool logic_uScript_IsHUDElementLinkedToBlock_False_248 = true;

	private SubGraph_AddMessageWithPadSupport logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_249 = new SubGraph_AddMessageWithPadSupport();

	private uScript_AddMessage.MessageData logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboard_249;

	private uScript_AddMessage.MessageData logic_SubGraph_AddMessageWithPadSupport_messageControlPad_249;

	private uScript_AddMessage.MessageSpeaker logic_SubGraph_AddMessageWithPadSupport_speaker_249;

	private ManOnScreenMessages.OnScreenMessage logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboardReturn_249;

	private ManOnScreenMessages.OnScreenMessage logic_SubGraph_AddMessageWithPadSupport_messageControlPadReturn_249;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_254 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_254 = "";

	private bool logic_uScript_EnableGlow_enable_254 = true;

	private bool logic_uScript_EnableGlow_Out_254 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_257 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_257 = "";

	private bool logic_uScript_EnableGlow_enable_257;

	private bool logic_uScript_EnableGlow_Out_257 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_258 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_258;

	private bool logic_uScriptAct_SetBool_Out_258 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_258 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_258 = true;

	private uScript_HideArrow logic_uScript_HideArrow_uScript_HideArrow_261 = new uScript_HideArrow();

	private bool logic_uScript_HideArrow_Out_261 = true;

	private uScript_LockHudGroup logic_uScript_LockHudGroup_uScript_LockHudGroup_263 = new uScript_LockHudGroup();

	private ManHUD.HUDGroup logic_uScript_LockHudGroup_group_263;

	private bool logic_uScript_LockHudGroup_locked_263 = true;

	private bool logic_uScript_LockHudGroup_Out_263 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_268 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_268 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_268 = true;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_268 = true;

	private uScript_LockPlayerInput logic_uScript_LockPlayerInput_uScript_LockPlayerInput_270 = new uScript_LockPlayerInput();

	private bool logic_uScript_LockPlayerInput_lockInput_270 = true;

	private bool logic_uScript_LockPlayerInput_includeCamera_270 = true;

	private bool logic_uScript_LockPlayerInput_Out_270 = true;

	private SubGraph_AddMessageWithPadSupport logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_273 = new SubGraph_AddMessageWithPadSupport();

	private uScript_AddMessage.MessageData logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboard_273;

	private uScript_AddMessage.MessageData logic_SubGraph_AddMessageWithPadSupport_messageControlPad_273;

	private uScript_AddMessage.MessageSpeaker logic_SubGraph_AddMessageWithPadSupport_speaker_273;

	private ManOnScreenMessages.OnScreenMessage logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboardReturn_273;

	private ManOnScreenMessages.OnScreenMessage logic_SubGraph_AddMessageWithPadSupport_messageControlPadReturn_273;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_274 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_274;

	private bool logic_uScriptAct_SetBool_Out_274 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_274 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_274 = true;

	private uScript_CraftingUIHighlightCraftButton logic_uScript_CraftingUIHighlightCraftButton_uScript_CraftingUIHighlightCraftButton_276 = new uScript_CraftingUIHighlightCraftButton();

	private ManHUD.HUDElementType logic_uScript_CraftingUIHighlightCraftButton_targetMenuType_276 = ManHUD.HUDElementType.BlockRecipeSelect;

	private bool logic_uScript_CraftingUIHighlightCraftButton_Out_276 = true;

	private bool logic_uScript_CraftingUIHighlightCraftButton_Waiting_276 = true;

	private bool logic_uScript_CraftingUIHighlightCraftButton_Selected_276 = true;

	private uScript_LockPause logic_uScript_LockPause_uScript_LockPause_277 = new uScript_LockPause();

	private bool logic_uScript_LockPause_lockPause_277 = true;

	private ManPauseGame.DisablePauseReason logic_uScript_LockPause_disabledReason_277 = ManPauseGame.DisablePauseReason.Tutorial;

	private bool logic_uScript_LockPause_Out_277 = true;

	private uScript_DisableClosingCraftingUIWhenTooFarAway logic_uScript_DisableClosingCraftingUIWhenTooFarAway_uScript_DisableClosingCraftingUIWhenTooFarAway_278 = new uScript_DisableClosingCraftingUIWhenTooFarAway();

	private TankBlock logic_uScript_DisableClosingCraftingUIWhenTooFarAway_craftingBlock_278;

	private bool logic_uScript_DisableClosingCraftingUIWhenTooFarAway_Out_278 = true;

	private uScript_LockTechInteraction logic_uScript_LockTechInteraction_uScript_LockTechInteraction_281 = new uScript_LockTechInteraction();

	private Tank logic_uScript_LockTechInteraction_tech_281;

	private BlockTypes[] logic_uScript_LockTechInteraction_excludedBlocks_281 = new BlockTypes[0];

	private TankBlock[] logic_uScript_LockTechInteraction_excludedUniqueBlocks_281 = new TankBlock[0];

	private bool logic_uScript_LockTechInteraction_Out_281 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_282 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_282;

	private bool logic_uScriptAct_SetBool_Out_282 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_282 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_282 = true;

	private uScript_CraftingUIHighlightItem logic_uScript_CraftingUIHighlightItem_uScript_CraftingUIHighlightItem_283 = new uScript_CraftingUIHighlightItem();

	private ManHUD.HUDElementType logic_uScript_CraftingUIHighlightItem_targetMenuType_283 = ManHUD.HUDElementType.BlockRecipeSelect;

	private ItemTypeInfo logic_uScript_CraftingUIHighlightItem_itemToHighlight_283;

	private bool logic_uScript_CraftingUIHighlightItem_Out_283 = true;

	private bool logic_uScript_CraftingUIHighlightItem_Waiting_283 = true;

	private bool logic_uScript_CraftingUIHighlightItem_Selected_283 = true;

	private uScriptAct_AddInt_v2 logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_286 = new uScriptAct_AddInt_v2();

	private int logic_uScriptAct_AddInt_v2_A_286;

	private int logic_uScriptAct_AddInt_v2_B_286 = 1;

	private int logic_uScriptAct_AddInt_v2_IntResult_286;

	private float logic_uScriptAct_AddInt_v2_FloatResult_286;

	private bool logic_uScriptAct_AddInt_v2_Out_286 = true;

	private uScript_LockTechInteraction logic_uScript_LockTechInteraction_uScript_LockTechInteraction_287 = new uScript_LockTechInteraction();

	private Tank logic_uScript_LockTechInteraction_tech_287;

	private BlockTypes[] logic_uScript_LockTechInteraction_excludedBlocks_287 = new BlockTypes[0];

	private TankBlock[] logic_uScript_LockTechInteraction_excludedUniqueBlocks_287 = new TankBlock[0];

	private bool logic_uScript_LockTechInteraction_Out_287 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_290 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_290;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_290;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_290;

	private bool logic_uScript_AddMessage_Out_290 = true;

	private bool logic_uScript_AddMessage_Shown_290 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_291 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_291;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_291;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_291;

	private bool logic_uScript_AddMessage_Out_291 = true;

	private bool logic_uScript_AddMessage_Shown_291 = true;

	private uScript_LockHudGroup logic_uScript_LockHudGroup_uScript_LockHudGroup_292 = new uScript_LockHudGroup();

	private ManHUD.HUDGroup logic_uScript_LockHudGroup_group_292;

	private bool logic_uScript_LockHudGroup_locked_292;

	private bool logic_uScript_LockHudGroup_Out_292 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_293 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_293;

	private bool logic_uScriptCon_CompareBool_True_293 = true;

	private bool logic_uScriptCon_CompareBool_False_293 = true;

	private uScript_CompareBlockTypes logic_uScript_CompareBlockTypes_uScript_CompareBlockTypes_294 = new uScript_CompareBlockTypes();

	private BlockTypes logic_uScript_CompareBlockTypes_A_294;

	private BlockTypes logic_uScript_CompareBlockTypes_B_294;

	private bool logic_uScript_CompareBlockTypes_EqualTo_294 = true;

	private bool logic_uScript_CompareBlockTypes_NotEqualTo_294 = true;

	private uScriptCon_ManualSwitch logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_298 = new uScriptCon_ManualSwitch();

	private int logic_uScriptCon_ManualSwitch_CurrentOutput_298;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_301 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_301;

	private bool logic_uScriptAct_SetBool_Out_301 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_301 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_301 = true;

	private uScript_LockPause logic_uScript_LockPause_uScript_LockPause_302 = new uScript_LockPause();

	private bool logic_uScript_LockPause_lockPause_302;

	private ManPauseGame.DisablePauseReason logic_uScript_LockPause_disabledReason_302 = ManPauseGame.DisablePauseReason.Tutorial;

	private bool logic_uScript_LockPause_Out_302 = true;

	private uScriptAct_AddInt_v2 logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_305 = new uScriptAct_AddInt_v2();

	private int logic_uScriptAct_AddInt_v2_A_305;

	private int logic_uScriptAct_AddInt_v2_B_305 = 1;

	private int logic_uScriptAct_AddInt_v2_IntResult_305;

	private float logic_uScriptAct_AddInt_v2_FloatResult_305;

	private bool logic_uScriptAct_AddInt_v2_Out_305 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_310 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_310;

	private bool logic_uScriptAct_SetBool_Out_310 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_310 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_310 = true;

	private SubGraph_Crafting_Tutorial_Finish logic_SubGraph_Crafting_Tutorial_Finish_SubGraph_Crafting_Tutorial_Finish_316 = new SubGraph_Crafting_Tutorial_Finish();

	private Tank logic_SubGraph_Crafting_Tutorial_Finish_craftingBaseTech_316;

	private Tank logic_SubGraph_Crafting_Tutorial_Finish_NPCTech_316;

	private ExternalBehaviorTree logic_SubGraph_Crafting_Tutorial_Finish_NPCFlyAwayAI_316;

	private Transform logic_SubGraph_Crafting_Tutorial_Finish_NPCDespawnParticleEffect_316;

	private uScript_LockPlayerInput logic_uScript_LockPlayerInput_uScript_LockPlayerInput_317 = new uScript_LockPlayerInput();

	private bool logic_uScript_LockPlayerInput_lockInput_317;

	private bool logic_uScript_LockPlayerInput_includeCamera_317;

	private bool logic_uScript_LockPlayerInput_Out_317 = true;

	private SubGraph_SaveLoadInt logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_318 = new SubGraph_SaveLoadInt();

	private int logic_SubGraph_SaveLoadInt_restartValue_318 = 1;

	private int logic_SubGraph_SaveLoadInt_integer_318;

	private object logic_SubGraph_SaveLoadInt_intAsVariable_318 = "";

	private string logic_SubGraph_SaveLoadInt_uniqueID_318 = "Stage3";

	private uScript_DirectEnemiesOutOfEncounter logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_320 = new uScript_DirectEnemiesOutOfEncounter();

	private GameObject logic_uScript_DirectEnemiesOutOfEncounter_ownerNode_320;

	private bool logic_uScript_DirectEnemiesOutOfEncounter_Out_320 = true;

	private BlockTypes event_UnityEngine_GameObject_BlockType_288;

	private int event_UnityEngine_GameObject_BlockTypeTotal_288;

	private int event_UnityEngine_GameObject_BlockTotal_288;

	private TankBlock event_UnityEngine_GameObject_Block_288;

	private TankBlock event_UnityEngine_GameObject_CrafterBlock_288;

	private void SyncUnityHooks()
	{
		SyncEventListeners();
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
					uScript_SaveLoad2.SaveEvent += Instance_SaveEvent_27;
					uScript_SaveLoad2.LoadEvent += Instance_LoadEvent_27;
					uScript_SaveLoad2.RestartEvent += Instance_RestartEvent_27;
				}
			}
		}
		if (null == owner_Connection_21 || !m_RegisteredForEvents)
		{
			owner_Connection_21 = parentGameObject;
		}
		if (null == owner_Connection_54 || !m_RegisteredForEvents)
		{
			owner_Connection_54 = parentGameObject;
		}
		if (null == owner_Connection_58 || !m_RegisteredForEvents)
		{
			owner_Connection_58 = parentGameObject;
			if (null != owner_Connection_58)
			{
				uScript_EncounterUpdate uScript_EncounterUpdate2 = owner_Connection_58.GetComponent<uScript_EncounterUpdate>();
				if (null == uScript_EncounterUpdate2)
				{
					uScript_EncounterUpdate2 = owner_Connection_58.AddComponent<uScript_EncounterUpdate>();
				}
				if (null != uScript_EncounterUpdate2)
				{
					uScript_EncounterUpdate2.OnUpdate += Instance_OnUpdate_76;
					uScript_EncounterUpdate2.OnSuspend += Instance_OnSuspend_76;
					uScript_EncounterUpdate2.OnResume += Instance_OnResume_76;
				}
			}
		}
		if (null == owner_Connection_70 || !m_RegisteredForEvents)
		{
			owner_Connection_70 = parentGameObject;
		}
		if (null == owner_Connection_150 || !m_RegisteredForEvents)
		{
			owner_Connection_150 = parentGameObject;
		}
		if (null == owner_Connection_163 || !m_RegisteredForEvents)
		{
			owner_Connection_163 = parentGameObject;
		}
		if (null == owner_Connection_177 || !m_RegisteredForEvents)
		{
			owner_Connection_177 = parentGameObject;
		}
		if (null == owner_Connection_179 || !m_RegisteredForEvents)
		{
			owner_Connection_179 = parentGameObject;
		}
		if (null == owner_Connection_203 || !m_RegisteredForEvents)
		{
			owner_Connection_203 = parentGameObject;
		}
		if (null == owner_Connection_225 || !m_RegisteredForEvents)
		{
			owner_Connection_225 = parentGameObject;
		}
		if (null == owner_Connection_309 || !m_RegisteredForEvents)
		{
			owner_Connection_309 = parentGameObject;
			if (null != owner_Connection_309)
			{
				uScript_BlockCraftedEvent uScript_BlockCraftedEvent2 = owner_Connection_309.GetComponent<uScript_BlockCraftedEvent>();
				if (null == uScript_BlockCraftedEvent2)
				{
					uScript_BlockCraftedEvent2 = owner_Connection_309.AddComponent<uScript_BlockCraftedEvent>();
				}
				if (null != uScript_BlockCraftedEvent2)
				{
					uScript_BlockCraftedEvent2.BlockCraftedEvent += Instance_BlockCraftedEvent_288;
				}
			}
		}
		if (null == owner_Connection_321 || !m_RegisteredForEvents)
		{
			owner_Connection_321 = parentGameObject;
		}
	}

	private void RegisterForUnityHooks()
	{
		SyncEventListeners();
		if (!m_RegisteredForEvents && null != owner_Connection_11)
		{
			uScript_SaveLoad uScript_SaveLoad2 = owner_Connection_11.GetComponent<uScript_SaveLoad>();
			if (null == uScript_SaveLoad2)
			{
				uScript_SaveLoad2 = owner_Connection_11.AddComponent<uScript_SaveLoad>();
			}
			if (null != uScript_SaveLoad2)
			{
				uScript_SaveLoad2.SaveEvent += Instance_SaveEvent_27;
				uScript_SaveLoad2.LoadEvent += Instance_LoadEvent_27;
				uScript_SaveLoad2.RestartEvent += Instance_RestartEvent_27;
			}
		}
		if (!m_RegisteredForEvents && null != owner_Connection_58)
		{
			uScript_EncounterUpdate uScript_EncounterUpdate2 = owner_Connection_58.GetComponent<uScript_EncounterUpdate>();
			if (null == uScript_EncounterUpdate2)
			{
				uScript_EncounterUpdate2 = owner_Connection_58.AddComponent<uScript_EncounterUpdate>();
			}
			if (null != uScript_EncounterUpdate2)
			{
				uScript_EncounterUpdate2.OnUpdate += Instance_OnUpdate_76;
				uScript_EncounterUpdate2.OnSuspend += Instance_OnSuspend_76;
				uScript_EncounterUpdate2.OnResume += Instance_OnResume_76;
			}
		}
		if (!m_RegisteredForEvents && null != owner_Connection_309)
		{
			uScript_BlockCraftedEvent uScript_BlockCraftedEvent2 = owner_Connection_309.GetComponent<uScript_BlockCraftedEvent>();
			if (null == uScript_BlockCraftedEvent2)
			{
				uScript_BlockCraftedEvent2 = owner_Connection_309.AddComponent<uScript_BlockCraftedEvent>();
			}
			if (null != uScript_BlockCraftedEvent2)
			{
				uScript_BlockCraftedEvent2.BlockCraftedEvent += Instance_BlockCraftedEvent_288;
			}
		}
	}

	private void SyncEventListeners()
	{
	}

	private void UnregisterEventListeners()
	{
		if (null != owner_Connection_11)
		{
			uScript_SaveLoad component = owner_Connection_11.GetComponent<uScript_SaveLoad>();
			if (null != component)
			{
				component.SaveEvent -= Instance_SaveEvent_27;
				component.LoadEvent -= Instance_LoadEvent_27;
				component.RestartEvent -= Instance_RestartEvent_27;
			}
		}
		if (null != owner_Connection_58)
		{
			uScript_EncounterUpdate component2 = owner_Connection_58.GetComponent<uScript_EncounterUpdate>();
			if (null != component2)
			{
				component2.OnUpdate -= Instance_OnUpdate_76;
				component2.OnSuspend -= Instance_OnSuspend_76;
				component2.OnResume -= Instance_OnResume_76;
			}
		}
		if (null != owner_Connection_309)
		{
			uScript_BlockCraftedEvent component3 = owner_Connection_309.GetComponent<uScript_BlockCraftedEvent>();
			if (null != component3)
			{
				component3.BlockCraftedEvent -= Instance_BlockCraftedEvent_288;
			}
		}
	}

	public override void SetParent(GameObject g)
	{
		parentGameObject = g;
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_1.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_3.SetParent(g);
		logic_uScript_GetAndCheckBlocks_uScript_GetAndCheckBlocks_4.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_6.SetParent(g);
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_9.SetParent(g);
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_16.SetParent(g);
		logic_uScript_HideArrow_uScript_HideArrow_17.SetParent(g);
		logic_uScript_PausePopulation_uScript_PausePopulation_20.SetParent(g);
		logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_SubGraph_Crafting_Tutorial_AttachBlockToBase_24.SetParent(g);
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_25.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_32.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_35.SetParent(g);
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_36.SetParent(g);
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_37.SetParent(g);
		logic_uScript_SpawnBlocksFromData_uScript_SpawnBlocksFromData_39.SetParent(g);
		logic_uScript_PausePopulation_uScript_PausePopulation_40.SetParent(g);
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_41.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_43.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_44.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_45.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_49.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_50.SetParent(g);
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_51.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_55.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_56.SetParent(g);
		logic_uScript_LockTechInteraction_uScript_LockTechInteraction_60.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_64.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_65.SetParent(g);
		logic_uScript_LockTechStacks_uScript_LockTechStacks_67.SetParent(g);
		logic_SubGraph_Crafting_Tutorial_Init_SubGraph_Crafting_Tutorial_Init_68.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_71.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_72.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_77.SetParent(g);
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_80.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_81.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_83.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_90.SetParent(g);
		logic_uScript_HideHUDElement_uScript_HideHUDElement_94.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_95.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_96.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_97.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_101.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_102.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_103.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_104.SetParent(g);
		logic_uScript_DisableClosingCraftingUIWhenTooFarAway_uScript_DisableClosingCraftingUIWhenTooFarAway_106.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_108.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_109.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_110.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_112.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_118.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_119.SetParent(g);
		logic_uScriptAct_Concatenate_uScriptAct_Concatenate_123.SetParent(g);
		logic_uScriptAct_PrintText_uScriptAct_PrintText_125.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_130.SetParent(g);
		logic_uScript_LockTechInteraction_uScript_LockTechInteraction_131.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_138.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_139.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_141.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_143.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_147.SetParent(g);
		logic_uScript_GetNamedBlock_uScript_GetNamedBlock_149.SetParent(g);
		logic_uScript_LockBlock_uScript_LockBlock_152.SetParent(g);
		logic_uScript_LockBlock_uScript_LockBlock_154.SetParent(g);
		logic_uScript_LockBlock_uScript_LockBlock_155.SetParent(g);
		logic_uScript_LockBlock_uScript_LockBlock_156.SetParent(g);
		logic_uScript_LockBlock_uScript_LockBlock_157.SetParent(g);
		logic_uScript_GetNamedBlock_uScript_GetNamedBlock_158.SetParent(g);
		logic_uScript_LockBlock_uScript_LockBlock_161.SetParent(g);
		logic_uScript_LockBlock_uScript_LockBlock_162.SetParent(g);
		logic_uScript_LockBlock_uScript_LockBlock_164.SetParent(g);
		logic_uScript_LockBlock_uScript_LockBlock_165.SetParent(g);
		logic_uScript_LockBlock_uScript_LockBlock_166.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_167.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_168.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_169.SetParent(g);
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_170.SetParent(g);
		logic_uScript_GetBlockSpawnDataPositionName_uScript_GetBlockSpawnDataPositionName_172.SetParent(g);
		logic_uScript_LockTutorialBlockAttach_uScript_LockTutorialBlockAttach_173.SetParent(g);
		logic_uScript_KeepVisibleInEncounterArea_uScript_KeepVisibleInEncounterArea_178.SetParent(g);
		logic_uScript_AccessListBlock_uScript_AccessListBlock_181.SetParent(g);
		logic_uScript_GetAndCheckBlocks_uScript_GetAndCheckBlocks_185.SetParent(g);
		logic_uScript_LockBlockAttach_uScript_LockBlockAttach_186.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_190.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_191.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_192.SetParent(g);
		logic_uScript_AccessListBlockSpawnData_uScript_AccessListBlockSpawnData_193.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_194.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_195.SetParent(g);
		logic_uScript_CastBlock_uScript_CastBlock_197.SetParent(g);
		logic_uScript_CastBlock_uScript_CastBlock_200.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_202.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_204.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_205.SetParent(g);
		logic_uScript_GetAndCheckBlocks_uScript_GetAndCheckBlocks_209.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_210.SetParent(g);
		logic_uScript_KeepBlockInvulnerable_uScript_KeepBlockInvulnerable_213.SetParent(g);
		logic_uScript_KeepVisibleInEncounterArea_uScript_KeepVisibleInEncounterArea_215.SetParent(g);
		logic_uScript_GetBlockSpawnDataPositionName_uScript_GetBlockSpawnDataPositionName_216.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_218.SetParent(g);
		logic_uScript_LockTutorialBlockAttach_uScript_LockTutorialBlockAttach_220.SetParent(g);
		logic_uScript_LockBlockAttach_uScript_LockBlockAttach_222.SetParent(g);
		logic_uScript_AccessListBlockSpawnData_uScript_AccessListBlockSpawnData_223.SetParent(g);
		logic_uScript_AccessListBlock_uScript_AccessListBlock_227.SetParent(g);
		logic_uScript_KeepBlockInvulnerable_uScript_KeepBlockInvulnerable_230.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_233.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_234.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_235.SetParent(g);
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_237.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_238.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_240.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_242.SetParent(g);
		logic_uScript_DisableClosingCraftingUIWhenTooFarAway_uScript_DisableClosingCraftingUIWhenTooFarAway_244.SetParent(g);
		logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_245.SetParent(g);
		logic_uScript_IsHUDElementLinkedToBlock_uScript_IsHUDElementLinkedToBlock_248.SetParent(g);
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_249.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_254.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_257.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_258.SetParent(g);
		logic_uScript_HideArrow_uScript_HideArrow_261.SetParent(g);
		logic_uScript_LockHudGroup_uScript_LockHudGroup_263.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_268.SetParent(g);
		logic_uScript_LockPlayerInput_uScript_LockPlayerInput_270.SetParent(g);
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_273.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_274.SetParent(g);
		logic_uScript_CraftingUIHighlightCraftButton_uScript_CraftingUIHighlightCraftButton_276.SetParent(g);
		logic_uScript_LockPause_uScript_LockPause_277.SetParent(g);
		logic_uScript_DisableClosingCraftingUIWhenTooFarAway_uScript_DisableClosingCraftingUIWhenTooFarAway_278.SetParent(g);
		logic_uScript_LockTechInteraction_uScript_LockTechInteraction_281.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_282.SetParent(g);
		logic_uScript_CraftingUIHighlightItem_uScript_CraftingUIHighlightItem_283.SetParent(g);
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_286.SetParent(g);
		logic_uScript_LockTechInteraction_uScript_LockTechInteraction_287.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_290.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_291.SetParent(g);
		logic_uScript_LockHudGroup_uScript_LockHudGroup_292.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_293.SetParent(g);
		logic_uScript_CompareBlockTypes_uScript_CompareBlockTypes_294.SetParent(g);
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_298.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_301.SetParent(g);
		logic_uScript_LockPause_uScript_LockPause_302.SetParent(g);
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_305.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_310.SetParent(g);
		logic_SubGraph_Crafting_Tutorial_Finish_SubGraph_Crafting_Tutorial_Finish_316.SetParent(g);
		logic_uScript_LockPlayerInput_uScript_LockPlayerInput_317.SetParent(g);
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_318.SetParent(g);
		logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_320.SetParent(g);
		owner_Connection_11 = parentGameObject;
		owner_Connection_21 = parentGameObject;
		owner_Connection_54 = parentGameObject;
		owner_Connection_58 = parentGameObject;
		owner_Connection_70 = parentGameObject;
		owner_Connection_150 = parentGameObject;
		owner_Connection_163 = parentGameObject;
		owner_Connection_177 = parentGameObject;
		owner_Connection_179 = parentGameObject;
		owner_Connection_203 = parentGameObject;
		owner_Connection_225 = parentGameObject;
		owner_Connection_309 = parentGameObject;
		owner_Connection_321 = parentGameObject;
	}

	public void Awake()
	{
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_6.Awake();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_9.Awake();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_16.Awake();
		logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_SubGraph_Crafting_Tutorial_AttachBlockToBase_24.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_32.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_35.Awake();
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_36.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_44.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_49.Awake();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_51.Awake();
		logic_SubGraph_Crafting_Tutorial_Init_SubGraph_Crafting_Tutorial_Init_68.Awake();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_80.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_95.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_96.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_97.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_147.Awake();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_170.Awake();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_249.Awake();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_273.Awake();
		logic_SubGraph_Crafting_Tutorial_Finish_SubGraph_Crafting_Tutorial_Finish_316.Awake();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_318.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_6.Save_Out += SubGraph_SaveLoadBool_Save_Out_6;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_6.Load_Out += SubGraph_SaveLoadBool_Load_Out_6;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_6.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_6;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_9.Out += SubGraph_CompleteObjectiveStage_Out_9;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_16.Save_Out += SubGraph_SaveLoadInt_Save_Out_16;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_16.Load_Out += SubGraph_SaveLoadInt_Load_Out_16;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_16.Restart_Out += SubGraph_SaveLoadInt_Restart_Out_16;
		logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_SubGraph_Crafting_Tutorial_AttachBlockToBase_24.Block_Attached += SubGraph_Crafting_Tutorial_AttachBlockToBase_Block_Attached_24;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_25.Output1 += uScriptCon_ManualSwitch_Output1_25;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_25.Output2 += uScriptCon_ManualSwitch_Output2_25;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_25.Output3 += uScriptCon_ManualSwitch_Output3_25;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_25.Output4 += uScriptCon_ManualSwitch_Output4_25;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_25.Output5 += uScriptCon_ManualSwitch_Output5_25;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_25.Output6 += uScriptCon_ManualSwitch_Output6_25;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_25.Output7 += uScriptCon_ManualSwitch_Output7_25;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_25.Output8 += uScriptCon_ManualSwitch_Output8_25;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_32.Save_Out += SubGraph_SaveLoadBool_Save_Out_32;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_32.Load_Out += SubGraph_SaveLoadBool_Load_Out_32;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_32.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_32;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_35.Save_Out += SubGraph_SaveLoadBool_Save_Out_35;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_35.Load_Out += SubGraph_SaveLoadBool_Load_Out_35;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_35.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_35;
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_36.Out += SubGraph_Crafting_Tutorial_ManageBlock_Out_36;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_44.Save_Out += SubGraph_SaveLoadBool_Save_Out_44;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_44.Load_Out += SubGraph_SaveLoadBool_Load_Out_44;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_44.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_44;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_49.Save_Out += SubGraph_SaveLoadBool_Save_Out_49;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_49.Load_Out += SubGraph_SaveLoadBool_Load_Out_49;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_49.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_49;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_51.Out += SubGraph_CompleteObjectiveStage_Out_51;
		logic_SubGraph_Crafting_Tutorial_Init_SubGraph_Crafting_Tutorial_Init_68.Out += SubGraph_Crafting_Tutorial_Init_Out_68;
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_80.Out += SubGraph_LoadObjectiveStates_Out_80;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_95.Save_Out += SubGraph_SaveLoadBool_Save_Out_95;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_95.Load_Out += SubGraph_SaveLoadBool_Load_Out_95;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_95.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_95;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_96.Save_Out += SubGraph_SaveLoadBool_Save_Out_96;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_96.Load_Out += SubGraph_SaveLoadBool_Load_Out_96;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_96.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_96;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_97.Save_Out += SubGraph_SaveLoadBool_Save_Out_97;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_97.Load_Out += SubGraph_SaveLoadBool_Load_Out_97;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_97.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_97;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_147.Save_Out += SubGraph_SaveLoadBool_Save_Out_147;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_147.Load_Out += SubGraph_SaveLoadBool_Load_Out_147;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_147.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_147;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_170.Save_Out += SubGraph_SaveLoadInt_Save_Out_170;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_170.Load_Out += SubGraph_SaveLoadInt_Load_Out_170;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_170.Restart_Out += SubGraph_SaveLoadInt_Restart_Out_170;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_237.Output1 += uScriptCon_ManualSwitch_Output1_237;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_237.Output2 += uScriptCon_ManualSwitch_Output2_237;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_237.Output3 += uScriptCon_ManualSwitch_Output3_237;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_237.Output4 += uScriptCon_ManualSwitch_Output4_237;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_237.Output5 += uScriptCon_ManualSwitch_Output5_237;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_237.Output6 += uScriptCon_ManualSwitch_Output6_237;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_237.Output7 += uScriptCon_ManualSwitch_Output7_237;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_237.Output8 += uScriptCon_ManualSwitch_Output8_237;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_249.Out += SubGraph_AddMessageWithPadSupport_Out_249;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_249.Shown += SubGraph_AddMessageWithPadSupport_Shown_249;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_273.Out += SubGraph_AddMessageWithPadSupport_Out_273;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_273.Shown += SubGraph_AddMessageWithPadSupport_Shown_273;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_298.Output1 += uScriptCon_ManualSwitch_Output1_298;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_298.Output2 += uScriptCon_ManualSwitch_Output2_298;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_298.Output3 += uScriptCon_ManualSwitch_Output3_298;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_298.Output4 += uScriptCon_ManualSwitch_Output4_298;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_298.Output5 += uScriptCon_ManualSwitch_Output5_298;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_298.Output6 += uScriptCon_ManualSwitch_Output6_298;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_298.Output7 += uScriptCon_ManualSwitch_Output7_298;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_298.Output8 += uScriptCon_ManualSwitch_Output8_298;
		logic_SubGraph_Crafting_Tutorial_Finish_SubGraph_Crafting_Tutorial_Finish_316.Out += SubGraph_Crafting_Tutorial_Finish_Out_316;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_318.Save_Out += SubGraph_SaveLoadInt_Save_Out_318;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_318.Load_Out += SubGraph_SaveLoadInt_Load_Out_318;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_318.Restart_Out += SubGraph_SaveLoadInt_Restart_Out_318;
	}

	public void Start()
	{
		SyncUnityHooks();
		m_RegisteredForEvents = true;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_6.Start();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_9.Start();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_16.Start();
		logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_SubGraph_Crafting_Tutorial_AttachBlockToBase_24.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_32.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_35.Start();
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_36.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_44.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_49.Start();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_51.Start();
		logic_SubGraph_Crafting_Tutorial_Init_SubGraph_Crafting_Tutorial_Init_68.Start();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_80.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_95.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_96.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_97.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_147.Start();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_170.Start();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_249.Start();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_273.Start();
		logic_SubGraph_Crafting_Tutorial_Finish_SubGraph_Crafting_Tutorial_Finish_316.Start();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_318.Start();
	}

	public void OnEnable()
	{
		RegisterForUnityHooks();
		m_RegisteredForEvents = true;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_6.OnEnable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_9.OnEnable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_16.OnEnable();
		logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_SubGraph_Crafting_Tutorial_AttachBlockToBase_24.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_32.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_35.OnEnable();
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_36.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_44.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_49.OnEnable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_51.OnEnable();
		logic_SubGraph_Crafting_Tutorial_Init_SubGraph_Crafting_Tutorial_Init_68.OnEnable();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_80.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_95.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_96.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_97.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_147.OnEnable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_170.OnEnable();
		logic_uScript_LockTutorialBlockAttach_uScript_LockTutorialBlockAttach_173.OnEnable();
		logic_uScript_LockTutorialBlockAttach_uScript_LockTutorialBlockAttach_220.OnEnable();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_249.OnEnable();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_273.OnEnable();
		logic_uScript_CraftingUIHighlightCraftButton_uScript_CraftingUIHighlightCraftButton_276.OnEnable();
		logic_uScript_CraftingUIHighlightItem_uScript_CraftingUIHighlightItem_283.OnEnable();
		logic_SubGraph_Crafting_Tutorial_Finish_SubGraph_Crafting_Tutorial_Finish_316.OnEnable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_318.OnEnable();
		logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_320.OnEnable();
	}

	public void OnDisable()
	{
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_6.OnDisable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_9.OnDisable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_16.OnDisable();
		logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_SubGraph_Crafting_Tutorial_AttachBlockToBase_24.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_32.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_35.OnDisable();
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_36.OnDisable();
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_37.OnDisable();
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_41.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_44.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_49.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_50.OnDisable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_51.OnDisable();
		logic_SubGraph_Crafting_Tutorial_Init_SubGraph_Crafting_Tutorial_Init_68.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_77.OnDisable();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_80.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_95.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_96.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_97.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_103.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_139.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_141.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_147.OnDisable();
		logic_uScript_GetNamedBlock_uScript_GetNamedBlock_149.OnDisable();
		logic_uScript_GetNamedBlock_uScript_GetNamedBlock_158.OnDisable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_170.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_194.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_204.OnDisable();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_249.OnDisable();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_273.OnDisable();
		logic_uScript_CraftingUIHighlightCraftButton_uScript_CraftingUIHighlightCraftButton_276.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_290.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_291.OnDisable();
		logic_SubGraph_Crafting_Tutorial_Finish_SubGraph_Crafting_Tutorial_Finish_316.OnDisable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_318.OnDisable();
		UnregisterEventListeners();
		m_RegisteredForEvents = false;
	}

	public void Update()
	{
		SyncEventListeners();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_6.Update();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_9.Update();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_16.Update();
		logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_SubGraph_Crafting_Tutorial_AttachBlockToBase_24.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_32.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_35.Update();
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_36.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_44.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_49.Update();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_51.Update();
		logic_SubGraph_Crafting_Tutorial_Init_SubGraph_Crafting_Tutorial_Init_68.Update();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_80.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_95.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_96.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_97.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_147.Update();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_170.Update();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_249.Update();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_273.Update();
		logic_SubGraph_Crafting_Tutorial_Finish_SubGraph_Crafting_Tutorial_Finish_316.Update();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_318.Update();
	}

	public void OnDestroy()
	{
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_6.OnDestroy();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_9.OnDestroy();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_16.OnDestroy();
		logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_SubGraph_Crafting_Tutorial_AttachBlockToBase_24.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_32.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_35.OnDestroy();
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_36.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_44.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_49.OnDestroy();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_51.OnDestroy();
		logic_SubGraph_Crafting_Tutorial_Init_SubGraph_Crafting_Tutorial_Init_68.OnDestroy();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_80.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_95.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_96.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_97.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_147.OnDestroy();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_170.OnDestroy();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_249.OnDestroy();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_273.OnDestroy();
		logic_SubGraph_Crafting_Tutorial_Finish_SubGraph_Crafting_Tutorial_Finish_316.OnDestroy();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_318.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_6.Save_Out -= SubGraph_SaveLoadBool_Save_Out_6;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_6.Load_Out -= SubGraph_SaveLoadBool_Load_Out_6;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_6.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_6;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_9.Out -= SubGraph_CompleteObjectiveStage_Out_9;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_16.Save_Out -= SubGraph_SaveLoadInt_Save_Out_16;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_16.Load_Out -= SubGraph_SaveLoadInt_Load_Out_16;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_16.Restart_Out -= SubGraph_SaveLoadInt_Restart_Out_16;
		logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_SubGraph_Crafting_Tutorial_AttachBlockToBase_24.Block_Attached -= SubGraph_Crafting_Tutorial_AttachBlockToBase_Block_Attached_24;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_25.Output1 -= uScriptCon_ManualSwitch_Output1_25;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_25.Output2 -= uScriptCon_ManualSwitch_Output2_25;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_25.Output3 -= uScriptCon_ManualSwitch_Output3_25;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_25.Output4 -= uScriptCon_ManualSwitch_Output4_25;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_25.Output5 -= uScriptCon_ManualSwitch_Output5_25;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_25.Output6 -= uScriptCon_ManualSwitch_Output6_25;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_25.Output7 -= uScriptCon_ManualSwitch_Output7_25;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_25.Output8 -= uScriptCon_ManualSwitch_Output8_25;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_32.Save_Out -= SubGraph_SaveLoadBool_Save_Out_32;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_32.Load_Out -= SubGraph_SaveLoadBool_Load_Out_32;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_32.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_32;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_35.Save_Out -= SubGraph_SaveLoadBool_Save_Out_35;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_35.Load_Out -= SubGraph_SaveLoadBool_Load_Out_35;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_35.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_35;
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_36.Out -= SubGraph_Crafting_Tutorial_ManageBlock_Out_36;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_44.Save_Out -= SubGraph_SaveLoadBool_Save_Out_44;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_44.Load_Out -= SubGraph_SaveLoadBool_Load_Out_44;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_44.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_44;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_49.Save_Out -= SubGraph_SaveLoadBool_Save_Out_49;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_49.Load_Out -= SubGraph_SaveLoadBool_Load_Out_49;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_49.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_49;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_51.Out -= SubGraph_CompleteObjectiveStage_Out_51;
		logic_SubGraph_Crafting_Tutorial_Init_SubGraph_Crafting_Tutorial_Init_68.Out -= SubGraph_Crafting_Tutorial_Init_Out_68;
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_80.Out -= SubGraph_LoadObjectiveStates_Out_80;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_95.Save_Out -= SubGraph_SaveLoadBool_Save_Out_95;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_95.Load_Out -= SubGraph_SaveLoadBool_Load_Out_95;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_95.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_95;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_96.Save_Out -= SubGraph_SaveLoadBool_Save_Out_96;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_96.Load_Out -= SubGraph_SaveLoadBool_Load_Out_96;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_96.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_96;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_97.Save_Out -= SubGraph_SaveLoadBool_Save_Out_97;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_97.Load_Out -= SubGraph_SaveLoadBool_Load_Out_97;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_97.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_97;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_147.Save_Out -= SubGraph_SaveLoadBool_Save_Out_147;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_147.Load_Out -= SubGraph_SaveLoadBool_Load_Out_147;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_147.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_147;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_170.Save_Out -= SubGraph_SaveLoadInt_Save_Out_170;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_170.Load_Out -= SubGraph_SaveLoadInt_Load_Out_170;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_170.Restart_Out -= SubGraph_SaveLoadInt_Restart_Out_170;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_237.Output1 -= uScriptCon_ManualSwitch_Output1_237;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_237.Output2 -= uScriptCon_ManualSwitch_Output2_237;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_237.Output3 -= uScriptCon_ManualSwitch_Output3_237;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_237.Output4 -= uScriptCon_ManualSwitch_Output4_237;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_237.Output5 -= uScriptCon_ManualSwitch_Output5_237;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_237.Output6 -= uScriptCon_ManualSwitch_Output6_237;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_237.Output7 -= uScriptCon_ManualSwitch_Output7_237;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_237.Output8 -= uScriptCon_ManualSwitch_Output8_237;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_249.Out -= SubGraph_AddMessageWithPadSupport_Out_249;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_249.Shown -= SubGraph_AddMessageWithPadSupport_Shown_249;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_273.Out -= SubGraph_AddMessageWithPadSupport_Out_273;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_273.Shown -= SubGraph_AddMessageWithPadSupport_Shown_273;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_298.Output1 -= uScriptCon_ManualSwitch_Output1_298;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_298.Output2 -= uScriptCon_ManualSwitch_Output2_298;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_298.Output3 -= uScriptCon_ManualSwitch_Output3_298;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_298.Output4 -= uScriptCon_ManualSwitch_Output4_298;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_298.Output5 -= uScriptCon_ManualSwitch_Output5_298;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_298.Output6 -= uScriptCon_ManualSwitch_Output6_298;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_298.Output7 -= uScriptCon_ManualSwitch_Output7_298;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_298.Output8 -= uScriptCon_ManualSwitch_Output8_298;
		logic_SubGraph_Crafting_Tutorial_Finish_SubGraph_Crafting_Tutorial_Finish_316.Out -= SubGraph_Crafting_Tutorial_Finish_Out_316;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_318.Save_Out -= SubGraph_SaveLoadInt_Save_Out_318;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_318.Load_Out -= SubGraph_SaveLoadInt_Load_Out_318;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_318.Restart_Out -= SubGraph_SaveLoadInt_Restart_Out_318;
	}

	public void OnGUI()
	{
		logic_uScriptAct_PrintText_uScriptAct_PrintText_125.OnGUI();
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

	private void Instance_OnUpdate_76(object o, EventArgs e)
	{
		Relay_OnUpdate_76();
	}

	private void Instance_OnSuspend_76(object o, EventArgs e)
	{
		Relay_OnSuspend_76();
	}

	private void Instance_OnResume_76(object o, EventArgs e)
	{
		Relay_OnResume_76();
	}

	private void Instance_BlockCraftedEvent_288(object o, uScript_BlockCraftedEvent.BlockCraftedEventArgs e)
	{
		event_UnityEngine_GameObject_BlockType_288 = e.BlockType;
		event_UnityEngine_GameObject_BlockTypeTotal_288 = e.BlockTypeTotal;
		event_UnityEngine_GameObject_BlockTotal_288 = e.BlockTotal;
		event_UnityEngine_GameObject_Block_288 = e.Block;
		event_UnityEngine_GameObject_CrafterBlock_288 = e.CrafterBlock;
		Relay_BlockCraftedEvent_288();
	}

	private void SubGraph_SaveLoadBool_Save_Out_6(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_6 = e.boolean;
		local_msgIntroShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_6;
		Relay_Save_Out_6();
	}

	private void SubGraph_SaveLoadBool_Load_Out_6(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_6 = e.boolean;
		local_msgIntroShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_6;
		Relay_Load_Out_6();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_6(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_6 = e.boolean;
		local_msgIntroShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_6;
		Relay_Restart_Out_6();
	}

	private void SubGraph_CompleteObjectiveStage_Out_9(object o, SubGraph_CompleteObjectiveStage.LogicEventArgs e)
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_9 = e.objectiveStage;
		local_Stage_System_Int32 = logic_SubGraph_CompleteObjectiveStage_objectiveStage_9;
		Relay_Out_9();
	}

	private void SubGraph_SaveLoadInt_Save_Out_16(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_16 = e.integer;
		local_Stage_System_Int32 = logic_SubGraph_SaveLoadInt_integer_16;
		Relay_Save_Out_16();
	}

	private void SubGraph_SaveLoadInt_Load_Out_16(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_16 = e.integer;
		local_Stage_System_Int32 = logic_SubGraph_SaveLoadInt_integer_16;
		Relay_Load_Out_16();
	}

	private void SubGraph_SaveLoadInt_Restart_Out_16(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_16 = e.integer;
		local_Stage_System_Int32 = logic_SubGraph_SaveLoadInt_integer_16;
		Relay_Restart_Out_16();
	}

	private void SubGraph_Crafting_Tutorial_AttachBlockToBase_Block_Attached_24(object o, SubGraph_Crafting_Tutorial_AttachBlockToBase.LogicEventArgs e)
	{
		Relay_Block_Attached_24();
	}

	private void uScriptCon_ManualSwitch_Output1_25(object o, EventArgs e)
	{
		Relay_Output1_25();
	}

	private void uScriptCon_ManualSwitch_Output2_25(object o, EventArgs e)
	{
		Relay_Output2_25();
	}

	private void uScriptCon_ManualSwitch_Output3_25(object o, EventArgs e)
	{
		Relay_Output3_25();
	}

	private void uScriptCon_ManualSwitch_Output4_25(object o, EventArgs e)
	{
		Relay_Output4_25();
	}

	private void uScriptCon_ManualSwitch_Output5_25(object o, EventArgs e)
	{
		Relay_Output5_25();
	}

	private void uScriptCon_ManualSwitch_Output6_25(object o, EventArgs e)
	{
		Relay_Output6_25();
	}

	private void uScriptCon_ManualSwitch_Output7_25(object o, EventArgs e)
	{
		Relay_Output7_25();
	}

	private void uScriptCon_ManualSwitch_Output8_25(object o, EventArgs e)
	{
		Relay_Output8_25();
	}

	private void SubGraph_SaveLoadBool_Save_Out_32(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_32 = e.boolean;
		local_msgBaseFoundShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_32;
		Relay_Save_Out_32();
	}

	private void SubGraph_SaveLoadBool_Load_Out_32(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_32 = e.boolean;
		local_msgBaseFoundShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_32;
		Relay_Load_Out_32();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_32(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_32 = e.boolean;
		local_msgBaseFoundShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_32;
		Relay_Restart_Out_32();
	}

	private void SubGraph_SaveLoadBool_Save_Out_35(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_35 = e.boolean;
		local_IsFusionMachineAttached_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_35;
		Relay_Save_Out_35();
	}

	private void SubGraph_SaveLoadBool_Load_Out_35(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_35 = e.boolean;
		local_IsFusionMachineAttached_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_35;
		Relay_Load_Out_35();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_35(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_35 = e.boolean;
		local_IsFusionMachineAttached_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_35;
		Relay_Restart_Out_35();
	}

	private void SubGraph_Crafting_Tutorial_ManageBlock_Out_36(object o, SubGraph_Crafting_Tutorial_ManageBlock.LogicEventArgs e)
	{
		logic_SubGraph_Crafting_Tutorial_ManageBlock_block_36 = e.block;
		blockSpawnDataFusionMachine = logic_SubGraph_Crafting_Tutorial_ManageBlock_blockSpawnData_36;
		local_FusionMachineBlock_TankBlock = logic_SubGraph_Crafting_Tutorial_ManageBlock_block_36;
		Relay_Out_36();
	}

	private void SubGraph_SaveLoadBool_Save_Out_44(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_44 = e.boolean;
		local_msgFusionMachineSpawnedShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_44;
		Relay_Save_Out_44();
	}

	private void SubGraph_SaveLoadBool_Load_Out_44(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_44 = e.boolean;
		local_msgFusionMachineSpawnedShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_44;
		Relay_Load_Out_44();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_44(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_44 = e.boolean;
		local_msgFusionMachineSpawnedShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_44;
		Relay_Restart_Out_44();
	}

	private void SubGraph_SaveLoadBool_Save_Out_49(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_49 = e.boolean;
		local_FusionMachineSpawned_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_49;
		Relay_Save_Out_49();
	}

	private void SubGraph_SaveLoadBool_Load_Out_49(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_49 = e.boolean;
		local_FusionMachineSpawned_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_49;
		Relay_Load_Out_49();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_49(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_49 = e.boolean;
		local_FusionMachineSpawned_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_49;
		Relay_Restart_Out_49();
	}

	private void SubGraph_CompleteObjectiveStage_Out_51(object o, SubGraph_CompleteObjectiveStage.LogicEventArgs e)
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_51 = e.objectiveStage;
		local_Stage_System_Int32 = logic_SubGraph_CompleteObjectiveStage_objectiveStage_51;
		Relay_Out_51();
	}

	private void SubGraph_Crafting_Tutorial_Init_Out_68(object o, SubGraph_Crafting_Tutorial_Init.LogicEventArgs e)
	{
		logic_SubGraph_Crafting_Tutorial_Init_CraftingBaseTech_68 = e.CraftingBaseTech;
		logic_SubGraph_Crafting_Tutorial_Init_NPCTech_68 = e.NPCTech;
		local_FusionCraftingBaseTech_Tank = logic_SubGraph_Crafting_Tutorial_Init_CraftingBaseTech_68;
		local_NPCTech_Tank = logic_SubGraph_Crafting_Tutorial_Init_NPCTech_68;
		Relay_Out_68();
	}

	private void SubGraph_LoadObjectiveStates_Out_80(object o, SubGraph_LoadObjectiveStates.LogicEventArgs e)
	{
		Relay_Out_80();
	}

	private void SubGraph_SaveLoadBool_Save_Out_95(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_95 = e.boolean;
		local_CraftingMenuOpened_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_95;
		Relay_Save_Out_95();
	}

	private void SubGraph_SaveLoadBool_Load_Out_95(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_95 = e.boolean;
		local_CraftingMenuOpened_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_95;
		Relay_Load_Out_95();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_95(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_95 = e.boolean;
		local_CraftingMenuOpened_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_95;
		Relay_Restart_Out_95();
	}

	private void SubGraph_SaveLoadBool_Save_Out_96(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_96 = e.boolean;
		local_BlockCrafted_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_96;
		Relay_Save_Out_96();
	}

	private void SubGraph_SaveLoadBool_Load_Out_96(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_96 = e.boolean;
		local_BlockCrafted_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_96;
		Relay_Load_Out_96();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_96(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_96 = e.boolean;
		local_BlockCrafted_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_96;
		Relay_Restart_Out_96();
	}

	private void SubGraph_SaveLoadBool_Save_Out_97(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_97 = e.boolean;
		local_CraftingInProgress_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_97;
		Relay_Save_Out_97();
	}

	private void SubGraph_SaveLoadBool_Load_Out_97(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_97 = e.boolean;
		local_CraftingInProgress_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_97;
		Relay_Load_Out_97();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_97(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_97 = e.boolean;
		local_CraftingInProgress_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_97;
		Relay_Restart_Out_97();
	}

	private void SubGraph_SaveLoadBool_Save_Out_147(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_147 = e.boolean;
		local_CanInteractWithFusionMachine_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_147;
		Relay_Save_Out_147();
	}

	private void SubGraph_SaveLoadBool_Load_Out_147(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_147 = e.boolean;
		local_CanInteractWithFusionMachine_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_147;
		Relay_Load_Out_147();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_147(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_147 = e.boolean;
		local_CanInteractWithFusionMachine_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_147;
		Relay_Restart_Out_147();
	}

	private void SubGraph_SaveLoadInt_Save_Out_170(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_170 = e.integer;
		local_Stage2_System_Int32 = logic_SubGraph_SaveLoadInt_integer_170;
		Relay_Save_Out_170();
	}

	private void SubGraph_SaveLoadInt_Load_Out_170(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_170 = e.integer;
		local_Stage2_System_Int32 = logic_SubGraph_SaveLoadInt_integer_170;
		Relay_Load_Out_170();
	}

	private void SubGraph_SaveLoadInt_Restart_Out_170(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_170 = e.integer;
		local_Stage2_System_Int32 = logic_SubGraph_SaveLoadInt_integer_170;
		Relay_Restart_Out_170();
	}

	private void uScriptCon_ManualSwitch_Output1_237(object o, EventArgs e)
	{
		Relay_Output1_237();
	}

	private void uScriptCon_ManualSwitch_Output2_237(object o, EventArgs e)
	{
		Relay_Output2_237();
	}

	private void uScriptCon_ManualSwitch_Output3_237(object o, EventArgs e)
	{
		Relay_Output3_237();
	}

	private void uScriptCon_ManualSwitch_Output4_237(object o, EventArgs e)
	{
		Relay_Output4_237();
	}

	private void uScriptCon_ManualSwitch_Output5_237(object o, EventArgs e)
	{
		Relay_Output5_237();
	}

	private void uScriptCon_ManualSwitch_Output6_237(object o, EventArgs e)
	{
		Relay_Output6_237();
	}

	private void uScriptCon_ManualSwitch_Output7_237(object o, EventArgs e)
	{
		Relay_Output7_237();
	}

	private void uScriptCon_ManualSwitch_Output8_237(object o, EventArgs e)
	{
		Relay_Output8_237();
	}

	private void SubGraph_AddMessageWithPadSupport_Out_249(object o, SubGraph_AddMessageWithPadSupport.LogicEventArgs e)
	{
		logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboardReturn_249 = e.messageMouseKeyboardReturn;
		logic_SubGraph_AddMessageWithPadSupport_messageControlPadReturn_249 = e.messageControlPadReturn;
		Relay_Out_249();
	}

	private void SubGraph_AddMessageWithPadSupport_Shown_249(object o, SubGraph_AddMessageWithPadSupport.LogicEventArgs e)
	{
		logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboardReturn_249 = e.messageMouseKeyboardReturn;
		logic_SubGraph_AddMessageWithPadSupport_messageControlPadReturn_249 = e.messageControlPadReturn;
		Relay_Shown_249();
	}

	private void SubGraph_AddMessageWithPadSupport_Out_273(object o, SubGraph_AddMessageWithPadSupport.LogicEventArgs e)
	{
		logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboardReturn_273 = e.messageMouseKeyboardReturn;
		logic_SubGraph_AddMessageWithPadSupport_messageControlPadReturn_273 = e.messageControlPadReturn;
		Relay_Out_273();
	}

	private void SubGraph_AddMessageWithPadSupport_Shown_273(object o, SubGraph_AddMessageWithPadSupport.LogicEventArgs e)
	{
		logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboardReturn_273 = e.messageMouseKeyboardReturn;
		logic_SubGraph_AddMessageWithPadSupport_messageControlPadReturn_273 = e.messageControlPadReturn;
		Relay_Shown_273();
	}

	private void uScriptCon_ManualSwitch_Output1_298(object o, EventArgs e)
	{
		Relay_Output1_298();
	}

	private void uScriptCon_ManualSwitch_Output2_298(object o, EventArgs e)
	{
		Relay_Output2_298();
	}

	private void uScriptCon_ManualSwitch_Output3_298(object o, EventArgs e)
	{
		Relay_Output3_298();
	}

	private void uScriptCon_ManualSwitch_Output4_298(object o, EventArgs e)
	{
		Relay_Output4_298();
	}

	private void uScriptCon_ManualSwitch_Output5_298(object o, EventArgs e)
	{
		Relay_Output5_298();
	}

	private void uScriptCon_ManualSwitch_Output6_298(object o, EventArgs e)
	{
		Relay_Output6_298();
	}

	private void uScriptCon_ManualSwitch_Output7_298(object o, EventArgs e)
	{
		Relay_Output7_298();
	}

	private void uScriptCon_ManualSwitch_Output8_298(object o, EventArgs e)
	{
		Relay_Output8_298();
	}

	private void SubGraph_Crafting_Tutorial_Finish_Out_316(object o, SubGraph_Crafting_Tutorial_Finish.LogicEventArgs e)
	{
		Relay_Out_316();
	}

	private void SubGraph_SaveLoadInt_Save_Out_318(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_318 = e.integer;
		local_Stage3_System_Int32 = logic_SubGraph_SaveLoadInt_integer_318;
		Relay_Save_Out_318();
	}

	private void SubGraph_SaveLoadInt_Load_Out_318(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_318 = e.integer;
		local_Stage3_System_Int32 = logic_SubGraph_SaveLoadInt_integer_318;
		Relay_Load_Out_318();
	}

	private void SubGraph_SaveLoadInt_Restart_Out_318(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_318 = e.integer;
		local_Stage3_System_Int32 = logic_SubGraph_SaveLoadInt_integer_318;
		Relay_Restart_Out_318();
	}

	private void Relay_In_1()
	{
		logic_uScript_SetEncounterTarget_owner_1 = owner_Connection_21;
		logic_uScript_SetEncounterTarget_visibleObject_1 = local_NPCTech_Tank;
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_1.In(logic_uScript_SetEncounterTarget_owner_1, logic_uScript_SetEncounterTarget_visibleObject_1);
		if (logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_1.Out)
		{
			Relay_In_37();
		}
	}

	private void Relay_In_3()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_3 = messageTag;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_3.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_3, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_3);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_3.Out)
		{
			Relay_In_17();
		}
	}

	private void Relay_In_4()
	{
		int num = 0;
		Array array = blockSpawnDataFusionMachine;
		if (logic_uScript_GetAndCheckBlocks_blockData_4.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckBlocks_blockData_4, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckBlocks_blockData_4, num, array.Length);
		num += array.Length;
		logic_uScript_GetAndCheckBlocks_ownerNode_4 = owner_Connection_70;
		logic_uScript_GetAndCheckBlocks_uScript_GetAndCheckBlocks_4.In(logic_uScript_GetAndCheckBlocks_blockData_4, logic_uScript_GetAndCheckBlocks_ownerNode_4, ref logic_uScript_GetAndCheckBlocks_blocks_4);
		bool allAlive = logic_uScript_GetAndCheckBlocks_uScript_GetAndCheckBlocks_4.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckBlocks_uScript_GetAndCheckBlocks_4.SomeAlive;
		if (allAlive)
		{
			Relay_In_143();
		}
		if (someAlive)
		{
			Relay_In_143();
		}
	}

	private void Relay_Save_Out_6()
	{
		Relay_Save_44();
	}

	private void Relay_Load_Out_6()
	{
		Relay_Load_44();
	}

	private void Relay_Restart_Out_6()
	{
		Relay_Set_False_44();
	}

	private void Relay_Save_6()
	{
		logic_SubGraph_SaveLoadBool_boolean_6 = local_msgIntroShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_6 = local_msgIntroShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_6.Save(ref logic_SubGraph_SaveLoadBool_boolean_6, logic_SubGraph_SaveLoadBool_boolAsVariable_6, logic_SubGraph_SaveLoadBool_uniqueID_6);
	}

	private void Relay_Load_6()
	{
		logic_SubGraph_SaveLoadBool_boolean_6 = local_msgIntroShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_6 = local_msgIntroShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_6.Load(ref logic_SubGraph_SaveLoadBool_boolean_6, logic_SubGraph_SaveLoadBool_boolAsVariable_6, logic_SubGraph_SaveLoadBool_uniqueID_6);
	}

	private void Relay_Set_True_6()
	{
		logic_SubGraph_SaveLoadBool_boolean_6 = local_msgIntroShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_6 = local_msgIntroShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_6.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_6, logic_SubGraph_SaveLoadBool_boolAsVariable_6, logic_SubGraph_SaveLoadBool_uniqueID_6);
	}

	private void Relay_Set_False_6()
	{
		logic_SubGraph_SaveLoadBool_boolean_6 = local_msgIntroShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_6 = local_msgIntroShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_6.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_6, logic_SubGraph_SaveLoadBool_boolAsVariable_6, logic_SubGraph_SaveLoadBool_uniqueID_6);
	}

	private void Relay_Out_9()
	{
	}

	private void Relay_In_9()
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_9 = local_Stage_System_Int32;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_9.In(ref logic_SubGraph_CompleteObjectiveStage_objectiveStage_9, logic_SubGraph_CompleteObjectiveStage_isFinalObjective_9);
	}

	private void Relay_Save_Out_16()
	{
		Relay_Save_170();
	}

	private void Relay_Load_Out_16()
	{
		Relay_Load_170();
	}

	private void Relay_Restart_Out_16()
	{
		Relay_Restart_170();
	}

	private void Relay_Save_16()
	{
		logic_SubGraph_SaveLoadInt_integer_16 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_16 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_16.Save(logic_SubGraph_SaveLoadInt_restartValue_16, ref logic_SubGraph_SaveLoadInt_integer_16, logic_SubGraph_SaveLoadInt_intAsVariable_16, logic_SubGraph_SaveLoadInt_uniqueID_16);
	}

	private void Relay_Load_16()
	{
		logic_SubGraph_SaveLoadInt_integer_16 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_16 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_16.Load(logic_SubGraph_SaveLoadInt_restartValue_16, ref logic_SubGraph_SaveLoadInt_integer_16, logic_SubGraph_SaveLoadInt_intAsVariable_16, logic_SubGraph_SaveLoadInt_uniqueID_16);
	}

	private void Relay_Restart_16()
	{
		logic_SubGraph_SaveLoadInt_integer_16 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_16 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_16.Restart(logic_SubGraph_SaveLoadInt_restartValue_16, ref logic_SubGraph_SaveLoadInt_integer_16, logic_SubGraph_SaveLoadInt_intAsVariable_16, logic_SubGraph_SaveLoadInt_uniqueID_16);
	}

	private void Relay_In_17()
	{
		logic_uScript_HideArrow_uScript_HideArrow_17.In();
		if (logic_uScript_HideArrow_uScript_HideArrow_17.Out)
		{
			Relay_In_118();
		}
	}

	private void Relay_Pause_20()
	{
		logic_uScript_PausePopulation_uScript_PausePopulation_20.Pause();
		if (logic_uScript_PausePopulation_uScript_PausePopulation_20.Out)
		{
			Relay_In_3();
		}
	}

	private void Relay_UnPause_20()
	{
		logic_uScript_PausePopulation_uScript_PausePopulation_20.UnPause();
		if (logic_uScript_PausePopulation_uScript_PausePopulation_20.Out)
		{
			Relay_In_3();
		}
	}

	private void Relay_Block_Attached_24()
	{
		Relay_True_90();
	}

	private void Relay_In_24()
	{
		logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_Block_24 = local_FusionMachineBlock_TankBlock;
		logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_CraftingBaseTech_24 = local_FusionCraftingBaseTech_Tank;
		int num = 0;
		Array array = ghostBlockFusionMachine;
		if (logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_ghostBlockData_24.Length != num + array.Length)
		{
			Array.Resize(ref logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_ghostBlockData_24, num + array.Length);
		}
		Array.Copy(array, 0, logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_ghostBlockData_24, num, array.Length);
		num += array.Length;
		logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_BlockType_24 = BlockTypeFusionMachine;
		logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_SubGraph_Crafting_Tutorial_AttachBlockToBase_24.In(logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_Block_24, logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_CraftingBaseTech_24, logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_ghostBlockData_24, logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_BlockType_24, logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_BlockPosition_24);
	}

	private void Relay_Output1_25()
	{
		Relay_In_51();
	}

	private void Relay_Output2_25()
	{
		Relay_In_83();
	}

	private void Relay_Output3_25()
	{
		Relay_In_237();
	}

	private void Relay_Output4_25()
	{
	}

	private void Relay_Output5_25()
	{
	}

	private void Relay_Output6_25()
	{
	}

	private void Relay_Output7_25()
	{
	}

	private void Relay_Output8_25()
	{
	}

	private void Relay_In_25()
	{
		logic_uScriptCon_ManualSwitch_CurrentOutput_25 = local_Stage_System_Int32;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_25.In(logic_uScriptCon_ManualSwitch_CurrentOutput_25);
	}

	private void Relay_SaveEvent_27()
	{
		Relay_Save_16();
	}

	private void Relay_LoadEvent_27()
	{
		Relay_Load_16();
	}

	private void Relay_RestartEvent_27()
	{
		Relay_Restart_16();
	}

	private void Relay_Save_Out_32()
	{
		Relay_Save_6();
	}

	private void Relay_Load_Out_32()
	{
		Relay_Load_6();
	}

	private void Relay_Restart_Out_32()
	{
		Relay_Set_False_6();
	}

	private void Relay_Save_32()
	{
		logic_SubGraph_SaveLoadBool_boolean_32 = local_msgBaseFoundShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_32 = local_msgBaseFoundShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_32.Save(ref logic_SubGraph_SaveLoadBool_boolean_32, logic_SubGraph_SaveLoadBool_boolAsVariable_32, logic_SubGraph_SaveLoadBool_uniqueID_32);
	}

	private void Relay_Load_32()
	{
		logic_SubGraph_SaveLoadBool_boolean_32 = local_msgBaseFoundShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_32 = local_msgBaseFoundShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_32.Load(ref logic_SubGraph_SaveLoadBool_boolean_32, logic_SubGraph_SaveLoadBool_boolAsVariable_32, logic_SubGraph_SaveLoadBool_uniqueID_32);
	}

	private void Relay_Set_True_32()
	{
		logic_SubGraph_SaveLoadBool_boolean_32 = local_msgBaseFoundShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_32 = local_msgBaseFoundShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_32.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_32, logic_SubGraph_SaveLoadBool_boolAsVariable_32, logic_SubGraph_SaveLoadBool_uniqueID_32);
	}

	private void Relay_Set_False_32()
	{
		logic_SubGraph_SaveLoadBool_boolean_32 = local_msgBaseFoundShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_32 = local_msgBaseFoundShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_32.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_32, logic_SubGraph_SaveLoadBool_boolAsVariable_32, logic_SubGraph_SaveLoadBool_uniqueID_32);
	}

	private void Relay_Save_Out_35()
	{
		Relay_Save_32();
	}

	private void Relay_Load_Out_35()
	{
		Relay_Load_32();
	}

	private void Relay_Restart_Out_35()
	{
		Relay_Set_False_32();
	}

	private void Relay_Save_35()
	{
		logic_SubGraph_SaveLoadBool_boolean_35 = local_IsFusionMachineAttached_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_35 = local_IsFusionMachineAttached_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_35.Save(ref logic_SubGraph_SaveLoadBool_boolean_35, logic_SubGraph_SaveLoadBool_boolAsVariable_35, logic_SubGraph_SaveLoadBool_uniqueID_35);
	}

	private void Relay_Load_35()
	{
		logic_SubGraph_SaveLoadBool_boolean_35 = local_IsFusionMachineAttached_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_35 = local_IsFusionMachineAttached_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_35.Load(ref logic_SubGraph_SaveLoadBool_boolean_35, logic_SubGraph_SaveLoadBool_boolAsVariable_35, logic_SubGraph_SaveLoadBool_uniqueID_35);
	}

	private void Relay_Set_True_35()
	{
		logic_SubGraph_SaveLoadBool_boolean_35 = local_IsFusionMachineAttached_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_35 = local_IsFusionMachineAttached_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_35.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_35, logic_SubGraph_SaveLoadBool_boolAsVariable_35, logic_SubGraph_SaveLoadBool_uniqueID_35);
	}

	private void Relay_Set_False_35()
	{
		logic_SubGraph_SaveLoadBool_boolean_35 = local_IsFusionMachineAttached_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_35 = local_IsFusionMachineAttached_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_35.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_35, logic_SubGraph_SaveLoadBool_boolAsVariable_35, logic_SubGraph_SaveLoadBool_uniqueID_35);
	}

	private void Relay_Out_36()
	{
		Relay_In_130();
	}

	private void Relay_In_36()
	{
		int num = 0;
		Array array = blockSpawnDataFusionMachine;
		if (logic_SubGraph_Crafting_Tutorial_ManageBlock_blockSpawnData_36.Length != num + array.Length)
		{
			Array.Resize(ref logic_SubGraph_Crafting_Tutorial_ManageBlock_blockSpawnData_36, num + array.Length);
		}
		Array.Copy(array, 0, logic_SubGraph_Crafting_Tutorial_ManageBlock_blockSpawnData_36, num, array.Length);
		num += array.Length;
		logic_SubGraph_Crafting_Tutorial_ManageBlock_block_36 = local_FusionMachineBlock_TankBlock;
		logic_SubGraph_Crafting_Tutorial_ManageBlock_msgBlockOutsideArea_36 = msgBlockOutsideArea;
		logic_SubGraph_Crafting_Tutorial_ManageBlock_messageSpeaker_36 = messageSpeaker;
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_36.In(ref logic_SubGraph_Crafting_Tutorial_ManageBlock_blockSpawnData_36, logic_SubGraph_Crafting_Tutorial_ManageBlock_blockIndex_36, ref logic_SubGraph_Crafting_Tutorial_ManageBlock_block_36, logic_SubGraph_Crafting_Tutorial_ManageBlock_allowAnchoring_36, logic_SubGraph_Crafting_Tutorial_ManageBlock_msgBlockOutsideArea_36, logic_SubGraph_Crafting_Tutorial_ManageBlock_messageSpeaker_36);
	}

	private void Relay_In_37()
	{
		logic_uScript_IsPlayerInRangeOfTech_tech_37 = local_FusionCraftingBaseTech_Tank;
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_37.In(logic_uScript_IsPlayerInRangeOfTech_tech_37, logic_uScript_IsPlayerInRangeOfTech_range_37, logic_uScript_IsPlayerInRangeOfTech_techs_37);
		if (logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_37.InRange)
		{
			Relay_In_45();
		}
	}

	private void Relay_In_39()
	{
		int num = 0;
		Array array = blockSpawnDataFusionMachine;
		if (logic_uScript_SpawnBlocksFromData_blockData_39.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SpawnBlocksFromData_blockData_39, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SpawnBlocksFromData_blockData_39, num, array.Length);
		num += array.Length;
		logic_uScript_SpawnBlocksFromData_ownerNode_39 = owner_Connection_54;
		logic_uScript_SpawnBlocksFromData_uScript_SpawnBlocksFromData_39.In(logic_uScript_SpawnBlocksFromData_blockData_39, logic_uScript_SpawnBlocksFromData_ownerNode_39);
		if (logic_uScript_SpawnBlocksFromData_uScript_SpawnBlocksFromData_39.Out)
		{
			Relay_In_4();
		}
	}

	private void Relay_Pause_40()
	{
		logic_uScript_PausePopulation_uScript_PausePopulation_40.Pause();
		if (logic_uScript_PausePopulation_uScript_PausePopulation_40.Out)
		{
			Relay_True_43();
		}
	}

	private void Relay_UnPause_40()
	{
		logic_uScript_PausePopulation_uScript_PausePopulation_40.UnPause();
		if (logic_uScript_PausePopulation_uScript_PausePopulation_40.Out)
		{
			Relay_True_43();
		}
	}

	private void Relay_In_41()
	{
		logic_uScript_IsPlayerInRangeOfTech_tech_41 = local_FusionCraftingBaseTech_Tank;
		logic_uScript_IsPlayerInRangeOfTech_range_41 = distBaseFound;
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_41.In(logic_uScript_IsPlayerInRangeOfTech_tech_41, logic_uScript_IsPlayerInRangeOfTech_range_41, logic_uScript_IsPlayerInRangeOfTech_techs_41);
		bool inRange = logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_41.InRange;
		bool outOfRange = logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_41.OutOfRange;
		if (inRange)
		{
			Relay_Pause_40();
		}
		if (outOfRange)
		{
			Relay_UnPause_20();
		}
	}

	private void Relay_True_43()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_43.True(out logic_uScriptAct_SetBool_Target_43);
		local_NearBase_System_Boolean = logic_uScriptAct_SetBool_Target_43;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_43.Out)
		{
			Relay_In_25();
		}
	}

	private void Relay_False_43()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_43.False(out logic_uScriptAct_SetBool_Target_43);
		local_NearBase_System_Boolean = logic_uScriptAct_SetBool_Target_43;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_43.Out)
		{
			Relay_In_25();
		}
	}

	private void Relay_Save_Out_44()
	{
		Relay_Save_147();
	}

	private void Relay_Load_Out_44()
	{
		Relay_Load_147();
	}

	private void Relay_Restart_Out_44()
	{
		Relay_Set_False_147();
	}

	private void Relay_Save_44()
	{
		logic_SubGraph_SaveLoadBool_boolean_44 = local_msgFusionMachineSpawnedShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_44 = local_msgFusionMachineSpawnedShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_44.Save(ref logic_SubGraph_SaveLoadBool_boolean_44, logic_SubGraph_SaveLoadBool_boolAsVariable_44, logic_SubGraph_SaveLoadBool_uniqueID_44);
	}

	private void Relay_Load_44()
	{
		logic_SubGraph_SaveLoadBool_boolean_44 = local_msgFusionMachineSpawnedShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_44 = local_msgFusionMachineSpawnedShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_44.Load(ref logic_SubGraph_SaveLoadBool_boolean_44, logic_SubGraph_SaveLoadBool_boolAsVariable_44, logic_SubGraph_SaveLoadBool_uniqueID_44);
	}

	private void Relay_Set_True_44()
	{
		logic_SubGraph_SaveLoadBool_boolean_44 = local_msgFusionMachineSpawnedShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_44 = local_msgFusionMachineSpawnedShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_44.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_44, logic_SubGraph_SaveLoadBool_boolAsVariable_44, logic_SubGraph_SaveLoadBool_uniqueID_44);
	}

	private void Relay_Set_False_44()
	{
		logic_SubGraph_SaveLoadBool_boolean_44 = local_msgFusionMachineSpawnedShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_44 = local_msgFusionMachineSpawnedShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_44.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_44, logic_SubGraph_SaveLoadBool_boolAsVariable_44, logic_SubGraph_SaveLoadBool_uniqueID_44);
	}

	private void Relay_In_45()
	{
		logic_uScriptCon_CompareBool_Bool_45 = local_msgIntroShown_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_45.In(logic_uScriptCon_CompareBool_Bool_45);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_45.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_45.False;
		if (num)
		{
			Relay_In_41();
		}
		if (flag)
		{
			Relay_True_64();
		}
	}

	private void Relay_Save_Out_49()
	{
		Relay_Save_35();
	}

	private void Relay_Load_Out_49()
	{
		Relay_Load_35();
	}

	private void Relay_Restart_Out_49()
	{
		Relay_Set_False_35();
	}

	private void Relay_Save_49()
	{
		logic_SubGraph_SaveLoadBool_boolean_49 = local_FusionMachineSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_49 = local_FusionMachineSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_49.Save(ref logic_SubGraph_SaveLoadBool_boolean_49, logic_SubGraph_SaveLoadBool_boolAsVariable_49, logic_SubGraph_SaveLoadBool_uniqueID_49);
	}

	private void Relay_Load_49()
	{
		logic_SubGraph_SaveLoadBool_boolean_49 = local_FusionMachineSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_49 = local_FusionMachineSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_49.Load(ref logic_SubGraph_SaveLoadBool_boolean_49, logic_SubGraph_SaveLoadBool_boolAsVariable_49, logic_SubGraph_SaveLoadBool_uniqueID_49);
	}

	private void Relay_Set_True_49()
	{
		logic_SubGraph_SaveLoadBool_boolean_49 = local_FusionMachineSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_49 = local_FusionMachineSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_49.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_49, logic_SubGraph_SaveLoadBool_boolAsVariable_49, logic_SubGraph_SaveLoadBool_uniqueID_49);
	}

	private void Relay_Set_False_49()
	{
		logic_SubGraph_SaveLoadBool_boolean_49 = local_FusionMachineSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_49 = local_FusionMachineSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_49.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_49, logic_SubGraph_SaveLoadBool_boolAsVariable_49, logic_SubGraph_SaveLoadBool_uniqueID_49);
	}

	private void Relay_In_50()
	{
		logic_uScript_AddMessage_messageData_50 = msg02BaseFound;
		logic_uScript_AddMessage_speaker_50 = messageSpeaker;
		logic_uScript_AddMessage_Return_50 = logic_uScript_AddMessage_uScript_AddMessage_50.In(logic_uScript_AddMessage_messageData_50, logic_uScript_AddMessage_speaker_50);
		if (logic_uScript_AddMessage_uScript_AddMessage_50.Shown)
		{
			Relay_True_71();
		}
	}

	private void Relay_Out_51()
	{
	}

	private void Relay_In_51()
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_51 = local_Stage_System_Int32;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_51.In(ref logic_SubGraph_CompleteObjectiveStage_objectiveStage_51, logic_SubGraph_CompleteObjectiveStage_isFinalObjective_51);
	}

	private void Relay_True_55()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_55.True(out logic_uScriptAct_SetBool_Target_55);
		local_NearBase_System_Boolean = logic_uScriptAct_SetBool_Target_55;
	}

	private void Relay_False_55()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_55.False(out logic_uScriptAct_SetBool_Target_55);
		local_NearBase_System_Boolean = logic_uScriptAct_SetBool_Target_55;
	}

	private void Relay_In_56()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_56.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_56.Out)
		{
			Relay_In_130();
		}
	}

	private void Relay_In_60()
	{
		logic_uScript_LockTechInteraction_tech_60 = local_FusionCraftingBaseTech_Tank;
		logic_uScript_LockTechInteraction_uScript_LockTechInteraction_60.In(logic_uScript_LockTechInteraction_tech_60, logic_uScript_LockTechInteraction_excludedBlocks_60, logic_uScript_LockTechInteraction_excludedUniqueBlocks_60);
		if (logic_uScript_LockTechInteraction_uScript_LockTechInteraction_60.Out)
		{
			Relay_In_67();
		}
	}

	private void Relay_True_64()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_64.True(out logic_uScriptAct_SetBool_Target_64);
		local_msgIntroShown_System_Boolean = logic_uScriptAct_SetBool_Target_64;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_64.Out)
		{
			Relay_In_77();
		}
	}

	private void Relay_False_64()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_64.False(out logic_uScriptAct_SetBool_Target_64);
		local_msgIntroShown_System_Boolean = logic_uScriptAct_SetBool_Target_64;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_64.Out)
		{
			Relay_In_77();
		}
	}

	private void Relay_True_65()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_65.True(out logic_uScriptAct_SetBool_Target_65);
		local_FusionMachineSpawned_System_Boolean = logic_uScriptAct_SetBool_Target_65;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_65.Out)
		{
			Relay_In_39();
		}
	}

	private void Relay_False_65()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_65.False(out logic_uScriptAct_SetBool_Target_65);
		local_FusionMachineSpawned_System_Boolean = logic_uScriptAct_SetBool_Target_65;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_65.Out)
		{
			Relay_In_39();
		}
	}

	private void Relay_In_67()
	{
		logic_uScript_LockTechStacks_tech_67 = local_FusionCraftingBaseTech_Tank;
		logic_uScript_LockTechStacks_uScript_LockTechStacks_67.In(logic_uScript_LockTechStacks_tech_67);
		if (logic_uScript_LockTechStacks_uScript_LockTechStacks_67.Out)
		{
			Relay_In_320();
		}
	}

	private void Relay_Out_68()
	{
		Relay_In_233();
	}

	private void Relay_In_68()
	{
		int num = 0;
		Array array = baseSpawnData;
		if (logic_SubGraph_Crafting_Tutorial_Init_baseSpawnData_68.Length != num + array.Length)
		{
			Array.Resize(ref logic_SubGraph_Crafting_Tutorial_Init_baseSpawnData_68, num + array.Length);
		}
		Array.Copy(array, 0, logic_SubGraph_Crafting_Tutorial_Init_baseSpawnData_68, num, array.Length);
		num += array.Length;
		int num2 = 0;
		Array array2 = blockSpawnData;
		if (logic_SubGraph_Crafting_Tutorial_Init_blockSpawnData_68.Length != num2 + array2.Length)
		{
			Array.Resize(ref logic_SubGraph_Crafting_Tutorial_Init_blockSpawnData_68, num2 + array2.Length);
		}
		Array.Copy(array2, 0, logic_SubGraph_Crafting_Tutorial_Init_blockSpawnData_68, num2, array2.Length);
		num2 += array2.Length;
		int num3 = 0;
		Array nPCSpawnData = NPCSpawnData;
		if (logic_SubGraph_Crafting_Tutorial_Init_NPCSpawnData_68.Length != num3 + nPCSpawnData.Length)
		{
			Array.Resize(ref logic_SubGraph_Crafting_Tutorial_Init_NPCSpawnData_68, num3 + nPCSpawnData.Length);
		}
		Array.Copy(nPCSpawnData, 0, logic_SubGraph_Crafting_Tutorial_Init_NPCSpawnData_68, num3, nPCSpawnData.Length);
		num3 += nPCSpawnData.Length;
		logic_SubGraph_Crafting_Tutorial_Init_completedBasePreset_68 = completedBasePreset;
		logic_SubGraph_Crafting_Tutorial_Init_basePosition_68 = basePosition;
		logic_SubGraph_Crafting_Tutorial_Init_clearSceneryRadius_68 = clearSceneryRadius;
		logic_SubGraph_Crafting_Tutorial_Init_CraftingBaseTech_68 = local_FusionCraftingBaseTech_Tank;
		logic_SubGraph_Crafting_Tutorial_Init_NPCTech_68 = local_NPCTech_Tank;
		logic_SubGraph_Crafting_Tutorial_Init_SubGraph_Crafting_Tutorial_Init_68.In(logic_SubGraph_Crafting_Tutorial_Init_baseSpawnData_68, logic_SubGraph_Crafting_Tutorial_Init_blockSpawnData_68, logic_SubGraph_Crafting_Tutorial_Init_NPCSpawnData_68, logic_SubGraph_Crafting_Tutorial_Init_completedBasePreset_68, logic_SubGraph_Crafting_Tutorial_Init_useNPCAsTutorialTech_68, logic_SubGraph_Crafting_Tutorial_Init_spawnBase_68, logic_SubGraph_Crafting_Tutorial_Init_basePosition_68, logic_SubGraph_Crafting_Tutorial_Init_clearSceneryRadius_68, ref logic_SubGraph_Crafting_Tutorial_Init_CraftingBaseTech_68, ref logic_SubGraph_Crafting_Tutorial_Init_NPCTech_68);
	}

	private void Relay_True_71()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_71.True(out logic_uScriptAct_SetBool_Target_71);
		local_msgBaseFoundShown_System_Boolean = logic_uScriptAct_SetBool_Target_71;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_71.Out)
		{
			Relay_In_81();
		}
	}

	private void Relay_False_71()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_71.False(out logic_uScriptAct_SetBool_Target_71);
		local_msgBaseFoundShown_System_Boolean = logic_uScriptAct_SetBool_Target_71;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_71.Out)
		{
			Relay_In_81();
		}
	}

	private void Relay_In_72()
	{
		logic_uScriptCon_CompareBool_Bool_72 = local_FusionMachineSpawned_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_72.In(logic_uScriptCon_CompareBool_Bool_72);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_72.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_72.False;
		if (num)
		{
			Relay_In_36();
		}
		if (flag)
		{
			Relay_In_56();
		}
	}

	private void Relay_OnUpdate_76()
	{
		Relay_In_68();
	}

	private void Relay_OnSuspend_76()
	{
	}

	private void Relay_OnResume_76()
	{
	}

	private void Relay_In_77()
	{
		logic_uScript_AddMessage_messageData_77 = msg01Intro;
		logic_uScript_AddMessage_speaker_77 = messageSpeaker;
		logic_uScript_AddMessage_Return_77 = logic_uScript_AddMessage_uScript_AddMessage_77.In(logic_uScript_AddMessage_messageData_77, logic_uScript_AddMessage_speaker_77);
		if (logic_uScript_AddMessage_uScript_AddMessage_77.Out)
		{
			Relay_In_41();
		}
	}

	private void Relay_Out_80()
	{
	}

	private void Relay_In_80()
	{
		logic_SubGraph_LoadObjectiveStates_currentObjective_80 = local_Stage_System_Int32;
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_80.In(logic_SubGraph_LoadObjectiveStates_currentObjective_80);
	}

	private void Relay_In_81()
	{
		logic_uScriptCon_CompareBool_Bool_81 = local_FusionMachineSpawned_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_81.In(logic_uScriptCon_CompareBool_Bool_81);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_81.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_81.False;
		if (num)
		{
			Relay_In_4();
		}
		if (flag)
		{
			Relay_True_65();
		}
	}

	private void Relay_In_83()
	{
		logic_uScriptCon_CompareBool_Bool_83 = local_msgBaseFoundShown_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_83.In(logic_uScriptCon_CompareBool_Bool_83);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_83.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_83.False;
		if (num)
		{
			Relay_In_81();
		}
		if (flag)
		{
			Relay_In_50();
		}
	}

	private void Relay_True_90()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_90.True(out logic_uScriptAct_SetBool_Target_90);
		local_IsFusionMachineAttached_System_Boolean = logic_uScriptAct_SetBool_Target_90;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_90.Out)
		{
			Relay_In_94();
		}
	}

	private void Relay_False_90()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_90.False(out logic_uScriptAct_SetBool_Target_90);
		local_IsFusionMachineAttached_System_Boolean = logic_uScriptAct_SetBool_Target_90;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_90.Out)
		{
			Relay_In_94();
		}
	}

	private void Relay_In_94()
	{
		logic_uScript_HideHUDElement_hudElement_94 = local_CraftingMenu_ManHUD_HUDElementType;
		logic_uScript_HideHUDElement_uScript_HideHUDElement_94.In(logic_uScript_HideHUDElement_hudElement_94);
		if (logic_uScript_HideHUDElement_uScript_HideHUDElement_94.Out)
		{
			Relay_In_9();
		}
	}

	private void Relay_Save_Out_95()
	{
		Relay_Save_97();
	}

	private void Relay_Load_Out_95()
	{
		Relay_Load_97();
	}

	private void Relay_Restart_Out_95()
	{
		Relay_Set_False_97();
	}

	private void Relay_Save_95()
	{
		logic_SubGraph_SaveLoadBool_boolean_95 = local_CraftingMenuOpened_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_95 = local_CraftingMenuOpened_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_95.Save(ref logic_SubGraph_SaveLoadBool_boolean_95, logic_SubGraph_SaveLoadBool_boolAsVariable_95, logic_SubGraph_SaveLoadBool_uniqueID_95);
	}

	private void Relay_Load_95()
	{
		logic_SubGraph_SaveLoadBool_boolean_95 = local_CraftingMenuOpened_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_95 = local_CraftingMenuOpened_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_95.Load(ref logic_SubGraph_SaveLoadBool_boolean_95, logic_SubGraph_SaveLoadBool_boolAsVariable_95, logic_SubGraph_SaveLoadBool_uniqueID_95);
	}

	private void Relay_Set_True_95()
	{
		logic_SubGraph_SaveLoadBool_boolean_95 = local_CraftingMenuOpened_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_95 = local_CraftingMenuOpened_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_95.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_95, logic_SubGraph_SaveLoadBool_boolAsVariable_95, logic_SubGraph_SaveLoadBool_uniqueID_95);
	}

	private void Relay_Set_False_95()
	{
		logic_SubGraph_SaveLoadBool_boolean_95 = local_CraftingMenuOpened_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_95 = local_CraftingMenuOpened_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_95.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_95, logic_SubGraph_SaveLoadBool_boolAsVariable_95, logic_SubGraph_SaveLoadBool_uniqueID_95);
	}

	private void Relay_Save_Out_96()
	{
	}

	private void Relay_Load_Out_96()
	{
		Relay_In_80();
	}

	private void Relay_Restart_Out_96()
	{
		Relay_False_55();
	}

	private void Relay_Save_96()
	{
		logic_SubGraph_SaveLoadBool_boolean_96 = local_BlockCrafted_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_96 = local_BlockCrafted_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_96.Save(ref logic_SubGraph_SaveLoadBool_boolean_96, logic_SubGraph_SaveLoadBool_boolAsVariable_96, logic_SubGraph_SaveLoadBool_uniqueID_96);
	}

	private void Relay_Load_96()
	{
		logic_SubGraph_SaveLoadBool_boolean_96 = local_BlockCrafted_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_96 = local_BlockCrafted_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_96.Load(ref logic_SubGraph_SaveLoadBool_boolean_96, logic_SubGraph_SaveLoadBool_boolAsVariable_96, logic_SubGraph_SaveLoadBool_uniqueID_96);
	}

	private void Relay_Set_True_96()
	{
		logic_SubGraph_SaveLoadBool_boolean_96 = local_BlockCrafted_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_96 = local_BlockCrafted_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_96.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_96, logic_SubGraph_SaveLoadBool_boolAsVariable_96, logic_SubGraph_SaveLoadBool_uniqueID_96);
	}

	private void Relay_Set_False_96()
	{
		logic_SubGraph_SaveLoadBool_boolean_96 = local_BlockCrafted_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_96 = local_BlockCrafted_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_96.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_96, logic_SubGraph_SaveLoadBool_boolAsVariable_96, logic_SubGraph_SaveLoadBool_uniqueID_96);
	}

	private void Relay_Save_Out_97()
	{
		Relay_Save_96();
	}

	private void Relay_Load_Out_97()
	{
		Relay_Load_96();
	}

	private void Relay_Restart_Out_97()
	{
		Relay_Set_False_96();
	}

	private void Relay_Save_97()
	{
		logic_SubGraph_SaveLoadBool_boolean_97 = local_CraftingInProgress_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_97 = local_CraftingInProgress_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_97.Save(ref logic_SubGraph_SaveLoadBool_boolean_97, logic_SubGraph_SaveLoadBool_boolAsVariable_97, logic_SubGraph_SaveLoadBool_uniqueID_97);
	}

	private void Relay_Load_97()
	{
		logic_SubGraph_SaveLoadBool_boolean_97 = local_CraftingInProgress_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_97 = local_CraftingInProgress_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_97.Load(ref logic_SubGraph_SaveLoadBool_boolean_97, logic_SubGraph_SaveLoadBool_boolAsVariable_97, logic_SubGraph_SaveLoadBool_uniqueID_97);
	}

	private void Relay_Set_True_97()
	{
		logic_SubGraph_SaveLoadBool_boolean_97 = local_CraftingInProgress_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_97 = local_CraftingInProgress_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_97.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_97, logic_SubGraph_SaveLoadBool_boolAsVariable_97, logic_SubGraph_SaveLoadBool_uniqueID_97);
	}

	private void Relay_Set_False_97()
	{
		logic_SubGraph_SaveLoadBool_boolean_97 = local_CraftingInProgress_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_97 = local_CraftingInProgress_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_97.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_97, logic_SubGraph_SaveLoadBool_boolAsVariable_97, logic_SubGraph_SaveLoadBool_uniqueID_97);
	}

	private void Relay_In_101()
	{
		logic_uScriptCon_CompareBool_Bool_101 = local_CraftingMenuOpened_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_101.In(logic_uScriptCon_CompareBool_Bool_101);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_101.True)
		{
			Relay_In_109();
		}
	}

	private void Relay_True_102()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_102.True(out logic_uScriptAct_SetBool_Target_102);
		local_NearBase_System_Boolean = logic_uScriptAct_SetBool_Target_102;
	}

	private void Relay_False_102()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_102.False(out logic_uScriptAct_SetBool_Target_102);
		local_NearBase_System_Boolean = logic_uScriptAct_SetBool_Target_102;
	}

	private void Relay_In_103()
	{
		logic_uScript_AddMessage_messageData_103 = msgLeavingMissionArea;
		logic_uScript_AddMessage_speaker_103 = messageSpeaker;
		logic_uScript_AddMessage_Return_103 = logic_uScript_AddMessage_uScript_AddMessage_103.In(logic_uScript_AddMessage_messageData_103, logic_uScript_AddMessage_speaker_103);
		if (logic_uScript_AddMessage_uScript_AddMessage_103.Out)
		{
			Relay_False_102();
		}
	}

	private void Relay_In_104()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_104.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_104.Out)
		{
			Relay_In_110();
			Relay_In_101();
		}
	}

	private void Relay_DisableAutoCloseUI_106()
	{
		logic_uScript_DisableClosingCraftingUIWhenTooFarAway_craftingBlock_106 = local_FusionMachineBlock_TankBlock;
		logic_uScript_DisableClosingCraftingUIWhenTooFarAway_uScript_DisableClosingCraftingUIWhenTooFarAway_106.DisableAutoCloseUI(logic_uScript_DisableClosingCraftingUIWhenTooFarAway_craftingBlock_106);
		if (logic_uScript_DisableClosingCraftingUIWhenTooFarAway_uScript_DisableClosingCraftingUIWhenTooFarAway_106.Out)
		{
			Relay_In_104();
		}
	}

	private void Relay_EnableAutoCloseUI_106()
	{
		logic_uScript_DisableClosingCraftingUIWhenTooFarAway_craftingBlock_106 = local_FusionMachineBlock_TankBlock;
		logic_uScript_DisableClosingCraftingUIWhenTooFarAway_uScript_DisableClosingCraftingUIWhenTooFarAway_106.EnableAutoCloseUI(logic_uScript_DisableClosingCraftingUIWhenTooFarAway_craftingBlock_106);
		if (logic_uScript_DisableClosingCraftingUIWhenTooFarAway_uScript_DisableClosingCraftingUIWhenTooFarAway_106.Out)
		{
			Relay_In_104();
		}
	}

	private void Relay_In_108()
	{
		logic_uScript_EnableGlow_targetObject_108 = local_FusionMachineBlock_TankBlock;
		logic_uScript_EnableGlow_uScript_EnableGlow_108.In(logic_uScript_EnableGlow_targetObject_108, logic_uScript_EnableGlow_enable_108);
		if (logic_uScript_EnableGlow_uScript_EnableGlow_108.Out)
		{
			Relay_EnableAutoCloseUI_106();
		}
	}

	private void Relay_In_109()
	{
		logic_uScriptCon_CompareBool_Bool_109 = local_CraftingInProgress_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_109.In(logic_uScriptCon_CompareBool_Bool_109);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_109.False)
		{
			Relay_False_112();
		}
	}

	private void Relay_In_110()
	{
		logic_uScriptCon_CompareBool_Bool_110 = local_NearBase_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_110.In(logic_uScriptCon_CompareBool_Bool_110);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_110.True)
		{
			Relay_In_103();
		}
	}

	private void Relay_True_112()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_112.True(out logic_uScriptAct_SetBool_Target_112);
		local_CraftingMenuOpened_System_Boolean = logic_uScriptAct_SetBool_Target_112;
	}

	private void Relay_False_112()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_112.False(out logic_uScriptAct_SetBool_Target_112);
		local_CraftingMenuOpened_System_Boolean = logic_uScriptAct_SetBool_Target_112;
	}

	private void Relay_In_118()
	{
		logic_uScriptCon_CompareBool_Bool_118 = local_FusionMachineSpawned_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_118.In(logic_uScriptCon_CompareBool_Bool_118);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_118.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_118.False;
		if (num)
		{
			Relay_In_108();
		}
		if (flag)
		{
			Relay_In_119();
		}
	}

	private void Relay_In_119()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_119.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_119.Out)
		{
			Relay_In_104();
		}
	}

	private void Relay_In_123()
	{
		int num = 0;
		if (logic_uScriptAct_Concatenate_A_123.Length <= num)
		{
			Array.Resize(ref logic_uScriptAct_Concatenate_A_123, num + 1);
		}
		logic_uScriptAct_Concatenate_A_123[num++] = local_127_System_String;
		int num2 = 0;
		if (logic_uScriptAct_Concatenate_B_123.Length <= num2)
		{
			Array.Resize(ref logic_uScriptAct_Concatenate_B_123, num2 + 1);
		}
		logic_uScriptAct_Concatenate_B_123[num2++] = local_CraftingMenuOpened_System_Boolean;
		logic_uScriptAct_Concatenate_uScriptAct_Concatenate_123.In(logic_uScriptAct_Concatenate_A_123, logic_uScriptAct_Concatenate_B_123, logic_uScriptAct_Concatenate_Separator_123, out logic_uScriptAct_Concatenate_Result_123);
		local_124_System_String = logic_uScriptAct_Concatenate_Result_123;
		if (logic_uScriptAct_Concatenate_uScriptAct_Concatenate_123.Out)
		{
			Relay_ShowLabel_125();
		}
	}

	private void Relay_ShowLabel_125()
	{
		logic_uScriptAct_PrintText_Text_125 = local_124_System_String;
		logic_uScriptAct_PrintText_uScriptAct_PrintText_125.ShowLabel(logic_uScriptAct_PrintText_Text_125, logic_uScriptAct_PrintText_FontSize_125, logic_uScriptAct_PrintText_FontStyle_125, logic_uScriptAct_PrintText_FontColor_125, logic_uScriptAct_PrintText_textAnchor_125, logic_uScriptAct_PrintText_EdgePadding_125, logic_uScriptAct_PrintText_time_125);
	}

	private void Relay_HideLabel_125()
	{
		logic_uScriptAct_PrintText_Text_125 = local_124_System_String;
		logic_uScriptAct_PrintText_uScriptAct_PrintText_125.HideLabel(logic_uScriptAct_PrintText_Text_125, logic_uScriptAct_PrintText_FontSize_125, logic_uScriptAct_PrintText_FontStyle_125, logic_uScriptAct_PrintText_FontColor_125, logic_uScriptAct_PrintText_textAnchor_125, logic_uScriptAct_PrintText_EdgePadding_125, logic_uScriptAct_PrintText_time_125);
	}

	private void Relay_In_130()
	{
		logic_uScriptCon_CompareBool_Bool_130 = local_CanInteractWithFusionMachine_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_130.In(logic_uScriptCon_CompareBool_Bool_130);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_130.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_130.False;
		if (num)
		{
			Relay_In_131();
		}
		if (flag)
		{
			Relay_In_60();
		}
	}

	private void Relay_In_131()
	{
		logic_uScript_LockTechInteraction_tech_131 = local_FusionCraftingBaseTech_Tank;
		int num = 0;
		if (logic_uScript_LockTechInteraction_excludedBlocks_131.Length <= num)
		{
			Array.Resize(ref logic_uScript_LockTechInteraction_excludedBlocks_131, num + 1);
		}
		logic_uScript_LockTechInteraction_excludedBlocks_131[num++] = BlockTypeFusionMachine;
		int num2 = 0;
		if (logic_uScript_LockTechInteraction_excludedUniqueBlocks_131.Length <= num2)
		{
			Array.Resize(ref logic_uScript_LockTechInteraction_excludedUniqueBlocks_131, num2 + 1);
		}
		logic_uScript_LockTechInteraction_excludedUniqueBlocks_131[num2++] = local_FusionMachineBlock_TankBlock;
		logic_uScript_LockTechInteraction_uScript_LockTechInteraction_131.In(logic_uScript_LockTechInteraction_tech_131, logic_uScript_LockTechInteraction_excludedBlocks_131, logic_uScript_LockTechInteraction_excludedUniqueBlocks_131);
		if (logic_uScript_LockTechInteraction_uScript_LockTechInteraction_131.Out)
		{
			Relay_In_67();
		}
	}

	private void Relay_True_138()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_138.True(out logic_uScriptAct_SetBool_Target_138);
		local_msgFusionMachineSpawnedShown_System_Boolean = logic_uScriptAct_SetBool_Target_138;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_138.Out)
		{
			Relay_In_24();
		}
	}

	private void Relay_False_138()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_138.False(out logic_uScriptAct_SetBool_Target_138);
		local_msgFusionMachineSpawnedShown_System_Boolean = logic_uScriptAct_SetBool_Target_138;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_138.Out)
		{
			Relay_In_24();
		}
	}

	private void Relay_In_139()
	{
		logic_uScript_AddMessage_messageData_139 = msg08FusionMachineSpawned;
		logic_uScript_AddMessage_speaker_139 = messageSpeaker;
		logic_uScript_AddMessage_Return_139 = logic_uScript_AddMessage_uScript_AddMessage_139.In(logic_uScript_AddMessage_messageData_139, logic_uScript_AddMessage_speaker_139);
		if (logic_uScript_AddMessage_uScript_AddMessage_139.Shown)
		{
			Relay_True_138();
		}
	}

	private void Relay_In_141()
	{
		logic_uScript_AddMessage_messageData_141 = msg09AttachFusionMachine;
		logic_uScript_AddMessage_speaker_141 = messageSpeaker;
		logic_uScript_AddMessage_Return_141 = logic_uScript_AddMessage_uScript_AddMessage_141.In(logic_uScript_AddMessage_messageData_141, logic_uScript_AddMessage_speaker_141);
		if (logic_uScript_AddMessage_uScript_AddMessage_141.Out)
		{
			Relay_In_24();
		}
	}

	private void Relay_In_143()
	{
		logic_uScriptCon_CompareBool_Bool_143 = local_msgFusionMachineSpawnedShown_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_143.In(logic_uScriptCon_CompareBool_Bool_143);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_143.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_143.False;
		if (num)
		{
			Relay_In_141();
		}
		if (flag)
		{
			Relay_In_139();
		}
	}

	private void Relay_Save_Out_147()
	{
		Relay_Save_95();
	}

	private void Relay_Load_Out_147()
	{
		Relay_Load_95();
	}

	private void Relay_Restart_Out_147()
	{
		Relay_Set_False_95();
	}

	private void Relay_Save_147()
	{
		logic_SubGraph_SaveLoadBool_boolean_147 = local_CanInteractWithFusionMachine_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_147 = local_CanInteractWithFusionMachine_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_147.Save(ref logic_SubGraph_SaveLoadBool_boolean_147, logic_SubGraph_SaveLoadBool_boolAsVariable_147, logic_SubGraph_SaveLoadBool_uniqueID_147);
	}

	private void Relay_Load_147()
	{
		logic_SubGraph_SaveLoadBool_boolean_147 = local_CanInteractWithFusionMachine_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_147 = local_CanInteractWithFusionMachine_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_147.Load(ref logic_SubGraph_SaveLoadBool_boolean_147, logic_SubGraph_SaveLoadBool_boolAsVariable_147, logic_SubGraph_SaveLoadBool_uniqueID_147);
	}

	private void Relay_Set_True_147()
	{
		logic_SubGraph_SaveLoadBool_boolean_147 = local_CanInteractWithFusionMachine_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_147 = local_CanInteractWithFusionMachine_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_147.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_147, logic_SubGraph_SaveLoadBool_boolAsVariable_147, logic_SubGraph_SaveLoadBool_uniqueID_147);
	}

	private void Relay_Set_False_147()
	{
		logic_SubGraph_SaveLoadBool_boolean_147 = local_CanInteractWithFusionMachine_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_147 = local_CanInteractWithFusionMachine_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_147.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_147, logic_SubGraph_SaveLoadBool_boolAsVariable_147, logic_SubGraph_SaveLoadBool_uniqueID_147);
	}

	private void Relay_In_149()
	{
		logic_uScript_GetNamedBlock_name_149 = local_FusionBlock1Name_System_String;
		logic_uScript_GetNamedBlock_owner_149 = owner_Connection_150;
		logic_uScript_GetNamedBlock_Return_149 = logic_uScript_GetNamedBlock_uScript_GetNamedBlock_149.In(logic_uScript_GetNamedBlock_name_149, logic_uScript_GetNamedBlock_owner_149);
		local_151_TankBlock = logic_uScript_GetNamedBlock_Return_149;
		if (logic_uScript_GetNamedBlock_uScript_GetNamedBlock_149.Out)
		{
			Relay_In_152();
		}
	}

	private void Relay_In_152()
	{
		logic_uScript_LockBlock_block_152 = local_151_TankBlock;
		logic_uScript_LockBlock_uScript_LockBlock_152.In(logic_uScript_LockBlock_block_152, logic_uScript_LockBlock_functionalityToLock_152);
		if (logic_uScript_LockBlock_uScript_LockBlock_152.Out)
		{
			Relay_In_154();
		}
	}

	private void Relay_In_154()
	{
		logic_uScript_LockBlock_block_154 = local_151_TankBlock;
		logic_uScript_LockBlock_uScript_LockBlock_154.In(logic_uScript_LockBlock_block_154, logic_uScript_LockBlock_functionalityToLock_154);
		if (logic_uScript_LockBlock_uScript_LockBlock_154.Out)
		{
			Relay_In_155();
		}
	}

	private void Relay_In_155()
	{
		logic_uScript_LockBlock_block_155 = local_151_TankBlock;
		logic_uScript_LockBlock_uScript_LockBlock_155.In(logic_uScript_LockBlock_block_155, logic_uScript_LockBlock_functionalityToLock_155);
		if (logic_uScript_LockBlock_uScript_LockBlock_155.Out)
		{
			Relay_In_156();
		}
	}

	private void Relay_In_156()
	{
		logic_uScript_LockBlock_block_156 = local_151_TankBlock;
		logic_uScript_LockBlock_uScript_LockBlock_156.In(logic_uScript_LockBlock_block_156, logic_uScript_LockBlock_functionalityToLock_156);
		if (logic_uScript_LockBlock_uScript_LockBlock_156.Out)
		{
			Relay_In_157();
		}
	}

	private void Relay_In_157()
	{
		logic_uScript_LockBlock_uScript_LockBlock_157.In(logic_uScript_LockBlock_block_157, logic_uScript_LockBlock_functionalityToLock_157);
		if (logic_uScript_LockBlock_uScript_LockBlock_157.Out)
		{
			Relay_In_158();
		}
	}

	private void Relay_In_158()
	{
		logic_uScript_GetNamedBlock_name_158 = local_FusionBlock2Name_System_String;
		logic_uScript_GetNamedBlock_owner_158 = owner_Connection_163;
		logic_uScript_GetNamedBlock_Return_158 = logic_uScript_GetNamedBlock_uScript_GetNamedBlock_158.In(logic_uScript_GetNamedBlock_name_158, logic_uScript_GetNamedBlock_owner_158);
		local_160_TankBlock = logic_uScript_GetNamedBlock_Return_158;
		if (logic_uScript_GetNamedBlock_uScript_GetNamedBlock_158.Out)
		{
			Relay_In_165();
		}
	}

	private void Relay_In_161()
	{
		logic_uScript_LockBlock_block_161 = local_160_TankBlock;
		logic_uScript_LockBlock_uScript_LockBlock_161.In(logic_uScript_LockBlock_block_161, logic_uScript_LockBlock_functionalityToLock_161);
		if (logic_uScript_LockBlock_uScript_LockBlock_161.Out)
		{
			Relay_In_166();
		}
	}

	private void Relay_In_162()
	{
		logic_uScript_LockBlock_block_162 = local_160_TankBlock;
		logic_uScript_LockBlock_uScript_LockBlock_162.In(logic_uScript_LockBlock_block_162, logic_uScript_LockBlock_functionalityToLock_162);
		if (logic_uScript_LockBlock_uScript_LockBlock_162.Out)
		{
			Relay_In_161();
		}
	}

	private void Relay_In_164()
	{
		logic_uScript_LockBlock_block_164 = local_160_TankBlock;
		logic_uScript_LockBlock_uScript_LockBlock_164.In(logic_uScript_LockBlock_block_164, logic_uScript_LockBlock_functionalityToLock_164);
		if (logic_uScript_LockBlock_uScript_LockBlock_164.Out)
		{
			Relay_In_162();
		}
	}

	private void Relay_In_165()
	{
		logic_uScript_LockBlock_block_165 = local_160_TankBlock;
		logic_uScript_LockBlock_uScript_LockBlock_165.In(logic_uScript_LockBlock_block_165, logic_uScript_LockBlock_functionalityToLock_165);
		if (logic_uScript_LockBlock_uScript_LockBlock_165.Out)
		{
			Relay_In_164();
		}
	}

	private void Relay_In_166()
	{
		logic_uScript_LockBlock_uScript_LockBlock_166.In(logic_uScript_LockBlock_block_166, logic_uScript_LockBlock_functionalityToLock_166);
		if (logic_uScript_LockBlock_uScript_LockBlock_166.Out)
		{
			Relay_In_72();
		}
	}

	private void Relay_In_167()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_167.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_167.Out)
		{
			Relay_In_168();
		}
	}

	private void Relay_In_168()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_168.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_168.Out)
		{
			Relay_In_169();
		}
	}

	private void Relay_In_169()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_169.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_169.Out)
		{
			Relay_In_72();
		}
	}

	private void Relay_Save_Out_170()
	{
		Relay_Save_318();
	}

	private void Relay_Load_Out_170()
	{
		Relay_Load_318();
	}

	private void Relay_Restart_Out_170()
	{
		Relay_Restart_318();
	}

	private void Relay_Save_170()
	{
		logic_SubGraph_SaveLoadInt_integer_170 = local_Stage2_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_170 = local_Stage2_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_170.Save(logic_SubGraph_SaveLoadInt_restartValue_170, ref logic_SubGraph_SaveLoadInt_integer_170, logic_SubGraph_SaveLoadInt_intAsVariable_170, logic_SubGraph_SaveLoadInt_uniqueID_170);
	}

	private void Relay_Load_170()
	{
		logic_SubGraph_SaveLoadInt_integer_170 = local_Stage2_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_170 = local_Stage2_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_170.Load(logic_SubGraph_SaveLoadInt_restartValue_170, ref logic_SubGraph_SaveLoadInt_integer_170, logic_SubGraph_SaveLoadInt_intAsVariable_170, logic_SubGraph_SaveLoadInt_uniqueID_170);
	}

	private void Relay_Restart_170()
	{
		logic_SubGraph_SaveLoadInt_integer_170 = local_Stage2_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_170 = local_Stage2_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_170.Restart(logic_SubGraph_SaveLoadInt_restartValue_170, ref logic_SubGraph_SaveLoadInt_integer_170, logic_SubGraph_SaveLoadInt_intAsVariable_170, logic_SubGraph_SaveLoadInt_uniqueID_170);
	}

	private void Relay_In_172()
	{
		logic_uScript_GetBlockSpawnDataPositionName_blockData_172 = local_174_SpawnBlockData;
		logic_uScript_GetBlockSpawnDataPositionName_uScript_GetBlockSpawnDataPositionName_172.In(logic_uScript_GetBlockSpawnDataPositionName_blockData_172, out logic_uScript_GetBlockSpawnDataPositionName_positionName_172);
		local_189_System_String = logic_uScript_GetBlockSpawnDataPositionName_positionName_172;
		if (logic_uScript_GetBlockSpawnDataPositionName_uScript_GetBlockSpawnDataPositionName_172.Out)
		{
			Relay_In_178();
		}
	}

	private void Relay_In_173()
	{
		logic_uScript_LockTutorialBlockAttach_block_173 = local_FusionBlock1_TankBlock;
		logic_uScript_LockTutorialBlockAttach_uScript_LockTutorialBlockAttach_173.In(logic_uScript_LockTutorialBlockAttach_block_173);
		if (logic_uScript_LockTutorialBlockAttach_uScript_LockTutorialBlockAttach_173.Out)
		{
			Relay_AtIndex_193();
		}
	}

	private void Relay_In_178()
	{
		logic_uScript_KeepVisibleInEncounterArea_ownerNode_178 = owner_Connection_177;
		logic_uScript_KeepVisibleInEncounterArea_objectToEnclose_178 = local_FusionBlock1_TankBlock;
		logic_uScript_KeepVisibleInEncounterArea_resetPosName_178 = local_189_System_String;
		logic_uScript_KeepVisibleInEncounterArea_uScript_KeepVisibleInEncounterArea_178.In(logic_uScript_KeepVisibleInEncounterArea_ownerNode_178, logic_uScript_KeepVisibleInEncounterArea_objectToEnclose_178, logic_uScript_KeepVisibleInEncounterArea_resetPosName_178, out logic_uScript_KeepVisibleInEncounterArea_positionBeforeReset_178);
		bool insideArea = logic_uScript_KeepVisibleInEncounterArea_uScript_KeepVisibleInEncounterArea_178.InsideArea;
		bool resetFromOutsideArea = logic_uScript_KeepVisibleInEncounterArea_uScript_KeepVisibleInEncounterArea_178.ResetFromOutsideArea;
		if (insideArea)
		{
			Relay_In_195();
		}
		if (resetFromOutsideArea)
		{
			Relay_In_194();
		}
	}

	private void Relay_AtIndex_181()
	{
		int num = 0;
		Array array = local_176_TankBlockArray;
		if (logic_uScript_AccessListBlock_blockList_181.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListBlock_blockList_181, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListBlock_blockList_181, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListBlock_uScript_AccessListBlock_181.AtIndex(ref logic_uScript_AccessListBlock_blockList_181, logic_uScript_AccessListBlock_index_181, out logic_uScript_AccessListBlock_value_181);
		local_176_TankBlockArray = logic_uScript_AccessListBlock_blockList_181;
		local_FusionBlock1_TankBlock = logic_uScript_AccessListBlock_value_181;
		if (logic_uScript_AccessListBlock_uScript_AccessListBlock_181.Out)
		{
			Relay_In_190();
		}
	}

	private void Relay_In_185()
	{
		int num = 0;
		Array array = blockSpawnData;
		if (logic_uScript_GetAndCheckBlocks_blockData_185.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckBlocks_blockData_185, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckBlocks_blockData_185, num, array.Length);
		num += array.Length;
		logic_uScript_GetAndCheckBlocks_ownerNode_185 = owner_Connection_179;
		int num2 = 0;
		Array array2 = local_176_TankBlockArray;
		if (logic_uScript_GetAndCheckBlocks_blocks_185.Length != num2 + array2.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckBlocks_blocks_185, num2 + array2.Length);
		}
		Array.Copy(array2, 0, logic_uScript_GetAndCheckBlocks_blocks_185, num2, array2.Length);
		num2 += array2.Length;
		logic_uScript_GetAndCheckBlocks_uScript_GetAndCheckBlocks_185.In(logic_uScript_GetAndCheckBlocks_blockData_185, logic_uScript_GetAndCheckBlocks_ownerNode_185, ref logic_uScript_GetAndCheckBlocks_blocks_185);
		local_176_TankBlockArray = logic_uScript_GetAndCheckBlocks_blocks_185;
		bool allAlive = logic_uScript_GetAndCheckBlocks_uScript_GetAndCheckBlocks_185.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckBlocks_uScript_GetAndCheckBlocks_185.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckBlocks_uScript_GetAndCheckBlocks_185.AllDead;
		if (allAlive)
		{
			Relay_AtIndex_181();
		}
		if (someAlive)
		{
			Relay_In_192();
		}
		if (allDead)
		{
			Relay_In_192();
		}
	}

	private void Relay_In_186()
	{
		logic_uScript_LockBlockAttach_block_186 = local_FusionBlock1_TankBlock;
		logic_uScript_LockBlockAttach_uScript_LockBlockAttach_186.In(logic_uScript_LockBlockAttach_block_186);
		if (logic_uScript_LockBlockAttach_uScript_LockBlockAttach_186.Out)
		{
			Relay_AtIndex_193();
		}
	}

	private void Relay_In_190()
	{
		logic_uScriptCon_CompareBool_Bool_190 = local_allowAnchoring_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_190.In(logic_uScriptCon_CompareBool_Bool_190);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_190.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_190.False;
		if (num)
		{
			Relay_In_186();
		}
		if (flag)
		{
			Relay_In_173();
		}
	}

	private void Relay_In_191()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_191.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_191.Out)
		{
			Relay_In_195();
		}
	}

	private void Relay_In_192()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_192.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_192.Out)
		{
			Relay_In_191();
		}
	}

	private void Relay_AtIndex_193()
	{
		int num = 0;
		Array array = blockSpawnData;
		if (logic_uScript_AccessListBlockSpawnData_dataList_193.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListBlockSpawnData_dataList_193, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListBlockSpawnData_dataList_193, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListBlockSpawnData_uScript_AccessListBlockSpawnData_193.AtIndex(logic_uScript_AccessListBlockSpawnData_dataList_193, logic_uScript_AccessListBlockSpawnData_index_193, out logic_uScript_AccessListBlockSpawnData_value_193);
		local_174_SpawnBlockData = logic_uScript_AccessListBlockSpawnData_value_193;
		if (logic_uScript_AccessListBlockSpawnData_uScript_AccessListBlockSpawnData_193.Out)
		{
			Relay_In_172();
		}
	}

	private void Relay_In_194()
	{
		logic_uScript_AddMessage_messageData_194 = msgBlockOutsideArea;
		logic_uScript_AddMessage_speaker_194 = messageSpeaker;
		logic_uScript_AddMessage_Return_194 = logic_uScript_AddMessage_uScript_AddMessage_194.In(logic_uScript_AddMessage_messageData_194, logic_uScript_AddMessage_speaker_194);
	}

	private void Relay_In_195()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_195.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_195.Out)
		{
			Relay_In_209();
		}
	}

	private void Relay_In_197()
	{
		logic_uScript_CastBlock_uScript_CastBlock_197.In(logic_uScript_CastBlock_block_197, out logic_uScript_CastBlock_outBlock_197);
		local_FusionBlock1_TankBlock = logic_uScript_CastBlock_outBlock_197;
	}

	private void Relay_In_200()
	{
		logic_uScript_CastBlock_uScript_CastBlock_200.In(logic_uScript_CastBlock_block_200, out logic_uScript_CastBlock_outBlock_200);
		local_FusionBlock2_TankBlock = logic_uScript_CastBlock_outBlock_200;
	}

	private void Relay_In_202()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_202.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_202.Out)
		{
			Relay_In_210();
		}
	}

	private void Relay_In_204()
	{
		logic_uScript_AddMessage_messageData_204 = msgBlockOutsideArea;
		logic_uScript_AddMessage_speaker_204 = messageSpeaker;
		logic_uScript_AddMessage_Return_204 = logic_uScript_AddMessage_uScript_AddMessage_204.In(logic_uScript_AddMessage_messageData_204, logic_uScript_AddMessage_speaker_204);
	}

	private void Relay_In_205()
	{
		logic_uScriptCon_CompareBool_Bool_205 = local_allowAnchoring_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_205.In(logic_uScriptCon_CompareBool_Bool_205);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_205.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_205.False;
		if (num)
		{
			Relay_In_222();
		}
		if (flag)
		{
			Relay_In_220();
		}
	}

	private void Relay_In_209()
	{
		int num = 0;
		Array array = blockSpawnData;
		if (logic_uScript_GetAndCheckBlocks_blockData_209.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckBlocks_blockData_209, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckBlocks_blockData_209, num, array.Length);
		num += array.Length;
		logic_uScript_GetAndCheckBlocks_ownerNode_209 = owner_Connection_225;
		int num2 = 0;
		Array array2 = local_229_TankBlockArray;
		if (logic_uScript_GetAndCheckBlocks_blocks_209.Length != num2 + array2.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckBlocks_blocks_209, num2 + array2.Length);
		}
		Array.Copy(array2, 0, logic_uScript_GetAndCheckBlocks_blocks_209, num2, array2.Length);
		num2 += array2.Length;
		logic_uScript_GetAndCheckBlocks_uScript_GetAndCheckBlocks_209.In(logic_uScript_GetAndCheckBlocks_blockData_209, logic_uScript_GetAndCheckBlocks_ownerNode_209, ref logic_uScript_GetAndCheckBlocks_blocks_209);
		local_229_TankBlockArray = logic_uScript_GetAndCheckBlocks_blocks_209;
		bool allAlive = logic_uScript_GetAndCheckBlocks_uScript_GetAndCheckBlocks_209.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckBlocks_uScript_GetAndCheckBlocks_209.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckBlocks_uScript_GetAndCheckBlocks_209.AllDead;
		if (allAlive)
		{
			Relay_AtIndex_227();
		}
		if (someAlive)
		{
			Relay_In_218();
		}
		if (allDead)
		{
			Relay_In_218();
		}
	}

	private void Relay_In_210()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_210.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_210.Out)
		{
			Relay_In_149();
		}
	}

	private void Relay_In_213()
	{
		logic_uScript_KeepBlockInvulnerable_block_213 = local_FusionBlock2_TankBlock;
		logic_uScript_KeepBlockInvulnerable_uScript_KeepBlockInvulnerable_213.In(logic_uScript_KeepBlockInvulnerable_block_213);
	}

	private void Relay_In_215()
	{
		logic_uScript_KeepVisibleInEncounterArea_ownerNode_215 = owner_Connection_203;
		logic_uScript_KeepVisibleInEncounterArea_objectToEnclose_215 = local_FusionBlock2_TankBlock;
		logic_uScript_KeepVisibleInEncounterArea_resetPosName_215 = local_212_System_String;
		logic_uScript_KeepVisibleInEncounterArea_uScript_KeepVisibleInEncounterArea_215.In(logic_uScript_KeepVisibleInEncounterArea_ownerNode_215, logic_uScript_KeepVisibleInEncounterArea_objectToEnclose_215, logic_uScript_KeepVisibleInEncounterArea_resetPosName_215, out logic_uScript_KeepVisibleInEncounterArea_positionBeforeReset_215);
		bool insideArea = logic_uScript_KeepVisibleInEncounterArea_uScript_KeepVisibleInEncounterArea_215.InsideArea;
		bool resetFromOutsideArea = logic_uScript_KeepVisibleInEncounterArea_uScript_KeepVisibleInEncounterArea_215.ResetFromOutsideArea;
		if (insideArea)
		{
			Relay_In_210();
		}
		if (resetFromOutsideArea)
		{
			Relay_In_204();
		}
	}

	private void Relay_In_216()
	{
		logic_uScript_GetBlockSpawnDataPositionName_blockData_216 = local_208_SpawnBlockData;
		logic_uScript_GetBlockSpawnDataPositionName_uScript_GetBlockSpawnDataPositionName_216.In(logic_uScript_GetBlockSpawnDataPositionName_blockData_216, out logic_uScript_GetBlockSpawnDataPositionName_positionName_216);
		local_212_System_String = logic_uScript_GetBlockSpawnDataPositionName_positionName_216;
		if (logic_uScript_GetBlockSpawnDataPositionName_uScript_GetBlockSpawnDataPositionName_216.Out)
		{
			Relay_In_215();
		}
	}

	private void Relay_In_218()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_218.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_218.Out)
		{
			Relay_In_202();
		}
	}

	private void Relay_In_220()
	{
		logic_uScript_LockTutorialBlockAttach_block_220 = local_FusionBlock2_TankBlock;
		logic_uScript_LockTutorialBlockAttach_uScript_LockTutorialBlockAttach_220.In(logic_uScript_LockTutorialBlockAttach_block_220);
		if (logic_uScript_LockTutorialBlockAttach_uScript_LockTutorialBlockAttach_220.Out)
		{
			Relay_AtIndex_223();
		}
	}

	private void Relay_In_222()
	{
		logic_uScript_LockBlockAttach_block_222 = local_FusionBlock2_TankBlock;
		logic_uScript_LockBlockAttach_uScript_LockBlockAttach_222.In(logic_uScript_LockBlockAttach_block_222);
		if (logic_uScript_LockBlockAttach_uScript_LockBlockAttach_222.Out)
		{
			Relay_AtIndex_223();
		}
	}

	private void Relay_AtIndex_223()
	{
		int num = 0;
		Array array = blockSpawnData;
		if (logic_uScript_AccessListBlockSpawnData_dataList_223.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListBlockSpawnData_dataList_223, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListBlockSpawnData_dataList_223, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListBlockSpawnData_uScript_AccessListBlockSpawnData_223.AtIndex(logic_uScript_AccessListBlockSpawnData_dataList_223, logic_uScript_AccessListBlockSpawnData_index_223, out logic_uScript_AccessListBlockSpawnData_value_223);
		local_208_SpawnBlockData = logic_uScript_AccessListBlockSpawnData_value_223;
		if (logic_uScript_AccessListBlockSpawnData_uScript_AccessListBlockSpawnData_223.Out)
		{
			Relay_In_216();
		}
	}

	private void Relay_AtIndex_227()
	{
		int num = 0;
		Array array = local_229_TankBlockArray;
		if (logic_uScript_AccessListBlock_blockList_227.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListBlock_blockList_227, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListBlock_blockList_227, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListBlock_uScript_AccessListBlock_227.AtIndex(ref logic_uScript_AccessListBlock_blockList_227, logic_uScript_AccessListBlock_index_227, out logic_uScript_AccessListBlock_value_227);
		local_229_TankBlockArray = logic_uScript_AccessListBlock_blockList_227;
		local_FusionBlock2_TankBlock = logic_uScript_AccessListBlock_value_227;
		if (logic_uScript_AccessListBlock_uScript_AccessListBlock_227.Out)
		{
			Relay_In_205();
		}
	}

	private void Relay_In_230()
	{
		logic_uScript_KeepBlockInvulnerable_block_230 = local_FusionBlock1_TankBlock;
		logic_uScript_KeepBlockInvulnerable_uScript_KeepBlockInvulnerable_230.In(logic_uScript_KeepBlockInvulnerable_block_230);
	}

	private void Relay_In_233()
	{
		logic_uScriptCon_CompareBool_Bool_233 = local_CanSuckUpBlocks_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_233.In(logic_uScriptCon_CompareBool_Bool_233);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_233.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_233.False;
		if (num)
		{
			Relay_In_234();
		}
		if (flag)
		{
			Relay_In_185();
		}
	}

	private void Relay_In_234()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_234.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_234.Out)
		{
			Relay_In_235();
		}
	}

	private void Relay_In_235()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_235.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_235.Out)
		{
			Relay_In_167();
		}
	}

	private void Relay_Output1_237()
	{
		Relay_True_238();
	}

	private void Relay_Output2_237()
	{
		Relay_In_240();
	}

	private void Relay_Output3_237()
	{
	}

	private void Relay_Output4_237()
	{
	}

	private void Relay_Output5_237()
	{
	}

	private void Relay_Output6_237()
	{
	}

	private void Relay_Output7_237()
	{
	}

	private void Relay_Output8_237()
	{
	}

	private void Relay_In_237()
	{
		logic_uScriptCon_ManualSwitch_CurrentOutput_237 = local_Stage2_System_Int32;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_237.In(logic_uScriptCon_ManualSwitch_CurrentOutput_237);
	}

	private void Relay_True_238()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_238.True(out logic_uScriptAct_SetBool_Target_238);
		local_CanInteractWithFusionMachine_System_Boolean = logic_uScriptAct_SetBool_Target_238;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_238.Out)
		{
			Relay_In_242();
		}
	}

	private void Relay_False_238()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_238.False(out logic_uScriptAct_SetBool_Target_238);
		local_CanInteractWithFusionMachine_System_Boolean = logic_uScriptAct_SetBool_Target_238;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_238.Out)
		{
			Relay_In_242();
		}
	}

	private void Relay_In_240()
	{
		logic_uScriptCon_CompareBool_Bool_240 = local_CraftingInProgress_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_240.In(logic_uScriptCon_CompareBool_Bool_240);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_240.True)
		{
			Relay_True_310();
		}
	}

	private void Relay_In_242()
	{
		logic_uScriptCon_CompareBool_Bool_242 = local_CraftingMenuOpened_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_242.In(logic_uScriptCon_CompareBool_Bool_242);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_242.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_242.False;
		if (num)
		{
			Relay_In_273();
		}
		if (flag)
		{
			Relay_In_249();
		}
	}

	private void Relay_DisableAutoCloseUI_244()
	{
		logic_uScript_DisableClosingCraftingUIWhenTooFarAway_craftingBlock_244 = local_FusionMachineBlock_TankBlock;
		logic_uScript_DisableClosingCraftingUIWhenTooFarAway_uScript_DisableClosingCraftingUIWhenTooFarAway_244.DisableAutoCloseUI(logic_uScript_DisableClosingCraftingUIWhenTooFarAway_craftingBlock_244);
		if (logic_uScript_DisableClosingCraftingUIWhenTooFarAway_uScript_DisableClosingCraftingUIWhenTooFarAway_244.Out)
		{
			Relay_In_248();
		}
	}

	private void Relay_EnableAutoCloseUI_244()
	{
		logic_uScript_DisableClosingCraftingUIWhenTooFarAway_craftingBlock_244 = local_FusionMachineBlock_TankBlock;
		logic_uScript_DisableClosingCraftingUIWhenTooFarAway_uScript_DisableClosingCraftingUIWhenTooFarAway_244.EnableAutoCloseUI(logic_uScript_DisableClosingCraftingUIWhenTooFarAway_craftingBlock_244);
		if (logic_uScript_DisableClosingCraftingUIWhenTooFarAway_uScript_DisableClosingCraftingUIWhenTooFarAway_244.Out)
		{
			Relay_In_248();
		}
	}

	private void Relay_In_245()
	{
		logic_uScript_PointArrowAtVisible_targetObject_245 = local_FusionMachineBlock_TankBlock;
		logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_245.In(logic_uScript_PointArrowAtVisible_targetObject_245, logic_uScript_PointArrowAtVisible_timeToShowFor_245, logic_uScript_PointArrowAtVisible_offset_245);
		if (logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_245.Out)
		{
			Relay_In_254();
		}
	}

	private void Relay_In_248()
	{
		logic_uScript_IsHUDElementLinkedToBlock_hudElement_248 = local_CraftingMenu_ManHUD_HUDElementType;
		logic_uScript_IsHUDElementLinkedToBlock_targetBlock_248 = local_FusionMachineBlock_TankBlock;
		logic_uScript_IsHUDElementLinkedToBlock_uScript_IsHUDElementLinkedToBlock_248.In(logic_uScript_IsHUDElementLinkedToBlock_hudElement_248, logic_uScript_IsHUDElementLinkedToBlock_targetBlock_248);
		if (logic_uScript_IsHUDElementLinkedToBlock_uScript_IsHUDElementLinkedToBlock_248.True)
		{
			Relay_In_261();
		}
	}

	private void Relay_Out_249()
	{
		Relay_In_245();
	}

	private void Relay_Shown_249()
	{
	}

	private void Relay_In_249()
	{
		logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboard_249 = msg10OpenMenu;
		logic_SubGraph_AddMessageWithPadSupport_messageControlPad_249 = msg10OpenMenu_Pad;
		logic_SubGraph_AddMessageWithPadSupport_speaker_249 = messageSpeaker;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_249.In(logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboard_249, logic_SubGraph_AddMessageWithPadSupport_messageControlPad_249, logic_SubGraph_AddMessageWithPadSupport_speaker_249);
	}

	private void Relay_In_254()
	{
		logic_uScript_EnableGlow_targetObject_254 = local_FusionMachineBlock_TankBlock;
		logic_uScript_EnableGlow_uScript_EnableGlow_254.In(logic_uScript_EnableGlow_targetObject_254, logic_uScript_EnableGlow_enable_254);
		if (logic_uScript_EnableGlow_uScript_EnableGlow_254.Out)
		{
			Relay_DisableAutoCloseUI_244();
		}
	}

	private void Relay_In_257()
	{
		logic_uScript_EnableGlow_targetObject_257 = local_FusionMachineBlock_TankBlock;
		logic_uScript_EnableGlow_uScript_EnableGlow_257.In(logic_uScript_EnableGlow_targetObject_257, logic_uScript_EnableGlow_enable_257);
		if (logic_uScript_EnableGlow_uScript_EnableGlow_257.Out)
		{
			Relay_True_258();
		}
	}

	private void Relay_True_258()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_258.True(out logic_uScriptAct_SetBool_Target_258);
		local_CraftingMenuOpened_System_Boolean = logic_uScriptAct_SetBool_Target_258;
	}

	private void Relay_False_258()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_258.False(out logic_uScriptAct_SetBool_Target_258);
		local_CraftingMenuOpened_System_Boolean = logic_uScriptAct_SetBool_Target_258;
	}

	private void Relay_In_261()
	{
		logic_uScript_HideArrow_uScript_HideArrow_261.In();
		if (logic_uScript_HideArrow_uScript_HideArrow_261.Out)
		{
			Relay_In_257();
		}
	}

	private void Relay_In_263()
	{
		logic_uScript_LockHudGroup_uScript_LockHudGroup_263.In(logic_uScript_LockHudGroup_group_263, logic_uScript_LockHudGroup_locked_263);
		if (logic_uScript_LockHudGroup_uScript_LockHudGroup_263.Out)
		{
			Relay_In_281();
		}
	}

	private void Relay_In_268()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_268 = messageTag;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_268.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_268, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_268);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_268.Out)
		{
			Relay_True_282();
		}
	}

	private void Relay_In_270()
	{
		logic_uScript_LockPlayerInput_uScript_LockPlayerInput_270.In(logic_uScript_LockPlayerInput_lockInput_270, logic_uScript_LockPlayerInput_includeCamera_270);
		if (logic_uScript_LockPlayerInput_uScript_LockPlayerInput_270.Out)
		{
			Relay_In_283();
		}
	}

	private void Relay_Out_273()
	{
		Relay_In_277();
	}

	private void Relay_Shown_273()
	{
	}

	private void Relay_In_273()
	{
		logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboard_273 = msg11CraftBlock;
		logic_SubGraph_AddMessageWithPadSupport_messageControlPad_273 = msg11CraftBlock_Pad;
		logic_SubGraph_AddMessageWithPadSupport_speaker_273 = messageSpeaker;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_273.In(logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboard_273, logic_SubGraph_AddMessageWithPadSupport_messageControlPad_273, logic_SubGraph_AddMessageWithPadSupport_speaker_273);
	}

	private void Relay_True_274()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_274.True(out logic_uScriptAct_SetBool_Target_274);
		local_CraftingInProgress_System_Boolean = logic_uScriptAct_SetBool_Target_274;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_274.Out)
		{
			Relay_In_286();
		}
	}

	private void Relay_False_274()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_274.False(out logic_uScriptAct_SetBool_Target_274);
		local_CraftingInProgress_System_Boolean = logic_uScriptAct_SetBool_Target_274;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_274.Out)
		{
			Relay_In_286();
		}
	}

	private void Relay_In_276()
	{
		logic_uScript_CraftingUIHighlightCraftButton_uScript_CraftingUIHighlightCraftButton_276.In(logic_uScript_CraftingUIHighlightCraftButton_targetMenuType_276);
		if (logic_uScript_CraftingUIHighlightCraftButton_uScript_CraftingUIHighlightCraftButton_276.Selected)
		{
			Relay_EnableAutoCloseUI_278();
		}
	}

	private void Relay_In_277()
	{
		logic_uScript_LockPause_uScript_LockPause_277.In(logic_uScript_LockPause_lockPause_277, logic_uScript_LockPause_disabledReason_277);
		if (logic_uScript_LockPause_uScript_LockPause_277.Out)
		{
			Relay_In_263();
		}
	}

	private void Relay_DisableAutoCloseUI_278()
	{
		logic_uScript_DisableClosingCraftingUIWhenTooFarAway_craftingBlock_278 = local_FusionMachineBlock_TankBlock;
		logic_uScript_DisableClosingCraftingUIWhenTooFarAway_uScript_DisableClosingCraftingUIWhenTooFarAway_278.DisableAutoCloseUI(logic_uScript_DisableClosingCraftingUIWhenTooFarAway_craftingBlock_278);
		if (logic_uScript_DisableClosingCraftingUIWhenTooFarAway_uScript_DisableClosingCraftingUIWhenTooFarAway_278.Out)
		{
			Relay_In_268();
		}
	}

	private void Relay_EnableAutoCloseUI_278()
	{
		logic_uScript_DisableClosingCraftingUIWhenTooFarAway_craftingBlock_278 = local_FusionMachineBlock_TankBlock;
		logic_uScript_DisableClosingCraftingUIWhenTooFarAway_uScript_DisableClosingCraftingUIWhenTooFarAway_278.EnableAutoCloseUI(logic_uScript_DisableClosingCraftingUIWhenTooFarAway_craftingBlock_278);
		if (logic_uScript_DisableClosingCraftingUIWhenTooFarAway_uScript_DisableClosingCraftingUIWhenTooFarAway_278.Out)
		{
			Relay_In_268();
		}
	}

	private void Relay_In_281()
	{
		logic_uScript_LockTechInteraction_tech_281 = local_FusionCraftingBaseTech_Tank;
		int num = 0;
		if (logic_uScript_LockTechInteraction_excludedBlocks_281.Length <= num)
		{
			Array.Resize(ref logic_uScript_LockTechInteraction_excludedBlocks_281, num + 1);
		}
		logic_uScript_LockTechInteraction_excludedBlocks_281[num++] = BlockTypeFusionMachine;
		int num2 = 0;
		if (logic_uScript_LockTechInteraction_excludedUniqueBlocks_281.Length <= num2)
		{
			Array.Resize(ref logic_uScript_LockTechInteraction_excludedUniqueBlocks_281, num2 + 1);
		}
		logic_uScript_LockTechInteraction_excludedUniqueBlocks_281[num2++] = local_FusionMachineBlock_TankBlock;
		logic_uScript_LockTechInteraction_uScript_LockTechInteraction_281.In(logic_uScript_LockTechInteraction_tech_281, logic_uScript_LockTechInteraction_excludedBlocks_281, logic_uScript_LockTechInteraction_excludedUniqueBlocks_281);
		if (logic_uScript_LockTechInteraction_uScript_LockTechInteraction_281.Out)
		{
			Relay_In_270();
		}
	}

	private void Relay_True_282()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_282.True(out logic_uScriptAct_SetBool_Target_282);
		local_CanSuckUpBlocks_System_Boolean = logic_uScriptAct_SetBool_Target_282;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_282.Out)
		{
			Relay_True_274();
		}
	}

	private void Relay_False_282()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_282.False(out logic_uScriptAct_SetBool_Target_282);
		local_CanSuckUpBlocks_System_Boolean = logic_uScriptAct_SetBool_Target_282;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_282.Out)
		{
			Relay_True_274();
		}
	}

	private void Relay_In_283()
	{
		logic_uScript_CraftingUIHighlightItem_itemToHighlight_283 = blockTypeToHighlight;
		logic_uScript_CraftingUIHighlightItem_uScript_CraftingUIHighlightItem_283.In(logic_uScript_CraftingUIHighlightItem_targetMenuType_283, logic_uScript_CraftingUIHighlightItem_itemToHighlight_283);
		if (logic_uScript_CraftingUIHighlightItem_uScript_CraftingUIHighlightItem_283.Selected)
		{
			Relay_In_276();
		}
	}

	private void Relay_In_286()
	{
		logic_uScriptAct_AddInt_v2_A_286 = local_Stage2_System_Int32;
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_286.In(logic_uScriptAct_AddInt_v2_A_286, logic_uScriptAct_AddInt_v2_B_286, out logic_uScriptAct_AddInt_v2_IntResult_286, out logic_uScriptAct_AddInt_v2_FloatResult_286);
		local_Stage2_System_Int32 = logic_uScriptAct_AddInt_v2_IntResult_286;
	}

	private void Relay_In_287()
	{
		logic_uScript_LockTechInteraction_tech_287 = local_FusionCraftingBaseTech_Tank;
		logic_uScript_LockTechInteraction_uScript_LockTechInteraction_287.In(logic_uScript_LockTechInteraction_tech_287, logic_uScript_LockTechInteraction_excludedBlocks_287, logic_uScript_LockTechInteraction_excludedUniqueBlocks_287);
		if (logic_uScript_LockTechInteraction_uScript_LockTechInteraction_287.Out)
		{
			Relay_In_293();
		}
	}

	private void Relay_BlockCraftedEvent_288()
	{
		BlockTypeFusionMachine = event_UnityEngine_GameObject_BlockType_288;
		Relay_In_294();
	}

	private void Relay_In_290()
	{
		logic_uScript_AddMessage_messageData_290 = msg14Complete;
		logic_uScript_AddMessage_speaker_290 = messageSpeaker;
		logic_uScript_AddMessage_Return_290 = logic_uScript_AddMessage_uScript_AddMessage_290.In(logic_uScript_AddMessage_messageData_290, logic_uScript_AddMessage_speaker_290);
		if (logic_uScript_AddMessage_uScript_AddMessage_290.Shown)
		{
			Relay_In_316();
		}
	}

	private void Relay_In_291()
	{
		logic_uScript_AddMessage_messageData_291 = msg13CraftingInProgress;
		logic_uScript_AddMessage_speaker_291 = messageSpeaker;
		logic_uScript_AddMessage_Return_291 = logic_uScript_AddMessage_uScript_AddMessage_291.In(logic_uScript_AddMessage_messageData_291, logic_uScript_AddMessage_speaker_291);
		if (logic_uScript_AddMessage_uScript_AddMessage_291.Shown)
		{
			Relay_In_305();
		}
	}

	private void Relay_In_292()
	{
		logic_uScript_LockHudGroup_uScript_LockHudGroup_292.In(logic_uScript_LockHudGroup_group_292, logic_uScript_LockHudGroup_locked_292);
		if (logic_uScript_LockHudGroup_uScript_LockHudGroup_292.Out)
		{
			Relay_In_317();
		}
	}

	private void Relay_In_293()
	{
		logic_uScriptCon_CompareBool_Bool_293 = local_BlockCrafted_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_293.In(logic_uScriptCon_CompareBool_Bool_293);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_293.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_293.False;
		if (num)
		{
			Relay_In_298();
		}
		if (flag)
		{
			Relay_In_302();
		}
	}

	private void Relay_In_294()
	{
		logic_uScript_CompareBlockTypes_A_294 = BlockTypeFusionMachine;
		logic_uScript_CompareBlockTypes_B_294 = BlockTypeFusionMachine;
		logic_uScript_CompareBlockTypes_uScript_CompareBlockTypes_294.In(logic_uScript_CompareBlockTypes_A_294, logic_uScript_CompareBlockTypes_B_294);
		if (logic_uScript_CompareBlockTypes_uScript_CompareBlockTypes_294.EqualTo)
		{
			Relay_True_301();
		}
	}

	private void Relay_Output1_298()
	{
		Relay_In_291();
	}

	private void Relay_Output2_298()
	{
		Relay_In_290();
	}

	private void Relay_Output3_298()
	{
	}

	private void Relay_Output4_298()
	{
	}

	private void Relay_Output5_298()
	{
	}

	private void Relay_Output6_298()
	{
	}

	private void Relay_Output7_298()
	{
	}

	private void Relay_Output8_298()
	{
	}

	private void Relay_In_298()
	{
		logic_uScriptCon_ManualSwitch_CurrentOutput_298 = local_Stage3_System_Int32;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_298.In(logic_uScriptCon_ManualSwitch_CurrentOutput_298);
	}

	private void Relay_True_301()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_301.True(out logic_uScriptAct_SetBool_Target_301);
		local_BlockCrafted_System_Boolean = logic_uScriptAct_SetBool_Target_301;
	}

	private void Relay_False_301()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_301.False(out logic_uScriptAct_SetBool_Target_301);
		local_BlockCrafted_System_Boolean = logic_uScriptAct_SetBool_Target_301;
	}

	private void Relay_In_302()
	{
		logic_uScript_LockPause_uScript_LockPause_302.In(logic_uScript_LockPause_lockPause_302, logic_uScript_LockPause_disabledReason_302);
		if (logic_uScript_LockPause_uScript_LockPause_302.Out)
		{
			Relay_In_292();
		}
	}

	private void Relay_In_305()
	{
		logic_uScriptAct_AddInt_v2_A_305 = local_Stage3_System_Int32;
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_305.In(logic_uScriptAct_AddInt_v2_A_305, logic_uScriptAct_AddInt_v2_B_305, out logic_uScriptAct_AddInt_v2_IntResult_305, out logic_uScriptAct_AddInt_v2_FloatResult_305);
		local_Stage3_System_Int32 = logic_uScriptAct_AddInt_v2_IntResult_305;
	}

	private void Relay_True_310()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_310.True(out logic_uScriptAct_SetBool_Target_310);
		local_CanInteractWithFusionMachine_System_Boolean = logic_uScriptAct_SetBool_Target_310;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_310.Out)
		{
			Relay_In_287();
		}
	}

	private void Relay_False_310()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_310.False(out logic_uScriptAct_SetBool_Target_310);
		local_CanInteractWithFusionMachine_System_Boolean = logic_uScriptAct_SetBool_Target_310;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_310.Out)
		{
			Relay_In_287();
		}
	}

	private void Relay_Out_316()
	{
	}

	private void Relay_In_316()
	{
		logic_SubGraph_Crafting_Tutorial_Finish_craftingBaseTech_316 = local_FusionCraftingBaseTech_Tank;
		logic_SubGraph_Crafting_Tutorial_Finish_NPCTech_316 = local_NPCTech_Tank;
		logic_SubGraph_Crafting_Tutorial_Finish_NPCFlyAwayAI_316 = NPCFlyAwayAI;
		logic_SubGraph_Crafting_Tutorial_Finish_NPCDespawnParticleEffect_316 = NPCDespawnParticleEffect;
		logic_SubGraph_Crafting_Tutorial_Finish_SubGraph_Crafting_Tutorial_Finish_316.In(logic_SubGraph_Crafting_Tutorial_Finish_craftingBaseTech_316, logic_SubGraph_Crafting_Tutorial_Finish_NPCTech_316, logic_SubGraph_Crafting_Tutorial_Finish_NPCFlyAwayAI_316, logic_SubGraph_Crafting_Tutorial_Finish_NPCDespawnParticleEffect_316);
	}

	private void Relay_In_317()
	{
		logic_uScript_LockPlayerInput_uScript_LockPlayerInput_317.In(logic_uScript_LockPlayerInput_lockInput_317, logic_uScript_LockPlayerInput_includeCamera_317);
	}

	private void Relay_Save_Out_318()
	{
		Relay_Save_49();
	}

	private void Relay_Load_Out_318()
	{
		Relay_Load_49();
	}

	private void Relay_Restart_Out_318()
	{
		Relay_Set_False_49();
	}

	private void Relay_Save_318()
	{
		logic_SubGraph_SaveLoadInt_integer_318 = local_Stage3_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_318 = local_Stage3_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_318.Save(logic_SubGraph_SaveLoadInt_restartValue_318, ref logic_SubGraph_SaveLoadInt_integer_318, logic_SubGraph_SaveLoadInt_intAsVariable_318, logic_SubGraph_SaveLoadInt_uniqueID_318);
	}

	private void Relay_Load_318()
	{
		logic_SubGraph_SaveLoadInt_integer_318 = local_Stage3_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_318 = local_Stage3_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_318.Load(logic_SubGraph_SaveLoadInt_restartValue_318, ref logic_SubGraph_SaveLoadInt_integer_318, logic_SubGraph_SaveLoadInt_intAsVariable_318, logic_SubGraph_SaveLoadInt_uniqueID_318);
	}

	private void Relay_Restart_318()
	{
		logic_SubGraph_SaveLoadInt_integer_318 = local_Stage3_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_318 = local_Stage3_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_318.Restart(logic_SubGraph_SaveLoadInt_restartValue_318, ref logic_SubGraph_SaveLoadInt_integer_318, logic_SubGraph_SaveLoadInt_intAsVariable_318, logic_SubGraph_SaveLoadInt_uniqueID_318);
	}

	private void Relay_In_320()
	{
		logic_uScript_DirectEnemiesOutOfEncounter_ownerNode_320 = owner_Connection_321;
		logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_320.In(logic_uScript_DirectEnemiesOutOfEncounter_ownerNode_320);
		if (logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_320.Out)
		{
			Relay_In_1();
		}
	}
}
