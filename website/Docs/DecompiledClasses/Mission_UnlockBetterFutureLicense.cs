using System;
using BehaviorDesigner.Runtime;
using UnityEngine;

[Serializable]
[FriendlyName("Untitled", "")]
[NodePath("Graphs")]
public class Mission_UnlockBetterFutureLicense : uScriptLogic
{
	private GameObject parentGameObject;

	private uScript_GUI thisScriptsOnGuiListener;

	private bool m_RegisteredForEvents;

	public bool _DEBUGIgnoreMoneyCheck;

	[Multiline(3)]
	public string clearMsgTagWhenNormal = "Angry";

	public float clearSceneryRadius;

	public float distNPCFound;

	public BlockTypes interactableBlockType;

	public int LicenseCost;

	private bool local_170_System_Boolean;

	private TankBlock local_175_TankBlock;

	private Tank[] local_53_TankArray = new Tank[0];

	private int local_91_System_Int32;

	private int local_CurrentMoney_System_Int32;

	private bool local_HasEnoughMoney_System_Boolean;

	private bool local_Init_System_Boolean;

	private bool local_LicensePurchased_System_Boolean;

	private bool local_msg01Shown_System_Boolean;

	private ManOnScreenMessages.OnScreenMessage local_Msg02ClickScreen_ManOnScreenMessages_OnScreenMessage;

	private bool local_msg03aShown_System_Boolean;

	private bool local_msg03bShown_System_Boolean;

	private bool local_NearNPC_System_Boolean;

	private Tank local_NPCTech_Tank;

	private int local_Stage_System_Int32 = 1;

	private TankBlock local_TerminalBlock_TankBlock;

	private bool local_WaitingOnPrompt_System_Boolean;

	public uScript_AddMessage.MessageSpeaker messageSpeaker;

	[Multiline(3)]
	public string messageTag = "";

	public uScript_AddMessage.MessageData msg01Intro;

	public uScript_AddMessage.MessageData msg02ClickScreen;

	public uScript_AddMessage.MessageData msg02ClickScreen_Pad;

	public uScript_AddMessage.MessageData msg03aPurchaseDeclined;

	public uScript_AddMessage.MessageData msg03bNotEnoughMoney;

	public uScript_AddMessage.MessageData msg05LicensePurchased;

	public LocalisedString msgPromptAccept;

	public LocalisedString msgPromptDecline;

	public LocalisedString msgPromptNoMoney;

	public LocalisedString msgPromptText;

	public Transform NPCDespawnParticleEffect;

	public ExternalBehaviorTree NPCFlyAwayAI;

	[Multiline(3)]
	public string NPCPosition = "";

	public SpawnTechData[] NPCSpawnData = new SpawnTechData[0];

	private GameObject owner_Connection_8;

	private GameObject owner_Connection_10;

	private GameObject owner_Connection_22;

	private GameObject owner_Connection_24;

	private GameObject owner_Connection_26;

	private GameObject owner_Connection_32;

	private GameObject owner_Connection_33;

	private GameObject owner_Connection_42;

	private GameObject owner_Connection_169;

	private SubGraph_SaveLoadInt logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_0 = new SubGraph_SaveLoadInt();

	private int logic_SubGraph_SaveLoadInt_restartValue_0 = 1;

	private int logic_SubGraph_SaveLoadInt_integer_0;

	private object logic_SubGraph_SaveLoadInt_intAsVariable_0 = "";

	private string logic_SubGraph_SaveLoadInt_uniqueID_0 = "Stage";

	private uScript_SetTankInvulnerable logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_2 = new uScript_SetTankInvulnerable();

	private bool logic_uScript_SetTankInvulnerable_invulnerable_2 = true;

	private Tank logic_uScript_SetTankInvulnerable_tank_2;

	private bool logic_uScript_SetTankInvulnerable_Out_2 = true;

	private uScript_SetEncounterTarget logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_6 = new uScript_SetEncounterTarget();

	private GameObject logic_uScript_SetEncounterTarget_owner_6;

	private object logic_uScript_SetEncounterTarget_visibleObject_6 = "";

	private bool logic_uScript_SetEncounterTarget_Out_6 = true;

	private uScript_PausePopulation logic_uScript_PausePopulation_uScript_PausePopulation_7 = new uScript_PausePopulation();

	private bool logic_uScript_PausePopulation_Out_7 = true;

	private SubGraph_CompleteObjectiveStage logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_11 = new SubGraph_CompleteObjectiveStage();

	private int logic_SubGraph_CompleteObjectiveStage_objectiveStage_11;

	private bool logic_SubGraph_CompleteObjectiveStage_isFinalObjective_11;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_13 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_13;

	private bool logic_uScriptCon_CompareBool_True_13 = true;

	private bool logic_uScriptCon_CompareBool_False_13 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_14 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_14 = new Tank[0];

	private int logic_uScript_AccessListTech_index_14;

	private Tank logic_uScript_AccessListTech_value_14;

	private bool logic_uScript_AccessListTech_Out_14 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_15 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_15;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_15;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_15;

	private bool logic_uScript_AddMessage_Out_15 = true;

	private bool logic_uScript_AddMessage_Shown_15 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_17 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_17;

	private bool logic_uScriptAct_SetBool_Out_17 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_17 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_17 = true;

	private uScript_RemoveScenery logic_uScript_RemoveScenery_uScript_RemoveScenery_19 = new uScript_RemoveScenery();

	private GameObject logic_uScript_RemoveScenery_ownerNode_19;

	private string logic_uScript_RemoveScenery_positionName_19 = "";

	private float logic_uScript_RemoveScenery_radius_19;

	private bool logic_uScript_RemoveScenery_preventChunksSpawning_19 = true;

	private bool logic_uScript_RemoveScenery_Out_19 = true;

	private uScriptCon_ManualSwitch logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_21 = new uScriptCon_ManualSwitch();

	private int logic_uScriptCon_ManualSwitch_CurrentOutput_21;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_23 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_23;

	private bool logic_uScriptAct_SetBool_Out_23 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_23 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_23 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_30 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_30 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_34 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_34 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_34;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_34 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_34;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_34 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_34 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_34 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_34 = true;

	private uScript_FinishEncounter logic_uScript_FinishEncounter_uScript_FinishEncounter_35 = new uScript_FinishEncounter();

	private GameObject logic_uScript_FinishEncounter_owner_35;

	private bool logic_uScript_FinishEncounter_Out_35 = true;

	private uScript_DirectEnemiesOutOfEncounter logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_37 = new uScript_DirectEnemiesOutOfEncounter();

	private GameObject logic_uScript_DirectEnemiesOutOfEncounter_ownerNode_37;

	private bool logic_uScript_DirectEnemiesOutOfEncounter_Out_37 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_38 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_38 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_38;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_38 = 0.3f;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_38;

	private bool logic_uScript_SpawnTechsFromData_Out_38 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_40 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_40 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_40 = true;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_40 = true;

	private uScript_IsPlayerInRangeOfTech logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_43 = new uScript_IsPlayerInRangeOfTech();

	private Tank logic_uScript_IsPlayerInRangeOfTech_tech_43;

	private float logic_uScript_IsPlayerInRangeOfTech_range_43;

	private Tank[] logic_uScript_IsPlayerInRangeOfTech_techs_43 = new Tank[0];

	private bool logic_uScript_IsPlayerInRangeOfTech_Out_43 = true;

	private bool logic_uScript_IsPlayerInRangeOfTech_InRange_43 = true;

	private bool logic_uScript_IsPlayerInRangeOfTech_OutOfRange_43 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_44 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_44;

	private bool logic_uScriptAct_SetBool_Out_44 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_44 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_44 = true;

	private SubGraph_LoadObjectiveStates logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_47 = new SubGraph_LoadObjectiveStates();

	private int logic_SubGraph_LoadObjectiveStates_currentObjective_47;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_50 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_50;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_50 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_50 = "Init";

	private uScript_PausePopulation logic_uScript_PausePopulation_uScript_PausePopulation_54 = new uScript_PausePopulation();

	private bool logic_uScript_PausePopulation_Out_54 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_55 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_55;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_55 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_55 = "LicensePurchased";

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_58 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_58;

	private bool logic_uScriptCon_CompareBool_True_58 = true;

	private bool logic_uScriptCon_CompareBool_False_58 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_63 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_63;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_63 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_63 = "msg01Shown";

	private uScript_FlyTechUpAndAway logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_64 = new uScript_FlyTechUpAndAway();

	private Tank logic_uScript_FlyTechUpAndAway_tech_64;

	private float logic_uScript_FlyTechUpAndAway_maxLifetime_64 = 5f;

	private float logic_uScript_FlyTechUpAndAway_targetHeight_64 = 150f;

	private ExternalBehaviorTree logic_uScript_FlyTechUpAndAway_aiTree_64;

	private Transform logic_uScript_FlyTechUpAndAway_removalParticles_64;

	private bool logic_uScript_FlyTechUpAndAway_Out_64 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_68 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_68;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_68;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_68;

	private bool logic_uScript_AddMessage_Out_68 = true;

	private bool logic_uScript_AddMessage_Shown_68 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_72 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_72;

	private bool logic_uScriptCon_CompareBool_True_72 = true;

	private bool logic_uScriptCon_CompareBool_False_72 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_74 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_74;

	private bool logic_uScriptAct_SetBool_Out_74 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_74 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_74 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_76 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_76;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_76 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_76 = "msg03aShown";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_77 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_77;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_77 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_77 = "msg03bShown";

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_79 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_79;

	private bool logic_uScriptCon_CompareBool_True_79 = true;

	private bool logic_uScriptCon_CompareBool_False_79 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_82 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_82;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_82;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_82;

	private bool logic_uScript_AddMessage_Out_82 = true;

	private bool logic_uScript_AddMessage_Shown_82 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_86 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_86;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_86;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_86;

	private bool logic_uScript_AddMessage_Out_86 = true;

	private bool logic_uScript_AddMessage_Shown_86 = true;

	private uScript_AddMoney logic_uScript_AddMoney_uScript_AddMoney_89 = new uScript_AddMoney();

	private int logic_uScript_AddMoney_amount_89;

	private bool logic_uScript_AddMoney_Out_89 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_92 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_92;

	private bool logic_uScriptAct_SetBool_Out_92 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_92 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_92 = true;

	private uScriptAct_MultiplyInt_v2 logic_uScriptAct_MultiplyInt_v2_uScriptAct_MultiplyInt_v2_93 = new uScriptAct_MultiplyInt_v2();

	private int logic_uScriptAct_MultiplyInt_v2_A_93;

	private int logic_uScriptAct_MultiplyInt_v2_B_93 = -1;

	private int logic_uScriptAct_MultiplyInt_v2_IntResult_93;

	private float logic_uScriptAct_MultiplyInt_v2_FloatResult_93;

	private bool logic_uScriptAct_MultiplyInt_v2_Out_93 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_94 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_94;

	private bool logic_uScriptAct_SetBool_Out_94 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_94 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_94 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_97 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_97;

	private bool logic_uScriptCon_CompareBool_True_97 = true;

	private bool logic_uScriptCon_CompareBool_False_97 = true;

	private uScript_GetTankBlock logic_uScript_GetTankBlock_uScript_GetTankBlock_98 = new uScript_GetTankBlock();

	private Tank logic_uScript_GetTankBlock_tank_98;

	private BlockTypes logic_uScript_GetTankBlock_blockType_98;

	private TankBlock logic_uScript_GetTankBlock_Return_98;

	private bool logic_uScript_GetTankBlock_Out_98 = true;

	private bool logic_uScript_GetTankBlock_Returned_98 = true;

	private bool logic_uScript_GetTankBlock_NotFound_98 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_103 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_103;

	private bool logic_uScriptCon_CompareBool_True_103 = true;

	private bool logic_uScriptCon_CompareBool_False_103 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_105 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_105;

	private bool logic_uScriptAct_SetBool_Out_105 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_105 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_105 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_107 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_107;

	private bool logic_uScriptCon_CompareBool_True_107 = true;

	private bool logic_uScriptCon_CompareBool_False_107 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_109 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_109;

	private bool logic_uScriptAct_SetBool_Out_109 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_109 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_109 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_110 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_110 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_111 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_111 = true;

	private uScript_HideArrow logic_uScript_HideArrow_uScript_HideArrow_113 = new uScript_HideArrow();

	private bool logic_uScript_HideArrow_Out_113 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_114 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_114 = "";

	private bool logic_uScript_EnableGlow_enable_114;

	private bool logic_uScript_EnableGlow_Out_114 = true;

	private uScript_GetTankBlock logic_uScript_GetTankBlock_uScript_GetTankBlock_117 = new uScript_GetTankBlock();

	private Tank logic_uScript_GetTankBlock_tank_117;

	private BlockTypes logic_uScript_GetTankBlock_blockType_117;

	private TankBlock logic_uScript_GetTankBlock_Return_117;

	private bool logic_uScript_GetTankBlock_Out_117 = true;

	private bool logic_uScript_GetTankBlock_Returned_117 = true;

	private bool logic_uScript_GetTankBlock_NotFound_117 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_119 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_119;

	private bool logic_uScriptAct_SetBool_Out_119 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_119 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_119 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_122 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_122;

	private bool logic_uScriptAct_SetBool_Out_122 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_122 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_122 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_123 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_123;

	private bool logic_uScriptAct_SetBool_Out_123 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_123 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_123 = true;

	private SubGraph_AddMessageWithPadSupport logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_125 = new SubGraph_AddMessageWithPadSupport();

	private uScript_AddMessage.MessageData logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboard_125;

	private uScript_AddMessage.MessageData logic_SubGraph_AddMessageWithPadSupport_messageControlPad_125;

	private uScript_AddMessage.MessageSpeaker logic_SubGraph_AddMessageWithPadSupport_speaker_125;

	private ManOnScreenMessages.OnScreenMessage logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboardReturn_125;

	private ManOnScreenMessages.OnScreenMessage logic_SubGraph_AddMessageWithPadSupport_messageControlPadReturn_125;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_130 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_130 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_132 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_132;

	private bool logic_uScriptCon_CompareBool_True_132 = true;

	private bool logic_uScriptCon_CompareBool_False_132 = true;

	private uScript_MissionPromptBlock_Show logic_uScript_MissionPromptBlock_Show_uScript_MissionPromptBlock_Show_133 = new uScript_MissionPromptBlock_Show();

	private LocalisedString logic_uScript_MissionPromptBlock_Show_bodyText_133;

	private LocalisedString logic_uScript_MissionPromptBlock_Show_acceptButtonText_133;

	private LocalisedString logic_uScript_MissionPromptBlock_Show_rejectButtonText_133;

	private TankBlock logic_uScript_MissionPromptBlock_Show_targetBlock_133;

	private bool logic_uScript_MissionPromptBlock_Show_highlightBlock_133 = true;

	private bool logic_uScript_MissionPromptBlock_Show_singleUse_133;

	private bool logic_uScript_MissionPromptBlock_Show_Out_133 = true;

	private uScript_GetCurrentMoneyEarned logic_uScript_GetCurrentMoneyEarned_uScript_GetCurrentMoneyEarned_135 = new uScript_GetCurrentMoneyEarned();

	private int logic_uScript_GetCurrentMoneyEarned_Return_135;

	private bool logic_uScript_GetCurrentMoneyEarned_Out_135 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_138 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_138 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_140 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_140;

	private bool logic_uScriptCon_CompareBool_True_140 = true;

	private bool logic_uScriptCon_CompareBool_False_140 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_142 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_142;

	private bool logic_uScriptAct_SetBool_Out_142 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_142 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_142 = true;

	private uScriptCon_CompareInt logic_uScriptCon_CompareInt_uScriptCon_CompareInt_145 = new uScriptCon_CompareInt();

	private int logic_uScriptCon_CompareInt_A_145;

	private int logic_uScriptCon_CompareInt_B_145;

	private bool logic_uScriptCon_CompareInt_GreaterThan_145 = true;

	private bool logic_uScriptCon_CompareInt_GreaterThanOrEqualTo_145 = true;

	private bool logic_uScriptCon_CompareInt_EqualTo_145 = true;

	private bool logic_uScriptCon_CompareInt_NotEqualTo_145 = true;

	private bool logic_uScriptCon_CompareInt_LessThanOrEqualTo_145 = true;

	private bool logic_uScriptCon_CompareInt_LessThan_145 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_149 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_149;

	private bool logic_uScriptCon_CompareBool_True_149 = true;

	private bool logic_uScriptCon_CompareBool_False_149 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_154 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_154 = true;

	private uScriptCon_CompareInt logic_uScriptCon_CompareInt_uScriptCon_CompareInt_157 = new uScriptCon_CompareInt();

	private int logic_uScriptCon_CompareInt_A_157;

	private int logic_uScriptCon_CompareInt_B_157;

	private bool logic_uScriptCon_CompareInt_GreaterThan_157 = true;

	private bool logic_uScriptCon_CompareInt_GreaterThanOrEqualTo_157 = true;

	private bool logic_uScriptCon_CompareInt_EqualTo_157 = true;

	private bool logic_uScriptCon_CompareInt_NotEqualTo_157 = true;

	private bool logic_uScriptCon_CompareInt_LessThanOrEqualTo_157 = true;

	private bool logic_uScriptCon_CompareInt_LessThan_157 = true;

	private uScript_MissionPromptBlock_Show logic_uScript_MissionPromptBlock_Show_uScript_MissionPromptBlock_Show_158 = new uScript_MissionPromptBlock_Show();

	private LocalisedString logic_uScript_MissionPromptBlock_Show_bodyText_158;

	private LocalisedString logic_uScript_MissionPromptBlock_Show_acceptButtonText_158;

	private LocalisedString logic_uScript_MissionPromptBlock_Show_rejectButtonText_158;

	private TankBlock logic_uScript_MissionPromptBlock_Show_targetBlock_158;

	private bool logic_uScript_MissionPromptBlock_Show_highlightBlock_158 = true;

	private bool logic_uScript_MissionPromptBlock_Show_singleUse_158;

	private bool logic_uScript_MissionPromptBlock_Show_Out_158 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_160 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_160;

	private bool logic_uScriptCon_CompareBool_True_160 = true;

	private bool logic_uScriptCon_CompareBool_False_160 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_161 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_161;

	private bool logic_uScriptAct_SetBool_Out_161 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_161 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_161 = true;

	private uScript_GetCurrentMoneyEarned logic_uScript_GetCurrentMoneyEarned_uScript_GetCurrentMoneyEarned_162 = new uScript_GetCurrentMoneyEarned();

	private int logic_uScript_GetCurrentMoneyEarned_Return_162;

	private bool logic_uScript_GetCurrentMoneyEarned_Out_162 = true;

	private uScript_CompareBlock logic_uScript_CompareBlock_uScript_CompareBlock_165 = new uScript_CompareBlock();

	private TankBlock logic_uScript_CompareBlock_A_165;

	private TankBlock logic_uScript_CompareBlock_B_165;

	private bool logic_uScript_CompareBlock_EqualTo_165 = true;

	private bool logic_uScript_CompareBlock_NotEqualTo_165 = true;

	private uScript_RemoveOnScreenMessage logic_uScript_RemoveOnScreenMessage_uScript_RemoveOnScreenMessage_166 = new uScript_RemoveOnScreenMessage();

	private ManOnScreenMessages.OnScreenMessage logic_uScript_RemoveOnScreenMessage_onScreenMessage_166;

	private bool logic_uScript_RemoveOnScreenMessage_instant_166;

	private bool logic_uScript_RemoveOnScreenMessage_Out_166 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_168 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_168;

	private bool logic_uScriptCon_CompareBool_True_168 = true;

	private bool logic_uScriptCon_CompareBool_False_168 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_172 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_172 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_172 = true;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_172 = true;

	private uScript_MissionPromptBlock_Hide logic_uScript_MissionPromptBlock_Hide_uScript_MissionPromptBlock_Hide_176 = new uScript_MissionPromptBlock_Hide();

	private TankBlock logic_uScript_MissionPromptBlock_Hide_targetBlock_176;

	private bool logic_uScript_MissionPromptBlock_Hide_Out_176 = true;

	private TankBlock event_UnityEngine_GameObject_TankBlock_164;

	private bool event_UnityEngine_GameObject_Accepted_164;

	private void SyncUnityHooks()
	{
		SyncEventListeners();
		if (null == owner_Connection_8 || !m_RegisteredForEvents)
		{
			owner_Connection_8 = parentGameObject;
			if (null != owner_Connection_8)
			{
				uScript_EncounterUpdate uScript_EncounterUpdate2 = owner_Connection_8.GetComponent<uScript_EncounterUpdate>();
				if (null == uScript_EncounterUpdate2)
				{
					uScript_EncounterUpdate2 = owner_Connection_8.AddComponent<uScript_EncounterUpdate>();
				}
				if (null != uScript_EncounterUpdate2)
				{
					uScript_EncounterUpdate2.OnUpdate += Instance_OnUpdate_27;
					uScript_EncounterUpdate2.OnSuspend += Instance_OnSuspend_27;
					uScript_EncounterUpdate2.OnResume += Instance_OnResume_27;
				}
			}
		}
		if (null == owner_Connection_10 || !m_RegisteredForEvents)
		{
			owner_Connection_10 = parentGameObject;
		}
		if (null == owner_Connection_22 || !m_RegisteredForEvents)
		{
			owner_Connection_22 = parentGameObject;
		}
		if (null == owner_Connection_24 || !m_RegisteredForEvents)
		{
			owner_Connection_24 = parentGameObject;
		}
		if (null == owner_Connection_26 || !m_RegisteredForEvents)
		{
			owner_Connection_26 = parentGameObject;
		}
		if (null == owner_Connection_32 || !m_RegisteredForEvents)
		{
			owner_Connection_32 = parentGameObject;
			if (null != owner_Connection_32)
			{
				uScript_SaveLoad uScript_SaveLoad2 = owner_Connection_32.GetComponent<uScript_SaveLoad>();
				if (null == uScript_SaveLoad2)
				{
					uScript_SaveLoad2 = owner_Connection_32.AddComponent<uScript_SaveLoad>();
				}
				if (null != uScript_SaveLoad2)
				{
					uScript_SaveLoad2.SaveEvent += Instance_SaveEvent_48;
					uScript_SaveLoad2.LoadEvent += Instance_LoadEvent_48;
					uScript_SaveLoad2.RestartEvent += Instance_RestartEvent_48;
				}
			}
		}
		if (null == owner_Connection_33 || !m_RegisteredForEvents)
		{
			owner_Connection_33 = parentGameObject;
		}
		if (null == owner_Connection_42 || !m_RegisteredForEvents)
		{
			owner_Connection_42 = parentGameObject;
		}
		if (!(null == owner_Connection_169) && m_RegisteredForEvents)
		{
			return;
		}
		owner_Connection_169 = parentGameObject;
		if (null != owner_Connection_169)
		{
			uScript_MissionPromptBlock_OnResult uScript_MissionPromptBlock_OnResult2 = owner_Connection_169.GetComponent<uScript_MissionPromptBlock_OnResult>();
			if (null == uScript_MissionPromptBlock_OnResult2)
			{
				uScript_MissionPromptBlock_OnResult2 = owner_Connection_169.AddComponent<uScript_MissionPromptBlock_OnResult>();
			}
			if (null != uScript_MissionPromptBlock_OnResult2)
			{
				uScript_MissionPromptBlock_OnResult2.ResponseEvent += Instance_ResponseEvent_164;
			}
		}
	}

	private void RegisterForUnityHooks()
	{
		SyncEventListeners();
		if (!m_RegisteredForEvents && null != owner_Connection_8)
		{
			uScript_EncounterUpdate uScript_EncounterUpdate2 = owner_Connection_8.GetComponent<uScript_EncounterUpdate>();
			if (null == uScript_EncounterUpdate2)
			{
				uScript_EncounterUpdate2 = owner_Connection_8.AddComponent<uScript_EncounterUpdate>();
			}
			if (null != uScript_EncounterUpdate2)
			{
				uScript_EncounterUpdate2.OnUpdate += Instance_OnUpdate_27;
				uScript_EncounterUpdate2.OnSuspend += Instance_OnSuspend_27;
				uScript_EncounterUpdate2.OnResume += Instance_OnResume_27;
			}
		}
		if (!m_RegisteredForEvents && null != owner_Connection_32)
		{
			uScript_SaveLoad uScript_SaveLoad2 = owner_Connection_32.GetComponent<uScript_SaveLoad>();
			if (null == uScript_SaveLoad2)
			{
				uScript_SaveLoad2 = owner_Connection_32.AddComponent<uScript_SaveLoad>();
			}
			if (null != uScript_SaveLoad2)
			{
				uScript_SaveLoad2.SaveEvent += Instance_SaveEvent_48;
				uScript_SaveLoad2.LoadEvent += Instance_LoadEvent_48;
				uScript_SaveLoad2.RestartEvent += Instance_RestartEvent_48;
			}
		}
		if (!m_RegisteredForEvents && null != owner_Connection_169)
		{
			uScript_MissionPromptBlock_OnResult uScript_MissionPromptBlock_OnResult2 = owner_Connection_169.GetComponent<uScript_MissionPromptBlock_OnResult>();
			if (null == uScript_MissionPromptBlock_OnResult2)
			{
				uScript_MissionPromptBlock_OnResult2 = owner_Connection_169.AddComponent<uScript_MissionPromptBlock_OnResult>();
			}
			if (null != uScript_MissionPromptBlock_OnResult2)
			{
				uScript_MissionPromptBlock_OnResult2.ResponseEvent += Instance_ResponseEvent_164;
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
			uScript_EncounterUpdate component = owner_Connection_8.GetComponent<uScript_EncounterUpdate>();
			if (null != component)
			{
				component.OnUpdate -= Instance_OnUpdate_27;
				component.OnSuspend -= Instance_OnSuspend_27;
				component.OnResume -= Instance_OnResume_27;
			}
		}
		if (null != owner_Connection_32)
		{
			uScript_SaveLoad component2 = owner_Connection_32.GetComponent<uScript_SaveLoad>();
			if (null != component2)
			{
				component2.SaveEvent -= Instance_SaveEvent_48;
				component2.LoadEvent -= Instance_LoadEvent_48;
				component2.RestartEvent -= Instance_RestartEvent_48;
			}
		}
		if (null != owner_Connection_169)
		{
			uScript_MissionPromptBlock_OnResult component3 = owner_Connection_169.GetComponent<uScript_MissionPromptBlock_OnResult>();
			if (null != component3)
			{
				component3.ResponseEvent -= Instance_ResponseEvent_164;
			}
		}
	}

	public override void SetParent(GameObject g)
	{
		parentGameObject = g;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_0.SetParent(g);
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_2.SetParent(g);
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_6.SetParent(g);
		logic_uScript_PausePopulation_uScript_PausePopulation_7.SetParent(g);
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_11.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_13.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_14.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_15.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_17.SetParent(g);
		logic_uScript_RemoveScenery_uScript_RemoveScenery_19.SetParent(g);
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_21.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_23.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_30.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_34.SetParent(g);
		logic_uScript_FinishEncounter_uScript_FinishEncounter_35.SetParent(g);
		logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_37.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_38.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_40.SetParent(g);
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_43.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_44.SetParent(g);
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_47.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_50.SetParent(g);
		logic_uScript_PausePopulation_uScript_PausePopulation_54.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_55.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_58.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_63.SetParent(g);
		logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_64.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_68.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_72.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_74.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_76.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_77.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_79.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_82.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_86.SetParent(g);
		logic_uScript_AddMoney_uScript_AddMoney_89.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_92.SetParent(g);
		logic_uScriptAct_MultiplyInt_v2_uScriptAct_MultiplyInt_v2_93.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_94.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_97.SetParent(g);
		logic_uScript_GetTankBlock_uScript_GetTankBlock_98.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_103.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_105.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_107.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_109.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_110.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_111.SetParent(g);
		logic_uScript_HideArrow_uScript_HideArrow_113.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_114.SetParent(g);
		logic_uScript_GetTankBlock_uScript_GetTankBlock_117.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_119.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_122.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_123.SetParent(g);
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_125.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_130.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_132.SetParent(g);
		logic_uScript_MissionPromptBlock_Show_uScript_MissionPromptBlock_Show_133.SetParent(g);
		logic_uScript_GetCurrentMoneyEarned_uScript_GetCurrentMoneyEarned_135.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_138.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_140.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_142.SetParent(g);
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_145.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_149.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_154.SetParent(g);
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_157.SetParent(g);
		logic_uScript_MissionPromptBlock_Show_uScript_MissionPromptBlock_Show_158.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_160.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_161.SetParent(g);
		logic_uScript_GetCurrentMoneyEarned_uScript_GetCurrentMoneyEarned_162.SetParent(g);
		logic_uScript_CompareBlock_uScript_CompareBlock_165.SetParent(g);
		logic_uScript_RemoveOnScreenMessage_uScript_RemoveOnScreenMessage_166.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_168.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_172.SetParent(g);
		logic_uScript_MissionPromptBlock_Hide_uScript_MissionPromptBlock_Hide_176.SetParent(g);
		owner_Connection_8 = parentGameObject;
		owner_Connection_10 = parentGameObject;
		owner_Connection_22 = parentGameObject;
		owner_Connection_24 = parentGameObject;
		owner_Connection_26 = parentGameObject;
		owner_Connection_32 = parentGameObject;
		owner_Connection_33 = parentGameObject;
		owner_Connection_42 = parentGameObject;
		owner_Connection_169 = parentGameObject;
	}

	public void Awake()
	{
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_0.Awake();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_11.Awake();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_47.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_50.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_55.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_63.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_76.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_77.Awake();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_125.Awake();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_0.Save_Out += SubGraph_SaveLoadInt_Save_Out_0;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_0.Load_Out += SubGraph_SaveLoadInt_Load_Out_0;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_0.Restart_Out += SubGraph_SaveLoadInt_Restart_Out_0;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_11.Out += SubGraph_CompleteObjectiveStage_Out_11;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_21.Output1 += uScriptCon_ManualSwitch_Output1_21;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_21.Output2 += uScriptCon_ManualSwitch_Output2_21;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_21.Output3 += uScriptCon_ManualSwitch_Output3_21;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_21.Output4 += uScriptCon_ManualSwitch_Output4_21;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_21.Output5 += uScriptCon_ManualSwitch_Output5_21;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_21.Output6 += uScriptCon_ManualSwitch_Output6_21;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_21.Output7 += uScriptCon_ManualSwitch_Output7_21;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_21.Output8 += uScriptCon_ManualSwitch_Output8_21;
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_47.Out += SubGraph_LoadObjectiveStates_Out_47;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_50.Save_Out += SubGraph_SaveLoadBool_Save_Out_50;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_50.Load_Out += SubGraph_SaveLoadBool_Load_Out_50;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_50.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_50;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_55.Save_Out += SubGraph_SaveLoadBool_Save_Out_55;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_55.Load_Out += SubGraph_SaveLoadBool_Load_Out_55;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_55.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_55;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_63.Save_Out += SubGraph_SaveLoadBool_Save_Out_63;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_63.Load_Out += SubGraph_SaveLoadBool_Load_Out_63;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_63.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_63;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_76.Save_Out += SubGraph_SaveLoadBool_Save_Out_76;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_76.Load_Out += SubGraph_SaveLoadBool_Load_Out_76;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_76.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_76;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_77.Save_Out += SubGraph_SaveLoadBool_Save_Out_77;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_77.Load_Out += SubGraph_SaveLoadBool_Load_Out_77;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_77.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_77;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_125.Out += SubGraph_AddMessageWithPadSupport_Out_125;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_125.Shown += SubGraph_AddMessageWithPadSupport_Shown_125;
	}

	public void Start()
	{
		SyncUnityHooks();
		m_RegisteredForEvents = true;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_0.Start();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_11.Start();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_47.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_50.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_55.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_63.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_76.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_77.Start();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_125.Start();
	}

	public void OnEnable()
	{
		RegisterForUnityHooks();
		m_RegisteredForEvents = true;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_0.OnEnable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_11.OnEnable();
		logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_37.OnEnable();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_47.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_50.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_55.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_63.OnEnable();
		logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_64.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_76.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_77.OnEnable();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_125.OnEnable();
	}

	public void OnDisable()
	{
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_0.OnDisable();
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_2.OnDisable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_11.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_15.OnDisable();
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_43.OnDisable();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_47.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_50.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_55.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_63.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_68.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_76.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_77.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_82.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_86.OnDisable();
		logic_uScript_GetTankBlock_uScript_GetTankBlock_98.OnDisable();
		logic_uScript_GetTankBlock_uScript_GetTankBlock_117.OnDisable();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_125.OnDisable();
		UnregisterEventListeners();
		m_RegisteredForEvents = false;
	}

	public void Update()
	{
		SyncEventListeners();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_0.Update();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_11.Update();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_47.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_50.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_55.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_63.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_76.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_77.Update();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_125.Update();
	}

	public void OnDestroy()
	{
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_0.OnDestroy();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_11.OnDestroy();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_47.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_50.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_55.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_63.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_76.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_77.OnDestroy();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_125.OnDestroy();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_0.Save_Out -= SubGraph_SaveLoadInt_Save_Out_0;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_0.Load_Out -= SubGraph_SaveLoadInt_Load_Out_0;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_0.Restart_Out -= SubGraph_SaveLoadInt_Restart_Out_0;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_11.Out -= SubGraph_CompleteObjectiveStage_Out_11;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_21.Output1 -= uScriptCon_ManualSwitch_Output1_21;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_21.Output2 -= uScriptCon_ManualSwitch_Output2_21;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_21.Output3 -= uScriptCon_ManualSwitch_Output3_21;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_21.Output4 -= uScriptCon_ManualSwitch_Output4_21;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_21.Output5 -= uScriptCon_ManualSwitch_Output5_21;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_21.Output6 -= uScriptCon_ManualSwitch_Output6_21;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_21.Output7 -= uScriptCon_ManualSwitch_Output7_21;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_21.Output8 -= uScriptCon_ManualSwitch_Output8_21;
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_47.Out -= SubGraph_LoadObjectiveStates_Out_47;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_50.Save_Out -= SubGraph_SaveLoadBool_Save_Out_50;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_50.Load_Out -= SubGraph_SaveLoadBool_Load_Out_50;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_50.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_50;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_55.Save_Out -= SubGraph_SaveLoadBool_Save_Out_55;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_55.Load_Out -= SubGraph_SaveLoadBool_Load_Out_55;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_55.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_55;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_63.Save_Out -= SubGraph_SaveLoadBool_Save_Out_63;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_63.Load_Out -= SubGraph_SaveLoadBool_Load_Out_63;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_63.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_63;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_76.Save_Out -= SubGraph_SaveLoadBool_Save_Out_76;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_76.Load_Out -= SubGraph_SaveLoadBool_Load_Out_76;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_76.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_76;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_77.Save_Out -= SubGraph_SaveLoadBool_Save_Out_77;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_77.Load_Out -= SubGraph_SaveLoadBool_Load_Out_77;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_77.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_77;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_125.Out -= SubGraph_AddMessageWithPadSupport_Out_125;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_125.Shown -= SubGraph_AddMessageWithPadSupport_Shown_125;
	}

	private void Instance_OnUpdate_27(object o, EventArgs e)
	{
		Relay_OnUpdate_27();
	}

	private void Instance_OnSuspend_27(object o, EventArgs e)
	{
		Relay_OnSuspend_27();
	}

	private void Instance_OnResume_27(object o, EventArgs e)
	{
		Relay_OnResume_27();
	}

	private void Instance_SaveEvent_48(object o, EventArgs e)
	{
		Relay_SaveEvent_48();
	}

	private void Instance_LoadEvent_48(object o, EventArgs e)
	{
		Relay_LoadEvent_48();
	}

	private void Instance_RestartEvent_48(object o, EventArgs e)
	{
		Relay_RestartEvent_48();
	}

	private void Instance_ResponseEvent_164(object o, uScript_MissionPromptBlock_OnResult.PromptResultEventArgs e)
	{
		event_UnityEngine_GameObject_TankBlock_164 = e.TankBlock;
		event_UnityEngine_GameObject_Accepted_164 = e.Accepted;
		Relay_ResponseEvent_164();
	}

	private void SubGraph_SaveLoadInt_Save_Out_0(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_0 = e.integer;
		local_Stage_System_Int32 = logic_SubGraph_SaveLoadInt_integer_0;
		Relay_Save_Out_0();
	}

	private void SubGraph_SaveLoadInt_Load_Out_0(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_0 = e.integer;
		local_Stage_System_Int32 = logic_SubGraph_SaveLoadInt_integer_0;
		Relay_Load_Out_0();
	}

	private void SubGraph_SaveLoadInt_Restart_Out_0(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_0 = e.integer;
		local_Stage_System_Int32 = logic_SubGraph_SaveLoadInt_integer_0;
		Relay_Restart_Out_0();
	}

	private void SubGraph_CompleteObjectiveStage_Out_11(object o, SubGraph_CompleteObjectiveStage.LogicEventArgs e)
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_11 = e.objectiveStage;
		local_Stage_System_Int32 = logic_SubGraph_CompleteObjectiveStage_objectiveStage_11;
		Relay_Out_11();
	}

	private void uScriptCon_ManualSwitch_Output1_21(object o, EventArgs e)
	{
		Relay_Output1_21();
	}

	private void uScriptCon_ManualSwitch_Output2_21(object o, EventArgs e)
	{
		Relay_Output2_21();
	}

	private void uScriptCon_ManualSwitch_Output3_21(object o, EventArgs e)
	{
		Relay_Output3_21();
	}

	private void uScriptCon_ManualSwitch_Output4_21(object o, EventArgs e)
	{
		Relay_Output4_21();
	}

	private void uScriptCon_ManualSwitch_Output5_21(object o, EventArgs e)
	{
		Relay_Output5_21();
	}

	private void uScriptCon_ManualSwitch_Output6_21(object o, EventArgs e)
	{
		Relay_Output6_21();
	}

	private void uScriptCon_ManualSwitch_Output7_21(object o, EventArgs e)
	{
		Relay_Output7_21();
	}

	private void uScriptCon_ManualSwitch_Output8_21(object o, EventArgs e)
	{
		Relay_Output8_21();
	}

	private void SubGraph_LoadObjectiveStates_Out_47(object o, SubGraph_LoadObjectiveStates.LogicEventArgs e)
	{
		Relay_Out_47();
	}

	private void SubGraph_SaveLoadBool_Save_Out_50(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_50 = e.boolean;
		local_Init_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_50;
		Relay_Save_Out_50();
	}

	private void SubGraph_SaveLoadBool_Load_Out_50(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_50 = e.boolean;
		local_Init_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_50;
		Relay_Load_Out_50();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_50(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_50 = e.boolean;
		local_Init_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_50;
		Relay_Restart_Out_50();
	}

	private void SubGraph_SaveLoadBool_Save_Out_55(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_55 = e.boolean;
		local_LicensePurchased_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_55;
		Relay_Save_Out_55();
	}

	private void SubGraph_SaveLoadBool_Load_Out_55(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_55 = e.boolean;
		local_LicensePurchased_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_55;
		Relay_Load_Out_55();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_55(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_55 = e.boolean;
		local_LicensePurchased_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_55;
		Relay_Restart_Out_55();
	}

	private void SubGraph_SaveLoadBool_Save_Out_63(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_63 = e.boolean;
		local_msg01Shown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_63;
		Relay_Save_Out_63();
	}

	private void SubGraph_SaveLoadBool_Load_Out_63(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_63 = e.boolean;
		local_msg01Shown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_63;
		Relay_Load_Out_63();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_63(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_63 = e.boolean;
		local_msg01Shown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_63;
		Relay_Restart_Out_63();
	}

	private void SubGraph_SaveLoadBool_Save_Out_76(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_76 = e.boolean;
		local_msg03aShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_76;
		Relay_Save_Out_76();
	}

	private void SubGraph_SaveLoadBool_Load_Out_76(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_76 = e.boolean;
		local_msg03aShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_76;
		Relay_Load_Out_76();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_76(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_76 = e.boolean;
		local_msg03aShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_76;
		Relay_Restart_Out_76();
	}

	private void SubGraph_SaveLoadBool_Save_Out_77(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_77 = e.boolean;
		local_msg03bShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_77;
		Relay_Save_Out_77();
	}

	private void SubGraph_SaveLoadBool_Load_Out_77(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_77 = e.boolean;
		local_msg03bShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_77;
		Relay_Load_Out_77();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_77(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_77 = e.boolean;
		local_msg03bShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_77;
		Relay_Restart_Out_77();
	}

	private void SubGraph_AddMessageWithPadSupport_Out_125(object o, SubGraph_AddMessageWithPadSupport.LogicEventArgs e)
	{
		logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboardReturn_125 = e.messageMouseKeyboardReturn;
		logic_SubGraph_AddMessageWithPadSupport_messageControlPadReturn_125 = e.messageControlPadReturn;
		local_Msg02ClickScreen_ManOnScreenMessages_OnScreenMessage = logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboardReturn_125;
		local_Msg02ClickScreen_ManOnScreenMessages_OnScreenMessage = logic_SubGraph_AddMessageWithPadSupport_messageControlPadReturn_125;
		Relay_Out_125();
	}

	private void SubGraph_AddMessageWithPadSupport_Shown_125(object o, SubGraph_AddMessageWithPadSupport.LogicEventArgs e)
	{
		logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboardReturn_125 = e.messageMouseKeyboardReturn;
		logic_SubGraph_AddMessageWithPadSupport_messageControlPadReturn_125 = e.messageControlPadReturn;
		local_Msg02ClickScreen_ManOnScreenMessages_OnScreenMessage = logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboardReturn_125;
		local_Msg02ClickScreen_ManOnScreenMessages_OnScreenMessage = logic_SubGraph_AddMessageWithPadSupport_messageControlPadReturn_125;
		Relay_Shown_125();
	}

	private void Relay_Save_Out_0()
	{
		Relay_Save_50();
	}

	private void Relay_Load_Out_0()
	{
		Relay_Load_50();
	}

	private void Relay_Restart_Out_0()
	{
		Relay_Set_False_50();
	}

	private void Relay_Save_0()
	{
		logic_SubGraph_SaveLoadInt_integer_0 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_0 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_0.Save(logic_SubGraph_SaveLoadInt_restartValue_0, ref logic_SubGraph_SaveLoadInt_integer_0, logic_SubGraph_SaveLoadInt_intAsVariable_0, logic_SubGraph_SaveLoadInt_uniqueID_0);
	}

	private void Relay_Load_0()
	{
		logic_SubGraph_SaveLoadInt_integer_0 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_0 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_0.Load(logic_SubGraph_SaveLoadInt_restartValue_0, ref logic_SubGraph_SaveLoadInt_integer_0, logic_SubGraph_SaveLoadInt_intAsVariable_0, logic_SubGraph_SaveLoadInt_uniqueID_0);
	}

	private void Relay_Restart_0()
	{
		logic_SubGraph_SaveLoadInt_integer_0 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_0 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_0.Restart(logic_SubGraph_SaveLoadInt_restartValue_0, ref logic_SubGraph_SaveLoadInt_integer_0, logic_SubGraph_SaveLoadInt_intAsVariable_0, logic_SubGraph_SaveLoadInt_uniqueID_0);
	}

	private void Relay_In_2()
	{
		logic_uScript_SetTankInvulnerable_tank_2 = local_NPCTech_Tank;
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_2.In(logic_uScript_SetTankInvulnerable_invulnerable_2, logic_uScript_SetTankInvulnerable_tank_2);
		if (logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_2.Out)
		{
			Relay_In_37();
		}
	}

	private void Relay_In_6()
	{
		logic_uScript_SetEncounterTarget_owner_6 = owner_Connection_10;
		logic_uScript_SetEncounterTarget_visibleObject_6 = local_NPCTech_Tank;
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_6.In(logic_uScript_SetEncounterTarget_owner_6, logic_uScript_SetEncounterTarget_visibleObject_6);
		if (logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_6.Out)
		{
			Relay_In_117();
		}
	}

	private void Relay_Pause_7()
	{
		logic_uScript_PausePopulation_uScript_PausePopulation_7.Pause();
		if (logic_uScript_PausePopulation_uScript_PausePopulation_7.Out)
		{
			Relay_True_44();
		}
	}

	private void Relay_UnPause_7()
	{
		logic_uScript_PausePopulation_uScript_PausePopulation_7.UnPause();
		if (logic_uScript_PausePopulation_uScript_PausePopulation_7.Out)
		{
			Relay_True_44();
		}
	}

	private void Relay_Out_11()
	{
	}

	private void Relay_In_11()
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_11 = local_Stage_System_Int32;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_11.In(ref logic_SubGraph_CompleteObjectiveStage_objectiveStage_11, logic_SubGraph_CompleteObjectiveStage_isFinalObjective_11);
	}

	private void Relay_In_13()
	{
		logic_uScriptCon_CompareBool_Bool_13 = local_Init_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_13.In(logic_uScriptCon_CompareBool_Bool_13);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_13.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_13.False;
		if (num)
		{
			Relay_In_19();
		}
		if (flag)
		{
			Relay_True_17();
		}
	}

	private void Relay_AtIndex_14()
	{
		int num = 0;
		Array array = local_53_TankArray;
		if (logic_uScript_AccessListTech_techList_14.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_14, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_14, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_14.AtIndex(ref logic_uScript_AccessListTech_techList_14, logic_uScript_AccessListTech_index_14, out logic_uScript_AccessListTech_value_14);
		local_53_TankArray = logic_uScript_AccessListTech_techList_14;
		local_NPCTech_Tank = logic_uScript_AccessListTech_value_14;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_14.Out)
		{
			Relay_In_2();
		}
	}

	private void Relay_In_15()
	{
		logic_uScript_AddMessage_messageData_15 = msg05LicensePurchased;
		logic_uScript_AddMessage_speaker_15 = messageSpeaker;
		logic_uScript_AddMessage_Return_15 = logic_uScript_AddMessage_uScript_AddMessage_15.In(logic_uScript_AddMessage_messageData_15, logic_uScript_AddMessage_speaker_15);
		if (logic_uScript_AddMessage_uScript_AddMessage_15.Shown)
		{
			Relay_In_64();
		}
	}

	private void Relay_True_17()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_17.True(out logic_uScriptAct_SetBool_Target_17);
		local_Init_System_Boolean = logic_uScriptAct_SetBool_Target_17;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_17.Out)
		{
			Relay_InitialSpawn_38();
		}
	}

	private void Relay_False_17()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_17.False(out logic_uScriptAct_SetBool_Target_17);
		local_Init_System_Boolean = logic_uScriptAct_SetBool_Target_17;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_17.Out)
		{
			Relay_InitialSpawn_38();
		}
	}

	private void Relay_In_19()
	{
		logic_uScript_RemoveScenery_ownerNode_19 = owner_Connection_26;
		logic_uScript_RemoveScenery_positionName_19 = NPCPosition;
		logic_uScript_RemoveScenery_radius_19 = clearSceneryRadius;
		logic_uScript_RemoveScenery_uScript_RemoveScenery_19.In(logic_uScript_RemoveScenery_ownerNode_19, logic_uScript_RemoveScenery_positionName_19, logic_uScript_RemoveScenery_radius_19, logic_uScript_RemoveScenery_preventChunksSpawning_19);
		if (logic_uScript_RemoveScenery_uScript_RemoveScenery_19.Out)
		{
			Relay_In_34();
		}
	}

	private void Relay_Output1_21()
	{
		Relay_In_11();
	}

	private void Relay_Output2_21()
	{
		Relay_In_58();
	}

	private void Relay_Output3_21()
	{
	}

	private void Relay_Output4_21()
	{
	}

	private void Relay_Output5_21()
	{
	}

	private void Relay_Output6_21()
	{
	}

	private void Relay_Output7_21()
	{
	}

	private void Relay_Output8_21()
	{
	}

	private void Relay_In_21()
	{
		logic_uScriptCon_ManualSwitch_CurrentOutput_21 = local_Stage_System_Int32;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_21.In(logic_uScriptCon_ManualSwitch_CurrentOutput_21);
	}

	private void Relay_True_23()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_23.True(out logic_uScriptAct_SetBool_Target_23);
		local_NearNPC_System_Boolean = logic_uScriptAct_SetBool_Target_23;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_23.Out)
		{
			Relay_False_122();
		}
	}

	private void Relay_False_23()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_23.False(out logic_uScriptAct_SetBool_Target_23);
		local_NearNPC_System_Boolean = logic_uScriptAct_SetBool_Target_23;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_23.Out)
		{
			Relay_False_122();
		}
	}

	private void Relay_OnUpdate_27()
	{
		Relay_In_13();
	}

	private void Relay_OnSuspend_27()
	{
	}

	private void Relay_OnResume_27()
	{
	}

	private void Relay_In_30()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_30.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_30.Out)
		{
			Relay_In_37();
		}
	}

	private void Relay_In_34()
	{
		int num = 0;
		Array nPCSpawnData = NPCSpawnData;
		if (logic_uScript_GetAndCheckTechs_techData_34.Length != num + nPCSpawnData.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_34, num + nPCSpawnData.Length);
		}
		Array.Copy(nPCSpawnData, 0, logic_uScript_GetAndCheckTechs_techData_34, num, nPCSpawnData.Length);
		num += nPCSpawnData.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_34 = owner_Connection_22;
		int num2 = 0;
		Array array = local_53_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_34.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_34, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techs_34, num2, array.Length);
		num2 += array.Length;
		logic_uScript_GetAndCheckTechs_Return_34 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_34.In(logic_uScript_GetAndCheckTechs_techData_34, logic_uScript_GetAndCheckTechs_ownerNode_34, ref logic_uScript_GetAndCheckTechs_techs_34);
		local_53_TankArray = logic_uScript_GetAndCheckTechs_techs_34;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_34.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_34.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_34.AllDead;
		if (allAlive)
		{
			Relay_AtIndex_14();
		}
		if (someAlive)
		{
			Relay_AtIndex_14();
		}
		if (allDead)
		{
			Relay_In_30();
		}
	}

	private void Relay_Succeed_35()
	{
		logic_uScript_FinishEncounter_owner_35 = owner_Connection_42;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_35.Succeed(logic_uScript_FinishEncounter_owner_35);
	}

	private void Relay_Fail_35()
	{
		logic_uScript_FinishEncounter_owner_35 = owner_Connection_42;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_35.Fail(logic_uScript_FinishEncounter_owner_35);
	}

	private void Relay_In_37()
	{
		logic_uScript_DirectEnemiesOutOfEncounter_ownerNode_37 = owner_Connection_33;
		logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_37.In(logic_uScript_DirectEnemiesOutOfEncounter_ownerNode_37);
		if (logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_37.Out)
		{
			Relay_In_6();
		}
	}

	private void Relay_InitialSpawn_38()
	{
		int num = 0;
		Array nPCSpawnData = NPCSpawnData;
		if (logic_uScript_SpawnTechsFromData_spawnData_38.Length != num + nPCSpawnData.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_38, num + nPCSpawnData.Length);
		}
		Array.Copy(nPCSpawnData, 0, logic_uScript_SpawnTechsFromData_spawnData_38, num, nPCSpawnData.Length);
		num += nPCSpawnData.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_38 = owner_Connection_24;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_38.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_38, logic_uScript_SpawnTechsFromData_ownerNode_38, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_38, logic_uScript_SpawnTechsFromData_allowResurrection_38);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_38.Out)
		{
			Relay_In_19();
		}
	}

	private void Relay_In_40()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_40 = messageTag;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_40.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_40, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_40);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_40.Out)
		{
			Relay_In_113();
		}
	}

	private void Relay_In_43()
	{
		logic_uScript_IsPlayerInRangeOfTech_tech_43 = local_NPCTech_Tank;
		logic_uScript_IsPlayerInRangeOfTech_range_43 = distNPCFound;
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_43.In(logic_uScript_IsPlayerInRangeOfTech_tech_43, logic_uScript_IsPlayerInRangeOfTech_range_43, logic_uScript_IsPlayerInRangeOfTech_techs_43);
		bool inRange = logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_43.InRange;
		bool outOfRange = logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_43.OutOfRange;
		if (inRange)
		{
			Relay_Pause_7();
		}
		if (outOfRange)
		{
			Relay_UnPause_54();
		}
	}

	private void Relay_True_44()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_44.True(out logic_uScriptAct_SetBool_Target_44);
		local_NearNPC_System_Boolean = logic_uScriptAct_SetBool_Target_44;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_44.Out)
		{
			Relay_In_21();
		}
	}

	private void Relay_False_44()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_44.False(out logic_uScriptAct_SetBool_Target_44);
		local_NearNPC_System_Boolean = logic_uScriptAct_SetBool_Target_44;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_44.Out)
		{
			Relay_In_21();
		}
	}

	private void Relay_Out_47()
	{
	}

	private void Relay_In_47()
	{
		logic_SubGraph_LoadObjectiveStates_currentObjective_47 = local_Stage_System_Int32;
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_47.In(logic_SubGraph_LoadObjectiveStates_currentObjective_47);
	}

	private void Relay_SaveEvent_48()
	{
		Relay_Save_0();
	}

	private void Relay_LoadEvent_48()
	{
		Relay_Load_0();
	}

	private void Relay_RestartEvent_48()
	{
		Relay_Restart_0();
	}

	private void Relay_Save_Out_50()
	{
		Relay_Save_63();
	}

	private void Relay_Load_Out_50()
	{
		Relay_Load_63();
	}

	private void Relay_Restart_Out_50()
	{
		Relay_Set_False_63();
	}

	private void Relay_Save_50()
	{
		logic_SubGraph_SaveLoadBool_boolean_50 = local_Init_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_50 = local_Init_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_50.Save(ref logic_SubGraph_SaveLoadBool_boolean_50, logic_SubGraph_SaveLoadBool_boolAsVariable_50, logic_SubGraph_SaveLoadBool_uniqueID_50);
	}

	private void Relay_Load_50()
	{
		logic_SubGraph_SaveLoadBool_boolean_50 = local_Init_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_50 = local_Init_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_50.Load(ref logic_SubGraph_SaveLoadBool_boolean_50, logic_SubGraph_SaveLoadBool_boolAsVariable_50, logic_SubGraph_SaveLoadBool_uniqueID_50);
	}

	private void Relay_Set_True_50()
	{
		logic_SubGraph_SaveLoadBool_boolean_50 = local_Init_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_50 = local_Init_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_50.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_50, logic_SubGraph_SaveLoadBool_boolAsVariable_50, logic_SubGraph_SaveLoadBool_uniqueID_50);
	}

	private void Relay_Set_False_50()
	{
		logic_SubGraph_SaveLoadBool_boolean_50 = local_Init_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_50 = local_Init_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_50.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_50, logic_SubGraph_SaveLoadBool_boolAsVariable_50, logic_SubGraph_SaveLoadBool_uniqueID_50);
	}

	private void Relay_Pause_54()
	{
		logic_uScript_PausePopulation_uScript_PausePopulation_54.Pause();
		if (logic_uScript_PausePopulation_uScript_PausePopulation_54.Out)
		{
			Relay_False_119();
		}
	}

	private void Relay_UnPause_54()
	{
		logic_uScript_PausePopulation_uScript_PausePopulation_54.UnPause();
		if (logic_uScript_PausePopulation_uScript_PausePopulation_54.Out)
		{
			Relay_False_119();
		}
	}

	private void Relay_Save_Out_55()
	{
	}

	private void Relay_Load_Out_55()
	{
		Relay_In_47();
	}

	private void Relay_Restart_Out_55()
	{
		Relay_False_23();
	}

	private void Relay_Save_55()
	{
		logic_SubGraph_SaveLoadBool_boolean_55 = local_LicensePurchased_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_55 = local_LicensePurchased_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_55.Save(ref logic_SubGraph_SaveLoadBool_boolean_55, logic_SubGraph_SaveLoadBool_boolAsVariable_55, logic_SubGraph_SaveLoadBool_uniqueID_55);
	}

	private void Relay_Load_55()
	{
		logic_SubGraph_SaveLoadBool_boolean_55 = local_LicensePurchased_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_55 = local_LicensePurchased_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_55.Load(ref logic_SubGraph_SaveLoadBool_boolean_55, logic_SubGraph_SaveLoadBool_boolAsVariable_55, logic_SubGraph_SaveLoadBool_uniqueID_55);
	}

	private void Relay_Set_True_55()
	{
		logic_SubGraph_SaveLoadBool_boolean_55 = local_LicensePurchased_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_55 = local_LicensePurchased_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_55.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_55, logic_SubGraph_SaveLoadBool_boolAsVariable_55, logic_SubGraph_SaveLoadBool_uniqueID_55);
	}

	private void Relay_Set_False_55()
	{
		logic_SubGraph_SaveLoadBool_boolean_55 = local_LicensePurchased_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_55 = local_LicensePurchased_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_55.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_55, logic_SubGraph_SaveLoadBool_boolAsVariable_55, logic_SubGraph_SaveLoadBool_uniqueID_55);
	}

	private void Relay_In_58()
	{
		logic_uScriptCon_CompareBool_Bool_58 = local_LicensePurchased_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_58.In(logic_uScriptCon_CompareBool_Bool_58);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_58.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_58.False;
		if (num)
		{
			Relay_In_176();
		}
		if (flag)
		{
			Relay_In_97();
		}
	}

	private void Relay_Save_Out_63()
	{
		Relay_Save_76();
	}

	private void Relay_Load_Out_63()
	{
		Relay_Load_76();
	}

	private void Relay_Restart_Out_63()
	{
		Relay_Set_False_76();
	}

	private void Relay_Save_63()
	{
		logic_SubGraph_SaveLoadBool_boolean_63 = local_msg01Shown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_63 = local_msg01Shown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_63.Save(ref logic_SubGraph_SaveLoadBool_boolean_63, logic_SubGraph_SaveLoadBool_boolAsVariable_63, logic_SubGraph_SaveLoadBool_uniqueID_63);
	}

	private void Relay_Load_63()
	{
		logic_SubGraph_SaveLoadBool_boolean_63 = local_msg01Shown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_63 = local_msg01Shown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_63.Load(ref logic_SubGraph_SaveLoadBool_boolean_63, logic_SubGraph_SaveLoadBool_boolAsVariable_63, logic_SubGraph_SaveLoadBool_uniqueID_63);
	}

	private void Relay_Set_True_63()
	{
		logic_SubGraph_SaveLoadBool_boolean_63 = local_msg01Shown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_63 = local_msg01Shown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_63.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_63, logic_SubGraph_SaveLoadBool_boolAsVariable_63, logic_SubGraph_SaveLoadBool_uniqueID_63);
	}

	private void Relay_Set_False_63()
	{
		logic_SubGraph_SaveLoadBool_boolean_63 = local_msg01Shown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_63 = local_msg01Shown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_63.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_63, logic_SubGraph_SaveLoadBool_boolAsVariable_63, logic_SubGraph_SaveLoadBool_uniqueID_63);
	}

	private void Relay_In_64()
	{
		logic_uScript_FlyTechUpAndAway_tech_64 = local_NPCTech_Tank;
		logic_uScript_FlyTechUpAndAway_aiTree_64 = NPCFlyAwayAI;
		logic_uScript_FlyTechUpAndAway_removalParticles_64 = NPCDespawnParticleEffect;
		logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_64.In(logic_uScript_FlyTechUpAndAway_tech_64, logic_uScript_FlyTechUpAndAway_maxLifetime_64, logic_uScript_FlyTechUpAndAway_targetHeight_64, logic_uScript_FlyTechUpAndAway_aiTree_64, logic_uScript_FlyTechUpAndAway_removalParticles_64);
		if (logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_64.Out)
		{
			Relay_Succeed_35();
		}
	}

	private void Relay_In_68()
	{
		logic_uScript_AddMessage_messageData_68 = msg01Intro;
		logic_uScript_AddMessage_speaker_68 = messageSpeaker;
		logic_uScript_AddMessage_Return_68 = logic_uScript_AddMessage_uScript_AddMessage_68.In(logic_uScript_AddMessage_messageData_68, logic_uScript_AddMessage_speaker_68);
		if (logic_uScript_AddMessage_uScript_AddMessage_68.Shown)
		{
			Relay_True_74();
		}
	}

	private void Relay_In_72()
	{
		logic_uScriptCon_CompareBool_Bool_72 = local_msg01Shown_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_72.In(logic_uScriptCon_CompareBool_Bool_72);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_72.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_72.False;
		if (num)
		{
			Relay_In_98();
		}
		if (flag)
		{
			Relay_In_68();
		}
	}

	private void Relay_True_74()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_74.True(out logic_uScriptAct_SetBool_Target_74);
		local_msg01Shown_System_Boolean = logic_uScriptAct_SetBool_Target_74;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_74.Out)
		{
			Relay_In_98();
		}
	}

	private void Relay_False_74()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_74.False(out logic_uScriptAct_SetBool_Target_74);
		local_msg01Shown_System_Boolean = logic_uScriptAct_SetBool_Target_74;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_74.Out)
		{
			Relay_In_98();
		}
	}

	private void Relay_Save_Out_76()
	{
		Relay_Save_77();
	}

	private void Relay_Load_Out_76()
	{
		Relay_Load_77();
	}

	private void Relay_Restart_Out_76()
	{
		Relay_Set_False_77();
	}

	private void Relay_Save_76()
	{
		logic_SubGraph_SaveLoadBool_boolean_76 = local_msg03aShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_76 = local_msg03aShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_76.Save(ref logic_SubGraph_SaveLoadBool_boolean_76, logic_SubGraph_SaveLoadBool_boolAsVariable_76, logic_SubGraph_SaveLoadBool_uniqueID_76);
	}

	private void Relay_Load_76()
	{
		logic_SubGraph_SaveLoadBool_boolean_76 = local_msg03aShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_76 = local_msg03aShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_76.Load(ref logic_SubGraph_SaveLoadBool_boolean_76, logic_SubGraph_SaveLoadBool_boolAsVariable_76, logic_SubGraph_SaveLoadBool_uniqueID_76);
	}

	private void Relay_Set_True_76()
	{
		logic_SubGraph_SaveLoadBool_boolean_76 = local_msg03aShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_76 = local_msg03aShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_76.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_76, logic_SubGraph_SaveLoadBool_boolAsVariable_76, logic_SubGraph_SaveLoadBool_uniqueID_76);
	}

	private void Relay_Set_False_76()
	{
		logic_SubGraph_SaveLoadBool_boolean_76 = local_msg03aShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_76 = local_msg03aShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_76.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_76, logic_SubGraph_SaveLoadBool_boolAsVariable_76, logic_SubGraph_SaveLoadBool_uniqueID_76);
	}

	private void Relay_Save_Out_77()
	{
		Relay_Save_55();
	}

	private void Relay_Load_Out_77()
	{
		Relay_Load_55();
	}

	private void Relay_Restart_Out_77()
	{
		Relay_Set_False_55();
	}

	private void Relay_Save_77()
	{
		logic_SubGraph_SaveLoadBool_boolean_77 = local_msg03bShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_77 = local_msg03bShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_77.Save(ref logic_SubGraph_SaveLoadBool_boolean_77, logic_SubGraph_SaveLoadBool_boolAsVariable_77, logic_SubGraph_SaveLoadBool_uniqueID_77);
	}

	private void Relay_Load_77()
	{
		logic_SubGraph_SaveLoadBool_boolean_77 = local_msg03bShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_77 = local_msg03bShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_77.Load(ref logic_SubGraph_SaveLoadBool_boolean_77, logic_SubGraph_SaveLoadBool_boolAsVariable_77, logic_SubGraph_SaveLoadBool_uniqueID_77);
	}

	private void Relay_Set_True_77()
	{
		logic_SubGraph_SaveLoadBool_boolean_77 = local_msg03bShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_77 = local_msg03bShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_77.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_77, logic_SubGraph_SaveLoadBool_boolAsVariable_77, logic_SubGraph_SaveLoadBool_uniqueID_77);
	}

	private void Relay_Set_False_77()
	{
		logic_SubGraph_SaveLoadBool_boolean_77 = local_msg03bShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_77 = local_msg03bShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_77.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_77, logic_SubGraph_SaveLoadBool_boolAsVariable_77, logic_SubGraph_SaveLoadBool_uniqueID_77);
	}

	private void Relay_In_79()
	{
		logic_uScriptCon_CompareBool_Bool_79 = local_HasEnoughMoney_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_79.In(logic_uScriptCon_CompareBool_Bool_79);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_79.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_79.False;
		if (num)
		{
			Relay_In_93();
		}
		if (flag)
		{
			Relay_In_107();
		}
	}

	private void Relay_In_82()
	{
		logic_uScript_AddMessage_messageData_82 = msg03bNotEnoughMoney;
		logic_uScript_AddMessage_speaker_82 = messageSpeaker;
		logic_uScript_AddMessage_Return_82 = logic_uScript_AddMessage_uScript_AddMessage_82.In(logic_uScript_AddMessage_messageData_82, logic_uScript_AddMessage_speaker_82);
		if (logic_uScript_AddMessage_uScript_AddMessage_82.Shown)
		{
			Relay_True_109();
		}
	}

	private void Relay_In_86()
	{
		logic_uScript_AddMessage_messageData_86 = msg03aPurchaseDeclined;
		logic_uScript_AddMessage_speaker_86 = messageSpeaker;
		logic_uScript_AddMessage_Return_86 = logic_uScript_AddMessage_uScript_AddMessage_86.In(logic_uScript_AddMessage_messageData_86, logic_uScript_AddMessage_speaker_86);
		if (logic_uScript_AddMessage_uScript_AddMessage_86.Shown)
		{
			Relay_True_105();
		}
	}

	private void Relay_In_89()
	{
		logic_uScript_AddMoney_amount_89 = local_91_System_Int32;
		logic_uScript_AddMoney_uScript_AddMoney_89.In(logic_uScript_AddMoney_amount_89);
		if (logic_uScript_AddMoney_uScript_AddMoney_89.Out)
		{
			Relay_True_92();
		}
	}

	private void Relay_True_92()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_92.True(out logic_uScriptAct_SetBool_Target_92);
		local_LicensePurchased_System_Boolean = logic_uScriptAct_SetBool_Target_92;
	}

	private void Relay_False_92()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_92.False(out logic_uScriptAct_SetBool_Target_92);
		local_LicensePurchased_System_Boolean = logic_uScriptAct_SetBool_Target_92;
	}

	private void Relay_In_93()
	{
		logic_uScriptAct_MultiplyInt_v2_A_93 = LicenseCost;
		logic_uScriptAct_MultiplyInt_v2_uScriptAct_MultiplyInt_v2_93.In(logic_uScriptAct_MultiplyInt_v2_A_93, logic_uScriptAct_MultiplyInt_v2_B_93, out logic_uScriptAct_MultiplyInt_v2_IntResult_93, out logic_uScriptAct_MultiplyInt_v2_FloatResult_93);
		local_91_System_Int32 = logic_uScriptAct_MultiplyInt_v2_IntResult_93;
		if (logic_uScriptAct_MultiplyInt_v2_uScriptAct_MultiplyInt_v2_93.Out)
		{
			Relay_In_89();
		}
	}

	private void Relay_True_94()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_94.True(out logic_uScriptAct_SetBool_Target_94);
		local_WaitingOnPrompt_System_Boolean = logic_uScriptAct_SetBool_Target_94;
	}

	private void Relay_False_94()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_94.False(out logic_uScriptAct_SetBool_Target_94);
		local_WaitingOnPrompt_System_Boolean = logic_uScriptAct_SetBool_Target_94;
	}

	private void Relay_In_97()
	{
		logic_uScriptCon_CompareBool_Bool_97 = local_WaitingOnPrompt_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_97.In(logic_uScriptCon_CompareBool_Bool_97);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_97.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_97.False;
		if (num)
		{
			Relay_In_160();
		}
		if (flag)
		{
			Relay_In_72();
		}
	}

	private void Relay_In_98()
	{
		logic_uScript_GetTankBlock_tank_98 = local_NPCTech_Tank;
		logic_uScript_GetTankBlock_blockType_98 = interactableBlockType;
		logic_uScript_GetTankBlock_Return_98 = logic_uScript_GetTankBlock_uScript_GetTankBlock_98.In(logic_uScript_GetTankBlock_tank_98, logic_uScript_GetTankBlock_blockType_98);
		local_TerminalBlock_TankBlock = logic_uScript_GetTankBlock_Return_98;
		if (logic_uScript_GetTankBlock_uScript_GetTankBlock_98.Returned)
		{
			Relay_In_125();
		}
	}

	private void Relay_In_103()
	{
		logic_uScriptCon_CompareBool_Bool_103 = local_msg03aShown_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_103.In(logic_uScriptCon_CompareBool_Bool_103);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_103.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_103.False;
		if (num)
		{
			Relay_In_111();
		}
		if (flag)
		{
			Relay_In_86();
		}
	}

	private void Relay_True_105()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_105.True(out logic_uScriptAct_SetBool_Target_105);
		local_msg03aShown_System_Boolean = logic_uScriptAct_SetBool_Target_105;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_105.Out)
		{
			Relay_In_111();
		}
	}

	private void Relay_False_105()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_105.False(out logic_uScriptAct_SetBool_Target_105);
		local_msg03aShown_System_Boolean = logic_uScriptAct_SetBool_Target_105;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_105.Out)
		{
			Relay_In_111();
		}
	}

	private void Relay_In_107()
	{
		logic_uScriptCon_CompareBool_Bool_107 = local_msg03bShown_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_107.In(logic_uScriptCon_CompareBool_Bool_107);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_107.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_107.False;
		if (num)
		{
			Relay_In_110();
		}
		if (flag)
		{
			Relay_In_82();
		}
	}

	private void Relay_True_109()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_109.True(out logic_uScriptAct_SetBool_Target_109);
		local_msg03bShown_System_Boolean = logic_uScriptAct_SetBool_Target_109;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_109.Out)
		{
			Relay_In_110();
		}
	}

	private void Relay_False_109()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_109.False(out logic_uScriptAct_SetBool_Target_109);
		local_msg03bShown_System_Boolean = logic_uScriptAct_SetBool_Target_109;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_109.Out)
		{
			Relay_In_110();
		}
	}

	private void Relay_In_110()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_110.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_110.Out)
		{
			Relay_False_94();
		}
	}

	private void Relay_In_111()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_111.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_111.Out)
		{
			Relay_False_94();
		}
	}

	private void Relay_In_113()
	{
		logic_uScript_HideArrow_uScript_HideArrow_113.In();
		if (logic_uScript_HideArrow_uScript_HideArrow_113.Out)
		{
			Relay_In_114();
		}
	}

	private void Relay_In_114()
	{
		logic_uScript_EnableGlow_targetObject_114 = local_TerminalBlock_TankBlock;
		logic_uScript_EnableGlow_uScript_EnableGlow_114.In(logic_uScript_EnableGlow_targetObject_114, logic_uScript_EnableGlow_enable_114);
	}

	private void Relay_In_117()
	{
		logic_uScript_GetTankBlock_tank_117 = local_NPCTech_Tank;
		logic_uScript_GetTankBlock_blockType_117 = interactableBlockType;
		logic_uScript_GetTankBlock_Return_117 = logic_uScript_GetTankBlock_uScript_GetTankBlock_117.In(logic_uScript_GetTankBlock_tank_117, logic_uScript_GetTankBlock_blockType_117);
		local_TerminalBlock_TankBlock = logic_uScript_GetTankBlock_Return_117;
		if (logic_uScript_GetTankBlock_uScript_GetTankBlock_117.Out)
		{
			Relay_In_43();
		}
	}

	private void Relay_True_119()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_119.True(out logic_uScriptAct_SetBool_Target_119);
		local_NearNPC_System_Boolean = logic_uScriptAct_SetBool_Target_119;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_119.Out)
		{
			Relay_In_40();
		}
	}

	private void Relay_False_119()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_119.False(out logic_uScriptAct_SetBool_Target_119);
		local_NearNPC_System_Boolean = logic_uScriptAct_SetBool_Target_119;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_119.Out)
		{
			Relay_In_40();
		}
	}

	private void Relay_True_122()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_122.True(out logic_uScriptAct_SetBool_Target_122);
		local_WaitingOnPrompt_System_Boolean = logic_uScriptAct_SetBool_Target_122;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_122.Out)
		{
			Relay_False_123();
		}
	}

	private void Relay_False_122()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_122.False(out logic_uScriptAct_SetBool_Target_122);
		local_WaitingOnPrompt_System_Boolean = logic_uScriptAct_SetBool_Target_122;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_122.Out)
		{
			Relay_False_123();
		}
	}

	private void Relay_True_123()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_123.True(out logic_uScriptAct_SetBool_Target_123);
		local_HasEnoughMoney_System_Boolean = logic_uScriptAct_SetBool_Target_123;
	}

	private void Relay_False_123()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_123.False(out logic_uScriptAct_SetBool_Target_123);
		local_HasEnoughMoney_System_Boolean = logic_uScriptAct_SetBool_Target_123;
	}

	private void Relay_Out_125()
	{
		Relay_In_132();
	}

	private void Relay_Shown_125()
	{
	}

	private void Relay_In_125()
	{
		logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboard_125 = msg02ClickScreen;
		logic_SubGraph_AddMessageWithPadSupport_messageControlPad_125 = msg02ClickScreen_Pad;
		logic_SubGraph_AddMessageWithPadSupport_speaker_125 = messageSpeaker;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_125.In(logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboard_125, logic_SubGraph_AddMessageWithPadSupport_messageControlPad_125, logic_SubGraph_AddMessageWithPadSupport_speaker_125);
	}

	private void Relay_In_130()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_130.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_130.Out)
		{
			Relay_In_149();
		}
	}

	private void Relay_In_132()
	{
		logic_uScriptCon_CompareBool_Bool_132 = _DEBUGIgnoreMoneyCheck;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_132.In(logic_uScriptCon_CompareBool_Bool_132);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_132.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_132.False;
		if (num)
		{
			Relay_In_154();
		}
		if (flag)
		{
			Relay_In_162();
		}
	}

	private void Relay_In_133()
	{
		logic_uScript_MissionPromptBlock_Show_bodyText_133 = msgPromptText;
		logic_uScript_MissionPromptBlock_Show_acceptButtonText_133 = msgPromptNoMoney;
		logic_uScript_MissionPromptBlock_Show_targetBlock_133 = local_TerminalBlock_TankBlock;
		logic_uScript_MissionPromptBlock_Show_uScript_MissionPromptBlock_Show_133.In(logic_uScript_MissionPromptBlock_Show_bodyText_133, logic_uScript_MissionPromptBlock_Show_acceptButtonText_133, logic_uScript_MissionPromptBlock_Show_rejectButtonText_133, logic_uScript_MissionPromptBlock_Show_targetBlock_133, logic_uScript_MissionPromptBlock_Show_highlightBlock_133, logic_uScript_MissionPromptBlock_Show_singleUse_133);
		if (logic_uScript_MissionPromptBlock_Show_uScript_MissionPromptBlock_Show_133.Out)
		{
			Relay_False_161();
		}
	}

	private void Relay_In_135()
	{
		logic_uScript_GetCurrentMoneyEarned_Return_135 = logic_uScript_GetCurrentMoneyEarned_uScript_GetCurrentMoneyEarned_135.In();
		local_CurrentMoney_System_Int32 = logic_uScript_GetCurrentMoneyEarned_Return_135;
		if (logic_uScript_GetCurrentMoneyEarned_uScript_GetCurrentMoneyEarned_135.Out)
		{
			Relay_In_157();
		}
	}

	private void Relay_In_138()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_138.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_138.Out)
		{
			Relay_In_158();
		}
	}

	private void Relay_In_140()
	{
		logic_uScriptCon_CompareBool_Bool_140 = local_HasEnoughMoney_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_140.In(logic_uScriptCon_CompareBool_Bool_140);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_140.True)
		{
			Relay_In_133();
		}
	}

	private void Relay_True_142()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_142.True(out logic_uScriptAct_SetBool_Target_142);
		local_WaitingOnPrompt_System_Boolean = logic_uScriptAct_SetBool_Target_142;
	}

	private void Relay_False_142()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_142.False(out logic_uScriptAct_SetBool_Target_142);
		local_WaitingOnPrompt_System_Boolean = logic_uScriptAct_SetBool_Target_142;
	}

	private void Relay_In_145()
	{
		logic_uScriptCon_CompareInt_A_145 = local_CurrentMoney_System_Int32;
		logic_uScriptCon_CompareInt_B_145 = LicenseCost;
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_145.In(logic_uScriptCon_CompareInt_A_145, logic_uScriptCon_CompareInt_B_145);
		bool greaterThanOrEqualTo = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_145.GreaterThanOrEqualTo;
		bool lessThan = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_145.LessThan;
		if (greaterThanOrEqualTo)
		{
			Relay_In_138();
		}
		if (lessThan)
		{
			Relay_In_133();
		}
	}

	private void Relay_In_149()
	{
		logic_uScriptCon_CompareBool_Bool_149 = local_HasEnoughMoney_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_149.In(logic_uScriptCon_CompareBool_Bool_149);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_149.False)
		{
			Relay_In_158();
		}
	}

	private void Relay_In_154()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_154.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_154.Out)
		{
			Relay_In_138();
		}
	}

	private void Relay_In_157()
	{
		logic_uScriptCon_CompareInt_A_157 = local_CurrentMoney_System_Int32;
		logic_uScriptCon_CompareInt_B_157 = LicenseCost;
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_157.In(logic_uScriptCon_CompareInt_A_157, logic_uScriptCon_CompareInt_B_157);
		bool greaterThanOrEqualTo = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_157.GreaterThanOrEqualTo;
		bool lessThan = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_157.LessThan;
		if (greaterThanOrEqualTo)
		{
			Relay_In_149();
		}
		if (lessThan)
		{
			Relay_In_140();
		}
	}

	private void Relay_In_158()
	{
		logic_uScript_MissionPromptBlock_Show_bodyText_158 = msgPromptText;
		logic_uScript_MissionPromptBlock_Show_acceptButtonText_158 = msgPromptAccept;
		logic_uScript_MissionPromptBlock_Show_rejectButtonText_158 = msgPromptDecline;
		logic_uScript_MissionPromptBlock_Show_targetBlock_158 = local_TerminalBlock_TankBlock;
		logic_uScript_MissionPromptBlock_Show_uScript_MissionPromptBlock_Show_158.In(logic_uScript_MissionPromptBlock_Show_bodyText_158, logic_uScript_MissionPromptBlock_Show_acceptButtonText_158, logic_uScript_MissionPromptBlock_Show_rejectButtonText_158, logic_uScript_MissionPromptBlock_Show_targetBlock_158, logic_uScript_MissionPromptBlock_Show_highlightBlock_158, logic_uScript_MissionPromptBlock_Show_singleUse_158);
		if (logic_uScript_MissionPromptBlock_Show_uScript_MissionPromptBlock_Show_158.Out)
		{
			Relay_True_161();
		}
	}

	private void Relay_In_160()
	{
		logic_uScriptCon_CompareBool_Bool_160 = _DEBUGIgnoreMoneyCheck;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_160.In(logic_uScriptCon_CompareBool_Bool_160);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_160.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_160.False;
		if (num)
		{
			Relay_In_130();
		}
		if (flag)
		{
			Relay_In_135();
		}
	}

	private void Relay_True_161()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_161.True(out logic_uScriptAct_SetBool_Target_161);
		local_HasEnoughMoney_System_Boolean = logic_uScriptAct_SetBool_Target_161;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_161.Out)
		{
			Relay_True_142();
		}
	}

	private void Relay_False_161()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_161.False(out logic_uScriptAct_SetBool_Target_161);
		local_HasEnoughMoney_System_Boolean = logic_uScriptAct_SetBool_Target_161;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_161.Out)
		{
			Relay_True_142();
		}
	}

	private void Relay_In_162()
	{
		logic_uScript_GetCurrentMoneyEarned_Return_162 = logic_uScript_GetCurrentMoneyEarned_uScript_GetCurrentMoneyEarned_162.In();
		local_CurrentMoney_System_Int32 = logic_uScript_GetCurrentMoneyEarned_Return_162;
		if (logic_uScript_GetCurrentMoneyEarned_uScript_GetCurrentMoneyEarned_162.Out)
		{
			Relay_In_145();
		}
	}

	private void Relay_ResponseEvent_164()
	{
		local_175_TankBlock = event_UnityEngine_GameObject_TankBlock_164;
		local_170_System_Boolean = event_UnityEngine_GameObject_Accepted_164;
		Relay_In_165();
	}

	private void Relay_In_165()
	{
		logic_uScript_CompareBlock_A_165 = local_175_TankBlock;
		logic_uScript_CompareBlock_B_165 = local_TerminalBlock_TankBlock;
		logic_uScript_CompareBlock_uScript_CompareBlock_165.In(logic_uScript_CompareBlock_A_165, logic_uScript_CompareBlock_B_165);
		if (logic_uScript_CompareBlock_uScript_CompareBlock_165.EqualTo)
		{
			Relay_In_172();
		}
	}

	private void Relay_In_166()
	{
		logic_uScript_RemoveOnScreenMessage_onScreenMessage_166 = local_Msg02ClickScreen_ManOnScreenMessages_OnScreenMessage;
		logic_uScript_RemoveOnScreenMessage_uScript_RemoveOnScreenMessage_166.In(logic_uScript_RemoveOnScreenMessage_onScreenMessage_166, logic_uScript_RemoveOnScreenMessage_instant_166);
		if (logic_uScript_RemoveOnScreenMessage_uScript_RemoveOnScreenMessage_166.Out)
		{
			Relay_In_168();
		}
	}

	private void Relay_In_168()
	{
		logic_uScriptCon_CompareBool_Bool_168 = local_170_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_168.In(logic_uScriptCon_CompareBool_Bool_168);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_168.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_168.False;
		if (num)
		{
			Relay_In_79();
		}
		if (flag)
		{
			Relay_In_103();
		}
	}

	private void Relay_In_172()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_172 = clearMsgTagWhenNormal;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_172.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_172, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_172);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_172.Out)
		{
			Relay_In_166();
		}
	}

	private void Relay_In_176()
	{
		logic_uScript_MissionPromptBlock_Hide_targetBlock_176 = local_TerminalBlock_TankBlock;
		logic_uScript_MissionPromptBlock_Hide_uScript_MissionPromptBlock_Hide_176.In(logic_uScript_MissionPromptBlock_Hide_targetBlock_176);
		if (logic_uScript_MissionPromptBlock_Hide_uScript_MissionPromptBlock_Hide_176.Out)
		{
			Relay_In_15();
		}
	}
}
