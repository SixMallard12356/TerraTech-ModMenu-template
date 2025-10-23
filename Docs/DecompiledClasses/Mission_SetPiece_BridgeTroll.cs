using System;
using BehaviorDesigner.Runtime;
using UnityEngine;

[Serializable]
[FriendlyName("Untitled", "")]
[NodePath("Graphs")]
public class Mission_SetPiece_BridgeTroll : uScriptLogic
{
	private delegate void ContinueExecution();

	private GameObject parentGameObject;

	private uScript_GUI thisScriptsOnGuiListener;

	private bool m_RegisteredForEvents;

	private ContinueExecution m_ContinueExecution;

	private bool m_Breakpoint;

	private const int MaxRelayCallCount = 1000;

	private int relayCallCount;

	public bool _DEBUGIgnoreMoneyCheck;

	[Multiline(3)]
	public string AngryZone = "";

	[Multiline(3)]
	public string clearMsgTagWhenAngry = "Normal";

	[Multiline(3)]
	public string clearMsgTagWhenNormal = "Angry";

	public float distNPCFound;

	public uScript_AddMessage.MessageSpeaker genericSpeaker;

	public BlockTypes interactableBlockType;

	[Multiline(3)]
	public string KillBoxTrigger = "";

	public int LicenseCost;

	private int local_189_System_Int32 = 1;

	private TankBlock local_221_TankBlock;

	private bool local_229_System_Boolean;

	private int local_87_System_Int32;

	private bool local_AngeredNPC_System_Boolean;

	private int local_CurrentMoney_System_Int32;

	private bool local_FallenOffBridge_System_Boolean;

	private bool local_HasEnoughMoney_System_Boolean;

	private bool local_HighlightBlock_System_Boolean;

	private bool local_Init_System_Boolean;

	private bool local_LicensePurchased_System_Boolean;

	private bool local_msg01Shown_System_Boolean;

	private ManOnScreenMessages.OnScreenMessage local_Msg02ClickScreen_ManOnScreenMessages_OnScreenMessage;

	private bool local_msg03aShown_System_Boolean;

	private bool local_msg03bShown_System_Boolean;

	private bool local_NearNPC_System_Boolean;

	private Tank local_NPCTech_Tank;

	private Tank[] local_NPCTechs_TankArray = new Tank[0];

	private Tank local_PaymentPointTech_Tank;

	private Tank[] local_PaymentTechs_TankArray = new Tank[0];

	private int local_Stage_System_Int32 = 1;

	private TankBlock local_TerminalBlock_TankBlock;

	private bool local_WaitingOnPrompt_System_Boolean;

	public uScript_AddMessage.MessageSpeaker messageSpeaker;

	public uScript_AddMessage.MessageData msg01Intro;

	public uScript_AddMessage.MessageData msg02ClickScreen;

	public uScript_AddMessage.MessageData msg02ClickScreen_Pad;

	public uScript_AddMessage.MessageData msg03aPurchaseDeclined;

	public uScript_AddMessage.MessageData msg03bNotEnoughMoney;

	public uScript_AddMessage.MessageData msg05LicensePurchased;

	public uScript_AddMessage.MessageData msgAngered;

	public uScript_AddMessage.MessageData msgMissionComplete;

	public uScript_AddMessage.MessageData msgMissionCompleteBridgeFall;

	public LocalisedString msgPromptAccept;

	public LocalisedString msgPromptDecline;

	public LocalisedString msgPromptNoMoney;

	public LocalisedString msgPromptText;

	public Transform NPCDespawnParticleEffect;

	public ExternalBehaviorTree NPCFlyAwayAI;

	public SpawnTechData[] NPCSpawnData = new SpawnTechData[0];

	public SpawnTechData[] VendorSpawnData = new SpawnTechData[0];

	private GameObject owner_Connection_6;

	private GameObject owner_Connection_7;

	private GameObject owner_Connection_17;

	private GameObject owner_Connection_18;

	private GameObject owner_Connection_23;

	private GameObject owner_Connection_31;

	private GameObject owner_Connection_135;

	private GameObject owner_Connection_152;

	private GameObject owner_Connection_154;

	private GameObject owner_Connection_187;

	private GameObject owner_Connection_216;

	private GameObject owner_Connection_220;

	private GameObject owner_Connection_249;

	private GameObject owner_Connection_250;

	private SubGraph_SaveLoadInt logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_0 = new SubGraph_SaveLoadInt();

	private int logic_SubGraph_SaveLoadInt_restartValue_0 = 1;

	private int logic_SubGraph_SaveLoadInt_integer_0;

	private object logic_SubGraph_SaveLoadInt_intAsVariable_0 = "";

	private string logic_SubGraph_SaveLoadInt_uniqueID_0 = "Stage";

	private uScript_SetTankInvulnerable logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_2 = new uScript_SetTankInvulnerable();

	private bool logic_uScript_SetTankInvulnerable_invulnerable_2 = true;

	private Tank logic_uScript_SetTankInvulnerable_tank_2;

	private bool logic_uScript_SetTankInvulnerable_Out_2 = true;

	private uScript_SetEncounterTarget logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_5 = new uScript_SetEncounterTarget();

	private GameObject logic_uScript_SetEncounterTarget_owner_5;

	private object logic_uScript_SetEncounterTarget_visibleObject_5 = "";

	private bool logic_uScript_SetEncounterTarget_Out_5 = true;

	private SubGraph_CompleteObjectiveStage logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_8 = new SubGraph_CompleteObjectiveStage();

	private int logic_SubGraph_CompleteObjectiveStage_objectiveStage_8;

	private bool logic_SubGraph_CompleteObjectiveStage_isFinalObjective_8;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_10 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_10;

	private bool logic_uScriptCon_CompareBool_True_10 = true;

	private bool logic_uScriptCon_CompareBool_False_10 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_11 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_11 = new Tank[0];

	private int logic_uScript_AccessListTech_index_11;

	private Tank logic_uScript_AccessListTech_value_11;

	private bool logic_uScript_AccessListTech_Out_11 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_12 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_12;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_12;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_12;

	private bool logic_uScript_AddMessage_Out_12 = true;

	private bool logic_uScript_AddMessage_Shown_12 = true;

	private uScriptCon_ManualSwitch logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_16 = new uScriptCon_ManualSwitch();

	private int logic_uScriptCon_ManualSwitch_CurrentOutput_16;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_24 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_24 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_24;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_24 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_24;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_24 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_24 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_24 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_24 = true;

	private uScript_FinishEncounter logic_uScript_FinishEncounter_uScript_FinishEncounter_25 = new uScript_FinishEncounter();

	private GameObject logic_uScript_FinishEncounter_owner_25;

	private bool logic_uScript_FinishEncounter_Out_25 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_27 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_27 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_27;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_27 = 0.3f;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_27;

	private bool logic_uScript_SpawnTechsFromData_Out_27 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_29 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_29 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_29 = true;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_29 = true;

	private uScript_IsPlayerInRangeOfTech logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_32 = new uScript_IsPlayerInRangeOfTech();

	private Tank logic_uScript_IsPlayerInRangeOfTech_tech_32;

	private float logic_uScript_IsPlayerInRangeOfTech_range_32;

	private Tank[] logic_uScript_IsPlayerInRangeOfTech_techs_32 = new Tank[0];

	private bool logic_uScript_IsPlayerInRangeOfTech_Out_32 = true;

	private bool logic_uScript_IsPlayerInRangeOfTech_InRange_32 = true;

	private bool logic_uScript_IsPlayerInRangeOfTech_OutOfRange_32 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_33 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_33;

	private bool logic_uScriptAct_SetBool_Out_33 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_33 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_33 = true;

	private SubGraph_LoadObjectiveStates logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_36 = new SubGraph_LoadObjectiveStates();

	private int logic_SubGraph_LoadObjectiveStates_currentObjective_36;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_38 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_38;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_38 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_38 = "Init";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_41 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_41;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_41 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_41 = "LicensePurchased";

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_44 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_44;

	private bool logic_uScriptCon_CompareBool_True_44 = true;

	private bool logic_uScriptCon_CompareBool_False_44 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_46 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_46;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_46 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_46 = "msg01Shown";

	private uScript_FlyTechUpAndAway logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_47 = new uScript_FlyTechUpAndAway();

	private Tank logic_uScript_FlyTechUpAndAway_tech_47;

	private float logic_uScript_FlyTechUpAndAway_maxLifetime_47 = 5f;

	private float logic_uScript_FlyTechUpAndAway_targetHeight_47 = 150f;

	private ExternalBehaviorTree logic_uScript_FlyTechUpAndAway_aiTree_47;

	private Transform logic_uScript_FlyTechUpAndAway_removalParticles_47;

	private bool logic_uScript_FlyTechUpAndAway_Out_47 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_51 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_51;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_51;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_51;

	private bool logic_uScript_AddMessage_Out_51 = true;

	private bool logic_uScript_AddMessage_Shown_51 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_55 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_55;

	private bool logic_uScriptCon_CompareBool_True_55 = true;

	private bool logic_uScriptCon_CompareBool_False_55 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_57 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_57;

	private bool logic_uScriptAct_SetBool_Out_57 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_57 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_57 = true;

	private uScript_GetCurrentMoneyEarned logic_uScript_GetCurrentMoneyEarned_uScript_GetCurrentMoneyEarned_58 = new uScript_GetCurrentMoneyEarned();

	private int logic_uScript_GetCurrentMoneyEarned_Return_58;

	private bool logic_uScript_GetCurrentMoneyEarned_Out_58 = true;

	private uScriptCon_CompareInt logic_uScriptCon_CompareInt_uScriptCon_CompareInt_60 = new uScriptCon_CompareInt();

	private int logic_uScriptCon_CompareInt_A_60;

	private int logic_uScriptCon_CompareInt_B_60;

	private bool logic_uScriptCon_CompareInt_GreaterThan_60 = true;

	private bool logic_uScriptCon_CompareInt_GreaterThanOrEqualTo_60 = true;

	private bool logic_uScriptCon_CompareInt_EqualTo_60 = true;

	private bool logic_uScriptCon_CompareInt_NotEqualTo_60 = true;

	private bool logic_uScriptCon_CompareInt_LessThanOrEqualTo_60 = true;

	private bool logic_uScriptCon_CompareInt_LessThan_60 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_66 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_66;

	private bool logic_uScriptAct_SetBool_Out_66 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_66 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_66 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_70 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_70;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_70 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_70 = "msg03aShown";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_71 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_71;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_71 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_71 = "msg03bShown";

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_73 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_73;

	private bool logic_uScriptAct_SetBool_Out_73 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_73 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_73 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_75 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_75;

	private bool logic_uScriptCon_CompareBool_True_75 = true;

	private bool logic_uScriptCon_CompareBool_False_75 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_78 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_78;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_78;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_78;

	private bool logic_uScript_AddMessage_Out_78 = true;

	private bool logic_uScript_AddMessage_Shown_78 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_82 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_82;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_82;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_82;

	private bool logic_uScript_AddMessage_Out_82 = true;

	private bool logic_uScript_AddMessage_Shown_82 = true;

	private uScript_AddMoney logic_uScript_AddMoney_uScript_AddMoney_85 = new uScript_AddMoney();

	private int logic_uScript_AddMoney_amount_85;

	private bool logic_uScript_AddMoney_Out_85 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_88 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_88;

	private bool logic_uScriptAct_SetBool_Out_88 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_88 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_88 = true;

	private uScriptAct_MultiplyInt_v2 logic_uScriptAct_MultiplyInt_v2_uScriptAct_MultiplyInt_v2_89 = new uScriptAct_MultiplyInt_v2();

	private int logic_uScriptAct_MultiplyInt_v2_A_89;

	private int logic_uScriptAct_MultiplyInt_v2_B_89 = -1;

	private int logic_uScriptAct_MultiplyInt_v2_IntResult_89;

	private float logic_uScriptAct_MultiplyInt_v2_FloatResult_89;

	private bool logic_uScriptAct_MultiplyInt_v2_Out_89 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_90 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_90;

	private bool logic_uScriptAct_SetBool_Out_90 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_90 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_90 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_93 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_93;

	private bool logic_uScriptCon_CompareBool_True_93 = true;

	private bool logic_uScriptCon_CompareBool_False_93 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_96 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_96;

	private bool logic_uScriptCon_CompareBool_True_96 = true;

	private bool logic_uScriptCon_CompareBool_False_96 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_98 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_98;

	private bool logic_uScriptAct_SetBool_Out_98 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_98 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_98 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_100 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_100;

	private bool logic_uScriptCon_CompareBool_True_100 = true;

	private bool logic_uScriptCon_CompareBool_False_100 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_102 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_102;

	private bool logic_uScriptAct_SetBool_Out_102 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_102 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_102 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_103 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_103 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_104 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_104 = true;

	private uScript_GetTankBlock logic_uScript_GetTankBlock_uScript_GetTankBlock_107 = new uScript_GetTankBlock();

	private Tank logic_uScript_GetTankBlock_tank_107;

	private BlockTypes logic_uScript_GetTankBlock_blockType_107;

	private TankBlock logic_uScript_GetTankBlock_Return_107;

	private bool logic_uScript_GetTankBlock_Out_107 = true;

	private bool logic_uScript_GetTankBlock_Returned_107 = true;

	private bool logic_uScript_GetTankBlock_NotFound_107 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_108 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_108;

	private bool logic_uScriptAct_SetBool_Out_108 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_108 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_108 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_110 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_110 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_112 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_112;

	private bool logic_uScriptCon_CompareBool_True_112 = true;

	private bool logic_uScriptCon_CompareBool_False_112 = true;

	private SubGraph_AddMessageWithPadSupport logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_114 = new SubGraph_AddMessageWithPadSupport();

	private uScript_AddMessage.MessageData logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboard_114;

	private uScript_AddMessage.MessageData logic_SubGraph_AddMessageWithPadSupport_messageControlPad_114;

	private uScript_AddMessage.MessageSpeaker logic_SubGraph_AddMessageWithPadSupport_speaker_114;

	private ManOnScreenMessages.OnScreenMessage logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboardReturn_114;

	private ManOnScreenMessages.OnScreenMessage logic_SubGraph_AddMessageWithPadSupport_messageControlPadReturn_114;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_121 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_121 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_121 = true;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_121 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_122 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_122;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_122 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_122 = "AngeredNPC";

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_124 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_124;

	private bool logic_uScriptCon_CompareBool_True_124 = true;

	private bool logic_uScriptCon_CompareBool_False_124 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_125 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_125;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_125;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_125;

	private bool logic_uScript_AddMessage_Out_125 = true;

	private bool logic_uScript_AddMessage_Shown_125 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_128 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_128;

	private bool logic_uScriptCon_CompareBool_True_128 = true;

	private bool logic_uScriptCon_CompareBool_False_128 = true;

	private uScript_FinishEncounter logic_uScript_FinishEncounter_uScript_FinishEncounter_130 = new uScript_FinishEncounter();

	private GameObject logic_uScript_FinishEncounter_owner_130;

	private bool logic_uScript_FinishEncounter_Out_130 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_133 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_133;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_133;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_133;

	private bool logic_uScript_AddMessage_Out_133 = true;

	private bool logic_uScript_AddMessage_Shown_133 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_138 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_138 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_138 = true;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_138 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_139 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_139 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_139 = true;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_139 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_142 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_142 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_142 = true;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_142 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_143 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_143;

	private bool logic_uScriptAct_SetBool_Out_143 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_143 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_143 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_145 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_145;

	private bool logic_uScriptCon_CompareBool_True_145 = true;

	private bool logic_uScriptCon_CompareBool_False_145 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_147 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_147 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_147 = true;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_147 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_149 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_149 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_149 = true;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_149 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_153 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_153 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_153;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_153 = 0.3f;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_153;

	private bool logic_uScript_SpawnTechsFromData_Out_153 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_155 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_155 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_155;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_155 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_155;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_155 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_155 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_155 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_155 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_157 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_157 = new Tank[0];

	private int logic_uScript_AccessListTech_index_157;

	private Tank logic_uScript_AccessListTech_value_157;

	private bool logic_uScript_AccessListTech_Out_157 = true;

	private uScript_SetTankInvulnerable logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_159 = new uScript_SetTankInvulnerable();

	private bool logic_uScript_SetTankInvulnerable_invulnerable_159 = true;

	private Tank logic_uScript_SetTankInvulnerable_tank_159;

	private bool logic_uScript_SetTankInvulnerable_Out_159 = true;

	private uScript_FlyTechUpAndAway logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_163 = new uScript_FlyTechUpAndAway();

	private Tank logic_uScript_FlyTechUpAndAway_tech_163;

	private float logic_uScript_FlyTechUpAndAway_maxLifetime_163 = 5f;

	private float logic_uScript_FlyTechUpAndAway_targetHeight_163 = 150f;

	private ExternalBehaviorTree logic_uScript_FlyTechUpAndAway_aiTree_163;

	private Transform logic_uScript_FlyTechUpAndAway_removalParticles_163;

	private bool logic_uScript_FlyTechUpAndAway_Out_163 = true;

	private uScript_FlyTechUpAndAway logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_168 = new uScript_FlyTechUpAndAway();

	private Tank logic_uScript_FlyTechUpAndAway_tech_168;

	private float logic_uScript_FlyTechUpAndAway_maxLifetime_168 = 5f;

	private float logic_uScript_FlyTechUpAndAway_targetHeight_168 = 150f;

	private ExternalBehaviorTree logic_uScript_FlyTechUpAndAway_aiTree_168;

	private Transform logic_uScript_FlyTechUpAndAway_removalParticles_168;

	private bool logic_uScript_FlyTechUpAndAway_Out_168 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_171 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_171;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_171;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_171;

	private bool logic_uScript_AddMessage_Out_171 = true;

	private bool logic_uScript_AddMessage_Shown_171 = true;

	private uScript_IsVisibleInTrigger logic_uScript_IsVisibleInTrigger_uScript_IsVisibleInTrigger_174 = new uScript_IsVisibleInTrigger();

	private object logic_uScript_IsVisibleInTrigger_visibleObject_174 = "";

	private string logic_uScript_IsVisibleInTrigger_triggerAreaName_174 = "";

	private bool logic_uScript_IsVisibleInTrigger_Out_174 = true;

	private bool logic_uScript_IsVisibleInTrigger_InRange_174 = true;

	private bool logic_uScript_IsVisibleInTrigger_OutOfRange_174 = true;

	private uScript_DamageTechs logic_uScript_DamageTechs_uScript_DamageTechs_176 = new uScript_DamageTechs();

	private Tank[] logic_uScript_DamageTechs_techs_176 = new Tank[0];

	private float logic_uScript_DamageTechs_dmgPercent_176 = 100f;

	private bool logic_uScript_DamageTechs_givePlyrCredit_176;

	private float logic_uScript_DamageTechs_leaveBlksPercent_176;

	private bool logic_uScript_DamageTechs_makeVulnerable_176;

	private bool logic_uScript_DamageTechs_Out_176 = true;

	private uScript_SetTankInvulnerable logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_180 = new uScript_SetTankInvulnerable();

	private bool logic_uScript_SetTankInvulnerable_invulnerable_180;

	private Tank logic_uScript_SetTankInvulnerable_tank_180;

	private bool logic_uScript_SetTankInvulnerable_Out_180 = true;

	private uScript_FlyTechUpAndAway logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_182 = new uScript_FlyTechUpAndAway();

	private Tank logic_uScript_FlyTechUpAndAway_tech_182;

	private float logic_uScript_FlyTechUpAndAway_maxLifetime_182 = 5f;

	private float logic_uScript_FlyTechUpAndAway_targetHeight_182 = 150f;

	private ExternalBehaviorTree logic_uScript_FlyTechUpAndAway_aiTree_182;

	private Transform logic_uScript_FlyTechUpAndAway_removalParticles_182;

	private bool logic_uScript_FlyTechUpAndAway_Out_182 = true;

	private uScript_GetTankBlock logic_uScript_GetTankBlock_uScript_GetTankBlock_183 = new uScript_GetTankBlock();

	private Tank logic_uScript_GetTankBlock_tank_183;

	private BlockTypes logic_uScript_GetTankBlock_blockType_183;

	private TankBlock logic_uScript_GetTankBlock_Return_183;

	private bool logic_uScript_GetTankBlock_Out_183 = true;

	private bool logic_uScript_GetTankBlock_Returned_183 = true;

	private bool logic_uScript_GetTankBlock_NotFound_183 = true;

	private uScript_SetTankTeam logic_uScript_SetTankTeam_uScript_SetTankTeam_185 = new uScript_SetTankTeam();

	private Tank logic_uScript_SetTankTeam_tank_185;

	private int logic_uScript_SetTankTeam_team_185;

	private bool logic_uScript_SetTankTeam_Out_185 = true;

	private uScript_SetEncounterTarget logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_186 = new uScript_SetEncounterTarget();

	private GameObject logic_uScript_SetEncounterTarget_owner_186;

	private object logic_uScript_SetEncounterTarget_visibleObject_186 = "";

	private bool logic_uScript_SetEncounterTarget_Out_186 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_195 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_195 = true;

	private uScript_PausePopulation logic_uScript_PausePopulation_uScript_PausePopulation_196 = new uScript_PausePopulation();

	private bool logic_uScript_PausePopulation_Out_196 = true;

	private uScript_PausePopulation logic_uScript_PausePopulation_uScript_PausePopulation_197 = new uScript_PausePopulation();

	private bool logic_uScript_PausePopulation_Out_197 = true;

	private uScript_PausePopulation logic_uScript_PausePopulation_uScript_PausePopulation_198 = new uScript_PausePopulation();

	private bool logic_uScript_PausePopulation_Out_198 = true;

	private uScript_PausePopulation logic_uScript_PausePopulation_uScript_PausePopulation_199 = new uScript_PausePopulation();

	private bool logic_uScript_PausePopulation_Out_199 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_200 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_200;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_200 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_200 = "NearNPC";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_201 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_201;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_201 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_201 = "HasEnoughMoney";

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_202 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_202;

	private bool logic_uScriptCon_CompareBool_True_202 = true;

	private bool logic_uScriptCon_CompareBool_False_202 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_203 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_203;

	private bool logic_uScriptAct_SetBool_Out_203 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_203 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_203 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_205 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_205;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_205;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_205;

	private bool logic_uScript_AddMessage_Out_205 = true;

	private bool logic_uScript_AddMessage_Shown_205 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_210 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_210;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_210 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_210 = "FallenOffBridge";

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_212 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_212;

	private bool logic_uScriptCon_CompareBool_True_212 = true;

	private bool logic_uScriptCon_CompareBool_False_212 = true;

	private uScript_DirectEnemiesOutOfEncounter logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_213 = new uScript_DirectEnemiesOutOfEncounter();

	private GameObject logic_uScript_DirectEnemiesOutOfEncounter_ownerNode_213;

	private bool logic_uScript_DirectEnemiesOutOfEncounter_Out_213 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_214 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_214;

	private bool logic_uScriptAct_SetBool_Out_214 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_214 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_214 = true;

	private uScript_MissionPromptBlock_Show logic_uScript_MissionPromptBlock_Show_uScript_MissionPromptBlock_Show_217 = new uScript_MissionPromptBlock_Show();

	private LocalisedString logic_uScript_MissionPromptBlock_Show_bodyText_217;

	private LocalisedString logic_uScript_MissionPromptBlock_Show_acceptButtonText_217;

	private LocalisedString logic_uScript_MissionPromptBlock_Show_rejectButtonText_217;

	private TankBlock logic_uScript_MissionPromptBlock_Show_targetBlock_217;

	private bool logic_uScript_MissionPromptBlock_Show_highlightBlock_217;

	private bool logic_uScript_MissionPromptBlock_Show_singleUse_217;

	private bool logic_uScript_MissionPromptBlock_Show_Out_217 = true;

	private uScript_MissionPromptBlock_Show logic_uScript_MissionPromptBlock_Show_uScript_MissionPromptBlock_Show_218 = new uScript_MissionPromptBlock_Show();

	private LocalisedString logic_uScript_MissionPromptBlock_Show_bodyText_218;

	private LocalisedString logic_uScript_MissionPromptBlock_Show_acceptButtonText_218;

	private LocalisedString logic_uScript_MissionPromptBlock_Show_rejectButtonText_218;

	private TankBlock logic_uScript_MissionPromptBlock_Show_targetBlock_218;

	private bool logic_uScript_MissionPromptBlock_Show_highlightBlock_218;

	private bool logic_uScript_MissionPromptBlock_Show_singleUse_218;

	private bool logic_uScript_MissionPromptBlock_Show_Out_218 = true;

	private uScript_CompareBlock logic_uScript_CompareBlock_uScript_CompareBlock_222 = new uScript_CompareBlock();

	private TankBlock logic_uScript_CompareBlock_A_222;

	private TankBlock logic_uScript_CompareBlock_B_222;

	private bool logic_uScript_CompareBlock_EqualTo_222 = true;

	private bool logic_uScript_CompareBlock_NotEqualTo_222 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_225 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_225 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_225 = true;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_225 = true;

	private uScript_RemoveOnScreenMessage logic_uScript_RemoveOnScreenMessage_uScript_RemoveOnScreenMessage_227 = new uScript_RemoveOnScreenMessage();

	private ManOnScreenMessages.OnScreenMessage logic_uScript_RemoveOnScreenMessage_onScreenMessage_227;

	private bool logic_uScript_RemoveOnScreenMessage_instant_227;

	private bool logic_uScript_RemoveOnScreenMessage_Out_227 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_230 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_230;

	private bool logic_uScriptCon_CompareBool_True_230 = true;

	private bool logic_uScriptCon_CompareBool_False_230 = true;

	private uScript_MissionPromptBlock_Hide logic_uScript_MissionPromptBlock_Hide_uScript_MissionPromptBlock_Hide_231 = new uScript_MissionPromptBlock_Hide();

	private TankBlock logic_uScript_MissionPromptBlock_Hide_targetBlock_231;

	private bool logic_uScript_MissionPromptBlock_Hide_Out_231 = true;

	private uScript_MissionPromptBlock_Hide logic_uScript_MissionPromptBlock_Hide_uScript_MissionPromptBlock_Hide_233 = new uScript_MissionPromptBlock_Hide();

	private TankBlock logic_uScript_MissionPromptBlock_Hide_targetBlock_233;

	private bool logic_uScript_MissionPromptBlock_Hide_Out_233 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_235 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_235;

	private bool logic_uScriptCon_CompareBool_True_235 = true;

	private bool logic_uScriptCon_CompareBool_False_235 = true;

	private uScriptCon_CompareInt logic_uScriptCon_CompareInt_uScriptCon_CompareInt_238 = new uScriptCon_CompareInt();

	private int logic_uScriptCon_CompareInt_A_238;

	private int logic_uScriptCon_CompareInt_B_238;

	private bool logic_uScriptCon_CompareInt_GreaterThan_238 = true;

	private bool logic_uScriptCon_CompareInt_GreaterThanOrEqualTo_238 = true;

	private bool logic_uScriptCon_CompareInt_EqualTo_238 = true;

	private bool logic_uScriptCon_CompareInt_NotEqualTo_238 = true;

	private bool logic_uScriptCon_CompareInt_LessThanOrEqualTo_238 = true;

	private bool logic_uScriptCon_CompareInt_LessThan_238 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_240 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_240 = true;

	private uScript_GetCurrentMoneyEarned logic_uScript_GetCurrentMoneyEarned_uScript_GetCurrentMoneyEarned_241 = new uScript_GetCurrentMoneyEarned();

	private int logic_uScript_GetCurrentMoneyEarned_Return_241;

	private bool logic_uScript_GetCurrentMoneyEarned_Out_241 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_242 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_242;

	private bool logic_uScriptCon_CompareBool_True_242 = true;

	private bool logic_uScriptCon_CompareBool_False_242 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_243 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_243 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_246 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_246;

	private bool logic_uScriptCon_CompareBool_True_246 = true;

	private bool logic_uScriptCon_CompareBool_False_246 = true;

	private uScript_PausePopulation logic_uScript_PausePopulation_uScript_PausePopulation_247 = new uScript_PausePopulation();

	private bool logic_uScript_PausePopulation_Out_247 = true;

	private uScript_ClearEncounterTarget logic_uScript_ClearEncounterTarget_uScript_ClearEncounterTarget_248 = new uScript_ClearEncounterTarget();

	private GameObject logic_uScript_ClearEncounterTarget_owner_248;

	private bool logic_uScript_ClearEncounterTarget_Out_248 = true;

	private uScript_ClearEncounterTarget logic_uScript_ClearEncounterTarget_uScript_ClearEncounterTarget_251 = new uScript_ClearEncounterTarget();

	private GameObject logic_uScript_ClearEncounterTarget_owner_251;

	private bool logic_uScript_ClearEncounterTarget_Out_251 = true;

	private uScript_IsPlayerInTrigger logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_252 = new uScript_IsPlayerInTrigger();

	private string logic_uScript_IsPlayerInTrigger_triggerAreaName_252 = "";

	private bool logic_uScript_IsPlayerInTrigger_Out_252 = true;

	private bool logic_uScript_IsPlayerInTrigger_AllInRange_252 = true;

	private bool logic_uScript_IsPlayerInTrigger_InRange_252 = true;

	private bool logic_uScript_IsPlayerInTrigger_SomeOutOfRange_252 = true;

	private bool logic_uScript_IsPlayerInTrigger_OutOfRange_252 = true;

	private uScript_MissionPromptBlock_Hide logic_uScript_MissionPromptBlock_Hide_uScript_MissionPromptBlock_Hide_254 = new uScript_MissionPromptBlock_Hide();

	private TankBlock logic_uScript_MissionPromptBlock_Hide_targetBlock_254;

	private bool logic_uScript_MissionPromptBlock_Hide_Out_254 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_256 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_256;

	private bool logic_uScriptAct_SetBool_Out_256 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_256 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_256 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_261 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_261;

	private bool logic_uScriptAct_SetBool_Out_261 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_261 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_261 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_262 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_262;

	private bool logic_uScriptAct_SetBool_Out_262 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_262 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_262 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_264 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_264;

	private bool logic_uScriptAct_SetBool_Out_264 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_264 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_264 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_267 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_267;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_267 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_267 = "HighlightBlock";

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_268 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_268;

	private bool logic_uScriptAct_SetBool_Out_268 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_268 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_268 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_270 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_270;

	private bool logic_uScriptAct_SetBool_Out_270 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_270 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_270 = true;

	private uScript_GetTankBlock logic_uScript_GetTankBlock_uScript_GetTankBlock_274 = new uScript_GetTankBlock();

	private Tank logic_uScript_GetTankBlock_tank_274;

	private BlockTypes logic_uScript_GetTankBlock_blockType_274;

	private TankBlock logic_uScript_GetTankBlock_Return_274;

	private bool logic_uScript_GetTankBlock_Out_274 = true;

	private bool logic_uScript_GetTankBlock_Returned_274 = true;

	private bool logic_uScript_GetTankBlock_NotFound_274 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_280 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_280;

	private bool logic_uScriptAct_SetBool_Out_280 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_280 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_280 = true;

	private TankBlock event_UnityEngine_GameObject_TankBlock_228;

	private bool event_UnityEngine_GameObject_Accepted_228;

	private void SyncUnityHooks()
	{
		SyncEventListeners();
		if (null == owner_Connection_6 || !m_RegisteredForEvents)
		{
			owner_Connection_6 = parentGameObject;
			if (null != owner_Connection_6)
			{
				uScript_EncounterUpdate uScript_EncounterUpdate2 = owner_Connection_6.GetComponent<uScript_EncounterUpdate>();
				if (null == uScript_EncounterUpdate2)
				{
					uScript_EncounterUpdate2 = owner_Connection_6.AddComponent<uScript_EncounterUpdate>();
				}
				if (null != uScript_EncounterUpdate2)
				{
					uScript_EncounterUpdate2.OnUpdate += Instance_OnUpdate_19;
					uScript_EncounterUpdate2.OnSuspend += Instance_OnSuspend_19;
					uScript_EncounterUpdate2.OnResume += Instance_OnResume_19;
				}
			}
		}
		if (null == owner_Connection_7 || !m_RegisteredForEvents)
		{
			owner_Connection_7 = parentGameObject;
		}
		if (null == owner_Connection_17 || !m_RegisteredForEvents)
		{
			owner_Connection_17 = parentGameObject;
		}
		if (null == owner_Connection_18 || !m_RegisteredForEvents)
		{
			owner_Connection_18 = parentGameObject;
		}
		if (null == owner_Connection_23 || !m_RegisteredForEvents)
		{
			owner_Connection_23 = parentGameObject;
			if (null != owner_Connection_23)
			{
				uScript_SaveLoad uScript_SaveLoad2 = owner_Connection_23.GetComponent<uScript_SaveLoad>();
				if (null == uScript_SaveLoad2)
				{
					uScript_SaveLoad2 = owner_Connection_23.AddComponent<uScript_SaveLoad>();
				}
				if (null != uScript_SaveLoad2)
				{
					uScript_SaveLoad2.SaveEvent += Instance_SaveEvent_37;
					uScript_SaveLoad2.LoadEvent += Instance_LoadEvent_37;
					uScript_SaveLoad2.RestartEvent += Instance_RestartEvent_37;
				}
			}
		}
		if (null == owner_Connection_31 || !m_RegisteredForEvents)
		{
			owner_Connection_31 = parentGameObject;
		}
		if (null == owner_Connection_135 || !m_RegisteredForEvents)
		{
			owner_Connection_135 = parentGameObject;
		}
		if (null == owner_Connection_152 || !m_RegisteredForEvents)
		{
			owner_Connection_152 = parentGameObject;
		}
		if (null == owner_Connection_154 || !m_RegisteredForEvents)
		{
			owner_Connection_154 = parentGameObject;
		}
		if (null == owner_Connection_187 || !m_RegisteredForEvents)
		{
			owner_Connection_187 = parentGameObject;
		}
		if (null == owner_Connection_216 || !m_RegisteredForEvents)
		{
			owner_Connection_216 = parentGameObject;
		}
		if (null == owner_Connection_220 || !m_RegisteredForEvents)
		{
			owner_Connection_220 = parentGameObject;
			if (null != owner_Connection_220)
			{
				uScript_MissionPromptBlock_OnResult uScript_MissionPromptBlock_OnResult2 = owner_Connection_220.GetComponent<uScript_MissionPromptBlock_OnResult>();
				if (null == uScript_MissionPromptBlock_OnResult2)
				{
					uScript_MissionPromptBlock_OnResult2 = owner_Connection_220.AddComponent<uScript_MissionPromptBlock_OnResult>();
				}
				if (null != uScript_MissionPromptBlock_OnResult2)
				{
					uScript_MissionPromptBlock_OnResult2.ResponseEvent += Instance_ResponseEvent_228;
				}
			}
		}
		if (null == owner_Connection_249 || !m_RegisteredForEvents)
		{
			owner_Connection_249 = parentGameObject;
		}
		if (null == owner_Connection_250 || !m_RegisteredForEvents)
		{
			owner_Connection_250 = parentGameObject;
		}
	}

	private void RegisterForUnityHooks()
	{
		SyncEventListeners();
		if (!m_RegisteredForEvents && null != owner_Connection_6)
		{
			uScript_EncounterUpdate uScript_EncounterUpdate2 = owner_Connection_6.GetComponent<uScript_EncounterUpdate>();
			if (null == uScript_EncounterUpdate2)
			{
				uScript_EncounterUpdate2 = owner_Connection_6.AddComponent<uScript_EncounterUpdate>();
			}
			if (null != uScript_EncounterUpdate2)
			{
				uScript_EncounterUpdate2.OnUpdate += Instance_OnUpdate_19;
				uScript_EncounterUpdate2.OnSuspend += Instance_OnSuspend_19;
				uScript_EncounterUpdate2.OnResume += Instance_OnResume_19;
			}
		}
		if (!m_RegisteredForEvents && null != owner_Connection_23)
		{
			uScript_SaveLoad uScript_SaveLoad2 = owner_Connection_23.GetComponent<uScript_SaveLoad>();
			if (null == uScript_SaveLoad2)
			{
				uScript_SaveLoad2 = owner_Connection_23.AddComponent<uScript_SaveLoad>();
			}
			if (null != uScript_SaveLoad2)
			{
				uScript_SaveLoad2.SaveEvent += Instance_SaveEvent_37;
				uScript_SaveLoad2.LoadEvent += Instance_LoadEvent_37;
				uScript_SaveLoad2.RestartEvent += Instance_RestartEvent_37;
			}
		}
		if (!m_RegisteredForEvents && null != owner_Connection_220)
		{
			uScript_MissionPromptBlock_OnResult uScript_MissionPromptBlock_OnResult2 = owner_Connection_220.GetComponent<uScript_MissionPromptBlock_OnResult>();
			if (null == uScript_MissionPromptBlock_OnResult2)
			{
				uScript_MissionPromptBlock_OnResult2 = owner_Connection_220.AddComponent<uScript_MissionPromptBlock_OnResult>();
			}
			if (null != uScript_MissionPromptBlock_OnResult2)
			{
				uScript_MissionPromptBlock_OnResult2.ResponseEvent += Instance_ResponseEvent_228;
			}
		}
	}

	private void SyncEventListeners()
	{
	}

	private void UnregisterEventListeners()
	{
		if (null != owner_Connection_6)
		{
			uScript_EncounterUpdate component = owner_Connection_6.GetComponent<uScript_EncounterUpdate>();
			if (null != component)
			{
				component.OnUpdate -= Instance_OnUpdate_19;
				component.OnSuspend -= Instance_OnSuspend_19;
				component.OnResume -= Instance_OnResume_19;
			}
		}
		if (null != owner_Connection_23)
		{
			uScript_SaveLoad component2 = owner_Connection_23.GetComponent<uScript_SaveLoad>();
			if (null != component2)
			{
				component2.SaveEvent -= Instance_SaveEvent_37;
				component2.LoadEvent -= Instance_LoadEvent_37;
				component2.RestartEvent -= Instance_RestartEvent_37;
			}
		}
		if (null != owner_Connection_220)
		{
			uScript_MissionPromptBlock_OnResult component3 = owner_Connection_220.GetComponent<uScript_MissionPromptBlock_OnResult>();
			if (null != component3)
			{
				component3.ResponseEvent -= Instance_ResponseEvent_228;
			}
		}
	}

	public override void SetParent(GameObject g)
	{
		parentGameObject = g;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_0.SetParent(g);
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_2.SetParent(g);
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_5.SetParent(g);
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_8.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_10.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_11.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_12.SetParent(g);
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_16.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_24.SetParent(g);
		logic_uScript_FinishEncounter_uScript_FinishEncounter_25.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_27.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_29.SetParent(g);
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_32.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_33.SetParent(g);
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_36.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_38.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_41.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_44.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_46.SetParent(g);
		logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_47.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_51.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_55.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_57.SetParent(g);
		logic_uScript_GetCurrentMoneyEarned_uScript_GetCurrentMoneyEarned_58.SetParent(g);
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_60.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_66.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_70.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_71.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_73.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_75.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_78.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_82.SetParent(g);
		logic_uScript_AddMoney_uScript_AddMoney_85.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_88.SetParent(g);
		logic_uScriptAct_MultiplyInt_v2_uScriptAct_MultiplyInt_v2_89.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_90.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_93.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_96.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_98.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_100.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_102.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_103.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_104.SetParent(g);
		logic_uScript_GetTankBlock_uScript_GetTankBlock_107.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_108.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_110.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_112.SetParent(g);
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_114.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_121.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_122.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_124.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_125.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_128.SetParent(g);
		logic_uScript_FinishEncounter_uScript_FinishEncounter_130.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_133.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_138.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_139.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_142.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_143.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_145.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_147.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_149.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_153.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_155.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_157.SetParent(g);
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_159.SetParent(g);
		logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_163.SetParent(g);
		logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_168.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_171.SetParent(g);
		logic_uScript_IsVisibleInTrigger_uScript_IsVisibleInTrigger_174.SetParent(g);
		logic_uScript_DamageTechs_uScript_DamageTechs_176.SetParent(g);
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_180.SetParent(g);
		logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_182.SetParent(g);
		logic_uScript_GetTankBlock_uScript_GetTankBlock_183.SetParent(g);
		logic_uScript_SetTankTeam_uScript_SetTankTeam_185.SetParent(g);
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_186.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_195.SetParent(g);
		logic_uScript_PausePopulation_uScript_PausePopulation_196.SetParent(g);
		logic_uScript_PausePopulation_uScript_PausePopulation_197.SetParent(g);
		logic_uScript_PausePopulation_uScript_PausePopulation_198.SetParent(g);
		logic_uScript_PausePopulation_uScript_PausePopulation_199.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_200.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_201.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_202.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_203.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_205.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_210.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_212.SetParent(g);
		logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_213.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_214.SetParent(g);
		logic_uScript_MissionPromptBlock_Show_uScript_MissionPromptBlock_Show_217.SetParent(g);
		logic_uScript_MissionPromptBlock_Show_uScript_MissionPromptBlock_Show_218.SetParent(g);
		logic_uScript_CompareBlock_uScript_CompareBlock_222.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_225.SetParent(g);
		logic_uScript_RemoveOnScreenMessage_uScript_RemoveOnScreenMessage_227.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_230.SetParent(g);
		logic_uScript_MissionPromptBlock_Hide_uScript_MissionPromptBlock_Hide_231.SetParent(g);
		logic_uScript_MissionPromptBlock_Hide_uScript_MissionPromptBlock_Hide_233.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_235.SetParent(g);
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_238.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_240.SetParent(g);
		logic_uScript_GetCurrentMoneyEarned_uScript_GetCurrentMoneyEarned_241.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_242.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_243.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_246.SetParent(g);
		logic_uScript_PausePopulation_uScript_PausePopulation_247.SetParent(g);
		logic_uScript_ClearEncounterTarget_uScript_ClearEncounterTarget_248.SetParent(g);
		logic_uScript_ClearEncounterTarget_uScript_ClearEncounterTarget_251.SetParent(g);
		logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_252.SetParent(g);
		logic_uScript_MissionPromptBlock_Hide_uScript_MissionPromptBlock_Hide_254.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_256.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_261.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_262.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_264.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_267.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_268.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_270.SetParent(g);
		logic_uScript_GetTankBlock_uScript_GetTankBlock_274.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_280.SetParent(g);
		owner_Connection_6 = parentGameObject;
		owner_Connection_7 = parentGameObject;
		owner_Connection_17 = parentGameObject;
		owner_Connection_18 = parentGameObject;
		owner_Connection_23 = parentGameObject;
		owner_Connection_31 = parentGameObject;
		owner_Connection_135 = parentGameObject;
		owner_Connection_152 = parentGameObject;
		owner_Connection_154 = parentGameObject;
		owner_Connection_187 = parentGameObject;
		owner_Connection_216 = parentGameObject;
		owner_Connection_220 = parentGameObject;
		owner_Connection_249 = parentGameObject;
		owner_Connection_250 = parentGameObject;
	}

	public void Awake()
	{
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_0.Awake();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_8.Awake();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_36.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_38.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_41.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_46.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_70.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_71.Awake();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_114.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_122.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_200.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_201.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_210.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_267.Awake();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_0.Save_Out += SubGraph_SaveLoadInt_Save_Out_0;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_0.Load_Out += SubGraph_SaveLoadInt_Load_Out_0;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_0.Restart_Out += SubGraph_SaveLoadInt_Restart_Out_0;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_8.Out += SubGraph_CompleteObjectiveStage_Out_8;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_16.Output1 += uScriptCon_ManualSwitch_Output1_16;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_16.Output2 += uScriptCon_ManualSwitch_Output2_16;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_16.Output3 += uScriptCon_ManualSwitch_Output3_16;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_16.Output4 += uScriptCon_ManualSwitch_Output4_16;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_16.Output5 += uScriptCon_ManualSwitch_Output5_16;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_16.Output6 += uScriptCon_ManualSwitch_Output6_16;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_16.Output7 += uScriptCon_ManualSwitch_Output7_16;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_16.Output8 += uScriptCon_ManualSwitch_Output8_16;
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_36.Out += SubGraph_LoadObjectiveStates_Out_36;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_38.Save_Out += SubGraph_SaveLoadBool_Save_Out_38;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_38.Load_Out += SubGraph_SaveLoadBool_Load_Out_38;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_38.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_38;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_41.Save_Out += SubGraph_SaveLoadBool_Save_Out_41;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_41.Load_Out += SubGraph_SaveLoadBool_Load_Out_41;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_41.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_41;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_46.Save_Out += SubGraph_SaveLoadBool_Save_Out_46;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_46.Load_Out += SubGraph_SaveLoadBool_Load_Out_46;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_46.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_46;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_70.Save_Out += SubGraph_SaveLoadBool_Save_Out_70;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_70.Load_Out += SubGraph_SaveLoadBool_Load_Out_70;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_70.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_70;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_71.Save_Out += SubGraph_SaveLoadBool_Save_Out_71;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_71.Load_Out += SubGraph_SaveLoadBool_Load_Out_71;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_71.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_71;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_114.Out += SubGraph_AddMessageWithPadSupport_Out_114;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_114.Shown += SubGraph_AddMessageWithPadSupport_Shown_114;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_122.Save_Out += SubGraph_SaveLoadBool_Save_Out_122;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_122.Load_Out += SubGraph_SaveLoadBool_Load_Out_122;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_122.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_122;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_200.Save_Out += SubGraph_SaveLoadBool_Save_Out_200;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_200.Load_Out += SubGraph_SaveLoadBool_Load_Out_200;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_200.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_200;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_201.Save_Out += SubGraph_SaveLoadBool_Save_Out_201;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_201.Load_Out += SubGraph_SaveLoadBool_Load_Out_201;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_201.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_201;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_210.Save_Out += SubGraph_SaveLoadBool_Save_Out_210;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_210.Load_Out += SubGraph_SaveLoadBool_Load_Out_210;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_210.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_210;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_267.Save_Out += SubGraph_SaveLoadBool_Save_Out_267;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_267.Load_Out += SubGraph_SaveLoadBool_Load_Out_267;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_267.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_267;
	}

	public void Start()
	{
		SyncUnityHooks();
		m_RegisteredForEvents = true;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_0.Start();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_8.Start();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_36.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_38.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_41.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_46.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_70.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_71.Start();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_114.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_122.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_200.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_201.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_210.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_267.Start();
	}

	public void OnEnable()
	{
		RegisterForUnityHooks();
		m_RegisteredForEvents = true;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_0.OnEnable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_8.OnEnable();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_36.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_38.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_41.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_46.OnEnable();
		logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_47.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_70.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_71.OnEnable();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_114.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_122.OnEnable();
		logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_163.OnEnable();
		logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_168.OnEnable();
		logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_182.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_200.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_201.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_210.OnEnable();
		logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_213.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_267.OnEnable();
	}

	public void OnDisable()
	{
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_0.OnDisable();
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_2.OnDisable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_8.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_12.OnDisable();
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_32.OnDisable();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_36.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_38.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_41.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_46.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_51.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_70.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_71.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_78.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_82.OnDisable();
		logic_uScript_GetTankBlock_uScript_GetTankBlock_107.OnDisable();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_114.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_122.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_125.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_133.OnDisable();
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_159.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_171.OnDisable();
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_180.OnDisable();
		logic_uScript_GetTankBlock_uScript_GetTankBlock_183.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_200.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_201.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_205.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_210.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_267.OnDisable();
		logic_uScript_GetTankBlock_uScript_GetTankBlock_274.OnDisable();
		UnregisterEventListeners();
		m_RegisteredForEvents = false;
	}

	public void Update()
	{
		if (relayCallCount < 1000)
		{
			relayCallCount = 0;
		}
		if (m_ContinueExecution != null)
		{
			ContinueExecution continueExecution = m_ContinueExecution;
			m_ContinueExecution = null;
			m_Breakpoint = false;
			continueExecution();
			return;
		}
		UpdateEditorValues();
		SyncEventListeners();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_0.Update();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_8.Update();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_36.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_38.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_41.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_46.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_70.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_71.Update();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_114.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_122.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_200.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_201.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_210.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_267.Update();
	}

	public void OnDestroy()
	{
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_0.OnDestroy();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_8.OnDestroy();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_36.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_38.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_41.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_46.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_70.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_71.OnDestroy();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_114.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_122.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_200.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_201.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_210.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_267.OnDestroy();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_0.Save_Out -= SubGraph_SaveLoadInt_Save_Out_0;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_0.Load_Out -= SubGraph_SaveLoadInt_Load_Out_0;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_0.Restart_Out -= SubGraph_SaveLoadInt_Restart_Out_0;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_8.Out -= SubGraph_CompleteObjectiveStage_Out_8;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_16.Output1 -= uScriptCon_ManualSwitch_Output1_16;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_16.Output2 -= uScriptCon_ManualSwitch_Output2_16;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_16.Output3 -= uScriptCon_ManualSwitch_Output3_16;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_16.Output4 -= uScriptCon_ManualSwitch_Output4_16;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_16.Output5 -= uScriptCon_ManualSwitch_Output5_16;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_16.Output6 -= uScriptCon_ManualSwitch_Output6_16;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_16.Output7 -= uScriptCon_ManualSwitch_Output7_16;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_16.Output8 -= uScriptCon_ManualSwitch_Output8_16;
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_36.Out -= SubGraph_LoadObjectiveStates_Out_36;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_38.Save_Out -= SubGraph_SaveLoadBool_Save_Out_38;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_38.Load_Out -= SubGraph_SaveLoadBool_Load_Out_38;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_38.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_38;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_41.Save_Out -= SubGraph_SaveLoadBool_Save_Out_41;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_41.Load_Out -= SubGraph_SaveLoadBool_Load_Out_41;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_41.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_41;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_46.Save_Out -= SubGraph_SaveLoadBool_Save_Out_46;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_46.Load_Out -= SubGraph_SaveLoadBool_Load_Out_46;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_46.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_46;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_70.Save_Out -= SubGraph_SaveLoadBool_Save_Out_70;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_70.Load_Out -= SubGraph_SaveLoadBool_Load_Out_70;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_70.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_70;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_71.Save_Out -= SubGraph_SaveLoadBool_Save_Out_71;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_71.Load_Out -= SubGraph_SaveLoadBool_Load_Out_71;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_71.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_71;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_114.Out -= SubGraph_AddMessageWithPadSupport_Out_114;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_114.Shown -= SubGraph_AddMessageWithPadSupport_Shown_114;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_122.Save_Out -= SubGraph_SaveLoadBool_Save_Out_122;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_122.Load_Out -= SubGraph_SaveLoadBool_Load_Out_122;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_122.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_122;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_200.Save_Out -= SubGraph_SaveLoadBool_Save_Out_200;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_200.Load_Out -= SubGraph_SaveLoadBool_Load_Out_200;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_200.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_200;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_201.Save_Out -= SubGraph_SaveLoadBool_Save_Out_201;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_201.Load_Out -= SubGraph_SaveLoadBool_Load_Out_201;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_201.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_201;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_210.Save_Out -= SubGraph_SaveLoadBool_Save_Out_210;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_210.Load_Out -= SubGraph_SaveLoadBool_Load_Out_210;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_210.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_210;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_267.Save_Out -= SubGraph_SaveLoadBool_Save_Out_267;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_267.Load_Out -= SubGraph_SaveLoadBool_Load_Out_267;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_267.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_267;
	}

	private void Instance_OnUpdate_19(object o, EventArgs e)
	{
		if (relayCallCount < 1000)
		{
			relayCallCount = 0;
		}
		Relay_OnUpdate_19();
	}

	private void Instance_OnSuspend_19(object o, EventArgs e)
	{
		if (relayCallCount < 1000)
		{
			relayCallCount = 0;
		}
		Relay_OnSuspend_19();
	}

	private void Instance_OnResume_19(object o, EventArgs e)
	{
		if (relayCallCount < 1000)
		{
			relayCallCount = 0;
		}
		Relay_OnResume_19();
	}

	private void Instance_SaveEvent_37(object o, EventArgs e)
	{
		if (relayCallCount < 1000)
		{
			relayCallCount = 0;
		}
		Relay_SaveEvent_37();
	}

	private void Instance_LoadEvent_37(object o, EventArgs e)
	{
		if (relayCallCount < 1000)
		{
			relayCallCount = 0;
		}
		Relay_LoadEvent_37();
	}

	private void Instance_RestartEvent_37(object o, EventArgs e)
	{
		if (relayCallCount < 1000)
		{
			relayCallCount = 0;
		}
		Relay_RestartEvent_37();
	}

	private void Instance_ResponseEvent_228(object o, uScript_MissionPromptBlock_OnResult.PromptResultEventArgs e)
	{
		if (relayCallCount < 1000)
		{
			relayCallCount = 0;
		}
		event_UnityEngine_GameObject_TankBlock_228 = e.TankBlock;
		event_UnityEngine_GameObject_Accepted_228 = e.Accepted;
		Relay_ResponseEvent_228();
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

	private void SubGraph_CompleteObjectiveStage_Out_8(object o, SubGraph_CompleteObjectiveStage.LogicEventArgs e)
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_8 = e.objectiveStage;
		local_Stage_System_Int32 = logic_SubGraph_CompleteObjectiveStage_objectiveStage_8;
		Relay_Out_8();
	}

	private void uScriptCon_ManualSwitch_Output1_16(object o, EventArgs e)
	{
		Relay_Output1_16();
	}

	private void uScriptCon_ManualSwitch_Output2_16(object o, EventArgs e)
	{
		Relay_Output2_16();
	}

	private void uScriptCon_ManualSwitch_Output3_16(object o, EventArgs e)
	{
		Relay_Output3_16();
	}

	private void uScriptCon_ManualSwitch_Output4_16(object o, EventArgs e)
	{
		Relay_Output4_16();
	}

	private void uScriptCon_ManualSwitch_Output5_16(object o, EventArgs e)
	{
		Relay_Output5_16();
	}

	private void uScriptCon_ManualSwitch_Output6_16(object o, EventArgs e)
	{
		Relay_Output6_16();
	}

	private void uScriptCon_ManualSwitch_Output7_16(object o, EventArgs e)
	{
		Relay_Output7_16();
	}

	private void uScriptCon_ManualSwitch_Output8_16(object o, EventArgs e)
	{
		Relay_Output8_16();
	}

	private void SubGraph_LoadObjectiveStates_Out_36(object o, SubGraph_LoadObjectiveStates.LogicEventArgs e)
	{
		Relay_Out_36();
	}

	private void SubGraph_SaveLoadBool_Save_Out_38(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_38 = e.boolean;
		local_Init_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_38;
		Relay_Save_Out_38();
	}

	private void SubGraph_SaveLoadBool_Load_Out_38(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_38 = e.boolean;
		local_Init_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_38;
		Relay_Load_Out_38();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_38(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_38 = e.boolean;
		local_Init_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_38;
		Relay_Restart_Out_38();
	}

	private void SubGraph_SaveLoadBool_Save_Out_41(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_41 = e.boolean;
		local_LicensePurchased_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_41;
		Relay_Save_Out_41();
	}

	private void SubGraph_SaveLoadBool_Load_Out_41(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_41 = e.boolean;
		local_LicensePurchased_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_41;
		Relay_Load_Out_41();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_41(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_41 = e.boolean;
		local_LicensePurchased_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_41;
		Relay_Restart_Out_41();
	}

	private void SubGraph_SaveLoadBool_Save_Out_46(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_46 = e.boolean;
		local_msg01Shown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_46;
		Relay_Save_Out_46();
	}

	private void SubGraph_SaveLoadBool_Load_Out_46(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_46 = e.boolean;
		local_msg01Shown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_46;
		Relay_Load_Out_46();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_46(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_46 = e.boolean;
		local_msg01Shown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_46;
		Relay_Restart_Out_46();
	}

	private void SubGraph_SaveLoadBool_Save_Out_70(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_70 = e.boolean;
		local_msg03aShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_70;
		Relay_Save_Out_70();
	}

	private void SubGraph_SaveLoadBool_Load_Out_70(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_70 = e.boolean;
		local_msg03aShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_70;
		Relay_Load_Out_70();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_70(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_70 = e.boolean;
		local_msg03aShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_70;
		Relay_Restart_Out_70();
	}

	private void SubGraph_SaveLoadBool_Save_Out_71(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_71 = e.boolean;
		local_msg03bShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_71;
		Relay_Save_Out_71();
	}

	private void SubGraph_SaveLoadBool_Load_Out_71(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_71 = e.boolean;
		local_msg03bShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_71;
		Relay_Load_Out_71();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_71(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_71 = e.boolean;
		local_msg03bShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_71;
		Relay_Restart_Out_71();
	}

	private void SubGraph_AddMessageWithPadSupport_Out_114(object o, SubGraph_AddMessageWithPadSupport.LogicEventArgs e)
	{
		logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboardReturn_114 = e.messageMouseKeyboardReturn;
		logic_SubGraph_AddMessageWithPadSupport_messageControlPadReturn_114 = e.messageControlPadReturn;
		local_Msg02ClickScreen_ManOnScreenMessages_OnScreenMessage = logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboardReturn_114;
		local_Msg02ClickScreen_ManOnScreenMessages_OnScreenMessage = logic_SubGraph_AddMessageWithPadSupport_messageControlPadReturn_114;
		Relay_Out_114();
	}

	private void SubGraph_AddMessageWithPadSupport_Shown_114(object o, SubGraph_AddMessageWithPadSupport.LogicEventArgs e)
	{
		logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboardReturn_114 = e.messageMouseKeyboardReturn;
		logic_SubGraph_AddMessageWithPadSupport_messageControlPadReturn_114 = e.messageControlPadReturn;
		local_Msg02ClickScreen_ManOnScreenMessages_OnScreenMessage = logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboardReturn_114;
		local_Msg02ClickScreen_ManOnScreenMessages_OnScreenMessage = logic_SubGraph_AddMessageWithPadSupport_messageControlPadReturn_114;
		Relay_Shown_114();
	}

	private void SubGraph_SaveLoadBool_Save_Out_122(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_122 = e.boolean;
		local_AngeredNPC_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_122;
		Relay_Save_Out_122();
	}

	private void SubGraph_SaveLoadBool_Load_Out_122(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_122 = e.boolean;
		local_AngeredNPC_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_122;
		Relay_Load_Out_122();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_122(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_122 = e.boolean;
		local_AngeredNPC_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_122;
		Relay_Restart_Out_122();
	}

	private void SubGraph_SaveLoadBool_Save_Out_200(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_200 = e.boolean;
		local_NearNPC_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_200;
		Relay_Save_Out_200();
	}

	private void SubGraph_SaveLoadBool_Load_Out_200(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_200 = e.boolean;
		local_NearNPC_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_200;
		Relay_Load_Out_200();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_200(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_200 = e.boolean;
		local_NearNPC_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_200;
		Relay_Restart_Out_200();
	}

	private void SubGraph_SaveLoadBool_Save_Out_201(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_201 = e.boolean;
		local_HasEnoughMoney_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_201;
		Relay_Save_Out_201();
	}

	private void SubGraph_SaveLoadBool_Load_Out_201(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_201 = e.boolean;
		local_HasEnoughMoney_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_201;
		Relay_Load_Out_201();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_201(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_201 = e.boolean;
		local_HasEnoughMoney_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_201;
		Relay_Restart_Out_201();
	}

	private void SubGraph_SaveLoadBool_Save_Out_210(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_210 = e.boolean;
		local_FallenOffBridge_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_210;
		Relay_Save_Out_210();
	}

	private void SubGraph_SaveLoadBool_Load_Out_210(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_210 = e.boolean;
		local_FallenOffBridge_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_210;
		Relay_Load_Out_210();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_210(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_210 = e.boolean;
		local_FallenOffBridge_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_210;
		Relay_Restart_Out_210();
	}

	private void SubGraph_SaveLoadBool_Save_Out_267(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_267 = e.boolean;
		local_HighlightBlock_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_267;
		Relay_Save_Out_267();
	}

	private void SubGraph_SaveLoadBool_Load_Out_267(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_267 = e.boolean;
		local_HighlightBlock_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_267;
		Relay_Load_Out_267();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_267(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_267 = e.boolean;
		local_HighlightBlock_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_267;
		Relay_Restart_Out_267();
	}

	private void Relay_Save_Out_0()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("99e1a38c-0f52-4898-a776-1e0343769fc1", "", Relay_Save_Out_0))
			{
				Relay_Save_38();
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at SubGraph_SaveLoadInt.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Load_Out_0()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("99e1a38c-0f52-4898-a776-1e0343769fc1", "", Relay_Load_Out_0))
			{
				Relay_Load_38();
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at SubGraph_SaveLoadInt.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Restart_Out_0()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("99e1a38c-0f52-4898-a776-1e0343769fc1", "", Relay_Restart_Out_0))
			{
				Relay_Set_False_38();
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at SubGraph_SaveLoadInt.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Save_0()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("99e1a38c-0f52-4898-a776-1e0343769fc1", "", Relay_Save_0))
			{
				logic_SubGraph_SaveLoadInt_integer_0 = local_Stage_System_Int32;
				logic_SubGraph_SaveLoadInt_intAsVariable_0 = local_Stage_System_Int32;
				logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_0.Save(logic_SubGraph_SaveLoadInt_restartValue_0, ref logic_SubGraph_SaveLoadInt_integer_0, logic_SubGraph_SaveLoadInt_intAsVariable_0, logic_SubGraph_SaveLoadInt_uniqueID_0);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at SubGraph_SaveLoadInt.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Load_0()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("99e1a38c-0f52-4898-a776-1e0343769fc1", "", Relay_Load_0))
			{
				logic_SubGraph_SaveLoadInt_integer_0 = local_Stage_System_Int32;
				logic_SubGraph_SaveLoadInt_intAsVariable_0 = local_Stage_System_Int32;
				logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_0.Load(logic_SubGraph_SaveLoadInt_restartValue_0, ref logic_SubGraph_SaveLoadInt_integer_0, logic_SubGraph_SaveLoadInt_intAsVariable_0, logic_SubGraph_SaveLoadInt_uniqueID_0);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at SubGraph_SaveLoadInt.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Restart_0()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("99e1a38c-0f52-4898-a776-1e0343769fc1", "", Relay_Restart_0))
			{
				logic_SubGraph_SaveLoadInt_integer_0 = local_Stage_System_Int32;
				logic_SubGraph_SaveLoadInt_intAsVariable_0 = local_Stage_System_Int32;
				logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_0.Restart(logic_SubGraph_SaveLoadInt_restartValue_0, ref logic_SubGraph_SaveLoadInt_integer_0, logic_SubGraph_SaveLoadInt_intAsVariable_0, logic_SubGraph_SaveLoadInt_uniqueID_0);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at SubGraph_SaveLoadInt.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_2()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("3af24faa-5be8-40eb-aefe-15ee257e5145", "Set_tank_invulnerable", Relay_In_2))
			{
				logic_uScript_SetTankInvulnerable_tank_2 = local_NPCTech_Tank;
				logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_2.In(logic_uScript_SetTankInvulnerable_invulnerable_2, logic_uScript_SetTankInvulnerable_tank_2);
				if (logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_2.Out)
				{
					Relay_In_5();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at Set tank invulnerable.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_5()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("d733e3b6-1d58-49c5-8a67-7ef0095d7307", "uScript_SetEncounterTarget", Relay_In_5))
			{
				logic_uScript_SetEncounterTarget_owner_5 = owner_Connection_7;
				logic_uScript_SetEncounterTarget_visibleObject_5 = local_NPCTech_Tank;
				logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_5.In(logic_uScript_SetEncounterTarget_owner_5, logic_uScript_SetEncounterTarget_visibleObject_5);
				if (logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_5.Out)
				{
					Relay_In_32();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at uScript_SetEncounterTarget.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Out_8()
	{
		if (relayCallCount++ < 1000)
		{
			CheckDebugBreak("3a78624b-1d7f-469c-9662-b1c066ed2c24", "SubGraph_CompleteObjectiveStage", Relay_Out_8);
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at SubGraph_CompleteObjectiveStage.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_8()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("3a78624b-1d7f-469c-9662-b1c066ed2c24", "SubGraph_CompleteObjectiveStage", Relay_In_8))
			{
				logic_SubGraph_CompleteObjectiveStage_objectiveStage_8 = local_Stage_System_Int32;
				logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_8.In(ref logic_SubGraph_CompleteObjectiveStage_objectiveStage_8, logic_SubGraph_CompleteObjectiveStage_isFinalObjective_8);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at SubGraph_CompleteObjectiveStage.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_10()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("3e42a16c-99eb-4cda-94c8-d3ae3a9de301", "Compare_Bool", Relay_In_10))
			{
				logic_uScriptCon_CompareBool_Bool_10 = local_Init_System_Boolean;
				logic_uScriptCon_CompareBool_uScriptCon_CompareBool_10.In(logic_uScriptCon_CompareBool_Bool_10);
				bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_10.True;
				bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_10.False;
				if (num)
				{
					Relay_In_155();
				}
				if (flag)
				{
					Relay_In_213();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at Compare Bool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_AtIndex_11()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("782adb72-0fd4-4ced-947e-9d0a2e0dac80", "uScript_AccessListTech", Relay_AtIndex_11))
			{
				int num = 0;
				Array array = local_NPCTechs_TankArray;
				if (logic_uScript_AccessListTech_techList_11.Length != num + array.Length)
				{
					Array.Resize(ref logic_uScript_AccessListTech_techList_11, num + array.Length);
				}
				Array.Copy(array, 0, logic_uScript_AccessListTech_techList_11, num, array.Length);
				num += array.Length;
				logic_uScript_AccessListTech_uScript_AccessListTech_11.AtIndex(ref logic_uScript_AccessListTech_techList_11, logic_uScript_AccessListTech_index_11, out logic_uScript_AccessListTech_value_11);
				local_NPCTechs_TankArray = logic_uScript_AccessListTech_techList_11;
				local_NPCTech_Tank = logic_uScript_AccessListTech_value_11;
				if (logic_uScript_AccessListTech_uScript_AccessListTech_11.Out)
				{
					Relay_In_128();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at uScript_AccessListTech.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_12()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("198922b9-cad9-451a-a6c3-97e4549fe5d0", "uScript_AddMessage", Relay_In_12))
			{
				logic_uScript_AddMessage_messageData_12 = msg05LicensePurchased;
				logic_uScript_AddMessage_speaker_12 = messageSpeaker;
				logic_uScript_AddMessage_Return_12 = logic_uScript_AddMessage_uScript_AddMessage_12.In(logic_uScript_AddMessage_messageData_12, logic_uScript_AddMessage_speaker_12);
				if (logic_uScript_AddMessage_uScript_AddMessage_12.Shown)
				{
					Relay_In_47();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at uScript_AddMessage.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Output1_16()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("ba26ce34-7707-460a-a2b1-3710b7346a87", "Manual_Switch", Relay_Output1_16))
			{
				Relay_In_8();
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at Manual Switch.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Output2_16()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("ba26ce34-7707-460a-a2b1-3710b7346a87", "Manual_Switch", Relay_Output2_16))
			{
				Relay_In_44();
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at Manual Switch.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Output3_16()
	{
		if (relayCallCount++ < 1000)
		{
			CheckDebugBreak("ba26ce34-7707-460a-a2b1-3710b7346a87", "Manual_Switch", Relay_Output3_16);
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at Manual Switch.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Output4_16()
	{
		if (relayCallCount++ < 1000)
		{
			CheckDebugBreak("ba26ce34-7707-460a-a2b1-3710b7346a87", "Manual_Switch", Relay_Output4_16);
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at Manual Switch.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Output5_16()
	{
		if (relayCallCount++ < 1000)
		{
			CheckDebugBreak("ba26ce34-7707-460a-a2b1-3710b7346a87", "Manual_Switch", Relay_Output5_16);
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at Manual Switch.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Output6_16()
	{
		if (relayCallCount++ < 1000)
		{
			CheckDebugBreak("ba26ce34-7707-460a-a2b1-3710b7346a87", "Manual_Switch", Relay_Output6_16);
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at Manual Switch.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Output7_16()
	{
		if (relayCallCount++ < 1000)
		{
			CheckDebugBreak("ba26ce34-7707-460a-a2b1-3710b7346a87", "Manual_Switch", Relay_Output7_16);
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at Manual Switch.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Output8_16()
	{
		if (relayCallCount++ < 1000)
		{
			CheckDebugBreak("ba26ce34-7707-460a-a2b1-3710b7346a87", "Manual_Switch", Relay_Output8_16);
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at Manual Switch.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_16()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("ba26ce34-7707-460a-a2b1-3710b7346a87", "Manual_Switch", Relay_In_16))
			{
				logic_uScriptCon_ManualSwitch_CurrentOutput_16 = local_Stage_System_Int32;
				logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_16.In(logic_uScriptCon_ManualSwitch_CurrentOutput_16);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at Manual Switch.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_OnUpdate_19()
	{
		if (!CheckDebugBreak("908c5051-e398-4a52-93ad-addfa5bfc9ca", "Encounter_Update", Relay_OnUpdate_19))
		{
			Relay_In_10();
		}
	}

	private void Relay_OnSuspend_19()
	{
		CheckDebugBreak("908c5051-e398-4a52-93ad-addfa5bfc9ca", "Encounter_Update", Relay_OnSuspend_19);
	}

	private void Relay_OnResume_19()
	{
		CheckDebugBreak("908c5051-e398-4a52-93ad-addfa5bfc9ca", "Encounter_Update", Relay_OnResume_19);
	}

	private void Relay_In_24()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("4d85f99e-026e-4269-b7fc-3cdd5daeb618", "uScript_GetAndCheckTechs", Relay_In_24))
			{
				int num = 0;
				Array nPCSpawnData = NPCSpawnData;
				if (logic_uScript_GetAndCheckTechs_techData_24.Length != num + nPCSpawnData.Length)
				{
					Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_24, num + nPCSpawnData.Length);
				}
				Array.Copy(nPCSpawnData, 0, logic_uScript_GetAndCheckTechs_techData_24, num, nPCSpawnData.Length);
				num += nPCSpawnData.Length;
				logic_uScript_GetAndCheckTechs_ownerNode_24 = owner_Connection_17;
				int num2 = 0;
				Array array = local_NPCTechs_TankArray;
				if (logic_uScript_GetAndCheckTechs_techs_24.Length != num2 + array.Length)
				{
					Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_24, num2 + array.Length);
				}
				Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techs_24, num2, array.Length);
				num2 += array.Length;
				logic_uScript_GetAndCheckTechs_Return_24 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_24.In(logic_uScript_GetAndCheckTechs_techData_24, logic_uScript_GetAndCheckTechs_ownerNode_24, ref logic_uScript_GetAndCheckTechs_techs_24);
				local_NPCTechs_TankArray = logic_uScript_GetAndCheckTechs_techs_24;
				bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_24.AllAlive;
				bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_24.SomeAlive;
				bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_24.AllDead;
				if (allAlive)
				{
					Relay_AtIndex_11();
				}
				if (someAlive)
				{
					Relay_AtIndex_11();
				}
				if (allDead)
				{
					Relay_In_149();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at uScript_GetAndCheckTechs.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Succeed_25()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("f222adee-c8e9-4f30-b212-adc6cd01ed5d", "uScript_FinishEncounter", Relay_Succeed_25))
			{
				logic_uScript_FinishEncounter_owner_25 = owner_Connection_31;
				logic_uScript_FinishEncounter_uScript_FinishEncounter_25.Succeed(logic_uScript_FinishEncounter_owner_25);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at uScript_FinishEncounter.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Fail_25()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("f222adee-c8e9-4f30-b212-adc6cd01ed5d", "uScript_FinishEncounter", Relay_Fail_25))
			{
				logic_uScript_FinishEncounter_owner_25 = owner_Connection_31;
				logic_uScript_FinishEncounter_uScript_FinishEncounter_25.Fail(logic_uScript_FinishEncounter_owner_25);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at uScript_FinishEncounter.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_InitialSpawn_27()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("ebc8635f-d3ca-44a8-8c19-967766254dff", "uScript_SpawnTechsFromData", Relay_InitialSpawn_27))
			{
				int num = 0;
				Array nPCSpawnData = NPCSpawnData;
				if (logic_uScript_SpawnTechsFromData_spawnData_27.Length != num + nPCSpawnData.Length)
				{
					Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_27, num + nPCSpawnData.Length);
				}
				Array.Copy(nPCSpawnData, 0, logic_uScript_SpawnTechsFromData_spawnData_27, num, nPCSpawnData.Length);
				num += nPCSpawnData.Length;
				logic_uScript_SpawnTechsFromData_ownerNode_27 = owner_Connection_18;
				logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_27.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_27, logic_uScript_SpawnTechsFromData_ownerNode_27, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_27, logic_uScript_SpawnTechsFromData_allowResurrection_27);
				if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_27.Out)
				{
					Relay_True_214();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at uScript_SpawnTechsFromData.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_29()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("5e197823-6db9-4cf9-b79e-719c304eae33", "uScript_ClearOnScreenMessagesWithTag", Relay_In_29))
			{
				logic_uScript_ClearOnScreenMessagesWithTag_tag_29 = clearMsgTagWhenAngry;
				logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_29.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_29, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_29);
				if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_29.Out)
				{
					Relay_In_138();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at uScript_ClearOnScreenMessagesWithTag.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_32()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("52e6c390-e3b3-4463-b679-9efbc68e5ca7", "Distance_Is_player_in_range_of_tech", Relay_In_32))
			{
				logic_uScript_IsPlayerInRangeOfTech_tech_32 = local_NPCTech_Tank;
				logic_uScript_IsPlayerInRangeOfTech_range_32 = distNPCFound;
				logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_32.In(logic_uScript_IsPlayerInRangeOfTech_tech_32, logic_uScript_IsPlayerInRangeOfTech_range_32, logic_uScript_IsPlayerInRangeOfTech_techs_32);
				bool inRange = logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_32.InRange;
				bool outOfRange = logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_32.OutOfRange;
				if (inRange)
				{
					Relay_In_124();
				}
				if (outOfRange)
				{
					Relay_UnPause_196();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at Distance/Is player in range of tech.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_True_33()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("d3919fbd-dd62-4365-9890-885317844744", "Set_Bool", Relay_True_33))
			{
				logic_uScriptAct_SetBool_uScriptAct_SetBool_33.True(out logic_uScriptAct_SetBool_Target_33);
				local_NearNPC_System_Boolean = logic_uScriptAct_SetBool_Target_33;
				if (logic_uScriptAct_SetBool_uScriptAct_SetBool_33.Out)
				{
					Relay_In_252();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at Set Bool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_False_33()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("d3919fbd-dd62-4365-9890-885317844744", "Set_Bool", Relay_False_33))
			{
				logic_uScriptAct_SetBool_uScriptAct_SetBool_33.False(out logic_uScriptAct_SetBool_Target_33);
				local_NearNPC_System_Boolean = logic_uScriptAct_SetBool_Target_33;
				if (logic_uScriptAct_SetBool_uScriptAct_SetBool_33.Out)
				{
					Relay_In_252();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at Set Bool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Out_36()
	{
		if (relayCallCount++ < 1000)
		{
			CheckDebugBreak("b3dbc796-b259-4e0d-a301-ef9137f79150", "SubGraph_LoadObjectiveStates", Relay_Out_36);
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at SubGraph_LoadObjectiveStates.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_36()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("b3dbc796-b259-4e0d-a301-ef9137f79150", "SubGraph_LoadObjectiveStates", Relay_In_36))
			{
				logic_SubGraph_LoadObjectiveStates_currentObjective_36 = local_Stage_System_Int32;
				logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_36.In(logic_SubGraph_LoadObjectiveStates_currentObjective_36);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at SubGraph_LoadObjectiveStates.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_SaveEvent_37()
	{
		if (!CheckDebugBreak("02af2480-79d4-42e6-b009-60da1db38643", "uScript_SaveLoad", Relay_SaveEvent_37))
		{
			Relay_Save_0();
		}
	}

	private void Relay_LoadEvent_37()
	{
		if (!CheckDebugBreak("02af2480-79d4-42e6-b009-60da1db38643", "uScript_SaveLoad", Relay_LoadEvent_37))
		{
			Relay_Load_0();
		}
	}

	private void Relay_RestartEvent_37()
	{
		if (!CheckDebugBreak("02af2480-79d4-42e6-b009-60da1db38643", "uScript_SaveLoad", Relay_RestartEvent_37))
		{
			Relay_Restart_0();
		}
	}

	private void Relay_Save_Out_38()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("b233383b-8cba-4e0f-9658-96e6673d37b1", "", Relay_Save_Out_38))
			{
				Relay_Save_46();
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Load_Out_38()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("b233383b-8cba-4e0f-9658-96e6673d37b1", "", Relay_Load_Out_38))
			{
				Relay_Load_46();
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Restart_Out_38()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("b233383b-8cba-4e0f-9658-96e6673d37b1", "", Relay_Restart_Out_38))
			{
				Relay_Set_False_46();
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Save_38()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("b233383b-8cba-4e0f-9658-96e6673d37b1", "", Relay_Save_38))
			{
				logic_SubGraph_SaveLoadBool_boolean_38 = local_Init_System_Boolean;
				logic_SubGraph_SaveLoadBool_boolAsVariable_38 = local_Init_System_Boolean;
				logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_38.Save(ref logic_SubGraph_SaveLoadBool_boolean_38, logic_SubGraph_SaveLoadBool_boolAsVariable_38, logic_SubGraph_SaveLoadBool_uniqueID_38);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Load_38()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("b233383b-8cba-4e0f-9658-96e6673d37b1", "", Relay_Load_38))
			{
				logic_SubGraph_SaveLoadBool_boolean_38 = local_Init_System_Boolean;
				logic_SubGraph_SaveLoadBool_boolAsVariable_38 = local_Init_System_Boolean;
				logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_38.Load(ref logic_SubGraph_SaveLoadBool_boolean_38, logic_SubGraph_SaveLoadBool_boolAsVariable_38, logic_SubGraph_SaveLoadBool_uniqueID_38);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Set_True_38()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("b233383b-8cba-4e0f-9658-96e6673d37b1", "", Relay_Set_True_38))
			{
				logic_SubGraph_SaveLoadBool_boolean_38 = local_Init_System_Boolean;
				logic_SubGraph_SaveLoadBool_boolAsVariable_38 = local_Init_System_Boolean;
				logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_38.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_38, logic_SubGraph_SaveLoadBool_boolAsVariable_38, logic_SubGraph_SaveLoadBool_uniqueID_38);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Set_False_38()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("b233383b-8cba-4e0f-9658-96e6673d37b1", "", Relay_Set_False_38))
			{
				logic_SubGraph_SaveLoadBool_boolean_38 = local_Init_System_Boolean;
				logic_SubGraph_SaveLoadBool_boolAsVariable_38 = local_Init_System_Boolean;
				logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_38.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_38, logic_SubGraph_SaveLoadBool_boolAsVariable_38, logic_SubGraph_SaveLoadBool_uniqueID_38);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Save_Out_41()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("910e636f-dfe3-41c3-968e-2282238b8c82", "", Relay_Save_Out_41))
			{
				Relay_Save_122();
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Load_Out_41()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("910e636f-dfe3-41c3-968e-2282238b8c82", "", Relay_Load_Out_41))
			{
				Relay_Load_122();
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Restart_Out_41()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("910e636f-dfe3-41c3-968e-2282238b8c82", "", Relay_Restart_Out_41))
			{
				Relay_Set_False_122();
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Save_41()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("910e636f-dfe3-41c3-968e-2282238b8c82", "", Relay_Save_41))
			{
				logic_SubGraph_SaveLoadBool_boolean_41 = local_LicensePurchased_System_Boolean;
				logic_SubGraph_SaveLoadBool_boolAsVariable_41 = local_LicensePurchased_System_Boolean;
				logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_41.Save(ref logic_SubGraph_SaveLoadBool_boolean_41, logic_SubGraph_SaveLoadBool_boolAsVariable_41, logic_SubGraph_SaveLoadBool_uniqueID_41);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Load_41()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("910e636f-dfe3-41c3-968e-2282238b8c82", "", Relay_Load_41))
			{
				logic_SubGraph_SaveLoadBool_boolean_41 = local_LicensePurchased_System_Boolean;
				logic_SubGraph_SaveLoadBool_boolAsVariable_41 = local_LicensePurchased_System_Boolean;
				logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_41.Load(ref logic_SubGraph_SaveLoadBool_boolean_41, logic_SubGraph_SaveLoadBool_boolAsVariable_41, logic_SubGraph_SaveLoadBool_uniqueID_41);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Set_True_41()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("910e636f-dfe3-41c3-968e-2282238b8c82", "", Relay_Set_True_41))
			{
				logic_SubGraph_SaveLoadBool_boolean_41 = local_LicensePurchased_System_Boolean;
				logic_SubGraph_SaveLoadBool_boolAsVariable_41 = local_LicensePurchased_System_Boolean;
				logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_41.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_41, logic_SubGraph_SaveLoadBool_boolAsVariable_41, logic_SubGraph_SaveLoadBool_uniqueID_41);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Set_False_41()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("910e636f-dfe3-41c3-968e-2282238b8c82", "", Relay_Set_False_41))
			{
				logic_SubGraph_SaveLoadBool_boolean_41 = local_LicensePurchased_System_Boolean;
				logic_SubGraph_SaveLoadBool_boolAsVariable_41 = local_LicensePurchased_System_Boolean;
				logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_41.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_41, logic_SubGraph_SaveLoadBool_boolAsVariable_41, logic_SubGraph_SaveLoadBool_uniqueID_41);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_44()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("339bf64c-a5f8-4175-88cc-4230d8518c9f", "Compare_Bool", Relay_In_44))
			{
				logic_uScriptCon_CompareBool_Bool_44 = local_LicensePurchased_System_Boolean;
				logic_uScriptCon_CompareBool_uScriptCon_CompareBool_44.In(logic_uScriptCon_CompareBool_Bool_44);
				bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_44.True;
				bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_44.False;
				if (num)
				{
					Relay_In_142();
				}
				if (flag)
				{
					Relay_In_93();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at Compare Bool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Save_Out_46()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("0809ce17-e25c-4deb-bed8-26130e125cb5", "", Relay_Save_Out_46))
			{
				Relay_Save_70();
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Load_Out_46()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("0809ce17-e25c-4deb-bed8-26130e125cb5", "", Relay_Load_Out_46))
			{
				Relay_Load_70();
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Restart_Out_46()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("0809ce17-e25c-4deb-bed8-26130e125cb5", "", Relay_Restart_Out_46))
			{
				Relay_Set_False_70();
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Save_46()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("0809ce17-e25c-4deb-bed8-26130e125cb5", "", Relay_Save_46))
			{
				logic_SubGraph_SaveLoadBool_boolean_46 = local_msg01Shown_System_Boolean;
				logic_SubGraph_SaveLoadBool_boolAsVariable_46 = local_msg01Shown_System_Boolean;
				logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_46.Save(ref logic_SubGraph_SaveLoadBool_boolean_46, logic_SubGraph_SaveLoadBool_boolAsVariable_46, logic_SubGraph_SaveLoadBool_uniqueID_46);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Load_46()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("0809ce17-e25c-4deb-bed8-26130e125cb5", "", Relay_Load_46))
			{
				logic_SubGraph_SaveLoadBool_boolean_46 = local_msg01Shown_System_Boolean;
				logic_SubGraph_SaveLoadBool_boolAsVariable_46 = local_msg01Shown_System_Boolean;
				logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_46.Load(ref logic_SubGraph_SaveLoadBool_boolean_46, logic_SubGraph_SaveLoadBool_boolAsVariable_46, logic_SubGraph_SaveLoadBool_uniqueID_46);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Set_True_46()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("0809ce17-e25c-4deb-bed8-26130e125cb5", "", Relay_Set_True_46))
			{
				logic_SubGraph_SaveLoadBool_boolean_46 = local_msg01Shown_System_Boolean;
				logic_SubGraph_SaveLoadBool_boolAsVariable_46 = local_msg01Shown_System_Boolean;
				logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_46.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_46, logic_SubGraph_SaveLoadBool_boolAsVariable_46, logic_SubGraph_SaveLoadBool_uniqueID_46);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Set_False_46()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("0809ce17-e25c-4deb-bed8-26130e125cb5", "", Relay_Set_False_46))
			{
				logic_SubGraph_SaveLoadBool_boolean_46 = local_msg01Shown_System_Boolean;
				logic_SubGraph_SaveLoadBool_boolAsVariable_46 = local_msg01Shown_System_Boolean;
				logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_46.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_46, logic_SubGraph_SaveLoadBool_boolAsVariable_46, logic_SubGraph_SaveLoadBool_uniqueID_46);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_47()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("042cf08c-9b5a-4fa7-8ab3-26ce57a9369e", "uScript_FlyTechUpAndAway", Relay_In_47))
			{
				logic_uScript_FlyTechUpAndAway_tech_47 = local_NPCTech_Tank;
				logic_uScript_FlyTechUpAndAway_aiTree_47 = NPCFlyAwayAI;
				logic_uScript_FlyTechUpAndAway_removalParticles_47 = NPCDespawnParticleEffect;
				logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_47.In(logic_uScript_FlyTechUpAndAway_tech_47, logic_uScript_FlyTechUpAndAway_maxLifetime_47, logic_uScript_FlyTechUpAndAway_targetHeight_47, logic_uScript_FlyTechUpAndAway_aiTree_47, logic_uScript_FlyTechUpAndAway_removalParticles_47);
				if (logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_47.Out)
				{
					Relay_In_163();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at uScript_FlyTechUpAndAway.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_51()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("f9c9abfd-409c-4b8b-a10f-975ac07a8a2e", "uScript_AddMessage", Relay_In_51))
			{
				logic_uScript_AddMessage_messageData_51 = msg01Intro;
				logic_uScript_AddMessage_speaker_51 = messageSpeaker;
				logic_uScript_AddMessage_Return_51 = logic_uScript_AddMessage_uScript_AddMessage_51.In(logic_uScript_AddMessage_messageData_51, logic_uScript_AddMessage_speaker_51);
				if (logic_uScript_AddMessage_uScript_AddMessage_51.Shown)
				{
					Relay_True_57();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at uScript_AddMessage.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_55()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("540f8aec-0c5a-4101-9ffc-92a6cd45a40e", "Compare_Bool", Relay_In_55))
			{
				logic_uScriptCon_CompareBool_Bool_55 = local_msg01Shown_System_Boolean;
				logic_uScriptCon_CompareBool_uScriptCon_CompareBool_55.In(logic_uScriptCon_CompareBool_Bool_55);
				bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_55.True;
				bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_55.False;
				if (num)
				{
					Relay_In_114();
				}
				if (flag)
				{
					Relay_In_51();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at Compare Bool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_True_57()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("6a199c7f-5eb1-4b90-bb9c-c5c98b3f399b", "Set_Bool", Relay_True_57))
			{
				logic_uScriptAct_SetBool_uScriptAct_SetBool_57.True(out logic_uScriptAct_SetBool_Target_57);
				local_msg01Shown_System_Boolean = logic_uScriptAct_SetBool_Target_57;
				if (logic_uScriptAct_SetBool_uScriptAct_SetBool_57.Out)
				{
					Relay_In_114();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at Set Bool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_False_57()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("6a199c7f-5eb1-4b90-bb9c-c5c98b3f399b", "Set_Bool", Relay_False_57))
			{
				logic_uScriptAct_SetBool_uScriptAct_SetBool_57.False(out logic_uScriptAct_SetBool_Target_57);
				local_msg01Shown_System_Boolean = logic_uScriptAct_SetBool_Target_57;
				if (logic_uScriptAct_SetBool_uScriptAct_SetBool_57.Out)
				{
					Relay_In_114();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at Set Bool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_58()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("c31689d8-f6f6-4198-a39c-6907c68d09d9", "uScript_GetCurrentMoneyEarned", Relay_In_58))
			{
				logic_uScript_GetCurrentMoneyEarned_Return_58 = logic_uScript_GetCurrentMoneyEarned_uScript_GetCurrentMoneyEarned_58.In();
				local_CurrentMoney_System_Int32 = logic_uScript_GetCurrentMoneyEarned_Return_58;
				if (logic_uScript_GetCurrentMoneyEarned_uScript_GetCurrentMoneyEarned_58.Out)
				{
					Relay_In_60();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at uScript_GetCurrentMoneyEarned.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_60()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("c656dd25-b2e3-4a4a-9c0f-eef18fdd2108", "Compare_Int", Relay_In_60))
			{
				logic_uScriptCon_CompareInt_A_60 = local_CurrentMoney_System_Int32;
				logic_uScriptCon_CompareInt_B_60 = LicenseCost;
				logic_uScriptCon_CompareInt_uScriptCon_CompareInt_60.In(logic_uScriptCon_CompareInt_A_60, logic_uScriptCon_CompareInt_B_60);
				bool greaterThanOrEqualTo = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_60.GreaterThanOrEqualTo;
				bool lessThan = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_60.LessThan;
				if (greaterThanOrEqualTo)
				{
					Relay_In_243();
				}
				if (lessThan)
				{
					Relay_True_270();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at Compare Int.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_True_66()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("fbdc8cc5-10ed-4630-b640-c907271af87f", "Set_Bool", Relay_True_66))
			{
				logic_uScriptAct_SetBool_uScriptAct_SetBool_66.True(out logic_uScriptAct_SetBool_Target_66);
				local_WaitingOnPrompt_System_Boolean = logic_uScriptAct_SetBool_Target_66;
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at Set Bool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_False_66()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("fbdc8cc5-10ed-4630-b640-c907271af87f", "Set_Bool", Relay_False_66))
			{
				logic_uScriptAct_SetBool_uScriptAct_SetBool_66.False(out logic_uScriptAct_SetBool_Target_66);
				local_WaitingOnPrompt_System_Boolean = logic_uScriptAct_SetBool_Target_66;
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at Set Bool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Save_Out_70()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("d60e00b3-a37a-4497-b0e4-043f7f920697", "", Relay_Save_Out_70))
			{
				Relay_Save_71();
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Load_Out_70()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("d60e00b3-a37a-4497-b0e4-043f7f920697", "", Relay_Load_Out_70))
			{
				Relay_Load_71();
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Restart_Out_70()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("d60e00b3-a37a-4497-b0e4-043f7f920697", "", Relay_Restart_Out_70))
			{
				Relay_Set_False_71();
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Save_70()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("d60e00b3-a37a-4497-b0e4-043f7f920697", "", Relay_Save_70))
			{
				logic_SubGraph_SaveLoadBool_boolean_70 = local_msg03aShown_System_Boolean;
				logic_SubGraph_SaveLoadBool_boolAsVariable_70 = local_msg03aShown_System_Boolean;
				logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_70.Save(ref logic_SubGraph_SaveLoadBool_boolean_70, logic_SubGraph_SaveLoadBool_boolAsVariable_70, logic_SubGraph_SaveLoadBool_uniqueID_70);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Load_70()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("d60e00b3-a37a-4497-b0e4-043f7f920697", "", Relay_Load_70))
			{
				logic_SubGraph_SaveLoadBool_boolean_70 = local_msg03aShown_System_Boolean;
				logic_SubGraph_SaveLoadBool_boolAsVariable_70 = local_msg03aShown_System_Boolean;
				logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_70.Load(ref logic_SubGraph_SaveLoadBool_boolean_70, logic_SubGraph_SaveLoadBool_boolAsVariable_70, logic_SubGraph_SaveLoadBool_uniqueID_70);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Set_True_70()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("d60e00b3-a37a-4497-b0e4-043f7f920697", "", Relay_Set_True_70))
			{
				logic_SubGraph_SaveLoadBool_boolean_70 = local_msg03aShown_System_Boolean;
				logic_SubGraph_SaveLoadBool_boolAsVariable_70 = local_msg03aShown_System_Boolean;
				logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_70.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_70, logic_SubGraph_SaveLoadBool_boolAsVariable_70, logic_SubGraph_SaveLoadBool_uniqueID_70);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Set_False_70()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("d60e00b3-a37a-4497-b0e4-043f7f920697", "", Relay_Set_False_70))
			{
				logic_SubGraph_SaveLoadBool_boolean_70 = local_msg03aShown_System_Boolean;
				logic_SubGraph_SaveLoadBool_boolAsVariable_70 = local_msg03aShown_System_Boolean;
				logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_70.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_70, logic_SubGraph_SaveLoadBool_boolAsVariable_70, logic_SubGraph_SaveLoadBool_uniqueID_70);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Save_Out_71()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("eea97f4d-d68e-404d-87f0-523b40e1ff70", "", Relay_Save_Out_71))
			{
				Relay_Save_41();
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Load_Out_71()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("eea97f4d-d68e-404d-87f0-523b40e1ff70", "", Relay_Load_Out_71))
			{
				Relay_Load_41();
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Restart_Out_71()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("eea97f4d-d68e-404d-87f0-523b40e1ff70", "", Relay_Restart_Out_71))
			{
				Relay_Set_False_41();
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Save_71()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("eea97f4d-d68e-404d-87f0-523b40e1ff70", "", Relay_Save_71))
			{
				logic_SubGraph_SaveLoadBool_boolean_71 = local_msg03bShown_System_Boolean;
				logic_SubGraph_SaveLoadBool_boolAsVariable_71 = local_msg03bShown_System_Boolean;
				logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_71.Save(ref logic_SubGraph_SaveLoadBool_boolean_71, logic_SubGraph_SaveLoadBool_boolAsVariable_71, logic_SubGraph_SaveLoadBool_uniqueID_71);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Load_71()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("eea97f4d-d68e-404d-87f0-523b40e1ff70", "", Relay_Load_71))
			{
				logic_SubGraph_SaveLoadBool_boolean_71 = local_msg03bShown_System_Boolean;
				logic_SubGraph_SaveLoadBool_boolAsVariable_71 = local_msg03bShown_System_Boolean;
				logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_71.Load(ref logic_SubGraph_SaveLoadBool_boolean_71, logic_SubGraph_SaveLoadBool_boolAsVariable_71, logic_SubGraph_SaveLoadBool_uniqueID_71);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Set_True_71()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("eea97f4d-d68e-404d-87f0-523b40e1ff70", "", Relay_Set_True_71))
			{
				logic_SubGraph_SaveLoadBool_boolean_71 = local_msg03bShown_System_Boolean;
				logic_SubGraph_SaveLoadBool_boolAsVariable_71 = local_msg03bShown_System_Boolean;
				logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_71.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_71, logic_SubGraph_SaveLoadBool_boolAsVariable_71, logic_SubGraph_SaveLoadBool_uniqueID_71);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Set_False_71()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("eea97f4d-d68e-404d-87f0-523b40e1ff70", "", Relay_Set_False_71))
			{
				logic_SubGraph_SaveLoadBool_boolean_71 = local_msg03bShown_System_Boolean;
				logic_SubGraph_SaveLoadBool_boolAsVariable_71 = local_msg03bShown_System_Boolean;
				logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_71.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_71, logic_SubGraph_SaveLoadBool_boolAsVariable_71, logic_SubGraph_SaveLoadBool_uniqueID_71);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_True_73()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("fe87377f-aa35-412c-bd6e-62816addacee", "Set_Bool", Relay_True_73))
			{
				logic_uScriptAct_SetBool_uScriptAct_SetBool_73.True(out logic_uScriptAct_SetBool_Target_73);
				local_HasEnoughMoney_System_Boolean = logic_uScriptAct_SetBool_Target_73;
				if (logic_uScriptAct_SetBool_uScriptAct_SetBool_73.Out)
				{
					Relay_True_66();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at Set Bool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_False_73()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("fe87377f-aa35-412c-bd6e-62816addacee", "Set_Bool", Relay_False_73))
			{
				logic_uScriptAct_SetBool_uScriptAct_SetBool_73.False(out logic_uScriptAct_SetBool_Target_73);
				local_HasEnoughMoney_System_Boolean = logic_uScriptAct_SetBool_Target_73;
				if (logic_uScriptAct_SetBool_uScriptAct_SetBool_73.Out)
				{
					Relay_True_66();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at Set Bool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_75()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("11335cb8-8921-4b38-b6d7-26d8234726b6", "Compare_Bool", Relay_In_75))
			{
				logic_uScriptCon_CompareBool_Bool_75 = local_HasEnoughMoney_System_Boolean;
				logic_uScriptCon_CompareBool_uScriptCon_CompareBool_75.In(logic_uScriptCon_CompareBool_Bool_75);
				bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_75.True;
				bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_75.False;
				if (num)
				{
					Relay_In_89();
				}
				if (flag)
				{
					Relay_In_100();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at Compare Bool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_78()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("8bbd0d51-d034-4292-ae5f-482d0bbac0d3", "uScript_AddMessage", Relay_In_78))
			{
				logic_uScript_AddMessage_messageData_78 = msg03bNotEnoughMoney;
				logic_uScript_AddMessage_speaker_78 = messageSpeaker;
				logic_uScript_AddMessage_Return_78 = logic_uScript_AddMessage_uScript_AddMessage_78.In(logic_uScript_AddMessage_messageData_78, logic_uScript_AddMessage_speaker_78);
				if (logic_uScript_AddMessage_uScript_AddMessage_78.Shown)
				{
					Relay_True_102();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at uScript_AddMessage.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_82()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("f8903fa4-93df-47e8-9e75-73f477be1a59", "uScript_AddMessage", Relay_In_82))
			{
				logic_uScript_AddMessage_messageData_82 = msg03aPurchaseDeclined;
				logic_uScript_AddMessage_speaker_82 = messageSpeaker;
				logic_uScript_AddMessage_Return_82 = logic_uScript_AddMessage_uScript_AddMessage_82.In(logic_uScript_AddMessage_messageData_82, logic_uScript_AddMessage_speaker_82);
				if (logic_uScript_AddMessage_uScript_AddMessage_82.Shown)
				{
					Relay_True_98();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at uScript_AddMessage.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_85()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("72403299-9b6e-4998-874b-ffd47f3b4b06", "uScript_AddMoney", Relay_In_85))
			{
				logic_uScript_AddMoney_amount_85 = local_87_System_Int32;
				logic_uScript_AddMoney_uScript_AddMoney_85.In(logic_uScript_AddMoney_amount_85);
				if (logic_uScript_AddMoney_uScript_AddMoney_85.Out)
				{
					Relay_True_88();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at uScript_AddMoney.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_True_88()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("ed54524c-0bea-4dc0-a53c-f18f0e2b1784", "Set_Bool", Relay_True_88))
			{
				logic_uScriptAct_SetBool_uScriptAct_SetBool_88.True(out logic_uScriptAct_SetBool_Target_88);
				local_LicensePurchased_System_Boolean = logic_uScriptAct_SetBool_Target_88;
				if (logic_uScriptAct_SetBool_uScriptAct_SetBool_88.Out)
				{
					Relay_False_262();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at Set Bool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_False_88()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("ed54524c-0bea-4dc0-a53c-f18f0e2b1784", "Set_Bool", Relay_False_88))
			{
				logic_uScriptAct_SetBool_uScriptAct_SetBool_88.False(out logic_uScriptAct_SetBool_Target_88);
				local_LicensePurchased_System_Boolean = logic_uScriptAct_SetBool_Target_88;
				if (logic_uScriptAct_SetBool_uScriptAct_SetBool_88.Out)
				{
					Relay_False_262();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at Set Bool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_89()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("e8f42c68-db2a-4dad-8925-cf548c1f5bba", "Multiply_Int", Relay_In_89))
			{
				logic_uScriptAct_MultiplyInt_v2_A_89 = LicenseCost;
				logic_uScriptAct_MultiplyInt_v2_uScriptAct_MultiplyInt_v2_89.In(logic_uScriptAct_MultiplyInt_v2_A_89, logic_uScriptAct_MultiplyInt_v2_B_89, out logic_uScriptAct_MultiplyInt_v2_IntResult_89, out logic_uScriptAct_MultiplyInt_v2_FloatResult_89);
				local_87_System_Int32 = logic_uScriptAct_MultiplyInt_v2_IntResult_89;
				if (logic_uScriptAct_MultiplyInt_v2_uScriptAct_MultiplyInt_v2_89.Out)
				{
					Relay_In_85();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at Multiply Int.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_True_90()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("f953c472-e955-4b0b-8cdf-84b26a386909", "Set_Bool", Relay_True_90))
			{
				logic_uScriptAct_SetBool_uScriptAct_SetBool_90.True(out logic_uScriptAct_SetBool_Target_90);
				local_WaitingOnPrompt_System_Boolean = logic_uScriptAct_SetBool_Target_90;
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at Set Bool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_False_90()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("f953c472-e955-4b0b-8cdf-84b26a386909", "Set_Bool", Relay_False_90))
			{
				logic_uScriptAct_SetBool_uScriptAct_SetBool_90.False(out logic_uScriptAct_SetBool_Target_90);
				local_WaitingOnPrompt_System_Boolean = logic_uScriptAct_SetBool_Target_90;
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at Set Bool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_93()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("1b805e04-cc3a-4977-815b-62d959b46b03", "Compare_Bool", Relay_In_93))
			{
				logic_uScriptCon_CompareBool_Bool_93 = local_WaitingOnPrompt_System_Boolean;
				logic_uScriptCon_CompareBool_uScriptCon_CompareBool_93.In(logic_uScriptCon_CompareBool_Bool_93);
				bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_93.True;
				bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_93.False;
				if (num)
				{
					Relay_In_235();
				}
				if (flag)
				{
					Relay_In_55();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at Compare Bool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_96()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("513c05f8-723f-45e3-8b2d-48a835a56155", "Compare_Bool", Relay_In_96))
			{
				logic_uScriptCon_CompareBool_Bool_96 = local_msg03aShown_System_Boolean;
				logic_uScriptCon_CompareBool_uScriptCon_CompareBool_96.In(logic_uScriptCon_CompareBool_Bool_96);
				bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_96.True;
				bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_96.False;
				if (num)
				{
					Relay_In_104();
				}
				if (flag)
				{
					Relay_In_82();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at Compare Bool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_True_98()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("16b93728-99ca-4af8-9432-d4d471aed476", "Set_Bool", Relay_True_98))
			{
				logic_uScriptAct_SetBool_uScriptAct_SetBool_98.True(out logic_uScriptAct_SetBool_Target_98);
				local_msg03aShown_System_Boolean = logic_uScriptAct_SetBool_Target_98;
				if (logic_uScriptAct_SetBool_uScriptAct_SetBool_98.Out)
				{
					Relay_In_104();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at Set Bool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_False_98()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("16b93728-99ca-4af8-9432-d4d471aed476", "Set_Bool", Relay_False_98))
			{
				logic_uScriptAct_SetBool_uScriptAct_SetBool_98.False(out logic_uScriptAct_SetBool_Target_98);
				local_msg03aShown_System_Boolean = logic_uScriptAct_SetBool_Target_98;
				if (logic_uScriptAct_SetBool_uScriptAct_SetBool_98.Out)
				{
					Relay_In_104();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at Set Bool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_100()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("275fc713-2f87-49c4-b42e-6f22a92d1217", "Compare_Bool", Relay_In_100))
			{
				logic_uScriptCon_CompareBool_Bool_100 = local_msg03bShown_System_Boolean;
				logic_uScriptCon_CompareBool_uScriptCon_CompareBool_100.In(logic_uScriptCon_CompareBool_Bool_100);
				bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_100.True;
				bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_100.False;
				if (num)
				{
					Relay_In_103();
				}
				if (flag)
				{
					Relay_In_78();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at Compare Bool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_True_102()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("17981083-63d7-4705-a289-98ecad96afe5", "Set_Bool", Relay_True_102))
			{
				logic_uScriptAct_SetBool_uScriptAct_SetBool_102.True(out logic_uScriptAct_SetBool_Target_102);
				local_msg03bShown_System_Boolean = logic_uScriptAct_SetBool_Target_102;
				if (logic_uScriptAct_SetBool_uScriptAct_SetBool_102.Out)
				{
					Relay_In_103();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at Set Bool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_False_102()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("17981083-63d7-4705-a289-98ecad96afe5", "Set_Bool", Relay_False_102))
			{
				logic_uScriptAct_SetBool_uScriptAct_SetBool_102.False(out logic_uScriptAct_SetBool_Target_102);
				local_msg03bShown_System_Boolean = logic_uScriptAct_SetBool_Target_102;
				if (logic_uScriptAct_SetBool_uScriptAct_SetBool_102.Out)
				{
					Relay_In_103();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at Set Bool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_103()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("305e96c8-53d4-442d-9e2f-3eab6302f05d", "Pass", Relay_In_103))
			{
				logic_uScriptAct_Passthrough_uScriptAct_Passthrough_103.In();
				if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_103.Out)
				{
					Relay_False_90();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at Pass.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_104()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("1ee6b3eb-d10d-4f8a-9d3f-585155a6675a", "Pass", Relay_In_104))
			{
				logic_uScriptAct_Passthrough_uScriptAct_Passthrough_104.In();
				if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_104.Out)
				{
					Relay_False_90();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at Pass.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_107()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("8cf4445b-08ae-40a1-b56e-42195ce65f90", "Tank_Get_Tank_Block", Relay_In_107))
			{
				logic_uScript_GetTankBlock_tank_107 = local_PaymentPointTech_Tank;
				logic_uScript_GetTankBlock_blockType_107 = interactableBlockType;
				logic_uScript_GetTankBlock_Return_107 = logic_uScript_GetTankBlock_uScript_GetTankBlock_107.In(logic_uScript_GetTankBlock_tank_107, logic_uScript_GetTankBlock_blockType_107);
				local_TerminalBlock_TankBlock = logic_uScript_GetTankBlock_Return_107;
				if (logic_uScript_GetTankBlock_uScript_GetTankBlock_107.Out)
				{
					Relay_In_254();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at Tank/Get Tank Block.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_True_108()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("8cf81a68-bc87-48f7-8b8e-3de5f98f7a66", "Set_Bool", Relay_True_108))
			{
				logic_uScriptAct_SetBool_uScriptAct_SetBool_108.True(out logic_uScriptAct_SetBool_Target_108);
				local_NearNPC_System_Boolean = logic_uScriptAct_SetBool_Target_108;
				if (logic_uScriptAct_SetBool_uScriptAct_SetBool_108.Out)
				{
					Relay_In_29();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at Set Bool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_False_108()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("8cf81a68-bc87-48f7-8b8e-3de5f98f7a66", "Set_Bool", Relay_False_108))
			{
				logic_uScriptAct_SetBool_uScriptAct_SetBool_108.False(out logic_uScriptAct_SetBool_Target_108);
				local_NearNPC_System_Boolean = logic_uScriptAct_SetBool_Target_108;
				if (logic_uScriptAct_SetBool_uScriptAct_SetBool_108.Out)
				{
					Relay_In_29();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at Set Bool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_110()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("dcb0bc80-228d-4c0e-99f5-8bef9a0ca4dd", "Pass", Relay_In_110))
			{
				logic_uScriptAct_Passthrough_uScriptAct_Passthrough_110.In();
				if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_110.Out)
				{
					Relay_In_243();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at Pass.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_112()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("06a3e5b2-5bf2-4375-a1b6-ef014fd194d8", "Compare_Bool", Relay_In_112))
			{
				logic_uScriptCon_CompareBool_Bool_112 = _DEBUGIgnoreMoneyCheck;
				logic_uScriptCon_CompareBool_uScriptCon_CompareBool_112.In(logic_uScriptCon_CompareBool_Bool_112);
				bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_112.True;
				bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_112.False;
				if (num)
				{
					Relay_In_110();
				}
				if (flag)
				{
					Relay_In_58();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at Compare Bool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Out_114()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("7043e7d4-14ae-48f9-96b5-b11e7f1637a1", "SubGraph_AddMessageWithPadSupport", Relay_Out_114))
			{
				Relay_In_112();
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at SubGraph_AddMessageWithPadSupport.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Shown_114()
	{
		if (relayCallCount++ < 1000)
		{
			CheckDebugBreak("7043e7d4-14ae-48f9-96b5-b11e7f1637a1", "SubGraph_AddMessageWithPadSupport", Relay_Shown_114);
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at SubGraph_AddMessageWithPadSupport.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_114()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("7043e7d4-14ae-48f9-96b5-b11e7f1637a1", "SubGraph_AddMessageWithPadSupport", Relay_In_114))
			{
				logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboard_114 = msg02ClickScreen;
				logic_SubGraph_AddMessageWithPadSupport_messageControlPad_114 = msg02ClickScreen_Pad;
				logic_SubGraph_AddMessageWithPadSupport_speaker_114 = messageSpeaker;
				logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_114.In(logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboard_114, logic_SubGraph_AddMessageWithPadSupport_messageControlPad_114, logic_SubGraph_AddMessageWithPadSupport_speaker_114);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at SubGraph_AddMessageWithPadSupport.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_121()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("bdef9702-8b2c-49fc-8e09-6a379425c2db", "uScript_ClearOnScreenMessagesWithTag", Relay_In_121))
			{
				logic_uScript_ClearOnScreenMessagesWithTag_tag_121 = clearMsgTagWhenAngry;
				logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_121.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_121, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_121);
				if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_121.Out)
				{
					Relay_In_125();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at uScript_ClearOnScreenMessagesWithTag.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Save_Out_122()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("5d41dfd3-fc77-4c7f-bddd-397497de635c", "", Relay_Save_Out_122))
			{
				Relay_Save_200();
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Load_Out_122()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("5d41dfd3-fc77-4c7f-bddd-397497de635c", "", Relay_Load_Out_122))
			{
				Relay_Load_200();
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Restart_Out_122()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("5d41dfd3-fc77-4c7f-bddd-397497de635c", "", Relay_Restart_Out_122))
			{
				Relay_Set_False_200();
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Save_122()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("5d41dfd3-fc77-4c7f-bddd-397497de635c", "", Relay_Save_122))
			{
				logic_SubGraph_SaveLoadBool_boolean_122 = local_AngeredNPC_System_Boolean;
				logic_SubGraph_SaveLoadBool_boolAsVariable_122 = local_AngeredNPC_System_Boolean;
				logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_122.Save(ref logic_SubGraph_SaveLoadBool_boolean_122, logic_SubGraph_SaveLoadBool_boolAsVariable_122, logic_SubGraph_SaveLoadBool_uniqueID_122);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Load_122()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("5d41dfd3-fc77-4c7f-bddd-397497de635c", "", Relay_Load_122))
			{
				logic_SubGraph_SaveLoadBool_boolean_122 = local_AngeredNPC_System_Boolean;
				logic_SubGraph_SaveLoadBool_boolAsVariable_122 = local_AngeredNPC_System_Boolean;
				logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_122.Load(ref logic_SubGraph_SaveLoadBool_boolean_122, logic_SubGraph_SaveLoadBool_boolAsVariable_122, logic_SubGraph_SaveLoadBool_uniqueID_122);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Set_True_122()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("5d41dfd3-fc77-4c7f-bddd-397497de635c", "", Relay_Set_True_122))
			{
				logic_SubGraph_SaveLoadBool_boolean_122 = local_AngeredNPC_System_Boolean;
				logic_SubGraph_SaveLoadBool_boolAsVariable_122 = local_AngeredNPC_System_Boolean;
				logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_122.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_122, logic_SubGraph_SaveLoadBool_boolAsVariable_122, logic_SubGraph_SaveLoadBool_uniqueID_122);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Set_False_122()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("5d41dfd3-fc77-4c7f-bddd-397497de635c", "", Relay_Set_False_122))
			{
				logic_SubGraph_SaveLoadBool_boolean_122 = local_AngeredNPC_System_Boolean;
				logic_SubGraph_SaveLoadBool_boolAsVariable_122 = local_AngeredNPC_System_Boolean;
				logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_122.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_122, logic_SubGraph_SaveLoadBool_boolAsVariable_122, logic_SubGraph_SaveLoadBool_uniqueID_122);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_124()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("dad8e5bb-0c28-4cb7-b854-804fc2beb32b", "Compare_Bool", Relay_In_124))
			{
				logic_uScriptCon_CompareBool_Bool_124 = local_AngeredNPC_System_Boolean;
				logic_uScriptCon_CompareBool_uScriptCon_CompareBool_124.In(logic_uScriptCon_CompareBool_Bool_124);
				if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_124.False)
				{
					Relay_True_33();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at Compare Bool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_125()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("47aafbf5-8a01-45e0-96ea-6b2fd041a45d", "uScript_AddMessage", Relay_In_125))
			{
				logic_uScript_AddMessage_messageData_125 = msgAngered;
				logic_uScript_AddMessage_speaker_125 = messageSpeaker;
				logic_uScript_AddMessage_Return_125 = logic_uScript_AddMessage_uScript_AddMessage_125.In(logic_uScript_AddMessage_messageData_125, logic_uScript_AddMessage_speaker_125);
				if (logic_uScript_AddMessage_uScript_AddMessage_125.Out)
				{
					Relay_In_180();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at uScript_AddMessage.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_128()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("f78fd5e4-30e6-498f-9ed1-9e2d54d63942", "Compare_Bool", Relay_In_128))
			{
				logic_uScriptCon_CompareBool_Bool_128 = local_AngeredNPC_System_Boolean;
				logic_uScriptCon_CompareBool_uScriptCon_CompareBool_128.In(logic_uScriptCon_CompareBool_Bool_128);
				bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_128.True;
				bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_128.False;
				if (num)
				{
					Relay_In_174();
				}
				if (flag)
				{
					Relay_In_2();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at Compare Bool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Succeed_130()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("f622f884-4da0-41bb-a61b-cd8e3db3e42b", "uScript_FinishEncounter", Relay_Succeed_130))
			{
				logic_uScript_FinishEncounter_owner_130 = owner_Connection_135;
				logic_uScript_FinishEncounter_uScript_FinishEncounter_130.Succeed(logic_uScript_FinishEncounter_owner_130);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at uScript_FinishEncounter.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Fail_130()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("f622f884-4da0-41bb-a61b-cd8e3db3e42b", "uScript_FinishEncounter", Relay_Fail_130))
			{
				logic_uScript_FinishEncounter_owner_130 = owner_Connection_135;
				logic_uScript_FinishEncounter_uScript_FinishEncounter_130.Fail(logic_uScript_FinishEncounter_owner_130);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at uScript_FinishEncounter.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_133()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("61ff6464-53ea-44bd-b5f5-1cc237f54ad5", "uScript_AddMessage", Relay_In_133))
			{
				logic_uScript_AddMessage_messageData_133 = msgMissionComplete;
				logic_uScript_AddMessage_speaker_133 = genericSpeaker;
				logic_uScript_AddMessage_Return_133 = logic_uScript_AddMessage_uScript_AddMessage_133.In(logic_uScript_AddMessage_messageData_133, logic_uScript_AddMessage_speaker_133);
				if (logic_uScript_AddMessage_uScript_AddMessage_133.Out)
				{
					Relay_In_168();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at uScript_AddMessage.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_138()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("fd11164c-c917-4de8-938d-7dd522554103", "uScript_ClearOnScreenMessagesWithTag", Relay_In_138))
			{
				logic_uScript_ClearOnScreenMessagesWithTag_tag_138 = clearMsgTagWhenNormal;
				logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_138.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_138, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_138);
				if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_138.Out)
				{
					Relay_In_107();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at uScript_ClearOnScreenMessagesWithTag.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_139()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("6796b9f1-6225-4596-85eb-f85b927fa933", "uScript_ClearOnScreenMessagesWithTag", Relay_In_139))
			{
				logic_uScript_ClearOnScreenMessagesWithTag_tag_139 = clearMsgTagWhenNormal;
				logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_139.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_139, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_139);
				if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_139.Out)
				{
					Relay_In_12();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at uScript_ClearOnScreenMessagesWithTag.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_142()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("419abceb-0fcd-4e50-a4d3-f6ff6d3a40e7", "uScript_ClearOnScreenMessagesWithTag", Relay_In_142))
			{
				logic_uScript_ClearOnScreenMessagesWithTag_tag_142 = clearMsgTagWhenAngry;
				logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_142.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_142, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_142);
				if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_142.Out)
				{
					Relay_In_139();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at uScript_ClearOnScreenMessagesWithTag.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_True_143()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("7d5a8032-296e-4439-b0cc-7af7bec17c60", "Set_Bool", Relay_True_143))
			{
				logic_uScriptAct_SetBool_uScriptAct_SetBool_143.True(out logic_uScriptAct_SetBool_Target_143);
				local_AngeredNPC_System_Boolean = logic_uScriptAct_SetBool_Target_143;
				if (logic_uScriptAct_SetBool_uScriptAct_SetBool_143.Out)
				{
					Relay_False_268();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at Set Bool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_False_143()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("7d5a8032-296e-4439-b0cc-7af7bec17c60", "Set_Bool", Relay_False_143))
			{
				logic_uScriptAct_SetBool_uScriptAct_SetBool_143.False(out logic_uScriptAct_SetBool_Target_143);
				local_AngeredNPC_System_Boolean = logic_uScriptAct_SetBool_Target_143;
				if (logic_uScriptAct_SetBool_uScriptAct_SetBool_143.Out)
				{
					Relay_False_268();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at Set Bool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_145()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("1bee8d38-f9c8-4039-be8b-fee95756570a", "Compare_Bool", Relay_In_145))
			{
				logic_uScriptCon_CompareBool_Bool_145 = local_AngeredNPC_System_Boolean;
				logic_uScriptCon_CompareBool_uScriptCon_CompareBool_145.In(logic_uScriptCon_CompareBool_Bool_145);
				if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_145.False)
				{
					Relay_UnPause_197();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at Compare Bool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_147()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("df7cc2c3-e3f7-489c-a3fa-5049180d403e", "uScript_ClearOnScreenMessagesWithTag", Relay_In_147))
			{
				logic_uScript_ClearOnScreenMessagesWithTag_tag_147 = clearMsgTagWhenNormal;
				logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_147.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_147, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_147);
				if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_147.Out)
				{
					Relay_In_202();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at uScript_ClearOnScreenMessagesWithTag.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_149()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("88c7478d-9157-4282-9a35-730885ef05aa", "uScript_ClearOnScreenMessagesWithTag", Relay_In_149))
			{
				logic_uScript_ClearOnScreenMessagesWithTag_tag_149 = clearMsgTagWhenAngry;
				logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_149.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_149, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_149);
				if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_149.Out)
				{
					Relay_In_147();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at uScript_ClearOnScreenMessagesWithTag.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_InitialSpawn_153()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("fe5a2007-3ef2-44c7-ab45-29cd419ab261", "uScript_SpawnTechsFromData", Relay_InitialSpawn_153))
			{
				int num = 0;
				Array vendorSpawnData = VendorSpawnData;
				if (logic_uScript_SpawnTechsFromData_spawnData_153.Length != num + vendorSpawnData.Length)
				{
					Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_153, num + vendorSpawnData.Length);
				}
				Array.Copy(vendorSpawnData, 0, logic_uScript_SpawnTechsFromData_spawnData_153, num, vendorSpawnData.Length);
				num += vendorSpawnData.Length;
				logic_uScript_SpawnTechsFromData_ownerNode_153 = owner_Connection_152;
				logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_153.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_153, logic_uScript_SpawnTechsFromData_ownerNode_153, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_153, logic_uScript_SpawnTechsFromData_allowResurrection_153);
				if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_153.Out)
				{
					Relay_InitialSpawn_27();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at uScript_SpawnTechsFromData.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_155()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("2d86f4ab-ef7b-457d-99df-4126984adabe", "uScript_GetAndCheckTechs", Relay_In_155))
			{
				int num = 0;
				Array vendorSpawnData = VendorSpawnData;
				if (logic_uScript_GetAndCheckTechs_techData_155.Length != num + vendorSpawnData.Length)
				{
					Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_155, num + vendorSpawnData.Length);
				}
				Array.Copy(vendorSpawnData, 0, logic_uScript_GetAndCheckTechs_techData_155, num, vendorSpawnData.Length);
				num += vendorSpawnData.Length;
				logic_uScript_GetAndCheckTechs_ownerNode_155 = owner_Connection_154;
				int num2 = 0;
				Array array = local_PaymentTechs_TankArray;
				if (logic_uScript_GetAndCheckTechs_techs_155.Length != num2 + array.Length)
				{
					Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_155, num2 + array.Length);
				}
				Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techs_155, num2, array.Length);
				num2 += array.Length;
				logic_uScript_GetAndCheckTechs_Return_155 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_155.In(logic_uScript_GetAndCheckTechs_techData_155, logic_uScript_GetAndCheckTechs_ownerNode_155, ref logic_uScript_GetAndCheckTechs_techs_155);
				local_PaymentTechs_TankArray = logic_uScript_GetAndCheckTechs_techs_155;
				bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_155.AllAlive;
				bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_155.SomeAlive;
				bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_155.AllDead;
				if (allAlive)
				{
					Relay_AtIndex_157();
				}
				if (someAlive)
				{
					Relay_AtIndex_157();
				}
				if (allDead)
				{
					Relay_In_195();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at uScript_GetAndCheckTechs.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_AtIndex_157()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("68345f3c-0533-4021-b3a4-ac0b1e3b8347", "uScript_AccessListTech", Relay_AtIndex_157))
			{
				int num = 0;
				Array array = local_PaymentTechs_TankArray;
				if (logic_uScript_AccessListTech_techList_157.Length != num + array.Length)
				{
					Array.Resize(ref logic_uScript_AccessListTech_techList_157, num + array.Length);
				}
				Array.Copy(array, 0, logic_uScript_AccessListTech_techList_157, num, array.Length);
				num += array.Length;
				logic_uScript_AccessListTech_uScript_AccessListTech_157.AtIndex(ref logic_uScript_AccessListTech_techList_157, logic_uScript_AccessListTech_index_157, out logic_uScript_AccessListTech_value_157);
				local_PaymentTechs_TankArray = logic_uScript_AccessListTech_techList_157;
				local_PaymentPointTech_Tank = logic_uScript_AccessListTech_value_157;
				if (logic_uScript_AccessListTech_uScript_AccessListTech_157.Out)
				{
					Relay_In_159();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at uScript_AccessListTech.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_159()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("9fb74ec7-8ad4-49f6-9358-b95a9422dfad", "Set_tank_invulnerable", Relay_In_159))
			{
				logic_uScript_SetTankInvulnerable_tank_159 = local_PaymentPointTech_Tank;
				logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_159.In(logic_uScript_SetTankInvulnerable_invulnerable_159, logic_uScript_SetTankInvulnerable_tank_159);
				if (logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_159.Out)
				{
					Relay_In_274();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at Set tank invulnerable.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_163()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("1ca3827f-06d9-49ff-84b3-4d7e7e1ef3f4", "uScript_FlyTechUpAndAway", Relay_In_163))
			{
				logic_uScript_FlyTechUpAndAway_tech_163 = local_PaymentPointTech_Tank;
				logic_uScript_FlyTechUpAndAway_aiTree_163 = NPCFlyAwayAI;
				logic_uScript_FlyTechUpAndAway_removalParticles_163 = NPCDespawnParticleEffect;
				logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_163.In(logic_uScript_FlyTechUpAndAway_tech_163, logic_uScript_FlyTechUpAndAway_maxLifetime_163, logic_uScript_FlyTechUpAndAway_targetHeight_163, logic_uScript_FlyTechUpAndAway_aiTree_163, logic_uScript_FlyTechUpAndAway_removalParticles_163);
				if (logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_163.Out)
				{
					Relay_In_171();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at uScript_FlyTechUpAndAway.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_168()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("e56f8b80-c7a5-43d9-b250-205fec93ab8a", "uScript_FlyTechUpAndAway", Relay_In_168))
			{
				logic_uScript_FlyTechUpAndAway_tech_168 = local_PaymentPointTech_Tank;
				logic_uScript_FlyTechUpAndAway_aiTree_168 = NPCFlyAwayAI;
				logic_uScript_FlyTechUpAndAway_removalParticles_168 = NPCDespawnParticleEffect;
				logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_168.In(logic_uScript_FlyTechUpAndAway_tech_168, logic_uScript_FlyTechUpAndAway_maxLifetime_168, logic_uScript_FlyTechUpAndAway_targetHeight_168, logic_uScript_FlyTechUpAndAway_aiTree_168, logic_uScript_FlyTechUpAndAway_removalParticles_168);
				if (logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_168.Out)
				{
					Relay_UnPause_247();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at uScript_FlyTechUpAndAway.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_171()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("49392d40-86f3-4cdd-b2ee-cccb31243a47", "uScript_AddMessage", Relay_In_171))
			{
				logic_uScript_AddMessage_messageData_171 = msgMissionComplete;
				logic_uScript_AddMessage_speaker_171 = genericSpeaker;
				logic_uScript_AddMessage_Return_171 = logic_uScript_AddMessage_uScript_AddMessage_171.In(logic_uScript_AddMessage_messageData_171, logic_uScript_AddMessage_speaker_171);
				if (logic_uScript_AddMessage_uScript_AddMessage_171.Out)
				{
					Relay_UnPause_199();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at uScript_AddMessage.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_174()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("735ca71e-1bab-430b-a7c2-287ee0fcd6aa", "Distance_Is_Visible_in_Trigger_Area", Relay_In_174))
			{
				logic_uScript_IsVisibleInTrigger_visibleObject_174 = local_NPCTech_Tank;
				logic_uScript_IsVisibleInTrigger_triggerAreaName_174 = KillBoxTrigger;
				logic_uScript_IsVisibleInTrigger_uScript_IsVisibleInTrigger_174.In(logic_uScript_IsVisibleInTrigger_visibleObject_174, logic_uScript_IsVisibleInTrigger_triggerAreaName_174);
				if (logic_uScript_IsVisibleInTrigger_uScript_IsVisibleInTrigger_174.InRange)
				{
					Relay_In_176();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at Distance/Is Visible in Trigger Area.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_176()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("5f81f80c-2143-437b-a26c-0675ee6dea89", "uScript_DamageTechs", Relay_In_176))
			{
				int num = 0;
				Array array = local_NPCTechs_TankArray;
				if (logic_uScript_DamageTechs_techs_176.Length != num + array.Length)
				{
					Array.Resize(ref logic_uScript_DamageTechs_techs_176, num + array.Length);
				}
				Array.Copy(array, 0, logic_uScript_DamageTechs_techs_176, num, array.Length);
				num += array.Length;
				logic_uScript_DamageTechs_uScript_DamageTechs_176.In(logic_uScript_DamageTechs_techs_176, logic_uScript_DamageTechs_dmgPercent_176, logic_uScript_DamageTechs_givePlyrCredit_176, logic_uScript_DamageTechs_leaveBlksPercent_176, logic_uScript_DamageTechs_makeVulnerable_176);
				if (logic_uScript_DamageTechs_uScript_DamageTechs_176.Out)
				{
					Relay_True_203();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at uScript_DamageTechs.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_180()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("4b5c566b-a245-45a1-878c-81c0c4bbc045", "Set_tank_invulnerable", Relay_In_180))
			{
				logic_uScript_SetTankInvulnerable_tank_180 = local_NPCTech_Tank;
				logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_180.In(logic_uScript_SetTankInvulnerable_invulnerable_180, logic_uScript_SetTankInvulnerable_tank_180);
				if (logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_180.Out)
				{
					Relay_In_185();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at Set tank invulnerable.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_182()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("638fe9b9-fc65-4302-9a0f-1e074a326ac2", "uScript_FlyTechUpAndAway", Relay_In_182))
			{
				logic_uScript_FlyTechUpAndAway_tech_182 = local_PaymentPointTech_Tank;
				logic_uScript_FlyTechUpAndAway_aiTree_182 = NPCFlyAwayAI;
				logic_uScript_FlyTechUpAndAway_removalParticles_182 = NPCDespawnParticleEffect;
				logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_182.In(logic_uScript_FlyTechUpAndAway_tech_182, logic_uScript_FlyTechUpAndAway_maxLifetime_182, logic_uScript_FlyTechUpAndAway_targetHeight_182, logic_uScript_FlyTechUpAndAway_aiTree_182, logic_uScript_FlyTechUpAndAway_removalParticles_182);
				if (logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_182.Out)
				{
					Relay_In_186();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at uScript_FlyTechUpAndAway.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_183()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("f912f23b-aa4f-4277-b07c-fb39580378ae", "Tank_Get_Tank_Block", Relay_In_183))
			{
				logic_uScript_GetTankBlock_tank_183 = local_PaymentPointTech_Tank;
				logic_uScript_GetTankBlock_blockType_183 = interactableBlockType;
				logic_uScript_GetTankBlock_Return_183 = logic_uScript_GetTankBlock_uScript_GetTankBlock_183.In(logic_uScript_GetTankBlock_tank_183, logic_uScript_GetTankBlock_blockType_183);
				local_TerminalBlock_TankBlock = logic_uScript_GetTankBlock_Return_183;
				if (logic_uScript_GetTankBlock_uScript_GetTankBlock_183.Out)
				{
					Relay_In_182();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at Tank/Get Tank Block.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_185()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("e4aad498-e0b2-4998-9d64-6c989e790932", "AI_Set_team", Relay_In_185))
			{
				logic_uScript_SetTankTeam_tank_185 = local_NPCTech_Tank;
				logic_uScript_SetTankTeam_team_185 = local_189_System_Int32;
				logic_uScript_SetTankTeam_uScript_SetTankTeam_185.In(logic_uScript_SetTankTeam_tank_185, logic_uScript_SetTankTeam_team_185);
				if (logic_uScript_SetTankTeam_uScript_SetTankTeam_185.Out)
				{
					Relay_In_183();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at AI/Set team.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_186()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("131d0878-9157-4f03-a6f4-21576711c7db", "uScript_SetEncounterTarget", Relay_In_186))
			{
				logic_uScript_SetEncounterTarget_owner_186 = owner_Connection_187;
				logic_uScript_SetEncounterTarget_visibleObject_186 = local_NPCTech_Tank;
				logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_186.In(logic_uScript_SetEncounterTarget_owner_186, logic_uScript_SetEncounterTarget_visibleObject_186);
				if (logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_186.Out)
				{
					Relay_True_143();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at uScript_SetEncounterTarget.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_195()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("16319500-9c26-4c16-9ad4-589882886708", "Pass", Relay_In_195))
			{
				logic_uScriptAct_Passthrough_uScriptAct_Passthrough_195.In();
				if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_195.Out)
				{
					Relay_False_280();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at Pass.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Pause_196()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("a4559962-8a36-47e4-9724-1de6c42ebb13", "uScript_PausePopulation", Relay_Pause_196))
			{
				logic_uScript_PausePopulation_uScript_PausePopulation_196.Pause();
				if (logic_uScript_PausePopulation_uScript_PausePopulation_196.Out)
				{
					Relay_False_108();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at uScript_PausePopulation.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_UnPause_196()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("a4559962-8a36-47e4-9724-1de6c42ebb13", "uScript_PausePopulation", Relay_UnPause_196))
			{
				logic_uScript_PausePopulation_uScript_PausePopulation_196.UnPause();
				if (logic_uScript_PausePopulation_uScript_PausePopulation_196.Out)
				{
					Relay_False_108();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at uScript_PausePopulation.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Pause_197()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("3d0941c5-2f9f-4320-95f2-f735bc83535e", "uScript_PausePopulation", Relay_Pause_197))
			{
				logic_uScript_PausePopulation_uScript_PausePopulation_197.Pause();
				if (logic_uScript_PausePopulation_uScript_PausePopulation_197.Out)
				{
					Relay_In_121();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at uScript_PausePopulation.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_UnPause_197()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("3d0941c5-2f9f-4320-95f2-f735bc83535e", "uScript_PausePopulation", Relay_UnPause_197))
			{
				logic_uScript_PausePopulation_uScript_PausePopulation_197.UnPause();
				if (logic_uScript_PausePopulation_uScript_PausePopulation_197.Out)
				{
					Relay_In_121();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at uScript_PausePopulation.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Pause_198()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("e4c7f104-96ad-497e-86a5-91fbb22bb137", "uScript_PausePopulation", Relay_Pause_198))
			{
				logic_uScript_PausePopulation_uScript_PausePopulation_198.Pause();
				if (logic_uScript_PausePopulation_uScript_PausePopulation_198.Out)
				{
					Relay_In_16();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at uScript_PausePopulation.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_UnPause_198()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("e4c7f104-96ad-497e-86a5-91fbb22bb137", "uScript_PausePopulation", Relay_UnPause_198))
			{
				logic_uScript_PausePopulation_uScript_PausePopulation_198.UnPause();
				if (logic_uScript_PausePopulation_uScript_PausePopulation_198.Out)
				{
					Relay_In_16();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at uScript_PausePopulation.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Pause_199()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("5ccc6a75-25d3-480b-a359-26ee86e90759", "uScript_PausePopulation", Relay_Pause_199))
			{
				logic_uScript_PausePopulation_uScript_PausePopulation_199.Pause();
				if (logic_uScript_PausePopulation_uScript_PausePopulation_199.Out)
				{
					Relay_In_233();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at uScript_PausePopulation.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_UnPause_199()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("5ccc6a75-25d3-480b-a359-26ee86e90759", "uScript_PausePopulation", Relay_UnPause_199))
			{
				logic_uScript_PausePopulation_uScript_PausePopulation_199.UnPause();
				if (logic_uScript_PausePopulation_uScript_PausePopulation_199.Out)
				{
					Relay_In_233();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at uScript_PausePopulation.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Save_Out_200()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("775990ed-1aac-43d8-9d70-e34c361fa0c3", "", Relay_Save_Out_200))
			{
				Relay_Save_201();
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Load_Out_200()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("775990ed-1aac-43d8-9d70-e34c361fa0c3", "", Relay_Load_Out_200))
			{
				Relay_Load_201();
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Restart_Out_200()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("775990ed-1aac-43d8-9d70-e34c361fa0c3", "", Relay_Restart_Out_200))
			{
				Relay_Set_False_201();
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Save_200()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("775990ed-1aac-43d8-9d70-e34c361fa0c3", "", Relay_Save_200))
			{
				logic_SubGraph_SaveLoadBool_boolean_200 = local_NearNPC_System_Boolean;
				logic_SubGraph_SaveLoadBool_boolAsVariable_200 = local_NearNPC_System_Boolean;
				logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_200.Save(ref logic_SubGraph_SaveLoadBool_boolean_200, logic_SubGraph_SaveLoadBool_boolAsVariable_200, logic_SubGraph_SaveLoadBool_uniqueID_200);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Load_200()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("775990ed-1aac-43d8-9d70-e34c361fa0c3", "", Relay_Load_200))
			{
				logic_SubGraph_SaveLoadBool_boolean_200 = local_NearNPC_System_Boolean;
				logic_SubGraph_SaveLoadBool_boolAsVariable_200 = local_NearNPC_System_Boolean;
				logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_200.Load(ref logic_SubGraph_SaveLoadBool_boolean_200, logic_SubGraph_SaveLoadBool_boolAsVariable_200, logic_SubGraph_SaveLoadBool_uniqueID_200);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Set_True_200()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("775990ed-1aac-43d8-9d70-e34c361fa0c3", "", Relay_Set_True_200))
			{
				logic_SubGraph_SaveLoadBool_boolean_200 = local_NearNPC_System_Boolean;
				logic_SubGraph_SaveLoadBool_boolAsVariable_200 = local_NearNPC_System_Boolean;
				logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_200.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_200, logic_SubGraph_SaveLoadBool_boolAsVariable_200, logic_SubGraph_SaveLoadBool_uniqueID_200);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Set_False_200()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("775990ed-1aac-43d8-9d70-e34c361fa0c3", "", Relay_Set_False_200))
			{
				logic_SubGraph_SaveLoadBool_boolean_200 = local_NearNPC_System_Boolean;
				logic_SubGraph_SaveLoadBool_boolAsVariable_200 = local_NearNPC_System_Boolean;
				logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_200.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_200, logic_SubGraph_SaveLoadBool_boolAsVariable_200, logic_SubGraph_SaveLoadBool_uniqueID_200);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Save_Out_201()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("c698d79d-21d6-4243-8947-0ad7c2b2975a", "", Relay_Save_Out_201))
			{
				Relay_Save_210();
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Load_Out_201()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("c698d79d-21d6-4243-8947-0ad7c2b2975a", "", Relay_Load_Out_201))
			{
				Relay_Load_210();
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Restart_Out_201()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("c698d79d-21d6-4243-8947-0ad7c2b2975a", "", Relay_Restart_Out_201))
			{
				Relay_Set_False_210();
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Save_201()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("c698d79d-21d6-4243-8947-0ad7c2b2975a", "", Relay_Save_201))
			{
				logic_SubGraph_SaveLoadBool_boolean_201 = local_HasEnoughMoney_System_Boolean;
				logic_SubGraph_SaveLoadBool_boolAsVariable_201 = local_HasEnoughMoney_System_Boolean;
				logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_201.Save(ref logic_SubGraph_SaveLoadBool_boolean_201, logic_SubGraph_SaveLoadBool_boolAsVariable_201, logic_SubGraph_SaveLoadBool_uniqueID_201);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Load_201()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("c698d79d-21d6-4243-8947-0ad7c2b2975a", "", Relay_Load_201))
			{
				logic_SubGraph_SaveLoadBool_boolean_201 = local_HasEnoughMoney_System_Boolean;
				logic_SubGraph_SaveLoadBool_boolAsVariable_201 = local_HasEnoughMoney_System_Boolean;
				logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_201.Load(ref logic_SubGraph_SaveLoadBool_boolean_201, logic_SubGraph_SaveLoadBool_boolAsVariable_201, logic_SubGraph_SaveLoadBool_uniqueID_201);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Set_True_201()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("c698d79d-21d6-4243-8947-0ad7c2b2975a", "", Relay_Set_True_201))
			{
				logic_SubGraph_SaveLoadBool_boolean_201 = local_HasEnoughMoney_System_Boolean;
				logic_SubGraph_SaveLoadBool_boolAsVariable_201 = local_HasEnoughMoney_System_Boolean;
				logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_201.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_201, logic_SubGraph_SaveLoadBool_boolAsVariable_201, logic_SubGraph_SaveLoadBool_uniqueID_201);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Set_False_201()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("c698d79d-21d6-4243-8947-0ad7c2b2975a", "", Relay_Set_False_201))
			{
				logic_SubGraph_SaveLoadBool_boolean_201 = local_HasEnoughMoney_System_Boolean;
				logic_SubGraph_SaveLoadBool_boolAsVariable_201 = local_HasEnoughMoney_System_Boolean;
				logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_201.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_201, logic_SubGraph_SaveLoadBool_boolAsVariable_201, logic_SubGraph_SaveLoadBool_uniqueID_201);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_202()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("9175c142-616f-4b3c-ab40-ec9770ac2d99", "Compare_Bool", Relay_In_202))
			{
				logic_uScriptCon_CompareBool_Bool_202 = local_FallenOffBridge_System_Boolean;
				logic_uScriptCon_CompareBool_uScriptCon_CompareBool_202.In(logic_uScriptCon_CompareBool_Bool_202);
				bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_202.True;
				bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_202.False;
				if (num)
				{
					Relay_In_205();
				}
				if (flag)
				{
					Relay_In_133();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at Compare Bool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_True_203()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("b9fbaeb2-0ce0-47f3-b0f0-f6bcef306901", "Set_Bool", Relay_True_203))
			{
				logic_uScriptAct_SetBool_uScriptAct_SetBool_203.True(out logic_uScriptAct_SetBool_Target_203);
				local_FallenOffBridge_System_Boolean = logic_uScriptAct_SetBool_Target_203;
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at Set Bool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_False_203()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("b9fbaeb2-0ce0-47f3-b0f0-f6bcef306901", "Set_Bool", Relay_False_203))
			{
				logic_uScriptAct_SetBool_uScriptAct_SetBool_203.False(out logic_uScriptAct_SetBool_Target_203);
				local_FallenOffBridge_System_Boolean = logic_uScriptAct_SetBool_Target_203;
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at Set Bool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_205()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("ded7f5b3-2e74-4eb9-ad70-a857e2c030eb", "uScript_AddMessage", Relay_In_205))
			{
				logic_uScript_AddMessage_messageData_205 = msgMissionCompleteBridgeFall;
				logic_uScript_AddMessage_speaker_205 = genericSpeaker;
				logic_uScript_AddMessage_Return_205 = logic_uScript_AddMessage_uScript_AddMessage_205.In(logic_uScript_AddMessage_messageData_205, logic_uScript_AddMessage_speaker_205);
				if (logic_uScript_AddMessage_uScript_AddMessage_205.Out)
				{
					Relay_In_168();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at uScript_AddMessage.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Save_Out_210()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("9a3e1982-7de0-4c94-8884-f2c7e4fb61ce", "", Relay_Save_Out_210))
			{
				Relay_Save_267();
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Load_Out_210()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("9a3e1982-7de0-4c94-8884-f2c7e4fb61ce", "", Relay_Load_Out_210))
			{
				Relay_Load_267();
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Restart_Out_210()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("9a3e1982-7de0-4c94-8884-f2c7e4fb61ce", "", Relay_Restart_Out_210))
			{
				Relay_Set_False_267();
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Save_210()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("9a3e1982-7de0-4c94-8884-f2c7e4fb61ce", "", Relay_Save_210))
			{
				logic_SubGraph_SaveLoadBool_boolean_210 = local_FallenOffBridge_System_Boolean;
				logic_SubGraph_SaveLoadBool_boolAsVariable_210 = local_FallenOffBridge_System_Boolean;
				logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_210.Save(ref logic_SubGraph_SaveLoadBool_boolean_210, logic_SubGraph_SaveLoadBool_boolAsVariable_210, logic_SubGraph_SaveLoadBool_uniqueID_210);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Load_210()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("9a3e1982-7de0-4c94-8884-f2c7e4fb61ce", "", Relay_Load_210))
			{
				logic_SubGraph_SaveLoadBool_boolean_210 = local_FallenOffBridge_System_Boolean;
				logic_SubGraph_SaveLoadBool_boolAsVariable_210 = local_FallenOffBridge_System_Boolean;
				logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_210.Load(ref logic_SubGraph_SaveLoadBool_boolean_210, logic_SubGraph_SaveLoadBool_boolAsVariable_210, logic_SubGraph_SaveLoadBool_uniqueID_210);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Set_True_210()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("9a3e1982-7de0-4c94-8884-f2c7e4fb61ce", "", Relay_Set_True_210))
			{
				logic_SubGraph_SaveLoadBool_boolean_210 = local_FallenOffBridge_System_Boolean;
				logic_SubGraph_SaveLoadBool_boolAsVariable_210 = local_FallenOffBridge_System_Boolean;
				logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_210.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_210, logic_SubGraph_SaveLoadBool_boolAsVariable_210, logic_SubGraph_SaveLoadBool_uniqueID_210);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Set_False_210()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("9a3e1982-7de0-4c94-8884-f2c7e4fb61ce", "", Relay_Set_False_210))
			{
				logic_SubGraph_SaveLoadBool_boolean_210 = local_FallenOffBridge_System_Boolean;
				logic_SubGraph_SaveLoadBool_boolAsVariable_210 = local_FallenOffBridge_System_Boolean;
				logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_210.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_210, logic_SubGraph_SaveLoadBool_boolAsVariable_210, logic_SubGraph_SaveLoadBool_uniqueID_210);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_212()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("1d20956b-09f8-43b8-93f0-c429b334ffd1", "Compare_Bool", Relay_In_212))
			{
				logic_uScriptCon_CompareBool_Bool_212 = local_LicensePurchased_System_Boolean;
				logic_uScriptCon_CompareBool_uScriptCon_CompareBool_212.In(logic_uScriptCon_CompareBool_Bool_212);
				bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_212.True;
				bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_212.False;
				if (num)
				{
					Relay_Pause_198();
				}
				if (flag)
				{
					Relay_In_145();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at Compare Bool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_213()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("c7ec0edc-b2eb-4eff-bda8-71449fdd1749", "uScript_DirectEnemiesOutOfEncounter", Relay_In_213))
			{
				logic_uScript_DirectEnemiesOutOfEncounter_ownerNode_213 = owner_Connection_216;
				logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_213.In(logic_uScript_DirectEnemiesOutOfEncounter_ownerNode_213);
				if (logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_213.Out)
				{
					Relay_InitialSpawn_153();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at uScript_DirectEnemiesOutOfEncounter.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_True_214()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("88aabb32-59f6-4409-856f-1da42cf482c2", "Set_Bool", Relay_True_214))
			{
				logic_uScriptAct_SetBool_uScriptAct_SetBool_214.True(out logic_uScriptAct_SetBool_Target_214);
				local_Init_System_Boolean = logic_uScriptAct_SetBool_Target_214;
				if (logic_uScriptAct_SetBool_uScriptAct_SetBool_214.Out)
				{
					Relay_In_155();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at Set Bool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_False_214()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("88aabb32-59f6-4409-856f-1da42cf482c2", "Set_Bool", Relay_False_214))
			{
				logic_uScriptAct_SetBool_uScriptAct_SetBool_214.False(out logic_uScriptAct_SetBool_Target_214);
				local_Init_System_Boolean = logic_uScriptAct_SetBool_Target_214;
				if (logic_uScriptAct_SetBool_uScriptAct_SetBool_214.Out)
				{
					Relay_In_155();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at Set Bool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_217()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("58b709e2-fa4f-4314-8d76-ab200103eb81", "Show_Mission_Prompt_on_Block", Relay_In_217))
			{
				logic_uScript_MissionPromptBlock_Show_bodyText_217 = msgPromptText;
				logic_uScript_MissionPromptBlock_Show_acceptButtonText_217 = msgPromptAccept;
				logic_uScript_MissionPromptBlock_Show_rejectButtonText_217 = msgPromptDecline;
				logic_uScript_MissionPromptBlock_Show_targetBlock_217 = local_TerminalBlock_TankBlock;
				logic_uScript_MissionPromptBlock_Show_highlightBlock_217 = local_HighlightBlock_System_Boolean;
				logic_uScript_MissionPromptBlock_Show_uScript_MissionPromptBlock_Show_217.In(logic_uScript_MissionPromptBlock_Show_bodyText_217, logic_uScript_MissionPromptBlock_Show_acceptButtonText_217, logic_uScript_MissionPromptBlock_Show_rejectButtonText_217, logic_uScript_MissionPromptBlock_Show_targetBlock_217, logic_uScript_MissionPromptBlock_Show_highlightBlock_217, logic_uScript_MissionPromptBlock_Show_singleUse_217);
				if (logic_uScript_MissionPromptBlock_Show_uScript_MissionPromptBlock_Show_217.Out)
				{
					Relay_True_73();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at Show Mission Prompt on Block.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_218()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("09deabb0-1e6c-429d-a3be-73c4b2c29a1d", "Show_Mission_Prompt_on_Block", Relay_In_218))
			{
				logic_uScript_MissionPromptBlock_Show_bodyText_218 = msgPromptText;
				logic_uScript_MissionPromptBlock_Show_acceptButtonText_218 = msgPromptNoMoney;
				logic_uScript_MissionPromptBlock_Show_targetBlock_218 = local_TerminalBlock_TankBlock;
				logic_uScript_MissionPromptBlock_Show_highlightBlock_218 = local_HighlightBlock_System_Boolean;
				logic_uScript_MissionPromptBlock_Show_uScript_MissionPromptBlock_Show_218.In(logic_uScript_MissionPromptBlock_Show_bodyText_218, logic_uScript_MissionPromptBlock_Show_acceptButtonText_218, logic_uScript_MissionPromptBlock_Show_rejectButtonText_218, logic_uScript_MissionPromptBlock_Show_targetBlock_218, logic_uScript_MissionPromptBlock_Show_highlightBlock_218, logic_uScript_MissionPromptBlock_Show_singleUse_218);
				if (logic_uScript_MissionPromptBlock_Show_uScript_MissionPromptBlock_Show_218.Out)
				{
					Relay_False_73();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at Show Mission Prompt on Block.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_222()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("9fa71bdd-4279-4b63-8c7b-fb2ef9f414da", "Compare_Block", Relay_In_222))
			{
				logic_uScript_CompareBlock_A_222 = local_221_TankBlock;
				logic_uScript_CompareBlock_B_222 = local_TerminalBlock_TankBlock;
				logic_uScript_CompareBlock_uScript_CompareBlock_222.In(logic_uScript_CompareBlock_A_222, logic_uScript_CompareBlock_B_222);
				if (logic_uScript_CompareBlock_uScript_CompareBlock_222.EqualTo)
				{
					Relay_In_225();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at Compare Block.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_225()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("23bb58ab-b825-4e5c-9c17-8542325e93a7", "uScript_ClearOnScreenMessagesWithTag", Relay_In_225))
			{
				logic_uScript_ClearOnScreenMessagesWithTag_tag_225 = clearMsgTagWhenNormal;
				logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_225.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_225, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_225);
				if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_225.Out)
				{
					Relay_In_227();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at uScript_ClearOnScreenMessagesWithTag.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_227()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("8eaa11f8-9bc4-4233-8fd7-16bd320935cd", "uScript_RemoveOnScreenMessage", Relay_In_227))
			{
				logic_uScript_RemoveOnScreenMessage_onScreenMessage_227 = local_Msg02ClickScreen_ManOnScreenMessages_OnScreenMessage;
				logic_uScript_RemoveOnScreenMessage_uScript_RemoveOnScreenMessage_227.In(logic_uScript_RemoveOnScreenMessage_onScreenMessage_227, logic_uScript_RemoveOnScreenMessage_instant_227);
				if (logic_uScript_RemoveOnScreenMessage_uScript_RemoveOnScreenMessage_227.Out)
				{
					Relay_In_230();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at uScript_RemoveOnScreenMessage.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_ResponseEvent_228()
	{
		if (!CheckDebugBreak("e8ed804e-568f-46be-a3ea-15189f85b76e", "On_Mission_Prompt_Result", Relay_ResponseEvent_228))
		{
			local_221_TankBlock = event_UnityEngine_GameObject_TankBlock_228;
			local_229_System_Boolean = event_UnityEngine_GameObject_Accepted_228;
			Relay_In_222();
		}
	}

	private void Relay_In_230()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("dab9bfe5-7f4f-4e9c-b409-401f454d4869", "Compare_Bool", Relay_In_230))
			{
				logic_uScriptCon_CompareBool_Bool_230 = local_229_System_Boolean;
				logic_uScriptCon_CompareBool_uScriptCon_CompareBool_230.In(logic_uScriptCon_CompareBool_Bool_230);
				bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_230.True;
				bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_230.False;
				if (num)
				{
					Relay_In_75();
				}
				if (flag)
				{
					Relay_In_96();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at Compare Bool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_231()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("6cc70b75-ab77-4755-8697-8e8b24fced36", "Hide_Mission_Prompt_on_Block", Relay_In_231))
			{
				logic_uScript_MissionPromptBlock_Hide_targetBlock_231 = local_TerminalBlock_TankBlock;
				logic_uScript_MissionPromptBlock_Hide_uScript_MissionPromptBlock_Hide_231.In(logic_uScript_MissionPromptBlock_Hide_targetBlock_231);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at Hide Mission Prompt on Block.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_233()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("6bd82948-f256-4236-b073-9d8a7fff260f", "Hide_Mission_Prompt_on_Block", Relay_In_233))
			{
				logic_uScript_MissionPromptBlock_Hide_targetBlock_233 = local_TerminalBlock_TankBlock;
				logic_uScript_MissionPromptBlock_Hide_uScript_MissionPromptBlock_Hide_233.In(logic_uScript_MissionPromptBlock_Hide_targetBlock_233);
				if (logic_uScript_MissionPromptBlock_Hide_uScript_MissionPromptBlock_Hide_233.Out)
				{
					Relay_In_251();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at Hide Mission Prompt on Block.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_235()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("0c7c0a49-e35f-4f31-9177-fc89ffdd14c9", "Compare_Bool", Relay_In_235))
			{
				logic_uScriptCon_CompareBool_Bool_235 = _DEBUGIgnoreMoneyCheck;
				logic_uScriptCon_CompareBool_uScriptCon_CompareBool_235.In(logic_uScriptCon_CompareBool_Bool_235);
				bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_235.True;
				bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_235.False;
				if (num)
				{
					Relay_In_240();
				}
				if (flag)
				{
					Relay_In_241();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at Compare Bool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_238()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("3c6dd7e8-8bd5-4db0-b348-0a5b32147c45", "Compare_Int", Relay_In_238))
			{
				logic_uScriptCon_CompareInt_A_238 = local_CurrentMoney_System_Int32;
				logic_uScriptCon_CompareInt_B_238 = LicenseCost;
				logic_uScriptCon_CompareInt_uScriptCon_CompareInt_238.In(logic_uScriptCon_CompareInt_A_238, logic_uScriptCon_CompareInt_B_238);
				bool greaterThanOrEqualTo = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_238.GreaterThanOrEqualTo;
				bool lessThan = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_238.LessThan;
				if (greaterThanOrEqualTo)
				{
					Relay_In_242();
				}
				if (lessThan)
				{
					Relay_In_246();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at Compare Int.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_240()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("ed4170db-d6fc-463d-b84d-703b8501f08f", "Pass", Relay_In_240))
			{
				logic_uScriptAct_Passthrough_uScriptAct_Passthrough_240.In();
				if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_240.Out)
				{
					Relay_In_242();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at Pass.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_241()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("34d22335-4f25-4295-b442-70938731699c", "uScript_GetCurrentMoneyEarned", Relay_In_241))
			{
				logic_uScript_GetCurrentMoneyEarned_Return_241 = logic_uScript_GetCurrentMoneyEarned_uScript_GetCurrentMoneyEarned_241.In();
				local_CurrentMoney_System_Int32 = logic_uScript_GetCurrentMoneyEarned_Return_241;
				if (logic_uScript_GetCurrentMoneyEarned_uScript_GetCurrentMoneyEarned_241.Out)
				{
					Relay_In_238();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at uScript_GetCurrentMoneyEarned.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_242()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("c0c08a72-3d8a-4c0d-9c1c-2485ed0b20cb", "Compare_Bool", Relay_In_242))
			{
				logic_uScriptCon_CompareBool_Bool_242 = local_HasEnoughMoney_System_Boolean;
				logic_uScriptCon_CompareBool_uScriptCon_CompareBool_242.In(logic_uScriptCon_CompareBool_Bool_242);
				if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_242.False)
				{
					Relay_True_261();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at Compare Bool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_243()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("cc595f07-4953-4c75-b610-f7a1ea345743", "Pass", Relay_In_243))
			{
				logic_uScriptAct_Passthrough_uScriptAct_Passthrough_243.In();
				if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_243.Out)
				{
					Relay_True_261();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at Pass.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_246()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("a6bcd9ad-0d40-470b-bc96-09a1645e68c4", "Compare_Bool", Relay_In_246))
			{
				logic_uScriptCon_CompareBool_Bool_246 = local_HasEnoughMoney_System_Boolean;
				logic_uScriptCon_CompareBool_uScriptCon_CompareBool_246.In(logic_uScriptCon_CompareBool_Bool_246);
				if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_246.True)
				{
					Relay_True_270();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at Compare Bool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Pause_247()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("fdfe06d1-ff5e-4f96-9cf3-6499a7049e13", "uScript_PausePopulation", Relay_Pause_247))
			{
				logic_uScript_PausePopulation_uScript_PausePopulation_247.Pause();
				if (logic_uScript_PausePopulation_uScript_PausePopulation_247.Out)
				{
					Relay_In_248();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at uScript_PausePopulation.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_UnPause_247()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("fdfe06d1-ff5e-4f96-9cf3-6499a7049e13", "uScript_PausePopulation", Relay_UnPause_247))
			{
				logic_uScript_PausePopulation_uScript_PausePopulation_247.UnPause();
				if (logic_uScript_PausePopulation_uScript_PausePopulation_247.Out)
				{
					Relay_In_248();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at uScript_PausePopulation.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_248()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("48f6eb47-3499-45c8-8b62-d78f9a05cb89", "uScript_ClearEncounterTarget", Relay_In_248))
			{
				logic_uScript_ClearEncounterTarget_owner_248 = owner_Connection_249;
				logic_uScript_ClearEncounterTarget_uScript_ClearEncounterTarget_248.In(logic_uScript_ClearEncounterTarget_owner_248);
				if (logic_uScript_ClearEncounterTarget_uScript_ClearEncounterTarget_248.Out)
				{
					Relay_Succeed_130();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at uScript_ClearEncounterTarget.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_251()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("aeb2525c-ef77-460b-b108-5eafa2771d85", "uScript_ClearEncounterTarget", Relay_In_251))
			{
				logic_uScript_ClearEncounterTarget_owner_251 = owner_Connection_250;
				logic_uScript_ClearEncounterTarget_uScript_ClearEncounterTarget_251.In(logic_uScript_ClearEncounterTarget_owner_251);
				if (logic_uScript_ClearEncounterTarget_uScript_ClearEncounterTarget_251.Out)
				{
					Relay_Succeed_25();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at uScript_ClearEncounterTarget.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_252()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("5e711b2e-a781-4a0d-99dd-a81bdc5d946d", "Distance_Is_Player_in_Trigger_Area", Relay_In_252))
			{
				logic_uScript_IsPlayerInTrigger_triggerAreaName_252 = AngryZone;
				logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_252.In(logic_uScript_IsPlayerInTrigger_triggerAreaName_252);
				bool inRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_252.InRange;
				bool outOfRange = logic_uScript_IsPlayerInTrigger_uScript_IsPlayerInTrigger_252.OutOfRange;
				if (inRange)
				{
					Relay_In_212();
				}
				if (outOfRange)
				{
					Relay_Pause_198();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at Distance/Is Player in Trigger Area.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_254()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("441a0091-b060-48ec-b9bf-5a133f70af2e", "Hide_Mission_Prompt_on_Block", Relay_In_254))
			{
				logic_uScript_MissionPromptBlock_Hide_targetBlock_254 = local_TerminalBlock_TankBlock;
				logic_uScript_MissionPromptBlock_Hide_uScript_MissionPromptBlock_Hide_254.In(logic_uScript_MissionPromptBlock_Hide_targetBlock_254);
				if (logic_uScript_MissionPromptBlock_Hide_uScript_MissionPromptBlock_Hide_254.Out)
				{
					Relay_False_256();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at Hide Mission Prompt on Block.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_True_256()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("316c6f29-1c73-4928-8c39-57cb9da09dd0", "Set_Bool", Relay_True_256))
			{
				logic_uScriptAct_SetBool_uScriptAct_SetBool_256.True(out logic_uScriptAct_SetBool_Target_256);
				local_WaitingOnPrompt_System_Boolean = logic_uScriptAct_SetBool_Target_256;
				if (logic_uScriptAct_SetBool_uScriptAct_SetBool_256.Out)
				{
					Relay_False_264();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at Set Bool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_False_256()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("316c6f29-1c73-4928-8c39-57cb9da09dd0", "Set_Bool", Relay_False_256))
			{
				logic_uScriptAct_SetBool_uScriptAct_SetBool_256.False(out logic_uScriptAct_SetBool_Target_256);
				local_WaitingOnPrompt_System_Boolean = logic_uScriptAct_SetBool_Target_256;
				if (logic_uScriptAct_SetBool_uScriptAct_SetBool_256.Out)
				{
					Relay_False_264();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at Set Bool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_True_261()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("4708c8d5-e1b9-4e99-8aa5-10d4df0ad18d", "Set_Bool", Relay_True_261))
			{
				logic_uScriptAct_SetBool_uScriptAct_SetBool_261.True(out logic_uScriptAct_SetBool_Target_261);
				local_HighlightBlock_System_Boolean = logic_uScriptAct_SetBool_Target_261;
				if (logic_uScriptAct_SetBool_uScriptAct_SetBool_261.Out)
				{
					Relay_In_217();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at Set Bool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_False_261()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("4708c8d5-e1b9-4e99-8aa5-10d4df0ad18d", "Set_Bool", Relay_False_261))
			{
				logic_uScriptAct_SetBool_uScriptAct_SetBool_261.False(out logic_uScriptAct_SetBool_Target_261);
				local_HighlightBlock_System_Boolean = logic_uScriptAct_SetBool_Target_261;
				if (logic_uScriptAct_SetBool_uScriptAct_SetBool_261.Out)
				{
					Relay_In_217();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at Set Bool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_True_262()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("c80ca2a7-3502-4e7d-9ab0-873c97c2d945", "Set_Bool", Relay_True_262))
			{
				logic_uScriptAct_SetBool_uScriptAct_SetBool_262.True(out logic_uScriptAct_SetBool_Target_262);
				local_HighlightBlock_System_Boolean = logic_uScriptAct_SetBool_Target_262;
				if (logic_uScriptAct_SetBool_uScriptAct_SetBool_262.Out)
				{
					Relay_In_231();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at Set Bool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_False_262()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("c80ca2a7-3502-4e7d-9ab0-873c97c2d945", "Set_Bool", Relay_False_262))
			{
				logic_uScriptAct_SetBool_uScriptAct_SetBool_262.False(out logic_uScriptAct_SetBool_Target_262);
				local_HighlightBlock_System_Boolean = logic_uScriptAct_SetBool_Target_262;
				if (logic_uScriptAct_SetBool_uScriptAct_SetBool_262.Out)
				{
					Relay_In_231();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at Set Bool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_True_264()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("339c49af-c25f-4bb8-aa5b-b619df191b52", "Set_Bool", Relay_True_264))
			{
				logic_uScriptAct_SetBool_uScriptAct_SetBool_264.True(out logic_uScriptAct_SetBool_Target_264);
				local_HighlightBlock_System_Boolean = logic_uScriptAct_SetBool_Target_264;
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at Set Bool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_False_264()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("339c49af-c25f-4bb8-aa5b-b619df191b52", "Set_Bool", Relay_False_264))
			{
				logic_uScriptAct_SetBool_uScriptAct_SetBool_264.False(out logic_uScriptAct_SetBool_Target_264);
				local_HighlightBlock_System_Boolean = logic_uScriptAct_SetBool_Target_264;
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at Set Bool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Save_Out_267()
	{
		if (relayCallCount++ < 1000)
		{
			CheckDebugBreak("c7191e7c-0802-495e-8c8d-cf096a0ca26c", "", Relay_Save_Out_267);
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Load_Out_267()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("c7191e7c-0802-495e-8c8d-cf096a0ca26c", "", Relay_Load_Out_267))
			{
				Relay_In_36();
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Restart_Out_267()
	{
		if (relayCallCount++ < 1000)
		{
			CheckDebugBreak("c7191e7c-0802-495e-8c8d-cf096a0ca26c", "", Relay_Restart_Out_267);
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Save_267()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("c7191e7c-0802-495e-8c8d-cf096a0ca26c", "", Relay_Save_267))
			{
				logic_SubGraph_SaveLoadBool_boolean_267 = local_HighlightBlock_System_Boolean;
				logic_SubGraph_SaveLoadBool_boolAsVariable_267 = local_HighlightBlock_System_Boolean;
				logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_267.Save(ref logic_SubGraph_SaveLoadBool_boolean_267, logic_SubGraph_SaveLoadBool_boolAsVariable_267, logic_SubGraph_SaveLoadBool_uniqueID_267);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Load_267()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("c7191e7c-0802-495e-8c8d-cf096a0ca26c", "", Relay_Load_267))
			{
				logic_SubGraph_SaveLoadBool_boolean_267 = local_HighlightBlock_System_Boolean;
				logic_SubGraph_SaveLoadBool_boolAsVariable_267 = local_HighlightBlock_System_Boolean;
				logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_267.Load(ref logic_SubGraph_SaveLoadBool_boolean_267, logic_SubGraph_SaveLoadBool_boolAsVariable_267, logic_SubGraph_SaveLoadBool_uniqueID_267);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Set_True_267()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("c7191e7c-0802-495e-8c8d-cf096a0ca26c", "", Relay_Set_True_267))
			{
				logic_SubGraph_SaveLoadBool_boolean_267 = local_HighlightBlock_System_Boolean;
				logic_SubGraph_SaveLoadBool_boolAsVariable_267 = local_HighlightBlock_System_Boolean;
				logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_267.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_267, logic_SubGraph_SaveLoadBool_boolAsVariable_267, logic_SubGraph_SaveLoadBool_uniqueID_267);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_Set_False_267()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("c7191e7c-0802-495e-8c8d-cf096a0ca26c", "", Relay_Set_False_267))
			{
				logic_SubGraph_SaveLoadBool_boolean_267 = local_HighlightBlock_System_Boolean;
				logic_SubGraph_SaveLoadBool_boolAsVariable_267 = local_HighlightBlock_System_Boolean;
				logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_267.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_267, logic_SubGraph_SaveLoadBool_boolAsVariable_267, logic_SubGraph_SaveLoadBool_uniqueID_267);
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at SubGraph_SaveLoadBool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_True_268()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("737d4f68-3343-404d-90da-2d663644d03f", "Set_Bool", Relay_True_268))
			{
				logic_uScriptAct_SetBool_uScriptAct_SetBool_268.True(out logic_uScriptAct_SetBool_Target_268);
				local_HighlightBlock_System_Boolean = logic_uScriptAct_SetBool_Target_268;
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at Set Bool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_False_268()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("737d4f68-3343-404d-90da-2d663644d03f", "Set_Bool", Relay_False_268))
			{
				logic_uScriptAct_SetBool_uScriptAct_SetBool_268.False(out logic_uScriptAct_SetBool_Target_268);
				local_HighlightBlock_System_Boolean = logic_uScriptAct_SetBool_Target_268;
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at Set Bool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_True_270()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("c58b301b-0678-4229-9467-53a9261c7124", "Set_Bool", Relay_True_270))
			{
				logic_uScriptAct_SetBool_uScriptAct_SetBool_270.True(out logic_uScriptAct_SetBool_Target_270);
				local_HighlightBlock_System_Boolean = logic_uScriptAct_SetBool_Target_270;
				if (logic_uScriptAct_SetBool_uScriptAct_SetBool_270.Out)
				{
					Relay_In_218();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at Set Bool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_False_270()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("c58b301b-0678-4229-9467-53a9261c7124", "Set_Bool", Relay_False_270))
			{
				logic_uScriptAct_SetBool_uScriptAct_SetBool_270.False(out logic_uScriptAct_SetBool_Target_270);
				local_HighlightBlock_System_Boolean = logic_uScriptAct_SetBool_Target_270;
				if (logic_uScriptAct_SetBool_uScriptAct_SetBool_270.Out)
				{
					Relay_In_218();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at Set Bool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_In_274()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("a59094fa-f601-4200-ac6b-ae721628c6ce", "Tank_Get_Tank_Block", Relay_In_274))
			{
				logic_uScript_GetTankBlock_tank_274 = local_PaymentPointTech_Tank;
				logic_uScript_GetTankBlock_blockType_274 = interactableBlockType;
				logic_uScript_GetTankBlock_Return_274 = logic_uScript_GetTankBlock_uScript_GetTankBlock_274.In(logic_uScript_GetTankBlock_tank_274, logic_uScript_GetTankBlock_blockType_274);
				local_TerminalBlock_TankBlock = logic_uScript_GetTankBlock_Return_274;
				if (logic_uScript_GetTankBlock_uScript_GetTankBlock_274.Returned)
				{
					Relay_False_280();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at Tank/Get Tank Block.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_True_280()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("a2b45470-910e-40e0-8eee-3f24115bd365", "Set_Bool", Relay_True_280))
			{
				logic_uScriptAct_SetBool_uScriptAct_SetBool_280.True(out logic_uScriptAct_SetBool_Target_280);
				local_WaitingOnPrompt_System_Boolean = logic_uScriptAct_SetBool_Target_280;
				if (logic_uScriptAct_SetBool_uScriptAct_SetBool_280.Out)
				{
					Relay_In_24();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at Set Bool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void Relay_False_280()
	{
		if (relayCallCount++ < 1000)
		{
			if (!CheckDebugBreak("a2b45470-910e-40e0-8eee-3f24115bd365", "Set_Bool", Relay_False_280))
			{
				logic_uScriptAct_SetBool_uScriptAct_SetBool_280.False(out logic_uScriptAct_SetBool_Target_280);
				local_WaitingOnPrompt_System_Boolean = logic_uScriptAct_SetBool_Target_280;
				if (logic_uScriptAct_SetBool_uScriptAct_SetBool_280.Out)
				{
					Relay_In_24();
				}
			}
		}
		else
		{
			uScriptDebug.Log("Possible infinite loop detected in uScript Mission_SetPiece_BridgeTroll.uscript at Set Bool.  If this is in error you can change the Maximum Node Recursion in the Preferences Panel and regenerate the script.", uScriptDebug.Type.Error);
		}
	}

	private void UpdateEditorValues()
	{
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_SetPiece_BridgeTroll.uscript:msg05LicensePurchased", msg05LicensePurchased);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("b20b9704-346a-4835-b1cf-c6d04c5aef75", msg05LicensePurchased);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_SetPiece_BridgeTroll.uscript:NPCSpawnData", NPCSpawnData);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("ed578749-6a98-4212-b05a-90629b341faa", NPCSpawnData);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_SetPiece_BridgeTroll.uscript:Stage", local_Stage_System_Int32);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("a168b2d8-209c-45ec-b3a4-8df3bcec7be3", local_Stage_System_Int32);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_SetPiece_BridgeTroll.uscript:distNPCFound", distNPCFound);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("1198e806-1b9b-4c24-800e-15d1cb2bfbc2", distNPCFound);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_SetPiece_BridgeTroll.uscript:msg01Intro", msg01Intro);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("c184c131-5da9-4f94-98db-8cfcc0186b39", msg01Intro);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_SetPiece_BridgeTroll.uscript:msg01Shown", local_msg01Shown_System_Boolean);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("03f10350-4fb4-4f4b-8197-4b020b1e3dc1", local_msg01Shown_System_Boolean);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_SetPiece_BridgeTroll.uscript:msgPromptAccept", msgPromptAccept);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("77ea6421-15eb-4ece-8a0d-21911254f028", msgPromptAccept);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_SetPiece_BridgeTroll.uscript:msgPromptDecline", msgPromptDecline);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("2f1a128e-81dc-46c0-993d-6cb973fca45e", msgPromptDecline);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_SetPiece_BridgeTroll.uscript:msgPromptNoMoney", msgPromptNoMoney);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("24a2b539-89dc-4cff-8092-31919e600506", msgPromptNoMoney);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_SetPiece_BridgeTroll.uscript:msgPromptText", msgPromptText);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("89138e90-d7fe-4815-bf63-2094efe622bd", msgPromptText);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_SetPiece_BridgeTroll.uscript:msg03bNotEnoughMoney", msg03bNotEnoughMoney);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("1466e145-de28-4a44-85b5-4710b0ed09f5", msg03bNotEnoughMoney);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_SetPiece_BridgeTroll.uscript:msg03aPurchaseDeclined", msg03aPurchaseDeclined);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("3f2a5770-7305-48f2-8b2f-7cb3cf9eceb3", msg03aPurchaseDeclined);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_SetPiece_BridgeTroll.uscript:87", local_87_System_Int32);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("61374bed-9a74-407b-84dc-92d5c0325900", local_87_System_Int32);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_SetPiece_BridgeTroll.uscript:msg03aShown", local_msg03aShown_System_Boolean);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("eee518ae-352c-4867-942b-88ec4e8b85bc", local_msg03aShown_System_Boolean);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_SetPiece_BridgeTroll.uscript:msg03bShown", local_msg03bShown_System_Boolean);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("1e0b6e2d-1f4d-4c30-b566-0975c328ada9", local_msg03bShown_System_Boolean);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_SetPiece_BridgeTroll.uscript:NearNPC", local_NearNPC_System_Boolean);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("3e85a68c-d5ec-4ee6-aca1-bcdf229e376a", local_NearNPC_System_Boolean);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_SetPiece_BridgeTroll.uscript:msg02ClickScreen", msg02ClickScreen);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("119693e1-e9e8-4854-a4ea-f563ab2576be", msg02ClickScreen);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_SetPiece_BridgeTroll.uscript:msg02ClickScreen_Pad", msg02ClickScreen_Pad);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("936f579d-036a-4a1a-8ab1-b2001f21d81a", msg02ClickScreen_Pad);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_SetPiece_BridgeTroll.uscript:messageSpeaker", messageSpeaker);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("26ce6040-9e0b-4af5-b3b0-dad128492c25", messageSpeaker);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_SetPiece_BridgeTroll.uscript:msgAngered", msgAngered);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("1e1aaf48-0c4b-48cd-9abd-7cc031f50709", msgAngered);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_SetPiece_BridgeTroll.uscript:AngeredNPC", local_AngeredNPC_System_Boolean);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("53ab58d7-3106-4deb-9da1-5062e7951cc7", local_AngeredNPC_System_Boolean);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_SetPiece_BridgeTroll.uscript:clearMsgTagWhenAngry", clearMsgTagWhenAngry);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("855c2d32-0017-408c-b716-10f9f84df454", clearMsgTagWhenAngry);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_SetPiece_BridgeTroll.uscript:VendorSpawnData", VendorSpawnData);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("a5b09377-2802-472e-9358-bafd9a0cf099", VendorSpawnData);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_SetPiece_BridgeTroll.uscript:PaymentTechs", local_PaymentTechs_TankArray);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("5f12ccfa-cde9-4742-ab4a-6c310f79e79b", local_PaymentTechs_TankArray);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_SetPiece_BridgeTroll.uscript:msgMissionComplete", msgMissionComplete);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("61c0b0f0-9aad-4403-9b0e-657a89a8a993", msgMissionComplete);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_SetPiece_BridgeTroll.uscript:KillBoxTrigger", KillBoxTrigger);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("89a91726-909c-4d2e-a7e8-16cf3c085621", KillBoxTrigger);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_SetPiece_BridgeTroll.uscript:NPCTechs", local_NPCTechs_TankArray);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("3e01d68d-ad72-4945-bf46-ca94c3a9bab0", local_NPCTechs_TankArray);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_SetPiece_BridgeTroll.uscript:189", local_189_System_Int32);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("92473c0f-93d0-4f69-a72b-f6624bed2306", local_189_System_Int32);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_SetPiece_BridgeTroll.uscript:NPCTech", local_NPCTech_Tank);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("c811d27d-cdcd-4df7-b0ee-c54c367437e4", local_NPCTech_Tank);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_SetPiece_BridgeTroll.uscript:NPCDespawnParticleEffect", NPCDespawnParticleEffect);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("489871f0-ff13-4e59-bedd-16c690c3aa83", NPCDespawnParticleEffect);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_SetPiece_BridgeTroll.uscript:NPCFlyAwayAI", NPCFlyAwayAI);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("63cfee5d-a372-40a7-a49f-af6e2cd5255e", NPCFlyAwayAI);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_SetPiece_BridgeTroll.uscript:msgMissionCompleteBridgeFall", msgMissionCompleteBridgeFall);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("e1732fa5-da58-46b2-aa11-167c0c0bda40", msgMissionCompleteBridgeFall);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_SetPiece_BridgeTroll.uscript:genericSpeaker", genericSpeaker);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("37edf627-4dd0-4903-a3a5-04929d6e1ab5", genericSpeaker);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_SetPiece_BridgeTroll.uscript:FallenOffBridge", local_FallenOffBridge_System_Boolean);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("60a33a3f-5637-4763-8d65-0bbc52e99196", local_FallenOffBridge_System_Boolean);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_SetPiece_BridgeTroll.uscript:LicensePurchased", local_LicensePurchased_System_Boolean);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("e44e99c6-2140-4f5e-9dd7-57f449cca83f", local_LicensePurchased_System_Boolean);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_SetPiece_BridgeTroll.uscript:Init", local_Init_System_Boolean);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("ab869e08-2ef5-4d9b-b933-7c4bfd0f3b4b", local_Init_System_Boolean);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_SetPiece_BridgeTroll.uscript:221", local_221_TankBlock);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("bbf0fb34-bd1e-4ee9-a446-049c9f0d0d58", local_221_TankBlock);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_SetPiece_BridgeTroll.uscript:clearMsgTagWhenNormal", clearMsgTagWhenNormal);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("f8be433d-3ead-4764-bd1f-b029632a5906", clearMsgTagWhenNormal);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_SetPiece_BridgeTroll.uscript:Msg02ClickScreen", local_Msg02ClickScreen_ManOnScreenMessages_OnScreenMessage);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("456ed435-cd91-42db-a92b-31edbc9f2191", local_Msg02ClickScreen_ManOnScreenMessages_OnScreenMessage);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_SetPiece_BridgeTroll.uscript:229", local_229_System_Boolean);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("ec9fa96c-65b7-4e00-8ccf-0d304d9b151c", local_229_System_Boolean);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_SetPiece_BridgeTroll.uscript:CurrentMoney", local_CurrentMoney_System_Int32);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("bd5495b1-4c8e-42b1-ac45-550b92af6cc0", local_CurrentMoney_System_Int32);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_SetPiece_BridgeTroll.uscript:LicenseCost", LicenseCost);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("a8309f3e-3bf8-453b-b816-fe0382c87aed", LicenseCost);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_SetPiece_BridgeTroll.uscript:_DEBUGIgnoreMoneyCheck", _DEBUGIgnoreMoneyCheck);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("70741082-4de2-4474-b2e9-fb236cf8a1ea", _DEBUGIgnoreMoneyCheck);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_SetPiece_BridgeTroll.uscript:HasEnoughMoney", local_HasEnoughMoney_System_Boolean);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("41bb1506-29ab-4433-967a-3fb71ef08e63", local_HasEnoughMoney_System_Boolean);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_SetPiece_BridgeTroll.uscript:AngryZone", AngryZone);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("c21fbd9f-22f0-4457-9d9e-5f479ed70f03", AngryZone);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_SetPiece_BridgeTroll.uscript:HighlightBlock", local_HighlightBlock_System_Boolean);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("745a4a5d-e008-4840-b5b0-684411f7633a", local_HighlightBlock_System_Boolean);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_SetPiece_BridgeTroll.uscript:PaymentPointTech", local_PaymentPointTech_Tank);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("8be84d01-81a7-4007-ae97-bbd34214a28d", local_PaymentPointTech_Tank);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_SetPiece_BridgeTroll.uscript:TerminalBlock", local_TerminalBlock_TankBlock);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("7ba63130-8564-4b00-b4f6-80d56c43dada", local_TerminalBlock_TankBlock);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_SetPiece_BridgeTroll.uscript:interactableBlockType", interactableBlockType);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("f3de9c1b-3c4e-49f8-a7c7-dc5e9f5a7a2f", interactableBlockType);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("Mission_SetPiece_BridgeTroll.uscript:WaitingOnPrompt", local_WaitingOnPrompt_System_Boolean);
		uScript_MasterComponent.LatestMasterComponent.UpdateNodeValue("8d6bac44-670a-4c2b-94a0-5df2632a3450", local_WaitingOnPrompt_System_Boolean);
	}

	private bool CheckDebugBreak(string guid, string name, ContinueExecution method)
	{
		if (m_Breakpoint)
		{
			return true;
		}
		if (uScript_MasterComponent.FindBreakpoint(guid))
		{
			if (!(uScript_MasterComponent.LatestMasterComponent.CurrentBreakpoint == guid))
			{
				uScript_MasterComponent.LatestMasterComponent.CurrentBreakpoint = guid;
				UpdateEditorValues();
				Debug.Log(("uScript BREAK Node:" + name + " ((Time: " + Time.time) ?? "");
				Debug.Break();
				m_ContinueExecution = method.Invoke;
				m_Breakpoint = true;
				return true;
			}
			uScript_MasterComponent.LatestMasterComponent.CurrentBreakpoint = "";
		}
		return false;
	}
}
