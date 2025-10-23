using System;
using BehaviorDesigner.Runtime;
using UnityEngine;

[Serializable]
[NodePath("Graphs")]
[FriendlyName("msgShownMinionDefeated", "")]
public class Mission_Unlock_SpaceJunkers_License : uScriptLogic
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

	private Tank[] local_114_TankArray = new Tank[0];

	private Tank[] local_152_TankArray = new Tank[0];

	private Tank[] local_171_TankArray = new Tank[0];

	private Tank[] local_223_TankArray = new Tank[0];

	private string local_350_System_String = "";

	private string local_352_System_String = "HasBeenShown:";

	private Tank[] local_53_TankArray = new Tank[0];

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

	private bool local_Initialize_System_Boolean;

	private bool local_msgBaseFoundShown_System_Boolean;

	private bool local_msgIntro_System_Boolean;

	private bool local_NearNPC_System_Boolean;

	private Tank local_NPCTech_Tank;

	private Tank[] local_Q1Enemy_TankArray = new Tank[0];

	private bool local_Q1EnemyAlive_System_Boolean;

	private Tank[] local_Q2Enemy_TankArray = new Tank[0];

	private bool local_Q2EnemyAlive_System_Boolean;

	private Tank[] local_Q3Enemy_TankArray = new Tank[0];

	private bool local_Q3EnemyAlive_System_Boolean;

	private bool local_Question01WrongAnswers_System_Boolean;

	private bool local_Question02WrongAnswers_System_Boolean;

	private bool local_Question03WrongAnswers_System_Boolean;

	private int local_Stage_System_Int32 = 1;

	private int local_Stage2QuestionOne_System_Int32 = 1;

	private int local_Stage3QuestionTwo_System_Int32 = 1;

	private int local_Stage4QuestionThree_System_Int32 = 1;

	public uScript_AddMessage.MessageSpeaker messageSpeaker;

	[Multiline(3)]
	public string messageTag = "";

	public uScript_AddMessage.MessageData msg01Intro;

	public uScript_AddMessage.MessageData msg02BaseFound;

	public uScript_AddMessage.MessageData msg02BaseFound_Pad;

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

	public SpawnTechData[] Q1EnemyTechData = new SpawnTechData[0];

	public SpawnTechData[] Q2EnemyTechData = new SpawnTechData[0];

	public SpawnTechData[] Q3EnemyTechData = new SpawnTechData[0];

	public float WaitingTime;

	private GameObject owner_Connection_3;

	private GameObject owner_Connection_29;

	private GameObject owner_Connection_37;

	private GameObject owner_Connection_45;

	private GameObject owner_Connection_74;

	private GameObject owner_Connection_77;

	private GameObject owner_Connection_78;

	private GameObject owner_Connection_79;

	private GameObject owner_Connection_110;

	private GameObject owner_Connection_192;

	private GameObject owner_Connection_207;

	private GameObject owner_Connection_251;

	private GameObject owner_Connection_268;

	private GameObject owner_Connection_272;

	private GameObject owner_Connection_305;

	private GameObject owner_Connection_338;

	private GameObject owner_Connection_344;

	private GameObject owner_Connection_357;

	private GameObject owner_Connection_362;

	private GameObject owner_Connection_370;

	private GameObject owner_Connection_379;

	private GameObject owner_Connection_435;

	private GameObject owner_Connection_444;

	private GameObject owner_Connection_447;

	private GameObject owner_Connection_449;

	private GameObject owner_Connection_451;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_8 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_8;

	private bool logic_uScriptAct_SetBool_Out_8 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_8 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_8 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_12 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_12;

	private bool logic_uScriptCon_CompareBool_True_12 = true;

	private bool logic_uScriptCon_CompareBool_False_12 = true;

	private uScript_GetCircuitChargeInfo logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_15 = new uScript_GetCircuitChargeInfo();

	private TankBlock logic_uScript_GetCircuitChargeInfo_block_15;

	private Tank logic_uScript_GetCircuitChargeInfo_tech_15;

	private BlockTypes logic_uScript_GetCircuitChargeInfo_blockType_15;

	private int logic_uScript_GetCircuitChargeInfo_Return_15;

	private bool logic_uScript_GetCircuitChargeInfo_Out_15 = true;

	private bool logic_uScript_GetCircuitChargeInfo_HasValidBlock_15 = true;

	private uScript_SetCustomRadarTeamID logic_uScript_SetCustomRadarTeamID_uScript_SetCustomRadarTeamID_16 = new uScript_SetCustomRadarTeamID();

	private Tank logic_uScript_SetCustomRadarTeamID_tech_16;

	private int logic_uScript_SetCustomRadarTeamID_radarTeamID_16 = -2;

	private bool logic_uScript_SetCustomRadarTeamID_Out_16 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_17 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_17;

	private bool logic_uScriptCon_CompareBool_True_17 = true;

	private bool logic_uScriptCon_CompareBool_False_17 = true;

	private uScript_SetTankInvulnerable logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_20 = new uScript_SetTankInvulnerable();

	private bool logic_uScript_SetTankInvulnerable_invulnerable_20 = true;

	private Tank logic_uScript_SetTankInvulnerable_tank_20;

	private bool logic_uScript_SetTankInvulnerable_Out_20 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_21 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_21 = true;

	private uScript_SetCustomRadarTeamID logic_uScript_SetCustomRadarTeamID_uScript_SetCustomRadarTeamID_23 = new uScript_SetCustomRadarTeamID();

	private Tank logic_uScript_SetCustomRadarTeamID_tech_23;

	private int logic_uScript_SetCustomRadarTeamID_radarTeamID_23 = -2;

	private bool logic_uScript_SetCustomRadarTeamID_Out_23 = true;

	private uScript_GetCircuitChargeInfo logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_24 = new uScript_GetCircuitChargeInfo();

	private TankBlock logic_uScript_GetCircuitChargeInfo_block_24;

	private Tank logic_uScript_GetCircuitChargeInfo_tech_24;

	private BlockTypes logic_uScript_GetCircuitChargeInfo_blockType_24;

	private int logic_uScript_GetCircuitChargeInfo_Return_24;

	private bool logic_uScript_GetCircuitChargeInfo_Out_24 = true;

	private bool logic_uScript_GetCircuitChargeInfo_HasValidBlock_24 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_27 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_27;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_27;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_27;

	private bool logic_uScript_AddMessage_Out_27 = true;

	private bool logic_uScript_AddMessage_Shown_27 = true;

	private uScriptCon_CompareInt logic_uScriptCon_CompareInt_uScriptCon_CompareInt_30 = new uScriptCon_CompareInt();

	private int logic_uScriptCon_CompareInt_A_30;

	private int logic_uScriptCon_CompareInt_B_30;

	private bool logic_uScriptCon_CompareInt_GreaterThan_30 = true;

	private bool logic_uScriptCon_CompareInt_GreaterThanOrEqualTo_30 = true;

	private bool logic_uScriptCon_CompareInt_EqualTo_30 = true;

	private bool logic_uScriptCon_CompareInt_NotEqualTo_30 = true;

	private bool logic_uScriptCon_CompareInt_LessThanOrEqualTo_30 = true;

	private bool logic_uScriptCon_CompareInt_LessThan_30 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_31 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_31;

	private bool logic_uScriptAct_SetBool_Out_31 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_31 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_31 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_35 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_35 = 3;

	private int logic_uScriptAct_SetInt_Target_35;

	private bool logic_uScriptAct_SetInt_Out_35 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_39 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_39 = 3;

	private int logic_uScriptAct_SetInt_Target_39;

	private bool logic_uScriptAct_SetInt_Out_39 = true;

	private uScript_LockTechInteraction logic_uScript_LockTechInteraction_uScript_LockTechInteraction_41 = new uScript_LockTechInteraction();

	private Tank logic_uScript_LockTechInteraction_tech_41;

	private BlockTypes[] logic_uScript_LockTechInteraction_excludedBlocks_41 = new BlockTypes[0];

	private TankBlock[] logic_uScript_LockTechInteraction_excludedUniqueBlocks_41 = new TankBlock[0];

	private bool logic_uScript_LockTechInteraction_Out_41 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_42 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_42 = true;

	private uScript_GetCircuitChargeInfo logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_43 = new uScript_GetCircuitChargeInfo();

	private TankBlock logic_uScript_GetCircuitChargeInfo_block_43;

	private Tank logic_uScript_GetCircuitChargeInfo_tech_43;

	private BlockTypes logic_uScript_GetCircuitChargeInfo_blockType_43;

	private int logic_uScript_GetCircuitChargeInfo_Return_43;

	private bool logic_uScript_GetCircuitChargeInfo_Out_43 = true;

	private bool logic_uScript_GetCircuitChargeInfo_HasValidBlock_43 = true;

	private uScript_GetCircuitChargeInfo logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_47 = new uScript_GetCircuitChargeInfo();

	private TankBlock logic_uScript_GetCircuitChargeInfo_block_47;

	private Tank logic_uScript_GetCircuitChargeInfo_tech_47;

	private BlockTypes logic_uScript_GetCircuitChargeInfo_blockType_47;

	private int logic_uScript_GetCircuitChargeInfo_Return_47;

	private bool logic_uScript_GetCircuitChargeInfo_Out_47 = true;

	private bool logic_uScript_GetCircuitChargeInfo_HasValidBlock_47 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_50 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_50;

	private bool logic_uScriptAct_SetBool_Out_50 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_50 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_50 = true;

	private uScript_LockTechInteraction logic_uScript_LockTechInteraction_uScript_LockTechInteraction_51 = new uScript_LockTechInteraction();

	private Tank logic_uScript_LockTechInteraction_tech_51;

	private BlockTypes[] logic_uScript_LockTechInteraction_excludedBlocks_51 = new BlockTypes[0];

	private TankBlock[] logic_uScript_LockTechInteraction_excludedUniqueBlocks_51 = new TankBlock[0];

	private bool logic_uScript_LockTechInteraction_Out_51 = true;

	private uScriptCon_CompareInt logic_uScriptCon_CompareInt_uScriptCon_CompareInt_52 = new uScriptCon_CompareInt();

	private int logic_uScriptCon_CompareInt_A_52;

	private int logic_uScriptCon_CompareInt_B_52;

	private bool logic_uScriptCon_CompareInt_GreaterThan_52 = true;

	private bool logic_uScriptCon_CompareInt_GreaterThanOrEqualTo_52 = true;

	private bool logic_uScriptCon_CompareInt_EqualTo_52 = true;

	private bool logic_uScriptCon_CompareInt_NotEqualTo_52 = true;

	private bool logic_uScriptCon_CompareInt_LessThanOrEqualTo_52 = true;

	private bool logic_uScriptCon_CompareInt_LessThan_52 = true;

	private uScript_SetTankInvulnerable logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_56 = new uScript_SetTankInvulnerable();

	private bool logic_uScript_SetTankInvulnerable_invulnerable_56 = true;

	private Tank logic_uScript_SetTankInvulnerable_tank_56;

	private bool logic_uScript_SetTankInvulnerable_Out_56 = true;

	private uScriptCon_CompareInt logic_uScriptCon_CompareInt_uScriptCon_CompareInt_61 = new uScriptCon_CompareInt();

	private int logic_uScriptCon_CompareInt_A_61;

	private int logic_uScriptCon_CompareInt_B_61;

	private bool logic_uScriptCon_CompareInt_GreaterThan_61 = true;

	private bool logic_uScriptCon_CompareInt_GreaterThanOrEqualTo_61 = true;

	private bool logic_uScriptCon_CompareInt_EqualTo_61 = true;

	private bool logic_uScriptCon_CompareInt_NotEqualTo_61 = true;

	private bool logic_uScriptCon_CompareInt_LessThanOrEqualTo_61 = true;

	private bool logic_uScriptCon_CompareInt_LessThan_61 = true;

	private uScript_PausePopulation logic_uScript_PausePopulation_uScript_PausePopulation_66 = new uScript_PausePopulation();

	private bool logic_uScript_PausePopulation_Out_66 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_68 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_68 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_71 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_71;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_71 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_71 = "CanPushButtons";

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_72 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_72;

	private bool logic_uScriptAct_SetBool_Out_72 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_72 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_72 = true;

	private uScript_LockTechInteraction logic_uScript_LockTechInteraction_uScript_LockTechInteraction_73 = new uScript_LockTechInteraction();

	private Tank logic_uScript_LockTechInteraction_tech_73;

	private BlockTypes[] logic_uScript_LockTechInteraction_excludedBlocks_73 = new BlockTypes[0];

	private TankBlock[] logic_uScript_LockTechInteraction_excludedUniqueBlocks_73 = new TankBlock[0];

	private bool logic_uScript_LockTechInteraction_Out_73 = true;

	private uScriptCon_CompareInt logic_uScriptCon_CompareInt_uScriptCon_CompareInt_75 = new uScriptCon_CompareInt();

	private int logic_uScriptCon_CompareInt_A_75;

	private int logic_uScriptCon_CompareInt_B_75;

	private bool logic_uScriptCon_CompareInt_GreaterThan_75 = true;

	private bool logic_uScriptCon_CompareInt_GreaterThanOrEqualTo_75 = true;

	private bool logic_uScriptCon_CompareInt_EqualTo_75 = true;

	private bool logic_uScriptCon_CompareInt_NotEqualTo_75 = true;

	private bool logic_uScriptCon_CompareInt_LessThanOrEqualTo_75 = true;

	private bool logic_uScriptCon_CompareInt_LessThan_75 = true;

	private uScript_LockTechInteraction logic_uScript_LockTechInteraction_uScript_LockTechInteraction_76 = new uScript_LockTechInteraction();

	private Tank logic_uScript_LockTechInteraction_tech_76;

	private BlockTypes[] logic_uScript_LockTechInteraction_excludedBlocks_76 = new BlockTypes[0];

	private TankBlock[] logic_uScript_LockTechInteraction_excludedUniqueBlocks_76 = new TankBlock[0];

	private bool logic_uScript_LockTechInteraction_Out_76 = true;

	private uScript_LockTech logic_uScript_LockTech_uScript_LockTech_82 = new uScript_LockTech();

	private Tank logic_uScript_LockTech_tech_82;

	private uScript_LockTech.TechLockType logic_uScript_LockTech_lockType_82 = uScript_LockTech.TechLockType.LockAll;

	private bool logic_uScript_LockTech_Out_82 = true;

	private uScriptCon_CompareInt logic_uScriptCon_CompareInt_uScriptCon_CompareInt_83 = new uScriptCon_CompareInt();

	private int logic_uScriptCon_CompareInt_A_83;

	private int logic_uScriptCon_CompareInt_B_83;

	private bool logic_uScriptCon_CompareInt_GreaterThan_83 = true;

	private bool logic_uScriptCon_CompareInt_GreaterThanOrEqualTo_83 = true;

	private bool logic_uScriptCon_CompareInt_EqualTo_83 = true;

	private bool logic_uScriptCon_CompareInt_NotEqualTo_83 = true;

	private bool logic_uScriptCon_CompareInt_LessThanOrEqualTo_83 = true;

	private bool logic_uScriptCon_CompareInt_LessThan_83 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_84 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_84;

	private bool logic_uScriptAct_SetBool_Out_84 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_84 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_84 = true;

	private SubGraph_CompleteObjectiveStage logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_85 = new SubGraph_CompleteObjectiveStage();

	private int logic_SubGraph_CompleteObjectiveStage_objectiveStage_85;

	private bool logic_SubGraph_CompleteObjectiveStage_isFinalObjective_85;

	private uScriptCon_CompareInt logic_uScriptCon_CompareInt_uScriptCon_CompareInt_86 = new uScriptCon_CompareInt();

	private int logic_uScriptCon_CompareInt_A_86;

	private int logic_uScriptCon_CompareInt_B_86;

	private bool logic_uScriptCon_CompareInt_GreaterThan_86 = true;

	private bool logic_uScriptCon_CompareInt_GreaterThanOrEqualTo_86 = true;

	private bool logic_uScriptCon_CompareInt_EqualTo_86 = true;

	private bool logic_uScriptCon_CompareInt_NotEqualTo_86 = true;

	private bool logic_uScriptCon_CompareInt_LessThanOrEqualTo_86 = true;

	private bool logic_uScriptCon_CompareInt_LessThan_86 = true;

	private uScript_SetCustomRadarTeamID logic_uScript_SetCustomRadarTeamID_uScript_SetCustomRadarTeamID_87 = new uScript_SetCustomRadarTeamID();

	private Tank logic_uScript_SetCustomRadarTeamID_tech_87;

	private int logic_uScript_SetCustomRadarTeamID_radarTeamID_87 = -2;

	private bool logic_uScript_SetCustomRadarTeamID_Out_87 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_88 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_88 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_88;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_88 = 0.3f;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_88;

	private bool logic_uScript_SpawnTechsFromData_Out_88 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_89 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_89;

	private bool logic_uScriptAct_SetBool_Out_89 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_89 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_89 = true;

	private uScript_Wait logic_uScript_Wait_uScript_Wait_90 = new uScript_Wait();

	private float logic_uScript_Wait_seconds_90;

	private bool logic_uScript_Wait_repeat_90 = true;

	private bool logic_uScript_Wait_Waited_90 = true;

	private uScriptCon_CompareInt logic_uScriptCon_CompareInt_uScriptCon_CompareInt_94 = new uScriptCon_CompareInt();

	private int logic_uScriptCon_CompareInt_A_94;

	private int logic_uScriptCon_CompareInt_B_94;

	private bool logic_uScriptCon_CompareInt_GreaterThan_94 = true;

	private bool logic_uScriptCon_CompareInt_GreaterThanOrEqualTo_94 = true;

	private bool logic_uScriptCon_CompareInt_EqualTo_94 = true;

	private bool logic_uScriptCon_CompareInt_NotEqualTo_94 = true;

	private bool logic_uScriptCon_CompareInt_LessThanOrEqualTo_94 = true;

	private bool logic_uScriptCon_CompareInt_LessThan_94 = true;

	private uScriptCon_CompareInt logic_uScriptCon_CompareInt_uScriptCon_CompareInt_95 = new uScriptCon_CompareInt();

	private int logic_uScriptCon_CompareInt_A_95;

	private int logic_uScriptCon_CompareInt_B_95;

	private bool logic_uScriptCon_CompareInt_GreaterThan_95 = true;

	private bool logic_uScriptCon_CompareInt_GreaterThanOrEqualTo_95 = true;

	private bool logic_uScriptCon_CompareInt_EqualTo_95 = true;

	private bool logic_uScriptCon_CompareInt_NotEqualTo_95 = true;

	private bool logic_uScriptCon_CompareInt_LessThanOrEqualTo_95 = true;

	private bool logic_uScriptCon_CompareInt_LessThan_95 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_96 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_96 = 3;

	private int logic_uScriptAct_SetInt_Target_96;

	private bool logic_uScriptAct_SetInt_Out_96 = true;

	private uScript_LockTechInteraction logic_uScript_LockTechInteraction_uScript_LockTechInteraction_98 = new uScript_LockTechInteraction();

	private Tank logic_uScript_LockTechInteraction_tech_98;

	private BlockTypes[] logic_uScript_LockTechInteraction_excludedBlocks_98 = new BlockTypes[0];

	private TankBlock[] logic_uScript_LockTechInteraction_excludedUniqueBlocks_98 = new TankBlock[0];

	private bool logic_uScript_LockTechInteraction_Out_98 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_100 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_100;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_100;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_100;

	private bool logic_uScript_AddMessage_Out_100 = true;

	private bool logic_uScript_AddMessage_Shown_100 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_101 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_101;

	private bool logic_uScriptAct_SetBool_Out_101 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_101 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_101 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_103 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_103 = 2;

	private int logic_uScriptAct_SetInt_Target_103;

	private bool logic_uScriptAct_SetInt_Out_103 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_104 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_104 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_104;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_104 = 0.3f;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_104;

	private bool logic_uScript_SpawnTechsFromData_Out_104 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_105 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_105;

	private bool logic_uScriptAct_SetBool_Out_105 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_105 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_105 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_107 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_107 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_107 = true;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_107 = true;

	private uScript_SetCustomRadarTeamID logic_uScript_SetCustomRadarTeamID_uScript_SetCustomRadarTeamID_109 = new uScript_SetCustomRadarTeamID();

	private Tank logic_uScript_SetCustomRadarTeamID_tech_109;

	private int logic_uScript_SetCustomRadarTeamID_radarTeamID_109 = -2;

	private bool logic_uScript_SetCustomRadarTeamID_Out_109 = true;

	private uScript_GetCircuitChargeInfo logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_111 = new uScript_GetCircuitChargeInfo();

	private TankBlock logic_uScript_GetCircuitChargeInfo_block_111;

	private Tank logic_uScript_GetCircuitChargeInfo_tech_111;

	private BlockTypes logic_uScript_GetCircuitChargeInfo_blockType_111;

	private int logic_uScript_GetCircuitChargeInfo_Return_111;

	private bool logic_uScript_GetCircuitChargeInfo_Out_111 = true;

	private bool logic_uScript_GetCircuitChargeInfo_HasValidBlock_111 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_113 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_113;

	private bool logic_uScriptAct_SetBool_Out_113 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_113 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_113 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_116 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_116;

	private bool logic_uScriptAct_SetBool_Out_116 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_116 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_116 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_117 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_117;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_117 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_117 = "msgIntro";

	private uScript_GetTankBlock logic_uScript_GetTankBlock_uScript_GetTankBlock_120 = new uScript_GetTankBlock();

	private Tank logic_uScript_GetTankBlock_tank_120;

	private BlockTypes logic_uScript_GetTankBlock_blockType_120;

	private TankBlock logic_uScript_GetTankBlock_Return_120;

	private bool logic_uScript_GetTankBlock_Out_120 = true;

	private bool logic_uScript_GetTankBlock_Returned_120 = true;

	private bool logic_uScript_GetTankBlock_NotFound_120 = true;

	private uScript_GetTankBlock logic_uScript_GetTankBlock_uScript_GetTankBlock_123 = new uScript_GetTankBlock();

	private Tank logic_uScript_GetTankBlock_tank_123;

	private BlockTypes logic_uScript_GetTankBlock_blockType_123;

	private TankBlock logic_uScript_GetTankBlock_Return_123;

	private bool logic_uScript_GetTankBlock_Out_123 = true;

	private bool logic_uScript_GetTankBlock_Returned_123 = true;

	private bool logic_uScript_GetTankBlock_NotFound_123 = true;

	private uScriptCon_CompareInt logic_uScriptCon_CompareInt_uScriptCon_CompareInt_124 = new uScriptCon_CompareInt();

	private int logic_uScriptCon_CompareInt_A_124;

	private int logic_uScriptCon_CompareInt_B_124;

	private bool logic_uScriptCon_CompareInt_GreaterThan_124 = true;

	private bool logic_uScriptCon_CompareInt_GreaterThanOrEqualTo_124 = true;

	private bool logic_uScriptCon_CompareInt_EqualTo_124 = true;

	private bool logic_uScriptCon_CompareInt_NotEqualTo_124 = true;

	private bool logic_uScriptCon_CompareInt_LessThanOrEqualTo_124 = true;

	private bool logic_uScriptCon_CompareInt_LessThan_124 = true;

	private uScript_GetTankBlock logic_uScript_GetTankBlock_uScript_GetTankBlock_132 = new uScript_GetTankBlock();

	private Tank logic_uScript_GetTankBlock_tank_132;

	private BlockTypes logic_uScript_GetTankBlock_blockType_132;

	private TankBlock logic_uScript_GetTankBlock_Return_132;

	private bool logic_uScript_GetTankBlock_Out_132 = true;

	private bool logic_uScript_GetTankBlock_Returned_132 = true;

	private bool logic_uScript_GetTankBlock_NotFound_132 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_133 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_133 = new Tank[0];

	private int logic_uScript_AccessListTech_index_133;

	private Tank logic_uScript_AccessListTech_value_133;

	private bool logic_uScript_AccessListTech_Out_133 = true;

	private uScriptCon_ManualSwitch logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_134 = new uScriptCon_ManualSwitch();

	private int logic_uScriptCon_ManualSwitch_CurrentOutput_134;

	private uScript_SetTankHideBlockLimit logic_uScript_SetTankHideBlockLimit_uScript_SetTankHideBlockLimit_137 = new uScript_SetTankHideBlockLimit();

	private bool logic_uScript_SetTankHideBlockLimit_hidden_137 = true;

	private Tank logic_uScript_SetTankHideBlockLimit_tech_137;

	private bool logic_uScript_SetTankHideBlockLimit_Out_137 = true;

	private uScript_LockTechInteraction logic_uScript_LockTechInteraction_uScript_LockTechInteraction_138 = new uScript_LockTechInteraction();

	private Tank logic_uScript_LockTechInteraction_tech_138;

	private BlockTypes[] logic_uScript_LockTechInteraction_excludedBlocks_138 = new BlockTypes[0];

	private TankBlock[] logic_uScript_LockTechInteraction_excludedUniqueBlocks_138 = new TankBlock[0];

	private bool logic_uScript_LockTechInteraction_Out_138 = true;

	private uScript_Wait logic_uScript_Wait_uScript_Wait_139 = new uScript_Wait();

	private float logic_uScript_Wait_seconds_139;

	private bool logic_uScript_Wait_repeat_139 = true;

	private bool logic_uScript_Wait_Waited_139 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_143 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_143;

	private bool logic_uScriptAct_SetBool_Out_143 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_143 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_143 = true;

	private uScript_SetCustomRadarTeamID logic_uScript_SetCustomRadarTeamID_uScript_SetCustomRadarTeamID_149 = new uScript_SetCustomRadarTeamID();

	private Tank logic_uScript_SetCustomRadarTeamID_tech_149;

	private int logic_uScript_SetCustomRadarTeamID_radarTeamID_149 = -2;

	private bool logic_uScript_SetCustomRadarTeamID_Out_149 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_150 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_150;

	private bool logic_uScriptAct_SetBool_Out_150 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_150 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_150 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_151 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_151 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_151;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_151 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_151;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_151 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_151 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_151 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_151 = true;

	private uScript_PausePopulation logic_uScript_PausePopulation_uScript_PausePopulation_155 = new uScript_PausePopulation();

	private bool logic_uScript_PausePopulation_Out_155 = true;

	private uScriptCon_CompareInt logic_uScriptCon_CompareInt_uScriptCon_CompareInt_157 = new uScriptCon_CompareInt();

	private int logic_uScriptCon_CompareInt_A_157;

	private int logic_uScriptCon_CompareInt_B_157;

	private bool logic_uScriptCon_CompareInt_GreaterThan_157 = true;

	private bool logic_uScriptCon_CompareInt_GreaterThanOrEqualTo_157 = true;

	private bool logic_uScriptCon_CompareInt_EqualTo_157 = true;

	private bool logic_uScriptCon_CompareInt_NotEqualTo_157 = true;

	private bool logic_uScriptCon_CompareInt_LessThanOrEqualTo_157 = true;

	private bool logic_uScriptCon_CompareInt_LessThan_157 = true;

	private uScript_LockTech logic_uScript_LockTech_uScript_LockTech_160 = new uScript_LockTech();

	private Tank logic_uScript_LockTech_tech_160;

	private uScript_LockTech.TechLockType logic_uScript_LockTech_lockType_160 = uScript_LockTech.TechLockType.LockAll;

	private bool logic_uScript_LockTech_Out_160 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_161 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_161;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_161;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_161;

	private bool logic_uScript_AddMessage_Out_161 = true;

	private bool logic_uScript_AddMessage_Shown_161 = true;

	private uScript_GetCircuitChargeInfo logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_162 = new uScript_GetCircuitChargeInfo();

	private TankBlock logic_uScript_GetCircuitChargeInfo_block_162;

	private Tank logic_uScript_GetCircuitChargeInfo_tech_162;

	private BlockTypes logic_uScript_GetCircuitChargeInfo_blockType_162;

	private int logic_uScript_GetCircuitChargeInfo_Return_162;

	private bool logic_uScript_GetCircuitChargeInfo_Out_162 = true;

	private bool logic_uScript_GetCircuitChargeInfo_HasValidBlock_162 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_163 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_163 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_165 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_165 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_165;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_165 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_165;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_165 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_165 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_165 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_165 = true;

	private uScript_SetTankInvulnerable logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_168 = new uScript_SetTankInvulnerable();

	private bool logic_uScript_SetTankInvulnerable_invulnerable_168 = true;

	private Tank logic_uScript_SetTankInvulnerable_tank_168;

	private bool logic_uScript_SetTankInvulnerable_Out_168 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_169 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_169;

	private bool logic_uScriptAct_SetBool_Out_169 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_169 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_169 = true;

	private uScript_LockTech logic_uScript_LockTech_uScript_LockTech_172 = new uScript_LockTech();

	private Tank logic_uScript_LockTech_tech_172;

	private uScript_LockTech.TechLockType logic_uScript_LockTech_lockType_172 = uScript_LockTech.TechLockType.LockAll;

	private bool logic_uScript_LockTech_Out_172 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_174 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_174;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_174;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_174;

	private bool logic_uScript_AddMessage_Out_174 = true;

	private bool logic_uScript_AddMessage_Shown_174 = true;

	private uScript_LockTechInteraction logic_uScript_LockTechInteraction_uScript_LockTechInteraction_176 = new uScript_LockTechInteraction();

	private Tank logic_uScript_LockTechInteraction_tech_176;

	private BlockTypes[] logic_uScript_LockTechInteraction_excludedBlocks_176 = new BlockTypes[0];

	private TankBlock[] logic_uScript_LockTechInteraction_excludedUniqueBlocks_176 = new TankBlock[0];

	private bool logic_uScript_LockTechInteraction_Out_176 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_183 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_183;

	private bool logic_uScriptCon_CompareBool_True_183 = true;

	private bool logic_uScriptCon_CompareBool_False_183 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_186 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_186;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_186;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_186;

	private bool logic_uScript_AddMessage_Out_186 = true;

	private bool logic_uScript_AddMessage_Shown_186 = true;

	private uScript_IsPlayerInRangeOfTech logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_187 = new uScript_IsPlayerInRangeOfTech();

	private Tank logic_uScript_IsPlayerInRangeOfTech_tech_187;

	private float logic_uScript_IsPlayerInRangeOfTech_range_187;

	private Tank[] logic_uScript_IsPlayerInRangeOfTech_techs_187 = new Tank[0];

	private bool logic_uScript_IsPlayerInRangeOfTech_Out_187 = true;

	private bool logic_uScript_IsPlayerInRangeOfTech_InRange_187 = true;

	private bool logic_uScript_IsPlayerInRangeOfTech_OutOfRange_187 = true;

	private SubGraph_SaveLoadInt logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_189 = new SubGraph_SaveLoadInt();

	private int logic_SubGraph_SaveLoadInt_restartValue_189 = 1;

	private int logic_SubGraph_SaveLoadInt_integer_189;

	private object logic_SubGraph_SaveLoadInt_intAsVariable_189 = "";

	private string logic_SubGraph_SaveLoadInt_uniqueID_189 = "Stage3QuestionTwo";

	private uScript_GetCircuitChargeInfo logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_190 = new uScript_GetCircuitChargeInfo();

	private TankBlock logic_uScript_GetCircuitChargeInfo_block_190;

	private Tank logic_uScript_GetCircuitChargeInfo_tech_190;

	private BlockTypes logic_uScript_GetCircuitChargeInfo_blockType_190;

	private int logic_uScript_GetCircuitChargeInfo_Return_190;

	private bool logic_uScript_GetCircuitChargeInfo_Out_190 = true;

	private bool logic_uScript_GetCircuitChargeInfo_HasValidBlock_190 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_193 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_193;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_193;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_193;

	private bool logic_uScript_AddMessage_Out_193 = true;

	private bool logic_uScript_AddMessage_Shown_193 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_194 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_194 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_194;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_194 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_194;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_194 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_194 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_194 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_194 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_198 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_198;

	private bool logic_uScriptCon_CompareBool_True_198 = true;

	private bool logic_uScriptCon_CompareBool_False_198 = true;

	private uScript_GetCircuitChargeInfo logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_199 = new uScript_GetCircuitChargeInfo();

	private TankBlock logic_uScript_GetCircuitChargeInfo_block_199;

	private Tank logic_uScript_GetCircuitChargeInfo_tech_199;

	private BlockTypes logic_uScript_GetCircuitChargeInfo_blockType_199;

	private int logic_uScript_GetCircuitChargeInfo_Return_199;

	private bool logic_uScript_GetCircuitChargeInfo_Out_199 = true;

	private bool logic_uScript_GetCircuitChargeInfo_HasValidBlock_199 = true;

	private uScript_GetCircuitChargeInfo logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_201 = new uScript_GetCircuitChargeInfo();

	private TankBlock logic_uScript_GetCircuitChargeInfo_block_201;

	private Tank logic_uScript_GetCircuitChargeInfo_tech_201;

	private BlockTypes logic_uScript_GetCircuitChargeInfo_blockType_201;

	private int logic_uScript_GetCircuitChargeInfo_Return_201;

	private bool logic_uScript_GetCircuitChargeInfo_Out_201 = true;

	private bool logic_uScript_GetCircuitChargeInfo_HasValidBlock_201 = true;

	private uScript_GetTankBlock logic_uScript_GetTankBlock_uScript_GetTankBlock_206 = new uScript_GetTankBlock();

	private Tank logic_uScript_GetTankBlock_tank_206;

	private BlockTypes logic_uScript_GetTankBlock_blockType_206;

	private TankBlock logic_uScript_GetTankBlock_Return_206;

	private bool logic_uScript_GetTankBlock_Out_206 = true;

	private bool logic_uScript_GetTankBlock_Returned_206 = true;

	private bool logic_uScript_GetTankBlock_NotFound_206 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_208 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_208 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_211 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_211 = new Tank[0];

	private int logic_uScript_AccessListTech_index_211;

	private Tank logic_uScript_AccessListTech_value_211;

	private bool logic_uScript_AccessListTech_Out_211 = true;

	private uScript_SetEncounterTarget logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_212 = new uScript_SetEncounterTarget();

	private GameObject logic_uScript_SetEncounterTarget_owner_212;

	private object logic_uScript_SetEncounterTarget_visibleObject_212 = "";

	private bool logic_uScript_SetEncounterTarget_Out_212 = true;

	private uScriptCon_CompareInt logic_uScriptCon_CompareInt_uScriptCon_CompareInt_215 = new uScriptCon_CompareInt();

	private int logic_uScriptCon_CompareInt_A_215;

	private int logic_uScriptCon_CompareInt_B_215;

	private bool logic_uScriptCon_CompareInt_GreaterThan_215 = true;

	private bool logic_uScriptCon_CompareInt_GreaterThanOrEqualTo_215 = true;

	private bool logic_uScriptCon_CompareInt_EqualTo_215 = true;

	private bool logic_uScriptCon_CompareInt_NotEqualTo_215 = true;

	private bool logic_uScriptCon_CompareInt_LessThanOrEqualTo_215 = true;

	private bool logic_uScriptCon_CompareInt_LessThan_215 = true;

	private uScript_GetCircuitChargeInfo logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_217 = new uScript_GetCircuitChargeInfo();

	private TankBlock logic_uScript_GetCircuitChargeInfo_block_217;

	private Tank logic_uScript_GetCircuitChargeInfo_tech_217;

	private BlockTypes logic_uScript_GetCircuitChargeInfo_blockType_217;

	private int logic_uScript_GetCircuitChargeInfo_Return_217;

	private bool logic_uScript_GetCircuitChargeInfo_Out_217 = true;

	private bool logic_uScript_GetCircuitChargeInfo_HasValidBlock_217 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_218 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_218;

	private bool logic_uScriptCon_CompareBool_True_218 = true;

	private bool logic_uScriptCon_CompareBool_False_218 = true;

	private uScript_LockTech logic_uScript_LockTech_uScript_LockTech_219 = new uScript_LockTech();

	private Tank logic_uScript_LockTech_tech_219;

	private uScript_LockTech.TechLockType logic_uScript_LockTech_lockType_219 = uScript_LockTech.TechLockType.LockAll;

	private bool logic_uScript_LockTech_Out_219 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_224 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_224 = new Tank[0];

	private int logic_uScript_AccessListTech_index_224;

	private Tank logic_uScript_AccessListTech_value_224;

	private bool logic_uScript_AccessListTech_Out_224 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_226 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_226;

	private bool logic_uScriptAct_SetBool_Out_226 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_226 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_226 = true;

	private uScript_GetCircuitChargeInfo logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_227 = new uScript_GetCircuitChargeInfo();

	private TankBlock logic_uScript_GetCircuitChargeInfo_block_227;

	private Tank logic_uScript_GetCircuitChargeInfo_tech_227;

	private BlockTypes logic_uScript_GetCircuitChargeInfo_blockType_227;

	private int logic_uScript_GetCircuitChargeInfo_Return_227;

	private bool logic_uScript_GetCircuitChargeInfo_Out_227 = true;

	private bool logic_uScript_GetCircuitChargeInfo_HasValidBlock_227 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_228 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_228 = new Tank[0];

	private int logic_uScript_AccessListTech_index_228;

	private Tank logic_uScript_AccessListTech_value_228;

	private bool logic_uScript_AccessListTech_Out_228 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_229 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_229 = 1;

	private int logic_uScriptAct_SetInt_Target_229;

	private bool logic_uScriptAct_SetInt_Out_229 = true;

	private uScriptCon_ManualSwitch logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_233 = new uScriptCon_ManualSwitch();

	private int logic_uScriptCon_ManualSwitch_CurrentOutput_233;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_236 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_236 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_236;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_236 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_236;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_236 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_236 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_236 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_236 = true;

	private uScript_SetTankInvulnerable logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_237 = new uScript_SetTankInvulnerable();

	private bool logic_uScript_SetTankInvulnerable_invulnerable_237 = true;

	private Tank logic_uScript_SetTankInvulnerable_tank_237;

	private bool logic_uScript_SetTankInvulnerable_Out_237 = true;

	private uScript_SetTankHideBlockLimit logic_uScript_SetTankHideBlockLimit_uScript_SetTankHideBlockLimit_238 = new uScript_SetTankHideBlockLimit();

	private bool logic_uScript_SetTankHideBlockLimit_hidden_238 = true;

	private Tank logic_uScript_SetTankHideBlockLimit_tech_238;

	private bool logic_uScript_SetTankHideBlockLimit_Out_238 = true;

	private uScript_SetTankHideBlockLimit logic_uScript_SetTankHideBlockLimit_uScript_SetTankHideBlockLimit_240 = new uScript_SetTankHideBlockLimit();

	private bool logic_uScript_SetTankHideBlockLimit_hidden_240 = true;

	private Tank logic_uScript_SetTankHideBlockLimit_tech_240;

	private bool logic_uScript_SetTankHideBlockLimit_Out_240 = true;

	private uScript_RemoveScenery logic_uScript_RemoveScenery_uScript_RemoveScenery_241 = new uScript_RemoveScenery();

	private GameObject logic_uScript_RemoveScenery_ownerNode_241;

	private string logic_uScript_RemoveScenery_positionName_241 = "";

	private float logic_uScript_RemoveScenery_radius_241;

	private bool logic_uScript_RemoveScenery_preventChunksSpawning_241 = true;

	private bool logic_uScript_RemoveScenery_Out_241 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_244 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_244;

	private bool logic_uScriptAct_SetBool_Out_244 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_244 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_244 = true;

	private uScript_LockTechInteraction logic_uScript_LockTechInteraction_uScript_LockTechInteraction_245 = new uScript_LockTechInteraction();

	private Tank logic_uScript_LockTechInteraction_tech_245;

	private BlockTypes[] logic_uScript_LockTechInteraction_excludedBlocks_245 = new BlockTypes[0];

	private TankBlock[] logic_uScript_LockTechInteraction_excludedUniqueBlocks_245 = new TankBlock[0];

	private bool logic_uScript_LockTechInteraction_Out_245 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_248 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_248 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_255 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_255 = new Tank[0];

	private int logic_uScript_AccessListTech_index_255;

	private Tank logic_uScript_AccessListTech_value_255;

	private bool logic_uScript_AccessListTech_Out_255 = true;

	private uScript_LockTech logic_uScript_LockTech_uScript_LockTech_259 = new uScript_LockTech();

	private Tank logic_uScript_LockTech_tech_259;

	private uScript_LockTech.TechLockType logic_uScript_LockTech_lockType_259 = uScript_LockTech.TechLockType.LockAll;

	private bool logic_uScript_LockTech_Out_259 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_260 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_260;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_260;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_260;

	private bool logic_uScript_AddMessage_Out_260 = true;

	private bool logic_uScript_AddMessage_Shown_260 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_261 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_261;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_261;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_261;

	private bool logic_uScript_AddMessage_Out_261 = true;

	private bool logic_uScript_AddMessage_Shown_261 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_262 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_262;

	private bool logic_uScriptCon_CompareBool_True_262 = true;

	private bool logic_uScriptCon_CompareBool_False_262 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_264 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_264;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_264;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_264;

	private bool logic_uScript_AddMessage_Out_264 = true;

	private bool logic_uScript_AddMessage_Shown_264 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_266 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_266;

	private bool logic_uScriptCon_CompareBool_True_266 = true;

	private bool logic_uScriptCon_CompareBool_False_266 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_267 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_267 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_269 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_269;

	private bool logic_uScriptAct_SetBool_Out_269 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_269 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_269 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_274 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_274;

	private bool logic_uScriptAct_SetBool_Out_274 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_274 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_274 = true;

	private uScriptCon_CompareInt logic_uScriptCon_CompareInt_uScriptCon_CompareInt_277 = new uScriptCon_CompareInt();

	private int logic_uScriptCon_CompareInt_A_277;

	private int logic_uScriptCon_CompareInt_B_277;

	private bool logic_uScriptCon_CompareInt_GreaterThan_277 = true;

	private bool logic_uScriptCon_CompareInt_GreaterThanOrEqualTo_277 = true;

	private bool logic_uScriptCon_CompareInt_EqualTo_277 = true;

	private bool logic_uScriptCon_CompareInt_NotEqualTo_277 = true;

	private bool logic_uScriptCon_CompareInt_LessThanOrEqualTo_277 = true;

	private bool logic_uScriptCon_CompareInt_LessThan_277 = true;

	private SubGraph_SaveLoadInt logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_279 = new SubGraph_SaveLoadInt();

	private int logic_SubGraph_SaveLoadInt_restartValue_279 = 1;

	private int logic_SubGraph_SaveLoadInt_integer_279;

	private object logic_SubGraph_SaveLoadInt_intAsVariable_279 = "";

	private string logic_SubGraph_SaveLoadInt_uniqueID_279 = "Stage";

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_284 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_284 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_285 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_285 = 2;

	private int logic_uScriptAct_SetInt_Target_285;

	private bool logic_uScriptAct_SetInt_Out_285 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_286 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_286;

	private bool logic_uScriptAct_SetBool_Out_286 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_286 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_286 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_287 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_287 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_287;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_287 = 0.3f;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_287;

	private bool logic_uScript_SpawnTechsFromData_Out_287 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_288 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_288 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_291 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_291;

	private bool logic_uScriptCon_CompareBool_True_291 = true;

	private bool logic_uScriptCon_CompareBool_False_291 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_293 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_293;

	private bool logic_uScriptAct_SetBool_Out_293 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_293 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_293 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_294 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_294;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_294;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_294;

	private bool logic_uScript_AddMessage_Out_294 = true;

	private bool logic_uScript_AddMessage_Shown_294 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_295 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_295 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_296 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_296;

	private bool logic_uScriptAct_SetBool_Out_296 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_296 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_296 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_300 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_300;

	private bool logic_uScriptAct_SetBool_Out_300 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_300 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_300 = true;

	private SubGraph_SaveLoadInt logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_302 = new SubGraph_SaveLoadInt();

	private int logic_SubGraph_SaveLoadInt_restartValue_302 = 1;

	private int logic_SubGraph_SaveLoadInt_integer_302;

	private object logic_SubGraph_SaveLoadInt_intAsVariable_302 = "";

	private string logic_SubGraph_SaveLoadInt_uniqueID_302 = "Stage2QuestionOne";

	private SubGraph_CompleteObjectiveStage logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_308 = new SubGraph_CompleteObjectiveStage();

	private int logic_SubGraph_CompleteObjectiveStage_objectiveStage_308;

	private bool logic_SubGraph_CompleteObjectiveStage_isFinalObjective_308;

	private uScript_DirectEnemiesOutOfEncounter logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_311 = new uScript_DirectEnemiesOutOfEncounter();

	private GameObject logic_uScript_DirectEnemiesOutOfEncounter_ownerNode_311;

	private bool logic_uScript_DirectEnemiesOutOfEncounter_Out_311 = true;

	private uScript_Wait logic_uScript_Wait_uScript_Wait_313 = new uScript_Wait();

	private float logic_uScript_Wait_seconds_313;

	private bool logic_uScript_Wait_repeat_313 = true;

	private bool logic_uScript_Wait_Waited_313 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_314 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_314 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_314;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_314 = 0.3f;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_314;

	private bool logic_uScript_SpawnTechsFromData_Out_314 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_315 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_315 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_315;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_315 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_315;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_315 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_315 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_315 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_315 = true;

	private uScriptCon_ManualSwitch logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_316 = new uScriptCon_ManualSwitch();

	private int logic_uScriptCon_ManualSwitch_CurrentOutput_316;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_318 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_318 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_318;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_318 = 0.3f;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_318;

	private bool logic_uScript_SpawnTechsFromData_Out_318 = true;

	private uScript_SetTankHideBlockLimit logic_uScript_SetTankHideBlockLimit_uScript_SetTankHideBlockLimit_319 = new uScript_SetTankHideBlockLimit();

	private bool logic_uScript_SetTankHideBlockLimit_hidden_319 = true;

	private Tank logic_uScript_SetTankHideBlockLimit_tech_319;

	private bool logic_uScript_SetTankHideBlockLimit_Out_319 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_320 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_320;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_320 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_320 = "Initialize";

	private uScript_GetCircuitChargeInfo logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_322 = new uScript_GetCircuitChargeInfo();

	private TankBlock logic_uScript_GetCircuitChargeInfo_block_322;

	private Tank logic_uScript_GetCircuitChargeInfo_tech_322;

	private BlockTypes logic_uScript_GetCircuitChargeInfo_blockType_322;

	private int logic_uScript_GetCircuitChargeInfo_Return_322;

	private bool logic_uScript_GetCircuitChargeInfo_Out_322 = true;

	private bool logic_uScript_GetCircuitChargeInfo_HasValidBlock_322 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_323 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_323;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_323;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_323;

	private bool logic_uScript_AddMessage_Out_323 = true;

	private bool logic_uScript_AddMessage_Shown_323 = true;

	private SubGraph_SaveLoadInt logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_324 = new SubGraph_SaveLoadInt();

	private int logic_SubGraph_SaveLoadInt_restartValue_324 = 1;

	private int logic_SubGraph_SaveLoadInt_integer_324;

	private object logic_SubGraph_SaveLoadInt_intAsVariable_324 = "";

	private string logic_SubGraph_SaveLoadInt_uniqueID_324 = "Stage4QuestionThree";

	private uScript_LockTechInteraction logic_uScript_LockTechInteraction_uScript_LockTechInteraction_326 = new uScript_LockTechInteraction();

	private Tank logic_uScript_LockTechInteraction_tech_326;

	private BlockTypes[] logic_uScript_LockTechInteraction_excludedBlocks_326 = new BlockTypes[0];

	private TankBlock[] logic_uScript_LockTechInteraction_excludedUniqueBlocks_326 = new TankBlock[0];

	private bool logic_uScript_LockTechInteraction_Out_326 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_328 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_328 = true;

	private uScriptCon_ManualSwitch logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_331 = new uScriptCon_ManualSwitch();

	private int logic_uScriptCon_ManualSwitch_CurrentOutput_331;

	private uScript_SetTankInvulnerable logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_335 = new uScript_SetTankInvulnerable();

	private bool logic_uScript_SetTankInvulnerable_invulnerable_335 = true;

	private Tank logic_uScript_SetTankInvulnerable_tank_335;

	private bool logic_uScript_SetTankInvulnerable_Out_335 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_339 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_339;

	private bool logic_uScriptAct_SetBool_Out_339 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_339 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_339 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_340 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_340;

	private bool logic_uScriptCon_CompareBool_True_340 = true;

	private bool logic_uScriptCon_CompareBool_False_340 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_341 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_341 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_341;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_341 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_341;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_341 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_341 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_341 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_341 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_343 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_343 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_343;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_343;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_343 = true;

	private bool logic_uScript_SpawnTechsFromData_Out_343 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_345 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_345;

	private bool logic_uScriptAct_SetBool_Out_345 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_345 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_345 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_347 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_347;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_347 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_347 = "Q1EnemyAlive";

	private uScriptAct_Concatenate logic_uScriptAct_Concatenate_uScriptAct_Concatenate_349 = new uScriptAct_Concatenate();

	private object[] logic_uScriptAct_Concatenate_A_349 = new object[0];

	private object[] logic_uScriptAct_Concatenate_B_349 = new object[0];

	private string logic_uScriptAct_Concatenate_Separator_349 = "";

	private string logic_uScriptAct_Concatenate_Result_349;

	private bool logic_uScriptAct_Concatenate_Out_349 = true;

	private uScriptAct_PrintText logic_uScriptAct_PrintText_uScriptAct_PrintText_351 = new uScriptAct_PrintText();

	private string logic_uScriptAct_PrintText_Text_351 = "";

	private int logic_uScriptAct_PrintText_FontSize_351 = 16;

	private FontStyle logic_uScriptAct_PrintText_FontStyle_351;

	private Color logic_uScriptAct_PrintText_FontColor_351 = new Color(0f, 0f, 0f, 1f);

	private TextAnchor logic_uScriptAct_PrintText_textAnchor_351;

	private int logic_uScriptAct_PrintText_EdgePadding_351 = 8;

	private float logic_uScriptAct_PrintText_time_351;

	private bool logic_uScriptAct_PrintText_Out_351 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_354 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_354 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_354;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_354;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_354 = true;

	private bool logic_uScript_SpawnTechsFromData_Out_354 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_355 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_355;

	private bool logic_uScriptAct_SetBool_Out_355 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_355 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_355 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_356 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_356 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_356;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_356 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_356;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_356 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_356 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_356 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_356 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_360 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_360;

	private bool logic_uScriptAct_SetBool_Out_360 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_360 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_360 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_361 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_361;

	private bool logic_uScriptAct_SetBool_Out_361 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_361 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_361 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_363 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_363;

	private bool logic_uScriptCon_CompareBool_True_363 = true;

	private bool logic_uScriptCon_CompareBool_False_363 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_372 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_372;

	private bool logic_uScriptCon_CompareBool_True_372 = true;

	private bool logic_uScriptCon_CompareBool_False_372 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_375 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_375;

	private bool logic_uScriptAct_SetBool_Out_375 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_375 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_375 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_377 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_377 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_377;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_377;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_377 = true;

	private bool logic_uScript_SpawnTechsFromData_Out_377 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_378 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_378;

	private bool logic_uScriptAct_SetBool_Out_378 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_378 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_378 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_381 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_381 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_381;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_381 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_381;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_381 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_381 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_381 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_381 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_382 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_382;

	private bool logic_uScriptAct_SetBool_Out_382 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_382 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_382 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_386 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_386;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_386 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_386 = "Q2EnemyAlive";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_388 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_388;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_388 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_388 = "Q3EnemyAlive";

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_390 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_390;

	private bool logic_uScriptAct_SetBool_Out_390 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_390 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_390 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_392 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_392;

	private bool logic_uScriptAct_SetBool_Out_392 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_392 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_392 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_393 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_393;

	private bool logic_uScriptAct_SetBool_Out_393 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_393 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_393 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_396 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_396 = 2;

	private int logic_uScriptAct_SetInt_Target_396;

	private bool logic_uScriptAct_SetInt_Out_396 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_405 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_405;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_405;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_405;

	private bool logic_uScript_AddMessage_Out_405 = true;

	private bool logic_uScript_AddMessage_Shown_405 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_409 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_409;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_409;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_409;

	private bool logic_uScript_AddMessage_Out_409 = true;

	private bool logic_uScript_AddMessage_Shown_409 = true;

	private uScriptAct_AddInt_v2 logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_410 = new uScriptAct_AddInt_v2();

	private int logic_uScriptAct_AddInt_v2_A_410;

	private int logic_uScriptAct_AddInt_v2_B_410 = 1;

	private int logic_uScriptAct_AddInt_v2_IntResult_410;

	private float logic_uScriptAct_AddInt_v2_FloatResult_410;

	private bool logic_uScriptAct_AddInt_v2_Out_410 = true;

	private uScript_Wait logic_uScript_Wait_uScript_Wait_412 = new uScript_Wait();

	private float logic_uScript_Wait_seconds_412;

	private bool logic_uScript_Wait_repeat_412 = true;

	private bool logic_uScript_Wait_Waited_412 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_415 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_415;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_415;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_415;

	private bool logic_uScript_AddMessage_Out_415 = true;

	private bool logic_uScript_AddMessage_Shown_415 = true;

	private uScript_Wait logic_uScript_Wait_uScript_Wait_418 = new uScript_Wait();

	private float logic_uScript_Wait_seconds_418;

	private bool logic_uScript_Wait_repeat_418 = true;

	private bool logic_uScript_Wait_Waited_418 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_420 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_420 = 1;

	private int logic_uScriptAct_SetInt_Target_420;

	private bool logic_uScriptAct_SetInt_Out_420 = true;

	private uScriptAct_AddInt_v2 logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_421 = new uScriptAct_AddInt_v2();

	private int logic_uScriptAct_AddInt_v2_A_421;

	private int logic_uScriptAct_AddInt_v2_B_421 = 1;

	private int logic_uScriptAct_AddInt_v2_IntResult_421;

	private float logic_uScriptAct_AddInt_v2_FloatResult_421;

	private bool logic_uScriptAct_AddInt_v2_Out_421 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_425 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_425;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_425;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_425;

	private bool logic_uScript_AddMessage_Out_425 = true;

	private bool logic_uScript_AddMessage_Shown_425 = true;

	private uScript_Wait logic_uScript_Wait_uScript_Wait_426 = new uScript_Wait();

	private float logic_uScript_Wait_seconds_426;

	private bool logic_uScript_Wait_repeat_426 = true;

	private bool logic_uScript_Wait_Waited_426 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_428 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_428;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_428;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_428;

	private bool logic_uScript_AddMessage_Out_428 = true;

	private bool logic_uScript_AddMessage_Shown_428 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_431 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_431 = 1;

	private int logic_uScriptAct_SetInt_Target_431;

	private bool logic_uScriptAct_SetInt_Out_431 = true;

	private uScriptAct_AddInt_v2 logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_433 = new uScriptAct_AddInt_v2();

	private int logic_uScriptAct_AddInt_v2_A_433;

	private int logic_uScriptAct_AddInt_v2_B_433 = 1;

	private int logic_uScriptAct_AddInt_v2_IntResult_433;

	private float logic_uScriptAct_AddInt_v2_FloatResult_433;

	private bool logic_uScriptAct_AddInt_v2_Out_433 = true;

	private uScript_RemoveTech logic_uScript_RemoveTech_uScript_RemoveTech_436 = new uScript_RemoveTech();

	private Tank logic_uScript_RemoveTech_tech_436;

	private bool logic_uScript_RemoveTech_Out_436 = true;

	private uScript_SpawnVFX logic_uScript_SpawnVFX_uScript_SpawnVFX_441 = new uScript_SpawnVFX();

	private GameObject logic_uScript_SpawnVFX_ownerNode_441;

	private Transform logic_uScript_SpawnVFX_vfxToSpawn_441;

	private string logic_uScript_SpawnVFX_spawnPosName_441 = "";

	private bool logic_uScript_SpawnVFX_Out_441 = true;

	private uScript_SpawnVFX logic_uScript_SpawnVFX_uScript_SpawnVFX_446 = new uScript_SpawnVFX();

	private GameObject logic_uScript_SpawnVFX_ownerNode_446;

	private Transform logic_uScript_SpawnVFX_vfxToSpawn_446;

	private string logic_uScript_SpawnVFX_spawnPosName_446 = "";

	private bool logic_uScript_SpawnVFX_Out_446 = true;

	private uScript_RemoveTech logic_uScript_RemoveTech_uScript_RemoveTech_452 = new uScript_RemoveTech();

	private Tank logic_uScript_RemoveTech_tech_452;

	private bool logic_uScript_RemoveTech_Out_452 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_454 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_454;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_454;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_454;

	private bool logic_uScript_AddMessage_Out_454 = true;

	private bool logic_uScript_AddMessage_Shown_454 = true;

	private uScript_RemoveTech logic_uScript_RemoveTech_uScript_RemoveTech_455 = new uScript_RemoveTech();

	private Tank logic_uScript_RemoveTech_tech_455;

	private bool logic_uScript_RemoveTech_Out_455 = true;

	private uScript_SpawnVFX logic_uScript_SpawnVFX_uScript_SpawnVFX_458 = new uScript_SpawnVFX();

	private GameObject logic_uScript_SpawnVFX_ownerNode_458;

	private Transform logic_uScript_SpawnVFX_vfxToSpawn_458;

	private string logic_uScript_SpawnVFX_spawnPosName_458 = "";

	private bool logic_uScript_SpawnVFX_Out_458 = true;

	private uScript_FlyTechUpAndAway logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_460 = new uScript_FlyTechUpAndAway();

	private Tank logic_uScript_FlyTechUpAndAway_tech_460;

	private float logic_uScript_FlyTechUpAndAway_maxLifetime_460 = 5f;

	private float logic_uScript_FlyTechUpAndAway_targetHeight_460 = 150f;

	private ExternalBehaviorTree logic_uScript_FlyTechUpAndAway_aiTree_460;

	private Transform logic_uScript_FlyTechUpAndAway_removalParticles_460;

	private bool logic_uScript_FlyTechUpAndAway_Out_460 = true;

	private uScript_FinishEncounter logic_uScript_FinishEncounter_uScript_FinishEncounter_465 = new uScript_FinishEncounter();

	private GameObject logic_uScript_FinishEncounter_owner_465;

	private bool logic_uScript_FinishEncounter_Out_465 = true;

	private uScript_RemoveTech logic_uScript_RemoveTech_uScript_RemoveTech_466 = new uScript_RemoveTech();

	private Tank logic_uScript_RemoveTech_tech_466;

	private bool logic_uScript_RemoveTech_Out_466 = true;

	private uScript_SpawnVFX logic_uScript_SpawnVFX_uScript_SpawnVFX_467 = new uScript_SpawnVFX();

	private GameObject logic_uScript_SpawnVFX_ownerNode_467;

	private Transform logic_uScript_SpawnVFX_vfxToSpawn_467;

	private string logic_uScript_SpawnVFX_spawnPosName_467 = "";

	private bool logic_uScript_SpawnVFX_Out_467 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_468 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_468 = 5;

	private int logic_uScriptAct_SetInt_Target_468;

	private bool logic_uScriptAct_SetInt_Out_468 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_471 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_471;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_471 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_471 = "Q1Wrong Answers";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_473 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_473;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_473 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_473 = "Q2Wrong Answers";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_474 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_474;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_474 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_474 = "Q3Wrong Answers";

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_476 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_476;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_476;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_476;

	private bool logic_uScript_AddMessage_Out_476 = true;

	private bool logic_uScript_AddMessage_Shown_476 = true;

	private SubGraph_CompleteObjectiveStage logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_481 = new SubGraph_CompleteObjectiveStage();

	private int logic_SubGraph_CompleteObjectiveStage_objectiveStage_481;

	private bool logic_SubGraph_CompleteObjectiveStage_isFinalObjective_481;

	private SubGraph_LoadObjectiveStates logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_484 = new SubGraph_LoadObjectiveStates();

	private int logic_SubGraph_LoadObjectiveStates_currentObjective_484;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_486 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_486;

	private bool logic_uScriptAct_SetBool_Out_486 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_486 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_486 = true;

	private uScript_IsPlayerInRangeOfTech logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_487 = new uScript_IsPlayerInRangeOfTech();

	private Tank logic_uScript_IsPlayerInRangeOfTech_tech_487;

	private float logic_uScript_IsPlayerInRangeOfTech_range_487 = 100f;

	private Tank[] logic_uScript_IsPlayerInRangeOfTech_techs_487 = new Tank[0];

	private bool logic_uScript_IsPlayerInRangeOfTech_Out_487 = true;

	private bool logic_uScript_IsPlayerInRangeOfTech_InRange_487 = true;

	private bool logic_uScript_IsPlayerInRangeOfTech_OutOfRange_487 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_490 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_490;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_490 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_490 = "MsgBaseFound";

	private SubGraph_AddMessageWithPadSupport logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_1062 = new SubGraph_AddMessageWithPadSupport();

	private uScript_AddMessage.MessageData logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboard_1062;

	private uScript_AddMessage.MessageData logic_SubGraph_AddMessageWithPadSupport_messageControlPad_1062;

	private uScript_AddMessage.MessageSpeaker logic_SubGraph_AddMessageWithPadSupport_speaker_1062;

	private ManOnScreenMessages.OnScreenMessage logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboardReturn_1062;

	private ManOnScreenMessages.OnScreenMessage logic_SubGraph_AddMessageWithPadSupport_messageControlPadReturn_1062;

	private void SyncUnityHooks()
	{
		SyncEventListeners();
		if (null == owner_Connection_3 || !m_RegisteredForEvents)
		{
			owner_Connection_3 = parentGameObject;
		}
		if (null == owner_Connection_29 || !m_RegisteredForEvents)
		{
			owner_Connection_29 = parentGameObject;
		}
		if (null == owner_Connection_37 || !m_RegisteredForEvents)
		{
			owner_Connection_37 = parentGameObject;
			if (null != owner_Connection_37)
			{
				uScript_EncounterUpdate uScript_EncounterUpdate2 = owner_Connection_37.GetComponent<uScript_EncounterUpdate>();
				if (null == uScript_EncounterUpdate2)
				{
					uScript_EncounterUpdate2 = owner_Connection_37.AddComponent<uScript_EncounterUpdate>();
				}
				if (null != uScript_EncounterUpdate2)
				{
					uScript_EncounterUpdate2.OnUpdate += Instance_OnUpdate_70;
					uScript_EncounterUpdate2.OnSuspend += Instance_OnSuspend_70;
					uScript_EncounterUpdate2.OnResume += Instance_OnResume_70;
				}
			}
		}
		if (null == owner_Connection_45 || !m_RegisteredForEvents)
		{
			owner_Connection_45 = parentGameObject;
		}
		if (null == owner_Connection_74 || !m_RegisteredForEvents)
		{
			owner_Connection_74 = parentGameObject;
		}
		if (null == owner_Connection_77 || !m_RegisteredForEvents)
		{
			owner_Connection_77 = parentGameObject;
		}
		if (null == owner_Connection_78 || !m_RegisteredForEvents)
		{
			owner_Connection_78 = parentGameObject;
		}
		if (null == owner_Connection_79 || !m_RegisteredForEvents)
		{
			owner_Connection_79 = parentGameObject;
		}
		if (null == owner_Connection_110 || !m_RegisteredForEvents)
		{
			owner_Connection_110 = parentGameObject;
		}
		if (null == owner_Connection_192 || !m_RegisteredForEvents)
		{
			owner_Connection_192 = parentGameObject;
		}
		if (null == owner_Connection_207 || !m_RegisteredForEvents)
		{
			owner_Connection_207 = parentGameObject;
			if (null != owner_Connection_207)
			{
				uScript_SaveLoad uScript_SaveLoad2 = owner_Connection_207.GetComponent<uScript_SaveLoad>();
				if (null == uScript_SaveLoad2)
				{
					uScript_SaveLoad2 = owner_Connection_207.AddComponent<uScript_SaveLoad>();
				}
				if (null != uScript_SaveLoad2)
				{
					uScript_SaveLoad2.SaveEvent += Instance_SaveEvent_182;
					uScript_SaveLoad2.LoadEvent += Instance_LoadEvent_182;
					uScript_SaveLoad2.RestartEvent += Instance_RestartEvent_182;
				}
			}
		}
		if (null == owner_Connection_251 || !m_RegisteredForEvents)
		{
			owner_Connection_251 = parentGameObject;
		}
		if (null == owner_Connection_268 || !m_RegisteredForEvents)
		{
			owner_Connection_268 = parentGameObject;
		}
		if (null == owner_Connection_272 || !m_RegisteredForEvents)
		{
			owner_Connection_272 = parentGameObject;
		}
		if (null == owner_Connection_305 || !m_RegisteredForEvents)
		{
			owner_Connection_305 = parentGameObject;
		}
		if (null == owner_Connection_338 || !m_RegisteredForEvents)
		{
			owner_Connection_338 = parentGameObject;
		}
		if (null == owner_Connection_344 || !m_RegisteredForEvents)
		{
			owner_Connection_344 = parentGameObject;
		}
		if (null == owner_Connection_357 || !m_RegisteredForEvents)
		{
			owner_Connection_357 = parentGameObject;
		}
		if (null == owner_Connection_362 || !m_RegisteredForEvents)
		{
			owner_Connection_362 = parentGameObject;
		}
		if (null == owner_Connection_370 || !m_RegisteredForEvents)
		{
			owner_Connection_370 = parentGameObject;
		}
		if (null == owner_Connection_379 || !m_RegisteredForEvents)
		{
			owner_Connection_379 = parentGameObject;
		}
		if (null == owner_Connection_435 || !m_RegisteredForEvents)
		{
			owner_Connection_435 = parentGameObject;
		}
		if (null == owner_Connection_444 || !m_RegisteredForEvents)
		{
			owner_Connection_444 = parentGameObject;
		}
		if (null == owner_Connection_447 || !m_RegisteredForEvents)
		{
			owner_Connection_447 = parentGameObject;
		}
		if (null == owner_Connection_449 || !m_RegisteredForEvents)
		{
			owner_Connection_449 = parentGameObject;
		}
		if (null == owner_Connection_451 || !m_RegisteredForEvents)
		{
			owner_Connection_451 = parentGameObject;
		}
	}

	private void RegisterForUnityHooks()
	{
		SyncEventListeners();
		if (!m_RegisteredForEvents && null != owner_Connection_37)
		{
			uScript_EncounterUpdate uScript_EncounterUpdate2 = owner_Connection_37.GetComponent<uScript_EncounterUpdate>();
			if (null == uScript_EncounterUpdate2)
			{
				uScript_EncounterUpdate2 = owner_Connection_37.AddComponent<uScript_EncounterUpdate>();
			}
			if (null != uScript_EncounterUpdate2)
			{
				uScript_EncounterUpdate2.OnUpdate += Instance_OnUpdate_70;
				uScript_EncounterUpdate2.OnSuspend += Instance_OnSuspend_70;
				uScript_EncounterUpdate2.OnResume += Instance_OnResume_70;
			}
		}
		if (!m_RegisteredForEvents && null != owner_Connection_207)
		{
			uScript_SaveLoad uScript_SaveLoad2 = owner_Connection_207.GetComponent<uScript_SaveLoad>();
			if (null == uScript_SaveLoad2)
			{
				uScript_SaveLoad2 = owner_Connection_207.AddComponent<uScript_SaveLoad>();
			}
			if (null != uScript_SaveLoad2)
			{
				uScript_SaveLoad2.SaveEvent += Instance_SaveEvent_182;
				uScript_SaveLoad2.LoadEvent += Instance_LoadEvent_182;
				uScript_SaveLoad2.RestartEvent += Instance_RestartEvent_182;
			}
		}
	}

	private void SyncEventListeners()
	{
	}

	private void UnregisterEventListeners()
	{
		if (null != owner_Connection_37)
		{
			uScript_EncounterUpdate component = owner_Connection_37.GetComponent<uScript_EncounterUpdate>();
			if (null != component)
			{
				component.OnUpdate -= Instance_OnUpdate_70;
				component.OnSuspend -= Instance_OnSuspend_70;
				component.OnResume -= Instance_OnResume_70;
			}
		}
		if (null != owner_Connection_207)
		{
			uScript_SaveLoad component2 = owner_Connection_207.GetComponent<uScript_SaveLoad>();
			if (null != component2)
			{
				component2.SaveEvent -= Instance_SaveEvent_182;
				component2.LoadEvent -= Instance_LoadEvent_182;
				component2.RestartEvent -= Instance_RestartEvent_182;
			}
		}
	}

	public override void SetParent(GameObject g)
	{
		parentGameObject = g;
		logic_uScriptAct_SetBool_uScriptAct_SetBool_8.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_12.SetParent(g);
		logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_15.SetParent(g);
		logic_uScript_SetCustomRadarTeamID_uScript_SetCustomRadarTeamID_16.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_17.SetParent(g);
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_20.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_21.SetParent(g);
		logic_uScript_SetCustomRadarTeamID_uScript_SetCustomRadarTeamID_23.SetParent(g);
		logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_24.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_27.SetParent(g);
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_30.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_31.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_35.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_39.SetParent(g);
		logic_uScript_LockTechInteraction_uScript_LockTechInteraction_41.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_42.SetParent(g);
		logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_43.SetParent(g);
		logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_47.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_50.SetParent(g);
		logic_uScript_LockTechInteraction_uScript_LockTechInteraction_51.SetParent(g);
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_52.SetParent(g);
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_56.SetParent(g);
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_61.SetParent(g);
		logic_uScript_PausePopulation_uScript_PausePopulation_66.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_68.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_71.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_72.SetParent(g);
		logic_uScript_LockTechInteraction_uScript_LockTechInteraction_73.SetParent(g);
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_75.SetParent(g);
		logic_uScript_LockTechInteraction_uScript_LockTechInteraction_76.SetParent(g);
		logic_uScript_LockTech_uScript_LockTech_82.SetParent(g);
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_83.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_84.SetParent(g);
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_85.SetParent(g);
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_86.SetParent(g);
		logic_uScript_SetCustomRadarTeamID_uScript_SetCustomRadarTeamID_87.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_88.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_89.SetParent(g);
		logic_uScript_Wait_uScript_Wait_90.SetParent(g);
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_94.SetParent(g);
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_95.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_96.SetParent(g);
		logic_uScript_LockTechInteraction_uScript_LockTechInteraction_98.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_100.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_101.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_103.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_104.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_105.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_107.SetParent(g);
		logic_uScript_SetCustomRadarTeamID_uScript_SetCustomRadarTeamID_109.SetParent(g);
		logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_111.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_113.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_116.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_117.SetParent(g);
		logic_uScript_GetTankBlock_uScript_GetTankBlock_120.SetParent(g);
		logic_uScript_GetTankBlock_uScript_GetTankBlock_123.SetParent(g);
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_124.SetParent(g);
		logic_uScript_GetTankBlock_uScript_GetTankBlock_132.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_133.SetParent(g);
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_134.SetParent(g);
		logic_uScript_SetTankHideBlockLimit_uScript_SetTankHideBlockLimit_137.SetParent(g);
		logic_uScript_LockTechInteraction_uScript_LockTechInteraction_138.SetParent(g);
		logic_uScript_Wait_uScript_Wait_139.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_143.SetParent(g);
		logic_uScript_SetCustomRadarTeamID_uScript_SetCustomRadarTeamID_149.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_150.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_151.SetParent(g);
		logic_uScript_PausePopulation_uScript_PausePopulation_155.SetParent(g);
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_157.SetParent(g);
		logic_uScript_LockTech_uScript_LockTech_160.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_161.SetParent(g);
		logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_162.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_163.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_165.SetParent(g);
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_168.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_169.SetParent(g);
		logic_uScript_LockTech_uScript_LockTech_172.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_174.SetParent(g);
		logic_uScript_LockTechInteraction_uScript_LockTechInteraction_176.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_183.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_186.SetParent(g);
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_187.SetParent(g);
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_189.SetParent(g);
		logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_190.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_193.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_194.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_198.SetParent(g);
		logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_199.SetParent(g);
		logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_201.SetParent(g);
		logic_uScript_GetTankBlock_uScript_GetTankBlock_206.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_208.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_211.SetParent(g);
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_212.SetParent(g);
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_215.SetParent(g);
		logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_217.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_218.SetParent(g);
		logic_uScript_LockTech_uScript_LockTech_219.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_224.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_226.SetParent(g);
		logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_227.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_228.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_229.SetParent(g);
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_233.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_236.SetParent(g);
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_237.SetParent(g);
		logic_uScript_SetTankHideBlockLimit_uScript_SetTankHideBlockLimit_238.SetParent(g);
		logic_uScript_SetTankHideBlockLimit_uScript_SetTankHideBlockLimit_240.SetParent(g);
		logic_uScript_RemoveScenery_uScript_RemoveScenery_241.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_244.SetParent(g);
		logic_uScript_LockTechInteraction_uScript_LockTechInteraction_245.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_248.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_255.SetParent(g);
		logic_uScript_LockTech_uScript_LockTech_259.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_260.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_261.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_262.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_264.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_266.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_267.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_269.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_274.SetParent(g);
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_277.SetParent(g);
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_279.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_284.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_285.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_286.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_287.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_288.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_291.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_293.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_294.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_295.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_296.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_300.SetParent(g);
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_302.SetParent(g);
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_308.SetParent(g);
		logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_311.SetParent(g);
		logic_uScript_Wait_uScript_Wait_313.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_314.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_315.SetParent(g);
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_316.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_318.SetParent(g);
		logic_uScript_SetTankHideBlockLimit_uScript_SetTankHideBlockLimit_319.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_320.SetParent(g);
		logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_322.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_323.SetParent(g);
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_324.SetParent(g);
		logic_uScript_LockTechInteraction_uScript_LockTechInteraction_326.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_328.SetParent(g);
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_331.SetParent(g);
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_335.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_339.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_340.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_341.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_343.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_345.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_347.SetParent(g);
		logic_uScriptAct_Concatenate_uScriptAct_Concatenate_349.SetParent(g);
		logic_uScriptAct_PrintText_uScriptAct_PrintText_351.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_354.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_355.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_356.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_360.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_361.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_363.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_372.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_375.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_377.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_378.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_381.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_382.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_386.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_388.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_390.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_392.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_393.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_396.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_405.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_409.SetParent(g);
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_410.SetParent(g);
		logic_uScript_Wait_uScript_Wait_412.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_415.SetParent(g);
		logic_uScript_Wait_uScript_Wait_418.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_420.SetParent(g);
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_421.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_425.SetParent(g);
		logic_uScript_Wait_uScript_Wait_426.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_428.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_431.SetParent(g);
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_433.SetParent(g);
		logic_uScript_RemoveTech_uScript_RemoveTech_436.SetParent(g);
		logic_uScript_SpawnVFX_uScript_SpawnVFX_441.SetParent(g);
		logic_uScript_SpawnVFX_uScript_SpawnVFX_446.SetParent(g);
		logic_uScript_RemoveTech_uScript_RemoveTech_452.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_454.SetParent(g);
		logic_uScript_RemoveTech_uScript_RemoveTech_455.SetParent(g);
		logic_uScript_SpawnVFX_uScript_SpawnVFX_458.SetParent(g);
		logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_460.SetParent(g);
		logic_uScript_FinishEncounter_uScript_FinishEncounter_465.SetParent(g);
		logic_uScript_RemoveTech_uScript_RemoveTech_466.SetParent(g);
		logic_uScript_SpawnVFX_uScript_SpawnVFX_467.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_468.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_471.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_473.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_474.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_476.SetParent(g);
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_481.SetParent(g);
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_484.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_486.SetParent(g);
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_487.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_490.SetParent(g);
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_1062.SetParent(g);
		owner_Connection_3 = parentGameObject;
		owner_Connection_29 = parentGameObject;
		owner_Connection_37 = parentGameObject;
		owner_Connection_45 = parentGameObject;
		owner_Connection_74 = parentGameObject;
		owner_Connection_77 = parentGameObject;
		owner_Connection_78 = parentGameObject;
		owner_Connection_79 = parentGameObject;
		owner_Connection_110 = parentGameObject;
		owner_Connection_192 = parentGameObject;
		owner_Connection_207 = parentGameObject;
		owner_Connection_251 = parentGameObject;
		owner_Connection_268 = parentGameObject;
		owner_Connection_272 = parentGameObject;
		owner_Connection_305 = parentGameObject;
		owner_Connection_338 = parentGameObject;
		owner_Connection_344 = parentGameObject;
		owner_Connection_357 = parentGameObject;
		owner_Connection_362 = parentGameObject;
		owner_Connection_370 = parentGameObject;
		owner_Connection_379 = parentGameObject;
		owner_Connection_435 = parentGameObject;
		owner_Connection_444 = parentGameObject;
		owner_Connection_447 = parentGameObject;
		owner_Connection_449 = parentGameObject;
		owner_Connection_451 = parentGameObject;
	}

	public void Awake()
	{
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_71.Awake();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_85.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_117.Awake();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_189.Awake();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_279.Awake();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_302.Awake();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_308.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_320.Awake();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_324.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_347.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_386.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_388.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_471.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_473.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_474.Awake();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_481.Awake();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_484.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_490.Awake();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_1062.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_71.Save_Out += SubGraph_SaveLoadBool_Save_Out_71;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_71.Load_Out += SubGraph_SaveLoadBool_Load_Out_71;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_71.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_71;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_85.Out += SubGraph_CompleteObjectiveStage_Out_85;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_117.Save_Out += SubGraph_SaveLoadBool_Save_Out_117;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_117.Load_Out += SubGraph_SaveLoadBool_Load_Out_117;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_117.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_117;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_134.Output1 += uScriptCon_ManualSwitch_Output1_134;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_134.Output2 += uScriptCon_ManualSwitch_Output2_134;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_134.Output3 += uScriptCon_ManualSwitch_Output3_134;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_134.Output4 += uScriptCon_ManualSwitch_Output4_134;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_134.Output5 += uScriptCon_ManualSwitch_Output5_134;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_134.Output6 += uScriptCon_ManualSwitch_Output6_134;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_134.Output7 += uScriptCon_ManualSwitch_Output7_134;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_134.Output8 += uScriptCon_ManualSwitch_Output8_134;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_189.Save_Out += SubGraph_SaveLoadInt_Save_Out_189;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_189.Load_Out += SubGraph_SaveLoadInt_Load_Out_189;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_189.Restart_Out += SubGraph_SaveLoadInt_Restart_Out_189;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_233.Output1 += uScriptCon_ManualSwitch_Output1_233;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_233.Output2 += uScriptCon_ManualSwitch_Output2_233;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_233.Output3 += uScriptCon_ManualSwitch_Output3_233;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_233.Output4 += uScriptCon_ManualSwitch_Output4_233;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_233.Output5 += uScriptCon_ManualSwitch_Output5_233;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_233.Output6 += uScriptCon_ManualSwitch_Output6_233;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_233.Output7 += uScriptCon_ManualSwitch_Output7_233;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_233.Output8 += uScriptCon_ManualSwitch_Output8_233;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_279.Save_Out += SubGraph_SaveLoadInt_Save_Out_279;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_279.Load_Out += SubGraph_SaveLoadInt_Load_Out_279;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_279.Restart_Out += SubGraph_SaveLoadInt_Restart_Out_279;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_302.Save_Out += SubGraph_SaveLoadInt_Save_Out_302;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_302.Load_Out += SubGraph_SaveLoadInt_Load_Out_302;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_302.Restart_Out += SubGraph_SaveLoadInt_Restart_Out_302;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_308.Out += SubGraph_CompleteObjectiveStage_Out_308;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_316.Output1 += uScriptCon_ManualSwitch_Output1_316;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_316.Output2 += uScriptCon_ManualSwitch_Output2_316;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_316.Output3 += uScriptCon_ManualSwitch_Output3_316;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_316.Output4 += uScriptCon_ManualSwitch_Output4_316;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_316.Output5 += uScriptCon_ManualSwitch_Output5_316;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_316.Output6 += uScriptCon_ManualSwitch_Output6_316;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_316.Output7 += uScriptCon_ManualSwitch_Output7_316;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_316.Output8 += uScriptCon_ManualSwitch_Output8_316;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_320.Save_Out += SubGraph_SaveLoadBool_Save_Out_320;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_320.Load_Out += SubGraph_SaveLoadBool_Load_Out_320;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_320.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_320;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_324.Save_Out += SubGraph_SaveLoadInt_Save_Out_324;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_324.Load_Out += SubGraph_SaveLoadInt_Load_Out_324;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_324.Restart_Out += SubGraph_SaveLoadInt_Restart_Out_324;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_331.Output1 += uScriptCon_ManualSwitch_Output1_331;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_331.Output2 += uScriptCon_ManualSwitch_Output2_331;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_331.Output3 += uScriptCon_ManualSwitch_Output3_331;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_331.Output4 += uScriptCon_ManualSwitch_Output4_331;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_331.Output5 += uScriptCon_ManualSwitch_Output5_331;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_331.Output6 += uScriptCon_ManualSwitch_Output6_331;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_331.Output7 += uScriptCon_ManualSwitch_Output7_331;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_331.Output8 += uScriptCon_ManualSwitch_Output8_331;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_347.Save_Out += SubGraph_SaveLoadBool_Save_Out_347;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_347.Load_Out += SubGraph_SaveLoadBool_Load_Out_347;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_347.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_347;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_386.Save_Out += SubGraph_SaveLoadBool_Save_Out_386;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_386.Load_Out += SubGraph_SaveLoadBool_Load_Out_386;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_386.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_386;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_388.Save_Out += SubGraph_SaveLoadBool_Save_Out_388;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_388.Load_Out += SubGraph_SaveLoadBool_Load_Out_388;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_388.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_388;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_471.Save_Out += SubGraph_SaveLoadBool_Save_Out_471;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_471.Load_Out += SubGraph_SaveLoadBool_Load_Out_471;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_471.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_471;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_473.Save_Out += SubGraph_SaveLoadBool_Save_Out_473;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_473.Load_Out += SubGraph_SaveLoadBool_Load_Out_473;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_473.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_473;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_474.Save_Out += SubGraph_SaveLoadBool_Save_Out_474;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_474.Load_Out += SubGraph_SaveLoadBool_Load_Out_474;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_474.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_474;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_481.Out += SubGraph_CompleteObjectiveStage_Out_481;
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_484.Out += SubGraph_LoadObjectiveStates_Out_484;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_490.Save_Out += SubGraph_SaveLoadBool_Save_Out_490;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_490.Load_Out += SubGraph_SaveLoadBool_Load_Out_490;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_490.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_490;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_1062.Out += SubGraph_AddMessageWithPadSupport_Out_1062;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_1062.Shown += SubGraph_AddMessageWithPadSupport_Shown_1062;
	}

	public void Start()
	{
		SyncUnityHooks();
		m_RegisteredForEvents = true;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_71.Start();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_85.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_117.Start();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_189.Start();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_279.Start();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_302.Start();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_308.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_320.Start();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_324.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_347.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_386.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_388.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_471.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_473.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_474.Start();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_481.Start();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_484.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_490.Start();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_1062.Start();
	}

	public void OnEnable()
	{
		RegisterForUnityHooks();
		m_RegisteredForEvents = true;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_71.OnEnable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_85.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_117.OnEnable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_189.OnEnable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_279.OnEnable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_302.OnEnable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_308.OnEnable();
		logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_311.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_320.OnEnable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_324.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_347.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_386.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_388.OnEnable();
		logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_460.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_471.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_473.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_474.OnEnable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_481.OnEnable();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_484.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_490.OnEnable();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_1062.OnEnable();
	}

	public void OnDisable()
	{
		logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_15.OnDisable();
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_20.OnDisable();
		logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_24.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_27.OnDisable();
		logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_43.OnDisable();
		logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_47.OnDisable();
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_56.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_71.OnDisable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_85.OnDisable();
		logic_uScript_Wait_uScript_Wait_90.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_100.OnDisable();
		logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_111.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_117.OnDisable();
		logic_uScript_GetTankBlock_uScript_GetTankBlock_120.OnDisable();
		logic_uScript_GetTankBlock_uScript_GetTankBlock_123.OnDisable();
		logic_uScript_GetTankBlock_uScript_GetTankBlock_132.OnDisable();
		logic_uScript_Wait_uScript_Wait_139.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_161.OnDisable();
		logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_162.OnDisable();
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_168.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_174.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_186.OnDisable();
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_187.OnDisable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_189.OnDisable();
		logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_190.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_193.OnDisable();
		logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_199.OnDisable();
		logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_201.OnDisable();
		logic_uScript_GetTankBlock_uScript_GetTankBlock_206.OnDisable();
		logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_217.OnDisable();
		logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_227.OnDisable();
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_237.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_260.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_261.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_264.OnDisable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_279.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_294.OnDisable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_302.OnDisable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_308.OnDisable();
		logic_uScript_Wait_uScript_Wait_313.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_320.OnDisable();
		logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_322.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_323.OnDisable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_324.OnDisable();
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_335.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_347.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_386.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_388.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_405.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_409.OnDisable();
		logic_uScript_Wait_uScript_Wait_412.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_415.OnDisable();
		logic_uScript_Wait_uScript_Wait_418.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_425.OnDisable();
		logic_uScript_Wait_uScript_Wait_426.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_428.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_454.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_471.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_473.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_474.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_476.OnDisable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_481.OnDisable();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_484.OnDisable();
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_487.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_490.OnDisable();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_1062.OnDisable();
		UnregisterEventListeners();
		m_RegisteredForEvents = false;
	}

	public void Update()
	{
		SyncEventListeners();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_71.Update();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_85.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_117.Update();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_189.Update();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_279.Update();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_302.Update();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_308.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_320.Update();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_324.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_347.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_386.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_388.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_471.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_473.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_474.Update();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_481.Update();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_484.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_490.Update();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_1062.Update();
	}

	public void OnDestroy()
	{
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_71.OnDestroy();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_85.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_117.OnDestroy();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_189.OnDestroy();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_279.OnDestroy();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_302.OnDestroy();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_308.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_320.OnDestroy();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_324.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_347.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_386.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_388.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_471.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_473.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_474.OnDestroy();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_481.OnDestroy();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_484.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_490.OnDestroy();
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_1062.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_71.Save_Out -= SubGraph_SaveLoadBool_Save_Out_71;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_71.Load_Out -= SubGraph_SaveLoadBool_Load_Out_71;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_71.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_71;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_85.Out -= SubGraph_CompleteObjectiveStage_Out_85;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_117.Save_Out -= SubGraph_SaveLoadBool_Save_Out_117;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_117.Load_Out -= SubGraph_SaveLoadBool_Load_Out_117;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_117.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_117;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_134.Output1 -= uScriptCon_ManualSwitch_Output1_134;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_134.Output2 -= uScriptCon_ManualSwitch_Output2_134;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_134.Output3 -= uScriptCon_ManualSwitch_Output3_134;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_134.Output4 -= uScriptCon_ManualSwitch_Output4_134;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_134.Output5 -= uScriptCon_ManualSwitch_Output5_134;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_134.Output6 -= uScriptCon_ManualSwitch_Output6_134;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_134.Output7 -= uScriptCon_ManualSwitch_Output7_134;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_134.Output8 -= uScriptCon_ManualSwitch_Output8_134;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_189.Save_Out -= SubGraph_SaveLoadInt_Save_Out_189;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_189.Load_Out -= SubGraph_SaveLoadInt_Load_Out_189;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_189.Restart_Out -= SubGraph_SaveLoadInt_Restart_Out_189;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_233.Output1 -= uScriptCon_ManualSwitch_Output1_233;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_233.Output2 -= uScriptCon_ManualSwitch_Output2_233;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_233.Output3 -= uScriptCon_ManualSwitch_Output3_233;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_233.Output4 -= uScriptCon_ManualSwitch_Output4_233;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_233.Output5 -= uScriptCon_ManualSwitch_Output5_233;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_233.Output6 -= uScriptCon_ManualSwitch_Output6_233;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_233.Output7 -= uScriptCon_ManualSwitch_Output7_233;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_233.Output8 -= uScriptCon_ManualSwitch_Output8_233;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_279.Save_Out -= SubGraph_SaveLoadInt_Save_Out_279;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_279.Load_Out -= SubGraph_SaveLoadInt_Load_Out_279;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_279.Restart_Out -= SubGraph_SaveLoadInt_Restart_Out_279;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_302.Save_Out -= SubGraph_SaveLoadInt_Save_Out_302;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_302.Load_Out -= SubGraph_SaveLoadInt_Load_Out_302;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_302.Restart_Out -= SubGraph_SaveLoadInt_Restart_Out_302;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_308.Out -= SubGraph_CompleteObjectiveStage_Out_308;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_316.Output1 -= uScriptCon_ManualSwitch_Output1_316;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_316.Output2 -= uScriptCon_ManualSwitch_Output2_316;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_316.Output3 -= uScriptCon_ManualSwitch_Output3_316;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_316.Output4 -= uScriptCon_ManualSwitch_Output4_316;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_316.Output5 -= uScriptCon_ManualSwitch_Output5_316;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_316.Output6 -= uScriptCon_ManualSwitch_Output6_316;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_316.Output7 -= uScriptCon_ManualSwitch_Output7_316;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_316.Output8 -= uScriptCon_ManualSwitch_Output8_316;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_320.Save_Out -= SubGraph_SaveLoadBool_Save_Out_320;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_320.Load_Out -= SubGraph_SaveLoadBool_Load_Out_320;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_320.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_320;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_324.Save_Out -= SubGraph_SaveLoadInt_Save_Out_324;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_324.Load_Out -= SubGraph_SaveLoadInt_Load_Out_324;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_324.Restart_Out -= SubGraph_SaveLoadInt_Restart_Out_324;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_331.Output1 -= uScriptCon_ManualSwitch_Output1_331;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_331.Output2 -= uScriptCon_ManualSwitch_Output2_331;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_331.Output3 -= uScriptCon_ManualSwitch_Output3_331;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_331.Output4 -= uScriptCon_ManualSwitch_Output4_331;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_331.Output5 -= uScriptCon_ManualSwitch_Output5_331;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_331.Output6 -= uScriptCon_ManualSwitch_Output6_331;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_331.Output7 -= uScriptCon_ManualSwitch_Output7_331;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_331.Output8 -= uScriptCon_ManualSwitch_Output8_331;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_347.Save_Out -= SubGraph_SaveLoadBool_Save_Out_347;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_347.Load_Out -= SubGraph_SaveLoadBool_Load_Out_347;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_347.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_347;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_386.Save_Out -= SubGraph_SaveLoadBool_Save_Out_386;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_386.Load_Out -= SubGraph_SaveLoadBool_Load_Out_386;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_386.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_386;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_388.Save_Out -= SubGraph_SaveLoadBool_Save_Out_388;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_388.Load_Out -= SubGraph_SaveLoadBool_Load_Out_388;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_388.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_388;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_471.Save_Out -= SubGraph_SaveLoadBool_Save_Out_471;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_471.Load_Out -= SubGraph_SaveLoadBool_Load_Out_471;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_471.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_471;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_473.Save_Out -= SubGraph_SaveLoadBool_Save_Out_473;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_473.Load_Out -= SubGraph_SaveLoadBool_Load_Out_473;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_473.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_473;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_474.Save_Out -= SubGraph_SaveLoadBool_Save_Out_474;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_474.Load_Out -= SubGraph_SaveLoadBool_Load_Out_474;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_474.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_474;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_481.Out -= SubGraph_CompleteObjectiveStage_Out_481;
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_484.Out -= SubGraph_LoadObjectiveStates_Out_484;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_490.Save_Out -= SubGraph_SaveLoadBool_Save_Out_490;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_490.Load_Out -= SubGraph_SaveLoadBool_Load_Out_490;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_490.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_490;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_1062.Out -= SubGraph_AddMessageWithPadSupport_Out_1062;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_1062.Shown -= SubGraph_AddMessageWithPadSupport_Shown_1062;
	}

	public void OnGUI()
	{
		logic_uScriptAct_PrintText_uScriptAct_PrintText_351.OnGUI();
	}

	private void Instance_OnUpdate_70(object o, EventArgs e)
	{
		Relay_OnUpdate_70();
	}

	private void Instance_OnSuspend_70(object o, EventArgs e)
	{
		Relay_OnSuspend_70();
	}

	private void Instance_OnResume_70(object o, EventArgs e)
	{
		Relay_OnResume_70();
	}

	private void Instance_SaveEvent_182(object o, EventArgs e)
	{
		Relay_SaveEvent_182();
	}

	private void Instance_LoadEvent_182(object o, EventArgs e)
	{
		Relay_LoadEvent_182();
	}

	private void Instance_RestartEvent_182(object o, EventArgs e)
	{
		Relay_RestartEvent_182();
	}

	private void SubGraph_SaveLoadBool_Save_Out_71(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_71 = e.boolean;
		local_CanPushButtons_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_71;
		Relay_Save_Out_71();
	}

	private void SubGraph_SaveLoadBool_Load_Out_71(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_71 = e.boolean;
		local_CanPushButtons_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_71;
		Relay_Load_Out_71();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_71(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_71 = e.boolean;
		local_CanPushButtons_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_71;
		Relay_Restart_Out_71();
	}

	private void SubGraph_CompleteObjectiveStage_Out_85(object o, SubGraph_CompleteObjectiveStage.LogicEventArgs e)
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_85 = e.objectiveStage;
		local_Stage_System_Int32 = logic_SubGraph_CompleteObjectiveStage_objectiveStage_85;
		Relay_Out_85();
	}

	private void SubGraph_SaveLoadBool_Save_Out_117(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_117 = e.boolean;
		local_msgIntro_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_117;
		Relay_Save_Out_117();
	}

	private void SubGraph_SaveLoadBool_Load_Out_117(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_117 = e.boolean;
		local_msgIntro_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_117;
		Relay_Load_Out_117();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_117(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_117 = e.boolean;
		local_msgIntro_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_117;
		Relay_Restart_Out_117();
	}

	private void uScriptCon_ManualSwitch_Output1_134(object o, EventArgs e)
	{
		Relay_Output1_134();
	}

	private void uScriptCon_ManualSwitch_Output2_134(object o, EventArgs e)
	{
		Relay_Output2_134();
	}

	private void uScriptCon_ManualSwitch_Output3_134(object o, EventArgs e)
	{
		Relay_Output3_134();
	}

	private void uScriptCon_ManualSwitch_Output4_134(object o, EventArgs e)
	{
		Relay_Output4_134();
	}

	private void uScriptCon_ManualSwitch_Output5_134(object o, EventArgs e)
	{
		Relay_Output5_134();
	}

	private void uScriptCon_ManualSwitch_Output6_134(object o, EventArgs e)
	{
		Relay_Output6_134();
	}

	private void uScriptCon_ManualSwitch_Output7_134(object o, EventArgs e)
	{
		Relay_Output7_134();
	}

	private void uScriptCon_ManualSwitch_Output8_134(object o, EventArgs e)
	{
		Relay_Output8_134();
	}

	private void SubGraph_SaveLoadInt_Save_Out_189(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_189 = e.integer;
		local_Stage3QuestionTwo_System_Int32 = logic_SubGraph_SaveLoadInt_integer_189;
		Relay_Save_Out_189();
	}

	private void SubGraph_SaveLoadInt_Load_Out_189(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_189 = e.integer;
		local_Stage3QuestionTwo_System_Int32 = logic_SubGraph_SaveLoadInt_integer_189;
		Relay_Load_Out_189();
	}

	private void SubGraph_SaveLoadInt_Restart_Out_189(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_189 = e.integer;
		local_Stage3QuestionTwo_System_Int32 = logic_SubGraph_SaveLoadInt_integer_189;
		Relay_Restart_Out_189();
	}

	private void uScriptCon_ManualSwitch_Output1_233(object o, EventArgs e)
	{
		Relay_Output1_233();
	}

	private void uScriptCon_ManualSwitch_Output2_233(object o, EventArgs e)
	{
		Relay_Output2_233();
	}

	private void uScriptCon_ManualSwitch_Output3_233(object o, EventArgs e)
	{
		Relay_Output3_233();
	}

	private void uScriptCon_ManualSwitch_Output4_233(object o, EventArgs e)
	{
		Relay_Output4_233();
	}

	private void uScriptCon_ManualSwitch_Output5_233(object o, EventArgs e)
	{
		Relay_Output5_233();
	}

	private void uScriptCon_ManualSwitch_Output6_233(object o, EventArgs e)
	{
		Relay_Output6_233();
	}

	private void uScriptCon_ManualSwitch_Output7_233(object o, EventArgs e)
	{
		Relay_Output7_233();
	}

	private void uScriptCon_ManualSwitch_Output8_233(object o, EventArgs e)
	{
		Relay_Output8_233();
	}

	private void SubGraph_SaveLoadInt_Save_Out_279(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_279 = e.integer;
		local_Stage_System_Int32 = logic_SubGraph_SaveLoadInt_integer_279;
		Relay_Save_Out_279();
	}

	private void SubGraph_SaveLoadInt_Load_Out_279(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_279 = e.integer;
		local_Stage_System_Int32 = logic_SubGraph_SaveLoadInt_integer_279;
		Relay_Load_Out_279();
	}

	private void SubGraph_SaveLoadInt_Restart_Out_279(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_279 = e.integer;
		local_Stage_System_Int32 = logic_SubGraph_SaveLoadInt_integer_279;
		Relay_Restart_Out_279();
	}

	private void SubGraph_SaveLoadInt_Save_Out_302(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_302 = e.integer;
		local_Stage2QuestionOne_System_Int32 = logic_SubGraph_SaveLoadInt_integer_302;
		Relay_Save_Out_302();
	}

	private void SubGraph_SaveLoadInt_Load_Out_302(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_302 = e.integer;
		local_Stage2QuestionOne_System_Int32 = logic_SubGraph_SaveLoadInt_integer_302;
		Relay_Load_Out_302();
	}

	private void SubGraph_SaveLoadInt_Restart_Out_302(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_302 = e.integer;
		local_Stage2QuestionOne_System_Int32 = logic_SubGraph_SaveLoadInt_integer_302;
		Relay_Restart_Out_302();
	}

	private void SubGraph_CompleteObjectiveStage_Out_308(object o, SubGraph_CompleteObjectiveStage.LogicEventArgs e)
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_308 = e.objectiveStage;
		local_Stage_System_Int32 = logic_SubGraph_CompleteObjectiveStage_objectiveStage_308;
		Relay_Out_308();
	}

	private void uScriptCon_ManualSwitch_Output1_316(object o, EventArgs e)
	{
		Relay_Output1_316();
	}

	private void uScriptCon_ManualSwitch_Output2_316(object o, EventArgs e)
	{
		Relay_Output2_316();
	}

	private void uScriptCon_ManualSwitch_Output3_316(object o, EventArgs e)
	{
		Relay_Output3_316();
	}

	private void uScriptCon_ManualSwitch_Output4_316(object o, EventArgs e)
	{
		Relay_Output4_316();
	}

	private void uScriptCon_ManualSwitch_Output5_316(object o, EventArgs e)
	{
		Relay_Output5_316();
	}

	private void uScriptCon_ManualSwitch_Output6_316(object o, EventArgs e)
	{
		Relay_Output6_316();
	}

	private void uScriptCon_ManualSwitch_Output7_316(object o, EventArgs e)
	{
		Relay_Output7_316();
	}

	private void uScriptCon_ManualSwitch_Output8_316(object o, EventArgs e)
	{
		Relay_Output8_316();
	}

	private void SubGraph_SaveLoadBool_Save_Out_320(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_320 = e.boolean;
		local_Initialize_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_320;
		Relay_Save_Out_320();
	}

	private void SubGraph_SaveLoadBool_Load_Out_320(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_320 = e.boolean;
		local_Initialize_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_320;
		Relay_Load_Out_320();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_320(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_320 = e.boolean;
		local_Initialize_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_320;
		Relay_Restart_Out_320();
	}

	private void SubGraph_SaveLoadInt_Save_Out_324(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_324 = e.integer;
		local_Stage4QuestionThree_System_Int32 = logic_SubGraph_SaveLoadInt_integer_324;
		Relay_Save_Out_324();
	}

	private void SubGraph_SaveLoadInt_Load_Out_324(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_324 = e.integer;
		local_Stage4QuestionThree_System_Int32 = logic_SubGraph_SaveLoadInt_integer_324;
		Relay_Load_Out_324();
	}

	private void SubGraph_SaveLoadInt_Restart_Out_324(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_324 = e.integer;
		local_Stage4QuestionThree_System_Int32 = logic_SubGraph_SaveLoadInt_integer_324;
		Relay_Restart_Out_324();
	}

	private void uScriptCon_ManualSwitch_Output1_331(object o, EventArgs e)
	{
		Relay_Output1_331();
	}

	private void uScriptCon_ManualSwitch_Output2_331(object o, EventArgs e)
	{
		Relay_Output2_331();
	}

	private void uScriptCon_ManualSwitch_Output3_331(object o, EventArgs e)
	{
		Relay_Output3_331();
	}

	private void uScriptCon_ManualSwitch_Output4_331(object o, EventArgs e)
	{
		Relay_Output4_331();
	}

	private void uScriptCon_ManualSwitch_Output5_331(object o, EventArgs e)
	{
		Relay_Output5_331();
	}

	private void uScriptCon_ManualSwitch_Output6_331(object o, EventArgs e)
	{
		Relay_Output6_331();
	}

	private void uScriptCon_ManualSwitch_Output7_331(object o, EventArgs e)
	{
		Relay_Output7_331();
	}

	private void uScriptCon_ManualSwitch_Output8_331(object o, EventArgs e)
	{
		Relay_Output8_331();
	}

	private void SubGraph_SaveLoadBool_Save_Out_347(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_347 = e.boolean;
		local_Q1EnemyAlive_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_347;
		Relay_Save_Out_347();
	}

	private void SubGraph_SaveLoadBool_Load_Out_347(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_347 = e.boolean;
		local_Q1EnemyAlive_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_347;
		Relay_Load_Out_347();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_347(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_347 = e.boolean;
		local_Q1EnemyAlive_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_347;
		Relay_Restart_Out_347();
	}

	private void SubGraph_SaveLoadBool_Save_Out_386(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_386 = e.boolean;
		local_Q2EnemyAlive_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_386;
		Relay_Save_Out_386();
	}

	private void SubGraph_SaveLoadBool_Load_Out_386(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_386 = e.boolean;
		local_Q2EnemyAlive_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_386;
		Relay_Load_Out_386();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_386(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_386 = e.boolean;
		local_Q2EnemyAlive_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_386;
		Relay_Restart_Out_386();
	}

	private void SubGraph_SaveLoadBool_Save_Out_388(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_388 = e.boolean;
		local_Q3EnemyAlive_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_388;
		Relay_Save_Out_388();
	}

	private void SubGraph_SaveLoadBool_Load_Out_388(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_388 = e.boolean;
		local_Q3EnemyAlive_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_388;
		Relay_Load_Out_388();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_388(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_388 = e.boolean;
		local_Q3EnemyAlive_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_388;
		Relay_Restart_Out_388();
	}

	private void SubGraph_SaveLoadBool_Save_Out_471(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_471 = e.boolean;
		local_Question01WrongAnswers_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_471;
		Relay_Save_Out_471();
	}

	private void SubGraph_SaveLoadBool_Load_Out_471(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_471 = e.boolean;
		local_Question01WrongAnswers_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_471;
		Relay_Load_Out_471();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_471(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_471 = e.boolean;
		local_Question01WrongAnswers_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_471;
		Relay_Restart_Out_471();
	}

	private void SubGraph_SaveLoadBool_Save_Out_473(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_473 = e.boolean;
		local_Question02WrongAnswers_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_473;
		Relay_Save_Out_473();
	}

	private void SubGraph_SaveLoadBool_Load_Out_473(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_473 = e.boolean;
		local_Question02WrongAnswers_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_473;
		Relay_Load_Out_473();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_473(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_473 = e.boolean;
		local_Question02WrongAnswers_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_473;
		Relay_Restart_Out_473();
	}

	private void SubGraph_SaveLoadBool_Save_Out_474(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_474 = e.boolean;
		local_Question03WrongAnswers_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_474;
		Relay_Save_Out_474();
	}

	private void SubGraph_SaveLoadBool_Load_Out_474(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_474 = e.boolean;
		local_Question03WrongAnswers_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_474;
		Relay_Load_Out_474();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_474(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_474 = e.boolean;
		local_Question03WrongAnswers_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_474;
		Relay_Restart_Out_474();
	}

	private void SubGraph_CompleteObjectiveStage_Out_481(object o, SubGraph_CompleteObjectiveStage.LogicEventArgs e)
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_481 = e.objectiveStage;
		local_Stage_System_Int32 = logic_SubGraph_CompleteObjectiveStage_objectiveStage_481;
		Relay_Out_481();
	}

	private void SubGraph_LoadObjectiveStates_Out_484(object o, SubGraph_LoadObjectiveStates.LogicEventArgs e)
	{
		Relay_Out_484();
	}

	private void SubGraph_SaveLoadBool_Save_Out_490(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_490 = e.boolean;
		local_msgBaseFoundShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_490;
		Relay_Save_Out_490();
	}

	private void SubGraph_SaveLoadBool_Load_Out_490(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_490 = e.boolean;
		local_msgBaseFoundShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_490;
		Relay_Load_Out_490();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_490(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_490 = e.boolean;
		local_msgBaseFoundShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_490;
		Relay_Restart_Out_490();
	}

	private void SubGraph_AddMessageWithPadSupport_Out_1062(object o, SubGraph_AddMessageWithPadSupport.LogicEventArgs e)
	{
		logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboardReturn_1062 = e.messageMouseKeyboardReturn;
		logic_SubGraph_AddMessageWithPadSupport_messageControlPadReturn_1062 = e.messageControlPadReturn;
		Relay_Out_1062();
	}

	private void SubGraph_AddMessageWithPadSupport_Shown_1062(object o, SubGraph_AddMessageWithPadSupport.LogicEventArgs e)
	{
		logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboardReturn_1062 = e.messageMouseKeyboardReturn;
		logic_SubGraph_AddMessageWithPadSupport_messageControlPadReturn_1062 = e.messageControlPadReturn;
		Relay_Shown_1062();
	}

	private void Relay_True_8()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_8.True(out logic_uScriptAct_SetBool_Target_8);
		local_CanPushButtons_System_Boolean = logic_uScriptAct_SetBool_Target_8;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_8.Out)
		{
			Relay_In_161();
		}
	}

	private void Relay_False_8()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_8.False(out logic_uScriptAct_SetBool_Target_8);
		local_CanPushButtons_System_Boolean = logic_uScriptAct_SetBool_Target_8;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_8.Out)
		{
			Relay_In_161();
		}
	}

	private void Relay_In_12()
	{
		logic_uScriptCon_CompareBool_Bool_12 = local_NearNPC_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_12.In(logic_uScriptCon_CompareBool_Bool_12);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_12.True)
		{
			Relay_In_323();
		}
	}

	private void Relay_In_15()
	{
		logic_uScript_GetCircuitChargeInfo_block_15 = local_ButtonBlock2_TankBlock;
		logic_uScript_GetCircuitChargeInfo_Return_15 = logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_15.In(logic_uScript_GetCircuitChargeInfo_block_15, logic_uScript_GetCircuitChargeInfo_tech_15, logic_uScript_GetCircuitChargeInfo_blockType_15);
		local_Button2Value_System_Int32 = logic_uScript_GetCircuitChargeInfo_Return_15;
		if (logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_15.Out)
		{
			Relay_In_95();
		}
	}

	private void Relay_In_16()
	{
		logic_uScript_SetCustomRadarTeamID_tech_16 = local_ButtonBase2Tech_Tank;
		logic_uScript_SetCustomRadarTeamID_uScript_SetCustomRadarTeamID_16.In(logic_uScript_SetCustomRadarTeamID_tech_16, logic_uScript_SetCustomRadarTeamID_radarTeamID_16);
		if (logic_uScript_SetCustomRadarTeamID_uScript_SetCustomRadarTeamID_16.Out)
		{
			Relay_In_240();
		}
	}

	private void Relay_In_17()
	{
		logic_uScriptCon_CompareBool_Bool_17 = local_Question03WrongAnswers_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_17.In(logic_uScriptCon_CompareBool_Bool_17);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_17.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_17.False;
		if (num)
		{
			Relay_In_193();
		}
		if (flag)
		{
			Relay_In_372();
		}
	}

	private void Relay_In_20()
	{
		logic_uScript_SetTankInvulnerable_tank_20 = local_ButtonBase2Tech_Tank;
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_20.In(logic_uScript_SetTankInvulnerable_invulnerable_20, logic_uScript_SetTankInvulnerable_tank_20);
		if (logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_20.Out)
		{
			Relay_In_16();
		}
	}

	private void Relay_In_21()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_21.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_21.Out)
		{
			Relay_In_194();
		}
	}

	private void Relay_In_23()
	{
		logic_uScript_SetCustomRadarTeamID_tech_23 = local_NPCTech_Tank;
		logic_uScript_SetCustomRadarTeamID_uScript_SetCustomRadarTeamID_23.In(logic_uScript_SetCustomRadarTeamID_tech_23, logic_uScript_SetCustomRadarTeamID_radarTeamID_23);
		if (logic_uScript_SetCustomRadarTeamID_uScript_SetCustomRadarTeamID_23.Out)
		{
			Relay_In_311();
		}
	}

	private void Relay_In_24()
	{
		logic_uScript_GetCircuitChargeInfo_block_24 = local_ButtonBlock3_TankBlock;
		logic_uScript_GetCircuitChargeInfo_Return_24 = logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_24.In(logic_uScript_GetCircuitChargeInfo_block_24, logic_uScript_GetCircuitChargeInfo_tech_24, logic_uScript_GetCircuitChargeInfo_blockType_24);
		local_Button3Value_System_Int32 = logic_uScript_GetCircuitChargeInfo_Return_24;
		if (logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_24.Out)
		{
			Relay_In_75();
		}
	}

	private void Relay_In_27()
	{
		logic_uScript_AddMessage_messageData_27 = msg09QuestionThreeCorrect;
		logic_uScript_AddMessage_speaker_27 = messageSpeaker;
		logic_uScript_AddMessage_Return_27 = logic_uScript_AddMessage_uScript_AddMessage_27.In(logic_uScript_AddMessage_messageData_27, logic_uScript_AddMessage_speaker_27);
		if (logic_uScript_AddMessage_uScript_AddMessage_27.Shown)
		{
			Relay_In_468();
		}
	}

	private void Relay_In_30()
	{
		logic_uScriptCon_CompareInt_A_30 = local_Button4Value_System_Int32;
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_30.In(logic_uScriptCon_CompareInt_A_30, logic_uScriptCon_CompareInt_B_30);
		if (logic_uScriptCon_CompareInt_uScriptCon_CompareInt_30.GreaterThan)
		{
			Relay_In_39();
		}
	}

	private void Relay_True_31()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_31.True(out logic_uScriptAct_SetBool_Target_31);
		local_Question01WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_31;
	}

	private void Relay_False_31()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_31.False(out logic_uScriptAct_SetBool_Target_31);
		local_Question01WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_31;
	}

	private void Relay_In_35()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_35.In(logic_uScriptAct_SetInt_Value_35, out logic_uScriptAct_SetInt_Target_35);
		local_Stage2QuestionOne_System_Int32 = logic_uScriptAct_SetInt_Target_35;
		if (logic_uScriptAct_SetInt_uScriptAct_SetInt_35.Out)
		{
			Relay_True_274();
		}
	}

	private void Relay_In_39()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_39.In(logic_uScriptAct_SetInt_Value_39, out logic_uScriptAct_SetInt_Target_39);
		local_Stage4QuestionThree_System_Int32 = logic_uScriptAct_SetInt_Target_39;
		if (logic_uScriptAct_SetInt_uScriptAct_SetInt_39.Out)
		{
			Relay_True_50();
		}
	}

	private void Relay_In_41()
	{
		logic_uScript_LockTechInteraction_tech_41 = local_ButtonBase4Tech_Tank;
		int num = 0;
		if (logic_uScript_LockTechInteraction_excludedBlocks_41.Length <= num)
		{
			Array.Resize(ref logic_uScript_LockTechInteraction_excludedBlocks_41, num + 1);
		}
		logic_uScript_LockTechInteraction_excludedBlocks_41[num++] = blockTypeButton4;
		int num2 = 0;
		if (logic_uScript_LockTechInteraction_excludedUniqueBlocks_41.Length <= num2)
		{
			Array.Resize(ref logic_uScript_LockTechInteraction_excludedUniqueBlocks_41, num2 + 1);
		}
		logic_uScript_LockTechInteraction_excludedUniqueBlocks_41[num2++] = local_ButtonBlock4_TankBlock;
		logic_uScript_LockTechInteraction_uScript_LockTechInteraction_41.In(logic_uScript_LockTechInteraction_tech_41, logic_uScript_LockTechInteraction_excludedBlocks_41, logic_uScript_LockTechInteraction_excludedUniqueBlocks_41);
		if (logic_uScript_LockTechInteraction_uScript_LockTechInteraction_41.Out)
		{
			Relay_In_487();
		}
	}

	private void Relay_In_42()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_42.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_42.Out)
		{
			Relay_In_295();
		}
	}

	private void Relay_In_43()
	{
		logic_uScript_GetCircuitChargeInfo_block_43 = local_ButtonBlock2_TankBlock;
		logic_uScript_GetCircuitChargeInfo_Return_43 = logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_43.In(logic_uScript_GetCircuitChargeInfo_block_43, logic_uScript_GetCircuitChargeInfo_tech_43, logic_uScript_GetCircuitChargeInfo_blockType_43);
		local_Button2Value_System_Int32 = logic_uScript_GetCircuitChargeInfo_Return_43;
		if (logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_43.Out)
		{
			Relay_In_157();
		}
	}

	private void Relay_In_47()
	{
		logic_uScript_GetCircuitChargeInfo_block_47 = local_ButtonBlock3_TankBlock;
		logic_uScript_GetCircuitChargeInfo_Return_47 = logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_47.In(logic_uScript_GetCircuitChargeInfo_block_47, logic_uScript_GetCircuitChargeInfo_tech_47, logic_uScript_GetCircuitChargeInfo_blockType_47);
		local_Button3Value_System_Int32 = logic_uScript_GetCircuitChargeInfo_Return_47;
		if (logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_47.Out)
		{
			Relay_In_215();
		}
	}

	private void Relay_True_50()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_50.True(out logic_uScriptAct_SetBool_Target_50);
		local_Question03WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_50;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_50.Out)
		{
			Relay_False_105();
		}
	}

	private void Relay_False_50()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_50.False(out logic_uScriptAct_SetBool_Target_50);
		local_Question03WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_50;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_50.Out)
		{
			Relay_False_105();
		}
	}

	private void Relay_In_51()
	{
		logic_uScript_LockTechInteraction_tech_51 = local_ButtonBase3Tech_Tank;
		int num = 0;
		if (logic_uScript_LockTechInteraction_excludedBlocks_51.Length <= num)
		{
			Array.Resize(ref logic_uScript_LockTechInteraction_excludedBlocks_51, num + 1);
		}
		logic_uScript_LockTechInteraction_excludedBlocks_51[num++] = blockTypeButton3;
		int num2 = 0;
		if (logic_uScript_LockTechInteraction_excludedUniqueBlocks_51.Length <= num2)
		{
			Array.Resize(ref logic_uScript_LockTechInteraction_excludedUniqueBlocks_51, num2 + 1);
		}
		logic_uScript_LockTechInteraction_excludedUniqueBlocks_51[num2++] = local_ButtonBlock3_TankBlock;
		logic_uScript_LockTechInteraction_uScript_LockTechInteraction_51.In(logic_uScript_LockTechInteraction_tech_51, logic_uScript_LockTechInteraction_excludedBlocks_51, logic_uScript_LockTechInteraction_excludedUniqueBlocks_51);
		if (logic_uScript_LockTechInteraction_uScript_LockTechInteraction_51.Out)
		{
			Relay_In_41();
		}
	}

	private void Relay_In_52()
	{
		logic_uScriptCon_CompareInt_A_52 = local_Button2Value_System_Int32;
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_52.In(logic_uScriptCon_CompareInt_A_52, logic_uScriptCon_CompareInt_B_52);
		bool greaterThan = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_52.GreaterThan;
		bool lessThanOrEqualTo = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_52.LessThanOrEqualTo;
		if (greaterThan)
		{
			Relay_In_35();
		}
		if (lessThanOrEqualTo)
		{
			Relay_In_227();
		}
	}

	private void Relay_In_56()
	{
		logic_uScript_SetTankInvulnerable_tank_56 = local_ButtonBase4Tech_Tank;
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_56.In(logic_uScript_SetTankInvulnerable_invulnerable_56, logic_uScript_SetTankInvulnerable_tank_56);
		if (logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_56.Out)
		{
			Relay_In_109();
		}
	}

	private void Relay_In_61()
	{
		logic_uScriptCon_CompareInt_A_61 = local_Button1Value_System_Int32;
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_61.In(logic_uScriptCon_CompareInt_A_61, logic_uScriptCon_CompareInt_B_61);
		bool greaterThan = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_61.GreaterThan;
		bool lessThanOrEqualTo = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_61.LessThanOrEqualTo;
		if (greaterThan)
		{
			Relay_In_96();
		}
		if (lessThanOrEqualTo)
		{
			Relay_In_43();
		}
	}

	private void Relay_Pause_66()
	{
		logic_uScript_PausePopulation_uScript_PausePopulation_66.Pause();
		if (logic_uScript_PausePopulation_uScript_PausePopulation_66.Out)
		{
			Relay_In_107();
		}
	}

	private void Relay_UnPause_66()
	{
		logic_uScript_PausePopulation_uScript_PausePopulation_66.UnPause();
		if (logic_uScript_PausePopulation_uScript_PausePopulation_66.Out)
		{
			Relay_In_107();
		}
	}

	private void Relay_In_68()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_68.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_68.Out)
		{
			Relay_In_267();
		}
	}

	private void Relay_OnUpdate_70()
	{
		Relay_In_266();
	}

	private void Relay_OnSuspend_70()
	{
	}

	private void Relay_OnResume_70()
	{
	}

	private void Relay_Save_Out_71()
	{
		Relay_Save_347();
	}

	private void Relay_Load_Out_71()
	{
		Relay_Load_347();
	}

	private void Relay_Restart_Out_71()
	{
		Relay_Set_False_347();
	}

	private void Relay_Save_71()
	{
		logic_SubGraph_SaveLoadBool_boolean_71 = local_CanPushButtons_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_71 = local_CanPushButtons_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_71.Save(ref logic_SubGraph_SaveLoadBool_boolean_71, logic_SubGraph_SaveLoadBool_boolAsVariable_71, logic_SubGraph_SaveLoadBool_uniqueID_71);
	}

	private void Relay_Load_71()
	{
		logic_SubGraph_SaveLoadBool_boolean_71 = local_CanPushButtons_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_71 = local_CanPushButtons_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_71.Load(ref logic_SubGraph_SaveLoadBool_boolean_71, logic_SubGraph_SaveLoadBool_boolAsVariable_71, logic_SubGraph_SaveLoadBool_uniqueID_71);
	}

	private void Relay_Set_True_71()
	{
		logic_SubGraph_SaveLoadBool_boolean_71 = local_CanPushButtons_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_71 = local_CanPushButtons_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_71.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_71, logic_SubGraph_SaveLoadBool_boolAsVariable_71, logic_SubGraph_SaveLoadBool_uniqueID_71);
	}

	private void Relay_Set_False_71()
	{
		logic_SubGraph_SaveLoadBool_boolean_71 = local_CanPushButtons_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_71 = local_CanPushButtons_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_71.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_71, logic_SubGraph_SaveLoadBool_boolAsVariable_71, logic_SubGraph_SaveLoadBool_uniqueID_71);
	}

	private void Relay_True_72()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_72.True(out logic_uScriptAct_SetBool_Target_72);
		local_Question01WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_72;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_72.Out)
		{
			Relay_False_226();
		}
	}

	private void Relay_False_72()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_72.False(out logic_uScriptAct_SetBool_Target_72);
		local_Question01WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_72;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_72.Out)
		{
			Relay_False_226();
		}
	}

	private void Relay_In_73()
	{
		logic_uScript_LockTechInteraction_tech_73 = local_ButtonBase2Tech_Tank;
		int num = 0;
		if (logic_uScript_LockTechInteraction_excludedBlocks_73.Length <= num)
		{
			Array.Resize(ref logic_uScript_LockTechInteraction_excludedBlocks_73, num + 1);
		}
		logic_uScript_LockTechInteraction_excludedBlocks_73[num++] = blockTypeButton2;
		int num2 = 0;
		if (logic_uScript_LockTechInteraction_excludedUniqueBlocks_73.Length <= num2)
		{
			Array.Resize(ref logic_uScript_LockTechInteraction_excludedUniqueBlocks_73, num2 + 1);
		}
		logic_uScript_LockTechInteraction_excludedUniqueBlocks_73[num2++] = local_ButtonBlock2_TankBlock;
		logic_uScript_LockTechInteraction_uScript_LockTechInteraction_73.In(logic_uScript_LockTechInteraction_tech_73, logic_uScript_LockTechInteraction_excludedBlocks_73, logic_uScript_LockTechInteraction_excludedUniqueBlocks_73);
		if (logic_uScript_LockTechInteraction_uScript_LockTechInteraction_73.Out)
		{
			Relay_In_51();
		}
	}

	private void Relay_In_75()
	{
		logic_uScriptCon_CompareInt_A_75 = local_Button3Value_System_Int32;
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_75.In(logic_uScriptCon_CompareInt_A_75, logic_uScriptCon_CompareInt_B_75);
		bool greaterThan = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_75.GreaterThan;
		bool lessThanOrEqualTo = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_75.LessThanOrEqualTo;
		if (greaterThan)
		{
			Relay_In_39();
		}
		if (lessThanOrEqualTo)
		{
			Relay_In_190();
		}
	}

	private void Relay_In_76()
	{
		logic_uScript_LockTechInteraction_tech_76 = local_NPCTech_Tank;
		logic_uScript_LockTechInteraction_uScript_LockTechInteraction_76.In(logic_uScript_LockTechInteraction_tech_76, logic_uScript_LockTechInteraction_excludedBlocks_76, logic_uScript_LockTechInteraction_excludedUniqueBlocks_76);
		if (logic_uScript_LockTechInteraction_uScript_LockTechInteraction_76.Out)
		{
			Relay_In_23();
		}
	}

	private void Relay_In_82()
	{
		logic_uScript_LockTech_tech_82 = local_ButtonBase3Tech_Tank;
		logic_uScript_LockTech_uScript_LockTech_82.In(logic_uScript_LockTech_tech_82, logic_uScript_LockTech_lockType_82);
		if (logic_uScript_LockTech_uScript_LockTech_82.Out)
		{
			Relay_In_335();
		}
	}

	private void Relay_In_83()
	{
		logic_uScriptCon_CompareInt_A_83 = local_Button4Value_System_Int32;
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_83.In(logic_uScriptCon_CompareInt_A_83, logic_uScriptCon_CompareInt_B_83);
		if (logic_uScriptCon_CompareInt_uScriptCon_CompareInt_83.GreaterThan)
		{
			Relay_In_35();
		}
	}

	private void Relay_True_84()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_84.True(out logic_uScriptAct_SetBool_Target_84);
		local_Initialize_System_Boolean = logic_uScriptAct_SetBool_Target_84;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_84.Out)
		{
			Relay_InitialSpawn_88();
		}
	}

	private void Relay_False_84()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_84.False(out logic_uScriptAct_SetBool_Target_84);
		local_Initialize_System_Boolean = logic_uScriptAct_SetBool_Target_84;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_84.Out)
		{
			Relay_InitialSpawn_88();
		}
	}

	private void Relay_Out_85()
	{
	}

	private void Relay_In_85()
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_85 = local_Stage_System_Int32;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_85.In(ref logic_SubGraph_CompleteObjectiveStage_objectiveStage_85, logic_SubGraph_CompleteObjectiveStage_isFinalObjective_85);
	}

	private void Relay_In_86()
	{
		logic_uScriptCon_CompareInt_A_86 = local_Button1Value_System_Int32;
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_86.In(logic_uScriptCon_CompareInt_A_86, logic_uScriptCon_CompareInt_B_86);
		bool greaterThan = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_86.GreaterThan;
		bool lessThanOrEqualTo = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_86.LessThanOrEqualTo;
		if (greaterThan)
		{
			Relay_In_39();
		}
		if (lessThanOrEqualTo)
		{
			Relay_In_15();
		}
	}

	private void Relay_In_87()
	{
		logic_uScript_SetCustomRadarTeamID_tech_87 = local_ButtonBase3Tech_Tank;
		logic_uScript_SetCustomRadarTeamID_uScript_SetCustomRadarTeamID_87.In(logic_uScript_SetCustomRadarTeamID_tech_87, logic_uScript_SetCustomRadarTeamID_radarTeamID_87);
		if (logic_uScript_SetCustomRadarTeamID_uScript_SetCustomRadarTeamID_87.Out)
		{
			Relay_In_137();
		}
	}

	private void Relay_InitialSpawn_88()
	{
		int num = 0;
		Array array = buttonbase1SpawnData;
		if (logic_uScript_SpawnTechsFromData_spawnData_88.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_88, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SpawnTechsFromData_spawnData_88, num, array.Length);
		num += array.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_88 = owner_Connection_79;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_88.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_88, logic_uScript_SpawnTechsFromData_ownerNode_88, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_88, logic_uScript_SpawnTechsFromData_allowResurrection_88);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_88.Out)
		{
			Relay_InitialSpawn_314();
		}
	}

	private void Relay_True_89()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_89.True(out logic_uScriptAct_SetBool_Target_89);
		local_CanPushButtons_System_Boolean = logic_uScriptAct_SetBool_Target_89;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_89.Out)
		{
			Relay_In_410();
		}
	}

	private void Relay_False_89()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_89.False(out logic_uScriptAct_SetBool_Target_89);
		local_CanPushButtons_System_Boolean = logic_uScriptAct_SetBool_Target_89;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_89.Out)
		{
			Relay_In_410();
		}
	}

	private void Relay_In_90()
	{
		logic_uScript_Wait_seconds_90 = WaitingTime;
		logic_uScript_Wait_uScript_Wait_90.In(logic_uScript_Wait_seconds_90, logic_uScript_Wait_repeat_90);
		if (logic_uScript_Wait_uScript_Wait_90.Waited)
		{
			Relay_False_143();
		}
	}

	private void Relay_In_94()
	{
		logic_uScriptCon_CompareInt_A_94 = local_Button3Value_System_Int32;
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_94.In(logic_uScriptCon_CompareInt_A_94, logic_uScriptCon_CompareInt_B_94);
		bool greaterThan = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_94.GreaterThan;
		bool lessThanOrEqualTo = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_94.LessThanOrEqualTo;
		if (greaterThan)
		{
			Relay_In_35();
		}
		if (lessThanOrEqualTo)
		{
			Relay_In_201();
		}
	}

	private void Relay_In_95()
	{
		logic_uScriptCon_CompareInt_A_95 = local_Button2Value_System_Int32;
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_95.In(logic_uScriptCon_CompareInt_A_95, logic_uScriptCon_CompareInt_B_95);
		bool greaterThan = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_95.GreaterThan;
		bool lessThanOrEqualTo = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_95.LessThanOrEqualTo;
		if (greaterThan)
		{
			Relay_False_244();
		}
		if (lessThanOrEqualTo)
		{
			Relay_In_24();
		}
	}

	private void Relay_In_96()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_96.In(logic_uScriptAct_SetInt_Value_96, out logic_uScriptAct_SetInt_Target_96);
		local_Stage3QuestionTwo_System_Int32 = logic_uScriptAct_SetInt_Target_96;
		if (logic_uScriptAct_SetInt_uScriptAct_SetInt_96.Out)
		{
			Relay_True_113();
		}
	}

	private void Relay_In_98()
	{
		logic_uScript_LockTechInteraction_tech_98 = local_ButtonBase3Tech_Tank;
		logic_uScript_LockTechInteraction_uScript_LockTechInteraction_98.In(logic_uScript_LockTechInteraction_tech_98, logic_uScript_LockTechInteraction_excludedBlocks_98, logic_uScript_LockTechInteraction_excludedUniqueBlocks_98);
		if (logic_uScript_LockTechInteraction_uScript_LockTechInteraction_98.Out)
		{
			Relay_In_245();
		}
	}

	private void Relay_In_100()
	{
		logic_uScript_AddMessage_messageData_100 = msg06QuestionTwoCorrect;
		logic_uScript_AddMessage_speaker_100 = messageSpeaker;
		logic_uScript_AddMessage_Return_100 = logic_uScript_AddMessage_uScript_AddMessage_100.In(logic_uScript_AddMessage_messageData_100, logic_uScript_AddMessage_speaker_100);
		if (logic_uScript_AddMessage_uScript_AddMessage_100.Shown)
		{
			Relay_In_308();
		}
	}

	private void Relay_True_101()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_101.True(out logic_uScriptAct_SetBool_Target_101);
		local_CanPushButtons_System_Boolean = logic_uScriptAct_SetBool_Target_101;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_101.Out)
		{
			Relay_In_162();
		}
	}

	private void Relay_False_101()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_101.False(out logic_uScriptAct_SetBool_Target_101);
		local_CanPushButtons_System_Boolean = logic_uScriptAct_SetBool_Target_101;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_101.Out)
		{
			Relay_In_162();
		}
	}

	private void Relay_In_103()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_103.In(logic_uScriptAct_SetInt_Value_103, out logic_uScriptAct_SetInt_Target_103);
		local_Stage2QuestionOne_System_Int32 = logic_uScriptAct_SetInt_Target_103;
	}

	private void Relay_InitialSpawn_104()
	{
		int num = 0;
		Array array = buttonbase3SpawnData;
		if (logic_uScript_SpawnTechsFromData_spawnData_104.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_104, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SpawnTechsFromData_spawnData_104, num, array.Length);
		num += array.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_104 = owner_Connection_77;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_104.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_104, logic_uScript_SpawnTechsFromData_ownerNode_104, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_104, logic_uScript_SpawnTechsFromData_allowResurrection_104);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_104.Out)
		{
			Relay_InitialSpawn_287();
		}
	}

	private void Relay_True_105()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_105.True(out logic_uScriptAct_SetBool_Target_105);
		local_CanPushButtons_System_Boolean = logic_uScriptAct_SetBool_Target_105;
	}

	private void Relay_False_105()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_105.False(out logic_uScriptAct_SetBool_Target_105);
		local_CanPushButtons_System_Boolean = logic_uScriptAct_SetBool_Target_105;
	}

	private void Relay_In_107()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_107 = messageTag;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_107.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_107, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_107);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_107.Out)
		{
			Relay_In_12();
		}
	}

	private void Relay_In_109()
	{
		logic_uScript_SetCustomRadarTeamID_tech_109 = local_ButtonBase4Tech_Tank;
		logic_uScript_SetCustomRadarTeamID_uScript_SetCustomRadarTeamID_109.In(logic_uScript_SetCustomRadarTeamID_tech_109, logic_uScript_SetCustomRadarTeamID_radarTeamID_109);
		if (logic_uScript_SetCustomRadarTeamID_uScript_SetCustomRadarTeamID_109.Out)
		{
			Relay_In_319();
		}
	}

	private void Relay_In_111()
	{
		logic_uScript_GetCircuitChargeInfo_block_111 = local_ButtonBlock4_TankBlock;
		logic_uScript_GetCircuitChargeInfo_Return_111 = logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_111.In(logic_uScript_GetCircuitChargeInfo_block_111, logic_uScript_GetCircuitChargeInfo_tech_111, logic_uScript_GetCircuitChargeInfo_blockType_111);
		local_Button4Value_System_Int32 = logic_uScript_GetCircuitChargeInfo_Return_111;
		if (logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_111.Out)
		{
			Relay_In_277();
		}
	}

	private void Relay_True_113()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_113.True(out logic_uScriptAct_SetBool_Target_113);
		local_Question02WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_113;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_113.Out)
		{
			Relay_False_300();
		}
	}

	private void Relay_False_113()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_113.False(out logic_uScriptAct_SetBool_Target_113);
		local_Question02WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_113;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_113.Out)
		{
			Relay_False_300();
		}
	}

	private void Relay_True_116()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_116.True(out logic_uScriptAct_SetBool_Target_116);
		local_CanPushButtons_System_Boolean = logic_uScriptAct_SetBool_Target_116;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_116.Out)
		{
			Relay_In_285();
		}
	}

	private void Relay_False_116()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_116.False(out logic_uScriptAct_SetBool_Target_116);
		local_CanPushButtons_System_Boolean = logic_uScriptAct_SetBool_Target_116;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_116.Out)
		{
			Relay_In_285();
		}
	}

	private void Relay_Save_Out_117()
	{
		Relay_Save_490();
	}

	private void Relay_Load_Out_117()
	{
		Relay_Load_490();
	}

	private void Relay_Restart_Out_117()
	{
		Relay_Set_False_490();
	}

	private void Relay_Save_117()
	{
		logic_SubGraph_SaveLoadBool_boolean_117 = local_msgIntro_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_117 = local_msgIntro_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_117.Save(ref logic_SubGraph_SaveLoadBool_boolean_117, logic_SubGraph_SaveLoadBool_boolAsVariable_117, logic_SubGraph_SaveLoadBool_uniqueID_117);
	}

	private void Relay_Load_117()
	{
		logic_SubGraph_SaveLoadBool_boolean_117 = local_msgIntro_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_117 = local_msgIntro_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_117.Load(ref logic_SubGraph_SaveLoadBool_boolean_117, logic_SubGraph_SaveLoadBool_boolAsVariable_117, logic_SubGraph_SaveLoadBool_uniqueID_117);
	}

	private void Relay_Set_True_117()
	{
		logic_SubGraph_SaveLoadBool_boolean_117 = local_msgIntro_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_117 = local_msgIntro_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_117.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_117, logic_SubGraph_SaveLoadBool_boolAsVariable_117, logic_SubGraph_SaveLoadBool_uniqueID_117);
	}

	private void Relay_Set_False_117()
	{
		logic_SubGraph_SaveLoadBool_boolean_117 = local_msgIntro_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_117 = local_msgIntro_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_117.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_117, logic_SubGraph_SaveLoadBool_boolAsVariable_117, logic_SubGraph_SaveLoadBool_uniqueID_117);
	}

	private void Relay_In_120()
	{
		logic_uScript_GetTankBlock_tank_120 = local_ButtonBase1Tech_Tank;
		logic_uScript_GetTankBlock_blockType_120 = blockTypeButton1;
		logic_uScript_GetTankBlock_Return_120 = logic_uScript_GetTankBlock_uScript_GetTankBlock_120.In(logic_uScript_GetTankBlock_tank_120, logic_uScript_GetTankBlock_blockType_120);
		local_ButtonBlock1_TankBlock = logic_uScript_GetTankBlock_Return_120;
		if (logic_uScript_GetTankBlock_uScript_GetTankBlock_120.Returned)
		{
			Relay_In_123();
		}
	}

	private void Relay_In_123()
	{
		logic_uScript_GetTankBlock_tank_123 = local_ButtonBase2Tech_Tank;
		logic_uScript_GetTankBlock_blockType_123 = blockTypeButton2;
		logic_uScript_GetTankBlock_Return_123 = logic_uScript_GetTankBlock_uScript_GetTankBlock_123.In(logic_uScript_GetTankBlock_tank_123, logic_uScript_GetTankBlock_blockType_123);
		local_ButtonBlock2_TankBlock = logic_uScript_GetTankBlock_Return_123;
		if (logic_uScript_GetTankBlock_uScript_GetTankBlock_123.Returned)
		{
			Relay_In_206();
		}
	}

	private void Relay_In_124()
	{
		logic_uScriptCon_CompareInt_A_124 = local_Button1Value_System_Int32;
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_124.In(logic_uScriptCon_CompareInt_A_124, logic_uScriptCon_CompareInt_B_124);
		bool greaterThan = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_124.GreaterThan;
		bool lessThanOrEqualTo = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_124.LessThanOrEqualTo;
		if (greaterThan)
		{
			Relay_False_72();
		}
		if (lessThanOrEqualTo)
		{
			Relay_In_217();
		}
	}

	private void Relay_In_132()
	{
		logic_uScript_GetTankBlock_tank_132 = local_ButtonBase4Tech_Tank;
		logic_uScript_GetTankBlock_blockType_132 = blockTypeButton4;
		logic_uScript_GetTankBlock_Return_132 = logic_uScript_GetTankBlock_uScript_GetTankBlock_132.In(logic_uScript_GetTankBlock_tank_132, logic_uScript_GetTankBlock_blockType_132);
		local_ButtonBlock4_TankBlock = logic_uScript_GetTankBlock_Return_132;
		if (logic_uScript_GetTankBlock_uScript_GetTankBlock_132.Returned)
		{
			Relay_In_291();
		}
	}

	private void Relay_AtIndex_133()
	{
		int num = 0;
		Array array = local_114_TankArray;
		if (logic_uScript_AccessListTech_techList_133.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_133, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_133, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_133.AtIndex(ref logic_uScript_AccessListTech_techList_133, logic_uScript_AccessListTech_index_133, out logic_uScript_AccessListTech_value_133);
		local_114_TankArray = logic_uScript_AccessListTech_techList_133;
		local_ButtonBase1Tech_Tank = logic_uScript_AccessListTech_value_133;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_133.Out)
		{
			Relay_In_160();
		}
	}

	private void Relay_Output1_134()
	{
		Relay_True_286();
	}

	private void Relay_Output2_134()
	{
		Relay_In_100();
	}

	private void Relay_Output3_134()
	{
		Relay_In_183();
	}

	private void Relay_Output4_134()
	{
		Relay_In_415();
	}

	private void Relay_Output5_134()
	{
	}

	private void Relay_Output6_134()
	{
	}

	private void Relay_Output7_134()
	{
	}

	private void Relay_Output8_134()
	{
	}

	private void Relay_In_134()
	{
		logic_uScriptCon_ManualSwitch_CurrentOutput_134 = local_Stage3QuestionTwo_System_Int32;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_134.In(logic_uScriptCon_ManualSwitch_CurrentOutput_134);
	}

	private void Relay_In_137()
	{
		logic_uScript_SetTankHideBlockLimit_tech_137 = local_ButtonBase3Tech_Tank;
		logic_uScript_SetTankHideBlockLimit_uScript_SetTankHideBlockLimit_137.In(logic_uScript_SetTankHideBlockLimit_hidden_137, logic_uScript_SetTankHideBlockLimit_tech_137);
		if (logic_uScript_SetTankHideBlockLimit_uScript_SetTankHideBlockLimit_137.Out)
		{
			Relay_In_165();
		}
	}

	private void Relay_In_138()
	{
		logic_uScript_LockTechInteraction_tech_138 = local_ButtonBase2Tech_Tank;
		logic_uScript_LockTechInteraction_uScript_LockTechInteraction_138.In(logic_uScript_LockTechInteraction_tech_138, logic_uScript_LockTechInteraction_excludedBlocks_138, logic_uScript_LockTechInteraction_excludedUniqueBlocks_138);
		if (logic_uScript_LockTechInteraction_uScript_LockTechInteraction_138.Out)
		{
			Relay_In_98();
		}
	}

	private void Relay_In_139()
	{
		logic_uScript_Wait_seconds_139 = WaitingTime;
		logic_uScript_Wait_uScript_Wait_139.In(logic_uScript_Wait_seconds_139, logic_uScript_Wait_repeat_139);
		if (logic_uScript_Wait_uScript_Wait_139.Waited)
		{
			Relay_False_31();
		}
	}

	private void Relay_True_143()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_143.True(out logic_uScriptAct_SetBool_Target_143);
		local_Question02WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_143;
	}

	private void Relay_False_143()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_143.False(out logic_uScriptAct_SetBool_Target_143);
		local_Question02WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_143;
	}

	private void Relay_In_149()
	{
		logic_uScript_SetCustomRadarTeamID_tech_149 = local_ButtonBase1Tech_Tank;
		logic_uScript_SetCustomRadarTeamID_uScript_SetCustomRadarTeamID_149.In(logic_uScript_SetCustomRadarTeamID_tech_149, logic_uScript_SetCustomRadarTeamID_radarTeamID_149);
		if (logic_uScript_SetCustomRadarTeamID_uScript_SetCustomRadarTeamID_149.Out)
		{
			Relay_In_238();
		}
	}

	private void Relay_True_150()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_150.True(out logic_uScriptAct_SetBool_Target_150);
		local_NearNPC_System_Boolean = logic_uScriptAct_SetBool_Target_150;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_150.Out)
		{
			Relay_In_331();
		}
	}

	private void Relay_False_150()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_150.False(out logic_uScriptAct_SetBool_Target_150);
		local_NearNPC_System_Boolean = logic_uScriptAct_SetBool_Target_150;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_150.Out)
		{
			Relay_In_331();
		}
	}

	private void Relay_In_151()
	{
		int num = 0;
		Array array = buttonbase3SpawnData;
		if (logic_uScript_GetAndCheckTechs_techData_151.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_151, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techData_151, num, array.Length);
		num += array.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_151 = owner_Connection_192;
		int num2 = 0;
		Array array2 = local_223_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_151.Length != num2 + array2.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_151, num2 + array2.Length);
		}
		Array.Copy(array2, 0, logic_uScript_GetAndCheckTechs_techs_151, num2, array2.Length);
		num2 += array2.Length;
		logic_uScript_GetAndCheckTechs_Return_151 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_151.In(logic_uScript_GetAndCheckTechs_techData_151, logic_uScript_GetAndCheckTechs_ownerNode_151, ref logic_uScript_GetAndCheckTechs_techs_151);
		local_223_TankArray = logic_uScript_GetAndCheckTechs_techs_151;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_151.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_151.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_151.AllDead;
		if (allAlive)
		{
			Relay_AtIndex_255();
		}
		if (someAlive)
		{
			Relay_AtIndex_255();
		}
		if (allDead)
		{
			Relay_In_288();
		}
	}

	private void Relay_Pause_155()
	{
		logic_uScript_PausePopulation_uScript_PausePopulation_155.Pause();
		if (logic_uScript_PausePopulation_uScript_PausePopulation_155.Out)
		{
			Relay_True_150();
		}
	}

	private void Relay_UnPause_155()
	{
		logic_uScript_PausePopulation_uScript_PausePopulation_155.UnPause();
		if (logic_uScript_PausePopulation_uScript_PausePopulation_155.Out)
		{
			Relay_True_150();
		}
	}

	private void Relay_In_157()
	{
		logic_uScriptCon_CompareInt_A_157 = local_Button2Value_System_Int32;
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_157.In(logic_uScriptCon_CompareInt_A_157, logic_uScriptCon_CompareInt_B_157);
		bool greaterThan = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_157.GreaterThan;
		bool lessThanOrEqualTo = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_157.LessThanOrEqualTo;
		if (greaterThan)
		{
			Relay_In_96();
		}
		if (lessThanOrEqualTo)
		{
			Relay_In_47();
		}
	}

	private void Relay_In_160()
	{
		logic_uScript_LockTech_tech_160 = local_ButtonBase1Tech_Tank;
		logic_uScript_LockTech_uScript_LockTech_160.In(logic_uScript_LockTech_tech_160, logic_uScript_LockTech_lockType_160);
		if (logic_uScript_LockTech_uScript_LockTech_160.Out)
		{
			Relay_In_237();
		}
	}

	private void Relay_In_161()
	{
		logic_uScript_AddMessage_messageData_161 = msg08QuestionThree;
		logic_uScript_AddMessage_speaker_161 = messageSpeaker;
		logic_uScript_AddMessage_Return_161 = logic_uScript_AddMessage_uScript_AddMessage_161.In(logic_uScript_AddMessage_messageData_161, logic_uScript_AddMessage_speaker_161);
		if (logic_uScript_AddMessage_uScript_AddMessage_161.Out)
		{
			Relay_In_322();
		}
	}

	private void Relay_In_162()
	{
		logic_uScript_GetCircuitChargeInfo_block_162 = local_ButtonBlock1_TankBlock;
		logic_uScript_GetCircuitChargeInfo_Return_162 = logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_162.In(logic_uScript_GetCircuitChargeInfo_block_162, logic_uScript_GetCircuitChargeInfo_tech_162, logic_uScript_GetCircuitChargeInfo_blockType_162);
		local_Button1Value_System_Int32 = logic_uScript_GetCircuitChargeInfo_Return_162;
		if (logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_162.Out)
		{
			Relay_In_124();
		}
	}

	private void Relay_In_163()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_163.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_163.Out)
		{
			Relay_In_315();
		}
	}

	private void Relay_In_165()
	{
		int num = 0;
		Array array = buttonbase4SpawnData;
		if (logic_uScript_GetAndCheckTechs_techData_165.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_165, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techData_165, num, array.Length);
		num += array.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_165 = owner_Connection_251;
		int num2 = 0;
		Array array2 = local_53_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_165.Length != num2 + array2.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_165, num2 + array2.Length);
		}
		Array.Copy(array2, 0, logic_uScript_GetAndCheckTechs_techs_165, num2, array2.Length);
		num2 += array2.Length;
		logic_uScript_GetAndCheckTechs_Return_165 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_165.In(logic_uScript_GetAndCheckTechs_techData_165, logic_uScript_GetAndCheckTechs_ownerNode_165, ref logic_uScript_GetAndCheckTechs_techs_165);
		local_53_TankArray = logic_uScript_GetAndCheckTechs_techs_165;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_165.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_165.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_165.AllDead;
		if (allAlive)
		{
			Relay_AtIndex_228();
		}
		if (someAlive)
		{
			Relay_AtIndex_228();
		}
		if (allDead)
		{
			Relay_In_208();
		}
	}

	private void Relay_In_168()
	{
		logic_uScript_SetTankInvulnerable_tank_168 = local_NPCTech_Tank;
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_168.In(logic_uScript_SetTankInvulnerable_invulnerable_168, logic_uScript_SetTankInvulnerable_tank_168);
		if (logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_168.Out)
		{
			Relay_In_219();
		}
	}

	private void Relay_True_169()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_169.True(out logic_uScriptAct_SetBool_Target_169);
		local_CanPushButtons_System_Boolean = logic_uScriptAct_SetBool_Target_169;
	}

	private void Relay_False_169()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_169.False(out logic_uScriptAct_SetBool_Target_169);
		local_CanPushButtons_System_Boolean = logic_uScriptAct_SetBool_Target_169;
	}

	private void Relay_In_172()
	{
		logic_uScript_LockTech_tech_172 = local_ButtonBase4Tech_Tank;
		logic_uScript_LockTech_uScript_LockTech_172.In(logic_uScript_LockTech_tech_172, logic_uScript_LockTech_lockType_172);
		if (logic_uScript_LockTech_uScript_LockTech_172.Out)
		{
			Relay_In_56();
		}
	}

	private void Relay_In_174()
	{
		logic_uScript_AddMessage_messageData_174 = msg04QuestionOneWrong;
		logic_uScript_AddMessage_speaker_174 = messageSpeaker;
		logic_uScript_AddMessage_Return_174 = logic_uScript_AddMessage_uScript_AddMessage_174.In(logic_uScript_AddMessage_messageData_174, logic_uScript_AddMessage_speaker_174);
		if (logic_uScript_AddMessage_uScript_AddMessage_174.Out)
		{
			Relay_In_139();
		}
	}

	private void Relay_In_176()
	{
		logic_uScript_LockTechInteraction_tech_176 = local_ButtonBase1Tech_Tank;
		logic_uScript_LockTechInteraction_uScript_LockTechInteraction_176.In(logic_uScript_LockTechInteraction_tech_176, logic_uScript_LockTechInteraction_excludedBlocks_176, logic_uScript_LockTechInteraction_excludedUniqueBlocks_176);
		if (logic_uScript_LockTechInteraction_uScript_LockTechInteraction_176.Out)
		{
			Relay_In_138();
		}
	}

	private void Relay_SaveEvent_182()
	{
		Relay_Save_279();
	}

	private void Relay_LoadEvent_182()
	{
		Relay_Load_279();
	}

	private void Relay_RestartEvent_182()
	{
		Relay_Restart_279();
	}

	private void Relay_In_183()
	{
		logic_uScriptCon_CompareBool_Bool_183 = local_Question02WrongAnswers_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_183.In(logic_uScriptCon_CompareBool_Bool_183);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_183.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_183.False;
		if (num)
		{
			Relay_In_260();
		}
		if (flag)
		{
			Relay_In_363();
		}
	}

	private void Relay_In_186()
	{
		logic_uScript_AddMessage_messageData_186 = msg03QuestionOneCorrect;
		logic_uScript_AddMessage_speaker_186 = messageSpeaker;
		logic_uScript_AddMessage_Return_186 = logic_uScript_AddMessage_uScript_AddMessage_186.In(logic_uScript_AddMessage_messageData_186, logic_uScript_AddMessage_speaker_186);
		if (logic_uScript_AddMessage_uScript_AddMessage_186.Shown)
		{
			Relay_In_85();
		}
	}

	private void Relay_In_187()
	{
		logic_uScript_IsPlayerInRangeOfTech_tech_187 = local_NPCTech_Tank;
		logic_uScript_IsPlayerInRangeOfTech_range_187 = distNPCFound;
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_187.In(logic_uScript_IsPlayerInRangeOfTech_tech_187, logic_uScript_IsPlayerInRangeOfTech_range_187, logic_uScript_IsPlayerInRangeOfTech_techs_187);
		bool inRange = logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_187.InRange;
		bool outOfRange = logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_187.OutOfRange;
		if (inRange)
		{
			Relay_Pause_155();
		}
		if (outOfRange)
		{
			Relay_UnPause_66();
		}
	}

	private void Relay_Save_Out_189()
	{
		Relay_Save_324();
	}

	private void Relay_Load_Out_189()
	{
		Relay_Load_324();
	}

	private void Relay_Restart_Out_189()
	{
		Relay_Restart_324();
	}

	private void Relay_Save_189()
	{
		logic_SubGraph_SaveLoadInt_integer_189 = local_Stage3QuestionTwo_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_189 = local_Stage3QuestionTwo_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_189.Save(logic_SubGraph_SaveLoadInt_restartValue_189, ref logic_SubGraph_SaveLoadInt_integer_189, logic_SubGraph_SaveLoadInt_intAsVariable_189, logic_SubGraph_SaveLoadInt_uniqueID_189);
	}

	private void Relay_Load_189()
	{
		logic_SubGraph_SaveLoadInt_integer_189 = local_Stage3QuestionTwo_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_189 = local_Stage3QuestionTwo_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_189.Load(logic_SubGraph_SaveLoadInt_restartValue_189, ref logic_SubGraph_SaveLoadInt_integer_189, logic_SubGraph_SaveLoadInt_intAsVariable_189, logic_SubGraph_SaveLoadInt_uniqueID_189);
	}

	private void Relay_Restart_189()
	{
		logic_SubGraph_SaveLoadInt_integer_189 = local_Stage3QuestionTwo_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_189 = local_Stage3QuestionTwo_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_189.Restart(logic_SubGraph_SaveLoadInt_restartValue_189, ref logic_SubGraph_SaveLoadInt_integer_189, logic_SubGraph_SaveLoadInt_intAsVariable_189, logic_SubGraph_SaveLoadInt_uniqueID_189);
	}

	private void Relay_In_190()
	{
		logic_uScript_GetCircuitChargeInfo_block_190 = local_ButtonBlock4_TankBlock;
		logic_uScript_GetCircuitChargeInfo_Return_190 = logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_190.In(logic_uScript_GetCircuitChargeInfo_block_190, logic_uScript_GetCircuitChargeInfo_tech_190, logic_uScript_GetCircuitChargeInfo_blockType_190);
		local_Button4Value_System_Int32 = logic_uScript_GetCircuitChargeInfo_Return_190;
		if (logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_190.Out)
		{
			Relay_In_30();
		}
	}

	private void Relay_In_193()
	{
		logic_uScript_AddMessage_messageData_193 = msg10QuestionThreeWrong;
		logic_uScript_AddMessage_speaker_193 = messageSpeaker;
		logic_uScript_AddMessage_Return_193 = logic_uScript_AddMessage_uScript_AddMessage_193.In(logic_uScript_AddMessage_messageData_193, logic_uScript_AddMessage_speaker_193);
		if (logic_uScript_AddMessage_uScript_AddMessage_193.Out)
		{
			Relay_In_313();
		}
	}

	private void Relay_In_194()
	{
		int num = 0;
		Array array = buttonbase2SpawnData;
		if (logic_uScript_GetAndCheckTechs_techData_194.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_194, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techData_194, num, array.Length);
		num += array.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_194 = owner_Connection_3;
		int num2 = 0;
		Array array2 = local_152_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_194.Length != num2 + array2.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_194, num2 + array2.Length);
		}
		Array.Copy(array2, 0, logic_uScript_GetAndCheckTechs_techs_194, num2, array2.Length);
		num2 += array2.Length;
		logic_uScript_GetAndCheckTechs_Return_194 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_194.In(logic_uScript_GetAndCheckTechs_techData_194, logic_uScript_GetAndCheckTechs_ownerNode_194, ref logic_uScript_GetAndCheckTechs_techs_194);
		local_152_TankArray = logic_uScript_GetAndCheckTechs_techs_194;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_194.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_194.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_194.AllDead;
		if (allAlive)
		{
			Relay_AtIndex_211();
		}
		if (someAlive)
		{
			Relay_AtIndex_211();
		}
		if (allDead)
		{
			Relay_In_42();
		}
	}

	private void Relay_In_198()
	{
		logic_uScriptCon_CompareBool_Bool_198 = local_msgIntro_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_198.In(logic_uScriptCon_CompareBool_Bool_198);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_198.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_198.False;
		if (num)
		{
			Relay_In_187();
		}
		if (flag)
		{
			Relay_True_486();
		}
	}

	private void Relay_In_199()
	{
		logic_uScript_GetCircuitChargeInfo_block_199 = local_ButtonBlock1_TankBlock;
		logic_uScript_GetCircuitChargeInfo_Return_199 = logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_199.In(logic_uScript_GetCircuitChargeInfo_block_199, logic_uScript_GetCircuitChargeInfo_tech_199, logic_uScript_GetCircuitChargeInfo_blockType_199);
		local_Button1Value_System_Int32 = logic_uScript_GetCircuitChargeInfo_Return_199;
		if (logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_199.Out)
		{
			Relay_In_61();
		}
	}

	private void Relay_In_201()
	{
		logic_uScript_GetCircuitChargeInfo_block_201 = local_ButtonBlock4_TankBlock;
		logic_uScript_GetCircuitChargeInfo_Return_201 = logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_201.In(logic_uScript_GetCircuitChargeInfo_block_201, logic_uScript_GetCircuitChargeInfo_tech_201, logic_uScript_GetCircuitChargeInfo_blockType_201);
		local_Button4Value_System_Int32 = logic_uScript_GetCircuitChargeInfo_Return_201;
		if (logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_201.Out)
		{
			Relay_In_83();
		}
	}

	private void Relay_In_206()
	{
		logic_uScript_GetTankBlock_tank_206 = local_ButtonBase3Tech_Tank;
		logic_uScript_GetTankBlock_blockType_206 = blockTypeButton3;
		logic_uScript_GetTankBlock_Return_206 = logic_uScript_GetTankBlock_uScript_GetTankBlock_206.In(logic_uScript_GetTankBlock_tank_206, logic_uScript_GetTankBlock_blockType_206);
		local_ButtonBlock3_TankBlock = logic_uScript_GetTankBlock_Return_206;
		if (logic_uScript_GetTankBlock_uScript_GetTankBlock_206.Returned)
		{
			Relay_In_132();
		}
	}

	private void Relay_In_208()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_208.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_208.Out)
		{
			Relay_In_163();
		}
	}

	private void Relay_AtIndex_211()
	{
		int num = 0;
		Array array = local_152_TankArray;
		if (logic_uScript_AccessListTech_techList_211.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_211, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_211, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_211.AtIndex(ref logic_uScript_AccessListTech_techList_211, logic_uScript_AccessListTech_index_211, out logic_uScript_AccessListTech_value_211);
		local_152_TankArray = logic_uScript_AccessListTech_techList_211;
		local_ButtonBase2Tech_Tank = logic_uScript_AccessListTech_value_211;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_211.Out)
		{
			Relay_In_259();
		}
	}

	private void Relay_In_212()
	{
		logic_uScript_SetEncounterTarget_owner_212 = owner_Connection_29;
		logic_uScript_SetEncounterTarget_visibleObject_212 = local_NPCTech_Tank;
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_212.In(logic_uScript_SetEncounterTarget_owner_212, logic_uScript_SetEncounterTarget_visibleObject_212);
		if (logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_212.Out)
		{
			Relay_In_120();
		}
	}

	private void Relay_In_215()
	{
		logic_uScriptCon_CompareInt_A_215 = local_Button3Value_System_Int32;
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_215.In(logic_uScriptCon_CompareInt_A_215, logic_uScriptCon_CompareInt_B_215);
		bool greaterThan = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_215.GreaterThan;
		bool lessThanOrEqualTo = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_215.LessThanOrEqualTo;
		if (greaterThan)
		{
			Relay_False_392();
		}
		if (lessThanOrEqualTo)
		{
			Relay_In_111();
		}
	}

	private void Relay_In_217()
	{
		logic_uScript_GetCircuitChargeInfo_block_217 = local_ButtonBlock2_TankBlock;
		logic_uScript_GetCircuitChargeInfo_Return_217 = logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_217.In(logic_uScript_GetCircuitChargeInfo_block_217, logic_uScript_GetCircuitChargeInfo_tech_217, logic_uScript_GetCircuitChargeInfo_blockType_217);
		local_Button2Value_System_Int32 = logic_uScript_GetCircuitChargeInfo_Return_217;
		if (logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_217.Out)
		{
			Relay_In_52();
		}
	}

	private void Relay_In_218()
	{
		logic_uScriptCon_CompareBool_Bool_218 = local_msgBaseFoundShown_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_218.In(logic_uScriptCon_CompareBool_Bool_218);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_218.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_218.False;
		if (num)
		{
			Relay_In_294();
		}
		if (flag)
		{
			Relay_In_1062();
		}
	}

	private void Relay_In_219()
	{
		logic_uScript_LockTech_tech_219 = local_NPCTech_Tank;
		logic_uScript_LockTech_uScript_LockTech_219.In(logic_uScript_LockTech_tech_219, logic_uScript_LockTech_lockType_219);
		if (logic_uScript_LockTech_uScript_LockTech_219.Out)
		{
			Relay_In_76();
		}
	}

	private void Relay_AtIndex_224()
	{
		int num = 0;
		Array array = local_171_TankArray;
		if (logic_uScript_AccessListTech_techList_224.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_224, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_224, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_224.AtIndex(ref logic_uScript_AccessListTech_techList_224, logic_uScript_AccessListTech_index_224, out logic_uScript_AccessListTech_value_224);
		local_171_TankArray = logic_uScript_AccessListTech_techList_224;
		local_NPCTech_Tank = logic_uScript_AccessListTech_value_224;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_224.Out)
		{
			Relay_In_168();
		}
	}

	private void Relay_True_226()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_226.True(out logic_uScriptAct_SetBool_Target_226);
		local_CanPushButtons_System_Boolean = logic_uScriptAct_SetBool_Target_226;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_226.Out)
		{
			Relay_In_103();
		}
	}

	private void Relay_False_226()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_226.False(out logic_uScriptAct_SetBool_Target_226);
		local_CanPushButtons_System_Boolean = logic_uScriptAct_SetBool_Target_226;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_226.Out)
		{
			Relay_In_103();
		}
	}

	private void Relay_In_227()
	{
		logic_uScript_GetCircuitChargeInfo_block_227 = local_ButtonBlock3_TankBlock;
		logic_uScript_GetCircuitChargeInfo_Return_227 = logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_227.In(logic_uScript_GetCircuitChargeInfo_block_227, logic_uScript_GetCircuitChargeInfo_tech_227, logic_uScript_GetCircuitChargeInfo_blockType_227);
		local_Button3Value_System_Int32 = logic_uScript_GetCircuitChargeInfo_Return_227;
		if (logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_227.Out)
		{
			Relay_In_94();
		}
	}

	private void Relay_AtIndex_228()
	{
		int num = 0;
		Array array = local_53_TankArray;
		if (logic_uScript_AccessListTech_techList_228.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_228, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_228, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_228.AtIndex(ref logic_uScript_AccessListTech_techList_228, logic_uScript_AccessListTech_index_228, out logic_uScript_AccessListTech_value_228);
		local_53_TankArray = logic_uScript_AccessListTech_techList_228;
		local_ButtonBase4Tech_Tank = logic_uScript_AccessListTech_value_228;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_228.Out)
		{
			Relay_In_172();
		}
	}

	private void Relay_In_229()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_229.In(logic_uScriptAct_SetInt_Value_229, out logic_uScriptAct_SetInt_Target_229);
		local_Stage2QuestionOne_System_Int32 = logic_uScriptAct_SetInt_Target_229;
	}

	private void Relay_Output1_233()
	{
		Relay_In_218();
	}

	private void Relay_Output2_233()
	{
		Relay_In_186();
	}

	private void Relay_Output3_233()
	{
		Relay_In_262();
	}

	private void Relay_Output4_233()
	{
		Relay_In_409();
	}

	private void Relay_Output5_233()
	{
	}

	private void Relay_Output6_233()
	{
	}

	private void Relay_Output7_233()
	{
	}

	private void Relay_Output8_233()
	{
	}

	private void Relay_In_233()
	{
		logic_uScriptCon_ManualSwitch_CurrentOutput_233 = local_Stage2QuestionOne_System_Int32;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_233.In(logic_uScriptCon_ManualSwitch_CurrentOutput_233);
	}

	private void Relay_In_236()
	{
		int num = 0;
		Array array = buttonbase1SpawnData;
		if (logic_uScript_GetAndCheckTechs_techData_236.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_236, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techData_236, num, array.Length);
		num += array.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_236 = owner_Connection_110;
		int num2 = 0;
		Array array2 = local_114_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_236.Length != num2 + array2.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_236, num2 + array2.Length);
		}
		Array.Copy(array2, 0, logic_uScript_GetAndCheckTechs_techs_236, num2, array2.Length);
		num2 += array2.Length;
		logic_uScript_GetAndCheckTechs_Return_236 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_236.In(logic_uScript_GetAndCheckTechs_techData_236, logic_uScript_GetAndCheckTechs_ownerNode_236, ref logic_uScript_GetAndCheckTechs_techs_236);
		local_114_TankArray = logic_uScript_GetAndCheckTechs_techs_236;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_236.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_236.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_236.AllDead;
		if (allAlive)
		{
			Relay_AtIndex_133();
		}
		if (someAlive)
		{
			Relay_AtIndex_133();
		}
		if (allDead)
		{
			Relay_In_248();
		}
	}

	private void Relay_In_237()
	{
		logic_uScript_SetTankInvulnerable_tank_237 = local_ButtonBase1Tech_Tank;
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_237.In(logic_uScript_SetTankInvulnerable_invulnerable_237, logic_uScript_SetTankInvulnerable_tank_237);
		if (logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_237.Out)
		{
			Relay_In_149();
		}
	}

	private void Relay_In_238()
	{
		logic_uScript_SetTankHideBlockLimit_tech_238 = local_ButtonBase1Tech_Tank;
		logic_uScript_SetTankHideBlockLimit_uScript_SetTankHideBlockLimit_238.In(logic_uScript_SetTankHideBlockLimit_hidden_238, logic_uScript_SetTankHideBlockLimit_tech_238);
		if (logic_uScript_SetTankHideBlockLimit_uScript_SetTankHideBlockLimit_238.Out)
		{
			Relay_In_194();
		}
	}

	private void Relay_In_240()
	{
		logic_uScript_SetTankHideBlockLimit_tech_240 = local_ButtonBase2Tech_Tank;
		logic_uScript_SetTankHideBlockLimit_uScript_SetTankHideBlockLimit_240.In(logic_uScript_SetTankHideBlockLimit_hidden_240, logic_uScript_SetTankHideBlockLimit_tech_240);
		if (logic_uScript_SetTankHideBlockLimit_uScript_SetTankHideBlockLimit_240.Out)
		{
			Relay_In_151();
		}
	}

	private void Relay_In_241()
	{
		logic_uScript_RemoveScenery_ownerNode_241 = owner_Connection_78;
		logic_uScript_RemoveScenery_positionName_241 = ButtonBasePosition;
		logic_uScript_RemoveScenery_radius_241 = clearSceneryRadius;
		logic_uScript_RemoveScenery_uScript_RemoveScenery_241.In(logic_uScript_RemoveScenery_ownerNode_241, logic_uScript_RemoveScenery_positionName_241, logic_uScript_RemoveScenery_radius_241, logic_uScript_RemoveScenery_preventChunksSpawning_241);
		if (logic_uScript_RemoveScenery_uScript_RemoveScenery_241.Out)
		{
			Relay_In_236();
		}
	}

	private void Relay_True_244()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_244.True(out logic_uScriptAct_SetBool_Target_244);
		local_Question03WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_244;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_244.Out)
		{
			Relay_False_116();
		}
	}

	private void Relay_False_244()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_244.False(out logic_uScriptAct_SetBool_Target_244);
		local_Question03WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_244;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_244.Out)
		{
			Relay_False_116();
		}
	}

	private void Relay_In_245()
	{
		logic_uScript_LockTechInteraction_tech_245 = local_ButtonBase4Tech_Tank;
		logic_uScript_LockTechInteraction_uScript_LockTechInteraction_245.In(logic_uScript_LockTechInteraction_tech_245, logic_uScript_LockTechInteraction_excludedBlocks_245, logic_uScript_LockTechInteraction_excludedUniqueBlocks_245);
		if (logic_uScript_LockTechInteraction_uScript_LockTechInteraction_245.Out)
		{
			Relay_In_487();
		}
	}

	private void Relay_In_248()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_248.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_248.Out)
		{
			Relay_In_21();
		}
	}

	private void Relay_AtIndex_255()
	{
		int num = 0;
		Array array = local_223_TankArray;
		if (logic_uScript_AccessListTech_techList_255.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_255, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_255, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_255.AtIndex(ref logic_uScript_AccessListTech_techList_255, logic_uScript_AccessListTech_index_255, out logic_uScript_AccessListTech_value_255);
		local_223_TankArray = logic_uScript_AccessListTech_techList_255;
		local_ButtonBase3Tech_Tank = logic_uScript_AccessListTech_value_255;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_255.Out)
		{
			Relay_In_82();
		}
	}

	private void Relay_In_259()
	{
		logic_uScript_LockTech_tech_259 = local_ButtonBase2Tech_Tank;
		logic_uScript_LockTech_uScript_LockTech_259.In(logic_uScript_LockTech_tech_259, logic_uScript_LockTech_lockType_259);
		if (logic_uScript_LockTech_uScript_LockTech_259.Out)
		{
			Relay_In_20();
		}
	}

	private void Relay_In_260()
	{
		logic_uScript_AddMessage_messageData_260 = msg07QuestionTwoWrong;
		logic_uScript_AddMessage_speaker_260 = messageSpeaker;
		logic_uScript_AddMessage_Return_260 = logic_uScript_AddMessage_uScript_AddMessage_260.In(logic_uScript_AddMessage_messageData_260, logic_uScript_AddMessage_speaker_260);
		if (logic_uScript_AddMessage_uScript_AddMessage_260.Out)
		{
			Relay_In_90();
		}
	}

	private void Relay_In_261()
	{
		logic_uScript_AddMessage_messageData_261 = msg01Intro;
		logic_uScript_AddMessage_speaker_261 = messageSpeaker;
		logic_uScript_AddMessage_Return_261 = logic_uScript_AddMessage_uScript_AddMessage_261.In(logic_uScript_AddMessage_messageData_261, logic_uScript_AddMessage_speaker_261);
		if (logic_uScript_AddMessage_uScript_AddMessage_261.Out)
		{
			Relay_In_187();
		}
	}

	private void Relay_In_262()
	{
		logic_uScriptCon_CompareBool_Bool_262 = local_Question01WrongAnswers_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_262.In(logic_uScriptCon_CompareBool_Bool_262);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_262.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_262.False;
		if (num)
		{
			Relay_In_174();
		}
		if (flag)
		{
			Relay_In_340();
		}
	}

	private void Relay_In_264()
	{
		logic_uScript_AddMessage_messageData_264 = msg05QuestionTwo;
		logic_uScript_AddMessage_speaker_264 = messageSpeaker;
		logic_uScript_AddMessage_Return_264 = logic_uScript_AddMessage_uScript_AddMessage_264.In(logic_uScript_AddMessage_messageData_264, logic_uScript_AddMessage_speaker_264);
		if (logic_uScript_AddMessage_uScript_AddMessage_264.Out)
		{
			Relay_In_199();
		}
	}

	private void Relay_In_266()
	{
		logic_uScriptCon_CompareBool_Bool_266 = local_Initialize_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_266.In(logic_uScriptCon_CompareBool_Bool_266);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_266.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_266.False;
		if (num)
		{
			Relay_In_328();
		}
		if (flag)
		{
			Relay_True_84();
		}
	}

	private void Relay_In_267()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_267.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_267.Out)
		{
			Relay_In_311();
		}
	}

	private void Relay_True_269()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_269.True(out logic_uScriptAct_SetBool_Target_269);
		local_NearNPC_System_Boolean = logic_uScriptAct_SetBool_Target_269;
	}

	private void Relay_False_269()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_269.False(out logic_uScriptAct_SetBool_Target_269);
		local_NearNPC_System_Boolean = logic_uScriptAct_SetBool_Target_269;
	}

	private void Relay_True_274()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_274.True(out logic_uScriptAct_SetBool_Target_274);
		local_Question01WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_274;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_274.Out)
		{
			Relay_False_169();
		}
	}

	private void Relay_False_274()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_274.False(out logic_uScriptAct_SetBool_Target_274);
		local_Question01WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_274;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_274.Out)
		{
			Relay_False_169();
		}
	}

	private void Relay_In_277()
	{
		logic_uScriptCon_CompareInt_A_277 = local_Button4Value_System_Int32;
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_277.In(logic_uScriptCon_CompareInt_A_277, logic_uScriptCon_CompareInt_B_277);
		if (logic_uScriptCon_CompareInt_uScriptCon_CompareInt_277.GreaterThan)
		{
			Relay_In_96();
		}
	}

	private void Relay_Save_Out_279()
	{
		Relay_Save_302();
	}

	private void Relay_Load_Out_279()
	{
		Relay_Load_302();
	}

	private void Relay_Restart_Out_279()
	{
		Relay_Restart_302();
	}

	private void Relay_Save_279()
	{
		logic_SubGraph_SaveLoadInt_integer_279 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_279 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_279.Save(logic_SubGraph_SaveLoadInt_restartValue_279, ref logic_SubGraph_SaveLoadInt_integer_279, logic_SubGraph_SaveLoadInt_intAsVariable_279, logic_SubGraph_SaveLoadInt_uniqueID_279);
	}

	private void Relay_Load_279()
	{
		logic_SubGraph_SaveLoadInt_integer_279 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_279 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_279.Load(logic_SubGraph_SaveLoadInt_restartValue_279, ref logic_SubGraph_SaveLoadInt_integer_279, logic_SubGraph_SaveLoadInt_intAsVariable_279, logic_SubGraph_SaveLoadInt_uniqueID_279);
	}

	private void Relay_Restart_279()
	{
		logic_SubGraph_SaveLoadInt_integer_279 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_279 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_279.Restart(logic_SubGraph_SaveLoadInt_restartValue_279, ref logic_SubGraph_SaveLoadInt_integer_279, logic_SubGraph_SaveLoadInt_intAsVariable_279, logic_SubGraph_SaveLoadInt_uniqueID_279);
	}

	private void Relay_In_284()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_284.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_284.Out)
		{
			Relay_In_165();
		}
	}

	private void Relay_In_285()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_285.In(logic_uScriptAct_SetInt_Value_285, out logic_uScriptAct_SetInt_Target_285);
		local_Stage4QuestionThree_System_Int32 = logic_uScriptAct_SetInt_Target_285;
	}

	private void Relay_True_286()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_286.True(out logic_uScriptAct_SetBool_Target_286);
		local_CanPushButtons_System_Boolean = logic_uScriptAct_SetBool_Target_286;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_286.Out)
		{
			Relay_In_264();
		}
	}

	private void Relay_False_286()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_286.False(out logic_uScriptAct_SetBool_Target_286);
		local_CanPushButtons_System_Boolean = logic_uScriptAct_SetBool_Target_286;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_286.Out)
		{
			Relay_In_264();
		}
	}

	private void Relay_InitialSpawn_287()
	{
		int num = 0;
		Array array = buttonbase4SpawnData;
		if (logic_uScript_SpawnTechsFromData_spawnData_287.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_287, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SpawnTechsFromData_spawnData_287, num, array.Length);
		num += array.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_287 = owner_Connection_45;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_287.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_287, logic_uScript_SpawnTechsFromData_ownerNode_287, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_287, logic_uScript_SpawnTechsFromData_allowResurrection_287);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_287.Out)
		{
			Relay_InitialSpawn_318();
		}
	}

	private void Relay_In_288()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_288.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_288.Out)
		{
			Relay_In_284();
		}
	}

	private void Relay_In_291()
	{
		logic_uScriptCon_CompareBool_Bool_291 = local_CanPushButtons_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_291.In(logic_uScriptCon_CompareBool_Bool_291);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_291.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_291.False;
		if (num)
		{
			Relay_In_326();
		}
		if (flag)
		{
			Relay_In_176();
		}
	}

	private void Relay_True_293()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_293.True(out logic_uScriptAct_SetBool_Target_293);
		local_msgBaseFoundShown_System_Boolean = logic_uScriptAct_SetBool_Target_293;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_293.Out)
		{
			Relay_True_101();
		}
	}

	private void Relay_False_293()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_293.False(out logic_uScriptAct_SetBool_Target_293);
		local_msgBaseFoundShown_System_Boolean = logic_uScriptAct_SetBool_Target_293;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_293.Out)
		{
			Relay_True_101();
		}
	}

	private void Relay_In_294()
	{
		logic_uScript_AddMessage_messageData_294 = msg03QuestionOne;
		logic_uScript_AddMessage_speaker_294 = messageSpeaker;
		logic_uScript_AddMessage_Return_294 = logic_uScript_AddMessage_uScript_AddMessage_294.In(logic_uScript_AddMessage_messageData_294, logic_uScript_AddMessage_speaker_294);
		if (logic_uScript_AddMessage_uScript_AddMessage_294.Out)
		{
			Relay_In_162();
		}
	}

	private void Relay_In_295()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_295.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_295.Out)
		{
			Relay_In_151();
		}
	}

	private void Relay_True_296()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_296.True(out logic_uScriptAct_SetBool_Target_296);
		local_Question03WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_296;
	}

	private void Relay_False_296()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_296.False(out logic_uScriptAct_SetBool_Target_296);
		local_Question03WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_296;
	}

	private void Relay_True_300()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_300.True(out logic_uScriptAct_SetBool_Target_300);
		local_CanPushButtons_System_Boolean = logic_uScriptAct_SetBool_Target_300;
	}

	private void Relay_False_300()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_300.False(out logic_uScriptAct_SetBool_Target_300);
		local_CanPushButtons_System_Boolean = logic_uScriptAct_SetBool_Target_300;
	}

	private void Relay_Save_Out_302()
	{
		Relay_Save_189();
	}

	private void Relay_Load_Out_302()
	{
		Relay_Load_189();
	}

	private void Relay_Restart_Out_302()
	{
		Relay_Restart_189();
	}

	private void Relay_Save_302()
	{
		logic_SubGraph_SaveLoadInt_integer_302 = local_Stage2QuestionOne_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_302 = local_Stage2QuestionOne_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_302.Save(logic_SubGraph_SaveLoadInt_restartValue_302, ref logic_SubGraph_SaveLoadInt_integer_302, logic_SubGraph_SaveLoadInt_intAsVariable_302, logic_SubGraph_SaveLoadInt_uniqueID_302);
	}

	private void Relay_Load_302()
	{
		logic_SubGraph_SaveLoadInt_integer_302 = local_Stage2QuestionOne_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_302 = local_Stage2QuestionOne_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_302.Load(logic_SubGraph_SaveLoadInt_restartValue_302, ref logic_SubGraph_SaveLoadInt_integer_302, logic_SubGraph_SaveLoadInt_intAsVariable_302, logic_SubGraph_SaveLoadInt_uniqueID_302);
	}

	private void Relay_Restart_302()
	{
		logic_SubGraph_SaveLoadInt_integer_302 = local_Stage2QuestionOne_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_302 = local_Stage2QuestionOne_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_302.Restart(logic_SubGraph_SaveLoadInt_restartValue_302, ref logic_SubGraph_SaveLoadInt_integer_302, logic_SubGraph_SaveLoadInt_intAsVariable_302, logic_SubGraph_SaveLoadInt_uniqueID_302);
	}

	private void Relay_Out_308()
	{
	}

	private void Relay_In_308()
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_308 = local_Stage_System_Int32;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_308.In(ref logic_SubGraph_CompleteObjectiveStage_objectiveStage_308, logic_SubGraph_CompleteObjectiveStage_isFinalObjective_308);
	}

	private void Relay_In_311()
	{
		logic_uScript_DirectEnemiesOutOfEncounter_ownerNode_311 = owner_Connection_74;
		logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_311.In(logic_uScript_DirectEnemiesOutOfEncounter_ownerNode_311);
		if (logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_311.Out)
		{
			Relay_In_212();
		}
	}

	private void Relay_In_313()
	{
		logic_uScript_Wait_seconds_313 = WaitingTime;
		logic_uScript_Wait_uScript_Wait_313.In(logic_uScript_Wait_seconds_313, logic_uScript_Wait_repeat_313);
		if (logic_uScript_Wait_uScript_Wait_313.Waited)
		{
			Relay_False_296();
		}
	}

	private void Relay_InitialSpawn_314()
	{
		int num = 0;
		Array array = buttonbase2SpawnData;
		if (logic_uScript_SpawnTechsFromData_spawnData_314.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_314, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SpawnTechsFromData_spawnData_314, num, array.Length);
		num += array.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_314 = owner_Connection_305;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_314.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_314, logic_uScript_SpawnTechsFromData_ownerNode_314, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_314, logic_uScript_SpawnTechsFromData_allowResurrection_314);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_314.Out)
		{
			Relay_InitialSpawn_104();
		}
	}

	private void Relay_In_315()
	{
		int num = 0;
		Array nPCSpawnData = NPCSpawnData;
		if (logic_uScript_GetAndCheckTechs_techData_315.Length != num + nPCSpawnData.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_315, num + nPCSpawnData.Length);
		}
		Array.Copy(nPCSpawnData, 0, logic_uScript_GetAndCheckTechs_techData_315, num, nPCSpawnData.Length);
		num += nPCSpawnData.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_315 = owner_Connection_268;
		int num2 = 0;
		Array array = local_171_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_315.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_315, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techs_315, num2, array.Length);
		num2 += array.Length;
		logic_uScript_GetAndCheckTechs_Return_315 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_315.In(logic_uScript_GetAndCheckTechs_techData_315, logic_uScript_GetAndCheckTechs_ownerNode_315, ref logic_uScript_GetAndCheckTechs_techs_315);
		local_171_TankArray = logic_uScript_GetAndCheckTechs_techs_315;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_315.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_315.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_315.AllDead;
		if (allAlive)
		{
			Relay_AtIndex_224();
		}
		if (someAlive)
		{
			Relay_AtIndex_224();
		}
		if (allDead)
		{
			Relay_In_68();
		}
	}

	private void Relay_Output1_316()
	{
		Relay_True_8();
	}

	private void Relay_Output2_316()
	{
		Relay_In_27();
	}

	private void Relay_Output3_316()
	{
		Relay_In_17();
	}

	private void Relay_Output4_316()
	{
		Relay_In_428();
	}

	private void Relay_Output5_316()
	{
		Relay_In_454();
	}

	private void Relay_Output6_316()
	{
	}

	private void Relay_Output7_316()
	{
	}

	private void Relay_Output8_316()
	{
	}

	private void Relay_In_316()
	{
		logic_uScriptCon_ManualSwitch_CurrentOutput_316 = local_Stage4QuestionThree_System_Int32;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_316.In(logic_uScriptCon_ManualSwitch_CurrentOutput_316);
	}

	private void Relay_InitialSpawn_318()
	{
		int num = 0;
		Array nPCSpawnData = NPCSpawnData;
		if (logic_uScript_SpawnTechsFromData_spawnData_318.Length != num + nPCSpawnData.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_318, num + nPCSpawnData.Length);
		}
		Array.Copy(nPCSpawnData, 0, logic_uScript_SpawnTechsFromData_spawnData_318, num, nPCSpawnData.Length);
		num += nPCSpawnData.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_318 = owner_Connection_272;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_318.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_318, logic_uScript_SpawnTechsFromData_ownerNode_318, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_318, logic_uScript_SpawnTechsFromData_allowResurrection_318);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_318.Out)
		{
			Relay_In_241();
		}
	}

	private void Relay_In_319()
	{
		logic_uScript_SetTankHideBlockLimit_tech_319 = local_ButtonBase4Tech_Tank;
		logic_uScript_SetTankHideBlockLimit_uScript_SetTankHideBlockLimit_319.In(logic_uScript_SetTankHideBlockLimit_hidden_319, logic_uScript_SetTankHideBlockLimit_tech_319);
		if (logic_uScript_SetTankHideBlockLimit_uScript_SetTankHideBlockLimit_319.Out)
		{
			Relay_In_315();
		}
	}

	private void Relay_Save_Out_320()
	{
		Relay_Save_117();
	}

	private void Relay_Load_Out_320()
	{
		Relay_Load_117();
	}

	private void Relay_Restart_Out_320()
	{
		Relay_Set_False_117();
	}

	private void Relay_Save_320()
	{
		logic_SubGraph_SaveLoadBool_boolean_320 = local_Initialize_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_320 = local_Initialize_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_320.Save(ref logic_SubGraph_SaveLoadBool_boolean_320, logic_SubGraph_SaveLoadBool_boolAsVariable_320, logic_SubGraph_SaveLoadBool_uniqueID_320);
	}

	private void Relay_Load_320()
	{
		logic_SubGraph_SaveLoadBool_boolean_320 = local_Initialize_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_320 = local_Initialize_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_320.Load(ref logic_SubGraph_SaveLoadBool_boolean_320, logic_SubGraph_SaveLoadBool_boolAsVariable_320, logic_SubGraph_SaveLoadBool_uniqueID_320);
	}

	private void Relay_Set_True_320()
	{
		logic_SubGraph_SaveLoadBool_boolean_320 = local_Initialize_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_320 = local_Initialize_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_320.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_320, logic_SubGraph_SaveLoadBool_boolAsVariable_320, logic_SubGraph_SaveLoadBool_uniqueID_320);
	}

	private void Relay_Set_False_320()
	{
		logic_SubGraph_SaveLoadBool_boolean_320 = local_Initialize_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_320 = local_Initialize_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_320.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_320, logic_SubGraph_SaveLoadBool_boolAsVariable_320, logic_SubGraph_SaveLoadBool_uniqueID_320);
	}

	private void Relay_In_322()
	{
		logic_uScript_GetCircuitChargeInfo_block_322 = local_ButtonBlock1_TankBlock;
		logic_uScript_GetCircuitChargeInfo_Return_322 = logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_322.In(logic_uScript_GetCircuitChargeInfo_block_322, logic_uScript_GetCircuitChargeInfo_tech_322, logic_uScript_GetCircuitChargeInfo_blockType_322);
		local_Button1Value_System_Int32 = logic_uScript_GetCircuitChargeInfo_Return_322;
		if (logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_322.Out)
		{
			Relay_In_86();
		}
	}

	private void Relay_In_323()
	{
		logic_uScript_AddMessage_messageData_323 = msgLeavingMissionArea;
		logic_uScript_AddMessage_speaker_323 = messageSpeaker;
		logic_uScript_AddMessage_Return_323 = logic_uScript_AddMessage_uScript_AddMessage_323.In(logic_uScript_AddMessage_messageData_323, logic_uScript_AddMessage_speaker_323);
		if (logic_uScript_AddMessage_uScript_AddMessage_323.Out)
		{
			Relay_False_269();
		}
	}

	private void Relay_Save_Out_324()
	{
		Relay_Save_320();
	}

	private void Relay_Load_Out_324()
	{
		Relay_Load_320();
	}

	private void Relay_Restart_Out_324()
	{
		Relay_Set_False_320();
	}

	private void Relay_Save_324()
	{
		logic_SubGraph_SaveLoadInt_integer_324 = local_Stage4QuestionThree_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_324 = local_Stage4QuestionThree_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_324.Save(logic_SubGraph_SaveLoadInt_restartValue_324, ref logic_SubGraph_SaveLoadInt_integer_324, logic_SubGraph_SaveLoadInt_intAsVariable_324, logic_SubGraph_SaveLoadInt_uniqueID_324);
	}

	private void Relay_Load_324()
	{
		logic_SubGraph_SaveLoadInt_integer_324 = local_Stage4QuestionThree_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_324 = local_Stage4QuestionThree_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_324.Load(logic_SubGraph_SaveLoadInt_restartValue_324, ref logic_SubGraph_SaveLoadInt_integer_324, logic_SubGraph_SaveLoadInt_intAsVariable_324, logic_SubGraph_SaveLoadInt_uniqueID_324);
	}

	private void Relay_Restart_324()
	{
		logic_SubGraph_SaveLoadInt_integer_324 = local_Stage4QuestionThree_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_324 = local_Stage4QuestionThree_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_324.Restart(logic_SubGraph_SaveLoadInt_restartValue_324, ref logic_SubGraph_SaveLoadInt_integer_324, logic_SubGraph_SaveLoadInt_intAsVariable_324, logic_SubGraph_SaveLoadInt_uniqueID_324);
	}

	private void Relay_In_326()
	{
		logic_uScript_LockTechInteraction_tech_326 = local_ButtonBase1Tech_Tank;
		int num = 0;
		if (logic_uScript_LockTechInteraction_excludedBlocks_326.Length <= num)
		{
			Array.Resize(ref logic_uScript_LockTechInteraction_excludedBlocks_326, num + 1);
		}
		logic_uScript_LockTechInteraction_excludedBlocks_326[num++] = blockTypeButton1;
		int num2 = 0;
		if (logic_uScript_LockTechInteraction_excludedUniqueBlocks_326.Length <= num2)
		{
			Array.Resize(ref logic_uScript_LockTechInteraction_excludedUniqueBlocks_326, num2 + 1);
		}
		logic_uScript_LockTechInteraction_excludedUniqueBlocks_326[num2++] = local_ButtonBlock1_TankBlock;
		logic_uScript_LockTechInteraction_uScript_LockTechInteraction_326.In(logic_uScript_LockTechInteraction_tech_326, logic_uScript_LockTechInteraction_excludedBlocks_326, logic_uScript_LockTechInteraction_excludedUniqueBlocks_326);
		if (logic_uScript_LockTechInteraction_uScript_LockTechInteraction_326.Out)
		{
			Relay_In_73();
		}
	}

	private void Relay_In_328()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_328.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_328.Out)
		{
			Relay_In_236();
		}
	}

	private void Relay_Output1_331()
	{
		Relay_In_481();
	}

	private void Relay_Output2_331()
	{
		Relay_In_233();
	}

	private void Relay_Output3_331()
	{
		Relay_In_134();
	}

	private void Relay_Output4_331()
	{
		Relay_In_316();
	}

	private void Relay_Output5_331()
	{
	}

	private void Relay_Output6_331()
	{
	}

	private void Relay_Output7_331()
	{
	}

	private void Relay_Output8_331()
	{
	}

	private void Relay_In_331()
	{
		logic_uScriptCon_ManualSwitch_CurrentOutput_331 = local_Stage_System_Int32;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_331.In(logic_uScriptCon_ManualSwitch_CurrentOutput_331);
	}

	private void Relay_In_335()
	{
		logic_uScript_SetTankInvulnerable_tank_335 = local_ButtonBase3Tech_Tank;
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_335.In(logic_uScript_SetTankInvulnerable_invulnerable_335, logic_uScript_SetTankInvulnerable_tank_335);
		if (logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_335.Out)
		{
			Relay_In_87();
		}
	}

	private void Relay_True_339()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_339.True(out logic_uScriptAct_SetBool_Target_339);
		local_Q1EnemyAlive_System_Boolean = logic_uScriptAct_SetBool_Target_339;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_339.Out)
		{
			Relay_True_89();
		}
	}

	private void Relay_False_339()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_339.False(out logic_uScriptAct_SetBool_Target_339);
		local_Q1EnemyAlive_System_Boolean = logic_uScriptAct_SetBool_Target_339;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_339.Out)
		{
			Relay_True_89();
		}
	}

	private void Relay_In_340()
	{
		logic_uScriptCon_CompareBool_Bool_340 = local_Q1EnemyAlive_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_340.In(logic_uScriptCon_CompareBool_Bool_340);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_340.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_340.False;
		if (num)
		{
			Relay_In_405();
		}
		if (flag)
		{
			Relay_InitialSpawn_343();
		}
	}

	private void Relay_In_341()
	{
		int num = 0;
		Array q1EnemyTechData = Q1EnemyTechData;
		if (logic_uScript_GetAndCheckTechs_techData_341.Length != num + q1EnemyTechData.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_341, num + q1EnemyTechData.Length);
		}
		Array.Copy(q1EnemyTechData, 0, logic_uScript_GetAndCheckTechs_techData_341, num, q1EnemyTechData.Length);
		num += q1EnemyTechData.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_341 = owner_Connection_344;
		int num2 = 0;
		Array array = local_Q1Enemy_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_341.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_341, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techs_341, num2, array.Length);
		num2 += array.Length;
		logic_uScript_GetAndCheckTechs_Return_341 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_341.In(logic_uScript_GetAndCheckTechs_techData_341, logic_uScript_GetAndCheckTechs_ownerNode_341, ref logic_uScript_GetAndCheckTechs_techs_341);
		local_Q1Enemy_TankArray = logic_uScript_GetAndCheckTechs_techs_341;
		if (logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_341.AllDead)
		{
			Relay_False_339();
		}
	}

	private void Relay_InitialSpawn_343()
	{
		int num = 0;
		Array q1EnemyTechData = Q1EnemyTechData;
		if (logic_uScript_SpawnTechsFromData_spawnData_343.Length != num + q1EnemyTechData.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_343, num + q1EnemyTechData.Length);
		}
		Array.Copy(q1EnemyTechData, 0, logic_uScript_SpawnTechsFromData_spawnData_343, num, q1EnemyTechData.Length);
		num += q1EnemyTechData.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_343 = owner_Connection_338;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_343.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_343, logic_uScript_SpawnTechsFromData_ownerNode_343, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_343, logic_uScript_SpawnTechsFromData_allowResurrection_343);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_343.Out)
		{
			Relay_True_345();
		}
	}

	private void Relay_True_345()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_345.True(out logic_uScriptAct_SetBool_Target_345);
		local_Q1EnemyAlive_System_Boolean = logic_uScriptAct_SetBool_Target_345;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_345.Out)
		{
			Relay_In_341();
		}
	}

	private void Relay_False_345()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_345.False(out logic_uScriptAct_SetBool_Target_345);
		local_Q1EnemyAlive_System_Boolean = logic_uScriptAct_SetBool_Target_345;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_345.Out)
		{
			Relay_In_341();
		}
	}

	private void Relay_Save_Out_347()
	{
		Relay_Save_386();
	}

	private void Relay_Load_Out_347()
	{
		Relay_Load_386();
	}

	private void Relay_Restart_Out_347()
	{
		Relay_Set_False_386();
	}

	private void Relay_Save_347()
	{
		logic_SubGraph_SaveLoadBool_boolean_347 = local_Q1EnemyAlive_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_347 = local_Q1EnemyAlive_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_347.Save(ref logic_SubGraph_SaveLoadBool_boolean_347, logic_SubGraph_SaveLoadBool_boolAsVariable_347, logic_SubGraph_SaveLoadBool_uniqueID_347);
	}

	private void Relay_Load_347()
	{
		logic_SubGraph_SaveLoadBool_boolean_347 = local_Q1EnemyAlive_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_347 = local_Q1EnemyAlive_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_347.Load(ref logic_SubGraph_SaveLoadBool_boolean_347, logic_SubGraph_SaveLoadBool_boolAsVariable_347, logic_SubGraph_SaveLoadBool_uniqueID_347);
	}

	private void Relay_Set_True_347()
	{
		logic_SubGraph_SaveLoadBool_boolean_347 = local_Q1EnemyAlive_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_347 = local_Q1EnemyAlive_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_347.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_347, logic_SubGraph_SaveLoadBool_boolAsVariable_347, logic_SubGraph_SaveLoadBool_uniqueID_347);
	}

	private void Relay_Set_False_347()
	{
		logic_SubGraph_SaveLoadBool_boolean_347 = local_Q1EnemyAlive_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_347 = local_Q1EnemyAlive_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_347.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_347, logic_SubGraph_SaveLoadBool_boolAsVariable_347, logic_SubGraph_SaveLoadBool_uniqueID_347);
	}

	private void Relay_In_349()
	{
		int num = 0;
		if (logic_uScriptAct_Concatenate_A_349.Length <= num)
		{
			Array.Resize(ref logic_uScriptAct_Concatenate_A_349, num + 1);
		}
		logic_uScriptAct_Concatenate_A_349[num++] = local_352_System_String;
		int num2 = 0;
		if (logic_uScriptAct_Concatenate_B_349.Length <= num2)
		{
			Array.Resize(ref logic_uScriptAct_Concatenate_B_349, num2 + 1);
		}
		logic_uScriptAct_Concatenate_B_349[num2++] = local_msgIntro_System_Boolean;
		logic_uScriptAct_Concatenate_uScriptAct_Concatenate_349.In(logic_uScriptAct_Concatenate_A_349, logic_uScriptAct_Concatenate_B_349, logic_uScriptAct_Concatenate_Separator_349, out logic_uScriptAct_Concatenate_Result_349);
		local_350_System_String = logic_uScriptAct_Concatenate_Result_349;
		if (logic_uScriptAct_Concatenate_uScriptAct_Concatenate_349.Out)
		{
			Relay_ShowLabel_351();
		}
	}

	private void Relay_ShowLabel_351()
	{
		logic_uScriptAct_PrintText_Text_351 = local_350_System_String;
		logic_uScriptAct_PrintText_uScriptAct_PrintText_351.ShowLabel(logic_uScriptAct_PrintText_Text_351, logic_uScriptAct_PrintText_FontSize_351, logic_uScriptAct_PrintText_FontStyle_351, logic_uScriptAct_PrintText_FontColor_351, logic_uScriptAct_PrintText_textAnchor_351, logic_uScriptAct_PrintText_EdgePadding_351, logic_uScriptAct_PrintText_time_351);
	}

	private void Relay_HideLabel_351()
	{
		logic_uScriptAct_PrintText_Text_351 = local_350_System_String;
		logic_uScriptAct_PrintText_uScriptAct_PrintText_351.HideLabel(logic_uScriptAct_PrintText_Text_351, logic_uScriptAct_PrintText_FontSize_351, logic_uScriptAct_PrintText_FontStyle_351, logic_uScriptAct_PrintText_FontColor_351, logic_uScriptAct_PrintText_textAnchor_351, logic_uScriptAct_PrintText_EdgePadding_351, logic_uScriptAct_PrintText_time_351);
	}

	private void Relay_InitialSpawn_354()
	{
		int num = 0;
		Array q2EnemyTechData = Q2EnemyTechData;
		if (logic_uScript_SpawnTechsFromData_spawnData_354.Length != num + q2EnemyTechData.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_354, num + q2EnemyTechData.Length);
		}
		Array.Copy(q2EnemyTechData, 0, logic_uScript_SpawnTechsFromData_spawnData_354, num, q2EnemyTechData.Length);
		num += q2EnemyTechData.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_354 = owner_Connection_362;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_354.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_354, logic_uScript_SpawnTechsFromData_ownerNode_354, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_354, logic_uScript_SpawnTechsFromData_allowResurrection_354);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_354.Out)
		{
			Relay_True_361();
		}
	}

	private void Relay_True_355()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_355.True(out logic_uScriptAct_SetBool_Target_355);
		local_Q2EnemyAlive_System_Boolean = logic_uScriptAct_SetBool_Target_355;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_355.Out)
		{
			Relay_True_360();
		}
	}

	private void Relay_False_355()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_355.False(out logic_uScriptAct_SetBool_Target_355);
		local_Q2EnemyAlive_System_Boolean = logic_uScriptAct_SetBool_Target_355;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_355.Out)
		{
			Relay_True_360();
		}
	}

	private void Relay_In_356()
	{
		int num = 0;
		Array q2EnemyTechData = Q2EnemyTechData;
		if (logic_uScript_GetAndCheckTechs_techData_356.Length != num + q2EnemyTechData.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_356, num + q2EnemyTechData.Length);
		}
		Array.Copy(q2EnemyTechData, 0, logic_uScript_GetAndCheckTechs_techData_356, num, q2EnemyTechData.Length);
		num += q2EnemyTechData.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_356 = owner_Connection_357;
		int num2 = 0;
		Array array = local_Q2Enemy_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_356.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_356, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techs_356, num2, array.Length);
		num2 += array.Length;
		logic_uScript_GetAndCheckTechs_Return_356 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_356.In(logic_uScript_GetAndCheckTechs_techData_356, logic_uScript_GetAndCheckTechs_ownerNode_356, ref logic_uScript_GetAndCheckTechs_techs_356);
		local_Q2Enemy_TankArray = logic_uScript_GetAndCheckTechs_techs_356;
		if (logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_356.AllDead)
		{
			Relay_False_355();
		}
	}

	private void Relay_True_360()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_360.True(out logic_uScriptAct_SetBool_Target_360);
		local_CanPushButtons_System_Boolean = logic_uScriptAct_SetBool_Target_360;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_360.Out)
		{
			Relay_In_421();
		}
	}

	private void Relay_False_360()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_360.False(out logic_uScriptAct_SetBool_Target_360);
		local_CanPushButtons_System_Boolean = logic_uScriptAct_SetBool_Target_360;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_360.Out)
		{
			Relay_In_421();
		}
	}

	private void Relay_True_361()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_361.True(out logic_uScriptAct_SetBool_Target_361);
		local_Q2EnemyAlive_System_Boolean = logic_uScriptAct_SetBool_Target_361;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_361.Out)
		{
			Relay_In_356();
		}
	}

	private void Relay_False_361()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_361.False(out logic_uScriptAct_SetBool_Target_361);
		local_Q2EnemyAlive_System_Boolean = logic_uScriptAct_SetBool_Target_361;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_361.Out)
		{
			Relay_In_356();
		}
	}

	private void Relay_In_363()
	{
		logic_uScriptCon_CompareBool_Bool_363 = local_Q2EnemyAlive_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_363.In(logic_uScriptCon_CompareBool_Bool_363);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_363.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_363.False;
		if (num)
		{
			Relay_In_425();
		}
		if (flag)
		{
			Relay_InitialSpawn_354();
		}
	}

	private void Relay_In_372()
	{
		logic_uScriptCon_CompareBool_Bool_372 = local_Q3EnemyAlive_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_372.In(logic_uScriptCon_CompareBool_Bool_372);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_372.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_372.False;
		if (num)
		{
			Relay_In_476();
		}
		if (flag)
		{
			Relay_InitialSpawn_377();
		}
	}

	private void Relay_True_375()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_375.True(out logic_uScriptAct_SetBool_Target_375);
		local_Q3EnemyAlive_System_Boolean = logic_uScriptAct_SetBool_Target_375;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_375.Out)
		{
			Relay_In_381();
		}
	}

	private void Relay_False_375()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_375.False(out logic_uScriptAct_SetBool_Target_375);
		local_Q3EnemyAlive_System_Boolean = logic_uScriptAct_SetBool_Target_375;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_375.Out)
		{
			Relay_In_381();
		}
	}

	private void Relay_InitialSpawn_377()
	{
		int num = 0;
		Array q3EnemyTechData = Q3EnemyTechData;
		if (logic_uScript_SpawnTechsFromData_spawnData_377.Length != num + q3EnemyTechData.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_377, num + q3EnemyTechData.Length);
		}
		Array.Copy(q3EnemyTechData, 0, logic_uScript_SpawnTechsFromData_spawnData_377, num, q3EnemyTechData.Length);
		num += q3EnemyTechData.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_377 = owner_Connection_379;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_377.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_377, logic_uScript_SpawnTechsFromData_ownerNode_377, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_377, logic_uScript_SpawnTechsFromData_allowResurrection_377);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_377.Out)
		{
			Relay_True_375();
		}
	}

	private void Relay_True_378()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_378.True(out logic_uScriptAct_SetBool_Target_378);
		local_CanPushButtons_System_Boolean = logic_uScriptAct_SetBool_Target_378;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_378.Out)
		{
			Relay_In_433();
		}
	}

	private void Relay_False_378()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_378.False(out logic_uScriptAct_SetBool_Target_378);
		local_CanPushButtons_System_Boolean = logic_uScriptAct_SetBool_Target_378;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_378.Out)
		{
			Relay_In_433();
		}
	}

	private void Relay_In_381()
	{
		int num = 0;
		Array q3EnemyTechData = Q3EnemyTechData;
		if (logic_uScript_GetAndCheckTechs_techData_381.Length != num + q3EnemyTechData.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_381, num + q3EnemyTechData.Length);
		}
		Array.Copy(q3EnemyTechData, 0, logic_uScript_GetAndCheckTechs_techData_381, num, q3EnemyTechData.Length);
		num += q3EnemyTechData.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_381 = owner_Connection_370;
		int num2 = 0;
		Array array = local_Q3Enemy_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_381.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_381, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techs_381, num2, array.Length);
		num2 += array.Length;
		logic_uScript_GetAndCheckTechs_Return_381 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_381.In(logic_uScript_GetAndCheckTechs_techData_381, logic_uScript_GetAndCheckTechs_ownerNode_381, ref logic_uScript_GetAndCheckTechs_techs_381);
		local_Q3Enemy_TankArray = logic_uScript_GetAndCheckTechs_techs_381;
		if (logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_381.AllDead)
		{
			Relay_False_382();
		}
	}

	private void Relay_True_382()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_382.True(out logic_uScriptAct_SetBool_Target_382);
		local_Q3EnemyAlive_System_Boolean = logic_uScriptAct_SetBool_Target_382;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_382.Out)
		{
			Relay_True_378();
		}
	}

	private void Relay_False_382()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_382.False(out logic_uScriptAct_SetBool_Target_382);
		local_Q3EnemyAlive_System_Boolean = logic_uScriptAct_SetBool_Target_382;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_382.Out)
		{
			Relay_True_378();
		}
	}

	private void Relay_Save_Out_386()
	{
		Relay_Save_388();
	}

	private void Relay_Load_Out_386()
	{
		Relay_Load_388();
	}

	private void Relay_Restart_Out_386()
	{
		Relay_Set_False_388();
	}

	private void Relay_Save_386()
	{
		logic_SubGraph_SaveLoadBool_boolean_386 = local_Q2EnemyAlive_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_386 = local_Q2EnemyAlive_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_386.Save(ref logic_SubGraph_SaveLoadBool_boolean_386, logic_SubGraph_SaveLoadBool_boolAsVariable_386, logic_SubGraph_SaveLoadBool_uniqueID_386);
	}

	private void Relay_Load_386()
	{
		logic_SubGraph_SaveLoadBool_boolean_386 = local_Q2EnemyAlive_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_386 = local_Q2EnemyAlive_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_386.Load(ref logic_SubGraph_SaveLoadBool_boolean_386, logic_SubGraph_SaveLoadBool_boolAsVariable_386, logic_SubGraph_SaveLoadBool_uniqueID_386);
	}

	private void Relay_Set_True_386()
	{
		logic_SubGraph_SaveLoadBool_boolean_386 = local_Q2EnemyAlive_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_386 = local_Q2EnemyAlive_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_386.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_386, logic_SubGraph_SaveLoadBool_boolAsVariable_386, logic_SubGraph_SaveLoadBool_uniqueID_386);
	}

	private void Relay_Set_False_386()
	{
		logic_SubGraph_SaveLoadBool_boolean_386 = local_Q2EnemyAlive_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_386 = local_Q2EnemyAlive_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_386.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_386, logic_SubGraph_SaveLoadBool_boolAsVariable_386, logic_SubGraph_SaveLoadBool_uniqueID_386);
	}

	private void Relay_Save_Out_388()
	{
		Relay_Save_471();
	}

	private void Relay_Load_Out_388()
	{
		Relay_Load_471();
	}

	private void Relay_Restart_Out_388()
	{
		Relay_Set_False_471();
	}

	private void Relay_Save_388()
	{
		logic_SubGraph_SaveLoadBool_boolean_388 = local_Q3EnemyAlive_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_388 = local_Q3EnemyAlive_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_388.Save(ref logic_SubGraph_SaveLoadBool_boolean_388, logic_SubGraph_SaveLoadBool_boolAsVariable_388, logic_SubGraph_SaveLoadBool_uniqueID_388);
	}

	private void Relay_Load_388()
	{
		logic_SubGraph_SaveLoadBool_boolean_388 = local_Q3EnemyAlive_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_388 = local_Q3EnemyAlive_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_388.Load(ref logic_SubGraph_SaveLoadBool_boolean_388, logic_SubGraph_SaveLoadBool_boolAsVariable_388, logic_SubGraph_SaveLoadBool_uniqueID_388);
	}

	private void Relay_Set_True_388()
	{
		logic_SubGraph_SaveLoadBool_boolean_388 = local_Q3EnemyAlive_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_388 = local_Q3EnemyAlive_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_388.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_388, logic_SubGraph_SaveLoadBool_boolAsVariable_388, logic_SubGraph_SaveLoadBool_uniqueID_388);
	}

	private void Relay_Set_False_388()
	{
		logic_SubGraph_SaveLoadBool_boolean_388 = local_Q3EnemyAlive_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_388 = local_Q3EnemyAlive_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_388.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_388, logic_SubGraph_SaveLoadBool_boolAsVariable_388, logic_SubGraph_SaveLoadBool_uniqueID_388);
	}

	private void Relay_True_390()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_390.True(out logic_uScriptAct_SetBool_Target_390);
		local_NearNPC_System_Boolean = logic_uScriptAct_SetBool_Target_390;
	}

	private void Relay_False_390()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_390.False(out logic_uScriptAct_SetBool_Target_390);
		local_NearNPC_System_Boolean = logic_uScriptAct_SetBool_Target_390;
	}

	private void Relay_True_392()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_392.True(out logic_uScriptAct_SetBool_Target_392);
		local_Question02WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_392;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_392.Out)
		{
			Relay_False_393();
		}
	}

	private void Relay_False_392()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_392.False(out logic_uScriptAct_SetBool_Target_392);
		local_Question02WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_392;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_392.Out)
		{
			Relay_False_393();
		}
	}

	private void Relay_True_393()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_393.True(out logic_uScriptAct_SetBool_Target_393);
		local_CanPushButtons_System_Boolean = logic_uScriptAct_SetBool_Target_393;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_393.Out)
		{
			Relay_In_396();
		}
	}

	private void Relay_False_393()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_393.False(out logic_uScriptAct_SetBool_Target_393);
		local_CanPushButtons_System_Boolean = logic_uScriptAct_SetBool_Target_393;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_393.Out)
		{
			Relay_In_396();
		}
	}

	private void Relay_In_396()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_396.In(logic_uScriptAct_SetInt_Value_396, out logic_uScriptAct_SetInt_Target_396);
		local_Stage3QuestionTwo_System_Int32 = logic_uScriptAct_SetInt_Target_396;
	}

	private void Relay_In_405()
	{
		logic_uScript_AddMessage_messageData_405 = msgSpawnMinion;
		logic_uScript_AddMessage_speaker_405 = messageSpeaker;
		logic_uScript_AddMessage_Return_405 = logic_uScript_AddMessage_uScript_AddMessage_405.In(logic_uScript_AddMessage_messageData_405, logic_uScript_AddMessage_speaker_405);
		if (logic_uScript_AddMessage_uScript_AddMessage_405.Out)
		{
			Relay_In_341();
		}
	}

	private void Relay_In_409()
	{
		logic_uScript_AddMessage_messageData_409 = msgMinionDefeated;
		logic_uScript_AddMessage_speaker_409 = messageSpeaker;
		logic_uScript_AddMessage_Return_409 = logic_uScript_AddMessage_uScript_AddMessage_409.In(logic_uScript_AddMessage_messageData_409, logic_uScript_AddMessage_speaker_409);
		if (logic_uScript_AddMessage_uScript_AddMessage_409.Out)
		{
			Relay_In_412();
		}
	}

	private void Relay_In_410()
	{
		logic_uScriptAct_AddInt_v2_A_410 = local_Stage2QuestionOne_System_Int32;
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_410.In(logic_uScriptAct_AddInt_v2_A_410, logic_uScriptAct_AddInt_v2_B_410, out logic_uScriptAct_AddInt_v2_IntResult_410, out logic_uScriptAct_AddInt_v2_FloatResult_410);
		local_Stage2QuestionOne_System_Int32 = logic_uScriptAct_AddInt_v2_IntResult_410;
	}

	private void Relay_In_412()
	{
		logic_uScript_Wait_seconds_412 = WaitingTime;
		logic_uScript_Wait_uScript_Wait_412.In(logic_uScript_Wait_seconds_412, logic_uScript_Wait_repeat_412);
		if (logic_uScript_Wait_uScript_Wait_412.Waited)
		{
			Relay_In_229();
		}
	}

	private void Relay_In_415()
	{
		logic_uScript_AddMessage_messageData_415 = msgMinionDefeated;
		logic_uScript_AddMessage_speaker_415 = messageSpeaker;
		logic_uScript_AddMessage_Return_415 = logic_uScript_AddMessage_uScript_AddMessage_415.In(logic_uScript_AddMessage_messageData_415, logic_uScript_AddMessage_speaker_415);
		if (logic_uScript_AddMessage_uScript_AddMessage_415.Out)
		{
			Relay_In_418();
		}
	}

	private void Relay_In_418()
	{
		logic_uScript_Wait_seconds_418 = WaitingTime;
		logic_uScript_Wait_uScript_Wait_418.In(logic_uScript_Wait_seconds_418, logic_uScript_Wait_repeat_418);
		if (logic_uScript_Wait_uScript_Wait_418.Waited)
		{
			Relay_In_420();
		}
	}

	private void Relay_In_420()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_420.In(logic_uScriptAct_SetInt_Value_420, out logic_uScriptAct_SetInt_Target_420);
		local_Stage3QuestionTwo_System_Int32 = logic_uScriptAct_SetInt_Target_420;
	}

	private void Relay_In_421()
	{
		logic_uScriptAct_AddInt_v2_A_421 = local_Stage3QuestionTwo_System_Int32;
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_421.In(logic_uScriptAct_AddInt_v2_A_421, logic_uScriptAct_AddInt_v2_B_421, out logic_uScriptAct_AddInt_v2_IntResult_421, out logic_uScriptAct_AddInt_v2_FloatResult_421);
		local_Stage3QuestionTwo_System_Int32 = logic_uScriptAct_AddInt_v2_IntResult_421;
	}

	private void Relay_In_425()
	{
		logic_uScript_AddMessage_messageData_425 = msgSpawnMinion;
		logic_uScript_AddMessage_speaker_425 = messageSpeaker;
		logic_uScript_AddMessage_Return_425 = logic_uScript_AddMessage_uScript_AddMessage_425.In(logic_uScript_AddMessage_messageData_425, logic_uScript_AddMessage_speaker_425);
		if (logic_uScript_AddMessage_uScript_AddMessage_425.Out)
		{
			Relay_In_356();
		}
	}

	private void Relay_In_426()
	{
		logic_uScript_Wait_seconds_426 = WaitingTime;
		logic_uScript_Wait_uScript_Wait_426.In(logic_uScript_Wait_seconds_426, logic_uScript_Wait_repeat_426);
		if (logic_uScript_Wait_uScript_Wait_426.Waited)
		{
			Relay_In_431();
		}
	}

	private void Relay_In_428()
	{
		logic_uScript_AddMessage_messageData_428 = msgMinionDefeated;
		logic_uScript_AddMessage_speaker_428 = messageSpeaker;
		logic_uScript_AddMessage_Return_428 = logic_uScript_AddMessage_uScript_AddMessage_428.In(logic_uScript_AddMessage_messageData_428, logic_uScript_AddMessage_speaker_428);
		if (logic_uScript_AddMessage_uScript_AddMessage_428.Out)
		{
			Relay_In_426();
		}
	}

	private void Relay_In_431()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_431.In(logic_uScriptAct_SetInt_Value_431, out logic_uScriptAct_SetInt_Target_431);
		local_Stage4QuestionThree_System_Int32 = logic_uScriptAct_SetInt_Target_431;
	}

	private void Relay_In_433()
	{
		logic_uScriptAct_AddInt_v2_A_433 = local_Stage4QuestionThree_System_Int32;
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_433.In(logic_uScriptAct_AddInt_v2_A_433, logic_uScriptAct_AddInt_v2_B_433, out logic_uScriptAct_AddInt_v2_IntResult_433, out logic_uScriptAct_AddInt_v2_FloatResult_433);
		local_Stage4QuestionThree_System_Int32 = logic_uScriptAct_AddInt_v2_IntResult_433;
	}

	private void Relay_In_436()
	{
		logic_uScript_RemoveTech_tech_436 = local_ButtonBase4Tech_Tank;
		logic_uScript_RemoveTech_uScript_RemoveTech_436.In(logic_uScript_RemoveTech_tech_436);
		if (logic_uScript_RemoveTech_uScript_RemoveTech_436.Out)
		{
			Relay_In_460();
		}
	}

	private void Relay_In_441()
	{
		logic_uScript_SpawnVFX_ownerNode_441 = owner_Connection_447;
		logic_uScript_SpawnVFX_vfxToSpawn_441 = NPCDespawnParticleEffect;
		logic_uScript_SpawnVFX_spawnPosName_441 = ButtonBase2VFXSpawn;
		logic_uScript_SpawnVFX_uScript_SpawnVFX_441.In(logic_uScript_SpawnVFX_ownerNode_441, logic_uScript_SpawnVFX_vfxToSpawn_441, logic_uScript_SpawnVFX_spawnPosName_441);
		if (logic_uScript_SpawnVFX_uScript_SpawnVFX_441.Out)
		{
			Relay_In_452();
		}
	}

	private void Relay_In_446()
	{
		logic_uScript_SpawnVFX_ownerNode_446 = owner_Connection_435;
		logic_uScript_SpawnVFX_vfxToSpawn_446 = NPCDespawnParticleEffect;
		logic_uScript_SpawnVFX_spawnPosName_446 = ButtonBase4VFXSpawn;
		logic_uScript_SpawnVFX_uScript_SpawnVFX_446.In(logic_uScript_SpawnVFX_ownerNode_446, logic_uScript_SpawnVFX_vfxToSpawn_446, logic_uScript_SpawnVFX_spawnPosName_446);
		if (logic_uScript_SpawnVFX_uScript_SpawnVFX_446.Out)
		{
			Relay_In_436();
		}
	}

	private void Relay_In_452()
	{
		logic_uScript_RemoveTech_tech_452 = local_ButtonBase2Tech_Tank;
		logic_uScript_RemoveTech_uScript_RemoveTech_452.In(logic_uScript_RemoveTech_tech_452);
		if (logic_uScript_RemoveTech_uScript_RemoveTech_452.Out)
		{
			Relay_In_458();
		}
	}

	private void Relay_In_454()
	{
		logic_uScript_AddMessage_messageData_454 = msgEncounterComplete;
		logic_uScript_AddMessage_speaker_454 = messageSpeaker;
		logic_uScript_AddMessage_Return_454 = logic_uScript_AddMessage_uScript_AddMessage_454.In(logic_uScript_AddMessage_messageData_454, logic_uScript_AddMessage_speaker_454);
		if (logic_uScript_AddMessage_uScript_AddMessage_454.Shown)
		{
			Relay_In_467();
		}
	}

	private void Relay_In_455()
	{
		logic_uScript_RemoveTech_tech_455 = local_ButtonBase1Tech_Tank;
		logic_uScript_RemoveTech_uScript_RemoveTech_455.In(logic_uScript_RemoveTech_tech_455);
		if (logic_uScript_RemoveTech_uScript_RemoveTech_455.Out)
		{
			Relay_In_441();
		}
	}

	private void Relay_In_458()
	{
		logic_uScript_SpawnVFX_ownerNode_458 = owner_Connection_449;
		logic_uScript_SpawnVFX_vfxToSpawn_458 = NPCDespawnParticleEffect;
		logic_uScript_SpawnVFX_spawnPosName_458 = ButtonBase3VFXSpawn;
		logic_uScript_SpawnVFX_uScript_SpawnVFX_458.In(logic_uScript_SpawnVFX_ownerNode_458, logic_uScript_SpawnVFX_vfxToSpawn_458, logic_uScript_SpawnVFX_spawnPosName_458);
		if (logic_uScript_SpawnVFX_uScript_SpawnVFX_458.Out)
		{
			Relay_In_466();
		}
	}

	private void Relay_In_460()
	{
		logic_uScript_FlyTechUpAndAway_tech_460 = local_NPCTech_Tank;
		logic_uScript_FlyTechUpAndAway_aiTree_460 = NPCFlyAwayAI;
		logic_uScript_FlyTechUpAndAway_removalParticles_460 = NPCDespawnParticleEffect;
		logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_460.In(logic_uScript_FlyTechUpAndAway_tech_460, logic_uScript_FlyTechUpAndAway_maxLifetime_460, logic_uScript_FlyTechUpAndAway_targetHeight_460, logic_uScript_FlyTechUpAndAway_aiTree_460, logic_uScript_FlyTechUpAndAway_removalParticles_460);
		if (logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_460.Out)
		{
			Relay_Succeed_465();
		}
	}

	private void Relay_Succeed_465()
	{
		logic_uScript_FinishEncounter_owner_465 = owner_Connection_451;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_465.Succeed(logic_uScript_FinishEncounter_owner_465);
	}

	private void Relay_Fail_465()
	{
		logic_uScript_FinishEncounter_owner_465 = owner_Connection_451;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_465.Fail(logic_uScript_FinishEncounter_owner_465);
	}

	private void Relay_In_466()
	{
		logic_uScript_RemoveTech_tech_466 = local_ButtonBase3Tech_Tank;
		logic_uScript_RemoveTech_uScript_RemoveTech_466.In(logic_uScript_RemoveTech_tech_466);
		if (logic_uScript_RemoveTech_uScript_RemoveTech_466.Out)
		{
			Relay_In_446();
		}
	}

	private void Relay_In_467()
	{
		logic_uScript_SpawnVFX_ownerNode_467 = owner_Connection_444;
		logic_uScript_SpawnVFX_vfxToSpawn_467 = NPCDespawnParticleEffect;
		logic_uScript_SpawnVFX_spawnPosName_467 = ButtonBase1VFXSpawn;
		logic_uScript_SpawnVFX_uScript_SpawnVFX_467.In(logic_uScript_SpawnVFX_ownerNode_467, logic_uScript_SpawnVFX_vfxToSpawn_467, logic_uScript_SpawnVFX_spawnPosName_467);
		if (logic_uScript_SpawnVFX_uScript_SpawnVFX_467.Out)
		{
			Relay_In_455();
		}
	}

	private void Relay_In_468()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_468.In(logic_uScriptAct_SetInt_Value_468, out logic_uScriptAct_SetInt_Target_468);
		local_Stage4QuestionThree_System_Int32 = logic_uScriptAct_SetInt_Target_468;
	}

	private void Relay_Save_Out_471()
	{
		Relay_Save_473();
	}

	private void Relay_Load_Out_471()
	{
		Relay_Load_473();
	}

	private void Relay_Restart_Out_471()
	{
		Relay_Set_False_473();
	}

	private void Relay_Save_471()
	{
		logic_SubGraph_SaveLoadBool_boolean_471 = local_Question01WrongAnswers_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_471 = local_Question01WrongAnswers_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_471.Save(ref logic_SubGraph_SaveLoadBool_boolean_471, logic_SubGraph_SaveLoadBool_boolAsVariable_471, logic_SubGraph_SaveLoadBool_uniqueID_471);
	}

	private void Relay_Load_471()
	{
		logic_SubGraph_SaveLoadBool_boolean_471 = local_Question01WrongAnswers_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_471 = local_Question01WrongAnswers_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_471.Load(ref logic_SubGraph_SaveLoadBool_boolean_471, logic_SubGraph_SaveLoadBool_boolAsVariable_471, logic_SubGraph_SaveLoadBool_uniqueID_471);
	}

	private void Relay_Set_True_471()
	{
		logic_SubGraph_SaveLoadBool_boolean_471 = local_Question01WrongAnswers_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_471 = local_Question01WrongAnswers_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_471.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_471, logic_SubGraph_SaveLoadBool_boolAsVariable_471, logic_SubGraph_SaveLoadBool_uniqueID_471);
	}

	private void Relay_Set_False_471()
	{
		logic_SubGraph_SaveLoadBool_boolean_471 = local_Question01WrongAnswers_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_471 = local_Question01WrongAnswers_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_471.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_471, logic_SubGraph_SaveLoadBool_boolAsVariable_471, logic_SubGraph_SaveLoadBool_uniqueID_471);
	}

	private void Relay_Save_Out_473()
	{
		Relay_Save_474();
	}

	private void Relay_Load_Out_473()
	{
		Relay_Load_474();
	}

	private void Relay_Restart_Out_473()
	{
		Relay_Set_False_474();
	}

	private void Relay_Save_473()
	{
		logic_SubGraph_SaveLoadBool_boolean_473 = local_Question02WrongAnswers_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_473 = local_Question02WrongAnswers_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_473.Save(ref logic_SubGraph_SaveLoadBool_boolean_473, logic_SubGraph_SaveLoadBool_boolAsVariable_473, logic_SubGraph_SaveLoadBool_uniqueID_473);
	}

	private void Relay_Load_473()
	{
		logic_SubGraph_SaveLoadBool_boolean_473 = local_Question02WrongAnswers_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_473 = local_Question02WrongAnswers_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_473.Load(ref logic_SubGraph_SaveLoadBool_boolean_473, logic_SubGraph_SaveLoadBool_boolAsVariable_473, logic_SubGraph_SaveLoadBool_uniqueID_473);
	}

	private void Relay_Set_True_473()
	{
		logic_SubGraph_SaveLoadBool_boolean_473 = local_Question02WrongAnswers_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_473 = local_Question02WrongAnswers_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_473.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_473, logic_SubGraph_SaveLoadBool_boolAsVariable_473, logic_SubGraph_SaveLoadBool_uniqueID_473);
	}

	private void Relay_Set_False_473()
	{
		logic_SubGraph_SaveLoadBool_boolean_473 = local_Question02WrongAnswers_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_473 = local_Question02WrongAnswers_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_473.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_473, logic_SubGraph_SaveLoadBool_boolAsVariable_473, logic_SubGraph_SaveLoadBool_uniqueID_473);
	}

	private void Relay_Save_Out_474()
	{
	}

	private void Relay_Load_Out_474()
	{
		Relay_In_484();
	}

	private void Relay_Restart_Out_474()
	{
		Relay_False_390();
	}

	private void Relay_Save_474()
	{
		logic_SubGraph_SaveLoadBool_boolean_474 = local_Question03WrongAnswers_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_474 = local_Question03WrongAnswers_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_474.Save(ref logic_SubGraph_SaveLoadBool_boolean_474, logic_SubGraph_SaveLoadBool_boolAsVariable_474, logic_SubGraph_SaveLoadBool_uniqueID_474);
	}

	private void Relay_Load_474()
	{
		logic_SubGraph_SaveLoadBool_boolean_474 = local_Question03WrongAnswers_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_474 = local_Question03WrongAnswers_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_474.Load(ref logic_SubGraph_SaveLoadBool_boolean_474, logic_SubGraph_SaveLoadBool_boolAsVariable_474, logic_SubGraph_SaveLoadBool_uniqueID_474);
	}

	private void Relay_Set_True_474()
	{
		logic_SubGraph_SaveLoadBool_boolean_474 = local_Question03WrongAnswers_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_474 = local_Question03WrongAnswers_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_474.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_474, logic_SubGraph_SaveLoadBool_boolAsVariable_474, logic_SubGraph_SaveLoadBool_uniqueID_474);
	}

	private void Relay_Set_False_474()
	{
		logic_SubGraph_SaveLoadBool_boolean_474 = local_Question03WrongAnswers_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_474 = local_Question03WrongAnswers_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_474.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_474, logic_SubGraph_SaveLoadBool_boolAsVariable_474, logic_SubGraph_SaveLoadBool_uniqueID_474);
	}

	private void Relay_In_476()
	{
		logic_uScript_AddMessage_messageData_476 = msgSpawnMinion;
		logic_uScript_AddMessage_speaker_476 = messageSpeaker;
		logic_uScript_AddMessage_Return_476 = logic_uScript_AddMessage_uScript_AddMessage_476.In(logic_uScript_AddMessage_messageData_476, logic_uScript_AddMessage_speaker_476);
		if (logic_uScript_AddMessage_uScript_AddMessage_476.Out)
		{
			Relay_In_381();
		}
	}

	private void Relay_Out_481()
	{
	}

	private void Relay_In_481()
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_481 = local_Stage_System_Int32;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_481.In(ref logic_SubGraph_CompleteObjectiveStage_objectiveStage_481, logic_SubGraph_CompleteObjectiveStage_isFinalObjective_481);
	}

	private void Relay_Out_484()
	{
	}

	private void Relay_In_484()
	{
		logic_SubGraph_LoadObjectiveStates_currentObjective_484 = local_Stage_System_Int32;
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_484.In(logic_SubGraph_LoadObjectiveStates_currentObjective_484);
	}

	private void Relay_True_486()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_486.True(out logic_uScriptAct_SetBool_Target_486);
		local_msgIntro_System_Boolean = logic_uScriptAct_SetBool_Target_486;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_486.Out)
		{
			Relay_In_261();
		}
	}

	private void Relay_False_486()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_486.False(out logic_uScriptAct_SetBool_Target_486);
		local_msgIntro_System_Boolean = logic_uScriptAct_SetBool_Target_486;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_486.Out)
		{
			Relay_In_261();
		}
	}

	private void Relay_In_487()
	{
		logic_uScript_IsPlayerInRangeOfTech_tech_487 = local_NPCTech_Tank;
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_487.In(logic_uScript_IsPlayerInRangeOfTech_tech_487, logic_uScript_IsPlayerInRangeOfTech_range_487, logic_uScript_IsPlayerInRangeOfTech_techs_487);
		if (logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_487.InRange)
		{
			Relay_In_198();
		}
	}

	private void Relay_Save_Out_490()
	{
		Relay_Save_71();
	}

	private void Relay_Load_Out_490()
	{
		Relay_Load_71();
	}

	private void Relay_Restart_Out_490()
	{
		Relay_Set_False_71();
	}

	private void Relay_Save_490()
	{
		logic_SubGraph_SaveLoadBool_boolean_490 = local_msgBaseFoundShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_490 = local_msgBaseFoundShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_490.Save(ref logic_SubGraph_SaveLoadBool_boolean_490, logic_SubGraph_SaveLoadBool_boolAsVariable_490, logic_SubGraph_SaveLoadBool_uniqueID_490);
	}

	private void Relay_Load_490()
	{
		logic_SubGraph_SaveLoadBool_boolean_490 = local_msgBaseFoundShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_490 = local_msgBaseFoundShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_490.Load(ref logic_SubGraph_SaveLoadBool_boolean_490, logic_SubGraph_SaveLoadBool_boolAsVariable_490, logic_SubGraph_SaveLoadBool_uniqueID_490);
	}

	private void Relay_Set_True_490()
	{
		logic_SubGraph_SaveLoadBool_boolean_490 = local_msgBaseFoundShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_490 = local_msgBaseFoundShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_490.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_490, logic_SubGraph_SaveLoadBool_boolAsVariable_490, logic_SubGraph_SaveLoadBool_uniqueID_490);
	}

	private void Relay_Set_False_490()
	{
		logic_SubGraph_SaveLoadBool_boolean_490 = local_msgBaseFoundShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_490 = local_msgBaseFoundShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_490.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_490, logic_SubGraph_SaveLoadBool_boolAsVariable_490, logic_SubGraph_SaveLoadBool_uniqueID_490);
	}

	private void Relay_Out_1062()
	{
	}

	private void Relay_Shown_1062()
	{
		Relay_True_293();
	}

	private void Relay_In_1062()
	{
		logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboard_1062 = msg02BaseFound;
		logic_SubGraph_AddMessageWithPadSupport_messageControlPad_1062 = msg02BaseFound_Pad;
		logic_SubGraph_AddMessageWithPadSupport_speaker_1062 = messageSpeaker;
		logic_SubGraph_AddMessageWithPadSupport_SubGraph_AddMessageWithPadSupport_1062.In(logic_SubGraph_AddMessageWithPadSupport_messageMouseKeyboard_1062, logic_SubGraph_AddMessageWithPadSupport_messageControlPad_1062, logic_SubGraph_AddMessageWithPadSupport_speaker_1062);
	}
}
