using System;
using BehaviorDesigner.Runtime;
using UnityEngine;

[Serializable]
[NodePath("Graphs")]
[FriendlyName("Untitled", "")]
public class Mission_Grade1_Crafting_Mission_Template : uScriptLogic
{
	private GameObject parentGameObject;

	private uScript_GUI thisScriptsOnGuiListener;

	private bool m_RegisteredForEvents;

	[Multiline(3)]
	public string BasePosition = "";

	[Multiline(3)]
	public string BaseVFXSpawn = "";

	public BlockTypes BlockTypeFusionMachine;

	public ItemTypeInfo blockTypeToHighlight;

	public float clearSceneryRadius;

	public SpawnTechData[] CraftingBaseSpawnData = new SpawnTechData[0];

	public float distBaseFound;

	public SpawnTechData[] EnemyTechData = new SpawnTechData[0];

	private string local_126_System_String = "";

	private string local_129_System_String = "IsAlive: ";

	private Tank[] local_221_TankArray = new Tank[0];

	private Tank[] local_229_TankArray = new Tank[0];

	private bool local_AllBlocksCrafted_System_Boolean;

	private bool local_CanInteractWithFusionMachine_System_Boolean;

	private TankBlock local_craftedCacheBlock1_TankBlock;

	private TankBlock local_craftedCacheBlock2_TankBlock;

	private TankBlock local_craftedCacheBlock3_TankBlock;

	private TankBlock local_craftedCacheBlock4_TankBlock;

	private TankBlock local_CrafterFromEvent_TankBlock;

	private Tank local_CraftingBaseTech_Tank;

	private bool local_CraftingInProgress_System_Boolean;

	private ManHUD.HUDElementType local_CraftingMenu_ManHUD_HUDElementType = ManHUD.HUDElementType.BlockRecipeSelect;

	private bool local_CraftingMenuOpened_System_Boolean;

	private int local_CurrentAmountType_System_Int32;

	private Tank[] local_Enemy_TankArray = new Tank[0];

	private bool local_EnemyAlive_System_Boolean;

	private Tank local_FusionCraftingBaseTech_Tank;

	private TankBlock local_FusionMachineBlock_TankBlock;

	private bool local_FusionMachineSpawned_System_Boolean;

	private bool local_Initialize_System_Boolean;

	private TankBlock local_lastCraftedBlock_TankBlock;

	private BlockTypes local_lastCraftedBlockType_BlockTypes;

	private bool local_msgBaseFoundShown_System_Boolean;

	private bool local_msgIntroShown_System_Boolean;

	private bool local_NearBase_System_Boolean;

	private Tank local_NPCTech_Tank;

	private int local_Stage_System_Int32 = 1;

	private int local_Stage3_System_Int32 = 1;

	public uScript_AddMessage.MessageSpeaker messageSpeaker;

	[Multiline(3)]
	public string messageTag = "";

	public uScript_AddMessage.MessageData msg01Intro;

	public uScript_AddMessage.MessageData msg02BaseFound;

	public uScript_AddMessage.MessageData msg03OpenMenu;

	public uScript_AddMessage.MessageData msg03OpenMenu_Pad;

	public uScript_AddMessage.MessageData msg04CraftBlock;

	public uScript_AddMessage.MessageData msg04CraftBlock_Pad;

	public uScript_AddMessage.MessageData msg05CraftAmountOfBlocksNeeded;

	public uScript_AddMessage.MessageData msg06AllBlocksCrafted;

	public uScript_AddMessage.MessageData msg07Complete;

	public uScript_AddMessage.MessageData msgLeavingMissionArea;

	public uScript_AddMessage.MessageData msgSpawnMinion;

	public Transform NPCDespawnParticleEffect;

	public ExternalBehaviorTree NPCFlyAwayAI;

	public SpawnTechData[] NPCSpawnData = new SpawnTechData[0];

	public float ParticleTimer = 2.5f;

	public int targetAmount = 3;

	public BlockTypes targetBlockType;

	private GameObject owner_Connection_8;

	private GameObject owner_Connection_14;

	private GameObject owner_Connection_34;

	private GameObject owner_Connection_155;

	private GameObject owner_Connection_165;

	private GameObject owner_Connection_169;

	private GameObject owner_Connection_182;

	private GameObject owner_Connection_184;

	private GameObject owner_Connection_199;

	private GameObject owner_Connection_204;

	private GameObject owner_Connection_206;

	private GameObject owner_Connection_208;

	private GameObject owner_Connection_217;

	private GameObject owner_Connection_227;

	private GameObject owner_Connection_236;

	private GameObject owner_Connection_272;

	private GameObject owner_Connection_329;

	private uScript_SetEncounterTarget logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_0 = new uScript_SetEncounterTarget();

	private GameObject logic_uScript_SetEncounterTarget_owner_0;

	private object logic_uScript_SetEncounterTarget_visibleObject_0 = "";

	private bool logic_uScript_SetEncounterTarget_Out_0 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_2 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_2 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_2 = true;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_2 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_4 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_4;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_4 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_4 = "msgIntroShown";

	private SubGraph_CompleteObjectiveStage logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_7 = new SubGraph_CompleteObjectiveStage();

	private int logic_SubGraph_CompleteObjectiveStage_objectiveStage_7;

	private bool logic_SubGraph_CompleteObjectiveStage_isFinalObjective_7;

	private SubGraph_SaveLoadInt logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_11 = new SubGraph_SaveLoadInt();

	private int logic_SubGraph_SaveLoadInt_restartValue_11 = 1;

	private int logic_SubGraph_SaveLoadInt_integer_11;

	private object logic_SubGraph_SaveLoadInt_intAsVariable_11 = "";

	private string logic_SubGraph_SaveLoadInt_uniqueID_11 = "Stage";

	private uScript_HideArrow logic_uScript_HideArrow_uScript_HideArrow_12 = new uScript_HideArrow();

	private bool logic_uScript_HideArrow_Out_12 = true;

	private uScript_PausePopulation logic_uScript_PausePopulation_uScript_PausePopulation_13 = new uScript_PausePopulation();

	private bool logic_uScript_PausePopulation_Out_13 = true;

	private uScriptCon_ManualSwitch logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_15 = new uScriptCon_ManualSwitch();

	private int logic_uScriptCon_ManualSwitch_CurrentOutput_15;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_20 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_20;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_20 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_20 = "msgBaseFoundShown";

	private uScript_IsPlayerInRangeOfTech logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_22 = new uScript_IsPlayerInRangeOfTech();

	private Tank logic_uScript_IsPlayerInRangeOfTech_tech_22;

	private float logic_uScript_IsPlayerInRangeOfTech_range_22 = 100f;

	private Tank[] logic_uScript_IsPlayerInRangeOfTech_techs_22 = new Tank[0];

	private bool logic_uScript_IsPlayerInRangeOfTech_Out_22 = true;

	private bool logic_uScript_IsPlayerInRangeOfTech_InRange_22 = true;

	private bool logic_uScript_IsPlayerInRangeOfTech_OutOfRange_22 = true;

	private uScript_PausePopulation logic_uScript_PausePopulation_uScript_PausePopulation_24 = new uScript_PausePopulation();

	private bool logic_uScript_PausePopulation_Out_24 = true;

	private uScript_IsPlayerInRangeOfTech logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_25 = new uScript_IsPlayerInRangeOfTech();

	private Tank logic_uScript_IsPlayerInRangeOfTech_tech_25;

	private float logic_uScript_IsPlayerInRangeOfTech_range_25;

	private Tank[] logic_uScript_IsPlayerInRangeOfTech_techs_25 = new Tank[0];

	private bool logic_uScript_IsPlayerInRangeOfTech_Out_25 = true;

	private bool logic_uScript_IsPlayerInRangeOfTech_InRange_25 = true;

	private bool logic_uScript_IsPlayerInRangeOfTech_OutOfRange_25 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_26 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_26;

	private bool logic_uScriptAct_SetBool_Out_26 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_26 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_26 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_27 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_27;

	private bool logic_uScriptCon_CompareBool_True_27 = true;

	private bool logic_uScriptCon_CompareBool_False_27 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_31 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_31;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_31;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_31;

	private bool logic_uScript_AddMessage_Out_31 = true;

	private bool logic_uScript_AddMessage_Shown_31 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_33 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_33;

	private bool logic_uScriptAct_SetBool_Out_33 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_33 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_33 = true;

	private uScript_LockTechInteraction logic_uScript_LockTechInteraction_uScript_LockTechInteraction_35 = new uScript_LockTechInteraction();

	private Tank logic_uScript_LockTechInteraction_tech_35;

	private BlockTypes[] logic_uScript_LockTechInteraction_excludedBlocks_35 = new BlockTypes[0];

	private TankBlock[] logic_uScript_LockTechInteraction_excludedUniqueBlocks_35 = new TankBlock[0];

	private bool logic_uScript_LockTechInteraction_Out_35 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_36 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_36;

	private bool logic_uScriptAct_SetBool_Out_36 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_36 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_36 = true;

	private uScript_LockTechStacks logic_uScript_LockTechStacks_uScript_LockTechStacks_37 = new uScript_LockTechStacks();

	private Tank logic_uScript_LockTechStacks_tech_37;

	private bool logic_uScript_LockTechStacks_Out_37 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_38 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_38;

	private bool logic_uScriptAct_SetBool_Out_38 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_38 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_38 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_42 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_42;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_42;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_42;

	private bool logic_uScript_AddMessage_Out_42 = true;

	private bool logic_uScript_AddMessage_Shown_42 = true;

	private SubGraph_LoadObjectiveStates logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_45 = new SubGraph_LoadObjectiveStates();

	private int logic_SubGraph_LoadObjectiveStates_currentObjective_45;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_46 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_46;

	private bool logic_uScriptCon_CompareBool_True_46 = true;

	private bool logic_uScriptCon_CompareBool_False_46 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_47 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_47 = "";

	private bool logic_uScript_EnableGlow_enable_47 = true;

	private bool logic_uScript_EnableGlow_Out_47 = true;

	private uScript_IsHUDElementLinkedToBlock logic_uScript_IsHUDElementLinkedToBlock_uScript_IsHUDElementLinkedToBlock_48 = new uScript_IsHUDElementLinkedToBlock();

	private ManHUD.HUDElementType logic_uScript_IsHUDElementLinkedToBlock_hudElement_48;

	private TankBlock logic_uScript_IsHUDElementLinkedToBlock_targetBlock_48;

	private bool logic_uScript_IsHUDElementLinkedToBlock_True_48 = true;

	private bool logic_uScript_IsHUDElementLinkedToBlock_False_48 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_49 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_49 = "";

	private bool logic_uScript_EnableGlow_enable_49;

	private bool logic_uScript_EnableGlow_Out_49 = true;

	private uScript_DisableClosingCraftingUIWhenTooFarAway logic_uScript_DisableClosingCraftingUIWhenTooFarAway_uScript_DisableClosingCraftingUIWhenTooFarAway_52 = new uScript_DisableClosingCraftingUIWhenTooFarAway();

	private TankBlock logic_uScript_DisableClosingCraftingUIWhenTooFarAway_craftingBlock_52;

	private bool logic_uScript_DisableClosingCraftingUIWhenTooFarAway_Out_52 = true;

	private uScript_PointArrowAtVisible logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_53 = new uScript_PointArrowAtVisible();

	private object logic_uScript_PointArrowAtVisible_targetObject_53 = "";

	private float logic_uScript_PointArrowAtVisible_timeToShowFor_53 = -1f;

	private Vector3 logic_uScript_PointArrowAtVisible_offset_53 = new Vector3(0f, 0f, 0f);

	private bool logic_uScript_PointArrowAtVisible_Out_53 = true;

	private SubGraph_AddMessageWithPadSupport logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_56 = new SubGraph_AddMessageWithPadSupport();

	private uScript_AddMessage.MessageData logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboard_56;

	private uScript_AddMessage.MessageData logic_SubGraph_AddMessageWithPadSupport_messageControlPad_56;

	private uScript_AddMessage.MessageSpeaker logic_SubGraph_AddMessageWithPadSupport_speaker_56;

	private ManOnScreenMessages.OnScreenMessage logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboardReturn_56;

	private ManOnScreenMessages.OnScreenMessage logic_SubGraph_AddMessageWithPadSupport_messageControlPadReturn_56;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_58 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_58;

	private bool logic_uScriptAct_SetBool_Out_58 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_58 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_58 = true;

	private uScript_HideArrow logic_uScript_HideArrow_uScript_HideArrow_59 = new uScript_HideArrow();

	private bool logic_uScript_HideArrow_Out_59 = true;

	private uScript_HideHUDElement logic_uScript_HideHUDElement_uScript_HideHUDElement_61 = new uScript_HideHUDElement();

	private ManHUD.HUDElementType logic_uScript_HideHUDElement_hudElement_61;

	private bool logic_uScript_HideHUDElement_Out_61 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_68 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_68;

	private bool logic_uScriptCon_CompareBool_True_68 = true;

	private bool logic_uScriptCon_CompareBool_False_68 = true;

	private uScript_LockPlayerInput logic_uScript_LockPlayerInput_uScript_LockPlayerInput_69 = new uScript_LockPlayerInput();

	private bool logic_uScript_LockPlayerInput_lockInput_69 = true;

	private bool logic_uScript_LockPlayerInput_includeCamera_69 = true;

	private bool logic_uScript_LockPlayerInput_Out_69 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_71 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_71;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_71;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_71;

	private bool logic_uScript_AddMessage_Out_71 = true;

	private bool logic_uScript_AddMessage_Shown_71 = true;

	private uScript_LockPlayerInput logic_uScript_LockPlayerInput_uScript_LockPlayerInput_72 = new uScript_LockPlayerInput();

	private bool logic_uScript_LockPlayerInput_lockInput_72;

	private bool logic_uScript_LockPlayerInput_includeCamera_72;

	private bool logic_uScript_LockPlayerInput_Out_72 = true;

	private uScript_CraftingUIHighlightItem logic_uScript_CraftingUIHighlightItem_uScript_CraftingUIHighlightItem_74 = new uScript_CraftingUIHighlightItem();

	private ManHUD.HUDElementType logic_uScript_CraftingUIHighlightItem_targetMenuType_74 = ManHUD.HUDElementType.BlockRecipeSelect;

	private ItemTypeInfo logic_uScript_CraftingUIHighlightItem_itemToHighlight_74;

	private bool logic_uScript_CraftingUIHighlightItem_Out_74 = true;

	private bool logic_uScript_CraftingUIHighlightItem_Waiting_74 = true;

	private bool logic_uScript_CraftingUIHighlightItem_Selected_74 = true;

	private uScript_LockPause logic_uScript_LockPause_uScript_LockPause_78 = new uScript_LockPause();

	private bool logic_uScript_LockPause_lockPause_78;

	private ManPauseGame.DisablePauseReason logic_uScript_LockPause_disabledReason_78 = ManPauseGame.DisablePauseReason.Tutorial;

	private bool logic_uScript_LockPause_Out_78 = true;

	private uScript_LockHudGroup logic_uScript_LockHudGroup_uScript_LockHudGroup_79 = new uScript_LockHudGroup();

	private ManHUD.HUDGroup logic_uScript_LockHudGroup_group_79;

	private bool logic_uScript_LockHudGroup_locked_79 = true;

	private bool logic_uScript_LockHudGroup_Out_79 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_80 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_80;

	private bool logic_uScriptAct_SetBool_Out_80 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_80 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_80 = true;

	private uScript_LockPause logic_uScript_LockPause_uScript_LockPause_82 = new uScript_LockPause();

	private bool logic_uScript_LockPause_lockPause_82 = true;

	private ManPauseGame.DisablePauseReason logic_uScript_LockPause_disabledReason_82 = ManPauseGame.DisablePauseReason.Tutorial;

	private bool logic_uScript_LockPause_Out_82 = true;

	private SubGraph_AddMessageWithPadSupport logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_83 = new SubGraph_AddMessageWithPadSupport();

	private uScript_AddMessage.MessageData logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboard_83;

	private uScript_AddMessage.MessageData logic_SubGraph_AddMessageWithPadSupport_messageControlPad_83;

	private uScript_AddMessage.MessageSpeaker logic_SubGraph_AddMessageWithPadSupport_speaker_83;

	private ManOnScreenMessages.OnScreenMessage logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboardReturn_83;

	private ManOnScreenMessages.OnScreenMessage logic_SubGraph_AddMessageWithPadSupport_messageControlPadReturn_83;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_84 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_84 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_84 = true;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_84 = true;

	private uScript_LockTechInteraction logic_uScript_LockTechInteraction_uScript_LockTechInteraction_86 = new uScript_LockTechInteraction();

	private Tank logic_uScript_LockTechInteraction_tech_86;

	private BlockTypes[] logic_uScript_LockTechInteraction_excludedBlocks_86 = new BlockTypes[0];

	private TankBlock[] logic_uScript_LockTechInteraction_excludedUniqueBlocks_86 = new TankBlock[0];

	private bool logic_uScript_LockTechInteraction_Out_86 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_87 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_87;

	private bool logic_uScriptCon_CompareBool_True_87 = true;

	private bool logic_uScriptCon_CompareBool_False_87 = true;

	private uScript_LockHudGroup logic_uScript_LockHudGroup_uScript_LockHudGroup_89 = new uScript_LockHudGroup();

	private ManHUD.HUDGroup logic_uScript_LockHudGroup_group_89;

	private bool logic_uScript_LockHudGroup_locked_89;

	private bool logic_uScript_LockHudGroup_Out_89 = true;

	private uScript_DisableClosingCraftingUIWhenTooFarAway logic_uScript_DisableClosingCraftingUIWhenTooFarAway_uScript_DisableClosingCraftingUIWhenTooFarAway_91 = new uScript_DisableClosingCraftingUIWhenTooFarAway();

	private TankBlock logic_uScript_DisableClosingCraftingUIWhenTooFarAway_craftingBlock_91;

	private bool logic_uScript_DisableClosingCraftingUIWhenTooFarAway_Out_91 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_96 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_96;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_96 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_96 = "CraftingMenuOpened";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_97 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_97;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_97 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_97 = "AllBlocksCrafted";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_98 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_98;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_98 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_98 = "CraftingInProgress";

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_103 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_103;

	private bool logic_uScriptCon_CompareBool_True_103 = true;

	private bool logic_uScriptCon_CompareBool_False_103 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_104 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_104;

	private bool logic_uScriptAct_SetBool_Out_104 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_104 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_104 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_105 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_105;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_105;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_105;

	private bool logic_uScript_AddMessage_Out_105 = true;

	private bool logic_uScript_AddMessage_Shown_105 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_106 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_106 = true;

	private uScript_DisableClosingCraftingUIWhenTooFarAway logic_uScript_DisableClosingCraftingUIWhenTooFarAway_uScript_DisableClosingCraftingUIWhenTooFarAway_108 = new uScript_DisableClosingCraftingUIWhenTooFarAway();

	private TankBlock logic_uScript_DisableClosingCraftingUIWhenTooFarAway_craftingBlock_108;

	private bool logic_uScript_DisableClosingCraftingUIWhenTooFarAway_Out_108 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_110 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_110 = "";

	private bool logic_uScript_EnableGlow_enable_110;

	private bool logic_uScript_EnableGlow_Out_110 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_111 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_111;

	private bool logic_uScriptCon_CompareBool_True_111 = true;

	private bool logic_uScriptCon_CompareBool_False_111 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_112 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_112;

	private bool logic_uScriptCon_CompareBool_True_112 = true;

	private bool logic_uScriptCon_CompareBool_False_112 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_114 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_114;

	private bool logic_uScriptAct_SetBool_Out_114 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_114 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_114 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_120 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_120;

	private bool logic_uScriptCon_CompareBool_True_120 = true;

	private bool logic_uScriptCon_CompareBool_False_120 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_121 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_121 = true;

	private uScriptAct_Concatenate logic_uScriptAct_Concatenate_uScriptAct_Concatenate_125 = new uScriptAct_Concatenate();

	private object[] logic_uScriptAct_Concatenate_A_125 = new object[0];

	private object[] logic_uScriptAct_Concatenate_B_125 = new object[0];

	private string logic_uScriptAct_Concatenate_Separator_125 = "";

	private string logic_uScriptAct_Concatenate_Result_125;

	private bool logic_uScriptAct_Concatenate_Out_125 = true;

	private uScriptAct_PrintText logic_uScriptAct_PrintText_uScriptAct_PrintText_127 = new uScriptAct_PrintText();

	private string logic_uScriptAct_PrintText_Text_127 = "";

	private int logic_uScriptAct_PrintText_FontSize_127 = 16;

	private FontStyle logic_uScriptAct_PrintText_FontStyle_127;

	private Color logic_uScriptAct_PrintText_FontColor_127 = new Color(0f, 0f, 0f, 1f);

	private TextAnchor logic_uScriptAct_PrintText_textAnchor_127;

	private int logic_uScriptAct_PrintText_EdgePadding_127 = 8;

	private float logic_uScriptAct_PrintText_time_127;

	private bool logic_uScriptAct_PrintText_Out_127 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_131 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_131;

	private bool logic_uScriptCon_CompareBool_True_131 = true;

	private bool logic_uScriptCon_CompareBool_False_131 = true;

	private uScript_LockTechInteraction logic_uScript_LockTechInteraction_uScript_LockTechInteraction_132 = new uScript_LockTechInteraction();

	private Tank logic_uScript_LockTechInteraction_tech_132;

	private BlockTypes[] logic_uScript_LockTechInteraction_excludedBlocks_132 = new BlockTypes[0];

	private TankBlock[] logic_uScript_LockTechInteraction_excludedUniqueBlocks_132 = new TankBlock[0];

	private bool logic_uScript_LockTechInteraction_Out_132 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_135 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_135;

	private bool logic_uScriptAct_SetBool_Out_135 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_135 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_135 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_138 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_138;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_138 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_138 = "CanInteractWithFusionMachine";

	private uScriptCon_ManualSwitch logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_140 = new uScriptCon_ManualSwitch();

	private int logic_uScriptCon_ManualSwitch_CurrentOutput_140;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_144 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_144;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_144;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_144;

	private bool logic_uScript_AddMessage_Out_144 = true;

	private bool logic_uScript_AddMessage_Shown_144 = true;

	private uScriptAct_AddInt_v2 logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_146 = new uScriptAct_AddInt_v2();

	private int logic_uScriptAct_AddInt_v2_A_146;

	private int logic_uScriptAct_AddInt_v2_B_146 = 1;

	private int logic_uScriptAct_AddInt_v2_IntResult_146;

	private float logic_uScriptAct_AddInt_v2_FloatResult_146;

	private bool logic_uScriptAct_AddInt_v2_Out_146 = true;

	private uScriptCon_CompareInt logic_uScriptCon_CompareInt_uScriptCon_CompareInt_147 = new uScriptCon_CompareInt();

	private int logic_uScriptCon_CompareInt_A_147;

	private int logic_uScriptCon_CompareInt_B_147;

	private bool logic_uScriptCon_CompareInt_GreaterThan_147 = true;

	private bool logic_uScriptCon_CompareInt_GreaterThanOrEqualTo_147 = true;

	private bool logic_uScriptCon_CompareInt_EqualTo_147 = true;

	private bool logic_uScriptCon_CompareInt_NotEqualTo_147 = true;

	private bool logic_uScriptCon_CompareInt_LessThanOrEqualTo_147 = true;

	private bool logic_uScriptCon_CompareInt_LessThan_147 = true;

	private uScript_GetTankBlock logic_uScript_GetTankBlock_uScript_GetTankBlock_148 = new uScript_GetTankBlock();

	private Tank logic_uScript_GetTankBlock_tank_148;

	private BlockTypes logic_uScript_GetTankBlock_blockType_148;

	private TankBlock logic_uScript_GetTankBlock_Return_148;

	private bool logic_uScript_GetTankBlock_Out_148 = true;

	private bool logic_uScript_GetTankBlock_Returned_148 = true;

	private bool logic_uScript_GetTankBlock_NotFound_148 = true;

	private uScript_CompareBlockTypes logic_uScript_CompareBlockTypes_uScript_CompareBlockTypes_153 = new uScript_CompareBlockTypes();

	private BlockTypes logic_uScript_CompareBlockTypes_A_153;

	private BlockTypes logic_uScript_CompareBlockTypes_B_153;

	private bool logic_uScript_CompareBlockTypes_EqualTo_153 = true;

	private bool logic_uScript_CompareBlockTypes_NotEqualTo_153 = true;

	private uScriptAct_AddInt_v2 logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_157 = new uScriptAct_AddInt_v2();

	private int logic_uScriptAct_AddInt_v2_A_157;

	private int logic_uScriptAct_AddInt_v2_B_157 = 1;

	private int logic_uScriptAct_AddInt_v2_IntResult_157;

	private float logic_uScriptAct_AddInt_v2_FloatResult_157;

	private bool logic_uScriptAct_AddInt_v2_Out_157 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_158 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_158;

	private bool logic_uScriptAct_SetBool_Out_158 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_158 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_158 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_162 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_162 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_162;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_162;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_162 = true;

	private bool logic_uScript_SpawnTechsFromData_Out_162 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_163 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_163;

	private bool logic_uScriptAct_SetBool_Out_163 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_163 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_163 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_164 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_164 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_164;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_164 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_164;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_164 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_164 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_164 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_164 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_167 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_167;

	private bool logic_uScriptAct_SetBool_Out_167 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_167 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_167 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_168 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_168;

	private bool logic_uScriptCon_CompareBool_True_168 = true;

	private bool logic_uScriptCon_CompareBool_False_168 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_172 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_172;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_172;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_172;

	private bool logic_uScript_AddMessage_Out_172 = true;

	private bool logic_uScript_AddMessage_Shown_172 = true;

	private uScriptAct_AddInt_v2 logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_176 = new uScriptAct_AddInt_v2();

	private int logic_uScriptAct_AddInt_v2_A_176;

	private int logic_uScriptAct_AddInt_v2_B_176 = 1;

	private int logic_uScriptAct_AddInt_v2_IntResult_176;

	private float logic_uScriptAct_AddInt_v2_FloatResult_176;

	private bool logic_uScriptAct_AddInt_v2_Out_176 = true;

	private SubGraph_SaveLoadInt logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_178 = new SubGraph_SaveLoadInt();

	private int logic_SubGraph_SaveLoadInt_restartValue_178;

	private int logic_SubGraph_SaveLoadInt_integer_178;

	private object logic_SubGraph_SaveLoadInt_intAsVariable_178 = "";

	private string logic_SubGraph_SaveLoadInt_uniqueID_178 = "CurrentAmountType";

	private uScript_SetQuestObjectiveCount logic_uScript_SetQuestObjectiveCount_uScript_SetQuestObjectiveCount_181 = new uScript_SetQuestObjectiveCount();

	private GameObject logic_uScript_SetQuestObjectiveCount_owner_181;

	private int logic_uScript_SetQuestObjectiveCount_objectiveId_181;

	private int logic_uScript_SetQuestObjectiveCount_currentCount_181;

	private bool logic_uScript_SetQuestObjectiveCount_Out_181 = true;

	private uScript_SetQuestObjectiveCount logic_uScript_SetQuestObjectiveCount_uScript_SetQuestObjectiveCount_183 = new uScript_SetQuestObjectiveCount();

	private GameObject logic_uScript_SetQuestObjectiveCount_owner_183;

	private int logic_uScript_SetQuestObjectiveCount_objectiveId_183;

	private int logic_uScript_SetQuestObjectiveCount_currentCount_183;

	private bool logic_uScript_SetQuestObjectiveCount_Out_183 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_188 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_188 = true;

	private uScript_RemoveScenery logic_uScript_RemoveScenery_uScript_RemoveScenery_190 = new uScript_RemoveScenery();

	private GameObject logic_uScript_RemoveScenery_ownerNode_190;

	private string logic_uScript_RemoveScenery_positionName_190 = "";

	private float logic_uScript_RemoveScenery_radius_190;

	private bool logic_uScript_RemoveScenery_preventChunksSpawning_190 = true;

	private bool logic_uScript_RemoveScenery_Out_190 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_191 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_191 = new Tank[0];

	private int logic_uScript_AccessListTech_index_191;

	private Tank logic_uScript_AccessListTech_value_191;

	private bool logic_uScript_AccessListTech_Out_191 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_192 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_192 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_192;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_192 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_192;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_192 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_192 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_192 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_192 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_194 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_194;

	private bool logic_uScriptAct_SetBool_Out_194 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_194 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_194 = true;

	private uScript_SpawnVFX logic_uScript_SpawnVFX_uScript_SpawnVFX_197 = new uScript_SpawnVFX();

	private GameObject logic_uScript_SpawnVFX_ownerNode_197;

	private Transform logic_uScript_SpawnVFX_vfxToSpawn_197;

	private string logic_uScript_SpawnVFX_spawnPosName_197 = "";

	private bool logic_uScript_SpawnVFX_Out_197 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_198 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_198 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_198;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_198 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_198;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_198 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_198 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_198 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_198 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_200 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_200 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_200;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_200 = 0.3f;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_200;

	private bool logic_uScript_SpawnTechsFromData_Out_200 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_201 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_201 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_201;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_201 = 0.3f;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_201;

	private bool logic_uScript_SpawnTechsFromData_Out_201 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_207 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_207;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_207 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_207 = "Initialize";

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_209 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_209 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_210 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_210 = true;

	private uScript_RemoveTech logic_uScript_RemoveTech_uScript_RemoveTech_212 = new uScript_RemoveTech();

	private Tank logic_uScript_RemoveTech_tech_212;

	private bool logic_uScript_RemoveTech_Out_212 = true;

	private uScript_SetTankInvulnerable logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_213 = new uScript_SetTankInvulnerable();

	private bool logic_uScript_SetTankInvulnerable_invulnerable_213 = true;

	private Tank logic_uScript_SetTankInvulnerable_tank_213;

	private bool logic_uScript_SetTankInvulnerable_Out_213 = true;

	private uScript_SetCustomRadarTeamID logic_uScript_SetCustomRadarTeamID_uScript_SetCustomRadarTeamID_214 = new uScript_SetCustomRadarTeamID();

	private Tank logic_uScript_SetCustomRadarTeamID_tech_214;

	private int logic_uScript_SetCustomRadarTeamID_radarTeamID_214 = -2;

	private bool logic_uScript_SetCustomRadarTeamID_Out_214 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_216 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_216 = true;

	private uScript_LockTechInteraction logic_uScript_LockTechInteraction_uScript_LockTechInteraction_218 = new uScript_LockTechInteraction();

	private Tank logic_uScript_LockTechInteraction_tech_218;

	private BlockTypes[] logic_uScript_LockTechInteraction_excludedBlocks_218 = new BlockTypes[0];

	private TankBlock[] logic_uScript_LockTechInteraction_excludedUniqueBlocks_218 = new TankBlock[0];

	private bool logic_uScript_LockTechInteraction_Out_218 = true;

	private uScript_SetTankHideBlockLimit logic_uScript_SetTankHideBlockLimit_uScript_SetTankHideBlockLimit_224 = new uScript_SetTankHideBlockLimit();

	private bool logic_uScript_SetTankHideBlockLimit_hidden_224 = true;

	private Tank logic_uScript_SetTankHideBlockLimit_tech_224;

	private bool logic_uScript_SetTankHideBlockLimit_Out_224 = true;

	private uScript_LockTech logic_uScript_LockTech_uScript_LockTech_225 = new uScript_LockTech();

	private Tank logic_uScript_LockTech_tech_225;

	private uScript_LockTech.TechLockType logic_uScript_LockTech_lockType_225 = uScript_LockTech.TechLockType.LockAll;

	private bool logic_uScript_LockTech_Out_225 = true;

	private uScript_FlyTechUpAndAway logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_232 = new uScript_FlyTechUpAndAway();

	private Tank logic_uScript_FlyTechUpAndAway_tech_232;

	private float logic_uScript_FlyTechUpAndAway_maxLifetime_232 = 5f;

	private float logic_uScript_FlyTechUpAndAway_targetHeight_232 = 150f;

	private ExternalBehaviorTree logic_uScript_FlyTechUpAndAway_aiTree_232;

	private Transform logic_uScript_FlyTechUpAndAway_removalParticles_232;

	private bool logic_uScript_FlyTechUpAndAway_Out_232 = true;

	private uScript_LockTech logic_uScript_LockTech_uScript_LockTech_234 = new uScript_LockTech();

	private Tank logic_uScript_LockTech_tech_234;

	private uScript_LockTech.TechLockType logic_uScript_LockTech_lockType_234 = uScript_LockTech.TechLockType.LockAll;

	private bool logic_uScript_LockTech_Out_234 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_235 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_235 = new Tank[0];

	private int logic_uScript_AccessListTech_index_235;

	private Tank logic_uScript_AccessListTech_value_235;

	private bool logic_uScript_AccessListTech_Out_235 = true;

	private uScript_FinishEncounter logic_uScript_FinishEncounter_uScript_FinishEncounter_237 = new uScript_FinishEncounter();

	private GameObject logic_uScript_FinishEncounter_owner_237;

	private bool logic_uScript_FinishEncounter_Out_237 = true;

	private uScript_SetTankInvulnerable logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_239 = new uScript_SetTankInvulnerable();

	private bool logic_uScript_SetTankInvulnerable_invulnerable_239 = true;

	private Tank logic_uScript_SetTankInvulnerable_tank_239;

	private bool logic_uScript_SetTankInvulnerable_Out_239 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_240 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_240 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_241 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_241;

	private bool logic_uScriptCon_CompareBool_True_241 = true;

	private bool logic_uScriptCon_CompareBool_False_241 = true;

	private uScript_SetCustomRadarTeamID logic_uScript_SetCustomRadarTeamID_uScript_SetCustomRadarTeamID_242 = new uScript_SetCustomRadarTeamID();

	private Tank logic_uScript_SetCustomRadarTeamID_tech_242;

	private int logic_uScript_SetCustomRadarTeamID_radarTeamID_242 = -2;

	private bool logic_uScript_SetCustomRadarTeamID_Out_242 = true;

	private SubGraph_CompleteObjectiveStage logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_255 = new SubGraph_CompleteObjectiveStage();

	private int logic_SubGraph_CompleteObjectiveStage_objectiveStage_255;

	private bool logic_SubGraph_CompleteObjectiveStage_isFinalObjective_255;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_256 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_256;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_256;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_256;

	private bool logic_uScript_AddMessage_Out_256 = true;

	private bool logic_uScript_AddMessage_Shown_256 = true;

	private uScript_CraftingUIHighlightRepeatButton logic_uScript_CraftingUIHighlightRepeatButton_uScript_CraftingUIHighlightRepeatButton_264 = new uScript_CraftingUIHighlightRepeatButton();

	private bool logic_uScript_CraftingUIHighlightRepeatButton_requiredToggleState_264 = true;

	private ManHUD.HUDElementType logic_uScript_CraftingUIHighlightRepeatButton_targetMenuType_264 = ManHUD.HUDElementType.BlockRecipeSelect;

	private bool logic_uScript_CraftingUIHighlightRepeatButton_Out_264 = true;

	private bool logic_uScript_CraftingUIHighlightRepeatButton_Waiting_264 = true;

	private bool logic_uScript_CraftingUIHighlightRepeatButton_ToggledToRequiredState_264 = true;

	private uScript_CraftingUIHighlightCraftButton logic_uScript_CraftingUIHighlightCraftButton_uScript_CraftingUIHighlightCraftButton_265 = new uScript_CraftingUIHighlightCraftButton();

	private ManHUD.HUDElementType logic_uScript_CraftingUIHighlightCraftButton_targetMenuType_265 = ManHUD.HUDElementType.BlockRecipeSelect;

	private bool logic_uScript_CraftingUIHighlightCraftButton_Out_265 = true;

	private bool logic_uScript_CraftingUIHighlightCraftButton_Waiting_265 = true;

	private bool logic_uScript_CraftingUIHighlightCraftButton_Selected_265 = true;

	private uScript_LockTechInteraction logic_uScript_LockTechInteraction_uScript_LockTechInteraction_266 = new uScript_LockTechInteraction();

	private Tank logic_uScript_LockTechInteraction_tech_266;

	private BlockTypes[] logic_uScript_LockTechInteraction_excludedBlocks_266 = new BlockTypes[0];

	private TankBlock[] logic_uScript_LockTechInteraction_excludedUniqueBlocks_266 = new TankBlock[0];

	private bool logic_uScript_LockTechInteraction_Out_266 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_268 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_268 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_269 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_269 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_270 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_270 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_271 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_271 = true;

	private uScript_SetQuestObjectiveCount logic_uScript_SetQuestObjectiveCount_uScript_SetQuestObjectiveCount_274 = new uScript_SetQuestObjectiveCount();

	private GameObject logic_uScript_SetQuestObjectiveCount_owner_274;

	private int logic_uScript_SetQuestObjectiveCount_objectiveId_274;

	private int logic_uScript_SetQuestObjectiveCount_currentCount_274;

	private bool logic_uScript_SetQuestObjectiveCount_Out_274 = true;

	private uScript_CompareBlock logic_uScript_CompareBlock_uScript_CompareBlock_276 = new uScript_CompareBlock();

	private TankBlock logic_uScript_CompareBlock_A_276;

	private TankBlock logic_uScript_CompareBlock_B_276;

	private bool logic_uScript_CompareBlock_EqualTo_276 = true;

	private bool logic_uScript_CompareBlock_NotEqualTo_276 = true;

	private uScript_LockTechInteraction logic_uScript_LockTechInteraction_uScript_LockTechInteraction_279 = new uScript_LockTechInteraction();

	private Tank logic_uScript_LockTechInteraction_tech_279;

	private BlockTypes[] logic_uScript_LockTechInteraction_excludedBlocks_279 = new BlockTypes[0];

	private TankBlock[] logic_uScript_LockTechInteraction_excludedUniqueBlocks_279 = new TankBlock[0];

	private bool logic_uScript_LockTechInteraction_Out_279 = true;

	private uScript_LockTechInteraction logic_uScript_LockTechInteraction_uScript_LockTechInteraction_281 = new uScript_LockTechInteraction();

	private Tank logic_uScript_LockTechInteraction_tech_281;

	private BlockTypes[] logic_uScript_LockTechInteraction_excludedBlocks_281 = new BlockTypes[0];

	private TankBlock[] logic_uScript_LockTechInteraction_excludedUniqueBlocks_281 = new TankBlock[0];

	private bool logic_uScript_LockTechInteraction_Out_281 = true;

	private uScriptCon_CompareInt logic_uScriptCon_CompareInt_uScriptCon_CompareInt_283 = new uScriptCon_CompareInt();

	private int logic_uScriptCon_CompareInt_A_283;

	private int logic_uScriptCon_CompareInt_B_283 = 2;

	private bool logic_uScriptCon_CompareInt_GreaterThan_283 = true;

	private bool logic_uScriptCon_CompareInt_GreaterThanOrEqualTo_283 = true;

	private bool logic_uScriptCon_CompareInt_EqualTo_283 = true;

	private bool logic_uScriptCon_CompareInt_NotEqualTo_283 = true;

	private bool logic_uScriptCon_CompareInt_LessThanOrEqualTo_283 = true;

	private bool logic_uScriptCon_CompareInt_LessThan_283 = true;

	private uScript_SetBlock logic_uScript_SetBlock_uScript_SetBlock_285 = new uScript_SetBlock();

	private TankBlock logic_uScript_SetBlock_Value_285;

	private TankBlock logic_uScript_SetBlock_TargetGameObject_285;

	private bool logic_uScript_SetBlock_Out_285 = true;

	private SubGraph_RemoveBlockAfterDelay logic_SubGraph_RemoveBlockAfterDelay_SubGraph_RemoveBlockAfterDelay_289 = new SubGraph_RemoveBlockAfterDelay();

	private TankBlock logic_SubGraph_RemoveBlockAfterDelay_BlockToRemove_289;

	private float logic_SubGraph_RemoveBlockAfterDelay_TimeToWait_289;

	private Transform logic_SubGraph_RemoveBlockAfterDelay_DespawnParticlesTransform_289;

	private TankBlock logic_SubGraph_RemoveBlockAfterDelay_BlockResult_289;

	private uScript_CompareBlock logic_uScript_CompareBlock_uScript_CompareBlock_293 = new uScript_CompareBlock();

	private TankBlock logic_uScript_CompareBlock_A_293;

	private TankBlock logic_uScript_CompareBlock_B_293;

	private bool logic_uScript_CompareBlock_EqualTo_293 = true;

	private bool logic_uScript_CompareBlock_NotEqualTo_293 = true;

	private uScript_SetBlock logic_uScript_SetBlock_uScript_SetBlock_298 = new uScript_SetBlock();

	private TankBlock logic_uScript_SetBlock_Value_298;

	private TankBlock logic_uScript_SetBlock_TargetGameObject_298;

	private bool logic_uScript_SetBlock_Out_298 = true;

	private uScript_CompareBlock logic_uScript_CompareBlock_uScript_CompareBlock_299 = new uScript_CompareBlock();

	private TankBlock logic_uScript_CompareBlock_A_299;

	private TankBlock logic_uScript_CompareBlock_B_299;

	private bool logic_uScript_CompareBlock_EqualTo_299 = true;

	private bool logic_uScript_CompareBlock_NotEqualTo_299 = true;

	private uScript_SetBlock logic_uScript_SetBlock_uScript_SetBlock_301 = new uScript_SetBlock();

	private TankBlock logic_uScript_SetBlock_Value_301;

	private TankBlock logic_uScript_SetBlock_TargetGameObject_301;

	private bool logic_uScript_SetBlock_Out_301 = true;

	private uScript_CompareBlock logic_uScript_CompareBlock_uScript_CompareBlock_303 = new uScript_CompareBlock();

	private TankBlock logic_uScript_CompareBlock_A_303;

	private TankBlock logic_uScript_CompareBlock_B_303;

	private bool logic_uScript_CompareBlock_EqualTo_303 = true;

	private bool logic_uScript_CompareBlock_NotEqualTo_303 = true;

	private uScript_SetBlock logic_uScript_SetBlock_uScript_SetBlock_306 = new uScript_SetBlock();

	private TankBlock logic_uScript_SetBlock_Value_306;

	private TankBlock logic_uScript_SetBlock_TargetGameObject_306;

	private bool logic_uScript_SetBlock_Out_306 = true;

	private uScript_CompareBlock logic_uScript_CompareBlock_uScript_CompareBlock_308 = new uScript_CompareBlock();

	private TankBlock logic_uScript_CompareBlock_A_308;

	private TankBlock logic_uScript_CompareBlock_B_308;

	private bool logic_uScript_CompareBlock_EqualTo_308 = true;

	private bool logic_uScript_CompareBlock_NotEqualTo_308 = true;

	private SubGraph_RemoveBlockAfterDelay logic_SubGraph_RemoveBlockAfterDelay_SubGraph_RemoveBlockAfterDelay_314 = new SubGraph_RemoveBlockAfterDelay();

	private TankBlock logic_SubGraph_RemoveBlockAfterDelay_BlockToRemove_314;

	private float logic_SubGraph_RemoveBlockAfterDelay_TimeToWait_314;

	private Transform logic_SubGraph_RemoveBlockAfterDelay_DespawnParticlesTransform_314;

	private TankBlock logic_SubGraph_RemoveBlockAfterDelay_BlockResult_314;

	private SubGraph_RemoveBlockAfterDelay logic_SubGraph_RemoveBlockAfterDelay_SubGraph_RemoveBlockAfterDelay_318 = new SubGraph_RemoveBlockAfterDelay();

	private TankBlock logic_SubGraph_RemoveBlockAfterDelay_BlockToRemove_318;

	private float logic_SubGraph_RemoveBlockAfterDelay_TimeToWait_318;

	private Transform logic_SubGraph_RemoveBlockAfterDelay_DespawnParticlesTransform_318;

	private TankBlock logic_SubGraph_RemoveBlockAfterDelay_BlockResult_318;

	private uScriptAct_PrintList logic_uScriptAct_PrintList_uScriptAct_PrintList_319 = new uScriptAct_PrintList();

	private string[] logic_uScriptAct_PrintList_Strings_319 = new string[1] { "<color=\"#FF0000\">ERROR: We've hit the end of our cached recycled blocks cache for the mission! Need to extend it / review implementation!</color>" };

	private bool logic_uScriptAct_PrintList_Out_319 = true;

	private SubGraph_RemoveBlockAfterDelay logic_SubGraph_RemoveBlockAfterDelay_SubGraph_RemoveBlockAfterDelay_320 = new SubGraph_RemoveBlockAfterDelay();

	private TankBlock logic_SubGraph_RemoveBlockAfterDelay_BlockToRemove_320;

	private float logic_SubGraph_RemoveBlockAfterDelay_TimeToWait_320;

	private Transform logic_SubGraph_RemoveBlockAfterDelay_DespawnParticlesTransform_320;

	private TankBlock logic_SubGraph_RemoveBlockAfterDelay_BlockResult_320;

	private uScriptCon_CompareInt logic_uScriptCon_CompareInt_uScriptCon_CompareInt_326 = new uScriptCon_CompareInt();

	private int logic_uScriptCon_CompareInt_A_326;

	private int logic_uScriptCon_CompareInt_B_326 = 2;

	private bool logic_uScriptCon_CompareInt_GreaterThan_326 = true;

	private bool logic_uScriptCon_CompareInt_GreaterThanOrEqualTo_326 = true;

	private bool logic_uScriptCon_CompareInt_EqualTo_326 = true;

	private bool logic_uScriptCon_CompareInt_NotEqualTo_326 = true;

	private bool logic_uScriptCon_CompareInt_LessThanOrEqualTo_326 = true;

	private bool logic_uScriptCon_CompareInt_LessThan_326 = true;

	private uScript_DirectEnemiesOutOfEncounter logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_328 = new uScript_DirectEnemiesOutOfEncounter();

	private GameObject logic_uScript_DirectEnemiesOutOfEncounter_ownerNode_328;

	private bool logic_uScript_DirectEnemiesOutOfEncounter_Out_328 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_330 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_330;

	private bool logic_uScriptCon_CompareBool_True_330 = true;

	private bool logic_uScriptCon_CompareBool_False_330 = true;

	private SubGraph_SaveLoadInt logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_699 = new SubGraph_SaveLoadInt();

	private int logic_SubGraph_SaveLoadInt_restartValue_699 = 1;

	private int logic_SubGraph_SaveLoadInt_integer_699;

	private object logic_SubGraph_SaveLoadInt_intAsVariable_699 = "";

	private string logic_SubGraph_SaveLoadInt_uniqueID_699 = "Stage3";

	private BlockTypes event_UnityEngine_GameObject_BlockType_154;

	private int event_UnityEngine_GameObject_BlockTypeTotal_154;

	private int event_UnityEngine_GameObject_BlockTotal_154;

	private TankBlock event_UnityEngine_GameObject_Block_154;

	private TankBlock event_UnityEngine_GameObject_CrafterBlock_154;

	private void SyncUnityHooks()
	{
		SyncEventListeners();
		if (null == owner_Connection_8 || !m_RegisteredForEvents)
		{
			owner_Connection_8 = parentGameObject;
			if (null != owner_Connection_8)
			{
				uScript_SaveLoad uScript_SaveLoad2 = owner_Connection_8.GetComponent<uScript_SaveLoad>();
				if (null == uScript_SaveLoad2)
				{
					uScript_SaveLoad2 = owner_Connection_8.AddComponent<uScript_SaveLoad>();
				}
				if (null != uScript_SaveLoad2)
				{
					uScript_SaveLoad2.SaveEvent += Instance_SaveEvent_16;
					uScript_SaveLoad2.LoadEvent += Instance_LoadEvent_16;
					uScript_SaveLoad2.RestartEvent += Instance_RestartEvent_16;
				}
			}
		}
		if (null == owner_Connection_14 || !m_RegisteredForEvents)
		{
			owner_Connection_14 = parentGameObject;
		}
		if (null == owner_Connection_34 || !m_RegisteredForEvents)
		{
			owner_Connection_34 = parentGameObject;
			if (null != owner_Connection_34)
			{
				uScript_EncounterUpdate uScript_EncounterUpdate2 = owner_Connection_34.GetComponent<uScript_EncounterUpdate>();
				if (null == uScript_EncounterUpdate2)
				{
					uScript_EncounterUpdate2 = owner_Connection_34.AddComponent<uScript_EncounterUpdate>();
				}
				if (null != uScript_EncounterUpdate2)
				{
					uScript_EncounterUpdate2.OnUpdate += Instance_OnUpdate_41;
					uScript_EncounterUpdate2.OnSuspend += Instance_OnSuspend_41;
					uScript_EncounterUpdate2.OnResume += Instance_OnResume_41;
				}
			}
		}
		if (null == owner_Connection_155 || !m_RegisteredForEvents)
		{
			owner_Connection_155 = parentGameObject;
			if (null != owner_Connection_155)
			{
				uScript_BlockCraftedEvent uScript_BlockCraftedEvent2 = owner_Connection_155.GetComponent<uScript_BlockCraftedEvent>();
				if (null == uScript_BlockCraftedEvent2)
				{
					uScript_BlockCraftedEvent2 = owner_Connection_155.AddComponent<uScript_BlockCraftedEvent>();
				}
				if (null != uScript_BlockCraftedEvent2)
				{
					uScript_BlockCraftedEvent2.BlockCraftedEvent += Instance_BlockCraftedEvent_154;
				}
			}
		}
		if (null == owner_Connection_165 || !m_RegisteredForEvents)
		{
			owner_Connection_165 = parentGameObject;
		}
		if (null == owner_Connection_169 || !m_RegisteredForEvents)
		{
			owner_Connection_169 = parentGameObject;
		}
		if (null == owner_Connection_182 || !m_RegisteredForEvents)
		{
			owner_Connection_182 = parentGameObject;
		}
		if (null == owner_Connection_184 || !m_RegisteredForEvents)
		{
			owner_Connection_184 = parentGameObject;
		}
		if (null == owner_Connection_199 || !m_RegisteredForEvents)
		{
			owner_Connection_199 = parentGameObject;
		}
		if (null == owner_Connection_204 || !m_RegisteredForEvents)
		{
			owner_Connection_204 = parentGameObject;
		}
		if (null == owner_Connection_206 || !m_RegisteredForEvents)
		{
			owner_Connection_206 = parentGameObject;
		}
		if (null == owner_Connection_208 || !m_RegisteredForEvents)
		{
			owner_Connection_208 = parentGameObject;
		}
		if (null == owner_Connection_217 || !m_RegisteredForEvents)
		{
			owner_Connection_217 = parentGameObject;
		}
		if (null == owner_Connection_227 || !m_RegisteredForEvents)
		{
			owner_Connection_227 = parentGameObject;
		}
		if (null == owner_Connection_236 || !m_RegisteredForEvents)
		{
			owner_Connection_236 = parentGameObject;
		}
		if (null == owner_Connection_272 || !m_RegisteredForEvents)
		{
			owner_Connection_272 = parentGameObject;
		}
		if (null == owner_Connection_329 || !m_RegisteredForEvents)
		{
			owner_Connection_329 = parentGameObject;
		}
	}

	private void RegisterForUnityHooks()
	{
		SyncEventListeners();
		if (!m_RegisteredForEvents && null != owner_Connection_8)
		{
			uScript_SaveLoad uScript_SaveLoad2 = owner_Connection_8.GetComponent<uScript_SaveLoad>();
			if (null == uScript_SaveLoad2)
			{
				uScript_SaveLoad2 = owner_Connection_8.AddComponent<uScript_SaveLoad>();
			}
			if (null != uScript_SaveLoad2)
			{
				uScript_SaveLoad2.SaveEvent += Instance_SaveEvent_16;
				uScript_SaveLoad2.LoadEvent += Instance_LoadEvent_16;
				uScript_SaveLoad2.RestartEvent += Instance_RestartEvent_16;
			}
		}
		if (!m_RegisteredForEvents && null != owner_Connection_34)
		{
			uScript_EncounterUpdate uScript_EncounterUpdate2 = owner_Connection_34.GetComponent<uScript_EncounterUpdate>();
			if (null == uScript_EncounterUpdate2)
			{
				uScript_EncounterUpdate2 = owner_Connection_34.AddComponent<uScript_EncounterUpdate>();
			}
			if (null != uScript_EncounterUpdate2)
			{
				uScript_EncounterUpdate2.OnUpdate += Instance_OnUpdate_41;
				uScript_EncounterUpdate2.OnSuspend += Instance_OnSuspend_41;
				uScript_EncounterUpdate2.OnResume += Instance_OnResume_41;
			}
		}
		if (!m_RegisteredForEvents && null != owner_Connection_155)
		{
			uScript_BlockCraftedEvent uScript_BlockCraftedEvent2 = owner_Connection_155.GetComponent<uScript_BlockCraftedEvent>();
			if (null == uScript_BlockCraftedEvent2)
			{
				uScript_BlockCraftedEvent2 = owner_Connection_155.AddComponent<uScript_BlockCraftedEvent>();
			}
			if (null != uScript_BlockCraftedEvent2)
			{
				uScript_BlockCraftedEvent2.BlockCraftedEvent += Instance_BlockCraftedEvent_154;
			}
		}
	}

	private void SyncEventListeners()
	{
	}

	private void UnregisterEventListeners()
	{
		if (null != owner_Connection_8)
		{
			uScript_SaveLoad component = owner_Connection_8.GetComponent<uScript_SaveLoad>();
			if (null != component)
			{
				component.SaveEvent -= Instance_SaveEvent_16;
				component.LoadEvent -= Instance_LoadEvent_16;
				component.RestartEvent -= Instance_RestartEvent_16;
			}
		}
		if (null != owner_Connection_34)
		{
			uScript_EncounterUpdate component2 = owner_Connection_34.GetComponent<uScript_EncounterUpdate>();
			if (null != component2)
			{
				component2.OnUpdate -= Instance_OnUpdate_41;
				component2.OnSuspend -= Instance_OnSuspend_41;
				component2.OnResume -= Instance_OnResume_41;
			}
		}
		if (null != owner_Connection_155)
		{
			uScript_BlockCraftedEvent component3 = owner_Connection_155.GetComponent<uScript_BlockCraftedEvent>();
			if (null != component3)
			{
				component3.BlockCraftedEvent -= Instance_BlockCraftedEvent_154;
			}
		}
	}

	public override void SetParent(GameObject g)
	{
		parentGameObject = g;
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_0.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_2.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_4.SetParent(g);
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_7.SetParent(g);
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_11.SetParent(g);
		logic_uScript_HideArrow_uScript_HideArrow_12.SetParent(g);
		logic_uScript_PausePopulation_uScript_PausePopulation_13.SetParent(g);
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_15.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_20.SetParent(g);
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_22.SetParent(g);
		logic_uScript_PausePopulation_uScript_PausePopulation_24.SetParent(g);
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_25.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_26.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_27.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_31.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_33.SetParent(g);
		logic_uScript_LockTechInteraction_uScript_LockTechInteraction_35.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_36.SetParent(g);
		logic_uScript_LockTechStacks_uScript_LockTechStacks_37.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_38.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_42.SetParent(g);
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_45.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_46.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_47.SetParent(g);
		logic_uScript_IsHUDElementLinkedToBlock_uScript_IsHUDElementLinkedToBlock_48.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_49.SetParent(g);
		logic_uScript_DisableClosingCraftingUIWhenTooFarAway_uScript_DisableClosingCraftingUIWhenTooFarAway_52.SetParent(g);
		logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_53.SetParent(g);
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_56.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_58.SetParent(g);
		logic_uScript_HideArrow_uScript_HideArrow_59.SetParent(g);
		logic_uScript_HideHUDElement_uScript_HideHUDElement_61.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_68.SetParent(g);
		logic_uScript_LockPlayerInput_uScript_LockPlayerInput_69.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_71.SetParent(g);
		logic_uScript_LockPlayerInput_uScript_LockPlayerInput_72.SetParent(g);
		logic_uScript_CraftingUIHighlightItem_uScript_CraftingUIHighlightItem_74.SetParent(g);
		logic_uScript_LockPause_uScript_LockPause_78.SetParent(g);
		logic_uScript_LockHudGroup_uScript_LockHudGroup_79.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_80.SetParent(g);
		logic_uScript_LockPause_uScript_LockPause_82.SetParent(g);
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_83.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_84.SetParent(g);
		logic_uScript_LockTechInteraction_uScript_LockTechInteraction_86.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_87.SetParent(g);
		logic_uScript_LockHudGroup_uScript_LockHudGroup_89.SetParent(g);
		logic_uScript_DisableClosingCraftingUIWhenTooFarAway_uScript_DisableClosingCraftingUIWhenTooFarAway_91.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_96.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_97.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_98.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_103.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_104.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_105.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_106.SetParent(g);
		logic_uScript_DisableClosingCraftingUIWhenTooFarAway_uScript_DisableClosingCraftingUIWhenTooFarAway_108.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_110.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_111.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_112.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_114.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_120.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_121.SetParent(g);
		logic_uScriptAct_Concatenate_uScriptAct_Concatenate_125.SetParent(g);
		logic_uScriptAct_PrintText_uScriptAct_PrintText_127.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_131.SetParent(g);
		logic_uScript_LockTechInteraction_uScript_LockTechInteraction_132.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_135.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_138.SetParent(g);
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_140.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_144.SetParent(g);
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_146.SetParent(g);
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_147.SetParent(g);
		logic_uScript_GetTankBlock_uScript_GetTankBlock_148.SetParent(g);
		logic_uScript_CompareBlockTypes_uScript_CompareBlockTypes_153.SetParent(g);
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_157.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_158.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_162.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_163.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_164.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_167.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_168.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_172.SetParent(g);
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_176.SetParent(g);
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_178.SetParent(g);
		logic_uScript_SetQuestObjectiveCount_uScript_SetQuestObjectiveCount_181.SetParent(g);
		logic_uScript_SetQuestObjectiveCount_uScript_SetQuestObjectiveCount_183.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_188.SetParent(g);
		logic_uScript_RemoveScenery_uScript_RemoveScenery_190.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_191.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_192.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_194.SetParent(g);
		logic_uScript_SpawnVFX_uScript_SpawnVFX_197.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_198.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_200.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_201.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_207.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_209.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_210.SetParent(g);
		logic_uScript_RemoveTech_uScript_RemoveTech_212.SetParent(g);
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_213.SetParent(g);
		logic_uScript_SetCustomRadarTeamID_uScript_SetCustomRadarTeamID_214.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_216.SetParent(g);
		logic_uScript_LockTechInteraction_uScript_LockTechInteraction_218.SetParent(g);
		logic_uScript_SetTankHideBlockLimit_uScript_SetTankHideBlockLimit_224.SetParent(g);
		logic_uScript_LockTech_uScript_LockTech_225.SetParent(g);
		logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_232.SetParent(g);
		logic_uScript_LockTech_uScript_LockTech_234.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_235.SetParent(g);
		logic_uScript_FinishEncounter_uScript_FinishEncounter_237.SetParent(g);
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_239.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_240.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_241.SetParent(g);
		logic_uScript_SetCustomRadarTeamID_uScript_SetCustomRadarTeamID_242.SetParent(g);
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_255.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_256.SetParent(g);
		logic_uScript_CraftingUIHighlightRepeatButton_uScript_CraftingUIHighlightRepeatButton_264.SetParent(g);
		logic_uScript_CraftingUIHighlightCraftButton_uScript_CraftingUIHighlightCraftButton_265.SetParent(g);
		logic_uScript_LockTechInteraction_uScript_LockTechInteraction_266.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_268.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_269.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_270.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_271.SetParent(g);
		logic_uScript_SetQuestObjectiveCount_uScript_SetQuestObjectiveCount_274.SetParent(g);
		logic_uScript_CompareBlock_uScript_CompareBlock_276.SetParent(g);
		logic_uScript_LockTechInteraction_uScript_LockTechInteraction_279.SetParent(g);
		logic_uScript_LockTechInteraction_uScript_LockTechInteraction_281.SetParent(g);
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_283.SetParent(g);
		logic_uScript_SetBlock_uScript_SetBlock_285.SetParent(g);
		logic_SubGraph_RemoveBlockAfterDelay_SubGraph_RemoveBlockAfterDelay_289.SetParent(g);
		logic_uScript_CompareBlock_uScript_CompareBlock_293.SetParent(g);
		logic_uScript_SetBlock_uScript_SetBlock_298.SetParent(g);
		logic_uScript_CompareBlock_uScript_CompareBlock_299.SetParent(g);
		logic_uScript_SetBlock_uScript_SetBlock_301.SetParent(g);
		logic_uScript_CompareBlock_uScript_CompareBlock_303.SetParent(g);
		logic_uScript_SetBlock_uScript_SetBlock_306.SetParent(g);
		logic_uScript_CompareBlock_uScript_CompareBlock_308.SetParent(g);
		logic_SubGraph_RemoveBlockAfterDelay_SubGraph_RemoveBlockAfterDelay_314.SetParent(g);
		logic_SubGraph_RemoveBlockAfterDelay_SubGraph_RemoveBlockAfterDelay_318.SetParent(g);
		logic_uScriptAct_PrintList_uScriptAct_PrintList_319.SetParent(g);
		logic_SubGraph_RemoveBlockAfterDelay_SubGraph_RemoveBlockAfterDelay_320.SetParent(g);
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_326.SetParent(g);
		logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_328.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_330.SetParent(g);
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_699.SetParent(g);
		owner_Connection_8 = parentGameObject;
		owner_Connection_14 = parentGameObject;
		owner_Connection_34 = parentGameObject;
		owner_Connection_155 = parentGameObject;
		owner_Connection_165 = parentGameObject;
		owner_Connection_169 = parentGameObject;
		owner_Connection_182 = parentGameObject;
		owner_Connection_184 = parentGameObject;
		owner_Connection_199 = parentGameObject;
		owner_Connection_204 = parentGameObject;
		owner_Connection_206 = parentGameObject;
		owner_Connection_208 = parentGameObject;
		owner_Connection_217 = parentGameObject;
		owner_Connection_227 = parentGameObject;
		owner_Connection_236 = parentGameObject;
		owner_Connection_272 = parentGameObject;
		owner_Connection_329 = parentGameObject;
	}

	public void Awake()
	{
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_4.Awake();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_7.Awake();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_11.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_20.Awake();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_45.Awake();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_56.Awake();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_83.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_96.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_97.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_98.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_138.Awake();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_178.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_207.Awake();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_255.Awake();
		logic_SubGraph_RemoveBlockAfterDelay_SubGraph_RemoveBlockAfterDelay_289.Awake();
		logic_SubGraph_RemoveBlockAfterDelay_SubGraph_RemoveBlockAfterDelay_314.Awake();
		logic_SubGraph_RemoveBlockAfterDelay_SubGraph_RemoveBlockAfterDelay_318.Awake();
		logic_SubGraph_RemoveBlockAfterDelay_SubGraph_RemoveBlockAfterDelay_320.Awake();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_699.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_4.Save_Out += SubGraph_SaveLoadBool_Save_Out_4;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_4.Load_Out += SubGraph_SaveLoadBool_Load_Out_4;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_4.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_4;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_7.Out += SubGraph_CompleteObjectiveStage_Out_7;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_11.Save_Out += SubGraph_SaveLoadInt_Save_Out_11;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_11.Load_Out += SubGraph_SaveLoadInt_Load_Out_11;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_11.Restart_Out += SubGraph_SaveLoadInt_Restart_Out_11;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_15.Output1 += uScriptCon_ManualSwitch_Output1_15;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_15.Output2 += uScriptCon_ManualSwitch_Output2_15;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_15.Output3 += uScriptCon_ManualSwitch_Output3_15;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_15.Output4 += uScriptCon_ManualSwitch_Output4_15;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_15.Output5 += uScriptCon_ManualSwitch_Output5_15;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_15.Output6 += uScriptCon_ManualSwitch_Output6_15;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_15.Output7 += uScriptCon_ManualSwitch_Output7_15;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_15.Output8 += uScriptCon_ManualSwitch_Output8_15;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_20.Save_Out += SubGraph_SaveLoadBool_Save_Out_20;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_20.Load_Out += SubGraph_SaveLoadBool_Load_Out_20;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_20.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_20;
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_45.Out += SubGraph_LoadObjectiveStates_Out_45;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_56.Out += SubGraph_AddMessageWithPadSupport_Out_56;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_56.Shown += SubGraph_AddMessageWithPadSupport_Shown_56;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_83.Out += SubGraph_AddMessageWithPadSupport_Out_83;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_83.Shown += SubGraph_AddMessageWithPadSupport_Shown_83;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_96.Save_Out += SubGraph_SaveLoadBool_Save_Out_96;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_96.Load_Out += SubGraph_SaveLoadBool_Load_Out_96;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_96.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_96;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_97.Save_Out += SubGraph_SaveLoadBool_Save_Out_97;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_97.Load_Out += SubGraph_SaveLoadBool_Load_Out_97;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_97.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_97;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_98.Save_Out += SubGraph_SaveLoadBool_Save_Out_98;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_98.Load_Out += SubGraph_SaveLoadBool_Load_Out_98;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_98.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_98;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_138.Save_Out += SubGraph_SaveLoadBool_Save_Out_138;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_138.Load_Out += SubGraph_SaveLoadBool_Load_Out_138;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_138.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_138;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_140.Output1 += uScriptCon_ManualSwitch_Output1_140;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_140.Output2 += uScriptCon_ManualSwitch_Output2_140;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_140.Output3 += uScriptCon_ManualSwitch_Output3_140;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_140.Output4 += uScriptCon_ManualSwitch_Output4_140;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_140.Output5 += uScriptCon_ManualSwitch_Output5_140;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_140.Output6 += uScriptCon_ManualSwitch_Output6_140;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_140.Output7 += uScriptCon_ManualSwitch_Output7_140;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_140.Output8 += uScriptCon_ManualSwitch_Output8_140;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_178.Save_Out += SubGraph_SaveLoadInt_Save_Out_178;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_178.Load_Out += SubGraph_SaveLoadInt_Load_Out_178;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_178.Restart_Out += SubGraph_SaveLoadInt_Restart_Out_178;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_207.Save_Out += SubGraph_SaveLoadBool_Save_Out_207;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_207.Load_Out += SubGraph_SaveLoadBool_Load_Out_207;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_207.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_207;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_255.Out += SubGraph_CompleteObjectiveStage_Out_255;
		logic_SubGraph_RemoveBlockAfterDelay_SubGraph_RemoveBlockAfterDelay_289.OutNoBlock += SubGraph_RemoveBlockAfterDelay_OutNoBlock_289;
		logic_SubGraph_RemoveBlockAfterDelay_SubGraph_RemoveBlockAfterDelay_289.Out += SubGraph_RemoveBlockAfterDelay_Out_289;
		logic_SubGraph_RemoveBlockAfterDelay_SubGraph_RemoveBlockAfterDelay_314.OutNoBlock += SubGraph_RemoveBlockAfterDelay_OutNoBlock_314;
		logic_SubGraph_RemoveBlockAfterDelay_SubGraph_RemoveBlockAfterDelay_314.Out += SubGraph_RemoveBlockAfterDelay_Out_314;
		logic_SubGraph_RemoveBlockAfterDelay_SubGraph_RemoveBlockAfterDelay_318.OutNoBlock += SubGraph_RemoveBlockAfterDelay_OutNoBlock_318;
		logic_SubGraph_RemoveBlockAfterDelay_SubGraph_RemoveBlockAfterDelay_318.Out += SubGraph_RemoveBlockAfterDelay_Out_318;
		logic_SubGraph_RemoveBlockAfterDelay_SubGraph_RemoveBlockAfterDelay_320.OutNoBlock += SubGraph_RemoveBlockAfterDelay_OutNoBlock_320;
		logic_SubGraph_RemoveBlockAfterDelay_SubGraph_RemoveBlockAfterDelay_320.Out += SubGraph_RemoveBlockAfterDelay_Out_320;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_699.Save_Out += SubGraph_SaveLoadInt_Save_Out_699;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_699.Load_Out += SubGraph_SaveLoadInt_Load_Out_699;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_699.Restart_Out += SubGraph_SaveLoadInt_Restart_Out_699;
	}

	public void Start()
	{
		SyncUnityHooks();
		m_RegisteredForEvents = true;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_4.Start();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_7.Start();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_11.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_20.Start();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_45.Start();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_56.Start();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_83.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_96.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_97.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_98.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_138.Start();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_178.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_207.Start();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_255.Start();
		logic_SubGraph_RemoveBlockAfterDelay_SubGraph_RemoveBlockAfterDelay_289.Start();
		logic_SubGraph_RemoveBlockAfterDelay_SubGraph_RemoveBlockAfterDelay_314.Start();
		logic_SubGraph_RemoveBlockAfterDelay_SubGraph_RemoveBlockAfterDelay_318.Start();
		logic_SubGraph_RemoveBlockAfterDelay_SubGraph_RemoveBlockAfterDelay_320.Start();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_699.Start();
	}

	public void OnEnable()
	{
		RegisterForUnityHooks();
		m_RegisteredForEvents = true;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_4.OnEnable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_7.OnEnable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_11.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_20.OnEnable();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_45.OnEnable();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_56.OnEnable();
		logic_uScript_CraftingUIHighlightItem_uScript_CraftingUIHighlightItem_74.OnEnable();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_83.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_96.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_97.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_98.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_138.OnEnable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_178.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_207.OnEnable();
		logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_232.OnEnable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_255.OnEnable();
		logic_uScript_CraftingUIHighlightRepeatButton_uScript_CraftingUIHighlightRepeatButton_264.OnEnable();
		logic_uScript_CraftingUIHighlightCraftButton_uScript_CraftingUIHighlightCraftButton_265.OnEnable();
		logic_SubGraph_RemoveBlockAfterDelay_SubGraph_RemoveBlockAfterDelay_289.OnEnable();
		logic_SubGraph_RemoveBlockAfterDelay_SubGraph_RemoveBlockAfterDelay_314.OnEnable();
		logic_SubGraph_RemoveBlockAfterDelay_SubGraph_RemoveBlockAfterDelay_318.OnEnable();
		logic_SubGraph_RemoveBlockAfterDelay_SubGraph_RemoveBlockAfterDelay_320.OnEnable();
		logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_328.OnEnable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_699.OnEnable();
	}

	public void OnDisable()
	{
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_4.OnDisable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_7.OnDisable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_11.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_20.OnDisable();
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_22.OnDisable();
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_25.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_31.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_42.OnDisable();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_45.OnDisable();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_56.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_71.OnDisable();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_83.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_96.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_97.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_98.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_105.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_138.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_144.OnDisable();
		logic_uScript_GetTankBlock_uScript_GetTankBlock_148.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_172.OnDisable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_178.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_207.OnDisable();
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_213.OnDisable();
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_239.OnDisable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_255.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_256.OnDisable();
		logic_uScript_CraftingUIHighlightRepeatButton_uScript_CraftingUIHighlightRepeatButton_264.OnDisable();
		logic_uScript_CraftingUIHighlightCraftButton_uScript_CraftingUIHighlightCraftButton_265.OnDisable();
		logic_SubGraph_RemoveBlockAfterDelay_SubGraph_RemoveBlockAfterDelay_289.OnDisable();
		logic_SubGraph_RemoveBlockAfterDelay_SubGraph_RemoveBlockAfterDelay_314.OnDisable();
		logic_SubGraph_RemoveBlockAfterDelay_SubGraph_RemoveBlockAfterDelay_318.OnDisable();
		logic_SubGraph_RemoveBlockAfterDelay_SubGraph_RemoveBlockAfterDelay_320.OnDisable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_699.OnDisable();
		UnregisterEventListeners();
		m_RegisteredForEvents = false;
	}

	public void Update()
	{
		SyncEventListeners();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_4.Update();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_7.Update();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_11.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_20.Update();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_45.Update();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_56.Update();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_83.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_96.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_97.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_98.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_138.Update();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_178.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_207.Update();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_255.Update();
		logic_SubGraph_RemoveBlockAfterDelay_SubGraph_RemoveBlockAfterDelay_289.Update();
		logic_SubGraph_RemoveBlockAfterDelay_SubGraph_RemoveBlockAfterDelay_314.Update();
		logic_SubGraph_RemoveBlockAfterDelay_SubGraph_RemoveBlockAfterDelay_318.Update();
		logic_SubGraph_RemoveBlockAfterDelay_SubGraph_RemoveBlockAfterDelay_320.Update();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_699.Update();
	}

	public void OnDestroy()
	{
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_4.OnDestroy();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_7.OnDestroy();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_11.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_20.OnDestroy();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_45.OnDestroy();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_56.OnDestroy();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_83.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_96.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_97.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_98.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_138.OnDestroy();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_178.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_207.OnDestroy();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_255.OnDestroy();
		logic_SubGraph_RemoveBlockAfterDelay_SubGraph_RemoveBlockAfterDelay_289.OnDestroy();
		logic_SubGraph_RemoveBlockAfterDelay_SubGraph_RemoveBlockAfterDelay_314.OnDestroy();
		logic_SubGraph_RemoveBlockAfterDelay_SubGraph_RemoveBlockAfterDelay_318.OnDestroy();
		logic_SubGraph_RemoveBlockAfterDelay_SubGraph_RemoveBlockAfterDelay_320.OnDestroy();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_699.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_4.Save_Out -= SubGraph_SaveLoadBool_Save_Out_4;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_4.Load_Out -= SubGraph_SaveLoadBool_Load_Out_4;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_4.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_4;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_7.Out -= SubGraph_CompleteObjectiveStage_Out_7;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_11.Save_Out -= SubGraph_SaveLoadInt_Save_Out_11;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_11.Load_Out -= SubGraph_SaveLoadInt_Load_Out_11;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_11.Restart_Out -= SubGraph_SaveLoadInt_Restart_Out_11;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_15.Output1 -= uScriptCon_ManualSwitch_Output1_15;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_15.Output2 -= uScriptCon_ManualSwitch_Output2_15;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_15.Output3 -= uScriptCon_ManualSwitch_Output3_15;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_15.Output4 -= uScriptCon_ManualSwitch_Output4_15;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_15.Output5 -= uScriptCon_ManualSwitch_Output5_15;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_15.Output6 -= uScriptCon_ManualSwitch_Output6_15;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_15.Output7 -= uScriptCon_ManualSwitch_Output7_15;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_15.Output8 -= uScriptCon_ManualSwitch_Output8_15;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_20.Save_Out -= SubGraph_SaveLoadBool_Save_Out_20;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_20.Load_Out -= SubGraph_SaveLoadBool_Load_Out_20;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_20.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_20;
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_45.Out -= SubGraph_LoadObjectiveStates_Out_45;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_56.Out -= SubGraph_AddMessageWithPadSupport_Out_56;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_56.Shown -= SubGraph_AddMessageWithPadSupport_Shown_56;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_83.Out -= SubGraph_AddMessageWithPadSupport_Out_83;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_83.Shown -= SubGraph_AddMessageWithPadSupport_Shown_83;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_96.Save_Out -= SubGraph_SaveLoadBool_Save_Out_96;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_96.Load_Out -= SubGraph_SaveLoadBool_Load_Out_96;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_96.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_96;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_97.Save_Out -= SubGraph_SaveLoadBool_Save_Out_97;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_97.Load_Out -= SubGraph_SaveLoadBool_Load_Out_97;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_97.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_97;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_98.Save_Out -= SubGraph_SaveLoadBool_Save_Out_98;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_98.Load_Out -= SubGraph_SaveLoadBool_Load_Out_98;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_98.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_98;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_138.Save_Out -= SubGraph_SaveLoadBool_Save_Out_138;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_138.Load_Out -= SubGraph_SaveLoadBool_Load_Out_138;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_138.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_138;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_140.Output1 -= uScriptCon_ManualSwitch_Output1_140;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_140.Output2 -= uScriptCon_ManualSwitch_Output2_140;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_140.Output3 -= uScriptCon_ManualSwitch_Output3_140;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_140.Output4 -= uScriptCon_ManualSwitch_Output4_140;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_140.Output5 -= uScriptCon_ManualSwitch_Output5_140;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_140.Output6 -= uScriptCon_ManualSwitch_Output6_140;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_140.Output7 -= uScriptCon_ManualSwitch_Output7_140;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_140.Output8 -= uScriptCon_ManualSwitch_Output8_140;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_178.Save_Out -= SubGraph_SaveLoadInt_Save_Out_178;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_178.Load_Out -= SubGraph_SaveLoadInt_Load_Out_178;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_178.Restart_Out -= SubGraph_SaveLoadInt_Restart_Out_178;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_207.Save_Out -= SubGraph_SaveLoadBool_Save_Out_207;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_207.Load_Out -= SubGraph_SaveLoadBool_Load_Out_207;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_207.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_207;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_255.Out -= SubGraph_CompleteObjectiveStage_Out_255;
		logic_SubGraph_RemoveBlockAfterDelay_SubGraph_RemoveBlockAfterDelay_289.OutNoBlock -= SubGraph_RemoveBlockAfterDelay_OutNoBlock_289;
		logic_SubGraph_RemoveBlockAfterDelay_SubGraph_RemoveBlockAfterDelay_289.Out -= SubGraph_RemoveBlockAfterDelay_Out_289;
		logic_SubGraph_RemoveBlockAfterDelay_SubGraph_RemoveBlockAfterDelay_314.OutNoBlock -= SubGraph_RemoveBlockAfterDelay_OutNoBlock_314;
		logic_SubGraph_RemoveBlockAfterDelay_SubGraph_RemoveBlockAfterDelay_314.Out -= SubGraph_RemoveBlockAfterDelay_Out_314;
		logic_SubGraph_RemoveBlockAfterDelay_SubGraph_RemoveBlockAfterDelay_318.OutNoBlock -= SubGraph_RemoveBlockAfterDelay_OutNoBlock_318;
		logic_SubGraph_RemoveBlockAfterDelay_SubGraph_RemoveBlockAfterDelay_318.Out -= SubGraph_RemoveBlockAfterDelay_Out_318;
		logic_SubGraph_RemoveBlockAfterDelay_SubGraph_RemoveBlockAfterDelay_320.OutNoBlock -= SubGraph_RemoveBlockAfterDelay_OutNoBlock_320;
		logic_SubGraph_RemoveBlockAfterDelay_SubGraph_RemoveBlockAfterDelay_320.Out -= SubGraph_RemoveBlockAfterDelay_Out_320;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_699.Save_Out -= SubGraph_SaveLoadInt_Save_Out_699;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_699.Load_Out -= SubGraph_SaveLoadInt_Load_Out_699;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_699.Restart_Out -= SubGraph_SaveLoadInt_Restart_Out_699;
	}

	public void OnGUI()
	{
		logic_uScriptAct_PrintText_uScriptAct_PrintText_127.OnGUI();
	}

	private void Instance_SaveEvent_16(object o, EventArgs e)
	{
		Relay_SaveEvent_16();
	}

	private void Instance_LoadEvent_16(object o, EventArgs e)
	{
		Relay_LoadEvent_16();
	}

	private void Instance_RestartEvent_16(object o, EventArgs e)
	{
		Relay_RestartEvent_16();
	}

	private void Instance_OnUpdate_41(object o, EventArgs e)
	{
		Relay_OnUpdate_41();
	}

	private void Instance_OnSuspend_41(object o, EventArgs e)
	{
		Relay_OnSuspend_41();
	}

	private void Instance_OnResume_41(object o, EventArgs e)
	{
		Relay_OnResume_41();
	}

	private void Instance_BlockCraftedEvent_154(object o, uScript_BlockCraftedEvent.BlockCraftedEventArgs e)
	{
		event_UnityEngine_GameObject_BlockType_154 = e.BlockType;
		event_UnityEngine_GameObject_BlockTypeTotal_154 = e.BlockTypeTotal;
		event_UnityEngine_GameObject_BlockTotal_154 = e.BlockTotal;
		event_UnityEngine_GameObject_Block_154 = e.Block;
		event_UnityEngine_GameObject_CrafterBlock_154 = e.CrafterBlock;
		Relay_BlockCraftedEvent_154();
	}

	private void SubGraph_SaveLoadBool_Save_Out_4(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_4 = e.boolean;
		local_msgIntroShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_4;
		Relay_Save_Out_4();
	}

	private void SubGraph_SaveLoadBool_Load_Out_4(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_4 = e.boolean;
		local_msgIntroShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_4;
		Relay_Load_Out_4();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_4(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_4 = e.boolean;
		local_msgIntroShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_4;
		Relay_Restart_Out_4();
	}

	private void SubGraph_CompleteObjectiveStage_Out_7(object o, SubGraph_CompleteObjectiveStage.LogicEventArgs e)
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_7 = e.objectiveStage;
		local_Stage_System_Int32 = logic_SubGraph_CompleteObjectiveStage_objectiveStage_7;
		Relay_Out_7();
	}

	private void SubGraph_SaveLoadInt_Save_Out_11(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_11 = e.integer;
		local_Stage_System_Int32 = logic_SubGraph_SaveLoadInt_integer_11;
		Relay_Save_Out_11();
	}

	private void SubGraph_SaveLoadInt_Load_Out_11(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_11 = e.integer;
		local_Stage_System_Int32 = logic_SubGraph_SaveLoadInt_integer_11;
		Relay_Load_Out_11();
	}

	private void SubGraph_SaveLoadInt_Restart_Out_11(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_11 = e.integer;
		local_Stage_System_Int32 = logic_SubGraph_SaveLoadInt_integer_11;
		Relay_Restart_Out_11();
	}

	private void uScriptCon_ManualSwitch_Output1_15(object o, EventArgs e)
	{
		Relay_Output1_15();
	}

	private void uScriptCon_ManualSwitch_Output2_15(object o, EventArgs e)
	{
		Relay_Output2_15();
	}

	private void uScriptCon_ManualSwitch_Output3_15(object o, EventArgs e)
	{
		Relay_Output3_15();
	}

	private void uScriptCon_ManualSwitch_Output4_15(object o, EventArgs e)
	{
		Relay_Output4_15();
	}

	private void uScriptCon_ManualSwitch_Output5_15(object o, EventArgs e)
	{
		Relay_Output5_15();
	}

	private void uScriptCon_ManualSwitch_Output6_15(object o, EventArgs e)
	{
		Relay_Output6_15();
	}

	private void uScriptCon_ManualSwitch_Output7_15(object o, EventArgs e)
	{
		Relay_Output7_15();
	}

	private void uScriptCon_ManualSwitch_Output8_15(object o, EventArgs e)
	{
		Relay_Output8_15();
	}

	private void SubGraph_SaveLoadBool_Save_Out_20(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_20 = e.boolean;
		local_msgBaseFoundShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_20;
		Relay_Save_Out_20();
	}

	private void SubGraph_SaveLoadBool_Load_Out_20(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_20 = e.boolean;
		local_msgBaseFoundShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_20;
		Relay_Load_Out_20();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_20(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_20 = e.boolean;
		local_msgBaseFoundShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_20;
		Relay_Restart_Out_20();
	}

	private void SubGraph_LoadObjectiveStates_Out_45(object o, SubGraph_LoadObjectiveStates.LogicEventArgs e)
	{
		Relay_Out_45();
	}

	private void SubGraph_AddMessageWithPadSupport_Out_56(object o, SubGraph_AddMessageWithPadSupport.LogicEventArgs e)
	{
		logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboardReturn_56 = e.messageMouseKeyboardReturn;
		logic_SubGraph_AddMessageWithPadSupport_messageControlPadReturn_56 = e.messageControlPadReturn;
		Relay_Out_56();
	}

	private void SubGraph_AddMessageWithPadSupport_Shown_56(object o, SubGraph_AddMessageWithPadSupport.LogicEventArgs e)
	{
		logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboardReturn_56 = e.messageMouseKeyboardReturn;
		logic_SubGraph_AddMessageWithPadSupport_messageControlPadReturn_56 = e.messageControlPadReturn;
		Relay_Shown_56();
	}

	private void SubGraph_AddMessageWithPadSupport_Out_83(object o, SubGraph_AddMessageWithPadSupport.LogicEventArgs e)
	{
		logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboardReturn_83 = e.messageMouseKeyboardReturn;
		logic_SubGraph_AddMessageWithPadSupport_messageControlPadReturn_83 = e.messageControlPadReturn;
		Relay_Out_83();
	}

	private void SubGraph_AddMessageWithPadSupport_Shown_83(object o, SubGraph_AddMessageWithPadSupport.LogicEventArgs e)
	{
		logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboardReturn_83 = e.messageMouseKeyboardReturn;
		logic_SubGraph_AddMessageWithPadSupport_messageControlPadReturn_83 = e.messageControlPadReturn;
		Relay_Shown_83();
	}

	private void SubGraph_SaveLoadBool_Save_Out_96(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_96 = e.boolean;
		local_CraftingMenuOpened_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_96;
		Relay_Save_Out_96();
	}

	private void SubGraph_SaveLoadBool_Load_Out_96(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_96 = e.boolean;
		local_CraftingMenuOpened_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_96;
		Relay_Load_Out_96();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_96(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_96 = e.boolean;
		local_CraftingMenuOpened_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_96;
		Relay_Restart_Out_96();
	}

	private void SubGraph_SaveLoadBool_Save_Out_97(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_97 = e.boolean;
		local_AllBlocksCrafted_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_97;
		Relay_Save_Out_97();
	}

	private void SubGraph_SaveLoadBool_Load_Out_97(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_97 = e.boolean;
		local_AllBlocksCrafted_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_97;
		Relay_Load_Out_97();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_97(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_97 = e.boolean;
		local_AllBlocksCrafted_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_97;
		Relay_Restart_Out_97();
	}

	private void SubGraph_SaveLoadBool_Save_Out_98(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_98 = e.boolean;
		local_CraftingInProgress_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_98;
		Relay_Save_Out_98();
	}

	private void SubGraph_SaveLoadBool_Load_Out_98(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_98 = e.boolean;
		local_CraftingInProgress_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_98;
		Relay_Load_Out_98();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_98(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_98 = e.boolean;
		local_CraftingInProgress_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_98;
		Relay_Restart_Out_98();
	}

	private void SubGraph_SaveLoadBool_Save_Out_138(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_138 = e.boolean;
		local_CanInteractWithFusionMachine_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_138;
		Relay_Save_Out_138();
	}

	private void SubGraph_SaveLoadBool_Load_Out_138(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_138 = e.boolean;
		local_CanInteractWithFusionMachine_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_138;
		Relay_Load_Out_138();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_138(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_138 = e.boolean;
		local_CanInteractWithFusionMachine_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_138;
		Relay_Restart_Out_138();
	}

	private void uScriptCon_ManualSwitch_Output1_140(object o, EventArgs e)
	{
		Relay_Output1_140();
	}

	private void uScriptCon_ManualSwitch_Output2_140(object o, EventArgs e)
	{
		Relay_Output2_140();
	}

	private void uScriptCon_ManualSwitch_Output3_140(object o, EventArgs e)
	{
		Relay_Output3_140();
	}

	private void uScriptCon_ManualSwitch_Output4_140(object o, EventArgs e)
	{
		Relay_Output4_140();
	}

	private void uScriptCon_ManualSwitch_Output5_140(object o, EventArgs e)
	{
		Relay_Output5_140();
	}

	private void uScriptCon_ManualSwitch_Output6_140(object o, EventArgs e)
	{
		Relay_Output6_140();
	}

	private void uScriptCon_ManualSwitch_Output7_140(object o, EventArgs e)
	{
		Relay_Output7_140();
	}

	private void uScriptCon_ManualSwitch_Output8_140(object o, EventArgs e)
	{
		Relay_Output8_140();
	}

	private void SubGraph_SaveLoadInt_Save_Out_178(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_178 = e.integer;
		local_CurrentAmountType_System_Int32 = logic_SubGraph_SaveLoadInt_integer_178;
		Relay_Save_Out_178();
	}

	private void SubGraph_SaveLoadInt_Load_Out_178(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_178 = e.integer;
		local_CurrentAmountType_System_Int32 = logic_SubGraph_SaveLoadInt_integer_178;
		Relay_Load_Out_178();
	}

	private void SubGraph_SaveLoadInt_Restart_Out_178(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_178 = e.integer;
		local_CurrentAmountType_System_Int32 = logic_SubGraph_SaveLoadInt_integer_178;
		Relay_Restart_Out_178();
	}

	private void SubGraph_SaveLoadBool_Save_Out_207(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_207 = e.boolean;
		local_Initialize_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_207;
		Relay_Save_Out_207();
	}

	private void SubGraph_SaveLoadBool_Load_Out_207(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_207 = e.boolean;
		local_Initialize_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_207;
		Relay_Load_Out_207();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_207(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_207 = e.boolean;
		local_Initialize_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_207;
		Relay_Restart_Out_207();
	}

	private void SubGraph_CompleteObjectiveStage_Out_255(object o, SubGraph_CompleteObjectiveStage.LogicEventArgs e)
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_255 = e.objectiveStage;
		local_Stage_System_Int32 = logic_SubGraph_CompleteObjectiveStage_objectiveStage_255;
		Relay_Out_255();
	}

	private void SubGraph_RemoveBlockAfterDelay_OutNoBlock_289(object o, SubGraph_RemoveBlockAfterDelay.LogicEventArgs e)
	{
		logic_SubGraph_RemoveBlockAfterDelay_BlockResult_289 = e.BlockResult;
		local_craftedCacheBlock1_TankBlock = logic_SubGraph_RemoveBlockAfterDelay_BlockResult_289;
		Relay_OutNoBlock_289();
	}

	private void SubGraph_RemoveBlockAfterDelay_Out_289(object o, SubGraph_RemoveBlockAfterDelay.LogicEventArgs e)
	{
		logic_SubGraph_RemoveBlockAfterDelay_BlockResult_289 = e.BlockResult;
		local_craftedCacheBlock1_TankBlock = logic_SubGraph_RemoveBlockAfterDelay_BlockResult_289;
		Relay_Out_289();
	}

	private void SubGraph_RemoveBlockAfterDelay_OutNoBlock_314(object o, SubGraph_RemoveBlockAfterDelay.LogicEventArgs e)
	{
		logic_SubGraph_RemoveBlockAfterDelay_BlockResult_314 = e.BlockResult;
		local_craftedCacheBlock2_TankBlock = logic_SubGraph_RemoveBlockAfterDelay_BlockResult_314;
		Relay_OutNoBlock_314();
	}

	private void SubGraph_RemoveBlockAfterDelay_Out_314(object o, SubGraph_RemoveBlockAfterDelay.LogicEventArgs e)
	{
		logic_SubGraph_RemoveBlockAfterDelay_BlockResult_314 = e.BlockResult;
		local_craftedCacheBlock2_TankBlock = logic_SubGraph_RemoveBlockAfterDelay_BlockResult_314;
		Relay_Out_314();
	}

	private void SubGraph_RemoveBlockAfterDelay_OutNoBlock_318(object o, SubGraph_RemoveBlockAfterDelay.LogicEventArgs e)
	{
		logic_SubGraph_RemoveBlockAfterDelay_BlockResult_318 = e.BlockResult;
		local_craftedCacheBlock3_TankBlock = logic_SubGraph_RemoveBlockAfterDelay_BlockResult_318;
		Relay_OutNoBlock_318();
	}

	private void SubGraph_RemoveBlockAfterDelay_Out_318(object o, SubGraph_RemoveBlockAfterDelay.LogicEventArgs e)
	{
		logic_SubGraph_RemoveBlockAfterDelay_BlockResult_318 = e.BlockResult;
		local_craftedCacheBlock3_TankBlock = logic_SubGraph_RemoveBlockAfterDelay_BlockResult_318;
		Relay_Out_318();
	}

	private void SubGraph_RemoveBlockAfterDelay_OutNoBlock_320(object o, SubGraph_RemoveBlockAfterDelay.LogicEventArgs e)
	{
		logic_SubGraph_RemoveBlockAfterDelay_BlockResult_320 = e.BlockResult;
		local_craftedCacheBlock4_TankBlock = logic_SubGraph_RemoveBlockAfterDelay_BlockResult_320;
		Relay_OutNoBlock_320();
	}

	private void SubGraph_RemoveBlockAfterDelay_Out_320(object o, SubGraph_RemoveBlockAfterDelay.LogicEventArgs e)
	{
		logic_SubGraph_RemoveBlockAfterDelay_BlockResult_320 = e.BlockResult;
		local_craftedCacheBlock4_TankBlock = logic_SubGraph_RemoveBlockAfterDelay_BlockResult_320;
		Relay_Out_320();
	}

	private void SubGraph_SaveLoadInt_Save_Out_699(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_699 = e.integer;
		local_Stage3_System_Int32 = logic_SubGraph_SaveLoadInt_integer_699;
		Relay_Save_Out_699();
	}

	private void SubGraph_SaveLoadInt_Load_Out_699(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_699 = e.integer;
		local_Stage3_System_Int32 = logic_SubGraph_SaveLoadInt_integer_699;
		Relay_Load_Out_699();
	}

	private void SubGraph_SaveLoadInt_Restart_Out_699(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_699 = e.integer;
		local_Stage3_System_Int32 = logic_SubGraph_SaveLoadInt_integer_699;
		Relay_Restart_Out_699();
	}

	private void Relay_In_0()
	{
		logic_uScript_SetEncounterTarget_owner_0 = owner_Connection_14;
		logic_uScript_SetEncounterTarget_visibleObject_0 = local_NPCTech_Tank;
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_0.In(logic_uScript_SetEncounterTarget_owner_0, logic_uScript_SetEncounterTarget_visibleObject_0);
		if (logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_0.Out)
		{
			Relay_In_22();
		}
	}

	private void Relay_In_2()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_2 = messageTag;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_2.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_2, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_2);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_2.Out)
		{
			Relay_In_12();
		}
	}

	private void Relay_Save_Out_4()
	{
		Relay_Save_138();
	}

	private void Relay_Load_Out_4()
	{
		Relay_Load_138();
	}

	private void Relay_Restart_Out_4()
	{
		Relay_Set_False_138();
	}

	private void Relay_Save_4()
	{
		logic_SubGraph_SaveLoadBool_boolean_4 = local_msgIntroShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_4 = local_msgIntroShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_4.Save(ref logic_SubGraph_SaveLoadBool_boolean_4, logic_SubGraph_SaveLoadBool_boolAsVariable_4, logic_SubGraph_SaveLoadBool_uniqueID_4);
	}

	private void Relay_Load_4()
	{
		logic_SubGraph_SaveLoadBool_boolean_4 = local_msgIntroShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_4 = local_msgIntroShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_4.Load(ref logic_SubGraph_SaveLoadBool_boolean_4, logic_SubGraph_SaveLoadBool_boolAsVariable_4, logic_SubGraph_SaveLoadBool_uniqueID_4);
	}

	private void Relay_Set_True_4()
	{
		logic_SubGraph_SaveLoadBool_boolean_4 = local_msgIntroShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_4 = local_msgIntroShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_4.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_4, logic_SubGraph_SaveLoadBool_boolAsVariable_4, logic_SubGraph_SaveLoadBool_uniqueID_4);
	}

	private void Relay_Set_False_4()
	{
		logic_SubGraph_SaveLoadBool_boolean_4 = local_msgIntroShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_4 = local_msgIntroShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_4.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_4, logic_SubGraph_SaveLoadBool_boolAsVariable_4, logic_SubGraph_SaveLoadBool_uniqueID_4);
	}

	private void Relay_Out_7()
	{
	}

	private void Relay_In_7()
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_7 = local_Stage_System_Int32;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_7.In(ref logic_SubGraph_CompleteObjectiveStage_objectiveStage_7, logic_SubGraph_CompleteObjectiveStage_isFinalObjective_7);
	}

	private void Relay_Save_Out_11()
	{
		Relay_Save_699();
	}

	private void Relay_Load_Out_11()
	{
		Relay_Load_699();
	}

	private void Relay_Restart_Out_11()
	{
		Relay_Restart_699();
	}

	private void Relay_Save_11()
	{
		logic_SubGraph_SaveLoadInt_integer_11 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_11 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_11.Save(logic_SubGraph_SaveLoadInt_restartValue_11, ref logic_SubGraph_SaveLoadInt_integer_11, logic_SubGraph_SaveLoadInt_intAsVariable_11, logic_SubGraph_SaveLoadInt_uniqueID_11);
	}

	private void Relay_Load_11()
	{
		logic_SubGraph_SaveLoadInt_integer_11 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_11 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_11.Load(logic_SubGraph_SaveLoadInt_restartValue_11, ref logic_SubGraph_SaveLoadInt_integer_11, logic_SubGraph_SaveLoadInt_intAsVariable_11, logic_SubGraph_SaveLoadInt_uniqueID_11);
	}

	private void Relay_Restart_11()
	{
		logic_SubGraph_SaveLoadInt_integer_11 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_11 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_11.Restart(logic_SubGraph_SaveLoadInt_restartValue_11, ref logic_SubGraph_SaveLoadInt_integer_11, logic_SubGraph_SaveLoadInt_intAsVariable_11, logic_SubGraph_SaveLoadInt_uniqueID_11);
	}

	private void Relay_In_12()
	{
		logic_uScript_HideArrow_uScript_HideArrow_12.In();
		if (logic_uScript_HideArrow_uScript_HideArrow_12.Out)
		{
			Relay_In_120();
		}
	}

	private void Relay_Pause_13()
	{
		logic_uScript_PausePopulation_uScript_PausePopulation_13.Pause();
		if (logic_uScript_PausePopulation_uScript_PausePopulation_13.Out)
		{
			Relay_In_2();
		}
	}

	private void Relay_UnPause_13()
	{
		logic_uScript_PausePopulation_uScript_PausePopulation_13.UnPause();
		if (logic_uScript_PausePopulation_uScript_PausePopulation_13.Out)
		{
			Relay_In_2();
		}
	}

	private void Relay_Output1_15()
	{
		Relay_In_46();
	}

	private void Relay_Output2_15()
	{
		Relay_True_135();
	}

	private void Relay_Output3_15()
	{
		Relay_In_140();
	}

	private void Relay_Output4_15()
	{
	}

	private void Relay_Output5_15()
	{
	}

	private void Relay_Output6_15()
	{
	}

	private void Relay_Output7_15()
	{
	}

	private void Relay_Output8_15()
	{
	}

	private void Relay_In_15()
	{
		logic_uScriptCon_ManualSwitch_CurrentOutput_15 = local_Stage_System_Int32;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_15.In(logic_uScriptCon_ManualSwitch_CurrentOutput_15);
	}

	private void Relay_SaveEvent_16()
	{
		Relay_Save_11();
	}

	private void Relay_LoadEvent_16()
	{
		Relay_Load_11();
	}

	private void Relay_RestartEvent_16()
	{
		Relay_Restart_11();
	}

	private void Relay_Save_Out_20()
	{
		Relay_Save_4();
	}

	private void Relay_Load_Out_20()
	{
		Relay_Load_4();
	}

	private void Relay_Restart_Out_20()
	{
		Relay_Set_False_4();
	}

	private void Relay_Save_20()
	{
		logic_SubGraph_SaveLoadBool_boolean_20 = local_msgBaseFoundShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_20 = local_msgBaseFoundShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_20.Save(ref logic_SubGraph_SaveLoadBool_boolean_20, logic_SubGraph_SaveLoadBool_boolAsVariable_20, logic_SubGraph_SaveLoadBool_uniqueID_20);
	}

	private void Relay_Load_20()
	{
		logic_SubGraph_SaveLoadBool_boolean_20 = local_msgBaseFoundShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_20 = local_msgBaseFoundShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_20.Load(ref logic_SubGraph_SaveLoadBool_boolean_20, logic_SubGraph_SaveLoadBool_boolAsVariable_20, logic_SubGraph_SaveLoadBool_uniqueID_20);
	}

	private void Relay_Set_True_20()
	{
		logic_SubGraph_SaveLoadBool_boolean_20 = local_msgBaseFoundShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_20 = local_msgBaseFoundShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_20.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_20, logic_SubGraph_SaveLoadBool_boolAsVariable_20, logic_SubGraph_SaveLoadBool_uniqueID_20);
	}

	private void Relay_Set_False_20()
	{
		logic_SubGraph_SaveLoadBool_boolean_20 = local_msgBaseFoundShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_20 = local_msgBaseFoundShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_20.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_20, logic_SubGraph_SaveLoadBool_boolAsVariable_20, logic_SubGraph_SaveLoadBool_uniqueID_20);
	}

	private void Relay_In_22()
	{
		logic_uScript_IsPlayerInRangeOfTech_tech_22 = local_CraftingBaseTech_Tank;
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_22.In(logic_uScript_IsPlayerInRangeOfTech_tech_22, logic_uScript_IsPlayerInRangeOfTech_range_22, logic_uScript_IsPlayerInRangeOfTech_techs_22);
		if (logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_22.InRange)
		{
			Relay_In_27();
		}
	}

	private void Relay_Pause_24()
	{
		logic_uScript_PausePopulation_uScript_PausePopulation_24.Pause();
		if (logic_uScript_PausePopulation_uScript_PausePopulation_24.Out)
		{
			Relay_True_26();
		}
	}

	private void Relay_UnPause_24()
	{
		logic_uScript_PausePopulation_uScript_PausePopulation_24.UnPause();
		if (logic_uScript_PausePopulation_uScript_PausePopulation_24.Out)
		{
			Relay_True_26();
		}
	}

	private void Relay_In_25()
	{
		logic_uScript_IsPlayerInRangeOfTech_tech_25 = local_CraftingBaseTech_Tank;
		logic_uScript_IsPlayerInRangeOfTech_range_25 = distBaseFound;
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_25.In(logic_uScript_IsPlayerInRangeOfTech_tech_25, logic_uScript_IsPlayerInRangeOfTech_range_25, logic_uScript_IsPlayerInRangeOfTech_techs_25);
		bool inRange = logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_25.InRange;
		bool outOfRange = logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_25.OutOfRange;
		if (inRange)
		{
			Relay_Pause_24();
		}
		if (outOfRange)
		{
			Relay_UnPause_13();
		}
	}

	private void Relay_True_26()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_26.True(out logic_uScriptAct_SetBool_Target_26);
		local_NearBase_System_Boolean = logic_uScriptAct_SetBool_Target_26;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_26.Out)
		{
			Relay_In_15();
		}
	}

	private void Relay_False_26()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_26.False(out logic_uScriptAct_SetBool_Target_26);
		local_NearBase_System_Boolean = logic_uScriptAct_SetBool_Target_26;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_26.Out)
		{
			Relay_In_15();
		}
	}

	private void Relay_In_27()
	{
		logic_uScriptCon_CompareBool_Bool_27 = local_msgIntroShown_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_27.In(logic_uScriptCon_CompareBool_Bool_27);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_27.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_27.False;
		if (num)
		{
			Relay_In_25();
		}
		if (flag)
		{
			Relay_True_36();
		}
	}

	private void Relay_In_31()
	{
		logic_uScript_AddMessage_messageData_31 = msg02BaseFound;
		logic_uScript_AddMessage_speaker_31 = messageSpeaker;
		logic_uScript_AddMessage_Return_31 = logic_uScript_AddMessage_uScript_AddMessage_31.In(logic_uScript_AddMessage_messageData_31, logic_uScript_AddMessage_speaker_31);
		if (logic_uScript_AddMessage_uScript_AddMessage_31.Shown)
		{
			Relay_True_38();
		}
	}

	private void Relay_True_33()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_33.True(out logic_uScriptAct_SetBool_Target_33);
		local_NearBase_System_Boolean = logic_uScriptAct_SetBool_Target_33;
	}

	private void Relay_False_33()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_33.False(out logic_uScriptAct_SetBool_Target_33);
		local_NearBase_System_Boolean = logic_uScriptAct_SetBool_Target_33;
	}

	private void Relay_In_35()
	{
		logic_uScript_LockTechInteraction_tech_35 = local_CraftingBaseTech_Tank;
		logic_uScript_LockTechInteraction_uScript_LockTechInteraction_35.In(logic_uScript_LockTechInteraction_tech_35, logic_uScript_LockTechInteraction_excludedBlocks_35, logic_uScript_LockTechInteraction_excludedUniqueBlocks_35);
		if (logic_uScript_LockTechInteraction_uScript_LockTechInteraction_35.Out)
		{
			Relay_In_37();
		}
	}

	private void Relay_True_36()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_36.True(out logic_uScriptAct_SetBool_Target_36);
		local_msgIntroShown_System_Boolean = logic_uScriptAct_SetBool_Target_36;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_36.Out)
		{
			Relay_In_42();
		}
	}

	private void Relay_False_36()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_36.False(out logic_uScriptAct_SetBool_Target_36);
		local_msgIntroShown_System_Boolean = logic_uScriptAct_SetBool_Target_36;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_36.Out)
		{
			Relay_In_42();
		}
	}

	private void Relay_In_37()
	{
		logic_uScript_LockTechStacks_tech_37 = local_CraftingBaseTech_Tank;
		logic_uScript_LockTechStacks_uScript_LockTechStacks_37.In(logic_uScript_LockTechStacks_tech_37);
		if (logic_uScript_LockTechStacks_uScript_LockTechStacks_37.Out)
		{
			Relay_In_328();
		}
	}

	private void Relay_True_38()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_38.True(out logic_uScriptAct_SetBool_Target_38);
		local_msgBaseFoundShown_System_Boolean = logic_uScriptAct_SetBool_Target_38;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_38.Out)
		{
			Relay_In_61();
		}
	}

	private void Relay_False_38()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_38.False(out logic_uScriptAct_SetBool_Target_38);
		local_msgBaseFoundShown_System_Boolean = logic_uScriptAct_SetBool_Target_38;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_38.Out)
		{
			Relay_In_61();
		}
	}

	private void Relay_OnUpdate_41()
	{
		Relay_In_241();
	}

	private void Relay_OnSuspend_41()
	{
	}

	private void Relay_OnResume_41()
	{
	}

	private void Relay_In_42()
	{
		logic_uScript_AddMessage_messageData_42 = msg01Intro;
		logic_uScript_AddMessage_speaker_42 = messageSpeaker;
		logic_uScript_AddMessage_Return_42 = logic_uScript_AddMessage_uScript_AddMessage_42.In(logic_uScript_AddMessage_messageData_42, logic_uScript_AddMessage_speaker_42);
		if (logic_uScript_AddMessage_uScript_AddMessage_42.Out)
		{
			Relay_In_25();
		}
	}

	private void Relay_Out_45()
	{
		Relay_In_283();
	}

	private void Relay_In_45()
	{
		logic_SubGraph_LoadObjectiveStates_currentObjective_45 = local_Stage_System_Int32;
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_45.In(logic_SubGraph_LoadObjectiveStates_currentObjective_45);
	}

	private void Relay_In_46()
	{
		logic_uScriptCon_CompareBool_Bool_46 = local_msgBaseFoundShown_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_46.In(logic_uScriptCon_CompareBool_Bool_46);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_46.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_46.False;
		if (num)
		{
			Relay_In_61();
		}
		if (flag)
		{
			Relay_In_31();
		}
	}

	private void Relay_In_47()
	{
		logic_uScript_EnableGlow_targetObject_47 = local_FusionMachineBlock_TankBlock;
		logic_uScript_EnableGlow_uScript_EnableGlow_47.In(logic_uScript_EnableGlow_targetObject_47, logic_uScript_EnableGlow_enable_47);
		if (logic_uScript_EnableGlow_uScript_EnableGlow_47.Out)
		{
			Relay_DisableAutoCloseUI_52();
		}
	}

	private void Relay_In_48()
	{
		logic_uScript_IsHUDElementLinkedToBlock_hudElement_48 = local_CraftingMenu_ManHUD_HUDElementType;
		logic_uScript_IsHUDElementLinkedToBlock_targetBlock_48 = local_FusionMachineBlock_TankBlock;
		logic_uScript_IsHUDElementLinkedToBlock_uScript_IsHUDElementLinkedToBlock_48.In(logic_uScript_IsHUDElementLinkedToBlock_hudElement_48, logic_uScript_IsHUDElementLinkedToBlock_targetBlock_48);
		if (logic_uScript_IsHUDElementLinkedToBlock_uScript_IsHUDElementLinkedToBlock_48.True)
		{
			Relay_In_59();
		}
	}

	private void Relay_In_49()
	{
		logic_uScript_EnableGlow_targetObject_49 = local_FusionMachineBlock_TankBlock;
		logic_uScript_EnableGlow_uScript_EnableGlow_49.In(logic_uScript_EnableGlow_targetObject_49, logic_uScript_EnableGlow_enable_49);
		if (logic_uScript_EnableGlow_uScript_EnableGlow_49.Out)
		{
			Relay_True_58();
		}
	}

	private void Relay_DisableAutoCloseUI_52()
	{
		logic_uScript_DisableClosingCraftingUIWhenTooFarAway_craftingBlock_52 = local_FusionMachineBlock_TankBlock;
		logic_uScript_DisableClosingCraftingUIWhenTooFarAway_uScript_DisableClosingCraftingUIWhenTooFarAway_52.DisableAutoCloseUI(logic_uScript_DisableClosingCraftingUIWhenTooFarAway_craftingBlock_52);
		if (logic_uScript_DisableClosingCraftingUIWhenTooFarAway_uScript_DisableClosingCraftingUIWhenTooFarAway_52.Out)
		{
			Relay_In_48();
		}
	}

	private void Relay_EnableAutoCloseUI_52()
	{
		logic_uScript_DisableClosingCraftingUIWhenTooFarAway_craftingBlock_52 = local_FusionMachineBlock_TankBlock;
		logic_uScript_DisableClosingCraftingUIWhenTooFarAway_uScript_DisableClosingCraftingUIWhenTooFarAway_52.EnableAutoCloseUI(logic_uScript_DisableClosingCraftingUIWhenTooFarAway_craftingBlock_52);
		if (logic_uScript_DisableClosingCraftingUIWhenTooFarAway_uScript_DisableClosingCraftingUIWhenTooFarAway_52.Out)
		{
			Relay_In_48();
		}
	}

	private void Relay_In_53()
	{
		logic_uScript_PointArrowAtVisible_targetObject_53 = local_FusionMachineBlock_TankBlock;
		logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_53.In(logic_uScript_PointArrowAtVisible_targetObject_53, logic_uScript_PointArrowAtVisible_timeToShowFor_53, logic_uScript_PointArrowAtVisible_offset_53);
		if (logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_53.Out)
		{
			Relay_In_47();
		}
	}

	private void Relay_Out_56()
	{
		Relay_In_53();
	}

	private void Relay_Shown_56()
	{
	}

	private void Relay_In_56()
	{
		logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboard_56 = msg03OpenMenu;
		logic_SubGraph_AddMessageWithPadSupport_messageControlPad_56 = msg03OpenMenu_Pad;
		logic_SubGraph_AddMessageWithPadSupport_speaker_56 = messageSpeaker;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_56.In(logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboard_56, logic_SubGraph_AddMessageWithPadSupport_messageControlPad_56, logic_SubGraph_AddMessageWithPadSupport_speaker_56);
	}

	private void Relay_True_58()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_58.True(out logic_uScriptAct_SetBool_Target_58);
		local_CraftingMenuOpened_System_Boolean = logic_uScriptAct_SetBool_Target_58;
	}

	private void Relay_False_58()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_58.False(out logic_uScriptAct_SetBool_Target_58);
		local_CraftingMenuOpened_System_Boolean = logic_uScriptAct_SetBool_Target_58;
	}

	private void Relay_In_59()
	{
		logic_uScript_HideArrow_uScript_HideArrow_59.In();
		if (logic_uScript_HideArrow_uScript_HideArrow_59.Out)
		{
			Relay_In_49();
		}
	}

	private void Relay_In_61()
	{
		logic_uScript_HideHUDElement_hudElement_61 = local_CraftingMenu_ManHUD_HUDElementType;
		logic_uScript_HideHUDElement_uScript_HideHUDElement_61.In(logic_uScript_HideHUDElement_hudElement_61);
		if (logic_uScript_HideHUDElement_uScript_HideHUDElement_61.Out)
		{
			Relay_In_7();
		}
	}

	private void Relay_In_68()
	{
		logic_uScriptCon_CompareBool_Bool_68 = local_CraftingMenuOpened_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_68.In(logic_uScriptCon_CompareBool_Bool_68);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_68.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_68.False;
		if (num)
		{
			Relay_In_330();
		}
		if (flag)
		{
			Relay_In_56();
		}
	}

	private void Relay_In_69()
	{
		logic_uScript_LockPlayerInput_uScript_LockPlayerInput_69.In(logic_uScript_LockPlayerInput_lockInput_69, logic_uScript_LockPlayerInput_includeCamera_69);
		if (logic_uScript_LockPlayerInput_uScript_LockPlayerInput_69.Out)
		{
			Relay_In_74();
		}
	}

	private void Relay_In_71()
	{
		logic_uScript_AddMessage_messageData_71 = msg07Complete;
		logic_uScript_AddMessage_speaker_71 = messageSpeaker;
		logic_uScript_AddMessage_Return_71 = logic_uScript_AddMessage_uScript_AddMessage_71.In(logic_uScript_AddMessage_messageData_71, logic_uScript_AddMessage_speaker_71);
		if (logic_uScript_AddMessage_uScript_AddMessage_71.Shown)
		{
			Relay_In_232();
		}
	}

	private void Relay_In_72()
	{
		logic_uScript_LockPlayerInput_uScript_LockPlayerInput_72.In(logic_uScript_LockPlayerInput_lockInput_72, logic_uScript_LockPlayerInput_includeCamera_72);
		if (logic_uScript_LockPlayerInput_uScript_LockPlayerInput_72.Out)
		{
			Relay_In_289();
			Relay_In_314();
			Relay_In_318();
			Relay_In_320();
		}
	}

	private void Relay_In_74()
	{
		logic_uScript_CraftingUIHighlightItem_itemToHighlight_74 = blockTypeToHighlight;
		logic_uScript_CraftingUIHighlightItem_uScript_CraftingUIHighlightItem_74.In(logic_uScript_CraftingUIHighlightItem_targetMenuType_74, logic_uScript_CraftingUIHighlightItem_itemToHighlight_74);
		if (logic_uScript_CraftingUIHighlightItem_uScript_CraftingUIHighlightItem_74.Selected)
		{
			Relay_In_264();
		}
	}

	private void Relay_In_78()
	{
		logic_uScript_LockPause_uScript_LockPause_78.In(logic_uScript_LockPause_lockPause_78, logic_uScript_LockPause_disabledReason_78);
		if (logic_uScript_LockPause_uScript_LockPause_78.Out)
		{
			Relay_In_89();
		}
	}

	private void Relay_In_79()
	{
		logic_uScript_LockHudGroup_uScript_LockHudGroup_79.In(logic_uScript_LockHudGroup_group_79, logic_uScript_LockHudGroup_locked_79);
		if (logic_uScript_LockHudGroup_uScript_LockHudGroup_79.Out)
		{
			Relay_In_86();
		}
	}

	private void Relay_True_80()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_80.True(out logic_uScriptAct_SetBool_Target_80);
		local_CraftingInProgress_System_Boolean = logic_uScriptAct_SetBool_Target_80;
	}

	private void Relay_False_80()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_80.False(out logic_uScriptAct_SetBool_Target_80);
		local_CraftingInProgress_System_Boolean = logic_uScriptAct_SetBool_Target_80;
	}

	private void Relay_In_82()
	{
		logic_uScript_LockPause_uScript_LockPause_82.In(logic_uScript_LockPause_lockPause_82, logic_uScript_LockPause_disabledReason_82);
		if (logic_uScript_LockPause_uScript_LockPause_82.Out)
		{
			Relay_In_79();
		}
	}

	private void Relay_Out_83()
	{
		Relay_In_82();
	}

	private void Relay_Shown_83()
	{
	}

	private void Relay_In_83()
	{
		logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboard_83 = msg04CraftBlock;
		logic_SubGraph_AddMessageWithPadSupport_messageControlPad_83 = msg04CraftBlock_Pad;
		logic_SubGraph_AddMessageWithPadSupport_speaker_83 = messageSpeaker;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_83.In(logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboard_83, logic_SubGraph_AddMessageWithPadSupport_messageControlPad_83, logic_SubGraph_AddMessageWithPadSupport_speaker_83);
	}

	private void Relay_In_84()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_84 = messageTag;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_84.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_84, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_84);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_84.Out)
		{
			Relay_True_80();
		}
	}

	private void Relay_In_86()
	{
		logic_uScript_LockTechInteraction_tech_86 = local_FusionCraftingBaseTech_Tank;
		int num = 0;
		if (logic_uScript_LockTechInteraction_excludedBlocks_86.Length <= num)
		{
			Array.Resize(ref logic_uScript_LockTechInteraction_excludedBlocks_86, num + 1);
		}
		logic_uScript_LockTechInteraction_excludedBlocks_86[num++] = BlockTypeFusionMachine;
		int num2 = 0;
		if (logic_uScript_LockTechInteraction_excludedUniqueBlocks_86.Length <= num2)
		{
			Array.Resize(ref logic_uScript_LockTechInteraction_excludedUniqueBlocks_86, num2 + 1);
		}
		logic_uScript_LockTechInteraction_excludedUniqueBlocks_86[num2++] = local_FusionMachineBlock_TankBlock;
		logic_uScript_LockTechInteraction_uScript_LockTechInteraction_86.In(logic_uScript_LockTechInteraction_tech_86, logic_uScript_LockTechInteraction_excludedBlocks_86, logic_uScript_LockTechInteraction_excludedUniqueBlocks_86);
		if (logic_uScript_LockTechInteraction_uScript_LockTechInteraction_86.Out)
		{
			Relay_In_69();
		}
	}

	private void Relay_In_87()
	{
		logic_uScriptCon_CompareBool_Bool_87 = local_AllBlocksCrafted_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_87.In(logic_uScriptCon_CompareBool_Bool_87);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_87.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_87.False;
		if (num)
		{
			Relay_In_255();
		}
		if (flag)
		{
			Relay_In_256();
		}
	}

	private void Relay_In_89()
	{
		logic_uScript_LockHudGroup_uScript_LockHudGroup_89.In(logic_uScript_LockHudGroup_group_89, logic_uScript_LockHudGroup_locked_89);
		if (logic_uScript_LockHudGroup_uScript_LockHudGroup_89.Out)
		{
			Relay_In_72();
		}
	}

	private void Relay_DisableAutoCloseUI_91()
	{
		logic_uScript_DisableClosingCraftingUIWhenTooFarAway_craftingBlock_91 = local_FusionMachineBlock_TankBlock;
		logic_uScript_DisableClosingCraftingUIWhenTooFarAway_uScript_DisableClosingCraftingUIWhenTooFarAway_91.DisableAutoCloseUI(logic_uScript_DisableClosingCraftingUIWhenTooFarAway_craftingBlock_91);
		if (logic_uScript_DisableClosingCraftingUIWhenTooFarAway_uScript_DisableClosingCraftingUIWhenTooFarAway_91.Out)
		{
			Relay_In_84();
		}
	}

	private void Relay_EnableAutoCloseUI_91()
	{
		logic_uScript_DisableClosingCraftingUIWhenTooFarAway_craftingBlock_91 = local_FusionMachineBlock_TankBlock;
		logic_uScript_DisableClosingCraftingUIWhenTooFarAway_uScript_DisableClosingCraftingUIWhenTooFarAway_91.EnableAutoCloseUI(logic_uScript_DisableClosingCraftingUIWhenTooFarAway_craftingBlock_91);
		if (logic_uScript_DisableClosingCraftingUIWhenTooFarAway_uScript_DisableClosingCraftingUIWhenTooFarAway_91.Out)
		{
			Relay_In_84();
		}
	}

	private void Relay_Save_Out_96()
	{
		Relay_Save_98();
	}

	private void Relay_Load_Out_96()
	{
		Relay_Load_98();
	}

	private void Relay_Restart_Out_96()
	{
		Relay_Set_False_98();
	}

	private void Relay_Save_96()
	{
		logic_SubGraph_SaveLoadBool_boolean_96 = local_CraftingMenuOpened_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_96 = local_CraftingMenuOpened_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_96.Save(ref logic_SubGraph_SaveLoadBool_boolean_96, logic_SubGraph_SaveLoadBool_boolAsVariable_96, logic_SubGraph_SaveLoadBool_uniqueID_96);
	}

	private void Relay_Load_96()
	{
		logic_SubGraph_SaveLoadBool_boolean_96 = local_CraftingMenuOpened_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_96 = local_CraftingMenuOpened_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_96.Load(ref logic_SubGraph_SaveLoadBool_boolean_96, logic_SubGraph_SaveLoadBool_boolAsVariable_96, logic_SubGraph_SaveLoadBool_uniqueID_96);
	}

	private void Relay_Set_True_96()
	{
		logic_SubGraph_SaveLoadBool_boolean_96 = local_CraftingMenuOpened_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_96 = local_CraftingMenuOpened_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_96.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_96, logic_SubGraph_SaveLoadBool_boolAsVariable_96, logic_SubGraph_SaveLoadBool_uniqueID_96);
	}

	private void Relay_Set_False_96()
	{
		logic_SubGraph_SaveLoadBool_boolean_96 = local_CraftingMenuOpened_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_96 = local_CraftingMenuOpened_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_96.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_96, logic_SubGraph_SaveLoadBool_boolAsVariable_96, logic_SubGraph_SaveLoadBool_uniqueID_96);
	}

	private void Relay_Save_Out_97()
	{
	}

	private void Relay_Load_Out_97()
	{
		Relay_In_45();
	}

	private void Relay_Restart_Out_97()
	{
		Relay_False_33();
	}

	private void Relay_Save_97()
	{
		logic_SubGraph_SaveLoadBool_boolean_97 = local_AllBlocksCrafted_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_97 = local_AllBlocksCrafted_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_97.Save(ref logic_SubGraph_SaveLoadBool_boolean_97, logic_SubGraph_SaveLoadBool_boolAsVariable_97, logic_SubGraph_SaveLoadBool_uniqueID_97);
	}

	private void Relay_Load_97()
	{
		logic_SubGraph_SaveLoadBool_boolean_97 = local_AllBlocksCrafted_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_97 = local_AllBlocksCrafted_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_97.Load(ref logic_SubGraph_SaveLoadBool_boolean_97, logic_SubGraph_SaveLoadBool_boolAsVariable_97, logic_SubGraph_SaveLoadBool_uniqueID_97);
	}

	private void Relay_Set_True_97()
	{
		logic_SubGraph_SaveLoadBool_boolean_97 = local_AllBlocksCrafted_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_97 = local_AllBlocksCrafted_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_97.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_97, logic_SubGraph_SaveLoadBool_boolAsVariable_97, logic_SubGraph_SaveLoadBool_uniqueID_97);
	}

	private void Relay_Set_False_97()
	{
		logic_SubGraph_SaveLoadBool_boolean_97 = local_AllBlocksCrafted_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_97 = local_AllBlocksCrafted_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_97.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_97, logic_SubGraph_SaveLoadBool_boolAsVariable_97, logic_SubGraph_SaveLoadBool_uniqueID_97);
	}

	private void Relay_Save_Out_98()
	{
		Relay_Save_97();
	}

	private void Relay_Load_Out_98()
	{
		Relay_Load_97();
	}

	private void Relay_Restart_Out_98()
	{
		Relay_Set_False_97();
	}

	private void Relay_Save_98()
	{
		logic_SubGraph_SaveLoadBool_boolean_98 = local_CraftingInProgress_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_98 = local_CraftingInProgress_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_98.Save(ref logic_SubGraph_SaveLoadBool_boolean_98, logic_SubGraph_SaveLoadBool_boolAsVariable_98, logic_SubGraph_SaveLoadBool_uniqueID_98);
	}

	private void Relay_Load_98()
	{
		logic_SubGraph_SaveLoadBool_boolean_98 = local_CraftingInProgress_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_98 = local_CraftingInProgress_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_98.Load(ref logic_SubGraph_SaveLoadBool_boolean_98, logic_SubGraph_SaveLoadBool_boolAsVariable_98, logic_SubGraph_SaveLoadBool_uniqueID_98);
	}

	private void Relay_Set_True_98()
	{
		logic_SubGraph_SaveLoadBool_boolean_98 = local_CraftingInProgress_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_98 = local_CraftingInProgress_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_98.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_98, logic_SubGraph_SaveLoadBool_boolAsVariable_98, logic_SubGraph_SaveLoadBool_uniqueID_98);
	}

	private void Relay_Set_False_98()
	{
		logic_SubGraph_SaveLoadBool_boolean_98 = local_CraftingInProgress_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_98 = local_CraftingInProgress_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_98.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_98, logic_SubGraph_SaveLoadBool_boolAsVariable_98, logic_SubGraph_SaveLoadBool_uniqueID_98);
	}

	private void Relay_In_103()
	{
		logic_uScriptCon_CompareBool_Bool_103 = local_CraftingMenuOpened_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_103.In(logic_uScriptCon_CompareBool_Bool_103);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_103.True)
		{
			Relay_In_111();
		}
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
		logic_uScript_AddMessage_messageData_105 = msgLeavingMissionArea;
		logic_uScript_AddMessage_speaker_105 = messageSpeaker;
		logic_uScript_AddMessage_Return_105 = logic_uScript_AddMessage_uScript_AddMessage_105.In(logic_uScript_AddMessage_messageData_105, logic_uScript_AddMessage_speaker_105);
		if (logic_uScript_AddMessage_uScript_AddMessage_105.Out)
		{
			Relay_False_104();
		}
	}

	private void Relay_In_106()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_106.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_106.Out)
		{
			Relay_In_112();
			Relay_In_103();
		}
	}

	private void Relay_DisableAutoCloseUI_108()
	{
		logic_uScript_DisableClosingCraftingUIWhenTooFarAway_craftingBlock_108 = local_FusionMachineBlock_TankBlock;
		logic_uScript_DisableClosingCraftingUIWhenTooFarAway_uScript_DisableClosingCraftingUIWhenTooFarAway_108.DisableAutoCloseUI(logic_uScript_DisableClosingCraftingUIWhenTooFarAway_craftingBlock_108);
		if (logic_uScript_DisableClosingCraftingUIWhenTooFarAway_uScript_DisableClosingCraftingUIWhenTooFarAway_108.Out)
		{
			Relay_In_106();
		}
	}

	private void Relay_EnableAutoCloseUI_108()
	{
		logic_uScript_DisableClosingCraftingUIWhenTooFarAway_craftingBlock_108 = local_FusionMachineBlock_TankBlock;
		logic_uScript_DisableClosingCraftingUIWhenTooFarAway_uScript_DisableClosingCraftingUIWhenTooFarAway_108.EnableAutoCloseUI(logic_uScript_DisableClosingCraftingUIWhenTooFarAway_craftingBlock_108);
		if (logic_uScript_DisableClosingCraftingUIWhenTooFarAway_uScript_DisableClosingCraftingUIWhenTooFarAway_108.Out)
		{
			Relay_In_106();
		}
	}

	private void Relay_In_110()
	{
		logic_uScript_EnableGlow_targetObject_110 = local_FusionMachineBlock_TankBlock;
		logic_uScript_EnableGlow_uScript_EnableGlow_110.In(logic_uScript_EnableGlow_targetObject_110, logic_uScript_EnableGlow_enable_110);
		if (logic_uScript_EnableGlow_uScript_EnableGlow_110.Out)
		{
			Relay_EnableAutoCloseUI_108();
		}
	}

	private void Relay_In_111()
	{
		logic_uScriptCon_CompareBool_Bool_111 = local_CraftingInProgress_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_111.In(logic_uScriptCon_CompareBool_Bool_111);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_111.False)
		{
			Relay_False_114();
		}
	}

	private void Relay_In_112()
	{
		logic_uScriptCon_CompareBool_Bool_112 = local_NearBase_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_112.In(logic_uScriptCon_CompareBool_Bool_112);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_112.True)
		{
			Relay_In_105();
		}
	}

	private void Relay_True_114()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_114.True(out logic_uScriptAct_SetBool_Target_114);
		local_CraftingMenuOpened_System_Boolean = logic_uScriptAct_SetBool_Target_114;
	}

	private void Relay_False_114()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_114.False(out logic_uScriptAct_SetBool_Target_114);
		local_CraftingMenuOpened_System_Boolean = logic_uScriptAct_SetBool_Target_114;
	}

	private void Relay_In_120()
	{
		logic_uScriptCon_CompareBool_Bool_120 = local_FusionMachineSpawned_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_120.In(logic_uScriptCon_CompareBool_Bool_120);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_120.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_120.False;
		if (num)
		{
			Relay_In_110();
		}
		if (flag)
		{
			Relay_In_121();
		}
	}

	private void Relay_In_121()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_121.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_121.Out)
		{
			Relay_In_106();
		}
	}

	private void Relay_In_125()
	{
		int num = 0;
		if (logic_uScriptAct_Concatenate_A_125.Length <= num)
		{
			Array.Resize(ref logic_uScriptAct_Concatenate_A_125, num + 1);
		}
		logic_uScriptAct_Concatenate_A_125[num++] = local_129_System_String;
		logic_uScriptAct_Concatenate_uScriptAct_Concatenate_125.In(logic_uScriptAct_Concatenate_A_125, logic_uScriptAct_Concatenate_B_125, logic_uScriptAct_Concatenate_Separator_125, out logic_uScriptAct_Concatenate_Result_125);
		local_126_System_String = logic_uScriptAct_Concatenate_Result_125;
		if (logic_uScriptAct_Concatenate_uScriptAct_Concatenate_125.Out)
		{
			Relay_ShowLabel_127();
		}
	}

	private void Relay_ShowLabel_127()
	{
		logic_uScriptAct_PrintText_Text_127 = local_126_System_String;
		logic_uScriptAct_PrintText_uScriptAct_PrintText_127.ShowLabel(logic_uScriptAct_PrintText_Text_127, logic_uScriptAct_PrintText_FontSize_127, logic_uScriptAct_PrintText_FontStyle_127, logic_uScriptAct_PrintText_FontColor_127, logic_uScriptAct_PrintText_textAnchor_127, logic_uScriptAct_PrintText_EdgePadding_127, logic_uScriptAct_PrintText_time_127);
	}

	private void Relay_HideLabel_127()
	{
		logic_uScriptAct_PrintText_Text_127 = local_126_System_String;
		logic_uScriptAct_PrintText_uScriptAct_PrintText_127.HideLabel(logic_uScriptAct_PrintText_Text_127, logic_uScriptAct_PrintText_FontSize_127, logic_uScriptAct_PrintText_FontStyle_127, logic_uScriptAct_PrintText_FontColor_127, logic_uScriptAct_PrintText_textAnchor_127, logic_uScriptAct_PrintText_EdgePadding_127, logic_uScriptAct_PrintText_time_127);
	}

	private void Relay_In_131()
	{
		logic_uScriptCon_CompareBool_Bool_131 = local_CanInteractWithFusionMachine_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_131.In(logic_uScriptCon_CompareBool_Bool_131);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_131.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_131.False;
		if (num)
		{
			Relay_In_132();
		}
		if (flag)
		{
			Relay_In_35();
		}
	}

	private void Relay_In_132()
	{
		logic_uScript_LockTechInteraction_tech_132 = local_CraftingBaseTech_Tank;
		int num = 0;
		if (logic_uScript_LockTechInteraction_excludedBlocks_132.Length <= num)
		{
			Array.Resize(ref logic_uScript_LockTechInteraction_excludedBlocks_132, num + 1);
		}
		logic_uScript_LockTechInteraction_excludedBlocks_132[num++] = BlockTypeFusionMachine;
		int num2 = 0;
		if (logic_uScript_LockTechInteraction_excludedUniqueBlocks_132.Length <= num2)
		{
			Array.Resize(ref logic_uScript_LockTechInteraction_excludedUniqueBlocks_132, num2 + 1);
		}
		logic_uScript_LockTechInteraction_excludedUniqueBlocks_132[num2++] = local_FusionMachineBlock_TankBlock;
		logic_uScript_LockTechInteraction_uScript_LockTechInteraction_132.In(logic_uScript_LockTechInteraction_tech_132, logic_uScript_LockTechInteraction_excludedBlocks_132, logic_uScript_LockTechInteraction_excludedUniqueBlocks_132);
		if (logic_uScript_LockTechInteraction_uScript_LockTechInteraction_132.Out)
		{
			Relay_In_37();
		}
	}

	private void Relay_True_135()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_135.True(out logic_uScriptAct_SetBool_Target_135);
		local_CanInteractWithFusionMachine_System_Boolean = logic_uScriptAct_SetBool_Target_135;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_135.Out)
		{
			Relay_In_68();
		}
	}

	private void Relay_False_135()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_135.False(out logic_uScriptAct_SetBool_Target_135);
		local_CanInteractWithFusionMachine_System_Boolean = logic_uScriptAct_SetBool_Target_135;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_135.Out)
		{
			Relay_In_68();
		}
	}

	private void Relay_Save_Out_138()
	{
		Relay_Save_96();
	}

	private void Relay_Load_Out_138()
	{
		Relay_Load_96();
	}

	private void Relay_Restart_Out_138()
	{
		Relay_Set_False_96();
	}

	private void Relay_Save_138()
	{
		logic_SubGraph_SaveLoadBool_boolean_138 = local_CanInteractWithFusionMachine_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_138 = local_CanInteractWithFusionMachine_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_138.Save(ref logic_SubGraph_SaveLoadBool_boolean_138, logic_SubGraph_SaveLoadBool_boolAsVariable_138, logic_SubGraph_SaveLoadBool_uniqueID_138);
	}

	private void Relay_Load_138()
	{
		logic_SubGraph_SaveLoadBool_boolean_138 = local_CanInteractWithFusionMachine_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_138 = local_CanInteractWithFusionMachine_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_138.Load(ref logic_SubGraph_SaveLoadBool_boolean_138, logic_SubGraph_SaveLoadBool_boolAsVariable_138, logic_SubGraph_SaveLoadBool_uniqueID_138);
	}

	private void Relay_Set_True_138()
	{
		logic_SubGraph_SaveLoadBool_boolean_138 = local_CanInteractWithFusionMachine_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_138 = local_CanInteractWithFusionMachine_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_138.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_138, logic_SubGraph_SaveLoadBool_boolAsVariable_138, logic_SubGraph_SaveLoadBool_uniqueID_138);
	}

	private void Relay_Set_False_138()
	{
		logic_SubGraph_SaveLoadBool_boolean_138 = local_CanInteractWithFusionMachine_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_138 = local_CanInteractWithFusionMachine_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_138.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_138, logic_SubGraph_SaveLoadBool_boolAsVariable_138, logic_SubGraph_SaveLoadBool_uniqueID_138);
	}

	private void Relay_Output1_140()
	{
		Relay_In_279();
	}

	private void Relay_Output2_140()
	{
		Relay_In_281();
	}

	private void Relay_Output3_140()
	{
		Relay_In_71();
	}

	private void Relay_Output4_140()
	{
	}

	private void Relay_Output5_140()
	{
	}

	private void Relay_Output6_140()
	{
	}

	private void Relay_Output7_140()
	{
	}

	private void Relay_Output8_140()
	{
	}

	private void Relay_In_140()
	{
		logic_uScriptCon_ManualSwitch_CurrentOutput_140 = local_Stage3_System_Int32;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_140.In(logic_uScriptCon_ManualSwitch_CurrentOutput_140);
	}

	private void Relay_In_144()
	{
		logic_uScript_AddMessage_messageData_144 = msg06AllBlocksCrafted;
		logic_uScript_AddMessage_speaker_144 = messageSpeaker;
		logic_uScript_AddMessage_Return_144 = logic_uScript_AddMessage_uScript_AddMessage_144.In(logic_uScript_AddMessage_messageData_144, logic_uScript_AddMessage_speaker_144);
		if (logic_uScript_AddMessage_uScript_AddMessage_144.Shown)
		{
			Relay_In_146();
		}
	}

	private void Relay_In_146()
	{
		logic_uScriptAct_AddInt_v2_A_146 = local_Stage3_System_Int32;
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_146.In(logic_uScriptAct_AddInt_v2_A_146, logic_uScriptAct_AddInt_v2_B_146, out logic_uScriptAct_AddInt_v2_IntResult_146, out logic_uScriptAct_AddInt_v2_FloatResult_146);
		local_Stage3_System_Int32 = logic_uScriptAct_AddInt_v2_IntResult_146;
	}

	private void Relay_In_147()
	{
		logic_uScriptCon_CompareInt_A_147 = local_CurrentAmountType_System_Int32;
		logic_uScriptCon_CompareInt_B_147 = targetAmount;
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_147.In(logic_uScriptCon_CompareInt_A_147, logic_uScriptCon_CompareInt_B_147);
		if (logic_uScriptCon_CompareInt_uScriptCon_CompareInt_147.GreaterThanOrEqualTo)
		{
			Relay_True_158();
		}
	}

	private void Relay_In_148()
	{
		logic_uScript_GetTankBlock_tank_148 = local_CraftingBaseTech_Tank;
		logic_uScript_GetTankBlock_blockType_148 = BlockTypeFusionMachine;
		logic_uScript_GetTankBlock_Return_148 = logic_uScript_GetTankBlock_uScript_GetTankBlock_148.In(logic_uScript_GetTankBlock_tank_148, logic_uScript_GetTankBlock_blockType_148);
		local_FusionMachineBlock_TankBlock = logic_uScript_GetTankBlock_Return_148;
		if (logic_uScript_GetTankBlock_uScript_GetTankBlock_148.Returned)
		{
			Relay_In_131();
		}
	}

	private void Relay_In_153()
	{
		logic_uScript_CompareBlockTypes_A_153 = local_lastCraftedBlockType_BlockTypes;
		logic_uScript_CompareBlockTypes_B_153 = targetBlockType;
		logic_uScript_CompareBlockTypes_uScript_CompareBlockTypes_153.In(logic_uScript_CompareBlockTypes_A_153, logic_uScript_CompareBlockTypes_B_153);
		if (logic_uScript_CompareBlockTypes_uScript_CompareBlockTypes_153.EqualTo)
		{
			Relay_In_326();
		}
	}

	private void Relay_BlockCraftedEvent_154()
	{
		local_lastCraftedBlockType_BlockTypes = event_UnityEngine_GameObject_BlockType_154;
		local_lastCraftedBlock_TankBlock = event_UnityEngine_GameObject_Block_154;
		local_CrafterFromEvent_TankBlock = event_UnityEngine_GameObject_CrafterBlock_154;
		Relay_In_276();
	}

	private void Relay_In_157()
	{
		logic_uScriptAct_AddInt_v2_A_157 = local_CurrentAmountType_System_Int32;
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_157.In(logic_uScriptAct_AddInt_v2_A_157, logic_uScriptAct_AddInt_v2_B_157, out logic_uScriptAct_AddInt_v2_IntResult_157, out logic_uScriptAct_AddInt_v2_FloatResult_157);
		local_CurrentAmountType_System_Int32 = logic_uScriptAct_AddInt_v2_IntResult_157;
		if (logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_157.Out)
		{
			Relay_SetCount_183();
		}
	}

	private void Relay_True_158()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_158.True(out logic_uScriptAct_SetBool_Target_158);
		local_AllBlocksCrafted_System_Boolean = logic_uScriptAct_SetBool_Target_158;
	}

	private void Relay_False_158()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_158.False(out logic_uScriptAct_SetBool_Target_158);
		local_AllBlocksCrafted_System_Boolean = logic_uScriptAct_SetBool_Target_158;
	}

	private void Relay_InitialSpawn_162()
	{
		int num = 0;
		Array enemyTechData = EnemyTechData;
		if (logic_uScript_SpawnTechsFromData_spawnData_162.Length != num + enemyTechData.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_162, num + enemyTechData.Length);
		}
		Array.Copy(enemyTechData, 0, logic_uScript_SpawnTechsFromData_spawnData_162, num, enemyTechData.Length);
		num += enemyTechData.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_162 = owner_Connection_169;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_162.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_162, logic_uScript_SpawnTechsFromData_ownerNode_162, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_162, logic_uScript_SpawnTechsFromData_allowResurrection_162);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_162.Out)
		{
			Relay_True_167();
		}
	}

	private void Relay_True_163()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_163.True(out logic_uScriptAct_SetBool_Target_163);
		local_EnemyAlive_System_Boolean = logic_uScriptAct_SetBool_Target_163;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_163.Out)
		{
			Relay_In_176();
		}
	}

	private void Relay_False_163()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_163.False(out logic_uScriptAct_SetBool_Target_163);
		local_EnemyAlive_System_Boolean = logic_uScriptAct_SetBool_Target_163;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_163.Out)
		{
			Relay_In_176();
		}
	}

	private void Relay_In_164()
	{
		int num = 0;
		Array enemyTechData = EnemyTechData;
		if (logic_uScript_GetAndCheckTechs_techData_164.Length != num + enemyTechData.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_164, num + enemyTechData.Length);
		}
		Array.Copy(enemyTechData, 0, logic_uScript_GetAndCheckTechs_techData_164, num, enemyTechData.Length);
		num += enemyTechData.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_164 = owner_Connection_165;
		int num2 = 0;
		Array array = local_Enemy_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_164.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_164, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techs_164, num2, array.Length);
		num2 += array.Length;
		logic_uScript_GetAndCheckTechs_Return_164 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_164.In(logic_uScript_GetAndCheckTechs_techData_164, logic_uScript_GetAndCheckTechs_ownerNode_164, ref logic_uScript_GetAndCheckTechs_techs_164);
		local_Enemy_TankArray = logic_uScript_GetAndCheckTechs_techs_164;
		if (logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_164.AllDead)
		{
			Relay_False_163();
		}
	}

	private void Relay_True_167()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_167.True(out logic_uScriptAct_SetBool_Target_167);
		local_EnemyAlive_System_Boolean = logic_uScriptAct_SetBool_Target_167;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_167.Out)
		{
			Relay_In_164();
		}
	}

	private void Relay_False_167()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_167.False(out logic_uScriptAct_SetBool_Target_167);
		local_EnemyAlive_System_Boolean = logic_uScriptAct_SetBool_Target_167;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_167.Out)
		{
			Relay_In_164();
		}
	}

	private void Relay_In_168()
	{
		logic_uScriptCon_CompareBool_Bool_168 = local_EnemyAlive_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_168.In(logic_uScriptCon_CompareBool_Bool_168);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_168.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_168.False;
		if (num)
		{
			Relay_In_172();
		}
		if (flag)
		{
			Relay_InitialSpawn_162();
		}
	}

	private void Relay_In_172()
	{
		logic_uScript_AddMessage_messageData_172 = msgSpawnMinion;
		logic_uScript_AddMessage_speaker_172 = messageSpeaker;
		logic_uScript_AddMessage_Return_172 = logic_uScript_AddMessage_uScript_AddMessage_172.In(logic_uScript_AddMessage_messageData_172, logic_uScript_AddMessage_speaker_172);
		if (logic_uScript_AddMessage_uScript_AddMessage_172.Out)
		{
			Relay_In_164();
		}
	}

	private void Relay_In_176()
	{
		logic_uScriptAct_AddInt_v2_A_176 = local_Stage3_System_Int32;
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_176.In(logic_uScriptAct_AddInt_v2_A_176, logic_uScriptAct_AddInt_v2_B_176, out logic_uScriptAct_AddInt_v2_IntResult_176, out logic_uScriptAct_AddInt_v2_FloatResult_176);
		local_Stage3_System_Int32 = logic_uScriptAct_AddInt_v2_IntResult_176;
	}

	private void Relay_Save_Out_178()
	{
		Relay_Save_207();
	}

	private void Relay_Load_Out_178()
	{
		Relay_Load_207();
	}

	private void Relay_Restart_Out_178()
	{
		Relay_Set_False_207();
	}

	private void Relay_Save_178()
	{
		logic_SubGraph_SaveLoadInt_integer_178 = local_CurrentAmountType_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_178 = local_CurrentAmountType_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_178.Save(logic_SubGraph_SaveLoadInt_restartValue_178, ref logic_SubGraph_SaveLoadInt_integer_178, logic_SubGraph_SaveLoadInt_intAsVariable_178, logic_SubGraph_SaveLoadInt_uniqueID_178);
	}

	private void Relay_Load_178()
	{
		logic_SubGraph_SaveLoadInt_integer_178 = local_CurrentAmountType_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_178 = local_CurrentAmountType_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_178.Load(logic_SubGraph_SaveLoadInt_restartValue_178, ref logic_SubGraph_SaveLoadInt_integer_178, logic_SubGraph_SaveLoadInt_intAsVariable_178, logic_SubGraph_SaveLoadInt_uniqueID_178);
	}

	private void Relay_Restart_178()
	{
		logic_SubGraph_SaveLoadInt_integer_178 = local_CurrentAmountType_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_178 = local_CurrentAmountType_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_178.Restart(logic_SubGraph_SaveLoadInt_restartValue_178, ref logic_SubGraph_SaveLoadInt_integer_178, logic_SubGraph_SaveLoadInt_intAsVariable_178, logic_SubGraph_SaveLoadInt_uniqueID_178);
	}

	private void Relay_SetCount_181()
	{
		logic_uScript_SetQuestObjectiveCount_owner_181 = owner_Connection_182;
		logic_uScript_SetQuestObjectiveCount_objectiveId_181 = local_Stage_System_Int32;
		logic_uScript_SetQuestObjectiveCount_currentCount_181 = local_CurrentAmountType_System_Int32;
		logic_uScript_SetQuestObjectiveCount_uScript_SetQuestObjectiveCount_181.SetCount(logic_uScript_SetQuestObjectiveCount_owner_181, logic_uScript_SetQuestObjectiveCount_objectiveId_181, logic_uScript_SetQuestObjectiveCount_currentCount_181);
		if (logic_uScript_SetQuestObjectiveCount_uScript_SetQuestObjectiveCount_181.Out)
		{
			Relay_In_83();
		}
	}

	private void Relay_SetCount_183()
	{
		logic_uScript_SetQuestObjectiveCount_owner_183 = owner_Connection_184;
		logic_uScript_SetQuestObjectiveCount_objectiveId_183 = local_Stage_System_Int32;
		logic_uScript_SetQuestObjectiveCount_currentCount_183 = local_CurrentAmountType_System_Int32;
		logic_uScript_SetQuestObjectiveCount_uScript_SetQuestObjectiveCount_183.SetCount(logic_uScript_SetQuestObjectiveCount_owner_183, logic_uScript_SetQuestObjectiveCount_objectiveId_183, logic_uScript_SetQuestObjectiveCount_currentCount_183);
		if (logic_uScript_SetQuestObjectiveCount_uScript_SetQuestObjectiveCount_183.Out)
		{
			Relay_In_293();
		}
	}

	private void Relay_In_188()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_188.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_188.Out)
		{
			Relay_In_240();
		}
	}

	private void Relay_In_190()
	{
		logic_uScript_RemoveScenery_ownerNode_190 = owner_Connection_217;
		logic_uScript_RemoveScenery_positionName_190 = BasePosition;
		logic_uScript_RemoveScenery_radius_190 = clearSceneryRadius;
		logic_uScript_RemoveScenery_uScript_RemoveScenery_190.In(logic_uScript_RemoveScenery_ownerNode_190, logic_uScript_RemoveScenery_positionName_190, logic_uScript_RemoveScenery_radius_190, logic_uScript_RemoveScenery_preventChunksSpawning_190);
		if (logic_uScript_RemoveScenery_uScript_RemoveScenery_190.Out)
		{
			Relay_In_192();
		}
	}

	private void Relay_AtIndex_191()
	{
		int num = 0;
		Array array = local_221_TankArray;
		if (logic_uScript_AccessListTech_techList_191.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_191, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_191, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_191.AtIndex(ref logic_uScript_AccessListTech_techList_191, logic_uScript_AccessListTech_index_191, out logic_uScript_AccessListTech_value_191);
		local_221_TankArray = logic_uScript_AccessListTech_techList_191;
		local_CraftingBaseTech_Tank = logic_uScript_AccessListTech_value_191;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_191.Out)
		{
			Relay_In_234();
		}
	}

	private void Relay_In_192()
	{
		int num = 0;
		Array craftingBaseSpawnData = CraftingBaseSpawnData;
		if (logic_uScript_GetAndCheckTechs_techData_192.Length != num + craftingBaseSpawnData.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_192, num + craftingBaseSpawnData.Length);
		}
		Array.Copy(craftingBaseSpawnData, 0, logic_uScript_GetAndCheckTechs_techData_192, num, craftingBaseSpawnData.Length);
		num += craftingBaseSpawnData.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_192 = owner_Connection_204;
		int num2 = 0;
		Array array = local_221_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_192.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_192, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techs_192, num2, array.Length);
		num2 += array.Length;
		logic_uScript_GetAndCheckTechs_Return_192 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_192.In(logic_uScript_GetAndCheckTechs_techData_192, logic_uScript_GetAndCheckTechs_ownerNode_192, ref logic_uScript_GetAndCheckTechs_techs_192);
		local_221_TankArray = logic_uScript_GetAndCheckTechs_techs_192;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_192.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_192.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_192.AllDead;
		if (allAlive)
		{
			Relay_AtIndex_191();
		}
		if (someAlive)
		{
			Relay_AtIndex_191();
		}
		if (allDead)
		{
			Relay_In_216();
		}
	}

	private void Relay_True_194()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_194.True(out logic_uScriptAct_SetBool_Target_194);
		local_Initialize_System_Boolean = logic_uScriptAct_SetBool_Target_194;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_194.Out)
		{
			Relay_InitialSpawn_200();
		}
	}

	private void Relay_False_194()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_194.False(out logic_uScriptAct_SetBool_Target_194);
		local_Initialize_System_Boolean = logic_uScriptAct_SetBool_Target_194;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_194.Out)
		{
			Relay_InitialSpawn_200();
		}
	}

	private void Relay_In_197()
	{
		logic_uScript_SpawnVFX_ownerNode_197 = owner_Connection_199;
		logic_uScript_SpawnVFX_vfxToSpawn_197 = NPCDespawnParticleEffect;
		logic_uScript_SpawnVFX_spawnPosName_197 = BaseVFXSpawn;
		logic_uScript_SpawnVFX_uScript_SpawnVFX_197.In(logic_uScript_SpawnVFX_ownerNode_197, logic_uScript_SpawnVFX_vfxToSpawn_197, logic_uScript_SpawnVFX_spawnPosName_197);
		if (logic_uScript_SpawnVFX_uScript_SpawnVFX_197.Out)
		{
			Relay_In_212();
		}
	}

	private void Relay_In_198()
	{
		int num = 0;
		Array nPCSpawnData = NPCSpawnData;
		if (logic_uScript_GetAndCheckTechs_techData_198.Length != num + nPCSpawnData.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_198, num + nPCSpawnData.Length);
		}
		Array.Copy(nPCSpawnData, 0, logic_uScript_GetAndCheckTechs_techData_198, num, nPCSpawnData.Length);
		num += nPCSpawnData.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_198 = owner_Connection_236;
		int num2 = 0;
		Array array = local_229_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_198.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_198, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techs_198, num2, array.Length);
		num2 += array.Length;
		logic_uScript_GetAndCheckTechs_Return_198 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_198.In(logic_uScript_GetAndCheckTechs_techData_198, logic_uScript_GetAndCheckTechs_ownerNode_198, ref logic_uScript_GetAndCheckTechs_techs_198);
		local_229_TankArray = logic_uScript_GetAndCheckTechs_techs_198;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_198.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_198.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_198.AllDead;
		if (allAlive)
		{
			Relay_AtIndex_235();
		}
		if (someAlive)
		{
			Relay_AtIndex_235();
		}
		if (allDead)
		{
			Relay_In_188();
		}
	}

	private void Relay_InitialSpawn_200()
	{
		int num = 0;
		Array craftingBaseSpawnData = CraftingBaseSpawnData;
		if (logic_uScript_SpawnTechsFromData_spawnData_200.Length != num + craftingBaseSpawnData.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_200, num + craftingBaseSpawnData.Length);
		}
		Array.Copy(craftingBaseSpawnData, 0, logic_uScript_SpawnTechsFromData_spawnData_200, num, craftingBaseSpawnData.Length);
		num += craftingBaseSpawnData.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_200 = owner_Connection_208;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_200.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_200, logic_uScript_SpawnTechsFromData_ownerNode_200, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_200, logic_uScript_SpawnTechsFromData_allowResurrection_200);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_200.Out)
		{
			Relay_InitialSpawn_201();
		}
	}

	private void Relay_InitialSpawn_201()
	{
		int num = 0;
		Array nPCSpawnData = NPCSpawnData;
		if (logic_uScript_SpawnTechsFromData_spawnData_201.Length != num + nPCSpawnData.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_201, num + nPCSpawnData.Length);
		}
		Array.Copy(nPCSpawnData, 0, logic_uScript_SpawnTechsFromData_spawnData_201, num, nPCSpawnData.Length);
		num += nPCSpawnData.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_201 = owner_Connection_227;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_201.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_201, logic_uScript_SpawnTechsFromData_ownerNode_201, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_201, logic_uScript_SpawnTechsFromData_allowResurrection_201);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_201.Out)
		{
			Relay_In_190();
		}
	}

	private void Relay_Save_Out_207()
	{
		Relay_Save_20();
	}

	private void Relay_Load_Out_207()
	{
		Relay_Load_20();
	}

	private void Relay_Restart_Out_207()
	{
		Relay_Set_False_20();
	}

	private void Relay_Save_207()
	{
		logic_SubGraph_SaveLoadBool_boolean_207 = local_Initialize_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_207 = local_Initialize_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_207.Save(ref logic_SubGraph_SaveLoadBool_boolean_207, logic_SubGraph_SaveLoadBool_boolAsVariable_207, logic_SubGraph_SaveLoadBool_uniqueID_207);
	}

	private void Relay_Load_207()
	{
		logic_SubGraph_SaveLoadBool_boolean_207 = local_Initialize_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_207 = local_Initialize_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_207.Load(ref logic_SubGraph_SaveLoadBool_boolean_207, logic_SubGraph_SaveLoadBool_boolAsVariable_207, logic_SubGraph_SaveLoadBool_uniqueID_207);
	}

	private void Relay_Set_True_207()
	{
		logic_SubGraph_SaveLoadBool_boolean_207 = local_Initialize_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_207 = local_Initialize_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_207.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_207, logic_SubGraph_SaveLoadBool_boolAsVariable_207, logic_SubGraph_SaveLoadBool_uniqueID_207);
	}

	private void Relay_Set_False_207()
	{
		logic_SubGraph_SaveLoadBool_boolean_207 = local_Initialize_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_207 = local_Initialize_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_207.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_207, logic_SubGraph_SaveLoadBool_boolAsVariable_207, logic_SubGraph_SaveLoadBool_uniqueID_207);
	}

	private void Relay_In_209()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_209.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_209.Out)
		{
			Relay_In_198();
		}
	}

	private void Relay_In_210()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_210.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_210.Out)
		{
			Relay_In_192();
		}
	}

	private void Relay_In_212()
	{
		logic_uScript_RemoveTech_tech_212 = local_CraftingBaseTech_Tank;
		logic_uScript_RemoveTech_uScript_RemoveTech_212.In(logic_uScript_RemoveTech_tech_212);
	}

	private void Relay_In_213()
	{
		logic_uScript_SetTankInvulnerable_tank_213 = local_NPCTech_Tank;
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_213.In(logic_uScript_SetTankInvulnerable_invulnerable_213, logic_uScript_SetTankInvulnerable_tank_213);
		if (logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_213.Out)
		{
			Relay_In_225();
		}
	}

	private void Relay_In_214()
	{
		logic_uScript_SetCustomRadarTeamID_tech_214 = local_NPCTech_Tank;
		logic_uScript_SetCustomRadarTeamID_uScript_SetCustomRadarTeamID_214.In(logic_uScript_SetCustomRadarTeamID_tech_214, logic_uScript_SetCustomRadarTeamID_radarTeamID_214);
		if (logic_uScript_SetCustomRadarTeamID_uScript_SetCustomRadarTeamID_214.Out)
		{
			Relay_In_271();
		}
	}

	private void Relay_In_216()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_216.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_216.Out)
		{
			Relay_In_209();
		}
	}

	private void Relay_In_218()
	{
		logic_uScript_LockTechInteraction_tech_218 = local_NPCTech_Tank;
		logic_uScript_LockTechInteraction_uScript_LockTechInteraction_218.In(logic_uScript_LockTechInteraction_tech_218, logic_uScript_LockTechInteraction_excludedBlocks_218, logic_uScript_LockTechInteraction_excludedUniqueBlocks_218);
		if (logic_uScript_LockTechInteraction_uScript_LockTechInteraction_218.Out)
		{
			Relay_In_214();
		}
	}

	private void Relay_In_224()
	{
		logic_uScript_SetTankHideBlockLimit_tech_224 = local_CraftingBaseTech_Tank;
		logic_uScript_SetTankHideBlockLimit_uScript_SetTankHideBlockLimit_224.In(logic_uScript_SetTankHideBlockLimit_hidden_224, logic_uScript_SetTankHideBlockLimit_tech_224);
		if (logic_uScript_SetTankHideBlockLimit_uScript_SetTankHideBlockLimit_224.Out)
		{
			Relay_In_198();
		}
	}

	private void Relay_In_225()
	{
		logic_uScript_LockTech_tech_225 = local_NPCTech_Tank;
		logic_uScript_LockTech_uScript_LockTech_225.In(logic_uScript_LockTech_tech_225, logic_uScript_LockTech_lockType_225);
		if (logic_uScript_LockTech_uScript_LockTech_225.Out)
		{
			Relay_In_218();
		}
	}

	private void Relay_In_232()
	{
		logic_uScript_FlyTechUpAndAway_tech_232 = local_NPCTech_Tank;
		logic_uScript_FlyTechUpAndAway_aiTree_232 = NPCFlyAwayAI;
		logic_uScript_FlyTechUpAndAway_removalParticles_232 = NPCDespawnParticleEffect;
		logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_232.In(logic_uScript_FlyTechUpAndAway_tech_232, logic_uScript_FlyTechUpAndAway_maxLifetime_232, logic_uScript_FlyTechUpAndAway_targetHeight_232, logic_uScript_FlyTechUpAndAway_aiTree_232, logic_uScript_FlyTechUpAndAway_removalParticles_232);
		if (logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_232.Out)
		{
			Relay_Succeed_237();
		}
	}

	private void Relay_In_234()
	{
		logic_uScript_LockTech_tech_234 = local_CraftingBaseTech_Tank;
		logic_uScript_LockTech_uScript_LockTech_234.In(logic_uScript_LockTech_tech_234, logic_uScript_LockTech_lockType_234);
		if (logic_uScript_LockTech_uScript_LockTech_234.Out)
		{
			Relay_In_239();
		}
	}

	private void Relay_AtIndex_235()
	{
		int num = 0;
		Array array = local_229_TankArray;
		if (logic_uScript_AccessListTech_techList_235.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_235, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_235, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_235.AtIndex(ref logic_uScript_AccessListTech_techList_235, logic_uScript_AccessListTech_index_235, out logic_uScript_AccessListTech_value_235);
		local_229_TankArray = logic_uScript_AccessListTech_techList_235;
		local_NPCTech_Tank = logic_uScript_AccessListTech_value_235;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_235.Out)
		{
			Relay_In_213();
		}
	}

	private void Relay_Succeed_237()
	{
		logic_uScript_FinishEncounter_owner_237 = owner_Connection_206;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_237.Succeed(logic_uScript_FinishEncounter_owner_237);
	}

	private void Relay_Fail_237()
	{
		logic_uScript_FinishEncounter_owner_237 = owner_Connection_206;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_237.Fail(logic_uScript_FinishEncounter_owner_237);
	}

	private void Relay_In_239()
	{
		logic_uScript_SetTankInvulnerable_tank_239 = local_CraftingBaseTech_Tank;
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_239.In(logic_uScript_SetTankInvulnerable_invulnerable_239, logic_uScript_SetTankInvulnerable_tank_239);
		if (logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_239.Out)
		{
			Relay_In_242();
		}
	}

	private void Relay_In_240()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_240.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_240.Out)
		{
			Relay_In_268();
		}
	}

	private void Relay_In_241()
	{
		logic_uScriptCon_CompareBool_Bool_241 = local_Initialize_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_241.In(logic_uScriptCon_CompareBool_Bool_241);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_241.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_241.False;
		if (num)
		{
			Relay_In_210();
		}
		if (flag)
		{
			Relay_True_194();
		}
	}

	private void Relay_In_242()
	{
		logic_uScript_SetCustomRadarTeamID_tech_242 = local_CraftingBaseTech_Tank;
		logic_uScript_SetCustomRadarTeamID_uScript_SetCustomRadarTeamID_242.In(logic_uScript_SetCustomRadarTeamID_tech_242, logic_uScript_SetCustomRadarTeamID_radarTeamID_242);
		if (logic_uScript_SetCustomRadarTeamID_uScript_SetCustomRadarTeamID_242.Out)
		{
			Relay_In_224();
		}
	}

	private void Relay_Out_255()
	{
	}

	private void Relay_In_255()
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_255 = local_Stage_System_Int32;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_255.In(ref logic_SubGraph_CompleteObjectiveStage_objectiveStage_255, logic_SubGraph_CompleteObjectiveStage_isFinalObjective_255);
	}

	private void Relay_In_256()
	{
		logic_uScript_AddMessage_messageData_256 = msg05CraftAmountOfBlocksNeeded;
		logic_uScript_AddMessage_speaker_256 = messageSpeaker;
		logic_uScript_AddMessage_Return_256 = logic_uScript_AddMessage_uScript_AddMessage_256.In(logic_uScript_AddMessage_messageData_256, logic_uScript_AddMessage_speaker_256);
		if (logic_uScript_AddMessage_uScript_AddMessage_256.Out)
		{
			Relay_In_78();
		}
	}

	private void Relay_In_264()
	{
		logic_uScript_CraftingUIHighlightRepeatButton_uScript_CraftingUIHighlightRepeatButton_264.In(logic_uScript_CraftingUIHighlightRepeatButton_requiredToggleState_264, logic_uScript_CraftingUIHighlightRepeatButton_targetMenuType_264);
		if (logic_uScript_CraftingUIHighlightRepeatButton_uScript_CraftingUIHighlightRepeatButton_264.ToggledToRequiredState)
		{
			Relay_In_265();
		}
	}

	private void Relay_In_265()
	{
		logic_uScript_CraftingUIHighlightCraftButton_uScript_CraftingUIHighlightCraftButton_265.In(logic_uScript_CraftingUIHighlightCraftButton_targetMenuType_265);
		if (logic_uScript_CraftingUIHighlightCraftButton_uScript_CraftingUIHighlightCraftButton_265.Selected)
		{
			Relay_EnableAutoCloseUI_91();
		}
	}

	private void Relay_In_266()
	{
		logic_uScript_LockTechInteraction_tech_266 = local_CraftingBaseTech_Tank;
		logic_uScript_LockTechInteraction_uScript_LockTechInteraction_266.In(logic_uScript_LockTechInteraction_tech_266, logic_uScript_LockTechInteraction_excludedBlocks_266, logic_uScript_LockTechInteraction_excludedUniqueBlocks_266);
		if (logic_uScript_LockTechInteraction_uScript_LockTechInteraction_266.Out)
		{
			Relay_In_87();
		}
	}

	private void Relay_In_268()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_268.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_268.Out)
		{
			Relay_In_269();
		}
	}

	private void Relay_In_269()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_269.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_269.Out)
		{
			Relay_In_148();
		}
	}

	private void Relay_In_270()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_270.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_270.Out)
		{
			Relay_In_148();
		}
	}

	private void Relay_In_271()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_271.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_271.Out)
		{
			Relay_In_270();
		}
	}

	private void Relay_SetCount_274()
	{
		logic_uScript_SetQuestObjectiveCount_owner_274 = owner_Connection_272;
		logic_uScript_SetQuestObjectiveCount_objectiveId_274 = local_Stage_System_Int32;
		logic_uScript_SetQuestObjectiveCount_currentCount_274 = local_CurrentAmountType_System_Int32;
		logic_uScript_SetQuestObjectiveCount_uScript_SetQuestObjectiveCount_274.SetCount(logic_uScript_SetQuestObjectiveCount_owner_274, logic_uScript_SetQuestObjectiveCount_objectiveId_274, logic_uScript_SetQuestObjectiveCount_currentCount_274);
	}

	private void Relay_In_276()
	{
		logic_uScript_CompareBlock_A_276 = local_CrafterFromEvent_TankBlock;
		logic_uScript_CompareBlock_B_276 = local_FusionMachineBlock_TankBlock;
		logic_uScript_CompareBlock_uScript_CompareBlock_276.In(logic_uScript_CompareBlock_A_276, logic_uScript_CompareBlock_B_276);
		if (logic_uScript_CompareBlock_uScript_CompareBlock_276.EqualTo)
		{
			Relay_In_153();
		}
	}

	private void Relay_In_279()
	{
		logic_uScript_LockTechInteraction_tech_279 = local_CraftingBaseTech_Tank;
		logic_uScript_LockTechInteraction_uScript_LockTechInteraction_279.In(logic_uScript_LockTechInteraction_tech_279, logic_uScript_LockTechInteraction_excludedBlocks_279, logic_uScript_LockTechInteraction_excludedUniqueBlocks_279);
		if (logic_uScript_LockTechInteraction_uScript_LockTechInteraction_279.Out)
		{
			Relay_In_144();
		}
	}

	private void Relay_In_281()
	{
		logic_uScript_LockTechInteraction_tech_281 = local_CraftingBaseTech_Tank;
		logic_uScript_LockTechInteraction_uScript_LockTechInteraction_281.In(logic_uScript_LockTechInteraction_tech_281, logic_uScript_LockTechInteraction_excludedBlocks_281, logic_uScript_LockTechInteraction_excludedUniqueBlocks_281);
		if (logic_uScript_LockTechInteraction_uScript_LockTechInteraction_281.Out)
		{
			Relay_In_168();
		}
	}

	private void Relay_In_283()
	{
		logic_uScriptCon_CompareInt_A_283 = local_Stage_System_Int32;
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_283.In(logic_uScriptCon_CompareInt_A_283, logic_uScriptCon_CompareInt_B_283);
		if (logic_uScriptCon_CompareInt_uScriptCon_CompareInt_283.EqualTo)
		{
			Relay_SetCount_274();
		}
	}

	private void Relay_In_285()
	{
		logic_uScript_SetBlock_Value_285 = local_lastCraftedBlock_TankBlock;
		logic_uScript_SetBlock_uScript_SetBlock_285.In(logic_uScript_SetBlock_Value_285, out logic_uScript_SetBlock_TargetGameObject_285);
		local_craftedCacheBlock1_TankBlock = logic_uScript_SetBlock_TargetGameObject_285;
	}

	private void Relay_OutNoBlock_289()
	{
	}

	private void Relay_Out_289()
	{
		Relay_In_147();
	}

	private void Relay_In_289()
	{
		logic_SubGraph_RemoveBlockAfterDelay_BlockToRemove_289 = local_craftedCacheBlock1_TankBlock;
		logic_SubGraph_RemoveBlockAfterDelay_TimeToWait_289 = ParticleTimer;
		logic_SubGraph_RemoveBlockAfterDelay_DespawnParticlesTransform_289 = NPCDespawnParticleEffect;
		logic_SubGraph_RemoveBlockAfterDelay_SubGraph_RemoveBlockAfterDelay_289.In(logic_SubGraph_RemoveBlockAfterDelay_BlockToRemove_289, logic_SubGraph_RemoveBlockAfterDelay_TimeToWait_289, logic_SubGraph_RemoveBlockAfterDelay_DespawnParticlesTransform_289);
	}

	private void Relay_In_293()
	{
		logic_uScript_CompareBlock_A_293 = local_craftedCacheBlock1_TankBlock;
		logic_uScript_CompareBlock_uScript_CompareBlock_293.In(logic_uScript_CompareBlock_A_293, logic_uScript_CompareBlock_B_293);
		bool equalTo = logic_uScript_CompareBlock_uScript_CompareBlock_293.EqualTo;
		bool notEqualTo = logic_uScript_CompareBlock_uScript_CompareBlock_293.NotEqualTo;
		if (equalTo)
		{
			Relay_In_285();
		}
		if (notEqualTo)
		{
			Relay_In_299();
		}
	}

	private void Relay_In_298()
	{
		logic_uScript_SetBlock_Value_298 = local_lastCraftedBlock_TankBlock;
		logic_uScript_SetBlock_uScript_SetBlock_298.In(logic_uScript_SetBlock_Value_298, out logic_uScript_SetBlock_TargetGameObject_298);
		local_craftedCacheBlock2_TankBlock = logic_uScript_SetBlock_TargetGameObject_298;
	}

	private void Relay_In_299()
	{
		logic_uScript_CompareBlock_A_299 = local_craftedCacheBlock2_TankBlock;
		logic_uScript_CompareBlock_uScript_CompareBlock_299.In(logic_uScript_CompareBlock_A_299, logic_uScript_CompareBlock_B_299);
		bool equalTo = logic_uScript_CompareBlock_uScript_CompareBlock_299.EqualTo;
		bool notEqualTo = logic_uScript_CompareBlock_uScript_CompareBlock_299.NotEqualTo;
		if (equalTo)
		{
			Relay_In_298();
		}
		if (notEqualTo)
		{
			Relay_In_303();
		}
	}

	private void Relay_In_301()
	{
		logic_uScript_SetBlock_Value_301 = local_lastCraftedBlock_TankBlock;
		logic_uScript_SetBlock_uScript_SetBlock_301.In(logic_uScript_SetBlock_Value_301, out logic_uScript_SetBlock_TargetGameObject_301);
		local_craftedCacheBlock3_TankBlock = logic_uScript_SetBlock_TargetGameObject_301;
	}

	private void Relay_In_303()
	{
		logic_uScript_CompareBlock_A_303 = local_craftedCacheBlock3_TankBlock;
		logic_uScript_CompareBlock_uScript_CompareBlock_303.In(logic_uScript_CompareBlock_A_303, logic_uScript_CompareBlock_B_303);
		bool equalTo = logic_uScript_CompareBlock_uScript_CompareBlock_303.EqualTo;
		bool notEqualTo = logic_uScript_CompareBlock_uScript_CompareBlock_303.NotEqualTo;
		if (equalTo)
		{
			Relay_In_301();
		}
		if (notEqualTo)
		{
			Relay_In_308();
		}
	}

	private void Relay_In_306()
	{
		logic_uScript_SetBlock_Value_306 = local_lastCraftedBlock_TankBlock;
		logic_uScript_SetBlock_uScript_SetBlock_306.In(logic_uScript_SetBlock_Value_306, out logic_uScript_SetBlock_TargetGameObject_306);
		local_craftedCacheBlock4_TankBlock = logic_uScript_SetBlock_TargetGameObject_306;
	}

	private void Relay_In_308()
	{
		logic_uScript_CompareBlock_A_308 = local_craftedCacheBlock4_TankBlock;
		logic_uScript_CompareBlock_uScript_CompareBlock_308.In(logic_uScript_CompareBlock_A_308, logic_uScript_CompareBlock_B_308);
		bool equalTo = logic_uScript_CompareBlock_uScript_CompareBlock_308.EqualTo;
		bool notEqualTo = logic_uScript_CompareBlock_uScript_CompareBlock_308.NotEqualTo;
		if (equalTo)
		{
			Relay_In_306();
		}
		if (notEqualTo)
		{
			Relay_In_319();
		}
	}

	private void Relay_OutNoBlock_314()
	{
	}

	private void Relay_Out_314()
	{
		Relay_In_147();
	}

	private void Relay_In_314()
	{
		logic_SubGraph_RemoveBlockAfterDelay_BlockToRemove_314 = local_craftedCacheBlock2_TankBlock;
		logic_SubGraph_RemoveBlockAfterDelay_TimeToWait_314 = ParticleTimer;
		logic_SubGraph_RemoveBlockAfterDelay_DespawnParticlesTransform_314 = NPCDespawnParticleEffect;
		logic_SubGraph_RemoveBlockAfterDelay_SubGraph_RemoveBlockAfterDelay_314.In(logic_SubGraph_RemoveBlockAfterDelay_BlockToRemove_314, logic_SubGraph_RemoveBlockAfterDelay_TimeToWait_314, logic_SubGraph_RemoveBlockAfterDelay_DespawnParticlesTransform_314);
	}

	private void Relay_OutNoBlock_318()
	{
	}

	private void Relay_Out_318()
	{
		Relay_In_147();
	}

	private void Relay_In_318()
	{
		logic_SubGraph_RemoveBlockAfterDelay_BlockToRemove_318 = local_craftedCacheBlock3_TankBlock;
		logic_SubGraph_RemoveBlockAfterDelay_TimeToWait_318 = ParticleTimer;
		logic_SubGraph_RemoveBlockAfterDelay_DespawnParticlesTransform_318 = NPCDespawnParticleEffect;
		logic_SubGraph_RemoveBlockAfterDelay_SubGraph_RemoveBlockAfterDelay_318.In(logic_SubGraph_RemoveBlockAfterDelay_BlockToRemove_318, logic_SubGraph_RemoveBlockAfterDelay_TimeToWait_318, logic_SubGraph_RemoveBlockAfterDelay_DespawnParticlesTransform_318);
	}

	private void Relay_In_319()
	{
		logic_uScriptAct_PrintList_uScriptAct_PrintList_319.In(logic_uScriptAct_PrintList_Strings_319);
	}

	private void Relay_OutNoBlock_320()
	{
	}

	private void Relay_Out_320()
	{
		Relay_In_147();
	}

	private void Relay_In_320()
	{
		logic_SubGraph_RemoveBlockAfterDelay_BlockToRemove_320 = local_craftedCacheBlock4_TankBlock;
		logic_SubGraph_RemoveBlockAfterDelay_TimeToWait_320 = ParticleTimer;
		logic_SubGraph_RemoveBlockAfterDelay_DespawnParticlesTransform_320 = NPCDespawnParticleEffect;
		logic_SubGraph_RemoveBlockAfterDelay_SubGraph_RemoveBlockAfterDelay_320.In(logic_SubGraph_RemoveBlockAfterDelay_BlockToRemove_320, logic_SubGraph_RemoveBlockAfterDelay_TimeToWait_320, logic_SubGraph_RemoveBlockAfterDelay_DespawnParticlesTransform_320);
	}

	private void Relay_In_326()
	{
		logic_uScriptCon_CompareInt_A_326 = local_Stage_System_Int32;
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_326.In(logic_uScriptCon_CompareInt_A_326, logic_uScriptCon_CompareInt_B_326);
		if (logic_uScriptCon_CompareInt_uScriptCon_CompareInt_326.EqualTo)
		{
			Relay_In_157();
		}
	}

	private void Relay_In_328()
	{
		logic_uScript_DirectEnemiesOutOfEncounter_ownerNode_328 = owner_Connection_329;
		logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_328.In(logic_uScript_DirectEnemiesOutOfEncounter_ownerNode_328);
		if (logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_328.Out)
		{
			Relay_In_0();
		}
	}

	private void Relay_In_330()
	{
		logic_uScriptCon_CompareBool_Bool_330 = local_CraftingInProgress_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_330.In(logic_uScriptCon_CompareBool_Bool_330);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_330.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_330.False;
		if (num)
		{
			Relay_In_266();
		}
		if (flag)
		{
			Relay_SetCount_181();
		}
	}

	private void Relay_Save_Out_699()
	{
		Relay_Save_178();
	}

	private void Relay_Load_Out_699()
	{
		Relay_Load_178();
	}

	private void Relay_Restart_Out_699()
	{
		Relay_Restart_178();
	}

	private void Relay_Save_699()
	{
		logic_SubGraph_SaveLoadInt_integer_699 = local_Stage3_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_699 = local_Stage3_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_699.Save(logic_SubGraph_SaveLoadInt_restartValue_699, ref logic_SubGraph_SaveLoadInt_integer_699, logic_SubGraph_SaveLoadInt_intAsVariable_699, logic_SubGraph_SaveLoadInt_uniqueID_699);
	}

	private void Relay_Load_699()
	{
		logic_SubGraph_SaveLoadInt_integer_699 = local_Stage3_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_699 = local_Stage3_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_699.Load(logic_SubGraph_SaveLoadInt_restartValue_699, ref logic_SubGraph_SaveLoadInt_integer_699, logic_SubGraph_SaveLoadInt_intAsVariable_699, logic_SubGraph_SaveLoadInt_uniqueID_699);
	}

	private void Relay_Restart_699()
	{
		logic_SubGraph_SaveLoadInt_integer_699 = local_Stage3_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_699 = local_Stage3_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_699.Restart(logic_SubGraph_SaveLoadInt_restartValue_699, ref logic_SubGraph_SaveLoadInt_integer_699, logic_SubGraph_SaveLoadInt_intAsVariable_699, logic_SubGraph_SaveLoadInt_uniqueID_699);
	}
}
