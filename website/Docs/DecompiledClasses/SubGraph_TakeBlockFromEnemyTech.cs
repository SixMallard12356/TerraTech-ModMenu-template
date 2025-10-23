using System;
using UnityEngine;

[Serializable]
[NodePath("Graphs")]
[FriendlyName("SubGraph_TakeBlockFromEnemyTech", "")]
public class SubGraph_TakeBlockFromEnemyTech : uScriptLogic
{
	public delegate void uScriptEventHandler(object sender, LogicEventArgs args);

	public class LogicEventArgs : EventArgs
	{
		public TankBlock ReturnedTargetBlock;
	}

	private GameObject parentGameObject;

	private uScript_GUI thisScriptsOnGuiListener;

	private bool m_RegisteredForEvents;

	private BlockTypes external_22;

	private LocalisedString[] external_42 = new LocalisedString[0];

	private LocalisedString[] external_47 = new LocalisedString[0];

	private LocalisedString[] external_53 = new LocalisedString[0];

	private LocalisedString[] external_26 = new LocalisedString[0];

	private LocalisedString[] external_57 = new LocalisedString[0];

	private LocalisedString[] external_69 = new LocalisedString[0];

	private LocalisedString[] external_27 = new LocalisedString[0];

	private SpawnTechData[] external_23 = new SpawnTechData[0];

	private TankBlock external_28;

	private Tank[] local_126_TankArray = new Tank[0];

	private ManOnScreenMessages.OnScreenMessage local_AttachBlockmsg_ManOnScreenMessages_OnScreenMessage;

	private ManOnScreenMessages.OnScreenMessage local_AttackTheEnemymsg_ManOnScreenMessages_OnScreenMessage;

	private bool local_EnemyDead_System_Boolean;

	private string local_EnemyDeadID_System_String = "EnemyDead";

	private ManOnScreenMessages.OnScreenMessage local_EnemyDroppedBlockmsg_ManOnScreenMessages_OnScreenMessage;

	private bool local_EnemySpotted_System_Boolean;

	private string local_EnemySpottedID_System_String = "EnemySpotted";

	private ManOnScreenMessages.OnScreenMessage local_EnemySpottedmsg_ManOnScreenMessages_OnScreenMessage;

	private string local_msgGroup01_System_String = "msgGroup01";

	private string local_msgGroup02_System_String = "msgGroup02";

	private bool local_NotifiedAttackEnemy_System_Boolean;

	private string local_NotifiedAttackEnemyID_System_String = "NotifiedAttackEnemy";

	private bool local_NotifiedDroppedBlock_System_Boolean;

	private string local_NotifiedDroppedBlockID_System_String = "NotifiedDroppedBlock";

	private bool local_ObjectiveComplete_System_Boolean;

	private string local_ObjectiveCompleteID_System_String = "ObjectiveComplete";

	private ManOnScreenMessages.OnScreenMessage local_PickUpBlockmsg_ManOnScreenMessages_OnScreenMessage;

	private Tank local_ReturnedEnemyTech_Tank;

	private TankBlock local_ReturnedTargetBlock_TankBlock;

	private string local_TargetBlock_System_String = "TargetBlock";

	private bool local_TargetBlockCached_System_Boolean;

	private bool local_TechsSpawned_System_Boolean;

	private string local_TechsSpawnedID_System_String = "TechsSpawned";

	private GameObject owner_Connection_3;

	private GameObject owner_Connection_8;

	private GameObject owner_Connection_75;

	private GameObject owner_Connection_78;

	private GameObject owner_Connection_84;

	private GameObject owner_Connection_87;

	private GameObject owner_Connection_90;

	private GameObject owner_Connection_105;

	private GameObject owner_Connection_125;

	private GameObject owner_Connection_128;

	private GameObject owner_Connection_139;

	private GameObject owner_Connection_142;

	private GameObject owner_Connection_145;

	private GameObject owner_Connection_149;

	private GameObject owner_Connection_152;

	private GameObject owner_Connection_157;

	private uScript_DoesPlayerHaveBlock logic_uScript_DoesPlayerHaveBlock_uScript_DoesPlayerHaveBlock_0 = new uScript_DoesPlayerHaveBlock();

	private BlockTypes logic_uScript_DoesPlayerHaveBlock_block_0;

	private bool logic_uScript_DoesPlayerHaveBlock_isDragging_0;

	private bool logic_uScript_DoesPlayerHaveBlock_Out_0 = true;

	private bool logic_uScript_DoesPlayerHaveBlock_True_0 = true;

	private bool logic_uScript_DoesPlayerHaveBlock_False_0 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_1 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_1 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_1 = ManOnScreenMessages.MessagePriority.Medium;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_1;

	private string logic_uScript_AddOnScreenMessage_tag_1 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_1;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_1;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_1;

	private bool logic_uScript_AddOnScreenMessage_Out_1 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_1 = true;

	private uScript_GetTankBlock logic_uScript_GetTankBlock_uScript_GetTankBlock_4 = new uScript_GetTankBlock();

	private Tank logic_uScript_GetTankBlock_tank_4;

	private BlockTypes logic_uScript_GetTankBlock_blockType_4;

	private TankBlock logic_uScript_GetTankBlock_Return_4;

	private bool logic_uScript_GetTankBlock_Out_4 = true;

	private bool logic_uScript_GetTankBlock_Returned_4 = true;

	private bool logic_uScript_GetTankBlock_NotFound_4 = true;

	private uScript_GetNamedBlock logic_uScript_GetNamedBlock_uScript_GetNamedBlock_5 = new uScript_GetNamedBlock();

	private string logic_uScript_GetNamedBlock_name_5 = "";

	private GameObject logic_uScript_GetNamedBlock_owner_5;

	private TankBlock logic_uScript_GetNamedBlock_Return_5;

	private bool logic_uScript_GetNamedBlock_Out_5 = true;

	private bool logic_uScript_GetNamedBlock_Destroyed_5 = true;

	private bool logic_uScript_GetNamedBlock_BlockExists_5 = true;

	private bool logic_uScript_GetNamedBlock_WaitingForBlock_5 = true;

	private bool logic_uScript_GetNamedBlock_NoBlock_5 = true;

	private uScript_KeepBlockInvulnerable logic_uScript_KeepBlockInvulnerable_uScript_KeepBlockInvulnerable_7 = new uScript_KeepBlockInvulnerable();

	private TankBlock logic_uScript_KeepBlockInvulnerable_block_7;

	private bool logic_uScript_KeepBlockInvulnerable_Out_7 = true;

	private uScript_SaveNamedBlock logic_uScript_SaveNamedBlock_uScript_SaveNamedBlock_9 = new uScript_SaveNamedBlock();

	private TankBlock logic_uScript_SaveNamedBlock_block_9;

	private string logic_uScript_SaveNamedBlock_uniqueName_9 = "";

	private GameObject logic_uScript_SaveNamedBlock_owner_9;

	private bool logic_uScript_SaveNamedBlock_Out_9 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_17 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_17 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_17 = ManOnScreenMessages.MessagePriority.Medium;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_17;

	private string logic_uScript_AddOnScreenMessage_tag_17 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_17;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_17;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_17;

	private bool logic_uScript_AddOnScreenMessage_Out_17 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_17 = true;

	private uScript_DiscoverBlock logic_uScript_DiscoverBlock_uScript_DiscoverBlock_18 = new uScript_DiscoverBlock();

	private BlockTypes logic_uScript_DiscoverBlock_blockType_18;

	private bool logic_uScript_DiscoverBlock_Out_18 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_30 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_30;

	private bool logic_uScriptCon_CompareBool_True_30 = true;

	private bool logic_uScriptCon_CompareBool_False_30 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_32 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_32;

	private bool logic_uScriptAct_SetBool_Out_32 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_32 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_32 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_36 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_36;

	private bool logic_uScriptCon_CompareBool_True_36 = true;

	private bool logic_uScriptCon_CompareBool_False_36 = true;

	private uScript_BlockAttachedToTech logic_uScript_BlockAttachedToTech_uScript_BlockAttachedToTech_37 = new uScript_BlockAttachedToTech();

	private Tank logic_uScript_BlockAttachedToTech_tech_37;

	private TankBlock logic_uScript_BlockAttachedToTech_block_37;

	private bool logic_uScript_BlockAttachedToTech_True_37 = true;

	private bool logic_uScript_BlockAttachedToTech_False_37 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_41 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_41 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_41 = ManOnScreenMessages.MessagePriority.Medium;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_41;

	private string logic_uScript_AddOnScreenMessage_tag_41 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_41;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_41;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_41;

	private bool logic_uScript_AddOnScreenMessage_Out_41 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_41 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_46 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_46 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_46 = ManOnScreenMessages.MessagePriority.Medium;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_46;

	private string logic_uScript_AddOnScreenMessage_tag_46 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_46;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_46;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_46;

	private bool logic_uScript_AddOnScreenMessage_Out_46 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_46 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_49 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_49;

	private bool logic_uScriptAct_SetBool_Out_49 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_49 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_49 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_50 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_50;

	private bool logic_uScriptCon_CompareBool_True_50 = true;

	private bool logic_uScriptCon_CompareBool_False_50 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_52 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_52 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_52 = ManOnScreenMessages.MessagePriority.Medium;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_52;

	private string logic_uScript_AddOnScreenMessage_tag_52 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_52;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_52;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_52;

	private bool logic_uScript_AddOnScreenMessage_Out_52 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_52 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_54 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_54;

	private bool logic_uScriptAct_SetBool_Out_54 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_54 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_54 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_56 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_56 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_56 = ManOnScreenMessages.MessagePriority.Medium;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_56 = true;

	private string logic_uScript_AddOnScreenMessage_tag_56 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_56;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_56;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_56;

	private bool logic_uScript_AddOnScreenMessage_Out_56 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_56 = true;

	private uScript_IsPlayerInRangeOfVisible logic_uScript_IsPlayerInRangeOfVisible_uScript_IsPlayerInRangeOfVisible_61 = new uScript_IsPlayerInRangeOfVisible();

	private object logic_uScript_IsPlayerInRangeOfVisible_visibleObject_61 = "";

	private float logic_uScript_IsPlayerInRangeOfVisible_range_61 = 50f;

	private bool logic_uScript_IsPlayerInRangeOfVisible_Out_61 = true;

	private bool logic_uScript_IsPlayerInRangeOfVisible_InRange_61 = true;

	private bool logic_uScript_IsPlayerInRangeOfVisible_OutOfRange_61 = true;

	private uScript_IsPlayerInRangeOfVisible logic_uScript_IsPlayerInRangeOfVisible_uScript_IsPlayerInRangeOfVisible_62 = new uScript_IsPlayerInRangeOfVisible();

	private object logic_uScript_IsPlayerInRangeOfVisible_visibleObject_62 = "";

	private float logic_uScript_IsPlayerInRangeOfVisible_range_62 = 75f;

	private bool logic_uScript_IsPlayerInRangeOfVisible_Out_62 = true;

	private bool logic_uScript_IsPlayerInRangeOfVisible_InRange_62 = true;

	private bool logic_uScript_IsPlayerInRangeOfVisible_OutOfRange_62 = true;

	private uScript_IsPlayerInRangeOfVisible logic_uScript_IsPlayerInRangeOfVisible_uScript_IsPlayerInRangeOfVisible_63 = new uScript_IsPlayerInRangeOfVisible();

	private object logic_uScript_IsPlayerInRangeOfVisible_visibleObject_63 = "";

	private float logic_uScript_IsPlayerInRangeOfVisible_range_63 = 50f;

	private bool logic_uScript_IsPlayerInRangeOfVisible_Out_63 = true;

	private bool logic_uScript_IsPlayerInRangeOfVisible_InRange_63 = true;

	private bool logic_uScript_IsPlayerInRangeOfVisible_OutOfRange_63 = true;

	private uScript_IsPlayerInteractingWithBlock logic_uScript_IsPlayerInteractingWithBlock_uScript_IsPlayerInteractingWithBlock_66 = new uScript_IsPlayerInteractingWithBlock();

	private TankBlock logic_uScript_IsPlayerInteractingWithBlock_block_66;

	private bool logic_uScript_IsPlayerInteractingWithBlock_Out_66 = true;

	private bool logic_uScript_IsPlayerInteractingWithBlock_Interacted_66 = true;

	private bool logic_uScript_IsPlayerInteractingWithBlock_NotInteracted_66 = true;

	private bool logic_uScript_IsPlayerInteractingWithBlock_Dragging_66 = true;

	private bool logic_uScript_IsPlayerInteractingWithBlock_NotDragging_66 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_68 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_68 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_68 = ManOnScreenMessages.MessagePriority.Medium;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_68 = true;

	private string logic_uScript_AddOnScreenMessage_tag_68 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_68;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_68;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_68;

	private bool logic_uScript_AddOnScreenMessage_Out_68 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_68 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_71 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_71;

	private bool logic_uScriptAct_SetBool_Out_71 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_71 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_71 = true;

	private uScript_SaveVariable logic_uScript_SaveVariable_uScript_SaveVariable_76 = new uScript_SaveVariable();

	private object logic_uScript_SaveVariable_variable_76 = "";

	private GameObject logic_uScript_SaveVariable_owner_76;

	private string logic_uScript_SaveVariable_uniqueId_76 = "";

	private bool logic_uScript_SaveVariable_Out_76 = true;

	private uScript_LoadBool logic_uScript_LoadBool_uScript_LoadBool_77 = new uScript_LoadBool();

	private bool logic_uScript_LoadBool_data_77;

	private GameObject logic_uScript_LoadBool_owner_77;

	private string logic_uScript_LoadBool_uniqueName_77 = "";

	private bool logic_uScript_LoadBool_Out_77 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_80 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_80;

	private bool logic_uScriptAct_SetBool_Out_80 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_80 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_80 = true;

	private uScript_SaveVariable logic_uScript_SaveVariable_uScript_SaveVariable_83 = new uScript_SaveVariable();

	private object logic_uScript_SaveVariable_variable_83 = "";

	private GameObject logic_uScript_SaveVariable_owner_83;

	private string logic_uScript_SaveVariable_uniqueId_83 = "";

	private bool logic_uScript_SaveVariable_Out_83 = true;

	private uScript_SaveVariable logic_uScript_SaveVariable_uScript_SaveVariable_86 = new uScript_SaveVariable();

	private object logic_uScript_SaveVariable_variable_86 = "";

	private GameObject logic_uScript_SaveVariable_owner_86;

	private string logic_uScript_SaveVariable_uniqueId_86 = "";

	private bool logic_uScript_SaveVariable_Out_86 = true;

	private uScript_SaveVariable logic_uScript_SaveVariable_uScript_SaveVariable_89 = new uScript_SaveVariable();

	private object logic_uScript_SaveVariable_variable_89 = "";

	private GameObject logic_uScript_SaveVariable_owner_89;

	private string logic_uScript_SaveVariable_uniqueId_89 = "";

	private bool logic_uScript_SaveVariable_Out_89 = true;

	private uScript_LoadBool logic_uScript_LoadBool_uScript_LoadBool_91 = new uScript_LoadBool();

	private bool logic_uScript_LoadBool_data_91;

	private GameObject logic_uScript_LoadBool_owner_91;

	private string logic_uScript_LoadBool_uniqueName_91 = "";

	private bool logic_uScript_LoadBool_Out_91 = true;

	private uScript_LoadBool logic_uScript_LoadBool_uScript_LoadBool_92 = new uScript_LoadBool();

	private bool logic_uScript_LoadBool_data_92;

	private GameObject logic_uScript_LoadBool_owner_92;

	private string logic_uScript_LoadBool_uniqueName_92 = "";

	private bool logic_uScript_LoadBool_Out_92 = true;

	private uScript_LoadBool logic_uScript_LoadBool_uScript_LoadBool_93 = new uScript_LoadBool();

	private bool logic_uScript_LoadBool_data_93;

	private GameObject logic_uScript_LoadBool_owner_93;

	private string logic_uScript_LoadBool_uniqueName_93 = "";

	private bool logic_uScript_LoadBool_Out_93 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_94 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_94;

	private bool logic_uScriptAct_SetBool_Out_94 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_94 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_94 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_95 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_95;

	private bool logic_uScriptAct_SetBool_Out_95 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_95 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_95 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_96 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_96;

	private bool logic_uScriptAct_SetBool_Out_96 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_96 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_96 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_100 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_100;

	private bool logic_uScriptCon_CompareBool_True_100 = true;

	private bool logic_uScriptCon_CompareBool_False_100 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_102 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_102;

	private bool logic_uScriptAct_SetBool_Out_102 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_102 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_102 = true;

	private uScript_SaveVariable logic_uScript_SaveVariable_uScript_SaveVariable_106 = new uScript_SaveVariable();

	private object logic_uScript_SaveVariable_variable_106 = "";

	private GameObject logic_uScript_SaveVariable_owner_106;

	private string logic_uScript_SaveVariable_uniqueId_106 = "";

	private bool logic_uScript_SaveVariable_Out_106 = true;

	private uScript_LoadBool logic_uScript_LoadBool_uScript_LoadBool_107 = new uScript_LoadBool();

	private bool logic_uScript_LoadBool_data_107;

	private GameObject logic_uScript_LoadBool_owner_107;

	private string logic_uScript_LoadBool_uniqueName_107 = "";

	private bool logic_uScript_LoadBool_Out_107 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_108 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_108;

	private bool logic_uScriptAct_SetBool_Out_108 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_108 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_108 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_114 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_114 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_114 = true;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_114 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_117 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_117 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_117;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_117 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_119 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_119 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_119 = true;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_119 = true;

	private uScript_IsOnScreenMessage logic_uScript_IsOnScreenMessage_uScript_IsOnScreenMessage_120 = new uScript_IsOnScreenMessage();

	private ManOnScreenMessages.OnScreenMessage logic_uScript_IsOnScreenMessage_onScreenMessage_120;

	private bool logic_uScript_IsOnScreenMessage_Out_120 = true;

	private bool logic_uScript_IsOnScreenMessage_True_120 = true;

	private bool logic_uScript_IsOnScreenMessage_False_120 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_122 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_122;

	private bool logic_uScriptCon_CompareBool_True_122 = true;

	private bool logic_uScriptCon_CompareBool_False_122 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_124 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_124 = new Tank[0];

	private int logic_uScript_AccessListTech_index_124;

	private Tank logic_uScript_AccessListTech_value_124;

	private bool logic_uScript_AccessListTech_Out_124 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_127 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_127 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_127;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_127 = 0.3f;

	private bool logic_uScript_SpawnTechsFromData_Out_127 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_129 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_129;

	private bool logic_uScriptCon_CompareBool_True_129 = true;

	private bool logic_uScriptCon_CompareBool_False_129 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_130 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_130 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_130;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_130 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_130;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_130 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_130 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_130 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_130 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_132 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_132;

	private bool logic_uScriptAct_SetBool_Out_132 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_132 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_132 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_135 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_135;

	private bool logic_uScriptCon_CompareBool_True_135 = true;

	private bool logic_uScriptCon_CompareBool_False_135 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_136 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_136;

	private bool logic_uScriptAct_SetBool_Out_136 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_136 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_136 = true;

	private uScript_SetEncounterTarget logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_138 = new uScript_SetEncounterTarget();

	private GameObject logic_uScript_SetEncounterTarget_owner_138;

	private object logic_uScript_SetEncounterTarget_visibleObject_138 = "";

	private bool logic_uScript_SetEncounterTarget_Out_138 = true;

	private uScript_MoveEncounterWithVisible logic_uScript_MoveEncounterWithVisible_uScript_MoveEncounterWithVisible_141 = new uScript_MoveEncounterWithVisible();

	private GameObject logic_uScript_MoveEncounterWithVisible_ownerNode_141;

	private object logic_uScript_MoveEncounterWithVisible_visibleObject_141 = "";

	private bool logic_uScript_MoveEncounterWithVisible_Out_141 = true;

	private uScript_MoveEncounterWithVisible logic_uScript_MoveEncounterWithVisible_uScript_MoveEncounterWithVisible_146 = new uScript_MoveEncounterWithVisible();

	private GameObject logic_uScript_MoveEncounterWithVisible_ownerNode_146;

	private object logic_uScript_MoveEncounterWithVisible_visibleObject_146 = "";

	private bool logic_uScript_MoveEncounterWithVisible_Out_146 = true;

	private uScript_SetEncounterTarget logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_148 = new uScript_SetEncounterTarget();

	private GameObject logic_uScript_SetEncounterTarget_owner_148;

	private object logic_uScript_SetEncounterTarget_visibleObject_148 = "";

	private bool logic_uScript_SetEncounterTarget_Out_148 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_151 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_151;

	private bool logic_uScriptAct_SetBool_Out_151 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_151 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_151 = true;

	private uScript_LoadBool logic_uScript_LoadBool_uScript_LoadBool_153 = new uScript_LoadBool();

	private bool logic_uScript_LoadBool_data_153;

	private GameObject logic_uScript_LoadBool_owner_153;

	private string logic_uScript_LoadBool_uniqueName_153 = "";

	private bool logic_uScript_LoadBool_Out_153 = true;

	private uScript_SaveVariable logic_uScript_SaveVariable_uScript_SaveVariable_155 = new uScript_SaveVariable();

	private object logic_uScript_SaveVariable_variable_155 = "";

	private GameObject logic_uScript_SaveVariable_owner_155;

	private string logic_uScript_SaveVariable_uniqueId_155 = "";

	private bool logic_uScript_SaveVariable_Out_155 = true;

	private uScript_FinishEncounter logic_uScript_FinishEncounter_uScript_FinishEncounter_158 = new uScript_FinishEncounter();

	private GameObject logic_uScript_FinishEncounter_owner_158;

	private bool logic_uScript_FinishEncounter_Out_158 = true;

	[FriendlyName("Complete")]
	public event uScriptEventHandler Complete;

	private void SyncUnityHooks()
	{
		SyncEventListeners();
		if (null == owner_Connection_3 || !m_RegisteredForEvents)
		{
			owner_Connection_3 = parentGameObject;
		}
		if (null == owner_Connection_8 || !m_RegisteredForEvents)
		{
			owner_Connection_8 = parentGameObject;
		}
		if (null == owner_Connection_75 || !m_RegisteredForEvents)
		{
			owner_Connection_75 = parentGameObject;
			if (null != owner_Connection_75)
			{
				uScript_SaveLoad uScript_SaveLoad2 = owner_Connection_75.GetComponent<uScript_SaveLoad>();
				if (null == uScript_SaveLoad2)
				{
					uScript_SaveLoad2 = owner_Connection_75.AddComponent<uScript_SaveLoad>();
				}
				if (null != uScript_SaveLoad2)
				{
					uScript_SaveLoad2.SaveEvent += Instance_SaveEvent_74;
					uScript_SaveLoad2.LoadEvent += Instance_LoadEvent_74;
					uScript_SaveLoad2.RestartEvent += Instance_RestartEvent_74;
				}
			}
		}
		if (null == owner_Connection_78 || !m_RegisteredForEvents)
		{
			owner_Connection_78 = parentGameObject;
		}
		if (null == owner_Connection_84 || !m_RegisteredForEvents)
		{
			owner_Connection_84 = parentGameObject;
		}
		if (null == owner_Connection_87 || !m_RegisteredForEvents)
		{
			owner_Connection_87 = parentGameObject;
		}
		if (null == owner_Connection_90 || !m_RegisteredForEvents)
		{
			owner_Connection_90 = parentGameObject;
		}
		if (null == owner_Connection_105 || !m_RegisteredForEvents)
		{
			owner_Connection_105 = parentGameObject;
		}
		if (null == owner_Connection_125 || !m_RegisteredForEvents)
		{
			owner_Connection_125 = parentGameObject;
		}
		if (null == owner_Connection_128 || !m_RegisteredForEvents)
		{
			owner_Connection_128 = parentGameObject;
		}
		if (null == owner_Connection_139 || !m_RegisteredForEvents)
		{
			owner_Connection_139 = parentGameObject;
		}
		if (null == owner_Connection_142 || !m_RegisteredForEvents)
		{
			owner_Connection_142 = parentGameObject;
		}
		if (null == owner_Connection_145 || !m_RegisteredForEvents)
		{
			owner_Connection_145 = parentGameObject;
		}
		if (null == owner_Connection_149 || !m_RegisteredForEvents)
		{
			owner_Connection_149 = parentGameObject;
		}
		if (null == owner_Connection_152 || !m_RegisteredForEvents)
		{
			owner_Connection_152 = parentGameObject;
		}
		if (null == owner_Connection_157 || !m_RegisteredForEvents)
		{
			owner_Connection_157 = parentGameObject;
		}
	}

	private void RegisterForUnityHooks()
	{
		SyncEventListeners();
		if (!m_RegisteredForEvents && null != owner_Connection_75)
		{
			uScript_SaveLoad uScript_SaveLoad2 = owner_Connection_75.GetComponent<uScript_SaveLoad>();
			if (null == uScript_SaveLoad2)
			{
				uScript_SaveLoad2 = owner_Connection_75.AddComponent<uScript_SaveLoad>();
			}
			if (null != uScript_SaveLoad2)
			{
				uScript_SaveLoad2.SaveEvent += Instance_SaveEvent_74;
				uScript_SaveLoad2.LoadEvent += Instance_LoadEvent_74;
				uScript_SaveLoad2.RestartEvent += Instance_RestartEvent_74;
			}
		}
	}

	private void SyncEventListeners()
	{
	}

	private void UnregisterEventListeners()
	{
		if (null != owner_Connection_75)
		{
			uScript_SaveLoad component = owner_Connection_75.GetComponent<uScript_SaveLoad>();
			if (null != component)
			{
				component.SaveEvent -= Instance_SaveEvent_74;
				component.LoadEvent -= Instance_LoadEvent_74;
				component.RestartEvent -= Instance_RestartEvent_74;
			}
		}
	}

	public override void SetParent(GameObject g)
	{
		parentGameObject = g;
		logic_uScript_DoesPlayerHaveBlock_uScript_DoesPlayerHaveBlock_0.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_1.SetParent(g);
		logic_uScript_GetTankBlock_uScript_GetTankBlock_4.SetParent(g);
		logic_uScript_GetNamedBlock_uScript_GetNamedBlock_5.SetParent(g);
		logic_uScript_KeepBlockInvulnerable_uScript_KeepBlockInvulnerable_7.SetParent(g);
		logic_uScript_SaveNamedBlock_uScript_SaveNamedBlock_9.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_17.SetParent(g);
		logic_uScript_DiscoverBlock_uScript_DiscoverBlock_18.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_30.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_32.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_36.SetParent(g);
		logic_uScript_BlockAttachedToTech_uScript_BlockAttachedToTech_37.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_41.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_46.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_49.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_50.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_52.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_54.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_56.SetParent(g);
		logic_uScript_IsPlayerInRangeOfVisible_uScript_IsPlayerInRangeOfVisible_61.SetParent(g);
		logic_uScript_IsPlayerInRangeOfVisible_uScript_IsPlayerInRangeOfVisible_62.SetParent(g);
		logic_uScript_IsPlayerInRangeOfVisible_uScript_IsPlayerInRangeOfVisible_63.SetParent(g);
		logic_uScript_IsPlayerInteractingWithBlock_uScript_IsPlayerInteractingWithBlock_66.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_68.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_71.SetParent(g);
		logic_uScript_SaveVariable_uScript_SaveVariable_76.SetParent(g);
		logic_uScript_LoadBool_uScript_LoadBool_77.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_80.SetParent(g);
		logic_uScript_SaveVariable_uScript_SaveVariable_83.SetParent(g);
		logic_uScript_SaveVariable_uScript_SaveVariable_86.SetParent(g);
		logic_uScript_SaveVariable_uScript_SaveVariable_89.SetParent(g);
		logic_uScript_LoadBool_uScript_LoadBool_91.SetParent(g);
		logic_uScript_LoadBool_uScript_LoadBool_92.SetParent(g);
		logic_uScript_LoadBool_uScript_LoadBool_93.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_94.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_95.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_96.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_100.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_102.SetParent(g);
		logic_uScript_SaveVariable_uScript_SaveVariable_106.SetParent(g);
		logic_uScript_LoadBool_uScript_LoadBool_107.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_108.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_114.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_117.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_119.SetParent(g);
		logic_uScript_IsOnScreenMessage_uScript_IsOnScreenMessage_120.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_122.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_124.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_127.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_129.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_130.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_132.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_135.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_136.SetParent(g);
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_138.SetParent(g);
		logic_uScript_MoveEncounterWithVisible_uScript_MoveEncounterWithVisible_141.SetParent(g);
		logic_uScript_MoveEncounterWithVisible_uScript_MoveEncounterWithVisible_146.SetParent(g);
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_148.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_151.SetParent(g);
		logic_uScript_LoadBool_uScript_LoadBool_153.SetParent(g);
		logic_uScript_SaveVariable_uScript_SaveVariable_155.SetParent(g);
		logic_uScript_FinishEncounter_uScript_FinishEncounter_158.SetParent(g);
		owner_Connection_3 = parentGameObject;
		owner_Connection_8 = parentGameObject;
		owner_Connection_75 = parentGameObject;
		owner_Connection_78 = parentGameObject;
		owner_Connection_84 = parentGameObject;
		owner_Connection_87 = parentGameObject;
		owner_Connection_90 = parentGameObject;
		owner_Connection_105 = parentGameObject;
		owner_Connection_125 = parentGameObject;
		owner_Connection_128 = parentGameObject;
		owner_Connection_139 = parentGameObject;
		owner_Connection_142 = parentGameObject;
		owner_Connection_145 = parentGameObject;
		owner_Connection_149 = parentGameObject;
		owner_Connection_152 = parentGameObject;
		owner_Connection_157 = parentGameObject;
	}

	public void Awake()
	{
	}

	public void Start()
	{
		SyncUnityHooks();
		m_RegisteredForEvents = true;
	}

	public void OnEnable()
	{
		RegisterForUnityHooks();
		m_RegisteredForEvents = true;
	}

	public void OnDisable()
	{
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_1.OnDisable();
		logic_uScript_GetNamedBlock_uScript_GetNamedBlock_5.OnDisable();
		logic_uScript_SaveNamedBlock_uScript_SaveNamedBlock_9.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_17.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_41.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_46.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_52.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_56.OnDisable();
		logic_uScript_IsPlayerInRangeOfVisible_uScript_IsPlayerInRangeOfVisible_61.OnDisable();
		logic_uScript_IsPlayerInRangeOfVisible_uScript_IsPlayerInRangeOfVisible_62.OnDisable();
		logic_uScript_IsPlayerInRangeOfVisible_uScript_IsPlayerInRangeOfVisible_63.OnDisable();
		logic_uScript_IsPlayerInteractingWithBlock_uScript_IsPlayerInteractingWithBlock_66.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_68.OnDisable();
		logic_uScript_LoadBool_uScript_LoadBool_77.OnDisable();
		logic_uScript_LoadBool_uScript_LoadBool_91.OnDisable();
		logic_uScript_LoadBool_uScript_LoadBool_92.OnDisable();
		logic_uScript_LoadBool_uScript_LoadBool_93.OnDisable();
		logic_uScript_LoadBool_uScript_LoadBool_107.OnDisable();
		logic_uScript_LoadBool_uScript_LoadBool_153.OnDisable();
		UnregisterEventListeners();
		m_RegisteredForEvents = false;
	}

	public void Update()
	{
		SyncEventListeners();
	}

	public void OnDestroy()
	{
	}

	private void Instance_SaveEvent_74(object o, EventArgs e)
	{
		Relay_SaveEvent_74();
	}

	private void Instance_LoadEvent_74(object o, EventArgs e)
	{
		Relay_LoadEvent_74();
	}

	private void Instance_RestartEvent_74(object o, EventArgs e)
	{
		Relay_RestartEvent_74();
	}

	[FriendlyName("In", "")]
	public void In([FriendlyName("blockToGet", "")] BlockTypes blockToGet, [FriendlyName("msgEnemySpotted", "")] LocalisedString[] msgEnemySpotted, [FriendlyName("msgAttackTheEnemy", "")] LocalisedString[] msgAttackTheEnemy, [FriendlyName("msgEnemyDroppedBlock", "")] LocalisedString[] msgEnemyDroppedBlock, [FriendlyName("msgBlockDestroyed", "")] LocalisedString[] msgBlockDestroyed, [FriendlyName("msgPickUpBlock", "")] LocalisedString[] msgPickUpBlock, [FriendlyName("msgAttachBlock", "")] LocalisedString[] msgAttachBlock, [FriendlyName("msgPlayerHasBlock", "")] LocalisedString[] msgPlayerHasBlock, [FriendlyName("enemyTechData", "")] SpawnTechData[] enemyTechData)
	{
		external_22 = blockToGet;
		external_42 = msgEnemySpotted;
		external_47 = msgAttackTheEnemy;
		external_53 = msgEnemyDroppedBlock;
		external_26 = msgBlockDestroyed;
		external_57 = msgPickUpBlock;
		external_69 = msgAttachBlock;
		external_27 = msgPlayerHasBlock;
		external_23 = enemyTechData;
		Relay_In_30();
	}

	private void Relay_In_0()
	{
		logic_uScript_DoesPlayerHaveBlock_block_0 = external_22;
		logic_uScript_DoesPlayerHaveBlock_uScript_DoesPlayerHaveBlock_0.In(logic_uScript_DoesPlayerHaveBlock_block_0, logic_uScript_DoesPlayerHaveBlock_isDragging_0);
		bool num = logic_uScript_DoesPlayerHaveBlock_uScript_DoesPlayerHaveBlock_0.True;
		bool flag = logic_uScript_DoesPlayerHaveBlock_uScript_DoesPlayerHaveBlock_0.False;
		if (num)
		{
			Relay_True_32();
		}
		if (flag)
		{
			Relay_In_129();
		}
	}

	private void Relay_In_1()
	{
		int num = 0;
		Array array = external_27;
		if (logic_uScript_AddOnScreenMessage_locString_1.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_1, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_1, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_Return_1 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_1.In(logic_uScript_AddOnScreenMessage_locString_1, logic_uScript_AddOnScreenMessage_msgPriority_1, logic_uScript_AddOnScreenMessage_holdMsg_1, logic_uScript_AddOnScreenMessage_tag_1, logic_uScript_AddOnScreenMessage_speaker_1, logic_uScript_AddOnScreenMessage_side_1);
	}

	private void Relay_In_4()
	{
		logic_uScript_GetTankBlock_tank_4 = local_ReturnedEnemyTech_Tank;
		logic_uScript_GetTankBlock_blockType_4 = external_22;
		logic_uScript_GetTankBlock_Return_4 = logic_uScript_GetTankBlock_uScript_GetTankBlock_4.In(logic_uScript_GetTankBlock_tank_4, logic_uScript_GetTankBlock_blockType_4);
		local_ReturnedTargetBlock_TankBlock = logic_uScript_GetTankBlock_Return_4;
		if (logic_uScript_GetTankBlock_uScript_GetTankBlock_4.Returned)
		{
			Relay_In_9();
		}
	}

	private void Relay_In_5()
	{
		logic_uScript_GetNamedBlock_name_5 = local_TargetBlock_System_String;
		logic_uScript_GetNamedBlock_owner_5 = owner_Connection_8;
		logic_uScript_GetNamedBlock_Return_5 = logic_uScript_GetNamedBlock_uScript_GetNamedBlock_5.In(logic_uScript_GetNamedBlock_name_5, logic_uScript_GetNamedBlock_owner_5);
		external_28 = logic_uScript_GetNamedBlock_Return_5;
		local_ReturnedTargetBlock_TankBlock = logic_uScript_GetNamedBlock_Return_5;
		bool destroyed = logic_uScript_GetNamedBlock_uScript_GetNamedBlock_5.Destroyed;
		bool blockExists = logic_uScript_GetNamedBlock_uScript_GetNamedBlock_5.BlockExists;
		bool noBlock = logic_uScript_GetNamedBlock_uScript_GetNamedBlock_5.NoBlock;
		if (destroyed)
		{
			Relay_In_17();
		}
		if (blockExists)
		{
			Relay_In_7();
		}
		if (noBlock)
		{
			Relay_In_135();
		}
	}

	private void Relay_In_7()
	{
		logic_uScript_KeepBlockInvulnerable_block_7 = local_ReturnedTargetBlock_TankBlock;
		logic_uScript_KeepBlockInvulnerable_uScript_KeepBlockInvulnerable_7.In(logic_uScript_KeepBlockInvulnerable_block_7);
		if (logic_uScript_KeepBlockInvulnerable_uScript_KeepBlockInvulnerable_7.Out)
		{
			Relay_In_122();
		}
	}

	private void Relay_In_9()
	{
		logic_uScript_SaveNamedBlock_block_9 = local_ReturnedTargetBlock_TankBlock;
		logic_uScript_SaveNamedBlock_uniqueName_9 = local_TargetBlock_System_String;
		logic_uScript_SaveNamedBlock_owner_9 = owner_Connection_3;
		logic_uScript_SaveNamedBlock_uScript_SaveNamedBlock_9.In(logic_uScript_SaveNamedBlock_block_9, logic_uScript_SaveNamedBlock_uniqueName_9, logic_uScript_SaveNamedBlock_owner_9);
		if (logic_uScript_SaveNamedBlock_uScript_SaveNamedBlock_9.Out)
		{
			Relay_True_136();
		}
	}

	private void Relay_In_17()
	{
		int num = 0;
		Array array = external_26;
		if (logic_uScript_AddOnScreenMessage_locString_17.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_17, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_17, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_Return_17 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_17.In(logic_uScript_AddOnScreenMessage_locString_17, logic_uScript_AddOnScreenMessage_msgPriority_17, logic_uScript_AddOnScreenMessage_holdMsg_17, logic_uScript_AddOnScreenMessage_tag_17, logic_uScript_AddOnScreenMessage_speaker_17, logic_uScript_AddOnScreenMessage_side_17);
		if (logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_17.Out)
		{
			Relay_In_18();
		}
	}

	private void Relay_In_18()
	{
		logic_uScript_DiscoverBlock_blockType_18 = external_22;
		logic_uScript_DiscoverBlock_uScript_DiscoverBlock_18.In(logic_uScript_DiscoverBlock_blockType_18);
		if (logic_uScript_DiscoverBlock_uScript_DiscoverBlock_18.Out)
		{
			Relay_Fail_158();
		}
	}

	private void Relay_Connection_21()
	{
	}

	private void Relay_Connection_22()
	{
	}

	private void Relay_Connection_23()
	{
	}

	private void Relay_Connection_26()
	{
	}

	private void Relay_Connection_27()
	{
	}

	private void Relay_Connection_28()
	{
	}

	private void Relay_Connection_29()
	{
		if (this.Complete != null)
		{
			LogicEventArgs e = new LogicEventArgs();
			e.ReturnedTargetBlock = external_28;
			this.Complete(this, e);
		}
	}

	private void Relay_In_30()
	{
		logic_uScriptCon_CompareBool_Bool_30 = local_ObjectiveComplete_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_30.In(logic_uScriptCon_CompareBool_Bool_30);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_30.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_30.False;
		if (num)
		{
			Relay_Connection_29();
		}
		if (flag)
		{
			Relay_In_0();
		}
	}

	private void Relay_True_32()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_32.True(out logic_uScriptAct_SetBool_Target_32);
		local_ObjectiveComplete_System_Boolean = logic_uScriptAct_SetBool_Target_32;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_32.Out)
		{
			Relay_In_114();
		}
	}

	private void Relay_False_32()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_32.False(out logic_uScriptAct_SetBool_Target_32);
		local_ObjectiveComplete_System_Boolean = logic_uScriptAct_SetBool_Target_32;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_32.Out)
		{
			Relay_In_114();
		}
	}

	private void Relay_In_36()
	{
		logic_uScriptCon_CompareBool_Bool_36 = local_EnemySpotted_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_36.In(logic_uScriptCon_CompareBool_Bool_36);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_36.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_36.False;
		if (num)
		{
			Relay_In_61();
		}
		if (flag)
		{
			Relay_True_49();
		}
	}

	private void Relay_In_37()
	{
		logic_uScript_BlockAttachedToTech_tech_37 = local_ReturnedEnemyTech_Tank;
		logic_uScript_BlockAttachedToTech_block_37 = local_ReturnedTargetBlock_TankBlock;
		logic_uScript_BlockAttachedToTech_uScript_BlockAttachedToTech_37.In(logic_uScript_BlockAttachedToTech_tech_37, logic_uScript_BlockAttachedToTech_block_37);
		bool num = logic_uScript_BlockAttachedToTech_uScript_BlockAttachedToTech_37.True;
		bool flag = logic_uScript_BlockAttachedToTech_uScript_BlockAttachedToTech_37.False;
		if (num)
		{
			Relay_In_146();
		}
		if (flag)
		{
			Relay_In_141();
		}
	}

	private void Relay_In_41()
	{
		int num = 0;
		Array array = external_42;
		if (logic_uScript_AddOnScreenMessage_locString_41.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_41, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_41, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_tag_41 = local_msgGroup01_System_String;
		logic_uScript_AddOnScreenMessage_Return_41 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_41.In(logic_uScript_AddOnScreenMessage_locString_41, logic_uScript_AddOnScreenMessage_msgPriority_41, logic_uScript_AddOnScreenMessage_holdMsg_41, logic_uScript_AddOnScreenMessage_tag_41, logic_uScript_AddOnScreenMessage_speaker_41, logic_uScript_AddOnScreenMessage_side_41);
		local_EnemySpottedmsg_ManOnScreenMessages_OnScreenMessage = logic_uScript_AddOnScreenMessage_Return_41;
	}

	private void Relay_Connection_42()
	{
	}

	private void Relay_In_46()
	{
		int num = 0;
		Array array = external_47;
		if (logic_uScript_AddOnScreenMessage_locString_46.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_46, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_46, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_tag_46 = local_msgGroup01_System_String;
		logic_uScript_AddOnScreenMessage_Return_46 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_46.In(logic_uScript_AddOnScreenMessage_locString_46, logic_uScript_AddOnScreenMessage_msgPriority_46, logic_uScript_AddOnScreenMessage_holdMsg_46, logic_uScript_AddOnScreenMessage_tag_46, logic_uScript_AddOnScreenMessage_speaker_46, logic_uScript_AddOnScreenMessage_side_46);
		local_AttackTheEnemymsg_ManOnScreenMessages_OnScreenMessage = logic_uScript_AddOnScreenMessage_Return_46;
	}

	private void Relay_Connection_47()
	{
	}

	private void Relay_True_49()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_49.True(out logic_uScriptAct_SetBool_Target_49);
		local_EnemySpotted_System_Boolean = logic_uScriptAct_SetBool_Target_49;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_49.Out)
		{
			Relay_In_41();
		}
	}

	private void Relay_False_49()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_49.False(out logic_uScriptAct_SetBool_Target_49);
		local_EnemySpotted_System_Boolean = logic_uScriptAct_SetBool_Target_49;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_49.Out)
		{
			Relay_In_41();
		}
	}

	private void Relay_In_50()
	{
		logic_uScriptCon_CompareBool_Bool_50 = local_NotifiedDroppedBlock_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_50.In(logic_uScriptCon_CompareBool_Bool_50);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_50.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_50.False;
		if (num)
		{
			Relay_In_63();
		}
		if (flag)
		{
			Relay_True_54();
		}
	}

	private void Relay_In_52()
	{
		int num = 0;
		Array array = external_53;
		if (logic_uScript_AddOnScreenMessage_locString_52.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_52, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_52, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_Return_52 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_52.In(logic_uScript_AddOnScreenMessage_locString_52, logic_uScript_AddOnScreenMessage_msgPriority_52, logic_uScript_AddOnScreenMessage_holdMsg_52, logic_uScript_AddOnScreenMessage_tag_52, logic_uScript_AddOnScreenMessage_speaker_52, logic_uScript_AddOnScreenMessage_side_52);
		local_EnemyDroppedBlockmsg_ManOnScreenMessages_OnScreenMessage = logic_uScript_AddOnScreenMessage_Return_52;
	}

	private void Relay_Connection_53()
	{
	}

	private void Relay_True_54()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_54.True(out logic_uScriptAct_SetBool_Target_54);
		local_NotifiedDroppedBlock_System_Boolean = logic_uScriptAct_SetBool_Target_54;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_54.Out)
		{
			Relay_In_117();
		}
	}

	private void Relay_False_54()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_54.False(out logic_uScriptAct_SetBool_Target_54);
		local_NotifiedDroppedBlock_System_Boolean = logic_uScriptAct_SetBool_Target_54;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_54.Out)
		{
			Relay_In_117();
		}
	}

	private void Relay_In_56()
	{
		int num = 0;
		Array array = external_57;
		if (logic_uScript_AddOnScreenMessage_locString_56.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_56, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_56, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_tag_56 = local_msgGroup02_System_String;
		logic_uScript_AddOnScreenMessage_Return_56 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_56.In(logic_uScript_AddOnScreenMessage_locString_56, logic_uScript_AddOnScreenMessage_msgPriority_56, logic_uScript_AddOnScreenMessage_holdMsg_56, logic_uScript_AddOnScreenMessage_tag_56, logic_uScript_AddOnScreenMessage_speaker_56, logic_uScript_AddOnScreenMessage_side_56);
		local_PickUpBlockmsg_ManOnScreenMessages_OnScreenMessage = logic_uScript_AddOnScreenMessage_Return_56;
	}

	private void Relay_Connection_57()
	{
	}

	private void Relay_In_61()
	{
		logic_uScript_IsPlayerInRangeOfVisible_visibleObject_61 = local_ReturnedEnemyTech_Tank;
		logic_uScript_IsPlayerInRangeOfVisible_uScript_IsPlayerInRangeOfVisible_61.In(logic_uScript_IsPlayerInRangeOfVisible_visibleObject_61, logic_uScript_IsPlayerInRangeOfVisible_range_61);
		if (logic_uScript_IsPlayerInRangeOfVisible_uScript_IsPlayerInRangeOfVisible_61.InRange)
		{
			Relay_In_100();
		}
	}

	private void Relay_In_62()
	{
		logic_uScript_IsPlayerInRangeOfVisible_visibleObject_62 = local_ReturnedEnemyTech_Tank;
		logic_uScript_IsPlayerInRangeOfVisible_uScript_IsPlayerInRangeOfVisible_62.In(logic_uScript_IsPlayerInRangeOfVisible_visibleObject_62, logic_uScript_IsPlayerInRangeOfVisible_range_62);
		if (logic_uScript_IsPlayerInRangeOfVisible_uScript_IsPlayerInRangeOfVisible_62.InRange)
		{
			Relay_In_36();
		}
	}

	private void Relay_In_63()
	{
		logic_uScript_IsPlayerInRangeOfVisible_visibleObject_63 = local_ReturnedTargetBlock_TankBlock;
		logic_uScript_IsPlayerInRangeOfVisible_uScript_IsPlayerInRangeOfVisible_63.In(logic_uScript_IsPlayerInRangeOfVisible_visibleObject_63, logic_uScript_IsPlayerInRangeOfVisible_range_63);
		bool inRange = logic_uScript_IsPlayerInRangeOfVisible_uScript_IsPlayerInRangeOfVisible_63.InRange;
		bool outOfRange = logic_uScript_IsPlayerInRangeOfVisible_uScript_IsPlayerInRangeOfVisible_63.OutOfRange;
		if (inRange)
		{
			Relay_In_120();
		}
		if (outOfRange)
		{
			Relay_In_119();
		}
	}

	private void Relay_In_66()
	{
		logic_uScript_IsPlayerInteractingWithBlock_block_66 = local_ReturnedTargetBlock_TankBlock;
		logic_uScript_IsPlayerInteractingWithBlock_uScript_IsPlayerInteractingWithBlock_66.In(logic_uScript_IsPlayerInteractingWithBlock_block_66);
		bool interacted = logic_uScript_IsPlayerInteractingWithBlock_uScript_IsPlayerInteractingWithBlock_66.Interacted;
		bool notInteracted = logic_uScript_IsPlayerInteractingWithBlock_uScript_IsPlayerInteractingWithBlock_66.NotInteracted;
		bool dragging = logic_uScript_IsPlayerInteractingWithBlock_uScript_IsPlayerInteractingWithBlock_66.Dragging;
		if (interacted)
		{
			Relay_In_56();
		}
		if (notInteracted)
		{
			Relay_In_56();
		}
		if (dragging)
		{
			Relay_In_68();
		}
	}

	private void Relay_In_68()
	{
		int num = 0;
		Array array = external_69;
		if (logic_uScript_AddOnScreenMessage_locString_68.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_68, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_68, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_tag_68 = local_msgGroup02_System_String;
		logic_uScript_AddOnScreenMessage_Return_68 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_68.In(logic_uScript_AddOnScreenMessage_locString_68, logic_uScript_AddOnScreenMessage_msgPriority_68, logic_uScript_AddOnScreenMessage_holdMsg_68, logic_uScript_AddOnScreenMessage_tag_68, logic_uScript_AddOnScreenMessage_speaker_68, logic_uScript_AddOnScreenMessage_side_68);
		local_AttachBlockmsg_ManOnScreenMessages_OnScreenMessage = logic_uScript_AddOnScreenMessage_Return_68;
	}

	private void Relay_Connection_69()
	{
	}

	private void Relay_True_71()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_71.True(out logic_uScriptAct_SetBool_Target_71);
		local_EnemyDead_System_Boolean = logic_uScriptAct_SetBool_Target_71;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_71.Out)
		{
			Relay_In_5();
		}
	}

	private void Relay_False_71()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_71.False(out logic_uScriptAct_SetBool_Target_71);
		local_EnemyDead_System_Boolean = logic_uScriptAct_SetBool_Target_71;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_71.Out)
		{
			Relay_In_5();
		}
	}

	private void Relay_SaveEvent_74()
	{
		Relay_In_76();
	}

	private void Relay_LoadEvent_74()
	{
		Relay_In_77();
	}

	private void Relay_RestartEvent_74()
	{
		Relay_False_80();
	}

	private void Relay_In_76()
	{
		logic_uScript_SaveVariable_variable_76 = local_EnemySpotted_System_Boolean;
		logic_uScript_SaveVariable_owner_76 = owner_Connection_78;
		logic_uScript_SaveVariable_uniqueId_76 = local_EnemySpottedID_System_String;
		logic_uScript_SaveVariable_uScript_SaveVariable_76.In(logic_uScript_SaveVariable_variable_76, logic_uScript_SaveVariable_owner_76, logic_uScript_SaveVariable_uniqueId_76);
		if (logic_uScript_SaveVariable_uScript_SaveVariable_76.Out)
		{
			Relay_In_83();
		}
	}

	private void Relay_In_77()
	{
		logic_uScript_LoadBool_data_77 = local_EnemySpotted_System_Boolean;
		logic_uScript_LoadBool_owner_77 = owner_Connection_78;
		logic_uScript_LoadBool_uniqueName_77 = local_EnemySpottedID_System_String;
		logic_uScript_LoadBool_uScript_LoadBool_77.In(ref logic_uScript_LoadBool_data_77, logic_uScript_LoadBool_owner_77, logic_uScript_LoadBool_uniqueName_77);
		local_EnemySpotted_System_Boolean = logic_uScript_LoadBool_data_77;
		if (logic_uScript_LoadBool_uScript_LoadBool_77.Out)
		{
			Relay_In_91();
		}
	}

	private void Relay_True_80()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_80.True(out logic_uScriptAct_SetBool_Target_80);
		local_EnemySpotted_System_Boolean = logic_uScriptAct_SetBool_Target_80;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_80.Out)
		{
			Relay_False_94();
		}
	}

	private void Relay_False_80()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_80.False(out logic_uScriptAct_SetBool_Target_80);
		local_EnemySpotted_System_Boolean = logic_uScriptAct_SetBool_Target_80;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_80.Out)
		{
			Relay_False_94();
		}
	}

	private void Relay_In_83()
	{
		logic_uScript_SaveVariable_variable_83 = local_ObjectiveComplete_System_Boolean;
		logic_uScript_SaveVariable_owner_83 = owner_Connection_84;
		logic_uScript_SaveVariable_uniqueId_83 = local_ObjectiveCompleteID_System_String;
		logic_uScript_SaveVariable_uScript_SaveVariable_83.In(logic_uScript_SaveVariable_variable_83, logic_uScript_SaveVariable_owner_83, logic_uScript_SaveVariable_uniqueId_83);
		if (logic_uScript_SaveVariable_uScript_SaveVariable_83.Out)
		{
			Relay_In_86();
		}
	}

	private void Relay_In_86()
	{
		logic_uScript_SaveVariable_variable_86 = local_EnemyDead_System_Boolean;
		logic_uScript_SaveVariable_owner_86 = owner_Connection_87;
		logic_uScript_SaveVariable_uniqueId_86 = local_EnemyDeadID_System_String;
		logic_uScript_SaveVariable_uScript_SaveVariable_86.In(logic_uScript_SaveVariable_variable_86, logic_uScript_SaveVariable_owner_86, logic_uScript_SaveVariable_uniqueId_86);
		if (logic_uScript_SaveVariable_uScript_SaveVariable_86.Out)
		{
			Relay_In_89();
		}
	}

	private void Relay_In_89()
	{
		logic_uScript_SaveVariable_variable_89 = local_NotifiedDroppedBlock_System_Boolean;
		logic_uScript_SaveVariable_owner_89 = owner_Connection_90;
		logic_uScript_SaveVariable_uniqueId_89 = local_NotifiedDroppedBlockID_System_String;
		logic_uScript_SaveVariable_uScript_SaveVariable_89.In(logic_uScript_SaveVariable_variable_89, logic_uScript_SaveVariable_owner_89, logic_uScript_SaveVariable_uniqueId_89);
		if (logic_uScript_SaveVariable_uScript_SaveVariable_89.Out)
		{
			Relay_In_106();
		}
	}

	private void Relay_In_91()
	{
		logic_uScript_LoadBool_data_91 = local_ObjectiveComplete_System_Boolean;
		logic_uScript_LoadBool_owner_91 = owner_Connection_84;
		logic_uScript_LoadBool_uniqueName_91 = local_ObjectiveCompleteID_System_String;
		logic_uScript_LoadBool_uScript_LoadBool_91.In(ref logic_uScript_LoadBool_data_91, logic_uScript_LoadBool_owner_91, logic_uScript_LoadBool_uniqueName_91);
		local_ObjectiveComplete_System_Boolean = logic_uScript_LoadBool_data_91;
		if (logic_uScript_LoadBool_uScript_LoadBool_91.Out)
		{
			Relay_In_92();
		}
	}

	private void Relay_In_92()
	{
		logic_uScript_LoadBool_data_92 = local_EnemyDead_System_Boolean;
		logic_uScript_LoadBool_owner_92 = owner_Connection_87;
		logic_uScript_LoadBool_uniqueName_92 = local_EnemyDeadID_System_String;
		logic_uScript_LoadBool_uScript_LoadBool_92.In(ref logic_uScript_LoadBool_data_92, logic_uScript_LoadBool_owner_92, logic_uScript_LoadBool_uniqueName_92);
		local_EnemyDead_System_Boolean = logic_uScript_LoadBool_data_92;
		if (logic_uScript_LoadBool_uScript_LoadBool_92.Out)
		{
			Relay_In_93();
		}
	}

	private void Relay_In_93()
	{
		logic_uScript_LoadBool_data_93 = local_NotifiedDroppedBlock_System_Boolean;
		logic_uScript_LoadBool_owner_93 = owner_Connection_90;
		logic_uScript_LoadBool_uniqueName_93 = local_NotifiedDroppedBlockID_System_String;
		logic_uScript_LoadBool_uScript_LoadBool_93.In(ref logic_uScript_LoadBool_data_93, logic_uScript_LoadBool_owner_93, logic_uScript_LoadBool_uniqueName_93);
		local_NotifiedDroppedBlock_System_Boolean = logic_uScript_LoadBool_data_93;
		if (logic_uScript_LoadBool_uScript_LoadBool_93.Out)
		{
			Relay_In_107();
		}
	}

	private void Relay_True_94()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_94.True(out logic_uScriptAct_SetBool_Target_94);
		local_ObjectiveComplete_System_Boolean = logic_uScriptAct_SetBool_Target_94;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_94.Out)
		{
			Relay_False_95();
		}
	}

	private void Relay_False_94()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_94.False(out logic_uScriptAct_SetBool_Target_94);
		local_ObjectiveComplete_System_Boolean = logic_uScriptAct_SetBool_Target_94;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_94.Out)
		{
			Relay_False_95();
		}
	}

	private void Relay_True_95()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_95.True(out logic_uScriptAct_SetBool_Target_95);
		local_EnemyDead_System_Boolean = logic_uScriptAct_SetBool_Target_95;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_95.Out)
		{
			Relay_False_96();
		}
	}

	private void Relay_False_95()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_95.False(out logic_uScriptAct_SetBool_Target_95);
		local_EnemyDead_System_Boolean = logic_uScriptAct_SetBool_Target_95;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_95.Out)
		{
			Relay_False_96();
		}
	}

	private void Relay_True_96()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_96.True(out logic_uScriptAct_SetBool_Target_96);
		local_NotifiedDroppedBlock_System_Boolean = logic_uScriptAct_SetBool_Target_96;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_96.Out)
		{
			Relay_False_108();
		}
	}

	private void Relay_False_96()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_96.False(out logic_uScriptAct_SetBool_Target_96);
		local_NotifiedDroppedBlock_System_Boolean = logic_uScriptAct_SetBool_Target_96;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_96.Out)
		{
			Relay_False_108();
		}
	}

	private void Relay_In_100()
	{
		logic_uScriptCon_CompareBool_Bool_100 = local_NotifiedAttackEnemy_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_100.In(logic_uScriptCon_CompareBool_Bool_100);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_100.False)
		{
			Relay_True_102();
		}
	}

	private void Relay_True_102()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_102.True(out logic_uScriptAct_SetBool_Target_102);
		local_NotifiedAttackEnemy_System_Boolean = logic_uScriptAct_SetBool_Target_102;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_102.Out)
		{
			Relay_In_46();
		}
	}

	private void Relay_False_102()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_102.False(out logic_uScriptAct_SetBool_Target_102);
		local_NotifiedAttackEnemy_System_Boolean = logic_uScriptAct_SetBool_Target_102;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_102.Out)
		{
			Relay_In_46();
		}
	}

	private void Relay_In_106()
	{
		logic_uScript_SaveVariable_variable_106 = local_NotifiedAttackEnemy_System_Boolean;
		logic_uScript_SaveVariable_owner_106 = owner_Connection_105;
		logic_uScript_SaveVariable_uniqueId_106 = local_NotifiedAttackEnemyID_System_String;
		logic_uScript_SaveVariable_uScript_SaveVariable_106.In(logic_uScript_SaveVariable_variable_106, logic_uScript_SaveVariable_owner_106, logic_uScript_SaveVariable_uniqueId_106);
		if (logic_uScript_SaveVariable_uScript_SaveVariable_106.Out)
		{
			Relay_In_155();
		}
	}

	private void Relay_In_107()
	{
		logic_uScript_LoadBool_data_107 = local_NotifiedAttackEnemy_System_Boolean;
		logic_uScript_LoadBool_owner_107 = owner_Connection_105;
		logic_uScript_LoadBool_uniqueName_107 = local_NotifiedAttackEnemyID_System_String;
		logic_uScript_LoadBool_uScript_LoadBool_107.In(ref logic_uScript_LoadBool_data_107, logic_uScript_LoadBool_owner_107, logic_uScript_LoadBool_uniqueName_107);
		local_NotifiedAttackEnemy_System_Boolean = logic_uScript_LoadBool_data_107;
		if (logic_uScript_LoadBool_uScript_LoadBool_107.Out)
		{
			Relay_In_153();
		}
	}

	private void Relay_True_108()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_108.True(out logic_uScriptAct_SetBool_Target_108);
		local_NotifiedAttackEnemy_System_Boolean = logic_uScriptAct_SetBool_Target_108;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_108.Out)
		{
			Relay_False_151();
		}
	}

	private void Relay_False_108()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_108.False(out logic_uScriptAct_SetBool_Target_108);
		local_NotifiedAttackEnemy_System_Boolean = logic_uScriptAct_SetBool_Target_108;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_108.Out)
		{
			Relay_False_151();
		}
	}

	private void Relay_In_114()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_114 = local_msgGroup02_System_String;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_114.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_114, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_114);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_114.Out)
		{
			Relay_In_1();
		}
	}

	private void Relay_In_117()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_117 = local_msgGroup01_System_String;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_117.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_117, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_117);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_117.Out)
		{
			Relay_In_52();
		}
	}

	private void Relay_In_119()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_119 = local_msgGroup02_System_String;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_119.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_119, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_119);
	}

	private void Relay_In_120()
	{
		logic_uScript_IsOnScreenMessage_onScreenMessage_120 = local_EnemyDroppedBlockmsg_ManOnScreenMessages_OnScreenMessage;
		logic_uScript_IsOnScreenMessage_uScript_IsOnScreenMessage_120.In(logic_uScript_IsOnScreenMessage_onScreenMessage_120);
		if (logic_uScript_IsOnScreenMessage_uScript_IsOnScreenMessage_120.False)
		{
			Relay_In_66();
		}
	}

	private void Relay_In_122()
	{
		logic_uScriptCon_CompareBool_Bool_122 = local_EnemyDead_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_122.In(logic_uScriptCon_CompareBool_Bool_122);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_122.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_122.False;
		if (num)
		{
			Relay_In_141();
		}
		if (flag)
		{
			Relay_In_37();
		}
	}

	private void Relay_AtIndex_124()
	{
		int num = 0;
		Array array = local_126_TankArray;
		if (logic_uScript_AccessListTech_techList_124.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_124, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_124, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_124.AtIndex(ref logic_uScript_AccessListTech_techList_124, logic_uScript_AccessListTech_index_124, out logic_uScript_AccessListTech_value_124);
		local_126_TankArray = logic_uScript_AccessListTech_techList_124;
		local_ReturnedEnemyTech_Tank = logic_uScript_AccessListTech_value_124;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_124.Out)
		{
			Relay_In_5();
		}
	}

	private void Relay_InitialSpawn_127()
	{
		int num = 0;
		Array array = external_23;
		if (logic_uScript_SpawnTechsFromData_spawnData_127.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_127, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SpawnTechsFromData_spawnData_127, num, array.Length);
		num += array.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_127 = owner_Connection_128;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_127.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_127, logic_uScript_SpawnTechsFromData_ownerNode_127, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_127);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_127.Out)
		{
			Relay_In_130();
		}
	}

	private void Relay_In_129()
	{
		logic_uScriptCon_CompareBool_Bool_129 = local_TechsSpawned_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_129.In(logic_uScriptCon_CompareBool_Bool_129);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_129.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_129.False;
		if (num)
		{
			Relay_In_130();
		}
		if (flag)
		{
			Relay_True_132();
		}
	}

	private void Relay_In_130()
	{
		int num = 0;
		Array array = external_23;
		if (logic_uScript_GetAndCheckTechs_techData_130.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_130, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techData_130, num, array.Length);
		num += array.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_130 = owner_Connection_125;
		int num2 = 0;
		Array array2 = local_126_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_130.Length != num2 + array2.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_130, num2 + array2.Length);
		}
		Array.Copy(array2, 0, logic_uScript_GetAndCheckTechs_techs_130, num2, array2.Length);
		num2 += array2.Length;
		logic_uScript_GetAndCheckTechs_Return_130 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_130.In(logic_uScript_GetAndCheckTechs_techData_130, logic_uScript_GetAndCheckTechs_ownerNode_130, ref logic_uScript_GetAndCheckTechs_techs_130);
		local_126_TankArray = logic_uScript_GetAndCheckTechs_techs_130;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_130.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_130.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_130.AllDead;
		if (allAlive)
		{
			Relay_AtIndex_124();
		}
		if (someAlive)
		{
			Relay_AtIndex_124();
		}
		if (allDead)
		{
			Relay_True_71();
		}
	}

	private void Relay_True_132()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_132.True(out logic_uScriptAct_SetBool_Target_132);
		local_TechsSpawned_System_Boolean = logic_uScriptAct_SetBool_Target_132;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_132.Out)
		{
			Relay_InitialSpawn_127();
		}
	}

	private void Relay_False_132()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_132.False(out logic_uScriptAct_SetBool_Target_132);
		local_TechsSpawned_System_Boolean = logic_uScriptAct_SetBool_Target_132;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_132.Out)
		{
			Relay_InitialSpawn_127();
		}
	}

	private void Relay_In_135()
	{
		logic_uScriptCon_CompareBool_Bool_135 = local_TargetBlockCached_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_135.In(logic_uScriptCon_CompareBool_Bool_135);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_135.False)
		{
			Relay_In_4();
		}
	}

	private void Relay_True_136()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_136.True(out logic_uScriptAct_SetBool_Target_136);
		local_TargetBlockCached_System_Boolean = logic_uScriptAct_SetBool_Target_136;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_136.Out)
		{
			Relay_In_7();
		}
	}

	private void Relay_False_136()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_136.False(out logic_uScriptAct_SetBool_Target_136);
		local_TargetBlockCached_System_Boolean = logic_uScriptAct_SetBool_Target_136;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_136.Out)
		{
			Relay_In_7();
		}
	}

	private void Relay_In_138()
	{
		logic_uScript_SetEncounterTarget_owner_138 = owner_Connection_139;
		logic_uScript_SetEncounterTarget_visibleObject_138 = local_ReturnedTargetBlock_TankBlock;
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_138.In(logic_uScript_SetEncounterTarget_owner_138, logic_uScript_SetEncounterTarget_visibleObject_138);
		if (logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_138.Out)
		{
			Relay_In_50();
		}
	}

	private void Relay_In_141()
	{
		logic_uScript_MoveEncounterWithVisible_ownerNode_141 = owner_Connection_142;
		logic_uScript_MoveEncounterWithVisible_visibleObject_141 = local_ReturnedTargetBlock_TankBlock;
		logic_uScript_MoveEncounterWithVisible_uScript_MoveEncounterWithVisible_141.In(logic_uScript_MoveEncounterWithVisible_ownerNode_141, logic_uScript_MoveEncounterWithVisible_visibleObject_141);
		if (logic_uScript_MoveEncounterWithVisible_uScript_MoveEncounterWithVisible_141.Out)
		{
			Relay_In_138();
		}
	}

	private void Relay_In_146()
	{
		logic_uScript_MoveEncounterWithVisible_ownerNode_146 = owner_Connection_145;
		logic_uScript_MoveEncounterWithVisible_visibleObject_146 = local_ReturnedEnemyTech_Tank;
		logic_uScript_MoveEncounterWithVisible_uScript_MoveEncounterWithVisible_146.In(logic_uScript_MoveEncounterWithVisible_ownerNode_146, logic_uScript_MoveEncounterWithVisible_visibleObject_146);
		if (logic_uScript_MoveEncounterWithVisible_uScript_MoveEncounterWithVisible_146.Out)
		{
			Relay_In_148();
		}
	}

	private void Relay_In_148()
	{
		logic_uScript_SetEncounterTarget_owner_148 = owner_Connection_149;
		logic_uScript_SetEncounterTarget_visibleObject_148 = local_ReturnedEnemyTech_Tank;
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_148.In(logic_uScript_SetEncounterTarget_owner_148, logic_uScript_SetEncounterTarget_visibleObject_148);
		if (logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_148.Out)
		{
			Relay_In_62();
		}
	}

	private void Relay_True_151()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_151.True(out logic_uScriptAct_SetBool_Target_151);
		local_TechsSpawned_System_Boolean = logic_uScriptAct_SetBool_Target_151;
	}

	private void Relay_False_151()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_151.False(out logic_uScriptAct_SetBool_Target_151);
		local_TechsSpawned_System_Boolean = logic_uScriptAct_SetBool_Target_151;
	}

	private void Relay_In_153()
	{
		logic_uScript_LoadBool_data_153 = local_TechsSpawned_System_Boolean;
		logic_uScript_LoadBool_owner_153 = owner_Connection_152;
		logic_uScript_LoadBool_uniqueName_153 = local_TechsSpawnedID_System_String;
		logic_uScript_LoadBool_uScript_LoadBool_153.In(ref logic_uScript_LoadBool_data_153, logic_uScript_LoadBool_owner_153, logic_uScript_LoadBool_uniqueName_153);
		local_TechsSpawned_System_Boolean = logic_uScript_LoadBool_data_153;
	}

	private void Relay_In_155()
	{
		logic_uScript_SaveVariable_variable_155 = local_TechsSpawned_System_Boolean;
		logic_uScript_SaveVariable_owner_155 = owner_Connection_152;
		logic_uScript_SaveVariable_uniqueId_155 = local_TechsSpawnedID_System_String;
		logic_uScript_SaveVariable_uScript_SaveVariable_155.In(logic_uScript_SaveVariable_variable_155, logic_uScript_SaveVariable_owner_155, logic_uScript_SaveVariable_uniqueId_155);
	}

	private void Relay_Succeed_158()
	{
		logic_uScript_FinishEncounter_owner_158 = owner_Connection_157;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_158.Succeed(logic_uScript_FinishEncounter_owner_158);
	}

	private void Relay_Fail_158()
	{
		logic_uScript_FinishEncounter_owner_158 = owner_Connection_157;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_158.Fail(logic_uScript_FinishEncounter_owner_158);
	}
}
