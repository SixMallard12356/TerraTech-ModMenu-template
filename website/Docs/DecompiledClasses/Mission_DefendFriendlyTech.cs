using System;
using UnityEngine;

[Serializable]
[FriendlyName("SubGraph_DefeatEnemyTechs", "")]
[NodePath("Graphs")]
public class Mission_DefendFriendlyTech : uScriptLogic
{
	private GameObject parentGameObject;

	private uScript_GUI thisScriptsOnGuiListener;

	private bool m_RegisteredForEvents;

	public bool AllowEnemyGroupToRespawn = true;

	[Multiline(3)]
	public string clearSceneryPos = "";

	public float clearSceneryRadius;

	public float DelayBetweenEnemyGroupSpaws;

	public float DelayBetweenRespawnArivals;

	public SpawnTechData enemyGroupData;

	public WaveSizeSpecification EnemyGroupSize;

	public SpawnTechData[] friendlyTechData = new SpawnTechData[0];

	private int local_168_System_Int32;

	private float local_distAtWhichDistressSignalSent_System_Single = 200f;

	private float local_distAtWhichFriendlyTechAbandonded_System_Single = 150f;

	private float local_distAtWhichFriendlyTechFound_System_Single = 50f;

	private float local_distAtWhichLeavingMissionArea_System_Single = 100f;

	private Tank local_FriendlyTech_Tank;

	private bool local_FriendlyTechFound_System_Boolean;

	private bool local_FriendlyTechSpawned_System_Boolean;

	private string local_Msg_System_String = "Msg";

	private bool local_MsgDistressShown_System_Boolean;

	private bool local_MsgFoundShown_System_Boolean;

	private bool local_ShownMsgEnemiesGivingUp_System_Boolean;

	private bool local_ShownMsgEnemiesIncoming_System_Boolean;

	private int local_StartValue_System_Int32;

	private Tank[] local_Techs_TankArray = new Tank[0];

	private float local_TimerCount_System_Single;

	private bool local_TimerStarted_System_Boolean;

	private int local_TotalGroupEnemiesSpawned_System_Int32;

	public ManOnScreenMessages.Speaker messageSpeaker;

	public LocalisedString[] msgDistressSignal = new LocalisedString[0];

	public LocalisedString[] msgEnemiesGivingUp = new LocalisedString[0];

	public LocalisedString[] msgEnemiesIncoming = new LocalisedString[0];

	public LocalisedString[] msgFriendlyTechFound = new LocalisedString[0];

	public LocalisedString[] msgLeavingMissionArea = new LocalisedString[0];

	public LocalisedString[] msgMissionComplete = new LocalisedString[0];

	public LocalisedString[] msgMissionFailedDeath = new LocalisedString[0];

	public LocalisedString[] msgMissionFailedDistance = new LocalisedString[0];

	public int ReinforcementSubGroupSize;

	private GameObject owner_Connection_0;

	private GameObject owner_Connection_8;

	private GameObject owner_Connection_11;

	private GameObject owner_Connection_24;

	private GameObject owner_Connection_44;

	private GameObject owner_Connection_47;

	private GameObject owner_Connection_50;

	private GameObject owner_Connection_56;

	private GameObject owner_Connection_61;

	private GameObject owner_Connection_65;

	private GameObject owner_Connection_68;

	private GameObject owner_Connection_73;

	private GameObject owner_Connection_84;

	private GameObject owner_Connection_96;

	private GameObject owner_Connection_97;

	private GameObject owner_Connection_102;

	private GameObject owner_Connection_107;

	private GameObject owner_Connection_165;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_2 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_2 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_2;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_2 = 0.3f;

	private bool logic_uScript_SpawnTechsFromData_Out_2 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_4 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_4;

	private bool logic_uScriptAct_SetBool_Out_4 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_4 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_4 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_5 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_5;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_5 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_5 = "FriendlyTechSpawned";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_6 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_6;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_6 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_6 = "MsgFoundShown";

	private uScript_IsPlayerInRangeOfTech logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_13 = new uScript_IsPlayerInRangeOfTech();

	private Tank logic_uScript_IsPlayerInRangeOfTech_tech_13;

	private float logic_uScript_IsPlayerInRangeOfTech_range_13;

	private Tank[] logic_uScript_IsPlayerInRangeOfTech_techs_13 = new Tank[0];

	private bool logic_uScript_IsPlayerInRangeOfTech_Out_13 = true;

	private bool logic_uScript_IsPlayerInRangeOfTech_InRange_13 = true;

	private bool logic_uScript_IsPlayerInRangeOfTech_OutOfRange_13 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_16 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_16;

	private bool logic_uScriptCon_CompareBool_True_16 = true;

	private bool logic_uScriptCon_CompareBool_False_16 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_17 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_17;

	private bool logic_uScriptAct_SetBool_Out_17 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_17 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_17 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_18 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_18 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_18 = ManOnScreenMessages.MessagePriority.High;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_18;

	private string logic_uScript_AddOnScreenMessage_tag_18 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_18;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_18;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_18;

	private bool logic_uScript_AddOnScreenMessage_Out_18 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_18 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_21 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_21;

	private bool logic_uScriptCon_CompareBool_True_21 = true;

	private bool logic_uScriptCon_CompareBool_False_21 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_22 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_22 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_22 = ManOnScreenMessages.MessagePriority.Medium;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_22;

	private string logic_uScript_AddOnScreenMessage_tag_22 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_22;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_22;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_22;

	private bool logic_uScript_AddOnScreenMessage_Out_22 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_22 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_26 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_26 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_26 = true;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_26 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_28 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_28;

	private bool logic_uScriptCon_CompareBool_True_28 = true;

	private bool logic_uScriptCon_CompareBool_False_28 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_30 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_30;

	private bool logic_uScriptCon_CompareBool_True_30 = true;

	private bool logic_uScriptCon_CompareBool_False_30 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_32 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_32;

	private bool logic_uScriptAct_SetBool_Out_32 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_32 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_32 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_33 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_33;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_33 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_33 = "FriendlyTechFound";

	private uScript_FinishEncounter logic_uScript_FinishEncounter_uScript_FinishEncounter_34 = new uScript_FinishEncounter();

	private GameObject logic_uScript_FinishEncounter_owner_34;

	private bool logic_uScript_FinishEncounter_Out_34 = true;

	private uScript_SetTankTeam logic_uScript_SetTankTeam_uScript_SetTankTeam_38 = new uScript_SetTankTeam();

	private Tank logic_uScript_SetTankTeam_tank_38;

	private int logic_uScript_SetTankTeam_team_38;

	private bool logic_uScript_SetTankTeam_Out_38 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_42 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_42;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_42 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_42 = "TimerStarted";

	private uScript_StartEncounterTimer logic_uScript_StartEncounterTimer_uScript_StartEncounterTimer_43 = new uScript_StartEncounterTimer();

	private GameObject logic_uScript_StartEncounterTimer_owner_43;

	private bool logic_uScript_StartEncounterTimer_Out_43 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_45 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_45;

	private bool logic_uScriptAct_SetBool_Out_45 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_45 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_45 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_51 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_51 = new Tank[0];

	private int logic_uScript_AccessListTech_index_51;

	private Tank logic_uScript_AccessListTech_value_51;

	private bool logic_uScript_AccessListTech_Out_51 = true;

	private uScript_MoveEncounterWithVisible logic_uScript_MoveEncounterWithVisible_uScript_MoveEncounterWithVisible_55 = new uScript_MoveEncounterWithVisible();

	private GameObject logic_uScript_MoveEncounterWithVisible_ownerNode_55;

	private object logic_uScript_MoveEncounterWithVisible_visibleObject_55 = "";

	private bool logic_uScript_MoveEncounterWithVisible_Out_55 = true;

	private uScript_SetEncounterTarget logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_57 = new uScript_SetEncounterTarget();

	private GameObject logic_uScript_SetEncounterTarget_owner_57;

	private object logic_uScript_SetEncounterTarget_visibleObject_57 = "";

	private bool logic_uScript_SetEncounterTarget_Out_57 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_58 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_58 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_58;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_58 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_58;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_58 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_58 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_58 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_58 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_60 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_60 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_60;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_60 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_60;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_60 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_60 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_60 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_60 = true;

	private uScript_GetEncounterTimeRemaining logic_uScript_GetEncounterTimeRemaining_uScript_GetEncounterTimeRemaining_64 = new uScript_GetEncounterTimeRemaining();

	private GameObject logic_uScript_GetEncounterTimeRemaining_owner_64;

	private float logic_uScript_GetEncounterTimeRemaining_Return_64;

	private bool logic_uScript_GetEncounterTimeRemaining_Out_64 = true;

	private uScriptCon_CompareFloat logic_uScriptCon_CompareFloat_uScriptCon_CompareFloat_67 = new uScriptCon_CompareFloat();

	private float logic_uScriptCon_CompareFloat_A_67;

	private float logic_uScriptCon_CompareFloat_B_67;

	private bool logic_uScriptCon_CompareFloat_GreaterThan_67 = true;

	private bool logic_uScriptCon_CompareFloat_GreaterThanOrEqualTo_67 = true;

	private bool logic_uScriptCon_CompareFloat_EqualTo_67 = true;

	private bool logic_uScriptCon_CompareFloat_NotEqualTo_67 = true;

	private bool logic_uScriptCon_CompareFloat_LessThanOrEqualTo_67 = true;

	private bool logic_uScriptCon_CompareFloat_LessThan_67 = true;

	private uScript_FinishEncounter logic_uScript_FinishEncounter_uScript_FinishEncounter_69 = new uScript_FinishEncounter();

	private GameObject logic_uScript_FinishEncounter_owner_69;

	private bool logic_uScript_FinishEncounter_Out_69 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_72 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_72 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_72 = ManOnScreenMessages.MessagePriority.Medium;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_72;

	private string logic_uScript_AddOnScreenMessage_tag_72 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_72;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_72;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_72;

	private bool logic_uScript_AddOnScreenMessage_Out_72 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_72 = true;

	private uScript_FinishEncounter logic_uScript_FinishEncounter_uScript_FinishEncounter_75 = new uScript_FinishEncounter();

	private GameObject logic_uScript_FinishEncounter_owner_75;

	private bool logic_uScript_FinishEncounter_Out_75 = true;

	private uScript_IsPlayerInRangeOfTech logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_77 = new uScript_IsPlayerInRangeOfTech();

	private Tank logic_uScript_IsPlayerInRangeOfTech_tech_77;

	private float logic_uScript_IsPlayerInRangeOfTech_range_77;

	private Tank[] logic_uScript_IsPlayerInRangeOfTech_techs_77 = new Tank[0];

	private bool logic_uScript_IsPlayerInRangeOfTech_Out_77 = true;

	private bool logic_uScript_IsPlayerInRangeOfTech_InRange_77 = true;

	private bool logic_uScript_IsPlayerInRangeOfTech_OutOfRange_77 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_80 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_80 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_80 = ManOnScreenMessages.MessagePriority.Medium;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_80;

	private string logic_uScript_AddOnScreenMessage_tag_80 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_80;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_80;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_80;

	private bool logic_uScript_AddOnScreenMessage_Out_80 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_80 = true;

	private uScript_FinishEncounter logic_uScript_FinishEncounter_uScript_FinishEncounter_83 = new uScript_FinishEncounter();

	private GameObject logic_uScript_FinishEncounter_owner_83;

	private bool logic_uScript_FinishEncounter_Out_83 = true;

	private uScript_IsPlayerInRangeOfTech logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_87 = new uScript_IsPlayerInRangeOfTech();

	private Tank logic_uScript_IsPlayerInRangeOfTech_tech_87;

	private float logic_uScript_IsPlayerInRangeOfTech_range_87;

	private Tank[] logic_uScript_IsPlayerInRangeOfTech_techs_87 = new Tank[0];

	private bool logic_uScript_IsPlayerInRangeOfTech_Out_87 = true;

	private bool logic_uScript_IsPlayerInRangeOfTech_InRange_87 = true;

	private bool logic_uScript_IsPlayerInRangeOfTech_OutOfRange_87 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_88 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_88 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_88 = ManOnScreenMessages.MessagePriority.Medium;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_88;

	private string logic_uScript_AddOnScreenMessage_tag_88 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_88;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_88;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_88;

	private bool logic_uScript_AddOnScreenMessage_Out_88 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_88 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_91 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_91 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_91 = true;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_91 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_93 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_93 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_93 = true;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_93 = true;

	private uScript_RemoveEncounterTimer logic_uScript_RemoveEncounterTimer_uScript_RemoveEncounterTimer_95 = new uScript_RemoveEncounterTimer();

	private GameObject logic_uScript_RemoveEncounterTimer_owner_95;

	private bool logic_uScript_RemoveEncounterTimer_Out_95 = true;

	private uScript_RemoveEncounterTimer logic_uScript_RemoveEncounterTimer_uScript_RemoveEncounterTimer_98 = new uScript_RemoveEncounterTimer();

	private GameObject logic_uScript_RemoveEncounterTimer_owner_98;

	private bool logic_uScript_RemoveEncounterTimer_Out_98 = true;

	private SubGraph_SaveLoadInt logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_99 = new SubGraph_SaveLoadInt();

	private int logic_SubGraph_SaveLoadInt_restartValue_99;

	private int logic_SubGraph_SaveLoadInt_integer_99;

	private object logic_SubGraph_SaveLoadInt_intAsVariable_99 = "";

	private string logic_SubGraph_SaveLoadInt_uniqueID_99 = "EnemyGroup";

	private uScript_SpawnTechWaveFromData logic_uScript_SpawnTechWaveFromData_uScript_SpawnTechWaveFromData_100 = new uScript_SpawnTechWaveFromData();

	private GameObject logic_uScript_SpawnTechWaveFromData_ownerNode_100;

	private SpawnTechData logic_uScript_SpawnTechWaveFromData_spawnData_100;

	private WaveSizeSpecification logic_uScript_SpawnTechWaveFromData_waveSize_100;

	private float logic_uScript_SpawnTechWaveFromData_delayBetweenSpawns_100;

	private int logic_uScript_SpawnTechWaveFromData_numSpawnedSoFar_100;

	private bool logic_uScript_SpawnTechWaveFromData_allowRespawn_100;

	private int logic_uScript_SpawnTechWaveFromData_respawnGroupSize_100;

	private float logic_uScript_SpawnTechWaveFromData_delayBetweenRespawnGroups_100;

	private int logic_uScript_SpawnTechWaveFromData_Return_100;

	private bool logic_uScript_SpawnTechWaveFromData_Out_100 = true;

	private bool logic_uScript_SpawnTechWaveFromData_RespawnedWaveGroup_100 = true;

	private bool logic_uScript_SpawnTechWaveFromData_AllTechKilled_100 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_103 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_103 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_103 = ManOnScreenMessages.MessagePriority.Medium;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_103;

	private string logic_uScript_AddOnScreenMessage_tag_103 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_103;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_103;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_103;

	private bool logic_uScript_AddOnScreenMessage_Out_103 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_103 = true;

	private uScript_RemoveEncounterTimer logic_uScript_RemoveEncounterTimer_uScript_RemoveEncounterTimer_105 = new uScript_RemoveEncounterTimer();

	private GameObject logic_uScript_RemoveEncounterTimer_owner_105;

	private bool logic_uScript_RemoveEncounterTimer_Out_105 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_106 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_106 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_106 = true;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_106 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_115 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_115;

	private bool logic_uScriptAct_SetBool_Out_115 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_115 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_115 = true;

	private uScriptCon_CompareFloat logic_uScriptCon_CompareFloat_uScriptCon_CompareFloat_118 = new uScriptCon_CompareFloat();

	private float logic_uScriptCon_CompareFloat_A_118;

	private float logic_uScriptCon_CompareFloat_B_118 = 10f;

	private bool logic_uScriptCon_CompareFloat_GreaterThan_118 = true;

	private bool logic_uScriptCon_CompareFloat_GreaterThanOrEqualTo_118 = true;

	private bool logic_uScriptCon_CompareFloat_EqualTo_118 = true;

	private bool logic_uScriptCon_CompareFloat_NotEqualTo_118 = true;

	private bool logic_uScriptCon_CompareFloat_LessThanOrEqualTo_118 = true;

	private bool logic_uScriptCon_CompareFloat_LessThan_118 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_121 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_121 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_121 = ManOnScreenMessages.MessagePriority.Medium;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_121;

	private string logic_uScript_AddOnScreenMessage_tag_121 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_121;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_121;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_121;

	private bool logic_uScript_AddOnScreenMessage_Out_121 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_121 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_123 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_123;

	private bool logic_uScriptAct_SetBool_Out_123 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_123 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_123 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_125 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_125;

	private bool logic_uScriptCon_CompareBool_True_125 = true;

	private bool logic_uScriptCon_CompareBool_False_125 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_127 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_127;

	private bool logic_uScriptCon_CompareBool_True_127 = true;

	private bool logic_uScriptCon_CompareBool_False_127 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_130 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_130;

	private bool logic_uScriptAct_SetBool_Out_130 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_130 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_130 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_131 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_131 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_131 = ManOnScreenMessages.MessagePriority.Medium;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_131;

	private string logic_uScript_AddOnScreenMessage_tag_131 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_131;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_131;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_131;

	private bool logic_uScript_AddOnScreenMessage_Out_131 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_131 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_137 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_137;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_137 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_137 = "ShownMsgEnemiesIncoming";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_138 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_138;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_138 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_138 = "ShownMsgEnemiesGivingUp";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_139 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_139;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_139 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_139 = "AllowEnemyGroupToRespawn";

	private uScript_IsPlayerInRangeOfTech logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_143 = new uScript_IsPlayerInRangeOfTech();

	private Tank logic_uScript_IsPlayerInRangeOfTech_tech_143;

	private float logic_uScript_IsPlayerInRangeOfTech_range_143;

	private Tank[] logic_uScript_IsPlayerInRangeOfTech_techs_143 = new Tank[0];

	private bool logic_uScript_IsPlayerInRangeOfTech_Out_143 = true;

	private bool logic_uScript_IsPlayerInRangeOfTech_InRange_143 = true;

	private bool logic_uScript_IsPlayerInRangeOfTech_OutOfRange_143 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_144 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_144;

	private bool logic_uScriptAct_SetBool_Out_144 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_144 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_144 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_145 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_145;

	private bool logic_uScriptCon_CompareBool_True_145 = true;

	private bool logic_uScriptCon_CompareBool_False_145 = true;

	private uScript_AddOnScreenMessage logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_146 = new uScript_AddOnScreenMessage();

	private LocalisedString[] logic_uScript_AddOnScreenMessage_locString_146 = new LocalisedString[0];

	private ManOnScreenMessages.MessagePriority logic_uScript_AddOnScreenMessage_msgPriority_146 = ManOnScreenMessages.MessagePriority.High;

	private bool logic_uScript_AddOnScreenMessage_holdMsg_146;

	private string logic_uScript_AddOnScreenMessage_tag_146 = "";

	private ManOnScreenMessages.Speaker logic_uScript_AddOnScreenMessage_speaker_146;

	private ManOnScreenMessages.Side logic_uScript_AddOnScreenMessage_side_146;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddOnScreenMessage_Return_146;

	private bool logic_uScript_AddOnScreenMessage_Out_146 = true;

	private bool logic_uScript_AddOnScreenMessage_Shown_146 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_150 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_150;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_150 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_150 = "MsgDistressShown";

	private uScript_DiscoverBlock logic_uScript_DiscoverBlock_uScript_DiscoverBlock_151 = new uScript_DiscoverBlock();

	private BlockTypes logic_uScript_DiscoverBlock_blockType_151 = BlockTypes.GSOAIGuardController_111;

	private bool logic_uScript_DiscoverBlock_Out_151 = true;

	private uScript_SetTankTeam logic_uScript_SetTankTeam_uScript_SetTankTeam_161 = new uScript_SetTankTeam();

	private Tank logic_uScript_SetTankTeam_tank_161;

	private int logic_uScript_SetTankTeam_team_161 = -1;

	private bool logic_uScript_SetTankTeam_Out_161 = true;

	private uScript_RemoveScenery logic_uScript_RemoveScenery_uScript_RemoveScenery_166 = new uScript_RemoveScenery();

	private GameObject logic_uScript_RemoveScenery_ownerNode_166;

	private string logic_uScript_RemoveScenery_positionName_166 = "";

	private float logic_uScript_RemoveScenery_radius_166;

	private bool logic_uScript_RemoveScenery_preventChunksSpawning_166 = true;

	private bool logic_uScript_RemoveScenery_Out_166 = true;

	private uScript_GetPlayerTeam logic_uScript_GetPlayerTeam_uScript_GetPlayerTeam_167 = new uScript_GetPlayerTeam();

	private int logic_uScript_GetPlayerTeam_Return_167;

	private bool logic_uScript_GetPlayerTeam_Out_167 = true;

	private void SyncUnityHooks()
	{
		SyncEventListeners();
		if (null == owner_Connection_0 || !m_RegisteredForEvents)
		{
			owner_Connection_0 = parentGameObject;
		}
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
					uScript_SaveLoad2.SaveEvent += Instance_SaveEvent_9;
					uScript_SaveLoad2.LoadEvent += Instance_LoadEvent_9;
					uScript_SaveLoad2.RestartEvent += Instance_RestartEvent_9;
				}
			}
		}
		if (null == owner_Connection_11 || !m_RegisteredForEvents)
		{
			owner_Connection_11 = parentGameObject;
			if (null != owner_Connection_11)
			{
				uScript_EncounterUpdate uScript_EncounterUpdate2 = owner_Connection_11.GetComponent<uScript_EncounterUpdate>();
				if (null == uScript_EncounterUpdate2)
				{
					uScript_EncounterUpdate2 = owner_Connection_11.AddComponent<uScript_EncounterUpdate>();
				}
				if (null != uScript_EncounterUpdate2)
				{
					uScript_EncounterUpdate2.OnUpdate += Instance_OnUpdate_10;
					uScript_EncounterUpdate2.OnSuspend += Instance_OnSuspend_10;
					uScript_EncounterUpdate2.OnResume += Instance_OnResume_10;
				}
			}
		}
		if (null == owner_Connection_24 || !m_RegisteredForEvents)
		{
			owner_Connection_24 = parentGameObject;
		}
		if (null == owner_Connection_44 || !m_RegisteredForEvents)
		{
			owner_Connection_44 = parentGameObject;
		}
		if (null == owner_Connection_47 || !m_RegisteredForEvents)
		{
			owner_Connection_47 = parentGameObject;
		}
		if (null == owner_Connection_50 || !m_RegisteredForEvents)
		{
			owner_Connection_50 = parentGameObject;
		}
		if (null == owner_Connection_56 || !m_RegisteredForEvents)
		{
			owner_Connection_56 = parentGameObject;
		}
		if (null == owner_Connection_61 || !m_RegisteredForEvents)
		{
			owner_Connection_61 = parentGameObject;
		}
		if (null == owner_Connection_65 || !m_RegisteredForEvents)
		{
			owner_Connection_65 = parentGameObject;
		}
		if (null == owner_Connection_68 || !m_RegisteredForEvents)
		{
			owner_Connection_68 = parentGameObject;
		}
		if (null == owner_Connection_73 || !m_RegisteredForEvents)
		{
			owner_Connection_73 = parentGameObject;
		}
		if (null == owner_Connection_84 || !m_RegisteredForEvents)
		{
			owner_Connection_84 = parentGameObject;
		}
		if (null == owner_Connection_96 || !m_RegisteredForEvents)
		{
			owner_Connection_96 = parentGameObject;
		}
		if (null == owner_Connection_97 || !m_RegisteredForEvents)
		{
			owner_Connection_97 = parentGameObject;
		}
		if (null == owner_Connection_102 || !m_RegisteredForEvents)
		{
			owner_Connection_102 = parentGameObject;
		}
		if (null == owner_Connection_107 || !m_RegisteredForEvents)
		{
			owner_Connection_107 = parentGameObject;
		}
		if (null == owner_Connection_165 || !m_RegisteredForEvents)
		{
			owner_Connection_165 = parentGameObject;
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
				uScript_SaveLoad2.SaveEvent += Instance_SaveEvent_9;
				uScript_SaveLoad2.LoadEvent += Instance_LoadEvent_9;
				uScript_SaveLoad2.RestartEvent += Instance_RestartEvent_9;
			}
		}
		if (!m_RegisteredForEvents && null != owner_Connection_11)
		{
			uScript_EncounterUpdate uScript_EncounterUpdate2 = owner_Connection_11.GetComponent<uScript_EncounterUpdate>();
			if (null == uScript_EncounterUpdate2)
			{
				uScript_EncounterUpdate2 = owner_Connection_11.AddComponent<uScript_EncounterUpdate>();
			}
			if (null != uScript_EncounterUpdate2)
			{
				uScript_EncounterUpdate2.OnUpdate += Instance_OnUpdate_10;
				uScript_EncounterUpdate2.OnSuspend += Instance_OnSuspend_10;
				uScript_EncounterUpdate2.OnResume += Instance_OnResume_10;
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
				component.SaveEvent -= Instance_SaveEvent_9;
				component.LoadEvent -= Instance_LoadEvent_9;
				component.RestartEvent -= Instance_RestartEvent_9;
			}
		}
		if (null != owner_Connection_11)
		{
			uScript_EncounterUpdate component2 = owner_Connection_11.GetComponent<uScript_EncounterUpdate>();
			if (null != component2)
			{
				component2.OnUpdate -= Instance_OnUpdate_10;
				component2.OnSuspend -= Instance_OnSuspend_10;
				component2.OnResume -= Instance_OnResume_10;
			}
		}
	}

	public override void SetParent(GameObject g)
	{
		parentGameObject = g;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_2.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_4.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_5.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_6.SetParent(g);
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_13.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_16.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_17.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_18.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_21.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_22.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_26.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_28.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_30.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_32.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_33.SetParent(g);
		logic_uScript_FinishEncounter_uScript_FinishEncounter_34.SetParent(g);
		logic_uScript_SetTankTeam_uScript_SetTankTeam_38.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_42.SetParent(g);
		logic_uScript_StartEncounterTimer_uScript_StartEncounterTimer_43.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_45.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_51.SetParent(g);
		logic_uScript_MoveEncounterWithVisible_uScript_MoveEncounterWithVisible_55.SetParent(g);
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_57.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_58.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_60.SetParent(g);
		logic_uScript_GetEncounterTimeRemaining_uScript_GetEncounterTimeRemaining_64.SetParent(g);
		logic_uScriptCon_CompareFloat_uScriptCon_CompareFloat_67.SetParent(g);
		logic_uScript_FinishEncounter_uScript_FinishEncounter_69.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_72.SetParent(g);
		logic_uScript_FinishEncounter_uScript_FinishEncounter_75.SetParent(g);
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_77.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_80.SetParent(g);
		logic_uScript_FinishEncounter_uScript_FinishEncounter_83.SetParent(g);
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_87.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_88.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_91.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_93.SetParent(g);
		logic_uScript_RemoveEncounterTimer_uScript_RemoveEncounterTimer_95.SetParent(g);
		logic_uScript_RemoveEncounterTimer_uScript_RemoveEncounterTimer_98.SetParent(g);
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_99.SetParent(g);
		logic_uScript_SpawnTechWaveFromData_uScript_SpawnTechWaveFromData_100.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_103.SetParent(g);
		logic_uScript_RemoveEncounterTimer_uScript_RemoveEncounterTimer_105.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_106.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_115.SetParent(g);
		logic_uScriptCon_CompareFloat_uScriptCon_CompareFloat_118.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_121.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_123.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_125.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_127.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_130.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_131.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_137.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_138.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_139.SetParent(g);
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_143.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_144.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_145.SetParent(g);
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_146.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_150.SetParent(g);
		logic_uScript_DiscoverBlock_uScript_DiscoverBlock_151.SetParent(g);
		logic_uScript_SetTankTeam_uScript_SetTankTeam_161.SetParent(g);
		logic_uScript_RemoveScenery_uScript_RemoveScenery_166.SetParent(g);
		logic_uScript_GetPlayerTeam_uScript_GetPlayerTeam_167.SetParent(g);
		owner_Connection_0 = parentGameObject;
		owner_Connection_8 = parentGameObject;
		owner_Connection_11 = parentGameObject;
		owner_Connection_24 = parentGameObject;
		owner_Connection_44 = parentGameObject;
		owner_Connection_47 = parentGameObject;
		owner_Connection_50 = parentGameObject;
		owner_Connection_56 = parentGameObject;
		owner_Connection_61 = parentGameObject;
		owner_Connection_65 = parentGameObject;
		owner_Connection_68 = parentGameObject;
		owner_Connection_73 = parentGameObject;
		owner_Connection_84 = parentGameObject;
		owner_Connection_96 = parentGameObject;
		owner_Connection_97 = parentGameObject;
		owner_Connection_102 = parentGameObject;
		owner_Connection_107 = parentGameObject;
		owner_Connection_165 = parentGameObject;
	}

	public void Awake()
	{
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_5.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_6.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_33.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_42.Awake();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_99.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_137.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_138.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_139.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_150.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_5.Save_Out += SubGraph_SaveLoadBool_Save_Out_5;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_5.Load_Out += SubGraph_SaveLoadBool_Load_Out_5;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_5.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_5;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_6.Save_Out += SubGraph_SaveLoadBool_Save_Out_6;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_6.Load_Out += SubGraph_SaveLoadBool_Load_Out_6;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_6.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_6;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_33.Save_Out += SubGraph_SaveLoadBool_Save_Out_33;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_33.Load_Out += SubGraph_SaveLoadBool_Load_Out_33;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_33.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_33;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_42.Save_Out += SubGraph_SaveLoadBool_Save_Out_42;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_42.Load_Out += SubGraph_SaveLoadBool_Load_Out_42;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_42.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_42;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_99.Save_Out += SubGraph_SaveLoadInt_Save_Out_99;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_99.Load_Out += SubGraph_SaveLoadInt_Load_Out_99;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_99.Restart_Out += SubGraph_SaveLoadInt_Restart_Out_99;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_137.Save_Out += SubGraph_SaveLoadBool_Save_Out_137;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_137.Load_Out += SubGraph_SaveLoadBool_Load_Out_137;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_137.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_137;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_138.Save_Out += SubGraph_SaveLoadBool_Save_Out_138;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_138.Load_Out += SubGraph_SaveLoadBool_Load_Out_138;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_138.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_138;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_139.Save_Out += SubGraph_SaveLoadBool_Save_Out_139;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_139.Load_Out += SubGraph_SaveLoadBool_Load_Out_139;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_139.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_139;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_150.Save_Out += SubGraph_SaveLoadBool_Save_Out_150;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_150.Load_Out += SubGraph_SaveLoadBool_Load_Out_150;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_150.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_150;
	}

	public void Start()
	{
		SyncUnityHooks();
		m_RegisteredForEvents = true;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_5.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_6.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_33.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_42.Start();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_99.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_137.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_138.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_139.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_150.Start();
	}

	public void OnEnable()
	{
		RegisterForUnityHooks();
		m_RegisteredForEvents = true;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_5.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_6.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_33.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_42.OnEnable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_99.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_137.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_138.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_139.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_150.OnEnable();
	}

	public void OnDisable()
	{
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_5.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_6.OnDisable();
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_13.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_18.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_22.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_33.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_42.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_72.OnDisable();
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_77.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_80.OnDisable();
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_87.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_88.OnDisable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_99.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_103.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_121.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_131.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_137.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_138.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_139.OnDisable();
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_143.OnDisable();
		logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_146.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_150.OnDisable();
		UnregisterEventListeners();
		m_RegisteredForEvents = false;
	}

	public void Update()
	{
		SyncEventListeners();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_5.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_6.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_33.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_42.Update();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_99.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_137.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_138.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_139.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_150.Update();
	}

	public void OnDestroy()
	{
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_5.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_6.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_33.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_42.OnDestroy();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_99.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_137.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_138.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_139.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_150.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_5.Save_Out -= SubGraph_SaveLoadBool_Save_Out_5;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_5.Load_Out -= SubGraph_SaveLoadBool_Load_Out_5;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_5.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_5;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_6.Save_Out -= SubGraph_SaveLoadBool_Save_Out_6;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_6.Load_Out -= SubGraph_SaveLoadBool_Load_Out_6;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_6.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_6;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_33.Save_Out -= SubGraph_SaveLoadBool_Save_Out_33;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_33.Load_Out -= SubGraph_SaveLoadBool_Load_Out_33;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_33.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_33;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_42.Save_Out -= SubGraph_SaveLoadBool_Save_Out_42;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_42.Load_Out -= SubGraph_SaveLoadBool_Load_Out_42;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_42.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_42;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_99.Save_Out -= SubGraph_SaveLoadInt_Save_Out_99;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_99.Load_Out -= SubGraph_SaveLoadInt_Load_Out_99;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_99.Restart_Out -= SubGraph_SaveLoadInt_Restart_Out_99;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_137.Save_Out -= SubGraph_SaveLoadBool_Save_Out_137;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_137.Load_Out -= SubGraph_SaveLoadBool_Load_Out_137;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_137.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_137;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_138.Save_Out -= SubGraph_SaveLoadBool_Save_Out_138;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_138.Load_Out -= SubGraph_SaveLoadBool_Load_Out_138;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_138.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_138;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_139.Save_Out -= SubGraph_SaveLoadBool_Save_Out_139;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_139.Load_Out -= SubGraph_SaveLoadBool_Load_Out_139;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_139.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_139;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_150.Save_Out -= SubGraph_SaveLoadBool_Save_Out_150;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_150.Load_Out -= SubGraph_SaveLoadBool_Load_Out_150;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_150.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_150;
	}

	private void Instance_SaveEvent_9(object o, EventArgs e)
	{
		Relay_SaveEvent_9();
	}

	private void Instance_LoadEvent_9(object o, EventArgs e)
	{
		Relay_LoadEvent_9();
	}

	private void Instance_RestartEvent_9(object o, EventArgs e)
	{
		Relay_RestartEvent_9();
	}

	private void Instance_OnUpdate_10(object o, EventArgs e)
	{
		Relay_OnUpdate_10();
	}

	private void Instance_OnSuspend_10(object o, EventArgs e)
	{
		Relay_OnSuspend_10();
	}

	private void Instance_OnResume_10(object o, EventArgs e)
	{
		Relay_OnResume_10();
	}

	private void SubGraph_SaveLoadBool_Save_Out_5(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_5 = e.boolean;
		local_FriendlyTechSpawned_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_5;
		Relay_Save_Out_5();
	}

	private void SubGraph_SaveLoadBool_Load_Out_5(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_5 = e.boolean;
		local_FriendlyTechSpawned_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_5;
		Relay_Load_Out_5();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_5(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_5 = e.boolean;
		local_FriendlyTechSpawned_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_5;
		Relay_Restart_Out_5();
	}

	private void SubGraph_SaveLoadBool_Save_Out_6(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_6 = e.boolean;
		local_MsgFoundShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_6;
		Relay_Save_Out_6();
	}

	private void SubGraph_SaveLoadBool_Load_Out_6(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_6 = e.boolean;
		local_MsgFoundShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_6;
		Relay_Load_Out_6();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_6(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_6 = e.boolean;
		local_MsgFoundShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_6;
		Relay_Restart_Out_6();
	}

	private void SubGraph_SaveLoadBool_Save_Out_33(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_33 = e.boolean;
		local_FriendlyTechFound_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_33;
		Relay_Save_Out_33();
	}

	private void SubGraph_SaveLoadBool_Load_Out_33(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_33 = e.boolean;
		local_FriendlyTechFound_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_33;
		Relay_Load_Out_33();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_33(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_33 = e.boolean;
		local_FriendlyTechFound_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_33;
		Relay_Restart_Out_33();
	}

	private void SubGraph_SaveLoadBool_Save_Out_42(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_42 = e.boolean;
		local_TimerStarted_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_42;
		Relay_Save_Out_42();
	}

	private void SubGraph_SaveLoadBool_Load_Out_42(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_42 = e.boolean;
		local_TimerStarted_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_42;
		Relay_Load_Out_42();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_42(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_42 = e.boolean;
		local_TimerStarted_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_42;
		Relay_Restart_Out_42();
	}

	private void SubGraph_SaveLoadInt_Save_Out_99(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_99 = e.integer;
		local_TotalGroupEnemiesSpawned_System_Int32 = logic_SubGraph_SaveLoadInt_integer_99;
		Relay_Save_Out_99();
	}

	private void SubGraph_SaveLoadInt_Load_Out_99(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_99 = e.integer;
		local_TotalGroupEnemiesSpawned_System_Int32 = logic_SubGraph_SaveLoadInt_integer_99;
		Relay_Load_Out_99();
	}

	private void SubGraph_SaveLoadInt_Restart_Out_99(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_99 = e.integer;
		local_TotalGroupEnemiesSpawned_System_Int32 = logic_SubGraph_SaveLoadInt_integer_99;
		Relay_Restart_Out_99();
	}

	private void SubGraph_SaveLoadBool_Save_Out_137(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_137 = e.boolean;
		local_ShownMsgEnemiesIncoming_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_137;
		Relay_Save_Out_137();
	}

	private void SubGraph_SaveLoadBool_Load_Out_137(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_137 = e.boolean;
		local_ShownMsgEnemiesIncoming_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_137;
		Relay_Load_Out_137();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_137(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_137 = e.boolean;
		local_ShownMsgEnemiesIncoming_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_137;
		Relay_Restart_Out_137();
	}

	private void SubGraph_SaveLoadBool_Save_Out_138(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_138 = e.boolean;
		local_ShownMsgEnemiesGivingUp_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_138;
		Relay_Save_Out_138();
	}

	private void SubGraph_SaveLoadBool_Load_Out_138(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_138 = e.boolean;
		local_ShownMsgEnemiesGivingUp_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_138;
		Relay_Load_Out_138();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_138(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_138 = e.boolean;
		local_ShownMsgEnemiesGivingUp_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_138;
		Relay_Restart_Out_138();
	}

	private void SubGraph_SaveLoadBool_Save_Out_139(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_139 = e.boolean;
		AllowEnemyGroupToRespawn = logic_SubGraph_SaveLoadBool_boolean_139;
		Relay_Save_Out_139();
	}

	private void SubGraph_SaveLoadBool_Load_Out_139(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_139 = e.boolean;
		AllowEnemyGroupToRespawn = logic_SubGraph_SaveLoadBool_boolean_139;
		Relay_Load_Out_139();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_139(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_139 = e.boolean;
		AllowEnemyGroupToRespawn = logic_SubGraph_SaveLoadBool_boolean_139;
		Relay_Restart_Out_139();
	}

	private void SubGraph_SaveLoadBool_Save_Out_150(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_150 = e.boolean;
		local_MsgDistressShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_150;
		Relay_Save_Out_150();
	}

	private void SubGraph_SaveLoadBool_Load_Out_150(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_150 = e.boolean;
		local_MsgDistressShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_150;
		Relay_Load_Out_150();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_150(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_150 = e.boolean;
		local_MsgDistressShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_150;
		Relay_Restart_Out_150();
	}

	private void Relay_InitialSpawn_2()
	{
		int num = 0;
		Array array = friendlyTechData;
		if (logic_uScript_SpawnTechsFromData_spawnData_2.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_2, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SpawnTechsFromData_spawnData_2, num, array.Length);
		num += array.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_2 = owner_Connection_0;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_2.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_2, logic_uScript_SpawnTechsFromData_ownerNode_2, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_2);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_2.Out)
		{
			Relay_True_4();
		}
	}

	private void Relay_True_4()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_4.True(out logic_uScriptAct_SetBool_Target_4);
		local_FriendlyTechSpawned_System_Boolean = logic_uScriptAct_SetBool_Target_4;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_4.Out)
		{
			Relay_In_166();
		}
	}

	private void Relay_False_4()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_4.False(out logic_uScriptAct_SetBool_Target_4);
		local_FriendlyTechSpawned_System_Boolean = logic_uScriptAct_SetBool_Target_4;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_4.Out)
		{
			Relay_In_166();
		}
	}

	private void Relay_Save_Out_5()
	{
		Relay_Save_33();
	}

	private void Relay_Load_Out_5()
	{
		Relay_Load_33();
	}

	private void Relay_Restart_Out_5()
	{
		Relay_Set_False_33();
	}

	private void Relay_Save_5()
	{
		logic_SubGraph_SaveLoadBool_boolean_5 = local_FriendlyTechSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_5 = local_FriendlyTechSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_5.Save(ref logic_SubGraph_SaveLoadBool_boolean_5, logic_SubGraph_SaveLoadBool_boolAsVariable_5, logic_SubGraph_SaveLoadBool_uniqueID_5);
	}

	private void Relay_Load_5()
	{
		logic_SubGraph_SaveLoadBool_boolean_5 = local_FriendlyTechSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_5 = local_FriendlyTechSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_5.Load(ref logic_SubGraph_SaveLoadBool_boolean_5, logic_SubGraph_SaveLoadBool_boolAsVariable_5, logic_SubGraph_SaveLoadBool_uniqueID_5);
	}

	private void Relay_Set_True_5()
	{
		logic_SubGraph_SaveLoadBool_boolean_5 = local_FriendlyTechSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_5 = local_FriendlyTechSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_5.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_5, logic_SubGraph_SaveLoadBool_boolAsVariable_5, logic_SubGraph_SaveLoadBool_uniqueID_5);
	}

	private void Relay_Set_False_5()
	{
		logic_SubGraph_SaveLoadBool_boolean_5 = local_FriendlyTechSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_5 = local_FriendlyTechSpawned_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_5.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_5, logic_SubGraph_SaveLoadBool_boolAsVariable_5, logic_SubGraph_SaveLoadBool_uniqueID_5);
	}

	private void Relay_Save_Out_6()
	{
		Relay_Save_42();
	}

	private void Relay_Load_Out_6()
	{
		Relay_Load_42();
	}

	private void Relay_Restart_Out_6()
	{
		Relay_Set_False_42();
	}

	private void Relay_Save_6()
	{
		logic_SubGraph_SaveLoadBool_boolean_6 = local_MsgFoundShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_6 = local_MsgFoundShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_6.Save(ref logic_SubGraph_SaveLoadBool_boolean_6, logic_SubGraph_SaveLoadBool_boolAsVariable_6, logic_SubGraph_SaveLoadBool_uniqueID_6);
	}

	private void Relay_Load_6()
	{
		logic_SubGraph_SaveLoadBool_boolean_6 = local_MsgFoundShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_6 = local_MsgFoundShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_6.Load(ref logic_SubGraph_SaveLoadBool_boolean_6, logic_SubGraph_SaveLoadBool_boolAsVariable_6, logic_SubGraph_SaveLoadBool_uniqueID_6);
	}

	private void Relay_Set_True_6()
	{
		logic_SubGraph_SaveLoadBool_boolean_6 = local_MsgFoundShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_6 = local_MsgFoundShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_6.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_6, logic_SubGraph_SaveLoadBool_boolAsVariable_6, logic_SubGraph_SaveLoadBool_uniqueID_6);
	}

	private void Relay_Set_False_6()
	{
		logic_SubGraph_SaveLoadBool_boolean_6 = local_MsgFoundShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_6 = local_MsgFoundShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_6.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_6, logic_SubGraph_SaveLoadBool_boolAsVariable_6, logic_SubGraph_SaveLoadBool_uniqueID_6);
	}

	private void Relay_SaveEvent_9()
	{
		Relay_Save_5();
	}

	private void Relay_LoadEvent_9()
	{
		Relay_Load_5();
	}

	private void Relay_RestartEvent_9()
	{
		Relay_Set_False_5();
	}

	private void Relay_OnUpdate_10()
	{
		Relay_In_151();
	}

	private void Relay_OnSuspend_10()
	{
	}

	private void Relay_OnResume_10()
	{
	}

	private void Relay_In_13()
	{
		logic_uScript_IsPlayerInRangeOfTech_tech_13 = local_FriendlyTech_Tank;
		logic_uScript_IsPlayerInRangeOfTech_range_13 = local_distAtWhichFriendlyTechFound_System_Single;
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_13.In(logic_uScript_IsPlayerInRangeOfTech_tech_13, logic_uScript_IsPlayerInRangeOfTech_range_13, logic_uScript_IsPlayerInRangeOfTech_techs_13);
		bool inRange = logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_13.InRange;
		bool outOfRange = logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_13.OutOfRange;
		if (inRange)
		{
			Relay_In_16();
		}
		if (outOfRange)
		{
			Relay_In_143();
		}
	}

	private void Relay_In_16()
	{
		logic_uScriptCon_CompareBool_Bool_16 = local_MsgFoundShown_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_16.In(logic_uScriptCon_CompareBool_Bool_16);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_16.False)
		{
			Relay_In_18();
		}
	}

	private void Relay_True_17()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_17.True(out logic_uScriptAct_SetBool_Target_17);
		local_MsgFoundShown_System_Boolean = logic_uScriptAct_SetBool_Target_17;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_17.Out)
		{
			Relay_True_32();
		}
	}

	private void Relay_False_17()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_17.False(out logic_uScriptAct_SetBool_Target_17);
		local_MsgFoundShown_System_Boolean = logic_uScriptAct_SetBool_Target_17;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_17.Out)
		{
			Relay_True_32();
		}
	}

	private void Relay_In_18()
	{
		int num = 0;
		Array array = msgFriendlyTechFound;
		if (logic_uScript_AddOnScreenMessage_locString_18.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_18, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_18, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_speaker_18 = messageSpeaker;
		logic_uScript_AddOnScreenMessage_Return_18 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_18.In(logic_uScript_AddOnScreenMessage_locString_18, logic_uScript_AddOnScreenMessage_msgPriority_18, logic_uScript_AddOnScreenMessage_holdMsg_18, logic_uScript_AddOnScreenMessage_tag_18, logic_uScript_AddOnScreenMessage_speaker_18, logic_uScript_AddOnScreenMessage_side_18);
		if (logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_18.Shown)
		{
			Relay_True_17();
		}
	}

	private void Relay_In_21()
	{
		logic_uScriptCon_CompareBool_Bool_21 = local_FriendlyTechSpawned_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_21.In(logic_uScriptCon_CompareBool_Bool_21);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_21.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_21.False;
		if (num)
		{
			Relay_In_166();
		}
		if (flag)
		{
			Relay_InitialSpawn_2();
		}
	}

	private void Relay_In_22()
	{
		int num = 0;
		Array array = msgMissionComplete;
		if (logic_uScript_AddOnScreenMessage_locString_22.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_22, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_22, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_speaker_22 = messageSpeaker;
		logic_uScript_AddOnScreenMessage_Return_22 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_22.In(logic_uScript_AddOnScreenMessage_locString_22, logic_uScript_AddOnScreenMessage_msgPriority_22, logic_uScript_AddOnScreenMessage_holdMsg_22, logic_uScript_AddOnScreenMessage_tag_22, logic_uScript_AddOnScreenMessage_speaker_22, logic_uScript_AddOnScreenMessage_side_22);
		if (logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_22.Out)
		{
			Relay_In_167();
		}
	}

	private void Relay_In_26()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_26 = local_Msg_System_String;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_26.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_26, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_26);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_26.Out)
		{
			Relay_In_22();
		}
	}

	private void Relay_In_28()
	{
		logic_uScriptCon_CompareBool_Bool_28 = local_TimerStarted_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_28.In(logic_uScriptCon_CompareBool_Bool_28);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_28.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_28.False;
		if (num)
		{
			Relay_In_77();
		}
		if (flag)
		{
			Relay_In_43();
		}
	}

	private void Relay_In_30()
	{
		logic_uScriptCon_CompareBool_Bool_30 = local_FriendlyTechFound_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_30.In(logic_uScriptCon_CompareBool_Bool_30);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_30.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_30.False;
		if (num)
		{
			Relay_In_28();
		}
		if (flag)
		{
			Relay_In_13();
		}
	}

	private void Relay_True_32()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_32.True(out logic_uScriptAct_SetBool_Target_32);
		local_FriendlyTechFound_System_Boolean = logic_uScriptAct_SetBool_Target_32;
	}

	private void Relay_False_32()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_32.False(out logic_uScriptAct_SetBool_Target_32);
		local_FriendlyTechFound_System_Boolean = logic_uScriptAct_SetBool_Target_32;
	}

	private void Relay_Save_Out_33()
	{
		Relay_Save_150();
	}

	private void Relay_Load_Out_33()
	{
		Relay_Load_150();
	}

	private void Relay_Restart_Out_33()
	{
		Relay_Set_False_150();
	}

	private void Relay_Save_33()
	{
		logic_SubGraph_SaveLoadBool_boolean_33 = local_FriendlyTechFound_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_33 = local_FriendlyTechFound_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_33.Save(ref logic_SubGraph_SaveLoadBool_boolean_33, logic_SubGraph_SaveLoadBool_boolAsVariable_33, logic_SubGraph_SaveLoadBool_uniqueID_33);
	}

	private void Relay_Load_33()
	{
		logic_SubGraph_SaveLoadBool_boolean_33 = local_FriendlyTechFound_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_33 = local_FriendlyTechFound_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_33.Load(ref logic_SubGraph_SaveLoadBool_boolean_33, logic_SubGraph_SaveLoadBool_boolAsVariable_33, logic_SubGraph_SaveLoadBool_uniqueID_33);
	}

	private void Relay_Set_True_33()
	{
		logic_SubGraph_SaveLoadBool_boolean_33 = local_FriendlyTechFound_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_33 = local_FriendlyTechFound_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_33.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_33, logic_SubGraph_SaveLoadBool_boolAsVariable_33, logic_SubGraph_SaveLoadBool_uniqueID_33);
	}

	private void Relay_Set_False_33()
	{
		logic_SubGraph_SaveLoadBool_boolean_33 = local_FriendlyTechFound_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_33 = local_FriendlyTechFound_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_33.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_33, logic_SubGraph_SaveLoadBool_boolAsVariable_33, logic_SubGraph_SaveLoadBool_uniqueID_33);
	}

	private void Relay_Succeed_34()
	{
		logic_uScript_FinishEncounter_owner_34 = owner_Connection_24;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_34.Succeed(logic_uScript_FinishEncounter_owner_34);
	}

	private void Relay_Fail_34()
	{
		logic_uScript_FinishEncounter_owner_34 = owner_Connection_24;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_34.Fail(logic_uScript_FinishEncounter_owner_34);
	}

	private void Relay_In_38()
	{
		logic_uScript_SetTankTeam_tank_38 = local_FriendlyTech_Tank;
		logic_uScript_SetTankTeam_team_38 = local_168_System_Int32;
		logic_uScript_SetTankTeam_uScript_SetTankTeam_38.In(logic_uScript_SetTankTeam_tank_38, logic_uScript_SetTankTeam_team_38);
		if (logic_uScript_SetTankTeam_uScript_SetTankTeam_38.Out)
		{
			Relay_Succeed_34();
		}
	}

	private void Relay_Save_Out_42()
	{
		Relay_Save_99();
	}

	private void Relay_Load_Out_42()
	{
		Relay_Load_99();
	}

	private void Relay_Restart_Out_42()
	{
		Relay_Restart_99();
	}

	private void Relay_Save_42()
	{
		logic_SubGraph_SaveLoadBool_boolean_42 = local_TimerStarted_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_42 = local_TimerStarted_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_42.Save(ref logic_SubGraph_SaveLoadBool_boolean_42, logic_SubGraph_SaveLoadBool_boolAsVariable_42, logic_SubGraph_SaveLoadBool_uniqueID_42);
	}

	private void Relay_Load_42()
	{
		logic_SubGraph_SaveLoadBool_boolean_42 = local_TimerStarted_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_42 = local_TimerStarted_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_42.Load(ref logic_SubGraph_SaveLoadBool_boolean_42, logic_SubGraph_SaveLoadBool_boolAsVariable_42, logic_SubGraph_SaveLoadBool_uniqueID_42);
	}

	private void Relay_Set_True_42()
	{
		logic_SubGraph_SaveLoadBool_boolean_42 = local_TimerStarted_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_42 = local_TimerStarted_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_42.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_42, logic_SubGraph_SaveLoadBool_boolAsVariable_42, logic_SubGraph_SaveLoadBool_uniqueID_42);
	}

	private void Relay_Set_False_42()
	{
		logic_SubGraph_SaveLoadBool_boolean_42 = local_TimerStarted_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_42 = local_TimerStarted_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_42.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_42, logic_SubGraph_SaveLoadBool_boolAsVariable_42, logic_SubGraph_SaveLoadBool_uniqueID_42);
	}

	private void Relay_In_43()
	{
		logic_uScript_StartEncounterTimer_owner_43 = owner_Connection_44;
		logic_uScript_StartEncounterTimer_uScript_StartEncounterTimer_43.In(logic_uScript_StartEncounterTimer_owner_43);
		if (logic_uScript_StartEncounterTimer_uScript_StartEncounterTimer_43.Out)
		{
			Relay_True_45();
		}
	}

	private void Relay_True_45()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_45.True(out logic_uScriptAct_SetBool_Target_45);
		local_TimerStarted_System_Boolean = logic_uScriptAct_SetBool_Target_45;
	}

	private void Relay_False_45()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_45.False(out logic_uScriptAct_SetBool_Target_45);
		local_TimerStarted_System_Boolean = logic_uScriptAct_SetBool_Target_45;
	}

	private void Relay_AtIndex_51()
	{
		int num = 0;
		Array array = local_Techs_TankArray;
		if (logic_uScript_AccessListTech_techList_51.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_51, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_51, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_51.AtIndex(ref logic_uScript_AccessListTech_techList_51, logic_uScript_AccessListTech_index_51, out logic_uScript_AccessListTech_value_51);
		local_Techs_TankArray = logic_uScript_AccessListTech_techList_51;
		local_FriendlyTech_Tank = logic_uScript_AccessListTech_value_51;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_51.Out)
		{
			Relay_In_57();
		}
	}

	private void Relay_In_55()
	{
		logic_uScript_MoveEncounterWithVisible_ownerNode_55 = owner_Connection_56;
		logic_uScript_MoveEncounterWithVisible_visibleObject_55 = local_FriendlyTech_Tank;
		logic_uScript_MoveEncounterWithVisible_uScript_MoveEncounterWithVisible_55.In(logic_uScript_MoveEncounterWithVisible_ownerNode_55, logic_uScript_MoveEncounterWithVisible_visibleObject_55);
		if (logic_uScript_MoveEncounterWithVisible_uScript_MoveEncounterWithVisible_55.Out)
		{
			Relay_In_30();
		}
	}

	private void Relay_In_57()
	{
		logic_uScript_SetEncounterTarget_owner_57 = owner_Connection_47;
		logic_uScript_SetEncounterTarget_visibleObject_57 = local_FriendlyTech_Tank;
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_57.In(logic_uScript_SetEncounterTarget_owner_57, logic_uScript_SetEncounterTarget_visibleObject_57);
		if (logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_57.Out)
		{
			Relay_In_55();
		}
	}

	private void Relay_In_58()
	{
		int num = 0;
		Array array = friendlyTechData;
		if (logic_uScript_GetAndCheckTechs_techData_58.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_58, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techData_58, num, array.Length);
		num += array.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_58 = owner_Connection_50;
		int num2 = 0;
		Array array2 = local_Techs_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_58.Length != num2 + array2.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_58, num2 + array2.Length);
		}
		Array.Copy(array2, 0, logic_uScript_GetAndCheckTechs_techs_58, num2, array2.Length);
		num2 += array2.Length;
		logic_uScript_GetAndCheckTechs_Return_58 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_58.In(logic_uScript_GetAndCheckTechs_techData_58, logic_uScript_GetAndCheckTechs_ownerNode_58, ref logic_uScript_GetAndCheckTechs_techs_58);
		local_Techs_TankArray = logic_uScript_GetAndCheckTechs_techs_58;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_58.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_58.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_58.AllDead;
		if (allAlive)
		{
			Relay_AtIndex_51();
		}
		if (someAlive)
		{
			Relay_AtIndex_51();
		}
		if (allDead)
		{
			Relay_In_106();
		}
	}

	private void Relay_In_60()
	{
		int num = 0;
		Array array = friendlyTechData;
		if (logic_uScript_GetAndCheckTechs_techData_60.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_60, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techData_60, num, array.Length);
		num += array.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_60 = owner_Connection_61;
		int num2 = 0;
		Array array2 = local_Techs_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_60.Length != num2 + array2.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_60, num2 + array2.Length);
		}
		Array.Copy(array2, 0, logic_uScript_GetAndCheckTechs_techs_60, num2, array2.Length);
		num2 += array2.Length;
		logic_uScript_GetAndCheckTechs_Return_60 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_60.In(logic_uScript_GetAndCheckTechs_techData_60, logic_uScript_GetAndCheckTechs_ownerNode_60, ref logic_uScript_GetAndCheckTechs_techs_60);
		local_Techs_TankArray = logic_uScript_GetAndCheckTechs_techs_60;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_60.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_60.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_60.AllDead;
		if (allAlive)
		{
			Relay_TimeRemainingPercent_64();
		}
		if (someAlive)
		{
			Relay_TimeRemainingPercent_64();
		}
		if (allDead)
		{
			Relay_In_93();
		}
	}

	private void Relay_TimeRemaining_64()
	{
		logic_uScript_GetEncounterTimeRemaining_owner_64 = owner_Connection_65;
		logic_uScript_GetEncounterTimeRemaining_Return_64 = logic_uScript_GetEncounterTimeRemaining_uScript_GetEncounterTimeRemaining_64.TimeRemaining(logic_uScript_GetEncounterTimeRemaining_owner_64);
		local_TimerCount_System_Single = logic_uScript_GetEncounterTimeRemaining_Return_64;
		if (logic_uScript_GetEncounterTimeRemaining_uScript_GetEncounterTimeRemaining_64.Out)
		{
			Relay_In_67();
		}
	}

	private void Relay_TimeRemainingPercent_64()
	{
		logic_uScript_GetEncounterTimeRemaining_owner_64 = owner_Connection_65;
		logic_uScript_GetEncounterTimeRemaining_Return_64 = logic_uScript_GetEncounterTimeRemaining_uScript_GetEncounterTimeRemaining_64.TimeRemainingPercent(logic_uScript_GetEncounterTimeRemaining_owner_64);
		local_TimerCount_System_Single = logic_uScript_GetEncounterTimeRemaining_Return_64;
		if (logic_uScript_GetEncounterTimeRemaining_uScript_GetEncounterTimeRemaining_64.Out)
		{
			Relay_In_67();
		}
	}

	private void Relay_In_67()
	{
		logic_uScriptCon_CompareFloat_A_67 = local_TimerCount_System_Single;
		logic_uScriptCon_CompareFloat_uScriptCon_CompareFloat_67.In(logic_uScriptCon_CompareFloat_A_67, logic_uScriptCon_CompareFloat_B_67);
		bool greaterThan = logic_uScriptCon_CompareFloat_uScriptCon_CompareFloat_67.GreaterThan;
		bool equalTo = logic_uScriptCon_CompareFloat_uScriptCon_CompareFloat_67.EqualTo;
		if (greaterThan)
		{
			Relay_In_118();
		}
		if (equalTo)
		{
			Relay_In_26();
		}
	}

	private void Relay_Succeed_69()
	{
		logic_uScript_FinishEncounter_owner_69 = owner_Connection_68;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_69.Succeed(logic_uScript_FinishEncounter_owner_69);
	}

	private void Relay_Fail_69()
	{
		logic_uScript_FinishEncounter_owner_69 = owner_Connection_68;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_69.Fail(logic_uScript_FinishEncounter_owner_69);
	}

	private void Relay_In_72()
	{
		int num = 0;
		Array array = msgMissionFailedDeath;
		if (logic_uScript_AddOnScreenMessage_locString_72.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_72, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_72, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_speaker_72 = messageSpeaker;
		logic_uScript_AddOnScreenMessage_Return_72 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_72.In(logic_uScript_AddOnScreenMessage_locString_72, logic_uScript_AddOnScreenMessage_msgPriority_72, logic_uScript_AddOnScreenMessage_holdMsg_72, logic_uScript_AddOnScreenMessage_tag_72, logic_uScript_AddOnScreenMessage_speaker_72, logic_uScript_AddOnScreenMessage_side_72);
		if (logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_72.Out)
		{
			Relay_In_95();
		}
	}

	private void Relay_Succeed_75()
	{
		logic_uScript_FinishEncounter_owner_75 = owner_Connection_73;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_75.Succeed(logic_uScript_FinishEncounter_owner_75);
	}

	private void Relay_Fail_75()
	{
		logic_uScript_FinishEncounter_owner_75 = owner_Connection_73;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_75.Fail(logic_uScript_FinishEncounter_owner_75);
	}

	private void Relay_In_77()
	{
		logic_uScript_IsPlayerInRangeOfTech_tech_77 = local_FriendlyTech_Tank;
		logic_uScript_IsPlayerInRangeOfTech_range_77 = local_distAtWhichFriendlyTechAbandonded_System_Single;
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_77.In(logic_uScript_IsPlayerInRangeOfTech_tech_77, logic_uScript_IsPlayerInRangeOfTech_range_77, logic_uScript_IsPlayerInRangeOfTech_techs_77);
		bool inRange = logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_77.InRange;
		bool outOfRange = logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_77.OutOfRange;
		if (inRange)
		{
			Relay_In_87();
		}
		if (outOfRange)
		{
			Relay_In_91();
		}
	}

	private void Relay_In_80()
	{
		int num = 0;
		Array array = msgMissionFailedDistance;
		if (logic_uScript_AddOnScreenMessage_locString_80.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_80, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_80, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_speaker_80 = messageSpeaker;
		logic_uScript_AddOnScreenMessage_Return_80 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_80.In(logic_uScript_AddOnScreenMessage_locString_80, logic_uScript_AddOnScreenMessage_msgPriority_80, logic_uScript_AddOnScreenMessage_holdMsg_80, logic_uScript_AddOnScreenMessage_tag_80, logic_uScript_AddOnScreenMessage_speaker_80, logic_uScript_AddOnScreenMessage_side_80);
		if (logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_80.Out)
		{
			Relay_In_98();
		}
	}

	private void Relay_Succeed_83()
	{
		logic_uScript_FinishEncounter_owner_83 = owner_Connection_84;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_83.Succeed(logic_uScript_FinishEncounter_owner_83);
	}

	private void Relay_Fail_83()
	{
		logic_uScript_FinishEncounter_owner_83 = owner_Connection_84;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_83.Fail(logic_uScript_FinishEncounter_owner_83);
	}

	private void Relay_In_87()
	{
		logic_uScript_IsPlayerInRangeOfTech_tech_87 = local_FriendlyTech_Tank;
		logic_uScript_IsPlayerInRangeOfTech_range_87 = local_distAtWhichLeavingMissionArea_System_Single;
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_87.In(logic_uScript_IsPlayerInRangeOfTech_tech_87, logic_uScript_IsPlayerInRangeOfTech_range_87, logic_uScript_IsPlayerInRangeOfTech_techs_87);
		bool inRange = logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_87.InRange;
		bool outOfRange = logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_87.OutOfRange;
		if (inRange)
		{
			Relay_In_60();
		}
		if (outOfRange)
		{
			Relay_In_88();
		}
	}

	private void Relay_In_88()
	{
		int num = 0;
		Array array = msgLeavingMissionArea;
		if (logic_uScript_AddOnScreenMessage_locString_88.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_88, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_88, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_tag_88 = local_Msg_System_String;
		logic_uScript_AddOnScreenMessage_speaker_88 = messageSpeaker;
		logic_uScript_AddOnScreenMessage_Return_88 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_88.In(logic_uScript_AddOnScreenMessage_locString_88, logic_uScript_AddOnScreenMessage_msgPriority_88, logic_uScript_AddOnScreenMessage_holdMsg_88, logic_uScript_AddOnScreenMessage_tag_88, logic_uScript_AddOnScreenMessage_speaker_88, logic_uScript_AddOnScreenMessage_side_88);
	}

	private void Relay_In_91()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_91 = local_Msg_System_String;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_91.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_91, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_91);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_91.Out)
		{
			Relay_In_80();
		}
	}

	private void Relay_In_93()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_93 = local_Msg_System_String;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_93.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_93, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_93);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_93.Out)
		{
			Relay_In_72();
		}
	}

	private void Relay_In_95()
	{
		logic_uScript_RemoveEncounterTimer_owner_95 = owner_Connection_96;
		logic_uScript_RemoveEncounterTimer_uScript_RemoveEncounterTimer_95.In(logic_uScript_RemoveEncounterTimer_owner_95);
		if (logic_uScript_RemoveEncounterTimer_uScript_RemoveEncounterTimer_95.Out)
		{
			Relay_Fail_69();
		}
	}

	private void Relay_In_98()
	{
		logic_uScript_RemoveEncounterTimer_owner_98 = owner_Connection_97;
		logic_uScript_RemoveEncounterTimer_uScript_RemoveEncounterTimer_98.In(logic_uScript_RemoveEncounterTimer_owner_98);
		if (logic_uScript_RemoveEncounterTimer_uScript_RemoveEncounterTimer_98.Out)
		{
			Relay_In_161();
		}
	}

	private void Relay_Save_Out_99()
	{
		Relay_Save_137();
	}

	private void Relay_Load_Out_99()
	{
		Relay_Load_137();
	}

	private void Relay_Restart_Out_99()
	{
		Relay_Set_False_137();
	}

	private void Relay_Save_99()
	{
		logic_SubGraph_SaveLoadInt_restartValue_99 = local_StartValue_System_Int32;
		logic_SubGraph_SaveLoadInt_integer_99 = local_TotalGroupEnemiesSpawned_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_99 = local_TotalGroupEnemiesSpawned_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_99.Save(logic_SubGraph_SaveLoadInt_restartValue_99, ref logic_SubGraph_SaveLoadInt_integer_99, logic_SubGraph_SaveLoadInt_intAsVariable_99, logic_SubGraph_SaveLoadInt_uniqueID_99);
	}

	private void Relay_Load_99()
	{
		logic_SubGraph_SaveLoadInt_restartValue_99 = local_StartValue_System_Int32;
		logic_SubGraph_SaveLoadInt_integer_99 = local_TotalGroupEnemiesSpawned_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_99 = local_TotalGroupEnemiesSpawned_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_99.Load(logic_SubGraph_SaveLoadInt_restartValue_99, ref logic_SubGraph_SaveLoadInt_integer_99, logic_SubGraph_SaveLoadInt_intAsVariable_99, logic_SubGraph_SaveLoadInt_uniqueID_99);
	}

	private void Relay_Restart_99()
	{
		logic_SubGraph_SaveLoadInt_restartValue_99 = local_StartValue_System_Int32;
		logic_SubGraph_SaveLoadInt_integer_99 = local_TotalGroupEnemiesSpawned_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_99 = local_TotalGroupEnemiesSpawned_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_99.Restart(logic_SubGraph_SaveLoadInt_restartValue_99, ref logic_SubGraph_SaveLoadInt_integer_99, logic_SubGraph_SaveLoadInt_intAsVariable_99, logic_SubGraph_SaveLoadInt_uniqueID_99);
	}

	private void Relay_TickWave_100()
	{
		logic_uScript_SpawnTechWaveFromData_ownerNode_100 = owner_Connection_107;
		logic_uScript_SpawnTechWaveFromData_spawnData_100 = enemyGroupData;
		logic_uScript_SpawnTechWaveFromData_waveSize_100 = EnemyGroupSize;
		logic_uScript_SpawnTechWaveFromData_delayBetweenSpawns_100 = DelayBetweenEnemyGroupSpaws;
		logic_uScript_SpawnTechWaveFromData_numSpawnedSoFar_100 = local_TotalGroupEnemiesSpawned_System_Int32;
		logic_uScript_SpawnTechWaveFromData_allowRespawn_100 = AllowEnemyGroupToRespawn;
		logic_uScript_SpawnTechWaveFromData_respawnGroupSize_100 = ReinforcementSubGroupSize;
		logic_uScript_SpawnTechWaveFromData_delayBetweenRespawnGroups_100 = DelayBetweenRespawnArivals;
		logic_uScript_SpawnTechWaveFromData_Return_100 = logic_uScript_SpawnTechWaveFromData_uScript_SpawnTechWaveFromData_100.TickWave(logic_uScript_SpawnTechWaveFromData_ownerNode_100, logic_uScript_SpawnTechWaveFromData_spawnData_100, logic_uScript_SpawnTechWaveFromData_waveSize_100, logic_uScript_SpawnTechWaveFromData_delayBetweenSpawns_100, logic_uScript_SpawnTechWaveFromData_numSpawnedSoFar_100, logic_uScript_SpawnTechWaveFromData_allowRespawn_100, logic_uScript_SpawnTechWaveFromData_respawnGroupSize_100, logic_uScript_SpawnTechWaveFromData_delayBetweenRespawnGroups_100);
		local_TotalGroupEnemiesSpawned_System_Int32 = logic_uScript_SpawnTechWaveFromData_Return_100;
		if (logic_uScript_SpawnTechWaveFromData_uScript_SpawnTechWaveFromData_100.Out)
		{
			Relay_In_125();
		}
	}

	private void Relay_In_103()
	{
		int num = 0;
		Array array = msgMissionFailedDeath;
		if (logic_uScript_AddOnScreenMessage_locString_103.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_103, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_103, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_speaker_103 = messageSpeaker;
		logic_uScript_AddOnScreenMessage_Return_103 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_103.In(logic_uScript_AddOnScreenMessage_locString_103, logic_uScript_AddOnScreenMessage_msgPriority_103, logic_uScript_AddOnScreenMessage_holdMsg_103, logic_uScript_AddOnScreenMessage_tag_103, logic_uScript_AddOnScreenMessage_speaker_103, logic_uScript_AddOnScreenMessage_side_103);
		if (logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_103.Out)
		{
			Relay_In_105();
		}
	}

	private void Relay_In_105()
	{
		logic_uScript_RemoveEncounterTimer_owner_105 = owner_Connection_102;
		logic_uScript_RemoveEncounterTimer_uScript_RemoveEncounterTimer_105.In(logic_uScript_RemoveEncounterTimer_owner_105);
		if (logic_uScript_RemoveEncounterTimer_uScript_RemoveEncounterTimer_105.Out)
		{
			Relay_Fail_75();
		}
	}

	private void Relay_In_106()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_106 = local_Msg_System_String;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_106.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_106, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_106);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_106.Out)
		{
			Relay_In_103();
		}
	}

	private void Relay_True_115()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_115.True(out logic_uScriptAct_SetBool_Target_115);
		AllowEnemyGroupToRespawn = logic_uScriptAct_SetBool_Target_115;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_115.Out)
		{
			Relay_In_127();
		}
	}

	private void Relay_False_115()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_115.False(out logic_uScriptAct_SetBool_Target_115);
		AllowEnemyGroupToRespawn = logic_uScriptAct_SetBool_Target_115;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_115.Out)
		{
			Relay_In_127();
		}
	}

	private void Relay_In_118()
	{
		logic_uScriptCon_CompareFloat_A_118 = local_TimerCount_System_Single;
		logic_uScriptCon_CompareFloat_uScriptCon_CompareFloat_118.In(logic_uScriptCon_CompareFloat_A_118, logic_uScriptCon_CompareFloat_B_118);
		bool greaterThan = logic_uScriptCon_CompareFloat_uScriptCon_CompareFloat_118.GreaterThan;
		bool lessThanOrEqualTo = logic_uScriptCon_CompareFloat_uScriptCon_CompareFloat_118.LessThanOrEqualTo;
		if (greaterThan)
		{
			Relay_TickWave_100();
		}
		if (lessThanOrEqualTo)
		{
			Relay_False_115();
		}
	}

	private void Relay_In_121()
	{
		int num = 0;
		Array array = msgEnemiesIncoming;
		if (logic_uScript_AddOnScreenMessage_locString_121.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_121, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_121, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_tag_121 = local_Msg_System_String;
		logic_uScript_AddOnScreenMessage_speaker_121 = messageSpeaker;
		logic_uScript_AddOnScreenMessage_Return_121 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_121.In(logic_uScript_AddOnScreenMessage_locString_121, logic_uScript_AddOnScreenMessage_msgPriority_121, logic_uScript_AddOnScreenMessage_holdMsg_121, logic_uScript_AddOnScreenMessage_tag_121, logic_uScript_AddOnScreenMessage_speaker_121, logic_uScript_AddOnScreenMessage_side_121);
		if (logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_121.Out)
		{
			Relay_True_123();
		}
	}

	private void Relay_True_123()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_123.True(out logic_uScriptAct_SetBool_Target_123);
		local_ShownMsgEnemiesIncoming_System_Boolean = logic_uScriptAct_SetBool_Target_123;
	}

	private void Relay_False_123()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_123.False(out logic_uScriptAct_SetBool_Target_123);
		local_ShownMsgEnemiesIncoming_System_Boolean = logic_uScriptAct_SetBool_Target_123;
	}

	private void Relay_In_125()
	{
		logic_uScriptCon_CompareBool_Bool_125 = local_ShownMsgEnemiesIncoming_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_125.In(logic_uScriptCon_CompareBool_Bool_125);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_125.False)
		{
			Relay_In_121();
		}
	}

	private void Relay_In_127()
	{
		logic_uScriptCon_CompareBool_Bool_127 = local_ShownMsgEnemiesGivingUp_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_127.In(logic_uScriptCon_CompareBool_Bool_127);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_127.False)
		{
			Relay_In_131();
		}
	}

	private void Relay_True_130()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_130.True(out logic_uScriptAct_SetBool_Target_130);
		local_ShownMsgEnemiesGivingUp_System_Boolean = logic_uScriptAct_SetBool_Target_130;
	}

	private void Relay_False_130()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_130.False(out logic_uScriptAct_SetBool_Target_130);
		local_ShownMsgEnemiesGivingUp_System_Boolean = logic_uScriptAct_SetBool_Target_130;
	}

	private void Relay_In_131()
	{
		int num = 0;
		Array array = msgEnemiesGivingUp;
		if (logic_uScript_AddOnScreenMessage_locString_131.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_131, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_131, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_tag_131 = local_Msg_System_String;
		logic_uScript_AddOnScreenMessage_speaker_131 = messageSpeaker;
		logic_uScript_AddOnScreenMessage_Return_131 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_131.In(logic_uScript_AddOnScreenMessage_locString_131, logic_uScript_AddOnScreenMessage_msgPriority_131, logic_uScript_AddOnScreenMessage_holdMsg_131, logic_uScript_AddOnScreenMessage_tag_131, logic_uScript_AddOnScreenMessage_speaker_131, logic_uScript_AddOnScreenMessage_side_131);
		if (logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_131.Out)
		{
			Relay_True_130();
		}
	}

	private void Relay_Save_Out_137()
	{
		Relay_Save_138();
	}

	private void Relay_Load_Out_137()
	{
		Relay_Load_138();
	}

	private void Relay_Restart_Out_137()
	{
		Relay_Set_False_138();
	}

	private void Relay_Save_137()
	{
		logic_SubGraph_SaveLoadBool_boolean_137 = local_ShownMsgEnemiesIncoming_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_137 = local_ShownMsgEnemiesIncoming_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_137.Save(ref logic_SubGraph_SaveLoadBool_boolean_137, logic_SubGraph_SaveLoadBool_boolAsVariable_137, logic_SubGraph_SaveLoadBool_uniqueID_137);
	}

	private void Relay_Load_137()
	{
		logic_SubGraph_SaveLoadBool_boolean_137 = local_ShownMsgEnemiesIncoming_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_137 = local_ShownMsgEnemiesIncoming_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_137.Load(ref logic_SubGraph_SaveLoadBool_boolean_137, logic_SubGraph_SaveLoadBool_boolAsVariable_137, logic_SubGraph_SaveLoadBool_uniqueID_137);
	}

	private void Relay_Set_True_137()
	{
		logic_SubGraph_SaveLoadBool_boolean_137 = local_ShownMsgEnemiesIncoming_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_137 = local_ShownMsgEnemiesIncoming_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_137.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_137, logic_SubGraph_SaveLoadBool_boolAsVariable_137, logic_SubGraph_SaveLoadBool_uniqueID_137);
	}

	private void Relay_Set_False_137()
	{
		logic_SubGraph_SaveLoadBool_boolean_137 = local_ShownMsgEnemiesIncoming_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_137 = local_ShownMsgEnemiesIncoming_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_137.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_137, logic_SubGraph_SaveLoadBool_boolAsVariable_137, logic_SubGraph_SaveLoadBool_uniqueID_137);
	}

	private void Relay_Save_Out_138()
	{
		Relay_Save_139();
	}

	private void Relay_Load_Out_138()
	{
		Relay_Load_139();
	}

	private void Relay_Restart_Out_138()
	{
		Relay_Set_True_139();
	}

	private void Relay_Save_138()
	{
		logic_SubGraph_SaveLoadBool_boolean_138 = local_ShownMsgEnemiesGivingUp_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_138 = local_ShownMsgEnemiesGivingUp_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_138.Save(ref logic_SubGraph_SaveLoadBool_boolean_138, logic_SubGraph_SaveLoadBool_boolAsVariable_138, logic_SubGraph_SaveLoadBool_uniqueID_138);
	}

	private void Relay_Load_138()
	{
		logic_SubGraph_SaveLoadBool_boolean_138 = local_ShownMsgEnemiesGivingUp_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_138 = local_ShownMsgEnemiesGivingUp_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_138.Load(ref logic_SubGraph_SaveLoadBool_boolean_138, logic_SubGraph_SaveLoadBool_boolAsVariable_138, logic_SubGraph_SaveLoadBool_uniqueID_138);
	}

	private void Relay_Set_True_138()
	{
		logic_SubGraph_SaveLoadBool_boolean_138 = local_ShownMsgEnemiesGivingUp_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_138 = local_ShownMsgEnemiesGivingUp_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_138.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_138, logic_SubGraph_SaveLoadBool_boolAsVariable_138, logic_SubGraph_SaveLoadBool_uniqueID_138);
	}

	private void Relay_Set_False_138()
	{
		logic_SubGraph_SaveLoadBool_boolean_138 = local_ShownMsgEnemiesGivingUp_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_138 = local_ShownMsgEnemiesGivingUp_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_138.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_138, logic_SubGraph_SaveLoadBool_boolAsVariable_138, logic_SubGraph_SaveLoadBool_uniqueID_138);
	}

	private void Relay_Save_Out_139()
	{
	}

	private void Relay_Load_Out_139()
	{
	}

	private void Relay_Restart_Out_139()
	{
	}

	private void Relay_Save_139()
	{
		logic_SubGraph_SaveLoadBool_boolean_139 = AllowEnemyGroupToRespawn;
		logic_SubGraph_SaveLoadBool_boolAsVariable_139 = AllowEnemyGroupToRespawn;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_139.Save(ref logic_SubGraph_SaveLoadBool_boolean_139, logic_SubGraph_SaveLoadBool_boolAsVariable_139, logic_SubGraph_SaveLoadBool_uniqueID_139);
	}

	private void Relay_Load_139()
	{
		logic_SubGraph_SaveLoadBool_boolean_139 = AllowEnemyGroupToRespawn;
		logic_SubGraph_SaveLoadBool_boolAsVariable_139 = AllowEnemyGroupToRespawn;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_139.Load(ref logic_SubGraph_SaveLoadBool_boolean_139, logic_SubGraph_SaveLoadBool_boolAsVariable_139, logic_SubGraph_SaveLoadBool_uniqueID_139);
	}

	private void Relay_Set_True_139()
	{
		logic_SubGraph_SaveLoadBool_boolean_139 = AllowEnemyGroupToRespawn;
		logic_SubGraph_SaveLoadBool_boolAsVariable_139 = AllowEnemyGroupToRespawn;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_139.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_139, logic_SubGraph_SaveLoadBool_boolAsVariable_139, logic_SubGraph_SaveLoadBool_uniqueID_139);
	}

	private void Relay_Set_False_139()
	{
		logic_SubGraph_SaveLoadBool_boolean_139 = AllowEnemyGroupToRespawn;
		logic_SubGraph_SaveLoadBool_boolAsVariable_139 = AllowEnemyGroupToRespawn;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_139.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_139, logic_SubGraph_SaveLoadBool_boolAsVariable_139, logic_SubGraph_SaveLoadBool_uniqueID_139);
	}

	private void Relay_In_143()
	{
		logic_uScript_IsPlayerInRangeOfTech_tech_143 = local_FriendlyTech_Tank;
		logic_uScript_IsPlayerInRangeOfTech_range_143 = local_distAtWhichDistressSignalSent_System_Single;
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_143.In(logic_uScript_IsPlayerInRangeOfTech_tech_143, logic_uScript_IsPlayerInRangeOfTech_range_143, logic_uScript_IsPlayerInRangeOfTech_techs_143);
		if (logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_143.InRange)
		{
			Relay_In_145();
		}
	}

	private void Relay_True_144()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_144.True(out logic_uScriptAct_SetBool_Target_144);
		local_MsgDistressShown_System_Boolean = logic_uScriptAct_SetBool_Target_144;
	}

	private void Relay_False_144()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_144.False(out logic_uScriptAct_SetBool_Target_144);
		local_MsgDistressShown_System_Boolean = logic_uScriptAct_SetBool_Target_144;
	}

	private void Relay_In_145()
	{
		logic_uScriptCon_CompareBool_Bool_145 = local_MsgDistressShown_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_145.In(logic_uScriptCon_CompareBool_Bool_145);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_145.False)
		{
			Relay_In_146();
		}
	}

	private void Relay_In_146()
	{
		int num = 0;
		Array array = msgDistressSignal;
		if (logic_uScript_AddOnScreenMessage_locString_146.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AddOnScreenMessage_locString_146, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AddOnScreenMessage_locString_146, num, array.Length);
		num += array.Length;
		logic_uScript_AddOnScreenMessage_speaker_146 = messageSpeaker;
		logic_uScript_AddOnScreenMessage_Return_146 = logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_146.In(logic_uScript_AddOnScreenMessage_locString_146, logic_uScript_AddOnScreenMessage_msgPriority_146, logic_uScript_AddOnScreenMessage_holdMsg_146, logic_uScript_AddOnScreenMessage_tag_146, logic_uScript_AddOnScreenMessage_speaker_146, logic_uScript_AddOnScreenMessage_side_146);
		if (logic_uScript_AddOnScreenMessage_uScript_AddOnScreenMessage_146.Out)
		{
			Relay_True_144();
		}
	}

	private void Relay_Save_Out_150()
	{
		Relay_Save_6();
	}

	private void Relay_Load_Out_150()
	{
		Relay_Load_6();
	}

	private void Relay_Restart_Out_150()
	{
		Relay_Set_False_6();
	}

	private void Relay_Save_150()
	{
		logic_SubGraph_SaveLoadBool_boolean_150 = local_MsgDistressShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_150 = local_MsgDistressShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_150.Save(ref logic_SubGraph_SaveLoadBool_boolean_150, logic_SubGraph_SaveLoadBool_boolAsVariable_150, logic_SubGraph_SaveLoadBool_uniqueID_150);
	}

	private void Relay_Load_150()
	{
		logic_SubGraph_SaveLoadBool_boolean_150 = local_MsgDistressShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_150 = local_MsgDistressShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_150.Load(ref logic_SubGraph_SaveLoadBool_boolean_150, logic_SubGraph_SaveLoadBool_boolAsVariable_150, logic_SubGraph_SaveLoadBool_uniqueID_150);
	}

	private void Relay_Set_True_150()
	{
		logic_SubGraph_SaveLoadBool_boolean_150 = local_MsgDistressShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_150 = local_MsgDistressShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_150.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_150, logic_SubGraph_SaveLoadBool_boolAsVariable_150, logic_SubGraph_SaveLoadBool_uniqueID_150);
	}

	private void Relay_Set_False_150()
	{
		logic_SubGraph_SaveLoadBool_boolean_150 = local_MsgDistressShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_150 = local_MsgDistressShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_150.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_150, logic_SubGraph_SaveLoadBool_boolAsVariable_150, logic_SubGraph_SaveLoadBool_uniqueID_150);
	}

	private void Relay_In_151()
	{
		logic_uScript_DiscoverBlock_uScript_DiscoverBlock_151.In(logic_uScript_DiscoverBlock_blockType_151);
		if (logic_uScript_DiscoverBlock_uScript_DiscoverBlock_151.Out)
		{
			Relay_In_21();
		}
	}

	private void Relay_In_161()
	{
		logic_uScript_SetTankTeam_tank_161 = local_FriendlyTech_Tank;
		logic_uScript_SetTankTeam_uScript_SetTankTeam_161.In(logic_uScript_SetTankTeam_tank_161, logic_uScript_SetTankTeam_team_161);
		if (logic_uScript_SetTankTeam_uScript_SetTankTeam_161.Out)
		{
			Relay_Fail_83();
		}
	}

	private void Relay_In_166()
	{
		logic_uScript_RemoveScenery_ownerNode_166 = owner_Connection_165;
		logic_uScript_RemoveScenery_positionName_166 = clearSceneryPos;
		logic_uScript_RemoveScenery_radius_166 = clearSceneryRadius;
		logic_uScript_RemoveScenery_uScript_RemoveScenery_166.In(logic_uScript_RemoveScenery_ownerNode_166, logic_uScript_RemoveScenery_positionName_166, logic_uScript_RemoveScenery_radius_166, logic_uScript_RemoveScenery_preventChunksSpawning_166);
		if (logic_uScript_RemoveScenery_uScript_RemoveScenery_166.Out)
		{
			Relay_In_58();
		}
	}

	private void Relay_In_167()
	{
		logic_uScript_GetPlayerTeam_Return_167 = logic_uScript_GetPlayerTeam_uScript_GetPlayerTeam_167.In();
		local_168_System_Int32 = logic_uScript_GetPlayerTeam_Return_167;
		if (logic_uScript_GetPlayerTeam_uScript_GetPlayerTeam_167.Out)
		{
			Relay_In_38();
		}
	}
}
