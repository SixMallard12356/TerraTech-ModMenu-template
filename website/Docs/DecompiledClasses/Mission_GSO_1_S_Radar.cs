using System;
using UnityEngine;

[Serializable]
[FriendlyName("GSO 1-S3 Radar", "")]
[NodePath("Graphs")]
public class Mission_GSO_1_S_Radar : uScriptLogic
{
	private GameObject parentGameObject;

	private uScript_GUI thisScriptsOnGuiListener;

	private bool m_RegisteredForEvents;

	private GameHints.HintID local_161_GameHints_HintID = GameHints.HintID.Minimap;

	private GameHints.HintID local_163_GameHints_HintID = GameHints.HintID.Radar;

	private Tank[] local_71_TankArray = new Tank[0];

	private bool local_AI_Set_System_Boolean;

	private string local_BlockRadar_System_String = "Radar";

	private Vector3 local_EncounterPos_UnityEngine_Vector3 = new Vector3(0f, 0f, 0f);

	private bool local_Enemy_Defeated_System_Boolean;

	private bool local_EnemySpawned_System_Boolean;

	private Vector3 local_InitialPos_UnityEngine_Vector3 = new Vector3(0f, 0f, 0f);

	private bool local_MsgThiefSpottedShown_System_Boolean;

	private bool local_PositionCached_System_Boolean;

	private bool local_QuestLogShown_System_Boolean;

	private bool local_RadarAttached_System_Boolean;

	private TankBlock local_RadarBlock_TankBlock;

	private BlockTypes local_RadarBlockType_BlockTypes = BlockTypes.GSORadar_111;

	private int local_Stage_System_Int32 = 1;

	private Tank local_ThiefEnemy_Tank;

	public uScript_AddMessage.MessageSpeaker messageSpeaker;

	[Multiline(3)]
	public string messageTag = "";

	public uScript_AddMessage.MessageData msg01EnemyFound;

	public uScript_AddMessage.MessageData msg02PickUpAttachRadar;

	public uScript_AddMessage.MessageData msg04RadarAttached;

	public uScript_AddMessage.MessageData msg05RadarDestroyed;

	public Transform Particles;

	[Multiline(3)]
	public string PositionId = "";

	public SpawnTechData SpawnDataThief;

	private GameObject owner_Connection_2;

	private GameObject owner_Connection_11;

	private GameObject owner_Connection_14;

	private GameObject owner_Connection_28;

	private GameObject owner_Connection_38;

	private GameObject owner_Connection_43;

	private GameObject owner_Connection_47;

	private GameObject owner_Connection_52;

	private GameObject owner_Connection_58;

	private GameObject owner_Connection_60;

	private GameObject owner_Connection_62;

	private GameObject owner_Connection_70;

	private GameObject owner_Connection_78;

	private GameObject owner_Connection_85;

	private GameObject owner_Connection_89;

	private GameObject owner_Connection_110;

	private GameObject owner_Connection_113;

	private GameObject owner_Connection_127;

	private GameObject owner_Connection_129;

	private uScript_FireParticlesTowardsGround logic_uScript_FireParticlesTowardsGround_uScript_FireParticlesTowardsGround_0 = new uScript_FireParticlesTowardsGround();

	private Vector3 logic_uScript_FireParticlesTowardsGround_groundPos_0;

	private Transform logic_uScript_FireParticlesTowardsGround_particleEffect_0;

	private GameObject logic_uScript_FireParticlesTowardsGround_owner_0;

	private string logic_uScript_FireParticlesTowardsGround_uniqueName_0 = "fireRain";

	private Transform logic_uScript_FireParticlesTowardsGround_Return_0;

	private bool logic_uScript_FireParticlesTowardsGround_Delivered_0 = true;

	private bool logic_uScript_FireParticlesTowardsGround_Out_0 = true;

	private uScriptCon_ManualSwitch logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_3 = new uScriptCon_ManualSwitch();

	private int logic_uScriptCon_ManualSwitch_CurrentOutput_3;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_5 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_5;

	private bool logic_uScriptAct_SetBool_Out_5 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_5 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_5 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_6 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_6;

	private bool logic_uScriptCon_CompareBool_True_6 = true;

	private bool logic_uScriptCon_CompareBool_False_6 = true;

	private uScript_GetTankBlock logic_uScript_GetTankBlock_uScript_GetTankBlock_9 = new uScript_GetTankBlock();

	private Tank logic_uScript_GetTankBlock_tank_9;

	private BlockTypes logic_uScript_GetTankBlock_blockType_9;

	private TankBlock logic_uScript_GetTankBlock_Return_9;

	private bool logic_uScript_GetTankBlock_Out_9 = true;

	private bool logic_uScript_GetTankBlock_Returned_9 = true;

	private bool logic_uScript_GetTankBlock_NotFound_9 = true;

	private uScript_SaveNamedBlock logic_uScript_SaveNamedBlock_uScript_SaveNamedBlock_10 = new uScript_SaveNamedBlock();

	private TankBlock logic_uScript_SaveNamedBlock_block_10;

	private string logic_uScript_SaveNamedBlock_uniqueName_10 = "";

	private GameObject logic_uScript_SaveNamedBlock_owner_10;

	private bool logic_uScript_SaveNamedBlock_Out_10 = true;

	private uScript_GetNamedBlock logic_uScript_GetNamedBlock_uScript_GetNamedBlock_12 = new uScript_GetNamedBlock();

	private string logic_uScript_GetNamedBlock_name_12 = "";

	private GameObject logic_uScript_GetNamedBlock_owner_12;

	private TankBlock logic_uScript_GetNamedBlock_Return_12;

	private bool logic_uScript_GetNamedBlock_Out_12 = true;

	private bool logic_uScript_GetNamedBlock_Destroyed_12 = true;

	private bool logic_uScript_GetNamedBlock_BlockExists_12 = true;

	private bool logic_uScript_GetNamedBlock_WaitingForBlock_12 = true;

	private bool logic_uScript_GetNamedBlock_NoBlock_12 = true;

	private uScript_AI_SetPOI logic_uScript_AI_SetPOI_uScript_AI_SetPOI_17 = new uScript_AI_SetPOI();

	private Tank logic_uScript_AI_SetPOI_tank_17;

	private bool logic_uScript_AI_SetPOI_usePOI_17 = true;

	private Vector3 logic_uScript_AI_SetPOI_position_17;

	private float logic_uScript_AI_SetPOI_distance_17 = 250f;

	private bool logic_uScript_AI_SetPOI_Out_17 = true;

	private uScript_KeepBlockInvulnerable logic_uScript_KeepBlockInvulnerable_uScript_KeepBlockInvulnerable_20 = new uScript_KeepBlockInvulnerable();

	private TankBlock logic_uScript_KeepBlockInvulnerable_block_20;

	private bool logic_uScript_KeepBlockInvulnerable_Out_20 = true;

	private uScript_DoesTankHave logic_uScript_DoesTankHave_uScript_DoesTankHave_26 = new uScript_DoesTankHave();

	private Tank logic_uScript_DoesTankHave_tank_26;

	private int logic_uScript_DoesTankHave_amountOfPieces_26 = 1;

	private BlockTypes logic_uScript_DoesTankHave_block_26;

	private bool logic_uScript_DoesTankHave_checkForAmountOnly_26;

	private bool logic_uScript_DoesTankHave_Out_26 = true;

	private bool logic_uScript_DoesTankHave_True_26 = true;

	private bool logic_uScript_DoesTankHave_False_26 = true;

	private uScript_GetPositionInEncounter logic_uScript_GetPositionInEncounter_uScript_GetPositionInEncounter_32 = new uScript_GetPositionInEncounter();

	private GameObject logic_uScript_GetPositionInEncounter_ownerNode_32;

	private string logic_uScript_GetPositionInEncounter_posName_32 = "";

	private Vector3 logic_uScript_GetPositionInEncounter_Return_32;

	private bool logic_uScript_GetPositionInEncounter_Out_32 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_34 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_34;

	private bool logic_uScriptAct_SetBool_Out_34 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_34 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_34 = true;

	private uScriptAct_SetVector3 logic_uScriptAct_SetVector3_uScriptAct_SetVector3_35 = new uScriptAct_SetVector3();

	private Vector3 logic_uScriptAct_SetVector3_Value_35;

	private Vector3 logic_uScriptAct_SetVector3_TargetVector3_35;

	private bool logic_uScriptAct_SetVector3_Out_35 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_36 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_36;

	private bool logic_uScriptCon_CompareBool_True_36 = true;

	private bool logic_uScriptCon_CompareBool_False_36 = true;

	private SubGraph_BaseBomb_ShowQuestLog logic_SubGraph_BaseBomb_ShowQuestLog_SubGraph_BaseBomb_ShowQuestLog_44 = new SubGraph_BaseBomb_ShowQuestLog();

	private bool logic_SubGraph_BaseBomb_ShowQuestLog_Flag_QuestLogShown_44;

	private uScript_PlayerInRangeOfCurrentEncounter logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_46 = new uScript_PlayerInRangeOfCurrentEncounter();

	private GameObject logic_uScript_PlayerInRangeOfCurrentEncounter_ownerNode_46;

	private bool logic_uScript_PlayerInRangeOfCurrentEncounter_Out_46 = true;

	private bool logic_uScript_PlayerInRangeOfCurrentEncounter_True_46 = true;

	private bool logic_uScript_PlayerInRangeOfCurrentEncounter_False_46 = true;

	private uScript_MoveEncounterWithVisible logic_uScript_MoveEncounterWithVisible_uScript_MoveEncounterWithVisible_50 = new uScript_MoveEncounterWithVisible();

	private GameObject logic_uScript_MoveEncounterWithVisible_ownerNode_50;

	private object logic_uScript_MoveEncounterWithVisible_visibleObject_50 = "";

	private bool logic_uScript_MoveEncounterWithVisible_Out_50 = true;

	private uScript_MoveEncounterWithVisible logic_uScript_MoveEncounterWithVisible_uScript_MoveEncounterWithVisible_51 = new uScript_MoveEncounterWithVisible();

	private GameObject logic_uScript_MoveEncounterWithVisible_ownerNode_51;

	private object logic_uScript_MoveEncounterWithVisible_visibleObject_51 = "";

	private bool logic_uScript_MoveEncounterWithVisible_Out_51 = true;

	private uScript_SetEncounterTarget logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_56 = new uScript_SetEncounterTarget();

	private GameObject logic_uScript_SetEncounterTarget_owner_56;

	private object logic_uScript_SetEncounterTarget_visibleObject_56 = "";

	private bool logic_uScript_SetEncounterTarget_Out_56 = true;

	private uScript_SetEncounterTarget logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_59 = new uScript_SetEncounterTarget();

	private GameObject logic_uScript_SetEncounterTarget_owner_59;

	private object logic_uScript_SetEncounterTarget_visibleObject_59 = "";

	private bool logic_uScript_SetEncounterTarget_Out_59 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_64 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_64 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_64;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_64 = 0.3f;

	private bool logic_uScript_SpawnTechsFromData_Out_64 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_65 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_65 = new Tank[0];

	private int logic_uScript_AccessListTech_index_65;

	private Tank logic_uScript_AccessListTech_value_65;

	private bool logic_uScript_AccessListTech_Out_65 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_66 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_66;

	private bool logic_uScriptCon_CompareBool_True_66 = true;

	private bool logic_uScriptCon_CompareBool_False_66 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_68 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_68 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_68;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_68 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_68;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_68 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_68 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_68 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_68 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_73 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_73;

	private bool logic_uScriptAct_SetBool_Out_73 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_73 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_73 = true;

	private uScript_SetTankInvulnerable logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_75 = new uScript_SetTankInvulnerable();

	private bool logic_uScript_SetTankInvulnerable_invulnerable_75;

	private Tank logic_uScript_SetTankInvulnerable_tank_75;

	private bool logic_uScript_SetTankInvulnerable_Out_75 = true;

	private uScript_SpawnBlockAbovePlayer logic_uScript_SpawnBlockAbovePlayer_uScript_SpawnBlockAbovePlayer_77 = new uScript_SpawnBlockAbovePlayer();

	private BlockTypes logic_uScript_SpawnBlockAbovePlayer_blockType_77;

	private string logic_uScript_SpawnBlockAbovePlayer_uniqueName_77 = "";

	private GameObject logic_uScript_SpawnBlockAbovePlayer_owner_77;

	private TankBlock logic_uScript_SpawnBlockAbovePlayer_Return_77;

	private bool logic_uScript_SpawnBlockAbovePlayer_Out_77 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_79 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_79 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_79 = true;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_79 = true;

	private uScript_PlayerInRangeOfCurrentEncounter logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_84 = new uScript_PlayerInRangeOfCurrentEncounter();

	private GameObject logic_uScript_PlayerInRangeOfCurrentEncounter_ownerNode_84;

	private bool logic_uScript_PlayerInRangeOfCurrentEncounter_Out_84 = true;

	private bool logic_uScript_PlayerInRangeOfCurrentEncounter_True_84 = true;

	private bool logic_uScript_PlayerInRangeOfCurrentEncounter_False_84 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_86 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_86 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_86 = true;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_86 = true;

	private SubGraph_SaveLoadInt logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_87 = new SubGraph_SaveLoadInt();

	private int logic_SubGraph_SaveLoadInt_restartValue_87 = 1;

	private int logic_SubGraph_SaveLoadInt_integer_87;

	private object logic_SubGraph_SaveLoadInt_intAsVariable_87 = "";

	private string logic_SubGraph_SaveLoadInt_uniqueID_87 = "Stage";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_91 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_91;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_91 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_91 = "AI Set";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_92 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_92;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_92 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_92 = "Enemy Defeated";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_93 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_93;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_93 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_93 = "PositionCached";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_94 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_94;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_94 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_94 = "AddedQuestLog";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_95 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_95;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_95 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_95 = "EnemySpawned";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_96 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_96;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_96 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_96 = "MsgThiefSpottedShown";

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_103 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_103;

	private bool logic_uScriptCon_CompareBool_True_103 = true;

	private bool logic_uScriptCon_CompareBool_False_103 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_104 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_104;

	private bool logic_uScriptAct_SetBool_Out_104 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_104 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_104 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_106 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_106 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_107 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_107 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_108 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_108 = true;

	private uScript_FinishEncounter logic_uScript_FinishEncounter_uScript_FinishEncounter_109 = new uScript_FinishEncounter();

	private GameObject logic_uScript_FinishEncounter_owner_109;

	private bool logic_uScript_FinishEncounter_Out_109 = true;

	private uScript_FinishEncounter logic_uScript_FinishEncounter_uScript_FinishEncounter_112 = new uScript_FinishEncounter();

	private GameObject logic_uScript_FinishEncounter_owner_112;

	private bool logic_uScript_FinishEncounter_Out_112 = true;

	private uScript_DoesPlayerHaveBlock logic_uScript_DoesPlayerHaveBlock_uScript_DoesPlayerHaveBlock_116 = new uScript_DoesPlayerHaveBlock();

	private BlockTypes logic_uScript_DoesPlayerHaveBlock_block_116;

	private bool logic_uScript_DoesPlayerHaveBlock_isDragging_116;

	private bool logic_uScript_DoesPlayerHaveBlock_Out_116 = true;

	private bool logic_uScript_DoesPlayerHaveBlock_True_116 = true;

	private bool logic_uScript_DoesPlayerHaveBlock_False_116 = true;

	private uScript_SetTankInvulnerable logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_117 = new uScript_SetTankInvulnerable();

	private bool logic_uScript_SetTankInvulnerable_invulnerable_117;

	private Tank logic_uScript_SetTankInvulnerable_tank_117;

	private bool logic_uScript_SetTankInvulnerable_Out_117 = true;

	private SubGraph_LoadObjectiveStates logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_120 = new SubGraph_LoadObjectiveStates();

	private int logic_SubGraph_LoadObjectiveStates_currentObjective_120;

	private uScript_SetTankInvulnerable logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_123 = new uScript_SetTankInvulnerable();

	private bool logic_uScript_SetTankInvulnerable_invulnerable_123;

	private Tank logic_uScript_SetTankInvulnerable_tank_123;

	private bool logic_uScript_SetTankInvulnerable_Out_123 = true;

	private uScript_MoveEncounterWithVisible logic_uScript_MoveEncounterWithVisible_uScript_MoveEncounterWithVisible_124 = new uScript_MoveEncounterWithVisible();

	private GameObject logic_uScript_MoveEncounterWithVisible_ownerNode_124;

	private object logic_uScript_MoveEncounterWithVisible_visibleObject_124 = "";

	private bool logic_uScript_MoveEncounterWithVisible_Out_124 = true;

	private uScript_SetEncounterTarget logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_126 = new uScript_SetEncounterTarget();

	private GameObject logic_uScript_SetEncounterTarget_owner_126;

	private object logic_uScript_SetEncounterTarget_visibleObject_126 = "";

	private bool logic_uScript_SetEncounterTarget_Out_126 = true;

	private uScript_SetTankInvulnerable logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_131 = new uScript_SetTankInvulnerable();

	private bool logic_uScript_SetTankInvulnerable_invulnerable_131 = true;

	private Tank logic_uScript_SetTankInvulnerable_tank_131;

	private bool logic_uScript_SetTankInvulnerable_Out_131 = true;

	private SubGraph_CompleteObjectiveStage logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_134 = new SubGraph_CompleteObjectiveStage();

	private int logic_SubGraph_CompleteObjectiveStage_objectiveStage_134;

	private bool logic_SubGraph_CompleteObjectiveStage_isFinalObjective_134;

	private SubGraph_CompleteObjectiveStage logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_136 = new SubGraph_CompleteObjectiveStage();

	private int logic_SubGraph_CompleteObjectiveStage_objectiveStage_136;

	private bool logic_SubGraph_CompleteObjectiveStage_isFinalObjective_136;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_138 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_138;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_138;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_138;

	private bool logic_uScript_AddMessage_Out_138 = true;

	private bool logic_uScript_AddMessage_Shown_138 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_140 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_140;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_140;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_140;

	private bool logic_uScript_AddMessage_Out_140 = true;

	private bool logic_uScript_AddMessage_Shown_140 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_143 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_143;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_143;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_143;

	private bool logic_uScript_AddMessage_Out_143 = true;

	private bool logic_uScript_AddMessage_Shown_143 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_147 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_147;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_147;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_147;

	private bool logic_uScript_AddMessage_Out_147 = true;

	private bool logic_uScript_AddMessage_Shown_147 = true;

	private uScript_PointArrowAtBlock logic_uScript_PointArrowAtBlock_uScript_PointArrowAtBlock_150 = new uScript_PointArrowAtBlock();

	private TankBlock logic_uScript_PointArrowAtBlock_block_150;

	private float logic_uScript_PointArrowAtBlock_timeToShowFor_150 = 0.1f;

	private Vector3 logic_uScript_PointArrowAtBlock_offset_150 = new Vector3(0f, 0f, 0f);

	private bool logic_uScript_PointArrowAtBlock_Out_150 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_153 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_153;

	private bool logic_uScriptCon_CompareBool_True_153 = true;

	private bool logic_uScriptCon_CompareBool_False_153 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_154 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_154;

	private bool logic_uScriptAct_SetBool_Out_154 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_154 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_154 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_157 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_157;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_157 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_157 = "RadarAttached";

	private uScript_ShowHint logic_uScript_ShowHint_uScript_ShowHint_158 = new uScript_ShowHint();

	private GameHints.HintID logic_uScript_ShowHint_hintId_158;

	private bool logic_uScript_ShowHint_Out_158 = true;

	private uScript_ShowHint logic_uScript_ShowHint_uScript_ShowHint_159 = new uScript_ShowHint();

	private GameHints.HintID logic_uScript_ShowHint_hintId_159;

	private bool logic_uScript_ShowHint_Out_159 = true;

	private uScript_HasHintBeenShownBefore logic_uScript_HasHintBeenShownBefore_uScript_HasHintBeenShownBefore_160 = new uScript_HasHintBeenShownBefore();

	private GameHints.HintID logic_uScript_HasHintBeenShownBefore_hintID_160;

	private bool logic_uScript_HasHintBeenShownBefore_Shown_160 = true;

	private bool logic_uScript_HasHintBeenShownBefore_NotShown_160 = true;

	private uScript_HasHintBeenShownBefore logic_uScript_HasHintBeenShownBefore_uScript_HasHintBeenShownBefore_162 = new uScript_HasHintBeenShownBefore();

	private GameHints.HintID logic_uScript_HasHintBeenShownBefore_hintID_162;

	private bool logic_uScript_HasHintBeenShownBefore_Shown_162 = true;

	private bool logic_uScript_HasHintBeenShownBefore_NotShown_162 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_166 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_166 = "";

	private bool logic_uScript_EnableGlow_enable_166 = true;

	private bool logic_uScript_EnableGlow_Out_166 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_168 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_168 = "";

	private bool logic_uScript_EnableGlow_enable_168;

	private bool logic_uScript_EnableGlow_Out_168 = true;

	private uScript_IsCoreEncounterCompleted logic_uScript_IsCoreEncounterCompleted_uScript_IsCoreEncounterCompleted_365 = new uScript_IsCoreEncounterCompleted();

	private FactionSubTypes logic_uScript_IsCoreEncounterCompleted_corp_365 = FactionSubTypes.GSO;

	private int logic_uScript_IsCoreEncounterCompleted_grade_365 = 1;

	private string logic_uScript_IsCoreEncounterCompleted_encounterName_365 = "1-S Battery";

	private bool logic_uScript_IsCoreEncounterCompleted_True_365 = true;

	private bool logic_uScript_IsCoreEncounterCompleted_False_365 = true;

	private void SyncUnityHooks()
	{
		SyncEventListeners();
		if (null == owner_Connection_2 || !m_RegisteredForEvents)
		{
			owner_Connection_2 = parentGameObject;
		}
		if (null == owner_Connection_11 || !m_RegisteredForEvents)
		{
			owner_Connection_11 = parentGameObject;
		}
		if (null == owner_Connection_14 || !m_RegisteredForEvents)
		{
			owner_Connection_14 = parentGameObject;
		}
		if (null == owner_Connection_28 || !m_RegisteredForEvents)
		{
			owner_Connection_28 = parentGameObject;
		}
		if (null == owner_Connection_38 || !m_RegisteredForEvents)
		{
			owner_Connection_38 = parentGameObject;
		}
		if (null == owner_Connection_43 || !m_RegisteredForEvents)
		{
			owner_Connection_43 = parentGameObject;
		}
		if (null == owner_Connection_47 || !m_RegisteredForEvents)
		{
			owner_Connection_47 = parentGameObject;
		}
		if (null == owner_Connection_52 || !m_RegisteredForEvents)
		{
			owner_Connection_52 = parentGameObject;
		}
		if (null == owner_Connection_58 || !m_RegisteredForEvents)
		{
			owner_Connection_58 = parentGameObject;
		}
		if (null == owner_Connection_60 || !m_RegisteredForEvents)
		{
			owner_Connection_60 = parentGameObject;
		}
		if (null == owner_Connection_62 || !m_RegisteredForEvents)
		{
			owner_Connection_62 = parentGameObject;
			if (null != owner_Connection_62)
			{
				uScript_EncounterUpdate uScript_EncounterUpdate2 = owner_Connection_62.GetComponent<uScript_EncounterUpdate>();
				if (null == uScript_EncounterUpdate2)
				{
					uScript_EncounterUpdate2 = owner_Connection_62.AddComponent<uScript_EncounterUpdate>();
				}
				if (null != uScript_EncounterUpdate2)
				{
					uScript_EncounterUpdate2.OnUpdate += Instance_OnUpdate_63;
					uScript_EncounterUpdate2.OnSuspend += Instance_OnSuspend_63;
					uScript_EncounterUpdate2.OnResume += Instance_OnResume_63;
				}
			}
		}
		if (null == owner_Connection_70 || !m_RegisteredForEvents)
		{
			owner_Connection_70 = parentGameObject;
		}
		if (null == owner_Connection_78 || !m_RegisteredForEvents)
		{
			owner_Connection_78 = parentGameObject;
		}
		if (null == owner_Connection_85 || !m_RegisteredForEvents)
		{
			owner_Connection_85 = parentGameObject;
		}
		if (null == owner_Connection_89 || !m_RegisteredForEvents)
		{
			owner_Connection_89 = parentGameObject;
			if (null != owner_Connection_89)
			{
				uScript_SaveLoad uScript_SaveLoad2 = owner_Connection_89.GetComponent<uScript_SaveLoad>();
				if (null == uScript_SaveLoad2)
				{
					uScript_SaveLoad2 = owner_Connection_89.AddComponent<uScript_SaveLoad>();
				}
				if (null != uScript_SaveLoad2)
				{
					uScript_SaveLoad2.SaveEvent += Instance_SaveEvent_88;
					uScript_SaveLoad2.LoadEvent += Instance_LoadEvent_88;
					uScript_SaveLoad2.RestartEvent += Instance_RestartEvent_88;
				}
			}
		}
		if (null == owner_Connection_110 || !m_RegisteredForEvents)
		{
			owner_Connection_110 = parentGameObject;
		}
		if (null == owner_Connection_113 || !m_RegisteredForEvents)
		{
			owner_Connection_113 = parentGameObject;
		}
		if (null == owner_Connection_127 || !m_RegisteredForEvents)
		{
			owner_Connection_127 = parentGameObject;
		}
		if (null == owner_Connection_129 || !m_RegisteredForEvents)
		{
			owner_Connection_129 = parentGameObject;
		}
	}

	private void RegisterForUnityHooks()
	{
		SyncEventListeners();
		if (!m_RegisteredForEvents && null != owner_Connection_62)
		{
			uScript_EncounterUpdate uScript_EncounterUpdate2 = owner_Connection_62.GetComponent<uScript_EncounterUpdate>();
			if (null == uScript_EncounterUpdate2)
			{
				uScript_EncounterUpdate2 = owner_Connection_62.AddComponent<uScript_EncounterUpdate>();
			}
			if (null != uScript_EncounterUpdate2)
			{
				uScript_EncounterUpdate2.OnUpdate += Instance_OnUpdate_63;
				uScript_EncounterUpdate2.OnSuspend += Instance_OnSuspend_63;
				uScript_EncounterUpdate2.OnResume += Instance_OnResume_63;
			}
		}
		if (!m_RegisteredForEvents && null != owner_Connection_89)
		{
			uScript_SaveLoad uScript_SaveLoad2 = owner_Connection_89.GetComponent<uScript_SaveLoad>();
			if (null == uScript_SaveLoad2)
			{
				uScript_SaveLoad2 = owner_Connection_89.AddComponent<uScript_SaveLoad>();
			}
			if (null != uScript_SaveLoad2)
			{
				uScript_SaveLoad2.SaveEvent += Instance_SaveEvent_88;
				uScript_SaveLoad2.LoadEvent += Instance_LoadEvent_88;
				uScript_SaveLoad2.RestartEvent += Instance_RestartEvent_88;
			}
		}
	}

	private void SyncEventListeners()
	{
	}

	private void UnregisterEventListeners()
	{
		if (null != owner_Connection_62)
		{
			uScript_EncounterUpdate component = owner_Connection_62.GetComponent<uScript_EncounterUpdate>();
			if (null != component)
			{
				component.OnUpdate -= Instance_OnUpdate_63;
				component.OnSuspend -= Instance_OnSuspend_63;
				component.OnResume -= Instance_OnResume_63;
			}
		}
		if (null != owner_Connection_89)
		{
			uScript_SaveLoad component2 = owner_Connection_89.GetComponent<uScript_SaveLoad>();
			if (null != component2)
			{
				component2.SaveEvent -= Instance_SaveEvent_88;
				component2.LoadEvent -= Instance_LoadEvent_88;
				component2.RestartEvent -= Instance_RestartEvent_88;
			}
		}
	}

	public override void SetParent(GameObject g)
	{
		parentGameObject = g;
		logic_uScript_FireParticlesTowardsGround_uScript_FireParticlesTowardsGround_0.SetParent(g);
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_3.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_5.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_6.SetParent(g);
		logic_uScript_GetTankBlock_uScript_GetTankBlock_9.SetParent(g);
		logic_uScript_SaveNamedBlock_uScript_SaveNamedBlock_10.SetParent(g);
		logic_uScript_GetNamedBlock_uScript_GetNamedBlock_12.SetParent(g);
		logic_uScript_AI_SetPOI_uScript_AI_SetPOI_17.SetParent(g);
		logic_uScript_KeepBlockInvulnerable_uScript_KeepBlockInvulnerable_20.SetParent(g);
		logic_uScript_DoesTankHave_uScript_DoesTankHave_26.SetParent(g);
		logic_uScript_GetPositionInEncounter_uScript_GetPositionInEncounter_32.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_34.SetParent(g);
		logic_uScriptAct_SetVector3_uScriptAct_SetVector3_35.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_36.SetParent(g);
		logic_SubGraph_BaseBomb_ShowQuestLog_SubGraph_BaseBomb_ShowQuestLog_44.SetParent(g);
		logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_46.SetParent(g);
		logic_uScript_MoveEncounterWithVisible_uScript_MoveEncounterWithVisible_50.SetParent(g);
		logic_uScript_MoveEncounterWithVisible_uScript_MoveEncounterWithVisible_51.SetParent(g);
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_56.SetParent(g);
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_59.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_64.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_65.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_66.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_68.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_73.SetParent(g);
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_75.SetParent(g);
		logic_uScript_SpawnBlockAbovePlayer_uScript_SpawnBlockAbovePlayer_77.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_79.SetParent(g);
		logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_84.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_86.SetParent(g);
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_87.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_91.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_92.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_93.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_94.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_95.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_96.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_103.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_104.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_106.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_107.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_108.SetParent(g);
		logic_uScript_FinishEncounter_uScript_FinishEncounter_109.SetParent(g);
		logic_uScript_FinishEncounter_uScript_FinishEncounter_112.SetParent(g);
		logic_uScript_DoesPlayerHaveBlock_uScript_DoesPlayerHaveBlock_116.SetParent(g);
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_117.SetParent(g);
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_120.SetParent(g);
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_123.SetParent(g);
		logic_uScript_MoveEncounterWithVisible_uScript_MoveEncounterWithVisible_124.SetParent(g);
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_126.SetParent(g);
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_131.SetParent(g);
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_134.SetParent(g);
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_136.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_138.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_140.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_143.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_147.SetParent(g);
		logic_uScript_PointArrowAtBlock_uScript_PointArrowAtBlock_150.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_153.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_154.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_157.SetParent(g);
		logic_uScript_ShowHint_uScript_ShowHint_158.SetParent(g);
		logic_uScript_ShowHint_uScript_ShowHint_159.SetParent(g);
		logic_uScript_HasHintBeenShownBefore_uScript_HasHintBeenShownBefore_160.SetParent(g);
		logic_uScript_HasHintBeenShownBefore_uScript_HasHintBeenShownBefore_162.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_166.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_168.SetParent(g);
		logic_uScript_IsCoreEncounterCompleted_uScript_IsCoreEncounterCompleted_365.SetParent(g);
		owner_Connection_2 = parentGameObject;
		owner_Connection_11 = parentGameObject;
		owner_Connection_14 = parentGameObject;
		owner_Connection_28 = parentGameObject;
		owner_Connection_38 = parentGameObject;
		owner_Connection_43 = parentGameObject;
		owner_Connection_47 = parentGameObject;
		owner_Connection_52 = parentGameObject;
		owner_Connection_58 = parentGameObject;
		owner_Connection_60 = parentGameObject;
		owner_Connection_62 = parentGameObject;
		owner_Connection_70 = parentGameObject;
		owner_Connection_78 = parentGameObject;
		owner_Connection_85 = parentGameObject;
		owner_Connection_89 = parentGameObject;
		owner_Connection_110 = parentGameObject;
		owner_Connection_113 = parentGameObject;
		owner_Connection_127 = parentGameObject;
		owner_Connection_129 = parentGameObject;
	}

	public void Awake()
	{
		logic_SubGraph_BaseBomb_ShowQuestLog_SubGraph_BaseBomb_ShowQuestLog_44.Awake();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_87.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_91.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_92.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_93.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_94.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_95.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_96.Awake();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_120.Awake();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_134.Awake();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_136.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_157.Awake();
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_3.Output1 += uScriptCon_ManualSwitch_Output1_3;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_3.Output2 += uScriptCon_ManualSwitch_Output2_3;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_3.Output3 += uScriptCon_ManualSwitch_Output3_3;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_3.Output4 += uScriptCon_ManualSwitch_Output4_3;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_3.Output5 += uScriptCon_ManualSwitch_Output5_3;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_3.Output6 += uScriptCon_ManualSwitch_Output6_3;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_3.Output7 += uScriptCon_ManualSwitch_Output7_3;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_3.Output8 += uScriptCon_ManualSwitch_Output8_3;
		logic_SubGraph_BaseBomb_ShowQuestLog_SubGraph_BaseBomb_ShowQuestLog_44.Out += SubGraph_BaseBomb_ShowQuestLog_Out_44;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_87.Save_Out += SubGraph_SaveLoadInt_Save_Out_87;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_87.Load_Out += SubGraph_SaveLoadInt_Load_Out_87;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_87.Restart_Out += SubGraph_SaveLoadInt_Restart_Out_87;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_91.Save_Out += SubGraph_SaveLoadBool_Save_Out_91;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_91.Load_Out += SubGraph_SaveLoadBool_Load_Out_91;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_91.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_91;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_92.Save_Out += SubGraph_SaveLoadBool_Save_Out_92;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_92.Load_Out += SubGraph_SaveLoadBool_Load_Out_92;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_92.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_92;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_93.Save_Out += SubGraph_SaveLoadBool_Save_Out_93;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_93.Load_Out += SubGraph_SaveLoadBool_Load_Out_93;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_93.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_93;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_94.Save_Out += SubGraph_SaveLoadBool_Save_Out_94;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_94.Load_Out += SubGraph_SaveLoadBool_Load_Out_94;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_94.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_94;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_95.Save_Out += SubGraph_SaveLoadBool_Save_Out_95;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_95.Load_Out += SubGraph_SaveLoadBool_Load_Out_95;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_95.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_95;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_96.Save_Out += SubGraph_SaveLoadBool_Save_Out_96;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_96.Load_Out += SubGraph_SaveLoadBool_Load_Out_96;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_96.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_96;
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_120.Out += SubGraph_LoadObjectiveStates_Out_120;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_134.Out += SubGraph_CompleteObjectiveStage_Out_134;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_136.Out += SubGraph_CompleteObjectiveStage_Out_136;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_157.Save_Out += SubGraph_SaveLoadBool_Save_Out_157;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_157.Load_Out += SubGraph_SaveLoadBool_Load_Out_157;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_157.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_157;
	}

	public void Start()
	{
		SyncUnityHooks();
		m_RegisteredForEvents = true;
		logic_SubGraph_BaseBomb_ShowQuestLog_SubGraph_BaseBomb_ShowQuestLog_44.Start();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_87.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_91.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_92.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_93.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_94.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_95.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_96.Start();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_120.Start();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_134.Start();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_136.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_157.Start();
	}

	public void OnEnable()
	{
		RegisterForUnityHooks();
		m_RegisteredForEvents = true;
		logic_SubGraph_BaseBomb_ShowQuestLog_SubGraph_BaseBomb_ShowQuestLog_44.OnEnable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_87.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_91.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_92.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_93.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_94.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_95.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_96.OnEnable();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_120.OnEnable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_134.OnEnable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_136.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_157.OnEnable();
		logic_uScript_HasHintBeenShownBefore_uScript_HasHintBeenShownBefore_160.OnEnable();
		logic_uScript_HasHintBeenShownBefore_uScript_HasHintBeenShownBefore_162.OnEnable();
		logic_uScript_IsCoreEncounterCompleted_uScript_IsCoreEncounterCompleted_365.OnEnable();
	}

	public void OnDisable()
	{
		logic_uScript_FireParticlesTowardsGround_uScript_FireParticlesTowardsGround_0.OnDisable();
		logic_uScript_SaveNamedBlock_uScript_SaveNamedBlock_10.OnDisable();
		logic_uScript_GetNamedBlock_uScript_GetNamedBlock_12.OnDisable();
		logic_SubGraph_BaseBomb_ShowQuestLog_SubGraph_BaseBomb_ShowQuestLog_44.OnDisable();
		logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_46.OnDisable();
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_75.OnDisable();
		logic_uScript_SpawnBlockAbovePlayer_uScript_SpawnBlockAbovePlayer_77.OnDisable();
		logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_84.OnDisable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_87.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_91.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_92.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_93.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_94.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_95.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_96.OnDisable();
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_117.OnDisable();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_120.OnDisable();
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_123.OnDisable();
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_131.OnDisable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_134.OnDisable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_136.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_138.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_140.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_143.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_147.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_157.OnDisable();
		UnregisterEventListeners();
		m_RegisteredForEvents = false;
	}

	public void Update()
	{
		SyncEventListeners();
		logic_SubGraph_BaseBomb_ShowQuestLog_SubGraph_BaseBomb_ShowQuestLog_44.Update();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_87.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_91.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_92.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_93.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_94.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_95.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_96.Update();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_120.Update();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_134.Update();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_136.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_157.Update();
	}

	public void OnDestroy()
	{
		logic_SubGraph_BaseBomb_ShowQuestLog_SubGraph_BaseBomb_ShowQuestLog_44.OnDestroy();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_87.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_91.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_92.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_93.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_94.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_95.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_96.OnDestroy();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_120.OnDestroy();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_134.OnDestroy();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_136.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_157.OnDestroy();
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_3.Output1 -= uScriptCon_ManualSwitch_Output1_3;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_3.Output2 -= uScriptCon_ManualSwitch_Output2_3;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_3.Output3 -= uScriptCon_ManualSwitch_Output3_3;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_3.Output4 -= uScriptCon_ManualSwitch_Output4_3;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_3.Output5 -= uScriptCon_ManualSwitch_Output5_3;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_3.Output6 -= uScriptCon_ManualSwitch_Output6_3;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_3.Output7 -= uScriptCon_ManualSwitch_Output7_3;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_3.Output8 -= uScriptCon_ManualSwitch_Output8_3;
		logic_SubGraph_BaseBomb_ShowQuestLog_SubGraph_BaseBomb_ShowQuestLog_44.Out -= SubGraph_BaseBomb_ShowQuestLog_Out_44;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_87.Save_Out -= SubGraph_SaveLoadInt_Save_Out_87;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_87.Load_Out -= SubGraph_SaveLoadInt_Load_Out_87;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_87.Restart_Out -= SubGraph_SaveLoadInt_Restart_Out_87;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_91.Save_Out -= SubGraph_SaveLoadBool_Save_Out_91;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_91.Load_Out -= SubGraph_SaveLoadBool_Load_Out_91;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_91.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_91;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_92.Save_Out -= SubGraph_SaveLoadBool_Save_Out_92;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_92.Load_Out -= SubGraph_SaveLoadBool_Load_Out_92;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_92.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_92;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_93.Save_Out -= SubGraph_SaveLoadBool_Save_Out_93;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_93.Load_Out -= SubGraph_SaveLoadBool_Load_Out_93;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_93.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_93;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_94.Save_Out -= SubGraph_SaveLoadBool_Save_Out_94;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_94.Load_Out -= SubGraph_SaveLoadBool_Load_Out_94;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_94.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_94;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_95.Save_Out -= SubGraph_SaveLoadBool_Save_Out_95;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_95.Load_Out -= SubGraph_SaveLoadBool_Load_Out_95;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_95.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_95;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_96.Save_Out -= SubGraph_SaveLoadBool_Save_Out_96;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_96.Load_Out -= SubGraph_SaveLoadBool_Load_Out_96;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_96.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_96;
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_120.Out -= SubGraph_LoadObjectiveStates_Out_120;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_134.Out -= SubGraph_CompleteObjectiveStage_Out_134;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_136.Out -= SubGraph_CompleteObjectiveStage_Out_136;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_157.Save_Out -= SubGraph_SaveLoadBool_Save_Out_157;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_157.Load_Out -= SubGraph_SaveLoadBool_Load_Out_157;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_157.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_157;
	}

	private void Instance_OnUpdate_63(object o, EventArgs e)
	{
		Relay_OnUpdate_63();
	}

	private void Instance_OnSuspend_63(object o, EventArgs e)
	{
		Relay_OnSuspend_63();
	}

	private void Instance_OnResume_63(object o, EventArgs e)
	{
		Relay_OnResume_63();
	}

	private void Instance_SaveEvent_88(object o, EventArgs e)
	{
		Relay_SaveEvent_88();
	}

	private void Instance_LoadEvent_88(object o, EventArgs e)
	{
		Relay_LoadEvent_88();
	}

	private void Instance_RestartEvent_88(object o, EventArgs e)
	{
		Relay_RestartEvent_88();
	}

	private void uScriptCon_ManualSwitch_Output1_3(object o, EventArgs e)
	{
		Relay_Output1_3();
	}

	private void uScriptCon_ManualSwitch_Output2_3(object o, EventArgs e)
	{
		Relay_Output2_3();
	}

	private void uScriptCon_ManualSwitch_Output3_3(object o, EventArgs e)
	{
		Relay_Output3_3();
	}

	private void uScriptCon_ManualSwitch_Output4_3(object o, EventArgs e)
	{
		Relay_Output4_3();
	}

	private void uScriptCon_ManualSwitch_Output5_3(object o, EventArgs e)
	{
		Relay_Output5_3();
	}

	private void uScriptCon_ManualSwitch_Output6_3(object o, EventArgs e)
	{
		Relay_Output6_3();
	}

	private void uScriptCon_ManualSwitch_Output7_3(object o, EventArgs e)
	{
		Relay_Output7_3();
	}

	private void uScriptCon_ManualSwitch_Output8_3(object o, EventArgs e)
	{
		Relay_Output8_3();
	}

	private void SubGraph_BaseBomb_ShowQuestLog_Out_44(object o, SubGraph_BaseBomb_ShowQuestLog.LogicEventArgs e)
	{
		logic_SubGraph_BaseBomb_ShowQuestLog_Flag_QuestLogShown_44 = e.Flag_QuestLogShown;
		local_QuestLogShown_System_Boolean = logic_SubGraph_BaseBomb_ShowQuestLog_Flag_QuestLogShown_44;
		Relay_Out_44();
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

	private void SubGraph_SaveLoadBool_Save_Out_91(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_91 = e.boolean;
		local_AI_Set_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_91;
		Relay_Save_Out_91();
	}

	private void SubGraph_SaveLoadBool_Load_Out_91(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_91 = e.boolean;
		local_AI_Set_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_91;
		Relay_Load_Out_91();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_91(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_91 = e.boolean;
		local_AI_Set_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_91;
		Relay_Restart_Out_91();
	}

	private void SubGraph_SaveLoadBool_Save_Out_92(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_92 = e.boolean;
		local_Enemy_Defeated_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_92;
		Relay_Save_Out_92();
	}

	private void SubGraph_SaveLoadBool_Load_Out_92(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_92 = e.boolean;
		local_Enemy_Defeated_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_92;
		Relay_Load_Out_92();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_92(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_92 = e.boolean;
		local_Enemy_Defeated_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_92;
		Relay_Restart_Out_92();
	}

	private void SubGraph_SaveLoadBool_Save_Out_93(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_93 = e.boolean;
		local_PositionCached_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_93;
		Relay_Save_Out_93();
	}

	private void SubGraph_SaveLoadBool_Load_Out_93(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_93 = e.boolean;
		local_PositionCached_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_93;
		Relay_Load_Out_93();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_93(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_93 = e.boolean;
		local_PositionCached_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_93;
		Relay_Restart_Out_93();
	}

	private void SubGraph_SaveLoadBool_Save_Out_94(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_94 = e.boolean;
		local_QuestLogShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_94;
		Relay_Save_Out_94();
	}

	private void SubGraph_SaveLoadBool_Load_Out_94(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_94 = e.boolean;
		local_QuestLogShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_94;
		Relay_Load_Out_94();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_94(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_94 = e.boolean;
		local_QuestLogShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_94;
		Relay_Restart_Out_94();
	}

	private void SubGraph_SaveLoadBool_Save_Out_95(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_95 = e.boolean;
		local_EnemySpawned_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_95;
		Relay_Save_Out_95();
	}

	private void SubGraph_SaveLoadBool_Load_Out_95(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_95 = e.boolean;
		local_EnemySpawned_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_95;
		Relay_Load_Out_95();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_95(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_95 = e.boolean;
		local_EnemySpawned_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_95;
		Relay_Restart_Out_95();
	}

	private void SubGraph_SaveLoadBool_Save_Out_96(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_96 = e.boolean;
		local_MsgThiefSpottedShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_96;
		Relay_Save_Out_96();
	}

	private void SubGraph_SaveLoadBool_Load_Out_96(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_96 = e.boolean;
		local_MsgThiefSpottedShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_96;
		Relay_Load_Out_96();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_96(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_96 = e.boolean;
		local_MsgThiefSpottedShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_96;
		Relay_Restart_Out_96();
	}

	private void SubGraph_LoadObjectiveStates_Out_120(object o, SubGraph_LoadObjectiveStates.LogicEventArgs e)
	{
		Relay_Out_120();
	}

	private void SubGraph_CompleteObjectiveStage_Out_134(object o, SubGraph_CompleteObjectiveStage.LogicEventArgs e)
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_134 = e.objectiveStage;
		local_Stage_System_Int32 = logic_SubGraph_CompleteObjectiveStage_objectiveStage_134;
		Relay_Out_134();
	}

	private void SubGraph_CompleteObjectiveStage_Out_136(object o, SubGraph_CompleteObjectiveStage.LogicEventArgs e)
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_136 = e.objectiveStage;
		local_Stage_System_Int32 = logic_SubGraph_CompleteObjectiveStage_objectiveStage_136;
		Relay_Out_136();
	}

	private void SubGraph_SaveLoadBool_Save_Out_157(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_157 = e.boolean;
		local_RadarAttached_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_157;
		Relay_Save_Out_157();
	}

	private void SubGraph_SaveLoadBool_Load_Out_157(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_157 = e.boolean;
		local_RadarAttached_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_157;
		Relay_Load_Out_157();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_157(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_157 = e.boolean;
		local_RadarAttached_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_157;
		Relay_Restart_Out_157();
	}

	private void Relay_In_0()
	{
		logic_uScript_FireParticlesTowardsGround_groundPos_0 = local_EncounterPos_UnityEngine_Vector3;
		logic_uScript_FireParticlesTowardsGround_particleEffect_0 = Particles;
		logic_uScript_FireParticlesTowardsGround_owner_0 = owner_Connection_2;
		logic_uScript_FireParticlesTowardsGround_Return_0 = logic_uScript_FireParticlesTowardsGround_uScript_FireParticlesTowardsGround_0.In(logic_uScript_FireParticlesTowardsGround_groundPos_0, logic_uScript_FireParticlesTowardsGround_particleEffect_0, logic_uScript_FireParticlesTowardsGround_owner_0, logic_uScript_FireParticlesTowardsGround_uniqueName_0);
		if (logic_uScript_FireParticlesTowardsGround_uScript_FireParticlesTowardsGround_0.Delivered)
		{
			Relay_In_44();
		}
	}

	private void Relay_Output1_3()
	{
		Relay_In_56();
	}

	private void Relay_Output2_3()
	{
		Relay_In_126();
	}

	private void Relay_Output3_3()
	{
		Relay_In_59();
	}

	private void Relay_Output4_3()
	{
	}

	private void Relay_Output5_3()
	{
	}

	private void Relay_Output6_3()
	{
	}

	private void Relay_Output7_3()
	{
	}

	private void Relay_Output8_3()
	{
	}

	private void Relay_In_3()
	{
		logic_uScriptCon_ManualSwitch_CurrentOutput_3 = local_Stage_System_Int32;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_3.In(logic_uScriptCon_ManualSwitch_CurrentOutput_3);
	}

	private void Relay_True_5()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_5.True(out logic_uScriptAct_SetBool_Target_5);
		local_Enemy_Defeated_System_Boolean = logic_uScriptAct_SetBool_Target_5;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_5.Out)
		{
			Relay_In_12();
		}
	}

	private void Relay_False_5()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_5.False(out logic_uScriptAct_SetBool_Target_5);
		local_Enemy_Defeated_System_Boolean = logic_uScriptAct_SetBool_Target_5;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_5.Out)
		{
			Relay_In_12();
		}
	}

	private void Relay_In_6()
	{
		logic_uScriptCon_CompareBool_Bool_6 = local_Enemy_Defeated_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_6.In(logic_uScriptCon_CompareBool_Bool_6);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_6.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_6.False;
		if (num)
		{
			Relay_In_136();
		}
		if (flag)
		{
			Relay_In_26();
		}
	}

	private void Relay_In_9()
	{
		logic_uScript_GetTankBlock_tank_9 = local_ThiefEnemy_Tank;
		logic_uScript_GetTankBlock_blockType_9 = local_RadarBlockType_BlockTypes;
		logic_uScript_GetTankBlock_Return_9 = logic_uScript_GetTankBlock_uScript_GetTankBlock_9.In(logic_uScript_GetTankBlock_tank_9, logic_uScript_GetTankBlock_blockType_9);
		local_RadarBlock_TankBlock = logic_uScript_GetTankBlock_Return_9;
		if (logic_uScript_GetTankBlock_uScript_GetTankBlock_9.Returned)
		{
			Relay_In_10();
		}
	}

	private void Relay_In_10()
	{
		logic_uScript_SaveNamedBlock_block_10 = local_RadarBlock_TankBlock;
		logic_uScript_SaveNamedBlock_uniqueName_10 = local_BlockRadar_System_String;
		logic_uScript_SaveNamedBlock_owner_10 = owner_Connection_11;
		logic_uScript_SaveNamedBlock_uScript_SaveNamedBlock_10.In(logic_uScript_SaveNamedBlock_block_10, logic_uScript_SaveNamedBlock_uniqueName_10, logic_uScript_SaveNamedBlock_owner_10);
		if (logic_uScript_SaveNamedBlock_uScript_SaveNamedBlock_10.Out)
		{
			Relay_In_20();
		}
	}

	private void Relay_In_12()
	{
		logic_uScript_GetNamedBlock_name_12 = local_BlockRadar_System_String;
		logic_uScript_GetNamedBlock_owner_12 = owner_Connection_14;
		logic_uScript_GetNamedBlock_Return_12 = logic_uScript_GetNamedBlock_uScript_GetNamedBlock_12.In(logic_uScript_GetNamedBlock_name_12, logic_uScript_GetNamedBlock_owner_12);
		local_RadarBlock_TankBlock = logic_uScript_GetNamedBlock_Return_12;
		bool destroyed = logic_uScript_GetNamedBlock_uScript_GetNamedBlock_12.Destroyed;
		bool blockExists = logic_uScript_GetNamedBlock_uScript_GetNamedBlock_12.BlockExists;
		bool waitingForBlock = logic_uScript_GetNamedBlock_uScript_GetNamedBlock_12.WaitingForBlock;
		bool noBlock = logic_uScript_GetNamedBlock_uScript_GetNamedBlock_12.NoBlock;
		if (destroyed)
		{
			Relay_In_79();
		}
		if (blockExists)
		{
			Relay_In_106();
		}
		if (waitingForBlock)
		{
			Relay_In_107();
		}
		if (noBlock)
		{
			Relay_In_9();
		}
	}

	private void Relay_In_17()
	{
		logic_uScript_AI_SetPOI_tank_17 = local_ThiefEnemy_Tank;
		logic_uScript_AI_SetPOI_position_17 = local_InitialPos_UnityEngine_Vector3;
		logic_uScript_AI_SetPOI_uScript_AI_SetPOI_17.In(logic_uScript_AI_SetPOI_tank_17, logic_uScript_AI_SetPOI_usePOI_17, logic_uScript_AI_SetPOI_position_17, logic_uScript_AI_SetPOI_distance_17);
		if (logic_uScript_AI_SetPOI_uScript_AI_SetPOI_17.Out)
		{
			Relay_In_123();
		}
	}

	private void Relay_In_20()
	{
		logic_uScript_KeepBlockInvulnerable_block_20 = local_RadarBlock_TankBlock;
		logic_uScript_KeepBlockInvulnerable_uScript_KeepBlockInvulnerable_20.In(logic_uScript_KeepBlockInvulnerable_block_20);
		if (logic_uScript_KeepBlockInvulnerable_uScript_KeepBlockInvulnerable_20.Out)
		{
			Relay_In_3();
		}
	}

	private void Relay_In_26()
	{
		logic_uScript_DoesTankHave_tank_26 = local_ThiefEnemy_Tank;
		logic_uScript_DoesTankHave_block_26 = local_RadarBlockType_BlockTypes;
		logic_uScript_DoesTankHave_uScript_DoesTankHave_26.In(logic_uScript_DoesTankHave_tank_26, logic_uScript_DoesTankHave_amountOfPieces_26, logic_uScript_DoesTankHave_block_26, logic_uScript_DoesTankHave_checkForAmountOnly_26);
		if (logic_uScript_DoesTankHave_uScript_DoesTankHave_26.False)
		{
			Relay_In_136();
		}
	}

	private void Relay_In_32()
	{
		logic_uScript_GetPositionInEncounter_ownerNode_32 = owner_Connection_38;
		logic_uScript_GetPositionInEncounter_posName_32 = PositionId;
		logic_uScript_GetPositionInEncounter_Return_32 = logic_uScript_GetPositionInEncounter_uScript_GetPositionInEncounter_32.In(logic_uScript_GetPositionInEncounter_ownerNode_32, logic_uScript_GetPositionInEncounter_posName_32);
		local_EncounterPos_UnityEngine_Vector3 = logic_uScript_GetPositionInEncounter_Return_32;
		if (logic_uScript_GetPositionInEncounter_uScript_GetPositionInEncounter_32.Out)
		{
			Relay_In_36();
		}
	}

	private void Relay_True_34()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_34.True(out logic_uScriptAct_SetBool_Target_34);
		local_PositionCached_System_Boolean = logic_uScriptAct_SetBool_Target_34;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_34.Out)
		{
			Relay_In_0();
		}
	}

	private void Relay_False_34()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_34.False(out logic_uScriptAct_SetBool_Target_34);
		local_PositionCached_System_Boolean = logic_uScriptAct_SetBool_Target_34;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_34.Out)
		{
			Relay_In_0();
		}
	}

	private void Relay_In_35()
	{
		logic_uScriptAct_SetVector3_Value_35 = local_EncounterPos_UnityEngine_Vector3;
		logic_uScriptAct_SetVector3_uScriptAct_SetVector3_35.In(logic_uScriptAct_SetVector3_Value_35, out logic_uScriptAct_SetVector3_TargetVector3_35);
		local_InitialPos_UnityEngine_Vector3 = logic_uScriptAct_SetVector3_TargetVector3_35;
		if (logic_uScriptAct_SetVector3_uScriptAct_SetVector3_35.Out)
		{
			Relay_True_34();
		}
	}

	private void Relay_In_36()
	{
		logic_uScriptCon_CompareBool_Bool_36 = local_PositionCached_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_36.In(logic_uScriptCon_CompareBool_Bool_36);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_36.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_36.False;
		if (num)
		{
			Relay_In_0();
		}
		if (flag)
		{
			Relay_In_35();
		}
	}

	private void Relay_Out_44()
	{
		Relay_In_66();
	}

	private void Relay_In_44()
	{
		logic_SubGraph_BaseBomb_ShowQuestLog_Flag_QuestLogShown_44 = local_QuestLogShown_System_Boolean;
		logic_SubGraph_BaseBomb_ShowQuestLog_SubGraph_BaseBomb_ShowQuestLog_44.In(ref logic_SubGraph_BaseBomb_ShowQuestLog_Flag_QuestLogShown_44);
	}

	private void Relay_In_46()
	{
		logic_uScript_PlayerInRangeOfCurrentEncounter_ownerNode_46 = owner_Connection_47;
		logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_46.In(logic_uScript_PlayerInRangeOfCurrentEncounter_ownerNode_46);
		bool num = logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_46.True;
		bool flag = logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_46.False;
		if (num)
		{
			Relay_In_134();
		}
		if (flag)
		{
			Relay_In_131();
		}
	}

	private void Relay_In_50()
	{
		logic_uScript_MoveEncounterWithVisible_ownerNode_50 = owner_Connection_28;
		logic_uScript_MoveEncounterWithVisible_visibleObject_50 = local_ThiefEnemy_Tank;
		logic_uScript_MoveEncounterWithVisible_uScript_MoveEncounterWithVisible_50.In(logic_uScript_MoveEncounterWithVisible_ownerNode_50, logic_uScript_MoveEncounterWithVisible_visibleObject_50);
		if (logic_uScript_MoveEncounterWithVisible_uScript_MoveEncounterWithVisible_50.Out)
		{
			Relay_In_46();
		}
	}

	private void Relay_In_51()
	{
		logic_uScript_MoveEncounterWithVisible_ownerNode_51 = owner_Connection_52;
		logic_uScript_MoveEncounterWithVisible_visibleObject_51 = local_RadarBlock_TankBlock;
		logic_uScript_MoveEncounterWithVisible_uScript_MoveEncounterWithVisible_51.In(logic_uScript_MoveEncounterWithVisible_ownerNode_51, logic_uScript_MoveEncounterWithVisible_visibleObject_51);
		if (logic_uScript_MoveEncounterWithVisible_uScript_MoveEncounterWithVisible_51.Out)
		{
			Relay_In_84();
		}
	}

	private void Relay_In_56()
	{
		logic_uScript_SetEncounterTarget_owner_56 = owner_Connection_58;
		logic_uScript_SetEncounterTarget_visibleObject_56 = local_ThiefEnemy_Tank;
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_56.In(logic_uScript_SetEncounterTarget_owner_56, logic_uScript_SetEncounterTarget_visibleObject_56);
		if (logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_56.Out)
		{
			Relay_In_50();
		}
	}

	private void Relay_In_59()
	{
		logic_uScript_SetEncounterTarget_owner_59 = owner_Connection_60;
		logic_uScript_SetEncounterTarget_visibleObject_59 = local_RadarBlock_TankBlock;
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_59.In(logic_uScript_SetEncounterTarget_owner_59, logic_uScript_SetEncounterTarget_visibleObject_59);
		if (logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_59.Out)
		{
			Relay_In_51();
		}
	}

	private void Relay_OnUpdate_63()
	{
		Relay_In_153();
	}

	private void Relay_OnSuspend_63()
	{
	}

	private void Relay_OnResume_63()
	{
	}

	private void Relay_InitialSpawn_64()
	{
		int num = 0;
		if (logic_uScript_SpawnTechsFromData_spawnData_64.Length <= num)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_64, num + 1);
		}
		logic_uScript_SpawnTechsFromData_spawnData_64[num++] = SpawnDataThief;
		logic_uScript_SpawnTechsFromData_ownerNode_64 = owner_Connection_43;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_64.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_64, logic_uScript_SpawnTechsFromData_ownerNode_64, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_64);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_64.Out)
		{
			Relay_In_68();
		}
	}

	private void Relay_AtIndex_65()
	{
		int num = 0;
		Array array = local_71_TankArray;
		if (logic_uScript_AccessListTech_techList_65.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_65, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_65, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_65.AtIndex(ref logic_uScript_AccessListTech_techList_65, logic_uScript_AccessListTech_index_65, out logic_uScript_AccessListTech_value_65);
		local_71_TankArray = logic_uScript_AccessListTech_techList_65;
		local_ThiefEnemy_Tank = logic_uScript_AccessListTech_value_65;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_65.Out)
		{
			Relay_In_17();
		}
	}

	private void Relay_In_66()
	{
		logic_uScriptCon_CompareBool_Bool_66 = local_EnemySpawned_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_66.In(logic_uScriptCon_CompareBool_Bool_66);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_66.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_66.False;
		if (num)
		{
			Relay_In_68();
		}
		if (flag)
		{
			Relay_True_73();
		}
	}

	private void Relay_In_68()
	{
		int num = 0;
		if (logic_uScript_GetAndCheckTechs_techData_68.Length <= num)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_68, num + 1);
		}
		logic_uScript_GetAndCheckTechs_techData_68[num++] = SpawnDataThief;
		logic_uScript_GetAndCheckTechs_ownerNode_68 = owner_Connection_70;
		int num2 = 0;
		Array array = local_71_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_68.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_68, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techs_68, num2, array.Length);
		num2 += array.Length;
		logic_uScript_GetAndCheckTechs_Return_68 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_68.In(logic_uScript_GetAndCheckTechs_techData_68, logic_uScript_GetAndCheckTechs_ownerNode_68, ref logic_uScript_GetAndCheckTechs_techs_68);
		local_71_TankArray = logic_uScript_GetAndCheckTechs_techs_68;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_68.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_68.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_68.AllDead;
		bool waitingToSpawn = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_68.WaitingToSpawn;
		if (allAlive)
		{
			Relay_AtIndex_65();
		}
		if (someAlive)
		{
			Relay_AtIndex_65();
		}
		if (allDead)
		{
			Relay_True_5();
		}
		if (waitingToSpawn)
		{
			Relay_In_108();
		}
	}

	private void Relay_True_73()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_73.True(out logic_uScriptAct_SetBool_Target_73);
		local_EnemySpawned_System_Boolean = logic_uScriptAct_SetBool_Target_73;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_73.Out)
		{
			Relay_InitialSpawn_64();
		}
	}

	private void Relay_False_73()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_73.False(out logic_uScriptAct_SetBool_Target_73);
		local_EnemySpawned_System_Boolean = logic_uScriptAct_SetBool_Target_73;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_73.Out)
		{
			Relay_InitialSpawn_64();
		}
	}

	private void Relay_In_75()
	{
		logic_uScript_SetTankInvulnerable_tank_75 = local_ThiefEnemy_Tank;
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_75.In(logic_uScript_SetTankInvulnerable_invulnerable_75, logic_uScript_SetTankInvulnerable_tank_75);
		if (logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_75.Out)
		{
			Relay_In_103();
		}
	}

	private void Relay_In_77()
	{
		logic_uScript_SpawnBlockAbovePlayer_blockType_77 = local_RadarBlockType_BlockTypes;
		logic_uScript_SpawnBlockAbovePlayer_uniqueName_77 = local_BlockRadar_System_String;
		logic_uScript_SpawnBlockAbovePlayer_owner_77 = owner_Connection_78;
		logic_uScript_SpawnBlockAbovePlayer_Return_77 = logic_uScript_SpawnBlockAbovePlayer_uScript_SpawnBlockAbovePlayer_77.In(logic_uScript_SpawnBlockAbovePlayer_blockType_77, logic_uScript_SpawnBlockAbovePlayer_uniqueName_77, logic_uScript_SpawnBlockAbovePlayer_owner_77);
		if (logic_uScript_SpawnBlockAbovePlayer_uScript_SpawnBlockAbovePlayer_77.Out)
		{
			Relay_Succeed_112();
		}
	}

	private void Relay_In_79()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_79 = messageTag;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_79.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_79, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_79);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_79.Out)
		{
			Relay_In_143();
		}
	}

	private void Relay_In_84()
	{
		logic_uScript_PlayerInRangeOfCurrentEncounter_ownerNode_84 = owner_Connection_85;
		logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_84.In(logic_uScript_PlayerInRangeOfCurrentEncounter_ownerNode_84);
		bool num = logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_84.True;
		bool flag = logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_84.False;
		if (num)
		{
			Relay_In_140();
		}
		if (flag)
		{
			Relay_In_86();
		}
	}

	private void Relay_In_86()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_86 = messageTag;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_86.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_86, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_86);
	}

	private void Relay_Save_Out_87()
	{
		Relay_Save_157();
	}

	private void Relay_Load_Out_87()
	{
		Relay_Load_157();
	}

	private void Relay_Restart_Out_87()
	{
		Relay_Set_False_157();
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

	private void Relay_SaveEvent_88()
	{
		Relay_Save_87();
	}

	private void Relay_LoadEvent_88()
	{
		Relay_Load_87();
	}

	private void Relay_RestartEvent_88()
	{
		Relay_Restart_87();
	}

	private void Relay_Save_Out_91()
	{
		Relay_Save_92();
	}

	private void Relay_Load_Out_91()
	{
		Relay_Load_92();
	}

	private void Relay_Restart_Out_91()
	{
		Relay_Set_False_92();
	}

	private void Relay_Save_91()
	{
		logic_SubGraph_SaveLoadBool_boolean_91 = local_AI_Set_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_91 = local_AI_Set_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_91.Save(ref logic_SubGraph_SaveLoadBool_boolean_91, logic_SubGraph_SaveLoadBool_boolAsVariable_91, logic_SubGraph_SaveLoadBool_uniqueID_91);
	}

	private void Relay_Load_91()
	{
		logic_SubGraph_SaveLoadBool_boolean_91 = local_AI_Set_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_91 = local_AI_Set_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_91.Load(ref logic_SubGraph_SaveLoadBool_boolean_91, logic_SubGraph_SaveLoadBool_boolAsVariable_91, logic_SubGraph_SaveLoadBool_uniqueID_91);
	}

	private void Relay_Set_True_91()
	{
		logic_SubGraph_SaveLoadBool_boolean_91 = local_AI_Set_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_91 = local_AI_Set_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_91.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_91, logic_SubGraph_SaveLoadBool_boolAsVariable_91, logic_SubGraph_SaveLoadBool_uniqueID_91);
	}

	private void Relay_Set_False_91()
	{
		logic_SubGraph_SaveLoadBool_boolean_91 = local_AI_Set_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_91 = local_AI_Set_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_91.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_91, logic_SubGraph_SaveLoadBool_boolAsVariable_91, logic_SubGraph_SaveLoadBool_uniqueID_91);
	}

	private void Relay_Save_Out_92()
	{
		Relay_Save_93();
	}

	private void Relay_Load_Out_92()
	{
		Relay_Load_93();
	}

	private void Relay_Restart_Out_92()
	{
		Relay_Set_False_93();
	}

	private void Relay_Save_92()
	{
		logic_SubGraph_SaveLoadBool_boolean_92 = local_Enemy_Defeated_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_92 = local_Enemy_Defeated_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_92.Save(ref logic_SubGraph_SaveLoadBool_boolean_92, logic_SubGraph_SaveLoadBool_boolAsVariable_92, logic_SubGraph_SaveLoadBool_uniqueID_92);
	}

	private void Relay_Load_92()
	{
		logic_SubGraph_SaveLoadBool_boolean_92 = local_Enemy_Defeated_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_92 = local_Enemy_Defeated_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_92.Load(ref logic_SubGraph_SaveLoadBool_boolean_92, logic_SubGraph_SaveLoadBool_boolAsVariable_92, logic_SubGraph_SaveLoadBool_uniqueID_92);
	}

	private void Relay_Set_True_92()
	{
		logic_SubGraph_SaveLoadBool_boolean_92 = local_Enemy_Defeated_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_92 = local_Enemy_Defeated_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_92.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_92, logic_SubGraph_SaveLoadBool_boolAsVariable_92, logic_SubGraph_SaveLoadBool_uniqueID_92);
	}

	private void Relay_Set_False_92()
	{
		logic_SubGraph_SaveLoadBool_boolean_92 = local_Enemy_Defeated_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_92 = local_Enemy_Defeated_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_92.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_92, logic_SubGraph_SaveLoadBool_boolAsVariable_92, logic_SubGraph_SaveLoadBool_uniqueID_92);
	}

	private void Relay_Save_Out_93()
	{
		Relay_Save_94();
	}

	private void Relay_Load_Out_93()
	{
		Relay_Load_94();
	}

	private void Relay_Restart_Out_93()
	{
		Relay_Set_False_94();
	}

	private void Relay_Save_93()
	{
		logic_SubGraph_SaveLoadBool_boolean_93 = local_PositionCached_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_93 = local_PositionCached_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_93.Save(ref logic_SubGraph_SaveLoadBool_boolean_93, logic_SubGraph_SaveLoadBool_boolAsVariable_93, logic_SubGraph_SaveLoadBool_uniqueID_93);
	}

	private void Relay_Load_93()
	{
		logic_SubGraph_SaveLoadBool_boolean_93 = local_PositionCached_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_93 = local_PositionCached_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_93.Load(ref logic_SubGraph_SaveLoadBool_boolean_93, logic_SubGraph_SaveLoadBool_boolAsVariable_93, logic_SubGraph_SaveLoadBool_uniqueID_93);
	}

	private void Relay_Set_True_93()
	{
		logic_SubGraph_SaveLoadBool_boolean_93 = local_PositionCached_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_93 = local_PositionCached_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_93.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_93, logic_SubGraph_SaveLoadBool_boolAsVariable_93, logic_SubGraph_SaveLoadBool_uniqueID_93);
	}

	private void Relay_Set_False_93()
	{
		logic_SubGraph_SaveLoadBool_boolean_93 = local_PositionCached_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_93 = local_PositionCached_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_93.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_93, logic_SubGraph_SaveLoadBool_boolAsVariable_93, logic_SubGraph_SaveLoadBool_uniqueID_93);
	}

	private void Relay_Save_Out_94()
	{
		Relay_Save_95();
	}

	private void Relay_Load_Out_94()
	{
		Relay_Load_95();
	}

	private void Relay_Restart_Out_94()
	{
		Relay_Set_False_95();
	}

	private void Relay_Save_94()
	{
		logic_SubGraph_SaveLoadBool_boolean_94 = local_QuestLogShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_94 = local_QuestLogShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_94.Save(ref logic_SubGraph_SaveLoadBool_boolean_94, logic_SubGraph_SaveLoadBool_boolAsVariable_94, logic_SubGraph_SaveLoadBool_uniqueID_94);
	}

	private void Relay_Load_94()
	{
		logic_SubGraph_SaveLoadBool_boolean_94 = local_QuestLogShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_94 = local_QuestLogShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_94.Load(ref logic_SubGraph_SaveLoadBool_boolean_94, logic_SubGraph_SaveLoadBool_boolAsVariable_94, logic_SubGraph_SaveLoadBool_uniqueID_94);
	}

	private void Relay_Set_True_94()
	{
		logic_SubGraph_SaveLoadBool_boolean_94 = local_QuestLogShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_94 = local_QuestLogShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_94.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_94, logic_SubGraph_SaveLoadBool_boolAsVariable_94, logic_SubGraph_SaveLoadBool_uniqueID_94);
	}

	private void Relay_Set_False_94()
	{
		logic_SubGraph_SaveLoadBool_boolean_94 = local_QuestLogShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_94 = local_QuestLogShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_94.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_94, logic_SubGraph_SaveLoadBool_boolAsVariable_94, logic_SubGraph_SaveLoadBool_uniqueID_94);
	}

	private void Relay_Save_Out_95()
	{
		Relay_Save_96();
	}

	private void Relay_Load_Out_95()
	{
		Relay_Load_96();
	}

	private void Relay_Restart_Out_95()
	{
		Relay_Set_False_96();
	}

	private void Relay_Save_95()
	{
		logic_SubGraph_SaveLoadBool_boolean_95 = local_EnemySpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_95 = local_EnemySpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_95.Save(ref logic_SubGraph_SaveLoadBool_boolean_95, logic_SubGraph_SaveLoadBool_boolAsVariable_95, logic_SubGraph_SaveLoadBool_uniqueID_95);
	}

	private void Relay_Load_95()
	{
		logic_SubGraph_SaveLoadBool_boolean_95 = local_EnemySpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_95 = local_EnemySpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_95.Load(ref logic_SubGraph_SaveLoadBool_boolean_95, logic_SubGraph_SaveLoadBool_boolAsVariable_95, logic_SubGraph_SaveLoadBool_uniqueID_95);
	}

	private void Relay_Set_True_95()
	{
		logic_SubGraph_SaveLoadBool_boolean_95 = local_EnemySpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_95 = local_EnemySpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_95.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_95, logic_SubGraph_SaveLoadBool_boolAsVariable_95, logic_SubGraph_SaveLoadBool_uniqueID_95);
	}

	private void Relay_Set_False_95()
	{
		logic_SubGraph_SaveLoadBool_boolean_95 = local_EnemySpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_95 = local_EnemySpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_95.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_95, logic_SubGraph_SaveLoadBool_boolAsVariable_95, logic_SubGraph_SaveLoadBool_uniqueID_95);
	}

	private void Relay_Save_Out_96()
	{
	}

	private void Relay_Load_Out_96()
	{
		Relay_In_120();
	}

	private void Relay_Restart_Out_96()
	{
	}

	private void Relay_Save_96()
	{
		logic_SubGraph_SaveLoadBool_boolean_96 = local_MsgThiefSpottedShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_96 = local_MsgThiefSpottedShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_96.Save(ref logic_SubGraph_SaveLoadBool_boolean_96, logic_SubGraph_SaveLoadBool_boolAsVariable_96, logic_SubGraph_SaveLoadBool_uniqueID_96);
	}

	private void Relay_Load_96()
	{
		logic_SubGraph_SaveLoadBool_boolean_96 = local_MsgThiefSpottedShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_96 = local_MsgThiefSpottedShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_96.Load(ref logic_SubGraph_SaveLoadBool_boolean_96, logic_SubGraph_SaveLoadBool_boolAsVariable_96, logic_SubGraph_SaveLoadBool_uniqueID_96);
	}

	private void Relay_Set_True_96()
	{
		logic_SubGraph_SaveLoadBool_boolean_96 = local_MsgThiefSpottedShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_96 = local_MsgThiefSpottedShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_96.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_96, logic_SubGraph_SaveLoadBool_boolAsVariable_96, logic_SubGraph_SaveLoadBool_uniqueID_96);
	}

	private void Relay_Set_False_96()
	{
		logic_SubGraph_SaveLoadBool_boolean_96 = local_MsgThiefSpottedShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_96 = local_MsgThiefSpottedShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_96.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_96, logic_SubGraph_SaveLoadBool_boolAsVariable_96, logic_SubGraph_SaveLoadBool_uniqueID_96);
	}

	private void Relay_In_103()
	{
		logic_uScriptCon_CompareBool_Bool_103 = local_MsgThiefSpottedShown_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_103.In(logic_uScriptCon_CompareBool_Bool_103);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_103.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_103.False;
		if (num)
		{
			Relay_In_6();
		}
		if (flag)
		{
			Relay_True_104();
		}
	}

	private void Relay_True_104()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_104.True(out logic_uScriptAct_SetBool_Target_104);
		local_MsgThiefSpottedShown_System_Boolean = logic_uScriptAct_SetBool_Target_104;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_104.Out)
		{
			Relay_In_138();
		}
	}

	private void Relay_False_104()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_104.False(out logic_uScriptAct_SetBool_Target_104);
		local_MsgThiefSpottedShown_System_Boolean = logic_uScriptAct_SetBool_Target_104;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_104.Out)
		{
			Relay_In_138();
		}
	}

	private void Relay_In_106()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_106.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_106.Out)
		{
			Relay_In_20();
		}
	}

	private void Relay_In_107()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_107.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_107.Out)
		{
			Relay_In_20();
		}
	}

	private void Relay_In_108()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_108.In();
	}

	private void Relay_Succeed_109()
	{
		logic_uScript_FinishEncounter_owner_109 = owner_Connection_110;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_109.Succeed(logic_uScript_FinishEncounter_owner_109);
	}

	private void Relay_Fail_109()
	{
		logic_uScript_FinishEncounter_owner_109 = owner_Connection_110;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_109.Fail(logic_uScript_FinishEncounter_owner_109);
	}

	private void Relay_Succeed_112()
	{
		logic_uScript_FinishEncounter_owner_112 = owner_Connection_113;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_112.Succeed(logic_uScript_FinishEncounter_owner_112);
	}

	private void Relay_Fail_112()
	{
		logic_uScript_FinishEncounter_owner_112 = owner_Connection_113;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_112.Fail(logic_uScript_FinishEncounter_owner_112);
	}

	private void Relay_In_116()
	{
		logic_uScript_DoesPlayerHaveBlock_block_116 = local_RadarBlockType_BlockTypes;
		logic_uScript_DoesPlayerHaveBlock_uScript_DoesPlayerHaveBlock_116.In(logic_uScript_DoesPlayerHaveBlock_block_116, logic_uScript_DoesPlayerHaveBlock_isDragging_116);
		bool num = logic_uScript_DoesPlayerHaveBlock_uScript_DoesPlayerHaveBlock_116.True;
		bool flag = logic_uScript_DoesPlayerHaveBlock_uScript_DoesPlayerHaveBlock_116.False;
		if (num)
		{
			Relay_True_154();
		}
		if (flag)
		{
			Relay_In_32();
		}
	}

	private void Relay_In_117()
	{
		logic_uScript_SetTankInvulnerable_tank_117 = local_ThiefEnemy_Tank;
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_117.In(logic_uScript_SetTankInvulnerable_invulnerable_117, logic_uScript_SetTankInvulnerable_tank_117);
		if (logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_117.Out)
		{
			Relay_In_168();
		}
	}

	private void Relay_Out_120()
	{
	}

	private void Relay_In_120()
	{
		logic_SubGraph_LoadObjectiveStates_currentObjective_120 = local_Stage_System_Int32;
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_120.In(logic_SubGraph_LoadObjectiveStates_currentObjective_120);
	}

	private void Relay_In_123()
	{
		logic_uScript_SetTankInvulnerable_tank_123 = local_ThiefEnemy_Tank;
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_123.In(logic_uScript_SetTankInvulnerable_invulnerable_123, logic_uScript_SetTankInvulnerable_tank_123);
		if (logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_123.Out)
		{
			Relay_In_12();
		}
	}

	private void Relay_In_124()
	{
		logic_uScript_MoveEncounterWithVisible_ownerNode_124 = owner_Connection_129;
		logic_uScript_MoveEncounterWithVisible_visibleObject_124 = local_ThiefEnemy_Tank;
		logic_uScript_MoveEncounterWithVisible_uScript_MoveEncounterWithVisible_124.In(logic_uScript_MoveEncounterWithVisible_ownerNode_124, logic_uScript_MoveEncounterWithVisible_visibleObject_124);
		if (logic_uScript_MoveEncounterWithVisible_uScript_MoveEncounterWithVisible_124.Out)
		{
			Relay_In_75();
		}
	}

	private void Relay_In_126()
	{
		logic_uScript_SetEncounterTarget_owner_126 = owner_Connection_127;
		logic_uScript_SetEncounterTarget_visibleObject_126 = local_ThiefEnemy_Tank;
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_126.In(logic_uScript_SetEncounterTarget_owner_126, logic_uScript_SetEncounterTarget_visibleObject_126);
		if (logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_126.Out)
		{
			Relay_In_124();
		}
	}

	private void Relay_In_131()
	{
		logic_uScript_SetTankInvulnerable_tank_131 = local_ThiefEnemy_Tank;
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_131.In(logic_uScript_SetTankInvulnerable_invulnerable_131, logic_uScript_SetTankInvulnerable_tank_131);
	}

	private void Relay_Out_134()
	{
	}

	private void Relay_In_134()
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_134 = local_Stage_System_Int32;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_134.In(ref logic_SubGraph_CompleteObjectiveStage_objectiveStage_134, logic_SubGraph_CompleteObjectiveStage_isFinalObjective_134);
	}

	private void Relay_Out_136()
	{
	}

	private void Relay_In_136()
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_136 = local_Stage_System_Int32;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_136.In(ref logic_SubGraph_CompleteObjectiveStage_objectiveStage_136, logic_SubGraph_CompleteObjectiveStage_isFinalObjective_136);
	}

	private void Relay_In_138()
	{
		logic_uScript_AddMessage_messageData_138 = msg01EnemyFound;
		logic_uScript_AddMessage_speaker_138 = messageSpeaker;
		logic_uScript_AddMessage_Return_138 = logic_uScript_AddMessage_uScript_AddMessage_138.In(logic_uScript_AddMessage_messageData_138, logic_uScript_AddMessage_speaker_138);
		if (logic_uScript_AddMessage_uScript_AddMessage_138.Out)
		{
			Relay_In_6();
		}
	}

	private void Relay_In_140()
	{
		logic_uScript_AddMessage_messageData_140 = msg02PickUpAttachRadar;
		logic_uScript_AddMessage_speaker_140 = messageSpeaker;
		logic_uScript_AddMessage_Return_140 = logic_uScript_AddMessage_uScript_AddMessage_140.In(logic_uScript_AddMessage_messageData_140, logic_uScript_AddMessage_speaker_140);
		if (logic_uScript_AddMessage_uScript_AddMessage_140.Out)
		{
			Relay_In_150();
		}
	}

	private void Relay_In_143()
	{
		logic_uScript_AddMessage_messageData_143 = msg05RadarDestroyed;
		logic_uScript_AddMessage_speaker_143 = messageSpeaker;
		logic_uScript_AddMessage_Return_143 = logic_uScript_AddMessage_uScript_AddMessage_143.In(logic_uScript_AddMessage_messageData_143, logic_uScript_AddMessage_speaker_143);
		if (logic_uScript_AddMessage_uScript_AddMessage_143.Out)
		{
			Relay_In_77();
		}
	}

	private void Relay_In_147()
	{
		logic_uScript_AddMessage_messageData_147 = msg04RadarAttached;
		logic_uScript_AddMessage_speaker_147 = messageSpeaker;
		logic_uScript_AddMessage_Return_147 = logic_uScript_AddMessage_uScript_AddMessage_147.In(logic_uScript_AddMessage_messageData_147, logic_uScript_AddMessage_speaker_147);
		if (logic_uScript_AddMessage_uScript_AddMessage_147.Shown)
		{
			Relay_In_365();
		}
	}

	private void Relay_In_150()
	{
		logic_uScript_PointArrowAtBlock_block_150 = local_RadarBlock_TankBlock;
		logic_uScript_PointArrowAtBlock_uScript_PointArrowAtBlock_150.In(logic_uScript_PointArrowAtBlock_block_150, logic_uScript_PointArrowAtBlock_timeToShowFor_150, logic_uScript_PointArrowAtBlock_offset_150);
		if (logic_uScript_PointArrowAtBlock_uScript_PointArrowAtBlock_150.Out)
		{
			Relay_In_166();
		}
	}

	private void Relay_In_153()
	{
		logic_uScriptCon_CompareBool_Bool_153 = local_RadarAttached_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_153.In(logic_uScriptCon_CompareBool_Bool_153);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_153.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_153.False;
		if (num)
		{
			Relay_In_147();
		}
		if (flag)
		{
			Relay_In_116();
		}
	}

	private void Relay_True_154()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_154.True(out logic_uScriptAct_SetBool_Target_154);
		local_RadarAttached_System_Boolean = logic_uScriptAct_SetBool_Target_154;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_154.Out)
		{
			Relay_In_117();
		}
	}

	private void Relay_False_154()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_154.False(out logic_uScriptAct_SetBool_Target_154);
		local_RadarAttached_System_Boolean = logic_uScriptAct_SetBool_Target_154;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_154.Out)
		{
			Relay_In_117();
		}
	}

	private void Relay_Save_Out_157()
	{
		Relay_Save_91();
	}

	private void Relay_Load_Out_157()
	{
		Relay_Load_91();
	}

	private void Relay_Restart_Out_157()
	{
		Relay_Set_False_91();
	}

	private void Relay_Save_157()
	{
		logic_SubGraph_SaveLoadBool_boolean_157 = local_RadarAttached_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_157 = local_RadarAttached_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_157.Save(ref logic_SubGraph_SaveLoadBool_boolean_157, logic_SubGraph_SaveLoadBool_boolAsVariable_157, logic_SubGraph_SaveLoadBool_uniqueID_157);
	}

	private void Relay_Load_157()
	{
		logic_SubGraph_SaveLoadBool_boolean_157 = local_RadarAttached_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_157 = local_RadarAttached_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_157.Load(ref logic_SubGraph_SaveLoadBool_boolean_157, logic_SubGraph_SaveLoadBool_boolAsVariable_157, logic_SubGraph_SaveLoadBool_uniqueID_157);
	}

	private void Relay_Set_True_157()
	{
		logic_SubGraph_SaveLoadBool_boolean_157 = local_RadarAttached_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_157 = local_RadarAttached_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_157.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_157, logic_SubGraph_SaveLoadBool_boolAsVariable_157, logic_SubGraph_SaveLoadBool_uniqueID_157);
	}

	private void Relay_Set_False_157()
	{
		logic_SubGraph_SaveLoadBool_boolean_157 = local_RadarAttached_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_157 = local_RadarAttached_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_157.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_157, logic_SubGraph_SaveLoadBool_boolAsVariable_157, logic_SubGraph_SaveLoadBool_uniqueID_157);
	}

	private void Relay_In_158()
	{
		logic_uScript_ShowHint_hintId_158 = local_163_GameHints_HintID;
		logic_uScript_ShowHint_uScript_ShowHint_158.In(logic_uScript_ShowHint_hintId_158);
		if (logic_uScript_ShowHint_uScript_ShowHint_158.Out)
		{
			Relay_Succeed_109();
		}
	}

	private void Relay_In_159()
	{
		logic_uScript_ShowHint_hintId_159 = local_161_GameHints_HintID;
		logic_uScript_ShowHint_uScript_ShowHint_159.In(logic_uScript_ShowHint_hintId_159);
		if (logic_uScript_ShowHint_uScript_ShowHint_159.Out)
		{
			Relay_In_162();
		}
	}

	private void Relay_In_160()
	{
		logic_uScript_HasHintBeenShownBefore_hintID_160 = local_161_GameHints_HintID;
		logic_uScript_HasHintBeenShownBefore_uScript_HasHintBeenShownBefore_160.In(logic_uScript_HasHintBeenShownBefore_hintID_160);
		bool shown = logic_uScript_HasHintBeenShownBefore_uScript_HasHintBeenShownBefore_160.Shown;
		bool notShown = logic_uScript_HasHintBeenShownBefore_uScript_HasHintBeenShownBefore_160.NotShown;
		if (shown)
		{
			Relay_In_162();
		}
		if (notShown)
		{
			Relay_In_159();
		}
	}

	private void Relay_In_162()
	{
		logic_uScript_HasHintBeenShownBefore_hintID_162 = local_163_GameHints_HintID;
		logic_uScript_HasHintBeenShownBefore_uScript_HasHintBeenShownBefore_162.In(logic_uScript_HasHintBeenShownBefore_hintID_162);
		bool shown = logic_uScript_HasHintBeenShownBefore_uScript_HasHintBeenShownBefore_162.Shown;
		bool notShown = logic_uScript_HasHintBeenShownBefore_uScript_HasHintBeenShownBefore_162.NotShown;
		if (shown)
		{
			Relay_Succeed_109();
		}
		if (notShown)
		{
			Relay_In_158();
		}
	}

	private void Relay_In_166()
	{
		logic_uScript_EnableGlow_targetObject_166 = local_RadarBlock_TankBlock;
		logic_uScript_EnableGlow_uScript_EnableGlow_166.In(logic_uScript_EnableGlow_targetObject_166, logic_uScript_EnableGlow_enable_166);
	}

	private void Relay_In_168()
	{
		logic_uScript_EnableGlow_targetObject_168 = local_RadarBlock_TankBlock;
		logic_uScript_EnableGlow_uScript_EnableGlow_168.In(logic_uScript_EnableGlow_targetObject_168, logic_uScript_EnableGlow_enable_168);
		if (logic_uScript_EnableGlow_uScript_EnableGlow_168.Out)
		{
			Relay_In_147();
		}
	}

	private void Relay_In_365()
	{
		logic_uScript_IsCoreEncounterCompleted_uScript_IsCoreEncounterCompleted_365.In(logic_uScript_IsCoreEncounterCompleted_corp_365, logic_uScript_IsCoreEncounterCompleted_grade_365, logic_uScript_IsCoreEncounterCompleted_encounterName_365);
		bool num = logic_uScript_IsCoreEncounterCompleted_uScript_IsCoreEncounterCompleted_365.True;
		bool flag = logic_uScript_IsCoreEncounterCompleted_uScript_IsCoreEncounterCompleted_365.False;
		if (num)
		{
			Relay_Succeed_109();
		}
		if (flag)
		{
			Relay_In_160();
		}
	}
}
