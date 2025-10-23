using System;
using BehaviorDesigner.Runtime;
using UnityEngine;

[Serializable]
[NodePath("Graphs")]
[FriendlyName("msgShownMinionDefeated", "")]
public class Mission_Grade1_Quiz_Mission_Template : uScriptLogic
{
	private GameObject parentGameObject;

	private uScript_GUI thisScriptsOnGuiListener;

	private bool m_RegisteredForEvents;

	public BlockTypes blockTypeButton1;

	public BlockTypes blockTypeButton2;

	public BlockTypes blockTypeButton3;

	public BlockTypes blockTypeButton4;

	public SpawnTechData[] buttonbase1SpawnData = new SpawnTechData[0];

	[Multiline(3)]
	public string ButtonBase1VFXSpawn = "";

	public SpawnTechData[] buttonbase2SpawnData = new SpawnTechData[0];

	[Multiline(3)]
	public string ButtonBase2VFXSpawn = "";

	public SpawnTechData[] buttonbase3SpawnData = new SpawnTechData[0];

	[Multiline(3)]
	public string ButtonBase3VFXSpawn = "";

	public SpawnTechData[] buttonbase4SpawnData = new SpawnTechData[0];

	[Multiline(3)]
	public string ButtonBase4VFXSpawn = "";

	[Multiline(3)]
	public string ButtonBasePosition = "";

	public float clearSceneryRadius;

	public float distNPCFound;

	public SpawnTechData[] EnemyTechData = new SpawnTechData[0];

	private Tank[] local_121_TankArray = new Tank[0];

	private string local_189_System_String = "";

	private string local_191_System_String = "Question:";

	private Tank[] local_26_TankArray = new Tank[0];

	private Tank[] local_57_TankArray = new Tank[0];

	private Tank[] local_84_TankArray = new Tank[0];

	private Tank[] local_94_TankArray = new Tank[0];

	private int local_Button1Value_System_Int32;

	private int local_Button2Value_System_Int32;

	private int local_Button3Value_System_Int32;

	private int local_Button4Value_System_Int32;

	private Tank local_ButtonBase1Tech_Tank;

	private Tank local_ButtonBase2Tech_Tank;

	private Tank local_ButtonBase3Tech_Tank;

	private Tank local_ButtonBase4Tech_Tank;

	private TankBlock local_ButtonBlock1_TankBlock;

	private TankBlock local_ButtonBlock2_TankBlock;

	private TankBlock local_ButtonBlock3_TankBlock;

	private TankBlock local_ButtonBlock4_TankBlock;

	private bool local_CanPushButtons_System_Boolean;

	private Tank[] local_Enemy_TankArray = new Tank[0];

	private bool local_Initialize_System_Boolean;

	private bool local_msgBaseFoundShown_System_Boolean;

	private bool local_msgIntro_System_Boolean;

	private bool local_NearNPC_System_Boolean;

	private Tank local_NPCTech_Tank;

	private bool local_Q1EnemyAlive_System_Boolean;

	private bool local_Q2EnemyAlive_System_Boolean;

	private bool local_Q3EnemyAlive_System_Boolean;

	private bool local_Question01WrongAnswers_System_Boolean;

	private bool local_Question02WrongAnswers_System_Boolean;

	private bool local_Question03WrongAnswers_System_Boolean;

	private int local_QuestionOne_System_Int32 = 1;

	private int local_QuestionsStage_System_Int32 = 1;

	private int local_QuestionStage_System_Int32 = 1;

	private int local_QuestionThree_System_Int32 = 1;

	private int local_QuestionTwo_System_Int32 = 1;

	private int local_Stage_System_Int32 = 1;

	public uScript_AddMessage.MessageSpeaker messageSpeaker;

	[Multiline(3)]
	public string messageTag = "";

	public uScript_AddMessage.MessageData msg01Intro;

	public uScript_AddMessage.MessageData msg02BaseFound;

	public uScript_AddMessage.MessageData msg03QuestionOne;

	public uScript_AddMessage.MessageData msg03QuestionOneCorrect;

	public uScript_AddMessage.MessageData msg04QuestionOneWrong;

	public uScript_AddMessage.MessageData msg05QuestionTwo;

	public uScript_AddMessage.MessageData msg06QuestionTwoCorrect;

	public uScript_AddMessage.MessageData msg07QuestionTwoWrong;

	public uScript_AddMessage.MessageData msg08QuestionThree;

	public uScript_AddMessage.MessageData msg09QuestionThreeCorrect;

	public uScript_AddMessage.MessageData msg10QuestionThreeWrong;

	public uScript_AddMessage.MessageData msgEncounterComplete;

	public uScript_AddMessage.MessageData msgLeavingMissionArea;

	public uScript_AddMessage.MessageData msgMinionDefeated;

	public uScript_AddMessage.MessageData msgSpawnMinion;

	public Transform NPCDespawnParticleEffect;

	public ExternalBehaviorTree NPCFlyAwayAI;

	public SpawnTechData[] NPCSpawnData = new SpawnTechData[0];

	public bool Q1Button1;

	public bool Q1Button2;

	public bool Q1Button3;

	public bool Q1Button4;

	public bool Q2Button1;

	public bool Q2Button2;

	public bool Q2Button3;

	public bool Q2Button4;

	public bool Q3Button1;

	public bool Q3Button2;

	public bool Q3Button3;

	public bool Q3Button4;

	public float WaitingTime;

	private GameObject owner_Connection_2;

	private GameObject owner_Connection_14;

	private GameObject owner_Connection_18;

	private GameObject owner_Connection_23;

	private GameObject owner_Connection_38;

	private GameObject owner_Connection_40;

	private GameObject owner_Connection_41;

	private GameObject owner_Connection_42;

	private GameObject owner_Connection_56;

	private GameObject owner_Connection_104;

	private GameObject owner_Connection_114;

	private GameObject owner_Connection_139;

	private GameObject owner_Connection_149;

	private GameObject owner_Connection_151;

	private GameObject owner_Connection_168;

	private GameObject owner_Connection_253;

	private GameObject owner_Connection_283;

	private GameObject owner_Connection_415;

	private GameObject owner_Connection_421;

	private GameObject owner_Connection_497;

	private GameObject owner_Connection_519;

	private GameObject owner_Connection_531;

	private GameObject owner_Connection_589;

	private GameObject owner_Connection_611;

	private GameObject owner_Connection_615;

	private GameObject owner_Connection_618;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_7 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_7;

	private bool logic_uScriptCon_CompareBool_True_7 = true;

	private bool logic_uScriptCon_CompareBool_False_7 = true;

	private uScript_SetCustomRadarTeamID logic_uScript_SetCustomRadarTeamID_uScript_SetCustomRadarTeamID_8 = new uScript_SetCustomRadarTeamID();

	private Tank logic_uScript_SetCustomRadarTeamID_tech_8;

	private int logic_uScript_SetCustomRadarTeamID_radarTeamID_8 = -2;

	private bool logic_uScript_SetCustomRadarTeamID_Out_8 = true;

	private uScript_SetTankInvulnerable logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_9 = new uScript_SetTankInvulnerable();

	private bool logic_uScript_SetTankInvulnerable_invulnerable_9 = true;

	private Tank logic_uScript_SetTankInvulnerable_tank_9;

	private bool logic_uScript_SetTankInvulnerable_Out_9 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_10 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_10 = true;

	private uScript_SetCustomRadarTeamID logic_uScript_SetCustomRadarTeamID_uScript_SetCustomRadarTeamID_12 = new uScript_SetCustomRadarTeamID();

	private Tank logic_uScript_SetCustomRadarTeamID_tech_12;

	private int logic_uScript_SetCustomRadarTeamID_radarTeamID_12 = -2;

	private bool logic_uScript_SetCustomRadarTeamID_Out_12 = true;

	private uScript_LockTechInteraction logic_uScript_LockTechInteraction_uScript_LockTechInteraction_20 = new uScript_LockTechInteraction();

	private Tank logic_uScript_LockTechInteraction_tech_20;

	private BlockTypes[] logic_uScript_LockTechInteraction_excludedBlocks_20 = new BlockTypes[0];

	private TankBlock[] logic_uScript_LockTechInteraction_excludedUniqueBlocks_20 = new TankBlock[0];

	private bool logic_uScript_LockTechInteraction_Out_20 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_21 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_21 = true;

	private uScript_LockTechInteraction logic_uScript_LockTechInteraction_uScript_LockTechInteraction_25 = new uScript_LockTechInteraction();

	private Tank logic_uScript_LockTechInteraction_tech_25;

	private BlockTypes[] logic_uScript_LockTechInteraction_excludedBlocks_25 = new BlockTypes[0];

	private TankBlock[] logic_uScript_LockTechInteraction_excludedUniqueBlocks_25 = new TankBlock[0];

	private bool logic_uScript_LockTechInteraction_Out_25 = true;

	private uScript_SetTankInvulnerable logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_28 = new uScript_SetTankInvulnerable();

	private bool logic_uScript_SetTankInvulnerable_invulnerable_28 = true;

	private Tank logic_uScript_SetTankInvulnerable_tank_28;

	private bool logic_uScript_SetTankInvulnerable_Out_28 = true;

	private uScript_PausePopulation logic_uScript_PausePopulation_uScript_PausePopulation_33 = new uScript_PausePopulation();

	private bool logic_uScript_PausePopulation_Out_33 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_34 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_34 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_36 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_36;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_36 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_36 = "CanPushButtons";

	private uScript_LockTechInteraction logic_uScript_LockTechInteraction_uScript_LockTechInteraction_37 = new uScript_LockTechInteraction();

	private Tank logic_uScript_LockTechInteraction_tech_37;

	private BlockTypes[] logic_uScript_LockTechInteraction_excludedBlocks_37 = new BlockTypes[0];

	private TankBlock[] logic_uScript_LockTechInteraction_excludedUniqueBlocks_37 = new TankBlock[0];

	private bool logic_uScript_LockTechInteraction_Out_37 = true;

	private uScript_LockTechInteraction logic_uScript_LockTechInteraction_uScript_LockTechInteraction_39 = new uScript_LockTechInteraction();

	private Tank logic_uScript_LockTechInteraction_tech_39;

	private BlockTypes[] logic_uScript_LockTechInteraction_excludedBlocks_39 = new BlockTypes[0];

	private TankBlock[] logic_uScript_LockTechInteraction_excludedUniqueBlocks_39 = new TankBlock[0];

	private bool logic_uScript_LockTechInteraction_Out_39 = true;

	private uScript_LockTech logic_uScript_LockTech_uScript_LockTech_44 = new uScript_LockTech();

	private Tank logic_uScript_LockTech_tech_44;

	private uScript_LockTech.TechLockType logic_uScript_LockTech_lockType_44 = uScript_LockTech.TechLockType.LockAll;

	private bool logic_uScript_LockTech_Out_44 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_45 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_45;

	private bool logic_uScriptAct_SetBool_Out_45 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_45 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_45 = true;

	private uScript_SetCustomRadarTeamID logic_uScript_SetCustomRadarTeamID_uScript_SetCustomRadarTeamID_46 = new uScript_SetCustomRadarTeamID();

	private Tank logic_uScript_SetCustomRadarTeamID_tech_46;

	private int logic_uScript_SetCustomRadarTeamID_radarTeamID_46 = -2;

	private bool logic_uScript_SetCustomRadarTeamID_Out_46 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_47 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_47 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_47;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_47 = 0.3f;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_47;

	private bool logic_uScript_SpawnTechsFromData_Out_47 = true;

	private uScript_LockTechInteraction logic_uScript_LockTechInteraction_uScript_LockTechInteraction_50 = new uScript_LockTechInteraction();

	private Tank logic_uScript_LockTechInteraction_tech_50;

	private BlockTypes[] logic_uScript_LockTechInteraction_excludedBlocks_50 = new BlockTypes[0];

	private TankBlock[] logic_uScript_LockTechInteraction_excludedUniqueBlocks_50 = new TankBlock[0];

	private bool logic_uScript_LockTechInteraction_Out_50 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_52 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_52 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_52;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_52 = 0.3f;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_52;

	private bool logic_uScript_SpawnTechsFromData_Out_52 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_54 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_54 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_54 = true;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_54 = true;

	private uScript_SetCustomRadarTeamID logic_uScript_SetCustomRadarTeamID_uScript_SetCustomRadarTeamID_55 = new uScript_SetCustomRadarTeamID();

	private Tank logic_uScript_SetCustomRadarTeamID_tech_55;

	private int logic_uScript_SetCustomRadarTeamID_radarTeamID_55 = -2;

	private bool logic_uScript_SetCustomRadarTeamID_Out_55 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_58 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_58;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_58 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_58 = "msgIntro";

	private uScript_GetTankBlock logic_uScript_GetTankBlock_uScript_GetTankBlock_61 = new uScript_GetTankBlock();

	private Tank logic_uScript_GetTankBlock_tank_61;

	private BlockTypes logic_uScript_GetTankBlock_blockType_61;

	private TankBlock logic_uScript_GetTankBlock_Return_61;

	private bool logic_uScript_GetTankBlock_Out_61 = true;

	private bool logic_uScript_GetTankBlock_Returned_61 = true;

	private bool logic_uScript_GetTankBlock_NotFound_61 = true;

	private uScript_GetTankBlock logic_uScript_GetTankBlock_uScript_GetTankBlock_64 = new uScript_GetTankBlock();

	private Tank logic_uScript_GetTankBlock_tank_64;

	private BlockTypes logic_uScript_GetTankBlock_blockType_64;

	private TankBlock logic_uScript_GetTankBlock_Return_64;

	private bool logic_uScript_GetTankBlock_Out_64 = true;

	private bool logic_uScript_GetTankBlock_Returned_64 = true;

	private bool logic_uScript_GetTankBlock_NotFound_64 = true;

	private uScript_GetTankBlock logic_uScript_GetTankBlock_uScript_GetTankBlock_71 = new uScript_GetTankBlock();

	private Tank logic_uScript_GetTankBlock_tank_71;

	private BlockTypes logic_uScript_GetTankBlock_blockType_71;

	private TankBlock logic_uScript_GetTankBlock_Return_71;

	private bool logic_uScript_GetTankBlock_Out_71 = true;

	private bool logic_uScript_GetTankBlock_Returned_71 = true;

	private bool logic_uScript_GetTankBlock_NotFound_71 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_72 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_72 = new Tank[0];

	private int logic_uScript_AccessListTech_index_72;

	private Tank logic_uScript_AccessListTech_value_72;

	private bool logic_uScript_AccessListTech_Out_72 = true;

	private uScript_SetTankHideBlockLimit logic_uScript_SetTankHideBlockLimit_uScript_SetTankHideBlockLimit_74 = new uScript_SetTankHideBlockLimit();

	private bool logic_uScript_SetTankHideBlockLimit_hidden_74 = true;

	private Tank logic_uScript_SetTankHideBlockLimit_tech_74;

	private bool logic_uScript_SetTankHideBlockLimit_Out_74 = true;

	private uScript_LockTechInteraction logic_uScript_LockTechInteraction_uScript_LockTechInteraction_75 = new uScript_LockTechInteraction();

	private Tank logic_uScript_LockTechInteraction_tech_75;

	private BlockTypes[] logic_uScript_LockTechInteraction_excludedBlocks_75 = new BlockTypes[0];

	private TankBlock[] logic_uScript_LockTechInteraction_excludedUniqueBlocks_75 = new TankBlock[0];

	private bool logic_uScript_LockTechInteraction_Out_75 = true;

	private uScript_SetCustomRadarTeamID logic_uScript_SetCustomRadarTeamID_uScript_SetCustomRadarTeamID_81 = new uScript_SetCustomRadarTeamID();

	private Tank logic_uScript_SetCustomRadarTeamID_tech_81;

	private int logic_uScript_SetCustomRadarTeamID_radarTeamID_81 = -2;

	private bool logic_uScript_SetCustomRadarTeamID_Out_81 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_82 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_82;

	private bool logic_uScriptAct_SetBool_Out_82 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_82 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_82 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_83 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_83 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_83;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_83 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_83;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_83 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_83 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_83 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_83 = true;

	private uScript_PausePopulation logic_uScript_PausePopulation_uScript_PausePopulation_86 = new uScript_PausePopulation();

	private bool logic_uScript_PausePopulation_Out_86 = true;

	private uScript_LockTech logic_uScript_LockTech_uScript_LockTech_89 = new uScript_LockTech();

	private Tank logic_uScript_LockTech_tech_89;

	private uScript_LockTech.TechLockType logic_uScript_LockTech_lockType_89 = uScript_LockTech.TechLockType.LockAll;

	private bool logic_uScript_LockTech_Out_89 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_90 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_90 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_91 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_91 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_91;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_91 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_91;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_91 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_91 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_91 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_91 = true;

	private uScript_SetTankInvulnerable logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_93 = new uScript_SetTankInvulnerable();

	private bool logic_uScript_SetTankInvulnerable_invulnerable_93 = true;

	private Tank logic_uScript_SetTankInvulnerable_tank_93;

	private bool logic_uScript_SetTankInvulnerable_Out_93 = true;

	private uScript_LockTech logic_uScript_LockTech_uScript_LockTech_95 = new uScript_LockTech();

	private Tank logic_uScript_LockTech_tech_95;

	private uScript_LockTech.TechLockType logic_uScript_LockTech_lockType_95 = uScript_LockTech.TechLockType.LockAll;

	private bool logic_uScript_LockTech_Out_95 = true;

	private uScript_LockTechInteraction logic_uScript_LockTechInteraction_uScript_LockTechInteraction_97 = new uScript_LockTechInteraction();

	private Tank logic_uScript_LockTechInteraction_tech_97;

	private BlockTypes[] logic_uScript_LockTechInteraction_excludedBlocks_97 = new BlockTypes[0];

	private TankBlock[] logic_uScript_LockTechInteraction_excludedUniqueBlocks_97 = new TankBlock[0];

	private bool logic_uScript_LockTechInteraction_Out_97 = true;

	private uScript_IsPlayerInRangeOfTech logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_101 = new uScript_IsPlayerInRangeOfTech();

	private Tank logic_uScript_IsPlayerInRangeOfTech_tech_101;

	private float logic_uScript_IsPlayerInRangeOfTech_range_101;

	private Tank[] logic_uScript_IsPlayerInRangeOfTech_techs_101 = new Tank[0];

	private bool logic_uScript_IsPlayerInRangeOfTech_Out_101 = true;

	private bool logic_uScript_IsPlayerInRangeOfTech_InRange_101 = true;

	private bool logic_uScript_IsPlayerInRangeOfTech_OutOfRange_101 = true;

	private SubGraph_SaveLoadInt logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_102 = new SubGraph_SaveLoadInt();

	private int logic_SubGraph_SaveLoadInt_restartValue_102 = 1;

	private int logic_SubGraph_SaveLoadInt_integer_102;

	private object logic_SubGraph_SaveLoadInt_intAsVariable_102 = "";

	private string logic_SubGraph_SaveLoadInt_uniqueID_102 = "Stage3QuestionTwo";

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_105 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_105 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_105;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_105 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_105;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_105 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_105 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_105 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_105 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_108 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_108;

	private bool logic_uScriptCon_CompareBool_True_108 = true;

	private bool logic_uScriptCon_CompareBool_False_108 = true;

	private uScript_GetTankBlock logic_uScript_GetTankBlock_uScript_GetTankBlock_113 = new uScript_GetTankBlock();

	private Tank logic_uScript_GetTankBlock_tank_113;

	private BlockTypes logic_uScript_GetTankBlock_blockType_113;

	private TankBlock logic_uScript_GetTankBlock_Return_113;

	private bool logic_uScript_GetTankBlock_Out_113 = true;

	private bool logic_uScript_GetTankBlock_Returned_113 = true;

	private bool logic_uScript_GetTankBlock_NotFound_113 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_115 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_115 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_116 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_116 = new Tank[0];

	private int logic_uScript_AccessListTech_index_116;

	private Tank logic_uScript_AccessListTech_value_116;

	private bool logic_uScript_AccessListTech_Out_116 = true;

	private uScript_SetEncounterTarget logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_117 = new uScript_SetEncounterTarget();

	private GameObject logic_uScript_SetEncounterTarget_owner_117;

	private object logic_uScript_SetEncounterTarget_visibleObject_117 = "";

	private bool logic_uScript_SetEncounterTarget_Out_117 = true;

	private uScript_LockTech logic_uScript_LockTech_uScript_LockTech_119 = new uScript_LockTech();

	private Tank logic_uScript_LockTech_tech_119;

	private uScript_LockTech.TechLockType logic_uScript_LockTech_lockType_119 = uScript_LockTech.TechLockType.LockAll;

	private bool logic_uScript_LockTech_Out_119 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_122 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_122 = new Tank[0];

	private int logic_uScript_AccessListTech_index_122;

	private Tank logic_uScript_AccessListTech_value_122;

	private bool logic_uScript_AccessListTech_Out_122 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_124 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_124 = new Tank[0];

	private int logic_uScript_AccessListTech_index_124;

	private Tank logic_uScript_AccessListTech_value_124;

	private bool logic_uScript_AccessListTech_Out_124 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_128 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_128 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_128;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_128 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_128;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_128 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_128 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_128 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_128 = true;

	private uScript_SetTankInvulnerable logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_129 = new uScript_SetTankInvulnerable();

	private bool logic_uScript_SetTankInvulnerable_invulnerable_129 = true;

	private Tank logic_uScript_SetTankInvulnerable_tank_129;

	private bool logic_uScript_SetTankInvulnerable_Out_129 = true;

	private uScript_SetTankHideBlockLimit logic_uScript_SetTankHideBlockLimit_uScript_SetTankHideBlockLimit_130 = new uScript_SetTankHideBlockLimit();

	private bool logic_uScript_SetTankHideBlockLimit_hidden_130 = true;

	private Tank logic_uScript_SetTankHideBlockLimit_tech_130;

	private bool logic_uScript_SetTankHideBlockLimit_Out_130 = true;

	private uScript_SetTankHideBlockLimit logic_uScript_SetTankHideBlockLimit_uScript_SetTankHideBlockLimit_132 = new uScript_SetTankHideBlockLimit();

	private bool logic_uScript_SetTankHideBlockLimit_hidden_132 = true;

	private Tank logic_uScript_SetTankHideBlockLimit_tech_132;

	private bool logic_uScript_SetTankHideBlockLimit_Out_132 = true;

	private uScript_RemoveScenery logic_uScript_RemoveScenery_uScript_RemoveScenery_133 = new uScript_RemoveScenery();

	private GameObject logic_uScript_RemoveScenery_ownerNode_133;

	private string logic_uScript_RemoveScenery_positionName_133 = "";

	private float logic_uScript_RemoveScenery_radius_133;

	private bool logic_uScript_RemoveScenery_preventChunksSpawning_133 = true;

	private bool logic_uScript_RemoveScenery_Out_133 = true;

	private uScript_LockTechInteraction logic_uScript_LockTechInteraction_uScript_LockTechInteraction_136 = new uScript_LockTechInteraction();

	private Tank logic_uScript_LockTechInteraction_tech_136;

	private BlockTypes[] logic_uScript_LockTechInteraction_excludedBlocks_136 = new BlockTypes[0];

	private TankBlock[] logic_uScript_LockTechInteraction_excludedUniqueBlocks_136 = new TankBlock[0];

	private bool logic_uScript_LockTechInteraction_Out_136 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_138 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_138 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_140 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_140 = new Tank[0];

	private int logic_uScript_AccessListTech_index_140;

	private Tank logic_uScript_AccessListTech_value_140;

	private bool logic_uScript_AccessListTech_Out_140 = true;

	private uScript_LockTech logic_uScript_LockTech_uScript_LockTech_143 = new uScript_LockTech();

	private Tank logic_uScript_LockTech_tech_143;

	private uScript_LockTech.TechLockType logic_uScript_LockTech_lockType_143 = uScript_LockTech.TechLockType.LockAll;

	private bool logic_uScript_LockTech_Out_143 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_144 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_144;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_144;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_144;

	private bool logic_uScript_AddMessage_Out_144 = true;

	private bool logic_uScript_AddMessage_Shown_144 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_147 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_147;

	private bool logic_uScriptCon_CompareBool_True_147 = true;

	private bool logic_uScriptCon_CompareBool_False_147 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_148 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_148 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_150 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_150;

	private bool logic_uScriptAct_SetBool_Out_150 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_150 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_150 = true;

	private SubGraph_SaveLoadInt logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_153 = new SubGraph_SaveLoadInt();

	private int logic_SubGraph_SaveLoadInt_restartValue_153 = 1;

	private int logic_SubGraph_SaveLoadInt_integer_153;

	private object logic_SubGraph_SaveLoadInt_intAsVariable_153 = "";

	private string logic_SubGraph_SaveLoadInt_uniqueID_153 = "Stage";

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_157 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_157 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_158 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_158 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_158;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_158 = 0.3f;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_158;

	private bool logic_uScript_SpawnTechsFromData_Out_158 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_159 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_159 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_162 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_162;

	private bool logic_uScriptCon_CompareBool_True_162 = true;

	private bool logic_uScriptCon_CompareBool_False_162 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_163 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_163 = true;

	private SubGraph_SaveLoadInt logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_167 = new SubGraph_SaveLoadInt();

	private int logic_SubGraph_SaveLoadInt_restartValue_167 = 1;

	private int logic_SubGraph_SaveLoadInt_integer_167;

	private object logic_SubGraph_SaveLoadInt_intAsVariable_167 = "";

	private string logic_SubGraph_SaveLoadInt_uniqueID_167 = "Stage2QuestionOne";

	private uScript_DirectEnemiesOutOfEncounter logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_171 = new uScript_DirectEnemiesOutOfEncounter();

	private GameObject logic_uScript_DirectEnemiesOutOfEncounter_ownerNode_171;

	private bool logic_uScript_DirectEnemiesOutOfEncounter_Out_171 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_172 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_172 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_172;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_172 = 0.3f;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_172;

	private bool logic_uScript_SpawnTechsFromData_Out_172 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_173 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_173 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_173;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_173 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_173;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_173 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_173 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_173 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_173 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_174 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_174 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_174;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_174 = 0.3f;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_174;

	private bool logic_uScript_SpawnTechsFromData_Out_174 = true;

	private uScript_SetTankHideBlockLimit logic_uScript_SetTankHideBlockLimit_uScript_SetTankHideBlockLimit_175 = new uScript_SetTankHideBlockLimit();

	private bool logic_uScript_SetTankHideBlockLimit_hidden_175 = true;

	private Tank logic_uScript_SetTankHideBlockLimit_tech_175;

	private bool logic_uScript_SetTankHideBlockLimit_Out_175 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_176 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_176;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_176 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_176 = "Initialize";

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_177 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_177;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_177;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_177;

	private bool logic_uScript_AddMessage_Out_177 = true;

	private bool logic_uScript_AddMessage_Shown_177 = true;

	private SubGraph_SaveLoadInt logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_178 = new SubGraph_SaveLoadInt();

	private int logic_SubGraph_SaveLoadInt_restartValue_178 = 1;

	private int logic_SubGraph_SaveLoadInt_integer_178;

	private object logic_SubGraph_SaveLoadInt_intAsVariable_178 = "";

	private string logic_SubGraph_SaveLoadInt_uniqueID_178 = "Stage4QuestionThree";

	private uScript_LockTechInteraction logic_uScript_LockTechInteraction_uScript_LockTechInteraction_180 = new uScript_LockTechInteraction();

	private Tank logic_uScript_LockTechInteraction_tech_180;

	private BlockTypes[] logic_uScript_LockTechInteraction_excludedBlocks_180 = new BlockTypes[0];

	private TankBlock[] logic_uScript_LockTechInteraction_excludedUniqueBlocks_180 = new TankBlock[0];

	private bool logic_uScript_LockTechInteraction_Out_180 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_181 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_181 = true;

	private uScript_SetTankInvulnerable logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_185 = new uScript_SetTankInvulnerable();

	private bool logic_uScript_SetTankInvulnerable_invulnerable_185 = true;

	private Tank logic_uScript_SetTankInvulnerable_tank_185;

	private bool logic_uScript_SetTankInvulnerable_Out_185 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_186 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_186;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_186 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_186 = "Q1EnemyAlive";

	private uScriptAct_Concatenate logic_uScriptAct_Concatenate_uScriptAct_Concatenate_188 = new uScriptAct_Concatenate();

	private object[] logic_uScriptAct_Concatenate_A_188 = new object[0];

	private object[] logic_uScriptAct_Concatenate_B_188 = new object[0];

	private string logic_uScriptAct_Concatenate_Separator_188 = "";

	private string logic_uScriptAct_Concatenate_Result_188;

	private bool logic_uScriptAct_Concatenate_Out_188 = true;

	private uScriptAct_PrintText logic_uScriptAct_PrintText_uScriptAct_PrintText_190 = new uScriptAct_PrintText();

	private string logic_uScriptAct_PrintText_Text_190 = "";

	private int logic_uScriptAct_PrintText_FontSize_190 = 16;

	private FontStyle logic_uScriptAct_PrintText_FontStyle_190;

	private Color logic_uScriptAct_PrintText_FontColor_190 = new Color(0f, 0f, 0f, 1f);

	private TextAnchor logic_uScriptAct_PrintText_textAnchor_190;

	private int logic_uScriptAct_PrintText_EdgePadding_190 = 8;

	private float logic_uScriptAct_PrintText_time_190;

	private bool logic_uScriptAct_PrintText_Out_190 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_192 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_192;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_192 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_192 = "Q2EnemyAlive";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_194 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_194;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_194 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_194 = "Q3EnemyAlive";

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_196 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_196;

	private bool logic_uScriptAct_SetBool_Out_196 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_196 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_196 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_200 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_200;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_200 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_200 = "Q1Wrong Answers";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_202 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_202;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_202 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_202 = "Q2Wrong Answers";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_203 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_203;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_203 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_203 = "Q3Wrong Answers";

	private SubGraph_LoadObjectiveStates logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_206 = new SubGraph_LoadObjectiveStates();

	private int logic_SubGraph_LoadObjectiveStates_currentObjective_206;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_208 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_208;

	private bool logic_uScriptAct_SetBool_Out_208 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_208 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_208 = true;

	private uScript_IsPlayerInRangeOfTech logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_209 = new uScript_IsPlayerInRangeOfTech();

	private Tank logic_uScript_IsPlayerInRangeOfTech_tech_209;

	private float logic_uScript_IsPlayerInRangeOfTech_range_209 = 100f;

	private Tank[] logic_uScript_IsPlayerInRangeOfTech_techs_209 = new Tank[0];

	private bool logic_uScript_IsPlayerInRangeOfTech_Out_209 = true;

	private bool logic_uScript_IsPlayerInRangeOfTech_InRange_209 = true;

	private bool logic_uScript_IsPlayerInRangeOfTech_OutOfRange_209 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_212 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_212;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_212 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_212 = "MsgBaseFound";

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_214 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_214;

	private bool logic_uScriptCon_CompareBool_True_214 = true;

	private bool logic_uScriptCon_CompareBool_False_214 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_218 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_218;

	private bool logic_uScriptAct_SetBool_Out_218 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_218 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_218 = true;

	private SubGraph_CompleteObjectiveStage logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_220 = new SubGraph_CompleteObjectiveStage();

	private int logic_SubGraph_CompleteObjectiveStage_objectiveStage_220;

	private bool logic_SubGraph_CompleteObjectiveStage_isFinalObjective_220;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_221 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_221;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_221;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_221;

	private bool logic_uScript_AddMessage_Out_221 = true;

	private bool logic_uScript_AddMessage_Shown_221 = true;

	private uScriptCon_ManualSwitch logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_222 = new uScriptCon_ManualSwitch();

	private int logic_uScriptCon_ManualSwitch_CurrentOutput_222;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_231 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_231 = 4;

	private int logic_uScriptAct_SetInt_Target_231;

	private bool logic_uScriptAct_SetInt_Out_231 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_232 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_232;

	private bool logic_uScriptAct_SetBool_Out_232 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_232 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_232 = true;

	private uScriptAct_AddInt_v2 logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_234 = new uScriptAct_AddInt_v2();

	private int logic_uScriptAct_AddInt_v2_A_234;

	private int logic_uScriptAct_AddInt_v2_B_234 = 1;

	private int logic_uScriptAct_AddInt_v2_IntResult_234;

	private float logic_uScriptAct_AddInt_v2_FloatResult_234;

	private bool logic_uScriptAct_AddInt_v2_Out_234 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_235 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_235 = 4;

	private int logic_uScriptAct_SetInt_Target_235;

	private bool logic_uScriptAct_SetInt_Out_235 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_238 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_238;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_238;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_238;

	private bool logic_uScript_AddMessage_Out_238 = true;

	private bool logic_uScript_AddMessage_Shown_238 = true;

	private uScriptCon_CompareInt logic_uScriptCon_CompareInt_uScriptCon_CompareInt_239 = new uScriptCon_CompareInt();

	private int logic_uScriptCon_CompareInt_A_239;

	private int logic_uScriptCon_CompareInt_B_239;

	private bool logic_uScriptCon_CompareInt_GreaterThan_239 = true;

	private bool logic_uScriptCon_CompareInt_GreaterThanOrEqualTo_239 = true;

	private bool logic_uScriptCon_CompareInt_EqualTo_239 = true;

	private bool logic_uScriptCon_CompareInt_NotEqualTo_239 = true;

	private bool logic_uScriptCon_CompareInt_LessThanOrEqualTo_239 = true;

	private bool logic_uScriptCon_CompareInt_LessThan_239 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_241 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_241 = 4;

	private int logic_uScriptAct_SetInt_Target_241;

	private bool logic_uScriptAct_SetInt_Out_241 = true;

	private uScript_Wait logic_uScript_Wait_uScript_Wait_242 = new uScript_Wait();

	private float logic_uScript_Wait_seconds_242;

	private bool logic_uScript_Wait_repeat_242 = true;

	private bool logic_uScript_Wait_Waited_242 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_243 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_243 = 3;

	private int logic_uScriptAct_SetInt_Target_243;

	private bool logic_uScriptAct_SetInt_Out_243 = true;

	private uScriptCon_ManualSwitch logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_245 = new uScriptCon_ManualSwitch();

	private int logic_uScriptCon_ManualSwitch_CurrentOutput_245;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_249 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_249;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_249;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_249;

	private bool logic_uScript_AddMessage_Out_249 = true;

	private bool logic_uScript_AddMessage_Shown_249 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_254 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_254;

	private bool logic_uScriptCon_CompareBool_True_254 = true;

	private bool logic_uScriptCon_CompareBool_False_254 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_255 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_255;

	private bool logic_uScriptAct_SetBool_Out_255 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_255 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_255 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_256 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_256;

	private bool logic_uScriptAct_SetBool_Out_256 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_256 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_256 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_257 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_257;

	private bool logic_uScriptCon_CompareBool_True_257 = true;

	private bool logic_uScriptCon_CompareBool_False_257 = true;

	private uScriptAct_AddInt_v2 logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_258 = new uScriptAct_AddInt_v2();

	private int logic_uScriptAct_AddInt_v2_A_258;

	private int logic_uScriptAct_AddInt_v2_B_258 = 1;

	private int logic_uScriptAct_AddInt_v2_IntResult_258;

	private float logic_uScriptAct_AddInt_v2_FloatResult_258;

	private bool logic_uScriptAct_AddInt_v2_Out_258 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_261 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_261;

	private bool logic_uScriptAct_SetBool_Out_261 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_261 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_261 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_265 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_265;

	private bool logic_uScriptAct_SetBool_Out_265 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_265 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_265 = true;

	private uScriptCon_CompareInt logic_uScriptCon_CompareInt_uScriptCon_CompareInt_267 = new uScriptCon_CompareInt();

	private int logic_uScriptCon_CompareInt_A_267;

	private int logic_uScriptCon_CompareInt_B_267;

	private bool logic_uScriptCon_CompareInt_GreaterThan_267 = true;

	private bool logic_uScriptCon_CompareInt_GreaterThanOrEqualTo_267 = true;

	private bool logic_uScriptCon_CompareInt_EqualTo_267 = true;

	private bool logic_uScriptCon_CompareInt_NotEqualTo_267 = true;

	private bool logic_uScriptCon_CompareInt_LessThanOrEqualTo_267 = true;

	private bool logic_uScriptCon_CompareInt_LessThan_267 = true;

	private uScriptCon_CompareInt logic_uScriptCon_CompareInt_uScriptCon_CompareInt_271 = new uScriptCon_CompareInt();

	private int logic_uScriptCon_CompareInt_A_271;

	private int logic_uScriptCon_CompareInt_B_271;

	private bool logic_uScriptCon_CompareInt_GreaterThan_271 = true;

	private bool logic_uScriptCon_CompareInt_GreaterThanOrEqualTo_271 = true;

	private bool logic_uScriptCon_CompareInt_EqualTo_271 = true;

	private bool logic_uScriptCon_CompareInt_NotEqualTo_271 = true;

	private bool logic_uScriptCon_CompareInt_LessThanOrEqualTo_271 = true;

	private bool logic_uScriptCon_CompareInt_LessThan_271 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_275 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_275;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_275;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_275;

	private bool logic_uScript_AddMessage_Out_275 = true;

	private bool logic_uScript_AddMessage_Shown_275 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_276 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_276 = 2;

	private int logic_uScriptAct_SetInt_Target_276;

	private bool logic_uScriptAct_SetInt_Out_276 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_277 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_277 = 3;

	private int logic_uScriptAct_SetInt_Target_277;

	private bool logic_uScriptAct_SetInt_Out_277 = true;

	private uScript_GetCircuitChargeInfo logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_279 = new uScript_GetCircuitChargeInfo();

	private TankBlock logic_uScript_GetCircuitChargeInfo_block_279;

	private Tank logic_uScript_GetCircuitChargeInfo_tech_279;

	private BlockTypes logic_uScript_GetCircuitChargeInfo_blockType_279;

	private int logic_uScript_GetCircuitChargeInfo_Return_279;

	private bool logic_uScript_GetCircuitChargeInfo_Out_279 = true;

	private bool logic_uScript_GetCircuitChargeInfo_HasValidBlock_279 = true;

	private uScriptCon_ManualSwitch logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_280 = new uScriptCon_ManualSwitch();

	private int logic_uScriptCon_ManualSwitch_CurrentOutput_280;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_284 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_284;

	private bool logic_uScriptAct_SetBool_Out_284 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_284 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_284 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_288 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_288 = 1;

	private int logic_uScriptAct_SetInt_Target_288;

	private bool logic_uScriptAct_SetInt_Out_288 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_289 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_289;

	private bool logic_uScriptCon_CompareBool_True_289 = true;

	private bool logic_uScriptCon_CompareBool_False_289 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_292 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_292;

	private bool logic_uScriptAct_SetBool_Out_292 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_292 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_292 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_294 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_294;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_294;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_294;

	private bool logic_uScript_AddMessage_Out_294 = true;

	private bool logic_uScript_AddMessage_Shown_294 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_295 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_295;

	private bool logic_uScriptAct_SetBool_Out_295 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_295 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_295 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_296 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_296;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_296;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_296;

	private bool logic_uScript_AddMessage_Out_296 = true;

	private bool logic_uScript_AddMessage_Shown_296 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_297 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_297;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_297;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_297;

	private bool logic_uScript_AddMessage_Out_297 = true;

	private bool logic_uScript_AddMessage_Shown_297 = true;

	private uScriptCon_CompareInt logic_uScriptCon_CompareInt_uScriptCon_CompareInt_299 = new uScriptCon_CompareInt();

	private int logic_uScriptCon_CompareInt_A_299;

	private int logic_uScriptCon_CompareInt_B_299;

	private bool logic_uScriptCon_CompareInt_GreaterThan_299 = true;

	private bool logic_uScriptCon_CompareInt_GreaterThanOrEqualTo_299 = true;

	private bool logic_uScriptCon_CompareInt_EqualTo_299 = true;

	private bool logic_uScriptCon_CompareInt_NotEqualTo_299 = true;

	private bool logic_uScriptCon_CompareInt_LessThanOrEqualTo_299 = true;

	private bool logic_uScriptCon_CompareInt_LessThan_299 = true;

	private uScript_GetCircuitChargeInfo logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_302 = new uScript_GetCircuitChargeInfo();

	private TankBlock logic_uScript_GetCircuitChargeInfo_block_302;

	private Tank logic_uScript_GetCircuitChargeInfo_tech_302;

	private BlockTypes logic_uScript_GetCircuitChargeInfo_blockType_302;

	private int logic_uScript_GetCircuitChargeInfo_Return_302;

	private bool logic_uScript_GetCircuitChargeInfo_Out_302 = true;

	private bool logic_uScript_GetCircuitChargeInfo_HasValidBlock_302 = true;

	private uScript_GetCircuitChargeInfo logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_305 = new uScript_GetCircuitChargeInfo();

	private TankBlock logic_uScript_GetCircuitChargeInfo_block_305;

	private Tank logic_uScript_GetCircuitChargeInfo_tech_305;

	private BlockTypes logic_uScript_GetCircuitChargeInfo_blockType_305;

	private int logic_uScript_GetCircuitChargeInfo_Return_305;

	private bool logic_uScript_GetCircuitChargeInfo_Out_305 = true;

	private bool logic_uScript_GetCircuitChargeInfo_HasValidBlock_305 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_307 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_307;

	private bool logic_uScriptAct_SetBool_Out_307 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_307 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_307 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_308 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_308 = 1;

	private int logic_uScriptAct_SetInt_Target_308;

	private bool logic_uScriptAct_SetInt_Out_308 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_310 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_310;

	private bool logic_uScriptCon_CompareBool_True_310 = true;

	private bool logic_uScriptCon_CompareBool_False_310 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_311 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_311 = 3;

	private int logic_uScriptAct_SetInt_Target_311;

	private bool logic_uScriptAct_SetInt_Out_311 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_314 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_314;

	private bool logic_uScriptAct_SetBool_Out_314 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_314 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_314 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_315 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_315;

	private bool logic_uScriptCon_CompareBool_True_315 = true;

	private bool logic_uScriptCon_CompareBool_False_315 = true;

	private uScriptCon_CompareInt logic_uScriptCon_CompareInt_uScriptCon_CompareInt_316 = new uScriptCon_CompareInt();

	private int logic_uScriptCon_CompareInt_A_316;

	private int logic_uScriptCon_CompareInt_B_316;

	private bool logic_uScriptCon_CompareInt_GreaterThan_316 = true;

	private bool logic_uScriptCon_CompareInt_GreaterThanOrEqualTo_316 = true;

	private bool logic_uScriptCon_CompareInt_EqualTo_316 = true;

	private bool logic_uScriptCon_CompareInt_NotEqualTo_316 = true;

	private bool logic_uScriptCon_CompareInt_LessThanOrEqualTo_316 = true;

	private bool logic_uScriptCon_CompareInt_LessThan_316 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_317 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_317;

	private bool logic_uScriptAct_SetBool_Out_317 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_317 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_317 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_320 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_320;

	private bool logic_uScriptAct_SetBool_Out_320 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_320 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_320 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_321 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_321 = 3;

	private int logic_uScriptAct_SetInt_Target_321;

	private bool logic_uScriptAct_SetInt_Out_321 = true;

	private uScript_GetCircuitChargeInfo logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_323 = new uScript_GetCircuitChargeInfo();

	private TankBlock logic_uScript_GetCircuitChargeInfo_block_323;

	private Tank logic_uScript_GetCircuitChargeInfo_tech_323;

	private BlockTypes logic_uScript_GetCircuitChargeInfo_blockType_323;

	private int logic_uScript_GetCircuitChargeInfo_Return_323;

	private bool logic_uScript_GetCircuitChargeInfo_Out_323 = true;

	private bool logic_uScript_GetCircuitChargeInfo_HasValidBlock_323 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_324 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_324;

	private bool logic_uScriptAct_SetBool_Out_324 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_324 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_324 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_327 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_327;

	private bool logic_uScriptAct_SetBool_Out_327 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_327 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_327 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_330 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_330 = 1;

	private int logic_uScriptAct_SetInt_Target_330;

	private bool logic_uScriptAct_SetInt_Out_330 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_334 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_334 = 1;

	private int logic_uScriptAct_SetInt_Target_334;

	private bool logic_uScriptAct_SetInt_Out_334 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_344 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_344;

	private bool logic_uScriptAct_SetBool_Out_344 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_344 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_344 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_345 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_345;

	private bool logic_uScriptAct_SetBool_Out_345 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_345 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_345 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_347 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_347;

	private bool logic_uScriptAct_SetBool_Out_347 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_347 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_347 = true;

	private uScript_GetCircuitChargeInfo logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_351 = new uScript_GetCircuitChargeInfo();

	private TankBlock logic_uScript_GetCircuitChargeInfo_block_351;

	private Tank logic_uScript_GetCircuitChargeInfo_tech_351;

	private BlockTypes logic_uScript_GetCircuitChargeInfo_blockType_351;

	private int logic_uScript_GetCircuitChargeInfo_Return_351;

	private bool logic_uScript_GetCircuitChargeInfo_Out_351 = true;

	private bool logic_uScript_GetCircuitChargeInfo_HasValidBlock_351 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_352 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_352 = 4;

	private int logic_uScriptAct_SetInt_Target_352;

	private bool logic_uScriptAct_SetInt_Out_352 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_353 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_353 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_353;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_353;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_353 = true;

	private bool logic_uScript_SpawnTechsFromData_Out_353 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_355 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_355;

	private bool logic_uScriptAct_SetBool_Out_355 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_355 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_355 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_356 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_356;

	private bool logic_uScriptAct_SetBool_Out_356 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_356 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_356 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_357 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_357 = 4;

	private int logic_uScriptAct_SetInt_Target_357;

	private bool logic_uScriptAct_SetInt_Out_357 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_359 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_359;

	private bool logic_uScriptAct_SetBool_Out_359 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_359 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_359 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_360 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_360;

	private bool logic_uScriptAct_SetBool_Out_360 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_360 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_360 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_364 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_364;

	private bool logic_uScriptAct_SetBool_Out_364 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_364 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_364 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_365 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_365;

	private bool logic_uScriptCon_CompareBool_True_365 = true;

	private bool logic_uScriptCon_CompareBool_False_365 = true;

	private uScript_Wait logic_uScript_Wait_uScript_Wait_368 = new uScript_Wait();

	private float logic_uScript_Wait_seconds_368;

	private bool logic_uScript_Wait_repeat_368 = true;

	private bool logic_uScript_Wait_Waited_368 = true;

	private uScriptCon_CompareInt logic_uScriptCon_CompareInt_uScriptCon_CompareInt_369 = new uScriptCon_CompareInt();

	private int logic_uScriptCon_CompareInt_A_369;

	private int logic_uScriptCon_CompareInt_B_369;

	private bool logic_uScriptCon_CompareInt_GreaterThan_369 = true;

	private bool logic_uScriptCon_CompareInt_GreaterThanOrEqualTo_369 = true;

	private bool logic_uScriptCon_CompareInt_EqualTo_369 = true;

	private bool logic_uScriptCon_CompareInt_NotEqualTo_369 = true;

	private bool logic_uScriptCon_CompareInt_LessThanOrEqualTo_369 = true;

	private bool logic_uScriptCon_CompareInt_LessThan_369 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_370 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_370 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_370;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_370 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_370;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_370 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_370 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_370 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_370 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_375 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_375;

	private bool logic_uScriptCon_CompareBool_True_375 = true;

	private bool logic_uScriptCon_CompareBool_False_375 = true;

	private uScript_Wait logic_uScript_Wait_uScript_Wait_379 = new uScript_Wait();

	private float logic_uScript_Wait_seconds_379;

	private bool logic_uScript_Wait_repeat_379 = true;

	private bool logic_uScript_Wait_Waited_379 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_381 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_381;

	private bool logic_uScriptAct_SetBool_Out_381 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_381 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_381 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_382 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_382;

	private bool logic_uScriptAct_SetBool_Out_382 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_382 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_382 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_383 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_383;

	private bool logic_uScriptAct_SetBool_Out_383 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_383 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_383 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_385 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_385;

	private bool logic_uScriptCon_CompareBool_True_385 = true;

	private bool logic_uScriptCon_CompareBool_False_385 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_388 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_388;

	private bool logic_uScriptAct_SetBool_Out_388 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_388 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_388 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_389 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_389 = 1;

	private int logic_uScriptAct_SetInt_Target_389;

	private bool logic_uScriptAct_SetInt_Out_389 = true;

	private uScript_Wait logic_uScript_Wait_uScript_Wait_396 = new uScript_Wait();

	private float logic_uScript_Wait_seconds_396;

	private bool logic_uScript_Wait_repeat_396 = true;

	private bool logic_uScript_Wait_Waited_396 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_400 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_400;

	private bool logic_uScriptAct_SetBool_Out_400 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_400 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_400 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_407 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_407;

	private bool logic_uScriptAct_SetBool_Out_407 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_407 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_407 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_410 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_410 = 3;

	private int logic_uScriptAct_SetInt_Target_410;

	private bool logic_uScriptAct_SetInt_Out_410 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_411 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_411 = 3;

	private int logic_uScriptAct_SetInt_Target_411;

	private bool logic_uScriptAct_SetInt_Out_411 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_412 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_412 = 1;

	private int logic_uScriptAct_SetInt_Target_412;

	private bool logic_uScriptAct_SetInt_Out_412 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_413 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_413 = 3;

	private int logic_uScriptAct_SetInt_Target_413;

	private bool logic_uScriptAct_SetInt_Out_413 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_416 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_416;

	private bool logic_uScriptAct_SetBool_Out_416 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_416 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_416 = true;

	private uScriptAct_AddInt_v2 logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_417 = new uScriptAct_AddInt_v2();

	private int logic_uScriptAct_AddInt_v2_A_417;

	private int logic_uScriptAct_AddInt_v2_B_417 = 1;

	private int logic_uScriptAct_AddInt_v2_IntResult_417;

	private float logic_uScriptAct_AddInt_v2_FloatResult_417;

	private bool logic_uScriptAct_AddInt_v2_Out_417 = true;

	private uScriptCon_CompareInt logic_uScriptCon_CompareInt_uScriptCon_CompareInt_418 = new uScriptCon_CompareInt();

	private int logic_uScriptCon_CompareInt_A_418;

	private int logic_uScriptCon_CompareInt_B_418;

	private bool logic_uScriptCon_CompareInt_GreaterThan_418 = true;

	private bool logic_uScriptCon_CompareInt_GreaterThanOrEqualTo_418 = true;

	private bool logic_uScriptCon_CompareInt_EqualTo_418 = true;

	private bool logic_uScriptCon_CompareInt_NotEqualTo_418 = true;

	private bool logic_uScriptCon_CompareInt_LessThanOrEqualTo_418 = true;

	private bool logic_uScriptCon_CompareInt_LessThan_418 = true;

	private uScriptCon_ManualSwitch logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_422 = new uScriptCon_ManualSwitch();

	private int logic_uScriptCon_ManualSwitch_CurrentOutput_422;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_425 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_425;

	private bool logic_uScriptAct_SetBool_Out_425 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_425 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_425 = true;

	private uScript_Wait logic_uScript_Wait_uScript_Wait_426 = new uScript_Wait();

	private float logic_uScript_Wait_seconds_426;

	private bool logic_uScript_Wait_repeat_426 = true;

	private bool logic_uScript_Wait_Waited_426 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_427 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_427;

	private bool logic_uScriptCon_CompareBool_True_427 = true;

	private bool logic_uScriptCon_CompareBool_False_427 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_429 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_429;

	private bool logic_uScriptAct_SetBool_Out_429 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_429 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_429 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_430 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_430 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_430;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_430 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_430;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_430 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_430 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_430 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_430 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_431 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_431;

	private bool logic_uScriptAct_SetBool_Out_431 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_431 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_431 = true;

	private uScript_GetCircuitChargeInfo logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_433 = new uScript_GetCircuitChargeInfo();

	private TankBlock logic_uScript_GetCircuitChargeInfo_block_433;

	private Tank logic_uScript_GetCircuitChargeInfo_tech_433;

	private BlockTypes logic_uScript_GetCircuitChargeInfo_blockType_433;

	private int logic_uScript_GetCircuitChargeInfo_Return_433;

	private bool logic_uScript_GetCircuitChargeInfo_Out_433 = true;

	private bool logic_uScript_GetCircuitChargeInfo_HasValidBlock_433 = true;

	private uScript_GetCircuitChargeInfo logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_434 = new uScript_GetCircuitChargeInfo();

	private TankBlock logic_uScript_GetCircuitChargeInfo_block_434;

	private Tank logic_uScript_GetCircuitChargeInfo_tech_434;

	private BlockTypes logic_uScript_GetCircuitChargeInfo_blockType_434;

	private int logic_uScript_GetCircuitChargeInfo_Return_434;

	private bool logic_uScript_GetCircuitChargeInfo_Out_434 = true;

	private bool logic_uScript_GetCircuitChargeInfo_HasValidBlock_434 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_435 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_435 = 4;

	private int logic_uScriptAct_SetInt_Target_435;

	private bool logic_uScriptAct_SetInt_Out_435 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_436 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_436;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_436;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_436;

	private bool logic_uScript_AddMessage_Out_436 = true;

	private bool logic_uScript_AddMessage_Shown_436 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_437 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_437;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_437;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_437;

	private bool logic_uScript_AddMessage_Out_437 = true;

	private bool logic_uScript_AddMessage_Shown_437 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_438 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_438;

	private bool logic_uScriptAct_SetBool_Out_438 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_438 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_438 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_439 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_439 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_439;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_439;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_439 = true;

	private bool logic_uScript_SpawnTechsFromData_Out_439 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_446 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_446;

	private bool logic_uScriptAct_SetBool_Out_446 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_446 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_446 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_447 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_447;

	private bool logic_uScriptAct_SetBool_Out_447 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_447 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_447 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_449 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_449;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_449;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_449;

	private bool logic_uScript_AddMessage_Out_449 = true;

	private bool logic_uScript_AddMessage_Shown_449 = true;

	private uScript_Wait logic_uScript_Wait_uScript_Wait_450 = new uScript_Wait();

	private float logic_uScript_Wait_seconds_450;

	private bool logic_uScript_Wait_repeat_450 = true;

	private bool logic_uScript_Wait_Waited_450 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_452 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_452;

	private bool logic_uScriptAct_SetBool_Out_452 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_452 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_452 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_454 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_454;

	private bool logic_uScriptCon_CompareBool_True_454 = true;

	private bool logic_uScriptCon_CompareBool_False_454 = true;

	private uScript_GetCircuitChargeInfo logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_456 = new uScript_GetCircuitChargeInfo();

	private TankBlock logic_uScript_GetCircuitChargeInfo_block_456;

	private Tank logic_uScript_GetCircuitChargeInfo_tech_456;

	private BlockTypes logic_uScript_GetCircuitChargeInfo_blockType_456;

	private int logic_uScript_GetCircuitChargeInfo_Return_456;

	private bool logic_uScript_GetCircuitChargeInfo_Out_456 = true;

	private bool logic_uScript_GetCircuitChargeInfo_HasValidBlock_456 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_458 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_458 = 3;

	private int logic_uScriptAct_SetInt_Target_458;

	private bool logic_uScriptAct_SetInt_Out_458 = true;

	private uScriptAct_AddInt_v2 logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_461 = new uScriptAct_AddInt_v2();

	private int logic_uScriptAct_AddInt_v2_A_461;

	private int logic_uScriptAct_AddInt_v2_B_461 = 1;

	private int logic_uScriptAct_AddInt_v2_IntResult_461;

	private float logic_uScriptAct_AddInt_v2_FloatResult_461;

	private bool logic_uScriptAct_AddInt_v2_Out_461 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_466 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_466;

	private bool logic_uScriptCon_CompareBool_True_466 = true;

	private bool logic_uScriptCon_CompareBool_False_466 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_467 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_467;

	private bool logic_uScriptCon_CompareBool_True_467 = true;

	private bool logic_uScriptCon_CompareBool_False_467 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_468 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_468;

	private bool logic_uScriptAct_SetBool_Out_468 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_468 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_468 = true;

	private uScriptCon_CompareInt logic_uScriptCon_CompareInt_uScriptCon_CompareInt_469 = new uScriptCon_CompareInt();

	private int logic_uScriptCon_CompareInt_A_469;

	private int logic_uScriptCon_CompareInt_B_469;

	private bool logic_uScriptCon_CompareInt_GreaterThan_469 = true;

	private bool logic_uScriptCon_CompareInt_GreaterThanOrEqualTo_469 = true;

	private bool logic_uScriptCon_CompareInt_EqualTo_469 = true;

	private bool logic_uScriptCon_CompareInt_NotEqualTo_469 = true;

	private bool logic_uScriptCon_CompareInt_LessThanOrEqualTo_469 = true;

	private bool logic_uScriptCon_CompareInt_LessThan_469 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_471 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_471;

	private bool logic_uScriptAct_SetBool_Out_471 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_471 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_471 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_472 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_472;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_472;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_472;

	private bool logic_uScript_AddMessage_Out_472 = true;

	private bool logic_uScript_AddMessage_Shown_472 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_474 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_474;

	private bool logic_uScriptAct_SetBool_Out_474 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_474 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_474 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_475 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_475;

	private bool logic_uScriptAct_SetBool_Out_475 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_475 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_475 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_478 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_478 = 4;

	private int logic_uScriptAct_SetInt_Target_478;

	private bool logic_uScriptAct_SetInt_Out_478 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_479 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_479;

	private bool logic_uScriptAct_SetBool_Out_479 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_479 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_479 = true;

	private uScript_RemoveTech logic_uScript_RemoveTech_uScript_RemoveTech_481 = new uScript_RemoveTech();

	private Tank logic_uScript_RemoveTech_tech_481;

	private bool logic_uScript_RemoveTech_Out_481 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_482 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_482;

	private bool logic_uScriptAct_SetBool_Out_482 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_482 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_482 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_485 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_485 = 3;

	private int logic_uScriptAct_SetInt_Target_485;

	private bool logic_uScriptAct_SetInt_Out_485 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_487 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_487;

	private bool logic_uScriptAct_SetBool_Out_487 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_487 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_487 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_489 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_489;

	private bool logic_uScriptAct_SetBool_Out_489 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_489 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_489 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_494 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_494;

	private bool logic_uScriptAct_SetBool_Out_494 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_494 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_494 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_495 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_495 = 4;

	private int logic_uScriptAct_SetInt_Target_495;

	private bool logic_uScriptAct_SetInt_Out_495 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_496 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_496 = 4;

	private int logic_uScriptAct_SetInt_Target_496;

	private bool logic_uScriptAct_SetInt_Out_496 = true;

	private uScriptCon_CompareInt logic_uScriptCon_CompareInt_uScriptCon_CompareInt_500 = new uScriptCon_CompareInt();

	private int logic_uScriptCon_CompareInt_A_500;

	private int logic_uScriptCon_CompareInt_B_500;

	private bool logic_uScriptCon_CompareInt_GreaterThan_500 = true;

	private bool logic_uScriptCon_CompareInt_GreaterThanOrEqualTo_500 = true;

	private bool logic_uScriptCon_CompareInt_EqualTo_500 = true;

	private bool logic_uScriptCon_CompareInt_NotEqualTo_500 = true;

	private bool logic_uScriptCon_CompareInt_LessThanOrEqualTo_500 = true;

	private bool logic_uScriptCon_CompareInt_LessThan_500 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_504 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_504;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_504;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_504;

	private bool logic_uScript_AddMessage_Out_504 = true;

	private bool logic_uScript_AddMessage_Shown_504 = true;

	private uScript_GetCircuitChargeInfo logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_509 = new uScript_GetCircuitChargeInfo();

	private TankBlock logic_uScript_GetCircuitChargeInfo_block_509;

	private Tank logic_uScript_GetCircuitChargeInfo_tech_509;

	private BlockTypes logic_uScript_GetCircuitChargeInfo_blockType_509;

	private int logic_uScript_GetCircuitChargeInfo_Return_509;

	private bool logic_uScript_GetCircuitChargeInfo_Out_509 = true;

	private bool logic_uScript_GetCircuitChargeInfo_HasValidBlock_509 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_510 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_510 = 6;

	private int logic_uScriptAct_SetInt_Target_510;

	private bool logic_uScriptAct_SetInt_Out_510 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_511 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_511;

	private bool logic_uScriptAct_SetBool_Out_511 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_511 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_511 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_512 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_512;

	private bool logic_uScriptAct_SetBool_Out_512 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_512 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_512 = true;

	private uScript_SpawnVFX logic_uScript_SpawnVFX_uScript_SpawnVFX_513 = new uScript_SpawnVFX();

	private GameObject logic_uScript_SpawnVFX_ownerNode_513;

	private Transform logic_uScript_SpawnVFX_vfxToSpawn_513;

	private string logic_uScript_SpawnVFX_spawnPosName_513 = "";

	private bool logic_uScript_SpawnVFX_Out_513 = true;

	private uScriptCon_ManualSwitch logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_515 = new uScriptCon_ManualSwitch();

	private int logic_uScriptCon_ManualSwitch_CurrentOutput_515;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_516 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_516;

	private bool logic_uScriptAct_SetBool_Out_516 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_516 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_516 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_518 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_518;

	private bool logic_uScriptAct_SetBool_Out_518 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_518 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_518 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_520 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_520;

	private bool logic_uScriptAct_SetBool_Out_520 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_520 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_520 = true;

	private uScriptCon_CompareInt logic_uScriptCon_CompareInt_uScriptCon_CompareInt_522 = new uScriptCon_CompareInt();

	private int logic_uScriptCon_CompareInt_A_522;

	private int logic_uScriptCon_CompareInt_B_522;

	private bool logic_uScriptCon_CompareInt_GreaterThan_522 = true;

	private bool logic_uScriptCon_CompareInt_GreaterThanOrEqualTo_522 = true;

	private bool logic_uScriptCon_CompareInt_EqualTo_522 = true;

	private bool logic_uScriptCon_CompareInt_NotEqualTo_522 = true;

	private bool logic_uScriptCon_CompareInt_LessThanOrEqualTo_522 = true;

	private bool logic_uScriptCon_CompareInt_LessThan_522 = true;

	private uScriptAct_AddInt_v2 logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_523 = new uScriptAct_AddInt_v2();

	private int logic_uScriptAct_AddInt_v2_A_523;

	private int logic_uScriptAct_AddInt_v2_B_523 = 1;

	private int logic_uScriptAct_AddInt_v2_IntResult_523;

	private float logic_uScriptAct_AddInt_v2_FloatResult_523;

	private bool logic_uScriptAct_AddInt_v2_Out_523 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_526 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_526;

	private bool logic_uScriptAct_SetBool_Out_526 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_526 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_526 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_528 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_528;

	private bool logic_uScriptAct_SetBool_Out_528 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_528 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_528 = true;

	private uScriptAct_AddInt_v2 logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_529 = new uScriptAct_AddInt_v2();

	private int logic_uScriptAct_AddInt_v2_A_529;

	private int logic_uScriptAct_AddInt_v2_B_529 = 1;

	private int logic_uScriptAct_AddInt_v2_IntResult_529;

	private float logic_uScriptAct_AddInt_v2_FloatResult_529;

	private bool logic_uScriptAct_AddInt_v2_Out_529 = true;

	private uScript_GetCircuitChargeInfo logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_533 = new uScript_GetCircuitChargeInfo();

	private TankBlock logic_uScript_GetCircuitChargeInfo_block_533;

	private Tank logic_uScript_GetCircuitChargeInfo_tech_533;

	private BlockTypes logic_uScript_GetCircuitChargeInfo_blockType_533;

	private int logic_uScript_GetCircuitChargeInfo_Return_533;

	private bool logic_uScript_GetCircuitChargeInfo_Out_533 = true;

	private bool logic_uScript_GetCircuitChargeInfo_HasValidBlock_533 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_540 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_540;

	private bool logic_uScriptAct_SetBool_Out_540 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_540 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_540 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_541 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_541;

	private bool logic_uScriptAct_SetBool_Out_541 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_541 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_541 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_542 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_542;

	private bool logic_uScriptAct_SetBool_Out_542 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_542 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_542 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_549 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_549;

	private bool logic_uScriptAct_SetBool_Out_549 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_549 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_549 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_550 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_550;

	private bool logic_uScriptCon_CompareBool_True_550 = true;

	private bool logic_uScriptCon_CompareBool_False_550 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_552 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_552 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_552;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_552 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_552;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_552 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_552 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_552 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_552 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_553 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_553 = 3;

	private int logic_uScriptAct_SetInt_Target_553;

	private bool logic_uScriptAct_SetInt_Out_553 = true;

	private uScript_GetCircuitChargeInfo logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_554 = new uScript_GetCircuitChargeInfo();

	private TankBlock logic_uScript_GetCircuitChargeInfo_block_554;

	private Tank logic_uScript_GetCircuitChargeInfo_tech_554;

	private BlockTypes logic_uScript_GetCircuitChargeInfo_blockType_554;

	private int logic_uScript_GetCircuitChargeInfo_Return_554;

	private bool logic_uScript_GetCircuitChargeInfo_Out_554 = true;

	private bool logic_uScript_GetCircuitChargeInfo_HasValidBlock_554 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_555 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_555;

	private bool logic_uScriptCon_CompareBool_True_555 = true;

	private bool logic_uScriptCon_CompareBool_False_555 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_562 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_562;

	private bool logic_uScriptCon_CompareBool_True_562 = true;

	private bool logic_uScriptCon_CompareBool_False_562 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_566 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_566;

	private bool logic_uScriptAct_SetBool_Out_566 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_566 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_566 = true;

	private uScript_Wait logic_uScript_Wait_uScript_Wait_567 = new uScript_Wait();

	private float logic_uScript_Wait_seconds_567;

	private bool logic_uScript_Wait_repeat_567 = true;

	private bool logic_uScript_Wait_Waited_567 = true;

	private uScript_RemoveTech logic_uScript_RemoveTech_uScript_RemoveTech_568 = new uScript_RemoveTech();

	private Tank logic_uScript_RemoveTech_tech_568;

	private bool logic_uScript_RemoveTech_Out_568 = true;

	private uScript_RemoveTech logic_uScript_RemoveTech_uScript_RemoveTech_570 = new uScript_RemoveTech();

	private Tank logic_uScript_RemoveTech_tech_570;

	private bool logic_uScript_RemoveTech_Out_570 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_572 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_572 = 1;

	private int logic_uScriptAct_SetInt_Target_572;

	private bool logic_uScriptAct_SetInt_Out_572 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_574 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_574 = 4;

	private int logic_uScriptAct_SetInt_Target_574;

	private bool logic_uScriptAct_SetInt_Out_574 = true;

	private uScriptCon_CompareInt logic_uScriptCon_CompareInt_uScriptCon_CompareInt_575 = new uScriptCon_CompareInt();

	private int logic_uScriptCon_CompareInt_A_575;

	private int logic_uScriptCon_CompareInt_B_575;

	private bool logic_uScriptCon_CompareInt_GreaterThan_575 = true;

	private bool logic_uScriptCon_CompareInt_GreaterThanOrEqualTo_575 = true;

	private bool logic_uScriptCon_CompareInt_EqualTo_575 = true;

	private bool logic_uScriptCon_CompareInt_NotEqualTo_575 = true;

	private bool logic_uScriptCon_CompareInt_LessThanOrEqualTo_575 = true;

	private bool logic_uScriptCon_CompareInt_LessThan_575 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_576 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_576;

	private bool logic_uScriptAct_SetBool_Out_576 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_576 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_576 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_580 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_580;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_580;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_580;

	private bool logic_uScript_AddMessage_Out_580 = true;

	private bool logic_uScript_AddMessage_Shown_580 = true;

	private uScript_SpawnVFX logic_uScript_SpawnVFX_uScript_SpawnVFX_582 = new uScript_SpawnVFX();

	private GameObject logic_uScript_SpawnVFX_ownerNode_582;

	private Transform logic_uScript_SpawnVFX_vfxToSpawn_582;

	private string logic_uScript_SpawnVFX_spawnPosName_582 = "";

	private bool logic_uScript_SpawnVFX_Out_582 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_585 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_585 = 4;

	private int logic_uScriptAct_SetInt_Target_585;

	private bool logic_uScriptAct_SetInt_Out_585 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_586 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_586 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_586;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_586;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_586 = true;

	private bool logic_uScript_SpawnTechsFromData_Out_586 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_588 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_588 = 3;

	private int logic_uScriptAct_SetInt_Target_588;

	private bool logic_uScriptAct_SetInt_Out_588 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_590 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_590;

	private bool logic_uScriptCon_CompareBool_True_590 = true;

	private bool logic_uScriptCon_CompareBool_False_590 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_592 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_592;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_592;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_592;

	private bool logic_uScript_AddMessage_Out_592 = true;

	private bool logic_uScript_AddMessage_Shown_592 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_593 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_593;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_593;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_593;

	private bool logic_uScript_AddMessage_Out_593 = true;

	private bool logic_uScript_AddMessage_Shown_593 = true;

	private uScript_RemoveTech logic_uScript_RemoveTech_uScript_RemoveTech_594 = new uScript_RemoveTech();

	private Tank logic_uScript_RemoveTech_tech_594;

	private bool logic_uScript_RemoveTech_Out_594 = true;

	private uScript_FlyTechUpAndAway logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_597 = new uScript_FlyTechUpAndAway();

	private Tank logic_uScript_FlyTechUpAndAway_tech_597;

	private float logic_uScript_FlyTechUpAndAway_maxLifetime_597 = 5f;

	private float logic_uScript_FlyTechUpAndAway_targetHeight_597 = 150f;

	private ExternalBehaviorTree logic_uScript_FlyTechUpAndAway_aiTree_597;

	private Transform logic_uScript_FlyTechUpAndAway_removalParticles_597;

	private bool logic_uScript_FlyTechUpAndAway_Out_597 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_598 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_598;

	private bool logic_uScriptAct_SetBool_Out_598 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_598 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_598 = true;

	private uScript_Wait logic_uScript_Wait_uScript_Wait_600 = new uScript_Wait();

	private float logic_uScript_Wait_seconds_600;

	private bool logic_uScript_Wait_repeat_600 = true;

	private bool logic_uScript_Wait_Waited_600 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_601 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_601;

	private bool logic_uScriptAct_SetBool_Out_601 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_601 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_601 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_604 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_604 = 1;

	private int logic_uScriptAct_SetInt_Target_604;

	private bool logic_uScriptAct_SetInt_Out_604 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_610 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_610;

	private bool logic_uScriptAct_SetBool_Out_610 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_610 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_610 = true;

	private uScript_Wait logic_uScript_Wait_uScript_Wait_612 = new uScript_Wait();

	private float logic_uScript_Wait_seconds_612;

	private bool logic_uScript_Wait_repeat_612 = true;

	private bool logic_uScript_Wait_Waited_612 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_616 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_616;

	private bool logic_uScriptCon_CompareBool_True_616 = true;

	private bool logic_uScriptCon_CompareBool_False_616 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_621 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_621;

	private bool logic_uScriptCon_CompareBool_True_621 = true;

	private bool logic_uScriptCon_CompareBool_False_621 = true;

	private uScriptCon_CompareInt logic_uScriptCon_CompareInt_uScriptCon_CompareInt_622 = new uScriptCon_CompareInt();

	private int logic_uScriptCon_CompareInt_A_622;

	private int logic_uScriptCon_CompareInt_B_622;

	private bool logic_uScriptCon_CompareInt_GreaterThan_622 = true;

	private bool logic_uScriptCon_CompareInt_GreaterThanOrEqualTo_622 = true;

	private bool logic_uScriptCon_CompareInt_EqualTo_622 = true;

	private bool logic_uScriptCon_CompareInt_NotEqualTo_622 = true;

	private bool logic_uScriptCon_CompareInt_LessThanOrEqualTo_622 = true;

	private bool logic_uScriptCon_CompareInt_LessThan_622 = true;

	private uScript_FinishEncounter logic_uScript_FinishEncounter_uScript_FinishEncounter_624 = new uScript_FinishEncounter();

	private GameObject logic_uScript_FinishEncounter_owner_624;

	private bool logic_uScript_FinishEncounter_Out_624 = true;

	private uScript_SpawnVFX logic_uScript_SpawnVFX_uScript_SpawnVFX_625 = new uScript_SpawnVFX();

	private GameObject logic_uScript_SpawnVFX_ownerNode_625;

	private Transform logic_uScript_SpawnVFX_vfxToSpawn_625;

	private string logic_uScript_SpawnVFX_spawnPosName_625 = "";

	private bool logic_uScript_SpawnVFX_Out_625 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_626 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_626 = 3;

	private int logic_uScriptAct_SetInt_Target_626;

	private bool logic_uScriptAct_SetInt_Out_626 = true;

	private uScript_SpawnVFX logic_uScript_SpawnVFX_uScript_SpawnVFX_627 = new uScript_SpawnVFX();

	private GameObject logic_uScript_SpawnVFX_ownerNode_627;

	private Transform logic_uScript_SpawnVFX_vfxToSpawn_627;

	private string logic_uScript_SpawnVFX_spawnPosName_627 = "";

	private bool logic_uScript_SpawnVFX_Out_627 = true;

	private uScript_GetCircuitChargeInfo logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_628 = new uScript_GetCircuitChargeInfo();

	private TankBlock logic_uScript_GetCircuitChargeInfo_block_628;

	private Tank logic_uScript_GetCircuitChargeInfo_tech_628;

	private BlockTypes logic_uScript_GetCircuitChargeInfo_blockType_628;

	private int logic_uScript_GetCircuitChargeInfo_Return_628;

	private bool logic_uScript_GetCircuitChargeInfo_Out_628 = true;

	private bool logic_uScript_GetCircuitChargeInfo_HasValidBlock_628 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_629 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_629;

	private bool logic_uScriptAct_SetBool_Out_629 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_629 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_629 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_635 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_635;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_635;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_635;

	private bool logic_uScript_AddMessage_Out_635 = true;

	private bool logic_uScript_AddMessage_Shown_635 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_638 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_638;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_638;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_638;

	private bool logic_uScript_AddMessage_Out_638 = true;

	private bool logic_uScript_AddMessage_Shown_638 = true;

	private SubGraph_SaveLoadInt logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_644 = new SubGraph_SaveLoadInt();

	private int logic_SubGraph_SaveLoadInt_restartValue_644 = 1;

	private int logic_SubGraph_SaveLoadInt_integer_644;

	private object logic_SubGraph_SaveLoadInt_intAsVariable_644 = "";

	private string logic_SubGraph_SaveLoadInt_uniqueID_644 = "QuestionNumber:";

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_647 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_647;

	private bool logic_uScriptAct_SetBool_Out_647 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_647 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_647 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_649 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_649 = 3;

	private int logic_uScriptAct_SetInt_Target_649;

	private bool logic_uScriptAct_SetInt_Out_649 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_651 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_651 = 4;

	private int logic_uScriptAct_SetInt_Target_651;

	private bool logic_uScriptAct_SetInt_Out_651 = true;

	private void SyncUnityHooks()
	{
		SyncEventListeners();
		if (null == owner_Connection_2 || !m_RegisteredForEvents)
		{
			owner_Connection_2 = parentGameObject;
		}
		if (null == owner_Connection_14 || !m_RegisteredForEvents)
		{
			owner_Connection_14 = parentGameObject;
		}
		if (null == owner_Connection_18 || !m_RegisteredForEvents)
		{
			owner_Connection_18 = parentGameObject;
			if (null != owner_Connection_18)
			{
				uScript_EncounterUpdate uScript_EncounterUpdate2 = owner_Connection_18.GetComponent<uScript_EncounterUpdate>();
				if (null == uScript_EncounterUpdate2)
				{
					uScript_EncounterUpdate2 = owner_Connection_18.AddComponent<uScript_EncounterUpdate>();
				}
				if (null != uScript_EncounterUpdate2)
				{
					uScript_EncounterUpdate2.OnUpdate += Instance_OnUpdate_35;
					uScript_EncounterUpdate2.OnSuspend += Instance_OnSuspend_35;
					uScript_EncounterUpdate2.OnResume += Instance_OnResume_35;
				}
			}
		}
		if (null == owner_Connection_23 || !m_RegisteredForEvents)
		{
			owner_Connection_23 = parentGameObject;
		}
		if (null == owner_Connection_38 || !m_RegisteredForEvents)
		{
			owner_Connection_38 = parentGameObject;
		}
		if (null == owner_Connection_40 || !m_RegisteredForEvents)
		{
			owner_Connection_40 = parentGameObject;
		}
		if (null == owner_Connection_41 || !m_RegisteredForEvents)
		{
			owner_Connection_41 = parentGameObject;
		}
		if (null == owner_Connection_42 || !m_RegisteredForEvents)
		{
			owner_Connection_42 = parentGameObject;
		}
		if (null == owner_Connection_56 || !m_RegisteredForEvents)
		{
			owner_Connection_56 = parentGameObject;
		}
		if (null == owner_Connection_104 || !m_RegisteredForEvents)
		{
			owner_Connection_104 = parentGameObject;
		}
		if (null == owner_Connection_114 || !m_RegisteredForEvents)
		{
			owner_Connection_114 = parentGameObject;
			if (null != owner_Connection_114)
			{
				uScript_SaveLoad uScript_SaveLoad2 = owner_Connection_114.GetComponent<uScript_SaveLoad>();
				if (null == uScript_SaveLoad2)
				{
					uScript_SaveLoad2 = owner_Connection_114.AddComponent<uScript_SaveLoad>();
				}
				if (null != uScript_SaveLoad2)
				{
					uScript_SaveLoad2.SaveEvent += Instance_SaveEvent_100;
					uScript_SaveLoad2.LoadEvent += Instance_LoadEvent_100;
					uScript_SaveLoad2.RestartEvent += Instance_RestartEvent_100;
				}
			}
		}
		if (null == owner_Connection_139 || !m_RegisteredForEvents)
		{
			owner_Connection_139 = parentGameObject;
		}
		if (null == owner_Connection_149 || !m_RegisteredForEvents)
		{
			owner_Connection_149 = parentGameObject;
		}
		if (null == owner_Connection_151 || !m_RegisteredForEvents)
		{
			owner_Connection_151 = parentGameObject;
		}
		if (null == owner_Connection_168 || !m_RegisteredForEvents)
		{
			owner_Connection_168 = parentGameObject;
		}
		if (null == owner_Connection_253 || !m_RegisteredForEvents)
		{
			owner_Connection_253 = parentGameObject;
		}
		if (null == owner_Connection_283 || !m_RegisteredForEvents)
		{
			owner_Connection_283 = parentGameObject;
		}
		if (null == owner_Connection_415 || !m_RegisteredForEvents)
		{
			owner_Connection_415 = parentGameObject;
		}
		if (null == owner_Connection_421 || !m_RegisteredForEvents)
		{
			owner_Connection_421 = parentGameObject;
		}
		if (null == owner_Connection_497 || !m_RegisteredForEvents)
		{
			owner_Connection_497 = parentGameObject;
		}
		if (null == owner_Connection_519 || !m_RegisteredForEvents)
		{
			owner_Connection_519 = parentGameObject;
		}
		if (null == owner_Connection_531 || !m_RegisteredForEvents)
		{
			owner_Connection_531 = parentGameObject;
		}
		if (null == owner_Connection_589 || !m_RegisteredForEvents)
		{
			owner_Connection_589 = parentGameObject;
		}
		if (null == owner_Connection_611 || !m_RegisteredForEvents)
		{
			owner_Connection_611 = parentGameObject;
		}
		if (null == owner_Connection_615 || !m_RegisteredForEvents)
		{
			owner_Connection_615 = parentGameObject;
		}
		if (null == owner_Connection_618 || !m_RegisteredForEvents)
		{
			owner_Connection_618 = parentGameObject;
		}
	}

	private void RegisterForUnityHooks()
	{
		SyncEventListeners();
		if (!m_RegisteredForEvents && null != owner_Connection_18)
		{
			uScript_EncounterUpdate uScript_EncounterUpdate2 = owner_Connection_18.GetComponent<uScript_EncounterUpdate>();
			if (null == uScript_EncounterUpdate2)
			{
				uScript_EncounterUpdate2 = owner_Connection_18.AddComponent<uScript_EncounterUpdate>();
			}
			if (null != uScript_EncounterUpdate2)
			{
				uScript_EncounterUpdate2.OnUpdate += Instance_OnUpdate_35;
				uScript_EncounterUpdate2.OnSuspend += Instance_OnSuspend_35;
				uScript_EncounterUpdate2.OnResume += Instance_OnResume_35;
			}
		}
		if (!m_RegisteredForEvents && null != owner_Connection_114)
		{
			uScript_SaveLoad uScript_SaveLoad2 = owner_Connection_114.GetComponent<uScript_SaveLoad>();
			if (null == uScript_SaveLoad2)
			{
				uScript_SaveLoad2 = owner_Connection_114.AddComponent<uScript_SaveLoad>();
			}
			if (null != uScript_SaveLoad2)
			{
				uScript_SaveLoad2.SaveEvent += Instance_SaveEvent_100;
				uScript_SaveLoad2.LoadEvent += Instance_LoadEvent_100;
				uScript_SaveLoad2.RestartEvent += Instance_RestartEvent_100;
			}
		}
	}

	private void SyncEventListeners()
	{
	}

	private void UnregisterEventListeners()
	{
		if (null != owner_Connection_18)
		{
			uScript_EncounterUpdate component = owner_Connection_18.GetComponent<uScript_EncounterUpdate>();
			if (null != component)
			{
				component.OnUpdate -= Instance_OnUpdate_35;
				component.OnSuspend -= Instance_OnSuspend_35;
				component.OnResume -= Instance_OnResume_35;
			}
		}
		if (null != owner_Connection_114)
		{
			uScript_SaveLoad component2 = owner_Connection_114.GetComponent<uScript_SaveLoad>();
			if (null != component2)
			{
				component2.SaveEvent -= Instance_SaveEvent_100;
				component2.LoadEvent -= Instance_LoadEvent_100;
				component2.RestartEvent -= Instance_RestartEvent_100;
			}
		}
	}

	public override void SetParent(GameObject g)
	{
		parentGameObject = g;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_7.SetParent(g);
		logic_uScript_SetCustomRadarTeamID_uScript_SetCustomRadarTeamID_8.SetParent(g);
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_9.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_10.SetParent(g);
		logic_uScript_SetCustomRadarTeamID_uScript_SetCustomRadarTeamID_12.SetParent(g);
		logic_uScript_LockTechInteraction_uScript_LockTechInteraction_20.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_21.SetParent(g);
		logic_uScript_LockTechInteraction_uScript_LockTechInteraction_25.SetParent(g);
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_28.SetParent(g);
		logic_uScript_PausePopulation_uScript_PausePopulation_33.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_34.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_36.SetParent(g);
		logic_uScript_LockTechInteraction_uScript_LockTechInteraction_37.SetParent(g);
		logic_uScript_LockTechInteraction_uScript_LockTechInteraction_39.SetParent(g);
		logic_uScript_LockTech_uScript_LockTech_44.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_45.SetParent(g);
		logic_uScript_SetCustomRadarTeamID_uScript_SetCustomRadarTeamID_46.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_47.SetParent(g);
		logic_uScript_LockTechInteraction_uScript_LockTechInteraction_50.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_52.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_54.SetParent(g);
		logic_uScript_SetCustomRadarTeamID_uScript_SetCustomRadarTeamID_55.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_58.SetParent(g);
		logic_uScript_GetTankBlock_uScript_GetTankBlock_61.SetParent(g);
		logic_uScript_GetTankBlock_uScript_GetTankBlock_64.SetParent(g);
		logic_uScript_GetTankBlock_uScript_GetTankBlock_71.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_72.SetParent(g);
		logic_uScript_SetTankHideBlockLimit_uScript_SetTankHideBlockLimit_74.SetParent(g);
		logic_uScript_LockTechInteraction_uScript_LockTechInteraction_75.SetParent(g);
		logic_uScript_SetCustomRadarTeamID_uScript_SetCustomRadarTeamID_81.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_82.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_83.SetParent(g);
		logic_uScript_PausePopulation_uScript_PausePopulation_86.SetParent(g);
		logic_uScript_LockTech_uScript_LockTech_89.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_90.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_91.SetParent(g);
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_93.SetParent(g);
		logic_uScript_LockTech_uScript_LockTech_95.SetParent(g);
		logic_uScript_LockTechInteraction_uScript_LockTechInteraction_97.SetParent(g);
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_101.SetParent(g);
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_102.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_105.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_108.SetParent(g);
		logic_uScript_GetTankBlock_uScript_GetTankBlock_113.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_115.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_116.SetParent(g);
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_117.SetParent(g);
		logic_uScript_LockTech_uScript_LockTech_119.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_122.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_124.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_128.SetParent(g);
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_129.SetParent(g);
		logic_uScript_SetTankHideBlockLimit_uScript_SetTankHideBlockLimit_130.SetParent(g);
		logic_uScript_SetTankHideBlockLimit_uScript_SetTankHideBlockLimit_132.SetParent(g);
		logic_uScript_RemoveScenery_uScript_RemoveScenery_133.SetParent(g);
		logic_uScript_LockTechInteraction_uScript_LockTechInteraction_136.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_138.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_140.SetParent(g);
		logic_uScript_LockTech_uScript_LockTech_143.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_144.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_147.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_148.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_150.SetParent(g);
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_153.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_157.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_158.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_159.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_162.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_163.SetParent(g);
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_167.SetParent(g);
		logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_171.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_172.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_173.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_174.SetParent(g);
		logic_uScript_SetTankHideBlockLimit_uScript_SetTankHideBlockLimit_175.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_176.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_177.SetParent(g);
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_178.SetParent(g);
		logic_uScript_LockTechInteraction_uScript_LockTechInteraction_180.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_181.SetParent(g);
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_185.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_186.SetParent(g);
		logic_uScriptAct_Concatenate_uScriptAct_Concatenate_188.SetParent(g);
		logic_uScriptAct_PrintText_uScriptAct_PrintText_190.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_192.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_194.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_196.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_200.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_202.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_203.SetParent(g);
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_206.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_208.SetParent(g);
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_209.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_212.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_214.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_218.SetParent(g);
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_220.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_221.SetParent(g);
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_222.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_231.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_232.SetParent(g);
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_234.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_235.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_238.SetParent(g);
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_239.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_241.SetParent(g);
		logic_uScript_Wait_uScript_Wait_242.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_243.SetParent(g);
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_245.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_249.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_254.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_255.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_256.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_257.SetParent(g);
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_258.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_261.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_265.SetParent(g);
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_267.SetParent(g);
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_271.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_275.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_276.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_277.SetParent(g);
		logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_279.SetParent(g);
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_280.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_284.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_288.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_289.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_292.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_294.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_295.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_296.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_297.SetParent(g);
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_299.SetParent(g);
		logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_302.SetParent(g);
		logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_305.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_307.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_308.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_310.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_311.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_314.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_315.SetParent(g);
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_316.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_317.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_320.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_321.SetParent(g);
		logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_323.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_324.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_327.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_330.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_334.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_344.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_345.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_347.SetParent(g);
		logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_351.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_352.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_353.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_355.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_356.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_357.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_359.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_360.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_364.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_365.SetParent(g);
		logic_uScript_Wait_uScript_Wait_368.SetParent(g);
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_369.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_370.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_375.SetParent(g);
		logic_uScript_Wait_uScript_Wait_379.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_381.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_382.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_383.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_385.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_388.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_389.SetParent(g);
		logic_uScript_Wait_uScript_Wait_396.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_400.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_407.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_410.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_411.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_412.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_413.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_416.SetParent(g);
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_417.SetParent(g);
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_418.SetParent(g);
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_422.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_425.SetParent(g);
		logic_uScript_Wait_uScript_Wait_426.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_427.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_429.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_430.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_431.SetParent(g);
		logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_433.SetParent(g);
		logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_434.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_435.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_436.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_437.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_438.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_439.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_446.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_447.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_449.SetParent(g);
		logic_uScript_Wait_uScript_Wait_450.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_452.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_454.SetParent(g);
		logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_456.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_458.SetParent(g);
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_461.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_466.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_467.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_468.SetParent(g);
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_469.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_471.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_472.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_474.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_475.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_478.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_479.SetParent(g);
		logic_uScript_RemoveTech_uScript_RemoveTech_481.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_482.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_485.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_487.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_489.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_494.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_495.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_496.SetParent(g);
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_500.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_504.SetParent(g);
		logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_509.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_510.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_511.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_512.SetParent(g);
		logic_uScript_SpawnVFX_uScript_SpawnVFX_513.SetParent(g);
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_515.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_516.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_518.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_520.SetParent(g);
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_522.SetParent(g);
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_523.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_526.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_528.SetParent(g);
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_529.SetParent(g);
		logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_533.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_540.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_541.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_542.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_549.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_550.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_552.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_553.SetParent(g);
		logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_554.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_555.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_562.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_566.SetParent(g);
		logic_uScript_Wait_uScript_Wait_567.SetParent(g);
		logic_uScript_RemoveTech_uScript_RemoveTech_568.SetParent(g);
		logic_uScript_RemoveTech_uScript_RemoveTech_570.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_572.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_574.SetParent(g);
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_575.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_576.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_580.SetParent(g);
		logic_uScript_SpawnVFX_uScript_SpawnVFX_582.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_585.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_586.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_588.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_590.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_592.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_593.SetParent(g);
		logic_uScript_RemoveTech_uScript_RemoveTech_594.SetParent(g);
		logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_597.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_598.SetParent(g);
		logic_uScript_Wait_uScript_Wait_600.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_601.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_604.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_610.SetParent(g);
		logic_uScript_Wait_uScript_Wait_612.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_616.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_621.SetParent(g);
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_622.SetParent(g);
		logic_uScript_FinishEncounter_uScript_FinishEncounter_624.SetParent(g);
		logic_uScript_SpawnVFX_uScript_SpawnVFX_625.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_626.SetParent(g);
		logic_uScript_SpawnVFX_uScript_SpawnVFX_627.SetParent(g);
		logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_628.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_629.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_635.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_638.SetParent(g);
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_644.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_647.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_649.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_651.SetParent(g);
		owner_Connection_2 = parentGameObject;
		owner_Connection_14 = parentGameObject;
		owner_Connection_18 = parentGameObject;
		owner_Connection_23 = parentGameObject;
		owner_Connection_38 = parentGameObject;
		owner_Connection_40 = parentGameObject;
		owner_Connection_41 = parentGameObject;
		owner_Connection_42 = parentGameObject;
		owner_Connection_56 = parentGameObject;
		owner_Connection_104 = parentGameObject;
		owner_Connection_114 = parentGameObject;
		owner_Connection_139 = parentGameObject;
		owner_Connection_149 = parentGameObject;
		owner_Connection_151 = parentGameObject;
		owner_Connection_168 = parentGameObject;
		owner_Connection_253 = parentGameObject;
		owner_Connection_283 = parentGameObject;
		owner_Connection_415 = parentGameObject;
		owner_Connection_421 = parentGameObject;
		owner_Connection_497 = parentGameObject;
		owner_Connection_519 = parentGameObject;
		owner_Connection_531 = parentGameObject;
		owner_Connection_589 = parentGameObject;
		owner_Connection_611 = parentGameObject;
		owner_Connection_615 = parentGameObject;
		owner_Connection_618 = parentGameObject;
	}

	public void Awake()
	{
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_36.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_58.Awake();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_102.Awake();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_153.Awake();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_167.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_176.Awake();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_178.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_186.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_192.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_194.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_200.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_202.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_203.Awake();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_206.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_212.Awake();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_220.Awake();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_644.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_36.Save_Out += SubGraph_SaveLoadBool_Save_Out_36;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_36.Load_Out += SubGraph_SaveLoadBool_Load_Out_36;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_36.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_36;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_58.Save_Out += SubGraph_SaveLoadBool_Save_Out_58;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_58.Load_Out += SubGraph_SaveLoadBool_Load_Out_58;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_58.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_58;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_102.Save_Out += SubGraph_SaveLoadInt_Save_Out_102;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_102.Load_Out += SubGraph_SaveLoadInt_Load_Out_102;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_102.Restart_Out += SubGraph_SaveLoadInt_Restart_Out_102;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_153.Save_Out += SubGraph_SaveLoadInt_Save_Out_153;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_153.Load_Out += SubGraph_SaveLoadInt_Load_Out_153;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_153.Restart_Out += SubGraph_SaveLoadInt_Restart_Out_153;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_167.Save_Out += SubGraph_SaveLoadInt_Save_Out_167;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_167.Load_Out += SubGraph_SaveLoadInt_Load_Out_167;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_167.Restart_Out += SubGraph_SaveLoadInt_Restart_Out_167;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_176.Save_Out += SubGraph_SaveLoadBool_Save_Out_176;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_176.Load_Out += SubGraph_SaveLoadBool_Load_Out_176;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_176.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_176;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_178.Save_Out += SubGraph_SaveLoadInt_Save_Out_178;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_178.Load_Out += SubGraph_SaveLoadInt_Load_Out_178;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_178.Restart_Out += SubGraph_SaveLoadInt_Restart_Out_178;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_186.Save_Out += SubGraph_SaveLoadBool_Save_Out_186;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_186.Load_Out += SubGraph_SaveLoadBool_Load_Out_186;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_186.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_186;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_192.Save_Out += SubGraph_SaveLoadBool_Save_Out_192;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_192.Load_Out += SubGraph_SaveLoadBool_Load_Out_192;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_192.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_192;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_194.Save_Out += SubGraph_SaveLoadBool_Save_Out_194;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_194.Load_Out += SubGraph_SaveLoadBool_Load_Out_194;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_194.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_194;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_200.Save_Out += SubGraph_SaveLoadBool_Save_Out_200;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_200.Load_Out += SubGraph_SaveLoadBool_Load_Out_200;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_200.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_200;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_202.Save_Out += SubGraph_SaveLoadBool_Save_Out_202;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_202.Load_Out += SubGraph_SaveLoadBool_Load_Out_202;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_202.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_202;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_203.Save_Out += SubGraph_SaveLoadBool_Save_Out_203;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_203.Load_Out += SubGraph_SaveLoadBool_Load_Out_203;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_203.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_203;
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_206.Out += SubGraph_LoadObjectiveStates_Out_206;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_212.Save_Out += SubGraph_SaveLoadBool_Save_Out_212;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_212.Load_Out += SubGraph_SaveLoadBool_Load_Out_212;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_212.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_212;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_220.Out += SubGraph_CompleteObjectiveStage_Out_220;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_222.Output1 += uScriptCon_ManualSwitch_Output1_222;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_222.Output2 += uScriptCon_ManualSwitch_Output2_222;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_222.Output3 += uScriptCon_ManualSwitch_Output3_222;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_222.Output4 += uScriptCon_ManualSwitch_Output4_222;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_222.Output5 += uScriptCon_ManualSwitch_Output5_222;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_222.Output6 += uScriptCon_ManualSwitch_Output6_222;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_222.Output7 += uScriptCon_ManualSwitch_Output7_222;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_222.Output8 += uScriptCon_ManualSwitch_Output8_222;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_245.Output1 += uScriptCon_ManualSwitch_Output1_245;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_245.Output2 += uScriptCon_ManualSwitch_Output2_245;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_245.Output3 += uScriptCon_ManualSwitch_Output3_245;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_245.Output4 += uScriptCon_ManualSwitch_Output4_245;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_245.Output5 += uScriptCon_ManualSwitch_Output5_245;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_245.Output6 += uScriptCon_ManualSwitch_Output6_245;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_245.Output7 += uScriptCon_ManualSwitch_Output7_245;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_245.Output8 += uScriptCon_ManualSwitch_Output8_245;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_280.Output1 += uScriptCon_ManualSwitch_Output1_280;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_280.Output2 += uScriptCon_ManualSwitch_Output2_280;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_280.Output3 += uScriptCon_ManualSwitch_Output3_280;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_280.Output4 += uScriptCon_ManualSwitch_Output4_280;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_280.Output5 += uScriptCon_ManualSwitch_Output5_280;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_280.Output6 += uScriptCon_ManualSwitch_Output6_280;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_280.Output7 += uScriptCon_ManualSwitch_Output7_280;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_280.Output8 += uScriptCon_ManualSwitch_Output8_280;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_422.Output1 += uScriptCon_ManualSwitch_Output1_422;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_422.Output2 += uScriptCon_ManualSwitch_Output2_422;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_422.Output3 += uScriptCon_ManualSwitch_Output3_422;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_422.Output4 += uScriptCon_ManualSwitch_Output4_422;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_422.Output5 += uScriptCon_ManualSwitch_Output5_422;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_422.Output6 += uScriptCon_ManualSwitch_Output6_422;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_422.Output7 += uScriptCon_ManualSwitch_Output7_422;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_422.Output8 += uScriptCon_ManualSwitch_Output8_422;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_515.Output1 += uScriptCon_ManualSwitch_Output1_515;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_515.Output2 += uScriptCon_ManualSwitch_Output2_515;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_515.Output3 += uScriptCon_ManualSwitch_Output3_515;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_515.Output4 += uScriptCon_ManualSwitch_Output4_515;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_515.Output5 += uScriptCon_ManualSwitch_Output5_515;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_515.Output6 += uScriptCon_ManualSwitch_Output6_515;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_515.Output7 += uScriptCon_ManualSwitch_Output7_515;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_515.Output8 += uScriptCon_ManualSwitch_Output8_515;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_644.Save_Out += SubGraph_SaveLoadInt_Save_Out_644;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_644.Load_Out += SubGraph_SaveLoadInt_Load_Out_644;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_644.Restart_Out += SubGraph_SaveLoadInt_Restart_Out_644;
	}

	public void Start()
	{
		SyncUnityHooks();
		m_RegisteredForEvents = true;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_36.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_58.Start();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_102.Start();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_153.Start();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_167.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_176.Start();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_178.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_186.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_192.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_194.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_200.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_202.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_203.Start();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_206.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_212.Start();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_220.Start();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_644.Start();
	}

	public void OnEnable()
	{
		RegisterForUnityHooks();
		m_RegisteredForEvents = true;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_36.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_58.OnEnable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_102.OnEnable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_153.OnEnable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_167.OnEnable();
		logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_171.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_176.OnEnable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_178.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_186.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_192.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_194.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_200.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_202.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_203.OnEnable();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_206.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_212.OnEnable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_220.OnEnable();
		logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_597.OnEnable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_644.OnEnable();
	}

	public void OnDisable()
	{
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_9.OnDisable();
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_28.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_36.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_58.OnDisable();
		logic_uScript_GetTankBlock_uScript_GetTankBlock_61.OnDisable();
		logic_uScript_GetTankBlock_uScript_GetTankBlock_64.OnDisable();
		logic_uScript_GetTankBlock_uScript_GetTankBlock_71.OnDisable();
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_93.OnDisable();
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_101.OnDisable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_102.OnDisable();
		logic_uScript_GetTankBlock_uScript_GetTankBlock_113.OnDisable();
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_129.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_144.OnDisable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_153.OnDisable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_167.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_176.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_177.OnDisable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_178.OnDisable();
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_185.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_186.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_192.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_194.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_200.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_202.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_203.OnDisable();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_206.OnDisable();
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_209.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_212.OnDisable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_220.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_221.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_238.OnDisable();
		logic_uScript_Wait_uScript_Wait_242.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_249.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_275.OnDisable();
		logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_279.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_294.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_296.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_297.OnDisable();
		logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_302.OnDisable();
		logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_305.OnDisable();
		logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_323.OnDisable();
		logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_351.OnDisable();
		logic_uScript_Wait_uScript_Wait_368.OnDisable();
		logic_uScript_Wait_uScript_Wait_379.OnDisable();
		logic_uScript_Wait_uScript_Wait_396.OnDisable();
		logic_uScript_Wait_uScript_Wait_426.OnDisable();
		logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_433.OnDisable();
		logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_434.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_436.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_437.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_449.OnDisable();
		logic_uScript_Wait_uScript_Wait_450.OnDisable();
		logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_456.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_472.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_504.OnDisable();
		logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_509.OnDisable();
		logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_533.OnDisable();
		logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_554.OnDisable();
		logic_uScript_Wait_uScript_Wait_567.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_580.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_592.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_593.OnDisable();
		logic_uScript_Wait_uScript_Wait_600.OnDisable();
		logic_uScript_Wait_uScript_Wait_612.OnDisable();
		logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_628.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_635.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_638.OnDisable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_644.OnDisable();
		UnregisterEventListeners();
		m_RegisteredForEvents = false;
	}

	public void Update()
	{
		SyncEventListeners();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_36.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_58.Update();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_102.Update();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_153.Update();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_167.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_176.Update();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_178.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_186.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_192.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_194.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_200.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_202.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_203.Update();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_206.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_212.Update();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_220.Update();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_644.Update();
	}

	public void OnDestroy()
	{
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_36.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_58.OnDestroy();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_102.OnDestroy();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_153.OnDestroy();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_167.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_176.OnDestroy();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_178.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_186.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_192.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_194.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_200.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_202.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_203.OnDestroy();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_206.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_212.OnDestroy();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_220.OnDestroy();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_644.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_36.Save_Out -= SubGraph_SaveLoadBool_Save_Out_36;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_36.Load_Out -= SubGraph_SaveLoadBool_Load_Out_36;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_36.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_36;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_58.Save_Out -= SubGraph_SaveLoadBool_Save_Out_58;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_58.Load_Out -= SubGraph_SaveLoadBool_Load_Out_58;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_58.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_58;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_102.Save_Out -= SubGraph_SaveLoadInt_Save_Out_102;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_102.Load_Out -= SubGraph_SaveLoadInt_Load_Out_102;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_102.Restart_Out -= SubGraph_SaveLoadInt_Restart_Out_102;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_153.Save_Out -= SubGraph_SaveLoadInt_Save_Out_153;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_153.Load_Out -= SubGraph_SaveLoadInt_Load_Out_153;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_153.Restart_Out -= SubGraph_SaveLoadInt_Restart_Out_153;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_167.Save_Out -= SubGraph_SaveLoadInt_Save_Out_167;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_167.Load_Out -= SubGraph_SaveLoadInt_Load_Out_167;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_167.Restart_Out -= SubGraph_SaveLoadInt_Restart_Out_167;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_176.Save_Out -= SubGraph_SaveLoadBool_Save_Out_176;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_176.Load_Out -= SubGraph_SaveLoadBool_Load_Out_176;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_176.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_176;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_178.Save_Out -= SubGraph_SaveLoadInt_Save_Out_178;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_178.Load_Out -= SubGraph_SaveLoadInt_Load_Out_178;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_178.Restart_Out -= SubGraph_SaveLoadInt_Restart_Out_178;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_186.Save_Out -= SubGraph_SaveLoadBool_Save_Out_186;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_186.Load_Out -= SubGraph_SaveLoadBool_Load_Out_186;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_186.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_186;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_192.Save_Out -= SubGraph_SaveLoadBool_Save_Out_192;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_192.Load_Out -= SubGraph_SaveLoadBool_Load_Out_192;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_192.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_192;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_194.Save_Out -= SubGraph_SaveLoadBool_Save_Out_194;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_194.Load_Out -= SubGraph_SaveLoadBool_Load_Out_194;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_194.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_194;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_200.Save_Out -= SubGraph_SaveLoadBool_Save_Out_200;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_200.Load_Out -= SubGraph_SaveLoadBool_Load_Out_200;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_200.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_200;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_202.Save_Out -= SubGraph_SaveLoadBool_Save_Out_202;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_202.Load_Out -= SubGraph_SaveLoadBool_Load_Out_202;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_202.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_202;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_203.Save_Out -= SubGraph_SaveLoadBool_Save_Out_203;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_203.Load_Out -= SubGraph_SaveLoadBool_Load_Out_203;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_203.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_203;
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_206.Out -= SubGraph_LoadObjectiveStates_Out_206;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_212.Save_Out -= SubGraph_SaveLoadBool_Save_Out_212;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_212.Load_Out -= SubGraph_SaveLoadBool_Load_Out_212;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_212.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_212;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_220.Out -= SubGraph_CompleteObjectiveStage_Out_220;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_222.Output1 -= uScriptCon_ManualSwitch_Output1_222;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_222.Output2 -= uScriptCon_ManualSwitch_Output2_222;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_222.Output3 -= uScriptCon_ManualSwitch_Output3_222;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_222.Output4 -= uScriptCon_ManualSwitch_Output4_222;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_222.Output5 -= uScriptCon_ManualSwitch_Output5_222;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_222.Output6 -= uScriptCon_ManualSwitch_Output6_222;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_222.Output7 -= uScriptCon_ManualSwitch_Output7_222;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_222.Output8 -= uScriptCon_ManualSwitch_Output8_222;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_245.Output1 -= uScriptCon_ManualSwitch_Output1_245;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_245.Output2 -= uScriptCon_ManualSwitch_Output2_245;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_245.Output3 -= uScriptCon_ManualSwitch_Output3_245;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_245.Output4 -= uScriptCon_ManualSwitch_Output4_245;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_245.Output5 -= uScriptCon_ManualSwitch_Output5_245;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_245.Output6 -= uScriptCon_ManualSwitch_Output6_245;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_245.Output7 -= uScriptCon_ManualSwitch_Output7_245;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_245.Output8 -= uScriptCon_ManualSwitch_Output8_245;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_280.Output1 -= uScriptCon_ManualSwitch_Output1_280;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_280.Output2 -= uScriptCon_ManualSwitch_Output2_280;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_280.Output3 -= uScriptCon_ManualSwitch_Output3_280;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_280.Output4 -= uScriptCon_ManualSwitch_Output4_280;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_280.Output5 -= uScriptCon_ManualSwitch_Output5_280;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_280.Output6 -= uScriptCon_ManualSwitch_Output6_280;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_280.Output7 -= uScriptCon_ManualSwitch_Output7_280;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_280.Output8 -= uScriptCon_ManualSwitch_Output8_280;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_422.Output1 -= uScriptCon_ManualSwitch_Output1_422;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_422.Output2 -= uScriptCon_ManualSwitch_Output2_422;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_422.Output3 -= uScriptCon_ManualSwitch_Output3_422;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_422.Output4 -= uScriptCon_ManualSwitch_Output4_422;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_422.Output5 -= uScriptCon_ManualSwitch_Output5_422;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_422.Output6 -= uScriptCon_ManualSwitch_Output6_422;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_422.Output7 -= uScriptCon_ManualSwitch_Output7_422;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_422.Output8 -= uScriptCon_ManualSwitch_Output8_422;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_515.Output1 -= uScriptCon_ManualSwitch_Output1_515;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_515.Output2 -= uScriptCon_ManualSwitch_Output2_515;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_515.Output3 -= uScriptCon_ManualSwitch_Output3_515;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_515.Output4 -= uScriptCon_ManualSwitch_Output4_515;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_515.Output5 -= uScriptCon_ManualSwitch_Output5_515;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_515.Output6 -= uScriptCon_ManualSwitch_Output6_515;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_515.Output7 -= uScriptCon_ManualSwitch_Output7_515;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_515.Output8 -= uScriptCon_ManualSwitch_Output8_515;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_644.Save_Out -= SubGraph_SaveLoadInt_Save_Out_644;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_644.Load_Out -= SubGraph_SaveLoadInt_Load_Out_644;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_644.Restart_Out -= SubGraph_SaveLoadInt_Restart_Out_644;
	}

	public void OnGUI()
	{
		logic_uScriptAct_PrintText_uScriptAct_PrintText_190.OnGUI();
	}

	private void Instance_OnUpdate_35(object o, EventArgs e)
	{
		Relay_OnUpdate_35();
	}

	private void Instance_OnSuspend_35(object o, EventArgs e)
	{
		Relay_OnSuspend_35();
	}

	private void Instance_OnResume_35(object o, EventArgs e)
	{
		Relay_OnResume_35();
	}

	private void Instance_SaveEvent_100(object o, EventArgs e)
	{
		Relay_SaveEvent_100();
	}

	private void Instance_LoadEvent_100(object o, EventArgs e)
	{
		Relay_LoadEvent_100();
	}

	private void Instance_RestartEvent_100(object o, EventArgs e)
	{
		Relay_RestartEvent_100();
	}

	private void SubGraph_SaveLoadBool_Save_Out_36(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_36 = e.boolean;
		local_CanPushButtons_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_36;
		Relay_Save_Out_36();
	}

	private void SubGraph_SaveLoadBool_Load_Out_36(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_36 = e.boolean;
		local_CanPushButtons_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_36;
		Relay_Load_Out_36();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_36(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_36 = e.boolean;
		local_CanPushButtons_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_36;
		Relay_Restart_Out_36();
	}

	private void SubGraph_SaveLoadBool_Save_Out_58(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_58 = e.boolean;
		local_msgIntro_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_58;
		Relay_Save_Out_58();
	}

	private void SubGraph_SaveLoadBool_Load_Out_58(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_58 = e.boolean;
		local_msgIntro_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_58;
		Relay_Load_Out_58();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_58(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_58 = e.boolean;
		local_msgIntro_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_58;
		Relay_Restart_Out_58();
	}

	private void SubGraph_SaveLoadInt_Save_Out_102(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_102 = e.integer;
		local_QuestionTwo_System_Int32 = logic_SubGraph_SaveLoadInt_integer_102;
		Relay_Save_Out_102();
	}

	private void SubGraph_SaveLoadInt_Load_Out_102(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_102 = e.integer;
		local_QuestionTwo_System_Int32 = logic_SubGraph_SaveLoadInt_integer_102;
		Relay_Load_Out_102();
	}

	private void SubGraph_SaveLoadInt_Restart_Out_102(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_102 = e.integer;
		local_QuestionTwo_System_Int32 = logic_SubGraph_SaveLoadInt_integer_102;
		Relay_Restart_Out_102();
	}

	private void SubGraph_SaveLoadInt_Save_Out_153(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_153 = e.integer;
		local_Stage_System_Int32 = logic_SubGraph_SaveLoadInt_integer_153;
		Relay_Save_Out_153();
	}

	private void SubGraph_SaveLoadInt_Load_Out_153(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_153 = e.integer;
		local_Stage_System_Int32 = logic_SubGraph_SaveLoadInt_integer_153;
		Relay_Load_Out_153();
	}

	private void SubGraph_SaveLoadInt_Restart_Out_153(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_153 = e.integer;
		local_Stage_System_Int32 = logic_SubGraph_SaveLoadInt_integer_153;
		Relay_Restart_Out_153();
	}

	private void SubGraph_SaveLoadInt_Save_Out_167(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_167 = e.integer;
		local_QuestionOne_System_Int32 = logic_SubGraph_SaveLoadInt_integer_167;
		Relay_Save_Out_167();
	}

	private void SubGraph_SaveLoadInt_Load_Out_167(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_167 = e.integer;
		local_QuestionOne_System_Int32 = logic_SubGraph_SaveLoadInt_integer_167;
		Relay_Load_Out_167();
	}

	private void SubGraph_SaveLoadInt_Restart_Out_167(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_167 = e.integer;
		local_QuestionOne_System_Int32 = logic_SubGraph_SaveLoadInt_integer_167;
		Relay_Restart_Out_167();
	}

	private void SubGraph_SaveLoadBool_Save_Out_176(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_176 = e.boolean;
		local_Initialize_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_176;
		Relay_Save_Out_176();
	}

	private void SubGraph_SaveLoadBool_Load_Out_176(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_176 = e.boolean;
		local_Initialize_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_176;
		Relay_Load_Out_176();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_176(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_176 = e.boolean;
		local_Initialize_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_176;
		Relay_Restart_Out_176();
	}

	private void SubGraph_SaveLoadInt_Save_Out_178(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_178 = e.integer;
		local_QuestionThree_System_Int32 = logic_SubGraph_SaveLoadInt_integer_178;
		Relay_Save_Out_178();
	}

	private void SubGraph_SaveLoadInt_Load_Out_178(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_178 = e.integer;
		local_QuestionThree_System_Int32 = logic_SubGraph_SaveLoadInt_integer_178;
		Relay_Load_Out_178();
	}

	private void SubGraph_SaveLoadInt_Restart_Out_178(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_178 = e.integer;
		local_QuestionThree_System_Int32 = logic_SubGraph_SaveLoadInt_integer_178;
		Relay_Restart_Out_178();
	}

	private void SubGraph_SaveLoadBool_Save_Out_186(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_186 = e.boolean;
		local_Q1EnemyAlive_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_186;
		Relay_Save_Out_186();
	}

	private void SubGraph_SaveLoadBool_Load_Out_186(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_186 = e.boolean;
		local_Q1EnemyAlive_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_186;
		Relay_Load_Out_186();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_186(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_186 = e.boolean;
		local_Q1EnemyAlive_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_186;
		Relay_Restart_Out_186();
	}

	private void SubGraph_SaveLoadBool_Save_Out_192(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_192 = e.boolean;
		local_Q2EnemyAlive_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_192;
		Relay_Save_Out_192();
	}

	private void SubGraph_SaveLoadBool_Load_Out_192(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_192 = e.boolean;
		local_Q2EnemyAlive_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_192;
		Relay_Load_Out_192();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_192(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_192 = e.boolean;
		local_Q2EnemyAlive_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_192;
		Relay_Restart_Out_192();
	}

	private void SubGraph_SaveLoadBool_Save_Out_194(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_194 = e.boolean;
		local_Q3EnemyAlive_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_194;
		Relay_Save_Out_194();
	}

	private void SubGraph_SaveLoadBool_Load_Out_194(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_194 = e.boolean;
		local_Q3EnemyAlive_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_194;
		Relay_Load_Out_194();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_194(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_194 = e.boolean;
		local_Q3EnemyAlive_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_194;
		Relay_Restart_Out_194();
	}

	private void SubGraph_SaveLoadBool_Save_Out_200(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_200 = e.boolean;
		local_Question01WrongAnswers_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_200;
		Relay_Save_Out_200();
	}

	private void SubGraph_SaveLoadBool_Load_Out_200(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_200 = e.boolean;
		local_Question01WrongAnswers_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_200;
		Relay_Load_Out_200();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_200(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_200 = e.boolean;
		local_Question01WrongAnswers_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_200;
		Relay_Restart_Out_200();
	}

	private void SubGraph_SaveLoadBool_Save_Out_202(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_202 = e.boolean;
		local_Question02WrongAnswers_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_202;
		Relay_Save_Out_202();
	}

	private void SubGraph_SaveLoadBool_Load_Out_202(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_202 = e.boolean;
		local_Question02WrongAnswers_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_202;
		Relay_Load_Out_202();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_202(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_202 = e.boolean;
		local_Question02WrongAnswers_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_202;
		Relay_Restart_Out_202();
	}

	private void SubGraph_SaveLoadBool_Save_Out_203(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_203 = e.boolean;
		local_Question03WrongAnswers_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_203;
		Relay_Save_Out_203();
	}

	private void SubGraph_SaveLoadBool_Load_Out_203(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_203 = e.boolean;
		local_Question03WrongAnswers_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_203;
		Relay_Load_Out_203();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_203(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_203 = e.boolean;
		local_Question03WrongAnswers_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_203;
		Relay_Restart_Out_203();
	}

	private void SubGraph_LoadObjectiveStates_Out_206(object o, SubGraph_LoadObjectiveStates.LogicEventArgs e)
	{
		Relay_Out_206();
	}

	private void SubGraph_SaveLoadBool_Save_Out_212(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_212 = e.boolean;
		local_msgBaseFoundShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_212;
		Relay_Save_Out_212();
	}

	private void SubGraph_SaveLoadBool_Load_Out_212(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_212 = e.boolean;
		local_msgBaseFoundShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_212;
		Relay_Load_Out_212();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_212(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_212 = e.boolean;
		local_msgBaseFoundShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_212;
		Relay_Restart_Out_212();
	}

	private void SubGraph_CompleteObjectiveStage_Out_220(object o, SubGraph_CompleteObjectiveStage.LogicEventArgs e)
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_220 = e.objectiveStage;
		local_Stage_System_Int32 = logic_SubGraph_CompleteObjectiveStage_objectiveStage_220;
		Relay_Out_220();
	}

	private void uScriptCon_ManualSwitch_Output1_222(object o, EventArgs e)
	{
		Relay_Output1_222();
	}

	private void uScriptCon_ManualSwitch_Output2_222(object o, EventArgs e)
	{
		Relay_Output2_222();
	}

	private void uScriptCon_ManualSwitch_Output3_222(object o, EventArgs e)
	{
		Relay_Output3_222();
	}

	private void uScriptCon_ManualSwitch_Output4_222(object o, EventArgs e)
	{
		Relay_Output4_222();
	}

	private void uScriptCon_ManualSwitch_Output5_222(object o, EventArgs e)
	{
		Relay_Output5_222();
	}

	private void uScriptCon_ManualSwitch_Output6_222(object o, EventArgs e)
	{
		Relay_Output6_222();
	}

	private void uScriptCon_ManualSwitch_Output7_222(object o, EventArgs e)
	{
		Relay_Output7_222();
	}

	private void uScriptCon_ManualSwitch_Output8_222(object o, EventArgs e)
	{
		Relay_Output8_222();
	}

	private void uScriptCon_ManualSwitch_Output1_245(object o, EventArgs e)
	{
		Relay_Output1_245();
	}

	private void uScriptCon_ManualSwitch_Output2_245(object o, EventArgs e)
	{
		Relay_Output2_245();
	}

	private void uScriptCon_ManualSwitch_Output3_245(object o, EventArgs e)
	{
		Relay_Output3_245();
	}

	private void uScriptCon_ManualSwitch_Output4_245(object o, EventArgs e)
	{
		Relay_Output4_245();
	}

	private void uScriptCon_ManualSwitch_Output5_245(object o, EventArgs e)
	{
		Relay_Output5_245();
	}

	private void uScriptCon_ManualSwitch_Output6_245(object o, EventArgs e)
	{
		Relay_Output6_245();
	}

	private void uScriptCon_ManualSwitch_Output7_245(object o, EventArgs e)
	{
		Relay_Output7_245();
	}

	private void uScriptCon_ManualSwitch_Output8_245(object o, EventArgs e)
	{
		Relay_Output8_245();
	}

	private void uScriptCon_ManualSwitch_Output1_280(object o, EventArgs e)
	{
		Relay_Output1_280();
	}

	private void uScriptCon_ManualSwitch_Output2_280(object o, EventArgs e)
	{
		Relay_Output2_280();
	}

	private void uScriptCon_ManualSwitch_Output3_280(object o, EventArgs e)
	{
		Relay_Output3_280();
	}

	private void uScriptCon_ManualSwitch_Output4_280(object o, EventArgs e)
	{
		Relay_Output4_280();
	}

	private void uScriptCon_ManualSwitch_Output5_280(object o, EventArgs e)
	{
		Relay_Output5_280();
	}

	private void uScriptCon_ManualSwitch_Output6_280(object o, EventArgs e)
	{
		Relay_Output6_280();
	}

	private void uScriptCon_ManualSwitch_Output7_280(object o, EventArgs e)
	{
		Relay_Output7_280();
	}

	private void uScriptCon_ManualSwitch_Output8_280(object o, EventArgs e)
	{
		Relay_Output8_280();
	}

	private void uScriptCon_ManualSwitch_Output1_422(object o, EventArgs e)
	{
		Relay_Output1_422();
	}

	private void uScriptCon_ManualSwitch_Output2_422(object o, EventArgs e)
	{
		Relay_Output2_422();
	}

	private void uScriptCon_ManualSwitch_Output3_422(object o, EventArgs e)
	{
		Relay_Output3_422();
	}

	private void uScriptCon_ManualSwitch_Output4_422(object o, EventArgs e)
	{
		Relay_Output4_422();
	}

	private void uScriptCon_ManualSwitch_Output5_422(object o, EventArgs e)
	{
		Relay_Output5_422();
	}

	private void uScriptCon_ManualSwitch_Output6_422(object o, EventArgs e)
	{
		Relay_Output6_422();
	}

	private void uScriptCon_ManualSwitch_Output7_422(object o, EventArgs e)
	{
		Relay_Output7_422();
	}

	private void uScriptCon_ManualSwitch_Output8_422(object o, EventArgs e)
	{
		Relay_Output8_422();
	}

	private void uScriptCon_ManualSwitch_Output1_515(object o, EventArgs e)
	{
		Relay_Output1_515();
	}

	private void uScriptCon_ManualSwitch_Output2_515(object o, EventArgs e)
	{
		Relay_Output2_515();
	}

	private void uScriptCon_ManualSwitch_Output3_515(object o, EventArgs e)
	{
		Relay_Output3_515();
	}

	private void uScriptCon_ManualSwitch_Output4_515(object o, EventArgs e)
	{
		Relay_Output4_515();
	}

	private void uScriptCon_ManualSwitch_Output5_515(object o, EventArgs e)
	{
		Relay_Output5_515();
	}

	private void uScriptCon_ManualSwitch_Output6_515(object o, EventArgs e)
	{
		Relay_Output6_515();
	}

	private void uScriptCon_ManualSwitch_Output7_515(object o, EventArgs e)
	{
		Relay_Output7_515();
	}

	private void uScriptCon_ManualSwitch_Output8_515(object o, EventArgs e)
	{
		Relay_Output8_515();
	}

	private void SubGraph_SaveLoadInt_Save_Out_644(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_644 = e.integer;
		local_QuestionStage_System_Int32 = logic_SubGraph_SaveLoadInt_integer_644;
		Relay_Save_Out_644();
	}

	private void SubGraph_SaveLoadInt_Load_Out_644(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_644 = e.integer;
		local_QuestionStage_System_Int32 = logic_SubGraph_SaveLoadInt_integer_644;
		Relay_Load_Out_644();
	}

	private void SubGraph_SaveLoadInt_Restart_Out_644(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_644 = e.integer;
		local_QuestionStage_System_Int32 = logic_SubGraph_SaveLoadInt_integer_644;
		Relay_Restart_Out_644();
	}

	private void Relay_In_7()
	{
		logic_uScriptCon_CompareBool_Bool_7 = local_NearNPC_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_7.In(logic_uScriptCon_CompareBool_Bool_7);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_7.True)
		{
			Relay_In_177();
		}
	}

	private void Relay_In_8()
	{
		logic_uScript_SetCustomRadarTeamID_tech_8 = local_ButtonBase2Tech_Tank;
		logic_uScript_SetCustomRadarTeamID_uScript_SetCustomRadarTeamID_8.In(logic_uScript_SetCustomRadarTeamID_tech_8, logic_uScript_SetCustomRadarTeamID_radarTeamID_8);
		if (logic_uScript_SetCustomRadarTeamID_uScript_SetCustomRadarTeamID_8.Out)
		{
			Relay_In_132();
		}
	}

	private void Relay_In_9()
	{
		logic_uScript_SetTankInvulnerable_tank_9 = local_ButtonBase2Tech_Tank;
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_9.In(logic_uScript_SetTankInvulnerable_invulnerable_9, logic_uScript_SetTankInvulnerable_tank_9);
		if (logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_9.Out)
		{
			Relay_In_8();
		}
	}

	private void Relay_In_10()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_10.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_10.Out)
		{
			Relay_In_105();
		}
	}

	private void Relay_In_12()
	{
		logic_uScript_SetCustomRadarTeamID_tech_12 = local_NPCTech_Tank;
		logic_uScript_SetCustomRadarTeamID_uScript_SetCustomRadarTeamID_12.In(logic_uScript_SetCustomRadarTeamID_tech_12, logic_uScript_SetCustomRadarTeamID_radarTeamID_12);
		if (logic_uScript_SetCustomRadarTeamID_uScript_SetCustomRadarTeamID_12.Out)
		{
			Relay_In_171();
		}
	}

	private void Relay_In_20()
	{
		logic_uScript_LockTechInteraction_tech_20 = local_ButtonBase4Tech_Tank;
		int num = 0;
		if (logic_uScript_LockTechInteraction_excludedBlocks_20.Length <= num)
		{
			Array.Resize(ref logic_uScript_LockTechInteraction_excludedBlocks_20, num + 1);
		}
		logic_uScript_LockTechInteraction_excludedBlocks_20[num++] = blockTypeButton4;
		int num2 = 0;
		if (logic_uScript_LockTechInteraction_excludedUniqueBlocks_20.Length <= num2)
		{
			Array.Resize(ref logic_uScript_LockTechInteraction_excludedUniqueBlocks_20, num2 + 1);
		}
		logic_uScript_LockTechInteraction_excludedUniqueBlocks_20[num2++] = local_ButtonBlock4_TankBlock;
		logic_uScript_LockTechInteraction_uScript_LockTechInteraction_20.In(logic_uScript_LockTechInteraction_tech_20, logic_uScript_LockTechInteraction_excludedBlocks_20, logic_uScript_LockTechInteraction_excludedUniqueBlocks_20);
		if (logic_uScript_LockTechInteraction_uScript_LockTechInteraction_20.Out)
		{
			Relay_In_209();
		}
	}

	private void Relay_In_21()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_21.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_21.Out)
		{
			Relay_In_163();
		}
	}

	private void Relay_In_25()
	{
		logic_uScript_LockTechInteraction_tech_25 = local_ButtonBase3Tech_Tank;
		int num = 0;
		if (logic_uScript_LockTechInteraction_excludedBlocks_25.Length <= num)
		{
			Array.Resize(ref logic_uScript_LockTechInteraction_excludedBlocks_25, num + 1);
		}
		logic_uScript_LockTechInteraction_excludedBlocks_25[num++] = blockTypeButton3;
		int num2 = 0;
		if (logic_uScript_LockTechInteraction_excludedUniqueBlocks_25.Length <= num2)
		{
			Array.Resize(ref logic_uScript_LockTechInteraction_excludedUniqueBlocks_25, num2 + 1);
		}
		logic_uScript_LockTechInteraction_excludedUniqueBlocks_25[num2++] = local_ButtonBlock3_TankBlock;
		logic_uScript_LockTechInteraction_uScript_LockTechInteraction_25.In(logic_uScript_LockTechInteraction_tech_25, logic_uScript_LockTechInteraction_excludedBlocks_25, logic_uScript_LockTechInteraction_excludedUniqueBlocks_25);
		if (logic_uScript_LockTechInteraction_uScript_LockTechInteraction_25.Out)
		{
			Relay_In_20();
		}
	}

	private void Relay_In_28()
	{
		logic_uScript_SetTankInvulnerable_tank_28 = local_ButtonBase4Tech_Tank;
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_28.In(logic_uScript_SetTankInvulnerable_invulnerable_28, logic_uScript_SetTankInvulnerable_tank_28);
		if (logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_28.Out)
		{
			Relay_In_55();
		}
	}

	private void Relay_Pause_33()
	{
		logic_uScript_PausePopulation_uScript_PausePopulation_33.Pause();
		if (logic_uScript_PausePopulation_uScript_PausePopulation_33.Out)
		{
			Relay_In_54();
		}
	}

	private void Relay_UnPause_33()
	{
		logic_uScript_PausePopulation_uScript_PausePopulation_33.UnPause();
		if (logic_uScript_PausePopulation_uScript_PausePopulation_33.Out)
		{
			Relay_In_54();
		}
	}

	private void Relay_In_34()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_34.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_34.Out)
		{
			Relay_In_148();
		}
	}

	private void Relay_OnUpdate_35()
	{
		Relay_In_147();
	}

	private void Relay_OnSuspend_35()
	{
	}

	private void Relay_OnResume_35()
	{
	}

	private void Relay_Save_Out_36()
	{
		Relay_Save_186();
	}

	private void Relay_Load_Out_36()
	{
		Relay_Load_186();
	}

	private void Relay_Restart_Out_36()
	{
		Relay_Set_False_186();
	}

	private void Relay_Save_36()
	{
		logic_SubGraph_SaveLoadBool_boolean_36 = local_CanPushButtons_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_36 = local_CanPushButtons_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_36.Save(ref logic_SubGraph_SaveLoadBool_boolean_36, logic_SubGraph_SaveLoadBool_boolAsVariable_36, logic_SubGraph_SaveLoadBool_uniqueID_36);
	}

	private void Relay_Load_36()
	{
		logic_SubGraph_SaveLoadBool_boolean_36 = local_CanPushButtons_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_36 = local_CanPushButtons_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_36.Load(ref logic_SubGraph_SaveLoadBool_boolean_36, logic_SubGraph_SaveLoadBool_boolAsVariable_36, logic_SubGraph_SaveLoadBool_uniqueID_36);
	}

	private void Relay_Set_True_36()
	{
		logic_SubGraph_SaveLoadBool_boolean_36 = local_CanPushButtons_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_36 = local_CanPushButtons_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_36.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_36, logic_SubGraph_SaveLoadBool_boolAsVariable_36, logic_SubGraph_SaveLoadBool_uniqueID_36);
	}

	private void Relay_Set_False_36()
	{
		logic_SubGraph_SaveLoadBool_boolean_36 = local_CanPushButtons_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_36 = local_CanPushButtons_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_36.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_36, logic_SubGraph_SaveLoadBool_boolAsVariable_36, logic_SubGraph_SaveLoadBool_uniqueID_36);
	}

	private void Relay_In_37()
	{
		logic_uScript_LockTechInteraction_tech_37 = local_ButtonBase2Tech_Tank;
		int num = 0;
		if (logic_uScript_LockTechInteraction_excludedBlocks_37.Length <= num)
		{
			Array.Resize(ref logic_uScript_LockTechInteraction_excludedBlocks_37, num + 1);
		}
		logic_uScript_LockTechInteraction_excludedBlocks_37[num++] = blockTypeButton2;
		int num2 = 0;
		if (logic_uScript_LockTechInteraction_excludedUniqueBlocks_37.Length <= num2)
		{
			Array.Resize(ref logic_uScript_LockTechInteraction_excludedUniqueBlocks_37, num2 + 1);
		}
		logic_uScript_LockTechInteraction_excludedUniqueBlocks_37[num2++] = local_ButtonBlock2_TankBlock;
		logic_uScript_LockTechInteraction_uScript_LockTechInteraction_37.In(logic_uScript_LockTechInteraction_tech_37, logic_uScript_LockTechInteraction_excludedBlocks_37, logic_uScript_LockTechInteraction_excludedUniqueBlocks_37);
		if (logic_uScript_LockTechInteraction_uScript_LockTechInteraction_37.Out)
		{
			Relay_In_25();
		}
	}

	private void Relay_In_39()
	{
		logic_uScript_LockTechInteraction_tech_39 = local_NPCTech_Tank;
		logic_uScript_LockTechInteraction_uScript_LockTechInteraction_39.In(logic_uScript_LockTechInteraction_tech_39, logic_uScript_LockTechInteraction_excludedBlocks_39, logic_uScript_LockTechInteraction_excludedUniqueBlocks_39);
		if (logic_uScript_LockTechInteraction_uScript_LockTechInteraction_39.Out)
		{
			Relay_In_12();
		}
	}

	private void Relay_In_44()
	{
		logic_uScript_LockTech_tech_44 = local_ButtonBase3Tech_Tank;
		logic_uScript_LockTech_uScript_LockTech_44.In(logic_uScript_LockTech_tech_44, logic_uScript_LockTech_lockType_44);
		if (logic_uScript_LockTech_uScript_LockTech_44.Out)
		{
			Relay_In_185();
		}
	}

	private void Relay_True_45()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_45.True(out logic_uScriptAct_SetBool_Target_45);
		local_Initialize_System_Boolean = logic_uScriptAct_SetBool_Target_45;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_45.Out)
		{
			Relay_InitialSpawn_47();
		}
	}

	private void Relay_False_45()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_45.False(out logic_uScriptAct_SetBool_Target_45);
		local_Initialize_System_Boolean = logic_uScriptAct_SetBool_Target_45;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_45.Out)
		{
			Relay_InitialSpawn_47();
		}
	}

	private void Relay_In_46()
	{
		logic_uScript_SetCustomRadarTeamID_tech_46 = local_ButtonBase3Tech_Tank;
		logic_uScript_SetCustomRadarTeamID_uScript_SetCustomRadarTeamID_46.In(logic_uScript_SetCustomRadarTeamID_tech_46, logic_uScript_SetCustomRadarTeamID_radarTeamID_46);
		if (logic_uScript_SetCustomRadarTeamID_uScript_SetCustomRadarTeamID_46.Out)
		{
			Relay_In_74();
		}
	}

	private void Relay_InitialSpawn_47()
	{
		int num = 0;
		Array array = buttonbase1SpawnData;
		if (logic_uScript_SpawnTechsFromData_spawnData_47.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_47, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SpawnTechsFromData_spawnData_47, num, array.Length);
		num += array.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_47 = owner_Connection_42;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_47.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_47, logic_uScript_SpawnTechsFromData_ownerNode_47, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_47, logic_uScript_SpawnTechsFromData_allowResurrection_47);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_47.Out)
		{
			Relay_InitialSpawn_172();
		}
	}

	private void Relay_In_50()
	{
		logic_uScript_LockTechInteraction_tech_50 = local_ButtonBase3Tech_Tank;
		logic_uScript_LockTechInteraction_uScript_LockTechInteraction_50.In(logic_uScript_LockTechInteraction_tech_50, logic_uScript_LockTechInteraction_excludedBlocks_50, logic_uScript_LockTechInteraction_excludedUniqueBlocks_50);
		if (logic_uScript_LockTechInteraction_uScript_LockTechInteraction_50.Out)
		{
			Relay_In_136();
		}
	}

	private void Relay_InitialSpawn_52()
	{
		int num = 0;
		Array array = buttonbase3SpawnData;
		if (logic_uScript_SpawnTechsFromData_spawnData_52.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_52, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SpawnTechsFromData_spawnData_52, num, array.Length);
		num += array.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_52 = owner_Connection_40;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_52.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_52, logic_uScript_SpawnTechsFromData_ownerNode_52, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_52, logic_uScript_SpawnTechsFromData_allowResurrection_52);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_52.Out)
		{
			Relay_InitialSpawn_158();
		}
	}

	private void Relay_In_54()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_54 = messageTag;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_54.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_54, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_54);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_54.Out)
		{
			Relay_In_7();
		}
	}

	private void Relay_In_55()
	{
		logic_uScript_SetCustomRadarTeamID_tech_55 = local_ButtonBase4Tech_Tank;
		logic_uScript_SetCustomRadarTeamID_uScript_SetCustomRadarTeamID_55.In(logic_uScript_SetCustomRadarTeamID_tech_55, logic_uScript_SetCustomRadarTeamID_radarTeamID_55);
		if (logic_uScript_SetCustomRadarTeamID_uScript_SetCustomRadarTeamID_55.Out)
		{
			Relay_In_175();
		}
	}

	private void Relay_Save_Out_58()
	{
		Relay_Save_212();
	}

	private void Relay_Load_Out_58()
	{
		Relay_Load_212();
	}

	private void Relay_Restart_Out_58()
	{
		Relay_Set_False_212();
	}

	private void Relay_Save_58()
	{
		logic_SubGraph_SaveLoadBool_boolean_58 = local_msgIntro_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_58 = local_msgIntro_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_58.Save(ref logic_SubGraph_SaveLoadBool_boolean_58, logic_SubGraph_SaveLoadBool_boolAsVariable_58, logic_SubGraph_SaveLoadBool_uniqueID_58);
	}

	private void Relay_Load_58()
	{
		logic_SubGraph_SaveLoadBool_boolean_58 = local_msgIntro_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_58 = local_msgIntro_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_58.Load(ref logic_SubGraph_SaveLoadBool_boolean_58, logic_SubGraph_SaveLoadBool_boolAsVariable_58, logic_SubGraph_SaveLoadBool_uniqueID_58);
	}

	private void Relay_Set_True_58()
	{
		logic_SubGraph_SaveLoadBool_boolean_58 = local_msgIntro_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_58 = local_msgIntro_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_58.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_58, logic_SubGraph_SaveLoadBool_boolAsVariable_58, logic_SubGraph_SaveLoadBool_uniqueID_58);
	}

	private void Relay_Set_False_58()
	{
		logic_SubGraph_SaveLoadBool_boolean_58 = local_msgIntro_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_58 = local_msgIntro_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_58.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_58, logic_SubGraph_SaveLoadBool_boolAsVariable_58, logic_SubGraph_SaveLoadBool_uniqueID_58);
	}

	private void Relay_In_61()
	{
		logic_uScript_GetTankBlock_tank_61 = local_ButtonBase1Tech_Tank;
		logic_uScript_GetTankBlock_blockType_61 = blockTypeButton1;
		logic_uScript_GetTankBlock_Return_61 = logic_uScript_GetTankBlock_uScript_GetTankBlock_61.In(logic_uScript_GetTankBlock_tank_61, logic_uScript_GetTankBlock_blockType_61);
		local_ButtonBlock1_TankBlock = logic_uScript_GetTankBlock_Return_61;
		if (logic_uScript_GetTankBlock_uScript_GetTankBlock_61.Returned)
		{
			Relay_In_64();
		}
	}

	private void Relay_In_64()
	{
		logic_uScript_GetTankBlock_tank_64 = local_ButtonBase2Tech_Tank;
		logic_uScript_GetTankBlock_blockType_64 = blockTypeButton2;
		logic_uScript_GetTankBlock_Return_64 = logic_uScript_GetTankBlock_uScript_GetTankBlock_64.In(logic_uScript_GetTankBlock_tank_64, logic_uScript_GetTankBlock_blockType_64);
		local_ButtonBlock2_TankBlock = logic_uScript_GetTankBlock_Return_64;
		if (logic_uScript_GetTankBlock_uScript_GetTankBlock_64.Returned)
		{
			Relay_In_113();
		}
	}

	private void Relay_In_71()
	{
		logic_uScript_GetTankBlock_tank_71 = local_ButtonBase4Tech_Tank;
		logic_uScript_GetTankBlock_blockType_71 = blockTypeButton4;
		logic_uScript_GetTankBlock_Return_71 = logic_uScript_GetTankBlock_uScript_GetTankBlock_71.In(logic_uScript_GetTankBlock_tank_71, logic_uScript_GetTankBlock_blockType_71);
		local_ButtonBlock4_TankBlock = logic_uScript_GetTankBlock_Return_71;
		if (logic_uScript_GetTankBlock_uScript_GetTankBlock_71.Returned)
		{
			Relay_In_162();
		}
	}

	private void Relay_AtIndex_72()
	{
		int num = 0;
		Array array = local_57_TankArray;
		if (logic_uScript_AccessListTech_techList_72.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_72, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_72, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_72.AtIndex(ref logic_uScript_AccessListTech_techList_72, logic_uScript_AccessListTech_index_72, out logic_uScript_AccessListTech_value_72);
		local_57_TankArray = logic_uScript_AccessListTech_techList_72;
		local_ButtonBase1Tech_Tank = logic_uScript_AccessListTech_value_72;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_72.Out)
		{
			Relay_In_89();
		}
	}

	private void Relay_In_74()
	{
		logic_uScript_SetTankHideBlockLimit_tech_74 = local_ButtonBase3Tech_Tank;
		logic_uScript_SetTankHideBlockLimit_uScript_SetTankHideBlockLimit_74.In(logic_uScript_SetTankHideBlockLimit_hidden_74, logic_uScript_SetTankHideBlockLimit_tech_74);
		if (logic_uScript_SetTankHideBlockLimit_uScript_SetTankHideBlockLimit_74.Out)
		{
			Relay_In_91();
		}
	}

	private void Relay_In_75()
	{
		logic_uScript_LockTechInteraction_tech_75 = local_ButtonBase2Tech_Tank;
		logic_uScript_LockTechInteraction_uScript_LockTechInteraction_75.In(logic_uScript_LockTechInteraction_tech_75, logic_uScript_LockTechInteraction_excludedBlocks_75, logic_uScript_LockTechInteraction_excludedUniqueBlocks_75);
		if (logic_uScript_LockTechInteraction_uScript_LockTechInteraction_75.Out)
		{
			Relay_In_50();
		}
	}

	private void Relay_In_81()
	{
		logic_uScript_SetCustomRadarTeamID_tech_81 = local_ButtonBase1Tech_Tank;
		logic_uScript_SetCustomRadarTeamID_uScript_SetCustomRadarTeamID_81.In(logic_uScript_SetCustomRadarTeamID_tech_81, logic_uScript_SetCustomRadarTeamID_radarTeamID_81);
		if (logic_uScript_SetCustomRadarTeamID_uScript_SetCustomRadarTeamID_81.Out)
		{
			Relay_In_130();
		}
	}

	private void Relay_True_82()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_82.True(out logic_uScriptAct_SetBool_Target_82);
		local_NearNPC_System_Boolean = logic_uScriptAct_SetBool_Target_82;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_82.Out)
		{
			Relay_In_222();
		}
	}

	private void Relay_False_82()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_82.False(out logic_uScriptAct_SetBool_Target_82);
		local_NearNPC_System_Boolean = logic_uScriptAct_SetBool_Target_82;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_82.Out)
		{
			Relay_In_222();
		}
	}

	private void Relay_In_83()
	{
		int num = 0;
		Array array = buttonbase3SpawnData;
		if (logic_uScript_GetAndCheckTechs_techData_83.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_83, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techData_83, num, array.Length);
		num += array.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_83 = owner_Connection_104;
		int num2 = 0;
		Array array2 = local_121_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_83.Length != num2 + array2.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_83, num2 + array2.Length);
		}
		Array.Copy(array2, 0, logic_uScript_GetAndCheckTechs_techs_83, num2, array2.Length);
		num2 += array2.Length;
		logic_uScript_GetAndCheckTechs_Return_83 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_83.In(logic_uScript_GetAndCheckTechs_techData_83, logic_uScript_GetAndCheckTechs_ownerNode_83, ref logic_uScript_GetAndCheckTechs_techs_83);
		local_121_TankArray = logic_uScript_GetAndCheckTechs_techs_83;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_83.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_83.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_83.AllDead;
		if (allAlive)
		{
			Relay_AtIndex_140();
		}
		if (someAlive)
		{
			Relay_AtIndex_140();
		}
		if (allDead)
		{
			Relay_In_159();
		}
	}

	private void Relay_Pause_86()
	{
		logic_uScript_PausePopulation_uScript_PausePopulation_86.Pause();
		if (logic_uScript_PausePopulation_uScript_PausePopulation_86.Out)
		{
			Relay_True_82();
		}
	}

	private void Relay_UnPause_86()
	{
		logic_uScript_PausePopulation_uScript_PausePopulation_86.UnPause();
		if (logic_uScript_PausePopulation_uScript_PausePopulation_86.Out)
		{
			Relay_True_82();
		}
	}

	private void Relay_In_89()
	{
		logic_uScript_LockTech_tech_89 = local_ButtonBase1Tech_Tank;
		logic_uScript_LockTech_uScript_LockTech_89.In(logic_uScript_LockTech_tech_89, logic_uScript_LockTech_lockType_89);
		if (logic_uScript_LockTech_uScript_LockTech_89.Out)
		{
			Relay_In_129();
		}
	}

	private void Relay_In_90()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_90.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_90.Out)
		{
			Relay_In_173();
		}
	}

	private void Relay_In_91()
	{
		int num = 0;
		Array array = buttonbase4SpawnData;
		if (logic_uScript_GetAndCheckTechs_techData_91.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_91, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techData_91, num, array.Length);
		num += array.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_91 = owner_Connection_139;
		int num2 = 0;
		Array array2 = local_26_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_91.Length != num2 + array2.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_91, num2 + array2.Length);
		}
		Array.Copy(array2, 0, logic_uScript_GetAndCheckTechs_techs_91, num2, array2.Length);
		num2 += array2.Length;
		logic_uScript_GetAndCheckTechs_Return_91 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_91.In(logic_uScript_GetAndCheckTechs_techData_91, logic_uScript_GetAndCheckTechs_ownerNode_91, ref logic_uScript_GetAndCheckTechs_techs_91);
		local_26_TankArray = logic_uScript_GetAndCheckTechs_techs_91;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_91.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_91.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_91.AllDead;
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
			Relay_In_115();
		}
	}

	private void Relay_In_93()
	{
		logic_uScript_SetTankInvulnerable_tank_93 = local_NPCTech_Tank;
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_93.In(logic_uScript_SetTankInvulnerable_invulnerable_93, logic_uScript_SetTankInvulnerable_tank_93);
		if (logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_93.Out)
		{
			Relay_In_119();
		}
	}

	private void Relay_In_95()
	{
		logic_uScript_LockTech_tech_95 = local_ButtonBase4Tech_Tank;
		logic_uScript_LockTech_uScript_LockTech_95.In(logic_uScript_LockTech_tech_95, logic_uScript_LockTech_lockType_95);
		if (logic_uScript_LockTech_uScript_LockTech_95.Out)
		{
			Relay_In_28();
		}
	}

	private void Relay_In_97()
	{
		logic_uScript_LockTechInteraction_tech_97 = local_ButtonBase1Tech_Tank;
		logic_uScript_LockTechInteraction_uScript_LockTechInteraction_97.In(logic_uScript_LockTechInteraction_tech_97, logic_uScript_LockTechInteraction_excludedBlocks_97, logic_uScript_LockTechInteraction_excludedUniqueBlocks_97);
		if (logic_uScript_LockTechInteraction_uScript_LockTechInteraction_97.Out)
		{
			Relay_In_75();
		}
	}

	private void Relay_SaveEvent_100()
	{
		Relay_Save_153();
	}

	private void Relay_LoadEvent_100()
	{
		Relay_Load_153();
	}

	private void Relay_RestartEvent_100()
	{
		Relay_Restart_153();
	}

	private void Relay_In_101()
	{
		logic_uScript_IsPlayerInRangeOfTech_tech_101 = local_NPCTech_Tank;
		logic_uScript_IsPlayerInRangeOfTech_range_101 = distNPCFound;
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_101.In(logic_uScript_IsPlayerInRangeOfTech_tech_101, logic_uScript_IsPlayerInRangeOfTech_range_101, logic_uScript_IsPlayerInRangeOfTech_techs_101);
		bool inRange = logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_101.InRange;
		bool outOfRange = logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_101.OutOfRange;
		if (inRange)
		{
			Relay_Pause_86();
		}
		if (outOfRange)
		{
			Relay_UnPause_33();
		}
	}

	private void Relay_Save_Out_102()
	{
		Relay_Save_178();
	}

	private void Relay_Load_Out_102()
	{
		Relay_Load_178();
	}

	private void Relay_Restart_Out_102()
	{
		Relay_Restart_178();
	}

	private void Relay_Save_102()
	{
		logic_SubGraph_SaveLoadInt_integer_102 = local_QuestionTwo_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_102 = local_QuestionTwo_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_102.Save(logic_SubGraph_SaveLoadInt_restartValue_102, ref logic_SubGraph_SaveLoadInt_integer_102, logic_SubGraph_SaveLoadInt_intAsVariable_102, logic_SubGraph_SaveLoadInt_uniqueID_102);
	}

	private void Relay_Load_102()
	{
		logic_SubGraph_SaveLoadInt_integer_102 = local_QuestionTwo_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_102 = local_QuestionTwo_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_102.Load(logic_SubGraph_SaveLoadInt_restartValue_102, ref logic_SubGraph_SaveLoadInt_integer_102, logic_SubGraph_SaveLoadInt_intAsVariable_102, logic_SubGraph_SaveLoadInt_uniqueID_102);
	}

	private void Relay_Restart_102()
	{
		logic_SubGraph_SaveLoadInt_integer_102 = local_QuestionTwo_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_102 = local_QuestionTwo_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_102.Restart(logic_SubGraph_SaveLoadInt_restartValue_102, ref logic_SubGraph_SaveLoadInt_integer_102, logic_SubGraph_SaveLoadInt_intAsVariable_102, logic_SubGraph_SaveLoadInt_uniqueID_102);
	}

	private void Relay_In_105()
	{
		int num = 0;
		Array array = buttonbase2SpawnData;
		if (logic_uScript_GetAndCheckTechs_techData_105.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_105, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techData_105, num, array.Length);
		num += array.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_105 = owner_Connection_2;
		int num2 = 0;
		Array array2 = local_84_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_105.Length != num2 + array2.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_105, num2 + array2.Length);
		}
		Array.Copy(array2, 0, logic_uScript_GetAndCheckTechs_techs_105, num2, array2.Length);
		num2 += array2.Length;
		logic_uScript_GetAndCheckTechs_Return_105 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_105.In(logic_uScript_GetAndCheckTechs_techData_105, logic_uScript_GetAndCheckTechs_ownerNode_105, ref logic_uScript_GetAndCheckTechs_techs_105);
		local_84_TankArray = logic_uScript_GetAndCheckTechs_techs_105;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_105.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_105.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_105.AllDead;
		if (allAlive)
		{
			Relay_AtIndex_116();
		}
		if (someAlive)
		{
			Relay_AtIndex_116();
		}
		if (allDead)
		{
			Relay_In_21();
		}
	}

	private void Relay_In_108()
	{
		logic_uScriptCon_CompareBool_Bool_108 = local_msgIntro_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_108.In(logic_uScriptCon_CompareBool_Bool_108);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_108.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_108.False;
		if (num)
		{
			Relay_In_101();
		}
		if (flag)
		{
			Relay_True_208();
		}
	}

	private void Relay_In_113()
	{
		logic_uScript_GetTankBlock_tank_113 = local_ButtonBase3Tech_Tank;
		logic_uScript_GetTankBlock_blockType_113 = blockTypeButton3;
		logic_uScript_GetTankBlock_Return_113 = logic_uScript_GetTankBlock_uScript_GetTankBlock_113.In(logic_uScript_GetTankBlock_tank_113, logic_uScript_GetTankBlock_blockType_113);
		local_ButtonBlock3_TankBlock = logic_uScript_GetTankBlock_Return_113;
		if (logic_uScript_GetTankBlock_uScript_GetTankBlock_113.Returned)
		{
			Relay_In_71();
		}
	}

	private void Relay_In_115()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_115.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_115.Out)
		{
			Relay_In_90();
		}
	}

	private void Relay_AtIndex_116()
	{
		int num = 0;
		Array array = local_84_TankArray;
		if (logic_uScript_AccessListTech_techList_116.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_116, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_116, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_116.AtIndex(ref logic_uScript_AccessListTech_techList_116, logic_uScript_AccessListTech_index_116, out logic_uScript_AccessListTech_value_116);
		local_84_TankArray = logic_uScript_AccessListTech_techList_116;
		local_ButtonBase2Tech_Tank = logic_uScript_AccessListTech_value_116;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_116.Out)
		{
			Relay_In_143();
		}
	}

	private void Relay_In_117()
	{
		logic_uScript_SetEncounterTarget_owner_117 = owner_Connection_14;
		logic_uScript_SetEncounterTarget_visibleObject_117 = local_NPCTech_Tank;
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_117.In(logic_uScript_SetEncounterTarget_owner_117, logic_uScript_SetEncounterTarget_visibleObject_117);
		if (logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_117.Out)
		{
			Relay_In_61();
		}
	}

	private void Relay_In_119()
	{
		logic_uScript_LockTech_tech_119 = local_NPCTech_Tank;
		logic_uScript_LockTech_uScript_LockTech_119.In(logic_uScript_LockTech_tech_119, logic_uScript_LockTech_lockType_119);
		if (logic_uScript_LockTech_uScript_LockTech_119.Out)
		{
			Relay_In_39();
		}
	}

	private void Relay_AtIndex_122()
	{
		int num = 0;
		Array array = local_94_TankArray;
		if (logic_uScript_AccessListTech_techList_122.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_122, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_122, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_122.AtIndex(ref logic_uScript_AccessListTech_techList_122, logic_uScript_AccessListTech_index_122, out logic_uScript_AccessListTech_value_122);
		local_94_TankArray = logic_uScript_AccessListTech_techList_122;
		local_NPCTech_Tank = logic_uScript_AccessListTech_value_122;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_122.Out)
		{
			Relay_In_93();
		}
	}

	private void Relay_AtIndex_124()
	{
		int num = 0;
		Array array = local_26_TankArray;
		if (logic_uScript_AccessListTech_techList_124.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_124, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_124, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_124.AtIndex(ref logic_uScript_AccessListTech_techList_124, logic_uScript_AccessListTech_index_124, out logic_uScript_AccessListTech_value_124);
		local_26_TankArray = logic_uScript_AccessListTech_techList_124;
		local_ButtonBase4Tech_Tank = logic_uScript_AccessListTech_value_124;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_124.Out)
		{
			Relay_In_95();
		}
	}

	private void Relay_In_128()
	{
		int num = 0;
		Array array = buttonbase1SpawnData;
		if (logic_uScript_GetAndCheckTechs_techData_128.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_128, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techData_128, num, array.Length);
		num += array.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_128 = owner_Connection_56;
		int num2 = 0;
		Array array2 = local_57_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_128.Length != num2 + array2.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_128, num2 + array2.Length);
		}
		Array.Copy(array2, 0, logic_uScript_GetAndCheckTechs_techs_128, num2, array2.Length);
		num2 += array2.Length;
		logic_uScript_GetAndCheckTechs_Return_128 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_128.In(logic_uScript_GetAndCheckTechs_techData_128, logic_uScript_GetAndCheckTechs_ownerNode_128, ref logic_uScript_GetAndCheckTechs_techs_128);
		local_57_TankArray = logic_uScript_GetAndCheckTechs_techs_128;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_128.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_128.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_128.AllDead;
		if (allAlive)
		{
			Relay_AtIndex_72();
		}
		if (someAlive)
		{
			Relay_AtIndex_72();
		}
		if (allDead)
		{
			Relay_In_138();
		}
	}

	private void Relay_In_129()
	{
		logic_uScript_SetTankInvulnerable_tank_129 = local_ButtonBase1Tech_Tank;
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_129.In(logic_uScript_SetTankInvulnerable_invulnerable_129, logic_uScript_SetTankInvulnerable_tank_129);
		if (logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_129.Out)
		{
			Relay_In_81();
		}
	}

	private void Relay_In_130()
	{
		logic_uScript_SetTankHideBlockLimit_tech_130 = local_ButtonBase1Tech_Tank;
		logic_uScript_SetTankHideBlockLimit_uScript_SetTankHideBlockLimit_130.In(logic_uScript_SetTankHideBlockLimit_hidden_130, logic_uScript_SetTankHideBlockLimit_tech_130);
		if (logic_uScript_SetTankHideBlockLimit_uScript_SetTankHideBlockLimit_130.Out)
		{
			Relay_In_105();
		}
	}

	private void Relay_In_132()
	{
		logic_uScript_SetTankHideBlockLimit_tech_132 = local_ButtonBase2Tech_Tank;
		logic_uScript_SetTankHideBlockLimit_uScript_SetTankHideBlockLimit_132.In(logic_uScript_SetTankHideBlockLimit_hidden_132, logic_uScript_SetTankHideBlockLimit_tech_132);
		if (logic_uScript_SetTankHideBlockLimit_uScript_SetTankHideBlockLimit_132.Out)
		{
			Relay_In_83();
		}
	}

	private void Relay_In_133()
	{
		logic_uScript_RemoveScenery_ownerNode_133 = owner_Connection_41;
		logic_uScript_RemoveScenery_positionName_133 = ButtonBasePosition;
		logic_uScript_RemoveScenery_radius_133 = clearSceneryRadius;
		logic_uScript_RemoveScenery_uScript_RemoveScenery_133.In(logic_uScript_RemoveScenery_ownerNode_133, logic_uScript_RemoveScenery_positionName_133, logic_uScript_RemoveScenery_radius_133, logic_uScript_RemoveScenery_preventChunksSpawning_133);
		if (logic_uScript_RemoveScenery_uScript_RemoveScenery_133.Out)
		{
			Relay_In_128();
		}
	}

	private void Relay_In_136()
	{
		logic_uScript_LockTechInteraction_tech_136 = local_ButtonBase4Tech_Tank;
		logic_uScript_LockTechInteraction_uScript_LockTechInteraction_136.In(logic_uScript_LockTechInteraction_tech_136, logic_uScript_LockTechInteraction_excludedBlocks_136, logic_uScript_LockTechInteraction_excludedUniqueBlocks_136);
		if (logic_uScript_LockTechInteraction_uScript_LockTechInteraction_136.Out)
		{
			Relay_In_209();
		}
	}

	private void Relay_In_138()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_138.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_138.Out)
		{
			Relay_In_10();
		}
	}

	private void Relay_AtIndex_140()
	{
		int num = 0;
		Array array = local_121_TankArray;
		if (logic_uScript_AccessListTech_techList_140.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_140, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_140, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_140.AtIndex(ref logic_uScript_AccessListTech_techList_140, logic_uScript_AccessListTech_index_140, out logic_uScript_AccessListTech_value_140);
		local_121_TankArray = logic_uScript_AccessListTech_techList_140;
		local_ButtonBase3Tech_Tank = logic_uScript_AccessListTech_value_140;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_140.Out)
		{
			Relay_In_44();
		}
	}

	private void Relay_In_143()
	{
		logic_uScript_LockTech_tech_143 = local_ButtonBase2Tech_Tank;
		logic_uScript_LockTech_uScript_LockTech_143.In(logic_uScript_LockTech_tech_143, logic_uScript_LockTech_lockType_143);
		if (logic_uScript_LockTech_uScript_LockTech_143.Out)
		{
			Relay_In_9();
		}
	}

	private void Relay_In_144()
	{
		logic_uScript_AddMessage_messageData_144 = msg01Intro;
		logic_uScript_AddMessage_speaker_144 = messageSpeaker;
		logic_uScript_AddMessage_Return_144 = logic_uScript_AddMessage_uScript_AddMessage_144.In(logic_uScript_AddMessage_messageData_144, logic_uScript_AddMessage_speaker_144);
		if (logic_uScript_AddMessage_uScript_AddMessage_144.Out)
		{
			Relay_In_101();
		}
	}

	private void Relay_In_147()
	{
		logic_uScriptCon_CompareBool_Bool_147 = local_Initialize_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_147.In(logic_uScriptCon_CompareBool_Bool_147);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_147.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_147.False;
		if (num)
		{
			Relay_In_181();
		}
		if (flag)
		{
			Relay_True_45();
		}
	}

	private void Relay_In_148()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_148.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_148.Out)
		{
			Relay_In_171();
		}
	}

	private void Relay_True_150()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_150.True(out logic_uScriptAct_SetBool_Target_150);
		local_NearNPC_System_Boolean = logic_uScriptAct_SetBool_Target_150;
	}

	private void Relay_False_150()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_150.False(out logic_uScriptAct_SetBool_Target_150);
		local_NearNPC_System_Boolean = logic_uScriptAct_SetBool_Target_150;
	}

	private void Relay_Save_Out_153()
	{
		Relay_Save_644();
	}

	private void Relay_Load_Out_153()
	{
		Relay_Load_644();
	}

	private void Relay_Restart_Out_153()
	{
		Relay_Restart_644();
	}

	private void Relay_Save_153()
	{
		logic_SubGraph_SaveLoadInt_integer_153 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_153 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_153.Save(logic_SubGraph_SaveLoadInt_restartValue_153, ref logic_SubGraph_SaveLoadInt_integer_153, logic_SubGraph_SaveLoadInt_intAsVariable_153, logic_SubGraph_SaveLoadInt_uniqueID_153);
	}

	private void Relay_Load_153()
	{
		logic_SubGraph_SaveLoadInt_integer_153 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_153 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_153.Load(logic_SubGraph_SaveLoadInt_restartValue_153, ref logic_SubGraph_SaveLoadInt_integer_153, logic_SubGraph_SaveLoadInt_intAsVariable_153, logic_SubGraph_SaveLoadInt_uniqueID_153);
	}

	private void Relay_Restart_153()
	{
		logic_SubGraph_SaveLoadInt_integer_153 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_153 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_153.Restart(logic_SubGraph_SaveLoadInt_restartValue_153, ref logic_SubGraph_SaveLoadInt_integer_153, logic_SubGraph_SaveLoadInt_intAsVariable_153, logic_SubGraph_SaveLoadInt_uniqueID_153);
	}

	private void Relay_In_157()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_157.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_157.Out)
		{
			Relay_In_91();
		}
	}

	private void Relay_InitialSpawn_158()
	{
		int num = 0;
		Array array = buttonbase4SpawnData;
		if (logic_uScript_SpawnTechsFromData_spawnData_158.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_158, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SpawnTechsFromData_spawnData_158, num, array.Length);
		num += array.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_158 = owner_Connection_23;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_158.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_158, logic_uScript_SpawnTechsFromData_ownerNode_158, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_158, logic_uScript_SpawnTechsFromData_allowResurrection_158);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_158.Out)
		{
			Relay_InitialSpawn_174();
		}
	}

	private void Relay_In_159()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_159.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_159.Out)
		{
			Relay_In_157();
		}
	}

	private void Relay_In_162()
	{
		logic_uScriptCon_CompareBool_Bool_162 = local_CanPushButtons_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_162.In(logic_uScriptCon_CompareBool_Bool_162);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_162.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_162.False;
		if (num)
		{
			Relay_In_180();
		}
		if (flag)
		{
			Relay_In_97();
		}
	}

	private void Relay_In_163()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_163.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_163.Out)
		{
			Relay_In_83();
		}
	}

	private void Relay_Save_Out_167()
	{
		Relay_Save_102();
	}

	private void Relay_Load_Out_167()
	{
		Relay_Load_102();
	}

	private void Relay_Restart_Out_167()
	{
		Relay_Restart_102();
	}

	private void Relay_Save_167()
	{
		logic_SubGraph_SaveLoadInt_integer_167 = local_QuestionOne_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_167 = local_QuestionOne_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_167.Save(logic_SubGraph_SaveLoadInt_restartValue_167, ref logic_SubGraph_SaveLoadInt_integer_167, logic_SubGraph_SaveLoadInt_intAsVariable_167, logic_SubGraph_SaveLoadInt_uniqueID_167);
	}

	private void Relay_Load_167()
	{
		logic_SubGraph_SaveLoadInt_integer_167 = local_QuestionOne_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_167 = local_QuestionOne_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_167.Load(logic_SubGraph_SaveLoadInt_restartValue_167, ref logic_SubGraph_SaveLoadInt_integer_167, logic_SubGraph_SaveLoadInt_intAsVariable_167, logic_SubGraph_SaveLoadInt_uniqueID_167);
	}

	private void Relay_Restart_167()
	{
		logic_SubGraph_SaveLoadInt_integer_167 = local_QuestionOne_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_167 = local_QuestionOne_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_167.Restart(logic_SubGraph_SaveLoadInt_restartValue_167, ref logic_SubGraph_SaveLoadInt_integer_167, logic_SubGraph_SaveLoadInt_intAsVariable_167, logic_SubGraph_SaveLoadInt_uniqueID_167);
	}

	private void Relay_In_171()
	{
		logic_uScript_DirectEnemiesOutOfEncounter_ownerNode_171 = owner_Connection_38;
		logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_171.In(logic_uScript_DirectEnemiesOutOfEncounter_ownerNode_171);
		if (logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_171.Out)
		{
			Relay_In_117();
		}
	}

	private void Relay_InitialSpawn_172()
	{
		int num = 0;
		Array array = buttonbase2SpawnData;
		if (logic_uScript_SpawnTechsFromData_spawnData_172.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_172, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SpawnTechsFromData_spawnData_172, num, array.Length);
		num += array.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_172 = owner_Connection_168;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_172.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_172, logic_uScript_SpawnTechsFromData_ownerNode_172, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_172, logic_uScript_SpawnTechsFromData_allowResurrection_172);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_172.Out)
		{
			Relay_InitialSpawn_52();
		}
	}

	private void Relay_In_173()
	{
		int num = 0;
		Array nPCSpawnData = NPCSpawnData;
		if (logic_uScript_GetAndCheckTechs_techData_173.Length != num + nPCSpawnData.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_173, num + nPCSpawnData.Length);
		}
		Array.Copy(nPCSpawnData, 0, logic_uScript_GetAndCheckTechs_techData_173, num, nPCSpawnData.Length);
		num += nPCSpawnData.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_173 = owner_Connection_149;
		int num2 = 0;
		Array array = local_94_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_173.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_173, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techs_173, num2, array.Length);
		num2 += array.Length;
		logic_uScript_GetAndCheckTechs_Return_173 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_173.In(logic_uScript_GetAndCheckTechs_techData_173, logic_uScript_GetAndCheckTechs_ownerNode_173, ref logic_uScript_GetAndCheckTechs_techs_173);
		local_94_TankArray = logic_uScript_GetAndCheckTechs_techs_173;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_173.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_173.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_173.AllDead;
		if (allAlive)
		{
			Relay_AtIndex_122();
		}
		if (someAlive)
		{
			Relay_AtIndex_122();
		}
		if (allDead)
		{
			Relay_In_34();
		}
	}

	private void Relay_InitialSpawn_174()
	{
		int num = 0;
		Array nPCSpawnData = NPCSpawnData;
		if (logic_uScript_SpawnTechsFromData_spawnData_174.Length != num + nPCSpawnData.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_174, num + nPCSpawnData.Length);
		}
		Array.Copy(nPCSpawnData, 0, logic_uScript_SpawnTechsFromData_spawnData_174, num, nPCSpawnData.Length);
		num += nPCSpawnData.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_174 = owner_Connection_151;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_174.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_174, logic_uScript_SpawnTechsFromData_ownerNode_174, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_174, logic_uScript_SpawnTechsFromData_allowResurrection_174);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_174.Out)
		{
			Relay_In_133();
		}
	}

	private void Relay_In_175()
	{
		logic_uScript_SetTankHideBlockLimit_tech_175 = local_ButtonBase4Tech_Tank;
		logic_uScript_SetTankHideBlockLimit_uScript_SetTankHideBlockLimit_175.In(logic_uScript_SetTankHideBlockLimit_hidden_175, logic_uScript_SetTankHideBlockLimit_tech_175);
		if (logic_uScript_SetTankHideBlockLimit_uScript_SetTankHideBlockLimit_175.Out)
		{
			Relay_In_173();
		}
	}

	private void Relay_Save_Out_176()
	{
		Relay_Save_58();
	}

	private void Relay_Load_Out_176()
	{
		Relay_Load_58();
	}

	private void Relay_Restart_Out_176()
	{
		Relay_Set_False_58();
	}

	private void Relay_Save_176()
	{
		logic_SubGraph_SaveLoadBool_boolean_176 = local_Initialize_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_176 = local_Initialize_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_176.Save(ref logic_SubGraph_SaveLoadBool_boolean_176, logic_SubGraph_SaveLoadBool_boolAsVariable_176, logic_SubGraph_SaveLoadBool_uniqueID_176);
	}

	private void Relay_Load_176()
	{
		logic_SubGraph_SaveLoadBool_boolean_176 = local_Initialize_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_176 = local_Initialize_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_176.Load(ref logic_SubGraph_SaveLoadBool_boolean_176, logic_SubGraph_SaveLoadBool_boolAsVariable_176, logic_SubGraph_SaveLoadBool_uniqueID_176);
	}

	private void Relay_Set_True_176()
	{
		logic_SubGraph_SaveLoadBool_boolean_176 = local_Initialize_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_176 = local_Initialize_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_176.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_176, logic_SubGraph_SaveLoadBool_boolAsVariable_176, logic_SubGraph_SaveLoadBool_uniqueID_176);
	}

	private void Relay_Set_False_176()
	{
		logic_SubGraph_SaveLoadBool_boolean_176 = local_Initialize_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_176 = local_Initialize_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_176.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_176, logic_SubGraph_SaveLoadBool_boolAsVariable_176, logic_SubGraph_SaveLoadBool_uniqueID_176);
	}

	private void Relay_In_177()
	{
		logic_uScript_AddMessage_messageData_177 = msgLeavingMissionArea;
		logic_uScript_AddMessage_speaker_177 = messageSpeaker;
		logic_uScript_AddMessage_Return_177 = logic_uScript_AddMessage_uScript_AddMessage_177.In(logic_uScript_AddMessage_messageData_177, logic_uScript_AddMessage_speaker_177);
		if (logic_uScript_AddMessage_uScript_AddMessage_177.Out)
		{
			Relay_False_150();
		}
	}

	private void Relay_Save_Out_178()
	{
		Relay_Save_176();
	}

	private void Relay_Load_Out_178()
	{
		Relay_Load_176();
	}

	private void Relay_Restart_Out_178()
	{
		Relay_Set_False_176();
	}

	private void Relay_Save_178()
	{
		logic_SubGraph_SaveLoadInt_integer_178 = local_QuestionThree_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_178 = local_QuestionThree_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_178.Save(logic_SubGraph_SaveLoadInt_restartValue_178, ref logic_SubGraph_SaveLoadInt_integer_178, logic_SubGraph_SaveLoadInt_intAsVariable_178, logic_SubGraph_SaveLoadInt_uniqueID_178);
	}

	private void Relay_Load_178()
	{
		logic_SubGraph_SaveLoadInt_integer_178 = local_QuestionThree_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_178 = local_QuestionThree_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_178.Load(logic_SubGraph_SaveLoadInt_restartValue_178, ref logic_SubGraph_SaveLoadInt_integer_178, logic_SubGraph_SaveLoadInt_intAsVariable_178, logic_SubGraph_SaveLoadInt_uniqueID_178);
	}

	private void Relay_Restart_178()
	{
		logic_SubGraph_SaveLoadInt_integer_178 = local_QuestionThree_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_178 = local_QuestionThree_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_178.Restart(logic_SubGraph_SaveLoadInt_restartValue_178, ref logic_SubGraph_SaveLoadInt_integer_178, logic_SubGraph_SaveLoadInt_intAsVariable_178, logic_SubGraph_SaveLoadInt_uniqueID_178);
	}

	private void Relay_In_180()
	{
		logic_uScript_LockTechInteraction_tech_180 = local_ButtonBase1Tech_Tank;
		int num = 0;
		if (logic_uScript_LockTechInteraction_excludedBlocks_180.Length <= num)
		{
			Array.Resize(ref logic_uScript_LockTechInteraction_excludedBlocks_180, num + 1);
		}
		logic_uScript_LockTechInteraction_excludedBlocks_180[num++] = blockTypeButton1;
		int num2 = 0;
		if (logic_uScript_LockTechInteraction_excludedUniqueBlocks_180.Length <= num2)
		{
			Array.Resize(ref logic_uScript_LockTechInteraction_excludedUniqueBlocks_180, num2 + 1);
		}
		logic_uScript_LockTechInteraction_excludedUniqueBlocks_180[num2++] = local_ButtonBlock1_TankBlock;
		logic_uScript_LockTechInteraction_uScript_LockTechInteraction_180.In(logic_uScript_LockTechInteraction_tech_180, logic_uScript_LockTechInteraction_excludedBlocks_180, logic_uScript_LockTechInteraction_excludedUniqueBlocks_180);
		if (logic_uScript_LockTechInteraction_uScript_LockTechInteraction_180.Out)
		{
			Relay_In_37();
		}
	}

	private void Relay_In_181()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_181.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_181.Out)
		{
			Relay_In_128();
		}
	}

	private void Relay_In_185()
	{
		logic_uScript_SetTankInvulnerable_tank_185 = local_ButtonBase3Tech_Tank;
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_185.In(logic_uScript_SetTankInvulnerable_invulnerable_185, logic_uScript_SetTankInvulnerable_tank_185);
		if (logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_185.Out)
		{
			Relay_In_46();
		}
	}

	private void Relay_Save_Out_186()
	{
		Relay_Save_192();
	}

	private void Relay_Load_Out_186()
	{
		Relay_Load_192();
	}

	private void Relay_Restart_Out_186()
	{
		Relay_Set_False_192();
	}

	private void Relay_Save_186()
	{
		logic_SubGraph_SaveLoadBool_boolean_186 = local_Q1EnemyAlive_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_186 = local_Q1EnemyAlive_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_186.Save(ref logic_SubGraph_SaveLoadBool_boolean_186, logic_SubGraph_SaveLoadBool_boolAsVariable_186, logic_SubGraph_SaveLoadBool_uniqueID_186);
	}

	private void Relay_Load_186()
	{
		logic_SubGraph_SaveLoadBool_boolean_186 = local_Q1EnemyAlive_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_186 = local_Q1EnemyAlive_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_186.Load(ref logic_SubGraph_SaveLoadBool_boolean_186, logic_SubGraph_SaveLoadBool_boolAsVariable_186, logic_SubGraph_SaveLoadBool_uniqueID_186);
	}

	private void Relay_Set_True_186()
	{
		logic_SubGraph_SaveLoadBool_boolean_186 = local_Q1EnemyAlive_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_186 = local_Q1EnemyAlive_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_186.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_186, logic_SubGraph_SaveLoadBool_boolAsVariable_186, logic_SubGraph_SaveLoadBool_uniqueID_186);
	}

	private void Relay_Set_False_186()
	{
		logic_SubGraph_SaveLoadBool_boolean_186 = local_Q1EnemyAlive_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_186 = local_Q1EnemyAlive_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_186.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_186, logic_SubGraph_SaveLoadBool_boolAsVariable_186, logic_SubGraph_SaveLoadBool_uniqueID_186);
	}

	private void Relay_In_188()
	{
		int num = 0;
		if (logic_uScriptAct_Concatenate_A_188.Length <= num)
		{
			Array.Resize(ref logic_uScriptAct_Concatenate_A_188, num + 1);
		}
		logic_uScriptAct_Concatenate_A_188[num++] = local_191_System_String;
		int num2 = 0;
		if (logic_uScriptAct_Concatenate_B_188.Length <= num2)
		{
			Array.Resize(ref logic_uScriptAct_Concatenate_B_188, num2 + 1);
		}
		logic_uScriptAct_Concatenate_B_188[num2++] = local_QuestionsStage_System_Int32;
		logic_uScriptAct_Concatenate_uScriptAct_Concatenate_188.In(logic_uScriptAct_Concatenate_A_188, logic_uScriptAct_Concatenate_B_188, logic_uScriptAct_Concatenate_Separator_188, out logic_uScriptAct_Concatenate_Result_188);
		local_189_System_String = logic_uScriptAct_Concatenate_Result_188;
		if (logic_uScriptAct_Concatenate_uScriptAct_Concatenate_188.Out)
		{
			Relay_ShowLabel_190();
		}
	}

	private void Relay_ShowLabel_190()
	{
		logic_uScriptAct_PrintText_Text_190 = local_189_System_String;
		logic_uScriptAct_PrintText_uScriptAct_PrintText_190.ShowLabel(logic_uScriptAct_PrintText_Text_190, logic_uScriptAct_PrintText_FontSize_190, logic_uScriptAct_PrintText_FontStyle_190, logic_uScriptAct_PrintText_FontColor_190, logic_uScriptAct_PrintText_textAnchor_190, logic_uScriptAct_PrintText_EdgePadding_190, logic_uScriptAct_PrintText_time_190);
	}

	private void Relay_HideLabel_190()
	{
		logic_uScriptAct_PrintText_Text_190 = local_189_System_String;
		logic_uScriptAct_PrintText_uScriptAct_PrintText_190.HideLabel(logic_uScriptAct_PrintText_Text_190, logic_uScriptAct_PrintText_FontSize_190, logic_uScriptAct_PrintText_FontStyle_190, logic_uScriptAct_PrintText_FontColor_190, logic_uScriptAct_PrintText_textAnchor_190, logic_uScriptAct_PrintText_EdgePadding_190, logic_uScriptAct_PrintText_time_190);
	}

	private void Relay_Save_Out_192()
	{
		Relay_Save_194();
	}

	private void Relay_Load_Out_192()
	{
		Relay_Load_194();
	}

	private void Relay_Restart_Out_192()
	{
		Relay_Set_False_194();
	}

	private void Relay_Save_192()
	{
		logic_SubGraph_SaveLoadBool_boolean_192 = local_Q2EnemyAlive_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_192 = local_Q2EnemyAlive_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_192.Save(ref logic_SubGraph_SaveLoadBool_boolean_192, logic_SubGraph_SaveLoadBool_boolAsVariable_192, logic_SubGraph_SaveLoadBool_uniqueID_192);
	}

	private void Relay_Load_192()
	{
		logic_SubGraph_SaveLoadBool_boolean_192 = local_Q2EnemyAlive_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_192 = local_Q2EnemyAlive_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_192.Load(ref logic_SubGraph_SaveLoadBool_boolean_192, logic_SubGraph_SaveLoadBool_boolAsVariable_192, logic_SubGraph_SaveLoadBool_uniqueID_192);
	}

	private void Relay_Set_True_192()
	{
		logic_SubGraph_SaveLoadBool_boolean_192 = local_Q2EnemyAlive_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_192 = local_Q2EnemyAlive_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_192.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_192, logic_SubGraph_SaveLoadBool_boolAsVariable_192, logic_SubGraph_SaveLoadBool_uniqueID_192);
	}

	private void Relay_Set_False_192()
	{
		logic_SubGraph_SaveLoadBool_boolean_192 = local_Q2EnemyAlive_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_192 = local_Q2EnemyAlive_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_192.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_192, logic_SubGraph_SaveLoadBool_boolAsVariable_192, logic_SubGraph_SaveLoadBool_uniqueID_192);
	}

	private void Relay_Save_Out_194()
	{
		Relay_Save_200();
	}

	private void Relay_Load_Out_194()
	{
		Relay_Load_200();
	}

	private void Relay_Restart_Out_194()
	{
		Relay_Set_False_200();
	}

	private void Relay_Save_194()
	{
		logic_SubGraph_SaveLoadBool_boolean_194 = local_Q3EnemyAlive_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_194 = local_Q3EnemyAlive_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_194.Save(ref logic_SubGraph_SaveLoadBool_boolean_194, logic_SubGraph_SaveLoadBool_boolAsVariable_194, logic_SubGraph_SaveLoadBool_uniqueID_194);
	}

	private void Relay_Load_194()
	{
		logic_SubGraph_SaveLoadBool_boolean_194 = local_Q3EnemyAlive_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_194 = local_Q3EnemyAlive_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_194.Load(ref logic_SubGraph_SaveLoadBool_boolean_194, logic_SubGraph_SaveLoadBool_boolAsVariable_194, logic_SubGraph_SaveLoadBool_uniqueID_194);
	}

	private void Relay_Set_True_194()
	{
		logic_SubGraph_SaveLoadBool_boolean_194 = local_Q3EnemyAlive_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_194 = local_Q3EnemyAlive_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_194.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_194, logic_SubGraph_SaveLoadBool_boolAsVariable_194, logic_SubGraph_SaveLoadBool_uniqueID_194);
	}

	private void Relay_Set_False_194()
	{
		logic_SubGraph_SaveLoadBool_boolean_194 = local_Q3EnemyAlive_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_194 = local_Q3EnemyAlive_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_194.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_194, logic_SubGraph_SaveLoadBool_boolAsVariable_194, logic_SubGraph_SaveLoadBool_uniqueID_194);
	}

	private void Relay_True_196()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_196.True(out logic_uScriptAct_SetBool_Target_196);
		local_NearNPC_System_Boolean = logic_uScriptAct_SetBool_Target_196;
	}

	private void Relay_False_196()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_196.False(out logic_uScriptAct_SetBool_Target_196);
		local_NearNPC_System_Boolean = logic_uScriptAct_SetBool_Target_196;
	}

	private void Relay_Save_Out_200()
	{
		Relay_Save_202();
	}

	private void Relay_Load_Out_200()
	{
		Relay_Load_202();
	}

	private void Relay_Restart_Out_200()
	{
		Relay_Set_False_202();
	}

	private void Relay_Save_200()
	{
		logic_SubGraph_SaveLoadBool_boolean_200 = local_Question01WrongAnswers_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_200 = local_Question01WrongAnswers_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_200.Save(ref logic_SubGraph_SaveLoadBool_boolean_200, logic_SubGraph_SaveLoadBool_boolAsVariable_200, logic_SubGraph_SaveLoadBool_uniqueID_200);
	}

	private void Relay_Load_200()
	{
		logic_SubGraph_SaveLoadBool_boolean_200 = local_Question01WrongAnswers_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_200 = local_Question01WrongAnswers_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_200.Load(ref logic_SubGraph_SaveLoadBool_boolean_200, logic_SubGraph_SaveLoadBool_boolAsVariable_200, logic_SubGraph_SaveLoadBool_uniqueID_200);
	}

	private void Relay_Set_True_200()
	{
		logic_SubGraph_SaveLoadBool_boolean_200 = local_Question01WrongAnswers_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_200 = local_Question01WrongAnswers_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_200.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_200, logic_SubGraph_SaveLoadBool_boolAsVariable_200, logic_SubGraph_SaveLoadBool_uniqueID_200);
	}

	private void Relay_Set_False_200()
	{
		logic_SubGraph_SaveLoadBool_boolean_200 = local_Question01WrongAnswers_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_200 = local_Question01WrongAnswers_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_200.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_200, logic_SubGraph_SaveLoadBool_boolAsVariable_200, logic_SubGraph_SaveLoadBool_uniqueID_200);
	}

	private void Relay_Save_Out_202()
	{
		Relay_Save_203();
	}

	private void Relay_Load_Out_202()
	{
		Relay_Load_203();
	}

	private void Relay_Restart_Out_202()
	{
		Relay_Set_False_203();
	}

	private void Relay_Save_202()
	{
		logic_SubGraph_SaveLoadBool_boolean_202 = local_Question02WrongAnswers_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_202 = local_Question02WrongAnswers_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_202.Save(ref logic_SubGraph_SaveLoadBool_boolean_202, logic_SubGraph_SaveLoadBool_boolAsVariable_202, logic_SubGraph_SaveLoadBool_uniqueID_202);
	}

	private void Relay_Load_202()
	{
		logic_SubGraph_SaveLoadBool_boolean_202 = local_Question02WrongAnswers_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_202 = local_Question02WrongAnswers_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_202.Load(ref logic_SubGraph_SaveLoadBool_boolean_202, logic_SubGraph_SaveLoadBool_boolAsVariable_202, logic_SubGraph_SaveLoadBool_uniqueID_202);
	}

	private void Relay_Set_True_202()
	{
		logic_SubGraph_SaveLoadBool_boolean_202 = local_Question02WrongAnswers_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_202 = local_Question02WrongAnswers_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_202.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_202, logic_SubGraph_SaveLoadBool_boolAsVariable_202, logic_SubGraph_SaveLoadBool_uniqueID_202);
	}

	private void Relay_Set_False_202()
	{
		logic_SubGraph_SaveLoadBool_boolean_202 = local_Question02WrongAnswers_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_202 = local_Question02WrongAnswers_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_202.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_202, logic_SubGraph_SaveLoadBool_boolAsVariable_202, logic_SubGraph_SaveLoadBool_uniqueID_202);
	}

	private void Relay_Save_Out_203()
	{
	}

	private void Relay_Load_Out_203()
	{
		Relay_In_206();
	}

	private void Relay_Restart_Out_203()
	{
		Relay_False_196();
	}

	private void Relay_Save_203()
	{
		logic_SubGraph_SaveLoadBool_boolean_203 = local_Question03WrongAnswers_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_203 = local_Question03WrongAnswers_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_203.Save(ref logic_SubGraph_SaveLoadBool_boolean_203, logic_SubGraph_SaveLoadBool_boolAsVariable_203, logic_SubGraph_SaveLoadBool_uniqueID_203);
	}

	private void Relay_Load_203()
	{
		logic_SubGraph_SaveLoadBool_boolean_203 = local_Question03WrongAnswers_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_203 = local_Question03WrongAnswers_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_203.Load(ref logic_SubGraph_SaveLoadBool_boolean_203, logic_SubGraph_SaveLoadBool_boolAsVariable_203, logic_SubGraph_SaveLoadBool_uniqueID_203);
	}

	private void Relay_Set_True_203()
	{
		logic_SubGraph_SaveLoadBool_boolean_203 = local_Question03WrongAnswers_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_203 = local_Question03WrongAnswers_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_203.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_203, logic_SubGraph_SaveLoadBool_boolAsVariable_203, logic_SubGraph_SaveLoadBool_uniqueID_203);
	}

	private void Relay_Set_False_203()
	{
		logic_SubGraph_SaveLoadBool_boolean_203 = local_Question03WrongAnswers_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_203 = local_Question03WrongAnswers_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_203.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_203, logic_SubGraph_SaveLoadBool_boolAsVariable_203, logic_SubGraph_SaveLoadBool_uniqueID_203);
	}

	private void Relay_Out_206()
	{
	}

	private void Relay_In_206()
	{
		logic_SubGraph_LoadObjectiveStates_currentObjective_206 = local_Stage_System_Int32;
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_206.In(logic_SubGraph_LoadObjectiveStates_currentObjective_206);
	}

	private void Relay_True_208()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_208.True(out logic_uScriptAct_SetBool_Target_208);
		local_msgIntro_System_Boolean = logic_uScriptAct_SetBool_Target_208;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_208.Out)
		{
			Relay_In_144();
		}
	}

	private void Relay_False_208()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_208.False(out logic_uScriptAct_SetBool_Target_208);
		local_msgIntro_System_Boolean = logic_uScriptAct_SetBool_Target_208;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_208.Out)
		{
			Relay_In_144();
		}
	}

	private void Relay_In_209()
	{
		logic_uScript_IsPlayerInRangeOfTech_tech_209 = local_NPCTech_Tank;
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_209.In(logic_uScript_IsPlayerInRangeOfTech_tech_209, logic_uScript_IsPlayerInRangeOfTech_range_209, logic_uScript_IsPlayerInRangeOfTech_techs_209);
		if (logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_209.InRange)
		{
			Relay_In_108();
		}
	}

	private void Relay_Save_Out_212()
	{
		Relay_Save_36();
	}

	private void Relay_Load_Out_212()
	{
		Relay_Load_36();
	}

	private void Relay_Restart_Out_212()
	{
		Relay_Set_False_36();
	}

	private void Relay_Save_212()
	{
		logic_SubGraph_SaveLoadBool_boolean_212 = local_msgBaseFoundShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_212 = local_msgBaseFoundShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_212.Save(ref logic_SubGraph_SaveLoadBool_boolean_212, logic_SubGraph_SaveLoadBool_boolAsVariable_212, logic_SubGraph_SaveLoadBool_uniqueID_212);
	}

	private void Relay_Load_212()
	{
		logic_SubGraph_SaveLoadBool_boolean_212 = local_msgBaseFoundShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_212 = local_msgBaseFoundShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_212.Load(ref logic_SubGraph_SaveLoadBool_boolean_212, logic_SubGraph_SaveLoadBool_boolAsVariable_212, logic_SubGraph_SaveLoadBool_uniqueID_212);
	}

	private void Relay_Set_True_212()
	{
		logic_SubGraph_SaveLoadBool_boolean_212 = local_msgBaseFoundShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_212 = local_msgBaseFoundShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_212.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_212, logic_SubGraph_SaveLoadBool_boolAsVariable_212, logic_SubGraph_SaveLoadBool_uniqueID_212);
	}

	private void Relay_Set_False_212()
	{
		logic_SubGraph_SaveLoadBool_boolean_212 = local_msgBaseFoundShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_212 = local_msgBaseFoundShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_212.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_212, logic_SubGraph_SaveLoadBool_boolAsVariable_212, logic_SubGraph_SaveLoadBool_uniqueID_212);
	}

	private void Relay_In_214()
	{
		logic_uScriptCon_CompareBool_Bool_214 = local_msgBaseFoundShown_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_214.In(logic_uScriptCon_CompareBool_Bool_214);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_214.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_214.False;
		if (num)
		{
			Relay_In_220();
		}
		if (flag)
		{
			Relay_In_221();
		}
	}

	private void Relay_True_218()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_218.True(out logic_uScriptAct_SetBool_Target_218);
		local_msgBaseFoundShown_System_Boolean = logic_uScriptAct_SetBool_Target_218;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_218.Out)
		{
			Relay_In_220();
		}
	}

	private void Relay_False_218()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_218.False(out logic_uScriptAct_SetBool_Target_218);
		local_msgBaseFoundShown_System_Boolean = logic_uScriptAct_SetBool_Target_218;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_218.Out)
		{
			Relay_In_220();
		}
	}

	private void Relay_Out_220()
	{
	}

	private void Relay_In_220()
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_220 = local_Stage_System_Int32;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_220.In(ref logic_SubGraph_CompleteObjectiveStage_objectiveStage_220, logic_SubGraph_CompleteObjectiveStage_isFinalObjective_220);
	}

	private void Relay_In_221()
	{
		logic_uScript_AddMessage_messageData_221 = msg02BaseFound;
		logic_uScript_AddMessage_speaker_221 = messageSpeaker;
		logic_uScript_AddMessage_Return_221 = logic_uScript_AddMessage_uScript_AddMessage_221.In(logic_uScript_AddMessage_messageData_221, logic_uScript_AddMessage_speaker_221);
		if (logic_uScript_AddMessage_uScript_AddMessage_221.Shown)
		{
			Relay_True_218();
		}
	}

	private void Relay_Output1_222()
	{
		Relay_In_214();
	}

	private void Relay_Output2_222()
	{
		Relay_In_245();
	}

	private void Relay_Output3_222()
	{
	}

	private void Relay_Output4_222()
	{
	}

	private void Relay_Output5_222()
	{
	}

	private void Relay_Output6_222()
	{
	}

	private void Relay_Output7_222()
	{
	}

	private void Relay_Output8_222()
	{
	}

	private void Relay_In_222()
	{
		logic_uScriptCon_ManualSwitch_CurrentOutput_222 = local_Stage_System_Int32;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_222.In(logic_uScriptCon_ManualSwitch_CurrentOutput_222);
	}

	private void Relay_In_231()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_231.In(logic_uScriptAct_SetInt_Value_231, out logic_uScriptAct_SetInt_Target_231);
		local_QuestionOne_System_Int32 = logic_uScriptAct_SetInt_Target_231;
	}

	private void Relay_True_232()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_232.True(out logic_uScriptAct_SetBool_Target_232);
		local_Question02WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_232;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_232.Out)
		{
			Relay_False_438();
		}
	}

	private void Relay_False_232()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_232.False(out logic_uScriptAct_SetBool_Target_232);
		local_Question02WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_232;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_232.Out)
		{
			Relay_False_438();
		}
	}

	private void Relay_In_234()
	{
		logic_uScriptAct_AddInt_v2_A_234 = local_QuestionTwo_System_Int32;
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_234.In(logic_uScriptAct_AddInt_v2_A_234, logic_uScriptAct_AddInt_v2_B_234, out logic_uScriptAct_AddInt_v2_IntResult_234, out logic_uScriptAct_AddInt_v2_FloatResult_234);
		local_QuestionTwo_System_Int32 = logic_uScriptAct_AddInt_v2_IntResult_234;
	}

	private void Relay_In_235()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_235.In(logic_uScriptAct_SetInt_Value_235, out logic_uScriptAct_SetInt_Target_235);
		local_QuestionTwo_System_Int32 = logic_uScriptAct_SetInt_Target_235;
	}

	private void Relay_In_238()
	{
		logic_uScript_AddMessage_messageData_238 = msg06QuestionTwoCorrect;
		logic_uScript_AddMessage_speaker_238 = messageSpeaker;
		logic_uScript_AddMessage_Return_238 = logic_uScript_AddMessage_uScript_AddMessage_238.In(logic_uScript_AddMessage_messageData_238, logic_uScript_AddMessage_speaker_238);
		if (logic_uScript_AddMessage_uScript_AddMessage_238.Out)
		{
			Relay_In_368();
		}
	}

	private void Relay_In_239()
	{
		logic_uScriptCon_CompareInt_A_239 = local_Button1Value_System_Int32;
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_239.In(logic_uScriptCon_CompareInt_A_239, logic_uScriptCon_CompareInt_B_239);
		bool greaterThan = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_239.GreaterThan;
		bool lessThanOrEqualTo = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_239.LessThanOrEqualTo;
		if (greaterThan)
		{
			Relay_In_467();
		}
		if (lessThanOrEqualTo)
		{
			Relay_In_433();
		}
	}

	private void Relay_In_241()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_241.In(logic_uScriptAct_SetInt_Value_241, out logic_uScriptAct_SetInt_Target_241);
		local_QuestionTwo_System_Int32 = logic_uScriptAct_SetInt_Target_241;
	}

	private void Relay_In_242()
	{
		logic_uScript_Wait_seconds_242 = WaitingTime;
		logic_uScript_Wait_uScript_Wait_242.In(logic_uScript_Wait_seconds_242, logic_uScript_Wait_repeat_242);
		if (logic_uScript_Wait_uScript_Wait_242.Waited)
		{
			Relay_False_364();
		}
	}

	private void Relay_In_243()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_243.In(logic_uScriptAct_SetInt_Value_243, out logic_uScriptAct_SetInt_Target_243);
		local_QuestionOne_System_Int32 = logic_uScriptAct_SetInt_Target_243;
	}

	private void Relay_Output1_245()
	{
		Relay_In_280();
	}

	private void Relay_Output2_245()
	{
		Relay_In_422();
	}

	private void Relay_Output3_245()
	{
		Relay_In_515();
	}

	private void Relay_Output4_245()
	{
	}

	private void Relay_Output5_245()
	{
	}

	private void Relay_Output6_245()
	{
	}

	private void Relay_Output7_245()
	{
	}

	private void Relay_Output8_245()
	{
	}

	private void Relay_In_245()
	{
		logic_uScriptCon_ManualSwitch_CurrentOutput_245 = local_QuestionsStage_System_Int32;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_245.In(logic_uScriptCon_ManualSwitch_CurrentOutput_245);
	}

	private void Relay_In_249()
	{
		logic_uScript_AddMessage_messageData_249 = msgMinionDefeated;
		logic_uScript_AddMessage_speaker_249 = messageSpeaker;
		logic_uScript_AddMessage_Return_249 = logic_uScript_AddMessage_uScript_AddMessage_249.In(logic_uScript_AddMessage_messageData_249, logic_uScript_AddMessage_speaker_249);
		if (logic_uScript_AddMessage_uScript_AddMessage_249.Out)
		{
			Relay_In_450();
		}
	}

	private void Relay_In_254()
	{
		logic_uScriptCon_CompareBool_Bool_254 = Q1Button2;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_254.In(logic_uScriptCon_CompareBool_Bool_254);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_254.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_254.False;
		if (num)
		{
			Relay_False_471();
		}
		if (flag)
		{
			Relay_True_355();
		}
	}

	private void Relay_True_255()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_255.True(out logic_uScriptAct_SetBool_Target_255);
		local_CanPushButtons_System_Boolean = logic_uScriptAct_SetBool_Target_255;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_255.Out)
		{
			Relay_In_357();
		}
	}

	private void Relay_False_255()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_255.False(out logic_uScriptAct_SetBool_Target_255);
		local_CanPushButtons_System_Boolean = logic_uScriptAct_SetBool_Target_255;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_255.Out)
		{
			Relay_In_357();
		}
	}

	private void Relay_True_256()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_256.True(out logic_uScriptAct_SetBool_Target_256);
		local_CanPushButtons_System_Boolean = logic_uScriptAct_SetBool_Target_256;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_256.Out)
		{
			Relay_In_231();
		}
	}

	private void Relay_False_256()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_256.False(out logic_uScriptAct_SetBool_Target_256);
		local_CanPushButtons_System_Boolean = logic_uScriptAct_SetBool_Target_256;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_256.Out)
		{
			Relay_In_231();
		}
	}

	private void Relay_In_257()
	{
		logic_uScriptCon_CompareBool_Bool_257 = Q1Button3;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_257.In(logic_uScriptCon_CompareBool_Bool_257);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_257.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_257.False;
		if (num)
		{
			Relay_False_431();
		}
		if (flag)
		{
			Relay_True_356();
		}
	}

	private void Relay_In_258()
	{
		logic_uScriptAct_AddInt_v2_A_258 = local_QuestionTwo_System_Int32;
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_258.In(logic_uScriptAct_AddInt_v2_A_258, logic_uScriptAct_AddInt_v2_B_258, out logic_uScriptAct_AddInt_v2_IntResult_258, out logic_uScriptAct_AddInt_v2_FloatResult_258);
		local_QuestionTwo_System_Int32 = logic_uScriptAct_AddInt_v2_IntResult_258;
	}

	private void Relay_True_261()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_261.True(out logic_uScriptAct_SetBool_Target_261);
		local_CanPushButtons_System_Boolean = logic_uScriptAct_SetBool_Target_261;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_261.Out)
		{
			Relay_In_651();
		}
	}

	private void Relay_False_261()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_261.False(out logic_uScriptAct_SetBool_Target_261);
		local_CanPushButtons_System_Boolean = logic_uScriptAct_SetBool_Target_261;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_261.Out)
		{
			Relay_In_651();
		}
	}

	private void Relay_True_265()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_265.True(out logic_uScriptAct_SetBool_Target_265);
		local_CanPushButtons_System_Boolean = logic_uScriptAct_SetBool_Target_265;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_265.Out)
		{
			Relay_In_410();
		}
	}

	private void Relay_False_265()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_265.False(out logic_uScriptAct_SetBool_Target_265);
		local_CanPushButtons_System_Boolean = logic_uScriptAct_SetBool_Target_265;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_265.Out)
		{
			Relay_In_410();
		}
	}

	private void Relay_In_267()
	{
		logic_uScriptCon_CompareInt_A_267 = local_Button2Value_System_Int32;
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_267.In(logic_uScriptCon_CompareInt_A_267, logic_uScriptCon_CompareInt_B_267);
		bool greaterThan = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_267.GreaterThan;
		bool lessThanOrEqualTo = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_267.LessThanOrEqualTo;
		if (greaterThan)
		{
			Relay_In_454();
		}
		if (lessThanOrEqualTo)
		{
			Relay_In_434();
		}
	}

	private void Relay_In_271()
	{
		logic_uScriptCon_CompareInt_A_271 = local_Button2Value_System_Int32;
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_271.In(logic_uScriptCon_CompareInt_A_271, logic_uScriptCon_CompareInt_B_271);
		bool greaterThan = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_271.GreaterThan;
		bool lessThanOrEqualTo = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_271.LessThanOrEqualTo;
		if (greaterThan)
		{
			Relay_In_254();
		}
		if (lessThanOrEqualTo)
		{
			Relay_In_302();
		}
	}

	private void Relay_In_275()
	{
		logic_uScript_AddMessage_messageData_275 = msg05QuestionTwo;
		logic_uScript_AddMessage_speaker_275 = messageSpeaker;
		logic_uScript_AddMessage_Return_275 = logic_uScript_AddMessage_uScript_AddMessage_275.In(logic_uScript_AddMessage_messageData_275, logic_uScript_AddMessage_speaker_275);
		if (logic_uScript_AddMessage_uScript_AddMessage_275.Out)
		{
			Relay_In_279();
		}
	}

	private void Relay_In_276()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_276.In(logic_uScriptAct_SetInt_Value_276, out logic_uScriptAct_SetInt_Target_276);
		local_QuestionsStage_System_Int32 = logic_uScriptAct_SetInt_Target_276;
		if (logic_uScriptAct_SetInt_uScriptAct_SetInt_276.Out)
		{
			Relay_In_288();
		}
	}

	private void Relay_In_277()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_277.In(logic_uScriptAct_SetInt_Value_277, out logic_uScriptAct_SetInt_Target_277);
		local_QuestionOne_System_Int32 = logic_uScriptAct_SetInt_Target_277;
	}

	private void Relay_In_279()
	{
		logic_uScript_GetCircuitChargeInfo_block_279 = local_ButtonBlock1_TankBlock;
		logic_uScript_GetCircuitChargeInfo_Return_279 = logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_279.In(logic_uScript_GetCircuitChargeInfo_block_279, logic_uScript_GetCircuitChargeInfo_tech_279, logic_uScript_GetCircuitChargeInfo_blockType_279);
		local_Button1Value_System_Int32 = logic_uScript_GetCircuitChargeInfo_Return_279;
		if (logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_279.Out)
		{
			Relay_In_316();
		}
	}

	private void Relay_Output1_280()
	{
		Relay_False_474();
	}

	private void Relay_Output2_280()
	{
		Relay_In_449();
	}

	private void Relay_Output3_280()
	{
		Relay_In_437();
	}

	private void Relay_Output4_280()
	{
		Relay_In_427();
	}

	private void Relay_Output5_280()
	{
		Relay_In_436();
	}

	private void Relay_Output6_280()
	{
	}

	private void Relay_Output7_280()
	{
	}

	private void Relay_Output8_280()
	{
	}

	private void Relay_In_280()
	{
		logic_uScriptCon_ManualSwitch_CurrentOutput_280 = local_QuestionOne_System_Int32;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_280.In(logic_uScriptCon_ManualSwitch_CurrentOutput_280);
	}

	private void Relay_True_284()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_284.True(out logic_uScriptAct_SetBool_Target_284);
		local_Q2EnemyAlive_System_Boolean = logic_uScriptAct_SetBool_Target_284;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_284.Out)
		{
			Relay_False_324();
		}
	}

	private void Relay_False_284()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_284.False(out logic_uScriptAct_SetBool_Target_284);
		local_Q2EnemyAlive_System_Boolean = logic_uScriptAct_SetBool_Target_284;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_284.Out)
		{
			Relay_False_324();
		}
	}

	private void Relay_In_288()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_288.In(logic_uScriptAct_SetInt_Value_288, out logic_uScriptAct_SetInt_Target_288);
		local_QuestionTwo_System_Int32 = logic_uScriptAct_SetInt_Target_288;
	}

	private void Relay_In_289()
	{
		logic_uScriptCon_CompareBool_Bool_289 = local_Q1EnemyAlive_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_289.In(logic_uScriptCon_CompareBool_Bool_289);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_289.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_289.False;
		if (num)
		{
			Relay_In_472();
		}
		if (flag)
		{
			Relay_InitialSpawn_439();
		}
	}

	private void Relay_True_292()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_292.True(out logic_uScriptAct_SetBool_Target_292);
		local_CanPushButtons_System_Boolean = logic_uScriptAct_SetBool_Target_292;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_292.Out)
		{
			Relay_In_352();
		}
	}

	private void Relay_False_292()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_292.False(out logic_uScriptAct_SetBool_Target_292);
		local_CanPushButtons_System_Boolean = logic_uScriptAct_SetBool_Target_292;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_292.Out)
		{
			Relay_In_352();
		}
	}

	private void Relay_In_294()
	{
		logic_uScript_AddMessage_messageData_294 = msg07QuestionTwoWrong;
		logic_uScript_AddMessage_speaker_294 = messageSpeaker;
		logic_uScript_AddMessage_Return_294 = logic_uScript_AddMessage_uScript_AddMessage_294.In(logic_uScript_AddMessage_messageData_294, logic_uScript_AddMessage_speaker_294);
		if (logic_uScript_AddMessage_uScript_AddMessage_294.Out)
		{
			Relay_In_396();
		}
	}

	private void Relay_True_295()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_295.True(out logic_uScriptAct_SetBool_Target_295);
		local_Question02WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_295;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_295.Out)
		{
			Relay_False_383();
		}
	}

	private void Relay_False_295()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_295.False(out logic_uScriptAct_SetBool_Target_295);
		local_Question02WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_295;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_295.Out)
		{
			Relay_False_383();
		}
	}

	private void Relay_In_296()
	{
		logic_uScript_AddMessage_messageData_296 = msg04QuestionOneWrong;
		logic_uScript_AddMessage_speaker_296 = messageSpeaker;
		logic_uScript_AddMessage_Return_296 = logic_uScript_AddMessage_uScript_AddMessage_296.In(logic_uScript_AddMessage_messageData_296, logic_uScript_AddMessage_speaker_296);
		if (logic_uScript_AddMessage_uScript_AddMessage_296.Out)
		{
			Relay_In_242();
		}
	}

	private void Relay_In_297()
	{
		logic_uScript_AddMessage_messageData_297 = msgSpawnMinion;
		logic_uScript_AddMessage_speaker_297 = messageSpeaker;
		logic_uScript_AddMessage_Return_297 = logic_uScript_AddMessage_uScript_AddMessage_297.In(logic_uScript_AddMessage_messageData_297, logic_uScript_AddMessage_speaker_297);
		if (logic_uScript_AddMessage_uScript_AddMessage_297.Out)
		{
			Relay_In_430();
		}
	}

	private void Relay_In_299()
	{
		logic_uScriptCon_CompareInt_A_299 = local_Button4Value_System_Int32;
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_299.In(logic_uScriptCon_CompareInt_A_299, logic_uScriptCon_CompareInt_B_299);
		if (logic_uScriptCon_CompareInt_uScriptCon_CompareInt_299.GreaterThan)
		{
			Relay_In_315();
		}
	}

	private void Relay_In_302()
	{
		logic_uScript_GetCircuitChargeInfo_block_302 = local_ButtonBlock3_TankBlock;
		logic_uScript_GetCircuitChargeInfo_Return_302 = logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_302.In(logic_uScript_GetCircuitChargeInfo_block_302, logic_uScript_GetCircuitChargeInfo_tech_302, logic_uScript_GetCircuitChargeInfo_blockType_302);
		local_Button3Value_System_Int32 = logic_uScript_GetCircuitChargeInfo_Return_302;
		if (logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_302.Out)
		{
			Relay_In_469();
		}
	}

	private void Relay_In_305()
	{
		logic_uScript_GetCircuitChargeInfo_block_305 = local_ButtonBlock4_TankBlock;
		logic_uScript_GetCircuitChargeInfo_Return_305 = logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_305.In(logic_uScript_GetCircuitChargeInfo_block_305, logic_uScript_GetCircuitChargeInfo_tech_305, logic_uScript_GetCircuitChargeInfo_blockType_305);
		local_Button4Value_System_Int32 = logic_uScript_GetCircuitChargeInfo_Return_305;
		if (logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_305.Out)
		{
			Relay_In_418();
		}
	}

	private void Relay_True_307()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_307.True(out logic_uScriptAct_SetBool_Target_307);
		local_CanPushButtons_System_Boolean = logic_uScriptAct_SetBool_Target_307;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_307.Out)
		{
			Relay_In_461();
		}
	}

	private void Relay_False_307()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_307.False(out logic_uScriptAct_SetBool_Target_307);
		local_CanPushButtons_System_Boolean = logic_uScriptAct_SetBool_Target_307;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_307.Out)
		{
			Relay_In_461();
		}
	}

	private void Relay_In_308()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_308.In(logic_uScriptAct_SetInt_Value_308, out logic_uScriptAct_SetInt_Target_308);
		local_QuestionOne_System_Int32 = logic_uScriptAct_SetInt_Target_308;
	}

	private void Relay_In_310()
	{
		logic_uScriptCon_CompareBool_Bool_310 = Q1Button4;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_310.In(logic_uScriptCon_CompareBool_Bool_310);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_310.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_310.False;
		if (num)
		{
			Relay_False_320();
		}
		if (flag)
		{
			Relay_True_400();
		}
	}

	private void Relay_In_311()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_311.In(logic_uScriptAct_SetInt_Value_311, out logic_uScriptAct_SetInt_Target_311);
		local_QuestionOne_System_Int32 = logic_uScriptAct_SetInt_Target_311;
	}

	private void Relay_True_314()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_314.True(out logic_uScriptAct_SetBool_Target_314);
		local_Question01WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_314;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_314.Out)
		{
			Relay_False_479();
		}
	}

	private void Relay_False_314()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_314.False(out logic_uScriptAct_SetBool_Target_314);
		local_Question01WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_314;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_314.Out)
		{
			Relay_False_479();
		}
	}

	private void Relay_In_315()
	{
		logic_uScriptCon_CompareBool_Bool_315 = Q2Button4;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_315.In(logic_uScriptCon_CompareBool_Bool_315);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_315.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_315.False;
		if (num)
		{
			Relay_False_295();
		}
		if (flag)
		{
			Relay_True_344();
		}
	}

	private void Relay_In_316()
	{
		logic_uScriptCon_CompareInt_A_316 = local_Button1Value_System_Int32;
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_316.In(logic_uScriptCon_CompareInt_A_316, logic_uScriptCon_CompareInt_B_316);
		bool greaterThan = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_316.GreaterThan;
		bool lessThanOrEqualTo = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_316.LessThanOrEqualTo;
		if (greaterThan)
		{
			Relay_In_375();
		}
		if (lessThanOrEqualTo)
		{
			Relay_In_456();
		}
	}

	private void Relay_True_317()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_317.True(out logic_uScriptAct_SetBool_Target_317);
		local_Question01WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_317;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_317.Out)
		{
			Relay_True_307();
		}
	}

	private void Relay_False_317()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_317.False(out logic_uScriptAct_SetBool_Target_317);
		local_Question01WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_317;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_317.Out)
		{
			Relay_True_307();
		}
	}

	private void Relay_True_320()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_320.True(out logic_uScriptAct_SetBool_Target_320);
		local_Question01WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_320;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_320.Out)
		{
			Relay_False_647();
		}
	}

	private void Relay_False_320()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_320.False(out logic_uScriptAct_SetBool_Target_320);
		local_Question01WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_320;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_320.Out)
		{
			Relay_False_647();
		}
	}

	private void Relay_In_321()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_321.In(logic_uScriptAct_SetInt_Value_321, out logic_uScriptAct_SetInt_Target_321);
		local_QuestionTwo_System_Int32 = logic_uScriptAct_SetInt_Target_321;
	}

	private void Relay_In_323()
	{
		logic_uScript_GetCircuitChargeInfo_block_323 = local_ButtonBlock1_TankBlock;
		logic_uScript_GetCircuitChargeInfo_Return_323 = logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_323.In(logic_uScript_GetCircuitChargeInfo_block_323, logic_uScript_GetCircuitChargeInfo_tech_323, logic_uScript_GetCircuitChargeInfo_blockType_323);
		local_Button1Value_System_Int32 = logic_uScript_GetCircuitChargeInfo_Return_323;
		if (logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_323.Out)
		{
			Relay_In_239();
		}
	}

	private void Relay_True_324()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_324.True(out logic_uScriptAct_SetBool_Target_324);
		local_Question02WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_324;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_324.Out)
		{
			Relay_True_475();
		}
	}

	private void Relay_False_324()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_324.False(out logic_uScriptAct_SetBool_Target_324);
		local_Question02WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_324;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_324.Out)
		{
			Relay_True_475();
		}
	}

	private void Relay_True_327()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_327.True(out logic_uScriptAct_SetBool_Target_327);
		local_CanPushButtons_System_Boolean = logic_uScriptAct_SetBool_Target_327;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_327.Out)
		{
			Relay_In_321();
		}
	}

	private void Relay_False_327()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_327.False(out logic_uScriptAct_SetBool_Target_327);
		local_CanPushButtons_System_Boolean = logic_uScriptAct_SetBool_Target_327;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_327.Out)
		{
			Relay_In_321();
		}
	}

	private void Relay_In_330()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_330.In(logic_uScriptAct_SetInt_Value_330, out logic_uScriptAct_SetInt_Target_330);
		local_QuestionThree_System_Int32 = logic_uScriptAct_SetInt_Target_330;
	}

	private void Relay_In_334()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_334.In(logic_uScriptAct_SetInt_Value_334, out logic_uScriptAct_SetInt_Target_334);
		local_QuestionsStage_System_Int32 = logic_uScriptAct_SetInt_Target_334;
		if (logic_uScriptAct_SetInt_uScriptAct_SetInt_334.Out)
		{
			Relay_In_412();
		}
	}

	private void Relay_True_344()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_344.True(out logic_uScriptAct_SetBool_Target_344);
		local_Question02WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_344;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_344.Out)
		{
			Relay_False_425();
		}
	}

	private void Relay_False_344()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_344.False(out logic_uScriptAct_SetBool_Target_344);
		local_Question02WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_344;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_344.Out)
		{
			Relay_False_425();
		}
	}

	private void Relay_True_345()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_345.True(out logic_uScriptAct_SetBool_Target_345);
		local_Question02WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_345;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_345.Out)
		{
			Relay_False_429();
		}
	}

	private void Relay_False_345()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_345.False(out logic_uScriptAct_SetBool_Target_345);
		local_Question02WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_345;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_345.Out)
		{
			Relay_False_429();
		}
	}

	private void Relay_True_347()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_347.True(out logic_uScriptAct_SetBool_Target_347);
		local_Question02WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_347;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_347.Out)
		{
			Relay_False_416();
		}
	}

	private void Relay_False_347()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_347.False(out logic_uScriptAct_SetBool_Target_347);
		local_Question02WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_347;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_347.Out)
		{
			Relay_False_416();
		}
	}

	private void Relay_In_351()
	{
		logic_uScript_GetCircuitChargeInfo_block_351 = local_ButtonBlock4_TankBlock;
		logic_uScript_GetCircuitChargeInfo_Return_351 = logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_351.In(logic_uScript_GetCircuitChargeInfo_block_351, logic_uScript_GetCircuitChargeInfo_tech_351, logic_uScript_GetCircuitChargeInfo_blockType_351);
		local_Button4Value_System_Int32 = logic_uScript_GetCircuitChargeInfo_Return_351;
		if (logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_351.Out)
		{
			Relay_In_299();
		}
	}

	private void Relay_In_352()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_352.In(logic_uScriptAct_SetInt_Value_352, out logic_uScriptAct_SetInt_Target_352);
		local_QuestionTwo_System_Int32 = logic_uScriptAct_SetInt_Target_352;
	}

	private void Relay_InitialSpawn_353()
	{
		int num = 0;
		Array enemyTechData = EnemyTechData;
		if (logic_uScript_SpawnTechsFromData_spawnData_353.Length != num + enemyTechData.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_353, num + enemyTechData.Length);
		}
		Array.Copy(enemyTechData, 0, logic_uScript_SpawnTechsFromData_spawnData_353, num, enemyTechData.Length);
		num += enemyTechData.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_353 = owner_Connection_253;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_353.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_353, logic_uScript_SpawnTechsFromData_ownerNode_353, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_353, logic_uScript_SpawnTechsFromData_allowResurrection_353);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_353.Out)
		{
			Relay_True_359();
		}
	}

	private void Relay_True_355()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_355.True(out logic_uScriptAct_SetBool_Target_355);
		local_Question01WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_355;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_355.Out)
		{
			Relay_False_255();
		}
	}

	private void Relay_False_355()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_355.False(out logic_uScriptAct_SetBool_Target_355);
		local_Question01WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_355;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_355.Out)
		{
			Relay_False_255();
		}
	}

	private void Relay_True_356()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_356.True(out logic_uScriptAct_SetBool_Target_356);
		local_Question01WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_356;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_356.Out)
		{
			Relay_False_407();
		}
	}

	private void Relay_False_356()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_356.False(out logic_uScriptAct_SetBool_Target_356);
		local_Question01WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_356;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_356.Out)
		{
			Relay_False_407();
		}
	}

	private void Relay_In_357()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_357.In(logic_uScriptAct_SetInt_Value_357, out logic_uScriptAct_SetInt_Target_357);
		local_QuestionOne_System_Int32 = logic_uScriptAct_SetInt_Target_357;
	}

	private void Relay_True_359()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_359.True(out logic_uScriptAct_SetBool_Target_359);
		local_Q2EnemyAlive_System_Boolean = logic_uScriptAct_SetBool_Target_359;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_359.Out)
		{
			Relay_In_430();
		}
	}

	private void Relay_False_359()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_359.False(out logic_uScriptAct_SetBool_Target_359);
		local_Q2EnemyAlive_System_Boolean = logic_uScriptAct_SetBool_Target_359;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_359.Out)
		{
			Relay_In_430();
		}
	}

	private void Relay_True_360()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_360.True(out logic_uScriptAct_SetBool_Target_360);
		local_CanPushButtons_System_Boolean = logic_uScriptAct_SetBool_Target_360;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_360.Out)
		{
			Relay_In_311();
		}
	}

	private void Relay_False_360()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_360.False(out logic_uScriptAct_SetBool_Target_360);
		local_CanPushButtons_System_Boolean = logic_uScriptAct_SetBool_Target_360;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_360.Out)
		{
			Relay_In_311();
		}
	}

	private void Relay_True_364()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_364.True(out logic_uScriptAct_SetBool_Target_364);
		local_Question01WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_364;
	}

	private void Relay_False_364()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_364.False(out logic_uScriptAct_SetBool_Target_364);
		local_Question01WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_364;
	}

	private void Relay_In_365()
	{
		logic_uScriptCon_CompareBool_Bool_365 = local_Q2EnemyAlive_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_365.In(logic_uScriptCon_CompareBool_Bool_365);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_365.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_365.False;
		if (num)
		{
			Relay_In_297();
		}
		if (flag)
		{
			Relay_InitialSpawn_353();
		}
	}

	private void Relay_In_368()
	{
		logic_uScript_Wait_seconds_368 = WaitingTime;
		logic_uScript_Wait_uScript_Wait_368.In(logic_uScript_Wait_seconds_368, logic_uScript_Wait_repeat_368);
		if (logic_uScript_Wait_uScript_Wait_368.Waited)
		{
			Relay_In_458();
		}
	}

	private void Relay_In_369()
	{
		logic_uScriptCon_CompareInt_A_369 = local_Button3Value_System_Int32;
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_369.In(logic_uScriptCon_CompareInt_A_369, logic_uScriptCon_CompareInt_B_369);
		bool greaterThan = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_369.GreaterThan;
		bool lessThanOrEqualTo = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_369.LessThanOrEqualTo;
		if (greaterThan)
		{
			Relay_In_385();
		}
		if (lessThanOrEqualTo)
		{
			Relay_In_351();
		}
	}

	private void Relay_In_370()
	{
		int num = 0;
		Array enemyTechData = EnemyTechData;
		if (logic_uScript_GetAndCheckTechs_techData_370.Length != num + enemyTechData.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_370, num + enemyTechData.Length);
		}
		Array.Copy(enemyTechData, 0, logic_uScript_GetAndCheckTechs_techData_370, num, enemyTechData.Length);
		num += enemyTechData.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_370 = owner_Connection_421;
		int num2 = 0;
		Array array = local_Enemy_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_370.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_370, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techs_370, num2, array.Length);
		num2 += array.Length;
		logic_uScript_GetAndCheckTechs_Return_370 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_370.In(logic_uScript_GetAndCheckTechs_techData_370, logic_uScript_GetAndCheckTechs_ownerNode_370, ref logic_uScript_GetAndCheckTechs_techs_370);
		local_Enemy_TankArray = logic_uScript_GetAndCheckTechs_techs_370;
		if (logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_370.AllDead)
		{
			Relay_In_417();
		}
	}

	private void Relay_In_375()
	{
		logic_uScriptCon_CompareBool_Bool_375 = Q2Button1;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_375.In(logic_uScriptCon_CompareBool_Bool_375);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_375.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_375.False;
		if (num)
		{
			Relay_False_382();
		}
		if (flag)
		{
			Relay_True_347();
		}
	}

	private void Relay_In_379()
	{
		logic_uScript_Wait_seconds_379 = WaitingTime;
		logic_uScript_Wait_uScript_Wait_379.In(logic_uScript_Wait_seconds_379, logic_uScript_Wait_repeat_379);
		if (logic_uScript_Wait_uScript_Wait_379.Waited)
		{
			Relay_In_389();
		}
	}

	private void Relay_True_381()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_381.True(out logic_uScriptAct_SetBool_Target_381);
		local_Question02WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_381;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_381.Out)
		{
			Relay_False_292();
		}
	}

	private void Relay_False_381()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_381.False(out logic_uScriptAct_SetBool_Target_381);
		local_Question02WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_381;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_381.Out)
		{
			Relay_False_292();
		}
	}

	private void Relay_True_382()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_382.True(out logic_uScriptAct_SetBool_Target_382);
		local_Question02WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_382;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_382.Out)
		{
			Relay_False_265();
		}
	}

	private void Relay_False_382()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_382.False(out logic_uScriptAct_SetBool_Target_382);
		local_Question02WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_382;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_382.Out)
		{
			Relay_False_265();
		}
	}

	private void Relay_True_383()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_383.True(out logic_uScriptAct_SetBool_Target_383);
		local_CanPushButtons_System_Boolean = logic_uScriptAct_SetBool_Target_383;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_383.Out)
		{
			Relay_In_411();
		}
	}

	private void Relay_False_383()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_383.False(out logic_uScriptAct_SetBool_Target_383);
		local_CanPushButtons_System_Boolean = logic_uScriptAct_SetBool_Target_383;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_383.Out)
		{
			Relay_In_411();
		}
	}

	private void Relay_In_385()
	{
		logic_uScriptCon_CompareBool_Bool_385 = Q2Button3;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_385.In(logic_uScriptCon_CompareBool_Bool_385);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_385.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_385.False;
		if (num)
		{
			Relay_False_232();
		}
		if (flag)
		{
			Relay_True_381();
		}
	}

	private void Relay_True_388()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_388.True(out logic_uScriptAct_SetBool_Target_388);
		local_Question02WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_388;
	}

	private void Relay_False_388()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_388.False(out logic_uScriptAct_SetBool_Target_388);
		local_Question02WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_388;
	}

	private void Relay_In_389()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_389.In(logic_uScriptAct_SetInt_Value_389, out logic_uScriptAct_SetInt_Target_389);
		local_QuestionsStage_System_Int32 = logic_uScriptAct_SetInt_Target_389;
		if (logic_uScriptAct_SetInt_uScriptAct_SetInt_389.Out)
		{
			Relay_In_308();
		}
	}

	private void Relay_In_396()
	{
		logic_uScript_Wait_seconds_396 = WaitingTime;
		logic_uScript_Wait_uScript_Wait_396.In(logic_uScript_Wait_seconds_396, logic_uScript_Wait_repeat_396);
		if (logic_uScript_Wait_uScript_Wait_396.Waited)
		{
			Relay_False_388();
		}
	}

	private void Relay_True_400()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_400.True(out logic_uScriptAct_SetBool_Target_400);
		local_Question01WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_400;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_400.Out)
		{
			Relay_False_261();
		}
	}

	private void Relay_False_400()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_400.False(out logic_uScriptAct_SetBool_Target_400);
		local_Question01WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_400;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_400.Out)
		{
			Relay_False_261();
		}
	}

	private void Relay_True_407()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_407.True(out logic_uScriptAct_SetBool_Target_407);
		local_CanPushButtons_System_Boolean = logic_uScriptAct_SetBool_Target_407;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_407.Out)
		{
			Relay_In_478();
		}
	}

	private void Relay_False_407()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_407.False(out logic_uScriptAct_SetBool_Target_407);
		local_CanPushButtons_System_Boolean = logic_uScriptAct_SetBool_Target_407;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_407.Out)
		{
			Relay_In_478();
		}
	}

	private void Relay_In_410()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_410.In(logic_uScriptAct_SetInt_Value_410, out logic_uScriptAct_SetInt_Target_410);
		local_QuestionTwo_System_Int32 = logic_uScriptAct_SetInt_Target_410;
	}

	private void Relay_In_411()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_411.In(logic_uScriptAct_SetInt_Value_411, out logic_uScriptAct_SetInt_Target_411);
		local_QuestionTwo_System_Int32 = logic_uScriptAct_SetInt_Target_411;
	}

	private void Relay_In_412()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_412.In(logic_uScriptAct_SetInt_Value_412, out logic_uScriptAct_SetInt_Target_412);
		local_QuestionOne_System_Int32 = logic_uScriptAct_SetInt_Target_412;
	}

	private void Relay_In_413()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_413.In(logic_uScriptAct_SetInt_Value_413, out logic_uScriptAct_SetInt_Target_413);
		local_QuestionTwo_System_Int32 = logic_uScriptAct_SetInt_Target_413;
	}

	private void Relay_True_416()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_416.True(out logic_uScriptAct_SetBool_Target_416);
		local_CanPushButtons_System_Boolean = logic_uScriptAct_SetBool_Target_416;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_416.Out)
		{
			Relay_In_235();
		}
	}

	private void Relay_False_416()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_416.False(out logic_uScriptAct_SetBool_Target_416);
		local_CanPushButtons_System_Boolean = logic_uScriptAct_SetBool_Target_416;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_416.Out)
		{
			Relay_In_235();
		}
	}

	private void Relay_In_417()
	{
		logic_uScriptAct_AddInt_v2_A_417 = local_QuestionOne_System_Int32;
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_417.In(logic_uScriptAct_AddInt_v2_A_417, logic_uScriptAct_AddInt_v2_B_417, out logic_uScriptAct_AddInt_v2_IntResult_417, out logic_uScriptAct_AddInt_v2_FloatResult_417);
		local_QuestionOne_System_Int32 = logic_uScriptAct_AddInt_v2_IntResult_417;
	}

	private void Relay_In_418()
	{
		logic_uScriptCon_CompareInt_A_418 = local_Button4Value_System_Int32;
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_418.In(logic_uScriptCon_CompareInt_A_418, logic_uScriptCon_CompareInt_B_418);
		if (logic_uScriptCon_CompareInt_uScriptCon_CompareInt_418.GreaterThan)
		{
			Relay_In_310();
		}
	}

	private void Relay_Output1_422()
	{
		Relay_False_284();
	}

	private void Relay_Output2_422()
	{
		Relay_In_275();
	}

	private void Relay_Output3_422()
	{
		Relay_In_238();
	}

	private void Relay_Output4_422()
	{
		Relay_In_466();
	}

	private void Relay_Output5_422()
	{
		Relay_In_249();
	}

	private void Relay_Output6_422()
	{
	}

	private void Relay_Output7_422()
	{
	}

	private void Relay_Output8_422()
	{
	}

	private void Relay_In_422()
	{
		logic_uScriptCon_ManualSwitch_CurrentOutput_422 = local_QuestionTwo_System_Int32;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_422.In(logic_uScriptCon_ManualSwitch_CurrentOutput_422);
	}

	private void Relay_True_425()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_425.True(out logic_uScriptAct_SetBool_Target_425);
		local_CanPushButtons_System_Boolean = logic_uScriptAct_SetBool_Target_425;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_425.Out)
		{
			Relay_In_241();
		}
	}

	private void Relay_False_425()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_425.False(out logic_uScriptAct_SetBool_Target_425);
		local_CanPushButtons_System_Boolean = logic_uScriptAct_SetBool_Target_425;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_425.Out)
		{
			Relay_In_241();
		}
	}

	private void Relay_In_426()
	{
		logic_uScript_Wait_seconds_426 = WaitingTime;
		logic_uScript_Wait_uScript_Wait_426.In(logic_uScript_Wait_seconds_426, logic_uScript_Wait_repeat_426);
		if (logic_uScript_Wait_uScript_Wait_426.Waited)
		{
			Relay_In_276();
		}
	}

	private void Relay_In_427()
	{
		logic_uScriptCon_CompareBool_Bool_427 = local_Question01WrongAnswers_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_427.In(logic_uScriptCon_CompareBool_Bool_427);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_427.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_427.False;
		if (num)
		{
			Relay_In_296();
		}
		if (flag)
		{
			Relay_In_289();
		}
	}

	private void Relay_True_429()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_429.True(out logic_uScriptAct_SetBool_Target_429);
		local_CanPushButtons_System_Boolean = logic_uScriptAct_SetBool_Target_429;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_429.Out)
		{
			Relay_In_435();
		}
	}

	private void Relay_False_429()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_429.False(out logic_uScriptAct_SetBool_Target_429);
		local_CanPushButtons_System_Boolean = logic_uScriptAct_SetBool_Target_429;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_429.Out)
		{
			Relay_In_435();
		}
	}

	private void Relay_In_430()
	{
		int num = 0;
		Array enemyTechData = EnemyTechData;
		if (logic_uScript_GetAndCheckTechs_techData_430.Length != num + enemyTechData.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_430, num + enemyTechData.Length);
		}
		Array.Copy(enemyTechData, 0, logic_uScript_GetAndCheckTechs_techData_430, num, enemyTechData.Length);
		num += enemyTechData.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_430 = owner_Connection_415;
		int num2 = 0;
		Array array = local_Enemy_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_430.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_430, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techs_430, num2, array.Length);
		num2 += array.Length;
		logic_uScript_GetAndCheckTechs_Return_430 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_430.In(logic_uScript_GetAndCheckTechs_techData_430, logic_uScript_GetAndCheckTechs_ownerNode_430, ref logic_uScript_GetAndCheckTechs_techs_430);
		local_Enemy_TankArray = logic_uScript_GetAndCheckTechs_techs_430;
		if (logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_430.AllDead)
		{
			Relay_In_258();
		}
	}

	private void Relay_True_431()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_431.True(out logic_uScriptAct_SetBool_Target_431);
		local_Question01WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_431;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_431.Out)
		{
			Relay_False_360();
		}
	}

	private void Relay_False_431()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_431.False(out logic_uScriptAct_SetBool_Target_431);
		local_Question01WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_431;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_431.Out)
		{
			Relay_False_360();
		}
	}

	private void Relay_In_433()
	{
		logic_uScript_GetCircuitChargeInfo_block_433 = local_ButtonBlock2_TankBlock;
		logic_uScript_GetCircuitChargeInfo_Return_433 = logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_433.In(logic_uScript_GetCircuitChargeInfo_block_433, logic_uScript_GetCircuitChargeInfo_tech_433, logic_uScript_GetCircuitChargeInfo_blockType_433);
		local_Button2Value_System_Int32 = logic_uScript_GetCircuitChargeInfo_Return_433;
		if (logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_433.Out)
		{
			Relay_In_271();
		}
	}

	private void Relay_In_434()
	{
		logic_uScript_GetCircuitChargeInfo_block_434 = local_ButtonBlock3_TankBlock;
		logic_uScript_GetCircuitChargeInfo_Return_434 = logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_434.In(logic_uScript_GetCircuitChargeInfo_block_434, logic_uScript_GetCircuitChargeInfo_tech_434, logic_uScript_GetCircuitChargeInfo_blockType_434);
		local_Button3Value_System_Int32 = logic_uScript_GetCircuitChargeInfo_Return_434;
		if (logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_434.Out)
		{
			Relay_In_369();
		}
	}

	private void Relay_In_435()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_435.In(logic_uScriptAct_SetInt_Value_435, out logic_uScriptAct_SetInt_Target_435);
		local_QuestionTwo_System_Int32 = logic_uScriptAct_SetInt_Target_435;
	}

	private void Relay_In_436()
	{
		logic_uScript_AddMessage_messageData_436 = msgMinionDefeated;
		logic_uScript_AddMessage_speaker_436 = messageSpeaker;
		logic_uScript_AddMessage_Return_436 = logic_uScript_AddMessage_uScript_AddMessage_436.In(logic_uScript_AddMessage_messageData_436, logic_uScript_AddMessage_speaker_436);
		if (logic_uScript_AddMessage_uScript_AddMessage_436.Out)
		{
			Relay_In_379();
		}
	}

	private void Relay_In_437()
	{
		logic_uScript_AddMessage_messageData_437 = msg03QuestionOneCorrect;
		logic_uScript_AddMessage_speaker_437 = messageSpeaker;
		logic_uScript_AddMessage_Return_437 = logic_uScript_AddMessage_uScript_AddMessage_437.In(logic_uScript_AddMessage_messageData_437, logic_uScript_AddMessage_speaker_437);
		if (logic_uScript_AddMessage_uScript_AddMessage_437.Out)
		{
			Relay_In_426();
		}
	}

	private void Relay_True_438()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_438.True(out logic_uScriptAct_SetBool_Target_438);
		local_CanPushButtons_System_Boolean = logic_uScriptAct_SetBool_Target_438;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_438.Out)
		{
			Relay_In_413();
		}
	}

	private void Relay_False_438()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_438.False(out logic_uScriptAct_SetBool_Target_438);
		local_CanPushButtons_System_Boolean = logic_uScriptAct_SetBool_Target_438;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_438.Out)
		{
			Relay_In_413();
		}
	}

	private void Relay_InitialSpawn_439()
	{
		int num = 0;
		Array enemyTechData = EnemyTechData;
		if (logic_uScript_SpawnTechsFromData_spawnData_439.Length != num + enemyTechData.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_439, num + enemyTechData.Length);
		}
		Array.Copy(enemyTechData, 0, logic_uScript_SpawnTechsFromData_spawnData_439, num, enemyTechData.Length);
		num += enemyTechData.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_439 = owner_Connection_283;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_439.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_439, logic_uScript_SpawnTechsFromData_ownerNode_439, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_439, logic_uScript_SpawnTechsFromData_allowResurrection_439);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_439.Out)
		{
			Relay_True_452();
		}
	}

	private void Relay_True_446()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_446.True(out logic_uScriptAct_SetBool_Target_446);
		local_CanPushButtons_System_Boolean = logic_uScriptAct_SetBool_Target_446;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_446.Out)
		{
			Relay_In_243();
		}
	}

	private void Relay_False_446()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_446.False(out logic_uScriptAct_SetBool_Target_446);
		local_CanPushButtons_System_Boolean = logic_uScriptAct_SetBool_Target_446;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_446.Out)
		{
			Relay_In_243();
		}
	}

	private void Relay_True_447()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_447.True(out logic_uScriptAct_SetBool_Target_447);
		local_Question02WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_447;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_447.Out)
		{
			Relay_False_327();
		}
	}

	private void Relay_False_447()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_447.False(out logic_uScriptAct_SetBool_Target_447);
		local_Question02WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_447;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_447.Out)
		{
			Relay_False_327();
		}
	}

	private void Relay_In_449()
	{
		logic_uScript_AddMessage_messageData_449 = msg03QuestionOne;
		logic_uScript_AddMessage_speaker_449 = messageSpeaker;
		logic_uScript_AddMessage_Return_449 = logic_uScript_AddMessage_uScript_AddMessage_449.In(logic_uScript_AddMessage_messageData_449, logic_uScript_AddMessage_speaker_449);
		if (logic_uScript_AddMessage_uScript_AddMessage_449.Out)
		{
			Relay_In_323();
		}
	}

	private void Relay_In_450()
	{
		logic_uScript_Wait_seconds_450 = WaitingTime;
		logic_uScript_Wait_uScript_Wait_450.In(logic_uScript_Wait_seconds_450, logic_uScript_Wait_repeat_450);
		if (logic_uScript_Wait_uScript_Wait_450.Waited)
		{
			Relay_In_334();
		}
	}

	private void Relay_True_452()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_452.True(out logic_uScriptAct_SetBool_Target_452);
		local_Q1EnemyAlive_System_Boolean = logic_uScriptAct_SetBool_Target_452;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_452.Out)
		{
			Relay_In_370();
		}
	}

	private void Relay_False_452()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_452.False(out logic_uScriptAct_SetBool_Target_452);
		local_Q1EnemyAlive_System_Boolean = logic_uScriptAct_SetBool_Target_452;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_452.Out)
		{
			Relay_In_370();
		}
	}

	private void Relay_In_454()
	{
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_454.In(logic_uScriptCon_CompareBool_Bool_454);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_454.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_454.False;
		if (num)
		{
			Relay_False_447();
		}
		if (flag)
		{
			Relay_True_345();
		}
	}

	private void Relay_In_456()
	{
		logic_uScript_GetCircuitChargeInfo_block_456 = local_ButtonBlock2_TankBlock;
		logic_uScript_GetCircuitChargeInfo_Return_456 = logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_456.In(logic_uScript_GetCircuitChargeInfo_block_456, logic_uScript_GetCircuitChargeInfo_tech_456, logic_uScript_GetCircuitChargeInfo_blockType_456);
		local_Button2Value_System_Int32 = logic_uScript_GetCircuitChargeInfo_Return_456;
		if (logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_456.Out)
		{
			Relay_In_267();
		}
	}

	private void Relay_In_458()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_458.In(logic_uScriptAct_SetInt_Value_458, out logic_uScriptAct_SetInt_Target_458);
		local_QuestionsStage_System_Int32 = logic_uScriptAct_SetInt_Target_458;
		if (logic_uScriptAct_SetInt_uScriptAct_SetInt_458.Out)
		{
			Relay_In_330();
		}
	}

	private void Relay_In_461()
	{
		logic_uScriptAct_AddInt_v2_A_461 = local_QuestionOne_System_Int32;
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_461.In(logic_uScriptAct_AddInt_v2_A_461, logic_uScriptAct_AddInt_v2_B_461, out logic_uScriptAct_AddInt_v2_IntResult_461, out logic_uScriptAct_AddInt_v2_FloatResult_461);
		local_QuestionOne_System_Int32 = logic_uScriptAct_AddInt_v2_IntResult_461;
	}

	private void Relay_In_466()
	{
		logic_uScriptCon_CompareBool_Bool_466 = local_Question02WrongAnswers_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_466.In(logic_uScriptCon_CompareBool_Bool_466);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_466.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_466.False;
		if (num)
		{
			Relay_In_294();
		}
		if (flag)
		{
			Relay_In_365();
		}
	}

	private void Relay_In_467()
	{
		logic_uScriptCon_CompareBool_Bool_467 = Q1Button1;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_467.In(logic_uScriptCon_CompareBool_Bool_467);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_467.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_467.False;
		if (num)
		{
			Relay_False_314();
		}
		if (flag)
		{
			Relay_True_468();
		}
	}

	private void Relay_True_468()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_468.True(out logic_uScriptAct_SetBool_Target_468);
		local_Question01WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_468;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_468.Out)
		{
			Relay_False_256();
		}
	}

	private void Relay_False_468()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_468.False(out logic_uScriptAct_SetBool_Target_468);
		local_Question01WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_468;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_468.Out)
		{
			Relay_False_256();
		}
	}

	private void Relay_In_469()
	{
		logic_uScriptCon_CompareInt_A_469 = local_Button3Value_System_Int32;
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_469.In(logic_uScriptCon_CompareInt_A_469, logic_uScriptCon_CompareInt_B_469);
		bool greaterThan = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_469.GreaterThan;
		bool lessThanOrEqualTo = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_469.LessThanOrEqualTo;
		if (greaterThan)
		{
			Relay_In_257();
		}
		if (lessThanOrEqualTo)
		{
			Relay_In_305();
		}
	}

	private void Relay_True_471()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_471.True(out logic_uScriptAct_SetBool_Target_471);
		local_Question01WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_471;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_471.Out)
		{
			Relay_False_446();
		}
	}

	private void Relay_False_471()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_471.False(out logic_uScriptAct_SetBool_Target_471);
		local_Question01WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_471;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_471.Out)
		{
			Relay_False_446();
		}
	}

	private void Relay_In_472()
	{
		logic_uScript_AddMessage_messageData_472 = msgSpawnMinion;
		logic_uScript_AddMessage_speaker_472 = messageSpeaker;
		logic_uScript_AddMessage_Return_472 = logic_uScript_AddMessage_uScript_AddMessage_472.In(logic_uScript_AddMessage_messageData_472, logic_uScript_AddMessage_speaker_472);
		if (logic_uScript_AddMessage_uScript_AddMessage_472.Out)
		{
			Relay_In_370();
		}
	}

	private void Relay_True_474()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_474.True(out logic_uScriptAct_SetBool_Target_474);
		local_Q1EnemyAlive_System_Boolean = logic_uScriptAct_SetBool_Target_474;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_474.Out)
		{
			Relay_False_317();
		}
	}

	private void Relay_False_474()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_474.False(out logic_uScriptAct_SetBool_Target_474);
		local_Q1EnemyAlive_System_Boolean = logic_uScriptAct_SetBool_Target_474;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_474.Out)
		{
			Relay_False_317();
		}
	}

	private void Relay_True_475()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_475.True(out logic_uScriptAct_SetBool_Target_475);
		local_CanPushButtons_System_Boolean = logic_uScriptAct_SetBool_Target_475;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_475.Out)
		{
			Relay_In_234();
		}
	}

	private void Relay_False_475()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_475.False(out logic_uScriptAct_SetBool_Target_475);
		local_CanPushButtons_System_Boolean = logic_uScriptAct_SetBool_Target_475;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_475.Out)
		{
			Relay_In_234();
		}
	}

	private void Relay_In_478()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_478.In(logic_uScriptAct_SetInt_Value_478, out logic_uScriptAct_SetInt_Target_478);
		local_QuestionOne_System_Int32 = logic_uScriptAct_SetInt_Target_478;
	}

	private void Relay_True_479()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_479.True(out logic_uScriptAct_SetBool_Target_479);
		local_CanPushButtons_System_Boolean = logic_uScriptAct_SetBool_Target_479;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_479.Out)
		{
			Relay_In_277();
		}
	}

	private void Relay_False_479()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_479.False(out logic_uScriptAct_SetBool_Target_479);
		local_CanPushButtons_System_Boolean = logic_uScriptAct_SetBool_Target_479;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_479.Out)
		{
			Relay_In_277();
		}
	}

	private void Relay_In_481()
	{
		logic_uScript_RemoveTech_tech_481 = local_ButtonBase3Tech_Tank;
		logic_uScript_RemoveTech_uScript_RemoveTech_481.In(logic_uScript_RemoveTech_tech_481);
		if (logic_uScript_RemoveTech_uScript_RemoveTech_481.Out)
		{
			Relay_In_582();
		}
	}

	private void Relay_True_482()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_482.True(out logic_uScriptAct_SetBool_Target_482);
		local_Question03WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_482;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_482.Out)
		{
			Relay_False_528();
		}
	}

	private void Relay_False_482()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_482.False(out logic_uScriptAct_SetBool_Target_482);
		local_Question03WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_482;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_482.Out)
		{
			Relay_False_528();
		}
	}

	private void Relay_In_485()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_485.In(logic_uScriptAct_SetInt_Value_485, out logic_uScriptAct_SetInt_Target_485);
		local_QuestionThree_System_Int32 = logic_uScriptAct_SetInt_Target_485;
	}

	private void Relay_True_487()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_487.True(out logic_uScriptAct_SetBool_Target_487);
		local_Question03WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_487;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_487.Out)
		{
			Relay_False_549();
		}
	}

	private void Relay_False_487()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_487.False(out logic_uScriptAct_SetBool_Target_487);
		local_Question03WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_487;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_487.Out)
		{
			Relay_False_549();
		}
	}

	private void Relay_True_489()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_489.True(out logic_uScriptAct_SetBool_Target_489);
		local_Q3EnemyAlive_System_Boolean = logic_uScriptAct_SetBool_Target_489;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_489.Out)
		{
			Relay_In_552();
		}
	}

	private void Relay_False_489()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_489.False(out logic_uScriptAct_SetBool_Target_489);
		local_Q3EnemyAlive_System_Boolean = logic_uScriptAct_SetBool_Target_489;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_489.Out)
		{
			Relay_In_552();
		}
	}

	private void Relay_True_494()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_494.True(out logic_uScriptAct_SetBool_Target_494);
		local_Question03WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_494;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_494.Out)
		{
			Relay_False_512();
		}
	}

	private void Relay_False_494()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_494.False(out logic_uScriptAct_SetBool_Target_494);
		local_Question03WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_494;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_494.Out)
		{
			Relay_False_512();
		}
	}

	private void Relay_In_495()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_495.In(logic_uScriptAct_SetInt_Value_495, out logic_uScriptAct_SetInt_Target_495);
		local_QuestionThree_System_Int32 = logic_uScriptAct_SetInt_Target_495;
	}

	private void Relay_In_496()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_496.In(logic_uScriptAct_SetInt_Value_496, out logic_uScriptAct_SetInt_Target_496);
		local_QuestionThree_System_Int32 = logic_uScriptAct_SetInt_Target_496;
	}

	private void Relay_In_500()
	{
		logic_uScriptCon_CompareInt_A_500 = local_Button2Value_System_Int32;
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_500.In(logic_uScriptCon_CompareInt_A_500, logic_uScriptCon_CompareInt_B_500);
		bool greaterThan = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_500.GreaterThan;
		bool lessThanOrEqualTo = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_500.LessThanOrEqualTo;
		if (greaterThan)
		{
			Relay_In_555();
		}
		if (lessThanOrEqualTo)
		{
			Relay_In_554();
		}
	}

	private void Relay_In_504()
	{
		logic_uScript_AddMessage_messageData_504 = msgMinionDefeated;
		logic_uScript_AddMessage_speaker_504 = messageSpeaker;
		logic_uScript_AddMessage_Return_504 = logic_uScript_AddMessage_uScript_AddMessage_504.In(logic_uScript_AddMessage_messageData_504, logic_uScript_AddMessage_speaker_504);
		if (logic_uScript_AddMessage_uScript_AddMessage_504.Out)
		{
			Relay_In_600();
		}
	}

	private void Relay_In_509()
	{
		logic_uScript_GetCircuitChargeInfo_block_509 = local_ButtonBlock2_TankBlock;
		logic_uScript_GetCircuitChargeInfo_Return_509 = logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_509.In(logic_uScript_GetCircuitChargeInfo_block_509, logic_uScript_GetCircuitChargeInfo_tech_509, logic_uScript_GetCircuitChargeInfo_blockType_509);
		local_Button2Value_System_Int32 = logic_uScript_GetCircuitChargeInfo_Return_509;
		if (logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_509.Out)
		{
			Relay_In_500();
		}
	}

	private void Relay_In_510()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_510.In(logic_uScriptAct_SetInt_Value_510, out logic_uScriptAct_SetInt_Target_510);
		local_QuestionThree_System_Int32 = logic_uScriptAct_SetInt_Target_510;
	}

	private void Relay_True_511()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_511.True(out logic_uScriptAct_SetBool_Target_511);
		local_Question03WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_511;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_511.Out)
		{
			Relay_False_542();
		}
	}

	private void Relay_False_511()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_511.False(out logic_uScriptAct_SetBool_Target_511);
		local_Question03WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_511;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_511.Out)
		{
			Relay_False_542();
		}
	}

	private void Relay_True_512()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_512.True(out logic_uScriptAct_SetBool_Target_512);
		local_CanPushButtons_System_Boolean = logic_uScriptAct_SetBool_Target_512;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_512.Out)
		{
			Relay_In_585();
		}
	}

	private void Relay_False_512()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_512.False(out logic_uScriptAct_SetBool_Target_512);
		local_CanPushButtons_System_Boolean = logic_uScriptAct_SetBool_Target_512;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_512.Out)
		{
			Relay_In_585();
		}
	}

	private void Relay_In_513()
	{
		logic_uScript_SpawnVFX_ownerNode_513 = owner_Connection_618;
		logic_uScript_SpawnVFX_vfxToSpawn_513 = NPCDespawnParticleEffect;
		logic_uScript_SpawnVFX_spawnPosName_513 = ButtonBase3VFXSpawn;
		logic_uScript_SpawnVFX_uScript_SpawnVFX_513.In(logic_uScript_SpawnVFX_ownerNode_513, logic_uScript_SpawnVFX_vfxToSpawn_513, logic_uScript_SpawnVFX_spawnPosName_513);
		if (logic_uScript_SpawnVFX_uScript_SpawnVFX_513.Out)
		{
			Relay_In_481();
		}
	}

	private void Relay_Output1_515()
	{
		Relay_False_576();
	}

	private void Relay_Output2_515()
	{
		Relay_In_638();
	}

	private void Relay_Output3_515()
	{
		Relay_In_635();
	}

	private void Relay_Output4_515()
	{
		Relay_In_562();
	}

	private void Relay_Output5_515()
	{
		Relay_In_504();
	}

	private void Relay_Output6_515()
	{
		Relay_In_593();
	}

	private void Relay_Output7_515()
	{
	}

	private void Relay_Output8_515()
	{
	}

	private void Relay_In_515()
	{
		logic_uScriptCon_ManualSwitch_CurrentOutput_515 = local_QuestionThree_System_Int32;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_515.In(logic_uScriptCon_ManualSwitch_CurrentOutput_515);
	}

	private void Relay_True_516()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_516.True(out logic_uScriptAct_SetBool_Target_516);
		local_Question03WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_516;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_516.Out)
		{
			Relay_False_526();
		}
	}

	private void Relay_False_516()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_516.False(out logic_uScriptAct_SetBool_Target_516);
		local_Question03WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_516;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_516.Out)
		{
			Relay_False_526();
		}
	}

	private void Relay_True_518()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_518.True(out logic_uScriptAct_SetBool_Target_518);
		local_Question03WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_518;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_518.Out)
		{
			Relay_False_520();
		}
	}

	private void Relay_False_518()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_518.False(out logic_uScriptAct_SetBool_Target_518);
		local_Question03WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_518;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_518.Out)
		{
			Relay_False_520();
		}
	}

	private void Relay_True_520()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_520.True(out logic_uScriptAct_SetBool_Target_520);
		local_CanPushButtons_System_Boolean = logic_uScriptAct_SetBool_Target_520;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_520.Out)
		{
			Relay_In_574();
		}
	}

	private void Relay_False_520()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_520.False(out logic_uScriptAct_SetBool_Target_520);
		local_CanPushButtons_System_Boolean = logic_uScriptAct_SetBool_Target_520;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_520.Out)
		{
			Relay_In_574();
		}
	}

	private void Relay_In_522()
	{
		logic_uScriptCon_CompareInt_A_522 = local_Button3Value_System_Int32;
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_522.In(logic_uScriptCon_CompareInt_A_522, logic_uScriptCon_CompareInt_B_522);
		bool greaterThan = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_522.GreaterThan;
		bool lessThanOrEqualTo = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_522.LessThanOrEqualTo;
		if (greaterThan)
		{
			Relay_In_621();
		}
		if (lessThanOrEqualTo)
		{
			Relay_In_628();
		}
	}

	private void Relay_In_523()
	{
		logic_uScriptAct_AddInt_v2_A_523 = local_QuestionThree_System_Int32;
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_523.In(logic_uScriptAct_AddInt_v2_A_523, logic_uScriptAct_AddInt_v2_B_523, out logic_uScriptAct_AddInt_v2_IntResult_523, out logic_uScriptAct_AddInt_v2_FloatResult_523);
		local_QuestionThree_System_Int32 = logic_uScriptAct_AddInt_v2_IntResult_523;
	}

	private void Relay_True_526()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_526.True(out logic_uScriptAct_SetBool_Target_526);
		local_CanPushButtons_System_Boolean = logic_uScriptAct_SetBool_Target_526;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_526.Out)
		{
			Relay_In_553();
		}
	}

	private void Relay_False_526()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_526.False(out logic_uScriptAct_SetBool_Target_526);
		local_CanPushButtons_System_Boolean = logic_uScriptAct_SetBool_Target_526;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_526.Out)
		{
			Relay_In_553();
		}
	}

	private void Relay_True_528()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_528.True(out logic_uScriptAct_SetBool_Target_528);
		local_CanPushButtons_System_Boolean = logic_uScriptAct_SetBool_Target_528;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_528.Out)
		{
			Relay_In_626();
		}
	}

	private void Relay_False_528()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_528.False(out logic_uScriptAct_SetBool_Target_528);
		local_CanPushButtons_System_Boolean = logic_uScriptAct_SetBool_Target_528;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_528.Out)
		{
			Relay_In_626();
		}
	}

	private void Relay_In_529()
	{
		logic_uScriptAct_AddInt_v2_A_529 = local_QuestionThree_System_Int32;
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_529.In(logic_uScriptAct_AddInt_v2_A_529, logic_uScriptAct_AddInt_v2_B_529, out logic_uScriptAct_AddInt_v2_IntResult_529, out logic_uScriptAct_AddInt_v2_FloatResult_529);
		local_QuestionThree_System_Int32 = logic_uScriptAct_AddInt_v2_IntResult_529;
	}

	private void Relay_In_533()
	{
		logic_uScript_GetCircuitChargeInfo_block_533 = local_ButtonBlock1_TankBlock;
		logic_uScript_GetCircuitChargeInfo_Return_533 = logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_533.In(logic_uScript_GetCircuitChargeInfo_block_533, logic_uScript_GetCircuitChargeInfo_tech_533, logic_uScript_GetCircuitChargeInfo_blockType_533);
		local_Button1Value_System_Int32 = logic_uScript_GetCircuitChargeInfo_Return_533;
		if (logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_533.Out)
		{
			Relay_In_622();
		}
	}

	private void Relay_True_540()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_540.True(out logic_uScriptAct_SetBool_Target_540);
		local_Question03WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_540;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_540.Out)
		{
			Relay_False_629();
		}
	}

	private void Relay_False_540()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_540.False(out logic_uScriptAct_SetBool_Target_540);
		local_Question03WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_540;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_540.Out)
		{
			Relay_False_629();
		}
	}

	private void Relay_True_541()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_541.True(out logic_uScriptAct_SetBool_Target_541);
		local_CanPushButtons_System_Boolean = logic_uScriptAct_SetBool_Target_541;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_541.Out)
		{
			Relay_In_496();
		}
	}

	private void Relay_False_541()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_541.False(out logic_uScriptAct_SetBool_Target_541);
		local_CanPushButtons_System_Boolean = logic_uScriptAct_SetBool_Target_541;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_541.Out)
		{
			Relay_In_496();
		}
	}

	private void Relay_True_542()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_542.True(out logic_uScriptAct_SetBool_Target_542);
		local_CanPushButtons_System_Boolean = logic_uScriptAct_SetBool_Target_542;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_542.Out)
		{
			Relay_In_495();
		}
	}

	private void Relay_False_542()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_542.False(out logic_uScriptAct_SetBool_Target_542);
		local_CanPushButtons_System_Boolean = logic_uScriptAct_SetBool_Target_542;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_542.Out)
		{
			Relay_In_495();
		}
	}

	private void Relay_True_549()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_549.True(out logic_uScriptAct_SetBool_Target_549);
		local_CanPushButtons_System_Boolean = logic_uScriptAct_SetBool_Target_549;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_549.Out)
		{
			Relay_In_485();
		}
	}

	private void Relay_False_549()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_549.False(out logic_uScriptAct_SetBool_Target_549);
		local_CanPushButtons_System_Boolean = logic_uScriptAct_SetBool_Target_549;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_549.Out)
		{
			Relay_In_485();
		}
	}

	private void Relay_In_550()
	{
		logic_uScriptCon_CompareBool_Bool_550 = Q3Button1;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_550.In(logic_uScriptCon_CompareBool_Bool_550);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_550.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_550.False;
		if (num)
		{
			Relay_False_540();
		}
		if (flag)
		{
			Relay_True_511();
		}
	}

	private void Relay_In_552()
	{
		int num = 0;
		Array enemyTechData = EnemyTechData;
		if (logic_uScript_GetAndCheckTechs_techData_552.Length != num + enemyTechData.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_552, num + enemyTechData.Length);
		}
		Array.Copy(enemyTechData, 0, logic_uScript_GetAndCheckTechs_techData_552, num, enemyTechData.Length);
		num += enemyTechData.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_552 = owner_Connection_497;
		int num2 = 0;
		Array array = local_Enemy_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_552.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_552, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techs_552, num2, array.Length);
		num2 += array.Length;
		logic_uScript_GetAndCheckTechs_Return_552 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_552.In(logic_uScript_GetAndCheckTechs_techData_552, logic_uScript_GetAndCheckTechs_ownerNode_552, ref logic_uScript_GetAndCheckTechs_techs_552);
		local_Enemy_TankArray = logic_uScript_GetAndCheckTechs_techs_552;
		if (logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_552.AllDead)
		{
			Relay_In_529();
		}
	}

	private void Relay_In_553()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_553.In(logic_uScriptAct_SetInt_Value_553, out logic_uScriptAct_SetInt_Target_553);
		local_QuestionThree_System_Int32 = logic_uScriptAct_SetInt_Target_553;
	}

	private void Relay_In_554()
	{
		logic_uScript_GetCircuitChargeInfo_block_554 = local_ButtonBlock3_TankBlock;
		logic_uScript_GetCircuitChargeInfo_Return_554 = logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_554.In(logic_uScript_GetCircuitChargeInfo_block_554, logic_uScript_GetCircuitChargeInfo_tech_554, logic_uScript_GetCircuitChargeInfo_blockType_554);
		local_Button3Value_System_Int32 = logic_uScript_GetCircuitChargeInfo_Return_554;
		if (logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_554.Out)
		{
			Relay_In_522();
		}
	}

	private void Relay_In_555()
	{
		logic_uScriptCon_CompareBool_Bool_555 = Q3Button2;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_555.In(logic_uScriptCon_CompareBool_Bool_555);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_555.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_555.False;
		if (num)
		{
			Relay_False_482();
		}
		if (flag)
		{
			Relay_True_610();
		}
	}

	private void Relay_In_562()
	{
		logic_uScriptCon_CompareBool_Bool_562 = local_Question03WrongAnswers_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_562.In(logic_uScriptCon_CompareBool_Bool_562);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_562.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_562.False;
		if (num)
		{
			Relay_In_580();
		}
		if (flag)
		{
			Relay_In_616();
		}
	}

	private void Relay_True_566()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_566.True(out logic_uScriptAct_SetBool_Target_566);
		local_CanPushButtons_System_Boolean = logic_uScriptAct_SetBool_Target_566;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_566.Out)
		{
			Relay_In_523();
		}
	}

	private void Relay_False_566()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_566.False(out logic_uScriptAct_SetBool_Target_566);
		local_CanPushButtons_System_Boolean = logic_uScriptAct_SetBool_Target_566;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_566.Out)
		{
			Relay_In_523();
		}
	}

	private void Relay_In_567()
	{
		logic_uScript_Wait_seconds_567 = WaitingTime;
		logic_uScript_Wait_uScript_Wait_567.In(logic_uScript_Wait_seconds_567, logic_uScript_Wait_repeat_567);
		if (logic_uScript_Wait_uScript_Wait_567.Waited)
		{
			Relay_False_598();
		}
	}

	private void Relay_In_568()
	{
		logic_uScript_RemoveTech_tech_568 = local_ButtonBase2Tech_Tank;
		logic_uScript_RemoveTech_uScript_RemoveTech_568.In(logic_uScript_RemoveTech_tech_568);
		if (logic_uScript_RemoveTech_uScript_RemoveTech_568.Out)
		{
			Relay_In_513();
		}
	}

	private void Relay_In_570()
	{
		logic_uScript_RemoveTech_tech_570 = local_ButtonBase1Tech_Tank;
		logic_uScript_RemoveTech_uScript_RemoveTech_570.In(logic_uScript_RemoveTech_tech_570);
		if (logic_uScript_RemoveTech_uScript_RemoveTech_570.Out)
		{
			Relay_In_625();
		}
	}

	private void Relay_In_572()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_572.In(logic_uScriptAct_SetInt_Value_572, out logic_uScriptAct_SetInt_Target_572);
		local_QuestionOne_System_Int32 = logic_uScriptAct_SetInt_Target_572;
	}

	private void Relay_In_574()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_574.In(logic_uScriptAct_SetInt_Value_574, out logic_uScriptAct_SetInt_Target_574);
		local_QuestionThree_System_Int32 = logic_uScriptAct_SetInt_Target_574;
	}

	private void Relay_In_575()
	{
		logic_uScriptCon_CompareInt_A_575 = local_Button4Value_System_Int32;
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_575.In(logic_uScriptCon_CompareInt_A_575, logic_uScriptCon_CompareInt_B_575);
		if (logic_uScriptCon_CompareInt_uScriptCon_CompareInt_575.GreaterThan)
		{
			Relay_In_590();
		}
	}

	private void Relay_True_576()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_576.True(out logic_uScriptAct_SetBool_Target_576);
		local_Q3EnemyAlive_System_Boolean = logic_uScriptAct_SetBool_Target_576;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_576.Out)
		{
			Relay_False_601();
		}
	}

	private void Relay_False_576()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_576.False(out logic_uScriptAct_SetBool_Target_576);
		local_Q3EnemyAlive_System_Boolean = logic_uScriptAct_SetBool_Target_576;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_576.Out)
		{
			Relay_False_601();
		}
	}

	private void Relay_In_580()
	{
		logic_uScript_AddMessage_messageData_580 = msg10QuestionThreeWrong;
		logic_uScript_AddMessage_speaker_580 = messageSpeaker;
		logic_uScript_AddMessage_Return_580 = logic_uScript_AddMessage_uScript_AddMessage_580.In(logic_uScript_AddMessage_messageData_580, logic_uScript_AddMessage_speaker_580);
		if (logic_uScript_AddMessage_uScript_AddMessage_580.Out)
		{
			Relay_In_567();
		}
	}

	private void Relay_In_582()
	{
		logic_uScript_SpawnVFX_ownerNode_582 = owner_Connection_519;
		logic_uScript_SpawnVFX_vfxToSpawn_582 = NPCDespawnParticleEffect;
		logic_uScript_SpawnVFX_spawnPosName_582 = ButtonBase4VFXSpawn;
		logic_uScript_SpawnVFX_uScript_SpawnVFX_582.In(logic_uScript_SpawnVFX_ownerNode_582, logic_uScript_SpawnVFX_vfxToSpawn_582, logic_uScript_SpawnVFX_spawnPosName_582);
		if (logic_uScript_SpawnVFX_uScript_SpawnVFX_582.Out)
		{
			Relay_In_594();
		}
	}

	private void Relay_In_585()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_585.In(logic_uScriptAct_SetInt_Value_585, out logic_uScriptAct_SetInt_Target_585);
		local_QuestionThree_System_Int32 = logic_uScriptAct_SetInt_Target_585;
	}

	private void Relay_InitialSpawn_586()
	{
		int num = 0;
		Array enemyTechData = EnemyTechData;
		if (logic_uScript_SpawnTechsFromData_spawnData_586.Length != num + enemyTechData.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_586, num + enemyTechData.Length);
		}
		Array.Copy(enemyTechData, 0, logic_uScript_SpawnTechsFromData_spawnData_586, num, enemyTechData.Length);
		num += enemyTechData.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_586 = owner_Connection_611;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_586.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_586, logic_uScript_SpawnTechsFromData_ownerNode_586, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_586, logic_uScript_SpawnTechsFromData_allowResurrection_586);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_586.Out)
		{
			Relay_True_489();
		}
	}

	private void Relay_In_588()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_588.In(logic_uScriptAct_SetInt_Value_588, out logic_uScriptAct_SetInt_Target_588);
		local_QuestionThree_System_Int32 = logic_uScriptAct_SetInt_Target_588;
	}

	private void Relay_In_590()
	{
		logic_uScriptCon_CompareBool_Bool_590 = Q3Button4;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_590.In(logic_uScriptCon_CompareBool_Bool_590);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_590.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_590.False;
		if (num)
		{
			Relay_False_487();
		}
		if (flag)
		{
			Relay_True_518();
		}
	}

	private void Relay_In_592()
	{
		logic_uScript_AddMessage_messageData_592 = msgSpawnMinion;
		logic_uScript_AddMessage_speaker_592 = messageSpeaker;
		logic_uScript_AddMessage_Return_592 = logic_uScript_AddMessage_uScript_AddMessage_592.In(logic_uScript_AddMessage_messageData_592, logic_uScript_AddMessage_speaker_592);
		if (logic_uScript_AddMessage_uScript_AddMessage_592.Out)
		{
			Relay_In_552();
		}
	}

	private void Relay_In_593()
	{
		logic_uScript_AddMessage_messageData_593 = msgEncounterComplete;
		logic_uScript_AddMessage_speaker_593 = messageSpeaker;
		logic_uScript_AddMessage_Return_593 = logic_uScript_AddMessage_uScript_AddMessage_593.In(logic_uScript_AddMessage_messageData_593, logic_uScript_AddMessage_speaker_593);
		if (logic_uScript_AddMessage_uScript_AddMessage_593.Shown)
		{
			Relay_In_627();
		}
	}

	private void Relay_In_594()
	{
		logic_uScript_RemoveTech_tech_594 = local_ButtonBase4Tech_Tank;
		logic_uScript_RemoveTech_uScript_RemoveTech_594.In(logic_uScript_RemoveTech_tech_594);
		if (logic_uScript_RemoveTech_uScript_RemoveTech_594.Out)
		{
			Relay_In_597();
		}
	}

	private void Relay_In_597()
	{
		logic_uScript_FlyTechUpAndAway_tech_597 = local_NPCTech_Tank;
		logic_uScript_FlyTechUpAndAway_aiTree_597 = NPCFlyAwayAI;
		logic_uScript_FlyTechUpAndAway_removalParticles_597 = NPCDespawnParticleEffect;
		logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_597.In(logic_uScript_FlyTechUpAndAway_tech_597, logic_uScript_FlyTechUpAndAway_maxLifetime_597, logic_uScript_FlyTechUpAndAway_targetHeight_597, logic_uScript_FlyTechUpAndAway_aiTree_597, logic_uScript_FlyTechUpAndAway_removalParticles_597);
		if (logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_597.Out)
		{
			Relay_Succeed_624();
		}
	}

	private void Relay_True_598()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_598.True(out logic_uScriptAct_SetBool_Target_598);
		local_Question03WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_598;
	}

	private void Relay_False_598()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_598.False(out logic_uScriptAct_SetBool_Target_598);
		local_Question03WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_598;
	}

	private void Relay_In_600()
	{
		logic_uScript_Wait_seconds_600 = WaitingTime;
		logic_uScript_Wait_uScript_Wait_600.In(logic_uScript_Wait_seconds_600, logic_uScript_Wait_repeat_600);
		if (logic_uScript_Wait_uScript_Wait_600.Waited)
		{
			Relay_In_604();
		}
	}

	private void Relay_True_601()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_601.True(out logic_uScriptAct_SetBool_Target_601);
		local_Question03WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_601;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_601.Out)
		{
			Relay_True_566();
		}
	}

	private void Relay_False_601()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_601.False(out logic_uScriptAct_SetBool_Target_601);
		local_Question03WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_601;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_601.Out)
		{
			Relay_True_566();
		}
	}

	private void Relay_In_604()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_604.In(logic_uScriptAct_SetInt_Value_604, out logic_uScriptAct_SetInt_Target_604);
		local_QuestionsStage_System_Int32 = logic_uScriptAct_SetInt_Target_604;
		if (logic_uScriptAct_SetInt_uScriptAct_SetInt_604.Out)
		{
			Relay_In_572();
		}
	}

	private void Relay_True_610()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_610.True(out logic_uScriptAct_SetBool_Target_610);
		local_Question03WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_610;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_610.Out)
		{
			Relay_False_541();
		}
	}

	private void Relay_False_610()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_610.False(out logic_uScriptAct_SetBool_Target_610);
		local_Question03WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_610;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_610.Out)
		{
			Relay_False_541();
		}
	}

	private void Relay_In_612()
	{
		logic_uScript_Wait_seconds_612 = WaitingTime;
		logic_uScript_Wait_uScript_Wait_612.In(logic_uScript_Wait_seconds_612, logic_uScript_Wait_repeat_612);
		if (logic_uScript_Wait_uScript_Wait_612.Waited)
		{
			Relay_In_510();
		}
	}

	private void Relay_In_616()
	{
		logic_uScriptCon_CompareBool_Bool_616 = local_Q3EnemyAlive_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_616.In(logic_uScriptCon_CompareBool_Bool_616);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_616.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_616.False;
		if (num)
		{
			Relay_In_592();
		}
		if (flag)
		{
			Relay_InitialSpawn_586();
		}
	}

	private void Relay_In_621()
	{
		logic_uScriptCon_CompareBool_Bool_621 = Q3Button3;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_621.In(logic_uScriptCon_CompareBool_Bool_621);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_621.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_621.False;
		if (num)
		{
			Relay_False_516();
		}
		if (flag)
		{
			Relay_True_494();
		}
	}

	private void Relay_In_622()
	{
		logic_uScriptCon_CompareInt_A_622 = local_Button1Value_System_Int32;
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_622.In(logic_uScriptCon_CompareInt_A_622, logic_uScriptCon_CompareInt_B_622);
		bool greaterThan = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_622.GreaterThan;
		bool lessThanOrEqualTo = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_622.LessThanOrEqualTo;
		if (greaterThan)
		{
			Relay_In_550();
		}
		if (lessThanOrEqualTo)
		{
			Relay_In_509();
		}
	}

	private void Relay_Succeed_624()
	{
		logic_uScript_FinishEncounter_owner_624 = owner_Connection_615;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_624.Succeed(logic_uScript_FinishEncounter_owner_624);
	}

	private void Relay_Fail_624()
	{
		logic_uScript_FinishEncounter_owner_624 = owner_Connection_615;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_624.Fail(logic_uScript_FinishEncounter_owner_624);
	}

	private void Relay_In_625()
	{
		logic_uScript_SpawnVFX_ownerNode_625 = owner_Connection_589;
		logic_uScript_SpawnVFX_vfxToSpawn_625 = NPCDespawnParticleEffect;
		logic_uScript_SpawnVFX_spawnPosName_625 = ButtonBase2VFXSpawn;
		logic_uScript_SpawnVFX_uScript_SpawnVFX_625.In(logic_uScript_SpawnVFX_ownerNode_625, logic_uScript_SpawnVFX_vfxToSpawn_625, logic_uScript_SpawnVFX_spawnPosName_625);
		if (logic_uScript_SpawnVFX_uScript_SpawnVFX_625.Out)
		{
			Relay_In_568();
		}
	}

	private void Relay_In_626()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_626.In(logic_uScriptAct_SetInt_Value_626, out logic_uScriptAct_SetInt_Target_626);
		local_QuestionThree_System_Int32 = logic_uScriptAct_SetInt_Target_626;
	}

	private void Relay_In_627()
	{
		logic_uScript_SpawnVFX_ownerNode_627 = owner_Connection_531;
		logic_uScript_SpawnVFX_vfxToSpawn_627 = NPCDespawnParticleEffect;
		logic_uScript_SpawnVFX_spawnPosName_627 = ButtonBase1VFXSpawn;
		logic_uScript_SpawnVFX_uScript_SpawnVFX_627.In(logic_uScript_SpawnVFX_ownerNode_627, logic_uScript_SpawnVFX_vfxToSpawn_627, logic_uScript_SpawnVFX_spawnPosName_627);
		if (logic_uScript_SpawnVFX_uScript_SpawnVFX_627.Out)
		{
			Relay_In_570();
		}
	}

	private void Relay_In_628()
	{
		logic_uScript_GetCircuitChargeInfo_block_628 = local_ButtonBlock4_TankBlock;
		logic_uScript_GetCircuitChargeInfo_Return_628 = logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_628.In(logic_uScript_GetCircuitChargeInfo_block_628, logic_uScript_GetCircuitChargeInfo_tech_628, logic_uScript_GetCircuitChargeInfo_blockType_628);
		local_Button4Value_System_Int32 = logic_uScript_GetCircuitChargeInfo_Return_628;
		if (logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_628.Out)
		{
			Relay_In_575();
		}
	}

	private void Relay_True_629()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_629.True(out logic_uScriptAct_SetBool_Target_629);
		local_CanPushButtons_System_Boolean = logic_uScriptAct_SetBool_Target_629;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_629.Out)
		{
			Relay_In_588();
		}
	}

	private void Relay_False_629()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_629.False(out logic_uScriptAct_SetBool_Target_629);
		local_CanPushButtons_System_Boolean = logic_uScriptAct_SetBool_Target_629;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_629.Out)
		{
			Relay_In_588();
		}
	}

	private void Relay_In_635()
	{
		logic_uScript_AddMessage_messageData_635 = msg09QuestionThreeCorrect;
		logic_uScript_AddMessage_speaker_635 = messageSpeaker;
		logic_uScript_AddMessage_Return_635 = logic_uScript_AddMessage_uScript_AddMessage_635.In(logic_uScript_AddMessage_messageData_635, logic_uScript_AddMessage_speaker_635);
		if (logic_uScript_AddMessage_uScript_AddMessage_635.Out)
		{
			Relay_In_612();
		}
	}

	private void Relay_In_638()
	{
		logic_uScript_AddMessage_messageData_638 = msg08QuestionThree;
		logic_uScript_AddMessage_speaker_638 = messageSpeaker;
		logic_uScript_AddMessage_Return_638 = logic_uScript_AddMessage_uScript_AddMessage_638.In(logic_uScript_AddMessage_messageData_638, logic_uScript_AddMessage_speaker_638);
		if (logic_uScript_AddMessage_uScript_AddMessage_638.Out)
		{
			Relay_In_533();
		}
	}

	private void Relay_Save_Out_644()
	{
		Relay_Save_167();
	}

	private void Relay_Load_Out_644()
	{
		Relay_Load_167();
	}

	private void Relay_Restart_Out_644()
	{
		Relay_Restart_167();
	}

	private void Relay_Save_644()
	{
		logic_SubGraph_SaveLoadInt_integer_644 = local_QuestionStage_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_644 = local_QuestionStage_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_644.Save(logic_SubGraph_SaveLoadInt_restartValue_644, ref logic_SubGraph_SaveLoadInt_integer_644, logic_SubGraph_SaveLoadInt_intAsVariable_644, logic_SubGraph_SaveLoadInt_uniqueID_644);
	}

	private void Relay_Load_644()
	{
		logic_SubGraph_SaveLoadInt_integer_644 = local_QuestionStage_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_644 = local_QuestionStage_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_644.Load(logic_SubGraph_SaveLoadInt_restartValue_644, ref logic_SubGraph_SaveLoadInt_integer_644, logic_SubGraph_SaveLoadInt_intAsVariable_644, logic_SubGraph_SaveLoadInt_uniqueID_644);
	}

	private void Relay_Restart_644()
	{
		logic_SubGraph_SaveLoadInt_integer_644 = local_QuestionStage_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_644 = local_QuestionStage_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_644.Restart(logic_SubGraph_SaveLoadInt_restartValue_644, ref logic_SubGraph_SaveLoadInt_integer_644, logic_SubGraph_SaveLoadInt_intAsVariable_644, logic_SubGraph_SaveLoadInt_uniqueID_644);
	}

	private void Relay_True_647()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_647.True(out logic_uScriptAct_SetBool_Target_647);
		local_CanPushButtons_System_Boolean = logic_uScriptAct_SetBool_Target_647;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_647.Out)
		{
			Relay_In_649();
		}
	}

	private void Relay_False_647()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_647.False(out logic_uScriptAct_SetBool_Target_647);
		local_CanPushButtons_System_Boolean = logic_uScriptAct_SetBool_Target_647;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_647.Out)
		{
			Relay_In_649();
		}
	}

	private void Relay_In_649()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_649.In(logic_uScriptAct_SetInt_Value_649, out logic_uScriptAct_SetInt_Target_649);
		local_QuestionOne_System_Int32 = logic_uScriptAct_SetInt_Target_649;
	}

	private void Relay_In_651()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_651.In(logic_uScriptAct_SetInt_Value_651, out logic_uScriptAct_SetInt_Target_651);
		local_QuestionOne_System_Int32 = logic_uScriptAct_SetInt_Target_651;
	}
}
