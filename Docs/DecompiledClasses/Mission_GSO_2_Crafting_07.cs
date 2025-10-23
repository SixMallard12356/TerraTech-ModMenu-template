using System;
using BehaviorDesigner.Runtime;
using UnityEngine;

[Serializable]
[NodePath("Graphs")]
[FriendlyName("Untitled", "")]
public class Mission_GSO_2_Crafting_07 : uScriptLogic
{
	private GameObject parentGameObject;

	private uScript_GUI thisScriptsOnGuiListener;

	private bool m_RegisteredForEvents;

	public ChunkTypes[] baseAllowedResourceTypes = new ChunkTypes[0];

	[Multiline(3)]
	public string basePosition = "";

	public SpawnTechData[] baseSpawnData = new SpawnTechData[0];

	public SpawnBlockData[] blockSpawnData = new SpawnBlockData[0];

	public BlockTypes blockTypeToCraft;

	public ItemTypeInfo blockTypeToHighlight;

	public float clearSceneryRadius;

	public TankPreset completedBasePreset;

	public ItemTypeInfo ComponentTypeToHighlight;

	public float distBaseFound;

	public GhostBlockSpawnData[] ghostBlock = new GhostBlockSpawnData[0];

	private TankBlock[] local_165_TankBlockArray = new TankBlock[0];

	private BlockTypes local_92_BlockTypes;

	private bool local_BlockAttachedToNPC_System_Boolean;

	private bool local_ComponentCrafted_System_Boolean;

	private TankBlock local_ComponentFactoryBlock_TankBlock;

	private ManHUD.HUDElementType local_ComponentMenu_ManHUD_HUDElementType = ManHUD.HUDElementType.ComponentRecipeSelect;

	private bool local_ComponentMenuOpened_System_Boolean;

	private Tank local_CraftingBaseTech_Tank;

	private bool local_CraftingBlockSelected_System_Boolean;

	private int local_CraftingHintMessage_System_Int32 = 1;

	private ManHUD.HUDElementType local_CraftingMenu_ManHUD_HUDElementType = ManHUD.HUDElementType.BlockRecipeSelect;

	private bool local_CraftingMenuOpened_System_Boolean;

	private TankBlock local_FabricatorBlock_TankBlock;

	private TankBlock local_GhostBlock_TankBlock;

	private bool local_GhostBlockSpawned_System_Boolean;

	private bool local_msgBaseFoundShown_System_Boolean;

	private bool local_msgBlockRecipeShown_System_Boolean;

	private bool local_msgComponentBeingCraftedShown_System_Boolean;

	private bool local_msgComponentExplanationShown_System_Boolean;

	private bool local_msgIntroShown_System_Boolean;

	private bool local_msgWorkOutHowToCraftShown_System_Boolean;

	private bool local_NearBase_System_Boolean;

	private Tank local_NPCTech_Tank;

	private bool local_ResourcesSpawned_System_Boolean;

	private int local_Stage_System_Int32 = 1;

	private int local_Stage2Steps_System_Int32 = 1;

	public uScript_AddMessage.MessageSpeaker messageSpeaker;

	[Multiline(3)]
	public string messageTag = "";

	public uScript_AddMessage.MessageData msg01Intro;

	public uScript_AddMessage.MessageData msg02BaseFound;

	public uScript_AddMessage.MessageData msg03OpenFabricatorMenu;

	public uScript_AddMessage.MessageData msg04aSelectBlockToCraft;

	public uScript_AddMessage.MessageData msg04aSelectBlockToCraft_Pad;

	public uScript_AddMessage.MessageData msg04bBlockRecipe;

	public uScript_AddMessage.MessageData msg04cCraftBlock;

	public uScript_AddMessage.MessageData msg04dComponentExplanation;

	public uScript_AddMessage.MessageData msg05OpenComponentMenu;

	public uScript_AddMessage.MessageData msg05OpenComponentMenu_Pad;

	public uScript_AddMessage.MessageData msg06aComponentBeingCrafted;

	public uScript_AddMessage.MessageData msg06bWorkOutHowToCraft;

	public uScript_AddMessage.MessageData msg06CraftComponent;

	public uScript_AddMessage.MessageData msg07AttachBlock;

	public uScript_AddMessage.MessageData msg08Complete;

	public uScript_AddMessage.MessageData msg09Reward;

	public uScript_AddMessage.MessageData msgCraftingHint01;

	public uScript_AddMessage.MessageData msgCraftingHint02;

	public uScript_AddMessage.MessageData msgCraftingHint03;

	public uScript_AddMessage.MessageData msgCraftingHint04;

	public uScript_AddMessage.MessageData msgLeavingMissionArea01;

	public uScript_AddMessage.MessageData msgLeavingMissionArea02;

	public Transform NPCDespawnParticleEffect;

	public ExternalBehaviorTree NPCFlyAwayAI;

	public SpawnTechData[] NPCSpawnData = new SpawnTechData[0];

	public ChunkTypes[] resourceList = new ChunkTypes[0];

	public float TEMP_TimeWaitForComponentCrafted;

	public float timeRepeatCraftingHint;

	private GameObject owner_Connection_1;

	private GameObject owner_Connection_5;

	private GameObject owner_Connection_72;

	private GameObject owner_Connection_98;

	private GameObject owner_Connection_159;

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

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_9 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_9;

	private bool logic_uScriptAct_SetBool_Out_9 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_9 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_9 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_12 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_12;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_12 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_12 = "msgBlockRecipeShown";

	private uScript_PausePopulation logic_uScript_PausePopulation_uScript_PausePopulation_13 = new uScript_PausePopulation();

	private bool logic_uScript_PausePopulation_Out_13 = true;

	private uScript_PausePopulation logic_uScript_PausePopulation_uScript_PausePopulation_15 = new uScript_PausePopulation();

	private bool logic_uScript_PausePopulation_Out_15 = true;

	private uScript_HideArrow logic_uScript_HideArrow_uScript_HideArrow_16 = new uScript_HideArrow();

	private bool logic_uScript_HideArrow_Out_16 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_17 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_17;

	private bool logic_uScriptAct_SetBool_Out_17 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_17 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_17 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_18 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_18 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_18 = true;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_18 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_20 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_20;

	private bool logic_uScriptAct_SetBool_Out_20 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_20 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_20 = true;

	private uScript_IsPlayerInRangeOfTech logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_21 = new uScript_IsPlayerInRangeOfTech();

	private Tank logic_uScript_IsPlayerInRangeOfTech_tech_21;

	private float logic_uScript_IsPlayerInRangeOfTech_range_21 = 100f;

	private Tank[] logic_uScript_IsPlayerInRangeOfTech_techs_21 = new Tank[0];

	private bool logic_uScript_IsPlayerInRangeOfTech_Out_21 = true;

	private bool logic_uScript_IsPlayerInRangeOfTech_InRange_21 = true;

	private bool logic_uScript_IsPlayerInRangeOfTech_OutOfRange_21 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_23 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_23;

	private bool logic_uScriptCon_CompareBool_True_23 = true;

	private bool logic_uScriptCon_CompareBool_False_23 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_25 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_25;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_25 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_25 = "msgIntroShown";

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_28 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_28;

	private bool logic_uScriptAct_SetBool_Out_28 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_28 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_28 = true;

	private uScriptCon_ManualSwitch logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_30 = new uScriptCon_ManualSwitch();

	private int logic_uScriptCon_ManualSwitch_CurrentOutput_30;

	private SubGraph_SaveLoadInt logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_31 = new SubGraph_SaveLoadInt();

	private int logic_SubGraph_SaveLoadInt_restartValue_31 = 1;

	private int logic_SubGraph_SaveLoadInt_integer_31;

	private object logic_SubGraph_SaveLoadInt_intAsVariable_31 = "";

	private string logic_SubGraph_SaveLoadInt_uniqueID_31 = "Stage";

	private SubGraph_LoadObjectiveStates logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_33 = new SubGraph_LoadObjectiveStates();

	private int logic_SubGraph_LoadObjectiveStates_currentObjective_33;

	private SubGraph_CompleteObjectiveStage logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_40 = new SubGraph_CompleteObjectiveStage();

	private int logic_SubGraph_CompleteObjectiveStage_objectiveStage_40;

	private bool logic_SubGraph_CompleteObjectiveStage_isFinalObjective_40;

	private SubGraph_CompleteObjectiveStage logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_42 = new SubGraph_CompleteObjectiveStage();

	private int logic_SubGraph_CompleteObjectiveStage_objectiveStage_42;

	private bool logic_SubGraph_CompleteObjectiveStage_isFinalObjective_42;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_44 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_44;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_44;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_44;

	private bool logic_uScript_AddMessage_Out_44 = true;

	private bool logic_uScript_AddMessage_Shown_44 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_47 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_47;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_47;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_47;

	private bool logic_uScript_AddMessage_Out_47 = true;

	private bool logic_uScript_AddMessage_Shown_47 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_50 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_50;

	private bool logic_uScriptCon_CompareBool_True_50 = true;

	private bool logic_uScriptCon_CompareBool_False_50 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_51 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_51;

	private bool logic_uScriptAct_SetBool_Out_51 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_51 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_51 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_55 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_55;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_55;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_55;

	private bool logic_uScript_AddMessage_Out_55 = true;

	private bool logic_uScript_AddMessage_Shown_55 = true;

	private SubGraph_Crafting_Tutorial_Finish logic_SubGraph_Crafting_Tutorial_Finish_SubGraph_Crafting_Tutorial_Finish_56 = new SubGraph_Crafting_Tutorial_Finish();

	private Tank logic_SubGraph_Crafting_Tutorial_Finish_craftingBaseTech_56;

	private Tank logic_SubGraph_Crafting_Tutorial_Finish_NPCTech_56;

	private ExternalBehaviorTree logic_SubGraph_Crafting_Tutorial_Finish_NPCFlyAwayAI_56;

	private Transform logic_SubGraph_Crafting_Tutorial_Finish_NPCDespawnParticleEffect_56;

	private SubGraph_Crafting_Tutorial_Init logic_SubGraph_Crafting_Tutorial_Init_SubGraph_Crafting_Tutorial_Init_67 = new SubGraph_Crafting_Tutorial_Init();

	private SpawnTechData[] logic_SubGraph_Crafting_Tutorial_Init_baseSpawnData_67 = new SpawnTechData[0];

	private SpawnBlockData[] logic_SubGraph_Crafting_Tutorial_Init_blockSpawnData_67 = new SpawnBlockData[0];

	private SpawnTechData[] logic_SubGraph_Crafting_Tutorial_Init_NPCSpawnData_67 = new SpawnTechData[0];

	private TankPreset logic_SubGraph_Crafting_Tutorial_Init_completedBasePreset_67;

	private bool logic_SubGraph_Crafting_Tutorial_Init_useNPCAsTutorialTech_67 = true;

	private bool logic_SubGraph_Crafting_Tutorial_Init_spawnBase_67 = true;

	private string logic_SubGraph_Crafting_Tutorial_Init_basePosition_67 = "";

	private float logic_SubGraph_Crafting_Tutorial_Init_clearSceneryRadius_67;

	private Tank logic_SubGraph_Crafting_Tutorial_Init_CraftingBaseTech_67;

	private Tank logic_SubGraph_Crafting_Tutorial_Init_NPCTech_67;

	private uScript_SetEncounterTarget logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_73 = new uScript_SetEncounterTarget();

	private GameObject logic_uScript_SetEncounterTarget_owner_73;

	private object logic_uScript_SetEncounterTarget_visibleObject_73 = "";

	private bool logic_uScript_SetEncounterTarget_Out_73 = true;

	private uScript_CompareBlockTypes logic_uScript_CompareBlockTypes_uScript_CompareBlockTypes_75 = new uScript_CompareBlockTypes();

	private BlockTypes logic_uScript_CompareBlockTypes_A_75;

	private BlockTypes logic_uScript_CompareBlockTypes_B_75;

	private bool logic_uScript_CompareBlockTypes_EqualTo_75 = true;

	private bool logic_uScript_CompareBlockTypes_NotEqualTo_75 = true;

	private uScript_HideArrow logic_uScript_HideArrow_uScript_HideArrow_76 = new uScript_HideArrow();

	private bool logic_uScript_HideArrow_Out_76 = true;

	private uScript_LockTechInteraction logic_uScript_LockTechInteraction_uScript_LockTechInteraction_77 = new uScript_LockTechInteraction();

	private Tank logic_uScript_LockTechInteraction_tech_77;

	private BlockTypes[] logic_uScript_LockTechInteraction_excludedBlocks_77 = new BlockTypes[1] { BlockTypes.GSOComponentFactory_323 };

	private TankBlock[] logic_uScript_LockTechInteraction_excludedUniqueBlocks_77 = new TankBlock[0];

	private bool logic_uScript_LockTechInteraction_Out_77 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_78 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_78;

	private bool logic_uScriptCon_CompareBool_True_78 = true;

	private bool logic_uScriptCon_CompareBool_False_78 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_80 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_80;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_80;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_80;

	private bool logic_uScript_AddMessage_Out_80 = true;

	private bool logic_uScript_AddMessage_Shown_80 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_90 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_90;

	private bool logic_uScriptAct_SetBool_Out_90 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_90 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_90 = true;

	private uScript_LockPlayerInput logic_uScript_LockPlayerInput_uScript_LockPlayerInput_91 = new uScript_LockPlayerInput();

	private bool logic_uScript_LockPlayerInput_lockInput_91;

	private bool logic_uScript_LockPlayerInput_includeCamera_91;

	private bool logic_uScript_LockPlayerInput_Out_91 = true;

	private uScript_LockPlayerInput logic_uScript_LockPlayerInput_uScript_LockPlayerInput_93 = new uScript_LockPlayerInput();

	private bool logic_uScript_LockPlayerInput_lockInput_93 = true;

	private bool logic_uScript_LockPlayerInput_includeCamera_93 = true;

	private bool logic_uScript_LockPlayerInput_Out_93 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_94 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_94 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_94 = true;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_94 = true;

	private uScript_LockPause logic_uScript_LockPause_uScript_LockPause_95 = new uScript_LockPause();

	private bool logic_uScript_LockPause_lockPause_95 = true;

	private ManPauseGame.DisablePauseReason logic_uScript_LockPause_disabledReason_95 = ManPauseGame.DisablePauseReason.Tutorial;

	private bool logic_uScript_LockPause_Out_95 = true;

	private uScript_PointArrowAtVisible logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_99 = new uScript_PointArrowAtVisible();

	private object logic_uScript_PointArrowAtVisible_targetObject_99 = "";

	private float logic_uScript_PointArrowAtVisible_timeToShowFor_99 = -1f;

	private Vector3 logic_uScript_PointArrowAtVisible_offset_99 = new Vector3(0f, 0f, 0f);

	private bool logic_uScript_PointArrowAtVisible_Out_99 = true;

	private uScript_LockPause logic_uScript_LockPause_uScript_LockPause_100 = new uScript_LockPause();

	private bool logic_uScript_LockPause_lockPause_100;

	private ManPauseGame.DisablePauseReason logic_uScript_LockPause_disabledReason_100 = ManPauseGame.DisablePauseReason.Tutorial;

	private bool logic_uScript_LockPause_Out_100 = true;

	private SubGraph_CompleteObjectiveStage logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_101 = new SubGraph_CompleteObjectiveStage();

	private int logic_SubGraph_CompleteObjectiveStage_objectiveStage_101;

	private bool logic_SubGraph_CompleteObjectiveStage_isFinalObjective_101;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_103 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_103;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_103;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_103;

	private bool logic_uScript_AddMessage_Out_103 = true;

	private bool logic_uScript_AddMessage_Shown_103 = true;

	private uScript_DoesTechHaveBlockAtPosition logic_uScript_DoesTechHaveBlockAtPosition_uScript_DoesTechHaveBlockAtPosition_106 = new uScript_DoesTechHaveBlockAtPosition();

	private Tank logic_uScript_DoesTechHaveBlockAtPosition_tech_106;

	private BlockTypes logic_uScript_DoesTechHaveBlockAtPosition_blockType_106;

	private Vector3 logic_uScript_DoesTechHaveBlockAtPosition_localPosition_106 = new Vector3(-1f, 0f, 0f);

	private bool logic_uScript_DoesTechHaveBlockAtPosition_True_106 = true;

	private bool logic_uScript_DoesTechHaveBlockAtPosition_False_106 = true;

	private uScriptCon_CompareInt logic_uScriptCon_CompareInt_uScriptCon_CompareInt_109 = new uScriptCon_CompareInt();

	private int logic_uScriptCon_CompareInt_A_109;

	private int logic_uScriptCon_CompareInt_B_109 = 3;

	private bool logic_uScriptCon_CompareInt_GreaterThan_109 = true;

	private bool logic_uScriptCon_CompareInt_GreaterThanOrEqualTo_109 = true;

	private bool logic_uScriptCon_CompareInt_EqualTo_109 = true;

	private bool logic_uScriptCon_CompareInt_NotEqualTo_109 = true;

	private bool logic_uScriptCon_CompareInt_LessThanOrEqualTo_109 = true;

	private bool logic_uScriptCon_CompareInt_LessThan_109 = true;

	private uScript_SetCustomRadarTeamID logic_uScript_SetCustomRadarTeamID_uScript_SetCustomRadarTeamID_111 = new uScript_SetCustomRadarTeamID();

	private Tank logic_uScript_SetCustomRadarTeamID_tech_111;

	private int logic_uScript_SetCustomRadarTeamID_radarTeamID_111 = -2;

	private bool logic_uScript_SetCustomRadarTeamID_Out_111 = true;

	private uScript_LockTech logic_uScript_LockTech_uScript_LockTech_112 = new uScript_LockTech();

	private Tank logic_uScript_LockTech_tech_112;

	private uScript_LockTech.TechLockType logic_uScript_LockTech_lockType_112 = uScript_LockTech.TechLockType.LockDetachAndInteraction;

	private bool logic_uScript_LockTech_Out_112 = true;

	private uScript_SetTankTeam logic_uScript_SetTankTeam_uScript_SetTankTeam_113 = new uScript_SetTankTeam();

	private Tank logic_uScript_SetTankTeam_tank_113;

	private int logic_uScript_SetTankTeam_team_113;

	private bool logic_uScript_SetTankTeam_Out_113 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_119 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_119;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_119;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_119;

	private bool logic_uScript_AddMessage_Out_119 = true;

	private bool logic_uScript_AddMessage_Shown_119 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_121 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_121;

	private bool logic_uScriptAct_SetBool_Out_121 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_121 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_121 = true;

	private uScript_LockTechInteraction logic_uScript_LockTechInteraction_uScript_LockTechInteraction_122 = new uScript_LockTechInteraction();

	private Tank logic_uScript_LockTechInteraction_tech_122;

	private BlockTypes[] logic_uScript_LockTechInteraction_excludedBlocks_122 = new BlockTypes[1] { BlockTypes.GSOFabricator_322 };

	private TankBlock[] logic_uScript_LockTechInteraction_excludedUniqueBlocks_122 = new TankBlock[0];

	private bool logic_uScript_LockTechInteraction_Out_122 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_125 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_125;

	private bool logic_uScriptCon_CompareBool_True_125 = true;

	private bool logic_uScriptCon_CompareBool_False_125 = true;

	private uScript_PointArrowAtVisible logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_126 = new uScript_PointArrowAtVisible();

	private object logic_uScript_PointArrowAtVisible_targetObject_126 = "";

	private float logic_uScript_PointArrowAtVisible_timeToShowFor_126 = -1f;

	private Vector3 logic_uScript_PointArrowAtVisible_offset_126 = new Vector3(0f, 0f, 0f);

	private bool logic_uScript_PointArrowAtVisible_Out_126 = true;

	private uScript_HideArrow logic_uScript_HideArrow_uScript_HideArrow_128 = new uScript_HideArrow();

	private bool logic_uScript_HideArrow_Out_128 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_129 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_129;

	private bool logic_uScriptCon_CompareBool_True_129 = true;

	private bool logic_uScriptCon_CompareBool_False_129 = true;

	private uScript_LockPlayerInput logic_uScript_LockPlayerInput_uScript_LockPlayerInput_131 = new uScript_LockPlayerInput();

	private bool logic_uScript_LockPlayerInput_lockInput_131 = true;

	private bool logic_uScript_LockPlayerInput_includeCamera_131 = true;

	private bool logic_uScript_LockPlayerInput_Out_131 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_135 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_135;

	private bool logic_uScriptAct_SetBool_Out_135 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_135 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_135 = true;

	private uScript_LockPause logic_uScript_LockPause_uScript_LockPause_137 = new uScript_LockPause();

	private bool logic_uScript_LockPause_lockPause_137;

	private ManPauseGame.DisablePauseReason logic_uScript_LockPause_disabledReason_137 = ManPauseGame.DisablePauseReason.Tutorial;

	private bool logic_uScript_LockPause_Out_137 = true;

	private uScript_LockPlayerInput logic_uScript_LockPlayerInput_uScript_LockPlayerInput_138 = new uScript_LockPlayerInput();

	private bool logic_uScript_LockPlayerInput_lockInput_138;

	private bool logic_uScript_LockPlayerInput_includeCamera_138;

	private bool logic_uScript_LockPlayerInput_Out_138 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_139 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_139;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_139;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_139;

	private bool logic_uScript_AddMessage_Out_139 = true;

	private bool logic_uScript_AddMessage_Shown_139 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_143 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_143;

	private bool logic_uScriptAct_SetBool_Out_143 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_143 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_143 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_146 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_146;

	private bool logic_uScriptCon_CompareBool_True_146 = true;

	private bool logic_uScriptCon_CompareBool_False_146 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_148 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_148;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_148;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_148;

	private bool logic_uScript_AddMessage_Out_148 = true;

	private bool logic_uScript_AddMessage_Shown_148 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_149 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_149;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_149;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_149;

	private bool logic_uScript_AddMessage_Out_149 = true;

	private bool logic_uScript_AddMessage_Shown_149 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_152 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_152;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_152 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_152 = "ResourcesSpawned";

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_155 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_155;

	private bool logic_uScriptAct_SetBool_Out_155 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_155 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_155 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_156 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_156;

	private bool logic_uScriptCon_CompareBool_True_156 = true;

	private bool logic_uScriptCon_CompareBool_False_156 = true;

	private uScript_AccessListBlock logic_uScript_AccessListBlock_uScript_AccessListBlock_157 = new uScript_AccessListBlock();

	private TankBlock[] logic_uScript_AccessListBlock_blockList_157 = new TankBlock[0];

	private int logic_uScript_AccessListBlock_index_157;

	private TankBlock logic_uScript_AccessListBlock_value_157;

	private bool logic_uScript_AccessListBlock_Out_157 = true;

	private uScript_RemoveAllGhostBlocksOnTech logic_uScript_RemoveAllGhostBlocksOnTech_uScript_RemoveAllGhostBlocksOnTech_158 = new uScript_RemoveAllGhostBlocksOnTech();

	private Tank logic_uScript_RemoveAllGhostBlocksOnTech_targetTech_158;

	private bool logic_uScript_RemoveAllGhostBlocksOnTech_Out_158 = true;

	private uScript_SpawnGhostBlocks logic_uScript_SpawnGhostBlocks_uScript_SpawnGhostBlocks_162 = new uScript_SpawnGhostBlocks();

	private GhostBlockSpawnData[] logic_uScript_SpawnGhostBlocks_ghostBlockData_162 = new GhostBlockSpawnData[0];

	private GameObject logic_uScript_SpawnGhostBlocks_ownerNode_162;

	private Tank logic_uScript_SpawnGhostBlocks_targetTech_162;

	private TankBlock[] logic_uScript_SpawnGhostBlocks_Return_162;

	private bool logic_uScript_SpawnGhostBlocks_OnAlreadySpawned_162 = true;

	private bool logic_uScript_SpawnGhostBlocks_OnSpawned_162 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_163 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_163;

	private bool logic_uScriptAct_SetBool_Out_163 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_163 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_163 = true;

	private uScript_PointArrowAtBlock logic_uScript_PointArrowAtBlock_uScript_PointArrowAtBlock_164 = new uScript_PointArrowAtBlock();

	private TankBlock logic_uScript_PointArrowAtBlock_block_164;

	private float logic_uScript_PointArrowAtBlock_timeToShowFor_164 = -1f;

	private Vector3 logic_uScript_PointArrowAtBlock_offset_164 = new Vector3(0f, 0f, 0f);

	private bool logic_uScript_PointArrowAtBlock_Out_164 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_166 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_166;

	private bool logic_uScriptCon_CompareBool_True_166 = true;

	private bool logic_uScriptCon_CompareBool_False_166 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_170 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_170;

	private bool logic_uScriptCon_CompareBool_True_170 = true;

	private bool logic_uScriptCon_CompareBool_False_170 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_171 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_171;

	private bool logic_uScriptAct_SetBool_Out_171 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_171 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_171 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_176 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_176;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_176 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_176 = "BlockAttachedToNPC";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_177 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_177;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_177 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_177 = "GhostBlockSpawned";

	private uScript_SetTankTeam logic_uScript_SetTankTeam_uScript_SetTankTeam_178 = new uScript_SetTankTeam();

	private Tank logic_uScript_SetTankTeam_tank_178;

	private int logic_uScript_SetTankTeam_team_178 = -2;

	private bool logic_uScript_SetTankTeam_Out_178 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_181 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_181;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_181 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_181 = "msgBaseFoundShown";

	private uScriptCon_ManualSwitch logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_183 = new uScriptCon_ManualSwitch();

	private int logic_uScriptCon_ManualSwitch_CurrentOutput_183;

	private uScriptAct_AddInt_v2 logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_185 = new uScriptAct_AddInt_v2();

	private int logic_uScriptAct_AddInt_v2_A_185;

	private int logic_uScriptAct_AddInt_v2_B_185 = 1;

	private int logic_uScriptAct_AddInt_v2_IntResult_185;

	private float logic_uScriptAct_AddInt_v2_FloatResult_185;

	private bool logic_uScriptAct_AddInt_v2_Out_185 = true;

	private SubGraph_SaveLoadInt logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_188 = new SubGraph_SaveLoadInt();

	private int logic_SubGraph_SaveLoadInt_restartValue_188 = 1;

	private int logic_SubGraph_SaveLoadInt_integer_188;

	private object logic_SubGraph_SaveLoadInt_intAsVariable_188 = "";

	private string logic_SubGraph_SaveLoadInt_uniqueID_188 = "Stage2Steps";

	private uScript_LockPause logic_uScript_LockPause_uScript_LockPause_189 = new uScript_LockPause();

	private bool logic_uScript_LockPause_lockPause_189 = true;

	private ManPauseGame.DisablePauseReason logic_uScript_LockPause_disabledReason_189 = ManPauseGame.DisablePauseReason.Tutorial;

	private bool logic_uScript_LockPause_Out_189 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_190 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_190;

	private bool logic_uScriptAct_SetBool_Out_190 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_190 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_190 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_192 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_192;

	private bool logic_uScriptCon_CompareBool_True_192 = true;

	private bool logic_uScriptCon_CompareBool_False_192 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_193 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_193;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_193;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_193;

	private bool logic_uScript_AddMessage_Out_193 = true;

	private bool logic_uScript_AddMessage_Shown_193 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_197 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_197;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_197 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_197 = "msgWorkOutHowToCraftShown";

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_198 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_198;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_198;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_198;

	private bool logic_uScript_AddMessage_Out_198 = true;

	private bool logic_uScript_AddMessage_Shown_198 = true;

	private uScript_CraftingUIHighlightItem logic_uScript_CraftingUIHighlightItem_uScript_CraftingUIHighlightItem_201 = new uScript_CraftingUIHighlightItem();

	private ManHUD.HUDElementType logic_uScript_CraftingUIHighlightItem_targetMenuType_201;

	private ItemTypeInfo logic_uScript_CraftingUIHighlightItem_itemToHighlight_201;

	private bool logic_uScript_CraftingUIHighlightItem_Out_201 = true;

	private bool logic_uScript_CraftingUIHighlightItem_Waiting_201 = true;

	private bool logic_uScript_CraftingUIHighlightItem_Selected_201 = true;

	private uScript_CraftingUIHighlightItem logic_uScript_CraftingUIHighlightItem_uScript_CraftingUIHighlightItem_203 = new uScript_CraftingUIHighlightItem();

	private ManHUD.HUDElementType logic_uScript_CraftingUIHighlightItem_targetMenuType_203 = ManHUD.HUDElementType.ComponentRecipeSelect;

	private ItemTypeInfo logic_uScript_CraftingUIHighlightItem_itemToHighlight_203;

	private bool logic_uScript_CraftingUIHighlightItem_Out_203 = true;

	private bool logic_uScript_CraftingUIHighlightItem_Waiting_203 = true;

	private bool logic_uScript_CraftingUIHighlightItem_Selected_203 = true;

	private uScript_CraftingUIHighlightCraftButton logic_uScript_CraftingUIHighlightCraftButton_uScript_CraftingUIHighlightCraftButton_205 = new uScript_CraftingUIHighlightCraftButton();

	private ManHUD.HUDElementType logic_uScript_CraftingUIHighlightCraftButton_targetMenuType_205 = ManHUD.HUDElementType.ComponentRecipeSelect;

	private bool logic_uScript_CraftingUIHighlightCraftButton_Out_205 = true;

	private bool logic_uScript_CraftingUIHighlightCraftButton_Waiting_205 = true;

	private bool logic_uScript_CraftingUIHighlightCraftButton_Selected_205 = true;

	private uScript_CraftingUIHighlightRecipeIngredient logic_uScript_CraftingUIHighlightRecipeIngredient_uScript_CraftingUIHighlightRecipeIngredient_207 = new uScript_CraftingUIHighlightRecipeIngredient();

	private ManHUD.HUDElementType logic_uScript_CraftingUIHighlightRecipeIngredient_targetMenuType_207 = ManHUD.HUDElementType.BlockRecipeSelect;

	private int logic_uScript_CraftingUIHighlightRecipeIngredient_ingredientNumber_207 = 1;

	private bool logic_uScript_CraftingUIHighlightRecipeIngredient_Out_207 = true;

	private uScript_LockTechInteraction logic_uScript_LockTechInteraction_uScript_LockTechInteraction_208 = new uScript_LockTechInteraction();

	private Tank logic_uScript_LockTechInteraction_tech_208;

	private BlockTypes[] logic_uScript_LockTechInteraction_excludedBlocks_208 = new BlockTypes[0];

	private TankBlock[] logic_uScript_LockTechInteraction_excludedUniqueBlocks_208 = new TankBlock[0];

	private bool logic_uScript_LockTechInteraction_Out_208 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_212 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_212;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_212;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_212;

	private bool logic_uScript_AddMessage_Out_212 = true;

	private bool logic_uScript_AddMessage_Shown_212 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_214 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_214;

	private bool logic_uScriptAct_SetBool_Out_214 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_214 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_214 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_218 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_218;

	private bool logic_uScriptCon_CompareBool_True_218 = true;

	private bool logic_uScriptCon_CompareBool_False_218 = true;

	private uScript_LockTechInteraction logic_uScript_LockTechInteraction_uScript_LockTechInteraction_220 = new uScript_LockTechInteraction();

	private Tank logic_uScript_LockTechInteraction_tech_220;

	private BlockTypes[] logic_uScript_LockTechInteraction_excludedBlocks_220 = new BlockTypes[0];

	private TankBlock[] logic_uScript_LockTechInteraction_excludedUniqueBlocks_220 = new TankBlock[0];

	private bool logic_uScript_LockTechInteraction_Out_220 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_221 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_221;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_221;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_221;

	private bool logic_uScript_AddMessage_Out_221 = true;

	private bool logic_uScript_AddMessage_Shown_221 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_223 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_223;

	private bool logic_uScriptCon_CompareBool_True_223 = true;

	private bool logic_uScriptCon_CompareBool_False_223 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_226 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_226;

	private bool logic_uScriptAct_SetBool_Out_226 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_226 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_226 = true;

	private uScript_Wait logic_uScript_Wait_uScript_Wait_228 = new uScript_Wait();

	private float logic_uScript_Wait_seconds_228;

	private bool logic_uScript_Wait_repeat_228;

	private bool logic_uScript_Wait_Waited_228 = true;

	private uScript_MaskHUD logic_uScript_MaskHUD_uScript_MaskHUD_230 = new uScript_MaskHUD();

	private RectTransform logic_uScript_MaskHUD_singleUnmaskedTransform_230;

	private RectTransform[] logic_uScript_MaskHUD_unmaskedTransforms_230 = new RectTransform[0];

	private bool logic_uScript_MaskHUD_Out_230 = true;

	private uScript_Wait logic_uScript_Wait_uScript_Wait_231 = new uScript_Wait();

	private float logic_uScript_Wait_seconds_231 = 4f;

	private bool logic_uScript_Wait_repeat_231;

	private bool logic_uScript_Wait_Waited_231 = true;

	private uScript_Wait logic_uScript_Wait_uScript_Wait_232 = new uScript_Wait();

	private float logic_uScript_Wait_seconds_232 = 4f;

	private bool logic_uScript_Wait_repeat_232;

	private bool logic_uScript_Wait_Waited_232 = true;

	private uScript_CraftingUIHighlightRecipeIngredient logic_uScript_CraftingUIHighlightRecipeIngredient_uScript_CraftingUIHighlightRecipeIngredient_233 = new uScript_CraftingUIHighlightRecipeIngredient();

	private ManHUD.HUDElementType logic_uScript_CraftingUIHighlightRecipeIngredient_targetMenuType_233 = ManHUD.HUDElementType.BlockRecipeSelect;

	private int logic_uScript_CraftingUIHighlightRecipeIngredient_ingredientNumber_233 = 3;

	private bool logic_uScript_CraftingUIHighlightRecipeIngredient_Out_233 = true;

	private uScript_HideHUDElement logic_uScript_HideHUDElement_uScript_HideHUDElement_234 = new uScript_HideHUDElement();

	private ManHUD.HUDElementType logic_uScript_HideHUDElement_hudElement_234 = ManHUD.HUDElementType.BlockRecipeSelect;

	private bool logic_uScript_HideHUDElement_Out_234 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_235 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_235 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_239 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_239;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_239;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_239;

	private bool logic_uScript_AddMessage_Out_239 = true;

	private bool logic_uScript_AddMessage_Shown_239 = true;

	private uScript_Wait logic_uScript_Wait_uScript_Wait_240 = new uScript_Wait();

	private float logic_uScript_Wait_seconds_240;

	private bool logic_uScript_Wait_repeat_240 = true;

	private bool logic_uScript_Wait_Waited_240 = true;

	private uScriptCon_ManualSwitch logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_242 = new uScriptCon_ManualSwitch();

	private int logic_uScriptCon_ManualSwitch_CurrentOutput_242;

	private uScriptAct_AddInt_v2 logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_244 = new uScriptAct_AddInt_v2();

	private int logic_uScriptAct_AddInt_v2_A_244;

	private int logic_uScriptAct_AddInt_v2_B_244 = 1;

	private int logic_uScriptAct_AddInt_v2_IntResult_244;

	private float logic_uScriptAct_AddInt_v2_FloatResult_244;

	private bool logic_uScriptAct_AddInt_v2_Out_244 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_248 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_248;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_248;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_248;

	private bool logic_uScript_AddMessage_Out_248 = true;

	private bool logic_uScript_AddMessage_Shown_248 = true;

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

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_255 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_255 = 1;

	private int logic_uScriptAct_SetInt_Target_255;

	private bool logic_uScriptAct_SetInt_Out_255 = true;

	private uScript_Wait logic_uScript_Wait_uScript_Wait_257 = new uScript_Wait();

	private float logic_uScript_Wait_seconds_257 = 8f;

	private bool logic_uScript_Wait_repeat_257 = true;

	private bool logic_uScript_Wait_Waited_257 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_258 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_258 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_258 = true;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_258 = true;

	private uScriptCon_CompareInt logic_uScriptCon_CompareInt_uScriptCon_CompareInt_260 = new uScriptCon_CompareInt();

	private int logic_uScriptCon_CompareInt_A_260;

	private int logic_uScriptCon_CompareInt_B_260 = 4;

	private bool logic_uScriptCon_CompareInt_GreaterThan_260 = true;

	private bool logic_uScriptCon_CompareInt_GreaterThanOrEqualTo_260 = true;

	private bool logic_uScriptCon_CompareInt_EqualTo_260 = true;

	private bool logic_uScriptCon_CompareInt_NotEqualTo_260 = true;

	private bool logic_uScriptCon_CompareInt_LessThanOrEqualTo_260 = true;

	private bool logic_uScriptCon_CompareInt_LessThan_260 = true;

	private uScriptCon_CompareInt logic_uScriptCon_CompareInt_uScriptCon_CompareInt_265 = new uScriptCon_CompareInt();

	private int logic_uScriptCon_CompareInt_A_265;

	private int logic_uScriptCon_CompareInt_B_265 = 3;

	private bool logic_uScriptCon_CompareInt_GreaterThan_265 = true;

	private bool logic_uScriptCon_CompareInt_GreaterThanOrEqualTo_265 = true;

	private bool logic_uScriptCon_CompareInt_EqualTo_265 = true;

	private bool logic_uScriptCon_CompareInt_NotEqualTo_265 = true;

	private bool logic_uScriptCon_CompareInt_LessThanOrEqualTo_265 = true;

	private bool logic_uScriptCon_CompareInt_LessThan_265 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_267 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_267;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_267;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_267;

	private bool logic_uScript_AddMessage_Out_267 = true;

	private bool logic_uScript_AddMessage_Shown_267 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_269 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_269 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_271 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_271;

	private bool logic_uScriptCon_CompareBool_True_271 = true;

	private bool logic_uScriptCon_CompareBool_False_271 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_273 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_273;

	private bool logic_uScriptAct_SetBool_Out_273 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_273 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_273 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_275 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_275;

	private bool logic_uScriptAct_SetBool_Out_275 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_275 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_275 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_279 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_279;

	private bool logic_uScriptAct_SetBool_Out_279 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_279 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_279 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_280 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_280;

	private bool logic_uScriptAct_SetBool_Out_280 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_280 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_280 = true;

	private uScript_SetTankTeam logic_uScript_SetTankTeam_uScript_SetTankTeam_281 = new uScript_SetTankTeam();

	private Tank logic_uScript_SetTankTeam_tank_281;

	private int logic_uScript_SetTankTeam_team_281;

	private bool logic_uScript_SetTankTeam_Out_281 = true;

	private uScript_LockTechInteraction logic_uScript_LockTechInteraction_uScript_LockTechInteraction_283 = new uScript_LockTechInteraction();

	private Tank logic_uScript_LockTechInteraction_tech_283;

	private BlockTypes[] logic_uScript_LockTechInteraction_excludedBlocks_283 = new BlockTypes[0];

	private TankBlock[] logic_uScript_LockTechInteraction_excludedUniqueBlocks_283 = new TankBlock[0];

	private bool logic_uScript_LockTechInteraction_Out_283 = true;

	private uScript_LockTech logic_uScript_LockTech_uScript_LockTech_285 = new uScript_LockTech();

	private Tank logic_uScript_LockTech_tech_285;

	private uScript_LockTech.TechLockType logic_uScript_LockTech_lockType_285 = uScript_LockTech.TechLockType.LockAttach;

	private bool logic_uScript_LockTech_Out_285 = true;

	private uScript_RestrictItemPickup logic_uScript_RestrictItemPickup_uScript_RestrictItemPickup_287 = new uScript_RestrictItemPickup();

	private Tank logic_uScript_RestrictItemPickup_tech_287;

	private ChunkTypes[] logic_uScript_RestrictItemPickup_typesToAccept_287 = new ChunkTypes[0];

	private bool logic_uScript_RestrictItemPickup_Out_287 = true;

	private uScript_RestrictItemPickup logic_uScript_RestrictItemPickup_uScript_RestrictItemPickup_291 = new uScript_RestrictItemPickup();

	private Tank logic_uScript_RestrictItemPickup_tech_291;

	private ChunkTypes[] logic_uScript_RestrictItemPickup_typesToAccept_291 = new ChunkTypes[0];

	private bool logic_uScript_RestrictItemPickup_Out_291 = true;

	private uScript_SpawnResourceListOnHolder logic_uScript_SpawnResourceListOnHolder_uScript_SpawnResourceListOnHolder_292 = new uScript_SpawnResourceListOnHolder();

	private Tank logic_uScript_SpawnResourceListOnHolder_tech_292;

	private ChunkTypes[] logic_uScript_SpawnResourceListOnHolder_chunks_292 = new ChunkTypes[0];

	private BlockTypes logic_uScript_SpawnResourceListOnHolder_blockType_292 = BlockTypes.GSOReceiver_111;

	private bool logic_uScript_SpawnResourceListOnHolder_Out_292 = true;

	private uScript_LockTechInteraction logic_uScript_LockTechInteraction_uScript_LockTechInteraction_296 = new uScript_LockTechInteraction();

	private Tank logic_uScript_LockTechInteraction_tech_296;

	private BlockTypes[] logic_uScript_LockTechInteraction_excludedBlocks_296 = new BlockTypes[0];

	private TankBlock[] logic_uScript_LockTechInteraction_excludedUniqueBlocks_296 = new TankBlock[0];

	private bool logic_uScript_LockTechInteraction_Out_296 = true;

	private uScript_LockTechInteraction logic_uScript_LockTechInteraction_uScript_LockTechInteraction_298 = new uScript_LockTechInteraction();

	private Tank logic_uScript_LockTechInteraction_tech_298;

	private BlockTypes[] logic_uScript_LockTechInteraction_excludedBlocks_298 = new BlockTypes[2]
	{
		BlockTypes.GSOFabricator_322,
		BlockTypes.GSOComponentFactory_323
	};

	private TankBlock[] logic_uScript_LockTechInteraction_excludedUniqueBlocks_298 = new TankBlock[0];

	private bool logic_uScript_LockTechInteraction_Out_298 = true;

	private uScript_LockTechInteraction logic_uScript_LockTechInteraction_uScript_LockTechInteraction_299 = new uScript_LockTechInteraction();

	private Tank logic_uScript_LockTechInteraction_tech_299;

	private BlockTypes[] logic_uScript_LockTechInteraction_excludedBlocks_299 = new BlockTypes[0];

	private TankBlock[] logic_uScript_LockTechInteraction_excludedUniqueBlocks_299 = new TankBlock[0];

	private bool logic_uScript_LockTechInteraction_Out_299 = true;

	private uScript_DisableClosingCraftingUIWhenTooFarAway logic_uScript_DisableClosingCraftingUIWhenTooFarAway_uScript_DisableClosingCraftingUIWhenTooFarAway_301 = new uScript_DisableClosingCraftingUIWhenTooFarAway();

	private TankBlock logic_uScript_DisableClosingCraftingUIWhenTooFarAway_craftingBlock_301;

	private bool logic_uScript_DisableClosingCraftingUIWhenTooFarAway_Out_301 = true;

	private uScript_DisableClosingCraftingUIWhenTooFarAway logic_uScript_DisableClosingCraftingUIWhenTooFarAway_uScript_DisableClosingCraftingUIWhenTooFarAway_303 = new uScript_DisableClosingCraftingUIWhenTooFarAway();

	private TankBlock logic_uScript_DisableClosingCraftingUIWhenTooFarAway_craftingBlock_303;

	private bool logic_uScript_DisableClosingCraftingUIWhenTooFarAway_Out_303 = true;

	private uScript_DisableClosingCraftingUIWhenTooFarAway logic_uScript_DisableClosingCraftingUIWhenTooFarAway_uScript_DisableClosingCraftingUIWhenTooFarAway_305 = new uScript_DisableClosingCraftingUIWhenTooFarAway();

	private TankBlock logic_uScript_DisableClosingCraftingUIWhenTooFarAway_craftingBlock_305;

	private bool logic_uScript_DisableClosingCraftingUIWhenTooFarAway_Out_305 = true;

	private uScript_DisableClosingCraftingUIWhenTooFarAway logic_uScript_DisableClosingCraftingUIWhenTooFarAway_uScript_DisableClosingCraftingUIWhenTooFarAway_307 = new uScript_DisableClosingCraftingUIWhenTooFarAway();

	private TankBlock logic_uScript_DisableClosingCraftingUIWhenTooFarAway_craftingBlock_307;

	private bool logic_uScript_DisableClosingCraftingUIWhenTooFarAway_Out_307 = true;

	private uScript_DisableClosingCraftingUIWhenTooFarAway logic_uScript_DisableClosingCraftingUIWhenTooFarAway_uScript_DisableClosingCraftingUIWhenTooFarAway_310 = new uScript_DisableClosingCraftingUIWhenTooFarAway();

	private TankBlock logic_uScript_DisableClosingCraftingUIWhenTooFarAway_craftingBlock_310;

	private bool logic_uScript_DisableClosingCraftingUIWhenTooFarAway_Out_310 = true;

	private uScript_LockPause logic_uScript_LockPause_uScript_LockPause_311 = new uScript_LockPause();

	private bool logic_uScript_LockPause_lockPause_311;

	private ManPauseGame.DisablePauseReason logic_uScript_LockPause_disabledReason_311 = ManPauseGame.DisablePauseReason.Tutorial;

	private bool logic_uScript_LockPause_Out_311 = true;

	private uScript_LockPlayerInput logic_uScript_LockPlayerInput_uScript_LockPlayerInput_312 = new uScript_LockPlayerInput();

	private bool logic_uScript_LockPlayerInput_lockInput_312;

	private bool logic_uScript_LockPlayerInput_includeCamera_312;

	private bool logic_uScript_LockPlayerInput_Out_312 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_313 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_313;

	private bool logic_uScriptCon_CompareBool_True_313 = true;

	private bool logic_uScriptCon_CompareBool_False_313 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_315 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_315;

	private bool logic_uScriptAct_SetBool_Out_315 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_315 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_315 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_321 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_321;

	private bool logic_uScriptAct_SetBool_Out_321 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_321 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_321 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_322 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_322;

	private bool logic_uScriptCon_CompareBool_True_322 = true;

	private bool logic_uScriptCon_CompareBool_False_322 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_323 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_323;

	private bool logic_uScriptCon_CompareBool_True_323 = true;

	private bool logic_uScriptCon_CompareBool_False_323 = true;

	private uScript_DisableClosingCraftingUIWhenTooFarAway logic_uScript_DisableClosingCraftingUIWhenTooFarAway_uScript_DisableClosingCraftingUIWhenTooFarAway_326 = new uScript_DisableClosingCraftingUIWhenTooFarAway();

	private TankBlock logic_uScript_DisableClosingCraftingUIWhenTooFarAway_craftingBlock_326;

	private bool logic_uScript_DisableClosingCraftingUIWhenTooFarAway_Out_326 = true;

	private uScript_GetTankBlock logic_uScript_GetTankBlock_uScript_GetTankBlock_329 = new uScript_GetTankBlock();

	private Tank logic_uScript_GetTankBlock_tank_329;

	private BlockTypes logic_uScript_GetTankBlock_blockType_329 = BlockTypes.GSOFabricator_322;

	private TankBlock logic_uScript_GetTankBlock_Return_329;

	private bool logic_uScript_GetTankBlock_Out_329 = true;

	private bool logic_uScript_GetTankBlock_Returned_329 = true;

	private bool logic_uScript_GetTankBlock_NotFound_329 = true;

	private uScript_GetTankBlock logic_uScript_GetTankBlock_uScript_GetTankBlock_332 = new uScript_GetTankBlock();

	private Tank logic_uScript_GetTankBlock_tank_332;

	private BlockTypes logic_uScript_GetTankBlock_blockType_332 = BlockTypes.GSOComponentFactory_323;

	private TankBlock logic_uScript_GetTankBlock_Return_332;

	private bool logic_uScript_GetTankBlock_Out_332 = true;

	private bool logic_uScript_GetTankBlock_Returned_332 = true;

	private bool logic_uScript_GetTankBlock_NotFound_332 = true;

	private uScript_HideHUDElement logic_uScript_HideHUDElement_uScript_HideHUDElement_335 = new uScript_HideHUDElement();

	private ManHUD.HUDElementType logic_uScript_HideHUDElement_hudElement_335;

	private bool logic_uScript_HideHUDElement_Out_335 = true;

	private uScript_IsHUDElementLinkedToBlock logic_uScript_IsHUDElementLinkedToBlock_uScript_IsHUDElementLinkedToBlock_336 = new uScript_IsHUDElementLinkedToBlock();

	private ManHUD.HUDElementType logic_uScript_IsHUDElementLinkedToBlock_hudElement_336;

	private TankBlock logic_uScript_IsHUDElementLinkedToBlock_targetBlock_336;

	private bool logic_uScript_IsHUDElementLinkedToBlock_True_336 = true;

	private bool logic_uScript_IsHUDElementLinkedToBlock_False_336 = true;

	private uScript_IsHUDElementLinkedToBlock logic_uScript_IsHUDElementLinkedToBlock_uScript_IsHUDElementLinkedToBlock_339 = new uScript_IsHUDElementLinkedToBlock();

	private ManHUD.HUDElementType logic_uScript_IsHUDElementLinkedToBlock_hudElement_339;

	private TankBlock logic_uScript_IsHUDElementLinkedToBlock_targetBlock_339;

	private bool logic_uScript_IsHUDElementLinkedToBlock_True_339 = true;

	private bool logic_uScript_IsHUDElementLinkedToBlock_False_339 = true;

	private uScript_HideHUDElement logic_uScript_HideHUDElement_uScript_HideHUDElement_342 = new uScript_HideHUDElement();

	private ManHUD.HUDElementType logic_uScript_HideHUDElement_hudElement_342;

	private bool logic_uScript_HideHUDElement_Out_342 = true;

	private SubGraph_AddMessageWithPadSupport logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_344 = new SubGraph_AddMessageWithPadSupport();

	private uScript_AddMessage.MessageData logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboard_344;

	private uScript_AddMessage.MessageData logic_SubGraph_AddMessageWithPadSupport_messageControlPad_344;

	private uScript_AddMessage.MessageSpeaker logic_SubGraph_AddMessageWithPadSupport_speaker_344;

	private ManOnScreenMessages.OnScreenMessage logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboardReturn_344;

	private ManOnScreenMessages.OnScreenMessage logic_SubGraph_AddMessageWithPadSupport_messageControlPadReturn_344;

	private SubGraph_AddMessageWithPadSupport logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_346 = new SubGraph_AddMessageWithPadSupport();

	private uScript_AddMessage.MessageData logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboard_346;

	private uScript_AddMessage.MessageData logic_SubGraph_AddMessageWithPadSupport_messageControlPad_346;

	private uScript_AddMessage.MessageSpeaker logic_SubGraph_AddMessageWithPadSupport_speaker_346;

	private ManOnScreenMessages.OnScreenMessage logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboardReturn_346;

	private ManOnScreenMessages.OnScreenMessage logic_SubGraph_AddMessageWithPadSupport_messageControlPadReturn_346;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_348 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_348 = "";

	private bool logic_uScript_EnableGlow_enable_348 = true;

	private bool logic_uScript_EnableGlow_Out_348 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_350 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_350 = "";

	private bool logic_uScript_EnableGlow_enable_350;

	private bool logic_uScript_EnableGlow_Out_350 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_352 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_352 = "";

	private bool logic_uScript_EnableGlow_enable_352 = true;

	private bool logic_uScript_EnableGlow_Out_352 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_354 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_354 = "";

	private bool logic_uScript_EnableGlow_enable_354;

	private bool logic_uScript_EnableGlow_Out_354 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_356 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_356 = "";

	private bool logic_uScript_EnableGlow_enable_356 = true;

	private bool logic_uScript_EnableGlow_Out_356 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_358 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_358 = "";

	private bool logic_uScript_EnableGlow_enable_358 = true;

	private bool logic_uScript_EnableGlow_Out_358 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_361 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_361 = "";

	private bool logic_uScript_EnableGlow_enable_361;

	private bool logic_uScript_EnableGlow_Out_361 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_362 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_362 = "";

	private bool logic_uScript_EnableGlow_enable_362;

	private bool logic_uScript_EnableGlow_Out_362 = true;

	private uScript_SetTankHideBlockLimit logic_uScript_SetTankHideBlockLimit_uScript_SetTankHideBlockLimit_364 = new uScript_SetTankHideBlockLimit();

	private bool logic_uScript_SetTankHideBlockLimit_hidden_364 = true;

	private Tank logic_uScript_SetTankHideBlockLimit_tech_364;

	private bool logic_uScript_SetTankHideBlockLimit_Out_364 = true;

	private uScript_SetTankHideBlockLimit logic_uScript_SetTankHideBlockLimit_uScript_SetTankHideBlockLimit_365 = new uScript_SetTankHideBlockLimit();

	private bool logic_uScript_SetTankHideBlockLimit_hidden_365;

	private Tank logic_uScript_SetTankHideBlockLimit_tech_365;

	private bool logic_uScript_SetTankHideBlockLimit_Out_365 = true;

	private uScript_SendAnaliticsEvent logic_uScript_SendAnaliticsEvent_uScript_SendAnaliticsEvent_366 = new uScript_SendAnaliticsEvent();

	private string logic_uScript_SendAnaliticsEvent_analiticsEvent_366 = "tutorial_start";

	private string logic_uScript_SendAnaliticsEvent_parameterName_366 = "crafting_tutorial";

	private object logic_uScript_SendAnaliticsEvent_parameter_366 = "7";

	private bool logic_uScript_SendAnaliticsEvent_Out_366 = true;

	private uScript_SendAnaliticsEvent logic_uScript_SendAnaliticsEvent_uScript_SendAnaliticsEvent_367 = new uScript_SendAnaliticsEvent();

	private string logic_uScript_SendAnaliticsEvent_analiticsEvent_367 = "tutorial_complete";

	private string logic_uScript_SendAnaliticsEvent_parameterName_367 = "crafting_tutorial";

	private object logic_uScript_SendAnaliticsEvent_parameter_367 = "7";

	private bool logic_uScript_SendAnaliticsEvent_Out_367 = true;

	private uScript_LockHudGroup logic_uScript_LockHudGroup_uScript_LockHudGroup_368 = new uScript_LockHudGroup();

	private ManHUD.HUDGroup logic_uScript_LockHudGroup_group_368;

	private bool logic_uScript_LockHudGroup_locked_368 = true;

	private bool logic_uScript_LockHudGroup_Out_368 = true;

	private uScript_LockHudGroup logic_uScript_LockHudGroup_uScript_LockHudGroup_369 = new uScript_LockHudGroup();

	private ManHUD.HUDGroup logic_uScript_LockHudGroup_group_369;

	private bool logic_uScript_LockHudGroup_locked_369;

	private bool logic_uScript_LockHudGroup_Out_369 = true;

	private BlockTypes event_UnityEngine_GameObject_BlockType_85;

	private int event_UnityEngine_GameObject_BlockTypeTotal_85;

	private int event_UnityEngine_GameObject_BlockTotal_85;

	private TankBlock event_UnityEngine_GameObject_Block_85;

	private TankBlock event_UnityEngine_GameObject_CrafterBlock_85;

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
		if (null == owner_Connection_72 || !m_RegisteredForEvents)
		{
			owner_Connection_72 = parentGameObject;
		}
		if (null == owner_Connection_98 || !m_RegisteredForEvents)
		{
			owner_Connection_98 = parentGameObject;
			if (null != owner_Connection_98)
			{
				uScript_BlockCraftedEvent uScript_BlockCraftedEvent2 = owner_Connection_98.GetComponent<uScript_BlockCraftedEvent>();
				if (null == uScript_BlockCraftedEvent2)
				{
					uScript_BlockCraftedEvent2 = owner_Connection_98.AddComponent<uScript_BlockCraftedEvent>();
				}
				if (null != uScript_BlockCraftedEvent2)
				{
					uScript_BlockCraftedEvent2.BlockCraftedEvent += Instance_BlockCraftedEvent_85;
				}
			}
		}
		if (null == owner_Connection_159 || !m_RegisteredForEvents)
		{
			owner_Connection_159 = parentGameObject;
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
		if (!m_RegisteredForEvents && null != owner_Connection_98)
		{
			uScript_BlockCraftedEvent uScript_BlockCraftedEvent2 = owner_Connection_98.GetComponent<uScript_BlockCraftedEvent>();
			if (null == uScript_BlockCraftedEvent2)
			{
				uScript_BlockCraftedEvent2 = owner_Connection_98.AddComponent<uScript_BlockCraftedEvent>();
			}
			if (null != uScript_BlockCraftedEvent2)
			{
				uScript_BlockCraftedEvent2.BlockCraftedEvent += Instance_BlockCraftedEvent_85;
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
		if (null != owner_Connection_98)
		{
			uScript_BlockCraftedEvent component3 = owner_Connection_98.GetComponent<uScript_BlockCraftedEvent>();
			if (null != component3)
			{
				component3.BlockCraftedEvent -= Instance_BlockCraftedEvent_85;
			}
		}
	}

	public override void SetParent(GameObject g)
	{
		parentGameObject = g;
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_2.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_7.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_9.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_12.SetParent(g);
		logic_uScript_PausePopulation_uScript_PausePopulation_13.SetParent(g);
		logic_uScript_PausePopulation_uScript_PausePopulation_15.SetParent(g);
		logic_uScript_HideArrow_uScript_HideArrow_16.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_17.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_18.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_20.SetParent(g);
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_21.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_23.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_25.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_28.SetParent(g);
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_30.SetParent(g);
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_31.SetParent(g);
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_33.SetParent(g);
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_40.SetParent(g);
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_42.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_44.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_47.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_50.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_51.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_55.SetParent(g);
		logic_SubGraph_Crafting_Tutorial_Finish_SubGraph_Crafting_Tutorial_Finish_56.SetParent(g);
		logic_SubGraph_Crafting_Tutorial_Init_SubGraph_Crafting_Tutorial_Init_67.SetParent(g);
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_73.SetParent(g);
		logic_uScript_CompareBlockTypes_uScript_CompareBlockTypes_75.SetParent(g);
		logic_uScript_HideArrow_uScript_HideArrow_76.SetParent(g);
		logic_uScript_LockTechInteraction_uScript_LockTechInteraction_77.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_78.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_80.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_90.SetParent(g);
		logic_uScript_LockPlayerInput_uScript_LockPlayerInput_91.SetParent(g);
		logic_uScript_LockPlayerInput_uScript_LockPlayerInput_93.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_94.SetParent(g);
		logic_uScript_LockPause_uScript_LockPause_95.SetParent(g);
		logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_99.SetParent(g);
		logic_uScript_LockPause_uScript_LockPause_100.SetParent(g);
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_101.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_103.SetParent(g);
		logic_uScript_DoesTechHaveBlockAtPosition_uScript_DoesTechHaveBlockAtPosition_106.SetParent(g);
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_109.SetParent(g);
		logic_uScript_SetCustomRadarTeamID_uScript_SetCustomRadarTeamID_111.SetParent(g);
		logic_uScript_LockTech_uScript_LockTech_112.SetParent(g);
		logic_uScript_SetTankTeam_uScript_SetTankTeam_113.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_119.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_121.SetParent(g);
		logic_uScript_LockTechInteraction_uScript_LockTechInteraction_122.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_125.SetParent(g);
		logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_126.SetParent(g);
		logic_uScript_HideArrow_uScript_HideArrow_128.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_129.SetParent(g);
		logic_uScript_LockPlayerInput_uScript_LockPlayerInput_131.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_135.SetParent(g);
		logic_uScript_LockPause_uScript_LockPause_137.SetParent(g);
		logic_uScript_LockPlayerInput_uScript_LockPlayerInput_138.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_139.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_143.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_146.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_148.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_149.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_152.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_155.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_156.SetParent(g);
		logic_uScript_AccessListBlock_uScript_AccessListBlock_157.SetParent(g);
		logic_uScript_RemoveAllGhostBlocksOnTech_uScript_RemoveAllGhostBlocksOnTech_158.SetParent(g);
		logic_uScript_SpawnGhostBlocks_uScript_SpawnGhostBlocks_162.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_163.SetParent(g);
		logic_uScript_PointArrowAtBlock_uScript_PointArrowAtBlock_164.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_166.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_170.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_171.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_176.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_177.SetParent(g);
		logic_uScript_SetTankTeam_uScript_SetTankTeam_178.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_181.SetParent(g);
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_183.SetParent(g);
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_185.SetParent(g);
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_188.SetParent(g);
		logic_uScript_LockPause_uScript_LockPause_189.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_190.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_192.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_193.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_197.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_198.SetParent(g);
		logic_uScript_CraftingUIHighlightItem_uScript_CraftingUIHighlightItem_201.SetParent(g);
		logic_uScript_CraftingUIHighlightItem_uScript_CraftingUIHighlightItem_203.SetParent(g);
		logic_uScript_CraftingUIHighlightCraftButton_uScript_CraftingUIHighlightCraftButton_205.SetParent(g);
		logic_uScript_CraftingUIHighlightRecipeIngredient_uScript_CraftingUIHighlightRecipeIngredient_207.SetParent(g);
		logic_uScript_LockTechInteraction_uScript_LockTechInteraction_208.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_212.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_214.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_218.SetParent(g);
		logic_uScript_LockTechInteraction_uScript_LockTechInteraction_220.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_221.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_223.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_226.SetParent(g);
		logic_uScript_Wait_uScript_Wait_228.SetParent(g);
		logic_uScript_MaskHUD_uScript_MaskHUD_230.SetParent(g);
		logic_uScript_Wait_uScript_Wait_231.SetParent(g);
		logic_uScript_Wait_uScript_Wait_232.SetParent(g);
		logic_uScript_CraftingUIHighlightRecipeIngredient_uScript_CraftingUIHighlightRecipeIngredient_233.SetParent(g);
		logic_uScript_HideHUDElement_uScript_HideHUDElement_234.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_235.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_239.SetParent(g);
		logic_uScript_Wait_uScript_Wait_240.SetParent(g);
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_242.SetParent(g);
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_244.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_248.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_252.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_254.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_255.SetParent(g);
		logic_uScript_Wait_uScript_Wait_257.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_258.SetParent(g);
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_260.SetParent(g);
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_265.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_267.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_269.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_271.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_273.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_275.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_279.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_280.SetParent(g);
		logic_uScript_SetTankTeam_uScript_SetTankTeam_281.SetParent(g);
		logic_uScript_LockTechInteraction_uScript_LockTechInteraction_283.SetParent(g);
		logic_uScript_LockTech_uScript_LockTech_285.SetParent(g);
		logic_uScript_RestrictItemPickup_uScript_RestrictItemPickup_287.SetParent(g);
		logic_uScript_RestrictItemPickup_uScript_RestrictItemPickup_291.SetParent(g);
		logic_uScript_SpawnResourceListOnHolder_uScript_SpawnResourceListOnHolder_292.SetParent(g);
		logic_uScript_LockTechInteraction_uScript_LockTechInteraction_296.SetParent(g);
		logic_uScript_LockTechInteraction_uScript_LockTechInteraction_298.SetParent(g);
		logic_uScript_LockTechInteraction_uScript_LockTechInteraction_299.SetParent(g);
		logic_uScript_DisableClosingCraftingUIWhenTooFarAway_uScript_DisableClosingCraftingUIWhenTooFarAway_301.SetParent(g);
		logic_uScript_DisableClosingCraftingUIWhenTooFarAway_uScript_DisableClosingCraftingUIWhenTooFarAway_303.SetParent(g);
		logic_uScript_DisableClosingCraftingUIWhenTooFarAway_uScript_DisableClosingCraftingUIWhenTooFarAway_305.SetParent(g);
		logic_uScript_DisableClosingCraftingUIWhenTooFarAway_uScript_DisableClosingCraftingUIWhenTooFarAway_307.SetParent(g);
		logic_uScript_DisableClosingCraftingUIWhenTooFarAway_uScript_DisableClosingCraftingUIWhenTooFarAway_310.SetParent(g);
		logic_uScript_LockPause_uScript_LockPause_311.SetParent(g);
		logic_uScript_LockPlayerInput_uScript_LockPlayerInput_312.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_313.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_315.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_321.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_322.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_323.SetParent(g);
		logic_uScript_DisableClosingCraftingUIWhenTooFarAway_uScript_DisableClosingCraftingUIWhenTooFarAway_326.SetParent(g);
		logic_uScript_GetTankBlock_uScript_GetTankBlock_329.SetParent(g);
		logic_uScript_GetTankBlock_uScript_GetTankBlock_332.SetParent(g);
		logic_uScript_HideHUDElement_uScript_HideHUDElement_335.SetParent(g);
		logic_uScript_IsHUDElementLinkedToBlock_uScript_IsHUDElementLinkedToBlock_336.SetParent(g);
		logic_uScript_IsHUDElementLinkedToBlock_uScript_IsHUDElementLinkedToBlock_339.SetParent(g);
		logic_uScript_HideHUDElement_uScript_HideHUDElement_342.SetParent(g);
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_344.SetParent(g);
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_346.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_348.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_350.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_352.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_354.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_356.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_358.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_361.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_362.SetParent(g);
		logic_uScript_SetTankHideBlockLimit_uScript_SetTankHideBlockLimit_364.SetParent(g);
		logic_uScript_SetTankHideBlockLimit_uScript_SetTankHideBlockLimit_365.SetParent(g);
		logic_uScript_SendAnaliticsEvent_uScript_SendAnaliticsEvent_366.SetParent(g);
		logic_uScript_SendAnaliticsEvent_uScript_SendAnaliticsEvent_367.SetParent(g);
		logic_uScript_LockHudGroup_uScript_LockHudGroup_368.SetParent(g);
		logic_uScript_LockHudGroup_uScript_LockHudGroup_369.SetParent(g);
		owner_Connection_1 = parentGameObject;
		owner_Connection_5 = parentGameObject;
		owner_Connection_72 = parentGameObject;
		owner_Connection_98 = parentGameObject;
		owner_Connection_159 = parentGameObject;
	}

	public void Awake()
	{
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_12.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_25.Awake();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_31.Awake();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_33.Awake();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_40.Awake();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_42.Awake();
		logic_SubGraph_Crafting_Tutorial_Finish_SubGraph_Crafting_Tutorial_Finish_56.Awake();
		logic_SubGraph_Crafting_Tutorial_Init_SubGraph_Crafting_Tutorial_Init_67.Awake();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_101.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_152.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_176.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_177.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_181.Awake();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_188.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_197.Awake();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_344.Awake();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_346.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_12.Save_Out += SubGraph_SaveLoadBool_Save_Out_12;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_12.Load_Out += SubGraph_SaveLoadBool_Load_Out_12;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_12.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_12;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_25.Save_Out += SubGraph_SaveLoadBool_Save_Out_25;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_25.Load_Out += SubGraph_SaveLoadBool_Load_Out_25;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_25.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_25;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_30.Output1 += uScriptCon_ManualSwitch_Output1_30;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_30.Output2 += uScriptCon_ManualSwitch_Output2_30;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_30.Output3 += uScriptCon_ManualSwitch_Output3_30;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_30.Output4 += uScriptCon_ManualSwitch_Output4_30;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_30.Output5 += uScriptCon_ManualSwitch_Output5_30;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_30.Output6 += uScriptCon_ManualSwitch_Output6_30;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_30.Output7 += uScriptCon_ManualSwitch_Output7_30;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_30.Output8 += uScriptCon_ManualSwitch_Output8_30;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_31.Save_Out += SubGraph_SaveLoadInt_Save_Out_31;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_31.Load_Out += SubGraph_SaveLoadInt_Load_Out_31;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_31.Restart_Out += SubGraph_SaveLoadInt_Restart_Out_31;
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_33.Out += SubGraph_LoadObjectiveStates_Out_33;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_40.Out += SubGraph_CompleteObjectiveStage_Out_40;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_42.Out += SubGraph_CompleteObjectiveStage_Out_42;
		logic_SubGraph_Crafting_Tutorial_Finish_SubGraph_Crafting_Tutorial_Finish_56.Out += SubGraph_Crafting_Tutorial_Finish_Out_56;
		logic_SubGraph_Crafting_Tutorial_Init_SubGraph_Crafting_Tutorial_Init_67.Out += SubGraph_Crafting_Tutorial_Init_Out_67;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_101.Out += SubGraph_CompleteObjectiveStage_Out_101;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_152.Save_Out += SubGraph_SaveLoadBool_Save_Out_152;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_152.Load_Out += SubGraph_SaveLoadBool_Load_Out_152;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_152.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_152;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_176.Save_Out += SubGraph_SaveLoadBool_Save_Out_176;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_176.Load_Out += SubGraph_SaveLoadBool_Load_Out_176;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_176.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_176;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_177.Save_Out += SubGraph_SaveLoadBool_Save_Out_177;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_177.Load_Out += SubGraph_SaveLoadBool_Load_Out_177;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_177.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_177;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_181.Save_Out += SubGraph_SaveLoadBool_Save_Out_181;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_181.Load_Out += SubGraph_SaveLoadBool_Load_Out_181;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_181.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_181;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_183.Output1 += uScriptCon_ManualSwitch_Output1_183;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_183.Output2 += uScriptCon_ManualSwitch_Output2_183;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_183.Output3 += uScriptCon_ManualSwitch_Output3_183;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_183.Output4 += uScriptCon_ManualSwitch_Output4_183;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_183.Output5 += uScriptCon_ManualSwitch_Output5_183;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_183.Output6 += uScriptCon_ManualSwitch_Output6_183;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_183.Output7 += uScriptCon_ManualSwitch_Output7_183;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_183.Output8 += uScriptCon_ManualSwitch_Output8_183;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_188.Save_Out += SubGraph_SaveLoadInt_Save_Out_188;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_188.Load_Out += SubGraph_SaveLoadInt_Load_Out_188;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_188.Restart_Out += SubGraph_SaveLoadInt_Restart_Out_188;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_197.Save_Out += SubGraph_SaveLoadBool_Save_Out_197;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_197.Load_Out += SubGraph_SaveLoadBool_Load_Out_197;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_197.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_197;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_242.Output1 += uScriptCon_ManualSwitch_Output1_242;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_242.Output2 += uScriptCon_ManualSwitch_Output2_242;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_242.Output3 += uScriptCon_ManualSwitch_Output3_242;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_242.Output4 += uScriptCon_ManualSwitch_Output4_242;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_242.Output5 += uScriptCon_ManualSwitch_Output5_242;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_242.Output6 += uScriptCon_ManualSwitch_Output6_242;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_242.Output7 += uScriptCon_ManualSwitch_Output7_242;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_242.Output8 += uScriptCon_ManualSwitch_Output8_242;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_344.Out += SubGraph_AddMessageWithPadSupport_Out_344;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_344.Shown += SubGraph_AddMessageWithPadSupport_Shown_344;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_346.Out += SubGraph_AddMessageWithPadSupport_Out_346;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_346.Shown += SubGraph_AddMessageWithPadSupport_Shown_346;
	}

	public void Start()
	{
		SyncUnityHooks();
		m_RegisteredForEvents = true;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_12.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_25.Start();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_31.Start();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_33.Start();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_40.Start();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_42.Start();
		logic_SubGraph_Crafting_Tutorial_Finish_SubGraph_Crafting_Tutorial_Finish_56.Start();
		logic_SubGraph_Crafting_Tutorial_Init_SubGraph_Crafting_Tutorial_Init_67.Start();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_101.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_152.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_176.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_177.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_181.Start();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_188.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_197.Start();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_344.Start();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_346.Start();
	}

	public void OnEnable()
	{
		RegisterForUnityHooks();
		m_RegisteredForEvents = true;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_12.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_25.OnEnable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_31.OnEnable();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_33.OnEnable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_40.OnEnable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_42.OnEnable();
		logic_SubGraph_Crafting_Tutorial_Finish_SubGraph_Crafting_Tutorial_Finish_56.OnEnable();
		logic_SubGraph_Crafting_Tutorial_Init_SubGraph_Crafting_Tutorial_Init_67.OnEnable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_101.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_152.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_176.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_177.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_181.OnEnable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_188.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_197.OnEnable();
		logic_uScript_CraftingUIHighlightItem_uScript_CraftingUIHighlightItem_201.OnEnable();
		logic_uScript_CraftingUIHighlightItem_uScript_CraftingUIHighlightItem_203.OnEnable();
		logic_uScript_CraftingUIHighlightCraftButton_uScript_CraftingUIHighlightCraftButton_205.OnEnable();
		logic_uScript_CraftingUIHighlightRecipeIngredient_uScript_CraftingUIHighlightRecipeIngredient_207.OnEnable();
		logic_uScript_CraftingUIHighlightRecipeIngredient_uScript_CraftingUIHighlightRecipeIngredient_233.OnEnable();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_344.OnEnable();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_346.OnEnable();
	}

	public void OnDisable()
	{
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_2.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_12.OnDisable();
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_21.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_25.OnDisable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_31.OnDisable();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_33.OnDisable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_40.OnDisable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_42.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_44.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_47.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_55.OnDisable();
		logic_SubGraph_Crafting_Tutorial_Finish_SubGraph_Crafting_Tutorial_Finish_56.OnDisable();
		logic_SubGraph_Crafting_Tutorial_Init_SubGraph_Crafting_Tutorial_Init_67.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_80.OnDisable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_101.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_103.OnDisable();
		logic_uScript_SetTankTeam_uScript_SetTankTeam_113.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_119.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_139.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_148.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_149.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_152.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_176.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_177.OnDisable();
		logic_uScript_SetTankTeam_uScript_SetTankTeam_178.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_181.OnDisable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_188.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_193.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_197.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_198.OnDisable();
		logic_uScript_CraftingUIHighlightCraftButton_uScript_CraftingUIHighlightCraftButton_205.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_212.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_221.OnDisable();
		logic_uScript_Wait_uScript_Wait_228.OnDisable();
		logic_uScript_Wait_uScript_Wait_231.OnDisable();
		logic_uScript_Wait_uScript_Wait_232.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_239.OnDisable();
		logic_uScript_Wait_uScript_Wait_240.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_248.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_252.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_254.OnDisable();
		logic_uScript_Wait_uScript_Wait_257.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_267.OnDisable();
		logic_uScript_SetTankTeam_uScript_SetTankTeam_281.OnDisable();
		logic_uScript_GetTankBlock_uScript_GetTankBlock_329.OnDisable();
		logic_uScript_GetTankBlock_uScript_GetTankBlock_332.OnDisable();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_344.OnDisable();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_346.OnDisable();
		UnregisterEventListeners();
		m_RegisteredForEvents = false;
	}

	public void Update()
	{
		SyncEventListeners();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_12.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_25.Update();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_31.Update();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_33.Update();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_40.Update();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_42.Update();
		logic_SubGraph_Crafting_Tutorial_Finish_SubGraph_Crafting_Tutorial_Finish_56.Update();
		logic_SubGraph_Crafting_Tutorial_Init_SubGraph_Crafting_Tutorial_Init_67.Update();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_101.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_152.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_176.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_177.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_181.Update();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_188.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_197.Update();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_344.Update();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_346.Update();
	}

	public void OnDestroy()
	{
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_12.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_25.OnDestroy();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_31.OnDestroy();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_33.OnDestroy();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_40.OnDestroy();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_42.OnDestroy();
		logic_SubGraph_Crafting_Tutorial_Finish_SubGraph_Crafting_Tutorial_Finish_56.OnDestroy();
		logic_SubGraph_Crafting_Tutorial_Init_SubGraph_Crafting_Tutorial_Init_67.OnDestroy();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_101.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_152.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_176.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_177.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_181.OnDestroy();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_188.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_197.OnDestroy();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_344.OnDestroy();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_346.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_12.Save_Out -= SubGraph_SaveLoadBool_Save_Out_12;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_12.Load_Out -= SubGraph_SaveLoadBool_Load_Out_12;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_12.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_12;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_25.Save_Out -= SubGraph_SaveLoadBool_Save_Out_25;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_25.Load_Out -= SubGraph_SaveLoadBool_Load_Out_25;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_25.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_25;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_30.Output1 -= uScriptCon_ManualSwitch_Output1_30;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_30.Output2 -= uScriptCon_ManualSwitch_Output2_30;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_30.Output3 -= uScriptCon_ManualSwitch_Output3_30;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_30.Output4 -= uScriptCon_ManualSwitch_Output4_30;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_30.Output5 -= uScriptCon_ManualSwitch_Output5_30;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_30.Output6 -= uScriptCon_ManualSwitch_Output6_30;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_30.Output7 -= uScriptCon_ManualSwitch_Output7_30;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_30.Output8 -= uScriptCon_ManualSwitch_Output8_30;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_31.Save_Out -= SubGraph_SaveLoadInt_Save_Out_31;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_31.Load_Out -= SubGraph_SaveLoadInt_Load_Out_31;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_31.Restart_Out -= SubGraph_SaveLoadInt_Restart_Out_31;
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_33.Out -= SubGraph_LoadObjectiveStates_Out_33;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_40.Out -= SubGraph_CompleteObjectiveStage_Out_40;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_42.Out -= SubGraph_CompleteObjectiveStage_Out_42;
		logic_SubGraph_Crafting_Tutorial_Finish_SubGraph_Crafting_Tutorial_Finish_56.Out -= SubGraph_Crafting_Tutorial_Finish_Out_56;
		logic_SubGraph_Crafting_Tutorial_Init_SubGraph_Crafting_Tutorial_Init_67.Out -= SubGraph_Crafting_Tutorial_Init_Out_67;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_101.Out -= SubGraph_CompleteObjectiveStage_Out_101;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_152.Save_Out -= SubGraph_SaveLoadBool_Save_Out_152;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_152.Load_Out -= SubGraph_SaveLoadBool_Load_Out_152;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_152.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_152;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_176.Save_Out -= SubGraph_SaveLoadBool_Save_Out_176;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_176.Load_Out -= SubGraph_SaveLoadBool_Load_Out_176;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_176.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_176;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_177.Save_Out -= SubGraph_SaveLoadBool_Save_Out_177;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_177.Load_Out -= SubGraph_SaveLoadBool_Load_Out_177;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_177.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_177;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_181.Save_Out -= SubGraph_SaveLoadBool_Save_Out_181;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_181.Load_Out -= SubGraph_SaveLoadBool_Load_Out_181;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_181.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_181;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_183.Output1 -= uScriptCon_ManualSwitch_Output1_183;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_183.Output2 -= uScriptCon_ManualSwitch_Output2_183;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_183.Output3 -= uScriptCon_ManualSwitch_Output3_183;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_183.Output4 -= uScriptCon_ManualSwitch_Output4_183;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_183.Output5 -= uScriptCon_ManualSwitch_Output5_183;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_183.Output6 -= uScriptCon_ManualSwitch_Output6_183;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_183.Output7 -= uScriptCon_ManualSwitch_Output7_183;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_183.Output8 -= uScriptCon_ManualSwitch_Output8_183;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_188.Save_Out -= SubGraph_SaveLoadInt_Save_Out_188;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_188.Load_Out -= SubGraph_SaveLoadInt_Load_Out_188;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_188.Restart_Out -= SubGraph_SaveLoadInt_Restart_Out_188;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_197.Save_Out -= SubGraph_SaveLoadBool_Save_Out_197;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_197.Load_Out -= SubGraph_SaveLoadBool_Load_Out_197;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_197.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_197;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_242.Output1 -= uScriptCon_ManualSwitch_Output1_242;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_242.Output2 -= uScriptCon_ManualSwitch_Output2_242;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_242.Output3 -= uScriptCon_ManualSwitch_Output3_242;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_242.Output4 -= uScriptCon_ManualSwitch_Output4_242;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_242.Output5 -= uScriptCon_ManualSwitch_Output5_242;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_242.Output6 -= uScriptCon_ManualSwitch_Output6_242;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_242.Output7 -= uScriptCon_ManualSwitch_Output7_242;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_242.Output8 -= uScriptCon_ManualSwitch_Output8_242;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_344.Out -= SubGraph_AddMessageWithPadSupport_Out_344;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_344.Shown -= SubGraph_AddMessageWithPadSupport_Shown_344;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_346.Out -= SubGraph_AddMessageWithPadSupport_Out_346;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_346.Shown -= SubGraph_AddMessageWithPadSupport_Shown_346;
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

	private void Instance_BlockCraftedEvent_85(object o, uScript_BlockCraftedEvent.BlockCraftedEventArgs e)
	{
		event_UnityEngine_GameObject_BlockType_85 = e.BlockType;
		event_UnityEngine_GameObject_BlockTypeTotal_85 = e.BlockTypeTotal;
		event_UnityEngine_GameObject_BlockTotal_85 = e.BlockTotal;
		event_UnityEngine_GameObject_Block_85 = e.Block;
		event_UnityEngine_GameObject_CrafterBlock_85 = e.CrafterBlock;
		Relay_BlockCraftedEvent_85();
	}

	private void SubGraph_SaveLoadBool_Save_Out_12(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_12 = e.boolean;
		local_msgBlockRecipeShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_12;
		Relay_Save_Out_12();
	}

	private void SubGraph_SaveLoadBool_Load_Out_12(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_12 = e.boolean;
		local_msgBlockRecipeShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_12;
		Relay_Load_Out_12();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_12(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_12 = e.boolean;
		local_msgBlockRecipeShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_12;
		Relay_Restart_Out_12();
	}

	private void SubGraph_SaveLoadBool_Save_Out_25(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_25 = e.boolean;
		local_msgIntroShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_25;
		Relay_Save_Out_25();
	}

	private void SubGraph_SaveLoadBool_Load_Out_25(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_25 = e.boolean;
		local_msgIntroShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_25;
		Relay_Load_Out_25();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_25(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_25 = e.boolean;
		local_msgIntroShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_25;
		Relay_Restart_Out_25();
	}

	private void uScriptCon_ManualSwitch_Output1_30(object o, EventArgs e)
	{
		Relay_Output1_30();
	}

	private void uScriptCon_ManualSwitch_Output2_30(object o, EventArgs e)
	{
		Relay_Output2_30();
	}

	private void uScriptCon_ManualSwitch_Output3_30(object o, EventArgs e)
	{
		Relay_Output3_30();
	}

	private void uScriptCon_ManualSwitch_Output4_30(object o, EventArgs e)
	{
		Relay_Output4_30();
	}

	private void uScriptCon_ManualSwitch_Output5_30(object o, EventArgs e)
	{
		Relay_Output5_30();
	}

	private void uScriptCon_ManualSwitch_Output6_30(object o, EventArgs e)
	{
		Relay_Output6_30();
	}

	private void uScriptCon_ManualSwitch_Output7_30(object o, EventArgs e)
	{
		Relay_Output7_30();
	}

	private void uScriptCon_ManualSwitch_Output8_30(object o, EventArgs e)
	{
		Relay_Output8_30();
	}

	private void SubGraph_SaveLoadInt_Save_Out_31(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_31 = e.integer;
		local_Stage_System_Int32 = logic_SubGraph_SaveLoadInt_integer_31;
		Relay_Save_Out_31();
	}

	private void SubGraph_SaveLoadInt_Load_Out_31(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_31 = e.integer;
		local_Stage_System_Int32 = logic_SubGraph_SaveLoadInt_integer_31;
		Relay_Load_Out_31();
	}

	private void SubGraph_SaveLoadInt_Restart_Out_31(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_31 = e.integer;
		local_Stage_System_Int32 = logic_SubGraph_SaveLoadInt_integer_31;
		Relay_Restart_Out_31();
	}

	private void SubGraph_LoadObjectiveStates_Out_33(object o, SubGraph_LoadObjectiveStates.LogicEventArgs e)
	{
		Relay_Out_33();
	}

	private void SubGraph_CompleteObjectiveStage_Out_40(object o, SubGraph_CompleteObjectiveStage.LogicEventArgs e)
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_40 = e.objectiveStage;
		local_Stage_System_Int32 = logic_SubGraph_CompleteObjectiveStage_objectiveStage_40;
		Relay_Out_40();
	}

	private void SubGraph_CompleteObjectiveStage_Out_42(object o, SubGraph_CompleteObjectiveStage.LogicEventArgs e)
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_42 = e.objectiveStage;
		local_Stage_System_Int32 = logic_SubGraph_CompleteObjectiveStage_objectiveStage_42;
		Relay_Out_42();
	}

	private void SubGraph_Crafting_Tutorial_Finish_Out_56(object o, SubGraph_Crafting_Tutorial_Finish.LogicEventArgs e)
	{
		Relay_Out_56();
	}

	private void SubGraph_Crafting_Tutorial_Init_Out_67(object o, SubGraph_Crafting_Tutorial_Init.LogicEventArgs e)
	{
		logic_SubGraph_Crafting_Tutorial_Init_CraftingBaseTech_67 = e.CraftingBaseTech;
		logic_SubGraph_Crafting_Tutorial_Init_NPCTech_67 = e.NPCTech;
		local_CraftingBaseTech_Tank = logic_SubGraph_Crafting_Tutorial_Init_CraftingBaseTech_67;
		local_NPCTech_Tank = logic_SubGraph_Crafting_Tutorial_Init_NPCTech_67;
		Relay_Out_67();
	}

	private void SubGraph_CompleteObjectiveStage_Out_101(object o, SubGraph_CompleteObjectiveStage.LogicEventArgs e)
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_101 = e.objectiveStage;
		local_Stage_System_Int32 = logic_SubGraph_CompleteObjectiveStage_objectiveStage_101;
		Relay_Out_101();
	}

	private void SubGraph_SaveLoadBool_Save_Out_152(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_152 = e.boolean;
		local_ResourcesSpawned_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_152;
		Relay_Save_Out_152();
	}

	private void SubGraph_SaveLoadBool_Load_Out_152(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_152 = e.boolean;
		local_ResourcesSpawned_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_152;
		Relay_Load_Out_152();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_152(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_152 = e.boolean;
		local_ResourcesSpawned_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_152;
		Relay_Restart_Out_152();
	}

	private void SubGraph_SaveLoadBool_Save_Out_176(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_176 = e.boolean;
		local_BlockAttachedToNPC_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_176;
		Relay_Save_Out_176();
	}

	private void SubGraph_SaveLoadBool_Load_Out_176(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_176 = e.boolean;
		local_BlockAttachedToNPC_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_176;
		Relay_Load_Out_176();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_176(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_176 = e.boolean;
		local_BlockAttachedToNPC_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_176;
		Relay_Restart_Out_176();
	}

	private void SubGraph_SaveLoadBool_Save_Out_177(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_177 = e.boolean;
		local_GhostBlockSpawned_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_177;
		Relay_Save_Out_177();
	}

	private void SubGraph_SaveLoadBool_Load_Out_177(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_177 = e.boolean;
		local_GhostBlockSpawned_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_177;
		Relay_Load_Out_177();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_177(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_177 = e.boolean;
		local_GhostBlockSpawned_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_177;
		Relay_Restart_Out_177();
	}

	private void SubGraph_SaveLoadBool_Save_Out_181(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_181 = e.boolean;
		local_msgBaseFoundShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_181;
		Relay_Save_Out_181();
	}

	private void SubGraph_SaveLoadBool_Load_Out_181(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_181 = e.boolean;
		local_msgBaseFoundShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_181;
		Relay_Load_Out_181();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_181(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_181 = e.boolean;
		local_msgBaseFoundShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_181;
		Relay_Restart_Out_181();
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

	private void SubGraph_SaveLoadInt_Save_Out_188(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_188 = e.integer;
		local_Stage2Steps_System_Int32 = logic_SubGraph_SaveLoadInt_integer_188;
		Relay_Save_Out_188();
	}

	private void SubGraph_SaveLoadInt_Load_Out_188(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_188 = e.integer;
		local_Stage2Steps_System_Int32 = logic_SubGraph_SaveLoadInt_integer_188;
		Relay_Load_Out_188();
	}

	private void SubGraph_SaveLoadInt_Restart_Out_188(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_188 = e.integer;
		local_Stage2Steps_System_Int32 = logic_SubGraph_SaveLoadInt_integer_188;
		Relay_Restart_Out_188();
	}

	private void SubGraph_SaveLoadBool_Save_Out_197(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_197 = e.boolean;
		local_msgWorkOutHowToCraftShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_197;
		Relay_Save_Out_197();
	}

	private void SubGraph_SaveLoadBool_Load_Out_197(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_197 = e.boolean;
		local_msgWorkOutHowToCraftShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_197;
		Relay_Load_Out_197();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_197(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_197 = e.boolean;
		local_msgWorkOutHowToCraftShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_197;
		Relay_Restart_Out_197();
	}

	private void uScriptCon_ManualSwitch_Output1_242(object o, EventArgs e)
	{
		Relay_Output1_242();
	}

	private void uScriptCon_ManualSwitch_Output2_242(object o, EventArgs e)
	{
		Relay_Output2_242();
	}

	private void uScriptCon_ManualSwitch_Output3_242(object o, EventArgs e)
	{
		Relay_Output3_242();
	}

	private void uScriptCon_ManualSwitch_Output4_242(object o, EventArgs e)
	{
		Relay_Output4_242();
	}

	private void uScriptCon_ManualSwitch_Output5_242(object o, EventArgs e)
	{
		Relay_Output5_242();
	}

	private void uScriptCon_ManualSwitch_Output6_242(object o, EventArgs e)
	{
		Relay_Output6_242();
	}

	private void uScriptCon_ManualSwitch_Output7_242(object o, EventArgs e)
	{
		Relay_Output7_242();
	}

	private void uScriptCon_ManualSwitch_Output8_242(object o, EventArgs e)
	{
		Relay_Output8_242();
	}

	private void SubGraph_AddMessageWithPadSupport_Out_344(object o, SubGraph_AddMessageWithPadSupport.LogicEventArgs e)
	{
		logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboardReturn_344 = e.messageMouseKeyboardReturn;
		logic_SubGraph_AddMessageWithPadSupport_messageControlPadReturn_344 = e.messageControlPadReturn;
		Relay_Out_344();
	}

	private void SubGraph_AddMessageWithPadSupport_Shown_344(object o, SubGraph_AddMessageWithPadSupport.LogicEventArgs e)
	{
		logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboardReturn_344 = e.messageMouseKeyboardReturn;
		logic_SubGraph_AddMessageWithPadSupport_messageControlPadReturn_344 = e.messageControlPadReturn;
		Relay_Shown_344();
	}

	private void SubGraph_AddMessageWithPadSupport_Out_346(object o, SubGraph_AddMessageWithPadSupport.LogicEventArgs e)
	{
		logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboardReturn_346 = e.messageMouseKeyboardReturn;
		logic_SubGraph_AddMessageWithPadSupport_messageControlPadReturn_346 = e.messageControlPadReturn;
		Relay_Out_346();
	}

	private void SubGraph_AddMessageWithPadSupport_Shown_346(object o, SubGraph_AddMessageWithPadSupport.LogicEventArgs e)
	{
		logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboardReturn_346 = e.messageMouseKeyboardReturn;
		logic_SubGraph_AddMessageWithPadSupport_messageControlPadReturn_346 = e.messageControlPadReturn;
		Relay_Shown_346();
	}

	private void Relay_OnUpdate_0()
	{
		Relay_In_67();
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
			Relay_Pause_13();
		}
		if (outOfRange)
		{
			Relay_UnPause_15();
		}
	}

	private void Relay_SaveEvent_4()
	{
		Relay_Save_31();
	}

	private void Relay_LoadEvent_4()
	{
		Relay_Load_31();
	}

	private void Relay_RestartEvent_4()
	{
		Relay_Restart_31();
	}

	private void Relay_In_7()
	{
		logic_uScriptCon_CompareBool_Bool_7 = local_msgBaseFoundShown_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_7.In(logic_uScriptCon_CompareBool_Bool_7);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_7.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_7.False;
		if (num)
		{
			Relay_In_183();
		}
		if (flag)
		{
			Relay_In_208();
		}
	}

	private void Relay_True_9()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_9.True(out logic_uScriptAct_SetBool_Target_9);
		local_msgBaseFoundShown_System_Boolean = logic_uScriptAct_SetBool_Target_9;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_9.Out)
		{
			Relay_In_183();
		}
	}

	private void Relay_False_9()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_9.False(out logic_uScriptAct_SetBool_Target_9);
		local_msgBaseFoundShown_System_Boolean = logic_uScriptAct_SetBool_Target_9;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_9.Out)
		{
			Relay_In_183();
		}
	}

	private void Relay_Save_Out_12()
	{
	}

	private void Relay_Load_Out_12()
	{
		Relay_In_33();
	}

	private void Relay_Restart_Out_12()
	{
		Relay_False_28();
	}

	private void Relay_Save_12()
	{
		logic_SubGraph_SaveLoadBool_boolean_12 = local_msgBlockRecipeShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_12 = local_msgBlockRecipeShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_12.Save(ref logic_SubGraph_SaveLoadBool_boolean_12, logic_SubGraph_SaveLoadBool_boolAsVariable_12, logic_SubGraph_SaveLoadBool_uniqueID_12);
	}

	private void Relay_Load_12()
	{
		logic_SubGraph_SaveLoadBool_boolean_12 = local_msgBlockRecipeShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_12 = local_msgBlockRecipeShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_12.Load(ref logic_SubGraph_SaveLoadBool_boolean_12, logic_SubGraph_SaveLoadBool_boolAsVariable_12, logic_SubGraph_SaveLoadBool_uniqueID_12);
	}

	private void Relay_Set_True_12()
	{
		logic_SubGraph_SaveLoadBool_boolean_12 = local_msgBlockRecipeShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_12 = local_msgBlockRecipeShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_12.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_12, logic_SubGraph_SaveLoadBool_boolAsVariable_12, logic_SubGraph_SaveLoadBool_uniqueID_12);
	}

	private void Relay_Set_False_12()
	{
		logic_SubGraph_SaveLoadBool_boolean_12 = local_msgBlockRecipeShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_12 = local_msgBlockRecipeShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_12.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_12, logic_SubGraph_SaveLoadBool_boolAsVariable_12, logic_SubGraph_SaveLoadBool_uniqueID_12);
	}

	private void Relay_Pause_13()
	{
		logic_uScript_PausePopulation_uScript_PausePopulation_13.Pause();
		if (logic_uScript_PausePopulation_uScript_PausePopulation_13.Out)
		{
			Relay_True_17();
		}
	}

	private void Relay_UnPause_13()
	{
		logic_uScript_PausePopulation_uScript_PausePopulation_13.UnPause();
		if (logic_uScript_PausePopulation_uScript_PausePopulation_13.Out)
		{
			Relay_True_17();
		}
	}

	private void Relay_Pause_15()
	{
		logic_uScript_PausePopulation_uScript_PausePopulation_15.Pause();
		if (logic_uScript_PausePopulation_uScript_PausePopulation_15.Out)
		{
			Relay_In_311();
		}
	}

	private void Relay_UnPause_15()
	{
		logic_uScript_PausePopulation_uScript_PausePopulation_15.UnPause();
		if (logic_uScript_PausePopulation_uScript_PausePopulation_15.Out)
		{
			Relay_In_311();
		}
	}

	private void Relay_In_16()
	{
		logic_uScript_HideArrow_uScript_HideArrow_16.In();
		if (logic_uScript_HideArrow_uScript_HideArrow_16.Out)
		{
			Relay_In_361();
		}
	}

	private void Relay_True_17()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_17.True(out logic_uScriptAct_SetBool_Target_17);
		local_NearBase_System_Boolean = logic_uScriptAct_SetBool_Target_17;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_17.Out)
		{
			Relay_In_30();
		}
	}

	private void Relay_False_17()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_17.False(out logic_uScriptAct_SetBool_Target_17);
		local_NearBase_System_Boolean = logic_uScriptAct_SetBool_Target_17;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_17.Out)
		{
			Relay_In_30();
		}
	}

	private void Relay_In_18()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_18 = messageTag;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_18.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_18, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_18);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_18.Out)
		{
			Relay_In_16();
		}
	}

	private void Relay_True_20()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_20.True(out logic_uScriptAct_SetBool_Target_20);
		local_msgIntroShown_System_Boolean = logic_uScriptAct_SetBool_Target_20;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_20.Out)
		{
			Relay_In_44();
		}
	}

	private void Relay_False_20()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_20.False(out logic_uScriptAct_SetBool_Target_20);
		local_msgIntroShown_System_Boolean = logic_uScriptAct_SetBool_Target_20;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_20.Out)
		{
			Relay_In_44();
		}
	}

	private void Relay_In_21()
	{
		logic_uScript_IsPlayerInRangeOfTech_tech_21 = local_CraftingBaseTech_Tank;
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_21.In(logic_uScript_IsPlayerInRangeOfTech_tech_21, logic_uScript_IsPlayerInRangeOfTech_range_21, logic_uScript_IsPlayerInRangeOfTech_techs_21);
		if (logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_21.InRange)
		{
			Relay_In_23();
		}
	}

	private void Relay_In_23()
	{
		logic_uScriptCon_CompareBool_Bool_23 = local_msgIntroShown_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_23.In(logic_uScriptCon_CompareBool_Bool_23);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_23.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_23.False;
		if (num)
		{
			Relay_In_2();
		}
		if (flag)
		{
			Relay_True_20();
		}
	}

	private void Relay_Save_Out_25()
	{
		Relay_Save_181();
	}

	private void Relay_Load_Out_25()
	{
		Relay_Load_181();
	}

	private void Relay_Restart_Out_25()
	{
		Relay_Set_False_181();
	}

	private void Relay_Save_25()
	{
		logic_SubGraph_SaveLoadBool_boolean_25 = local_msgIntroShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_25 = local_msgIntroShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_25.Save(ref logic_SubGraph_SaveLoadBool_boolean_25, logic_SubGraph_SaveLoadBool_boolAsVariable_25, logic_SubGraph_SaveLoadBool_uniqueID_25);
	}

	private void Relay_Load_25()
	{
		logic_SubGraph_SaveLoadBool_boolean_25 = local_msgIntroShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_25 = local_msgIntroShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_25.Load(ref logic_SubGraph_SaveLoadBool_boolean_25, logic_SubGraph_SaveLoadBool_boolAsVariable_25, logic_SubGraph_SaveLoadBool_uniqueID_25);
	}

	private void Relay_Set_True_25()
	{
		logic_SubGraph_SaveLoadBool_boolean_25 = local_msgIntroShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_25 = local_msgIntroShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_25.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_25, logic_SubGraph_SaveLoadBool_boolAsVariable_25, logic_SubGraph_SaveLoadBool_uniqueID_25);
	}

	private void Relay_Set_False_25()
	{
		logic_SubGraph_SaveLoadBool_boolean_25 = local_msgIntroShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_25 = local_msgIntroShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_25.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_25, logic_SubGraph_SaveLoadBool_boolAsVariable_25, logic_SubGraph_SaveLoadBool_uniqueID_25);
	}

	private void Relay_True_28()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_28.True(out logic_uScriptAct_SetBool_Target_28);
		local_NearBase_System_Boolean = logic_uScriptAct_SetBool_Target_28;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_28.Out)
		{
			Relay_False_275();
		}
	}

	private void Relay_False_28()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_28.False(out logic_uScriptAct_SetBool_Target_28);
		local_NearBase_System_Boolean = logic_uScriptAct_SetBool_Target_28;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_28.Out)
		{
			Relay_False_275();
		}
	}

	private void Relay_Output1_30()
	{
		Relay_In_296();
	}

	private void Relay_Output2_30()
	{
		Relay_In_7();
	}

	private void Relay_Output3_30()
	{
		Relay_In_271();
	}

	private void Relay_Output4_30()
	{
		Relay_In_112();
	}

	private void Relay_Output5_30()
	{
	}

	private void Relay_Output6_30()
	{
	}

	private void Relay_Output7_30()
	{
	}

	private void Relay_Output8_30()
	{
	}

	private void Relay_In_30()
	{
		logic_uScriptCon_ManualSwitch_CurrentOutput_30 = local_Stage_System_Int32;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_30.In(logic_uScriptCon_ManualSwitch_CurrentOutput_30);
	}

	private void Relay_Save_Out_31()
	{
		Relay_Save_188();
	}

	private void Relay_Load_Out_31()
	{
		Relay_Load_188();
	}

	private void Relay_Restart_Out_31()
	{
		Relay_Restart_188();
	}

	private void Relay_Save_31()
	{
		logic_SubGraph_SaveLoadInt_integer_31 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_31 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_31.Save(logic_SubGraph_SaveLoadInt_restartValue_31, ref logic_SubGraph_SaveLoadInt_integer_31, logic_SubGraph_SaveLoadInt_intAsVariable_31, logic_SubGraph_SaveLoadInt_uniqueID_31);
	}

	private void Relay_Load_31()
	{
		logic_SubGraph_SaveLoadInt_integer_31 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_31 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_31.Load(logic_SubGraph_SaveLoadInt_restartValue_31, ref logic_SubGraph_SaveLoadInt_integer_31, logic_SubGraph_SaveLoadInt_intAsVariable_31, logic_SubGraph_SaveLoadInt_uniqueID_31);
	}

	private void Relay_Restart_31()
	{
		logic_SubGraph_SaveLoadInt_integer_31 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_31 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_31.Restart(logic_SubGraph_SaveLoadInt_restartValue_31, ref logic_SubGraph_SaveLoadInt_integer_31, logic_SubGraph_SaveLoadInt_intAsVariable_31, logic_SubGraph_SaveLoadInt_uniqueID_31);
	}

	private void Relay_Out_33()
	{
	}

	private void Relay_In_33()
	{
		logic_SubGraph_LoadObjectiveStates_currentObjective_33 = local_Stage_System_Int32;
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_33.In(logic_SubGraph_LoadObjectiveStates_currentObjective_33);
	}

	private void Relay_Out_40()
	{
		Relay_In_291();
	}

	private void Relay_In_40()
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_40 = local_Stage_System_Int32;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_40.In(ref logic_SubGraph_CompleteObjectiveStage_objectiveStage_40, logic_SubGraph_CompleteObjectiveStage_isFinalObjective_40);
	}

	private void Relay_Out_42()
	{
	}

	private void Relay_In_42()
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_42 = local_Stage_System_Int32;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_42.In(ref logic_SubGraph_CompleteObjectiveStage_objectiveStage_42, logic_SubGraph_CompleteObjectiveStage_isFinalObjective_42);
	}

	private void Relay_In_44()
	{
		logic_uScript_AddMessage_messageData_44 = msg01Intro;
		logic_uScript_AddMessage_speaker_44 = messageSpeaker;
		logic_uScript_AddMessage_Return_44 = logic_uScript_AddMessage_uScript_AddMessage_44.In(logic_uScript_AddMessage_messageData_44, logic_uScript_AddMessage_speaker_44);
		if (logic_uScript_AddMessage_uScript_AddMessage_44.Out)
		{
			Relay_In_2();
		}
	}

	private void Relay_In_47()
	{
		logic_uScript_AddMessage_messageData_47 = msg02BaseFound;
		logic_uScript_AddMessage_speaker_47 = messageSpeaker;
		logic_uScript_AddMessage_Return_47 = logic_uScript_AddMessage_uScript_AddMessage_47.In(logic_uScript_AddMessage_messageData_47, logic_uScript_AddMessage_speaker_47);
		if (logic_uScript_AddMessage_uScript_AddMessage_47.Shown)
		{
			Relay_In_335();
		}
	}

	private void Relay_In_50()
	{
		logic_uScriptCon_CompareBool_Bool_50 = local_NearBase_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_50.In(logic_uScriptCon_CompareBool_Bool_50);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_50.True)
		{
			Relay_In_265();
		}
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

	private void Relay_In_55()
	{
		logic_uScript_AddMessage_messageData_55 = msgLeavingMissionArea01;
		logic_uScript_AddMessage_speaker_55 = messageSpeaker;
		logic_uScript_AddMessage_Return_55 = logic_uScript_AddMessage_uScript_AddMessage_55.In(logic_uScript_AddMessage_messageData_55, logic_uScript_AddMessage_speaker_55);
		if (logic_uScript_AddMessage_uScript_AddMessage_55.Out)
		{
			Relay_False_51();
		}
	}

	private void Relay_Out_56()
	{
		Relay_In_367();
	}

	private void Relay_In_56()
	{
		logic_SubGraph_Crafting_Tutorial_Finish_craftingBaseTech_56 = local_CraftingBaseTech_Tank;
		logic_SubGraph_Crafting_Tutorial_Finish_NPCTech_56 = local_NPCTech_Tank;
		logic_SubGraph_Crafting_Tutorial_Finish_NPCFlyAwayAI_56 = NPCFlyAwayAI;
		logic_SubGraph_Crafting_Tutorial_Finish_NPCDespawnParticleEffect_56 = NPCDespawnParticleEffect;
		logic_SubGraph_Crafting_Tutorial_Finish_SubGraph_Crafting_Tutorial_Finish_56.In(logic_SubGraph_Crafting_Tutorial_Finish_craftingBaseTech_56, logic_SubGraph_Crafting_Tutorial_Finish_NPCTech_56, logic_SubGraph_Crafting_Tutorial_Finish_NPCFlyAwayAI_56, logic_SubGraph_Crafting_Tutorial_Finish_NPCDespawnParticleEffect_56);
	}

	private void Relay_Out_67()
	{
		Relay_In_285();
	}

	private void Relay_In_67()
	{
		int num = 0;
		Array array = baseSpawnData;
		if (logic_SubGraph_Crafting_Tutorial_Init_baseSpawnData_67.Length != num + array.Length)
		{
			Array.Resize(ref logic_SubGraph_Crafting_Tutorial_Init_baseSpawnData_67, num + array.Length);
		}
		Array.Copy(array, 0, logic_SubGraph_Crafting_Tutorial_Init_baseSpawnData_67, num, array.Length);
		num += array.Length;
		int num2 = 0;
		Array array2 = blockSpawnData;
		if (logic_SubGraph_Crafting_Tutorial_Init_blockSpawnData_67.Length != num2 + array2.Length)
		{
			Array.Resize(ref logic_SubGraph_Crafting_Tutorial_Init_blockSpawnData_67, num2 + array2.Length);
		}
		Array.Copy(array2, 0, logic_SubGraph_Crafting_Tutorial_Init_blockSpawnData_67, num2, array2.Length);
		num2 += array2.Length;
		int num3 = 0;
		Array nPCSpawnData = NPCSpawnData;
		if (logic_SubGraph_Crafting_Tutorial_Init_NPCSpawnData_67.Length != num3 + nPCSpawnData.Length)
		{
			Array.Resize(ref logic_SubGraph_Crafting_Tutorial_Init_NPCSpawnData_67, num3 + nPCSpawnData.Length);
		}
		Array.Copy(nPCSpawnData, 0, logic_SubGraph_Crafting_Tutorial_Init_NPCSpawnData_67, num3, nPCSpawnData.Length);
		num3 += nPCSpawnData.Length;
		logic_SubGraph_Crafting_Tutorial_Init_completedBasePreset_67 = completedBasePreset;
		logic_SubGraph_Crafting_Tutorial_Init_basePosition_67 = basePosition;
		logic_SubGraph_Crafting_Tutorial_Init_clearSceneryRadius_67 = clearSceneryRadius;
		logic_SubGraph_Crafting_Tutorial_Init_CraftingBaseTech_67 = local_CraftingBaseTech_Tank;
		logic_SubGraph_Crafting_Tutorial_Init_NPCTech_67 = local_NPCTech_Tank;
		logic_SubGraph_Crafting_Tutorial_Init_SubGraph_Crafting_Tutorial_Init_67.In(logic_SubGraph_Crafting_Tutorial_Init_baseSpawnData_67, logic_SubGraph_Crafting_Tutorial_Init_blockSpawnData_67, logic_SubGraph_Crafting_Tutorial_Init_NPCSpawnData_67, logic_SubGraph_Crafting_Tutorial_Init_completedBasePreset_67, logic_SubGraph_Crafting_Tutorial_Init_useNPCAsTutorialTech_67, logic_SubGraph_Crafting_Tutorial_Init_spawnBase_67, logic_SubGraph_Crafting_Tutorial_Init_basePosition_67, logic_SubGraph_Crafting_Tutorial_Init_clearSceneryRadius_67, ref logic_SubGraph_Crafting_Tutorial_Init_CraftingBaseTech_67, ref logic_SubGraph_Crafting_Tutorial_Init_NPCTech_67);
	}

	private void Relay_In_73()
	{
		logic_uScript_SetEncounterTarget_owner_73 = owner_Connection_72;
		logic_uScript_SetEncounterTarget_visibleObject_73 = local_NPCTech_Tank;
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_73.In(logic_uScript_SetEncounterTarget_owner_73, logic_uScript_SetEncounterTarget_visibleObject_73);
		if (logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_73.Out)
		{
			Relay_In_156();
		}
	}

	private void Relay_In_75()
	{
		logic_uScript_CompareBlockTypes_A_75 = local_92_BlockTypes;
		logic_uScript_CompareBlockTypes_B_75 = blockTypeToCraft;
		logic_uScript_CompareBlockTypes_uScript_CompareBlockTypes_75.In(logic_uScript_CompareBlockTypes_A_75, logic_uScript_CompareBlockTypes_B_75);
		if (logic_uScript_CompareBlockTypes_uScript_CompareBlockTypes_75.EqualTo)
		{
			Relay_In_109();
		}
	}

	private void Relay_In_76()
	{
		logic_uScript_HideArrow_uScript_HideArrow_76.In();
		if (logic_uScript_HideArrow_uScript_HideArrow_76.Out)
		{
			Relay_In_354();
		}
	}

	private void Relay_In_77()
	{
		logic_uScript_LockTechInteraction_tech_77 = local_CraftingBaseTech_Tank;
		logic_uScript_LockTechInteraction_uScript_LockTechInteraction_77.In(logic_uScript_LockTechInteraction_tech_77, logic_uScript_LockTechInteraction_excludedBlocks_77, logic_uScript_LockTechInteraction_excludedUniqueBlocks_77);
		if (logic_uScript_LockTechInteraction_uScript_LockTechInteraction_77.Out)
		{
			Relay_In_99();
		}
	}

	private void Relay_In_78()
	{
		logic_uScriptCon_CompareBool_Bool_78 = local_ComponentMenuOpened_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_78.In(logic_uScriptCon_CompareBool_Bool_78);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_78.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_78.False;
		if (num)
		{
			Relay_In_80();
		}
		if (flag)
		{
			Relay_In_218();
		}
	}

	private void Relay_In_80()
	{
		logic_uScript_AddMessage_messageData_80 = msg06CraftComponent;
		logic_uScript_AddMessage_speaker_80 = messageSpeaker;
		logic_uScript_AddMessage_Return_80 = logic_uScript_AddMessage_uScript_AddMessage_80.In(logic_uScript_AddMessage_messageData_80, logic_uScript_AddMessage_speaker_80);
		if (logic_uScript_AddMessage_uScript_AddMessage_80.Out)
		{
			Relay_In_95();
		}
	}

	private void Relay_BlockCraftedEvent_85()
	{
		local_92_BlockTypes = event_UnityEngine_GameObject_BlockType_85;
		Relay_In_75();
	}

	private void Relay_True_90()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_90.True(out logic_uScriptAct_SetBool_Target_90);
		local_ComponentMenuOpened_System_Boolean = logic_uScriptAct_SetBool_Target_90;
	}

	private void Relay_False_90()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_90.False(out logic_uScriptAct_SetBool_Target_90);
		local_ComponentMenuOpened_System_Boolean = logic_uScriptAct_SetBool_Target_90;
	}

	private void Relay_In_91()
	{
		logic_uScript_LockPlayerInput_uScript_LockPlayerInput_91.In(logic_uScript_LockPlayerInput_lockInput_91, logic_uScript_LockPlayerInput_includeCamera_91);
		if (logic_uScript_LockPlayerInput_uScript_LockPlayerInput_91.Out)
		{
			Relay_In_94();
		}
	}

	private void Relay_In_93()
	{
		logic_uScript_LockPlayerInput_uScript_LockPlayerInput_93.In(logic_uScript_LockPlayerInput_lockInput_93, logic_uScript_LockPlayerInput_includeCamera_93);
		if (logic_uScript_LockPlayerInput_uScript_LockPlayerInput_93.Out)
		{
			Relay_In_203();
		}
	}

	private void Relay_In_94()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_94 = messageTag;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_94.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_94, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_94);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_94.Out)
		{
			Relay_In_42();
		}
	}

	private void Relay_In_95()
	{
		logic_uScript_LockPause_uScript_LockPause_95.In(logic_uScript_LockPause_lockPause_95, logic_uScript_LockPause_disabledReason_95);
		if (logic_uScript_LockPause_uScript_LockPause_95.Out)
		{
			Relay_In_93();
		}
	}

	private void Relay_In_99()
	{
		logic_uScript_PointArrowAtVisible_targetObject_99 = local_ComponentFactoryBlock_TankBlock;
		logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_99.In(logic_uScript_PointArrowAtVisible_targetObject_99, logic_uScript_PointArrowAtVisible_timeToShowFor_99, logic_uScript_PointArrowAtVisible_offset_99);
		if (logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_99.Out)
		{
			Relay_In_352();
		}
	}

	private void Relay_In_100()
	{
		logic_uScript_LockPause_uScript_LockPause_100.In(logic_uScript_LockPause_lockPause_100, logic_uScript_LockPause_disabledReason_100);
		if (logic_uScript_LockPause_uScript_LockPause_100.Out)
		{
			Relay_In_91();
		}
	}

	private void Relay_Out_101()
	{
	}

	private void Relay_In_101()
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_101 = local_Stage_System_Int32;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_101.In(ref logic_SubGraph_CompleteObjectiveStage_objectiveStage_101, logic_SubGraph_CompleteObjectiveStage_isFinalObjective_101);
	}

	private void Relay_In_103()
	{
		logic_uScript_AddMessage_messageData_103 = msg07AttachBlock;
		logic_uScript_AddMessage_speaker_103 = messageSpeaker;
		logic_uScript_AddMessage_Return_103 = logic_uScript_AddMessage_uScript_AddMessage_103.In(logic_uScript_AddMessage_messageData_103, logic_uScript_AddMessage_speaker_103);
	}

	private void Relay_In_106()
	{
		logic_uScript_DoesTechHaveBlockAtPosition_tech_106 = local_NPCTech_Tank;
		logic_uScript_DoesTechHaveBlockAtPosition_blockType_106 = blockTypeToCraft;
		logic_uScript_DoesTechHaveBlockAtPosition_uScript_DoesTechHaveBlockAtPosition_106.In(logic_uScript_DoesTechHaveBlockAtPosition_tech_106, logic_uScript_DoesTechHaveBlockAtPosition_blockType_106, logic_uScript_DoesTechHaveBlockAtPosition_localPosition_106);
		bool num = logic_uScript_DoesTechHaveBlockAtPosition_uScript_DoesTechHaveBlockAtPosition_106.True;
		bool flag = logic_uScript_DoesTechHaveBlockAtPosition_uScript_DoesTechHaveBlockAtPosition_106.False;
		if (num)
		{
			Relay_In_158();
		}
		if (flag)
		{
			Relay_In_103();
		}
	}

	private void Relay_In_109()
	{
		logic_uScriptCon_CompareInt_A_109 = local_Stage_System_Int32;
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_109.In(logic_uScriptCon_CompareInt_A_109, logic_uScriptCon_CompareInt_B_109);
		if (logic_uScriptCon_CompareInt_uScriptCon_CompareInt_109.EqualTo)
		{
			Relay_In_101();
		}
	}

	private void Relay_In_111()
	{
		logic_uScript_SetCustomRadarTeamID_tech_111 = local_NPCTech_Tank;
		logic_uScript_SetCustomRadarTeamID_uScript_SetCustomRadarTeamID_111.In(logic_uScript_SetCustomRadarTeamID_tech_111, logic_uScript_SetCustomRadarTeamID_radarTeamID_111);
		if (logic_uScript_SetCustomRadarTeamID_uScript_SetCustomRadarTeamID_111.Out)
		{
			Relay_In_299();
		}
	}

	private void Relay_In_112()
	{
		logic_uScript_LockTech_tech_112 = local_NPCTech_Tank;
		logic_uScript_LockTech_uScript_LockTech_112.In(logic_uScript_LockTech_tech_112, logic_uScript_LockTech_lockType_112);
		if (logic_uScript_LockTech_uScript_LockTech_112.Out)
		{
			Relay_In_364();
		}
	}

	private void Relay_In_113()
	{
		logic_uScript_SetTankTeam_tank_113 = local_NPCTech_Tank;
		logic_uScript_SetTankTeam_uScript_SetTankTeam_113.In(logic_uScript_SetTankTeam_tank_113, logic_uScript_SetTankTeam_team_113);
		if (logic_uScript_SetTankTeam_uScript_SetTankTeam_113.Out)
		{
			Relay_In_111();
		}
	}

	private void Relay_In_119()
	{
		logic_uScript_AddMessage_messageData_119 = msg03OpenFabricatorMenu;
		logic_uScript_AddMessage_speaker_119 = messageSpeaker;
		logic_uScript_AddMessage_Return_119 = logic_uScript_AddMessage_uScript_AddMessage_119.In(logic_uScript_AddMessage_messageData_119, logic_uScript_AddMessage_speaker_119);
		if (logic_uScript_AddMessage_uScript_AddMessage_119.Out)
		{
			Relay_In_122();
		}
	}

	private void Relay_True_121()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_121.True(out logic_uScriptAct_SetBool_Target_121);
		local_CraftingMenuOpened_System_Boolean = logic_uScriptAct_SetBool_Target_121;
	}

	private void Relay_False_121()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_121.False(out logic_uScriptAct_SetBool_Target_121);
		local_CraftingMenuOpened_System_Boolean = logic_uScriptAct_SetBool_Target_121;
	}

	private void Relay_In_122()
	{
		logic_uScript_LockTechInteraction_tech_122 = local_CraftingBaseTech_Tank;
		logic_uScript_LockTechInteraction_uScript_LockTechInteraction_122.In(logic_uScript_LockTechInteraction_tech_122, logic_uScript_LockTechInteraction_excludedBlocks_122, logic_uScript_LockTechInteraction_excludedUniqueBlocks_122);
		if (logic_uScript_LockTechInteraction_uScript_LockTechInteraction_122.Out)
		{
			Relay_In_126();
		}
	}

	private void Relay_In_125()
	{
		logic_uScriptCon_CompareBool_Bool_125 = local_CraftingMenuOpened_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_125.In(logic_uScriptCon_CompareBool_Bool_125);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_125.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_125.False;
		if (num)
		{
			Relay_In_189();
		}
		if (flag)
		{
			Relay_In_119();
		}
	}

	private void Relay_In_126()
	{
		logic_uScript_PointArrowAtVisible_targetObject_126 = local_FabricatorBlock_TankBlock;
		logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_126.In(logic_uScript_PointArrowAtVisible_targetObject_126, logic_uScript_PointArrowAtVisible_timeToShowFor_126, logic_uScript_PointArrowAtVisible_offset_126);
		if (logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_126.Out)
		{
			Relay_In_348();
		}
	}

	private void Relay_In_128()
	{
		logic_uScript_HideArrow_uScript_HideArrow_128.In();
		if (logic_uScript_HideArrow_uScript_HideArrow_128.Out)
		{
			Relay_In_350();
		}
	}

	private void Relay_In_129()
	{
		logic_uScriptCon_CompareBool_Bool_129 = local_CraftingBlockSelected_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_129.In(logic_uScriptCon_CompareBool_Bool_129);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_129.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_129.False;
		if (num)
		{
			Relay_In_146();
		}
		if (flag)
		{
			Relay_In_344();
		}
	}

	private void Relay_In_131()
	{
		logic_uScript_LockPlayerInput_uScript_LockPlayerInput_131.In(logic_uScript_LockPlayerInput_lockInput_131, logic_uScript_LockPlayerInput_includeCamera_131);
		if (logic_uScript_LockPlayerInput_uScript_LockPlayerInput_131.Out)
		{
			Relay_In_201();
		}
	}

	private void Relay_True_135()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_135.True(out logic_uScriptAct_SetBool_Target_135);
		local_CraftingBlockSelected_System_Boolean = logic_uScriptAct_SetBool_Target_135;
	}

	private void Relay_False_135()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_135.False(out logic_uScriptAct_SetBool_Target_135);
		local_CraftingBlockSelected_System_Boolean = logic_uScriptAct_SetBool_Target_135;
	}

	private void Relay_In_137()
	{
		logic_uScript_LockPause_uScript_LockPause_137.In(logic_uScript_LockPause_lockPause_137, logic_uScript_LockPause_disabledReason_137);
		if (logic_uScript_LockPause_uScript_LockPause_137.Out)
		{
			Relay_In_369();
		}
	}

	private void Relay_In_138()
	{
		logic_uScript_LockPlayerInput_uScript_LockPlayerInput_138.In(logic_uScript_LockPlayerInput_lockInput_138, logic_uScript_LockPlayerInput_includeCamera_138);
		if (logic_uScript_LockPlayerInput_uScript_LockPlayerInput_138.Out)
		{
			Relay_In_185();
		}
	}

	private void Relay_In_139()
	{
		logic_uScript_AddMessage_messageData_139 = msg08Complete;
		logic_uScript_AddMessage_speaker_139 = messageSpeaker;
		logic_uScript_AddMessage_Return_139 = logic_uScript_AddMessage_uScript_AddMessage_139.In(logic_uScript_AddMessage_messageData_139, logic_uScript_AddMessage_speaker_139);
		if (logic_uScript_AddMessage_uScript_AddMessage_139.Shown)
		{
			Relay_In_56();
		}
	}

	private void Relay_True_143()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_143.True(out logic_uScriptAct_SetBool_Target_143);
		local_msgBlockRecipeShown_System_Boolean = logic_uScriptAct_SetBool_Target_143;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_143.Out)
		{
			Relay_In_235();
		}
	}

	private void Relay_False_143()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_143.False(out logic_uScriptAct_SetBool_Target_143);
		local_msgBlockRecipeShown_System_Boolean = logic_uScriptAct_SetBool_Target_143;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_143.Out)
		{
			Relay_In_235();
		}
	}

	private void Relay_In_146()
	{
		logic_uScriptCon_CompareBool_Bool_146 = local_msgBlockRecipeShown_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_146.In(logic_uScriptCon_CompareBool_Bool_146);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_146.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_146.False;
		if (num)
		{
			Relay_In_149();
		}
		if (flag)
		{
			Relay_In_148();
		}
	}

	private void Relay_In_148()
	{
		logic_uScript_AddMessage_messageData_148 = msg04bBlockRecipe;
		logic_uScript_AddMessage_speaker_148 = messageSpeaker;
		logic_uScript_AddMessage_Return_148 = logic_uScript_AddMessage_uScript_AddMessage_148.In(logic_uScript_AddMessage_messageData_148, logic_uScript_AddMessage_speaker_148);
		if (logic_uScript_AddMessage_uScript_AddMessage_148.Out)
		{
			Relay_In_207();
		}
	}

	private void Relay_In_149()
	{
		logic_uScript_AddMessage_messageData_149 = msg04cCraftBlock;
		logic_uScript_AddMessage_speaker_149 = messageSpeaker;
		logic_uScript_AddMessage_Return_149 = logic_uScript_AddMessage_uScript_AddMessage_149.In(logic_uScript_AddMessage_messageData_149, logic_uScript_AddMessage_speaker_149);
		if (logic_uScript_AddMessage_uScript_AddMessage_149.Out)
		{
			Relay_In_235();
		}
	}

	private void Relay_Save_Out_152()
	{
		Relay_Save_176();
	}

	private void Relay_Load_Out_152()
	{
		Relay_Load_176();
	}

	private void Relay_Restart_Out_152()
	{
		Relay_Set_False_176();
	}

	private void Relay_Save_152()
	{
		logic_SubGraph_SaveLoadBool_boolean_152 = local_ResourcesSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_152 = local_ResourcesSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_152.Save(ref logic_SubGraph_SaveLoadBool_boolean_152, logic_SubGraph_SaveLoadBool_boolAsVariable_152, logic_SubGraph_SaveLoadBool_uniqueID_152);
	}

	private void Relay_Load_152()
	{
		logic_SubGraph_SaveLoadBool_boolean_152 = local_ResourcesSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_152 = local_ResourcesSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_152.Load(ref logic_SubGraph_SaveLoadBool_boolean_152, logic_SubGraph_SaveLoadBool_boolAsVariable_152, logic_SubGraph_SaveLoadBool_uniqueID_152);
	}

	private void Relay_Set_True_152()
	{
		logic_SubGraph_SaveLoadBool_boolean_152 = local_ResourcesSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_152 = local_ResourcesSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_152.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_152, logic_SubGraph_SaveLoadBool_boolAsVariable_152, logic_SubGraph_SaveLoadBool_uniqueID_152);
	}

	private void Relay_Set_False_152()
	{
		logic_SubGraph_SaveLoadBool_boolean_152 = local_ResourcesSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_152 = local_ResourcesSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_152.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_152, logic_SubGraph_SaveLoadBool_boolAsVariable_152, logic_SubGraph_SaveLoadBool_uniqueID_152);
	}

	private void Relay_True_155()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_155.True(out logic_uScriptAct_SetBool_Target_155);
		local_ResourcesSpawned_System_Boolean = logic_uScriptAct_SetBool_Target_155;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_155.Out)
		{
			Relay_In_292();
		}
	}

	private void Relay_False_155()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_155.False(out logic_uScriptAct_SetBool_Target_155);
		local_ResourcesSpawned_System_Boolean = logic_uScriptAct_SetBool_Target_155;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_155.Out)
		{
			Relay_In_292();
		}
	}

	private void Relay_In_156()
	{
		logic_uScriptCon_CompareBool_Bool_156 = local_ResourcesSpawned_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_156.In(logic_uScriptCon_CompareBool_Bool_156);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_156.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_156.False;
		if (num)
		{
			Relay_In_329();
		}
		if (flag)
		{
			Relay_True_155();
		}
	}

	private void Relay_AtIndex_157()
	{
		int num = 0;
		Array array = local_165_TankBlockArray;
		if (logic_uScript_AccessListBlock_blockList_157.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListBlock_blockList_157, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListBlock_blockList_157, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListBlock_uScript_AccessListBlock_157.AtIndex(ref logic_uScript_AccessListBlock_blockList_157, logic_uScript_AccessListBlock_index_157, out logic_uScript_AccessListBlock_value_157);
		local_165_TankBlockArray = logic_uScript_AccessListBlock_blockList_157;
		local_GhostBlock_TankBlock = logic_uScript_AccessListBlock_value_157;
		if (logic_uScript_AccessListBlock_uScript_AccessListBlock_157.Out)
		{
			Relay_In_164();
		}
	}

	private void Relay_In_158()
	{
		logic_uScript_RemoveAllGhostBlocksOnTech_targetTech_158 = local_NPCTech_Tank;
		logic_uScript_RemoveAllGhostBlocksOnTech_uScript_RemoveAllGhostBlocksOnTech_158.In(logic_uScript_RemoveAllGhostBlocksOnTech_targetTech_158);
		if (logic_uScript_RemoveAllGhostBlocksOnTech_uScript_RemoveAllGhostBlocksOnTech_158.Out)
		{
			Relay_In_358();
		}
	}

	private void Relay_TrySpawnOnTech_162()
	{
		int num = 0;
		Array array = ghostBlock;
		if (logic_uScript_SpawnGhostBlocks_ghostBlockData_162.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SpawnGhostBlocks_ghostBlockData_162, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SpawnGhostBlocks_ghostBlockData_162, num, array.Length);
		num += array.Length;
		logic_uScript_SpawnGhostBlocks_ownerNode_162 = owner_Connection_159;
		logic_uScript_SpawnGhostBlocks_targetTech_162 = local_NPCTech_Tank;
		logic_uScript_SpawnGhostBlocks_Return_162 = logic_uScript_SpawnGhostBlocks_uScript_SpawnGhostBlocks_162.TrySpawnOnTech(logic_uScript_SpawnGhostBlocks_ghostBlockData_162, logic_uScript_SpawnGhostBlocks_ownerNode_162, logic_uScript_SpawnGhostBlocks_targetTech_162);
		local_165_TankBlockArray = logic_uScript_SpawnGhostBlocks_Return_162;
		if (logic_uScript_SpawnGhostBlocks_uScript_SpawnGhostBlocks_162.OnAlreadySpawned)
		{
			Relay_AtIndex_157();
		}
	}

	private void Relay_True_163()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_163.True(out logic_uScriptAct_SetBool_Target_163);
		local_GhostBlockSpawned_System_Boolean = logic_uScriptAct_SetBool_Target_163;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_163.Out)
		{
			Relay_TrySpawnOnTech_162();
		}
	}

	private void Relay_False_163()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_163.False(out logic_uScriptAct_SetBool_Target_163);
		local_GhostBlockSpawned_System_Boolean = logic_uScriptAct_SetBool_Target_163;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_163.Out)
		{
			Relay_TrySpawnOnTech_162();
		}
	}

	private void Relay_In_164()
	{
		logic_uScript_PointArrowAtBlock_block_164 = local_GhostBlock_TankBlock;
		logic_uScript_PointArrowAtBlock_uScript_PointArrowAtBlock_164.In(logic_uScript_PointArrowAtBlock_block_164, logic_uScript_PointArrowAtBlock_timeToShowFor_164, logic_uScript_PointArrowAtBlock_offset_164);
		if (logic_uScript_PointArrowAtBlock_uScript_PointArrowAtBlock_164.Out)
		{
			Relay_In_356();
		}
	}

	private void Relay_In_166()
	{
		logic_uScriptCon_CompareBool_Bool_166 = local_GhostBlockSpawned_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_166.In(logic_uScriptCon_CompareBool_Bool_166);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_166.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_166.False;
		if (num)
		{
			Relay_In_164();
		}
		if (flag)
		{
			Relay_True_163();
		}
	}

	private void Relay_In_170()
	{
		logic_uScriptCon_CompareBool_Bool_170 = local_BlockAttachedToNPC_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_170.In(logic_uScriptCon_CompareBool_Bool_170);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_170.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_170.False;
		if (num)
		{
			Relay_In_178();
		}
		if (flag)
		{
			Relay_In_166();
		}
	}

	private void Relay_True_171()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_171.True(out logic_uScriptAct_SetBool_Target_171);
		local_BlockAttachedToNPC_System_Boolean = logic_uScriptAct_SetBool_Target_171;
	}

	private void Relay_False_171()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_171.False(out logic_uScriptAct_SetBool_Target_171);
		local_BlockAttachedToNPC_System_Boolean = logic_uScriptAct_SetBool_Target_171;
	}

	private void Relay_Save_Out_176()
	{
		Relay_Save_177();
	}

	private void Relay_Load_Out_176()
	{
		Relay_Load_177();
	}

	private void Relay_Restart_Out_176()
	{
		Relay_Set_False_177();
	}

	private void Relay_Save_176()
	{
		logic_SubGraph_SaveLoadBool_boolean_176 = local_BlockAttachedToNPC_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_176 = local_BlockAttachedToNPC_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_176.Save(ref logic_SubGraph_SaveLoadBool_boolean_176, logic_SubGraph_SaveLoadBool_boolAsVariable_176, logic_SubGraph_SaveLoadBool_uniqueID_176);
	}

	private void Relay_Load_176()
	{
		logic_SubGraph_SaveLoadBool_boolean_176 = local_BlockAttachedToNPC_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_176 = local_BlockAttachedToNPC_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_176.Load(ref logic_SubGraph_SaveLoadBool_boolean_176, logic_SubGraph_SaveLoadBool_boolAsVariable_176, logic_SubGraph_SaveLoadBool_uniqueID_176);
	}

	private void Relay_Set_True_176()
	{
		logic_SubGraph_SaveLoadBool_boolean_176 = local_BlockAttachedToNPC_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_176 = local_BlockAttachedToNPC_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_176.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_176, logic_SubGraph_SaveLoadBool_boolAsVariable_176, logic_SubGraph_SaveLoadBool_uniqueID_176);
	}

	private void Relay_Set_False_176()
	{
		logic_SubGraph_SaveLoadBool_boolean_176 = local_BlockAttachedToNPC_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_176 = local_BlockAttachedToNPC_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_176.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_176, logic_SubGraph_SaveLoadBool_boolAsVariable_176, logic_SubGraph_SaveLoadBool_uniqueID_176);
	}

	private void Relay_Save_Out_177()
	{
		Relay_Save_25();
	}

	private void Relay_Load_Out_177()
	{
		Relay_Load_25();
	}

	private void Relay_Restart_Out_177()
	{
		Relay_Set_False_25();
	}

	private void Relay_Save_177()
	{
		logic_SubGraph_SaveLoadBool_boolean_177 = local_GhostBlockSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_177 = local_GhostBlockSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_177.Save(ref logic_SubGraph_SaveLoadBool_boolean_177, logic_SubGraph_SaveLoadBool_boolAsVariable_177, logic_SubGraph_SaveLoadBool_uniqueID_177);
	}

	private void Relay_Load_177()
	{
		logic_SubGraph_SaveLoadBool_boolean_177 = local_GhostBlockSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_177 = local_GhostBlockSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_177.Load(ref logic_SubGraph_SaveLoadBool_boolean_177, logic_SubGraph_SaveLoadBool_boolAsVariable_177, logic_SubGraph_SaveLoadBool_uniqueID_177);
	}

	private void Relay_Set_True_177()
	{
		logic_SubGraph_SaveLoadBool_boolean_177 = local_GhostBlockSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_177 = local_GhostBlockSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_177.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_177, logic_SubGraph_SaveLoadBool_boolAsVariable_177, logic_SubGraph_SaveLoadBool_uniqueID_177);
	}

	private void Relay_Set_False_177()
	{
		logic_SubGraph_SaveLoadBool_boolean_177 = local_GhostBlockSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_177 = local_GhostBlockSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_177.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_177, logic_SubGraph_SaveLoadBool_boolAsVariable_177, logic_SubGraph_SaveLoadBool_uniqueID_177);
	}

	private void Relay_In_178()
	{
		logic_uScript_SetTankTeam_tank_178 = local_NPCTech_Tank;
		logic_uScript_SetTankTeam_uScript_SetTankTeam_178.In(logic_uScript_SetTankTeam_tank_178, logic_uScript_SetTankTeam_team_178);
		if (logic_uScript_SetTankTeam_uScript_SetTankTeam_178.Out)
		{
			Relay_In_139();
		}
	}

	private void Relay_Save_Out_181()
	{
		Relay_Save_197();
	}

	private void Relay_Load_Out_181()
	{
		Relay_Load_197();
	}

	private void Relay_Restart_Out_181()
	{
		Relay_Set_False_197();
	}

	private void Relay_Save_181()
	{
		logic_SubGraph_SaveLoadBool_boolean_181 = local_msgBaseFoundShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_181 = local_msgBaseFoundShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_181.Save(ref logic_SubGraph_SaveLoadBool_boolean_181, logic_SubGraph_SaveLoadBool_boolAsVariable_181, logic_SubGraph_SaveLoadBool_uniqueID_181);
	}

	private void Relay_Load_181()
	{
		logic_SubGraph_SaveLoadBool_boolean_181 = local_msgBaseFoundShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_181 = local_msgBaseFoundShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_181.Load(ref logic_SubGraph_SaveLoadBool_boolean_181, logic_SubGraph_SaveLoadBool_boolAsVariable_181, logic_SubGraph_SaveLoadBool_uniqueID_181);
	}

	private void Relay_Set_True_181()
	{
		logic_SubGraph_SaveLoadBool_boolean_181 = local_msgBaseFoundShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_181 = local_msgBaseFoundShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_181.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_181, logic_SubGraph_SaveLoadBool_boolAsVariable_181, logic_SubGraph_SaveLoadBool_uniqueID_181);
	}

	private void Relay_Set_False_181()
	{
		logic_SubGraph_SaveLoadBool_boolean_181 = local_msgBaseFoundShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_181 = local_msgBaseFoundShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_181.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_181, logic_SubGraph_SaveLoadBool_boolAsVariable_181, logic_SubGraph_SaveLoadBool_uniqueID_181);
	}

	private void Relay_Output1_183()
	{
		Relay_In_125();
	}

	private void Relay_Output2_183()
	{
		Relay_In_78();
	}

	private void Relay_Output3_183()
	{
	}

	private void Relay_Output4_183()
	{
	}

	private void Relay_Output5_183()
	{
	}

	private void Relay_Output6_183()
	{
	}

	private void Relay_Output7_183()
	{
	}

	private void Relay_Output8_183()
	{
	}

	private void Relay_In_183()
	{
		logic_uScriptCon_ManualSwitch_CurrentOutput_183 = local_Stage2Steps_System_Int32;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_183.In(logic_uScriptCon_ManualSwitch_CurrentOutput_183);
	}

	private void Relay_In_185()
	{
		logic_uScriptAct_AddInt_v2_A_185 = local_Stage2Steps_System_Int32;
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_185.In(logic_uScriptAct_AddInt_v2_A_185, logic_uScriptAct_AddInt_v2_B_185, out logic_uScriptAct_AddInt_v2_IntResult_185, out logic_uScriptAct_AddInt_v2_FloatResult_185);
		local_Stage2Steps_System_Int32 = logic_uScriptAct_AddInt_v2_IntResult_185;
	}

	private void Relay_Save_Out_188()
	{
		Relay_Save_152();
	}

	private void Relay_Load_Out_188()
	{
		Relay_Load_152();
	}

	private void Relay_Restart_Out_188()
	{
		Relay_Set_False_152();
	}

	private void Relay_Save_188()
	{
		logic_SubGraph_SaveLoadInt_integer_188 = local_Stage2Steps_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_188 = local_Stage2Steps_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_188.Save(logic_SubGraph_SaveLoadInt_restartValue_188, ref logic_SubGraph_SaveLoadInt_integer_188, logic_SubGraph_SaveLoadInt_intAsVariable_188, logic_SubGraph_SaveLoadInt_uniqueID_188);
	}

	private void Relay_Load_188()
	{
		logic_SubGraph_SaveLoadInt_integer_188 = local_Stage2Steps_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_188 = local_Stage2Steps_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_188.Load(logic_SubGraph_SaveLoadInt_restartValue_188, ref logic_SubGraph_SaveLoadInt_integer_188, logic_SubGraph_SaveLoadInt_intAsVariable_188, logic_SubGraph_SaveLoadInt_uniqueID_188);
	}

	private void Relay_Restart_188()
	{
		logic_SubGraph_SaveLoadInt_integer_188 = local_Stage2Steps_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_188 = local_Stage2Steps_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_188.Restart(logic_SubGraph_SaveLoadInt_restartValue_188, ref logic_SubGraph_SaveLoadInt_integer_188, logic_SubGraph_SaveLoadInt_intAsVariable_188, logic_SubGraph_SaveLoadInt_uniqueID_188);
	}

	private void Relay_In_189()
	{
		logic_uScript_LockPause_uScript_LockPause_189.In(logic_uScript_LockPause_lockPause_189, logic_uScript_LockPause_disabledReason_189);
		if (logic_uScript_LockPause_uScript_LockPause_189.Out)
		{
			Relay_In_368();
		}
	}

	private void Relay_True_190()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_190.True(out logic_uScriptAct_SetBool_Target_190);
		local_msgWorkOutHowToCraftShown_System_Boolean = logic_uScriptAct_SetBool_Target_190;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_190.Out)
		{
			Relay_In_240();
		}
	}

	private void Relay_False_190()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_190.False(out logic_uScriptAct_SetBool_Target_190);
		local_msgWorkOutHowToCraftShown_System_Boolean = logic_uScriptAct_SetBool_Target_190;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_190.Out)
		{
			Relay_In_240();
		}
	}

	private void Relay_In_192()
	{
		logic_uScriptCon_CompareBool_Bool_192 = local_msgWorkOutHowToCraftShown_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_192.In(logic_uScriptCon_CompareBool_Bool_192);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_192.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_192.False;
		if (num)
		{
			Relay_In_240();
		}
		if (flag)
		{
			Relay_In_193();
		}
	}

	private void Relay_In_193()
	{
		logic_uScript_AddMessage_messageData_193 = msg06bWorkOutHowToCraft;
		logic_uScript_AddMessage_speaker_193 = messageSpeaker;
		logic_uScript_AddMessage_Return_193 = logic_uScript_AddMessage_uScript_AddMessage_193.In(logic_uScript_AddMessage_messageData_193, logic_uScript_AddMessage_speaker_193);
		if (logic_uScript_AddMessage_uScript_AddMessage_193.Shown)
		{
			Relay_True_190();
		}
	}

	private void Relay_Save_Out_197()
	{
		Relay_Save_12();
	}

	private void Relay_Load_Out_197()
	{
		Relay_Load_12();
	}

	private void Relay_Restart_Out_197()
	{
		Relay_Set_False_12();
	}

	private void Relay_Save_197()
	{
		logic_SubGraph_SaveLoadBool_boolean_197 = local_msgWorkOutHowToCraftShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_197 = local_msgWorkOutHowToCraftShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_197.Save(ref logic_SubGraph_SaveLoadBool_boolean_197, logic_SubGraph_SaveLoadBool_boolAsVariable_197, logic_SubGraph_SaveLoadBool_uniqueID_197);
	}

	private void Relay_Load_197()
	{
		logic_SubGraph_SaveLoadBool_boolean_197 = local_msgWorkOutHowToCraftShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_197 = local_msgWorkOutHowToCraftShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_197.Load(ref logic_SubGraph_SaveLoadBool_boolean_197, logic_SubGraph_SaveLoadBool_boolAsVariable_197, logic_SubGraph_SaveLoadBool_uniqueID_197);
	}

	private void Relay_Set_True_197()
	{
		logic_SubGraph_SaveLoadBool_boolean_197 = local_msgWorkOutHowToCraftShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_197 = local_msgWorkOutHowToCraftShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_197.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_197, logic_SubGraph_SaveLoadBool_boolAsVariable_197, logic_SubGraph_SaveLoadBool_uniqueID_197);
	}

	private void Relay_Set_False_197()
	{
		logic_SubGraph_SaveLoadBool_boolean_197 = local_msgWorkOutHowToCraftShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_197 = local_msgWorkOutHowToCraftShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_197.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_197, logic_SubGraph_SaveLoadBool_boolAsVariable_197, logic_SubGraph_SaveLoadBool_uniqueID_197);
	}

	private void Relay_In_198()
	{
		logic_uScript_AddMessage_messageData_198 = msg09Reward;
		logic_uScript_AddMessage_speaker_198 = messageSpeaker;
		logic_uScript_AddMessage_Return_198 = logic_uScript_AddMessage_uScript_AddMessage_198.In(logic_uScript_AddMessage_messageData_198, logic_uScript_AddMessage_speaker_198);
		if (logic_uScript_AddMessage_uScript_AddMessage_198.Out)
		{
			Relay_In_281();
		}
	}

	private void Relay_In_201()
	{
		logic_uScript_CraftingUIHighlightItem_targetMenuType_201 = local_CraftingMenu_ManHUD_HUDElementType;
		logic_uScript_CraftingUIHighlightItem_itemToHighlight_201 = blockTypeToHighlight;
		logic_uScript_CraftingUIHighlightItem_uScript_CraftingUIHighlightItem_201.In(logic_uScript_CraftingUIHighlightItem_targetMenuType_201, logic_uScript_CraftingUIHighlightItem_itemToHighlight_201);
		if (logic_uScript_CraftingUIHighlightItem_uScript_CraftingUIHighlightItem_201.Selected)
		{
			Relay_True_135();
		}
	}

	private void Relay_In_203()
	{
		logic_uScript_CraftingUIHighlightItem_itemToHighlight_203 = ComponentTypeToHighlight;
		logic_uScript_CraftingUIHighlightItem_uScript_CraftingUIHighlightItem_203.In(logic_uScript_CraftingUIHighlightItem_targetMenuType_203, logic_uScript_CraftingUIHighlightItem_itemToHighlight_203);
		if (logic_uScript_CraftingUIHighlightItem_uScript_CraftingUIHighlightItem_203.Selected)
		{
			Relay_In_205();
		}
	}

	private void Relay_In_205()
	{
		logic_uScript_CraftingUIHighlightCraftButton_uScript_CraftingUIHighlightCraftButton_205.In(logic_uScript_CraftingUIHighlightCraftButton_targetMenuType_205);
		if (logic_uScript_CraftingUIHighlightCraftButton_uScript_CraftingUIHighlightCraftButton_205.Selected)
		{
			Relay_EnableAutoCloseUI_307();
		}
	}

	private void Relay_In_207()
	{
		logic_uScript_CraftingUIHighlightRecipeIngredient_uScript_CraftingUIHighlightRecipeIngredient_207.In(logic_uScript_CraftingUIHighlightRecipeIngredient_targetMenuType_207, logic_uScript_CraftingUIHighlightRecipeIngredient_ingredientNumber_207);
		if (logic_uScript_CraftingUIHighlightRecipeIngredient_uScript_CraftingUIHighlightRecipeIngredient_207.Out)
		{
			Relay_In_231();
		}
	}

	private void Relay_In_208()
	{
		logic_uScript_LockTechInteraction_tech_208 = local_CraftingBaseTech_Tank;
		logic_uScript_LockTechInteraction_uScript_LockTechInteraction_208.In(logic_uScript_LockTechInteraction_tech_208, logic_uScript_LockTechInteraction_excludedBlocks_208, logic_uScript_LockTechInteraction_excludedUniqueBlocks_208);
		if (logic_uScript_LockTechInteraction_uScript_LockTechInteraction_208.Out)
		{
			Relay_In_47();
		}
	}

	private void Relay_In_212()
	{
		logic_uScript_AddMessage_messageData_212 = msg04dComponentExplanation;
		logic_uScript_AddMessage_speaker_212 = messageSpeaker;
		logic_uScript_AddMessage_Return_212 = logic_uScript_AddMessage_uScript_AddMessage_212.In(logic_uScript_AddMessage_messageData_212, logic_uScript_AddMessage_speaker_212);
		if (logic_uScript_AddMessage_uScript_AddMessage_212.Shown)
		{
			Relay_In_342();
		}
	}

	private void Relay_True_214()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_214.True(out logic_uScriptAct_SetBool_Target_214);
		local_msgComponentExplanationShown_System_Boolean = logic_uScriptAct_SetBool_Target_214;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_214.Out)
		{
			Relay_In_77();
		}
	}

	private void Relay_False_214()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_214.False(out logic_uScriptAct_SetBool_Target_214);
		local_msgComponentExplanationShown_System_Boolean = logic_uScriptAct_SetBool_Target_214;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_214.Out)
		{
			Relay_In_77();
		}
	}

	private void Relay_In_218()
	{
		logic_uScriptCon_CompareBool_Bool_218 = local_msgComponentExplanationShown_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_218.In(logic_uScriptCon_CompareBool_Bool_218);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_218.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_218.False;
		if (num)
		{
			Relay_In_346();
		}
		if (flag)
		{
			Relay_In_220();
		}
	}

	private void Relay_In_220()
	{
		logic_uScript_LockTechInteraction_tech_220 = local_CraftingBaseTech_Tank;
		logic_uScript_LockTechInteraction_uScript_LockTechInteraction_220.In(logic_uScript_LockTechInteraction_tech_220, logic_uScript_LockTechInteraction_excludedBlocks_220, logic_uScript_LockTechInteraction_excludedUniqueBlocks_220);
		if (logic_uScript_LockTechInteraction_uScript_LockTechInteraction_220.Out)
		{
			Relay_In_212();
		}
	}

	private void Relay_In_221()
	{
		logic_uScript_AddMessage_messageData_221 = msg06aComponentBeingCrafted;
		logic_uScript_AddMessage_speaker_221 = messageSpeaker;
		logic_uScript_AddMessage_Return_221 = logic_uScript_AddMessage_uScript_AddMessage_221.In(logic_uScript_AddMessage_messageData_221, logic_uScript_AddMessage_speaker_221);
		if (logic_uScript_AddMessage_uScript_AddMessage_221.Out)
		{
			Relay_In_283();
		}
	}

	private void Relay_In_223()
	{
		logic_uScriptCon_CompareBool_Bool_223 = local_msgComponentBeingCraftedShown_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_223.In(logic_uScriptCon_CompareBool_Bool_223);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_223.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_223.False;
		if (num)
		{
			Relay_In_283();
		}
		if (flag)
		{
			Relay_True_226();
		}
	}

	private void Relay_True_226()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_226.True(out logic_uScriptAct_SetBool_Target_226);
		local_msgComponentBeingCraftedShown_System_Boolean = logic_uScriptAct_SetBool_Target_226;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_226.Out)
		{
			Relay_In_221();
		}
	}

	private void Relay_False_226()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_226.False(out logic_uScriptAct_SetBool_Target_226);
		local_msgComponentBeingCraftedShown_System_Boolean = logic_uScriptAct_SetBool_Target_226;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_226.Out)
		{
			Relay_In_221();
		}
	}

	private void Relay_In_228()
	{
		logic_uScript_Wait_seconds_228 = TEMP_TimeWaitForComponentCrafted;
		logic_uScript_Wait_uScript_Wait_228.In(logic_uScript_Wait_seconds_228, logic_uScript_Wait_repeat_228);
		if (logic_uScript_Wait_uScript_Wait_228.Waited)
		{
			Relay_True_273();
		}
	}

	private void Relay_Unmask_230()
	{
		logic_uScript_MaskHUD_uScript_MaskHUD_230.Unmask(logic_uScript_MaskHUD_singleUnmaskedTransform_230, logic_uScript_MaskHUD_unmaskedTransforms_230);
		if (logic_uScript_MaskHUD_uScript_MaskHUD_230.Out)
		{
			Relay_In_234();
		}
	}

	private void Relay_MaskScreen_230()
	{
		logic_uScript_MaskHUD_uScript_MaskHUD_230.MaskScreen(logic_uScript_MaskHUD_singleUnmaskedTransform_230, logic_uScript_MaskHUD_unmaskedTransforms_230);
		if (logic_uScript_MaskHUD_uScript_MaskHUD_230.Out)
		{
			Relay_In_234();
		}
	}

	private void Relay_In_231()
	{
		logic_uScript_Wait_uScript_Wait_231.In(logic_uScript_Wait_seconds_231, logic_uScript_Wait_repeat_231);
		if (logic_uScript_Wait_uScript_Wait_231.Waited)
		{
			Relay_In_233();
		}
	}

	private void Relay_In_232()
	{
		logic_uScript_Wait_uScript_Wait_232.In(logic_uScript_Wait_seconds_232, logic_uScript_Wait_repeat_232);
		if (logic_uScript_Wait_uScript_Wait_232.Waited)
		{
			Relay_Unmask_230();
		}
	}

	private void Relay_In_233()
	{
		logic_uScript_CraftingUIHighlightRecipeIngredient_uScript_CraftingUIHighlightRecipeIngredient_233.In(logic_uScript_CraftingUIHighlightRecipeIngredient_targetMenuType_233, logic_uScript_CraftingUIHighlightRecipeIngredient_ingredientNumber_233);
		if (logic_uScript_CraftingUIHighlightRecipeIngredient_uScript_CraftingUIHighlightRecipeIngredient_233.Out)
		{
			Relay_In_232();
		}
	}

	private void Relay_In_234()
	{
		logic_uScript_HideHUDElement_uScript_HideHUDElement_234.In(logic_uScript_HideHUDElement_hudElement_234);
		if (logic_uScript_HideHUDElement_uScript_HideHUDElement_234.Out)
		{
			Relay_True_143();
		}
	}

	private void Relay_In_235()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_235.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_235.Out)
		{
			Relay_EnableAutoCloseUI_303();
		}
	}

	private void Relay_In_239()
	{
		logic_uScript_AddMessage_messageData_239 = msgCraftingHint01;
		logic_uScript_AddMessage_speaker_239 = messageSpeaker;
		logic_uScript_AddMessage_Return_239 = logic_uScript_AddMessage_uScript_AddMessage_239.In(logic_uScript_AddMessage_messageData_239, logic_uScript_AddMessage_speaker_239);
		if (logic_uScript_AddMessage_uScript_AddMessage_239.Out)
		{
			Relay_In_269();
		}
	}

	private void Relay_In_240()
	{
		logic_uScript_Wait_seconds_240 = timeRepeatCraftingHint;
		logic_uScript_Wait_uScript_Wait_240.In(logic_uScript_Wait_seconds_240, logic_uScript_Wait_repeat_240);
		if (logic_uScript_Wait_uScript_Wait_240.Waited)
		{
			Relay_In_242();
		}
	}

	private void Relay_Output1_242()
	{
		Relay_In_239();
	}

	private void Relay_Output2_242()
	{
		Relay_In_248();
	}

	private void Relay_Output3_242()
	{
		Relay_In_252();
	}

	private void Relay_Output4_242()
	{
		Relay_In_254();
	}

	private void Relay_Output5_242()
	{
	}

	private void Relay_Output6_242()
	{
	}

	private void Relay_Output7_242()
	{
	}

	private void Relay_Output8_242()
	{
	}

	private void Relay_In_242()
	{
		logic_uScriptCon_ManualSwitch_CurrentOutput_242 = local_CraftingHintMessage_System_Int32;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_242.In(logic_uScriptCon_ManualSwitch_CurrentOutput_242);
	}

	private void Relay_In_244()
	{
		logic_uScriptAct_AddInt_v2_A_244 = local_CraftingHintMessage_System_Int32;
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_244.In(logic_uScriptAct_AddInt_v2_A_244, logic_uScriptAct_AddInt_v2_B_244, out logic_uScriptAct_AddInt_v2_IntResult_244, out logic_uScriptAct_AddInt_v2_FloatResult_244);
		local_CraftingHintMessage_System_Int32 = logic_uScriptAct_AddInt_v2_IntResult_244;
	}

	private void Relay_In_248()
	{
		logic_uScript_AddMessage_messageData_248 = msgCraftingHint02;
		logic_uScript_AddMessage_speaker_248 = messageSpeaker;
		logic_uScript_AddMessage_Return_248 = logic_uScript_AddMessage_uScript_AddMessage_248.In(logic_uScript_AddMessage_messageData_248, logic_uScript_AddMessage_speaker_248);
		if (logic_uScript_AddMessage_uScript_AddMessage_248.Out)
		{
			Relay_In_269();
		}
	}

	private void Relay_In_252()
	{
		logic_uScript_AddMessage_messageData_252 = msgCraftingHint03;
		logic_uScript_AddMessage_speaker_252 = messageSpeaker;
		logic_uScript_AddMessage_Return_252 = logic_uScript_AddMessage_uScript_AddMessage_252.In(logic_uScript_AddMessage_messageData_252, logic_uScript_AddMessage_speaker_252);
		if (logic_uScript_AddMessage_uScript_AddMessage_252.Out)
		{
			Relay_In_269();
		}
	}

	private void Relay_In_254()
	{
		logic_uScript_AddMessage_messageData_254 = msgCraftingHint04;
		logic_uScript_AddMessage_speaker_254 = messageSpeaker;
		logic_uScript_AddMessage_Return_254 = logic_uScript_AddMessage_uScript_AddMessage_254.In(logic_uScript_AddMessage_messageData_254, logic_uScript_AddMessage_speaker_254);
		if (logic_uScript_AddMessage_uScript_AddMessage_254.Out)
		{
			Relay_In_269();
		}
	}

	private void Relay_In_255()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_255.In(logic_uScriptAct_SetInt_Value_255, out logic_uScriptAct_SetInt_Target_255);
		local_CraftingHintMessage_System_Int32 = logic_uScriptAct_SetInt_Target_255;
	}

	private void Relay_In_257()
	{
		logic_uScript_Wait_uScript_Wait_257.In(logic_uScript_Wait_seconds_257, logic_uScript_Wait_repeat_257);
		if (logic_uScript_Wait_uScript_Wait_257.Waited)
		{
			Relay_In_258();
		}
	}

	private void Relay_In_258()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_258 = messageTag;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_258.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_258, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_258);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_258.Out)
		{
			Relay_In_260();
		}
	}

	private void Relay_In_260()
	{
		logic_uScriptCon_CompareInt_A_260 = local_CraftingHintMessage_System_Int32;
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_260.In(logic_uScriptCon_CompareInt_A_260, logic_uScriptCon_CompareInt_B_260);
		bool greaterThanOrEqualTo = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_260.GreaterThanOrEqualTo;
		bool lessThan = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_260.LessThan;
		if (greaterThanOrEqualTo)
		{
			Relay_In_255();
		}
		if (lessThan)
		{
			Relay_In_244();
		}
	}

	private void Relay_In_265()
	{
		logic_uScriptCon_CompareInt_A_265 = local_Stage_System_Int32;
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_265.In(logic_uScriptCon_CompareInt_A_265, logic_uScriptCon_CompareInt_B_265);
		bool greaterThanOrEqualTo = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_265.GreaterThanOrEqualTo;
		bool lessThan = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_265.LessThan;
		if (greaterThanOrEqualTo)
		{
			Relay_In_267();
		}
		if (lessThan)
		{
			Relay_In_55();
		}
	}

	private void Relay_In_267()
	{
		logic_uScript_AddMessage_messageData_267 = msgLeavingMissionArea02;
		logic_uScript_AddMessage_speaker_267 = messageSpeaker;
		logic_uScript_AddMessage_Return_267 = logic_uScript_AddMessage_uScript_AddMessage_267.In(logic_uScript_AddMessage_messageData_267, logic_uScript_AddMessage_speaker_267);
		if (logic_uScript_AddMessage_uScript_AddMessage_267.Out)
		{
			Relay_False_51();
		}
	}

	private void Relay_In_269()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_269.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_269.Out)
		{
			Relay_In_260();
		}
	}

	private void Relay_In_271()
	{
		logic_uScriptCon_CompareBool_Bool_271 = local_ComponentCrafted_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_271.In(logic_uScriptCon_CompareBool_Bool_271);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_271.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_271.False;
		if (num)
		{
			Relay_In_298();
		}
		if (flag)
		{
			Relay_In_223();
		}
	}

	private void Relay_True_273()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_273.True(out logic_uScriptAct_SetBool_Target_273);
		local_ComponentCrafted_System_Boolean = logic_uScriptAct_SetBool_Target_273;
	}

	private void Relay_False_273()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_273.False(out logic_uScriptAct_SetBool_Target_273);
		local_ComponentCrafted_System_Boolean = logic_uScriptAct_SetBool_Target_273;
	}

	private void Relay_True_275()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_275.True(out logic_uScriptAct_SetBool_Target_275);
		local_CraftingMenuOpened_System_Boolean = logic_uScriptAct_SetBool_Target_275;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_275.Out)
		{
			Relay_False_279();
		}
	}

	private void Relay_False_275()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_275.False(out logic_uScriptAct_SetBool_Target_275);
		local_CraftingMenuOpened_System_Boolean = logic_uScriptAct_SetBool_Target_275;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_275.Out)
		{
			Relay_False_279();
		}
	}

	private void Relay_True_279()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_279.True(out logic_uScriptAct_SetBool_Target_279);
		local_CraftingBlockSelected_System_Boolean = logic_uScriptAct_SetBool_Target_279;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_279.Out)
		{
			Relay_False_280();
		}
	}

	private void Relay_False_279()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_279.False(out logic_uScriptAct_SetBool_Target_279);
		local_CraftingBlockSelected_System_Boolean = logic_uScriptAct_SetBool_Target_279;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_279.Out)
		{
			Relay_False_280();
		}
	}

	private void Relay_True_280()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_280.True(out logic_uScriptAct_SetBool_Target_280);
		local_ComponentMenuOpened_System_Boolean = logic_uScriptAct_SetBool_Target_280;
	}

	private void Relay_False_280()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_280.False(out logic_uScriptAct_SetBool_Target_280);
		local_ComponentMenuOpened_System_Boolean = logic_uScriptAct_SetBool_Target_280;
	}

	private void Relay_In_281()
	{
		logic_uScript_SetTankTeam_tank_281 = local_CraftingBaseTech_Tank;
		logic_uScript_SetTankTeam_uScript_SetTankTeam_281.In(logic_uScript_SetTankTeam_tank_281, logic_uScript_SetTankTeam_team_281);
		if (logic_uScript_SetTankTeam_uScript_SetTankTeam_281.Out)
		{
			Relay_In_365();
		}
	}

	private void Relay_In_283()
	{
		logic_uScript_LockTechInteraction_tech_283 = local_CraftingBaseTech_Tank;
		logic_uScript_LockTechInteraction_uScript_LockTechInteraction_283.In(logic_uScript_LockTechInteraction_tech_283, logic_uScript_LockTechInteraction_excludedBlocks_283, logic_uScript_LockTechInteraction_excludedUniqueBlocks_283);
		if (logic_uScript_LockTechInteraction_uScript_LockTechInteraction_283.Out)
		{
			Relay_In_228();
		}
	}

	private void Relay_In_285()
	{
		logic_uScript_LockTech_tech_285 = local_CraftingBaseTech_Tank;
		logic_uScript_LockTech_uScript_LockTech_285.In(logic_uScript_LockTech_tech_285, logic_uScript_LockTech_lockType_285);
		if (logic_uScript_LockTech_uScript_LockTech_285.Out)
		{
			Relay_In_73();
		}
	}

	private void Relay_In_287()
	{
		logic_uScript_RestrictItemPickup_tech_287 = local_CraftingBaseTech_Tank;
		int num = 0;
		Array array = baseAllowedResourceTypes;
		if (logic_uScript_RestrictItemPickup_typesToAccept_287.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_RestrictItemPickup_typesToAccept_287, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_RestrictItemPickup_typesToAccept_287, num, array.Length);
		num += array.Length;
		logic_uScript_RestrictItemPickup_uScript_RestrictItemPickup_287.In(logic_uScript_RestrictItemPickup_tech_287, logic_uScript_RestrictItemPickup_typesToAccept_287);
		if (logic_uScript_RestrictItemPickup_uScript_RestrictItemPickup_287.Out)
		{
			Relay_In_192();
		}
	}

	private void Relay_In_291()
	{
		logic_uScript_RestrictItemPickup_tech_291 = local_CraftingBaseTech_Tank;
		logic_uScript_RestrictItemPickup_uScript_RestrictItemPickup_291.In(logic_uScript_RestrictItemPickup_tech_291, logic_uScript_RestrictItemPickup_typesToAccept_291);
		if (logic_uScript_RestrictItemPickup_uScript_RestrictItemPickup_291.Out)
		{
			Relay_In_366();
		}
	}

	private void Relay_In_292()
	{
		logic_uScript_SpawnResourceListOnHolder_tech_292 = local_CraftingBaseTech_Tank;
		int num = 0;
		Array array = resourceList;
		if (logic_uScript_SpawnResourceListOnHolder_chunks_292.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SpawnResourceListOnHolder_chunks_292, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SpawnResourceListOnHolder_chunks_292, num, array.Length);
		num += array.Length;
		logic_uScript_SpawnResourceListOnHolder_uScript_SpawnResourceListOnHolder_292.In(logic_uScript_SpawnResourceListOnHolder_tech_292, logic_uScript_SpawnResourceListOnHolder_chunks_292, logic_uScript_SpawnResourceListOnHolder_blockType_292);
		if (logic_uScript_SpawnResourceListOnHolder_uScript_SpawnResourceListOnHolder_292.Out)
		{
			Relay_In_329();
		}
	}

	private void Relay_In_296()
	{
		logic_uScript_LockTechInteraction_tech_296 = local_CraftingBaseTech_Tank;
		logic_uScript_LockTechInteraction_uScript_LockTechInteraction_296.In(logic_uScript_LockTechInteraction_tech_296, logic_uScript_LockTechInteraction_excludedBlocks_296, logic_uScript_LockTechInteraction_excludedUniqueBlocks_296);
		if (logic_uScript_LockTechInteraction_uScript_LockTechInteraction_296.Out)
		{
			Relay_In_40();
		}
	}

	private void Relay_In_298()
	{
		logic_uScript_LockTechInteraction_tech_298 = local_CraftingBaseTech_Tank;
		logic_uScript_LockTechInteraction_uScript_LockTechInteraction_298.In(logic_uScript_LockTechInteraction_tech_298, logic_uScript_LockTechInteraction_excludedBlocks_298, logic_uScript_LockTechInteraction_excludedUniqueBlocks_298);
		if (logic_uScript_LockTechInteraction_uScript_LockTechInteraction_298.Out)
		{
			Relay_In_287();
		}
	}

	private void Relay_In_299()
	{
		logic_uScript_LockTechInteraction_tech_299 = local_CraftingBaseTech_Tank;
		logic_uScript_LockTechInteraction_uScript_LockTechInteraction_299.In(logic_uScript_LockTechInteraction_tech_299, logic_uScript_LockTechInteraction_excludedBlocks_299, logic_uScript_LockTechInteraction_excludedUniqueBlocks_299);
		if (logic_uScript_LockTechInteraction_uScript_LockTechInteraction_299.Out)
		{
			Relay_In_170();
		}
	}

	private void Relay_DisableAutoCloseUI_301()
	{
		logic_uScript_DisableClosingCraftingUIWhenTooFarAway_craftingBlock_301 = local_FabricatorBlock_TankBlock;
		logic_uScript_DisableClosingCraftingUIWhenTooFarAway_uScript_DisableClosingCraftingUIWhenTooFarAway_301.DisableAutoCloseUI(logic_uScript_DisableClosingCraftingUIWhenTooFarAway_craftingBlock_301);
		if (logic_uScript_DisableClosingCraftingUIWhenTooFarAway_uScript_DisableClosingCraftingUIWhenTooFarAway_301.Out)
		{
			Relay_In_336();
		}
	}

	private void Relay_EnableAutoCloseUI_301()
	{
		logic_uScript_DisableClosingCraftingUIWhenTooFarAway_craftingBlock_301 = local_FabricatorBlock_TankBlock;
		logic_uScript_DisableClosingCraftingUIWhenTooFarAway_uScript_DisableClosingCraftingUIWhenTooFarAway_301.EnableAutoCloseUI(logic_uScript_DisableClosingCraftingUIWhenTooFarAway_craftingBlock_301);
		if (logic_uScript_DisableClosingCraftingUIWhenTooFarAway_uScript_DisableClosingCraftingUIWhenTooFarAway_301.Out)
		{
			Relay_In_336();
		}
	}

	private void Relay_DisableAutoCloseUI_303()
	{
		logic_uScript_DisableClosingCraftingUIWhenTooFarAway_craftingBlock_303 = local_FabricatorBlock_TankBlock;
		logic_uScript_DisableClosingCraftingUIWhenTooFarAway_uScript_DisableClosingCraftingUIWhenTooFarAway_303.DisableAutoCloseUI(logic_uScript_DisableClosingCraftingUIWhenTooFarAway_craftingBlock_303);
		if (logic_uScript_DisableClosingCraftingUIWhenTooFarAway_uScript_DisableClosingCraftingUIWhenTooFarAway_303.Out)
		{
			Relay_In_137();
		}
	}

	private void Relay_EnableAutoCloseUI_303()
	{
		logic_uScript_DisableClosingCraftingUIWhenTooFarAway_craftingBlock_303 = local_FabricatorBlock_TankBlock;
		logic_uScript_DisableClosingCraftingUIWhenTooFarAway_uScript_DisableClosingCraftingUIWhenTooFarAway_303.EnableAutoCloseUI(logic_uScript_DisableClosingCraftingUIWhenTooFarAway_craftingBlock_303);
		if (logic_uScript_DisableClosingCraftingUIWhenTooFarAway_uScript_DisableClosingCraftingUIWhenTooFarAway_303.Out)
		{
			Relay_In_137();
		}
	}

	private void Relay_DisableAutoCloseUI_305()
	{
		logic_uScript_DisableClosingCraftingUIWhenTooFarAway_craftingBlock_305 = local_ComponentFactoryBlock_TankBlock;
		logic_uScript_DisableClosingCraftingUIWhenTooFarAway_uScript_DisableClosingCraftingUIWhenTooFarAway_305.DisableAutoCloseUI(logic_uScript_DisableClosingCraftingUIWhenTooFarAway_craftingBlock_305);
		if (logic_uScript_DisableClosingCraftingUIWhenTooFarAway_uScript_DisableClosingCraftingUIWhenTooFarAway_305.Out)
		{
			Relay_In_339();
		}
	}

	private void Relay_EnableAutoCloseUI_305()
	{
		logic_uScript_DisableClosingCraftingUIWhenTooFarAway_craftingBlock_305 = local_ComponentFactoryBlock_TankBlock;
		logic_uScript_DisableClosingCraftingUIWhenTooFarAway_uScript_DisableClosingCraftingUIWhenTooFarAway_305.EnableAutoCloseUI(logic_uScript_DisableClosingCraftingUIWhenTooFarAway_craftingBlock_305);
		if (logic_uScript_DisableClosingCraftingUIWhenTooFarAway_uScript_DisableClosingCraftingUIWhenTooFarAway_305.Out)
		{
			Relay_In_339();
		}
	}

	private void Relay_DisableAutoCloseUI_307()
	{
		logic_uScript_DisableClosingCraftingUIWhenTooFarAway_craftingBlock_307 = local_ComponentFactoryBlock_TankBlock;
		logic_uScript_DisableClosingCraftingUIWhenTooFarAway_uScript_DisableClosingCraftingUIWhenTooFarAway_307.DisableAutoCloseUI(logic_uScript_DisableClosingCraftingUIWhenTooFarAway_craftingBlock_307);
		if (logic_uScript_DisableClosingCraftingUIWhenTooFarAway_uScript_DisableClosingCraftingUIWhenTooFarAway_307.Out)
		{
			Relay_In_100();
		}
	}

	private void Relay_EnableAutoCloseUI_307()
	{
		logic_uScript_DisableClosingCraftingUIWhenTooFarAway_craftingBlock_307 = local_ComponentFactoryBlock_TankBlock;
		logic_uScript_DisableClosingCraftingUIWhenTooFarAway_uScript_DisableClosingCraftingUIWhenTooFarAway_307.EnableAutoCloseUI(logic_uScript_DisableClosingCraftingUIWhenTooFarAway_craftingBlock_307);
		if (logic_uScript_DisableClosingCraftingUIWhenTooFarAway_uScript_DisableClosingCraftingUIWhenTooFarAway_307.Out)
		{
			Relay_In_100();
		}
	}

	private void Relay_DisableAutoCloseUI_310()
	{
		logic_uScript_DisableClosingCraftingUIWhenTooFarAway_craftingBlock_310 = local_ComponentFactoryBlock_TankBlock;
		logic_uScript_DisableClosingCraftingUIWhenTooFarAway_uScript_DisableClosingCraftingUIWhenTooFarAway_310.DisableAutoCloseUI(logic_uScript_DisableClosingCraftingUIWhenTooFarAway_craftingBlock_310);
		if (logic_uScript_DisableClosingCraftingUIWhenTooFarAway_uScript_DisableClosingCraftingUIWhenTooFarAway_310.Out)
		{
			Relay_In_50();
			Relay_In_322();
			Relay_In_323();
		}
	}

	private void Relay_EnableAutoCloseUI_310()
	{
		logic_uScript_DisableClosingCraftingUIWhenTooFarAway_craftingBlock_310 = local_ComponentFactoryBlock_TankBlock;
		logic_uScript_DisableClosingCraftingUIWhenTooFarAway_uScript_DisableClosingCraftingUIWhenTooFarAway_310.EnableAutoCloseUI(logic_uScript_DisableClosingCraftingUIWhenTooFarAway_craftingBlock_310);
		if (logic_uScript_DisableClosingCraftingUIWhenTooFarAway_uScript_DisableClosingCraftingUIWhenTooFarAway_310.Out)
		{
			Relay_In_50();
			Relay_In_322();
			Relay_In_323();
		}
	}

	private void Relay_In_311()
	{
		logic_uScript_LockPause_uScript_LockPause_311.In(logic_uScript_LockPause_lockPause_311, logic_uScript_LockPause_disabledReason_311);
		if (logic_uScript_LockPause_uScript_LockPause_311.Out)
		{
			Relay_In_312();
		}
	}

	private void Relay_In_312()
	{
		logic_uScript_LockPlayerInput_uScript_LockPlayerInput_312.In(logic_uScript_LockPlayerInput_lockInput_312, logic_uScript_LockPlayerInput_includeCamera_312);
		if (logic_uScript_LockPlayerInput_uScript_LockPlayerInput_312.Out)
		{
			Relay_In_18();
		}
	}

	private void Relay_In_313()
	{
		logic_uScriptCon_CompareBool_Bool_313 = local_CraftingBlockSelected_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_313.In(logic_uScriptCon_CompareBool_Bool_313);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_313.False)
		{
			Relay_False_315();
		}
	}

	private void Relay_True_315()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_315.True(out logic_uScriptAct_SetBool_Target_315);
		local_CraftingMenuOpened_System_Boolean = logic_uScriptAct_SetBool_Target_315;
	}

	private void Relay_False_315()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_315.False(out logic_uScriptAct_SetBool_Target_315);
		local_CraftingMenuOpened_System_Boolean = logic_uScriptAct_SetBool_Target_315;
	}

	private void Relay_True_321()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_321.True(out logic_uScriptAct_SetBool_Target_321);
		local_ComponentMenuOpened_System_Boolean = logic_uScriptAct_SetBool_Target_321;
	}

	private void Relay_False_321()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_321.False(out logic_uScriptAct_SetBool_Target_321);
		local_ComponentMenuOpened_System_Boolean = logic_uScriptAct_SetBool_Target_321;
	}

	private void Relay_In_322()
	{
		logic_uScriptCon_CompareBool_Bool_322 = local_CraftingMenuOpened_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_322.In(logic_uScriptCon_CompareBool_Bool_322);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_322.True)
		{
			Relay_In_313();
		}
	}

	private void Relay_In_323()
	{
		logic_uScriptCon_CompareBool_Bool_323 = local_ComponentMenuOpened_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_323.In(logic_uScriptCon_CompareBool_Bool_323);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_323.True)
		{
			Relay_False_321();
		}
	}

	private void Relay_DisableAutoCloseUI_326()
	{
		logic_uScript_DisableClosingCraftingUIWhenTooFarAway_craftingBlock_326 = local_FabricatorBlock_TankBlock;
		logic_uScript_DisableClosingCraftingUIWhenTooFarAway_uScript_DisableClosingCraftingUIWhenTooFarAway_326.DisableAutoCloseUI(logic_uScript_DisableClosingCraftingUIWhenTooFarAway_craftingBlock_326);
		if (logic_uScript_DisableClosingCraftingUIWhenTooFarAway_uScript_DisableClosingCraftingUIWhenTooFarAway_326.Out)
		{
			Relay_EnableAutoCloseUI_310();
		}
	}

	private void Relay_EnableAutoCloseUI_326()
	{
		logic_uScript_DisableClosingCraftingUIWhenTooFarAway_craftingBlock_326 = local_FabricatorBlock_TankBlock;
		logic_uScript_DisableClosingCraftingUIWhenTooFarAway_uScript_DisableClosingCraftingUIWhenTooFarAway_326.EnableAutoCloseUI(logic_uScript_DisableClosingCraftingUIWhenTooFarAway_craftingBlock_326);
		if (logic_uScript_DisableClosingCraftingUIWhenTooFarAway_uScript_DisableClosingCraftingUIWhenTooFarAway_326.Out)
		{
			Relay_EnableAutoCloseUI_310();
		}
	}

	private void Relay_In_329()
	{
		logic_uScript_GetTankBlock_tank_329 = local_CraftingBaseTech_Tank;
		logic_uScript_GetTankBlock_Return_329 = logic_uScript_GetTankBlock_uScript_GetTankBlock_329.In(logic_uScript_GetTankBlock_tank_329, logic_uScript_GetTankBlock_blockType_329);
		local_FabricatorBlock_TankBlock = logic_uScript_GetTankBlock_Return_329;
		if (logic_uScript_GetTankBlock_uScript_GetTankBlock_329.Returned)
		{
			Relay_In_332();
		}
	}

	private void Relay_In_332()
	{
		logic_uScript_GetTankBlock_tank_332 = local_CraftingBaseTech_Tank;
		logic_uScript_GetTankBlock_Return_332 = logic_uScript_GetTankBlock_uScript_GetTankBlock_332.In(logic_uScript_GetTankBlock_tank_332, logic_uScript_GetTankBlock_blockType_332);
		local_ComponentFactoryBlock_TankBlock = logic_uScript_GetTankBlock_Return_332;
		if (logic_uScript_GetTankBlock_uScript_GetTankBlock_332.Returned)
		{
			Relay_In_21();
		}
	}

	private void Relay_In_335()
	{
		logic_uScript_HideHUDElement_hudElement_335 = local_CraftingMenu_ManHUD_HUDElementType;
		logic_uScript_HideHUDElement_uScript_HideHUDElement_335.In(logic_uScript_HideHUDElement_hudElement_335);
		if (logic_uScript_HideHUDElement_uScript_HideHUDElement_335.Out)
		{
			Relay_True_9();
		}
	}

	private void Relay_In_336()
	{
		logic_uScript_IsHUDElementLinkedToBlock_hudElement_336 = local_CraftingMenu_ManHUD_HUDElementType;
		logic_uScript_IsHUDElementLinkedToBlock_targetBlock_336 = local_FabricatorBlock_TankBlock;
		logic_uScript_IsHUDElementLinkedToBlock_uScript_IsHUDElementLinkedToBlock_336.In(logic_uScript_IsHUDElementLinkedToBlock_hudElement_336, logic_uScript_IsHUDElementLinkedToBlock_targetBlock_336);
		if (logic_uScript_IsHUDElementLinkedToBlock_uScript_IsHUDElementLinkedToBlock_336.True)
		{
			Relay_In_128();
		}
	}

	private void Relay_In_339()
	{
		logic_uScript_IsHUDElementLinkedToBlock_hudElement_339 = local_ComponentMenu_ManHUD_HUDElementType;
		logic_uScript_IsHUDElementLinkedToBlock_targetBlock_339 = local_ComponentFactoryBlock_TankBlock;
		logic_uScript_IsHUDElementLinkedToBlock_uScript_IsHUDElementLinkedToBlock_339.In(logic_uScript_IsHUDElementLinkedToBlock_hudElement_339, logic_uScript_IsHUDElementLinkedToBlock_targetBlock_339);
		if (logic_uScript_IsHUDElementLinkedToBlock_uScript_IsHUDElementLinkedToBlock_339.True)
		{
			Relay_In_76();
		}
	}

	private void Relay_In_342()
	{
		logic_uScript_HideHUDElement_hudElement_342 = local_ComponentMenu_ManHUD_HUDElementType;
		logic_uScript_HideHUDElement_uScript_HideHUDElement_342.In(logic_uScript_HideHUDElement_hudElement_342);
		if (logic_uScript_HideHUDElement_uScript_HideHUDElement_342.Out)
		{
			Relay_True_214();
		}
	}

	private void Relay_Out_344()
	{
		Relay_In_131();
	}

	private void Relay_Shown_344()
	{
	}

	private void Relay_In_344()
	{
		logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboard_344 = msg04aSelectBlockToCraft;
		logic_SubGraph_AddMessageWithPadSupport_messageControlPad_344 = msg04aSelectBlockToCraft_Pad;
		logic_SubGraph_AddMessageWithPadSupport_speaker_344 = messageSpeaker;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_344.In(logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboard_344, logic_SubGraph_AddMessageWithPadSupport_messageControlPad_344, logic_SubGraph_AddMessageWithPadSupport_speaker_344);
	}

	private void Relay_Out_346()
	{
		Relay_In_77();
	}

	private void Relay_Shown_346()
	{
	}

	private void Relay_In_346()
	{
		logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboard_346 = msg05OpenComponentMenu;
		logic_SubGraph_AddMessageWithPadSupport_messageControlPad_346 = msg05OpenComponentMenu_Pad;
		logic_SubGraph_AddMessageWithPadSupport_speaker_346 = messageSpeaker;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_346.In(logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboard_346, logic_SubGraph_AddMessageWithPadSupport_messageControlPad_346, logic_SubGraph_AddMessageWithPadSupport_speaker_346);
	}

	private void Relay_In_348()
	{
		logic_uScript_EnableGlow_targetObject_348 = local_FabricatorBlock_TankBlock;
		logic_uScript_EnableGlow_uScript_EnableGlow_348.In(logic_uScript_EnableGlow_targetObject_348, logic_uScript_EnableGlow_enable_348);
		if (logic_uScript_EnableGlow_uScript_EnableGlow_348.Out)
		{
			Relay_DisableAutoCloseUI_301();
		}
	}

	private void Relay_In_350()
	{
		logic_uScript_EnableGlow_targetObject_350 = local_FabricatorBlock_TankBlock;
		logic_uScript_EnableGlow_uScript_EnableGlow_350.In(logic_uScript_EnableGlow_targetObject_350, logic_uScript_EnableGlow_enable_350);
		if (logic_uScript_EnableGlow_uScript_EnableGlow_350.Out)
		{
			Relay_True_121();
		}
	}

	private void Relay_In_352()
	{
		logic_uScript_EnableGlow_targetObject_352 = local_ComponentFactoryBlock_TankBlock;
		logic_uScript_EnableGlow_uScript_EnableGlow_352.In(logic_uScript_EnableGlow_targetObject_352, logic_uScript_EnableGlow_enable_352);
		if (logic_uScript_EnableGlow_uScript_EnableGlow_352.Out)
		{
			Relay_DisableAutoCloseUI_305();
		}
	}

	private void Relay_In_354()
	{
		logic_uScript_EnableGlow_targetObject_354 = local_ComponentFactoryBlock_TankBlock;
		logic_uScript_EnableGlow_uScript_EnableGlow_354.In(logic_uScript_EnableGlow_targetObject_354, logic_uScript_EnableGlow_enable_354);
		if (logic_uScript_EnableGlow_uScript_EnableGlow_354.Out)
		{
			Relay_True_90();
		}
	}

	private void Relay_In_356()
	{
		logic_uScript_EnableGlow_targetObject_356 = local_GhostBlock_TankBlock;
		logic_uScript_EnableGlow_uScript_EnableGlow_356.In(logic_uScript_EnableGlow_targetObject_356, logic_uScript_EnableGlow_enable_356);
		if (logic_uScript_EnableGlow_uScript_EnableGlow_356.Out)
		{
			Relay_In_106();
		}
	}

	private void Relay_In_358()
	{
		logic_uScript_EnableGlow_targetObject_358 = local_GhostBlock_TankBlock;
		logic_uScript_EnableGlow_uScript_EnableGlow_358.In(logic_uScript_EnableGlow_targetObject_358, logic_uScript_EnableGlow_enable_358);
		if (logic_uScript_EnableGlow_uScript_EnableGlow_358.Out)
		{
			Relay_True_171();
		}
	}

	private void Relay_In_361()
	{
		logic_uScript_EnableGlow_targetObject_361 = local_FabricatorBlock_TankBlock;
		logic_uScript_EnableGlow_uScript_EnableGlow_361.In(logic_uScript_EnableGlow_targetObject_361, logic_uScript_EnableGlow_enable_361);
		if (logic_uScript_EnableGlow_uScript_EnableGlow_361.Out)
		{
			Relay_In_362();
		}
	}

	private void Relay_In_362()
	{
		logic_uScript_EnableGlow_targetObject_362 = local_ComponentFactoryBlock_TankBlock;
		logic_uScript_EnableGlow_uScript_EnableGlow_362.In(logic_uScript_EnableGlow_targetObject_362, logic_uScript_EnableGlow_enable_362);
		if (logic_uScript_EnableGlow_uScript_EnableGlow_362.Out)
		{
			Relay_EnableAutoCloseUI_326();
		}
	}

	private void Relay_In_364()
	{
		logic_uScript_SetTankHideBlockLimit_tech_364 = local_NPCTech_Tank;
		logic_uScript_SetTankHideBlockLimit_uScript_SetTankHideBlockLimit_364.In(logic_uScript_SetTankHideBlockLimit_hidden_364, logic_uScript_SetTankHideBlockLimit_tech_364);
		if (logic_uScript_SetTankHideBlockLimit_uScript_SetTankHideBlockLimit_364.Out)
		{
			Relay_In_113();
		}
	}

	private void Relay_In_365()
	{
		logic_uScript_SetTankHideBlockLimit_tech_365 = local_CraftingBaseTech_Tank;
		logic_uScript_SetTankHideBlockLimit_uScript_SetTankHideBlockLimit_365.In(logic_uScript_SetTankHideBlockLimit_hidden_365, logic_uScript_SetTankHideBlockLimit_tech_365);
	}

	private void Relay_In_366()
	{
		logic_uScript_SendAnaliticsEvent_uScript_SendAnaliticsEvent_366.In(logic_uScript_SendAnaliticsEvent_analiticsEvent_366, logic_uScript_SendAnaliticsEvent_parameterName_366, logic_uScript_SendAnaliticsEvent_parameter_366);
	}

	private void Relay_In_367()
	{
		logic_uScript_SendAnaliticsEvent_uScript_SendAnaliticsEvent_367.In(logic_uScript_SendAnaliticsEvent_analiticsEvent_367, logic_uScript_SendAnaliticsEvent_parameterName_367, logic_uScript_SendAnaliticsEvent_parameter_367);
		if (logic_uScript_SendAnaliticsEvent_uScript_SendAnaliticsEvent_367.Out)
		{
			Relay_In_198();
		}
	}

	private void Relay_In_368()
	{
		logic_uScript_LockHudGroup_uScript_LockHudGroup_368.In(logic_uScript_LockHudGroup_group_368, logic_uScript_LockHudGroup_locked_368);
		if (logic_uScript_LockHudGroup_uScript_LockHudGroup_368.Out)
		{
			Relay_In_129();
		}
	}

	private void Relay_In_369()
	{
		logic_uScript_LockHudGroup_uScript_LockHudGroup_369.In(logic_uScript_LockHudGroup_group_369, logic_uScript_LockHudGroup_locked_369);
		if (logic_uScript_LockHudGroup_uScript_LockHudGroup_369.Out)
		{
			Relay_In_138();
		}
	}
}
