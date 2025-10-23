using System;
using UnityEngine;

[Serializable]
[NodePath("Graphs")]
[FriendlyName("", "")]
public class GC_Harvesting_Mission : uScriptLogic
{
	private GameObject parentGameObject;

	private uScript_GUI thisScriptsOnGuiListener;

	private bool m_RegisteredForEvents;

	public BlockTypes blockTypeSilo;

	public float distNPCFound;

	private Tank[] local_150_TankArray = new Tank[0];

	private ChunkTypes[] local_184_ChunkTypesArray = new ChunkTypes[10]
	{
		ChunkTypes.Wood,
		ChunkTypes.RubberJelly,
		ChunkTypes.PlumbiteOre,
		ChunkTypes.TitaniteOre,
		ChunkTypes.CarbiteOre,
		ChunkTypes.OleiteJelly,
		ChunkTypes.RoditeOre,
		ChunkTypes.IgniteShard,
		ChunkTypes.CelestiteShard,
		ChunkTypes.EruditeShard
	};

	private bool local_Init_System_Boolean;

	private bool local_msgIntroShown_System_Boolean;

	private bool local_NearNPC_System_Boolean;

	private Tank local_NPCTech_Tank;

	private int local_NumResourceTypes_System_Int32;

	private ChunkTypes local_RandomResourceType_ChunkTypes;

	private bool local_RandomResourceTypeSet_System_Boolean;

	private ChunkTypes local_ResourceType01_ChunkTypes;

	private ChunkTypes local_ResourceType02_ChunkTypes;

	private ChunkTypes local_ResourceType03_ChunkTypes;

	private ChunkTypes local_ResourceType04_ChunkTypes;

	private TankBlock local_SiloBlock_TankBlock;

	private int local_Stage_System_Int32 = 1;

	private float local_TimeRemaining_System_Single;

	public uScript_AddMessage.MessageSpeaker messageSpeaker;

	[Multiline(3)]
	public string messageTag = "";

	public uScript_AddMessage.MessageData msg01Intro;

	public uScript_AddMessage.MessageData msg02NPCFound;

	public uScript_AddMessage.MessageData msg03Complete;

	public uScript_AddMessage.MessageData msgLeavingMissionArea;

	[Multiline(3)]
	public string NPCPosition = "";

	public SpawnTechData[] NPCSpawnData = new SpawnTechData[0];

	public ChunkTypes[] resourceTypesAccepted = new ChunkTypes[0];

	public bool timedMission;

	public bool useRandomResourceType;

	private GameObject owner_Connection_1;

	private GameObject owner_Connection_3;

	private GameObject owner_Connection_30;

	private GameObject owner_Connection_44;

	private GameObject owner_Connection_70;

	private GameObject owner_Connection_102;

	private GameObject owner_Connection_103;

	private GameObject owner_Connection_106;

	private GameObject owner_Connection_107;

	private GameObject owner_Connection_115;

	private GameObject owner_Connection_120;

	private GameObject owner_Connection_124;

	private GameObject owner_Connection_139;

	private GameObject owner_Connection_142;

	private GameObject owner_Connection_145;

	private GameObject owner_Connection_148;

	private GameObject owner_Connection_170;

	private GameObject owner_Connection_174;

	private GameObject owner_Connection_179;

	private GameObject owner_Connection_232;

	private uScriptCon_ManualSwitch logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_4 = new uScriptCon_ManualSwitch();

	private int logic_uScriptCon_ManualSwitch_CurrentOutput_4;

	private uScript_IsPlayerInRangeOfTech logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_6 = new uScript_IsPlayerInRangeOfTech();

	private Tank logic_uScript_IsPlayerInRangeOfTech_tech_6;

	private float logic_uScript_IsPlayerInRangeOfTech_range_6 = 100f;

	private Tank[] logic_uScript_IsPlayerInRangeOfTech_techs_6 = new Tank[0];

	private bool logic_uScript_IsPlayerInRangeOfTech_Out_6 = true;

	private bool logic_uScript_IsPlayerInRangeOfTech_InRange_6 = true;

	private bool logic_uScript_IsPlayerInRangeOfTech_OutOfRange_6 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_8 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_8;

	private bool logic_uScriptAct_SetBool_Out_8 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_8 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_8 = true;

	private uScript_IsPlayerInRangeOfTech logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_9 = new uScript_IsPlayerInRangeOfTech();

	private Tank logic_uScript_IsPlayerInRangeOfTech_tech_9;

	private float logic_uScript_IsPlayerInRangeOfTech_range_9;

	private Tank[] logic_uScript_IsPlayerInRangeOfTech_techs_9 = new Tank[0];

	private bool logic_uScript_IsPlayerInRangeOfTech_Out_9 = true;

	private bool logic_uScript_IsPlayerInRangeOfTech_InRange_9 = true;

	private bool logic_uScript_IsPlayerInRangeOfTech_OutOfRange_9 = true;

	private uScript_SetEncounterTarget logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_11 = new uScript_SetEncounterTarget();

	private GameObject logic_uScript_SetEncounterTarget_owner_11;

	private object logic_uScript_SetEncounterTarget_visibleObject_11 = "";

	private bool logic_uScript_SetEncounterTarget_Out_11 = true;

	private uScript_PausePopulation logic_uScript_PausePopulation_uScript_PausePopulation_12 = new uScript_PausePopulation();

	private bool logic_uScript_PausePopulation_Out_12 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_14 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_14;

	private bool logic_uScriptCon_CompareBool_True_14 = true;

	private bool logic_uScriptCon_CompareBool_False_14 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_19 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_19;

	private bool logic_uScriptCon_CompareBool_True_19 = true;

	private bool logic_uScriptCon_CompareBool_False_19 = true;

	private uScript_PausePopulation logic_uScript_PausePopulation_uScript_PausePopulation_21 = new uScript_PausePopulation();

	private bool logic_uScript_PausePopulation_Out_21 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_23 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_23;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_23;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_23;

	private bool logic_uScript_AddMessage_Out_23 = true;

	private bool logic_uScript_AddMessage_Shown_23 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_26 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_26;

	private bool logic_uScriptAct_SetBool_Out_26 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_26 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_26 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_27 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_27;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_27;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_27;

	private bool logic_uScript_AddMessage_Out_27 = true;

	private bool logic_uScript_AddMessage_Shown_27 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_29 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_29;

	private bool logic_uScriptAct_SetBool_Out_29 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_29 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_29 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_32 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_32 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_32 = true;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_32 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_35 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_35;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_35 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_35 = "msgIntroShown";

	private uScript_RestrictItemPickup logic_uScript_RestrictItemPickup_uScript_RestrictItemPickup_36 = new uScript_RestrictItemPickup();

	private Tank logic_uScript_RestrictItemPickup_tech_36;

	private ChunkTypes[] logic_uScript_RestrictItemPickup_typesToAccept_36 = new ChunkTypes[0];

	private bool logic_uScript_RestrictItemPickup_Out_36 = true;

	private uScript_GetTankBlock logic_uScript_GetTankBlock_uScript_GetTankBlock_41 = new uScript_GetTankBlock();

	private Tank logic_uScript_GetTankBlock_tank_41;

	private BlockTypes logic_uScript_GetTankBlock_blockType_41;

	private TankBlock logic_uScript_GetTankBlock_Return_41;

	private bool logic_uScript_GetTankBlock_Out_41 = true;

	private bool logic_uScript_GetTankBlock_Returned_41 = true;

	private bool logic_uScript_GetTankBlock_NotFound_41 = true;

	private uScript_FinishEncounter logic_uScript_FinishEncounter_uScript_FinishEncounter_43 = new uScript_FinishEncounter();

	private GameObject logic_uScript_FinishEncounter_owner_43;

	private bool logic_uScript_FinishEncounter_Out_43 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_47 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_47;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_47;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_47;

	private bool logic_uScript_AddMessage_Out_47 = true;

	private bool logic_uScript_AddMessage_Shown_47 = true;

	private uScript_AI_SetBehaviourType logic_uScript_AI_SetBehaviourType_uScript_AI_SetBehaviourType_48 = new uScript_AI_SetBehaviourType();

	private Tank logic_uScript_AI_SetBehaviourType_tank_48;

	private TechAI.AITypes logic_uScript_AI_SetBehaviourType_aiType_48 = TechAI.AITypes.Idle;

	private bool logic_uScript_AI_SetBehaviourType_Out_48 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_52 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_52;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_52;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_52;

	private bool logic_uScript_AddMessage_Out_52 = true;

	private bool logic_uScript_AddMessage_Shown_52 = true;

	private SubGraph_CompleteObjectiveStage logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_53 = new SubGraph_CompleteObjectiveStage();

	private int logic_SubGraph_CompleteObjectiveStage_objectiveStage_53;

	private bool logic_SubGraph_CompleteObjectiveStage_isFinalObjective_53;

	private uScript_SetTankTeam logic_uScript_SetTankTeam_uScript_SetTankTeam_55 = new uScript_SetTankTeam();

	private Tank logic_uScript_SetTankTeam_tank_55;

	private int logic_uScript_SetTankTeam_team_55;

	private bool logic_uScript_SetTankTeam_Out_55 = true;

	private uScript_SetTankTeam logic_uScript_SetTankTeam_uScript_SetTankTeam_58 = new uScript_SetTankTeam();

	private Tank logic_uScript_SetTankTeam_tank_58;

	private int logic_uScript_SetTankTeam_team_58 = -2;

	private bool logic_uScript_SetTankTeam_Out_58 = true;

	private uScript_SetCustomRadarTeamID logic_uScript_SetCustomRadarTeamID_uScript_SetCustomRadarTeamID_59 = new uScript_SetCustomRadarTeamID();

	private Tank logic_uScript_SetCustomRadarTeamID_tech_59;

	private int logic_uScript_SetCustomRadarTeamID_radarTeamID_59 = -2;

	private bool logic_uScript_SetCustomRadarTeamID_Out_59 = true;

	private uScript_RemoveTech logic_uScript_RemoveTech_uScript_RemoveTech_61 = new uScript_RemoveTech();

	private Tank logic_uScript_RemoveTech_tech_61;

	private bool logic_uScript_RemoveTech_Out_61 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_65 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_65;

	private bool logic_uScriptAct_SetBool_Out_65 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_65 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_65 = true;

	private SubGraph_LoadObjectiveStates logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_66 = new SubGraph_LoadObjectiveStates();

	private int logic_SubGraph_LoadObjectiveStates_currentObjective_66;

	private uScript_SetQuestObjectiveVisible logic_uScript_SetQuestObjectiveVisible_uScript_SetQuestObjectiveVisible_69 = new uScript_SetQuestObjectiveVisible();

	private GameObject logic_uScript_SetQuestObjectiveVisible_owner_69;

	private int logic_uScript_SetQuestObjectiveVisible_objectiveId_69 = 3;

	private bool logic_uScript_SetQuestObjectiveVisible_visible_69 = true;

	private bool logic_uScript_SetQuestObjectiveVisible_Out_69 = true;

	private uScriptCon_CompareInt logic_uScriptCon_CompareInt_uScriptCon_CompareInt_71 = new uScriptCon_CompareInt();

	private int logic_uScriptCon_CompareInt_A_71;

	private int logic_uScriptCon_CompareInt_B_71 = 1;

	private bool logic_uScriptCon_CompareInt_GreaterThan_71 = true;

	private bool logic_uScriptCon_CompareInt_GreaterThanOrEqualTo_71 = true;

	private bool logic_uScriptCon_CompareInt_EqualTo_71 = true;

	private bool logic_uScriptCon_CompareInt_NotEqualTo_71 = true;

	private bool logic_uScriptCon_CompareInt_LessThanOrEqualTo_71 = true;

	private bool logic_uScriptCon_CompareInt_LessThan_71 = true;

	private uScriptCon_CompareInt logic_uScriptCon_CompareInt_uScriptCon_CompareInt_73 = new uScriptCon_CompareInt();

	private int logic_uScriptCon_CompareInt_A_73;

	private int logic_uScriptCon_CompareInt_B_73 = 2;

	private bool logic_uScriptCon_CompareInt_GreaterThan_73 = true;

	private bool logic_uScriptCon_CompareInt_GreaterThanOrEqualTo_73 = true;

	private bool logic_uScriptCon_CompareInt_EqualTo_73 = true;

	private bool logic_uScriptCon_CompareInt_NotEqualTo_73 = true;

	private bool logic_uScriptCon_CompareInt_LessThanOrEqualTo_73 = true;

	private bool logic_uScriptCon_CompareInt_LessThan_73 = true;

	private uScriptCon_CompareInt logic_uScriptCon_CompareInt_uScriptCon_CompareInt_75 = new uScriptCon_CompareInt();

	private int logic_uScriptCon_CompareInt_A_75;

	private int logic_uScriptCon_CompareInt_B_75 = 3;

	private bool logic_uScriptCon_CompareInt_GreaterThan_75 = true;

	private bool logic_uScriptCon_CompareInt_GreaterThanOrEqualTo_75 = true;

	private bool logic_uScriptCon_CompareInt_EqualTo_75 = true;

	private bool logic_uScriptCon_CompareInt_NotEqualTo_75 = true;

	private bool logic_uScriptCon_CompareInt_LessThanOrEqualTo_75 = true;

	private bool logic_uScriptCon_CompareInt_LessThan_75 = true;

	private uScriptCon_CompareInt logic_uScriptCon_CompareInt_uScriptCon_CompareInt_77 = new uScriptCon_CompareInt();

	private int logic_uScriptCon_CompareInt_A_77;

	private int logic_uScriptCon_CompareInt_B_77 = 4;

	private bool logic_uScriptCon_CompareInt_GreaterThan_77 = true;

	private bool logic_uScriptCon_CompareInt_GreaterThanOrEqualTo_77 = true;

	private bool logic_uScriptCon_CompareInt_EqualTo_77 = true;

	private bool logic_uScriptCon_CompareInt_NotEqualTo_77 = true;

	private bool logic_uScriptCon_CompareInt_LessThanOrEqualTo_77 = true;

	private bool logic_uScriptCon_CompareInt_LessThan_77 = true;

	private uScriptCon_CompareInt logic_uScriptCon_CompareInt_uScriptCon_CompareInt_79 = new uScriptCon_CompareInt();

	private int logic_uScriptCon_CompareInt_A_79;

	private int logic_uScriptCon_CompareInt_B_79 = 1;

	private bool logic_uScriptCon_CompareInt_GreaterThan_79 = true;

	private bool logic_uScriptCon_CompareInt_GreaterThanOrEqualTo_79 = true;

	private bool logic_uScriptCon_CompareInt_EqualTo_79 = true;

	private bool logic_uScriptCon_CompareInt_NotEqualTo_79 = true;

	private bool logic_uScriptCon_CompareInt_LessThanOrEqualTo_79 = true;

	private bool logic_uScriptCon_CompareInt_LessThan_79 = true;

	private uScriptCon_CompareInt logic_uScriptCon_CompareInt_uScriptCon_CompareInt_81 = new uScriptCon_CompareInt();

	private int logic_uScriptCon_CompareInt_A_81;

	private int logic_uScriptCon_CompareInt_B_81 = 2;

	private bool logic_uScriptCon_CompareInt_GreaterThan_81 = true;

	private bool logic_uScriptCon_CompareInt_GreaterThanOrEqualTo_81 = true;

	private bool logic_uScriptCon_CompareInt_EqualTo_81 = true;

	private bool logic_uScriptCon_CompareInt_NotEqualTo_81 = true;

	private bool logic_uScriptCon_CompareInt_LessThanOrEqualTo_81 = true;

	private bool logic_uScriptCon_CompareInt_LessThan_81 = true;

	private uScriptCon_CompareInt logic_uScriptCon_CompareInt_uScriptCon_CompareInt_83 = new uScriptCon_CompareInt();

	private int logic_uScriptCon_CompareInt_A_83;

	private int logic_uScriptCon_CompareInt_B_83 = 3;

	private bool logic_uScriptCon_CompareInt_GreaterThan_83 = true;

	private bool logic_uScriptCon_CompareInt_GreaterThanOrEqualTo_83 = true;

	private bool logic_uScriptCon_CompareInt_EqualTo_83 = true;

	private bool logic_uScriptCon_CompareInt_NotEqualTo_83 = true;

	private bool logic_uScriptCon_CompareInt_LessThanOrEqualTo_83 = true;

	private bool logic_uScriptCon_CompareInt_LessThan_83 = true;

	private uScriptCon_CompareInt logic_uScriptCon_CompareInt_uScriptCon_CompareInt_84 = new uScriptCon_CompareInt();

	private int logic_uScriptCon_CompareInt_A_84;

	private int logic_uScriptCon_CompareInt_B_84 = 4;

	private bool logic_uScriptCon_CompareInt_GreaterThan_84 = true;

	private bool logic_uScriptCon_CompareInt_GreaterThanOrEqualTo_84 = true;

	private bool logic_uScriptCon_CompareInt_EqualTo_84 = true;

	private bool logic_uScriptCon_CompareInt_NotEqualTo_84 = true;

	private bool logic_uScriptCon_CompareInt_LessThanOrEqualTo_84 = true;

	private bool logic_uScriptCon_CompareInt_LessThan_84 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_87 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_87 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_88 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_88 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_89 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_89 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_90 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_90 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_91 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_91 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_92 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_92 = true;

	private uScriptCon_CompareInt logic_uScriptCon_CompareInt_uScriptCon_CompareInt_93 = new uScriptCon_CompareInt();

	private int logic_uScriptCon_CompareInt_A_93;

	private int logic_uScriptCon_CompareInt_B_93 = 1;

	private bool logic_uScriptCon_CompareInt_GreaterThan_93 = true;

	private bool logic_uScriptCon_CompareInt_GreaterThanOrEqualTo_93 = true;

	private bool logic_uScriptCon_CompareInt_EqualTo_93 = true;

	private bool logic_uScriptCon_CompareInt_NotEqualTo_93 = true;

	private bool logic_uScriptCon_CompareInt_LessThanOrEqualTo_93 = true;

	private bool logic_uScriptCon_CompareInt_LessThan_93 = true;

	private uScriptCon_CompareInt logic_uScriptCon_CompareInt_uScriptCon_CompareInt_95 = new uScriptCon_CompareInt();

	private int logic_uScriptCon_CompareInt_A_95;

	private int logic_uScriptCon_CompareInt_B_95 = 2;

	private bool logic_uScriptCon_CompareInt_GreaterThan_95 = true;

	private bool logic_uScriptCon_CompareInt_GreaterThanOrEqualTo_95 = true;

	private bool logic_uScriptCon_CompareInt_EqualTo_95 = true;

	private bool logic_uScriptCon_CompareInt_NotEqualTo_95 = true;

	private bool logic_uScriptCon_CompareInt_LessThanOrEqualTo_95 = true;

	private bool logic_uScriptCon_CompareInt_LessThan_95 = true;

	private uScriptCon_CompareInt logic_uScriptCon_CompareInt_uScriptCon_CompareInt_99 = new uScriptCon_CompareInt();

	private int logic_uScriptCon_CompareInt_A_99;

	private int logic_uScriptCon_CompareInt_B_99 = 3;

	private bool logic_uScriptCon_CompareInt_GreaterThan_99 = true;

	private bool logic_uScriptCon_CompareInt_GreaterThanOrEqualTo_99 = true;

	private bool logic_uScriptCon_CompareInt_EqualTo_99 = true;

	private bool logic_uScriptCon_CompareInt_NotEqualTo_99 = true;

	private bool logic_uScriptCon_CompareInt_LessThanOrEqualTo_99 = true;

	private bool logic_uScriptCon_CompareInt_LessThan_99 = true;

	private uScriptCon_CompareInt logic_uScriptCon_CompareInt_uScriptCon_CompareInt_100 = new uScriptCon_CompareInt();

	private int logic_uScriptCon_CompareInt_A_100;

	private int logic_uScriptCon_CompareInt_B_100 = 4;

	private bool logic_uScriptCon_CompareInt_GreaterThan_100 = true;

	private bool logic_uScriptCon_CompareInt_GreaterThanOrEqualTo_100 = true;

	private bool logic_uScriptCon_CompareInt_EqualTo_100 = true;

	private bool logic_uScriptCon_CompareInt_NotEqualTo_100 = true;

	private bool logic_uScriptCon_CompareInt_LessThanOrEqualTo_100 = true;

	private bool logic_uScriptCon_CompareInt_LessThan_100 = true;

	private uScript_GetQuestObjectiveCompleted logic_uScript_GetQuestObjectiveCompleted_uScript_GetQuestObjectiveCompleted_101 = new uScript_GetQuestObjectiveCompleted();

	private GameObject logic_uScript_GetQuestObjectiveCompleted_owner_101;

	private int logic_uScript_GetQuestObjectiveCompleted_objectiveId_101 = 2;

	private bool logic_uScript_GetQuestObjectiveCompleted_Complete_101 = true;

	private bool logic_uScript_GetQuestObjectiveCompleted_Incomplete_101 = true;

	private uScript_GetQuestObjectiveCompleted logic_uScript_GetQuestObjectiveCompleted_uScript_GetQuestObjectiveCompleted_104 = new uScript_GetQuestObjectiveCompleted();

	private GameObject logic_uScript_GetQuestObjectiveCompleted_owner_104;

	private int logic_uScript_GetQuestObjectiveCompleted_objectiveId_104 = 3;

	private bool logic_uScript_GetQuestObjectiveCompleted_Complete_104 = true;

	private bool logic_uScript_GetQuestObjectiveCompleted_Incomplete_104 = true;

	private uScript_GetQuestObjectiveCompleted logic_uScript_GetQuestObjectiveCompleted_uScript_GetQuestObjectiveCompleted_105 = new uScript_GetQuestObjectiveCompleted();

	private GameObject logic_uScript_GetQuestObjectiveCompleted_owner_105;

	private int logic_uScript_GetQuestObjectiveCompleted_objectiveId_105 = 5;

	private bool logic_uScript_GetQuestObjectiveCompleted_Complete_105 = true;

	private bool logic_uScript_GetQuestObjectiveCompleted_Incomplete_105 = true;

	private uScript_GetQuestObjectiveCompleted logic_uScript_GetQuestObjectiveCompleted_uScript_GetQuestObjectiveCompleted_108 = new uScript_GetQuestObjectiveCompleted();

	private GameObject logic_uScript_GetQuestObjectiveCompleted_owner_108;

	private int logic_uScript_GetQuestObjectiveCompleted_objectiveId_108 = 4;

	private bool logic_uScript_GetQuestObjectiveCompleted_Complete_108 = true;

	private bool logic_uScript_GetQuestObjectiveCompleted_Incomplete_108 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_109 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_109 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_110 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_110 = true;

	private uScript_SetQuestObjectiveVisible logic_uScript_SetQuestObjectiveVisible_uScript_SetQuestObjectiveVisible_113 = new uScript_SetQuestObjectiveVisible();

	private GameObject logic_uScript_SetQuestObjectiveVisible_owner_113;

	private int logic_uScript_SetQuestObjectiveVisible_objectiveId_113;

	private bool logic_uScript_SetQuestObjectiveVisible_visible_113 = true;

	private bool logic_uScript_SetQuestObjectiveVisible_Out_113 = true;

	private uScriptAct_AddInt_v2 logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_114 = new uScriptAct_AddInt_v2();

	private int logic_uScriptAct_AddInt_v2_A_114;

	private int logic_uScriptAct_AddInt_v2_B_114;

	private int logic_uScriptAct_AddInt_v2_IntResult_114;

	private float logic_uScriptAct_AddInt_v2_FloatResult_114;

	private bool logic_uScriptAct_AddInt_v2_Out_114 = true;

	private uScriptCon_CompareInt logic_uScriptCon_CompareInt_uScriptCon_CompareInt_117 = new uScriptCon_CompareInt();

	private int logic_uScriptCon_CompareInt_A_117;

	private int logic_uScriptCon_CompareInt_B_117 = 2;

	private bool logic_uScriptCon_CompareInt_GreaterThan_117 = true;

	private bool logic_uScriptCon_CompareInt_GreaterThanOrEqualTo_117 = true;

	private bool logic_uScriptCon_CompareInt_EqualTo_117 = true;

	private bool logic_uScriptCon_CompareInt_NotEqualTo_117 = true;

	private bool logic_uScriptCon_CompareInt_LessThanOrEqualTo_117 = true;

	private bool logic_uScriptCon_CompareInt_LessThan_117 = true;

	private uScript_SetQuestObjectiveVisible logic_uScript_SetQuestObjectiveVisible_uScript_SetQuestObjectiveVisible_121 = new uScript_SetQuestObjectiveVisible();

	private GameObject logic_uScript_SetQuestObjectiveVisible_owner_121;

	private int logic_uScript_SetQuestObjectiveVisible_objectiveId_121 = 4;

	private bool logic_uScript_SetQuestObjectiveVisible_visible_121 = true;

	private bool logic_uScript_SetQuestObjectiveVisible_Out_121 = true;

	private uScriptCon_CompareInt logic_uScriptCon_CompareInt_uScriptCon_CompareInt_122 = new uScriptCon_CompareInt();

	private int logic_uScriptCon_CompareInt_A_122;

	private int logic_uScriptCon_CompareInt_B_122 = 3;

	private bool logic_uScriptCon_CompareInt_GreaterThan_122 = true;

	private bool logic_uScriptCon_CompareInt_GreaterThanOrEqualTo_122 = true;

	private bool logic_uScriptCon_CompareInt_EqualTo_122 = true;

	private bool logic_uScriptCon_CompareInt_NotEqualTo_122 = true;

	private bool logic_uScriptCon_CompareInt_LessThanOrEqualTo_122 = true;

	private bool logic_uScriptCon_CompareInt_LessThan_122 = true;

	private uScript_SetQuestObjectiveVisible logic_uScript_SetQuestObjectiveVisible_uScript_SetQuestObjectiveVisible_125 = new uScript_SetQuestObjectiveVisible();

	private GameObject logic_uScript_SetQuestObjectiveVisible_owner_125;

	private int logic_uScript_SetQuestObjectiveVisible_objectiveId_125 = 5;

	private bool logic_uScript_SetQuestObjectiveVisible_visible_125 = true;

	private bool logic_uScript_SetQuestObjectiveVisible_Out_125 = true;

	private uScriptCon_CompareInt logic_uScriptCon_CompareInt_uScriptCon_CompareInt_126 = new uScriptCon_CompareInt();

	private int logic_uScriptCon_CompareInt_A_126;

	private int logic_uScriptCon_CompareInt_B_126 = 4;

	private bool logic_uScriptCon_CompareInt_GreaterThan_126 = true;

	private bool logic_uScriptCon_CompareInt_GreaterThanOrEqualTo_126 = true;

	private bool logic_uScriptCon_CompareInt_EqualTo_126 = true;

	private bool logic_uScriptCon_CompareInt_NotEqualTo_126 = true;

	private bool logic_uScriptCon_CompareInt_LessThanOrEqualTo_126 = true;

	private bool logic_uScriptCon_CompareInt_LessThan_126 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_131 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_131;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_131 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_131 = "Init";

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_134 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_134 = true;

	private uScript_SetCustomRadarTeamID logic_uScript_SetCustomRadarTeamID_uScript_SetCustomRadarTeamID_135 = new uScript_SetCustomRadarTeamID();

	private Tank logic_uScript_SetCustomRadarTeamID_tech_135;

	private int logic_uScript_SetCustomRadarTeamID_radarTeamID_135 = -2;

	private bool logic_uScript_SetCustomRadarTeamID_Out_135 = true;

	private uScript_RemoveScenery logic_uScript_RemoveScenery_uScript_RemoveScenery_136 = new uScript_RemoveScenery();

	private GameObject logic_uScript_RemoveScenery_ownerNode_136;

	private string logic_uScript_RemoveScenery_positionName_136 = "";

	private float logic_uScript_RemoveScenery_radius_136 = 25f;

	private bool logic_uScript_RemoveScenery_preventChunksSpawning_136 = true;

	private bool logic_uScript_RemoveScenery_Out_136 = true;

	private uScript_SetTankInvulnerable logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_137 = new uScript_SetTankInvulnerable();

	private bool logic_uScript_SetTankInvulnerable_invulnerable_137 = true;

	private Tank logic_uScript_SetTankInvulnerable_tank_137;

	private bool logic_uScript_SetTankInvulnerable_Out_137 = true;

	private uScript_LockTech logic_uScript_LockTech_uScript_LockTech_141 = new uScript_LockTech();

	private Tank logic_uScript_LockTech_tech_141;

	private uScript_LockTech.TechLockType logic_uScript_LockTech_lockType_141 = uScript_LockTech.TechLockType.LockAll;

	private bool logic_uScript_LockTech_Out_141 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_143 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_143;

	private bool logic_uScriptCon_CompareBool_True_143 = true;

	private bool logic_uScriptCon_CompareBool_False_143 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_144 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_144;

	private bool logic_uScriptAct_SetBool_Out_144 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_144 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_144 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_146 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_146 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_146;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_146 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_146;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_146 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_146 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_146 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_146 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_147 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_147 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_147;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_147 = 0.3f;

	private bool logic_uScript_SpawnTechsFromData_Out_147 = true;

	private uScript_LockTechInteraction logic_uScript_LockTechInteraction_uScript_LockTechInteraction_149 = new uScript_LockTechInteraction();

	private Tank logic_uScript_LockTechInteraction_tech_149;

	private BlockTypes[] logic_uScript_LockTechInteraction_excludedBlocks_149 = new BlockTypes[0];

	private bool logic_uScript_LockTechInteraction_Out_149 = true;

	private uScript_DirectEnemiesOutOfEncounter logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_151 = new uScript_DirectEnemiesOutOfEncounter();

	private GameObject logic_uScript_DirectEnemiesOutOfEncounter_ownerNode_151;

	private bool logic_uScript_DirectEnemiesOutOfEncounter_Out_151 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_152 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_152 = new Tank[0];

	private int logic_uScript_AccessListTech_index_152;

	private Tank logic_uScript_AccessListTech_value_152;

	private bool logic_uScript_AccessListTech_Out_152 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_153 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_153 = true;

	private SubGraph_SaveLoadInt logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_161 = new SubGraph_SaveLoadInt();

	private int logic_SubGraph_SaveLoadInt_restartValue_161 = 1;

	private int logic_SubGraph_SaveLoadInt_integer_161;

	private object logic_SubGraph_SaveLoadInt_intAsVariable_161 = "";

	private string logic_SubGraph_SaveLoadInt_uniqueID_161 = "Stage";

	private uScriptCon_CompareInt logic_uScriptCon_CompareInt_uScriptCon_CompareInt_163 = new uScriptCon_CompareInt();

	private int logic_uScriptCon_CompareInt_A_163;

	private int logic_uScriptCon_CompareInt_B_163 = 1;

	private bool logic_uScriptCon_CompareInt_GreaterThan_163 = true;

	private bool logic_uScriptCon_CompareInt_GreaterThanOrEqualTo_163 = true;

	private bool logic_uScriptCon_CompareInt_EqualTo_163 = true;

	private bool logic_uScriptCon_CompareInt_NotEqualTo_163 = true;

	private bool logic_uScriptCon_CompareInt_LessThanOrEqualTo_163 = true;

	private bool logic_uScriptCon_CompareInt_LessThan_163 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_165 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_165;

	private bool logic_uScriptCon_CompareBool_True_165 = true;

	private bool logic_uScriptCon_CompareBool_False_165 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_167 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_167;

	private bool logic_uScriptCon_CompareBool_True_167 = true;

	private bool logic_uScriptCon_CompareBool_False_167 = true;

	private uScript_StartEncounterTimer logic_uScript_StartEncounterTimer_uScript_StartEncounterTimer_169 = new uScript_StartEncounterTimer();

	private GameObject logic_uScript_StartEncounterTimer_owner_169;

	private bool logic_uScript_StartEncounterTimer_Out_169 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_171 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_171 = true;

	private uScriptCon_CompareFloat logic_uScriptCon_CompareFloat_uScriptCon_CompareFloat_173 = new uScriptCon_CompareFloat();

	private float logic_uScriptCon_CompareFloat_A_173;

	private float logic_uScriptCon_CompareFloat_B_173;

	private bool logic_uScriptCon_CompareFloat_GreaterThan_173 = true;

	private bool logic_uScriptCon_CompareFloat_GreaterThanOrEqualTo_173 = true;

	private bool logic_uScriptCon_CompareFloat_EqualTo_173 = true;

	private bool logic_uScriptCon_CompareFloat_NotEqualTo_173 = true;

	private bool logic_uScriptCon_CompareFloat_LessThanOrEqualTo_173 = true;

	private bool logic_uScriptCon_CompareFloat_LessThan_173 = true;

	private uScript_GetEncounterTimeRemaining logic_uScript_GetEncounterTimeRemaining_uScript_GetEncounterTimeRemaining_175 = new uScript_GetEncounterTimeRemaining();

	private GameObject logic_uScript_GetEncounterTimeRemaining_owner_175;

	private float logic_uScript_GetEncounterTimeRemaining_Return_175;

	private bool logic_uScript_GetEncounterTimeRemaining_Out_175 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_177 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_177 = true;

	private uScript_FinishEncounter logic_uScript_FinishEncounter_uScript_FinishEncounter_178 = new uScript_FinishEncounter();

	private GameObject logic_uScript_FinishEncounter_owner_178;

	private bool logic_uScript_FinishEncounter_Out_178 = true;

	private uScript_GetRandomChunkType logic_uScript_GetRandomChunkType_uScript_GetRandomChunkType_182 = new uScript_GetRandomChunkType();

	private ChunkTypes[] logic_uScript_GetRandomChunkType_chunkList_182 = new ChunkTypes[0];

	private ChunkTypes logic_uScript_GetRandomChunkType_Return_182;

	private bool logic_uScript_GetRandomChunkType_Out_182 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_183 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_183;

	private bool logic_uScriptCon_CompareBool_True_183 = true;

	private bool logic_uScriptCon_CompareBool_False_183 = true;

	private uScript_RestrictItemPickup logic_uScript_RestrictItemPickup_uScript_RestrictItemPickup_186 = new uScript_RestrictItemPickup();

	private Tank logic_uScript_RestrictItemPickup_tech_186;

	private ChunkTypes[] logic_uScript_RestrictItemPickup_typesToAccept_186 = new ChunkTypes[0];

	private bool logic_uScript_RestrictItemPickup_Out_186 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_190 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_190;

	private bool logic_uScriptCon_CompareBool_True_190 = true;

	private bool logic_uScriptCon_CompareBool_False_190 = true;

	private SubGraph_Harvesting_Mission_ResourceTracker logic_SubGraph_Harvesting_Mission_ResourceTracker_SubGraph_Harvesting_Mission_ResourceTracker_191 = new SubGraph_Harvesting_Mission_ResourceTracker();

	private TankBlock logic_SubGraph_Harvesting_Mission_ResourceTracker_block_191;

	private ChunkTypes logic_SubGraph_Harvesting_Mission_ResourceTracker_resourceType_191;

	private int logic_SubGraph_Harvesting_Mission_ResourceTracker_objectiveID_191 = 2;

	private uScriptAct_Log logic_uScriptAct_Log_uScriptAct_Log_194 = new uScriptAct_Log();

	private object logic_uScriptAct_Log_Prefix_194 = "";

	private object[] logic_uScriptAct_Log_Target_194 = new object[0];

	private object logic_uScriptAct_Log_Postfix_194 = "";

	private bool logic_uScriptAct_Log_Out_194 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_195 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_195;

	private bool logic_uScriptCon_CompareBool_True_195 = true;

	private bool logic_uScriptCon_CompareBool_False_195 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_197 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_197;

	private bool logic_uScriptAct_SetBool_Out_197 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_197 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_197 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_198 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_198;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_198 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_198 = "RandomResourceTypeSet";

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_200 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_200 = true;

	private uScript_AccessListChunk logic_uScript_AccessListChunk_uScript_AccessListChunk_201 = new uScript_AccessListChunk();

	private ChunkTypes[] logic_uScript_AccessListChunk_chunkList_201 = new ChunkTypes[0];

	private int logic_uScript_AccessListChunk_index_201;

	private ChunkTypes logic_uScript_AccessListChunk_value_201;

	private bool logic_uScript_AccessListChunk_Out_201 = true;

	private SubGraph_Harvesting_Mission_ResourceTracker logic_SubGraph_Harvesting_Mission_ResourceTracker_SubGraph_Harvesting_Mission_ResourceTracker_204 = new SubGraph_Harvesting_Mission_ResourceTracker();

	private TankBlock logic_SubGraph_Harvesting_Mission_ResourceTracker_block_204;

	private ChunkTypes logic_SubGraph_Harvesting_Mission_ResourceTracker_resourceType_204;

	private int logic_SubGraph_Harvesting_Mission_ResourceTracker_objectiveID_204 = 2;

	private uScript_AccessListChunk logic_uScript_AccessListChunk_uScript_AccessListChunk_207 = new uScript_AccessListChunk();

	private ChunkTypes[] logic_uScript_AccessListChunk_chunkList_207 = new ChunkTypes[0];

	private int logic_uScript_AccessListChunk_index_207 = 1;

	private ChunkTypes logic_uScript_AccessListChunk_value_207;

	private bool logic_uScript_AccessListChunk_Out_207 = true;

	private SubGraph_Harvesting_Mission_ResourceTracker logic_SubGraph_Harvesting_Mission_ResourceTracker_SubGraph_Harvesting_Mission_ResourceTracker_211 = new SubGraph_Harvesting_Mission_ResourceTracker();

	private TankBlock logic_SubGraph_Harvesting_Mission_ResourceTracker_block_211;

	private ChunkTypes logic_SubGraph_Harvesting_Mission_ResourceTracker_resourceType_211;

	private int logic_SubGraph_Harvesting_Mission_ResourceTracker_objectiveID_211 = 3;

	private uScript_AccessListChunk logic_uScript_AccessListChunk_uScript_AccessListChunk_214 = new uScript_AccessListChunk();

	private ChunkTypes[] logic_uScript_AccessListChunk_chunkList_214 = new ChunkTypes[0];

	private int logic_uScript_AccessListChunk_index_214 = 2;

	private ChunkTypes logic_uScript_AccessListChunk_value_214;

	private bool logic_uScript_AccessListChunk_Out_214 = true;

	private SubGraph_Harvesting_Mission_ResourceTracker logic_SubGraph_Harvesting_Mission_ResourceTracker_SubGraph_Harvesting_Mission_ResourceTracker_216 = new SubGraph_Harvesting_Mission_ResourceTracker();

	private TankBlock logic_SubGraph_Harvesting_Mission_ResourceTracker_block_216;

	private ChunkTypes logic_SubGraph_Harvesting_Mission_ResourceTracker_resourceType_216;

	private int logic_SubGraph_Harvesting_Mission_ResourceTracker_objectiveID_216 = 4;

	private uScript_AccessListChunk logic_uScript_AccessListChunk_uScript_AccessListChunk_219 = new uScript_AccessListChunk();

	private ChunkTypes[] logic_uScript_AccessListChunk_chunkList_219 = new ChunkTypes[0];

	private int logic_uScript_AccessListChunk_index_219 = 3;

	private ChunkTypes logic_uScript_AccessListChunk_value_219;

	private bool logic_uScript_AccessListChunk_Out_219 = true;

	private SubGraph_Harvesting_Mission_ResourceTracker logic_SubGraph_Harvesting_Mission_ResourceTracker_SubGraph_Harvesting_Mission_ResourceTracker_222 = new SubGraph_Harvesting_Mission_ResourceTracker();

	private TankBlock logic_SubGraph_Harvesting_Mission_ResourceTracker_block_222;

	private ChunkTypes logic_SubGraph_Harvesting_Mission_ResourceTracker_resourceType_222;

	private int logic_SubGraph_Harvesting_Mission_ResourceTracker_objectiveID_222 = 5;

	private uScript_GetChunkListLength logic_uScript_GetChunkListLength_uScript_GetChunkListLength_225 = new uScript_GetChunkListLength();

	private ChunkTypes[] logic_uScript_GetChunkListLength_chunkList_225 = new ChunkTypes[0];

	private int logic_uScript_GetChunkListLength_Return_225;

	private bool logic_uScript_GetChunkListLength_Out_225 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_228 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_228 = 1;

	private int logic_uScriptAct_SetInt_Target_228;

	private bool logic_uScriptAct_SetInt_Out_228 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_230 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_230 = true;

	private uScript_RemoveEncounterTimer logic_uScript_RemoveEncounterTimer_uScript_RemoveEncounterTimer_231 = new uScript_RemoveEncounterTimer();

	private GameObject logic_uScript_RemoveEncounterTimer_owner_231;

	private bool logic_uScript_RemoveEncounterTimer_Out_231 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_233 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_233;

	private bool logic_uScriptCon_CompareBool_True_233 = true;

	private bool logic_uScriptCon_CompareBool_False_233 = true;

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
		if (null == owner_Connection_30 || !m_RegisteredForEvents)
		{
			owner_Connection_30 = parentGameObject;
		}
		if (null == owner_Connection_44 || !m_RegisteredForEvents)
		{
			owner_Connection_44 = parentGameObject;
		}
		if (null == owner_Connection_70 || !m_RegisteredForEvents)
		{
			owner_Connection_70 = parentGameObject;
		}
		if (null == owner_Connection_102 || !m_RegisteredForEvents)
		{
			owner_Connection_102 = parentGameObject;
		}
		if (null == owner_Connection_103 || !m_RegisteredForEvents)
		{
			owner_Connection_103 = parentGameObject;
		}
		if (null == owner_Connection_106 || !m_RegisteredForEvents)
		{
			owner_Connection_106 = parentGameObject;
		}
		if (null == owner_Connection_107 || !m_RegisteredForEvents)
		{
			owner_Connection_107 = parentGameObject;
		}
		if (null == owner_Connection_115 || !m_RegisteredForEvents)
		{
			owner_Connection_115 = parentGameObject;
		}
		if (null == owner_Connection_120 || !m_RegisteredForEvents)
		{
			owner_Connection_120 = parentGameObject;
		}
		if (null == owner_Connection_124 || !m_RegisteredForEvents)
		{
			owner_Connection_124 = parentGameObject;
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
		if (null == owner_Connection_148 || !m_RegisteredForEvents)
		{
			owner_Connection_148 = parentGameObject;
		}
		if (null == owner_Connection_170 || !m_RegisteredForEvents)
		{
			owner_Connection_170 = parentGameObject;
		}
		if (null == owner_Connection_174 || !m_RegisteredForEvents)
		{
			owner_Connection_174 = parentGameObject;
		}
		if (null == owner_Connection_179 || !m_RegisteredForEvents)
		{
			owner_Connection_179 = parentGameObject;
		}
		if (null == owner_Connection_232 || !m_RegisteredForEvents)
		{
			owner_Connection_232 = parentGameObject;
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
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_4.SetParent(g);
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_6.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_8.SetParent(g);
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_9.SetParent(g);
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_11.SetParent(g);
		logic_uScript_PausePopulation_uScript_PausePopulation_12.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_14.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_19.SetParent(g);
		logic_uScript_PausePopulation_uScript_PausePopulation_21.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_23.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_26.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_27.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_29.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_32.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_35.SetParent(g);
		logic_uScript_RestrictItemPickup_uScript_RestrictItemPickup_36.SetParent(g);
		logic_uScript_GetTankBlock_uScript_GetTankBlock_41.SetParent(g);
		logic_uScript_FinishEncounter_uScript_FinishEncounter_43.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_47.SetParent(g);
		logic_uScript_AI_SetBehaviourType_uScript_AI_SetBehaviourType_48.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_52.SetParent(g);
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_53.SetParent(g);
		logic_uScript_SetTankTeam_uScript_SetTankTeam_55.SetParent(g);
		logic_uScript_SetTankTeam_uScript_SetTankTeam_58.SetParent(g);
		logic_uScript_SetCustomRadarTeamID_uScript_SetCustomRadarTeamID_59.SetParent(g);
		logic_uScript_RemoveTech_uScript_RemoveTech_61.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_65.SetParent(g);
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_66.SetParent(g);
		logic_uScript_SetQuestObjectiveVisible_uScript_SetQuestObjectiveVisible_69.SetParent(g);
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_71.SetParent(g);
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_73.SetParent(g);
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_75.SetParent(g);
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_77.SetParent(g);
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_79.SetParent(g);
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_81.SetParent(g);
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_83.SetParent(g);
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_84.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_87.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_88.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_89.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_90.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_91.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_92.SetParent(g);
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_93.SetParent(g);
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_95.SetParent(g);
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_99.SetParent(g);
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_100.SetParent(g);
		logic_uScript_GetQuestObjectiveCompleted_uScript_GetQuestObjectiveCompleted_101.SetParent(g);
		logic_uScript_GetQuestObjectiveCompleted_uScript_GetQuestObjectiveCompleted_104.SetParent(g);
		logic_uScript_GetQuestObjectiveCompleted_uScript_GetQuestObjectiveCompleted_105.SetParent(g);
		logic_uScript_GetQuestObjectiveCompleted_uScript_GetQuestObjectiveCompleted_108.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_109.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_110.SetParent(g);
		logic_uScript_SetQuestObjectiveVisible_uScript_SetQuestObjectiveVisible_113.SetParent(g);
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_114.SetParent(g);
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_117.SetParent(g);
		logic_uScript_SetQuestObjectiveVisible_uScript_SetQuestObjectiveVisible_121.SetParent(g);
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_122.SetParent(g);
		logic_uScript_SetQuestObjectiveVisible_uScript_SetQuestObjectiveVisible_125.SetParent(g);
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_126.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_131.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_134.SetParent(g);
		logic_uScript_SetCustomRadarTeamID_uScript_SetCustomRadarTeamID_135.SetParent(g);
		logic_uScript_RemoveScenery_uScript_RemoveScenery_136.SetParent(g);
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_137.SetParent(g);
		logic_uScript_LockTech_uScript_LockTech_141.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_143.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_144.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_146.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_147.SetParent(g);
		logic_uScript_LockTechInteraction_uScript_LockTechInteraction_149.SetParent(g);
		logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_151.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_152.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_153.SetParent(g);
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_161.SetParent(g);
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_163.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_165.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_167.SetParent(g);
		logic_uScript_StartEncounterTimer_uScript_StartEncounterTimer_169.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_171.SetParent(g);
		logic_uScriptCon_CompareFloat_uScriptCon_CompareFloat_173.SetParent(g);
		logic_uScript_GetEncounterTimeRemaining_uScript_GetEncounterTimeRemaining_175.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_177.SetParent(g);
		logic_uScript_FinishEncounter_uScript_FinishEncounter_178.SetParent(g);
		logic_uScript_GetRandomChunkType_uScript_GetRandomChunkType_182.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_183.SetParent(g);
		logic_uScript_RestrictItemPickup_uScript_RestrictItemPickup_186.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_190.SetParent(g);
		logic_SubGraph_Harvesting_Mission_ResourceTracker_SubGraph_Harvesting_Mission_ResourceTracker_191.SetParent(g);
		logic_uScriptAct_Log_uScriptAct_Log_194.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_195.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_197.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_198.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_200.SetParent(g);
		logic_uScript_AccessListChunk_uScript_AccessListChunk_201.SetParent(g);
		logic_SubGraph_Harvesting_Mission_ResourceTracker_SubGraph_Harvesting_Mission_ResourceTracker_204.SetParent(g);
		logic_uScript_AccessListChunk_uScript_AccessListChunk_207.SetParent(g);
		logic_SubGraph_Harvesting_Mission_ResourceTracker_SubGraph_Harvesting_Mission_ResourceTracker_211.SetParent(g);
		logic_uScript_AccessListChunk_uScript_AccessListChunk_214.SetParent(g);
		logic_SubGraph_Harvesting_Mission_ResourceTracker_SubGraph_Harvesting_Mission_ResourceTracker_216.SetParent(g);
		logic_uScript_AccessListChunk_uScript_AccessListChunk_219.SetParent(g);
		logic_SubGraph_Harvesting_Mission_ResourceTracker_SubGraph_Harvesting_Mission_ResourceTracker_222.SetParent(g);
		logic_uScript_GetChunkListLength_uScript_GetChunkListLength_225.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_228.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_230.SetParent(g);
		logic_uScript_RemoveEncounterTimer_uScript_RemoveEncounterTimer_231.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_233.SetParent(g);
		owner_Connection_1 = parentGameObject;
		owner_Connection_3 = parentGameObject;
		owner_Connection_30 = parentGameObject;
		owner_Connection_44 = parentGameObject;
		owner_Connection_70 = parentGameObject;
		owner_Connection_102 = parentGameObject;
		owner_Connection_103 = parentGameObject;
		owner_Connection_106 = parentGameObject;
		owner_Connection_107 = parentGameObject;
		owner_Connection_115 = parentGameObject;
		owner_Connection_120 = parentGameObject;
		owner_Connection_124 = parentGameObject;
		owner_Connection_139 = parentGameObject;
		owner_Connection_142 = parentGameObject;
		owner_Connection_145 = parentGameObject;
		owner_Connection_148 = parentGameObject;
		owner_Connection_170 = parentGameObject;
		owner_Connection_174 = parentGameObject;
		owner_Connection_179 = parentGameObject;
		owner_Connection_232 = parentGameObject;
	}

	public void Awake()
	{
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_35.Awake();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_53.Awake();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_66.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_131.Awake();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_161.Awake();
		logic_SubGraph_Harvesting_Mission_ResourceTracker_SubGraph_Harvesting_Mission_ResourceTracker_191.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_198.Awake();
		logic_SubGraph_Harvesting_Mission_ResourceTracker_SubGraph_Harvesting_Mission_ResourceTracker_204.Awake();
		logic_SubGraph_Harvesting_Mission_ResourceTracker_SubGraph_Harvesting_Mission_ResourceTracker_211.Awake();
		logic_SubGraph_Harvesting_Mission_ResourceTracker_SubGraph_Harvesting_Mission_ResourceTracker_216.Awake();
		logic_SubGraph_Harvesting_Mission_ResourceTracker_SubGraph_Harvesting_Mission_ResourceTracker_222.Awake();
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_4.Output1 += uScriptCon_ManualSwitch_Output1_4;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_4.Output2 += uScriptCon_ManualSwitch_Output2_4;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_4.Output3 += uScriptCon_ManualSwitch_Output3_4;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_4.Output4 += uScriptCon_ManualSwitch_Output4_4;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_4.Output5 += uScriptCon_ManualSwitch_Output5_4;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_4.Output6 += uScriptCon_ManualSwitch_Output6_4;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_4.Output7 += uScriptCon_ManualSwitch_Output7_4;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_4.Output8 += uScriptCon_ManualSwitch_Output8_4;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_35.Save_Out += SubGraph_SaveLoadBool_Save_Out_35;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_35.Load_Out += SubGraph_SaveLoadBool_Load_Out_35;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_35.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_35;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_53.Out += SubGraph_CompleteObjectiveStage_Out_53;
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_66.Out += SubGraph_LoadObjectiveStates_Out_66;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_131.Save_Out += SubGraph_SaveLoadBool_Save_Out_131;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_131.Load_Out += SubGraph_SaveLoadBool_Load_Out_131;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_131.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_131;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_161.Save_Out += SubGraph_SaveLoadInt_Save_Out_161;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_161.Load_Out += SubGraph_SaveLoadInt_Load_Out_161;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_161.Restart_Out += SubGraph_SaveLoadInt_Restart_Out_161;
		logic_SubGraph_Harvesting_Mission_ResourceTracker_SubGraph_Harvesting_Mission_ResourceTracker_191.Out += SubGraph_Harvesting_Mission_ResourceTracker_Out_191;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_198.Save_Out += SubGraph_SaveLoadBool_Save_Out_198;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_198.Load_Out += SubGraph_SaveLoadBool_Load_Out_198;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_198.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_198;
		logic_SubGraph_Harvesting_Mission_ResourceTracker_SubGraph_Harvesting_Mission_ResourceTracker_204.Out += SubGraph_Harvesting_Mission_ResourceTracker_Out_204;
		logic_SubGraph_Harvesting_Mission_ResourceTracker_SubGraph_Harvesting_Mission_ResourceTracker_211.Out += SubGraph_Harvesting_Mission_ResourceTracker_Out_211;
		logic_SubGraph_Harvesting_Mission_ResourceTracker_SubGraph_Harvesting_Mission_ResourceTracker_216.Out += SubGraph_Harvesting_Mission_ResourceTracker_Out_216;
		logic_SubGraph_Harvesting_Mission_ResourceTracker_SubGraph_Harvesting_Mission_ResourceTracker_222.Out += SubGraph_Harvesting_Mission_ResourceTracker_Out_222;
	}

	public void Start()
	{
		SyncUnityHooks();
		m_RegisteredForEvents = true;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_35.Start();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_53.Start();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_66.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_131.Start();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_161.Start();
		logic_SubGraph_Harvesting_Mission_ResourceTracker_SubGraph_Harvesting_Mission_ResourceTracker_191.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_198.Start();
		logic_SubGraph_Harvesting_Mission_ResourceTracker_SubGraph_Harvesting_Mission_ResourceTracker_204.Start();
		logic_SubGraph_Harvesting_Mission_ResourceTracker_SubGraph_Harvesting_Mission_ResourceTracker_211.Start();
		logic_SubGraph_Harvesting_Mission_ResourceTracker_SubGraph_Harvesting_Mission_ResourceTracker_216.Start();
		logic_SubGraph_Harvesting_Mission_ResourceTracker_SubGraph_Harvesting_Mission_ResourceTracker_222.Start();
	}

	public void OnEnable()
	{
		RegisterForUnityHooks();
		m_RegisteredForEvents = true;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_35.OnEnable();
		logic_uScript_AI_SetBehaviourType_uScript_AI_SetBehaviourType_48.OnEnable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_53.OnEnable();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_66.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_131.OnEnable();
		logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_151.OnEnable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_161.OnEnable();
		logic_SubGraph_Harvesting_Mission_ResourceTracker_SubGraph_Harvesting_Mission_ResourceTracker_191.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_198.OnEnable();
		logic_SubGraph_Harvesting_Mission_ResourceTracker_SubGraph_Harvesting_Mission_ResourceTracker_204.OnEnable();
		logic_SubGraph_Harvesting_Mission_ResourceTracker_SubGraph_Harvesting_Mission_ResourceTracker_211.OnEnable();
		logic_SubGraph_Harvesting_Mission_ResourceTracker_SubGraph_Harvesting_Mission_ResourceTracker_216.OnEnable();
		logic_SubGraph_Harvesting_Mission_ResourceTracker_SubGraph_Harvesting_Mission_ResourceTracker_222.OnEnable();
	}

	public void OnDisable()
	{
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_6.OnDisable();
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_9.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_23.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_27.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_35.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_47.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_52.OnDisable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_53.OnDisable();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_66.OnDisable();
		logic_uScript_GetQuestObjectiveCompleted_uScript_GetQuestObjectiveCompleted_101.OnDisable();
		logic_uScript_GetQuestObjectiveCompleted_uScript_GetQuestObjectiveCompleted_104.OnDisable();
		logic_uScript_GetQuestObjectiveCompleted_uScript_GetQuestObjectiveCompleted_105.OnDisable();
		logic_uScript_GetQuestObjectiveCompleted_uScript_GetQuestObjectiveCompleted_108.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_131.OnDisable();
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_137.OnDisable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_161.OnDisable();
		logic_SubGraph_Harvesting_Mission_ResourceTracker_SubGraph_Harvesting_Mission_ResourceTracker_191.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_198.OnDisable();
		logic_SubGraph_Harvesting_Mission_ResourceTracker_SubGraph_Harvesting_Mission_ResourceTracker_204.OnDisable();
		logic_SubGraph_Harvesting_Mission_ResourceTracker_SubGraph_Harvesting_Mission_ResourceTracker_211.OnDisable();
		logic_SubGraph_Harvesting_Mission_ResourceTracker_SubGraph_Harvesting_Mission_ResourceTracker_216.OnDisable();
		logic_SubGraph_Harvesting_Mission_ResourceTracker_SubGraph_Harvesting_Mission_ResourceTracker_222.OnDisable();
		UnregisterEventListeners();
		m_RegisteredForEvents = false;
	}

	public void Update()
	{
		SyncEventListeners();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_35.Update();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_53.Update();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_66.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_131.Update();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_161.Update();
		logic_SubGraph_Harvesting_Mission_ResourceTracker_SubGraph_Harvesting_Mission_ResourceTracker_191.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_198.Update();
		logic_SubGraph_Harvesting_Mission_ResourceTracker_SubGraph_Harvesting_Mission_ResourceTracker_204.Update();
		logic_SubGraph_Harvesting_Mission_ResourceTracker_SubGraph_Harvesting_Mission_ResourceTracker_211.Update();
		logic_SubGraph_Harvesting_Mission_ResourceTracker_SubGraph_Harvesting_Mission_ResourceTracker_216.Update();
		logic_SubGraph_Harvesting_Mission_ResourceTracker_SubGraph_Harvesting_Mission_ResourceTracker_222.Update();
	}

	public void OnDestroy()
	{
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_35.OnDestroy();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_53.OnDestroy();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_66.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_131.OnDestroy();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_161.OnDestroy();
		logic_SubGraph_Harvesting_Mission_ResourceTracker_SubGraph_Harvesting_Mission_ResourceTracker_191.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_198.OnDestroy();
		logic_SubGraph_Harvesting_Mission_ResourceTracker_SubGraph_Harvesting_Mission_ResourceTracker_204.OnDestroy();
		logic_SubGraph_Harvesting_Mission_ResourceTracker_SubGraph_Harvesting_Mission_ResourceTracker_211.OnDestroy();
		logic_SubGraph_Harvesting_Mission_ResourceTracker_SubGraph_Harvesting_Mission_ResourceTracker_216.OnDestroy();
		logic_SubGraph_Harvesting_Mission_ResourceTracker_SubGraph_Harvesting_Mission_ResourceTracker_222.OnDestroy();
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_4.Output1 -= uScriptCon_ManualSwitch_Output1_4;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_4.Output2 -= uScriptCon_ManualSwitch_Output2_4;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_4.Output3 -= uScriptCon_ManualSwitch_Output3_4;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_4.Output4 -= uScriptCon_ManualSwitch_Output4_4;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_4.Output5 -= uScriptCon_ManualSwitch_Output5_4;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_4.Output6 -= uScriptCon_ManualSwitch_Output6_4;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_4.Output7 -= uScriptCon_ManualSwitch_Output7_4;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_4.Output8 -= uScriptCon_ManualSwitch_Output8_4;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_35.Save_Out -= SubGraph_SaveLoadBool_Save_Out_35;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_35.Load_Out -= SubGraph_SaveLoadBool_Load_Out_35;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_35.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_35;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_53.Out -= SubGraph_CompleteObjectiveStage_Out_53;
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_66.Out -= SubGraph_LoadObjectiveStates_Out_66;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_131.Save_Out -= SubGraph_SaveLoadBool_Save_Out_131;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_131.Load_Out -= SubGraph_SaveLoadBool_Load_Out_131;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_131.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_131;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_161.Save_Out -= SubGraph_SaveLoadInt_Save_Out_161;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_161.Load_Out -= SubGraph_SaveLoadInt_Load_Out_161;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_161.Restart_Out -= SubGraph_SaveLoadInt_Restart_Out_161;
		logic_SubGraph_Harvesting_Mission_ResourceTracker_SubGraph_Harvesting_Mission_ResourceTracker_191.Out -= SubGraph_Harvesting_Mission_ResourceTracker_Out_191;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_198.Save_Out -= SubGraph_SaveLoadBool_Save_Out_198;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_198.Load_Out -= SubGraph_SaveLoadBool_Load_Out_198;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_198.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_198;
		logic_SubGraph_Harvesting_Mission_ResourceTracker_SubGraph_Harvesting_Mission_ResourceTracker_204.Out -= SubGraph_Harvesting_Mission_ResourceTracker_Out_204;
		logic_SubGraph_Harvesting_Mission_ResourceTracker_SubGraph_Harvesting_Mission_ResourceTracker_211.Out -= SubGraph_Harvesting_Mission_ResourceTracker_Out_211;
		logic_SubGraph_Harvesting_Mission_ResourceTracker_SubGraph_Harvesting_Mission_ResourceTracker_216.Out -= SubGraph_Harvesting_Mission_ResourceTracker_Out_216;
		logic_SubGraph_Harvesting_Mission_ResourceTracker_SubGraph_Harvesting_Mission_ResourceTracker_222.Out -= SubGraph_Harvesting_Mission_ResourceTracker_Out_222;
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

	private void uScriptCon_ManualSwitch_Output1_4(object o, EventArgs e)
	{
		Relay_Output1_4();
	}

	private void uScriptCon_ManualSwitch_Output2_4(object o, EventArgs e)
	{
		Relay_Output2_4();
	}

	private void uScriptCon_ManualSwitch_Output3_4(object o, EventArgs e)
	{
		Relay_Output3_4();
	}

	private void uScriptCon_ManualSwitch_Output4_4(object o, EventArgs e)
	{
		Relay_Output4_4();
	}

	private void uScriptCon_ManualSwitch_Output5_4(object o, EventArgs e)
	{
		Relay_Output5_4();
	}

	private void uScriptCon_ManualSwitch_Output6_4(object o, EventArgs e)
	{
		Relay_Output6_4();
	}

	private void uScriptCon_ManualSwitch_Output7_4(object o, EventArgs e)
	{
		Relay_Output7_4();
	}

	private void uScriptCon_ManualSwitch_Output8_4(object o, EventArgs e)
	{
		Relay_Output8_4();
	}

	private void SubGraph_SaveLoadBool_Save_Out_35(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_35 = e.boolean;
		local_msgIntroShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_35;
		Relay_Save_Out_35();
	}

	private void SubGraph_SaveLoadBool_Load_Out_35(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_35 = e.boolean;
		local_msgIntroShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_35;
		Relay_Load_Out_35();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_35(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_35 = e.boolean;
		local_msgIntroShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_35;
		Relay_Restart_Out_35();
	}

	private void SubGraph_CompleteObjectiveStage_Out_53(object o, SubGraph_CompleteObjectiveStage.LogicEventArgs e)
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_53 = e.objectiveStage;
		local_Stage_System_Int32 = logic_SubGraph_CompleteObjectiveStage_objectiveStage_53;
		Relay_Out_53();
	}

	private void SubGraph_LoadObjectiveStates_Out_66(object o, SubGraph_LoadObjectiveStates.LogicEventArgs e)
	{
		Relay_Out_66();
	}

	private void SubGraph_SaveLoadBool_Save_Out_131(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_131 = e.boolean;
		local_Init_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_131;
		Relay_Save_Out_131();
	}

	private void SubGraph_SaveLoadBool_Load_Out_131(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_131 = e.boolean;
		local_Init_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_131;
		Relay_Load_Out_131();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_131(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_131 = e.boolean;
		local_Init_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_131;
		Relay_Restart_Out_131();
	}

	private void SubGraph_SaveLoadInt_Save_Out_161(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_161 = e.integer;
		local_Stage_System_Int32 = logic_SubGraph_SaveLoadInt_integer_161;
		Relay_Save_Out_161();
	}

	private void SubGraph_SaveLoadInt_Load_Out_161(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_161 = e.integer;
		local_Stage_System_Int32 = logic_SubGraph_SaveLoadInt_integer_161;
		Relay_Load_Out_161();
	}

	private void SubGraph_SaveLoadInt_Restart_Out_161(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_161 = e.integer;
		local_Stage_System_Int32 = logic_SubGraph_SaveLoadInt_integer_161;
		Relay_Restart_Out_161();
	}

	private void SubGraph_Harvesting_Mission_ResourceTracker_Out_191(object o, SubGraph_Harvesting_Mission_ResourceTracker.LogicEventArgs e)
	{
		Relay_Out_191();
	}

	private void SubGraph_SaveLoadBool_Save_Out_198(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_198 = e.boolean;
		local_RandomResourceTypeSet_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_198;
		Relay_Save_Out_198();
	}

	private void SubGraph_SaveLoadBool_Load_Out_198(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_198 = e.boolean;
		local_RandomResourceTypeSet_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_198;
		Relay_Load_Out_198();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_198(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_198 = e.boolean;
		local_RandomResourceTypeSet_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_198;
		Relay_Restart_Out_198();
	}

	private void SubGraph_Harvesting_Mission_ResourceTracker_Out_204(object o, SubGraph_Harvesting_Mission_ResourceTracker.LogicEventArgs e)
	{
		Relay_Out_204();
	}

	private void SubGraph_Harvesting_Mission_ResourceTracker_Out_211(object o, SubGraph_Harvesting_Mission_ResourceTracker.LogicEventArgs e)
	{
		Relay_Out_211();
	}

	private void SubGraph_Harvesting_Mission_ResourceTracker_Out_216(object o, SubGraph_Harvesting_Mission_ResourceTracker.LogicEventArgs e)
	{
		Relay_Out_216();
	}

	private void SubGraph_Harvesting_Mission_ResourceTracker_Out_222(object o, SubGraph_Harvesting_Mission_ResourceTracker.LogicEventArgs e)
	{
		Relay_Out_222();
	}

	private void Relay_SaveEvent_0()
	{
		Relay_Save_161();
	}

	private void Relay_LoadEvent_0()
	{
		Relay_Load_161();
	}

	private void Relay_RestartEvent_0()
	{
		Relay_Restart_161();
	}

	private void Relay_OnUpdate_2()
	{
		Relay_In_143();
	}

	private void Relay_OnSuspend_2()
	{
	}

	private void Relay_OnResume_2()
	{
	}

	private void Relay_Output1_4()
	{
		Relay_In_52();
	}

	private void Relay_Output2_4()
	{
		Relay_In_165();
	}

	private void Relay_Output3_4()
	{
		Relay_In_71();
	}

	private void Relay_Output4_4()
	{
		Relay_In_73();
	}

	private void Relay_Output5_4()
	{
		Relay_In_75();
	}

	private void Relay_Output6_4()
	{
		Relay_In_77();
	}

	private void Relay_Output7_4()
	{
	}

	private void Relay_Output8_4()
	{
	}

	private void Relay_In_4()
	{
		logic_uScriptCon_ManualSwitch_CurrentOutput_4 = local_Stage_System_Int32;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_4.In(logic_uScriptCon_ManualSwitch_CurrentOutput_4);
	}

	private void Relay_In_6()
	{
		logic_uScript_IsPlayerInRangeOfTech_tech_6 = local_NPCTech_Tank;
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_6.In(logic_uScript_IsPlayerInRangeOfTech_tech_6, logic_uScript_IsPlayerInRangeOfTech_range_6, logic_uScript_IsPlayerInRangeOfTech_techs_6);
		if (logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_6.InRange)
		{
			Relay_In_27();
		}
	}

	private void Relay_True_8()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_8.True(out logic_uScriptAct_SetBool_Target_8);
		local_NearNPC_System_Boolean = logic_uScriptAct_SetBool_Target_8;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_8.Out)
		{
			Relay_In_4();
		}
	}

	private void Relay_False_8()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_8.False(out logic_uScriptAct_SetBool_Target_8);
		local_NearNPC_System_Boolean = logic_uScriptAct_SetBool_Target_8;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_8.Out)
		{
			Relay_In_4();
		}
	}

	private void Relay_In_9()
	{
		logic_uScript_IsPlayerInRangeOfTech_tech_9 = local_NPCTech_Tank;
		logic_uScript_IsPlayerInRangeOfTech_range_9 = distNPCFound;
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_9.In(logic_uScript_IsPlayerInRangeOfTech_tech_9, logic_uScript_IsPlayerInRangeOfTech_range_9, logic_uScript_IsPlayerInRangeOfTech_techs_9);
		bool inRange = logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_9.InRange;
		bool outOfRange = logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_9.OutOfRange;
		if (inRange)
		{
			Relay_Pause_12();
		}
		if (outOfRange)
		{
			Relay_UnPause_21();
		}
	}

	private void Relay_In_11()
	{
		logic_uScript_SetEncounterTarget_owner_11 = owner_Connection_30;
		logic_uScript_SetEncounterTarget_visibleObject_11 = local_NPCTech_Tank;
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_11.In(logic_uScript_SetEncounterTarget_owner_11, logic_uScript_SetEncounterTarget_visibleObject_11);
		if (logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_11.Out)
		{
			Relay_In_183();
		}
	}

	private void Relay_Pause_12()
	{
		logic_uScript_PausePopulation_uScript_PausePopulation_12.Pause();
		if (logic_uScript_PausePopulation_uScript_PausePopulation_12.Out)
		{
			Relay_True_8();
		}
	}

	private void Relay_UnPause_12()
	{
		logic_uScript_PausePopulation_uScript_PausePopulation_12.UnPause();
		if (logic_uScript_PausePopulation_uScript_PausePopulation_12.Out)
		{
			Relay_True_8();
		}
	}

	private void Relay_In_14()
	{
		logic_uScriptCon_CompareBool_Bool_14 = local_msgIntroShown_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_14.In(logic_uScriptCon_CompareBool_Bool_14);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_14.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_14.False;
		if (num)
		{
			Relay_In_9();
		}
		if (flag)
		{
			Relay_In_6();
		}
	}

	private void Relay_In_19()
	{
		logic_uScriptCon_CompareBool_Bool_19 = local_NearNPC_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_19.In(logic_uScriptCon_CompareBool_Bool_19);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_19.True)
		{
			Relay_In_23();
		}
	}

	private void Relay_Pause_21()
	{
		logic_uScript_PausePopulation_uScript_PausePopulation_21.Pause();
		if (logic_uScript_PausePopulation_uScript_PausePopulation_21.Out)
		{
			Relay_In_32();
		}
	}

	private void Relay_UnPause_21()
	{
		logic_uScript_PausePopulation_uScript_PausePopulation_21.UnPause();
		if (logic_uScript_PausePopulation_uScript_PausePopulation_21.Out)
		{
			Relay_In_32();
		}
	}

	private void Relay_In_23()
	{
		logic_uScript_AddMessage_messageData_23 = msgLeavingMissionArea;
		logic_uScript_AddMessage_speaker_23 = messageSpeaker;
		logic_uScript_AddMessage_Return_23 = logic_uScript_AddMessage_uScript_AddMessage_23.In(logic_uScript_AddMessage_messageData_23, logic_uScript_AddMessage_speaker_23);
		if (logic_uScript_AddMessage_uScript_AddMessage_23.Out)
		{
			Relay_False_26();
		}
	}

	private void Relay_True_26()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_26.True(out logic_uScriptAct_SetBool_Target_26);
		local_NearNPC_System_Boolean = logic_uScriptAct_SetBool_Target_26;
	}

	private void Relay_False_26()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_26.False(out logic_uScriptAct_SetBool_Target_26);
		local_NearNPC_System_Boolean = logic_uScriptAct_SetBool_Target_26;
	}

	private void Relay_In_27()
	{
		logic_uScript_AddMessage_messageData_27 = msg01Intro;
		logic_uScript_AddMessage_speaker_27 = messageSpeaker;
		logic_uScript_AddMessage_Return_27 = logic_uScript_AddMessage_uScript_AddMessage_27.In(logic_uScript_AddMessage_messageData_27, logic_uScript_AddMessage_speaker_27);
		if (logic_uScript_AddMessage_uScript_AddMessage_27.Out)
		{
			Relay_True_29();
		}
	}

	private void Relay_True_29()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_29.True(out logic_uScriptAct_SetBool_Target_29);
		local_msgIntroShown_System_Boolean = logic_uScriptAct_SetBool_Target_29;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_29.Out)
		{
			Relay_In_9();
		}
	}

	private void Relay_False_29()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_29.False(out logic_uScriptAct_SetBool_Target_29);
		local_msgIntroShown_System_Boolean = logic_uScriptAct_SetBool_Target_29;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_29.Out)
		{
			Relay_In_9();
		}
	}

	private void Relay_In_32()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_32 = messageTag;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_32.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_32, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_32);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_32.Out)
		{
			Relay_In_163();
		}
	}

	private void Relay_Save_Out_35()
	{
	}

	private void Relay_Load_Out_35()
	{
		Relay_In_66();
	}

	private void Relay_Restart_Out_35()
	{
		Relay_False_65();
	}

	private void Relay_Save_35()
	{
		logic_SubGraph_SaveLoadBool_boolean_35 = local_msgIntroShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_35 = local_msgIntroShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_35.Save(ref logic_SubGraph_SaveLoadBool_boolean_35, logic_SubGraph_SaveLoadBool_boolAsVariable_35, logic_SubGraph_SaveLoadBool_uniqueID_35);
	}

	private void Relay_Load_35()
	{
		logic_SubGraph_SaveLoadBool_boolean_35 = local_msgIntroShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_35 = local_msgIntroShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_35.Load(ref logic_SubGraph_SaveLoadBool_boolean_35, logic_SubGraph_SaveLoadBool_boolAsVariable_35, logic_SubGraph_SaveLoadBool_uniqueID_35);
	}

	private void Relay_Set_True_35()
	{
		logic_SubGraph_SaveLoadBool_boolean_35 = local_msgIntroShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_35 = local_msgIntroShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_35.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_35, logic_SubGraph_SaveLoadBool_boolAsVariable_35, logic_SubGraph_SaveLoadBool_uniqueID_35);
	}

	private void Relay_Set_False_35()
	{
		logic_SubGraph_SaveLoadBool_boolean_35 = local_msgIntroShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_35 = local_msgIntroShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_35.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_35, logic_SubGraph_SaveLoadBool_boolAsVariable_35, logic_SubGraph_SaveLoadBool_uniqueID_35);
	}

	private void Relay_In_36()
	{
		logic_uScript_RestrictItemPickup_tech_36 = local_NPCTech_Tank;
		int num = 0;
		Array array = resourceTypesAccepted;
		if (logic_uScript_RestrictItemPickup_typesToAccept_36.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_RestrictItemPickup_typesToAccept_36, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_RestrictItemPickup_typesToAccept_36, num, array.Length);
		num += array.Length;
		logic_uScript_RestrictItemPickup_uScript_RestrictItemPickup_36.In(logic_uScript_RestrictItemPickup_tech_36, logic_uScript_RestrictItemPickup_typesToAccept_36);
		if (logic_uScript_RestrictItemPickup_uScript_RestrictItemPickup_36.Out)
		{
			Relay_In_225();
		}
	}

	private void Relay_In_41()
	{
		logic_uScript_GetTankBlock_tank_41 = local_NPCTech_Tank;
		logic_uScript_GetTankBlock_blockType_41 = blockTypeSilo;
		logic_uScript_GetTankBlock_Return_41 = logic_uScript_GetTankBlock_uScript_GetTankBlock_41.In(logic_uScript_GetTankBlock_tank_41, logic_uScript_GetTankBlock_blockType_41);
		local_SiloBlock_TankBlock = logic_uScript_GetTankBlock_Return_41;
		if (logic_uScript_GetTankBlock_uScript_GetTankBlock_41.Returned)
		{
			Relay_In_79();
		}
	}

	private void Relay_Succeed_43()
	{
		logic_uScript_FinishEncounter_owner_43 = owner_Connection_44;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_43.Succeed(logic_uScript_FinishEncounter_owner_43);
	}

	private void Relay_Fail_43()
	{
		logic_uScript_FinishEncounter_owner_43 = owner_Connection_44;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_43.Fail(logic_uScript_FinishEncounter_owner_43);
	}

	private void Relay_In_47()
	{
		logic_uScript_AddMessage_messageData_47 = msg03Complete;
		logic_uScript_AddMessage_speaker_47 = messageSpeaker;
		logic_uScript_AddMessage_Return_47 = logic_uScript_AddMessage_uScript_AddMessage_47.In(logic_uScript_AddMessage_messageData_47, logic_uScript_AddMessage_speaker_47);
		if (logic_uScript_AddMessage_uScript_AddMessage_47.Shown)
		{
			Relay_In_61();
		}
	}

	private void Relay_In_48()
	{
		logic_uScript_AI_SetBehaviourType_tank_48 = local_NPCTech_Tank;
		logic_uScript_AI_SetBehaviourType_uScript_AI_SetBehaviourType_48.In(logic_uScript_AI_SetBehaviourType_tank_48, logic_uScript_AI_SetBehaviourType_aiType_48);
		if (logic_uScript_AI_SetBehaviourType_uScript_AI_SetBehaviourType_48.Out)
		{
			Relay_In_55();
		}
	}

	private void Relay_In_52()
	{
		logic_uScript_AddMessage_messageData_52 = msg02NPCFound;
		logic_uScript_AddMessage_speaker_52 = messageSpeaker;
		logic_uScript_AddMessage_Return_52 = logic_uScript_AddMessage_uScript_AddMessage_52.In(logic_uScript_AddMessage_messageData_52, logic_uScript_AddMessage_speaker_52);
		if (logic_uScript_AddMessage_uScript_AddMessage_52.Shown)
		{
			Relay_In_53();
		}
	}

	private void Relay_Out_53()
	{
		Relay_In_167();
	}

	private void Relay_In_53()
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_53 = local_Stage_System_Int32;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_53.In(ref logic_SubGraph_CompleteObjectiveStage_objectiveStage_53, logic_SubGraph_CompleteObjectiveStage_isFinalObjective_53);
	}

	private void Relay_In_55()
	{
		logic_uScript_SetTankTeam_tank_55 = local_NPCTech_Tank;
		logic_uScript_SetTankTeam_uScript_SetTankTeam_55.In(logic_uScript_SetTankTeam_tank_55, logic_uScript_SetTankTeam_team_55);
		if (logic_uScript_SetTankTeam_uScript_SetTankTeam_55.Out)
		{
			Relay_In_59();
		}
	}

	private void Relay_In_58()
	{
		logic_uScript_SetTankTeam_tank_58 = local_NPCTech_Tank;
		logic_uScript_SetTankTeam_uScript_SetTankTeam_58.In(logic_uScript_SetTankTeam_tank_58, logic_uScript_SetTankTeam_team_58);
		if (logic_uScript_SetTankTeam_uScript_SetTankTeam_58.Out)
		{
			Relay_In_47();
		}
	}

	private void Relay_In_59()
	{
		logic_uScript_SetCustomRadarTeamID_tech_59 = local_NPCTech_Tank;
		logic_uScript_SetCustomRadarTeamID_uScript_SetCustomRadarTeamID_59.In(logic_uScript_SetCustomRadarTeamID_tech_59, logic_uScript_SetCustomRadarTeamID_radarTeamID_59);
		if (logic_uScript_SetCustomRadarTeamID_uScript_SetCustomRadarTeamID_59.Out)
		{
			Relay_In_41();
		}
	}

	private void Relay_In_61()
	{
		logic_uScript_RemoveTech_tech_61 = local_NPCTech_Tank;
		logic_uScript_RemoveTech_uScript_RemoveTech_61.In(logic_uScript_RemoveTech_tech_61);
		if (logic_uScript_RemoveTech_uScript_RemoveTech_61.Out)
		{
			Relay_Succeed_43();
		}
	}

	private void Relay_True_65()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_65.True(out logic_uScriptAct_SetBool_Target_65);
		local_NearNPC_System_Boolean = logic_uScriptAct_SetBool_Target_65;
	}

	private void Relay_False_65()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_65.False(out logic_uScriptAct_SetBool_Target_65);
		local_NearNPC_System_Boolean = logic_uScriptAct_SetBool_Target_65;
	}

	private void Relay_Out_66()
	{
	}

	private void Relay_In_66()
	{
		logic_SubGraph_LoadObjectiveStates_currentObjective_66 = local_Stage_System_Int32;
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_66.In(logic_SubGraph_LoadObjectiveStates_currentObjective_66);
	}

	private void Relay_SetVisible_69()
	{
		logic_uScript_SetQuestObjectiveVisible_owner_69 = owner_Connection_70;
		logic_uScript_SetQuestObjectiveVisible_uScript_SetQuestObjectiveVisible_69.SetVisible(logic_uScript_SetQuestObjectiveVisible_owner_69, logic_uScript_SetQuestObjectiveVisible_objectiveId_69, logic_uScript_SetQuestObjectiveVisible_visible_69);
		if (logic_uScript_SetQuestObjectiveVisible_uScript_SetQuestObjectiveVisible_69.Out)
		{
			Relay_In_122();
		}
	}

	private void Relay_In_71()
	{
		logic_uScriptCon_CompareInt_A_71 = local_NumResourceTypes_System_Int32;
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_71.In(logic_uScriptCon_CompareInt_A_71, logic_uScriptCon_CompareInt_B_71);
		if (logic_uScriptCon_CompareInt_uScriptCon_CompareInt_71.EqualTo)
		{
			Relay_In_58();
		}
	}

	private void Relay_In_73()
	{
		logic_uScriptCon_CompareInt_A_73 = local_NumResourceTypes_System_Int32;
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_73.In(logic_uScriptCon_CompareInt_A_73, logic_uScriptCon_CompareInt_B_73);
		if (logic_uScriptCon_CompareInt_uScriptCon_CompareInt_73.EqualTo)
		{
			Relay_In_58();
		}
	}

	private void Relay_In_75()
	{
		logic_uScriptCon_CompareInt_A_75 = local_NumResourceTypes_System_Int32;
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_75.In(logic_uScriptCon_CompareInt_A_75, logic_uScriptCon_CompareInt_B_75);
		if (logic_uScriptCon_CompareInt_uScriptCon_CompareInt_75.EqualTo)
		{
			Relay_In_58();
		}
	}

	private void Relay_In_77()
	{
		logic_uScriptCon_CompareInt_A_77 = local_NumResourceTypes_System_Int32;
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_77.In(logic_uScriptCon_CompareInt_A_77, logic_uScriptCon_CompareInt_B_77);
		if (logic_uScriptCon_CompareInt_uScriptCon_CompareInt_77.EqualTo)
		{
			Relay_In_58();
		}
	}

	private void Relay_In_79()
	{
		logic_uScriptCon_CompareInt_A_79 = local_NumResourceTypes_System_Int32;
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_79.In(logic_uScriptCon_CompareInt_A_79, logic_uScriptCon_CompareInt_B_79);
		bool greaterThanOrEqualTo = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_79.GreaterThanOrEqualTo;
		bool lessThan = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_79.LessThan;
		if (greaterThanOrEqualTo)
		{
			Relay_In_190();
		}
		if (lessThan)
		{
			Relay_In_87();
		}
	}

	private void Relay_In_81()
	{
		logic_uScriptCon_CompareInt_A_81 = local_NumResourceTypes_System_Int32;
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_81.In(logic_uScriptCon_CompareInt_A_81, logic_uScriptCon_CompareInt_B_81);
		bool greaterThanOrEqualTo = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_81.GreaterThanOrEqualTo;
		bool lessThan = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_81.LessThan;
		if (greaterThanOrEqualTo)
		{
			Relay_AtIndex_207();
		}
		if (lessThan)
		{
			Relay_In_88();
		}
	}

	private void Relay_In_83()
	{
		logic_uScriptCon_CompareInt_A_83 = local_NumResourceTypes_System_Int32;
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_83.In(logic_uScriptCon_CompareInt_A_83, logic_uScriptCon_CompareInt_B_83);
		bool greaterThanOrEqualTo = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_83.GreaterThanOrEqualTo;
		bool lessThan = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_83.LessThan;
		if (greaterThanOrEqualTo)
		{
			Relay_AtIndex_214();
		}
		if (lessThan)
		{
			Relay_In_90();
		}
	}

	private void Relay_In_84()
	{
		logic_uScriptCon_CompareInt_A_84 = local_NumResourceTypes_System_Int32;
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_84.In(logic_uScriptCon_CompareInt_A_84, logic_uScriptCon_CompareInt_B_84);
		bool greaterThanOrEqualTo = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_84.GreaterThanOrEqualTo;
		bool lessThan = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_84.LessThan;
		if (greaterThanOrEqualTo)
		{
			Relay_AtIndex_219();
		}
		if (lessThan)
		{
			Relay_In_89();
		}
	}

	private void Relay_In_87()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_87.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_87.Out)
		{
			Relay_In_88();
		}
	}

	private void Relay_In_88()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_88.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_88.Out)
		{
			Relay_In_90();
		}
	}

	private void Relay_In_89()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_89.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_89.Out)
		{
			Relay_In_93();
		}
	}

	private void Relay_In_90()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_90.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_90.Out)
		{
			Relay_In_89();
		}
	}

	private void Relay_In_91()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_91.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_91.Out)
		{
			Relay_In_109();
		}
	}

	private void Relay_In_92()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_92.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_92.Out)
		{
			Relay_In_91();
		}
	}

	private void Relay_In_93()
	{
		logic_uScriptCon_CompareInt_A_93 = local_NumResourceTypes_System_Int32;
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_93.In(logic_uScriptCon_CompareInt_A_93, logic_uScriptCon_CompareInt_B_93);
		bool greaterThanOrEqualTo = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_93.GreaterThanOrEqualTo;
		bool lessThan = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_93.LessThan;
		if (greaterThanOrEqualTo)
		{
			Relay_In_101();
		}
		if (lessThan)
		{
			Relay_In_92();
		}
	}

	private void Relay_In_95()
	{
		logic_uScriptCon_CompareInt_A_95 = local_NumResourceTypes_System_Int32;
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_95.In(logic_uScriptCon_CompareInt_A_95, logic_uScriptCon_CompareInt_B_95);
		bool greaterThanOrEqualTo = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_95.GreaterThanOrEqualTo;
		bool lessThan = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_95.LessThan;
		if (greaterThanOrEqualTo)
		{
			Relay_In_104();
		}
		if (lessThan)
		{
			Relay_In_91();
		}
	}

	private void Relay_In_99()
	{
		logic_uScriptCon_CompareInt_A_99 = local_NumResourceTypes_System_Int32;
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_99.In(logic_uScriptCon_CompareInt_A_99, logic_uScriptCon_CompareInt_B_99);
		bool greaterThanOrEqualTo = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_99.GreaterThanOrEqualTo;
		bool lessThan = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_99.LessThan;
		if (greaterThanOrEqualTo)
		{
			Relay_In_108();
		}
		if (lessThan)
		{
			Relay_In_109();
		}
	}

	private void Relay_In_100()
	{
		logic_uScriptCon_CompareInt_A_100 = local_NumResourceTypes_System_Int32;
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_100.In(logic_uScriptCon_CompareInt_A_100, logic_uScriptCon_CompareInt_B_100);
		bool greaterThanOrEqualTo = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_100.GreaterThanOrEqualTo;
		bool lessThan = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_100.LessThan;
		if (greaterThanOrEqualTo)
		{
			Relay_In_105();
		}
		if (lessThan)
		{
			Relay_In_110();
		}
	}

	private void Relay_In_101()
	{
		logic_uScript_GetQuestObjectiveCompleted_owner_101 = owner_Connection_103;
		logic_uScript_GetQuestObjectiveCompleted_uScript_GetQuestObjectiveCompleted_101.In(logic_uScript_GetQuestObjectiveCompleted_owner_101, logic_uScript_GetQuestObjectiveCompleted_objectiveId_101);
		if (logic_uScript_GetQuestObjectiveCompleted_uScript_GetQuestObjectiveCompleted_101.Complete)
		{
			Relay_In_95();
		}
	}

	private void Relay_In_104()
	{
		logic_uScript_GetQuestObjectiveCompleted_owner_104 = owner_Connection_102;
		logic_uScript_GetQuestObjectiveCompleted_uScript_GetQuestObjectiveCompleted_104.In(logic_uScript_GetQuestObjectiveCompleted_owner_104, logic_uScript_GetQuestObjectiveCompleted_objectiveId_104);
		if (logic_uScript_GetQuestObjectiveCompleted_uScript_GetQuestObjectiveCompleted_104.Complete)
		{
			Relay_In_99();
		}
	}

	private void Relay_In_105()
	{
		logic_uScript_GetQuestObjectiveCompleted_owner_105 = owner_Connection_107;
		logic_uScript_GetQuestObjectiveCompleted_uScript_GetQuestObjectiveCompleted_105.In(logic_uScript_GetQuestObjectiveCompleted_owner_105, logic_uScript_GetQuestObjectiveCompleted_objectiveId_105);
		if (logic_uScript_GetQuestObjectiveCompleted_uScript_GetQuestObjectiveCompleted_105.Complete)
		{
			Relay_In_114();
		}
	}

	private void Relay_In_108()
	{
		logic_uScript_GetQuestObjectiveCompleted_owner_108 = owner_Connection_106;
		logic_uScript_GetQuestObjectiveCompleted_uScript_GetQuestObjectiveCompleted_108.In(logic_uScript_GetQuestObjectiveCompleted_owner_108, logic_uScript_GetQuestObjectiveCompleted_objectiveId_108);
		if (logic_uScript_GetQuestObjectiveCompleted_uScript_GetQuestObjectiveCompleted_108.Complete)
		{
			Relay_In_100();
		}
	}

	private void Relay_In_109()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_109.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_109.Out)
		{
			Relay_In_110();
		}
	}

	private void Relay_In_110()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_110.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_110.Out)
		{
			Relay_In_114();
		}
	}

	private void Relay_SetVisible_113()
	{
		logic_uScript_SetQuestObjectiveVisible_owner_113 = owner_Connection_115;
		logic_uScript_SetQuestObjectiveVisible_objectiveId_113 = local_Stage_System_Int32;
		logic_uScript_SetQuestObjectiveVisible_uScript_SetQuestObjectiveVisible_113.SetVisible(logic_uScript_SetQuestObjectiveVisible_owner_113, logic_uScript_SetQuestObjectiveVisible_objectiveId_113, logic_uScript_SetQuestObjectiveVisible_visible_113);
		if (logic_uScript_SetQuestObjectiveVisible_uScript_SetQuestObjectiveVisible_113.Out)
		{
			Relay_In_233();
		}
	}

	private void Relay_In_114()
	{
		logic_uScriptAct_AddInt_v2_A_114 = local_NumResourceTypes_System_Int32;
		logic_uScriptAct_AddInt_v2_B_114 = local_Stage_System_Int32;
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_114.In(logic_uScriptAct_AddInt_v2_A_114, logic_uScriptAct_AddInt_v2_B_114, out logic_uScriptAct_AddInt_v2_IntResult_114, out logic_uScriptAct_AddInt_v2_FloatResult_114);
		local_Stage_System_Int32 = logic_uScriptAct_AddInt_v2_IntResult_114;
		if (logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_114.Out)
		{
			Relay_SetVisible_113();
		}
	}

	private void Relay_In_117()
	{
		logic_uScriptCon_CompareInt_A_117 = local_NumResourceTypes_System_Int32;
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_117.In(logic_uScriptCon_CompareInt_A_117, logic_uScriptCon_CompareInt_B_117);
		if (logic_uScriptCon_CompareInt_uScriptCon_CompareInt_117.GreaterThanOrEqualTo)
		{
			Relay_SetVisible_69();
		}
	}

	private void Relay_SetVisible_121()
	{
		logic_uScript_SetQuestObjectiveVisible_owner_121 = owner_Connection_120;
		logic_uScript_SetQuestObjectiveVisible_uScript_SetQuestObjectiveVisible_121.SetVisible(logic_uScript_SetQuestObjectiveVisible_owner_121, logic_uScript_SetQuestObjectiveVisible_objectiveId_121, logic_uScript_SetQuestObjectiveVisible_visible_121);
		if (logic_uScript_SetQuestObjectiveVisible_uScript_SetQuestObjectiveVisible_121.Out)
		{
			Relay_In_126();
		}
	}

	private void Relay_In_122()
	{
		logic_uScriptCon_CompareInt_A_122 = local_NumResourceTypes_System_Int32;
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_122.In(logic_uScriptCon_CompareInt_A_122, logic_uScriptCon_CompareInt_B_122);
		if (logic_uScriptCon_CompareInt_uScriptCon_CompareInt_122.GreaterThanOrEqualTo)
		{
			Relay_SetVisible_121();
		}
	}

	private void Relay_SetVisible_125()
	{
		logic_uScript_SetQuestObjectiveVisible_owner_125 = owner_Connection_124;
		logic_uScript_SetQuestObjectiveVisible_uScript_SetQuestObjectiveVisible_125.SetVisible(logic_uScript_SetQuestObjectiveVisible_owner_125, logic_uScript_SetQuestObjectiveVisible_objectiveId_125, logic_uScript_SetQuestObjectiveVisible_visible_125);
	}

	private void Relay_In_126()
	{
		logic_uScriptCon_CompareInt_A_126 = local_NumResourceTypes_System_Int32;
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_126.In(logic_uScriptCon_CompareInt_A_126, logic_uScriptCon_CompareInt_B_126);
		if (logic_uScriptCon_CompareInt_uScriptCon_CompareInt_126.GreaterThanOrEqualTo)
		{
			Relay_SetVisible_125();
		}
	}

	private void Relay_Save_Out_131()
	{
		Relay_Save_198();
	}

	private void Relay_Load_Out_131()
	{
		Relay_Load_198();
	}

	private void Relay_Restart_Out_131()
	{
		Relay_Set_False_198();
	}

	private void Relay_Save_131()
	{
		logic_SubGraph_SaveLoadBool_boolean_131 = local_Init_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_131 = local_Init_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_131.Save(ref logic_SubGraph_SaveLoadBool_boolean_131, logic_SubGraph_SaveLoadBool_boolAsVariable_131, logic_SubGraph_SaveLoadBool_uniqueID_131);
	}

	private void Relay_Load_131()
	{
		logic_SubGraph_SaveLoadBool_boolean_131 = local_Init_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_131 = local_Init_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_131.Load(ref logic_SubGraph_SaveLoadBool_boolean_131, logic_SubGraph_SaveLoadBool_boolAsVariable_131, logic_SubGraph_SaveLoadBool_uniqueID_131);
	}

	private void Relay_Set_True_131()
	{
		logic_SubGraph_SaveLoadBool_boolean_131 = local_Init_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_131 = local_Init_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_131.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_131, logic_SubGraph_SaveLoadBool_boolAsVariable_131, logic_SubGraph_SaveLoadBool_uniqueID_131);
	}

	private void Relay_Set_False_131()
	{
		logic_SubGraph_SaveLoadBool_boolean_131 = local_Init_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_131 = local_Init_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_131.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_131, logic_SubGraph_SaveLoadBool_boolAsVariable_131, logic_SubGraph_SaveLoadBool_uniqueID_131);
	}

	private void Relay_In_134()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_134.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_134.Out)
		{
			Relay_In_151();
		}
	}

	private void Relay_In_135()
	{
		logic_uScript_SetCustomRadarTeamID_tech_135 = local_NPCTech_Tank;
		logic_uScript_SetCustomRadarTeamID_uScript_SetCustomRadarTeamID_135.In(logic_uScript_SetCustomRadarTeamID_tech_135, logic_uScript_SetCustomRadarTeamID_radarTeamID_135);
		if (logic_uScript_SetCustomRadarTeamID_uScript_SetCustomRadarTeamID_135.Out)
		{
			Relay_In_151();
		}
	}

	private void Relay_In_136()
	{
		logic_uScript_RemoveScenery_ownerNode_136 = owner_Connection_148;
		logic_uScript_RemoveScenery_positionName_136 = NPCPosition;
		logic_uScript_RemoveScenery_uScript_RemoveScenery_136.In(logic_uScript_RemoveScenery_ownerNode_136, logic_uScript_RemoveScenery_positionName_136, logic_uScript_RemoveScenery_radius_136, logic_uScript_RemoveScenery_preventChunksSpawning_136);
		if (logic_uScript_RemoveScenery_uScript_RemoveScenery_136.Out)
		{
			Relay_In_146();
		}
	}

	private void Relay_In_137()
	{
		logic_uScript_SetTankInvulnerable_tank_137 = local_NPCTech_Tank;
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_137.In(logic_uScript_SetTankInvulnerable_invulnerable_137, logic_uScript_SetTankInvulnerable_tank_137);
		if (logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_137.Out)
		{
			Relay_In_141();
		}
	}

	private void Relay_In_141()
	{
		logic_uScript_LockTech_tech_141 = local_NPCTech_Tank;
		logic_uScript_LockTech_uScript_LockTech_141.In(logic_uScript_LockTech_tech_141, logic_uScript_LockTech_lockType_141);
		if (logic_uScript_LockTech_uScript_LockTech_141.Out)
		{
			Relay_In_149();
		}
	}

	private void Relay_In_143()
	{
		logic_uScriptCon_CompareBool_Bool_143 = local_Init_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_143.In(logic_uScriptCon_CompareBool_Bool_143);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_143.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_143.False;
		if (num)
		{
			Relay_In_146();
		}
		if (flag)
		{
			Relay_True_144();
		}
	}

	private void Relay_True_144()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_144.True(out logic_uScriptAct_SetBool_Target_144);
		local_Init_System_Boolean = logic_uScriptAct_SetBool_Target_144;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_144.Out)
		{
			Relay_InitialSpawn_147();
		}
	}

	private void Relay_False_144()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_144.False(out logic_uScriptAct_SetBool_Target_144);
		local_Init_System_Boolean = logic_uScriptAct_SetBool_Target_144;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_144.Out)
		{
			Relay_InitialSpawn_147();
		}
	}

	private void Relay_In_146()
	{
		int num = 0;
		Array nPCSpawnData = NPCSpawnData;
		if (logic_uScript_GetAndCheckTechs_techData_146.Length != num + nPCSpawnData.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_146, num + nPCSpawnData.Length);
		}
		Array.Copy(nPCSpawnData, 0, logic_uScript_GetAndCheckTechs_techData_146, num, nPCSpawnData.Length);
		num += nPCSpawnData.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_146 = owner_Connection_145;
		int num2 = 0;
		Array array = local_150_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_146.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_146, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techs_146, num2, array.Length);
		num2 += array.Length;
		logic_uScript_GetAndCheckTechs_Return_146 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_146.In(logic_uScript_GetAndCheckTechs_techData_146, logic_uScript_GetAndCheckTechs_ownerNode_146, ref logic_uScript_GetAndCheckTechs_techs_146);
		local_150_TankArray = logic_uScript_GetAndCheckTechs_techs_146;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_146.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_146.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_146.AllDead;
		if (allAlive)
		{
			Relay_AtIndex_152();
		}
		if (someAlive)
		{
			Relay_AtIndex_152();
		}
		if (allDead)
		{
			Relay_In_153();
		}
	}

	private void Relay_InitialSpawn_147()
	{
		int num = 0;
		Array nPCSpawnData = NPCSpawnData;
		if (logic_uScript_SpawnTechsFromData_spawnData_147.Length != num + nPCSpawnData.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_147, num + nPCSpawnData.Length);
		}
		Array.Copy(nPCSpawnData, 0, logic_uScript_SpawnTechsFromData_spawnData_147, num, nPCSpawnData.Length);
		num += nPCSpawnData.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_147 = owner_Connection_142;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_147.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_147, logic_uScript_SpawnTechsFromData_ownerNode_147, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_147);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_147.Out)
		{
			Relay_In_136();
		}
	}

	private void Relay_In_149()
	{
		logic_uScript_LockTechInteraction_tech_149 = local_NPCTech_Tank;
		logic_uScript_LockTechInteraction_uScript_LockTechInteraction_149.In(logic_uScript_LockTechInteraction_tech_149, logic_uScript_LockTechInteraction_excludedBlocks_149);
		if (logic_uScript_LockTechInteraction_uScript_LockTechInteraction_149.Out)
		{
			Relay_In_135();
		}
	}

	private void Relay_In_151()
	{
		logic_uScript_DirectEnemiesOutOfEncounter_ownerNode_151 = owner_Connection_139;
		logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_151.In(logic_uScript_DirectEnemiesOutOfEncounter_ownerNode_151);
		if (logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_151.Out)
		{
			Relay_In_11();
		}
	}

	private void Relay_AtIndex_152()
	{
		int num = 0;
		Array array = local_150_TankArray;
		if (logic_uScript_AccessListTech_techList_152.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_152, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_152, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_152.AtIndex(ref logic_uScript_AccessListTech_techList_152, logic_uScript_AccessListTech_index_152, out logic_uScript_AccessListTech_value_152);
		local_150_TankArray = logic_uScript_AccessListTech_techList_152;
		local_NPCTech_Tank = logic_uScript_AccessListTech_value_152;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_152.Out)
		{
			Relay_In_137();
		}
	}

	private void Relay_In_153()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_153.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_153.Out)
		{
			Relay_In_134();
		}
	}

	private void Relay_Save_Out_161()
	{
		Relay_Save_131();
	}

	private void Relay_Load_Out_161()
	{
		Relay_Load_131();
	}

	private void Relay_Restart_Out_161()
	{
		Relay_Set_False_131();
	}

	private void Relay_Save_161()
	{
		logic_SubGraph_SaveLoadInt_integer_161 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_161 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_161.Save(logic_SubGraph_SaveLoadInt_restartValue_161, ref logic_SubGraph_SaveLoadInt_integer_161, logic_SubGraph_SaveLoadInt_intAsVariable_161, logic_SubGraph_SaveLoadInt_uniqueID_161);
	}

	private void Relay_Load_161()
	{
		logic_SubGraph_SaveLoadInt_integer_161 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_161 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_161.Load(logic_SubGraph_SaveLoadInt_restartValue_161, ref logic_SubGraph_SaveLoadInt_integer_161, logic_SubGraph_SaveLoadInt_intAsVariable_161, logic_SubGraph_SaveLoadInt_uniqueID_161);
	}

	private void Relay_Restart_161()
	{
		logic_SubGraph_SaveLoadInt_integer_161 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_161 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_161.Restart(logic_SubGraph_SaveLoadInt_restartValue_161, ref logic_SubGraph_SaveLoadInt_integer_161, logic_SubGraph_SaveLoadInt_intAsVariable_161, logic_SubGraph_SaveLoadInt_uniqueID_161);
	}

	private void Relay_In_163()
	{
		logic_uScriptCon_CompareInt_A_163 = local_Stage_System_Int32;
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_163.In(logic_uScriptCon_CompareInt_A_163, logic_uScriptCon_CompareInt_B_163);
		if (logic_uScriptCon_CompareInt_uScriptCon_CompareInt_163.EqualTo)
		{
			Relay_In_19();
		}
	}

	private void Relay_In_165()
	{
		logic_uScriptCon_CompareBool_Bool_165 = timedMission;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_165.In(logic_uScriptCon_CompareBool_Bool_165);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_165.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_165.False;
		if (num)
		{
			Relay_TimeRemaining_175();
		}
		if (flag)
		{
			Relay_In_177();
		}
	}

	private void Relay_In_167()
	{
		logic_uScriptCon_CompareBool_Bool_167 = timedMission;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_167.In(logic_uScriptCon_CompareBool_Bool_167);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_167.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_167.False;
		if (num)
		{
			Relay_In_169();
		}
		if (flag)
		{
			Relay_In_171();
		}
	}

	private void Relay_In_169()
	{
		logic_uScript_StartEncounterTimer_owner_169 = owner_Connection_170;
		logic_uScript_StartEncounterTimer_uScript_StartEncounterTimer_169.In(logic_uScript_StartEncounterTimer_owner_169);
		if (logic_uScript_StartEncounterTimer_uScript_StartEncounterTimer_169.Out)
		{
			Relay_In_117();
		}
	}

	private void Relay_In_171()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_171.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_171.Out)
		{
			Relay_In_117();
		}
	}

	private void Relay_In_173()
	{
		logic_uScriptCon_CompareFloat_A_173 = local_TimeRemaining_System_Single;
		logic_uScriptCon_CompareFloat_uScriptCon_CompareFloat_173.In(logic_uScriptCon_CompareFloat_A_173, logic_uScriptCon_CompareFloat_B_173);
		bool greaterThan = logic_uScriptCon_CompareFloat_uScriptCon_CompareFloat_173.GreaterThan;
		bool lessThanOrEqualTo = logic_uScriptCon_CompareFloat_uScriptCon_CompareFloat_173.LessThanOrEqualTo;
		if (greaterThan)
		{
			Relay_In_48();
		}
		if (lessThanOrEqualTo)
		{
			Relay_Fail_178();
		}
	}

	private void Relay_TimeRemaining_175()
	{
		logic_uScript_GetEncounterTimeRemaining_owner_175 = owner_Connection_174;
		logic_uScript_GetEncounterTimeRemaining_Return_175 = logic_uScript_GetEncounterTimeRemaining_uScript_GetEncounterTimeRemaining_175.TimeRemaining(logic_uScript_GetEncounterTimeRemaining_owner_175);
		local_TimeRemaining_System_Single = logic_uScript_GetEncounterTimeRemaining_Return_175;
		if (logic_uScript_GetEncounterTimeRemaining_uScript_GetEncounterTimeRemaining_175.Out)
		{
			Relay_In_173();
		}
	}

	private void Relay_TimeRemainingPercent_175()
	{
		logic_uScript_GetEncounterTimeRemaining_owner_175 = owner_Connection_174;
		logic_uScript_GetEncounterTimeRemaining_Return_175 = logic_uScript_GetEncounterTimeRemaining_uScript_GetEncounterTimeRemaining_175.TimeRemainingPercent(logic_uScript_GetEncounterTimeRemaining_owner_175);
		local_TimeRemaining_System_Single = logic_uScript_GetEncounterTimeRemaining_Return_175;
		if (logic_uScript_GetEncounterTimeRemaining_uScript_GetEncounterTimeRemaining_175.Out)
		{
			Relay_In_173();
		}
	}

	private void Relay_In_177()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_177.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_177.Out)
		{
			Relay_In_48();
		}
	}

	private void Relay_Succeed_178()
	{
		logic_uScript_FinishEncounter_owner_178 = owner_Connection_179;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_178.Succeed(logic_uScript_FinishEncounter_owner_178);
	}

	private void Relay_Fail_178()
	{
		logic_uScript_FinishEncounter_owner_178 = owner_Connection_179;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_178.Fail(logic_uScript_FinishEncounter_owner_178);
	}

	private void Relay_In_182()
	{
		int num = 0;
		Array array = local_184_ChunkTypesArray;
		if (logic_uScript_GetRandomChunkType_chunkList_182.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_GetRandomChunkType_chunkList_182, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetRandomChunkType_chunkList_182, num, array.Length);
		num += array.Length;
		logic_uScript_GetRandomChunkType_Return_182 = logic_uScript_GetRandomChunkType_uScript_GetRandomChunkType_182.In(logic_uScript_GetRandomChunkType_chunkList_182);
		local_RandomResourceType_ChunkTypes = logic_uScript_GetRandomChunkType_Return_182;
		if (logic_uScript_GetRandomChunkType_uScript_GetRandomChunkType_182.Out)
		{
			Relay_In_194();
		}
	}

	private void Relay_In_183()
	{
		logic_uScriptCon_CompareBool_Bool_183 = useRandomResourceType;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_183.In(logic_uScriptCon_CompareBool_Bool_183);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_183.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_183.False;
		if (num)
		{
			Relay_In_195();
		}
		if (flag)
		{
			Relay_In_230();
		}
	}

	private void Relay_In_186()
	{
		logic_uScript_RestrictItemPickup_tech_186 = local_NPCTech_Tank;
		int num = 0;
		if (logic_uScript_RestrictItemPickup_typesToAccept_186.Length <= num)
		{
			Array.Resize(ref logic_uScript_RestrictItemPickup_typesToAccept_186, num + 1);
		}
		logic_uScript_RestrictItemPickup_typesToAccept_186[num++] = local_RandomResourceType_ChunkTypes;
		logic_uScript_RestrictItemPickup_uScript_RestrictItemPickup_186.In(logic_uScript_RestrictItemPickup_tech_186, logic_uScript_RestrictItemPickup_typesToAccept_186);
		if (logic_uScript_RestrictItemPickup_uScript_RestrictItemPickup_186.Out)
		{
			Relay_In_228();
		}
	}

	private void Relay_In_190()
	{
		logic_uScriptCon_CompareBool_Bool_190 = useRandomResourceType;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_190.In(logic_uScriptCon_CompareBool_Bool_190);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_190.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_190.False;
		if (num)
		{
			Relay_In_191();
		}
		if (flag)
		{
			Relay_AtIndex_201();
		}
	}

	private void Relay_Out_191()
	{
		Relay_In_200();
	}

	private void Relay_In_191()
	{
		logic_SubGraph_Harvesting_Mission_ResourceTracker_block_191 = local_SiloBlock_TankBlock;
		logic_SubGraph_Harvesting_Mission_ResourceTracker_resourceType_191 = local_RandomResourceType_ChunkTypes;
		logic_SubGraph_Harvesting_Mission_ResourceTracker_SubGraph_Harvesting_Mission_ResourceTracker_191.In(logic_SubGraph_Harvesting_Mission_ResourceTracker_block_191, logic_SubGraph_Harvesting_Mission_ResourceTracker_resourceType_191, logic_SubGraph_Harvesting_Mission_ResourceTracker_objectiveID_191);
	}

	private void Relay_In_194()
	{
		int num = 0;
		if (logic_uScriptAct_Log_Target_194.Length <= num)
		{
			Array.Resize(ref logic_uScriptAct_Log_Target_194, num + 1);
		}
		logic_uScriptAct_Log_Target_194[num++] = local_RandomResourceType_ChunkTypes;
		logic_uScriptAct_Log_uScriptAct_Log_194.In(logic_uScriptAct_Log_Prefix_194, logic_uScriptAct_Log_Target_194, logic_uScriptAct_Log_Postfix_194);
		if (logic_uScriptAct_Log_uScriptAct_Log_194.Out)
		{
			Relay_In_186();
		}
	}

	private void Relay_In_195()
	{
		logic_uScriptCon_CompareBool_Bool_195 = local_RandomResourceTypeSet_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_195.In(logic_uScriptCon_CompareBool_Bool_195);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_195.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_195.False;
		if (num)
		{
			Relay_In_186();
		}
		if (flag)
		{
			Relay_True_197();
		}
	}

	private void Relay_True_197()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_197.True(out logic_uScriptAct_SetBool_Target_197);
		local_RandomResourceTypeSet_System_Boolean = logic_uScriptAct_SetBool_Target_197;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_197.Out)
		{
			Relay_In_182();
		}
	}

	private void Relay_False_197()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_197.False(out logic_uScriptAct_SetBool_Target_197);
		local_RandomResourceTypeSet_System_Boolean = logic_uScriptAct_SetBool_Target_197;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_197.Out)
		{
			Relay_In_182();
		}
	}

	private void Relay_Save_Out_198()
	{
		Relay_Save_35();
	}

	private void Relay_Load_Out_198()
	{
		Relay_Load_35();
	}

	private void Relay_Restart_Out_198()
	{
		Relay_Set_False_35();
	}

	private void Relay_Save_198()
	{
		logic_SubGraph_SaveLoadBool_boolean_198 = local_RandomResourceTypeSet_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_198 = local_RandomResourceTypeSet_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_198.Save(ref logic_SubGraph_SaveLoadBool_boolean_198, logic_SubGraph_SaveLoadBool_boolAsVariable_198, logic_SubGraph_SaveLoadBool_uniqueID_198);
	}

	private void Relay_Load_198()
	{
		logic_SubGraph_SaveLoadBool_boolean_198 = local_RandomResourceTypeSet_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_198 = local_RandomResourceTypeSet_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_198.Load(ref logic_SubGraph_SaveLoadBool_boolean_198, logic_SubGraph_SaveLoadBool_boolAsVariable_198, logic_SubGraph_SaveLoadBool_uniqueID_198);
	}

	private void Relay_Set_True_198()
	{
		logic_SubGraph_SaveLoadBool_boolean_198 = local_RandomResourceTypeSet_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_198 = local_RandomResourceTypeSet_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_198.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_198, logic_SubGraph_SaveLoadBool_boolAsVariable_198, logic_SubGraph_SaveLoadBool_uniqueID_198);
	}

	private void Relay_Set_False_198()
	{
		logic_SubGraph_SaveLoadBool_boolean_198 = local_RandomResourceTypeSet_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_198 = local_RandomResourceTypeSet_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_198.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_198, logic_SubGraph_SaveLoadBool_boolAsVariable_198, logic_SubGraph_SaveLoadBool_uniqueID_198);
	}

	private void Relay_In_200()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_200.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_200.Out)
		{
			Relay_In_93();
		}
	}

	private void Relay_AtIndex_201()
	{
		int num = 0;
		Array array = resourceTypesAccepted;
		if (logic_uScript_AccessListChunk_chunkList_201.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListChunk_chunkList_201, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListChunk_chunkList_201, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListChunk_uScript_AccessListChunk_201.AtIndex(ref logic_uScript_AccessListChunk_chunkList_201, logic_uScript_AccessListChunk_index_201, out logic_uScript_AccessListChunk_value_201);
		resourceTypesAccepted = logic_uScript_AccessListChunk_chunkList_201;
		local_ResourceType01_ChunkTypes = logic_uScript_AccessListChunk_value_201;
		if (logic_uScript_AccessListChunk_uScript_AccessListChunk_201.Out)
		{
			Relay_In_204();
		}
	}

	private void Relay_Out_204()
	{
		Relay_In_81();
	}

	private void Relay_In_204()
	{
		logic_SubGraph_Harvesting_Mission_ResourceTracker_block_204 = local_SiloBlock_TankBlock;
		logic_SubGraph_Harvesting_Mission_ResourceTracker_resourceType_204 = local_ResourceType01_ChunkTypes;
		logic_SubGraph_Harvesting_Mission_ResourceTracker_SubGraph_Harvesting_Mission_ResourceTracker_204.In(logic_SubGraph_Harvesting_Mission_ResourceTracker_block_204, logic_SubGraph_Harvesting_Mission_ResourceTracker_resourceType_204, logic_SubGraph_Harvesting_Mission_ResourceTracker_objectiveID_204);
	}

	private void Relay_AtIndex_207()
	{
		int num = 0;
		Array array = resourceTypesAccepted;
		if (logic_uScript_AccessListChunk_chunkList_207.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListChunk_chunkList_207, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListChunk_chunkList_207, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListChunk_uScript_AccessListChunk_207.AtIndex(ref logic_uScript_AccessListChunk_chunkList_207, logic_uScript_AccessListChunk_index_207, out logic_uScript_AccessListChunk_value_207);
		resourceTypesAccepted = logic_uScript_AccessListChunk_chunkList_207;
		local_ResourceType02_ChunkTypes = logic_uScript_AccessListChunk_value_207;
		if (logic_uScript_AccessListChunk_uScript_AccessListChunk_207.Out)
		{
			Relay_In_211();
		}
	}

	private void Relay_Out_211()
	{
		Relay_In_83();
	}

	private void Relay_In_211()
	{
		logic_SubGraph_Harvesting_Mission_ResourceTracker_block_211 = local_SiloBlock_TankBlock;
		logic_SubGraph_Harvesting_Mission_ResourceTracker_resourceType_211 = local_ResourceType02_ChunkTypes;
		logic_SubGraph_Harvesting_Mission_ResourceTracker_SubGraph_Harvesting_Mission_ResourceTracker_211.In(logic_SubGraph_Harvesting_Mission_ResourceTracker_block_211, logic_SubGraph_Harvesting_Mission_ResourceTracker_resourceType_211, logic_SubGraph_Harvesting_Mission_ResourceTracker_objectiveID_211);
	}

	private void Relay_AtIndex_214()
	{
		int num = 0;
		Array array = resourceTypesAccepted;
		if (logic_uScript_AccessListChunk_chunkList_214.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListChunk_chunkList_214, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListChunk_chunkList_214, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListChunk_uScript_AccessListChunk_214.AtIndex(ref logic_uScript_AccessListChunk_chunkList_214, logic_uScript_AccessListChunk_index_214, out logic_uScript_AccessListChunk_value_214);
		resourceTypesAccepted = logic_uScript_AccessListChunk_chunkList_214;
		local_ResourceType03_ChunkTypes = logic_uScript_AccessListChunk_value_214;
		if (logic_uScript_AccessListChunk_uScript_AccessListChunk_214.Out)
		{
			Relay_In_216();
		}
	}

	private void Relay_Out_216()
	{
		Relay_In_84();
	}

	private void Relay_In_216()
	{
		logic_SubGraph_Harvesting_Mission_ResourceTracker_block_216 = local_SiloBlock_TankBlock;
		logic_SubGraph_Harvesting_Mission_ResourceTracker_resourceType_216 = local_ResourceType03_ChunkTypes;
		logic_SubGraph_Harvesting_Mission_ResourceTracker_SubGraph_Harvesting_Mission_ResourceTracker_216.In(logic_SubGraph_Harvesting_Mission_ResourceTracker_block_216, logic_SubGraph_Harvesting_Mission_ResourceTracker_resourceType_216, logic_SubGraph_Harvesting_Mission_ResourceTracker_objectiveID_216);
	}

	private void Relay_AtIndex_219()
	{
		int num = 0;
		Array array = resourceTypesAccepted;
		if (logic_uScript_AccessListChunk_chunkList_219.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListChunk_chunkList_219, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListChunk_chunkList_219, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListChunk_uScript_AccessListChunk_219.AtIndex(ref logic_uScript_AccessListChunk_chunkList_219, logic_uScript_AccessListChunk_index_219, out logic_uScript_AccessListChunk_value_219);
		resourceTypesAccepted = logic_uScript_AccessListChunk_chunkList_219;
		local_ResourceType04_ChunkTypes = logic_uScript_AccessListChunk_value_219;
		if (logic_uScript_AccessListChunk_uScript_AccessListChunk_219.Out)
		{
			Relay_In_222();
		}
	}

	private void Relay_Out_222()
	{
		Relay_In_93();
	}

	private void Relay_In_222()
	{
		logic_SubGraph_Harvesting_Mission_ResourceTracker_block_222 = local_SiloBlock_TankBlock;
		logic_SubGraph_Harvesting_Mission_ResourceTracker_resourceType_222 = local_ResourceType04_ChunkTypes;
		logic_SubGraph_Harvesting_Mission_ResourceTracker_SubGraph_Harvesting_Mission_ResourceTracker_222.In(logic_SubGraph_Harvesting_Mission_ResourceTracker_block_222, logic_SubGraph_Harvesting_Mission_ResourceTracker_resourceType_222, logic_SubGraph_Harvesting_Mission_ResourceTracker_objectiveID_222);
	}

	private void Relay_In_225()
	{
		int num = 0;
		Array array = resourceTypesAccepted;
		if (logic_uScript_GetChunkListLength_chunkList_225.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_GetChunkListLength_chunkList_225, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetChunkListLength_chunkList_225, num, array.Length);
		num += array.Length;
		logic_uScript_GetChunkListLength_Return_225 = logic_uScript_GetChunkListLength_uScript_GetChunkListLength_225.In(logic_uScript_GetChunkListLength_chunkList_225);
		local_NumResourceTypes_System_Int32 = logic_uScript_GetChunkListLength_Return_225;
		if (logic_uScript_GetChunkListLength_uScript_GetChunkListLength_225.Out)
		{
			Relay_In_14();
		}
	}

	private void Relay_In_228()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_228.In(logic_uScriptAct_SetInt_Value_228, out logic_uScriptAct_SetInt_Target_228);
		local_NumResourceTypes_System_Int32 = logic_uScriptAct_SetInt_Target_228;
		if (logic_uScriptAct_SetInt_uScriptAct_SetInt_228.Out)
		{
			Relay_In_14();
		}
	}

	private void Relay_In_230()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_230.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_230.Out)
		{
			Relay_In_36();
		}
	}

	private void Relay_In_231()
	{
		logic_uScript_RemoveEncounterTimer_owner_231 = owner_Connection_232;
		logic_uScript_RemoveEncounterTimer_uScript_RemoveEncounterTimer_231.In(logic_uScript_RemoveEncounterTimer_owner_231);
	}

	private void Relay_In_233()
	{
		logic_uScriptCon_CompareBool_Bool_233 = timedMission;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_233.In(logic_uScriptCon_CompareBool_Bool_233);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_233.True)
		{
			Relay_In_231();
		}
	}
}
