using System;
using BehaviorDesigner.Runtime;
using UnityEngine;

[Serializable]
[NodePath("Graphs")]
[FriendlyName("Untitled", "")]
public class Mission_GSO_2_Crafting_02 : uScriptLogic
{
	private GameObject parentGameObject;

	private uScript_GUI thisScriptsOnGuiListener;

	private bool m_RegisteredForEvents;

	[Multiline(3)]
	public string basePosition = "";

	public SpawnTechData[] baseSpawnData = new SpawnTechData[0];

	public SpawnBlockData[] blockSpawnData = new SpawnBlockData[0];

	public SpawnBlockData[] blockSpawnDataFabricator = new SpawnBlockData[0];

	public BlockTypes blockTypeConveyor;

	public BlockTypes blockTypeToCraft;

	public ItemTypeInfo blockTypeToHighlight;

	public float clearSceneryRadius;

	public TankPreset completedBasePreset;

	public float distBaseFound;

	public GhostBlockSpawnData[] ghostBlockConveyor01 = new GhostBlockSpawnData[0];

	public GhostBlockSpawnData[] ghostBlockConveyor02 = new GhostBlockSpawnData[0];

	public GhostBlockSpawnData[] ghostBlockFabricator = new GhostBlockSpawnData[0];

	public GhostBlockSpawnData[] ghostBlockReceiver = new GhostBlockSpawnData[0];

	private TankBlock[] local_107_TankBlockArray = new TankBlock[0];

	private BlockTypes local_11_BlockTypes;

	private TankBlock[] local_125_TankBlockArray = new TankBlock[0];

	private bool local_BlockCrafted_System_Boolean;

	private TankBlock local_ConveyorBlock01_TankBlock;

	private TankBlock local_ConveyorBlock02_TankBlock;

	private Tank local_CraftingBaseTech_Tank;

	private bool local_CraftingInProgress_System_Boolean;

	private ManHUD.HUDElementType local_CraftingMenu_ManHUD_HUDElementType = ManHUD.HUDElementType.BlockRecipeSelect;

	private bool local_CraftingMenuOpened_System_Boolean;

	private bool local_DraggingConveyor_System_Boolean;

	private TankBlock local_FabricatorBlock_TankBlock;

	private bool local_FabricatorSpawned_System_Boolean;

	private TankBlock local_GhostBlockConveyor01_TankBlock;

	private bool local_GhostBlockConveyor01Spawned_System_Boolean;

	private TankBlock local_GhostBlockConveyor02_TankBlock;

	private bool local_GhostBlockConveyor02Spawned_System_Boolean;

	private bool local_msgBaseFoundShown_System_Boolean;

	private bool local_msgConveyorsAttachedShown_System_Boolean;

	private bool local_msgFabricatorSpawnedShown_System_Boolean;

	private bool local_msgIntroShown_System_Boolean;

	private bool local_msgReceiverAttachedShown_System_Boolean;

	private bool local_NearBase_System_Boolean;

	private Tank local_NPCTech_Tank;

	private int local_NumConveyorsAttached_System_Int32 = 2;

	private TankBlock local_ReceiverBlock_TankBlock;

	private bool local_Resource01Spawned_System_Boolean;

	private int local_Stage_System_Int32 = 1;

	private int local_Stage2Steps_System_Int32 = 1;

	public uScript_AddMessage.MessageSpeaker messageSpeaker;

	[Multiline(3)]
	public string messageTag = "";

	public uScript_AddMessage.MessageData msg01Intro;

	public uScript_AddMessage.MessageData msg02BaseFound;

	public uScript_AddMessage.MessageData msg03AttachReceiver;

	public uScript_AddMessage.MessageData msg04ReceiverAttached;

	public uScript_AddMessage.MessageData msg05SiloExplanation;

	public uScript_AddMessage.MessageData msg06AttachConveyors;

	public uScript_AddMessage.MessageData msg07ConveyorsAttached;

	public uScript_AddMessage.MessageData msg08FabricatorSpawned;

	public uScript_AddMessage.MessageData msg09AttachFabricator;

	public uScript_AddMessage.MessageData msg10OpenMenu;

	public uScript_AddMessage.MessageData msg10OpenMenu_Pad;

	public uScript_AddMessage.MessageData msg11CraftBlock;

	public uScript_AddMessage.MessageData msg11CraftBlock_Pad;

	public uScript_AddMessage.MessageData msg12Complete;

	public uScript_AddMessage.MessageData msgBlockOutsideArea;

	public uScript_AddMessage.MessageData msgLeavingMissionArea;

	public Transform NPCDespawnParticleEffect;

	public ExternalBehaviorTree NPCFlyAwayAI;

	public SpawnTechData[] NPCSpawnData = new SpawnTechData[0];

	public ChunkTypes[] resourceList = new ChunkTypes[0];

	public float timeWaitForChunksInSilos;

	private GameObject owner_Connection_1;

	private GameObject owner_Connection_7;

	private GameObject owner_Connection_10;

	private GameObject owner_Connection_110;

	private GameObject owner_Connection_121;

	private GameObject owner_Connection_157;

	private GameObject owner_Connection_170;

	private GameObject owner_Connection_284;

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

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_14 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_14 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_14 = true;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_14 = true;

	private SubGraph_Crafting_Tutorial_Init logic_SubGraph_Crafting_Tutorial_Init_SubGraph_Crafting_Tutorial_Init_15 = new SubGraph_Crafting_Tutorial_Init();

	private SpawnTechData[] logic_SubGraph_Crafting_Tutorial_Init_baseSpawnData_15 = new SpawnTechData[0];

	private SpawnBlockData[] logic_SubGraph_Crafting_Tutorial_Init_blockSpawnData_15 = new SpawnBlockData[0];

	private SpawnTechData[] logic_SubGraph_Crafting_Tutorial_Init_NPCSpawnData_15 = new SpawnTechData[0];

	private TankPreset logic_SubGraph_Crafting_Tutorial_Init_completedBasePreset_15;

	private bool logic_SubGraph_Crafting_Tutorial_Init_useNPCAsTutorialTech_15;

	private bool logic_SubGraph_Crafting_Tutorial_Init_spawnBase_15 = true;

	private string logic_SubGraph_Crafting_Tutorial_Init_basePosition_15 = "";

	private float logic_SubGraph_Crafting_Tutorial_Init_clearSceneryRadius_15;

	private Tank logic_SubGraph_Crafting_Tutorial_Init_CraftingBaseTech_15;

	private Tank logic_SubGraph_Crafting_Tutorial_Init_NPCTech_15;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_22 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_22;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_22 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_22 = "Resource01Spawned";

	private uScript_LockTechStacks logic_uScript_LockTechStacks_uScript_LockTechStacks_24 = new uScript_LockTechStacks();

	private Tank logic_uScript_LockTechStacks_tech_24;

	private bool logic_uScript_LockTechStacks_Out_24 = true;

	private SubGraph_Crafting_Tutorial_ManageBlock logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_27 = new SubGraph_Crafting_Tutorial_ManageBlock();

	private SpawnBlockData[] logic_SubGraph_Crafting_Tutorial_ManageBlock_blockSpawnData_27 = new SpawnBlockData[0];

	private int logic_SubGraph_Crafting_Tutorial_ManageBlock_blockIndex_27 = 2;

	private TankBlock logic_SubGraph_Crafting_Tutorial_ManageBlock_block_27;

	private bool logic_SubGraph_Crafting_Tutorial_ManageBlock_allowAnchoring_27;

	private uScript_AddMessage.MessageData logic_SubGraph_Crafting_Tutorial_ManageBlock_msgBlockOutsideArea_27;

	private uScript_AddMessage.MessageSpeaker logic_SubGraph_Crafting_Tutorial_ManageBlock_messageSpeaker_27;

	private SubGraph_Crafting_Tutorial_ManageBlock logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_32 = new SubGraph_Crafting_Tutorial_ManageBlock();

	private SpawnBlockData[] logic_SubGraph_Crafting_Tutorial_ManageBlock_blockSpawnData_32 = new SpawnBlockData[0];

	private int logic_SubGraph_Crafting_Tutorial_ManageBlock_blockIndex_32;

	private TankBlock logic_SubGraph_Crafting_Tutorial_ManageBlock_block_32;

	private bool logic_SubGraph_Crafting_Tutorial_ManageBlock_allowAnchoring_32;

	private uScript_AddMessage.MessageData logic_SubGraph_Crafting_Tutorial_ManageBlock_msgBlockOutsideArea_32;

	private uScript_AddMessage.MessageSpeaker logic_SubGraph_Crafting_Tutorial_ManageBlock_messageSpeaker_32;

	private SubGraph_Crafting_Tutorial_ManageBlock logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_34 = new SubGraph_Crafting_Tutorial_ManageBlock();

	private SpawnBlockData[] logic_SubGraph_Crafting_Tutorial_ManageBlock_blockSpawnData_34 = new SpawnBlockData[0];

	private int logic_SubGraph_Crafting_Tutorial_ManageBlock_blockIndex_34 = 1;

	private TankBlock logic_SubGraph_Crafting_Tutorial_ManageBlock_block_34;

	private bool logic_SubGraph_Crafting_Tutorial_ManageBlock_allowAnchoring_34;

	private uScript_AddMessage.MessageData logic_SubGraph_Crafting_Tutorial_ManageBlock_msgBlockOutsideArea_34;

	private uScript_AddMessage.MessageSpeaker logic_SubGraph_Crafting_Tutorial_ManageBlock_messageSpeaker_34;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_35 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_35;

	private bool logic_uScriptCon_CompareBool_True_35 = true;

	private bool logic_uScriptCon_CompareBool_False_35 = true;

	private uScript_IsPlayerInRangeOfTech logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_37 = new uScript_IsPlayerInRangeOfTech();

	private Tank logic_uScript_IsPlayerInRangeOfTech_tech_37;

	private float logic_uScript_IsPlayerInRangeOfTech_range_37 = 100f;

	private Tank[] logic_uScript_IsPlayerInRangeOfTech_techs_37 = new Tank[0];

	private bool logic_uScript_IsPlayerInRangeOfTech_Out_37 = true;

	private bool logic_uScript_IsPlayerInRangeOfTech_InRange_37 = true;

	private bool logic_uScript_IsPlayerInRangeOfTech_OutOfRange_37 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_39 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_39;

	private bool logic_uScriptAct_SetBool_Out_39 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_39 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_39 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_40 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_40;

	private bool logic_uScriptAct_SetBool_Out_40 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_40 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_40 = true;

	private uScript_RestrictItemPickup logic_uScript_RestrictItemPickup_uScript_RestrictItemPickup_42 = new uScript_RestrictItemPickup();

	private Tank logic_uScript_RestrictItemPickup_tech_42;

	private ChunkTypes[] logic_uScript_RestrictItemPickup_typesToAccept_42 = new ChunkTypes[0];

	private bool logic_uScript_RestrictItemPickup_Out_42 = true;

	private SubGraph_SaveLoadInt logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_44 = new SubGraph_SaveLoadInt();

	private int logic_SubGraph_SaveLoadInt_restartValue_44 = 2;

	private int logic_SubGraph_SaveLoadInt_integer_44;

	private object logic_SubGraph_SaveLoadInt_intAsVariable_44 = "";

	private string logic_SubGraph_SaveLoadInt_uniqueID_44 = "NumConveyorsAttached";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_47 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_47;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_47 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_47 = "msgIntroShown";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_48 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_48;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_48 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_48 = "msgBaseFoundShown";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_49 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_49;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_49 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_49 = "msgReceiverAttachedShown";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_53 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_53;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_53 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_53 = "msgConveyorsAttachedShown";

	private uScript_HideArrow logic_uScript_HideArrow_uScript_HideArrow_54 = new uScript_HideArrow();

	private bool logic_uScript_HideArrow_Out_54 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_56 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_56;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_56 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_56 = "FabricatorSpawned";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_58 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_58;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_58 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_58 = "msgFabricatorSpawnedShown";

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_60 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_60;

	private bool logic_uScriptAct_SetBool_Out_60 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_60 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_60 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_61 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_61;

	private bool logic_uScriptAct_SetBool_Out_61 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_61 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_61 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_64 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_64;

	private bool logic_uScriptAct_SetBool_Out_64 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_64 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_64 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_65 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_65;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_65 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_65 = "BlockCrafted";

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_68 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_68;

	private bool logic_uScriptAct_SetBool_Out_68 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_68 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_68 = true;

	private SubGraph_Crafting_Tutorial_ManageBlock logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_70 = new SubGraph_Crafting_Tutorial_ManageBlock();

	private SpawnBlockData[] logic_SubGraph_Crafting_Tutorial_ManageBlock_blockSpawnData_70 = new SpawnBlockData[0];

	private int logic_SubGraph_Crafting_Tutorial_ManageBlock_blockIndex_70;

	private TankBlock logic_SubGraph_Crafting_Tutorial_ManageBlock_block_70;

	private bool logic_SubGraph_Crafting_Tutorial_ManageBlock_allowAnchoring_70;

	private uScript_AddMessage.MessageData logic_SubGraph_Crafting_Tutorial_ManageBlock_msgBlockOutsideArea_70;

	private uScript_AddMessage.MessageSpeaker logic_SubGraph_Crafting_Tutorial_ManageBlock_messageSpeaker_70;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_73 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_73;

	private bool logic_uScriptCon_CompareBool_True_73 = true;

	private bool logic_uScriptCon_CompareBool_False_73 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_74 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_74 = true;

	private uScriptCon_ManualSwitch logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_75 = new uScriptCon_ManualSwitch();

	private int logic_uScriptCon_ManualSwitch_CurrentOutput_75;

	private uScript_IsPlayerInRangeOfTech logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_77 = new uScript_IsPlayerInRangeOfTech();

	private Tank logic_uScript_IsPlayerInRangeOfTech_tech_77;

	private float logic_uScript_IsPlayerInRangeOfTech_range_77;

	private Tank[] logic_uScript_IsPlayerInRangeOfTech_techs_77 = new Tank[0];

	private bool logic_uScript_IsPlayerInRangeOfTech_Out_77 = true;

	private bool logic_uScript_IsPlayerInRangeOfTech_InRange_77 = true;

	private bool logic_uScript_IsPlayerInRangeOfTech_OutOfRange_77 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_81 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_81;

	private bool logic_uScriptCon_CompareBool_True_81 = true;

	private bool logic_uScriptCon_CompareBool_False_81 = true;

	private SubGraph_Crafting_Tutorial_AttachBlockToBase logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_SubGraph_Crafting_Tutorial_AttachBlockToBase_84 = new SubGraph_Crafting_Tutorial_AttachBlockToBase();

	private TankBlock logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_Block_84;

	private Tank logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_CraftingBaseTech_84;

	private GhostBlockSpawnData[] logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_ghostBlockData_84 = new GhostBlockSpawnData[0];

	private BlockTypes logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_BlockType_84 = BlockTypes.GSOReceiver_111;

	private Vector3 logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_BlockPosition_84 = new Vector3(-1f, 0f, -1f);

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_87 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_87;

	private bool logic_uScriptAct_SetBool_Out_87 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_87 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_87 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_88 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_88;

	private bool logic_uScriptAct_SetBool_Out_88 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_88 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_88 = true;

	private uScript_Wait logic_uScript_Wait_uScript_Wait_91 = new uScript_Wait();

	private float logic_uScript_Wait_seconds_91;

	private bool logic_uScript_Wait_repeat_91;

	private bool logic_uScript_Wait_Waited_91 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_92 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_92;

	private bool logic_uScriptCon_CompareBool_True_92 = true;

	private bool logic_uScriptCon_CompareBool_False_92 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_94 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_94;

	private bool logic_uScriptCon_CompareBool_True_94 = true;

	private bool logic_uScriptCon_CompareBool_False_94 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_96 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_96;

	private bool logic_uScriptAct_SetBool_Out_96 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_96 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_96 = true;

	private uScript_HideArrow logic_uScript_HideArrow_uScript_HideArrow_99 = new uScript_HideArrow();

	private bool logic_uScript_HideArrow_Out_99 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_100 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_100 = true;

	private uScript_AccessListBlock logic_uScript_AccessListBlock_uScript_AccessListBlock_101 = new uScript_AccessListBlock();

	private TankBlock[] logic_uScript_AccessListBlock_blockList_101 = new TankBlock[0];

	private int logic_uScript_AccessListBlock_index_101;

	private TankBlock logic_uScript_AccessListBlock_value_101;

	private bool logic_uScript_AccessListBlock_Out_101 = true;

	private uScript_PointArrowAtVisible logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_105 = new uScript_PointArrowAtVisible();

	private object logic_uScript_PointArrowAtVisible_targetObject_105 = "";

	private float logic_uScript_PointArrowAtVisible_timeToShowFor_105 = -1f;

	private Vector3 logic_uScript_PointArrowAtVisible_offset_105 = new Vector3(0f, 0f, 0f);

	private bool logic_uScript_PointArrowAtVisible_Out_105 = true;

	private uScript_AccessListBlock logic_uScript_AccessListBlock_uScript_AccessListBlock_111 = new uScript_AccessListBlock();

	private TankBlock[] logic_uScript_AccessListBlock_blockList_111 = new TankBlock[0];

	private int logic_uScript_AccessListBlock_index_111;

	private TankBlock logic_uScript_AccessListBlock_value_111;

	private bool logic_uScript_AccessListBlock_Out_111 = true;

	private uScript_DoesTechHaveBlockAtPosition logic_uScript_DoesTechHaveBlockAtPosition_uScript_DoesTechHaveBlockAtPosition_112 = new uScript_DoesTechHaveBlockAtPosition();

	private Tank logic_uScript_DoesTechHaveBlockAtPosition_tech_112;

	private BlockTypes logic_uScript_DoesTechHaveBlockAtPosition_blockType_112;

	private Vector3 logic_uScript_DoesTechHaveBlockAtPosition_localPosition_112 = new Vector3(2f, 0f, 2f);

	private bool logic_uScript_DoesTechHaveBlockAtPosition_True_112 = true;

	private bool logic_uScript_DoesTechHaveBlockAtPosition_False_112 = true;

	private uScriptCon_ManualSwitch logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_113 = new uScriptCon_ManualSwitch();

	private int logic_uScriptCon_ManualSwitch_CurrentOutput_113;

	private uScriptCon_ManualSwitch logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_115 = new uScriptCon_ManualSwitch();

	private int logic_uScriptCon_ManualSwitch_CurrentOutput_115;

	private uScript_BlockAttachedToTech logic_uScript_BlockAttachedToTech_uScript_BlockAttachedToTech_116 = new uScript_BlockAttachedToTech();

	private Tank logic_uScript_BlockAttachedToTech_tech_116;

	private TankBlock logic_uScript_BlockAttachedToTech_block_116;

	private bool logic_uScript_BlockAttachedToTech_True_116 = true;

	private bool logic_uScript_BlockAttachedToTech_False_116 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_117 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_117 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_118 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_118;

	private bool logic_uScriptAct_SetBool_Out_118 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_118 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_118 = true;

	private uScript_BlockAttachedToTech logic_uScript_BlockAttachedToTech_uScript_BlockAttachedToTech_122 = new uScript_BlockAttachedToTech();

	private Tank logic_uScript_BlockAttachedToTech_tech_122;

	private TankBlock logic_uScript_BlockAttachedToTech_block_122;

	private bool logic_uScript_BlockAttachedToTech_True_122 = true;

	private bool logic_uScript_BlockAttachedToTech_False_122 = true;

	private uScript_PointArrowAtVisible logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_126 = new uScript_PointArrowAtVisible();

	private object logic_uScript_PointArrowAtVisible_targetObject_126 = "";

	private float logic_uScript_PointArrowAtVisible_timeToShowFor_126 = -1f;

	private Vector3 logic_uScript_PointArrowAtVisible_offset_126 = new Vector3(0f, 0f, 0f);

	private bool logic_uScript_PointArrowAtVisible_Out_126 = true;

	private uScript_IsPlayerInteractingWithBlock logic_uScript_IsPlayerInteractingWithBlock_uScript_IsPlayerInteractingWithBlock_130 = new uScript_IsPlayerInteractingWithBlock();

	private TankBlock logic_uScript_IsPlayerInteractingWithBlock_block_130;

	private bool logic_uScript_IsPlayerInteractingWithBlock_Out_130 = true;

	private bool logic_uScript_IsPlayerInteractingWithBlock_Interacted_130 = true;

	private bool logic_uScript_IsPlayerInteractingWithBlock_NotInteracted_130 = true;

	private bool logic_uScript_IsPlayerInteractingWithBlock_Dragging_130 = true;

	private bool logic_uScript_IsPlayerInteractingWithBlock_NotDragging_130 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_131 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_131;

	private bool logic_uScriptAct_SetBool_Out_131 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_131 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_131 = true;

	private uScript_SpawnGhostBlocks logic_uScript_SpawnGhostBlocks_uScript_SpawnGhostBlocks_132 = new uScript_SpawnGhostBlocks();

	private GhostBlockSpawnData[] logic_uScript_SpawnGhostBlocks_ghostBlockData_132 = new GhostBlockSpawnData[0];

	private GameObject logic_uScript_SpawnGhostBlocks_ownerNode_132;

	private Tank logic_uScript_SpawnGhostBlocks_targetTech_132;

	private TankBlock[] logic_uScript_SpawnGhostBlocks_Return_132;

	private bool logic_uScript_SpawnGhostBlocks_OnAlreadySpawned_132 = true;

	private bool logic_uScript_SpawnGhostBlocks_OnSpawned_132 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_133 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_133;

	private bool logic_uScriptAct_SetBool_Out_133 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_133 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_133 = true;

	private uScriptCon_CompareInt logic_uScriptCon_CompareInt_uScriptCon_CompareInt_134 = new uScriptCon_CompareInt();

	private int logic_uScriptCon_CompareInt_A_134;

	private int logic_uScriptCon_CompareInt_B_134 = 4;

	private bool logic_uScriptCon_CompareInt_GreaterThan_134 = true;

	private bool logic_uScriptCon_CompareInt_GreaterThanOrEqualTo_134 = true;

	private bool logic_uScriptCon_CompareInt_EqualTo_134 = true;

	private bool logic_uScriptCon_CompareInt_NotEqualTo_134 = true;

	private bool logic_uScriptCon_CompareInt_LessThanOrEqualTo_134 = true;

	private bool logic_uScriptCon_CompareInt_LessThan_134 = true;

	private uScript_SpawnGhostBlocks logic_uScript_SpawnGhostBlocks_uScript_SpawnGhostBlocks_135 = new uScript_SpawnGhostBlocks();

	private GhostBlockSpawnData[] logic_uScript_SpawnGhostBlocks_ghostBlockData_135 = new GhostBlockSpawnData[0];

	private GameObject logic_uScript_SpawnGhostBlocks_ownerNode_135;

	private Tank logic_uScript_SpawnGhostBlocks_targetTech_135;

	private TankBlock[] logic_uScript_SpawnGhostBlocks_Return_135;

	private bool logic_uScript_SpawnGhostBlocks_OnAlreadySpawned_135 = true;

	private bool logic_uScript_SpawnGhostBlocks_OnSpawned_135 = true;

	private uScript_IsPlayerInteractingWithBlock logic_uScript_IsPlayerInteractingWithBlock_uScript_IsPlayerInteractingWithBlock_136 = new uScript_IsPlayerInteractingWithBlock();

	private TankBlock logic_uScript_IsPlayerInteractingWithBlock_block_136;

	private bool logic_uScript_IsPlayerInteractingWithBlock_Out_136 = true;

	private bool logic_uScript_IsPlayerInteractingWithBlock_Interacted_136 = true;

	private bool logic_uScript_IsPlayerInteractingWithBlock_NotInteracted_136 = true;

	private bool logic_uScript_IsPlayerInteractingWithBlock_Dragging_136 = true;

	private bool logic_uScript_IsPlayerInteractingWithBlock_NotDragging_136 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_138 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_138;

	private bool logic_uScriptCon_CompareBool_True_138 = true;

	private bool logic_uScriptCon_CompareBool_False_138 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_140 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_140;

	private bool logic_uScriptCon_CompareBool_True_140 = true;

	private bool logic_uScriptCon_CompareBool_False_140 = true;

	private uScript_RemoveAllGhostBlocksOnTech logic_uScript_RemoveAllGhostBlocksOnTech_uScript_RemoveAllGhostBlocksOnTech_141 = new uScript_RemoveAllGhostBlocksOnTech();

	private Tank logic_uScript_RemoveAllGhostBlocksOnTech_targetTech_141;

	private bool logic_uScript_RemoveAllGhostBlocksOnTech_Out_141 = true;

	private uScript_DoesTechHaveBlockAtPosition logic_uScript_DoesTechHaveBlockAtPosition_uScript_DoesTechHaveBlockAtPosition_142 = new uScript_DoesTechHaveBlockAtPosition();

	private Tank logic_uScript_DoesTechHaveBlockAtPosition_tech_142;

	private BlockTypes logic_uScript_DoesTechHaveBlockAtPosition_blockType_142;

	private Vector3 logic_uScript_DoesTechHaveBlockAtPosition_localPosition_142 = new Vector3(2f, 0f, 3f);

	private bool logic_uScript_DoesTechHaveBlockAtPosition_True_142 = true;

	private bool logic_uScript_DoesTechHaveBlockAtPosition_False_142 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_148 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_148 = true;

	private uScriptAct_AddInt_v2 logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_149 = new uScriptAct_AddInt_v2();

	private int logic_uScriptAct_AddInt_v2_A_149;

	private int logic_uScriptAct_AddInt_v2_B_149 = 1;

	private int logic_uScriptAct_AddInt_v2_IntResult_149;

	private float logic_uScriptAct_AddInt_v2_FloatResult_149;

	private bool logic_uScriptAct_AddInt_v2_Out_149 = true;

	private uScript_PointArrowAtVisible logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_150 = new uScript_PointArrowAtVisible();

	private object logic_uScript_PointArrowAtVisible_targetObject_150 = "";

	private float logic_uScript_PointArrowAtVisible_timeToShowFor_150 = -1f;

	private Vector3 logic_uScript_PointArrowAtVisible_offset_150 = new Vector3(0f, 0f, 0f);

	private bool logic_uScript_PointArrowAtVisible_Out_150 = true;

	private uScript_PointArrowAtVisible logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_156 = new uScript_PointArrowAtVisible();

	private object logic_uScript_PointArrowAtVisible_targetObject_156 = "";

	private float logic_uScript_PointArrowAtVisible_timeToShowFor_156 = -1f;

	private Vector3 logic_uScript_PointArrowAtVisible_offset_156 = new Vector3(0f, 0f, 0f);

	private bool logic_uScript_PointArrowAtVisible_Out_156 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_159 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_159;

	private bool logic_uScriptCon_CompareBool_True_159 = true;

	private bool logic_uScriptCon_CompareBool_False_159 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_160 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_160;

	private bool logic_uScriptCon_CompareBool_True_160 = true;

	private bool logic_uScriptCon_CompareBool_False_160 = true;

	private uScript_GetAndCheckBlocks logic_uScript_GetAndCheckBlocks_uScript_GetAndCheckBlocks_164 = new uScript_GetAndCheckBlocks();

	private SpawnBlockData[] logic_uScript_GetAndCheckBlocks_blockData_164 = new SpawnBlockData[0];

	private GameObject logic_uScript_GetAndCheckBlocks_ownerNode_164;

	private TankBlock[] logic_uScript_GetAndCheckBlocks_blocks_164 = new TankBlock[0];

	private bool logic_uScript_GetAndCheckBlocks_AllAlive_164 = true;

	private bool logic_uScript_GetAndCheckBlocks_SomeAlive_164 = true;

	private bool logic_uScript_GetAndCheckBlocks_AllDead_164 = true;

	private bool logic_uScript_GetAndCheckBlocks_WaitingToSpawn_164 = true;

	private uScript_SpawnBlocksFromData logic_uScript_SpawnBlocksFromData_uScript_SpawnBlocksFromData_167 = new uScript_SpawnBlocksFromData();

	private SpawnBlockData[] logic_uScript_SpawnBlocksFromData_blockData_167 = new SpawnBlockData[0];

	private GameObject logic_uScript_SpawnBlocksFromData_ownerNode_167;

	private bool logic_uScript_SpawnBlocksFromData_Out_167 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_169 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_169;

	private bool logic_uScriptAct_SetBool_Out_169 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_169 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_169 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_171 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_171;

	private bool logic_uScriptAct_SetBool_Out_171 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_171 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_171 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_172 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_172;

	private bool logic_uScriptCon_CompareBool_True_172 = true;

	private bool logic_uScriptCon_CompareBool_False_172 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_176 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_176;

	private bool logic_uScriptAct_SetBool_Out_176 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_176 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_176 = true;

	private SubGraph_Crafting_Tutorial_AttachBlockToBase logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_SubGraph_Crafting_Tutorial_AttachBlockToBase_177 = new SubGraph_Crafting_Tutorial_AttachBlockToBase();

	private TankBlock logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_Block_177;

	private Tank logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_CraftingBaseTech_177;

	private GhostBlockSpawnData[] logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_ghostBlockData_177 = new GhostBlockSpawnData[0];

	private BlockTypes logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_BlockType_177 = BlockTypes.GSOFabricator_322;

	private Vector3 logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_BlockPosition_177 = new Vector3(2f, 0f, 4f);

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_181 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_181;

	private bool logic_uScriptCon_CompareBool_True_181 = true;

	private bool logic_uScriptCon_CompareBool_False_181 = true;

	private uScript_LockPlayerInput logic_uScript_LockPlayerInput_uScript_LockPlayerInput_182 = new uScript_LockPlayerInput();

	private bool logic_uScript_LockPlayerInput_lockInput_182 = true;

	private bool logic_uScript_LockPlayerInput_includeCamera_182 = true;

	private bool logic_uScript_LockPlayerInput_Out_182 = true;

	private uScript_PointArrowAtVisible logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_184 = new uScript_PointArrowAtVisible();

	private object logic_uScript_PointArrowAtVisible_targetObject_184 = "";

	private float logic_uScript_PointArrowAtVisible_timeToShowFor_184 = -1f;

	private Vector3 logic_uScript_PointArrowAtVisible_offset_184 = new Vector3(0f, 0f, 0f);

	private bool logic_uScript_PointArrowAtVisible_Out_184 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_185 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_185 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_185 = true;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_185 = true;

	private uScript_LockTechInteraction logic_uScript_LockTechInteraction_uScript_LockTechInteraction_186 = new uScript_LockTechInteraction();

	private Tank logic_uScript_LockTechInteraction_tech_186;

	private BlockTypes[] logic_uScript_LockTechInteraction_excludedBlocks_186 = new BlockTypes[0];

	private TankBlock[] logic_uScript_LockTechInteraction_excludedUniqueBlocks_186 = new TankBlock[0];

	private bool logic_uScript_LockTechInteraction_Out_186 = true;

	private uScript_LockPause logic_uScript_LockPause_uScript_LockPause_188 = new uScript_LockPause();

	private bool logic_uScript_LockPause_lockPause_188;

	private ManPauseGame.DisablePauseReason logic_uScript_LockPause_disabledReason_188 = ManPauseGame.DisablePauseReason.Tutorial;

	private bool logic_uScript_LockPause_Out_188 = true;

	private uScript_LockTechInteraction logic_uScript_LockTechInteraction_uScript_LockTechInteraction_189 = new uScript_LockTechInteraction();

	private Tank logic_uScript_LockTechInteraction_tech_189;

	private BlockTypes[] logic_uScript_LockTechInteraction_excludedBlocks_189 = new BlockTypes[1] { BlockTypes.GSOFabricator_322 };

	private TankBlock[] logic_uScript_LockTechInteraction_excludedUniqueBlocks_189 = new TankBlock[0];

	private bool logic_uScript_LockTechInteraction_Out_189 = true;

	private uScript_HideArrow logic_uScript_HideArrow_uScript_HideArrow_191 = new uScript_HideArrow();

	private bool logic_uScript_HideArrow_Out_191 = true;

	private uScript_LockTechInteraction logic_uScript_LockTechInteraction_uScript_LockTechInteraction_192 = new uScript_LockTechInteraction();

	private Tank logic_uScript_LockTechInteraction_tech_192;

	private BlockTypes[] logic_uScript_LockTechInteraction_excludedBlocks_192 = new BlockTypes[1] { BlockTypes.GSOFabricator_322 };

	private TankBlock[] logic_uScript_LockTechInteraction_excludedUniqueBlocks_192 = new TankBlock[0];

	private bool logic_uScript_LockTechInteraction_Out_192 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_194 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_194;

	private bool logic_uScriptCon_CompareBool_True_194 = true;

	private bool logic_uScriptCon_CompareBool_False_194 = true;

	private uScript_LockPause logic_uScript_LockPause_uScript_LockPause_196 = new uScript_LockPause();

	private bool logic_uScript_LockPause_lockPause_196 = true;

	private ManPauseGame.DisablePauseReason logic_uScript_LockPause_disabledReason_196 = ManPauseGame.DisablePauseReason.Tutorial;

	private bool logic_uScript_LockPause_Out_196 = true;

	private uScript_LockPlayerInput logic_uScript_LockPlayerInput_uScript_LockPlayerInput_197 = new uScript_LockPlayerInput();

	private bool logic_uScript_LockPlayerInput_lockInput_197;

	private bool logic_uScript_LockPlayerInput_includeCamera_197;

	private bool logic_uScript_LockPlayerInput_Out_197 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_198 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_198;

	private bool logic_uScriptAct_SetBool_Out_198 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_198 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_198 = true;

	private SubGraph_SaveLoadInt logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_201 = new SubGraph_SaveLoadInt();

	private int logic_SubGraph_SaveLoadInt_restartValue_201 = 1;

	private int logic_SubGraph_SaveLoadInt_integer_201;

	private object logic_SubGraph_SaveLoadInt_intAsVariable_201 = "";

	private string logic_SubGraph_SaveLoadInt_uniqueID_201 = "Stage";

	private SubGraph_LoadObjectiveStates logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_203 = new SubGraph_LoadObjectiveStates();

	private int logic_SubGraph_LoadObjectiveStates_currentObjective_203;

	private uScriptCon_ManualSwitch logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_205 = new uScriptCon_ManualSwitch();

	private int logic_uScriptCon_ManualSwitch_CurrentOutput_205;

	private uScriptAct_AddInt_v2 logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_207 = new uScriptAct_AddInt_v2();

	private int logic_uScriptAct_AddInt_v2_A_207;

	private int logic_uScriptAct_AddInt_v2_B_207 = 1;

	private int logic_uScriptAct_AddInt_v2_IntResult_207;

	private float logic_uScriptAct_AddInt_v2_FloatResult_207;

	private bool logic_uScriptAct_AddInt_v2_Out_207 = true;

	private uScriptAct_AddInt_v2 logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_209 = new uScriptAct_AddInt_v2();

	private int logic_uScriptAct_AddInt_v2_A_209;

	private int logic_uScriptAct_AddInt_v2_B_209 = 1;

	private int logic_uScriptAct_AddInt_v2_IntResult_209;

	private float logic_uScriptAct_AddInt_v2_FloatResult_209;

	private bool logic_uScriptAct_AddInt_v2_Out_209 = true;

	private uScriptAct_AddInt_v2 logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_212 = new uScriptAct_AddInt_v2();

	private int logic_uScriptAct_AddInt_v2_A_212;

	private int logic_uScriptAct_AddInt_v2_B_212 = 1;

	private int logic_uScriptAct_AddInt_v2_IntResult_212;

	private float logic_uScriptAct_AddInt_v2_FloatResult_212;

	private bool logic_uScriptAct_AddInt_v2_Out_212 = true;

	private SubGraph_SaveLoadInt logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_214 = new SubGraph_SaveLoadInt();

	private int logic_SubGraph_SaveLoadInt_restartValue_214 = 1;

	private int logic_SubGraph_SaveLoadInt_integer_214;

	private object logic_SubGraph_SaveLoadInt_intAsVariable_214 = "";

	private string logic_SubGraph_SaveLoadInt_uniqueID_214 = "Stage2Steps";

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_216 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_216;

	private bool logic_uScriptAct_SetBool_Out_216 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_216 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_216 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_217 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_217;

	private bool logic_uScriptCon_CompareBool_True_217 = true;

	private bool logic_uScriptCon_CompareBool_False_217 = true;

	private uScript_LockTechInteraction logic_uScript_LockTechInteraction_uScript_LockTechInteraction_223 = new uScript_LockTechInteraction();

	private Tank logic_uScript_LockTechInteraction_tech_223;

	private BlockTypes[] logic_uScript_LockTechInteraction_excludedBlocks_223 = new BlockTypes[0];

	private TankBlock[] logic_uScript_LockTechInteraction_excludedUniqueBlocks_223 = new TankBlock[0];

	private bool logic_uScript_LockTechInteraction_Out_223 = true;

	private SubGraph_CompleteObjectiveStage logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_226 = new SubGraph_CompleteObjectiveStage();

	private int logic_SubGraph_CompleteObjectiveStage_objectiveStage_226;

	private bool logic_SubGraph_CompleteObjectiveStage_isFinalObjective_226;

	private SubGraph_CompleteObjectiveStage logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_228 = new SubGraph_CompleteObjectiveStage();

	private int logic_SubGraph_CompleteObjectiveStage_objectiveStage_228;

	private bool logic_SubGraph_CompleteObjectiveStage_isFinalObjective_228;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_231 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_231;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_231;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_231;

	private bool logic_uScript_AddMessage_Out_231 = true;

	private bool logic_uScript_AddMessage_Shown_231 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_234 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_234;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_234;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_234;

	private bool logic_uScript_AddMessage_Out_234 = true;

	private bool logic_uScript_AddMessage_Shown_234 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_236 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_236;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_236;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_236;

	private bool logic_uScript_AddMessage_Out_236 = true;

	private bool logic_uScript_AddMessage_Shown_236 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_238 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_238;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_238;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_238;

	private bool logic_uScript_AddMessage_Out_238 = true;

	private bool logic_uScript_AddMessage_Shown_238 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_243 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_243;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_243;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_243;

	private bool logic_uScript_AddMessage_Out_243 = true;

	private bool logic_uScript_AddMessage_Shown_243 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_245 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_245;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_245;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_245;

	private bool logic_uScript_AddMessage_Out_245 = true;

	private bool logic_uScript_AddMessage_Shown_245 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_249 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_249;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_249;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_249;

	private bool logic_uScript_AddMessage_Out_249 = true;

	private bool logic_uScript_AddMessage_Shown_249 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_252 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_252;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_252;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_252;

	private bool logic_uScript_AddMessage_Out_252 = true;

	private bool logic_uScript_AddMessage_Shown_252 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_254 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_254;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_254;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_254;

	private bool logic_uScript_AddMessage_Out_254 = true;

	private bool logic_uScript_AddMessage_Shown_254 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_258 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_258;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_258;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_258;

	private bool logic_uScript_AddMessage_Out_258 = true;

	private bool logic_uScript_AddMessage_Shown_258 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_262 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_262;

	private bool logic_uScriptCon_CompareBool_True_262 = true;

	private bool logic_uScriptCon_CompareBool_False_262 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_263 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_263;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_263;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_263;

	private bool logic_uScript_AddMessage_Out_263 = true;

	private bool logic_uScript_AddMessage_Shown_263 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_264 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_264;

	private bool logic_uScriptAct_SetBool_Out_264 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_264 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_264 = true;

	private SubGraph_Crafting_Tutorial_Finish logic_SubGraph_Crafting_Tutorial_Finish_SubGraph_Crafting_Tutorial_Finish_282 = new SubGraph_Crafting_Tutorial_Finish();

	private Tank logic_SubGraph_Crafting_Tutorial_Finish_craftingBaseTech_282;

	private Tank logic_SubGraph_Crafting_Tutorial_Finish_NPCTech_282;

	private ExternalBehaviorTree logic_SubGraph_Crafting_Tutorial_Finish_NPCFlyAwayAI_282;

	private Transform logic_SubGraph_Crafting_Tutorial_Finish_NPCDespawnParticleEffect_282;

	private uScript_SetEncounterTarget logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_283 = new uScript_SetEncounterTarget();

	private GameObject logic_uScript_SetEncounterTarget_owner_283;

	private object logic_uScript_SetEncounterTarget_visibleObject_283 = "";

	private bool logic_uScript_SetEncounterTarget_Out_283 = true;

	private uScript_CraftingUIHighlightCraftButton logic_uScript_CraftingUIHighlightCraftButton_uScript_CraftingUIHighlightCraftButton_287 = new uScript_CraftingUIHighlightCraftButton();

	private ManHUD.HUDElementType logic_uScript_CraftingUIHighlightCraftButton_targetMenuType_287 = ManHUD.HUDElementType.BlockRecipeSelect;

	private bool logic_uScript_CraftingUIHighlightCraftButton_Out_287 = true;

	private bool logic_uScript_CraftingUIHighlightCraftButton_Waiting_287 = true;

	private bool logic_uScript_CraftingUIHighlightCraftButton_Selected_287 = true;

	private uScript_CraftingUIHighlightItem logic_uScript_CraftingUIHighlightItem_uScript_CraftingUIHighlightItem_288 = new uScript_CraftingUIHighlightItem();

	private ManHUD.HUDElementType logic_uScript_CraftingUIHighlightItem_targetMenuType_288 = ManHUD.HUDElementType.BlockRecipeSelect;

	private ItemTypeInfo logic_uScript_CraftingUIHighlightItem_itemToHighlight_288;

	private bool logic_uScript_CraftingUIHighlightItem_Out_288 = true;

	private bool logic_uScript_CraftingUIHighlightItem_Waiting_288 = true;

	private bool logic_uScript_CraftingUIHighlightItem_Selected_288 = true;

	private uScript_SpawnResourceListOnHolder logic_uScript_SpawnResourceListOnHolder_uScript_SpawnResourceListOnHolder_289 = new uScript_SpawnResourceListOnHolder();

	private Tank logic_uScript_SpawnResourceListOnHolder_tech_289;

	private ChunkTypes[] logic_uScript_SpawnResourceListOnHolder_chunks_289 = new ChunkTypes[0];

	private BlockTypes logic_uScript_SpawnResourceListOnHolder_blockType_289 = BlockTypes.GSOReceiver_111;

	private bool logic_uScript_SpawnResourceListOnHolder_Out_289 = true;

	private uScript_LockPause logic_uScript_LockPause_uScript_LockPause_292 = new uScript_LockPause();

	private bool logic_uScript_LockPause_lockPause_292;

	private ManPauseGame.DisablePauseReason logic_uScript_LockPause_disabledReason_292 = ManPauseGame.DisablePauseReason.Tutorial;

	private bool logic_uScript_LockPause_Out_292 = true;

	private uScript_LockPlayerInput logic_uScript_LockPlayerInput_uScript_LockPlayerInput_293 = new uScript_LockPlayerInput();

	private bool logic_uScript_LockPlayerInput_lockInput_293;

	private bool logic_uScript_LockPlayerInput_includeCamera_293;

	private bool logic_uScript_LockPlayerInput_Out_293 = true;

	private uScript_DisableClosingCraftingUIWhenTooFarAway logic_uScript_DisableClosingCraftingUIWhenTooFarAway_uScript_DisableClosingCraftingUIWhenTooFarAway_294 = new uScript_DisableClosingCraftingUIWhenTooFarAway();

	private TankBlock logic_uScript_DisableClosingCraftingUIWhenTooFarAway_craftingBlock_294;

	private bool logic_uScript_DisableClosingCraftingUIWhenTooFarAway_Out_294 = true;

	private uScript_DisableClosingCraftingUIWhenTooFarAway logic_uScript_DisableClosingCraftingUIWhenTooFarAway_uScript_DisableClosingCraftingUIWhenTooFarAway_297 = new uScript_DisableClosingCraftingUIWhenTooFarAway();

	private TankBlock logic_uScript_DisableClosingCraftingUIWhenTooFarAway_craftingBlock_297;

	private bool logic_uScript_DisableClosingCraftingUIWhenTooFarAway_Out_297 = true;

	private uScript_DisableClosingCraftingUIWhenTooFarAway logic_uScript_DisableClosingCraftingUIWhenTooFarAway_uScript_DisableClosingCraftingUIWhenTooFarAway_298 = new uScript_DisableClosingCraftingUIWhenTooFarAway();

	private TankBlock logic_uScript_DisableClosingCraftingUIWhenTooFarAway_craftingBlock_298;

	private bool logic_uScript_DisableClosingCraftingUIWhenTooFarAway_Out_298 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_300 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_300;

	private bool logic_uScriptCon_CompareBool_True_300 = true;

	private bool logic_uScriptCon_CompareBool_False_300 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_301 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_301;

	private bool logic_uScriptCon_CompareBool_True_301 = true;

	private bool logic_uScriptCon_CompareBool_False_301 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_304 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_304;

	private bool logic_uScriptAct_SetBool_Out_304 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_304 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_304 = true;

	private uScript_IsHUDElementLinkedToBlock logic_uScript_IsHUDElementLinkedToBlock_uScript_IsHUDElementLinkedToBlock_307 = new uScript_IsHUDElementLinkedToBlock();

	private ManHUD.HUDElementType logic_uScript_IsHUDElementLinkedToBlock_hudElement_307;

	private TankBlock logic_uScript_IsHUDElementLinkedToBlock_targetBlock_307;

	private bool logic_uScript_IsHUDElementLinkedToBlock_True_307 = true;

	private bool logic_uScript_IsHUDElementLinkedToBlock_False_307 = true;

	private uScript_HideHUDElement logic_uScript_HideHUDElement_uScript_HideHUDElement_309 = new uScript_HideHUDElement();

	private ManHUD.HUDElementType logic_uScript_HideHUDElement_hudElement_309;

	private bool logic_uScript_HideHUDElement_Out_309 = true;

	private SubGraph_AddMessageWithPadSupport logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_311 = new SubGraph_AddMessageWithPadSupport();

	private uScript_AddMessage.MessageData logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboard_311;

	private uScript_AddMessage.MessageData logic_SubGraph_AddMessageWithPadSupport_messageControlPad_311;

	private uScript_AddMessage.MessageSpeaker logic_SubGraph_AddMessageWithPadSupport_speaker_311;

	private ManOnScreenMessages.OnScreenMessage logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboardReturn_311;

	private ManOnScreenMessages.OnScreenMessage logic_SubGraph_AddMessageWithPadSupport_messageControlPadReturn_311;

	private SubGraph_AddMessageWithPadSupport logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_318 = new SubGraph_AddMessageWithPadSupport();

	private uScript_AddMessage.MessageData logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboard_318;

	private uScript_AddMessage.MessageData logic_SubGraph_AddMessageWithPadSupport_messageControlPad_318;

	private uScript_AddMessage.MessageSpeaker logic_SubGraph_AddMessageWithPadSupport_speaker_318;

	private ManOnScreenMessages.OnScreenMessage logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboardReturn_318;

	private ManOnScreenMessages.OnScreenMessage logic_SubGraph_AddMessageWithPadSupport_messageControlPadReturn_318;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_319 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_319 = "";

	private bool logic_uScript_EnableGlow_enable_319;

	private bool logic_uScript_EnableGlow_Out_319 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_321 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_321 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_322 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_322;

	private bool logic_uScriptCon_CompareBool_True_322 = true;

	private bool logic_uScriptCon_CompareBool_False_322 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_325 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_325 = "";

	private bool logic_uScript_EnableGlow_enable_325;

	private bool logic_uScript_EnableGlow_Out_325 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_326 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_326 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_328 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_328;

	private bool logic_uScriptCon_CompareBool_True_328 = true;

	private bool logic_uScriptCon_CompareBool_False_328 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_329 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_329 = "";

	private bool logic_uScript_EnableGlow_enable_329 = true;

	private bool logic_uScript_EnableGlow_Out_329 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_331 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_331 = "";

	private bool logic_uScript_EnableGlow_enable_331 = true;

	private bool logic_uScript_EnableGlow_Out_331 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_333 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_333 = "";

	private bool logic_uScript_EnableGlow_enable_333;

	private bool logic_uScript_EnableGlow_Out_333 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_336 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_336 = "";

	private bool logic_uScript_EnableGlow_enable_336;

	private bool logic_uScript_EnableGlow_Out_336 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_338 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_338 = "";

	private bool logic_uScript_EnableGlow_enable_338;

	private bool logic_uScript_EnableGlow_Out_338 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_339 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_339 = "";

	private bool logic_uScript_EnableGlow_enable_339;

	private bool logic_uScript_EnableGlow_Out_339 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_342 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_342 = "";

	private bool logic_uScript_EnableGlow_enable_342 = true;

	private bool logic_uScript_EnableGlow_Out_342 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_344 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_344 = "";

	private bool logic_uScript_EnableGlow_enable_344 = true;

	private bool logic_uScript_EnableGlow_Out_344 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_345 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_345 = "";

	private bool logic_uScript_EnableGlow_enable_345 = true;

	private bool logic_uScript_EnableGlow_Out_345 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_348 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_348 = "";

	private bool logic_uScript_EnableGlow_enable_348;

	private bool logic_uScript_EnableGlow_Out_348 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_349 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_349 = "";

	private bool logic_uScript_EnableGlow_enable_349;

	private bool logic_uScript_EnableGlow_Out_349 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_352 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_352 = "";

	private bool logic_uScript_EnableGlow_enable_352;

	private bool logic_uScript_EnableGlow_Out_352 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_354 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_354 = "";

	private bool logic_uScript_EnableGlow_enable_354;

	private bool logic_uScript_EnableGlow_Out_354 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_356 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_356 = "";

	private bool logic_uScript_EnableGlow_enable_356;

	private bool logic_uScript_EnableGlow_Out_356 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_357 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_357;

	private bool logic_uScriptCon_CompareBool_True_357 = true;

	private bool logic_uScriptCon_CompareBool_False_357 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_358 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_358 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_359 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_359 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_363 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_363 = "";

	private bool logic_uScript_EnableGlow_enable_363;

	private bool logic_uScript_EnableGlow_Out_363 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_364 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_364;

	private bool logic_uScriptCon_CompareBool_True_364 = true;

	private bool logic_uScriptCon_CompareBool_False_364 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_366 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_366 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_368 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_368;

	private bool logic_uScriptCon_CompareBool_True_368 = true;

	private bool logic_uScriptCon_CompareBool_False_368 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_369 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_369 = "";

	private bool logic_uScript_EnableGlow_enable_369;

	private bool logic_uScript_EnableGlow_Out_369 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_370 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_370 = true;

	private uScript_SendAnaliticsEvent logic_uScript_SendAnaliticsEvent_uScript_SendAnaliticsEvent_371 = new uScript_SendAnaliticsEvent();

	private string logic_uScript_SendAnaliticsEvent_analiticsEvent_371 = "tutorial_complete";

	private string logic_uScript_SendAnaliticsEvent_parameterName_371 = "crafting_tutorial";

	private object logic_uScript_SendAnaliticsEvent_parameter_371 = "2";

	private bool logic_uScript_SendAnaliticsEvent_Out_371 = true;

	private uScript_SendAnaliticsEvent logic_uScript_SendAnaliticsEvent_uScript_SendAnaliticsEvent_372 = new uScript_SendAnaliticsEvent();

	private string logic_uScript_SendAnaliticsEvent_analiticsEvent_372 = "tutorial_start";

	private string logic_uScript_SendAnaliticsEvent_parameterName_372 = "crafting_tutorial";

	private object logic_uScript_SendAnaliticsEvent_parameter_372 = "2";

	private bool logic_uScript_SendAnaliticsEvent_Out_372 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_374 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_374;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_374 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_374 = "CraftingInProgress";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_376 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_376;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_376 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_376 = "CraftingMenuOpened";

	private uScript_LockHudGroup logic_uScript_LockHudGroup_uScript_LockHudGroup_377 = new uScript_LockHudGroup();

	private ManHUD.HUDGroup logic_uScript_LockHudGroup_group_377;

	private bool logic_uScript_LockHudGroup_locked_377 = true;

	private bool logic_uScript_LockHudGroup_Out_377 = true;

	private uScript_LockHudGroup logic_uScript_LockHudGroup_uScript_LockHudGroup_378 = new uScript_LockHudGroup();

	private ManHUD.HUDGroup logic_uScript_LockHudGroup_group_378;

	private bool logic_uScript_LockHudGroup_locked_378;

	private bool logic_uScript_LockHudGroup_Out_378 = true;

	private BlockTypes event_UnityEngine_GameObject_BlockType_9;

	private int event_UnityEngine_GameObject_BlockTypeTotal_9;

	private int event_UnityEngine_GameObject_BlockTotal_9;

	private TankBlock event_UnityEngine_GameObject_Block_9;

	private TankBlock event_UnityEngine_GameObject_CrafterBlock_9;

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
		if (null == owner_Connection_110 || !m_RegisteredForEvents)
		{
			owner_Connection_110 = parentGameObject;
		}
		if (null == owner_Connection_121 || !m_RegisteredForEvents)
		{
			owner_Connection_121 = parentGameObject;
		}
		if (null == owner_Connection_157 || !m_RegisteredForEvents)
		{
			owner_Connection_157 = parentGameObject;
		}
		if (null == owner_Connection_170 || !m_RegisteredForEvents)
		{
			owner_Connection_170 = parentGameObject;
		}
		if (null == owner_Connection_284 || !m_RegisteredForEvents)
		{
			owner_Connection_284 = parentGameObject;
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
	}

	public override void SetParent(GameObject g)
	{
		parentGameObject = g;
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_2.SetParent(g);
		logic_uScript_PausePopulation_uScript_PausePopulation_3.SetParent(g);
		logic_uScript_PausePopulation_uScript_PausePopulation_4.SetParent(g);
		logic_uScript_CompareBlockTypes_uScript_CompareBlockTypes_12.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_14.SetParent(g);
		logic_SubGraph_Crafting_Tutorial_Init_SubGraph_Crafting_Tutorial_Init_15.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_22.SetParent(g);
		logic_uScript_LockTechStacks_uScript_LockTechStacks_24.SetParent(g);
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_27.SetParent(g);
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_32.SetParent(g);
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_34.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_35.SetParent(g);
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_37.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_39.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_40.SetParent(g);
		logic_uScript_RestrictItemPickup_uScript_RestrictItemPickup_42.SetParent(g);
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_44.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_47.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_48.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_49.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_53.SetParent(g);
		logic_uScript_HideArrow_uScript_HideArrow_54.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_56.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_58.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_60.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_61.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_64.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_65.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_68.SetParent(g);
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_70.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_73.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_74.SetParent(g);
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_75.SetParent(g);
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_77.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_81.SetParent(g);
		logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_SubGraph_Crafting_Tutorial_AttachBlockToBase_84.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_87.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_88.SetParent(g);
		logic_uScript_Wait_uScript_Wait_91.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_92.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_94.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_96.SetParent(g);
		logic_uScript_HideArrow_uScript_HideArrow_99.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_100.SetParent(g);
		logic_uScript_AccessListBlock_uScript_AccessListBlock_101.SetParent(g);
		logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_105.SetParent(g);
		logic_uScript_AccessListBlock_uScript_AccessListBlock_111.SetParent(g);
		logic_uScript_DoesTechHaveBlockAtPosition_uScript_DoesTechHaveBlockAtPosition_112.SetParent(g);
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_113.SetParent(g);
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_115.SetParent(g);
		logic_uScript_BlockAttachedToTech_uScript_BlockAttachedToTech_116.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_117.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_118.SetParent(g);
		logic_uScript_BlockAttachedToTech_uScript_BlockAttachedToTech_122.SetParent(g);
		logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_126.SetParent(g);
		logic_uScript_IsPlayerInteractingWithBlock_uScript_IsPlayerInteractingWithBlock_130.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_131.SetParent(g);
		logic_uScript_SpawnGhostBlocks_uScript_SpawnGhostBlocks_132.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_133.SetParent(g);
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_134.SetParent(g);
		logic_uScript_SpawnGhostBlocks_uScript_SpawnGhostBlocks_135.SetParent(g);
		logic_uScript_IsPlayerInteractingWithBlock_uScript_IsPlayerInteractingWithBlock_136.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_138.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_140.SetParent(g);
		logic_uScript_RemoveAllGhostBlocksOnTech_uScript_RemoveAllGhostBlocksOnTech_141.SetParent(g);
		logic_uScript_DoesTechHaveBlockAtPosition_uScript_DoesTechHaveBlockAtPosition_142.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_148.SetParent(g);
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_149.SetParent(g);
		logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_150.SetParent(g);
		logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_156.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_159.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_160.SetParent(g);
		logic_uScript_GetAndCheckBlocks_uScript_GetAndCheckBlocks_164.SetParent(g);
		logic_uScript_SpawnBlocksFromData_uScript_SpawnBlocksFromData_167.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_169.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_171.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_172.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_176.SetParent(g);
		logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_SubGraph_Crafting_Tutorial_AttachBlockToBase_177.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_181.SetParent(g);
		logic_uScript_LockPlayerInput_uScript_LockPlayerInput_182.SetParent(g);
		logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_184.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_185.SetParent(g);
		logic_uScript_LockTechInteraction_uScript_LockTechInteraction_186.SetParent(g);
		logic_uScript_LockPause_uScript_LockPause_188.SetParent(g);
		logic_uScript_LockTechInteraction_uScript_LockTechInteraction_189.SetParent(g);
		logic_uScript_HideArrow_uScript_HideArrow_191.SetParent(g);
		logic_uScript_LockTechInteraction_uScript_LockTechInteraction_192.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_194.SetParent(g);
		logic_uScript_LockPause_uScript_LockPause_196.SetParent(g);
		logic_uScript_LockPlayerInput_uScript_LockPlayerInput_197.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_198.SetParent(g);
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_201.SetParent(g);
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_203.SetParent(g);
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_205.SetParent(g);
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_207.SetParent(g);
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_209.SetParent(g);
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_212.SetParent(g);
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_214.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_216.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_217.SetParent(g);
		logic_uScript_LockTechInteraction_uScript_LockTechInteraction_223.SetParent(g);
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_226.SetParent(g);
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_228.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_231.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_234.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_236.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_238.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_243.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_245.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_249.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_252.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_254.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_258.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_262.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_263.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_264.SetParent(g);
		logic_SubGraph_Crafting_Tutorial_Finish_SubGraph_Crafting_Tutorial_Finish_282.SetParent(g);
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_283.SetParent(g);
		logic_uScript_CraftingUIHighlightCraftButton_uScript_CraftingUIHighlightCraftButton_287.SetParent(g);
		logic_uScript_CraftingUIHighlightItem_uScript_CraftingUIHighlightItem_288.SetParent(g);
		logic_uScript_SpawnResourceListOnHolder_uScript_SpawnResourceListOnHolder_289.SetParent(g);
		logic_uScript_LockPause_uScript_LockPause_292.SetParent(g);
		logic_uScript_LockPlayerInput_uScript_LockPlayerInput_293.SetParent(g);
		logic_uScript_DisableClosingCraftingUIWhenTooFarAway_uScript_DisableClosingCraftingUIWhenTooFarAway_294.SetParent(g);
		logic_uScript_DisableClosingCraftingUIWhenTooFarAway_uScript_DisableClosingCraftingUIWhenTooFarAway_297.SetParent(g);
		logic_uScript_DisableClosingCraftingUIWhenTooFarAway_uScript_DisableClosingCraftingUIWhenTooFarAway_298.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_300.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_301.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_304.SetParent(g);
		logic_uScript_IsHUDElementLinkedToBlock_uScript_IsHUDElementLinkedToBlock_307.SetParent(g);
		logic_uScript_HideHUDElement_uScript_HideHUDElement_309.SetParent(g);
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_311.SetParent(g);
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_318.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_319.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_321.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_322.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_325.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_326.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_328.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_329.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_331.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_333.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_336.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_338.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_339.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_342.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_344.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_345.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_348.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_349.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_352.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_354.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_356.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_357.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_358.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_359.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_363.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_364.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_366.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_368.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_369.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_370.SetParent(g);
		logic_uScript_SendAnaliticsEvent_uScript_SendAnaliticsEvent_371.SetParent(g);
		logic_uScript_SendAnaliticsEvent_uScript_SendAnaliticsEvent_372.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_374.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_376.SetParent(g);
		logic_uScript_LockHudGroup_uScript_LockHudGroup_377.SetParent(g);
		logic_uScript_LockHudGroup_uScript_LockHudGroup_378.SetParent(g);
		owner_Connection_1 = parentGameObject;
		owner_Connection_7 = parentGameObject;
		owner_Connection_10 = parentGameObject;
		owner_Connection_110 = parentGameObject;
		owner_Connection_121 = parentGameObject;
		owner_Connection_157 = parentGameObject;
		owner_Connection_170 = parentGameObject;
		owner_Connection_284 = parentGameObject;
	}

	public void Awake()
	{
		logic_SubGraph_Crafting_Tutorial_Init_SubGraph_Crafting_Tutorial_Init_15.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_22.Awake();
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_27.Awake();
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_32.Awake();
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_34.Awake();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_44.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_47.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_48.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_49.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_53.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_56.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_58.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_65.Awake();
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_70.Awake();
		logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_SubGraph_Crafting_Tutorial_AttachBlockToBase_84.Awake();
		logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_SubGraph_Crafting_Tutorial_AttachBlockToBase_177.Awake();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_201.Awake();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_203.Awake();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_214.Awake();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_226.Awake();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_228.Awake();
		logic_SubGraph_Crafting_Tutorial_Finish_SubGraph_Crafting_Tutorial_Finish_282.Awake();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_311.Awake();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_318.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_374.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_376.Awake();
		logic_SubGraph_Crafting_Tutorial_Init_SubGraph_Crafting_Tutorial_Init_15.Out += SubGraph_Crafting_Tutorial_Init_Out_15;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_22.Save_Out += SubGraph_SaveLoadBool_Save_Out_22;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_22.Load_Out += SubGraph_SaveLoadBool_Load_Out_22;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_22.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_22;
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_27.Out += SubGraph_Crafting_Tutorial_ManageBlock_Out_27;
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_32.Out += SubGraph_Crafting_Tutorial_ManageBlock_Out_32;
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_34.Out += SubGraph_Crafting_Tutorial_ManageBlock_Out_34;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_44.Save_Out += SubGraph_SaveLoadInt_Save_Out_44;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_44.Load_Out += SubGraph_SaveLoadInt_Load_Out_44;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_44.Restart_Out += SubGraph_SaveLoadInt_Restart_Out_44;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_47.Save_Out += SubGraph_SaveLoadBool_Save_Out_47;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_47.Load_Out += SubGraph_SaveLoadBool_Load_Out_47;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_47.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_47;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_48.Save_Out += SubGraph_SaveLoadBool_Save_Out_48;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_48.Load_Out += SubGraph_SaveLoadBool_Load_Out_48;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_48.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_48;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_49.Save_Out += SubGraph_SaveLoadBool_Save_Out_49;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_49.Load_Out += SubGraph_SaveLoadBool_Load_Out_49;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_49.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_49;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_53.Save_Out += SubGraph_SaveLoadBool_Save_Out_53;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_53.Load_Out += SubGraph_SaveLoadBool_Load_Out_53;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_53.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_53;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_56.Save_Out += SubGraph_SaveLoadBool_Save_Out_56;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_56.Load_Out += SubGraph_SaveLoadBool_Load_Out_56;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_56.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_56;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_58.Save_Out += SubGraph_SaveLoadBool_Save_Out_58;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_58.Load_Out += SubGraph_SaveLoadBool_Load_Out_58;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_58.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_58;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_65.Save_Out += SubGraph_SaveLoadBool_Save_Out_65;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_65.Load_Out += SubGraph_SaveLoadBool_Load_Out_65;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_65.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_65;
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_70.Out += SubGraph_Crafting_Tutorial_ManageBlock_Out_70;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_75.Output1 += uScriptCon_ManualSwitch_Output1_75;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_75.Output2 += uScriptCon_ManualSwitch_Output2_75;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_75.Output3 += uScriptCon_ManualSwitch_Output3_75;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_75.Output4 += uScriptCon_ManualSwitch_Output4_75;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_75.Output5 += uScriptCon_ManualSwitch_Output5_75;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_75.Output6 += uScriptCon_ManualSwitch_Output6_75;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_75.Output7 += uScriptCon_ManualSwitch_Output7_75;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_75.Output8 += uScriptCon_ManualSwitch_Output8_75;
		logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_SubGraph_Crafting_Tutorial_AttachBlockToBase_84.Block_Attached += SubGraph_Crafting_Tutorial_AttachBlockToBase_Block_Attached_84;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_113.Output1 += uScriptCon_ManualSwitch_Output1_113;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_113.Output2 += uScriptCon_ManualSwitch_Output2_113;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_113.Output3 += uScriptCon_ManualSwitch_Output3_113;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_113.Output4 += uScriptCon_ManualSwitch_Output4_113;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_113.Output5 += uScriptCon_ManualSwitch_Output5_113;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_113.Output6 += uScriptCon_ManualSwitch_Output6_113;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_113.Output7 += uScriptCon_ManualSwitch_Output7_113;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_113.Output8 += uScriptCon_ManualSwitch_Output8_113;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_115.Output1 += uScriptCon_ManualSwitch_Output1_115;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_115.Output2 += uScriptCon_ManualSwitch_Output2_115;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_115.Output3 += uScriptCon_ManualSwitch_Output3_115;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_115.Output4 += uScriptCon_ManualSwitch_Output4_115;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_115.Output5 += uScriptCon_ManualSwitch_Output5_115;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_115.Output6 += uScriptCon_ManualSwitch_Output6_115;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_115.Output7 += uScriptCon_ManualSwitch_Output7_115;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_115.Output8 += uScriptCon_ManualSwitch_Output8_115;
		logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_SubGraph_Crafting_Tutorial_AttachBlockToBase_177.Block_Attached += SubGraph_Crafting_Tutorial_AttachBlockToBase_Block_Attached_177;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_201.Save_Out += SubGraph_SaveLoadInt_Save_Out_201;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_201.Load_Out += SubGraph_SaveLoadInt_Load_Out_201;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_201.Restart_Out += SubGraph_SaveLoadInt_Restart_Out_201;
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_203.Out += SubGraph_LoadObjectiveStates_Out_203;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_205.Output1 += uScriptCon_ManualSwitch_Output1_205;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_205.Output2 += uScriptCon_ManualSwitch_Output2_205;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_205.Output3 += uScriptCon_ManualSwitch_Output3_205;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_205.Output4 += uScriptCon_ManualSwitch_Output4_205;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_205.Output5 += uScriptCon_ManualSwitch_Output5_205;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_205.Output6 += uScriptCon_ManualSwitch_Output6_205;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_205.Output7 += uScriptCon_ManualSwitch_Output7_205;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_205.Output8 += uScriptCon_ManualSwitch_Output8_205;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_214.Save_Out += SubGraph_SaveLoadInt_Save_Out_214;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_214.Load_Out += SubGraph_SaveLoadInt_Load_Out_214;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_214.Restart_Out += SubGraph_SaveLoadInt_Restart_Out_214;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_226.Out += SubGraph_CompleteObjectiveStage_Out_226;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_228.Out += SubGraph_CompleteObjectiveStage_Out_228;
		logic_SubGraph_Crafting_Tutorial_Finish_SubGraph_Crafting_Tutorial_Finish_282.Out += SubGraph_Crafting_Tutorial_Finish_Out_282;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_311.Out += SubGraph_AddMessageWithPadSupport_Out_311;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_311.Shown += SubGraph_AddMessageWithPadSupport_Shown_311;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_318.Out += SubGraph_AddMessageWithPadSupport_Out_318;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_318.Shown += SubGraph_AddMessageWithPadSupport_Shown_318;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_374.Save_Out += SubGraph_SaveLoadBool_Save_Out_374;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_374.Load_Out += SubGraph_SaveLoadBool_Load_Out_374;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_374.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_374;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_376.Save_Out += SubGraph_SaveLoadBool_Save_Out_376;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_376.Load_Out += SubGraph_SaveLoadBool_Load_Out_376;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_376.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_376;
	}

	public void Start()
	{
		SyncUnityHooks();
		m_RegisteredForEvents = true;
		logic_SubGraph_Crafting_Tutorial_Init_SubGraph_Crafting_Tutorial_Init_15.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_22.Start();
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_27.Start();
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_32.Start();
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_34.Start();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_44.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_47.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_48.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_49.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_53.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_56.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_58.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_65.Start();
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_70.Start();
		logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_SubGraph_Crafting_Tutorial_AttachBlockToBase_84.Start();
		logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_SubGraph_Crafting_Tutorial_AttachBlockToBase_177.Start();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_201.Start();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_203.Start();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_214.Start();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_226.Start();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_228.Start();
		logic_SubGraph_Crafting_Tutorial_Finish_SubGraph_Crafting_Tutorial_Finish_282.Start();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_311.Start();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_318.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_374.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_376.Start();
	}

	public void OnEnable()
	{
		RegisterForUnityHooks();
		m_RegisteredForEvents = true;
		logic_SubGraph_Crafting_Tutorial_Init_SubGraph_Crafting_Tutorial_Init_15.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_22.OnEnable();
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_27.OnEnable();
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_32.OnEnable();
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_34.OnEnable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_44.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_47.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_48.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_49.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_53.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_56.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_58.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_65.OnEnable();
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_70.OnEnable();
		logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_SubGraph_Crafting_Tutorial_AttachBlockToBase_84.OnEnable();
		logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_SubGraph_Crafting_Tutorial_AttachBlockToBase_177.OnEnable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_201.OnEnable();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_203.OnEnable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_214.OnEnable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_226.OnEnable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_228.OnEnable();
		logic_SubGraph_Crafting_Tutorial_Finish_SubGraph_Crafting_Tutorial_Finish_282.OnEnable();
		logic_uScript_CraftingUIHighlightCraftButton_uScript_CraftingUIHighlightCraftButton_287.OnEnable();
		logic_uScript_CraftingUIHighlightItem_uScript_CraftingUIHighlightItem_288.OnEnable();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_311.OnEnable();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_318.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_374.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_376.OnEnable();
	}

	public void OnDisable()
	{
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_2.OnDisable();
		logic_SubGraph_Crafting_Tutorial_Init_SubGraph_Crafting_Tutorial_Init_15.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_22.OnDisable();
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_27.OnDisable();
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_32.OnDisable();
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_34.OnDisable();
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_37.OnDisable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_44.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_47.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_48.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_49.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_53.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_56.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_58.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_65.OnDisable();
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_70.OnDisable();
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_77.OnDisable();
		logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_SubGraph_Crafting_Tutorial_AttachBlockToBase_84.OnDisable();
		logic_uScript_Wait_uScript_Wait_91.OnDisable();
		logic_uScript_IsPlayerInteractingWithBlock_uScript_IsPlayerInteractingWithBlock_130.OnDisable();
		logic_uScript_IsPlayerInteractingWithBlock_uScript_IsPlayerInteractingWithBlock_136.OnDisable();
		logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_SubGraph_Crafting_Tutorial_AttachBlockToBase_177.OnDisable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_201.OnDisable();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_203.OnDisable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_214.OnDisable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_226.OnDisable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_228.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_231.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_234.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_236.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_238.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_243.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_245.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_249.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_252.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_254.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_258.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_263.OnDisable();
		logic_SubGraph_Crafting_Tutorial_Finish_SubGraph_Crafting_Tutorial_Finish_282.OnDisable();
		logic_uScript_CraftingUIHighlightCraftButton_uScript_CraftingUIHighlightCraftButton_287.OnDisable();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_311.OnDisable();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_318.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_374.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_376.OnDisable();
		UnregisterEventListeners();
		m_RegisteredForEvents = false;
	}

	public void Update()
	{
		SyncEventListeners();
		logic_SubGraph_Crafting_Tutorial_Init_SubGraph_Crafting_Tutorial_Init_15.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_22.Update();
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_27.Update();
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_32.Update();
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_34.Update();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_44.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_47.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_48.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_49.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_53.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_56.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_58.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_65.Update();
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_70.Update();
		logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_SubGraph_Crafting_Tutorial_AttachBlockToBase_84.Update();
		logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_SubGraph_Crafting_Tutorial_AttachBlockToBase_177.Update();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_201.Update();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_203.Update();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_214.Update();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_226.Update();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_228.Update();
		logic_SubGraph_Crafting_Tutorial_Finish_SubGraph_Crafting_Tutorial_Finish_282.Update();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_311.Update();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_318.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_374.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_376.Update();
	}

	public void OnDestroy()
	{
		logic_SubGraph_Crafting_Tutorial_Init_SubGraph_Crafting_Tutorial_Init_15.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_22.OnDestroy();
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_27.OnDestroy();
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_32.OnDestroy();
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_34.OnDestroy();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_44.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_47.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_48.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_49.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_53.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_56.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_58.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_65.OnDestroy();
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_70.OnDestroy();
		logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_SubGraph_Crafting_Tutorial_AttachBlockToBase_84.OnDestroy();
		logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_SubGraph_Crafting_Tutorial_AttachBlockToBase_177.OnDestroy();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_201.OnDestroy();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_203.OnDestroy();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_214.OnDestroy();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_226.OnDestroy();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_228.OnDestroy();
		logic_SubGraph_Crafting_Tutorial_Finish_SubGraph_Crafting_Tutorial_Finish_282.OnDestroy();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_311.OnDestroy();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_318.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_374.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_376.OnDestroy();
		logic_SubGraph_Crafting_Tutorial_Init_SubGraph_Crafting_Tutorial_Init_15.Out -= SubGraph_Crafting_Tutorial_Init_Out_15;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_22.Save_Out -= SubGraph_SaveLoadBool_Save_Out_22;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_22.Load_Out -= SubGraph_SaveLoadBool_Load_Out_22;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_22.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_22;
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_27.Out -= SubGraph_Crafting_Tutorial_ManageBlock_Out_27;
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_32.Out -= SubGraph_Crafting_Tutorial_ManageBlock_Out_32;
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_34.Out -= SubGraph_Crafting_Tutorial_ManageBlock_Out_34;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_44.Save_Out -= SubGraph_SaveLoadInt_Save_Out_44;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_44.Load_Out -= SubGraph_SaveLoadInt_Load_Out_44;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_44.Restart_Out -= SubGraph_SaveLoadInt_Restart_Out_44;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_47.Save_Out -= SubGraph_SaveLoadBool_Save_Out_47;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_47.Load_Out -= SubGraph_SaveLoadBool_Load_Out_47;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_47.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_47;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_48.Save_Out -= SubGraph_SaveLoadBool_Save_Out_48;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_48.Load_Out -= SubGraph_SaveLoadBool_Load_Out_48;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_48.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_48;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_49.Save_Out -= SubGraph_SaveLoadBool_Save_Out_49;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_49.Load_Out -= SubGraph_SaveLoadBool_Load_Out_49;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_49.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_49;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_53.Save_Out -= SubGraph_SaveLoadBool_Save_Out_53;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_53.Load_Out -= SubGraph_SaveLoadBool_Load_Out_53;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_53.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_53;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_56.Save_Out -= SubGraph_SaveLoadBool_Save_Out_56;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_56.Load_Out -= SubGraph_SaveLoadBool_Load_Out_56;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_56.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_56;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_58.Save_Out -= SubGraph_SaveLoadBool_Save_Out_58;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_58.Load_Out -= SubGraph_SaveLoadBool_Load_Out_58;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_58.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_58;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_65.Save_Out -= SubGraph_SaveLoadBool_Save_Out_65;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_65.Load_Out -= SubGraph_SaveLoadBool_Load_Out_65;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_65.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_65;
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_70.Out -= SubGraph_Crafting_Tutorial_ManageBlock_Out_70;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_75.Output1 -= uScriptCon_ManualSwitch_Output1_75;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_75.Output2 -= uScriptCon_ManualSwitch_Output2_75;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_75.Output3 -= uScriptCon_ManualSwitch_Output3_75;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_75.Output4 -= uScriptCon_ManualSwitch_Output4_75;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_75.Output5 -= uScriptCon_ManualSwitch_Output5_75;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_75.Output6 -= uScriptCon_ManualSwitch_Output6_75;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_75.Output7 -= uScriptCon_ManualSwitch_Output7_75;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_75.Output8 -= uScriptCon_ManualSwitch_Output8_75;
		logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_SubGraph_Crafting_Tutorial_AttachBlockToBase_84.Block_Attached -= SubGraph_Crafting_Tutorial_AttachBlockToBase_Block_Attached_84;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_113.Output1 -= uScriptCon_ManualSwitch_Output1_113;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_113.Output2 -= uScriptCon_ManualSwitch_Output2_113;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_113.Output3 -= uScriptCon_ManualSwitch_Output3_113;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_113.Output4 -= uScriptCon_ManualSwitch_Output4_113;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_113.Output5 -= uScriptCon_ManualSwitch_Output5_113;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_113.Output6 -= uScriptCon_ManualSwitch_Output6_113;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_113.Output7 -= uScriptCon_ManualSwitch_Output7_113;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_113.Output8 -= uScriptCon_ManualSwitch_Output8_113;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_115.Output1 -= uScriptCon_ManualSwitch_Output1_115;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_115.Output2 -= uScriptCon_ManualSwitch_Output2_115;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_115.Output3 -= uScriptCon_ManualSwitch_Output3_115;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_115.Output4 -= uScriptCon_ManualSwitch_Output4_115;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_115.Output5 -= uScriptCon_ManualSwitch_Output5_115;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_115.Output6 -= uScriptCon_ManualSwitch_Output6_115;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_115.Output7 -= uScriptCon_ManualSwitch_Output7_115;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_115.Output8 -= uScriptCon_ManualSwitch_Output8_115;
		logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_SubGraph_Crafting_Tutorial_AttachBlockToBase_177.Block_Attached -= SubGraph_Crafting_Tutorial_AttachBlockToBase_Block_Attached_177;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_201.Save_Out -= SubGraph_SaveLoadInt_Save_Out_201;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_201.Load_Out -= SubGraph_SaveLoadInt_Load_Out_201;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_201.Restart_Out -= SubGraph_SaveLoadInt_Restart_Out_201;
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_203.Out -= SubGraph_LoadObjectiveStates_Out_203;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_205.Output1 -= uScriptCon_ManualSwitch_Output1_205;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_205.Output2 -= uScriptCon_ManualSwitch_Output2_205;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_205.Output3 -= uScriptCon_ManualSwitch_Output3_205;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_205.Output4 -= uScriptCon_ManualSwitch_Output4_205;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_205.Output5 -= uScriptCon_ManualSwitch_Output5_205;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_205.Output6 -= uScriptCon_ManualSwitch_Output6_205;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_205.Output7 -= uScriptCon_ManualSwitch_Output7_205;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_205.Output8 -= uScriptCon_ManualSwitch_Output8_205;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_214.Save_Out -= SubGraph_SaveLoadInt_Save_Out_214;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_214.Load_Out -= SubGraph_SaveLoadInt_Load_Out_214;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_214.Restart_Out -= SubGraph_SaveLoadInt_Restart_Out_214;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_226.Out -= SubGraph_CompleteObjectiveStage_Out_226;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_228.Out -= SubGraph_CompleteObjectiveStage_Out_228;
		logic_SubGraph_Crafting_Tutorial_Finish_SubGraph_Crafting_Tutorial_Finish_282.Out -= SubGraph_Crafting_Tutorial_Finish_Out_282;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_311.Out -= SubGraph_AddMessageWithPadSupport_Out_311;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_311.Shown -= SubGraph_AddMessageWithPadSupport_Shown_311;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_318.Out -= SubGraph_AddMessageWithPadSupport_Out_318;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_318.Shown -= SubGraph_AddMessageWithPadSupport_Shown_318;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_374.Save_Out -= SubGraph_SaveLoadBool_Save_Out_374;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_374.Load_Out -= SubGraph_SaveLoadBool_Load_Out_374;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_374.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_374;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_376.Save_Out -= SubGraph_SaveLoadBool_Save_Out_376;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_376.Load_Out -= SubGraph_SaveLoadBool_Load_Out_376;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_376.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_376;
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

	private void Instance_SaveEvent_6(object o, EventArgs e)
	{
		Relay_SaveEvent_6();
	}

	private void Instance_LoadEvent_6(object o, EventArgs e)
	{
		Relay_LoadEvent_6();
	}

	private void Instance_RestartEvent_6(object o, EventArgs e)
	{
		Relay_RestartEvent_6();
	}

	private void Instance_BlockCraftedEvent_9(object o, uScript_BlockCraftedEvent.BlockCraftedEventArgs e)
	{
		event_UnityEngine_GameObject_BlockType_9 = e.BlockType;
		event_UnityEngine_GameObject_BlockTypeTotal_9 = e.BlockTypeTotal;
		event_UnityEngine_GameObject_BlockTotal_9 = e.BlockTotal;
		event_UnityEngine_GameObject_Block_9 = e.Block;
		event_UnityEngine_GameObject_CrafterBlock_9 = e.CrafterBlock;
		Relay_BlockCraftedEvent_9();
	}

	private void SubGraph_Crafting_Tutorial_Init_Out_15(object o, SubGraph_Crafting_Tutorial_Init.LogicEventArgs e)
	{
		logic_SubGraph_Crafting_Tutorial_Init_CraftingBaseTech_15 = e.CraftingBaseTech;
		logic_SubGraph_Crafting_Tutorial_Init_NPCTech_15 = e.NPCTech;
		local_CraftingBaseTech_Tank = logic_SubGraph_Crafting_Tutorial_Init_CraftingBaseTech_15;
		local_NPCTech_Tank = logic_SubGraph_Crafting_Tutorial_Init_NPCTech_15;
		Relay_Out_15();
	}

	private void SubGraph_SaveLoadBool_Save_Out_22(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_22 = e.boolean;
		local_Resource01Spawned_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_22;
		Relay_Save_Out_22();
	}

	private void SubGraph_SaveLoadBool_Load_Out_22(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_22 = e.boolean;
		local_Resource01Spawned_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_22;
		Relay_Load_Out_22();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_22(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_22 = e.boolean;
		local_Resource01Spawned_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_22;
		Relay_Restart_Out_22();
	}

	private void SubGraph_Crafting_Tutorial_ManageBlock_Out_27(object o, SubGraph_Crafting_Tutorial_ManageBlock.LogicEventArgs e)
	{
		logic_SubGraph_Crafting_Tutorial_ManageBlock_block_27 = e.block;
		blockSpawnData = logic_SubGraph_Crafting_Tutorial_ManageBlock_blockSpawnData_27;
		local_ConveyorBlock02_TankBlock = logic_SubGraph_Crafting_Tutorial_ManageBlock_block_27;
		Relay_Out_27();
	}

	private void SubGraph_Crafting_Tutorial_ManageBlock_Out_32(object o, SubGraph_Crafting_Tutorial_ManageBlock.LogicEventArgs e)
	{
		logic_SubGraph_Crafting_Tutorial_ManageBlock_block_32 = e.block;
		blockSpawnData = logic_SubGraph_Crafting_Tutorial_ManageBlock_blockSpawnData_32;
		local_ReceiverBlock_TankBlock = logic_SubGraph_Crafting_Tutorial_ManageBlock_block_32;
		Relay_Out_32();
	}

	private void SubGraph_Crafting_Tutorial_ManageBlock_Out_34(object o, SubGraph_Crafting_Tutorial_ManageBlock.LogicEventArgs e)
	{
		logic_SubGraph_Crafting_Tutorial_ManageBlock_block_34 = e.block;
		blockSpawnData = logic_SubGraph_Crafting_Tutorial_ManageBlock_blockSpawnData_34;
		local_ConveyorBlock01_TankBlock = logic_SubGraph_Crafting_Tutorial_ManageBlock_block_34;
		Relay_Out_34();
	}

	private void SubGraph_SaveLoadInt_Save_Out_44(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_44 = e.integer;
		local_NumConveyorsAttached_System_Int32 = logic_SubGraph_SaveLoadInt_integer_44;
		Relay_Save_Out_44();
	}

	private void SubGraph_SaveLoadInt_Load_Out_44(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_44 = e.integer;
		local_NumConveyorsAttached_System_Int32 = logic_SubGraph_SaveLoadInt_integer_44;
		Relay_Load_Out_44();
	}

	private void SubGraph_SaveLoadInt_Restart_Out_44(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_44 = e.integer;
		local_NumConveyorsAttached_System_Int32 = logic_SubGraph_SaveLoadInt_integer_44;
		Relay_Restart_Out_44();
	}

	private void SubGraph_SaveLoadBool_Save_Out_47(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_47 = e.boolean;
		local_msgIntroShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_47;
		Relay_Save_Out_47();
	}

	private void SubGraph_SaveLoadBool_Load_Out_47(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_47 = e.boolean;
		local_msgIntroShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_47;
		Relay_Load_Out_47();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_47(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_47 = e.boolean;
		local_msgIntroShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_47;
		Relay_Restart_Out_47();
	}

	private void SubGraph_SaveLoadBool_Save_Out_48(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_48 = e.boolean;
		local_msgBaseFoundShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_48;
		Relay_Save_Out_48();
	}

	private void SubGraph_SaveLoadBool_Load_Out_48(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_48 = e.boolean;
		local_msgBaseFoundShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_48;
		Relay_Load_Out_48();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_48(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_48 = e.boolean;
		local_msgBaseFoundShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_48;
		Relay_Restart_Out_48();
	}

	private void SubGraph_SaveLoadBool_Save_Out_49(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_49 = e.boolean;
		local_msgReceiverAttachedShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_49;
		Relay_Save_Out_49();
	}

	private void SubGraph_SaveLoadBool_Load_Out_49(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_49 = e.boolean;
		local_msgReceiverAttachedShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_49;
		Relay_Load_Out_49();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_49(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_49 = e.boolean;
		local_msgReceiverAttachedShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_49;
		Relay_Restart_Out_49();
	}

	private void SubGraph_SaveLoadBool_Save_Out_53(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_53 = e.boolean;
		local_msgConveyorsAttachedShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_53;
		Relay_Save_Out_53();
	}

	private void SubGraph_SaveLoadBool_Load_Out_53(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_53 = e.boolean;
		local_msgConveyorsAttachedShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_53;
		Relay_Load_Out_53();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_53(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_53 = e.boolean;
		local_msgConveyorsAttachedShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_53;
		Relay_Restart_Out_53();
	}

	private void SubGraph_SaveLoadBool_Save_Out_56(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_56 = e.boolean;
		local_FabricatorSpawned_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_56;
		Relay_Save_Out_56();
	}

	private void SubGraph_SaveLoadBool_Load_Out_56(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_56 = e.boolean;
		local_FabricatorSpawned_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_56;
		Relay_Load_Out_56();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_56(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_56 = e.boolean;
		local_FabricatorSpawned_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_56;
		Relay_Restart_Out_56();
	}

	private void SubGraph_SaveLoadBool_Save_Out_58(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_58 = e.boolean;
		local_msgFabricatorSpawnedShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_58;
		Relay_Save_Out_58();
	}

	private void SubGraph_SaveLoadBool_Load_Out_58(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_58 = e.boolean;
		local_msgFabricatorSpawnedShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_58;
		Relay_Load_Out_58();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_58(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_58 = e.boolean;
		local_msgFabricatorSpawnedShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_58;
		Relay_Restart_Out_58();
	}

	private void SubGraph_SaveLoadBool_Save_Out_65(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_65 = e.boolean;
		local_BlockCrafted_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_65;
		Relay_Save_Out_65();
	}

	private void SubGraph_SaveLoadBool_Load_Out_65(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_65 = e.boolean;
		local_BlockCrafted_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_65;
		Relay_Load_Out_65();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_65(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_65 = e.boolean;
		local_BlockCrafted_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_65;
		Relay_Restart_Out_65();
	}

	private void SubGraph_Crafting_Tutorial_ManageBlock_Out_70(object o, SubGraph_Crafting_Tutorial_ManageBlock.LogicEventArgs e)
	{
		logic_SubGraph_Crafting_Tutorial_ManageBlock_block_70 = e.block;
		blockSpawnDataFabricator = logic_SubGraph_Crafting_Tutorial_ManageBlock_blockSpawnData_70;
		local_FabricatorBlock_TankBlock = logic_SubGraph_Crafting_Tutorial_ManageBlock_block_70;
		Relay_Out_70();
	}

	private void uScriptCon_ManualSwitch_Output1_75(object o, EventArgs e)
	{
		Relay_Output1_75();
	}

	private void uScriptCon_ManualSwitch_Output2_75(object o, EventArgs e)
	{
		Relay_Output2_75();
	}

	private void uScriptCon_ManualSwitch_Output3_75(object o, EventArgs e)
	{
		Relay_Output3_75();
	}

	private void uScriptCon_ManualSwitch_Output4_75(object o, EventArgs e)
	{
		Relay_Output4_75();
	}

	private void uScriptCon_ManualSwitch_Output5_75(object o, EventArgs e)
	{
		Relay_Output5_75();
	}

	private void uScriptCon_ManualSwitch_Output6_75(object o, EventArgs e)
	{
		Relay_Output6_75();
	}

	private void uScriptCon_ManualSwitch_Output7_75(object o, EventArgs e)
	{
		Relay_Output7_75();
	}

	private void uScriptCon_ManualSwitch_Output8_75(object o, EventArgs e)
	{
		Relay_Output8_75();
	}

	private void SubGraph_Crafting_Tutorial_AttachBlockToBase_Block_Attached_84(object o, SubGraph_Crafting_Tutorial_AttachBlockToBase.LogicEventArgs e)
	{
		Relay_Block_Attached_84();
	}

	private void uScriptCon_ManualSwitch_Output1_113(object o, EventArgs e)
	{
		Relay_Output1_113();
	}

	private void uScriptCon_ManualSwitch_Output2_113(object o, EventArgs e)
	{
		Relay_Output2_113();
	}

	private void uScriptCon_ManualSwitch_Output3_113(object o, EventArgs e)
	{
		Relay_Output3_113();
	}

	private void uScriptCon_ManualSwitch_Output4_113(object o, EventArgs e)
	{
		Relay_Output4_113();
	}

	private void uScriptCon_ManualSwitch_Output5_113(object o, EventArgs e)
	{
		Relay_Output5_113();
	}

	private void uScriptCon_ManualSwitch_Output6_113(object o, EventArgs e)
	{
		Relay_Output6_113();
	}

	private void uScriptCon_ManualSwitch_Output7_113(object o, EventArgs e)
	{
		Relay_Output7_113();
	}

	private void uScriptCon_ManualSwitch_Output8_113(object o, EventArgs e)
	{
		Relay_Output8_113();
	}

	private void uScriptCon_ManualSwitch_Output1_115(object o, EventArgs e)
	{
		Relay_Output1_115();
	}

	private void uScriptCon_ManualSwitch_Output2_115(object o, EventArgs e)
	{
		Relay_Output2_115();
	}

	private void uScriptCon_ManualSwitch_Output3_115(object o, EventArgs e)
	{
		Relay_Output3_115();
	}

	private void uScriptCon_ManualSwitch_Output4_115(object o, EventArgs e)
	{
		Relay_Output4_115();
	}

	private void uScriptCon_ManualSwitch_Output5_115(object o, EventArgs e)
	{
		Relay_Output5_115();
	}

	private void uScriptCon_ManualSwitch_Output6_115(object o, EventArgs e)
	{
		Relay_Output6_115();
	}

	private void uScriptCon_ManualSwitch_Output7_115(object o, EventArgs e)
	{
		Relay_Output7_115();
	}

	private void uScriptCon_ManualSwitch_Output8_115(object o, EventArgs e)
	{
		Relay_Output8_115();
	}

	private void SubGraph_Crafting_Tutorial_AttachBlockToBase_Block_Attached_177(object o, SubGraph_Crafting_Tutorial_AttachBlockToBase.LogicEventArgs e)
	{
		Relay_Block_Attached_177();
	}

	private void SubGraph_SaveLoadInt_Save_Out_201(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_201 = e.integer;
		local_Stage_System_Int32 = logic_SubGraph_SaveLoadInt_integer_201;
		Relay_Save_Out_201();
	}

	private void SubGraph_SaveLoadInt_Load_Out_201(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_201 = e.integer;
		local_Stage_System_Int32 = logic_SubGraph_SaveLoadInt_integer_201;
		Relay_Load_Out_201();
	}

	private void SubGraph_SaveLoadInt_Restart_Out_201(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_201 = e.integer;
		local_Stage_System_Int32 = logic_SubGraph_SaveLoadInt_integer_201;
		Relay_Restart_Out_201();
	}

	private void SubGraph_LoadObjectiveStates_Out_203(object o, SubGraph_LoadObjectiveStates.LogicEventArgs e)
	{
		Relay_Out_203();
	}

	private void uScriptCon_ManualSwitch_Output1_205(object o, EventArgs e)
	{
		Relay_Output1_205();
	}

	private void uScriptCon_ManualSwitch_Output2_205(object o, EventArgs e)
	{
		Relay_Output2_205();
	}

	private void uScriptCon_ManualSwitch_Output3_205(object o, EventArgs e)
	{
		Relay_Output3_205();
	}

	private void uScriptCon_ManualSwitch_Output4_205(object o, EventArgs e)
	{
		Relay_Output4_205();
	}

	private void uScriptCon_ManualSwitch_Output5_205(object o, EventArgs e)
	{
		Relay_Output5_205();
	}

	private void uScriptCon_ManualSwitch_Output6_205(object o, EventArgs e)
	{
		Relay_Output6_205();
	}

	private void uScriptCon_ManualSwitch_Output7_205(object o, EventArgs e)
	{
		Relay_Output7_205();
	}

	private void uScriptCon_ManualSwitch_Output8_205(object o, EventArgs e)
	{
		Relay_Output8_205();
	}

	private void SubGraph_SaveLoadInt_Save_Out_214(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_214 = e.integer;
		local_Stage2Steps_System_Int32 = logic_SubGraph_SaveLoadInt_integer_214;
		Relay_Save_Out_214();
	}

	private void SubGraph_SaveLoadInt_Load_Out_214(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_214 = e.integer;
		local_Stage2Steps_System_Int32 = logic_SubGraph_SaveLoadInt_integer_214;
		Relay_Load_Out_214();
	}

	private void SubGraph_SaveLoadInt_Restart_Out_214(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_214 = e.integer;
		local_Stage2Steps_System_Int32 = logic_SubGraph_SaveLoadInt_integer_214;
		Relay_Restart_Out_214();
	}

	private void SubGraph_CompleteObjectiveStage_Out_226(object o, SubGraph_CompleteObjectiveStage.LogicEventArgs e)
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_226 = e.objectiveStage;
		local_Stage_System_Int32 = logic_SubGraph_CompleteObjectiveStage_objectiveStage_226;
		Relay_Out_226();
	}

	private void SubGraph_CompleteObjectiveStage_Out_228(object o, SubGraph_CompleteObjectiveStage.LogicEventArgs e)
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_228 = e.objectiveStage;
		local_Stage_System_Int32 = logic_SubGraph_CompleteObjectiveStage_objectiveStage_228;
		Relay_Out_228();
	}

	private void SubGraph_Crafting_Tutorial_Finish_Out_282(object o, SubGraph_Crafting_Tutorial_Finish.LogicEventArgs e)
	{
		Relay_Out_282();
	}

	private void SubGraph_AddMessageWithPadSupport_Out_311(object o, SubGraph_AddMessageWithPadSupport.LogicEventArgs e)
	{
		logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboardReturn_311 = e.messageMouseKeyboardReturn;
		logic_SubGraph_AddMessageWithPadSupport_messageControlPadReturn_311 = e.messageControlPadReturn;
		Relay_Out_311();
	}

	private void SubGraph_AddMessageWithPadSupport_Shown_311(object o, SubGraph_AddMessageWithPadSupport.LogicEventArgs e)
	{
		logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboardReturn_311 = e.messageMouseKeyboardReturn;
		logic_SubGraph_AddMessageWithPadSupport_messageControlPadReturn_311 = e.messageControlPadReturn;
		Relay_Shown_311();
	}

	private void SubGraph_AddMessageWithPadSupport_Out_318(object o, SubGraph_AddMessageWithPadSupport.LogicEventArgs e)
	{
		logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboardReturn_318 = e.messageMouseKeyboardReturn;
		logic_SubGraph_AddMessageWithPadSupport_messageControlPadReturn_318 = e.messageControlPadReturn;
		Relay_Out_318();
	}

	private void SubGraph_AddMessageWithPadSupport_Shown_318(object o, SubGraph_AddMessageWithPadSupport.LogicEventArgs e)
	{
		logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboardReturn_318 = e.messageMouseKeyboardReturn;
		logic_SubGraph_AddMessageWithPadSupport_messageControlPadReturn_318 = e.messageControlPadReturn;
		Relay_Shown_318();
	}

	private void SubGraph_SaveLoadBool_Save_Out_374(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_374 = e.boolean;
		local_CraftingInProgress_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_374;
		Relay_Save_Out_374();
	}

	private void SubGraph_SaveLoadBool_Load_Out_374(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_374 = e.boolean;
		local_CraftingInProgress_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_374;
		Relay_Load_Out_374();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_374(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_374 = e.boolean;
		local_CraftingInProgress_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_374;
		Relay_Restart_Out_374();
	}

	private void SubGraph_SaveLoadBool_Save_Out_376(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_376 = e.boolean;
		local_CraftingMenuOpened_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_376;
		Relay_Save_Out_376();
	}

	private void SubGraph_SaveLoadBool_Load_Out_376(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_376 = e.boolean;
		local_CraftingMenuOpened_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_376;
		Relay_Load_Out_376();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_376(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_376 = e.boolean;
		local_CraftingMenuOpened_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_376;
		Relay_Restart_Out_376();
	}

	private void Relay_OnUpdate_0()
	{
		Relay_In_15();
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
			Relay_True_40();
		}
	}

	private void Relay_UnPause_3()
	{
		logic_uScript_PausePopulation_uScript_PausePopulation_3.UnPause();
		if (logic_uScript_PausePopulation_uScript_PausePopulation_3.Out)
		{
			Relay_True_40();
		}
	}

	private void Relay_Pause_4()
	{
		logic_uScript_PausePopulation_uScript_PausePopulation_4.Pause();
		if (logic_uScript_PausePopulation_uScript_PausePopulation_4.Out)
		{
			Relay_In_292();
		}
	}

	private void Relay_UnPause_4()
	{
		logic_uScript_PausePopulation_uScript_PausePopulation_4.UnPause();
		if (logic_uScript_PausePopulation_uScript_PausePopulation_4.Out)
		{
			Relay_In_292();
		}
	}

	private void Relay_SaveEvent_6()
	{
		Relay_Save_44();
	}

	private void Relay_LoadEvent_6()
	{
		Relay_Load_44();
	}

	private void Relay_RestartEvent_6()
	{
		Relay_Restart_44();
	}

	private void Relay_BlockCraftedEvent_9()
	{
		local_11_BlockTypes = event_UnityEngine_GameObject_BlockType_9;
		Relay_In_12();
	}

	private void Relay_In_12()
	{
		logic_uScript_CompareBlockTypes_A_12 = local_11_BlockTypes;
		logic_uScript_CompareBlockTypes_B_12 = blockTypeToCraft;
		logic_uScript_CompareBlockTypes_uScript_CompareBlockTypes_12.In(logic_uScript_CompareBlockTypes_A_12, logic_uScript_CompareBlockTypes_B_12);
		if (logic_uScript_CompareBlockTypes_uScript_CompareBlockTypes_12.EqualTo)
		{
			Relay_True_64();
		}
	}

	private void Relay_In_14()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_14 = messageTag;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_14.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_14, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_14);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_14.Out)
		{
			Relay_In_54();
		}
	}

	private void Relay_Out_15()
	{
		Relay_In_32();
	}

	private void Relay_In_15()
	{
		int num = 0;
		Array array = baseSpawnData;
		if (logic_SubGraph_Crafting_Tutorial_Init_baseSpawnData_15.Length != num + array.Length)
		{
			Array.Resize(ref logic_SubGraph_Crafting_Tutorial_Init_baseSpawnData_15, num + array.Length);
		}
		Array.Copy(array, 0, logic_SubGraph_Crafting_Tutorial_Init_baseSpawnData_15, num, array.Length);
		num += array.Length;
		int num2 = 0;
		Array array2 = blockSpawnData;
		if (logic_SubGraph_Crafting_Tutorial_Init_blockSpawnData_15.Length != num2 + array2.Length)
		{
			Array.Resize(ref logic_SubGraph_Crafting_Tutorial_Init_blockSpawnData_15, num2 + array2.Length);
		}
		Array.Copy(array2, 0, logic_SubGraph_Crafting_Tutorial_Init_blockSpawnData_15, num2, array2.Length);
		num2 += array2.Length;
		int num3 = 0;
		Array nPCSpawnData = NPCSpawnData;
		if (logic_SubGraph_Crafting_Tutorial_Init_NPCSpawnData_15.Length != num3 + nPCSpawnData.Length)
		{
			Array.Resize(ref logic_SubGraph_Crafting_Tutorial_Init_NPCSpawnData_15, num3 + nPCSpawnData.Length);
		}
		Array.Copy(nPCSpawnData, 0, logic_SubGraph_Crafting_Tutorial_Init_NPCSpawnData_15, num3, nPCSpawnData.Length);
		num3 += nPCSpawnData.Length;
		logic_SubGraph_Crafting_Tutorial_Init_completedBasePreset_15 = completedBasePreset;
		logic_SubGraph_Crafting_Tutorial_Init_basePosition_15 = basePosition;
		logic_SubGraph_Crafting_Tutorial_Init_clearSceneryRadius_15 = clearSceneryRadius;
		logic_SubGraph_Crafting_Tutorial_Init_CraftingBaseTech_15 = local_CraftingBaseTech_Tank;
		logic_SubGraph_Crafting_Tutorial_Init_NPCTech_15 = local_NPCTech_Tank;
		logic_SubGraph_Crafting_Tutorial_Init_SubGraph_Crafting_Tutorial_Init_15.In(logic_SubGraph_Crafting_Tutorial_Init_baseSpawnData_15, logic_SubGraph_Crafting_Tutorial_Init_blockSpawnData_15, logic_SubGraph_Crafting_Tutorial_Init_NPCSpawnData_15, logic_SubGraph_Crafting_Tutorial_Init_completedBasePreset_15, logic_SubGraph_Crafting_Tutorial_Init_useNPCAsTutorialTech_15, logic_SubGraph_Crafting_Tutorial_Init_spawnBase_15, logic_SubGraph_Crafting_Tutorial_Init_basePosition_15, logic_SubGraph_Crafting_Tutorial_Init_clearSceneryRadius_15, ref logic_SubGraph_Crafting_Tutorial_Init_CraftingBaseTech_15, ref logic_SubGraph_Crafting_Tutorial_Init_NPCTech_15);
	}

	private void Relay_Save_Out_22()
	{
		Relay_Save_47();
	}

	private void Relay_Load_Out_22()
	{
		Relay_Load_47();
	}

	private void Relay_Restart_Out_22()
	{
		Relay_Set_False_47();
	}

	private void Relay_Save_22()
	{
		logic_SubGraph_SaveLoadBool_boolean_22 = local_Resource01Spawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_22 = local_Resource01Spawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_22.Save(ref logic_SubGraph_SaveLoadBool_boolean_22, logic_SubGraph_SaveLoadBool_boolAsVariable_22, logic_SubGraph_SaveLoadBool_uniqueID_22);
	}

	private void Relay_Load_22()
	{
		logic_SubGraph_SaveLoadBool_boolean_22 = local_Resource01Spawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_22 = local_Resource01Spawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_22.Load(ref logic_SubGraph_SaveLoadBool_boolean_22, logic_SubGraph_SaveLoadBool_boolAsVariable_22, logic_SubGraph_SaveLoadBool_uniqueID_22);
	}

	private void Relay_Set_True_22()
	{
		logic_SubGraph_SaveLoadBool_boolean_22 = local_Resource01Spawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_22 = local_Resource01Spawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_22.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_22, logic_SubGraph_SaveLoadBool_boolAsVariable_22, logic_SubGraph_SaveLoadBool_uniqueID_22);
	}

	private void Relay_Set_False_22()
	{
		logic_SubGraph_SaveLoadBool_boolean_22 = local_Resource01Spawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_22 = local_Resource01Spawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_22.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_22, logic_SubGraph_SaveLoadBool_boolAsVariable_22, logic_SubGraph_SaveLoadBool_uniqueID_22);
	}

	private void Relay_In_24()
	{
		logic_uScript_LockTechStacks_tech_24 = local_CraftingBaseTech_Tank;
		logic_uScript_LockTechStacks_uScript_LockTechStacks_24.In(logic_uScript_LockTechStacks_tech_24);
		if (logic_uScript_LockTechStacks_uScript_LockTechStacks_24.Out)
		{
			Relay_In_42();
		}
	}

	private void Relay_Out_27()
	{
		Relay_In_73();
	}

	private void Relay_In_27()
	{
		int num = 0;
		Array array = blockSpawnData;
		if (logic_SubGraph_Crafting_Tutorial_ManageBlock_blockSpawnData_27.Length != num + array.Length)
		{
			Array.Resize(ref logic_SubGraph_Crafting_Tutorial_ManageBlock_blockSpawnData_27, num + array.Length);
		}
		Array.Copy(array, 0, logic_SubGraph_Crafting_Tutorial_ManageBlock_blockSpawnData_27, num, array.Length);
		num += array.Length;
		logic_SubGraph_Crafting_Tutorial_ManageBlock_block_27 = local_ConveyorBlock02_TankBlock;
		logic_SubGraph_Crafting_Tutorial_ManageBlock_msgBlockOutsideArea_27 = msgBlockOutsideArea;
		logic_SubGraph_Crafting_Tutorial_ManageBlock_messageSpeaker_27 = messageSpeaker;
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_27.In(ref logic_SubGraph_Crafting_Tutorial_ManageBlock_blockSpawnData_27, logic_SubGraph_Crafting_Tutorial_ManageBlock_blockIndex_27, ref logic_SubGraph_Crafting_Tutorial_ManageBlock_block_27, logic_SubGraph_Crafting_Tutorial_ManageBlock_allowAnchoring_27, logic_SubGraph_Crafting_Tutorial_ManageBlock_msgBlockOutsideArea_27, logic_SubGraph_Crafting_Tutorial_ManageBlock_messageSpeaker_27);
	}

	private void Relay_Out_32()
	{
		Relay_In_34();
	}

	private void Relay_In_32()
	{
		int num = 0;
		Array array = blockSpawnData;
		if (logic_SubGraph_Crafting_Tutorial_ManageBlock_blockSpawnData_32.Length != num + array.Length)
		{
			Array.Resize(ref logic_SubGraph_Crafting_Tutorial_ManageBlock_blockSpawnData_32, num + array.Length);
		}
		Array.Copy(array, 0, logic_SubGraph_Crafting_Tutorial_ManageBlock_blockSpawnData_32, num, array.Length);
		num += array.Length;
		logic_SubGraph_Crafting_Tutorial_ManageBlock_block_32 = local_ReceiverBlock_TankBlock;
		logic_SubGraph_Crafting_Tutorial_ManageBlock_msgBlockOutsideArea_32 = msgBlockOutsideArea;
		logic_SubGraph_Crafting_Tutorial_ManageBlock_messageSpeaker_32 = messageSpeaker;
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_32.In(ref logic_SubGraph_Crafting_Tutorial_ManageBlock_blockSpawnData_32, logic_SubGraph_Crafting_Tutorial_ManageBlock_blockIndex_32, ref logic_SubGraph_Crafting_Tutorial_ManageBlock_block_32, logic_SubGraph_Crafting_Tutorial_ManageBlock_allowAnchoring_32, logic_SubGraph_Crafting_Tutorial_ManageBlock_msgBlockOutsideArea_32, logic_SubGraph_Crafting_Tutorial_ManageBlock_messageSpeaker_32);
	}

	private void Relay_Out_34()
	{
		Relay_In_27();
	}

	private void Relay_In_34()
	{
		int num = 0;
		Array array = blockSpawnData;
		if (logic_SubGraph_Crafting_Tutorial_ManageBlock_blockSpawnData_34.Length != num + array.Length)
		{
			Array.Resize(ref logic_SubGraph_Crafting_Tutorial_ManageBlock_blockSpawnData_34, num + array.Length);
		}
		Array.Copy(array, 0, logic_SubGraph_Crafting_Tutorial_ManageBlock_blockSpawnData_34, num, array.Length);
		num += array.Length;
		logic_SubGraph_Crafting_Tutorial_ManageBlock_block_34 = local_ConveyorBlock01_TankBlock;
		logic_SubGraph_Crafting_Tutorial_ManageBlock_msgBlockOutsideArea_34 = msgBlockOutsideArea;
		logic_SubGraph_Crafting_Tutorial_ManageBlock_messageSpeaker_34 = messageSpeaker;
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_34.In(ref logic_SubGraph_Crafting_Tutorial_ManageBlock_blockSpawnData_34, logic_SubGraph_Crafting_Tutorial_ManageBlock_blockIndex_34, ref logic_SubGraph_Crafting_Tutorial_ManageBlock_block_34, logic_SubGraph_Crafting_Tutorial_ManageBlock_allowAnchoring_34, logic_SubGraph_Crafting_Tutorial_ManageBlock_msgBlockOutsideArea_34, logic_SubGraph_Crafting_Tutorial_ManageBlock_messageSpeaker_34);
	}

	private void Relay_In_35()
	{
		logic_uScriptCon_CompareBool_Bool_35 = local_msgIntroShown_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_35.In(logic_uScriptCon_CompareBool_Bool_35);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_35.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_35.False;
		if (num)
		{
			Relay_In_2();
		}
		if (flag)
		{
			Relay_True_39();
		}
	}

	private void Relay_In_37()
	{
		logic_uScript_IsPlayerInRangeOfTech_tech_37 = local_CraftingBaseTech_Tank;
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_37.In(logic_uScript_IsPlayerInRangeOfTech_tech_37, logic_uScript_IsPlayerInRangeOfTech_range_37, logic_uScript_IsPlayerInRangeOfTech_techs_37);
		if (logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_37.InRange)
		{
			Relay_In_35();
		}
	}

	private void Relay_True_39()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_39.True(out logic_uScriptAct_SetBool_Target_39);
		local_msgIntroShown_System_Boolean = logic_uScriptAct_SetBool_Target_39;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_39.Out)
		{
			Relay_In_231();
		}
	}

	private void Relay_False_39()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_39.False(out logic_uScriptAct_SetBool_Target_39);
		local_msgIntroShown_System_Boolean = logic_uScriptAct_SetBool_Target_39;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_39.Out)
		{
			Relay_In_231();
		}
	}

	private void Relay_True_40()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_40.True(out logic_uScriptAct_SetBool_Target_40);
		local_NearBase_System_Boolean = logic_uScriptAct_SetBool_Target_40;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_40.Out)
		{
			Relay_In_75();
		}
	}

	private void Relay_False_40()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_40.False(out logic_uScriptAct_SetBool_Target_40);
		local_NearBase_System_Boolean = logic_uScriptAct_SetBool_Target_40;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_40.Out)
		{
			Relay_In_75();
		}
	}

	private void Relay_In_42()
	{
		logic_uScript_RestrictItemPickup_tech_42 = local_CraftingBaseTech_Tank;
		logic_uScript_RestrictItemPickup_uScript_RestrictItemPickup_42.In(logic_uScript_RestrictItemPickup_tech_42, logic_uScript_RestrictItemPickup_typesToAccept_42);
		if (logic_uScript_RestrictItemPickup_uScript_RestrictItemPickup_42.Out)
		{
			Relay_In_283();
		}
	}

	private void Relay_Save_Out_44()
	{
		Relay_Save_201();
	}

	private void Relay_Load_Out_44()
	{
		Relay_Load_201();
	}

	private void Relay_Restart_Out_44()
	{
		Relay_Restart_201();
	}

	private void Relay_Save_44()
	{
		logic_SubGraph_SaveLoadInt_integer_44 = local_NumConveyorsAttached_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_44 = local_NumConveyorsAttached_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_44.Save(logic_SubGraph_SaveLoadInt_restartValue_44, ref logic_SubGraph_SaveLoadInt_integer_44, logic_SubGraph_SaveLoadInt_intAsVariable_44, logic_SubGraph_SaveLoadInt_uniqueID_44);
	}

	private void Relay_Load_44()
	{
		logic_SubGraph_SaveLoadInt_integer_44 = local_NumConveyorsAttached_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_44 = local_NumConveyorsAttached_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_44.Load(logic_SubGraph_SaveLoadInt_restartValue_44, ref logic_SubGraph_SaveLoadInt_integer_44, logic_SubGraph_SaveLoadInt_intAsVariable_44, logic_SubGraph_SaveLoadInt_uniqueID_44);
	}

	private void Relay_Restart_44()
	{
		logic_SubGraph_SaveLoadInt_integer_44 = local_NumConveyorsAttached_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_44 = local_NumConveyorsAttached_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_44.Restart(logic_SubGraph_SaveLoadInt_restartValue_44, ref logic_SubGraph_SaveLoadInt_integer_44, logic_SubGraph_SaveLoadInt_intAsVariable_44, logic_SubGraph_SaveLoadInt_uniqueID_44);
	}

	private void Relay_Save_Out_47()
	{
		Relay_Save_48();
	}

	private void Relay_Load_Out_47()
	{
		Relay_Load_48();
	}

	private void Relay_Restart_Out_47()
	{
		Relay_Set_False_48();
	}

	private void Relay_Save_47()
	{
		logic_SubGraph_SaveLoadBool_boolean_47 = local_msgIntroShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_47 = local_msgIntroShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_47.Save(ref logic_SubGraph_SaveLoadBool_boolean_47, logic_SubGraph_SaveLoadBool_boolAsVariable_47, logic_SubGraph_SaveLoadBool_uniqueID_47);
	}

	private void Relay_Load_47()
	{
		logic_SubGraph_SaveLoadBool_boolean_47 = local_msgIntroShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_47 = local_msgIntroShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_47.Load(ref logic_SubGraph_SaveLoadBool_boolean_47, logic_SubGraph_SaveLoadBool_boolAsVariable_47, logic_SubGraph_SaveLoadBool_uniqueID_47);
	}

	private void Relay_Set_True_47()
	{
		logic_SubGraph_SaveLoadBool_boolean_47 = local_msgIntroShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_47 = local_msgIntroShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_47.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_47, logic_SubGraph_SaveLoadBool_boolAsVariable_47, logic_SubGraph_SaveLoadBool_uniqueID_47);
	}

	private void Relay_Set_False_47()
	{
		logic_SubGraph_SaveLoadBool_boolean_47 = local_msgIntroShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_47 = local_msgIntroShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_47.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_47, logic_SubGraph_SaveLoadBool_boolAsVariable_47, logic_SubGraph_SaveLoadBool_uniqueID_47);
	}

	private void Relay_Save_Out_48()
	{
		Relay_Save_49();
	}

	private void Relay_Load_Out_48()
	{
		Relay_Load_49();
	}

	private void Relay_Restart_Out_48()
	{
		Relay_Set_False_49();
	}

	private void Relay_Save_48()
	{
		logic_SubGraph_SaveLoadBool_boolean_48 = local_msgBaseFoundShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_48 = local_msgBaseFoundShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_48.Save(ref logic_SubGraph_SaveLoadBool_boolean_48, logic_SubGraph_SaveLoadBool_boolAsVariable_48, logic_SubGraph_SaveLoadBool_uniqueID_48);
	}

	private void Relay_Load_48()
	{
		logic_SubGraph_SaveLoadBool_boolean_48 = local_msgBaseFoundShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_48 = local_msgBaseFoundShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_48.Load(ref logic_SubGraph_SaveLoadBool_boolean_48, logic_SubGraph_SaveLoadBool_boolAsVariable_48, logic_SubGraph_SaveLoadBool_uniqueID_48);
	}

	private void Relay_Set_True_48()
	{
		logic_SubGraph_SaveLoadBool_boolean_48 = local_msgBaseFoundShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_48 = local_msgBaseFoundShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_48.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_48, logic_SubGraph_SaveLoadBool_boolAsVariable_48, logic_SubGraph_SaveLoadBool_uniqueID_48);
	}

	private void Relay_Set_False_48()
	{
		logic_SubGraph_SaveLoadBool_boolean_48 = local_msgBaseFoundShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_48 = local_msgBaseFoundShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_48.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_48, logic_SubGraph_SaveLoadBool_boolAsVariable_48, logic_SubGraph_SaveLoadBool_uniqueID_48);
	}

	private void Relay_Save_Out_49()
	{
		Relay_Save_53();
	}

	private void Relay_Load_Out_49()
	{
		Relay_Load_53();
	}

	private void Relay_Restart_Out_49()
	{
		Relay_Set_False_53();
	}

	private void Relay_Save_49()
	{
		logic_SubGraph_SaveLoadBool_boolean_49 = local_msgReceiverAttachedShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_49 = local_msgReceiverAttachedShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_49.Save(ref logic_SubGraph_SaveLoadBool_boolean_49, logic_SubGraph_SaveLoadBool_boolAsVariable_49, logic_SubGraph_SaveLoadBool_uniqueID_49);
	}

	private void Relay_Load_49()
	{
		logic_SubGraph_SaveLoadBool_boolean_49 = local_msgReceiverAttachedShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_49 = local_msgReceiverAttachedShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_49.Load(ref logic_SubGraph_SaveLoadBool_boolean_49, logic_SubGraph_SaveLoadBool_boolAsVariable_49, logic_SubGraph_SaveLoadBool_uniqueID_49);
	}

	private void Relay_Set_True_49()
	{
		logic_SubGraph_SaveLoadBool_boolean_49 = local_msgReceiverAttachedShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_49 = local_msgReceiverAttachedShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_49.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_49, logic_SubGraph_SaveLoadBool_boolAsVariable_49, logic_SubGraph_SaveLoadBool_uniqueID_49);
	}

	private void Relay_Set_False_49()
	{
		logic_SubGraph_SaveLoadBool_boolean_49 = local_msgReceiverAttachedShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_49 = local_msgReceiverAttachedShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_49.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_49, logic_SubGraph_SaveLoadBool_boolAsVariable_49, logic_SubGraph_SaveLoadBool_uniqueID_49);
	}

	private void Relay_Save_Out_53()
	{
		Relay_Save_56();
	}

	private void Relay_Load_Out_53()
	{
		Relay_Load_56();
	}

	private void Relay_Restart_Out_53()
	{
		Relay_Set_False_56();
	}

	private void Relay_Save_53()
	{
		logic_SubGraph_SaveLoadBool_boolean_53 = local_msgConveyorsAttachedShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_53 = local_msgConveyorsAttachedShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_53.Save(ref logic_SubGraph_SaveLoadBool_boolean_53, logic_SubGraph_SaveLoadBool_boolAsVariable_53, logic_SubGraph_SaveLoadBool_uniqueID_53);
	}

	private void Relay_Load_53()
	{
		logic_SubGraph_SaveLoadBool_boolean_53 = local_msgConveyorsAttachedShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_53 = local_msgConveyorsAttachedShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_53.Load(ref logic_SubGraph_SaveLoadBool_boolean_53, logic_SubGraph_SaveLoadBool_boolAsVariable_53, logic_SubGraph_SaveLoadBool_uniqueID_53);
	}

	private void Relay_Set_True_53()
	{
		logic_SubGraph_SaveLoadBool_boolean_53 = local_msgConveyorsAttachedShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_53 = local_msgConveyorsAttachedShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_53.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_53, logic_SubGraph_SaveLoadBool_boolAsVariable_53, logic_SubGraph_SaveLoadBool_uniqueID_53);
	}

	private void Relay_Set_False_53()
	{
		logic_SubGraph_SaveLoadBool_boolean_53 = local_msgConveyorsAttachedShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_53 = local_msgConveyorsAttachedShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_53.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_53, logic_SubGraph_SaveLoadBool_boolAsVariable_53, logic_SubGraph_SaveLoadBool_uniqueID_53);
	}

	private void Relay_In_54()
	{
		logic_uScript_HideArrow_uScript_HideArrow_54.In();
		if (logic_uScript_HideArrow_uScript_HideArrow_54.Out)
		{
			Relay_In_349();
		}
	}

	private void Relay_Save_Out_56()
	{
		Relay_Save_58();
	}

	private void Relay_Load_Out_56()
	{
		Relay_Load_58();
	}

	private void Relay_Restart_Out_56()
	{
		Relay_Set_False_58();
	}

	private void Relay_Save_56()
	{
		logic_SubGraph_SaveLoadBool_boolean_56 = local_FabricatorSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_56 = local_FabricatorSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_56.Save(ref logic_SubGraph_SaveLoadBool_boolean_56, logic_SubGraph_SaveLoadBool_boolAsVariable_56, logic_SubGraph_SaveLoadBool_uniqueID_56);
	}

	private void Relay_Load_56()
	{
		logic_SubGraph_SaveLoadBool_boolean_56 = local_FabricatorSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_56 = local_FabricatorSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_56.Load(ref logic_SubGraph_SaveLoadBool_boolean_56, logic_SubGraph_SaveLoadBool_boolAsVariable_56, logic_SubGraph_SaveLoadBool_uniqueID_56);
	}

	private void Relay_Set_True_56()
	{
		logic_SubGraph_SaveLoadBool_boolean_56 = local_FabricatorSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_56 = local_FabricatorSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_56.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_56, logic_SubGraph_SaveLoadBool_boolAsVariable_56, logic_SubGraph_SaveLoadBool_uniqueID_56);
	}

	private void Relay_Set_False_56()
	{
		logic_SubGraph_SaveLoadBool_boolean_56 = local_FabricatorSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_56 = local_FabricatorSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_56.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_56, logic_SubGraph_SaveLoadBool_boolAsVariable_56, logic_SubGraph_SaveLoadBool_uniqueID_56);
	}

	private void Relay_Save_Out_58()
	{
		Relay_Save_376();
	}

	private void Relay_Load_Out_58()
	{
		Relay_Load_376();
	}

	private void Relay_Restart_Out_58()
	{
		Relay_Set_False_376();
	}

	private void Relay_Save_58()
	{
		logic_SubGraph_SaveLoadBool_boolean_58 = local_msgFabricatorSpawnedShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_58 = local_msgFabricatorSpawnedShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_58.Save(ref logic_SubGraph_SaveLoadBool_boolean_58, logic_SubGraph_SaveLoadBool_boolAsVariable_58, logic_SubGraph_SaveLoadBool_uniqueID_58);
	}

	private void Relay_Load_58()
	{
		logic_SubGraph_SaveLoadBool_boolean_58 = local_msgFabricatorSpawnedShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_58 = local_msgFabricatorSpawnedShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_58.Load(ref logic_SubGraph_SaveLoadBool_boolean_58, logic_SubGraph_SaveLoadBool_boolAsVariable_58, logic_SubGraph_SaveLoadBool_uniqueID_58);
	}

	private void Relay_Set_True_58()
	{
		logic_SubGraph_SaveLoadBool_boolean_58 = local_msgFabricatorSpawnedShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_58 = local_msgFabricatorSpawnedShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_58.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_58, logic_SubGraph_SaveLoadBool_boolAsVariable_58, logic_SubGraph_SaveLoadBool_uniqueID_58);
	}

	private void Relay_Set_False_58()
	{
		logic_SubGraph_SaveLoadBool_boolean_58 = local_msgFabricatorSpawnedShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_58 = local_msgFabricatorSpawnedShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_58.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_58, logic_SubGraph_SaveLoadBool_boolAsVariable_58, logic_SubGraph_SaveLoadBool_uniqueID_58);
	}

	private void Relay_True_60()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_60.True(out logic_uScriptAct_SetBool_Target_60);
		local_GhostBlockConveyor01Spawned_System_Boolean = logic_uScriptAct_SetBool_Target_60;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_60.Out)
		{
			Relay_False_61();
		}
	}

	private void Relay_False_60()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_60.False(out logic_uScriptAct_SetBool_Target_60);
		local_GhostBlockConveyor01Spawned_System_Boolean = logic_uScriptAct_SetBool_Target_60;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_60.Out)
		{
			Relay_False_61();
		}
	}

	private void Relay_True_61()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_61.True(out logic_uScriptAct_SetBool_Target_61);
		local_GhostBlockConveyor02Spawned_System_Boolean = logic_uScriptAct_SetBool_Target_61;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_61.Out)
		{
			Relay_False_68();
		}
	}

	private void Relay_False_61()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_61.False(out logic_uScriptAct_SetBool_Target_61);
		local_GhostBlockConveyor02Spawned_System_Boolean = logic_uScriptAct_SetBool_Target_61;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_61.Out)
		{
			Relay_False_68();
		}
	}

	private void Relay_True_64()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_64.True(out logic_uScriptAct_SetBool_Target_64);
		local_BlockCrafted_System_Boolean = logic_uScriptAct_SetBool_Target_64;
	}

	private void Relay_False_64()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_64.False(out logic_uScriptAct_SetBool_Target_64);
		local_BlockCrafted_System_Boolean = logic_uScriptAct_SetBool_Target_64;
	}

	private void Relay_Save_Out_65()
	{
	}

	private void Relay_Load_Out_65()
	{
		Relay_In_203();
	}

	private void Relay_Restart_Out_65()
	{
		Relay_False_60();
	}

	private void Relay_Save_65()
	{
		logic_SubGraph_SaveLoadBool_boolean_65 = local_BlockCrafted_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_65 = local_BlockCrafted_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_65.Save(ref logic_SubGraph_SaveLoadBool_boolean_65, logic_SubGraph_SaveLoadBool_boolAsVariable_65, logic_SubGraph_SaveLoadBool_uniqueID_65);
	}

	private void Relay_Load_65()
	{
		logic_SubGraph_SaveLoadBool_boolean_65 = local_BlockCrafted_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_65 = local_BlockCrafted_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_65.Load(ref logic_SubGraph_SaveLoadBool_boolean_65, logic_SubGraph_SaveLoadBool_boolAsVariable_65, logic_SubGraph_SaveLoadBool_uniqueID_65);
	}

	private void Relay_Set_True_65()
	{
		logic_SubGraph_SaveLoadBool_boolean_65 = local_BlockCrafted_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_65 = local_BlockCrafted_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_65.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_65, logic_SubGraph_SaveLoadBool_boolAsVariable_65, logic_SubGraph_SaveLoadBool_uniqueID_65);
	}

	private void Relay_Set_False_65()
	{
		logic_SubGraph_SaveLoadBool_boolean_65 = local_BlockCrafted_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_65 = local_BlockCrafted_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_65.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_65, logic_SubGraph_SaveLoadBool_boolAsVariable_65, logic_SubGraph_SaveLoadBool_uniqueID_65);
	}

	private void Relay_True_68()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_68.True(out logic_uScriptAct_SetBool_Target_68);
		local_NearBase_System_Boolean = logic_uScriptAct_SetBool_Target_68;
	}

	private void Relay_False_68()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_68.False(out logic_uScriptAct_SetBool_Target_68);
		local_NearBase_System_Boolean = logic_uScriptAct_SetBool_Target_68;
	}

	private void Relay_Out_70()
	{
		Relay_In_24();
	}

	private void Relay_In_70()
	{
		int num = 0;
		Array array = blockSpawnDataFabricator;
		if (logic_SubGraph_Crafting_Tutorial_ManageBlock_blockSpawnData_70.Length != num + array.Length)
		{
			Array.Resize(ref logic_SubGraph_Crafting_Tutorial_ManageBlock_blockSpawnData_70, num + array.Length);
		}
		Array.Copy(array, 0, logic_SubGraph_Crafting_Tutorial_ManageBlock_blockSpawnData_70, num, array.Length);
		num += array.Length;
		logic_SubGraph_Crafting_Tutorial_ManageBlock_block_70 = local_FabricatorBlock_TankBlock;
		logic_SubGraph_Crafting_Tutorial_ManageBlock_msgBlockOutsideArea_70 = msgBlockOutsideArea;
		logic_SubGraph_Crafting_Tutorial_ManageBlock_messageSpeaker_70 = messageSpeaker;
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_70.In(ref logic_SubGraph_Crafting_Tutorial_ManageBlock_blockSpawnData_70, logic_SubGraph_Crafting_Tutorial_ManageBlock_blockIndex_70, ref logic_SubGraph_Crafting_Tutorial_ManageBlock_block_70, logic_SubGraph_Crafting_Tutorial_ManageBlock_allowAnchoring_70, logic_SubGraph_Crafting_Tutorial_ManageBlock_msgBlockOutsideArea_70, logic_SubGraph_Crafting_Tutorial_ManageBlock_messageSpeaker_70);
	}

	private void Relay_In_73()
	{
		logic_uScriptCon_CompareBool_Bool_73 = local_FabricatorSpawned_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_73.In(logic_uScriptCon_CompareBool_Bool_73);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_73.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_73.False;
		if (num)
		{
			Relay_In_70();
		}
		if (flag)
		{
			Relay_In_74();
		}
	}

	private void Relay_In_74()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_74.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_74.Out)
		{
			Relay_In_24();
		}
	}

	private void Relay_Output1_75()
	{
		Relay_In_77();
	}

	private void Relay_Output2_75()
	{
		Relay_In_223();
	}

	private void Relay_Output3_75()
	{
		Relay_In_217();
	}

	private void Relay_Output4_75()
	{
	}

	private void Relay_Output5_75()
	{
	}

	private void Relay_Output6_75()
	{
	}

	private void Relay_Output7_75()
	{
	}

	private void Relay_Output8_75()
	{
	}

	private void Relay_In_75()
	{
		logic_uScriptCon_ManualSwitch_CurrentOutput_75 = local_Stage_System_Int32;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_75.In(logic_uScriptCon_ManualSwitch_CurrentOutput_75);
	}

	private void Relay_In_77()
	{
		logic_uScript_IsPlayerInRangeOfTech_tech_77 = local_CraftingBaseTech_Tank;
		logic_uScript_IsPlayerInRangeOfTech_range_77 = distBaseFound;
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_77.In(logic_uScript_IsPlayerInRangeOfTech_tech_77, logic_uScript_IsPlayerInRangeOfTech_range_77, logic_uScript_IsPlayerInRangeOfTech_techs_77);
		if (logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_77.InRange)
		{
			Relay_In_226();
		}
	}

	private void Relay_In_81()
	{
		logic_uScriptCon_CompareBool_Bool_81 = local_msgBaseFoundShown_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_81.In(logic_uScriptCon_CompareBool_Bool_81);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_81.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_81.False;
		if (num)
		{
			Relay_In_236();
		}
		if (flag)
		{
			Relay_In_234();
		}
	}

	private void Relay_Block_Attached_84()
	{
		Relay_In_207();
	}

	private void Relay_In_84()
	{
		logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_Block_84 = local_ReceiverBlock_TankBlock;
		logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_CraftingBaseTech_84 = local_CraftingBaseTech_Tank;
		int num = 0;
		Array array = ghostBlockReceiver;
		if (logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_ghostBlockData_84.Length != num + array.Length)
		{
			Array.Resize(ref logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_ghostBlockData_84, num + array.Length);
		}
		Array.Copy(array, 0, logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_ghostBlockData_84, num, array.Length);
		num += array.Length;
		logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_SubGraph_Crafting_Tutorial_AttachBlockToBase_84.In(logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_Block_84, logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_CraftingBaseTech_84, logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_ghostBlockData_84, logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_BlockType_84, logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_BlockPosition_84);
	}

	private void Relay_True_87()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_87.True(out logic_uScriptAct_SetBool_Target_87);
		local_msgBaseFoundShown_System_Boolean = logic_uScriptAct_SetBool_Target_87;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_87.Out)
		{
			Relay_In_84();
		}
	}

	private void Relay_False_87()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_87.False(out logic_uScriptAct_SetBool_Target_87);
		local_msgBaseFoundShown_System_Boolean = logic_uScriptAct_SetBool_Target_87;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_87.Out)
		{
			Relay_In_84();
		}
	}

	private void Relay_True_88()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_88.True(out logic_uScriptAct_SetBool_Target_88);
		local_Resource01Spawned_System_Boolean = logic_uScriptAct_SetBool_Target_88;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_88.Out)
		{
			Relay_In_289();
		}
	}

	private void Relay_False_88()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_88.False(out logic_uScriptAct_SetBool_Target_88);
		local_Resource01Spawned_System_Boolean = logic_uScriptAct_SetBool_Target_88;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_88.Out)
		{
			Relay_In_289();
		}
	}

	private void Relay_In_91()
	{
		logic_uScript_Wait_seconds_91 = timeWaitForChunksInSilos;
		logic_uScript_Wait_uScript_Wait_91.In(logic_uScript_Wait_seconds_91, logic_uScript_Wait_repeat_91);
		if (logic_uScript_Wait_uScript_Wait_91.Waited)
		{
			Relay_In_243();
		}
	}

	private void Relay_In_92()
	{
		logic_uScriptCon_CompareBool_Bool_92 = local_msgReceiverAttachedShown_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_92.In(logic_uScriptCon_CompareBool_Bool_92);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_92.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_92.False;
		if (num)
		{
			Relay_In_94();
		}
		if (flag)
		{
			Relay_In_238();
		}
	}

	private void Relay_In_94()
	{
		logic_uScriptCon_CompareBool_Bool_94 = local_Resource01Spawned_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_94.In(logic_uScriptCon_CompareBool_Bool_94);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_94.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_94.False;
		if (num)
		{
			Relay_In_91();
		}
		if (flag)
		{
			Relay_True_88();
		}
	}

	private void Relay_True_96()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_96.True(out logic_uScriptAct_SetBool_Target_96);
		local_msgReceiverAttachedShown_System_Boolean = logic_uScriptAct_SetBool_Target_96;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_96.Out)
		{
			Relay_In_94();
		}
	}

	private void Relay_False_96()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_96.False(out logic_uScriptAct_SetBool_Target_96);
		local_msgReceiverAttachedShown_System_Boolean = logic_uScriptAct_SetBool_Target_96;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_96.Out)
		{
			Relay_In_94();
		}
	}

	private void Relay_In_99()
	{
		logic_uScript_HideArrow_uScript_HideArrow_99.In();
		if (logic_uScript_HideArrow_uScript_HideArrow_99.Out)
		{
			Relay_In_149();
		}
	}

	private void Relay_In_100()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_100.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_100.Out)
		{
			Relay_True_131();
		}
	}

	private void Relay_AtIndex_101()
	{
		int num = 0;
		Array array = local_125_TankBlockArray;
		if (logic_uScript_AccessListBlock_blockList_101.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListBlock_blockList_101, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListBlock_blockList_101, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListBlock_uScript_AccessListBlock_101.AtIndex(ref logic_uScript_AccessListBlock_blockList_101, logic_uScript_AccessListBlock_index_101, out logic_uScript_AccessListBlock_value_101);
		local_125_TankBlockArray = logic_uScript_AccessListBlock_blockList_101;
		local_GhostBlockConveyor02_TankBlock = logic_uScript_AccessListBlock_value_101;
		if (logic_uScript_AccessListBlock_uScript_AccessListBlock_101.Out)
		{
			Relay_In_105();
		}
	}

	private void Relay_In_105()
	{
		logic_uScript_PointArrowAtVisible_targetObject_105 = local_GhostBlockConveyor02_TankBlock;
		logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_105.In(logic_uScript_PointArrowAtVisible_targetObject_105, logic_uScript_PointArrowAtVisible_timeToShowFor_105, logic_uScript_PointArrowAtVisible_offset_105);
		if (logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_105.Out)
		{
			Relay_In_344();
		}
	}

	private void Relay_AtIndex_111()
	{
		int num = 0;
		Array array = local_107_TankBlockArray;
		if (logic_uScript_AccessListBlock_blockList_111.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListBlock_blockList_111, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListBlock_blockList_111, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListBlock_uScript_AccessListBlock_111.AtIndex(ref logic_uScript_AccessListBlock_blockList_111, logic_uScript_AccessListBlock_index_111, out logic_uScript_AccessListBlock_value_111);
		local_107_TankBlockArray = logic_uScript_AccessListBlock_blockList_111;
		local_GhostBlockConveyor01_TankBlock = logic_uScript_AccessListBlock_value_111;
		if (logic_uScript_AccessListBlock_uScript_AccessListBlock_111.Out)
		{
			Relay_In_156();
		}
	}

	private void Relay_In_112()
	{
		logic_uScript_DoesTechHaveBlockAtPosition_tech_112 = local_CraftingBaseTech_Tank;
		logic_uScript_DoesTechHaveBlockAtPosition_blockType_112 = blockTypeConveyor;
		logic_uScript_DoesTechHaveBlockAtPosition_uScript_DoesTechHaveBlockAtPosition_112.In(logic_uScript_DoesTechHaveBlockAtPosition_tech_112, logic_uScript_DoesTechHaveBlockAtPosition_blockType_112, logic_uScript_DoesTechHaveBlockAtPosition_localPosition_112);
		if (logic_uScript_DoesTechHaveBlockAtPosition_uScript_DoesTechHaveBlockAtPosition_112.True)
		{
			Relay_In_141();
		}
	}

	private void Relay_Output1_113()
	{
	}

	private void Relay_Output2_113()
	{
		Relay_In_138();
	}

	private void Relay_Output3_113()
	{
		Relay_In_140();
	}

	private void Relay_Output4_113()
	{
	}

	private void Relay_Output5_113()
	{
	}

	private void Relay_Output6_113()
	{
	}

	private void Relay_Output7_113()
	{
	}

	private void Relay_Output8_113()
	{
	}

	private void Relay_In_113()
	{
		logic_uScriptCon_ManualSwitch_CurrentOutput_113 = local_NumConveyorsAttached_System_Int32;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_113.In(logic_uScriptCon_ManualSwitch_CurrentOutput_113);
	}

	private void Relay_Output1_115()
	{
	}

	private void Relay_Output2_115()
	{
		Relay_In_112();
	}

	private void Relay_Output3_115()
	{
		Relay_In_142();
	}

	private void Relay_Output4_115()
	{
	}

	private void Relay_Output5_115()
	{
	}

	private void Relay_Output6_115()
	{
	}

	private void Relay_Output7_115()
	{
	}

	private void Relay_Output8_115()
	{
	}

	private void Relay_In_115()
	{
		logic_uScriptCon_ManualSwitch_CurrentOutput_115 = local_NumConveyorsAttached_System_Int32;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_115.In(logic_uScriptCon_ManualSwitch_CurrentOutput_115);
	}

	private void Relay_In_116()
	{
		logic_uScript_BlockAttachedToTech_tech_116 = local_CraftingBaseTech_Tank;
		logic_uScript_BlockAttachedToTech_block_116 = local_ConveyorBlock02_TankBlock;
		logic_uScript_BlockAttachedToTech_uScript_BlockAttachedToTech_116.In(logic_uScript_BlockAttachedToTech_tech_116, logic_uScript_BlockAttachedToTech_block_116);
		bool num = logic_uScript_BlockAttachedToTech_uScript_BlockAttachedToTech_116.True;
		bool flag = logic_uScript_BlockAttachedToTech_uScript_BlockAttachedToTech_116.False;
		if (num)
		{
			Relay_In_336();
		}
		if (flag)
		{
			Relay_In_126();
		}
	}

	private void Relay_In_117()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_117.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_117.Out)
		{
			Relay_In_115();
		}
	}

	private void Relay_True_118()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_118.True(out logic_uScriptAct_SetBool_Target_118);
		local_GhostBlockConveyor02Spawned_System_Boolean = logic_uScriptAct_SetBool_Target_118;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_118.Out)
		{
			Relay_TrySpawnOnTech_132();
		}
	}

	private void Relay_False_118()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_118.False(out logic_uScriptAct_SetBool_Target_118);
		local_GhostBlockConveyor02Spawned_System_Boolean = logic_uScriptAct_SetBool_Target_118;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_118.Out)
		{
			Relay_TrySpawnOnTech_132();
		}
	}

	private void Relay_In_122()
	{
		logic_uScript_BlockAttachedToTech_tech_122 = local_CraftingBaseTech_Tank;
		logic_uScript_BlockAttachedToTech_block_122 = local_ConveyorBlock01_TankBlock;
		logic_uScript_BlockAttachedToTech_uScript_BlockAttachedToTech_122.In(logic_uScript_BlockAttachedToTech_tech_122, logic_uScript_BlockAttachedToTech_block_122);
		bool num = logic_uScript_BlockAttachedToTech_uScript_BlockAttachedToTech_122.True;
		bool flag = logic_uScript_BlockAttachedToTech_uScript_BlockAttachedToTech_122.False;
		if (num)
		{
			Relay_In_333();
		}
		if (flag)
		{
			Relay_In_150();
		}
	}

	private void Relay_In_126()
	{
		logic_uScript_PointArrowAtVisible_targetObject_126 = local_ConveyorBlock02_TankBlock;
		logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_126.In(logic_uScript_PointArrowAtVisible_targetObject_126, logic_uScript_PointArrowAtVisible_timeToShowFor_126, logic_uScript_PointArrowAtVisible_offset_126);
		if (logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_126.Out)
		{
			Relay_In_331();
		}
	}

	private void Relay_In_130()
	{
		logic_uScript_IsPlayerInteractingWithBlock_block_130 = local_ConveyorBlock02_TankBlock;
		logic_uScript_IsPlayerInteractingWithBlock_uScript_IsPlayerInteractingWithBlock_130.In(logic_uScript_IsPlayerInteractingWithBlock_block_130);
		bool dragging = logic_uScript_IsPlayerInteractingWithBlock_uScript_IsPlayerInteractingWithBlock_130.Dragging;
		bool notDragging = logic_uScript_IsPlayerInteractingWithBlock_uScript_IsPlayerInteractingWithBlock_130.NotDragging;
		if (dragging)
		{
			Relay_True_131();
		}
		if (notDragging)
		{
			Relay_False_131();
		}
	}

	private void Relay_True_131()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_131.True(out logic_uScriptAct_SetBool_Target_131);
		local_DraggingConveyor_System_Boolean = logic_uScriptAct_SetBool_Target_131;
		bool setTrue = logic_uScriptAct_SetBool_uScriptAct_SetBool_131.SetTrue;
		bool setFalse = logic_uScriptAct_SetBool_uScriptAct_SetBool_131.SetFalse;
		if (setTrue)
		{
			Relay_In_338();
		}
		if (setFalse)
		{
			Relay_In_322();
		}
	}

	private void Relay_False_131()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_131.False(out logic_uScriptAct_SetBool_Target_131);
		local_DraggingConveyor_System_Boolean = logic_uScriptAct_SetBool_Target_131;
		bool setTrue = logic_uScriptAct_SetBool_uScriptAct_SetBool_131.SetTrue;
		bool setFalse = logic_uScriptAct_SetBool_uScriptAct_SetBool_131.SetFalse;
		if (setTrue)
		{
			Relay_In_338();
		}
		if (setFalse)
		{
			Relay_In_322();
		}
	}

	private void Relay_TrySpawnOnTech_132()
	{
		int num = 0;
		Array array = ghostBlockConveyor02;
		if (logic_uScript_SpawnGhostBlocks_ghostBlockData_132.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SpawnGhostBlocks_ghostBlockData_132, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SpawnGhostBlocks_ghostBlockData_132, num, array.Length);
		num += array.Length;
		logic_uScript_SpawnGhostBlocks_ownerNode_132 = owner_Connection_121;
		logic_uScript_SpawnGhostBlocks_targetTech_132 = local_CraftingBaseTech_Tank;
		logic_uScript_SpawnGhostBlocks_Return_132 = logic_uScript_SpawnGhostBlocks_uScript_SpawnGhostBlocks_132.TrySpawnOnTech(logic_uScript_SpawnGhostBlocks_ghostBlockData_132, logic_uScript_SpawnGhostBlocks_ownerNode_132, logic_uScript_SpawnGhostBlocks_targetTech_132);
		local_125_TankBlockArray = logic_uScript_SpawnGhostBlocks_Return_132;
		if (logic_uScript_SpawnGhostBlocks_uScript_SpawnGhostBlocks_132.OnAlreadySpawned)
		{
			Relay_AtIndex_101();
		}
	}

	private void Relay_True_133()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_133.True(out logic_uScriptAct_SetBool_Target_133);
		local_GhostBlockConveyor01Spawned_System_Boolean = logic_uScriptAct_SetBool_Target_133;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_133.Out)
		{
			Relay_TrySpawnOnTech_135();
		}
	}

	private void Relay_False_133()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_133.False(out logic_uScriptAct_SetBool_Target_133);
		local_GhostBlockConveyor01Spawned_System_Boolean = logic_uScriptAct_SetBool_Target_133;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_133.Out)
		{
			Relay_TrySpawnOnTech_135();
		}
	}

	private void Relay_In_134()
	{
		logic_uScriptCon_CompareInt_A_134 = local_NumConveyorsAttached_System_Int32;
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_134.In(logic_uScriptCon_CompareInt_A_134, logic_uScriptCon_CompareInt_B_134);
		bool equalTo = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_134.EqualTo;
		bool notEqualTo = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_134.NotEqualTo;
		if (equalTo)
		{
			Relay_In_212();
		}
		if (notEqualTo)
		{
			Relay_In_245();
		}
	}

	private void Relay_TrySpawnOnTech_135()
	{
		int num = 0;
		Array array = ghostBlockConveyor01;
		if (logic_uScript_SpawnGhostBlocks_ghostBlockData_135.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SpawnGhostBlocks_ghostBlockData_135, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SpawnGhostBlocks_ghostBlockData_135, num, array.Length);
		num += array.Length;
		logic_uScript_SpawnGhostBlocks_ownerNode_135 = owner_Connection_110;
		logic_uScript_SpawnGhostBlocks_targetTech_135 = local_CraftingBaseTech_Tank;
		logic_uScript_SpawnGhostBlocks_Return_135 = logic_uScript_SpawnGhostBlocks_uScript_SpawnGhostBlocks_135.TrySpawnOnTech(logic_uScript_SpawnGhostBlocks_ghostBlockData_135, logic_uScript_SpawnGhostBlocks_ownerNode_135, logic_uScript_SpawnGhostBlocks_targetTech_135);
		local_107_TankBlockArray = logic_uScript_SpawnGhostBlocks_Return_135;
		if (logic_uScript_SpawnGhostBlocks_uScript_SpawnGhostBlocks_135.OnAlreadySpawned)
		{
			Relay_AtIndex_111();
		}
	}

	private void Relay_In_136()
	{
		logic_uScript_IsPlayerInteractingWithBlock_block_136 = local_ConveyorBlock01_TankBlock;
		logic_uScript_IsPlayerInteractingWithBlock_uScript_IsPlayerInteractingWithBlock_136.In(logic_uScript_IsPlayerInteractingWithBlock_block_136);
		bool dragging = logic_uScript_IsPlayerInteractingWithBlock_uScript_IsPlayerInteractingWithBlock_136.Dragging;
		bool notDragging = logic_uScript_IsPlayerInteractingWithBlock_uScript_IsPlayerInteractingWithBlock_136.NotDragging;
		if (dragging)
		{
			Relay_In_100();
		}
		if (notDragging)
		{
			Relay_In_130();
		}
	}

	private void Relay_In_138()
	{
		logic_uScriptCon_CompareBool_Bool_138 = local_GhostBlockConveyor01Spawned_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_138.In(logic_uScriptCon_CompareBool_Bool_138);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_138.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_138.False;
		if (num)
		{
			Relay_AtIndex_111();
		}
		if (flag)
		{
			Relay_True_133();
		}
	}

	private void Relay_In_140()
	{
		logic_uScriptCon_CompareBool_Bool_140 = local_GhostBlockConveyor02Spawned_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_140.In(logic_uScriptCon_CompareBool_Bool_140);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_140.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_140.False;
		if (num)
		{
			Relay_AtIndex_101();
		}
		if (flag)
		{
			Relay_True_118();
		}
	}

	private void Relay_In_141()
	{
		logic_uScript_RemoveAllGhostBlocksOnTech_targetTech_141 = local_CraftingBaseTech_Tank;
		logic_uScript_RemoveAllGhostBlocksOnTech_uScript_RemoveAllGhostBlocksOnTech_141.In(logic_uScript_RemoveAllGhostBlocksOnTech_targetTech_141);
		if (logic_uScript_RemoveAllGhostBlocksOnTech_uScript_RemoveAllGhostBlocksOnTech_141.Out)
		{
			Relay_In_99();
		}
	}

	private void Relay_In_142()
	{
		logic_uScript_DoesTechHaveBlockAtPosition_tech_142 = local_CraftingBaseTech_Tank;
		logic_uScript_DoesTechHaveBlockAtPosition_blockType_142 = blockTypeConveyor;
		logic_uScript_DoesTechHaveBlockAtPosition_uScript_DoesTechHaveBlockAtPosition_142.In(logic_uScript_DoesTechHaveBlockAtPosition_tech_142, logic_uScript_DoesTechHaveBlockAtPosition_blockType_142, logic_uScript_DoesTechHaveBlockAtPosition_localPosition_142);
		if (logic_uScript_DoesTechHaveBlockAtPosition_uScript_DoesTechHaveBlockAtPosition_142.True)
		{
			Relay_In_141();
		}
	}

	private void Relay_In_148()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_148.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_148.Out)
		{
			Relay_In_117();
		}
	}

	private void Relay_In_149()
	{
		logic_uScriptAct_AddInt_v2_A_149 = local_NumConveyorsAttached_System_Int32;
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_149.In(logic_uScriptAct_AddInt_v2_A_149, logic_uScriptAct_AddInt_v2_B_149, out logic_uScriptAct_AddInt_v2_IntResult_149, out logic_uScriptAct_AddInt_v2_FloatResult_149);
		local_NumConveyorsAttached_System_Int32 = logic_uScriptAct_AddInt_v2_IntResult_149;
	}

	private void Relay_In_150()
	{
		logic_uScript_PointArrowAtVisible_targetObject_150 = local_ConveyorBlock01_TankBlock;
		logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_150.In(logic_uScript_PointArrowAtVisible_targetObject_150, logic_uScript_PointArrowAtVisible_timeToShowFor_150, logic_uScript_PointArrowAtVisible_offset_150);
		if (logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_150.Out)
		{
			Relay_In_329();
		}
	}

	private void Relay_In_156()
	{
		logic_uScript_PointArrowAtVisible_targetObject_156 = local_GhostBlockConveyor01_TankBlock;
		logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_156.In(logic_uScript_PointArrowAtVisible_targetObject_156, logic_uScript_PointArrowAtVisible_timeToShowFor_156, logic_uScript_PointArrowAtVisible_offset_156);
		if (logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_156.Out)
		{
			Relay_In_342();
		}
	}

	private void Relay_In_159()
	{
		logic_uScriptCon_CompareBool_Bool_159 = local_msgConveyorsAttachedShown_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_159.In(logic_uScriptCon_CompareBool_Bool_159);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_159.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_159.False;
		if (num)
		{
			Relay_In_160();
		}
		if (flag)
		{
			Relay_In_249();
		}
	}

	private void Relay_In_160()
	{
		logic_uScriptCon_CompareBool_Bool_160 = local_FabricatorSpawned_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_160.In(logic_uScriptCon_CompareBool_Bool_160);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_160.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_160.False;
		if (num)
		{
			Relay_In_164();
		}
		if (flag)
		{
			Relay_True_176();
		}
	}

	private void Relay_In_164()
	{
		int num = 0;
		Array array = blockSpawnDataFabricator;
		if (logic_uScript_GetAndCheckBlocks_blockData_164.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckBlocks_blockData_164, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckBlocks_blockData_164, num, array.Length);
		num += array.Length;
		logic_uScript_GetAndCheckBlocks_ownerNode_164 = owner_Connection_157;
		logic_uScript_GetAndCheckBlocks_uScript_GetAndCheckBlocks_164.In(logic_uScript_GetAndCheckBlocks_blockData_164, logic_uScript_GetAndCheckBlocks_ownerNode_164, ref logic_uScript_GetAndCheckBlocks_blocks_164);
		bool allAlive = logic_uScript_GetAndCheckBlocks_uScript_GetAndCheckBlocks_164.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckBlocks_uScript_GetAndCheckBlocks_164.SomeAlive;
		if (allAlive)
		{
			Relay_In_172();
		}
		if (someAlive)
		{
			Relay_In_172();
		}
	}

	private void Relay_In_167()
	{
		int num = 0;
		Array array = blockSpawnDataFabricator;
		if (logic_uScript_SpawnBlocksFromData_blockData_167.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SpawnBlocksFromData_blockData_167, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SpawnBlocksFromData_blockData_167, num, array.Length);
		num += array.Length;
		logic_uScript_SpawnBlocksFromData_ownerNode_167 = owner_Connection_170;
		logic_uScript_SpawnBlocksFromData_uScript_SpawnBlocksFromData_167.In(logic_uScript_SpawnBlocksFromData_blockData_167, logic_uScript_SpawnBlocksFromData_ownerNode_167);
		if (logic_uScript_SpawnBlocksFromData_uScript_SpawnBlocksFromData_167.Out)
		{
			Relay_In_164();
		}
	}

	private void Relay_True_169()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_169.True(out logic_uScriptAct_SetBool_Target_169);
		local_msgFabricatorSpawnedShown_System_Boolean = logic_uScriptAct_SetBool_Target_169;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_169.Out)
		{
			Relay_In_177();
		}
	}

	private void Relay_False_169()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_169.False(out logic_uScriptAct_SetBool_Target_169);
		local_msgFabricatorSpawnedShown_System_Boolean = logic_uScriptAct_SetBool_Target_169;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_169.Out)
		{
			Relay_In_177();
		}
	}

	private void Relay_True_171()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_171.True(out logic_uScriptAct_SetBool_Target_171);
		local_msgConveyorsAttachedShown_System_Boolean = logic_uScriptAct_SetBool_Target_171;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_171.Out)
		{
			Relay_In_160();
		}
	}

	private void Relay_False_171()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_171.False(out logic_uScriptAct_SetBool_Target_171);
		local_msgConveyorsAttachedShown_System_Boolean = logic_uScriptAct_SetBool_Target_171;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_171.Out)
		{
			Relay_In_160();
		}
	}

	private void Relay_In_172()
	{
		logic_uScriptCon_CompareBool_Bool_172 = local_msgFabricatorSpawnedShown_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_172.In(logic_uScriptCon_CompareBool_Bool_172);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_172.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_172.False;
		if (num)
		{
			Relay_In_254();
		}
		if (flag)
		{
			Relay_In_252();
		}
	}

	private void Relay_True_176()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_176.True(out logic_uScriptAct_SetBool_Target_176);
		local_FabricatorSpawned_System_Boolean = logic_uScriptAct_SetBool_Target_176;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_176.Out)
		{
			Relay_In_167();
		}
	}

	private void Relay_False_176()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_176.False(out logic_uScriptAct_SetBool_Target_176);
		local_FabricatorSpawned_System_Boolean = logic_uScriptAct_SetBool_Target_176;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_176.Out)
		{
			Relay_In_167();
		}
	}

	private void Relay_Block_Attached_177()
	{
		Relay_In_309();
	}

	private void Relay_In_177()
	{
		logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_Block_177 = local_FabricatorBlock_TankBlock;
		logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_CraftingBaseTech_177 = local_CraftingBaseTech_Tank;
		int num = 0;
		Array array = ghostBlockFabricator;
		if (logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_ghostBlockData_177.Length != num + array.Length)
		{
			Array.Resize(ref logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_ghostBlockData_177, num + array.Length);
		}
		Array.Copy(array, 0, logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_ghostBlockData_177, num, array.Length);
		num += array.Length;
		logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_SubGraph_Crafting_Tutorial_AttachBlockToBase_177.In(logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_Block_177, logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_CraftingBaseTech_177, logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_ghostBlockData_177, logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_BlockType_177, logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_BlockPosition_177);
	}

	private void Relay_In_181()
	{
		logic_uScriptCon_CompareBool_Bool_181 = local_BlockCrafted_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_181.In(logic_uScriptCon_CompareBool_Bool_181);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_181.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_181.False;
		if (num)
		{
			Relay_In_258();
		}
		if (flag)
		{
			Relay_In_188();
		}
	}

	private void Relay_In_182()
	{
		logic_uScript_LockPlayerInput_uScript_LockPlayerInput_182.In(logic_uScript_LockPlayerInput_lockInput_182, logic_uScript_LockPlayerInput_includeCamera_182);
		if (logic_uScript_LockPlayerInput_uScript_LockPlayerInput_182.Out)
		{
			Relay_In_288();
		}
	}

	private void Relay_In_184()
	{
		logic_uScript_PointArrowAtVisible_targetObject_184 = local_FabricatorBlock_TankBlock;
		logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_184.In(logic_uScript_PointArrowAtVisible_targetObject_184, logic_uScript_PointArrowAtVisible_timeToShowFor_184, logic_uScript_PointArrowAtVisible_offset_184);
		if (logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_184.Out)
		{
			Relay_In_345();
		}
	}

	private void Relay_In_185()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_185 = messageTag;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_185.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_185, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_185);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_185.Out)
		{
			Relay_True_198();
		}
	}

	private void Relay_In_186()
	{
		logic_uScript_LockTechInteraction_tech_186 = local_CraftingBaseTech_Tank;
		logic_uScript_LockTechInteraction_uScript_LockTechInteraction_186.In(logic_uScript_LockTechInteraction_tech_186, logic_uScript_LockTechInteraction_excludedBlocks_186, logic_uScript_LockTechInteraction_excludedUniqueBlocks_186);
		if (logic_uScript_LockTechInteraction_uScript_LockTechInteraction_186.Out)
		{
			Relay_In_181();
		}
	}

	private void Relay_In_188()
	{
		logic_uScript_LockPause_uScript_LockPause_188.In(logic_uScript_LockPause_lockPause_188, logic_uScript_LockPause_disabledReason_188);
		if (logic_uScript_LockPause_uScript_LockPause_188.Out)
		{
			Relay_In_378();
		}
	}

	private void Relay_In_189()
	{
		logic_uScript_LockTechInteraction_tech_189 = local_CraftingBaseTech_Tank;
		logic_uScript_LockTechInteraction_uScript_LockTechInteraction_189.In(logic_uScript_LockTechInteraction_tech_189, logic_uScript_LockTechInteraction_excludedBlocks_189, logic_uScript_LockTechInteraction_excludedUniqueBlocks_189);
		if (logic_uScript_LockTechInteraction_uScript_LockTechInteraction_189.Out)
		{
			Relay_In_184();
		}
	}

	private void Relay_In_191()
	{
		logic_uScript_HideArrow_uScript_HideArrow_191.In();
		if (logic_uScript_HideArrow_uScript_HideArrow_191.Out)
		{
			Relay_In_348();
		}
	}

	private void Relay_In_192()
	{
		logic_uScript_LockTechInteraction_tech_192 = local_CraftingBaseTech_Tank;
		logic_uScript_LockTechInteraction_uScript_LockTechInteraction_192.In(logic_uScript_LockTechInteraction_tech_192, logic_uScript_LockTechInteraction_excludedBlocks_192, logic_uScript_LockTechInteraction_excludedUniqueBlocks_192);
		if (logic_uScript_LockTechInteraction_uScript_LockTechInteraction_192.Out)
		{
			Relay_In_182();
		}
	}

	private void Relay_In_194()
	{
		logic_uScriptCon_CompareBool_Bool_194 = local_CraftingInProgress_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_194.In(logic_uScriptCon_CompareBool_Bool_194);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_194.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_194.False;
		if (num)
		{
			Relay_In_186();
		}
		if (flag)
		{
			Relay_In_318();
		}
	}

	private void Relay_In_196()
	{
		logic_uScript_LockPause_uScript_LockPause_196.In(logic_uScript_LockPause_lockPause_196, logic_uScript_LockPause_disabledReason_196);
		if (logic_uScript_LockPause_uScript_LockPause_196.Out)
		{
			Relay_In_377();
		}
	}

	private void Relay_In_197()
	{
		logic_uScript_LockPlayerInput_uScript_LockPlayerInput_197.In(logic_uScript_LockPlayerInput_lockInput_197, logic_uScript_LockPlayerInput_includeCamera_197);
	}

	private void Relay_True_198()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_198.True(out logic_uScriptAct_SetBool_Target_198);
		local_CraftingInProgress_System_Boolean = logic_uScriptAct_SetBool_Target_198;
	}

	private void Relay_False_198()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_198.False(out logic_uScriptAct_SetBool_Target_198);
		local_CraftingInProgress_System_Boolean = logic_uScriptAct_SetBool_Target_198;
	}

	private void Relay_Save_Out_201()
	{
		Relay_Save_214();
	}

	private void Relay_Load_Out_201()
	{
		Relay_Load_214();
	}

	private void Relay_Restart_Out_201()
	{
		Relay_Restart_214();
	}

	private void Relay_Save_201()
	{
		logic_SubGraph_SaveLoadInt_integer_201 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_201 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_201.Save(logic_SubGraph_SaveLoadInt_restartValue_201, ref logic_SubGraph_SaveLoadInt_integer_201, logic_SubGraph_SaveLoadInt_intAsVariable_201, logic_SubGraph_SaveLoadInt_uniqueID_201);
	}

	private void Relay_Load_201()
	{
		logic_SubGraph_SaveLoadInt_integer_201 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_201 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_201.Load(logic_SubGraph_SaveLoadInt_restartValue_201, ref logic_SubGraph_SaveLoadInt_integer_201, logic_SubGraph_SaveLoadInt_intAsVariable_201, logic_SubGraph_SaveLoadInt_uniqueID_201);
	}

	private void Relay_Restart_201()
	{
		logic_SubGraph_SaveLoadInt_integer_201 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_201 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_201.Restart(logic_SubGraph_SaveLoadInt_restartValue_201, ref logic_SubGraph_SaveLoadInt_integer_201, logic_SubGraph_SaveLoadInt_intAsVariable_201, logic_SubGraph_SaveLoadInt_uniqueID_201);
	}

	private void Relay_Out_203()
	{
	}

	private void Relay_In_203()
	{
		logic_SubGraph_LoadObjectiveStates_currentObjective_203 = local_Stage_System_Int32;
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_203.In(logic_SubGraph_LoadObjectiveStates_currentObjective_203);
	}

	private void Relay_Output1_205()
	{
		Relay_In_81();
	}

	private void Relay_Output2_205()
	{
		Relay_In_92();
	}

	private void Relay_Output3_205()
	{
		Relay_In_134();
	}

	private void Relay_Output4_205()
	{
		Relay_In_159();
	}

	private void Relay_Output5_205()
	{
	}

	private void Relay_Output6_205()
	{
	}

	private void Relay_Output7_205()
	{
	}

	private void Relay_Output8_205()
	{
	}

	private void Relay_In_205()
	{
		logic_uScriptCon_ManualSwitch_CurrentOutput_205 = local_Stage2Steps_System_Int32;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_205.In(logic_uScriptCon_ManualSwitch_CurrentOutput_205);
	}

	private void Relay_In_207()
	{
		logic_uScriptAct_AddInt_v2_A_207 = local_Stage2Steps_System_Int32;
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_207.In(logic_uScriptAct_AddInt_v2_A_207, logic_uScriptAct_AddInt_v2_B_207, out logic_uScriptAct_AddInt_v2_IntResult_207, out logic_uScriptAct_AddInt_v2_FloatResult_207);
		local_Stage2Steps_System_Int32 = logic_uScriptAct_AddInt_v2_IntResult_207;
	}

	private void Relay_In_209()
	{
		logic_uScriptAct_AddInt_v2_A_209 = local_Stage2Steps_System_Int32;
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_209.In(logic_uScriptAct_AddInt_v2_A_209, logic_uScriptAct_AddInt_v2_B_209, out logic_uScriptAct_AddInt_v2_IntResult_209, out logic_uScriptAct_AddInt_v2_FloatResult_209);
		local_Stage2Steps_System_Int32 = logic_uScriptAct_AddInt_v2_IntResult_209;
	}

	private void Relay_In_212()
	{
		logic_uScriptAct_AddInt_v2_A_212 = local_Stage2Steps_System_Int32;
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_212.In(logic_uScriptAct_AddInt_v2_A_212, logic_uScriptAct_AddInt_v2_B_212, out logic_uScriptAct_AddInt_v2_IntResult_212, out logic_uScriptAct_AddInt_v2_FloatResult_212);
		local_Stage2Steps_System_Int32 = logic_uScriptAct_AddInt_v2_IntResult_212;
	}

	private void Relay_Save_Out_214()
	{
		Relay_Save_22();
	}

	private void Relay_Load_Out_214()
	{
		Relay_Load_22();
	}

	private void Relay_Restart_Out_214()
	{
		Relay_Set_False_22();
	}

	private void Relay_Save_214()
	{
		logic_SubGraph_SaveLoadInt_integer_214 = local_Stage2Steps_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_214 = local_Stage2Steps_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_214.Save(logic_SubGraph_SaveLoadInt_restartValue_214, ref logic_SubGraph_SaveLoadInt_integer_214, logic_SubGraph_SaveLoadInt_intAsVariable_214, logic_SubGraph_SaveLoadInt_uniqueID_214);
	}

	private void Relay_Load_214()
	{
		logic_SubGraph_SaveLoadInt_integer_214 = local_Stage2Steps_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_214 = local_Stage2Steps_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_214.Load(logic_SubGraph_SaveLoadInt_restartValue_214, ref logic_SubGraph_SaveLoadInt_integer_214, logic_SubGraph_SaveLoadInt_intAsVariable_214, logic_SubGraph_SaveLoadInt_uniqueID_214);
	}

	private void Relay_Restart_214()
	{
		logic_SubGraph_SaveLoadInt_integer_214 = local_Stage2Steps_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_214 = local_Stage2Steps_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_214.Restart(logic_SubGraph_SaveLoadInt_restartValue_214, ref logic_SubGraph_SaveLoadInt_integer_214, logic_SubGraph_SaveLoadInt_intAsVariable_214, logic_SubGraph_SaveLoadInt_uniqueID_214);
	}

	private void Relay_True_216()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_216.True(out logic_uScriptAct_SetBool_Target_216);
		local_CraftingMenuOpened_System_Boolean = logic_uScriptAct_SetBool_Target_216;
	}

	private void Relay_False_216()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_216.False(out logic_uScriptAct_SetBool_Target_216);
		local_CraftingMenuOpened_System_Boolean = logic_uScriptAct_SetBool_Target_216;
	}

	private void Relay_In_217()
	{
		logic_uScriptCon_CompareBool_Bool_217 = local_CraftingMenuOpened_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_217.In(logic_uScriptCon_CompareBool_Bool_217);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_217.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_217.False;
		if (num)
		{
			Relay_In_194();
		}
		if (flag)
		{
			Relay_In_311();
		}
	}

	private void Relay_In_223()
	{
		logic_uScript_LockTechInteraction_tech_223 = local_CraftingBaseTech_Tank;
		logic_uScript_LockTechInteraction_uScript_LockTechInteraction_223.In(logic_uScript_LockTechInteraction_tech_223, logic_uScript_LockTechInteraction_excludedBlocks_223, logic_uScript_LockTechInteraction_excludedUniqueBlocks_223);
		if (logic_uScript_LockTechInteraction_uScript_LockTechInteraction_223.Out)
		{
			Relay_In_205();
		}
	}

	private void Relay_Out_226()
	{
		Relay_In_372();
	}

	private void Relay_In_226()
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_226 = local_Stage_System_Int32;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_226.In(ref logic_SubGraph_CompleteObjectiveStage_objectiveStage_226, logic_SubGraph_CompleteObjectiveStage_isFinalObjective_226);
	}

	private void Relay_Out_228()
	{
	}

	private void Relay_In_228()
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_228 = local_Stage_System_Int32;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_228.In(ref logic_SubGraph_CompleteObjectiveStage_objectiveStage_228, logic_SubGraph_CompleteObjectiveStage_isFinalObjective_228);
	}

	private void Relay_In_231()
	{
		logic_uScript_AddMessage_messageData_231 = msg01Intro;
		logic_uScript_AddMessage_speaker_231 = messageSpeaker;
		logic_uScript_AddMessage_Return_231 = logic_uScript_AddMessage_uScript_AddMessage_231.In(logic_uScript_AddMessage_messageData_231, logic_uScript_AddMessage_speaker_231);
		if (logic_uScript_AddMessage_uScript_AddMessage_231.Out)
		{
			Relay_In_2();
		}
	}

	private void Relay_In_234()
	{
		logic_uScript_AddMessage_messageData_234 = msg02BaseFound;
		logic_uScript_AddMessage_speaker_234 = messageSpeaker;
		logic_uScript_AddMessage_Return_234 = logic_uScript_AddMessage_uScript_AddMessage_234.In(logic_uScript_AddMessage_messageData_234, logic_uScript_AddMessage_speaker_234);
		if (logic_uScript_AddMessage_uScript_AddMessage_234.Shown)
		{
			Relay_True_87();
		}
	}

	private void Relay_In_236()
	{
		logic_uScript_AddMessage_messageData_236 = msg03AttachReceiver;
		logic_uScript_AddMessage_speaker_236 = messageSpeaker;
		logic_uScript_AddMessage_Return_236 = logic_uScript_AddMessage_uScript_AddMessage_236.In(logic_uScript_AddMessage_messageData_236, logic_uScript_AddMessage_speaker_236);
		if (logic_uScript_AddMessage_uScript_AddMessage_236.Out)
		{
			Relay_In_84();
		}
	}

	private void Relay_In_238()
	{
		logic_uScript_AddMessage_messageData_238 = msg04ReceiverAttached;
		logic_uScript_AddMessage_speaker_238 = messageSpeaker;
		logic_uScript_AddMessage_Return_238 = logic_uScript_AddMessage_uScript_AddMessage_238.In(logic_uScript_AddMessage_messageData_238, logic_uScript_AddMessage_speaker_238);
		if (logic_uScript_AddMessage_uScript_AddMessage_238.Shown)
		{
			Relay_True_96();
		}
	}

	private void Relay_In_243()
	{
		logic_uScript_AddMessage_messageData_243 = msg05SiloExplanation;
		logic_uScript_AddMessage_speaker_243 = messageSpeaker;
		logic_uScript_AddMessage_Return_243 = logic_uScript_AddMessage_uScript_AddMessage_243.In(logic_uScript_AddMessage_messageData_243, logic_uScript_AddMessage_speaker_243);
		if (logic_uScript_AddMessage_uScript_AddMessage_243.Shown)
		{
			Relay_In_209();
		}
	}

	private void Relay_In_245()
	{
		logic_uScript_AddMessage_messageData_245 = msg06AttachConveyors;
		logic_uScript_AddMessage_speaker_245 = messageSpeaker;
		logic_uScript_AddMessage_Return_245 = logic_uScript_AddMessage_uScript_AddMessage_245.In(logic_uScript_AddMessage_messageData_245, logic_uScript_AddMessage_speaker_245);
		if (logic_uScript_AddMessage_uScript_AddMessage_245.Out)
		{
			Relay_In_136();
		}
	}

	private void Relay_In_249()
	{
		logic_uScript_AddMessage_messageData_249 = msg07ConveyorsAttached;
		logic_uScript_AddMessage_speaker_249 = messageSpeaker;
		logic_uScript_AddMessage_Return_249 = logic_uScript_AddMessage_uScript_AddMessage_249.In(logic_uScript_AddMessage_messageData_249, logic_uScript_AddMessage_speaker_249);
		if (logic_uScript_AddMessage_uScript_AddMessage_249.Shown)
		{
			Relay_True_171();
		}
	}

	private void Relay_In_252()
	{
		logic_uScript_AddMessage_messageData_252 = msg08FabricatorSpawned;
		logic_uScript_AddMessage_speaker_252 = messageSpeaker;
		logic_uScript_AddMessage_Return_252 = logic_uScript_AddMessage_uScript_AddMessage_252.In(logic_uScript_AddMessage_messageData_252, logic_uScript_AddMessage_speaker_252);
		if (logic_uScript_AddMessage_uScript_AddMessage_252.Shown)
		{
			Relay_True_169();
		}
	}

	private void Relay_In_254()
	{
		logic_uScript_AddMessage_messageData_254 = msg09AttachFabricator;
		logic_uScript_AddMessage_speaker_254 = messageSpeaker;
		logic_uScript_AddMessage_Return_254 = logic_uScript_AddMessage_uScript_AddMessage_254.In(logic_uScript_AddMessage_messageData_254, logic_uScript_AddMessage_speaker_254);
		if (logic_uScript_AddMessage_uScript_AddMessage_254.Out)
		{
			Relay_In_177();
		}
	}

	private void Relay_In_258()
	{
		logic_uScript_AddMessage_messageData_258 = msg12Complete;
		logic_uScript_AddMessage_speaker_258 = messageSpeaker;
		logic_uScript_AddMessage_Return_258 = logic_uScript_AddMessage_uScript_AddMessage_258.In(logic_uScript_AddMessage_messageData_258, logic_uScript_AddMessage_speaker_258);
		if (logic_uScript_AddMessage_uScript_AddMessage_258.Shown)
		{
			Relay_In_282();
		}
	}

	private void Relay_In_262()
	{
		logic_uScriptCon_CompareBool_Bool_262 = local_NearBase_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_262.In(logic_uScriptCon_CompareBool_Bool_262);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_262.True)
		{
			Relay_In_263();
		}
	}

	private void Relay_In_263()
	{
		logic_uScript_AddMessage_messageData_263 = msgLeavingMissionArea;
		logic_uScript_AddMessage_speaker_263 = messageSpeaker;
		logic_uScript_AddMessage_Return_263 = logic_uScript_AddMessage_uScript_AddMessage_263.In(logic_uScript_AddMessage_messageData_263, logic_uScript_AddMessage_speaker_263);
		if (logic_uScript_AddMessage_uScript_AddMessage_263.Out)
		{
			Relay_False_264();
		}
	}

	private void Relay_True_264()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_264.True(out logic_uScriptAct_SetBool_Target_264);
		local_NearBase_System_Boolean = logic_uScriptAct_SetBool_Target_264;
	}

	private void Relay_False_264()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_264.False(out logic_uScriptAct_SetBool_Target_264);
		local_NearBase_System_Boolean = logic_uScriptAct_SetBool_Target_264;
	}

	private void Relay_Out_282()
	{
		Relay_In_371();
	}

	private void Relay_In_282()
	{
		logic_SubGraph_Crafting_Tutorial_Finish_craftingBaseTech_282 = local_CraftingBaseTech_Tank;
		logic_SubGraph_Crafting_Tutorial_Finish_NPCTech_282 = local_NPCTech_Tank;
		logic_SubGraph_Crafting_Tutorial_Finish_NPCFlyAwayAI_282 = NPCFlyAwayAI;
		logic_SubGraph_Crafting_Tutorial_Finish_NPCDespawnParticleEffect_282 = NPCDespawnParticleEffect;
		logic_SubGraph_Crafting_Tutorial_Finish_SubGraph_Crafting_Tutorial_Finish_282.In(logic_SubGraph_Crafting_Tutorial_Finish_craftingBaseTech_282, logic_SubGraph_Crafting_Tutorial_Finish_NPCTech_282, logic_SubGraph_Crafting_Tutorial_Finish_NPCFlyAwayAI_282, logic_SubGraph_Crafting_Tutorial_Finish_NPCDespawnParticleEffect_282);
	}

	private void Relay_In_283()
	{
		logic_uScript_SetEncounterTarget_owner_283 = owner_Connection_284;
		logic_uScript_SetEncounterTarget_visibleObject_283 = local_NPCTech_Tank;
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_283.In(logic_uScript_SetEncounterTarget_owner_283, logic_uScript_SetEncounterTarget_visibleObject_283);
		if (logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_283.Out)
		{
			Relay_In_37();
		}
	}

	private void Relay_In_287()
	{
		logic_uScript_CraftingUIHighlightCraftButton_uScript_CraftingUIHighlightCraftButton_287.In(logic_uScript_CraftingUIHighlightCraftButton_targetMenuType_287);
		if (logic_uScript_CraftingUIHighlightCraftButton_uScript_CraftingUIHighlightCraftButton_287.Selected)
		{
			Relay_EnableAutoCloseUI_297();
		}
	}

	private void Relay_In_288()
	{
		logic_uScript_CraftingUIHighlightItem_itemToHighlight_288 = blockTypeToHighlight;
		logic_uScript_CraftingUIHighlightItem_uScript_CraftingUIHighlightItem_288.In(logic_uScript_CraftingUIHighlightItem_targetMenuType_288, logic_uScript_CraftingUIHighlightItem_itemToHighlight_288);
		if (logic_uScript_CraftingUIHighlightItem_uScript_CraftingUIHighlightItem_288.Selected)
		{
			Relay_In_287();
		}
	}

	private void Relay_In_289()
	{
		logic_uScript_SpawnResourceListOnHolder_tech_289 = local_CraftingBaseTech_Tank;
		int num = 0;
		Array array = resourceList;
		if (logic_uScript_SpawnResourceListOnHolder_chunks_289.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SpawnResourceListOnHolder_chunks_289, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SpawnResourceListOnHolder_chunks_289, num, array.Length);
		num += array.Length;
		logic_uScript_SpawnResourceListOnHolder_uScript_SpawnResourceListOnHolder_289.In(logic_uScript_SpawnResourceListOnHolder_tech_289, logic_uScript_SpawnResourceListOnHolder_chunks_289, logic_uScript_SpawnResourceListOnHolder_blockType_289);
		if (logic_uScript_SpawnResourceListOnHolder_uScript_SpawnResourceListOnHolder_289.Out)
		{
			Relay_In_91();
		}
	}

	private void Relay_In_292()
	{
		logic_uScript_LockPause_uScript_LockPause_292.In(logic_uScript_LockPause_lockPause_292, logic_uScript_LockPause_disabledReason_292);
		if (logic_uScript_LockPause_uScript_LockPause_292.Out)
		{
			Relay_In_293();
		}
	}

	private void Relay_In_293()
	{
		logic_uScript_LockPlayerInput_uScript_LockPlayerInput_293.In(logic_uScript_LockPlayerInput_lockInput_293, logic_uScript_LockPlayerInput_includeCamera_293);
		if (logic_uScript_LockPlayerInput_uScript_LockPlayerInput_293.Out)
		{
			Relay_In_14();
		}
	}

	private void Relay_DisableAutoCloseUI_294()
	{
		logic_uScript_DisableClosingCraftingUIWhenTooFarAway_craftingBlock_294 = local_FabricatorBlock_TankBlock;
		logic_uScript_DisableClosingCraftingUIWhenTooFarAway_uScript_DisableClosingCraftingUIWhenTooFarAway_294.DisableAutoCloseUI(logic_uScript_DisableClosingCraftingUIWhenTooFarAway_craftingBlock_294);
		if (logic_uScript_DisableClosingCraftingUIWhenTooFarAway_uScript_DisableClosingCraftingUIWhenTooFarAway_294.Out)
		{
			Relay_In_307();
		}
	}

	private void Relay_EnableAutoCloseUI_294()
	{
		logic_uScript_DisableClosingCraftingUIWhenTooFarAway_craftingBlock_294 = local_FabricatorBlock_TankBlock;
		logic_uScript_DisableClosingCraftingUIWhenTooFarAway_uScript_DisableClosingCraftingUIWhenTooFarAway_294.EnableAutoCloseUI(logic_uScript_DisableClosingCraftingUIWhenTooFarAway_craftingBlock_294);
		if (logic_uScript_DisableClosingCraftingUIWhenTooFarAway_uScript_DisableClosingCraftingUIWhenTooFarAway_294.Out)
		{
			Relay_In_307();
		}
	}

	private void Relay_DisableAutoCloseUI_297()
	{
		logic_uScript_DisableClosingCraftingUIWhenTooFarAway_craftingBlock_297 = local_FabricatorBlock_TankBlock;
		logic_uScript_DisableClosingCraftingUIWhenTooFarAway_uScript_DisableClosingCraftingUIWhenTooFarAway_297.DisableAutoCloseUI(logic_uScript_DisableClosingCraftingUIWhenTooFarAway_craftingBlock_297);
		if (logic_uScript_DisableClosingCraftingUIWhenTooFarAway_uScript_DisableClosingCraftingUIWhenTooFarAway_297.Out)
		{
			Relay_In_185();
		}
	}

	private void Relay_EnableAutoCloseUI_297()
	{
		logic_uScript_DisableClosingCraftingUIWhenTooFarAway_craftingBlock_297 = local_FabricatorBlock_TankBlock;
		logic_uScript_DisableClosingCraftingUIWhenTooFarAway_uScript_DisableClosingCraftingUIWhenTooFarAway_297.EnableAutoCloseUI(logic_uScript_DisableClosingCraftingUIWhenTooFarAway_craftingBlock_297);
		if (logic_uScript_DisableClosingCraftingUIWhenTooFarAway_uScript_DisableClosingCraftingUIWhenTooFarAway_297.Out)
		{
			Relay_In_185();
		}
	}

	private void Relay_DisableAutoCloseUI_298()
	{
		logic_uScript_DisableClosingCraftingUIWhenTooFarAway_craftingBlock_298 = local_FabricatorBlock_TankBlock;
		logic_uScript_DisableClosingCraftingUIWhenTooFarAway_uScript_DisableClosingCraftingUIWhenTooFarAway_298.DisableAutoCloseUI(logic_uScript_DisableClosingCraftingUIWhenTooFarAway_craftingBlock_298);
		if (logic_uScript_DisableClosingCraftingUIWhenTooFarAway_uScript_DisableClosingCraftingUIWhenTooFarAway_298.Out)
		{
			Relay_In_370();
		}
	}

	private void Relay_EnableAutoCloseUI_298()
	{
		logic_uScript_DisableClosingCraftingUIWhenTooFarAway_craftingBlock_298 = local_FabricatorBlock_TankBlock;
		logic_uScript_DisableClosingCraftingUIWhenTooFarAway_uScript_DisableClosingCraftingUIWhenTooFarAway_298.EnableAutoCloseUI(logic_uScript_DisableClosingCraftingUIWhenTooFarAway_craftingBlock_298);
		if (logic_uScript_DisableClosingCraftingUIWhenTooFarAway_uScript_DisableClosingCraftingUIWhenTooFarAway_298.Out)
		{
			Relay_In_370();
		}
	}

	private void Relay_In_300()
	{
		logic_uScriptCon_CompareBool_Bool_300 = local_CraftingMenuOpened_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_300.In(logic_uScriptCon_CompareBool_Bool_300);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_300.True)
		{
			Relay_In_301();
		}
	}

	private void Relay_In_301()
	{
		logic_uScriptCon_CompareBool_Bool_301 = local_CraftingInProgress_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_301.In(logic_uScriptCon_CompareBool_Bool_301);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_301.False)
		{
			Relay_False_304();
		}
	}

	private void Relay_True_304()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_304.True(out logic_uScriptAct_SetBool_Target_304);
		local_CraftingMenuOpened_System_Boolean = logic_uScriptAct_SetBool_Target_304;
	}

	private void Relay_False_304()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_304.False(out logic_uScriptAct_SetBool_Target_304);
		local_CraftingMenuOpened_System_Boolean = logic_uScriptAct_SetBool_Target_304;
	}

	private void Relay_In_307()
	{
		logic_uScript_IsHUDElementLinkedToBlock_hudElement_307 = local_CraftingMenu_ManHUD_HUDElementType;
		logic_uScript_IsHUDElementLinkedToBlock_targetBlock_307 = local_FabricatorBlock_TankBlock;
		logic_uScript_IsHUDElementLinkedToBlock_uScript_IsHUDElementLinkedToBlock_307.In(logic_uScript_IsHUDElementLinkedToBlock_hudElement_307, logic_uScript_IsHUDElementLinkedToBlock_targetBlock_307);
		if (logic_uScript_IsHUDElementLinkedToBlock_uScript_IsHUDElementLinkedToBlock_307.True)
		{
			Relay_In_191();
		}
	}

	private void Relay_In_309()
	{
		logic_uScript_HideHUDElement_hudElement_309 = local_CraftingMenu_ManHUD_HUDElementType;
		logic_uScript_HideHUDElement_uScript_HideHUDElement_309.In(logic_uScript_HideHUDElement_hudElement_309);
		if (logic_uScript_HideHUDElement_uScript_HideHUDElement_309.Out)
		{
			Relay_In_228();
		}
	}

	private void Relay_Out_311()
	{
		Relay_In_189();
	}

	private void Relay_Shown_311()
	{
	}

	private void Relay_In_311()
	{
		logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboard_311 = msg10OpenMenu;
		logic_SubGraph_AddMessageWithPadSupport_messageControlPad_311 = msg10OpenMenu_Pad;
		logic_SubGraph_AddMessageWithPadSupport_speaker_311 = messageSpeaker;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_311.In(logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboard_311, logic_SubGraph_AddMessageWithPadSupport_messageControlPad_311, logic_SubGraph_AddMessageWithPadSupport_speaker_311);
	}

	private void Relay_Out_318()
	{
		Relay_In_196();
	}

	private void Relay_Shown_318()
	{
	}

	private void Relay_In_318()
	{
		logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboard_318 = msg11CraftBlock;
		logic_SubGraph_AddMessageWithPadSupport_messageControlPad_318 = msg11CraftBlock_Pad;
		logic_SubGraph_AddMessageWithPadSupport_speaker_318 = messageSpeaker;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_318.In(logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboard_318, logic_SubGraph_AddMessageWithPadSupport_messageControlPad_318, logic_SubGraph_AddMessageWithPadSupport_speaker_318);
	}

	private void Relay_In_319()
	{
		logic_uScript_EnableGlow_targetObject_319 = local_GhostBlockConveyor01_TankBlock;
		logic_uScript_EnableGlow_uScript_EnableGlow_319.In(logic_uScript_EnableGlow_targetObject_319, logic_uScript_EnableGlow_enable_319);
		if (logic_uScript_EnableGlow_uScript_EnableGlow_319.Out)
		{
			Relay_In_328();
		}
	}

	private void Relay_In_321()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_321.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_321.Out)
		{
			Relay_In_328();
		}
	}

	private void Relay_In_322()
	{
		logic_uScriptCon_CompareBool_Bool_322 = local_GhostBlockConveyor01Spawned_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_322.In(logic_uScriptCon_CompareBool_Bool_322);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_322.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_322.False;
		if (num)
		{
			Relay_In_319();
		}
		if (flag)
		{
			Relay_In_321();
		}
	}

	private void Relay_In_325()
	{
		logic_uScript_EnableGlow_targetObject_325 = local_GhostBlockConveyor02_TankBlock;
		logic_uScript_EnableGlow_uScript_EnableGlow_325.In(logic_uScript_EnableGlow_targetObject_325, logic_uScript_EnableGlow_enable_325);
		if (logic_uScript_EnableGlow_uScript_EnableGlow_325.Out)
		{
			Relay_In_122();
		}
	}

	private void Relay_In_326()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_326.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_326.Out)
		{
			Relay_In_122();
		}
	}

	private void Relay_In_328()
	{
		logic_uScriptCon_CompareBool_Bool_328 = local_GhostBlockConveyor02Spawned_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_328.In(logic_uScriptCon_CompareBool_Bool_328);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_328.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_328.False;
		if (num)
		{
			Relay_In_325();
		}
		if (flag)
		{
			Relay_In_326();
		}
	}

	private void Relay_In_329()
	{
		logic_uScript_EnableGlow_targetObject_329 = local_ConveyorBlock01_TankBlock;
		logic_uScript_EnableGlow_uScript_EnableGlow_329.In(logic_uScript_EnableGlow_targetObject_329, logic_uScript_EnableGlow_enable_329);
		if (logic_uScript_EnableGlow_uScript_EnableGlow_329.Out)
		{
			Relay_In_148();
		}
	}

	private void Relay_In_331()
	{
		logic_uScript_EnableGlow_targetObject_331 = local_ConveyorBlock02_TankBlock;
		logic_uScript_EnableGlow_uScript_EnableGlow_331.In(logic_uScript_EnableGlow_targetObject_331, logic_uScript_EnableGlow_enable_331);
		if (logic_uScript_EnableGlow_uScript_EnableGlow_331.Out)
		{
			Relay_In_115();
		}
	}

	private void Relay_In_333()
	{
		logic_uScript_EnableGlow_targetObject_333 = local_ConveyorBlock01_TankBlock;
		logic_uScript_EnableGlow_uScript_EnableGlow_333.In(logic_uScript_EnableGlow_targetObject_333, logic_uScript_EnableGlow_enable_333);
		if (logic_uScript_EnableGlow_uScript_EnableGlow_333.Out)
		{
			Relay_In_116();
		}
	}

	private void Relay_In_336()
	{
		logic_uScript_EnableGlow_targetObject_336 = local_ConveyorBlock02_TankBlock;
		logic_uScript_EnableGlow_uScript_EnableGlow_336.In(logic_uScript_EnableGlow_targetObject_336, logic_uScript_EnableGlow_enable_336);
		if (logic_uScript_EnableGlow_uScript_EnableGlow_336.Out)
		{
			Relay_In_115();
		}
	}

	private void Relay_In_338()
	{
		logic_uScript_EnableGlow_targetObject_338 = local_ConveyorBlock01_TankBlock;
		logic_uScript_EnableGlow_uScript_EnableGlow_338.In(logic_uScript_EnableGlow_targetObject_338, logic_uScript_EnableGlow_enable_338);
		if (logic_uScript_EnableGlow_uScript_EnableGlow_338.Out)
		{
			Relay_In_339();
		}
	}

	private void Relay_In_339()
	{
		logic_uScript_EnableGlow_targetObject_339 = local_ConveyorBlock02_TankBlock;
		logic_uScript_EnableGlow_uScript_EnableGlow_339.In(logic_uScript_EnableGlow_targetObject_339, logic_uScript_EnableGlow_enable_339);
		if (logic_uScript_EnableGlow_uScript_EnableGlow_339.Out)
		{
			Relay_In_113();
		}
	}

	private void Relay_In_342()
	{
		logic_uScript_EnableGlow_targetObject_342 = local_GhostBlockConveyor01_TankBlock;
		logic_uScript_EnableGlow_uScript_EnableGlow_342.In(logic_uScript_EnableGlow_targetObject_342, logic_uScript_EnableGlow_enable_342);
	}

	private void Relay_In_344()
	{
		logic_uScript_EnableGlow_targetObject_344 = local_GhostBlockConveyor02_TankBlock;
		logic_uScript_EnableGlow_uScript_EnableGlow_344.In(logic_uScript_EnableGlow_targetObject_344, logic_uScript_EnableGlow_enable_344);
	}

	private void Relay_In_345()
	{
		logic_uScript_EnableGlow_targetObject_345 = local_FabricatorBlock_TankBlock;
		logic_uScript_EnableGlow_uScript_EnableGlow_345.In(logic_uScript_EnableGlow_targetObject_345, logic_uScript_EnableGlow_enable_345);
		if (logic_uScript_EnableGlow_uScript_EnableGlow_345.Out)
		{
			Relay_DisableAutoCloseUI_294();
		}
	}

	private void Relay_In_348()
	{
		logic_uScript_EnableGlow_targetObject_348 = local_FabricatorBlock_TankBlock;
		logic_uScript_EnableGlow_uScript_EnableGlow_348.In(logic_uScript_EnableGlow_targetObject_348, logic_uScript_EnableGlow_enable_348);
		if (logic_uScript_EnableGlow_uScript_EnableGlow_348.Out)
		{
			Relay_True_216();
		}
	}

	private void Relay_In_349()
	{
		logic_uScript_EnableGlow_targetObject_349 = local_ReceiverBlock_TankBlock;
		logic_uScript_EnableGlow_uScript_EnableGlow_349.In(logic_uScript_EnableGlow_targetObject_349, logic_uScript_EnableGlow_enable_349);
		if (logic_uScript_EnableGlow_uScript_EnableGlow_349.Out)
		{
			Relay_In_354();
		}
	}

	private void Relay_In_352()
	{
		logic_uScript_EnableGlow_targetObject_352 = local_ConveyorBlock02_TankBlock;
		logic_uScript_EnableGlow_uScript_EnableGlow_352.In(logic_uScript_EnableGlow_targetObject_352, logic_uScript_EnableGlow_enable_352);
		if (logic_uScript_EnableGlow_uScript_EnableGlow_352.Out)
		{
			Relay_In_364();
		}
	}

	private void Relay_In_354()
	{
		logic_uScript_EnableGlow_targetObject_354 = local_ConveyorBlock01_TankBlock;
		logic_uScript_EnableGlow_uScript_EnableGlow_354.In(logic_uScript_EnableGlow_targetObject_354, logic_uScript_EnableGlow_enable_354);
		if (logic_uScript_EnableGlow_uScript_EnableGlow_354.Out)
		{
			Relay_In_352();
		}
	}

	private void Relay_In_356()
	{
		logic_uScript_EnableGlow_targetObject_356 = local_GhostBlockConveyor01_TankBlock;
		logic_uScript_EnableGlow_uScript_EnableGlow_356.In(logic_uScript_EnableGlow_targetObject_356, logic_uScript_EnableGlow_enable_356);
		if (logic_uScript_EnableGlow_uScript_EnableGlow_356.Out)
		{
			Relay_In_357();
		}
	}

	private void Relay_In_357()
	{
		logic_uScriptCon_CompareBool_Bool_357 = local_GhostBlockConveyor02Spawned_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_357.In(logic_uScriptCon_CompareBool_Bool_357);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_357.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_357.False;
		if (num)
		{
			Relay_In_363();
		}
		if (flag)
		{
			Relay_In_358();
		}
	}

	private void Relay_In_358()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_358.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_358.Out)
		{
			Relay_In_368();
		}
	}

	private void Relay_In_359()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_359.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_359.Out)
		{
			Relay_In_357();
		}
	}

	private void Relay_In_363()
	{
		logic_uScript_EnableGlow_targetObject_363 = local_GhostBlockConveyor02_TankBlock;
		logic_uScript_EnableGlow_uScript_EnableGlow_363.In(logic_uScript_EnableGlow_targetObject_363, logic_uScript_EnableGlow_enable_363);
		if (logic_uScript_EnableGlow_uScript_EnableGlow_363.Out)
		{
			Relay_In_368();
		}
	}

	private void Relay_In_364()
	{
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_364.In(logic_uScriptCon_CompareBool_Bool_364);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_364.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_364.False;
		if (num)
		{
			Relay_In_356();
		}
		if (flag)
		{
			Relay_In_359();
		}
	}

	private void Relay_In_366()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_366.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_366.Out)
		{
			Relay_In_370();
		}
	}

	private void Relay_In_368()
	{
		logic_uScriptCon_CompareBool_Bool_368 = local_FabricatorSpawned_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_368.In(logic_uScriptCon_CompareBool_Bool_368);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_368.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_368.False;
		if (num)
		{
			Relay_In_369();
		}
		if (flag)
		{
			Relay_In_366();
		}
	}

	private void Relay_In_369()
	{
		logic_uScript_EnableGlow_targetObject_369 = local_FabricatorBlock_TankBlock;
		logic_uScript_EnableGlow_uScript_EnableGlow_369.In(logic_uScript_EnableGlow_targetObject_369, logic_uScript_EnableGlow_enable_369);
		if (logic_uScript_EnableGlow_uScript_EnableGlow_369.Out)
		{
			Relay_EnableAutoCloseUI_298();
		}
	}

	private void Relay_In_370()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_370.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_370.Out)
		{
			Relay_In_262();
			Relay_In_300();
		}
	}

	private void Relay_In_371()
	{
		logic_uScript_SendAnaliticsEvent_uScript_SendAnaliticsEvent_371.In(logic_uScript_SendAnaliticsEvent_analiticsEvent_371, logic_uScript_SendAnaliticsEvent_parameterName_371, logic_uScript_SendAnaliticsEvent_parameter_371);
	}

	private void Relay_In_372()
	{
		logic_uScript_SendAnaliticsEvent_uScript_SendAnaliticsEvent_372.In(logic_uScript_SendAnaliticsEvent_analiticsEvent_372, logic_uScript_SendAnaliticsEvent_parameterName_372, logic_uScript_SendAnaliticsEvent_parameter_372);
	}

	private void Relay_Save_Out_374()
	{
		Relay_Save_65();
	}

	private void Relay_Load_Out_374()
	{
		Relay_Load_65();
	}

	private void Relay_Restart_Out_374()
	{
		Relay_Set_False_65();
	}

	private void Relay_Save_374()
	{
		logic_SubGraph_SaveLoadBool_boolean_374 = local_CraftingInProgress_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_374 = local_CraftingInProgress_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_374.Save(ref logic_SubGraph_SaveLoadBool_boolean_374, logic_SubGraph_SaveLoadBool_boolAsVariable_374, logic_SubGraph_SaveLoadBool_uniqueID_374);
	}

	private void Relay_Load_374()
	{
		logic_SubGraph_SaveLoadBool_boolean_374 = local_CraftingInProgress_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_374 = local_CraftingInProgress_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_374.Load(ref logic_SubGraph_SaveLoadBool_boolean_374, logic_SubGraph_SaveLoadBool_boolAsVariable_374, logic_SubGraph_SaveLoadBool_uniqueID_374);
	}

	private void Relay_Set_True_374()
	{
		logic_SubGraph_SaveLoadBool_boolean_374 = local_CraftingInProgress_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_374 = local_CraftingInProgress_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_374.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_374, logic_SubGraph_SaveLoadBool_boolAsVariable_374, logic_SubGraph_SaveLoadBool_uniqueID_374);
	}

	private void Relay_Set_False_374()
	{
		logic_SubGraph_SaveLoadBool_boolean_374 = local_CraftingInProgress_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_374 = local_CraftingInProgress_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_374.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_374, logic_SubGraph_SaveLoadBool_boolAsVariable_374, logic_SubGraph_SaveLoadBool_uniqueID_374);
	}

	private void Relay_Save_Out_376()
	{
		Relay_Save_374();
	}

	private void Relay_Load_Out_376()
	{
		Relay_Load_374();
	}

	private void Relay_Restart_Out_376()
	{
		Relay_Set_False_374();
	}

	private void Relay_Save_376()
	{
		logic_SubGraph_SaveLoadBool_boolean_376 = local_CraftingMenuOpened_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_376 = local_CraftingMenuOpened_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_376.Save(ref logic_SubGraph_SaveLoadBool_boolean_376, logic_SubGraph_SaveLoadBool_boolAsVariable_376, logic_SubGraph_SaveLoadBool_uniqueID_376);
	}

	private void Relay_Load_376()
	{
		logic_SubGraph_SaveLoadBool_boolean_376 = local_CraftingMenuOpened_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_376 = local_CraftingMenuOpened_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_376.Load(ref logic_SubGraph_SaveLoadBool_boolean_376, logic_SubGraph_SaveLoadBool_boolAsVariable_376, logic_SubGraph_SaveLoadBool_uniqueID_376);
	}

	private void Relay_Set_True_376()
	{
		logic_SubGraph_SaveLoadBool_boolean_376 = local_CraftingMenuOpened_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_376 = local_CraftingMenuOpened_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_376.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_376, logic_SubGraph_SaveLoadBool_boolAsVariable_376, logic_SubGraph_SaveLoadBool_uniqueID_376);
	}

	private void Relay_Set_False_376()
	{
		logic_SubGraph_SaveLoadBool_boolean_376 = local_CraftingMenuOpened_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_376 = local_CraftingMenuOpened_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_376.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_376, logic_SubGraph_SaveLoadBool_boolAsVariable_376, logic_SubGraph_SaveLoadBool_uniqueID_376);
	}

	private void Relay_In_377()
	{
		logic_uScript_LockHudGroup_uScript_LockHudGroup_377.In(logic_uScript_LockHudGroup_group_377, logic_uScript_LockHudGroup_locked_377);
		if (logic_uScript_LockHudGroup_uScript_LockHudGroup_377.Out)
		{
			Relay_In_192();
		}
	}

	private void Relay_In_378()
	{
		logic_uScript_LockHudGroup_uScript_LockHudGroup_378.In(logic_uScript_LockHudGroup_group_378, logic_uScript_LockHudGroup_locked_378);
		if (logic_uScript_LockHudGroup_uScript_LockHudGroup_378.Out)
		{
			Relay_In_197();
		}
	}
}
