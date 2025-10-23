using System;
using UnityEngine;

[Serializable]
[FriendlyName("Untitled", "")]
[NodePath("Graphs")]
public class Challenge_Gauntlet : uScriptLogic
{
	private GameObject parentGameObject;

	private uScript_GUI thisScriptsOnGuiListener;

	private bool m_RegisteredForEvents;

	public LocalisedString Accept;

	private float local_102_System_Single;

	private float local_90_System_Single;

	private int local_currentCheckpoint_System_Int32;

	private bool local_GauntletComplete_System_Boolean;

	private bool local_GauntletStarted_System_Boolean;

	private bool local_IntroScreenFinished_System_Boolean;

	private string local_msg_System_String = "msg";

	private ManOnScreenMessages.OnScreenMessage local_MsgBuildTech_ManOnScreenMessages_OnScreenMessage;

	private bool local_MsgBuildTechTriggered_System_Boolean;

	private ManOnScreenMessages.OnScreenMessage local_MsgReadyToStart_ManOnScreenMessages_OnScreenMessage;

	private bool local_MsgReadyToStartTriggered_System_Boolean;

	public LocalisedString[] msgBuildTech = new LocalisedString[0];

	public LocalisedString[] msgBuildTech_Pad = new LocalisedString[0];

	public LocalisedString[] msgGauntletComplete = new LocalisedString[0];

	public LocalisedString MsgIntroScreen;

	public LocalisedString[] msgOutOfBounds = new LocalisedString[0];

	public LocalisedString[] msgOutOfBoundsWarning = new LocalisedString[0];

	public LocalisedString[] msgReadyToStart = new LocalisedString[0];

	public float startAreaMsgDuration;

	public float startAreaMsgRefreshTime;

	private GameObject owner_Connection_1;

	private GameObject owner_Connection_44;

	private GameObject owner_Connection_59;

	private GameObject owner_Connection_126;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_2 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_2 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_2 = ManOnScreenMessages.MessagePriority.Medium;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_2 = true;

	private string logic_uScript_AddOnScreenMessage_tag_2 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_2;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_2;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_2;

	private bool logic_uScript_AddOnScreenMessage_Out_2 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_2 = true;

	private uScript_IsPlayerInBeam logic_uScript_IsPlayerInBeam_uScript_IsPlayerInBeam_4 = new uScript_IsPlayerInBeam();

	private bool logic_uScript_IsPlayerInBeam_True_4 = true;

	private bool logic_uScript_IsPlayerInBeam_False_4 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_5 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_5 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_5 = ManOnScreenMessages.MessagePriority.Medium;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_5 = true;

	private string logic_uScript_AddOnScreenMessage_tag_5 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_5;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_5;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_5;

	private bool logic_uScript_AddOnScreenMessage_Out_5 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_5 = true;

	private uScript_RemoveOnScreenMessage logic_uScript_RemoveOnScreenMessage_uScript_RemoveOnScreenMessage_11 = new uScript_RemoveOnScreenMessage();

	private ManOnScreenMessages.OnScreenMessage logic_uScript_RemoveOnScreenMessage_onScreenMessage_11;

	private bool logic_uScript_RemoveOnScreenMessage_instant_11;

	private bool logic_uScript_RemoveOnScreenMessage_Out_11 = true;

	private uScript_RemoveOnScreenMessage logic_uScript_RemoveOnScreenMessage_uScript_RemoveOnScreenMessage_12 = new uScript_RemoveOnScreenMessage();

	private ManOnScreenMessages.OnScreenMessage logic_uScript_RemoveOnScreenMessage_onScreenMessage_12;

	private bool logic_uScript_RemoveOnScreenMessage_instant_12;

	private bool logic_uScript_RemoveOnScreenMessage_Out_12 = true;

	private uScript_GetCheckPointIndex logic_uScript_GetCheckPointIndex_uScript_GetCheckPointIndex_15 = new uScript_GetCheckPointIndex();

	private int logic_uScript_GetCheckPointIndex_Return_15;

	private bool logic_uScript_GetCheckPointIndex_Out_15 = true;

	private uScriptCon_CompareInt logic_uScriptCon_CompareInt_uScriptCon_CompareInt_17 = new uScriptCon_CompareInt();

	private int logic_uScriptCon_CompareInt_A_17;

	private int logic_uScriptCon_CompareInt_B_17;

	private bool logic_uScriptCon_CompareInt_GreaterThan_17 = true;

	private bool logic_uScriptCon_CompareInt_GreaterThanOrEqualTo_17 = true;

	private bool logic_uScriptCon_CompareInt_EqualTo_17 = true;

	private bool logic_uScriptCon_CompareInt_NotEqualTo_17 = true;

	private bool logic_uScriptCon_CompareInt_LessThanOrEqualTo_17 = true;

	private bool logic_uScriptCon_CompareInt_LessThan_17 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_19 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_19 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_19 = true;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_19 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_21 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_21;

	private bool logic_uScriptAct_SetBool_Out_21 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_21 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_21 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_23 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_23;

	private bool logic_uScriptCon_CompareBool_True_23 = true;

	private bool logic_uScriptCon_CompareBool_False_23 = true;

	private uScript_DisablePlayerInput logic_uScript_DisablePlayerInput_uScript_DisablePlayerInput_26 = new uScript_DisablePlayerInput();

	private bool logic_uScript_DisablePlayerInput_disableInput_26;

	private bool logic_uScript_DisablePlayerInput_Out_26 = true;

	private uScript_ShowPrompt logic_uScript_ShowPrompt_uScript_ShowPrompt_28 = new uScript_ShowPrompt();

	private LocalisedString logic_uScript_ShowPrompt_bodyText_28;

	private LocalisedString logic_uScript_ShowPrompt_acceptButtonText_28;

	private LocalisedString logic_uScript_ShowPrompt_rejectButtonText_28;

	private uScript_ShowPrompt.Context logic_uScript_ShowPrompt_Return_28;

	private bool logic_uScript_ShowPrompt_Out_28 = true;

	private uScript_DisablePlayerInput logic_uScript_DisablePlayerInput_uScript_DisablePlayerInput_29 = new uScript_DisablePlayerInput();

	private bool logic_uScript_DisablePlayerInput_disableInput_29 = true;

	private bool logic_uScript_DisablePlayerInput_Out_29 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_30 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_30;

	private bool logic_uScriptCon_CompareBool_True_30 = true;

	private bool logic_uScriptCon_CompareBool_False_30 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_32 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_32;

	private bool logic_uScriptAct_SetBool_Out_32 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_32 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_32 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_36 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_36 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_36 = ManOnScreenMessages.MessagePriority.High;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_36;

	private string logic_uScript_AddOnScreenMessage_tag_36 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_36;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_36;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_36;

	private bool logic_uScript_AddOnScreenMessage_Out_36 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_36 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_38 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_38 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_38 = true;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_38 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_39 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_39;

	private bool logic_uScriptCon_CompareBool_True_39 = true;

	private bool logic_uScriptCon_CompareBool_False_39 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_41 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_41;

	private bool logic_uScriptAct_SetBool_Out_41 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_41 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_41 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_46 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_46 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_46 = ManOnScreenMessages.MessagePriority.High;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_46;

	private string logic_uScript_AddOnScreenMessage_tag_46 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_46;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_46;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_46;

	private bool logic_uScript_AddOnScreenMessage_Out_46 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_46 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_49 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_49 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_49 = ManOnScreenMessages.MessagePriority.High;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_49;

	private string logic_uScript_AddOnScreenMessage_tag_49 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_49;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_49;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_49;

	private bool logic_uScript_AddOnScreenMessage_Out_49 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_49 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_52 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_52 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_52 = true;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_52 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_53 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_53 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_53 = true;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_53 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_55 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_55;

	private bool logic_uScriptCon_CompareBool_True_55 = true;

	private bool logic_uScriptCon_CompareBool_False_55 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_58 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_58;

	private bool logic_uScriptCon_CompareBool_True_58 = true;

	private bool logic_uScriptCon_CompareBool_False_58 = true;

	private uScript_DisablePlayerInput logic_uScript_DisablePlayerInput_uScript_DisablePlayerInput_60 = new uScript_DisablePlayerInput();

	private bool logic_uScript_DisablePlayerInput_disableInput_60 = true;

	private bool logic_uScript_DisablePlayerInput_Out_60 = true;

	private uScript_GraphEvents logic_uScript_GraphEvents_uScript_GraphEvents_65 = new uScript_GraphEvents();

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_67 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_67;

	private bool logic_uScriptAct_SetBool_Out_67 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_67 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_67 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_68 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_68;

	private bool logic_uScriptAct_SetBool_Out_68 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_68 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_68 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_69 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_69;

	private bool logic_uScriptAct_SetBool_Out_69 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_69 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_69 = true;

	private uScript_BlankNode logic_uScript_BlankNode_uScript_BlankNode_73 = new uScript_BlankNode();

	private bool logic_uScript_BlankNode_Out_73 = true;

	private uScript_Gauntlet_IsTutorialActive logic_uScript_Gauntlet_IsTutorialActive_uScript_Gauntlet_IsTutorialActive_75 = new uScript_Gauntlet_IsTutorialActive();

	private bool logic_uScript_Gauntlet_IsTutorialActive_True_75 = true;

	private bool logic_uScript_Gauntlet_IsTutorialActive_False_75 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_76 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_76;

	private int logic_uScriptAct_SetInt_Target_76;

	private bool logic_uScriptAct_SetInt_Out_76 = true;

	private uScript_DisablePlayerInput logic_uScript_DisablePlayerInput_uScript_DisablePlayerInput_78 = new uScript_DisablePlayerInput();

	private bool logic_uScript_DisablePlayerInput_disableInput_78;

	private bool logic_uScript_DisablePlayerInput_Out_78 = true;

	private uScript_BlockPaletteOptions logic_uScript_BlockPaletteOptions_uScript_BlockPaletteOptions_79 = new uScript_BlockPaletteOptions();

	private bool logic_uScript_BlockPaletteOptions_show_79;

	private bool logic_uScript_BlockPaletteOptions_open_79;

	private bool logic_uScript_BlockPaletteOptions_Out_79 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_80 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_80;

	private bool logic_uScriptAct_SetBool_Out_80 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_80 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_80 = true;

	private uScript_BlockPaletteOptions logic_uScript_BlockPaletteOptions_uScript_BlockPaletteOptions_82 = new uScript_BlockPaletteOptions();

	private bool logic_uScript_BlockPaletteOptions_show_82 = true;

	private bool logic_uScript_BlockPaletteOptions_open_82 = true;

	private bool logic_uScript_BlockPaletteOptions_Out_82 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_83 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_83 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_83 = true;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_83 = true;

	private uScriptAct_Stopwatch logic_uScriptAct_Stopwatch_uScriptAct_Stopwatch_85 = new uScriptAct_Stopwatch();

	private float logic_uScriptAct_Stopwatch_Seconds_85;

	private bool logic_uScriptAct_Stopwatch_Started_85 = true;

	private bool logic_uScriptAct_Stopwatch_Stopped_85 = true;

	private bool logic_uScriptAct_Stopwatch_Reset_85 = true;

	private bool logic_uScriptAct_Stopwatch_CheckedTime_85 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_87 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_87;

	private bool logic_uScriptCon_CompareBool_True_87 = true;

	private bool logic_uScriptCon_CompareBool_False_87 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_88 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_88;

	private bool logic_uScriptAct_SetBool_Out_88 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_88 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_88 = true;

	private uScriptCon_CompareFloat logic_uScriptCon_CompareFloat_uScriptCon_CompareFloat_89 = new uScriptCon_CompareFloat();

	private float logic_uScriptCon_CompareFloat_A_89;

	private float logic_uScriptCon_CompareFloat_B_89;

	private bool logic_uScriptCon_CompareFloat_GreaterThan_89 = true;

	private bool logic_uScriptCon_CompareFloat_GreaterThanOrEqualTo_89 = true;

	private bool logic_uScriptCon_CompareFloat_EqualTo_89 = true;

	private bool logic_uScriptCon_CompareFloat_NotEqualTo_89 = true;

	private bool logic_uScriptCon_CompareFloat_LessThanOrEqualTo_89 = true;

	private bool logic_uScriptCon_CompareFloat_LessThan_89 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_93 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_93;

	private bool logic_uScriptAct_SetBool_Out_93 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_93 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_93 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_94 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_94;

	private bool logic_uScriptAct_SetBool_Out_94 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_94 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_94 = true;

	private uScriptCon_CompareFloat logic_uScriptCon_CompareFloat_uScriptCon_CompareFloat_97 = new uScriptCon_CompareFloat();

	private float logic_uScriptCon_CompareFloat_A_97;

	private float logic_uScriptCon_CompareFloat_B_97;

	private bool logic_uScriptCon_CompareFloat_GreaterThan_97 = true;

	private bool logic_uScriptCon_CompareFloat_GreaterThanOrEqualTo_97 = true;

	private bool logic_uScriptCon_CompareFloat_EqualTo_97 = true;

	private bool logic_uScriptCon_CompareFloat_NotEqualTo_97 = true;

	private bool logic_uScriptCon_CompareFloat_LessThanOrEqualTo_97 = true;

	private bool logic_uScriptCon_CompareFloat_LessThan_97 = true;

	private uScriptAct_Stopwatch logic_uScriptAct_Stopwatch_uScriptAct_Stopwatch_98 = new uScriptAct_Stopwatch();

	private float logic_uScriptAct_Stopwatch_Seconds_98;

	private bool logic_uScriptAct_Stopwatch_Started_98 = true;

	private bool logic_uScriptAct_Stopwatch_Stopped_98 = true;

	private bool logic_uScriptAct_Stopwatch_Reset_98 = true;

	private bool logic_uScriptAct_Stopwatch_CheckedTime_98 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_100 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_100;

	private bool logic_uScriptCon_CompareBool_True_100 = true;

	private bool logic_uScriptCon_CompareBool_False_100 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_101 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_101;

	private bool logic_uScriptAct_SetBool_Out_101 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_101 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_101 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_103 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_103;

	private bool logic_uScriptAct_SetBool_Out_103 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_103 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_103 = true;

	private uScriptCon_CompareFloat logic_uScriptCon_CompareFloat_uScriptCon_CompareFloat_105 = new uScriptCon_CompareFloat();

	private float logic_uScriptCon_CompareFloat_A_105;

	private float logic_uScriptCon_CompareFloat_B_105;

	private bool logic_uScriptCon_CompareFloat_GreaterThan_105 = true;

	private bool logic_uScriptCon_CompareFloat_GreaterThanOrEqualTo_105 = true;

	private bool logic_uScriptCon_CompareFloat_EqualTo_105 = true;

	private bool logic_uScriptCon_CompareFloat_NotEqualTo_105 = true;

	private bool logic_uScriptCon_CompareFloat_LessThanOrEqualTo_105 = true;

	private bool logic_uScriptCon_CompareFloat_LessThan_105 = true;

	private uScriptCon_CompareFloat logic_uScriptCon_CompareFloat_uScriptCon_CompareFloat_107 = new uScriptCon_CompareFloat();

	private float logic_uScriptCon_CompareFloat_A_107;

	private float logic_uScriptCon_CompareFloat_B_107;

	private bool logic_uScriptCon_CompareFloat_GreaterThan_107 = true;

	private bool logic_uScriptCon_CompareFloat_GreaterThanOrEqualTo_107 = true;

	private bool logic_uScriptCon_CompareFloat_EqualTo_107 = true;

	private bool logic_uScriptCon_CompareFloat_NotEqualTo_107 = true;

	private bool logic_uScriptCon_CompareFloat_LessThanOrEqualTo_107 = true;

	private bool logic_uScriptCon_CompareFloat_LessThan_107 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_108 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_108;

	private bool logic_uScriptAct_SetBool_Out_108 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_108 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_108 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_110 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_110 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_110 = true;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_110 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_117 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_117;

	private bool logic_uScriptAct_SetBool_Out_117 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_117 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_117 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_119 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_119;

	private bool logic_uScriptAct_SetBool_Out_119 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_119 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_119 = true;

	private uScript_BlockPaletteOptions logic_uScript_BlockPaletteOptions_uScript_BlockPaletteOptions_123 = new uScript_BlockPaletteOptions();

	private bool logic_uScript_BlockPaletteOptions_show_123;

	private bool logic_uScript_BlockPaletteOptions_open_123;

	private bool logic_uScript_BlockPaletteOptions_Out_123 = true;

	private uScript_Gauntlet_SetStartTutorialButtonVisibility logic_uScript_Gauntlet_SetStartTutorialButtonVisibility_uScript_Gauntlet_SetStartTutorialButtonVisibility_124 = new uScript_Gauntlet_SetStartTutorialButtonVisibility();

	private bool logic_uScript_Gauntlet_SetStartTutorialButtonVisibility_enabled_124 = true;

	private bool logic_uScript_Gauntlet_SetStartTutorialButtonVisibility_Out_124 = true;

	private uScript_GetJoypadControlMode logic_uScript_GetJoypadControlMode_uScript_GetJoypadControlMode_129 = new uScript_GetJoypadControlMode();

	private bool logic_uScript_GetJoypadControlMode_Joypad_129 = true;

	private bool logic_uScript_GetJoypadControlMode_MouseAndKeyboard_129 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_133 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_133 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_133 = ManOnScreenMessages.MessagePriority.Medium;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_133 = true;

	private string logic_uScript_AddOnScreenMessage_tag_133 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_133;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_133;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_133;

	private bool logic_uScript_AddOnScreenMessage_Out_133 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_133 = true;

	private CheckpointChallenge.EndReason event_UnityEngine_GameObject_EndReason_125;

	private float event_UnityEngine_GameObject_EndTime_125;

	private int event_UnityEngine_GameObject_CheckpointIndex_127;

	private void SyncUnityHooks()
	{
		SyncEventListeners();
		if (null == owner_Connection_1 || !m_RegisteredForEvents)
		{
			owner_Connection_1 = parentGameObject;
			if (null != owner_Connection_1)
			{
				uScript_Update uScript_Update2 = owner_Connection_1.GetComponent<uScript_Update>();
				if (null == uScript_Update2)
				{
					uScript_Update2 = owner_Connection_1.AddComponent<uScript_Update>();
				}
				if (null != uScript_Update2)
				{
					uScript_Update2.OnUpdate += Instance_OnUpdate_0;
					uScript_Update2.OnLateUpdate += Instance_OnLateUpdate_0;
					uScript_Update2.OnFixedUpdate += Instance_OnFixedUpdate_0;
				}
			}
		}
		if (null == owner_Connection_44 || !m_RegisteredForEvents)
		{
			owner_Connection_44 = parentGameObject;
			if (null != owner_Connection_44)
			{
				uScript_BoundsWarningEvent uScript_BoundsWarningEvent2 = owner_Connection_44.GetComponent<uScript_BoundsWarningEvent>();
				if (null == uScript_BoundsWarningEvent2)
				{
					uScript_BoundsWarningEvent2 = owner_Connection_44.AddComponent<uScript_BoundsWarningEvent>();
				}
				if (null != uScript_BoundsWarningEvent2)
				{
					uScript_BoundsWarningEvent2.OnBoundsWarningCaution += Instance_OnBoundsWarningCaution_43;
					uScript_BoundsWarningEvent2.OnBoundsWarningIllegal += Instance_OnBoundsWarningIllegal_43;
				}
			}
		}
		if (null == owner_Connection_59 || !m_RegisteredForEvents)
		{
			owner_Connection_59 = parentGameObject;
			if (null != owner_Connection_59)
			{
				uScript_CheckPointChallengeEndedEvent uScript_CheckPointChallengeEndedEvent2 = owner_Connection_59.GetComponent<uScript_CheckPointChallengeEndedEvent>();
				if (null == uScript_CheckPointChallengeEndedEvent2)
				{
					uScript_CheckPointChallengeEndedEvent2 = owner_Connection_59.AddComponent<uScript_CheckPointChallengeEndedEvent>();
				}
				if (null != uScript_CheckPointChallengeEndedEvent2)
				{
					uScript_CheckPointChallengeEndedEvent2.OnSuccess += Instance_OnSuccess_125;
					uScript_CheckPointChallengeEndedEvent2.OnFail += Instance_OnFail_125;
				}
			}
		}
		if (!(null == owner_Connection_126) && m_RegisteredForEvents)
		{
			return;
		}
		owner_Connection_126 = parentGameObject;
		if (null != owner_Connection_126)
		{
			uScript_CheckPointPassedEvent uScript_CheckPointPassedEvent2 = owner_Connection_126.GetComponent<uScript_CheckPointPassedEvent>();
			if (null == uScript_CheckPointPassedEvent2)
			{
				uScript_CheckPointPassedEvent2 = owner_Connection_126.AddComponent<uScript_CheckPointPassedEvent>();
			}
			if (null != uScript_CheckPointPassedEvent2)
			{
				uScript_CheckPointPassedEvent2.OnCheckPointPassed += Instance_OnCheckPointPassed_127;
			}
		}
	}

	private void RegisterForUnityHooks()
	{
		SyncEventListeners();
		if (!m_RegisteredForEvents && null != owner_Connection_1)
		{
			uScript_Update uScript_Update2 = owner_Connection_1.GetComponent<uScript_Update>();
			if (null == uScript_Update2)
			{
				uScript_Update2 = owner_Connection_1.AddComponent<uScript_Update>();
			}
			if (null != uScript_Update2)
			{
				uScript_Update2.OnUpdate += Instance_OnUpdate_0;
				uScript_Update2.OnLateUpdate += Instance_OnLateUpdate_0;
				uScript_Update2.OnFixedUpdate += Instance_OnFixedUpdate_0;
			}
		}
		if (!m_RegisteredForEvents && null != owner_Connection_44)
		{
			uScript_BoundsWarningEvent uScript_BoundsWarningEvent2 = owner_Connection_44.GetComponent<uScript_BoundsWarningEvent>();
			if (null == uScript_BoundsWarningEvent2)
			{
				uScript_BoundsWarningEvent2 = owner_Connection_44.AddComponent<uScript_BoundsWarningEvent>();
			}
			if (null != uScript_BoundsWarningEvent2)
			{
				uScript_BoundsWarningEvent2.OnBoundsWarningCaution += Instance_OnBoundsWarningCaution_43;
				uScript_BoundsWarningEvent2.OnBoundsWarningIllegal += Instance_OnBoundsWarningIllegal_43;
			}
		}
		if (!m_RegisteredForEvents && null != owner_Connection_59)
		{
			uScript_CheckPointChallengeEndedEvent uScript_CheckPointChallengeEndedEvent2 = owner_Connection_59.GetComponent<uScript_CheckPointChallengeEndedEvent>();
			if (null == uScript_CheckPointChallengeEndedEvent2)
			{
				uScript_CheckPointChallengeEndedEvent2 = owner_Connection_59.AddComponent<uScript_CheckPointChallengeEndedEvent>();
			}
			if (null != uScript_CheckPointChallengeEndedEvent2)
			{
				uScript_CheckPointChallengeEndedEvent2.OnSuccess += Instance_OnSuccess_125;
				uScript_CheckPointChallengeEndedEvent2.OnFail += Instance_OnFail_125;
			}
		}
		if (!m_RegisteredForEvents && null != owner_Connection_126)
		{
			uScript_CheckPointPassedEvent uScript_CheckPointPassedEvent2 = owner_Connection_126.GetComponent<uScript_CheckPointPassedEvent>();
			if (null == uScript_CheckPointPassedEvent2)
			{
				uScript_CheckPointPassedEvent2 = owner_Connection_126.AddComponent<uScript_CheckPointPassedEvent>();
			}
			if (null != uScript_CheckPointPassedEvent2)
			{
				uScript_CheckPointPassedEvent2.OnCheckPointPassed += Instance_OnCheckPointPassed_127;
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
			uScript_Update component = owner_Connection_1.GetComponent<uScript_Update>();
			if (null != component)
			{
				component.OnUpdate -= Instance_OnUpdate_0;
				component.OnLateUpdate -= Instance_OnLateUpdate_0;
				component.OnFixedUpdate -= Instance_OnFixedUpdate_0;
			}
		}
		if (null != owner_Connection_44)
		{
			uScript_BoundsWarningEvent component2 = owner_Connection_44.GetComponent<uScript_BoundsWarningEvent>();
			if (null != component2)
			{
				component2.OnBoundsWarningCaution -= Instance_OnBoundsWarningCaution_43;
				component2.OnBoundsWarningIllegal -= Instance_OnBoundsWarningIllegal_43;
			}
		}
		if (null != owner_Connection_59)
		{
			uScript_CheckPointChallengeEndedEvent component3 = owner_Connection_59.GetComponent<uScript_CheckPointChallengeEndedEvent>();
			if (null != component3)
			{
				component3.OnSuccess -= Instance_OnSuccess_125;
				component3.OnFail -= Instance_OnFail_125;
			}
		}
		if (null != owner_Connection_126)
		{
			uScript_CheckPointPassedEvent component4 = owner_Connection_126.GetComponent<uScript_CheckPointPassedEvent>();
			if (null != component4)
			{
				component4.OnCheckPointPassed -= Instance_OnCheckPointPassed_127;
			}
		}
	}

	public override void SetParent(GameObject g)
	{
		parentGameObject = g;
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_2.SetParent(g);
		logic_uScript_IsPlayerInBeam_uScript_IsPlayerInBeam_4.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_5.SetParent(g);
		logic_uScript_RemoveOnScreenMessage_uScript_RemoveOnScreenMessage_11.SetParent(g);
		logic_uScript_RemoveOnScreenMessage_uScript_RemoveOnScreenMessage_12.SetParent(g);
		logic_uScript_GetCheckPointIndex_uScript_GetCheckPointIndex_15.SetParent(g);
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_17.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_19.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_21.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_23.SetParent(g);
		logic_uScript_DisablePlayerInput_uScript_DisablePlayerInput_26.SetParent(g);
		logic_uScript_ShowPrompt_uScript_ShowPrompt_28.SetParent(g);
		logic_uScript_DisablePlayerInput_uScript_DisablePlayerInput_29.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_30.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_32.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_36.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_38.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_39.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_41.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_46.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_49.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_52.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_53.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_55.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_58.SetParent(g);
		logic_uScript_DisablePlayerInput_uScript_DisablePlayerInput_60.SetParent(g);
		logic_uScript_GraphEvents_uScript_GraphEvents_65.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_67.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_68.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_69.SetParent(g);
		logic_uScript_BlankNode_uScript_BlankNode_73.SetParent(g);
		logic_uScript_Gauntlet_IsTutorialActive_uScript_Gauntlet_IsTutorialActive_75.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_76.SetParent(g);
		logic_uScript_DisablePlayerInput_uScript_DisablePlayerInput_78.SetParent(g);
		logic_uScript_BlockPaletteOptions_uScript_BlockPaletteOptions_79.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_80.SetParent(g);
		logic_uScript_BlockPaletteOptions_uScript_BlockPaletteOptions_82.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_83.SetParent(g);
		logic_uScriptAct_Stopwatch_uScriptAct_Stopwatch_85.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_87.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_88.SetParent(g);
		logic_uScriptCon_CompareFloat_uScriptCon_CompareFloat_89.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_93.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_94.SetParent(g);
		logic_uScriptCon_CompareFloat_uScriptCon_CompareFloat_97.SetParent(g);
		logic_uScriptAct_Stopwatch_uScriptAct_Stopwatch_98.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_100.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_101.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_103.SetParent(g);
		logic_uScriptCon_CompareFloat_uScriptCon_CompareFloat_105.SetParent(g);
		logic_uScriptCon_CompareFloat_uScriptCon_CompareFloat_107.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_108.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_110.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_117.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_119.SetParent(g);
		logic_uScript_BlockPaletteOptions_uScript_BlockPaletteOptions_123.SetParent(g);
		logic_uScript_Gauntlet_SetStartTutorialButtonVisibility_uScript_Gauntlet_SetStartTutorialButtonVisibility_124.SetParent(g);
		logic_uScript_GetJoypadControlMode_uScript_GetJoypadControlMode_129.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_133.SetParent(g);
		owner_Connection_1 = parentGameObject;
		owner_Connection_44 = parentGameObject;
		owner_Connection_59 = parentGameObject;
		owner_Connection_126 = parentGameObject;
	}

	public void Awake()
	{
		logic_uScript_GraphEvents_uScript_GraphEvents_65.uScriptEnable += uScript_GraphEvents_uScriptEnable_65;
		logic_uScript_GraphEvents_uScript_GraphEvents_65.uScriptDisable += uScript_GraphEvents_uScriptDisable_65;
		logic_uScript_GraphEvents_uScript_GraphEvents_65.uScriptDestroy += uScript_GraphEvents_uScriptDestroy_65;
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
		logic_uScript_GetCheckPointIndex_uScript_GetCheckPointIndex_15.OnEnable();
		logic_uScript_ShowPrompt_uScript_ShowPrompt_28.OnEnable();
		logic_uScript_GraphEvents_uScript_GraphEvents_65.OnEnable();
	}

	public void OnDisable()
	{
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_2.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_5.OnDisable();
		logic_uScript_GetCheckPointIndex_uScript_GetCheckPointIndex_15.OnDisable();
		logic_uScript_ShowPrompt_uScript_ShowPrompt_28.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_36.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_46.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_49.OnDisable();
		logic_uScript_GraphEvents_uScript_GraphEvents_65.OnDisable();
		logic_uScript_GetJoypadControlMode_uScript_GetJoypadControlMode_129.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_133.OnDisable();
		UnregisterEventListeners();
		m_RegisteredForEvents = false;
	}

	public void Update()
	{
		SyncEventListeners();
		logic_uScriptAct_Stopwatch_uScriptAct_Stopwatch_85.Update();
		logic_uScriptAct_Stopwatch_uScriptAct_Stopwatch_98.Update();
	}

	public void OnDestroy()
	{
		logic_uScript_GraphEvents_uScript_GraphEvents_65.OnDestroy();
		logic_uScript_Gauntlet_IsTutorialActive_uScript_Gauntlet_IsTutorialActive_75.OnDestroy();
		logic_uScript_GraphEvents_uScript_GraphEvents_65.uScriptEnable -= uScript_GraphEvents_uScriptEnable_65;
		logic_uScript_GraphEvents_uScript_GraphEvents_65.uScriptDisable -= uScript_GraphEvents_uScriptDisable_65;
		logic_uScript_GraphEvents_uScript_GraphEvents_65.uScriptDestroy -= uScript_GraphEvents_uScriptDestroy_65;
	}

	private void Instance_OnUpdate_0(object o, EventArgs e)
	{
		Relay_OnUpdate_0();
	}

	private void Instance_OnLateUpdate_0(object o, EventArgs e)
	{
		Relay_OnLateUpdate_0();
	}

	private void Instance_OnFixedUpdate_0(object o, EventArgs e)
	{
		Relay_OnFixedUpdate_0();
	}

	private void Instance_OnBoundsWarningCaution_43(object o, EventArgs e)
	{
		Relay_OnBoundsWarningCaution_43();
	}

	private void Instance_OnBoundsWarningIllegal_43(object o, EventArgs e)
	{
		Relay_OnBoundsWarningIllegal_43();
	}

	private void Instance_OnSuccess_125(object o, uScript_CheckPointChallengeEndedEvent.CheckpointChallengeEndedEventArgs e)
	{
		event_UnityEngine_GameObject_EndReason_125 = e.EndReason;
		event_UnityEngine_GameObject_EndTime_125 = e.EndTime;
		Relay_OnSuccess_125();
	}

	private void Instance_OnFail_125(object o, uScript_CheckPointChallengeEndedEvent.CheckpointChallengeEndedEventArgs e)
	{
		event_UnityEngine_GameObject_EndReason_125 = e.EndReason;
		event_UnityEngine_GameObject_EndTime_125 = e.EndTime;
		Relay_OnFail_125();
	}

	private void Instance_OnCheckPointPassed_127(object o, uScript_CheckPointPassedEvent.CheckpointPassedEventArgs e)
	{
		event_UnityEngine_GameObject_CheckpointIndex_127 = e.CheckpointIndex;
		Relay_OnCheckPointPassed_127();
	}

	private void uScript_GraphEvents_uScriptEnable_65(object o, EventArgs e)
	{
		Relay_uScriptEnable_65();
	}

	private void uScript_GraphEvents_uScriptDisable_65(object o, EventArgs e)
	{
		Relay_uScriptDisable_65();
	}

	private void uScript_GraphEvents_uScriptDestroy_65(object o, EventArgs e)
	{
		Relay_uScriptDestroy_65();
	}

	private void Relay_OnUpdate_0()
	{
		Relay_In_75();
	}

	private void Relay_OnLateUpdate_0()
	{
	}

	private void Relay_OnFixedUpdate_0()
	{
	}

	private void Relay_In_2()
	{
		int num = 0;
		Array array = msgBuildTech_Pad;
		if (logic_uScript_AddOnScreenMessage_locString_2.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_2, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_2, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_tag_2 = local_msg_System_String;
		logic_uScript_AddOnScreenMessage_Return_2 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_2.In(logic_uScript_AddOnScreenMessage_locString_2, logic_uScript_AddOnScreenMessage_msgPriority_2, logic_uScript_AddOnScreenMessage_holdMsg_2, logic_uScript_AddOnScreenMessage_tag_2, logic_uScript_AddOnScreenMessage_speaker_2, logic_uScript_AddOnScreenMessage_side_2);
		local_MsgBuildTech_ManOnScreenMessages_OnScreenMessage = logic_uScript_AddOnScreenMessage_Return_2;
		if (logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_2.Out)
		{
			Relay_ResetTimer_85();
		}
	}

	private void Relay_In_4()
	{
		logic_uScript_IsPlayerInBeam_uScript_IsPlayerInBeam_4.In();
		bool num = logic_uScript_IsPlayerInBeam_uScript_IsPlayerInBeam_4.True;
		bool flag = logic_uScript_IsPlayerInBeam_uScript_IsPlayerInBeam_4.False;
		if (num)
		{
			Relay_In_87();
		}
		if (flag)
		{
			Relay_In_100();
		}
	}

	private void Relay_In_5()
	{
		int num = 0;
		Array array = msgReadyToStart;
		if (logic_uScript_AddOnScreenMessage_locString_5.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_5, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_5, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_tag_5 = local_msg_System_String;
		logic_uScript_AddOnScreenMessage_Return_5 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_5.In(logic_uScript_AddOnScreenMessage_locString_5, logic_uScript_AddOnScreenMessage_msgPriority_5, logic_uScript_AddOnScreenMessage_holdMsg_5, logic_uScript_AddOnScreenMessage_tag_5, logic_uScript_AddOnScreenMessage_speaker_5, logic_uScript_AddOnScreenMessage_side_5);
		local_MsgReadyToStart_ManOnScreenMessages_OnScreenMessage = logic_uScript_AddOnScreenMessage_Return_5;
		if (logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_5.Out)
		{
			Relay_ResetTimer_98();
		}
	}

	private void Relay_In_11()
	{
		logic_uScript_RemoveOnScreenMessage_onScreenMessage_11 = local_MsgReadyToStart_ManOnScreenMessages_OnScreenMessage;
		logic_uScript_RemoveOnScreenMessage_uScript_RemoveOnScreenMessage_11.In(logic_uScript_RemoveOnScreenMessage_onScreenMessage_11, logic_uScript_RemoveOnScreenMessage_instant_11);
		if (logic_uScript_RemoveOnScreenMessage_uScript_RemoveOnScreenMessage_11.Out)
		{
			Relay_In_129();
		}
	}

	private void Relay_In_12()
	{
		logic_uScript_RemoveOnScreenMessage_onScreenMessage_12 = local_MsgBuildTech_ManOnScreenMessages_OnScreenMessage;
		logic_uScript_RemoveOnScreenMessage_uScript_RemoveOnScreenMessage_12.In(logic_uScript_RemoveOnScreenMessage_onScreenMessage_12, logic_uScript_RemoveOnScreenMessage_instant_12);
		if (logic_uScript_RemoveOnScreenMessage_uScript_RemoveOnScreenMessage_12.Out)
		{
			Relay_In_5();
		}
	}

	private void Relay_In_15()
	{
		logic_uScript_GetCheckPointIndex_Return_15 = logic_uScript_GetCheckPointIndex_uScript_GetCheckPointIndex_15.In();
		local_currentCheckpoint_System_Int32 = logic_uScript_GetCheckPointIndex_Return_15;
		if (logic_uScript_GetCheckPointIndex_uScript_GetCheckPointIndex_15.Out)
		{
			Relay_In_17();
		}
	}

	private void Relay_In_17()
	{
		logic_uScriptCon_CompareInt_A_17 = local_currentCheckpoint_System_Int32;
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_17.In(logic_uScriptCon_CompareInt_A_17, logic_uScriptCon_CompareInt_B_17);
		if (logic_uScriptCon_CompareInt_uScriptCon_CompareInt_17.EqualTo)
		{
			Relay_True_21();
		}
	}

	private void Relay_In_19()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_19 = local_msg_System_String;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_19.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_19, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_19);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_19.Out)
		{
			Relay_In_79();
		}
	}

	private void Relay_True_21()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_21.True(out logic_uScriptAct_SetBool_Target_21);
		local_GauntletStarted_System_Boolean = logic_uScriptAct_SetBool_Target_21;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_21.Out)
		{
			Relay_In_19();
		}
	}

	private void Relay_False_21()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_21.False(out logic_uScriptAct_SetBool_Target_21);
		local_GauntletStarted_System_Boolean = logic_uScriptAct_SetBool_Target_21;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_21.Out)
		{
			Relay_In_19();
		}
	}

	private void Relay_In_23()
	{
		logic_uScriptCon_CompareBool_Bool_23 = local_GauntletStarted_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_23.In(logic_uScriptCon_CompareBool_Bool_23);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_23.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_23.False;
		if (num)
		{
			Relay_In_39();
		}
		if (flag)
		{
			Relay_In_4();
		}
	}

	private void Relay_In_26()
	{
		logic_uScript_DisablePlayerInput_uScript_DisablePlayerInput_26.In(logic_uScript_DisablePlayerInput_disableInput_26);
		if (logic_uScript_DisablePlayerInput_uScript_DisablePlayerInput_26.Out)
		{
			Relay_True_32();
		}
	}

	private void Relay_In_28()
	{
		logic_uScript_ShowPrompt_bodyText_28 = MsgIntroScreen;
		logic_uScript_ShowPrompt_acceptButtonText_28 = Accept;
		logic_uScript_ShowPrompt_Return_28 = logic_uScript_ShowPrompt_uScript_ShowPrompt_28.In(logic_uScript_ShowPrompt_bodyText_28, logic_uScript_ShowPrompt_acceptButtonText_28, logic_uScript_ShowPrompt_rejectButtonText_28);
		if (logic_uScript_ShowPrompt_uScript_ShowPrompt_28.Out)
		{
			Relay_In_82();
		}
	}

	private void Relay_In_29()
	{
		logic_uScript_DisablePlayerInput_uScript_DisablePlayerInput_29.In(logic_uScript_DisablePlayerInput_disableInput_29);
		if (logic_uScript_DisablePlayerInput_uScript_DisablePlayerInput_29.Out)
		{
			Relay_In_123();
		}
	}

	private void Relay_In_30()
	{
		logic_uScriptCon_CompareBool_Bool_30 = local_IntroScreenFinished_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_30.In(logic_uScriptCon_CompareBool_Bool_30);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_30.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_30.False;
		if (num)
		{
			Relay_In_78();
		}
		if (flag)
		{
			Relay_In_29();
		}
	}

	private void Relay_True_32()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_32.True(out logic_uScriptAct_SetBool_Target_32);
		local_IntroScreenFinished_System_Boolean = logic_uScriptAct_SetBool_Target_32;
	}

	private void Relay_False_32()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_32.False(out logic_uScriptAct_SetBool_Target_32);
		local_IntroScreenFinished_System_Boolean = logic_uScriptAct_SetBool_Target_32;
	}

	private void Relay_In_36()
	{
		int num = 0;
		Array array = msgGauntletComplete;
		if (logic_uScript_AddOnScreenMessage_locString_36.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_36, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_36, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_tag_36 = local_msg_System_String;
		logic_uScript_AddOnScreenMessage_Return_36 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_36.In(logic_uScript_AddOnScreenMessage_locString_36, logic_uScript_AddOnScreenMessage_msgPriority_36, logic_uScript_AddOnScreenMessage_holdMsg_36, logic_uScript_AddOnScreenMessage_tag_36, logic_uScript_AddOnScreenMessage_speaker_36, logic_uScript_AddOnScreenMessage_side_36);
	}

	private void Relay_In_38()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_38 = local_msg_System_String;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_38.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_38, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_38);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_38.Out)
		{
			Relay_In_36();
		}
	}

	private void Relay_In_39()
	{
		logic_uScriptCon_CompareBool_Bool_39 = local_GauntletComplete_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_39.In(logic_uScriptCon_CompareBool_Bool_39);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_39.True)
		{
			Relay_In_60();
		}
	}

	private void Relay_True_41()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_41.True(out logic_uScriptAct_SetBool_Target_41);
		local_GauntletComplete_System_Boolean = logic_uScriptAct_SetBool_Target_41;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_41.Out)
		{
			Relay_In_38();
		}
	}

	private void Relay_False_41()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_41.False(out logic_uScriptAct_SetBool_Target_41);
		local_GauntletComplete_System_Boolean = logic_uScriptAct_SetBool_Target_41;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_41.Out)
		{
			Relay_In_38();
		}
	}

	private void Relay_OnBoundsWarningCaution_43()
	{
	}

	private void Relay_OnBoundsWarningIllegal_43()
	{
		Relay_In_55();
	}

	private void Relay_In_46()
	{
		int num = 0;
		Array array = msgOutOfBoundsWarning;
		if (logic_uScript_AddOnScreenMessage_locString_46.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_46, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_46, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_tag_46 = local_msg_System_String;
		logic_uScript_AddOnScreenMessage_Return_46 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_46.In(logic_uScript_AddOnScreenMessage_locString_46, logic_uScript_AddOnScreenMessage_msgPriority_46, logic_uScript_AddOnScreenMessage_holdMsg_46, logic_uScript_AddOnScreenMessage_tag_46, logic_uScript_AddOnScreenMessage_speaker_46, logic_uScript_AddOnScreenMessage_side_46);
	}

	private void Relay_In_49()
	{
		int num = 0;
		Array array = msgOutOfBounds;
		if (logic_uScript_AddOnScreenMessage_locString_49.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_49, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_49, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_tag_49 = local_msg_System_String;
		logic_uScript_AddOnScreenMessage_Return_49 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_49.In(logic_uScript_AddOnScreenMessage_locString_49, logic_uScript_AddOnScreenMessage_msgPriority_49, logic_uScript_AddOnScreenMessage_holdMsg_49, logic_uScript_AddOnScreenMessage_tag_49, logic_uScript_AddOnScreenMessage_speaker_49, logic_uScript_AddOnScreenMessage_side_49);
	}

	private void Relay_In_52()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_52 = local_msg_System_String;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_52.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_52, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_52);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_52.Out)
		{
			Relay_In_49();
		}
	}

	private void Relay_In_53()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_53 = local_msg_System_String;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_53.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_53, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_53);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_53.Out)
		{
			Relay_In_46();
		}
	}

	private void Relay_In_55()
	{
		logic_uScriptCon_CompareBool_Bool_55 = local_GauntletComplete_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_55.In(logic_uScriptCon_CompareBool_Bool_55);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_55.False)
		{
			Relay_In_52();
		}
	}

	private void Relay_In_58()
	{
		logic_uScriptCon_CompareBool_Bool_58 = local_GauntletComplete_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_58.In(logic_uScriptCon_CompareBool_Bool_58);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_58.False)
		{
			Relay_In_53();
		}
	}

	private void Relay_In_60()
	{
		logic_uScript_DisablePlayerInput_uScript_DisablePlayerInput_60.In(logic_uScript_DisablePlayerInput_disableInput_60);
		if (logic_uScript_DisablePlayerInput_uScript_DisablePlayerInput_60.Out)
		{
			Relay_In_73();
		}
	}

	private void Relay_uScriptEnable_65()
	{
		Relay_False_67();
	}

	private void Relay_uScriptDisable_65()
	{
		Relay_False_80();
	}

	private void Relay_uScriptDestroy_65()
	{
		Relay_False_80();
	}

	private void Relay_True_67()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_67.True(out logic_uScriptAct_SetBool_Target_67);
		local_IntroScreenFinished_System_Boolean = logic_uScriptAct_SetBool_Target_67;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_67.Out)
		{
			Relay_False_68();
		}
	}

	private void Relay_False_67()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_67.False(out logic_uScriptAct_SetBool_Target_67);
		local_IntroScreenFinished_System_Boolean = logic_uScriptAct_SetBool_Target_67;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_67.Out)
		{
			Relay_False_68();
		}
	}

	private void Relay_True_68()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_68.True(out logic_uScriptAct_SetBool_Target_68);
		local_GauntletStarted_System_Boolean = logic_uScriptAct_SetBool_Target_68;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_68.Out)
		{
			Relay_False_69();
		}
	}

	private void Relay_False_68()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_68.False(out logic_uScriptAct_SetBool_Target_68);
		local_GauntletStarted_System_Boolean = logic_uScriptAct_SetBool_Target_68;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_68.Out)
		{
			Relay_False_69();
		}
	}

	private void Relay_True_69()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_69.True(out logic_uScriptAct_SetBool_Target_69);
		local_GauntletComplete_System_Boolean = logic_uScriptAct_SetBool_Target_69;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_69.Out)
		{
			Relay_False_119();
		}
	}

	private void Relay_False_69()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_69.False(out logic_uScriptAct_SetBool_Target_69);
		local_GauntletComplete_System_Boolean = logic_uScriptAct_SetBool_Target_69;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_69.Out)
		{
			Relay_False_119();
		}
	}

	private void Relay_In_73()
	{
		logic_uScript_BlankNode_uScript_BlankNode_73.In();
	}

	private void Relay_In_75()
	{
		logic_uScript_Gauntlet_IsTutorialActive_uScript_Gauntlet_IsTutorialActive_75.In();
		if (logic_uScript_Gauntlet_IsTutorialActive_uScript_Gauntlet_IsTutorialActive_75.False)
		{
			Relay_In_30();
		}
	}

	private void Relay_In_76()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_76.In(logic_uScriptAct_SetInt_Value_76, out logic_uScriptAct_SetInt_Target_76);
		local_currentCheckpoint_System_Int32 = logic_uScriptAct_SetInt_Target_76;
	}

	private void Relay_In_78()
	{
		logic_uScript_DisablePlayerInput_uScript_DisablePlayerInput_78.In(logic_uScript_DisablePlayerInput_disableInput_78);
		if (logic_uScript_DisablePlayerInput_uScript_DisablePlayerInput_78.Out)
		{
			Relay_In_23();
		}
	}

	private void Relay_In_79()
	{
		logic_uScript_BlockPaletteOptions_uScript_BlockPaletteOptions_79.In(logic_uScript_BlockPaletteOptions_show_79, logic_uScript_BlockPaletteOptions_open_79);
	}

	private void Relay_True_80()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_80.True(out logic_uScriptAct_SetBool_Target_80);
		local_IntroScreenFinished_System_Boolean = logic_uScriptAct_SetBool_Target_80;
	}

	private void Relay_False_80()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_80.False(out logic_uScriptAct_SetBool_Target_80);
		local_IntroScreenFinished_System_Boolean = logic_uScriptAct_SetBool_Target_80;
	}

	private void Relay_In_82()
	{
		logic_uScript_BlockPaletteOptions_uScript_BlockPaletteOptions_82.In(logic_uScript_BlockPaletteOptions_show_82, logic_uScript_BlockPaletteOptions_open_82);
		if (logic_uScript_BlockPaletteOptions_uScript_BlockPaletteOptions_82.Out)
		{
			Relay_In_124();
		}
	}

	private void Relay_In_83()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_83 = local_msg_System_String;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_83.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_83, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_83);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_83.Out)
		{
			Relay_In_105();
		}
	}

	private void Relay_StartTimer_85()
	{
		logic_uScriptAct_Stopwatch_uScriptAct_Stopwatch_85.StartTimer(out logic_uScriptAct_Stopwatch_Seconds_85);
		local_90_System_Single = logic_uScriptAct_Stopwatch_Seconds_85;
		if (logic_uScriptAct_Stopwatch_uScriptAct_Stopwatch_85.Started)
		{
			Relay_In_89();
		}
	}

	private void Relay_Stop_85()
	{
		logic_uScriptAct_Stopwatch_uScriptAct_Stopwatch_85.Stop(out logic_uScriptAct_Stopwatch_Seconds_85);
		local_90_System_Single = logic_uScriptAct_Stopwatch_Seconds_85;
		if (logic_uScriptAct_Stopwatch_uScriptAct_Stopwatch_85.Started)
		{
			Relay_In_89();
		}
	}

	private void Relay_ResetTimer_85()
	{
		logic_uScriptAct_Stopwatch_uScriptAct_Stopwatch_85.ResetTimer(out logic_uScriptAct_Stopwatch_Seconds_85);
		local_90_System_Single = logic_uScriptAct_Stopwatch_Seconds_85;
		if (logic_uScriptAct_Stopwatch_uScriptAct_Stopwatch_85.Started)
		{
			Relay_In_89();
		}
	}

	private void Relay_CheckTime_85()
	{
		logic_uScriptAct_Stopwatch_uScriptAct_Stopwatch_85.CheckTime(out logic_uScriptAct_Stopwatch_Seconds_85);
		local_90_System_Single = logic_uScriptAct_Stopwatch_Seconds_85;
		if (logic_uScriptAct_Stopwatch_uScriptAct_Stopwatch_85.Started)
		{
			Relay_In_89();
		}
	}

	private void Relay_In_87()
	{
		logic_uScriptCon_CompareBool_Bool_87 = local_MsgBuildTechTriggered_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_87.In(logic_uScriptCon_CompareBool_Bool_87);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_87.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_87.False;
		if (num)
		{
			Relay_StartTimer_85();
		}
		if (flag)
		{
			Relay_True_88();
		}
	}

	private void Relay_True_88()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_88.True(out logic_uScriptAct_SetBool_Target_88);
		local_MsgBuildTechTriggered_System_Boolean = logic_uScriptAct_SetBool_Target_88;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_88.Out)
		{
			Relay_False_93();
		}
	}

	private void Relay_False_88()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_88.False(out logic_uScriptAct_SetBool_Target_88);
		local_MsgBuildTechTriggered_System_Boolean = logic_uScriptAct_SetBool_Target_88;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_88.Out)
		{
			Relay_False_93();
		}
	}

	private void Relay_In_89()
	{
		logic_uScriptCon_CompareFloat_A_89 = local_90_System_Single;
		logic_uScriptCon_CompareFloat_B_89 = startAreaMsgDuration;
		logic_uScriptCon_CompareFloat_uScriptCon_CompareFloat_89.In(logic_uScriptCon_CompareFloat_A_89, logic_uScriptCon_CompareFloat_B_89);
		if (logic_uScriptCon_CompareFloat_uScriptCon_CompareFloat_89.GreaterThan)
		{
			Relay_In_83();
		}
	}

	private void Relay_True_93()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_93.True(out logic_uScriptAct_SetBool_Target_93);
		local_MsgReadyToStartTriggered_System_Boolean = logic_uScriptAct_SetBool_Target_93;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_93.Out)
		{
			Relay_In_11();
		}
	}

	private void Relay_False_93()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_93.False(out logic_uScriptAct_SetBool_Target_93);
		local_MsgReadyToStartTriggered_System_Boolean = logic_uScriptAct_SetBool_Target_93;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_93.Out)
		{
			Relay_In_11();
		}
	}

	private void Relay_True_94()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_94.True(out logic_uScriptAct_SetBool_Target_94);
		local_MsgBuildTechTriggered_System_Boolean = logic_uScriptAct_SetBool_Target_94;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_94.Out)
		{
			Relay_In_12();
		}
	}

	private void Relay_False_94()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_94.False(out logic_uScriptAct_SetBool_Target_94);
		local_MsgBuildTechTriggered_System_Boolean = logic_uScriptAct_SetBool_Target_94;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_94.Out)
		{
			Relay_In_12();
		}
	}

	private void Relay_In_97()
	{
		logic_uScriptCon_CompareFloat_A_97 = local_102_System_Single;
		logic_uScriptCon_CompareFloat_B_97 = startAreaMsgDuration;
		logic_uScriptCon_CompareFloat_uScriptCon_CompareFloat_97.In(logic_uScriptCon_CompareFloat_A_97, logic_uScriptCon_CompareFloat_B_97);
		if (logic_uScriptCon_CompareFloat_uScriptCon_CompareFloat_97.GreaterThan)
		{
			Relay_In_110();
		}
	}

	private void Relay_StartTimer_98()
	{
		logic_uScriptAct_Stopwatch_uScriptAct_Stopwatch_98.StartTimer(out logic_uScriptAct_Stopwatch_Seconds_98);
		local_102_System_Single = logic_uScriptAct_Stopwatch_Seconds_98;
		if (logic_uScriptAct_Stopwatch_uScriptAct_Stopwatch_98.Started)
		{
			Relay_In_97();
		}
	}

	private void Relay_Stop_98()
	{
		logic_uScriptAct_Stopwatch_uScriptAct_Stopwatch_98.Stop(out logic_uScriptAct_Stopwatch_Seconds_98);
		local_102_System_Single = logic_uScriptAct_Stopwatch_Seconds_98;
		if (logic_uScriptAct_Stopwatch_uScriptAct_Stopwatch_98.Started)
		{
			Relay_In_97();
		}
	}

	private void Relay_ResetTimer_98()
	{
		logic_uScriptAct_Stopwatch_uScriptAct_Stopwatch_98.ResetTimer(out logic_uScriptAct_Stopwatch_Seconds_98);
		local_102_System_Single = logic_uScriptAct_Stopwatch_Seconds_98;
		if (logic_uScriptAct_Stopwatch_uScriptAct_Stopwatch_98.Started)
		{
			Relay_In_97();
		}
	}

	private void Relay_CheckTime_98()
	{
		logic_uScriptAct_Stopwatch_uScriptAct_Stopwatch_98.CheckTime(out logic_uScriptAct_Stopwatch_Seconds_98);
		local_102_System_Single = logic_uScriptAct_Stopwatch_Seconds_98;
		if (logic_uScriptAct_Stopwatch_uScriptAct_Stopwatch_98.Started)
		{
			Relay_In_97();
		}
	}

	private void Relay_In_100()
	{
		logic_uScriptCon_CompareBool_Bool_100 = local_MsgReadyToStartTriggered_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_100.In(logic_uScriptCon_CompareBool_Bool_100);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_100.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_100.False;
		if (num)
		{
			Relay_StartTimer_98();
		}
		if (flag)
		{
			Relay_True_101();
		}
	}

	private void Relay_True_101()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_101.True(out logic_uScriptAct_SetBool_Target_101);
		local_MsgReadyToStartTriggered_System_Boolean = logic_uScriptAct_SetBool_Target_101;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_101.Out)
		{
			Relay_False_94();
		}
	}

	private void Relay_False_101()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_101.False(out logic_uScriptAct_SetBool_Target_101);
		local_MsgReadyToStartTriggered_System_Boolean = logic_uScriptAct_SetBool_Target_101;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_101.Out)
		{
			Relay_False_94();
		}
	}

	private void Relay_True_103()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_103.True(out logic_uScriptAct_SetBool_Target_103);
		local_MsgBuildTechTriggered_System_Boolean = logic_uScriptAct_SetBool_Target_103;
	}

	private void Relay_False_103()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_103.False(out logic_uScriptAct_SetBool_Target_103);
		local_MsgBuildTechTriggered_System_Boolean = logic_uScriptAct_SetBool_Target_103;
	}

	private void Relay_In_105()
	{
		logic_uScriptCon_CompareFloat_A_105 = local_90_System_Single;
		logic_uScriptCon_CompareFloat_B_105 = startAreaMsgRefreshTime;
		logic_uScriptCon_CompareFloat_uScriptCon_CompareFloat_105.In(logic_uScriptCon_CompareFloat_A_105, logic_uScriptCon_CompareFloat_B_105);
		if (logic_uScriptCon_CompareFloat_uScriptCon_CompareFloat_105.GreaterThan)
		{
			Relay_False_103();
		}
	}

	private void Relay_In_107()
	{
		logic_uScriptCon_CompareFloat_A_107 = local_102_System_Single;
		logic_uScriptCon_CompareFloat_B_107 = startAreaMsgRefreshTime;
		logic_uScriptCon_CompareFloat_uScriptCon_CompareFloat_107.In(logic_uScriptCon_CompareFloat_A_107, logic_uScriptCon_CompareFloat_B_107);
		if (logic_uScriptCon_CompareFloat_uScriptCon_CompareFloat_107.GreaterThan)
		{
			Relay_False_108();
		}
	}

	private void Relay_True_108()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_108.True(out logic_uScriptAct_SetBool_Target_108);
		local_MsgReadyToStartTriggered_System_Boolean = logic_uScriptAct_SetBool_Target_108;
	}

	private void Relay_False_108()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_108.False(out logic_uScriptAct_SetBool_Target_108);
		local_MsgReadyToStartTriggered_System_Boolean = logic_uScriptAct_SetBool_Target_108;
	}

	private void Relay_In_110()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_110 = local_msg_System_String;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_110.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_110, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_110);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_110.Out)
		{
			Relay_In_107();
		}
	}

	private void Relay_True_117()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_117.True(out logic_uScriptAct_SetBool_Target_117);
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_117.Out)
		{
			Relay_In_76();
		}
	}

	private void Relay_False_117()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_117.False(out logic_uScriptAct_SetBool_Target_117);
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_117.Out)
		{
			Relay_In_76();
		}
	}

	private void Relay_True_119()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_119.True(out logic_uScriptAct_SetBool_Target_119);
		local_MsgBuildTechTriggered_System_Boolean = logic_uScriptAct_SetBool_Target_119;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_119.Out)
		{
			Relay_False_117();
		}
	}

	private void Relay_False_119()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_119.False(out logic_uScriptAct_SetBool_Target_119);
		local_MsgBuildTechTriggered_System_Boolean = logic_uScriptAct_SetBool_Target_119;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_119.Out)
		{
			Relay_False_117();
		}
	}

	private void Relay_In_123()
	{
		logic_uScript_BlockPaletteOptions_uScript_BlockPaletteOptions_123.In(logic_uScript_BlockPaletteOptions_show_123, logic_uScript_BlockPaletteOptions_open_123);
		if (logic_uScript_BlockPaletteOptions_uScript_BlockPaletteOptions_123.Out)
		{
			Relay_In_28();
		}
	}

	private void Relay_In_124()
	{
		logic_uScript_Gauntlet_SetStartTutorialButtonVisibility_uScript_Gauntlet_SetStartTutorialButtonVisibility_124.In(logic_uScript_Gauntlet_SetStartTutorialButtonVisibility_enabled_124);
		if (logic_uScript_Gauntlet_SetStartTutorialButtonVisibility_uScript_Gauntlet_SetStartTutorialButtonVisibility_124.Out)
		{
			Relay_In_26();
		}
	}

	private void Relay_OnSuccess_125()
	{
		Relay_True_41();
	}

	private void Relay_OnFail_125()
	{
	}

	private void Relay_OnCheckPointPassed_127()
	{
		Relay_In_15();
	}

	private void Relay_In_129()
	{
		logic_uScript_GetJoypadControlMode_uScript_GetJoypadControlMode_129.In();
		bool joypad = logic_uScript_GetJoypadControlMode_uScript_GetJoypadControlMode_129.Joypad;
		bool mouseAndKeyboard = logic_uScript_GetJoypadControlMode_uScript_GetJoypadControlMode_129.MouseAndKeyboard;
		if (joypad)
		{
			Relay_In_2();
		}
		if (mouseAndKeyboard)
		{
			Relay_In_133();
		}
	}

	private void Relay_In_133()
	{
		int num = 0;
		Array array = msgBuildTech;
		if (logic_uScript_AddOnScreenMessage_locString_133.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_133, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_133, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_tag_133 = local_msg_System_String;
		logic_uScript_AddOnScreenMessage_Return_133 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_133.In(logic_uScript_AddOnScreenMessage_locString_133, logic_uScript_AddOnScreenMessage_msgPriority_133, logic_uScript_AddOnScreenMessage_holdMsg_133, logic_uScript_AddOnScreenMessage_tag_133, logic_uScript_AddOnScreenMessage_speaker_133, logic_uScript_AddOnScreenMessage_side_133);
		local_MsgBuildTech_ManOnScreenMessages_OnScreenMessage = logic_uScript_AddOnScreenMessage_Return_133;
		if (logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_133.Out)
		{
			Relay_ResetTimer_85();
		}
	}
}
