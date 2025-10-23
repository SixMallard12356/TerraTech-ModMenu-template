using System;
using BehaviorDesigner.Runtime;
using UnityEngine;

[Serializable]
[NodePath("Graphs")]
[FriendlyName("Untitled", "")]
public class Mission_GSO_2_Crafting_03 : uScriptLogic
{
	private GameObject parentGameObject;

	private uScript_GUI thisScriptsOnGuiListener;

	private bool m_RegisteredForEvents;

	[Multiline(3)]
	public string basePosition = "";

	public SpawnTechData[] baseSpawnData = new SpawnTechData[0];

	public SpawnBlockData[] blockSpawnData = new SpawnBlockData[0];

	public SpawnBlockData[] blockSpawnDataGenerator = new SpawnBlockData[0];

	public float clearSceneryRadius;

	public TankPreset completedBasePreset;

	public float distBaseFound;

	public int filterResourceAmount;

	public ChunkTypes filterResourceType;

	public GhostBlockSpawnData[] ghostBlockFilter = new GhostBlockSpawnData[0];

	public GhostBlockSpawnData[] ghostBlockGenerator = new GhostBlockSpawnData[0];

	public GhostBlockSpawnData[] ghostBlockReceiver = new GhostBlockSpawnData[0];

	private Vector3 local_121_UnityEngine_Vector3 = new Vector3(0f, 0f, 0f);

	private TankBlock local_148_TankBlock;

	private ManHUD.HUDElementType local_72_ManHUD_HUDElementType = ManHUD.HUDElementType.FilterMenu;

	private bool local_AllowFilterInteraction_System_Boolean;

	private bool local_BatteriesDrained_System_Boolean;

	private Tank local_CraftingBaseTech_Tank;

	private TankBlock local_FilterBlock_TankBlock;

	private bool local_FilterMenuOpened_System_Boolean;

	private bool local_FilterSetToFuel_System_Boolean;

	private bool local_FinalChunksSpawned_System_Boolean;

	private bool local_FinalResourcesSpawned_System_Boolean;

	private bool local_GeneratorAttached_System_Boolean;

	private TankBlock local_GeneratorBlock_TankBlock;

	private bool local_GeneratorSpawned_System_Boolean;

	private bool local_MsgAttachGeneratorShown_System_Boolean;

	private bool local_MsgAttachReceiverShown_System_Boolean;

	private bool local_MsgBaseExplantionShown_System_Boolean;

	private bool local_MsgBaseFoundShown_System_Boolean;

	private bool local_msgFilterSetToFuelShown_System_Boolean;

	private bool local_MsgFilterTrainedShown_System_Boolean;

	private bool local_msgIntroShown_System_Boolean;

	private bool local_msgReceiverAttachedShown_System_Boolean;

	private bool local_MsgSeeGeneratorWorkingShown_System_Boolean;

	private bool local_MsgSetFilterShown_System_Boolean;

	private bool local_MsgWoodSpawned_System_Boolean;

	private bool local_NearBase_System_Boolean;

	private Tank local_NPCTech_Tank;

	private TankBlock local_ReceiverBlock_TankBlock;

	private bool local_ReceiverSpawned_System_Boolean;

	private bool local_ResourcesSpawned_System_Boolean;

	private bool local_ShieldPowered_System_Boolean;

	private int local_Stage_System_Int32 = 1;

	private bool local_WoodBurnt_System_Boolean;

	public uScript_AddMessage.MessageSpeaker messageSpeaker;

	[Multiline(3)]
	public string messageTag = "";

	public uScript_AddMessage.MessageData msg01Intro;

	public uScript_AddMessage.MessageData msg02BaseFound;

	public uScript_AddMessage.MessageData msg03AttachFilter;

	public uScript_AddMessage.MessageData msg04FilterAttached;

	public uScript_AddMessage.MessageData msg05SetFilter;

	public uScript_AddMessage.MessageData msg05SetFilter_Pad;

	public uScript_AddMessage.MessageData msg06FilterSet;

	public uScript_AddMessage.MessageData msg07aFeedGenerator;

	public uScript_AddMessage.MessageData msg07GeneratorSpawned;

	public uScript_AddMessage.MessageData msg08AttachGenerator;

	public uScript_AddMessage.MessageData msg09GeneratorAttached;

	public uScript_AddMessage.MessageData msg10AttachReceiver;

	public uScript_AddMessage.MessageData msg11ReceiverAttached;

	public uScript_AddMessage.MessageData msg12FilterExplanation;

	public uScript_AddMessage.MessageData msg13FuelExplanation;

	public uScript_AddMessage.MessageData msg14OpenFilterMenu;

	public uScript_AddMessage.MessageData msg14OpenFilterMenu_Pad1;

	public uScript_AddMessage.MessageData msg14OpenFilterMenu_Pad2;

	public uScript_AddMessage.MessageData msg15SelectFuelChunks;

	public uScript_AddMessage.MessageData msg15SelectFuelChunks_Pad;

	public uScript_AddMessage.MessageData msg16FilterSetToFuel;

	public uScript_AddMessage.MessageData msg17Complete;

	public uScript_AddMessage.MessageData msgBlockOutsideArea;

	public uScript_AddMessage.MessageData msgLeavingMissionArea;

	public Transform NPCDespawnParticleEffect;

	public ExternalBehaviorTree NPCFlyAwayAI;

	public SpawnTechData[] NPCSpawnData = new SpawnTechData[0];

	public ChunkTypes[] resourceListFuels = new ChunkTypes[0];

	public ChunkTypes[] resourceListWoods = new ChunkTypes[0];

	[Multiline(3)]
	public string resourceSpawnPos = "";

	public ItemTypeInfo ResourceTypeToFilterWood;

	public BlockTypes shieldBlockType;

	public float timeWaitForResourcesInSilo;

	private GameObject owner_Connection_1;

	private GameObject owner_Connection_10;

	private GameObject owner_Connection_119;

	private GameObject owner_Connection_140;

	private GameObject owner_Connection_142;

	private GameObject owner_Connection_292;

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

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_7 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_7;

	private bool logic_uScriptCon_CompareBool_True_7 = true;

	private bool logic_uScriptCon_CompareBool_False_7 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_8 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_8;

	private bool logic_uScriptAct_SetBool_Out_8 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_8 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_8 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_11 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_11;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_11 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_11 = "ResourcesSpawned";

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_13 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_13;

	private bool logic_uScriptAct_SetBool_Out_13 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_13 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_13 = true;

	private uScript_Wait logic_uScript_Wait_uScript_Wait_14 = new uScript_Wait();

	private float logic_uScript_Wait_seconds_14;

	private bool logic_uScript_Wait_repeat_14;

	private bool logic_uScript_Wait_Waited_14 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_17 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_17;

	private bool logic_uScriptCon_CompareBool_True_17 = true;

	private bool logic_uScriptCon_CompareBool_False_17 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_22 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_22;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_22 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_22 = "WoodBurnt";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_23 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_23;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_23 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_23 = "ReceiverSpawned";

	private uScript_IsFilterAcceptingSpecificItem logic_uScript_IsFilterAcceptingSpecificItem_uScript_IsFilterAcceptingSpecificItem_27 = new uScript_IsFilterAcceptingSpecificItem();

	private TankBlock logic_uScript_IsFilterAcceptingSpecificItem_filterBlock_27;

	private ItemTypeInfo logic_uScript_IsFilterAcceptingSpecificItem_itemType_27;

	private bool logic_uScript_IsFilterAcceptingSpecificItem_True_27 = true;

	private bool logic_uScript_IsFilterAcceptingSpecificItem_False_27 = true;

	private uScript_HideArrow logic_uScript_HideArrow_uScript_HideArrow_28 = new uScript_HideArrow();

	private bool logic_uScript_HideArrow_Out_28 = true;

	private SubGraph_Crafting_Tutorial_AttachBlockToBase logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_SubGraph_Crafting_Tutorial_AttachBlockToBase_29 = new SubGraph_Crafting_Tutorial_AttachBlockToBase();

	private TankBlock logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_Block_29;

	private Tank logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_CraftingBaseTech_29;

	private GhostBlockSpawnData[] logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_ghostBlockData_29 = new GhostBlockSpawnData[0];

	private BlockTypes logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_BlockType_29 = BlockTypes.GSOFilter_111;

	private Vector3 logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_BlockPosition_29 = new Vector3(-4f, 0f, -1f);

	private SubGraph_Crafting_Tutorial_AttachBlockToBase logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_SubGraph_Crafting_Tutorial_AttachBlockToBase_33 = new SubGraph_Crafting_Tutorial_AttachBlockToBase();

	private TankBlock logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_Block_33;

	private Tank logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_CraftingBaseTech_33;

	private GhostBlockSpawnData[] logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_ghostBlockData_33 = new GhostBlockSpawnData[0];

	private BlockTypes logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_BlockType_33 = BlockTypes.GSOReceiver_111;

	private Vector3 logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_BlockPosition_33 = new Vector3(-5f, 0f, -4f);

	private uScript_PointArrowAtVisible logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_36 = new uScript_PointArrowAtVisible();

	private object logic_uScript_PointArrowAtVisible_targetObject_36 = "";

	private float logic_uScript_PointArrowAtVisible_timeToShowFor_36 = -1f;

	private Vector3 logic_uScript_PointArrowAtVisible_offset_36 = new Vector3(0f, 0f, 0f);

	private bool logic_uScript_PointArrowAtVisible_Out_36 = true;

	private SubGraph_Crafting_Tutorial_ManageBlock logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_39 = new SubGraph_Crafting_Tutorial_ManageBlock();

	private SpawnBlockData[] logic_SubGraph_Crafting_Tutorial_ManageBlock_blockSpawnData_39 = new SpawnBlockData[0];

	private int logic_SubGraph_Crafting_Tutorial_ManageBlock_blockIndex_39;

	private TankBlock logic_SubGraph_Crafting_Tutorial_ManageBlock_block_39;

	private bool logic_SubGraph_Crafting_Tutorial_ManageBlock_allowAnchoring_39;

	private uScript_AddMessage.MessageData logic_SubGraph_Crafting_Tutorial_ManageBlock_msgBlockOutsideArea_39;

	private uScript_AddMessage.MessageSpeaker logic_SubGraph_Crafting_Tutorial_ManageBlock_messageSpeaker_39;

	private SubGraph_Crafting_Tutorial_AttachBlockToBase logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_SubGraph_Crafting_Tutorial_AttachBlockToBase_42 = new SubGraph_Crafting_Tutorial_AttachBlockToBase();

	private TankBlock logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_Block_42;

	private Tank logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_CraftingBaseTech_42;

	private GhostBlockSpawnData[] logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_ghostBlockData_42 = new GhostBlockSpawnData[0];

	private BlockTypes logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_BlockType_42 = BlockTypes.GSOGeneratorStatic_212;

	private Vector3 logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_BlockPosition_42 = new Vector3(-2f, 0f, 0f);

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_44 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_44;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_44 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_44 = "GeneratorSpawned";

	private uScript_LockTechStacks logic_uScript_LockTechStacks_uScript_LockTechStacks_45 = new uScript_LockTechStacks();

	private Tank logic_uScript_LockTechStacks_tech_45;

	private bool logic_uScript_LockTechStacks_Out_45 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_48 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_48;

	private bool logic_uScriptCon_CompareBool_True_48 = true;

	private bool logic_uScriptCon_CompareBool_False_48 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_49 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_49;

	private bool logic_uScriptAct_SetBool_Out_49 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_49 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_49 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_52 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_52;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_52 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_52 = "MsgBaseFoundShown";

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_53 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_53;

	private bool logic_uScriptAct_SetBool_Out_53 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_53 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_53 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_55 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_55;

	private bool logic_uScriptCon_CompareBool_True_55 = true;

	private bool logic_uScriptCon_CompareBool_False_55 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_58 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_58;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_58 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_58 = "MsgSetFilterShown";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_60 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_60;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_60 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_60 = "MsgAttachGeneratorShown";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_62 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_62;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_62 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_62 = "MsgAttachReceiverShown";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_64 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_64;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_64 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_64 = "MsgBaseExplantionShown";

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_65 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_65;

	private bool logic_uScriptCon_CompareBool_True_65 = true;

	private bool logic_uScriptCon_CompareBool_False_65 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_67 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_67;

	private bool logic_uScriptAct_SetBool_Out_67 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_67 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_67 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_70 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_70;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_70 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_70 = "FilterSetToFuel";

	private uScript_IsHUDElementVisible logic_uScript_IsHUDElementVisible_uScript_IsHUDElementVisible_71 = new uScript_IsHUDElementVisible();

	private ManHUD.HUDElementType logic_uScript_IsHUDElementVisible_hudElement_71;

	private bool logic_uScript_IsHUDElementVisible_True_71 = true;

	private bool logic_uScript_IsHUDElementVisible_False_71 = true;

	private uScript_PointArrowAtVisible logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_73 = new uScript_PointArrowAtVisible();

	private object logic_uScript_PointArrowAtVisible_targetObject_73 = "";

	private float logic_uScript_PointArrowAtVisible_timeToShowFor_73 = -1f;

	private Vector3 logic_uScript_PointArrowAtVisible_offset_73 = new Vector3(0f, 0f, 0f);

	private bool logic_uScript_PointArrowAtVisible_Out_73 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_76 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_76;

	private bool logic_uScriptAct_SetBool_Out_76 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_76 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_76 = true;

	private uScript_Wait logic_uScript_Wait_uScript_Wait_78 = new uScript_Wait();

	private float logic_uScript_Wait_seconds_78;

	private bool logic_uScript_Wait_repeat_78;

	private bool logic_uScript_Wait_Waited_78 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_79 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_79;

	private bool logic_uScriptAct_SetBool_Out_79 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_79 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_79 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_80 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_80;

	private bool logic_uScriptCon_CompareBool_True_80 = true;

	private bool logic_uScriptCon_CompareBool_False_80 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_82 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_82;

	private bool logic_uScriptCon_CompareBool_True_82 = true;

	private bool logic_uScriptCon_CompareBool_False_82 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_86 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_86;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_86 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_86 = "FinalChunksSpawned";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_87 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_87;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_87 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_87 = "FinalResourcesSpawned";

	private uScript_IsFilterSetToMode logic_uScript_IsFilterSetToMode_uScript_IsFilterSetToMode_88 = new uScript_IsFilterSetToMode();

	private TankBlock logic_uScript_IsFilterSetToMode_filterBlock_88;

	private ModuleItemFilter.AcceptMode logic_uScript_IsFilterSetToMode_desiredMode_88 = ModuleItemFilter.AcceptMode.Fuel;

	private bool logic_uScript_IsFilterSetToMode_True_88 = true;

	private bool logic_uScript_IsFilterSetToMode_False_88 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_90 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_90;

	private bool logic_uScriptCon_CompareBool_True_90 = true;

	private bool logic_uScriptCon_CompareBool_False_90 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_92 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_92;

	private bool logic_uScriptAct_SetBool_Out_92 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_92 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_92 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_93 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_93;

	private bool logic_uScriptAct_SetBool_Out_93 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_93 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_93 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_96 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_96;

	private bool logic_uScriptCon_CompareBool_True_96 = true;

	private bool logic_uScriptCon_CompareBool_False_96 = true;

	private uScript_LockTechInteraction logic_uScript_LockTechInteraction_uScript_LockTechInteraction_97 = new uScript_LockTechInteraction();

	private Tank logic_uScript_LockTechInteraction_tech_97;

	private BlockTypes[] logic_uScript_LockTechInteraction_excludedBlocks_97 = new BlockTypes[0];

	private bool logic_uScript_LockTechInteraction_Out_97 = true;

	private uScript_LockTechInteraction logic_uScript_LockTechInteraction_uScript_LockTechInteraction_99 = new uScript_LockTechInteraction();

	private Tank logic_uScript_LockTechInteraction_tech_99;

	private BlockTypes[] logic_uScript_LockTechInteraction_excludedBlocks_99 = new BlockTypes[1] { BlockTypes.GSOFilter_111 };

	private bool logic_uScript_LockTechInteraction_Out_99 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_103 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_103;

	private bool logic_uScriptCon_CompareBool_True_103 = true;

	private bool logic_uScriptCon_CompareBool_False_103 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_104 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_104;

	private bool logic_uScriptAct_SetBool_Out_104 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_104 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_104 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_107 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_107;

	private bool logic_uScriptAct_SetBool_Out_107 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_107 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_107 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_108 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_108;

	private bool logic_uScriptCon_CompareBool_True_108 = true;

	private bool logic_uScriptCon_CompareBool_False_108 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_110 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_110;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_110 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_110 = "MsgSeeGeneratorWorkingShown";

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_112 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_112;

	private bool logic_uScriptAct_SetBool_Out_112 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_112 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_112 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_114 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_114;

	private bool logic_uScriptCon_CompareBool_True_114 = true;

	private bool logic_uScriptCon_CompareBool_False_114 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_116 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_116;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_116 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_116 = "MsgFilterTrainedShown";

	private uScript_GetPositionInEncounter logic_uScript_GetPositionInEncounter_uScript_GetPositionInEncounter_122 = new uScript_GetPositionInEncounter();

	private GameObject logic_uScript_GetPositionInEncounter_ownerNode_122;

	private string logic_uScript_GetPositionInEncounter_posName_122 = "";

	private Vector3 logic_uScript_GetPositionInEncounter_Return_122;

	private bool logic_uScript_GetPositionInEncounter_Out_122 = true;

	private uScript_SpawnResourceAtPosition logic_uScript_SpawnResourceAtPosition_uScript_SpawnResourceAtPosition_123 = new uScript_SpawnResourceAtPosition();

	private ChunkTypes logic_uScript_SpawnResourceAtPosition_chunkType_123;

	private int logic_uScript_SpawnResourceAtPosition_quantity_123;

	private Vector3 logic_uScript_SpawnResourceAtPosition_position_123;

	private bool logic_uScript_SpawnResourceAtPosition_Out_123 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_124 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_124;

	private bool logic_uScriptAct_SetBool_Out_124 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_124 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_124 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_126 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_126;

	private bool logic_uScriptCon_CompareBool_True_126 = true;

	private bool logic_uScriptCon_CompareBool_False_126 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_129 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_129;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_129 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_129 = "MsgWoodSpawned";

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_130 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_130;

	private bool logic_uScriptCon_CompareBool_True_130 = true;

	private bool logic_uScriptCon_CompareBool_False_130 = true;

	private uScript_IsPlayerInRangeOfTech logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_132 = new uScript_IsPlayerInRangeOfTech();

	private Tank logic_uScript_IsPlayerInRangeOfTech_tech_132;

	private float logic_uScript_IsPlayerInRangeOfTech_range_132 = 100f;

	private Tank[] logic_uScript_IsPlayerInRangeOfTech_techs_132 = new Tank[0];

	private bool logic_uScript_IsPlayerInRangeOfTech_Out_132 = true;

	private bool logic_uScript_IsPlayerInRangeOfTech_InRange_132 = true;

	private bool logic_uScript_IsPlayerInRangeOfTech_OutOfRange_132 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_134 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_134;

	private bool logic_uScriptAct_SetBool_Out_134 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_134 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_134 = true;

	private SubGraph_Crafting_Tutorial_ManageBlock logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_136 = new SubGraph_Crafting_Tutorial_ManageBlock();

	private SpawnBlockData[] logic_SubGraph_Crafting_Tutorial_ManageBlock_blockSpawnData_136 = new SpawnBlockData[0];

	private int logic_SubGraph_Crafting_Tutorial_ManageBlock_blockIndex_136 = 1;

	private TankBlock logic_SubGraph_Crafting_Tutorial_ManageBlock_block_136;

	private bool logic_SubGraph_Crafting_Tutorial_ManageBlock_allowAnchoring_136;

	private uScript_AddMessage.MessageData logic_SubGraph_Crafting_Tutorial_ManageBlock_msgBlockOutsideArea_136;

	private uScript_AddMessage.MessageSpeaker logic_SubGraph_Crafting_Tutorial_ManageBlock_messageSpeaker_136;

	private uScript_GetAndCheckBlocks logic_uScript_GetAndCheckBlocks_uScript_GetAndCheckBlocks_139 = new uScript_GetAndCheckBlocks();

	private SpawnBlockData[] logic_uScript_GetAndCheckBlocks_blockData_139 = new SpawnBlockData[0];

	private GameObject logic_uScript_GetAndCheckBlocks_ownerNode_139;

	private TankBlock[] logic_uScript_GetAndCheckBlocks_blocks_139 = new TankBlock[0];

	private bool logic_uScript_GetAndCheckBlocks_AllAlive_139 = true;

	private bool logic_uScript_GetAndCheckBlocks_SomeAlive_139 = true;

	private bool logic_uScript_GetAndCheckBlocks_AllDead_139 = true;

	private bool logic_uScript_GetAndCheckBlocks_WaitingToSpawn_139 = true;

	private uScript_SpawnBlocksFromData logic_uScript_SpawnBlocksFromData_uScript_SpawnBlocksFromData_143 = new uScript_SpawnBlocksFromData();

	private SpawnBlockData[] logic_uScript_SpawnBlocksFromData_blockData_143 = new SpawnBlockData[0];

	private GameObject logic_uScript_SpawnBlocksFromData_ownerNode_143;

	private bool logic_uScript_SpawnBlocksFromData_Out_143 = true;

	private uScript_IsShieldBlockPowered logic_uScript_IsShieldBlockPowered_uScript_IsShieldBlockPowered_144 = new uScript_IsShieldBlockPowered();

	private TankBlock logic_uScript_IsShieldBlockPowered_shieldBlock_144;

	private bool logic_uScript_IsShieldBlockPowered_True_144 = true;

	private bool logic_uScript_IsShieldBlockPowered_False_144 = true;

	private uScript_GetTankBlock logic_uScript_GetTankBlock_uScript_GetTankBlock_145 = new uScript_GetTankBlock();

	private Tank logic_uScript_GetTankBlock_tank_145;

	private BlockTypes logic_uScript_GetTankBlock_blockType_145;

	private TankBlock logic_uScript_GetTankBlock_Return_145;

	private bool logic_uScript_GetTankBlock_Out_145 = true;

	private bool logic_uScript_GetTankBlock_Returned_145 = true;

	private bool logic_uScript_GetTankBlock_NotFound_145 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_149 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_149;

	private bool logic_uScriptCon_CompareBool_True_149 = true;

	private bool logic_uScriptCon_CompareBool_False_149 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_151 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_151;

	private bool logic_uScriptAct_SetBool_Out_151 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_151 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_151 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_156 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_156 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_156 = true;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_156 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_157 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_157 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_157 = true;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_157 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_160 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_160;

	private bool logic_uScriptAct_SetBool_Out_160 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_160 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_160 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_161 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_161;

	private bool logic_uScriptCon_CompareBool_True_161 = true;

	private bool logic_uScriptCon_CompareBool_False_161 = true;

	private uScript_HideArrow logic_uScript_HideArrow_uScript_HideArrow_163 = new uScript_HideArrow();

	private bool logic_uScript_HideArrow_Out_163 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_164 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_164;

	private bool logic_uScriptAct_SetBool_Out_164 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_164 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_164 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_165 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_165 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_165 = true;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_165 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_167 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_167;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_167 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_167 = "ShieldPowered";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_169 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_169;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_169 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_169 = "msgReceiverAttachedShown";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_171 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_171;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_171 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_171 = "msgFilterSetToFuelShown";

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_173 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_173;

	private bool logic_uScriptAct_SetBool_Out_173 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_173 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_173 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_175 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_175;

	private bool logic_uScriptCon_CompareBool_True_175 = true;

	private bool logic_uScriptCon_CompareBool_False_175 = true;

	private SubGraph_Crafting_Tutorial_ManageBlock logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_178 = new SubGraph_Crafting_Tutorial_ManageBlock();

	private SpawnBlockData[] logic_SubGraph_Crafting_Tutorial_ManageBlock_blockSpawnData_178 = new SpawnBlockData[0];

	private int logic_SubGraph_Crafting_Tutorial_ManageBlock_blockIndex_178;

	private TankBlock logic_SubGraph_Crafting_Tutorial_ManageBlock_block_178;

	private bool logic_SubGraph_Crafting_Tutorial_ManageBlock_allowAnchoring_178;

	private uScript_AddMessage.MessageData logic_SubGraph_Crafting_Tutorial_ManageBlock_msgBlockOutsideArea_178;

	private uScript_AddMessage.MessageSpeaker logic_SubGraph_Crafting_Tutorial_ManageBlock_messageSpeaker_178;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_179 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_179 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_180 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_180;

	private bool logic_uScriptCon_CompareBool_True_180 = true;

	private bool logic_uScriptCon_CompareBool_False_180 = true;

	private uScriptCon_ManualSwitch logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_183 = new uScriptCon_ManualSwitch();

	private int logic_uScriptCon_ManualSwitch_CurrentOutput_183;

	private SubGraph_SaveLoadInt logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_185 = new SubGraph_SaveLoadInt();

	private int logic_SubGraph_SaveLoadInt_restartValue_185 = 1;

	private int logic_SubGraph_SaveLoadInt_integer_185;

	private object logic_SubGraph_SaveLoadInt_intAsVariable_185 = "";

	private string logic_SubGraph_SaveLoadInt_uniqueID_185 = "Stage";

	private SubGraph_LoadObjectiveStates logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_187 = new SubGraph_LoadObjectiveStates();

	private int logic_SubGraph_LoadObjectiveStates_currentObjective_187;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_190 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_190;

	private bool logic_uScriptAct_SetBool_Out_190 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_190 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_190 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_192 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_192;

	private bool logic_uScriptAct_SetBool_Out_192 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_192 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_192 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_194 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_194;

	private bool logic_uScriptAct_SetBool_Out_194 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_194 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_194 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_196 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_196;

	private bool logic_uScriptAct_SetBool_Out_196 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_196 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_196 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_197 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_197;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_197 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_197 = "AllowFilterInteraction";

	private SubGraph_CompleteObjectiveStage logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_201 = new SubGraph_CompleteObjectiveStage();

	private int logic_SubGraph_CompleteObjectiveStage_objectiveStage_201;

	private bool logic_SubGraph_CompleteObjectiveStage_isFinalObjective_201;

	private SubGraph_CompleteObjectiveStage logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_203 = new SubGraph_CompleteObjectiveStage();

	private int logic_SubGraph_CompleteObjectiveStage_objectiveStage_203;

	private bool logic_SubGraph_CompleteObjectiveStage_isFinalObjective_203;

	private SubGraph_CompleteObjectiveStage logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_205 = new SubGraph_CompleteObjectiveStage();

	private int logic_SubGraph_CompleteObjectiveStage_objectiveStage_205;

	private bool logic_SubGraph_CompleteObjectiveStage_isFinalObjective_205;

	private SubGraph_CompleteObjectiveStage logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_207 = new SubGraph_CompleteObjectiveStage();

	private int logic_SubGraph_CompleteObjectiveStage_objectiveStage_207;

	private bool logic_SubGraph_CompleteObjectiveStage_isFinalObjective_207;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_208 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_208;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_208;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_208;

	private bool logic_uScript_AddMessage_Out_208 = true;

	private bool logic_uScript_AddMessage_Shown_208 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_211 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_211;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_211;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_211;

	private bool logic_uScript_AddMessage_Out_211 = true;

	private bool logic_uScript_AddMessage_Shown_211 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_215 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_215;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_215;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_215;

	private bool logic_uScript_AddMessage_Out_215 = true;

	private bool logic_uScript_AddMessage_Shown_215 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_218 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_218;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_218;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_218;

	private bool logic_uScript_AddMessage_Out_218 = true;

	private bool logic_uScript_AddMessage_Shown_218 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_222 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_222;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_222;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_222;

	private bool logic_uScript_AddMessage_Out_222 = true;

	private bool logic_uScript_AddMessage_Shown_222 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_226 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_226;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_226;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_226;

	private bool logic_uScript_AddMessage_Out_226 = true;

	private bool logic_uScript_AddMessage_Shown_226 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_229 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_229;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_229;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_229;

	private bool logic_uScript_AddMessage_Out_229 = true;

	private bool logic_uScript_AddMessage_Shown_229 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_232 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_232;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_232;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_232;

	private bool logic_uScript_AddMessage_Out_232 = true;

	private bool logic_uScript_AddMessage_Shown_232 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_235 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_235;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_235;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_235;

	private bool logic_uScript_AddMessage_Out_235 = true;

	private bool logic_uScript_AddMessage_Shown_235 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_238 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_238;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_238;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_238;

	private bool logic_uScript_AddMessage_Out_238 = true;

	private bool logic_uScript_AddMessage_Shown_238 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_242 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_242;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_242;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_242;

	private bool logic_uScript_AddMessage_Out_242 = true;

	private bool logic_uScript_AddMessage_Shown_242 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_245 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_245;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_245;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_245;

	private bool logic_uScript_AddMessage_Out_245 = true;

	private bool logic_uScript_AddMessage_Shown_245 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_251 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_251;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_251;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_251;

	private bool logic_uScript_AddMessage_Out_251 = true;

	private bool logic_uScript_AddMessage_Shown_251 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_254 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_254;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_254;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_254;

	private bool logic_uScript_AddMessage_Out_254 = true;

	private bool logic_uScript_AddMessage_Shown_254 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_255 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_255;

	private bool logic_uScriptCon_CompareBool_True_255 = true;

	private bool logic_uScriptCon_CompareBool_False_255 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_256 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_256;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_256;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_256;

	private bool logic_uScript_AddMessage_Out_256 = true;

	private bool logic_uScript_AddMessage_Shown_256 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_261 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_261;

	private bool logic_uScriptAct_SetBool_Out_261 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_261 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_261 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_269 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_269;

	private bool logic_uScriptAct_SetBool_Out_269 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_269 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_269 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_270 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_270;

	private bool logic_uScriptCon_CompareBool_True_270 = true;

	private bool logic_uScriptCon_CompareBool_False_270 = true;

	private uScript_HideArrow logic_uScript_HideArrow_uScript_HideArrow_272 = new uScript_HideArrow();

	private bool logic_uScript_HideArrow_Out_272 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_274 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_274;

	private bool logic_uScriptAct_SetBool_Out_274 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_274 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_274 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_275 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_275 = true;

	private SubGraph_Crafting_Tutorial_Finish logic_SubGraph_Crafting_Tutorial_Finish_SubGraph_Crafting_Tutorial_Finish_281 = new SubGraph_Crafting_Tutorial_Finish();

	private Tank logic_SubGraph_Crafting_Tutorial_Finish_craftingBaseTech_281;

	private Tank logic_SubGraph_Crafting_Tutorial_Finish_NPCTech_281;

	private ExternalBehaviorTree logic_SubGraph_Crafting_Tutorial_Finish_NPCFlyAwayAI_281;

	private Transform logic_SubGraph_Crafting_Tutorial_Finish_NPCDespawnParticleEffect_281;

	private SubGraph_Crafting_Tutorial_Init logic_SubGraph_Crafting_Tutorial_Init_SubGraph_Crafting_Tutorial_Init_287 = new SubGraph_Crafting_Tutorial_Init();

	private SpawnTechData[] logic_SubGraph_Crafting_Tutorial_Init_baseSpawnData_287 = new SpawnTechData[0];

	private SpawnBlockData[] logic_SubGraph_Crafting_Tutorial_Init_blockSpawnData_287 = new SpawnBlockData[0];

	private SpawnTechData[] logic_SubGraph_Crafting_Tutorial_Init_NPCSpawnData_287 = new SpawnTechData[0];

	private TankPreset logic_SubGraph_Crafting_Tutorial_Init_completedBasePreset_287;

	private bool logic_SubGraph_Crafting_Tutorial_Init_useNPCAsTutorialTech_287;

	private bool logic_SubGraph_Crafting_Tutorial_Init_spawnBase_287 = true;

	private string logic_SubGraph_Crafting_Tutorial_Init_basePosition_287 = "";

	private float logic_SubGraph_Crafting_Tutorial_Init_clearSceneryRadius_287;

	private Tank logic_SubGraph_Crafting_Tutorial_Init_CraftingBaseTech_287;

	private Tank logic_SubGraph_Crafting_Tutorial_Init_NPCTech_287;

	private uScript_SetEncounterTarget logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_293 = new uScript_SetEncounterTarget();

	private GameObject logic_uScript_SetEncounterTarget_owner_293;

	private object logic_uScript_SetEncounterTarget_visibleObject_293 = "";

	private bool logic_uScript_SetEncounterTarget_Out_293 = true;

	private uScript_SetBatteryChargeAmount logic_uScript_SetBatteryChargeAmount_uScript_SetBatteryChargeAmount_294 = new uScript_SetBatteryChargeAmount();

	private Tank logic_uScript_SetBatteryChargeAmount_tech_294;

	private float logic_uScript_SetBatteryChargeAmount_chargeAmount_294;

	private bool logic_uScript_SetBatteryChargeAmount_Out_294 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_297 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_297;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_297 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_297 = "BatteriesDrained";

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_298 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_298;

	private bool logic_uScriptCon_CompareBool_True_298 = true;

	private bool logic_uScriptCon_CompareBool_False_298 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_300 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_300;

	private bool logic_uScriptAct_SetBool_Out_300 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_300 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_300 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_302 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_302;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_302 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_302 = "msgIntroShown";

	private uScript_SpawnResourceListOnHolder logic_uScript_SpawnResourceListOnHolder_uScript_SpawnResourceListOnHolder_303 = new uScript_SpawnResourceListOnHolder();

	private Tank logic_uScript_SpawnResourceListOnHolder_tech_303;

	private ChunkTypes[] logic_uScript_SpawnResourceListOnHolder_chunks_303 = new ChunkTypes[0];

	private BlockTypes logic_uScript_SpawnResourceListOnHolder_blockType_303 = BlockTypes.GSOReceiver_111;

	private bool logic_uScript_SpawnResourceListOnHolder_Out_303 = true;

	private uScript_SpawnResourceListOnHolder logic_uScript_SpawnResourceListOnHolder_uScript_SpawnResourceListOnHolder_306 = new uScript_SpawnResourceListOnHolder();

	private Tank logic_uScript_SpawnResourceListOnHolder_tech_306;

	private ChunkTypes[] logic_uScript_SpawnResourceListOnHolder_chunks_306 = new ChunkTypes[0];

	private BlockTypes logic_uScript_SpawnResourceListOnHolder_blockType_306 = BlockTypes.GSOReceiver_111;

	private bool logic_uScript_SpawnResourceListOnHolder_Out_306 = true;

	private uScript_RestrictItemPickup logic_uScript_RestrictItemPickup_uScript_RestrictItemPickup_309 = new uScript_RestrictItemPickup();

	private Tank logic_uScript_RestrictItemPickup_tech_309;

	private ChunkTypes[] logic_uScript_RestrictItemPickup_typesToAccept_309 = new ChunkTypes[0];

	private bool logic_uScript_RestrictItemPickup_Out_309 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_312 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_312;

	private bool logic_uScriptCon_CompareBool_True_312 = true;

	private bool logic_uScriptCon_CompareBool_False_312 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_314 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_314;

	private bool logic_uScriptAct_SetBool_Out_314 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_314 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_314 = true;

	private SubGraph_CompleteObjectiveStage logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_316 = new SubGraph_CompleteObjectiveStage();

	private int logic_SubGraph_CompleteObjectiveStage_objectiveStage_316;

	private bool logic_SubGraph_CompleteObjectiveStage_isFinalObjective_316;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_317 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_317;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_317 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_317 = "GeneratorAttached";

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_321 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_321;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_321;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_321;

	private bool logic_uScript_AddMessage_Out_321 = true;

	private bool logic_uScript_AddMessage_Shown_321 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_322 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_322 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_322 = true;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_322 = true;

	private uScript_Wait logic_uScript_Wait_uScript_Wait_324 = new uScript_Wait();

	private float logic_uScript_Wait_seconds_324 = 5f;

	private bool logic_uScript_Wait_repeat_324;

	private bool logic_uScript_Wait_Waited_324 = true;

	private uScript_HideHUDElement logic_uScript_HideHUDElement_uScript_HideHUDElement_325 = new uScript_HideHUDElement();

	private ManHUD.HUDElementType logic_uScript_HideHUDElement_hudElement_325 = ManHUD.HUDElementType.HUDMask;

	private bool logic_uScript_HideHUDElement_Out_325 = true;

	private SubGraph_AddMessageWithPadSupport logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_326 = new SubGraph_AddMessageWithPadSupport();

	private uScript_AddMessage.MessageData logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboard_326;

	private uScript_AddMessage.MessageData logic_SubGraph_AddMessageWithPadSupport_messageControlPad_326;

	private uScript_AddMessage.MessageSpeaker logic_SubGraph_AddMessageWithPadSupport_speaker_326;

	private ManOnScreenMessages.OnScreenMessage logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboardReturn_326;

	private ManOnScreenMessages.OnScreenMessage logic_SubGraph_AddMessageWithPadSupport_messageControlPadReturn_326;

	private SubGraph_AddMessageWithPadSupport logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_329 = new SubGraph_AddMessageWithPadSupport();

	private uScript_AddMessage.MessageData logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboard_329;

	private uScript_AddMessage.MessageData logic_SubGraph_AddMessageWithPadSupport_messageControlPad_329;

	private uScript_AddMessage.MessageSpeaker logic_SubGraph_AddMessageWithPadSupport_speaker_329;

	private ManOnScreenMessages.OnScreenMessage logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboardReturn_329;

	private ManOnScreenMessages.OnScreenMessage logic_SubGraph_AddMessageWithPadSupport_messageControlPadReturn_329;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_332 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_332 = "";

	private bool logic_uScript_EnableGlow_enable_332 = true;

	private bool logic_uScript_EnableGlow_Out_332 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_335 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_335 = "";

	private bool logic_uScript_EnableGlow_enable_335;

	private bool logic_uScript_EnableGlow_Out_335 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_337 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_337 = "";

	private bool logic_uScript_EnableGlow_enable_337 = true;

	private bool logic_uScript_EnableGlow_Out_337 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_339 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_339 = "";

	private bool logic_uScript_EnableGlow_enable_339;

	private bool logic_uScript_EnableGlow_Out_339 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_340 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_340 = "";

	private bool logic_uScript_EnableGlow_enable_340;

	private bool logic_uScript_EnableGlow_Out_340 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_343 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_343 = "";

	private bool logic_uScript_EnableGlow_enable_343;

	private bool logic_uScript_EnableGlow_Out_343 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_345 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_345;

	private bool logic_uScriptCon_CompareBool_True_345 = true;

	private bool logic_uScriptCon_CompareBool_False_345 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_346 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_346 = "";

	private bool logic_uScript_EnableGlow_enable_346;

	private bool logic_uScript_EnableGlow_Out_346 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_348 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_348 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_349 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_349;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_349;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_349;

	private bool logic_uScript_AddMessage_Out_349 = true;

	private bool logic_uScript_AddMessage_Shown_349 = true;

	private uScript_GetJoypadControlMode logic_uScript_GetJoypadControlMode_uScript_GetJoypadControlMode_350 = new uScript_GetJoypadControlMode();

	private bool logic_uScript_GetJoypadControlMode_Joypad_350 = true;

	private bool logic_uScript_GetJoypadControlMode_MouseAndKeyboard_350 = true;

	private uScript_IsHUDElementVisible logic_uScript_IsHUDElementVisible_uScript_IsHUDElementVisible_353 = new uScript_IsHUDElementVisible();

	private ManHUD.HUDElementType logic_uScript_IsHUDElementVisible_hudElement_353 = ManHUD.HUDElementType.InteractionMode;

	private bool logic_uScript_IsHUDElementVisible_True_353 = true;

	private bool logic_uScript_IsHUDElementVisible_False_353 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_354 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_354;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_354;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_354;

	private bool logic_uScript_AddMessage_Out_354 = true;

	private bool logic_uScript_AddMessage_Shown_354 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_358 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_358;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_358;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_358;

	private bool logic_uScript_AddMessage_Out_358 = true;

	private bool logic_uScript_AddMessage_Shown_358 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_360 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_360 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_362 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_362 = "";

	private bool logic_uScript_EnableGlow_enable_362;

	private bool logic_uScript_EnableGlow_Out_362 = true;

	private uScript_HideArrow logic_uScript_HideArrow_uScript_HideArrow_363 = new uScript_HideArrow();

	private bool logic_uScript_HideArrow_Out_363 = true;

	private uScript_FilterTutorialHighlightMode logic_uScript_FilterTutorialHighlightMode_uScript_FilterTutorialHighlightMode_364 = new uScript_FilterTutorialHighlightMode();

	private UIFilterMenu.FilterAcceptMode logic_uScript_FilterTutorialHighlightMode_filterMode_364 = UIFilterMenu.FilterAcceptMode.SpecificCategory;

	private bool logic_uScript_FilterTutorialHighlightMode_Out_364 = true;

	private uScript_FilterTutorialClearHighlight logic_uScript_FilterTutorialClearHighlight_uScript_FilterTutorialClearHighlight_365 = new uScript_FilterTutorialClearHighlight();

	private bool logic_uScript_FilterTutorialClearHighlight_Out_365 = true;

	private uScript_FilterTutorialHighlightCategory logic_uScript_FilterTutorialHighlightCategory_uScript_FilterTutorialHighlightCategory_366 = new uScript_FilterTutorialHighlightCategory();

	private ChunkCategory logic_uScript_FilterTutorialHighlightCategory_chunkCategory_366 = ChunkCategory.Fuel;

	private bool logic_uScript_FilterTutorialHighlightCategory_Out_366 = true;

	private uScript_CheckFilterUISpecificGroupMenuVisible logic_uScript_CheckFilterUISpecificGroupMenuVisible_uScript_CheckFilterUISpecificGroupMenuVisible_367 = new uScript_CheckFilterUISpecificGroupMenuVisible();

	private bool logic_uScript_CheckFilterUISpecificGroupMenuVisible_Visible_367 = true;

	private bool logic_uScript_CheckFilterUISpecificGroupMenuVisible_NotVisible_367 = true;

	private uScript_SendAnaliticsEvent logic_uScript_SendAnaliticsEvent_uScript_SendAnaliticsEvent_368 = new uScript_SendAnaliticsEvent();

	private string logic_uScript_SendAnaliticsEvent_analiticsEvent_368 = "tutorial_start";

	private string logic_uScript_SendAnaliticsEvent_parameterName_368 = "crafting_tutorial";

	private object logic_uScript_SendAnaliticsEvent_parameter_368 = "3";

	private bool logic_uScript_SendAnaliticsEvent_Out_368 = true;

	private uScript_SendAnaliticsEvent logic_uScript_SendAnaliticsEvent_uScript_SendAnaliticsEvent_369 = new uScript_SendAnaliticsEvent();

	private string logic_uScript_SendAnaliticsEvent_analiticsEvent_369 = "tutorial_complete";

	private string logic_uScript_SendAnaliticsEvent_parameterName_369 = "crafting_tutorial";

	private object logic_uScript_SendAnaliticsEvent_parameter_369 = "3";

	private bool logic_uScript_SendAnaliticsEvent_Out_369 = true;

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
		if (null == owner_Connection_119 || !m_RegisteredForEvents)
		{
			owner_Connection_119 = parentGameObject;
		}
		if (null == owner_Connection_140 || !m_RegisteredForEvents)
		{
			owner_Connection_140 = parentGameObject;
		}
		if (null == owner_Connection_142 || !m_RegisteredForEvents)
		{
			owner_Connection_142 = parentGameObject;
		}
		if (null == owner_Connection_292 || !m_RegisteredForEvents)
		{
			owner_Connection_292 = parentGameObject;
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
	}

	public override void SetParent(GameObject g)
	{
		parentGameObject = g;
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_2.SetParent(g);
		logic_uScript_PausePopulation_uScript_PausePopulation_3.SetParent(g);
		logic_uScript_PausePopulation_uScript_PausePopulation_4.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_7.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_8.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_11.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_13.SetParent(g);
		logic_uScript_Wait_uScript_Wait_14.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_17.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_22.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_23.SetParent(g);
		logic_uScript_IsFilterAcceptingSpecificItem_uScript_IsFilterAcceptingSpecificItem_27.SetParent(g);
		logic_uScript_HideArrow_uScript_HideArrow_28.SetParent(g);
		logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_SubGraph_Crafting_Tutorial_AttachBlockToBase_29.SetParent(g);
		logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_SubGraph_Crafting_Tutorial_AttachBlockToBase_33.SetParent(g);
		logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_36.SetParent(g);
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_39.SetParent(g);
		logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_SubGraph_Crafting_Tutorial_AttachBlockToBase_42.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_44.SetParent(g);
		logic_uScript_LockTechStacks_uScript_LockTechStacks_45.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_48.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_49.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_52.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_53.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_55.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_58.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_60.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_62.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_64.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_65.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_67.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_70.SetParent(g);
		logic_uScript_IsHUDElementVisible_uScript_IsHUDElementVisible_71.SetParent(g);
		logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_73.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_76.SetParent(g);
		logic_uScript_Wait_uScript_Wait_78.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_79.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_80.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_82.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_86.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_87.SetParent(g);
		logic_uScript_IsFilterSetToMode_uScript_IsFilterSetToMode_88.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_90.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_92.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_93.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_96.SetParent(g);
		logic_uScript_LockTechInteraction_uScript_LockTechInteraction_97.SetParent(g);
		logic_uScript_LockTechInteraction_uScript_LockTechInteraction_99.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_103.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_104.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_107.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_108.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_110.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_112.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_114.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_116.SetParent(g);
		logic_uScript_GetPositionInEncounter_uScript_GetPositionInEncounter_122.SetParent(g);
		logic_uScript_SpawnResourceAtPosition_uScript_SpawnResourceAtPosition_123.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_124.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_126.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_129.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_130.SetParent(g);
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_132.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_134.SetParent(g);
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_136.SetParent(g);
		logic_uScript_GetAndCheckBlocks_uScript_GetAndCheckBlocks_139.SetParent(g);
		logic_uScript_SpawnBlocksFromData_uScript_SpawnBlocksFromData_143.SetParent(g);
		logic_uScript_IsShieldBlockPowered_uScript_IsShieldBlockPowered_144.SetParent(g);
		logic_uScript_GetTankBlock_uScript_GetTankBlock_145.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_149.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_151.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_156.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_157.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_160.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_161.SetParent(g);
		logic_uScript_HideArrow_uScript_HideArrow_163.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_164.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_165.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_167.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_169.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_171.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_173.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_175.SetParent(g);
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_178.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_179.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_180.SetParent(g);
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_183.SetParent(g);
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_185.SetParent(g);
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_187.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_190.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_192.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_194.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_196.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_197.SetParent(g);
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_201.SetParent(g);
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_203.SetParent(g);
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_205.SetParent(g);
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_207.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_208.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_211.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_215.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_218.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_222.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_226.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_229.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_232.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_235.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_238.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_242.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_245.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_251.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_254.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_255.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_256.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_261.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_269.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_270.SetParent(g);
		logic_uScript_HideArrow_uScript_HideArrow_272.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_274.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_275.SetParent(g);
		logic_SubGraph_Crafting_Tutorial_Finish_SubGraph_Crafting_Tutorial_Finish_281.SetParent(g);
		logic_SubGraph_Crafting_Tutorial_Init_SubGraph_Crafting_Tutorial_Init_287.SetParent(g);
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_293.SetParent(g);
		logic_uScript_SetBatteryChargeAmount_uScript_SetBatteryChargeAmount_294.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_297.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_298.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_300.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_302.SetParent(g);
		logic_uScript_SpawnResourceListOnHolder_uScript_SpawnResourceListOnHolder_303.SetParent(g);
		logic_uScript_SpawnResourceListOnHolder_uScript_SpawnResourceListOnHolder_306.SetParent(g);
		logic_uScript_RestrictItemPickup_uScript_RestrictItemPickup_309.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_312.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_314.SetParent(g);
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_316.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_317.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_321.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_322.SetParent(g);
		logic_uScript_Wait_uScript_Wait_324.SetParent(g);
		logic_uScript_HideHUDElement_uScript_HideHUDElement_325.SetParent(g);
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_326.SetParent(g);
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_329.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_332.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_335.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_337.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_339.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_340.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_343.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_345.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_346.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_348.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_349.SetParent(g);
		logic_uScript_GetJoypadControlMode_uScript_GetJoypadControlMode_350.SetParent(g);
		logic_uScript_IsHUDElementVisible_uScript_IsHUDElementVisible_353.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_354.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_358.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_360.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_362.SetParent(g);
		logic_uScript_HideArrow_uScript_HideArrow_363.SetParent(g);
		logic_uScript_FilterTutorialHighlightMode_uScript_FilterTutorialHighlightMode_364.SetParent(g);
		logic_uScript_FilterTutorialClearHighlight_uScript_FilterTutorialClearHighlight_365.SetParent(g);
		logic_uScript_FilterTutorialHighlightCategory_uScript_FilterTutorialHighlightCategory_366.SetParent(g);
		logic_uScript_CheckFilterUISpecificGroupMenuVisible_uScript_CheckFilterUISpecificGroupMenuVisible_367.SetParent(g);
		logic_uScript_SendAnaliticsEvent_uScript_SendAnaliticsEvent_368.SetParent(g);
		logic_uScript_SendAnaliticsEvent_uScript_SendAnaliticsEvent_369.SetParent(g);
		owner_Connection_1 = parentGameObject;
		owner_Connection_10 = parentGameObject;
		owner_Connection_119 = parentGameObject;
		owner_Connection_140 = parentGameObject;
		owner_Connection_142 = parentGameObject;
		owner_Connection_292 = parentGameObject;
	}

	public void Awake()
	{
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_11.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_22.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_23.Awake();
		logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_SubGraph_Crafting_Tutorial_AttachBlockToBase_29.Awake();
		logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_SubGraph_Crafting_Tutorial_AttachBlockToBase_33.Awake();
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_39.Awake();
		logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_SubGraph_Crafting_Tutorial_AttachBlockToBase_42.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_44.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_52.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_58.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_60.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_62.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_64.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_70.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_86.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_87.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_110.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_116.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_129.Awake();
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_136.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_167.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_169.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_171.Awake();
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_178.Awake();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_185.Awake();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_187.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_197.Awake();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_201.Awake();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_203.Awake();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_205.Awake();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_207.Awake();
		logic_SubGraph_Crafting_Tutorial_Finish_SubGraph_Crafting_Tutorial_Finish_281.Awake();
		logic_SubGraph_Crafting_Tutorial_Init_SubGraph_Crafting_Tutorial_Init_287.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_297.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_302.Awake();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_316.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_317.Awake();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_326.Awake();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_329.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_11.Save_Out += SubGraph_SaveLoadBool_Save_Out_11;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_11.Load_Out += SubGraph_SaveLoadBool_Load_Out_11;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_11.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_11;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_22.Save_Out += SubGraph_SaveLoadBool_Save_Out_22;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_22.Load_Out += SubGraph_SaveLoadBool_Load_Out_22;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_22.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_22;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_23.Save_Out += SubGraph_SaveLoadBool_Save_Out_23;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_23.Load_Out += SubGraph_SaveLoadBool_Load_Out_23;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_23.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_23;
		logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_SubGraph_Crafting_Tutorial_AttachBlockToBase_29.Block_Attached += SubGraph_Crafting_Tutorial_AttachBlockToBase_Block_Attached_29;
		logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_SubGraph_Crafting_Tutorial_AttachBlockToBase_33.Block_Attached += SubGraph_Crafting_Tutorial_AttachBlockToBase_Block_Attached_33;
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_39.Out += SubGraph_Crafting_Tutorial_ManageBlock_Out_39;
		logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_SubGraph_Crafting_Tutorial_AttachBlockToBase_42.Block_Attached += SubGraph_Crafting_Tutorial_AttachBlockToBase_Block_Attached_42;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_44.Save_Out += SubGraph_SaveLoadBool_Save_Out_44;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_44.Load_Out += SubGraph_SaveLoadBool_Load_Out_44;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_44.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_44;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_52.Save_Out += SubGraph_SaveLoadBool_Save_Out_52;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_52.Load_Out += SubGraph_SaveLoadBool_Load_Out_52;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_52.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_52;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_58.Save_Out += SubGraph_SaveLoadBool_Save_Out_58;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_58.Load_Out += SubGraph_SaveLoadBool_Load_Out_58;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_58.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_58;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_60.Save_Out += SubGraph_SaveLoadBool_Save_Out_60;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_60.Load_Out += SubGraph_SaveLoadBool_Load_Out_60;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_60.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_60;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_62.Save_Out += SubGraph_SaveLoadBool_Save_Out_62;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_62.Load_Out += SubGraph_SaveLoadBool_Load_Out_62;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_62.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_62;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_64.Save_Out += SubGraph_SaveLoadBool_Save_Out_64;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_64.Load_Out += SubGraph_SaveLoadBool_Load_Out_64;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_64.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_64;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_70.Save_Out += SubGraph_SaveLoadBool_Save_Out_70;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_70.Load_Out += SubGraph_SaveLoadBool_Load_Out_70;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_70.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_70;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_86.Save_Out += SubGraph_SaveLoadBool_Save_Out_86;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_86.Load_Out += SubGraph_SaveLoadBool_Load_Out_86;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_86.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_86;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_87.Save_Out += SubGraph_SaveLoadBool_Save_Out_87;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_87.Load_Out += SubGraph_SaveLoadBool_Load_Out_87;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_87.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_87;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_110.Save_Out += SubGraph_SaveLoadBool_Save_Out_110;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_110.Load_Out += SubGraph_SaveLoadBool_Load_Out_110;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_110.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_110;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_116.Save_Out += SubGraph_SaveLoadBool_Save_Out_116;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_116.Load_Out += SubGraph_SaveLoadBool_Load_Out_116;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_116.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_116;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_129.Save_Out += SubGraph_SaveLoadBool_Save_Out_129;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_129.Load_Out += SubGraph_SaveLoadBool_Load_Out_129;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_129.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_129;
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_136.Out += SubGraph_Crafting_Tutorial_ManageBlock_Out_136;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_167.Save_Out += SubGraph_SaveLoadBool_Save_Out_167;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_167.Load_Out += SubGraph_SaveLoadBool_Load_Out_167;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_167.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_167;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_169.Save_Out += SubGraph_SaveLoadBool_Save_Out_169;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_169.Load_Out += SubGraph_SaveLoadBool_Load_Out_169;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_169.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_169;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_171.Save_Out += SubGraph_SaveLoadBool_Save_Out_171;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_171.Load_Out += SubGraph_SaveLoadBool_Load_Out_171;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_171.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_171;
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_178.Out += SubGraph_Crafting_Tutorial_ManageBlock_Out_178;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_183.Output1 += uScriptCon_ManualSwitch_Output1_183;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_183.Output2 += uScriptCon_ManualSwitch_Output2_183;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_183.Output3 += uScriptCon_ManualSwitch_Output3_183;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_183.Output4 += uScriptCon_ManualSwitch_Output4_183;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_183.Output5 += uScriptCon_ManualSwitch_Output5_183;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_183.Output6 += uScriptCon_ManualSwitch_Output6_183;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_183.Output7 += uScriptCon_ManualSwitch_Output7_183;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_183.Output8 += uScriptCon_ManualSwitch_Output8_183;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_185.Save_Out += SubGraph_SaveLoadInt_Save_Out_185;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_185.Load_Out += SubGraph_SaveLoadInt_Load_Out_185;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_185.Restart_Out += SubGraph_SaveLoadInt_Restart_Out_185;
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_187.Out += SubGraph_LoadObjectiveStates_Out_187;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_197.Save_Out += SubGraph_SaveLoadBool_Save_Out_197;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_197.Load_Out += SubGraph_SaveLoadBool_Load_Out_197;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_197.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_197;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_201.Out += SubGraph_CompleteObjectiveStage_Out_201;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_203.Out += SubGraph_CompleteObjectiveStage_Out_203;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_205.Out += SubGraph_CompleteObjectiveStage_Out_205;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_207.Out += SubGraph_CompleteObjectiveStage_Out_207;
		logic_SubGraph_Crafting_Tutorial_Finish_SubGraph_Crafting_Tutorial_Finish_281.Out += SubGraph_Crafting_Tutorial_Finish_Out_281;
		logic_SubGraph_Crafting_Tutorial_Init_SubGraph_Crafting_Tutorial_Init_287.Out += SubGraph_Crafting_Tutorial_Init_Out_287;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_297.Save_Out += SubGraph_SaveLoadBool_Save_Out_297;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_297.Load_Out += SubGraph_SaveLoadBool_Load_Out_297;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_297.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_297;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_302.Save_Out += SubGraph_SaveLoadBool_Save_Out_302;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_302.Load_Out += SubGraph_SaveLoadBool_Load_Out_302;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_302.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_302;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_316.Out += SubGraph_CompleteObjectiveStage_Out_316;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_317.Save_Out += SubGraph_SaveLoadBool_Save_Out_317;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_317.Load_Out += SubGraph_SaveLoadBool_Load_Out_317;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_317.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_317;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_326.Out += SubGraph_AddMessageWithPadSupport_Out_326;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_326.Shown += SubGraph_AddMessageWithPadSupport_Shown_326;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_329.Out += SubGraph_AddMessageWithPadSupport_Out_329;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_329.Shown += SubGraph_AddMessageWithPadSupport_Shown_329;
	}

	public void Start()
	{
		SyncUnityHooks();
		m_RegisteredForEvents = true;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_11.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_22.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_23.Start();
		logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_SubGraph_Crafting_Tutorial_AttachBlockToBase_29.Start();
		logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_SubGraph_Crafting_Tutorial_AttachBlockToBase_33.Start();
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_39.Start();
		logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_SubGraph_Crafting_Tutorial_AttachBlockToBase_42.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_44.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_52.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_58.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_60.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_62.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_64.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_70.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_86.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_87.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_110.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_116.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_129.Start();
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_136.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_167.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_169.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_171.Start();
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_178.Start();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_185.Start();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_187.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_197.Start();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_201.Start();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_203.Start();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_205.Start();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_207.Start();
		logic_SubGraph_Crafting_Tutorial_Finish_SubGraph_Crafting_Tutorial_Finish_281.Start();
		logic_SubGraph_Crafting_Tutorial_Init_SubGraph_Crafting_Tutorial_Init_287.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_297.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_302.Start();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_316.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_317.Start();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_326.Start();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_329.Start();
	}

	public void OnEnable()
	{
		RegisterForUnityHooks();
		m_RegisteredForEvents = true;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_11.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_22.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_23.OnEnable();
		logic_uScript_IsFilterAcceptingSpecificItem_uScript_IsFilterAcceptingSpecificItem_27.OnEnable();
		logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_SubGraph_Crafting_Tutorial_AttachBlockToBase_29.OnEnable();
		logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_SubGraph_Crafting_Tutorial_AttachBlockToBase_33.OnEnable();
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_39.OnEnable();
		logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_SubGraph_Crafting_Tutorial_AttachBlockToBase_42.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_44.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_52.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_58.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_60.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_62.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_64.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_70.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_86.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_87.OnEnable();
		logic_uScript_IsFilterSetToMode_uScript_IsFilterSetToMode_88.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_110.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_116.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_129.OnEnable();
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_136.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_167.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_169.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_171.OnEnable();
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_178.OnEnable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_185.OnEnable();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_187.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_197.OnEnable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_201.OnEnable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_203.OnEnable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_205.OnEnable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_207.OnEnable();
		logic_SubGraph_Crafting_Tutorial_Finish_SubGraph_Crafting_Tutorial_Finish_281.OnEnable();
		logic_SubGraph_Crafting_Tutorial_Init_SubGraph_Crafting_Tutorial_Init_287.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_297.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_302.OnEnable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_316.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_317.OnEnable();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_326.OnEnable();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_329.OnEnable();
		logic_uScript_CheckFilterUISpecificGroupMenuVisible_uScript_CheckFilterUISpecificGroupMenuVisible_367.OnEnable();
	}

	public void OnDisable()
	{
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_2.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_11.OnDisable();
		logic_uScript_Wait_uScript_Wait_14.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_22.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_23.OnDisable();
		logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_SubGraph_Crafting_Tutorial_AttachBlockToBase_29.OnDisable();
		logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_SubGraph_Crafting_Tutorial_AttachBlockToBase_33.OnDisable();
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_39.OnDisable();
		logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_SubGraph_Crafting_Tutorial_AttachBlockToBase_42.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_44.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_52.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_58.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_60.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_62.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_64.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_70.OnDisable();
		logic_uScript_Wait_uScript_Wait_78.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_86.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_87.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_110.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_116.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_129.OnDisable();
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_132.OnDisable();
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_136.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_167.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_169.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_171.OnDisable();
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_178.OnDisable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_185.OnDisable();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_187.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_197.OnDisable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_201.OnDisable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_203.OnDisable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_205.OnDisable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_207.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_208.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_211.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_215.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_218.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_222.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_226.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_229.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_232.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_235.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_238.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_242.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_245.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_251.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_254.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_256.OnDisable();
		logic_SubGraph_Crafting_Tutorial_Finish_SubGraph_Crafting_Tutorial_Finish_281.OnDisable();
		logic_SubGraph_Crafting_Tutorial_Init_SubGraph_Crafting_Tutorial_Init_287.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_297.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_302.OnDisable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_316.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_317.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_321.OnDisable();
		logic_uScript_Wait_uScript_Wait_324.OnDisable();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_326.OnDisable();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_329.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_349.OnDisable();
		logic_uScript_GetJoypadControlMode_uScript_GetJoypadControlMode_350.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_354.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_358.OnDisable();
		UnregisterEventListeners();
		m_RegisteredForEvents = false;
	}

	public void Update()
	{
		SyncEventListeners();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_11.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_22.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_23.Update();
		logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_SubGraph_Crafting_Tutorial_AttachBlockToBase_29.Update();
		logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_SubGraph_Crafting_Tutorial_AttachBlockToBase_33.Update();
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_39.Update();
		logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_SubGraph_Crafting_Tutorial_AttachBlockToBase_42.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_44.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_52.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_58.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_60.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_62.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_64.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_70.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_86.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_87.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_110.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_116.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_129.Update();
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_136.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_167.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_169.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_171.Update();
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_178.Update();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_185.Update();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_187.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_197.Update();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_201.Update();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_203.Update();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_205.Update();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_207.Update();
		logic_SubGraph_Crafting_Tutorial_Finish_SubGraph_Crafting_Tutorial_Finish_281.Update();
		logic_SubGraph_Crafting_Tutorial_Init_SubGraph_Crafting_Tutorial_Init_287.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_297.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_302.Update();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_316.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_317.Update();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_326.Update();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_329.Update();
	}

	public void OnDestroy()
	{
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_11.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_22.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_23.OnDestroy();
		logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_SubGraph_Crafting_Tutorial_AttachBlockToBase_29.OnDestroy();
		logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_SubGraph_Crafting_Tutorial_AttachBlockToBase_33.OnDestroy();
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_39.OnDestroy();
		logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_SubGraph_Crafting_Tutorial_AttachBlockToBase_42.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_44.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_52.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_58.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_60.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_62.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_64.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_70.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_86.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_87.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_110.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_116.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_129.OnDestroy();
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_136.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_167.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_169.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_171.OnDestroy();
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_178.OnDestroy();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_185.OnDestroy();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_187.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_197.OnDestroy();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_201.OnDestroy();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_203.OnDestroy();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_205.OnDestroy();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_207.OnDestroy();
		logic_SubGraph_Crafting_Tutorial_Finish_SubGraph_Crafting_Tutorial_Finish_281.OnDestroy();
		logic_SubGraph_Crafting_Tutorial_Init_SubGraph_Crafting_Tutorial_Init_287.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_297.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_302.OnDestroy();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_316.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_317.OnDestroy();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_326.OnDestroy();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_329.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_11.Save_Out -= SubGraph_SaveLoadBool_Save_Out_11;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_11.Load_Out -= SubGraph_SaveLoadBool_Load_Out_11;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_11.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_11;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_22.Save_Out -= SubGraph_SaveLoadBool_Save_Out_22;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_22.Load_Out -= SubGraph_SaveLoadBool_Load_Out_22;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_22.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_22;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_23.Save_Out -= SubGraph_SaveLoadBool_Save_Out_23;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_23.Load_Out -= SubGraph_SaveLoadBool_Load_Out_23;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_23.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_23;
		logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_SubGraph_Crafting_Tutorial_AttachBlockToBase_29.Block_Attached -= SubGraph_Crafting_Tutorial_AttachBlockToBase_Block_Attached_29;
		logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_SubGraph_Crafting_Tutorial_AttachBlockToBase_33.Block_Attached -= SubGraph_Crafting_Tutorial_AttachBlockToBase_Block_Attached_33;
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_39.Out -= SubGraph_Crafting_Tutorial_ManageBlock_Out_39;
		logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_SubGraph_Crafting_Tutorial_AttachBlockToBase_42.Block_Attached -= SubGraph_Crafting_Tutorial_AttachBlockToBase_Block_Attached_42;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_44.Save_Out -= SubGraph_SaveLoadBool_Save_Out_44;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_44.Load_Out -= SubGraph_SaveLoadBool_Load_Out_44;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_44.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_44;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_52.Save_Out -= SubGraph_SaveLoadBool_Save_Out_52;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_52.Load_Out -= SubGraph_SaveLoadBool_Load_Out_52;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_52.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_52;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_58.Save_Out -= SubGraph_SaveLoadBool_Save_Out_58;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_58.Load_Out -= SubGraph_SaveLoadBool_Load_Out_58;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_58.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_58;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_60.Save_Out -= SubGraph_SaveLoadBool_Save_Out_60;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_60.Load_Out -= SubGraph_SaveLoadBool_Load_Out_60;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_60.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_60;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_62.Save_Out -= SubGraph_SaveLoadBool_Save_Out_62;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_62.Load_Out -= SubGraph_SaveLoadBool_Load_Out_62;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_62.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_62;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_64.Save_Out -= SubGraph_SaveLoadBool_Save_Out_64;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_64.Load_Out -= SubGraph_SaveLoadBool_Load_Out_64;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_64.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_64;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_70.Save_Out -= SubGraph_SaveLoadBool_Save_Out_70;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_70.Load_Out -= SubGraph_SaveLoadBool_Load_Out_70;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_70.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_70;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_86.Save_Out -= SubGraph_SaveLoadBool_Save_Out_86;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_86.Load_Out -= SubGraph_SaveLoadBool_Load_Out_86;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_86.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_86;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_87.Save_Out -= SubGraph_SaveLoadBool_Save_Out_87;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_87.Load_Out -= SubGraph_SaveLoadBool_Load_Out_87;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_87.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_87;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_110.Save_Out -= SubGraph_SaveLoadBool_Save_Out_110;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_110.Load_Out -= SubGraph_SaveLoadBool_Load_Out_110;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_110.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_110;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_116.Save_Out -= SubGraph_SaveLoadBool_Save_Out_116;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_116.Load_Out -= SubGraph_SaveLoadBool_Load_Out_116;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_116.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_116;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_129.Save_Out -= SubGraph_SaveLoadBool_Save_Out_129;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_129.Load_Out -= SubGraph_SaveLoadBool_Load_Out_129;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_129.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_129;
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_136.Out -= SubGraph_Crafting_Tutorial_ManageBlock_Out_136;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_167.Save_Out -= SubGraph_SaveLoadBool_Save_Out_167;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_167.Load_Out -= SubGraph_SaveLoadBool_Load_Out_167;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_167.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_167;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_169.Save_Out -= SubGraph_SaveLoadBool_Save_Out_169;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_169.Load_Out -= SubGraph_SaveLoadBool_Load_Out_169;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_169.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_169;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_171.Save_Out -= SubGraph_SaveLoadBool_Save_Out_171;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_171.Load_Out -= SubGraph_SaveLoadBool_Load_Out_171;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_171.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_171;
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_178.Out -= SubGraph_Crafting_Tutorial_ManageBlock_Out_178;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_183.Output1 -= uScriptCon_ManualSwitch_Output1_183;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_183.Output2 -= uScriptCon_ManualSwitch_Output2_183;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_183.Output3 -= uScriptCon_ManualSwitch_Output3_183;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_183.Output4 -= uScriptCon_ManualSwitch_Output4_183;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_183.Output5 -= uScriptCon_ManualSwitch_Output5_183;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_183.Output6 -= uScriptCon_ManualSwitch_Output6_183;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_183.Output7 -= uScriptCon_ManualSwitch_Output7_183;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_183.Output8 -= uScriptCon_ManualSwitch_Output8_183;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_185.Save_Out -= SubGraph_SaveLoadInt_Save_Out_185;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_185.Load_Out -= SubGraph_SaveLoadInt_Load_Out_185;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_185.Restart_Out -= SubGraph_SaveLoadInt_Restart_Out_185;
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_187.Out -= SubGraph_LoadObjectiveStates_Out_187;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_197.Save_Out -= SubGraph_SaveLoadBool_Save_Out_197;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_197.Load_Out -= SubGraph_SaveLoadBool_Load_Out_197;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_197.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_197;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_201.Out -= SubGraph_CompleteObjectiveStage_Out_201;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_203.Out -= SubGraph_CompleteObjectiveStage_Out_203;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_205.Out -= SubGraph_CompleteObjectiveStage_Out_205;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_207.Out -= SubGraph_CompleteObjectiveStage_Out_207;
		logic_SubGraph_Crafting_Tutorial_Finish_SubGraph_Crafting_Tutorial_Finish_281.Out -= SubGraph_Crafting_Tutorial_Finish_Out_281;
		logic_SubGraph_Crafting_Tutorial_Init_SubGraph_Crafting_Tutorial_Init_287.Out -= SubGraph_Crafting_Tutorial_Init_Out_287;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_297.Save_Out -= SubGraph_SaveLoadBool_Save_Out_297;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_297.Load_Out -= SubGraph_SaveLoadBool_Load_Out_297;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_297.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_297;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_302.Save_Out -= SubGraph_SaveLoadBool_Save_Out_302;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_302.Load_Out -= SubGraph_SaveLoadBool_Load_Out_302;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_302.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_302;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_316.Out -= SubGraph_CompleteObjectiveStage_Out_316;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_317.Save_Out -= SubGraph_SaveLoadBool_Save_Out_317;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_317.Load_Out -= SubGraph_SaveLoadBool_Load_Out_317;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_317.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_317;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_326.Out -= SubGraph_AddMessageWithPadSupport_Out_326;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_326.Shown -= SubGraph_AddMessageWithPadSupport_Shown_326;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_329.Out -= SubGraph_AddMessageWithPadSupport_Out_329;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_329.Shown -= SubGraph_AddMessageWithPadSupport_Shown_329;
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

	private void SubGraph_SaveLoadBool_Save_Out_11(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_11 = e.boolean;
		local_ResourcesSpawned_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_11;
		Relay_Save_Out_11();
	}

	private void SubGraph_SaveLoadBool_Load_Out_11(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_11 = e.boolean;
		local_ResourcesSpawned_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_11;
		Relay_Load_Out_11();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_11(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_11 = e.boolean;
		local_ResourcesSpawned_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_11;
		Relay_Restart_Out_11();
	}

	private void SubGraph_SaveLoadBool_Save_Out_22(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_22 = e.boolean;
		local_WoodBurnt_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_22;
		Relay_Save_Out_22();
	}

	private void SubGraph_SaveLoadBool_Load_Out_22(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_22 = e.boolean;
		local_WoodBurnt_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_22;
		Relay_Load_Out_22();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_22(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_22 = e.boolean;
		local_WoodBurnt_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_22;
		Relay_Restart_Out_22();
	}

	private void SubGraph_SaveLoadBool_Save_Out_23(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_23 = e.boolean;
		local_ReceiverSpawned_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_23;
		Relay_Save_Out_23();
	}

	private void SubGraph_SaveLoadBool_Load_Out_23(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_23 = e.boolean;
		local_ReceiverSpawned_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_23;
		Relay_Load_Out_23();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_23(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_23 = e.boolean;
		local_ReceiverSpawned_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_23;
		Relay_Restart_Out_23();
	}

	private void SubGraph_Crafting_Tutorial_AttachBlockToBase_Block_Attached_29(object o, SubGraph_Crafting_Tutorial_AttachBlockToBase.LogicEventArgs e)
	{
		Relay_Block_Attached_29();
	}

	private void SubGraph_Crafting_Tutorial_AttachBlockToBase_Block_Attached_33(object o, SubGraph_Crafting_Tutorial_AttachBlockToBase.LogicEventArgs e)
	{
		Relay_Block_Attached_33();
	}

	private void SubGraph_Crafting_Tutorial_ManageBlock_Out_39(object o, SubGraph_Crafting_Tutorial_ManageBlock.LogicEventArgs e)
	{
		logic_SubGraph_Crafting_Tutorial_ManageBlock_block_39 = e.block;
		blockSpawnData = logic_SubGraph_Crafting_Tutorial_ManageBlock_blockSpawnData_39;
		local_FilterBlock_TankBlock = logic_SubGraph_Crafting_Tutorial_ManageBlock_block_39;
		Relay_Out_39();
	}

	private void SubGraph_Crafting_Tutorial_AttachBlockToBase_Block_Attached_42(object o, SubGraph_Crafting_Tutorial_AttachBlockToBase.LogicEventArgs e)
	{
		Relay_Block_Attached_42();
	}

	private void SubGraph_SaveLoadBool_Save_Out_44(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_44 = e.boolean;
		local_GeneratorSpawned_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_44;
		Relay_Save_Out_44();
	}

	private void SubGraph_SaveLoadBool_Load_Out_44(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_44 = e.boolean;
		local_GeneratorSpawned_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_44;
		Relay_Load_Out_44();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_44(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_44 = e.boolean;
		local_GeneratorSpawned_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_44;
		Relay_Restart_Out_44();
	}

	private void SubGraph_SaveLoadBool_Save_Out_52(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_52 = e.boolean;
		local_MsgBaseFoundShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_52;
		Relay_Save_Out_52();
	}

	private void SubGraph_SaveLoadBool_Load_Out_52(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_52 = e.boolean;
		local_MsgBaseFoundShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_52;
		Relay_Load_Out_52();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_52(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_52 = e.boolean;
		local_MsgBaseFoundShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_52;
		Relay_Restart_Out_52();
	}

	private void SubGraph_SaveLoadBool_Save_Out_58(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_58 = e.boolean;
		local_MsgSetFilterShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_58;
		Relay_Save_Out_58();
	}

	private void SubGraph_SaveLoadBool_Load_Out_58(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_58 = e.boolean;
		local_MsgSetFilterShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_58;
		Relay_Load_Out_58();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_58(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_58 = e.boolean;
		local_MsgSetFilterShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_58;
		Relay_Restart_Out_58();
	}

	private void SubGraph_SaveLoadBool_Save_Out_60(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_60 = e.boolean;
		local_MsgAttachGeneratorShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_60;
		Relay_Save_Out_60();
	}

	private void SubGraph_SaveLoadBool_Load_Out_60(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_60 = e.boolean;
		local_MsgAttachGeneratorShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_60;
		Relay_Load_Out_60();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_60(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_60 = e.boolean;
		local_MsgAttachGeneratorShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_60;
		Relay_Restart_Out_60();
	}

	private void SubGraph_SaveLoadBool_Save_Out_62(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_62 = e.boolean;
		local_MsgAttachReceiverShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_62;
		Relay_Save_Out_62();
	}

	private void SubGraph_SaveLoadBool_Load_Out_62(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_62 = e.boolean;
		local_MsgAttachReceiverShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_62;
		Relay_Load_Out_62();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_62(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_62 = e.boolean;
		local_MsgAttachReceiverShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_62;
		Relay_Restart_Out_62();
	}

	private void SubGraph_SaveLoadBool_Save_Out_64(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_64 = e.boolean;
		local_MsgBaseExplantionShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_64;
		Relay_Save_Out_64();
	}

	private void SubGraph_SaveLoadBool_Load_Out_64(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_64 = e.boolean;
		local_MsgBaseExplantionShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_64;
		Relay_Load_Out_64();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_64(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_64 = e.boolean;
		local_MsgBaseExplantionShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_64;
		Relay_Restart_Out_64();
	}

	private void SubGraph_SaveLoadBool_Save_Out_70(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_70 = e.boolean;
		local_FilterSetToFuel_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_70;
		Relay_Save_Out_70();
	}

	private void SubGraph_SaveLoadBool_Load_Out_70(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_70 = e.boolean;
		local_FilterSetToFuel_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_70;
		Relay_Load_Out_70();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_70(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_70 = e.boolean;
		local_FilterSetToFuel_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_70;
		Relay_Restart_Out_70();
	}

	private void SubGraph_SaveLoadBool_Save_Out_86(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_86 = e.boolean;
		local_FinalChunksSpawned_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_86;
		Relay_Save_Out_86();
	}

	private void SubGraph_SaveLoadBool_Load_Out_86(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_86 = e.boolean;
		local_FinalChunksSpawned_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_86;
		Relay_Load_Out_86();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_86(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_86 = e.boolean;
		local_FinalChunksSpawned_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_86;
		Relay_Restart_Out_86();
	}

	private void SubGraph_SaveLoadBool_Save_Out_87(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_87 = e.boolean;
		local_FinalResourcesSpawned_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_87;
		Relay_Save_Out_87();
	}

	private void SubGraph_SaveLoadBool_Load_Out_87(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_87 = e.boolean;
		local_FinalResourcesSpawned_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_87;
		Relay_Load_Out_87();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_87(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_87 = e.boolean;
		local_FinalResourcesSpawned_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_87;
		Relay_Restart_Out_87();
	}

	private void SubGraph_SaveLoadBool_Save_Out_110(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_110 = e.boolean;
		local_MsgSeeGeneratorWorkingShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_110;
		Relay_Save_Out_110();
	}

	private void SubGraph_SaveLoadBool_Load_Out_110(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_110 = e.boolean;
		local_MsgSeeGeneratorWorkingShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_110;
		Relay_Load_Out_110();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_110(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_110 = e.boolean;
		local_MsgSeeGeneratorWorkingShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_110;
		Relay_Restart_Out_110();
	}

	private void SubGraph_SaveLoadBool_Save_Out_116(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_116 = e.boolean;
		local_MsgFilterTrainedShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_116;
		Relay_Save_Out_116();
	}

	private void SubGraph_SaveLoadBool_Load_Out_116(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_116 = e.boolean;
		local_MsgFilterTrainedShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_116;
		Relay_Load_Out_116();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_116(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_116 = e.boolean;
		local_MsgFilterTrainedShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_116;
		Relay_Restart_Out_116();
	}

	private void SubGraph_SaveLoadBool_Save_Out_129(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_129 = e.boolean;
		local_MsgWoodSpawned_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_129;
		Relay_Save_Out_129();
	}

	private void SubGraph_SaveLoadBool_Load_Out_129(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_129 = e.boolean;
		local_MsgWoodSpawned_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_129;
		Relay_Load_Out_129();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_129(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_129 = e.boolean;
		local_MsgWoodSpawned_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_129;
		Relay_Restart_Out_129();
	}

	private void SubGraph_Crafting_Tutorial_ManageBlock_Out_136(object o, SubGraph_Crafting_Tutorial_ManageBlock.LogicEventArgs e)
	{
		logic_SubGraph_Crafting_Tutorial_ManageBlock_block_136 = e.block;
		blockSpawnData = logic_SubGraph_Crafting_Tutorial_ManageBlock_blockSpawnData_136;
		local_ReceiverBlock_TankBlock = logic_SubGraph_Crafting_Tutorial_ManageBlock_block_136;
		Relay_Out_136();
	}

	private void SubGraph_SaveLoadBool_Save_Out_167(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_167 = e.boolean;
		local_ShieldPowered_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_167;
		Relay_Save_Out_167();
	}

	private void SubGraph_SaveLoadBool_Load_Out_167(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_167 = e.boolean;
		local_ShieldPowered_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_167;
		Relay_Load_Out_167();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_167(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_167 = e.boolean;
		local_ShieldPowered_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_167;
		Relay_Restart_Out_167();
	}

	private void SubGraph_SaveLoadBool_Save_Out_169(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_169 = e.boolean;
		local_msgReceiverAttachedShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_169;
		Relay_Save_Out_169();
	}

	private void SubGraph_SaveLoadBool_Load_Out_169(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_169 = e.boolean;
		local_msgReceiverAttachedShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_169;
		Relay_Load_Out_169();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_169(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_169 = e.boolean;
		local_msgReceiverAttachedShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_169;
		Relay_Restart_Out_169();
	}

	private void SubGraph_SaveLoadBool_Save_Out_171(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_171 = e.boolean;
		local_msgFilterSetToFuelShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_171;
		Relay_Save_Out_171();
	}

	private void SubGraph_SaveLoadBool_Load_Out_171(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_171 = e.boolean;
		local_msgFilterSetToFuelShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_171;
		Relay_Load_Out_171();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_171(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_171 = e.boolean;
		local_msgFilterSetToFuelShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_171;
		Relay_Restart_Out_171();
	}

	private void SubGraph_Crafting_Tutorial_ManageBlock_Out_178(object o, SubGraph_Crafting_Tutorial_ManageBlock.LogicEventArgs e)
	{
		logic_SubGraph_Crafting_Tutorial_ManageBlock_block_178 = e.block;
		blockSpawnDataGenerator = logic_SubGraph_Crafting_Tutorial_ManageBlock_blockSpawnData_178;
		local_GeneratorBlock_TankBlock = logic_SubGraph_Crafting_Tutorial_ManageBlock_block_178;
		Relay_Out_178();
	}

	private void uScriptCon_ManualSwitch_Output1_183(object o, EventArgs e)
	{
		Relay_Output1_183();
	}

	private void uScriptCon_ManualSwitch_Output2_183(object o, EventArgs e)
	{
		Relay_Output2_183();
	}

	private void uScriptCon_ManualSwitch_Output3_183(object o, EventArgs e)
	{
		Relay_Output3_183();
	}

	private void uScriptCon_ManualSwitch_Output4_183(object o, EventArgs e)
	{
		Relay_Output4_183();
	}

	private void uScriptCon_ManualSwitch_Output5_183(object o, EventArgs e)
	{
		Relay_Output5_183();
	}

	private void uScriptCon_ManualSwitch_Output6_183(object o, EventArgs e)
	{
		Relay_Output6_183();
	}

	private void uScriptCon_ManualSwitch_Output7_183(object o, EventArgs e)
	{
		Relay_Output7_183();
	}

	private void uScriptCon_ManualSwitch_Output8_183(object o, EventArgs e)
	{
		Relay_Output8_183();
	}

	private void SubGraph_SaveLoadInt_Save_Out_185(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_185 = e.integer;
		local_Stage_System_Int32 = logic_SubGraph_SaveLoadInt_integer_185;
		Relay_Save_Out_185();
	}

	private void SubGraph_SaveLoadInt_Load_Out_185(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_185 = e.integer;
		local_Stage_System_Int32 = logic_SubGraph_SaveLoadInt_integer_185;
		Relay_Load_Out_185();
	}

	private void SubGraph_SaveLoadInt_Restart_Out_185(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_185 = e.integer;
		local_Stage_System_Int32 = logic_SubGraph_SaveLoadInt_integer_185;
		Relay_Restart_Out_185();
	}

	private void SubGraph_LoadObjectiveStates_Out_187(object o, SubGraph_LoadObjectiveStates.LogicEventArgs e)
	{
		Relay_Out_187();
	}

	private void SubGraph_SaveLoadBool_Save_Out_197(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_197 = e.boolean;
		local_AllowFilterInteraction_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_197;
		Relay_Save_Out_197();
	}

	private void SubGraph_SaveLoadBool_Load_Out_197(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_197 = e.boolean;
		local_AllowFilterInteraction_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_197;
		Relay_Load_Out_197();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_197(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_197 = e.boolean;
		local_AllowFilterInteraction_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_197;
		Relay_Restart_Out_197();
	}

	private void SubGraph_CompleteObjectiveStage_Out_201(object o, SubGraph_CompleteObjectiveStage.LogicEventArgs e)
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_201 = e.objectiveStage;
		local_Stage_System_Int32 = logic_SubGraph_CompleteObjectiveStage_objectiveStage_201;
		Relay_Out_201();
	}

	private void SubGraph_CompleteObjectiveStage_Out_203(object o, SubGraph_CompleteObjectiveStage.LogicEventArgs e)
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_203 = e.objectiveStage;
		local_Stage_System_Int32 = logic_SubGraph_CompleteObjectiveStage_objectiveStage_203;
		Relay_Out_203();
	}

	private void SubGraph_CompleteObjectiveStage_Out_205(object o, SubGraph_CompleteObjectiveStage.LogicEventArgs e)
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_205 = e.objectiveStage;
		local_Stage_System_Int32 = logic_SubGraph_CompleteObjectiveStage_objectiveStage_205;
		Relay_Out_205();
	}

	private void SubGraph_CompleteObjectiveStage_Out_207(object o, SubGraph_CompleteObjectiveStage.LogicEventArgs e)
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_207 = e.objectiveStage;
		local_Stage_System_Int32 = logic_SubGraph_CompleteObjectiveStage_objectiveStage_207;
		Relay_Out_207();
	}

	private void SubGraph_Crafting_Tutorial_Finish_Out_281(object o, SubGraph_Crafting_Tutorial_Finish.LogicEventArgs e)
	{
		Relay_Out_281();
	}

	private void SubGraph_Crafting_Tutorial_Init_Out_287(object o, SubGraph_Crafting_Tutorial_Init.LogicEventArgs e)
	{
		logic_SubGraph_Crafting_Tutorial_Init_CraftingBaseTech_287 = e.CraftingBaseTech;
		logic_SubGraph_Crafting_Tutorial_Init_NPCTech_287 = e.NPCTech;
		local_CraftingBaseTech_Tank = logic_SubGraph_Crafting_Tutorial_Init_CraftingBaseTech_287;
		local_NPCTech_Tank = logic_SubGraph_Crafting_Tutorial_Init_NPCTech_287;
		Relay_Out_287();
	}

	private void SubGraph_SaveLoadBool_Save_Out_297(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_297 = e.boolean;
		local_BatteriesDrained_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_297;
		Relay_Save_Out_297();
	}

	private void SubGraph_SaveLoadBool_Load_Out_297(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_297 = e.boolean;
		local_BatteriesDrained_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_297;
		Relay_Load_Out_297();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_297(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_297 = e.boolean;
		local_BatteriesDrained_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_297;
		Relay_Restart_Out_297();
	}

	private void SubGraph_SaveLoadBool_Save_Out_302(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_302 = e.boolean;
		local_msgIntroShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_302;
		Relay_Save_Out_302();
	}

	private void SubGraph_SaveLoadBool_Load_Out_302(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_302 = e.boolean;
		local_msgIntroShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_302;
		Relay_Load_Out_302();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_302(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_302 = e.boolean;
		local_msgIntroShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_302;
		Relay_Restart_Out_302();
	}

	private void SubGraph_CompleteObjectiveStage_Out_316(object o, SubGraph_CompleteObjectiveStage.LogicEventArgs e)
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_316 = e.objectiveStage;
		local_Stage_System_Int32 = logic_SubGraph_CompleteObjectiveStage_objectiveStage_316;
		Relay_Out_316();
	}

	private void SubGraph_SaveLoadBool_Save_Out_317(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_317 = e.boolean;
		local_GeneratorAttached_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_317;
		Relay_Save_Out_317();
	}

	private void SubGraph_SaveLoadBool_Load_Out_317(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_317 = e.boolean;
		local_GeneratorAttached_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_317;
		Relay_Load_Out_317();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_317(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_317 = e.boolean;
		local_GeneratorAttached_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_317;
		Relay_Restart_Out_317();
	}

	private void SubGraph_AddMessageWithPadSupport_Out_326(object o, SubGraph_AddMessageWithPadSupport.LogicEventArgs e)
	{
		logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboardReturn_326 = e.messageMouseKeyboardReturn;
		logic_SubGraph_AddMessageWithPadSupport_messageControlPadReturn_326 = e.messageControlPadReturn;
		Relay_Out_326();
	}

	private void SubGraph_AddMessageWithPadSupport_Shown_326(object o, SubGraph_AddMessageWithPadSupport.LogicEventArgs e)
	{
		logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboardReturn_326 = e.messageMouseKeyboardReturn;
		logic_SubGraph_AddMessageWithPadSupport_messageControlPadReturn_326 = e.messageControlPadReturn;
		Relay_Shown_326();
	}

	private void SubGraph_AddMessageWithPadSupport_Out_329(object o, SubGraph_AddMessageWithPadSupport.LogicEventArgs e)
	{
		logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboardReturn_329 = e.messageMouseKeyboardReturn;
		logic_SubGraph_AddMessageWithPadSupport_messageControlPadReturn_329 = e.messageControlPadReturn;
		Relay_Out_329();
	}

	private void SubGraph_AddMessageWithPadSupport_Shown_329(object o, SubGraph_AddMessageWithPadSupport.LogicEventArgs e)
	{
		logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboardReturn_329 = e.messageMouseKeyboardReturn;
		logic_SubGraph_AddMessageWithPadSupport_messageControlPadReturn_329 = e.messageControlPadReturn;
		Relay_Shown_329();
	}

	private void Relay_OnUpdate_0()
	{
		Relay_In_287();
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
			Relay_True_164();
		}
	}

	private void Relay_UnPause_3()
	{
		logic_uScript_PausePopulation_uScript_PausePopulation_3.UnPause();
		if (logic_uScript_PausePopulation_uScript_PausePopulation_3.Out)
		{
			Relay_True_164();
		}
	}

	private void Relay_Pause_4()
	{
		logic_uScript_PausePopulation_uScript_PausePopulation_4.Pause();
		if (logic_uScript_PausePopulation_uScript_PausePopulation_4.Out)
		{
			Relay_In_165();
		}
	}

	private void Relay_UnPause_4()
	{
		logic_uScript_PausePopulation_uScript_PausePopulation_4.UnPause();
		if (logic_uScript_PausePopulation_uScript_PausePopulation_4.Out)
		{
			Relay_In_165();
		}
	}

	private void Relay_In_7()
	{
		logic_uScriptCon_CompareBool_Bool_7 = local_ResourcesSpawned_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_7.In(logic_uScriptCon_CompareBool_Bool_7);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_7.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_7.False;
		if (num)
		{
			Relay_In_14();
		}
		if (flag)
		{
			Relay_True_8();
		}
	}

	private void Relay_True_8()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_8.True(out logic_uScriptAct_SetBool_Target_8);
		local_ResourcesSpawned_System_Boolean = logic_uScriptAct_SetBool_Target_8;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_8.Out)
		{
			Relay_In_303();
		}
	}

	private void Relay_False_8()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_8.False(out logic_uScriptAct_SetBool_Target_8);
		local_ResourcesSpawned_System_Boolean = logic_uScriptAct_SetBool_Target_8;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_8.Out)
		{
			Relay_In_303();
		}
	}

	private void Relay_SaveEvent_9()
	{
		Relay_Save_185();
	}

	private void Relay_LoadEvent_9()
	{
		Relay_Load_185();
	}

	private void Relay_RestartEvent_9()
	{
		Relay_Restart_185();
	}

	private void Relay_Save_Out_11()
	{
		Relay_Save_23();
	}

	private void Relay_Load_Out_11()
	{
		Relay_Load_23();
	}

	private void Relay_Restart_Out_11()
	{
		Relay_Set_False_23();
	}

	private void Relay_Save_11()
	{
		logic_SubGraph_SaveLoadBool_boolean_11 = local_ResourcesSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_11 = local_ResourcesSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_11.Save(ref logic_SubGraph_SaveLoadBool_boolean_11, logic_SubGraph_SaveLoadBool_boolAsVariable_11, logic_SubGraph_SaveLoadBool_uniqueID_11);
	}

	private void Relay_Load_11()
	{
		logic_SubGraph_SaveLoadBool_boolean_11 = local_ResourcesSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_11 = local_ResourcesSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_11.Load(ref logic_SubGraph_SaveLoadBool_boolean_11, logic_SubGraph_SaveLoadBool_boolAsVariable_11, logic_SubGraph_SaveLoadBool_uniqueID_11);
	}

	private void Relay_Set_True_11()
	{
		logic_SubGraph_SaveLoadBool_boolean_11 = local_ResourcesSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_11 = local_ResourcesSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_11.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_11, logic_SubGraph_SaveLoadBool_boolAsVariable_11, logic_SubGraph_SaveLoadBool_uniqueID_11);
	}

	private void Relay_Set_False_11()
	{
		logic_SubGraph_SaveLoadBool_boolean_11 = local_ResourcesSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_11 = local_ResourcesSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_11.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_11, logic_SubGraph_SaveLoadBool_boolAsVariable_11, logic_SubGraph_SaveLoadBool_uniqueID_11);
	}

	private void Relay_True_13()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_13.True(out logic_uScriptAct_SetBool_Target_13);
		local_WoodBurnt_System_Boolean = logic_uScriptAct_SetBool_Target_13;
	}

	private void Relay_False_13()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_13.False(out logic_uScriptAct_SetBool_Target_13);
		local_WoodBurnt_System_Boolean = logic_uScriptAct_SetBool_Target_13;
	}

	private void Relay_In_14()
	{
		logic_uScript_Wait_seconds_14 = timeWaitForResourcesInSilo;
		logic_uScript_Wait_uScript_Wait_14.In(logic_uScript_Wait_seconds_14, logic_uScript_Wait_repeat_14);
		if (logic_uScript_Wait_uScript_Wait_14.Waited)
		{
			Relay_In_103();
		}
	}

	private void Relay_In_17()
	{
		logic_uScriptCon_CompareBool_Bool_17 = local_WoodBurnt_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_17.In(logic_uScriptCon_CompareBool_Bool_17);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_17.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_17.False;
		if (num)
		{
			Relay_In_65();
		}
		if (flag)
		{
			Relay_In_149();
		}
	}

	private void Relay_Save_Out_22()
	{
		Relay_Save_44();
	}

	private void Relay_Load_Out_22()
	{
		Relay_Load_44();
	}

	private void Relay_Restart_Out_22()
	{
		Relay_Set_False_44();
	}

	private void Relay_Save_22()
	{
		logic_SubGraph_SaveLoadBool_boolean_22 = local_WoodBurnt_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_22 = local_WoodBurnt_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_22.Save(ref logic_SubGraph_SaveLoadBool_boolean_22, logic_SubGraph_SaveLoadBool_boolAsVariable_22, logic_SubGraph_SaveLoadBool_uniqueID_22);
	}

	private void Relay_Load_22()
	{
		logic_SubGraph_SaveLoadBool_boolean_22 = local_WoodBurnt_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_22 = local_WoodBurnt_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_22.Load(ref logic_SubGraph_SaveLoadBool_boolean_22, logic_SubGraph_SaveLoadBool_boolAsVariable_22, logic_SubGraph_SaveLoadBool_uniqueID_22);
	}

	private void Relay_Set_True_22()
	{
		logic_SubGraph_SaveLoadBool_boolean_22 = local_WoodBurnt_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_22 = local_WoodBurnt_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_22.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_22, logic_SubGraph_SaveLoadBool_boolAsVariable_22, logic_SubGraph_SaveLoadBool_uniqueID_22);
	}

	private void Relay_Set_False_22()
	{
		logic_SubGraph_SaveLoadBool_boolean_22 = local_WoodBurnt_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_22 = local_WoodBurnt_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_22.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_22, logic_SubGraph_SaveLoadBool_boolAsVariable_22, logic_SubGraph_SaveLoadBool_uniqueID_22);
	}

	private void Relay_Save_Out_23()
	{
		Relay_Save_22();
	}

	private void Relay_Load_Out_23()
	{
		Relay_Load_22();
	}

	private void Relay_Restart_Out_23()
	{
		Relay_Set_False_22();
	}

	private void Relay_Save_23()
	{
		logic_SubGraph_SaveLoadBool_boolean_23 = local_ReceiverSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_23 = local_ReceiverSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_23.Save(ref logic_SubGraph_SaveLoadBool_boolean_23, logic_SubGraph_SaveLoadBool_boolAsVariable_23, logic_SubGraph_SaveLoadBool_uniqueID_23);
	}

	private void Relay_Load_23()
	{
		logic_SubGraph_SaveLoadBool_boolean_23 = local_ReceiverSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_23 = local_ReceiverSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_23.Load(ref logic_SubGraph_SaveLoadBool_boolean_23, logic_SubGraph_SaveLoadBool_boolAsVariable_23, logic_SubGraph_SaveLoadBool_uniqueID_23);
	}

	private void Relay_Set_True_23()
	{
		logic_SubGraph_SaveLoadBool_boolean_23 = local_ReceiverSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_23 = local_ReceiverSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_23.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_23, logic_SubGraph_SaveLoadBool_boolAsVariable_23, logic_SubGraph_SaveLoadBool_uniqueID_23);
	}

	private void Relay_Set_False_23()
	{
		logic_SubGraph_SaveLoadBool_boolean_23 = local_ReceiverSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_23 = local_ReceiverSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_23.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_23, logic_SubGraph_SaveLoadBool_boolAsVariable_23, logic_SubGraph_SaveLoadBool_uniqueID_23);
	}

	private void Relay_In_27()
	{
		logic_uScript_IsFilterAcceptingSpecificItem_filterBlock_27 = local_FilterBlock_TankBlock;
		logic_uScript_IsFilterAcceptingSpecificItem_itemType_27 = ResourceTypeToFilterWood;
		logic_uScript_IsFilterAcceptingSpecificItem_uScript_IsFilterAcceptingSpecificItem_27.In(logic_uScript_IsFilterAcceptingSpecificItem_filterBlock_27, logic_uScript_IsFilterAcceptingSpecificItem_itemType_27);
		if (logic_uScript_IsFilterAcceptingSpecificItem_uScript_IsFilterAcceptingSpecificItem_27.True)
		{
			Relay_False_192();
		}
	}

	private void Relay_In_28()
	{
		logic_uScript_HideArrow_uScript_HideArrow_28.In();
		if (logic_uScript_HideArrow_uScript_HideArrow_28.Out)
		{
			Relay_In_335();
		}
	}

	private void Relay_Block_Attached_29()
	{
		Relay_In_203();
	}

	private void Relay_In_29()
	{
		logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_Block_29 = local_FilterBlock_TankBlock;
		logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_CraftingBaseTech_29 = local_CraftingBaseTech_Tank;
		int num = 0;
		Array array = ghostBlockFilter;
		if (logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_ghostBlockData_29.Length != num + array.Length)
		{
			Array.Resize(ref logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_ghostBlockData_29, num + array.Length);
		}
		Array.Copy(array, 0, logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_ghostBlockData_29, num, array.Length);
		num += array.Length;
		logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_SubGraph_Crafting_Tutorial_AttachBlockToBase_29.In(logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_Block_29, logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_CraftingBaseTech_29, logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_ghostBlockData_29, logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_BlockType_29, logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_BlockPosition_29);
	}

	private void Relay_Block_Attached_33()
	{
		Relay_In_157();
	}

	private void Relay_In_33()
	{
		logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_Block_33 = local_ReceiverBlock_TankBlock;
		logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_CraftingBaseTech_33 = local_CraftingBaseTech_Tank;
		int num = 0;
		Array array = ghostBlockReceiver;
		if (logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_ghostBlockData_33.Length != num + array.Length)
		{
			Array.Resize(ref logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_ghostBlockData_33, num + array.Length);
		}
		Array.Copy(array, 0, logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_ghostBlockData_33, num, array.Length);
		num += array.Length;
		logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_SubGraph_Crafting_Tutorial_AttachBlockToBase_33.In(logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_Block_33, logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_CraftingBaseTech_33, logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_ghostBlockData_33, logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_BlockType_33, logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_BlockPosition_33);
	}

	private void Relay_In_36()
	{
		logic_uScript_PointArrowAtVisible_targetObject_36 = local_FilterBlock_TankBlock;
		logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_36.In(logic_uScript_PointArrowAtVisible_targetObject_36, logic_uScript_PointArrowAtVisible_timeToShowFor_36, logic_uScript_PointArrowAtVisible_offset_36);
		if (logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_36.Out)
		{
			Relay_In_332();
		}
	}

	private void Relay_Out_39()
	{
		Relay_In_136();
	}

	private void Relay_In_39()
	{
		int num = 0;
		Array array = blockSpawnData;
		if (logic_SubGraph_Crafting_Tutorial_ManageBlock_blockSpawnData_39.Length != num + array.Length)
		{
			Array.Resize(ref logic_SubGraph_Crafting_Tutorial_ManageBlock_blockSpawnData_39, num + array.Length);
		}
		Array.Copy(array, 0, logic_SubGraph_Crafting_Tutorial_ManageBlock_blockSpawnData_39, num, array.Length);
		num += array.Length;
		logic_SubGraph_Crafting_Tutorial_ManageBlock_block_39 = local_FilterBlock_TankBlock;
		logic_SubGraph_Crafting_Tutorial_ManageBlock_msgBlockOutsideArea_39 = msgBlockOutsideArea;
		logic_SubGraph_Crafting_Tutorial_ManageBlock_messageSpeaker_39 = messageSpeaker;
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_39.In(ref logic_SubGraph_Crafting_Tutorial_ManageBlock_blockSpawnData_39, logic_SubGraph_Crafting_Tutorial_ManageBlock_blockIndex_39, ref logic_SubGraph_Crafting_Tutorial_ManageBlock_block_39, logic_SubGraph_Crafting_Tutorial_ManageBlock_allowAnchoring_39, logic_SubGraph_Crafting_Tutorial_ManageBlock_msgBlockOutsideArea_39, logic_SubGraph_Crafting_Tutorial_ManageBlock_messageSpeaker_39);
	}

	private void Relay_Block_Attached_42()
	{
		Relay_In_156();
	}

	private void Relay_In_42()
	{
		logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_Block_42 = local_GeneratorBlock_TankBlock;
		logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_CraftingBaseTech_42 = local_CraftingBaseTech_Tank;
		int num = 0;
		Array array = ghostBlockGenerator;
		if (logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_ghostBlockData_42.Length != num + array.Length)
		{
			Array.Resize(ref logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_ghostBlockData_42, num + array.Length);
		}
		Array.Copy(array, 0, logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_ghostBlockData_42, num, array.Length);
		num += array.Length;
		logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_SubGraph_Crafting_Tutorial_AttachBlockToBase_42.In(logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_Block_42, logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_CraftingBaseTech_42, logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_ghostBlockData_42, logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_BlockType_42, logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_BlockPosition_42);
	}

	private void Relay_Save_Out_44()
	{
		Relay_Save_302();
	}

	private void Relay_Load_Out_44()
	{
		Relay_Load_302();
	}

	private void Relay_Restart_Out_44()
	{
		Relay_Set_False_302();
	}

	private void Relay_Save_44()
	{
		logic_SubGraph_SaveLoadBool_boolean_44 = local_GeneratorSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_44 = local_GeneratorSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_44.Save(ref logic_SubGraph_SaveLoadBool_boolean_44, logic_SubGraph_SaveLoadBool_boolAsVariable_44, logic_SubGraph_SaveLoadBool_uniqueID_44);
	}

	private void Relay_Load_44()
	{
		logic_SubGraph_SaveLoadBool_boolean_44 = local_GeneratorSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_44 = local_GeneratorSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_44.Load(ref logic_SubGraph_SaveLoadBool_boolean_44, logic_SubGraph_SaveLoadBool_boolAsVariable_44, logic_SubGraph_SaveLoadBool_uniqueID_44);
	}

	private void Relay_Set_True_44()
	{
		logic_SubGraph_SaveLoadBool_boolean_44 = local_GeneratorSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_44 = local_GeneratorSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_44.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_44, logic_SubGraph_SaveLoadBool_boolAsVariable_44, logic_SubGraph_SaveLoadBool_uniqueID_44);
	}

	private void Relay_Set_False_44()
	{
		logic_SubGraph_SaveLoadBool_boolean_44 = local_GeneratorSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_44 = local_GeneratorSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_44.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_44, logic_SubGraph_SaveLoadBool_boolAsVariable_44, logic_SubGraph_SaveLoadBool_uniqueID_44);
	}

	private void Relay_In_45()
	{
		logic_uScript_LockTechStacks_tech_45 = local_CraftingBaseTech_Tank;
		logic_uScript_LockTechStacks_uScript_LockTechStacks_45.In(logic_uScript_LockTechStacks_tech_45);
		if (logic_uScript_LockTechStacks_uScript_LockTechStacks_45.Out)
		{
			Relay_In_28();
		}
	}

	private void Relay_In_48()
	{
		logic_uScriptCon_CompareBool_Bool_48 = local_MsgBaseFoundShown_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_48.In(logic_uScriptCon_CompareBool_Bool_48);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_48.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_48.False;
		if (num)
		{
			Relay_In_215();
		}
		if (flag)
		{
			Relay_In_211();
		}
	}

	private void Relay_True_49()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_49.True(out logic_uScriptAct_SetBool_Target_49);
		local_MsgBaseFoundShown_System_Boolean = logic_uScriptAct_SetBool_Target_49;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_49.Out)
		{
			Relay_In_29();
		}
	}

	private void Relay_False_49()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_49.False(out logic_uScriptAct_SetBool_Target_49);
		local_MsgBaseFoundShown_System_Boolean = logic_uScriptAct_SetBool_Target_49;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_49.Out)
		{
			Relay_In_29();
		}
	}

	private void Relay_Save_Out_52()
	{
		Relay_Save_58();
	}

	private void Relay_Load_Out_52()
	{
		Relay_Load_58();
	}

	private void Relay_Restart_Out_52()
	{
		Relay_Set_False_58();
	}

	private void Relay_Save_52()
	{
		logic_SubGraph_SaveLoadBool_boolean_52 = local_MsgBaseFoundShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_52 = local_MsgBaseFoundShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_52.Save(ref logic_SubGraph_SaveLoadBool_boolean_52, logic_SubGraph_SaveLoadBool_boolAsVariable_52, logic_SubGraph_SaveLoadBool_uniqueID_52);
	}

	private void Relay_Load_52()
	{
		logic_SubGraph_SaveLoadBool_boolean_52 = local_MsgBaseFoundShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_52 = local_MsgBaseFoundShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_52.Load(ref logic_SubGraph_SaveLoadBool_boolean_52, logic_SubGraph_SaveLoadBool_boolAsVariable_52, logic_SubGraph_SaveLoadBool_uniqueID_52);
	}

	private void Relay_Set_True_52()
	{
		logic_SubGraph_SaveLoadBool_boolean_52 = local_MsgBaseFoundShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_52 = local_MsgBaseFoundShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_52.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_52, logic_SubGraph_SaveLoadBool_boolAsVariable_52, logic_SubGraph_SaveLoadBool_uniqueID_52);
	}

	private void Relay_Set_False_52()
	{
		logic_SubGraph_SaveLoadBool_boolean_52 = local_MsgBaseFoundShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_52 = local_MsgBaseFoundShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_52.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_52, logic_SubGraph_SaveLoadBool_boolAsVariable_52, logic_SubGraph_SaveLoadBool_uniqueID_52);
	}

	private void Relay_True_53()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_53.True(out logic_uScriptAct_SetBool_Target_53);
		local_MsgSetFilterShown_System_Boolean = logic_uScriptAct_SetBool_Target_53;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_53.Out)
		{
			Relay_In_126();
		}
	}

	private void Relay_False_53()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_53.False(out logic_uScriptAct_SetBool_Target_53);
		local_MsgSetFilterShown_System_Boolean = logic_uScriptAct_SetBool_Target_53;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_53.Out)
		{
			Relay_In_126();
		}
	}

	private void Relay_In_55()
	{
		logic_uScriptCon_CompareBool_Bool_55 = local_MsgSetFilterShown_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_55.In(logic_uScriptCon_CompareBool_Bool_55);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_55.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_55.False;
		if (num)
		{
			Relay_In_326();
		}
		if (flag)
		{
			Relay_In_218();
		}
	}

	private void Relay_Save_Out_58()
	{
		Relay_Save_60();
	}

	private void Relay_Load_Out_58()
	{
		Relay_Load_60();
	}

	private void Relay_Restart_Out_58()
	{
		Relay_Set_False_60();
	}

	private void Relay_Save_58()
	{
		logic_SubGraph_SaveLoadBool_boolean_58 = local_MsgSetFilterShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_58 = local_MsgSetFilterShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_58.Save(ref logic_SubGraph_SaveLoadBool_boolean_58, logic_SubGraph_SaveLoadBool_boolAsVariable_58, logic_SubGraph_SaveLoadBool_uniqueID_58);
	}

	private void Relay_Load_58()
	{
		logic_SubGraph_SaveLoadBool_boolean_58 = local_MsgSetFilterShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_58 = local_MsgSetFilterShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_58.Load(ref logic_SubGraph_SaveLoadBool_boolean_58, logic_SubGraph_SaveLoadBool_boolAsVariable_58, logic_SubGraph_SaveLoadBool_uniqueID_58);
	}

	private void Relay_Set_True_58()
	{
		logic_SubGraph_SaveLoadBool_boolean_58 = local_MsgSetFilterShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_58 = local_MsgSetFilterShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_58.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_58, logic_SubGraph_SaveLoadBool_boolAsVariable_58, logic_SubGraph_SaveLoadBool_uniqueID_58);
	}

	private void Relay_Set_False_58()
	{
		logic_SubGraph_SaveLoadBool_boolean_58 = local_MsgSetFilterShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_58 = local_MsgSetFilterShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_58.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_58, logic_SubGraph_SaveLoadBool_boolAsVariable_58, logic_SubGraph_SaveLoadBool_uniqueID_58);
	}

	private void Relay_Save_Out_60()
	{
		Relay_Save_62();
	}

	private void Relay_Load_Out_60()
	{
		Relay_Load_62();
	}

	private void Relay_Restart_Out_60()
	{
		Relay_Set_False_62();
	}

	private void Relay_Save_60()
	{
		logic_SubGraph_SaveLoadBool_boolean_60 = local_MsgAttachGeneratorShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_60 = local_MsgAttachGeneratorShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_60.Save(ref logic_SubGraph_SaveLoadBool_boolean_60, logic_SubGraph_SaveLoadBool_boolAsVariable_60, logic_SubGraph_SaveLoadBool_uniqueID_60);
	}

	private void Relay_Load_60()
	{
		logic_SubGraph_SaveLoadBool_boolean_60 = local_MsgAttachGeneratorShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_60 = local_MsgAttachGeneratorShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_60.Load(ref logic_SubGraph_SaveLoadBool_boolean_60, logic_SubGraph_SaveLoadBool_boolAsVariable_60, logic_SubGraph_SaveLoadBool_uniqueID_60);
	}

	private void Relay_Set_True_60()
	{
		logic_SubGraph_SaveLoadBool_boolean_60 = local_MsgAttachGeneratorShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_60 = local_MsgAttachGeneratorShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_60.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_60, logic_SubGraph_SaveLoadBool_boolAsVariable_60, logic_SubGraph_SaveLoadBool_uniqueID_60);
	}

	private void Relay_Set_False_60()
	{
		logic_SubGraph_SaveLoadBool_boolean_60 = local_MsgAttachGeneratorShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_60 = local_MsgAttachGeneratorShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_60.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_60, logic_SubGraph_SaveLoadBool_boolAsVariable_60, logic_SubGraph_SaveLoadBool_uniqueID_60);
	}

	private void Relay_Save_Out_62()
	{
		Relay_Save_64();
	}

	private void Relay_Load_Out_62()
	{
		Relay_Load_64();
	}

	private void Relay_Restart_Out_62()
	{
		Relay_Set_False_64();
	}

	private void Relay_Save_62()
	{
		logic_SubGraph_SaveLoadBool_boolean_62 = local_MsgAttachReceiverShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_62 = local_MsgAttachReceiverShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_62.Save(ref logic_SubGraph_SaveLoadBool_boolean_62, logic_SubGraph_SaveLoadBool_boolAsVariable_62, logic_SubGraph_SaveLoadBool_uniqueID_62);
	}

	private void Relay_Load_62()
	{
		logic_SubGraph_SaveLoadBool_boolean_62 = local_MsgAttachReceiverShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_62 = local_MsgAttachReceiverShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_62.Load(ref logic_SubGraph_SaveLoadBool_boolean_62, logic_SubGraph_SaveLoadBool_boolAsVariable_62, logic_SubGraph_SaveLoadBool_uniqueID_62);
	}

	private void Relay_Set_True_62()
	{
		logic_SubGraph_SaveLoadBool_boolean_62 = local_MsgAttachReceiverShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_62 = local_MsgAttachReceiverShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_62.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_62, logic_SubGraph_SaveLoadBool_boolAsVariable_62, logic_SubGraph_SaveLoadBool_uniqueID_62);
	}

	private void Relay_Set_False_62()
	{
		logic_SubGraph_SaveLoadBool_boolean_62 = local_MsgAttachReceiverShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_62 = local_MsgAttachReceiverShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_62.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_62, logic_SubGraph_SaveLoadBool_boolAsVariable_62, logic_SubGraph_SaveLoadBool_uniqueID_62);
	}

	private void Relay_Save_Out_64()
	{
		Relay_Save_70();
	}

	private void Relay_Load_Out_64()
	{
		Relay_Load_70();
	}

	private void Relay_Restart_Out_64()
	{
		Relay_Set_False_70();
	}

	private void Relay_Save_64()
	{
		logic_SubGraph_SaveLoadBool_boolean_64 = local_MsgBaseExplantionShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_64 = local_MsgBaseExplantionShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_64.Save(ref logic_SubGraph_SaveLoadBool_boolean_64, logic_SubGraph_SaveLoadBool_boolAsVariable_64, logic_SubGraph_SaveLoadBool_uniqueID_64);
	}

	private void Relay_Load_64()
	{
		logic_SubGraph_SaveLoadBool_boolean_64 = local_MsgBaseExplantionShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_64 = local_MsgBaseExplantionShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_64.Load(ref logic_SubGraph_SaveLoadBool_boolean_64, logic_SubGraph_SaveLoadBool_boolAsVariable_64, logic_SubGraph_SaveLoadBool_uniqueID_64);
	}

	private void Relay_Set_True_64()
	{
		logic_SubGraph_SaveLoadBool_boolean_64 = local_MsgBaseExplantionShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_64 = local_MsgBaseExplantionShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_64.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_64, logic_SubGraph_SaveLoadBool_boolAsVariable_64, logic_SubGraph_SaveLoadBool_uniqueID_64);
	}

	private void Relay_Set_False_64()
	{
		logic_SubGraph_SaveLoadBool_boolean_64 = local_MsgBaseExplantionShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_64 = local_MsgBaseExplantionShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_64.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_64, logic_SubGraph_SaveLoadBool_boolAsVariable_64, logic_SubGraph_SaveLoadBool_uniqueID_64);
	}

	private void Relay_In_65()
	{
		logic_uScriptCon_CompareBool_Bool_65 = local_FilterSetToFuel_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_65.In(logic_uScriptCon_CompareBool_Bool_65);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_65.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_65.False;
		if (num)
		{
			Relay_In_82();
		}
		if (flag)
		{
			Relay_True_194();
		}
	}

	private void Relay_True_67()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_67.True(out logic_uScriptAct_SetBool_Target_67);
		local_FilterSetToFuel_System_Boolean = logic_uScriptAct_SetBool_Target_67;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_67.Out)
		{
			Relay_False_196();
		}
	}

	private void Relay_False_67()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_67.False(out logic_uScriptAct_SetBool_Target_67);
		local_FilterSetToFuel_System_Boolean = logic_uScriptAct_SetBool_Target_67;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_67.Out)
		{
			Relay_False_196();
		}
	}

	private void Relay_Save_Out_70()
	{
		Relay_Save_197();
	}

	private void Relay_Load_Out_70()
	{
		Relay_Load_197();
	}

	private void Relay_Restart_Out_70()
	{
		Relay_Set_False_197();
	}

	private void Relay_Save_70()
	{
		logic_SubGraph_SaveLoadBool_boolean_70 = local_FilterSetToFuel_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_70 = local_FilterSetToFuel_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_70.Save(ref logic_SubGraph_SaveLoadBool_boolean_70, logic_SubGraph_SaveLoadBool_boolAsVariable_70, logic_SubGraph_SaveLoadBool_uniqueID_70);
	}

	private void Relay_Load_70()
	{
		logic_SubGraph_SaveLoadBool_boolean_70 = local_FilterSetToFuel_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_70 = local_FilterSetToFuel_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_70.Load(ref logic_SubGraph_SaveLoadBool_boolean_70, logic_SubGraph_SaveLoadBool_boolAsVariable_70, logic_SubGraph_SaveLoadBool_uniqueID_70);
	}

	private void Relay_Set_True_70()
	{
		logic_SubGraph_SaveLoadBool_boolean_70 = local_FilterSetToFuel_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_70 = local_FilterSetToFuel_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_70.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_70, logic_SubGraph_SaveLoadBool_boolAsVariable_70, logic_SubGraph_SaveLoadBool_uniqueID_70);
	}

	private void Relay_Set_False_70()
	{
		logic_SubGraph_SaveLoadBool_boolean_70 = local_FilterSetToFuel_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_70 = local_FilterSetToFuel_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_70.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_70, logic_SubGraph_SaveLoadBool_boolAsVariable_70, logic_SubGraph_SaveLoadBool_uniqueID_70);
	}

	private void Relay_In_71()
	{
		logic_uScript_IsHUDElementVisible_hudElement_71 = local_72_ManHUD_HUDElementType;
		logic_uScript_IsHUDElementVisible_uScript_IsHUDElementVisible_71.In(logic_uScript_IsHUDElementVisible_hudElement_71);
		bool num = logic_uScript_IsHUDElementVisible_uScript_IsHUDElementVisible_71.True;
		bool flag = logic_uScript_IsHUDElementVisible_uScript_IsHUDElementVisible_71.False;
		if (num)
		{
			Relay_In_270();
		}
		if (flag)
		{
			Relay_In_325();
		}
	}

	private void Relay_In_73()
	{
		logic_uScript_PointArrowAtVisible_targetObject_73 = local_FilterBlock_TankBlock;
		logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_73.In(logic_uScript_PointArrowAtVisible_targetObject_73, logic_uScript_PointArrowAtVisible_timeToShowFor_73, logic_uScript_PointArrowAtVisible_offset_73);
		if (logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_73.Out)
		{
			Relay_In_337();
		}
	}

	private void Relay_True_76()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_76.True(out logic_uScriptAct_SetBool_Target_76);
		local_FinalResourcesSpawned_System_Boolean = logic_uScriptAct_SetBool_Target_76;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_76.Out)
		{
			Relay_In_306();
		}
	}

	private void Relay_False_76()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_76.False(out logic_uScriptAct_SetBool_Target_76);
		local_FinalResourcesSpawned_System_Boolean = logic_uScriptAct_SetBool_Target_76;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_76.Out)
		{
			Relay_In_306();
		}
	}

	private void Relay_In_78()
	{
		logic_uScript_Wait_seconds_78 = timeWaitForResourcesInSilo;
		logic_uScript_Wait_uScript_Wait_78.In(logic_uScript_Wait_seconds_78, logic_uScript_Wait_repeat_78);
		if (logic_uScript_Wait_uScript_Wait_78.Waited)
		{
			Relay_True_79();
		}
	}

	private void Relay_True_79()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_79.True(out logic_uScriptAct_SetBool_Target_79);
		local_FinalChunksSpawned_System_Boolean = logic_uScriptAct_SetBool_Target_79;
	}

	private void Relay_False_79()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_79.False(out logic_uScriptAct_SetBool_Target_79);
		local_FinalChunksSpawned_System_Boolean = logic_uScriptAct_SetBool_Target_79;
	}

	private void Relay_In_80()
	{
		logic_uScriptCon_CompareBool_Bool_80 = local_FinalResourcesSpawned_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_80.In(logic_uScriptCon_CompareBool_Bool_80);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_80.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_80.False;
		if (num)
		{
			Relay_In_78();
		}
		if (flag)
		{
			Relay_True_76();
		}
	}

	private void Relay_In_82()
	{
		logic_uScriptCon_CompareBool_Bool_82 = local_FinalChunksSpawned_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_82.In(logic_uScriptCon_CompareBool_Bool_82);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_82.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_82.False;
		if (num)
		{
			Relay_In_254();
		}
		if (flag)
		{
			Relay_In_161();
		}
	}

	private void Relay_Save_Out_86()
	{
		Relay_Save_87();
	}

	private void Relay_Load_Out_86()
	{
		Relay_Load_87();
	}

	private void Relay_Restart_Out_86()
	{
		Relay_Set_False_87();
	}

	private void Relay_Save_86()
	{
		logic_SubGraph_SaveLoadBool_boolean_86 = local_FinalChunksSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_86 = local_FinalChunksSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_86.Save(ref logic_SubGraph_SaveLoadBool_boolean_86, logic_SubGraph_SaveLoadBool_boolAsVariable_86, logic_SubGraph_SaveLoadBool_uniqueID_86);
	}

	private void Relay_Load_86()
	{
		logic_SubGraph_SaveLoadBool_boolean_86 = local_FinalChunksSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_86 = local_FinalChunksSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_86.Load(ref logic_SubGraph_SaveLoadBool_boolean_86, logic_SubGraph_SaveLoadBool_boolAsVariable_86, logic_SubGraph_SaveLoadBool_uniqueID_86);
	}

	private void Relay_Set_True_86()
	{
		logic_SubGraph_SaveLoadBool_boolean_86 = local_FinalChunksSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_86 = local_FinalChunksSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_86.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_86, logic_SubGraph_SaveLoadBool_boolAsVariable_86, logic_SubGraph_SaveLoadBool_uniqueID_86);
	}

	private void Relay_Set_False_86()
	{
		logic_SubGraph_SaveLoadBool_boolean_86 = local_FinalChunksSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_86 = local_FinalChunksSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_86.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_86, logic_SubGraph_SaveLoadBool_boolAsVariable_86, logic_SubGraph_SaveLoadBool_uniqueID_86);
	}

	private void Relay_Save_Out_87()
	{
		Relay_Save_110();
	}

	private void Relay_Load_Out_87()
	{
		Relay_Load_110();
	}

	private void Relay_Restart_Out_87()
	{
		Relay_Set_False_110();
	}

	private void Relay_Save_87()
	{
		logic_SubGraph_SaveLoadBool_boolean_87 = local_FinalResourcesSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_87 = local_FinalResourcesSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_87.Save(ref logic_SubGraph_SaveLoadBool_boolean_87, logic_SubGraph_SaveLoadBool_boolAsVariable_87, logic_SubGraph_SaveLoadBool_uniqueID_87);
	}

	private void Relay_Load_87()
	{
		logic_SubGraph_SaveLoadBool_boolean_87 = local_FinalResourcesSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_87 = local_FinalResourcesSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_87.Load(ref logic_SubGraph_SaveLoadBool_boolean_87, logic_SubGraph_SaveLoadBool_boolAsVariable_87, logic_SubGraph_SaveLoadBool_uniqueID_87);
	}

	private void Relay_Set_True_87()
	{
		logic_SubGraph_SaveLoadBool_boolean_87 = local_FinalResourcesSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_87 = local_FinalResourcesSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_87.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_87, logic_SubGraph_SaveLoadBool_boolAsVariable_87, logic_SubGraph_SaveLoadBool_uniqueID_87);
	}

	private void Relay_Set_False_87()
	{
		logic_SubGraph_SaveLoadBool_boolean_87 = local_FinalResourcesSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_87 = local_FinalResourcesSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_87.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_87, logic_SubGraph_SaveLoadBool_boolAsVariable_87, logic_SubGraph_SaveLoadBool_uniqueID_87);
	}

	private void Relay_In_88()
	{
		logic_uScript_IsFilterSetToMode_filterBlock_88 = local_FilterBlock_TankBlock;
		logic_uScript_IsFilterSetToMode_uScript_IsFilterSetToMode_88.In(logic_uScript_IsFilterSetToMode_filterBlock_88, logic_uScript_IsFilterSetToMode_desiredMode_88);
		bool num = logic_uScript_IsFilterSetToMode_uScript_IsFilterSetToMode_88.True;
		bool flag = logic_uScript_IsFilterSetToMode_uScript_IsFilterSetToMode_88.False;
		if (num)
		{
			Relay_True_67();
		}
		if (flag)
		{
			Relay_In_71();
		}
	}

	private void Relay_In_90()
	{
		logic_uScriptCon_CompareBool_Bool_90 = local_GeneratorSpawned_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_90.In(logic_uScriptCon_CompareBool_Bool_90);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_90.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_90.False;
		if (num)
		{
			Relay_In_139();
		}
		if (flag)
		{
			Relay_True_92();
		}
	}

	private void Relay_True_92()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_92.True(out logic_uScriptAct_SetBool_Target_92);
		local_GeneratorSpawned_System_Boolean = logic_uScriptAct_SetBool_Target_92;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_92.Out)
		{
			Relay_In_143();
		}
	}

	private void Relay_False_92()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_92.False(out logic_uScriptAct_SetBool_Target_92);
		local_GeneratorSpawned_System_Boolean = logic_uScriptAct_SetBool_Target_92;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_92.Out)
		{
			Relay_In_143();
		}
	}

	private void Relay_True_93()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_93.True(out logic_uScriptAct_SetBool_Target_93);
		local_MsgAttachGeneratorShown_System_Boolean = logic_uScriptAct_SetBool_Target_93;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_93.Out)
		{
			Relay_In_42();
		}
	}

	private void Relay_False_93()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_93.False(out logic_uScriptAct_SetBool_Target_93);
		local_MsgAttachGeneratorShown_System_Boolean = logic_uScriptAct_SetBool_Target_93;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_93.Out)
		{
			Relay_In_42();
		}
	}

	private void Relay_In_96()
	{
		logic_uScriptCon_CompareBool_Bool_96 = local_MsgAttachGeneratorShown_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_96.In(logic_uScriptCon_CompareBool_Bool_96);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_96.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_96.False;
		if (num)
		{
			Relay_In_229();
		}
		if (flag)
		{
			Relay_In_226();
		}
	}

	private void Relay_In_97()
	{
		logic_uScript_LockTechInteraction_tech_97 = local_CraftingBaseTech_Tank;
		logic_uScript_LockTechInteraction_uScript_LockTechInteraction_97.In(logic_uScript_LockTechInteraction_tech_97, logic_uScript_LockTechInteraction_excludedBlocks_97);
		if (logic_uScript_LockTechInteraction_uScript_LockTechInteraction_97.Out)
		{
			Relay_In_309();
		}
	}

	private void Relay_In_99()
	{
		logic_uScript_LockTechInteraction_tech_99 = local_CraftingBaseTech_Tank;
		logic_uScript_LockTechInteraction_uScript_LockTechInteraction_99.In(logic_uScript_LockTechInteraction_tech_99, logic_uScript_LockTechInteraction_excludedBlocks_99);
		if (logic_uScript_LockTechInteraction_uScript_LockTechInteraction_99.Out)
		{
			Relay_In_309();
		}
	}

	private void Relay_In_103()
	{
		logic_uScriptCon_CompareBool_Bool_103 = local_MsgBaseExplantionShown_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_103.In(logic_uScriptCon_CompareBool_Bool_103);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_103.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_103.False;
		if (num)
		{
			Relay_In_245();
		}
		if (flag)
		{
			Relay_In_242();
		}
	}

	private void Relay_True_104()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_104.True(out logic_uScriptAct_SetBool_Target_104);
		local_MsgBaseExplantionShown_System_Boolean = logic_uScriptAct_SetBool_Target_104;
	}

	private void Relay_False_104()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_104.False(out logic_uScriptAct_SetBool_Target_104);
		local_MsgBaseExplantionShown_System_Boolean = logic_uScriptAct_SetBool_Target_104;
	}

	private void Relay_True_107()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_107.True(out logic_uScriptAct_SetBool_Target_107);
		local_MsgSeeGeneratorWorkingShown_System_Boolean = logic_uScriptAct_SetBool_Target_107;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_107.Out)
		{
			Relay_In_33();
		}
	}

	private void Relay_False_107()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_107.False(out logic_uScriptAct_SetBool_Target_107);
		local_MsgSeeGeneratorWorkingShown_System_Boolean = logic_uScriptAct_SetBool_Target_107;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_107.Out)
		{
			Relay_In_33();
		}
	}

	private void Relay_In_108()
	{
		logic_uScriptCon_CompareBool_Bool_108 = local_MsgSeeGeneratorWorkingShown_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_108.In(logic_uScriptCon_CompareBool_Bool_108);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_108.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_108.False;
		if (num)
		{
			Relay_In_235();
		}
		if (flag)
		{
			Relay_In_232();
		}
	}

	private void Relay_Save_Out_110()
	{
		Relay_Save_116();
	}

	private void Relay_Load_Out_110()
	{
		Relay_Load_116();
	}

	private void Relay_Restart_Out_110()
	{
		Relay_Set_False_116();
	}

	private void Relay_Save_110()
	{
		logic_SubGraph_SaveLoadBool_boolean_110 = local_MsgSeeGeneratorWorkingShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_110 = local_MsgSeeGeneratorWorkingShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_110.Save(ref logic_SubGraph_SaveLoadBool_boolean_110, logic_SubGraph_SaveLoadBool_boolAsVariable_110, logic_SubGraph_SaveLoadBool_uniqueID_110);
	}

	private void Relay_Load_110()
	{
		logic_SubGraph_SaveLoadBool_boolean_110 = local_MsgSeeGeneratorWorkingShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_110 = local_MsgSeeGeneratorWorkingShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_110.Load(ref logic_SubGraph_SaveLoadBool_boolean_110, logic_SubGraph_SaveLoadBool_boolAsVariable_110, logic_SubGraph_SaveLoadBool_uniqueID_110);
	}

	private void Relay_Set_True_110()
	{
		logic_SubGraph_SaveLoadBool_boolean_110 = local_MsgSeeGeneratorWorkingShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_110 = local_MsgSeeGeneratorWorkingShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_110.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_110, logic_SubGraph_SaveLoadBool_boolAsVariable_110, logic_SubGraph_SaveLoadBool_uniqueID_110);
	}

	private void Relay_Set_False_110()
	{
		logic_SubGraph_SaveLoadBool_boolean_110 = local_MsgSeeGeneratorWorkingShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_110 = local_MsgSeeGeneratorWorkingShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_110.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_110, logic_SubGraph_SaveLoadBool_boolAsVariable_110, logic_SubGraph_SaveLoadBool_uniqueID_110);
	}

	private void Relay_True_112()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_112.True(out logic_uScriptAct_SetBool_Target_112);
		local_MsgFilterTrainedShown_System_Boolean = logic_uScriptAct_SetBool_Target_112;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_112.Out)
		{
			Relay_In_90();
		}
	}

	private void Relay_False_112()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_112.False(out logic_uScriptAct_SetBool_Target_112);
		local_MsgFilterTrainedShown_System_Boolean = logic_uScriptAct_SetBool_Target_112;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_112.Out)
		{
			Relay_In_90();
		}
	}

	private void Relay_In_114()
	{
		logic_uScriptCon_CompareBool_Bool_114 = local_MsgFilterTrainedShown_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_114.In(logic_uScriptCon_CompareBool_Bool_114);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_114.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_114.False;
		if (num)
		{
			Relay_In_90();
		}
		if (flag)
		{
			Relay_In_222();
		}
	}

	private void Relay_Save_Out_116()
	{
		Relay_Save_129();
	}

	private void Relay_Load_Out_116()
	{
		Relay_Load_129();
	}

	private void Relay_Restart_Out_116()
	{
		Relay_Set_False_129();
	}

	private void Relay_Save_116()
	{
		logic_SubGraph_SaveLoadBool_boolean_116 = local_MsgFilterTrainedShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_116 = local_MsgFilterTrainedShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_116.Save(ref logic_SubGraph_SaveLoadBool_boolean_116, logic_SubGraph_SaveLoadBool_boolAsVariable_116, logic_SubGraph_SaveLoadBool_uniqueID_116);
	}

	private void Relay_Load_116()
	{
		logic_SubGraph_SaveLoadBool_boolean_116 = local_MsgFilterTrainedShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_116 = local_MsgFilterTrainedShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_116.Load(ref logic_SubGraph_SaveLoadBool_boolean_116, logic_SubGraph_SaveLoadBool_boolAsVariable_116, logic_SubGraph_SaveLoadBool_uniqueID_116);
	}

	private void Relay_Set_True_116()
	{
		logic_SubGraph_SaveLoadBool_boolean_116 = local_MsgFilterTrainedShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_116 = local_MsgFilterTrainedShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_116.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_116, logic_SubGraph_SaveLoadBool_boolAsVariable_116, logic_SubGraph_SaveLoadBool_uniqueID_116);
	}

	private void Relay_Set_False_116()
	{
		logic_SubGraph_SaveLoadBool_boolean_116 = local_MsgFilterTrainedShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_116 = local_MsgFilterTrainedShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_116.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_116, logic_SubGraph_SaveLoadBool_boolAsVariable_116, logic_SubGraph_SaveLoadBool_uniqueID_116);
	}

	private void Relay_In_122()
	{
		logic_uScript_GetPositionInEncounter_ownerNode_122 = owner_Connection_119;
		logic_uScript_GetPositionInEncounter_posName_122 = resourceSpawnPos;
		logic_uScript_GetPositionInEncounter_Return_122 = logic_uScript_GetPositionInEncounter_uScript_GetPositionInEncounter_122.In(logic_uScript_GetPositionInEncounter_ownerNode_122, logic_uScript_GetPositionInEncounter_posName_122);
		local_121_UnityEngine_Vector3 = logic_uScript_GetPositionInEncounter_Return_122;
		if (logic_uScript_GetPositionInEncounter_uScript_GetPositionInEncounter_122.Out)
		{
			Relay_In_123();
		}
	}

	private void Relay_In_123()
	{
		logic_uScript_SpawnResourceAtPosition_chunkType_123 = filterResourceType;
		logic_uScript_SpawnResourceAtPosition_quantity_123 = filterResourceAmount;
		logic_uScript_SpawnResourceAtPosition_position_123 = local_121_UnityEngine_Vector3;
		logic_uScript_SpawnResourceAtPosition_uScript_SpawnResourceAtPosition_123.In(logic_uScript_SpawnResourceAtPosition_chunkType_123, logic_uScript_SpawnResourceAtPosition_quantity_123, logic_uScript_SpawnResourceAtPosition_position_123);
		if (logic_uScript_SpawnResourceAtPosition_uScript_SpawnResourceAtPosition_123.Out)
		{
			Relay_True_124();
		}
	}

	private void Relay_True_124()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_124.True(out logic_uScriptAct_SetBool_Target_124);
		local_MsgWoodSpawned_System_Boolean = logic_uScriptAct_SetBool_Target_124;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_124.Out)
		{
			Relay_True_190();
		}
	}

	private void Relay_False_124()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_124.False(out logic_uScriptAct_SetBool_Target_124);
		local_MsgWoodSpawned_System_Boolean = logic_uScriptAct_SetBool_Target_124;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_124.Out)
		{
			Relay_True_190();
		}
	}

	private void Relay_In_126()
	{
		logic_uScriptCon_CompareBool_Bool_126 = local_MsgWoodSpawned_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_126.In(logic_uScriptCon_CompareBool_Bool_126);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_126.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_126.False;
		if (num)
		{
			Relay_True_190();
		}
		if (flag)
		{
			Relay_In_122();
		}
	}

	private void Relay_Save_Out_129()
	{
		Relay_Save_317();
	}

	private void Relay_Load_Out_129()
	{
		Relay_Load_317();
	}

	private void Relay_Restart_Out_129()
	{
		Relay_Set_False_317();
	}

	private void Relay_Save_129()
	{
		logic_SubGraph_SaveLoadBool_boolean_129 = local_MsgWoodSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_129 = local_MsgWoodSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_129.Save(ref logic_SubGraph_SaveLoadBool_boolean_129, logic_SubGraph_SaveLoadBool_boolAsVariable_129, logic_SubGraph_SaveLoadBool_uniqueID_129);
	}

	private void Relay_Load_129()
	{
		logic_SubGraph_SaveLoadBool_boolean_129 = local_MsgWoodSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_129 = local_MsgWoodSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_129.Load(ref logic_SubGraph_SaveLoadBool_boolean_129, logic_SubGraph_SaveLoadBool_boolAsVariable_129, logic_SubGraph_SaveLoadBool_uniqueID_129);
	}

	private void Relay_Set_True_129()
	{
		logic_SubGraph_SaveLoadBool_boolean_129 = local_MsgWoodSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_129 = local_MsgWoodSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_129.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_129, logic_SubGraph_SaveLoadBool_boolAsVariable_129, logic_SubGraph_SaveLoadBool_uniqueID_129);
	}

	private void Relay_Set_False_129()
	{
		logic_SubGraph_SaveLoadBool_boolean_129 = local_MsgWoodSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_129 = local_MsgWoodSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_129.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_129, logic_SubGraph_SaveLoadBool_boolAsVariable_129, logic_SubGraph_SaveLoadBool_uniqueID_129);
	}

	private void Relay_In_130()
	{
		logic_uScriptCon_CompareBool_Bool_130 = local_msgIntroShown_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_130.In(logic_uScriptCon_CompareBool_Bool_130);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_130.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_130.False;
		if (num)
		{
			Relay_In_2();
		}
		if (flag)
		{
			Relay_True_134();
		}
	}

	private void Relay_In_132()
	{
		logic_uScript_IsPlayerInRangeOfTech_tech_132 = local_CraftingBaseTech_Tank;
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_132.In(logic_uScript_IsPlayerInRangeOfTech_tech_132, logic_uScript_IsPlayerInRangeOfTech_range_132, logic_uScript_IsPlayerInRangeOfTech_techs_132);
		if (logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_132.InRange)
		{
			Relay_In_130();
		}
	}

	private void Relay_True_134()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_134.True(out logic_uScriptAct_SetBool_Target_134);
		local_msgIntroShown_System_Boolean = logic_uScriptAct_SetBool_Target_134;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_134.Out)
		{
			Relay_In_208();
		}
	}

	private void Relay_False_134()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_134.False(out logic_uScriptAct_SetBool_Target_134);
		local_msgIntroShown_System_Boolean = logic_uScriptAct_SetBool_Target_134;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_134.Out)
		{
			Relay_In_208();
		}
	}

	private void Relay_Out_136()
	{
		Relay_In_175();
	}

	private void Relay_In_136()
	{
		int num = 0;
		Array array = blockSpawnData;
		if (logic_SubGraph_Crafting_Tutorial_ManageBlock_blockSpawnData_136.Length != num + array.Length)
		{
			Array.Resize(ref logic_SubGraph_Crafting_Tutorial_ManageBlock_blockSpawnData_136, num + array.Length);
		}
		Array.Copy(array, 0, logic_SubGraph_Crafting_Tutorial_ManageBlock_blockSpawnData_136, num, array.Length);
		num += array.Length;
		logic_SubGraph_Crafting_Tutorial_ManageBlock_block_136 = local_ReceiverBlock_TankBlock;
		logic_SubGraph_Crafting_Tutorial_ManageBlock_msgBlockOutsideArea_136 = msgBlockOutsideArea;
		logic_SubGraph_Crafting_Tutorial_ManageBlock_messageSpeaker_136 = messageSpeaker;
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_136.In(ref logic_SubGraph_Crafting_Tutorial_ManageBlock_blockSpawnData_136, logic_SubGraph_Crafting_Tutorial_ManageBlock_blockIndex_136, ref logic_SubGraph_Crafting_Tutorial_ManageBlock_block_136, logic_SubGraph_Crafting_Tutorial_ManageBlock_allowAnchoring_136, logic_SubGraph_Crafting_Tutorial_ManageBlock_msgBlockOutsideArea_136, logic_SubGraph_Crafting_Tutorial_ManageBlock_messageSpeaker_136);
	}

	private void Relay_In_139()
	{
		int num = 0;
		Array array = blockSpawnDataGenerator;
		if (logic_uScript_GetAndCheckBlocks_blockData_139.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckBlocks_blockData_139, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckBlocks_blockData_139, num, array.Length);
		num += array.Length;
		logic_uScript_GetAndCheckBlocks_ownerNode_139 = owner_Connection_140;
		logic_uScript_GetAndCheckBlocks_uScript_GetAndCheckBlocks_139.In(logic_uScript_GetAndCheckBlocks_blockData_139, logic_uScript_GetAndCheckBlocks_ownerNode_139, ref logic_uScript_GetAndCheckBlocks_blocks_139);
		bool allAlive = logic_uScript_GetAndCheckBlocks_uScript_GetAndCheckBlocks_139.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckBlocks_uScript_GetAndCheckBlocks_139.SomeAlive;
		if (allAlive)
		{
			Relay_In_96();
		}
		if (someAlive)
		{
			Relay_In_96();
		}
	}

	private void Relay_In_143()
	{
		int num = 0;
		Array array = blockSpawnDataGenerator;
		if (logic_uScript_SpawnBlocksFromData_blockData_143.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SpawnBlocksFromData_blockData_143, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SpawnBlocksFromData_blockData_143, num, array.Length);
		num += array.Length;
		logic_uScript_SpawnBlocksFromData_ownerNode_143 = owner_Connection_142;
		logic_uScript_SpawnBlocksFromData_uScript_SpawnBlocksFromData_143.In(logic_uScript_SpawnBlocksFromData_blockData_143, logic_uScript_SpawnBlocksFromData_ownerNode_143);
		if (logic_uScript_SpawnBlocksFromData_uScript_SpawnBlocksFromData_143.Out)
		{
			Relay_In_139();
		}
	}

	private void Relay_In_144()
	{
		logic_uScript_IsShieldBlockPowered_shieldBlock_144 = local_148_TankBlock;
		logic_uScript_IsShieldBlockPowered_uScript_IsShieldBlockPowered_144.In(logic_uScript_IsShieldBlockPowered_shieldBlock_144);
		bool num = logic_uScript_IsShieldBlockPowered_uScript_IsShieldBlockPowered_144.True;
		bool flag = logic_uScript_IsShieldBlockPowered_uScript_IsShieldBlockPowered_144.False;
		if (num)
		{
			Relay_In_322();
		}
		if (flag)
		{
			Relay_In_324();
		}
	}

	private void Relay_In_145()
	{
		logic_uScript_GetTankBlock_tank_145 = local_CraftingBaseTech_Tank;
		logic_uScript_GetTankBlock_blockType_145 = shieldBlockType;
		logic_uScript_GetTankBlock_Return_145 = logic_uScript_GetTankBlock_uScript_GetTankBlock_145.In(logic_uScript_GetTankBlock_tank_145, logic_uScript_GetTankBlock_blockType_145);
		local_148_TankBlock = logic_uScript_GetTankBlock_Return_145;
		if (logic_uScript_GetTankBlock_uScript_GetTankBlock_145.Returned)
		{
			Relay_In_144();
		}
	}

	private void Relay_In_149()
	{
		logic_uScriptCon_CompareBool_Bool_149 = local_msgReceiverAttachedShown_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_149.In(logic_uScriptCon_CompareBool_Bool_149);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_149.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_149.False;
		if (num)
		{
			Relay_In_7();
		}
		if (flag)
		{
			Relay_In_238();
		}
	}

	private void Relay_True_151()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_151.True(out logic_uScriptAct_SetBool_Target_151);
		local_msgReceiverAttachedShown_System_Boolean = logic_uScriptAct_SetBool_Target_151;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_151.Out)
		{
			Relay_In_7();
		}
	}

	private void Relay_False_151()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_151.False(out logic_uScriptAct_SetBool_Target_151);
		local_msgReceiverAttachedShown_System_Boolean = logic_uScriptAct_SetBool_Target_151;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_151.Out)
		{
			Relay_In_7();
		}
	}

	private void Relay_In_156()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_156 = messageTag;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_156.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_156, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_156);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_156.Out)
		{
			Relay_True_314();
		}
	}

	private void Relay_In_157()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_157 = messageTag;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_157.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_157, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_157);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_157.Out)
		{
			Relay_In_207();
		}
	}

	private void Relay_True_160()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_160.True(out logic_uScriptAct_SetBool_Target_160);
		local_msgFilterSetToFuelShown_System_Boolean = logic_uScriptAct_SetBool_Target_160;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_160.Out)
		{
			Relay_In_80();
		}
	}

	private void Relay_False_160()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_160.False(out logic_uScriptAct_SetBool_Target_160);
		local_msgFilterSetToFuelShown_System_Boolean = logic_uScriptAct_SetBool_Target_160;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_160.Out)
		{
			Relay_In_80();
		}
	}

	private void Relay_In_161()
	{
		logic_uScriptCon_CompareBool_Bool_161 = local_msgFilterSetToFuelShown_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_161.In(logic_uScriptCon_CompareBool_Bool_161);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_161.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_161.False;
		if (num)
		{
			Relay_In_80();
		}
		if (flag)
		{
			Relay_In_251();
		}
	}

	private void Relay_In_163()
	{
		logic_uScript_HideArrow_uScript_HideArrow_163.In();
		if (logic_uScript_HideArrow_uScript_HideArrow_163.Out)
		{
			Relay_In_340();
		}
	}

	private void Relay_True_164()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_164.True(out logic_uScriptAct_SetBool_Target_164);
		local_NearBase_System_Boolean = logic_uScriptAct_SetBool_Target_164;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_164.Out)
		{
			Relay_In_183();
		}
	}

	private void Relay_False_164()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_164.False(out logic_uScriptAct_SetBool_Target_164);
		local_NearBase_System_Boolean = logic_uScriptAct_SetBool_Target_164;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_164.Out)
		{
			Relay_In_183();
		}
	}

	private void Relay_In_165()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_165 = messageTag;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_165.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_165, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_165);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_165.Out)
		{
			Relay_In_163();
		}
	}

	private void Relay_Save_Out_167()
	{
		Relay_Save_169();
	}

	private void Relay_Load_Out_167()
	{
		Relay_Load_169();
	}

	private void Relay_Restart_Out_167()
	{
		Relay_Set_False_169();
	}

	private void Relay_Save_167()
	{
		logic_SubGraph_SaveLoadBool_boolean_167 = local_ShieldPowered_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_167 = local_ShieldPowered_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_167.Save(ref logic_SubGraph_SaveLoadBool_boolean_167, logic_SubGraph_SaveLoadBool_boolAsVariable_167, logic_SubGraph_SaveLoadBool_uniqueID_167);
	}

	private void Relay_Load_167()
	{
		logic_SubGraph_SaveLoadBool_boolean_167 = local_ShieldPowered_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_167 = local_ShieldPowered_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_167.Load(ref logic_SubGraph_SaveLoadBool_boolean_167, logic_SubGraph_SaveLoadBool_boolAsVariable_167, logic_SubGraph_SaveLoadBool_uniqueID_167);
	}

	private void Relay_Set_True_167()
	{
		logic_SubGraph_SaveLoadBool_boolean_167 = local_ShieldPowered_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_167 = local_ShieldPowered_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_167.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_167, logic_SubGraph_SaveLoadBool_boolAsVariable_167, logic_SubGraph_SaveLoadBool_uniqueID_167);
	}

	private void Relay_Set_False_167()
	{
		logic_SubGraph_SaveLoadBool_boolean_167 = local_ShieldPowered_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_167 = local_ShieldPowered_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_167.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_167, logic_SubGraph_SaveLoadBool_boolAsVariable_167, logic_SubGraph_SaveLoadBool_uniqueID_167);
	}

	private void Relay_Save_Out_169()
	{
		Relay_Save_171();
	}

	private void Relay_Load_Out_169()
	{
		Relay_Load_171();
	}

	private void Relay_Restart_Out_169()
	{
		Relay_Set_False_171();
	}

	private void Relay_Save_169()
	{
		logic_SubGraph_SaveLoadBool_boolean_169 = local_msgReceiverAttachedShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_169 = local_msgReceiverAttachedShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_169.Save(ref logic_SubGraph_SaveLoadBool_boolean_169, logic_SubGraph_SaveLoadBool_boolAsVariable_169, logic_SubGraph_SaveLoadBool_uniqueID_169);
	}

	private void Relay_Load_169()
	{
		logic_SubGraph_SaveLoadBool_boolean_169 = local_msgReceiverAttachedShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_169 = local_msgReceiverAttachedShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_169.Load(ref logic_SubGraph_SaveLoadBool_boolean_169, logic_SubGraph_SaveLoadBool_boolAsVariable_169, logic_SubGraph_SaveLoadBool_uniqueID_169);
	}

	private void Relay_Set_True_169()
	{
		logic_SubGraph_SaveLoadBool_boolean_169 = local_msgReceiverAttachedShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_169 = local_msgReceiverAttachedShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_169.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_169, logic_SubGraph_SaveLoadBool_boolAsVariable_169, logic_SubGraph_SaveLoadBool_uniqueID_169);
	}

	private void Relay_Set_False_169()
	{
		logic_SubGraph_SaveLoadBool_boolean_169 = local_msgReceiverAttachedShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_169 = local_msgReceiverAttachedShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_169.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_169, logic_SubGraph_SaveLoadBool_boolAsVariable_169, logic_SubGraph_SaveLoadBool_uniqueID_169);
	}

	private void Relay_Save_Out_171()
	{
	}

	private void Relay_Load_Out_171()
	{
		Relay_In_187();
	}

	private void Relay_Restart_Out_171()
	{
		Relay_False_173();
	}

	private void Relay_Save_171()
	{
		logic_SubGraph_SaveLoadBool_boolean_171 = local_msgFilterSetToFuelShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_171 = local_msgFilterSetToFuelShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_171.Save(ref logic_SubGraph_SaveLoadBool_boolean_171, logic_SubGraph_SaveLoadBool_boolAsVariable_171, logic_SubGraph_SaveLoadBool_uniqueID_171);
	}

	private void Relay_Load_171()
	{
		logic_SubGraph_SaveLoadBool_boolean_171 = local_msgFilterSetToFuelShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_171 = local_msgFilterSetToFuelShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_171.Load(ref logic_SubGraph_SaveLoadBool_boolean_171, logic_SubGraph_SaveLoadBool_boolAsVariable_171, logic_SubGraph_SaveLoadBool_uniqueID_171);
	}

	private void Relay_Set_True_171()
	{
		logic_SubGraph_SaveLoadBool_boolean_171 = local_msgFilterSetToFuelShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_171 = local_msgFilterSetToFuelShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_171.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_171, logic_SubGraph_SaveLoadBool_boolAsVariable_171, logic_SubGraph_SaveLoadBool_uniqueID_171);
	}

	private void Relay_Set_False_171()
	{
		logic_SubGraph_SaveLoadBool_boolean_171 = local_msgFilterSetToFuelShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_171 = local_msgFilterSetToFuelShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_171.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_171, logic_SubGraph_SaveLoadBool_boolAsVariable_171, logic_SubGraph_SaveLoadBool_uniqueID_171);
	}

	private void Relay_True_173()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_173.True(out logic_uScriptAct_SetBool_Target_173);
		local_NearBase_System_Boolean = logic_uScriptAct_SetBool_Target_173;
	}

	private void Relay_False_173()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_173.False(out logic_uScriptAct_SetBool_Target_173);
		local_NearBase_System_Boolean = logic_uScriptAct_SetBool_Target_173;
	}

	private void Relay_In_175()
	{
		logic_uScriptCon_CompareBool_Bool_175 = local_GeneratorSpawned_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_175.In(logic_uScriptCon_CompareBool_Bool_175);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_175.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_175.False;
		if (num)
		{
			Relay_In_178();
		}
		if (flag)
		{
			Relay_In_179();
		}
	}

	private void Relay_Out_178()
	{
		Relay_In_180();
	}

	private void Relay_In_178()
	{
		int num = 0;
		Array array = blockSpawnDataGenerator;
		if (logic_SubGraph_Crafting_Tutorial_ManageBlock_blockSpawnData_178.Length != num + array.Length)
		{
			Array.Resize(ref logic_SubGraph_Crafting_Tutorial_ManageBlock_blockSpawnData_178, num + array.Length);
		}
		Array.Copy(array, 0, logic_SubGraph_Crafting_Tutorial_ManageBlock_blockSpawnData_178, num, array.Length);
		num += array.Length;
		logic_SubGraph_Crafting_Tutorial_ManageBlock_block_178 = local_GeneratorBlock_TankBlock;
		logic_SubGraph_Crafting_Tutorial_ManageBlock_msgBlockOutsideArea_178 = msgBlockOutsideArea;
		logic_SubGraph_Crafting_Tutorial_ManageBlock_messageSpeaker_178 = messageSpeaker;
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_178.In(ref logic_SubGraph_Crafting_Tutorial_ManageBlock_blockSpawnData_178, logic_SubGraph_Crafting_Tutorial_ManageBlock_blockIndex_178, ref logic_SubGraph_Crafting_Tutorial_ManageBlock_block_178, logic_SubGraph_Crafting_Tutorial_ManageBlock_allowAnchoring_178, logic_SubGraph_Crafting_Tutorial_ManageBlock_msgBlockOutsideArea_178, logic_SubGraph_Crafting_Tutorial_ManageBlock_messageSpeaker_178);
	}

	private void Relay_In_179()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_179.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_179.Out)
		{
			Relay_In_180();
		}
	}

	private void Relay_In_180()
	{
		logic_uScriptCon_CompareBool_Bool_180 = local_AllowFilterInteraction_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_180.In(logic_uScriptCon_CompareBool_Bool_180);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_180.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_180.False;
		if (num)
		{
			Relay_In_99();
		}
		if (flag)
		{
			Relay_In_97();
		}
	}

	private void Relay_Output1_183()
	{
		Relay_In_201();
	}

	private void Relay_Output2_183()
	{
		Relay_In_48();
	}

	private void Relay_Output3_183()
	{
		Relay_In_55();
	}

	private void Relay_Output4_183()
	{
		Relay_In_312();
	}

	private void Relay_Output5_183()
	{
		Relay_In_108();
	}

	private void Relay_Output6_183()
	{
		Relay_In_17();
	}

	private void Relay_Output7_183()
	{
	}

	private void Relay_Output8_183()
	{
	}

	private void Relay_In_183()
	{
		logic_uScriptCon_ManualSwitch_CurrentOutput_183 = local_Stage_System_Int32;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_183.In(logic_uScriptCon_ManualSwitch_CurrentOutput_183);
	}

	private void Relay_Save_Out_185()
	{
		Relay_Save_297();
	}

	private void Relay_Load_Out_185()
	{
		Relay_Load_297();
	}

	private void Relay_Restart_Out_185()
	{
		Relay_Set_False_297();
	}

	private void Relay_Save_185()
	{
		logic_SubGraph_SaveLoadInt_integer_185 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_185 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_185.Save(logic_SubGraph_SaveLoadInt_restartValue_185, ref logic_SubGraph_SaveLoadInt_integer_185, logic_SubGraph_SaveLoadInt_intAsVariable_185, logic_SubGraph_SaveLoadInt_uniqueID_185);
	}

	private void Relay_Load_185()
	{
		logic_SubGraph_SaveLoadInt_integer_185 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_185 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_185.Load(logic_SubGraph_SaveLoadInt_restartValue_185, ref logic_SubGraph_SaveLoadInt_integer_185, logic_SubGraph_SaveLoadInt_intAsVariable_185, logic_SubGraph_SaveLoadInt_uniqueID_185);
	}

	private void Relay_Restart_185()
	{
		logic_SubGraph_SaveLoadInt_integer_185 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_185 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_185.Restart(logic_SubGraph_SaveLoadInt_restartValue_185, ref logic_SubGraph_SaveLoadInt_integer_185, logic_SubGraph_SaveLoadInt_intAsVariable_185, logic_SubGraph_SaveLoadInt_uniqueID_185);
	}

	private void Relay_Out_187()
	{
	}

	private void Relay_In_187()
	{
		logic_SubGraph_LoadObjectiveStates_currentObjective_187 = local_Stage_System_Int32;
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_187.In(logic_SubGraph_LoadObjectiveStates_currentObjective_187);
	}

	private void Relay_True_190()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_190.True(out logic_uScriptAct_SetBool_Target_190);
		local_AllowFilterInteraction_System_Boolean = logic_uScriptAct_SetBool_Target_190;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_190.Out)
		{
			Relay_In_36();
		}
	}

	private void Relay_False_190()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_190.False(out logic_uScriptAct_SetBool_Target_190);
		local_AllowFilterInteraction_System_Boolean = logic_uScriptAct_SetBool_Target_190;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_190.Out)
		{
			Relay_In_36();
		}
	}

	private void Relay_True_192()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_192.True(out logic_uScriptAct_SetBool_Target_192);
		local_AllowFilterInteraction_System_Boolean = logic_uScriptAct_SetBool_Target_192;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_192.Out)
		{
			Relay_In_45();
		}
	}

	private void Relay_False_192()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_192.False(out logic_uScriptAct_SetBool_Target_192);
		local_AllowFilterInteraction_System_Boolean = logic_uScriptAct_SetBool_Target_192;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_192.Out)
		{
			Relay_In_45();
		}
	}

	private void Relay_True_194()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_194.True(out logic_uScriptAct_SetBool_Target_194);
		local_AllowFilterInteraction_System_Boolean = logic_uScriptAct_SetBool_Target_194;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_194.Out)
		{
			Relay_In_88();
		}
	}

	private void Relay_False_194()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_194.False(out logic_uScriptAct_SetBool_Target_194);
		local_AllowFilterInteraction_System_Boolean = logic_uScriptAct_SetBool_Target_194;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_194.Out)
		{
			Relay_In_88();
		}
	}

	private void Relay_True_196()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_196.True(out logic_uScriptAct_SetBool_Target_196);
		local_AllowFilterInteraction_System_Boolean = logic_uScriptAct_SetBool_Target_196;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_196.Out)
		{
			Relay_In_365();
		}
	}

	private void Relay_False_196()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_196.False(out logic_uScriptAct_SetBool_Target_196);
		local_AllowFilterInteraction_System_Boolean = logic_uScriptAct_SetBool_Target_196;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_196.Out)
		{
			Relay_In_365();
		}
	}

	private void Relay_Save_Out_197()
	{
		Relay_Save_86();
	}

	private void Relay_Load_Out_197()
	{
		Relay_Load_86();
	}

	private void Relay_Restart_Out_197()
	{
		Relay_Set_False_86();
	}

	private void Relay_Save_197()
	{
		logic_SubGraph_SaveLoadBool_boolean_197 = local_AllowFilterInteraction_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_197 = local_AllowFilterInteraction_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_197.Save(ref logic_SubGraph_SaveLoadBool_boolean_197, logic_SubGraph_SaveLoadBool_boolAsVariable_197, logic_SubGraph_SaveLoadBool_uniqueID_197);
	}

	private void Relay_Load_197()
	{
		logic_SubGraph_SaveLoadBool_boolean_197 = local_AllowFilterInteraction_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_197 = local_AllowFilterInteraction_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_197.Load(ref logic_SubGraph_SaveLoadBool_boolean_197, logic_SubGraph_SaveLoadBool_boolAsVariable_197, logic_SubGraph_SaveLoadBool_uniqueID_197);
	}

	private void Relay_Set_True_197()
	{
		logic_SubGraph_SaveLoadBool_boolean_197 = local_AllowFilterInteraction_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_197 = local_AllowFilterInteraction_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_197.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_197, logic_SubGraph_SaveLoadBool_boolAsVariable_197, logic_SubGraph_SaveLoadBool_uniqueID_197);
	}

	private void Relay_Set_False_197()
	{
		logic_SubGraph_SaveLoadBool_boolean_197 = local_AllowFilterInteraction_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_197 = local_AllowFilterInteraction_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_197.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_197, logic_SubGraph_SaveLoadBool_boolAsVariable_197, logic_SubGraph_SaveLoadBool_uniqueID_197);
	}

	private void Relay_Out_201()
	{
		Relay_In_368();
	}

	private void Relay_In_201()
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_201 = local_Stage_System_Int32;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_201.In(ref logic_SubGraph_CompleteObjectiveStage_objectiveStage_201, logic_SubGraph_CompleteObjectiveStage_isFinalObjective_201);
	}

	private void Relay_Out_203()
	{
	}

	private void Relay_In_203()
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_203 = local_Stage_System_Int32;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_203.In(ref logic_SubGraph_CompleteObjectiveStage_objectiveStage_203, logic_SubGraph_CompleteObjectiveStage_isFinalObjective_203);
	}

	private void Relay_Out_205()
	{
	}

	private void Relay_In_205()
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_205 = local_Stage_System_Int32;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_205.In(ref logic_SubGraph_CompleteObjectiveStage_objectiveStage_205, logic_SubGraph_CompleteObjectiveStage_isFinalObjective_205);
	}

	private void Relay_Out_207()
	{
	}

	private void Relay_In_207()
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_207 = local_Stage_System_Int32;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_207.In(ref logic_SubGraph_CompleteObjectiveStage_objectiveStage_207, logic_SubGraph_CompleteObjectiveStage_isFinalObjective_207);
	}

	private void Relay_In_208()
	{
		logic_uScript_AddMessage_messageData_208 = msg01Intro;
		logic_uScript_AddMessage_speaker_208 = messageSpeaker;
		logic_uScript_AddMessage_Return_208 = logic_uScript_AddMessage_uScript_AddMessage_208.In(logic_uScript_AddMessage_messageData_208, logic_uScript_AddMessage_speaker_208);
		if (logic_uScript_AddMessage_uScript_AddMessage_208.Out)
		{
			Relay_In_2();
		}
	}

	private void Relay_In_211()
	{
		logic_uScript_AddMessage_messageData_211 = msg02BaseFound;
		logic_uScript_AddMessage_speaker_211 = messageSpeaker;
		logic_uScript_AddMessage_Return_211 = logic_uScript_AddMessage_uScript_AddMessage_211.In(logic_uScript_AddMessage_messageData_211, logic_uScript_AddMessage_speaker_211);
		if (logic_uScript_AddMessage_uScript_AddMessage_211.Shown)
		{
			Relay_True_49();
		}
	}

	private void Relay_In_215()
	{
		logic_uScript_AddMessage_messageData_215 = msg03AttachFilter;
		logic_uScript_AddMessage_speaker_215 = messageSpeaker;
		logic_uScript_AddMessage_Return_215 = logic_uScript_AddMessage_uScript_AddMessage_215.In(logic_uScript_AddMessage_messageData_215, logic_uScript_AddMessage_speaker_215);
		if (logic_uScript_AddMessage_uScript_AddMessage_215.Out)
		{
			Relay_In_29();
		}
	}

	private void Relay_In_218()
	{
		logic_uScript_AddMessage_messageData_218 = msg04FilterAttached;
		logic_uScript_AddMessage_speaker_218 = messageSpeaker;
		logic_uScript_AddMessage_Return_218 = logic_uScript_AddMessage_uScript_AddMessage_218.In(logic_uScript_AddMessage_messageData_218, logic_uScript_AddMessage_speaker_218);
		if (logic_uScript_AddMessage_uScript_AddMessage_218.Shown)
		{
			Relay_True_53();
		}
	}

	private void Relay_In_222()
	{
		logic_uScript_AddMessage_messageData_222 = msg06FilterSet;
		logic_uScript_AddMessage_speaker_222 = messageSpeaker;
		logic_uScript_AddMessage_Return_222 = logic_uScript_AddMessage_uScript_AddMessage_222.In(logic_uScript_AddMessage_messageData_222, logic_uScript_AddMessage_speaker_222);
		if (logic_uScript_AddMessage_uScript_AddMessage_222.Shown)
		{
			Relay_True_112();
		}
	}

	private void Relay_In_226()
	{
		logic_uScript_AddMessage_messageData_226 = msg07GeneratorSpawned;
		logic_uScript_AddMessage_speaker_226 = messageSpeaker;
		logic_uScript_AddMessage_Return_226 = logic_uScript_AddMessage_uScript_AddMessage_226.In(logic_uScript_AddMessage_messageData_226, logic_uScript_AddMessage_speaker_226);
		if (logic_uScript_AddMessage_uScript_AddMessage_226.Shown)
		{
			Relay_True_93();
		}
	}

	private void Relay_In_229()
	{
		logic_uScript_AddMessage_messageData_229 = msg08AttachGenerator;
		logic_uScript_AddMessage_speaker_229 = messageSpeaker;
		logic_uScript_AddMessage_Return_229 = logic_uScript_AddMessage_uScript_AddMessage_229.In(logic_uScript_AddMessage_messageData_229, logic_uScript_AddMessage_speaker_229);
		if (logic_uScript_AddMessage_uScript_AddMessage_229.Out)
		{
			Relay_In_42();
		}
	}

	private void Relay_In_232()
	{
		logic_uScript_AddMessage_messageData_232 = msg09GeneratorAttached;
		logic_uScript_AddMessage_speaker_232 = messageSpeaker;
		logic_uScript_AddMessage_Return_232 = logic_uScript_AddMessage_uScript_AddMessage_232.In(logic_uScript_AddMessage_messageData_232, logic_uScript_AddMessage_speaker_232);
		if (logic_uScript_AddMessage_uScript_AddMessage_232.Shown)
		{
			Relay_True_107();
		}
	}

	private void Relay_In_235()
	{
		logic_uScript_AddMessage_messageData_235 = msg10AttachReceiver;
		logic_uScript_AddMessage_speaker_235 = messageSpeaker;
		logic_uScript_AddMessage_Return_235 = logic_uScript_AddMessage_uScript_AddMessage_235.In(logic_uScript_AddMessage_messageData_235, logic_uScript_AddMessage_speaker_235);
		if (logic_uScript_AddMessage_uScript_AddMessage_235.Out)
		{
			Relay_In_33();
		}
	}

	private void Relay_In_238()
	{
		logic_uScript_AddMessage_messageData_238 = msg11ReceiverAttached;
		logic_uScript_AddMessage_speaker_238 = messageSpeaker;
		logic_uScript_AddMessage_Return_238 = logic_uScript_AddMessage_uScript_AddMessage_238.In(logic_uScript_AddMessage_messageData_238, logic_uScript_AddMessage_speaker_238);
		if (logic_uScript_AddMessage_uScript_AddMessage_238.Shown)
		{
			Relay_True_151();
		}
	}

	private void Relay_In_242()
	{
		logic_uScript_AddMessage_messageData_242 = msg12FilterExplanation;
		logic_uScript_AddMessage_speaker_242 = messageSpeaker;
		logic_uScript_AddMessage_Return_242 = logic_uScript_AddMessage_uScript_AddMessage_242.In(logic_uScript_AddMessage_messageData_242, logic_uScript_AddMessage_speaker_242);
		if (logic_uScript_AddMessage_uScript_AddMessage_242.Shown)
		{
			Relay_True_104();
		}
	}

	private void Relay_In_245()
	{
		logic_uScript_AddMessage_messageData_245 = msg13FuelExplanation;
		logic_uScript_AddMessage_speaker_245 = messageSpeaker;
		logic_uScript_AddMessage_Return_245 = logic_uScript_AddMessage_uScript_AddMessage_245.In(logic_uScript_AddMessage_messageData_245, logic_uScript_AddMessage_speaker_245);
		if (logic_uScript_AddMessage_uScript_AddMessage_245.Shown)
		{
			Relay_True_13();
		}
	}

	private void Relay_In_251()
	{
		logic_uScript_AddMessage_messageData_251 = msg16FilterSetToFuel;
		logic_uScript_AddMessage_speaker_251 = messageSpeaker;
		logic_uScript_AddMessage_Return_251 = logic_uScript_AddMessage_uScript_AddMessage_251.In(logic_uScript_AddMessage_messageData_251, logic_uScript_AddMessage_speaker_251);
		if (logic_uScript_AddMessage_uScript_AddMessage_251.Shown)
		{
			Relay_True_160();
		}
	}

	private void Relay_In_254()
	{
		logic_uScript_AddMessage_messageData_254 = msg17Complete;
		logic_uScript_AddMessage_speaker_254 = messageSpeaker;
		logic_uScript_AddMessage_Return_254 = logic_uScript_AddMessage_uScript_AddMessage_254.In(logic_uScript_AddMessage_messageData_254, logic_uScript_AddMessage_speaker_254);
		if (logic_uScript_AddMessage_uScript_AddMessage_254.Shown)
		{
			Relay_In_281();
		}
	}

	private void Relay_In_255()
	{
		logic_uScriptCon_CompareBool_Bool_255 = local_NearBase_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_255.In(logic_uScriptCon_CompareBool_Bool_255);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_255.True)
		{
			Relay_In_256();
		}
	}

	private void Relay_In_256()
	{
		logic_uScript_AddMessage_messageData_256 = msgLeavingMissionArea;
		logic_uScript_AddMessage_speaker_256 = messageSpeaker;
		logic_uScript_AddMessage_Return_256 = logic_uScript_AddMessage_uScript_AddMessage_256.In(logic_uScript_AddMessage_messageData_256, logic_uScript_AddMessage_speaker_256);
		if (logic_uScript_AddMessage_uScript_AddMessage_256.Out)
		{
			Relay_False_261();
		}
	}

	private void Relay_True_261()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_261.True(out logic_uScriptAct_SetBool_Target_261);
		local_NearBase_System_Boolean = logic_uScriptAct_SetBool_Target_261;
	}

	private void Relay_False_261()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_261.False(out logic_uScriptAct_SetBool_Target_261);
		local_NearBase_System_Boolean = logic_uScriptAct_SetBool_Target_261;
	}

	private void Relay_True_269()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_269.True(out logic_uScriptAct_SetBool_Target_269);
		local_FilterMenuOpened_System_Boolean = logic_uScriptAct_SetBool_Target_269;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_269.Out)
		{
			Relay_In_350();
		}
	}

	private void Relay_False_269()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_269.False(out logic_uScriptAct_SetBool_Target_269);
		local_FilterMenuOpened_System_Boolean = logic_uScriptAct_SetBool_Target_269;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_269.Out)
		{
			Relay_In_350();
		}
	}

	private void Relay_In_270()
	{
		logic_uScriptCon_CompareBool_Bool_270 = local_FilterMenuOpened_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_270.In(logic_uScriptCon_CompareBool_Bool_270);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_270.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_270.False;
		if (num)
		{
			Relay_In_272();
		}
		if (flag)
		{
			Relay_In_275();
		}
	}

	private void Relay_In_272()
	{
		logic_uScript_HideArrow_uScript_HideArrow_272.In();
		if (logic_uScript_HideArrow_uScript_HideArrow_272.Out)
		{
			Relay_In_339();
		}
	}

	private void Relay_True_274()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_274.True(out logic_uScriptAct_SetBool_Target_274);
		local_FilterMenuOpened_System_Boolean = logic_uScriptAct_SetBool_Target_274;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_274.Out)
		{
			Relay_In_329();
		}
	}

	private void Relay_False_274()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_274.False(out logic_uScriptAct_SetBool_Target_274);
		local_FilterMenuOpened_System_Boolean = logic_uScriptAct_SetBool_Target_274;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_274.Out)
		{
			Relay_In_329();
		}
	}

	private void Relay_In_275()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_275.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_275.Out)
		{
			Relay_In_329();
		}
	}

	private void Relay_Out_281()
	{
		Relay_In_369();
	}

	private void Relay_In_281()
	{
		logic_SubGraph_Crafting_Tutorial_Finish_craftingBaseTech_281 = local_CraftingBaseTech_Tank;
		logic_SubGraph_Crafting_Tutorial_Finish_NPCTech_281 = local_NPCTech_Tank;
		logic_SubGraph_Crafting_Tutorial_Finish_NPCFlyAwayAI_281 = NPCFlyAwayAI;
		logic_SubGraph_Crafting_Tutorial_Finish_NPCDespawnParticleEffect_281 = NPCDespawnParticleEffect;
		logic_SubGraph_Crafting_Tutorial_Finish_SubGraph_Crafting_Tutorial_Finish_281.In(logic_SubGraph_Crafting_Tutorial_Finish_craftingBaseTech_281, logic_SubGraph_Crafting_Tutorial_Finish_NPCTech_281, logic_SubGraph_Crafting_Tutorial_Finish_NPCFlyAwayAI_281, logic_SubGraph_Crafting_Tutorial_Finish_NPCDespawnParticleEffect_281);
	}

	private void Relay_Out_287()
	{
		Relay_In_39();
	}

	private void Relay_In_287()
	{
		int num = 0;
		Array array = baseSpawnData;
		if (logic_SubGraph_Crafting_Tutorial_Init_baseSpawnData_287.Length != num + array.Length)
		{
			Array.Resize(ref logic_SubGraph_Crafting_Tutorial_Init_baseSpawnData_287, num + array.Length);
		}
		Array.Copy(array, 0, logic_SubGraph_Crafting_Tutorial_Init_baseSpawnData_287, num, array.Length);
		num += array.Length;
		int num2 = 0;
		Array array2 = blockSpawnData;
		if (logic_SubGraph_Crafting_Tutorial_Init_blockSpawnData_287.Length != num2 + array2.Length)
		{
			Array.Resize(ref logic_SubGraph_Crafting_Tutorial_Init_blockSpawnData_287, num2 + array2.Length);
		}
		Array.Copy(array2, 0, logic_SubGraph_Crafting_Tutorial_Init_blockSpawnData_287, num2, array2.Length);
		num2 += array2.Length;
		int num3 = 0;
		Array nPCSpawnData = NPCSpawnData;
		if (logic_SubGraph_Crafting_Tutorial_Init_NPCSpawnData_287.Length != num3 + nPCSpawnData.Length)
		{
			Array.Resize(ref logic_SubGraph_Crafting_Tutorial_Init_NPCSpawnData_287, num3 + nPCSpawnData.Length);
		}
		Array.Copy(nPCSpawnData, 0, logic_SubGraph_Crafting_Tutorial_Init_NPCSpawnData_287, num3, nPCSpawnData.Length);
		num3 += nPCSpawnData.Length;
		logic_SubGraph_Crafting_Tutorial_Init_completedBasePreset_287 = completedBasePreset;
		logic_SubGraph_Crafting_Tutorial_Init_basePosition_287 = basePosition;
		logic_SubGraph_Crafting_Tutorial_Init_clearSceneryRadius_287 = clearSceneryRadius;
		logic_SubGraph_Crafting_Tutorial_Init_CraftingBaseTech_287 = local_CraftingBaseTech_Tank;
		logic_SubGraph_Crafting_Tutorial_Init_NPCTech_287 = local_NPCTech_Tank;
		logic_SubGraph_Crafting_Tutorial_Init_SubGraph_Crafting_Tutorial_Init_287.In(logic_SubGraph_Crafting_Tutorial_Init_baseSpawnData_287, logic_SubGraph_Crafting_Tutorial_Init_blockSpawnData_287, logic_SubGraph_Crafting_Tutorial_Init_NPCSpawnData_287, logic_SubGraph_Crafting_Tutorial_Init_completedBasePreset_287, logic_SubGraph_Crafting_Tutorial_Init_useNPCAsTutorialTech_287, logic_SubGraph_Crafting_Tutorial_Init_spawnBase_287, logic_SubGraph_Crafting_Tutorial_Init_basePosition_287, logic_SubGraph_Crafting_Tutorial_Init_clearSceneryRadius_287, ref logic_SubGraph_Crafting_Tutorial_Init_CraftingBaseTech_287, ref logic_SubGraph_Crafting_Tutorial_Init_NPCTech_287);
	}

	private void Relay_In_293()
	{
		logic_uScript_SetEncounterTarget_owner_293 = owner_Connection_292;
		logic_uScript_SetEncounterTarget_visibleObject_293 = local_NPCTech_Tank;
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_293.In(logic_uScript_SetEncounterTarget_owner_293, logic_uScript_SetEncounterTarget_visibleObject_293);
		if (logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_293.Out)
		{
			Relay_In_298();
		}
	}

	private void Relay_In_294()
	{
		logic_uScript_SetBatteryChargeAmount_tech_294 = local_CraftingBaseTech_Tank;
		logic_uScript_SetBatteryChargeAmount_uScript_SetBatteryChargeAmount_294.In(logic_uScript_SetBatteryChargeAmount_tech_294, logic_uScript_SetBatteryChargeAmount_chargeAmount_294);
		if (logic_uScript_SetBatteryChargeAmount_uScript_SetBatteryChargeAmount_294.Out)
		{
			Relay_In_132();
		}
	}

	private void Relay_Save_Out_297()
	{
		Relay_Save_11();
	}

	private void Relay_Load_Out_297()
	{
		Relay_Load_11();
	}

	private void Relay_Restart_Out_297()
	{
		Relay_Set_False_11();
	}

	private void Relay_Save_297()
	{
		logic_SubGraph_SaveLoadBool_boolean_297 = local_BatteriesDrained_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_297 = local_BatteriesDrained_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_297.Save(ref logic_SubGraph_SaveLoadBool_boolean_297, logic_SubGraph_SaveLoadBool_boolAsVariable_297, logic_SubGraph_SaveLoadBool_uniqueID_297);
	}

	private void Relay_Load_297()
	{
		logic_SubGraph_SaveLoadBool_boolean_297 = local_BatteriesDrained_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_297 = local_BatteriesDrained_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_297.Load(ref logic_SubGraph_SaveLoadBool_boolean_297, logic_SubGraph_SaveLoadBool_boolAsVariable_297, logic_SubGraph_SaveLoadBool_uniqueID_297);
	}

	private void Relay_Set_True_297()
	{
		logic_SubGraph_SaveLoadBool_boolean_297 = local_BatteriesDrained_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_297 = local_BatteriesDrained_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_297.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_297, logic_SubGraph_SaveLoadBool_boolAsVariable_297, logic_SubGraph_SaveLoadBool_uniqueID_297);
	}

	private void Relay_Set_False_297()
	{
		logic_SubGraph_SaveLoadBool_boolean_297 = local_BatteriesDrained_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_297 = local_BatteriesDrained_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_297.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_297, logic_SubGraph_SaveLoadBool_boolAsVariable_297, logic_SubGraph_SaveLoadBool_uniqueID_297);
	}

	private void Relay_In_298()
	{
		logic_uScriptCon_CompareBool_Bool_298 = local_BatteriesDrained_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_298.In(logic_uScriptCon_CompareBool_Bool_298);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_298.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_298.False;
		if (num)
		{
			Relay_In_132();
		}
		if (flag)
		{
			Relay_True_300();
		}
	}

	private void Relay_True_300()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_300.True(out logic_uScriptAct_SetBool_Target_300);
		local_BatteriesDrained_System_Boolean = logic_uScriptAct_SetBool_Target_300;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_300.Out)
		{
			Relay_In_294();
		}
	}

	private void Relay_False_300()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_300.False(out logic_uScriptAct_SetBool_Target_300);
		local_BatteriesDrained_System_Boolean = logic_uScriptAct_SetBool_Target_300;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_300.Out)
		{
			Relay_In_294();
		}
	}

	private void Relay_Save_Out_302()
	{
		Relay_Save_52();
	}

	private void Relay_Load_Out_302()
	{
		Relay_Load_52();
	}

	private void Relay_Restart_Out_302()
	{
		Relay_Set_False_52();
	}

	private void Relay_Save_302()
	{
		logic_SubGraph_SaveLoadBool_boolean_302 = local_msgIntroShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_302 = local_msgIntroShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_302.Save(ref logic_SubGraph_SaveLoadBool_boolean_302, logic_SubGraph_SaveLoadBool_boolAsVariable_302, logic_SubGraph_SaveLoadBool_uniqueID_302);
	}

	private void Relay_Load_302()
	{
		logic_SubGraph_SaveLoadBool_boolean_302 = local_msgIntroShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_302 = local_msgIntroShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_302.Load(ref logic_SubGraph_SaveLoadBool_boolean_302, logic_SubGraph_SaveLoadBool_boolAsVariable_302, logic_SubGraph_SaveLoadBool_uniqueID_302);
	}

	private void Relay_Set_True_302()
	{
		logic_SubGraph_SaveLoadBool_boolean_302 = local_msgIntroShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_302 = local_msgIntroShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_302.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_302, logic_SubGraph_SaveLoadBool_boolAsVariable_302, logic_SubGraph_SaveLoadBool_uniqueID_302);
	}

	private void Relay_Set_False_302()
	{
		logic_SubGraph_SaveLoadBool_boolean_302 = local_msgIntroShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_302 = local_msgIntroShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_302.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_302, logic_SubGraph_SaveLoadBool_boolAsVariable_302, logic_SubGraph_SaveLoadBool_uniqueID_302);
	}

	private void Relay_In_303()
	{
		logic_uScript_SpawnResourceListOnHolder_tech_303 = local_CraftingBaseTech_Tank;
		int num = 0;
		Array array = resourceListWoods;
		if (logic_uScript_SpawnResourceListOnHolder_chunks_303.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SpawnResourceListOnHolder_chunks_303, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SpawnResourceListOnHolder_chunks_303, num, array.Length);
		num += array.Length;
		logic_uScript_SpawnResourceListOnHolder_uScript_SpawnResourceListOnHolder_303.In(logic_uScript_SpawnResourceListOnHolder_tech_303, logic_uScript_SpawnResourceListOnHolder_chunks_303, logic_uScript_SpawnResourceListOnHolder_blockType_303);
		if (logic_uScript_SpawnResourceListOnHolder_uScript_SpawnResourceListOnHolder_303.Out)
		{
			Relay_In_14();
		}
	}

	private void Relay_In_306()
	{
		logic_uScript_SpawnResourceListOnHolder_tech_306 = local_CraftingBaseTech_Tank;
		int num = 0;
		Array array = resourceListFuels;
		if (logic_uScript_SpawnResourceListOnHolder_chunks_306.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SpawnResourceListOnHolder_chunks_306, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SpawnResourceListOnHolder_chunks_306, num, array.Length);
		num += array.Length;
		logic_uScript_SpawnResourceListOnHolder_uScript_SpawnResourceListOnHolder_306.In(logic_uScript_SpawnResourceListOnHolder_tech_306, logic_uScript_SpawnResourceListOnHolder_chunks_306, logic_uScript_SpawnResourceListOnHolder_blockType_306);
		if (logic_uScript_SpawnResourceListOnHolder_uScript_SpawnResourceListOnHolder_306.Out)
		{
			Relay_In_78();
		}
	}

	private void Relay_In_309()
	{
		logic_uScript_RestrictItemPickup_tech_309 = local_CraftingBaseTech_Tank;
		logic_uScript_RestrictItemPickup_uScript_RestrictItemPickup_309.In(logic_uScript_RestrictItemPickup_tech_309, logic_uScript_RestrictItemPickup_typesToAccept_309);
		if (logic_uScript_RestrictItemPickup_uScript_RestrictItemPickup_309.Out)
		{
			Relay_In_293();
		}
	}

	private void Relay_In_312()
	{
		logic_uScriptCon_CompareBool_Bool_312 = local_GeneratorAttached_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_312.In(logic_uScriptCon_CompareBool_Bool_312);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_312.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_312.False;
		if (num)
		{
			Relay_In_145();
		}
		if (flag)
		{
			Relay_In_114();
		}
	}

	private void Relay_True_314()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_314.True(out logic_uScriptAct_SetBool_Target_314);
		local_GeneratorAttached_System_Boolean = logic_uScriptAct_SetBool_Target_314;
	}

	private void Relay_False_314()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_314.False(out logic_uScriptAct_SetBool_Target_314);
		local_GeneratorAttached_System_Boolean = logic_uScriptAct_SetBool_Target_314;
	}

	private void Relay_Out_316()
	{
	}

	private void Relay_In_316()
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_316 = local_Stage_System_Int32;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_316.In(ref logic_SubGraph_CompleteObjectiveStage_objectiveStage_316, logic_SubGraph_CompleteObjectiveStage_isFinalObjective_316);
	}

	private void Relay_Save_Out_317()
	{
		Relay_Save_167();
	}

	private void Relay_Load_Out_317()
	{
		Relay_Load_167();
	}

	private void Relay_Restart_Out_317()
	{
		Relay_Set_False_167();
	}

	private void Relay_Save_317()
	{
		logic_SubGraph_SaveLoadBool_boolean_317 = local_GeneratorAttached_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_317 = local_GeneratorAttached_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_317.Save(ref logic_SubGraph_SaveLoadBool_boolean_317, logic_SubGraph_SaveLoadBool_boolAsVariable_317, logic_SubGraph_SaveLoadBool_uniqueID_317);
	}

	private void Relay_Load_317()
	{
		logic_SubGraph_SaveLoadBool_boolean_317 = local_GeneratorAttached_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_317 = local_GeneratorAttached_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_317.Load(ref logic_SubGraph_SaveLoadBool_boolean_317, logic_SubGraph_SaveLoadBool_boolAsVariable_317, logic_SubGraph_SaveLoadBool_uniqueID_317);
	}

	private void Relay_Set_True_317()
	{
		logic_SubGraph_SaveLoadBool_boolean_317 = local_GeneratorAttached_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_317 = local_GeneratorAttached_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_317.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_317, logic_SubGraph_SaveLoadBool_boolAsVariable_317, logic_SubGraph_SaveLoadBool_uniqueID_317);
	}

	private void Relay_Set_False_317()
	{
		logic_SubGraph_SaveLoadBool_boolean_317 = local_GeneratorAttached_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_317 = local_GeneratorAttached_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_317.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_317, logic_SubGraph_SaveLoadBool_boolAsVariable_317, logic_SubGraph_SaveLoadBool_uniqueID_317);
	}

	private void Relay_In_321()
	{
		logic_uScript_AddMessage_messageData_321 = msg07aFeedGenerator;
		logic_uScript_AddMessage_speaker_321 = messageSpeaker;
		logic_uScript_AddMessage_Return_321 = logic_uScript_AddMessage_uScript_AddMessage_321.In(logic_uScript_AddMessage_messageData_321, logic_uScript_AddMessage_speaker_321);
	}

	private void Relay_In_322()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_322 = messageTag;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_322.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_322, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_322);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_322.Out)
		{
			Relay_In_316();
		}
	}

	private void Relay_In_324()
	{
		logic_uScript_Wait_uScript_Wait_324.In(logic_uScript_Wait_seconds_324, logic_uScript_Wait_repeat_324);
		if (logic_uScript_Wait_uScript_Wait_324.Waited)
		{
			Relay_In_321();
		}
	}

	private void Relay_In_325()
	{
		logic_uScript_HideHUDElement_uScript_HideHUDElement_325.In(logic_uScript_HideHUDElement_hudElement_325);
		if (logic_uScript_HideHUDElement_uScript_HideHUDElement_325.Out)
		{
			Relay_True_269();
		}
	}

	private void Relay_Out_326()
	{
		Relay_In_126();
	}

	private void Relay_Shown_326()
	{
	}

	private void Relay_In_326()
	{
		logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboard_326 = msg05SetFilter;
		logic_SubGraph_AddMessageWithPadSupport_messageControlPad_326 = msg05SetFilter_Pad;
		logic_SubGraph_AddMessageWithPadSupport_speaker_326 = messageSpeaker;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_326.In(logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboard_326, logic_SubGraph_AddMessageWithPadSupport_messageControlPad_326, logic_SubGraph_AddMessageWithPadSupport_speaker_326);
	}

	private void Relay_Out_329()
	{
		Relay_In_367();
	}

	private void Relay_Shown_329()
	{
	}

	private void Relay_In_329()
	{
		logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboard_329 = msg15SelectFuelChunks;
		logic_SubGraph_AddMessageWithPadSupport_messageControlPad_329 = msg15SelectFuelChunks_Pad;
		logic_SubGraph_AddMessageWithPadSupport_speaker_329 = messageSpeaker;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_329.In(logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboard_329, logic_SubGraph_AddMessageWithPadSupport_messageControlPad_329, logic_SubGraph_AddMessageWithPadSupport_speaker_329);
	}

	private void Relay_In_332()
	{
		logic_uScript_EnableGlow_targetObject_332 = local_FilterBlock_TankBlock;
		logic_uScript_EnableGlow_uScript_EnableGlow_332.In(logic_uScript_EnableGlow_targetObject_332, logic_uScript_EnableGlow_enable_332);
		if (logic_uScript_EnableGlow_uScript_EnableGlow_332.Out)
		{
			Relay_In_27();
		}
	}

	private void Relay_In_335()
	{
		logic_uScript_EnableGlow_targetObject_335 = local_FilterBlock_TankBlock;
		logic_uScript_EnableGlow_uScript_EnableGlow_335.In(logic_uScript_EnableGlow_targetObject_335, logic_uScript_EnableGlow_enable_335);
		if (logic_uScript_EnableGlow_uScript_EnableGlow_335.Out)
		{
			Relay_In_205();
		}
	}

	private void Relay_In_337()
	{
		logic_uScript_EnableGlow_targetObject_337 = local_FilterBlock_TankBlock;
		logic_uScript_EnableGlow_uScript_EnableGlow_337.In(logic_uScript_EnableGlow_targetObject_337, logic_uScript_EnableGlow_enable_337);
	}

	private void Relay_In_339()
	{
		logic_uScript_EnableGlow_targetObject_339 = local_FilterBlock_TankBlock;
		logic_uScript_EnableGlow_uScript_EnableGlow_339.In(logic_uScript_EnableGlow_targetObject_339, logic_uScript_EnableGlow_enable_339);
		if (logic_uScript_EnableGlow_uScript_EnableGlow_339.Out)
		{
			Relay_False_274();
		}
	}

	private void Relay_In_340()
	{
		logic_uScript_EnableGlow_targetObject_340 = local_FilterBlock_TankBlock;
		logic_uScript_EnableGlow_uScript_EnableGlow_340.In(logic_uScript_EnableGlow_targetObject_340, logic_uScript_EnableGlow_enable_340);
		if (logic_uScript_EnableGlow_uScript_EnableGlow_340.Out)
		{
			Relay_In_343();
		}
	}

	private void Relay_In_343()
	{
		logic_uScript_EnableGlow_targetObject_343 = local_ReceiverBlock_TankBlock;
		logic_uScript_EnableGlow_uScript_EnableGlow_343.In(logic_uScript_EnableGlow_targetObject_343, logic_uScript_EnableGlow_enable_343);
		if (logic_uScript_EnableGlow_uScript_EnableGlow_343.Out)
		{
			Relay_In_345();
		}
	}

	private void Relay_In_345()
	{
		logic_uScriptCon_CompareBool_Bool_345 = local_GeneratorSpawned_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_345.In(logic_uScriptCon_CompareBool_Bool_345);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_345.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_345.False;
		if (num)
		{
			Relay_In_346();
		}
		if (flag)
		{
			Relay_In_348();
		}
	}

	private void Relay_In_346()
	{
		logic_uScript_EnableGlow_targetObject_346 = local_GeneratorBlock_TankBlock;
		logic_uScript_EnableGlow_uScript_EnableGlow_346.In(logic_uScript_EnableGlow_targetObject_346, logic_uScript_EnableGlow_enable_346);
		if (logic_uScript_EnableGlow_uScript_EnableGlow_346.Out)
		{
			Relay_In_255();
		}
	}

	private void Relay_In_348()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_348.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_348.Out)
		{
			Relay_In_255();
		}
	}

	private void Relay_In_349()
	{
		logic_uScript_AddMessage_messageData_349 = msg14OpenFilterMenu;
		logic_uScript_AddMessage_speaker_349 = messageSpeaker;
		logic_uScript_AddMessage_Return_349 = logic_uScript_AddMessage_uScript_AddMessage_349.In(logic_uScript_AddMessage_messageData_349, logic_uScript_AddMessage_speaker_349);
		if (logic_uScript_AddMessage_uScript_AddMessage_349.Out)
		{
			Relay_In_360();
		}
	}

	private void Relay_In_350()
	{
		logic_uScript_GetJoypadControlMode_uScript_GetJoypadControlMode_350.In();
		bool joypad = logic_uScript_GetJoypadControlMode_uScript_GetJoypadControlMode_350.Joypad;
		bool mouseAndKeyboard = logic_uScript_GetJoypadControlMode_uScript_GetJoypadControlMode_350.MouseAndKeyboard;
		if (joypad)
		{
			Relay_In_353();
		}
		if (mouseAndKeyboard)
		{
			Relay_In_349();
		}
	}

	private void Relay_In_353()
	{
		logic_uScript_IsHUDElementVisible_uScript_IsHUDElementVisible_353.In(logic_uScript_IsHUDElementVisible_hudElement_353);
		bool num = logic_uScript_IsHUDElementVisible_uScript_IsHUDElementVisible_353.True;
		bool flag = logic_uScript_IsHUDElementVisible_uScript_IsHUDElementVisible_353.False;
		if (num)
		{
			Relay_In_358();
		}
		if (flag)
		{
			Relay_In_354();
		}
	}

	private void Relay_In_354()
	{
		logic_uScript_AddMessage_messageData_354 = msg14OpenFilterMenu_Pad1;
		logic_uScript_AddMessage_speaker_354 = messageSpeaker;
		logic_uScript_AddMessage_Return_354 = logic_uScript_AddMessage_uScript_AddMessage_354.In(logic_uScript_AddMessage_messageData_354, logic_uScript_AddMessage_speaker_354);
		if (logic_uScript_AddMessage_uScript_AddMessage_354.Out)
		{
			Relay_In_363();
		}
	}

	private void Relay_In_358()
	{
		logic_uScript_AddMessage_messageData_358 = msg14OpenFilterMenu_Pad2;
		logic_uScript_AddMessage_speaker_358 = messageSpeaker;
		logic_uScript_AddMessage_Return_358 = logic_uScript_AddMessage_uScript_AddMessage_358.In(logic_uScript_AddMessage_messageData_358, logic_uScript_AddMessage_speaker_358);
		if (logic_uScript_AddMessage_uScript_AddMessage_358.Out)
		{
			Relay_In_73();
		}
	}

	private void Relay_In_360()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_360.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_360.Out)
		{
			Relay_In_73();
		}
	}

	private void Relay_In_362()
	{
		logic_uScript_EnableGlow_targetObject_362 = local_FilterBlock_TankBlock;
		logic_uScript_EnableGlow_uScript_EnableGlow_362.In(logic_uScript_EnableGlow_targetObject_362, logic_uScript_EnableGlow_enable_362);
	}

	private void Relay_In_363()
	{
		logic_uScript_HideArrow_uScript_HideArrow_363.In();
		if (logic_uScript_HideArrow_uScript_HideArrow_363.Out)
		{
			Relay_In_362();
		}
	}

	private void Relay_In_364()
	{
		logic_uScript_FilterTutorialHighlightMode_uScript_FilterTutorialHighlightMode_364.In(logic_uScript_FilterTutorialHighlightMode_filterMode_364);
	}

	private void Relay_In_365()
	{
		logic_uScript_FilterTutorialClearHighlight_uScript_FilterTutorialClearHighlight_365.In();
	}

	private void Relay_In_366()
	{
		logic_uScript_FilterTutorialHighlightCategory_uScript_FilterTutorialHighlightCategory_366.In(logic_uScript_FilterTutorialHighlightCategory_chunkCategory_366);
	}

	private void Relay_In_367()
	{
		logic_uScript_CheckFilterUISpecificGroupMenuVisible_uScript_CheckFilterUISpecificGroupMenuVisible_367.In();
		bool visible = logic_uScript_CheckFilterUISpecificGroupMenuVisible_uScript_CheckFilterUISpecificGroupMenuVisible_367.Visible;
		bool notVisible = logic_uScript_CheckFilterUISpecificGroupMenuVisible_uScript_CheckFilterUISpecificGroupMenuVisible_367.NotVisible;
		if (visible)
		{
			Relay_In_366();
		}
		if (notVisible)
		{
			Relay_In_364();
		}
	}

	private void Relay_In_368()
	{
		logic_uScript_SendAnaliticsEvent_uScript_SendAnaliticsEvent_368.In(logic_uScript_SendAnaliticsEvent_analiticsEvent_368, logic_uScript_SendAnaliticsEvent_parameterName_368, logic_uScript_SendAnaliticsEvent_parameter_368);
	}

	private void Relay_In_369()
	{
		logic_uScript_SendAnaliticsEvent_uScript_SendAnaliticsEvent_369.In(logic_uScript_SendAnaliticsEvent_analiticsEvent_369, logic_uScript_SendAnaliticsEvent_parameterName_369, logic_uScript_SendAnaliticsEvent_parameter_369);
	}
}
