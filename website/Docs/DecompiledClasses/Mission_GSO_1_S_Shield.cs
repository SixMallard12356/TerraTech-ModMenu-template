using System;
using UnityEngine;

[Serializable]
[FriendlyName("GSO 1-S6 Shield", "")]
[NodePath("Graphs")]
public class Mission_GSO_1_S_Shield : uScriptLogic
{
	private GameObject parentGameObject;

	private uScript_GUI thisScriptsOnGuiListener;

	private bool m_RegisteredForEvents;

	private Tank[] local_142_TankArray = new Tank[0];

	private string local_BlockName_System_String = "Shield";

	private Vector3 local_EncounterPos_UnityEngine_Vector3 = new Vector3(0f, 0f, 0f);

	private bool local_Enemy_Dead_System_Boolean;

	private bool local_EnemySpawned_System_Boolean;

	private Vector3 local_InitialPos_UnityEngine_Vector3 = new Vector3(0f, 0f, 0f);

	private string local_msgShield_System_String = "msgShield";

	private bool local_PositionCached_System_Boolean;

	private bool local_QuestLogShown_System_Boolean;

	private Tank local_Robber_Tank;

	private TankBlock local_ShieldBlock_TankBlock;

	private BlockTypes local_ShieldBlockType_BlockTypes = BlockTypes.GSOShield_111;

	private int local_Stage_System_Int32 = 1;

	public LocalisedString[] Msg1EnemyEncounter = new LocalisedString[0];

	public LocalisedString[] Msg4PickUpShield = new LocalisedString[0];

	public LocalisedString[] Msg5AttachShield = new LocalisedString[0];

	public LocalisedString[] Msg6OopsNotRight = new LocalisedString[0];

	public LocalisedString[] Msg7ShieldAttached = new LocalisedString[0];

	public Transform Particles;

	[Multiline(3)]
	public string PositionId = "";

	public LocalisedString QL1FindRetriveShield;

	public LocalisedString QL2AttackTheRobber;

	public LocalisedString QL4AttachShield;

	public LocalisedString QLTitle;

	public SpawnTechData SpawnDataRobber;

	private GameObject owner_Connection_5;

	private GameObject owner_Connection_9;

	private GameObject owner_Connection_12;

	private GameObject owner_Connection_19;

	private GameObject owner_Connection_23;

	private GameObject owner_Connection_29;

	private GameObject owner_Connection_42;

	private GameObject owner_Connection_46;

	private GameObject owner_Connection_50;

	private GameObject owner_Connection_51;

	private GameObject owner_Connection_63;

	private GameObject owner_Connection_66;

	private GameObject owner_Connection_73;

	private GameObject owner_Connection_82;

	private GameObject owner_Connection_84;

	private GameObject owner_Connection_86;

	private GameObject owner_Connection_105;

	private GameObject owner_Connection_108;

	private GameObject owner_Connection_136;

	private GameObject owner_Connection_140;

	private GameObject owner_Connection_146;

	private GameObject owner_Connection_150;

	private GameObject owner_Connection_154;

	private GameObject owner_Connection_162;

	private GameObject owner_Connection_174;

	private GameObject owner_Connection_187;

	private GameObject owner_Connection_189;

	private uScriptCon_ManualSwitch logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_1 = new uScriptCon_ManualSwitch();

	private int logic_uScriptCon_ManualSwitch_CurrentOutput_1;

	private uScript_AI_SetPOI logic_uScript_AI_SetPOI_uScript_AI_SetPOI_7 = new uScript_AI_SetPOI();

	private Tank logic_uScript_AI_SetPOI_tank_7;

	private bool logic_uScript_AI_SetPOI_usePOI_7 = true;

	private Vector3 logic_uScript_AI_SetPOI_position_7;

	private float logic_uScript_AI_SetPOI_distance_7 = 50f;

	private bool logic_uScript_AI_SetPOI_Out_7 = true;

	private uScriptAct_SetVector3 logic_uScriptAct_SetVector3_uScriptAct_SetVector3_13 = new uScriptAct_SetVector3();

	private Vector3 logic_uScriptAct_SetVector3_Value_13;

	private Vector3 logic_uScriptAct_SetVector3_TargetVector3_13;

	private bool logic_uScriptAct_SetVector3_Out_13 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_18 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_18;

	private bool logic_uScriptAct_SetBool_Out_18 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_18 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_18 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_20 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_20 = 2;

	private int logic_uScriptAct_SetInt_Target_20;

	private bool logic_uScriptAct_SetInt_Out_20 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_21 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_21;

	private bool logic_uScriptCon_CompareBool_True_21 = true;

	private bool logic_uScriptCon_CompareBool_False_21 = true;

	private uScript_GetPositionInEncounter logic_uScript_GetPositionInEncounter_uScript_GetPositionInEncounter_28 = new uScript_GetPositionInEncounter();

	private GameObject logic_uScript_GetPositionInEncounter_ownerNode_28;

	private string logic_uScript_GetPositionInEncounter_posName_28 = "";

	private Vector3 logic_uScript_GetPositionInEncounter_Return_28;

	private bool logic_uScript_GetPositionInEncounter_Out_28 = true;

	private uScript_SaveVariable logic_uScript_SaveVariable_uScript_SaveVariable_31 = new uScript_SaveVariable();

	private object logic_uScript_SaveVariable_variable_31 = "";

	private GameObject logic_uScript_SaveVariable_owner_31;

	private string logic_uScript_SaveVariable_uniqueId_31 = "PositionCached";

	private bool logic_uScript_SaveVariable_Out_31 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_36 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_36 = 1;

	private int logic_uScriptAct_SetInt_Target_36;

	private bool logic_uScriptAct_SetInt_Out_36 = true;

	private uScript_GetNamedBlock logic_uScript_GetNamedBlock_uScript_GetNamedBlock_39 = new uScript_GetNamedBlock();

	private string logic_uScript_GetNamedBlock_name_39 = "";

	private GameObject logic_uScript_GetNamedBlock_owner_39;

	private TankBlock logic_uScript_GetNamedBlock_Return_39;

	private bool logic_uScript_GetNamedBlock_Out_39 = true;

	private bool logic_uScript_GetNamedBlock_Destroyed_39 = true;

	private bool logic_uScript_GetNamedBlock_BlockExists_39 = true;

	private bool logic_uScript_GetNamedBlock_WaitingForBlock_39 = true;

	private bool logic_uScript_GetNamedBlock_NoBlock_39 = true;

	private uScript_KeepBlockInvulnerable logic_uScript_KeepBlockInvulnerable_uScript_KeepBlockInvulnerable_40 = new uScript_KeepBlockInvulnerable();

	private TankBlock logic_uScript_KeepBlockInvulnerable_block_40;

	private bool logic_uScript_KeepBlockInvulnerable_Out_40 = true;

	private uScript_GetTankBlock logic_uScript_GetTankBlock_uScript_GetTankBlock_41 = new uScript_GetTankBlock();

	private Tank logic_uScript_GetTankBlock_tank_41;

	private BlockTypes logic_uScript_GetTankBlock_blockType_41;

	private TankBlock logic_uScript_GetTankBlock_Return_41;

	private bool logic_uScript_GetTankBlock_Out_41 = true;

	private bool logic_uScript_GetTankBlock_Returned_41 = true;

	private bool logic_uScript_GetTankBlock_NotFound_41 = true;

	private uScript_SetEncounterTarget logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_44 = new uScript_SetEncounterTarget();

	private GameObject logic_uScript_SetEncounterTarget_owner_44;

	private object logic_uScript_SetEncounterTarget_visibleObject_44 = "";

	private bool logic_uScript_SetEncounterTarget_Out_44 = true;

	private uScript_SetEncounterTarget logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_49 = new uScript_SetEncounterTarget();

	private GameObject logic_uScript_SetEncounterTarget_owner_49;

	private object logic_uScript_SetEncounterTarget_visibleObject_49 = "";

	private bool logic_uScript_SetEncounterTarget_Out_49 = true;

	private uScript_MoveEncounterWithVisible logic_uScript_MoveEncounterWithVisible_uScript_MoveEncounterWithVisible_52 = new uScript_MoveEncounterWithVisible();

	private GameObject logic_uScript_MoveEncounterWithVisible_ownerNode_52;

	private object logic_uScript_MoveEncounterWithVisible_visibleObject_52 = "";

	private bool logic_uScript_MoveEncounterWithVisible_Out_52 = true;

	private uScript_SaveNamedBlock logic_uScript_SaveNamedBlock_uScript_SaveNamedBlock_55 = new uScript_SaveNamedBlock();

	private TankBlock logic_uScript_SaveNamedBlock_block_55;

	private string logic_uScript_SaveNamedBlock_uniqueName_55 = "";

	private GameObject logic_uScript_SaveNamedBlock_owner_55;

	private bool logic_uScript_SaveNamedBlock_Out_55 = true;

	private uScript_PlayerInRangeOfCurrentEncounter logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_56 = new uScript_PlayerInRangeOfCurrentEncounter();

	private GameObject logic_uScript_PlayerInRangeOfCurrentEncounter_ownerNode_56;

	private bool logic_uScript_PlayerInRangeOfCurrentEncounter_Out_56 = true;

	private bool logic_uScript_PlayerInRangeOfCurrentEncounter_True_56 = true;

	private bool logic_uScript_PlayerInRangeOfCurrentEncounter_False_56 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_57 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_57;

	private bool logic_uScriptAct_SetBool_Out_57 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_57 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_57 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_59 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_59;

	private bool logic_uScriptAct_SetBool_Out_59 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_59 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_59 = true;

	private uScript_SaveVariable logic_uScript_SaveVariable_uScript_SaveVariable_60 = new uScript_SaveVariable();

	private object logic_uScript_SaveVariable_variable_60 = "";

	private GameObject logic_uScript_SaveVariable_owner_60;

	private string logic_uScript_SaveVariable_uniqueId_60 = "Enemy Dead";

	private bool logic_uScript_SaveVariable_Out_60 = true;

	private uScript_FireParticlesTowardsGround logic_uScript_FireParticlesTowardsGround_uScript_FireParticlesTowardsGround_65 = new uScript_FireParticlesTowardsGround();

	private Vector3 logic_uScript_FireParticlesTowardsGround_groundPos_65;

	private Transform logic_uScript_FireParticlesTowardsGround_particleEffect_65;

	private GameObject logic_uScript_FireParticlesTowardsGround_owner_65;

	private string logic_uScript_FireParticlesTowardsGround_uniqueName_65 = "fireRain";

	private Transform logic_uScript_FireParticlesTowardsGround_Return_65;

	private bool logic_uScript_FireParticlesTowardsGround_Delivered_65 = true;

	private bool logic_uScript_FireParticlesTowardsGround_Out_65 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_67 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_67;

	private bool logic_uScriptCon_CompareBool_True_67 = true;

	private bool logic_uScriptCon_CompareBool_False_67 = true;

	private uScript_SaveVariable logic_uScript_SaveVariable_uScript_SaveVariable_69 = new uScript_SaveVariable();

	private object logic_uScript_SaveVariable_variable_69 = "";

	private GameObject logic_uScript_SaveVariable_owner_69;

	private string logic_uScript_SaveVariable_uniqueId_69 = "AddedQuestLog";

	private bool logic_uScript_SaveVariable_Out_69 = true;

	private uScript_LoadBool logic_uScript_LoadBool_uScript_LoadBool_70 = new uScript_LoadBool();

	private bool logic_uScript_LoadBool_data_70;

	private GameObject logic_uScript_LoadBool_owner_70;

	private string logic_uScript_LoadBool_uniqueName_70 = "Enemy Dead";

	private bool logic_uScript_LoadBool_Out_70 = true;

	private uScript_MoveEncounterWithVisible logic_uScript_MoveEncounterWithVisible_uScript_MoveEncounterWithVisible_72 = new uScript_MoveEncounterWithVisible();

	private GameObject logic_uScript_MoveEncounterWithVisible_ownerNode_72;

	private object logic_uScript_MoveEncounterWithVisible_visibleObject_72 = "";

	private bool logic_uScript_MoveEncounterWithVisible_Out_72 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_77 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_77 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_77 = true;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_77 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_79 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_79 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_79 = true;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_79 = true;

	private SubGraph_BaseBomb_ShowQuestLog logic_SubGraph_BaseBomb_ShowQuestLog_SubGraph_BaseBomb_ShowQuestLog_80 = new SubGraph_BaseBomb_ShowQuestLog();

	private bool logic_SubGraph_BaseBomb_ShowQuestLog_Flag_QuestLogShown_80;

	private uScript_DoesTankHave logic_uScript_DoesTankHave_uScript_DoesTankHave_83 = new uScript_DoesTankHave();

	private Tank logic_uScript_DoesTankHave_tank_83;

	private int logic_uScript_DoesTankHave_amountOfPieces_83 = 1;

	private BlockTypes logic_uScript_DoesTankHave_block_83;

	private bool logic_uScript_DoesTankHave_checkForAmountOnly_83;

	private bool logic_uScript_DoesTankHave_Out_83 = true;

	private bool logic_uScript_DoesTankHave_True_83 = true;

	private bool logic_uScript_DoesTankHave_False_83 = true;

	private uScript_LoadBool logic_uScript_LoadBool_uScript_LoadBool_85 = new uScript_LoadBool();

	private bool logic_uScript_LoadBool_data_85;

	private GameObject logic_uScript_LoadBool_owner_85;

	private string logic_uScript_LoadBool_uniqueName_85 = "AddedQuestLog";

	private bool logic_uScript_LoadBool_Out_85 = true;

	private uScript_LoadBool logic_uScript_LoadBool_uScript_LoadBool_94 = new uScript_LoadBool();

	private bool logic_uScript_LoadBool_data_94;

	private GameObject logic_uScript_LoadBool_owner_94;

	private string logic_uScript_LoadBool_uniqueName_94 = "PositionCached";

	private bool logic_uScript_LoadBool_Out_94 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_96 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_96;

	private bool logic_uScriptAct_SetBool_Out_96 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_96 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_96 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_97 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_97 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_97 = ManOnScreenMessages.MessagePriority.Medium;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_97 = true;

	private string logic_uScript_AddOnScreenMessage_tag_97 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_97;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_97;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_97;

	private bool logic_uScript_AddOnScreenMessage_Out_97 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_97 = true;

	private uScript_DoesPlayerHaveBlock logic_uScript_DoesPlayerHaveBlock_uScript_DoesPlayerHaveBlock_98 = new uScript_DoesPlayerHaveBlock();

	private BlockTypes logic_uScript_DoesPlayerHaveBlock_block_98;

	private bool logic_uScript_DoesPlayerHaveBlock_isDragging_98;

	private bool logic_uScript_DoesPlayerHaveBlock_Out_98 = true;

	private bool logic_uScript_DoesPlayerHaveBlock_True_98 = true;

	private bool logic_uScript_DoesPlayerHaveBlock_False_98 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_102 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_102 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_102 = ManOnScreenMessages.MessagePriority.Medium;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_102;

	private string logic_uScript_AddOnScreenMessage_tag_102 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_102;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_102;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_102;

	private bool logic_uScript_AddOnScreenMessage_Out_102 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_102 = true;

	private uScript_SaveVariable logic_uScript_SaveVariable_uScript_SaveVariable_103 = new uScript_SaveVariable();

	private object logic_uScript_SaveVariable_variable_103 = "";

	private GameObject logic_uScript_SaveVariable_owner_103;

	private string logic_uScript_SaveVariable_uniqueId_103 = "Stage";

	private bool logic_uScript_SaveVariable_Out_103 = true;

	private uScript_LoadInt logic_uScript_LoadInt_uScript_LoadInt_106 = new uScript_LoadInt();

	private int logic_uScript_LoadInt_data_106;

	private GameObject logic_uScript_LoadInt_owner_106;

	private string logic_uScript_LoadInt_uniqueName_106 = "Stage";

	private bool logic_uScript_LoadInt_Out_106 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_134 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_134 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_134 = true;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_134 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_138 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_138 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_138;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_138 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_138;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_138 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_138 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_138 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_138 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_139 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_139;

	private bool logic_uScriptAct_SetBool_Out_139 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_139 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_139 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_143 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_143 = new Tank[0];

	private int logic_uScript_AccessListTech_index_143;

	private Tank logic_uScript_AccessListTech_value_143;

	private bool logic_uScript_AccessListTech_Out_143 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_144 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_144 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_144;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_144 = 0.3f;

	private bool logic_uScript_SpawnTechsFromData_Out_144 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_145 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_145;

	private bool logic_uScriptAct_SetBool_Out_145 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_145 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_145 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_147 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_147;

	private bool logic_uScriptCon_CompareBool_True_147 = true;

	private bool logic_uScriptCon_CompareBool_False_147 = true;

	private uScript_SaveVariable logic_uScript_SaveVariable_uScript_SaveVariable_149 = new uScript_SaveVariable();

	private object logic_uScript_SaveVariable_variable_149 = "";

	private GameObject logic_uScript_SaveVariable_owner_149;

	private string logic_uScript_SaveVariable_uniqueId_149 = "EnemySpawned";

	private bool logic_uScript_SaveVariable_Out_149 = true;

	private uScript_LoadBool logic_uScript_LoadBool_uScript_LoadBool_153 = new uScript_LoadBool();

	private bool logic_uScript_LoadBool_data_153;

	private GameObject logic_uScript_LoadBool_owner_153;

	private string logic_uScript_LoadBool_uniqueName_153 = "EnemySpawned";

	private bool logic_uScript_LoadBool_Out_153 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_156 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_156;

	private bool logic_uScriptAct_SetBool_Out_156 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_156 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_156 = true;

	private uScript_SetTankInvulnerable logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_157 = new uScript_SetTankInvulnerable();

	private bool logic_uScript_SetTankInvulnerable_invulnerable_157 = true;

	private Tank logic_uScript_SetTankInvulnerable_tank_157;

	private bool logic_uScript_SetTankInvulnerable_Out_157 = true;

	private uScript_SetTankInvulnerable logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_160 = new uScript_SetTankInvulnerable();

	private bool logic_uScript_SetTankInvulnerable_invulnerable_160;

	private Tank logic_uScript_SetTankInvulnerable_tank_160;

	private bool logic_uScript_SetTankInvulnerable_Out_160 = true;

	private uScript_SpawnBlockAbovePlayer logic_uScript_SpawnBlockAbovePlayer_uScript_SpawnBlockAbovePlayer_161 = new uScript_SpawnBlockAbovePlayer();

	private BlockTypes logic_uScript_SpawnBlockAbovePlayer_blockType_161;

	private string logic_uScript_SpawnBlockAbovePlayer_uniqueName_161 = "";

	private GameObject logic_uScript_SpawnBlockAbovePlayer_owner_161;

	private TankBlock logic_uScript_SpawnBlockAbovePlayer_Return_161;

	private bool logic_uScript_SpawnBlockAbovePlayer_Out_161 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_166 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_166 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_166 = ManOnScreenMessages.MessagePriority.Medium;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_166;

	private string logic_uScript_AddOnScreenMessage_tag_166 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_166;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_166;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_166;

	private bool logic_uScript_AddOnScreenMessage_Out_166 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_166 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_168 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_168 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_168 = true;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_168 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_172 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_172 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_172 = true;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_172 = true;

	private uScript_PlayerInRangeOfCurrentEncounter logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_175 = new uScript_PlayerInRangeOfCurrentEncounter();

	private GameObject logic_uScript_PlayerInRangeOfCurrentEncounter_ownerNode_175;

	private bool logic_uScript_PlayerInRangeOfCurrentEncounter_Out_175 = true;

	private bool logic_uScript_PlayerInRangeOfCurrentEncounter_True_175 = true;

	private bool logic_uScript_PlayerInRangeOfCurrentEncounter_False_175 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_177 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_177;

	private bool logic_uScriptCon_CompareBool_True_177 = true;

	private bool logic_uScriptCon_CompareBool_False_177 = true;

	private uScript_BlankNode logic_uScript_BlankNode_uScript_BlankNode_178 = new uScript_BlankNode();

	private bool logic_uScript_BlankNode_Out_178 = true;

	private uScript_BlankNode logic_uScript_BlankNode_uScript_BlankNode_179 = new uScript_BlankNode();

	private bool logic_uScript_BlankNode_Out_179 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_184 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_184 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_184 = ManOnScreenMessages.MessagePriority.Medium;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_184 = true;

	private string logic_uScript_AddOnScreenMessage_tag_184 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_184;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_184;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_184;

	private bool logic_uScript_AddOnScreenMessage_Out_184 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_184 = true;

	private uScript_SetTankInvulnerable logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_186 = new uScript_SetTankInvulnerable();

	private bool logic_uScript_SetTankInvulnerable_invulnerable_186;

	private Tank logic_uScript_SetTankInvulnerable_tank_186;

	private bool logic_uScript_SetTankInvulnerable_Out_186 = true;

	private uScript_FinishEncounter logic_uScript_FinishEncounter_uScript_FinishEncounter_188 = new uScript_FinishEncounter();

	private GameObject logic_uScript_FinishEncounter_owner_188;

	private bool logic_uScript_FinishEncounter_Out_188 = true;

	private uScript_FinishEncounter logic_uScript_FinishEncounter_uScript_FinishEncounter_190 = new uScript_FinishEncounter();

	private GameObject logic_uScript_FinishEncounter_owner_190;

	private bool logic_uScript_FinishEncounter_Out_190 = true;

	private void SyncUnityHooks()
	{
		SyncEventListeners();
		if (null == owner_Connection_5 || !m_RegisteredForEvents)
		{
			owner_Connection_5 = parentGameObject;
		}
		if (null == owner_Connection_9 || !m_RegisteredForEvents)
		{
			owner_Connection_9 = parentGameObject;
		}
		if (null == owner_Connection_12 || !m_RegisteredForEvents)
		{
			owner_Connection_12 = parentGameObject;
		}
		if (null == owner_Connection_19 || !m_RegisteredForEvents)
		{
			owner_Connection_19 = parentGameObject;
		}
		if (null == owner_Connection_23 || !m_RegisteredForEvents)
		{
			owner_Connection_23 = parentGameObject;
		}
		if (null == owner_Connection_29 || !m_RegisteredForEvents)
		{
			owner_Connection_29 = parentGameObject;
		}
		if (null == owner_Connection_42 || !m_RegisteredForEvents)
		{
			owner_Connection_42 = parentGameObject;
		}
		if (null == owner_Connection_46 || !m_RegisteredForEvents)
		{
			owner_Connection_46 = parentGameObject;
		}
		if (null == owner_Connection_50 || !m_RegisteredForEvents)
		{
			owner_Connection_50 = parentGameObject;
		}
		if (null == owner_Connection_51 || !m_RegisteredForEvents)
		{
			owner_Connection_51 = parentGameObject;
		}
		if (null == owner_Connection_63 || !m_RegisteredForEvents)
		{
			owner_Connection_63 = parentGameObject;
		}
		if (null == owner_Connection_66 || !m_RegisteredForEvents)
		{
			owner_Connection_66 = parentGameObject;
			if (null != owner_Connection_66)
			{
				uScript_SaveLoad uScript_SaveLoad2 = owner_Connection_66.GetComponent<uScript_SaveLoad>();
				if (null == uScript_SaveLoad2)
				{
					uScript_SaveLoad2 = owner_Connection_66.AddComponent<uScript_SaveLoad>();
				}
				if (null != uScript_SaveLoad2)
				{
					uScript_SaveLoad2.SaveEvent += Instance_SaveEvent_93;
					uScript_SaveLoad2.LoadEvent += Instance_LoadEvent_93;
					uScript_SaveLoad2.RestartEvent += Instance_RestartEvent_93;
				}
			}
		}
		if (null == owner_Connection_73 || !m_RegisteredForEvents)
		{
			owner_Connection_73 = parentGameObject;
		}
		if (null == owner_Connection_82 || !m_RegisteredForEvents)
		{
			owner_Connection_82 = parentGameObject;
		}
		if (null == owner_Connection_84 || !m_RegisteredForEvents)
		{
			owner_Connection_84 = parentGameObject;
		}
		if (null == owner_Connection_86 || !m_RegisteredForEvents)
		{
			owner_Connection_86 = parentGameObject;
		}
		if (null == owner_Connection_105 || !m_RegisteredForEvents)
		{
			owner_Connection_105 = parentGameObject;
		}
		if (null == owner_Connection_108 || !m_RegisteredForEvents)
		{
			owner_Connection_108 = parentGameObject;
		}
		if (null == owner_Connection_136 || !m_RegisteredForEvents)
		{
			owner_Connection_136 = parentGameObject;
			if (null != owner_Connection_136)
			{
				uScript_EncounterUpdate uScript_EncounterUpdate2 = owner_Connection_136.GetComponent<uScript_EncounterUpdate>();
				if (null == uScript_EncounterUpdate2)
				{
					uScript_EncounterUpdate2 = owner_Connection_136.AddComponent<uScript_EncounterUpdate>();
				}
				if (null != uScript_EncounterUpdate2)
				{
					uScript_EncounterUpdate2.OnUpdate += Instance_OnUpdate_137;
					uScript_EncounterUpdate2.OnSuspend += Instance_OnSuspend_137;
					uScript_EncounterUpdate2.OnResume += Instance_OnResume_137;
				}
			}
		}
		if (null == owner_Connection_140 || !m_RegisteredForEvents)
		{
			owner_Connection_140 = parentGameObject;
		}
		if (null == owner_Connection_146 || !m_RegisteredForEvents)
		{
			owner_Connection_146 = parentGameObject;
		}
		if (null == owner_Connection_150 || !m_RegisteredForEvents)
		{
			owner_Connection_150 = parentGameObject;
		}
		if (null == owner_Connection_154 || !m_RegisteredForEvents)
		{
			owner_Connection_154 = parentGameObject;
		}
		if (null == owner_Connection_162 || !m_RegisteredForEvents)
		{
			owner_Connection_162 = parentGameObject;
		}
		if (null == owner_Connection_174 || !m_RegisteredForEvents)
		{
			owner_Connection_174 = parentGameObject;
		}
		if (null == owner_Connection_187 || !m_RegisteredForEvents)
		{
			owner_Connection_187 = parentGameObject;
		}
		if (null == owner_Connection_189 || !m_RegisteredForEvents)
		{
			owner_Connection_189 = parentGameObject;
		}
	}

	private void RegisterForUnityHooks()
	{
		SyncEventListeners();
		if (!m_RegisteredForEvents && null != owner_Connection_66)
		{
			uScript_SaveLoad uScript_SaveLoad2 = owner_Connection_66.GetComponent<uScript_SaveLoad>();
			if (null == uScript_SaveLoad2)
			{
				uScript_SaveLoad2 = owner_Connection_66.AddComponent<uScript_SaveLoad>();
			}
			if (null != uScript_SaveLoad2)
			{
				uScript_SaveLoad2.SaveEvent += Instance_SaveEvent_93;
				uScript_SaveLoad2.LoadEvent += Instance_LoadEvent_93;
				uScript_SaveLoad2.RestartEvent += Instance_RestartEvent_93;
			}
		}
		if (!m_RegisteredForEvents && null != owner_Connection_136)
		{
			uScript_EncounterUpdate uScript_EncounterUpdate2 = owner_Connection_136.GetComponent<uScript_EncounterUpdate>();
			if (null == uScript_EncounterUpdate2)
			{
				uScript_EncounterUpdate2 = owner_Connection_136.AddComponent<uScript_EncounterUpdate>();
			}
			if (null != uScript_EncounterUpdate2)
			{
				uScript_EncounterUpdate2.OnUpdate += Instance_OnUpdate_137;
				uScript_EncounterUpdate2.OnSuspend += Instance_OnSuspend_137;
				uScript_EncounterUpdate2.OnResume += Instance_OnResume_137;
			}
		}
	}

	private void SyncEventListeners()
	{
	}

	private void UnregisterEventListeners()
	{
		if (null != owner_Connection_66)
		{
			uScript_SaveLoad component = owner_Connection_66.GetComponent<uScript_SaveLoad>();
			if (null != component)
			{
				component.SaveEvent -= Instance_SaveEvent_93;
				component.LoadEvent -= Instance_LoadEvent_93;
				component.RestartEvent -= Instance_RestartEvent_93;
			}
		}
		if (null != owner_Connection_136)
		{
			uScript_EncounterUpdate component2 = owner_Connection_136.GetComponent<uScript_EncounterUpdate>();
			if (null != component2)
			{
				component2.OnUpdate -= Instance_OnUpdate_137;
				component2.OnSuspend -= Instance_OnSuspend_137;
				component2.OnResume -= Instance_OnResume_137;
			}
		}
	}

	public override void SetParent(GameObject g)
	{
		parentGameObject = g;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_1.SetParent(g);
		logic_uScript_AI_SetPOI_uScript_AI_SetPOI_7.SetParent(g);
		logic_uScriptAct_SetVector3_uScriptAct_SetVector3_13.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_18.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_20.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_21.SetParent(g);
		logic_uScript_GetPositionInEncounter_uScript_GetPositionInEncounter_28.SetParent(g);
		logic_uScript_SaveVariable_uScript_SaveVariable_31.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_36.SetParent(g);
		logic_uScript_GetNamedBlock_uScript_GetNamedBlock_39.SetParent(g);
		logic_uScript_KeepBlockInvulnerable_uScript_KeepBlockInvulnerable_40.SetParent(g);
		logic_uScript_GetTankBlock_uScript_GetTankBlock_41.SetParent(g);
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_44.SetParent(g);
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_49.SetParent(g);
		logic_uScript_MoveEncounterWithVisible_uScript_MoveEncounterWithVisible_52.SetParent(g);
		logic_uScript_SaveNamedBlock_uScript_SaveNamedBlock_55.SetParent(g);
		logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_56.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_57.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_59.SetParent(g);
		logic_uScript_SaveVariable_uScript_SaveVariable_60.SetParent(g);
		logic_uScript_FireParticlesTowardsGround_uScript_FireParticlesTowardsGround_65.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_67.SetParent(g);
		logic_uScript_SaveVariable_uScript_SaveVariable_69.SetParent(g);
		logic_uScript_LoadBool_uScript_LoadBool_70.SetParent(g);
		logic_uScript_MoveEncounterWithVisible_uScript_MoveEncounterWithVisible_72.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_77.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_79.SetParent(g);
		logic_SubGraph_BaseBomb_ShowQuestLog_SubGraph_BaseBomb_ShowQuestLog_80.SetParent(g);
		logic_uScript_DoesTankHave_uScript_DoesTankHave_83.SetParent(g);
		logic_uScript_LoadBool_uScript_LoadBool_85.SetParent(g);
		logic_uScript_LoadBool_uScript_LoadBool_94.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_96.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_97.SetParent(g);
		logic_uScript_DoesPlayerHaveBlock_uScript_DoesPlayerHaveBlock_98.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_102.SetParent(g);
		logic_uScript_SaveVariable_uScript_SaveVariable_103.SetParent(g);
		logic_uScript_LoadInt_uScript_LoadInt_106.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_134.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_138.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_139.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_143.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_144.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_145.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_147.SetParent(g);
		logic_uScript_SaveVariable_uScript_SaveVariable_149.SetParent(g);
		logic_uScript_LoadBool_uScript_LoadBool_153.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_156.SetParent(g);
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_157.SetParent(g);
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_160.SetParent(g);
		logic_uScript_SpawnBlockAbovePlayer_uScript_SpawnBlockAbovePlayer_161.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_166.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_168.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_172.SetParent(g);
		logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_175.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_177.SetParent(g);
		logic_uScript_BlankNode_uScript_BlankNode_178.SetParent(g);
		logic_uScript_BlankNode_uScript_BlankNode_179.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_184.SetParent(g);
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_186.SetParent(g);
		logic_uScript_FinishEncounter_uScript_FinishEncounter_188.SetParent(g);
		logic_uScript_FinishEncounter_uScript_FinishEncounter_190.SetParent(g);
		owner_Connection_5 = parentGameObject;
		owner_Connection_9 = parentGameObject;
		owner_Connection_12 = parentGameObject;
		owner_Connection_19 = parentGameObject;
		owner_Connection_23 = parentGameObject;
		owner_Connection_29 = parentGameObject;
		owner_Connection_42 = parentGameObject;
		owner_Connection_46 = parentGameObject;
		owner_Connection_50 = parentGameObject;
		owner_Connection_51 = parentGameObject;
		owner_Connection_63 = parentGameObject;
		owner_Connection_66 = parentGameObject;
		owner_Connection_73 = parentGameObject;
		owner_Connection_82 = parentGameObject;
		owner_Connection_84 = parentGameObject;
		owner_Connection_86 = parentGameObject;
		owner_Connection_105 = parentGameObject;
		owner_Connection_108 = parentGameObject;
		owner_Connection_136 = parentGameObject;
		owner_Connection_140 = parentGameObject;
		owner_Connection_146 = parentGameObject;
		owner_Connection_150 = parentGameObject;
		owner_Connection_154 = parentGameObject;
		owner_Connection_162 = parentGameObject;
		owner_Connection_174 = parentGameObject;
		owner_Connection_187 = parentGameObject;
		owner_Connection_189 = parentGameObject;
	}

	public void Awake()
	{
		logic_SubGraph_BaseBomb_ShowQuestLog_SubGraph_BaseBomb_ShowQuestLog_80.Awake();
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_1.Output1 += uScriptCon_ManualSwitch_Output1_1;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_1.Output2 += uScriptCon_ManualSwitch_Output2_1;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_1.Output3 += uScriptCon_ManualSwitch_Output3_1;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_1.Output4 += uScriptCon_ManualSwitch_Output4_1;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_1.Output5 += uScriptCon_ManualSwitch_Output5_1;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_1.Output6 += uScriptCon_ManualSwitch_Output6_1;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_1.Output7 += uScriptCon_ManualSwitch_Output7_1;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_1.Output8 += uScriptCon_ManualSwitch_Output8_1;
		logic_SubGraph_BaseBomb_ShowQuestLog_SubGraph_BaseBomb_ShowQuestLog_80.Out += SubGraph_BaseBomb_ShowQuestLog_Out_80;
	}

	public void Start()
	{
		SyncUnityHooks();
		m_RegisteredForEvents = true;
		logic_SubGraph_BaseBomb_ShowQuestLog_SubGraph_BaseBomb_ShowQuestLog_80.Start();
	}

	public void OnEnable()
	{
		RegisterForUnityHooks();
		m_RegisteredForEvents = true;
		logic_SubGraph_BaseBomb_ShowQuestLog_SubGraph_BaseBomb_ShowQuestLog_80.OnEnable();
	}

	public void OnDisable()
	{
		logic_uScript_GetNamedBlock_uScript_GetNamedBlock_39.OnDisable();
		logic_uScript_SaveNamedBlock_uScript_SaveNamedBlock_55.OnDisable();
		logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_56.OnDisable();
		logic_uScript_FireParticlesTowardsGround_uScript_FireParticlesTowardsGround_65.OnDisable();
		logic_uScript_LoadBool_uScript_LoadBool_70.OnDisable();
		logic_SubGraph_BaseBomb_ShowQuestLog_SubGraph_BaseBomb_ShowQuestLog_80.OnDisable();
		logic_uScript_LoadBool_uScript_LoadBool_85.OnDisable();
		logic_uScript_LoadBool_uScript_LoadBool_94.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_97.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_102.OnDisable();
		logic_uScript_LoadInt_uScript_LoadInt_106.OnDisable();
		logic_uScript_LoadBool_uScript_LoadBool_153.OnDisable();
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_157.OnDisable();
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_160.OnDisable();
		logic_uScript_SpawnBlockAbovePlayer_uScript_SpawnBlockAbovePlayer_161.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_166.OnDisable();
		logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_175.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_184.OnDisable();
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_186.OnDisable();
		UnregisterEventListeners();
		m_RegisteredForEvents = false;
	}

	public void Update()
	{
		SyncEventListeners();
		logic_SubGraph_BaseBomb_ShowQuestLog_SubGraph_BaseBomb_ShowQuestLog_80.Update();
	}

	public void OnDestroy()
	{
		logic_SubGraph_BaseBomb_ShowQuestLog_SubGraph_BaseBomb_ShowQuestLog_80.OnDestroy();
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_1.Output1 -= uScriptCon_ManualSwitch_Output1_1;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_1.Output2 -= uScriptCon_ManualSwitch_Output2_1;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_1.Output3 -= uScriptCon_ManualSwitch_Output3_1;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_1.Output4 -= uScriptCon_ManualSwitch_Output4_1;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_1.Output5 -= uScriptCon_ManualSwitch_Output5_1;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_1.Output6 -= uScriptCon_ManualSwitch_Output6_1;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_1.Output7 -= uScriptCon_ManualSwitch_Output7_1;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_1.Output8 -= uScriptCon_ManualSwitch_Output8_1;
		logic_SubGraph_BaseBomb_ShowQuestLog_SubGraph_BaseBomb_ShowQuestLog_80.Out -= SubGraph_BaseBomb_ShowQuestLog_Out_80;
	}

	private void Instance_SaveEvent_93(object o, EventArgs e)
	{
		Relay_SaveEvent_93();
	}

	private void Instance_LoadEvent_93(object o, EventArgs e)
	{
		Relay_LoadEvent_93();
	}

	private void Instance_RestartEvent_93(object o, EventArgs e)
	{
		Relay_RestartEvent_93();
	}

	private void Instance_OnUpdate_137(object o, EventArgs e)
	{
		Relay_OnUpdate_137();
	}

	private void Instance_OnSuspend_137(object o, EventArgs e)
	{
		Relay_OnSuspend_137();
	}

	private void Instance_OnResume_137(object o, EventArgs e)
	{
		Relay_OnResume_137();
	}

	private void uScriptCon_ManualSwitch_Output1_1(object o, EventArgs e)
	{
		Relay_Output1_1();
	}

	private void uScriptCon_ManualSwitch_Output2_1(object o, EventArgs e)
	{
		Relay_Output2_1();
	}

	private void uScriptCon_ManualSwitch_Output3_1(object o, EventArgs e)
	{
		Relay_Output3_1();
	}

	private void uScriptCon_ManualSwitch_Output4_1(object o, EventArgs e)
	{
		Relay_Output4_1();
	}

	private void uScriptCon_ManualSwitch_Output5_1(object o, EventArgs e)
	{
		Relay_Output5_1();
	}

	private void uScriptCon_ManualSwitch_Output6_1(object o, EventArgs e)
	{
		Relay_Output6_1();
	}

	private void uScriptCon_ManualSwitch_Output7_1(object o, EventArgs e)
	{
		Relay_Output7_1();
	}

	private void uScriptCon_ManualSwitch_Output8_1(object o, EventArgs e)
	{
		Relay_Output8_1();
	}

	private void SubGraph_BaseBomb_ShowQuestLog_Out_80(object o, SubGraph_BaseBomb_ShowQuestLog.LogicEventArgs e)
	{
		logic_SubGraph_BaseBomb_ShowQuestLog_Flag_QuestLogShown_80 = e.Flag_QuestLogShown;
		local_QuestLogShown_System_Boolean = logic_SubGraph_BaseBomb_ShowQuestLog_Flag_QuestLogShown_80;
		Relay_Out_80();
	}

	private void Relay_Output1_1()
	{
		Relay_In_49();
	}

	private void Relay_Output2_1()
	{
		Relay_In_44();
	}

	private void Relay_Output3_1()
	{
	}

	private void Relay_Output4_1()
	{
	}

	private void Relay_Output5_1()
	{
	}

	private void Relay_Output6_1()
	{
	}

	private void Relay_Output7_1()
	{
	}

	private void Relay_Output8_1()
	{
	}

	private void Relay_In_1()
	{
		logic_uScriptCon_ManualSwitch_CurrentOutput_1 = local_Stage_System_Int32;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_1.In(logic_uScriptCon_ManualSwitch_CurrentOutput_1);
	}

	private void Relay_In_7()
	{
		logic_uScript_AI_SetPOI_tank_7 = local_Robber_Tank;
		logic_uScript_AI_SetPOI_position_7 = local_InitialPos_UnityEngine_Vector3;
		logic_uScript_AI_SetPOI_uScript_AI_SetPOI_7.In(logic_uScript_AI_SetPOI_tank_7, logic_uScript_AI_SetPOI_usePOI_7, logic_uScript_AI_SetPOI_position_7, logic_uScript_AI_SetPOI_distance_7);
		if (logic_uScript_AI_SetPOI_uScript_AI_SetPOI_7.Out)
		{
			Relay_In_39();
		}
	}

	private void Relay_In_13()
	{
		logic_uScriptAct_SetVector3_Value_13 = local_EncounterPos_UnityEngine_Vector3;
		logic_uScriptAct_SetVector3_uScriptAct_SetVector3_13.In(logic_uScriptAct_SetVector3_Value_13, out logic_uScriptAct_SetVector3_TargetVector3_13);
		local_InitialPos_UnityEngine_Vector3 = logic_uScriptAct_SetVector3_TargetVector3_13;
		if (logic_uScriptAct_SetVector3_uScriptAct_SetVector3_13.Out)
		{
			Relay_True_59();
		}
	}

	private void Relay_True_18()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_18.True(out logic_uScriptAct_SetBool_Target_18);
		local_Enemy_Dead_System_Boolean = logic_uScriptAct_SetBool_Target_18;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_18.Out)
		{
			Relay_False_96();
		}
	}

	private void Relay_False_18()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_18.False(out logic_uScriptAct_SetBool_Target_18);
		local_Enemy_Dead_System_Boolean = logic_uScriptAct_SetBool_Target_18;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_18.Out)
		{
			Relay_False_96();
		}
	}

	private void Relay_In_20()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_20.In(logic_uScriptAct_SetInt_Value_20, out logic_uScriptAct_SetInt_Target_20);
		local_Stage_System_Int32 = logic_uScriptAct_SetInt_Target_20;
	}

	private void Relay_In_21()
	{
		logic_uScriptCon_CompareBool_Bool_21 = local_Enemy_Dead_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_21.In(logic_uScriptCon_CompareBool_Bool_21);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_21.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_21.False;
		if (num)
		{
			Relay_In_79();
		}
		if (flag)
		{
			Relay_In_160();
		}
	}

	private void Relay_In_28()
	{
		logic_uScript_GetPositionInEncounter_ownerNode_28 = owner_Connection_82;
		logic_uScript_GetPositionInEncounter_posName_28 = PositionId;
		logic_uScript_GetPositionInEncounter_Return_28 = logic_uScript_GetPositionInEncounter_uScript_GetPositionInEncounter_28.In(logic_uScript_GetPositionInEncounter_ownerNode_28, logic_uScript_GetPositionInEncounter_posName_28);
		local_EncounterPos_UnityEngine_Vector3 = logic_uScript_GetPositionInEncounter_Return_28;
		if (logic_uScript_GetPositionInEncounter_uScript_GetPositionInEncounter_28.Out)
		{
			Relay_In_67();
		}
	}

	private void Relay_In_31()
	{
		logic_uScript_SaveVariable_variable_31 = local_PositionCached_System_Boolean;
		logic_uScript_SaveVariable_owner_31 = owner_Connection_84;
		logic_uScript_SaveVariable_uScript_SaveVariable_31.In(logic_uScript_SaveVariable_variable_31, logic_uScript_SaveVariable_owner_31, logic_uScript_SaveVariable_uniqueId_31);
		if (logic_uScript_SaveVariable_uScript_SaveVariable_31.Out)
		{
			Relay_In_60();
		}
	}

	private void Relay_In_36()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_36.In(logic_uScriptAct_SetInt_Value_36, out logic_uScriptAct_SetInt_Target_36);
		local_Stage_System_Int32 = logic_uScriptAct_SetInt_Target_36;
		if (logic_uScriptAct_SetInt_uScriptAct_SetInt_36.Out)
		{
			Relay_False_57();
		}
	}

	private void Relay_In_39()
	{
		logic_uScript_GetNamedBlock_name_39 = local_BlockName_System_String;
		logic_uScript_GetNamedBlock_owner_39 = owner_Connection_23;
		logic_uScript_GetNamedBlock_Return_39 = logic_uScript_GetNamedBlock_uScript_GetNamedBlock_39.In(logic_uScript_GetNamedBlock_name_39, logic_uScript_GetNamedBlock_owner_39);
		local_ShieldBlock_TankBlock = logic_uScript_GetNamedBlock_Return_39;
		bool destroyed = logic_uScript_GetNamedBlock_uScript_GetNamedBlock_39.Destroyed;
		bool blockExists = logic_uScript_GetNamedBlock_uScript_GetNamedBlock_39.BlockExists;
		bool waitingForBlock = logic_uScript_GetNamedBlock_uScript_GetNamedBlock_39.WaitingForBlock;
		bool noBlock = logic_uScript_GetNamedBlock_uScript_GetNamedBlock_39.NoBlock;
		if (destroyed)
		{
			Relay_In_168();
		}
		if (blockExists)
		{
			Relay_In_178();
		}
		if (waitingForBlock)
		{
			Relay_In_179();
		}
		if (noBlock)
		{
			Relay_In_41();
		}
	}

	private void Relay_In_40()
	{
		logic_uScript_KeepBlockInvulnerable_block_40 = local_ShieldBlock_TankBlock;
		logic_uScript_KeepBlockInvulnerable_uScript_KeepBlockInvulnerable_40.In(logic_uScript_KeepBlockInvulnerable_block_40);
		if (logic_uScript_KeepBlockInvulnerable_uScript_KeepBlockInvulnerable_40.Out)
		{
			Relay_In_80();
		}
	}

	private void Relay_In_41()
	{
		logic_uScript_GetTankBlock_tank_41 = local_Robber_Tank;
		logic_uScript_GetTankBlock_blockType_41 = local_ShieldBlockType_BlockTypes;
		logic_uScript_GetTankBlock_Return_41 = logic_uScript_GetTankBlock_uScript_GetTankBlock_41.In(logic_uScript_GetTankBlock_tank_41, logic_uScript_GetTankBlock_blockType_41);
		local_ShieldBlock_TankBlock = logic_uScript_GetTankBlock_Return_41;
		if (logic_uScript_GetTankBlock_uScript_GetTankBlock_41.Returned)
		{
			Relay_In_55();
		}
	}

	private void Relay_In_44()
	{
		logic_uScript_SetEncounterTarget_owner_44 = owner_Connection_51;
		logic_uScript_SetEncounterTarget_visibleObject_44 = local_ShieldBlock_TankBlock;
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_44.In(logic_uScript_SetEncounterTarget_owner_44, logic_uScript_SetEncounterTarget_visibleObject_44);
		if (logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_44.Out)
		{
			Relay_In_72();
		}
	}

	private void Relay_In_49()
	{
		logic_uScript_SetEncounterTarget_owner_49 = owner_Connection_86;
		logic_uScript_SetEncounterTarget_visibleObject_49 = local_Robber_Tank;
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_49.In(logic_uScript_SetEncounterTarget_owner_49, logic_uScript_SetEncounterTarget_visibleObject_49);
		if (logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_49.Out)
		{
			Relay_In_52();
		}
	}

	private void Relay_In_52()
	{
		logic_uScript_MoveEncounterWithVisible_ownerNode_52 = owner_Connection_50;
		logic_uScript_MoveEncounterWithVisible_visibleObject_52 = local_Robber_Tank;
		logic_uScript_MoveEncounterWithVisible_uScript_MoveEncounterWithVisible_52.In(logic_uScript_MoveEncounterWithVisible_ownerNode_52, logic_uScript_MoveEncounterWithVisible_visibleObject_52);
		if (logic_uScript_MoveEncounterWithVisible_uScript_MoveEncounterWithVisible_52.Out)
		{
			Relay_In_56();
		}
	}

	private void Relay_In_55()
	{
		logic_uScript_SaveNamedBlock_block_55 = local_ShieldBlock_TankBlock;
		logic_uScript_SaveNamedBlock_uniqueName_55 = local_BlockName_System_String;
		logic_uScript_SaveNamedBlock_owner_55 = owner_Connection_5;
		logic_uScript_SaveNamedBlock_uScript_SaveNamedBlock_55.In(logic_uScript_SaveNamedBlock_block_55, logic_uScript_SaveNamedBlock_uniqueName_55, logic_uScript_SaveNamedBlock_owner_55);
		if (logic_uScript_SaveNamedBlock_uScript_SaveNamedBlock_55.Out)
		{
			Relay_In_40();
		}
	}

	private void Relay_In_56()
	{
		logic_uScript_PlayerInRangeOfCurrentEncounter_ownerNode_56 = owner_Connection_42;
		logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_56.In(logic_uScript_PlayerInRangeOfCurrentEncounter_ownerNode_56);
		bool num = logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_56.True;
		bool flag = logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_56.False;
		if (num)
		{
			Relay_In_21();
		}
		if (flag)
		{
			Relay_In_77();
		}
	}

	private void Relay_True_57()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_57.True(out logic_uScriptAct_SetBool_Target_57);
		local_PositionCached_System_Boolean = logic_uScriptAct_SetBool_Target_57;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_57.Out)
		{
			Relay_False_18();
		}
	}

	private void Relay_False_57()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_57.False(out logic_uScriptAct_SetBool_Target_57);
		local_PositionCached_System_Boolean = logic_uScriptAct_SetBool_Target_57;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_57.Out)
		{
			Relay_False_18();
		}
	}

	private void Relay_True_59()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_59.True(out logic_uScriptAct_SetBool_Target_59);
		local_PositionCached_System_Boolean = logic_uScriptAct_SetBool_Target_59;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_59.Out)
		{
			Relay_In_65();
		}
	}

	private void Relay_False_59()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_59.False(out logic_uScriptAct_SetBool_Target_59);
		local_PositionCached_System_Boolean = logic_uScriptAct_SetBool_Target_59;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_59.Out)
		{
			Relay_In_65();
		}
	}

	private void Relay_In_60()
	{
		logic_uScript_SaveVariable_variable_60 = local_Enemy_Dead_System_Boolean;
		logic_uScript_SaveVariable_owner_60 = owner_Connection_73;
		logic_uScript_SaveVariable_uScript_SaveVariable_60.In(logic_uScript_SaveVariable_variable_60, logic_uScript_SaveVariable_owner_60, logic_uScript_SaveVariable_uniqueId_60);
		if (logic_uScript_SaveVariable_uScript_SaveVariable_60.Out)
		{
			Relay_In_69();
		}
	}

	private void Relay_In_65()
	{
		logic_uScript_FireParticlesTowardsGround_groundPos_65 = local_EncounterPos_UnityEngine_Vector3;
		logic_uScript_FireParticlesTowardsGround_particleEffect_65 = Particles;
		logic_uScript_FireParticlesTowardsGround_owner_65 = owner_Connection_46;
		logic_uScript_FireParticlesTowardsGround_Return_65 = logic_uScript_FireParticlesTowardsGround_uScript_FireParticlesTowardsGround_65.In(logic_uScript_FireParticlesTowardsGround_groundPos_65, logic_uScript_FireParticlesTowardsGround_particleEffect_65, logic_uScript_FireParticlesTowardsGround_owner_65, logic_uScript_FireParticlesTowardsGround_uniqueName_65);
		if (logic_uScript_FireParticlesTowardsGround_uScript_FireParticlesTowardsGround_65.Delivered)
		{
			Relay_In_147();
		}
	}

	private void Relay_In_67()
	{
		logic_uScriptCon_CompareBool_Bool_67 = local_PositionCached_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_67.In(logic_uScriptCon_CompareBool_Bool_67);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_67.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_67.False;
		if (num)
		{
			Relay_In_65();
		}
		if (flag)
		{
			Relay_In_13();
		}
	}

	private void Relay_In_69()
	{
		logic_uScript_SaveVariable_variable_69 = local_QuestLogShown_System_Boolean;
		logic_uScript_SaveVariable_owner_69 = owner_Connection_12;
		logic_uScript_SaveVariable_uScript_SaveVariable_69.In(logic_uScript_SaveVariable_variable_69, logic_uScript_SaveVariable_owner_69, logic_uScript_SaveVariable_uniqueId_69);
		if (logic_uScript_SaveVariable_uScript_SaveVariable_69.Out)
		{
			Relay_In_149();
		}
	}

	private void Relay_In_70()
	{
		logic_uScript_LoadBool_data_70 = local_Enemy_Dead_System_Boolean;
		logic_uScript_LoadBool_owner_70 = owner_Connection_29;
		logic_uScript_LoadBool_uScript_LoadBool_70.In(ref logic_uScript_LoadBool_data_70, logic_uScript_LoadBool_owner_70, logic_uScript_LoadBool_uniqueName_70);
		local_Enemy_Dead_System_Boolean = logic_uScript_LoadBool_data_70;
		if (logic_uScript_LoadBool_uScript_LoadBool_70.Out)
		{
			Relay_In_85();
		}
	}

	private void Relay_In_72()
	{
		logic_uScript_MoveEncounterWithVisible_ownerNode_72 = owner_Connection_63;
		logic_uScript_MoveEncounterWithVisible_visibleObject_72 = local_ShieldBlock_TankBlock;
		logic_uScript_MoveEncounterWithVisible_uScript_MoveEncounterWithVisible_72.In(logic_uScript_MoveEncounterWithVisible_ownerNode_72, logic_uScript_MoveEncounterWithVisible_visibleObject_72);
		if (logic_uScript_MoveEncounterWithVisible_uScript_MoveEncounterWithVisible_72.Out)
		{
			Relay_In_175();
		}
	}

	private void Relay_In_77()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_77 = local_msgShield_System_String;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_77.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_77, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_77);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_77.Out)
		{
			Relay_In_177();
		}
	}

	private void Relay_In_79()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_79 = local_msgShield_System_String;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_79.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_79, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_79);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_79.Out)
		{
			Relay_In_20();
		}
	}

	private void Relay_Out_80()
	{
		Relay_In_98();
	}

	private void Relay_In_80()
	{
		logic_SubGraph_BaseBomb_ShowQuestLog_Flag_QuestLogShown_80 = local_QuestLogShown_System_Boolean;
		logic_SubGraph_BaseBomb_ShowQuestLog_SubGraph_BaseBomb_ShowQuestLog_80.In(ref logic_SubGraph_BaseBomb_ShowQuestLog_Flag_QuestLogShown_80);
	}

	private void Relay_In_83()
	{
		logic_uScript_DoesTankHave_tank_83 = local_Robber_Tank;
		logic_uScript_DoesTankHave_block_83 = local_ShieldBlockType_BlockTypes;
		logic_uScript_DoesTankHave_uScript_DoesTankHave_83.In(logic_uScript_DoesTankHave_tank_83, logic_uScript_DoesTankHave_amountOfPieces_83, logic_uScript_DoesTankHave_block_83, logic_uScript_DoesTankHave_checkForAmountOnly_83);
		bool num = logic_uScript_DoesTankHave_uScript_DoesTankHave_83.True;
		bool flag = logic_uScript_DoesTankHave_uScript_DoesTankHave_83.False;
		if (num)
		{
			Relay_In_97();
		}
		if (flag)
		{
			Relay_In_79();
		}
	}

	private void Relay_In_85()
	{
		logic_uScript_LoadBool_data_85 = local_QuestLogShown_System_Boolean;
		logic_uScript_LoadBool_owner_85 = owner_Connection_19;
		logic_uScript_LoadBool_uScript_LoadBool_85.In(ref logic_uScript_LoadBool_data_85, logic_uScript_LoadBool_owner_85, logic_uScript_LoadBool_uniqueName_85);
		local_QuestLogShown_System_Boolean = logic_uScript_LoadBool_data_85;
		if (logic_uScript_LoadBool_uScript_LoadBool_85.Out)
		{
			Relay_In_153();
		}
	}

	private void Relay_SaveEvent_93()
	{
		Relay_In_103();
	}

	private void Relay_LoadEvent_93()
	{
		Relay_In_106();
	}

	private void Relay_RestartEvent_93()
	{
		Relay_In_36();
	}

	private void Relay_In_94()
	{
		logic_uScript_LoadBool_data_94 = local_PositionCached_System_Boolean;
		logic_uScript_LoadBool_owner_94 = owner_Connection_9;
		logic_uScript_LoadBool_uScript_LoadBool_94.In(ref logic_uScript_LoadBool_data_94, logic_uScript_LoadBool_owner_94, logic_uScript_LoadBool_uniqueName_94);
		local_PositionCached_System_Boolean = logic_uScript_LoadBool_data_94;
		if (logic_uScript_LoadBool_uScript_LoadBool_94.Out)
		{
			Relay_In_70();
		}
	}

	private void Relay_True_96()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_96.True(out logic_uScriptAct_SetBool_Target_96);
		local_QuestLogShown_System_Boolean = logic_uScriptAct_SetBool_Target_96;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_96.Out)
		{
			Relay_False_156();
		}
	}

	private void Relay_False_96()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_96.False(out logic_uScriptAct_SetBool_Target_96);
		local_QuestLogShown_System_Boolean = logic_uScriptAct_SetBool_Target_96;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_96.Out)
		{
			Relay_False_156();
		}
	}

	private void Relay_In_97()
	{
		int num = 0;
		Array msg1EnemyEncounter = Msg1EnemyEncounter;
		if (logic_uScript_AddOnScreenMessage_locString_97.Length != num + msg1EnemyEncounter.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_97, num + msg1EnemyEncounter.Length);
		}
		Array.Copy(msg1EnemyEncounter, 0, logic_uScript_AddOnScreenMessage_locString_97, num, msg1EnemyEncounter.Length);
		num += msg1EnemyEncounter.Length;
		logic_uScript_AddOnScreenMessage_tag_97 = local_msgShield_System_String;
		logic_uScript_AddOnScreenMessage_Return_97 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_97.In(logic_uScript_AddOnScreenMessage_locString_97, logic_uScript_AddOnScreenMessage_msgPriority_97, logic_uScript_AddOnScreenMessage_holdMsg_97, logic_uScript_AddOnScreenMessage_tag_97, logic_uScript_AddOnScreenMessage_speaker_97, logic_uScript_AddOnScreenMessage_side_97);
	}

	private void Relay_In_98()
	{
		logic_uScript_DoesPlayerHaveBlock_block_98 = local_ShieldBlockType_BlockTypes;
		logic_uScript_DoesPlayerHaveBlock_uScript_DoesPlayerHaveBlock_98.In(logic_uScript_DoesPlayerHaveBlock_block_98, logic_uScript_DoesPlayerHaveBlock_isDragging_98);
		bool num = logic_uScript_DoesPlayerHaveBlock_uScript_DoesPlayerHaveBlock_98.True;
		bool flag = logic_uScript_DoesPlayerHaveBlock_uScript_DoesPlayerHaveBlock_98.False;
		if (num)
		{
			Relay_In_134();
		}
		if (flag)
		{
			Relay_In_1();
		}
	}

	private void Relay_In_102()
	{
		int num = 0;
		Array msg7ShieldAttached = Msg7ShieldAttached;
		if (logic_uScript_AddOnScreenMessage_locString_102.Length != num + msg7ShieldAttached.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_102, num + msg7ShieldAttached.Length);
		}
		Array.Copy(msg7ShieldAttached, 0, logic_uScript_AddOnScreenMessage_locString_102, num, msg7ShieldAttached.Length);
		num += msg7ShieldAttached.Length;
		logic_uScript_AddOnScreenMessage_Return_102 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_102.In(logic_uScript_AddOnScreenMessage_locString_102, logic_uScript_AddOnScreenMessage_msgPriority_102, logic_uScript_AddOnScreenMessage_holdMsg_102, logic_uScript_AddOnScreenMessage_tag_102, logic_uScript_AddOnScreenMessage_speaker_102, logic_uScript_AddOnScreenMessage_side_102);
		if (logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_102.Out)
		{
			Relay_In_186();
		}
	}

	private void Relay_In_103()
	{
		logic_uScript_SaveVariable_variable_103 = local_Stage_System_Int32;
		logic_uScript_SaveVariable_owner_103 = owner_Connection_105;
		logic_uScript_SaveVariable_uScript_SaveVariable_103.In(logic_uScript_SaveVariable_variable_103, logic_uScript_SaveVariable_owner_103, logic_uScript_SaveVariable_uniqueId_103);
		if (logic_uScript_SaveVariable_uScript_SaveVariable_103.Out)
		{
			Relay_In_31();
		}
	}

	private void Relay_In_106()
	{
		logic_uScript_LoadInt_data_106 = local_Stage_System_Int32;
		logic_uScript_LoadInt_owner_106 = owner_Connection_108;
		logic_uScript_LoadInt_uScript_LoadInt_106.In(ref logic_uScript_LoadInt_data_106, logic_uScript_LoadInt_owner_106, logic_uScript_LoadInt_uniqueName_106);
		local_Stage_System_Int32 = logic_uScript_LoadInt_data_106;
		if (logic_uScript_LoadInt_uScript_LoadInt_106.Out)
		{
			Relay_In_94();
		}
	}

	private void Relay_In_134()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_134 = local_msgShield_System_String;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_134.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_134, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_134);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_134.Out)
		{
			Relay_In_102();
		}
	}

	private void Relay_OnUpdate_137()
	{
		Relay_In_28();
	}

	private void Relay_OnSuspend_137()
	{
	}

	private void Relay_OnResume_137()
	{
	}

	private void Relay_In_138()
	{
		int num = 0;
		if (logic_uScript_GetAndCheckTechs_techData_138.Length <= num)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_138, num + 1);
		}
		logic_uScript_GetAndCheckTechs_techData_138[num++] = SpawnDataRobber;
		logic_uScript_GetAndCheckTechs_ownerNode_138 = owner_Connection_146;
		int num2 = 0;
		Array array = local_142_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_138.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_138, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techs_138, num2, array.Length);
		num2 += array.Length;
		logic_uScript_GetAndCheckTechs_Return_138 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_138.In(logic_uScript_GetAndCheckTechs_techData_138, logic_uScript_GetAndCheckTechs_ownerNode_138, ref logic_uScript_GetAndCheckTechs_techs_138);
		local_142_TankArray = logic_uScript_GetAndCheckTechs_techs_138;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_138.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_138.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_138.AllDead;
		if (allAlive)
		{
			Relay_AtIndex_143();
		}
		if (someAlive)
		{
			Relay_AtIndex_143();
		}
		if (allDead)
		{
			Relay_True_145();
		}
	}

	private void Relay_True_139()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_139.True(out logic_uScriptAct_SetBool_Target_139);
		local_EnemySpawned_System_Boolean = logic_uScriptAct_SetBool_Target_139;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_139.Out)
		{
			Relay_InitialSpawn_144();
		}
	}

	private void Relay_False_139()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_139.False(out logic_uScriptAct_SetBool_Target_139);
		local_EnemySpawned_System_Boolean = logic_uScriptAct_SetBool_Target_139;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_139.Out)
		{
			Relay_InitialSpawn_144();
		}
	}

	private void Relay_AtIndex_143()
	{
		int num = 0;
		Array array = local_142_TankArray;
		if (logic_uScript_AccessListTech_techList_143.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_143, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_143, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_143.AtIndex(ref logic_uScript_AccessListTech_techList_143, logic_uScript_AccessListTech_index_143, out logic_uScript_AccessListTech_value_143);
		local_142_TankArray = logic_uScript_AccessListTech_techList_143;
		local_Robber_Tank = logic_uScript_AccessListTech_value_143;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_143.Out)
		{
			Relay_In_7();
		}
	}

	private void Relay_InitialSpawn_144()
	{
		int num = 0;
		if (logic_uScript_SpawnTechsFromData_spawnData_144.Length <= num)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_144, num + 1);
		}
		logic_uScript_SpawnTechsFromData_spawnData_144[num++] = SpawnDataRobber;
		logic_uScript_SpawnTechsFromData_ownerNode_144 = owner_Connection_140;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_144.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_144, logic_uScript_SpawnTechsFromData_ownerNode_144, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_144);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_144.Out)
		{
			Relay_In_138();
		}
	}

	private void Relay_True_145()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_145.True(out logic_uScriptAct_SetBool_Target_145);
		local_Enemy_Dead_System_Boolean = logic_uScriptAct_SetBool_Target_145;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_145.Out)
		{
			Relay_In_39();
		}
	}

	private void Relay_False_145()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_145.False(out logic_uScriptAct_SetBool_Target_145);
		local_Enemy_Dead_System_Boolean = logic_uScriptAct_SetBool_Target_145;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_145.Out)
		{
			Relay_In_39();
		}
	}

	private void Relay_In_147()
	{
		logic_uScriptCon_CompareBool_Bool_147 = local_EnemySpawned_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_147.In(logic_uScriptCon_CompareBool_Bool_147);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_147.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_147.False;
		if (num)
		{
			Relay_In_138();
		}
		if (flag)
		{
			Relay_True_139();
		}
	}

	private void Relay_In_149()
	{
		logic_uScript_SaveVariable_variable_149 = local_EnemySpawned_System_Boolean;
		logic_uScript_SaveVariable_owner_149 = owner_Connection_150;
		logic_uScript_SaveVariable_uScript_SaveVariable_149.In(logic_uScript_SaveVariable_variable_149, logic_uScript_SaveVariable_owner_149, logic_uScript_SaveVariable_uniqueId_149);
	}

	private void Relay_In_153()
	{
		logic_uScript_LoadBool_data_153 = local_EnemySpawned_System_Boolean;
		logic_uScript_LoadBool_owner_153 = owner_Connection_154;
		logic_uScript_LoadBool_uScript_LoadBool_153.In(ref logic_uScript_LoadBool_data_153, logic_uScript_LoadBool_owner_153, logic_uScript_LoadBool_uniqueName_153);
		local_EnemySpawned_System_Boolean = logic_uScript_LoadBool_data_153;
	}

	private void Relay_True_156()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_156.True(out logic_uScriptAct_SetBool_Target_156);
		local_EnemySpawned_System_Boolean = logic_uScriptAct_SetBool_Target_156;
	}

	private void Relay_False_156()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_156.False(out logic_uScriptAct_SetBool_Target_156);
		local_EnemySpawned_System_Boolean = logic_uScriptAct_SetBool_Target_156;
	}

	private void Relay_In_157()
	{
		logic_uScript_SetTankInvulnerable_tank_157 = local_Robber_Tank;
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_157.In(logic_uScript_SetTankInvulnerable_invulnerable_157, logic_uScript_SetTankInvulnerable_tank_157);
	}

	private void Relay_In_160()
	{
		logic_uScript_SetTankInvulnerable_tank_160 = local_Robber_Tank;
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_160.In(logic_uScript_SetTankInvulnerable_invulnerable_160, logic_uScript_SetTankInvulnerable_tank_160);
		if (logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_160.Out)
		{
			Relay_In_83();
		}
	}

	private void Relay_In_161()
	{
		logic_uScript_SpawnBlockAbovePlayer_blockType_161 = local_ShieldBlockType_BlockTypes;
		logic_uScript_SpawnBlockAbovePlayer_uniqueName_161 = local_BlockName_System_String;
		logic_uScript_SpawnBlockAbovePlayer_owner_161 = owner_Connection_162;
		logic_uScript_SpawnBlockAbovePlayer_Return_161 = logic_uScript_SpawnBlockAbovePlayer_uScript_SpawnBlockAbovePlayer_161.In(logic_uScript_SpawnBlockAbovePlayer_blockType_161, logic_uScript_SpawnBlockAbovePlayer_uniqueName_161, logic_uScript_SpawnBlockAbovePlayer_owner_161);
		if (logic_uScript_SpawnBlockAbovePlayer_uScript_SpawnBlockAbovePlayer_161.Out)
		{
			Relay_Fail_188();
		}
	}

	private void Relay_In_166()
	{
		int num = 0;
		Array msg6OopsNotRight = Msg6OopsNotRight;
		if (logic_uScript_AddOnScreenMessage_locString_166.Length != num + msg6OopsNotRight.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_166, num + msg6OopsNotRight.Length);
		}
		Array.Copy(msg6OopsNotRight, 0, logic_uScript_AddOnScreenMessage_locString_166, num, msg6OopsNotRight.Length);
		num += msg6OopsNotRight.Length;
		logic_uScript_AddOnScreenMessage_tag_166 = local_msgShield_System_String;
		logic_uScript_AddOnScreenMessage_Return_166 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_166.In(logic_uScript_AddOnScreenMessage_locString_166, logic_uScript_AddOnScreenMessage_msgPriority_166, logic_uScript_AddOnScreenMessage_holdMsg_166, logic_uScript_AddOnScreenMessage_tag_166, logic_uScript_AddOnScreenMessage_speaker_166, logic_uScript_AddOnScreenMessage_side_166);
		if (logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_166.Out)
		{
			Relay_In_161();
		}
	}

	private void Relay_In_168()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_168 = local_msgShield_System_String;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_168.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_168, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_168);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_168.Out)
		{
			Relay_In_166();
		}
	}

	private void Relay_In_172()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_172 = local_msgShield_System_String;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_172.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_172, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_172);
	}

	private void Relay_In_175()
	{
		logic_uScript_PlayerInRangeOfCurrentEncounter_ownerNode_175 = owner_Connection_174;
		logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_175.In(logic_uScript_PlayerInRangeOfCurrentEncounter_ownerNode_175);
		bool num = logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_175.True;
		bool flag = logic_uScript_PlayerInRangeOfCurrentEncounter_uScript_PlayerInRangeOfCurrentEncounter_175.False;
		if (num)
		{
			Relay_In_184();
		}
		if (flag)
		{
			Relay_In_172();
		}
	}

	private void Relay_In_177()
	{
		logic_uScriptCon_CompareBool_Bool_177 = local_Enemy_Dead_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_177.In(logic_uScriptCon_CompareBool_Bool_177);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_177.False)
		{
			Relay_In_157();
		}
	}

	private void Relay_In_178()
	{
		logic_uScript_BlankNode_uScript_BlankNode_178.In();
		if (logic_uScript_BlankNode_uScript_BlankNode_178.Out)
		{
			Relay_In_40();
		}
	}

	private void Relay_In_179()
	{
		logic_uScript_BlankNode_uScript_BlankNode_179.In();
		if (logic_uScript_BlankNode_uScript_BlankNode_179.Out)
		{
			Relay_In_40();
		}
	}

	private void Relay_In_184()
	{
		int num = 0;
		Array msg4PickUpShield = Msg4PickUpShield;
		if (logic_uScript_AddOnScreenMessage_locString_184.Length != num + msg4PickUpShield.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_184, num + msg4PickUpShield.Length);
		}
		Array.Copy(msg4PickUpShield, 0, logic_uScript_AddOnScreenMessage_locString_184, num, msg4PickUpShield.Length);
		num += msg4PickUpShield.Length;
		logic_uScript_AddOnScreenMessage_tag_184 = local_msgShield_System_String;
		logic_uScript_AddOnScreenMessage_Return_184 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_184.In(logic_uScript_AddOnScreenMessage_locString_184, logic_uScript_AddOnScreenMessage_msgPriority_184, logic_uScript_AddOnScreenMessage_holdMsg_184, logic_uScript_AddOnScreenMessage_tag_184, logic_uScript_AddOnScreenMessage_speaker_184, logic_uScript_AddOnScreenMessage_side_184);
	}

	private void Relay_In_186()
	{
		logic_uScript_SetTankInvulnerable_tank_186 = local_Robber_Tank;
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_186.In(logic_uScript_SetTankInvulnerable_invulnerable_186, logic_uScript_SetTankInvulnerable_tank_186);
		if (logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_186.Out)
		{
			Relay_Succeed_190();
		}
	}

	private void Relay_Succeed_188()
	{
		logic_uScript_FinishEncounter_owner_188 = owner_Connection_187;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_188.Succeed(logic_uScript_FinishEncounter_owner_188);
	}

	private void Relay_Fail_188()
	{
		logic_uScript_FinishEncounter_owner_188 = owner_Connection_187;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_188.Fail(logic_uScript_FinishEncounter_owner_188);
	}

	private void Relay_Succeed_190()
	{
		logic_uScript_FinishEncounter_owner_190 = owner_Connection_189;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_190.Succeed(logic_uScript_FinishEncounter_owner_190);
	}

	private void Relay_Fail_190()
	{
		logic_uScript_FinishEncounter_owner_190 = owner_Connection_189;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_190.Fail(logic_uScript_FinishEncounter_owner_190);
	}
}
