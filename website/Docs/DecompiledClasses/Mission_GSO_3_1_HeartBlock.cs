using System;
using UnityEngine;

[Serializable]
[NodePath("Graphs")]
[FriendlyName("Untitled", "")]
public class Mission_GSO_3_1_HeartBlock : uScriptLogic
{
	private GameObject parentGameObject;

	private uScript_GUI thisScriptsOnGuiListener;

	private bool m_RegisteredForEvents;

	public SpawnTechData[] enemyTechData = new SpawnTechData[0];

	public SpawnBlockData[] heartBlockData = new SpawnBlockData[0];

	public float heartBlockFoundDist;

	[Multiline(3)]
	public string heartBlockPosition = "";

	public float heartBlockSpawnDist;

	private TankBlock[] local_16_TankBlockArray = new TankBlock[0];

	private Vector3 local_27_UnityEngine_Vector3 = new Vector3(0f, 0f, 0f);

	private bool local_DefendMsgShown_System_Boolean;

	private bool local_EnemiesAliveMsgShown_System_Boolean;

	private bool local_EnemiesSpawned_System_Boolean;

	private Tank[] local_EnemyTechs_TankArray = new Tank[0];

	private TankBlock local_HeartBlock_TankBlock;

	private bool local_HeartBlockFound_System_Boolean;

	private bool local_HeartBlockSpawned_System_Boolean;

	private Tank local_HeartTech_Tank;

	private bool local_LockHeartBlock_System_Boolean;

	private bool local_MissionCompleteMsgShown_System_Boolean;

	private bool local_NotPoweredMsgShown_System_Boolean;

	private int local_Stage_System_Int32 = 1;

	private bool local_StartMsgShown_System_Boolean;

	public uScript_AddMessage.MessageSpeaker messageSpeaker;

	[Multiline(3)]
	public string MessageTag = "";

	public uScript_AddMessage.MessageData msg01FindHeartBlock;

	public uScript_AddMessage.MessageData msg02AnchorHeartBlock;

	public uScript_AddMessage.MessageData msg03EnemiesIncoming;

	public uScript_AddMessage.MessageData msg04DefendHeartBlock;

	public uScript_AddMessage.MessageData msg05PoweredButEnemiesAlive;

	public uScript_AddMessage.MessageData msg06EnemiesDeadButNotPowered;

	public uScript_AddMessage.MessageData msg07MissionComplete;

	public uScript_AddMessage.MessageData msg08MissionCompleteNotAnchored;

	public uScript_AddMessage.MessageData msg09HeartBlockExplanation;

	public uScript_AddMessage.MessageData msg10HeartBlockDestroyed;

	private GameObject owner_Connection_0;

	private GameObject owner_Connection_11;

	private GameObject owner_Connection_15;

	private GameObject owner_Connection_22;

	private GameObject owner_Connection_32;

	private GameObject owner_Connection_55;

	private GameObject owner_Connection_57;

	private GameObject owner_Connection_58;

	private GameObject owner_Connection_71;

	private GameObject owner_Connection_86;

	private GameObject owner_Connection_97;

	private GameObject owner_Connection_105;

	private uScriptCon_ManualSwitch logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_2 = new uScriptCon_ManualSwitch();

	private int logic_uScriptCon_ManualSwitch_CurrentOutput_2;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_4 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_4;

	private bool logic_uScriptCon_CompareBool_True_4 = true;

	private bool logic_uScriptCon_CompareBool_False_4 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_6 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_6;

	private bool logic_uScriptAct_SetBool_Out_6 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_6 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_6 = true;

	private uScript_GetAndCheckBlocks logic_uScript_GetAndCheckBlocks_uScript_GetAndCheckBlocks_7 = new uScript_GetAndCheckBlocks();

	private SpawnBlockData[] logic_uScript_GetAndCheckBlocks_blockData_7 = new SpawnBlockData[0];

	private GameObject logic_uScript_GetAndCheckBlocks_ownerNode_7;

	private TankBlock[] logic_uScript_GetAndCheckBlocks_blocks_7 = new TankBlock[0];

	private bool logic_uScript_GetAndCheckBlocks_AllAlive_7 = true;

	private bool logic_uScript_GetAndCheckBlocks_SomeAlive_7 = true;

	private bool logic_uScript_GetAndCheckBlocks_AllDead_7 = true;

	private bool logic_uScript_GetAndCheckBlocks_WaitingToSpawn_7 = true;

	private uScript_AccessListBlock logic_uScript_AccessListBlock_uScript_AccessListBlock_9 = new uScript_AccessListBlock();

	private TankBlock[] logic_uScript_AccessListBlock_blockList_9 = new TankBlock[0];

	private int logic_uScript_AccessListBlock_index_9;

	private TankBlock logic_uScript_AccessListBlock_value_9;

	private bool logic_uScript_AccessListBlock_Out_9 = true;

	private uScript_HealBlock logic_uScript_HealBlock_uScript_HealBlock_12 = new uScript_HealBlock();

	private TankBlock logic_uScript_HealBlock_block_12;

	private float logic_uScript_HealBlock_healAmount_12 = 999999f;

	private bool logic_uScript_HealBlock_Out_12 = true;

	private uScript_MoveEncounterWithVisible logic_uScript_MoveEncounterWithVisible_uScript_MoveEncounterWithVisible_14 = new uScript_MoveEncounterWithVisible();

	private GameObject logic_uScript_MoveEncounterWithVisible_ownerNode_14;

	private object logic_uScript_MoveEncounterWithVisible_visibleObject_14 = "";

	private bool logic_uScript_MoveEncounterWithVisible_Out_14 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_18 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_18;

	private bool logic_uScriptAct_SetBool_Out_18 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_18 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_18 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_20 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_20;

	private bool logic_uScriptCon_CompareBool_True_20 = true;

	private bool logic_uScriptCon_CompareBool_False_20 = true;

	private uScript_GetPositionInEncounter logic_uScript_GetPositionInEncounter_uScript_GetPositionInEncounter_23 = new uScript_GetPositionInEncounter();

	private GameObject logic_uScript_GetPositionInEncounter_ownerNode_23;

	private string logic_uScript_GetPositionInEncounter_posName_23 = "";

	private Vector3 logic_uScript_GetPositionInEncounter_Return_23;

	private bool logic_uScript_GetPositionInEncounter_Out_23 = true;

	private uScript_PlayerInRange logic_uScript_PlayerInRange_uScript_PlayerInRange_25 = new uScript_PlayerInRange();

	private Vector3 logic_uScript_PlayerInRange_position_25;

	private float logic_uScript_PlayerInRange_range_25;

	private bool logic_uScript_PlayerInRange_True_25 = true;

	private bool logic_uScript_PlayerInRange_False_25 = true;

	private bool logic_uScript_PlayerInRange_Out_25 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_29 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_29;

	private bool logic_uScriptAct_SetBool_Out_29 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_29 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_29 = true;

	private uScript_SpawnBlocksFromData logic_uScript_SpawnBlocksFromData_uScript_SpawnBlocksFromData_33 = new uScript_SpawnBlocksFromData();

	private SpawnBlockData[] logic_uScript_SpawnBlocksFromData_blockData_33 = new SpawnBlockData[0];

	private GameObject logic_uScript_SpawnBlocksFromData_ownerNode_33;

	private bool logic_uScript_SpawnBlocksFromData_Out_33 = true;

	private uScript_IsPlayerInRangeOfBlock logic_uScript_IsPlayerInRangeOfBlock_uScript_IsPlayerInRangeOfBlock_34 = new uScript_IsPlayerInRangeOfBlock();

	private TankBlock logic_uScript_IsPlayerInRangeOfBlock_block_34;

	private float logic_uScript_IsPlayerInRangeOfBlock_range_34;

	private bool logic_uScript_IsPlayerInRangeOfBlock_Out_34 = true;

	private bool logic_uScript_IsPlayerInRangeOfBlock_InRange_34 = true;

	private bool logic_uScript_IsPlayerInRangeOfBlock_OutOfRange_34 = true;

	private SubGraph_CompleteObjectiveStage logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_37 = new SubGraph_CompleteObjectiveStage();

	private int logic_SubGraph_CompleteObjectiveStage_objectiveStage_37;

	private bool logic_SubGraph_CompleteObjectiveStage_isFinalObjective_37;

	private uScript_LockBlockAttach logic_uScript_LockBlockAttach_uScript_LockBlockAttach_40 = new uScript_LockBlockAttach();

	private TankBlock logic_uScript_LockBlockAttach_block_40;

	private bool logic_uScript_LockBlockAttach_Out_40 = true;

	private uScript_IsBlockAnchored logic_uScript_IsBlockAnchored_uScript_IsBlockAnchored_41 = new uScript_IsBlockAnchored();

	private TankBlock logic_uScript_IsBlockAnchored_block_41;

	private bool logic_uScript_IsBlockAnchored_Out_41 = true;

	private bool logic_uScript_IsBlockAnchored_True_41 = true;

	private bool logic_uScript_IsBlockAnchored_False_41 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_45 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_45 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_45 = true;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_45 = true;

	private uScript_IsPlayerInRangeOfBlock logic_uScript_IsPlayerInRangeOfBlock_uScript_IsPlayerInRangeOfBlock_46 = new uScript_IsPlayerInRangeOfBlock();

	private TankBlock logic_uScript_IsPlayerInRangeOfBlock_block_46;

	private float logic_uScript_IsPlayerInRangeOfBlock_range_46 = 50f;

	private bool logic_uScript_IsPlayerInRangeOfBlock_Out_46 = true;

	private bool logic_uScript_IsPlayerInRangeOfBlock_InRange_46 = true;

	private bool logic_uScript_IsPlayerInRangeOfBlock_OutOfRange_46 = true;

	private SubGraph_CompleteObjectiveStage logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_47 = new SubGraph_CompleteObjectiveStage();

	private int logic_SubGraph_CompleteObjectiveStage_objectiveStage_47;

	private bool logic_SubGraph_CompleteObjectiveStage_isFinalObjective_47;

	private uScript_GetPlayerTankWithBlock logic_uScript_GetPlayerTankWithBlock_uScript_GetPlayerTankWithBlock_49 = new uScript_GetPlayerTankWithBlock();

	private BlockTypes logic_uScript_GetPlayerTankWithBlock_block_49;

	private TankBlock logic_uScript_GetPlayerTankWithBlock_tankBlock_49;

	private bool logic_uScript_GetPlayerTankWithBlock_useBlockType_49;

	private Tank logic_uScript_GetPlayerTankWithBlock_Return_49;

	private bool logic_uScript_GetPlayerTankWithBlock_Returned_49 = true;

	private bool logic_uScript_GetPlayerTankWithBlock_NotReturned_49 = true;

	private bool logic_uScript_GetPlayerTankWithBlock_Out_49 = true;

	private uScript_SetEncounterTarget logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_53 = new uScript_SetEncounterTarget();

	private GameObject logic_uScript_SetEncounterTarget_owner_53;

	private object logic_uScript_SetEncounterTarget_visibleObject_53 = "";

	private bool logic_uScript_SetEncounterTarget_Out_53 = true;

	private uScript_SetEncounterTarget logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_54 = new uScript_SetEncounterTarget();

	private GameObject logic_uScript_SetEncounterTarget_owner_54;

	private object logic_uScript_SetEncounterTarget_visibleObject_54 = "";

	private bool logic_uScript_SetEncounterTarget_Out_54 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_60 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_60;

	private bool logic_uScriptAct_SetBool_Out_60 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_60 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_60 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_62 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_62 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_62;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_62 = 0.3f;

	private bool logic_uScript_SpawnTechsFromData_Out_62 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_63 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_63;

	private bool logic_uScriptCon_CompareBool_True_63 = true;

	private bool logic_uScriptCon_CompareBool_False_63 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_65 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_65;

	private bool logic_uScriptCon_CompareBool_True_65 = true;

	private bool logic_uScriptCon_CompareBool_False_65 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_66 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_66;

	private bool logic_uScriptAct_SetBool_Out_66 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_66 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_66 = true;

	private uScript_IsHeartBlockOnline logic_uScript_IsHeartBlockOnline_uScript_IsHeartBlockOnline_68 = new uScript_IsHeartBlockOnline();

	private TankBlock logic_uScript_IsHeartBlockOnline_heartBlock_68;

	private bool logic_uScript_IsHeartBlockOnline_True_68 = true;

	private bool logic_uScript_IsHeartBlockOnline_False_68 = true;

	private uScript_SetOneTechAsEncounterTarget logic_uScript_SetOneTechAsEncounterTarget_uScript_SetOneTechAsEncounterTarget_69 = new uScript_SetOneTechAsEncounterTarget();

	private GameObject logic_uScript_SetOneTechAsEncounterTarget_owner_69;

	private Tank[] logic_uScript_SetOneTechAsEncounterTarget_techs_69 = new Tank[0];

	private Tank logic_uScript_SetOneTechAsEncounterTarget_Return_69;

	private bool logic_uScript_SetOneTechAsEncounterTarget_Out_69 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_72 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_72;

	private bool logic_uScriptCon_CompareBool_True_72 = true;

	private bool logic_uScriptCon_CompareBool_False_72 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_73 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_73;

	private bool logic_uScriptAct_SetBool_Out_73 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_73 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_73 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_77 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_77;

	private bool logic_uScriptAct_SetBool_Out_77 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_77 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_77 = true;

	private uScript_IsHeartBlockOnline logic_uScript_IsHeartBlockOnline_uScript_IsHeartBlockOnline_80 = new uScript_IsHeartBlockOnline();

	private TankBlock logic_uScript_IsHeartBlockOnline_heartBlock_80;

	private bool logic_uScript_IsHeartBlockOnline_True_80 = true;

	private bool logic_uScript_IsHeartBlockOnline_False_80 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_81 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_81;

	private bool logic_uScriptCon_CompareBool_True_81 = true;

	private bool logic_uScriptCon_CompareBool_False_81 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_82 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_82 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_82;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_82 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_82;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_82 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_82 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_82 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_82 = true;

	private uScript_IsBlockAnchored logic_uScript_IsBlockAnchored_uScript_IsBlockAnchored_83 = new uScript_IsBlockAnchored();

	private TankBlock logic_uScript_IsBlockAnchored_block_83;

	private bool logic_uScript_IsBlockAnchored_Out_83 = true;

	private bool logic_uScript_IsBlockAnchored_True_83 = true;

	private bool logic_uScript_IsBlockAnchored_False_83 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_85 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_85;

	private bool logic_uScriptAct_SetBool_Out_85 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_85 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_85 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_87 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_87;

	private bool logic_uScriptCon_CompareBool_True_87 = true;

	private bool logic_uScriptCon_CompareBool_False_87 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_91 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_91;

	private bool logic_uScriptCon_CompareBool_True_91 = true;

	private bool logic_uScriptCon_CompareBool_False_91 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_93 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_93;

	private bool logic_uScriptAct_SetBool_Out_93 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_93 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_93 = true;

	private SubGraph_CompleteObjectiveStage logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_96 = new SubGraph_CompleteObjectiveStage();

	private int logic_SubGraph_CompleteObjectiveStage_objectiveStage_96;

	private bool logic_SubGraph_CompleteObjectiveStage_isFinalObjective_96;

	private uScript_FinishEncounter logic_uScript_FinishEncounter_uScript_FinishEncounter_99 = new uScript_FinishEncounter();

	private GameObject logic_uScript_FinishEncounter_owner_99;

	private bool logic_uScript_FinishEncounter_Out_99 = true;

	private uScript_LockTech logic_uScript_LockTech_uScript_LockTech_101 = new uScript_LockTech();

	private Tank logic_uScript_LockTech_tech_101;

	private uScript_LockTech.TechLockType logic_uScript_LockTech_lockType_101 = uScript_LockTech.TechLockType.LockAll;

	private bool logic_uScript_LockTech_Out_101 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_102 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_102;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_102 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_102 = "HeartBlockFound";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_106 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_106;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_106 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_106 = "HeartBlockSpawned";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_108 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_108;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_108 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_108 = "EnemiesSpawned";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_111 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_111;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_111 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_111 = "StartMsgShown";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_112 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_112;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_112 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_112 = "DefendMsgShown";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_115 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_115;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_115 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_115 = "NotPoweredMsgShown";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_116 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_116;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_116 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_116 = "MissionCompleteMsgShown";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_117 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_117;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_117 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_117 = "EnemiesAliveMsgShown";

	private SubGraph_SaveLoadInt logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_120 = new SubGraph_SaveLoadInt();

	private int logic_SubGraph_SaveLoadInt_restartValue_120 = 1;

	private int logic_SubGraph_SaveLoadInt_integer_120;

	private object logic_SubGraph_SaveLoadInt_intAsVariable_120 = "";

	private string logic_SubGraph_SaveLoadInt_uniqueID_120 = "Stage";

	private SubGraph_LoadObjectiveStates logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_122 = new SubGraph_LoadObjectiveStates();

	private int logic_SubGraph_LoadObjectiveStates_currentObjective_122;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_129 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_129;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_129;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_129;

	private bool logic_uScript_AddMessage_Out_129 = true;

	private bool logic_uScript_AddMessage_Shown_129 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_130 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_130;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_130;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_130;

	private bool logic_uScript_AddMessage_Out_130 = true;

	private bool logic_uScript_AddMessage_Shown_130 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_135 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_135;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_135;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_135;

	private bool logic_uScript_AddMessage_Out_135 = true;

	private bool logic_uScript_AddMessage_Shown_135 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_138 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_138;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_138;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_138;

	private bool logic_uScript_AddMessage_Out_138 = true;

	private bool logic_uScript_AddMessage_Shown_138 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_139 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_139;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_139;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_139;

	private bool logic_uScript_AddMessage_Out_139 = true;

	private bool logic_uScript_AddMessage_Shown_139 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_144 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_144;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_144;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_144;

	private bool logic_uScript_AddMessage_Out_144 = true;

	private bool logic_uScript_AddMessage_Shown_144 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_145 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_145;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_145;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_145;

	private bool logic_uScript_AddMessage_Out_145 = true;

	private bool logic_uScript_AddMessage_Shown_145 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_149 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_149;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_149;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_149;

	private bool logic_uScript_AddMessage_Out_149 = true;

	private bool logic_uScript_AddMessage_Shown_149 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_151 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_151;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_151;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_151;

	private bool logic_uScript_AddMessage_Out_151 = true;

	private bool logic_uScript_AddMessage_Shown_151 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_154 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_154;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_154;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_154;

	private bool logic_uScript_AddMessage_Out_154 = true;

	private bool logic_uScript_AddMessage_Shown_154 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_157 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_157 = true;

	private uScriptCon_CompareInt logic_uScriptCon_CompareInt_uScriptCon_CompareInt_158 = new uScriptCon_CompareInt();

	private int logic_uScriptCon_CompareInt_A_158;

	private int logic_uScriptCon_CompareInt_B_158 = 3;

	private bool logic_uScriptCon_CompareInt_GreaterThan_158 = true;

	private bool logic_uScriptCon_CompareInt_GreaterThanOrEqualTo_158 = true;

	private bool logic_uScriptCon_CompareInt_EqualTo_158 = true;

	private bool logic_uScriptCon_CompareInt_NotEqualTo_158 = true;

	private bool logic_uScriptCon_CompareInt_LessThanOrEqualTo_158 = true;

	private bool logic_uScriptCon_CompareInt_LessThan_158 = true;

	private uScript_LockVisibleStackAccept logic_uScript_LockVisibleStackAccept_uScript_LockVisibleStackAccept_161 = new uScript_LockVisibleStackAccept();

	private object logic_uScript_LockVisibleStackAccept_targetObject_161 = "";

	private bool logic_uScript_LockVisibleStackAccept_Out_161 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_164 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_164;

	private bool logic_uScriptCon_CompareBool_True_164 = true;

	private bool logic_uScriptCon_CompareBool_False_164 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_165 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_165;

	private bool logic_uScriptAct_SetBool_Out_165 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_165 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_165 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_167 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_167;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_167 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_167 = "LockHeartBlock";

	private uScript_ShowHint logic_uScript_ShowHint_uScript_ShowHint_169 = new uScript_ShowHint();

	private GameHints.HintID logic_uScript_ShowHint_hintId_169 = GameHints.HintID.RecallStoredTech;

	private bool logic_uScript_ShowHint_Out_169 = true;

	private uScript_EnableBlockPalette logic_uScript_EnableBlockPalette_uScript_EnableBlockPalette_170 = new uScript_EnableBlockPalette();

	private bool logic_uScript_EnableBlockPalette_Out_170 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_378 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_378;

	private bool logic_uScriptAct_SetBool_Out_378 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_378 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_378 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_383 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_383;

	private bool logic_uScriptAct_SetBool_Out_383 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_383 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_383 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_386 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_386;

	private bool logic_uScriptAct_SetBool_Out_386 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_386 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_386 = true;

	private void SyncUnityHooks()
	{
		SyncEventListeners();
		if (null == owner_Connection_0 || !m_RegisteredForEvents)
		{
			owner_Connection_0 = parentGameObject;
			if (null != owner_Connection_0)
			{
				uScript_EncounterUpdate uScript_EncounterUpdate2 = owner_Connection_0.GetComponent<uScript_EncounterUpdate>();
				if (null == uScript_EncounterUpdate2)
				{
					uScript_EncounterUpdate2 = owner_Connection_0.AddComponent<uScript_EncounterUpdate>();
				}
				if (null != uScript_EncounterUpdate2)
				{
					uScript_EncounterUpdate2.OnUpdate += Instance_OnUpdate_1;
					uScript_EncounterUpdate2.OnSuspend += Instance_OnSuspend_1;
					uScript_EncounterUpdate2.OnResume += Instance_OnResume_1;
				}
			}
		}
		if (null == owner_Connection_11 || !m_RegisteredForEvents)
		{
			owner_Connection_11 = parentGameObject;
		}
		if (null == owner_Connection_15 || !m_RegisteredForEvents)
		{
			owner_Connection_15 = parentGameObject;
		}
		if (null == owner_Connection_22 || !m_RegisteredForEvents)
		{
			owner_Connection_22 = parentGameObject;
		}
		if (null == owner_Connection_32 || !m_RegisteredForEvents)
		{
			owner_Connection_32 = parentGameObject;
		}
		if (null == owner_Connection_55 || !m_RegisteredForEvents)
		{
			owner_Connection_55 = parentGameObject;
		}
		if (null == owner_Connection_57 || !m_RegisteredForEvents)
		{
			owner_Connection_57 = parentGameObject;
		}
		if (null == owner_Connection_58 || !m_RegisteredForEvents)
		{
			owner_Connection_58 = parentGameObject;
		}
		if (null == owner_Connection_71 || !m_RegisteredForEvents)
		{
			owner_Connection_71 = parentGameObject;
		}
		if (null == owner_Connection_86 || !m_RegisteredForEvents)
		{
			owner_Connection_86 = parentGameObject;
		}
		if (null == owner_Connection_97 || !m_RegisteredForEvents)
		{
			owner_Connection_97 = parentGameObject;
		}
		if (!(null == owner_Connection_105) && m_RegisteredForEvents)
		{
			return;
		}
		owner_Connection_105 = parentGameObject;
		if (null != owner_Connection_105)
		{
			uScript_SaveLoad uScript_SaveLoad2 = owner_Connection_105.GetComponent<uScript_SaveLoad>();
			if (null == uScript_SaveLoad2)
			{
				uScript_SaveLoad2 = owner_Connection_105.AddComponent<uScript_SaveLoad>();
			}
			if (null != uScript_SaveLoad2)
			{
				uScript_SaveLoad2.SaveEvent += Instance_SaveEvent_104;
				uScript_SaveLoad2.LoadEvent += Instance_LoadEvent_104;
				uScript_SaveLoad2.RestartEvent += Instance_RestartEvent_104;
			}
		}
	}

	private void RegisterForUnityHooks()
	{
		SyncEventListeners();
		if (!m_RegisteredForEvents && null != owner_Connection_0)
		{
			uScript_EncounterUpdate uScript_EncounterUpdate2 = owner_Connection_0.GetComponent<uScript_EncounterUpdate>();
			if (null == uScript_EncounterUpdate2)
			{
				uScript_EncounterUpdate2 = owner_Connection_0.AddComponent<uScript_EncounterUpdate>();
			}
			if (null != uScript_EncounterUpdate2)
			{
				uScript_EncounterUpdate2.OnUpdate += Instance_OnUpdate_1;
				uScript_EncounterUpdate2.OnSuspend += Instance_OnSuspend_1;
				uScript_EncounterUpdate2.OnResume += Instance_OnResume_1;
			}
		}
		if (!m_RegisteredForEvents && null != owner_Connection_105)
		{
			uScript_SaveLoad uScript_SaveLoad2 = owner_Connection_105.GetComponent<uScript_SaveLoad>();
			if (null == uScript_SaveLoad2)
			{
				uScript_SaveLoad2 = owner_Connection_105.AddComponent<uScript_SaveLoad>();
			}
			if (null != uScript_SaveLoad2)
			{
				uScript_SaveLoad2.SaveEvent += Instance_SaveEvent_104;
				uScript_SaveLoad2.LoadEvent += Instance_LoadEvent_104;
				uScript_SaveLoad2.RestartEvent += Instance_RestartEvent_104;
			}
		}
	}

	private void SyncEventListeners()
	{
	}

	private void UnregisterEventListeners()
	{
		if (null != owner_Connection_0)
		{
			uScript_EncounterUpdate component = owner_Connection_0.GetComponent<uScript_EncounterUpdate>();
			if (null != component)
			{
				component.OnUpdate -= Instance_OnUpdate_1;
				component.OnSuspend -= Instance_OnSuspend_1;
				component.OnResume -= Instance_OnResume_1;
			}
		}
		if (null != owner_Connection_105)
		{
			uScript_SaveLoad component2 = owner_Connection_105.GetComponent<uScript_SaveLoad>();
			if (null != component2)
			{
				component2.SaveEvent -= Instance_SaveEvent_104;
				component2.LoadEvent -= Instance_LoadEvent_104;
				component2.RestartEvent -= Instance_RestartEvent_104;
			}
		}
	}

	public override void SetParent(GameObject g)
	{
		parentGameObject = g;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_2.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_4.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_6.SetParent(g);
		logic_uScript_GetAndCheckBlocks_uScript_GetAndCheckBlocks_7.SetParent(g);
		logic_uScript_AccessListBlock_uScript_AccessListBlock_9.SetParent(g);
		logic_uScript_HealBlock_uScript_HealBlock_12.SetParent(g);
		logic_uScript_MoveEncounterWithVisible_uScript_MoveEncounterWithVisible_14.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_18.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_20.SetParent(g);
		logic_uScript_GetPositionInEncounter_uScript_GetPositionInEncounter_23.SetParent(g);
		logic_uScript_PlayerInRange_uScript_PlayerInRange_25.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_29.SetParent(g);
		logic_uScript_SpawnBlocksFromData_uScript_SpawnBlocksFromData_33.SetParent(g);
		logic_uScript_IsPlayerInRangeOfBlock_uScript_IsPlayerInRangeOfBlock_34.SetParent(g);
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_37.SetParent(g);
		logic_uScript_LockBlockAttach_uScript_LockBlockAttach_40.SetParent(g);
		logic_uScript_IsBlockAnchored_uScript_IsBlockAnchored_41.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_45.SetParent(g);
		logic_uScript_IsPlayerInRangeOfBlock_uScript_IsPlayerInRangeOfBlock_46.SetParent(g);
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_47.SetParent(g);
		logic_uScript_GetPlayerTankWithBlock_uScript_GetPlayerTankWithBlock_49.SetParent(g);
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_53.SetParent(g);
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_54.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_60.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_62.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_63.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_65.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_66.SetParent(g);
		logic_uScript_IsHeartBlockOnline_uScript_IsHeartBlockOnline_68.SetParent(g);
		logic_uScript_SetOneTechAsEncounterTarget_uScript_SetOneTechAsEncounterTarget_69.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_72.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_73.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_77.SetParent(g);
		logic_uScript_IsHeartBlockOnline_uScript_IsHeartBlockOnline_80.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_81.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_82.SetParent(g);
		logic_uScript_IsBlockAnchored_uScript_IsBlockAnchored_83.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_85.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_87.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_91.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_93.SetParent(g);
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_96.SetParent(g);
		logic_uScript_FinishEncounter_uScript_FinishEncounter_99.SetParent(g);
		logic_uScript_LockTech_uScript_LockTech_101.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_102.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_106.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_108.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_111.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_112.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_115.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_116.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_117.SetParent(g);
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_120.SetParent(g);
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_122.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_129.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_130.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_135.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_138.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_139.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_144.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_145.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_149.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_151.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_154.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_157.SetParent(g);
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_158.SetParent(g);
		logic_uScript_LockVisibleStackAccept_uScript_LockVisibleStackAccept_161.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_164.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_165.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_167.SetParent(g);
		logic_uScript_ShowHint_uScript_ShowHint_169.SetParent(g);
		logic_uScript_EnableBlockPalette_uScript_EnableBlockPalette_170.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_378.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_383.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_386.SetParent(g);
		owner_Connection_0 = parentGameObject;
		owner_Connection_11 = parentGameObject;
		owner_Connection_15 = parentGameObject;
		owner_Connection_22 = parentGameObject;
		owner_Connection_32 = parentGameObject;
		owner_Connection_55 = parentGameObject;
		owner_Connection_57 = parentGameObject;
		owner_Connection_58 = parentGameObject;
		owner_Connection_71 = parentGameObject;
		owner_Connection_86 = parentGameObject;
		owner_Connection_97 = parentGameObject;
		owner_Connection_105 = parentGameObject;
	}

	public void Awake()
	{
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_37.Awake();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_47.Awake();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_96.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_102.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_106.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_108.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_111.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_112.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_115.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_116.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_117.Awake();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_120.Awake();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_122.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_167.Awake();
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_2.Output1 += uScriptCon_ManualSwitch_Output1_2;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_2.Output2 += uScriptCon_ManualSwitch_Output2_2;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_2.Output3 += uScriptCon_ManualSwitch_Output3_2;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_2.Output4 += uScriptCon_ManualSwitch_Output4_2;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_2.Output5 += uScriptCon_ManualSwitch_Output5_2;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_2.Output6 += uScriptCon_ManualSwitch_Output6_2;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_2.Output7 += uScriptCon_ManualSwitch_Output7_2;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_2.Output8 += uScriptCon_ManualSwitch_Output8_2;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_37.Out += SubGraph_CompleteObjectiveStage_Out_37;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_47.Out += SubGraph_CompleteObjectiveStage_Out_47;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_96.Out += SubGraph_CompleteObjectiveStage_Out_96;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_102.Save_Out += SubGraph_SaveLoadBool_Save_Out_102;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_102.Load_Out += SubGraph_SaveLoadBool_Load_Out_102;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_102.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_102;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_106.Save_Out += SubGraph_SaveLoadBool_Save_Out_106;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_106.Load_Out += SubGraph_SaveLoadBool_Load_Out_106;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_106.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_106;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_108.Save_Out += SubGraph_SaveLoadBool_Save_Out_108;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_108.Load_Out += SubGraph_SaveLoadBool_Load_Out_108;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_108.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_108;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_111.Save_Out += SubGraph_SaveLoadBool_Save_Out_111;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_111.Load_Out += SubGraph_SaveLoadBool_Load_Out_111;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_111.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_111;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_112.Save_Out += SubGraph_SaveLoadBool_Save_Out_112;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_112.Load_Out += SubGraph_SaveLoadBool_Load_Out_112;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_112.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_112;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_115.Save_Out += SubGraph_SaveLoadBool_Save_Out_115;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_115.Load_Out += SubGraph_SaveLoadBool_Load_Out_115;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_115.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_115;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_116.Save_Out += SubGraph_SaveLoadBool_Save_Out_116;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_116.Load_Out += SubGraph_SaveLoadBool_Load_Out_116;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_116.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_116;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_117.Save_Out += SubGraph_SaveLoadBool_Save_Out_117;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_117.Load_Out += SubGraph_SaveLoadBool_Load_Out_117;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_117.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_117;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_120.Save_Out += SubGraph_SaveLoadInt_Save_Out_120;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_120.Load_Out += SubGraph_SaveLoadInt_Load_Out_120;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_120.Restart_Out += SubGraph_SaveLoadInt_Restart_Out_120;
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_122.Out += SubGraph_LoadObjectiveStates_Out_122;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_167.Save_Out += SubGraph_SaveLoadBool_Save_Out_167;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_167.Load_Out += SubGraph_SaveLoadBool_Load_Out_167;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_167.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_167;
	}

	public void Start()
	{
		SyncUnityHooks();
		m_RegisteredForEvents = true;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_37.Start();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_47.Start();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_96.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_102.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_106.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_108.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_111.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_112.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_115.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_116.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_117.Start();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_120.Start();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_122.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_167.Start();
	}

	public void OnEnable()
	{
		RegisterForUnityHooks();
		m_RegisteredForEvents = true;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_37.OnEnable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_47.OnEnable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_96.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_102.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_106.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_108.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_111.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_112.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_115.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_116.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_117.OnEnable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_120.OnEnable();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_122.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_167.OnEnable();
	}

	public void OnDisable()
	{
		logic_uScript_IsPlayerInRangeOfBlock_uScript_IsPlayerInRangeOfBlock_34.OnDisable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_37.OnDisable();
		logic_uScript_IsBlockAnchored_uScript_IsBlockAnchored_41.OnDisable();
		logic_uScript_IsPlayerInRangeOfBlock_uScript_IsPlayerInRangeOfBlock_46.OnDisable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_47.OnDisable();
		logic_uScript_SetOneTechAsEncounterTarget_uScript_SetOneTechAsEncounterTarget_69.OnDisable();
		logic_uScript_IsBlockAnchored_uScript_IsBlockAnchored_83.OnDisable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_96.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_102.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_106.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_108.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_111.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_112.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_115.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_116.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_117.OnDisable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_120.OnDisable();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_122.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_129.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_130.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_135.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_138.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_139.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_144.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_145.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_149.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_151.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_154.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_167.OnDisable();
		UnregisterEventListeners();
		m_RegisteredForEvents = false;
	}

	public void Update()
	{
		SyncEventListeners();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_37.Update();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_47.Update();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_96.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_102.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_106.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_108.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_111.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_112.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_115.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_116.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_117.Update();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_120.Update();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_122.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_167.Update();
	}

	public void OnDestroy()
	{
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_37.OnDestroy();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_47.OnDestroy();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_96.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_102.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_106.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_108.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_111.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_112.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_115.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_116.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_117.OnDestroy();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_120.OnDestroy();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_122.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_167.OnDestroy();
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_2.Output1 -= uScriptCon_ManualSwitch_Output1_2;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_2.Output2 -= uScriptCon_ManualSwitch_Output2_2;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_2.Output3 -= uScriptCon_ManualSwitch_Output3_2;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_2.Output4 -= uScriptCon_ManualSwitch_Output4_2;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_2.Output5 -= uScriptCon_ManualSwitch_Output5_2;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_2.Output6 -= uScriptCon_ManualSwitch_Output6_2;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_2.Output7 -= uScriptCon_ManualSwitch_Output7_2;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_2.Output8 -= uScriptCon_ManualSwitch_Output8_2;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_37.Out -= SubGraph_CompleteObjectiveStage_Out_37;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_47.Out -= SubGraph_CompleteObjectiveStage_Out_47;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_96.Out -= SubGraph_CompleteObjectiveStage_Out_96;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_102.Save_Out -= SubGraph_SaveLoadBool_Save_Out_102;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_102.Load_Out -= SubGraph_SaveLoadBool_Load_Out_102;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_102.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_102;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_106.Save_Out -= SubGraph_SaveLoadBool_Save_Out_106;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_106.Load_Out -= SubGraph_SaveLoadBool_Load_Out_106;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_106.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_106;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_108.Save_Out -= SubGraph_SaveLoadBool_Save_Out_108;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_108.Load_Out -= SubGraph_SaveLoadBool_Load_Out_108;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_108.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_108;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_111.Save_Out -= SubGraph_SaveLoadBool_Save_Out_111;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_111.Load_Out -= SubGraph_SaveLoadBool_Load_Out_111;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_111.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_111;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_112.Save_Out -= SubGraph_SaveLoadBool_Save_Out_112;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_112.Load_Out -= SubGraph_SaveLoadBool_Load_Out_112;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_112.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_112;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_115.Save_Out -= SubGraph_SaveLoadBool_Save_Out_115;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_115.Load_Out -= SubGraph_SaveLoadBool_Load_Out_115;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_115.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_115;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_116.Save_Out -= SubGraph_SaveLoadBool_Save_Out_116;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_116.Load_Out -= SubGraph_SaveLoadBool_Load_Out_116;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_116.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_116;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_117.Save_Out -= SubGraph_SaveLoadBool_Save_Out_117;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_117.Load_Out -= SubGraph_SaveLoadBool_Load_Out_117;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_117.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_117;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_120.Save_Out -= SubGraph_SaveLoadInt_Save_Out_120;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_120.Load_Out -= SubGraph_SaveLoadInt_Load_Out_120;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_120.Restart_Out -= SubGraph_SaveLoadInt_Restart_Out_120;
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_122.Out -= SubGraph_LoadObjectiveStates_Out_122;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_167.Save_Out -= SubGraph_SaveLoadBool_Save_Out_167;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_167.Load_Out -= SubGraph_SaveLoadBool_Load_Out_167;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_167.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_167;
	}

	private void Instance_OnUpdate_1(object o, EventArgs e)
	{
		Relay_OnUpdate_1();
	}

	private void Instance_OnSuspend_1(object o, EventArgs e)
	{
		Relay_OnSuspend_1();
	}

	private void Instance_OnResume_1(object o, EventArgs e)
	{
		Relay_OnResume_1();
	}

	private void Instance_SaveEvent_104(object o, EventArgs e)
	{
		Relay_SaveEvent_104();
	}

	private void Instance_LoadEvent_104(object o, EventArgs e)
	{
		Relay_LoadEvent_104();
	}

	private void Instance_RestartEvent_104(object o, EventArgs e)
	{
		Relay_RestartEvent_104();
	}

	private void uScriptCon_ManualSwitch_Output1_2(object o, EventArgs e)
	{
		Relay_Output1_2();
	}

	private void uScriptCon_ManualSwitch_Output2_2(object o, EventArgs e)
	{
		Relay_Output2_2();
	}

	private void uScriptCon_ManualSwitch_Output3_2(object o, EventArgs e)
	{
		Relay_Output3_2();
	}

	private void uScriptCon_ManualSwitch_Output4_2(object o, EventArgs e)
	{
		Relay_Output4_2();
	}

	private void uScriptCon_ManualSwitch_Output5_2(object o, EventArgs e)
	{
		Relay_Output5_2();
	}

	private void uScriptCon_ManualSwitch_Output6_2(object o, EventArgs e)
	{
		Relay_Output6_2();
	}

	private void uScriptCon_ManualSwitch_Output7_2(object o, EventArgs e)
	{
		Relay_Output7_2();
	}

	private void uScriptCon_ManualSwitch_Output8_2(object o, EventArgs e)
	{
		Relay_Output8_2();
	}

	private void SubGraph_CompleteObjectiveStage_Out_37(object o, SubGraph_CompleteObjectiveStage.LogicEventArgs e)
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_37 = e.objectiveStage;
		local_Stage_System_Int32 = logic_SubGraph_CompleteObjectiveStage_objectiveStage_37;
		Relay_Out_37();
	}

	private void SubGraph_CompleteObjectiveStage_Out_47(object o, SubGraph_CompleteObjectiveStage.LogicEventArgs e)
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_47 = e.objectiveStage;
		local_Stage_System_Int32 = logic_SubGraph_CompleteObjectiveStage_objectiveStage_47;
		Relay_Out_47();
	}

	private void SubGraph_CompleteObjectiveStage_Out_96(object o, SubGraph_CompleteObjectiveStage.LogicEventArgs e)
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_96 = e.objectiveStage;
		local_Stage_System_Int32 = logic_SubGraph_CompleteObjectiveStage_objectiveStage_96;
		Relay_Out_96();
	}

	private void SubGraph_SaveLoadBool_Save_Out_102(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_102 = e.boolean;
		local_HeartBlockFound_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_102;
		Relay_Save_Out_102();
	}

	private void SubGraph_SaveLoadBool_Load_Out_102(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_102 = e.boolean;
		local_HeartBlockFound_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_102;
		Relay_Load_Out_102();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_102(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_102 = e.boolean;
		local_HeartBlockFound_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_102;
		Relay_Restart_Out_102();
	}

	private void SubGraph_SaveLoadBool_Save_Out_106(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_106 = e.boolean;
		local_HeartBlockSpawned_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_106;
		Relay_Save_Out_106();
	}

	private void SubGraph_SaveLoadBool_Load_Out_106(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_106 = e.boolean;
		local_HeartBlockSpawned_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_106;
		Relay_Load_Out_106();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_106(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_106 = e.boolean;
		local_HeartBlockSpawned_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_106;
		Relay_Restart_Out_106();
	}

	private void SubGraph_SaveLoadBool_Save_Out_108(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_108 = e.boolean;
		local_EnemiesSpawned_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_108;
		Relay_Save_Out_108();
	}

	private void SubGraph_SaveLoadBool_Load_Out_108(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_108 = e.boolean;
		local_EnemiesSpawned_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_108;
		Relay_Load_Out_108();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_108(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_108 = e.boolean;
		local_EnemiesSpawned_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_108;
		Relay_Restart_Out_108();
	}

	private void SubGraph_SaveLoadBool_Save_Out_111(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_111 = e.boolean;
		local_StartMsgShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_111;
		Relay_Save_Out_111();
	}

	private void SubGraph_SaveLoadBool_Load_Out_111(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_111 = e.boolean;
		local_StartMsgShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_111;
		Relay_Load_Out_111();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_111(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_111 = e.boolean;
		local_StartMsgShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_111;
		Relay_Restart_Out_111();
	}

	private void SubGraph_SaveLoadBool_Save_Out_112(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_112 = e.boolean;
		local_DefendMsgShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_112;
		Relay_Save_Out_112();
	}

	private void SubGraph_SaveLoadBool_Load_Out_112(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_112 = e.boolean;
		local_DefendMsgShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_112;
		Relay_Load_Out_112();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_112(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_112 = e.boolean;
		local_DefendMsgShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_112;
		Relay_Restart_Out_112();
	}

	private void SubGraph_SaveLoadBool_Save_Out_115(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_115 = e.boolean;
		local_NotPoweredMsgShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_115;
		Relay_Save_Out_115();
	}

	private void SubGraph_SaveLoadBool_Load_Out_115(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_115 = e.boolean;
		local_NotPoweredMsgShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_115;
		Relay_Load_Out_115();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_115(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_115 = e.boolean;
		local_NotPoweredMsgShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_115;
		Relay_Restart_Out_115();
	}

	private void SubGraph_SaveLoadBool_Save_Out_116(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_116 = e.boolean;
		local_MissionCompleteMsgShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_116;
		Relay_Save_Out_116();
	}

	private void SubGraph_SaveLoadBool_Load_Out_116(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_116 = e.boolean;
		local_MissionCompleteMsgShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_116;
		Relay_Load_Out_116();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_116(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_116 = e.boolean;
		local_MissionCompleteMsgShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_116;
		Relay_Restart_Out_116();
	}

	private void SubGraph_SaveLoadBool_Save_Out_117(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_117 = e.boolean;
		local_EnemiesAliveMsgShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_117;
		Relay_Save_Out_117();
	}

	private void SubGraph_SaveLoadBool_Load_Out_117(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_117 = e.boolean;
		local_EnemiesAliveMsgShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_117;
		Relay_Load_Out_117();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_117(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_117 = e.boolean;
		local_EnemiesAliveMsgShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_117;
		Relay_Restart_Out_117();
	}

	private void SubGraph_SaveLoadInt_Save_Out_120(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_120 = e.integer;
		local_Stage_System_Int32 = logic_SubGraph_SaveLoadInt_integer_120;
		Relay_Save_Out_120();
	}

	private void SubGraph_SaveLoadInt_Load_Out_120(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_120 = e.integer;
		local_Stage_System_Int32 = logic_SubGraph_SaveLoadInt_integer_120;
		Relay_Load_Out_120();
	}

	private void SubGraph_SaveLoadInt_Restart_Out_120(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_120 = e.integer;
		local_Stage_System_Int32 = logic_SubGraph_SaveLoadInt_integer_120;
		Relay_Restart_Out_120();
	}

	private void SubGraph_LoadObjectiveStates_Out_122(object o, SubGraph_LoadObjectiveStates.LogicEventArgs e)
	{
		Relay_Out_122();
	}

	private void SubGraph_SaveLoadBool_Save_Out_167(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_167 = e.boolean;
		local_LockHeartBlock_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_167;
		Relay_Save_Out_167();
	}

	private void SubGraph_SaveLoadBool_Load_Out_167(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_167 = e.boolean;
		local_LockHeartBlock_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_167;
		Relay_Load_Out_167();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_167(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_167 = e.boolean;
		local_LockHeartBlock_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_167;
		Relay_Restart_Out_167();
	}

	private void Relay_OnUpdate_1()
	{
		Relay_In_4();
	}

	private void Relay_OnSuspend_1()
	{
	}

	private void Relay_OnResume_1()
	{
	}

	private void Relay_Output1_2()
	{
		Relay_In_34();
	}

	private void Relay_Output2_2()
	{
		Relay_In_41();
	}

	private void Relay_Output3_2()
	{
		Relay_In_63();
	}

	private void Relay_Output4_2()
	{
		Relay_In_151();
	}

	private void Relay_Output5_2()
	{
	}

	private void Relay_Output6_2()
	{
	}

	private void Relay_Output7_2()
	{
	}

	private void Relay_Output8_2()
	{
	}

	private void Relay_In_2()
	{
		logic_uScriptCon_ManualSwitch_CurrentOutput_2 = local_Stage_System_Int32;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_2.In(logic_uScriptCon_ManualSwitch_CurrentOutput_2);
	}

	private void Relay_In_4()
	{
		logic_uScriptCon_CompareBool_Bool_4 = local_StartMsgShown_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_4.In(logic_uScriptCon_CompareBool_Bool_4);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_4.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_4.False;
		if (num)
		{
			Relay_In_20();
		}
		if (flag)
		{
			Relay_True_6();
		}
	}

	private void Relay_True_6()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_6.True(out logic_uScriptAct_SetBool_Target_6);
		local_StartMsgShown_System_Boolean = logic_uScriptAct_SetBool_Target_6;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_6.Out)
		{
			Relay_In_129();
		}
	}

	private void Relay_False_6()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_6.False(out logic_uScriptAct_SetBool_Target_6);
		local_StartMsgShown_System_Boolean = logic_uScriptAct_SetBool_Target_6;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_6.Out)
		{
			Relay_In_129();
		}
	}

	private void Relay_In_7()
	{
		int num = 0;
		Array array = heartBlockData;
		if (logic_uScript_GetAndCheckBlocks_blockData_7.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckBlocks_blockData_7, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckBlocks_blockData_7, num, array.Length);
		num += array.Length;
		logic_uScript_GetAndCheckBlocks_ownerNode_7 = owner_Connection_11;
		int num2 = 0;
		Array array2 = local_16_TankBlockArray;
		if (logic_uScript_GetAndCheckBlocks_blocks_7.Length != num2 + array2.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckBlocks_blocks_7, num2 + array2.Length);
		}
		Array.Copy(array2, 0, logic_uScript_GetAndCheckBlocks_blocks_7, num2, array2.Length);
		num2 += array2.Length;
		logic_uScript_GetAndCheckBlocks_uScript_GetAndCheckBlocks_7.In(logic_uScript_GetAndCheckBlocks_blockData_7, logic_uScript_GetAndCheckBlocks_ownerNode_7, ref logic_uScript_GetAndCheckBlocks_blocks_7);
		local_16_TankBlockArray = logic_uScript_GetAndCheckBlocks_blocks_7;
		bool allAlive = logic_uScript_GetAndCheckBlocks_uScript_GetAndCheckBlocks_7.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckBlocks_uScript_GetAndCheckBlocks_7.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckBlocks_uScript_GetAndCheckBlocks_7.AllDead;
		if (allAlive)
		{
			Relay_AtIndex_9();
		}
		if (someAlive)
		{
			Relay_AtIndex_9();
		}
		if (allDead)
		{
			Relay_In_154();
		}
	}

	private void Relay_AtIndex_9()
	{
		int num = 0;
		Array array = local_16_TankBlockArray;
		if (logic_uScript_AccessListBlock_blockList_9.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListBlock_blockList_9, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListBlock_blockList_9, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListBlock_uScript_AccessListBlock_9.AtIndex(ref logic_uScript_AccessListBlock_blockList_9, logic_uScript_AccessListBlock_index_9, out logic_uScript_AccessListBlock_value_9);
		local_16_TankBlockArray = logic_uScript_AccessListBlock_blockList_9;
		local_HeartBlock_TankBlock = logic_uScript_AccessListBlock_value_9;
		if (logic_uScript_AccessListBlock_uScript_AccessListBlock_9.Out)
		{
			Relay_In_14();
		}
	}

	private void Relay_In_12()
	{
		logic_uScript_HealBlock_block_12 = local_HeartBlock_TankBlock;
		logic_uScript_HealBlock_uScript_HealBlock_12.In(logic_uScript_HealBlock_block_12, logic_uScript_HealBlock_healAmount_12);
		if (logic_uScript_HealBlock_uScript_HealBlock_12.Out)
		{
			Relay_In_161();
		}
	}

	private void Relay_In_14()
	{
		logic_uScript_MoveEncounterWithVisible_ownerNode_14 = owner_Connection_15;
		logic_uScript_MoveEncounterWithVisible_visibleObject_14 = local_HeartBlock_TankBlock;
		logic_uScript_MoveEncounterWithVisible_uScript_MoveEncounterWithVisible_14.In(logic_uScript_MoveEncounterWithVisible_ownerNode_14, logic_uScript_MoveEncounterWithVisible_visibleObject_14);
		if (logic_uScript_MoveEncounterWithVisible_uScript_MoveEncounterWithVisible_14.Out)
		{
			Relay_In_12();
		}
	}

	private void Relay_True_18()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_18.True(out logic_uScriptAct_SetBool_Target_18);
		local_HeartBlockSpawned_System_Boolean = logic_uScriptAct_SetBool_Target_18;
	}

	private void Relay_False_18()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_18.False(out logic_uScriptAct_SetBool_Target_18);
		local_HeartBlockSpawned_System_Boolean = logic_uScriptAct_SetBool_Target_18;
	}

	private void Relay_In_20()
	{
		logic_uScriptCon_CompareBool_Bool_20 = local_HeartBlockSpawned_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_20.In(logic_uScriptCon_CompareBool_Bool_20);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_20.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_20.False;
		if (num)
		{
			Relay_In_7();
		}
		if (flag)
		{
			Relay_In_23();
		}
	}

	private void Relay_In_23()
	{
		logic_uScript_GetPositionInEncounter_ownerNode_23 = owner_Connection_22;
		logic_uScript_GetPositionInEncounter_posName_23 = heartBlockPosition;
		logic_uScript_GetPositionInEncounter_Return_23 = logic_uScript_GetPositionInEncounter_uScript_GetPositionInEncounter_23.In(logic_uScript_GetPositionInEncounter_ownerNode_23, logic_uScript_GetPositionInEncounter_posName_23);
		local_27_UnityEngine_Vector3 = logic_uScript_GetPositionInEncounter_Return_23;
		if (logic_uScript_GetPositionInEncounter_uScript_GetPositionInEncounter_23.Out)
		{
			Relay_In_25();
		}
	}

	private void Relay_In_25()
	{
		logic_uScript_PlayerInRange_position_25 = local_27_UnityEngine_Vector3;
		logic_uScript_PlayerInRange_range_25 = heartBlockSpawnDist;
		logic_uScript_PlayerInRange_uScript_PlayerInRange_25.In(logic_uScript_PlayerInRange_position_25, logic_uScript_PlayerInRange_range_25);
		if (logic_uScript_PlayerInRange_uScript_PlayerInRange_25.True)
		{
			Relay_In_33();
		}
	}

	private void Relay_True_29()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_29.True(out logic_uScriptAct_SetBool_Target_29);
		local_HeartBlockSpawned_System_Boolean = logic_uScriptAct_SetBool_Target_29;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_29.Out)
		{
			Relay_In_7();
		}
	}

	private void Relay_False_29()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_29.False(out logic_uScriptAct_SetBool_Target_29);
		local_HeartBlockSpawned_System_Boolean = logic_uScriptAct_SetBool_Target_29;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_29.Out)
		{
			Relay_In_7();
		}
	}

	private void Relay_In_33()
	{
		int num = 0;
		Array array = heartBlockData;
		if (logic_uScript_SpawnBlocksFromData_blockData_33.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SpawnBlocksFromData_blockData_33, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SpawnBlocksFromData_blockData_33, num, array.Length);
		num += array.Length;
		logic_uScript_SpawnBlocksFromData_ownerNode_33 = owner_Connection_32;
		logic_uScript_SpawnBlocksFromData_uScript_SpawnBlocksFromData_33.In(logic_uScript_SpawnBlocksFromData_blockData_33, logic_uScript_SpawnBlocksFromData_ownerNode_33);
		if (logic_uScript_SpawnBlocksFromData_uScript_SpawnBlocksFromData_33.Out)
		{
			Relay_True_29();
		}
	}

	private void Relay_In_34()
	{
		logic_uScript_IsPlayerInRangeOfBlock_block_34 = local_HeartBlock_TankBlock;
		logic_uScript_IsPlayerInRangeOfBlock_range_34 = heartBlockFoundDist;
		logic_uScript_IsPlayerInRangeOfBlock_uScript_IsPlayerInRangeOfBlock_34.In(logic_uScript_IsPlayerInRangeOfBlock_block_34, logic_uScript_IsPlayerInRangeOfBlock_range_34);
		if (logic_uScript_IsPlayerInRangeOfBlock_uScript_IsPlayerInRangeOfBlock_34.InRange)
		{
			Relay_In_37();
		}
	}

	private void Relay_Out_37()
	{
	}

	private void Relay_In_37()
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_37 = local_Stage_System_Int32;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_37.In(ref logic_SubGraph_CompleteObjectiveStage_objectiveStage_37, logic_SubGraph_CompleteObjectiveStage_isFinalObjective_37);
	}

	private void Relay_In_40()
	{
		logic_uScript_LockBlockAttach_block_40 = local_HeartBlock_TankBlock;
		logic_uScript_LockBlockAttach_uScript_LockBlockAttach_40.In(logic_uScript_LockBlockAttach_block_40);
		if (logic_uScript_LockBlockAttach_uScript_LockBlockAttach_40.Out)
		{
			Relay_In_49();
		}
	}

	private void Relay_In_41()
	{
		logic_uScript_IsBlockAnchored_block_41 = local_HeartBlock_TankBlock;
		logic_uScript_IsBlockAnchored_uScript_IsBlockAnchored_41.In(logic_uScript_IsBlockAnchored_block_41);
		bool num = logic_uScript_IsBlockAnchored_uScript_IsBlockAnchored_41.True;
		bool flag = logic_uScript_IsBlockAnchored_uScript_IsBlockAnchored_41.False;
		if (num)
		{
			Relay_True_165();
		}
		if (flag)
		{
			Relay_In_46();
		}
	}

	private void Relay_In_45()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_45 = MessageTag;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_45.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_45, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_45);
	}

	private void Relay_In_46()
	{
		logic_uScript_IsPlayerInRangeOfBlock_block_46 = local_HeartBlock_TankBlock;
		logic_uScript_IsPlayerInRangeOfBlock_uScript_IsPlayerInRangeOfBlock_46.In(logic_uScript_IsPlayerInRangeOfBlock_block_46, logic_uScript_IsPlayerInRangeOfBlock_range_46);
		bool inRange = logic_uScript_IsPlayerInRangeOfBlock_uScript_IsPlayerInRangeOfBlock_46.InRange;
		bool outOfRange = logic_uScript_IsPlayerInRangeOfBlock_uScript_IsPlayerInRangeOfBlock_46.OutOfRange;
		if (inRange)
		{
			Relay_In_130();
		}
		if (outOfRange)
		{
			Relay_In_45();
		}
	}

	private void Relay_Out_47()
	{
	}

	private void Relay_In_47()
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_47 = local_Stage_System_Int32;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_47.In(ref logic_SubGraph_CompleteObjectiveStage_objectiveStage_47, logic_SubGraph_CompleteObjectiveStage_isFinalObjective_47);
	}

	private void Relay_In_49()
	{
		logic_uScript_GetPlayerTankWithBlock_tankBlock_49 = local_HeartBlock_TankBlock;
		logic_uScript_GetPlayerTankWithBlock_Return_49 = logic_uScript_GetPlayerTankWithBlock_uScript_GetPlayerTankWithBlock_49.In(logic_uScript_GetPlayerTankWithBlock_block_49, logic_uScript_GetPlayerTankWithBlock_tankBlock_49, logic_uScript_GetPlayerTankWithBlock_useBlockType_49);
		local_HeartTech_Tank = logic_uScript_GetPlayerTankWithBlock_Return_49;
		bool returned = logic_uScript_GetPlayerTankWithBlock_uScript_GetPlayerTankWithBlock_49.Returned;
		bool notReturned = logic_uScript_GetPlayerTankWithBlock_uScript_GetPlayerTankWithBlock_49.NotReturned;
		if (returned)
		{
			Relay_In_53();
		}
		if (notReturned)
		{
			Relay_In_54();
		}
	}

	private void Relay_In_53()
	{
		logic_uScript_SetEncounterTarget_owner_53 = owner_Connection_55;
		logic_uScript_SetEncounterTarget_visibleObject_53 = local_HeartTech_Tank;
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_53.In(logic_uScript_SetEncounterTarget_owner_53, logic_uScript_SetEncounterTarget_visibleObject_53);
		if (logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_53.Out)
		{
			Relay_In_164();
		}
	}

	private void Relay_In_54()
	{
		logic_uScript_SetEncounterTarget_owner_54 = owner_Connection_57;
		logic_uScript_SetEncounterTarget_visibleObject_54 = local_HeartBlock_TankBlock;
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_54.In(logic_uScript_SetEncounterTarget_owner_54, logic_uScript_SetEncounterTarget_visibleObject_54);
		if (logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_54.Out)
		{
			Relay_In_157();
		}
	}

	private void Relay_True_60()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_60.True(out logic_uScriptAct_SetBool_Target_60);
		local_EnemiesSpawned_System_Boolean = logic_uScriptAct_SetBool_Target_60;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_60.Out)
		{
			Relay_In_135();
		}
	}

	private void Relay_False_60()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_60.False(out logic_uScriptAct_SetBool_Target_60);
		local_EnemiesSpawned_System_Boolean = logic_uScriptAct_SetBool_Target_60;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_60.Out)
		{
			Relay_In_135();
		}
	}

	private void Relay_InitialSpawn_62()
	{
		int num = 0;
		Array array = enemyTechData;
		if (logic_uScript_SpawnTechsFromData_spawnData_62.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_62, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SpawnTechsFromData_spawnData_62, num, array.Length);
		num += array.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_62 = owner_Connection_58;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_62.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_62, logic_uScript_SpawnTechsFromData_ownerNode_62, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_62);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_62.Out)
		{
			Relay_In_82();
		}
	}

	private void Relay_In_63()
	{
		logic_uScriptCon_CompareBool_Bool_63 = local_EnemiesSpawned_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_63.In(logic_uScriptCon_CompareBool_Bool_63);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_63.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_63.False;
		if (num)
		{
			Relay_In_82();
		}
		if (flag)
		{
			Relay_True_60();
		}
	}

	private void Relay_In_65()
	{
		logic_uScriptCon_CompareBool_Bool_65 = local_NotPoweredMsgShown_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_65.In(logic_uScriptCon_CompareBool_Bool_65);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_65.False)
		{
			Relay_True_66();
		}
	}

	private void Relay_True_66()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_66.True(out logic_uScriptAct_SetBool_Target_66);
		local_NotPoweredMsgShown_System_Boolean = logic_uScriptAct_SetBool_Target_66;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_66.Out)
		{
			Relay_In_144();
		}
	}

	private void Relay_False_66()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_66.False(out logic_uScriptAct_SetBool_Target_66);
		local_NotPoweredMsgShown_System_Boolean = logic_uScriptAct_SetBool_Target_66;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_66.Out)
		{
			Relay_In_144();
		}
	}

	private void Relay_In_68()
	{
		logic_uScript_IsHeartBlockOnline_heartBlock_68 = local_HeartBlock_TankBlock;
		logic_uScript_IsHeartBlockOnline_uScript_IsHeartBlockOnline_68.In(logic_uScript_IsHeartBlockOnline_heartBlock_68);
		if (logic_uScript_IsHeartBlockOnline_uScript_IsHeartBlockOnline_68.True)
		{
			Relay_In_72();
		}
	}

	private void Relay_In_69()
	{
		logic_uScript_SetOneTechAsEncounterTarget_owner_69 = owner_Connection_71;
		int num = 0;
		Array array = local_EnemyTechs_TankArray;
		if (logic_uScript_SetOneTechAsEncounterTarget_techs_69.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SetOneTechAsEncounterTarget_techs_69, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SetOneTechAsEncounterTarget_techs_69, num, array.Length);
		num += array.Length;
		logic_uScript_SetOneTechAsEncounterTarget_Return_69 = logic_uScript_SetOneTechAsEncounterTarget_uScript_SetOneTechAsEncounterTarget_69.In(logic_uScript_SetOneTechAsEncounterTarget_owner_69, logic_uScript_SetOneTechAsEncounterTarget_techs_69);
		if (logic_uScript_SetOneTechAsEncounterTarget_uScript_SetOneTechAsEncounterTarget_69.Out)
		{
			Relay_In_91();
		}
	}

	private void Relay_In_72()
	{
		logic_uScriptCon_CompareBool_Bool_72 = local_EnemiesAliveMsgShown_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_72.In(logic_uScriptCon_CompareBool_Bool_72);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_72.False)
		{
			Relay_True_77();
		}
	}

	private void Relay_True_73()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_73.True(out logic_uScriptAct_SetBool_Target_73);
		local_DefendMsgShown_System_Boolean = logic_uScriptAct_SetBool_Target_73;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_73.Out)
		{
			Relay_In_138();
		}
	}

	private void Relay_False_73()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_73.False(out logic_uScriptAct_SetBool_Target_73);
		local_DefendMsgShown_System_Boolean = logic_uScriptAct_SetBool_Target_73;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_73.Out)
		{
			Relay_In_138();
		}
	}

	private void Relay_True_77()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_77.True(out logic_uScriptAct_SetBool_Target_77);
		local_EnemiesAliveMsgShown_System_Boolean = logic_uScriptAct_SetBool_Target_77;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_77.Out)
		{
			Relay_In_139();
		}
	}

	private void Relay_False_77()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_77.False(out logic_uScriptAct_SetBool_Target_77);
		local_EnemiesAliveMsgShown_System_Boolean = logic_uScriptAct_SetBool_Target_77;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_77.Out)
		{
			Relay_In_139();
		}
	}

	private void Relay_In_80()
	{
		logic_uScript_IsHeartBlockOnline_heartBlock_80 = local_HeartBlock_TankBlock;
		logic_uScript_IsHeartBlockOnline_uScript_IsHeartBlockOnline_80.In(logic_uScript_IsHeartBlockOnline_heartBlock_80);
		bool num = logic_uScript_IsHeartBlockOnline_uScript_IsHeartBlockOnline_80.True;
		bool flag = logic_uScript_IsHeartBlockOnline_uScript_IsHeartBlockOnline_80.False;
		if (num)
		{
			Relay_In_87();
		}
		if (flag)
		{
			Relay_In_65();
		}
	}

	private void Relay_In_81()
	{
		logic_uScriptCon_CompareBool_Bool_81 = local_MissionCompleteMsgShown_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_81.In(logic_uScriptCon_CompareBool_Bool_81);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_81.False)
		{
			Relay_In_149();
		}
	}

	private void Relay_In_82()
	{
		int num = 0;
		Array array = enemyTechData;
		if (logic_uScript_GetAndCheckTechs_techData_82.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_82, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techData_82, num, array.Length);
		num += array.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_82 = owner_Connection_86;
		int num2 = 0;
		Array array2 = local_EnemyTechs_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_82.Length != num2 + array2.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_82, num2 + array2.Length);
		}
		Array.Copy(array2, 0, logic_uScript_GetAndCheckTechs_techs_82, num2, array2.Length);
		num2 += array2.Length;
		logic_uScript_GetAndCheckTechs_Return_82 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_82.In(logic_uScript_GetAndCheckTechs_techData_82, logic_uScript_GetAndCheckTechs_ownerNode_82, ref logic_uScript_GetAndCheckTechs_techs_82);
		local_EnemyTechs_TankArray = logic_uScript_GetAndCheckTechs_techs_82;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_82.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_82.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_82.AllDead;
		if (allAlive)
		{
			Relay_In_69();
		}
		if (someAlive)
		{
			Relay_In_69();
		}
		if (allDead)
		{
			Relay_In_83();
		}
	}

	private void Relay_In_83()
	{
		logic_uScript_IsBlockAnchored_block_83 = local_HeartBlock_TankBlock;
		logic_uScript_IsBlockAnchored_uScript_IsBlockAnchored_83.In(logic_uScript_IsBlockAnchored_block_83);
		bool num = logic_uScript_IsBlockAnchored_uScript_IsBlockAnchored_83.True;
		bool flag = logic_uScript_IsBlockAnchored_uScript_IsBlockAnchored_83.False;
		if (num)
		{
			Relay_In_80();
		}
		if (flag)
		{
			Relay_In_81();
		}
	}

	private void Relay_True_85()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_85.True(out logic_uScriptAct_SetBool_Target_85);
		local_MissionCompleteMsgShown_System_Boolean = logic_uScriptAct_SetBool_Target_85;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_85.Out)
		{
			Relay_In_170();
		}
	}

	private void Relay_False_85()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_85.False(out logic_uScriptAct_SetBool_Target_85);
		local_MissionCompleteMsgShown_System_Boolean = logic_uScriptAct_SetBool_Target_85;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_85.Out)
		{
			Relay_In_170();
		}
	}

	private void Relay_In_87()
	{
		logic_uScriptCon_CompareBool_Bool_87 = local_MissionCompleteMsgShown_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_87.In(logic_uScriptCon_CompareBool_Bool_87);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_87.False)
		{
			Relay_In_145();
		}
	}

	private void Relay_In_91()
	{
		logic_uScriptCon_CompareBool_Bool_91 = local_DefendMsgShown_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_91.In(logic_uScriptCon_CompareBool_Bool_91);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_91.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_91.False;
		if (num)
		{
			Relay_In_68();
		}
		if (flag)
		{
			Relay_True_73();
		}
	}

	private void Relay_True_93()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_93.True(out logic_uScriptAct_SetBool_Target_93);
		local_MissionCompleteMsgShown_System_Boolean = logic_uScriptAct_SetBool_Target_93;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_93.Out)
		{
			Relay_In_170();
		}
	}

	private void Relay_False_93()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_93.False(out logic_uScriptAct_SetBool_Target_93);
		local_MissionCompleteMsgShown_System_Boolean = logic_uScriptAct_SetBool_Target_93;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_93.Out)
		{
			Relay_In_170();
		}
	}

	private void Relay_Out_96()
	{
	}

	private void Relay_In_96()
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_96 = local_Stage_System_Int32;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_96.In(ref logic_SubGraph_CompleteObjectiveStage_objectiveStage_96, logic_SubGraph_CompleteObjectiveStage_isFinalObjective_96);
	}

	private void Relay_Succeed_99()
	{
		logic_uScript_FinishEncounter_owner_99 = owner_Connection_97;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_99.Succeed(logic_uScript_FinishEncounter_owner_99);
	}

	private void Relay_Fail_99()
	{
		logic_uScript_FinishEncounter_owner_99 = owner_Connection_97;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_99.Fail(logic_uScript_FinishEncounter_owner_99);
	}

	private void Relay_In_101()
	{
		logic_uScript_LockTech_tech_101 = local_HeartTech_Tank;
		logic_uScript_LockTech_uScript_LockTech_101.In(logic_uScript_LockTech_tech_101, logic_uScript_LockTech_lockType_101);
		if (logic_uScript_LockTech_uScript_LockTech_101.Out)
		{
			Relay_In_2();
		}
	}

	private void Relay_Save_Out_102()
	{
		Relay_Save_106();
	}

	private void Relay_Load_Out_102()
	{
		Relay_Load_106();
	}

	private void Relay_Restart_Out_102()
	{
		Relay_Set_False_106();
	}

	private void Relay_Save_102()
	{
		logic_SubGraph_SaveLoadBool_boolean_102 = local_HeartBlockFound_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_102 = local_HeartBlockFound_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_102.Save(ref logic_SubGraph_SaveLoadBool_boolean_102, logic_SubGraph_SaveLoadBool_boolAsVariable_102, logic_SubGraph_SaveLoadBool_uniqueID_102);
	}

	private void Relay_Load_102()
	{
		logic_SubGraph_SaveLoadBool_boolean_102 = local_HeartBlockFound_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_102 = local_HeartBlockFound_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_102.Load(ref logic_SubGraph_SaveLoadBool_boolean_102, logic_SubGraph_SaveLoadBool_boolAsVariable_102, logic_SubGraph_SaveLoadBool_uniqueID_102);
	}

	private void Relay_Set_True_102()
	{
		logic_SubGraph_SaveLoadBool_boolean_102 = local_HeartBlockFound_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_102 = local_HeartBlockFound_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_102.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_102, logic_SubGraph_SaveLoadBool_boolAsVariable_102, logic_SubGraph_SaveLoadBool_uniqueID_102);
	}

	private void Relay_Set_False_102()
	{
		logic_SubGraph_SaveLoadBool_boolean_102 = local_HeartBlockFound_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_102 = local_HeartBlockFound_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_102.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_102, logic_SubGraph_SaveLoadBool_boolAsVariable_102, logic_SubGraph_SaveLoadBool_uniqueID_102);
	}

	private void Relay_SaveEvent_104()
	{
		Relay_Save_102();
	}

	private void Relay_LoadEvent_104()
	{
		Relay_Load_102();
	}

	private void Relay_RestartEvent_104()
	{
		Relay_Set_False_102();
	}

	private void Relay_Save_Out_106()
	{
		Relay_Save_167();
	}

	private void Relay_Load_Out_106()
	{
		Relay_Load_167();
	}

	private void Relay_Restart_Out_106()
	{
		Relay_Set_False_167();
	}

	private void Relay_Save_106()
	{
		logic_SubGraph_SaveLoadBool_boolean_106 = local_HeartBlockSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_106 = local_HeartBlockSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_106.Save(ref logic_SubGraph_SaveLoadBool_boolean_106, logic_SubGraph_SaveLoadBool_boolAsVariable_106, logic_SubGraph_SaveLoadBool_uniqueID_106);
	}

	private void Relay_Load_106()
	{
		logic_SubGraph_SaveLoadBool_boolean_106 = local_HeartBlockSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_106 = local_HeartBlockSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_106.Load(ref logic_SubGraph_SaveLoadBool_boolean_106, logic_SubGraph_SaveLoadBool_boolAsVariable_106, logic_SubGraph_SaveLoadBool_uniqueID_106);
	}

	private void Relay_Set_True_106()
	{
		logic_SubGraph_SaveLoadBool_boolean_106 = local_HeartBlockSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_106 = local_HeartBlockSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_106.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_106, logic_SubGraph_SaveLoadBool_boolAsVariable_106, logic_SubGraph_SaveLoadBool_uniqueID_106);
	}

	private void Relay_Set_False_106()
	{
		logic_SubGraph_SaveLoadBool_boolean_106 = local_HeartBlockSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_106 = local_HeartBlockSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_106.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_106, logic_SubGraph_SaveLoadBool_boolAsVariable_106, logic_SubGraph_SaveLoadBool_uniqueID_106);
	}

	private void Relay_Save_Out_108()
	{
		Relay_Save_111();
	}

	private void Relay_Load_Out_108()
	{
		Relay_Load_111();
	}

	private void Relay_Restart_Out_108()
	{
		Relay_Set_False_111();
	}

	private void Relay_Save_108()
	{
		logic_SubGraph_SaveLoadBool_boolean_108 = local_EnemiesSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_108 = local_EnemiesSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_108.Save(ref logic_SubGraph_SaveLoadBool_boolean_108, logic_SubGraph_SaveLoadBool_boolAsVariable_108, logic_SubGraph_SaveLoadBool_uniqueID_108);
	}

	private void Relay_Load_108()
	{
		logic_SubGraph_SaveLoadBool_boolean_108 = local_EnemiesSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_108 = local_EnemiesSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_108.Load(ref logic_SubGraph_SaveLoadBool_boolean_108, logic_SubGraph_SaveLoadBool_boolAsVariable_108, logic_SubGraph_SaveLoadBool_uniqueID_108);
	}

	private void Relay_Set_True_108()
	{
		logic_SubGraph_SaveLoadBool_boolean_108 = local_EnemiesSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_108 = local_EnemiesSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_108.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_108, logic_SubGraph_SaveLoadBool_boolAsVariable_108, logic_SubGraph_SaveLoadBool_uniqueID_108);
	}

	private void Relay_Set_False_108()
	{
		logic_SubGraph_SaveLoadBool_boolean_108 = local_EnemiesSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_108 = local_EnemiesSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_108.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_108, logic_SubGraph_SaveLoadBool_boolAsVariable_108, logic_SubGraph_SaveLoadBool_uniqueID_108);
	}

	private void Relay_Save_Out_111()
	{
		Relay_Save_112();
	}

	private void Relay_Load_Out_111()
	{
		Relay_Load_112();
	}

	private void Relay_Restart_Out_111()
	{
		Relay_Set_False_112();
	}

	private void Relay_Save_111()
	{
		logic_SubGraph_SaveLoadBool_boolean_111 = local_StartMsgShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_111 = local_StartMsgShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_111.Save(ref logic_SubGraph_SaveLoadBool_boolean_111, logic_SubGraph_SaveLoadBool_boolAsVariable_111, logic_SubGraph_SaveLoadBool_uniqueID_111);
	}

	private void Relay_Load_111()
	{
		logic_SubGraph_SaveLoadBool_boolean_111 = local_StartMsgShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_111 = local_StartMsgShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_111.Load(ref logic_SubGraph_SaveLoadBool_boolean_111, logic_SubGraph_SaveLoadBool_boolAsVariable_111, logic_SubGraph_SaveLoadBool_uniqueID_111);
	}

	private void Relay_Set_True_111()
	{
		logic_SubGraph_SaveLoadBool_boolean_111 = local_StartMsgShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_111 = local_StartMsgShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_111.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_111, logic_SubGraph_SaveLoadBool_boolAsVariable_111, logic_SubGraph_SaveLoadBool_uniqueID_111);
	}

	private void Relay_Set_False_111()
	{
		logic_SubGraph_SaveLoadBool_boolean_111 = local_StartMsgShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_111 = local_StartMsgShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_111.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_111, logic_SubGraph_SaveLoadBool_boolAsVariable_111, logic_SubGraph_SaveLoadBool_uniqueID_111);
	}

	private void Relay_Save_Out_112()
	{
		Relay_Save_117();
	}

	private void Relay_Load_Out_112()
	{
		Relay_Load_117();
	}

	private void Relay_Restart_Out_112()
	{
		Relay_Set_False_117();
	}

	private void Relay_Save_112()
	{
		logic_SubGraph_SaveLoadBool_boolean_112 = local_DefendMsgShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_112 = local_DefendMsgShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_112.Save(ref logic_SubGraph_SaveLoadBool_boolean_112, logic_SubGraph_SaveLoadBool_boolAsVariable_112, logic_SubGraph_SaveLoadBool_uniqueID_112);
	}

	private void Relay_Load_112()
	{
		logic_SubGraph_SaveLoadBool_boolean_112 = local_DefendMsgShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_112 = local_DefendMsgShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_112.Load(ref logic_SubGraph_SaveLoadBool_boolean_112, logic_SubGraph_SaveLoadBool_boolAsVariable_112, logic_SubGraph_SaveLoadBool_uniqueID_112);
	}

	private void Relay_Set_True_112()
	{
		logic_SubGraph_SaveLoadBool_boolean_112 = local_DefendMsgShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_112 = local_DefendMsgShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_112.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_112, logic_SubGraph_SaveLoadBool_boolAsVariable_112, logic_SubGraph_SaveLoadBool_uniqueID_112);
	}

	private void Relay_Set_False_112()
	{
		logic_SubGraph_SaveLoadBool_boolean_112 = local_DefendMsgShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_112 = local_DefendMsgShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_112.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_112, logic_SubGraph_SaveLoadBool_boolAsVariable_112, logic_SubGraph_SaveLoadBool_uniqueID_112);
	}

	private void Relay_Save_Out_115()
	{
		Relay_Save_116();
	}

	private void Relay_Load_Out_115()
	{
		Relay_Load_116();
	}

	private void Relay_Restart_Out_115()
	{
		Relay_Set_False_116();
	}

	private void Relay_Save_115()
	{
		logic_SubGraph_SaveLoadBool_boolean_115 = local_NotPoweredMsgShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_115 = local_NotPoweredMsgShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_115.Save(ref logic_SubGraph_SaveLoadBool_boolean_115, logic_SubGraph_SaveLoadBool_boolAsVariable_115, logic_SubGraph_SaveLoadBool_uniqueID_115);
	}

	private void Relay_Load_115()
	{
		logic_SubGraph_SaveLoadBool_boolean_115 = local_NotPoweredMsgShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_115 = local_NotPoweredMsgShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_115.Load(ref logic_SubGraph_SaveLoadBool_boolean_115, logic_SubGraph_SaveLoadBool_boolAsVariable_115, logic_SubGraph_SaveLoadBool_uniqueID_115);
	}

	private void Relay_Set_True_115()
	{
		logic_SubGraph_SaveLoadBool_boolean_115 = local_NotPoweredMsgShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_115 = local_NotPoweredMsgShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_115.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_115, logic_SubGraph_SaveLoadBool_boolAsVariable_115, logic_SubGraph_SaveLoadBool_uniqueID_115);
	}

	private void Relay_Set_False_115()
	{
		logic_SubGraph_SaveLoadBool_boolean_115 = local_NotPoweredMsgShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_115 = local_NotPoweredMsgShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_115.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_115, logic_SubGraph_SaveLoadBool_boolAsVariable_115, logic_SubGraph_SaveLoadBool_uniqueID_115);
	}

	private void Relay_Save_Out_116()
	{
		Relay_Save_120();
	}

	private void Relay_Load_Out_116()
	{
		Relay_Load_120();
	}

	private void Relay_Restart_Out_116()
	{
		Relay_Restart_120();
	}

	private void Relay_Save_116()
	{
		logic_SubGraph_SaveLoadBool_boolean_116 = local_MissionCompleteMsgShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_116 = local_MissionCompleteMsgShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_116.Save(ref logic_SubGraph_SaveLoadBool_boolean_116, logic_SubGraph_SaveLoadBool_boolAsVariable_116, logic_SubGraph_SaveLoadBool_uniqueID_116);
	}

	private void Relay_Load_116()
	{
		logic_SubGraph_SaveLoadBool_boolean_116 = local_MissionCompleteMsgShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_116 = local_MissionCompleteMsgShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_116.Load(ref logic_SubGraph_SaveLoadBool_boolean_116, logic_SubGraph_SaveLoadBool_boolAsVariable_116, logic_SubGraph_SaveLoadBool_uniqueID_116);
	}

	private void Relay_Set_True_116()
	{
		logic_SubGraph_SaveLoadBool_boolean_116 = local_MissionCompleteMsgShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_116 = local_MissionCompleteMsgShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_116.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_116, logic_SubGraph_SaveLoadBool_boolAsVariable_116, logic_SubGraph_SaveLoadBool_uniqueID_116);
	}

	private void Relay_Set_False_116()
	{
		logic_SubGraph_SaveLoadBool_boolean_116 = local_MissionCompleteMsgShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_116 = local_MissionCompleteMsgShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_116.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_116, logic_SubGraph_SaveLoadBool_boolAsVariable_116, logic_SubGraph_SaveLoadBool_uniqueID_116);
	}

	private void Relay_Save_Out_117()
	{
		Relay_Save_115();
	}

	private void Relay_Load_Out_117()
	{
		Relay_Load_115();
	}

	private void Relay_Restart_Out_117()
	{
		Relay_Set_False_115();
	}

	private void Relay_Save_117()
	{
		logic_SubGraph_SaveLoadBool_boolean_117 = local_EnemiesAliveMsgShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_117 = local_EnemiesAliveMsgShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_117.Save(ref logic_SubGraph_SaveLoadBool_boolean_117, logic_SubGraph_SaveLoadBool_boolAsVariable_117, logic_SubGraph_SaveLoadBool_uniqueID_117);
	}

	private void Relay_Load_117()
	{
		logic_SubGraph_SaveLoadBool_boolean_117 = local_EnemiesAliveMsgShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_117 = local_EnemiesAliveMsgShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_117.Load(ref logic_SubGraph_SaveLoadBool_boolean_117, logic_SubGraph_SaveLoadBool_boolAsVariable_117, logic_SubGraph_SaveLoadBool_uniqueID_117);
	}

	private void Relay_Set_True_117()
	{
		logic_SubGraph_SaveLoadBool_boolean_117 = local_EnemiesAliveMsgShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_117 = local_EnemiesAliveMsgShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_117.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_117, logic_SubGraph_SaveLoadBool_boolAsVariable_117, logic_SubGraph_SaveLoadBool_uniqueID_117);
	}

	private void Relay_Set_False_117()
	{
		logic_SubGraph_SaveLoadBool_boolean_117 = local_EnemiesAliveMsgShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_117 = local_EnemiesAliveMsgShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_117.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_117, logic_SubGraph_SaveLoadBool_boolAsVariable_117, logic_SubGraph_SaveLoadBool_uniqueID_117);
	}

	private void Relay_Save_Out_120()
	{
	}

	private void Relay_Load_Out_120()
	{
		Relay_In_122();
	}

	private void Relay_Restart_Out_120()
	{
	}

	private void Relay_Save_120()
	{
		logic_SubGraph_SaveLoadInt_integer_120 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_120 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_120.Save(logic_SubGraph_SaveLoadInt_restartValue_120, ref logic_SubGraph_SaveLoadInt_integer_120, logic_SubGraph_SaveLoadInt_intAsVariable_120, logic_SubGraph_SaveLoadInt_uniqueID_120);
	}

	private void Relay_Load_120()
	{
		logic_SubGraph_SaveLoadInt_integer_120 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_120 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_120.Load(logic_SubGraph_SaveLoadInt_restartValue_120, ref logic_SubGraph_SaveLoadInt_integer_120, logic_SubGraph_SaveLoadInt_intAsVariable_120, logic_SubGraph_SaveLoadInt_uniqueID_120);
	}

	private void Relay_Restart_120()
	{
		logic_SubGraph_SaveLoadInt_integer_120 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_120 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_120.Restart(logic_SubGraph_SaveLoadInt_restartValue_120, ref logic_SubGraph_SaveLoadInt_integer_120, logic_SubGraph_SaveLoadInt_intAsVariable_120, logic_SubGraph_SaveLoadInt_uniqueID_120);
	}

	private void Relay_Out_122()
	{
	}

	private void Relay_In_122()
	{
		logic_SubGraph_LoadObjectiveStates_currentObjective_122 = local_Stage_System_Int32;
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_122.In(logic_SubGraph_LoadObjectiveStates_currentObjective_122);
	}

	private void Relay_In_129()
	{
		logic_uScript_AddMessage_messageData_129 = msg01FindHeartBlock;
		logic_uScript_AddMessage_speaker_129 = messageSpeaker;
		logic_uScript_AddMessage_Return_129 = logic_uScript_AddMessage_uScript_AddMessage_129.In(logic_uScript_AddMessage_messageData_129, logic_uScript_AddMessage_speaker_129);
		if (logic_uScript_AddMessage_uScript_AddMessage_129.Out)
		{
			Relay_In_20();
		}
	}

	private void Relay_In_130()
	{
		logic_uScript_AddMessage_messageData_130 = msg02AnchorHeartBlock;
		logic_uScript_AddMessage_speaker_130 = messageSpeaker;
		logic_uScript_AddMessage_Return_130 = logic_uScript_AddMessage_uScript_AddMessage_130.In(logic_uScript_AddMessage_messageData_130, logic_uScript_AddMessage_speaker_130);
	}

	private void Relay_In_135()
	{
		logic_uScript_AddMessage_messageData_135 = msg03EnemiesIncoming;
		logic_uScript_AddMessage_speaker_135 = messageSpeaker;
		logic_uScript_AddMessage_Return_135 = logic_uScript_AddMessage_uScript_AddMessage_135.In(logic_uScript_AddMessage_messageData_135, logic_uScript_AddMessage_speaker_135);
		if (logic_uScript_AddMessage_uScript_AddMessage_135.Out)
		{
			Relay_InitialSpawn_62();
		}
	}

	private void Relay_In_138()
	{
		logic_uScript_AddMessage_messageData_138 = msg04DefendHeartBlock;
		logic_uScript_AddMessage_speaker_138 = messageSpeaker;
		logic_uScript_AddMessage_Return_138 = logic_uScript_AddMessage_uScript_AddMessage_138.In(logic_uScript_AddMessage_messageData_138, logic_uScript_AddMessage_speaker_138);
		if (logic_uScript_AddMessage_uScript_AddMessage_138.Out)
		{
			Relay_In_68();
		}
	}

	private void Relay_In_139()
	{
		logic_uScript_AddMessage_messageData_139 = msg05PoweredButEnemiesAlive;
		logic_uScript_AddMessage_speaker_139 = messageSpeaker;
		logic_uScript_AddMessage_Return_139 = logic_uScript_AddMessage_uScript_AddMessage_139.In(logic_uScript_AddMessage_messageData_139, logic_uScript_AddMessage_speaker_139);
	}

	private void Relay_In_144()
	{
		logic_uScript_AddMessage_messageData_144 = msg06EnemiesDeadButNotPowered;
		logic_uScript_AddMessage_speaker_144 = messageSpeaker;
		logic_uScript_AddMessage_Return_144 = logic_uScript_AddMessage_uScript_AddMessage_144.In(logic_uScript_AddMessage_messageData_144, logic_uScript_AddMessage_speaker_144);
	}

	private void Relay_In_145()
	{
		logic_uScript_AddMessage_messageData_145 = msg07MissionComplete;
		logic_uScript_AddMessage_speaker_145 = messageSpeaker;
		logic_uScript_AddMessage_Return_145 = logic_uScript_AddMessage_uScript_AddMessage_145.In(logic_uScript_AddMessage_messageData_145, logic_uScript_AddMessage_speaker_145);
		if (logic_uScript_AddMessage_uScript_AddMessage_145.Shown)
		{
			Relay_True_85();
		}
	}

	private void Relay_In_149()
	{
		logic_uScript_AddMessage_messageData_149 = msg08MissionCompleteNotAnchored;
		logic_uScript_AddMessage_speaker_149 = messageSpeaker;
		logic_uScript_AddMessage_Return_149 = logic_uScript_AddMessage_uScript_AddMessage_149.In(logic_uScript_AddMessage_messageData_149, logic_uScript_AddMessage_speaker_149);
		if (logic_uScript_AddMessage_uScript_AddMessage_149.Shown)
		{
			Relay_True_93();
		}
	}

	private void Relay_In_151()
	{
		logic_uScript_AddMessage_messageData_151 = msg09HeartBlockExplanation;
		logic_uScript_AddMessage_speaker_151 = messageSpeaker;
		logic_uScript_AddMessage_Return_151 = logic_uScript_AddMessage_uScript_AddMessage_151.In(logic_uScript_AddMessage_messageData_151, logic_uScript_AddMessage_speaker_151);
		if (logic_uScript_AddMessage_uScript_AddMessage_151.Out)
		{
			Relay_False_378();
		}
	}

	private void Relay_In_154()
	{
		logic_uScript_AddMessage_messageData_154 = msg10HeartBlockDestroyed;
		logic_uScript_AddMessage_speaker_154 = messageSpeaker;
		logic_uScript_AddMessage_Return_154 = logic_uScript_AddMessage_uScript_AddMessage_154.In(logic_uScript_AddMessage_messageData_154, logic_uScript_AddMessage_speaker_154);
		if (logic_uScript_AddMessage_uScript_AddMessage_154.Out)
		{
			Relay_False_18();
		}
	}

	private void Relay_In_157()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_157.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_157.Out)
		{
			Relay_In_2();
		}
	}

	private void Relay_In_158()
	{
		logic_uScriptCon_CompareInt_A_158 = local_Stage_System_Int32;
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_158.In(logic_uScriptCon_CompareInt_A_158, logic_uScriptCon_CompareInt_B_158);
		bool greaterThan = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_158.GreaterThan;
		bool lessThanOrEqualTo = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_158.LessThanOrEqualTo;
		if (greaterThan)
		{
			Relay_In_49();
		}
		if (lessThanOrEqualTo)
		{
			Relay_In_40();
		}
	}

	private void Relay_In_161()
	{
		logic_uScript_LockVisibleStackAccept_targetObject_161 = local_HeartBlock_TankBlock;
		logic_uScript_LockVisibleStackAccept_uScript_LockVisibleStackAccept_161.In(logic_uScript_LockVisibleStackAccept_targetObject_161);
		if (logic_uScript_LockVisibleStackAccept_uScript_LockVisibleStackAccept_161.Out)
		{
			Relay_In_158();
		}
	}

	private void Relay_In_164()
	{
		logic_uScriptCon_CompareBool_Bool_164 = local_LockHeartBlock_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_164.In(logic_uScriptCon_CompareBool_Bool_164);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_164.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_164.False;
		if (num)
		{
			Relay_In_101();
		}
		if (flag)
		{
			Relay_In_2();
		}
	}

	private void Relay_True_165()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_165.True(out logic_uScriptAct_SetBool_Target_165);
		local_LockHeartBlock_System_Boolean = logic_uScriptAct_SetBool_Target_165;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_165.Out)
		{
			Relay_In_47();
		}
	}

	private void Relay_False_165()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_165.False(out logic_uScriptAct_SetBool_Target_165);
		local_LockHeartBlock_System_Boolean = logic_uScriptAct_SetBool_Target_165;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_165.Out)
		{
			Relay_In_47();
		}
	}

	private void Relay_Save_Out_167()
	{
		Relay_Save_108();
	}

	private void Relay_Load_Out_167()
	{
		Relay_Load_108();
	}

	private void Relay_Restart_Out_167()
	{
		Relay_Set_False_108();
	}

	private void Relay_Save_167()
	{
		logic_SubGraph_SaveLoadBool_boolean_167 = local_LockHeartBlock_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_167 = local_LockHeartBlock_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_167.Save(ref logic_SubGraph_SaveLoadBool_boolean_167, logic_SubGraph_SaveLoadBool_boolAsVariable_167, logic_SubGraph_SaveLoadBool_uniqueID_167);
	}

	private void Relay_Load_167()
	{
		logic_SubGraph_SaveLoadBool_boolean_167 = local_LockHeartBlock_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_167 = local_LockHeartBlock_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_167.Load(ref logic_SubGraph_SaveLoadBool_boolean_167, logic_SubGraph_SaveLoadBool_boolAsVariable_167, logic_SubGraph_SaveLoadBool_uniqueID_167);
	}

	private void Relay_Set_True_167()
	{
		logic_SubGraph_SaveLoadBool_boolean_167 = local_LockHeartBlock_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_167 = local_LockHeartBlock_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_167.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_167, logic_SubGraph_SaveLoadBool_boolAsVariable_167, logic_SubGraph_SaveLoadBool_uniqueID_167);
	}

	private void Relay_Set_False_167()
	{
		logic_SubGraph_SaveLoadBool_boolean_167 = local_LockHeartBlock_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_167 = local_LockHeartBlock_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_167.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_167, logic_SubGraph_SaveLoadBool_boolAsVariable_167, logic_SubGraph_SaveLoadBool_uniqueID_167);
	}

	private void Relay_In_169()
	{
		logic_uScript_ShowHint_uScript_ShowHint_169.In(logic_uScript_ShowHint_hintId_169);
		if (logic_uScript_ShowHint_uScript_ShowHint_169.Out)
		{
			Relay_Succeed_99();
		}
	}

	private void Relay_In_170()
	{
		logic_uScript_EnableBlockPalette_uScript_EnableBlockPalette_170.In();
		if (logic_uScript_EnableBlockPalette_uScript_EnableBlockPalette_170.Out)
		{
			Relay_In_96();
		}
	}

	private void Relay_True_378()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_378.True(out logic_uScriptAct_SetBool_Target_378);
		local_LockHeartBlock_System_Boolean = logic_uScriptAct_SetBool_Target_378;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_378.Out)
		{
			Relay_In_169();
		}
	}

	private void Relay_False_378()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_378.False(out logic_uScriptAct_SetBool_Target_378);
		local_LockHeartBlock_System_Boolean = logic_uScriptAct_SetBool_Target_378;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_378.Out)
		{
			Relay_In_169();
		}
	}

	private void Relay_True_383()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_383.True(out logic_uScriptAct_SetBool_Target_383);
		local_LockHeartBlock_System_Boolean = logic_uScriptAct_SetBool_Target_383;
	}

	private void Relay_False_383()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_383.False(out logic_uScriptAct_SetBool_Target_383);
		local_LockHeartBlock_System_Boolean = logic_uScriptAct_SetBool_Target_383;
	}

	private void Relay_True_386()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_386.True(out logic_uScriptAct_SetBool_Target_386);
		local_LockHeartBlock_System_Boolean = logic_uScriptAct_SetBool_Target_386;
	}

	private void Relay_False_386()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_386.False(out logic_uScriptAct_SetBool_Target_386);
		local_LockHeartBlock_System_Boolean = logic_uScriptAct_SetBool_Target_386;
	}
}
