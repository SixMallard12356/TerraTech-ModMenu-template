using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[FriendlyName("Untitled", "")]
[NodePath("Graphs")]
public class Mission_GSO_1_3_FightFirstEnemy : uScriptLogic
{
	private GameObject parentGameObject;

	private uScript_GUI thisScriptsOnGuiListener;

	private bool m_RegisteredForEvents;

	public float distDriveTutorialCheck;

	public float distDriveWithoutFiringCheck;

	public float distDriveWithoutScavengingCheck;

	public float distRunFromEnemyCheck;

	public float enemyFightTimeoutSecs;

	public float enemyForwardSpawnDist;

	public TankPreset enemyPreset;

	public float enemyThrottleDriveModifer;

	public float enemyThrottleTurnModifer;

	private GameHints.HintID local_109_GameHints_HintID = GameHints.HintID.RotateBlock;

	private float local_20_System_Single;

	private bool local_BlockAttachedOrDrivenAway_System_Boolean;

	private bool local_EnemySpawned_System_Boolean;

	private bool local_Msg04FightEnemyShown_System_Boolean;

	private bool local_SetEnemyBlocks_System_Boolean;

	private int local_Stage_System_Int32 = 1;

	private List<TankBlock> local_TonysBlocks_System_Collections_Generic_List_1_TankBlock_;

	private Tank local_TonyTank_Tank;

	public uScript_AddMessage.MessageSpeaker messageSpeaker;

	[Multiline(1)]
	public string messageTag = "";

	public uScript_AddMessage.MessageData msg01TryDriving;

	public uScript_AddMessage.MessageData msg01TryDriving_Pad;

	public uScript_AddMessage.MessageData msg02TryShooting;

	public uScript_AddMessage.MessageData msg02TryShooting_Pad;

	public uScript_AddMessage.MessageData msg03EnemyIncoming;

	public uScript_AddMessage.MessageData msg04FightEnemy;

	public uScript_AddMessage.MessageData msg05ShootReminder;

	public uScript_AddMessage.MessageData msg05ShootReminder_Pad;

	public uScript_AddMessage.MessageData msg06ScavengeBlocks;

	public uScript_AddMessage.MessageData msg06ScavengeBlocks_Pad;

	private GameObject owner_Connection_8;

	private GameObject owner_Connection_21;

	private GameObject owner_Connection_25;

	private GameObject owner_Connection_43;

	private GameObject owner_Connection_46;

	private GameObject owner_Connection_57;

	private uScript_BlockDecay logic_uScript_BlockDecay_uScript_BlockDecay_0 = new uScript_BlockDecay();

	private bool logic_uScript_BlockDecay_allow_0 = true;

	private bool logic_uScript_BlockDecay_Out_0 = true;

	private uScript_SkipTutorial logic_uScript_SkipTutorial_uScript_SkipTutorial_1 = new uScript_SkipTutorial();

	private bool logic_uScript_SkipTutorial_Yes_1 = true;

	private bool logic_uScript_SkipTutorial_No_1 = true;

	private uScriptCon_ManualSwitch logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_2 = new uScriptCon_ManualSwitch();

	private int logic_uScriptCon_ManualSwitch_CurrentOutput_2;

	private uScript_HasPlayerTraveledDistance logic_uScript_HasPlayerTraveledDistance_uScript_HasPlayerTraveledDistance_4 = new uScript_HasPlayerTraveledDistance();

	private float logic_uScript_HasPlayerTraveledDistance_distance_4;

	private bool logic_uScript_HasPlayerTraveledDistance_True_4 = true;

	private bool logic_uScript_HasPlayerTraveledDistance_False_4 = true;

	private uScript_HasPlayerTraveledDistance logic_uScript_HasPlayerTraveledDistance_uScript_HasPlayerTraveledDistance_5 = new uScript_HasPlayerTraveledDistance();

	private float logic_uScript_HasPlayerTraveledDistance_distance_5;

	private bool logic_uScript_HasPlayerTraveledDistance_True_5 = true;

	private bool logic_uScript_HasPlayerTraveledDistance_False_5 = true;

	private uScript_HasPlayerFiredGun logic_uScript_HasPlayerFiredGun_uScript_HasPlayerFiredGun_6 = new uScript_HasPlayerFiredGun();

	private bool logic_uScript_HasPlayerFiredGun_True_6 = true;

	private bool logic_uScript_HasPlayerFiredGun_False_6 = true;

	private uScript_HasAnyBlockBeenAttached logic_uScript_HasAnyBlockBeenAttached_uScript_HasAnyBlockBeenAttached_10 = new uScript_HasAnyBlockBeenAttached();

	private List<TankBlock> logic_uScript_HasAnyBlockBeenAttached_blocks_10;

	private bool logic_uScript_HasAnyBlockBeenAttached_Out_10 = true;

	private bool logic_uScript_HasAnyBlockBeenAttached_True_10 = true;

	private bool logic_uScript_HasAnyBlockBeenAttached_False_10 = true;

	private uScript_HasPlayerTraveledDistance logic_uScript_HasPlayerTraveledDistance_uScript_HasPlayerTraveledDistance_12 = new uScript_HasPlayerTraveledDistance();

	private float logic_uScript_HasPlayerTraveledDistance_distance_12;

	private bool logic_uScript_HasPlayerTraveledDistance_True_12 = true;

	private bool logic_uScript_HasPlayerTraveledDistance_False_12 = true;

	private uScript_GetTankBlocks logic_uScript_GetTankBlocks_uScript_GetTankBlocks_14 = new uScript_GetTankBlocks();

	private Tank logic_uScript_GetTankBlocks_tank_14;

	private List<TankBlock> logic_uScript_GetTankBlocks_Return_14;

	private bool logic_uScript_GetTankBlocks_Out_14 = true;

	private uScript_HealDamageFromSource logic_uScript_HealDamageFromSource_uScript_HealDamageFromSource_15 = new uScript_HealDamageFromSource();

	private Tank logic_uScript_HealDamageFromSource_source_15;

	private bool logic_uScript_HealDamageFromSource_Out_15 = true;

	private uScriptCon_CompareFloat logic_uScriptCon_CompareFloat_uScriptCon_CompareFloat_18 = new uScriptCon_CompareFloat();

	private float logic_uScriptCon_CompareFloat_A_18;

	private float logic_uScriptCon_CompareFloat_B_18;

	private bool logic_uScriptCon_CompareFloat_GreaterThan_18 = true;

	private bool logic_uScriptCon_CompareFloat_GreaterThanOrEqualTo_18 = true;

	private bool logic_uScriptCon_CompareFloat_EqualTo_18 = true;

	private bool logic_uScriptCon_CompareFloat_NotEqualTo_18 = true;

	private bool logic_uScriptCon_CompareFloat_LessThanOrEqualTo_18 = true;

	private bool logic_uScriptCon_CompareFloat_LessThan_18 = true;

	private uScriptAct_Stopwatch logic_uScriptAct_Stopwatch_uScriptAct_Stopwatch_19 = new uScriptAct_Stopwatch();

	private float logic_uScriptAct_Stopwatch_Seconds_19;

	private bool logic_uScriptAct_Stopwatch_Started_19 = true;

	private bool logic_uScriptAct_Stopwatch_Stopped_19 = true;

	private bool logic_uScriptAct_Stopwatch_Reset_19 = true;

	private bool logic_uScriptAct_Stopwatch_CheckedTime_19 = true;

	private uScript_SetPopulation logic_uScript_SetPopulation_uScript_SetPopulation_23 = new uScript_SetPopulation();

	private bool logic_uScript_SetPopulation_automatic_23 = true;

	private bool logic_uScript_SetPopulation_Out_23 = true;

	private uScript_Save logic_uScript_Save_uScript_Save_24 = new uScript_Save();

	private ManFTUE.SaveStates logic_uScript_Save_state_24 = ManFTUE.SaveStates.Normal;

	private bool logic_uScript_Save_Out_24 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_27 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_27 = 4;

	private int logic_uScriptAct_SetInt_Target_27;

	private bool logic_uScriptAct_SetInt_Out_27 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_28 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_28;

	private bool logic_uScriptCon_CompareBool_True_28 = true;

	private bool logic_uScriptCon_CompareBool_False_28 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_29 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_29;

	private bool logic_uScriptAct_SetBool_Out_29 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_29 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_29 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_31 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_31 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_31 = true;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_31 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_34 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_34 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_34 = true;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_34 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_36 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_36 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_36 = true;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_36 = true;

	private uScript_Wait logic_uScript_Wait_uScript_Wait_37 = new uScript_Wait();

	private float logic_uScript_Wait_seconds_37 = 2f;

	private bool logic_uScript_Wait_repeat_37;

	private bool logic_uScript_Wait_Waited_37 = true;

	private uScript_SpawnEnemyInFrontOfPlayer logic_uScript_SpawnEnemyInFrontOfPlayer_uScript_SpawnEnemyInFrontOfPlayer_45 = new uScript_SpawnEnemyInFrontOfPlayer();

	private TankPreset logic_uScript_SpawnEnemyInFrontOfPlayer_preset_45;

	private string logic_uScript_SpawnEnemyInFrontOfPlayer_uniqueName_45 = "tony";

	private float logic_uScript_SpawnEnemyInFrontOfPlayer_distFromPlayer_45;

	private GameObject logic_uScript_SpawnEnemyInFrontOfPlayer_ownerNode_45;

	private bool logic_uScript_SpawnEnemyInFrontOfPlayer_Spawned_45 = true;

	private bool logic_uScript_SpawnEnemyInFrontOfPlayer_NotSpawned_45 = true;

	private uScript_GetNamedTech logic_uScript_GetNamedTech_uScript_GetNamedTech_47 = new uScript_GetNamedTech();

	private string logic_uScript_GetNamedTech_name_47 = "tony";

	private GameObject logic_uScript_GetNamedTech_ownerNode_47;

	private Tank logic_uScript_GetNamedTech_tech_47;

	private bool logic_uScript_GetNamedTech_Alive_47 = true;

	private bool logic_uScript_GetNamedTech_Dead_47 = true;

	private bool logic_uScript_GetNamedTech_WaitingToSpawn_47 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_49 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_49;

	private bool logic_uScriptCon_CompareBool_True_49 = true;

	private bool logic_uScriptCon_CompareBool_False_49 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_51 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_51;

	private bool logic_uScriptAct_SetBool_Out_51 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_51 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_51 = true;

	private uScript_SetupTutorialEnemy logic_uScript_SetupTutorialEnemy_uScript_SetupTutorialEnemy_54 = new uScript_SetupTutorialEnemy();

	private Tank logic_uScript_SetupTutorialEnemy_tech_54;

	private float logic_uScript_SetupTutorialEnemy_throttleDriveModifier_54;

	private float logic_uScript_SetupTutorialEnemy_throttleTurnModifier_54;

	private bool logic_uScript_SetupTutorialEnemy_Out_54 = true;

	private SubGraph_SaveLoadInt logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_56 = new SubGraph_SaveLoadInt();

	private int logic_SubGraph_SaveLoadInt_restartValue_56 = 1;

	private int logic_SubGraph_SaveLoadInt_integer_56;

	private object logic_SubGraph_SaveLoadInt_intAsVariable_56 = "";

	private string logic_SubGraph_SaveLoadInt_uniqueID_56 = "Stage";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_60 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_60;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_60 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_60 = "Msg04FightEnemyShown";

	private uScript_SetPopulationActive logic_uScript_SetPopulationActive_uScript_SetPopulationActive_61 = new uScript_SetPopulationActive();

	private bool logic_uScript_SetPopulationActive_active_61 = true;

	private bool logic_uScript_SetPopulationActive_Out_61 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_65 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_65;

	private bool logic_uScriptCon_CompareBool_True_65 = true;

	private bool logic_uScriptCon_CompareBool_False_65 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_67 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_67;

	private bool logic_uScriptAct_SetBool_Out_67 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_67 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_67 = true;

	private uScript_HasPlayerTraveledDistance logic_uScript_HasPlayerTraveledDistance_uScript_HasPlayerTraveledDistance_69 = new uScript_HasPlayerTraveledDistance();

	private float logic_uScript_HasPlayerTraveledDistance_distance_69;

	private bool logic_uScript_HasPlayerTraveledDistance_True_69 = true;

	private bool logic_uScript_HasPlayerTraveledDistance_False_69 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_73 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_73;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_73 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_73 = "EnemySpawned";

	private uScript_FinishEncounter logic_uScript_FinishEncounter_uScript_FinishEncounter_74 = new uScript_FinishEncounter();

	private GameObject logic_uScript_FinishEncounter_owner_74;

	private bool logic_uScript_FinishEncounter_Out_74 = true;

	private uScriptAct_AddInt_v2 logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_75 = new uScriptAct_AddInt_v2();

	private int logic_uScriptAct_AddInt_v2_A_75;

	private int logic_uScriptAct_AddInt_v2_B_75 = 1;

	private int logic_uScriptAct_AddInt_v2_IntResult_75;

	private float logic_uScriptAct_AddInt_v2_FloatResult_75;

	private bool logic_uScriptAct_AddInt_v2_Out_75 = true;

	private uScriptAct_AddInt_v2 logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_78 = new uScriptAct_AddInt_v2();

	private int logic_uScriptAct_AddInt_v2_A_78;

	private int logic_uScriptAct_AddInt_v2_B_78 = 1;

	private int logic_uScriptAct_AddInt_v2_IntResult_78;

	private float logic_uScriptAct_AddInt_v2_FloatResult_78;

	private bool logic_uScriptAct_AddInt_v2_Out_78 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_81 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_81;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_81;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_81;

	private bool logic_uScript_AddMessage_Out_81 = true;

	private bool logic_uScript_AddMessage_Shown_81 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_83 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_83;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_83;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_83;

	private bool logic_uScript_AddMessage_Out_83 = true;

	private bool logic_uScript_AddMessage_Shown_83 = true;

	private uScriptAct_AddInt_v2 logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_87 = new uScriptAct_AddInt_v2();

	private int logic_uScriptAct_AddInt_v2_A_87;

	private int logic_uScriptAct_AddInt_v2_B_87 = 1;

	private int logic_uScriptAct_AddInt_v2_IntResult_87;

	private float logic_uScriptAct_AddInt_v2_FloatResult_87;

	private bool logic_uScriptAct_AddInt_v2_Out_87 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_89 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_89 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_92 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_92;

	private bool logic_uScriptCon_CompareBool_True_92 = true;

	private bool logic_uScriptCon_CompareBool_False_92 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_94 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_94;

	private bool logic_uScriptAct_SetBool_Out_94 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_94 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_94 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_97 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_97;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_97 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_97 = "BlockAttachedOrDrivenAway";

	private uScript_HideHintFloating logic_uScript_HideHintFloating_uScript_HideHintFloating_98 = new uScript_HideHintFloating();

	private bool logic_uScript_HideHintFloating_Out_98 = true;

	private uScript_HideHintFloating logic_uScript_HideHintFloating_uScript_HideHintFloating_99 = new uScript_HideHintFloating();

	private bool logic_uScript_HideHintFloating_Out_99 = true;

	private uScript_HideHintFloating logic_uScript_HideHintFloating_uScript_HideHintFloating_100 = new uScript_HideHintFloating();

	private bool logic_uScript_HideHintFloating_Out_100 = true;

	private uScript_HideHintFloating logic_uScript_HideHintFloating_uScript_HideHintFloating_101 = new uScript_HideHintFloating();

	private bool logic_uScript_HideHintFloating_Out_101 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_102 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_102 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_102 = true;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_102 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_105 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_105 = true;

	private uScript_IsPlayerHoldingAnyBlock logic_uScript_IsPlayerHoldingAnyBlock_uScript_IsPlayerHoldingAnyBlock_106 = new uScript_IsPlayerHoldingAnyBlock();

	private bool logic_uScript_IsPlayerHoldingAnyBlock_Holding_106 = true;

	private bool logic_uScript_IsPlayerHoldingAnyBlock_NotHolding_106 = true;

	private uScript_ShowHint logic_uScript_ShowHint_uScript_ShowHint_107 = new uScript_ShowHint();

	private GameHints.HintID logic_uScript_ShowHint_hintId_107;

	private bool logic_uScript_ShowHint_Out_107 = true;

	private uScript_HasHintBeenShownBefore logic_uScript_HasHintBeenShownBefore_uScript_HasHintBeenShownBefore_108 = new uScript_HasHintBeenShownBefore();

	private GameHints.HintID logic_uScript_HasHintBeenShownBefore_hintID_108;

	private bool logic_uScript_HasHintBeenShownBefore_Shown_108 = true;

	private bool logic_uScript_HasHintBeenShownBefore_NotShown_108 = true;

	private SubGraph_AddMessageWithPadSupport logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_112 = new SubGraph_AddMessageWithPadSupport();

	private uScript_AddMessage.MessageData logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboard_112;

	private uScript_AddMessage.MessageData logic_SubGraph_AddMessageWithPadSupport_messageControlPad_112;

	private uScript_AddMessage.MessageSpeaker logic_SubGraph_AddMessageWithPadSupport_speaker_112;

	private ManOnScreenMessages.OnScreenMessage logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboardReturn_112;

	private ManOnScreenMessages.OnScreenMessage logic_SubGraph_AddMessageWithPadSupport_messageControlPadReturn_112;

	private SubGraph_AddMessageWithPadSupport logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_116 = new SubGraph_AddMessageWithPadSupport();

	private uScript_AddMessage.MessageData logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboard_116;

	private uScript_AddMessage.MessageData logic_SubGraph_AddMessageWithPadSupport_messageControlPad_116;

	private uScript_AddMessage.MessageSpeaker logic_SubGraph_AddMessageWithPadSupport_speaker_116;

	private ManOnScreenMessages.OnScreenMessage logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboardReturn_116;

	private ManOnScreenMessages.OnScreenMessage logic_SubGraph_AddMessageWithPadSupport_messageControlPadReturn_116;

	private SubGraph_AddMessageWithPadSupport logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_122 = new SubGraph_AddMessageWithPadSupport();

	private uScript_AddMessage.MessageData logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboard_122;

	private uScript_AddMessage.MessageData logic_SubGraph_AddMessageWithPadSupport_messageControlPad_122;

	private uScript_AddMessage.MessageSpeaker logic_SubGraph_AddMessageWithPadSupport_speaker_122;

	private ManOnScreenMessages.OnScreenMessage logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboardReturn_122;

	private ManOnScreenMessages.OnScreenMessage logic_SubGraph_AddMessageWithPadSupport_messageControlPadReturn_122;

	private SubGraph_ShowHintWithPadSupport logic_SubGraph_ShowHintWithPadSupport_SubGraph_ShowHintWithPadSupport_124 = new SubGraph_ShowHintWithPadSupport();

	private UIHintFloating.HintFloatTypes logic_SubGraph_ShowHintWithPadSupport_hintControlPad_124 = UIHintFloating.HintFloatTypes.Movement_Pad;

	private UIHintFloating.HintFloatTypes logic_SubGraph_ShowHintWithPadSupport_hintMouseKeyboard_124 = UIHintFloating.HintFloatTypes.Movement_Keyboard;

	private SubGraph_ShowHintWithPadSupport logic_SubGraph_ShowHintWithPadSupport_SubGraph_ShowHintWithPadSupport_125 = new SubGraph_ShowHintWithPadSupport();

	private UIHintFloating.HintFloatTypes logic_SubGraph_ShowHintWithPadSupport_hintControlPad_125 = UIHintFloating.HintFloatTypes.Shoot_Pad;

	private UIHintFloating.HintFloatTypes logic_SubGraph_ShowHintWithPadSupport_hintMouseKeyboard_125 = UIHintFloating.HintFloatTypes.Shoot_Keyboard;

	private SubGraph_ShowHintWithPadSupport logic_SubGraph_ShowHintWithPadSupport_SubGraph_ShowHintWithPadSupport_126 = new SubGraph_ShowHintWithPadSupport();

	private UIHintFloating.HintFloatTypes logic_SubGraph_ShowHintWithPadSupport_hintControlPad_126 = UIHintFloating.HintFloatTypes.Shoot_Pad;

	private UIHintFloating.HintFloatTypes logic_SubGraph_ShowHintWithPadSupport_hintMouseKeyboard_126 = UIHintFloating.HintFloatTypes.Shoot_Keyboard;

	private SubGraph_AddMessageWithPadSupport logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_127 = new SubGraph_AddMessageWithPadSupport();

	private uScript_AddMessage.MessageData logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboard_127;

	private uScript_AddMessage.MessageData logic_SubGraph_AddMessageWithPadSupport_messageControlPad_127;

	private uScript_AddMessage.MessageSpeaker logic_SubGraph_AddMessageWithPadSupport_speaker_127;

	private ManOnScreenMessages.OnScreenMessage logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboardReturn_127;

	private ManOnScreenMessages.OnScreenMessage logic_SubGraph_AddMessageWithPadSupport_messageControlPadReturn_127;

	private uScript_CompleteAchievement logic_uScript_CompleteAchievement_uScript_CompleteAchievement_129 = new uScript_CompleteAchievement();

	private ManAchievements.AchievementTypes logic_uScript_CompleteAchievement_achievementID_129;

	private bool logic_uScript_CompleteAchievement_Out_129 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_130 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_130 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_130 = true;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_130 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_132 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_132 = true;

	private uScript_SendAnaliticsEvent logic_uScript_SendAnaliticsEvent_uScript_SendAnaliticsEvent_133 = new uScript_SendAnaliticsEvent();

	private string logic_uScript_SendAnaliticsEvent_analiticsEvent_133 = "tutorial_stage";

	private string logic_uScript_SendAnaliticsEvent_parameterName_133 = "stage_complete";

	private object logic_uScript_SendAnaliticsEvent_parameter_133 = "fight_first_enemy";

	private bool logic_uScript_SendAnaliticsEvent_Out_133 = true;

	private void SyncUnityHooks()
	{
		SyncEventListeners();
		if (null == owner_Connection_8 || !m_RegisteredForEvents)
		{
			owner_Connection_8 = parentGameObject;
		}
		if (null == owner_Connection_21 || !m_RegisteredForEvents)
		{
			owner_Connection_21 = parentGameObject;
			if (null != owner_Connection_21)
			{
				uScript_GameObject uScript_GameObject2 = owner_Connection_21.GetComponent<uScript_GameObject>();
				if (null == uScript_GameObject2)
				{
					uScript_GameObject2 = owner_Connection_21.AddComponent<uScript_GameObject>();
				}
				if (null != uScript_GameObject2)
				{
					uScript_GameObject2.EnableEvent += Instance_EnableEvent_22;
					uScript_GameObject2.DisableEvent += Instance_DisableEvent_22;
					uScript_GameObject2.DestroyEvent += Instance_DestroyEvent_22;
				}
			}
		}
		if (null == owner_Connection_25 || !m_RegisteredForEvents)
		{
			owner_Connection_25 = parentGameObject;
		}
		if (null == owner_Connection_43 || !m_RegisteredForEvents)
		{
			owner_Connection_43 = parentGameObject;
			if (null != owner_Connection_43)
			{
				uScript_EncounterUpdate uScript_EncounterUpdate2 = owner_Connection_43.GetComponent<uScript_EncounterUpdate>();
				if (null == uScript_EncounterUpdate2)
				{
					uScript_EncounterUpdate2 = owner_Connection_43.AddComponent<uScript_EncounterUpdate>();
				}
				if (null != uScript_EncounterUpdate2)
				{
					uScript_EncounterUpdate2.OnUpdate += Instance_OnUpdate_44;
					uScript_EncounterUpdate2.OnSuspend += Instance_OnSuspend_44;
					uScript_EncounterUpdate2.OnResume += Instance_OnResume_44;
				}
			}
		}
		if (null == owner_Connection_46 || !m_RegisteredForEvents)
		{
			owner_Connection_46 = parentGameObject;
		}
		if (!(null == owner_Connection_57) && m_RegisteredForEvents)
		{
			return;
		}
		owner_Connection_57 = parentGameObject;
		if (null != owner_Connection_57)
		{
			uScript_SaveLoad uScript_SaveLoad2 = owner_Connection_57.GetComponent<uScript_SaveLoad>();
			if (null == uScript_SaveLoad2)
			{
				uScript_SaveLoad2 = owner_Connection_57.AddComponent<uScript_SaveLoad>();
			}
			if (null != uScript_SaveLoad2)
			{
				uScript_SaveLoad2.SaveEvent += Instance_SaveEvent_58;
				uScript_SaveLoad2.LoadEvent += Instance_LoadEvent_58;
				uScript_SaveLoad2.RestartEvent += Instance_RestartEvent_58;
			}
		}
	}

	private void RegisterForUnityHooks()
	{
		SyncEventListeners();
		if (!m_RegisteredForEvents && null != owner_Connection_21)
		{
			uScript_GameObject uScript_GameObject2 = owner_Connection_21.GetComponent<uScript_GameObject>();
			if (null == uScript_GameObject2)
			{
				uScript_GameObject2 = owner_Connection_21.AddComponent<uScript_GameObject>();
			}
			if (null != uScript_GameObject2)
			{
				uScript_GameObject2.EnableEvent += Instance_EnableEvent_22;
				uScript_GameObject2.DisableEvent += Instance_DisableEvent_22;
				uScript_GameObject2.DestroyEvent += Instance_DestroyEvent_22;
			}
		}
		if (!m_RegisteredForEvents && null != owner_Connection_43)
		{
			uScript_EncounterUpdate uScript_EncounterUpdate2 = owner_Connection_43.GetComponent<uScript_EncounterUpdate>();
			if (null == uScript_EncounterUpdate2)
			{
				uScript_EncounterUpdate2 = owner_Connection_43.AddComponent<uScript_EncounterUpdate>();
			}
			if (null != uScript_EncounterUpdate2)
			{
				uScript_EncounterUpdate2.OnUpdate += Instance_OnUpdate_44;
				uScript_EncounterUpdate2.OnSuspend += Instance_OnSuspend_44;
				uScript_EncounterUpdate2.OnResume += Instance_OnResume_44;
			}
		}
		if (!m_RegisteredForEvents && null != owner_Connection_57)
		{
			uScript_SaveLoad uScript_SaveLoad2 = owner_Connection_57.GetComponent<uScript_SaveLoad>();
			if (null == uScript_SaveLoad2)
			{
				uScript_SaveLoad2 = owner_Connection_57.AddComponent<uScript_SaveLoad>();
			}
			if (null != uScript_SaveLoad2)
			{
				uScript_SaveLoad2.SaveEvent += Instance_SaveEvent_58;
				uScript_SaveLoad2.LoadEvent += Instance_LoadEvent_58;
				uScript_SaveLoad2.RestartEvent += Instance_RestartEvent_58;
			}
		}
	}

	private void SyncEventListeners()
	{
	}

	private void UnregisterEventListeners()
	{
		if (null != owner_Connection_21)
		{
			uScript_GameObject component = owner_Connection_21.GetComponent<uScript_GameObject>();
			if (null != component)
			{
				component.EnableEvent -= Instance_EnableEvent_22;
				component.DisableEvent -= Instance_DisableEvent_22;
				component.DestroyEvent -= Instance_DestroyEvent_22;
			}
		}
		if (null != owner_Connection_43)
		{
			uScript_EncounterUpdate component2 = owner_Connection_43.GetComponent<uScript_EncounterUpdate>();
			if (null != component2)
			{
				component2.OnUpdate -= Instance_OnUpdate_44;
				component2.OnSuspend -= Instance_OnSuspend_44;
				component2.OnResume -= Instance_OnResume_44;
			}
		}
		if (null != owner_Connection_57)
		{
			uScript_SaveLoad component3 = owner_Connection_57.GetComponent<uScript_SaveLoad>();
			if (null != component3)
			{
				component3.SaveEvent -= Instance_SaveEvent_58;
				component3.LoadEvent -= Instance_LoadEvent_58;
				component3.RestartEvent -= Instance_RestartEvent_58;
			}
		}
	}

	public override void SetParent(GameObject g)
	{
		parentGameObject = g;
		logic_uScript_BlockDecay_uScript_BlockDecay_0.SetParent(g);
		logic_uScript_SkipTutorial_uScript_SkipTutorial_1.SetParent(g);
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_2.SetParent(g);
		logic_uScript_HasPlayerTraveledDistance_uScript_HasPlayerTraveledDistance_4.SetParent(g);
		logic_uScript_HasPlayerTraveledDistance_uScript_HasPlayerTraveledDistance_5.SetParent(g);
		logic_uScript_HasPlayerFiredGun_uScript_HasPlayerFiredGun_6.SetParent(g);
		logic_uScript_HasAnyBlockBeenAttached_uScript_HasAnyBlockBeenAttached_10.SetParent(g);
		logic_uScript_HasPlayerTraveledDistance_uScript_HasPlayerTraveledDistance_12.SetParent(g);
		logic_uScript_GetTankBlocks_uScript_GetTankBlocks_14.SetParent(g);
		logic_uScript_HealDamageFromSource_uScript_HealDamageFromSource_15.SetParent(g);
		logic_uScriptCon_CompareFloat_uScriptCon_CompareFloat_18.SetParent(g);
		logic_uScriptAct_Stopwatch_uScriptAct_Stopwatch_19.SetParent(g);
		logic_uScript_SetPopulation_uScript_SetPopulation_23.SetParent(g);
		logic_uScript_Save_uScript_Save_24.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_27.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_28.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_29.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_31.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_34.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_36.SetParent(g);
		logic_uScript_Wait_uScript_Wait_37.SetParent(g);
		logic_uScript_SpawnEnemyInFrontOfPlayer_uScript_SpawnEnemyInFrontOfPlayer_45.SetParent(g);
		logic_uScript_GetNamedTech_uScript_GetNamedTech_47.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_49.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_51.SetParent(g);
		logic_uScript_SetupTutorialEnemy_uScript_SetupTutorialEnemy_54.SetParent(g);
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_56.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_60.SetParent(g);
		logic_uScript_SetPopulationActive_uScript_SetPopulationActive_61.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_65.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_67.SetParent(g);
		logic_uScript_HasPlayerTraveledDistance_uScript_HasPlayerTraveledDistance_69.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_73.SetParent(g);
		logic_uScript_FinishEncounter_uScript_FinishEncounter_74.SetParent(g);
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_75.SetParent(g);
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_78.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_81.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_83.SetParent(g);
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_87.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_89.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_92.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_94.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_97.SetParent(g);
		logic_uScript_HideHintFloating_uScript_HideHintFloating_98.SetParent(g);
		logic_uScript_HideHintFloating_uScript_HideHintFloating_99.SetParent(g);
		logic_uScript_HideHintFloating_uScript_HideHintFloating_100.SetParent(g);
		logic_uScript_HideHintFloating_uScript_HideHintFloating_101.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_102.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_105.SetParent(g);
		logic_uScript_IsPlayerHoldingAnyBlock_uScript_IsPlayerHoldingAnyBlock_106.SetParent(g);
		logic_uScript_ShowHint_uScript_ShowHint_107.SetParent(g);
		logic_uScript_HasHintBeenShownBefore_uScript_HasHintBeenShownBefore_108.SetParent(g);
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_112.SetParent(g);
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_116.SetParent(g);
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_122.SetParent(g);
		logic_SubGraph_ShowHintWithPadSupport_SubGraph_ShowHintWithPadSupport_124.SetParent(g);
		logic_SubGraph_ShowHintWithPadSupport_SubGraph_ShowHintWithPadSupport_125.SetParent(g);
		logic_SubGraph_ShowHintWithPadSupport_SubGraph_ShowHintWithPadSupport_126.SetParent(g);
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_127.SetParent(g);
		logic_uScript_CompleteAchievement_uScript_CompleteAchievement_129.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_130.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_132.SetParent(g);
		logic_uScript_SendAnaliticsEvent_uScript_SendAnaliticsEvent_133.SetParent(g);
		owner_Connection_8 = parentGameObject;
		owner_Connection_21 = parentGameObject;
		owner_Connection_25 = parentGameObject;
		owner_Connection_43 = parentGameObject;
		owner_Connection_46 = parentGameObject;
		owner_Connection_57 = parentGameObject;
	}

	public void Awake()
	{
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_56.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_60.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_73.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_97.Awake();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_112.Awake();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_116.Awake();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_122.Awake();
		logic_SubGraph_ShowHintWithPadSupport_SubGraph_ShowHintWithPadSupport_124.Awake();
		logic_SubGraph_ShowHintWithPadSupport_SubGraph_ShowHintWithPadSupport_125.Awake();
		logic_SubGraph_ShowHintWithPadSupport_SubGraph_ShowHintWithPadSupport_126.Awake();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_127.Awake();
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_2.Output1 += uScriptCon_ManualSwitch_Output1_2;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_2.Output2 += uScriptCon_ManualSwitch_Output2_2;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_2.Output3 += uScriptCon_ManualSwitch_Output3_2;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_2.Output4 += uScriptCon_ManualSwitch_Output4_2;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_2.Output5 += uScriptCon_ManualSwitch_Output5_2;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_2.Output6 += uScriptCon_ManualSwitch_Output6_2;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_2.Output7 += uScriptCon_ManualSwitch_Output7_2;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_2.Output8 += uScriptCon_ManualSwitch_Output8_2;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_56.Save_Out += SubGraph_SaveLoadInt_Save_Out_56;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_56.Load_Out += SubGraph_SaveLoadInt_Load_Out_56;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_56.Restart_Out += SubGraph_SaveLoadInt_Restart_Out_56;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_60.Save_Out += SubGraph_SaveLoadBool_Save_Out_60;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_60.Load_Out += SubGraph_SaveLoadBool_Load_Out_60;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_60.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_60;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_73.Save_Out += SubGraph_SaveLoadBool_Save_Out_73;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_73.Load_Out += SubGraph_SaveLoadBool_Load_Out_73;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_73.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_73;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_97.Save_Out += SubGraph_SaveLoadBool_Save_Out_97;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_97.Load_Out += SubGraph_SaveLoadBool_Load_Out_97;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_97.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_97;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_112.Out += SubGraph_AddMessageWithPadSupport_Out_112;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_112.Shown += SubGraph_AddMessageWithPadSupport_Shown_112;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_116.Out += SubGraph_AddMessageWithPadSupport_Out_116;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_116.Shown += SubGraph_AddMessageWithPadSupport_Shown_116;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_122.Out += SubGraph_AddMessageWithPadSupport_Out_122;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_122.Shown += SubGraph_AddMessageWithPadSupport_Shown_122;
		logic_SubGraph_ShowHintWithPadSupport_SubGraph_ShowHintWithPadSupport_124.Out += SubGraph_ShowHintWithPadSupport_Out_124;
		logic_SubGraph_ShowHintWithPadSupport_SubGraph_ShowHintWithPadSupport_125.Out += SubGraph_ShowHintWithPadSupport_Out_125;
		logic_SubGraph_ShowHintWithPadSupport_SubGraph_ShowHintWithPadSupport_126.Out += SubGraph_ShowHintWithPadSupport_Out_126;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_127.Out += SubGraph_AddMessageWithPadSupport_Out_127;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_127.Shown += SubGraph_AddMessageWithPadSupport_Shown_127;
	}

	public void Start()
	{
		SyncUnityHooks();
		m_RegisteredForEvents = true;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_56.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_60.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_73.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_97.Start();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_112.Start();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_116.Start();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_122.Start();
		logic_SubGraph_ShowHintWithPadSupport_SubGraph_ShowHintWithPadSupport_124.Start();
		logic_SubGraph_ShowHintWithPadSupport_SubGraph_ShowHintWithPadSupport_125.Start();
		logic_SubGraph_ShowHintWithPadSupport_SubGraph_ShowHintWithPadSupport_126.Start();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_127.Start();
	}

	public void OnEnable()
	{
		RegisterForUnityHooks();
		m_RegisteredForEvents = true;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_56.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_60.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_73.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_97.OnEnable();
		logic_uScript_IsPlayerHoldingAnyBlock_uScript_IsPlayerHoldingAnyBlock_106.OnEnable();
		logic_uScript_HasHintBeenShownBefore_uScript_HasHintBeenShownBefore_108.OnEnable();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_112.OnEnable();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_116.OnEnable();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_122.OnEnable();
		logic_SubGraph_ShowHintWithPadSupport_SubGraph_ShowHintWithPadSupport_124.OnEnable();
		logic_SubGraph_ShowHintWithPadSupport_SubGraph_ShowHintWithPadSupport_125.OnEnable();
		logic_SubGraph_ShowHintWithPadSupport_SubGraph_ShowHintWithPadSupport_126.OnEnable();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_127.OnEnable();
	}

	public void OnDisable()
	{
		logic_uScript_HasPlayerTraveledDistance_uScript_HasPlayerTraveledDistance_4.OnDisable();
		logic_uScript_HasPlayerTraveledDistance_uScript_HasPlayerTraveledDistance_5.OnDisable();
		logic_uScript_HasPlayerFiredGun_uScript_HasPlayerFiredGun_6.OnDisable();
		logic_uScript_HasAnyBlockBeenAttached_uScript_HasAnyBlockBeenAttached_10.OnDisable();
		logic_uScript_HasPlayerTraveledDistance_uScript_HasPlayerTraveledDistance_12.OnDisable();
		logic_uScript_GetTankBlocks_uScript_GetTankBlocks_14.OnDisable();
		logic_uScript_HealDamageFromSource_uScript_HealDamageFromSource_15.OnDisable();
		logic_uScript_Save_uScript_Save_24.OnDisable();
		logic_uScript_Wait_uScript_Wait_37.OnDisable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_56.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_60.OnDisable();
		logic_uScript_HasPlayerTraveledDistance_uScript_HasPlayerTraveledDistance_69.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_73.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_81.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_83.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_97.OnDisable();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_112.OnDisable();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_116.OnDisable();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_122.OnDisable();
		logic_SubGraph_ShowHintWithPadSupport_SubGraph_ShowHintWithPadSupport_124.OnDisable();
		logic_SubGraph_ShowHintWithPadSupport_SubGraph_ShowHintWithPadSupport_125.OnDisable();
		logic_SubGraph_ShowHintWithPadSupport_SubGraph_ShowHintWithPadSupport_126.OnDisable();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_127.OnDisable();
		UnregisterEventListeners();
		m_RegisteredForEvents = false;
	}

	public void Update()
	{
		SyncEventListeners();
		logic_uScriptAct_Stopwatch_uScriptAct_Stopwatch_19.Update();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_56.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_60.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_73.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_97.Update();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_112.Update();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_116.Update();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_122.Update();
		logic_SubGraph_ShowHintWithPadSupport_SubGraph_ShowHintWithPadSupport_124.Update();
		logic_SubGraph_ShowHintWithPadSupport_SubGraph_ShowHintWithPadSupport_125.Update();
		logic_SubGraph_ShowHintWithPadSupport_SubGraph_ShowHintWithPadSupport_126.Update();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_127.Update();
	}

	public void OnDestroy()
	{
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_56.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_60.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_73.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_97.OnDestroy();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_112.OnDestroy();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_116.OnDestroy();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_122.OnDestroy();
		logic_SubGraph_ShowHintWithPadSupport_SubGraph_ShowHintWithPadSupport_124.OnDestroy();
		logic_SubGraph_ShowHintWithPadSupport_SubGraph_ShowHintWithPadSupport_125.OnDestroy();
		logic_SubGraph_ShowHintWithPadSupport_SubGraph_ShowHintWithPadSupport_126.OnDestroy();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_127.OnDestroy();
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_2.Output1 -= uScriptCon_ManualSwitch_Output1_2;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_2.Output2 -= uScriptCon_ManualSwitch_Output2_2;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_2.Output3 -= uScriptCon_ManualSwitch_Output3_2;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_2.Output4 -= uScriptCon_ManualSwitch_Output4_2;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_2.Output5 -= uScriptCon_ManualSwitch_Output5_2;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_2.Output6 -= uScriptCon_ManualSwitch_Output6_2;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_2.Output7 -= uScriptCon_ManualSwitch_Output7_2;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_2.Output8 -= uScriptCon_ManualSwitch_Output8_2;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_56.Save_Out -= SubGraph_SaveLoadInt_Save_Out_56;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_56.Load_Out -= SubGraph_SaveLoadInt_Load_Out_56;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_56.Restart_Out -= SubGraph_SaveLoadInt_Restart_Out_56;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_60.Save_Out -= SubGraph_SaveLoadBool_Save_Out_60;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_60.Load_Out -= SubGraph_SaveLoadBool_Load_Out_60;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_60.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_60;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_73.Save_Out -= SubGraph_SaveLoadBool_Save_Out_73;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_73.Load_Out -= SubGraph_SaveLoadBool_Load_Out_73;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_73.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_73;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_97.Save_Out -= SubGraph_SaveLoadBool_Save_Out_97;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_97.Load_Out -= SubGraph_SaveLoadBool_Load_Out_97;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_97.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_97;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_112.Out -= SubGraph_AddMessageWithPadSupport_Out_112;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_112.Shown -= SubGraph_AddMessageWithPadSupport_Shown_112;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_116.Out -= SubGraph_AddMessageWithPadSupport_Out_116;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_116.Shown -= SubGraph_AddMessageWithPadSupport_Shown_116;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_122.Out -= SubGraph_AddMessageWithPadSupport_Out_122;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_122.Shown -= SubGraph_AddMessageWithPadSupport_Shown_122;
		logic_SubGraph_ShowHintWithPadSupport_SubGraph_ShowHintWithPadSupport_124.Out -= SubGraph_ShowHintWithPadSupport_Out_124;
		logic_SubGraph_ShowHintWithPadSupport_SubGraph_ShowHintWithPadSupport_125.Out -= SubGraph_ShowHintWithPadSupport_Out_125;
		logic_SubGraph_ShowHintWithPadSupport_SubGraph_ShowHintWithPadSupport_126.Out -= SubGraph_ShowHintWithPadSupport_Out_126;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_127.Out -= SubGraph_AddMessageWithPadSupport_Out_127;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_127.Shown -= SubGraph_AddMessageWithPadSupport_Shown_127;
	}

	private void Instance_EnableEvent_22(object o, EventArgs e)
	{
		Relay_EnableEvent_22();
	}

	private void Instance_DisableEvent_22(object o, EventArgs e)
	{
		Relay_DisableEvent_22();
	}

	private void Instance_DestroyEvent_22(object o, EventArgs e)
	{
		Relay_DestroyEvent_22();
	}

	private void Instance_OnUpdate_44(object o, EventArgs e)
	{
		Relay_OnUpdate_44();
	}

	private void Instance_OnSuspend_44(object o, EventArgs e)
	{
		Relay_OnSuspend_44();
	}

	private void Instance_OnResume_44(object o, EventArgs e)
	{
		Relay_OnResume_44();
	}

	private void Instance_SaveEvent_58(object o, EventArgs e)
	{
		Relay_SaveEvent_58();
	}

	private void Instance_LoadEvent_58(object o, EventArgs e)
	{
		Relay_LoadEvent_58();
	}

	private void Instance_RestartEvent_58(object o, EventArgs e)
	{
		Relay_RestartEvent_58();
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

	private void SubGraph_SaveLoadInt_Save_Out_56(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_56 = e.integer;
		local_Stage_System_Int32 = logic_SubGraph_SaveLoadInt_integer_56;
		Relay_Save_Out_56();
	}

	private void SubGraph_SaveLoadInt_Load_Out_56(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_56 = e.integer;
		local_Stage_System_Int32 = logic_SubGraph_SaveLoadInt_integer_56;
		Relay_Load_Out_56();
	}

	private void SubGraph_SaveLoadInt_Restart_Out_56(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_56 = e.integer;
		local_Stage_System_Int32 = logic_SubGraph_SaveLoadInt_integer_56;
		Relay_Restart_Out_56();
	}

	private void SubGraph_SaveLoadBool_Save_Out_60(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_60 = e.boolean;
		local_Msg04FightEnemyShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_60;
		Relay_Save_Out_60();
	}

	private void SubGraph_SaveLoadBool_Load_Out_60(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_60 = e.boolean;
		local_Msg04FightEnemyShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_60;
		Relay_Load_Out_60();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_60(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_60 = e.boolean;
		local_Msg04FightEnemyShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_60;
		Relay_Restart_Out_60();
	}

	private void SubGraph_SaveLoadBool_Save_Out_73(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_73 = e.boolean;
		local_EnemySpawned_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_73;
		Relay_Save_Out_73();
	}

	private void SubGraph_SaveLoadBool_Load_Out_73(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_73 = e.boolean;
		local_EnemySpawned_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_73;
		Relay_Load_Out_73();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_73(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_73 = e.boolean;
		local_EnemySpawned_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_73;
		Relay_Restart_Out_73();
	}

	private void SubGraph_SaveLoadBool_Save_Out_97(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_97 = e.boolean;
		local_BlockAttachedOrDrivenAway_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_97;
		Relay_Save_Out_97();
	}

	private void SubGraph_SaveLoadBool_Load_Out_97(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_97 = e.boolean;
		local_BlockAttachedOrDrivenAway_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_97;
		Relay_Load_Out_97();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_97(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_97 = e.boolean;
		local_BlockAttachedOrDrivenAway_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_97;
		Relay_Restart_Out_97();
	}

	private void SubGraph_AddMessageWithPadSupport_Out_112(object o, SubGraph_AddMessageWithPadSupport.LogicEventArgs e)
	{
		logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboardReturn_112 = e.messageMouseKeyboardReturn;
		logic_SubGraph_AddMessageWithPadSupport_messageControlPadReturn_112 = e.messageControlPadReturn;
		Relay_Out_112();
	}

	private void SubGraph_AddMessageWithPadSupport_Shown_112(object o, SubGraph_AddMessageWithPadSupport.LogicEventArgs e)
	{
		logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboardReturn_112 = e.messageMouseKeyboardReturn;
		logic_SubGraph_AddMessageWithPadSupport_messageControlPadReturn_112 = e.messageControlPadReturn;
		Relay_Shown_112();
	}

	private void SubGraph_AddMessageWithPadSupport_Out_116(object o, SubGraph_AddMessageWithPadSupport.LogicEventArgs e)
	{
		logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboardReturn_116 = e.messageMouseKeyboardReturn;
		logic_SubGraph_AddMessageWithPadSupport_messageControlPadReturn_116 = e.messageControlPadReturn;
		Relay_Out_116();
	}

	private void SubGraph_AddMessageWithPadSupport_Shown_116(object o, SubGraph_AddMessageWithPadSupport.LogicEventArgs e)
	{
		logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboardReturn_116 = e.messageMouseKeyboardReturn;
		logic_SubGraph_AddMessageWithPadSupport_messageControlPadReturn_116 = e.messageControlPadReturn;
		Relay_Shown_116();
	}

	private void SubGraph_AddMessageWithPadSupport_Out_122(object o, SubGraph_AddMessageWithPadSupport.LogicEventArgs e)
	{
		logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboardReturn_122 = e.messageMouseKeyboardReturn;
		logic_SubGraph_AddMessageWithPadSupport_messageControlPadReturn_122 = e.messageControlPadReturn;
		Relay_Out_122();
	}

	private void SubGraph_AddMessageWithPadSupport_Shown_122(object o, SubGraph_AddMessageWithPadSupport.LogicEventArgs e)
	{
		logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboardReturn_122 = e.messageMouseKeyboardReturn;
		logic_SubGraph_AddMessageWithPadSupport_messageControlPadReturn_122 = e.messageControlPadReturn;
		Relay_Shown_122();
	}

	private void SubGraph_ShowHintWithPadSupport_Out_124(object o, SubGraph_ShowHintWithPadSupport.LogicEventArgs e)
	{
		Relay_Out_124();
	}

	private void SubGraph_ShowHintWithPadSupport_Out_125(object o, SubGraph_ShowHintWithPadSupport.LogicEventArgs e)
	{
		Relay_Out_125();
	}

	private void SubGraph_ShowHintWithPadSupport_Out_126(object o, SubGraph_ShowHintWithPadSupport.LogicEventArgs e)
	{
		Relay_Out_126();
	}

	private void SubGraph_AddMessageWithPadSupport_Out_127(object o, SubGraph_AddMessageWithPadSupport.LogicEventArgs e)
	{
		logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboardReturn_127 = e.messageMouseKeyboardReturn;
		logic_SubGraph_AddMessageWithPadSupport_messageControlPadReturn_127 = e.messageControlPadReturn;
		Relay_Out_127();
	}

	private void SubGraph_AddMessageWithPadSupport_Shown_127(object o, SubGraph_AddMessageWithPadSupport.LogicEventArgs e)
	{
		logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboardReturn_127 = e.messageMouseKeyboardReturn;
		logic_SubGraph_AddMessageWithPadSupport_messageControlPadReturn_127 = e.messageControlPadReturn;
		Relay_Shown_127();
	}

	private void Relay_In_0()
	{
		logic_uScript_BlockDecay_uScript_BlockDecay_0.In(logic_uScript_BlockDecay_allow_0);
		if (logic_uScript_BlockDecay_uScript_BlockDecay_0.Out)
		{
			Relay_In_1();
		}
	}

	private void Relay_In_1()
	{
		logic_uScript_SkipTutorial_uScript_SkipTutorial_1.In();
		bool yes = logic_uScript_SkipTutorial_uScript_SkipTutorial_1.Yes;
		bool no = logic_uScript_SkipTutorial_uScript_SkipTutorial_1.No;
		if (yes)
		{
			Relay_In_27();
		}
		if (no)
		{
			Relay_In_75();
		}
	}

	private void Relay_Output1_2()
	{
		Relay_In_0();
	}

	private void Relay_Output2_2()
	{
		Relay_In_112();
	}

	private void Relay_Output3_2()
	{
		Relay_In_65();
	}

	private void Relay_Output4_2()
	{
		Relay_In_36();
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
		logic_uScript_HasPlayerTraveledDistance_distance_4 = distDriveTutorialCheck;
		logic_uScript_HasPlayerTraveledDistance_uScript_HasPlayerTraveledDistance_4.In(logic_uScript_HasPlayerTraveledDistance_distance_4);
		if (logic_uScript_HasPlayerTraveledDistance_uScript_HasPlayerTraveledDistance_4.True)
		{
			Relay_In_31();
		}
	}

	private void Relay_In_5()
	{
		logic_uScript_HasPlayerTraveledDistance_distance_5 = distDriveWithoutFiringCheck;
		logic_uScript_HasPlayerTraveledDistance_uScript_HasPlayerTraveledDistance_5.In(logic_uScript_HasPlayerTraveledDistance_distance_5);
		if (logic_uScript_HasPlayerTraveledDistance_uScript_HasPlayerTraveledDistance_5.True)
		{
			Relay_In_99();
		}
	}

	private void Relay_In_6()
	{
		logic_uScript_HasPlayerFiredGun_uScript_HasPlayerFiredGun_6.In();
		bool num = logic_uScript_HasPlayerFiredGun_uScript_HasPlayerFiredGun_6.True;
		bool flag = logic_uScript_HasPlayerFiredGun_uScript_HasPlayerFiredGun_6.False;
		if (num)
		{
			Relay_In_37();
		}
		if (flag)
		{
			Relay_In_5();
		}
	}

	private void Relay_In_10()
	{
		logic_uScript_HasAnyBlockBeenAttached_blocks_10 = local_TonysBlocks_System_Collections_Generic_List_1_TankBlock_;
		logic_uScript_HasAnyBlockBeenAttached_uScript_HasAnyBlockBeenAttached_10.In(logic_uScript_HasAnyBlockBeenAttached_blocks_10);
		bool num = logic_uScript_HasAnyBlockBeenAttached_uScript_HasAnyBlockBeenAttached_10.True;
		bool flag = logic_uScript_HasAnyBlockBeenAttached_uScript_HasAnyBlockBeenAttached_10.False;
		if (num)
		{
			Relay_In_130();
		}
		if (flag)
		{
			Relay_In_132();
		}
	}

	private void Relay_In_12()
	{
		logic_uScript_HasPlayerTraveledDistance_distance_12 = distDriveWithoutScavengingCheck;
		logic_uScript_HasPlayerTraveledDistance_uScript_HasPlayerTraveledDistance_12.In(logic_uScript_HasPlayerTraveledDistance_distance_12);
		bool num = logic_uScript_HasPlayerTraveledDistance_uScript_HasPlayerTraveledDistance_12.True;
		bool flag = logic_uScript_HasPlayerTraveledDistance_uScript_HasPlayerTraveledDistance_12.False;
		if (num)
		{
			Relay_True_94();
		}
		if (flag)
		{
			Relay_In_106();
		}
	}

	private void Relay_In_14()
	{
		logic_uScript_GetTankBlocks_tank_14 = local_TonyTank_Tank;
		logic_uScript_GetTankBlocks_Return_14 = logic_uScript_GetTankBlocks_uScript_GetTankBlocks_14.In(logic_uScript_GetTankBlocks_tank_14);
		local_TonysBlocks_System_Collections_Generic_List_1_TankBlock_ = logic_uScript_GetTankBlocks_Return_14;
		if (logic_uScript_GetTankBlocks_uScript_GetTankBlocks_14.Out)
		{
			Relay_In_15();
		}
	}

	private void Relay_In_15()
	{
		logic_uScript_HealDamageFromSource_source_15 = local_TonyTank_Tank;
		logic_uScript_HealDamageFromSource_uScript_HealDamageFromSource_15.In(logic_uScript_HealDamageFromSource_source_15);
		if (logic_uScript_HealDamageFromSource_uScript_HealDamageFromSource_15.Out)
		{
			Relay_In_28();
		}
	}

	private void Relay_In_18()
	{
		logic_uScriptCon_CompareFloat_A_18 = local_20_System_Single;
		logic_uScriptCon_CompareFloat_B_18 = enemyFightTimeoutSecs;
		logic_uScriptCon_CompareFloat_uScriptCon_CompareFloat_18.In(logic_uScriptCon_CompareFloat_A_18, logic_uScriptCon_CompareFloat_B_18);
		if (logic_uScriptCon_CompareFloat_uScriptCon_CompareFloat_18.GreaterThanOrEqualTo)
		{
			Relay_In_87();
		}
	}

	private void Relay_StartTimer_19()
	{
		logic_uScriptAct_Stopwatch_uScriptAct_Stopwatch_19.StartTimer(out logic_uScriptAct_Stopwatch_Seconds_19);
		local_20_System_Single = logic_uScriptAct_Stopwatch_Seconds_19;
		if (logic_uScriptAct_Stopwatch_uScriptAct_Stopwatch_19.Started)
		{
			Relay_In_18();
		}
	}

	private void Relay_Stop_19()
	{
		logic_uScriptAct_Stopwatch_uScriptAct_Stopwatch_19.Stop(out logic_uScriptAct_Stopwatch_Seconds_19);
		local_20_System_Single = logic_uScriptAct_Stopwatch_Seconds_19;
		if (logic_uScriptAct_Stopwatch_uScriptAct_Stopwatch_19.Started)
		{
			Relay_In_18();
		}
	}

	private void Relay_ResetTimer_19()
	{
		logic_uScriptAct_Stopwatch_uScriptAct_Stopwatch_19.ResetTimer(out logic_uScriptAct_Stopwatch_Seconds_19);
		local_20_System_Single = logic_uScriptAct_Stopwatch_Seconds_19;
		if (logic_uScriptAct_Stopwatch_uScriptAct_Stopwatch_19.Started)
		{
			Relay_In_18();
		}
	}

	private void Relay_CheckTime_19()
	{
		logic_uScriptAct_Stopwatch_uScriptAct_Stopwatch_19.CheckTime(out logic_uScriptAct_Stopwatch_Seconds_19);
		local_20_System_Single = logic_uScriptAct_Stopwatch_Seconds_19;
		if (logic_uScriptAct_Stopwatch_uScriptAct_Stopwatch_19.Started)
		{
			Relay_In_18();
		}
	}

	private void Relay_EnableEvent_22()
	{
		Relay_ResetTimer_19();
	}

	private void Relay_DisableEvent_22()
	{
		Relay_ResetTimer_19();
	}

	private void Relay_DestroyEvent_22()
	{
	}

	private void Relay_In_23()
	{
		logic_uScript_SetPopulation_uScript_SetPopulation_23.In(logic_uScript_SetPopulation_automatic_23);
		if (logic_uScript_SetPopulation_uScript_SetPopulation_23.Out)
		{
			Relay_SetActive_61();
		}
	}

	private void Relay_In_24()
	{
		logic_uScript_Save_uScript_Save_24.In(logic_uScript_Save_state_24);
		if (logic_uScript_Save_uScript_Save_24.Out)
		{
			Relay_In_133();
		}
	}

	private void Relay_In_27()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_27.In(logic_uScriptAct_SetInt_Value_27, out logic_uScriptAct_SetInt_Target_27);
		local_Stage_System_Int32 = logic_uScriptAct_SetInt_Target_27;
	}

	private void Relay_In_28()
	{
		logic_uScriptCon_CompareBool_Bool_28 = local_Msg04FightEnemyShown_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_28.In(logic_uScriptCon_CompareBool_Bool_28);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_28.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_28.False;
		if (num)
		{
			Relay_In_127();
		}
		if (flag)
		{
			Relay_In_83();
		}
	}

	private void Relay_True_29()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_29.True(out logic_uScriptAct_SetBool_Target_29);
		local_Msg04FightEnemyShown_System_Boolean = logic_uScriptAct_SetBool_Target_29;
	}

	private void Relay_False_29()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_29.False(out logic_uScriptAct_SetBool_Target_29);
		local_Msg04FightEnemyShown_System_Boolean = logic_uScriptAct_SetBool_Target_29;
	}

	private void Relay_In_31()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_31 = messageTag;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_31.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_31, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_31);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_31.Out)
		{
			Relay_In_98();
		}
	}

	private void Relay_In_34()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_34 = messageTag;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_34.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_34, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_34);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_34.Out)
		{
			Relay_True_67();
		}
	}

	private void Relay_In_36()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_36 = messageTag;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_36.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_36, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_36);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_36.Out)
		{
			Relay_In_101();
		}
	}

	private void Relay_In_37()
	{
		logic_uScript_Wait_uScript_Wait_37.In(logic_uScript_Wait_seconds_37, logic_uScript_Wait_repeat_37);
		if (logic_uScript_Wait_uScript_Wait_37.Waited)
		{
			Relay_In_99();
		}
	}

	private void Relay_OnUpdate_44()
	{
		Relay_In_2();
	}

	private void Relay_OnSuspend_44()
	{
	}

	private void Relay_OnResume_44()
	{
	}

	private void Relay_In_45()
	{
		logic_uScript_SpawnEnemyInFrontOfPlayer_preset_45 = enemyPreset;
		logic_uScript_SpawnEnemyInFrontOfPlayer_distFromPlayer_45 = enemyForwardSpawnDist;
		logic_uScript_SpawnEnemyInFrontOfPlayer_ownerNode_45 = owner_Connection_46;
		logic_uScript_SpawnEnemyInFrontOfPlayer_uScript_SpawnEnemyInFrontOfPlayer_45.In(logic_uScript_SpawnEnemyInFrontOfPlayer_preset_45, logic_uScript_SpawnEnemyInFrontOfPlayer_uniqueName_45, logic_uScript_SpawnEnemyInFrontOfPlayer_distFromPlayer_45, logic_uScript_SpawnEnemyInFrontOfPlayer_ownerNode_45);
		if (logic_uScript_SpawnEnemyInFrontOfPlayer_uScript_SpawnEnemyInFrontOfPlayer_45.Spawned)
		{
			Relay_In_34();
		}
	}

	private void Relay_In_47()
	{
		logic_uScript_GetNamedTech_ownerNode_47 = owner_Connection_8;
		logic_uScript_GetNamedTech_tech_47 = local_TonyTank_Tank;
		logic_uScript_GetNamedTech_uScript_GetNamedTech_47.In(logic_uScript_GetNamedTech_name_47, logic_uScript_GetNamedTech_ownerNode_47, ref logic_uScript_GetNamedTech_tech_47);
		local_TonyTank_Tank = logic_uScript_GetNamedTech_tech_47;
		bool alive = logic_uScript_GetNamedTech_uScript_GetNamedTech_47.Alive;
		bool dead = logic_uScript_GetNamedTech_uScript_GetNamedTech_47.Dead;
		bool waitingToSpawn = logic_uScript_GetNamedTech_uScript_GetNamedTech_47.WaitingToSpawn;
		if (alive)
		{
			Relay_In_49();
		}
		if (dead)
		{
			Relay_In_100();
		}
		if (waitingToSpawn)
		{
			Relay_In_81();
		}
	}

	private void Relay_In_49()
	{
		logic_uScriptCon_CompareBool_Bool_49 = local_SetEnemyBlocks_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_49.In(logic_uScriptCon_CompareBool_Bool_49);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_49.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_49.False;
		if (num)
		{
			Relay_In_14();
		}
		if (flag)
		{
			Relay_In_54();
		}
	}

	private void Relay_True_51()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_51.True(out logic_uScriptAct_SetBool_Target_51);
		local_SetEnemyBlocks_System_Boolean = logic_uScriptAct_SetBool_Target_51;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_51.Out)
		{
			Relay_In_14();
		}
	}

	private void Relay_False_51()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_51.False(out logic_uScriptAct_SetBool_Target_51);
		local_SetEnemyBlocks_System_Boolean = logic_uScriptAct_SetBool_Target_51;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_51.Out)
		{
			Relay_In_14();
		}
	}

	private void Relay_In_54()
	{
		logic_uScript_SetupTutorialEnemy_tech_54 = local_TonyTank_Tank;
		logic_uScript_SetupTutorialEnemy_throttleDriveModifier_54 = enemyThrottleDriveModifer;
		logic_uScript_SetupTutorialEnemy_throttleTurnModifier_54 = enemyThrottleTurnModifer;
		logic_uScript_SetupTutorialEnemy_uScript_SetupTutorialEnemy_54.In(logic_uScript_SetupTutorialEnemy_tech_54, logic_uScript_SetupTutorialEnemy_throttleDriveModifier_54, logic_uScript_SetupTutorialEnemy_throttleTurnModifier_54);
		if (logic_uScript_SetupTutorialEnemy_uScript_SetupTutorialEnemy_54.Out)
		{
			Relay_True_51();
		}
	}

	private void Relay_Save_Out_56()
	{
		Relay_Save_60();
	}

	private void Relay_Load_Out_56()
	{
		Relay_Load_60();
	}

	private void Relay_Restart_Out_56()
	{
		Relay_Set_False_60();
	}

	private void Relay_Save_56()
	{
		logic_SubGraph_SaveLoadInt_integer_56 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_56 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_56.Save(logic_SubGraph_SaveLoadInt_restartValue_56, ref logic_SubGraph_SaveLoadInt_integer_56, logic_SubGraph_SaveLoadInt_intAsVariable_56, logic_SubGraph_SaveLoadInt_uniqueID_56);
	}

	private void Relay_Load_56()
	{
		logic_SubGraph_SaveLoadInt_integer_56 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_56 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_56.Load(logic_SubGraph_SaveLoadInt_restartValue_56, ref logic_SubGraph_SaveLoadInt_integer_56, logic_SubGraph_SaveLoadInt_intAsVariable_56, logic_SubGraph_SaveLoadInt_uniqueID_56);
	}

	private void Relay_Restart_56()
	{
		logic_SubGraph_SaveLoadInt_integer_56 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_56 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_56.Restart(logic_SubGraph_SaveLoadInt_restartValue_56, ref logic_SubGraph_SaveLoadInt_integer_56, logic_SubGraph_SaveLoadInt_intAsVariable_56, logic_SubGraph_SaveLoadInt_uniqueID_56);
	}

	private void Relay_SaveEvent_58()
	{
		Relay_Save_56();
	}

	private void Relay_LoadEvent_58()
	{
		Relay_Load_56();
	}

	private void Relay_RestartEvent_58()
	{
		Relay_Restart_56();
	}

	private void Relay_Save_Out_60()
	{
		Relay_Save_73();
	}

	private void Relay_Load_Out_60()
	{
		Relay_Load_73();
	}

	private void Relay_Restart_Out_60()
	{
		Relay_Set_False_73();
	}

	private void Relay_Save_60()
	{
		logic_SubGraph_SaveLoadBool_boolean_60 = local_Msg04FightEnemyShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_60 = local_Msg04FightEnemyShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_60.Save(ref logic_SubGraph_SaveLoadBool_boolean_60, logic_SubGraph_SaveLoadBool_boolAsVariable_60, logic_SubGraph_SaveLoadBool_uniqueID_60);
	}

	private void Relay_Load_60()
	{
		logic_SubGraph_SaveLoadBool_boolean_60 = local_Msg04FightEnemyShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_60 = local_Msg04FightEnemyShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_60.Load(ref logic_SubGraph_SaveLoadBool_boolean_60, logic_SubGraph_SaveLoadBool_boolAsVariable_60, logic_SubGraph_SaveLoadBool_uniqueID_60);
	}

	private void Relay_Set_True_60()
	{
		logic_SubGraph_SaveLoadBool_boolean_60 = local_Msg04FightEnemyShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_60 = local_Msg04FightEnemyShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_60.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_60, logic_SubGraph_SaveLoadBool_boolAsVariable_60, logic_SubGraph_SaveLoadBool_uniqueID_60);
	}

	private void Relay_Set_False_60()
	{
		logic_SubGraph_SaveLoadBool_boolean_60 = local_Msg04FightEnemyShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_60 = local_Msg04FightEnemyShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_60.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_60, logic_SubGraph_SaveLoadBool_boolAsVariable_60, logic_SubGraph_SaveLoadBool_uniqueID_60);
	}

	private void Relay_SetActive_61()
	{
		logic_uScript_SetPopulationActive_uScript_SetPopulationActive_61.SetActive(logic_uScript_SetPopulationActive_active_61);
		if (logic_uScript_SetPopulationActive_uScript_SetPopulationActive_61.Out)
		{
			Relay_In_24();
		}
	}

	private void Relay_In_65()
	{
		logic_uScriptCon_CompareBool_Bool_65 = local_EnemySpawned_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_65.In(logic_uScriptCon_CompareBool_Bool_65);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_65.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_65.False;
		if (num)
		{
			Relay_In_69();
		}
		if (flag)
		{
			Relay_In_116();
		}
	}

	private void Relay_True_67()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_67.True(out logic_uScriptAct_SetBool_Target_67);
		local_EnemySpawned_System_Boolean = logic_uScriptAct_SetBool_Target_67;
	}

	private void Relay_False_67()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_67.False(out logic_uScriptAct_SetBool_Target_67);
		local_EnemySpawned_System_Boolean = logic_uScriptAct_SetBool_Target_67;
	}

	private void Relay_In_69()
	{
		logic_uScript_HasPlayerTraveledDistance_distance_69 = distRunFromEnemyCheck;
		logic_uScript_HasPlayerTraveledDistance_uScript_HasPlayerTraveledDistance_69.In(logic_uScript_HasPlayerTraveledDistance_distance_69);
		bool num = logic_uScript_HasPlayerTraveledDistance_uScript_HasPlayerTraveledDistance_69.True;
		bool flag = logic_uScript_HasPlayerTraveledDistance_uScript_HasPlayerTraveledDistance_69.False;
		if (num)
		{
			Relay_In_87();
		}
		if (flag)
		{
			Relay_In_47();
		}
	}

	private void Relay_Save_Out_73()
	{
		Relay_Save_97();
	}

	private void Relay_Load_Out_73()
	{
		Relay_Load_97();
	}

	private void Relay_Restart_Out_73()
	{
		Relay_Set_False_97();
	}

	private void Relay_Save_73()
	{
		logic_SubGraph_SaveLoadBool_boolean_73 = local_EnemySpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_73 = local_EnemySpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_73.Save(ref logic_SubGraph_SaveLoadBool_boolean_73, logic_SubGraph_SaveLoadBool_boolAsVariable_73, logic_SubGraph_SaveLoadBool_uniqueID_73);
	}

	private void Relay_Load_73()
	{
		logic_SubGraph_SaveLoadBool_boolean_73 = local_EnemySpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_73 = local_EnemySpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_73.Load(ref logic_SubGraph_SaveLoadBool_boolean_73, logic_SubGraph_SaveLoadBool_boolAsVariable_73, logic_SubGraph_SaveLoadBool_uniqueID_73);
	}

	private void Relay_Set_True_73()
	{
		logic_SubGraph_SaveLoadBool_boolean_73 = local_EnemySpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_73 = local_EnemySpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_73.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_73, logic_SubGraph_SaveLoadBool_boolAsVariable_73, logic_SubGraph_SaveLoadBool_uniqueID_73);
	}

	private void Relay_Set_False_73()
	{
		logic_SubGraph_SaveLoadBool_boolean_73 = local_EnemySpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_73 = local_EnemySpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_73.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_73, logic_SubGraph_SaveLoadBool_boolAsVariable_73, logic_SubGraph_SaveLoadBool_uniqueID_73);
	}

	private void Relay_Succeed_74()
	{
		logic_uScript_FinishEncounter_owner_74 = owner_Connection_25;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_74.Succeed(logic_uScript_FinishEncounter_owner_74);
	}

	private void Relay_Fail_74()
	{
		logic_uScript_FinishEncounter_owner_74 = owner_Connection_25;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_74.Fail(logic_uScript_FinishEncounter_owner_74);
	}

	private void Relay_In_75()
	{
		logic_uScriptAct_AddInt_v2_A_75 = local_Stage_System_Int32;
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_75.In(logic_uScriptAct_AddInt_v2_A_75, logic_uScriptAct_AddInt_v2_B_75, out logic_uScriptAct_AddInt_v2_IntResult_75, out logic_uScriptAct_AddInt_v2_FloatResult_75);
		local_Stage_System_Int32 = logic_uScriptAct_AddInt_v2_IntResult_75;
	}

	private void Relay_In_78()
	{
		logic_uScriptAct_AddInt_v2_A_78 = local_Stage_System_Int32;
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_78.In(logic_uScriptAct_AddInt_v2_A_78, logic_uScriptAct_AddInt_v2_B_78, out logic_uScriptAct_AddInt_v2_IntResult_78, out logic_uScriptAct_AddInt_v2_FloatResult_78);
		local_Stage_System_Int32 = logic_uScriptAct_AddInt_v2_IntResult_78;
	}

	private void Relay_In_81()
	{
		logic_uScript_AddMessage_messageData_81 = msg03EnemyIncoming;
		logic_uScript_AddMessage_speaker_81 = messageSpeaker;
		logic_uScript_AddMessage_Return_81 = logic_uScript_AddMessage_uScript_AddMessage_81.In(logic_uScript_AddMessage_messageData_81, logic_uScript_AddMessage_speaker_81);
	}

	private void Relay_In_83()
	{
		logic_uScript_AddMessage_messageData_83 = msg04FightEnemy;
		logic_uScript_AddMessage_speaker_83 = messageSpeaker;
		logic_uScript_AddMessage_Return_83 = logic_uScript_AddMessage_uScript_AddMessage_83.In(logic_uScript_AddMessage_messageData_83, logic_uScript_AddMessage_speaker_83);
		if (logic_uScript_AddMessage_uScript_AddMessage_83.Shown)
		{
			Relay_True_29();
		}
	}

	private void Relay_In_87()
	{
		logic_uScriptAct_AddInt_v2_A_87 = local_Stage_System_Int32;
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_87.In(logic_uScriptAct_AddInt_v2_A_87, logic_uScriptAct_AddInt_v2_B_87, out logic_uScriptAct_AddInt_v2_IntResult_87, out logic_uScriptAct_AddInt_v2_FloatResult_87);
		local_Stage_System_Int32 = logic_uScriptAct_AddInt_v2_IntResult_87;
	}

	private void Relay_In_89()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_89.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_89.Out)
		{
			Relay_In_87();
		}
	}

	private void Relay_In_92()
	{
		logic_uScriptCon_CompareBool_Bool_92 = local_BlockAttachedOrDrivenAway_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_92.In(logic_uScriptCon_CompareBool_Bool_92);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_92.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_92.False;
		if (num)
		{
			Relay_In_105();
		}
		if (flag)
		{
			Relay_In_122();
		}
	}

	private void Relay_True_94()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_94.True(out logic_uScriptAct_SetBool_Target_94);
		local_BlockAttachedOrDrivenAway_System_Boolean = logic_uScriptAct_SetBool_Target_94;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_94.Out)
		{
			Relay_In_102();
		}
	}

	private void Relay_False_94()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_94.False(out logic_uScriptAct_SetBool_Target_94);
		local_BlockAttachedOrDrivenAway_System_Boolean = logic_uScriptAct_SetBool_Target_94;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_94.Out)
		{
			Relay_In_102();
		}
	}

	private void Relay_Save_Out_97()
	{
	}

	private void Relay_Load_Out_97()
	{
	}

	private void Relay_Restart_Out_97()
	{
	}

	private void Relay_Save_97()
	{
		logic_SubGraph_SaveLoadBool_boolean_97 = local_BlockAttachedOrDrivenAway_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_97 = local_BlockAttachedOrDrivenAway_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_97.Save(ref logic_SubGraph_SaveLoadBool_boolean_97, logic_SubGraph_SaveLoadBool_boolAsVariable_97, logic_SubGraph_SaveLoadBool_uniqueID_97);
	}

	private void Relay_Load_97()
	{
		logic_SubGraph_SaveLoadBool_boolean_97 = local_BlockAttachedOrDrivenAway_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_97 = local_BlockAttachedOrDrivenAway_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_97.Load(ref logic_SubGraph_SaveLoadBool_boolean_97, logic_SubGraph_SaveLoadBool_boolAsVariable_97, logic_SubGraph_SaveLoadBool_uniqueID_97);
	}

	private void Relay_Set_True_97()
	{
		logic_SubGraph_SaveLoadBool_boolean_97 = local_BlockAttachedOrDrivenAway_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_97 = local_BlockAttachedOrDrivenAway_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_97.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_97, logic_SubGraph_SaveLoadBool_boolAsVariable_97, logic_SubGraph_SaveLoadBool_uniqueID_97);
	}

	private void Relay_Set_False_97()
	{
		logic_SubGraph_SaveLoadBool_boolean_97 = local_BlockAttachedOrDrivenAway_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_97 = local_BlockAttachedOrDrivenAway_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_97.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_97, logic_SubGraph_SaveLoadBool_boolAsVariable_97, logic_SubGraph_SaveLoadBool_uniqueID_97);
	}

	private void Relay_In_98()
	{
		logic_uScript_HideHintFloating_uScript_HideHintFloating_98.In();
		if (logic_uScript_HideHintFloating_uScript_HideHintFloating_98.Out)
		{
			Relay_In_78();
		}
	}

	private void Relay_In_99()
	{
		logic_uScript_HideHintFloating_uScript_HideHintFloating_99.In();
		if (logic_uScript_HideHintFloating_uScript_HideHintFloating_99.Out)
		{
			Relay_In_45();
		}
	}

	private void Relay_In_100()
	{
		logic_uScript_HideHintFloating_uScript_HideHintFloating_100.In();
		if (logic_uScript_HideHintFloating_uScript_HideHintFloating_100.Out)
		{
			Relay_In_129();
		}
	}

	private void Relay_In_101()
	{
		logic_uScript_HideHintFloating_uScript_HideHintFloating_101.In();
		if (logic_uScript_HideHintFloating_uScript_HideHintFloating_101.Out)
		{
			Relay_In_23();
		}
	}

	private void Relay_In_102()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_102 = messageTag;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_102.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_102, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_102);
	}

	private void Relay_In_105()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_105.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_105.Out)
		{
			Relay_In_89();
		}
	}

	private void Relay_In_106()
	{
		logic_uScript_IsPlayerHoldingAnyBlock_uScript_IsPlayerHoldingAnyBlock_106.In();
		if (logic_uScript_IsPlayerHoldingAnyBlock_uScript_IsPlayerHoldingAnyBlock_106.Holding)
		{
			Relay_In_108();
		}
	}

	private void Relay_In_107()
	{
		logic_uScript_ShowHint_hintId_107 = local_109_GameHints_HintID;
		logic_uScript_ShowHint_uScript_ShowHint_107.In(logic_uScript_ShowHint_hintId_107);
	}

	private void Relay_In_108()
	{
		logic_uScript_HasHintBeenShownBefore_hintID_108 = local_109_GameHints_HintID;
		logic_uScript_HasHintBeenShownBefore_uScript_HasHintBeenShownBefore_108.In(logic_uScript_HasHintBeenShownBefore_hintID_108);
		if (logic_uScript_HasHintBeenShownBefore_uScript_HasHintBeenShownBefore_108.NotShown)
		{
			Relay_In_107();
		}
	}

	private void Relay_Out_112()
	{
		Relay_In_124();
	}

	private void Relay_Shown_112()
	{
	}

	private void Relay_In_112()
	{
		logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboard_112 = msg01TryDriving;
		logic_SubGraph_AddMessageWithPadSupport_messageControlPad_112 = msg01TryDriving_Pad;
		logic_SubGraph_AddMessageWithPadSupport_speaker_112 = messageSpeaker;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_112.In(logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboard_112, logic_SubGraph_AddMessageWithPadSupport_messageControlPad_112, logic_SubGraph_AddMessageWithPadSupport_speaker_112);
	}

	private void Relay_Out_116()
	{
		Relay_In_125();
	}

	private void Relay_Shown_116()
	{
	}

	private void Relay_In_116()
	{
		logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboard_116 = msg02TryShooting;
		logic_SubGraph_AddMessageWithPadSupport_messageControlPad_116 = msg02TryShooting_Pad;
		logic_SubGraph_AddMessageWithPadSupport_speaker_116 = messageSpeaker;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_116.In(logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboard_116, logic_SubGraph_AddMessageWithPadSupport_messageControlPad_116, logic_SubGraph_AddMessageWithPadSupport_speaker_116);
	}

	private void Relay_Out_122()
	{
		Relay_In_10();
	}

	private void Relay_Shown_122()
	{
	}

	private void Relay_In_122()
	{
		logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboard_122 = msg06ScavengeBlocks;
		logic_SubGraph_AddMessageWithPadSupport_messageControlPad_122 = msg06ScavengeBlocks_Pad;
		logic_SubGraph_AddMessageWithPadSupport_speaker_122 = messageSpeaker;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_122.In(logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboard_122, logic_SubGraph_AddMessageWithPadSupport_messageControlPad_122, logic_SubGraph_AddMessageWithPadSupport_speaker_122);
	}

	private void Relay_Out_124()
	{
		Relay_In_4();
	}

	private void Relay_In_124()
	{
		logic_SubGraph_ShowHintWithPadSupport_SubGraph_ShowHintWithPadSupport_124.In(logic_SubGraph_ShowHintWithPadSupport_hintControlPad_124, logic_SubGraph_ShowHintWithPadSupport_hintMouseKeyboard_124);
	}

	private void Relay_Out_125()
	{
		Relay_In_6();
	}

	private void Relay_In_125()
	{
		logic_SubGraph_ShowHintWithPadSupport_SubGraph_ShowHintWithPadSupport_125.In(logic_SubGraph_ShowHintWithPadSupport_hintControlPad_125, logic_SubGraph_ShowHintWithPadSupport_hintMouseKeyboard_125);
	}

	private void Relay_Out_126()
	{
		Relay_StartTimer_19();
	}

	private void Relay_In_126()
	{
		logic_SubGraph_ShowHintWithPadSupport_SubGraph_ShowHintWithPadSupport_126.In(logic_SubGraph_ShowHintWithPadSupport_hintControlPad_126, logic_SubGraph_ShowHintWithPadSupport_hintMouseKeyboard_126);
	}

	private void Relay_Out_127()
	{
		Relay_In_126();
	}

	private void Relay_Shown_127()
	{
	}

	private void Relay_In_127()
	{
		logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboard_127 = msg05ShootReminder;
		logic_SubGraph_AddMessageWithPadSupport_messageControlPad_127 = msg05ShootReminder_Pad;
		logic_SubGraph_AddMessageWithPadSupport_speaker_127 = messageSpeaker;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_127.In(logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboard_127, logic_SubGraph_AddMessageWithPadSupport_messageControlPad_127, logic_SubGraph_AddMessageWithPadSupport_speaker_127);
	}

	private void Relay_In_129()
	{
		logic_uScript_CompleteAchievement_uScript_CompleteAchievement_129.In(logic_uScript_CompleteAchievement_achievementID_129);
		if (logic_uScript_CompleteAchievement_uScript_CompleteAchievement_129.Out)
		{
			Relay_In_92();
		}
	}

	private void Relay_In_130()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_130 = messageTag;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_130.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_130, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_130);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_130.Out)
		{
			Relay_In_12();
		}
	}

	private void Relay_In_132()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_132.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_132.Out)
		{
			Relay_In_12();
		}
	}

	private void Relay_In_133()
	{
		logic_uScript_SendAnaliticsEvent_uScript_SendAnaliticsEvent_133.In(logic_uScript_SendAnaliticsEvent_analiticsEvent_133, logic_uScript_SendAnaliticsEvent_parameterName_133, logic_uScript_SendAnaliticsEvent_parameter_133);
		if (logic_uScript_SendAnaliticsEvent_uScript_SendAnaliticsEvent_133.Out)
		{
			Relay_Succeed_74();
		}
	}
}
