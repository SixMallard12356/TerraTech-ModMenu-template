using System;
using BehaviorDesigner.Runtime;
using UnityEngine;

[Serializable]
[FriendlyName("", "")]
[NodePath("Graphs")]
public class Mission_SetPiece_RR_Circuits_TutorialInput_Mission : uScriptLogic
{
	private GameObject parentGameObject;

	private uScript_GUI thisScriptsOnGuiListener;

	private bool m_RegisteredForEvents;

	public SpawnBlockData[] BeamSensorBlockSpawnData = new SpawnBlockData[0];

	public SpawnBlockData[] BlockSpawnData = new SpawnBlockData[0];

	public BlockTypes blockTypeBeamSensor;

	public BlockTypes blockTypeLight;

	public BlockTypes blockTypeToggle;

	public BlockTypes blockTypeWire;

	public SpawnTechData[] CircuirtsbaseSpawnData = new SpawnTechData[0];

	[Multiline(3)]
	public string CircuitsBasePosition = "";

	public float clearSceneryRadius;

	public TankPreset completedBasePreset;

	public float distBaseFound;

	public GhostBlockSpawnData[] GhostBlockBeamSensor = new GhostBlockSpawnData[0];

	public GhostBlockSpawnData[] GhostBlockLight = new GhostBlockSpawnData[0];

	public GhostBlockSpawnData[] ghostBlockWire01 = new GhostBlockSpawnData[0];

	public GhostBlockSpawnData[] ghostBlockWire02 = new GhostBlockSpawnData[0];

	public GhostBlockSpawnData[] ghostBlockWire03 = new GhostBlockSpawnData[0];

	private TankBlock[] local_227_TankBlockArray = new TankBlock[0];

	private TankBlock[] local_240_TankBlockArray = new TankBlock[0];

	private TankBlock[] local_329_TankBlockArray = new TankBlock[0];

	private string local_394_System_String = "";

	private string local_401_System_String = ", IsDark:";

	private string local_402_System_String = "isNearBase: ";

	private string local_403_System_String = "";

	private string local_404_System_String = "";

	private string local_405_System_String = "";

	private string local_406_System_String = "";

	private string local_407_System_String = "";

	private string local_411_System_String = ", StartingHour:";

	private string local_412_System_String = ", Stage:";

	private string local_413_System_String = "";

	private TankBlock local_BeamSensorBlock_TankBlock;

	private bool local_BeamSensorIsAttached_System_Boolean;

	private bool local_BeamSensorIsOn_System_Boolean;

	private bool local_BeamSensorIsSpawned_System_Boolean;

	private bool local_CanInteractWithToggle_System_Boolean;

	private Tank local_CircuitBaseTech_Tank;

	private bool local_DraggingWire_System_Boolean;

	private TankBlock local_GhostBlockWire1_TankBlock;

	private bool local_GhostBlockWire1Spawned_System_Boolean;

	private TankBlock local_GhostBlockWire2_TankBlock;

	private bool local_GhostBlockWire2Spawned_System_Boolean;

	private TankBlock local_GhostBlockWire3_TankBlock;

	private bool local_GhostBlockWire3Spawned_System_Boolean;

	private TankBlock local_Light2Block_TankBlock;

	private int local_Light2BlockSignalValue_System_Int32;

	private TankBlock local_LightBlock_TankBlock;

	private bool local_LightIsAttached_System_Boolean;

	private bool local_msgBaseFoundShown_System_Boolean;

	private bool local_msgBeamSensorAttachedIsOnShown_System_Boolean;

	private bool local_msgBeamSensorSpawnedShown_System_Boolean;

	private bool local_msgCompleteIsOnShown_System_Boolean;

	private bool local_msgIntroShown_System_Boolean;

	private bool local_msgLightAttachedShown_System_Boolean;

	private bool local_msgToggleIntroShown_System_Boolean;

	private bool local_msgToggleIsOnShown_System_Boolean;

	private bool local_msgToggleOffShown_System_Boolean;

	private bool local_msgWiresAttachedShown_System_Boolean;

	private bool local_NearBase_System_Boolean;

	private bool local_NonEncounterToggleUsed_System_Boolean;

	private Tank local_NPCTech_Tank;

	private int local_NumWiresAttached_System_Int32;

	private int local_Stage_System_Int32 = 1;

	private int local_StartingHour_System_Int32;

	private int local_SubStage2_System_Int32 = 1;

	private int local_SubStage3_System_Int32 = 1;

	private bool local_TechsInit_System_Boolean;

	private TankBlock local_ToggleBlock_TankBlock;

	private int local_ToggleInteractions_System_Int32;

	private bool local_ToggleIsOn_System_Boolean;

	private int local_ToggleSignalValue_System_Int32;

	private TankBlock local_Wire1Block_TankBlock;

	private TankBlock local_Wire2Block_TankBlock;

	private TankBlock local_Wire3Block_TankBlock;

	public uScript_AddMessage.MessageSpeaker messageSpeaker;

	[Multiline(3)]
	public string messageTag = "";

	public int MissionHour = 8;

	[Multiline(3)]
	public string MissionRangeTrig = "";

	public uScript_AddMessage.MessageData msg01Intro;

	public uScript_AddMessage.MessageData msg02BaseFound;

	public uScript_AddMessage.MessageData msg03ToggleIntro;

	public uScript_AddMessage.MessageData msg04PickupWire;

	public uScript_AddMessage.MessageData msg05AttachedWires;

	public uScript_AddMessage.MessageData msg06PickupLight;

	public uScript_AddMessage.MessageData msg07AttachedLight;

	public uScript_AddMessage.MessageData msg08SetToggleActive;

	public uScript_AddMessage.MessageData msg08SetToggleActive_Pad;

	public uScript_AddMessage.MessageData msg09ToggleIsOn;

	public uScript_AddMessage.MessageData msg10SetToggleInactive;

	public uScript_AddMessage.MessageData msg10SetToggleInactive_Pad;

	public uScript_AddMessage.MessageData msg11ToggleIsOff;

	public uScript_AddMessage.MessageData msg12BeamSensorSpawned;

	public uScript_AddMessage.MessageData msg13PickupBeamSensor;

	public uScript_AddMessage.MessageData msg13PickupBeamSensor_Pad;

	public uScript_AddMessage.MessageData msg14AttachedBeamSensor;

	public uScript_AddMessage.MessageData msg15SetBeamSensorActive;

	public uScript_AddMessage.MessageData msg17BeamSensorOn;

	public uScript_AddMessage.MessageData msg18Complete;

	public uScript_AddMessage.MessageData msgBlockOutsideArea;

	public uScript_AddMessage.MessageData msgLeavingMissionArea;

	public Transform NPCDespawnParticleEffect;

	public ExternalBehaviorTree NPCFlyAwayAI;

	public SpawnTechData[] NPCSpawnData = new SpawnTechData[0];

	private GameObject owner_Connection_1;

	private GameObject owner_Connection_3;

	private GameObject owner_Connection_35;

	private GameObject owner_Connection_67;

	private GameObject owner_Connection_72;

	private GameObject owner_Connection_73;

	private GameObject owner_Connection_297;

	private GameObject owner_Connection_301;

	private GameObject owner_Connection_320;

	private GameObject owner_Connection_429;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_5 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_5;

	private bool logic_uScriptCon_CompareBool_True_5 = true;

	private bool logic_uScriptCon_CompareBool_False_5 = true;

	private uScript_PausePopulation logic_uScript_PausePopulation_uScript_PausePopulation_7 = new uScript_PausePopulation();

	private bool logic_uScript_PausePopulation_Out_7 = true;

	private uScript_SetEncounterTarget logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_8 = new uScript_SetEncounterTarget();

	private GameObject logic_uScript_SetEncounterTarget_owner_8;

	private object logic_uScript_SetEncounterTarget_visibleObject_8 = "";

	private bool logic_uScript_SetEncounterTarget_Out_8 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_10 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_10;

	private bool logic_uScriptCon_CompareBool_True_10 = true;

	private bool logic_uScriptCon_CompareBool_False_10 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_13 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_13 = true;

	private uScript_PausePopulation logic_uScript_PausePopulation_uScript_PausePopulation_14 = new uScript_PausePopulation();

	private bool logic_uScript_PausePopulation_Out_14 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_16 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_16;

	private bool logic_uScriptAct_SetBool_Out_16 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_16 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_16 = true;

	private uScript_LockTechInteraction logic_uScript_LockTechInteraction_uScript_LockTechInteraction_17 = new uScript_LockTechInteraction();

	private Tank logic_uScript_LockTechInteraction_tech_17;

	private BlockTypes[] logic_uScript_LockTechInteraction_excludedBlocks_17 = new BlockTypes[0];

	private TankBlock[] logic_uScript_LockTechInteraction_excludedUniqueBlocks_17 = new TankBlock[0];

	private bool logic_uScript_LockTechInteraction_Out_17 = true;

	private uScript_IsPlayerInRangeOfTech logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_19 = new uScript_IsPlayerInRangeOfTech();

	private Tank logic_uScript_IsPlayerInRangeOfTech_tech_19;

	private float logic_uScript_IsPlayerInRangeOfTech_range_19;

	private Tank[] logic_uScript_IsPlayerInRangeOfTech_techs_19 = new Tank[0];

	private bool logic_uScript_IsPlayerInRangeOfTech_Out_19 = true;

	private bool logic_uScript_IsPlayerInRangeOfTech_InRange_19 = true;

	private bool logic_uScript_IsPlayerInRangeOfTech_OutOfRange_19 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_20 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_20 = "";

	private bool logic_uScript_EnableGlow_enable_20;

	private bool logic_uScript_EnableGlow_Out_20 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_21 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_21;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_21;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_21;

	private bool logic_uScript_AddMessage_Out_21 = true;

	private bool logic_uScript_AddMessage_Shown_21 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_22 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_22;

	private bool logic_uScriptAct_SetBool_Out_22 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_22 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_22 = true;

	private uScript_HideArrow logic_uScript_HideArrow_uScript_HideArrow_23 = new uScript_HideArrow();

	private bool logic_uScript_HideArrow_Out_23 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_24 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_24 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_24 = true;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_24 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_25 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_25;

	private bool logic_uScriptCon_CompareBool_True_25 = true;

	private bool logic_uScriptCon_CompareBool_False_25 = true;

	private uScript_IsPlayerInRangeOfTech logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_27 = new uScript_IsPlayerInRangeOfTech();

	private Tank logic_uScript_IsPlayerInRangeOfTech_tech_27;

	private float logic_uScript_IsPlayerInRangeOfTech_range_27 = 100f;

	private Tank[] logic_uScript_IsPlayerInRangeOfTech_techs_27 = new Tank[0];

	private bool logic_uScript_IsPlayerInRangeOfTech_Out_27 = true;

	private bool logic_uScript_IsPlayerInRangeOfTech_InRange_27 = true;

	private bool logic_uScript_IsPlayerInRangeOfTech_OutOfRange_27 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_28 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_28;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_28;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_28;

	private bool logic_uScript_AddMessage_Out_28 = true;

	private bool logic_uScript_AddMessage_Shown_28 = true;

	private uScriptCon_ManualSwitch logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_42 = new uScriptCon_ManualSwitch();

	private int logic_uScriptCon_ManualSwitch_CurrentOutput_42;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_45 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_45;

	private bool logic_uScriptAct_SetBool_Out_45 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_45 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_45 = true;

	private SubGraph_LoadObjectiveStates logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_47 = new SubGraph_LoadObjectiveStates();

	private int logic_SubGraph_LoadObjectiveStates_currentObjective_47;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_50 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_50;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_50 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_50 = "BeamSensorIsAttached";

	private SubGraph_SaveLoadInt logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_52 = new SubGraph_SaveLoadInt();

	private int logic_SubGraph_SaveLoadInt_restartValue_52;

	private int logic_SubGraph_SaveLoadInt_integer_52;

	private object logic_SubGraph_SaveLoadInt_intAsVariable_52 = "";

	private string logic_SubGraph_SaveLoadInt_uniqueID_52 = "WiresAttached";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_56 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_56;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_56 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_56 = "BeamSensorIsSpawned";

	private SubGraph_Crafting_Tutorial_AttachBlockToBase logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_SubGraph_Crafting_Tutorial_AttachBlockToBase_58 = new SubGraph_Crafting_Tutorial_AttachBlockToBase();

	private TankBlock logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_Block_58;

	private Tank logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_CraftingBaseTech_58;

	private GhostBlockSpawnData[] logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_ghostBlockData_58 = new GhostBlockSpawnData[0];

	private BlockTypes logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_BlockType_58;

	private Vector3 logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_BlockPosition_58 = new Vector3(0f, 1f, -8f);

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_59 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_59;

	private bool logic_uScriptCon_CompareBool_True_59 = true;

	private bool logic_uScriptCon_CompareBool_False_59 = true;

	private uScript_HideArrow logic_uScript_HideArrow_uScript_HideArrow_60 = new uScript_HideArrow();

	private bool logic_uScript_HideArrow_Out_60 = true;

	private uScript_RemoveAllGhostBlocksOnTech logic_uScript_RemoveAllGhostBlocksOnTech_uScript_RemoveAllGhostBlocksOnTech_62 = new uScript_RemoveAllGhostBlocksOnTech();

	private Tank logic_uScript_RemoveAllGhostBlocksOnTech_targetTech_62;

	private bool logic_uScript_RemoveAllGhostBlocksOnTech_Out_62 = true;

	private uScript_GetAndCheckBlocks logic_uScript_GetAndCheckBlocks_uScript_GetAndCheckBlocks_63 = new uScript_GetAndCheckBlocks();

	private SpawnBlockData[] logic_uScript_GetAndCheckBlocks_blockData_63 = new SpawnBlockData[0];

	private GameObject logic_uScript_GetAndCheckBlocks_ownerNode_63;

	private TankBlock[] logic_uScript_GetAndCheckBlocks_blocks_63 = new TankBlock[0];

	private bool logic_uScript_GetAndCheckBlocks_AllAlive_63 = true;

	private bool logic_uScript_GetAndCheckBlocks_SomeAlive_63 = true;

	private bool logic_uScript_GetAndCheckBlocks_AllDead_63 = true;

	private bool logic_uScript_GetAndCheckBlocks_WaitingToSpawn_63 = true;

	private uScript_SpawnBlocksFromData logic_uScript_SpawnBlocksFromData_uScript_SpawnBlocksFromData_66 = new uScript_SpawnBlocksFromData();

	private SpawnBlockData[] logic_uScript_SpawnBlocksFromData_blockData_66 = new SpawnBlockData[0];

	private GameObject logic_uScript_SpawnBlocksFromData_ownerNode_66;

	private bool logic_uScript_SpawnBlocksFromData_Out_66 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_69 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_69;

	private bool logic_uScriptAct_SetBool_Out_69 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_69 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_69 = true;

	private uScript_PlayerInRangeOfCurrentEncounter logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_74 = new uScript_PlayerInRangeOfCurrentEncounter();

	private GameObject logic_uScript_PlayerInRangeOfCurrentEncounter_ownerNode_74;

	private bool logic_uScript_PlayerInRangeOfCurrentEncounter_Out_74 = true;

	private bool logic_uScript_PlayerInRangeOfCurrentEncounter_True_74 = true;

	private bool logic_uScript_PlayerInRangeOfCurrentEncounter_False_74 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_75 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_75;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_75 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_75 = "CanInteractWithToggle";

	private uScript_LockTechInteraction logic_uScript_LockTechInteraction_uScript_LockTechInteraction_78 = new uScript_LockTechInteraction();

	private Tank logic_uScript_LockTechInteraction_tech_78;

	private BlockTypes[] logic_uScript_LockTechInteraction_excludedBlocks_78 = new BlockTypes[0];

	private TankBlock[] logic_uScript_LockTechInteraction_excludedUniqueBlocks_78 = new TankBlock[0];

	private bool logic_uScript_LockTechInteraction_Out_78 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_80 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_80;

	private bool logic_uScriptAct_SetBool_Out_80 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_80 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_80 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_82 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_82;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_82 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_82 = "ToggleIsOn";

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_84 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_84;

	private bool logic_uScriptAct_SetBool_Out_84 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_84 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_84 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_85 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_85;

	private bool logic_uScriptCon_CompareBool_True_85 = true;

	private bool logic_uScriptCon_CompareBool_False_85 = true;

	private uScriptCon_CompareInt logic_uScriptCon_CompareInt_uScriptCon_CompareInt_86 = new uScriptCon_CompareInt();

	private int logic_uScriptCon_CompareInt_A_86;

	private int logic_uScriptCon_CompareInt_B_86;

	private bool logic_uScriptCon_CompareInt_GreaterThan_86 = true;

	private bool logic_uScriptCon_CompareInt_GreaterThanOrEqualTo_86 = true;

	private bool logic_uScriptCon_CompareInt_EqualTo_86 = true;

	private bool logic_uScriptCon_CompareInt_NotEqualTo_86 = true;

	private bool logic_uScriptCon_CompareInt_LessThanOrEqualTo_86 = true;

	private bool logic_uScriptCon_CompareInt_LessThan_86 = true;

	private uScript_GetCircuitChargeInfo logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_89 = new uScript_GetCircuitChargeInfo();

	private TankBlock logic_uScript_GetCircuitChargeInfo_block_89;

	private Tank logic_uScript_GetCircuitChargeInfo_tech_89;

	private BlockTypes logic_uScript_GetCircuitChargeInfo_blockType_89;

	private int logic_uScript_GetCircuitChargeInfo_Return_89;

	private bool logic_uScript_GetCircuitChargeInfo_Out_89 = true;

	private bool logic_uScript_GetCircuitChargeInfo_HasValidBlock_89 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_91 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_91;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_91 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_91 = "BeamSensorIsOn";

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_92 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_92 = "";

	private bool logic_uScript_EnableGlow_enable_92;

	private bool logic_uScript_EnableGlow_Out_92 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_95 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_95 = "";

	private bool logic_uScript_EnableGlow_enable_95;

	private bool logic_uScript_EnableGlow_Out_95 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_97 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_97;

	private bool logic_uScriptAct_SetBool_Out_97 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_97 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_97 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_100 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_100;

	private bool logic_uScriptAct_SetBool_Out_100 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_100 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_100 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_102 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_102 = "";

	private bool logic_uScript_EnableGlow_enable_102;

	private bool logic_uScript_EnableGlow_Out_102 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_103 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_103;

	private bool logic_uScriptAct_SetBool_Out_103 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_103 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_103 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_105 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_105;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_105 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_105 = "LightIsAttached";

	private SubGraph_CompleteObjectiveStage logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_107 = new SubGraph_CompleteObjectiveStage();

	private int logic_SubGraph_CompleteObjectiveStage_objectiveStage_107;

	private bool logic_SubGraph_CompleteObjectiveStage_isFinalObjective_107;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_109 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_109;

	private bool logic_uScriptCon_CompareBool_True_109 = true;

	private bool logic_uScriptCon_CompareBool_False_109 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_111 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_111;

	private bool logic_uScriptAct_SetBool_Out_111 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_111 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_111 = true;

	private SubGraph_SaveLoadInt logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_112 = new SubGraph_SaveLoadInt();

	private int logic_SubGraph_SaveLoadInt_restartValue_112 = 1;

	private int logic_SubGraph_SaveLoadInt_integer_112;

	private object logic_SubGraph_SaveLoadInt_intAsVariable_112 = "";

	private string logic_SubGraph_SaveLoadInt_uniqueID_112 = "Stage";

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_115 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_115 = "";

	private bool logic_uScript_EnableGlow_enable_115;

	private bool logic_uScript_EnableGlow_Out_115 = true;

	private uScript_RemoveAllGhostBlocksOnTech logic_uScript_RemoveAllGhostBlocksOnTech_uScript_RemoveAllGhostBlocksOnTech_118 = new uScript_RemoveAllGhostBlocksOnTech();

	private Tank logic_uScript_RemoveAllGhostBlocksOnTech_targetTech_118;

	private bool logic_uScript_RemoveAllGhostBlocksOnTech_Out_118 = true;

	private SubGraph_CompleteObjectiveStage logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_123 = new SubGraph_CompleteObjectiveStage();

	private int logic_SubGraph_CompleteObjectiveStage_objectiveStage_123;

	private bool logic_SubGraph_CompleteObjectiveStage_isFinalObjective_123;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_125 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_125;

	private bool logic_uScriptAct_SetBool_Out_125 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_125 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_125 = true;

	private uScript_HideArrow logic_uScript_HideArrow_uScript_HideArrow_126 = new uScript_HideArrow();

	private bool logic_uScript_HideArrow_Out_126 = true;

	private SubGraph_Crafting_Tutorial_AttachBlockToBase logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_SubGraph_Crafting_Tutorial_AttachBlockToBase_128 = new SubGraph_Crafting_Tutorial_AttachBlockToBase();

	private TankBlock logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_Block_128;

	private Tank logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_CraftingBaseTech_128;

	private GhostBlockSpawnData[] logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_ghostBlockData_128 = new GhostBlockSpawnData[0];

	private BlockTypes logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_BlockType_128;

	private Vector3 logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_BlockPosition_128 = new Vector3(2f, 2f, 2f);

	private uScript_PointArrowAtVisible logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_131 = new uScript_PointArrowAtVisible();

	private object logic_uScript_PointArrowAtVisible_targetObject_131 = "";

	private float logic_uScript_PointArrowAtVisible_timeToShowFor_131 = -1f;

	private Vector3 logic_uScript_PointArrowAtVisible_offset_131 = new Vector3(0f, 0f, 0f);

	private bool logic_uScript_PointArrowAtVisible_Out_131 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_133 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_133 = "";

	private bool logic_uScript_EnableGlow_enable_133 = true;

	private bool logic_uScript_EnableGlow_Out_133 = true;

	private uScript_GetCircuitChargeInfo logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_137 = new uScript_GetCircuitChargeInfo();

	private TankBlock logic_uScript_GetCircuitChargeInfo_block_137;

	private Tank logic_uScript_GetCircuitChargeInfo_tech_137;

	private BlockTypes logic_uScript_GetCircuitChargeInfo_blockType_137;

	private int logic_uScript_GetCircuitChargeInfo_Return_137;

	private bool logic_uScript_GetCircuitChargeInfo_Out_137 = true;

	private bool logic_uScript_GetCircuitChargeInfo_HasValidBlock_137 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_138 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_138;

	private bool logic_uScriptAct_SetBool_Out_138 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_138 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_138 = true;

	private uScriptCon_CompareInt logic_uScriptCon_CompareInt_uScriptCon_CompareInt_139 = new uScriptCon_CompareInt();

	private int logic_uScriptCon_CompareInt_A_139;

	private int logic_uScriptCon_CompareInt_B_139;

	private bool logic_uScriptCon_CompareInt_GreaterThan_139 = true;

	private bool logic_uScriptCon_CompareInt_GreaterThanOrEqualTo_139 = true;

	private bool logic_uScriptCon_CompareInt_EqualTo_139 = true;

	private bool logic_uScriptCon_CompareInt_NotEqualTo_139 = true;

	private bool logic_uScriptCon_CompareInt_LessThanOrEqualTo_139 = true;

	private bool logic_uScriptCon_CompareInt_LessThan_139 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_140 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_140;

	private bool logic_uScriptCon_CompareBool_True_140 = true;

	private bool logic_uScriptCon_CompareBool_False_140 = true;

	private SubGraph_CompleteObjectiveStage logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_142 = new SubGraph_CompleteObjectiveStage();

	private int logic_SubGraph_CompleteObjectiveStage_objectiveStage_142;

	private bool logic_SubGraph_CompleteObjectiveStage_isFinalObjective_142;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_143 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_143;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_143 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_143 = "msgIntroShown";

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_146 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_146 = "";

	private bool logic_uScript_EnableGlow_enable_146;

	private bool logic_uScript_EnableGlow_Out_146 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_147 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_147 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_148 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_148 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_153 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_153;

	private bool logic_uScriptCon_CompareBool_True_153 = true;

	private bool logic_uScriptCon_CompareBool_False_153 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_154 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_154 = "";

	private bool logic_uScript_EnableGlow_enable_154;

	private bool logic_uScript_EnableGlow_Out_154 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_155 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_155 = "";

	private bool logic_uScript_EnableGlow_enable_155;

	private bool logic_uScript_EnableGlow_Out_155 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_156 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_156;

	private bool logic_uScriptCon_CompareBool_True_156 = true;

	private bool logic_uScriptCon_CompareBool_False_156 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_158 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_158 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_159 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_159;

	private bool logic_uScriptCon_CompareBool_True_159 = true;

	private bool logic_uScriptCon_CompareBool_False_159 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_160 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_160;

	private bool logic_uScriptAct_SetBool_Out_160 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_160 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_160 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_162 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_162;

	private bool logic_uScriptAct_SetBool_Out_162 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_162 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_162 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_163 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_163;

	private bool logic_uScriptCon_CompareBool_True_163 = true;

	private bool logic_uScriptCon_CompareBool_False_163 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_165 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_165;

	private bool logic_uScriptAct_SetBool_Out_165 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_165 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_165 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_168 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_168;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_168;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_168;

	private bool logic_uScript_AddMessage_Out_168 = true;

	private bool logic_uScript_AddMessage_Shown_168 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_171 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_171;

	private bool logic_uScriptCon_CompareBool_True_171 = true;

	private bool logic_uScriptCon_CompareBool_False_171 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_173 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_173;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_173 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_173 = "msgBaseFoundShown";

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_175 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_175;

	private bool logic_uScriptAct_SetBool_Out_175 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_175 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_175 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_177 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_177;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_177;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_177;

	private bool logic_uScript_AddMessage_Out_177 = true;

	private bool logic_uScript_AddMessage_Shown_177 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_178 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_178;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_178;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_178;

	private bool logic_uScript_AddMessage_Out_178 = true;

	private bool logic_uScript_AddMessage_Shown_178 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_179 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_179;

	private bool logic_uScriptCon_CompareBool_True_179 = true;

	private bool logic_uScriptCon_CompareBool_False_179 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_184 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_184;

	private bool logic_uScriptCon_CompareBool_True_184 = true;

	private bool logic_uScriptCon_CompareBool_False_184 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_186 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_186;

	private bool logic_uScriptAct_SetBool_Out_186 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_186 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_186 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_188 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_188;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_188;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_188;

	private bool logic_uScript_AddMessage_Out_188 = true;

	private bool logic_uScript_AddMessage_Shown_188 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_193 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_193;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_193 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_193 = "msgWiresAttachedShown";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_194 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_194;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_194 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_194 = "msgLightAttachedShown";

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_196 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_196;

	private bool logic_uScriptAct_SetBool_Out_196 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_196 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_196 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_197 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_197;

	private bool logic_uScriptCon_CompareBool_True_197 = true;

	private bool logic_uScriptCon_CompareBool_False_197 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_201 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_201;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_201;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_201;

	private bool logic_uScript_AddMessage_Out_201 = true;

	private bool logic_uScript_AddMessage_Shown_201 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_203 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_203;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_203 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_203 = "msgToggleIsOnShown";

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_206 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_206;

	private bool logic_uScriptCon_CompareBool_True_206 = true;

	private bool logic_uScriptCon_CompareBool_False_206 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_207 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_207;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_207;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_207;

	private bool logic_uScript_AddMessage_Out_207 = true;

	private bool logic_uScript_AddMessage_Shown_207 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_208 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_208;

	private bool logic_uScriptAct_SetBool_Out_208 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_208 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_208 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_211 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_211;

	private bool logic_uScriptAct_SetBool_Out_211 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_211 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_211 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_214 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_214;

	private bool logic_uScriptCon_CompareBool_True_214 = true;

	private bool logic_uScriptCon_CompareBool_False_214 = true;

	private uScript_DoesTechHaveBlockAtPosition logic_uScript_DoesTechHaveBlockAtPosition_uScript_DoesTechHaveBlockAtPosition_215 = new uScript_DoesTechHaveBlockAtPosition();

	private Tank logic_uScript_DoesTechHaveBlockAtPosition_tech_215;

	private BlockTypes logic_uScript_DoesTechHaveBlockAtPosition_blockType_215;

	private Vector3 logic_uScript_DoesTechHaveBlockAtPosition_localPosition_215 = new Vector3(2f, 1f, 0f);

	private bool logic_uScript_DoesTechHaveBlockAtPosition_True_215 = true;

	private bool logic_uScript_DoesTechHaveBlockAtPosition_False_215 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_216 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_216 = "";

	private bool logic_uScript_EnableGlow_enable_216;

	private bool logic_uScript_EnableGlow_Out_216 = true;

	private uScript_AccessListBlock logic_uScript_AccessListBlock_uScript_AccessListBlock_217 = new uScript_AccessListBlock();

	private TankBlock[] logic_uScript_AccessListBlock_blockList_217 = new TankBlock[0];

	private int logic_uScript_AccessListBlock_index_217;

	private TankBlock logic_uScript_AccessListBlock_value_217;

	private bool logic_uScript_AccessListBlock_Out_217 = true;

	private uScript_PointArrowAtVisible logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_219 = new uScript_PointArrowAtVisible();

	private object logic_uScript_PointArrowAtVisible_targetObject_219 = "";

	private float logic_uScript_PointArrowAtVisible_timeToShowFor_219 = -1f;

	private Vector3 logic_uScript_PointArrowAtVisible_offset_219 = new Vector3(0f, 0f, 0f);

	private bool logic_uScript_PointArrowAtVisible_Out_219 = true;

	private uScript_RemoveAllGhostBlocksOnTech logic_uScript_RemoveAllGhostBlocksOnTech_uScript_RemoveAllGhostBlocksOnTech_220 = new uScript_RemoveAllGhostBlocksOnTech();

	private Tank logic_uScript_RemoveAllGhostBlocksOnTech_targetTech_220;

	private bool logic_uScript_RemoveAllGhostBlocksOnTech_Out_220 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_221 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_221 = "";

	private bool logic_uScript_EnableGlow_enable_221;

	private bool logic_uScript_EnableGlow_Out_221 = true;

	private uScript_PointArrowAtVisible logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_222 = new uScript_PointArrowAtVisible();

	private object logic_uScript_PointArrowAtVisible_targetObject_222 = "";

	private float logic_uScript_PointArrowAtVisible_timeToShowFor_222 = -1f;

	private Vector3 logic_uScript_PointArrowAtVisible_offset_222 = new Vector3(0f, 0f, 0f);

	private bool logic_uScript_PointArrowAtVisible_Out_222 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_224 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_224;

	private bool logic_uScriptCon_CompareBool_True_224 = true;

	private bool logic_uScriptCon_CompareBool_False_224 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_226 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_226 = true;

	private uScriptAct_AddInt_v2 logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_228 = new uScriptAct_AddInt_v2();

	private int logic_uScriptAct_AddInt_v2_A_228;

	private int logic_uScriptAct_AddInt_v2_B_228 = 1;

	private int logic_uScriptAct_AddInt_v2_IntResult_228;

	private float logic_uScriptAct_AddInt_v2_FloatResult_228;

	private bool logic_uScriptAct_AddInt_v2_Out_228 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_231 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_231 = true;

	private uScript_AccessListBlock logic_uScript_AccessListBlock_uScript_AccessListBlock_232 = new uScript_AccessListBlock();

	private TankBlock[] logic_uScript_AccessListBlock_blockList_232 = new TankBlock[0];

	private int logic_uScript_AccessListBlock_index_232;

	private TankBlock logic_uScript_AccessListBlock_value_232;

	private bool logic_uScript_AccessListBlock_Out_232 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_237 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_237 = "";

	private bool logic_uScript_EnableGlow_enable_237;

	private bool logic_uScript_EnableGlow_Out_237 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_238 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_238 = true;

	private uScript_BlockAttachedToTech logic_uScript_BlockAttachedToTech_uScript_BlockAttachedToTech_239 = new uScript_BlockAttachedToTech();

	private Tank logic_uScript_BlockAttachedToTech_tech_239;

	private TankBlock logic_uScript_BlockAttachedToTech_block_239;

	private bool logic_uScript_BlockAttachedToTech_True_239 = true;

	private bool logic_uScript_BlockAttachedToTech_False_239 = true;

	private uScript_SpawnGhostBlocks logic_uScript_SpawnGhostBlocks_uScript_SpawnGhostBlocks_241 = new uScript_SpawnGhostBlocks();

	private GhostBlockSpawnData[] logic_uScript_SpawnGhostBlocks_ghostBlockData_241 = new GhostBlockSpawnData[0];

	private GameObject logic_uScript_SpawnGhostBlocks_ownerNode_241;

	private Tank logic_uScript_SpawnGhostBlocks_targetTech_241;

	private TankBlock[] logic_uScript_SpawnGhostBlocks_Return_241;

	private bool logic_uScript_SpawnGhostBlocks_OnAlreadySpawned_241 = true;

	private bool logic_uScript_SpawnGhostBlocks_OnSpawned_241 = true;

	private uScriptCon_CompareInt logic_uScriptCon_CompareInt_uScriptCon_CompareInt_244 = new uScriptCon_CompareInt();

	private int logic_uScriptCon_CompareInt_A_244;

	private int logic_uScriptCon_CompareInt_B_244 = 3;

	private bool logic_uScriptCon_CompareInt_GreaterThan_244 = true;

	private bool logic_uScriptCon_CompareInt_GreaterThanOrEqualTo_244 = true;

	private bool logic_uScriptCon_CompareInt_EqualTo_244 = true;

	private bool logic_uScriptCon_CompareInt_NotEqualTo_244 = true;

	private bool logic_uScriptCon_CompareInt_LessThanOrEqualTo_244 = true;

	private bool logic_uScriptCon_CompareInt_LessThan_244 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_246 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_246 = "";

	private bool logic_uScript_EnableGlow_enable_246 = true;

	private bool logic_uScript_EnableGlow_Out_246 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_249 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_249 = "";

	private bool logic_uScript_EnableGlow_enable_249 = true;

	private bool logic_uScript_EnableGlow_Out_249 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_251 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_251 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_252 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_252 = "";

	private bool logic_uScript_EnableGlow_enable_252;

	private bool logic_uScript_EnableGlow_Out_252 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_253 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_253 = true;

	private uScript_PointArrowAtVisible logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_254 = new uScript_PointArrowAtVisible();

	private object logic_uScript_PointArrowAtVisible_targetObject_254 = "";

	private float logic_uScript_PointArrowAtVisible_timeToShowFor_254 = -1f;

	private Vector3 logic_uScript_PointArrowAtVisible_offset_254 = new Vector3(0f, 0f, 0f);

	private bool logic_uScript_PointArrowAtVisible_Out_254 = true;

	private uScript_PointArrowAtVisible logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_257 = new uScript_PointArrowAtVisible();

	private object logic_uScript_PointArrowAtVisible_targetObject_257 = "";

	private float logic_uScript_PointArrowAtVisible_timeToShowFor_257 = -1f;

	private Vector3 logic_uScript_PointArrowAtVisible_offset_257 = new Vector3(0f, 0f, 0f);

	private bool logic_uScript_PointArrowAtVisible_Out_257 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_264 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_264 = "";

	private bool logic_uScript_EnableGlow_enable_264 = true;

	private bool logic_uScript_EnableGlow_Out_264 = true;

	private uScript_HideArrow logic_uScript_HideArrow_uScript_HideArrow_266 = new uScript_HideArrow();

	private bool logic_uScript_HideArrow_Out_266 = true;

	private uScript_SpawnGhostBlocks logic_uScript_SpawnGhostBlocks_uScript_SpawnGhostBlocks_267 = new uScript_SpawnGhostBlocks();

	private GhostBlockSpawnData[] logic_uScript_SpawnGhostBlocks_ghostBlockData_267 = new GhostBlockSpawnData[0];

	private GameObject logic_uScript_SpawnGhostBlocks_ownerNode_267;

	private Tank logic_uScript_SpawnGhostBlocks_targetTech_267;

	private TankBlock[] logic_uScript_SpawnGhostBlocks_Return_267;

	private bool logic_uScript_SpawnGhostBlocks_OnAlreadySpawned_267 = true;

	private bool logic_uScript_SpawnGhostBlocks_OnSpawned_267 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_269 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_269;

	private bool logic_uScriptAct_SetBool_Out_269 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_269 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_269 = true;

	private uScript_BlockAttachedToTech logic_uScript_BlockAttachedToTech_uScript_BlockAttachedToTech_271 = new uScript_BlockAttachedToTech();

	private Tank logic_uScript_BlockAttachedToTech_tech_271;

	private TankBlock logic_uScript_BlockAttachedToTech_block_271;

	private bool logic_uScript_BlockAttachedToTech_True_271 = true;

	private bool logic_uScript_BlockAttachedToTech_False_271 = true;

	private uScript_DoesTechHaveBlockAtPosition logic_uScript_DoesTechHaveBlockAtPosition_uScript_DoesTechHaveBlockAtPosition_275 = new uScript_DoesTechHaveBlockAtPosition();

	private Tank logic_uScript_DoesTechHaveBlockAtPosition_tech_275;

	private BlockTypes logic_uScript_DoesTechHaveBlockAtPosition_blockType_275;

	private Vector3 logic_uScript_DoesTechHaveBlockAtPosition_localPosition_275 = new Vector3(2f, 1f, 2f);

	private bool logic_uScript_DoesTechHaveBlockAtPosition_True_275 = true;

	private bool logic_uScript_DoesTechHaveBlockAtPosition_False_275 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_278 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_278;

	private bool logic_uScriptCon_CompareBool_True_278 = true;

	private bool logic_uScriptCon_CompareBool_False_278 = true;

	private uScriptCon_ManualSwitch logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_279 = new uScriptCon_ManualSwitch();

	private int logic_uScriptCon_ManualSwitch_CurrentOutput_279;

	private uScript_PointArrowAtVisible logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_281 = new uScript_PointArrowAtVisible();

	private object logic_uScript_PointArrowAtVisible_targetObject_281 = "";

	private float logic_uScript_PointArrowAtVisible_timeToShowFor_281 = -1f;

	private Vector3 logic_uScript_PointArrowAtVisible_offset_281 = new Vector3(0f, 0f, 0f);

	private bool logic_uScript_PointArrowAtVisible_Out_281 = true;

	private uScriptCon_ManualSwitch logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_282 = new uScriptCon_ManualSwitch();

	private int logic_uScriptCon_ManualSwitch_CurrentOutput_282;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_283 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_283;

	private bool logic_uScriptAct_SetBool_Out_283 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_283 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_283 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_285 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_285 = "";

	private bool logic_uScript_EnableGlow_enable_285;

	private bool logic_uScript_EnableGlow_Out_285 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_289 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_289;

	private bool logic_uScriptAct_SetBool_Out_289 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_289 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_289 = true;

	private uScript_SpawnGhostBlocks logic_uScript_SpawnGhostBlocks_uScript_SpawnGhostBlocks_291 = new uScript_SpawnGhostBlocks();

	private GhostBlockSpawnData[] logic_uScript_SpawnGhostBlocks_ghostBlockData_291 = new GhostBlockSpawnData[0];

	private GameObject logic_uScript_SpawnGhostBlocks_ownerNode_291;

	private Tank logic_uScript_SpawnGhostBlocks_targetTech_291;

	private TankBlock[] logic_uScript_SpawnGhostBlocks_Return_291;

	private bool logic_uScript_SpawnGhostBlocks_OnAlreadySpawned_291 = true;

	private bool logic_uScript_SpawnGhostBlocks_OnSpawned_291 = true;

	private uScript_IsPlayerInteractingWithBlock logic_uScript_IsPlayerInteractingWithBlock_uScript_IsPlayerInteractingWithBlock_293 = new uScript_IsPlayerInteractingWithBlock();

	private TankBlock logic_uScript_IsPlayerInteractingWithBlock_block_293;

	private bool logic_uScript_IsPlayerInteractingWithBlock_Out_293 = true;

	private bool logic_uScript_IsPlayerInteractingWithBlock_Interacted_293 = true;

	private bool logic_uScript_IsPlayerInteractingWithBlock_NotInteracted_293 = true;

	private bool logic_uScript_IsPlayerInteractingWithBlock_Dragging_293 = true;

	private bool logic_uScript_IsPlayerInteractingWithBlock_NotDragging_293 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_295 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_295 = "";

	private bool logic_uScript_EnableGlow_enable_295;

	private bool logic_uScript_EnableGlow_Out_295 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_298 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_298 = "";

	private bool logic_uScript_EnableGlow_enable_298;

	private bool logic_uScript_EnableGlow_Out_298 = true;

	private uScript_DoesTechHaveBlockAtPosition logic_uScript_DoesTechHaveBlockAtPosition_uScript_DoesTechHaveBlockAtPosition_306 = new uScript_DoesTechHaveBlockAtPosition();

	private Tank logic_uScript_DoesTechHaveBlockAtPosition_tech_306;

	private BlockTypes logic_uScript_DoesTechHaveBlockAtPosition_blockType_306;

	private Vector3 logic_uScript_DoesTechHaveBlockAtPosition_localPosition_306 = new Vector3(2f, 1f, 1f);

	private bool logic_uScript_DoesTechHaveBlockAtPosition_True_306 = true;

	private bool logic_uScript_DoesTechHaveBlockAtPosition_False_306 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_307 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_307 = "";

	private bool logic_uScript_EnableGlow_enable_307;

	private bool logic_uScript_EnableGlow_Out_307 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_308 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_308 = "";

	private bool logic_uScript_EnableGlow_enable_308;

	private bool logic_uScript_EnableGlow_Out_308 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_309 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_309 = "";

	private bool logic_uScript_EnableGlow_enable_309 = true;

	private bool logic_uScript_EnableGlow_Out_309 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_310 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_310 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_311 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_311 = "";

	private bool logic_uScript_EnableGlow_enable_311;

	private bool logic_uScript_EnableGlow_Out_311 = true;

	private uScript_IsPlayerInteractingWithBlock logic_uScript_IsPlayerInteractingWithBlock_uScript_IsPlayerInteractingWithBlock_313 = new uScript_IsPlayerInteractingWithBlock();

	private TankBlock logic_uScript_IsPlayerInteractingWithBlock_block_313;

	private bool logic_uScript_IsPlayerInteractingWithBlock_Out_313 = true;

	private bool logic_uScript_IsPlayerInteractingWithBlock_Interacted_313 = true;

	private bool logic_uScript_IsPlayerInteractingWithBlock_NotInteracted_313 = true;

	private bool logic_uScript_IsPlayerInteractingWithBlock_Dragging_313 = true;

	private bool logic_uScript_IsPlayerInteractingWithBlock_NotDragging_313 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_314 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_314 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_315 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_315;

	private bool logic_uScriptCon_CompareBool_True_315 = true;

	private bool logic_uScriptCon_CompareBool_False_315 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_317 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_317 = "";

	private bool logic_uScript_EnableGlow_enable_317 = true;

	private bool logic_uScript_EnableGlow_Out_317 = true;

	private uScriptCon_CompareInt logic_uScriptCon_CompareInt_uScriptCon_CompareInt_318 = new uScriptCon_CompareInt();

	private int logic_uScriptCon_CompareInt_A_318;

	private int logic_uScriptCon_CompareInt_B_318;

	private bool logic_uScriptCon_CompareInt_GreaterThan_318 = true;

	private bool logic_uScriptCon_CompareInt_GreaterThanOrEqualTo_318 = true;

	private bool logic_uScriptCon_CompareInt_EqualTo_318 = true;

	private bool logic_uScriptCon_CompareInt_NotEqualTo_318 = true;

	private bool logic_uScriptCon_CompareInt_LessThanOrEqualTo_318 = true;

	private bool logic_uScriptCon_CompareInt_LessThan_318 = true;

	private uScript_IsPlayerInteractingWithBlock logic_uScript_IsPlayerInteractingWithBlock_uScript_IsPlayerInteractingWithBlock_321 = new uScript_IsPlayerInteractingWithBlock();

	private TankBlock logic_uScript_IsPlayerInteractingWithBlock_block_321;

	private bool logic_uScript_IsPlayerInteractingWithBlock_Out_321 = true;

	private bool logic_uScript_IsPlayerInteractingWithBlock_Interacted_321 = true;

	private bool logic_uScript_IsPlayerInteractingWithBlock_NotInteracted_321 = true;

	private bool logic_uScript_IsPlayerInteractingWithBlock_Dragging_321 = true;

	private bool logic_uScript_IsPlayerInteractingWithBlock_NotDragging_321 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_322 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_322 = "";

	private bool logic_uScript_EnableGlow_enable_322 = true;

	private bool logic_uScript_EnableGlow_Out_322 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_323 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_323 = "";

	private bool logic_uScript_EnableGlow_enable_323;

	private bool logic_uScript_EnableGlow_Out_323 = true;

	private uScript_AccessListBlock logic_uScript_AccessListBlock_uScript_AccessListBlock_325 = new uScript_AccessListBlock();

	private TankBlock[] logic_uScript_AccessListBlock_blockList_325 = new TankBlock[0];

	private int logic_uScript_AccessListBlock_index_325;

	private TankBlock logic_uScript_AccessListBlock_value_325;

	private bool logic_uScript_AccessListBlock_Out_325 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_326 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_326 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_328 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_328 = "";

	private bool logic_uScript_EnableGlow_enable_328;

	private bool logic_uScript_EnableGlow_Out_328 = true;

	private uScriptCon_CompareInt logic_uScriptCon_CompareInt_uScriptCon_CompareInt_330 = new uScriptCon_CompareInt();

	private int logic_uScriptCon_CompareInt_A_330;

	private int logic_uScriptCon_CompareInt_B_330;

	private bool logic_uScriptCon_CompareInt_GreaterThan_330 = true;

	private bool logic_uScriptCon_CompareInt_GreaterThanOrEqualTo_330 = true;

	private bool logic_uScriptCon_CompareInt_EqualTo_330 = true;

	private bool logic_uScriptCon_CompareInt_NotEqualTo_330 = true;

	private bool logic_uScriptCon_CompareInt_LessThanOrEqualTo_330 = true;

	private bool logic_uScriptCon_CompareInt_LessThan_330 = true;

	private uScript_BlockAttachedToTech logic_uScript_BlockAttachedToTech_uScript_BlockAttachedToTech_335 = new uScript_BlockAttachedToTech();

	private Tank logic_uScript_BlockAttachedToTech_tech_335;

	private TankBlock logic_uScript_BlockAttachedToTech_block_335;

	private bool logic_uScript_BlockAttachedToTech_True_335 = true;

	private bool logic_uScript_BlockAttachedToTech_False_335 = true;

	private uScript_PointArrowAtVisible logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_336 = new uScript_PointArrowAtVisible();

	private object logic_uScript_PointArrowAtVisible_targetObject_336 = "";

	private float logic_uScript_PointArrowAtVisible_timeToShowFor_336 = -1f;

	private Vector3 logic_uScript_PointArrowAtVisible_offset_336 = new Vector3(0f, 0f, 0f);

	private bool logic_uScript_PointArrowAtVisible_Out_336 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_338 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_338 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_339 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_339;

	private bool logic_uScriptCon_CompareBool_True_339 = true;

	private bool logic_uScriptCon_CompareBool_False_339 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_340 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_340;

	private bool logic_uScriptCon_CompareBool_True_340 = true;

	private bool logic_uScriptCon_CompareBool_False_340 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_343 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_343;

	private bool logic_uScriptAct_SetBool_Out_343 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_343 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_343 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_346 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_346;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_346;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_346;

	private bool logic_uScript_AddMessage_Out_346 = true;

	private bool logic_uScript_AddMessage_Shown_346 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_347 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_347;

	private bool logic_uScriptCon_CompareBool_True_347 = true;

	private bool logic_uScriptCon_CompareBool_False_347 = true;

	private uScriptAct_AddInt_v2 logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_351 = new uScriptAct_AddInt_v2();

	private int logic_uScriptAct_AddInt_v2_A_351;

	private int logic_uScriptAct_AddInt_v2_B_351 = 1;

	private int logic_uScriptAct_AddInt_v2_IntResult_351;

	private float logic_uScriptAct_AddInt_v2_FloatResult_351;

	private bool logic_uScriptAct_AddInt_v2_Out_351 = true;

	private SubGraph_SaveLoadInt logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_354 = new SubGraph_SaveLoadInt();

	private int logic_SubGraph_SaveLoadInt_restartValue_354;

	private int logic_SubGraph_SaveLoadInt_integer_354;

	private object logic_SubGraph_SaveLoadInt_intAsVariable_354 = "";

	private string logic_SubGraph_SaveLoadInt_uniqueID_354 = "ToggleInteractions";

	private uScriptCon_CompareInt logic_uScriptCon_CompareInt_uScriptCon_CompareInt_355 = new uScriptCon_CompareInt();

	private int logic_uScriptCon_CompareInt_A_355;

	private int logic_uScriptCon_CompareInt_B_355 = 2;

	private bool logic_uScriptCon_CompareInt_GreaterThan_355 = true;

	private bool logic_uScriptCon_CompareInt_GreaterThanOrEqualTo_355 = true;

	private bool logic_uScriptCon_CompareInt_EqualTo_355 = true;

	private bool logic_uScriptCon_CompareInt_NotEqualTo_355 = true;

	private bool logic_uScriptCon_CompareInt_LessThanOrEqualTo_355 = true;

	private bool logic_uScriptCon_CompareInt_LessThan_355 = true;

	private uScriptCon_CompareInt logic_uScriptCon_CompareInt_uScriptCon_CompareInt_356 = new uScriptCon_CompareInt();

	private int logic_uScriptCon_CompareInt_A_356;

	private int logic_uScriptCon_CompareInt_B_356;

	private bool logic_uScriptCon_CompareInt_GreaterThan_356 = true;

	private bool logic_uScriptCon_CompareInt_GreaterThanOrEqualTo_356 = true;

	private bool logic_uScriptCon_CompareInt_EqualTo_356 = true;

	private bool logic_uScriptCon_CompareInt_NotEqualTo_356 = true;

	private bool logic_uScriptCon_CompareInt_LessThanOrEqualTo_356 = true;

	private bool logic_uScriptCon_CompareInt_LessThan_356 = true;

	private uScriptCon_ManualSwitch logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_358 = new uScriptCon_ManualSwitch();

	private int logic_uScriptCon_ManualSwitch_CurrentOutput_358;

	private uScriptAct_AddInt_v2 logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_361 = new uScriptAct_AddInt_v2();

	private int logic_uScriptAct_AddInt_v2_A_361;

	private int logic_uScriptAct_AddInt_v2_B_361 = 1;

	private int logic_uScriptAct_AddInt_v2_IntResult_361;

	private float logic_uScriptAct_AddInt_v2_FloatResult_361;

	private bool logic_uScriptAct_AddInt_v2_Out_361 = true;

	private uScript_PointArrowAtVisible logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_362 = new uScript_PointArrowAtVisible();

	private object logic_uScript_PointArrowAtVisible_targetObject_362 = "";

	private float logic_uScript_PointArrowAtVisible_timeToShowFor_362 = -1f;

	private Vector3 logic_uScript_PointArrowAtVisible_offset_362 = new Vector3(0f, 0f, 0f);

	private bool logic_uScript_PointArrowAtVisible_Out_362 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_363 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_363 = "";

	private bool logic_uScript_EnableGlow_enable_363 = true;

	private bool logic_uScript_EnableGlow_Out_363 = true;

	private uScript_PointArrowAtVisible logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_365 = new uScript_PointArrowAtVisible();

	private object logic_uScript_PointArrowAtVisible_targetObject_365 = "";

	private float logic_uScript_PointArrowAtVisible_timeToShowFor_365 = -1f;

	private Vector3 logic_uScript_PointArrowAtVisible_offset_365 = new Vector3(0f, 0f, 0f);

	private bool logic_uScript_PointArrowAtVisible_Out_365 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_366 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_366 = "";

	private bool logic_uScript_EnableGlow_enable_366;

	private bool logic_uScript_EnableGlow_Out_366 = true;

	private uScript_PointArrowAtVisible logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_368 = new uScript_PointArrowAtVisible();

	private object logic_uScript_PointArrowAtVisible_targetObject_368 = "";

	private float logic_uScript_PointArrowAtVisible_timeToShowFor_368 = -1f;

	private Vector3 logic_uScript_PointArrowAtVisible_offset_368 = new Vector3(0f, 0f, 0f);

	private bool logic_uScript_PointArrowAtVisible_Out_368 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_369 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_369 = "";

	private bool logic_uScript_EnableGlow_enable_369;

	private bool logic_uScript_EnableGlow_Out_369 = true;

	private uScript_HideArrow logic_uScript_HideArrow_uScript_HideArrow_371 = new uScript_HideArrow();

	private bool logic_uScript_HideArrow_Out_371 = true;

	private uScript_HideArrow logic_uScript_HideArrow_uScript_HideArrow_372 = new uScript_HideArrow();

	private bool logic_uScript_HideArrow_Out_372 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_374 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_374;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_374;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_374;

	private bool logic_uScript_AddMessage_Out_374 = true;

	private bool logic_uScript_AddMessage_Shown_374 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_377 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_377;

	private bool logic_uScriptCon_CompareBool_True_377 = true;

	private bool logic_uScriptCon_CompareBool_False_377 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_378 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_378;

	private bool logic_uScriptAct_SetBool_Out_378 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_378 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_378 = true;

	private SubGraph_SaveLoadInt logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_380 = new SubGraph_SaveLoadInt();

	private int logic_SubGraph_SaveLoadInt_restartValue_380;

	private int logic_SubGraph_SaveLoadInt_integer_380;

	private object logic_SubGraph_SaveLoadInt_intAsVariable_380 = "";

	private string logic_SubGraph_SaveLoadInt_uniqueID_380 = "StartingHour";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_383 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_383;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_383 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_383 = "msgToggleOffShown";

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_385 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_385;

	private bool logic_uScriptAct_SetBool_Out_385 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_385 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_385 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_387 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_387;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_387 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_387 = "msgCompleteIsOnShown";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_389 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_389;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_389 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_389 = "msgBeamSensorAttachedIsOnShown";

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_391 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_391;

	private bool logic_uScriptCon_CompareBool_True_391 = true;

	private bool logic_uScriptCon_CompareBool_False_391 = true;

	private uScriptAct_PrintText logic_uScriptAct_PrintText_uScriptAct_PrintText_393 = new uScriptAct_PrintText();

	private string logic_uScriptAct_PrintText_Text_393 = "";

	private int logic_uScriptAct_PrintText_FontSize_393 = 16;

	private FontStyle logic_uScriptAct_PrintText_FontStyle_393;

	private Color logic_uScriptAct_PrintText_FontColor_393 = new Color(0f, 0f, 0f, 1f);

	private TextAnchor logic_uScriptAct_PrintText_textAnchor_393;

	private int logic_uScriptAct_PrintText_EdgePadding_393 = 8;

	private float logic_uScriptAct_PrintText_time_393;

	private bool logic_uScriptAct_PrintText_Out_393 = true;

	private uScriptAct_Concatenate logic_uScriptAct_Concatenate_uScriptAct_Concatenate_395 = new uScriptAct_Concatenate();

	private object[] logic_uScriptAct_Concatenate_A_395 = new object[0];

	private object[] logic_uScriptAct_Concatenate_B_395 = new object[0];

	private string logic_uScriptAct_Concatenate_Separator_395 = "";

	private string logic_uScriptAct_Concatenate_Result_395;

	private bool logic_uScriptAct_Concatenate_Out_395 = true;

	private uScriptAct_Concatenate logic_uScriptAct_Concatenate_uScriptAct_Concatenate_397 = new uScriptAct_Concatenate();

	private object[] logic_uScriptAct_Concatenate_A_397 = new object[0];

	private object[] logic_uScriptAct_Concatenate_B_397 = new object[0];

	private string logic_uScriptAct_Concatenate_Separator_397 = "";

	private string logic_uScriptAct_Concatenate_Result_397;

	private bool logic_uScriptAct_Concatenate_Out_397 = true;

	private uScriptAct_Concatenate logic_uScriptAct_Concatenate_uScriptAct_Concatenate_398 = new uScriptAct_Concatenate();

	private object[] logic_uScriptAct_Concatenate_A_398 = new object[0];

	private object[] logic_uScriptAct_Concatenate_B_398 = new object[0];

	private string logic_uScriptAct_Concatenate_Separator_398 = "";

	private string logic_uScriptAct_Concatenate_Result_398;

	private bool logic_uScriptAct_Concatenate_Out_398 = true;

	private uScriptAct_Concatenate logic_uScriptAct_Concatenate_uScriptAct_Concatenate_400 = new uScriptAct_Concatenate();

	private object[] logic_uScriptAct_Concatenate_A_400 = new object[0];

	private object[] logic_uScriptAct_Concatenate_B_400 = new object[0];

	private string logic_uScriptAct_Concatenate_Separator_400 = "";

	private string logic_uScriptAct_Concatenate_Result_400;

	private bool logic_uScriptAct_Concatenate_Out_400 = true;

	private uScriptAct_Concatenate logic_uScriptAct_Concatenate_uScriptAct_Concatenate_408 = new uScriptAct_Concatenate();

	private object[] logic_uScriptAct_Concatenate_A_408 = new object[0];

	private object[] logic_uScriptAct_Concatenate_B_408 = new object[0];

	private string logic_uScriptAct_Concatenate_Separator_408 = "";

	private string logic_uScriptAct_Concatenate_Result_408;

	private bool logic_uScriptAct_Concatenate_Out_408 = true;

	private uScriptAct_Concatenate logic_uScriptAct_Concatenate_uScriptAct_Concatenate_409 = new uScriptAct_Concatenate();

	private object[] logic_uScriptAct_Concatenate_A_409 = new object[0];

	private object[] logic_uScriptAct_Concatenate_B_409 = new object[0];

	private string logic_uScriptAct_Concatenate_Separator_409 = "";

	private string logic_uScriptAct_Concatenate_Result_409;

	private bool logic_uScriptAct_Concatenate_Out_409 = true;

	private uScriptAct_Concatenate logic_uScriptAct_Concatenate_uScriptAct_Concatenate_410 = new uScriptAct_Concatenate();

	private object[] logic_uScriptAct_Concatenate_A_410 = new object[0];

	private object[] logic_uScriptAct_Concatenate_B_410 = new object[0];

	private string logic_uScriptAct_Concatenate_Separator_410 = "";

	private string logic_uScriptAct_Concatenate_Result_410;

	private bool logic_uScriptAct_Concatenate_Out_410 = true;

	private uScript_SetTimeOfDay logic_uScript_SetTimeOfDay_uScript_SetTimeOfDay_419 = new uScript_SetTimeOfDay();

	private int logic_uScript_SetTimeOfDay_hour_419 = 4;

	private Tank logic_uScript_SetTimeOfDay_tech_419;

	private bool logic_uScript_SetTimeOfDay_Out_419 = true;

	private uScript_SetTimeOfDay logic_uScript_SetTimeOfDay_uScript_SetTimeOfDay_420 = new uScript_SetTimeOfDay();

	private int logic_uScript_SetTimeOfDay_hour_420 = 12;

	private Tank logic_uScript_SetTimeOfDay_tech_420;

	private bool logic_uScript_SetTimeOfDay_Out_420 = true;

	private uScriptCon_CompareInt logic_uScriptCon_CompareInt_uScriptCon_CompareInt_421 = new uScriptCon_CompareInt();

	private int logic_uScriptCon_CompareInt_A_421;

	private int logic_uScriptCon_CompareInt_B_421 = 3;

	private bool logic_uScriptCon_CompareInt_GreaterThan_421 = true;

	private bool logic_uScriptCon_CompareInt_GreaterThanOrEqualTo_421 = true;

	private bool logic_uScriptCon_CompareInt_EqualTo_421 = true;

	private bool logic_uScriptCon_CompareInt_NotEqualTo_421 = true;

	private bool logic_uScriptCon_CompareInt_LessThanOrEqualTo_421 = true;

	private bool logic_uScriptCon_CompareInt_LessThan_421 = true;

	private uScript_SetTimeOfDay logic_uScript_SetTimeOfDay_uScript_SetTimeOfDay_422 = new uScript_SetTimeOfDay();

	private int logic_uScript_SetTimeOfDay_hour_422;

	private Tank logic_uScript_SetTimeOfDay_tech_422;

	private bool logic_uScript_SetTimeOfDay_Out_422 = true;

	private uScriptCon_CompareInt logic_uScriptCon_CompareInt_uScriptCon_CompareInt_424 = new uScriptCon_CompareInt();

	private int logic_uScriptCon_CompareInt_A_424;

	private int logic_uScriptCon_CompareInt_B_424 = 2;

	private bool logic_uScriptCon_CompareInt_GreaterThan_424 = true;

	private bool logic_uScriptCon_CompareInt_GreaterThanOrEqualTo_424 = true;

	private bool logic_uScriptCon_CompareInt_EqualTo_424 = true;

	private bool logic_uScriptCon_CompareInt_NotEqualTo_424 = true;

	private bool logic_uScriptCon_CompareInt_LessThanOrEqualTo_424 = true;

	private bool logic_uScriptCon_CompareInt_LessThan_424 = true;

	private uScript_PausePopulation logic_uScript_PausePopulation_uScript_PausePopulation_427 = new uScript_PausePopulation();

	private bool logic_uScript_PausePopulation_Out_427 = true;

	private uScript_PlayerInRangeOfCurrentEncounter logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_431 = new uScript_PlayerInRangeOfCurrentEncounter();

	private GameObject logic_uScript_PlayerInRangeOfCurrentEncounter_ownerNode_431;

	private bool logic_uScript_PlayerInRangeOfCurrentEncounter_Out_431 = true;

	private bool logic_uScript_PlayerInRangeOfCurrentEncounter_True_431 = true;

	private bool logic_uScript_PlayerInRangeOfCurrentEncounter_False_431 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_432 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_432;

	private bool logic_uScriptCon_CompareBool_True_432 = true;

	private bool logic_uScriptCon_CompareBool_False_432 = true;

	private uScript_GetTimeOfDay logic_uScript_GetTimeOfDay_uScript_GetTimeOfDay_433 = new uScript_GetTimeOfDay();

	private int logic_uScript_GetTimeOfDay_Return_433;

	private bool logic_uScript_GetTimeOfDay_Out_433 = true;

	private uScript_SetTimeOfDay logic_uScript_SetTimeOfDay_uScript_SetTimeOfDay_435 = new uScript_SetTimeOfDay();

	private int logic_uScript_SetTimeOfDay_hour_435;

	private Tank logic_uScript_SetTimeOfDay_tech_435;

	private bool logic_uScript_SetTimeOfDay_Out_435 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_436 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_436;

	private bool logic_uScriptAct_SetBool_Out_436 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_436 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_436 = true;

	private uScript_IsPlayerInTriggerSmart logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_437 = new uScript_IsPlayerInTriggerSmart();

	private string logic_uScript_IsPlayerInTriggerSmart_innerTriggerArea_437 = "";

	private string logic_uScript_IsPlayerInTriggerSmart_outerTriggerArea_437 = "";

	private bool logic_uScript_IsPlayerInTriggerSmart_inside_437;

	private bool logic_uScript_IsPlayerInTriggerSmart_Out_437 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_FirstEntered_437 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_AllInside_437 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_LastExited_437 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_AllOutside_437 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_SomeInside_437 = true;

	private bool logic_uScript_IsPlayerInTriggerSmart_SomeOutside_437 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_439 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_439 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_440 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_440 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_442 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_442 = true;

	private SubGraph_Crafting_Tutorial_Init logic_SubGraph_Crafting_Tutorial_Init_SubGraph_Crafting_Tutorial_Init_443 = new SubGraph_Crafting_Tutorial_Init();

	private SpawnTechData[] logic_SubGraph_Crafting_Tutorial_Init_baseSpawnData_443 = new SpawnTechData[0];

	private SpawnBlockData[] logic_SubGraph_Crafting_Tutorial_Init_blockSpawnData_443 = new SpawnBlockData[0];

	private SpawnTechData[] logic_SubGraph_Crafting_Tutorial_Init_NPCSpawnData_443 = new SpawnTechData[0];

	private TankPreset logic_SubGraph_Crafting_Tutorial_Init_completedBasePreset_443;

	private bool logic_SubGraph_Crafting_Tutorial_Init_useNPCAsTutorialTech_443;

	private bool logic_SubGraph_Crafting_Tutorial_Init_spawnBase_443 = true;

	private string logic_SubGraph_Crafting_Tutorial_Init_basePosition_443 = "";

	private float logic_SubGraph_Crafting_Tutorial_Init_clearSceneryRadius_443;

	private Tank logic_SubGraph_Crafting_Tutorial_Init_CraftingBaseTech_443;

	private Tank logic_SubGraph_Crafting_Tutorial_Init_NPCTech_443;

	private SubGraph_Crafting_Tutorial_ManageBlock logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_446 = new SubGraph_Crafting_Tutorial_ManageBlock();

	private SpawnBlockData[] logic_SubGraph_Crafting_Tutorial_ManageBlock_blockSpawnData_446 = new SpawnBlockData[0];

	private int logic_SubGraph_Crafting_Tutorial_ManageBlock_blockIndex_446;

	private TankBlock logic_SubGraph_Crafting_Tutorial_ManageBlock_block_446;

	private bool logic_SubGraph_Crafting_Tutorial_ManageBlock_allowAnchoring_446;

	private uScript_AddMessage.MessageData logic_SubGraph_Crafting_Tutorial_ManageBlock_msgBlockOutsideArea_446;

	private uScript_AddMessage.MessageSpeaker logic_SubGraph_Crafting_Tutorial_ManageBlock_messageSpeaker_446;

	private SubGraph_Crafting_Tutorial_ManageBlock logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_449 = new SubGraph_Crafting_Tutorial_ManageBlock();

	private SpawnBlockData[] logic_SubGraph_Crafting_Tutorial_ManageBlock_blockSpawnData_449 = new SpawnBlockData[0];

	private int logic_SubGraph_Crafting_Tutorial_ManageBlock_blockIndex_449;

	private TankBlock logic_SubGraph_Crafting_Tutorial_ManageBlock_block_449;

	private bool logic_SubGraph_Crafting_Tutorial_ManageBlock_allowAnchoring_449;

	private uScript_AddMessage.MessageData logic_SubGraph_Crafting_Tutorial_ManageBlock_msgBlockOutsideArea_449;

	private uScript_AddMessage.MessageSpeaker logic_SubGraph_Crafting_Tutorial_ManageBlock_messageSpeaker_449;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_460 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_460 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_463 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_463;

	private bool logic_uScriptCon_CompareBool_True_463 = true;

	private bool logic_uScriptCon_CompareBool_False_463 = true;

	private SubGraph_Crafting_Tutorial_ManageBlock logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_464 = new SubGraph_Crafting_Tutorial_ManageBlock();

	private SpawnBlockData[] logic_SubGraph_Crafting_Tutorial_ManageBlock_blockSpawnData_464 = new SpawnBlockData[0];

	private int logic_SubGraph_Crafting_Tutorial_ManageBlock_blockIndex_464 = 2;

	private TankBlock logic_SubGraph_Crafting_Tutorial_ManageBlock_block_464;

	private bool logic_SubGraph_Crafting_Tutorial_ManageBlock_allowAnchoring_464;

	private uScript_AddMessage.MessageData logic_SubGraph_Crafting_Tutorial_ManageBlock_msgBlockOutsideArea_464;

	private uScript_AddMessage.MessageSpeaker logic_SubGraph_Crafting_Tutorial_ManageBlock_messageSpeaker_464;

	private SubGraph_Crafting_Tutorial_ManageBlock logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_469 = new SubGraph_Crafting_Tutorial_ManageBlock();

	private SpawnBlockData[] logic_SubGraph_Crafting_Tutorial_ManageBlock_blockSpawnData_469 = new SpawnBlockData[0];

	private int logic_SubGraph_Crafting_Tutorial_ManageBlock_blockIndex_469 = 3;

	private TankBlock logic_SubGraph_Crafting_Tutorial_ManageBlock_block_469;

	private bool logic_SubGraph_Crafting_Tutorial_ManageBlock_allowAnchoring_469;

	private uScript_AddMessage.MessageData logic_SubGraph_Crafting_Tutorial_ManageBlock_msgBlockOutsideArea_469;

	private uScript_AddMessage.MessageSpeaker logic_SubGraph_Crafting_Tutorial_ManageBlock_messageSpeaker_469;

	private SubGraph_Crafting_Tutorial_ManageBlock logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_476 = new SubGraph_Crafting_Tutorial_ManageBlock();

	private SpawnBlockData[] logic_SubGraph_Crafting_Tutorial_ManageBlock_blockSpawnData_476 = new SpawnBlockData[0];

	private int logic_SubGraph_Crafting_Tutorial_ManageBlock_blockIndex_476 = 1;

	private TankBlock logic_SubGraph_Crafting_Tutorial_ManageBlock_block_476;

	private bool logic_SubGraph_Crafting_Tutorial_ManageBlock_allowAnchoring_476;

	private uScript_AddMessage.MessageData logic_SubGraph_Crafting_Tutorial_ManageBlock_msgBlockOutsideArea_476;

	private uScript_AddMessage.MessageSpeaker logic_SubGraph_Crafting_Tutorial_ManageBlock_messageSpeaker_476;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_480 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_480;

	private bool logic_uScriptCon_CompareBool_True_480 = true;

	private bool logic_uScriptCon_CompareBool_False_480 = true;

	private uScript_SetTimeOfDay logic_uScript_SetTimeOfDay_uScript_SetTimeOfDay_481 = new uScript_SetTimeOfDay();

	private int logic_uScript_SetTimeOfDay_hour_481;

	private Tank logic_uScript_SetTimeOfDay_tech_481;

	private bool logic_uScript_SetTimeOfDay_Out_481 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_482 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_482;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_482 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_482 = "TechInitialized";

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_486 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_486;

	private bool logic_uScriptCon_CompareBool_True_486 = true;

	private bool logic_uScriptCon_CompareBool_False_486 = true;

	private uScript_GetTankBlock logic_uScript_GetTankBlock_uScript_GetTankBlock_488 = new uScript_GetTankBlock();

	private Tank logic_uScript_GetTankBlock_tank_488;

	private BlockTypes logic_uScript_GetTankBlock_blockType_488;

	private TankBlock logic_uScript_GetTankBlock_Return_488;

	private bool logic_uScript_GetTankBlock_Out_488 = true;

	private bool logic_uScript_GetTankBlock_Returned_488 = true;

	private bool logic_uScript_GetTankBlock_NotFound_488 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_492 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_492 = "";

	private bool logic_uScript_EnableGlow_enable_492 = true;

	private bool logic_uScript_EnableGlow_Out_492 = true;

	private uScript_PointArrowAtVisible logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_493 = new uScript_PointArrowAtVisible();

	private object logic_uScript_PointArrowAtVisible_targetObject_493 = "";

	private float logic_uScript_PointArrowAtVisible_timeToShowFor_493 = -1f;

	private Vector3 logic_uScript_PointArrowAtVisible_offset_493 = new Vector3(0f, 0f, 0f);

	private bool logic_uScript_PointArrowAtVisible_Out_493 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_497 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_497;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_497;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_497;

	private bool logic_uScript_AddMessage_Out_497 = true;

	private bool logic_uScript_AddMessage_Shown_497 = true;

	private uScript_PointArrowAtVisible logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_499 = new uScript_PointArrowAtVisible();

	private object logic_uScript_PointArrowAtVisible_targetObject_499 = "";

	private float logic_uScript_PointArrowAtVisible_timeToShowFor_499 = -1f;

	private Vector3 logic_uScript_PointArrowAtVisible_offset_499 = new Vector3(0f, 0f, 0f);

	private bool logic_uScript_PointArrowAtVisible_Out_499 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_501 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_501 = "";

	private bool logic_uScript_EnableGlow_enable_501;

	private bool logic_uScript_EnableGlow_Out_501 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_502 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_502;

	private bool logic_uScriptAct_SetBool_Out_502 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_502 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_502 = true;

	private uScript_GetTankBlock logic_uScript_GetTankBlock_uScript_GetTankBlock_504 = new uScript_GetTankBlock();

	private Tank logic_uScript_GetTankBlock_tank_504;

	private BlockTypes logic_uScript_GetTankBlock_blockType_504;

	private TankBlock logic_uScript_GetTankBlock_Return_504;

	private bool logic_uScript_GetTankBlock_Out_504 = true;

	private bool logic_uScript_GetTankBlock_Returned_504 = true;

	private bool logic_uScript_GetTankBlock_NotFound_504 = true;

	private SubGraph_SaveLoadInt logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_508 = new SubGraph_SaveLoadInt();

	private int logic_SubGraph_SaveLoadInt_restartValue_508 = 1;

	private int logic_SubGraph_SaveLoadInt_integer_508;

	private object logic_SubGraph_SaveLoadInt_intAsVariable_508 = "";

	private string logic_SubGraph_SaveLoadInt_uniqueID_508 = "Objective2SubStage";

	private uScriptCon_ManualSwitch logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_510 = new uScriptCon_ManualSwitch();

	private int logic_uScriptCon_ManualSwitch_CurrentOutput_510;

	private uScriptAct_AddInt_v2 logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_513 = new uScriptAct_AddInt_v2();

	private int logic_uScriptAct_AddInt_v2_A_513;

	private int logic_uScriptAct_AddInt_v2_B_513 = 1;

	private int logic_uScriptAct_AddInt_v2_IntResult_513;

	private float logic_uScriptAct_AddInt_v2_FloatResult_513;

	private bool logic_uScriptAct_AddInt_v2_Out_513 = true;

	private uScriptCon_ManualSwitch logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_514 = new uScriptCon_ManualSwitch();

	private int logic_uScriptCon_ManualSwitch_CurrentOutput_514;

	private uScriptAct_AddInt_v2 logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_516 = new uScriptAct_AddInt_v2();

	private int logic_uScriptAct_AddInt_v2_A_516;

	private int logic_uScriptAct_AddInt_v2_B_516 = 1;

	private int logic_uScriptAct_AddInt_v2_IntResult_516;

	private float logic_uScriptAct_AddInt_v2_FloatResult_516;

	private bool logic_uScriptAct_AddInt_v2_Out_516 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_518 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_518;

	private bool logic_uScriptAct_SetBool_Out_518 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_518 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_518 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_523 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_523;

	private bool logic_uScriptCon_CompareBool_True_523 = true;

	private bool logic_uScriptCon_CompareBool_False_523 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_524 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_524;

	private bool logic_uScriptCon_CompareBool_True_524 = true;

	private bool logic_uScriptCon_CompareBool_False_524 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_525 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_525;

	private bool logic_uScriptAct_SetBool_Out_525 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_525 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_525 = true;

	private SubGraph_Crafting_Tutorial_Finish logic_SubGraph_Crafting_Tutorial_Finish_SubGraph_Crafting_Tutorial_Finish_527 = new SubGraph_Crafting_Tutorial_Finish();

	private Tank logic_SubGraph_Crafting_Tutorial_Finish_craftingBaseTech_527;

	private Tank logic_SubGraph_Crafting_Tutorial_Finish_NPCTech_527;

	private ExternalBehaviorTree logic_SubGraph_Crafting_Tutorial_Finish_NPCFlyAwayAI_527;

	private Transform logic_SubGraph_Crafting_Tutorial_Finish_NPCDespawnParticleEffect_527;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_528 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_528;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_528;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_528;

	private bool logic_uScript_AddMessage_Out_528 = true;

	private bool logic_uScript_AddMessage_Shown_528 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_534 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_534;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_534;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_534;

	private bool logic_uScript_AddMessage_Out_534 = true;

	private bool logic_uScript_AddMessage_Shown_534 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_537 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_537;

	private bool logic_uScriptAct_SetBool_Out_537 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_537 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_537 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_541 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_541;

	private bool logic_uScriptCon_CompareBool_True_541 = true;

	private bool logic_uScriptCon_CompareBool_False_541 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_545 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_545;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_545;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_545;

	private bool logic_uScript_AddMessage_Out_545 = true;

	private bool logic_uScript_AddMessage_Shown_545 = true;

	private uScript_GetCircuitChargeInfo logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_546 = new uScript_GetCircuitChargeInfo();

	private TankBlock logic_uScript_GetCircuitChargeInfo_block_546;

	private Tank logic_uScript_GetCircuitChargeInfo_tech_546;

	private BlockTypes logic_uScript_GetCircuitChargeInfo_blockType_546;

	private int logic_uScript_GetCircuitChargeInfo_Return_546;

	private bool logic_uScript_GetCircuitChargeInfo_Out_546 = true;

	private bool logic_uScript_GetCircuitChargeInfo_HasValidBlock_546 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_549 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_549;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_549;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_549;

	private bool logic_uScript_AddMessage_Out_549 = true;

	private bool logic_uScript_AddMessage_Shown_549 = true;

	private uScriptCon_CompareInt logic_uScriptCon_CompareInt_uScriptCon_CompareInt_553 = new uScriptCon_CompareInt();

	private int logic_uScriptCon_CompareInt_A_553;

	private int logic_uScriptCon_CompareInt_B_553;

	private bool logic_uScriptCon_CompareInt_GreaterThan_553 = true;

	private bool logic_uScriptCon_CompareInt_GreaterThanOrEqualTo_553 = true;

	private bool logic_uScriptCon_CompareInt_EqualTo_553 = true;

	private bool logic_uScriptCon_CompareInt_NotEqualTo_553 = true;

	private bool logic_uScriptCon_CompareInt_LessThanOrEqualTo_553 = true;

	private bool logic_uScriptCon_CompareInt_LessThan_553 = true;

	private SubGraph_SaveLoadInt logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_554 = new SubGraph_SaveLoadInt();

	private int logic_SubGraph_SaveLoadInt_restartValue_554 = 1;

	private int logic_SubGraph_SaveLoadInt_integer_554;

	private object logic_SubGraph_SaveLoadInt_intAsVariable_554 = "";

	private string logic_SubGraph_SaveLoadInt_uniqueID_554 = "Objective3SubStage";

	private SubGraph_AddMessageWithPadSupport logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_556 = new SubGraph_AddMessageWithPadSupport();

	private uScript_AddMessage.MessageData logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboard_556;

	private uScript_AddMessage.MessageData logic_SubGraph_AddMessageWithPadSupport_messageControlPad_556;

	private uScript_AddMessage.MessageSpeaker logic_SubGraph_AddMessageWithPadSupport_speaker_556;

	private ManOnScreenMessages.OnScreenMessage logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboardReturn_556;

	private ManOnScreenMessages.OnScreenMessage logic_SubGraph_AddMessageWithPadSupport_messageControlPadReturn_556;

	private SubGraph_AddMessageWithPadSupport logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_560 = new SubGraph_AddMessageWithPadSupport();

	private uScript_AddMessage.MessageData logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboard_560;

	private uScript_AddMessage.MessageData logic_SubGraph_AddMessageWithPadSupport_messageControlPad_560;

	private uScript_AddMessage.MessageSpeaker logic_SubGraph_AddMessageWithPadSupport_speaker_560;

	private ManOnScreenMessages.OnScreenMessage logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboardReturn_560;

	private ManOnScreenMessages.OnScreenMessage logic_SubGraph_AddMessageWithPadSupport_messageControlPadReturn_560;

	private SubGraph_AddMessageWithPadSupport logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_567 = new SubGraph_AddMessageWithPadSupport();

	private uScript_AddMessage.MessageData logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboard_567;

	private uScript_AddMessage.MessageData logic_SubGraph_AddMessageWithPadSupport_messageControlPad_567;

	private uScript_AddMessage.MessageSpeaker logic_SubGraph_AddMessageWithPadSupport_speaker_567;

	private ManOnScreenMessages.OnScreenMessage logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboardReturn_567;

	private ManOnScreenMessages.OnScreenMessage logic_SubGraph_AddMessageWithPadSupport_messageControlPadReturn_567;

	private void SyncUnityHooks()
	{
		SyncEventListeners();
		if (null == owner_Connection_1 || !m_RegisteredForEvents)
		{
			owner_Connection_1 = parentGameObject;
			if (null != owner_Connection_1)
			{
				uScript_SaveLoad uScript_SaveLoad2 = owner_Connection_1.GetComponent<uScript_SaveLoad>();
				if (null == uScript_SaveLoad2)
				{
					uScript_SaveLoad2 = owner_Connection_1.AddComponent<uScript_SaveLoad>();
				}
				if (null != uScript_SaveLoad2)
				{
					uScript_SaveLoad2.SaveEvent += Instance_SaveEvent_0;
					uScript_SaveLoad2.LoadEvent += Instance_LoadEvent_0;
					uScript_SaveLoad2.RestartEvent += Instance_RestartEvent_0;
				}
			}
		}
		if (null == owner_Connection_3 || !m_RegisteredForEvents)
		{
			owner_Connection_3 = parentGameObject;
			if (null != owner_Connection_3)
			{
				uScript_EncounterUpdate uScript_EncounterUpdate2 = owner_Connection_3.GetComponent<uScript_EncounterUpdate>();
				if (null == uScript_EncounterUpdate2)
				{
					uScript_EncounterUpdate2 = owner_Connection_3.AddComponent<uScript_EncounterUpdate>();
				}
				if (null != uScript_EncounterUpdate2)
				{
					uScript_EncounterUpdate2.OnUpdate += Instance_OnUpdate_2;
					uScript_EncounterUpdate2.OnSuspend += Instance_OnSuspend_2;
					uScript_EncounterUpdate2.OnResume += Instance_OnResume_2;
				}
			}
		}
		if (null == owner_Connection_35 || !m_RegisteredForEvents)
		{
			owner_Connection_35 = parentGameObject;
		}
		if (null == owner_Connection_67 || !m_RegisteredForEvents)
		{
			owner_Connection_67 = parentGameObject;
		}
		if (null == owner_Connection_72 || !m_RegisteredForEvents)
		{
			owner_Connection_72 = parentGameObject;
		}
		if (null == owner_Connection_73 || !m_RegisteredForEvents)
		{
			owner_Connection_73 = parentGameObject;
		}
		if (null == owner_Connection_297 || !m_RegisteredForEvents)
		{
			owner_Connection_297 = parentGameObject;
		}
		if (null == owner_Connection_301 || !m_RegisteredForEvents)
		{
			owner_Connection_301 = parentGameObject;
		}
		if (null == owner_Connection_320 || !m_RegisteredForEvents)
		{
			owner_Connection_320 = parentGameObject;
		}
		if (null == owner_Connection_429 || !m_RegisteredForEvents)
		{
			owner_Connection_429 = parentGameObject;
		}
	}

	private void RegisterForUnityHooks()
	{
		SyncEventListeners();
		if (!m_RegisteredForEvents && null != owner_Connection_1)
		{
			uScript_SaveLoad uScript_SaveLoad2 = owner_Connection_1.GetComponent<uScript_SaveLoad>();
			if (null == uScript_SaveLoad2)
			{
				uScript_SaveLoad2 = owner_Connection_1.AddComponent<uScript_SaveLoad>();
			}
			if (null != uScript_SaveLoad2)
			{
				uScript_SaveLoad2.SaveEvent += Instance_SaveEvent_0;
				uScript_SaveLoad2.LoadEvent += Instance_LoadEvent_0;
				uScript_SaveLoad2.RestartEvent += Instance_RestartEvent_0;
			}
		}
		if (!m_RegisteredForEvents && null != owner_Connection_3)
		{
			uScript_EncounterUpdate uScript_EncounterUpdate2 = owner_Connection_3.GetComponent<uScript_EncounterUpdate>();
			if (null == uScript_EncounterUpdate2)
			{
				uScript_EncounterUpdate2 = owner_Connection_3.AddComponent<uScript_EncounterUpdate>();
			}
			if (null != uScript_EncounterUpdate2)
			{
				uScript_EncounterUpdate2.OnUpdate += Instance_OnUpdate_2;
				uScript_EncounterUpdate2.OnSuspend += Instance_OnSuspend_2;
				uScript_EncounterUpdate2.OnResume += Instance_OnResume_2;
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
			uScript_SaveLoad component = owner_Connection_1.GetComponent<uScript_SaveLoad>();
			if (null != component)
			{
				component.SaveEvent -= Instance_SaveEvent_0;
				component.LoadEvent -= Instance_LoadEvent_0;
				component.RestartEvent -= Instance_RestartEvent_0;
			}
		}
		if (null != owner_Connection_3)
		{
			uScript_EncounterUpdate component2 = owner_Connection_3.GetComponent<uScript_EncounterUpdate>();
			if (null != component2)
			{
				component2.OnUpdate -= Instance_OnUpdate_2;
				component2.OnSuspend -= Instance_OnSuspend_2;
				component2.OnResume -= Instance_OnResume_2;
			}
		}
	}

	public override void SetParent(GameObject g)
	{
		parentGameObject = g;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_5.SetParent(g);
		logic_uScript_PausePopulation_uScript_PausePopulation_7.SetParent(g);
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_8.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_10.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_13.SetParent(g);
		logic_uScript_PausePopulation_uScript_PausePopulation_14.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_16.SetParent(g);
		logic_uScript_LockTechInteraction_uScript_LockTechInteraction_17.SetParent(g);
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_19.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_20.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_21.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_22.SetParent(g);
		logic_uScript_HideArrow_uScript_HideArrow_23.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_24.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_25.SetParent(g);
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_27.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_28.SetParent(g);
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_42.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_45.SetParent(g);
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_47.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_50.SetParent(g);
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_52.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_56.SetParent(g);
		logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_SubGraph_Crafting_Tutorial_AttachBlockToBase_58.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_59.SetParent(g);
		logic_uScript_HideArrow_uScript_HideArrow_60.SetParent(g);
		logic_uScript_RemoveAllGhostBlocksOnTech_uScript_RemoveAllGhostBlocksOnTech_62.SetParent(g);
		logic_uScript_GetAndCheckBlocks_uScript_GetAndCheckBlocks_63.SetParent(g);
		logic_uScript_SpawnBlocksFromData_uScript_SpawnBlocksFromData_66.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_69.SetParent(g);
		logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_74.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_75.SetParent(g);
		logic_uScript_LockTechInteraction_uScript_LockTechInteraction_78.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_80.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_82.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_84.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_85.SetParent(g);
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_86.SetParent(g);
		logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_89.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_91.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_92.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_95.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_97.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_100.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_102.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_103.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_105.SetParent(g);
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_107.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_109.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_111.SetParent(g);
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_112.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_115.SetParent(g);
		logic_uScript_RemoveAllGhostBlocksOnTech_uScript_RemoveAllGhostBlocksOnTech_118.SetParent(g);
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_123.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_125.SetParent(g);
		logic_uScript_HideArrow_uScript_HideArrow_126.SetParent(g);
		logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_SubGraph_Crafting_Tutorial_AttachBlockToBase_128.SetParent(g);
		logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_131.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_133.SetParent(g);
		logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_137.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_138.SetParent(g);
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_139.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_140.SetParent(g);
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_142.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_143.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_146.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_147.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_148.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_153.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_154.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_155.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_156.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_158.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_159.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_160.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_162.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_163.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_165.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_168.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_171.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_173.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_175.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_177.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_178.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_179.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_184.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_186.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_188.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_193.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_194.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_196.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_197.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_201.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_203.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_206.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_207.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_208.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_211.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_214.SetParent(g);
		logic_uScript_DoesTechHaveBlockAtPosition_uScript_DoesTechHaveBlockAtPosition_215.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_216.SetParent(g);
		logic_uScript_AccessListBlock_uScript_AccessListBlock_217.SetParent(g);
		logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_219.SetParent(g);
		logic_uScript_RemoveAllGhostBlocksOnTech_uScript_RemoveAllGhostBlocksOnTech_220.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_221.SetParent(g);
		logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_222.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_224.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_226.SetParent(g);
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_228.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_231.SetParent(g);
		logic_uScript_AccessListBlock_uScript_AccessListBlock_232.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_237.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_238.SetParent(g);
		logic_uScript_BlockAttachedToTech_uScript_BlockAttachedToTech_239.SetParent(g);
		logic_uScript_SpawnGhostBlocks_uScript_SpawnGhostBlocks_241.SetParent(g);
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_244.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_246.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_249.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_251.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_252.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_253.SetParent(g);
		logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_254.SetParent(g);
		logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_257.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_264.SetParent(g);
		logic_uScript_HideArrow_uScript_HideArrow_266.SetParent(g);
		logic_uScript_SpawnGhostBlocks_uScript_SpawnGhostBlocks_267.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_269.SetParent(g);
		logic_uScript_BlockAttachedToTech_uScript_BlockAttachedToTech_271.SetParent(g);
		logic_uScript_DoesTechHaveBlockAtPosition_uScript_DoesTechHaveBlockAtPosition_275.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_278.SetParent(g);
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_279.SetParent(g);
		logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_281.SetParent(g);
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_282.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_283.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_285.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_289.SetParent(g);
		logic_uScript_SpawnGhostBlocks_uScript_SpawnGhostBlocks_291.SetParent(g);
		logic_uScript_IsPlayerInteractingWithBlock_uScript_IsPlayerInteractingWithBlock_293.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_295.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_298.SetParent(g);
		logic_uScript_DoesTechHaveBlockAtPosition_uScript_DoesTechHaveBlockAtPosition_306.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_307.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_308.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_309.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_310.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_311.SetParent(g);
		logic_uScript_IsPlayerInteractingWithBlock_uScript_IsPlayerInteractingWithBlock_313.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_314.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_315.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_317.SetParent(g);
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_318.SetParent(g);
		logic_uScript_IsPlayerInteractingWithBlock_uScript_IsPlayerInteractingWithBlock_321.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_322.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_323.SetParent(g);
		logic_uScript_AccessListBlock_uScript_AccessListBlock_325.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_326.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_328.SetParent(g);
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_330.SetParent(g);
		logic_uScript_BlockAttachedToTech_uScript_BlockAttachedToTech_335.SetParent(g);
		logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_336.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_338.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_339.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_340.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_343.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_346.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_347.SetParent(g);
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_351.SetParent(g);
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_354.SetParent(g);
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_355.SetParent(g);
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_356.SetParent(g);
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_358.SetParent(g);
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_361.SetParent(g);
		logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_362.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_363.SetParent(g);
		logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_365.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_366.SetParent(g);
		logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_368.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_369.SetParent(g);
		logic_uScript_HideArrow_uScript_HideArrow_371.SetParent(g);
		logic_uScript_HideArrow_uScript_HideArrow_372.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_374.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_377.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_378.SetParent(g);
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_380.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_383.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_385.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_387.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_389.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_391.SetParent(g);
		logic_uScriptAct_PrintText_uScriptAct_PrintText_393.SetParent(g);
		logic_uScriptAct_Concatenate_uScriptAct_Concatenate_395.SetParent(g);
		logic_uScriptAct_Concatenate_uScriptAct_Concatenate_397.SetParent(g);
		logic_uScriptAct_Concatenate_uScriptAct_Concatenate_398.SetParent(g);
		logic_uScriptAct_Concatenate_uScriptAct_Concatenate_400.SetParent(g);
		logic_uScriptAct_Concatenate_uScriptAct_Concatenate_408.SetParent(g);
		logic_uScriptAct_Concatenate_uScriptAct_Concatenate_409.SetParent(g);
		logic_uScriptAct_Concatenate_uScriptAct_Concatenate_410.SetParent(g);
		logic_uScript_SetTimeOfDay_uScript_SetTimeOfDay_419.SetParent(g);
		logic_uScript_SetTimeOfDay_uScript_SetTimeOfDay_420.SetParent(g);
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_421.SetParent(g);
		logic_uScript_SetTimeOfDay_uScript_SetTimeOfDay_422.SetParent(g);
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_424.SetParent(g);
		logic_uScript_PausePopulation_uScript_PausePopulation_427.SetParent(g);
		logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_431.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_432.SetParent(g);
		logic_uScript_GetTimeOfDay_uScript_GetTimeOfDay_433.SetParent(g);
		logic_uScript_SetTimeOfDay_uScript_SetTimeOfDay_435.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_436.SetParent(g);
		logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_437.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_439.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_440.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_442.SetParent(g);
		logic_SubGraph_Crafting_Tutorial_Init_SubGraph_Crafting_Tutorial_Init_443.SetParent(g);
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_446.SetParent(g);
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_449.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_460.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_463.SetParent(g);
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_464.SetParent(g);
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_469.SetParent(g);
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_476.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_480.SetParent(g);
		logic_uScript_SetTimeOfDay_uScript_SetTimeOfDay_481.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_482.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_486.SetParent(g);
		logic_uScript_GetTankBlock_uScript_GetTankBlock_488.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_492.SetParent(g);
		logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_493.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_497.SetParent(g);
		logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_499.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_501.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_502.SetParent(g);
		logic_uScript_GetTankBlock_uScript_GetTankBlock_504.SetParent(g);
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_508.SetParent(g);
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_510.SetParent(g);
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_513.SetParent(g);
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_514.SetParent(g);
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_516.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_518.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_523.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_524.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_525.SetParent(g);
		logic_SubGraph_Crafting_Tutorial_Finish_SubGraph_Crafting_Tutorial_Finish_527.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_528.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_534.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_537.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_541.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_545.SetParent(g);
		logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_546.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_549.SetParent(g);
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_553.SetParent(g);
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_554.SetParent(g);
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_556.SetParent(g);
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_560.SetParent(g);
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_567.SetParent(g);
		owner_Connection_1 = parentGameObject;
		owner_Connection_3 = parentGameObject;
		owner_Connection_35 = parentGameObject;
		owner_Connection_67 = parentGameObject;
		owner_Connection_72 = parentGameObject;
		owner_Connection_73 = parentGameObject;
		owner_Connection_297 = parentGameObject;
		owner_Connection_301 = parentGameObject;
		owner_Connection_320 = parentGameObject;
		owner_Connection_429 = parentGameObject;
	}

	public void Awake()
	{
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_47.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_50.Awake();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_52.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_56.Awake();
		logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_SubGraph_Crafting_Tutorial_AttachBlockToBase_58.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_75.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_82.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_91.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_105.Awake();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_107.Awake();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_112.Awake();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_123.Awake();
		logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_SubGraph_Crafting_Tutorial_AttachBlockToBase_128.Awake();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_142.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_143.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_173.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_193.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_194.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_203.Awake();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_354.Awake();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_380.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_383.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_387.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_389.Awake();
		logic_SubGraph_Crafting_Tutorial_Init_SubGraph_Crafting_Tutorial_Init_443.Awake();
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_446.Awake();
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_449.Awake();
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_464.Awake();
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_469.Awake();
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_476.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_482.Awake();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_508.Awake();
		logic_SubGraph_Crafting_Tutorial_Finish_SubGraph_Crafting_Tutorial_Finish_527.Awake();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_554.Awake();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_556.Awake();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_560.Awake();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_567.Awake();
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_42.Output1 += uScriptCon_ManualSwitch_Output1_42;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_42.Output2 += uScriptCon_ManualSwitch_Output2_42;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_42.Output3 += uScriptCon_ManualSwitch_Output3_42;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_42.Output4 += uScriptCon_ManualSwitch_Output4_42;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_42.Output5 += uScriptCon_ManualSwitch_Output5_42;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_42.Output6 += uScriptCon_ManualSwitch_Output6_42;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_42.Output7 += uScriptCon_ManualSwitch_Output7_42;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_42.Output8 += uScriptCon_ManualSwitch_Output8_42;
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_47.Out += SubGraph_LoadObjectiveStates_Out_47;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_50.Save_Out += SubGraph_SaveLoadBool_Save_Out_50;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_50.Load_Out += SubGraph_SaveLoadBool_Load_Out_50;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_50.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_50;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_52.Save_Out += SubGraph_SaveLoadInt_Save_Out_52;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_52.Load_Out += SubGraph_SaveLoadInt_Load_Out_52;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_52.Restart_Out += SubGraph_SaveLoadInt_Restart_Out_52;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_56.Save_Out += SubGraph_SaveLoadBool_Save_Out_56;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_56.Load_Out += SubGraph_SaveLoadBool_Load_Out_56;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_56.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_56;
		logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_SubGraph_Crafting_Tutorial_AttachBlockToBase_58.Block_Attached += SubGraph_Crafting_Tutorial_AttachBlockToBase_Block_Attached_58;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_75.Save_Out += SubGraph_SaveLoadBool_Save_Out_75;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_75.Load_Out += SubGraph_SaveLoadBool_Load_Out_75;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_75.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_75;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_82.Save_Out += SubGraph_SaveLoadBool_Save_Out_82;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_82.Load_Out += SubGraph_SaveLoadBool_Load_Out_82;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_82.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_82;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_91.Save_Out += SubGraph_SaveLoadBool_Save_Out_91;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_91.Load_Out += SubGraph_SaveLoadBool_Load_Out_91;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_91.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_91;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_105.Save_Out += SubGraph_SaveLoadBool_Save_Out_105;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_105.Load_Out += SubGraph_SaveLoadBool_Load_Out_105;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_105.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_105;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_107.Out += SubGraph_CompleteObjectiveStage_Out_107;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_112.Save_Out += SubGraph_SaveLoadInt_Save_Out_112;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_112.Load_Out += SubGraph_SaveLoadInt_Load_Out_112;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_112.Restart_Out += SubGraph_SaveLoadInt_Restart_Out_112;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_123.Out += SubGraph_CompleteObjectiveStage_Out_123;
		logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_SubGraph_Crafting_Tutorial_AttachBlockToBase_128.Block_Attached += SubGraph_Crafting_Tutorial_AttachBlockToBase_Block_Attached_128;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_142.Out += SubGraph_CompleteObjectiveStage_Out_142;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_143.Save_Out += SubGraph_SaveLoadBool_Save_Out_143;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_143.Load_Out += SubGraph_SaveLoadBool_Load_Out_143;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_143.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_143;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_173.Save_Out += SubGraph_SaveLoadBool_Save_Out_173;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_173.Load_Out += SubGraph_SaveLoadBool_Load_Out_173;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_173.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_173;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_193.Save_Out += SubGraph_SaveLoadBool_Save_Out_193;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_193.Load_Out += SubGraph_SaveLoadBool_Load_Out_193;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_193.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_193;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_194.Save_Out += SubGraph_SaveLoadBool_Save_Out_194;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_194.Load_Out += SubGraph_SaveLoadBool_Load_Out_194;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_194.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_194;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_203.Save_Out += SubGraph_SaveLoadBool_Save_Out_203;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_203.Load_Out += SubGraph_SaveLoadBool_Load_Out_203;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_203.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_203;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_279.Output1 += uScriptCon_ManualSwitch_Output1_279;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_279.Output2 += uScriptCon_ManualSwitch_Output2_279;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_279.Output3 += uScriptCon_ManualSwitch_Output3_279;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_279.Output4 += uScriptCon_ManualSwitch_Output4_279;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_279.Output5 += uScriptCon_ManualSwitch_Output5_279;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_279.Output6 += uScriptCon_ManualSwitch_Output6_279;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_279.Output7 += uScriptCon_ManualSwitch_Output7_279;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_279.Output8 += uScriptCon_ManualSwitch_Output8_279;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_282.Output1 += uScriptCon_ManualSwitch_Output1_282;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_282.Output2 += uScriptCon_ManualSwitch_Output2_282;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_282.Output3 += uScriptCon_ManualSwitch_Output3_282;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_282.Output4 += uScriptCon_ManualSwitch_Output4_282;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_282.Output5 += uScriptCon_ManualSwitch_Output5_282;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_282.Output6 += uScriptCon_ManualSwitch_Output6_282;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_282.Output7 += uScriptCon_ManualSwitch_Output7_282;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_282.Output8 += uScriptCon_ManualSwitch_Output8_282;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_354.Save_Out += SubGraph_SaveLoadInt_Save_Out_354;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_354.Load_Out += SubGraph_SaveLoadInt_Load_Out_354;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_354.Restart_Out += SubGraph_SaveLoadInt_Restart_Out_354;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_358.Output1 += uScriptCon_ManualSwitch_Output1_358;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_358.Output2 += uScriptCon_ManualSwitch_Output2_358;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_358.Output3 += uScriptCon_ManualSwitch_Output3_358;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_358.Output4 += uScriptCon_ManualSwitch_Output4_358;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_358.Output5 += uScriptCon_ManualSwitch_Output5_358;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_358.Output6 += uScriptCon_ManualSwitch_Output6_358;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_358.Output7 += uScriptCon_ManualSwitch_Output7_358;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_358.Output8 += uScriptCon_ManualSwitch_Output8_358;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_380.Save_Out += SubGraph_SaveLoadInt_Save_Out_380;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_380.Load_Out += SubGraph_SaveLoadInt_Load_Out_380;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_380.Restart_Out += SubGraph_SaveLoadInt_Restart_Out_380;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_383.Save_Out += SubGraph_SaveLoadBool_Save_Out_383;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_383.Load_Out += SubGraph_SaveLoadBool_Load_Out_383;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_383.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_383;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_387.Save_Out += SubGraph_SaveLoadBool_Save_Out_387;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_387.Load_Out += SubGraph_SaveLoadBool_Load_Out_387;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_387.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_387;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_389.Save_Out += SubGraph_SaveLoadBool_Save_Out_389;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_389.Load_Out += SubGraph_SaveLoadBool_Load_Out_389;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_389.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_389;
		logic_SubGraph_Crafting_Tutorial_Init_SubGraph_Crafting_Tutorial_Init_443.Out += SubGraph_Crafting_Tutorial_Init_Out_443;
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_446.Out += SubGraph_Crafting_Tutorial_ManageBlock_Out_446;
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_449.Out += SubGraph_Crafting_Tutorial_ManageBlock_Out_449;
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_464.Out += SubGraph_Crafting_Tutorial_ManageBlock_Out_464;
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_469.Out += SubGraph_Crafting_Tutorial_ManageBlock_Out_469;
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_476.Out += SubGraph_Crafting_Tutorial_ManageBlock_Out_476;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_482.Save_Out += SubGraph_SaveLoadBool_Save_Out_482;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_482.Load_Out += SubGraph_SaveLoadBool_Load_Out_482;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_482.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_482;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_508.Save_Out += SubGraph_SaveLoadInt_Save_Out_508;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_508.Load_Out += SubGraph_SaveLoadInt_Load_Out_508;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_508.Restart_Out += SubGraph_SaveLoadInt_Restart_Out_508;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_510.Output1 += uScriptCon_ManualSwitch_Output1_510;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_510.Output2 += uScriptCon_ManualSwitch_Output2_510;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_510.Output3 += uScriptCon_ManualSwitch_Output3_510;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_510.Output4 += uScriptCon_ManualSwitch_Output4_510;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_510.Output5 += uScriptCon_ManualSwitch_Output5_510;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_510.Output6 += uScriptCon_ManualSwitch_Output6_510;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_510.Output7 += uScriptCon_ManualSwitch_Output7_510;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_510.Output8 += uScriptCon_ManualSwitch_Output8_510;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_514.Output1 += uScriptCon_ManualSwitch_Output1_514;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_514.Output2 += uScriptCon_ManualSwitch_Output2_514;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_514.Output3 += uScriptCon_ManualSwitch_Output3_514;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_514.Output4 += uScriptCon_ManualSwitch_Output4_514;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_514.Output5 += uScriptCon_ManualSwitch_Output5_514;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_514.Output6 += uScriptCon_ManualSwitch_Output6_514;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_514.Output7 += uScriptCon_ManualSwitch_Output7_514;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_514.Output8 += uScriptCon_ManualSwitch_Output8_514;
		logic_SubGraph_Crafting_Tutorial_Finish_SubGraph_Crafting_Tutorial_Finish_527.Out += SubGraph_Crafting_Tutorial_Finish_Out_527;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_554.Save_Out += SubGraph_SaveLoadInt_Save_Out_554;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_554.Load_Out += SubGraph_SaveLoadInt_Load_Out_554;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_554.Restart_Out += SubGraph_SaveLoadInt_Restart_Out_554;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_556.Out += SubGraph_AddMessageWithPadSupport_Out_556;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_556.Shown += SubGraph_AddMessageWithPadSupport_Shown_556;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_560.Out += SubGraph_AddMessageWithPadSupport_Out_560;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_560.Shown += SubGraph_AddMessageWithPadSupport_Shown_560;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_567.Out += SubGraph_AddMessageWithPadSupport_Out_567;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_567.Shown += SubGraph_AddMessageWithPadSupport_Shown_567;
	}

	public void Start()
	{
		SyncUnityHooks();
		m_RegisteredForEvents = true;
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_47.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_50.Start();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_52.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_56.Start();
		logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_SubGraph_Crafting_Tutorial_AttachBlockToBase_58.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_75.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_82.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_91.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_105.Start();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_107.Start();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_112.Start();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_123.Start();
		logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_SubGraph_Crafting_Tutorial_AttachBlockToBase_128.Start();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_142.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_143.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_173.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_193.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_194.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_203.Start();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_354.Start();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_380.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_383.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_387.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_389.Start();
		logic_SubGraph_Crafting_Tutorial_Init_SubGraph_Crafting_Tutorial_Init_443.Start();
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_446.Start();
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_449.Start();
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_464.Start();
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_469.Start();
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_476.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_482.Start();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_508.Start();
		logic_SubGraph_Crafting_Tutorial_Finish_SubGraph_Crafting_Tutorial_Finish_527.Start();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_554.Start();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_556.Start();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_560.Start();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_567.Start();
	}

	public void OnEnable()
	{
		RegisterForUnityHooks();
		m_RegisteredForEvents = true;
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_47.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_50.OnEnable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_52.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_56.OnEnable();
		logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_SubGraph_Crafting_Tutorial_AttachBlockToBase_58.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_75.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_82.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_91.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_105.OnEnable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_107.OnEnable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_112.OnEnable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_123.OnEnable();
		logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_SubGraph_Crafting_Tutorial_AttachBlockToBase_128.OnEnable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_142.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_143.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_173.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_193.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_194.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_203.OnEnable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_354.OnEnable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_380.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_383.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_387.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_389.OnEnable();
		logic_SubGraph_Crafting_Tutorial_Init_SubGraph_Crafting_Tutorial_Init_443.OnEnable();
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_446.OnEnable();
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_449.OnEnable();
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_464.OnEnable();
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_469.OnEnable();
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_476.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_482.OnEnable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_508.OnEnable();
		logic_SubGraph_Crafting_Tutorial_Finish_SubGraph_Crafting_Tutorial_Finish_527.OnEnable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_554.OnEnable();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_556.OnEnable();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_560.OnEnable();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_567.OnEnable();
	}

	public void OnDisable()
	{
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_19.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_21.OnDisable();
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_27.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_28.OnDisable();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_47.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_50.OnDisable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_52.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_56.OnDisable();
		logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_SubGraph_Crafting_Tutorial_AttachBlockToBase_58.OnDisable();
		logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_74.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_75.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_82.OnDisable();
		logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_89.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_91.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_105.OnDisable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_107.OnDisable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_112.OnDisable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_123.OnDisable();
		logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_SubGraph_Crafting_Tutorial_AttachBlockToBase_128.OnDisable();
		logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_137.OnDisable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_142.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_143.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_168.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_173.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_177.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_178.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_188.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_193.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_194.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_201.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_203.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_207.OnDisable();
		logic_uScript_IsPlayerInteractingWithBlock_uScript_IsPlayerInteractingWithBlock_293.OnDisable();
		logic_uScript_IsPlayerInteractingWithBlock_uScript_IsPlayerInteractingWithBlock_313.OnDisable();
		logic_uScript_IsPlayerInteractingWithBlock_uScript_IsPlayerInteractingWithBlock_321.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_346.OnDisable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_354.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_374.OnDisable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_380.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_383.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_387.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_389.OnDisable();
		logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_431.OnDisable();
		logic_SubGraph_Crafting_Tutorial_Init_SubGraph_Crafting_Tutorial_Init_443.OnDisable();
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_446.OnDisable();
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_449.OnDisable();
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_464.OnDisable();
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_469.OnDisable();
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_476.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_482.OnDisable();
		logic_uScript_GetTankBlock_uScript_GetTankBlock_488.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_497.OnDisable();
		logic_uScript_GetTankBlock_uScript_GetTankBlock_504.OnDisable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_508.OnDisable();
		logic_SubGraph_Crafting_Tutorial_Finish_SubGraph_Crafting_Tutorial_Finish_527.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_528.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_534.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_545.OnDisable();
		logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_546.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_549.OnDisable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_554.OnDisable();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_556.OnDisable();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_560.OnDisable();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_567.OnDisable();
		UnregisterEventListeners();
		m_RegisteredForEvents = false;
	}

	public void Update()
	{
		SyncEventListeners();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_47.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_50.Update();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_52.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_56.Update();
		logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_SubGraph_Crafting_Tutorial_AttachBlockToBase_58.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_75.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_82.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_91.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_105.Update();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_107.Update();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_112.Update();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_123.Update();
		logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_SubGraph_Crafting_Tutorial_AttachBlockToBase_128.Update();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_142.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_143.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_173.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_193.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_194.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_203.Update();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_354.Update();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_380.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_383.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_387.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_389.Update();
		logic_SubGraph_Crafting_Tutorial_Init_SubGraph_Crafting_Tutorial_Init_443.Update();
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_446.Update();
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_449.Update();
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_464.Update();
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_469.Update();
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_476.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_482.Update();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_508.Update();
		logic_SubGraph_Crafting_Tutorial_Finish_SubGraph_Crafting_Tutorial_Finish_527.Update();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_554.Update();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_556.Update();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_560.Update();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_567.Update();
	}

	public void OnDestroy()
	{
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_47.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_50.OnDestroy();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_52.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_56.OnDestroy();
		logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_SubGraph_Crafting_Tutorial_AttachBlockToBase_58.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_75.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_82.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_91.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_105.OnDestroy();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_107.OnDestroy();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_112.OnDestroy();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_123.OnDestroy();
		logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_SubGraph_Crafting_Tutorial_AttachBlockToBase_128.OnDestroy();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_142.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_143.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_173.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_193.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_194.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_203.OnDestroy();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_354.OnDestroy();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_380.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_383.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_387.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_389.OnDestroy();
		logic_SubGraph_Crafting_Tutorial_Init_SubGraph_Crafting_Tutorial_Init_443.OnDestroy();
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_446.OnDestroy();
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_449.OnDestroy();
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_464.OnDestroy();
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_469.OnDestroy();
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_476.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_482.OnDestroy();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_508.OnDestroy();
		logic_SubGraph_Crafting_Tutorial_Finish_SubGraph_Crafting_Tutorial_Finish_527.OnDestroy();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_554.OnDestroy();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_556.OnDestroy();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_560.OnDestroy();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_567.OnDestroy();
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_42.Output1 -= uScriptCon_ManualSwitch_Output1_42;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_42.Output2 -= uScriptCon_ManualSwitch_Output2_42;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_42.Output3 -= uScriptCon_ManualSwitch_Output3_42;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_42.Output4 -= uScriptCon_ManualSwitch_Output4_42;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_42.Output5 -= uScriptCon_ManualSwitch_Output5_42;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_42.Output6 -= uScriptCon_ManualSwitch_Output6_42;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_42.Output7 -= uScriptCon_ManualSwitch_Output7_42;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_42.Output8 -= uScriptCon_ManualSwitch_Output8_42;
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_47.Out -= SubGraph_LoadObjectiveStates_Out_47;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_50.Save_Out -= SubGraph_SaveLoadBool_Save_Out_50;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_50.Load_Out -= SubGraph_SaveLoadBool_Load_Out_50;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_50.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_50;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_52.Save_Out -= SubGraph_SaveLoadInt_Save_Out_52;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_52.Load_Out -= SubGraph_SaveLoadInt_Load_Out_52;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_52.Restart_Out -= SubGraph_SaveLoadInt_Restart_Out_52;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_56.Save_Out -= SubGraph_SaveLoadBool_Save_Out_56;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_56.Load_Out -= SubGraph_SaveLoadBool_Load_Out_56;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_56.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_56;
		logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_SubGraph_Crafting_Tutorial_AttachBlockToBase_58.Block_Attached -= SubGraph_Crafting_Tutorial_AttachBlockToBase_Block_Attached_58;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_75.Save_Out -= SubGraph_SaveLoadBool_Save_Out_75;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_75.Load_Out -= SubGraph_SaveLoadBool_Load_Out_75;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_75.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_75;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_82.Save_Out -= SubGraph_SaveLoadBool_Save_Out_82;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_82.Load_Out -= SubGraph_SaveLoadBool_Load_Out_82;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_82.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_82;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_91.Save_Out -= SubGraph_SaveLoadBool_Save_Out_91;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_91.Load_Out -= SubGraph_SaveLoadBool_Load_Out_91;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_91.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_91;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_105.Save_Out -= SubGraph_SaveLoadBool_Save_Out_105;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_105.Load_Out -= SubGraph_SaveLoadBool_Load_Out_105;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_105.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_105;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_107.Out -= SubGraph_CompleteObjectiveStage_Out_107;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_112.Save_Out -= SubGraph_SaveLoadInt_Save_Out_112;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_112.Load_Out -= SubGraph_SaveLoadInt_Load_Out_112;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_112.Restart_Out -= SubGraph_SaveLoadInt_Restart_Out_112;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_123.Out -= SubGraph_CompleteObjectiveStage_Out_123;
		logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_SubGraph_Crafting_Tutorial_AttachBlockToBase_128.Block_Attached -= SubGraph_Crafting_Tutorial_AttachBlockToBase_Block_Attached_128;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_142.Out -= SubGraph_CompleteObjectiveStage_Out_142;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_143.Save_Out -= SubGraph_SaveLoadBool_Save_Out_143;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_143.Load_Out -= SubGraph_SaveLoadBool_Load_Out_143;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_143.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_143;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_173.Save_Out -= SubGraph_SaveLoadBool_Save_Out_173;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_173.Load_Out -= SubGraph_SaveLoadBool_Load_Out_173;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_173.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_173;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_193.Save_Out -= SubGraph_SaveLoadBool_Save_Out_193;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_193.Load_Out -= SubGraph_SaveLoadBool_Load_Out_193;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_193.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_193;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_194.Save_Out -= SubGraph_SaveLoadBool_Save_Out_194;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_194.Load_Out -= SubGraph_SaveLoadBool_Load_Out_194;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_194.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_194;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_203.Save_Out -= SubGraph_SaveLoadBool_Save_Out_203;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_203.Load_Out -= SubGraph_SaveLoadBool_Load_Out_203;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_203.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_203;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_279.Output1 -= uScriptCon_ManualSwitch_Output1_279;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_279.Output2 -= uScriptCon_ManualSwitch_Output2_279;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_279.Output3 -= uScriptCon_ManualSwitch_Output3_279;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_279.Output4 -= uScriptCon_ManualSwitch_Output4_279;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_279.Output5 -= uScriptCon_ManualSwitch_Output5_279;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_279.Output6 -= uScriptCon_ManualSwitch_Output6_279;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_279.Output7 -= uScriptCon_ManualSwitch_Output7_279;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_279.Output8 -= uScriptCon_ManualSwitch_Output8_279;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_282.Output1 -= uScriptCon_ManualSwitch_Output1_282;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_282.Output2 -= uScriptCon_ManualSwitch_Output2_282;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_282.Output3 -= uScriptCon_ManualSwitch_Output3_282;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_282.Output4 -= uScriptCon_ManualSwitch_Output4_282;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_282.Output5 -= uScriptCon_ManualSwitch_Output5_282;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_282.Output6 -= uScriptCon_ManualSwitch_Output6_282;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_282.Output7 -= uScriptCon_ManualSwitch_Output7_282;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_282.Output8 -= uScriptCon_ManualSwitch_Output8_282;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_354.Save_Out -= SubGraph_SaveLoadInt_Save_Out_354;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_354.Load_Out -= SubGraph_SaveLoadInt_Load_Out_354;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_354.Restart_Out -= SubGraph_SaveLoadInt_Restart_Out_354;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_358.Output1 -= uScriptCon_ManualSwitch_Output1_358;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_358.Output2 -= uScriptCon_ManualSwitch_Output2_358;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_358.Output3 -= uScriptCon_ManualSwitch_Output3_358;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_358.Output4 -= uScriptCon_ManualSwitch_Output4_358;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_358.Output5 -= uScriptCon_ManualSwitch_Output5_358;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_358.Output6 -= uScriptCon_ManualSwitch_Output6_358;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_358.Output7 -= uScriptCon_ManualSwitch_Output7_358;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_358.Output8 -= uScriptCon_ManualSwitch_Output8_358;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_380.Save_Out -= SubGraph_SaveLoadInt_Save_Out_380;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_380.Load_Out -= SubGraph_SaveLoadInt_Load_Out_380;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_380.Restart_Out -= SubGraph_SaveLoadInt_Restart_Out_380;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_383.Save_Out -= SubGraph_SaveLoadBool_Save_Out_383;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_383.Load_Out -= SubGraph_SaveLoadBool_Load_Out_383;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_383.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_383;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_387.Save_Out -= SubGraph_SaveLoadBool_Save_Out_387;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_387.Load_Out -= SubGraph_SaveLoadBool_Load_Out_387;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_387.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_387;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_389.Save_Out -= SubGraph_SaveLoadBool_Save_Out_389;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_389.Load_Out -= SubGraph_SaveLoadBool_Load_Out_389;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_389.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_389;
		logic_SubGraph_Crafting_Tutorial_Init_SubGraph_Crafting_Tutorial_Init_443.Out -= SubGraph_Crafting_Tutorial_Init_Out_443;
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_446.Out -= SubGraph_Crafting_Tutorial_ManageBlock_Out_446;
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_449.Out -= SubGraph_Crafting_Tutorial_ManageBlock_Out_449;
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_464.Out -= SubGraph_Crafting_Tutorial_ManageBlock_Out_464;
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_469.Out -= SubGraph_Crafting_Tutorial_ManageBlock_Out_469;
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_476.Out -= SubGraph_Crafting_Tutorial_ManageBlock_Out_476;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_482.Save_Out -= SubGraph_SaveLoadBool_Save_Out_482;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_482.Load_Out -= SubGraph_SaveLoadBool_Load_Out_482;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_482.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_482;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_508.Save_Out -= SubGraph_SaveLoadInt_Save_Out_508;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_508.Load_Out -= SubGraph_SaveLoadInt_Load_Out_508;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_508.Restart_Out -= SubGraph_SaveLoadInt_Restart_Out_508;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_510.Output1 -= uScriptCon_ManualSwitch_Output1_510;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_510.Output2 -= uScriptCon_ManualSwitch_Output2_510;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_510.Output3 -= uScriptCon_ManualSwitch_Output3_510;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_510.Output4 -= uScriptCon_ManualSwitch_Output4_510;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_510.Output5 -= uScriptCon_ManualSwitch_Output5_510;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_510.Output6 -= uScriptCon_ManualSwitch_Output6_510;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_510.Output7 -= uScriptCon_ManualSwitch_Output7_510;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_510.Output8 -= uScriptCon_ManualSwitch_Output8_510;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_514.Output1 -= uScriptCon_ManualSwitch_Output1_514;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_514.Output2 -= uScriptCon_ManualSwitch_Output2_514;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_514.Output3 -= uScriptCon_ManualSwitch_Output3_514;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_514.Output4 -= uScriptCon_ManualSwitch_Output4_514;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_514.Output5 -= uScriptCon_ManualSwitch_Output5_514;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_514.Output6 -= uScriptCon_ManualSwitch_Output6_514;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_514.Output7 -= uScriptCon_ManualSwitch_Output7_514;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_514.Output8 -= uScriptCon_ManualSwitch_Output8_514;
		logic_SubGraph_Crafting_Tutorial_Finish_SubGraph_Crafting_Tutorial_Finish_527.Out -= SubGraph_Crafting_Tutorial_Finish_Out_527;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_554.Save_Out -= SubGraph_SaveLoadInt_Save_Out_554;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_554.Load_Out -= SubGraph_SaveLoadInt_Load_Out_554;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_554.Restart_Out -= SubGraph_SaveLoadInt_Restart_Out_554;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_556.Out -= SubGraph_AddMessageWithPadSupport_Out_556;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_556.Shown -= SubGraph_AddMessageWithPadSupport_Shown_556;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_560.Out -= SubGraph_AddMessageWithPadSupport_Out_560;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_560.Shown -= SubGraph_AddMessageWithPadSupport_Shown_560;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_567.Out -= SubGraph_AddMessageWithPadSupport_Out_567;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_567.Shown -= SubGraph_AddMessageWithPadSupport_Shown_567;
	}

	public void OnGUI()
	{
		logic_uScriptAct_PrintText_uScriptAct_PrintText_393.OnGUI();
	}

	private void Instance_SaveEvent_0(object o, EventArgs e)
	{
		Relay_SaveEvent_0();
	}

	private void Instance_LoadEvent_0(object o, EventArgs e)
	{
		Relay_LoadEvent_0();
	}

	private void Instance_RestartEvent_0(object o, EventArgs e)
	{
		Relay_RestartEvent_0();
	}

	private void Instance_OnUpdate_2(object o, EventArgs e)
	{
		Relay_OnUpdate_2();
	}

	private void Instance_OnSuspend_2(object o, EventArgs e)
	{
		Relay_OnSuspend_2();
	}

	private void Instance_OnResume_2(object o, EventArgs e)
	{
		Relay_OnResume_2();
	}

	private void uScriptCon_ManualSwitch_Output1_42(object o, EventArgs e)
	{
		Relay_Output1_42();
	}

	private void uScriptCon_ManualSwitch_Output2_42(object o, EventArgs e)
	{
		Relay_Output2_42();
	}

	private void uScriptCon_ManualSwitch_Output3_42(object o, EventArgs e)
	{
		Relay_Output3_42();
	}

	private void uScriptCon_ManualSwitch_Output4_42(object o, EventArgs e)
	{
		Relay_Output4_42();
	}

	private void uScriptCon_ManualSwitch_Output5_42(object o, EventArgs e)
	{
		Relay_Output5_42();
	}

	private void uScriptCon_ManualSwitch_Output6_42(object o, EventArgs e)
	{
		Relay_Output6_42();
	}

	private void uScriptCon_ManualSwitch_Output7_42(object o, EventArgs e)
	{
		Relay_Output7_42();
	}

	private void uScriptCon_ManualSwitch_Output8_42(object o, EventArgs e)
	{
		Relay_Output8_42();
	}

	private void SubGraph_LoadObjectiveStates_Out_47(object o, SubGraph_LoadObjectiveStates.LogicEventArgs e)
	{
		Relay_Out_47();
	}

	private void SubGraph_SaveLoadBool_Save_Out_50(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_50 = e.boolean;
		local_BeamSensorIsAttached_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_50;
		Relay_Save_Out_50();
	}

	private void SubGraph_SaveLoadBool_Load_Out_50(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_50 = e.boolean;
		local_BeamSensorIsAttached_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_50;
		Relay_Load_Out_50();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_50(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_50 = e.boolean;
		local_BeamSensorIsAttached_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_50;
		Relay_Restart_Out_50();
	}

	private void SubGraph_SaveLoadInt_Save_Out_52(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_52 = e.integer;
		local_NumWiresAttached_System_Int32 = logic_SubGraph_SaveLoadInt_integer_52;
		Relay_Save_Out_52();
	}

	private void SubGraph_SaveLoadInt_Load_Out_52(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_52 = e.integer;
		local_NumWiresAttached_System_Int32 = logic_SubGraph_SaveLoadInt_integer_52;
		Relay_Load_Out_52();
	}

	private void SubGraph_SaveLoadInt_Restart_Out_52(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_52 = e.integer;
		local_NumWiresAttached_System_Int32 = logic_SubGraph_SaveLoadInt_integer_52;
		Relay_Restart_Out_52();
	}

	private void SubGraph_SaveLoadBool_Save_Out_56(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_56 = e.boolean;
		local_BeamSensorIsSpawned_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_56;
		Relay_Save_Out_56();
	}

	private void SubGraph_SaveLoadBool_Load_Out_56(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_56 = e.boolean;
		local_BeamSensorIsSpawned_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_56;
		Relay_Load_Out_56();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_56(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_56 = e.boolean;
		local_BeamSensorIsSpawned_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_56;
		Relay_Restart_Out_56();
	}

	private void SubGraph_Crafting_Tutorial_AttachBlockToBase_Block_Attached_58(object o, SubGraph_Crafting_Tutorial_AttachBlockToBase.LogicEventArgs e)
	{
		Relay_Block_Attached_58();
	}

	private void SubGraph_SaveLoadBool_Save_Out_75(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_75 = e.boolean;
		local_CanInteractWithToggle_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_75;
		Relay_Save_Out_75();
	}

	private void SubGraph_SaveLoadBool_Load_Out_75(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_75 = e.boolean;
		local_CanInteractWithToggle_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_75;
		Relay_Load_Out_75();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_75(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_75 = e.boolean;
		local_CanInteractWithToggle_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_75;
		Relay_Restart_Out_75();
	}

	private void SubGraph_SaveLoadBool_Save_Out_82(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_82 = e.boolean;
		local_ToggleIsOn_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_82;
		Relay_Save_Out_82();
	}

	private void SubGraph_SaveLoadBool_Load_Out_82(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_82 = e.boolean;
		local_ToggleIsOn_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_82;
		Relay_Load_Out_82();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_82(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_82 = e.boolean;
		local_ToggleIsOn_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_82;
		Relay_Restart_Out_82();
	}

	private void SubGraph_SaveLoadBool_Save_Out_91(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_91 = e.boolean;
		local_BeamSensorIsOn_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_91;
		Relay_Save_Out_91();
	}

	private void SubGraph_SaveLoadBool_Load_Out_91(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_91 = e.boolean;
		local_BeamSensorIsOn_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_91;
		Relay_Load_Out_91();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_91(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_91 = e.boolean;
		local_BeamSensorIsOn_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_91;
		Relay_Restart_Out_91();
	}

	private void SubGraph_SaveLoadBool_Save_Out_105(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_105 = e.boolean;
		local_LightIsAttached_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_105;
		Relay_Save_Out_105();
	}

	private void SubGraph_SaveLoadBool_Load_Out_105(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_105 = e.boolean;
		local_LightIsAttached_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_105;
		Relay_Load_Out_105();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_105(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_105 = e.boolean;
		local_LightIsAttached_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_105;
		Relay_Restart_Out_105();
	}

	private void SubGraph_CompleteObjectiveStage_Out_107(object o, SubGraph_CompleteObjectiveStage.LogicEventArgs e)
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_107 = e.objectiveStage;
		local_Stage_System_Int32 = logic_SubGraph_CompleteObjectiveStage_objectiveStage_107;
		Relay_Out_107();
	}

	private void SubGraph_SaveLoadInt_Save_Out_112(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_112 = e.integer;
		local_Stage_System_Int32 = logic_SubGraph_SaveLoadInt_integer_112;
		Relay_Save_Out_112();
	}

	private void SubGraph_SaveLoadInt_Load_Out_112(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_112 = e.integer;
		local_Stage_System_Int32 = logic_SubGraph_SaveLoadInt_integer_112;
		Relay_Load_Out_112();
	}

	private void SubGraph_SaveLoadInt_Restart_Out_112(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_112 = e.integer;
		local_Stage_System_Int32 = logic_SubGraph_SaveLoadInt_integer_112;
		Relay_Restart_Out_112();
	}

	private void SubGraph_CompleteObjectiveStage_Out_123(object o, SubGraph_CompleteObjectiveStage.LogicEventArgs e)
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_123 = e.objectiveStage;
		local_Stage_System_Int32 = logic_SubGraph_CompleteObjectiveStage_objectiveStage_123;
		Relay_Out_123();
	}

	private void SubGraph_Crafting_Tutorial_AttachBlockToBase_Block_Attached_128(object o, SubGraph_Crafting_Tutorial_AttachBlockToBase.LogicEventArgs e)
	{
		Relay_Block_Attached_128();
	}

	private void SubGraph_CompleteObjectiveStage_Out_142(object o, SubGraph_CompleteObjectiveStage.LogicEventArgs e)
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_142 = e.objectiveStage;
		local_Stage_System_Int32 = logic_SubGraph_CompleteObjectiveStage_objectiveStage_142;
		Relay_Out_142();
	}

	private void SubGraph_SaveLoadBool_Save_Out_143(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_143 = e.boolean;
		local_msgIntroShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_143;
		Relay_Save_Out_143();
	}

	private void SubGraph_SaveLoadBool_Load_Out_143(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_143 = e.boolean;
		local_msgIntroShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_143;
		Relay_Load_Out_143();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_143(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_143 = e.boolean;
		local_msgIntroShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_143;
		Relay_Restart_Out_143();
	}

	private void SubGraph_SaveLoadBool_Save_Out_173(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_173 = e.boolean;
		local_msgBaseFoundShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_173;
		Relay_Save_Out_173();
	}

	private void SubGraph_SaveLoadBool_Load_Out_173(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_173 = e.boolean;
		local_msgBaseFoundShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_173;
		Relay_Load_Out_173();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_173(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_173 = e.boolean;
		local_msgBaseFoundShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_173;
		Relay_Restart_Out_173();
	}

	private void SubGraph_SaveLoadBool_Save_Out_193(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_193 = e.boolean;
		local_msgWiresAttachedShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_193;
		Relay_Save_Out_193();
	}

	private void SubGraph_SaveLoadBool_Load_Out_193(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_193 = e.boolean;
		local_msgWiresAttachedShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_193;
		Relay_Load_Out_193();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_193(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_193 = e.boolean;
		local_msgWiresAttachedShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_193;
		Relay_Restart_Out_193();
	}

	private void SubGraph_SaveLoadBool_Save_Out_194(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_194 = e.boolean;
		local_msgLightAttachedShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_194;
		Relay_Save_Out_194();
	}

	private void SubGraph_SaveLoadBool_Load_Out_194(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_194 = e.boolean;
		local_msgLightAttachedShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_194;
		Relay_Load_Out_194();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_194(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_194 = e.boolean;
		local_msgLightAttachedShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_194;
		Relay_Restart_Out_194();
	}

	private void SubGraph_SaveLoadBool_Save_Out_203(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_203 = e.boolean;
		local_msgToggleIsOnShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_203;
		Relay_Save_Out_203();
	}

	private void SubGraph_SaveLoadBool_Load_Out_203(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_203 = e.boolean;
		local_msgToggleIsOnShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_203;
		Relay_Load_Out_203();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_203(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_203 = e.boolean;
		local_msgToggleIsOnShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_203;
		Relay_Restart_Out_203();
	}

	private void uScriptCon_ManualSwitch_Output1_279(object o, EventArgs e)
	{
		Relay_Output1_279();
	}

	private void uScriptCon_ManualSwitch_Output2_279(object o, EventArgs e)
	{
		Relay_Output2_279();
	}

	private void uScriptCon_ManualSwitch_Output3_279(object o, EventArgs e)
	{
		Relay_Output3_279();
	}

	private void uScriptCon_ManualSwitch_Output4_279(object o, EventArgs e)
	{
		Relay_Output4_279();
	}

	private void uScriptCon_ManualSwitch_Output5_279(object o, EventArgs e)
	{
		Relay_Output5_279();
	}

	private void uScriptCon_ManualSwitch_Output6_279(object o, EventArgs e)
	{
		Relay_Output6_279();
	}

	private void uScriptCon_ManualSwitch_Output7_279(object o, EventArgs e)
	{
		Relay_Output7_279();
	}

	private void uScriptCon_ManualSwitch_Output8_279(object o, EventArgs e)
	{
		Relay_Output8_279();
	}

	private void uScriptCon_ManualSwitch_Output1_282(object o, EventArgs e)
	{
		Relay_Output1_282();
	}

	private void uScriptCon_ManualSwitch_Output2_282(object o, EventArgs e)
	{
		Relay_Output2_282();
	}

	private void uScriptCon_ManualSwitch_Output3_282(object o, EventArgs e)
	{
		Relay_Output3_282();
	}

	private void uScriptCon_ManualSwitch_Output4_282(object o, EventArgs e)
	{
		Relay_Output4_282();
	}

	private void uScriptCon_ManualSwitch_Output5_282(object o, EventArgs e)
	{
		Relay_Output5_282();
	}

	private void uScriptCon_ManualSwitch_Output6_282(object o, EventArgs e)
	{
		Relay_Output6_282();
	}

	private void uScriptCon_ManualSwitch_Output7_282(object o, EventArgs e)
	{
		Relay_Output7_282();
	}

	private void uScriptCon_ManualSwitch_Output8_282(object o, EventArgs e)
	{
		Relay_Output8_282();
	}

	private void SubGraph_SaveLoadInt_Save_Out_354(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_354 = e.integer;
		local_ToggleInteractions_System_Int32 = logic_SubGraph_SaveLoadInt_integer_354;
		Relay_Save_Out_354();
	}

	private void SubGraph_SaveLoadInt_Load_Out_354(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_354 = e.integer;
		local_ToggleInteractions_System_Int32 = logic_SubGraph_SaveLoadInt_integer_354;
		Relay_Load_Out_354();
	}

	private void SubGraph_SaveLoadInt_Restart_Out_354(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_354 = e.integer;
		local_ToggleInteractions_System_Int32 = logic_SubGraph_SaveLoadInt_integer_354;
		Relay_Restart_Out_354();
	}

	private void uScriptCon_ManualSwitch_Output1_358(object o, EventArgs e)
	{
		Relay_Output1_358();
	}

	private void uScriptCon_ManualSwitch_Output2_358(object o, EventArgs e)
	{
		Relay_Output2_358();
	}

	private void uScriptCon_ManualSwitch_Output3_358(object o, EventArgs e)
	{
		Relay_Output3_358();
	}

	private void uScriptCon_ManualSwitch_Output4_358(object o, EventArgs e)
	{
		Relay_Output4_358();
	}

	private void uScriptCon_ManualSwitch_Output5_358(object o, EventArgs e)
	{
		Relay_Output5_358();
	}

	private void uScriptCon_ManualSwitch_Output6_358(object o, EventArgs e)
	{
		Relay_Output6_358();
	}

	private void uScriptCon_ManualSwitch_Output7_358(object o, EventArgs e)
	{
		Relay_Output7_358();
	}

	private void uScriptCon_ManualSwitch_Output8_358(object o, EventArgs e)
	{
		Relay_Output8_358();
	}

	private void SubGraph_SaveLoadInt_Save_Out_380(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_380 = e.integer;
		local_StartingHour_System_Int32 = logic_SubGraph_SaveLoadInt_integer_380;
		Relay_Save_Out_380();
	}

	private void SubGraph_SaveLoadInt_Load_Out_380(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_380 = e.integer;
		local_StartingHour_System_Int32 = logic_SubGraph_SaveLoadInt_integer_380;
		Relay_Load_Out_380();
	}

	private void SubGraph_SaveLoadInt_Restart_Out_380(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_380 = e.integer;
		local_StartingHour_System_Int32 = logic_SubGraph_SaveLoadInt_integer_380;
		Relay_Restart_Out_380();
	}

	private void SubGraph_SaveLoadBool_Save_Out_383(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_383 = e.boolean;
		local_msgToggleOffShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_383;
		Relay_Save_Out_383();
	}

	private void SubGraph_SaveLoadBool_Load_Out_383(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_383 = e.boolean;
		local_msgToggleOffShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_383;
		Relay_Load_Out_383();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_383(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_383 = e.boolean;
		local_msgToggleOffShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_383;
		Relay_Restart_Out_383();
	}

	private void SubGraph_SaveLoadBool_Save_Out_387(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_387 = e.boolean;
		local_msgCompleteIsOnShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_387;
		Relay_Save_Out_387();
	}

	private void SubGraph_SaveLoadBool_Load_Out_387(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_387 = e.boolean;
		local_msgCompleteIsOnShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_387;
		Relay_Load_Out_387();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_387(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_387 = e.boolean;
		local_msgCompleteIsOnShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_387;
		Relay_Restart_Out_387();
	}

	private void SubGraph_SaveLoadBool_Save_Out_389(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_389 = e.boolean;
		local_msgBeamSensorAttachedIsOnShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_389;
		Relay_Save_Out_389();
	}

	private void SubGraph_SaveLoadBool_Load_Out_389(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_389 = e.boolean;
		local_msgBeamSensorAttachedIsOnShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_389;
		Relay_Load_Out_389();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_389(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_389 = e.boolean;
		local_msgBeamSensorAttachedIsOnShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_389;
		Relay_Restart_Out_389();
	}

	private void SubGraph_Crafting_Tutorial_Init_Out_443(object o, SubGraph_Crafting_Tutorial_Init.LogicEventArgs e)
	{
		logic_SubGraph_Crafting_Tutorial_Init_CraftingBaseTech_443 = e.CraftingBaseTech;
		logic_SubGraph_Crafting_Tutorial_Init_NPCTech_443 = e.NPCTech;
		local_CircuitBaseTech_Tank = logic_SubGraph_Crafting_Tutorial_Init_CraftingBaseTech_443;
		local_NPCTech_Tank = logic_SubGraph_Crafting_Tutorial_Init_NPCTech_443;
		Relay_Out_443();
	}

	private void SubGraph_Crafting_Tutorial_ManageBlock_Out_446(object o, SubGraph_Crafting_Tutorial_ManageBlock.LogicEventArgs e)
	{
		logic_SubGraph_Crafting_Tutorial_ManageBlock_block_446 = e.block;
		BlockSpawnData = logic_SubGraph_Crafting_Tutorial_ManageBlock_blockSpawnData_446;
		local_Wire1Block_TankBlock = logic_SubGraph_Crafting_Tutorial_ManageBlock_block_446;
		Relay_Out_446();
	}

	private void SubGraph_Crafting_Tutorial_ManageBlock_Out_449(object o, SubGraph_Crafting_Tutorial_ManageBlock.LogicEventArgs e)
	{
		logic_SubGraph_Crafting_Tutorial_ManageBlock_block_449 = e.block;
		BeamSensorBlockSpawnData = logic_SubGraph_Crafting_Tutorial_ManageBlock_blockSpawnData_449;
		local_BeamSensorBlock_TankBlock = logic_SubGraph_Crafting_Tutorial_ManageBlock_block_449;
		Relay_Out_449();
	}

	private void SubGraph_Crafting_Tutorial_ManageBlock_Out_464(object o, SubGraph_Crafting_Tutorial_ManageBlock.LogicEventArgs e)
	{
		logic_SubGraph_Crafting_Tutorial_ManageBlock_block_464 = e.block;
		BlockSpawnData = logic_SubGraph_Crafting_Tutorial_ManageBlock_blockSpawnData_464;
		local_Wire3Block_TankBlock = logic_SubGraph_Crafting_Tutorial_ManageBlock_block_464;
		Relay_Out_464();
	}

	private void SubGraph_Crafting_Tutorial_ManageBlock_Out_469(object o, SubGraph_Crafting_Tutorial_ManageBlock.LogicEventArgs e)
	{
		logic_SubGraph_Crafting_Tutorial_ManageBlock_block_469 = e.block;
		BlockSpawnData = logic_SubGraph_Crafting_Tutorial_ManageBlock_blockSpawnData_469;
		local_LightBlock_TankBlock = logic_SubGraph_Crafting_Tutorial_ManageBlock_block_469;
		Relay_Out_469();
	}

	private void SubGraph_Crafting_Tutorial_ManageBlock_Out_476(object o, SubGraph_Crafting_Tutorial_ManageBlock.LogicEventArgs e)
	{
		logic_SubGraph_Crafting_Tutorial_ManageBlock_block_476 = e.block;
		BlockSpawnData = logic_SubGraph_Crafting_Tutorial_ManageBlock_blockSpawnData_476;
		local_Wire2Block_TankBlock = logic_SubGraph_Crafting_Tutorial_ManageBlock_block_476;
		Relay_Out_476();
	}

	private void SubGraph_SaveLoadBool_Save_Out_482(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_482 = e.boolean;
		local_TechsInit_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_482;
		Relay_Save_Out_482();
	}

	private void SubGraph_SaveLoadBool_Load_Out_482(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_482 = e.boolean;
		local_TechsInit_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_482;
		Relay_Load_Out_482();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_482(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_482 = e.boolean;
		local_TechsInit_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_482;
		Relay_Restart_Out_482();
	}

	private void SubGraph_SaveLoadInt_Save_Out_508(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_508 = e.integer;
		local_SubStage2_System_Int32 = logic_SubGraph_SaveLoadInt_integer_508;
		Relay_Save_Out_508();
	}

	private void SubGraph_SaveLoadInt_Load_Out_508(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_508 = e.integer;
		local_SubStage2_System_Int32 = logic_SubGraph_SaveLoadInt_integer_508;
		Relay_Load_Out_508();
	}

	private void SubGraph_SaveLoadInt_Restart_Out_508(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_508 = e.integer;
		local_SubStage2_System_Int32 = logic_SubGraph_SaveLoadInt_integer_508;
		Relay_Restart_Out_508();
	}

	private void uScriptCon_ManualSwitch_Output1_510(object o, EventArgs e)
	{
		Relay_Output1_510();
	}

	private void uScriptCon_ManualSwitch_Output2_510(object o, EventArgs e)
	{
		Relay_Output2_510();
	}

	private void uScriptCon_ManualSwitch_Output3_510(object o, EventArgs e)
	{
		Relay_Output3_510();
	}

	private void uScriptCon_ManualSwitch_Output4_510(object o, EventArgs e)
	{
		Relay_Output4_510();
	}

	private void uScriptCon_ManualSwitch_Output5_510(object o, EventArgs e)
	{
		Relay_Output5_510();
	}

	private void uScriptCon_ManualSwitch_Output6_510(object o, EventArgs e)
	{
		Relay_Output6_510();
	}

	private void uScriptCon_ManualSwitch_Output7_510(object o, EventArgs e)
	{
		Relay_Output7_510();
	}

	private void uScriptCon_ManualSwitch_Output8_510(object o, EventArgs e)
	{
		Relay_Output8_510();
	}

	private void uScriptCon_ManualSwitch_Output1_514(object o, EventArgs e)
	{
		Relay_Output1_514();
	}

	private void uScriptCon_ManualSwitch_Output2_514(object o, EventArgs e)
	{
		Relay_Output2_514();
	}

	private void uScriptCon_ManualSwitch_Output3_514(object o, EventArgs e)
	{
		Relay_Output3_514();
	}

	private void uScriptCon_ManualSwitch_Output4_514(object o, EventArgs e)
	{
		Relay_Output4_514();
	}

	private void uScriptCon_ManualSwitch_Output5_514(object o, EventArgs e)
	{
		Relay_Output5_514();
	}

	private void uScriptCon_ManualSwitch_Output6_514(object o, EventArgs e)
	{
		Relay_Output6_514();
	}

	private void uScriptCon_ManualSwitch_Output7_514(object o, EventArgs e)
	{
		Relay_Output7_514();
	}

	private void uScriptCon_ManualSwitch_Output8_514(object o, EventArgs e)
	{
		Relay_Output8_514();
	}

	private void SubGraph_Crafting_Tutorial_Finish_Out_527(object o, SubGraph_Crafting_Tutorial_Finish.LogicEventArgs e)
	{
		Relay_Out_527();
	}

	private void SubGraph_SaveLoadInt_Save_Out_554(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_554 = e.integer;
		local_SubStage3_System_Int32 = logic_SubGraph_SaveLoadInt_integer_554;
		Relay_Save_Out_554();
	}

	private void SubGraph_SaveLoadInt_Load_Out_554(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_554 = e.integer;
		local_SubStage3_System_Int32 = logic_SubGraph_SaveLoadInt_integer_554;
		Relay_Load_Out_554();
	}

	private void SubGraph_SaveLoadInt_Restart_Out_554(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_554 = e.integer;
		local_SubStage3_System_Int32 = logic_SubGraph_SaveLoadInt_integer_554;
		Relay_Restart_Out_554();
	}

	private void SubGraph_AddMessageWithPadSupport_Out_556(object o, SubGraph_AddMessageWithPadSupport.LogicEventArgs e)
	{
		logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboardReturn_556 = e.messageMouseKeyboardReturn;
		logic_SubGraph_AddMessageWithPadSupport_messageControlPadReturn_556 = e.messageControlPadReturn;
		Relay_Out_556();
	}

	private void SubGraph_AddMessageWithPadSupport_Shown_556(object o, SubGraph_AddMessageWithPadSupport.LogicEventArgs e)
	{
		logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboardReturn_556 = e.messageMouseKeyboardReturn;
		logic_SubGraph_AddMessageWithPadSupport_messageControlPadReturn_556 = e.messageControlPadReturn;
		Relay_Shown_556();
	}

	private void SubGraph_AddMessageWithPadSupport_Out_560(object o, SubGraph_AddMessageWithPadSupport.LogicEventArgs e)
	{
		logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboardReturn_560 = e.messageMouseKeyboardReturn;
		logic_SubGraph_AddMessageWithPadSupport_messageControlPadReturn_560 = e.messageControlPadReturn;
		Relay_Out_560();
	}

	private void SubGraph_AddMessageWithPadSupport_Shown_560(object o, SubGraph_AddMessageWithPadSupport.LogicEventArgs e)
	{
		logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboardReturn_560 = e.messageMouseKeyboardReturn;
		logic_SubGraph_AddMessageWithPadSupport_messageControlPadReturn_560 = e.messageControlPadReturn;
		Relay_Shown_560();
	}

	private void SubGraph_AddMessageWithPadSupport_Out_567(object o, SubGraph_AddMessageWithPadSupport.LogicEventArgs e)
	{
		logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboardReturn_567 = e.messageMouseKeyboardReturn;
		logic_SubGraph_AddMessageWithPadSupport_messageControlPadReturn_567 = e.messageControlPadReturn;
		Relay_Out_567();
	}

	private void SubGraph_AddMessageWithPadSupport_Shown_567(object o, SubGraph_AddMessageWithPadSupport.LogicEventArgs e)
	{
		logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboardReturn_567 = e.messageMouseKeyboardReturn;
		logic_SubGraph_AddMessageWithPadSupport_messageControlPadReturn_567 = e.messageControlPadReturn;
		Relay_Shown_567();
	}

	private void Relay_SaveEvent_0()
	{
		Relay_Save_52();
	}

	private void Relay_LoadEvent_0()
	{
		Relay_Load_52();
	}

	private void Relay_RestartEvent_0()
	{
		Relay_Restart_52();
	}

	private void Relay_OnUpdate_2()
	{
		Relay_In_443();
	}

	private void Relay_OnSuspend_2()
	{
	}

	private void Relay_OnResume_2()
	{
	}

	private void Relay_In_5()
	{
		logic_uScriptCon_CompareBool_Bool_5 = local_BeamSensorIsSpawned_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_5.In(logic_uScriptCon_CompareBool_Bool_5);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_5.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_5.False;
		if (num)
		{
			Relay_In_20();
		}
		if (flag)
		{
			Relay_In_13();
		}
	}

	private void Relay_Pause_7()
	{
		logic_uScript_PausePopulation_uScript_PausePopulation_7.Pause();
		if (logic_uScript_PausePopulation_uScript_PausePopulation_7.Out)
		{
			Relay_In_24();
		}
	}

	private void Relay_UnPause_7()
	{
		logic_uScript_PausePopulation_uScript_PausePopulation_7.UnPause();
		if (logic_uScript_PausePopulation_uScript_PausePopulation_7.Out)
		{
			Relay_In_24();
		}
	}

	private void Relay_In_8()
	{
		logic_uScript_SetEncounterTarget_owner_8 = owner_Connection_35;
		logic_uScript_SetEncounterTarget_visibleObject_8 = local_NPCTech_Tank;
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_8.In(logic_uScript_SetEncounterTarget_owner_8, logic_uScript_SetEncounterTarget_visibleObject_8);
		if (logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_8.Out)
		{
			Relay_In_27();
		}
	}

	private void Relay_In_10()
	{
		logic_uScriptCon_CompareBool_Bool_10 = local_msgIntroShown_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_10.In(logic_uScriptCon_CompareBool_Bool_10);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_10.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_10.False;
		if (num)
		{
			Relay_In_19();
		}
		if (flag)
		{
			Relay_In_28();
		}
	}

	private void Relay_In_13()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_13.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_13.Out)
		{
			Relay_In_25();
		}
	}

	private void Relay_Pause_14()
	{
		logic_uScript_PausePopulation_uScript_PausePopulation_14.Pause();
		if (logic_uScript_PausePopulation_uScript_PausePopulation_14.Out)
		{
			Relay_In_74();
		}
	}

	private void Relay_UnPause_14()
	{
		logic_uScript_PausePopulation_uScript_PausePopulation_14.UnPause();
		if (logic_uScript_PausePopulation_uScript_PausePopulation_14.Out)
		{
			Relay_In_74();
		}
	}

	private void Relay_True_16()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_16.True(out logic_uScriptAct_SetBool_Target_16);
		local_NearBase_System_Boolean = logic_uScriptAct_SetBool_Target_16;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_16.Out)
		{
			Relay_In_42();
		}
	}

	private void Relay_False_16()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_16.False(out logic_uScriptAct_SetBool_Target_16);
		local_NearBase_System_Boolean = logic_uScriptAct_SetBool_Target_16;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_16.Out)
		{
			Relay_In_42();
		}
	}

	private void Relay_In_17()
	{
		logic_uScript_LockTechInteraction_tech_17 = local_CircuitBaseTech_Tank;
		logic_uScript_LockTechInteraction_uScript_LockTechInteraction_17.In(logic_uScript_LockTechInteraction_tech_17, logic_uScript_LockTechInteraction_excludedBlocks_17, logic_uScript_LockTechInteraction_excludedUniqueBlocks_17);
		if (logic_uScript_LockTechInteraction_uScript_LockTechInteraction_17.Out)
		{
			Relay_In_439();
		}
	}

	private void Relay_In_19()
	{
		logic_uScript_IsPlayerInRangeOfTech_tech_19 = local_CircuitBaseTech_Tank;
		logic_uScript_IsPlayerInRangeOfTech_range_19 = distBaseFound;
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_19.In(logic_uScript_IsPlayerInRangeOfTech_tech_19, logic_uScript_IsPlayerInRangeOfTech_range_19, logic_uScript_IsPlayerInRangeOfTech_techs_19);
		bool inRange = logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_19.InRange;
		bool outOfRange = logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_19.OutOfRange;
		if (inRange)
		{
			Relay_Pause_14();
		}
		if (outOfRange)
		{
			Relay_UnPause_7();
		}
	}

	private void Relay_In_20()
	{
		logic_uScript_EnableGlow_targetObject_20 = local_BeamSensorBlock_TankBlock;
		logic_uScript_EnableGlow_uScript_EnableGlow_20.In(logic_uScript_EnableGlow_targetObject_20, logic_uScript_EnableGlow_enable_20);
		if (logic_uScript_EnableGlow_uScript_EnableGlow_20.Out)
		{
			Relay_In_25();
		}
	}

	private void Relay_In_21()
	{
		logic_uScript_AddMessage_messageData_21 = msgLeavingMissionArea;
		logic_uScript_AddMessage_speaker_21 = messageSpeaker;
		logic_uScript_AddMessage_Return_21 = logic_uScript_AddMessage_uScript_AddMessage_21.In(logic_uScript_AddMessage_messageData_21, logic_uScript_AddMessage_speaker_21);
		if (logic_uScript_AddMessage_uScript_AddMessage_21.Out)
		{
			Relay_False_22();
		}
	}

	private void Relay_True_22()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_22.True(out logic_uScriptAct_SetBool_Target_22);
		local_NearBase_System_Boolean = logic_uScriptAct_SetBool_Target_22;
	}

	private void Relay_False_22()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_22.False(out logic_uScriptAct_SetBool_Target_22);
		local_NearBase_System_Boolean = logic_uScriptAct_SetBool_Target_22;
	}

	private void Relay_In_23()
	{
		logic_uScript_HideArrow_uScript_HideArrow_23.In();
		if (logic_uScript_HideArrow_uScript_HideArrow_23.Out)
		{
			Relay_In_92();
		}
	}

	private void Relay_In_24()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_24 = messageTag;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_24.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_24, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_24);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_24.Out)
		{
			Relay_In_23();
		}
	}

	private void Relay_In_25()
	{
		logic_uScriptCon_CompareBool_Bool_25 = local_NearBase_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_25.In(logic_uScriptCon_CompareBool_Bool_25);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_25.True)
		{
			Relay_In_21();
		}
	}

	private void Relay_In_27()
	{
		logic_uScript_IsPlayerInRangeOfTech_tech_27 = local_CircuitBaseTech_Tank;
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_27.In(logic_uScript_IsPlayerInRangeOfTech_tech_27, logic_uScript_IsPlayerInRangeOfTech_range_27, logic_uScript_IsPlayerInRangeOfTech_techs_27);
		if (logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_27.InRange)
		{
			Relay_In_10();
		}
	}

	private void Relay_In_28()
	{
		logic_uScript_AddMessage_messageData_28 = msg01Intro;
		logic_uScript_AddMessage_speaker_28 = messageSpeaker;
		logic_uScript_AddMessage_Return_28 = logic_uScript_AddMessage_uScript_AddMessage_28.In(logic_uScript_AddMessage_messageData_28, logic_uScript_AddMessage_speaker_28);
		if (logic_uScript_AddMessage_uScript_AddMessage_28.Shown)
		{
			Relay_True_385();
		}
	}

	private void Relay_Output1_42()
	{
		Relay_In_171();
	}

	private void Relay_Output2_42()
	{
		Relay_In_510();
	}

	private void Relay_Output3_42()
	{
		Relay_In_355();
	}

	private void Relay_Output4_42()
	{
		Relay_In_514();
	}

	private void Relay_Output5_42()
	{
	}

	private void Relay_Output6_42()
	{
	}

	private void Relay_Output7_42()
	{
	}

	private void Relay_Output8_42()
	{
	}

	private void Relay_In_42()
	{
		logic_uScriptCon_ManualSwitch_CurrentOutput_42 = local_Stage_System_Int32;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_42.In(logic_uScriptCon_ManualSwitch_CurrentOutput_42);
	}

	private void Relay_True_45()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_45.True(out logic_uScriptAct_SetBool_Target_45);
		local_NearBase_System_Boolean = logic_uScriptAct_SetBool_Target_45;
	}

	private void Relay_False_45()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_45.False(out logic_uScriptAct_SetBool_Target_45);
		local_NearBase_System_Boolean = logic_uScriptAct_SetBool_Target_45;
	}

	private void Relay_Out_47()
	{
		Relay_In_480();
	}

	private void Relay_In_47()
	{
		logic_SubGraph_LoadObjectiveStates_currentObjective_47 = local_Stage_System_Int32;
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_47.In(logic_SubGraph_LoadObjectiveStates_currentObjective_47);
	}

	private void Relay_Save_Out_50()
	{
		Relay_Save_91();
	}

	private void Relay_Load_Out_50()
	{
		Relay_Load_91();
	}

	private void Relay_Restart_Out_50()
	{
		Relay_Set_False_91();
	}

	private void Relay_Save_50()
	{
		logic_SubGraph_SaveLoadBool_boolean_50 = local_BeamSensorIsAttached_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_50 = local_BeamSensorIsAttached_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_50.Save(ref logic_SubGraph_SaveLoadBool_boolean_50, logic_SubGraph_SaveLoadBool_boolAsVariable_50, logic_SubGraph_SaveLoadBool_uniqueID_50);
	}

	private void Relay_Load_50()
	{
		logic_SubGraph_SaveLoadBool_boolean_50 = local_BeamSensorIsAttached_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_50 = local_BeamSensorIsAttached_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_50.Load(ref logic_SubGraph_SaveLoadBool_boolean_50, logic_SubGraph_SaveLoadBool_boolAsVariable_50, logic_SubGraph_SaveLoadBool_uniqueID_50);
	}

	private void Relay_Set_True_50()
	{
		logic_SubGraph_SaveLoadBool_boolean_50 = local_BeamSensorIsAttached_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_50 = local_BeamSensorIsAttached_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_50.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_50, logic_SubGraph_SaveLoadBool_boolAsVariable_50, logic_SubGraph_SaveLoadBool_uniqueID_50);
	}

	private void Relay_Set_False_50()
	{
		logic_SubGraph_SaveLoadBool_boolean_50 = local_BeamSensorIsAttached_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_50 = local_BeamSensorIsAttached_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_50.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_50, logic_SubGraph_SaveLoadBool_boolAsVariable_50, logic_SubGraph_SaveLoadBool_uniqueID_50);
	}

	private void Relay_Save_Out_52()
	{
		Relay_Save_112();
	}

	private void Relay_Load_Out_52()
	{
		Relay_Load_112();
	}

	private void Relay_Restart_Out_52()
	{
		Relay_Restart_112();
	}

	private void Relay_Save_52()
	{
		logic_SubGraph_SaveLoadInt_integer_52 = local_NumWiresAttached_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_52 = local_NumWiresAttached_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_52.Save(logic_SubGraph_SaveLoadInt_restartValue_52, ref logic_SubGraph_SaveLoadInt_integer_52, logic_SubGraph_SaveLoadInt_intAsVariable_52, logic_SubGraph_SaveLoadInt_uniqueID_52);
	}

	private void Relay_Load_52()
	{
		logic_SubGraph_SaveLoadInt_integer_52 = local_NumWiresAttached_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_52 = local_NumWiresAttached_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_52.Load(logic_SubGraph_SaveLoadInt_restartValue_52, ref logic_SubGraph_SaveLoadInt_integer_52, logic_SubGraph_SaveLoadInt_intAsVariable_52, logic_SubGraph_SaveLoadInt_uniqueID_52);
	}

	private void Relay_Restart_52()
	{
		logic_SubGraph_SaveLoadInt_integer_52 = local_NumWiresAttached_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_52 = local_NumWiresAttached_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_52.Restart(logic_SubGraph_SaveLoadInt_restartValue_52, ref logic_SubGraph_SaveLoadInt_integer_52, logic_SubGraph_SaveLoadInt_intAsVariable_52, logic_SubGraph_SaveLoadInt_uniqueID_52);
	}

	private void Relay_Save_Out_56()
	{
		Relay_Save_50();
	}

	private void Relay_Load_Out_56()
	{
		Relay_Load_50();
	}

	private void Relay_Restart_Out_56()
	{
		Relay_Set_False_50();
	}

	private void Relay_Save_56()
	{
		logic_SubGraph_SaveLoadBool_boolean_56 = local_BeamSensorIsSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_56 = local_BeamSensorIsSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_56.Save(ref logic_SubGraph_SaveLoadBool_boolean_56, logic_SubGraph_SaveLoadBool_boolAsVariable_56, logic_SubGraph_SaveLoadBool_uniqueID_56);
	}

	private void Relay_Load_56()
	{
		logic_SubGraph_SaveLoadBool_boolean_56 = local_BeamSensorIsSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_56 = local_BeamSensorIsSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_56.Load(ref logic_SubGraph_SaveLoadBool_boolean_56, logic_SubGraph_SaveLoadBool_boolAsVariable_56, logic_SubGraph_SaveLoadBool_uniqueID_56);
	}

	private void Relay_Set_True_56()
	{
		logic_SubGraph_SaveLoadBool_boolean_56 = local_BeamSensorIsSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_56 = local_BeamSensorIsSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_56.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_56, logic_SubGraph_SaveLoadBool_boolAsVariable_56, logic_SubGraph_SaveLoadBool_uniqueID_56);
	}

	private void Relay_Set_False_56()
	{
		logic_SubGraph_SaveLoadBool_boolean_56 = local_BeamSensorIsSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_56 = local_BeamSensorIsSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_56.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_56, logic_SubGraph_SaveLoadBool_boolAsVariable_56, logic_SubGraph_SaveLoadBool_uniqueID_56);
	}

	private void Relay_Block_Attached_58()
	{
		Relay_In_62();
	}

	private void Relay_In_58()
	{
		logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_Block_58 = local_BeamSensorBlock_TankBlock;
		logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_CraftingBaseTech_58 = local_CircuitBaseTech_Tank;
		int num = 0;
		Array ghostBlockBeamSensor = GhostBlockBeamSensor;
		if (logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_ghostBlockData_58.Length != num + ghostBlockBeamSensor.Length)
		{
			Array.Resize(ref logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_ghostBlockData_58, num + ghostBlockBeamSensor.Length);
		}
		Array.Copy(ghostBlockBeamSensor, 0, logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_ghostBlockData_58, num, ghostBlockBeamSensor.Length);
		num += ghostBlockBeamSensor.Length;
		logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_BlockType_58 = blockTypeBeamSensor;
		logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_SubGraph_Crafting_Tutorial_AttachBlockToBase_58.In(logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_Block_58, logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_CraftingBaseTech_58, logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_ghostBlockData_58, logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_BlockType_58, logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_BlockPosition_58);
	}

	private void Relay_In_59()
	{
		logic_uScriptCon_CompareBool_Bool_59 = local_BeamSensorIsSpawned_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_59.In(logic_uScriptCon_CompareBool_Bool_59);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_59.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_59.False;
		if (num)
		{
			Relay_In_63();
		}
		if (flag)
		{
			Relay_True_69();
		}
	}

	private void Relay_In_60()
	{
		logic_uScript_HideArrow_uScript_HideArrow_60.In();
		if (logic_uScript_HideArrow_uScript_HideArrow_60.Out)
		{
			Relay_True_211();
		}
	}

	private void Relay_In_62()
	{
		logic_uScript_RemoveAllGhostBlocksOnTech_targetTech_62 = local_CircuitBaseTech_Tank;
		logic_uScript_RemoveAllGhostBlocksOnTech_uScript_RemoveAllGhostBlocksOnTech_62.In(logic_uScript_RemoveAllGhostBlocksOnTech_targetTech_62);
		if (logic_uScript_RemoveAllGhostBlocksOnTech_uScript_RemoveAllGhostBlocksOnTech_62.Out)
		{
			Relay_In_60();
		}
	}

	private void Relay_In_63()
	{
		int num = 0;
		Array beamSensorBlockSpawnData = BeamSensorBlockSpawnData;
		if (logic_uScript_GetAndCheckBlocks_blockData_63.Length != num + beamSensorBlockSpawnData.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckBlocks_blockData_63, num + beamSensorBlockSpawnData.Length);
		}
		Array.Copy(beamSensorBlockSpawnData, 0, logic_uScript_GetAndCheckBlocks_blockData_63, num, beamSensorBlockSpawnData.Length);
		num += beamSensorBlockSpawnData.Length;
		logic_uScript_GetAndCheckBlocks_ownerNode_63 = owner_Connection_72;
		logic_uScript_GetAndCheckBlocks_uScript_GetAndCheckBlocks_63.In(logic_uScript_GetAndCheckBlocks_blockData_63, logic_uScript_GetAndCheckBlocks_ownerNode_63, ref logic_uScript_GetAndCheckBlocks_blocks_63);
		bool allAlive = logic_uScript_GetAndCheckBlocks_uScript_GetAndCheckBlocks_63.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckBlocks_uScript_GetAndCheckBlocks_63.SomeAlive;
		if (allAlive)
		{
			Relay_In_206();
		}
		if (someAlive)
		{
			Relay_In_206();
		}
	}

	private void Relay_In_66()
	{
		int num = 0;
		Array beamSensorBlockSpawnData = BeamSensorBlockSpawnData;
		if (logic_uScript_SpawnBlocksFromData_blockData_66.Length != num + beamSensorBlockSpawnData.Length)
		{
			Array.Resize(ref logic_uScript_SpawnBlocksFromData_blockData_66, num + beamSensorBlockSpawnData.Length);
		}
		Array.Copy(beamSensorBlockSpawnData, 0, logic_uScript_SpawnBlocksFromData_blockData_66, num, beamSensorBlockSpawnData.Length);
		num += beamSensorBlockSpawnData.Length;
		logic_uScript_SpawnBlocksFromData_ownerNode_66 = owner_Connection_67;
		logic_uScript_SpawnBlocksFromData_uScript_SpawnBlocksFromData_66.In(logic_uScript_SpawnBlocksFromData_blockData_66, logic_uScript_SpawnBlocksFromData_ownerNode_66);
		if (logic_uScript_SpawnBlocksFromData_uScript_SpawnBlocksFromData_66.Out)
		{
			Relay_In_63();
		}
	}

	private void Relay_True_69()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_69.True(out logic_uScriptAct_SetBool_Target_69);
		local_BeamSensorIsSpawned_System_Boolean = logic_uScriptAct_SetBool_Target_69;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_69.Out)
		{
			Relay_In_66();
		}
	}

	private void Relay_False_69()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_69.False(out logic_uScriptAct_SetBool_Target_69);
		local_BeamSensorIsSpawned_System_Boolean = logic_uScriptAct_SetBool_Target_69;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_69.Out)
		{
			Relay_In_66();
		}
	}

	private void Relay_In_74()
	{
		logic_uScript_PlayerInRangeOfCurrentEncounter_ownerNode_74 = owner_Connection_73;
		logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_74.In(logic_uScript_PlayerInRangeOfCurrentEncounter_ownerNode_74);
		if (logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_74.Out)
		{
			Relay_True_16();
		}
	}

	private void Relay_Save_Out_75()
	{
		Relay_Save_82();
	}

	private void Relay_Load_Out_75()
	{
		Relay_Load_82();
	}

	private void Relay_Restart_Out_75()
	{
		Relay_Set_False_82();
	}

	private void Relay_Save_75()
	{
		logic_SubGraph_SaveLoadBool_boolean_75 = local_CanInteractWithToggle_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_75 = local_CanInteractWithToggle_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_75.Save(ref logic_SubGraph_SaveLoadBool_boolean_75, logic_SubGraph_SaveLoadBool_boolAsVariable_75, logic_SubGraph_SaveLoadBool_uniqueID_75);
	}

	private void Relay_Load_75()
	{
		logic_SubGraph_SaveLoadBool_boolean_75 = local_CanInteractWithToggle_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_75 = local_CanInteractWithToggle_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_75.Load(ref logic_SubGraph_SaveLoadBool_boolean_75, logic_SubGraph_SaveLoadBool_boolAsVariable_75, logic_SubGraph_SaveLoadBool_uniqueID_75);
	}

	private void Relay_Set_True_75()
	{
		logic_SubGraph_SaveLoadBool_boolean_75 = local_CanInteractWithToggle_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_75 = local_CanInteractWithToggle_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_75.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_75, logic_SubGraph_SaveLoadBool_boolAsVariable_75, logic_SubGraph_SaveLoadBool_uniqueID_75);
	}

	private void Relay_Set_False_75()
	{
		logic_SubGraph_SaveLoadBool_boolean_75 = local_CanInteractWithToggle_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_75 = local_CanInteractWithToggle_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_75.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_75, logic_SubGraph_SaveLoadBool_boolAsVariable_75, logic_SubGraph_SaveLoadBool_uniqueID_75);
	}

	private void Relay_In_78()
	{
		logic_uScript_LockTechInteraction_tech_78 = local_CircuitBaseTech_Tank;
		int num = 0;
		if (logic_uScript_LockTechInteraction_excludedBlocks_78.Length <= num)
		{
			Array.Resize(ref logic_uScript_LockTechInteraction_excludedBlocks_78, num + 1);
		}
		logic_uScript_LockTechInteraction_excludedBlocks_78[num++] = blockTypeToggle;
		int num2 = 0;
		if (logic_uScript_LockTechInteraction_excludedUniqueBlocks_78.Length <= num2)
		{
			Array.Resize(ref logic_uScript_LockTechInteraction_excludedUniqueBlocks_78, num2 + 1);
		}
		logic_uScript_LockTechInteraction_excludedUniqueBlocks_78[num2++] = local_ToggleBlock_TankBlock;
		logic_uScript_LockTechInteraction_uScript_LockTechInteraction_78.In(logic_uScript_LockTechInteraction_tech_78, logic_uScript_LockTechInteraction_excludedBlocks_78, logic_uScript_LockTechInteraction_excludedUniqueBlocks_78);
		if (logic_uScript_LockTechInteraction_uScript_LockTechInteraction_78.Out)
		{
			Relay_In_8();
		}
	}

	private void Relay_True_80()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_80.True(out logic_uScriptAct_SetBool_Target_80);
		local_CanInteractWithToggle_System_Boolean = logic_uScriptAct_SetBool_Target_80;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_80.Out)
		{
			Relay_In_351();
		}
	}

	private void Relay_False_80()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_80.False(out logic_uScriptAct_SetBool_Target_80);
		local_CanInteractWithToggle_System_Boolean = logic_uScriptAct_SetBool_Target_80;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_80.Out)
		{
			Relay_In_351();
		}
	}

	private void Relay_Save_Out_82()
	{
		Relay_Save_56();
	}

	private void Relay_Load_Out_82()
	{
		Relay_Load_56();
	}

	private void Relay_Restart_Out_82()
	{
		Relay_Set_False_56();
	}

	private void Relay_Save_82()
	{
		logic_SubGraph_SaveLoadBool_boolean_82 = local_ToggleIsOn_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_82 = local_ToggleIsOn_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_82.Save(ref logic_SubGraph_SaveLoadBool_boolean_82, logic_SubGraph_SaveLoadBool_boolAsVariable_82, logic_SubGraph_SaveLoadBool_uniqueID_82);
	}

	private void Relay_Load_82()
	{
		logic_SubGraph_SaveLoadBool_boolean_82 = local_ToggleIsOn_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_82 = local_ToggleIsOn_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_82.Load(ref logic_SubGraph_SaveLoadBool_boolean_82, logic_SubGraph_SaveLoadBool_boolAsVariable_82, logic_SubGraph_SaveLoadBool_uniqueID_82);
	}

	private void Relay_Set_True_82()
	{
		logic_SubGraph_SaveLoadBool_boolean_82 = local_ToggleIsOn_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_82 = local_ToggleIsOn_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_82.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_82, logic_SubGraph_SaveLoadBool_boolAsVariable_82, logic_SubGraph_SaveLoadBool_uniqueID_82);
	}

	private void Relay_Set_False_82()
	{
		logic_SubGraph_SaveLoadBool_boolean_82 = local_ToggleIsOn_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_82 = local_ToggleIsOn_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_82.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_82, logic_SubGraph_SaveLoadBool_boolAsVariable_82, logic_SubGraph_SaveLoadBool_uniqueID_82);
	}

	private void Relay_True_84()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_84.True(out logic_uScriptAct_SetBool_Target_84);
		local_ToggleIsOn_System_Boolean = logic_uScriptAct_SetBool_Target_84;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_84.Out)
		{
			Relay_In_85();
		}
	}

	private void Relay_False_84()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_84.False(out logic_uScriptAct_SetBool_Target_84);
		local_ToggleIsOn_System_Boolean = logic_uScriptAct_SetBool_Target_84;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_84.Out)
		{
			Relay_In_85();
		}
	}

	private void Relay_In_85()
	{
		logic_uScriptCon_CompareBool_Bool_85 = local_ToggleIsOn_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_85.In(logic_uScriptCon_CompareBool_Bool_85);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_85.True)
		{
			Relay_False_80();
		}
	}

	private void Relay_In_86()
	{
		logic_uScriptCon_CompareInt_A_86 = local_ToggleSignalValue_System_Int32;
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_86.In(logic_uScriptCon_CompareInt_A_86, logic_uScriptCon_CompareInt_B_86);
		bool greaterThan = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_86.GreaterThan;
		bool equalTo = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_86.EqualTo;
		if (greaterThan)
		{
			Relay_True_84();
		}
		if (equalTo)
		{
			Relay_False_84();
		}
	}

	private void Relay_In_89()
	{
		logic_uScript_GetCircuitChargeInfo_block_89 = local_ToggleBlock_TankBlock;
		logic_uScript_GetCircuitChargeInfo_Return_89 = logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_89.In(logic_uScript_GetCircuitChargeInfo_block_89, logic_uScript_GetCircuitChargeInfo_tech_89, logic_uScript_GetCircuitChargeInfo_blockType_89);
		local_ToggleSignalValue_System_Int32 = logic_uScript_GetCircuitChargeInfo_Return_89;
		if (logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_89.Out)
		{
			Relay_In_86();
		}
	}

	private void Relay_Save_Out_91()
	{
		Relay_Save_482();
	}

	private void Relay_Load_Out_91()
	{
		Relay_Load_482();
	}

	private void Relay_Restart_Out_91()
	{
		Relay_Set_False_482();
	}

	private void Relay_Save_91()
	{
		logic_SubGraph_SaveLoadBool_boolean_91 = local_BeamSensorIsOn_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_91 = local_BeamSensorIsOn_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_91.Save(ref logic_SubGraph_SaveLoadBool_boolean_91, logic_SubGraph_SaveLoadBool_boolAsVariable_91, logic_SubGraph_SaveLoadBool_uniqueID_91);
	}

	private void Relay_Load_91()
	{
		logic_SubGraph_SaveLoadBool_boolean_91 = local_BeamSensorIsOn_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_91 = local_BeamSensorIsOn_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_91.Load(ref logic_SubGraph_SaveLoadBool_boolean_91, logic_SubGraph_SaveLoadBool_boolAsVariable_91, logic_SubGraph_SaveLoadBool_uniqueID_91);
	}

	private void Relay_Set_True_91()
	{
		logic_SubGraph_SaveLoadBool_boolean_91 = local_BeamSensorIsOn_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_91 = local_BeamSensorIsOn_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_91.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_91, logic_SubGraph_SaveLoadBool_boolAsVariable_91, logic_SubGraph_SaveLoadBool_uniqueID_91);
	}

	private void Relay_Set_False_91()
	{
		logic_SubGraph_SaveLoadBool_boolean_91 = local_BeamSensorIsOn_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_91 = local_BeamSensorIsOn_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_91.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_91, logic_SubGraph_SaveLoadBool_boolAsVariable_91, logic_SubGraph_SaveLoadBool_uniqueID_91);
	}

	private void Relay_In_92()
	{
		logic_uScript_EnableGlow_targetObject_92 = local_Wire1Block_TankBlock;
		logic_uScript_EnableGlow_uScript_EnableGlow_92.In(logic_uScript_EnableGlow_targetObject_92, logic_uScript_EnableGlow_enable_92);
		if (logic_uScript_EnableGlow_uScript_EnableGlow_92.Out)
		{
			Relay_In_95();
		}
	}

	private void Relay_In_95()
	{
		logic_uScript_EnableGlow_targetObject_95 = local_Wire2Block_TankBlock;
		logic_uScript_EnableGlow_uScript_EnableGlow_95.In(logic_uScript_EnableGlow_targetObject_95, logic_uScript_EnableGlow_enable_95);
		if (logic_uScript_EnableGlow_uScript_EnableGlow_95.Out)
		{
			Relay_In_102();
		}
	}

	private void Relay_True_97()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_97.True(out logic_uScriptAct_SetBool_Target_97);
		local_GhostBlockWire1Spawned_System_Boolean = logic_uScriptAct_SetBool_Target_97;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_97.Out)
		{
			Relay_False_100();
		}
	}

	private void Relay_False_97()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_97.False(out logic_uScriptAct_SetBool_Target_97);
		local_GhostBlockWire1Spawned_System_Boolean = logic_uScriptAct_SetBool_Target_97;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_97.Out)
		{
			Relay_False_100();
		}
	}

	private void Relay_True_100()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_100.True(out logic_uScriptAct_SetBool_Target_100);
		local_GhostBlockWire2Spawned_System_Boolean = logic_uScriptAct_SetBool_Target_100;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_100.Out)
		{
			Relay_False_103();
		}
	}

	private void Relay_False_100()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_100.False(out logic_uScriptAct_SetBool_Target_100);
		local_GhostBlockWire2Spawned_System_Boolean = logic_uScriptAct_SetBool_Target_100;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_100.Out)
		{
			Relay_False_103();
		}
	}

	private void Relay_In_102()
	{
		logic_uScript_EnableGlow_targetObject_102 = local_Wire3Block_TankBlock;
		logic_uScript_EnableGlow_uScript_EnableGlow_102.In(logic_uScript_EnableGlow_targetObject_102, logic_uScript_EnableGlow_enable_102);
		if (logic_uScript_EnableGlow_uScript_EnableGlow_102.Out)
		{
			Relay_In_115();
		}
	}

	private void Relay_True_103()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_103.True(out logic_uScriptAct_SetBool_Target_103);
		local_GhostBlockWire3Spawned_System_Boolean = logic_uScriptAct_SetBool_Target_103;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_103.Out)
		{
			Relay_False_45();
		}
	}

	private void Relay_False_103()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_103.False(out logic_uScriptAct_SetBool_Target_103);
		local_GhostBlockWire3Spawned_System_Boolean = logic_uScriptAct_SetBool_Target_103;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_103.Out)
		{
			Relay_False_45();
		}
	}

	private void Relay_Save_Out_105()
	{
		Relay_Save_75();
	}

	private void Relay_Load_Out_105()
	{
		Relay_Load_75();
	}

	private void Relay_Restart_Out_105()
	{
		Relay_Set_False_75();
	}

	private void Relay_Save_105()
	{
		logic_SubGraph_SaveLoadBool_boolean_105 = local_LightIsAttached_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_105 = local_LightIsAttached_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_105.Save(ref logic_SubGraph_SaveLoadBool_boolean_105, logic_SubGraph_SaveLoadBool_boolAsVariable_105, logic_SubGraph_SaveLoadBool_uniqueID_105);
	}

	private void Relay_Load_105()
	{
		logic_SubGraph_SaveLoadBool_boolean_105 = local_LightIsAttached_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_105 = local_LightIsAttached_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_105.Load(ref logic_SubGraph_SaveLoadBool_boolean_105, logic_SubGraph_SaveLoadBool_boolAsVariable_105, logic_SubGraph_SaveLoadBool_uniqueID_105);
	}

	private void Relay_Set_True_105()
	{
		logic_SubGraph_SaveLoadBool_boolean_105 = local_LightIsAttached_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_105 = local_LightIsAttached_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_105.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_105, logic_SubGraph_SaveLoadBool_boolAsVariable_105, logic_SubGraph_SaveLoadBool_uniqueID_105);
	}

	private void Relay_Set_False_105()
	{
		logic_SubGraph_SaveLoadBool_boolean_105 = local_LightIsAttached_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_105 = local_LightIsAttached_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_105.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_105, logic_SubGraph_SaveLoadBool_boolAsVariable_105, logic_SubGraph_SaveLoadBool_uniqueID_105);
	}

	private void Relay_Out_107()
	{
	}

	private void Relay_In_107()
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_107 = local_Stage_System_Int32;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_107.In(ref logic_SubGraph_CompleteObjectiveStage_objectiveStage_107, logic_SubGraph_CompleteObjectiveStage_isFinalObjective_107);
	}

	private void Relay_In_109()
	{
		logic_uScriptCon_CompareBool_Bool_109 = local_CanInteractWithToggle_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_109.In(logic_uScriptCon_CompareBool_Bool_109);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_109.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_109.False;
		if (num)
		{
			Relay_In_89();
		}
		if (flag)
		{
			Relay_True_111();
		}
	}

	private void Relay_True_111()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_111.True(out logic_uScriptAct_SetBool_Target_111);
		local_CanInteractWithToggle_System_Boolean = logic_uScriptAct_SetBool_Target_111;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_111.Out)
		{
			Relay_In_89();
		}
	}

	private void Relay_False_111()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_111.False(out logic_uScriptAct_SetBool_Target_111);
		local_CanInteractWithToggle_System_Boolean = logic_uScriptAct_SetBool_Target_111;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_111.Out)
		{
			Relay_In_89();
		}
	}

	private void Relay_Save_Out_112()
	{
		Relay_Save_508();
	}

	private void Relay_Load_Out_112()
	{
		Relay_Load_508();
	}

	private void Relay_Restart_Out_112()
	{
		Relay_Restart_508();
	}

	private void Relay_Save_112()
	{
		logic_SubGraph_SaveLoadInt_integer_112 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_112 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_112.Save(logic_SubGraph_SaveLoadInt_restartValue_112, ref logic_SubGraph_SaveLoadInt_integer_112, logic_SubGraph_SaveLoadInt_intAsVariable_112, logic_SubGraph_SaveLoadInt_uniqueID_112);
	}

	private void Relay_Load_112()
	{
		logic_SubGraph_SaveLoadInt_integer_112 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_112 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_112.Load(logic_SubGraph_SaveLoadInt_restartValue_112, ref logic_SubGraph_SaveLoadInt_integer_112, logic_SubGraph_SaveLoadInt_intAsVariable_112, logic_SubGraph_SaveLoadInt_uniqueID_112);
	}

	private void Relay_Restart_112()
	{
		logic_SubGraph_SaveLoadInt_integer_112 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_112 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_112.Restart(logic_SubGraph_SaveLoadInt_restartValue_112, ref logic_SubGraph_SaveLoadInt_integer_112, logic_SubGraph_SaveLoadInt_intAsVariable_112, logic_SubGraph_SaveLoadInt_uniqueID_112);
	}

	private void Relay_In_115()
	{
		logic_uScript_EnableGlow_targetObject_115 = local_LightBlock_TankBlock;
		logic_uScript_EnableGlow_uScript_EnableGlow_115.In(logic_uScript_EnableGlow_targetObject_115, logic_uScript_EnableGlow_enable_115);
		if (logic_uScript_EnableGlow_uScript_EnableGlow_115.Out)
		{
			Relay_In_159();
		}
	}

	private void Relay_In_118()
	{
		logic_uScript_RemoveAllGhostBlocksOnTech_targetTech_118 = local_CircuitBaseTech_Tank;
		logic_uScript_RemoveAllGhostBlocksOnTech_uScript_RemoveAllGhostBlocksOnTech_118.In(logic_uScript_RemoveAllGhostBlocksOnTech_targetTech_118);
		if (logic_uScript_RemoveAllGhostBlocksOnTech_uScript_RemoveAllGhostBlocksOnTech_118.Out)
		{
			Relay_In_126();
		}
	}

	private void Relay_Out_123()
	{
	}

	private void Relay_In_123()
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_123 = local_Stage_System_Int32;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_123.In(ref logic_SubGraph_CompleteObjectiveStage_objectiveStage_123, logic_SubGraph_CompleteObjectiveStage_isFinalObjective_123);
	}

	private void Relay_True_125()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_125.True(out logic_uScriptAct_SetBool_Target_125);
		local_LightIsAttached_System_Boolean = logic_uScriptAct_SetBool_Target_125;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_125.Out)
		{
			Relay_In_123();
		}
	}

	private void Relay_False_125()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_125.False(out logic_uScriptAct_SetBool_Target_125);
		local_LightIsAttached_System_Boolean = logic_uScriptAct_SetBool_Target_125;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_125.Out)
		{
			Relay_In_123();
		}
	}

	private void Relay_In_126()
	{
		logic_uScript_HideArrow_uScript_HideArrow_126.In();
		if (logic_uScript_HideArrow_uScript_HideArrow_126.Out)
		{
			Relay_True_125();
		}
	}

	private void Relay_Block_Attached_128()
	{
		Relay_In_118();
	}

	private void Relay_In_128()
	{
		logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_Block_128 = local_LightBlock_TankBlock;
		logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_CraftingBaseTech_128 = local_CircuitBaseTech_Tank;
		int num = 0;
		Array ghostBlockLight = GhostBlockLight;
		if (logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_ghostBlockData_128.Length != num + ghostBlockLight.Length)
		{
			Array.Resize(ref logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_ghostBlockData_128, num + ghostBlockLight.Length);
		}
		Array.Copy(ghostBlockLight, 0, logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_ghostBlockData_128, num, ghostBlockLight.Length);
		num += ghostBlockLight.Length;
		logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_BlockType_128 = blockTypeLight;
		logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_SubGraph_Crafting_Tutorial_AttachBlockToBase_128.In(logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_Block_128, logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_CraftingBaseTech_128, logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_ghostBlockData_128, logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_BlockType_128, logic_SubGraph_Crafting_Tutorial_AttachBlockToBase_BlockPosition_128);
	}

	private void Relay_In_131()
	{
		logic_uScript_PointArrowAtVisible_targetObject_131 = local_ToggleBlock_TankBlock;
		logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_131.In(logic_uScript_PointArrowAtVisible_targetObject_131, logic_uScript_PointArrowAtVisible_timeToShowFor_131, logic_uScript_PointArrowAtVisible_offset_131);
		if (logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_131.Out)
		{
			Relay_In_133();
		}
	}

	private void Relay_In_133()
	{
		logic_uScript_EnableGlow_targetObject_133 = local_ToggleBlock_TankBlock;
		logic_uScript_EnableGlow_uScript_EnableGlow_133.In(logic_uScript_EnableGlow_targetObject_133, logic_uScript_EnableGlow_enable_133);
		if (logic_uScript_EnableGlow_uScript_EnableGlow_133.Out)
		{
			Relay_In_109();
		}
	}

	private void Relay_In_137()
	{
		logic_uScript_GetCircuitChargeInfo_block_137 = local_ToggleBlock_TankBlock;
		logic_uScript_GetCircuitChargeInfo_Return_137 = logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_137.In(logic_uScript_GetCircuitChargeInfo_block_137, logic_uScript_GetCircuitChargeInfo_tech_137, logic_uScript_GetCircuitChargeInfo_blockType_137);
		local_ToggleSignalValue_System_Int32 = logic_uScript_GetCircuitChargeInfo_Return_137;
		if (logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_137.Out)
		{
			Relay_In_139();
		}
	}

	private void Relay_True_138()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_138.True(out logic_uScriptAct_SetBool_Target_138);
		local_CanInteractWithToggle_System_Boolean = logic_uScriptAct_SetBool_Target_138;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_138.Out)
		{
			Relay_In_361();
		}
	}

	private void Relay_False_138()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_138.False(out logic_uScriptAct_SetBool_Target_138);
		local_CanInteractWithToggle_System_Boolean = logic_uScriptAct_SetBool_Target_138;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_138.Out)
		{
			Relay_In_361();
		}
	}

	private void Relay_In_139()
	{
		logic_uScriptCon_CompareInt_A_139 = local_ToggleSignalValue_System_Int32;
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_139.In(logic_uScriptCon_CompareInt_A_139, logic_uScriptCon_CompareInt_B_139);
		bool greaterThan = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_139.GreaterThan;
		bool equalTo = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_139.EqualTo;
		if (greaterThan)
		{
			Relay_True_162();
		}
		if (equalTo)
		{
			Relay_False_162();
		}
	}

	private void Relay_In_140()
	{
		logic_uScriptCon_CompareBool_Bool_140 = local_CanInteractWithToggle_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_140.In(logic_uScriptCon_CompareBool_Bool_140);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_140.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_140.False;
		if (num)
		{
			Relay_In_137();
		}
		if (flag)
		{
			Relay_True_160();
		}
	}

	private void Relay_Out_142()
	{
	}

	private void Relay_In_142()
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_142 = local_Stage_System_Int32;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_142.In(ref logic_SubGraph_CompleteObjectiveStage_objectiveStage_142, logic_SubGraph_CompleteObjectiveStage_isFinalObjective_142);
	}

	private void Relay_Save_Out_143()
	{
		Relay_Save_173();
	}

	private void Relay_Load_Out_143()
	{
		Relay_Load_173();
	}

	private void Relay_Restart_Out_143()
	{
		Relay_Set_False_173();
	}

	private void Relay_Save_143()
	{
		logic_SubGraph_SaveLoadBool_boolean_143 = local_msgIntroShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_143 = local_msgIntroShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_143.Save(ref logic_SubGraph_SaveLoadBool_boolean_143, logic_SubGraph_SaveLoadBool_boolAsVariable_143, logic_SubGraph_SaveLoadBool_uniqueID_143);
	}

	private void Relay_Load_143()
	{
		logic_SubGraph_SaveLoadBool_boolean_143 = local_msgIntroShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_143 = local_msgIntroShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_143.Load(ref logic_SubGraph_SaveLoadBool_boolean_143, logic_SubGraph_SaveLoadBool_boolAsVariable_143, logic_SubGraph_SaveLoadBool_uniqueID_143);
	}

	private void Relay_Set_True_143()
	{
		logic_SubGraph_SaveLoadBool_boolean_143 = local_msgIntroShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_143 = local_msgIntroShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_143.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_143, logic_SubGraph_SaveLoadBool_boolAsVariable_143, logic_SubGraph_SaveLoadBool_uniqueID_143);
	}

	private void Relay_Set_False_143()
	{
		logic_SubGraph_SaveLoadBool_boolean_143 = local_msgIntroShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_143 = local_msgIntroShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_143.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_143, logic_SubGraph_SaveLoadBool_boolAsVariable_143, logic_SubGraph_SaveLoadBool_uniqueID_143);
	}

	private void Relay_In_146()
	{
		logic_uScript_EnableGlow_targetObject_146 = local_GhostBlockWire3_TankBlock;
		logic_uScript_EnableGlow_uScript_EnableGlow_146.In(logic_uScript_EnableGlow_targetObject_146, logic_uScript_EnableGlow_enable_146);
		if (logic_uScript_EnableGlow_uScript_EnableGlow_146.Out)
		{
			Relay_In_5();
		}
	}

	private void Relay_In_147()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_147.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_147.Out)
		{
			Relay_In_5();
		}
	}

	private void Relay_In_148()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_148.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_148.Out)
		{
			Relay_In_153();
		}
	}

	private void Relay_In_153()
	{
		logic_uScriptCon_CompareBool_Bool_153 = local_GhostBlockWire2Spawned_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_153.In(logic_uScriptCon_CompareBool_Bool_153);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_153.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_153.False;
		if (num)
		{
			Relay_In_154();
		}
		if (flag)
		{
			Relay_In_158();
		}
	}

	private void Relay_In_154()
	{
		logic_uScript_EnableGlow_targetObject_154 = local_GhostBlockWire2_TankBlock;
		logic_uScript_EnableGlow_uScript_EnableGlow_154.In(logic_uScript_EnableGlow_targetObject_154, logic_uScript_EnableGlow_enable_154);
		if (logic_uScript_EnableGlow_uScript_EnableGlow_154.Out)
		{
			Relay_In_156();
		}
	}

	private void Relay_In_155()
	{
		logic_uScript_EnableGlow_targetObject_155 = local_GhostBlockWire1_TankBlock;
		logic_uScript_EnableGlow_uScript_EnableGlow_155.In(logic_uScript_EnableGlow_targetObject_155, logic_uScript_EnableGlow_enable_155);
		if (logic_uScript_EnableGlow_uScript_EnableGlow_155.Out)
		{
			Relay_In_153();
		}
	}

	private void Relay_In_156()
	{
		logic_uScriptCon_CompareBool_Bool_156 = local_GhostBlockWire3Spawned_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_156.In(logic_uScriptCon_CompareBool_Bool_156);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_156.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_156.False;
		if (num)
		{
			Relay_In_146();
		}
		if (flag)
		{
			Relay_In_147();
		}
	}

	private void Relay_In_158()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_158.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_158.Out)
		{
			Relay_In_156();
		}
	}

	private void Relay_In_159()
	{
		logic_uScriptCon_CompareBool_Bool_159 = local_GhostBlockWire1Spawned_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_159.In(logic_uScriptCon_CompareBool_Bool_159);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_159.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_159.False;
		if (num)
		{
			Relay_In_155();
		}
		if (flag)
		{
			Relay_In_148();
		}
	}

	private void Relay_True_160()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_160.True(out logic_uScriptAct_SetBool_Target_160);
		local_CanInteractWithToggle_System_Boolean = logic_uScriptAct_SetBool_Target_160;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_160.Out)
		{
			Relay_In_137();
		}
	}

	private void Relay_False_160()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_160.False(out logic_uScriptAct_SetBool_Target_160);
		local_CanInteractWithToggle_System_Boolean = logic_uScriptAct_SetBool_Target_160;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_160.Out)
		{
			Relay_In_137();
		}
	}

	private void Relay_True_162()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_162.True(out logic_uScriptAct_SetBool_Target_162);
		local_ToggleIsOn_System_Boolean = logic_uScriptAct_SetBool_Target_162;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_162.Out)
		{
			Relay_In_163();
		}
	}

	private void Relay_False_162()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_162.False(out logic_uScriptAct_SetBool_Target_162);
		local_ToggleIsOn_System_Boolean = logic_uScriptAct_SetBool_Target_162;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_162.Out)
		{
			Relay_In_163();
		}
	}

	private void Relay_In_163()
	{
		logic_uScriptCon_CompareBool_Bool_163 = local_ToggleIsOn_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_163.In(logic_uScriptCon_CompareBool_Bool_163);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_163.False)
		{
			Relay_False_138();
		}
	}

	private void Relay_True_165()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_165.True(out logic_uScriptAct_SetBool_Target_165);
		local_msgBaseFoundShown_System_Boolean = logic_uScriptAct_SetBool_Target_165;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_165.Out)
		{
			Relay_In_493();
		}
	}

	private void Relay_False_165()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_165.False(out logic_uScriptAct_SetBool_Target_165);
		local_msgBaseFoundShown_System_Boolean = logic_uScriptAct_SetBool_Target_165;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_165.Out)
		{
			Relay_In_493();
		}
	}

	private void Relay_In_168()
	{
		logic_uScript_AddMessage_messageData_168 = msg02BaseFound;
		logic_uScript_AddMessage_speaker_168 = messageSpeaker;
		logic_uScript_AddMessage_Return_168 = logic_uScript_AddMessage_uScript_AddMessage_168.In(logic_uScript_AddMessage_messageData_168, logic_uScript_AddMessage_speaker_168);
		if (logic_uScript_AddMessage_uScript_AddMessage_168.Shown)
		{
			Relay_True_165();
		}
	}

	private void Relay_In_171()
	{
		logic_uScriptCon_CompareBool_Bool_171 = local_msgBaseFoundShown_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_171.In(logic_uScriptCon_CompareBool_Bool_171);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_171.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_171.False;
		if (num)
		{
			Relay_In_493();
		}
		if (flag)
		{
			Relay_In_168();
		}
	}

	private void Relay_Save_Out_173()
	{
		Relay_Save_193();
	}

	private void Relay_Load_Out_173()
	{
		Relay_Load_193();
	}

	private void Relay_Restart_Out_173()
	{
		Relay_Set_False_193();
	}

	private void Relay_Save_173()
	{
		logic_SubGraph_SaveLoadBool_boolean_173 = local_msgBaseFoundShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_173 = local_msgBaseFoundShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_173.Save(ref logic_SubGraph_SaveLoadBool_boolean_173, logic_SubGraph_SaveLoadBool_boolAsVariable_173, logic_SubGraph_SaveLoadBool_uniqueID_173);
	}

	private void Relay_Load_173()
	{
		logic_SubGraph_SaveLoadBool_boolean_173 = local_msgBaseFoundShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_173 = local_msgBaseFoundShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_173.Load(ref logic_SubGraph_SaveLoadBool_boolean_173, logic_SubGraph_SaveLoadBool_boolAsVariable_173, logic_SubGraph_SaveLoadBool_uniqueID_173);
	}

	private void Relay_Set_True_173()
	{
		logic_SubGraph_SaveLoadBool_boolean_173 = local_msgBaseFoundShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_173 = local_msgBaseFoundShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_173.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_173, logic_SubGraph_SaveLoadBool_boolAsVariable_173, logic_SubGraph_SaveLoadBool_uniqueID_173);
	}

	private void Relay_Set_False_173()
	{
		logic_SubGraph_SaveLoadBool_boolean_173 = local_msgBaseFoundShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_173 = local_msgBaseFoundShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_173.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_173, logic_SubGraph_SaveLoadBool_boolAsVariable_173, logic_SubGraph_SaveLoadBool_uniqueID_173);
	}

	private void Relay_True_175()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_175.True(out logic_uScriptAct_SetBool_Target_175);
		local_msgWiresAttachedShown_System_Boolean = logic_uScriptAct_SetBool_Target_175;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_175.Out)
		{
			Relay_In_128();
		}
	}

	private void Relay_False_175()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_175.False(out logic_uScriptAct_SetBool_Target_175);
		local_msgWiresAttachedShown_System_Boolean = logic_uScriptAct_SetBool_Target_175;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_175.Out)
		{
			Relay_In_128();
		}
	}

	private void Relay_In_177()
	{
		logic_uScript_AddMessage_messageData_177 = msg05AttachedWires;
		logic_uScript_AddMessage_speaker_177 = messageSpeaker;
		logic_uScript_AddMessage_Return_177 = logic_uScript_AddMessage_uScript_AddMessage_177.In(logic_uScript_AddMessage_messageData_177, logic_uScript_AddMessage_speaker_177);
		if (logic_uScript_AddMessage_uScript_AddMessage_177.Shown)
		{
			Relay_True_175();
		}
	}

	private void Relay_In_178()
	{
		logic_uScript_AddMessage_messageData_178 = msg06PickupLight;
		logic_uScript_AddMessage_speaker_178 = messageSpeaker;
		logic_uScript_AddMessage_Return_178 = logic_uScript_AddMessage_uScript_AddMessage_178.In(logic_uScript_AddMessage_messageData_178, logic_uScript_AddMessage_speaker_178);
		if (logic_uScript_AddMessage_uScript_AddMessage_178.Out)
		{
			Relay_In_128();
		}
	}

	private void Relay_In_179()
	{
		logic_uScriptCon_CompareBool_Bool_179 = local_msgWiresAttachedShown_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_179.In(logic_uScriptCon_CompareBool_Bool_179);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_179.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_179.False;
		if (num)
		{
			Relay_In_178();
		}
		if (flag)
		{
			Relay_In_177();
		}
	}

	private void Relay_In_184()
	{
		logic_uScriptCon_CompareBool_Bool_184 = local_msgLightAttachedShown_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_184.In(logic_uScriptCon_CompareBool_Bool_184);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_184.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_184.False;
		if (num)
		{
			Relay_In_556();
		}
		if (flag)
		{
			Relay_In_188();
		}
	}

	private void Relay_True_186()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_186.True(out logic_uScriptAct_SetBool_Target_186);
		local_msgLightAttachedShown_System_Boolean = logic_uScriptAct_SetBool_Target_186;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_186.Out)
		{
			Relay_In_486();
		}
	}

	private void Relay_False_186()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_186.False(out logic_uScriptAct_SetBool_Target_186);
		local_msgLightAttachedShown_System_Boolean = logic_uScriptAct_SetBool_Target_186;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_186.Out)
		{
			Relay_In_486();
		}
	}

	private void Relay_In_188()
	{
		logic_uScript_AddMessage_messageData_188 = msg07AttachedLight;
		logic_uScript_AddMessage_speaker_188 = messageSpeaker;
		logic_uScript_AddMessage_Return_188 = logic_uScript_AddMessage_uScript_AddMessage_188.In(logic_uScript_AddMessage_messageData_188, logic_uScript_AddMessage_speaker_188);
		if (logic_uScript_AddMessage_uScript_AddMessage_188.Shown)
		{
			Relay_True_186();
		}
	}

	private void Relay_Save_Out_193()
	{
		Relay_Save_194();
	}

	private void Relay_Load_Out_193()
	{
		Relay_Load_194();
	}

	private void Relay_Restart_Out_193()
	{
		Relay_Set_False_194();
	}

	private void Relay_Save_193()
	{
		logic_SubGraph_SaveLoadBool_boolean_193 = local_msgWiresAttachedShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_193 = local_msgWiresAttachedShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_193.Save(ref logic_SubGraph_SaveLoadBool_boolean_193, logic_SubGraph_SaveLoadBool_boolAsVariable_193, logic_SubGraph_SaveLoadBool_uniqueID_193);
	}

	private void Relay_Load_193()
	{
		logic_SubGraph_SaveLoadBool_boolean_193 = local_msgWiresAttachedShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_193 = local_msgWiresAttachedShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_193.Load(ref logic_SubGraph_SaveLoadBool_boolean_193, logic_SubGraph_SaveLoadBool_boolAsVariable_193, logic_SubGraph_SaveLoadBool_uniqueID_193);
	}

	private void Relay_Set_True_193()
	{
		logic_SubGraph_SaveLoadBool_boolean_193 = local_msgWiresAttachedShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_193 = local_msgWiresAttachedShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_193.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_193, logic_SubGraph_SaveLoadBool_boolAsVariable_193, logic_SubGraph_SaveLoadBool_uniqueID_193);
	}

	private void Relay_Set_False_193()
	{
		logic_SubGraph_SaveLoadBool_boolean_193 = local_msgWiresAttachedShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_193 = local_msgWiresAttachedShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_193.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_193, logic_SubGraph_SaveLoadBool_boolAsVariable_193, logic_SubGraph_SaveLoadBool_uniqueID_193);
	}

	private void Relay_Save_Out_194()
	{
		Relay_Save_203();
	}

	private void Relay_Load_Out_194()
	{
		Relay_Load_203();
	}

	private void Relay_Restart_Out_194()
	{
		Relay_Set_False_203();
	}

	private void Relay_Save_194()
	{
		logic_SubGraph_SaveLoadBool_boolean_194 = local_msgLightAttachedShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_194 = local_msgLightAttachedShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_194.Save(ref logic_SubGraph_SaveLoadBool_boolean_194, logic_SubGraph_SaveLoadBool_boolAsVariable_194, logic_SubGraph_SaveLoadBool_uniqueID_194);
	}

	private void Relay_Load_194()
	{
		logic_SubGraph_SaveLoadBool_boolean_194 = local_msgLightAttachedShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_194 = local_msgLightAttachedShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_194.Load(ref logic_SubGraph_SaveLoadBool_boolean_194, logic_SubGraph_SaveLoadBool_boolAsVariable_194, logic_SubGraph_SaveLoadBool_uniqueID_194);
	}

	private void Relay_Set_True_194()
	{
		logic_SubGraph_SaveLoadBool_boolean_194 = local_msgLightAttachedShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_194 = local_msgLightAttachedShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_194.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_194, logic_SubGraph_SaveLoadBool_boolAsVariable_194, logic_SubGraph_SaveLoadBool_uniqueID_194);
	}

	private void Relay_Set_False_194()
	{
		logic_SubGraph_SaveLoadBool_boolean_194 = local_msgLightAttachedShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_194 = local_msgLightAttachedShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_194.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_194, logic_SubGraph_SaveLoadBool_boolAsVariable_194, logic_SubGraph_SaveLoadBool_uniqueID_194);
	}

	private void Relay_True_196()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_196.True(out logic_uScriptAct_SetBool_Target_196);
		local_msgToggleIsOnShown_System_Boolean = logic_uScriptAct_SetBool_Target_196;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_196.Out)
		{
			Relay_In_362();
		}
	}

	private void Relay_False_196()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_196.False(out logic_uScriptAct_SetBool_Target_196);
		local_msgToggleIsOnShown_System_Boolean = logic_uScriptAct_SetBool_Target_196;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_196.Out)
		{
			Relay_In_362();
		}
	}

	private void Relay_In_197()
	{
		logic_uScriptCon_CompareBool_Bool_197 = local_msgToggleIsOnShown_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_197.In(logic_uScriptCon_CompareBool_Bool_197);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_197.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_197.False;
		if (num)
		{
			Relay_In_560();
		}
		if (flag)
		{
			Relay_In_201();
		}
	}

	private void Relay_In_201()
	{
		logic_uScript_AddMessage_messageData_201 = msg09ToggleIsOn;
		logic_uScript_AddMessage_speaker_201 = messageSpeaker;
		logic_uScript_AddMessage_Return_201 = logic_uScript_AddMessage_uScript_AddMessage_201.In(logic_uScript_AddMessage_messageData_201, logic_uScript_AddMessage_speaker_201);
		if (logic_uScript_AddMessage_uScript_AddMessage_201.Shown)
		{
			Relay_True_196();
		}
	}

	private void Relay_Save_Out_203()
	{
		Relay_Save_383();
	}

	private void Relay_Load_Out_203()
	{
		Relay_Load_383();
	}

	private void Relay_Restart_Out_203()
	{
		Relay_Set_False_383();
	}

	private void Relay_Save_203()
	{
		logic_SubGraph_SaveLoadBool_boolean_203 = local_msgToggleIsOnShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_203 = local_msgToggleIsOnShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_203.Save(ref logic_SubGraph_SaveLoadBool_boolean_203, logic_SubGraph_SaveLoadBool_boolAsVariable_203, logic_SubGraph_SaveLoadBool_uniqueID_203);
	}

	private void Relay_Load_203()
	{
		logic_SubGraph_SaveLoadBool_boolean_203 = local_msgToggleIsOnShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_203 = local_msgToggleIsOnShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_203.Load(ref logic_SubGraph_SaveLoadBool_boolean_203, logic_SubGraph_SaveLoadBool_boolAsVariable_203, logic_SubGraph_SaveLoadBool_uniqueID_203);
	}

	private void Relay_Set_True_203()
	{
		logic_SubGraph_SaveLoadBool_boolean_203 = local_msgToggleIsOnShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_203 = local_msgToggleIsOnShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_203.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_203, logic_SubGraph_SaveLoadBool_boolAsVariable_203, logic_SubGraph_SaveLoadBool_uniqueID_203);
	}

	private void Relay_Set_False_203()
	{
		logic_SubGraph_SaveLoadBool_boolean_203 = local_msgToggleIsOnShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_203 = local_msgToggleIsOnShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_203.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_203, logic_SubGraph_SaveLoadBool_boolAsVariable_203, logic_SubGraph_SaveLoadBool_uniqueID_203);
	}

	private void Relay_In_206()
	{
		logic_uScriptCon_CompareBool_Bool_206 = local_msgBeamSensorSpawnedShown_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_206.In(logic_uScriptCon_CompareBool_Bool_206);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_206.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_206.False;
		if (num)
		{
			Relay_In_567();
		}
		if (flag)
		{
			Relay_In_207();
		}
	}

	private void Relay_In_207()
	{
		logic_uScript_AddMessage_messageData_207 = msg12BeamSensorSpawned;
		logic_uScript_AddMessage_speaker_207 = messageSpeaker;
		logic_uScript_AddMessage_Return_207 = logic_uScript_AddMessage_uScript_AddMessage_207.In(logic_uScript_AddMessage_messageData_207, logic_uScript_AddMessage_speaker_207);
		if (logic_uScript_AddMessage_uScript_AddMessage_207.Shown)
		{
			Relay_True_208();
		}
	}

	private void Relay_True_208()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_208.True(out logic_uScriptAct_SetBool_Target_208);
		local_msgBeamSensorSpawnedShown_System_Boolean = logic_uScriptAct_SetBool_Target_208;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_208.Out)
		{
			Relay_In_58();
		}
	}

	private void Relay_False_208()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_208.False(out logic_uScriptAct_SetBool_Target_208);
		local_msgBeamSensorSpawnedShown_System_Boolean = logic_uScriptAct_SetBool_Target_208;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_208.Out)
		{
			Relay_In_58();
		}
	}

	private void Relay_True_211()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_211.True(out logic_uScriptAct_SetBool_Target_211);
		local_BeamSensorIsAttached_System_Boolean = logic_uScriptAct_SetBool_Target_211;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_211.Out)
		{
			Relay_In_516();
		}
	}

	private void Relay_False_211()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_211.False(out logic_uScriptAct_SetBool_Target_211);
		local_BeamSensorIsAttached_System_Boolean = logic_uScriptAct_SetBool_Target_211;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_211.Out)
		{
			Relay_In_516();
		}
	}

	private void Relay_In_214()
	{
		logic_uScriptCon_CompareBool_Bool_214 = local_GhostBlockWire2Spawned_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_214.In(logic_uScriptCon_CompareBool_Bool_214);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_214.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_214.False;
		if (num)
		{
			Relay_AtIndex_232();
		}
		if (flag)
		{
			Relay_True_343();
		}
	}

	private void Relay_In_215()
	{
		logic_uScript_DoesTechHaveBlockAtPosition_tech_215 = local_CircuitBaseTech_Tank;
		logic_uScript_DoesTechHaveBlockAtPosition_blockType_215 = blockTypeWire;
		logic_uScript_DoesTechHaveBlockAtPosition_uScript_DoesTechHaveBlockAtPosition_215.In(logic_uScript_DoesTechHaveBlockAtPosition_tech_215, logic_uScript_DoesTechHaveBlockAtPosition_blockType_215, logic_uScript_DoesTechHaveBlockAtPosition_localPosition_215);
		if (logic_uScript_DoesTechHaveBlockAtPosition_uScript_DoesTechHaveBlockAtPosition_215.True)
		{
			Relay_In_220();
		}
	}

	private void Relay_In_216()
	{
		logic_uScript_EnableGlow_targetObject_216 = local_GhostBlockWire3_TankBlock;
		logic_uScript_EnableGlow_uScript_EnableGlow_216.In(logic_uScript_EnableGlow_targetObject_216, logic_uScript_EnableGlow_enable_216);
		if (logic_uScript_EnableGlow_uScript_EnableGlow_216.Out)
		{
			Relay_In_239();
		}
	}

	private void Relay_AtIndex_217()
	{
		int num = 0;
		Array array = local_227_TankBlockArray;
		if (logic_uScript_AccessListBlock_blockList_217.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListBlock_blockList_217, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListBlock_blockList_217, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListBlock_uScript_AccessListBlock_217.AtIndex(ref logic_uScript_AccessListBlock_blockList_217, logic_uScript_AccessListBlock_index_217, out logic_uScript_AccessListBlock_value_217);
		local_227_TankBlockArray = logic_uScript_AccessListBlock_blockList_217;
		local_GhostBlockWire1_TankBlock = logic_uScript_AccessListBlock_value_217;
		if (logic_uScript_AccessListBlock_uScript_AccessListBlock_217.Out)
		{
			Relay_In_257();
		}
	}

	private void Relay_In_219()
	{
		logic_uScript_PointArrowAtVisible_targetObject_219 = local_Wire1Block_TankBlock;
		logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_219.In(logic_uScript_PointArrowAtVisible_targetObject_219, logic_uScript_PointArrowAtVisible_timeToShowFor_219, logic_uScript_PointArrowAtVisible_offset_219);
		if (logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_219.Out)
		{
			Relay_In_317();
		}
	}

	private void Relay_In_220()
	{
		logic_uScript_RemoveAllGhostBlocksOnTech_targetTech_220 = local_CircuitBaseTech_Tank;
		logic_uScript_RemoveAllGhostBlocksOnTech_uScript_RemoveAllGhostBlocksOnTech_220.In(logic_uScript_RemoveAllGhostBlocksOnTech_targetTech_220);
		if (logic_uScript_RemoveAllGhostBlocksOnTech_uScript_RemoveAllGhostBlocksOnTech_220.Out)
		{
			Relay_In_266();
		}
	}

	private void Relay_In_221()
	{
		logic_uScript_EnableGlow_targetObject_221 = local_Wire1Block_TankBlock;
		logic_uScript_EnableGlow_uScript_EnableGlow_221.In(logic_uScript_EnableGlow_targetObject_221, logic_uScript_EnableGlow_enable_221);
		if (logic_uScript_EnableGlow_uScript_EnableGlow_221.Out)
		{
			Relay_In_271();
		}
	}

	private void Relay_In_222()
	{
		logic_uScript_PointArrowAtVisible_targetObject_222 = local_GhostBlockWire3_TankBlock;
		logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_222.In(logic_uScript_PointArrowAtVisible_targetObject_222, logic_uScript_PointArrowAtVisible_timeToShowFor_222, logic_uScript_PointArrowAtVisible_offset_222);
		if (logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_222.Out)
		{
			Relay_In_264();
		}
	}

	private void Relay_In_224()
	{
		logic_uScriptCon_CompareBool_Bool_224 = local_GhostBlockWire3Spawned_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_224.In(logic_uScriptCon_CompareBool_Bool_224);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_224.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_224.False;
		if (num)
		{
			Relay_AtIndex_325();
		}
		if (flag)
		{
			Relay_True_269();
		}
	}

	private void Relay_In_226()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_226.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_226.Out)
		{
			Relay_In_239();
		}
	}

	private void Relay_In_228()
	{
		logic_uScriptAct_AddInt_v2_A_228 = local_NumWiresAttached_System_Int32;
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_228.In(logic_uScriptAct_AddInt_v2_A_228, logic_uScriptAct_AddInt_v2_B_228, out logic_uScriptAct_AddInt_v2_IntResult_228, out logic_uScriptAct_AddInt_v2_FloatResult_228);
		local_NumWiresAttached_System_Int32 = logic_uScriptAct_AddInt_v2_IntResult_228;
	}

	private void Relay_In_231()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_231.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_231.Out)
		{
			Relay_In_314();
		}
	}

	private void Relay_AtIndex_232()
	{
		int num = 0;
		Array array = local_240_TankBlockArray;
		if (logic_uScript_AccessListBlock_blockList_232.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListBlock_blockList_232, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListBlock_blockList_232, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListBlock_uScript_AccessListBlock_232.AtIndex(ref logic_uScript_AccessListBlock_blockList_232, logic_uScript_AccessListBlock_index_232, out logic_uScript_AccessListBlock_value_232);
		local_240_TankBlockArray = logic_uScript_AccessListBlock_blockList_232;
		local_GhostBlockWire2_TankBlock = logic_uScript_AccessListBlock_value_232;
		if (logic_uScript_AccessListBlock_uScript_AccessListBlock_232.Out)
		{
			Relay_In_254();
		}
	}

	private void Relay_In_237()
	{
		logic_uScript_EnableGlow_targetObject_237 = local_Wire3Block_TankBlock;
		logic_uScript_EnableGlow_uScript_EnableGlow_237.In(logic_uScript_EnableGlow_targetObject_237, logic_uScript_EnableGlow_enable_237);
		if (logic_uScript_EnableGlow_uScript_EnableGlow_237.Out)
		{
			Relay_In_330();
		}
	}

	private void Relay_In_238()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_238.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_238.Out)
		{
			Relay_In_326();
		}
	}

	private void Relay_In_239()
	{
		logic_uScript_BlockAttachedToTech_tech_239 = local_CircuitBaseTech_Tank;
		logic_uScript_BlockAttachedToTech_block_239 = local_Wire1Block_TankBlock;
		logic_uScript_BlockAttachedToTech_uScript_BlockAttachedToTech_239.In(logic_uScript_BlockAttachedToTech_tech_239, logic_uScript_BlockAttachedToTech_block_239);
		bool num = logic_uScript_BlockAttachedToTech_uScript_BlockAttachedToTech_239.True;
		bool flag = logic_uScript_BlockAttachedToTech_uScript_BlockAttachedToTech_239.False;
		if (num)
		{
			Relay_In_221();
		}
		if (flag)
		{
			Relay_In_219();
		}
	}

	private void Relay_TrySpawnOnTech_241()
	{
		int num = 0;
		Array array = ghostBlockWire01;
		if (logic_uScript_SpawnGhostBlocks_ghostBlockData_241.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SpawnGhostBlocks_ghostBlockData_241, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SpawnGhostBlocks_ghostBlockData_241, num, array.Length);
		num += array.Length;
		logic_uScript_SpawnGhostBlocks_ownerNode_241 = owner_Connection_297;
		logic_uScript_SpawnGhostBlocks_targetTech_241 = local_CircuitBaseTech_Tank;
		logic_uScript_SpawnGhostBlocks_Return_241 = logic_uScript_SpawnGhostBlocks_uScript_SpawnGhostBlocks_241.TrySpawnOnTech(logic_uScript_SpawnGhostBlocks_ghostBlockData_241, logic_uScript_SpawnGhostBlocks_ownerNode_241, logic_uScript_SpawnGhostBlocks_targetTech_241);
		local_227_TankBlockArray = logic_uScript_SpawnGhostBlocks_Return_241;
	}

	private void Relay_In_244()
	{
		logic_uScriptCon_CompareInt_A_244 = local_NumWiresAttached_System_Int32;
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_244.In(logic_uScriptCon_CompareInt_A_244, logic_uScriptCon_CompareInt_B_244);
		bool equalTo = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_244.EqualTo;
		bool notEqualTo = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_244.NotEqualTo;
		if (equalTo)
		{
			Relay_In_513();
		}
		if (notEqualTo)
		{
			Relay_In_347();
		}
	}

	private void Relay_In_246()
	{
		logic_uScript_EnableGlow_targetObject_246 = local_Wire3Block_TankBlock;
		logic_uScript_EnableGlow_uScript_EnableGlow_246.In(logic_uScript_EnableGlow_targetObject_246, logic_uScript_EnableGlow_enable_246);
		if (logic_uScript_EnableGlow_uScript_EnableGlow_246.Out)
		{
			Relay_In_310();
			Relay_In_330();
		}
	}

	private void Relay_In_249()
	{
		logic_uScript_EnableGlow_targetObject_249 = local_GhostBlockWire2_TankBlock;
		logic_uScript_EnableGlow_uScript_EnableGlow_249.In(logic_uScript_EnableGlow_targetObject_249, logic_uScript_EnableGlow_enable_249);
	}

	private void Relay_In_251()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_251.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_251.Out)
		{
			Relay_True_283();
		}
	}

	private void Relay_In_252()
	{
		logic_uScript_EnableGlow_targetObject_252 = local_Wire2Block_TankBlock;
		logic_uScript_EnableGlow_uScript_EnableGlow_252.In(logic_uScript_EnableGlow_targetObject_252, logic_uScript_EnableGlow_enable_252);
		if (logic_uScript_EnableGlow_uScript_EnableGlow_252.Out)
		{
			Relay_In_307();
		}
	}

	private void Relay_In_253()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_253.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_253.Out)
		{
			Relay_In_278();
		}
	}

	private void Relay_In_254()
	{
		logic_uScript_PointArrowAtVisible_targetObject_254 = local_GhostBlockWire2_TankBlock;
		logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_254.In(logic_uScript_PointArrowAtVisible_targetObject_254, logic_uScript_PointArrowAtVisible_timeToShowFor_254, logic_uScript_PointArrowAtVisible_offset_254);
		if (logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_254.Out)
		{
			Relay_In_249();
		}
	}

	private void Relay_In_257()
	{
		logic_uScript_PointArrowAtVisible_targetObject_257 = local_GhostBlockWire1_TankBlock;
		logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_257.In(logic_uScript_PointArrowAtVisible_targetObject_257, logic_uScript_PointArrowAtVisible_timeToShowFor_257, logic_uScript_PointArrowAtVisible_offset_257);
		if (logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_257.Out)
		{
			Relay_In_322();
		}
	}

	private void Relay_In_264()
	{
		logic_uScript_EnableGlow_targetObject_264 = local_GhostBlockWire3_TankBlock;
		logic_uScript_EnableGlow_uScript_EnableGlow_264.In(logic_uScript_EnableGlow_targetObject_264, logic_uScript_EnableGlow_enable_264);
	}

	private void Relay_In_266()
	{
		logic_uScript_HideArrow_uScript_HideArrow_266.In();
		if (logic_uScript_HideArrow_uScript_HideArrow_266.Out)
		{
			Relay_In_228();
		}
	}

	private void Relay_TrySpawnOnTech_267()
	{
		int num = 0;
		Array array = ghostBlockWire02;
		if (logic_uScript_SpawnGhostBlocks_ghostBlockData_267.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SpawnGhostBlocks_ghostBlockData_267, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SpawnGhostBlocks_ghostBlockData_267, num, array.Length);
		num += array.Length;
		logic_uScript_SpawnGhostBlocks_ownerNode_267 = owner_Connection_320;
		logic_uScript_SpawnGhostBlocks_targetTech_267 = local_CircuitBaseTech_Tank;
		logic_uScript_SpawnGhostBlocks_Return_267 = logic_uScript_SpawnGhostBlocks_uScript_SpawnGhostBlocks_267.TrySpawnOnTech(logic_uScript_SpawnGhostBlocks_ghostBlockData_267, logic_uScript_SpawnGhostBlocks_ownerNode_267, logic_uScript_SpawnGhostBlocks_targetTech_267);
		local_240_TankBlockArray = logic_uScript_SpawnGhostBlocks_Return_267;
	}

	private void Relay_True_269()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_269.True(out logic_uScriptAct_SetBool_Target_269);
		local_GhostBlockWire3Spawned_System_Boolean = logic_uScriptAct_SetBool_Target_269;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_269.Out)
		{
			Relay_TrySpawnOnTech_291();
		}
	}

	private void Relay_False_269()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_269.False(out logic_uScriptAct_SetBool_Target_269);
		local_GhostBlockWire3Spawned_System_Boolean = logic_uScriptAct_SetBool_Target_269;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_269.Out)
		{
			Relay_TrySpawnOnTech_291();
		}
	}

	private void Relay_In_271()
	{
		logic_uScript_BlockAttachedToTech_tech_271 = local_CircuitBaseTech_Tank;
		logic_uScript_BlockAttachedToTech_block_271 = local_Wire2Block_TankBlock;
		logic_uScript_BlockAttachedToTech_uScript_BlockAttachedToTech_271.In(logic_uScript_BlockAttachedToTech_tech_271, logic_uScript_BlockAttachedToTech_block_271);
		bool num = logic_uScript_BlockAttachedToTech_uScript_BlockAttachedToTech_271.True;
		bool flag = logic_uScript_BlockAttachedToTech_uScript_BlockAttachedToTech_271.False;
		if (num)
		{
			Relay_In_328();
		}
		if (flag)
		{
			Relay_In_281();
		}
	}

	private void Relay_In_275()
	{
		logic_uScript_DoesTechHaveBlockAtPosition_tech_275 = local_CircuitBaseTech_Tank;
		logic_uScript_DoesTechHaveBlockAtPosition_blockType_275 = blockTypeWire;
		logic_uScript_DoesTechHaveBlockAtPosition_uScript_DoesTechHaveBlockAtPosition_275.In(logic_uScript_DoesTechHaveBlockAtPosition_tech_275, logic_uScript_DoesTechHaveBlockAtPosition_blockType_275, logic_uScript_DoesTechHaveBlockAtPosition_localPosition_275);
		if (logic_uScript_DoesTechHaveBlockAtPosition_uScript_DoesTechHaveBlockAtPosition_275.True)
		{
			Relay_In_220();
		}
	}

	private void Relay_In_278()
	{
		logic_uScriptCon_CompareBool_Bool_278 = local_GhostBlockWire2Spawned_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_278.In(logic_uScriptCon_CompareBool_Bool_278);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_278.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_278.False;
		if (num)
		{
			Relay_In_308();
		}
		if (flag)
		{
			Relay_In_338();
		}
	}

	private void Relay_Output1_279()
	{
		Relay_In_306();
	}

	private void Relay_Output2_279()
	{
		Relay_In_275();
	}

	private void Relay_Output3_279()
	{
	}

	private void Relay_Output4_279()
	{
	}

	private void Relay_Output5_279()
	{
	}

	private void Relay_Output6_279()
	{
	}

	private void Relay_Output7_279()
	{
	}

	private void Relay_Output8_279()
	{
	}

	private void Relay_In_279()
	{
		logic_uScriptCon_ManualSwitch_CurrentOutput_279 = local_NumWiresAttached_System_Int32;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_279.In(logic_uScriptCon_ManualSwitch_CurrentOutput_279);
	}

	private void Relay_In_281()
	{
		logic_uScript_PointArrowAtVisible_targetObject_281 = local_Wire2Block_TankBlock;
		logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_281.In(logic_uScript_PointArrowAtVisible_targetObject_281, logic_uScript_PointArrowAtVisible_timeToShowFor_281, logic_uScript_PointArrowAtVisible_offset_281);
		if (logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_281.Out)
		{
			Relay_In_309();
		}
	}

	private void Relay_Output1_282()
	{
		Relay_In_214();
	}

	private void Relay_Output2_282()
	{
		Relay_In_224();
	}

	private void Relay_Output3_282()
	{
	}

	private void Relay_Output4_282()
	{
	}

	private void Relay_Output5_282()
	{
	}

	private void Relay_Output6_282()
	{
	}

	private void Relay_Output7_282()
	{
	}

	private void Relay_Output8_282()
	{
	}

	private void Relay_In_282()
	{
		logic_uScriptCon_ManualSwitch_CurrentOutput_282 = local_NumWiresAttached_System_Int32;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_282.In(logic_uScriptCon_ManualSwitch_CurrentOutput_282);
	}

	private void Relay_True_283()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_283.True(out logic_uScriptAct_SetBool_Target_283);
		local_DraggingWire_System_Boolean = logic_uScriptAct_SetBool_Target_283;
		bool setTrue = logic_uScriptAct_SetBool_uScriptAct_SetBool_283.SetTrue;
		bool setFalse = logic_uScriptAct_SetBool_uScriptAct_SetBool_283.SetFalse;
		if (setTrue)
		{
			Relay_In_323();
		}
		if (setFalse)
		{
			Relay_In_340();
		}
	}

	private void Relay_False_283()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_283.False(out logic_uScriptAct_SetBool_Target_283);
		local_DraggingWire_System_Boolean = logic_uScriptAct_SetBool_Target_283;
		bool setTrue = logic_uScriptAct_SetBool_uScriptAct_SetBool_283.SetTrue;
		bool setFalse = logic_uScriptAct_SetBool_uScriptAct_SetBool_283.SetFalse;
		if (setTrue)
		{
			Relay_In_323();
		}
		if (setFalse)
		{
			Relay_In_340();
		}
	}

	private void Relay_In_285()
	{
		logic_uScript_EnableGlow_targetObject_285 = local_Wire1Block_TankBlock;
		logic_uScript_EnableGlow_uScript_EnableGlow_285.In(logic_uScript_EnableGlow_targetObject_285, logic_uScript_EnableGlow_enable_285);
		if (logic_uScript_EnableGlow_uScript_EnableGlow_285.Out)
		{
			Relay_In_252();
		}
	}

	private void Relay_True_289()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_289.True(out logic_uScriptAct_SetBool_Target_289);
		local_GhostBlockWire1Spawned_System_Boolean = logic_uScriptAct_SetBool_Target_289;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_289.Out)
		{
			Relay_TrySpawnOnTech_241();
		}
	}

	private void Relay_False_289()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_289.False(out logic_uScriptAct_SetBool_Target_289);
		local_GhostBlockWire1Spawned_System_Boolean = logic_uScriptAct_SetBool_Target_289;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_289.Out)
		{
			Relay_TrySpawnOnTech_241();
		}
	}

	private void Relay_TrySpawnOnTech_291()
	{
		int num = 0;
		Array array = ghostBlockWire03;
		if (logic_uScript_SpawnGhostBlocks_ghostBlockData_291.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SpawnGhostBlocks_ghostBlockData_291, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SpawnGhostBlocks_ghostBlockData_291, num, array.Length);
		num += array.Length;
		logic_uScript_SpawnGhostBlocks_ownerNode_291 = owner_Connection_301;
		logic_uScript_SpawnGhostBlocks_targetTech_291 = local_CircuitBaseTech_Tank;
		logic_uScript_SpawnGhostBlocks_Return_291 = logic_uScript_SpawnGhostBlocks_uScript_SpawnGhostBlocks_291.TrySpawnOnTech(logic_uScript_SpawnGhostBlocks_ghostBlockData_291, logic_uScript_SpawnGhostBlocks_ownerNode_291, logic_uScript_SpawnGhostBlocks_targetTech_291);
		local_329_TankBlockArray = logic_uScript_SpawnGhostBlocks_Return_291;
	}

	private void Relay_In_293()
	{
		logic_uScript_IsPlayerInteractingWithBlock_block_293 = local_Wire1Block_TankBlock;
		logic_uScript_IsPlayerInteractingWithBlock_uScript_IsPlayerInteractingWithBlock_293.In(logic_uScript_IsPlayerInteractingWithBlock_block_293);
		bool dragging = logic_uScript_IsPlayerInteractingWithBlock_uScript_IsPlayerInteractingWithBlock_293.Dragging;
		bool notDragging = logic_uScript_IsPlayerInteractingWithBlock_uScript_IsPlayerInteractingWithBlock_293.NotDragging;
		if (dragging)
		{
			Relay_In_231();
		}
		if (notDragging)
		{
			Relay_In_321();
		}
	}

	private void Relay_In_295()
	{
		logic_uScript_EnableGlow_targetObject_295 = local_Wire3Block_TankBlock;
		logic_uScript_EnableGlow_uScript_EnableGlow_295.In(logic_uScript_EnableGlow_targetObject_295, logic_uScript_EnableGlow_enable_295);
		if (logic_uScript_EnableGlow_uScript_EnableGlow_295.Out)
		{
			Relay_In_318();
		}
	}

	private void Relay_In_298()
	{
		logic_uScript_EnableGlow_targetObject_298 = local_Wire2Block_TankBlock;
		logic_uScript_EnableGlow_uScript_EnableGlow_298.In(logic_uScript_EnableGlow_targetObject_298, logic_uScript_EnableGlow_enable_298);
		if (logic_uScript_EnableGlow_uScript_EnableGlow_298.Out)
		{
			Relay_In_295();
		}
	}

	private void Relay_In_306()
	{
		logic_uScript_DoesTechHaveBlockAtPosition_tech_306 = local_CircuitBaseTech_Tank;
		logic_uScript_DoesTechHaveBlockAtPosition_blockType_306 = blockTypeWire;
		logic_uScript_DoesTechHaveBlockAtPosition_uScript_DoesTechHaveBlockAtPosition_306.In(logic_uScript_DoesTechHaveBlockAtPosition_tech_306, logic_uScript_DoesTechHaveBlockAtPosition_blockType_306, logic_uScript_DoesTechHaveBlockAtPosition_localPosition_306);
		if (logic_uScript_DoesTechHaveBlockAtPosition_uScript_DoesTechHaveBlockAtPosition_306.True)
		{
			Relay_In_220();
		}
	}

	private void Relay_In_307()
	{
		logic_uScript_EnableGlow_targetObject_307 = local_Wire3Block_TankBlock;
		logic_uScript_EnableGlow_uScript_EnableGlow_307.In(logic_uScript_EnableGlow_targetObject_307, logic_uScript_EnableGlow_enable_307);
	}

	private void Relay_In_308()
	{
		logic_uScript_EnableGlow_targetObject_308 = local_GhostBlockWire2_TankBlock;
		logic_uScript_EnableGlow_uScript_EnableGlow_308.In(logic_uScript_EnableGlow_targetObject_308, logic_uScript_EnableGlow_enable_308);
		if (logic_uScript_EnableGlow_uScript_EnableGlow_308.Out)
		{
			Relay_In_315();
		}
	}

	private void Relay_In_309()
	{
		logic_uScript_EnableGlow_targetObject_309 = local_Wire2Block_TankBlock;
		logic_uScript_EnableGlow_uScript_EnableGlow_309.In(logic_uScript_EnableGlow_targetObject_309, logic_uScript_EnableGlow_enable_309);
		if (logic_uScript_EnableGlow_uScript_EnableGlow_309.Out)
		{
			Relay_In_326();
		}
	}

	private void Relay_In_310()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_310.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_310.Out)
		{
			Relay_In_330();
		}
	}

	private void Relay_In_311()
	{
		logic_uScript_EnableGlow_targetObject_311 = local_GhostBlockWire1_TankBlock;
		logic_uScript_EnableGlow_uScript_EnableGlow_311.In(logic_uScript_EnableGlow_targetObject_311, logic_uScript_EnableGlow_enable_311);
		if (logic_uScript_EnableGlow_uScript_EnableGlow_311.Out)
		{
			Relay_In_278();
		}
	}

	private void Relay_In_313()
	{
		logic_uScript_IsPlayerInteractingWithBlock_block_313 = local_Wire3Block_TankBlock;
		logic_uScript_IsPlayerInteractingWithBlock_uScript_IsPlayerInteractingWithBlock_313.In(logic_uScript_IsPlayerInteractingWithBlock_block_313);
		bool dragging = logic_uScript_IsPlayerInteractingWithBlock_uScript_IsPlayerInteractingWithBlock_313.Dragging;
		bool notDragging = logic_uScript_IsPlayerInteractingWithBlock_uScript_IsPlayerInteractingWithBlock_313.NotDragging;
		if (dragging)
		{
			Relay_True_283();
		}
		if (notDragging)
		{
			Relay_False_283();
		}
	}

	private void Relay_In_314()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_314.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_314.Out)
		{
			Relay_In_251();
		}
	}

	private void Relay_In_315()
	{
		logic_uScriptCon_CompareBool_Bool_315 = local_GhostBlockWire3Spawned_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_315.In(logic_uScriptCon_CompareBool_Bool_315);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_315.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_315.False;
		if (num)
		{
			Relay_In_216();
		}
		if (flag)
		{
			Relay_In_226();
		}
	}

	private void Relay_In_317()
	{
		logic_uScript_EnableGlow_targetObject_317 = local_Wire1Block_TankBlock;
		logic_uScript_EnableGlow_uScript_EnableGlow_317.In(logic_uScript_EnableGlow_targetObject_317, logic_uScript_EnableGlow_enable_317);
		if (logic_uScript_EnableGlow_uScript_EnableGlow_317.Out)
		{
			Relay_In_238();
		}
	}

	private void Relay_In_318()
	{
		logic_uScriptCon_CompareInt_A_318 = local_NumWiresAttached_System_Int32;
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_318.In(logic_uScriptCon_CompareInt_A_318, logic_uScriptCon_CompareInt_B_318);
		bool equalTo = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_318.EqualTo;
		bool notEqualTo = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_318.NotEqualTo;
		if (equalTo)
		{
			Relay_In_339();
		}
		if (notEqualTo)
		{
			Relay_In_282();
		}
	}

	private void Relay_In_321()
	{
		logic_uScript_IsPlayerInteractingWithBlock_block_321 = local_Wire2Block_TankBlock;
		logic_uScript_IsPlayerInteractingWithBlock_uScript_IsPlayerInteractingWithBlock_321.In(logic_uScript_IsPlayerInteractingWithBlock_block_321);
		bool dragging = logic_uScript_IsPlayerInteractingWithBlock_uScript_IsPlayerInteractingWithBlock_321.Dragging;
		bool notDragging = logic_uScript_IsPlayerInteractingWithBlock_uScript_IsPlayerInteractingWithBlock_321.NotDragging;
		if (dragging)
		{
			Relay_In_314();
		}
		if (notDragging)
		{
			Relay_In_313();
		}
	}

	private void Relay_In_322()
	{
		logic_uScript_EnableGlow_targetObject_322 = local_GhostBlockWire1_TankBlock;
		logic_uScript_EnableGlow_uScript_EnableGlow_322.In(logic_uScript_EnableGlow_targetObject_322, logic_uScript_EnableGlow_enable_322);
	}

	private void Relay_In_323()
	{
		logic_uScript_EnableGlow_targetObject_323 = local_Wire1Block_TankBlock;
		logic_uScript_EnableGlow_uScript_EnableGlow_323.In(logic_uScript_EnableGlow_targetObject_323, logic_uScript_EnableGlow_enable_323);
		if (logic_uScript_EnableGlow_uScript_EnableGlow_323.Out)
		{
			Relay_In_298();
		}
	}

	private void Relay_AtIndex_325()
	{
		int num = 0;
		Array array = local_329_TankBlockArray;
		if (logic_uScript_AccessListBlock_blockList_325.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListBlock_blockList_325, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListBlock_blockList_325, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListBlock_uScript_AccessListBlock_325.AtIndex(ref logic_uScript_AccessListBlock_blockList_325, logic_uScript_AccessListBlock_index_325, out logic_uScript_AccessListBlock_value_325);
		local_329_TankBlockArray = logic_uScript_AccessListBlock_blockList_325;
		local_GhostBlockWire3_TankBlock = logic_uScript_AccessListBlock_value_325;
		if (logic_uScript_AccessListBlock_uScript_AccessListBlock_325.Out)
		{
			Relay_In_222();
		}
	}

	private void Relay_In_326()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_326.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_326.Out)
		{
			Relay_In_310();
		}
	}

	private void Relay_In_328()
	{
		logic_uScript_EnableGlow_targetObject_328 = local_Wire2Block_TankBlock;
		logic_uScript_EnableGlow_uScript_EnableGlow_328.In(logic_uScript_EnableGlow_targetObject_328, logic_uScript_EnableGlow_enable_328);
		if (logic_uScript_EnableGlow_uScript_EnableGlow_328.Out)
		{
			Relay_In_335();
		}
	}

	private void Relay_In_330()
	{
		logic_uScriptCon_CompareInt_A_330 = local_NumWiresAttached_System_Int32;
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_330.In(logic_uScriptCon_CompareInt_A_330, logic_uScriptCon_CompareInt_B_330);
		bool equalTo = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_330.EqualTo;
		bool notEqualTo = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_330.NotEqualTo;
		if (equalTo)
		{
			Relay_In_215();
		}
		if (notEqualTo)
		{
			Relay_In_279();
		}
	}

	private void Relay_In_335()
	{
		logic_uScript_BlockAttachedToTech_tech_335 = local_CircuitBaseTech_Tank;
		logic_uScript_BlockAttachedToTech_block_335 = local_Wire3Block_TankBlock;
		logic_uScript_BlockAttachedToTech_uScript_BlockAttachedToTech_335.In(logic_uScript_BlockAttachedToTech_tech_335, logic_uScript_BlockAttachedToTech_block_335);
		bool num = logic_uScript_BlockAttachedToTech_uScript_BlockAttachedToTech_335.True;
		bool flag = logic_uScript_BlockAttachedToTech_uScript_BlockAttachedToTech_335.False;
		if (num)
		{
			Relay_In_237();
		}
		if (flag)
		{
			Relay_In_336();
		}
	}

	private void Relay_In_336()
	{
		logic_uScript_PointArrowAtVisible_targetObject_336 = local_Wire3Block_TankBlock;
		logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_336.In(logic_uScript_PointArrowAtVisible_targetObject_336, logic_uScript_PointArrowAtVisible_timeToShowFor_336, logic_uScript_PointArrowAtVisible_offset_336);
		if (logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_336.Out)
		{
			Relay_In_246();
		}
	}

	private void Relay_In_338()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_338.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_338.Out)
		{
			Relay_In_315();
		}
	}

	private void Relay_In_339()
	{
		logic_uScriptCon_CompareBool_Bool_339 = local_GhostBlockWire1Spawned_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_339.In(logic_uScriptCon_CompareBool_Bool_339);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_339.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_339.False;
		if (num)
		{
			Relay_AtIndex_217();
		}
		if (flag)
		{
			Relay_True_289();
		}
	}

	private void Relay_In_340()
	{
		logic_uScriptCon_CompareBool_Bool_340 = local_GhostBlockWire1Spawned_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_340.In(logic_uScriptCon_CompareBool_Bool_340);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_340.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_340.False;
		if (num)
		{
			Relay_In_311();
		}
		if (flag)
		{
			Relay_In_253();
		}
	}

	private void Relay_True_343()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_343.True(out logic_uScriptAct_SetBool_Target_343);
		local_GhostBlockWire2Spawned_System_Boolean = logic_uScriptAct_SetBool_Target_343;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_343.Out)
		{
			Relay_TrySpawnOnTech_267();
		}
	}

	private void Relay_False_343()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_343.False(out logic_uScriptAct_SetBool_Target_343);
		local_GhostBlockWire2Spawned_System_Boolean = logic_uScriptAct_SetBool_Target_343;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_343.Out)
		{
			Relay_TrySpawnOnTech_267();
		}
	}

	private void Relay_In_346()
	{
		logic_uScript_AddMessage_messageData_346 = msg04PickupWire;
		logic_uScript_AddMessage_speaker_346 = messageSpeaker;
		logic_uScript_AddMessage_Return_346 = logic_uScript_AddMessage_uScript_AddMessage_346.In(logic_uScript_AddMessage_messageData_346, logic_uScript_AddMessage_speaker_346);
		if (logic_uScript_AddMessage_uScript_AddMessage_346.Out)
		{
			Relay_In_293();
		}
	}

	private void Relay_In_347()
	{
		logic_uScriptCon_CompareBool_Bool_347 = local_msgToggleIntroShown_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_347.In(logic_uScriptCon_CompareBool_Bool_347);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_347.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_347.False;
		if (num)
		{
			Relay_In_346();
		}
		if (flag)
		{
			Relay_True_502();
		}
	}

	private void Relay_In_351()
	{
		logic_uScriptAct_AddInt_v2_A_351 = local_ToggleInteractions_System_Int32;
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_351.In(logic_uScriptAct_AddInt_v2_A_351, logic_uScriptAct_AddInt_v2_B_351, out logic_uScriptAct_AddInt_v2_IntResult_351, out logic_uScriptAct_AddInt_v2_FloatResult_351);
		local_ToggleInteractions_System_Int32 = logic_uScriptAct_AddInt_v2_IntResult_351;
		if (logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_351.Out)
		{
			Relay_In_365();
		}
	}

	private void Relay_Save_Out_354()
	{
		Relay_Save_380();
	}

	private void Relay_Load_Out_354()
	{
		Relay_Load_380();
	}

	private void Relay_Restart_Out_354()
	{
		Relay_Restart_380();
	}

	private void Relay_Save_354()
	{
		logic_SubGraph_SaveLoadInt_integer_354 = local_ToggleInteractions_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_354 = local_ToggleInteractions_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_354.Save(logic_SubGraph_SaveLoadInt_restartValue_354, ref logic_SubGraph_SaveLoadInt_integer_354, logic_SubGraph_SaveLoadInt_intAsVariable_354, logic_SubGraph_SaveLoadInt_uniqueID_354);
	}

	private void Relay_Load_354()
	{
		logic_SubGraph_SaveLoadInt_integer_354 = local_ToggleInteractions_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_354 = local_ToggleInteractions_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_354.Load(logic_SubGraph_SaveLoadInt_restartValue_354, ref logic_SubGraph_SaveLoadInt_integer_354, logic_SubGraph_SaveLoadInt_intAsVariable_354, logic_SubGraph_SaveLoadInt_uniqueID_354);
	}

	private void Relay_Restart_354()
	{
		logic_SubGraph_SaveLoadInt_integer_354 = local_ToggleInteractions_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_354 = local_ToggleInteractions_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_354.Restart(logic_SubGraph_SaveLoadInt_restartValue_354, ref logic_SubGraph_SaveLoadInt_integer_354, logic_SubGraph_SaveLoadInt_intAsVariable_354, logic_SubGraph_SaveLoadInt_uniqueID_354);
	}

	private void Relay_In_355()
	{
		logic_uScriptCon_CompareInt_A_355 = local_ToggleInteractions_System_Int32;
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_355.In(logic_uScriptCon_CompareInt_A_355, logic_uScriptCon_CompareInt_B_355);
		bool equalTo = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_355.EqualTo;
		bool notEqualTo = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_355.NotEqualTo;
		if (equalTo)
		{
			Relay_In_377();
		}
		if (notEqualTo)
		{
			Relay_In_356();
		}
	}

	private void Relay_In_356()
	{
		logic_uScriptCon_CompareInt_A_356 = local_ToggleInteractions_System_Int32;
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_356.In(logic_uScriptCon_CompareInt_A_356, logic_uScriptCon_CompareInt_B_356);
		bool equalTo = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_356.EqualTo;
		bool notEqualTo = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_356.NotEqualTo;
		if (equalTo)
		{
			Relay_In_184();
		}
		if (notEqualTo)
		{
			Relay_In_358();
		}
	}

	private void Relay_Output1_358()
	{
		Relay_In_197();
	}

	private void Relay_Output2_358()
	{
	}

	private void Relay_Output3_358()
	{
	}

	private void Relay_Output4_358()
	{
	}

	private void Relay_Output5_358()
	{
	}

	private void Relay_Output6_358()
	{
	}

	private void Relay_Output7_358()
	{
	}

	private void Relay_Output8_358()
	{
	}

	private void Relay_In_358()
	{
		logic_uScriptCon_ManualSwitch_CurrentOutput_358 = local_ToggleInteractions_System_Int32;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_358.In(logic_uScriptCon_ManualSwitch_CurrentOutput_358);
	}

	private void Relay_In_361()
	{
		logic_uScriptAct_AddInt_v2_A_361 = local_ToggleInteractions_System_Int32;
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_361.In(logic_uScriptAct_AddInt_v2_A_361, logic_uScriptAct_AddInt_v2_B_361, out logic_uScriptAct_AddInt_v2_IntResult_361, out logic_uScriptAct_AddInt_v2_FloatResult_361);
		local_ToggleInteractions_System_Int32 = logic_uScriptAct_AddInt_v2_IntResult_361;
		if (logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_361.Out)
		{
			Relay_In_368();
		}
	}

	private void Relay_In_362()
	{
		logic_uScript_PointArrowAtVisible_targetObject_362 = local_ToggleBlock_TankBlock;
		logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_362.In(logic_uScript_PointArrowAtVisible_targetObject_362, logic_uScript_PointArrowAtVisible_timeToShowFor_362, logic_uScript_PointArrowAtVisible_offset_362);
		if (logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_362.Out)
		{
			Relay_In_363();
		}
	}

	private void Relay_In_363()
	{
		logic_uScript_EnableGlow_targetObject_363 = local_ToggleBlock_TankBlock;
		logic_uScript_EnableGlow_uScript_EnableGlow_363.In(logic_uScript_EnableGlow_targetObject_363, logic_uScript_EnableGlow_enable_363);
		if (logic_uScript_EnableGlow_uScript_EnableGlow_363.Out)
		{
			Relay_In_140();
		}
	}

	private void Relay_In_365()
	{
		logic_uScript_PointArrowAtVisible_targetObject_365 = local_ToggleBlock_TankBlock;
		logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_365.In(logic_uScript_PointArrowAtVisible_targetObject_365, logic_uScript_PointArrowAtVisible_timeToShowFor_365, logic_uScript_PointArrowAtVisible_offset_365);
		if (logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_365.Out)
		{
			Relay_In_366();
		}
	}

	private void Relay_In_366()
	{
		logic_uScript_EnableGlow_targetObject_366 = local_ToggleBlock_TankBlock;
		logic_uScript_EnableGlow_uScript_EnableGlow_366.In(logic_uScript_EnableGlow_targetObject_366, logic_uScript_EnableGlow_enable_366);
		if (logic_uScript_EnableGlow_uScript_EnableGlow_366.Out)
		{
			Relay_In_371();
		}
	}

	private void Relay_In_368()
	{
		logic_uScript_PointArrowAtVisible_targetObject_368 = local_ToggleBlock_TankBlock;
		logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_368.In(logic_uScript_PointArrowAtVisible_targetObject_368, logic_uScript_PointArrowAtVisible_timeToShowFor_368, logic_uScript_PointArrowAtVisible_offset_368);
		if (logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_368.Out)
		{
			Relay_In_369();
		}
	}

	private void Relay_In_369()
	{
		logic_uScript_EnableGlow_targetObject_369 = local_ToggleBlock_TankBlock;
		logic_uScript_EnableGlow_uScript_EnableGlow_369.In(logic_uScript_EnableGlow_targetObject_369, logic_uScript_EnableGlow_enable_369);
		if (logic_uScript_EnableGlow_uScript_EnableGlow_369.Out)
		{
			Relay_In_372();
		}
	}

	private void Relay_In_371()
	{
		logic_uScript_HideArrow_uScript_HideArrow_371.In();
	}

	private void Relay_In_372()
	{
		logic_uScript_HideArrow_uScript_HideArrow_372.In();
	}

	private void Relay_In_374()
	{
		logic_uScript_AddMessage_messageData_374 = msg11ToggleIsOff;
		logic_uScript_AddMessage_speaker_374 = messageSpeaker;
		logic_uScript_AddMessage_Return_374 = logic_uScript_AddMessage_uScript_AddMessage_374.In(logic_uScript_AddMessage_messageData_374, logic_uScript_AddMessage_speaker_374);
		if (logic_uScript_AddMessage_uScript_AddMessage_374.Shown)
		{
			Relay_True_378();
		}
	}

	private void Relay_In_377()
	{
		logic_uScriptCon_CompareBool_Bool_377 = local_msgToggleOffShown_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_377.In(logic_uScriptCon_CompareBool_Bool_377);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_377.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_377.False;
		if (num)
		{
			Relay_In_142();
		}
		if (flag)
		{
			Relay_In_374();
		}
	}

	private void Relay_True_378()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_378.True(out logic_uScriptAct_SetBool_Target_378);
		local_msgToggleOffShown_System_Boolean = logic_uScriptAct_SetBool_Target_378;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_378.Out)
		{
			Relay_In_142();
		}
	}

	private void Relay_False_378()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_378.False(out logic_uScriptAct_SetBool_Target_378);
		local_msgToggleOffShown_System_Boolean = logic_uScriptAct_SetBool_Target_378;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_378.Out)
		{
			Relay_In_142();
		}
	}

	private void Relay_Save_Out_380()
	{
		Relay_Save_143();
	}

	private void Relay_Load_Out_380()
	{
		Relay_Load_143();
	}

	private void Relay_Restart_Out_380()
	{
		Relay_Set_False_143();
	}

	private void Relay_Save_380()
	{
		logic_SubGraph_SaveLoadInt_restartValue_380 = local_StartingHour_System_Int32;
		logic_SubGraph_SaveLoadInt_integer_380 = local_StartingHour_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_380 = local_StartingHour_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_380.Save(logic_SubGraph_SaveLoadInt_restartValue_380, ref logic_SubGraph_SaveLoadInt_integer_380, logic_SubGraph_SaveLoadInt_intAsVariable_380, logic_SubGraph_SaveLoadInt_uniqueID_380);
	}

	private void Relay_Load_380()
	{
		logic_SubGraph_SaveLoadInt_restartValue_380 = local_StartingHour_System_Int32;
		logic_SubGraph_SaveLoadInt_integer_380 = local_StartingHour_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_380 = local_StartingHour_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_380.Load(logic_SubGraph_SaveLoadInt_restartValue_380, ref logic_SubGraph_SaveLoadInt_integer_380, logic_SubGraph_SaveLoadInt_intAsVariable_380, logic_SubGraph_SaveLoadInt_uniqueID_380);
	}

	private void Relay_Restart_380()
	{
		logic_SubGraph_SaveLoadInt_restartValue_380 = local_StartingHour_System_Int32;
		logic_SubGraph_SaveLoadInt_integer_380 = local_StartingHour_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_380 = local_StartingHour_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_380.Restart(logic_SubGraph_SaveLoadInt_restartValue_380, ref logic_SubGraph_SaveLoadInt_integer_380, logic_SubGraph_SaveLoadInt_intAsVariable_380, logic_SubGraph_SaveLoadInt_uniqueID_380);
	}

	private void Relay_Save_Out_383()
	{
		Relay_Save_387();
	}

	private void Relay_Load_Out_383()
	{
		Relay_Load_387();
	}

	private void Relay_Restart_Out_383()
	{
		Relay_Set_False_387();
	}

	private void Relay_Save_383()
	{
		logic_SubGraph_SaveLoadBool_boolean_383 = local_msgToggleOffShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_383 = local_msgToggleOffShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_383.Save(ref logic_SubGraph_SaveLoadBool_boolean_383, logic_SubGraph_SaveLoadBool_boolAsVariable_383, logic_SubGraph_SaveLoadBool_uniqueID_383);
	}

	private void Relay_Load_383()
	{
		logic_SubGraph_SaveLoadBool_boolean_383 = local_msgToggleOffShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_383 = local_msgToggleOffShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_383.Load(ref logic_SubGraph_SaveLoadBool_boolean_383, logic_SubGraph_SaveLoadBool_boolAsVariable_383, logic_SubGraph_SaveLoadBool_uniqueID_383);
	}

	private void Relay_Set_True_383()
	{
		logic_SubGraph_SaveLoadBool_boolean_383 = local_msgToggleOffShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_383 = local_msgToggleOffShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_383.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_383, logic_SubGraph_SaveLoadBool_boolAsVariable_383, logic_SubGraph_SaveLoadBool_uniqueID_383);
	}

	private void Relay_Set_False_383()
	{
		logic_SubGraph_SaveLoadBool_boolean_383 = local_msgToggleOffShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_383 = local_msgToggleOffShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_383.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_383, logic_SubGraph_SaveLoadBool_boolAsVariable_383, logic_SubGraph_SaveLoadBool_uniqueID_383);
	}

	private void Relay_True_385()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_385.True(out logic_uScriptAct_SetBool_Target_385);
		local_msgIntroShown_System_Boolean = logic_uScriptAct_SetBool_Target_385;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_385.Out)
		{
			Relay_In_19();
		}
	}

	private void Relay_False_385()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_385.False(out logic_uScriptAct_SetBool_Target_385);
		local_msgIntroShown_System_Boolean = logic_uScriptAct_SetBool_Target_385;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_385.Out)
		{
			Relay_In_19();
		}
	}

	private void Relay_Save_Out_387()
	{
		Relay_Save_389();
	}

	private void Relay_Load_Out_387()
	{
		Relay_Load_389();
	}

	private void Relay_Restart_Out_387()
	{
		Relay_Set_False_389();
	}

	private void Relay_Save_387()
	{
		logic_SubGraph_SaveLoadBool_boolean_387 = local_msgCompleteIsOnShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_387 = local_msgCompleteIsOnShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_387.Save(ref logic_SubGraph_SaveLoadBool_boolean_387, logic_SubGraph_SaveLoadBool_boolAsVariable_387, logic_SubGraph_SaveLoadBool_uniqueID_387);
	}

	private void Relay_Load_387()
	{
		logic_SubGraph_SaveLoadBool_boolean_387 = local_msgCompleteIsOnShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_387 = local_msgCompleteIsOnShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_387.Load(ref logic_SubGraph_SaveLoadBool_boolean_387, logic_SubGraph_SaveLoadBool_boolAsVariable_387, logic_SubGraph_SaveLoadBool_uniqueID_387);
	}

	private void Relay_Set_True_387()
	{
		logic_SubGraph_SaveLoadBool_boolean_387 = local_msgCompleteIsOnShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_387 = local_msgCompleteIsOnShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_387.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_387, logic_SubGraph_SaveLoadBool_boolAsVariable_387, logic_SubGraph_SaveLoadBool_uniqueID_387);
	}

	private void Relay_Set_False_387()
	{
		logic_SubGraph_SaveLoadBool_boolean_387 = local_msgCompleteIsOnShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_387 = local_msgCompleteIsOnShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_387.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_387, logic_SubGraph_SaveLoadBool_boolAsVariable_387, logic_SubGraph_SaveLoadBool_uniqueID_387);
	}

	private void Relay_Save_Out_389()
	{
		Relay_Save_105();
	}

	private void Relay_Load_Out_389()
	{
		Relay_Load_105();
	}

	private void Relay_Restart_Out_389()
	{
		Relay_Set_False_105();
	}

	private void Relay_Save_389()
	{
		logic_SubGraph_SaveLoadBool_boolean_389 = local_msgBeamSensorAttachedIsOnShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_389 = local_msgBeamSensorAttachedIsOnShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_389.Save(ref logic_SubGraph_SaveLoadBool_boolean_389, logic_SubGraph_SaveLoadBool_boolAsVariable_389, logic_SubGraph_SaveLoadBool_uniqueID_389);
	}

	private void Relay_Load_389()
	{
		logic_SubGraph_SaveLoadBool_boolean_389 = local_msgBeamSensorAttachedIsOnShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_389 = local_msgBeamSensorAttachedIsOnShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_389.Load(ref logic_SubGraph_SaveLoadBool_boolean_389, logic_SubGraph_SaveLoadBool_boolAsVariable_389, logic_SubGraph_SaveLoadBool_uniqueID_389);
	}

	private void Relay_Set_True_389()
	{
		logic_SubGraph_SaveLoadBool_boolean_389 = local_msgBeamSensorAttachedIsOnShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_389 = local_msgBeamSensorAttachedIsOnShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_389.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_389, logic_SubGraph_SaveLoadBool_boolAsVariable_389, logic_SubGraph_SaveLoadBool_uniqueID_389);
	}

	private void Relay_Set_False_389()
	{
		logic_SubGraph_SaveLoadBool_boolean_389 = local_msgBeamSensorAttachedIsOnShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_389 = local_msgBeamSensorAttachedIsOnShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_389.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_389, logic_SubGraph_SaveLoadBool_boolAsVariable_389, logic_SubGraph_SaveLoadBool_uniqueID_389);
	}

	private void Relay_In_391()
	{
		logic_uScriptCon_CompareBool_Bool_391 = local_CanInteractWithToggle_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_391.In(logic_uScriptCon_CompareBool_Bool_391);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_391.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_391.False;
		if (num)
		{
			Relay_In_78();
		}
		if (flag)
		{
			Relay_In_17();
		}
	}

	private void Relay_ShowLabel_393()
	{
		logic_uScriptAct_PrintText_Text_393 = local_406_System_String;
		logic_uScriptAct_PrintText_uScriptAct_PrintText_393.ShowLabel(logic_uScriptAct_PrintText_Text_393, logic_uScriptAct_PrintText_FontSize_393, logic_uScriptAct_PrintText_FontStyle_393, logic_uScriptAct_PrintText_FontColor_393, logic_uScriptAct_PrintText_textAnchor_393, logic_uScriptAct_PrintText_EdgePadding_393, logic_uScriptAct_PrintText_time_393);
	}

	private void Relay_HideLabel_393()
	{
		logic_uScriptAct_PrintText_Text_393 = local_406_System_String;
		logic_uScriptAct_PrintText_uScriptAct_PrintText_393.HideLabel(logic_uScriptAct_PrintText_Text_393, logic_uScriptAct_PrintText_FontSize_393, logic_uScriptAct_PrintText_FontStyle_393, logic_uScriptAct_PrintText_FontColor_393, logic_uScriptAct_PrintText_textAnchor_393, logic_uScriptAct_PrintText_EdgePadding_393, logic_uScriptAct_PrintText_time_393);
	}

	private void Relay_In_395()
	{
		int num = 0;
		if (logic_uScriptAct_Concatenate_A_395.Length <= num)
		{
			Array.Resize(ref logic_uScriptAct_Concatenate_A_395, num + 1);
		}
		logic_uScriptAct_Concatenate_A_395[num++] = local_405_System_String;
		int num2 = 0;
		if (logic_uScriptAct_Concatenate_B_395.Length <= num2)
		{
			Array.Resize(ref logic_uScriptAct_Concatenate_B_395, num2 + 1);
		}
		logic_uScriptAct_Concatenate_B_395[num2++] = local_401_System_String;
		logic_uScriptAct_Concatenate_uScriptAct_Concatenate_395.In(logic_uScriptAct_Concatenate_A_395, logic_uScriptAct_Concatenate_B_395, logic_uScriptAct_Concatenate_Separator_395, out logic_uScriptAct_Concatenate_Result_395);
		local_407_System_String = logic_uScriptAct_Concatenate_Result_395;
		if (logic_uScriptAct_Concatenate_uScriptAct_Concatenate_395.Out)
		{
			Relay_In_410();
		}
	}

	private void Relay_In_397()
	{
		int num = 0;
		if (logic_uScriptAct_Concatenate_A_397.Length <= num)
		{
			Array.Resize(ref logic_uScriptAct_Concatenate_A_397, num + 1);
		}
		logic_uScriptAct_Concatenate_A_397[num++] = local_403_System_String;
		int num2 = 0;
		if (logic_uScriptAct_Concatenate_B_397.Length <= num2)
		{
			Array.Resize(ref logic_uScriptAct_Concatenate_B_397, num2 + 1);
		}
		logic_uScriptAct_Concatenate_B_397[num2++] = local_StartingHour_System_Int32;
		logic_uScriptAct_Concatenate_uScriptAct_Concatenate_397.In(logic_uScriptAct_Concatenate_A_397, logic_uScriptAct_Concatenate_B_397, logic_uScriptAct_Concatenate_Separator_397, out logic_uScriptAct_Concatenate_Result_397);
		local_405_System_String = logic_uScriptAct_Concatenate_Result_397;
		if (logic_uScriptAct_Concatenate_uScriptAct_Concatenate_397.Out)
		{
			Relay_In_395();
		}
	}

	private void Relay_In_398()
	{
		int num = 0;
		if (logic_uScriptAct_Concatenate_A_398.Length <= num)
		{
			Array.Resize(ref logic_uScriptAct_Concatenate_A_398, num + 1);
		}
		logic_uScriptAct_Concatenate_A_398[num++] = local_404_System_String;
		int num2 = 0;
		if (logic_uScriptAct_Concatenate_B_398.Length <= num2)
		{
			Array.Resize(ref logic_uScriptAct_Concatenate_B_398, num2 + 1);
		}
		logic_uScriptAct_Concatenate_B_398[num2++] = local_Stage_System_Int32;
		logic_uScriptAct_Concatenate_uScriptAct_Concatenate_398.In(logic_uScriptAct_Concatenate_A_398, logic_uScriptAct_Concatenate_B_398, logic_uScriptAct_Concatenate_Separator_398, out logic_uScriptAct_Concatenate_Result_398);
		local_413_System_String = logic_uScriptAct_Concatenate_Result_398;
		if (logic_uScriptAct_Concatenate_uScriptAct_Concatenate_398.Out)
		{
			Relay_In_400();
		}
	}

	private void Relay_In_400()
	{
		int num = 0;
		if (logic_uScriptAct_Concatenate_A_400.Length <= num)
		{
			Array.Resize(ref logic_uScriptAct_Concatenate_A_400, num + 1);
		}
		logic_uScriptAct_Concatenate_A_400[num++] = local_413_System_String;
		int num2 = 0;
		if (logic_uScriptAct_Concatenate_B_400.Length <= num2)
		{
			Array.Resize(ref logic_uScriptAct_Concatenate_B_400, num2 + 1);
		}
		logic_uScriptAct_Concatenate_B_400[num2++] = local_411_System_String;
		logic_uScriptAct_Concatenate_uScriptAct_Concatenate_400.In(logic_uScriptAct_Concatenate_A_400, logic_uScriptAct_Concatenate_B_400, logic_uScriptAct_Concatenate_Separator_400, out logic_uScriptAct_Concatenate_Result_400);
		local_403_System_String = logic_uScriptAct_Concatenate_Result_400;
		if (logic_uScriptAct_Concatenate_uScriptAct_Concatenate_400.Out)
		{
			Relay_In_397();
		}
	}

	private void Relay_In_408()
	{
		int num = 0;
		if (logic_uScriptAct_Concatenate_A_408.Length <= num)
		{
			Array.Resize(ref logic_uScriptAct_Concatenate_A_408, num + 1);
		}
		logic_uScriptAct_Concatenate_A_408[num++] = local_394_System_String;
		int num2 = 0;
		if (logic_uScriptAct_Concatenate_B_408.Length <= num2)
		{
			Array.Resize(ref logic_uScriptAct_Concatenate_B_408, num2 + 1);
		}
		logic_uScriptAct_Concatenate_B_408[num2++] = local_412_System_String;
		logic_uScriptAct_Concatenate_uScriptAct_Concatenate_408.In(logic_uScriptAct_Concatenate_A_408, logic_uScriptAct_Concatenate_B_408, logic_uScriptAct_Concatenate_Separator_408, out logic_uScriptAct_Concatenate_Result_408);
		local_404_System_String = logic_uScriptAct_Concatenate_Result_408;
		if (logic_uScriptAct_Concatenate_uScriptAct_Concatenate_408.Out)
		{
			Relay_In_398();
		}
	}

	private void Relay_In_409()
	{
		int num = 0;
		if (logic_uScriptAct_Concatenate_A_409.Length <= num)
		{
			Array.Resize(ref logic_uScriptAct_Concatenate_A_409, num + 1);
		}
		logic_uScriptAct_Concatenate_A_409[num++] = local_402_System_String;
		int num2 = 0;
		if (logic_uScriptAct_Concatenate_B_409.Length <= num2)
		{
			Array.Resize(ref logic_uScriptAct_Concatenate_B_409, num2 + 1);
		}
		logic_uScriptAct_Concatenate_B_409[num2++] = local_BeamSensorIsOn_System_Boolean;
		logic_uScriptAct_Concatenate_uScriptAct_Concatenate_409.In(logic_uScriptAct_Concatenate_A_409, logic_uScriptAct_Concatenate_B_409, logic_uScriptAct_Concatenate_Separator_409, out logic_uScriptAct_Concatenate_Result_409);
		local_394_System_String = logic_uScriptAct_Concatenate_Result_409;
		if (logic_uScriptAct_Concatenate_uScriptAct_Concatenate_409.Out)
		{
			Relay_In_408();
		}
	}

	private void Relay_In_410()
	{
		int num = 0;
		if (logic_uScriptAct_Concatenate_A_410.Length <= num)
		{
			Array.Resize(ref logic_uScriptAct_Concatenate_A_410, num + 1);
		}
		logic_uScriptAct_Concatenate_A_410[num++] = local_407_System_String;
		int num2 = 0;
		if (logic_uScriptAct_Concatenate_B_410.Length <= num2)
		{
			Array.Resize(ref logic_uScriptAct_Concatenate_B_410, num2 + 1);
		}
		logic_uScriptAct_Concatenate_B_410[num2++] = local_NumWiresAttached_System_Int32;
		logic_uScriptAct_Concatenate_uScriptAct_Concatenate_410.In(logic_uScriptAct_Concatenate_A_410, logic_uScriptAct_Concatenate_B_410, logic_uScriptAct_Concatenate_Separator_410, out logic_uScriptAct_Concatenate_Result_410);
		local_406_System_String = logic_uScriptAct_Concatenate_Result_410;
		if (logic_uScriptAct_Concatenate_uScriptAct_Concatenate_410.Out)
		{
			Relay_ShowLabel_393();
		}
	}

	private void Relay_In_419()
	{
		logic_uScript_SetTimeOfDay_uScript_SetTimeOfDay_419.In(logic_uScript_SetTimeOfDay_hour_419, logic_uScript_SetTimeOfDay_tech_419);
	}

	private void Relay_In_420()
	{
		logic_uScript_SetTimeOfDay_uScript_SetTimeOfDay_420.In(logic_uScript_SetTimeOfDay_hour_420, logic_uScript_SetTimeOfDay_tech_420);
	}

	private void Relay_In_421()
	{
		logic_uScriptCon_CompareInt_A_421 = local_Stage_System_Int32;
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_421.In(logic_uScriptCon_CompareInt_A_421, logic_uScriptCon_CompareInt_B_421);
		bool greaterThan = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_421.GreaterThan;
		bool lessThanOrEqualTo = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_421.LessThanOrEqualTo;
		if (greaterThan)
		{
			Relay_In_420();
		}
		if (lessThanOrEqualTo)
		{
			Relay_In_419();
		}
	}

	private void Relay_In_422()
	{
		logic_uScript_SetTimeOfDay_hour_422 = local_StartingHour_System_Int32;
		logic_uScript_SetTimeOfDay_uScript_SetTimeOfDay_422.In(logic_uScript_SetTimeOfDay_hour_422, logic_uScript_SetTimeOfDay_tech_422);
	}

	private void Relay_In_424()
	{
		logic_uScriptCon_CompareInt_A_424 = local_Stage_System_Int32;
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_424.In(logic_uScriptCon_CompareInt_A_424, logic_uScriptCon_CompareInt_B_424);
		bool greaterThan = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_424.GreaterThan;
		bool lessThanOrEqualTo = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_424.LessThanOrEqualTo;
		if (greaterThan)
		{
			Relay_In_421();
		}
		if (lessThanOrEqualTo)
		{
			Relay_In_422();
		}
	}

	private void Relay_Pause_427()
	{
		logic_uScript_PausePopulation_uScript_PausePopulation_427.Pause();
		if (logic_uScript_PausePopulation_uScript_PausePopulation_427.Out)
		{
			Relay_In_437();
		}
	}

	private void Relay_UnPause_427()
	{
		logic_uScript_PausePopulation_uScript_PausePopulation_427.UnPause();
		if (logic_uScript_PausePopulation_uScript_PausePopulation_427.Out)
		{
			Relay_In_437();
		}
	}

	private void Relay_In_431()
	{
		logic_uScript_PlayerInRangeOfCurrentEncounter_ownerNode_431 = owner_Connection_429;
		logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_431.In(logic_uScript_PlayerInRangeOfCurrentEncounter_ownerNode_431);
		bool num = logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_431.True;
		bool flag = logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_431.False;
		if (num)
		{
			Relay_Pause_427();
		}
		if (flag)
		{
			Relay_UnPause_427();
		}
	}

	private void Relay_In_432()
	{
		logic_uScriptCon_CompareBool_Bool_432 = local_TechsInit_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_432.In(logic_uScriptCon_CompareBool_Bool_432);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_432.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_432.False;
		if (num)
		{
			Relay_In_431();
		}
		if (flag)
		{
			Relay_True_436();
		}
	}

	private void Relay_In_433()
	{
		logic_uScript_GetTimeOfDay_Return_433 = logic_uScript_GetTimeOfDay_uScript_GetTimeOfDay_433.In();
		local_StartingHour_System_Int32 = logic_uScript_GetTimeOfDay_Return_433;
		if (logic_uScript_GetTimeOfDay_uScript_GetTimeOfDay_433.Out)
		{
			Relay_In_431();
		}
	}

	private void Relay_In_435()
	{
		logic_uScript_SetTimeOfDay_hour_435 = local_StartingHour_System_Int32;
		logic_uScript_SetTimeOfDay_uScript_SetTimeOfDay_435.In(logic_uScript_SetTimeOfDay_hour_435, logic_uScript_SetTimeOfDay_tech_435);
	}

	private void Relay_True_436()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_436.True(out logic_uScriptAct_SetBool_Target_436);
		local_TechsInit_System_Boolean = logic_uScriptAct_SetBool_Target_436;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_436.Out)
		{
			Relay_In_433();
		}
	}

	private void Relay_False_436()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_436.False(out logic_uScriptAct_SetBool_Target_436);
		local_TechsInit_System_Boolean = logic_uScriptAct_SetBool_Target_436;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_436.Out)
		{
			Relay_In_433();
		}
	}

	private void Relay_In_437()
	{
		logic_uScript_IsPlayerInTriggerSmart_innerTriggerArea_437 = MissionRangeTrig;
		logic_uScript_IsPlayerInTriggerSmart_outerTriggerArea_437 = MissionRangeTrig;
		logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_437.In(logic_uScript_IsPlayerInTriggerSmart_innerTriggerArea_437, logic_uScript_IsPlayerInTriggerSmart_outerTriggerArea_437, ref logic_uScript_IsPlayerInTriggerSmart_inside_437);
		bool num = logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_437.Out;
		bool allInside = logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_437.AllInside;
		bool lastExited = logic_uScript_IsPlayerInTriggerSmart_uScript_IsPlayerInTriggerSmart_437.LastExited;
		if (num)
		{
			Relay_In_391();
		}
		if (allInside)
		{
			Relay_In_481();
		}
		if (lastExited)
		{
			Relay_In_435();
		}
	}

	private void Relay_In_439()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_439.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_439.Out)
		{
			Relay_In_440();
		}
	}

	private void Relay_In_440()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_440.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_440.Out)
		{
			Relay_In_8();
		}
	}

	private void Relay_In_442()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_442.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_442.Out)
		{
			Relay_In_460();
		}
	}

	private void Relay_Out_443()
	{
		Relay_In_446();
	}

	private void Relay_In_443()
	{
		int num = 0;
		Array circuirtsbaseSpawnData = CircuirtsbaseSpawnData;
		if (logic_SubGraph_Crafting_Tutorial_Init_baseSpawnData_443.Length != num + circuirtsbaseSpawnData.Length)
		{
			Array.Resize(ref logic_SubGraph_Crafting_Tutorial_Init_baseSpawnData_443, num + circuirtsbaseSpawnData.Length);
		}
		Array.Copy(circuirtsbaseSpawnData, 0, logic_SubGraph_Crafting_Tutorial_Init_baseSpawnData_443, num, circuirtsbaseSpawnData.Length);
		num += circuirtsbaseSpawnData.Length;
		int num2 = 0;
		Array blockSpawnData = BlockSpawnData;
		if (logic_SubGraph_Crafting_Tutorial_Init_blockSpawnData_443.Length != num2 + blockSpawnData.Length)
		{
			Array.Resize(ref logic_SubGraph_Crafting_Tutorial_Init_blockSpawnData_443, num2 + blockSpawnData.Length);
		}
		Array.Copy(blockSpawnData, 0, logic_SubGraph_Crafting_Tutorial_Init_blockSpawnData_443, num2, blockSpawnData.Length);
		num2 += blockSpawnData.Length;
		int num3 = 0;
		Array nPCSpawnData = NPCSpawnData;
		if (logic_SubGraph_Crafting_Tutorial_Init_NPCSpawnData_443.Length != num3 + nPCSpawnData.Length)
		{
			Array.Resize(ref logic_SubGraph_Crafting_Tutorial_Init_NPCSpawnData_443, num3 + nPCSpawnData.Length);
		}
		Array.Copy(nPCSpawnData, 0, logic_SubGraph_Crafting_Tutorial_Init_NPCSpawnData_443, num3, nPCSpawnData.Length);
		num3 += nPCSpawnData.Length;
		logic_SubGraph_Crafting_Tutorial_Init_completedBasePreset_443 = completedBasePreset;
		logic_SubGraph_Crafting_Tutorial_Init_basePosition_443 = CircuitsBasePosition;
		logic_SubGraph_Crafting_Tutorial_Init_clearSceneryRadius_443 = clearSceneryRadius;
		logic_SubGraph_Crafting_Tutorial_Init_CraftingBaseTech_443 = local_CircuitBaseTech_Tank;
		logic_SubGraph_Crafting_Tutorial_Init_NPCTech_443 = local_NPCTech_Tank;
		logic_SubGraph_Crafting_Tutorial_Init_SubGraph_Crafting_Tutorial_Init_443.In(logic_SubGraph_Crafting_Tutorial_Init_baseSpawnData_443, logic_SubGraph_Crafting_Tutorial_Init_blockSpawnData_443, logic_SubGraph_Crafting_Tutorial_Init_NPCSpawnData_443, logic_SubGraph_Crafting_Tutorial_Init_completedBasePreset_443, logic_SubGraph_Crafting_Tutorial_Init_useNPCAsTutorialTech_443, logic_SubGraph_Crafting_Tutorial_Init_spawnBase_443, logic_SubGraph_Crafting_Tutorial_Init_basePosition_443, logic_SubGraph_Crafting_Tutorial_Init_clearSceneryRadius_443, ref logic_SubGraph_Crafting_Tutorial_Init_CraftingBaseTech_443, ref logic_SubGraph_Crafting_Tutorial_Init_NPCTech_443);
	}

	private void Relay_Out_446()
	{
		Relay_In_476();
	}

	private void Relay_In_446()
	{
		int num = 0;
		Array blockSpawnData = BlockSpawnData;
		if (logic_SubGraph_Crafting_Tutorial_ManageBlock_blockSpawnData_446.Length != num + blockSpawnData.Length)
		{
			Array.Resize(ref logic_SubGraph_Crafting_Tutorial_ManageBlock_blockSpawnData_446, num + blockSpawnData.Length);
		}
		Array.Copy(blockSpawnData, 0, logic_SubGraph_Crafting_Tutorial_ManageBlock_blockSpawnData_446, num, blockSpawnData.Length);
		num += blockSpawnData.Length;
		logic_SubGraph_Crafting_Tutorial_ManageBlock_block_446 = local_Wire1Block_TankBlock;
		logic_SubGraph_Crafting_Tutorial_ManageBlock_msgBlockOutsideArea_446 = msgBlockOutsideArea;
		logic_SubGraph_Crafting_Tutorial_ManageBlock_messageSpeaker_446 = messageSpeaker;
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_446.In(ref logic_SubGraph_Crafting_Tutorial_ManageBlock_blockSpawnData_446, logic_SubGraph_Crafting_Tutorial_ManageBlock_blockIndex_446, ref logic_SubGraph_Crafting_Tutorial_ManageBlock_block_446, logic_SubGraph_Crafting_Tutorial_ManageBlock_allowAnchoring_446, logic_SubGraph_Crafting_Tutorial_ManageBlock_msgBlockOutsideArea_446, logic_SubGraph_Crafting_Tutorial_ManageBlock_messageSpeaker_446);
	}

	private void Relay_Out_449()
	{
		Relay_In_432();
	}

	private void Relay_In_449()
	{
		int num = 0;
		Array beamSensorBlockSpawnData = BeamSensorBlockSpawnData;
		if (logic_SubGraph_Crafting_Tutorial_ManageBlock_blockSpawnData_449.Length != num + beamSensorBlockSpawnData.Length)
		{
			Array.Resize(ref logic_SubGraph_Crafting_Tutorial_ManageBlock_blockSpawnData_449, num + beamSensorBlockSpawnData.Length);
		}
		Array.Copy(beamSensorBlockSpawnData, 0, logic_SubGraph_Crafting_Tutorial_ManageBlock_blockSpawnData_449, num, beamSensorBlockSpawnData.Length);
		num += beamSensorBlockSpawnData.Length;
		logic_SubGraph_Crafting_Tutorial_ManageBlock_block_449 = local_BeamSensorBlock_TankBlock;
		logic_SubGraph_Crafting_Tutorial_ManageBlock_msgBlockOutsideArea_449 = msgBlockOutsideArea;
		logic_SubGraph_Crafting_Tutorial_ManageBlock_messageSpeaker_449 = messageSpeaker;
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_449.In(ref logic_SubGraph_Crafting_Tutorial_ManageBlock_blockSpawnData_449, logic_SubGraph_Crafting_Tutorial_ManageBlock_blockIndex_449, ref logic_SubGraph_Crafting_Tutorial_ManageBlock_block_449, logic_SubGraph_Crafting_Tutorial_ManageBlock_allowAnchoring_449, logic_SubGraph_Crafting_Tutorial_ManageBlock_msgBlockOutsideArea_449, logic_SubGraph_Crafting_Tutorial_ManageBlock_messageSpeaker_449);
	}

	private void Relay_In_460()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_460.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_460.Out)
		{
			Relay_In_432();
		}
	}

	private void Relay_In_463()
	{
		logic_uScriptCon_CompareBool_Bool_463 = local_BeamSensorIsSpawned_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_463.In(logic_uScriptCon_CompareBool_Bool_463);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_463.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_463.False;
		if (num)
		{
			Relay_In_449();
		}
		if (flag)
		{
			Relay_In_442();
		}
	}

	private void Relay_Out_464()
	{
		Relay_In_488();
	}

	private void Relay_In_464()
	{
		int num = 0;
		Array blockSpawnData = BlockSpawnData;
		if (logic_SubGraph_Crafting_Tutorial_ManageBlock_blockSpawnData_464.Length != num + blockSpawnData.Length)
		{
			Array.Resize(ref logic_SubGraph_Crafting_Tutorial_ManageBlock_blockSpawnData_464, num + blockSpawnData.Length);
		}
		Array.Copy(blockSpawnData, 0, logic_SubGraph_Crafting_Tutorial_ManageBlock_blockSpawnData_464, num, blockSpawnData.Length);
		num += blockSpawnData.Length;
		logic_SubGraph_Crafting_Tutorial_ManageBlock_block_464 = local_Wire3Block_TankBlock;
		logic_SubGraph_Crafting_Tutorial_ManageBlock_msgBlockOutsideArea_464 = msgBlockOutsideArea;
		logic_SubGraph_Crafting_Tutorial_ManageBlock_messageSpeaker_464 = messageSpeaker;
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_464.In(ref logic_SubGraph_Crafting_Tutorial_ManageBlock_blockSpawnData_464, logic_SubGraph_Crafting_Tutorial_ManageBlock_blockIndex_464, ref logic_SubGraph_Crafting_Tutorial_ManageBlock_block_464, logic_SubGraph_Crafting_Tutorial_ManageBlock_allowAnchoring_464, logic_SubGraph_Crafting_Tutorial_ManageBlock_msgBlockOutsideArea_464, logic_SubGraph_Crafting_Tutorial_ManageBlock_messageSpeaker_464);
	}

	private void Relay_Out_469()
	{
		Relay_In_463();
	}

	private void Relay_In_469()
	{
		int num = 0;
		Array blockSpawnData = BlockSpawnData;
		if (logic_SubGraph_Crafting_Tutorial_ManageBlock_blockSpawnData_469.Length != num + blockSpawnData.Length)
		{
			Array.Resize(ref logic_SubGraph_Crafting_Tutorial_ManageBlock_blockSpawnData_469, num + blockSpawnData.Length);
		}
		Array.Copy(blockSpawnData, 0, logic_SubGraph_Crafting_Tutorial_ManageBlock_blockSpawnData_469, num, blockSpawnData.Length);
		num += blockSpawnData.Length;
		logic_SubGraph_Crafting_Tutorial_ManageBlock_block_469 = local_LightBlock_TankBlock;
		logic_SubGraph_Crafting_Tutorial_ManageBlock_msgBlockOutsideArea_469 = msgBlockOutsideArea;
		logic_SubGraph_Crafting_Tutorial_ManageBlock_messageSpeaker_469 = messageSpeaker;
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_469.In(ref logic_SubGraph_Crafting_Tutorial_ManageBlock_blockSpawnData_469, logic_SubGraph_Crafting_Tutorial_ManageBlock_blockIndex_469, ref logic_SubGraph_Crafting_Tutorial_ManageBlock_block_469, logic_SubGraph_Crafting_Tutorial_ManageBlock_allowAnchoring_469, logic_SubGraph_Crafting_Tutorial_ManageBlock_msgBlockOutsideArea_469, logic_SubGraph_Crafting_Tutorial_ManageBlock_messageSpeaker_469);
	}

	private void Relay_Out_476()
	{
		Relay_In_464();
	}

	private void Relay_In_476()
	{
		int num = 0;
		Array blockSpawnData = BlockSpawnData;
		if (logic_SubGraph_Crafting_Tutorial_ManageBlock_blockSpawnData_476.Length != num + blockSpawnData.Length)
		{
			Array.Resize(ref logic_SubGraph_Crafting_Tutorial_ManageBlock_blockSpawnData_476, num + blockSpawnData.Length);
		}
		Array.Copy(blockSpawnData, 0, logic_SubGraph_Crafting_Tutorial_ManageBlock_blockSpawnData_476, num, blockSpawnData.Length);
		num += blockSpawnData.Length;
		logic_SubGraph_Crafting_Tutorial_ManageBlock_block_476 = local_Wire2Block_TankBlock;
		logic_SubGraph_Crafting_Tutorial_ManageBlock_msgBlockOutsideArea_476 = msgBlockOutsideArea;
		logic_SubGraph_Crafting_Tutorial_ManageBlock_messageSpeaker_476 = messageSpeaker;
		logic_SubGraph_Crafting_Tutorial_ManageBlock_SubGraph_Crafting_Tutorial_ManageBlock_476.In(ref logic_SubGraph_Crafting_Tutorial_ManageBlock_blockSpawnData_476, logic_SubGraph_Crafting_Tutorial_ManageBlock_blockIndex_476, ref logic_SubGraph_Crafting_Tutorial_ManageBlock_block_476, logic_SubGraph_Crafting_Tutorial_ManageBlock_allowAnchoring_476, logic_SubGraph_Crafting_Tutorial_ManageBlock_msgBlockOutsideArea_476, logic_SubGraph_Crafting_Tutorial_ManageBlock_messageSpeaker_476);
	}

	private void Relay_In_480()
	{
		logic_uScriptCon_CompareBool_Bool_480 = local_TechsInit_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_480.In(logic_uScriptCon_CompareBool_Bool_480);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_480.True)
		{
			Relay_In_424();
		}
	}

	private void Relay_In_481()
	{
		logic_uScript_SetTimeOfDay_hour_481 = MissionHour;
		logic_uScript_SetTimeOfDay_uScript_SetTimeOfDay_481.In(logic_uScript_SetTimeOfDay_hour_481, logic_uScript_SetTimeOfDay_tech_481);
	}

	private void Relay_Save_Out_482()
	{
	}

	private void Relay_Load_Out_482()
	{
		Relay_In_47();
	}

	private void Relay_Restart_Out_482()
	{
		Relay_False_97();
	}

	private void Relay_Save_482()
	{
		logic_SubGraph_SaveLoadBool_boolean_482 = local_TechsInit_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_482 = local_TechsInit_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_482.Save(ref logic_SubGraph_SaveLoadBool_boolean_482, logic_SubGraph_SaveLoadBool_boolAsVariable_482, logic_SubGraph_SaveLoadBool_uniqueID_482);
	}

	private void Relay_Load_482()
	{
		logic_SubGraph_SaveLoadBool_boolean_482 = local_TechsInit_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_482 = local_TechsInit_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_482.Load(ref logic_SubGraph_SaveLoadBool_boolean_482, logic_SubGraph_SaveLoadBool_boolAsVariable_482, logic_SubGraph_SaveLoadBool_uniqueID_482);
	}

	private void Relay_Set_True_482()
	{
		logic_SubGraph_SaveLoadBool_boolean_482 = local_TechsInit_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_482 = local_TechsInit_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_482.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_482, logic_SubGraph_SaveLoadBool_boolAsVariable_482, logic_SubGraph_SaveLoadBool_uniqueID_482);
	}

	private void Relay_Set_False_482()
	{
		logic_SubGraph_SaveLoadBool_boolean_482 = local_TechsInit_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_482 = local_TechsInit_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_482.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_482, logic_SubGraph_SaveLoadBool_boolAsVariable_482, logic_SubGraph_SaveLoadBool_uniqueID_482);
	}

	private void Relay_In_486()
	{
		logic_uScriptCon_CompareBool_Bool_486 = local_NonEncounterToggleUsed_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_486.In(logic_uScriptCon_CompareBool_Bool_486);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_486.False)
		{
			Relay_In_131();
		}
	}

	private void Relay_In_488()
	{
		logic_uScript_GetTankBlock_tank_488 = local_CircuitBaseTech_Tank;
		logic_uScript_GetTankBlock_blockType_488 = blockTypeToggle;
		logic_uScript_GetTankBlock_Return_488 = logic_uScript_GetTankBlock_uScript_GetTankBlock_488.In(logic_uScript_GetTankBlock_tank_488, logic_uScript_GetTankBlock_blockType_488);
		local_ToggleBlock_TankBlock = logic_uScript_GetTankBlock_Return_488;
		if (logic_uScript_GetTankBlock_uScript_GetTankBlock_488.Returned)
		{
			Relay_In_504();
		}
	}

	private void Relay_In_492()
	{
		logic_uScript_EnableGlow_targetObject_492 = local_ToggleBlock_TankBlock;
		logic_uScript_EnableGlow_uScript_EnableGlow_492.In(logic_uScript_EnableGlow_targetObject_492, logic_uScript_EnableGlow_enable_492);
		if (logic_uScript_EnableGlow_uScript_EnableGlow_492.Out)
		{
			Relay_In_497();
		}
	}

	private void Relay_In_493()
	{
		logic_uScript_PointArrowAtVisible_targetObject_493 = local_ToggleBlock_TankBlock;
		logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_493.In(logic_uScript_PointArrowAtVisible_targetObject_493, logic_uScript_PointArrowAtVisible_timeToShowFor_493, logic_uScript_PointArrowAtVisible_offset_493);
		if (logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_493.Out)
		{
			Relay_In_492();
		}
	}

	private void Relay_In_497()
	{
		logic_uScript_AddMessage_messageData_497 = msg03ToggleIntro;
		logic_uScript_AddMessage_speaker_497 = messageSpeaker;
		logic_uScript_AddMessage_Return_497 = logic_uScript_AddMessage_uScript_AddMessage_497.In(logic_uScript_AddMessage_messageData_497, logic_uScript_AddMessage_speaker_497);
		if (logic_uScript_AddMessage_uScript_AddMessage_497.Shown)
		{
			Relay_In_499();
		}
	}

	private void Relay_In_499()
	{
		logic_uScript_PointArrowAtVisible_targetObject_499 = local_ToggleBlock_TankBlock;
		logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_499.In(logic_uScript_PointArrowAtVisible_targetObject_499, logic_uScript_PointArrowAtVisible_timeToShowFor_499, logic_uScript_PointArrowAtVisible_offset_499);
		if (logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_499.Out)
		{
			Relay_In_501();
		}
	}

	private void Relay_In_501()
	{
		logic_uScript_EnableGlow_targetObject_501 = local_ToggleBlock_TankBlock;
		logic_uScript_EnableGlow_uScript_EnableGlow_501.In(logic_uScript_EnableGlow_targetObject_501, logic_uScript_EnableGlow_enable_501);
		if (logic_uScript_EnableGlow_uScript_EnableGlow_501.Out)
		{
			Relay_In_107();
		}
	}

	private void Relay_True_502()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_502.True(out logic_uScriptAct_SetBool_Target_502);
		local_msgToggleIntroShown_System_Boolean = logic_uScriptAct_SetBool_Target_502;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_502.Out)
		{
			Relay_In_293();
		}
	}

	private void Relay_False_502()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_502.False(out logic_uScriptAct_SetBool_Target_502);
		local_msgToggleIntroShown_System_Boolean = logic_uScriptAct_SetBool_Target_502;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_502.Out)
		{
			Relay_In_293();
		}
	}

	private void Relay_In_504()
	{
		logic_uScript_GetTankBlock_tank_504 = local_CircuitBaseTech_Tank;
		logic_uScript_GetTankBlock_blockType_504 = blockTypeLight;
		logic_uScript_GetTankBlock_Return_504 = logic_uScript_GetTankBlock_uScript_GetTankBlock_504.In(logic_uScript_GetTankBlock_tank_504, logic_uScript_GetTankBlock_blockType_504);
		local_Light2Block_TankBlock = logic_uScript_GetTankBlock_Return_504;
		if (logic_uScript_GetTankBlock_uScript_GetTankBlock_504.Returned)
		{
			Relay_In_469();
		}
	}

	private void Relay_Save_Out_508()
	{
		Relay_Save_554();
	}

	private void Relay_Load_Out_508()
	{
		Relay_Load_554();
	}

	private void Relay_Restart_Out_508()
	{
		Relay_Restart_554();
	}

	private void Relay_Save_508()
	{
		logic_SubGraph_SaveLoadInt_integer_508 = local_SubStage2_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_508 = local_SubStage2_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_508.Save(logic_SubGraph_SaveLoadInt_restartValue_508, ref logic_SubGraph_SaveLoadInt_integer_508, logic_SubGraph_SaveLoadInt_intAsVariable_508, logic_SubGraph_SaveLoadInt_uniqueID_508);
	}

	private void Relay_Load_508()
	{
		logic_SubGraph_SaveLoadInt_integer_508 = local_SubStage2_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_508 = local_SubStage2_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_508.Load(logic_SubGraph_SaveLoadInt_restartValue_508, ref logic_SubGraph_SaveLoadInt_integer_508, logic_SubGraph_SaveLoadInt_intAsVariable_508, logic_SubGraph_SaveLoadInt_uniqueID_508);
	}

	private void Relay_Restart_508()
	{
		logic_SubGraph_SaveLoadInt_integer_508 = local_SubStage2_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_508 = local_SubStage2_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_508.Restart(logic_SubGraph_SaveLoadInt_restartValue_508, ref logic_SubGraph_SaveLoadInt_integer_508, logic_SubGraph_SaveLoadInt_intAsVariable_508, logic_SubGraph_SaveLoadInt_uniqueID_508);
	}

	private void Relay_Output1_510()
	{
		Relay_In_244();
	}

	private void Relay_Output2_510()
	{
		Relay_In_179();
	}

	private void Relay_Output3_510()
	{
	}

	private void Relay_Output4_510()
	{
	}

	private void Relay_Output5_510()
	{
	}

	private void Relay_Output6_510()
	{
	}

	private void Relay_Output7_510()
	{
	}

	private void Relay_Output8_510()
	{
	}

	private void Relay_In_510()
	{
		logic_uScriptCon_ManualSwitch_CurrentOutput_510 = local_SubStage2_System_Int32;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_510.In(logic_uScriptCon_ManualSwitch_CurrentOutput_510);
	}

	private void Relay_In_513()
	{
		logic_uScriptAct_AddInt_v2_A_513 = local_SubStage2_System_Int32;
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_513.In(logic_uScriptAct_AddInt_v2_A_513, logic_uScriptAct_AddInt_v2_B_513, out logic_uScriptAct_AddInt_v2_IntResult_513, out logic_uScriptAct_AddInt_v2_FloatResult_513);
		local_SubStage2_System_Int32 = logic_uScriptAct_AddInt_v2_IntResult_513;
		if (logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_513.Out)
		{
			Relay_In_285();
		}
	}

	private void Relay_Output1_514()
	{
		Relay_In_59();
	}

	private void Relay_Output2_514()
	{
		Relay_In_523();
	}

	private void Relay_Output3_514()
	{
	}

	private void Relay_Output4_514()
	{
	}

	private void Relay_Output5_514()
	{
	}

	private void Relay_Output6_514()
	{
	}

	private void Relay_Output7_514()
	{
	}

	private void Relay_Output8_514()
	{
	}

	private void Relay_In_514()
	{
		logic_uScriptCon_ManualSwitch_CurrentOutput_514 = local_SubStage3_System_Int32;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_514.In(logic_uScriptCon_ManualSwitch_CurrentOutput_514);
	}

	private void Relay_In_516()
	{
		logic_uScriptAct_AddInt_v2_A_516 = local_SubStage3_System_Int32;
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_516.In(logic_uScriptAct_AddInt_v2_A_516, logic_uScriptAct_AddInt_v2_B_516, out logic_uScriptAct_AddInt_v2_IntResult_516, out logic_uScriptAct_AddInt_v2_FloatResult_516);
		local_SubStage3_System_Int32 = logic_uScriptAct_AddInt_v2_IntResult_516;
	}

	private void Relay_True_518()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_518.True(out logic_uScriptAct_SetBool_Target_518);
		local_msgBeamSensorAttachedIsOnShown_System_Boolean = logic_uScriptAct_SetBool_Target_518;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_518.Out)
		{
			Relay_In_546();
		}
	}

	private void Relay_False_518()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_518.False(out logic_uScriptAct_SetBool_Target_518);
		local_msgBeamSensorAttachedIsOnShown_System_Boolean = logic_uScriptAct_SetBool_Target_518;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_518.Out)
		{
			Relay_In_546();
		}
	}

	private void Relay_In_523()
	{
		logic_uScriptCon_CompareBool_Bool_523 = local_BeamSensorIsOn_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_523.In(logic_uScriptCon_CompareBool_Bool_523);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_523.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_523.False;
		if (num)
		{
			Relay_In_541();
		}
		if (flag)
		{
			Relay_In_524();
		}
	}

	private void Relay_In_524()
	{
		logic_uScriptCon_CompareBool_Bool_524 = local_msgBeamSensorAttachedIsOnShown_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_524.In(logic_uScriptCon_CompareBool_Bool_524);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_524.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_524.False;
		if (num)
		{
			Relay_In_549();
		}
		if (flag)
		{
			Relay_In_545();
		}
	}

	private void Relay_True_525()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_525.True(out logic_uScriptAct_SetBool_Target_525);
		local_msgCompleteIsOnShown_System_Boolean = logic_uScriptAct_SetBool_Target_525;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_525.Out)
		{
			Relay_In_534();
		}
	}

	private void Relay_False_525()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_525.False(out logic_uScriptAct_SetBool_Target_525);
		local_msgCompleteIsOnShown_System_Boolean = logic_uScriptAct_SetBool_Target_525;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_525.Out)
		{
			Relay_In_534();
		}
	}

	private void Relay_Out_527()
	{
	}

	private void Relay_In_527()
	{
		logic_SubGraph_Crafting_Tutorial_Finish_craftingBaseTech_527 = local_CircuitBaseTech_Tank;
		logic_SubGraph_Crafting_Tutorial_Finish_NPCTech_527 = local_NPCTech_Tank;
		logic_SubGraph_Crafting_Tutorial_Finish_NPCFlyAwayAI_527 = NPCFlyAwayAI;
		logic_SubGraph_Crafting_Tutorial_Finish_NPCDespawnParticleEffect_527 = NPCDespawnParticleEffect;
		logic_SubGraph_Crafting_Tutorial_Finish_SubGraph_Crafting_Tutorial_Finish_527.In(logic_SubGraph_Crafting_Tutorial_Finish_craftingBaseTech_527, logic_SubGraph_Crafting_Tutorial_Finish_NPCTech_527, logic_SubGraph_Crafting_Tutorial_Finish_NPCFlyAwayAI_527, logic_SubGraph_Crafting_Tutorial_Finish_NPCDespawnParticleEffect_527);
	}

	private void Relay_In_528()
	{
		logic_uScript_AddMessage_messageData_528 = msg17BeamSensorOn;
		logic_uScript_AddMessage_speaker_528 = messageSpeaker;
		logic_uScript_AddMessage_Return_528 = logic_uScript_AddMessage_uScript_AddMessage_528.In(logic_uScript_AddMessage_messageData_528, logic_uScript_AddMessage_speaker_528);
		if (logic_uScript_AddMessage_uScript_AddMessage_528.Shown)
		{
			Relay_True_525();
		}
	}

	private void Relay_In_534()
	{
		logic_uScript_AddMessage_messageData_534 = msg18Complete;
		logic_uScript_AddMessage_speaker_534 = messageSpeaker;
		logic_uScript_AddMessage_Return_534 = logic_uScript_AddMessage_uScript_AddMessage_534.In(logic_uScript_AddMessage_messageData_534, logic_uScript_AddMessage_speaker_534);
		if (logic_uScript_AddMessage_uScript_AddMessage_534.Shown)
		{
			Relay_In_527();
		}
	}

	private void Relay_True_537()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_537.True(out logic_uScriptAct_SetBool_Target_537);
		local_BeamSensorIsOn_System_Boolean = logic_uScriptAct_SetBool_Target_537;
	}

	private void Relay_False_537()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_537.False(out logic_uScriptAct_SetBool_Target_537);
		local_BeamSensorIsOn_System_Boolean = logic_uScriptAct_SetBool_Target_537;
	}

	private void Relay_In_541()
	{
		logic_uScriptCon_CompareBool_Bool_541 = local_msgCompleteIsOnShown_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_541.In(logic_uScriptCon_CompareBool_Bool_541);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_541.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_541.False;
		if (num)
		{
			Relay_In_534();
		}
		if (flag)
		{
			Relay_In_528();
		}
	}

	private void Relay_In_545()
	{
		logic_uScript_AddMessage_messageData_545 = msg14AttachedBeamSensor;
		logic_uScript_AddMessage_speaker_545 = messageSpeaker;
		logic_uScript_AddMessage_Return_545 = logic_uScript_AddMessage_uScript_AddMessage_545.In(logic_uScript_AddMessage_messageData_545, logic_uScript_AddMessage_speaker_545);
		if (logic_uScript_AddMessage_uScript_AddMessage_545.Shown)
		{
			Relay_True_518();
		}
	}

	private void Relay_In_546()
	{
		logic_uScript_GetCircuitChargeInfo_block_546 = local_Light2Block_TankBlock;
		logic_uScript_GetCircuitChargeInfo_Return_546 = logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_546.In(logic_uScript_GetCircuitChargeInfo_block_546, logic_uScript_GetCircuitChargeInfo_tech_546, logic_uScript_GetCircuitChargeInfo_blockType_546);
		local_Light2BlockSignalValue_System_Int32 = logic_uScript_GetCircuitChargeInfo_Return_546;
		if (logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_546.Out)
		{
			Relay_In_553();
		}
	}

	private void Relay_In_549()
	{
		logic_uScript_AddMessage_messageData_549 = msg15SetBeamSensorActive;
		logic_uScript_AddMessage_speaker_549 = messageSpeaker;
		logic_uScript_AddMessage_Return_549 = logic_uScript_AddMessage_uScript_AddMessage_549.In(logic_uScript_AddMessage_messageData_549, logic_uScript_AddMessage_speaker_549);
		if (logic_uScript_AddMessage_uScript_AddMessage_549.Out)
		{
			Relay_In_546();
		}
	}

	private void Relay_In_553()
	{
		logic_uScriptCon_CompareInt_A_553 = local_Light2BlockSignalValue_System_Int32;
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_553.In(logic_uScriptCon_CompareInt_A_553, logic_uScriptCon_CompareInt_B_553);
		if (logic_uScriptCon_CompareInt_uScriptCon_CompareInt_553.GreaterThan)
		{
			Relay_True_537();
		}
	}

	private void Relay_Save_Out_554()
	{
		Relay_Save_354();
	}

	private void Relay_Load_Out_554()
	{
		Relay_Load_354();
	}

	private void Relay_Restart_Out_554()
	{
		Relay_Restart_354();
	}

	private void Relay_Save_554()
	{
		logic_SubGraph_SaveLoadInt_integer_554 = local_SubStage3_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_554 = local_SubStage3_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_554.Save(logic_SubGraph_SaveLoadInt_restartValue_554, ref logic_SubGraph_SaveLoadInt_integer_554, logic_SubGraph_SaveLoadInt_intAsVariable_554, logic_SubGraph_SaveLoadInt_uniqueID_554);
	}

	private void Relay_Load_554()
	{
		logic_SubGraph_SaveLoadInt_integer_554 = local_SubStage3_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_554 = local_SubStage3_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_554.Load(logic_SubGraph_SaveLoadInt_restartValue_554, ref logic_SubGraph_SaveLoadInt_integer_554, logic_SubGraph_SaveLoadInt_intAsVariable_554, logic_SubGraph_SaveLoadInt_uniqueID_554);
	}

	private void Relay_Restart_554()
	{
		logic_SubGraph_SaveLoadInt_integer_554 = local_SubStage3_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_554 = local_SubStage3_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_554.Restart(logic_SubGraph_SaveLoadInt_restartValue_554, ref logic_SubGraph_SaveLoadInt_integer_554, logic_SubGraph_SaveLoadInt_intAsVariable_554, logic_SubGraph_SaveLoadInt_uniqueID_554);
	}

	private void Relay_Out_556()
	{
		Relay_In_486();
	}

	private void Relay_Shown_556()
	{
	}

	private void Relay_In_556()
	{
		logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboard_556 = msg08SetToggleActive;
		logic_SubGraph_AddMessageWithPadSupport_messageControlPad_556 = msg08SetToggleActive_Pad;
		logic_SubGraph_AddMessageWithPadSupport_speaker_556 = messageSpeaker;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_556.In(logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboard_556, logic_SubGraph_AddMessageWithPadSupport_messageControlPad_556, logic_SubGraph_AddMessageWithPadSupport_speaker_556);
	}

	private void Relay_Out_560()
	{
		Relay_In_362();
	}

	private void Relay_Shown_560()
	{
	}

	private void Relay_In_560()
	{
		logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboard_560 = msg10SetToggleInactive;
		logic_SubGraph_AddMessageWithPadSupport_messageControlPad_560 = msg10SetToggleInactive_Pad;
		logic_SubGraph_AddMessageWithPadSupport_speaker_560 = messageSpeaker;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_560.In(logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboard_560, logic_SubGraph_AddMessageWithPadSupport_messageControlPad_560, logic_SubGraph_AddMessageWithPadSupport_speaker_560);
	}

	private void Relay_Out_567()
	{
		Relay_In_58();
	}

	private void Relay_Shown_567()
	{
	}

	private void Relay_In_567()
	{
		logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboard_567 = msg13PickupBeamSensor;
		logic_SubGraph_AddMessageWithPadSupport_messageControlPad_567 = msg13PickupBeamSensor_Pad;
		logic_SubGraph_AddMessageWithPadSupport_speaker_567 = messageSpeaker;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_567.In(logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboard_567, logic_SubGraph_AddMessageWithPadSupport_messageControlPad_567, logic_SubGraph_AddMessageWithPadSupport_speaker_567);
	}
}
