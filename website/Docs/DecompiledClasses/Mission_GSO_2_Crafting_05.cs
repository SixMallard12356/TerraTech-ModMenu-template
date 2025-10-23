using System;
using BehaviorDesigner.Runtime;
using UnityEngine;

[Serializable]
[FriendlyName("Untitled", "")]
[NodePath("Graphs")]
public class Mission_GSO_2_Crafting_05 : uScriptLogic
{
	private delegate void ContinueExecution();

	private GameObject parentGameObject;

	private uScript_GUI thisScriptsOnGuiListener;

	private bool m_RegisteredForEvents;

	private ContinueExecution m_ContinueExecution;

	private bool m_Breakpoint;

	private const int MaxRelayCallCount = 1000;

	private int relayCallCount;

	public ChunkTypes[] baseAllowedResourceTypes = new ChunkTypes[0];

	[Multiline(3)]
	public string basePosition = "";

	public SpawnTechData[] baseSpawnData = new SpawnTechData[0];

	public SpawnBlockData[] blockSpawnData = new SpawnBlockData[0];

	public BlockTypes blockTypeFabricator;

	public BlockTypes blockTypeSilo;

	public BlockTypes blockTypeToCraft01 = BlockTypes.GSOWheelHub_111;

	public BlockTypes blockTypeToCraft02 = BlockTypes.GSOWheelHub_111;

	public BlockCategories blockTypeToCraftCategory;

	public ItemTypeInfo blockTypeToHighlight01;

	public ItemTypeInfo blockTypeToHighlight02;

	public float clearSceneryRadius;

	public TankPreset completedBasePreset;

	public float distBaseFound;

	public GhostBlockSpawnData[] ghostBlockRefinery = new GhostBlockSpawnData[0];

	private BlockTypes local_11_BlockTypes = BlockTypes.GSOWheelHub_111;

	private BlockTypes local_69_BlockTypes = BlockTypes.GSOWheelHub_111;

	private TankBlock local_73_TankBlock;

	private bool local_BlockCrafted01_System_Boolean;

	private bool local_BlockCrafted02_System_Boolean;

	private Tank local_CraftingBaseTech_Tank;

	private bool local_CraftingInProgress01_System_Boolean;

	private bool local_CraftingInProgress02_System_Boolean;

	private ManHUD.HUDElementType local_CraftingMenu_ManHUD_HUDElementType = ManHUD.HUDElementType.BlockRecipeSelect;

	private bool local_CraftingMenuOpened01_System_Boolean;

	private bool local_CraftingMenuOpened02_System_Boolean;

	private TankBlock local_FabricatorBlock_TankBlock;

	private string local_Msg_System_String = "";

	private bool local_msgBaseFoundShown_System_Boolean;

	private bool local_msgIntroShown_System_Boolean;

	private bool local_msgRefineryAttachedShown_System_Boolean;

	private bool local_msgRefineryExplanation02Shown_System_Boolean;

	private bool local_msgResourcesInSiloShown_System_Boolean;

	private bool local_NearBase_System_Boolean;

	private Tank local_NPCTech_Tank;

	private TankBlock local_RefineryBlock_TankBlock;

	private int local_Stage_System_Int32 = 1;

	public uScript_AddMessage.MessageSpeaker messageSpeaker;

	[Multiline(3)]
	public string messageTag = "";

	public uScript_AddMessage.MessageData msg01Intro;

	public uScript_AddMessage.MessageData msg02BaseFound;

	public uScript_AddMessage.MessageData msg03ResourcesReminder;

	public uScript_AddMessage.MessageData msg04ResourcesInSilo;

	public uScript_AddMessage.MessageData msg05AttachRefinery;

	public uScript_AddMessage.MessageData msg06CraftBlock01;

	public uScript_AddMessage.MessageData msg07RefineryExplanation01;

	public uScript_AddMessage.MessageData msg08CraftBlock02;

	public uScript_AddMessage.MessageData msg09RefineryExplanation02;

	public uScript_AddMessage.MessageData msg10Complete;

	public uScript_AddMessage.MessageData msgBlockOutsideArea;

	public uScript_AddMessage.MessageData msgLeavingMissionArea;

	public Transform NPCDespawnParticleEffect;

	public ExternalBehaviorTree NPCFlyAwayAI;

	public SpawnTechData[] NPCSpawnData = new SpawnTechData[0];

	public uScript_IsBlockHoldingResources.ResourceQuantity[] resourcesToHoldInSilo = new uScript_IsBlockHoldingResources.ResourceQuantity[0];

	public float timeRepeatResourcesReminder;

	private GameObject owner_Connection_1;

	private GameObject owner_Connection_7;

	private GameObject owner_Connection_10;

	private GameObject owner_Connection_68;

	private GameObject owner_Connection_206;

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

	private uScript_CompareBlockTypes logic_uScript_CompareBlockTypes_uScript_CompareBlockTypes_12 = new uScript_CompareBlockTypes();

	private BlockTypes logic_uScript_CompareBlockTypes_A_12;

	private BlockTypes logic_uScript_CompareBlockTypes_B_12;

	private bool logic_uScript_CompareBlockTypes_EqualTo_12 = true;

	private bool logic_uScript_CompareBlockTypes_NotEqualTo_12 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_15 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_15 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_15 = true;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_15 = true;

	private SubGraph_Crafting_Tutorial_ManageBlock logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_18 = new SubGraph_Crafting_Tutorial_ManageBlock();

	private SpawnBlockData[] logic_SubGraph_Crafting_Tutorial_ManageBlock_blockSpawnData_18 = new SpawnBlockData[0];

	private int logic_SubGraph_Crafting_Tutorial_ManageBlock_blockIndex_18;

	private TankBlock logic_SubGraph_Crafting_Tutorial_ManageBlock_block_18;

	private bool logic_SubGraph_Crafting_Tutorial_ManageBlock_allowAnchoring_18;

	private uScript_AddMessage.MessageData logic_SubGraph_Crafting_Tutorial_ManageBlock_msgBlockOutsideArea_18;

	private uScript_AddMessage.MessageSpeaker logic_SubGraph_Crafting_Tutorial_ManageBlock_messageSpeaker_18;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_19 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_19;

	private bool logic_uScriptCon_CompareBool_True_19 = true;

	private bool logic_uScriptCon_CompareBool_False_19 = true;

	private uScript_IsPlayerInRangeOfTech logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_21 = new uScript_IsPlayerInRangeOfTech();

	private Tank logic_uScript_IsPlayerInRangeOfTech_tech_21;

	private float logic_uScript_IsPlayerInRangeOfTech_range_21 = 100f;

	private Tank[] logic_uScript_IsPlayerInRangeOfTech_techs_21 = new Tank[0];

	private bool logic_uScript_IsPlayerInRangeOfTech_Out_21 = true;

	private bool logic_uScript_IsPlayerInRangeOfTech_InRange_21 = true;

	private bool logic_uScript_IsPlayerInRangeOfTech_OutOfRange_21 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_23 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_23;

	private bool logic_uScriptAct_SetBool_Out_23 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_23 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_23 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_24 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_24;

	private bool logic_uScriptAct_SetBool_Out_24 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_24 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_24 = true;

	private SubGraph_Crafting_Tutorial_AttachBlockToBase logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_SubGraph_Crafting_Tutorial_AttachBlockToBase_28 = new SubGraph_Crafting_Tutorial_AttachBlockToBase();

	private TankBlock logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_Block_28;

	private Tank logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_CraftingBaseTech_28;

	private GhostBlockSpawnData[] logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_ghostBlockData_28 = new GhostBlockSpawnData[0];

	private BlockTypes logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_BlockType_28 = BlockTypes.GSORefinery_222;

	private Vector3 logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_BlockPosition_28 = new Vector3(1f, 0f, -1f);

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_31 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_31;

	private bool logic_uScriptCon_CompareBool_True_31 = true;

	private bool logic_uScriptCon_CompareBool_False_31 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_33 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_33;

	private bool logic_uScriptAct_SetBool_Out_33 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_33 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_33 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_35 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_35;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_35 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_35 = "msgIntroShown";

	private uScript_HideArrow logic_uScript_HideArrow_uScript_HideArrow_36 = new uScript_HideArrow();

	private bool logic_uScript_HideArrow_Out_36 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_38 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_38;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_38 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_38 = "msgRefineryAttachedShown";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_40 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_40;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_40 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_40 = "msgBaseFoundShown";

	private uScript_GetTankBlock logic_uScript_GetTankBlock_uScript_GetTankBlock_41 = new uScript_GetTankBlock();

	private Tank logic_uScript_GetTankBlock_tank_41;

	private BlockTypes logic_uScript_GetTankBlock_blockType_41;

	private TankBlock logic_uScript_GetTankBlock_Return_41;

	private bool logic_uScript_GetTankBlock_Out_41 = true;

	private bool logic_uScript_GetTankBlock_Returned_41 = true;

	private bool logic_uScript_GetTankBlock_NotFound_41 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_47 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_47;

	private bool logic_uScriptAct_SetBool_Out_47 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_47 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_47 = true;

	private uScript_LockTechInteraction logic_uScript_LockTechInteraction_uScript_LockTechInteraction_49 = new uScript_LockTechInteraction();

	private Tank logic_uScript_LockTechInteraction_tech_49;

	private BlockTypes[] logic_uScript_LockTechInteraction_excludedBlocks_49 = new BlockTypes[1] { BlockTypes.GSOFabricator_322 };

	private bool logic_uScript_LockTechInteraction_Out_49 = true;

	private uScript_HideArrow logic_uScript_HideArrow_uScript_HideArrow_50 = new uScript_HideArrow();

	private bool logic_uScript_HideArrow_Out_50 = true;

	private uScript_LockPlayerInput logic_uScript_LockPlayerInput_uScript_LockPlayerInput_52 = new uScript_LockPlayerInput();

	private bool logic_uScript_LockPlayerInput_lockInput_52;

	private bool logic_uScript_LockPlayerInput_includeCamera_52;

	private bool logic_uScript_LockPlayerInput_Out_52 = true;

	private uScript_LockTechInteraction logic_uScript_LockTechInteraction_uScript_LockTechInteraction_54 = new uScript_LockTechInteraction();

	private Tank logic_uScript_LockTechInteraction_tech_54;

	private BlockTypes[] logic_uScript_LockTechInteraction_excludedBlocks_54 = new BlockTypes[0];

	private bool logic_uScript_LockTechInteraction_Out_54 = true;

	private uScript_PointArrowAtVisible logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_55 = new uScript_PointArrowAtVisible();

	private object logic_uScript_PointArrowAtVisible_targetObject_55 = "";

	private float logic_uScript_PointArrowAtVisible_timeToShowFor_55 = -1f;

	private Vector3 logic_uScript_PointArrowAtVisible_offset_55 = new Vector3(0f, 0f, 0f);

	private bool logic_uScript_PointArrowAtVisible_Out_55 = true;

	private uScript_LockTechInteraction logic_uScript_LockTechInteraction_uScript_LockTechInteraction_58 = new uScript_LockTechInteraction();

	private Tank logic_uScript_LockTechInteraction_tech_58;

	private BlockTypes[] logic_uScript_LockTechInteraction_excludedBlocks_58 = new BlockTypes[1] { BlockTypes.GSOFabricator_322 };

	private bool logic_uScript_LockTechInteraction_Out_58 = true;

	private uScript_LockPlayerInput logic_uScript_LockPlayerInput_uScript_LockPlayerInput_60 = new uScript_LockPlayerInput();

	private bool logic_uScript_LockPlayerInput_lockInput_60 = true;

	private bool logic_uScript_LockPlayerInput_includeCamera_60 = true;

	private bool logic_uScript_LockPlayerInput_Out_60 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_62 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_62;

	private bool logic_uScriptCon_CompareBool_True_62 = true;

	private bool logic_uScriptCon_CompareBool_False_62 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_63 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_63;

	private bool logic_uScriptCon_CompareBool_True_63 = true;

	private bool logic_uScriptCon_CompareBool_False_63 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_65 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_65;

	private bool logic_uScriptAct_SetBool_Out_65 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_65 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_65 = true;

	private uScript_CompareBlockTypes logic_uScript_CompareBlockTypes_uScript_CompareBlockTypes_66 = new uScript_CompareBlockTypes();

	private BlockTypes logic_uScript_CompareBlockTypes_A_66;

	private BlockTypes logic_uScript_CompareBlockTypes_B_66;

	private bool logic_uScript_CompareBlockTypes_EqualTo_66 = true;

	private bool logic_uScript_CompareBlockTypes_NotEqualTo_66 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_71 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_71;

	private bool logic_uScriptAct_SetBool_Out_71 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_71 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_71 = true;

	private uScript_IsBlockHoldingResources logic_uScript_IsBlockHoldingResources_uScript_IsBlockHoldingResources_72 = new uScript_IsBlockHoldingResources();

	private TankBlock logic_uScript_IsBlockHoldingResources_block_72;

	private uScript_IsBlockHoldingResources.ResourceQuantity[] logic_uScript_IsBlockHoldingResources_resources_72 = new uScript_IsBlockHoldingResources.ResourceQuantity[0];

	private bool logic_uScript_IsBlockHoldingResources_True_72 = true;

	private bool logic_uScript_IsBlockHoldingResources_False_72 = true;

	private uScript_RestrictItemPickup logic_uScript_RestrictItemPickup_uScript_RestrictItemPickup_75 = new uScript_RestrictItemPickup();

	private Tank logic_uScript_RestrictItemPickup_tech_75;

	private ChunkTypes[] logic_uScript_RestrictItemPickup_typesToAccept_75 = new ChunkTypes[0];

	private bool logic_uScript_RestrictItemPickup_Out_75 = true;

	private uScript_LockTechInteraction logic_uScript_LockTechInteraction_uScript_LockTechInteraction_78 = new uScript_LockTechInteraction();

	private Tank logic_uScript_LockTechInteraction_tech_78;

	private BlockTypes[] logic_uScript_LockTechInteraction_excludedBlocks_78 = new BlockTypes[0];

	private bool logic_uScript_LockTechInteraction_Out_78 = true;

	private uScript_LockTechInteraction logic_uScript_LockTechInteraction_uScript_LockTechInteraction_80 = new uScript_LockTechInteraction();

	private Tank logic_uScript_LockTechInteraction_tech_80;

	private BlockTypes[] logic_uScript_LockTechInteraction_excludedBlocks_80 = new BlockTypes[0];

	private bool logic_uScript_LockTechInteraction_Out_80 = true;

	private uScript_GetTankBlock logic_uScript_GetTankBlock_uScript_GetTankBlock_85 = new uScript_GetTankBlock();

	private Tank logic_uScript_GetTankBlock_tank_85;

	private BlockTypes logic_uScript_GetTankBlock_blockType_85;

	private TankBlock logic_uScript_GetTankBlock_Return_85;

	private bool logic_uScript_GetTankBlock_Out_85 = true;

	private bool logic_uScript_GetTankBlock_Returned_85 = true;

	private bool logic_uScript_GetTankBlock_NotFound_85 = true;

	private uScript_LockPause logic_uScript_LockPause_uScript_LockPause_87 = new uScript_LockPause();

	private bool logic_uScript_LockPause_lockPause_87 = true;

	private ManPauseGame.DisablePauseReason logic_uScript_LockPause_disabledReason_87 = ManPauseGame.DisablePauseReason.Tutorial;

	private bool logic_uScript_LockPause_Out_87 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_90 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_90;

	private bool logic_uScriptAct_SetBool_Out_90 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_90 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_90 = true;

	private uScript_PointArrowAtVisible logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_92 = new uScript_PointArrowAtVisible();

	private object logic_uScript_PointArrowAtVisible_targetObject_92 = "";

	private float logic_uScript_PointArrowAtVisible_timeToShowFor_92 = -1f;

	private Vector3 logic_uScript_PointArrowAtVisible_offset_92 = new Vector3(0f, 0f, 0f);

	private bool logic_uScript_PointArrowAtVisible_Out_92 = true;

	private uScript_LockPlayerInput logic_uScript_LockPlayerInput_uScript_LockPlayerInput_93 = new uScript_LockPlayerInput();

	private bool logic_uScript_LockPlayerInput_lockInput_93 = true;

	private bool logic_uScript_LockPlayerInput_includeCamera_93 = true;

	private bool logic_uScript_LockPlayerInput_Out_93 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_94 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_94;

	private bool logic_uScriptCon_CompareBool_True_94 = true;

	private bool logic_uScriptCon_CompareBool_False_94 = true;

	private uScript_HideArrow logic_uScript_HideArrow_uScript_HideArrow_97 = new uScript_HideArrow();

	private bool logic_uScript_HideArrow_Out_97 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_101 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_101;

	private bool logic_uScriptAct_SetBool_Out_101 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_101 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_101 = true;

	private uScript_LockTechInteraction logic_uScript_LockTechInteraction_uScript_LockTechInteraction_102 = new uScript_LockTechInteraction();

	private Tank logic_uScript_LockTechInteraction_tech_102;

	private BlockTypes[] logic_uScript_LockTechInteraction_excludedBlocks_102 = new BlockTypes[1] { BlockTypes.GSOFabricator_322 };

	private bool logic_uScript_LockTechInteraction_Out_102 = true;

	private uScript_LockPlayerInput logic_uScript_LockPlayerInput_uScript_LockPlayerInput_103 = new uScript_LockPlayerInput();

	private bool logic_uScript_LockPlayerInput_lockInput_103;

	private bool logic_uScript_LockPlayerInput_includeCamera_103;

	private bool logic_uScript_LockPlayerInput_Out_103 = true;

	private uScript_LockTechInteraction logic_uScript_LockTechInteraction_uScript_LockTechInteraction_104 = new uScript_LockTechInteraction();

	private Tank logic_uScript_LockTechInteraction_tech_104;

	private BlockTypes[] logic_uScript_LockTechInteraction_excludedBlocks_104 = new BlockTypes[1] { BlockTypes.GSOFabricator_322 };

	private bool logic_uScript_LockTechInteraction_Out_104 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_106 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_106;

	private bool logic_uScriptCon_CompareBool_True_106 = true;

	private bool logic_uScriptCon_CompareBool_False_106 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_107 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_107;

	private bool logic_uScriptAct_SetBool_Out_107 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_107 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_107 = true;

	private uScript_Wait logic_uScript_Wait_uScript_Wait_108 = new uScript_Wait();

	private float logic_uScript_Wait_seconds_108;

	private bool logic_uScript_Wait_repeat_108 = true;

	private bool logic_uScript_Wait_Waited_108 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_111 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_111;

	private bool logic_uScriptCon_CompareBool_True_111 = true;

	private bool logic_uScriptCon_CompareBool_False_111 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_113 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_113;

	private bool logic_uScriptAct_SetBool_Out_113 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_113 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_113 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_114 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_114;

	private bool logic_uScriptCon_CompareBool_True_114 = true;

	private bool logic_uScriptCon_CompareBool_False_114 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_117 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_117;

	private bool logic_uScriptAct_SetBool_Out_117 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_117 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_117 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_118 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_118 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_118 = true;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_118 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_120 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_120 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_120 = true;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_120 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_123 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_123;

	private bool logic_uScriptCon_CompareBool_True_123 = true;

	private bool logic_uScriptCon_CompareBool_False_123 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_124 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_124;

	private bool logic_uScriptCon_CompareBool_True_124 = true;

	private bool logic_uScriptCon_CompareBool_False_124 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_126 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_126;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_126 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_126 = "BlockCrafted01";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_129 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_129;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_129 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_129 = "BlockCrafted02";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_130 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_130;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_130 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_130 = "msgRefineryExplanation02Shown";

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_133 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_133;

	private bool logic_uScriptAct_SetBool_Out_133 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_133 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_133 = true;

	private uScript_LockTechInteraction logic_uScript_LockTechInteraction_uScript_LockTechInteraction_135 = new uScript_LockTechInteraction();

	private Tank logic_uScript_LockTechInteraction_tech_135;

	private BlockTypes[] logic_uScript_LockTechInteraction_excludedBlocks_135 = new BlockTypes[0];

	private bool logic_uScript_LockTechInteraction_Out_135 = true;

	private uScriptCon_ManualSwitch logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_138 = new uScriptCon_ManualSwitch();

	private int logic_uScriptCon_ManualSwitch_CurrentOutput_138;

	private SubGraph_SaveLoadInt logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_139 = new SubGraph_SaveLoadInt();

	private int logic_SubGraph_SaveLoadInt_restartValue_139 = 1;

	private int logic_SubGraph_SaveLoadInt_integer_139;

	private object logic_SubGraph_SaveLoadInt_intAsVariable_139 = "";

	private string logic_SubGraph_SaveLoadInt_uniqueID_139 = "Stage";

	private SubGraph_LoadObjectiveStates logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_141 = new SubGraph_LoadObjectiveStates();

	private int logic_SubGraph_LoadObjectiveStates_currentObjective_141;

	private SubGraph_CompleteObjectiveStage logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_144 = new SubGraph_CompleteObjectiveStage();

	private int logic_SubGraph_CompleteObjectiveStage_objectiveStage_144;

	private bool logic_SubGraph_CompleteObjectiveStage_isFinalObjective_144;

	private SubGraph_CompleteObjectiveStage logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_146 = new SubGraph_CompleteObjectiveStage();

	private int logic_SubGraph_CompleteObjectiveStage_objectiveStage_146;

	private bool logic_SubGraph_CompleteObjectiveStage_isFinalObjective_146;

	private SubGraph_CompleteObjectiveStage logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_148 = new SubGraph_CompleteObjectiveStage();

	private int logic_SubGraph_CompleteObjectiveStage_objectiveStage_148;

	private bool logic_SubGraph_CompleteObjectiveStage_isFinalObjective_148;

	private SubGraph_CompleteObjectiveStage logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_150 = new SubGraph_CompleteObjectiveStage();

	private int logic_SubGraph_CompleteObjectiveStage_objectiveStage_150;

	private bool logic_SubGraph_CompleteObjectiveStage_isFinalObjective_150;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_152 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_152;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_152;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_152;

	private bool logic_uScript_AddMessage_Out_152 = true;

	private bool logic_uScript_AddMessage_Shown_152 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_156 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_156;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_156;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_156;

	private bool logic_uScript_AddMessage_Out_156 = true;

	private bool logic_uScript_AddMessage_Shown_156 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_159 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_159;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_159;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_159;

	private bool logic_uScript_AddMessage_Out_159 = true;

	private bool logic_uScript_AddMessage_Shown_159 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_162 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_162;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_162;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_162;

	private bool logic_uScript_AddMessage_Out_162 = true;

	private bool logic_uScript_AddMessage_Shown_162 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_165 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_165;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_165;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_165;

	private bool logic_uScript_AddMessage_Out_165 = true;

	private bool logic_uScript_AddMessage_Shown_165 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_168 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_168;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_168;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_168;

	private bool logic_uScript_AddMessage_Out_168 = true;

	private bool logic_uScript_AddMessage_Shown_168 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_171 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_171;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_171;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_171;

	private bool logic_uScript_AddMessage_Out_171 = true;

	private bool logic_uScript_AddMessage_Shown_171 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_174 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_174;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_174;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_174;

	private bool logic_uScript_AddMessage_Out_174 = true;

	private bool logic_uScript_AddMessage_Shown_174 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_175 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_175;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_175;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_175;

	private bool logic_uScript_AddMessage_Out_175 = true;

	private bool logic_uScript_AddMessage_Shown_175 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_179 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_179;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_179;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_179;

	private bool logic_uScript_AddMessage_Out_179 = true;

	private bool logic_uScript_AddMessage_Shown_179 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_184 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_184;

	private bool logic_uScriptCon_CompareBool_True_184 = true;

	private bool logic_uScriptCon_CompareBool_False_184 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_185 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_185;

	private bool logic_uScriptAct_SetBool_Out_185 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_185 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_185 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_189 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_189;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_189;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_189;

	private bool logic_uScript_AddMessage_Out_189 = true;

	private bool logic_uScript_AddMessage_Shown_189 = true;

	private SubGraph_Crafting_Tutorial_Finish logic_SubGraph_Crafting_Tutorial_Finish_SubGraph_Crafting_Tutorial_Finish_195 = new SubGraph_Crafting_Tutorial_Finish();

	private Tank logic_SubGraph_Crafting_Tutorial_Finish_craftingBaseTech_195;

	private Tank logic_SubGraph_Crafting_Tutorial_Finish_NPCTech_195;

	private ExternalBehaviorTree logic_SubGraph_Crafting_Tutorial_Finish_NPCFlyAwayAI_195;

	private Transform logic_SubGraph_Crafting_Tutorial_Finish_NPCDespawnParticleEffect_195;

	private SubGraph_Crafting_Tutorial_Init logic_SubGraph_Crafting_Tutorial_Init_SubGraph_Crafting_Tutorial_Init_197 = new SubGraph_Crafting_Tutorial_Init();

	private SpawnTechData[] logic_SubGraph_Crafting_Tutorial_Init_baseSpawnData_197 = new SpawnTechData[0];

	private SpawnBlockData[] logic_SubGraph_Crafting_Tutorial_Init_blockSpawnData_197 = new SpawnBlockData[0];

	private SpawnTechData[] logic_SubGraph_Crafting_Tutorial_Init_NPCSpawnData_197 = new SpawnTechData[0];

	private TankPreset logic_SubGraph_Crafting_Tutorial_Init_completedBasePreset_197;

	private bool logic_SubGraph_Crafting_Tutorial_Init_useNPCAsTutorialTech_197;

	private bool logic_SubGraph_Crafting_Tutorial_Init_spawnBase_197 = true;

	private string logic_SubGraph_Crafting_Tutorial_Init_basePosition_197 = "";

	private float logic_SubGraph_Crafting_Tutorial_Init_clearSceneryRadius_197;

	private Tank logic_SubGraph_Crafting_Tutorial_Init_CraftingBaseTech_197;

	private Tank logic_SubGraph_Crafting_Tutorial_Init_NPCTech_197;

	private uScript_SetEncounterTarget logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_207 = new uScript_SetEncounterTarget();

	private GameObject logic_uScript_SetEncounterTarget_owner_207;

	private object logic_uScript_SetEncounterTarget_visibleObject_207 = "";

	private bool logic_uScript_SetEncounterTarget_Out_207 = true;

	private uScript_CraftingUIHighlightItem logic_uScript_CraftingUIHighlightItem_uScript_CraftingUIHighlightItem_208 = new uScript_CraftingUIHighlightItem();

	private ManHUD.HUDElementType logic_uScript_CraftingUIHighlightItem_targetMenuType_208 = ManHUD.HUDElementType.BlockRecipeSelect;

	private ItemTypeInfo logic_uScript_CraftingUIHighlightItem_itemToHighlight_208;

	private bool logic_uScript_CraftingUIHighlightItem_Out_208 = true;

	private bool logic_uScript_CraftingUIHighlightItem_Waiting_208 = true;

	private bool logic_uScript_CraftingUIHighlightItem_Selected_208 = true;

	private uScript_CraftingUIHighlightBlockCategory logic_uScript_CraftingUIHighlightBlockCategory_uScript_CraftingUIHighlightBlockCategory_209 = new uScript_CraftingUIHighlightBlockCategory();

	private BlockCategories logic_uScript_CraftingUIHighlightBlockCategory_blockCategory_209;

	private bool logic_uScript_CraftingUIHighlightBlockCategory_Out_209 = true;

	private bool logic_uScript_CraftingUIHighlightBlockCategory_Waiting_209 = true;

	private bool logic_uScript_CraftingUIHighlightBlockCategory_Selected_209 = true;

	private uScript_CraftingUIHighlightCraftButton logic_uScript_CraftingUIHighlightCraftButton_uScript_CraftingUIHighlightCraftButton_211 = new uScript_CraftingUIHighlightCraftButton();

	private ManHUD.HUDElementType logic_uScript_CraftingUIHighlightCraftButton_targetMenuType_211 = ManHUD.HUDElementType.BlockRecipeSelect;

	private bool logic_uScript_CraftingUIHighlightCraftButton_Out_211 = true;

	private bool logic_uScript_CraftingUIHighlightCraftButton_Waiting_211 = true;

	private bool logic_uScript_CraftingUIHighlightCraftButton_Selected_211 = true;

	private uScript_CraftingUIHighlightCraftButton logic_uScript_CraftingUIHighlightCraftButton_uScript_CraftingUIHighlightCraftButton_213 = new uScript_CraftingUIHighlightCraftButton();

	private ManHUD.HUDElementType logic_uScript_CraftingUIHighlightCraftButton_targetMenuType_213 = ManHUD.HUDElementType.BlockRecipeSelect;

	private bool logic_uScript_CraftingUIHighlightCraftButton_Out_213 = true;

	private bool logic_uScript_CraftingUIHighlightCraftButton_Waiting_213 = true;

	private bool logic_uScript_CraftingUIHighlightCraftButton_Selected_213 = true;

	private uScript_CraftingUIHighlightItem logic_uScript_CraftingUIHighlightItem_uScript_CraftingUIHighlightItem_214 = new uScript_CraftingUIHighlightItem();

	private ManHUD.HUDElementType logic_uScript_CraftingUIHighlightItem_targetMenuType_214 = ManHUD.HUDElementType.BlockRecipeSelect;

	private ItemTypeInfo logic_uScript_CraftingUIHighlightItem_itemToHighlight_214;

	private bool logic_uScript_CraftingUIHighlightItem_Out_214 = true;

	private bool logic_uScript_CraftingUIHighlightItem_Waiting_214 = true;

	private bool logic_uScript_CraftingUIHighlightItem_Selected_214 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_217 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_217;

	private bool logic_uScriptAct_SetBool_Out_217 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_217 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_217 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_218 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_218;

	private bool logic_uScriptAct_SetBool_Out_218 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_218 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_218 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_220 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_220;

	private bool logic_uScriptAct_SetBool_Out_220 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_220 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_220 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_222 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_222;

	private bool logic_uScriptAct_SetBool_Out_222 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_222 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_222 = true;

	private uScript_LockTechInteraction logic_uScript_LockTechInteraction_uScript_LockTechInteraction_224 = new uScript_LockTechInteraction();

	private Tank logic_uScript_LockTechInteraction_tech_224;

	private BlockTypes[] logic_uScript_LockTechInteraction_excludedBlocks_224 = new BlockTypes[0];

	private bool logic_uScript_LockTechInteraction_Out_224 = true;

	private uScript_DisableClosingCraftingUIWhenTooFarAway logic_uScript_DisableClosingCraftingUIWhenTooFarAway_uScript_DisableClosingCraftingUIWhenTooFarAway_225 = new uScript_DisableClosingCraftingUIWhenTooFarAway();

	private TankBlock logic_uScript_DisableClosingCraftingUIWhenTooFarAway_craftingBlock_225;

	private bool logic_uScript_DisableClosingCraftingUIWhenTooFarAway_Out_225 = true;

	private uScript_DisableClosingCraftingUIWhenTooFarAway logic_uScript_DisableClosingCraftingUIWhenTooFarAway_uScript_DisableClosingCraftingUIWhenTooFarAway_228 = new uScript_DisableClosingCraftingUIWhenTooFarAway();

	private TankBlock logic_uScript_DisableClosingCraftingUIWhenTooFarAway_craftingBlock_228;

	private bool logic_uScript_DisableClosingCraftingUIWhenTooFarAway_Out_228 = true;

	private uScript_DisableClosingCraftingUIWhenTooFarAway logic_uScript_DisableClosingCraftingUIWhenTooFarAway_uScript_DisableClosingCraftingUIWhenTooFarAway_230 = new uScript_DisableClosingCraftingUIWhenTooFarAway();

	private TankBlock logic_uScript_DisableClosingCraftingUIWhenTooFarAway_craftingBlock_230;

	private bool logic_uScript_DisableClosingCraftingUIWhenTooFarAway_Out_230 = true;

	private uScript_DisableClosingCraftingUIWhenTooFarAway logic_uScript_DisableClosingCraftingUIWhenTooFarAway_uScript_DisableClosingCraftingUIWhenTooFarAway_231 = new uScript_DisableClosingCraftingUIWhenTooFarAway();

	private TankBlock logic_uScript_DisableClosingCraftingUIWhenTooFarAway_craftingBlock_231;

	private bool logic_uScript_DisableClosingCraftingUIWhenTooFarAway_Out_231 = true;

	private uScript_DisableClosingCraftingUIWhenTooFarAway logic_uScript_DisableClosingCraftingUIWhenTooFarAway_uScript_DisableClosingCraftingUIWhenTooFarAway_233 = new uScript_DisableClosingCraftingUIWhenTooFarAway();

	private TankBlock logic_uScript_DisableClosingCraftingUIWhenTooFarAway_craftingBlock_233;

	private bool logic_uScript_DisableClosingCraftingUIWhenTooFarAway_Out_233 = true;

	private uScript_LockPlayerInput logic_uScript_LockPlayerInput_uScript_LockPlayerInput_235 = new uScript_LockPlayerInput();

	private bool logic_uScript_LockPlayerInput_lockInput_235;

	private bool logic_uScript_LockPlayerInput_includeCamera_235;

	private bool logic_uScript_LockPlayerInput_Out_235 = true;

	private uScript_LockPause logic_uScript_LockPause_uScript_LockPause_236 = new uScript_LockPause();

	private bool logic_uScript_LockPause_lockPause_236;

	private ManPauseGame.DisablePauseReason logic_uScript_LockPause_disabledReason_236 = ManPauseGame.DisablePauseReason.Tutorial;

	private bool logic_uScript_LockPause_Out_236 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_237 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_237;

	private bool logic_uScriptCon_CompareBool_True_237 = true;

	private bool logic_uScriptCon_CompareBool_False_237 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_238 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_238;

	private bool logic_uScriptCon_CompareBool_True_238 = true;

	private bool logic_uScriptCon_CompareBool_False_238 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_243 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_243;

	private bool logic_uScriptAct_SetBool_Out_243 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_243 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_243 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_244 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_244;

	private bool logic_uScriptCon_CompareBool_True_244 = true;

	private bool logic_uScriptCon_CompareBool_False_244 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_246 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_246;

	private bool logic_uScriptAct_SetBool_Out_246 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_246 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_246 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_248 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_248;

	private bool logic_uScriptCon_CompareBool_True_248 = true;

	private bool logic_uScriptCon_CompareBool_False_248 = true;

	private uScript_LockPause logic_uScript_LockPause_uScript_LockPause_251 = new uScript_LockPause();

	private bool logic_uScript_LockPause_lockPause_251;

	private ManPauseGame.DisablePauseReason logic_uScript_LockPause_disabledReason_251 = ManPauseGame.DisablePauseReason.Tutorial;

	private bool logic_uScript_LockPause_Out_251 = true;

	private uScript_LockPause logic_uScript_LockPause_uScript_LockPause_252 = new uScript_LockPause();

	private bool logic_uScript_LockPause_lockPause_252;

	private ManPauseGame.DisablePauseReason logic_uScript_LockPause_disabledReason_252 = ManPauseGame.DisablePauseReason.Tutorial;

	private bool logic_uScript_LockPause_Out_252 = true;

	private uScript_LockPause logic_uScript_LockPause_uScript_LockPause_253 = new uScript_LockPause();

	private bool logic_uScript_LockPause_lockPause_253 = true;

	private ManPauseGame.DisablePauseReason logic_uScript_LockPause_disabledReason_253 = ManPauseGame.DisablePauseReason.Tutorial;

	private bool logic_uScript_LockPause_Out_253 = true;

	private uScript_IsHUDElementLinkedToBlock logic_uScript_IsHUDElementLinkedToBlock_uScript_IsHUDElementLinkedToBlock_254 = new uScript_IsHUDElementLinkedToBlock();

	private ManHUD.HUDElementType logic_uScript_IsHUDElementLinkedToBlock_hudElement_254;

	private TankBlock logic_uScript_IsHUDElementLinkedToBlock_targetBlock_254;

	private bool logic_uScript_IsHUDElementLinkedToBlock_True_254 = true;

	private bool logic_uScript_IsHUDElementLinkedToBlock_False_254 = true;

	private uScript_IsHUDElementLinkedToBlock logic_uScript_IsHUDElementLinkedToBlock_uScript_IsHUDElementLinkedToBlock_257 = new uScript_IsHUDElementLinkedToBlock();

	private ManHUD.HUDElementType logic_uScript_IsHUDElementLinkedToBlock_hudElement_257;

	private TankBlock logic_uScript_IsHUDElementLinkedToBlock_targetBlock_257;

	private bool logic_uScript_IsHUDElementLinkedToBlock_True_257 = true;

	private bool logic_uScript_IsHUDElementLinkedToBlock_False_257 = true;

	private uScript_HideHUDElement logic_uScript_HideHUDElement_uScript_HideHUDElement_261 = new uScript_HideHUDElement();

	private ManHUD.HUDElementType logic_uScript_HideHUDElement_hudElement_261;

	private bool logic_uScript_HideHUDElement_Out_261 = true;

	private uScript_HideHUDElement logic_uScript_HideHUDElement_uScript_HideHUDElement_263 = new uScript_HideHUDElement();

	private ManHUD.HUDElementType logic_uScript_HideHUDElement_hudElement_263;

	private bool logic_uScript_HideHUDElement_Out_263 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_264 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_264 = "";

	private bool logic_uScript_EnableGlow_enable_264 = true;

	private bool logic_uScript_EnableGlow_Out_264 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_267 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_267 = "";

	private bool logic_uScript_EnableGlow_enable_267;

	private bool logic_uScript_EnableGlow_Out_267 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_269 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_269 = "";

	private bool logic_uScript_EnableGlow_enable_269 = true;

	private bool logic_uScript_EnableGlow_Out_269 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_271 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_271 = "";

	private bool logic_uScript_EnableGlow_enable_271;

	private bool logic_uScript_EnableGlow_Out_271 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_272 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_272 = "";

	private bool logic_uScript_EnableGlow_enable_272;

	private bool logic_uScript_EnableGlow_Out_272 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_275 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_275 = "";

	private bool logic_uScript_EnableGlow_enable_275;

	private bool logic_uScript_EnableGlow_Out_275 = true;

	private uScript_SendAnaliticsEvent logic_uScript_SendAnaliticsEvent_uScript_SendAnaliticsEvent_276 = new uScript_SendAnaliticsEvent();

	private string logic_uScript_SendAnaliticsEvent_analiticsEvent_276 = "tutorial_start";

	private string logic_uScript_SendAnaliticsEvent_parameterName_276 = "crafting_tutorial";

	private object logic_uScript_SendAnaliticsEvent_parameter_276 = "5";

	private bool logic_uScript_SendAnaliticsEvent_Out_276 = true;

	private uScript_SendAnaliticsEvent logic_uScript_SendAnaliticsEvent_uScript_SendAnaliticsEvent_277 = new uScript_SendAnaliticsEvent();

	private string logic_uScript_SendAnaliticsEvent_analiticsEvent_277 = "tutorial_complete";

	private string logic_uScript_SendAnaliticsEvent_parameterName_277 = "crafting_tutorial";

	private object logic_uScript_SendAnaliticsEvent_parameter_277 = "5";

	private bool logic_uScript_SendAnaliticsEvent_Out_277 = true;

	private uScript_LockHudGroup logic_uScript_LockHudGroup_uScript_LockHudGroup_278 = new uScript_LockHudGroup();

	private ManHUD.HUDGroup logic_uScript_LockHudGroup_group_278;

	private bool logic_uScript_LockHudGroup_locked_278 = true;

	private bool logic_uScript_LockHudGroup_Out_278 = true;

	private uScript_LockHudGroup logic_uScript_LockHudGroup_uScript_LockHudGroup_279 = new uScript_LockHudGroup();

	private ManHUD.HUDGroup logic_uScript_LockHudGroup_group_279;

	private bool logic_uScript_LockHudGroup_locked_279;

	private bool logic_uScript_LockHudGroup_Out_279 = true;

	private uScript_LockHudGroup logic_uScript_LockHudGroup_uScript_LockHudGroup_280 = new uScript_LockHudGroup();

	private ManHUD.HUDGroup logic_uScript_LockHudGroup_group_280;

	private bool logic_uScript_LockHudGroup_locked_280 = true;

	private bool logic_uScript_LockHudGroup_Out_280 = true;

	private uScript_LockHudGroup logic_uScript_LockHudGroup_uScript_LockHudGroup_281 = new uScript_LockHudGroup();

	private ManHUD.HUDGroup logic_uScript_LockHudGroup_group_281;

	private bool logic_uScript_LockHudGroup_locked_281;

	private bool logic_uScript_LockHudGroup_Out_281 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_576 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_576;

	private bool logic_uScriptCon_CompareBool_True_576 = true;

	private bool logic_uScriptCon_CompareBool_False_576 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_583 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_583;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_583 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_583 = "";

	private BlockTypes event_UnityEngine_GameObject_BlockType_9;

	private int event_UnityEngine_GameObject_BlockTypeTotal_9;

	private int event_UnityEngine_GameObject_BlockTotal_9;

	private BlockTypes event_UnityEngine_GameObject_BlockType_70;

	private int event_UnityEngine_GameObject_BlockTypeTotal_70;

	private int event_UnityEngine_GameObject_BlockTotal_70;

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
		if (null == owner_Connection_7 || !m_RegisteredForEvents)
		{
			owner_Connection_7 = parentGameObject;
			if (null != owner_Connection_7)
			{
				uScript_SaveLoad uScript_SaveLoad2 = owner_Connection_7.GetComponent<uScript_SaveLoad>();
				if (null == uScript_SaveLoad2)
				{
					uScript_SaveLoad2 = owner_Connection_7.AddComponent<uScript_SaveLoad>();
				}
				if (null != uScript_SaveLoad2)
				{
					uScript_SaveLoad2.SaveEvent += Instance_SaveEvent_6;
					uScript_SaveLoad2.LoadEvent += Instance_LoadEvent_6;
					uScript_SaveLoad2.RestartEvent += Instance_RestartEvent_6;
				}
			}
		}
		if (null == owner_Connection_10 || !m_RegisteredForEvents)
		{
			owner_Connection_10 = parentGameObject;
			if (null != owner_Connection_10)
			{
				uScript_BlockCraftedEvent uScript_BlockCraftedEvent2 = owner_Connection_10.GetComponent<uScript_BlockCraftedEvent>();
				if (null == uScript_BlockCraftedEvent2)
				{
					uScript_BlockCraftedEvent2 = owner_Connection_10.AddComponent<uScript_BlockCraftedEvent>();
				}
				if (null != uScript_BlockCraftedEvent2)
				{
					uScript_BlockCraftedEvent2.BlockCraftedEvent += Instance_BlockCraftedEvent_9;
				}
			}
		}
		if (null == owner_Connection_68 || !m_RegisteredForEvents)
		{
			owner_Connection_68 = parentGameObject;
			if (null != owner_Connection_68)
			{
				uScript_BlockCraftedEvent uScript_BlockCraftedEvent3 = owner_Connection_68.GetComponent<uScript_BlockCraftedEvent>();
				if (null == uScript_BlockCraftedEvent3)
				{
					uScript_BlockCraftedEvent3 = owner_Connection_68.AddComponent<uScript_BlockCraftedEvent>();
				}
				if (null != uScript_BlockCraftedEvent3)
				{
					uScript_BlockCraftedEvent3.BlockCraftedEvent += Instance_BlockCraftedEvent_70;
				}
			}
		}
		if (null == owner_Connection_206 || !m_RegisteredForEvents)
		{
			owner_Connection_206 = parentGameObject;
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
		if (!m_RegisteredForEvents && null != owner_Connection_7)
		{
			uScript_SaveLoad uScript_SaveLoad2 = owner_Connection_7.GetComponent<uScript_SaveLoad>();
			if (null == uScript_SaveLoad2)
			{
				uScript_SaveLoad2 = owner_Connection_7.AddComponent<uScript_SaveLoad>();
			}
			if (null != uScript_SaveLoad2)
			{
				uScript_SaveLoad2.SaveEvent += Instance_SaveEvent_6;
				uScript_SaveLoad2.LoadEvent += Instance_LoadEvent_6;
				uScript_SaveLoad2.RestartEvent += Instance_RestartEvent_6;
			}
		}
		if (!m_RegisteredForEvents && null != owner_Connection_10)
		{
			uScript_BlockCraftedEvent uScript_BlockCraftedEvent2 = owner_Connection_10.GetComponent<uScript_BlockCraftedEvent>();
			if (null == uScript_BlockCraftedEvent2)
			{
				uScript_BlockCraftedEvent2 = owner_Connection_10.AddComponent<uScript_BlockCraftedEvent>();
			}
			if (null != uScript_BlockCraftedEvent2)
			{
				uScript_BlockCraftedEvent2.BlockCraftedEvent += Instance_BlockCraftedEvent_9;
			}
		}
		if (!m_RegisteredForEvents && null != owner_Connection_68)
		{
			uScript_BlockCraftedEvent uScript_BlockCraftedEvent3 = owner_Connection_68.GetComponent<uScript_BlockCraftedEvent>();
			if (null == uScript_BlockCraftedEvent3)
			{
				uScript_BlockCraftedEvent3 = owner_Connection_68.AddComponent<uScript_BlockCraftedEvent>();
			}
			if (null != uScript_BlockCraftedEvent3)
			{
				uScript_BlockCraftedEvent3.BlockCraftedEvent += Instance_BlockCraftedEvent_70;
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
		if (null != owner_Connection_7)
		{
			uScript_SaveLoad component2 = owner_Connection_7.GetComponent<uScript_SaveLoad>();
			if (null != component2)
			{
				component2.SaveEvent -= Instance_SaveEvent_6;
				component2.LoadEvent -= Instance_LoadEvent_6;
				component2.RestartEvent -= Instance_RestartEvent_6;
			}
		}
		if (null != owner_Connection_10)
		{
			uScript_BlockCraftedEvent component3 = owner_Connection_10.GetComponent<uScript_BlockCraftedEvent>();
			if (null != component3)
			{
				component3.BlockCraftedEvent -= Instance_BlockCraftedEvent_9;
			}
		}
		if (null != owner_Connection_68)
		{
			uScript_BlockCraftedEvent component4 = owner_Connection_68.GetComponent<uScript_BlockCraftedEvent>();
			if (null != component4)
			{
				component4.BlockCraftedEvent -= Instance_BlockCraftedEvent_70;
			}
		}
	}

	public override void SetParent(GameObject g)
	{
		parentGameObject = g;
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_2.SetParent(g);
		logic_uScript_PausePopulation_uScript_PausePopulation_3.SetParent(g);
		logic_uScript_PausePopulation_uScript_PausePopulation_4.SetParent(g);
		logic_uScript_CompareBlockTypes_uScript_CompareBlockTypes_12.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_15.SetParent(g);
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_18.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_19.SetParent(g);
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_21.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_23.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_24.SetParent(g);
		logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_SubGraph_Crafting_Tutorial_AttachBlockToBase_28.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_31.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_33.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_35.SetParent(g);
		logic_uScript_HideArrow_uScript_HideArrow_36.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_38.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_40.SetParent(g);
		logic_uScript_GetTankBlock_uScript_GetTankBlock_41.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_47.SetParent(g);
		logic_uScript_LockTechInteraction_uScript_LockTechInteraction_49.SetParent(g);
		logic_uScript_HideArrow_uScript_HideArrow_50.SetParent(g);
		logic_uScript_LockPlayerInput_uScript_LockPlayerInput_52.SetParent(g);
		logic_uScript_LockTechInteraction_uScript_LockTechInteraction_54.SetParent(g);
		logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_55.SetParent(g);
		logic_uScript_LockTechInteraction_uScript_LockTechInteraction_58.SetParent(g);
		logic_uScript_LockPlayerInput_uScript_LockPlayerInput_60.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_62.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_63.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_65.SetParent(g);
		logic_uScript_CompareBlockTypes_uScript_CompareBlockTypes_66.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_71.SetParent(g);
		logic_uScript_IsBlockHoldingResources_uScript_IsBlockHoldingResources_72.SetParent(g);
		logic_uScript_RestrictItemPickup_uScript_RestrictItemPickup_75.SetParent(g);
		logic_uScript_LockTechInteraction_uScript_LockTechInteraction_78.SetParent(g);
		logic_uScript_LockTechInteraction_uScript_LockTechInteraction_80.SetParent(g);
		logic_uScript_GetTankBlock_uScript_GetTankBlock_85.SetParent(g);
		logic_uScript_LockPause_uScript_LockPause_87.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_90.SetParent(g);
		logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_92.SetParent(g);
		logic_uScript_LockPlayerInput_uScript_LockPlayerInput_93.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_94.SetParent(g);
		logic_uScript_HideArrow_uScript_HideArrow_97.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_101.SetParent(g);
		logic_uScript_LockTechInteraction_uScript_LockTechInteraction_102.SetParent(g);
		logic_uScript_LockPlayerInput_uScript_LockPlayerInput_103.SetParent(g);
		logic_uScript_LockTechInteraction_uScript_LockTechInteraction_104.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_106.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_107.SetParent(g);
		logic_uScript_Wait_uScript_Wait_108.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_111.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_113.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_114.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_117.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_118.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_120.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_123.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_124.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_126.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_129.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_130.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_133.SetParent(g);
		logic_uScript_LockTechInteraction_uScript_LockTechInteraction_135.SetParent(g);
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_138.SetParent(g);
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_139.SetParent(g);
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_141.SetParent(g);
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_144.SetParent(g);
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_146.SetParent(g);
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_148.SetParent(g);
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_150.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_152.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_156.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_159.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_162.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_165.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_168.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_171.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_174.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_175.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_179.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_184.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_185.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_189.SetParent(g);
		logic_SubGraph_Crafting_Tutorial_Finish_SubGraph_Crafting_Tutorial_Finish_195.SetParent(g);
		logic_SubGraph_Crafting_Tutorial_Init_SubGraph_Crafting_Tutorial_Init_197.SetParent(g);
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_207.SetParent(g);
		logic_uScript_CraftingUIHighlightItem_uScript_CraftingUIHighlightItem_208.SetParent(g);
		logic_uScript_CraftingUIHighlightBlockCategory_uScript_CraftingUIHighlightBlockCategory_209.SetParent(g);
		logic_uScript_CraftingUIHighlightCraftButton_uScript_CraftingUIHighlightCraftButton_211.SetParent(g);
		logic_uScript_CraftingUIHighlightCraftButton_uScript_CraftingUIHighlightCraftButton_213.SetParent(g);
		logic_uScript_CraftingUIHighlightItem_uScript_CraftingUIHighlightItem_214.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_217.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_218.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_220.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_222.SetParent(g);
		logic_uScript_LockTechInteraction_uScript_LockTechInteraction_224.SetParent(g);
		logic_uScript_DisableClosingCraftingUIWhenTooFarAway_uScript_DisableClosingCraftingUIWhenTooFarAway_225.SetParent(g);
		logic_uScript_DisableClosingCraftingUIWhenTooFarAway_uScript_DisableClosingCraftingUIWhenTooFarAway_228.SetParent(g);
		logic_uScript_DisableClosingCraftingUIWhenTooFarAway_uScript_DisableClosingCraftingUIWhenTooFarAway_230.SetParent(g);
		logic_uScript_DisableClosingCraftingUIWhenTooFarAway_uScript_DisableClosingCraftingUIWhenTooFarAway_231.SetParent(g);
		logic_uScript_DisableClosingCraftingUIWhenTooFarAway_uScript_DisableClosingCraftingUIWhenTooFarAway_233.SetParent(g);
		logic_uScript_LockPlayerInput_uScript_LockPlayerInput_235.SetParent(g);
		logic_uScript_LockPause_uScript_LockPause_236.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_237.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_238.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_243.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_244.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_246.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_248.SetParent(g);
		logic_uScript_LockPause_uScript_LockPause_251.SetParent(g);
		logic_uScript_LockPause_uScript_LockPause_252.SetParent(g);
		logic_uScript_LockPause_uScript_LockPause_253.SetParent(g);
		logic_uScript_IsHUDElementLinkedToBlock_uScript_IsHUDElementLinkedToBlock_254.SetParent(g);
		logic_uScript_IsHUDElementLinkedToBlock_uScript_IsHUDElementLinkedToBlock_257.SetParent(g);
		logic_uScript_HideHUDElement_uScript_HideHUDElement_261.SetParent(g);
		logic_uScript_HideHUDElement_uScript_HideHUDElement_263.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_264.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_267.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_269.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_271.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_272.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_275.SetParent(g);
		logic_uScript_SendAnaliticsEvent_uScript_SendAnaliticsEvent_276.SetParent(g);
		logic_uScript_SendAnaliticsEvent_uScript_SendAnaliticsEvent_277.SetParent(g);
		logic_uScript_LockHudGroup_uScript_LockHudGroup_278.SetParent(g);
		logic_uScript_LockHudGroup_uScript_LockHudGroup_279.SetParent(g);
		logic_uScript_LockHudGroup_uScript_LockHudGroup_280.SetParent(g);
		logic_uScript_LockHudGroup_uScript_LockHudGroup_281.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_576.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_583.SetParent(g);
		owner_Connection_1 = parentGameObject;
		owner_Connection_7 = parentGameObject;
		owner_Connection_10 = parentGameObject;
		owner_Connection_68 = parentGameObject;
		owner_Connection_206 = parentGameObject;
	}

	public void Awake()
	{
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_18.Awake();
		logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_SubGraph_Crafting_Tutorial_AttachBlockToBase_28.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_35.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_38.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_40.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_126.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_129.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_130.Awake();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_139.Awake();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_141.Awake();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_144.Awake();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_146.Awake();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_148.Awake();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_150.Awake();
		logic_SubGraph_Crafting_Tutorial_Finish_SubGraph_Crafting_Tutorial_Finish_195.Awake();
		logic_SubGraph_Crafting_Tutorial_Init_SubGraph_Crafting_Tutorial_Init_197.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_583.Awake();
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_18.Out += SubGraph_Crafting_Tutorial_ManageBlock_Out_18;
		logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_SubGraph_Crafting_Tutorial_AttachBlockToBase_28.Block_Attached += SubGraph_Crafting_Tutorial_AttachBlockToBase_Block_Attached_28;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_35.Save_Out += SubGraph_SaveLoadBool_Save_Out_35;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_35.Load_Out += SubGraph_SaveLoadBool_Load_Out_35;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_35.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_35;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_38.Save_Out += SubGraph_SaveLoadBool_Save_Out_38;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_38.Load_Out += SubGraph_SaveLoadBool_Load_Out_38;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_38.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_38;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_40.Save_Out += SubGraph_SaveLoadBool_Save_Out_40;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_40.Load_Out += SubGraph_SaveLoadBool_Load_Out_40;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_40.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_40;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_126.Save_Out += SubGraph_SaveLoadBool_Save_Out_126;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_126.Load_Out += SubGraph_SaveLoadBool_Load_Out_126;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_126.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_126;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_129.Save_Out += SubGraph_SaveLoadBool_Save_Out_129;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_129.Load_Out += SubGraph_SaveLoadBool_Load_Out_129;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_129.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_129;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_130.Save_Out += SubGraph_SaveLoadBool_Save_Out_130;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_130.Load_Out += SubGraph_SaveLoadBool_Load_Out_130;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_130.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_130;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_138.Output1 += uScriptCon_ManualSwitch_Output1_138;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_138.Output2 += uScriptCon_ManualSwitch_Output2_138;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_138.Output3 += uScriptCon_ManualSwitch_Output3_138;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_138.Output4 += uScriptCon_ManualSwitch_Output4_138;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_138.Output5 += uScriptCon_ManualSwitch_Output5_138;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_138.Output6 += uScriptCon_ManualSwitch_Output6_138;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_138.Output7 += uScriptCon_ManualSwitch_Output7_138;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_138.Output8 += uScriptCon_ManualSwitch_Output8_138;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_139.Save_Out += SubGraph_SaveLoadInt_Save_Out_139;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_139.Load_Out += SubGraph_SaveLoadInt_Load_Out_139;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_139.Restart_Out += SubGraph_SaveLoadInt_Restart_Out_139;
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_141.Out += SubGraph_LoadObjectiveStates_Out_141;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_144.Out += SubGraph_CompleteObjectiveStage_Out_144;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_146.Out += SubGraph_CompleteObjectiveStage_Out_146;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_148.Out += SubGraph_CompleteObjectiveStage_Out_148;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_150.Out += SubGraph_CompleteObjectiveStage_Out_150;
		logic_SubGraph_Crafting_Tutorial_Finish_SubGraph_Crafting_Tutorial_Finish_195.Out += SubGraph_Crafting_Tutorial_Finish_Out_195;
		logic_SubGraph_Crafting_Tutorial_Init_SubGraph_Crafting_Tutorial_Init_197.Out += SubGraph_Crafting_Tutorial_Init_Out_197;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_583.Save_Out += SubGraph_SaveLoadBool_Save_Out_583;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_583.Load_Out += SubGraph_SaveLoadBool_Load_Out_583;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_583.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_583;
	}

	public void Start()
	{
		SyncUnityHooks();
		m_RegisteredForEvents = true;
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_18.Start();
		logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_SubGraph_Crafting_Tutorial_AttachBlockToBase_28.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_35.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_38.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_40.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_126.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_129.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_130.Start();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_139.Start();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_141.Start();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_144.Start();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_146.Start();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_148.Start();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_150.Start();
		logic_SubGraph_Crafting_Tutorial_Finish_SubGraph_Crafting_Tutorial_Finish_195.Start();
		logic_SubGraph_Crafting_Tutorial_Init_SubGraph_Crafting_Tutorial_Init_197.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_583.Start();
	}

	public void OnEnable()
	{
		RegisterForUnityHooks();
		m_RegisteredForEvents = true;
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_18.OnEnable();
		logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_SubGraph_Crafting_Tutorial_AttachBlockToBase_28.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_35.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_38.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_40.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_126.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_129.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_130.OnEnable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_139.OnEnable();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_141.OnEnable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_144.OnEnable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_146.OnEnable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_148.OnEnable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_150.OnEnable();
		logic_SubGraph_Crafting_Tutorial_Finish_SubGraph_Crafting_Tutorial_Finish_195.OnEnable();
		logic_SubGraph_Crafting_Tutorial_Init_SubGraph_Crafting_Tutorial_Init_197.OnEnable();
		logic_uScript_CraftingUIHighlightItem_uScript_CraftingUIHighlightItem_208.OnEnable();
		logic_uScript_CraftingUIHighlightBlockCategory_uScript_CraftingUIHighlightBlockCategory_209.OnEnable();
		logic_uScript_CraftingUIHighlightCraftButton_uScript_CraftingUIHighlightCraftButton_211.OnEnable();
		logic_uScript_CraftingUIHighlightCraftButton_uScript_CraftingUIHighlightCraftButton_213.OnEnable();
		logic_uScript_CraftingUIHighlightItem_uScript_CraftingUIHighlightItem_214.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_583.OnEnable();
	}

	public void OnDisable()
	{
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_2.OnDisable();
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_18.OnDisable();
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_21.OnDisable();
		logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_SubGraph_Crafting_Tutorial_AttachBlockToBase_28.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_35.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_38.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_40.OnDisable();
		logic_uScript_GetTankBlock_uScript_GetTankBlock_41.OnDisable();
		logic_uScript_IsBlockHoldingResources_uScript_IsBlockHoldingResources_72.OnDisable();
		logic_uScript_GetTankBlock_uScript_GetTankBlock_85.OnDisable();
		logic_uScript_Wait_uScript_Wait_108.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_126.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_129.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_130.OnDisable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_139.OnDisable();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_141.OnDisable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_144.OnDisable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_146.OnDisable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_148.OnDisable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_150.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_152.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_156.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_159.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_162.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_165.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_168.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_171.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_174.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_175.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_179.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_189.OnDisable();
		logic_SubGraph_Crafting_Tutorial_Finish_SubGraph_Crafting_Tutorial_Finish_195.OnDisable();
		logic_SubGraph_Crafting_Tutorial_Init_SubGraph_Crafting_Tutorial_Init_197.OnDisable();
		logic_uScript_CraftingUIHighlightBlockCategory_uScript_CraftingUIHighlightBlockCategory_209.OnDisable();
		logic_uScript_CraftingUIHighlightCraftButton_uScript_CraftingUIHighlightCraftButton_211.OnDisable();
		logic_uScript_CraftingUIHighlightCraftButton_uScript_CraftingUIHighlightCraftButton_213.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_583.OnDisable();
		UnregisterEventListeners();
		m_RegisteredForEvents = false;
	}

	public void Update()
	{
		if (relayCallCount < 1000)
		{
			relayCallCount = 0;
		}
		if (m_ContinueExecution != null)
		{
			ContinueExecution continueExecution = m_ContinueExecution;
			m_ContinueExecution = null;
			m_Breakpoint = false;
			continueExecution();
			return;
		}
		UpdateEditorValues();
		SyncEventListeners();
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_18.Update();
		logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_SubGraph_Crafting_Tutorial_AttachBlockToBase_28.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_35.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_38.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_40.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_126.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_129.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_130.Update();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_139.Update();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_141.Update();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_144.Update();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_146.Update();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_148.Update();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_150.Update();
		logic_SubGraph_Crafting_Tutorial_Finish_SubGraph_Crafting_Tutorial_Finish_195.Update();
		logic_SubGraph_Crafting_Tutorial_Init_SubGraph_Crafting_Tutorial_Init_197.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_583.Update();
	}

	public void OnDestroy()
	{
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_18.OnDestroy();
		logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_SubGraph_Crafting_Tutorial_AttachBlockToBase_28.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_35.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_38.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_40.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_126.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_129.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_130.OnDestroy();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_139.OnDestroy();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_141.OnDestroy();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_144.OnDestroy();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_146.OnDestroy();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_148.OnDestroy();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_150.OnDestroy();
		logic_SubGraph_Crafting_Tutorial_Finish_SubGraph_Crafting_Tutorial_Finish_195.OnDestroy();
		logic_SubGraph_Crafting_Tutorial_Init_SubGraph_Crafting_Tutorial_Init_197.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_583.OnDestroy();
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_18.Out -= SubGraph_Crafting_Tutorial_ManageBlock_Out_18;
		logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_SubGraph_Crafting_Tutorial_AttachBlockToBase_28.Block_Attached -= SubGraph_Crafting_Tutorial_AttachBlockToBase_Block_Attached_28;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_35.Save_Out -= SubGraph_SaveLoadBool_Save_Out_35;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_35.Load_Out -= SubGraph_SaveLoadBool_Load_Out_35;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_35.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_35;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_38.Save_Out -= SubGraph_SaveLoadBool_Save_Out_38;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_38.Load_Out -= SubGraph_SaveLoadBool_Load_Out_38;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_38.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_38;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_40.Save_Out -= SubGraph_SaveLoadBool_Save_Out_40;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_40.Load_Out -= SubGraph_SaveLoadBool_Load_Out_40;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_40.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_40;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_126.Save_Out -= SubGraph_SaveLoadBool_Save_Out_126;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_126.Load_Out -= SubGraph_SaveLoadBool_Load_Out_126;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_126.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_126;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_129.Save_Out -= SubGraph_SaveLoadBool_Save_Out_129;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_129.Load_Out -= SubGraph_SaveLoadBool_Load_Out_129;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_129.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_129;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_130.Save_Out -= SubGraph_SaveLoadBool_Save_Out_130;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_130.Load_Out -= SubGraph_SaveLoadBool_Load_Out_130;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_130.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_130;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_138.Output1 -= uScriptCon_ManualSwitch_Output1_138;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_138.Output2 -= uScriptCon_ManualSwitch_Output2_138;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_138.Output3 -= uScriptCon_ManualSwitch_Output3_138;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_138.Output4 -= uScriptCon_ManualSwitch_Output4_138;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_138.Output5 -= uScriptCon_ManualSwitch_Output5_138;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_138.Output6 -= uScriptCon_ManualSwitch_Output6_138;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_138.Output7 -= uScriptCon_ManualSwitch_Output7_138;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_138.Output8 -= uScriptCon_ManualSwitch_Output8_138;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_139.Save_Out -= SubGraph_SaveLoadInt_Save_Out_139;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_139.Load_Out -= SubGraph_SaveLoadInt_Load_Out_139;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_139.Restart_Out -= SubGraph_SaveLoadInt_Restart_Out_139;
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_141.Out -= SubGraph_LoadObjectiveStates_Out_141;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_144.Out -= SubGraph_CompleteObjectiveStage_Out_144;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_146.Out -= SubGraph_CompleteObjectiveStage_Out_146;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_148.Out -= SubGraph_CompleteObjectiveStage_Out_148;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_150.Out -= SubGraph_CompleteObjectiveStage_Out_150;
		logic_SubGraph_Crafting_Tutorial_Finish_SubGraph_Crafting_Tutorial_Finish_195.Out -= SubGraph_Crafting_Tutorial_Finish_Out_195;
		logic_SubGraph_Crafting_Tutorial_Init_SubGraph_Crafting_Tutorial_Init_197.Out -= SubGraph_Crafting_Tutorial_Init_Out_197;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_583.Save_Out -= SubGraph_SaveLoadBool_Save_Out_583;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_583.Load_Out -= SubGraph_SaveLoadBool_Load_Out_583;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_583.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_583;
	}

	private void Instance_OnUpdate_0(object o, EventArgs e)
	{
		if (relayCallCount < 1000)
		{
			relayCallCount = 0;
		}
		Relay_OnUpdate_0();
	}

	private void Instance_OnSuspend_0(object o, EventArgs e)
	{
		if (relayCallCount < 1000)
		{
			relayCallCount = 0;
		}
		Relay_OnSuspend_0();
	}

	private void Instance_OnResume_0(object o, EventArgs e)
	{
		if (relayCallCount < 1000)
		{
			relayCallCount = 0;
		}
		Relay_OnResume_0();
	}

	private void Instance_SaveEvent_6(object o, EventArgs e)
	{
		if (relayCallCount < 1000)
		{
			relayCallCount = 0;
		}
		Relay_SaveEvent_6();
	}

	private void Instance_LoadEvent_6(object o, EventArgs e)
	{
		if (relayCallCount < 1000)
		{
			relayCallCount = 0;
		}
		Relay_LoadEvent_6();
	}

	private void Instance_RestartEvent_6(object o, EventArgs e)
	{
		if (relayCallCount < 1000)
		{
			relayCallCount = 0;
		}
		Relay_RestartEvent_6();
	}

	private void Instance_BlockCraftedEvent_9(object o, uScript_BlockCraftedEvent.BlockCraftedEventArgs e)
	{
		if (relayCallCount < 1000)
		{
			relayCallCount = 0;
		}
		event_UnityEngine_GameObject_BlockType_9 = e.BlockType;
		event_UnityEngine_GameObject_BlockTypeTotal_9 = e.BlockTypeTotal;
		event_UnityEngine_GameObject_BlockTotal_9 = e.BlockTotal;
		Relay_BlockCraftedEvent_9();
	}

	private void Instance_BlockCraftedEvent_70(object o, uScript_BlockCraftedEvent.BlockCraftedEventArgs e)
	{
		if (relayCallCount < 1000)
		{
			relayCallCount = 0;
		}
		event_UnityEngine_GameObject_BlockType_70 = e.BlockType;
		event_UnityEngine_GameObject_BlockTypeTotal_70 = e.BlockTypeTotal;
		event_UnityEngine_GameObject_BlockTotal_70 = e.BlockTotal;
		Relay_BlockCraftedEvent_70();
	}

	private void SubGraph_Crafting_Tutorial_ManageBlock_Out_18(object o, SubGraph_Crafting_Tutorial_ManageBlock.LogicEventArgs e)
	{
		logic_SubGraph_Crafting_Tutorial_ManageBlock_block_18 = e.block;
		blockSpawnData = logic_SubGraph_Crafting_Tutorial_ManageBlock_blockSpawnData_18;
		local_RefineryBlock_TankBlock = logic_SubGraph_Crafting_Tutorial_ManageBlock_block_18;
		Relay_Out_18();
	}

	private void SubGraph_Crafting_Tutorial_AttachBlockToBase_Block_Attached_28(object o, SubGraph_Crafting_Tutorial_AttachBlockToBase.LogicEventArgs e)
	{
		Relay_Block_Attached_28();
	}

	private void SubGraph_SaveLoadBool_Save_Out_35(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_35 = e.boolean;
		local_msgIntroShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_35;
		Relay_Save_Out_35();
	}

	private void SubGraph_SaveLoadBool_Load_Out_35(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_35 = e.boolean;
		local_msgIntroShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_35;
		Relay_Load_Out_35();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_35(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_35 = e.boolean;
		local_msgIntroShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_35;
		Relay_Restart_Out_35();
	}

	private void SubGraph_SaveLoadBool_Save_Out_38(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_38 = e.boolean;
		local_msgRefineryAttachedShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_38;
		Relay_Save_Out_38();
	}

	private void SubGraph_SaveLoadBool_Load_Out_38(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_38 = e.boolean;
		local_msgRefineryAttachedShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_38;
		Relay_Load_Out_38();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_38(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_38 = e.boolean;
		local_msgRefineryAttachedShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_38;
		Relay_Restart_Out_38();
	}

	private void SubGraph_SaveLoadBool_Save_Out_40(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_40 = e.boolean;
		local_msgBaseFoundShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_40;
		Relay_Save_Out_40();
	}

	private void SubGraph_SaveLoadBool_Load_Out_40(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_40 = e.boolean;
		local_msgBaseFoundShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_40;
		Relay_Load_Out_40();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_40(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_40 = e.boolean;
		local_msgBaseFoundShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_40;
		Relay_Restart_Out_40();
	}

	private void SubGraph_SaveLoadBool_Save_Out_126(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_126 = e.boolean;
		local_BlockCrafted01_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_126;
		Relay_Save_Out_126();
	}

	private void SubGraph_SaveLoadBool_Load_Out_126(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_126 = e.boolean;
		local_BlockCrafted01_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_126;
		Relay_Load_Out_126();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_126(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_126 = e.boolean;
		local_BlockCrafted01_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_126;
		Relay_Restart_Out_126();
	}

	private void SubGraph_SaveLoadBool_Save_Out_129(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_129 = e.boolean;
		local_BlockCrafted02_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_129;
		Relay_Save_Out_129();
	}

	private void SubGraph_SaveLoadBool_Load_Out_129(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_129 = e.boolean;
		local_BlockCrafted02_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_129;
		Relay_Load_Out_129();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_129(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_129 = e.boolean;
		local_BlockCrafted02_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_129;
		Relay_Restart_Out_129();
	}

	private void SubGraph_SaveLoadBool_Save_Out_130(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_130 = e.boolean;
		local_msgRefineryExplanation02Shown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_130;
		Relay_Save_Out_130();
	}

	private void SubGraph_SaveLoadBool_Load_Out_130(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_130 = e.boolean;
		local_msgRefineryExplanation02Shown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_130;
		Relay_Load_Out_130();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_130(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_130 = e.boolean;
		local_msgRefineryExplanation02Shown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_130;
		Relay_Restart_Out_130();
	}

	private void uScriptCon_ManualSwitch_Output1_138(object o, EventArgs e)
	{
		Relay_Output1_138();
	}

	private void uScriptCon_ManualSwitch_Output2_138(object o, EventArgs e)
	{
		Relay_Output2_138();
	}

	private void uScriptCon_ManualSwitch_Output3_138(object o, EventArgs e)
	{
		Relay_Output3_138();
	}

	private void uScriptCon_ManualSwitch_Output4_138(object o, EventArgs e)
	{
		Relay_Output4_138();
	}

	private void uScriptCon_ManualSwitch_Output5_138(object o, EventArgs e)
	{
		Relay_Output5_138();
	}

	private void uScriptCon_ManualSwitch_Output6_138(object o, EventArgs e)
	{
		Relay_Output6_138();
	}

	private void uScriptCon_ManualSwitch_Output7_138(object o, EventArgs e)
	{
		Relay_Output7_138();
	}

	private void uScriptCon_ManualSwitch_Output8_138(object o, EventArgs e)
	{
		Relay_Output8_138();
	}

	private void SubGraph_SaveLoadInt_Save_Out_139(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_139 = e.integer;
		local_Stage_System_Int32 = logic_SubGraph_SaveLoadInt_integer_139;
		Relay_Save_Out_139();
	}

	private void SubGraph_SaveLoadInt_Load_Out_139(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_139 = e.integer;
		local_Stage_System_Int32 = logic_SubGraph_SaveLoadInt_integer_139;
		Relay_Load_Out_139();
	}

	private void SubGraph_SaveLoadInt_Restart_Out_139(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_139 = e.integer;
		local_Stage_System_Int32 = logic_SubGraph_SaveLoadInt_integer_139;
		Relay_Restart_Out_139();
	}

	private void SubGraph_LoadObjectiveStates_Out_141(object o, SubGraph_LoadObjectiveStates.LogicEventArgs e)
	{
		Relay_Out_141();
	}

	private void SubGraph_CompleteObjectiveStage_Out_144(object o, SubGraph_CompleteObjectiveStage.LogicEventArgs e)
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_144 = e.objectiveStage;
		local_Stage_System_Int32 = logic_SubGraph_CompleteObjectiveStage_objectiveStage_144;
		Relay_Out_144();
	}

	private void SubGraph_CompleteObjectiveStage_Out_146(object o, SubGraph_CompleteObjectiveStage.LogicEventArgs e)
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_146 = e.objectiveStage;
		local_Stage_System_Int32 = logic_SubGraph_CompleteObjectiveStage_objectiveStage_146;
		Relay_Out_146();
	}

	private void SubGraph_CompleteObjectiveStage_Out_148(object o, SubGraph_CompleteObjectiveStage.LogicEventArgs e)
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_148 = e.objectiveStage;
		local_Stage_System_Int32 = logic_SubGraph_CompleteObjectiveStage_objectiveStage_148;
		Relay_Out_148();
	}

	private void SubGraph_CompleteObjectiveStage_Out_150(object o, SubGraph_CompleteObjectiveStage.LogicEventArgs e)
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_150 = e.objectiveStage;
		local_Stage_System_Int32 = logic_SubGraph_CompleteObjectiveStage_objectiveStage_150;
		Relay_Out_150();
	}

	private void SubGraph_Crafting_Tutorial_Finish_Out_195(object o, SubGraph_Crafting_Tutorial_Finish.LogicEventArgs e)
	{
		Relay_Out_195();
	}

	private void SubGraph_Crafting_Tutorial_Init_Out_197(object o, SubGraph_Crafting_Tutorial_Init.LogicEventArgs e)
	{
		logic_SubGraph_Crafting_Tutorial_Init_CraftingBaseTech_197 = e.CraftingBaseTech;
		logic_SubGraph_Crafting_Tutorial_Init_NPCTech_197 = e.NPCTech;
		local_CraftingBaseTech_Tank = logic_SubGraph_Crafting_Tutorial_Init_CraftingBaseTech_197;
		local_NPCTech_Tank = logic_SubGraph_Crafting_Tutorial_Init_NPCTech_197;
		Relay_Out_197();
	}

	private void SubGraph_SaveLoadBool_Save_Out_583(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_583 = e.boolean;
		local_CraftingInProgress02_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_583;
		Relay_Save_Out_583();
	}

	private void SubGraph_SaveLoadBool_Load_Out_583(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_583 = e.boolean;
		local_CraftingInProgress02_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_583;
		Relay_Load_Out_583();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_583(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_583 = e.boolean;
		local_CraftingInProgress02_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_583;
		Relay_Restart_Out_583();
	}

	private void Relay_OnUpdate_0()
	{
		if (!CheckDebugBreak("7cd4daee-89ad-496b-84c5-7dc87ae39b95", "Encounter_Update", Relay_OnUpdate_0))
		{
			Relay_In_197();
		}
	}

	private void Relay_OnSuspend_0()
	{
		CheckDebugBreak("7cd4daee-89ad-496b-84c5-7dc87ae39b95", "Encounter_Update", Relay_OnSuspend_0);
	}

	private void Relay_OnResume_0()
	{
		CheckDebugBreak("7cd4daee-89ad-496b-84c5-7dc87ae39b95", "Encounter_Update", Relay_OnResume_0);
	}

	private void Relay_In_2()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("35f1b408-29d7-4d0c-9a88-bda6af4e7e14", "Distance_Is_player_in_range_of_tech", Relay_In_2))
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
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at Distance/Is player in range of tech.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Pause_3()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("dffc37f9-0be2-44b1-9062-54b6c58aeef3", "uScript_PausePopulation", Relay_Pause_3))
			{
				logic_uScript_PausePopulation_uScript_PausePopulation_3.Pause();
				if (logic_uScript_PausePopulation_uScript_PausePopulation_3.Out)
				{
					Relay_True_24();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at uScript_PausePopulation.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_UnPause_3()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("dffc37f9-0be2-44b1-9062-54b6c58aeef3", "uScript_PausePopulation", Relay_UnPause_3))
			{
				logic_uScript_PausePopulation_uScript_PausePopulation_3.UnPause();
				if (logic_uScript_PausePopulation_uScript_PausePopulation_3.Out)
				{
					Relay_True_24();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at uScript_PausePopulation.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Pause_4()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("0f71c137-359d-4fbe-aadc-b067912cb925", "uScript_PausePopulation", Relay_Pause_4))
			{
				logic_uScript_PausePopulation_uScript_PausePopulation_4.Pause();
				if (logic_uScript_PausePopulation_uScript_PausePopulation_4.Out)
				{
					Relay_In_236();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at uScript_PausePopulation.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_UnPause_4()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("0f71c137-359d-4fbe-aadc-b067912cb925", "uScript_PausePopulation", Relay_UnPause_4))
			{
				logic_uScript_PausePopulation_uScript_PausePopulation_4.UnPause();
				if (logic_uScript_PausePopulation_uScript_PausePopulation_4.Out)
				{
					Relay_In_236();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at uScript_PausePopulation.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_SaveEvent_6()
	{
		if (!CheckDebugBreak("4c2f0bca-d6df-48c4-8341-cc540d924d11", "uScript_SaveLoad", Relay_SaveEvent_6))
		{
			Relay_Save_139();
		}
	}

	private void Relay_LoadEvent_6()
	{
		if (!CheckDebugBreak("4c2f0bca-d6df-48c4-8341-cc540d924d11", "uScript_SaveLoad", Relay_LoadEvent_6))
		{
			Relay_Load_139();
		}
	}

	private void Relay_RestartEvent_6()
	{
		if (!CheckDebugBreak("4c2f0bca-d6df-48c4-8341-cc540d924d11", "uScript_SaveLoad", Relay_RestartEvent_6))
		{
			Relay_Restart_139();
		}
	}

	private void Relay_BlockCraftedEvent_9()
	{
		if (!CheckDebugBreak("7d96ee92-474c-43ea-9b10-93dfad077895", "uScript_BlockCraftedEvent", Relay_BlockCraftedEvent_9))
		{
			local_11_BlockTypes = event_UnityEngine_GameObject_BlockType_9;
			Relay_In_12();
		}
	}

	private void Relay_In_12()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("2aedea98-eba7-4db5-ad2a-936ce7db4643", "Compare_BlockTypes", Relay_In_12))
			{
				logic_uScript_CompareBlockTypes_A_12 = local_11_BlockTypes;
				logic_uScript_CompareBlockTypes_B_12 = blockTypeToCraft02;
				logic_uScript_CompareBlockTypes_uScript_CompareBlockTypes_12.In(logic_uScript_CompareBlockTypes_A_12, logic_uScript_CompareBlockTypes_B_12);
				if (logic_uScript_CompareBlockTypes_uScript_CompareBlockTypes_12.EqualTo)
				{
					Relay_True_107();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at Compare BlockTypes.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_15()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("fa22800f-c261-4239-b2d0-36f763fe5119", "uScript_ClearOnScreenMessagesWithTag", Relay_In_15))
			{
				logic_uScript_ClearOnScreenMessagesWithTag_tag_15 = messageTag;
				logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_15.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_15, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_15);
				if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_15.Out)
				{
					Relay_In_36();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at uScript_ClearOnScreenMessagesWithTag.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Out_18()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("18dc04fc-e01d-41bf-9bf1-28ee885de2f2", "SubGraph_Crafting_Tutorial_ManageBlock", Relay_Out_18))
			{
				Relay_In_85();
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at SubGraph_Crafting_Tutorial_ManageBlock.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_18()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("18dc04fc-e01d-41bf-9bf1-28ee885de2f2", "SubGraph_Crafting_Tutorial_ManageBlock", Relay_In_18))
			{
				int num = 0;
				Array array = blockSpawnData;
				if (logic_SubGraph_Crafting_Tutorial_ManageBlock_blockSpawnData_18.Length != num + array.Length)
				{
					Array.Resize(ref logic_SubGraph_Crafting_Tutorial_ManageBlock_blockSpawnData_18, num + array.Length);
				}
				Array.Copy(array, 0, logic_SubGraph_Crafting_Tutorial_ManageBlock_blockSpawnData_18, num, array.Length);
				num += array.Length;
				logic_SubGraph_Crafting_Tutorial_ManageBlock_block_18 = local_RefineryBlock_TankBlock;
				logic_SubGraph_Crafting_Tutorial_ManageBlock_msgBlockOutsideArea_18 = msgBlockOutsideArea;
				logic_SubGraph_Crafting_Tutorial_ManageBlock_messageSpeaker_18 = messageSpeaker;
				logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_18.In(ref logic_SubGraph_Crafting_Tutorial_ManageBlock_blockSpawnData_18, logic_SubGraph_Crafting_Tutorial_ManageBlock_blockIndex_18, ref logic_SubGraph_Crafting_Tutorial_ManageBlock_block_18, logic_SubGraph_Crafting_Tutorial_ManageBlock_allowAnchoring_18, logic_SubGraph_Crafting_Tutorial_ManageBlock_msgBlockOutsideArea_18, logic_SubGraph_Crafting_Tutorial_ManageBlock_messageSpeaker_18);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at SubGraph_Crafting_Tutorial_ManageBlock.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_19()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("b6a09976-f8f7-4ec5-b6d2-0c3d0dcc30ef", "Compare_Bool", Relay_In_19))
			{
				logic_uScriptCon_CompareBool_Bool_19 = local_msgIntroShown_System_Boolean;
				logic_uScriptCon_CompareBool_uScriptCon_CompareBool_19.In(logic_uScriptCon_CompareBool_Bool_19);
				bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_19.True;
				bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_19.False;
				if (num)
				{
					Relay_In_2();
				}
				if (flag)
				{
					Relay_True_23();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at Compare Bool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_21()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("5d6d76a4-2210-4de6-8146-f50a8dfc157a", "Distance_Is_player_in_range_of_tech", Relay_In_21))
			{
				logic_uScript_IsPlayerInRangeOfTech_tech_21 = local_CraftingBaseTech_Tank;
				logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_21.In(logic_uScript_IsPlayerInRangeOfTech_tech_21, logic_uScript_IsPlayerInRangeOfTech_range_21, logic_uScript_IsPlayerInRangeOfTech_techs_21);
				if (logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_21.InRange)
				{
					Relay_In_19();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at Distance/Is player in range of tech.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_True_23()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("7e8250b3-c7dd-467c-9f1e-a91da103fb13", "Set_Bool", Relay_True_23))
			{
				logic_uScriptAct_SetBool_uScriptAct_SetBool_23.True(out logic_uScriptAct_SetBool_Target_23);
				local_msgIntroShown_System_Boolean = logic_uScriptAct_SetBool_Target_23;
				if (logic_uScriptAct_SetBool_uScriptAct_SetBool_23.Out)
				{
					Relay_In_152();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at Set Bool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_False_23()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("7e8250b3-c7dd-467c-9f1e-a91da103fb13", "Set_Bool", Relay_False_23))
			{
				logic_uScriptAct_SetBool_uScriptAct_SetBool_23.False(out logic_uScriptAct_SetBool_Target_23);
				local_msgIntroShown_System_Boolean = logic_uScriptAct_SetBool_Target_23;
				if (logic_uScriptAct_SetBool_uScriptAct_SetBool_23.Out)
				{
					Relay_In_152();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at Set Bool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_True_24()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("a1d619a5-7d4c-4a73-a08e-4cadc7250d2d", "Set_Bool", Relay_True_24))
			{
				logic_uScriptAct_SetBool_uScriptAct_SetBool_24.True(out logic_uScriptAct_SetBool_Target_24);
				local_NearBase_System_Boolean = logic_uScriptAct_SetBool_Target_24;
				if (logic_uScriptAct_SetBool_uScriptAct_SetBool_24.Out)
				{
					Relay_In_138();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at Set Bool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_False_24()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("a1d619a5-7d4c-4a73-a08e-4cadc7250d2d", "Set_Bool", Relay_False_24))
			{
				logic_uScriptAct_SetBool_uScriptAct_SetBool_24.False(out logic_uScriptAct_SetBool_Target_24);
				local_NearBase_System_Boolean = logic_uScriptAct_SetBool_Target_24;
				if (logic_uScriptAct_SetBool_uScriptAct_SetBool_24.Out)
				{
					Relay_In_138();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at Set Bool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Block_Attached_28()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("45669a85-a4be-4c9e-b2ff-d97d51ba4c0d", "SubGraph_Crafting_Tutorial_AttachBlockToBase", Relay_Block_Attached_28))
			{
				Relay_In_261();
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at SubGraph_Crafting_Tutorial_AttachBlockToBase.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_28()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("45669a85-a4be-4c9e-b2ff-d97d51ba4c0d", "SubGraph_Crafting_Tutorial_AttachBlockToBase", Relay_In_28))
			{
				logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_Block_28 = local_RefineryBlock_TankBlock;
				logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_CraftingBaseTech_28 = local_CraftingBaseTech_Tank;
				int num = 0;
				Array array = ghostBlockRefinery;
				if (logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_ghostBlockData_28.Length != num + array.Length)
				{
					Array.Resize(ref logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_ghostBlockData_28, num + array.Length);
				}
				Array.Copy(array, 0, logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_ghostBlockData_28, num, array.Length);
				num += array.Length;
				logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_SubGraph_Crafting_Tutorial_AttachBlockToBase_28.In(logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_Block_28, logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_CraftingBaseTech_28, logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_ghostBlockData_28, logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_BlockType_28, logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_BlockPosition_28);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at SubGraph_Crafting_Tutorial_AttachBlockToBase.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_31()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("ccd61b72-7e89-40a8-9ad8-7a92f3da0a6a", "Compare_Bool", Relay_In_31))
			{
				logic_uScriptCon_CompareBool_Bool_31 = local_msgResourcesInSiloShown_System_Boolean;
				logic_uScriptCon_CompareBool_uScriptCon_CompareBool_31.In(logic_uScriptCon_CompareBool_Bool_31);
				bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_31.True;
				bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_31.False;
				if (num)
				{
					Relay_In_165();
				}
				if (flag)
				{
					Relay_In_162();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at Compare Bool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_True_33()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("4fd71e22-6c28-4a58-8371-f23370892377", "Set_Bool", Relay_True_33))
			{
				logic_uScriptAct_SetBool_uScriptAct_SetBool_33.True(out logic_uScriptAct_SetBool_Target_33);
				local_msgResourcesInSiloShown_System_Boolean = logic_uScriptAct_SetBool_Target_33;
				if (logic_uScriptAct_SetBool_uScriptAct_SetBool_33.Out)
				{
					Relay_In_28();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at Set Bool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_False_33()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("4fd71e22-6c28-4a58-8371-f23370892377", "Set_Bool", Relay_False_33))
			{
				logic_uScriptAct_SetBool_uScriptAct_SetBool_33.False(out logic_uScriptAct_SetBool_Target_33);
				local_msgResourcesInSiloShown_System_Boolean = logic_uScriptAct_SetBool_Target_33;
				if (logic_uScriptAct_SetBool_uScriptAct_SetBool_33.Out)
				{
					Relay_In_28();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at Set Bool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Save_Out_35()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("cb8d3118-0363-427b-a13d-703e5861f397", "", Relay_Save_Out_35))
			{
				Relay_Save_38();
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Load_Out_35()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("cb8d3118-0363-427b-a13d-703e5861f397", "", Relay_Load_Out_35))
			{
				Relay_Load_38();
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Restart_Out_35()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("cb8d3118-0363-427b-a13d-703e5861f397", "", Relay_Restart_Out_35))
			{
				Relay_Set_False_38();
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Save_35()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("cb8d3118-0363-427b-a13d-703e5861f397", "", Relay_Save_35))
			{
				logic_SubGraph_SaveLoadBool_boolean_35 = local_msgIntroShown_System_Boolean;
				logic_SubGraph_SaveLoadBool_boolAsVariable_35 = local_msgIntroShown_System_Boolean;
				logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_35.Save(ref logic_SubGraph_SaveLoadBool_boolean_35, logic_SubGraph_SaveLoadBool_boolAsVariable_35, logic_SubGraph_SaveLoadBool_uniqueID_35);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Load_35()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("cb8d3118-0363-427b-a13d-703e5861f397", "", Relay_Load_35))
			{
				logic_SubGraph_SaveLoadBool_boolean_35 = local_msgIntroShown_System_Boolean;
				logic_SubGraph_SaveLoadBool_boolAsVariable_35 = local_msgIntroShown_System_Boolean;
				logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_35.Load(ref logic_SubGraph_SaveLoadBool_boolean_35, logic_SubGraph_SaveLoadBool_boolAsVariable_35, logic_SubGraph_SaveLoadBool_uniqueID_35);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Set_True_35()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("cb8d3118-0363-427b-a13d-703e5861f397", "", Relay_Set_True_35))
			{
				logic_SubGraph_SaveLoadBool_boolean_35 = local_msgIntroShown_System_Boolean;
				logic_SubGraph_SaveLoadBool_boolAsVariable_35 = local_msgIntroShown_System_Boolean;
				logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_35.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_35, logic_SubGraph_SaveLoadBool_boolAsVariable_35, logic_SubGraph_SaveLoadBool_uniqueID_35);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Set_False_35()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("cb8d3118-0363-427b-a13d-703e5861f397", "", Relay_Set_False_35))
			{
				logic_SubGraph_SaveLoadBool_boolean_35 = local_msgIntroShown_System_Boolean;
				logic_SubGraph_SaveLoadBool_boolAsVariable_35 = local_msgIntroShown_System_Boolean;
				logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_35.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_35, logic_SubGraph_SaveLoadBool_boolAsVariable_35, logic_SubGraph_SaveLoadBool_uniqueID_35);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_36()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("aaf26558-f3fc-4266-99f9-b8945bdbc1e9", "uScript_HideArrow", Relay_In_36))
			{
				logic_uScript_HideArrow_uScript_HideArrow_36.In();
				if (logic_uScript_HideArrow_uScript_HideArrow_36.Out)
				{
					Relay_In_272();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at uScript_HideArrow.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Save_Out_38()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("936ed4c0-0080-47a7-a033-0f3a49e9a686", "", Relay_Save_Out_38))
			{
				Relay_Save_40();
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Load_Out_38()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("936ed4c0-0080-47a7-a033-0f3a49e9a686", "", Relay_Load_Out_38))
			{
				Relay_Load_40();
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Restart_Out_38()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("936ed4c0-0080-47a7-a033-0f3a49e9a686", "", Relay_Restart_Out_38))
			{
				Relay_Set_False_40();
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Save_38()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("936ed4c0-0080-47a7-a033-0f3a49e9a686", "", Relay_Save_38))
			{
				logic_SubGraph_SaveLoadBool_boolean_38 = local_msgRefineryAttachedShown_System_Boolean;
				logic_SubGraph_SaveLoadBool_boolAsVariable_38 = local_msgRefineryAttachedShown_System_Boolean;
				logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_38.Save(ref logic_SubGraph_SaveLoadBool_boolean_38, logic_SubGraph_SaveLoadBool_boolAsVariable_38, logic_SubGraph_SaveLoadBool_uniqueID_38);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Load_38()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("936ed4c0-0080-47a7-a033-0f3a49e9a686", "", Relay_Load_38))
			{
				logic_SubGraph_SaveLoadBool_boolean_38 = local_msgRefineryAttachedShown_System_Boolean;
				logic_SubGraph_SaveLoadBool_boolAsVariable_38 = local_msgRefineryAttachedShown_System_Boolean;
				logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_38.Load(ref logic_SubGraph_SaveLoadBool_boolean_38, logic_SubGraph_SaveLoadBool_boolAsVariable_38, logic_SubGraph_SaveLoadBool_uniqueID_38);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Set_True_38()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("936ed4c0-0080-47a7-a033-0f3a49e9a686", "", Relay_Set_True_38))
			{
				logic_SubGraph_SaveLoadBool_boolean_38 = local_msgRefineryAttachedShown_System_Boolean;
				logic_SubGraph_SaveLoadBool_boolAsVariable_38 = local_msgRefineryAttachedShown_System_Boolean;
				logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_38.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_38, logic_SubGraph_SaveLoadBool_boolAsVariable_38, logic_SubGraph_SaveLoadBool_uniqueID_38);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Set_False_38()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("936ed4c0-0080-47a7-a033-0f3a49e9a686", "", Relay_Set_False_38))
			{
				logic_SubGraph_SaveLoadBool_boolean_38 = local_msgRefineryAttachedShown_System_Boolean;
				logic_SubGraph_SaveLoadBool_boolAsVariable_38 = local_msgRefineryAttachedShown_System_Boolean;
				logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_38.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_38, logic_SubGraph_SaveLoadBool_boolAsVariable_38, logic_SubGraph_SaveLoadBool_uniqueID_38);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Save_Out_40()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("0cb5f346-8142-477c-a414-3d1dce72854e", "", Relay_Save_Out_40))
			{
				Relay_Save_126();
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Load_Out_40()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("0cb5f346-8142-477c-a414-3d1dce72854e", "", Relay_Load_Out_40))
			{
				Relay_Load_126();
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Restart_Out_40()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("0cb5f346-8142-477c-a414-3d1dce72854e", "", Relay_Restart_Out_40))
			{
				Relay_Set_False_126();
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Save_40()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("0cb5f346-8142-477c-a414-3d1dce72854e", "", Relay_Save_40))
			{
				logic_SubGraph_SaveLoadBool_boolean_40 = local_msgBaseFoundShown_System_Boolean;
				logic_SubGraph_SaveLoadBool_boolAsVariable_40 = local_msgBaseFoundShown_System_Boolean;
				logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_40.Save(ref logic_SubGraph_SaveLoadBool_boolean_40, logic_SubGraph_SaveLoadBool_boolAsVariable_40, logic_SubGraph_SaveLoadBool_uniqueID_40);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Load_40()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("0cb5f346-8142-477c-a414-3d1dce72854e", "", Relay_Load_40))
			{
				logic_SubGraph_SaveLoadBool_boolean_40 = local_msgBaseFoundShown_System_Boolean;
				logic_SubGraph_SaveLoadBool_boolAsVariable_40 = local_msgBaseFoundShown_System_Boolean;
				logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_40.Load(ref logic_SubGraph_SaveLoadBool_boolean_40, logic_SubGraph_SaveLoadBool_boolAsVariable_40, logic_SubGraph_SaveLoadBool_uniqueID_40);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Set_True_40()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("0cb5f346-8142-477c-a414-3d1dce72854e", "", Relay_Set_True_40))
			{
				logic_SubGraph_SaveLoadBool_boolean_40 = local_msgBaseFoundShown_System_Boolean;
				logic_SubGraph_SaveLoadBool_boolAsVariable_40 = local_msgBaseFoundShown_System_Boolean;
				logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_40.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_40, logic_SubGraph_SaveLoadBool_boolAsVariable_40, logic_SubGraph_SaveLoadBool_uniqueID_40);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Set_False_40()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("0cb5f346-8142-477c-a414-3d1dce72854e", "", Relay_Set_False_40))
			{
				logic_SubGraph_SaveLoadBool_boolean_40 = local_msgBaseFoundShown_System_Boolean;
				logic_SubGraph_SaveLoadBool_boolAsVariable_40 = local_msgBaseFoundShown_System_Boolean;
				logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_40.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_40, logic_SubGraph_SaveLoadBool_boolAsVariable_40, logic_SubGraph_SaveLoadBool_uniqueID_40);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_41()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("f5c07786-d204-4e5a-9e90-e1cef0aaf840", "Tank_Get_Tank_Block", Relay_In_41))
			{
				logic_uScript_GetTankBlock_tank_41 = local_CraftingBaseTech_Tank;
				logic_uScript_GetTankBlock_blockType_41 = blockTypeSilo;
				logic_uScript_GetTankBlock_Return_41 = logic_uScript_GetTankBlock_uScript_GetTankBlock_41.In(logic_uScript_GetTankBlock_tank_41, logic_uScript_GetTankBlock_blockType_41);
				local_73_TankBlock = logic_uScript_GetTankBlock_Return_41;
				if (logic_uScript_GetTankBlock_uScript_GetTankBlock_41.Returned)
				{
					Relay_In_72();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at Tank/Get Tank Block.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_True_47()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("7bc115b3-feb9-4a50-9046-6a180db97742", "Set_Bool", Relay_True_47))
			{
				logic_uScriptAct_SetBool_uScriptAct_SetBool_47.True(out logic_uScriptAct_SetBool_Target_47);
				local_CraftingInProgress01_System_Boolean = logic_uScriptAct_SetBool_Target_47;
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at Set Bool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_False_47()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("7bc115b3-feb9-4a50-9046-6a180db97742", "Set_Bool", Relay_False_47))
			{
				logic_uScriptAct_SetBool_uScriptAct_SetBool_47.False(out logic_uScriptAct_SetBool_Target_47);
				local_CraftingInProgress01_System_Boolean = logic_uScriptAct_SetBool_Target_47;
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at Set Bool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_49()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("085874d6-9d72-46d5-90cf-b2d1b701adb3", "uScript_LockTechInteraction", Relay_In_49))
			{
				logic_uScript_LockTechInteraction_tech_49 = local_CraftingBaseTech_Tank;
				logic_uScript_LockTechInteraction_uScript_LockTechInteraction_49.In(logic_uScript_LockTechInteraction_tech_49, logic_uScript_LockTechInteraction_excludedBlocks_49);
				if (logic_uScript_LockTechInteraction_uScript_LockTechInteraction_49.Out)
				{
					Relay_In_60();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at uScript_LockTechInteraction.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_50()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("f9b17138-2ea0-4248-adef-515329cd38d6", "uScript_HideArrow", Relay_In_50))
			{
				logic_uScript_HideArrow_uScript_HideArrow_50.In();
				if (logic_uScript_HideArrow_uScript_HideArrow_50.Out)
				{
					Relay_In_267();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at uScript_HideArrow.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_52()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("7ed69609-6c93-41d3-ad13-0f70f4a2a787", "uScript_LockPlayerInput", Relay_In_52))
			{
				logic_uScript_LockPlayerInput_uScript_LockPlayerInput_52.In(logic_uScript_LockPlayerInput_lockInput_52, logic_uScript_LockPlayerInput_includeCamera_52);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at uScript_LockPlayerInput.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_54()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("a51b24d8-8d38-42e4-93de-87f13e57b80b", "uScript_LockTechInteraction", Relay_In_54))
			{
				logic_uScript_LockTechInteraction_tech_54 = local_CraftingBaseTech_Tank;
				logic_uScript_LockTechInteraction_uScript_LockTechInteraction_54.In(logic_uScript_LockTechInteraction_tech_54, logic_uScript_LockTechInteraction_excludedBlocks_54);
				if (logic_uScript_LockTechInteraction_uScript_LockTechInteraction_54.Out)
				{
					Relay_In_52();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at uScript_LockTechInteraction.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_55()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("af17a834-48e9-4863-8f9e-b9c661077549", "uScript_PointArrowAtVisible", Relay_In_55))
			{
				logic_uScript_PointArrowAtVisible_targetObject_55 = local_FabricatorBlock_TankBlock;
				logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_55.In(logic_uScript_PointArrowAtVisible_targetObject_55, logic_uScript_PointArrowAtVisible_timeToShowFor_55, logic_uScript_PointArrowAtVisible_offset_55);
				if (logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_55.Out)
				{
					Relay_In_264();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at uScript_PointArrowAtVisible.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_58()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("4bf1412d-30b5-42b9-a71e-49c02f18b94f", "uScript_LockTechInteraction", Relay_In_58))
			{
				logic_uScript_LockTechInteraction_tech_58 = local_CraftingBaseTech_Tank;
				logic_uScript_LockTechInteraction_uScript_LockTechInteraction_58.In(logic_uScript_LockTechInteraction_tech_58, logic_uScript_LockTechInteraction_excludedBlocks_58);
				if (logic_uScript_LockTechInteraction_uScript_LockTechInteraction_58.Out)
				{
					Relay_In_55();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at uScript_LockTechInteraction.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_60()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("be166ee3-5326-4500-a75f-efc4385ef07c", "uScript_LockPlayerInput", Relay_In_60))
			{
				logic_uScript_LockPlayerInput_uScript_LockPlayerInput_60.In(logic_uScript_LockPlayerInput_lockInput_60, logic_uScript_LockPlayerInput_includeCamera_60);
				if (logic_uScript_LockPlayerInput_uScript_LockPlayerInput_60.Out)
				{
					Relay_Category_209();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at uScript_LockPlayerInput.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_62()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("0493985a-4ff1-4319-bea7-1ebd6f2db7c6", "Compare_Bool", Relay_In_62))
			{
				logic_uScriptCon_CompareBool_Bool_62 = local_CraftingMenuOpened01_System_Boolean;
				logic_uScriptCon_CompareBool_uScriptCon_CompareBool_62.In(logic_uScriptCon_CompareBool_Bool_62);
				bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_62.True;
				bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_62.False;
				if (num)
				{
					Relay_In_253();
				}
				if (flag)
				{
					Relay_In_168();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at Compare Bool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_63()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("8ff08fd0-9a87-490c-b091-e2398c03a4f6", "Compare_Bool", Relay_In_63))
			{
				logic_uScriptCon_CompareBool_Bool_63 = local_CraftingInProgress01_System_Boolean;
				logic_uScriptCon_CompareBool_uScriptCon_CompareBool_63.In(logic_uScriptCon_CompareBool_Bool_63);
				bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_63.True;
				bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_63.False;
				if (num)
				{
					Relay_In_123();
				}
				if (flag)
				{
					Relay_In_49();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at Compare Bool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_True_65()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("9e967fac-a034-467b-b4f7-fb468ce929e4", "Set_Bool", Relay_True_65))
			{
				logic_uScriptAct_SetBool_uScriptAct_SetBool_65.True(out logic_uScriptAct_SetBool_Target_65);
				local_CraftingMenuOpened01_System_Boolean = logic_uScriptAct_SetBool_Target_65;
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at Set Bool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_False_65()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("9e967fac-a034-467b-b4f7-fb468ce929e4", "Set_Bool", Relay_False_65))
			{
				logic_uScriptAct_SetBool_uScriptAct_SetBool_65.False(out logic_uScriptAct_SetBool_Target_65);
				local_CraftingMenuOpened01_System_Boolean = logic_uScriptAct_SetBool_Target_65;
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at Set Bool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_66()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("a0828faa-36f0-47e3-81a0-c4c2f938b942", "Compare_BlockTypes", Relay_In_66))
			{
				logic_uScript_CompareBlockTypes_A_66 = local_69_BlockTypes;
				logic_uScript_CompareBlockTypes_B_66 = blockTypeToCraft01;
				logic_uScript_CompareBlockTypes_uScript_CompareBlockTypes_66.In(logic_uScript_CompareBlockTypes_A_66, logic_uScript_CompareBlockTypes_B_66);
				if (logic_uScript_CompareBlockTypes_uScript_CompareBlockTypes_66.EqualTo)
				{
					Relay_True_71();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at Compare BlockTypes.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_BlockCraftedEvent_70()
	{
		if (!CheckDebugBreak("7bf319c3-f25f-40a5-b207-449d68607ba3", "uScript_BlockCraftedEvent", Relay_BlockCraftedEvent_70))
		{
			local_69_BlockTypes = event_UnityEngine_GameObject_BlockType_70;
			Relay_In_66();
		}
	}

	private void Relay_True_71()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("7856e412-5a10-4dee-848c-fb106532fc99", "Set_Bool", Relay_True_71))
			{
				logic_uScriptAct_SetBool_uScriptAct_SetBool_71.True(out logic_uScriptAct_SetBool_Target_71);
				local_BlockCrafted01_System_Boolean = logic_uScriptAct_SetBool_Target_71;
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at Set Bool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_False_71()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("7856e412-5a10-4dee-848c-fb106532fc99", "Set_Bool", Relay_False_71))
			{
				logic_uScriptAct_SetBool_uScriptAct_SetBool_71.False(out logic_uScriptAct_SetBool_Target_71);
				local_BlockCrafted01_System_Boolean = logic_uScriptAct_SetBool_Target_71;
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at Set Bool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_72()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("36e36cdd-3968-463b-8921-951ed1f65e23", "uScript_IsBlockHoldingResources", Relay_In_72))
			{
				logic_uScript_IsBlockHoldingResources_block_72 = local_73_TankBlock;
				int num = 0;
				Array array = resourcesToHoldInSilo;
				if (logic_uScript_IsBlockHoldingResources_resources_72.Length != num + array.Length)
				{
					Array.Resize(ref logic_uScript_IsBlockHoldingResources_resources_72, num + array.Length);
				}
				Array.Copy(array, 0, logic_uScript_IsBlockHoldingResources_resources_72, num, array.Length);
				num += array.Length;
				logic_uScript_IsBlockHoldingResources_uScript_IsBlockHoldingResources_72.In(logic_uScript_IsBlockHoldingResources_block_72, logic_uScript_IsBlockHoldingResources_resources_72);
				bool num2 = logic_uScript_IsBlockHoldingResources_uScript_IsBlockHoldingResources_72.True;
				bool flag = logic_uScript_IsBlockHoldingResources_uScript_IsBlockHoldingResources_72.False;
				if (num2)
				{
					Relay_In_146();
				}
				if (flag)
				{
					Relay_In_108();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at uScript_IsBlockHoldingResources.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_75()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("944d2bc9-5972-42ca-9901-d676b07b2839", "uScript_RestrictItemPickup", Relay_In_75))
			{
				logic_uScript_RestrictItemPickup_tech_75 = local_CraftingBaseTech_Tank;
				int num = 0;
				Array array = baseAllowedResourceTypes;
				if (logic_uScript_RestrictItemPickup_typesToAccept_75.Length != num + array.Length)
				{
					Array.Resize(ref logic_uScript_RestrictItemPickup_typesToAccept_75, num + array.Length);
				}
				Array.Copy(array, 0, logic_uScript_RestrictItemPickup_typesToAccept_75, num, array.Length);
				num += array.Length;
				logic_uScript_RestrictItemPickup_uScript_RestrictItemPickup_75.In(logic_uScript_RestrictItemPickup_tech_75, logic_uScript_RestrictItemPickup_typesToAccept_75);
				if (logic_uScript_RestrictItemPickup_uScript_RestrictItemPickup_75.Out)
				{
					Relay_In_207();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at uScript_RestrictItemPickup.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_78()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("49b4e0d6-bcbb-46bb-9afc-b7f56dcd416f", "uScript_LockTechInteraction", Relay_In_78))
			{
				logic_uScript_LockTechInteraction_tech_78 = local_CraftingBaseTech_Tank;
				logic_uScript_LockTechInteraction_uScript_LockTechInteraction_78.In(logic_uScript_LockTechInteraction_tech_78, logic_uScript_LockTechInteraction_excludedBlocks_78);
				if (logic_uScript_LockTechInteraction_uScript_LockTechInteraction_78.Out)
				{
					Relay_In_31();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at uScript_LockTechInteraction.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_80()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("3baa706b-cf4a-4add-8fb8-9f2d43332e8c", "uScript_LockTechInteraction", Relay_In_80))
			{
				logic_uScript_LockTechInteraction_tech_80 = local_CraftingBaseTech_Tank;
				logic_uScript_LockTechInteraction_uScript_LockTechInteraction_80.In(logic_uScript_LockTechInteraction_tech_80, logic_uScript_LockTechInteraction_excludedBlocks_80);
				if (logic_uScript_LockTechInteraction_uScript_LockTechInteraction_80.Out)
				{
					Relay_In_111();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at uScript_LockTechInteraction.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_85()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("8092dddc-c7d0-43b4-9283-c1f46a00f501", "Tank_Get_Tank_Block", Relay_In_85))
			{
				logic_uScript_GetTankBlock_tank_85 = local_CraftingBaseTech_Tank;
				logic_uScript_GetTankBlock_blockType_85 = blockTypeFabricator;
				logic_uScript_GetTankBlock_Return_85 = logic_uScript_GetTankBlock_uScript_GetTankBlock_85.In(logic_uScript_GetTankBlock_tank_85, logic_uScript_GetTankBlock_blockType_85);
				local_FabricatorBlock_TankBlock = logic_uScript_GetTankBlock_Return_85;
				if (logic_uScript_GetTankBlock_uScript_GetTankBlock_85.Returned)
				{
					Relay_In_75();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at Tank/Get Tank Block.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_87()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("f6c8916f-4dc1-4db1-9bf3-37fa88a53c6b", "uScript_LockPause", Relay_In_87))
			{
				logic_uScript_LockPause_uScript_LockPause_87.In(logic_uScript_LockPause_lockPause_87, logic_uScript_LockPause_disabledReason_87);
				if (logic_uScript_LockPause_uScript_LockPause_87.Out)
				{
					Relay_In_280();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at uScript_LockPause.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_True_90()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("b9a224ee-d6df-4c86-8aba-c03e82a92b68", "Set_Bool", Relay_True_90))
			{
				logic_uScriptAct_SetBool_uScriptAct_SetBool_90.True(out logic_uScriptAct_SetBool_Target_90);
				local_CraftingMenuOpened02_System_Boolean = logic_uScriptAct_SetBool_Target_90;
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at Set Bool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_False_90()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("b9a224ee-d6df-4c86-8aba-c03e82a92b68", "Set_Bool", Relay_False_90))
			{
				logic_uScriptAct_SetBool_uScriptAct_SetBool_90.False(out logic_uScriptAct_SetBool_Target_90);
				local_CraftingMenuOpened02_System_Boolean = logic_uScriptAct_SetBool_Target_90;
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at Set Bool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_92()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("97d3c3b8-f5da-40f4-8653-0fc91e88f0bc", "uScript_PointArrowAtVisible", Relay_In_92))
			{
				logic_uScript_PointArrowAtVisible_targetObject_92 = local_FabricatorBlock_TankBlock;
				logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_92.In(logic_uScript_PointArrowAtVisible_targetObject_92, logic_uScript_PointArrowAtVisible_timeToShowFor_92, logic_uScript_PointArrowAtVisible_offset_92);
				if (logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_92.Out)
				{
					Relay_In_269();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at uScript_PointArrowAtVisible.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_93()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("b4f436f8-f009-41a8-97b4-6425385a9f48", "uScript_LockPlayerInput", Relay_In_93))
			{
				logic_uScript_LockPlayerInput_uScript_LockPlayerInput_93.In(logic_uScript_LockPlayerInput_lockInput_93, logic_uScript_LockPlayerInput_includeCamera_93);
				if (logic_uScript_LockPlayerInput_uScript_LockPlayerInput_93.Out)
				{
					Relay_In_214();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at uScript_LockPlayerInput.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_94()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("c1087436-94f6-4e70-930e-71aa01cfd5c8", "Compare_Bool", Relay_In_94))
			{
				logic_uScriptCon_CompareBool_Bool_94 = local_CraftingInProgress02_System_Boolean;
				logic_uScriptCon_CompareBool_uScriptCon_CompareBool_94.In(logic_uScriptCon_CompareBool_Bool_94);
				bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_94.True;
				bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_94.False;
				if (num)
				{
					Relay_In_224();
				}
				if (flag)
				{
					Relay_In_106();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at Compare Bool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_97()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("02862c0f-1ae0-44fe-8d06-54eeb9ebcc89", "uScript_HideArrow", Relay_In_97))
			{
				logic_uScript_HideArrow_uScript_HideArrow_97.In();
				if (logic_uScript_HideArrow_uScript_HideArrow_97.Out)
				{
					Relay_In_271();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at uScript_HideArrow.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_True_101()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("485cb5ad-a26b-4b16-902d-d2c98b7c9b95", "Set_Bool", Relay_True_101))
			{
				logic_uScriptAct_SetBool_uScriptAct_SetBool_101.True(out logic_uScriptAct_SetBool_Target_101);
				local_CraftingInProgress02_System_Boolean = logic_uScriptAct_SetBool_Target_101;
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at Set Bool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_False_101()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("485cb5ad-a26b-4b16-902d-d2c98b7c9b95", "Set_Bool", Relay_False_101))
			{
				logic_uScriptAct_SetBool_uScriptAct_SetBool_101.False(out logic_uScriptAct_SetBool_Target_101);
				local_CraftingInProgress02_System_Boolean = logic_uScriptAct_SetBool_Target_101;
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at Set Bool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_102()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("55aaa9bd-fca1-4cef-a682-40c8b303365d", "uScript_LockTechInteraction", Relay_In_102))
			{
				logic_uScript_LockTechInteraction_tech_102 = local_CraftingBaseTech_Tank;
				logic_uScript_LockTechInteraction_uScript_LockTechInteraction_102.In(logic_uScript_LockTechInteraction_tech_102, logic_uScript_LockTechInteraction_excludedBlocks_102);
				if (logic_uScript_LockTechInteraction_uScript_LockTechInteraction_102.Out)
				{
					Relay_In_93();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at uScript_LockTechInteraction.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_103()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("dbbb8280-235c-4ce7-8d13-cdbc13b6ca09", "uScript_LockPlayerInput", Relay_In_103))
			{
				logic_uScript_LockPlayerInput_uScript_LockPlayerInput_103.In(logic_uScript_LockPlayerInput_lockInput_103, logic_uScript_LockPlayerInput_includeCamera_103);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at uScript_LockPlayerInput.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_104()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("4768a7d1-cfd6-44cb-a0f9-d8fa03cdb5c5", "uScript_LockTechInteraction", Relay_In_104))
			{
				logic_uScript_LockTechInteraction_tech_104 = local_CraftingBaseTech_Tank;
				logic_uScript_LockTechInteraction_uScript_LockTechInteraction_104.In(logic_uScript_LockTechInteraction_tech_104, logic_uScript_LockTechInteraction_excludedBlocks_104);
				if (logic_uScript_LockTechInteraction_uScript_LockTechInteraction_104.Out)
				{
					Relay_In_92();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at uScript_LockTechInteraction.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_106()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("e16e8cf7-f550-428a-97fa-0852e781567a", "Compare_Bool", Relay_In_106))
			{
				logic_uScriptCon_CompareBool_Bool_106 = local_CraftingMenuOpened02_System_Boolean;
				logic_uScriptCon_CompareBool_uScriptCon_CompareBool_106.In(logic_uScriptCon_CompareBool_Bool_106);
				bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_106.True;
				bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_106.False;
				if (num)
				{
					Relay_In_87();
				}
				if (flag)
				{
					Relay_In_174();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at Compare Bool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_True_107()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("756b68e7-f234-478f-8ba6-824dd5a7667b", "Set_Bool", Relay_True_107))
			{
				logic_uScriptAct_SetBool_uScriptAct_SetBool_107.True(out logic_uScriptAct_SetBool_Target_107);
				local_BlockCrafted02_System_Boolean = logic_uScriptAct_SetBool_Target_107;
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at Set Bool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_False_107()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("756b68e7-f234-478f-8ba6-824dd5a7667b", "Set_Bool", Relay_False_107))
			{
				logic_uScriptAct_SetBool_uScriptAct_SetBool_107.False(out logic_uScriptAct_SetBool_Target_107);
				local_BlockCrafted02_System_Boolean = logic_uScriptAct_SetBool_Target_107;
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at Set Bool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_108()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("f0e093fc-43e0-45ce-a2cf-4fa0fbc1a49b", "uScript_Wait", Relay_In_108))
			{
				logic_uScript_Wait_seconds_108 = timeRepeatResourcesReminder;
				logic_uScript_Wait_uScript_Wait_108.In(logic_uScript_Wait_seconds_108, logic_uScript_Wait_repeat_108);
				if (logic_uScript_Wait_uScript_Wait_108.Waited)
				{
					Relay_In_159();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at uScript_Wait.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_111()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("04caf315-26e9-436d-9c4f-431711a9a99f", "Compare_Bool", Relay_In_111))
			{
				logic_uScriptCon_CompareBool_Bool_111 = local_msgBaseFoundShown_System_Boolean;
				logic_uScriptCon_CompareBool_uScriptCon_CompareBool_111.In(logic_uScriptCon_CompareBool_Bool_111);
				bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_111.True;
				bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_111.False;
				if (num)
				{
					Relay_In_41();
				}
				if (flag)
				{
					Relay_In_156();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at Compare Bool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_True_113()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("22773a2e-cea4-47bd-8d85-dd2d16bb2a9e", "Set_Bool", Relay_True_113))
			{
				logic_uScriptAct_SetBool_uScriptAct_SetBool_113.True(out logic_uScriptAct_SetBool_Target_113);
				local_msgBaseFoundShown_System_Boolean = logic_uScriptAct_SetBool_Target_113;
				if (logic_uScriptAct_SetBool_uScriptAct_SetBool_113.Out)
				{
					Relay_In_41();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at Set Bool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_False_113()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("22773a2e-cea4-47bd-8d85-dd2d16bb2a9e", "Set_Bool", Relay_False_113))
			{
				logic_uScriptAct_SetBool_uScriptAct_SetBool_113.False(out logic_uScriptAct_SetBool_Target_113);
				local_msgBaseFoundShown_System_Boolean = logic_uScriptAct_SetBool_Target_113;
				if (logic_uScriptAct_SetBool_uScriptAct_SetBool_113.Out)
				{
					Relay_In_41();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at Set Bool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_114()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("98a43c8b-491c-43bf-a581-88372dfa6d5e", "Compare_Bool", Relay_In_114))
			{
				logic_uScriptCon_CompareBool_Bool_114 = local_msgRefineryExplanation02Shown_System_Boolean;
				logic_uScriptCon_CompareBool_uScriptCon_CompareBool_114.In(logic_uScriptCon_CompareBool_Bool_114);
				bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_114.True;
				bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_114.False;
				if (num)
				{
					Relay_In_179();
				}
				if (flag)
				{
					Relay_In_175();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at Compare Bool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_True_117()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("cb8ecc61-2cde-4f98-9def-7e49b383ad50", "Set_Bool", Relay_True_117))
			{
				logic_uScriptAct_SetBool_uScriptAct_SetBool_117.True(out logic_uScriptAct_SetBool_Target_117);
				local_msgRefineryExplanation02Shown_System_Boolean = logic_uScriptAct_SetBool_Target_117;
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at Set Bool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_False_117()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("cb8ecc61-2cde-4f98-9def-7e49b383ad50", "Set_Bool", Relay_False_117))
			{
				logic_uScriptAct_SetBool_uScriptAct_SetBool_117.False(out logic_uScriptAct_SetBool_Target_117);
				local_msgRefineryExplanation02Shown_System_Boolean = logic_uScriptAct_SetBool_Target_117;
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at Set Bool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_118()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("a1447f5b-bf89-468b-9ce9-6c20800e9d1e", "uScript_ClearOnScreenMessagesWithTag", Relay_In_118))
			{
				logic_uScript_ClearOnScreenMessagesWithTag_tag_118 = local_Msg_System_String;
				logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_118.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_118, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_118);
				if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_118.Out)
				{
					Relay_True_101();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at uScript_ClearOnScreenMessagesWithTag.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_120()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("f6bbcf8c-c819-4411-be70-f848c2ee64dc", "uScript_ClearOnScreenMessagesWithTag", Relay_In_120))
			{
				logic_uScript_ClearOnScreenMessagesWithTag_tag_120 = messageTag;
				logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_120.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_120, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_120);
				if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_120.Out)
				{
					Relay_True_47();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at uScript_ClearOnScreenMessagesWithTag.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_123()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("38c453c7-4fc6-4c23-b744-cc105ef49125", "Compare_Bool", Relay_In_123))
			{
				logic_uScriptCon_CompareBool_Bool_123 = local_BlockCrafted01_System_Boolean;
				logic_uScriptCon_CompareBool_uScriptCon_CompareBool_123.In(logic_uScriptCon_CompareBool_Bool_123);
				bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_123.True;
				bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_123.False;
				if (num)
				{
					Relay_In_135();
				}
				if (flag)
				{
					Relay_In_54();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at Compare Bool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_124()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("c599bc6e-6b81-4c9e-8931-2a02420ef58a", "Compare_Bool", Relay_In_124))
			{
				logic_uScriptCon_CompareBool_Bool_124 = local_BlockCrafted02_System_Boolean;
				logic_uScriptCon_CompareBool_uScriptCon_CompareBool_124.In(logic_uScriptCon_CompareBool_Bool_124);
				bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_124.True;
				bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_124.False;
				if (num)
				{
					Relay_In_114();
				}
				if (flag)
				{
					Relay_In_103();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at Compare Bool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Save_Out_126()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("bb14f45c-d208-4b01-bb97-153811a24b7a", "", Relay_Save_Out_126))
			{
				Relay_Save_583();
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Load_Out_126()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("bb14f45c-d208-4b01-bb97-153811a24b7a", "", Relay_Load_Out_126))
			{
				Relay_Load_583();
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Restart_Out_126()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("bb14f45c-d208-4b01-bb97-153811a24b7a", "", Relay_Restart_Out_126))
			{
				Relay_Set_False_583();
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Save_126()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("bb14f45c-d208-4b01-bb97-153811a24b7a", "", Relay_Save_126))
			{
				logic_SubGraph_SaveLoadBool_boolean_126 = local_BlockCrafted01_System_Boolean;
				logic_SubGraph_SaveLoadBool_boolAsVariable_126 = local_BlockCrafted01_System_Boolean;
				logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_126.Save(ref logic_SubGraph_SaveLoadBool_boolean_126, logic_SubGraph_SaveLoadBool_boolAsVariable_126, logic_SubGraph_SaveLoadBool_uniqueID_126);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Load_126()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("bb14f45c-d208-4b01-bb97-153811a24b7a", "", Relay_Load_126))
			{
				logic_SubGraph_SaveLoadBool_boolean_126 = local_BlockCrafted01_System_Boolean;
				logic_SubGraph_SaveLoadBool_boolAsVariable_126 = local_BlockCrafted01_System_Boolean;
				logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_126.Load(ref logic_SubGraph_SaveLoadBool_boolean_126, logic_SubGraph_SaveLoadBool_boolAsVariable_126, logic_SubGraph_SaveLoadBool_uniqueID_126);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Set_True_126()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("bb14f45c-d208-4b01-bb97-153811a24b7a", "", Relay_Set_True_126))
			{
				logic_SubGraph_SaveLoadBool_boolean_126 = local_BlockCrafted01_System_Boolean;
				logic_SubGraph_SaveLoadBool_boolAsVariable_126 = local_BlockCrafted01_System_Boolean;
				logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_126.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_126, logic_SubGraph_SaveLoadBool_boolAsVariable_126, logic_SubGraph_SaveLoadBool_uniqueID_126);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Set_False_126()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("bb14f45c-d208-4b01-bb97-153811a24b7a", "", Relay_Set_False_126))
			{
				logic_SubGraph_SaveLoadBool_boolean_126 = local_BlockCrafted01_System_Boolean;
				logic_SubGraph_SaveLoadBool_boolAsVariable_126 = local_BlockCrafted01_System_Boolean;
				logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_126.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_126, logic_SubGraph_SaveLoadBool_boolAsVariable_126, logic_SubGraph_SaveLoadBool_uniqueID_126);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Save_Out_129()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("b9a192ad-128e-40b9-9094-4b842e2e4603", "", Relay_Save_Out_129))
			{
				Relay_Save_130();
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Load_Out_129()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("b9a192ad-128e-40b9-9094-4b842e2e4603", "", Relay_Load_Out_129))
			{
				Relay_Load_130();
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Restart_Out_129()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("b9a192ad-128e-40b9-9094-4b842e2e4603", "", Relay_Restart_Out_129))
			{
				Relay_Set_False_130();
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Save_129()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("b9a192ad-128e-40b9-9094-4b842e2e4603", "", Relay_Save_129))
			{
				logic_SubGraph_SaveLoadBool_boolean_129 = local_BlockCrafted02_System_Boolean;
				logic_SubGraph_SaveLoadBool_boolAsVariable_129 = local_BlockCrafted02_System_Boolean;
				logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_129.Save(ref logic_SubGraph_SaveLoadBool_boolean_129, logic_SubGraph_SaveLoadBool_boolAsVariable_129, logic_SubGraph_SaveLoadBool_uniqueID_129);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Load_129()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("b9a192ad-128e-40b9-9094-4b842e2e4603", "", Relay_Load_129))
			{
				logic_SubGraph_SaveLoadBool_boolean_129 = local_BlockCrafted02_System_Boolean;
				logic_SubGraph_SaveLoadBool_boolAsVariable_129 = local_BlockCrafted02_System_Boolean;
				logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_129.Load(ref logic_SubGraph_SaveLoadBool_boolean_129, logic_SubGraph_SaveLoadBool_boolAsVariable_129, logic_SubGraph_SaveLoadBool_uniqueID_129);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Set_True_129()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("b9a192ad-128e-40b9-9094-4b842e2e4603", "", Relay_Set_True_129))
			{
				logic_SubGraph_SaveLoadBool_boolean_129 = local_BlockCrafted02_System_Boolean;
				logic_SubGraph_SaveLoadBool_boolAsVariable_129 = local_BlockCrafted02_System_Boolean;
				logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_129.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_129, logic_SubGraph_SaveLoadBool_boolAsVariable_129, logic_SubGraph_SaveLoadBool_uniqueID_129);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Set_False_129()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("b9a192ad-128e-40b9-9094-4b842e2e4603", "", Relay_Set_False_129))
			{
				logic_SubGraph_SaveLoadBool_boolean_129 = local_BlockCrafted02_System_Boolean;
				logic_SubGraph_SaveLoadBool_boolAsVariable_129 = local_BlockCrafted02_System_Boolean;
				logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_129.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_129, logic_SubGraph_SaveLoadBool_boolAsVariable_129, logic_SubGraph_SaveLoadBool_uniqueID_129);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Save_Out_130()
	{
		if (relayCallCount++ < 1000)
		{
			CheckDebugBreak("91b3d3a8-eaec-4a1f-8727-7281b0b0de44", "", Relay_Save_Out_130);
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Load_Out_130()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("91b3d3a8-eaec-4a1f-8727-7281b0b0de44", "", Relay_Load_Out_130))
			{
				Relay_In_141();
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Restart_Out_130()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("91b3d3a8-eaec-4a1f-8727-7281b0b0de44", "", Relay_Restart_Out_130))
			{
				Relay_False_133();
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Save_130()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("91b3d3a8-eaec-4a1f-8727-7281b0b0de44", "", Relay_Save_130))
			{
				logic_SubGraph_SaveLoadBool_boolean_130 = local_msgRefineryExplanation02Shown_System_Boolean;
				logic_SubGraph_SaveLoadBool_boolAsVariable_130 = local_msgRefineryExplanation02Shown_System_Boolean;
				logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_130.Save(ref logic_SubGraph_SaveLoadBool_boolean_130, logic_SubGraph_SaveLoadBool_boolAsVariable_130, logic_SubGraph_SaveLoadBool_uniqueID_130);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Load_130()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("91b3d3a8-eaec-4a1f-8727-7281b0b0de44", "", Relay_Load_130))
			{
				logic_SubGraph_SaveLoadBool_boolean_130 = local_msgRefineryExplanation02Shown_System_Boolean;
				logic_SubGraph_SaveLoadBool_boolAsVariable_130 = local_msgRefineryExplanation02Shown_System_Boolean;
				logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_130.Load(ref logic_SubGraph_SaveLoadBool_boolean_130, logic_SubGraph_SaveLoadBool_boolAsVariable_130, logic_SubGraph_SaveLoadBool_uniqueID_130);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Set_True_130()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("91b3d3a8-eaec-4a1f-8727-7281b0b0de44", "", Relay_Set_True_130))
			{
				logic_SubGraph_SaveLoadBool_boolean_130 = local_msgRefineryExplanation02Shown_System_Boolean;
				logic_SubGraph_SaveLoadBool_boolAsVariable_130 = local_msgRefineryExplanation02Shown_System_Boolean;
				logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_130.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_130, logic_SubGraph_SaveLoadBool_boolAsVariable_130, logic_SubGraph_SaveLoadBool_uniqueID_130);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Set_False_130()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("91b3d3a8-eaec-4a1f-8727-7281b0b0de44", "", Relay_Set_False_130))
			{
				logic_SubGraph_SaveLoadBool_boolean_130 = local_msgRefineryExplanation02Shown_System_Boolean;
				logic_SubGraph_SaveLoadBool_boolAsVariable_130 = local_msgRefineryExplanation02Shown_System_Boolean;
				logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_130.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_130, logic_SubGraph_SaveLoadBool_boolAsVariable_130, logic_SubGraph_SaveLoadBool_uniqueID_130);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_True_133()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("c8ccd614-6168-4c14-bfa9-7bf543dc3feb", "Set_Bool", Relay_True_133))
			{
				logic_uScriptAct_SetBool_uScriptAct_SetBool_133.True(out logic_uScriptAct_SetBool_Target_133);
				local_NearBase_System_Boolean = logic_uScriptAct_SetBool_Target_133;
				if (logic_uScriptAct_SetBool_uScriptAct_SetBool_133.Out)
				{
					Relay_False_217();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at Set Bool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_False_133()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("c8ccd614-6168-4c14-bfa9-7bf543dc3feb", "Set_Bool", Relay_False_133))
			{
				logic_uScriptAct_SetBool_uScriptAct_SetBool_133.False(out logic_uScriptAct_SetBool_Target_133);
				local_NearBase_System_Boolean = logic_uScriptAct_SetBool_Target_133;
				if (logic_uScriptAct_SetBool_uScriptAct_SetBool_133.Out)
				{
					Relay_False_217();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at Set Bool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_135()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("9e98e677-ecf2-4dc7-97f7-cbeaef5a253d", "uScript_LockTechInteraction", Relay_In_135))
			{
				logic_uScript_LockTechInteraction_tech_135 = local_CraftingBaseTech_Tank;
				logic_uScript_LockTechInteraction_uScript_LockTechInteraction_135.In(logic_uScript_LockTechInteraction_tech_135, logic_uScript_LockTechInteraction_excludedBlocks_135);
				if (logic_uScript_LockTechInteraction_uScript_LockTechInteraction_135.Out)
				{
					Relay_In_171();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at uScript_LockTechInteraction.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Output1_138()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("85a086db-6f55-4695-8b55-9db9046be32e", "Manual_Switch", Relay_Output1_138))
			{
				Relay_In_144();
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at Manual Switch.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Output2_138()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("85a086db-6f55-4695-8b55-9db9046be32e", "Manual_Switch", Relay_Output2_138))
			{
				Relay_In_80();
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at Manual Switch.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Output3_138()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("85a086db-6f55-4695-8b55-9db9046be32e", "Manual_Switch", Relay_Output3_138))
			{
				Relay_In_78();
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at Manual Switch.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Output4_138()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("85a086db-6f55-4695-8b55-9db9046be32e", "Manual_Switch", Relay_Output4_138))
			{
				Relay_In_62();
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at Manual Switch.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Output5_138()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("85a086db-6f55-4695-8b55-9db9046be32e", "Manual_Switch", Relay_Output5_138))
			{
				Relay_In_94();
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at Manual Switch.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Output6_138()
	{
		if (relayCallCount++ < 1000)
		{
			CheckDebugBreak("85a086db-6f55-4695-8b55-9db9046be32e", "Manual_Switch", Relay_Output6_138);
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at Manual Switch.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Output7_138()
	{
		if (relayCallCount++ < 1000)
		{
			CheckDebugBreak("85a086db-6f55-4695-8b55-9db9046be32e", "Manual_Switch", Relay_Output7_138);
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at Manual Switch.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Output8_138()
	{
		if (relayCallCount++ < 1000)
		{
			CheckDebugBreak("85a086db-6f55-4695-8b55-9db9046be32e", "Manual_Switch", Relay_Output8_138);
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at Manual Switch.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_138()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("85a086db-6f55-4695-8b55-9db9046be32e", "Manual_Switch", Relay_In_138))
			{
				logic_uScriptCon_ManualSwitch_CurrentOutput_138 = local_Stage_System_Int32;
				logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_138.In(logic_uScriptCon_ManualSwitch_CurrentOutput_138);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at Manual Switch.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Save_Out_139()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("4894e754-bb05-4dfb-af01-2de7f53ce06b", "", Relay_Save_Out_139))
			{
				Relay_Save_35();
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at SubGraph_SaveLoadInt.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Load_Out_139()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("4894e754-bb05-4dfb-af01-2de7f53ce06b", "", Relay_Load_Out_139))
			{
				Relay_Load_35();
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at SubGraph_SaveLoadInt.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Restart_Out_139()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("4894e754-bb05-4dfb-af01-2de7f53ce06b", "", Relay_Restart_Out_139))
			{
				Relay_Set_False_35();
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at SubGraph_SaveLoadInt.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Save_139()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("4894e754-bb05-4dfb-af01-2de7f53ce06b", "", Relay_Save_139))
			{
				logic_SubGraph_SaveLoadInt_integer_139 = local_Stage_System_Int32;
				logic_SubGraph_SaveLoadInt_intAsVariable_139 = local_Stage_System_Int32;
				logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_139.Save(logic_SubGraph_SaveLoadInt_restartValue_139, ref logic_SubGraph_SaveLoadInt_integer_139, logic_SubGraph_SaveLoadInt_intAsVariable_139, logic_SubGraph_SaveLoadInt_uniqueID_139);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at SubGraph_SaveLoadInt.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Load_139()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("4894e754-bb05-4dfb-af01-2de7f53ce06b", "", Relay_Load_139))
			{
				logic_SubGraph_SaveLoadInt_integer_139 = local_Stage_System_Int32;
				logic_SubGraph_SaveLoadInt_intAsVariable_139 = local_Stage_System_Int32;
				logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_139.Load(logic_SubGraph_SaveLoadInt_restartValue_139, ref logic_SubGraph_SaveLoadInt_integer_139, logic_SubGraph_SaveLoadInt_intAsVariable_139, logic_SubGraph_SaveLoadInt_uniqueID_139);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at SubGraph_SaveLoadInt.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Restart_139()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("4894e754-bb05-4dfb-af01-2de7f53ce06b", "", Relay_Restart_139))
			{
				logic_SubGraph_SaveLoadInt_integer_139 = local_Stage_System_Int32;
				logic_SubGraph_SaveLoadInt_intAsVariable_139 = local_Stage_System_Int32;
				logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_139.Restart(logic_SubGraph_SaveLoadInt_restartValue_139, ref logic_SubGraph_SaveLoadInt_integer_139, logic_SubGraph_SaveLoadInt_intAsVariable_139, logic_SubGraph_SaveLoadInt_uniqueID_139);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at SubGraph_SaveLoadInt.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Out_141()
	{
		if (relayCallCount++ < 1000)
		{
			CheckDebugBreak("f81c7b9a-e604-4bf4-b692-5eb55d3d94bb", "SubGraph_LoadObjectiveStates", Relay_Out_141);
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at SubGraph_LoadObjectiveStates.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_141()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("f81c7b9a-e604-4bf4-b692-5eb55d3d94bb", "SubGraph_LoadObjectiveStates", Relay_In_141))
			{
				logic_SubGraph_LoadObjectiveStates_currentObjective_141 = local_Stage_System_Int32;
				logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_141.In(logic_SubGraph_LoadObjectiveStates_currentObjective_141);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at SubGraph_LoadObjectiveStates.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Out_144()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("4213f9e3-511b-416a-a377-29283d4e385a", "SubGraph_CompleteObjectiveStage", Relay_Out_144))
			{
				Relay_In_276();
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at SubGraph_CompleteObjectiveStage.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_144()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("4213f9e3-511b-416a-a377-29283d4e385a", "SubGraph_CompleteObjectiveStage", Relay_In_144))
			{
				logic_SubGraph_CompleteObjectiveStage_objectiveStage_144 = local_Stage_System_Int32;
				logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_144.In(ref logic_SubGraph_CompleteObjectiveStage_objectiveStage_144, logic_SubGraph_CompleteObjectiveStage_isFinalObjective_144);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at SubGraph_CompleteObjectiveStage.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Out_146()
	{
		if (relayCallCount++ < 1000)
		{
			CheckDebugBreak("5bcf5474-e29e-4e3f-8bdb-874506572ac8", "SubGraph_CompleteObjectiveStage", Relay_Out_146);
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at SubGraph_CompleteObjectiveStage.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_146()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("5bcf5474-e29e-4e3f-8bdb-874506572ac8", "SubGraph_CompleteObjectiveStage", Relay_In_146))
			{
				logic_SubGraph_CompleteObjectiveStage_objectiveStage_146 = local_Stage_System_Int32;
				logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_146.In(ref logic_SubGraph_CompleteObjectiveStage_objectiveStage_146, logic_SubGraph_CompleteObjectiveStage_isFinalObjective_146);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at SubGraph_CompleteObjectiveStage.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Out_148()
	{
		if (relayCallCount++ < 1000)
		{
			CheckDebugBreak("0f209df5-ce0f-4f27-9cf5-7aff20e82f0b", "SubGraph_CompleteObjectiveStage", Relay_Out_148);
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at SubGraph_CompleteObjectiveStage.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_148()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("0f209df5-ce0f-4f27-9cf5-7aff20e82f0b", "SubGraph_CompleteObjectiveStage", Relay_In_148))
			{
				logic_SubGraph_CompleteObjectiveStage_objectiveStage_148 = local_Stage_System_Int32;
				logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_148.In(ref logic_SubGraph_CompleteObjectiveStage_objectiveStage_148, logic_SubGraph_CompleteObjectiveStage_isFinalObjective_148);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at SubGraph_CompleteObjectiveStage.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Out_150()
	{
		if (relayCallCount++ < 1000)
		{
			CheckDebugBreak("96dcde76-37dc-4494-afc3-532213557c6c", "SubGraph_CompleteObjectiveStage", Relay_Out_150);
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at SubGraph_CompleteObjectiveStage.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_150()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("96dcde76-37dc-4494-afc3-532213557c6c", "SubGraph_CompleteObjectiveStage", Relay_In_150))
			{
				logic_SubGraph_CompleteObjectiveStage_objectiveStage_150 = local_Stage_System_Int32;
				logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_150.In(ref logic_SubGraph_CompleteObjectiveStage_objectiveStage_150, logic_SubGraph_CompleteObjectiveStage_isFinalObjective_150);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at SubGraph_CompleteObjectiveStage.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_152()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("4c3e1980-116e-4b5f-83c8-ed2505f5f2db", "uScript_AddMessage", Relay_In_152))
			{
				logic_uScript_AddMessage_messageData_152 = msg01Intro;
				logic_uScript_AddMessage_speaker_152 = messageSpeaker;
				logic_uScript_AddMessage_Return_152 = logic_uScript_AddMessage_uScript_AddMessage_152.In(logic_uScript_AddMessage_messageData_152, logic_uScript_AddMessage_speaker_152);
				if (logic_uScript_AddMessage_uScript_AddMessage_152.Out)
				{
					Relay_In_2();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at uScript_AddMessage.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_156()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("ba805773-b1f0-4ce5-928c-6e90cef4301a", "uScript_AddMessage", Relay_In_156))
			{
				logic_uScript_AddMessage_messageData_156 = msg02BaseFound;
				logic_uScript_AddMessage_speaker_156 = messageSpeaker;
				logic_uScript_AddMessage_Return_156 = logic_uScript_AddMessage_uScript_AddMessage_156.In(logic_uScript_AddMessage_messageData_156, logic_uScript_AddMessage_speaker_156);
				if (logic_uScript_AddMessage_uScript_AddMessage_156.Shown)
				{
					Relay_True_113();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at uScript_AddMessage.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_159()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("ecd9a3a9-9f93-43e2-ac65-5c0617b52bba", "uScript_AddMessage", Relay_In_159))
			{
				logic_uScript_AddMessage_messageData_159 = msg03ResourcesReminder;
				logic_uScript_AddMessage_speaker_159 = messageSpeaker;
				logic_uScript_AddMessage_Return_159 = logic_uScript_AddMessage_uScript_AddMessage_159.In(logic_uScript_AddMessage_messageData_159, logic_uScript_AddMessage_speaker_159);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at uScript_AddMessage.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_162()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("b2850540-3fc3-442c-842c-eb8b472f3310", "uScript_AddMessage", Relay_In_162))
			{
				logic_uScript_AddMessage_messageData_162 = msg04ResourcesInSilo;
				logic_uScript_AddMessage_speaker_162 = messageSpeaker;
				logic_uScript_AddMessage_Return_162 = logic_uScript_AddMessage_uScript_AddMessage_162.In(logic_uScript_AddMessage_messageData_162, logic_uScript_AddMessage_speaker_162);
				if (logic_uScript_AddMessage_uScript_AddMessage_162.Shown)
				{
					Relay_True_33();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at uScript_AddMessage.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_165()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("e2ca785c-20ff-456c-807c-d827c8c5f728", "uScript_AddMessage", Relay_In_165))
			{
				logic_uScript_AddMessage_messageData_165 = msg05AttachRefinery;
				logic_uScript_AddMessage_speaker_165 = messageSpeaker;
				logic_uScript_AddMessage_Return_165 = logic_uScript_AddMessage_uScript_AddMessage_165.In(logic_uScript_AddMessage_messageData_165, logic_uScript_AddMessage_speaker_165);
				if (logic_uScript_AddMessage_uScript_AddMessage_165.Out)
				{
					Relay_In_28();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at uScript_AddMessage.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_168()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("7554c10e-e088-47d2-aefd-a4060fa390b5", "uScript_AddMessage", Relay_In_168))
			{
				logic_uScript_AddMessage_messageData_168 = msg06CraftBlock01;
				logic_uScript_AddMessage_speaker_168 = messageSpeaker;
				logic_uScript_AddMessage_Return_168 = logic_uScript_AddMessage_uScript_AddMessage_168.In(logic_uScript_AddMessage_messageData_168, logic_uScript_AddMessage_speaker_168);
				if (logic_uScript_AddMessage_uScript_AddMessage_168.Out)
				{
					Relay_In_58();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at uScript_AddMessage.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_171()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("ee62faa8-823b-4e4f-9742-d2ef6c098912", "uScript_AddMessage", Relay_In_171))
			{
				logic_uScript_AddMessage_messageData_171 = msg07RefineryExplanation01;
				logic_uScript_AddMessage_speaker_171 = messageSpeaker;
				logic_uScript_AddMessage_Return_171 = logic_uScript_AddMessage_uScript_AddMessage_171.In(logic_uScript_AddMessage_messageData_171, logic_uScript_AddMessage_speaker_171);
				if (logic_uScript_AddMessage_uScript_AddMessage_171.Shown)
				{
					Relay_In_251();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at uScript_AddMessage.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_174()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("2698412f-f641-4d21-89da-af0dcd61fa7d", "uScript_AddMessage", Relay_In_174))
			{
				logic_uScript_AddMessage_messageData_174 = msg08CraftBlock02;
				logic_uScript_AddMessage_speaker_174 = messageSpeaker;
				logic_uScript_AddMessage_Return_174 = logic_uScript_AddMessage_uScript_AddMessage_174.In(logic_uScript_AddMessage_messageData_174, logic_uScript_AddMessage_speaker_174);
				if (logic_uScript_AddMessage_uScript_AddMessage_174.Out)
				{
					Relay_In_104();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at uScript_AddMessage.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_175()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("cbf54c24-c790-4efb-9169-cd243e4dfd87", "uScript_AddMessage", Relay_In_175))
			{
				logic_uScript_AddMessage_messageData_175 = msg09RefineryExplanation02;
				logic_uScript_AddMessage_speaker_175 = messageSpeaker;
				logic_uScript_AddMessage_Return_175 = logic_uScript_AddMessage_uScript_AddMessage_175.In(logic_uScript_AddMessage_messageData_175, logic_uScript_AddMessage_speaker_175);
				if (logic_uScript_AddMessage_uScript_AddMessage_175.Shown)
				{
					Relay_True_117();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at uScript_AddMessage.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_179()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("64336f7c-f586-423c-88f5-d3ec437d7b80", "uScript_AddMessage", Relay_In_179))
			{
				logic_uScript_AddMessage_messageData_179 = msg10Complete;
				logic_uScript_AddMessage_speaker_179 = messageSpeaker;
				logic_uScript_AddMessage_Return_179 = logic_uScript_AddMessage_uScript_AddMessage_179.In(logic_uScript_AddMessage_messageData_179, logic_uScript_AddMessage_speaker_179);
				if (logic_uScript_AddMessage_uScript_AddMessage_179.Shown)
				{
					Relay_In_252();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at uScript_AddMessage.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_184()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("79a24b62-2564-4664-bd14-5f86b669c364", "Compare_Bool", Relay_In_184))
			{
				logic_uScriptCon_CompareBool_Bool_184 = local_NearBase_System_Boolean;
				logic_uScriptCon_CompareBool_uScriptCon_CompareBool_184.In(logic_uScriptCon_CompareBool_Bool_184);
				if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_184.True)
				{
					Relay_In_189();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at Compare Bool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_True_185()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("eb242933-96e1-4c5e-b0bf-e2fa8094ed9a", "Set_Bool", Relay_True_185))
			{
				logic_uScriptAct_SetBool_uScriptAct_SetBool_185.True(out logic_uScriptAct_SetBool_Target_185);
				local_NearBase_System_Boolean = logic_uScriptAct_SetBool_Target_185;
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at Set Bool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_False_185()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("eb242933-96e1-4c5e-b0bf-e2fa8094ed9a", "Set_Bool", Relay_False_185))
			{
				logic_uScriptAct_SetBool_uScriptAct_SetBool_185.False(out logic_uScriptAct_SetBool_Target_185);
				local_NearBase_System_Boolean = logic_uScriptAct_SetBool_Target_185;
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at Set Bool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_189()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("670a4d29-1206-414e-a260-d2f2a64425d3", "uScript_AddMessage", Relay_In_189))
			{
				logic_uScript_AddMessage_messageData_189 = msgLeavingMissionArea;
				logic_uScript_AddMessage_speaker_189 = messageSpeaker;
				logic_uScript_AddMessage_Return_189 = logic_uScript_AddMessage_uScript_AddMessage_189.In(logic_uScript_AddMessage_messageData_189, logic_uScript_AddMessage_speaker_189);
				if (logic_uScript_AddMessage_uScript_AddMessage_189.Out)
				{
					Relay_False_185();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at uScript_AddMessage.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Out_195()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("59afaa76-c620-4c90-81ee-ef0a4649185f", "SubGraph_Crafting_Tutorial_Finish", Relay_Out_195))
			{
				Relay_In_277();
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at SubGraph_Crafting_Tutorial_Finish.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_195()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("59afaa76-c620-4c90-81ee-ef0a4649185f", "SubGraph_Crafting_Tutorial_Finish", Relay_In_195))
			{
				logic_SubGraph_Crafting_Tutorial_Finish_craftingBaseTech_195 = local_CraftingBaseTech_Tank;
				logic_SubGraph_Crafting_Tutorial_Finish_NPCTech_195 = local_NPCTech_Tank;
				logic_SubGraph_Crafting_Tutorial_Finish_NPCFlyAwayAI_195 = NPCFlyAwayAI;
				logic_SubGraph_Crafting_Tutorial_Finish_NPCDespawnParticleEffect_195 = NPCDespawnParticleEffect;
				logic_SubGraph_Crafting_Tutorial_Finish_SubGraph_Crafting_Tutorial_Finish_195.In(logic_SubGraph_Crafting_Tutorial_Finish_craftingBaseTech_195, logic_SubGraph_Crafting_Tutorial_Finish_NPCTech_195, logic_SubGraph_Crafting_Tutorial_Finish_NPCFlyAwayAI_195, logic_SubGraph_Crafting_Tutorial_Finish_NPCDespawnParticleEffect_195);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at SubGraph_Crafting_Tutorial_Finish.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Out_197()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("377c412e-a940-4c23-bdc3-ece877bb82e0", "SubGraph_Crafting_Tutorial_Init", Relay_Out_197))
			{
				Relay_In_18();
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at SubGraph_Crafting_Tutorial_Init.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_197()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("377c412e-a940-4c23-bdc3-ece877bb82e0", "SubGraph_Crafting_Tutorial_Init", Relay_In_197))
			{
				int num = 0;
				Array array = baseSpawnData;
				if (logic_SubGraph_Crafting_Tutorial_Init_baseSpawnData_197.Length != num + array.Length)
				{
					Array.Resize(ref logic_SubGraph_Crafting_Tutorial_Init_baseSpawnData_197, num + array.Length);
				}
				Array.Copy(array, 0, logic_SubGraph_Crafting_Tutorial_Init_baseSpawnData_197, num, array.Length);
				num += array.Length;
				int num2 = 0;
				Array array2 = blockSpawnData;
				if (logic_SubGraph_Crafting_Tutorial_Init_blockSpawnData_197.Length != num2 + array2.Length)
				{
					Array.Resize(ref logic_SubGraph_Crafting_Tutorial_Init_blockSpawnData_197, num2 + array2.Length);
				}
				Array.Copy(array2, 0, logic_SubGraph_Crafting_Tutorial_Init_blockSpawnData_197, num2, array2.Length);
				num2 += array2.Length;
				int num3 = 0;
				Array nPCSpawnData = NPCSpawnData;
				if (logic_SubGraph_Crafting_Tutorial_Init_NPCSpawnData_197.Length != num3 + nPCSpawnData.Length)
				{
					Array.Resize(ref logic_SubGraph_Crafting_Tutorial_Init_NPCSpawnData_197, num3 + nPCSpawnData.Length);
				}
				Array.Copy(nPCSpawnData, 0, logic_SubGraph_Crafting_Tutorial_Init_NPCSpawnData_197, num3, nPCSpawnData.Length);
				num3 += nPCSpawnData.Length;
				logic_SubGraph_Crafting_Tutorial_Init_completedBasePreset_197 = completedBasePreset;
				logic_SubGraph_Crafting_Tutorial_Init_basePosition_197 = basePosition;
				logic_SubGraph_Crafting_Tutorial_Init_clearSceneryRadius_197 = clearSceneryRadius;
				logic_SubGraph_Crafting_Tutorial_Init_CraftingBaseTech_197 = local_CraftingBaseTech_Tank;
				logic_SubGraph_Crafting_Tutorial_Init_NPCTech_197 = local_NPCTech_Tank;
				logic_SubGraph_Crafting_Tutorial_Init_SubGraph_Crafting_Tutorial_Init_197.In(logic_SubGraph_Crafting_Tutorial_Init_baseSpawnData_197, logic_SubGraph_Crafting_Tutorial_Init_blockSpawnData_197, logic_SubGraph_Crafting_Tutorial_Init_NPCSpawnData_197, logic_SubGraph_Crafting_Tutorial_Init_completedBasePreset_197, logic_SubGraph_Crafting_Tutorial_Init_useNPCAsTutorialTech_197, logic_SubGraph_Crafting_Tutorial_Init_spawnBase_197, logic_SubGraph_Crafting_Tutorial_Init_basePosition_197, logic_SubGraph_Crafting_Tutorial_Init_clearSceneryRadius_197, ref logic_SubGraph_Crafting_Tutorial_Init_CraftingBaseTech_197, ref logic_SubGraph_Crafting_Tutorial_Init_NPCTech_197);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at SubGraph_Crafting_Tutorial_Init.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_207()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("ab9e296e-5803-4329-b989-8c656ceaeeb5", "uScript_SetEncounterTarget", Relay_In_207))
			{
				logic_uScript_SetEncounterTarget_owner_207 = owner_Connection_206;
				logic_uScript_SetEncounterTarget_visibleObject_207 = local_NPCTech_Tank;
				logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_207.In(logic_uScript_SetEncounterTarget_owner_207, logic_uScript_SetEncounterTarget_visibleObject_207);
				if (logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_207.Out)
				{
					Relay_In_21();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at uScript_SetEncounterTarget.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_208()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("5ef44c57-a28d-4e9b-aa82-d67dd4134b6a", "uScript_CraftingUIHighlightItem", Relay_In_208))
			{
				logic_uScript_CraftingUIHighlightItem_itemToHighlight_208 = blockTypeToHighlight01;
				logic_uScript_CraftingUIHighlightItem_uScript_CraftingUIHighlightItem_208.In(logic_uScript_CraftingUIHighlightItem_targetMenuType_208, logic_uScript_CraftingUIHighlightItem_itemToHighlight_208);
				if (logic_uScript_CraftingUIHighlightItem_uScript_CraftingUIHighlightItem_208.Selected)
				{
					Relay_In_211();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at uScript_CraftingUIHighlightItem.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_AllCategory_209()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("2ed5bcdb-e23e-4245-94ce-3da6fb6c4d3c", "uScript_CraftingUIHighlightBlockCategory", Relay_AllCategory_209))
			{
				logic_uScript_CraftingUIHighlightBlockCategory_blockCategory_209 = blockTypeToCraftCategory;
				logic_uScript_CraftingUIHighlightBlockCategory_uScript_CraftingUIHighlightBlockCategory_209.AllCategory(logic_uScript_CraftingUIHighlightBlockCategory_blockCategory_209);
				if (logic_uScript_CraftingUIHighlightBlockCategory_uScript_CraftingUIHighlightBlockCategory_209.Selected)
				{
					Relay_In_208();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at uScript_CraftingUIHighlightBlockCategory.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Category_209()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("2ed5bcdb-e23e-4245-94ce-3da6fb6c4d3c", "uScript_CraftingUIHighlightBlockCategory", Relay_Category_209))
			{
				logic_uScript_CraftingUIHighlightBlockCategory_blockCategory_209 = blockTypeToCraftCategory;
				logic_uScript_CraftingUIHighlightBlockCategory_uScript_CraftingUIHighlightBlockCategory_209.Category(logic_uScript_CraftingUIHighlightBlockCategory_blockCategory_209);
				if (logic_uScript_CraftingUIHighlightBlockCategory_uScript_CraftingUIHighlightBlockCategory_209.Selected)
				{
					Relay_In_208();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at uScript_CraftingUIHighlightBlockCategory.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_211()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("cf026571-5c52-4d2c-9717-9cf21f2c8740", "uScript_CraftingUIHighlightCraftButton", Relay_In_211))
			{
				logic_uScript_CraftingUIHighlightCraftButton_uScript_CraftingUIHighlightCraftButton_211.In(logic_uScript_CraftingUIHighlightCraftButton_targetMenuType_211);
				if (logic_uScript_CraftingUIHighlightCraftButton_uScript_CraftingUIHighlightCraftButton_211.Selected)
				{
					Relay_EnableAutoCloseUI_228();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at uScript_CraftingUIHighlightCraftButton.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_213()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("987b7a74-2b17-485c-b1fd-a8f3c2dce289", "uScript_CraftingUIHighlightCraftButton", Relay_In_213))
			{
				logic_uScript_CraftingUIHighlightCraftButton_uScript_CraftingUIHighlightCraftButton_213.In(logic_uScript_CraftingUIHighlightCraftButton_targetMenuType_213);
				if (logic_uScript_CraftingUIHighlightCraftButton_uScript_CraftingUIHighlightCraftButton_213.Selected)
				{
					Relay_EnableAutoCloseUI_231();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at uScript_CraftingUIHighlightCraftButton.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_214()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("101c7735-fa1f-4fbd-96c3-b3f5a33786f8", "uScript_CraftingUIHighlightItem", Relay_In_214))
			{
				logic_uScript_CraftingUIHighlightItem_itemToHighlight_214 = blockTypeToHighlight02;
				logic_uScript_CraftingUIHighlightItem_uScript_CraftingUIHighlightItem_214.In(logic_uScript_CraftingUIHighlightItem_targetMenuType_214, logic_uScript_CraftingUIHighlightItem_itemToHighlight_214);
				if (logic_uScript_CraftingUIHighlightItem_uScript_CraftingUIHighlightItem_214.Selected)
				{
					Relay_In_213();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at uScript_CraftingUIHighlightItem.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_True_217()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("84f5cefa-dcb6-47c4-8a69-e5c7d3fe1600", "Set_Bool", Relay_True_217))
			{
				logic_uScriptAct_SetBool_uScriptAct_SetBool_217.True(out logic_uScriptAct_SetBool_Target_217);
				local_CraftingMenuOpened01_System_Boolean = logic_uScriptAct_SetBool_Target_217;
				if (logic_uScriptAct_SetBool_uScriptAct_SetBool_217.Out)
				{
					Relay_False_218();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at Set Bool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_False_217()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("84f5cefa-dcb6-47c4-8a69-e5c7d3fe1600", "Set_Bool", Relay_False_217))
			{
				logic_uScriptAct_SetBool_uScriptAct_SetBool_217.False(out logic_uScriptAct_SetBool_Target_217);
				local_CraftingMenuOpened01_System_Boolean = logic_uScriptAct_SetBool_Target_217;
				if (logic_uScriptAct_SetBool_uScriptAct_SetBool_217.Out)
				{
					Relay_False_218();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at Set Bool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_True_218()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("123cfd83-297c-4353-97d7-71b3064359b3", "Set_Bool", Relay_True_218))
			{
				logic_uScriptAct_SetBool_uScriptAct_SetBool_218.True(out logic_uScriptAct_SetBool_Target_218);
				local_CraftingInProgress01_System_Boolean = logic_uScriptAct_SetBool_Target_218;
				if (logic_uScriptAct_SetBool_uScriptAct_SetBool_218.Out)
				{
					Relay_False_222();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at Set Bool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_False_218()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("123cfd83-297c-4353-97d7-71b3064359b3", "Set_Bool", Relay_False_218))
			{
				logic_uScriptAct_SetBool_uScriptAct_SetBool_218.False(out logic_uScriptAct_SetBool_Target_218);
				local_CraftingInProgress01_System_Boolean = logic_uScriptAct_SetBool_Target_218;
				if (logic_uScriptAct_SetBool_uScriptAct_SetBool_218.Out)
				{
					Relay_False_222();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at Set Bool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_True_220()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("135de7c5-bcbd-40b8-bafd-1c3a1afe65c4", "Set_Bool", Relay_True_220))
			{
				logic_uScriptAct_SetBool_uScriptAct_SetBool_220.True(out logic_uScriptAct_SetBool_Target_220);
				local_CraftingInProgress02_System_Boolean = logic_uScriptAct_SetBool_Target_220;
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at Set Bool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_False_220()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("135de7c5-bcbd-40b8-bafd-1c3a1afe65c4", "Set_Bool", Relay_False_220))
			{
				logic_uScriptAct_SetBool_uScriptAct_SetBool_220.False(out logic_uScriptAct_SetBool_Target_220);
				local_CraftingInProgress02_System_Boolean = logic_uScriptAct_SetBool_Target_220;
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at Set Bool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_True_222()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("6e312412-342e-4b11-960f-a4578ce51f57", "Set_Bool", Relay_True_222))
			{
				logic_uScriptAct_SetBool_uScriptAct_SetBool_222.True(out logic_uScriptAct_SetBool_Target_222);
				local_CraftingMenuOpened02_System_Boolean = logic_uScriptAct_SetBool_Target_222;
				if (logic_uScriptAct_SetBool_uScriptAct_SetBool_222.Out)
				{
					Relay_False_220();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at Set Bool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_False_222()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("6e312412-342e-4b11-960f-a4578ce51f57", "Set_Bool", Relay_False_222))
			{
				logic_uScriptAct_SetBool_uScriptAct_SetBool_222.False(out logic_uScriptAct_SetBool_Target_222);
				local_CraftingMenuOpened02_System_Boolean = logic_uScriptAct_SetBool_Target_222;
				if (logic_uScriptAct_SetBool_uScriptAct_SetBool_222.Out)
				{
					Relay_False_220();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at Set Bool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_224()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("5aa8acd8-973b-4731-b14d-651c8716ef45", "uScript_LockTechInteraction", Relay_In_224))
			{
				logic_uScript_LockTechInteraction_tech_224 = local_CraftingBaseTech_Tank;
				logic_uScript_LockTechInteraction_uScript_LockTechInteraction_224.In(logic_uScript_LockTechInteraction_tech_224, logic_uScript_LockTechInteraction_excludedBlocks_224);
				if (logic_uScript_LockTechInteraction_uScript_LockTechInteraction_224.Out)
				{
					Relay_In_124();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at uScript_LockTechInteraction.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_DisableAutoCloseUI_225()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("958f12e4-518c-4509-862d-c8ece5b86fac", "uScript_DisableClosingCraftingUIWhenTooFarAway", Relay_DisableAutoCloseUI_225))
			{
				logic_uScript_DisableClosingCraftingUIWhenTooFarAway_craftingBlock_225 = local_FabricatorBlock_TankBlock;
				logic_uScript_DisableClosingCraftingUIWhenTooFarAway_uScript_DisableClosingCraftingUIWhenTooFarAway_225.DisableAutoCloseUI(logic_uScript_DisableClosingCraftingUIWhenTooFarAway_craftingBlock_225);
				if (logic_uScript_DisableClosingCraftingUIWhenTooFarAway_uScript_DisableClosingCraftingUIWhenTooFarAway_225.Out)
				{
					Relay_In_254();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at uScript_DisableClosingCraftingUIWhenTooFarAway.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_EnableAutoCloseUI_225()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("958f12e4-518c-4509-862d-c8ece5b86fac", "uScript_DisableClosingCraftingUIWhenTooFarAway", Relay_EnableAutoCloseUI_225))
			{
				logic_uScript_DisableClosingCraftingUIWhenTooFarAway_craftingBlock_225 = local_FabricatorBlock_TankBlock;
				logic_uScript_DisableClosingCraftingUIWhenTooFarAway_uScript_DisableClosingCraftingUIWhenTooFarAway_225.EnableAutoCloseUI(logic_uScript_DisableClosingCraftingUIWhenTooFarAway_craftingBlock_225);
				if (logic_uScript_DisableClosingCraftingUIWhenTooFarAway_uScript_DisableClosingCraftingUIWhenTooFarAway_225.Out)
				{
					Relay_In_254();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at uScript_DisableClosingCraftingUIWhenTooFarAway.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_DisableAutoCloseUI_228()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("5947d277-709b-44e8-8e84-a20416ff58d1", "uScript_DisableClosingCraftingUIWhenTooFarAway", Relay_DisableAutoCloseUI_228))
			{
				logic_uScript_DisableClosingCraftingUIWhenTooFarAway_craftingBlock_228 = local_FabricatorBlock_TankBlock;
				logic_uScript_DisableClosingCraftingUIWhenTooFarAway_uScript_DisableClosingCraftingUIWhenTooFarAway_228.DisableAutoCloseUI(logic_uScript_DisableClosingCraftingUIWhenTooFarAway_craftingBlock_228);
				if (logic_uScript_DisableClosingCraftingUIWhenTooFarAway_uScript_DisableClosingCraftingUIWhenTooFarAway_228.Out)
				{
					Relay_In_120();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at uScript_DisableClosingCraftingUIWhenTooFarAway.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_EnableAutoCloseUI_228()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("5947d277-709b-44e8-8e84-a20416ff58d1", "uScript_DisableClosingCraftingUIWhenTooFarAway", Relay_EnableAutoCloseUI_228))
			{
				logic_uScript_DisableClosingCraftingUIWhenTooFarAway_craftingBlock_228 = local_FabricatorBlock_TankBlock;
				logic_uScript_DisableClosingCraftingUIWhenTooFarAway_uScript_DisableClosingCraftingUIWhenTooFarAway_228.EnableAutoCloseUI(logic_uScript_DisableClosingCraftingUIWhenTooFarAway_craftingBlock_228);
				if (logic_uScript_DisableClosingCraftingUIWhenTooFarAway_uScript_DisableClosingCraftingUIWhenTooFarAway_228.Out)
				{
					Relay_In_120();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at uScript_DisableClosingCraftingUIWhenTooFarAway.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_DisableAutoCloseUI_230()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("e7578625-911d-48b9-aff8-ceea1a338251", "uScript_DisableClosingCraftingUIWhenTooFarAway", Relay_DisableAutoCloseUI_230))
			{
				logic_uScript_DisableClosingCraftingUIWhenTooFarAway_craftingBlock_230 = local_FabricatorBlock_TankBlock;
				logic_uScript_DisableClosingCraftingUIWhenTooFarAway_uScript_DisableClosingCraftingUIWhenTooFarAway_230.DisableAutoCloseUI(logic_uScript_DisableClosingCraftingUIWhenTooFarAway_craftingBlock_230);
				if (logic_uScript_DisableClosingCraftingUIWhenTooFarAway_uScript_DisableClosingCraftingUIWhenTooFarAway_230.Out)
				{
					Relay_In_257();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at uScript_DisableClosingCraftingUIWhenTooFarAway.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_EnableAutoCloseUI_230()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("e7578625-911d-48b9-aff8-ceea1a338251", "uScript_DisableClosingCraftingUIWhenTooFarAway", Relay_EnableAutoCloseUI_230))
			{
				logic_uScript_DisableClosingCraftingUIWhenTooFarAway_craftingBlock_230 = local_FabricatorBlock_TankBlock;
				logic_uScript_DisableClosingCraftingUIWhenTooFarAway_uScript_DisableClosingCraftingUIWhenTooFarAway_230.EnableAutoCloseUI(logic_uScript_DisableClosingCraftingUIWhenTooFarAway_craftingBlock_230);
				if (logic_uScript_DisableClosingCraftingUIWhenTooFarAway_uScript_DisableClosingCraftingUIWhenTooFarAway_230.Out)
				{
					Relay_In_257();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at uScript_DisableClosingCraftingUIWhenTooFarAway.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_DisableAutoCloseUI_231()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("c45e44d2-35e5-47b1-934e-4c2b97dc7681", "uScript_DisableClosingCraftingUIWhenTooFarAway", Relay_DisableAutoCloseUI_231))
			{
				logic_uScript_DisableClosingCraftingUIWhenTooFarAway_craftingBlock_231 = local_FabricatorBlock_TankBlock;
				logic_uScript_DisableClosingCraftingUIWhenTooFarAway_uScript_DisableClosingCraftingUIWhenTooFarAway_231.DisableAutoCloseUI(logic_uScript_DisableClosingCraftingUIWhenTooFarAway_craftingBlock_231);
				if (logic_uScript_DisableClosingCraftingUIWhenTooFarAway_uScript_DisableClosingCraftingUIWhenTooFarAway_231.Out)
				{
					Relay_In_118();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at uScript_DisableClosingCraftingUIWhenTooFarAway.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_EnableAutoCloseUI_231()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("c45e44d2-35e5-47b1-934e-4c2b97dc7681", "uScript_DisableClosingCraftingUIWhenTooFarAway", Relay_EnableAutoCloseUI_231))
			{
				logic_uScript_DisableClosingCraftingUIWhenTooFarAway_craftingBlock_231 = local_FabricatorBlock_TankBlock;
				logic_uScript_DisableClosingCraftingUIWhenTooFarAway_uScript_DisableClosingCraftingUIWhenTooFarAway_231.EnableAutoCloseUI(logic_uScript_DisableClosingCraftingUIWhenTooFarAway_craftingBlock_231);
				if (logic_uScript_DisableClosingCraftingUIWhenTooFarAway_uScript_DisableClosingCraftingUIWhenTooFarAway_231.Out)
				{
					Relay_In_118();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at uScript_DisableClosingCraftingUIWhenTooFarAway.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_DisableAutoCloseUI_233()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("7f2855a0-a7ae-4d24-9a85-51a6533af7e9", "uScript_DisableClosingCraftingUIWhenTooFarAway", Relay_DisableAutoCloseUI_233))
			{
				logic_uScript_DisableClosingCraftingUIWhenTooFarAway_craftingBlock_233 = local_FabricatorBlock_TankBlock;
				logic_uScript_DisableClosingCraftingUIWhenTooFarAway_uScript_DisableClosingCraftingUIWhenTooFarAway_233.DisableAutoCloseUI(logic_uScript_DisableClosingCraftingUIWhenTooFarAway_craftingBlock_233);
				if (logic_uScript_DisableClosingCraftingUIWhenTooFarAway_uScript_DisableClosingCraftingUIWhenTooFarAway_233.Out)
				{
					Relay_In_184();
					Relay_In_237();
					Relay_In_248();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at uScript_DisableClosingCraftingUIWhenTooFarAway.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_EnableAutoCloseUI_233()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("7f2855a0-a7ae-4d24-9a85-51a6533af7e9", "uScript_DisableClosingCraftingUIWhenTooFarAway", Relay_EnableAutoCloseUI_233))
			{
				logic_uScript_DisableClosingCraftingUIWhenTooFarAway_craftingBlock_233 = local_FabricatorBlock_TankBlock;
				logic_uScript_DisableClosingCraftingUIWhenTooFarAway_uScript_DisableClosingCraftingUIWhenTooFarAway_233.EnableAutoCloseUI(logic_uScript_DisableClosingCraftingUIWhenTooFarAway_craftingBlock_233);
				if (logic_uScript_DisableClosingCraftingUIWhenTooFarAway_uScript_DisableClosingCraftingUIWhenTooFarAway_233.Out)
				{
					Relay_In_184();
					Relay_In_237();
					Relay_In_248();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at uScript_DisableClosingCraftingUIWhenTooFarAway.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_235()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("afdd6f9f-f7ee-4d8f-bdd5-e5ecfa521207", "uScript_LockPlayerInput", Relay_In_235))
			{
				logic_uScript_LockPlayerInput_uScript_LockPlayerInput_235.In(logic_uScript_LockPlayerInput_lockInput_235, logic_uScript_LockPlayerInput_includeCamera_235);
				if (logic_uScript_LockPlayerInput_uScript_LockPlayerInput_235.Out)
				{
					Relay_In_15();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at uScript_LockPlayerInput.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_236()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("9bd27ea4-1bef-4afc-85ca-02d2b4f376ae", "uScript_LockPause", Relay_In_236))
			{
				logic_uScript_LockPause_uScript_LockPause_236.In(logic_uScript_LockPause_lockPause_236, logic_uScript_LockPause_disabledReason_236);
				if (logic_uScript_LockPause_uScript_LockPause_236.Out)
				{
					Relay_In_235();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at uScript_LockPause.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_237()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("a1895a5f-65d8-438e-a552-1dfdb4e6ba5c", "Compare_Bool", Relay_In_237))
			{
				logic_uScriptCon_CompareBool_Bool_237 = local_CraftingMenuOpened01_System_Boolean;
				logic_uScriptCon_CompareBool_uScriptCon_CompareBool_237.In(logic_uScriptCon_CompareBool_Bool_237);
				if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_237.True)
				{
					Relay_In_238();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at Compare Bool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_238()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("a0ae276e-654f-4f10-8557-afaff2d0dc35", "Compare_Bool", Relay_In_238))
			{
				logic_uScriptCon_CompareBool_Bool_238 = local_CraftingInProgress01_System_Boolean;
				logic_uScriptCon_CompareBool_uScriptCon_CompareBool_238.In(logic_uScriptCon_CompareBool_Bool_238);
				if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_238.False)
				{
					Relay_False_243();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at Compare Bool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_True_243()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("ab9c4b87-ae26-4a6c-9d29-a7eb9397da5f", "Set_Bool", Relay_True_243))
			{
				logic_uScriptAct_SetBool_uScriptAct_SetBool_243.True(out logic_uScriptAct_SetBool_Target_243);
				local_CraftingMenuOpened01_System_Boolean = logic_uScriptAct_SetBool_Target_243;
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at Set Bool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_False_243()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("ab9c4b87-ae26-4a6c-9d29-a7eb9397da5f", "Set_Bool", Relay_False_243))
			{
				logic_uScriptAct_SetBool_uScriptAct_SetBool_243.False(out logic_uScriptAct_SetBool_Target_243);
				local_CraftingMenuOpened01_System_Boolean = logic_uScriptAct_SetBool_Target_243;
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at Set Bool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_244()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("0957dd24-e6f9-4c81-8988-1cf0b14b2f08", "Compare_Bool", Relay_In_244))
			{
				logic_uScriptCon_CompareBool_Bool_244 = local_CraftingInProgress02_System_Boolean;
				logic_uScriptCon_CompareBool_uScriptCon_CompareBool_244.In(logic_uScriptCon_CompareBool_Bool_244);
				if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_244.False)
				{
					Relay_False_246();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at Compare Bool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_True_246()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("49cb4012-c094-49ac-a5be-93be12d8cdc3", "Set_Bool", Relay_True_246))
			{
				logic_uScriptAct_SetBool_uScriptAct_SetBool_246.True(out logic_uScriptAct_SetBool_Target_246);
				local_CraftingMenuOpened02_System_Boolean = logic_uScriptAct_SetBool_Target_246;
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at Set Bool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_False_246()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("49cb4012-c094-49ac-a5be-93be12d8cdc3", "Set_Bool", Relay_False_246))
			{
				logic_uScriptAct_SetBool_uScriptAct_SetBool_246.False(out logic_uScriptAct_SetBool_Target_246);
				local_CraftingMenuOpened02_System_Boolean = logic_uScriptAct_SetBool_Target_246;
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at Set Bool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_248()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("e4a925c8-142a-4756-9cc2-3d0fcdd79fa5", "Compare_Bool", Relay_In_248))
			{
				logic_uScriptCon_CompareBool_Bool_248 = local_CraftingMenuOpened02_System_Boolean;
				logic_uScriptCon_CompareBool_uScriptCon_CompareBool_248.In(logic_uScriptCon_CompareBool_Bool_248);
				if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_248.True)
				{
					Relay_In_244();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at Compare Bool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_251()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("4320fd41-9c57-44ef-ad32-af9d67ea96c7", "uScript_LockPause", Relay_In_251))
			{
				logic_uScript_LockPause_uScript_LockPause_251.In(logic_uScript_LockPause_lockPause_251, logic_uScript_LockPause_disabledReason_251);
				if (logic_uScript_LockPause_uScript_LockPause_251.Out)
				{
					Relay_In_279();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at uScript_LockPause.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_252()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("72cabd05-0f6e-4713-9caf-7e886c3938bc", "uScript_LockPause", Relay_In_252))
			{
				logic_uScript_LockPause_uScript_LockPause_252.In(logic_uScript_LockPause_lockPause_252, logic_uScript_LockPause_disabledReason_252);
				if (logic_uScript_LockPause_uScript_LockPause_252.Out)
				{
					Relay_In_281();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at uScript_LockPause.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_253()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("d0255d8c-69ab-4667-b7c4-d8ab45e8a3f7", "uScript_LockPause", Relay_In_253))
			{
				logic_uScript_LockPause_uScript_LockPause_253.In(logic_uScript_LockPause_lockPause_253, logic_uScript_LockPause_disabledReason_253);
				if (logic_uScript_LockPause_uScript_LockPause_253.Out)
				{
					Relay_In_278();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at uScript_LockPause.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_254()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("4c8bfe74-7aff-4912-b969-661a2fcef7e7", "uScript_IsHUDElementLinkedToBlock", Relay_In_254))
			{
				logic_uScript_IsHUDElementLinkedToBlock_hudElement_254 = local_CraftingMenu_ManHUD_HUDElementType;
				logic_uScript_IsHUDElementLinkedToBlock_targetBlock_254 = local_FabricatorBlock_TankBlock;
				logic_uScript_IsHUDElementLinkedToBlock_uScript_IsHUDElementLinkedToBlock_254.In(logic_uScript_IsHUDElementLinkedToBlock_hudElement_254, logic_uScript_IsHUDElementLinkedToBlock_targetBlock_254);
				if (logic_uScript_IsHUDElementLinkedToBlock_uScript_IsHUDElementLinkedToBlock_254.True)
				{
					Relay_In_50();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at uScript_IsHUDElementLinkedToBlock.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_257()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("bf9ece7d-e334-433a-9ef3-8bf54ce15ce2", "uScript_IsHUDElementLinkedToBlock", Relay_In_257))
			{
				logic_uScript_IsHUDElementLinkedToBlock_hudElement_257 = local_CraftingMenu_ManHUD_HUDElementType;
				logic_uScript_IsHUDElementLinkedToBlock_targetBlock_257 = local_FabricatorBlock_TankBlock;
				logic_uScript_IsHUDElementLinkedToBlock_uScript_IsHUDElementLinkedToBlock_257.In(logic_uScript_IsHUDElementLinkedToBlock_hudElement_257, logic_uScript_IsHUDElementLinkedToBlock_targetBlock_257);
				if (logic_uScript_IsHUDElementLinkedToBlock_uScript_IsHUDElementLinkedToBlock_257.True)
				{
					Relay_In_97();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at uScript_IsHUDElementLinkedToBlock.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_261()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("e857a9ac-9387-4568-81ad-f625f5425178", "uScript_HideHUDElement", Relay_In_261))
			{
				logic_uScript_HideHUDElement_hudElement_261 = local_CraftingMenu_ManHUD_HUDElementType;
				logic_uScript_HideHUDElement_uScript_HideHUDElement_261.In(logic_uScript_HideHUDElement_hudElement_261);
				if (logic_uScript_HideHUDElement_uScript_HideHUDElement_261.Out)
				{
					Relay_In_148();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at uScript_HideHUDElement.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_263()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("4a5c4ba0-8719-4553-ad87-f2b93f66675a", "uScript_HideHUDElement", Relay_In_263))
			{
				logic_uScript_HideHUDElement_hudElement_263 = local_CraftingMenu_ManHUD_HUDElementType;
				logic_uScript_HideHUDElement_uScript_HideHUDElement_263.In(logic_uScript_HideHUDElement_hudElement_263);
				if (logic_uScript_HideHUDElement_uScript_HideHUDElement_263.Out)
				{
					Relay_In_150();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at uScript_HideHUDElement.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_264()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("c35455ed-afe6-48a4-9b81-ef2ac041ac1b", "uScript_EnableGlow", Relay_In_264))
			{
				logic_uScript_EnableGlow_targetObject_264 = local_FabricatorBlock_TankBlock;
				logic_uScript_EnableGlow_uScript_EnableGlow_264.In(logic_uScript_EnableGlow_targetObject_264, logic_uScript_EnableGlow_enable_264);
				if (logic_uScript_EnableGlow_uScript_EnableGlow_264.Out)
				{
					Relay_DisableAutoCloseUI_225();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at uScript_EnableGlow.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_267()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("537662bc-0995-4d10-9d2d-2f2cf6e5c6c9", "uScript_EnableGlow", Relay_In_267))
			{
				logic_uScript_EnableGlow_targetObject_267 = local_FabricatorBlock_TankBlock;
				logic_uScript_EnableGlow_uScript_EnableGlow_267.In(logic_uScript_EnableGlow_targetObject_267, logic_uScript_EnableGlow_enable_267);
				if (logic_uScript_EnableGlow_uScript_EnableGlow_267.Out)
				{
					Relay_True_65();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at uScript_EnableGlow.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_269()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("e8454101-3d52-41ad-93ba-9faeac243f0e", "uScript_EnableGlow", Relay_In_269))
			{
				logic_uScript_EnableGlow_targetObject_269 = local_FabricatorBlock_TankBlock;
				logic_uScript_EnableGlow_uScript_EnableGlow_269.In(logic_uScript_EnableGlow_targetObject_269, logic_uScript_EnableGlow_enable_269);
				if (logic_uScript_EnableGlow_uScript_EnableGlow_269.Out)
				{
					Relay_DisableAutoCloseUI_230();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at uScript_EnableGlow.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_271()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("4b4c4b1d-cb8c-47f2-aa76-929802f25e12", "uScript_EnableGlow", Relay_In_271))
			{
				logic_uScript_EnableGlow_targetObject_271 = local_FabricatorBlock_TankBlock;
				logic_uScript_EnableGlow_uScript_EnableGlow_271.In(logic_uScript_EnableGlow_targetObject_271, logic_uScript_EnableGlow_enable_271);
				if (logic_uScript_EnableGlow_uScript_EnableGlow_271.Out)
				{
					Relay_True_90();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at uScript_EnableGlow.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_272()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("ef25c6be-2ed7-4edb-8ca6-fef30302aadc", "uScript_EnableGlow", Relay_In_272))
			{
				logic_uScript_EnableGlow_targetObject_272 = local_RefineryBlock_TankBlock;
				logic_uScript_EnableGlow_uScript_EnableGlow_272.In(logic_uScript_EnableGlow_targetObject_272, logic_uScript_EnableGlow_enable_272);
				if (logic_uScript_EnableGlow_uScript_EnableGlow_272.Out)
				{
					Relay_In_275();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at uScript_EnableGlow.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_275()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("71ae5092-097b-4c07-b52c-9b2e4313e015", "uScript_EnableGlow", Relay_In_275))
			{
				logic_uScript_EnableGlow_targetObject_275 = local_FabricatorBlock_TankBlock;
				logic_uScript_EnableGlow_uScript_EnableGlow_275.In(logic_uScript_EnableGlow_targetObject_275, logic_uScript_EnableGlow_enable_275);
				if (logic_uScript_EnableGlow_uScript_EnableGlow_275.Out)
				{
					Relay_EnableAutoCloseUI_233();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at uScript_EnableGlow.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_276()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("7b5e8d80-b4b7-45d8-b3b2-d0e3294818e6", "Send_Analytics_Event", Relay_In_276))
			{
				logic_uScript_SendAnaliticsEvent_uScript_SendAnaliticsEvent_276.In(logic_uScript_SendAnaliticsEvent_analiticsEvent_276, logic_uScript_SendAnaliticsEvent_parameterName_276, logic_uScript_SendAnaliticsEvent_parameter_276);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at Send Analytics Event.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_277()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("a1cff45f-09fc-4de1-8cac-069bf7d621e9", "Send_Analytics_Event", Relay_In_277))
			{
				logic_uScript_SendAnaliticsEvent_uScript_SendAnaliticsEvent_277.In(logic_uScript_SendAnaliticsEvent_analiticsEvent_277, logic_uScript_SendAnaliticsEvent_parameterName_277, logic_uScript_SendAnaliticsEvent_parameter_277);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at Send Analytics Event.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_278()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("7d276038-a42f-4c16-9f77-c547dd9518eb", "uScript_LockHudGroup", Relay_In_278))
			{
				logic_uScript_LockHudGroup_uScript_LockHudGroup_278.In(logic_uScript_LockHudGroup_group_278, logic_uScript_LockHudGroup_locked_278);
				if (logic_uScript_LockHudGroup_uScript_LockHudGroup_278.Out)
				{
					Relay_In_63();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at uScript_LockHudGroup.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_279()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("b9fae360-1e86-4a12-be80-57ff467426a5", "uScript_LockHudGroup", Relay_In_279))
			{
				logic_uScript_LockHudGroup_uScript_LockHudGroup_279.In(logic_uScript_LockHudGroup_group_279, logic_uScript_LockHudGroup_locked_279);
				if (logic_uScript_LockHudGroup_uScript_LockHudGroup_279.Out)
				{
					Relay_In_263();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at uScript_LockHudGroup.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_280()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("cc57431d-8273-454b-a309-494d40c33c63", "uScript_LockHudGroup", Relay_In_280))
			{
				logic_uScript_LockHudGroup_uScript_LockHudGroup_280.In(logic_uScript_LockHudGroup_group_280, logic_uScript_LockHudGroup_locked_280);
				if (logic_uScript_LockHudGroup_uScript_LockHudGroup_280.Out)
				{
					Relay_In_576();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at uScript_LockHudGroup.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_281()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("77ca0a03-471f-461c-a589-cc49eed9413a", "uScript_LockHudGroup", Relay_In_281))
			{
				logic_uScript_LockHudGroup_uScript_LockHudGroup_281.In(logic_uScript_LockHudGroup_group_281, logic_uScript_LockHudGroup_locked_281);
				if (logic_uScript_LockHudGroup_uScript_LockHudGroup_281.Out)
				{
					Relay_In_195();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at uScript_LockHudGroup.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_576()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("dae38ae9-db07-4bc2-9621-d1d4fbb9e79f", "Compare_Bool", Relay_In_576))
			{
				logic_uScriptCon_CompareBool_Bool_576 = local_CraftingInProgress02_System_Boolean;
				logic_uScriptCon_CompareBool_uScriptCon_CompareBool_576.In(logic_uScriptCon_CompareBool_Bool_576);
				bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_576.True;
				bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_576.False;
				if (num)
				{
					Relay_In_224();
				}
				if (flag)
				{
					Relay_In_102();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at Compare Bool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Save_Out_583()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("9f1b26af-b3a2-46db-be0a-c383d936040f", "", Relay_Save_Out_583))
			{
				Relay_Save_129();
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Load_Out_583()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("9f1b26af-b3a2-46db-be0a-c383d936040f", "", Relay_Load_Out_583))
			{
				Relay_Load_129();
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Restart_Out_583()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("9f1b26af-b3a2-46db-be0a-c383d936040f", "", Relay_Restart_Out_583))
			{
				Relay_Set_False_129();
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Save_583()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("9f1b26af-b3a2-46db-be0a-c383d936040f", "", Relay_Save_583))
			{
				logic_SubGraph_SaveLoadBool_boolean_583 = local_CraftingInProgress02_System_Boolean;
				logic_SubGraph_SaveLoadBool_boolAsVariable_583 = local_CraftingInProgress02_System_Boolean;
				logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_583.Save(ref logic_SubGraph_SaveLoadBool_boolean_583, logic_SubGraph_SaveLoadBool_boolAsVariable_583, logic_SubGraph_SaveLoadBool_uniqueID_583);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Load_583()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("9f1b26af-b3a2-46db-be0a-c383d936040f", "", Relay_Load_583))
			{
				logic_SubGraph_SaveLoadBool_boolean_583 = local_CraftingInProgress02_System_Boolean;
				logic_SubGraph_SaveLoadBool_boolAsVariable_583 = local_CraftingInProgress02_System_Boolean;
				logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_583.Load(ref logic_SubGraph_SaveLoadBool_boolean_583, logic_SubGraph_SaveLoadBool_boolAsVariable_583, logic_SubGraph_SaveLoadBool_uniqueID_583);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Set_True_583()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("9f1b26af-b3a2-46db-be0a-c383d936040f", "", Relay_Set_True_583))
			{
				logic_SubGraph_SaveLoadBool_boolean_583 = local_CraftingInProgress02_System_Boolean;
				logic_SubGraph_SaveLoadBool_boolAsVariable_583 = local_CraftingInProgress02_System_Boolean;
				logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_583.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_583, logic_SubGraph_SaveLoadBool_boolAsVariable_583, logic_SubGraph_SaveLoadBool_uniqueID_583);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Set_False_583()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("9f1b26af-b3a2-46db-be0a-c383d936040f", "", Relay_Set_False_583))
			{
				logic_SubGraph_SaveLoadBool_boolean_583 = local_CraftingInProgress02_System_Boolean;
				logic_SubGraph_SaveLoadBool_boolAsVariable_583 = local_CraftingInProgress02_System_Boolean;
				logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_583.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_583, logic_SubGraph_SaveLoadBool_boolAsVariable_583, logic_SubGraph_SaveLoadBool_uniqueID_583);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_GSO_2_Crafting_05.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void UpdateEditorValues()
	{
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_GSO_2_Crafting_05.uscript:distBaseFound", distBaseFound);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("f5845173-f1aa-44df-bd09-9d8b14ac03a1", distBaseFound);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_GSO_2_Crafting_05.uscript:11", local_11_BlockTypes);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("61deac81-a357-4093-8180-d7e38f6fec5d", local_11_BlockTypes);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_GSO_2_Crafting_05.uscript:blockTypeToCraft02", blockTypeToCraft02);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("2cb38419-a5c2-419a-afae-27ec9b6d536b", blockTypeToCraft02);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_GSO_2_Crafting_05.uscript:ghostBlockRefinery", ghostBlockRefinery);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("aeda88a2-cd68-4582-be8e-ec6d212cc6ad", ghostBlockRefinery);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_GSO_2_Crafting_05.uscript:msgResourcesInSiloShown", local_msgResourcesInSiloShown_System_Boolean);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("9314d5e2-61f7-4eef-84d0-ccb055f4a570", local_msgResourcesInSiloShown_System_Boolean);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_GSO_2_Crafting_05.uscript:msgIntroShown", local_msgIntroShown_System_Boolean);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("61dfe212-e9af-4a34-87c6-597ba9bf3b95", local_msgIntroShown_System_Boolean);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_GSO_2_Crafting_05.uscript:msgRefineryAttachedShown", local_msgRefineryAttachedShown_System_Boolean);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("77c890fa-e719-4f4c-976f-6c3dac52fcc8", local_msgRefineryAttachedShown_System_Boolean);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_GSO_2_Crafting_05.uscript:blockTypeSilo", blockTypeSilo);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("b3a67db7-f2be-4828-aef9-790d4dd619ae", blockTypeSilo);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_GSO_2_Crafting_05.uscript:blockTypeToCraft01", blockTypeToCraft01);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("ec2f831e-726c-4ccb-8a51-5a3c3c730927", blockTypeToCraft01);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_GSO_2_Crafting_05.uscript:69", local_69_BlockTypes);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("e6230087-67c2-460c-ba6b-687656e49d66", local_69_BlockTypes);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_GSO_2_Crafting_05.uscript:73", local_73_TankBlock);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("6b76e08a-a141-426d-8053-c6915f72480c", local_73_TankBlock);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_GSO_2_Crafting_05.uscript:resourcesToHoldInSilo", resourcesToHoldInSilo);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("084b9ebb-3f2f-49ca-83f6-5ff6c6cefeca", resourcesToHoldInSilo);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_GSO_2_Crafting_05.uscript:baseAllowedResourceTypes", baseAllowedResourceTypes);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("379ca25c-19a5-4e1e-8429-926b064cd647", baseAllowedResourceTypes);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_GSO_2_Crafting_05.uscript:blockTypeFabricator", blockTypeFabricator);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("527bd9e9-ac87-407b-8b7f-ffa7d553df45", blockTypeFabricator);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_GSO_2_Crafting_05.uscript:blockTypeToCraftCategory", blockTypeToCraftCategory);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("b8890483-3e90-4194-b17a-c2739d831e11", blockTypeToCraftCategory);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_GSO_2_Crafting_05.uscript:timeRepeatResourcesReminder", timeRepeatResourcesReminder);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("35ba0ea3-5819-492f-b704-1451fc97e5b6", timeRepeatResourcesReminder);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_GSO_2_Crafting_05.uscript:msgBaseFoundShown", local_msgBaseFoundShown_System_Boolean);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("ffaa25b0-66fe-4b3c-876e-05aa8ae28d8e", local_msgBaseFoundShown_System_Boolean);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_GSO_2_Crafting_05.uscript:Msg", local_Msg_System_String);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("39ed382c-a678-4d17-a2fd-a1d4f3ff5725", local_Msg_System_String);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_GSO_2_Crafting_05.uscript:messageTag", messageTag);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("4b652128-4d72-40d7-ae54-f81d839a6ce4", messageTag);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_GSO_2_Crafting_05.uscript:BlockCrafted01", local_BlockCrafted01_System_Boolean);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("fe060ff6-e0b4-4a11-a42a-5358c26b3554", local_BlockCrafted01_System_Boolean);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_GSO_2_Crafting_05.uscript:BlockCrafted02", local_BlockCrafted02_System_Boolean);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("48a78488-5d03-4989-a8d4-cd5f88585e24", local_BlockCrafted02_System_Boolean);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_GSO_2_Crafting_05.uscript:msgRefineryExplanation02Shown", local_msgRefineryExplanation02Shown_System_Boolean);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("dc51068e-35e8-476e-bb47-2298601b82a5", local_msgRefineryExplanation02Shown_System_Boolean);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_GSO_2_Crafting_05.uscript:Stage", local_Stage_System_Int32);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("44cf1528-436c-4dc9-86cd-7ff52965a5ed", local_Stage_System_Int32);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_GSO_2_Crafting_05.uscript:msg01Intro", msg01Intro);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("831a278e-8492-4b58-9c22-ef5bc40c3002", msg01Intro);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_GSO_2_Crafting_05.uscript:msg02BaseFound", msg02BaseFound);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("6cad5d4e-e24b-454f-8332-f88ffb51786d", msg02BaseFound);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_GSO_2_Crafting_05.uscript:msg03ResourcesReminder", msg03ResourcesReminder);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("85aebe80-cee6-4632-b735-201c45eeafbd", msg03ResourcesReminder);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_GSO_2_Crafting_05.uscript:msg04ResourcesInSilo", msg04ResourcesInSilo);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("09ebfbbf-c48c-450f-98c9-2bf6deae4982", msg04ResourcesInSilo);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_GSO_2_Crafting_05.uscript:msg05AttachRefinery", msg05AttachRefinery);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("46fdbd4c-ec60-40cc-9b97-93ac32e0d891", msg05AttachRefinery);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_GSO_2_Crafting_05.uscript:msg06CraftBlock01", msg06CraftBlock01);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("72f98e55-8502-4b9d-afee-bf111df7d68f", msg06CraftBlock01);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_GSO_2_Crafting_05.uscript:msg07RefineryExplanation01", msg07RefineryExplanation01);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("623c8260-a004-4890-a2ae-151acb587caa", msg07RefineryExplanation01);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_GSO_2_Crafting_05.uscript:msg08CraftBlock02", msg08CraftBlock02);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("20d6bf06-c96f-4de1-99d4-a3ec914dcb53", msg08CraftBlock02);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_GSO_2_Crafting_05.uscript:msg09RefineryExplanation02", msg09RefineryExplanation02);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("82c05ae1-3a5a-42e5-bd8f-3949d5d45e5c", msg09RefineryExplanation02);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_GSO_2_Crafting_05.uscript:msg10Complete", msg10Complete);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("9d26c093-0fa9-43f6-8abe-3eca7263a583", msg10Complete);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_GSO_2_Crafting_05.uscript:msgBlockOutsideArea", msgBlockOutsideArea);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("2180a7bd-e7ef-4e3d-95ad-41b751490e2a", msgBlockOutsideArea);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_GSO_2_Crafting_05.uscript:NearBase", local_NearBase_System_Boolean);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("d16f82b5-6215-414b-ae12-81aad52ee3ec", local_NearBase_System_Boolean);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_GSO_2_Crafting_05.uscript:messageSpeaker", messageSpeaker);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("4506cb2f-5437-4f98-bf97-8e747f9dc462", messageSpeaker);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_GSO_2_Crafting_05.uscript:msgLeavingMissionArea", msgLeavingMissionArea);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("0c5364e0-59e1-4b33-9a9d-ab3eaff3c2db", msgLeavingMissionArea);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_GSO_2_Crafting_05.uscript:NPCFlyAwayAI", NPCFlyAwayAI);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("3252c51a-af5b-4004-89fc-2cb12769c19d", NPCFlyAwayAI);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_GSO_2_Crafting_05.uscript:NPCDespawnParticleEffect", NPCDespawnParticleEffect);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("2f4a399d-3918-4886-ad5a-4c72b8336074", NPCDespawnParticleEffect);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_GSO_2_Crafting_05.uscript:completedBasePreset", completedBasePreset);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("1df94434-49e0-45c6-bffb-a3e1518d53a6", completedBasePreset);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_GSO_2_Crafting_05.uscript:blockSpawnData", blockSpawnData);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("76cafade-4878-4248-98b0-cee12e21dd22", blockSpawnData);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_GSO_2_Crafting_05.uscript:NPCSpawnData", NPCSpawnData);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("37751ffe-d940-4aac-92bd-2bebd6bfb22f", NPCSpawnData);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_GSO_2_Crafting_05.uscript:clearSceneryRadius", clearSceneryRadius);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("851f923d-9b4e-41c2-8ac7-a5c7a2d89aa6", clearSceneryRadius);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_GSO_2_Crafting_05.uscript:baseSpawnData", baseSpawnData);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("13baf444-6184-4aed-b1f5-cd4686973626", baseSpawnData);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_GSO_2_Crafting_05.uscript:basePosition", basePosition);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("3a9f293a-ef9d-4fb6-8b4b-08cd33450c72", basePosition);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_GSO_2_Crafting_05.uscript:NPCTech", local_NPCTech_Tank);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("f84c7438-c1ea-46c2-9491-f4c12829378d", local_NPCTech_Tank);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_GSO_2_Crafting_05.uscript:blockTypeToHighlight01", blockTypeToHighlight01);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("3c9dcc2f-f8c6-4db9-9c17-55a3eda18c13", blockTypeToHighlight01);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_GSO_2_Crafting_05.uscript:blockTypeToHighlight02", blockTypeToHighlight02);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("1468d595-dadb-42ea-b071-311f553e7b8d", blockTypeToHighlight02);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_GSO_2_Crafting_05.uscript:CraftingBaseTech", local_CraftingBaseTech_Tank);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("b4be51b4-5c02-48d9-bc31-7e2bb1aa17da", local_CraftingBaseTech_Tank);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_GSO_2_Crafting_05.uscript:CraftingInProgress01", local_CraftingInProgress01_System_Boolean);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("7213df93-6dcb-44a7-b6dd-541ebc69868d", local_CraftingInProgress01_System_Boolean);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_GSO_2_Crafting_05.uscript:CraftingMenuOpened01", local_CraftingMenuOpened01_System_Boolean);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("eaa4d26d-2447-4055-8521-1dd6c7be69ab", local_CraftingMenuOpened01_System_Boolean);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_GSO_2_Crafting_05.uscript:CraftingMenuOpened02", local_CraftingMenuOpened02_System_Boolean);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("12268dbb-ed1b-409d-9cc8-940ea21e6ef7", local_CraftingMenuOpened02_System_Boolean);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_GSO_2_Crafting_05.uscript:CraftingMenu", local_CraftingMenu_ManHUD_HUDElementType);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("f55c84d3-7fba-4e75-a974-a5017a0d94f8", local_CraftingMenu_ManHUD_HUDElementType);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_GSO_2_Crafting_05.uscript:RefineryBlock", local_RefineryBlock_TankBlock);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("1e4b4666-475b-4e28-a9c4-2d089c614bd1", local_RefineryBlock_TankBlock);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_GSO_2_Crafting_05.uscript:FabricatorBlock", local_FabricatorBlock_TankBlock);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("33bfdb93-bc50-47c8-be48-3a2ef44c1c04", local_FabricatorBlock_TankBlock);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_GSO_2_Crafting_05.uscript:CraftingInProgress02", local_CraftingInProgress02_System_Boolean);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("0f29fa85-3032-411b-8fd8-589d24488780", local_CraftingInProgress02_System_Boolean);
	}

	private bool CheckDebugBreak(string guid, string name, ContinueExecution method)
	{
		if (m_Breakpoint)
		{
			return true;
		}
		if (uScript_MasterComponent.FindBreakpoint(guid))
		{
			if (!(uScript_MasterComponent.LatestMasterComponent.CurrentBreakpoint == guid))
			{
				uScript_MasterComponent.LatestMasterComponent.CurrentBreakpoint = guid;
				UpdateEditorValues();
				Debug.Log(("uScript BREAK Node:" + name + " ((Time: " + Time.time) ?? "");
				Debug.Break();
				m_ContinueExecution = method.Invoke;
				m_Breakpoint = true;
				return true;
			}
			uScript_MasterComponent.LatestMasterComponent.CurrentBreakpoint = "";
		}
		return false;
	}
}
