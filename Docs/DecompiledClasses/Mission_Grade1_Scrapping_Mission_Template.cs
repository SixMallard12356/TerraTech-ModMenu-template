using System;
using BehaviorDesigner.Runtime;
using UnityEngine;

[Serializable]
[NodePath("Graphs")]
[FriendlyName("Untitled", "")]
public class Mission_Grade1_Scrapping_Mission_Template : uScriptLogic
{
	private GameObject parentGameObject;

	private uScript_GUI thisScriptsOnGuiListener;

	private bool m_RegisteredForEvents;

	[Multiline(3)]
	public string BasePosition = "";

	[Multiline(3)]
	public string BaseVFXSpawn = "";

	public BlockTypes BlockTypeSJSscrapper;

	public float clearSceneryRadius;

	public SpawnTechData[] CraftingBaseSpawnData = new SpawnTechData[0];

	public float distBaseFound;

	public SpawnTechData[] EnemyTechData = new SpawnTechData[0];

	private Tank[] local_104_TankArray = new Tank[0];

	private Tank[] local_106_TankArray = new Tank[0];

	private string local_63_System_String = "";

	private string local_64_System_String = "Shownmessage: ";

	private bool local_AllBlocksScrapped_System_Boolean;

	private bool local_CanSuckUpBlocks_System_Boolean;

	private TankBlock local_CrafterFromEvent_TankBlock;

	private Tank local_CraftingBaseTech_Tank;

	private int local_CurrentAmountType_System_Int32;

	private Tank[] local_Enemy_TankArray = new Tank[0];

	private bool local_EnemyAlive_System_Boolean;

	private bool local_Initialize_System_Boolean;

	private bool local_msgAllBlocksCraftedShown_System_Boolean;

	private bool local_msgBaseFoundShown_System_Boolean;

	private bool local_msgIntroShown_System_Boolean;

	private bool local_NearBase_System_Boolean;

	private Tank local_NPCTech_Tank;

	private TankBlock local_ScrapperBlock_TankBlock;

	private bool local_ScrappingInProgress_System_Boolean;

	private int local_Stage_System_Int32 = 1;

	private int local_Stage2_System_Int32 = 1;

	public uScript_AddMessage.MessageSpeaker messageSpeaker;

	[Multiline(3)]
	public string messageTag = "";

	public uScript_AddMessage.MessageData msg01Intro;

	public uScript_AddMessage.MessageData msg02BaseFound;

	public uScript_AddMessage.MessageData msg03PutBlocksInScrapper;

	public uScript_AddMessage.MessageData msg04AllBlocksScrapped;

	public uScript_AddMessage.MessageData msg05Complete;

	public uScript_AddMessage.MessageData msgLeavingMissionArea;

	public uScript_AddMessage.MessageData msgSpawnMinion;

	public Transform NPCDespawnParticleEffect;

	public ExternalBehaviorTree NPCFlyAwayAI;

	public SpawnTechData[] NPCSpawnData = new SpawnTechData[0];

	public int TargetAmount;

	private GameObject owner_Connection_14;

	private GameObject owner_Connection_22;

	private GameObject owner_Connection_81;

	private GameObject owner_Connection_85;

	private GameObject owner_Connection_94;

	private GameObject owner_Connection_101;

	private GameObject owner_Connection_110;

	private GameObject owner_Connection_115;

	private GameObject owner_Connection_137;

	private GameObject owner_Connection_142;

	private GameObject owner_Connection_151;

	private GameObject owner_Connection_156;

	private GameObject owner_Connection_176;

	private GameObject owner_Connection_186;

	private GameObject owner_Connection_190;

	private GameObject owner_Connection_205;

	private GameObject owner_Connection_515;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_0 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_0;

	private bool logic_uScriptAct_SetBool_Out_0 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_0 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_0 = true;

	private uScript_SetEncounterTarget logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_2 = new uScript_SetEncounterTarget();

	private GameObject logic_uScript_SetEncounterTarget_owner_2;

	private object logic_uScript_SetEncounterTarget_visibleObject_2 = "";

	private bool logic_uScript_SetEncounterTarget_Out_2 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_4 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_4;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_4;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_4;

	private bool logic_uScript_AddMessage_Out_4 = true;

	private bool logic_uScript_AddMessage_Shown_4 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_5 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_5 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_5 = true;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_5 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_7;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_7 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_7 = "msgIntroShown";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_9 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_9;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_9 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_9 = "Initialize";

	private SubGraph_CompleteObjectiveStage logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_12 = new SubGraph_CompleteObjectiveStage();

	private int logic_SubGraph_CompleteObjectiveStage_objectiveStage_12;

	private bool logic_SubGraph_CompleteObjectiveStage_isFinalObjective_12;

	private SubGraph_SaveLoadInt logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_18 = new SubGraph_SaveLoadInt();

	private int logic_SubGraph_SaveLoadInt_restartValue_18 = 1;

	private int logic_SubGraph_SaveLoadInt_integer_18;

	private object logic_SubGraph_SaveLoadInt_intAsVariable_18 = "";

	private string logic_SubGraph_SaveLoadInt_uniqueID_18 = "Stage";

	private uScript_HideArrow logic_uScript_HideArrow_uScript_HideArrow_20 = new uScript_HideArrow();

	private bool logic_uScript_HideArrow_Out_20 = true;

	private uScript_PausePopulation logic_uScript_PausePopulation_uScript_PausePopulation_21 = new uScript_PausePopulation();

	private bool logic_uScript_PausePopulation_Out_21 = true;

	private uScriptCon_ManualSwitch logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_23 = new uScriptCon_ManualSwitch();

	private int logic_uScriptCon_ManualSwitch_CurrentOutput_23;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_28 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_28;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_28 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_28 = "msgBaseFoundShown";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_30 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_30;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_30 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_30 = "ScrappingInProgress";

	private uScript_IsPlayerInRangeOfTech logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_31 = new uScript_IsPlayerInRangeOfTech();

	private Tank logic_uScript_IsPlayerInRangeOfTech_tech_31;

	private float logic_uScript_IsPlayerInRangeOfTech_range_31 = 100f;

	private Tank[] logic_uScript_IsPlayerInRangeOfTech_techs_31 = new Tank[0];

	private bool logic_uScript_IsPlayerInRangeOfTech_Out_31 = true;

	private bool logic_uScript_IsPlayerInRangeOfTech_InRange_31 = true;

	private bool logic_uScript_IsPlayerInRangeOfTech_OutOfRange_31 = true;

	private uScript_PausePopulation logic_uScript_PausePopulation_uScript_PausePopulation_33 = new uScript_PausePopulation();

	private bool logic_uScript_PausePopulation_Out_33 = true;

	private uScript_IsPlayerInRangeOfTech logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_34 = new uScript_IsPlayerInRangeOfTech();

	private Tank logic_uScript_IsPlayerInRangeOfTech_tech_34;

	private float logic_uScript_IsPlayerInRangeOfTech_range_34;

	private Tank[] logic_uScript_IsPlayerInRangeOfTech_techs_34 = new Tank[0];

	private bool logic_uScript_IsPlayerInRangeOfTech_Out_34 = true;

	private bool logic_uScript_IsPlayerInRangeOfTech_InRange_34 = true;

	private bool logic_uScript_IsPlayerInRangeOfTech_OutOfRange_34 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_35 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_35;

	private bool logic_uScriptAct_SetBool_Out_35 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_35 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_35 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_36 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_36;

	private bool logic_uScriptCon_CompareBool_True_36 = true;

	private bool logic_uScriptCon_CompareBool_False_36 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_37 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_37;

	private bool logic_uScriptCon_CompareBool_True_37 = true;

	private bool logic_uScriptCon_CompareBool_False_37 = true;

	private SubGraph_CompleteObjectiveStage logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_41 = new SubGraph_CompleteObjectiveStage();

	private int logic_SubGraph_CompleteObjectiveStage_objectiveStage_41;

	private bool logic_SubGraph_CompleteObjectiveStage_isFinalObjective_41;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_44 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_44;

	private bool logic_uScriptAct_SetBool_Out_44 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_44 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_44 = true;

	private uScript_LockTechInteraction logic_uScript_LockTechInteraction_uScript_LockTechInteraction_45 = new uScript_LockTechInteraction();

	private Tank logic_uScript_LockTechInteraction_tech_45;

	private BlockTypes[] logic_uScript_LockTechInteraction_excludedBlocks_45 = new BlockTypes[0];

	private TankBlock[] logic_uScript_LockTechInteraction_excludedUniqueBlocks_45 = new TankBlock[0];

	private bool logic_uScript_LockTechInteraction_Out_45 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_47 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_47;

	private bool logic_uScriptAct_SetBool_Out_47 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_47 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_47 = true;

	private uScript_LockTechStacks logic_uScript_LockTechStacks_uScript_LockTechStacks_48 = new uScript_LockTechStacks();

	private Tank logic_uScript_LockTechStacks_tech_48;

	private bool logic_uScript_LockTechStacks_Out_48 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_54 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_54;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_54;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_54;

	private bool logic_uScript_AddMessage_Out_54 = true;

	private bool logic_uScript_AddMessage_Shown_54 = true;

	private SubGraph_LoadObjectiveStates logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_57 = new SubGraph_LoadObjectiveStates();

	private int logic_SubGraph_LoadObjectiveStates_currentObjective_57;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_58 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_58;

	private bool logic_uScriptCon_CompareBool_True_58 = true;

	private bool logic_uScriptCon_CompareBool_False_58 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_59 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_59;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_59 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_59 = "CanSuckUpBlocks";

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_60 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_60;

	private bool logic_uScriptCon_CompareBool_True_60 = true;

	private bool logic_uScriptCon_CompareBool_False_60 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_61 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_61 = true;

	private uScriptAct_Concatenate logic_uScriptAct_Concatenate_uScriptAct_Concatenate_62 = new uScriptAct_Concatenate();

	private object[] logic_uScriptAct_Concatenate_A_62 = new object[0];

	private object[] logic_uScriptAct_Concatenate_B_62 = new object[0];

	private string logic_uScriptAct_Concatenate_Separator_62 = "";

	private string logic_uScriptAct_Concatenate_Result_62;

	private bool logic_uScriptAct_Concatenate_Out_62 = true;

	private uScriptAct_PrintText logic_uScriptAct_PrintText_uScriptAct_PrintText_66 = new uScriptAct_PrintText();

	private string logic_uScriptAct_PrintText_Text_66 = "";

	private int logic_uScriptAct_PrintText_FontSize_66 = 16;

	private FontStyle logic_uScriptAct_PrintText_FontStyle_66;

	private Color logic_uScriptAct_PrintText_FontColor_66 = new Color(0f, 0f, 0f, 1f);

	private TextAnchor logic_uScriptAct_PrintText_textAnchor_66;

	private int logic_uScriptAct_PrintText_EdgePadding_66 = 8;

	private float logic_uScriptAct_PrintText_time_66;

	private bool logic_uScriptAct_PrintText_Out_66 = true;

	private uScript_LockBlock logic_uScript_LockBlock_uScript_LockBlock_67 = new uScript_LockBlock();

	private TankBlock logic_uScript_LockBlock_block_67;

	private Visible.LockTimerTypes logic_uScript_LockBlock_functionalityToLock_67 = Visible.LockTimerTypes.ItemCollection;

	private bool logic_uScript_LockBlock_Out_67 = true;

	private uScript_SetTankInvulnerable logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_70 = new uScript_SetTankInvulnerable();

	private bool logic_uScript_SetTankInvulnerable_invulnerable_70 = true;

	private Tank logic_uScript_SetTankInvulnerable_tank_70;

	private bool logic_uScript_SetTankInvulnerable_Out_70 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_71 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_71 = new Tank[0];

	private int logic_uScript_AccessListTech_index_71;

	private Tank logic_uScript_AccessListTech_value_71;

	private bool logic_uScript_AccessListTech_Out_71 = true;

	private uScript_SetCustomRadarTeamID logic_uScript_SetCustomRadarTeamID_uScript_SetCustomRadarTeamID_77 = new uScript_SetCustomRadarTeamID();

	private Tank logic_uScript_SetCustomRadarTeamID_tech_77;

	private int logic_uScript_SetCustomRadarTeamID_radarTeamID_77 = -2;

	private bool logic_uScript_SetCustomRadarTeamID_Out_77 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_78 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_78 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_78;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_78 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_78;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_78 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_78 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_78 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_78 = true;

	private uScript_LockTech logic_uScript_LockTech_uScript_LockTech_79 = new uScript_LockTech();

	private Tank logic_uScript_LockTech_tech_79;

	private uScript_LockTech.TechLockType logic_uScript_LockTech_lockType_79 = uScript_LockTech.TechLockType.LockAll;

	private bool logic_uScript_LockTech_Out_79 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_83 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_83 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_83;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_83 = 0.3f;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_83;

	private bool logic_uScript_SpawnTechsFromData_Out_83 = true;

	private uScript_SetTankHideBlockLimit logic_uScript_SetTankHideBlockLimit_uScript_SetTankHideBlockLimit_86 = new uScript_SetTankHideBlockLimit();

	private bool logic_uScript_SetTankHideBlockLimit_hidden_86 = true;

	private Tank logic_uScript_SetTankHideBlockLimit_tech_86;

	private bool logic_uScript_SetTankHideBlockLimit_Out_86 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_87 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_87 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_89 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_89 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_89;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_89 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_89;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_89 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_89 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_89 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_89 = true;

	private uScript_RemoveScenery logic_uScript_RemoveScenery_uScript_RemoveScenery_91 = new uScript_RemoveScenery();

	private GameObject logic_uScript_RemoveScenery_ownerNode_91;

	private string logic_uScript_RemoveScenery_positionName_91 = "";

	private float logic_uScript_RemoveScenery_radius_91;

	private bool logic_uScript_RemoveScenery_preventChunksSpawning_91 = true;

	private bool logic_uScript_RemoveScenery_Out_91 = true;

	private uScript_SetCustomRadarTeamID logic_uScript_SetCustomRadarTeamID_uScript_SetCustomRadarTeamID_93 = new uScript_SetCustomRadarTeamID();

	private Tank logic_uScript_SetCustomRadarTeamID_tech_93;

	private int logic_uScript_SetCustomRadarTeamID_radarTeamID_93 = -2;

	private bool logic_uScript_SetCustomRadarTeamID_Out_93 = true;

	private uScript_SetTankInvulnerable logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_96 = new uScript_SetTankInvulnerable();

	private bool logic_uScript_SetTankInvulnerable_invulnerable_96 = true;

	private Tank logic_uScript_SetTankInvulnerable_tank_96;

	private bool logic_uScript_SetTankInvulnerable_Out_96 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_99 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_99 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_100 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_100;

	private bool logic_uScriptCon_CompareBool_True_100 = true;

	private bool logic_uScriptCon_CompareBool_False_100 = true;

	private uScript_LockTech logic_uScript_LockTech_uScript_LockTech_102 = new uScript_LockTech();

	private Tank logic_uScript_LockTech_tech_102;

	private uScript_LockTech.TechLockType logic_uScript_LockTech_lockType_102 = uScript_LockTech.TechLockType.LockAll;

	private bool logic_uScript_LockTech_Out_102 = true;

	private uScript_LockTechInteraction logic_uScript_LockTechInteraction_uScript_LockTechInteraction_103 = new uScript_LockTechInteraction();

	private Tank logic_uScript_LockTechInteraction_tech_103;

	private BlockTypes[] logic_uScript_LockTechInteraction_excludedBlocks_103 = new BlockTypes[0];

	private TankBlock[] logic_uScript_LockTechInteraction_excludedUniqueBlocks_103 = new TankBlock[0];

	private bool logic_uScript_LockTechInteraction_Out_103 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_105 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_105 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_109 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_109 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_111 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_111 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_112 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_112 = new Tank[0];

	private int logic_uScript_AccessListTech_index_112;

	private Tank logic_uScript_AccessListTech_value_112;

	private bool logic_uScript_AccessListTech_Out_112 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_113 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_113;

	private bool logic_uScriptAct_SetBool_Out_113 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_113 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_113 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_114 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_114 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_114;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_114 = 0.3f;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_114;

	private bool logic_uScript_SpawnTechsFromData_Out_114 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_117 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_117;

	private bool logic_uScriptAct_SetBool_Out_117 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_117 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_117 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_118 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_118;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_118;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_118;

	private bool logic_uScript_AddMessage_Out_118 = true;

	private bool logic_uScript_AddMessage_Shown_118 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_121 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_121;

	private bool logic_uScriptCon_CompareBool_True_121 = true;

	private bool logic_uScriptCon_CompareBool_False_121 = true;

	private uScript_GetTankBlock logic_uScript_GetTankBlock_uScript_GetTankBlock_123 = new uScript_GetTankBlock();

	private Tank logic_uScript_GetTankBlock_tank_123;

	private BlockTypes logic_uScript_GetTankBlock_blockType_123;

	private TankBlock logic_uScript_GetTankBlock_Return_123;

	private bool logic_uScript_GetTankBlock_Out_123 = true;

	private bool logic_uScript_GetTankBlock_Returned_123 = true;

	private bool logic_uScript_GetTankBlock_NotFound_123 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_127 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_127;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_127;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_127;

	private bool logic_uScript_AddMessage_Out_127 = true;

	private bool logic_uScript_AddMessage_Shown_127 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_128 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_128 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_128;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_128;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_128;

	private bool logic_uScript_SpawnTechsFromData_Out_128 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_129 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_129;

	private bool logic_uScriptCon_CompareBool_True_129 = true;

	private bool logic_uScriptCon_CompareBool_False_129 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_131 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_131;

	private bool logic_uScriptAct_SetBool_Out_131 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_131 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_131 = true;

	private uScriptCon_ManualSwitch logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_140 = new uScriptCon_ManualSwitch();

	private int logic_uScriptCon_ManualSwitch_CurrentOutput_140;

	private uScriptAct_AddInt_v2 logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_143 = new uScriptAct_AddInt_v2();

	private int logic_uScriptAct_AddInt_v2_A_143;

	private int logic_uScriptAct_AddInt_v2_B_143 = 1;

	private int logic_uScriptAct_AddInt_v2_IntResult_143;

	private float logic_uScriptAct_AddInt_v2_FloatResult_143;

	private bool logic_uScriptAct_AddInt_v2_Out_143 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_144 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_144 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_144;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_144 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_144;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_144 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_144 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_144 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_144 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_146 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_146;

	private bool logic_uScriptAct_SetBool_Out_146 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_146 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_146 = true;

	private uScript_SpawnVFX logic_uScript_SpawnVFX_uScript_SpawnVFX_148 = new uScript_SpawnVFX();

	private GameObject logic_uScript_SpawnVFX_ownerNode_148;

	private Transform logic_uScript_SpawnVFX_vfxToSpawn_148;

	private string logic_uScript_SpawnVFX_spawnPosName_148 = "";

	private bool logic_uScript_SpawnVFX_Out_148 = true;

	private uScript_FinishEncounter logic_uScript_FinishEncounter_uScript_FinishEncounter_157 = new uScript_FinishEncounter();

	private GameObject logic_uScript_FinishEncounter_owner_157;

	private bool logic_uScript_FinishEncounter_Out_157 = true;

	private uScript_RemoveTech logic_uScript_RemoveTech_uScript_RemoveTech_160 = new uScript_RemoveTech();

	private Tank logic_uScript_RemoveTech_tech_160;

	private bool logic_uScript_RemoveTech_Out_160 = true;

	private uScript_FlyTechUpAndAway logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_161 = new uScript_FlyTechUpAndAway();

	private Tank logic_uScript_FlyTechUpAndAway_tech_161;

	private float logic_uScript_FlyTechUpAndAway_maxLifetime_161 = 5f;

	private float logic_uScript_FlyTechUpAndAway_targetHeight_161 = 150f;

	private ExternalBehaviorTree logic_uScript_FlyTechUpAndAway_aiTree_161;

	private Transform logic_uScript_FlyTechUpAndAway_removalParticles_161;

	private bool logic_uScript_FlyTechUpAndAway_Out_161 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_162 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_162;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_162;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_162;

	private bool logic_uScript_AddMessage_Out_162 = true;

	private bool logic_uScript_AddMessage_Shown_162 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_164 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_164;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_164 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_164 = "AllBlocksScrapped";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_170 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_170;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_170 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_170 = "EnemyAlive";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_172 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_172;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_172 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_172 = "AllBlocksScrapped";

	private uScript_CompareBlock logic_uScript_CompareBlock_uScript_CompareBlock_175 = new uScript_CompareBlock();

	private TankBlock logic_uScript_CompareBlock_A_175;

	private TankBlock logic_uScript_CompareBlock_B_175;

	private bool logic_uScript_CompareBlock_EqualTo_175 = true;

	private bool logic_uScript_CompareBlock_NotEqualTo_175 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_180 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_180;

	private bool logic_uScriptAct_SetBool_Out_180 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_180 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_180 = true;

	private uScript_SetQuestObjectiveCount logic_uScript_SetQuestObjectiveCount_uScript_SetQuestObjectiveCount_182 = new uScript_SetQuestObjectiveCount();

	private GameObject logic_uScript_SetQuestObjectiveCount_owner_182;

	private int logic_uScript_SetQuestObjectiveCount_objectiveId_182;

	private int logic_uScript_SetQuestObjectiveCount_currentCount_182;

	private bool logic_uScript_SetQuestObjectiveCount_Out_182 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_183 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_183 = "";

	private bool logic_uScript_EnableGlow_enable_183;

	private bool logic_uScript_EnableGlow_Out_183 = true;

	private uScript_HideArrow logic_uScript_HideArrow_uScript_HideArrow_184 = new uScript_HideArrow();

	private bool logic_uScript_HideArrow_Out_184 = true;

	private uScriptCon_CompareInt logic_uScriptCon_CompareInt_uScriptCon_CompareInt_187 = new uScriptCon_CompareInt();

	private int logic_uScriptCon_CompareInt_A_187;

	private int logic_uScriptCon_CompareInt_B_187;

	private bool logic_uScriptCon_CompareInt_GreaterThan_187 = true;

	private bool logic_uScriptCon_CompareInt_GreaterThanOrEqualTo_187 = true;

	private bool logic_uScriptCon_CompareInt_EqualTo_187 = true;

	private bool logic_uScriptCon_CompareInt_NotEqualTo_187 = true;

	private bool logic_uScriptCon_CompareInt_LessThanOrEqualTo_187 = true;

	private bool logic_uScriptCon_CompareInt_LessThan_187 = true;

	private uScriptAct_AddInt_v2 logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_189 = new uScriptAct_AddInt_v2();

	private int logic_uScriptAct_AddInt_v2_A_189;

	private int logic_uScriptAct_AddInt_v2_B_189 = 1;

	private int logic_uScriptAct_AddInt_v2_IntResult_189;

	private float logic_uScriptAct_AddInt_v2_FloatResult_189;

	private bool logic_uScriptAct_AddInt_v2_Out_189 = true;

	private uScript_SetQuestObjectiveCount logic_uScript_SetQuestObjectiveCount_uScript_SetQuestObjectiveCount_191 = new uScript_SetQuestObjectiveCount();

	private GameObject logic_uScript_SetQuestObjectiveCount_owner_191;

	private int logic_uScript_SetQuestObjectiveCount_objectiveId_191;

	private int logic_uScript_SetQuestObjectiveCount_currentCount_191;

	private bool logic_uScript_SetQuestObjectiveCount_Out_191 = true;

	private uScriptCon_CompareInt logic_uScriptCon_CompareInt_uScriptCon_CompareInt_194 = new uScriptCon_CompareInt();

	private int logic_uScriptCon_CompareInt_A_194;

	private int logic_uScriptCon_CompareInt_B_194 = 2;

	private bool logic_uScriptCon_CompareInt_GreaterThan_194 = true;

	private bool logic_uScriptCon_CompareInt_GreaterThanOrEqualTo_194 = true;

	private bool logic_uScriptCon_CompareInt_EqualTo_194 = true;

	private bool logic_uScriptCon_CompareInt_NotEqualTo_194 = true;

	private bool logic_uScriptCon_CompareInt_LessThanOrEqualTo_194 = true;

	private bool logic_uScriptCon_CompareInt_LessThan_194 = true;

	private uScript_PointArrowAtVisible logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_196 = new uScript_PointArrowAtVisible();

	private object logic_uScript_PointArrowAtVisible_targetObject_196 = "";

	private float logic_uScript_PointArrowAtVisible_timeToShowFor_196 = -1f;

	private Vector3 logic_uScript_PointArrowAtVisible_offset_196 = new Vector3(0f, 0f, 0f);

	private bool logic_uScript_PointArrowAtVisible_Out_196 = true;

	private uScript_EnableGlow logic_uScript_EnableGlow_uScript_EnableGlow_201 = new uScript_EnableGlow();

	private object logic_uScript_EnableGlow_targetObject_201 = "";

	private bool logic_uScript_EnableGlow_enable_201 = true;

	private bool logic_uScript_EnableGlow_Out_201 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_202 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_202;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_202;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_202;

	private bool logic_uScript_AddMessage_Out_202 = true;

	private bool logic_uScript_AddMessage_Shown_202 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_204 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_204;

	private bool logic_uScriptAct_SetBool_Out_204 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_204 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_204 = true;

	private uScript_SetQuestObjectiveCount logic_uScript_SetQuestObjectiveCount_uScript_SetQuestObjectiveCount_208 = new uScript_SetQuestObjectiveCount();

	private GameObject logic_uScript_SetQuestObjectiveCount_owner_208;

	private int logic_uScript_SetQuestObjectiveCount_objectiveId_208;

	private int logic_uScript_SetQuestObjectiveCount_currentCount_208;

	private bool logic_uScript_SetQuestObjectiveCount_Out_208 = true;

	private SubGraph_SaveLoadInt logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_210 = new SubGraph_SaveLoadInt();

	private int logic_SubGraph_SaveLoadInt_restartValue_210 = 1;

	private int logic_SubGraph_SaveLoadInt_integer_210;

	private object logic_SubGraph_SaveLoadInt_intAsVariable_210 = "";

	private string logic_SubGraph_SaveLoadInt_uniqueID_210 = "Stage2";

	private SubGraph_SaveLoadInt logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_212 = new SubGraph_SaveLoadInt();

	private int logic_SubGraph_SaveLoadInt_restartValue_212;

	private int logic_SubGraph_SaveLoadInt_integer_212;

	private object logic_SubGraph_SaveLoadInt_intAsVariable_212 = "";

	private string logic_SubGraph_SaveLoadInt_uniqueID_212 = "Stage2";

	private uScriptCon_CompareInt logic_uScriptCon_CompareInt_uScriptCon_CompareInt_214 = new uScriptCon_CompareInt();

	private int logic_uScriptCon_CompareInt_A_214;

	private int logic_uScriptCon_CompareInt_B_214 = 1;

	private bool logic_uScriptCon_CompareInt_GreaterThan_214 = true;

	private bool logic_uScriptCon_CompareInt_GreaterThanOrEqualTo_214 = true;

	private bool logic_uScriptCon_CompareInt_EqualTo_214 = true;

	private bool logic_uScriptCon_CompareInt_NotEqualTo_214 = true;

	private bool logic_uScriptCon_CompareInt_LessThanOrEqualTo_214 = true;

	private bool logic_uScriptCon_CompareInt_LessThan_214 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_216 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_216;

	private int logic_uScriptAct_SetInt_Target_216;

	private bool logic_uScriptAct_SetInt_Out_216 = true;

	private uScriptCon_CompareInt logic_uScriptCon_CompareInt_uScriptCon_CompareInt_220 = new uScriptCon_CompareInt();

	private int logic_uScriptCon_CompareInt_A_220;

	private int logic_uScriptCon_CompareInt_B_220 = 2;

	private bool logic_uScriptCon_CompareInt_GreaterThan_220 = true;

	private bool logic_uScriptCon_CompareInt_GreaterThanOrEqualTo_220 = true;

	private bool logic_uScriptCon_CompareInt_EqualTo_220 = true;

	private bool logic_uScriptCon_CompareInt_NotEqualTo_220 = true;

	private bool logic_uScriptCon_CompareInt_LessThanOrEqualTo_220 = true;

	private bool logic_uScriptCon_CompareInt_LessThan_220 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_221 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_221;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_221;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_221;

	private bool logic_uScript_AddMessage_Out_221 = true;

	private bool logic_uScript_AddMessage_Shown_221 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_222 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_222;

	private bool logic_uScriptAct_SetBool_Out_222 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_222 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_222 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_223 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_223;

	private bool logic_uScriptCon_CompareBool_True_223 = true;

	private bool logic_uScriptCon_CompareBool_False_223 = true;

	private uScriptAct_AddInt_v2 logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_228 = new uScriptAct_AddInt_v2();

	private int logic_uScriptAct_AddInt_v2_A_228;

	private int logic_uScriptAct_AddInt_v2_B_228 = 1;

	private int logic_uScriptAct_AddInt_v2_IntResult_228;

	private float logic_uScriptAct_AddInt_v2_FloatResult_228;

	private bool logic_uScriptAct_AddInt_v2_Out_228 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_230 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_230;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_230 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_230 = "msgAllBlocksCraftedShown";

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_233 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_233 = 1;

	private int logic_uScriptAct_SetInt_Target_233;

	private bool logic_uScriptAct_SetInt_Out_233 = true;

	private uScript_DirectEnemiesOutOfEncounter logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_514 = new uScript_DirectEnemiesOutOfEncounter();

	private GameObject logic_uScript_DirectEnemiesOutOfEncounter_ownerNode_514;

	private bool logic_uScript_DirectEnemiesOutOfEncounter_Out_514 = true;

	private BlockTypes event_UnityEngine_GameObject_BlockType_173;

	private int event_UnityEngine_GameObject_BlockTypeTotal_173;

	private int event_UnityEngine_GameObject_BlockTotal_173;

	private TankBlock event_UnityEngine_GameObject_Block_173;

	private TankBlock event_UnityEngine_GameObject_CrafterBlock_173;

	private void SyncUnityHooks()
	{
		SyncEventListeners();
		if (null == owner_Connection_14 || !m_RegisteredForEvents)
		{
			owner_Connection_14 = parentGameObject;
			if (null != owner_Connection_14)
			{
				uScript_SaveLoad uScript_SaveLoad2 = owner_Connection_14.GetComponent<uScript_SaveLoad>();
				if (null == uScript_SaveLoad2)
				{
					uScript_SaveLoad2 = owner_Connection_14.AddComponent<uScript_SaveLoad>();
				}
				if (null != uScript_SaveLoad2)
				{
					uScript_SaveLoad2.SaveEvent += Instance_SaveEvent_24;
					uScript_SaveLoad2.LoadEvent += Instance_LoadEvent_24;
					uScript_SaveLoad2.RestartEvent += Instance_RestartEvent_24;
				}
			}
		}
		if (null == owner_Connection_22 || !m_RegisteredForEvents)
		{
			owner_Connection_22 = parentGameObject;
		}
		if (null == owner_Connection_81 || !m_RegisteredForEvents)
		{
			owner_Connection_81 = parentGameObject;
		}
		if (null == owner_Connection_85 || !m_RegisteredForEvents)
		{
			owner_Connection_85 = parentGameObject;
		}
		if (null == owner_Connection_94 || !m_RegisteredForEvents)
		{
			owner_Connection_94 = parentGameObject;
		}
		if (null == owner_Connection_101 || !m_RegisteredForEvents)
		{
			owner_Connection_101 = parentGameObject;
			if (null != owner_Connection_101)
			{
				uScript_EncounterUpdate uScript_EncounterUpdate2 = owner_Connection_101.GetComponent<uScript_EncounterUpdate>();
				if (null == uScript_EncounterUpdate2)
				{
					uScript_EncounterUpdate2 = owner_Connection_101.AddComponent<uScript_EncounterUpdate>();
				}
				if (null != uScript_EncounterUpdate2)
				{
					uScript_EncounterUpdate2.OnUpdate += Instance_OnUpdate_84;
					uScript_EncounterUpdate2.OnSuspend += Instance_OnSuspend_84;
					uScript_EncounterUpdate2.OnResume += Instance_OnResume_84;
				}
			}
		}
		if (null == owner_Connection_110 || !m_RegisteredForEvents)
		{
			owner_Connection_110 = parentGameObject;
		}
		if (null == owner_Connection_115 || !m_RegisteredForEvents)
		{
			owner_Connection_115 = parentGameObject;
		}
		if (null == owner_Connection_137 || !m_RegisteredForEvents)
		{
			owner_Connection_137 = parentGameObject;
		}
		if (null == owner_Connection_142 || !m_RegisteredForEvents)
		{
			owner_Connection_142 = parentGameObject;
		}
		if (null == owner_Connection_151 || !m_RegisteredForEvents)
		{
			owner_Connection_151 = parentGameObject;
		}
		if (null == owner_Connection_156 || !m_RegisteredForEvents)
		{
			owner_Connection_156 = parentGameObject;
		}
		if (null == owner_Connection_176 || !m_RegisteredForEvents)
		{
			owner_Connection_176 = parentGameObject;
			if (null != owner_Connection_176)
			{
				uScript_BlockScrappedEvent uScript_BlockScrappedEvent2 = owner_Connection_176.GetComponent<uScript_BlockScrappedEvent>();
				if (null == uScript_BlockScrappedEvent2)
				{
					uScript_BlockScrappedEvent2 = owner_Connection_176.AddComponent<uScript_BlockScrappedEvent>();
				}
				if (null != uScript_BlockScrappedEvent2)
				{
					uScript_BlockScrappedEvent2.BlockScrappedEvent += Instance_BlockScrappedEvent_173;
				}
			}
		}
		if (null == owner_Connection_186 || !m_RegisteredForEvents)
		{
			owner_Connection_186 = parentGameObject;
		}
		if (null == owner_Connection_190 || !m_RegisteredForEvents)
		{
			owner_Connection_190 = parentGameObject;
		}
		if (null == owner_Connection_205 || !m_RegisteredForEvents)
		{
			owner_Connection_205 = parentGameObject;
		}
		if (null == owner_Connection_515 || !m_RegisteredForEvents)
		{
			owner_Connection_515 = parentGameObject;
		}
	}

	private void RegisterForUnityHooks()
	{
		SyncEventListeners();
		if (!m_RegisteredForEvents && null != owner_Connection_14)
		{
			uScript_SaveLoad uScript_SaveLoad2 = owner_Connection_14.GetComponent<uScript_SaveLoad>();
			if (null == uScript_SaveLoad2)
			{
				uScript_SaveLoad2 = owner_Connection_14.AddComponent<uScript_SaveLoad>();
			}
			if (null != uScript_SaveLoad2)
			{
				uScript_SaveLoad2.SaveEvent += Instance_SaveEvent_24;
				uScript_SaveLoad2.LoadEvent += Instance_LoadEvent_24;
				uScript_SaveLoad2.RestartEvent += Instance_RestartEvent_24;
			}
		}
		if (!m_RegisteredForEvents && null != owner_Connection_101)
		{
			uScript_EncounterUpdate uScript_EncounterUpdate2 = owner_Connection_101.GetComponent<uScript_EncounterUpdate>();
			if (null == uScript_EncounterUpdate2)
			{
				uScript_EncounterUpdate2 = owner_Connection_101.AddComponent<uScript_EncounterUpdate>();
			}
			if (null != uScript_EncounterUpdate2)
			{
				uScript_EncounterUpdate2.OnUpdate += Instance_OnUpdate_84;
				uScript_EncounterUpdate2.OnSuspend += Instance_OnSuspend_84;
				uScript_EncounterUpdate2.OnResume += Instance_OnResume_84;
			}
		}
		if (!m_RegisteredForEvents && null != owner_Connection_176)
		{
			uScript_BlockScrappedEvent uScript_BlockScrappedEvent2 = owner_Connection_176.GetComponent<uScript_BlockScrappedEvent>();
			if (null == uScript_BlockScrappedEvent2)
			{
				uScript_BlockScrappedEvent2 = owner_Connection_176.AddComponent<uScript_BlockScrappedEvent>();
			}
			if (null != uScript_BlockScrappedEvent2)
			{
				uScript_BlockScrappedEvent2.BlockScrappedEvent += Instance_BlockScrappedEvent_173;
			}
		}
	}

	private void SyncEventListeners()
	{
	}

	private void UnregisterEventListeners()
	{
		if (null != owner_Connection_14)
		{
			uScript_SaveLoad component = owner_Connection_14.GetComponent<uScript_SaveLoad>();
			if (null != component)
			{
				component.SaveEvent -= Instance_SaveEvent_24;
				component.LoadEvent -= Instance_LoadEvent_24;
				component.RestartEvent -= Instance_RestartEvent_24;
			}
		}
		if (null != owner_Connection_101)
		{
			uScript_EncounterUpdate component2 = owner_Connection_101.GetComponent<uScript_EncounterUpdate>();
			if (null != component2)
			{
				component2.OnUpdate -= Instance_OnUpdate_84;
				component2.OnSuspend -= Instance_OnSuspend_84;
				component2.OnResume -= Instance_OnResume_84;
			}
		}
		if (null != owner_Connection_176)
		{
			uScript_BlockScrappedEvent component3 = owner_Connection_176.GetComponent<uScript_BlockScrappedEvent>();
			if (null != component3)
			{
				component3.BlockScrappedEvent -= Instance_BlockScrappedEvent_173;
			}
		}
	}

	public override void SetParent(GameObject g)
	{
		parentGameObject = g;
		logic_uScriptAct_SetBool_uScriptAct_SetBool_0.SetParent(g);
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_2.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_4.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_5.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_9.SetParent(g);
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_12.SetParent(g);
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_18.SetParent(g);
		logic_uScript_HideArrow_uScript_HideArrow_20.SetParent(g);
		logic_uScript_PausePopulation_uScript_PausePopulation_21.SetParent(g);
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_23.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_28.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_30.SetParent(g);
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_31.SetParent(g);
		logic_uScript_PausePopulation_uScript_PausePopulation_33.SetParent(g);
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_34.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_35.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_36.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_37.SetParent(g);
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_41.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_44.SetParent(g);
		logic_uScript_LockTechInteraction_uScript_LockTechInteraction_45.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_47.SetParent(g);
		logic_uScript_LockTechStacks_uScript_LockTechStacks_48.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_54.SetParent(g);
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_57.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_58.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_59.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_60.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_61.SetParent(g);
		logic_uScriptAct_Concatenate_uScriptAct_Concatenate_62.SetParent(g);
		logic_uScriptAct_PrintText_uScriptAct_PrintText_66.SetParent(g);
		logic_uScript_LockBlock_uScript_LockBlock_67.SetParent(g);
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_70.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_71.SetParent(g);
		logic_uScript_SetCustomRadarTeamID_uScript_SetCustomRadarTeamID_77.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_78.SetParent(g);
		logic_uScript_LockTech_uScript_LockTech_79.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_83.SetParent(g);
		logic_uScript_SetTankHideBlockLimit_uScript_SetTankHideBlockLimit_86.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_87.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_89.SetParent(g);
		logic_uScript_RemoveScenery_uScript_RemoveScenery_91.SetParent(g);
		logic_uScript_SetCustomRadarTeamID_uScript_SetCustomRadarTeamID_93.SetParent(g);
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_96.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_99.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_100.SetParent(g);
		logic_uScript_LockTech_uScript_LockTech_102.SetParent(g);
		logic_uScript_LockTechInteraction_uScript_LockTechInteraction_103.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_105.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_109.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_111.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_112.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_113.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_114.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_117.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_118.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_121.SetParent(g);
		logic_uScript_GetTankBlock_uScript_GetTankBlock_123.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_127.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_128.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_129.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_131.SetParent(g);
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_140.SetParent(g);
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_143.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_144.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_146.SetParent(g);
		logic_uScript_SpawnVFX_uScript_SpawnVFX_148.SetParent(g);
		logic_uScript_FinishEncounter_uScript_FinishEncounter_157.SetParent(g);
		logic_uScript_RemoveTech_uScript_RemoveTech_160.SetParent(g);
		logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_161.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_162.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_164.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_170.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_172.SetParent(g);
		logic_uScript_CompareBlock_uScript_CompareBlock_175.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_180.SetParent(g);
		logic_uScript_SetQuestObjectiveCount_uScript_SetQuestObjectiveCount_182.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_183.SetParent(g);
		logic_uScript_HideArrow_uScript_HideArrow_184.SetParent(g);
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_187.SetParent(g);
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_189.SetParent(g);
		logic_uScript_SetQuestObjectiveCount_uScript_SetQuestObjectiveCount_191.SetParent(g);
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_194.SetParent(g);
		logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_196.SetParent(g);
		logic_uScript_EnableGlow_uScript_EnableGlow_201.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_202.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_204.SetParent(g);
		logic_uScript_SetQuestObjectiveCount_uScript_SetQuestObjectiveCount_208.SetParent(g);
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_210.SetParent(g);
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_212.SetParent(g);
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_214.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_216.SetParent(g);
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_220.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_221.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_222.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_223.SetParent(g);
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_228.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_230.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_233.SetParent(g);
		logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_514.SetParent(g);
		owner_Connection_14 = parentGameObject;
		owner_Connection_22 = parentGameObject;
		owner_Connection_81 = parentGameObject;
		owner_Connection_85 = parentGameObject;
		owner_Connection_94 = parentGameObject;
		owner_Connection_101 = parentGameObject;
		owner_Connection_110 = parentGameObject;
		owner_Connection_115 = parentGameObject;
		owner_Connection_137 = parentGameObject;
		owner_Connection_142 = parentGameObject;
		owner_Connection_151 = parentGameObject;
		owner_Connection_156 = parentGameObject;
		owner_Connection_176 = parentGameObject;
		owner_Connection_186 = parentGameObject;
		owner_Connection_190 = parentGameObject;
		owner_Connection_205 = parentGameObject;
		owner_Connection_515 = parentGameObject;
	}

	public void Awake()
	{
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_9.Awake();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_12.Awake();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_18.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_28.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_30.Awake();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_41.Awake();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_57.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_59.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_164.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_170.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_172.Awake();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_210.Awake();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_212.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_230.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7.Save_Out += SubGraph_SaveLoadBool_Save_Out_7;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7.Load_Out += SubGraph_SaveLoadBool_Load_Out_7;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_7;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_9.Save_Out += SubGraph_SaveLoadBool_Save_Out_9;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_9.Load_Out += SubGraph_SaveLoadBool_Load_Out_9;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_9.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_9;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_12.Out += SubGraph_CompleteObjectiveStage_Out_12;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_18.Save_Out += SubGraph_SaveLoadInt_Save_Out_18;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_18.Load_Out += SubGraph_SaveLoadInt_Load_Out_18;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_18.Restart_Out += SubGraph_SaveLoadInt_Restart_Out_18;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_23.Output1 += uScriptCon_ManualSwitch_Output1_23;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_23.Output2 += uScriptCon_ManualSwitch_Output2_23;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_23.Output3 += uScriptCon_ManualSwitch_Output3_23;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_23.Output4 += uScriptCon_ManualSwitch_Output4_23;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_23.Output5 += uScriptCon_ManualSwitch_Output5_23;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_23.Output6 += uScriptCon_ManualSwitch_Output6_23;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_23.Output7 += uScriptCon_ManualSwitch_Output7_23;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_23.Output8 += uScriptCon_ManualSwitch_Output8_23;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_28.Save_Out += SubGraph_SaveLoadBool_Save_Out_28;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_28.Load_Out += SubGraph_SaveLoadBool_Load_Out_28;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_28.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_28;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_30.Save_Out += SubGraph_SaveLoadBool_Save_Out_30;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_30.Load_Out += SubGraph_SaveLoadBool_Load_Out_30;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_30.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_30;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_41.Out += SubGraph_CompleteObjectiveStage_Out_41;
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_57.Out += SubGraph_LoadObjectiveStates_Out_57;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_59.Save_Out += SubGraph_SaveLoadBool_Save_Out_59;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_59.Load_Out += SubGraph_SaveLoadBool_Load_Out_59;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_59.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_59;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_140.Output1 += uScriptCon_ManualSwitch_Output1_140;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_140.Output2 += uScriptCon_ManualSwitch_Output2_140;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_140.Output3 += uScriptCon_ManualSwitch_Output3_140;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_140.Output4 += uScriptCon_ManualSwitch_Output4_140;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_140.Output5 += uScriptCon_ManualSwitch_Output5_140;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_140.Output6 += uScriptCon_ManualSwitch_Output6_140;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_140.Output7 += uScriptCon_ManualSwitch_Output7_140;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_140.Output8 += uScriptCon_ManualSwitch_Output8_140;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_164.Save_Out += SubGraph_SaveLoadBool_Save_Out_164;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_164.Load_Out += SubGraph_SaveLoadBool_Load_Out_164;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_164.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_164;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_170.Save_Out += SubGraph_SaveLoadBool_Save_Out_170;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_170.Load_Out += SubGraph_SaveLoadBool_Load_Out_170;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_170.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_170;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_172.Save_Out += SubGraph_SaveLoadBool_Save_Out_172;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_172.Load_Out += SubGraph_SaveLoadBool_Load_Out_172;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_172.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_172;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_210.Save_Out += SubGraph_SaveLoadInt_Save_Out_210;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_210.Load_Out += SubGraph_SaveLoadInt_Load_Out_210;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_210.Restart_Out += SubGraph_SaveLoadInt_Restart_Out_210;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_212.Save_Out += SubGraph_SaveLoadInt_Save_Out_212;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_212.Load_Out += SubGraph_SaveLoadInt_Load_Out_212;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_212.Restart_Out += SubGraph_SaveLoadInt_Restart_Out_212;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_230.Save_Out += SubGraph_SaveLoadBool_Save_Out_230;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_230.Load_Out += SubGraph_SaveLoadBool_Load_Out_230;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_230.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_230;
	}

	public void Start()
	{
		SyncUnityHooks();
		m_RegisteredForEvents = true;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_9.Start();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_12.Start();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_18.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_28.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_30.Start();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_41.Start();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_57.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_59.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_164.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_170.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_172.Start();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_210.Start();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_212.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_230.Start();
	}

	public void OnEnable()
	{
		RegisterForUnityHooks();
		m_RegisteredForEvents = true;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_9.OnEnable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_12.OnEnable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_18.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_28.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_30.OnEnable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_41.OnEnable();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_57.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_59.OnEnable();
		logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_161.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_164.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_170.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_172.OnEnable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_210.OnEnable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_212.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_230.OnEnable();
		logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_514.OnEnable();
	}

	public void OnDisable()
	{
		logic_uScript_AddMessage_uScript_AddMessage_4.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_9.OnDisable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_12.OnDisable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_18.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_28.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_30.OnDisable();
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_31.OnDisable();
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_34.OnDisable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_41.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_54.OnDisable();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_57.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_59.OnDisable();
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_70.OnDisable();
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_96.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_118.OnDisable();
		logic_uScript_GetTankBlock_uScript_GetTankBlock_123.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_127.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_162.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_164.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_170.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_172.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_202.OnDisable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_210.OnDisable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_212.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_221.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_230.OnDisable();
		UnregisterEventListeners();
		m_RegisteredForEvents = false;
	}

	public void Update()
	{
		SyncEventListeners();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_9.Update();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_12.Update();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_18.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_28.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_30.Update();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_41.Update();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_57.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_59.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_164.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_170.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_172.Update();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_210.Update();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_212.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_230.Update();
	}

	public void OnDestroy()
	{
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_9.OnDestroy();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_12.OnDestroy();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_18.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_28.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_30.OnDestroy();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_41.OnDestroy();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_57.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_59.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_164.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_170.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_172.OnDestroy();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_210.OnDestroy();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_212.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_230.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7.Save_Out -= SubGraph_SaveLoadBool_Save_Out_7;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7.Load_Out -= SubGraph_SaveLoadBool_Load_Out_7;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_7;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_9.Save_Out -= SubGraph_SaveLoadBool_Save_Out_9;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_9.Load_Out -= SubGraph_SaveLoadBool_Load_Out_9;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_9.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_9;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_12.Out -= SubGraph_CompleteObjectiveStage_Out_12;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_18.Save_Out -= SubGraph_SaveLoadInt_Save_Out_18;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_18.Load_Out -= SubGraph_SaveLoadInt_Load_Out_18;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_18.Restart_Out -= SubGraph_SaveLoadInt_Restart_Out_18;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_23.Output1 -= uScriptCon_ManualSwitch_Output1_23;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_23.Output2 -= uScriptCon_ManualSwitch_Output2_23;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_23.Output3 -= uScriptCon_ManualSwitch_Output3_23;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_23.Output4 -= uScriptCon_ManualSwitch_Output4_23;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_23.Output5 -= uScriptCon_ManualSwitch_Output5_23;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_23.Output6 -= uScriptCon_ManualSwitch_Output6_23;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_23.Output7 -= uScriptCon_ManualSwitch_Output7_23;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_23.Output8 -= uScriptCon_ManualSwitch_Output8_23;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_28.Save_Out -= SubGraph_SaveLoadBool_Save_Out_28;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_28.Load_Out -= SubGraph_SaveLoadBool_Load_Out_28;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_28.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_28;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_30.Save_Out -= SubGraph_SaveLoadBool_Save_Out_30;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_30.Load_Out -= SubGraph_SaveLoadBool_Load_Out_30;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_30.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_30;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_41.Out -= SubGraph_CompleteObjectiveStage_Out_41;
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_57.Out -= SubGraph_LoadObjectiveStates_Out_57;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_59.Save_Out -= SubGraph_SaveLoadBool_Save_Out_59;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_59.Load_Out -= SubGraph_SaveLoadBool_Load_Out_59;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_59.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_59;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_140.Output1 -= uScriptCon_ManualSwitch_Output1_140;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_140.Output2 -= uScriptCon_ManualSwitch_Output2_140;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_140.Output3 -= uScriptCon_ManualSwitch_Output3_140;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_140.Output4 -= uScriptCon_ManualSwitch_Output4_140;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_140.Output5 -= uScriptCon_ManualSwitch_Output5_140;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_140.Output6 -= uScriptCon_ManualSwitch_Output6_140;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_140.Output7 -= uScriptCon_ManualSwitch_Output7_140;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_140.Output8 -= uScriptCon_ManualSwitch_Output8_140;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_164.Save_Out -= SubGraph_SaveLoadBool_Save_Out_164;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_164.Load_Out -= SubGraph_SaveLoadBool_Load_Out_164;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_164.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_164;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_170.Save_Out -= SubGraph_SaveLoadBool_Save_Out_170;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_170.Load_Out -= SubGraph_SaveLoadBool_Load_Out_170;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_170.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_170;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_172.Save_Out -= SubGraph_SaveLoadBool_Save_Out_172;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_172.Load_Out -= SubGraph_SaveLoadBool_Load_Out_172;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_172.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_172;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_210.Save_Out -= SubGraph_SaveLoadInt_Save_Out_210;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_210.Load_Out -= SubGraph_SaveLoadInt_Load_Out_210;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_210.Restart_Out -= SubGraph_SaveLoadInt_Restart_Out_210;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_212.Save_Out -= SubGraph_SaveLoadInt_Save_Out_212;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_212.Load_Out -= SubGraph_SaveLoadInt_Load_Out_212;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_212.Restart_Out -= SubGraph_SaveLoadInt_Restart_Out_212;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_230.Save_Out -= SubGraph_SaveLoadBool_Save_Out_230;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_230.Load_Out -= SubGraph_SaveLoadBool_Load_Out_230;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_230.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_230;
	}

	public void OnGUI()
	{
		logic_uScriptAct_PrintText_uScriptAct_PrintText_66.OnGUI();
	}

	private void Instance_SaveEvent_24(object o, EventArgs e)
	{
		Relay_SaveEvent_24();
	}

	private void Instance_LoadEvent_24(object o, EventArgs e)
	{
		Relay_LoadEvent_24();
	}

	private void Instance_RestartEvent_24(object o, EventArgs e)
	{
		Relay_RestartEvent_24();
	}

	private void Instance_OnUpdate_84(object o, EventArgs e)
	{
		Relay_OnUpdate_84();
	}

	private void Instance_OnSuspend_84(object o, EventArgs e)
	{
		Relay_OnSuspend_84();
	}

	private void Instance_OnResume_84(object o, EventArgs e)
	{
		Relay_OnResume_84();
	}

	private void Instance_BlockScrappedEvent_173(object o, uScript_BlockScrappedEvent.BlockScrappedEventArgs e)
	{
		event_UnityEngine_GameObject_BlockType_173 = e.BlockType;
		event_UnityEngine_GameObject_BlockTypeTotal_173 = e.BlockTypeTotal;
		event_UnityEngine_GameObject_BlockTotal_173 = e.BlockTotal;
		event_UnityEngine_GameObject_Block_173 = e.Block;
		event_UnityEngine_GameObject_CrafterBlock_173 = e.CrafterBlock;
		Relay_BlockScrappedEvent_173();
	}

	private void SubGraph_SaveLoadBool_Save_Out_7(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_7 = e.boolean;
		local_msgIntroShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_7;
		Relay_Save_Out_7();
	}

	private void SubGraph_SaveLoadBool_Load_Out_7(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_7 = e.boolean;
		local_msgIntroShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_7;
		Relay_Load_Out_7();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_7(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_7 = e.boolean;
		local_msgIntroShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_7;
		Relay_Restart_Out_7();
	}

	private void SubGraph_SaveLoadBool_Save_Out_9(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_9 = e.boolean;
		local_Initialize_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_9;
		Relay_Save_Out_9();
	}

	private void SubGraph_SaveLoadBool_Load_Out_9(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_9 = e.boolean;
		local_Initialize_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_9;
		Relay_Load_Out_9();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_9(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_9 = e.boolean;
		local_Initialize_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_9;
		Relay_Restart_Out_9();
	}

	private void SubGraph_CompleteObjectiveStage_Out_12(object o, SubGraph_CompleteObjectiveStage.LogicEventArgs e)
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_12 = e.objectiveStage;
		local_Stage_System_Int32 = logic_SubGraph_CompleteObjectiveStage_objectiveStage_12;
		Relay_Out_12();
	}

	private void SubGraph_SaveLoadInt_Save_Out_18(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_18 = e.integer;
		local_Stage_System_Int32 = logic_SubGraph_SaveLoadInt_integer_18;
		Relay_Save_Out_18();
	}

	private void SubGraph_SaveLoadInt_Load_Out_18(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_18 = e.integer;
		local_Stage_System_Int32 = logic_SubGraph_SaveLoadInt_integer_18;
		Relay_Load_Out_18();
	}

	private void SubGraph_SaveLoadInt_Restart_Out_18(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_18 = e.integer;
		local_Stage_System_Int32 = logic_SubGraph_SaveLoadInt_integer_18;
		Relay_Restart_Out_18();
	}

	private void uScriptCon_ManualSwitch_Output1_23(object o, EventArgs e)
	{
		Relay_Output1_23();
	}

	private void uScriptCon_ManualSwitch_Output2_23(object o, EventArgs e)
	{
		Relay_Output2_23();
	}

	private void uScriptCon_ManualSwitch_Output3_23(object o, EventArgs e)
	{
		Relay_Output3_23();
	}

	private void uScriptCon_ManualSwitch_Output4_23(object o, EventArgs e)
	{
		Relay_Output4_23();
	}

	private void uScriptCon_ManualSwitch_Output5_23(object o, EventArgs e)
	{
		Relay_Output5_23();
	}

	private void uScriptCon_ManualSwitch_Output6_23(object o, EventArgs e)
	{
		Relay_Output6_23();
	}

	private void uScriptCon_ManualSwitch_Output7_23(object o, EventArgs e)
	{
		Relay_Output7_23();
	}

	private void uScriptCon_ManualSwitch_Output8_23(object o, EventArgs e)
	{
		Relay_Output8_23();
	}

	private void SubGraph_SaveLoadBool_Save_Out_28(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_28 = e.boolean;
		local_msgBaseFoundShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_28;
		Relay_Save_Out_28();
	}

	private void SubGraph_SaveLoadBool_Load_Out_28(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_28 = e.boolean;
		local_msgBaseFoundShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_28;
		Relay_Load_Out_28();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_28(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_28 = e.boolean;
		local_msgBaseFoundShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_28;
		Relay_Restart_Out_28();
	}

	private void SubGraph_SaveLoadBool_Save_Out_30(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_30 = e.boolean;
		local_ScrappingInProgress_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_30;
		Relay_Save_Out_30();
	}

	private void SubGraph_SaveLoadBool_Load_Out_30(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_30 = e.boolean;
		local_ScrappingInProgress_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_30;
		Relay_Load_Out_30();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_30(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_30 = e.boolean;
		local_ScrappingInProgress_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_30;
		Relay_Restart_Out_30();
	}

	private void SubGraph_CompleteObjectiveStage_Out_41(object o, SubGraph_CompleteObjectiveStage.LogicEventArgs e)
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_41 = e.objectiveStage;
		local_Stage_System_Int32 = logic_SubGraph_CompleteObjectiveStage_objectiveStage_41;
		Relay_Out_41();
	}

	private void SubGraph_LoadObjectiveStates_Out_57(object o, SubGraph_LoadObjectiveStates.LogicEventArgs e)
	{
		Relay_Out_57();
	}

	private void SubGraph_SaveLoadBool_Save_Out_59(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_59 = e.boolean;
		local_CanSuckUpBlocks_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_59;
		Relay_Save_Out_59();
	}

	private void SubGraph_SaveLoadBool_Load_Out_59(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_59 = e.boolean;
		local_CanSuckUpBlocks_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_59;
		Relay_Load_Out_59();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_59(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_59 = e.boolean;
		local_CanSuckUpBlocks_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_59;
		Relay_Restart_Out_59();
	}

	private void uScriptCon_ManualSwitch_Output1_140(object o, EventArgs e)
	{
		Relay_Output1_140();
	}

	private void uScriptCon_ManualSwitch_Output2_140(object o, EventArgs e)
	{
		Relay_Output2_140();
	}

	private void uScriptCon_ManualSwitch_Output3_140(object o, EventArgs e)
	{
		Relay_Output3_140();
	}

	private void uScriptCon_ManualSwitch_Output4_140(object o, EventArgs e)
	{
		Relay_Output4_140();
	}

	private void uScriptCon_ManualSwitch_Output5_140(object o, EventArgs e)
	{
		Relay_Output5_140();
	}

	private void uScriptCon_ManualSwitch_Output6_140(object o, EventArgs e)
	{
		Relay_Output6_140();
	}

	private void uScriptCon_ManualSwitch_Output7_140(object o, EventArgs e)
	{
		Relay_Output7_140();
	}

	private void uScriptCon_ManualSwitch_Output8_140(object o, EventArgs e)
	{
		Relay_Output8_140();
	}

	private void SubGraph_SaveLoadBool_Save_Out_164(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_164 = e.boolean;
		local_AllBlocksScrapped_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_164;
		Relay_Save_Out_164();
	}

	private void SubGraph_SaveLoadBool_Load_Out_164(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_164 = e.boolean;
		local_AllBlocksScrapped_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_164;
		Relay_Load_Out_164();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_164(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_164 = e.boolean;
		local_AllBlocksScrapped_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_164;
		Relay_Restart_Out_164();
	}

	private void SubGraph_SaveLoadBool_Save_Out_170(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_170 = e.boolean;
		local_EnemyAlive_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_170;
		Relay_Save_Out_170();
	}

	private void SubGraph_SaveLoadBool_Load_Out_170(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_170 = e.boolean;
		local_EnemyAlive_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_170;
		Relay_Load_Out_170();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_170(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_170 = e.boolean;
		local_EnemyAlive_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_170;
		Relay_Restart_Out_170();
	}

	private void SubGraph_SaveLoadBool_Save_Out_172(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_172 = e.boolean;
		local_AllBlocksScrapped_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_172;
		Relay_Save_Out_172();
	}

	private void SubGraph_SaveLoadBool_Load_Out_172(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_172 = e.boolean;
		local_AllBlocksScrapped_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_172;
		Relay_Load_Out_172();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_172(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_172 = e.boolean;
		local_AllBlocksScrapped_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_172;
		Relay_Restart_Out_172();
	}

	private void SubGraph_SaveLoadInt_Save_Out_210(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_210 = e.integer;
		local_Stage2_System_Int32 = logic_SubGraph_SaveLoadInt_integer_210;
		Relay_Save_Out_210();
	}

	private void SubGraph_SaveLoadInt_Load_Out_210(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_210 = e.integer;
		local_Stage2_System_Int32 = logic_SubGraph_SaveLoadInt_integer_210;
		Relay_Load_Out_210();
	}

	private void SubGraph_SaveLoadInt_Restart_Out_210(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_210 = e.integer;
		local_Stage2_System_Int32 = logic_SubGraph_SaveLoadInt_integer_210;
		Relay_Restart_Out_210();
	}

	private void SubGraph_SaveLoadInt_Save_Out_212(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_212 = e.integer;
		local_CurrentAmountType_System_Int32 = logic_SubGraph_SaveLoadInt_integer_212;
		Relay_Save_Out_212();
	}

	private void SubGraph_SaveLoadInt_Load_Out_212(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_212 = e.integer;
		local_CurrentAmountType_System_Int32 = logic_SubGraph_SaveLoadInt_integer_212;
		Relay_Load_Out_212();
	}

	private void SubGraph_SaveLoadInt_Restart_Out_212(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_212 = e.integer;
		local_CurrentAmountType_System_Int32 = logic_SubGraph_SaveLoadInt_integer_212;
		Relay_Restart_Out_212();
	}

	private void SubGraph_SaveLoadBool_Save_Out_230(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_230 = e.boolean;
		local_msgAllBlocksCraftedShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_230;
		Relay_Save_Out_230();
	}

	private void SubGraph_SaveLoadBool_Load_Out_230(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_230 = e.boolean;
		local_msgAllBlocksCraftedShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_230;
		Relay_Load_Out_230();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_230(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_230 = e.boolean;
		local_msgAllBlocksCraftedShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_230;
		Relay_Restart_Out_230();
	}

	private void Relay_True_0()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_0.True(out logic_uScriptAct_SetBool_Target_0);
		local_NearBase_System_Boolean = logic_uScriptAct_SetBool_Target_0;
	}

	private void Relay_False_0()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_0.False(out logic_uScriptAct_SetBool_Target_0);
		local_NearBase_System_Boolean = logic_uScriptAct_SetBool_Target_0;
	}

	private void Relay_In_2()
	{
		logic_uScript_SetEncounterTarget_owner_2 = owner_Connection_22;
		logic_uScript_SetEncounterTarget_visibleObject_2 = local_NPCTech_Tank;
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_2.In(logic_uScript_SetEncounterTarget_owner_2, logic_uScript_SetEncounterTarget_visibleObject_2);
		if (logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_2.Out)
		{
			Relay_In_31();
		}
	}

	private void Relay_In_4()
	{
		logic_uScript_AddMessage_messageData_4 = msgLeavingMissionArea;
		logic_uScript_AddMessage_speaker_4 = messageSpeaker;
		logic_uScript_AddMessage_Return_4 = logic_uScript_AddMessage_uScript_AddMessage_4.In(logic_uScript_AddMessage_messageData_4, logic_uScript_AddMessage_speaker_4);
		if (logic_uScript_AddMessage_uScript_AddMessage_4.Out)
		{
			Relay_False_0();
		}
	}

	private void Relay_In_5()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_5 = messageTag;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_5.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_5, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_5);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_5.Out)
		{
			Relay_In_20();
		}
	}

	private void Relay_Save_Out_7()
	{
		Relay_Save_230();
	}

	private void Relay_Load_Out_7()
	{
		Relay_Load_230();
	}

	private void Relay_Restart_Out_7()
	{
		Relay_Set_False_230();
	}

	private void Relay_Save_7()
	{
		logic_SubGraph_SaveLoadBool_boolean_7 = local_msgIntroShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_7 = local_msgIntroShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7.Save(ref logic_SubGraph_SaveLoadBool_boolean_7, logic_SubGraph_SaveLoadBool_boolAsVariable_7, logic_SubGraph_SaveLoadBool_uniqueID_7);
	}

	private void Relay_Load_7()
	{
		logic_SubGraph_SaveLoadBool_boolean_7 = local_msgIntroShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_7 = local_msgIntroShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7.Load(ref logic_SubGraph_SaveLoadBool_boolean_7, logic_SubGraph_SaveLoadBool_boolAsVariable_7, logic_SubGraph_SaveLoadBool_uniqueID_7);
	}

	private void Relay_Set_True_7()
	{
		logic_SubGraph_SaveLoadBool_boolean_7 = local_msgIntroShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_7 = local_msgIntroShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_7, logic_SubGraph_SaveLoadBool_boolAsVariable_7, logic_SubGraph_SaveLoadBool_uniqueID_7);
	}

	private void Relay_Set_False_7()
	{
		logic_SubGraph_SaveLoadBool_boolean_7 = local_msgIntroShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_7 = local_msgIntroShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_7.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_7, logic_SubGraph_SaveLoadBool_boolAsVariable_7, logic_SubGraph_SaveLoadBool_uniqueID_7);
	}

	private void Relay_Save_Out_9()
	{
		Relay_Save_28();
	}

	private void Relay_Load_Out_9()
	{
		Relay_Load_28();
	}

	private void Relay_Restart_Out_9()
	{
		Relay_Set_False_28();
	}

	private void Relay_Save_9()
	{
		logic_SubGraph_SaveLoadBool_boolean_9 = local_Initialize_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_9 = local_Initialize_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_9.Save(ref logic_SubGraph_SaveLoadBool_boolean_9, logic_SubGraph_SaveLoadBool_boolAsVariable_9, logic_SubGraph_SaveLoadBool_uniqueID_9);
	}

	private void Relay_Load_9()
	{
		logic_SubGraph_SaveLoadBool_boolean_9 = local_Initialize_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_9 = local_Initialize_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_9.Load(ref logic_SubGraph_SaveLoadBool_boolean_9, logic_SubGraph_SaveLoadBool_boolAsVariable_9, logic_SubGraph_SaveLoadBool_uniqueID_9);
	}

	private void Relay_Set_True_9()
	{
		logic_SubGraph_SaveLoadBool_boolean_9 = local_Initialize_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_9 = local_Initialize_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_9.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_9, logic_SubGraph_SaveLoadBool_boolAsVariable_9, logic_SubGraph_SaveLoadBool_uniqueID_9);
	}

	private void Relay_Set_False_9()
	{
		logic_SubGraph_SaveLoadBool_boolean_9 = local_Initialize_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_9 = local_Initialize_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_9.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_9, logic_SubGraph_SaveLoadBool_boolAsVariable_9, logic_SubGraph_SaveLoadBool_uniqueID_9);
	}

	private void Relay_Out_12()
	{
	}

	private void Relay_In_12()
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_12 = local_Stage_System_Int32;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_12.In(ref logic_SubGraph_CompleteObjectiveStage_objectiveStage_12, logic_SubGraph_CompleteObjectiveStage_isFinalObjective_12);
	}

	private void Relay_Save_Out_18()
	{
		Relay_Save_210();
	}

	private void Relay_Load_Out_18()
	{
		Relay_Load_210();
	}

	private void Relay_Restart_Out_18()
	{
		Relay_Restart_210();
	}

	private void Relay_Save_18()
	{
		logic_SubGraph_SaveLoadInt_integer_18 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_18 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_18.Save(logic_SubGraph_SaveLoadInt_restartValue_18, ref logic_SubGraph_SaveLoadInt_integer_18, logic_SubGraph_SaveLoadInt_intAsVariable_18, logic_SubGraph_SaveLoadInt_uniqueID_18);
	}

	private void Relay_Load_18()
	{
		logic_SubGraph_SaveLoadInt_integer_18 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_18 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_18.Load(logic_SubGraph_SaveLoadInt_restartValue_18, ref logic_SubGraph_SaveLoadInt_integer_18, logic_SubGraph_SaveLoadInt_intAsVariable_18, logic_SubGraph_SaveLoadInt_uniqueID_18);
	}

	private void Relay_Restart_18()
	{
		logic_SubGraph_SaveLoadInt_integer_18 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_18 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_18.Restart(logic_SubGraph_SaveLoadInt_restartValue_18, ref logic_SubGraph_SaveLoadInt_integer_18, logic_SubGraph_SaveLoadInt_intAsVariable_18, logic_SubGraph_SaveLoadInt_uniqueID_18);
	}

	private void Relay_In_20()
	{
		logic_uScript_HideArrow_uScript_HideArrow_20.In();
		if (logic_uScript_HideArrow_uScript_HideArrow_20.Out)
		{
			Relay_In_36();
		}
	}

	private void Relay_Pause_21()
	{
		logic_uScript_PausePopulation_uScript_PausePopulation_21.Pause();
		if (logic_uScript_PausePopulation_uScript_PausePopulation_21.Out)
		{
			Relay_In_5();
		}
	}

	private void Relay_UnPause_21()
	{
		logic_uScript_PausePopulation_uScript_PausePopulation_21.UnPause();
		if (logic_uScript_PausePopulation_uScript_PausePopulation_21.Out)
		{
			Relay_In_5();
		}
	}

	private void Relay_Output1_23()
	{
		Relay_In_214();
	}

	private void Relay_Output2_23()
	{
		Relay_In_121();
	}

	private void Relay_Output3_23()
	{
		Relay_In_140();
	}

	private void Relay_Output4_23()
	{
	}

	private void Relay_Output5_23()
	{
	}

	private void Relay_Output6_23()
	{
	}

	private void Relay_Output7_23()
	{
	}

	private void Relay_Output8_23()
	{
	}

	private void Relay_In_23()
	{
		logic_uScriptCon_ManualSwitch_CurrentOutput_23 = local_Stage_System_Int32;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_23.In(logic_uScriptCon_ManualSwitch_CurrentOutput_23);
	}

	private void Relay_SaveEvent_24()
	{
		Relay_Save_18();
	}

	private void Relay_LoadEvent_24()
	{
		Relay_Load_18();
	}

	private void Relay_RestartEvent_24()
	{
		Relay_Restart_18();
	}

	private void Relay_Save_Out_28()
	{
		Relay_Save_7();
	}

	private void Relay_Load_Out_28()
	{
		Relay_Load_7();
	}

	private void Relay_Restart_Out_28()
	{
		Relay_Set_False_7();
	}

	private void Relay_Save_28()
	{
		logic_SubGraph_SaveLoadBool_boolean_28 = local_msgBaseFoundShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_28 = local_msgBaseFoundShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_28.Save(ref logic_SubGraph_SaveLoadBool_boolean_28, logic_SubGraph_SaveLoadBool_boolAsVariable_28, logic_SubGraph_SaveLoadBool_uniqueID_28);
	}

	private void Relay_Load_28()
	{
		logic_SubGraph_SaveLoadBool_boolean_28 = local_msgBaseFoundShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_28 = local_msgBaseFoundShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_28.Load(ref logic_SubGraph_SaveLoadBool_boolean_28, logic_SubGraph_SaveLoadBool_boolAsVariable_28, logic_SubGraph_SaveLoadBool_uniqueID_28);
	}

	private void Relay_Set_True_28()
	{
		logic_SubGraph_SaveLoadBool_boolean_28 = local_msgBaseFoundShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_28 = local_msgBaseFoundShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_28.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_28, logic_SubGraph_SaveLoadBool_boolAsVariable_28, logic_SubGraph_SaveLoadBool_uniqueID_28);
	}

	private void Relay_Set_False_28()
	{
		logic_SubGraph_SaveLoadBool_boolean_28 = local_msgBaseFoundShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_28 = local_msgBaseFoundShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_28.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_28, logic_SubGraph_SaveLoadBool_boolAsVariable_28, logic_SubGraph_SaveLoadBool_uniqueID_28);
	}

	private void Relay_Save_Out_30()
	{
		Relay_Save_170();
	}

	private void Relay_Load_Out_30()
	{
		Relay_Load_170();
	}

	private void Relay_Restart_Out_30()
	{
		Relay_Set_False_170();
	}

	private void Relay_Save_30()
	{
		logic_SubGraph_SaveLoadBool_boolean_30 = local_ScrappingInProgress_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_30 = local_ScrappingInProgress_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_30.Save(ref logic_SubGraph_SaveLoadBool_boolean_30, logic_SubGraph_SaveLoadBool_boolAsVariable_30, logic_SubGraph_SaveLoadBool_uniqueID_30);
	}

	private void Relay_Load_30()
	{
		logic_SubGraph_SaveLoadBool_boolean_30 = local_ScrappingInProgress_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_30 = local_ScrappingInProgress_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_30.Load(ref logic_SubGraph_SaveLoadBool_boolean_30, logic_SubGraph_SaveLoadBool_boolAsVariable_30, logic_SubGraph_SaveLoadBool_uniqueID_30);
	}

	private void Relay_Set_True_30()
	{
		logic_SubGraph_SaveLoadBool_boolean_30 = local_ScrappingInProgress_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_30 = local_ScrappingInProgress_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_30.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_30, logic_SubGraph_SaveLoadBool_boolAsVariable_30, logic_SubGraph_SaveLoadBool_uniqueID_30);
	}

	private void Relay_Set_False_30()
	{
		logic_SubGraph_SaveLoadBool_boolean_30 = local_ScrappingInProgress_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_30 = local_ScrappingInProgress_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_30.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_30, logic_SubGraph_SaveLoadBool_boolAsVariable_30, logic_SubGraph_SaveLoadBool_uniqueID_30);
	}

	private void Relay_In_31()
	{
		logic_uScript_IsPlayerInRangeOfTech_tech_31 = local_CraftingBaseTech_Tank;
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_31.In(logic_uScript_IsPlayerInRangeOfTech_tech_31, logic_uScript_IsPlayerInRangeOfTech_range_31, logic_uScript_IsPlayerInRangeOfTech_techs_31);
		if (logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_31.InRange)
		{
			Relay_In_37();
		}
	}

	private void Relay_Pause_33()
	{
		logic_uScript_PausePopulation_uScript_PausePopulation_33.Pause();
		if (logic_uScript_PausePopulation_uScript_PausePopulation_33.Out)
		{
			Relay_True_35();
		}
	}

	private void Relay_UnPause_33()
	{
		logic_uScript_PausePopulation_uScript_PausePopulation_33.UnPause();
		if (logic_uScript_PausePopulation_uScript_PausePopulation_33.Out)
		{
			Relay_True_35();
		}
	}

	private void Relay_In_34()
	{
		logic_uScript_IsPlayerInRangeOfTech_tech_34 = local_CraftingBaseTech_Tank;
		logic_uScript_IsPlayerInRangeOfTech_range_34 = distBaseFound;
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_34.In(logic_uScript_IsPlayerInRangeOfTech_tech_34, logic_uScript_IsPlayerInRangeOfTech_range_34, logic_uScript_IsPlayerInRangeOfTech_techs_34);
		bool inRange = logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_34.InRange;
		bool outOfRange = logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_34.OutOfRange;
		if (inRange)
		{
			Relay_Pause_33();
		}
		if (outOfRange)
		{
			Relay_UnPause_21();
		}
	}

	private void Relay_True_35()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_35.True(out logic_uScriptAct_SetBool_Target_35);
		local_NearBase_System_Boolean = logic_uScriptAct_SetBool_Target_35;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_35.Out)
		{
			Relay_In_23();
		}
	}

	private void Relay_False_35()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_35.False(out logic_uScriptAct_SetBool_Target_35);
		local_NearBase_System_Boolean = logic_uScriptAct_SetBool_Target_35;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_35.Out)
		{
			Relay_In_23();
		}
	}

	private void Relay_In_36()
	{
		logic_uScriptCon_CompareBool_Bool_36 = local_NearBase_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_36.In(logic_uScriptCon_CompareBool_Bool_36);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_36.True)
		{
			Relay_In_4();
		}
	}

	private void Relay_In_37()
	{
		logic_uScriptCon_CompareBool_Bool_37 = local_msgIntroShown_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_37.In(logic_uScriptCon_CompareBool_Bool_37);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_37.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_37.False;
		if (num)
		{
			Relay_In_34();
		}
		if (flag)
		{
			Relay_True_47();
		}
	}

	private void Relay_Out_41()
	{
	}

	private void Relay_In_41()
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_41 = local_Stage_System_Int32;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_41.In(ref logic_SubGraph_CompleteObjectiveStage_objectiveStage_41, logic_SubGraph_CompleteObjectiveStage_isFinalObjective_41);
	}

	private void Relay_True_44()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_44.True(out logic_uScriptAct_SetBool_Target_44);
		local_NearBase_System_Boolean = logic_uScriptAct_SetBool_Target_44;
	}

	private void Relay_False_44()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_44.False(out logic_uScriptAct_SetBool_Target_44);
		local_NearBase_System_Boolean = logic_uScriptAct_SetBool_Target_44;
	}

	private void Relay_In_45()
	{
		logic_uScript_LockTechInteraction_tech_45 = local_CraftingBaseTech_Tank;
		logic_uScript_LockTechInteraction_uScript_LockTechInteraction_45.In(logic_uScript_LockTechInteraction_tech_45, logic_uScript_LockTechInteraction_excludedBlocks_45, logic_uScript_LockTechInteraction_excludedUniqueBlocks_45);
		if (logic_uScript_LockTechInteraction_uScript_LockTechInteraction_45.Out)
		{
			Relay_In_60();
		}
	}

	private void Relay_True_47()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_47.True(out logic_uScriptAct_SetBool_Target_47);
		local_msgIntroShown_System_Boolean = logic_uScriptAct_SetBool_Target_47;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_47.Out)
		{
			Relay_In_54();
		}
	}

	private void Relay_False_47()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_47.False(out logic_uScriptAct_SetBool_Target_47);
		local_msgIntroShown_System_Boolean = logic_uScriptAct_SetBool_Target_47;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_47.Out)
		{
			Relay_In_54();
		}
	}

	private void Relay_In_48()
	{
		logic_uScript_LockTechStacks_tech_48 = local_CraftingBaseTech_Tank;
		logic_uScript_LockTechStacks_uScript_LockTechStacks_48.In(logic_uScript_LockTechStacks_tech_48);
		if (logic_uScript_LockTechStacks_uScript_LockTechStacks_48.Out)
		{
			Relay_In_514();
		}
	}

	private void Relay_In_54()
	{
		logic_uScript_AddMessage_messageData_54 = msg01Intro;
		logic_uScript_AddMessage_speaker_54 = messageSpeaker;
		logic_uScript_AddMessage_Return_54 = logic_uScript_AddMessage_uScript_AddMessage_54.In(logic_uScript_AddMessage_messageData_54, logic_uScript_AddMessage_speaker_54);
		if (logic_uScript_AddMessage_uScript_AddMessage_54.Out)
		{
			Relay_In_34();
		}
	}

	private void Relay_Out_57()
	{
		Relay_In_194();
	}

	private void Relay_In_57()
	{
		logic_SubGraph_LoadObjectiveStates_currentObjective_57 = local_Stage_System_Int32;
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_57.In(logic_SubGraph_LoadObjectiveStates_currentObjective_57);
	}

	private void Relay_In_58()
	{
		logic_uScriptCon_CompareBool_Bool_58 = local_msgBaseFoundShown_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_58.In(logic_uScriptCon_CompareBool_Bool_58);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_58.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_58.False;
		if (num)
		{
			Relay_In_12();
		}
		if (flag)
		{
			Relay_In_118();
		}
	}

	private void Relay_Save_Out_59()
	{
		Relay_Save_164();
	}

	private void Relay_Load_Out_59()
	{
		Relay_Load_164();
	}

	private void Relay_Restart_Out_59()
	{
		Relay_Set_False_164();
	}

	private void Relay_Save_59()
	{
		logic_SubGraph_SaveLoadBool_boolean_59 = local_CanSuckUpBlocks_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_59 = local_CanSuckUpBlocks_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_59.Save(ref logic_SubGraph_SaveLoadBool_boolean_59, logic_SubGraph_SaveLoadBool_boolAsVariable_59, logic_SubGraph_SaveLoadBool_uniqueID_59);
	}

	private void Relay_Load_59()
	{
		logic_SubGraph_SaveLoadBool_boolean_59 = local_CanSuckUpBlocks_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_59 = local_CanSuckUpBlocks_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_59.Load(ref logic_SubGraph_SaveLoadBool_boolean_59, logic_SubGraph_SaveLoadBool_boolAsVariable_59, logic_SubGraph_SaveLoadBool_uniqueID_59);
	}

	private void Relay_Set_True_59()
	{
		logic_SubGraph_SaveLoadBool_boolean_59 = local_CanSuckUpBlocks_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_59 = local_CanSuckUpBlocks_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_59.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_59, logic_SubGraph_SaveLoadBool_boolAsVariable_59, logic_SubGraph_SaveLoadBool_uniqueID_59);
	}

	private void Relay_Set_False_59()
	{
		logic_SubGraph_SaveLoadBool_boolean_59 = local_CanSuckUpBlocks_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_59 = local_CanSuckUpBlocks_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_59.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_59, logic_SubGraph_SaveLoadBool_boolAsVariable_59, logic_SubGraph_SaveLoadBool_uniqueID_59);
	}

	private void Relay_In_60()
	{
		logic_uScriptCon_CompareBool_Bool_60 = local_CanSuckUpBlocks_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_60.In(logic_uScriptCon_CompareBool_Bool_60);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_60.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_60.False;
		if (num)
		{
			Relay_In_67();
		}
		if (flag)
		{
			Relay_In_48();
		}
	}

	private void Relay_In_61()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_61.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_61.Out)
		{
			Relay_In_514();
		}
	}

	private void Relay_In_62()
	{
		int num = 0;
		if (logic_uScriptAct_Concatenate_A_62.Length <= num)
		{
			Array.Resize(ref logic_uScriptAct_Concatenate_A_62, num + 1);
		}
		logic_uScriptAct_Concatenate_A_62[num++] = local_64_System_String;
		int num2 = 0;
		if (logic_uScriptAct_Concatenate_B_62.Length <= num2)
		{
			Array.Resize(ref logic_uScriptAct_Concatenate_B_62, num2 + 1);
		}
		logic_uScriptAct_Concatenate_B_62[num2++] = local_msgAllBlocksCraftedShown_System_Boolean;
		logic_uScriptAct_Concatenate_uScriptAct_Concatenate_62.In(logic_uScriptAct_Concatenate_A_62, logic_uScriptAct_Concatenate_B_62, logic_uScriptAct_Concatenate_Separator_62, out logic_uScriptAct_Concatenate_Result_62);
		local_63_System_String = logic_uScriptAct_Concatenate_Result_62;
		if (logic_uScriptAct_Concatenate_uScriptAct_Concatenate_62.Out)
		{
			Relay_ShowLabel_66();
		}
	}

	private void Relay_ShowLabel_66()
	{
		logic_uScriptAct_PrintText_Text_66 = local_63_System_String;
		logic_uScriptAct_PrintText_uScriptAct_PrintText_66.ShowLabel(logic_uScriptAct_PrintText_Text_66, logic_uScriptAct_PrintText_FontSize_66, logic_uScriptAct_PrintText_FontStyle_66, logic_uScriptAct_PrintText_FontColor_66, logic_uScriptAct_PrintText_textAnchor_66, logic_uScriptAct_PrintText_EdgePadding_66, logic_uScriptAct_PrintText_time_66);
	}

	private void Relay_HideLabel_66()
	{
		logic_uScriptAct_PrintText_Text_66 = local_63_System_String;
		logic_uScriptAct_PrintText_uScriptAct_PrintText_66.HideLabel(logic_uScriptAct_PrintText_Text_66, logic_uScriptAct_PrintText_FontSize_66, logic_uScriptAct_PrintText_FontStyle_66, logic_uScriptAct_PrintText_FontColor_66, logic_uScriptAct_PrintText_textAnchor_66, logic_uScriptAct_PrintText_EdgePadding_66, logic_uScriptAct_PrintText_time_66);
	}

	private void Relay_In_67()
	{
		logic_uScript_LockBlock_block_67 = local_ScrapperBlock_TankBlock;
		logic_uScript_LockBlock_uScript_LockBlock_67.In(logic_uScript_LockBlock_block_67, logic_uScript_LockBlock_functionalityToLock_67);
		if (logic_uScript_LockBlock_uScript_LockBlock_67.Out)
		{
			Relay_In_61();
		}
	}

	private void Relay_In_70()
	{
		logic_uScript_SetTankInvulnerable_tank_70 = local_CraftingBaseTech_Tank;
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_70.In(logic_uScript_SetTankInvulnerable_invulnerable_70, logic_uScript_SetTankInvulnerable_tank_70);
		if (logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_70.Out)
		{
			Relay_In_93();
		}
	}

	private void Relay_AtIndex_71()
	{
		int num = 0;
		Array array = local_106_TankArray;
		if (logic_uScript_AccessListTech_techList_71.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_71, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_71, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_71.AtIndex(ref logic_uScript_AccessListTech_techList_71, logic_uScript_AccessListTech_index_71, out logic_uScript_AccessListTech_value_71);
		local_106_TankArray = logic_uScript_AccessListTech_techList_71;
		local_NPCTech_Tank = logic_uScript_AccessListTech_value_71;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_71.Out)
		{
			Relay_In_96();
		}
	}

	private void Relay_In_77()
	{
		logic_uScript_SetCustomRadarTeamID_tech_77 = local_NPCTech_Tank;
		logic_uScript_SetCustomRadarTeamID_uScript_SetCustomRadarTeamID_77.In(logic_uScript_SetCustomRadarTeamID_tech_77, logic_uScript_SetCustomRadarTeamID_radarTeamID_77);
		if (logic_uScript_SetCustomRadarTeamID_uScript_SetCustomRadarTeamID_77.Out)
		{
			Relay_In_123();
		}
	}

	private void Relay_In_78()
	{
		int num = 0;
		Array nPCSpawnData = NPCSpawnData;
		if (logic_uScript_GetAndCheckTechs_techData_78.Length != num + nPCSpawnData.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_78, num + nPCSpawnData.Length);
		}
		Array.Copy(nPCSpawnData, 0, logic_uScript_GetAndCheckTechs_techData_78, num, nPCSpawnData.Length);
		num += nPCSpawnData.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_78 = owner_Connection_115;
		int num2 = 0;
		Array array = local_106_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_78.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_78, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techs_78, num2, array.Length);
		num2 += array.Length;
		logic_uScript_GetAndCheckTechs_Return_78 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_78.In(logic_uScript_GetAndCheckTechs_techData_78, logic_uScript_GetAndCheckTechs_ownerNode_78, ref logic_uScript_GetAndCheckTechs_techs_78);
		local_106_TankArray = logic_uScript_GetAndCheckTechs_techs_78;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_78.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_78.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_78.AllDead;
		if (allAlive)
		{
			Relay_AtIndex_71();
		}
		if (someAlive)
		{
			Relay_AtIndex_71();
		}
		if (allDead)
		{
			Relay_In_87();
		}
	}

	private void Relay_In_79()
	{
		logic_uScript_LockTech_tech_79 = local_CraftingBaseTech_Tank;
		logic_uScript_LockTech_uScript_LockTech_79.In(logic_uScript_LockTech_tech_79, logic_uScript_LockTech_lockType_79);
		if (logic_uScript_LockTech_uScript_LockTech_79.Out)
		{
			Relay_In_70();
		}
	}

	private void Relay_InitialSpawn_83()
	{
		int num = 0;
		Array nPCSpawnData = NPCSpawnData;
		if (logic_uScript_SpawnTechsFromData_spawnData_83.Length != num + nPCSpawnData.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_83, num + nPCSpawnData.Length);
		}
		Array.Copy(nPCSpawnData, 0, logic_uScript_SpawnTechsFromData_spawnData_83, num, nPCSpawnData.Length);
		num += nPCSpawnData.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_83 = owner_Connection_85;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_83.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_83, logic_uScript_SpawnTechsFromData_ownerNode_83, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_83, logic_uScript_SpawnTechsFromData_allowResurrection_83);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_83.Out)
		{
			Relay_In_91();
		}
	}

	private void Relay_OnUpdate_84()
	{
		Relay_In_100();
	}

	private void Relay_OnSuspend_84()
	{
	}

	private void Relay_OnResume_84()
	{
	}

	private void Relay_In_86()
	{
		logic_uScript_SetTankHideBlockLimit_tech_86 = local_CraftingBaseTech_Tank;
		logic_uScript_SetTankHideBlockLimit_uScript_SetTankHideBlockLimit_86.In(logic_uScript_SetTankHideBlockLimit_hidden_86, logic_uScript_SetTankHideBlockLimit_tech_86);
		if (logic_uScript_SetTankHideBlockLimit_uScript_SetTankHideBlockLimit_86.Out)
		{
			Relay_In_78();
		}
	}

	private void Relay_In_87()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_87.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_87.Out)
		{
			Relay_In_109();
		}
	}

	private void Relay_In_89()
	{
		int num = 0;
		Array craftingBaseSpawnData = CraftingBaseSpawnData;
		if (logic_uScript_GetAndCheckTechs_techData_89.Length != num + craftingBaseSpawnData.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_89, num + craftingBaseSpawnData.Length);
		}
		Array.Copy(craftingBaseSpawnData, 0, logic_uScript_GetAndCheckTechs_techData_89, num, craftingBaseSpawnData.Length);
		num += craftingBaseSpawnData.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_89 = owner_Connection_81;
		int num2 = 0;
		Array array = local_104_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_89.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_89, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techs_89, num2, array.Length);
		num2 += array.Length;
		logic_uScript_GetAndCheckTechs_Return_89 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_89.In(logic_uScript_GetAndCheckTechs_techData_89, logic_uScript_GetAndCheckTechs_ownerNode_89, ref logic_uScript_GetAndCheckTechs_techs_89);
		local_104_TankArray = logic_uScript_GetAndCheckTechs_techs_89;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_89.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_89.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_89.AllDead;
		if (allAlive)
		{
			Relay_AtIndex_112();
		}
		if (someAlive)
		{
			Relay_AtIndex_112();
		}
		if (allDead)
		{
			Relay_In_99();
		}
	}

	private void Relay_In_91()
	{
		logic_uScript_RemoveScenery_ownerNode_91 = owner_Connection_94;
		logic_uScript_RemoveScenery_positionName_91 = BasePosition;
		logic_uScript_RemoveScenery_radius_91 = clearSceneryRadius;
		logic_uScript_RemoveScenery_uScript_RemoveScenery_91.In(logic_uScript_RemoveScenery_ownerNode_91, logic_uScript_RemoveScenery_positionName_91, logic_uScript_RemoveScenery_radius_91, logic_uScript_RemoveScenery_preventChunksSpawning_91);
		if (logic_uScript_RemoveScenery_uScript_RemoveScenery_91.Out)
		{
			Relay_In_89();
		}
	}

	private void Relay_In_93()
	{
		logic_uScript_SetCustomRadarTeamID_tech_93 = local_CraftingBaseTech_Tank;
		logic_uScript_SetCustomRadarTeamID_uScript_SetCustomRadarTeamID_93.In(logic_uScript_SetCustomRadarTeamID_tech_93, logic_uScript_SetCustomRadarTeamID_radarTeamID_93);
		if (logic_uScript_SetCustomRadarTeamID_uScript_SetCustomRadarTeamID_93.Out)
		{
			Relay_In_86();
		}
	}

	private void Relay_In_96()
	{
		logic_uScript_SetTankInvulnerable_tank_96 = local_NPCTech_Tank;
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_96.In(logic_uScript_SetTankInvulnerable_invulnerable_96, logic_uScript_SetTankInvulnerable_tank_96);
		if (logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_96.Out)
		{
			Relay_In_102();
		}
	}

	private void Relay_In_99()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_99.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_99.Out)
		{
			Relay_In_105();
		}
	}

	private void Relay_In_100()
	{
		logic_uScriptCon_CompareBool_Bool_100 = local_Initialize_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_100.In(logic_uScriptCon_CompareBool_Bool_100);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_100.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_100.False;
		if (num)
		{
			Relay_In_111();
		}
		if (flag)
		{
			Relay_True_113();
		}
	}

	private void Relay_In_102()
	{
		logic_uScript_LockTech_tech_102 = local_NPCTech_Tank;
		logic_uScript_LockTech_uScript_LockTech_102.In(logic_uScript_LockTech_tech_102, logic_uScript_LockTech_lockType_102);
		if (logic_uScript_LockTech_uScript_LockTech_102.Out)
		{
			Relay_In_103();
		}
	}

	private void Relay_In_103()
	{
		logic_uScript_LockTechInteraction_tech_103 = local_NPCTech_Tank;
		logic_uScript_LockTechInteraction_uScript_LockTechInteraction_103.In(logic_uScript_LockTechInteraction_tech_103, logic_uScript_LockTechInteraction_excludedBlocks_103, logic_uScript_LockTechInteraction_excludedUniqueBlocks_103);
		if (logic_uScript_LockTechInteraction_uScript_LockTechInteraction_103.Out)
		{
			Relay_In_77();
		}
	}

	private void Relay_In_105()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_105.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_105.Out)
		{
			Relay_In_78();
		}
	}

	private void Relay_In_109()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_109.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_109.Out)
		{
			Relay_In_123();
		}
	}

	private void Relay_In_111()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_111.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_111.Out)
		{
			Relay_In_89();
		}
	}

	private void Relay_AtIndex_112()
	{
		int num = 0;
		Array array = local_104_TankArray;
		if (logic_uScript_AccessListTech_techList_112.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_112, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_112, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_112.AtIndex(ref logic_uScript_AccessListTech_techList_112, logic_uScript_AccessListTech_index_112, out logic_uScript_AccessListTech_value_112);
		local_104_TankArray = logic_uScript_AccessListTech_techList_112;
		local_CraftingBaseTech_Tank = logic_uScript_AccessListTech_value_112;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_112.Out)
		{
			Relay_In_79();
		}
	}

	private void Relay_True_113()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_113.True(out logic_uScriptAct_SetBool_Target_113);
		local_Initialize_System_Boolean = logic_uScriptAct_SetBool_Target_113;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_113.Out)
		{
			Relay_InitialSpawn_114();
		}
	}

	private void Relay_False_113()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_113.False(out logic_uScriptAct_SetBool_Target_113);
		local_Initialize_System_Boolean = logic_uScriptAct_SetBool_Target_113;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_113.Out)
		{
			Relay_InitialSpawn_114();
		}
	}

	private void Relay_InitialSpawn_114()
	{
		int num = 0;
		Array craftingBaseSpawnData = CraftingBaseSpawnData;
		if (logic_uScript_SpawnTechsFromData_spawnData_114.Length != num + craftingBaseSpawnData.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_114, num + craftingBaseSpawnData.Length);
		}
		Array.Copy(craftingBaseSpawnData, 0, logic_uScript_SpawnTechsFromData_spawnData_114, num, craftingBaseSpawnData.Length);
		num += craftingBaseSpawnData.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_114 = owner_Connection_110;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_114.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_114, logic_uScript_SpawnTechsFromData_ownerNode_114, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_114, logic_uScript_SpawnTechsFromData_allowResurrection_114);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_114.Out)
		{
			Relay_InitialSpawn_83();
		}
	}

	private void Relay_True_117()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_117.True(out logic_uScriptAct_SetBool_Target_117);
		local_msgBaseFoundShown_System_Boolean = logic_uScriptAct_SetBool_Target_117;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_117.Out)
		{
			Relay_In_12();
		}
	}

	private void Relay_False_117()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_117.False(out logic_uScriptAct_SetBool_Target_117);
		local_msgBaseFoundShown_System_Boolean = logic_uScriptAct_SetBool_Target_117;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_117.Out)
		{
			Relay_In_12();
		}
	}

	private void Relay_In_118()
	{
		logic_uScript_AddMessage_messageData_118 = msg02BaseFound;
		logic_uScript_AddMessage_speaker_118 = messageSpeaker;
		logic_uScript_AddMessage_Return_118 = logic_uScript_AddMessage_uScript_AddMessage_118.In(logic_uScript_AddMessage_messageData_118, logic_uScript_AddMessage_speaker_118);
		if (logic_uScript_AddMessage_uScript_AddMessage_118.Shown)
		{
			Relay_True_117();
		}
	}

	private void Relay_In_121()
	{
		logic_uScriptCon_CompareBool_Bool_121 = local_AllBlocksScrapped_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_121.In(logic_uScriptCon_CompareBool_Bool_121);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_121.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_121.False;
		if (num)
		{
			Relay_In_233();
		}
		if (flag)
		{
			Relay_True_204();
		}
	}

	private void Relay_In_123()
	{
		logic_uScript_GetTankBlock_tank_123 = local_CraftingBaseTech_Tank;
		logic_uScript_GetTankBlock_blockType_123 = BlockTypeSJSscrapper;
		logic_uScript_GetTankBlock_Return_123 = logic_uScript_GetTankBlock_uScript_GetTankBlock_123.In(logic_uScript_GetTankBlock_tank_123, logic_uScript_GetTankBlock_blockType_123);
		local_ScrapperBlock_TankBlock = logic_uScript_GetTankBlock_Return_123;
		if (logic_uScript_GetTankBlock_uScript_GetTankBlock_123.Returned)
		{
			Relay_In_45();
		}
	}

	private void Relay_In_127()
	{
		logic_uScript_AddMessage_messageData_127 = msgSpawnMinion;
		logic_uScript_AddMessage_speaker_127 = messageSpeaker;
		logic_uScript_AddMessage_Return_127 = logic_uScript_AddMessage_uScript_AddMessage_127.In(logic_uScript_AddMessage_messageData_127, logic_uScript_AddMessage_speaker_127);
		if (logic_uScript_AddMessage_uScript_AddMessage_127.Out)
		{
			Relay_In_144();
		}
	}

	private void Relay_InitialSpawn_128()
	{
		int num = 0;
		Array enemyTechData = EnemyTechData;
		if (logic_uScript_SpawnTechsFromData_spawnData_128.Length != num + enemyTechData.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_128, num + enemyTechData.Length);
		}
		Array.Copy(enemyTechData, 0, logic_uScript_SpawnTechsFromData_spawnData_128, num, enemyTechData.Length);
		num += enemyTechData.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_128 = owner_Connection_142;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_128.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_128, logic_uScript_SpawnTechsFromData_ownerNode_128, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_128, logic_uScript_SpawnTechsFromData_allowResurrection_128);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_128.Out)
		{
			Relay_True_146();
		}
	}

	private void Relay_In_129()
	{
		logic_uScriptCon_CompareBool_Bool_129 = local_EnemyAlive_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_129.In(logic_uScriptCon_CompareBool_Bool_129);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_129.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_129.False;
		if (num)
		{
			Relay_In_127();
		}
		if (flag)
		{
			Relay_InitialSpawn_128();
		}
	}

	private void Relay_True_131()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_131.True(out logic_uScriptAct_SetBool_Target_131);
		local_EnemyAlive_System_Boolean = logic_uScriptAct_SetBool_Target_131;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_131.Out)
		{
			Relay_In_143();
		}
	}

	private void Relay_False_131()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_131.False(out logic_uScriptAct_SetBool_Target_131);
		local_EnemyAlive_System_Boolean = logic_uScriptAct_SetBool_Target_131;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_131.Out)
		{
			Relay_In_143();
		}
	}

	private void Relay_Output1_140()
	{
		Relay_In_223();
	}

	private void Relay_Output2_140()
	{
		Relay_In_129();
	}

	private void Relay_Output3_140()
	{
		Relay_In_162();
	}

	private void Relay_Output4_140()
	{
	}

	private void Relay_Output5_140()
	{
	}

	private void Relay_Output6_140()
	{
	}

	private void Relay_Output7_140()
	{
	}

	private void Relay_Output8_140()
	{
	}

	private void Relay_In_140()
	{
		logic_uScriptCon_ManualSwitch_CurrentOutput_140 = local_Stage2_System_Int32;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_140.In(logic_uScriptCon_ManualSwitch_CurrentOutput_140);
	}

	private void Relay_In_143()
	{
		logic_uScriptAct_AddInt_v2_A_143 = local_Stage2_System_Int32;
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_143.In(logic_uScriptAct_AddInt_v2_A_143, logic_uScriptAct_AddInt_v2_B_143, out logic_uScriptAct_AddInt_v2_IntResult_143, out logic_uScriptAct_AddInt_v2_FloatResult_143);
		local_Stage2_System_Int32 = logic_uScriptAct_AddInt_v2_IntResult_143;
	}

	private void Relay_In_144()
	{
		int num = 0;
		Array enemyTechData = EnemyTechData;
		if (logic_uScript_GetAndCheckTechs_techData_144.Length != num + enemyTechData.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_144, num + enemyTechData.Length);
		}
		Array.Copy(enemyTechData, 0, logic_uScript_GetAndCheckTechs_techData_144, num, enemyTechData.Length);
		num += enemyTechData.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_144 = owner_Connection_137;
		int num2 = 0;
		Array array = local_Enemy_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_144.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_144, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techs_144, num2, array.Length);
		num2 += array.Length;
		logic_uScript_GetAndCheckTechs_Return_144 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_144.In(logic_uScript_GetAndCheckTechs_techData_144, logic_uScript_GetAndCheckTechs_ownerNode_144, ref logic_uScript_GetAndCheckTechs_techs_144);
		local_Enemy_TankArray = logic_uScript_GetAndCheckTechs_techs_144;
		if (logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_144.AllDead)
		{
			Relay_False_131();
		}
	}

	private void Relay_True_146()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_146.True(out logic_uScriptAct_SetBool_Target_146);
		local_EnemyAlive_System_Boolean = logic_uScriptAct_SetBool_Target_146;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_146.Out)
		{
			Relay_In_144();
		}
	}

	private void Relay_False_146()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_146.False(out logic_uScriptAct_SetBool_Target_146);
		local_EnemyAlive_System_Boolean = logic_uScriptAct_SetBool_Target_146;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_146.Out)
		{
			Relay_In_144();
		}
	}

	private void Relay_In_148()
	{
		logic_uScript_SpawnVFX_ownerNode_148 = owner_Connection_156;
		logic_uScript_SpawnVFX_vfxToSpawn_148 = NPCDespawnParticleEffect;
		logic_uScript_SpawnVFX_spawnPosName_148 = BaseVFXSpawn;
		logic_uScript_SpawnVFX_uScript_SpawnVFX_148.In(logic_uScript_SpawnVFX_ownerNode_148, logic_uScript_SpawnVFX_vfxToSpawn_148, logic_uScript_SpawnVFX_spawnPosName_148);
		if (logic_uScript_SpawnVFX_uScript_SpawnVFX_148.Out)
		{
			Relay_In_160();
		}
	}

	private void Relay_Succeed_157()
	{
		logic_uScript_FinishEncounter_owner_157 = owner_Connection_151;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_157.Succeed(logic_uScript_FinishEncounter_owner_157);
	}

	private void Relay_Fail_157()
	{
		logic_uScript_FinishEncounter_owner_157 = owner_Connection_151;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_157.Fail(logic_uScript_FinishEncounter_owner_157);
	}

	private void Relay_In_160()
	{
		logic_uScript_RemoveTech_tech_160 = local_CraftingBaseTech_Tank;
		logic_uScript_RemoveTech_uScript_RemoveTech_160.In(logic_uScript_RemoveTech_tech_160);
	}

	private void Relay_In_161()
	{
		logic_uScript_FlyTechUpAndAway_tech_161 = local_NPCTech_Tank;
		logic_uScript_FlyTechUpAndAway_aiTree_161 = NPCFlyAwayAI;
		logic_uScript_FlyTechUpAndAway_removalParticles_161 = NPCDespawnParticleEffect;
		logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_161.In(logic_uScript_FlyTechUpAndAway_tech_161, logic_uScript_FlyTechUpAndAway_maxLifetime_161, logic_uScript_FlyTechUpAndAway_targetHeight_161, logic_uScript_FlyTechUpAndAway_aiTree_161, logic_uScript_FlyTechUpAndAway_removalParticles_161);
		if (logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_161.Out)
		{
			Relay_Succeed_157();
		}
	}

	private void Relay_In_162()
	{
		logic_uScript_AddMessage_messageData_162 = msg05Complete;
		logic_uScript_AddMessage_speaker_162 = messageSpeaker;
		logic_uScript_AddMessage_Return_162 = logic_uScript_AddMessage_uScript_AddMessage_162.In(logic_uScript_AddMessage_messageData_162, logic_uScript_AddMessage_speaker_162);
		if (logic_uScript_AddMessage_uScript_AddMessage_162.Shown)
		{
			Relay_In_161();
		}
	}

	private void Relay_Save_Out_164()
	{
	}

	private void Relay_Load_Out_164()
	{
		Relay_In_57();
	}

	private void Relay_Restart_Out_164()
	{
		Relay_False_44();
	}

	private void Relay_Save_164()
	{
		logic_SubGraph_SaveLoadBool_boolean_164 = local_AllBlocksScrapped_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_164 = local_AllBlocksScrapped_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_164.Save(ref logic_SubGraph_SaveLoadBool_boolean_164, logic_SubGraph_SaveLoadBool_boolAsVariable_164, logic_SubGraph_SaveLoadBool_uniqueID_164);
	}

	private void Relay_Load_164()
	{
		logic_SubGraph_SaveLoadBool_boolean_164 = local_AllBlocksScrapped_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_164 = local_AllBlocksScrapped_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_164.Load(ref logic_SubGraph_SaveLoadBool_boolean_164, logic_SubGraph_SaveLoadBool_boolAsVariable_164, logic_SubGraph_SaveLoadBool_uniqueID_164);
	}

	private void Relay_Set_True_164()
	{
		logic_SubGraph_SaveLoadBool_boolean_164 = local_AllBlocksScrapped_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_164 = local_AllBlocksScrapped_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_164.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_164, logic_SubGraph_SaveLoadBool_boolAsVariable_164, logic_SubGraph_SaveLoadBool_uniqueID_164);
	}

	private void Relay_Set_False_164()
	{
		logic_SubGraph_SaveLoadBool_boolean_164 = local_AllBlocksScrapped_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_164 = local_AllBlocksScrapped_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_164.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_164, logic_SubGraph_SaveLoadBool_boolAsVariable_164, logic_SubGraph_SaveLoadBool_uniqueID_164);
	}

	private void Relay_Save_Out_170()
	{
		Relay_Save_172();
	}

	private void Relay_Load_Out_170()
	{
		Relay_Load_172();
	}

	private void Relay_Restart_Out_170()
	{
		Relay_Set_False_172();
	}

	private void Relay_Save_170()
	{
		logic_SubGraph_SaveLoadBool_boolean_170 = local_EnemyAlive_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_170 = local_EnemyAlive_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_170.Save(ref logic_SubGraph_SaveLoadBool_boolean_170, logic_SubGraph_SaveLoadBool_boolAsVariable_170, logic_SubGraph_SaveLoadBool_uniqueID_170);
	}

	private void Relay_Load_170()
	{
		logic_SubGraph_SaveLoadBool_boolean_170 = local_EnemyAlive_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_170 = local_EnemyAlive_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_170.Load(ref logic_SubGraph_SaveLoadBool_boolean_170, logic_SubGraph_SaveLoadBool_boolAsVariable_170, logic_SubGraph_SaveLoadBool_uniqueID_170);
	}

	private void Relay_Set_True_170()
	{
		logic_SubGraph_SaveLoadBool_boolean_170 = local_EnemyAlive_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_170 = local_EnemyAlive_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_170.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_170, logic_SubGraph_SaveLoadBool_boolAsVariable_170, logic_SubGraph_SaveLoadBool_uniqueID_170);
	}

	private void Relay_Set_False_170()
	{
		logic_SubGraph_SaveLoadBool_boolean_170 = local_EnemyAlive_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_170 = local_EnemyAlive_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_170.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_170, logic_SubGraph_SaveLoadBool_boolAsVariable_170, logic_SubGraph_SaveLoadBool_uniqueID_170);
	}

	private void Relay_Save_Out_172()
	{
		Relay_Save_59();
	}

	private void Relay_Load_Out_172()
	{
		Relay_Load_59();
	}

	private void Relay_Restart_Out_172()
	{
		Relay_Set_False_59();
	}

	private void Relay_Save_172()
	{
		logic_SubGraph_SaveLoadBool_boolean_172 = local_AllBlocksScrapped_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_172 = local_AllBlocksScrapped_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_172.Save(ref logic_SubGraph_SaveLoadBool_boolean_172, logic_SubGraph_SaveLoadBool_boolAsVariable_172, logic_SubGraph_SaveLoadBool_uniqueID_172);
	}

	private void Relay_Load_172()
	{
		logic_SubGraph_SaveLoadBool_boolean_172 = local_AllBlocksScrapped_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_172 = local_AllBlocksScrapped_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_172.Load(ref logic_SubGraph_SaveLoadBool_boolean_172, logic_SubGraph_SaveLoadBool_boolAsVariable_172, logic_SubGraph_SaveLoadBool_uniqueID_172);
	}

	private void Relay_Set_True_172()
	{
		logic_SubGraph_SaveLoadBool_boolean_172 = local_AllBlocksScrapped_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_172 = local_AllBlocksScrapped_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_172.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_172, logic_SubGraph_SaveLoadBool_boolAsVariable_172, logic_SubGraph_SaveLoadBool_uniqueID_172);
	}

	private void Relay_Set_False_172()
	{
		logic_SubGraph_SaveLoadBool_boolean_172 = local_AllBlocksScrapped_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_172 = local_AllBlocksScrapped_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_172.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_172, logic_SubGraph_SaveLoadBool_boolAsVariable_172, logic_SubGraph_SaveLoadBool_uniqueID_172);
	}

	private void Relay_BlockScrappedEvent_173()
	{
		local_CrafterFromEvent_TankBlock = event_UnityEngine_GameObject_CrafterBlock_173;
		Relay_In_175();
	}

	private void Relay_In_175()
	{
		logic_uScript_CompareBlock_A_175 = local_CrafterFromEvent_TankBlock;
		logic_uScript_CompareBlock_B_175 = local_ScrapperBlock_TankBlock;
		logic_uScript_CompareBlock_uScript_CompareBlock_175.In(logic_uScript_CompareBlock_A_175, logic_uScript_CompareBlock_B_175);
		if (logic_uScript_CompareBlock_uScript_CompareBlock_175.EqualTo)
		{
			Relay_In_220();
		}
	}

	private void Relay_True_180()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_180.True(out logic_uScriptAct_SetBool_Target_180);
		local_AllBlocksScrapped_System_Boolean = logic_uScriptAct_SetBool_Target_180;
	}

	private void Relay_False_180()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_180.False(out logic_uScriptAct_SetBool_Target_180);
		local_AllBlocksScrapped_System_Boolean = logic_uScriptAct_SetBool_Target_180;
	}

	private void Relay_SetCount_182()
	{
		logic_uScript_SetQuestObjectiveCount_owner_182 = owner_Connection_186;
		logic_uScript_SetQuestObjectiveCount_objectiveId_182 = local_Stage_System_Int32;
		logic_uScript_SetQuestObjectiveCount_currentCount_182 = local_CurrentAmountType_System_Int32;
		logic_uScript_SetQuestObjectiveCount_uScript_SetQuestObjectiveCount_182.SetCount(logic_uScript_SetQuestObjectiveCount_owner_182, logic_uScript_SetQuestObjectiveCount_objectiveId_182, logic_uScript_SetQuestObjectiveCount_currentCount_182);
		if (logic_uScript_SetQuestObjectiveCount_uScript_SetQuestObjectiveCount_182.Out)
		{
			Relay_In_187();
		}
	}

	private void Relay_In_183()
	{
		logic_uScript_EnableGlow_targetObject_183 = local_ScrapperBlock_TankBlock;
		logic_uScript_EnableGlow_uScript_EnableGlow_183.In(logic_uScript_EnableGlow_targetObject_183, logic_uScript_EnableGlow_enable_183);
		if (logic_uScript_EnableGlow_uScript_EnableGlow_183.Out)
		{
			Relay_True_180();
		}
	}

	private void Relay_In_184()
	{
		logic_uScript_HideArrow_uScript_HideArrow_184.In();
		if (logic_uScript_HideArrow_uScript_HideArrow_184.Out)
		{
			Relay_In_183();
		}
	}

	private void Relay_In_187()
	{
		logic_uScriptCon_CompareInt_A_187 = local_CurrentAmountType_System_Int32;
		logic_uScriptCon_CompareInt_B_187 = TargetAmount;
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_187.In(logic_uScriptCon_CompareInt_A_187, logic_uScriptCon_CompareInt_B_187);
		if (logic_uScriptCon_CompareInt_uScriptCon_CompareInt_187.GreaterThanOrEqualTo)
		{
			Relay_In_184();
		}
	}

	private void Relay_In_189()
	{
		logic_uScriptAct_AddInt_v2_A_189 = local_CurrentAmountType_System_Int32;
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_189.In(logic_uScriptAct_AddInt_v2_A_189, logic_uScriptAct_AddInt_v2_B_189, out logic_uScriptAct_AddInt_v2_IntResult_189, out logic_uScriptAct_AddInt_v2_FloatResult_189);
		local_CurrentAmountType_System_Int32 = logic_uScriptAct_AddInt_v2_IntResult_189;
		if (logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_189.Out)
		{
			Relay_SetCount_182();
		}
	}

	private void Relay_SetCount_191()
	{
		logic_uScript_SetQuestObjectiveCount_owner_191 = owner_Connection_190;
		logic_uScript_SetQuestObjectiveCount_objectiveId_191 = local_Stage_System_Int32;
		logic_uScript_SetQuestObjectiveCount_currentCount_191 = local_CurrentAmountType_System_Int32;
		logic_uScript_SetQuestObjectiveCount_uScript_SetQuestObjectiveCount_191.SetCount(logic_uScript_SetQuestObjectiveCount_owner_191, logic_uScript_SetQuestObjectiveCount_objectiveId_191, logic_uScript_SetQuestObjectiveCount_currentCount_191);
	}

	private void Relay_In_194()
	{
		logic_uScriptCon_CompareInt_A_194 = local_Stage_System_Int32;
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_194.In(logic_uScriptCon_CompareInt_A_194, logic_uScriptCon_CompareInt_B_194);
		if (logic_uScriptCon_CompareInt_uScriptCon_CompareInt_194.EqualTo)
		{
			Relay_SetCount_191();
		}
	}

	private void Relay_In_196()
	{
		logic_uScript_PointArrowAtVisible_targetObject_196 = local_ScrapperBlock_TankBlock;
		logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_196.In(logic_uScript_PointArrowAtVisible_targetObject_196, logic_uScript_PointArrowAtVisible_timeToShowFor_196, logic_uScript_PointArrowAtVisible_offset_196);
		if (logic_uScript_PointArrowAtVisible_uScript_PointArrowAtVisible_196.Out)
		{
			Relay_In_201();
		}
	}

	private void Relay_In_201()
	{
		logic_uScript_EnableGlow_targetObject_201 = local_ScrapperBlock_TankBlock;
		logic_uScript_EnableGlow_uScript_EnableGlow_201.In(logic_uScript_EnableGlow_targetObject_201, logic_uScript_EnableGlow_enable_201);
	}

	private void Relay_In_202()
	{
		logic_uScript_AddMessage_messageData_202 = msg03PutBlocksInScrapper;
		logic_uScript_AddMessage_speaker_202 = messageSpeaker;
		logic_uScript_AddMessage_Return_202 = logic_uScript_AddMessage_uScript_AddMessage_202.In(logic_uScript_AddMessage_messageData_202, logic_uScript_AddMessage_speaker_202);
		if (logic_uScript_AddMessage_uScript_AddMessage_202.Out)
		{
			Relay_In_196();
		}
	}

	private void Relay_True_204()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_204.True(out logic_uScriptAct_SetBool_Target_204);
		local_CanSuckUpBlocks_System_Boolean = logic_uScriptAct_SetBool_Target_204;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_204.Out)
		{
			Relay_SetCount_208();
		}
	}

	private void Relay_False_204()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_204.False(out logic_uScriptAct_SetBool_Target_204);
		local_CanSuckUpBlocks_System_Boolean = logic_uScriptAct_SetBool_Target_204;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_204.Out)
		{
			Relay_SetCount_208();
		}
	}

	private void Relay_SetCount_208()
	{
		logic_uScript_SetQuestObjectiveCount_owner_208 = owner_Connection_205;
		logic_uScript_SetQuestObjectiveCount_objectiveId_208 = local_Stage_System_Int32;
		logic_uScript_SetQuestObjectiveCount_currentCount_208 = local_CurrentAmountType_System_Int32;
		logic_uScript_SetQuestObjectiveCount_uScript_SetQuestObjectiveCount_208.SetCount(logic_uScript_SetQuestObjectiveCount_owner_208, logic_uScript_SetQuestObjectiveCount_objectiveId_208, logic_uScript_SetQuestObjectiveCount_currentCount_208);
		if (logic_uScript_SetQuestObjectiveCount_uScript_SetQuestObjectiveCount_208.Out)
		{
			Relay_In_202();
		}
	}

	private void Relay_Save_Out_210()
	{
		Relay_Save_212();
	}

	private void Relay_Load_Out_210()
	{
		Relay_Load_212();
	}

	private void Relay_Restart_Out_210()
	{
		Relay_Restart_212();
	}

	private void Relay_Save_210()
	{
		logic_SubGraph_SaveLoadInt_integer_210 = local_Stage2_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_210 = local_Stage2_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_210.Save(logic_SubGraph_SaveLoadInt_restartValue_210, ref logic_SubGraph_SaveLoadInt_integer_210, logic_SubGraph_SaveLoadInt_intAsVariable_210, logic_SubGraph_SaveLoadInt_uniqueID_210);
	}

	private void Relay_Load_210()
	{
		logic_SubGraph_SaveLoadInt_integer_210 = local_Stage2_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_210 = local_Stage2_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_210.Load(logic_SubGraph_SaveLoadInt_restartValue_210, ref logic_SubGraph_SaveLoadInt_integer_210, logic_SubGraph_SaveLoadInt_intAsVariable_210, logic_SubGraph_SaveLoadInt_uniqueID_210);
	}

	private void Relay_Restart_210()
	{
		logic_SubGraph_SaveLoadInt_integer_210 = local_Stage2_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_210 = local_Stage2_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_210.Restart(logic_SubGraph_SaveLoadInt_restartValue_210, ref logic_SubGraph_SaveLoadInt_integer_210, logic_SubGraph_SaveLoadInt_intAsVariable_210, logic_SubGraph_SaveLoadInt_uniqueID_210);
	}

	private void Relay_Save_Out_212()
	{
		Relay_Save_9();
	}

	private void Relay_Load_Out_212()
	{
		Relay_Load_9();
	}

	private void Relay_Restart_Out_212()
	{
		Relay_Set_False_9();
	}

	private void Relay_Save_212()
	{
		logic_SubGraph_SaveLoadInt_integer_212 = local_CurrentAmountType_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_212 = local_CurrentAmountType_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_212.Save(logic_SubGraph_SaveLoadInt_restartValue_212, ref logic_SubGraph_SaveLoadInt_integer_212, logic_SubGraph_SaveLoadInt_intAsVariable_212, logic_SubGraph_SaveLoadInt_uniqueID_212);
	}

	private void Relay_Load_212()
	{
		logic_SubGraph_SaveLoadInt_integer_212 = local_CurrentAmountType_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_212 = local_CurrentAmountType_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_212.Load(logic_SubGraph_SaveLoadInt_restartValue_212, ref logic_SubGraph_SaveLoadInt_integer_212, logic_SubGraph_SaveLoadInt_intAsVariable_212, logic_SubGraph_SaveLoadInt_uniqueID_212);
	}

	private void Relay_Restart_212()
	{
		logic_SubGraph_SaveLoadInt_integer_212 = local_CurrentAmountType_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_212 = local_CurrentAmountType_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_212.Restart(logic_SubGraph_SaveLoadInt_restartValue_212, ref logic_SubGraph_SaveLoadInt_integer_212, logic_SubGraph_SaveLoadInt_intAsVariable_212, logic_SubGraph_SaveLoadInt_uniqueID_212);
	}

	private void Relay_In_214()
	{
		logic_uScriptCon_CompareInt_A_214 = local_Stage_System_Int32;
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_214.In(logic_uScriptCon_CompareInt_A_214, logic_uScriptCon_CompareInt_B_214);
		if (logic_uScriptCon_CompareInt_uScriptCon_CompareInt_214.LessThanOrEqualTo)
		{
			Relay_In_216();
		}
	}

	private void Relay_In_216()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_216.In(logic_uScriptAct_SetInt_Value_216, out logic_uScriptAct_SetInt_Target_216);
		local_CurrentAmountType_System_Int32 = logic_uScriptAct_SetInt_Target_216;
		if (logic_uScriptAct_SetInt_uScriptAct_SetInt_216.Out)
		{
			Relay_In_58();
		}
	}

	private void Relay_In_220()
	{
		logic_uScriptCon_CompareInt_A_220 = local_Stage_System_Int32;
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_220.In(logic_uScriptCon_CompareInt_A_220, logic_uScriptCon_CompareInt_B_220);
		if (logic_uScriptCon_CompareInt_uScriptCon_CompareInt_220.EqualTo)
		{
			Relay_In_189();
		}
	}

	private void Relay_In_221()
	{
		logic_uScript_AddMessage_messageData_221 = msg04AllBlocksScrapped;
		logic_uScript_AddMessage_speaker_221 = messageSpeaker;
		logic_uScript_AddMessage_Return_221 = logic_uScript_AddMessage_uScript_AddMessage_221.In(logic_uScript_AddMessage_messageData_221, logic_uScript_AddMessage_speaker_221);
		if (logic_uScript_AddMessage_uScript_AddMessage_221.Shown)
		{
			Relay_True_222();
		}
	}

	private void Relay_True_222()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_222.True(out logic_uScriptAct_SetBool_Target_222);
		local_msgAllBlocksCraftedShown_System_Boolean = logic_uScriptAct_SetBool_Target_222;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_222.Out)
		{
			Relay_In_228();
		}
	}

	private void Relay_False_222()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_222.False(out logic_uScriptAct_SetBool_Target_222);
		local_msgAllBlocksCraftedShown_System_Boolean = logic_uScriptAct_SetBool_Target_222;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_222.Out)
		{
			Relay_In_228();
		}
	}

	private void Relay_In_223()
	{
		logic_uScriptCon_CompareBool_Bool_223 = local_msgAllBlocksCraftedShown_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_223.In(logic_uScriptCon_CompareBool_Bool_223);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_223.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_223.False;
		if (num)
		{
			Relay_In_228();
		}
		if (flag)
		{
			Relay_In_221();
		}
	}

	private void Relay_In_228()
	{
		logic_uScriptAct_AddInt_v2_A_228 = local_Stage2_System_Int32;
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_228.In(logic_uScriptAct_AddInt_v2_A_228, logic_uScriptAct_AddInt_v2_B_228, out logic_uScriptAct_AddInt_v2_IntResult_228, out logic_uScriptAct_AddInt_v2_FloatResult_228);
		local_Stage2_System_Int32 = logic_uScriptAct_AddInt_v2_IntResult_228;
	}

	private void Relay_Save_Out_230()
	{
		Relay_Save_30();
	}

	private void Relay_Load_Out_230()
	{
		Relay_Load_30();
	}

	private void Relay_Restart_Out_230()
	{
		Relay_Set_False_30();
	}

	private void Relay_Save_230()
	{
		logic_SubGraph_SaveLoadBool_boolean_230 = local_msgAllBlocksCraftedShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_230 = local_msgAllBlocksCraftedShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_230.Save(ref logic_SubGraph_SaveLoadBool_boolean_230, logic_SubGraph_SaveLoadBool_boolAsVariable_230, logic_SubGraph_SaveLoadBool_uniqueID_230);
	}

	private void Relay_Load_230()
	{
		logic_SubGraph_SaveLoadBool_boolean_230 = local_msgAllBlocksCraftedShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_230 = local_msgAllBlocksCraftedShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_230.Load(ref logic_SubGraph_SaveLoadBool_boolean_230, logic_SubGraph_SaveLoadBool_boolAsVariable_230, logic_SubGraph_SaveLoadBool_uniqueID_230);
	}

	private void Relay_Set_True_230()
	{
		logic_SubGraph_SaveLoadBool_boolean_230 = local_msgAllBlocksCraftedShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_230 = local_msgAllBlocksCraftedShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_230.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_230, logic_SubGraph_SaveLoadBool_boolAsVariable_230, logic_SubGraph_SaveLoadBool_uniqueID_230);
	}

	private void Relay_Set_False_230()
	{
		logic_SubGraph_SaveLoadBool_boolean_230 = local_msgAllBlocksCraftedShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_230 = local_msgAllBlocksCraftedShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_230.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_230, logic_SubGraph_SaveLoadBool_boolAsVariable_230, logic_SubGraph_SaveLoadBool_uniqueID_230);
	}

	private void Relay_In_233()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_233.In(logic_uScriptAct_SetInt_Value_233, out logic_uScriptAct_SetInt_Target_233);
		local_Stage2_System_Int32 = logic_uScriptAct_SetInt_Target_233;
		if (logic_uScriptAct_SetInt_uScriptAct_SetInt_233.Out)
		{
			Relay_In_41();
		}
	}

	private void Relay_In_514()
	{
		logic_uScript_DirectEnemiesOutOfEncounter_ownerNode_514 = owner_Connection_515;
		logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_514.In(logic_uScript_DirectEnemiesOutOfEncounter_ownerNode_514);
		if (logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_514.Out)
		{
			Relay_In_2();
		}
	}
}
