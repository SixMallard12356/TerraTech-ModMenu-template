using System;
using BehaviorDesigner.Runtime;
using UnityEngine;

[Serializable]
[NodePath("Graphs")]
[FriendlyName("msg04QuestionOneCorrect", "")]
public class Mission_Grade2_Quiz_Mission_Template : uScriptLogic
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

	private Tank[] local_117_TankArray = new Tank[0];

	private Tank[] local_132_TankArray = new Tank[0];

	private Tank[] local_173_TankArray = new Tank[0];

	private string local_273_System_String = "";

	private string local_275_System_String = "Question: ";

	private Tank[] local_39_TankArray = new Tank[0];

	private Tank[] local_83_TankArray = new Tank[0];

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

	private bool local_Q4EnemyAlive_System_Boolean;

	private bool local_Q5EnemyAlive_System_Boolean;

	private bool local_Q6EnemyAlive_System_Boolean;

	private bool local_Question01WrongAnswers_System_Boolean;

	private bool local_Question02WrongAnswers_System_Boolean;

	private bool local_Question03WrongAnswers_System_Boolean;

	private bool local_Question04WrongAnswers_System_Boolean;

	private bool local_Question05WrongAnswers_System_Boolean;

	private bool local_Question06WrongAnswers_System_Boolean;

	private int local_QuestionFive_System_Int32 = 1;

	private int local_QuestionFour_System_Int32 = 1;

	private int local_QuestionOne_System_Int32 = 1;

	private int local_QuestionSix_System_Int32 = 1;

	private int local_QuestionsStage_System_Int32 = 1;

	private int local_QuestionThree_System_Int32 = 1;

	private int local_QuestionTwo_System_Int32 = 1;

	private int local_Stage_System_Int32 = 1;

	public uScript_AddMessage.MessageSpeaker messageSpeaker;

	[Multiline(3)]
	public string messageTag = "";

	public uScript_AddMessage.MessageData msg01Intro;

	public uScript_AddMessage.MessageData msg02BaseFound;

	public uScript_AddMessage.MessageData msg03QuestionOne;

	public uScript_AddMessage.MessageData msg04QuestionOneCorrect;

	public uScript_AddMessage.MessageData msg05QuestionOneWrong;

	public uScript_AddMessage.MessageData msg06QuestionTwo;

	public uScript_AddMessage.MessageData msg07QuestionTwoCorrect;

	public uScript_AddMessage.MessageData msg08QuestionTwoWrong;

	public uScript_AddMessage.MessageData msg09QuestionThree;

	public uScript_AddMessage.MessageData msg10QuestionThreeCorrect;

	public uScript_AddMessage.MessageData msg11QuestionThreeWrong;

	public uScript_AddMessage.MessageData msg12QuestionFour;

	public uScript_AddMessage.MessageData msg13QuestionFourCorrect;

	public uScript_AddMessage.MessageData msg14QuestionFourWrong;

	public uScript_AddMessage.MessageData msg15QuestionFive;

	public uScript_AddMessage.MessageData msg16QuestionFiveCorrect;

	public uScript_AddMessage.MessageData msg17QuestionFiveWrong;

	public uScript_AddMessage.MessageData msg18QuestionSix;

	public uScript_AddMessage.MessageData msg19QuestionSixCorrect;

	public uScript_AddMessage.MessageData msg20QuestionSixWrong;

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

	public bool Q4Button1;

	public bool Q4Button2;

	public bool Q4Button3;

	public bool Q4Button4;

	public bool Q5Button1;

	public bool Q5Button2;

	public bool Q5Button3;

	public bool Q5Button4;

	public bool Q6Button1;

	public bool Q6Button2;

	public bool Q6Button3;

	public bool Q6Button4;

	public float WaitingTime;

	private GameObject owner_Connection_2;

	private GameObject owner_Connection_21;

	private GameObject owner_Connection_27;

	private GameObject owner_Connection_33;

	private GameObject owner_Connection_54;

	private GameObject owner_Connection_56;

	private GameObject owner_Connection_57;

	private GameObject owner_Connection_58;

	private GameObject owner_Connection_82;

	private GameObject owner_Connection_149;

	private GameObject owner_Connection_162;

	private GameObject owner_Connection_196;

	private GameObject owner_Connection_210;

	private GameObject owner_Connection_213;

	private GameObject owner_Connection_237;

	private GameObject owner_Connection_263;

	private GameObject owner_Connection_267;

	private GameObject owner_Connection_279;

	private GameObject owner_Connection_282;

	private GameObject owner_Connection_289;

	private GameObject owner_Connection_296;

	private GameObject owner_Connection_328;

	private GameObject owner_Connection_337;

	private GameObject owner_Connection_340;

	private GameObject owner_Connection_342;

	private GameObject owner_Connection_344;

	private GameObject owner_Connection_415;

	private GameObject owner_Connection_421;

	private GameObject owner_Connection_485;

	private GameObject owner_Connection_490;

	private GameObject owner_Connection_509;

	private GameObject owner_Connection_527;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_9 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_9;

	private bool logic_uScriptCon_CompareBool_True_9 = true;

	private bool logic_uScriptCon_CompareBool_False_9 = true;

	private uScript_SetCustomRadarTeamID logic_uScript_SetCustomRadarTeamID_uScript_SetCustomRadarTeamID_11 = new uScript_SetCustomRadarTeamID();

	private Tank logic_uScript_SetCustomRadarTeamID_tech_11;

	private int logic_uScript_SetCustomRadarTeamID_radarTeamID_11 = -2;

	private bool logic_uScript_SetCustomRadarTeamID_Out_11 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_12 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_12;

	private bool logic_uScriptCon_CompareBool_True_12 = true;

	private bool logic_uScriptCon_CompareBool_False_12 = true;

	private uScript_SetTankInvulnerable logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_13 = new uScript_SetTankInvulnerable();

	private bool logic_uScript_SetTankInvulnerable_invulnerable_13 = true;

	private Tank logic_uScript_SetTankInvulnerable_tank_13;

	private bool logic_uScript_SetTankInvulnerable_Out_13 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_14 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_14 = true;

	private uScript_SetCustomRadarTeamID logic_uScript_SetCustomRadarTeamID_uScript_SetCustomRadarTeamID_16 = new uScript_SetCustomRadarTeamID();

	private Tank logic_uScript_SetCustomRadarTeamID_tech_16;

	private int logic_uScript_SetCustomRadarTeamID_radarTeamID_16 = -2;

	private bool logic_uScript_SetCustomRadarTeamID_Out_16 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_19 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_19;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_19;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_19;

	private bool logic_uScript_AddMessage_Out_19 = true;

	private bool logic_uScript_AddMessage_Shown_19 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_22 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_22;

	private bool logic_uScriptAct_SetBool_Out_22 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_22 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_22 = true;

	private uScript_LockTechInteraction logic_uScript_LockTechInteraction_uScript_LockTechInteraction_30 = new uScript_LockTechInteraction();

	private Tank logic_uScript_LockTechInteraction_tech_30;

	private BlockTypes[] logic_uScript_LockTechInteraction_excludedBlocks_30 = new BlockTypes[0];

	private TankBlock[] logic_uScript_LockTechInteraction_excludedUniqueBlocks_30 = new TankBlock[0];

	private bool logic_uScript_LockTechInteraction_Out_30 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_31 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_31 = true;

	private uScript_LockTechInteraction logic_uScript_LockTechInteraction_uScript_LockTechInteraction_37 = new uScript_LockTechInteraction();

	private Tank logic_uScript_LockTechInteraction_tech_37;

	private BlockTypes[] logic_uScript_LockTechInteraction_excludedBlocks_37 = new BlockTypes[0];

	private TankBlock[] logic_uScript_LockTechInteraction_excludedUniqueBlocks_37 = new TankBlock[0];

	private bool logic_uScript_LockTechInteraction_Out_37 = true;

	private uScriptCon_CompareInt logic_uScriptCon_CompareInt_uScriptCon_CompareInt_38 = new uScriptCon_CompareInt();

	private int logic_uScriptCon_CompareInt_A_38;

	private int logic_uScriptCon_CompareInt_B_38;

	private bool logic_uScriptCon_CompareInt_GreaterThan_38 = true;

	private bool logic_uScriptCon_CompareInt_GreaterThanOrEqualTo_38 = true;

	private bool logic_uScriptCon_CompareInt_EqualTo_38 = true;

	private bool logic_uScriptCon_CompareInt_NotEqualTo_38 = true;

	private bool logic_uScriptCon_CompareInt_LessThanOrEqualTo_38 = true;

	private bool logic_uScriptCon_CompareInt_LessThan_38 = true;

	private uScript_SetTankInvulnerable logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_41 = new uScript_SetTankInvulnerable();

	private bool logic_uScript_SetTankInvulnerable_invulnerable_41 = true;

	private Tank logic_uScript_SetTankInvulnerable_tank_41;

	private bool logic_uScript_SetTankInvulnerable_Out_41 = true;

	private uScript_PausePopulation logic_uScript_PausePopulation_uScript_PausePopulation_49 = new uScript_PausePopulation();

	private bool logic_uScript_PausePopulation_Out_49 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_50 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_50 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_52 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_52;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_52 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_52 = "CanPushButtons";

	private uScript_LockTechInteraction logic_uScript_LockTechInteraction_uScript_LockTechInteraction_53 = new uScript_LockTechInteraction();

	private Tank logic_uScript_LockTechInteraction_tech_53;

	private BlockTypes[] logic_uScript_LockTechInteraction_excludedBlocks_53 = new BlockTypes[0];

	private TankBlock[] logic_uScript_LockTechInteraction_excludedUniqueBlocks_53 = new TankBlock[0];

	private bool logic_uScript_LockTechInteraction_Out_53 = true;

	private uScript_LockTechInteraction logic_uScript_LockTechInteraction_uScript_LockTechInteraction_55 = new uScript_LockTechInteraction();

	private Tank logic_uScript_LockTechInteraction_tech_55;

	private BlockTypes[] logic_uScript_LockTechInteraction_excludedBlocks_55 = new BlockTypes[0];

	private TankBlock[] logic_uScript_LockTechInteraction_excludedUniqueBlocks_55 = new TankBlock[0];

	private bool logic_uScript_LockTechInteraction_Out_55 = true;

	private uScript_LockTech logic_uScript_LockTech_uScript_LockTech_61 = new uScript_LockTech();

	private Tank logic_uScript_LockTech_tech_61;

	private uScript_LockTech.TechLockType logic_uScript_LockTech_lockType_61 = uScript_LockTech.TechLockType.LockAll;

	private bool logic_uScript_LockTech_Out_61 = true;

	private uScriptCon_CompareInt logic_uScriptCon_CompareInt_uScriptCon_CompareInt_62 = new uScriptCon_CompareInt();

	private int logic_uScriptCon_CompareInt_A_62;

	private int logic_uScriptCon_CompareInt_B_62;

	private bool logic_uScriptCon_CompareInt_GreaterThan_62 = true;

	private bool logic_uScriptCon_CompareInt_GreaterThanOrEqualTo_62 = true;

	private bool logic_uScriptCon_CompareInt_EqualTo_62 = true;

	private bool logic_uScriptCon_CompareInt_NotEqualTo_62 = true;

	private bool logic_uScriptCon_CompareInt_LessThanOrEqualTo_62 = true;

	private bool logic_uScriptCon_CompareInt_LessThan_62 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_63 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_63;

	private bool logic_uScriptAct_SetBool_Out_63 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_63 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_63 = true;

	private uScriptCon_CompareInt logic_uScriptCon_CompareInt_uScriptCon_CompareInt_64 = new uScriptCon_CompareInt();

	private int logic_uScriptCon_CompareInt_A_64;

	private int logic_uScriptCon_CompareInt_B_64;

	private bool logic_uScriptCon_CompareInt_GreaterThan_64 = true;

	private bool logic_uScriptCon_CompareInt_GreaterThanOrEqualTo_64 = true;

	private bool logic_uScriptCon_CompareInt_EqualTo_64 = true;

	private bool logic_uScriptCon_CompareInt_NotEqualTo_64 = true;

	private bool logic_uScriptCon_CompareInt_LessThanOrEqualTo_64 = true;

	private bool logic_uScriptCon_CompareInt_LessThan_64 = true;

	private uScript_SetCustomRadarTeamID logic_uScript_SetCustomRadarTeamID_uScript_SetCustomRadarTeamID_65 = new uScript_SetCustomRadarTeamID();

	private Tank logic_uScript_SetCustomRadarTeamID_tech_65;

	private int logic_uScript_SetCustomRadarTeamID_radarTeamID_65 = -2;

	private bool logic_uScript_SetCustomRadarTeamID_Out_65 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_66 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_66 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_66;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_66 = 0.3f;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_66;

	private bool logic_uScript_SpawnTechsFromData_Out_66 = true;

	private uScript_Wait logic_uScript_Wait_uScript_Wait_67 = new uScript_Wait();

	private float logic_uScript_Wait_seconds_67;

	private bool logic_uScript_Wait_repeat_67 = true;

	private bool logic_uScript_Wait_Waited_67 = true;

	private uScriptCon_CompareInt logic_uScriptCon_CompareInt_uScriptCon_CompareInt_71 = new uScriptCon_CompareInt();

	private int logic_uScriptCon_CompareInt_A_71;

	private int logic_uScriptCon_CompareInt_B_71;

	private bool logic_uScriptCon_CompareInt_GreaterThan_71 = true;

	private bool logic_uScriptCon_CompareInt_GreaterThanOrEqualTo_71 = true;

	private bool logic_uScriptCon_CompareInt_EqualTo_71 = true;

	private bool logic_uScriptCon_CompareInt_NotEqualTo_71 = true;

	private bool logic_uScriptCon_CompareInt_LessThanOrEqualTo_71 = true;

	private bool logic_uScriptCon_CompareInt_LessThan_71 = true;

	private uScript_LockTechInteraction logic_uScript_LockTechInteraction_uScript_LockTechInteraction_73 = new uScript_LockTechInteraction();

	private Tank logic_uScript_LockTechInteraction_tech_73;

	private BlockTypes[] logic_uScript_LockTechInteraction_excludedBlocks_73 = new BlockTypes[0];

	private TankBlock[] logic_uScript_LockTechInteraction_excludedUniqueBlocks_73 = new TankBlock[0];

	private bool logic_uScript_LockTechInteraction_Out_73 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_75 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_75;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_75;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_75;

	private bool logic_uScript_AddMessage_Out_75 = true;

	private bool logic_uScript_AddMessage_Shown_75 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_76 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_76;

	private bool logic_uScriptAct_SetBool_Out_76 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_76 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_76 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_78 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_78 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_78;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_78 = 0.3f;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_78;

	private bool logic_uScript_SpawnTechsFromData_Out_78 = true;

	private uScript_ClearOnScreenMessagesWithTag logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_80 = new uScript_ClearOnScreenMessagesWithTag();

	private string logic_uScript_ClearOnScreenMessagesWithTag_tag_80 = "";

	private bool logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_80 = true;

	private bool logic_uScript_ClearOnScreenMessagesWithTag_Out_80 = true;

	private uScript_SetCustomRadarTeamID logic_uScript_SetCustomRadarTeamID_uScript_SetCustomRadarTeamID_81 = new uScript_SetCustomRadarTeamID();

	private Tank logic_uScript_SetCustomRadarTeamID_tech_81;

	private int logic_uScript_SetCustomRadarTeamID_radarTeamID_81 = -2;

	private bool logic_uScript_SetCustomRadarTeamID_Out_81 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_85 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_85;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_85 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_85 = "msgIntro";

	private uScript_GetTankBlock logic_uScript_GetTankBlock_uScript_GetTankBlock_88 = new uScript_GetTankBlock();

	private Tank logic_uScript_GetTankBlock_tank_88;

	private BlockTypes logic_uScript_GetTankBlock_blockType_88;

	private TankBlock logic_uScript_GetTankBlock_Return_88;

	private bool logic_uScript_GetTankBlock_Out_88 = true;

	private bool logic_uScript_GetTankBlock_Returned_88 = true;

	private bool logic_uScript_GetTankBlock_NotFound_88 = true;

	private uScript_GetTankBlock logic_uScript_GetTankBlock_uScript_GetTankBlock_91 = new uScript_GetTankBlock();

	private Tank logic_uScript_GetTankBlock_tank_91;

	private BlockTypes logic_uScript_GetTankBlock_blockType_91;

	private TankBlock logic_uScript_GetTankBlock_Return_91;

	private bool logic_uScript_GetTankBlock_Out_91 = true;

	private bool logic_uScript_GetTankBlock_Returned_91 = true;

	private bool logic_uScript_GetTankBlock_NotFound_91 = true;

	private uScriptCon_CompareInt logic_uScriptCon_CompareInt_uScriptCon_CompareInt_92 = new uScriptCon_CompareInt();

	private int logic_uScriptCon_CompareInt_A_92;

	private int logic_uScriptCon_CompareInt_B_92;

	private bool logic_uScriptCon_CompareInt_GreaterThan_92 = true;

	private bool logic_uScriptCon_CompareInt_GreaterThanOrEqualTo_92 = true;

	private bool logic_uScriptCon_CompareInt_EqualTo_92 = true;

	private bool logic_uScriptCon_CompareInt_NotEqualTo_92 = true;

	private bool logic_uScriptCon_CompareInt_LessThanOrEqualTo_92 = true;

	private bool logic_uScriptCon_CompareInt_LessThan_92 = true;

	private uScript_GetTankBlock logic_uScript_GetTankBlock_uScript_GetTankBlock_100 = new uScript_GetTankBlock();

	private Tank logic_uScript_GetTankBlock_tank_100;

	private BlockTypes logic_uScript_GetTankBlock_blockType_100;

	private TankBlock logic_uScript_GetTankBlock_Return_100;

	private bool logic_uScript_GetTankBlock_Out_100 = true;

	private bool logic_uScript_GetTankBlock_Returned_100 = true;

	private bool logic_uScript_GetTankBlock_NotFound_100 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_101 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_101 = new Tank[0];

	private int logic_uScript_AccessListTech_index_101;

	private Tank logic_uScript_AccessListTech_value_101;

	private bool logic_uScript_AccessListTech_Out_101 = true;

	private uScriptCon_ManualSwitch logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_102 = new uScriptCon_ManualSwitch();

	private int logic_uScriptCon_ManualSwitch_CurrentOutput_102;

	private uScript_SetTankHideBlockLimit logic_uScript_SetTankHideBlockLimit_uScript_SetTankHideBlockLimit_104 = new uScript_SetTankHideBlockLimit();

	private bool logic_uScript_SetTankHideBlockLimit_hidden_104 = true;

	private Tank logic_uScript_SetTankHideBlockLimit_tech_104;

	private bool logic_uScript_SetTankHideBlockLimit_Out_104 = true;

	private uScript_LockTechInteraction logic_uScript_LockTechInteraction_uScript_LockTechInteraction_105 = new uScript_LockTechInteraction();

	private Tank logic_uScript_LockTechInteraction_tech_105;

	private BlockTypes[] logic_uScript_LockTechInteraction_excludedBlocks_105 = new BlockTypes[0];

	private TankBlock[] logic_uScript_LockTechInteraction_excludedUniqueBlocks_105 = new TankBlock[0];

	private bool logic_uScript_LockTechInteraction_Out_105 = true;

	private uScript_Wait logic_uScript_Wait_uScript_Wait_106 = new uScript_Wait();

	private float logic_uScript_Wait_seconds_106;

	private bool logic_uScript_Wait_repeat_106 = true;

	private bool logic_uScript_Wait_Waited_106 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_109 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_109;

	private bool logic_uScriptAct_SetBool_Out_109 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_109 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_109 = true;

	private uScript_SetCustomRadarTeamID logic_uScript_SetCustomRadarTeamID_uScript_SetCustomRadarTeamID_114 = new uScript_SetCustomRadarTeamID();

	private Tank logic_uScript_SetCustomRadarTeamID_tech_114;

	private int logic_uScript_SetCustomRadarTeamID_radarTeamID_114 = -2;

	private bool logic_uScript_SetCustomRadarTeamID_Out_114 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_115 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_115;

	private bool logic_uScriptAct_SetBool_Out_115 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_115 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_115 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_116 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_116 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_116;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_116 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_116;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_116 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_116 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_116 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_116 = true;

	private uScript_PausePopulation logic_uScript_PausePopulation_uScript_PausePopulation_119 = new uScript_PausePopulation();

	private bool logic_uScript_PausePopulation_Out_119 = true;

	private uScript_LockTech logic_uScript_LockTech_uScript_LockTech_123 = new uScript_LockTech();

	private Tank logic_uScript_LockTech_tech_123;

	private uScript_LockTech.TechLockType logic_uScript_LockTech_lockType_123 = uScript_LockTech.TechLockType.LockAll;

	private bool logic_uScript_LockTech_Out_123 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_124 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_124;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_124;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_124;

	private bool logic_uScript_AddMessage_Out_124 = true;

	private bool logic_uScript_AddMessage_Shown_124 = true;

	private uScript_GetCircuitChargeInfo logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_125 = new uScript_GetCircuitChargeInfo();

	private TankBlock logic_uScript_GetCircuitChargeInfo_block_125;

	private Tank logic_uScript_GetCircuitChargeInfo_tech_125;

	private BlockTypes logic_uScript_GetCircuitChargeInfo_blockType_125;

	private int logic_uScript_GetCircuitChargeInfo_Return_125;

	private bool logic_uScript_GetCircuitChargeInfo_Out_125 = true;

	private bool logic_uScript_GetCircuitChargeInfo_HasValidBlock_125 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_126 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_126 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_127 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_127 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_127;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_127 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_127;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_127 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_127 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_127 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_127 = true;

	private uScript_SetTankInvulnerable logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_130 = new uScript_SetTankInvulnerable();

	private bool logic_uScript_SetTankInvulnerable_invulnerable_130 = true;

	private Tank logic_uScript_SetTankInvulnerable_tank_130;

	private bool logic_uScript_SetTankInvulnerable_Out_130 = true;

	private uScript_LockTech logic_uScript_LockTech_uScript_LockTech_133 = new uScript_LockTech();

	private Tank logic_uScript_LockTech_tech_133;

	private uScript_LockTech.TechLockType logic_uScript_LockTech_lockType_133 = uScript_LockTech.TechLockType.LockAll;

	private bool logic_uScript_LockTech_Out_133 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_135 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_135;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_135;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_135;

	private bool logic_uScript_AddMessage_Out_135 = true;

	private bool logic_uScript_AddMessage_Shown_135 = true;

	private uScript_LockTechInteraction logic_uScript_LockTechInteraction_uScript_LockTechInteraction_137 = new uScript_LockTechInteraction();

	private Tank logic_uScript_LockTechInteraction_tech_137;

	private BlockTypes[] logic_uScript_LockTechInteraction_excludedBlocks_137 = new BlockTypes[0];

	private TankBlock[] logic_uScript_LockTechInteraction_excludedUniqueBlocks_137 = new TankBlock[0];

	private bool logic_uScript_LockTechInteraction_Out_137 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_141 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_141;

	private bool logic_uScriptCon_CompareBool_True_141 = true;

	private bool logic_uScriptCon_CompareBool_False_141 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_144 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_144;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_144;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_144;

	private bool logic_uScript_AddMessage_Out_144 = true;

	private bool logic_uScript_AddMessage_Shown_144 = true;

	private uScript_IsPlayerInRangeOfTech logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_145 = new uScript_IsPlayerInRangeOfTech();

	private Tank logic_uScript_IsPlayerInRangeOfTech_tech_145;

	private float logic_uScript_IsPlayerInRangeOfTech_range_145;

	private Tank[] logic_uScript_IsPlayerInRangeOfTech_techs_145 = new Tank[0];

	private bool logic_uScript_IsPlayerInRangeOfTech_Out_145 = true;

	private bool logic_uScript_IsPlayerInRangeOfTech_InRange_145 = true;

	private bool logic_uScript_IsPlayerInRangeOfTech_OutOfRange_145 = true;

	private SubGraph_SaveLoadInt logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_147 = new SubGraph_SaveLoadInt();

	private int logic_SubGraph_SaveLoadInt_restartValue_147 = 1;

	private int logic_SubGraph_SaveLoadInt_integer_147;

	private object logic_SubGraph_SaveLoadInt_intAsVariable_147 = "";

	private string logic_SubGraph_SaveLoadInt_uniqueID_147 = "QuestionTwo";

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_150 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_150;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_150;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_150;

	private bool logic_uScript_AddMessage_Out_150 = true;

	private bool logic_uScript_AddMessage_Shown_150 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_151 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_151 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_151;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_151 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_151;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_151 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_151 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_151 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_151 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_154 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_154;

	private bool logic_uScriptCon_CompareBool_True_154 = true;

	private bool logic_uScriptCon_CompareBool_False_154 = true;

	private uScript_GetCircuitChargeInfo logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_156 = new uScript_GetCircuitChargeInfo();

	private TankBlock logic_uScript_GetCircuitChargeInfo_block_156;

	private Tank logic_uScript_GetCircuitChargeInfo_tech_156;

	private BlockTypes logic_uScript_GetCircuitChargeInfo_blockType_156;

	private int logic_uScript_GetCircuitChargeInfo_Return_156;

	private bool logic_uScript_GetCircuitChargeInfo_Out_156 = true;

	private bool logic_uScript_GetCircuitChargeInfo_HasValidBlock_156 = true;

	private uScript_GetTankBlock logic_uScript_GetTankBlock_uScript_GetTankBlock_161 = new uScript_GetTankBlock();

	private Tank logic_uScript_GetTankBlock_tank_161;

	private BlockTypes logic_uScript_GetTankBlock_blockType_161;

	private TankBlock logic_uScript_GetTankBlock_Return_161;

	private bool logic_uScript_GetTankBlock_Out_161 = true;

	private bool logic_uScript_GetTankBlock_Returned_161 = true;

	private bool logic_uScript_GetTankBlock_NotFound_161 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_163 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_163 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_164 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_164 = new Tank[0];

	private int logic_uScript_AccessListTech_index_164;

	private Tank logic_uScript_AccessListTech_value_164;

	private bool logic_uScript_AccessListTech_Out_164 = true;

	private uScript_SetEncounterTarget logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_165 = new uScript_SetEncounterTarget();

	private GameObject logic_uScript_SetEncounterTarget_owner_165;

	private object logic_uScript_SetEncounterTarget_visibleObject_165 = "";

	private bool logic_uScript_SetEncounterTarget_Out_165 = true;

	private uScript_GetCircuitChargeInfo logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_169 = new uScript_GetCircuitChargeInfo();

	private TankBlock logic_uScript_GetCircuitChargeInfo_block_169;

	private Tank logic_uScript_GetCircuitChargeInfo_tech_169;

	private BlockTypes logic_uScript_GetCircuitChargeInfo_blockType_169;

	private int logic_uScript_GetCircuitChargeInfo_Return_169;

	private bool logic_uScript_GetCircuitChargeInfo_Out_169 = true;

	private bool logic_uScript_GetCircuitChargeInfo_HasValidBlock_169 = true;

	private uScript_LockTech logic_uScript_LockTech_uScript_LockTech_170 = new uScript_LockTech();

	private Tank logic_uScript_LockTech_tech_170;

	private uScript_LockTech.TechLockType logic_uScript_LockTech_lockType_170 = uScript_LockTech.TechLockType.LockAll;

	private bool logic_uScript_LockTech_Out_170 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_174 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_174 = new Tank[0];

	private int logic_uScript_AccessListTech_index_174;

	private Tank logic_uScript_AccessListTech_value_174;

	private bool logic_uScript_AccessListTech_Out_174 = true;

	private uScript_GetCircuitChargeInfo logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_176 = new uScript_GetCircuitChargeInfo();

	private TankBlock logic_uScript_GetCircuitChargeInfo_block_176;

	private Tank logic_uScript_GetCircuitChargeInfo_tech_176;

	private BlockTypes logic_uScript_GetCircuitChargeInfo_blockType_176;

	private int logic_uScript_GetCircuitChargeInfo_Return_176;

	private bool logic_uScript_GetCircuitChargeInfo_Out_176 = true;

	private bool logic_uScript_GetCircuitChargeInfo_HasValidBlock_176 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_177 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_177 = new Tank[0];

	private int logic_uScript_AccessListTech_index_177;

	private Tank logic_uScript_AccessListTech_value_177;

	private bool logic_uScript_AccessListTech_Out_177 = true;

	private uScriptCon_ManualSwitch logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_181 = new uScriptCon_ManualSwitch();

	private int logic_uScriptCon_ManualSwitch_CurrentOutput_181;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_184 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_184 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_184;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_184 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_184;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_184 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_184 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_184 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_184 = true;

	private uScript_SetTankInvulnerable logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_185 = new uScript_SetTankInvulnerable();

	private bool logic_uScript_SetTankInvulnerable_invulnerable_185 = true;

	private Tank logic_uScript_SetTankInvulnerable_tank_185;

	private bool logic_uScript_SetTankInvulnerable_Out_185 = true;

	private uScript_SetTankHideBlockLimit logic_uScript_SetTankHideBlockLimit_uScript_SetTankHideBlockLimit_186 = new uScript_SetTankHideBlockLimit();

	private bool logic_uScript_SetTankHideBlockLimit_hidden_186 = true;

	private Tank logic_uScript_SetTankHideBlockLimit_tech_186;

	private bool logic_uScript_SetTankHideBlockLimit_Out_186 = true;

	private uScript_SetTankHideBlockLimit logic_uScript_SetTankHideBlockLimit_uScript_SetTankHideBlockLimit_188 = new uScript_SetTankHideBlockLimit();

	private bool logic_uScript_SetTankHideBlockLimit_hidden_188 = true;

	private Tank logic_uScript_SetTankHideBlockLimit_tech_188;

	private bool logic_uScript_SetTankHideBlockLimit_Out_188 = true;

	private uScript_RemoveScenery logic_uScript_RemoveScenery_uScript_RemoveScenery_189 = new uScript_RemoveScenery();

	private GameObject logic_uScript_RemoveScenery_ownerNode_189;

	private string logic_uScript_RemoveScenery_positionName_189 = "";

	private float logic_uScript_RemoveScenery_radius_189;

	private bool logic_uScript_RemoveScenery_preventChunksSpawning_189 = true;

	private bool logic_uScript_RemoveScenery_Out_189 = true;

	private uScript_LockTechInteraction logic_uScript_LockTechInteraction_uScript_LockTechInteraction_192 = new uScript_LockTechInteraction();

	private Tank logic_uScript_LockTechInteraction_tech_192;

	private BlockTypes[] logic_uScript_LockTechInteraction_excludedBlocks_192 = new BlockTypes[0];

	private TankBlock[] logic_uScript_LockTechInteraction_excludedUniqueBlocks_192 = new TankBlock[0];

	private bool logic_uScript_LockTechInteraction_Out_192 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_194 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_194 = true;

	private uScript_AccessListTech logic_uScript_AccessListTech_uScript_AccessListTech_197 = new uScript_AccessListTech();

	private Tank[] logic_uScript_AccessListTech_techList_197 = new Tank[0];

	private int logic_uScript_AccessListTech_index_197;

	private Tank logic_uScript_AccessListTech_value_197;

	private bool logic_uScript_AccessListTech_Out_197 = true;

	private uScript_LockTech logic_uScript_LockTech_uScript_LockTech_201 = new uScript_LockTech();

	private Tank logic_uScript_LockTech_tech_201;

	private uScript_LockTech.TechLockType logic_uScript_LockTech_lockType_201 = uScript_LockTech.TechLockType.LockAll;

	private bool logic_uScript_LockTech_Out_201 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_202 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_202;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_202;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_202;

	private bool logic_uScript_AddMessage_Out_202 = true;

	private bool logic_uScript_AddMessage_Shown_202 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_203 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_203;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_203;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_203;

	private bool logic_uScript_AddMessage_Out_203 = true;

	private bool logic_uScript_AddMessage_Shown_203 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_204 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_204;

	private bool logic_uScriptCon_CompareBool_True_204 = true;

	private bool logic_uScriptCon_CompareBool_False_204 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_206 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_206;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_206;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_206;

	private bool logic_uScript_AddMessage_Out_206 = true;

	private bool logic_uScript_AddMessage_Shown_206 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_208 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_208;

	private bool logic_uScriptCon_CompareBool_True_208 = true;

	private bool logic_uScriptCon_CompareBool_False_208 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_209 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_209 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_211 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_211;

	private bool logic_uScriptAct_SetBool_Out_211 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_211 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_211 = true;

	private SubGraph_SaveLoadInt logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_217 = new SubGraph_SaveLoadInt();

	private int logic_SubGraph_SaveLoadInt_restartValue_217 = 1;

	private int logic_SubGraph_SaveLoadInt_integer_217;

	private object logic_SubGraph_SaveLoadInt_intAsVariable_217 = "";

	private string logic_SubGraph_SaveLoadInt_uniqueID_217 = "Stage";

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_222 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_222 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_223 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_223;

	private bool logic_uScriptAct_SetBool_Out_223 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_223 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_223 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_224 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_224 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_224;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_224 = 0.3f;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_224;

	private bool logic_uScript_SpawnTechsFromData_Out_224 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_225 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_225 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_228 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_228;

	private bool logic_uScriptCon_CompareBool_True_228 = true;

	private bool logic_uScriptCon_CompareBool_False_228 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_230 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_230;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_230;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_230;

	private bool logic_uScript_AddMessage_Out_230 = true;

	private bool logic_uScript_AddMessage_Shown_230 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_231 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_231 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_232 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_232;

	private bool logic_uScriptAct_SetBool_Out_232 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_232 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_232 = true;

	private SubGraph_SaveLoadInt logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_236 = new SubGraph_SaveLoadInt();

	private int logic_SubGraph_SaveLoadInt_restartValue_236 = 1;

	private int logic_SubGraph_SaveLoadInt_integer_236;

	private object logic_SubGraph_SaveLoadInt_intAsVariable_236 = "";

	private string logic_SubGraph_SaveLoadInt_uniqueID_236 = "QuestionOne";

	private uScript_DirectEnemiesOutOfEncounter logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_241 = new uScript_DirectEnemiesOutOfEncounter();

	private GameObject logic_uScript_DirectEnemiesOutOfEncounter_ownerNode_241;

	private bool logic_uScript_DirectEnemiesOutOfEncounter_Out_241 = true;

	private uScript_Wait logic_uScript_Wait_uScript_Wait_242 = new uScript_Wait();

	private float logic_uScript_Wait_seconds_242;

	private bool logic_uScript_Wait_repeat_242 = true;

	private bool logic_uScript_Wait_Waited_242 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_243 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_243 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_243;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_243 = 0.3f;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_243;

	private bool logic_uScript_SpawnTechsFromData_Out_243 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_244 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_244 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_244;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_244 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_244;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_244 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_244 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_244 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_244 = true;

	private uScriptCon_ManualSwitch logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_245 = new uScriptCon_ManualSwitch();

	private int logic_uScriptCon_ManualSwitch_CurrentOutput_245;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_247 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_247 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_247;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_247 = 0.3f;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_247;

	private bool logic_uScript_SpawnTechsFromData_Out_247 = true;

	private uScript_SetTankHideBlockLimit logic_uScript_SetTankHideBlockLimit_uScript_SetTankHideBlockLimit_248 = new uScript_SetTankHideBlockLimit();

	private bool logic_uScript_SetTankHideBlockLimit_hidden_248 = true;

	private Tank logic_uScript_SetTankHideBlockLimit_tech_248;

	private bool logic_uScript_SetTankHideBlockLimit_Out_248 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_249 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_249;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_249 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_249 = "Initialize";

	private uScript_GetCircuitChargeInfo logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_250 = new uScript_GetCircuitChargeInfo();

	private TankBlock logic_uScript_GetCircuitChargeInfo_block_250;

	private Tank logic_uScript_GetCircuitChargeInfo_tech_250;

	private BlockTypes logic_uScript_GetCircuitChargeInfo_blockType_250;

	private int logic_uScript_GetCircuitChargeInfo_Return_250;

	private bool logic_uScript_GetCircuitChargeInfo_Out_250 = true;

	private bool logic_uScript_GetCircuitChargeInfo_HasValidBlock_250 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_251 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_251;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_251;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_251;

	private bool logic_uScript_AddMessage_Out_251 = true;

	private bool logic_uScript_AddMessage_Shown_251 = true;

	private SubGraph_SaveLoadInt logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_252 = new SubGraph_SaveLoadInt();

	private int logic_SubGraph_SaveLoadInt_restartValue_252 = 1;

	private int logic_SubGraph_SaveLoadInt_integer_252;

	private object logic_SubGraph_SaveLoadInt_intAsVariable_252 = "";

	private string logic_SubGraph_SaveLoadInt_uniqueID_252 = "QuestionThree";

	private uScript_LockTechInteraction logic_uScript_LockTechInteraction_uScript_LockTechInteraction_254 = new uScript_LockTechInteraction();

	private Tank logic_uScript_LockTechInteraction_tech_254;

	private BlockTypes[] logic_uScript_LockTechInteraction_excludedBlocks_254 = new BlockTypes[0];

	private TankBlock[] logic_uScript_LockTechInteraction_excludedUniqueBlocks_254 = new TankBlock[0];

	private bool logic_uScript_LockTechInteraction_Out_254 = true;

	private uScriptAct_Passthrough logic_uScriptAct_Passthrough_uScriptAct_Passthrough_255 = new uScriptAct_Passthrough();

	private bool logic_uScriptAct_Passthrough_Out_255 = true;

	private uScript_SetTankInvulnerable logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_260 = new uScript_SetTankInvulnerable();

	private bool logic_uScript_SetTankInvulnerable_invulnerable_260 = true;

	private Tank logic_uScript_SetTankInvulnerable_tank_260;

	private bool logic_uScript_SetTankInvulnerable_Out_260 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_264 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_264;

	private bool logic_uScriptCon_CompareBool_True_264 = true;

	private bool logic_uScriptCon_CompareBool_False_264 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_265 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_265 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_265;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_265 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_265;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_265 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_265 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_265 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_265 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_266 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_266 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_266;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_266;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_266 = true;

	private bool logic_uScript_SpawnTechsFromData_Out_266 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_268 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_268;

	private bool logic_uScriptAct_SetBool_Out_268 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_268 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_268 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_270 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_270;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_270 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_270 = "Q1EnemyAlive";

	private uScriptAct_Concatenate logic_uScriptAct_Concatenate_uScriptAct_Concatenate_272 = new uScriptAct_Concatenate();

	private object[] logic_uScriptAct_Concatenate_A_272 = new object[0];

	private object[] logic_uScriptAct_Concatenate_B_272 = new object[0];

	private string logic_uScriptAct_Concatenate_Separator_272 = "";

	private string logic_uScriptAct_Concatenate_Result_272;

	private bool logic_uScriptAct_Concatenate_Out_272 = true;

	private uScriptAct_PrintText logic_uScriptAct_PrintText_uScriptAct_PrintText_274 = new uScriptAct_PrintText();

	private string logic_uScriptAct_PrintText_Text_274 = "";

	private int logic_uScriptAct_PrintText_FontSize_274 = 16;

	private FontStyle logic_uScriptAct_PrintText_FontStyle_274;

	private Color logic_uScriptAct_PrintText_FontColor_274 = new Color(0f, 0f, 0f, 1f);

	private TextAnchor logic_uScriptAct_PrintText_textAnchor_274;

	private int logic_uScriptAct_PrintText_EdgePadding_274 = 8;

	private float logic_uScriptAct_PrintText_time_274;

	private bool logic_uScriptAct_PrintText_Out_274 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_277 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_277 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_277;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_277;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_277 = true;

	private bool logic_uScript_SpawnTechsFromData_Out_277 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_278 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_278 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_278;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_278 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_278;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_278 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_278 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_278 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_278 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_281 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_281;

	private bool logic_uScriptAct_SetBool_Out_281 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_281 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_281 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_283 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_283;

	private bool logic_uScriptCon_CompareBool_True_283 = true;

	private bool logic_uScriptCon_CompareBool_False_283 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_291 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_291;

	private bool logic_uScriptCon_CompareBool_True_291 = true;

	private bool logic_uScriptCon_CompareBool_False_291 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_293 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_293;

	private bool logic_uScriptAct_SetBool_Out_293 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_293 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_293 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_295 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_295 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_295;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_295;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_295 = true;

	private bool logic_uScript_SpawnTechsFromData_Out_295 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_297 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_297 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_297;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_297 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_297;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_297 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_297 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_297 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_297 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_301 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_301;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_301 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_301 = "Q2EnemyAlive";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_303 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_303;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_303 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_303 = "Q3EnemyAlive";

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_305 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_305;

	private bool logic_uScriptAct_SetBool_Out_305 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_305 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_305 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_309 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_309;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_309;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_309;

	private bool logic_uScript_AddMessage_Out_309 = true;

	private bool logic_uScript_AddMessage_Shown_309 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_313 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_313;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_313;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_313;

	private bool logic_uScript_AddMessage_Out_313 = true;

	private bool logic_uScript_AddMessage_Shown_313 = true;

	private uScriptAct_AddInt_v2 logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_314 = new uScriptAct_AddInt_v2();

	private int logic_uScriptAct_AddInt_v2_A_314;

	private int logic_uScriptAct_AddInt_v2_B_314 = 1;

	private int logic_uScriptAct_AddInt_v2_IntResult_314;

	private float logic_uScriptAct_AddInt_v2_FloatResult_314;

	private bool logic_uScriptAct_AddInt_v2_Out_314 = true;

	private uScript_Wait logic_uScript_Wait_uScript_Wait_316 = new uScript_Wait();

	private float logic_uScript_Wait_seconds_316;

	private bool logic_uScript_Wait_repeat_316 = true;

	private bool logic_uScript_Wait_Waited_316 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_320 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_320;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_320;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_320;

	private bool logic_uScript_AddMessage_Out_320 = true;

	private bool logic_uScript_AddMessage_Shown_320 = true;

	private uScript_Wait logic_uScript_Wait_uScript_Wait_321 = new uScript_Wait();

	private float logic_uScript_Wait_seconds_321;

	private bool logic_uScript_Wait_repeat_321 = true;

	private bool logic_uScript_Wait_Waited_321 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_323 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_323;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_323;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_323;

	private bool logic_uScript_AddMessage_Out_323 = true;

	private bool logic_uScript_AddMessage_Shown_323 = true;

	private uScriptAct_AddInt_v2 logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_326 = new uScriptAct_AddInt_v2();

	private int logic_uScriptAct_AddInt_v2_A_326;

	private int logic_uScriptAct_AddInt_v2_B_326 = 1;

	private int logic_uScriptAct_AddInt_v2_IntResult_326;

	private float logic_uScriptAct_AddInt_v2_FloatResult_326;

	private bool logic_uScriptAct_AddInt_v2_Out_326 = true;

	private uScript_RemoveTech logic_uScript_RemoveTech_uScript_RemoveTech_329 = new uScript_RemoveTech();

	private Tank logic_uScript_RemoveTech_tech_329;

	private bool logic_uScript_RemoveTech_Out_329 = true;

	private uScript_SpawnVFX logic_uScript_SpawnVFX_uScript_SpawnVFX_334 = new uScript_SpawnVFX();

	private GameObject logic_uScript_SpawnVFX_ownerNode_334;

	private Transform logic_uScript_SpawnVFX_vfxToSpawn_334;

	private string logic_uScript_SpawnVFX_spawnPosName_334 = "";

	private bool logic_uScript_SpawnVFX_Out_334 = true;

	private uScript_SpawnVFX logic_uScript_SpawnVFX_uScript_SpawnVFX_339 = new uScript_SpawnVFX();

	private GameObject logic_uScript_SpawnVFX_ownerNode_339;

	private Transform logic_uScript_SpawnVFX_vfxToSpawn_339;

	private string logic_uScript_SpawnVFX_spawnPosName_339 = "";

	private bool logic_uScript_SpawnVFX_Out_339 = true;

	private uScript_RemoveTech logic_uScript_RemoveTech_uScript_RemoveTech_345 = new uScript_RemoveTech();

	private Tank logic_uScript_RemoveTech_tech_345;

	private bool logic_uScript_RemoveTech_Out_345 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_347 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_347;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_347;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_347;

	private bool logic_uScript_AddMessage_Out_347 = true;

	private bool logic_uScript_AddMessage_Shown_347 = true;

	private uScript_RemoveTech logic_uScript_RemoveTech_uScript_RemoveTech_348 = new uScript_RemoveTech();

	private Tank logic_uScript_RemoveTech_tech_348;

	private bool logic_uScript_RemoveTech_Out_348 = true;

	private uScript_SpawnVFX logic_uScript_SpawnVFX_uScript_SpawnVFX_351 = new uScript_SpawnVFX();

	private GameObject logic_uScript_SpawnVFX_ownerNode_351;

	private Transform logic_uScript_SpawnVFX_vfxToSpawn_351;

	private string logic_uScript_SpawnVFX_spawnPosName_351 = "";

	private bool logic_uScript_SpawnVFX_Out_351 = true;

	private uScript_FlyTechUpAndAway logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_353 = new uScript_FlyTechUpAndAway();

	private Tank logic_uScript_FlyTechUpAndAway_tech_353;

	private float logic_uScript_FlyTechUpAndAway_maxLifetime_353 = 5f;

	private float logic_uScript_FlyTechUpAndAway_targetHeight_353 = 150f;

	private ExternalBehaviorTree logic_uScript_FlyTechUpAndAway_aiTree_353;

	private Transform logic_uScript_FlyTechUpAndAway_removalParticles_353;

	private bool logic_uScript_FlyTechUpAndAway_Out_353 = true;

	private uScript_FinishEncounter logic_uScript_FinishEncounter_uScript_FinishEncounter_358 = new uScript_FinishEncounter();

	private GameObject logic_uScript_FinishEncounter_owner_358;

	private bool logic_uScript_FinishEncounter_Out_358 = true;

	private uScript_RemoveTech logic_uScript_RemoveTech_uScript_RemoveTech_359 = new uScript_RemoveTech();

	private Tank logic_uScript_RemoveTech_tech_359;

	private bool logic_uScript_RemoveTech_Out_359 = true;

	private uScript_SpawnVFX logic_uScript_SpawnVFX_uScript_SpawnVFX_360 = new uScript_SpawnVFX();

	private GameObject logic_uScript_SpawnVFX_ownerNode_360;

	private Transform logic_uScript_SpawnVFX_vfxToSpawn_360;

	private string logic_uScript_SpawnVFX_spawnPosName_360 = "";

	private bool logic_uScript_SpawnVFX_Out_360 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_361 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_361 = 6;

	private int logic_uScriptAct_SetInt_Target_361;

	private bool logic_uScriptAct_SetInt_Out_361 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_363 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_363;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_363 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_363 = "Q1Wrong Answers";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_365 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_365;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_365 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_365 = "Q2Wrong Answers";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_366 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_366;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_366 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_366 = "Q3Wrong Answers";

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_368 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_368;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_368;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_368;

	private bool logic_uScript_AddMessage_Out_368 = true;

	private bool logic_uScript_AddMessage_Shown_368 = true;

	private SubGraph_LoadObjectiveStates logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_372 = new SubGraph_LoadObjectiveStates();

	private int logic_SubGraph_LoadObjectiveStates_currentObjective_372;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_374 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_374;

	private bool logic_uScriptAct_SetBool_Out_374 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_374 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_374 = true;

	private uScript_IsPlayerInRangeOfTech logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_375 = new uScript_IsPlayerInRangeOfTech();

	private Tank logic_uScript_IsPlayerInRangeOfTech_tech_375;

	private float logic_uScript_IsPlayerInRangeOfTech_range_375 = 100f;

	private Tank[] logic_uScript_IsPlayerInRangeOfTech_techs_375 = new Tank[0];

	private bool logic_uScript_IsPlayerInRangeOfTech_Out_375 = true;

	private bool logic_uScript_IsPlayerInRangeOfTech_InRange_375 = true;

	private bool logic_uScript_IsPlayerInRangeOfTech_OutOfRange_375 = true;

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_378 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_378;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_378 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_378 = "MsgBaseFound";

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_380 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_380;

	private bool logic_uScriptCon_CompareBool_True_380 = true;

	private bool logic_uScriptCon_CompareBool_False_380 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_384 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_384;

	private bool logic_uScriptAct_SetBool_Out_384 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_384 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_384 = true;

	private SubGraph_CompleteObjectiveStage logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_386 = new SubGraph_CompleteObjectiveStage();

	private int logic_SubGraph_CompleteObjectiveStage_objectiveStage_386;

	private bool logic_SubGraph_CompleteObjectiveStage_isFinalObjective_386;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_387 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_387;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_387;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_387;

	private bool logic_uScript_AddMessage_Out_387 = true;

	private bool logic_uScript_AddMessage_Shown_387 = true;

	private uScriptCon_ManualSwitch logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_389 = new uScriptCon_ManualSwitch();

	private int logic_uScriptCon_ManualSwitch_CurrentOutput_389;

	private SubGraph_SaveLoadInt logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_397 = new SubGraph_SaveLoadInt();

	private int logic_SubGraph_SaveLoadInt_restartValue_397 = 1;

	private int logic_SubGraph_SaveLoadInt_integer_397;

	private object logic_SubGraph_SaveLoadInt_intAsVariable_397 = "";

	private string logic_SubGraph_SaveLoadInt_uniqueID_397 = "QuestionFour";

	private SubGraph_SaveLoadInt logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_399 = new SubGraph_SaveLoadInt();

	private int logic_SubGraph_SaveLoadInt_restartValue_399 = 1;

	private int logic_SubGraph_SaveLoadInt_integer_399;

	private object logic_SubGraph_SaveLoadInt_intAsVariable_399 = "";

	private string logic_SubGraph_SaveLoadInt_uniqueID_399 = "QuestionFive";

	private SubGraph_SaveLoadInt logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_401 = new SubGraph_SaveLoadInt();

	private int logic_SubGraph_SaveLoadInt_restartValue_401 = 1;

	private int logic_SubGraph_SaveLoadInt_integer_401;

	private object logic_SubGraph_SaveLoadInt_intAsVariable_401 = "";

	private string logic_SubGraph_SaveLoadInt_uniqueID_401 = "QuestionSix";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_403 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_403;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_403 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_403 = "Q4EnemyAlive";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_405 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_405;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_405 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_405 = "Q5EnemyAlive";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_407 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_407;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_407 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_407 = "Q6EnemyAlive";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_411 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_411;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_411 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_411 = "Q4Wrong Answers";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_412 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_412;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_412 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_412 = "Q6Wrong Answers";

	private SubGraph_SaveLoadBool logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_413 = new SubGraph_SaveLoadBool();

	private bool logic_SubGraph_SaveLoadBool_boolean_413;

	private object logic_SubGraph_SaveLoadBool_boolAsVariable_413 = "";

	private string logic_SubGraph_SaveLoadBool_uniqueID_413 = "Q5Wrong Answers";

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_416 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_416;

	private bool logic_uScriptCon_CompareBool_True_416 = true;

	private bool logic_uScriptCon_CompareBool_False_416 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_417 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_417;

	private bool logic_uScriptAct_SetBool_Out_417 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_417 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_417 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_419 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_419;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_419;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_419;

	private bool logic_uScript_AddMessage_Out_419 = true;

	private bool logic_uScript_AddMessage_Shown_419 = true;

	private uScript_Wait logic_uScript_Wait_uScript_Wait_424 = new uScript_Wait();

	private float logic_uScript_Wait_seconds_424;

	private bool logic_uScript_Wait_repeat_424 = true;

	private bool logic_uScript_Wait_Waited_424 = true;

	private uScript_GetCircuitChargeInfo logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_428 = new uScript_GetCircuitChargeInfo();

	private TankBlock logic_uScript_GetCircuitChargeInfo_block_428;

	private Tank logic_uScript_GetCircuitChargeInfo_tech_428;

	private BlockTypes logic_uScript_GetCircuitChargeInfo_blockType_428;

	private int logic_uScript_GetCircuitChargeInfo_Return_428;

	private bool logic_uScript_GetCircuitChargeInfo_Out_428 = true;

	private bool logic_uScript_GetCircuitChargeInfo_HasValidBlock_428 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_429 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_429;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_429;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_429;

	private bool logic_uScript_AddMessage_Out_429 = true;

	private bool logic_uScript_AddMessage_Shown_429 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_433 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_433;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_433;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_433;

	private bool logic_uScript_AddMessage_Out_433 = true;

	private bool logic_uScript_AddMessage_Shown_433 = true;

	private uScript_Wait logic_uScript_Wait_uScript_Wait_434 = new uScript_Wait();

	private float logic_uScript_Wait_seconds_434;

	private bool logic_uScript_Wait_repeat_434 = true;

	private bool logic_uScript_Wait_Waited_434 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_436 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_436;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_436;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_436;

	private bool logic_uScript_AddMessage_Out_436 = true;

	private bool logic_uScript_AddMessage_Shown_436 = true;

	private uScriptCon_ManualSwitch logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_437 = new uScriptCon_ManualSwitch();

	private int logic_uScriptCon_ManualSwitch_CurrentOutput_437;

	private uScriptCon_CompareInt logic_uScriptCon_CompareInt_uScriptCon_CompareInt_438 = new uScriptCon_CompareInt();

	private int logic_uScriptCon_CompareInt_A_438;

	private int logic_uScriptCon_CompareInt_B_438;

	private bool logic_uScriptCon_CompareInt_GreaterThan_438 = true;

	private bool logic_uScriptCon_CompareInt_GreaterThanOrEqualTo_438 = true;

	private bool logic_uScriptCon_CompareInt_EqualTo_438 = true;

	private bool logic_uScriptCon_CompareInt_NotEqualTo_438 = true;

	private bool logic_uScriptCon_CompareInt_LessThanOrEqualTo_438 = true;

	private bool logic_uScriptCon_CompareInt_LessThan_438 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_440 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_440;

	private bool logic_uScriptCon_CompareBool_True_440 = true;

	private bool logic_uScriptCon_CompareBool_False_440 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_442 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_442;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_442;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_442;

	private bool logic_uScript_AddMessage_Out_442 = true;

	private bool logic_uScript_AddMessage_Shown_442 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_447 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_447 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_447;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_447;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_447 = true;

	private bool logic_uScript_SpawnTechsFromData_Out_447 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_448 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_448;

	private bool logic_uScriptAct_SetBool_Out_448 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_448 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_448 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_449 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_449 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_449;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_449 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_449;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_449 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_449 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_449 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_449 = true;

	private uScriptAct_AddInt_v2 logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_457 = new uScriptAct_AddInt_v2();

	private int logic_uScriptAct_AddInt_v2_A_457;

	private int logic_uScriptAct_AddInt_v2_B_457 = 1;

	private int logic_uScriptAct_AddInt_v2_IntResult_457;

	private float logic_uScriptAct_AddInt_v2_FloatResult_457;

	private bool logic_uScriptAct_AddInt_v2_Out_457 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_462 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_462;

	private bool logic_uScriptAct_SetBool_Out_462 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_462 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_462 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_463 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_463;

	private bool logic_uScriptCon_CompareBool_True_463 = true;

	private bool logic_uScriptCon_CompareBool_False_463 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_464 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_464 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_464;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_464;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_464 = true;

	private bool logic_uScript_SpawnTechsFromData_Out_464 = true;

	private uScriptAct_AddInt_v2 logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_465 = new uScriptAct_AddInt_v2();

	private int logic_uScriptAct_AddInt_v2_A_465;

	private int logic_uScriptAct_AddInt_v2_B_465 = 1;

	private int logic_uScriptAct_AddInt_v2_IntResult_465;

	private float logic_uScriptAct_AddInt_v2_FloatResult_465;

	private bool logic_uScriptAct_AddInt_v2_Out_465 = true;

	private uScriptCon_CompareInt logic_uScriptCon_CompareInt_uScriptCon_CompareInt_467 = new uScriptCon_CompareInt();

	private int logic_uScriptCon_CompareInt_A_467;

	private int logic_uScriptCon_CompareInt_B_467;

	private bool logic_uScriptCon_CompareInt_GreaterThan_467 = true;

	private bool logic_uScriptCon_CompareInt_GreaterThanOrEqualTo_467 = true;

	private bool logic_uScriptCon_CompareInt_EqualTo_467 = true;

	private bool logic_uScriptCon_CompareInt_NotEqualTo_467 = true;

	private bool logic_uScriptCon_CompareInt_LessThanOrEqualTo_467 = true;

	private bool logic_uScriptCon_CompareInt_LessThan_467 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_468 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_468;

	private bool logic_uScriptAct_SetBool_Out_468 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_468 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_468 = true;

	private uScript_Wait logic_uScript_Wait_uScript_Wait_471 = new uScript_Wait();

	private float logic_uScript_Wait_seconds_471;

	private bool logic_uScript_Wait_repeat_471 = true;

	private bool logic_uScript_Wait_Waited_471 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_472 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_472;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_472;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_472;

	private bool logic_uScript_AddMessage_Out_472 = true;

	private bool logic_uScript_AddMessage_Shown_472 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_476 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_476 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_476;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_476 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_476;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_476 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_476 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_476 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_476 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_478 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_478;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_478;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_478;

	private bool logic_uScript_AddMessage_Out_478 = true;

	private bool logic_uScript_AddMessage_Shown_478 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_481 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_481;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_481;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_481;

	private bool logic_uScript_AddMessage_Out_481 = true;

	private bool logic_uScript_AddMessage_Shown_481 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_482 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_482;

	private bool logic_uScriptCon_CompareBool_True_482 = true;

	private bool logic_uScriptCon_CompareBool_False_482 = true;

	private uScript_Wait logic_uScript_Wait_uScript_Wait_484 = new uScript_Wait();

	private float logic_uScript_Wait_seconds_484;

	private bool logic_uScript_Wait_repeat_484 = true;

	private bool logic_uScript_Wait_Waited_484 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_492 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_492;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_492;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_492;

	private bool logic_uScript_AddMessage_Out_492 = true;

	private bool logic_uScript_AddMessage_Shown_492 = true;

	private uScriptCon_ManualSwitch logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_494 = new uScriptCon_ManualSwitch();

	private int logic_uScriptCon_ManualSwitch_CurrentOutput_494;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_496 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_496;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_496;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_496;

	private bool logic_uScript_AddMessage_Out_496 = true;

	private bool logic_uScript_AddMessage_Shown_496 = true;

	private uScript_GetCircuitChargeInfo logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_497 = new uScript_GetCircuitChargeInfo();

	private TankBlock logic_uScript_GetCircuitChargeInfo_block_497;

	private Tank logic_uScript_GetCircuitChargeInfo_tech_497;

	private BlockTypes logic_uScript_GetCircuitChargeInfo_blockType_497;

	private int logic_uScript_GetCircuitChargeInfo_Return_497;

	private bool logic_uScript_GetCircuitChargeInfo_Out_497 = true;

	private bool logic_uScript_GetCircuitChargeInfo_HasValidBlock_497 = true;

	private uScriptCon_CompareInt logic_uScriptCon_CompareInt_uScriptCon_CompareInt_504 = new uScriptCon_CompareInt();

	private int logic_uScriptCon_CompareInt_A_504;

	private int logic_uScriptCon_CompareInt_B_504;

	private bool logic_uScriptCon_CompareInt_GreaterThan_504 = true;

	private bool logic_uScriptCon_CompareInt_GreaterThanOrEqualTo_504 = true;

	private bool logic_uScriptCon_CompareInt_EqualTo_504 = true;

	private bool logic_uScriptCon_CompareInt_NotEqualTo_504 = true;

	private bool logic_uScriptCon_CompareInt_LessThanOrEqualTo_504 = true;

	private bool logic_uScriptCon_CompareInt_LessThan_504 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_505 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_505;

	private bool logic_uScriptCon_CompareBool_True_505 = true;

	private bool logic_uScriptCon_CompareBool_False_505 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_511 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_511;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_511;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_511;

	private bool logic_uScript_AddMessage_Out_511 = true;

	private bool logic_uScript_AddMessage_Shown_511 = true;

	private uScript_GetCircuitChargeInfo logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_512 = new uScript_GetCircuitChargeInfo();

	private TankBlock logic_uScript_GetCircuitChargeInfo_block_512;

	private Tank logic_uScript_GetCircuitChargeInfo_tech_512;

	private BlockTypes logic_uScript_GetCircuitChargeInfo_blockType_512;

	private int logic_uScript_GetCircuitChargeInfo_Return_512;

	private bool logic_uScript_GetCircuitChargeInfo_Out_512 = true;

	private bool logic_uScript_GetCircuitChargeInfo_HasValidBlock_512 = true;

	private uScript_Wait logic_uScript_Wait_uScript_Wait_517 = new uScript_Wait();

	private float logic_uScript_Wait_seconds_517;

	private bool logic_uScript_Wait_repeat_517 = true;

	private bool logic_uScript_Wait_Waited_517 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_518 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_518;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_518;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_518;

	private bool logic_uScript_AddMessage_Out_518 = true;

	private bool logic_uScript_AddMessage_Shown_518 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_521 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_521;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_521;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_521;

	private bool logic_uScript_AddMessage_Out_521 = true;

	private bool logic_uScript_AddMessage_Shown_521 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_522 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_522;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_522;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_522;

	private bool logic_uScript_AddMessage_Out_522 = true;

	private bool logic_uScript_AddMessage_Shown_522 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_523 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_523;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_523;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_523;

	private bool logic_uScript_AddMessage_Out_523 = true;

	private bool logic_uScript_AddMessage_Shown_523 = true;

	private uScript_GetAndCheckTechs logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_526 = new uScript_GetAndCheckTechs();

	private SpawnTechData[] logic_uScript_GetAndCheckTechs_techData_526 = new SpawnTechData[0];

	private GameObject logic_uScript_GetAndCheckTechs_ownerNode_526;

	private Tank[] logic_uScript_GetAndCheckTechs_techs_526 = new Tank[0];

	private int logic_uScript_GetAndCheckTechs_Return_526;

	private bool logic_uScript_GetAndCheckTechs_AllAlive_526 = true;

	private bool logic_uScript_GetAndCheckTechs_SomeAlive_526 = true;

	private bool logic_uScript_GetAndCheckTechs_AllDead_526 = true;

	private bool logic_uScript_GetAndCheckTechs_WaitingToSpawn_526 = true;

	private uScriptCon_ManualSwitch logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_528 = new uScriptCon_ManualSwitch();

	private int logic_uScriptCon_ManualSwitch_CurrentOutput_528;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_529 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_529;

	private bool logic_uScriptAct_SetBool_Out_529 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_529 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_529 = true;

	private uScript_Wait logic_uScript_Wait_uScript_Wait_530 = new uScript_Wait();

	private float logic_uScript_Wait_seconds_530;

	private bool logic_uScript_Wait_repeat_530 = true;

	private bool logic_uScript_Wait_Waited_530 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_532 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_532;

	private bool logic_uScriptCon_CompareBool_True_532 = true;

	private bool logic_uScriptCon_CompareBool_False_532 = true;

	private uScript_SpawnTechsFromData logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_534 = new uScript_SpawnTechsFromData();

	private SpawnTechData[] logic_uScript_SpawnTechsFromData_spawnData_534 = new SpawnTechData[0];

	private GameObject logic_uScript_SpawnTechsFromData_ownerNode_534;

	private float logic_uScript_SpawnTechsFromData_delayBetweenSpawns_534;

	private bool logic_uScript_SpawnTechsFromData_allowResurrection_534 = true;

	private bool logic_uScript_SpawnTechsFromData_Out_534 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_535 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_535;

	private bool logic_uScriptAct_SetBool_Out_535 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_535 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_535 = true;

	private uScriptCon_ManualSwitch logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_543 = new uScriptCon_ManualSwitch();

	private int logic_uScriptCon_ManualSwitch_CurrentOutput_543;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_544 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_544;

	private bool logic_uScriptAct_SetBool_Out_544 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_544 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_544 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_547 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_547;

	private bool logic_uScriptAct_SetBool_Out_547 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_547 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_547 = true;

	private uScriptAct_AddInt_v2 logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_549 = new uScriptAct_AddInt_v2();

	private int logic_uScriptAct_AddInt_v2_A_549;

	private int logic_uScriptAct_AddInt_v2_B_549 = 1;

	private int logic_uScriptAct_AddInt_v2_IntResult_549;

	private float logic_uScriptAct_AddInt_v2_FloatResult_549;

	private bool logic_uScriptAct_AddInt_v2_Out_549 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_551 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_551;

	private bool logic_uScriptAct_SetBool_Out_551 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_551 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_551 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_552 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_552;

	private bool logic_uScriptAct_SetBool_Out_552 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_552 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_552 = true;

	private uScriptAct_AddInt_v2 logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_555 = new uScriptAct_AddInt_v2();

	private int logic_uScriptAct_AddInt_v2_A_555;

	private int logic_uScriptAct_AddInt_v2_B_555 = 1;

	private int logic_uScriptAct_AddInt_v2_IntResult_555;

	private float logic_uScriptAct_AddInt_v2_FloatResult_555;

	private bool logic_uScriptAct_AddInt_v2_Out_555 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_557 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_557 = 2;

	private int logic_uScriptAct_SetInt_Target_557;

	private bool logic_uScriptAct_SetInt_Out_557 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_559 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_559 = 1;

	private int logic_uScriptAct_SetInt_Target_559;

	private bool logic_uScriptAct_SetInt_Out_559 = true;

	private uScript_Wait logic_uScript_Wait_uScript_Wait_561 = new uScript_Wait();

	private float logic_uScript_Wait_seconds_561;

	private bool logic_uScript_Wait_repeat_561 = true;

	private bool logic_uScript_Wait_Waited_561 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_564 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_564;

	private bool logic_uScriptCon_CompareBool_True_564 = true;

	private bool logic_uScriptCon_CompareBool_False_564 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_566 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_566 = 3;

	private int logic_uScriptAct_SetInt_Target_566;

	private bool logic_uScriptAct_SetInt_Out_566 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_569 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_569;

	private bool logic_uScriptAct_SetBool_Out_569 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_569 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_569 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_570 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_570;

	private bool logic_uScriptAct_SetBool_Out_570 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_570 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_570 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_571 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_571;

	private bool logic_uScriptAct_SetBool_Out_571 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_571 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_571 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_572 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_572;

	private bool logic_uScriptAct_SetBool_Out_572 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_572 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_572 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_573 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_573 = 4;

	private int logic_uScriptAct_SetInt_Target_573;

	private bool logic_uScriptAct_SetInt_Out_573 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_577 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_577;

	private bool logic_uScriptAct_SetBool_Out_577 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_577 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_577 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_578 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_578;

	private bool logic_uScriptCon_CompareBool_True_578 = true;

	private bool logic_uScriptCon_CompareBool_False_578 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_586 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_586;

	private bool logic_uScriptAct_SetBool_Out_586 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_586 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_586 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_587 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_587;

	private bool logic_uScriptAct_SetBool_Out_587 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_587 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_587 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_588 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_588;

	private bool logic_uScriptAct_SetBool_Out_588 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_588 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_588 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_589 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_589 = 4;

	private int logic_uScriptAct_SetInt_Target_589;

	private bool logic_uScriptAct_SetInt_Out_589 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_590 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_590 = 3;

	private int logic_uScriptAct_SetInt_Target_590;

	private bool logic_uScriptAct_SetInt_Out_590 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_591 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_591;

	private bool logic_uScriptAct_SetBool_Out_591 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_591 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_591 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_592 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_592;

	private bool logic_uScriptCon_CompareBool_True_592 = true;

	private bool logic_uScriptCon_CompareBool_False_592 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_600 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_600;

	private bool logic_uScriptAct_SetBool_Out_600 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_600 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_600 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_601 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_601;

	private bool logic_uScriptAct_SetBool_Out_601 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_601 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_601 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_602 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_602;

	private bool logic_uScriptAct_SetBool_Out_602 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_602 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_602 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_603 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_603 = 4;

	private int logic_uScriptAct_SetInt_Target_603;

	private bool logic_uScriptAct_SetInt_Out_603 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_604 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_604 = 3;

	private int logic_uScriptAct_SetInt_Target_604;

	private bool logic_uScriptAct_SetInt_Out_604 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_606 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_606;

	private bool logic_uScriptAct_SetBool_Out_606 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_606 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_606 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_609 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_609;

	private bool logic_uScriptAct_SetBool_Out_609 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_609 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_609 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_610 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_610;

	private bool logic_uScriptCon_CompareBool_True_610 = true;

	private bool logic_uScriptCon_CompareBool_False_610 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_613 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_613;

	private bool logic_uScriptAct_SetBool_Out_613 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_613 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_613 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_615 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_615 = 4;

	private int logic_uScriptAct_SetInt_Target_615;

	private bool logic_uScriptAct_SetInt_Out_615 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_617 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_617 = 3;

	private int logic_uScriptAct_SetInt_Target_617;

	private bool logic_uScriptAct_SetInt_Out_617 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_618 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_618;

	private bool logic_uScriptAct_SetBool_Out_618 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_618 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_618 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_622 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_622 = 1;

	private int logic_uScriptAct_SetInt_Target_622;

	private bool logic_uScriptAct_SetInt_Out_622 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_623 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_623 = 3;

	private int logic_uScriptAct_SetInt_Target_623;

	private bool logic_uScriptAct_SetInt_Out_623 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_625 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_625;

	private bool logic_uScriptAct_SetBool_Out_625 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_625 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_625 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_626 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_626;

	private bool logic_uScriptCon_CompareBool_True_626 = true;

	private bool logic_uScriptCon_CompareBool_False_626 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_632 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_632;

	private bool logic_uScriptAct_SetBool_Out_632 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_632 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_632 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_634 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_634;

	private bool logic_uScriptAct_SetBool_Out_634 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_634 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_634 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_637 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_637;

	private bool logic_uScriptAct_SetBool_Out_637 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_637 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_637 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_638 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_638;

	private bool logic_uScriptAct_SetBool_Out_638 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_638 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_638 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_639 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_639 = 4;

	private int logic_uScriptAct_SetInt_Target_639;

	private bool logic_uScriptAct_SetInt_Out_639 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_641 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_641;

	private bool logic_uScriptAct_SetBool_Out_641 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_641 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_641 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_642 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_642 = 3;

	private int logic_uScriptAct_SetInt_Target_642;

	private bool logic_uScriptAct_SetInt_Out_642 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_646 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_646 = 4;

	private int logic_uScriptAct_SetInt_Target_646;

	private bool logic_uScriptAct_SetInt_Out_646 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_648 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_648;

	private bool logic_uScriptCon_CompareBool_True_648 = true;

	private bool logic_uScriptCon_CompareBool_False_648 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_649 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_649;

	private bool logic_uScriptAct_SetBool_Out_649 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_649 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_649 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_652 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_652;

	private bool logic_uScriptCon_CompareBool_True_652 = true;

	private bool logic_uScriptCon_CompareBool_False_652 = true;

	private uScriptCon_CompareInt logic_uScriptCon_CompareInt_uScriptCon_CompareInt_654 = new uScriptCon_CompareInt();

	private int logic_uScriptCon_CompareInt_A_654;

	private int logic_uScriptCon_CompareInt_B_654;

	private bool logic_uScriptCon_CompareInt_GreaterThan_654 = true;

	private bool logic_uScriptCon_CompareInt_GreaterThanOrEqualTo_654 = true;

	private bool logic_uScriptCon_CompareInt_EqualTo_654 = true;

	private bool logic_uScriptCon_CompareInt_NotEqualTo_654 = true;

	private bool logic_uScriptCon_CompareInt_LessThanOrEqualTo_654 = true;

	private bool logic_uScriptCon_CompareInt_LessThan_654 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_656 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_656;

	private bool logic_uScriptAct_SetBool_Out_656 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_656 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_656 = true;

	private uScript_GetCircuitChargeInfo logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_658 = new uScript_GetCircuitChargeInfo();

	private TankBlock logic_uScript_GetCircuitChargeInfo_block_658;

	private Tank logic_uScript_GetCircuitChargeInfo_tech_658;

	private BlockTypes logic_uScript_GetCircuitChargeInfo_blockType_658;

	private int logic_uScript_GetCircuitChargeInfo_Return_658;

	private bool logic_uScript_GetCircuitChargeInfo_Out_658 = true;

	private bool logic_uScript_GetCircuitChargeInfo_HasValidBlock_658 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_661 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_661;

	private bool logic_uScriptAct_SetBool_Out_661 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_661 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_661 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_663 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_663;

	private bool logic_uScriptAct_SetBool_Out_663 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_663 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_663 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_664 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_664;

	private bool logic_uScriptAct_SetBool_Out_664 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_664 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_664 = true;

	private uScript_GetCircuitChargeInfo logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_665 = new uScript_GetCircuitChargeInfo();

	private TankBlock logic_uScript_GetCircuitChargeInfo_block_665;

	private Tank logic_uScript_GetCircuitChargeInfo_tech_665;

	private BlockTypes logic_uScript_GetCircuitChargeInfo_blockType_665;

	private int logic_uScript_GetCircuitChargeInfo_Return_665;

	private bool logic_uScript_GetCircuitChargeInfo_Out_665 = true;

	private bool logic_uScript_GetCircuitChargeInfo_HasValidBlock_665 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_667 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_667;

	private bool logic_uScriptAct_SetBool_Out_667 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_667 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_667 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_668 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_668;

	private bool logic_uScriptAct_SetBool_Out_668 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_668 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_668 = true;

	private uScript_GetCircuitChargeInfo logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_669 = new uScript_GetCircuitChargeInfo();

	private TankBlock logic_uScript_GetCircuitChargeInfo_block_669;

	private Tank logic_uScript_GetCircuitChargeInfo_tech_669;

	private BlockTypes logic_uScript_GetCircuitChargeInfo_blockType_669;

	private int logic_uScript_GetCircuitChargeInfo_Return_669;

	private bool logic_uScript_GetCircuitChargeInfo_Out_669 = true;

	private bool logic_uScript_GetCircuitChargeInfo_HasValidBlock_669 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_674 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_674;

	private bool logic_uScriptAct_SetBool_Out_674 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_674 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_674 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_675 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_675;

	private bool logic_uScriptAct_SetBool_Out_675 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_675 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_675 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_676 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_676 = 4;

	private int logic_uScriptAct_SetInt_Target_676;

	private bool logic_uScriptAct_SetInt_Out_676 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_678 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_678 = 4;

	private int logic_uScriptAct_SetInt_Target_678;

	private bool logic_uScriptAct_SetInt_Out_678 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_679 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_679;

	private bool logic_uScriptCon_CompareBool_True_679 = true;

	private bool logic_uScriptCon_CompareBool_False_679 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_680 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_680 = 3;

	private int logic_uScriptAct_SetInt_Target_680;

	private bool logic_uScriptAct_SetInt_Out_680 = true;

	private uScript_GetCircuitChargeInfo logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_681 = new uScript_GetCircuitChargeInfo();

	private TankBlock logic_uScript_GetCircuitChargeInfo_block_681;

	private Tank logic_uScript_GetCircuitChargeInfo_tech_681;

	private BlockTypes logic_uScript_GetCircuitChargeInfo_blockType_681;

	private int logic_uScript_GetCircuitChargeInfo_Return_681;

	private bool logic_uScript_GetCircuitChargeInfo_Out_681 = true;

	private bool logic_uScript_GetCircuitChargeInfo_HasValidBlock_681 = true;

	private uScriptCon_CompareInt logic_uScriptCon_CompareInt_uScriptCon_CompareInt_682 = new uScriptCon_CompareInt();

	private int logic_uScriptCon_CompareInt_A_682;

	private int logic_uScriptCon_CompareInt_B_682;

	private bool logic_uScriptCon_CompareInt_GreaterThan_682 = true;

	private bool logic_uScriptCon_CompareInt_GreaterThanOrEqualTo_682 = true;

	private bool logic_uScriptCon_CompareInt_EqualTo_682 = true;

	private bool logic_uScriptCon_CompareInt_NotEqualTo_682 = true;

	private bool logic_uScriptCon_CompareInt_LessThanOrEqualTo_682 = true;

	private bool logic_uScriptCon_CompareInt_LessThan_682 = true;

	private uScriptCon_CompareInt logic_uScriptCon_CompareInt_uScriptCon_CompareInt_683 = new uScriptCon_CompareInt();

	private int logic_uScriptCon_CompareInt_A_683;

	private int logic_uScriptCon_CompareInt_B_683;

	private bool logic_uScriptCon_CompareInt_GreaterThan_683 = true;

	private bool logic_uScriptCon_CompareInt_GreaterThanOrEqualTo_683 = true;

	private bool logic_uScriptCon_CompareInt_EqualTo_683 = true;

	private bool logic_uScriptCon_CompareInt_NotEqualTo_683 = true;

	private bool logic_uScriptCon_CompareInt_LessThanOrEqualTo_683 = true;

	private bool logic_uScriptCon_CompareInt_LessThan_683 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_684 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_684 = 3;

	private int logic_uScriptAct_SetInt_Target_684;

	private bool logic_uScriptAct_SetInt_Out_684 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_686 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_686 = 3;

	private int logic_uScriptAct_SetInt_Target_686;

	private bool logic_uScriptAct_SetInt_Out_686 = true;

	private uScriptCon_CompareInt logic_uScriptCon_CompareInt_uScriptCon_CompareInt_688 = new uScriptCon_CompareInt();

	private int logic_uScriptCon_CompareInt_A_688;

	private int logic_uScriptCon_CompareInt_B_688;

	private bool logic_uScriptCon_CompareInt_GreaterThan_688 = true;

	private bool logic_uScriptCon_CompareInt_GreaterThanOrEqualTo_688 = true;

	private bool logic_uScriptCon_CompareInt_EqualTo_688 = true;

	private bool logic_uScriptCon_CompareInt_NotEqualTo_688 = true;

	private bool logic_uScriptCon_CompareInt_LessThanOrEqualTo_688 = true;

	private bool logic_uScriptCon_CompareInt_LessThan_688 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_689 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_689;

	private bool logic_uScriptAct_SetBool_Out_689 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_689 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_689 = true;

	private uScript_AddMessage logic_uScript_AddMessage_uScript_AddMessage_698 = new uScript_AddMessage();

	private uScript_AddMessage.MessageData logic_uScript_AddMessage_messageData_698;

	private uScript_AddMessage.MessageSpeaker logic_uScript_AddMessage_speaker_698;

	private ManOnScreenMessages.OnScreenMessage logic_uScript_AddMessage_Return_698;

	private bool logic_uScript_AddMessage_Out_698 = true;

	private bool logic_uScript_AddMessage_Shown_698 = true;

	private uScript_Wait logic_uScript_Wait_uScript_Wait_699 = new uScript_Wait();

	private float logic_uScript_Wait_seconds_699;

	private bool logic_uScript_Wait_repeat_699 = true;

	private bool logic_uScript_Wait_Waited_699 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_701 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_701 = 1;

	private int logic_uScriptAct_SetInt_Target_701;

	private bool logic_uScriptAct_SetInt_Out_701 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_703 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_703 = 1;

	private int logic_uScriptAct_SetInt_Target_703;

	private bool logic_uScriptAct_SetInt_Out_703 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_706 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_706 = 1;

	private int logic_uScriptAct_SetInt_Target_706;

	private bool logic_uScriptAct_SetInt_Out_706 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_708 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_708 = 1;

	private int logic_uScriptAct_SetInt_Target_708;

	private bool logic_uScriptAct_SetInt_Out_708 = true;

	private uScriptAct_AddInt_v2 logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_709 = new uScriptAct_AddInt_v2();

	private int logic_uScriptAct_AddInt_v2_A_709;

	private int logic_uScriptAct_AddInt_v2_B_709 = 1;

	private int logic_uScriptAct_AddInt_v2_IntResult_709;

	private float logic_uScriptAct_AddInt_v2_FloatResult_709;

	private bool logic_uScriptAct_AddInt_v2_Out_709 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_711 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_711;

	private bool logic_uScriptAct_SetBool_Out_711 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_711 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_711 = true;

	private uScriptAct_AddInt_v2 logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_713 = new uScriptAct_AddInt_v2();

	private int logic_uScriptAct_AddInt_v2_A_713;

	private int logic_uScriptAct_AddInt_v2_B_713 = 1;

	private int logic_uScriptAct_AddInt_v2_IntResult_713;

	private float logic_uScriptAct_AddInt_v2_FloatResult_713;

	private bool logic_uScriptAct_AddInt_v2_Out_713 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_714 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_714;

	private bool logic_uScriptAct_SetBool_Out_714 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_714 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_714 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_716 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_716;

	private bool logic_uScriptAct_SetBool_Out_716 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_716 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_716 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_720 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_720;

	private bool logic_uScriptCon_CompareBool_True_720 = true;

	private bool logic_uScriptCon_CompareBool_False_720 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_721 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_721;

	private bool logic_uScriptAct_SetBool_Out_721 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_721 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_721 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_723 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_723;

	private bool logic_uScriptAct_SetBool_Out_723 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_723 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_723 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_726 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_726;

	private bool logic_uScriptAct_SetBool_Out_726 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_726 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_726 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_727 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_727;

	private bool logic_uScriptAct_SetBool_Out_727 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_727 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_727 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_728 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_728 = 4;

	private int logic_uScriptAct_SetInt_Target_728;

	private bool logic_uScriptAct_SetInt_Out_728 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_732 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_732 = 3;

	private int logic_uScriptAct_SetInt_Target_732;

	private bool logic_uScriptAct_SetInt_Out_732 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_733 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_733 = 1;

	private int logic_uScriptAct_SetInt_Target_733;

	private bool logic_uScriptAct_SetInt_Out_733 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_734 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_734 = 4;

	private int logic_uScriptAct_SetInt_Target_734;

	private bool logic_uScriptAct_SetInt_Out_734 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_739 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_739 = 4;

	private int logic_uScriptAct_SetInt_Target_739;

	private bool logic_uScriptAct_SetInt_Out_739 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_740 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_740 = 3;

	private int logic_uScriptAct_SetInt_Target_740;

	private bool logic_uScriptAct_SetInt_Out_740 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_742 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_742;

	private bool logic_uScriptAct_SetBool_Out_742 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_742 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_742 = true;

	private uScriptCon_CompareInt logic_uScriptCon_CompareInt_uScriptCon_CompareInt_744 = new uScriptCon_CompareInt();

	private int logic_uScriptCon_CompareInt_A_744;

	private int logic_uScriptCon_CompareInt_B_744;

	private bool logic_uScriptCon_CompareInt_GreaterThan_744 = true;

	private bool logic_uScriptCon_CompareInt_GreaterThanOrEqualTo_744 = true;

	private bool logic_uScriptCon_CompareInt_EqualTo_744 = true;

	private bool logic_uScriptCon_CompareInt_NotEqualTo_744 = true;

	private bool logic_uScriptCon_CompareInt_LessThanOrEqualTo_744 = true;

	private bool logic_uScriptCon_CompareInt_LessThan_744 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_747 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_747;

	private bool logic_uScriptAct_SetBool_Out_747 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_747 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_747 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_748 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_748;

	private bool logic_uScriptAct_SetBool_Out_748 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_748 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_748 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_750 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_750;

	private bool logic_uScriptAct_SetBool_Out_750 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_750 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_750 = true;

	private uScript_GetCircuitChargeInfo logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_751 = new uScript_GetCircuitChargeInfo();

	private TankBlock logic_uScript_GetCircuitChargeInfo_block_751;

	private Tank logic_uScript_GetCircuitChargeInfo_tech_751;

	private BlockTypes logic_uScript_GetCircuitChargeInfo_blockType_751;

	private int logic_uScript_GetCircuitChargeInfo_Return_751;

	private bool logic_uScript_GetCircuitChargeInfo_Out_751 = true;

	private bool logic_uScript_GetCircuitChargeInfo_HasValidBlock_751 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_753 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_753;

	private bool logic_uScriptCon_CompareBool_True_753 = true;

	private bool logic_uScriptCon_CompareBool_False_753 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_758 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_758;

	private bool logic_uScriptAct_SetBool_Out_758 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_758 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_758 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_759 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_759;

	private bool logic_uScriptAct_SetBool_Out_759 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_759 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_759 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_760 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_760;

	private bool logic_uScriptAct_SetBool_Out_760 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_760 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_760 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_761 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_761 = 4;

	private int logic_uScriptAct_SetInt_Target_761;

	private bool logic_uScriptAct_SetInt_Out_761 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_762 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_762;

	private bool logic_uScriptCon_CompareBool_True_762 = true;

	private bool logic_uScriptCon_CompareBool_False_762 = true;

	private uScript_GetCircuitChargeInfo logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_763 = new uScript_GetCircuitChargeInfo();

	private TankBlock logic_uScript_GetCircuitChargeInfo_block_763;

	private Tank logic_uScript_GetCircuitChargeInfo_tech_763;

	private BlockTypes logic_uScript_GetCircuitChargeInfo_blockType_763;

	private int logic_uScript_GetCircuitChargeInfo_Return_763;

	private bool logic_uScript_GetCircuitChargeInfo_Out_763 = true;

	private bool logic_uScript_GetCircuitChargeInfo_HasValidBlock_763 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_764 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_764 = 3;

	private int logic_uScriptAct_SetInt_Target_764;

	private bool logic_uScriptAct_SetInt_Out_764 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_767 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_767;

	private bool logic_uScriptAct_SetBool_Out_767 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_767 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_767 = true;

	private uScriptCon_CompareInt logic_uScriptCon_CompareInt_uScriptCon_CompareInt_769 = new uScriptCon_CompareInt();

	private int logic_uScriptCon_CompareInt_A_769;

	private int logic_uScriptCon_CompareInt_B_769;

	private bool logic_uScriptCon_CompareInt_GreaterThan_769 = true;

	private bool logic_uScriptCon_CompareInt_GreaterThanOrEqualTo_769 = true;

	private bool logic_uScriptCon_CompareInt_EqualTo_769 = true;

	private bool logic_uScriptCon_CompareInt_NotEqualTo_769 = true;

	private bool logic_uScriptCon_CompareInt_LessThanOrEqualTo_769 = true;

	private bool logic_uScriptCon_CompareInt_LessThan_769 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_771 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_771 = 4;

	private int logic_uScriptAct_SetInt_Target_771;

	private bool logic_uScriptAct_SetInt_Out_771 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_772 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_772;

	private bool logic_uScriptAct_SetBool_Out_772 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_772 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_772 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_776 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_776;

	private bool logic_uScriptAct_SetBool_Out_776 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_776 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_776 = true;

	private uScriptCon_CompareInt logic_uScriptCon_CompareInt_uScriptCon_CompareInt_777 = new uScriptCon_CompareInt();

	private int logic_uScriptCon_CompareInt_A_777;

	private int logic_uScriptCon_CompareInt_B_777;

	private bool logic_uScriptCon_CompareInt_GreaterThan_777 = true;

	private bool logic_uScriptCon_CompareInt_GreaterThanOrEqualTo_777 = true;

	private bool logic_uScriptCon_CompareInt_EqualTo_777 = true;

	private bool logic_uScriptCon_CompareInt_NotEqualTo_777 = true;

	private bool logic_uScriptCon_CompareInt_LessThanOrEqualTo_777 = true;

	private bool logic_uScriptCon_CompareInt_LessThan_777 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_778 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_778;

	private bool logic_uScriptAct_SetBool_Out_778 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_778 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_778 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_781 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_781;

	private bool logic_uScriptAct_SetBool_Out_781 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_781 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_781 = true;

	private uScript_GetCircuitChargeInfo logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_784 = new uScript_GetCircuitChargeInfo();

	private TankBlock logic_uScript_GetCircuitChargeInfo_block_784;

	private Tank logic_uScript_GetCircuitChargeInfo_tech_784;

	private BlockTypes logic_uScript_GetCircuitChargeInfo_blockType_784;

	private int logic_uScript_GetCircuitChargeInfo_Return_784;

	private bool logic_uScript_GetCircuitChargeInfo_Out_784 = true;

	private bool logic_uScript_GetCircuitChargeInfo_HasValidBlock_784 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_787 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_787;

	private bool logic_uScriptCon_CompareBool_True_787 = true;

	private bool logic_uScriptCon_CompareBool_False_787 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_789 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_789 = 3;

	private int logic_uScriptAct_SetInt_Target_789;

	private bool logic_uScriptAct_SetInt_Out_789 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_792 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_792 = 1;

	private int logic_uScriptAct_SetInt_Target_792;

	private bool logic_uScriptAct_SetInt_Out_792 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_794 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_794 = 1;

	private int logic_uScriptAct_SetInt_Target_794;

	private bool logic_uScriptAct_SetInt_Out_794 = true;

	private uScriptAct_AddInt_v2 logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_795 = new uScriptAct_AddInt_v2();

	private int logic_uScriptAct_AddInt_v2_A_795;

	private int logic_uScriptAct_AddInt_v2_B_795 = 1;

	private int logic_uScriptAct_AddInt_v2_IntResult_795;

	private float logic_uScriptAct_AddInt_v2_FloatResult_795;

	private bool logic_uScriptAct_AddInt_v2_Out_795 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_796 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_796;

	private bool logic_uScriptAct_SetBool_Out_796 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_796 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_796 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_800 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_800;

	private bool logic_uScriptAct_SetBool_Out_800 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_800 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_800 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_801 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_801;

	private bool logic_uScriptAct_SetBool_Out_801 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_801 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_801 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_805 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_805 = 1;

	private int logic_uScriptAct_SetInt_Target_805;

	private bool logic_uScriptAct_SetInt_Out_805 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_806 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_806 = 1;

	private int logic_uScriptAct_SetInt_Target_806;

	private bool logic_uScriptAct_SetInt_Out_806 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_807 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_807 = 5;

	private int logic_uScriptAct_SetInt_Target_807;

	private bool logic_uScriptAct_SetInt_Out_807 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_810 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_810 = 1;

	private int logic_uScriptAct_SetInt_Target_810;

	private bool logic_uScriptAct_SetInt_Out_810 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_813 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_813 = 4;

	private int logic_uScriptAct_SetInt_Target_813;

	private bool logic_uScriptAct_SetInt_Out_813 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_814 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_814 = 3;

	private int logic_uScriptAct_SetInt_Target_814;

	private bool logic_uScriptAct_SetInt_Out_814 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_816 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_816;

	private bool logic_uScriptAct_SetBool_Out_816 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_816 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_816 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_819 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_819;

	private bool logic_uScriptAct_SetBool_Out_819 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_819 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_819 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_820 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_820;

	private bool logic_uScriptAct_SetBool_Out_820 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_820 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_820 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_822 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_822;

	private bool logic_uScriptAct_SetBool_Out_822 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_822 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_822 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_824 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_824;

	private bool logic_uScriptCon_CompareBool_True_824 = true;

	private bool logic_uScriptCon_CompareBool_False_824 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_825 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_825;

	private bool logic_uScriptCon_CompareBool_True_825 = true;

	private bool logic_uScriptCon_CompareBool_False_825 = true;

	private uScriptCon_CompareInt logic_uScriptCon_CompareInt_uScriptCon_CompareInt_826 = new uScriptCon_CompareInt();

	private int logic_uScriptCon_CompareInt_A_826;

	private int logic_uScriptCon_CompareInt_B_826;

	private bool logic_uScriptCon_CompareInt_GreaterThan_826 = true;

	private bool logic_uScriptCon_CompareInt_GreaterThanOrEqualTo_826 = true;

	private bool logic_uScriptCon_CompareInt_EqualTo_826 = true;

	private bool logic_uScriptCon_CompareInt_NotEqualTo_826 = true;

	private bool logic_uScriptCon_CompareInt_LessThanOrEqualTo_826 = true;

	private bool logic_uScriptCon_CompareInt_LessThan_826 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_831 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_831 = 4;

	private int logic_uScriptAct_SetInt_Target_831;

	private bool logic_uScriptAct_SetInt_Out_831 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_832 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_832;

	private bool logic_uScriptAct_SetBool_Out_832 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_832 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_832 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_834 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_834;

	private bool logic_uScriptAct_SetBool_Out_834 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_834 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_834 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_836 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_836 = 3;

	private int logic_uScriptAct_SetInt_Target_836;

	private bool logic_uScriptAct_SetInt_Out_836 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_838 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_838;

	private bool logic_uScriptAct_SetBool_Out_838 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_838 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_838 = true;

	private uScript_GetCircuitChargeInfo logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_841 = new uScript_GetCircuitChargeInfo();

	private TankBlock logic_uScript_GetCircuitChargeInfo_block_841;

	private Tank logic_uScript_GetCircuitChargeInfo_tech_841;

	private BlockTypes logic_uScript_GetCircuitChargeInfo_blockType_841;

	private int logic_uScript_GetCircuitChargeInfo_Return_841;

	private bool logic_uScript_GetCircuitChargeInfo_Out_841 = true;

	private bool logic_uScript_GetCircuitChargeInfo_HasValidBlock_841 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_842 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_842;

	private bool logic_uScriptAct_SetBool_Out_842 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_842 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_842 = true;

	private uScript_GetCircuitChargeInfo logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_843 = new uScript_GetCircuitChargeInfo();

	private TankBlock logic_uScript_GetCircuitChargeInfo_block_843;

	private Tank logic_uScript_GetCircuitChargeInfo_tech_843;

	private BlockTypes logic_uScript_GetCircuitChargeInfo_blockType_843;

	private int logic_uScript_GetCircuitChargeInfo_Return_843;

	private bool logic_uScript_GetCircuitChargeInfo_Out_843 = true;

	private bool logic_uScript_GetCircuitChargeInfo_HasValidBlock_843 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_844 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_844 = 3;

	private int logic_uScriptAct_SetInt_Target_844;

	private bool logic_uScriptAct_SetInt_Out_844 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_850 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_850;

	private bool logic_uScriptAct_SetBool_Out_850 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_850 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_850 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_851 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_851;

	private bool logic_uScriptCon_CompareBool_True_851 = true;

	private bool logic_uScriptCon_CompareBool_False_851 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_852 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_852;

	private bool logic_uScriptAct_SetBool_Out_852 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_852 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_852 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_856 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_856;

	private bool logic_uScriptAct_SetBool_Out_856 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_856 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_856 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_857 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_857 = 4;

	private int logic_uScriptAct_SetInt_Target_857;

	private bool logic_uScriptAct_SetInt_Out_857 = true;

	private uScriptCon_CompareInt logic_uScriptCon_CompareInt_uScriptCon_CompareInt_858 = new uScriptCon_CompareInt();

	private int logic_uScriptCon_CompareInt_A_858;

	private int logic_uScriptCon_CompareInt_B_858;

	private bool logic_uScriptCon_CompareInt_GreaterThan_858 = true;

	private bool logic_uScriptCon_CompareInt_GreaterThanOrEqualTo_858 = true;

	private bool logic_uScriptCon_CompareInt_EqualTo_858 = true;

	private bool logic_uScriptCon_CompareInt_NotEqualTo_858 = true;

	private bool logic_uScriptCon_CompareInt_LessThanOrEqualTo_858 = true;

	private bool logic_uScriptCon_CompareInt_LessThan_858 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_859 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_859;

	private bool logic_uScriptAct_SetBool_Out_859 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_859 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_859 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_861 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_861 = 3;

	private int logic_uScriptAct_SetInt_Target_861;

	private bool logic_uScriptAct_SetInt_Out_861 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_862 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_862 = 4;

	private int logic_uScriptAct_SetInt_Target_862;

	private bool logic_uScriptAct_SetInt_Out_862 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_863 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_863;

	private bool logic_uScriptAct_SetBool_Out_863 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_863 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_863 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_867 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_867;

	private bool logic_uScriptAct_SetBool_Out_867 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_867 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_867 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_868 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_868;

	private bool logic_uScriptAct_SetBool_Out_868 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_868 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_868 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_871 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_871;

	private bool logic_uScriptCon_CompareBool_True_871 = true;

	private bool logic_uScriptCon_CompareBool_False_871 = true;

	private uScriptCon_CompareInt logic_uScriptCon_CompareInt_uScriptCon_CompareInt_874 = new uScriptCon_CompareInt();

	private int logic_uScriptCon_CompareInt_A_874;

	private int logic_uScriptCon_CompareInt_B_874;

	private bool logic_uScriptCon_CompareInt_GreaterThan_874 = true;

	private bool logic_uScriptCon_CompareInt_GreaterThanOrEqualTo_874 = true;

	private bool logic_uScriptCon_CompareInt_EqualTo_874 = true;

	private bool logic_uScriptCon_CompareInt_NotEqualTo_874 = true;

	private bool logic_uScriptCon_CompareInt_LessThanOrEqualTo_874 = true;

	private bool logic_uScriptCon_CompareInt_LessThan_874 = true;

	private uScript_GetCircuitChargeInfo logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_877 = new uScript_GetCircuitChargeInfo();

	private TankBlock logic_uScript_GetCircuitChargeInfo_block_877;

	private Tank logic_uScript_GetCircuitChargeInfo_tech_877;

	private BlockTypes logic_uScript_GetCircuitChargeInfo_blockType_877;

	private int logic_uScript_GetCircuitChargeInfo_Return_877;

	private bool logic_uScript_GetCircuitChargeInfo_Out_877 = true;

	private bool logic_uScript_GetCircuitChargeInfo_HasValidBlock_877 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_878 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_878;

	private bool logic_uScriptAct_SetBool_Out_878 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_878 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_878 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_879 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_879;

	private bool logic_uScriptCon_CompareBool_True_879 = true;

	private bool logic_uScriptCon_CompareBool_False_879 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_882 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_882 = 4;

	private int logic_uScriptAct_SetInt_Target_882;

	private bool logic_uScriptAct_SetInt_Out_882 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_883 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_883;

	private bool logic_uScriptAct_SetBool_Out_883 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_883 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_883 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_885 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_885;

	private bool logic_uScriptAct_SetBool_Out_885 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_885 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_885 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_887 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_887 = 3;

	private int logic_uScriptAct_SetInt_Target_887;

	private bool logic_uScriptAct_SetInt_Out_887 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_889 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_889;

	private bool logic_uScriptAct_SetBool_Out_889 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_889 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_889 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_892 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_892;

	private bool logic_uScriptAct_SetBool_Out_892 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_892 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_892 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_899 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_899;

	private bool logic_uScriptAct_SetBool_Out_899 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_899 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_899 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_900 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_900;

	private bool logic_uScriptAct_SetBool_Out_900 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_900 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_900 = true;

	private uScriptCon_CompareInt logic_uScriptCon_CompareInt_uScriptCon_CompareInt_902 = new uScriptCon_CompareInt();

	private int logic_uScriptCon_CompareInt_A_902;

	private int logic_uScriptCon_CompareInt_B_902;

	private bool logic_uScriptCon_CompareInt_GreaterThan_902 = true;

	private bool logic_uScriptCon_CompareInt_GreaterThanOrEqualTo_902 = true;

	private bool logic_uScriptCon_CompareInt_EqualTo_902 = true;

	private bool logic_uScriptCon_CompareInt_NotEqualTo_902 = true;

	private bool logic_uScriptCon_CompareInt_LessThanOrEqualTo_902 = true;

	private bool logic_uScriptCon_CompareInt_LessThan_902 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_903 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_903 = 4;

	private int logic_uScriptAct_SetInt_Target_903;

	private bool logic_uScriptAct_SetInt_Out_903 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_904 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_904 = 3;

	private int logic_uScriptAct_SetInt_Target_904;

	private bool logic_uScriptAct_SetInt_Out_904 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_905 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_905;

	private bool logic_uScriptCon_CompareBool_True_905 = true;

	private bool logic_uScriptCon_CompareBool_False_905 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_907 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_907;

	private bool logic_uScriptAct_SetBool_Out_907 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_907 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_907 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_909 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_909;

	private bool logic_uScriptAct_SetBool_Out_909 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_909 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_909 = true;

	private uScript_GetCircuitChargeInfo logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_910 = new uScript_GetCircuitChargeInfo();

	private TankBlock logic_uScript_GetCircuitChargeInfo_block_910;

	private Tank logic_uScript_GetCircuitChargeInfo_tech_910;

	private BlockTypes logic_uScript_GetCircuitChargeInfo_blockType_910;

	private int logic_uScript_GetCircuitChargeInfo_Return_910;

	private bool logic_uScript_GetCircuitChargeInfo_Out_910 = true;

	private bool logic_uScript_GetCircuitChargeInfo_HasValidBlock_910 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_915 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_915;

	private bool logic_uScriptCon_CompareBool_True_915 = true;

	private bool logic_uScriptCon_CompareBool_False_915 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_921 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_921;

	private bool logic_uScriptAct_SetBool_Out_921 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_921 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_921 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_922 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_922 = 3;

	private int logic_uScriptAct_SetInt_Target_922;

	private bool logic_uScriptAct_SetInt_Out_922 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_923 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_923;

	private bool logic_uScriptAct_SetBool_Out_923 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_923 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_923 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_924 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_924;

	private bool logic_uScriptAct_SetBool_Out_924 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_924 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_924 = true;

	private uScript_GetCircuitChargeInfo logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_926 = new uScript_GetCircuitChargeInfo();

	private TankBlock logic_uScript_GetCircuitChargeInfo_block_926;

	private Tank logic_uScript_GetCircuitChargeInfo_tech_926;

	private BlockTypes logic_uScript_GetCircuitChargeInfo_blockType_926;

	private int logic_uScript_GetCircuitChargeInfo_Return_926;

	private bool logic_uScript_GetCircuitChargeInfo_Out_926 = true;

	private bool logic_uScript_GetCircuitChargeInfo_HasValidBlock_926 = true;

	private uScriptCon_CompareInt logic_uScriptCon_CompareInt_uScriptCon_CompareInt_927 = new uScriptCon_CompareInt();

	private int logic_uScriptCon_CompareInt_A_927;

	private int logic_uScriptCon_CompareInt_B_927;

	private bool logic_uScriptCon_CompareInt_GreaterThan_927 = true;

	private bool logic_uScriptCon_CompareInt_GreaterThanOrEqualTo_927 = true;

	private bool logic_uScriptCon_CompareInt_EqualTo_927 = true;

	private bool logic_uScriptCon_CompareInt_NotEqualTo_927 = true;

	private bool logic_uScriptCon_CompareInt_LessThanOrEqualTo_927 = true;

	private bool logic_uScriptCon_CompareInt_LessThan_927 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_928 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_928 = 4;

	private int logic_uScriptAct_SetInt_Target_928;

	private bool logic_uScriptAct_SetInt_Out_928 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_929 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_929 = 3;

	private int logic_uScriptAct_SetInt_Target_929;

	private bool logic_uScriptAct_SetInt_Out_929 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_931 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_931 = 4;

	private int logic_uScriptAct_SetInt_Target_931;

	private bool logic_uScriptAct_SetInt_Out_931 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_932 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_932;

	private bool logic_uScriptCon_CompareBool_True_932 = true;

	private bool logic_uScriptCon_CompareBool_False_932 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_933 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_933;

	private bool logic_uScriptAct_SetBool_Out_933 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_933 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_933 = true;

	private uScriptCon_CompareInt logic_uScriptCon_CompareInt_uScriptCon_CompareInt_936 = new uScriptCon_CompareInt();

	private int logic_uScriptCon_CompareInt_A_936;

	private int logic_uScriptCon_CompareInt_B_936;

	private bool logic_uScriptCon_CompareInt_GreaterThan_936 = true;

	private bool logic_uScriptCon_CompareInt_GreaterThanOrEqualTo_936 = true;

	private bool logic_uScriptCon_CompareInt_EqualTo_936 = true;

	private bool logic_uScriptCon_CompareInt_NotEqualTo_936 = true;

	private bool logic_uScriptCon_CompareInt_LessThanOrEqualTo_936 = true;

	private bool logic_uScriptCon_CompareInt_LessThan_936 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_938 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_938;

	private bool logic_uScriptAct_SetBool_Out_938 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_938 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_938 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_941 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_941;

	private bool logic_uScriptAct_SetBool_Out_941 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_941 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_941 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_943 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_943;

	private bool logic_uScriptAct_SetBool_Out_943 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_943 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_943 = true;

	private uScript_GetCircuitChargeInfo logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_944 = new uScript_GetCircuitChargeInfo();

	private TankBlock logic_uScript_GetCircuitChargeInfo_block_944;

	private Tank logic_uScript_GetCircuitChargeInfo_tech_944;

	private BlockTypes logic_uScript_GetCircuitChargeInfo_blockType_944;

	private int logic_uScript_GetCircuitChargeInfo_Return_944;

	private bool logic_uScript_GetCircuitChargeInfo_Out_944 = true;

	private bool logic_uScript_GetCircuitChargeInfo_HasValidBlock_944 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_945 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_945;

	private bool logic_uScriptAct_SetBool_Out_945 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_945 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_945 = true;

	private uScriptAct_AddInt_v2 logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_948 = new uScriptAct_AddInt_v2();

	private int logic_uScriptAct_AddInt_v2_A_948;

	private int logic_uScriptAct_AddInt_v2_B_948 = 1;

	private int logic_uScriptAct_AddInt_v2_IntResult_948;

	private float logic_uScriptAct_AddInt_v2_FloatResult_948;

	private bool logic_uScriptAct_AddInt_v2_Out_948 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_949 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_949;

	private bool logic_uScriptAct_SetBool_Out_949 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_949 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_949 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_950 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_950;

	private bool logic_uScriptAct_SetBool_Out_950 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_950 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_950 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_954 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_954;

	private bool logic_uScriptAct_SetBool_Out_954 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_954 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_954 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_957 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_957 = 1;

	private int logic_uScriptAct_SetInt_Target_957;

	private bool logic_uScriptAct_SetInt_Out_957 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_958 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_958 = 6;

	private int logic_uScriptAct_SetInt_Target_958;

	private bool logic_uScriptAct_SetInt_Out_958 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_959 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_959 = 1;

	private int logic_uScriptAct_SetInt_Target_959;

	private bool logic_uScriptAct_SetInt_Out_959 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_962 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_962 = 1;

	private int logic_uScriptAct_SetInt_Target_962;

	private bool logic_uScriptAct_SetInt_Out_962 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_965 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_965;

	private bool logic_uScriptAct_SetBool_Out_965 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_965 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_965 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_966 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_966;

	private bool logic_uScriptAct_SetBool_Out_966 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_966 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_966 = true;

	private uScriptAct_AddInt_v2 logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_967 = new uScriptAct_AddInt_v2();

	private int logic_uScriptAct_AddInt_v2_A_967;

	private int logic_uScriptAct_AddInt_v2_B_967 = 1;

	private int logic_uScriptAct_AddInt_v2_IntResult_967;

	private float logic_uScriptAct_AddInt_v2_FloatResult_967;

	private bool logic_uScriptAct_AddInt_v2_Out_967 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_969 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_969;

	private bool logic_uScriptAct_SetBool_Out_969 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_969 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_969 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_972 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_972 = 1;

	private int logic_uScriptAct_SetInt_Target_972;

	private bool logic_uScriptAct_SetInt_Out_972 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_974 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_974 = 1;

	private int logic_uScriptAct_SetInt_Target_974;

	private bool logic_uScriptAct_SetInt_Out_974 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_980 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_980;

	private bool logic_uScriptAct_SetBool_Out_980 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_980 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_980 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_981 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_981;

	private bool logic_uScriptAct_SetBool_Out_981 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_981 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_981 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_982 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_982 = 4;

	private int logic_uScriptAct_SetInt_Target_982;

	private bool logic_uScriptAct_SetInt_Out_982 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_983 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_983 = 3;

	private int logic_uScriptAct_SetInt_Target_983;

	private bool logic_uScriptAct_SetInt_Out_983 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_984 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_984;

	private bool logic_uScriptCon_CompareBool_True_984 = true;

	private bool logic_uScriptCon_CompareBool_False_984 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_986 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_986;

	private bool logic_uScriptAct_SetBool_Out_986 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_986 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_986 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_988 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_988;

	private bool logic_uScriptAct_SetBool_Out_988 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_988 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_988 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_989 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_989;

	private bool logic_uScriptAct_SetBool_Out_989 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_989 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_989 = true;

	private uScript_GetCircuitChargeInfo logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_992 = new uScript_GetCircuitChargeInfo();

	private TankBlock logic_uScript_GetCircuitChargeInfo_block_992;

	private Tank logic_uScript_GetCircuitChargeInfo_tech_992;

	private BlockTypes logic_uScript_GetCircuitChargeInfo_blockType_992;

	private int logic_uScript_GetCircuitChargeInfo_Return_992;

	private bool logic_uScript_GetCircuitChargeInfo_Out_992 = true;

	private bool logic_uScript_GetCircuitChargeInfo_HasValidBlock_992 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_996 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_996;

	private bool logic_uScriptAct_SetBool_Out_996 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_996 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_996 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_997 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_997;

	private bool logic_uScriptAct_SetBool_Out_997 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_997 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_997 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_998 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_998;

	private bool logic_uScriptCon_CompareBool_True_998 = true;

	private bool logic_uScriptCon_CompareBool_False_998 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_999 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_999 = 4;

	private int logic_uScriptAct_SetInt_Target_999;

	private bool logic_uScriptAct_SetInt_Out_999 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_1002 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_1002 = 3;

	private int logic_uScriptAct_SetInt_Target_1002;

	private bool logic_uScriptAct_SetInt_Out_1002 = true;

	private uScriptCon_CompareInt logic_uScriptCon_CompareInt_uScriptCon_CompareInt_1004 = new uScriptCon_CompareInt();

	private int logic_uScriptCon_CompareInt_A_1004;

	private int logic_uScriptCon_CompareInt_B_1004;

	private bool logic_uScriptCon_CompareInt_GreaterThan_1004 = true;

	private bool logic_uScriptCon_CompareInt_GreaterThanOrEqualTo_1004 = true;

	private bool logic_uScriptCon_CompareInt_EqualTo_1004 = true;

	private bool logic_uScriptCon_CompareInt_NotEqualTo_1004 = true;

	private bool logic_uScriptCon_CompareInt_LessThanOrEqualTo_1004 = true;

	private bool logic_uScriptCon_CompareInt_LessThan_1004 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_1005 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_1005;

	private bool logic_uScriptAct_SetBool_Out_1005 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_1005 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_1005 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_1007 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_1007;

	private bool logic_uScriptAct_SetBool_Out_1007 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_1007 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_1007 = true;

	private uScriptCon_CompareInt logic_uScriptCon_CompareInt_uScriptCon_CompareInt_1010 = new uScriptCon_CompareInt();

	private int logic_uScriptCon_CompareInt_A_1010;

	private int logic_uScriptCon_CompareInt_B_1010;

	private bool logic_uScriptCon_CompareInt_GreaterThan_1010 = true;

	private bool logic_uScriptCon_CompareInt_GreaterThanOrEqualTo_1010 = true;

	private bool logic_uScriptCon_CompareInt_EqualTo_1010 = true;

	private bool logic_uScriptCon_CompareInt_NotEqualTo_1010 = true;

	private bool logic_uScriptCon_CompareInt_LessThanOrEqualTo_1010 = true;

	private bool logic_uScriptCon_CompareInt_LessThan_1010 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_1012 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_1012 = 4;

	private int logic_uScriptAct_SetInt_Target_1012;

	private bool logic_uScriptAct_SetInt_Out_1012 = true;

	private uScript_GetCircuitChargeInfo logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_1013 = new uScript_GetCircuitChargeInfo();

	private TankBlock logic_uScript_GetCircuitChargeInfo_block_1013;

	private Tank logic_uScript_GetCircuitChargeInfo_tech_1013;

	private BlockTypes logic_uScript_GetCircuitChargeInfo_blockType_1013;

	private int logic_uScript_GetCircuitChargeInfo_Return_1013;

	private bool logic_uScript_GetCircuitChargeInfo_Out_1013 = true;

	private bool logic_uScript_GetCircuitChargeInfo_HasValidBlock_1013 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_1014 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_1014;

	private bool logic_uScriptAct_SetBool_Out_1014 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_1014 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_1014 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_1018 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_1018;

	private bool logic_uScriptAct_SetBool_Out_1018 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_1018 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_1018 = true;

	private uScript_GetCircuitChargeInfo logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_1019 = new uScript_GetCircuitChargeInfo();

	private TankBlock logic_uScript_GetCircuitChargeInfo_block_1019;

	private Tank logic_uScript_GetCircuitChargeInfo_tech_1019;

	private BlockTypes logic_uScript_GetCircuitChargeInfo_blockType_1019;

	private int logic_uScript_GetCircuitChargeInfo_Return_1019;

	private bool logic_uScript_GetCircuitChargeInfo_Out_1019 = true;

	private bool logic_uScript_GetCircuitChargeInfo_HasValidBlock_1019 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_1023 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_1023;

	private bool logic_uScriptAct_SetBool_Out_1023 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_1023 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_1023 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_1024 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_1024;

	private bool logic_uScriptAct_SetBool_Out_1024 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_1024 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_1024 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_1025 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_1025;

	private bool logic_uScriptAct_SetBool_Out_1025 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_1025 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_1025 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_1027 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_1027;

	private bool logic_uScriptCon_CompareBool_True_1027 = true;

	private bool logic_uScriptCon_CompareBool_False_1027 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_1029 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_1029 = 4;

	private int logic_uScriptAct_SetInt_Target_1029;

	private bool logic_uScriptAct_SetInt_Out_1029 = true;

	private uScriptCon_CompareBool logic_uScriptCon_CompareBool_uScriptCon_CompareBool_1030 = new uScriptCon_CompareBool();

	private bool logic_uScriptCon_CompareBool_Bool_1030;

	private bool logic_uScriptCon_CompareBool_True_1030 = true;

	private bool logic_uScriptCon_CompareBool_False_1030 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_1034 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_1034 = 3;

	private int logic_uScriptAct_SetInt_Target_1034;

	private bool logic_uScriptAct_SetInt_Out_1034 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_1037 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_1037;

	private bool logic_uScriptAct_SetBool_Out_1037 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_1037 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_1037 = true;

	private uScriptCon_CompareInt logic_uScriptCon_CompareInt_uScriptCon_CompareInt_1039 = new uScriptCon_CompareInt();

	private int logic_uScriptCon_CompareInt_A_1039;

	private int logic_uScriptCon_CompareInt_B_1039;

	private bool logic_uScriptCon_CompareInt_GreaterThan_1039 = true;

	private bool logic_uScriptCon_CompareInt_GreaterThanOrEqualTo_1039 = true;

	private bool logic_uScriptCon_CompareInt_EqualTo_1039 = true;

	private bool logic_uScriptCon_CompareInt_NotEqualTo_1039 = true;

	private bool logic_uScriptCon_CompareInt_LessThanOrEqualTo_1039 = true;

	private bool logic_uScriptCon_CompareInt_LessThan_1039 = true;

	private uScriptAct_SetInt logic_uScriptAct_SetInt_uScriptAct_SetInt_1040 = new uScriptAct_SetInt();

	private int logic_uScriptAct_SetInt_Value_1040 = 3;

	private int logic_uScriptAct_SetInt_Target_1040;

	private bool logic_uScriptAct_SetInt_Out_1040 = true;

	private uScriptAct_SetBool logic_uScriptAct_SetBool_uScriptAct_SetBool_1041 = new uScriptAct_SetBool();

	private bool logic_uScriptAct_SetBool_Target_1041;

	private bool logic_uScriptAct_SetBool_Out_1041 = true;

	private bool logic_uScriptAct_SetBool_SetTrue_1041 = true;

	private bool logic_uScriptAct_SetBool_SetFalse_1041 = true;

	private uScript_Wait logic_uScript_Wait_uScript_Wait_1045 = new uScript_Wait();

	private float logic_uScript_Wait_seconds_1045;

	private bool logic_uScript_Wait_repeat_1045 = true;

	private bool logic_uScript_Wait_Waited_1045 = true;

	private uScript_Wait logic_uScript_Wait_uScript_Wait_1047 = new uScript_Wait();

	private float logic_uScript_Wait_seconds_1047;

	private bool logic_uScript_Wait_repeat_1047 = true;

	private bool logic_uScript_Wait_Waited_1047 = true;

	private uScript_Wait logic_uScript_Wait_uScript_Wait_1049 = new uScript_Wait();

	private float logic_uScript_Wait_seconds_1049;

	private bool logic_uScript_Wait_repeat_1049 = true;

	private bool logic_uScript_Wait_Waited_1049 = true;

	private uScript_Wait logic_uScript_Wait_uScript_Wait_1051 = new uScript_Wait();

	private float logic_uScript_Wait_seconds_1051;

	private bool logic_uScript_Wait_repeat_1051 = true;

	private bool logic_uScript_Wait_Waited_1051 = true;

	private uScript_Wait logic_uScript_Wait_uScript_Wait_1053 = new uScript_Wait();

	private float logic_uScript_Wait_seconds_1053;

	private bool logic_uScript_Wait_repeat_1053 = true;

	private bool logic_uScript_Wait_Waited_1053 = true;

	private uScriptAct_AddInt_v2 logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_1055 = new uScriptAct_AddInt_v2();

	private int logic_uScriptAct_AddInt_v2_A_1055;

	private int logic_uScriptAct_AddInt_v2_B_1055 = 1;

	private int logic_uScriptAct_AddInt_v2_IntResult_1055;

	private float logic_uScriptAct_AddInt_v2_FloatResult_1055;

	private bool logic_uScriptAct_AddInt_v2_Out_1055 = true;

	private void SyncUnityHooks()
	{
		SyncEventListeners();
		if (null == owner_Connection_2 || !m_RegisteredForEvents)
		{
			owner_Connection_2 = parentGameObject;
		}
		if (null == owner_Connection_21 || !m_RegisteredForEvents)
		{
			owner_Connection_21 = parentGameObject;
		}
		if (null == owner_Connection_27 || !m_RegisteredForEvents)
		{
			owner_Connection_27 = parentGameObject;
			if (null != owner_Connection_27)
			{
				uScript_EncounterUpdate uScript_EncounterUpdate2 = owner_Connection_27.GetComponent<uScript_EncounterUpdate>();
				if (null == uScript_EncounterUpdate2)
				{
					uScript_EncounterUpdate2 = owner_Connection_27.AddComponent<uScript_EncounterUpdate>();
				}
				if (null != uScript_EncounterUpdate2)
				{
					uScript_EncounterUpdate2.OnUpdate += Instance_OnUpdate_51;
					uScript_EncounterUpdate2.OnSuspend += Instance_OnSuspend_51;
					uScript_EncounterUpdate2.OnResume += Instance_OnResume_51;
				}
			}
		}
		if (null == owner_Connection_33 || !m_RegisteredForEvents)
		{
			owner_Connection_33 = parentGameObject;
		}
		if (null == owner_Connection_54 || !m_RegisteredForEvents)
		{
			owner_Connection_54 = parentGameObject;
		}
		if (null == owner_Connection_56 || !m_RegisteredForEvents)
		{
			owner_Connection_56 = parentGameObject;
		}
		if (null == owner_Connection_57 || !m_RegisteredForEvents)
		{
			owner_Connection_57 = parentGameObject;
		}
		if (null == owner_Connection_58 || !m_RegisteredForEvents)
		{
			owner_Connection_58 = parentGameObject;
		}
		if (null == owner_Connection_82 || !m_RegisteredForEvents)
		{
			owner_Connection_82 = parentGameObject;
		}
		if (null == owner_Connection_149 || !m_RegisteredForEvents)
		{
			owner_Connection_149 = parentGameObject;
		}
		if (null == owner_Connection_162 || !m_RegisteredForEvents)
		{
			owner_Connection_162 = parentGameObject;
			if (null != owner_Connection_162)
			{
				uScript_SaveLoad uScript_SaveLoad2 = owner_Connection_162.GetComponent<uScript_SaveLoad>();
				if (null == uScript_SaveLoad2)
				{
					uScript_SaveLoad2 = owner_Connection_162.AddComponent<uScript_SaveLoad>();
				}
				if (null != uScript_SaveLoad2)
				{
					uScript_SaveLoad2.SaveEvent += Instance_SaveEvent_140;
					uScript_SaveLoad2.LoadEvent += Instance_LoadEvent_140;
					uScript_SaveLoad2.RestartEvent += Instance_RestartEvent_140;
				}
			}
		}
		if (null == owner_Connection_196 || !m_RegisteredForEvents)
		{
			owner_Connection_196 = parentGameObject;
		}
		if (null == owner_Connection_210 || !m_RegisteredForEvents)
		{
			owner_Connection_210 = parentGameObject;
		}
		if (null == owner_Connection_213 || !m_RegisteredForEvents)
		{
			owner_Connection_213 = parentGameObject;
		}
		if (null == owner_Connection_237 || !m_RegisteredForEvents)
		{
			owner_Connection_237 = parentGameObject;
		}
		if (null == owner_Connection_263 || !m_RegisteredForEvents)
		{
			owner_Connection_263 = parentGameObject;
		}
		if (null == owner_Connection_267 || !m_RegisteredForEvents)
		{
			owner_Connection_267 = parentGameObject;
		}
		if (null == owner_Connection_279 || !m_RegisteredForEvents)
		{
			owner_Connection_279 = parentGameObject;
		}
		if (null == owner_Connection_282 || !m_RegisteredForEvents)
		{
			owner_Connection_282 = parentGameObject;
		}
		if (null == owner_Connection_289 || !m_RegisteredForEvents)
		{
			owner_Connection_289 = parentGameObject;
		}
		if (null == owner_Connection_296 || !m_RegisteredForEvents)
		{
			owner_Connection_296 = parentGameObject;
		}
		if (null == owner_Connection_328 || !m_RegisteredForEvents)
		{
			owner_Connection_328 = parentGameObject;
		}
		if (null == owner_Connection_337 || !m_RegisteredForEvents)
		{
			owner_Connection_337 = parentGameObject;
		}
		if (null == owner_Connection_340 || !m_RegisteredForEvents)
		{
			owner_Connection_340 = parentGameObject;
		}
		if (null == owner_Connection_342 || !m_RegisteredForEvents)
		{
			owner_Connection_342 = parentGameObject;
		}
		if (null == owner_Connection_344 || !m_RegisteredForEvents)
		{
			owner_Connection_344 = parentGameObject;
		}
		if (null == owner_Connection_415 || !m_RegisteredForEvents)
		{
			owner_Connection_415 = parentGameObject;
		}
		if (null == owner_Connection_421 || !m_RegisteredForEvents)
		{
			owner_Connection_421 = parentGameObject;
		}
		if (null == owner_Connection_485 || !m_RegisteredForEvents)
		{
			owner_Connection_485 = parentGameObject;
		}
		if (null == owner_Connection_490 || !m_RegisteredForEvents)
		{
			owner_Connection_490 = parentGameObject;
		}
		if (null == owner_Connection_509 || !m_RegisteredForEvents)
		{
			owner_Connection_509 = parentGameObject;
		}
		if (null == owner_Connection_527 || !m_RegisteredForEvents)
		{
			owner_Connection_527 = parentGameObject;
		}
	}

	private void RegisterForUnityHooks()
	{
		SyncEventListeners();
		if (!m_RegisteredForEvents && null != owner_Connection_27)
		{
			uScript_EncounterUpdate uScript_EncounterUpdate2 = owner_Connection_27.GetComponent<uScript_EncounterUpdate>();
			if (null == uScript_EncounterUpdate2)
			{
				uScript_EncounterUpdate2 = owner_Connection_27.AddComponent<uScript_EncounterUpdate>();
			}
			if (null != uScript_EncounterUpdate2)
			{
				uScript_EncounterUpdate2.OnUpdate += Instance_OnUpdate_51;
				uScript_EncounterUpdate2.OnSuspend += Instance_OnSuspend_51;
				uScript_EncounterUpdate2.OnResume += Instance_OnResume_51;
			}
		}
		if (!m_RegisteredForEvents && null != owner_Connection_162)
		{
			uScript_SaveLoad uScript_SaveLoad2 = owner_Connection_162.GetComponent<uScript_SaveLoad>();
			if (null == uScript_SaveLoad2)
			{
				uScript_SaveLoad2 = owner_Connection_162.AddComponent<uScript_SaveLoad>();
			}
			if (null != uScript_SaveLoad2)
			{
				uScript_SaveLoad2.SaveEvent += Instance_SaveEvent_140;
				uScript_SaveLoad2.LoadEvent += Instance_LoadEvent_140;
				uScript_SaveLoad2.RestartEvent += Instance_RestartEvent_140;
			}
		}
	}

	private void SyncEventListeners()
	{
	}

	private void UnregisterEventListeners()
	{
		if (null != owner_Connection_27)
		{
			uScript_EncounterUpdate component = owner_Connection_27.GetComponent<uScript_EncounterUpdate>();
			if (null != component)
			{
				component.OnUpdate -= Instance_OnUpdate_51;
				component.OnSuspend -= Instance_OnSuspend_51;
				component.OnResume -= Instance_OnResume_51;
			}
		}
		if (null != owner_Connection_162)
		{
			uScript_SaveLoad component2 = owner_Connection_162.GetComponent<uScript_SaveLoad>();
			if (null != component2)
			{
				component2.SaveEvent -= Instance_SaveEvent_140;
				component2.LoadEvent -= Instance_LoadEvent_140;
				component2.RestartEvent -= Instance_RestartEvent_140;
			}
		}
	}

	public override void SetParent(GameObject g)
	{
		parentGameObject = g;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_9.SetParent(g);
		logic_uScript_SetCustomRadarTeamID_uScript_SetCustomRadarTeamID_11.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_12.SetParent(g);
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_13.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_14.SetParent(g);
		logic_uScript_SetCustomRadarTeamID_uScript_SetCustomRadarTeamID_16.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_19.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_22.SetParent(g);
		logic_uScript_LockTechInteraction_uScript_LockTechInteraction_30.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_31.SetParent(g);
		logic_uScript_LockTechInteraction_uScript_LockTechInteraction_37.SetParent(g);
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_38.SetParent(g);
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_41.SetParent(g);
		logic_uScript_PausePopulation_uScript_PausePopulation_49.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_50.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_52.SetParent(g);
		logic_uScript_LockTechInteraction_uScript_LockTechInteraction_53.SetParent(g);
		logic_uScript_LockTechInteraction_uScript_LockTechInteraction_55.SetParent(g);
		logic_uScript_LockTech_uScript_LockTech_61.SetParent(g);
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_62.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_63.SetParent(g);
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_64.SetParent(g);
		logic_uScript_SetCustomRadarTeamID_uScript_SetCustomRadarTeamID_65.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_66.SetParent(g);
		logic_uScript_Wait_uScript_Wait_67.SetParent(g);
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_71.SetParent(g);
		logic_uScript_LockTechInteraction_uScript_LockTechInteraction_73.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_75.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_76.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_78.SetParent(g);
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_80.SetParent(g);
		logic_uScript_SetCustomRadarTeamID_uScript_SetCustomRadarTeamID_81.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_85.SetParent(g);
		logic_uScript_GetTankBlock_uScript_GetTankBlock_88.SetParent(g);
		logic_uScript_GetTankBlock_uScript_GetTankBlock_91.SetParent(g);
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_92.SetParent(g);
		logic_uScript_GetTankBlock_uScript_GetTankBlock_100.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_101.SetParent(g);
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_102.SetParent(g);
		logic_uScript_SetTankHideBlockLimit_uScript_SetTankHideBlockLimit_104.SetParent(g);
		logic_uScript_LockTechInteraction_uScript_LockTechInteraction_105.SetParent(g);
		logic_uScript_Wait_uScript_Wait_106.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_109.SetParent(g);
		logic_uScript_SetCustomRadarTeamID_uScript_SetCustomRadarTeamID_114.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_115.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_116.SetParent(g);
		logic_uScript_PausePopulation_uScript_PausePopulation_119.SetParent(g);
		logic_uScript_LockTech_uScript_LockTech_123.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_124.SetParent(g);
		logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_125.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_126.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_127.SetParent(g);
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_130.SetParent(g);
		logic_uScript_LockTech_uScript_LockTech_133.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_135.SetParent(g);
		logic_uScript_LockTechInteraction_uScript_LockTechInteraction_137.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_141.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_144.SetParent(g);
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_145.SetParent(g);
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_147.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_150.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_151.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_154.SetParent(g);
		logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_156.SetParent(g);
		logic_uScript_GetTankBlock_uScript_GetTankBlock_161.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_163.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_164.SetParent(g);
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_165.SetParent(g);
		logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_169.SetParent(g);
		logic_uScript_LockTech_uScript_LockTech_170.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_174.SetParent(g);
		logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_176.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_177.SetParent(g);
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_181.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_184.SetParent(g);
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_185.SetParent(g);
		logic_uScript_SetTankHideBlockLimit_uScript_SetTankHideBlockLimit_186.SetParent(g);
		logic_uScript_SetTankHideBlockLimit_uScript_SetTankHideBlockLimit_188.SetParent(g);
		logic_uScript_RemoveScenery_uScript_RemoveScenery_189.SetParent(g);
		logic_uScript_LockTechInteraction_uScript_LockTechInteraction_192.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_194.SetParent(g);
		logic_uScript_AccessListTech_uScript_AccessListTech_197.SetParent(g);
		logic_uScript_LockTech_uScript_LockTech_201.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_202.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_203.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_204.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_206.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_208.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_209.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_211.SetParent(g);
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_217.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_222.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_223.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_224.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_225.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_228.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_230.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_231.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_232.SetParent(g);
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_236.SetParent(g);
		logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_241.SetParent(g);
		logic_uScript_Wait_uScript_Wait_242.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_243.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_244.SetParent(g);
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_245.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_247.SetParent(g);
		logic_uScript_SetTankHideBlockLimit_uScript_SetTankHideBlockLimit_248.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_249.SetParent(g);
		logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_250.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_251.SetParent(g);
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_252.SetParent(g);
		logic_uScript_LockTechInteraction_uScript_LockTechInteraction_254.SetParent(g);
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_255.SetParent(g);
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_260.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_264.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_265.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_266.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_268.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_270.SetParent(g);
		logic_uScriptAct_Concatenate_uScriptAct_Concatenate_272.SetParent(g);
		logic_uScriptAct_PrintText_uScriptAct_PrintText_274.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_277.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_278.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_281.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_283.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_291.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_293.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_295.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_297.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_301.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_303.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_305.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_309.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_313.SetParent(g);
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_314.SetParent(g);
		logic_uScript_Wait_uScript_Wait_316.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_320.SetParent(g);
		logic_uScript_Wait_uScript_Wait_321.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_323.SetParent(g);
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_326.SetParent(g);
		logic_uScript_RemoveTech_uScript_RemoveTech_329.SetParent(g);
		logic_uScript_SpawnVFX_uScript_SpawnVFX_334.SetParent(g);
		logic_uScript_SpawnVFX_uScript_SpawnVFX_339.SetParent(g);
		logic_uScript_RemoveTech_uScript_RemoveTech_345.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_347.SetParent(g);
		logic_uScript_RemoveTech_uScript_RemoveTech_348.SetParent(g);
		logic_uScript_SpawnVFX_uScript_SpawnVFX_351.SetParent(g);
		logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_353.SetParent(g);
		logic_uScript_FinishEncounter_uScript_FinishEncounter_358.SetParent(g);
		logic_uScript_RemoveTech_uScript_RemoveTech_359.SetParent(g);
		logic_uScript_SpawnVFX_uScript_SpawnVFX_360.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_361.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_363.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_365.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_366.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_368.SetParent(g);
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_372.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_374.SetParent(g);
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_375.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_378.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_380.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_384.SetParent(g);
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_386.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_387.SetParent(g);
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_389.SetParent(g);
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_397.SetParent(g);
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_399.SetParent(g);
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_401.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_403.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_405.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_407.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_411.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_412.SetParent(g);
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_413.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_416.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_417.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_419.SetParent(g);
		logic_uScript_Wait_uScript_Wait_424.SetParent(g);
		logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_428.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_429.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_433.SetParent(g);
		logic_uScript_Wait_uScript_Wait_434.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_436.SetParent(g);
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_437.SetParent(g);
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_438.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_440.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_442.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_447.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_448.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_449.SetParent(g);
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_457.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_462.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_463.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_464.SetParent(g);
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_465.SetParent(g);
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_467.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_468.SetParent(g);
		logic_uScript_Wait_uScript_Wait_471.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_472.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_476.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_478.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_481.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_482.SetParent(g);
		logic_uScript_Wait_uScript_Wait_484.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_492.SetParent(g);
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_494.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_496.SetParent(g);
		logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_497.SetParent(g);
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_504.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_505.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_511.SetParent(g);
		logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_512.SetParent(g);
		logic_uScript_Wait_uScript_Wait_517.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_518.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_521.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_522.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_523.SetParent(g);
		logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_526.SetParent(g);
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_528.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_529.SetParent(g);
		logic_uScript_Wait_uScript_Wait_530.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_532.SetParent(g);
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_534.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_535.SetParent(g);
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_543.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_544.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_547.SetParent(g);
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_549.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_551.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_552.SetParent(g);
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_555.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_557.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_559.SetParent(g);
		logic_uScript_Wait_uScript_Wait_561.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_564.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_566.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_569.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_570.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_571.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_572.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_573.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_577.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_578.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_586.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_587.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_588.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_589.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_590.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_591.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_592.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_600.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_601.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_602.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_603.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_604.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_606.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_609.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_610.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_613.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_615.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_617.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_618.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_622.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_623.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_625.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_626.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_632.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_634.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_637.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_638.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_639.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_641.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_642.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_646.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_648.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_649.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_652.SetParent(g);
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_654.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_656.SetParent(g);
		logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_658.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_661.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_663.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_664.SetParent(g);
		logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_665.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_667.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_668.SetParent(g);
		logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_669.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_674.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_675.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_676.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_678.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_679.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_680.SetParent(g);
		logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_681.SetParent(g);
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_682.SetParent(g);
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_683.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_684.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_686.SetParent(g);
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_688.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_689.SetParent(g);
		logic_uScript_AddMessage_uScript_AddMessage_698.SetParent(g);
		logic_uScript_Wait_uScript_Wait_699.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_701.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_703.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_706.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_708.SetParent(g);
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_709.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_711.SetParent(g);
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_713.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_714.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_716.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_720.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_721.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_723.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_726.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_727.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_728.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_732.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_733.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_734.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_739.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_740.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_742.SetParent(g);
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_744.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_747.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_748.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_750.SetParent(g);
		logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_751.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_753.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_758.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_759.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_760.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_761.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_762.SetParent(g);
		logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_763.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_764.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_767.SetParent(g);
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_769.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_771.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_772.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_776.SetParent(g);
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_777.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_778.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_781.SetParent(g);
		logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_784.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_787.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_789.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_792.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_794.SetParent(g);
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_795.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_796.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_800.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_801.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_805.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_806.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_807.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_810.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_813.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_814.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_816.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_819.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_820.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_822.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_824.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_825.SetParent(g);
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_826.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_831.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_832.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_834.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_836.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_838.SetParent(g);
		logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_841.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_842.SetParent(g);
		logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_843.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_844.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_850.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_851.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_852.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_856.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_857.SetParent(g);
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_858.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_859.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_861.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_862.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_863.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_867.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_868.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_871.SetParent(g);
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_874.SetParent(g);
		logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_877.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_878.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_879.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_882.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_883.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_885.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_887.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_889.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_892.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_899.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_900.SetParent(g);
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_902.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_903.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_904.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_905.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_907.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_909.SetParent(g);
		logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_910.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_915.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_921.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_922.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_923.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_924.SetParent(g);
		logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_926.SetParent(g);
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_927.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_928.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_929.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_931.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_932.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_933.SetParent(g);
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_936.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_938.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_941.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_943.SetParent(g);
		logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_944.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_945.SetParent(g);
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_948.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_949.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_950.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_954.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_957.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_958.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_959.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_962.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_965.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_966.SetParent(g);
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_967.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_969.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_972.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_974.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_980.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_981.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_982.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_983.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_984.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_986.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_988.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_989.SetParent(g);
		logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_992.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_996.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_997.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_998.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_999.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_1002.SetParent(g);
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_1004.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_1005.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_1007.SetParent(g);
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_1010.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_1012.SetParent(g);
		logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_1013.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_1014.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_1018.SetParent(g);
		logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_1019.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_1023.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_1024.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_1025.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_1027.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_1029.SetParent(g);
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_1030.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_1034.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_1037.SetParent(g);
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_1039.SetParent(g);
		logic_uScriptAct_SetInt_uScriptAct_SetInt_1040.SetParent(g);
		logic_uScriptAct_SetBool_uScriptAct_SetBool_1041.SetParent(g);
		logic_uScript_Wait_uScript_Wait_1045.SetParent(g);
		logic_uScript_Wait_uScript_Wait_1047.SetParent(g);
		logic_uScript_Wait_uScript_Wait_1049.SetParent(g);
		logic_uScript_Wait_uScript_Wait_1051.SetParent(g);
		logic_uScript_Wait_uScript_Wait_1053.SetParent(g);
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_1055.SetParent(g);
		owner_Connection_2 = parentGameObject;
		owner_Connection_21 = parentGameObject;
		owner_Connection_27 = parentGameObject;
		owner_Connection_33 = parentGameObject;
		owner_Connection_54 = parentGameObject;
		owner_Connection_56 = parentGameObject;
		owner_Connection_57 = parentGameObject;
		owner_Connection_58 = parentGameObject;
		owner_Connection_82 = parentGameObject;
		owner_Connection_149 = parentGameObject;
		owner_Connection_162 = parentGameObject;
		owner_Connection_196 = parentGameObject;
		owner_Connection_210 = parentGameObject;
		owner_Connection_213 = parentGameObject;
		owner_Connection_237 = parentGameObject;
		owner_Connection_263 = parentGameObject;
		owner_Connection_267 = parentGameObject;
		owner_Connection_279 = parentGameObject;
		owner_Connection_282 = parentGameObject;
		owner_Connection_289 = parentGameObject;
		owner_Connection_296 = parentGameObject;
		owner_Connection_328 = parentGameObject;
		owner_Connection_337 = parentGameObject;
		owner_Connection_340 = parentGameObject;
		owner_Connection_342 = parentGameObject;
		owner_Connection_344 = parentGameObject;
		owner_Connection_415 = parentGameObject;
		owner_Connection_421 = parentGameObject;
		owner_Connection_485 = parentGameObject;
		owner_Connection_490 = parentGameObject;
		owner_Connection_509 = parentGameObject;
		owner_Connection_527 = parentGameObject;
	}

	public void Awake()
	{
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_52.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_85.Awake();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_147.Awake();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_217.Awake();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_236.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_249.Awake();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_252.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_270.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_301.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_303.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_363.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_365.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_366.Awake();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_372.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_378.Awake();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_386.Awake();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_397.Awake();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_399.Awake();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_401.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_403.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_405.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_407.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_411.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_412.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_413.Awake();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_52.Save_Out += SubGraph_SaveLoadBool_Save_Out_52;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_52.Load_Out += SubGraph_SaveLoadBool_Load_Out_52;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_52.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_52;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_85.Save_Out += SubGraph_SaveLoadBool_Save_Out_85;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_85.Load_Out += SubGraph_SaveLoadBool_Load_Out_85;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_85.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_85;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_102.Output1 += uScriptCon_ManualSwitch_Output1_102;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_102.Output2 += uScriptCon_ManualSwitch_Output2_102;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_102.Output3 += uScriptCon_ManualSwitch_Output3_102;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_102.Output4 += uScriptCon_ManualSwitch_Output4_102;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_102.Output5 += uScriptCon_ManualSwitch_Output5_102;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_102.Output6 += uScriptCon_ManualSwitch_Output6_102;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_102.Output7 += uScriptCon_ManualSwitch_Output7_102;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_102.Output8 += uScriptCon_ManualSwitch_Output8_102;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_147.Save_Out += SubGraph_SaveLoadInt_Save_Out_147;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_147.Load_Out += SubGraph_SaveLoadInt_Load_Out_147;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_147.Restart_Out += SubGraph_SaveLoadInt_Restart_Out_147;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_181.Output1 += uScriptCon_ManualSwitch_Output1_181;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_181.Output2 += uScriptCon_ManualSwitch_Output2_181;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_181.Output3 += uScriptCon_ManualSwitch_Output3_181;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_181.Output4 += uScriptCon_ManualSwitch_Output4_181;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_181.Output5 += uScriptCon_ManualSwitch_Output5_181;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_181.Output6 += uScriptCon_ManualSwitch_Output6_181;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_181.Output7 += uScriptCon_ManualSwitch_Output7_181;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_181.Output8 += uScriptCon_ManualSwitch_Output8_181;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_217.Save_Out += SubGraph_SaveLoadInt_Save_Out_217;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_217.Load_Out += SubGraph_SaveLoadInt_Load_Out_217;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_217.Restart_Out += SubGraph_SaveLoadInt_Restart_Out_217;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_236.Save_Out += SubGraph_SaveLoadInt_Save_Out_236;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_236.Load_Out += SubGraph_SaveLoadInt_Load_Out_236;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_236.Restart_Out += SubGraph_SaveLoadInt_Restart_Out_236;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_245.Output1 += uScriptCon_ManualSwitch_Output1_245;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_245.Output2 += uScriptCon_ManualSwitch_Output2_245;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_245.Output3 += uScriptCon_ManualSwitch_Output3_245;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_245.Output4 += uScriptCon_ManualSwitch_Output4_245;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_245.Output5 += uScriptCon_ManualSwitch_Output5_245;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_245.Output6 += uScriptCon_ManualSwitch_Output6_245;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_245.Output7 += uScriptCon_ManualSwitch_Output7_245;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_245.Output8 += uScriptCon_ManualSwitch_Output8_245;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_249.Save_Out += SubGraph_SaveLoadBool_Save_Out_249;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_249.Load_Out += SubGraph_SaveLoadBool_Load_Out_249;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_249.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_249;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_252.Save_Out += SubGraph_SaveLoadInt_Save_Out_252;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_252.Load_Out += SubGraph_SaveLoadInt_Load_Out_252;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_252.Restart_Out += SubGraph_SaveLoadInt_Restart_Out_252;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_270.Save_Out += SubGraph_SaveLoadBool_Save_Out_270;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_270.Load_Out += SubGraph_SaveLoadBool_Load_Out_270;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_270.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_270;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_301.Save_Out += SubGraph_SaveLoadBool_Save_Out_301;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_301.Load_Out += SubGraph_SaveLoadBool_Load_Out_301;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_301.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_301;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_303.Save_Out += SubGraph_SaveLoadBool_Save_Out_303;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_303.Load_Out += SubGraph_SaveLoadBool_Load_Out_303;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_303.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_303;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_363.Save_Out += SubGraph_SaveLoadBool_Save_Out_363;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_363.Load_Out += SubGraph_SaveLoadBool_Load_Out_363;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_363.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_363;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_365.Save_Out += SubGraph_SaveLoadBool_Save_Out_365;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_365.Load_Out += SubGraph_SaveLoadBool_Load_Out_365;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_365.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_365;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_366.Save_Out += SubGraph_SaveLoadBool_Save_Out_366;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_366.Load_Out += SubGraph_SaveLoadBool_Load_Out_366;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_366.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_366;
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_372.Out += SubGraph_LoadObjectiveStates_Out_372;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_378.Save_Out += SubGraph_SaveLoadBool_Save_Out_378;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_378.Load_Out += SubGraph_SaveLoadBool_Load_Out_378;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_378.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_378;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_386.Out += SubGraph_CompleteObjectiveStage_Out_386;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_389.Output1 += uScriptCon_ManualSwitch_Output1_389;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_389.Output2 += uScriptCon_ManualSwitch_Output2_389;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_389.Output3 += uScriptCon_ManualSwitch_Output3_389;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_389.Output4 += uScriptCon_ManualSwitch_Output4_389;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_389.Output5 += uScriptCon_ManualSwitch_Output5_389;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_389.Output6 += uScriptCon_ManualSwitch_Output6_389;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_389.Output7 += uScriptCon_ManualSwitch_Output7_389;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_389.Output8 += uScriptCon_ManualSwitch_Output8_389;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_397.Save_Out += SubGraph_SaveLoadInt_Save_Out_397;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_397.Load_Out += SubGraph_SaveLoadInt_Load_Out_397;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_397.Restart_Out += SubGraph_SaveLoadInt_Restart_Out_397;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_399.Save_Out += SubGraph_SaveLoadInt_Save_Out_399;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_399.Load_Out += SubGraph_SaveLoadInt_Load_Out_399;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_399.Restart_Out += SubGraph_SaveLoadInt_Restart_Out_399;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_401.Save_Out += SubGraph_SaveLoadInt_Save_Out_401;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_401.Load_Out += SubGraph_SaveLoadInt_Load_Out_401;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_401.Restart_Out += SubGraph_SaveLoadInt_Restart_Out_401;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_403.Save_Out += SubGraph_SaveLoadBool_Save_Out_403;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_403.Load_Out += SubGraph_SaveLoadBool_Load_Out_403;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_403.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_403;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_405.Save_Out += SubGraph_SaveLoadBool_Save_Out_405;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_405.Load_Out += SubGraph_SaveLoadBool_Load_Out_405;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_405.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_405;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_407.Save_Out += SubGraph_SaveLoadBool_Save_Out_407;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_407.Load_Out += SubGraph_SaveLoadBool_Load_Out_407;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_407.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_407;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_411.Save_Out += SubGraph_SaveLoadBool_Save_Out_411;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_411.Load_Out += SubGraph_SaveLoadBool_Load_Out_411;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_411.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_411;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_412.Save_Out += SubGraph_SaveLoadBool_Save_Out_412;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_412.Load_Out += SubGraph_SaveLoadBool_Load_Out_412;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_412.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_412;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_413.Save_Out += SubGraph_SaveLoadBool_Save_Out_413;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_413.Load_Out += SubGraph_SaveLoadBool_Load_Out_413;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_413.Restart_Out += SubGraph_SaveLoadBool_Restart_Out_413;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_437.Output1 += uScriptCon_ManualSwitch_Output1_437;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_437.Output2 += uScriptCon_ManualSwitch_Output2_437;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_437.Output3 += uScriptCon_ManualSwitch_Output3_437;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_437.Output4 += uScriptCon_ManualSwitch_Output4_437;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_437.Output5 += uScriptCon_ManualSwitch_Output5_437;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_437.Output6 += uScriptCon_ManualSwitch_Output6_437;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_437.Output7 += uScriptCon_ManualSwitch_Output7_437;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_437.Output8 += uScriptCon_ManualSwitch_Output8_437;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_494.Output1 += uScriptCon_ManualSwitch_Output1_494;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_494.Output2 += uScriptCon_ManualSwitch_Output2_494;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_494.Output3 += uScriptCon_ManualSwitch_Output3_494;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_494.Output4 += uScriptCon_ManualSwitch_Output4_494;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_494.Output5 += uScriptCon_ManualSwitch_Output5_494;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_494.Output6 += uScriptCon_ManualSwitch_Output6_494;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_494.Output7 += uScriptCon_ManualSwitch_Output7_494;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_494.Output8 += uScriptCon_ManualSwitch_Output8_494;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_528.Output1 += uScriptCon_ManualSwitch_Output1_528;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_528.Output2 += uScriptCon_ManualSwitch_Output2_528;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_528.Output3 += uScriptCon_ManualSwitch_Output3_528;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_528.Output4 += uScriptCon_ManualSwitch_Output4_528;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_528.Output5 += uScriptCon_ManualSwitch_Output5_528;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_528.Output6 += uScriptCon_ManualSwitch_Output6_528;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_528.Output7 += uScriptCon_ManualSwitch_Output7_528;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_528.Output8 += uScriptCon_ManualSwitch_Output8_528;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_543.Output1 += uScriptCon_ManualSwitch_Output1_543;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_543.Output2 += uScriptCon_ManualSwitch_Output2_543;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_543.Output3 += uScriptCon_ManualSwitch_Output3_543;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_543.Output4 += uScriptCon_ManualSwitch_Output4_543;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_543.Output5 += uScriptCon_ManualSwitch_Output5_543;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_543.Output6 += uScriptCon_ManualSwitch_Output6_543;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_543.Output7 += uScriptCon_ManualSwitch_Output7_543;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_543.Output8 += uScriptCon_ManualSwitch_Output8_543;
	}

	public void Start()
	{
		SyncUnityHooks();
		m_RegisteredForEvents = true;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_52.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_85.Start();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_147.Start();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_217.Start();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_236.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_249.Start();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_252.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_270.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_301.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_303.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_363.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_365.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_366.Start();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_372.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_378.Start();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_386.Start();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_397.Start();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_399.Start();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_401.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_403.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_405.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_407.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_411.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_412.Start();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_413.Start();
	}

	public void OnEnable()
	{
		RegisterForUnityHooks();
		m_RegisteredForEvents = true;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_52.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_85.OnEnable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_147.OnEnable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_217.OnEnable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_236.OnEnable();
		logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_241.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_249.OnEnable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_252.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_270.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_301.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_303.OnEnable();
		logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_353.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_363.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_365.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_366.OnEnable();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_372.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_378.OnEnable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_386.OnEnable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_397.OnEnable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_399.OnEnable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_401.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_403.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_405.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_407.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_411.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_412.OnEnable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_413.OnEnable();
	}

	public void OnDisable()
	{
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_13.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_19.OnDisable();
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_41.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_52.OnDisable();
		logic_uScript_Wait_uScript_Wait_67.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_75.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_85.OnDisable();
		logic_uScript_GetTankBlock_uScript_GetTankBlock_88.OnDisable();
		logic_uScript_GetTankBlock_uScript_GetTankBlock_91.OnDisable();
		logic_uScript_GetTankBlock_uScript_GetTankBlock_100.OnDisable();
		logic_uScript_Wait_uScript_Wait_106.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_124.OnDisable();
		logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_125.OnDisable();
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_130.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_135.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_144.OnDisable();
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_145.OnDisable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_147.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_150.OnDisable();
		logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_156.OnDisable();
		logic_uScript_GetTankBlock_uScript_GetTankBlock_161.OnDisable();
		logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_169.OnDisable();
		logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_176.OnDisable();
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_185.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_202.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_203.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_206.OnDisable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_217.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_230.OnDisable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_236.OnDisable();
		logic_uScript_Wait_uScript_Wait_242.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_249.OnDisable();
		logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_250.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_251.OnDisable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_252.OnDisable();
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_260.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_270.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_301.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_303.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_309.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_313.OnDisable();
		logic_uScript_Wait_uScript_Wait_316.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_320.OnDisable();
		logic_uScript_Wait_uScript_Wait_321.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_323.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_347.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_363.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_365.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_366.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_368.OnDisable();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_372.OnDisable();
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_375.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_378.OnDisable();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_386.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_387.OnDisable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_397.OnDisable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_399.OnDisable();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_401.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_403.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_405.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_407.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_411.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_412.OnDisable();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_413.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_419.OnDisable();
		logic_uScript_Wait_uScript_Wait_424.OnDisable();
		logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_428.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_429.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_433.OnDisable();
		logic_uScript_Wait_uScript_Wait_434.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_436.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_442.OnDisable();
		logic_uScript_Wait_uScript_Wait_471.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_472.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_478.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_481.OnDisable();
		logic_uScript_Wait_uScript_Wait_484.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_492.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_496.OnDisable();
		logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_497.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_511.OnDisable();
		logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_512.OnDisable();
		logic_uScript_Wait_uScript_Wait_517.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_518.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_521.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_522.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_523.OnDisable();
		logic_uScript_Wait_uScript_Wait_530.OnDisable();
		logic_uScript_Wait_uScript_Wait_561.OnDisable();
		logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_658.OnDisable();
		logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_665.OnDisable();
		logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_669.OnDisable();
		logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_681.OnDisable();
		logic_uScript_AddMessage_uScript_AddMessage_698.OnDisable();
		logic_uScript_Wait_uScript_Wait_699.OnDisable();
		logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_751.OnDisable();
		logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_763.OnDisable();
		logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_784.OnDisable();
		logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_841.OnDisable();
		logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_843.OnDisable();
		logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_877.OnDisable();
		logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_910.OnDisable();
		logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_926.OnDisable();
		logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_944.OnDisable();
		logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_992.OnDisable();
		logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_1013.OnDisable();
		logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_1019.OnDisable();
		logic_uScript_Wait_uScript_Wait_1045.OnDisable();
		logic_uScript_Wait_uScript_Wait_1047.OnDisable();
		logic_uScript_Wait_uScript_Wait_1049.OnDisable();
		logic_uScript_Wait_uScript_Wait_1051.OnDisable();
		logic_uScript_Wait_uScript_Wait_1053.OnDisable();
		UnregisterEventListeners();
		m_RegisteredForEvents = false;
	}

	public void Update()
	{
		SyncEventListeners();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_52.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_85.Update();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_147.Update();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_217.Update();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_236.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_249.Update();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_252.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_270.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_301.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_303.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_363.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_365.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_366.Update();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_372.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_378.Update();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_386.Update();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_397.Update();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_399.Update();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_401.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_403.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_405.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_407.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_411.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_412.Update();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_413.Update();
	}

	public void OnDestroy()
	{
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_52.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_85.OnDestroy();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_147.OnDestroy();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_217.OnDestroy();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_236.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_249.OnDestroy();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_252.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_270.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_301.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_303.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_363.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_365.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_366.OnDestroy();
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_372.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_378.OnDestroy();
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_386.OnDestroy();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_397.OnDestroy();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_399.OnDestroy();
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_401.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_403.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_405.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_407.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_411.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_412.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_413.OnDestroy();
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_52.Save_Out -= SubGraph_SaveLoadBool_Save_Out_52;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_52.Load_Out -= SubGraph_SaveLoadBool_Load_Out_52;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_52.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_52;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_85.Save_Out -= SubGraph_SaveLoadBool_Save_Out_85;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_85.Load_Out -= SubGraph_SaveLoadBool_Load_Out_85;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_85.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_85;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_102.Output1 -= uScriptCon_ManualSwitch_Output1_102;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_102.Output2 -= uScriptCon_ManualSwitch_Output2_102;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_102.Output3 -= uScriptCon_ManualSwitch_Output3_102;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_102.Output4 -= uScriptCon_ManualSwitch_Output4_102;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_102.Output5 -= uScriptCon_ManualSwitch_Output5_102;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_102.Output6 -= uScriptCon_ManualSwitch_Output6_102;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_102.Output7 -= uScriptCon_ManualSwitch_Output7_102;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_102.Output8 -= uScriptCon_ManualSwitch_Output8_102;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_147.Save_Out -= SubGraph_SaveLoadInt_Save_Out_147;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_147.Load_Out -= SubGraph_SaveLoadInt_Load_Out_147;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_147.Restart_Out -= SubGraph_SaveLoadInt_Restart_Out_147;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_181.Output1 -= uScriptCon_ManualSwitch_Output1_181;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_181.Output2 -= uScriptCon_ManualSwitch_Output2_181;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_181.Output3 -= uScriptCon_ManualSwitch_Output3_181;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_181.Output4 -= uScriptCon_ManualSwitch_Output4_181;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_181.Output5 -= uScriptCon_ManualSwitch_Output5_181;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_181.Output6 -= uScriptCon_ManualSwitch_Output6_181;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_181.Output7 -= uScriptCon_ManualSwitch_Output7_181;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_181.Output8 -= uScriptCon_ManualSwitch_Output8_181;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_217.Save_Out -= SubGraph_SaveLoadInt_Save_Out_217;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_217.Load_Out -= SubGraph_SaveLoadInt_Load_Out_217;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_217.Restart_Out -= SubGraph_SaveLoadInt_Restart_Out_217;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_236.Save_Out -= SubGraph_SaveLoadInt_Save_Out_236;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_236.Load_Out -= SubGraph_SaveLoadInt_Load_Out_236;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_236.Restart_Out -= SubGraph_SaveLoadInt_Restart_Out_236;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_245.Output1 -= uScriptCon_ManualSwitch_Output1_245;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_245.Output2 -= uScriptCon_ManualSwitch_Output2_245;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_245.Output3 -= uScriptCon_ManualSwitch_Output3_245;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_245.Output4 -= uScriptCon_ManualSwitch_Output4_245;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_245.Output5 -= uScriptCon_ManualSwitch_Output5_245;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_245.Output6 -= uScriptCon_ManualSwitch_Output6_245;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_245.Output7 -= uScriptCon_ManualSwitch_Output7_245;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_245.Output8 -= uScriptCon_ManualSwitch_Output8_245;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_249.Save_Out -= SubGraph_SaveLoadBool_Save_Out_249;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_249.Load_Out -= SubGraph_SaveLoadBool_Load_Out_249;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_249.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_249;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_252.Save_Out -= SubGraph_SaveLoadInt_Save_Out_252;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_252.Load_Out -= SubGraph_SaveLoadInt_Load_Out_252;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_252.Restart_Out -= SubGraph_SaveLoadInt_Restart_Out_252;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_270.Save_Out -= SubGraph_SaveLoadBool_Save_Out_270;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_270.Load_Out -= SubGraph_SaveLoadBool_Load_Out_270;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_270.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_270;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_301.Save_Out -= SubGraph_SaveLoadBool_Save_Out_301;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_301.Load_Out -= SubGraph_SaveLoadBool_Load_Out_301;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_301.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_301;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_303.Save_Out -= SubGraph_SaveLoadBool_Save_Out_303;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_303.Load_Out -= SubGraph_SaveLoadBool_Load_Out_303;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_303.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_303;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_363.Save_Out -= SubGraph_SaveLoadBool_Save_Out_363;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_363.Load_Out -= SubGraph_SaveLoadBool_Load_Out_363;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_363.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_363;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_365.Save_Out -= SubGraph_SaveLoadBool_Save_Out_365;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_365.Load_Out -= SubGraph_SaveLoadBool_Load_Out_365;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_365.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_365;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_366.Save_Out -= SubGraph_SaveLoadBool_Save_Out_366;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_366.Load_Out -= SubGraph_SaveLoadBool_Load_Out_366;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_366.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_366;
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_372.Out -= SubGraph_LoadObjectiveStates_Out_372;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_378.Save_Out -= SubGraph_SaveLoadBool_Save_Out_378;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_378.Load_Out -= SubGraph_SaveLoadBool_Load_Out_378;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_378.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_378;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_386.Out -= SubGraph_CompleteObjectiveStage_Out_386;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_389.Output1 -= uScriptCon_ManualSwitch_Output1_389;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_389.Output2 -= uScriptCon_ManualSwitch_Output2_389;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_389.Output3 -= uScriptCon_ManualSwitch_Output3_389;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_389.Output4 -= uScriptCon_ManualSwitch_Output4_389;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_389.Output5 -= uScriptCon_ManualSwitch_Output5_389;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_389.Output6 -= uScriptCon_ManualSwitch_Output6_389;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_389.Output7 -= uScriptCon_ManualSwitch_Output7_389;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_389.Output8 -= uScriptCon_ManualSwitch_Output8_389;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_397.Save_Out -= SubGraph_SaveLoadInt_Save_Out_397;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_397.Load_Out -= SubGraph_SaveLoadInt_Load_Out_397;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_397.Restart_Out -= SubGraph_SaveLoadInt_Restart_Out_397;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_399.Save_Out -= SubGraph_SaveLoadInt_Save_Out_399;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_399.Load_Out -= SubGraph_SaveLoadInt_Load_Out_399;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_399.Restart_Out -= SubGraph_SaveLoadInt_Restart_Out_399;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_401.Save_Out -= SubGraph_SaveLoadInt_Save_Out_401;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_401.Load_Out -= SubGraph_SaveLoadInt_Load_Out_401;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_401.Restart_Out -= SubGraph_SaveLoadInt_Restart_Out_401;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_403.Save_Out -= SubGraph_SaveLoadBool_Save_Out_403;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_403.Load_Out -= SubGraph_SaveLoadBool_Load_Out_403;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_403.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_403;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_405.Save_Out -= SubGraph_SaveLoadBool_Save_Out_405;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_405.Load_Out -= SubGraph_SaveLoadBool_Load_Out_405;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_405.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_405;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_407.Save_Out -= SubGraph_SaveLoadBool_Save_Out_407;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_407.Load_Out -= SubGraph_SaveLoadBool_Load_Out_407;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_407.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_407;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_411.Save_Out -= SubGraph_SaveLoadBool_Save_Out_411;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_411.Load_Out -= SubGraph_SaveLoadBool_Load_Out_411;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_411.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_411;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_412.Save_Out -= SubGraph_SaveLoadBool_Save_Out_412;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_412.Load_Out -= SubGraph_SaveLoadBool_Load_Out_412;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_412.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_412;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_413.Save_Out -= SubGraph_SaveLoadBool_Save_Out_413;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_413.Load_Out -= SubGraph_SaveLoadBool_Load_Out_413;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_413.Restart_Out -= SubGraph_SaveLoadBool_Restart_Out_413;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_437.Output1 -= uScriptCon_ManualSwitch_Output1_437;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_437.Output2 -= uScriptCon_ManualSwitch_Output2_437;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_437.Output3 -= uScriptCon_ManualSwitch_Output3_437;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_437.Output4 -= uScriptCon_ManualSwitch_Output4_437;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_437.Output5 -= uScriptCon_ManualSwitch_Output5_437;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_437.Output6 -= uScriptCon_ManualSwitch_Output6_437;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_437.Output7 -= uScriptCon_ManualSwitch_Output7_437;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_437.Output8 -= uScriptCon_ManualSwitch_Output8_437;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_494.Output1 -= uScriptCon_ManualSwitch_Output1_494;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_494.Output2 -= uScriptCon_ManualSwitch_Output2_494;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_494.Output3 -= uScriptCon_ManualSwitch_Output3_494;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_494.Output4 -= uScriptCon_ManualSwitch_Output4_494;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_494.Output5 -= uScriptCon_ManualSwitch_Output5_494;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_494.Output6 -= uScriptCon_ManualSwitch_Output6_494;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_494.Output7 -= uScriptCon_ManualSwitch_Output7_494;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_494.Output8 -= uScriptCon_ManualSwitch_Output8_494;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_528.Output1 -= uScriptCon_ManualSwitch_Output1_528;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_528.Output2 -= uScriptCon_ManualSwitch_Output2_528;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_528.Output3 -= uScriptCon_ManualSwitch_Output3_528;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_528.Output4 -= uScriptCon_ManualSwitch_Output4_528;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_528.Output5 -= uScriptCon_ManualSwitch_Output5_528;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_528.Output6 -= uScriptCon_ManualSwitch_Output6_528;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_528.Output7 -= uScriptCon_ManualSwitch_Output7_528;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_528.Output8 -= uScriptCon_ManualSwitch_Output8_528;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_543.Output1 -= uScriptCon_ManualSwitch_Output1_543;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_543.Output2 -= uScriptCon_ManualSwitch_Output2_543;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_543.Output3 -= uScriptCon_ManualSwitch_Output3_543;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_543.Output4 -= uScriptCon_ManualSwitch_Output4_543;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_543.Output5 -= uScriptCon_ManualSwitch_Output5_543;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_543.Output6 -= uScriptCon_ManualSwitch_Output6_543;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_543.Output7 -= uScriptCon_ManualSwitch_Output7_543;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_543.Output8 -= uScriptCon_ManualSwitch_Output8_543;
	}

	public void OnGUI()
	{
		logic_uScriptAct_PrintText_uScriptAct_PrintText_274.OnGUI();
	}

	private void Instance_OnUpdate_51(object o, EventArgs e)
	{
		Relay_OnUpdate_51();
	}

	private void Instance_OnSuspend_51(object o, EventArgs e)
	{
		Relay_OnSuspend_51();
	}

	private void Instance_OnResume_51(object o, EventArgs e)
	{
		Relay_OnResume_51();
	}

	private void Instance_SaveEvent_140(object o, EventArgs e)
	{
		Relay_SaveEvent_140();
	}

	private void Instance_LoadEvent_140(object o, EventArgs e)
	{
		Relay_LoadEvent_140();
	}

	private void Instance_RestartEvent_140(object o, EventArgs e)
	{
		Relay_RestartEvent_140();
	}

	private void SubGraph_SaveLoadBool_Save_Out_52(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_52 = e.boolean;
		local_CanPushButtons_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_52;
		Relay_Save_Out_52();
	}

	private void SubGraph_SaveLoadBool_Load_Out_52(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_52 = e.boolean;
		local_CanPushButtons_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_52;
		Relay_Load_Out_52();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_52(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_52 = e.boolean;
		local_CanPushButtons_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_52;
		Relay_Restart_Out_52();
	}

	private void SubGraph_SaveLoadBool_Save_Out_85(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_85 = e.boolean;
		local_msgIntro_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_85;
		Relay_Save_Out_85();
	}

	private void SubGraph_SaveLoadBool_Load_Out_85(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_85 = e.boolean;
		local_msgIntro_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_85;
		Relay_Load_Out_85();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_85(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_85 = e.boolean;
		local_msgIntro_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_85;
		Relay_Restart_Out_85();
	}

	private void uScriptCon_ManualSwitch_Output1_102(object o, EventArgs e)
	{
		Relay_Output1_102();
	}

	private void uScriptCon_ManualSwitch_Output2_102(object o, EventArgs e)
	{
		Relay_Output2_102();
	}

	private void uScriptCon_ManualSwitch_Output3_102(object o, EventArgs e)
	{
		Relay_Output3_102();
	}

	private void uScriptCon_ManualSwitch_Output4_102(object o, EventArgs e)
	{
		Relay_Output4_102();
	}

	private void uScriptCon_ManualSwitch_Output5_102(object o, EventArgs e)
	{
		Relay_Output5_102();
	}

	private void uScriptCon_ManualSwitch_Output6_102(object o, EventArgs e)
	{
		Relay_Output6_102();
	}

	private void uScriptCon_ManualSwitch_Output7_102(object o, EventArgs e)
	{
		Relay_Output7_102();
	}

	private void uScriptCon_ManualSwitch_Output8_102(object o, EventArgs e)
	{
		Relay_Output8_102();
	}

	private void SubGraph_SaveLoadInt_Save_Out_147(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_147 = e.integer;
		local_QuestionTwo_System_Int32 = logic_SubGraph_SaveLoadInt_integer_147;
		Relay_Save_Out_147();
	}

	private void SubGraph_SaveLoadInt_Load_Out_147(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_147 = e.integer;
		local_QuestionTwo_System_Int32 = logic_SubGraph_SaveLoadInt_integer_147;
		Relay_Load_Out_147();
	}

	private void SubGraph_SaveLoadInt_Restart_Out_147(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_147 = e.integer;
		local_QuestionTwo_System_Int32 = logic_SubGraph_SaveLoadInt_integer_147;
		Relay_Restart_Out_147();
	}

	private void uScriptCon_ManualSwitch_Output1_181(object o, EventArgs e)
	{
		Relay_Output1_181();
	}

	private void uScriptCon_ManualSwitch_Output2_181(object o, EventArgs e)
	{
		Relay_Output2_181();
	}

	private void uScriptCon_ManualSwitch_Output3_181(object o, EventArgs e)
	{
		Relay_Output3_181();
	}

	private void uScriptCon_ManualSwitch_Output4_181(object o, EventArgs e)
	{
		Relay_Output4_181();
	}

	private void uScriptCon_ManualSwitch_Output5_181(object o, EventArgs e)
	{
		Relay_Output5_181();
	}

	private void uScriptCon_ManualSwitch_Output6_181(object o, EventArgs e)
	{
		Relay_Output6_181();
	}

	private void uScriptCon_ManualSwitch_Output7_181(object o, EventArgs e)
	{
		Relay_Output7_181();
	}

	private void uScriptCon_ManualSwitch_Output8_181(object o, EventArgs e)
	{
		Relay_Output8_181();
	}

	private void SubGraph_SaveLoadInt_Save_Out_217(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_217 = e.integer;
		local_Stage_System_Int32 = logic_SubGraph_SaveLoadInt_integer_217;
		Relay_Save_Out_217();
	}

	private void SubGraph_SaveLoadInt_Load_Out_217(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_217 = e.integer;
		local_Stage_System_Int32 = logic_SubGraph_SaveLoadInt_integer_217;
		Relay_Load_Out_217();
	}

	private void SubGraph_SaveLoadInt_Restart_Out_217(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_217 = e.integer;
		local_Stage_System_Int32 = logic_SubGraph_SaveLoadInt_integer_217;
		Relay_Restart_Out_217();
	}

	private void SubGraph_SaveLoadInt_Save_Out_236(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_236 = e.integer;
		local_QuestionOne_System_Int32 = logic_SubGraph_SaveLoadInt_integer_236;
		Relay_Save_Out_236();
	}

	private void SubGraph_SaveLoadInt_Load_Out_236(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_236 = e.integer;
		local_QuestionOne_System_Int32 = logic_SubGraph_SaveLoadInt_integer_236;
		Relay_Load_Out_236();
	}

	private void SubGraph_SaveLoadInt_Restart_Out_236(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_236 = e.integer;
		local_QuestionOne_System_Int32 = logic_SubGraph_SaveLoadInt_integer_236;
		Relay_Restart_Out_236();
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

	private void SubGraph_SaveLoadBool_Save_Out_249(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_249 = e.boolean;
		local_Initialize_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_249;
		Relay_Save_Out_249();
	}

	private void SubGraph_SaveLoadBool_Load_Out_249(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_249 = e.boolean;
		local_Initialize_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_249;
		Relay_Load_Out_249();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_249(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_249 = e.boolean;
		local_Initialize_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_249;
		Relay_Restart_Out_249();
	}

	private void SubGraph_SaveLoadInt_Save_Out_252(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_252 = e.integer;
		local_QuestionThree_System_Int32 = logic_SubGraph_SaveLoadInt_integer_252;
		Relay_Save_Out_252();
	}

	private void SubGraph_SaveLoadInt_Load_Out_252(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_252 = e.integer;
		local_QuestionThree_System_Int32 = logic_SubGraph_SaveLoadInt_integer_252;
		Relay_Load_Out_252();
	}

	private void SubGraph_SaveLoadInt_Restart_Out_252(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_252 = e.integer;
		local_QuestionThree_System_Int32 = logic_SubGraph_SaveLoadInt_integer_252;
		Relay_Restart_Out_252();
	}

	private void SubGraph_SaveLoadBool_Save_Out_270(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_270 = e.boolean;
		local_Q1EnemyAlive_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_270;
		Relay_Save_Out_270();
	}

	private void SubGraph_SaveLoadBool_Load_Out_270(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_270 = e.boolean;
		local_Q1EnemyAlive_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_270;
		Relay_Load_Out_270();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_270(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_270 = e.boolean;
		local_Q1EnemyAlive_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_270;
		Relay_Restart_Out_270();
	}

	private void SubGraph_SaveLoadBool_Save_Out_301(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_301 = e.boolean;
		local_Q2EnemyAlive_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_301;
		Relay_Save_Out_301();
	}

	private void SubGraph_SaveLoadBool_Load_Out_301(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_301 = e.boolean;
		local_Q2EnemyAlive_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_301;
		Relay_Load_Out_301();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_301(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_301 = e.boolean;
		local_Q2EnemyAlive_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_301;
		Relay_Restart_Out_301();
	}

	private void SubGraph_SaveLoadBool_Save_Out_303(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_303 = e.boolean;
		local_Q3EnemyAlive_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_303;
		Relay_Save_Out_303();
	}

	private void SubGraph_SaveLoadBool_Load_Out_303(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_303 = e.boolean;
		local_Q3EnemyAlive_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_303;
		Relay_Load_Out_303();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_303(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_303 = e.boolean;
		local_Q3EnemyAlive_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_303;
		Relay_Restart_Out_303();
	}

	private void SubGraph_SaveLoadBool_Save_Out_363(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_363 = e.boolean;
		local_Question01WrongAnswers_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_363;
		Relay_Save_Out_363();
	}

	private void SubGraph_SaveLoadBool_Load_Out_363(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_363 = e.boolean;
		local_Question01WrongAnswers_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_363;
		Relay_Load_Out_363();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_363(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_363 = e.boolean;
		local_Question01WrongAnswers_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_363;
		Relay_Restart_Out_363();
	}

	private void SubGraph_SaveLoadBool_Save_Out_365(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_365 = e.boolean;
		local_Question02WrongAnswers_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_365;
		Relay_Save_Out_365();
	}

	private void SubGraph_SaveLoadBool_Load_Out_365(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_365 = e.boolean;
		local_Question02WrongAnswers_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_365;
		Relay_Load_Out_365();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_365(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_365 = e.boolean;
		local_Question02WrongAnswers_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_365;
		Relay_Restart_Out_365();
	}

	private void SubGraph_SaveLoadBool_Save_Out_366(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_366 = e.boolean;
		local_Question03WrongAnswers_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_366;
		Relay_Save_Out_366();
	}

	private void SubGraph_SaveLoadBool_Load_Out_366(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_366 = e.boolean;
		local_Question03WrongAnswers_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_366;
		Relay_Load_Out_366();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_366(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_366 = e.boolean;
		local_Question03WrongAnswers_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_366;
		Relay_Restart_Out_366();
	}

	private void SubGraph_LoadObjectiveStates_Out_372(object o, SubGraph_LoadObjectiveStates.LogicEventArgs e)
	{
		Relay_Out_372();
	}

	private void SubGraph_SaveLoadBool_Save_Out_378(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_378 = e.boolean;
		local_msgBaseFoundShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_378;
		Relay_Save_Out_378();
	}

	private void SubGraph_SaveLoadBool_Load_Out_378(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_378 = e.boolean;
		local_msgBaseFoundShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_378;
		Relay_Load_Out_378();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_378(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_378 = e.boolean;
		local_msgBaseFoundShown_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_378;
		Relay_Restart_Out_378();
	}

	private void SubGraph_CompleteObjectiveStage_Out_386(object o, SubGraph_CompleteObjectiveStage.LogicEventArgs e)
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_386 = e.objectiveStage;
		local_Stage_System_Int32 = logic_SubGraph_CompleteObjectiveStage_objectiveStage_386;
		Relay_Out_386();
	}

	private void uScriptCon_ManualSwitch_Output1_389(object o, EventArgs e)
	{
		Relay_Output1_389();
	}

	private void uScriptCon_ManualSwitch_Output2_389(object o, EventArgs e)
	{
		Relay_Output2_389();
	}

	private void uScriptCon_ManualSwitch_Output3_389(object o, EventArgs e)
	{
		Relay_Output3_389();
	}

	private void uScriptCon_ManualSwitch_Output4_389(object o, EventArgs e)
	{
		Relay_Output4_389();
	}

	private void uScriptCon_ManualSwitch_Output5_389(object o, EventArgs e)
	{
		Relay_Output5_389();
	}

	private void uScriptCon_ManualSwitch_Output6_389(object o, EventArgs e)
	{
		Relay_Output6_389();
	}

	private void uScriptCon_ManualSwitch_Output7_389(object o, EventArgs e)
	{
		Relay_Output7_389();
	}

	private void uScriptCon_ManualSwitch_Output8_389(object o, EventArgs e)
	{
		Relay_Output8_389();
	}

	private void SubGraph_SaveLoadInt_Save_Out_397(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_397 = e.integer;
		local_QuestionFour_System_Int32 = logic_SubGraph_SaveLoadInt_integer_397;
		Relay_Save_Out_397();
	}

	private void SubGraph_SaveLoadInt_Load_Out_397(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_397 = e.integer;
		local_QuestionFour_System_Int32 = logic_SubGraph_SaveLoadInt_integer_397;
		Relay_Load_Out_397();
	}

	private void SubGraph_SaveLoadInt_Restart_Out_397(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_397 = e.integer;
		local_QuestionFour_System_Int32 = logic_SubGraph_SaveLoadInt_integer_397;
		Relay_Restart_Out_397();
	}

	private void SubGraph_SaveLoadInt_Save_Out_399(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_399 = e.integer;
		local_QuestionFive_System_Int32 = logic_SubGraph_SaveLoadInt_integer_399;
		Relay_Save_Out_399();
	}

	private void SubGraph_SaveLoadInt_Load_Out_399(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_399 = e.integer;
		local_QuestionFive_System_Int32 = logic_SubGraph_SaveLoadInt_integer_399;
		Relay_Load_Out_399();
	}

	private void SubGraph_SaveLoadInt_Restart_Out_399(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_399 = e.integer;
		local_QuestionFive_System_Int32 = logic_SubGraph_SaveLoadInt_integer_399;
		Relay_Restart_Out_399();
	}

	private void SubGraph_SaveLoadInt_Save_Out_401(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_401 = e.integer;
		local_QuestionSix_System_Int32 = logic_SubGraph_SaveLoadInt_integer_401;
		Relay_Save_Out_401();
	}

	private void SubGraph_SaveLoadInt_Load_Out_401(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_401 = e.integer;
		local_QuestionSix_System_Int32 = logic_SubGraph_SaveLoadInt_integer_401;
		Relay_Load_Out_401();
	}

	private void SubGraph_SaveLoadInt_Restart_Out_401(object o, SubGraph_SaveLoadInt.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadInt_integer_401 = e.integer;
		local_QuestionSix_System_Int32 = logic_SubGraph_SaveLoadInt_integer_401;
		Relay_Restart_Out_401();
	}

	private void SubGraph_SaveLoadBool_Save_Out_403(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_403 = e.boolean;
		local_Q4EnemyAlive_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_403;
		Relay_Save_Out_403();
	}

	private void SubGraph_SaveLoadBool_Load_Out_403(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_403 = e.boolean;
		local_Q4EnemyAlive_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_403;
		Relay_Load_Out_403();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_403(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_403 = e.boolean;
		local_Q4EnemyAlive_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_403;
		Relay_Restart_Out_403();
	}

	private void SubGraph_SaveLoadBool_Save_Out_405(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_405 = e.boolean;
		local_Q5EnemyAlive_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_405;
		Relay_Save_Out_405();
	}

	private void SubGraph_SaveLoadBool_Load_Out_405(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_405 = e.boolean;
		local_Q5EnemyAlive_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_405;
		Relay_Load_Out_405();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_405(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_405 = e.boolean;
		local_Q5EnemyAlive_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_405;
		Relay_Restart_Out_405();
	}

	private void SubGraph_SaveLoadBool_Save_Out_407(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_407 = e.boolean;
		local_Q6EnemyAlive_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_407;
		Relay_Save_Out_407();
	}

	private void SubGraph_SaveLoadBool_Load_Out_407(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_407 = e.boolean;
		local_Q6EnemyAlive_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_407;
		Relay_Load_Out_407();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_407(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_407 = e.boolean;
		local_Q6EnemyAlive_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_407;
		Relay_Restart_Out_407();
	}

	private void SubGraph_SaveLoadBool_Save_Out_411(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_411 = e.boolean;
		local_Question04WrongAnswers_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_411;
		Relay_Save_Out_411();
	}

	private void SubGraph_SaveLoadBool_Load_Out_411(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_411 = e.boolean;
		local_Question04WrongAnswers_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_411;
		Relay_Load_Out_411();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_411(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_411 = e.boolean;
		local_Question04WrongAnswers_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_411;
		Relay_Restart_Out_411();
	}

	private void SubGraph_SaveLoadBool_Save_Out_412(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_412 = e.boolean;
		local_Question06WrongAnswers_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_412;
		Relay_Save_Out_412();
	}

	private void SubGraph_SaveLoadBool_Load_Out_412(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_412 = e.boolean;
		local_Question06WrongAnswers_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_412;
		Relay_Load_Out_412();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_412(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_412 = e.boolean;
		local_Question06WrongAnswers_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_412;
		Relay_Restart_Out_412();
	}

	private void SubGraph_SaveLoadBool_Save_Out_413(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_413 = e.boolean;
		local_Question05WrongAnswers_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_413;
		Relay_Save_Out_413();
	}

	private void SubGraph_SaveLoadBool_Load_Out_413(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_413 = e.boolean;
		local_Question05WrongAnswers_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_413;
		Relay_Load_Out_413();
	}

	private void SubGraph_SaveLoadBool_Restart_Out_413(object o, SubGraph_SaveLoadBool.LogicEventArgs e)
	{
		logic_SubGraph_SaveLoadBool_boolean_413 = e.boolean;
		local_Question05WrongAnswers_System_Boolean = logic_SubGraph_SaveLoadBool_boolean_413;
		Relay_Restart_Out_413();
	}

	private void uScriptCon_ManualSwitch_Output1_437(object o, EventArgs e)
	{
		Relay_Output1_437();
	}

	private void uScriptCon_ManualSwitch_Output2_437(object o, EventArgs e)
	{
		Relay_Output2_437();
	}

	private void uScriptCon_ManualSwitch_Output3_437(object o, EventArgs e)
	{
		Relay_Output3_437();
	}

	private void uScriptCon_ManualSwitch_Output4_437(object o, EventArgs e)
	{
		Relay_Output4_437();
	}

	private void uScriptCon_ManualSwitch_Output5_437(object o, EventArgs e)
	{
		Relay_Output5_437();
	}

	private void uScriptCon_ManualSwitch_Output6_437(object o, EventArgs e)
	{
		Relay_Output6_437();
	}

	private void uScriptCon_ManualSwitch_Output7_437(object o, EventArgs e)
	{
		Relay_Output7_437();
	}

	private void uScriptCon_ManualSwitch_Output8_437(object o, EventArgs e)
	{
		Relay_Output8_437();
	}

	private void uScriptCon_ManualSwitch_Output1_494(object o, EventArgs e)
	{
		Relay_Output1_494();
	}

	private void uScriptCon_ManualSwitch_Output2_494(object o, EventArgs e)
	{
		Relay_Output2_494();
	}

	private void uScriptCon_ManualSwitch_Output3_494(object o, EventArgs e)
	{
		Relay_Output3_494();
	}

	private void uScriptCon_ManualSwitch_Output4_494(object o, EventArgs e)
	{
		Relay_Output4_494();
	}

	private void uScriptCon_ManualSwitch_Output5_494(object o, EventArgs e)
	{
		Relay_Output5_494();
	}

	private void uScriptCon_ManualSwitch_Output6_494(object o, EventArgs e)
	{
		Relay_Output6_494();
	}

	private void uScriptCon_ManualSwitch_Output7_494(object o, EventArgs e)
	{
		Relay_Output7_494();
	}

	private void uScriptCon_ManualSwitch_Output8_494(object o, EventArgs e)
	{
		Relay_Output8_494();
	}

	private void uScriptCon_ManualSwitch_Output1_528(object o, EventArgs e)
	{
		Relay_Output1_528();
	}

	private void uScriptCon_ManualSwitch_Output2_528(object o, EventArgs e)
	{
		Relay_Output2_528();
	}

	private void uScriptCon_ManualSwitch_Output3_528(object o, EventArgs e)
	{
		Relay_Output3_528();
	}

	private void uScriptCon_ManualSwitch_Output4_528(object o, EventArgs e)
	{
		Relay_Output4_528();
	}

	private void uScriptCon_ManualSwitch_Output5_528(object o, EventArgs e)
	{
		Relay_Output5_528();
	}

	private void uScriptCon_ManualSwitch_Output6_528(object o, EventArgs e)
	{
		Relay_Output6_528();
	}

	private void uScriptCon_ManualSwitch_Output7_528(object o, EventArgs e)
	{
		Relay_Output7_528();
	}

	private void uScriptCon_ManualSwitch_Output8_528(object o, EventArgs e)
	{
		Relay_Output8_528();
	}

	private void uScriptCon_ManualSwitch_Output1_543(object o, EventArgs e)
	{
		Relay_Output1_543();
	}

	private void uScriptCon_ManualSwitch_Output2_543(object o, EventArgs e)
	{
		Relay_Output2_543();
	}

	private void uScriptCon_ManualSwitch_Output3_543(object o, EventArgs e)
	{
		Relay_Output3_543();
	}

	private void uScriptCon_ManualSwitch_Output4_543(object o, EventArgs e)
	{
		Relay_Output4_543();
	}

	private void uScriptCon_ManualSwitch_Output5_543(object o, EventArgs e)
	{
		Relay_Output5_543();
	}

	private void uScriptCon_ManualSwitch_Output6_543(object o, EventArgs e)
	{
		Relay_Output6_543();
	}

	private void uScriptCon_ManualSwitch_Output7_543(object o, EventArgs e)
	{
		Relay_Output7_543();
	}

	private void uScriptCon_ManualSwitch_Output8_543(object o, EventArgs e)
	{
		Relay_Output8_543();
	}

	private void Relay_In_9()
	{
		logic_uScriptCon_CompareBool_Bool_9 = local_NearNPC_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_9.In(logic_uScriptCon_CompareBool_Bool_9);
		if (logic_uScriptCon_CompareBool_uScriptCon_CompareBool_9.True)
		{
			Relay_In_251();
		}
	}

	private void Relay_In_11()
	{
		logic_uScript_SetCustomRadarTeamID_tech_11 = local_ButtonBase2Tech_Tank;
		logic_uScript_SetCustomRadarTeamID_uScript_SetCustomRadarTeamID_11.In(logic_uScript_SetCustomRadarTeamID_tech_11, logic_uScript_SetCustomRadarTeamID_radarTeamID_11);
		if (logic_uScript_SetCustomRadarTeamID_uScript_SetCustomRadarTeamID_11.Out)
		{
			Relay_In_188();
		}
	}

	private void Relay_In_12()
	{
		logic_uScriptCon_CompareBool_Bool_12 = local_Question06WrongAnswers_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_12.In(logic_uScriptCon_CompareBool_Bool_12);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_12.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_12.False;
		if (num)
		{
			Relay_In_150();
		}
		if (flag)
		{
			Relay_In_291();
		}
	}

	private void Relay_In_13()
	{
		logic_uScript_SetTankInvulnerable_tank_13 = local_ButtonBase2Tech_Tank;
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_13.In(logic_uScript_SetTankInvulnerable_invulnerable_13, logic_uScript_SetTankInvulnerable_tank_13);
		if (logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_13.Out)
		{
			Relay_In_11();
		}
	}

	private void Relay_In_14()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_14.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_14.Out)
		{
			Relay_In_151();
		}
	}

	private void Relay_In_16()
	{
		logic_uScript_SetCustomRadarTeamID_tech_16 = local_NPCTech_Tank;
		logic_uScript_SetCustomRadarTeamID_uScript_SetCustomRadarTeamID_16.In(logic_uScript_SetCustomRadarTeamID_tech_16, logic_uScript_SetCustomRadarTeamID_radarTeamID_16);
		if (logic_uScript_SetCustomRadarTeamID_uScript_SetCustomRadarTeamID_16.Out)
		{
			Relay_In_241();
		}
	}

	private void Relay_In_19()
	{
		logic_uScript_AddMessage_messageData_19 = msg19QuestionSixCorrect;
		logic_uScript_AddMessage_speaker_19 = messageSpeaker;
		logic_uScript_AddMessage_Return_19 = logic_uScript_AddMessage_uScript_AddMessage_19.In(logic_uScript_AddMessage_messageData_19, logic_uScript_AddMessage_speaker_19);
		if (logic_uScript_AddMessage_uScript_AddMessage_19.Out)
		{
			Relay_In_1053();
		}
	}

	private void Relay_True_22()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_22.True(out logic_uScriptAct_SetBool_Target_22);
		local_Question01WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_22;
	}

	private void Relay_False_22()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_22.False(out logic_uScriptAct_SetBool_Target_22);
		local_Question01WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_22;
	}

	private void Relay_In_30()
	{
		logic_uScript_LockTechInteraction_tech_30 = local_ButtonBase4Tech_Tank;
		int num = 0;
		if (logic_uScript_LockTechInteraction_excludedBlocks_30.Length <= num)
		{
			Array.Resize(ref logic_uScript_LockTechInteraction_excludedBlocks_30, num + 1);
		}
		logic_uScript_LockTechInteraction_excludedBlocks_30[num++] = blockTypeButton4;
		int num2 = 0;
		if (logic_uScript_LockTechInteraction_excludedUniqueBlocks_30.Length <= num2)
		{
			Array.Resize(ref logic_uScript_LockTechInteraction_excludedUniqueBlocks_30, num2 + 1);
		}
		logic_uScript_LockTechInteraction_excludedUniqueBlocks_30[num2++] = local_ButtonBlock4_TankBlock;
		logic_uScript_LockTechInteraction_uScript_LockTechInteraction_30.In(logic_uScript_LockTechInteraction_tech_30, logic_uScript_LockTechInteraction_excludedBlocks_30, logic_uScript_LockTechInteraction_excludedUniqueBlocks_30);
		if (logic_uScript_LockTechInteraction_uScript_LockTechInteraction_30.Out)
		{
			Relay_In_375();
		}
	}

	private void Relay_In_31()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_31.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_31.Out)
		{
			Relay_In_231();
		}
	}

	private void Relay_In_37()
	{
		logic_uScript_LockTechInteraction_tech_37 = local_ButtonBase3Tech_Tank;
		int num = 0;
		if (logic_uScript_LockTechInteraction_excludedBlocks_37.Length <= num)
		{
			Array.Resize(ref logic_uScript_LockTechInteraction_excludedBlocks_37, num + 1);
		}
		logic_uScript_LockTechInteraction_excludedBlocks_37[num++] = blockTypeButton3;
		int num2 = 0;
		if (logic_uScript_LockTechInteraction_excludedUniqueBlocks_37.Length <= num2)
		{
			Array.Resize(ref logic_uScript_LockTechInteraction_excludedUniqueBlocks_37, num2 + 1);
		}
		logic_uScript_LockTechInteraction_excludedUniqueBlocks_37[num2++] = local_ButtonBlock3_TankBlock;
		logic_uScript_LockTechInteraction_uScript_LockTechInteraction_37.In(logic_uScript_LockTechInteraction_tech_37, logic_uScript_LockTechInteraction_excludedBlocks_37, logic_uScript_LockTechInteraction_excludedUniqueBlocks_37);
		if (logic_uScript_LockTechInteraction_uScript_LockTechInteraction_37.Out)
		{
			Relay_In_30();
		}
	}

	private void Relay_In_38()
	{
		logic_uScriptCon_CompareInt_A_38 = local_Button2Value_System_Int32;
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_38.In(logic_uScriptCon_CompareInt_A_38, logic_uScriptCon_CompareInt_B_38);
		bool greaterThan = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_38.GreaterThan;
		bool lessThanOrEqualTo = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_38.LessThanOrEqualTo;
		if (greaterThan)
		{
			Relay_In_578();
		}
		if (lessThanOrEqualTo)
		{
			Relay_In_176();
		}
	}

	private void Relay_In_41()
	{
		logic_uScript_SetTankInvulnerable_tank_41 = local_ButtonBase4Tech_Tank;
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_41.In(logic_uScript_SetTankInvulnerable_invulnerable_41, logic_uScript_SetTankInvulnerable_tank_41);
		if (logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_41.Out)
		{
			Relay_In_81();
		}
	}

	private void Relay_Pause_49()
	{
		logic_uScript_PausePopulation_uScript_PausePopulation_49.Pause();
		if (logic_uScript_PausePopulation_uScript_PausePopulation_49.Out)
		{
			Relay_In_80();
		}
	}

	private void Relay_UnPause_49()
	{
		logic_uScript_PausePopulation_uScript_PausePopulation_49.UnPause();
		if (logic_uScript_PausePopulation_uScript_PausePopulation_49.Out)
		{
			Relay_In_80();
		}
	}

	private void Relay_In_50()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_50.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_50.Out)
		{
			Relay_In_209();
		}
	}

	private void Relay_OnUpdate_51()
	{
		Relay_In_208();
	}

	private void Relay_OnSuspend_51()
	{
	}

	private void Relay_OnResume_51()
	{
	}

	private void Relay_Save_Out_52()
	{
		Relay_Save_270();
	}

	private void Relay_Load_Out_52()
	{
		Relay_Load_270();
	}

	private void Relay_Restart_Out_52()
	{
		Relay_Set_False_270();
	}

	private void Relay_Save_52()
	{
		logic_SubGraph_SaveLoadBool_boolean_52 = local_CanPushButtons_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_52 = local_CanPushButtons_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_52.Save(ref logic_SubGraph_SaveLoadBool_boolean_52, logic_SubGraph_SaveLoadBool_boolAsVariable_52, logic_SubGraph_SaveLoadBool_uniqueID_52);
	}

	private void Relay_Load_52()
	{
		logic_SubGraph_SaveLoadBool_boolean_52 = local_CanPushButtons_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_52 = local_CanPushButtons_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_52.Load(ref logic_SubGraph_SaveLoadBool_boolean_52, logic_SubGraph_SaveLoadBool_boolAsVariable_52, logic_SubGraph_SaveLoadBool_uniqueID_52);
	}

	private void Relay_Set_True_52()
	{
		logic_SubGraph_SaveLoadBool_boolean_52 = local_CanPushButtons_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_52 = local_CanPushButtons_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_52.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_52, logic_SubGraph_SaveLoadBool_boolAsVariable_52, logic_SubGraph_SaveLoadBool_uniqueID_52);
	}

	private void Relay_Set_False_52()
	{
		logic_SubGraph_SaveLoadBool_boolean_52 = local_CanPushButtons_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_52 = local_CanPushButtons_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_52.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_52, logic_SubGraph_SaveLoadBool_boolAsVariable_52, logic_SubGraph_SaveLoadBool_uniqueID_52);
	}

	private void Relay_In_53()
	{
		logic_uScript_LockTechInteraction_tech_53 = local_ButtonBase2Tech_Tank;
		int num = 0;
		if (logic_uScript_LockTechInteraction_excludedBlocks_53.Length <= num)
		{
			Array.Resize(ref logic_uScript_LockTechInteraction_excludedBlocks_53, num + 1);
		}
		logic_uScript_LockTechInteraction_excludedBlocks_53[num++] = blockTypeButton2;
		int num2 = 0;
		if (logic_uScript_LockTechInteraction_excludedUniqueBlocks_53.Length <= num2)
		{
			Array.Resize(ref logic_uScript_LockTechInteraction_excludedUniqueBlocks_53, num2 + 1);
		}
		logic_uScript_LockTechInteraction_excludedUniqueBlocks_53[num2++] = local_ButtonBlock2_TankBlock;
		logic_uScript_LockTechInteraction_uScript_LockTechInteraction_53.In(logic_uScript_LockTechInteraction_tech_53, logic_uScript_LockTechInteraction_excludedBlocks_53, logic_uScript_LockTechInteraction_excludedUniqueBlocks_53);
		if (logic_uScript_LockTechInteraction_uScript_LockTechInteraction_53.Out)
		{
			Relay_In_37();
		}
	}

	private void Relay_In_55()
	{
		logic_uScript_LockTechInteraction_tech_55 = local_NPCTech_Tank;
		logic_uScript_LockTechInteraction_uScript_LockTechInteraction_55.In(logic_uScript_LockTechInteraction_tech_55, logic_uScript_LockTechInteraction_excludedBlocks_55, logic_uScript_LockTechInteraction_excludedUniqueBlocks_55);
		if (logic_uScript_LockTechInteraction_uScript_LockTechInteraction_55.Out)
		{
			Relay_In_16();
		}
	}

	private void Relay_In_61()
	{
		logic_uScript_LockTech_tech_61 = local_ButtonBase3Tech_Tank;
		logic_uScript_LockTech_uScript_LockTech_61.In(logic_uScript_LockTech_tech_61, logic_uScript_LockTech_lockType_61);
		if (logic_uScript_LockTech_uScript_LockTech_61.Out)
		{
			Relay_In_260();
		}
	}

	private void Relay_In_62()
	{
		logic_uScriptCon_CompareInt_A_62 = local_Button4Value_System_Int32;
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_62.In(logic_uScriptCon_CompareInt_A_62, logic_uScriptCon_CompareInt_B_62);
		if (logic_uScriptCon_CompareInt_uScriptCon_CompareInt_62.GreaterThan)
		{
			Relay_In_610();
		}
	}

	private void Relay_True_63()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_63.True(out logic_uScriptAct_SetBool_Target_63);
		local_Initialize_System_Boolean = logic_uScriptAct_SetBool_Target_63;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_63.Out)
		{
			Relay_InitialSpawn_66();
		}
	}

	private void Relay_False_63()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_63.False(out logic_uScriptAct_SetBool_Target_63);
		local_Initialize_System_Boolean = logic_uScriptAct_SetBool_Target_63;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_63.Out)
		{
			Relay_InitialSpawn_66();
		}
	}

	private void Relay_In_64()
	{
		logic_uScriptCon_CompareInt_A_64 = local_Button1Value_System_Int32;
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_64.In(logic_uScriptCon_CompareInt_A_64, logic_uScriptCon_CompareInt_B_64);
		bool greaterThan = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_64.GreaterThan;
		bool lessThanOrEqualTo = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_64.LessThanOrEqualTo;
		if (greaterThan)
		{
			Relay_In_984();
		}
		if (lessThanOrEqualTo)
		{
			Relay_In_992();
		}
	}

	private void Relay_In_65()
	{
		logic_uScript_SetCustomRadarTeamID_tech_65 = local_ButtonBase3Tech_Tank;
		logic_uScript_SetCustomRadarTeamID_uScript_SetCustomRadarTeamID_65.In(logic_uScript_SetCustomRadarTeamID_tech_65, logic_uScript_SetCustomRadarTeamID_radarTeamID_65);
		if (logic_uScript_SetCustomRadarTeamID_uScript_SetCustomRadarTeamID_65.Out)
		{
			Relay_In_104();
		}
	}

	private void Relay_InitialSpawn_66()
	{
		int num = 0;
		Array array = buttonbase1SpawnData;
		if (logic_uScript_SpawnTechsFromData_spawnData_66.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_66, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SpawnTechsFromData_spawnData_66, num, array.Length);
		num += array.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_66 = owner_Connection_58;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_66.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_66, logic_uScript_SpawnTechsFromData_ownerNode_66, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_66, logic_uScript_SpawnTechsFromData_allowResurrection_66);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_66.Out)
		{
			Relay_InitialSpawn_243();
		}
	}

	private void Relay_In_67()
	{
		logic_uScript_Wait_seconds_67 = WaitingTime;
		logic_uScript_Wait_uScript_Wait_67.In(logic_uScript_Wait_seconds_67, logic_uScript_Wait_repeat_67);
		if (logic_uScript_Wait_uScript_Wait_67.Waited)
		{
			Relay_False_109();
		}
	}

	private void Relay_In_71()
	{
		logic_uScriptCon_CompareInt_A_71 = local_Button3Value_System_Int32;
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_71.In(logic_uScriptCon_CompareInt_A_71, logic_uScriptCon_CompareInt_B_71);
		bool greaterThan = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_71.GreaterThan;
		bool lessThanOrEqualTo = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_71.LessThanOrEqualTo;
		if (greaterThan)
		{
			Relay_In_592();
		}
		if (lessThanOrEqualTo)
		{
			Relay_In_156();
		}
	}

	private void Relay_In_73()
	{
		logic_uScript_LockTechInteraction_tech_73 = local_ButtonBase3Tech_Tank;
		logic_uScript_LockTechInteraction_uScript_LockTechInteraction_73.In(logic_uScript_LockTechInteraction_tech_73, logic_uScript_LockTechInteraction_excludedBlocks_73, logic_uScript_LockTechInteraction_excludedUniqueBlocks_73);
		if (logic_uScript_LockTechInteraction_uScript_LockTechInteraction_73.Out)
		{
			Relay_In_192();
		}
	}

	private void Relay_In_75()
	{
		logic_uScript_AddMessage_messageData_75 = msg07QuestionTwoCorrect;
		logic_uScript_AddMessage_speaker_75 = messageSpeaker;
		logic_uScript_AddMessage_Return_75 = logic_uScript_AddMessage_uScript_AddMessage_75.In(logic_uScript_AddMessage_messageData_75, logic_uScript_AddMessage_speaker_75);
		if (logic_uScript_AddMessage_uScript_AddMessage_75.Out)
		{
			Relay_In_1045();
		}
	}

	private void Relay_True_76()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_76.True(out logic_uScriptAct_SetBool_Target_76);
		local_CanPushButtons_System_Boolean = logic_uScriptAct_SetBool_Target_76;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_76.Out)
		{
			Relay_In_549();
		}
	}

	private void Relay_False_76()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_76.False(out logic_uScriptAct_SetBool_Target_76);
		local_CanPushButtons_System_Boolean = logic_uScriptAct_SetBool_Target_76;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_76.Out)
		{
			Relay_In_549();
		}
	}

	private void Relay_InitialSpawn_78()
	{
		int num = 0;
		Array array = buttonbase3SpawnData;
		if (logic_uScript_SpawnTechsFromData_spawnData_78.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_78, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SpawnTechsFromData_spawnData_78, num, array.Length);
		num += array.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_78 = owner_Connection_56;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_78.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_78, logic_uScript_SpawnTechsFromData_ownerNode_78, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_78, logic_uScript_SpawnTechsFromData_allowResurrection_78);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_78.Out)
		{
			Relay_InitialSpawn_224();
		}
	}

	private void Relay_In_80()
	{
		logic_uScript_ClearOnScreenMessagesWithTag_tag_80 = messageTag;
		logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_80.In(logic_uScript_ClearOnScreenMessagesWithTag_tag_80, logic_uScript_ClearOnScreenMessagesWithTag_clearCurrent_80);
		if (logic_uScript_ClearOnScreenMessagesWithTag_uScript_ClearOnScreenMessagesWithTag_80.Out)
		{
			Relay_In_9();
		}
	}

	private void Relay_In_81()
	{
		logic_uScript_SetCustomRadarTeamID_tech_81 = local_ButtonBase4Tech_Tank;
		logic_uScript_SetCustomRadarTeamID_uScript_SetCustomRadarTeamID_81.In(logic_uScript_SetCustomRadarTeamID_tech_81, logic_uScript_SetCustomRadarTeamID_radarTeamID_81);
		if (logic_uScript_SetCustomRadarTeamID_uScript_SetCustomRadarTeamID_81.Out)
		{
			Relay_In_248();
		}
	}

	private void Relay_Save_Out_85()
	{
		Relay_Save_378();
	}

	private void Relay_Load_Out_85()
	{
		Relay_Load_378();
	}

	private void Relay_Restart_Out_85()
	{
		Relay_Set_False_378();
	}

	private void Relay_Save_85()
	{
		logic_SubGraph_SaveLoadBool_boolean_85 = local_msgIntro_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_85 = local_msgIntro_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_85.Save(ref logic_SubGraph_SaveLoadBool_boolean_85, logic_SubGraph_SaveLoadBool_boolAsVariable_85, logic_SubGraph_SaveLoadBool_uniqueID_85);
	}

	private void Relay_Load_85()
	{
		logic_SubGraph_SaveLoadBool_boolean_85 = local_msgIntro_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_85 = local_msgIntro_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_85.Load(ref logic_SubGraph_SaveLoadBool_boolean_85, logic_SubGraph_SaveLoadBool_boolAsVariable_85, logic_SubGraph_SaveLoadBool_uniqueID_85);
	}

	private void Relay_Set_True_85()
	{
		logic_SubGraph_SaveLoadBool_boolean_85 = local_msgIntro_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_85 = local_msgIntro_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_85.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_85, logic_SubGraph_SaveLoadBool_boolAsVariable_85, logic_SubGraph_SaveLoadBool_uniqueID_85);
	}

	private void Relay_Set_False_85()
	{
		logic_SubGraph_SaveLoadBool_boolean_85 = local_msgIntro_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_85 = local_msgIntro_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_85.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_85, logic_SubGraph_SaveLoadBool_boolAsVariable_85, logic_SubGraph_SaveLoadBool_uniqueID_85);
	}

	private void Relay_In_88()
	{
		logic_uScript_GetTankBlock_tank_88 = local_ButtonBase1Tech_Tank;
		logic_uScript_GetTankBlock_blockType_88 = blockTypeButton1;
		logic_uScript_GetTankBlock_Return_88 = logic_uScript_GetTankBlock_uScript_GetTankBlock_88.In(logic_uScript_GetTankBlock_tank_88, logic_uScript_GetTankBlock_blockType_88);
		local_ButtonBlock1_TankBlock = logic_uScript_GetTankBlock_Return_88;
		if (logic_uScript_GetTankBlock_uScript_GetTankBlock_88.Returned)
		{
			Relay_In_91();
		}
	}

	private void Relay_In_91()
	{
		logic_uScript_GetTankBlock_tank_91 = local_ButtonBase2Tech_Tank;
		logic_uScript_GetTankBlock_blockType_91 = blockTypeButton2;
		logic_uScript_GetTankBlock_Return_91 = logic_uScript_GetTankBlock_uScript_GetTankBlock_91.In(logic_uScript_GetTankBlock_tank_91, logic_uScript_GetTankBlock_blockType_91);
		local_ButtonBlock2_TankBlock = logic_uScript_GetTankBlock_Return_91;
		if (logic_uScript_GetTankBlock_uScript_GetTankBlock_91.Returned)
		{
			Relay_In_161();
		}
	}

	private void Relay_In_92()
	{
		logic_uScriptCon_CompareInt_A_92 = local_Button1Value_System_Int32;
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_92.In(logic_uScriptCon_CompareInt_A_92, logic_uScriptCon_CompareInt_B_92);
		bool greaterThan = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_92.GreaterThan;
		bool lessThanOrEqualTo = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_92.LessThanOrEqualTo;
		if (greaterThan)
		{
			Relay_In_564();
		}
		if (lessThanOrEqualTo)
		{
			Relay_In_169();
		}
	}

	private void Relay_In_100()
	{
		logic_uScript_GetTankBlock_tank_100 = local_ButtonBase4Tech_Tank;
		logic_uScript_GetTankBlock_blockType_100 = blockTypeButton4;
		logic_uScript_GetTankBlock_Return_100 = logic_uScript_GetTankBlock_uScript_GetTankBlock_100.In(logic_uScript_GetTankBlock_tank_100, logic_uScript_GetTankBlock_blockType_100);
		local_ButtonBlock4_TankBlock = logic_uScript_GetTankBlock_Return_100;
		if (logic_uScript_GetTankBlock_uScript_GetTankBlock_100.Returned)
		{
			Relay_In_228();
		}
	}

	private void Relay_AtIndex_101()
	{
		int num = 0;
		Array array = local_83_TankArray;
		if (logic_uScript_AccessListTech_techList_101.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_101, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_101, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_101.AtIndex(ref logic_uScript_AccessListTech_techList_101, logic_uScript_AccessListTech_index_101, out logic_uScript_AccessListTech_value_101);
		local_83_TankArray = logic_uScript_AccessListTech_techList_101;
		local_ButtonBase1Tech_Tank = logic_uScript_AccessListTech_value_101;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_101.Out)
		{
			Relay_In_123();
		}
	}

	private void Relay_Output1_102()
	{
		Relay_False_551();
	}

	private void Relay_Output2_102()
	{
		Relay_In_206();
	}

	private void Relay_Output3_102()
	{
		Relay_In_75();
	}

	private void Relay_Output4_102()
	{
		Relay_In_141();
	}

	private void Relay_Output5_102()
	{
		Relay_In_698();
	}

	private void Relay_Output6_102()
	{
	}

	private void Relay_Output7_102()
	{
	}

	private void Relay_Output8_102()
	{
	}

	private void Relay_In_102()
	{
		logic_uScriptCon_ManualSwitch_CurrentOutput_102 = local_QuestionTwo_System_Int32;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_102.In(logic_uScriptCon_ManualSwitch_CurrentOutput_102);
	}

	private void Relay_In_104()
	{
		logic_uScript_SetTankHideBlockLimit_tech_104 = local_ButtonBase3Tech_Tank;
		logic_uScript_SetTankHideBlockLimit_uScript_SetTankHideBlockLimit_104.In(logic_uScript_SetTankHideBlockLimit_hidden_104, logic_uScript_SetTankHideBlockLimit_tech_104);
		if (logic_uScript_SetTankHideBlockLimit_uScript_SetTankHideBlockLimit_104.Out)
		{
			Relay_In_127();
		}
	}

	private void Relay_In_105()
	{
		logic_uScript_LockTechInteraction_tech_105 = local_ButtonBase2Tech_Tank;
		logic_uScript_LockTechInteraction_uScript_LockTechInteraction_105.In(logic_uScript_LockTechInteraction_tech_105, logic_uScript_LockTechInteraction_excludedBlocks_105, logic_uScript_LockTechInteraction_excludedUniqueBlocks_105);
		if (logic_uScript_LockTechInteraction_uScript_LockTechInteraction_105.Out)
		{
			Relay_In_73();
		}
	}

	private void Relay_In_106()
	{
		logic_uScript_Wait_seconds_106 = WaitingTime;
		logic_uScript_Wait_uScript_Wait_106.In(logic_uScript_Wait_seconds_106, logic_uScript_Wait_repeat_106);
		if (logic_uScript_Wait_uScript_Wait_106.Waited)
		{
			Relay_False_22();
		}
	}

	private void Relay_True_109()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_109.True(out logic_uScriptAct_SetBool_Target_109);
		local_Question02WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_109;
	}

	private void Relay_False_109()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_109.False(out logic_uScriptAct_SetBool_Target_109);
		local_Question02WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_109;
	}

	private void Relay_In_114()
	{
		logic_uScript_SetCustomRadarTeamID_tech_114 = local_ButtonBase1Tech_Tank;
		logic_uScript_SetCustomRadarTeamID_uScript_SetCustomRadarTeamID_114.In(logic_uScript_SetCustomRadarTeamID_tech_114, logic_uScript_SetCustomRadarTeamID_radarTeamID_114);
		if (logic_uScript_SetCustomRadarTeamID_uScript_SetCustomRadarTeamID_114.Out)
		{
			Relay_In_186();
		}
	}

	private void Relay_True_115()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_115.True(out logic_uScriptAct_SetBool_Target_115);
		local_NearNPC_System_Boolean = logic_uScriptAct_SetBool_Target_115;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_115.Out)
		{
			Relay_In_389();
		}
	}

	private void Relay_False_115()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_115.False(out logic_uScriptAct_SetBool_Target_115);
		local_NearNPC_System_Boolean = logic_uScriptAct_SetBool_Target_115;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_115.Out)
		{
			Relay_In_389();
		}
	}

	private void Relay_In_116()
	{
		int num = 0;
		Array array = buttonbase3SpawnData;
		if (logic_uScript_GetAndCheckTechs_techData_116.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_116, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techData_116, num, array.Length);
		num += array.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_116 = owner_Connection_149;
		int num2 = 0;
		Array array2 = local_173_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_116.Length != num2 + array2.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_116, num2 + array2.Length);
		}
		Array.Copy(array2, 0, logic_uScript_GetAndCheckTechs_techs_116, num2, array2.Length);
		num2 += array2.Length;
		logic_uScript_GetAndCheckTechs_Return_116 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_116.In(logic_uScript_GetAndCheckTechs_techData_116, logic_uScript_GetAndCheckTechs_ownerNode_116, ref logic_uScript_GetAndCheckTechs_techs_116);
		local_173_TankArray = logic_uScript_GetAndCheckTechs_techs_116;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_116.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_116.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_116.AllDead;
		if (allAlive)
		{
			Relay_AtIndex_197();
		}
		if (someAlive)
		{
			Relay_AtIndex_197();
		}
		if (allDead)
		{
			Relay_In_225();
		}
	}

	private void Relay_Pause_119()
	{
		logic_uScript_PausePopulation_uScript_PausePopulation_119.Pause();
		if (logic_uScript_PausePopulation_uScript_PausePopulation_119.Out)
		{
			Relay_True_115();
		}
	}

	private void Relay_UnPause_119()
	{
		logic_uScript_PausePopulation_uScript_PausePopulation_119.UnPause();
		if (logic_uScript_PausePopulation_uScript_PausePopulation_119.Out)
		{
			Relay_True_115();
		}
	}

	private void Relay_In_123()
	{
		logic_uScript_LockTech_tech_123 = local_ButtonBase1Tech_Tank;
		logic_uScript_LockTech_uScript_LockTech_123.In(logic_uScript_LockTech_tech_123, logic_uScript_LockTech_lockType_123);
		if (logic_uScript_LockTech_uScript_LockTech_123.Out)
		{
			Relay_In_185();
		}
	}

	private void Relay_In_124()
	{
		logic_uScript_AddMessage_messageData_124 = msg18QuestionSix;
		logic_uScript_AddMessage_speaker_124 = messageSpeaker;
		logic_uScript_AddMessage_Return_124 = logic_uScript_AddMessage_uScript_AddMessage_124.In(logic_uScript_AddMessage_messageData_124, logic_uScript_AddMessage_speaker_124);
		if (logic_uScript_AddMessage_uScript_AddMessage_124.Out)
		{
			Relay_In_250();
		}
	}

	private void Relay_In_125()
	{
		logic_uScript_GetCircuitChargeInfo_block_125 = local_ButtonBlock1_TankBlock;
		logic_uScript_GetCircuitChargeInfo_Return_125 = logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_125.In(logic_uScript_GetCircuitChargeInfo_block_125, logic_uScript_GetCircuitChargeInfo_tech_125, logic_uScript_GetCircuitChargeInfo_blockType_125);
		local_Button1Value_System_Int32 = logic_uScript_GetCircuitChargeInfo_Return_125;
		if (logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_125.Out)
		{
			Relay_In_92();
		}
	}

	private void Relay_In_126()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_126.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_126.Out)
		{
			Relay_In_244();
		}
	}

	private void Relay_In_127()
	{
		int num = 0;
		Array array = buttonbase4SpawnData;
		if (logic_uScript_GetAndCheckTechs_techData_127.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_127, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techData_127, num, array.Length);
		num += array.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_127 = owner_Connection_196;
		int num2 = 0;
		Array array2 = local_39_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_127.Length != num2 + array2.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_127, num2 + array2.Length);
		}
		Array.Copy(array2, 0, logic_uScript_GetAndCheckTechs_techs_127, num2, array2.Length);
		num2 += array2.Length;
		logic_uScript_GetAndCheckTechs_Return_127 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_127.In(logic_uScript_GetAndCheckTechs_techData_127, logic_uScript_GetAndCheckTechs_ownerNode_127, ref logic_uScript_GetAndCheckTechs_techs_127);
		local_39_TankArray = logic_uScript_GetAndCheckTechs_techs_127;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_127.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_127.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_127.AllDead;
		if (allAlive)
		{
			Relay_AtIndex_177();
		}
		if (someAlive)
		{
			Relay_AtIndex_177();
		}
		if (allDead)
		{
			Relay_In_163();
		}
	}

	private void Relay_In_130()
	{
		logic_uScript_SetTankInvulnerable_tank_130 = local_NPCTech_Tank;
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_130.In(logic_uScript_SetTankInvulnerable_invulnerable_130, logic_uScript_SetTankInvulnerable_tank_130);
		if (logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_130.Out)
		{
			Relay_In_170();
		}
	}

	private void Relay_In_133()
	{
		logic_uScript_LockTech_tech_133 = local_ButtonBase4Tech_Tank;
		logic_uScript_LockTech_uScript_LockTech_133.In(logic_uScript_LockTech_tech_133, logic_uScript_LockTech_lockType_133);
		if (logic_uScript_LockTech_uScript_LockTech_133.Out)
		{
			Relay_In_41();
		}
	}

	private void Relay_In_135()
	{
		logic_uScript_AddMessage_messageData_135 = msg05QuestionOneWrong;
		logic_uScript_AddMessage_speaker_135 = messageSpeaker;
		logic_uScript_AddMessage_Return_135 = logic_uScript_AddMessage_uScript_AddMessage_135.In(logic_uScript_AddMessage_messageData_135, logic_uScript_AddMessage_speaker_135);
		if (logic_uScript_AddMessage_uScript_AddMessage_135.Out)
		{
			Relay_In_106();
		}
	}

	private void Relay_In_137()
	{
		logic_uScript_LockTechInteraction_tech_137 = local_ButtonBase1Tech_Tank;
		logic_uScript_LockTechInteraction_uScript_LockTechInteraction_137.In(logic_uScript_LockTechInteraction_tech_137, logic_uScript_LockTechInteraction_excludedBlocks_137, logic_uScript_LockTechInteraction_excludedUniqueBlocks_137);
		if (logic_uScript_LockTechInteraction_uScript_LockTechInteraction_137.Out)
		{
			Relay_In_105();
		}
	}

	private void Relay_SaveEvent_140()
	{
		Relay_Save_217();
	}

	private void Relay_LoadEvent_140()
	{
		Relay_Load_217();
	}

	private void Relay_RestartEvent_140()
	{
		Relay_Restart_217();
	}

	private void Relay_In_141()
	{
		logic_uScriptCon_CompareBool_Bool_141 = local_Question02WrongAnswers_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_141.In(logic_uScriptCon_CompareBool_Bool_141);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_141.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_141.False;
		if (num)
		{
			Relay_In_202();
		}
		if (flag)
		{
			Relay_In_283();
		}
	}

	private void Relay_In_144()
	{
		logic_uScript_AddMessage_messageData_144 = msg04QuestionOneCorrect;
		logic_uScript_AddMessage_speaker_144 = messageSpeaker;
		logic_uScript_AddMessage_Return_144 = logic_uScript_AddMessage_uScript_AddMessage_144.In(logic_uScript_AddMessage_messageData_144, logic_uScript_AddMessage_speaker_144);
		if (logic_uScript_AddMessage_uScript_AddMessage_144.Out)
		{
			Relay_In_561();
		}
	}

	private void Relay_In_145()
	{
		logic_uScript_IsPlayerInRangeOfTech_tech_145 = local_NPCTech_Tank;
		logic_uScript_IsPlayerInRangeOfTech_range_145 = distNPCFound;
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_145.In(logic_uScript_IsPlayerInRangeOfTech_tech_145, logic_uScript_IsPlayerInRangeOfTech_range_145, logic_uScript_IsPlayerInRangeOfTech_techs_145);
		bool inRange = logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_145.InRange;
		bool outOfRange = logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_145.OutOfRange;
		if (inRange)
		{
			Relay_Pause_119();
		}
		if (outOfRange)
		{
			Relay_UnPause_49();
		}
	}

	private void Relay_Save_Out_147()
	{
		Relay_Save_252();
	}

	private void Relay_Load_Out_147()
	{
		Relay_Load_252();
	}

	private void Relay_Restart_Out_147()
	{
		Relay_Restart_252();
	}

	private void Relay_Save_147()
	{
		logic_SubGraph_SaveLoadInt_integer_147 = local_QuestionTwo_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_147 = local_QuestionTwo_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_147.Save(logic_SubGraph_SaveLoadInt_restartValue_147, ref logic_SubGraph_SaveLoadInt_integer_147, logic_SubGraph_SaveLoadInt_intAsVariable_147, logic_SubGraph_SaveLoadInt_uniqueID_147);
	}

	private void Relay_Load_147()
	{
		logic_SubGraph_SaveLoadInt_integer_147 = local_QuestionTwo_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_147 = local_QuestionTwo_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_147.Load(logic_SubGraph_SaveLoadInt_restartValue_147, ref logic_SubGraph_SaveLoadInt_integer_147, logic_SubGraph_SaveLoadInt_intAsVariable_147, logic_SubGraph_SaveLoadInt_uniqueID_147);
	}

	private void Relay_Restart_147()
	{
		logic_SubGraph_SaveLoadInt_integer_147 = local_QuestionTwo_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_147 = local_QuestionTwo_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_147.Restart(logic_SubGraph_SaveLoadInt_restartValue_147, ref logic_SubGraph_SaveLoadInt_integer_147, logic_SubGraph_SaveLoadInt_intAsVariable_147, logic_SubGraph_SaveLoadInt_uniqueID_147);
	}

	private void Relay_In_150()
	{
		logic_uScript_AddMessage_messageData_150 = msg20QuestionSixWrong;
		logic_uScript_AddMessage_speaker_150 = messageSpeaker;
		logic_uScript_AddMessage_Return_150 = logic_uScript_AddMessage_uScript_AddMessage_150.In(logic_uScript_AddMessage_messageData_150, logic_uScript_AddMessage_speaker_150);
		if (logic_uScript_AddMessage_uScript_AddMessage_150.Out)
		{
			Relay_In_242();
		}
	}

	private void Relay_In_151()
	{
		int num = 0;
		Array array = buttonbase2SpawnData;
		if (logic_uScript_GetAndCheckTechs_techData_151.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_151, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techData_151, num, array.Length);
		num += array.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_151 = owner_Connection_2;
		int num2 = 0;
		Array array2 = local_117_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_151.Length != num2 + array2.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_151, num2 + array2.Length);
		}
		Array.Copy(array2, 0, logic_uScript_GetAndCheckTechs_techs_151, num2, array2.Length);
		num2 += array2.Length;
		logic_uScript_GetAndCheckTechs_Return_151 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_151.In(logic_uScript_GetAndCheckTechs_techData_151, logic_uScript_GetAndCheckTechs_ownerNode_151, ref logic_uScript_GetAndCheckTechs_techs_151);
		local_117_TankArray = logic_uScript_GetAndCheckTechs_techs_151;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_151.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_151.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_151.AllDead;
		if (allAlive)
		{
			Relay_AtIndex_164();
		}
		if (someAlive)
		{
			Relay_AtIndex_164();
		}
		if (allDead)
		{
			Relay_In_31();
		}
	}

	private void Relay_In_154()
	{
		logic_uScriptCon_CompareBool_Bool_154 = local_msgIntro_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_154.In(logic_uScriptCon_CompareBool_Bool_154);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_154.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_154.False;
		if (num)
		{
			Relay_In_145();
		}
		if (flag)
		{
			Relay_True_374();
		}
	}

	private void Relay_In_156()
	{
		logic_uScript_GetCircuitChargeInfo_block_156 = local_ButtonBlock4_TankBlock;
		logic_uScript_GetCircuitChargeInfo_Return_156 = logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_156.In(logic_uScript_GetCircuitChargeInfo_block_156, logic_uScript_GetCircuitChargeInfo_tech_156, logic_uScript_GetCircuitChargeInfo_blockType_156);
		local_Button4Value_System_Int32 = logic_uScript_GetCircuitChargeInfo_Return_156;
		if (logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_156.Out)
		{
			Relay_In_62();
		}
	}

	private void Relay_In_161()
	{
		logic_uScript_GetTankBlock_tank_161 = local_ButtonBase3Tech_Tank;
		logic_uScript_GetTankBlock_blockType_161 = blockTypeButton3;
		logic_uScript_GetTankBlock_Return_161 = logic_uScript_GetTankBlock_uScript_GetTankBlock_161.In(logic_uScript_GetTankBlock_tank_161, logic_uScript_GetTankBlock_blockType_161);
		local_ButtonBlock3_TankBlock = logic_uScript_GetTankBlock_Return_161;
		if (logic_uScript_GetTankBlock_uScript_GetTankBlock_161.Returned)
		{
			Relay_In_100();
		}
	}

	private void Relay_In_163()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_163.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_163.Out)
		{
			Relay_In_126();
		}
	}

	private void Relay_AtIndex_164()
	{
		int num = 0;
		Array array = local_117_TankArray;
		if (logic_uScript_AccessListTech_techList_164.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_164, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_164, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_164.AtIndex(ref logic_uScript_AccessListTech_techList_164, logic_uScript_AccessListTech_index_164, out logic_uScript_AccessListTech_value_164);
		local_117_TankArray = logic_uScript_AccessListTech_techList_164;
		local_ButtonBase2Tech_Tank = logic_uScript_AccessListTech_value_164;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_164.Out)
		{
			Relay_In_201();
		}
	}

	private void Relay_In_165()
	{
		logic_uScript_SetEncounterTarget_owner_165 = owner_Connection_21;
		logic_uScript_SetEncounterTarget_visibleObject_165 = local_NPCTech_Tank;
		logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_165.In(logic_uScript_SetEncounterTarget_owner_165, logic_uScript_SetEncounterTarget_visibleObject_165);
		if (logic_uScript_SetEncounterTarget_uScript_SetEncounterTarget_165.Out)
		{
			Relay_In_88();
		}
	}

	private void Relay_In_169()
	{
		logic_uScript_GetCircuitChargeInfo_block_169 = local_ButtonBlock2_TankBlock;
		logic_uScript_GetCircuitChargeInfo_Return_169 = logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_169.In(logic_uScript_GetCircuitChargeInfo_block_169, logic_uScript_GetCircuitChargeInfo_tech_169, logic_uScript_GetCircuitChargeInfo_blockType_169);
		local_Button2Value_System_Int32 = logic_uScript_GetCircuitChargeInfo_Return_169;
		if (logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_169.Out)
		{
			Relay_In_38();
		}
	}

	private void Relay_In_170()
	{
		logic_uScript_LockTech_tech_170 = local_NPCTech_Tank;
		logic_uScript_LockTech_uScript_LockTech_170.In(logic_uScript_LockTech_tech_170, logic_uScript_LockTech_lockType_170);
		if (logic_uScript_LockTech_uScript_LockTech_170.Out)
		{
			Relay_In_55();
		}
	}

	private void Relay_AtIndex_174()
	{
		int num = 0;
		Array array = local_132_TankArray;
		if (logic_uScript_AccessListTech_techList_174.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_174, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_174, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_174.AtIndex(ref logic_uScript_AccessListTech_techList_174, logic_uScript_AccessListTech_index_174, out logic_uScript_AccessListTech_value_174);
		local_132_TankArray = logic_uScript_AccessListTech_techList_174;
		local_NPCTech_Tank = logic_uScript_AccessListTech_value_174;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_174.Out)
		{
			Relay_In_130();
		}
	}

	private void Relay_In_176()
	{
		logic_uScript_GetCircuitChargeInfo_block_176 = local_ButtonBlock3_TankBlock;
		logic_uScript_GetCircuitChargeInfo_Return_176 = logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_176.In(logic_uScript_GetCircuitChargeInfo_block_176, logic_uScript_GetCircuitChargeInfo_tech_176, logic_uScript_GetCircuitChargeInfo_blockType_176);
		local_Button3Value_System_Int32 = logic_uScript_GetCircuitChargeInfo_Return_176;
		if (logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_176.Out)
		{
			Relay_In_71();
		}
	}

	private void Relay_AtIndex_177()
	{
		int num = 0;
		Array array = local_39_TankArray;
		if (logic_uScript_AccessListTech_techList_177.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_177, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_177, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_177.AtIndex(ref logic_uScript_AccessListTech_techList_177, logic_uScript_AccessListTech_index_177, out logic_uScript_AccessListTech_value_177);
		local_39_TankArray = logic_uScript_AccessListTech_techList_177;
		local_ButtonBase4Tech_Tank = logic_uScript_AccessListTech_value_177;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_177.Out)
		{
			Relay_In_133();
		}
	}

	private void Relay_Output1_181()
	{
		Relay_False_547();
	}

	private void Relay_Output2_181()
	{
		Relay_In_230();
	}

	private void Relay_Output3_181()
	{
		Relay_In_144();
	}

	private void Relay_Output4_181()
	{
		Relay_In_204();
	}

	private void Relay_Output5_181()
	{
		Relay_In_313();
	}

	private void Relay_Output6_181()
	{
	}

	private void Relay_Output7_181()
	{
	}

	private void Relay_Output8_181()
	{
	}

	private void Relay_In_181()
	{
		logic_uScriptCon_ManualSwitch_CurrentOutput_181 = local_QuestionOne_System_Int32;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_181.In(logic_uScriptCon_ManualSwitch_CurrentOutput_181);
	}

	private void Relay_In_184()
	{
		int num = 0;
		Array array = buttonbase1SpawnData;
		if (logic_uScript_GetAndCheckTechs_techData_184.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_184, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techData_184, num, array.Length);
		num += array.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_184 = owner_Connection_82;
		int num2 = 0;
		Array array2 = local_83_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_184.Length != num2 + array2.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_184, num2 + array2.Length);
		}
		Array.Copy(array2, 0, logic_uScript_GetAndCheckTechs_techs_184, num2, array2.Length);
		num2 += array2.Length;
		logic_uScript_GetAndCheckTechs_Return_184 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_184.In(logic_uScript_GetAndCheckTechs_techData_184, logic_uScript_GetAndCheckTechs_ownerNode_184, ref logic_uScript_GetAndCheckTechs_techs_184);
		local_83_TankArray = logic_uScript_GetAndCheckTechs_techs_184;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_184.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_184.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_184.AllDead;
		if (allAlive)
		{
			Relay_AtIndex_101();
		}
		if (someAlive)
		{
			Relay_AtIndex_101();
		}
		if (allDead)
		{
			Relay_In_194();
		}
	}

	private void Relay_In_185()
	{
		logic_uScript_SetTankInvulnerable_tank_185 = local_ButtonBase1Tech_Tank;
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_185.In(logic_uScript_SetTankInvulnerable_invulnerable_185, logic_uScript_SetTankInvulnerable_tank_185);
		if (logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_185.Out)
		{
			Relay_In_114();
		}
	}

	private void Relay_In_186()
	{
		logic_uScript_SetTankHideBlockLimit_tech_186 = local_ButtonBase1Tech_Tank;
		logic_uScript_SetTankHideBlockLimit_uScript_SetTankHideBlockLimit_186.In(logic_uScript_SetTankHideBlockLimit_hidden_186, logic_uScript_SetTankHideBlockLimit_tech_186);
		if (logic_uScript_SetTankHideBlockLimit_uScript_SetTankHideBlockLimit_186.Out)
		{
			Relay_In_151();
		}
	}

	private void Relay_In_188()
	{
		logic_uScript_SetTankHideBlockLimit_tech_188 = local_ButtonBase2Tech_Tank;
		logic_uScript_SetTankHideBlockLimit_uScript_SetTankHideBlockLimit_188.In(logic_uScript_SetTankHideBlockLimit_hidden_188, logic_uScript_SetTankHideBlockLimit_tech_188);
		if (logic_uScript_SetTankHideBlockLimit_uScript_SetTankHideBlockLimit_188.Out)
		{
			Relay_In_116();
		}
	}

	private void Relay_In_189()
	{
		logic_uScript_RemoveScenery_ownerNode_189 = owner_Connection_57;
		logic_uScript_RemoveScenery_positionName_189 = ButtonBasePosition;
		logic_uScript_RemoveScenery_radius_189 = clearSceneryRadius;
		logic_uScript_RemoveScenery_uScript_RemoveScenery_189.In(logic_uScript_RemoveScenery_ownerNode_189, logic_uScript_RemoveScenery_positionName_189, logic_uScript_RemoveScenery_radius_189, logic_uScript_RemoveScenery_preventChunksSpawning_189);
		if (logic_uScript_RemoveScenery_uScript_RemoveScenery_189.Out)
		{
			Relay_In_184();
		}
	}

	private void Relay_In_192()
	{
		logic_uScript_LockTechInteraction_tech_192 = local_ButtonBase4Tech_Tank;
		logic_uScript_LockTechInteraction_uScript_LockTechInteraction_192.In(logic_uScript_LockTechInteraction_tech_192, logic_uScript_LockTechInteraction_excludedBlocks_192, logic_uScript_LockTechInteraction_excludedUniqueBlocks_192);
		if (logic_uScript_LockTechInteraction_uScript_LockTechInteraction_192.Out)
		{
			Relay_In_375();
		}
	}

	private void Relay_In_194()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_194.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_194.Out)
		{
			Relay_In_14();
		}
	}

	private void Relay_AtIndex_197()
	{
		int num = 0;
		Array array = local_173_TankArray;
		if (logic_uScript_AccessListTech_techList_197.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_AccessListTech_techList_197, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_AccessListTech_techList_197, num, array.Length);
		num += array.Length;
		logic_uScript_AccessListTech_uScript_AccessListTech_197.AtIndex(ref logic_uScript_AccessListTech_techList_197, logic_uScript_AccessListTech_index_197, out logic_uScript_AccessListTech_value_197);
		local_173_TankArray = logic_uScript_AccessListTech_techList_197;
		local_ButtonBase3Tech_Tank = logic_uScript_AccessListTech_value_197;
		if (logic_uScript_AccessListTech_uScript_AccessListTech_197.Out)
		{
			Relay_In_61();
		}
	}

	private void Relay_In_201()
	{
		logic_uScript_LockTech_tech_201 = local_ButtonBase2Tech_Tank;
		logic_uScript_LockTech_uScript_LockTech_201.In(logic_uScript_LockTech_tech_201, logic_uScript_LockTech_lockType_201);
		if (logic_uScript_LockTech_uScript_LockTech_201.Out)
		{
			Relay_In_13();
		}
	}

	private void Relay_In_202()
	{
		logic_uScript_AddMessage_messageData_202 = msg08QuestionTwoWrong;
		logic_uScript_AddMessage_speaker_202 = messageSpeaker;
		logic_uScript_AddMessage_Return_202 = logic_uScript_AddMessage_uScript_AddMessage_202.In(logic_uScript_AddMessage_messageData_202, logic_uScript_AddMessage_speaker_202);
		if (logic_uScript_AddMessage_uScript_AddMessage_202.Out)
		{
			Relay_In_67();
		}
	}

	private void Relay_In_203()
	{
		logic_uScript_AddMessage_messageData_203 = msg01Intro;
		logic_uScript_AddMessage_speaker_203 = messageSpeaker;
		logic_uScript_AddMessage_Return_203 = logic_uScript_AddMessage_uScript_AddMessage_203.In(logic_uScript_AddMessage_messageData_203, logic_uScript_AddMessage_speaker_203);
		if (logic_uScript_AddMessage_uScript_AddMessage_203.Out)
		{
			Relay_In_145();
		}
	}

	private void Relay_In_204()
	{
		logic_uScriptCon_CompareBool_Bool_204 = local_Question01WrongAnswers_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_204.In(logic_uScriptCon_CompareBool_Bool_204);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_204.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_204.False;
		if (num)
		{
			Relay_In_135();
		}
		if (flag)
		{
			Relay_In_264();
		}
	}

	private void Relay_In_206()
	{
		logic_uScript_AddMessage_messageData_206 = msg06QuestionTwo;
		logic_uScript_AddMessage_speaker_206 = messageSpeaker;
		logic_uScript_AddMessage_Return_206 = logic_uScript_AddMessage_uScript_AddMessage_206.In(logic_uScript_AddMessage_messageData_206, logic_uScript_AddMessage_speaker_206);
		if (logic_uScript_AddMessage_uScript_AddMessage_206.Out)
		{
			Relay_In_658();
		}
	}

	private void Relay_In_208()
	{
		logic_uScriptCon_CompareBool_Bool_208 = local_Initialize_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_208.In(logic_uScriptCon_CompareBool_Bool_208);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_208.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_208.False;
		if (num)
		{
			Relay_In_255();
		}
		if (flag)
		{
			Relay_True_63();
		}
	}

	private void Relay_In_209()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_209.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_209.Out)
		{
			Relay_In_241();
		}
	}

	private void Relay_True_211()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_211.True(out logic_uScriptAct_SetBool_Target_211);
		local_NearNPC_System_Boolean = logic_uScriptAct_SetBool_Target_211;
	}

	private void Relay_False_211()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_211.False(out logic_uScriptAct_SetBool_Target_211);
		local_NearNPC_System_Boolean = logic_uScriptAct_SetBool_Target_211;
	}

	private void Relay_Save_Out_217()
	{
		Relay_Save_236();
	}

	private void Relay_Load_Out_217()
	{
		Relay_Load_236();
	}

	private void Relay_Restart_Out_217()
	{
		Relay_Restart_236();
	}

	private void Relay_Save_217()
	{
		logic_SubGraph_SaveLoadInt_integer_217 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_217 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_217.Save(logic_SubGraph_SaveLoadInt_restartValue_217, ref logic_SubGraph_SaveLoadInt_integer_217, logic_SubGraph_SaveLoadInt_intAsVariable_217, logic_SubGraph_SaveLoadInt_uniqueID_217);
	}

	private void Relay_Load_217()
	{
		logic_SubGraph_SaveLoadInt_integer_217 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_217 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_217.Load(logic_SubGraph_SaveLoadInt_restartValue_217, ref logic_SubGraph_SaveLoadInt_integer_217, logic_SubGraph_SaveLoadInt_intAsVariable_217, logic_SubGraph_SaveLoadInt_uniqueID_217);
	}

	private void Relay_Restart_217()
	{
		logic_SubGraph_SaveLoadInt_integer_217 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_217 = local_Stage_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_217.Restart(logic_SubGraph_SaveLoadInt_restartValue_217, ref logic_SubGraph_SaveLoadInt_integer_217, logic_SubGraph_SaveLoadInt_intAsVariable_217, logic_SubGraph_SaveLoadInt_uniqueID_217);
	}

	private void Relay_In_222()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_222.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_222.Out)
		{
			Relay_In_127();
		}
	}

	private void Relay_True_223()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_223.True(out logic_uScriptAct_SetBool_Target_223);
		local_CanPushButtons_System_Boolean = logic_uScriptAct_SetBool_Target_223;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_223.Out)
		{
			Relay_In_555();
		}
	}

	private void Relay_False_223()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_223.False(out logic_uScriptAct_SetBool_Target_223);
		local_CanPushButtons_System_Boolean = logic_uScriptAct_SetBool_Target_223;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_223.Out)
		{
			Relay_In_555();
		}
	}

	private void Relay_InitialSpawn_224()
	{
		int num = 0;
		Array array = buttonbase4SpawnData;
		if (logic_uScript_SpawnTechsFromData_spawnData_224.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_224, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SpawnTechsFromData_spawnData_224, num, array.Length);
		num += array.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_224 = owner_Connection_33;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_224.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_224, logic_uScript_SpawnTechsFromData_ownerNode_224, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_224, logic_uScript_SpawnTechsFromData_allowResurrection_224);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_224.Out)
		{
			Relay_InitialSpawn_247();
		}
	}

	private void Relay_In_225()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_225.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_225.Out)
		{
			Relay_In_222();
		}
	}

	private void Relay_In_228()
	{
		logic_uScriptCon_CompareBool_Bool_228 = local_CanPushButtons_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_228.In(logic_uScriptCon_CompareBool_Bool_228);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_228.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_228.False;
		if (num)
		{
			Relay_In_254();
		}
		if (flag)
		{
			Relay_In_137();
		}
	}

	private void Relay_In_230()
	{
		logic_uScript_AddMessage_messageData_230 = msg03QuestionOne;
		logic_uScript_AddMessage_speaker_230 = messageSpeaker;
		logic_uScript_AddMessage_Return_230 = logic_uScript_AddMessage_uScript_AddMessage_230.In(logic_uScript_AddMessage_messageData_230, logic_uScript_AddMessage_speaker_230);
		if (logic_uScript_AddMessage_uScript_AddMessage_230.Out)
		{
			Relay_In_125();
		}
	}

	private void Relay_In_231()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_231.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_231.Out)
		{
			Relay_In_116();
		}
	}

	private void Relay_True_232()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_232.True(out logic_uScriptAct_SetBool_Target_232);
		local_Question06WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_232;
	}

	private void Relay_False_232()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_232.False(out logic_uScriptAct_SetBool_Target_232);
		local_Question06WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_232;
	}

	private void Relay_Save_Out_236()
	{
		Relay_Save_147();
	}

	private void Relay_Load_Out_236()
	{
		Relay_Load_147();
	}

	private void Relay_Restart_Out_236()
	{
		Relay_Restart_147();
	}

	private void Relay_Save_236()
	{
		logic_SubGraph_SaveLoadInt_integer_236 = local_QuestionOne_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_236 = local_QuestionOne_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_236.Save(logic_SubGraph_SaveLoadInt_restartValue_236, ref logic_SubGraph_SaveLoadInt_integer_236, logic_SubGraph_SaveLoadInt_intAsVariable_236, logic_SubGraph_SaveLoadInt_uniqueID_236);
	}

	private void Relay_Load_236()
	{
		logic_SubGraph_SaveLoadInt_integer_236 = local_QuestionOne_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_236 = local_QuestionOne_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_236.Load(logic_SubGraph_SaveLoadInt_restartValue_236, ref logic_SubGraph_SaveLoadInt_integer_236, logic_SubGraph_SaveLoadInt_intAsVariable_236, logic_SubGraph_SaveLoadInt_uniqueID_236);
	}

	private void Relay_Restart_236()
	{
		logic_SubGraph_SaveLoadInt_integer_236 = local_QuestionOne_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_236 = local_QuestionOne_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_236.Restart(logic_SubGraph_SaveLoadInt_restartValue_236, ref logic_SubGraph_SaveLoadInt_integer_236, logic_SubGraph_SaveLoadInt_intAsVariable_236, logic_SubGraph_SaveLoadInt_uniqueID_236);
	}

	private void Relay_In_241()
	{
		logic_uScript_DirectEnemiesOutOfEncounter_ownerNode_241 = owner_Connection_54;
		logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_241.In(logic_uScript_DirectEnemiesOutOfEncounter_ownerNode_241);
		if (logic_uScript_DirectEnemiesOutOfEncounter_uScript_DirectEnemiesOutOfEncounter_241.Out)
		{
			Relay_In_165();
		}
	}

	private void Relay_In_242()
	{
		logic_uScript_Wait_seconds_242 = WaitingTime;
		logic_uScript_Wait_uScript_Wait_242.In(logic_uScript_Wait_seconds_242, logic_uScript_Wait_repeat_242);
		if (logic_uScript_Wait_uScript_Wait_242.Waited)
		{
			Relay_False_232();
		}
	}

	private void Relay_InitialSpawn_243()
	{
		int num = 0;
		Array array = buttonbase2SpawnData;
		if (logic_uScript_SpawnTechsFromData_spawnData_243.Length != num + array.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_243, num + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_SpawnTechsFromData_spawnData_243, num, array.Length);
		num += array.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_243 = owner_Connection_237;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_243.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_243, logic_uScript_SpawnTechsFromData_ownerNode_243, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_243, logic_uScript_SpawnTechsFromData_allowResurrection_243);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_243.Out)
		{
			Relay_InitialSpawn_78();
		}
	}

	private void Relay_In_244()
	{
		int num = 0;
		Array nPCSpawnData = NPCSpawnData;
		if (logic_uScript_GetAndCheckTechs_techData_244.Length != num + nPCSpawnData.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_244, num + nPCSpawnData.Length);
		}
		Array.Copy(nPCSpawnData, 0, logic_uScript_GetAndCheckTechs_techData_244, num, nPCSpawnData.Length);
		num += nPCSpawnData.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_244 = owner_Connection_210;
		int num2 = 0;
		Array array = local_132_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_244.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_244, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techs_244, num2, array.Length);
		num2 += array.Length;
		logic_uScript_GetAndCheckTechs_Return_244 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_244.In(logic_uScript_GetAndCheckTechs_techData_244, logic_uScript_GetAndCheckTechs_ownerNode_244, ref logic_uScript_GetAndCheckTechs_techs_244);
		local_132_TankArray = logic_uScript_GetAndCheckTechs_techs_244;
		bool allAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_244.AllAlive;
		bool someAlive = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_244.SomeAlive;
		bool allDead = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_244.AllDead;
		if (allAlive)
		{
			Relay_AtIndex_174();
		}
		if (someAlive)
		{
			Relay_AtIndex_174();
		}
		if (allDead)
		{
			Relay_In_50();
		}
	}

	private void Relay_Output1_245()
	{
		Relay_False_969();
	}

	private void Relay_Output2_245()
	{
		Relay_In_124();
	}

	private void Relay_Output3_245()
	{
		Relay_In_19();
	}

	private void Relay_Output4_245()
	{
		Relay_In_12();
	}

	private void Relay_Output5_245()
	{
		Relay_In_323();
	}

	private void Relay_Output6_245()
	{
		Relay_In_347();
	}

	private void Relay_Output7_245()
	{
	}

	private void Relay_Output8_245()
	{
	}

	private void Relay_In_245()
	{
		logic_uScriptCon_ManualSwitch_CurrentOutput_245 = local_QuestionSix_System_Int32;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_245.In(logic_uScriptCon_ManualSwitch_CurrentOutput_245);
	}

	private void Relay_InitialSpawn_247()
	{
		int num = 0;
		Array nPCSpawnData = NPCSpawnData;
		if (logic_uScript_SpawnTechsFromData_spawnData_247.Length != num + nPCSpawnData.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_247, num + nPCSpawnData.Length);
		}
		Array.Copy(nPCSpawnData, 0, logic_uScript_SpawnTechsFromData_spawnData_247, num, nPCSpawnData.Length);
		num += nPCSpawnData.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_247 = owner_Connection_213;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_247.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_247, logic_uScript_SpawnTechsFromData_ownerNode_247, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_247, logic_uScript_SpawnTechsFromData_allowResurrection_247);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_247.Out)
		{
			Relay_In_189();
		}
	}

	private void Relay_In_248()
	{
		logic_uScript_SetTankHideBlockLimit_tech_248 = local_ButtonBase4Tech_Tank;
		logic_uScript_SetTankHideBlockLimit_uScript_SetTankHideBlockLimit_248.In(logic_uScript_SetTankHideBlockLimit_hidden_248, logic_uScript_SetTankHideBlockLimit_tech_248);
		if (logic_uScript_SetTankHideBlockLimit_uScript_SetTankHideBlockLimit_248.Out)
		{
			Relay_In_244();
		}
	}

	private void Relay_Save_Out_249()
	{
		Relay_Save_85();
	}

	private void Relay_Load_Out_249()
	{
		Relay_Load_85();
	}

	private void Relay_Restart_Out_249()
	{
		Relay_Set_False_85();
	}

	private void Relay_Save_249()
	{
		logic_SubGraph_SaveLoadBool_boolean_249 = local_Initialize_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_249 = local_Initialize_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_249.Save(ref logic_SubGraph_SaveLoadBool_boolean_249, logic_SubGraph_SaveLoadBool_boolAsVariable_249, logic_SubGraph_SaveLoadBool_uniqueID_249);
	}

	private void Relay_Load_249()
	{
		logic_SubGraph_SaveLoadBool_boolean_249 = local_Initialize_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_249 = local_Initialize_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_249.Load(ref logic_SubGraph_SaveLoadBool_boolean_249, logic_SubGraph_SaveLoadBool_boolAsVariable_249, logic_SubGraph_SaveLoadBool_uniqueID_249);
	}

	private void Relay_Set_True_249()
	{
		logic_SubGraph_SaveLoadBool_boolean_249 = local_Initialize_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_249 = local_Initialize_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_249.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_249, logic_SubGraph_SaveLoadBool_boolAsVariable_249, logic_SubGraph_SaveLoadBool_uniqueID_249);
	}

	private void Relay_Set_False_249()
	{
		logic_SubGraph_SaveLoadBool_boolean_249 = local_Initialize_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_249 = local_Initialize_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_249.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_249, logic_SubGraph_SaveLoadBool_boolAsVariable_249, logic_SubGraph_SaveLoadBool_uniqueID_249);
	}

	private void Relay_In_250()
	{
		logic_uScript_GetCircuitChargeInfo_block_250 = local_ButtonBlock1_TankBlock;
		logic_uScript_GetCircuitChargeInfo_Return_250 = logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_250.In(logic_uScript_GetCircuitChargeInfo_block_250, logic_uScript_GetCircuitChargeInfo_tech_250, logic_uScript_GetCircuitChargeInfo_blockType_250);
		local_Button1Value_System_Int32 = logic_uScript_GetCircuitChargeInfo_Return_250;
		if (logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_250.Out)
		{
			Relay_In_64();
		}
	}

	private void Relay_In_251()
	{
		logic_uScript_AddMessage_messageData_251 = msgLeavingMissionArea;
		logic_uScript_AddMessage_speaker_251 = messageSpeaker;
		logic_uScript_AddMessage_Return_251 = logic_uScript_AddMessage_uScript_AddMessage_251.In(logic_uScript_AddMessage_messageData_251, logic_uScript_AddMessage_speaker_251);
		if (logic_uScript_AddMessage_uScript_AddMessage_251.Out)
		{
			Relay_False_211();
		}
	}

	private void Relay_Save_Out_252()
	{
		Relay_Save_397();
	}

	private void Relay_Load_Out_252()
	{
		Relay_Load_397();
	}

	private void Relay_Restart_Out_252()
	{
		Relay_Restart_397();
	}

	private void Relay_Save_252()
	{
		logic_SubGraph_SaveLoadInt_integer_252 = local_QuestionThree_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_252 = local_QuestionThree_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_252.Save(logic_SubGraph_SaveLoadInt_restartValue_252, ref logic_SubGraph_SaveLoadInt_integer_252, logic_SubGraph_SaveLoadInt_intAsVariable_252, logic_SubGraph_SaveLoadInt_uniqueID_252);
	}

	private void Relay_Load_252()
	{
		logic_SubGraph_SaveLoadInt_integer_252 = local_QuestionThree_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_252 = local_QuestionThree_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_252.Load(logic_SubGraph_SaveLoadInt_restartValue_252, ref logic_SubGraph_SaveLoadInt_integer_252, logic_SubGraph_SaveLoadInt_intAsVariable_252, logic_SubGraph_SaveLoadInt_uniqueID_252);
	}

	private void Relay_Restart_252()
	{
		logic_SubGraph_SaveLoadInt_integer_252 = local_QuestionThree_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_252 = local_QuestionThree_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_252.Restart(logic_SubGraph_SaveLoadInt_restartValue_252, ref logic_SubGraph_SaveLoadInt_integer_252, logic_SubGraph_SaveLoadInt_intAsVariable_252, logic_SubGraph_SaveLoadInt_uniqueID_252);
	}

	private void Relay_In_254()
	{
		logic_uScript_LockTechInteraction_tech_254 = local_ButtonBase1Tech_Tank;
		int num = 0;
		if (logic_uScript_LockTechInteraction_excludedBlocks_254.Length <= num)
		{
			Array.Resize(ref logic_uScript_LockTechInteraction_excludedBlocks_254, num + 1);
		}
		logic_uScript_LockTechInteraction_excludedBlocks_254[num++] = blockTypeButton1;
		int num2 = 0;
		if (logic_uScript_LockTechInteraction_excludedUniqueBlocks_254.Length <= num2)
		{
			Array.Resize(ref logic_uScript_LockTechInteraction_excludedUniqueBlocks_254, num2 + 1);
		}
		logic_uScript_LockTechInteraction_excludedUniqueBlocks_254[num2++] = local_ButtonBlock1_TankBlock;
		logic_uScript_LockTechInteraction_uScript_LockTechInteraction_254.In(logic_uScript_LockTechInteraction_tech_254, logic_uScript_LockTechInteraction_excludedBlocks_254, logic_uScript_LockTechInteraction_excludedUniqueBlocks_254);
		if (logic_uScript_LockTechInteraction_uScript_LockTechInteraction_254.Out)
		{
			Relay_In_53();
		}
	}

	private void Relay_In_255()
	{
		logic_uScriptAct_Passthrough_uScriptAct_Passthrough_255.In();
		if (logic_uScriptAct_Passthrough_uScriptAct_Passthrough_255.Out)
		{
			Relay_In_184();
		}
	}

	private void Relay_In_260()
	{
		logic_uScript_SetTankInvulnerable_tank_260 = local_ButtonBase3Tech_Tank;
		logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_260.In(logic_uScript_SetTankInvulnerable_invulnerable_260, logic_uScript_SetTankInvulnerable_tank_260);
		if (logic_uScript_SetTankInvulnerable_uScript_SetTankInvulnerable_260.Out)
		{
			Relay_In_65();
		}
	}

	private void Relay_In_264()
	{
		logic_uScriptCon_CompareBool_Bool_264 = local_Q1EnemyAlive_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_264.In(logic_uScriptCon_CompareBool_Bool_264);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_264.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_264.False;
		if (num)
		{
			Relay_In_309();
		}
		if (flag)
		{
			Relay_InitialSpawn_266();
		}
	}

	private void Relay_In_265()
	{
		int num = 0;
		Array enemyTechData = EnemyTechData;
		if (logic_uScript_GetAndCheckTechs_techData_265.Length != num + enemyTechData.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_265, num + enemyTechData.Length);
		}
		Array.Copy(enemyTechData, 0, logic_uScript_GetAndCheckTechs_techData_265, num, enemyTechData.Length);
		num += enemyTechData.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_265 = owner_Connection_267;
		int num2 = 0;
		Array array = local_Enemy_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_265.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_265, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techs_265, num2, array.Length);
		num2 += array.Length;
		logic_uScript_GetAndCheckTechs_Return_265 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_265.In(logic_uScript_GetAndCheckTechs_techData_265, logic_uScript_GetAndCheckTechs_ownerNode_265, ref logic_uScript_GetAndCheckTechs_techs_265);
		local_Enemy_TankArray = logic_uScript_GetAndCheckTechs_techs_265;
		if (logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_265.AllDead)
		{
			Relay_In_314();
		}
	}

	private void Relay_InitialSpawn_266()
	{
		int num = 0;
		Array enemyTechData = EnemyTechData;
		if (logic_uScript_SpawnTechsFromData_spawnData_266.Length != num + enemyTechData.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_266, num + enemyTechData.Length);
		}
		Array.Copy(enemyTechData, 0, logic_uScript_SpawnTechsFromData_spawnData_266, num, enemyTechData.Length);
		num += enemyTechData.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_266 = owner_Connection_263;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_266.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_266, logic_uScript_SpawnTechsFromData_ownerNode_266, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_266, logic_uScript_SpawnTechsFromData_allowResurrection_266);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_266.Out)
		{
			Relay_True_268();
		}
	}

	private void Relay_True_268()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_268.True(out logic_uScriptAct_SetBool_Target_268);
		local_Q1EnemyAlive_System_Boolean = logic_uScriptAct_SetBool_Target_268;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_268.Out)
		{
			Relay_In_265();
		}
	}

	private void Relay_False_268()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_268.False(out logic_uScriptAct_SetBool_Target_268);
		local_Q1EnemyAlive_System_Boolean = logic_uScriptAct_SetBool_Target_268;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_268.Out)
		{
			Relay_In_265();
		}
	}

	private void Relay_Save_Out_270()
	{
		Relay_Save_301();
	}

	private void Relay_Load_Out_270()
	{
		Relay_Load_301();
	}

	private void Relay_Restart_Out_270()
	{
		Relay_Set_False_301();
	}

	private void Relay_Save_270()
	{
		logic_SubGraph_SaveLoadBool_boolean_270 = local_Q1EnemyAlive_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_270 = local_Q1EnemyAlive_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_270.Save(ref logic_SubGraph_SaveLoadBool_boolean_270, logic_SubGraph_SaveLoadBool_boolAsVariable_270, logic_SubGraph_SaveLoadBool_uniqueID_270);
	}

	private void Relay_Load_270()
	{
		logic_SubGraph_SaveLoadBool_boolean_270 = local_Q1EnemyAlive_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_270 = local_Q1EnemyAlive_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_270.Load(ref logic_SubGraph_SaveLoadBool_boolean_270, logic_SubGraph_SaveLoadBool_boolAsVariable_270, logic_SubGraph_SaveLoadBool_uniqueID_270);
	}

	private void Relay_Set_True_270()
	{
		logic_SubGraph_SaveLoadBool_boolean_270 = local_Q1EnemyAlive_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_270 = local_Q1EnemyAlive_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_270.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_270, logic_SubGraph_SaveLoadBool_boolAsVariable_270, logic_SubGraph_SaveLoadBool_uniqueID_270);
	}

	private void Relay_Set_False_270()
	{
		logic_SubGraph_SaveLoadBool_boolean_270 = local_Q1EnemyAlive_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_270 = local_Q1EnemyAlive_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_270.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_270, logic_SubGraph_SaveLoadBool_boolAsVariable_270, logic_SubGraph_SaveLoadBool_uniqueID_270);
	}

	private void Relay_In_272()
	{
		int num = 0;
		if (logic_uScriptAct_Concatenate_A_272.Length <= num)
		{
			Array.Resize(ref logic_uScriptAct_Concatenate_A_272, num + 1);
		}
		logic_uScriptAct_Concatenate_A_272[num++] = local_275_System_String;
		int num2 = 0;
		if (logic_uScriptAct_Concatenate_B_272.Length <= num2)
		{
			Array.Resize(ref logic_uScriptAct_Concatenate_B_272, num2 + 1);
		}
		logic_uScriptAct_Concatenate_B_272[num2++] = local_QuestionsStage_System_Int32;
		logic_uScriptAct_Concatenate_uScriptAct_Concatenate_272.In(logic_uScriptAct_Concatenate_A_272, logic_uScriptAct_Concatenate_B_272, logic_uScriptAct_Concatenate_Separator_272, out logic_uScriptAct_Concatenate_Result_272);
		local_273_System_String = logic_uScriptAct_Concatenate_Result_272;
		if (logic_uScriptAct_Concatenate_uScriptAct_Concatenate_272.Out)
		{
			Relay_ShowLabel_274();
		}
	}

	private void Relay_ShowLabel_274()
	{
		logic_uScriptAct_PrintText_Text_274 = local_273_System_String;
		logic_uScriptAct_PrintText_uScriptAct_PrintText_274.ShowLabel(logic_uScriptAct_PrintText_Text_274, logic_uScriptAct_PrintText_FontSize_274, logic_uScriptAct_PrintText_FontStyle_274, logic_uScriptAct_PrintText_FontColor_274, logic_uScriptAct_PrintText_textAnchor_274, logic_uScriptAct_PrintText_EdgePadding_274, logic_uScriptAct_PrintText_time_274);
	}

	private void Relay_HideLabel_274()
	{
		logic_uScriptAct_PrintText_Text_274 = local_273_System_String;
		logic_uScriptAct_PrintText_uScriptAct_PrintText_274.HideLabel(logic_uScriptAct_PrintText_Text_274, logic_uScriptAct_PrintText_FontSize_274, logic_uScriptAct_PrintText_FontStyle_274, logic_uScriptAct_PrintText_FontColor_274, logic_uScriptAct_PrintText_textAnchor_274, logic_uScriptAct_PrintText_EdgePadding_274, logic_uScriptAct_PrintText_time_274);
	}

	private void Relay_InitialSpawn_277()
	{
		int num = 0;
		Array enemyTechData = EnemyTechData;
		if (logic_uScript_SpawnTechsFromData_spawnData_277.Length != num + enemyTechData.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_277, num + enemyTechData.Length);
		}
		Array.Copy(enemyTechData, 0, logic_uScript_SpawnTechsFromData_spawnData_277, num, enemyTechData.Length);
		num += enemyTechData.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_277 = owner_Connection_282;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_277.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_277, logic_uScript_SpawnTechsFromData_ownerNode_277, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_277, logic_uScript_SpawnTechsFromData_allowResurrection_277);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_277.Out)
		{
			Relay_True_281();
		}
	}

	private void Relay_In_278()
	{
		int num = 0;
		Array enemyTechData = EnemyTechData;
		if (logic_uScript_GetAndCheckTechs_techData_278.Length != num + enemyTechData.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_278, num + enemyTechData.Length);
		}
		Array.Copy(enemyTechData, 0, logic_uScript_GetAndCheckTechs_techData_278, num, enemyTechData.Length);
		num += enemyTechData.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_278 = owner_Connection_279;
		int num2 = 0;
		Array array = local_Enemy_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_278.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_278, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techs_278, num2, array.Length);
		num2 += array.Length;
		logic_uScript_GetAndCheckTechs_Return_278 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_278.In(logic_uScript_GetAndCheckTechs_techData_278, logic_uScript_GetAndCheckTechs_ownerNode_278, ref logic_uScript_GetAndCheckTechs_techs_278);
		local_Enemy_TankArray = logic_uScript_GetAndCheckTechs_techs_278;
		if (logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_278.AllDead)
		{
			Relay_In_709();
		}
	}

	private void Relay_True_281()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_281.True(out logic_uScriptAct_SetBool_Target_281);
		local_Q2EnemyAlive_System_Boolean = logic_uScriptAct_SetBool_Target_281;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_281.Out)
		{
			Relay_In_278();
		}
	}

	private void Relay_False_281()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_281.False(out logic_uScriptAct_SetBool_Target_281);
		local_Q2EnemyAlive_System_Boolean = logic_uScriptAct_SetBool_Target_281;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_281.Out)
		{
			Relay_In_278();
		}
	}

	private void Relay_In_283()
	{
		logic_uScriptCon_CompareBool_Bool_283 = local_Q2EnemyAlive_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_283.In(logic_uScriptCon_CompareBool_Bool_283);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_283.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_283.False;
		if (num)
		{
			Relay_In_320();
		}
		if (flag)
		{
			Relay_InitialSpawn_277();
		}
	}

	private void Relay_In_291()
	{
		logic_uScriptCon_CompareBool_Bool_291 = local_Q6EnemyAlive_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_291.In(logic_uScriptCon_CompareBool_Bool_291);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_291.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_291.False;
		if (num)
		{
			Relay_In_368();
		}
		if (flag)
		{
			Relay_InitialSpawn_295();
		}
	}

	private void Relay_True_293()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_293.True(out logic_uScriptAct_SetBool_Target_293);
		local_Q6EnemyAlive_System_Boolean = logic_uScriptAct_SetBool_Target_293;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_293.Out)
		{
			Relay_In_297();
		}
	}

	private void Relay_False_293()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_293.False(out logic_uScriptAct_SetBool_Target_293);
		local_Q6EnemyAlive_System_Boolean = logic_uScriptAct_SetBool_Target_293;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_293.Out)
		{
			Relay_In_297();
		}
	}

	private void Relay_InitialSpawn_295()
	{
		int num = 0;
		Array enemyTechData = EnemyTechData;
		if (logic_uScript_SpawnTechsFromData_spawnData_295.Length != num + enemyTechData.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_295, num + enemyTechData.Length);
		}
		Array.Copy(enemyTechData, 0, logic_uScript_SpawnTechsFromData_spawnData_295, num, enemyTechData.Length);
		num += enemyTechData.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_295 = owner_Connection_296;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_295.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_295, logic_uScript_SpawnTechsFromData_ownerNode_295, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_295, logic_uScript_SpawnTechsFromData_allowResurrection_295);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_295.Out)
		{
			Relay_True_293();
		}
	}

	private void Relay_In_297()
	{
		int num = 0;
		Array enemyTechData = EnemyTechData;
		if (logic_uScript_GetAndCheckTechs_techData_297.Length != num + enemyTechData.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_297, num + enemyTechData.Length);
		}
		Array.Copy(enemyTechData, 0, logic_uScript_GetAndCheckTechs_techData_297, num, enemyTechData.Length);
		num += enemyTechData.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_297 = owner_Connection_289;
		int num2 = 0;
		Array array = local_Enemy_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_297.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_297, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techs_297, num2, array.Length);
		num2 += array.Length;
		logic_uScript_GetAndCheckTechs_Return_297 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_297.In(logic_uScript_GetAndCheckTechs_techData_297, logic_uScript_GetAndCheckTechs_ownerNode_297, ref logic_uScript_GetAndCheckTechs_techs_297);
		local_Enemy_TankArray = logic_uScript_GetAndCheckTechs_techs_297;
		if (logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_297.AllDead)
		{
			Relay_In_326();
		}
	}

	private void Relay_Save_Out_301()
	{
		Relay_Save_303();
	}

	private void Relay_Load_Out_301()
	{
		Relay_Load_303();
	}

	private void Relay_Restart_Out_301()
	{
		Relay_Set_False_303();
	}

	private void Relay_Save_301()
	{
		logic_SubGraph_SaveLoadBool_boolean_301 = local_Q2EnemyAlive_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_301 = local_Q2EnemyAlive_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_301.Save(ref logic_SubGraph_SaveLoadBool_boolean_301, logic_SubGraph_SaveLoadBool_boolAsVariable_301, logic_SubGraph_SaveLoadBool_uniqueID_301);
	}

	private void Relay_Load_301()
	{
		logic_SubGraph_SaveLoadBool_boolean_301 = local_Q2EnemyAlive_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_301 = local_Q2EnemyAlive_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_301.Load(ref logic_SubGraph_SaveLoadBool_boolean_301, logic_SubGraph_SaveLoadBool_boolAsVariable_301, logic_SubGraph_SaveLoadBool_uniqueID_301);
	}

	private void Relay_Set_True_301()
	{
		logic_SubGraph_SaveLoadBool_boolean_301 = local_Q2EnemyAlive_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_301 = local_Q2EnemyAlive_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_301.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_301, logic_SubGraph_SaveLoadBool_boolAsVariable_301, logic_SubGraph_SaveLoadBool_uniqueID_301);
	}

	private void Relay_Set_False_301()
	{
		logic_SubGraph_SaveLoadBool_boolean_301 = local_Q2EnemyAlive_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_301 = local_Q2EnemyAlive_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_301.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_301, logic_SubGraph_SaveLoadBool_boolAsVariable_301, logic_SubGraph_SaveLoadBool_uniqueID_301);
	}

	private void Relay_Save_Out_303()
	{
		Relay_Save_403();
	}

	private void Relay_Load_Out_303()
	{
		Relay_Load_403();
	}

	private void Relay_Restart_Out_303()
	{
		Relay_Set_False_403();
	}

	private void Relay_Save_303()
	{
		logic_SubGraph_SaveLoadBool_boolean_303 = local_Q3EnemyAlive_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_303 = local_Q3EnemyAlive_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_303.Save(ref logic_SubGraph_SaveLoadBool_boolean_303, logic_SubGraph_SaveLoadBool_boolAsVariable_303, logic_SubGraph_SaveLoadBool_uniqueID_303);
	}

	private void Relay_Load_303()
	{
		logic_SubGraph_SaveLoadBool_boolean_303 = local_Q3EnemyAlive_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_303 = local_Q3EnemyAlive_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_303.Load(ref logic_SubGraph_SaveLoadBool_boolean_303, logic_SubGraph_SaveLoadBool_boolAsVariable_303, logic_SubGraph_SaveLoadBool_uniqueID_303);
	}

	private void Relay_Set_True_303()
	{
		logic_SubGraph_SaveLoadBool_boolean_303 = local_Q3EnemyAlive_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_303 = local_Q3EnemyAlive_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_303.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_303, logic_SubGraph_SaveLoadBool_boolAsVariable_303, logic_SubGraph_SaveLoadBool_uniqueID_303);
	}

	private void Relay_Set_False_303()
	{
		logic_SubGraph_SaveLoadBool_boolean_303 = local_Q3EnemyAlive_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_303 = local_Q3EnemyAlive_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_303.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_303, logic_SubGraph_SaveLoadBool_boolAsVariable_303, logic_SubGraph_SaveLoadBool_uniqueID_303);
	}

	private void Relay_True_305()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_305.True(out logic_uScriptAct_SetBool_Target_305);
		local_NearNPC_System_Boolean = logic_uScriptAct_SetBool_Target_305;
	}

	private void Relay_False_305()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_305.False(out logic_uScriptAct_SetBool_Target_305);
		local_NearNPC_System_Boolean = logic_uScriptAct_SetBool_Target_305;
	}

	private void Relay_In_309()
	{
		logic_uScript_AddMessage_messageData_309 = msgSpawnMinion;
		logic_uScript_AddMessage_speaker_309 = messageSpeaker;
		logic_uScript_AddMessage_Return_309 = logic_uScript_AddMessage_uScript_AddMessage_309.In(logic_uScript_AddMessage_messageData_309, logic_uScript_AddMessage_speaker_309);
		if (logic_uScript_AddMessage_uScript_AddMessage_309.Out)
		{
			Relay_In_265();
		}
	}

	private void Relay_In_313()
	{
		logic_uScript_AddMessage_messageData_313 = msgMinionDefeated;
		logic_uScript_AddMessage_speaker_313 = messageSpeaker;
		logic_uScript_AddMessage_Return_313 = logic_uScript_AddMessage_uScript_AddMessage_313.In(logic_uScript_AddMessage_messageData_313, logic_uScript_AddMessage_speaker_313);
		if (logic_uScript_AddMessage_uScript_AddMessage_313.Out)
		{
			Relay_In_316();
		}
	}

	private void Relay_In_314()
	{
		logic_uScriptAct_AddInt_v2_A_314 = local_QuestionOne_System_Int32;
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_314.In(logic_uScriptAct_AddInt_v2_A_314, logic_uScriptAct_AddInt_v2_B_314, out logic_uScriptAct_AddInt_v2_IntResult_314, out logic_uScriptAct_AddInt_v2_FloatResult_314);
		local_QuestionOne_System_Int32 = logic_uScriptAct_AddInt_v2_IntResult_314;
	}

	private void Relay_In_316()
	{
		logic_uScript_Wait_seconds_316 = WaitingTime;
		logic_uScript_Wait_uScript_Wait_316.In(logic_uScript_Wait_seconds_316, logic_uScript_Wait_repeat_316);
		if (logic_uScript_Wait_uScript_Wait_316.Waited)
		{
			Relay_In_706();
		}
	}

	private void Relay_In_320()
	{
		logic_uScript_AddMessage_messageData_320 = msgSpawnMinion;
		logic_uScript_AddMessage_speaker_320 = messageSpeaker;
		logic_uScript_AddMessage_Return_320 = logic_uScript_AddMessage_uScript_AddMessage_320.In(logic_uScript_AddMessage_messageData_320, logic_uScript_AddMessage_speaker_320);
		if (logic_uScript_AddMessage_uScript_AddMessage_320.Out)
		{
			Relay_In_278();
		}
	}

	private void Relay_In_321()
	{
		logic_uScript_Wait_seconds_321 = WaitingTime;
		logic_uScript_Wait_uScript_Wait_321.In(logic_uScript_Wait_seconds_321, logic_uScript_Wait_repeat_321);
		if (logic_uScript_Wait_uScript_Wait_321.Waited)
		{
			Relay_In_972();
		}
	}

	private void Relay_In_323()
	{
		logic_uScript_AddMessage_messageData_323 = msgMinionDefeated;
		logic_uScript_AddMessage_speaker_323 = messageSpeaker;
		logic_uScript_AddMessage_Return_323 = logic_uScript_AddMessage_uScript_AddMessage_323.In(logic_uScript_AddMessage_messageData_323, logic_uScript_AddMessage_speaker_323);
		if (logic_uScript_AddMessage_uScript_AddMessage_323.Out)
		{
			Relay_In_321();
		}
	}

	private void Relay_In_326()
	{
		logic_uScriptAct_AddInt_v2_A_326 = local_QuestionSix_System_Int32;
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_326.In(logic_uScriptAct_AddInt_v2_A_326, logic_uScriptAct_AddInt_v2_B_326, out logic_uScriptAct_AddInt_v2_IntResult_326, out logic_uScriptAct_AddInt_v2_FloatResult_326);
		local_QuestionSix_System_Int32 = logic_uScriptAct_AddInt_v2_IntResult_326;
	}

	private void Relay_In_329()
	{
		logic_uScript_RemoveTech_tech_329 = local_ButtonBase4Tech_Tank;
		logic_uScript_RemoveTech_uScript_RemoveTech_329.In(logic_uScript_RemoveTech_tech_329);
		if (logic_uScript_RemoveTech_uScript_RemoveTech_329.Out)
		{
			Relay_In_353();
		}
	}

	private void Relay_In_334()
	{
		logic_uScript_SpawnVFX_ownerNode_334 = owner_Connection_340;
		logic_uScript_SpawnVFX_vfxToSpawn_334 = NPCDespawnParticleEffect;
		logic_uScript_SpawnVFX_spawnPosName_334 = ButtonBase2VFXSpawn;
		logic_uScript_SpawnVFX_uScript_SpawnVFX_334.In(logic_uScript_SpawnVFX_ownerNode_334, logic_uScript_SpawnVFX_vfxToSpawn_334, logic_uScript_SpawnVFX_spawnPosName_334);
		if (logic_uScript_SpawnVFX_uScript_SpawnVFX_334.Out)
		{
			Relay_In_345();
		}
	}

	private void Relay_In_339()
	{
		logic_uScript_SpawnVFX_ownerNode_339 = owner_Connection_328;
		logic_uScript_SpawnVFX_vfxToSpawn_339 = NPCDespawnParticleEffect;
		logic_uScript_SpawnVFX_spawnPosName_339 = ButtonBase4VFXSpawn;
		logic_uScript_SpawnVFX_uScript_SpawnVFX_339.In(logic_uScript_SpawnVFX_ownerNode_339, logic_uScript_SpawnVFX_vfxToSpawn_339, logic_uScript_SpawnVFX_spawnPosName_339);
		if (logic_uScript_SpawnVFX_uScript_SpawnVFX_339.Out)
		{
			Relay_In_329();
		}
	}

	private void Relay_In_345()
	{
		logic_uScript_RemoveTech_tech_345 = local_ButtonBase2Tech_Tank;
		logic_uScript_RemoveTech_uScript_RemoveTech_345.In(logic_uScript_RemoveTech_tech_345);
		if (logic_uScript_RemoveTech_uScript_RemoveTech_345.Out)
		{
			Relay_In_351();
		}
	}

	private void Relay_In_347()
	{
		logic_uScript_AddMessage_messageData_347 = msgEncounterComplete;
		logic_uScript_AddMessage_speaker_347 = messageSpeaker;
		logic_uScript_AddMessage_Return_347 = logic_uScript_AddMessage_uScript_AddMessage_347.In(logic_uScript_AddMessage_messageData_347, logic_uScript_AddMessage_speaker_347);
		if (logic_uScript_AddMessage_uScript_AddMessage_347.Shown)
		{
			Relay_In_360();
		}
	}

	private void Relay_In_348()
	{
		logic_uScript_RemoveTech_tech_348 = local_ButtonBase1Tech_Tank;
		logic_uScript_RemoveTech_uScript_RemoveTech_348.In(logic_uScript_RemoveTech_tech_348);
		if (logic_uScript_RemoveTech_uScript_RemoveTech_348.Out)
		{
			Relay_In_334();
		}
	}

	private void Relay_In_351()
	{
		logic_uScript_SpawnVFX_ownerNode_351 = owner_Connection_342;
		logic_uScript_SpawnVFX_vfxToSpawn_351 = NPCDespawnParticleEffect;
		logic_uScript_SpawnVFX_spawnPosName_351 = ButtonBase3VFXSpawn;
		logic_uScript_SpawnVFX_uScript_SpawnVFX_351.In(logic_uScript_SpawnVFX_ownerNode_351, logic_uScript_SpawnVFX_vfxToSpawn_351, logic_uScript_SpawnVFX_spawnPosName_351);
		if (logic_uScript_SpawnVFX_uScript_SpawnVFX_351.Out)
		{
			Relay_In_359();
		}
	}

	private void Relay_In_353()
	{
		logic_uScript_FlyTechUpAndAway_tech_353 = local_NPCTech_Tank;
		logic_uScript_FlyTechUpAndAway_aiTree_353 = NPCFlyAwayAI;
		logic_uScript_FlyTechUpAndAway_removalParticles_353 = NPCDespawnParticleEffect;
		logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_353.In(logic_uScript_FlyTechUpAndAway_tech_353, logic_uScript_FlyTechUpAndAway_maxLifetime_353, logic_uScript_FlyTechUpAndAway_targetHeight_353, logic_uScript_FlyTechUpAndAway_aiTree_353, logic_uScript_FlyTechUpAndAway_removalParticles_353);
		if (logic_uScript_FlyTechUpAndAway_uScript_FlyTechUpAndAway_353.Out)
		{
			Relay_Succeed_358();
		}
	}

	private void Relay_Succeed_358()
	{
		logic_uScript_FinishEncounter_owner_358 = owner_Connection_344;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_358.Succeed(logic_uScript_FinishEncounter_owner_358);
	}

	private void Relay_Fail_358()
	{
		logic_uScript_FinishEncounter_owner_358 = owner_Connection_344;
		logic_uScript_FinishEncounter_uScript_FinishEncounter_358.Fail(logic_uScript_FinishEncounter_owner_358);
	}

	private void Relay_In_359()
	{
		logic_uScript_RemoveTech_tech_359 = local_ButtonBase3Tech_Tank;
		logic_uScript_RemoveTech_uScript_RemoveTech_359.In(logic_uScript_RemoveTech_tech_359);
		if (logic_uScript_RemoveTech_uScript_RemoveTech_359.Out)
		{
			Relay_In_339();
		}
	}

	private void Relay_In_360()
	{
		logic_uScript_SpawnVFX_ownerNode_360 = owner_Connection_337;
		logic_uScript_SpawnVFX_vfxToSpawn_360 = NPCDespawnParticleEffect;
		logic_uScript_SpawnVFX_spawnPosName_360 = ButtonBase1VFXSpawn;
		logic_uScript_SpawnVFX_uScript_SpawnVFX_360.In(logic_uScript_SpawnVFX_ownerNode_360, logic_uScript_SpawnVFX_vfxToSpawn_360, logic_uScript_SpawnVFX_spawnPosName_360);
		if (logic_uScript_SpawnVFX_uScript_SpawnVFX_360.Out)
		{
			Relay_In_348();
		}
	}

	private void Relay_In_361()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_361.In(logic_uScriptAct_SetInt_Value_361, out logic_uScriptAct_SetInt_Target_361);
		local_QuestionSix_System_Int32 = logic_uScriptAct_SetInt_Target_361;
	}

	private void Relay_Save_Out_363()
	{
		Relay_Save_365();
	}

	private void Relay_Load_Out_363()
	{
		Relay_Load_365();
	}

	private void Relay_Restart_Out_363()
	{
		Relay_Set_False_365();
	}

	private void Relay_Save_363()
	{
		logic_SubGraph_SaveLoadBool_boolean_363 = local_Question01WrongAnswers_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_363 = local_Question01WrongAnswers_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_363.Save(ref logic_SubGraph_SaveLoadBool_boolean_363, logic_SubGraph_SaveLoadBool_boolAsVariable_363, logic_SubGraph_SaveLoadBool_uniqueID_363);
	}

	private void Relay_Load_363()
	{
		logic_SubGraph_SaveLoadBool_boolean_363 = local_Question01WrongAnswers_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_363 = local_Question01WrongAnswers_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_363.Load(ref logic_SubGraph_SaveLoadBool_boolean_363, logic_SubGraph_SaveLoadBool_boolAsVariable_363, logic_SubGraph_SaveLoadBool_uniqueID_363);
	}

	private void Relay_Set_True_363()
	{
		logic_SubGraph_SaveLoadBool_boolean_363 = local_Question01WrongAnswers_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_363 = local_Question01WrongAnswers_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_363.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_363, logic_SubGraph_SaveLoadBool_boolAsVariable_363, logic_SubGraph_SaveLoadBool_uniqueID_363);
	}

	private void Relay_Set_False_363()
	{
		logic_SubGraph_SaveLoadBool_boolean_363 = local_Question01WrongAnswers_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_363 = local_Question01WrongAnswers_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_363.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_363, logic_SubGraph_SaveLoadBool_boolAsVariable_363, logic_SubGraph_SaveLoadBool_uniqueID_363);
	}

	private void Relay_Save_Out_365()
	{
		Relay_Save_366();
	}

	private void Relay_Load_Out_365()
	{
		Relay_Load_366();
	}

	private void Relay_Restart_Out_365()
	{
		Relay_Set_False_366();
	}

	private void Relay_Save_365()
	{
		logic_SubGraph_SaveLoadBool_boolean_365 = local_Question02WrongAnswers_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_365 = local_Question02WrongAnswers_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_365.Save(ref logic_SubGraph_SaveLoadBool_boolean_365, logic_SubGraph_SaveLoadBool_boolAsVariable_365, logic_SubGraph_SaveLoadBool_uniqueID_365);
	}

	private void Relay_Load_365()
	{
		logic_SubGraph_SaveLoadBool_boolean_365 = local_Question02WrongAnswers_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_365 = local_Question02WrongAnswers_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_365.Load(ref logic_SubGraph_SaveLoadBool_boolean_365, logic_SubGraph_SaveLoadBool_boolAsVariable_365, logic_SubGraph_SaveLoadBool_uniqueID_365);
	}

	private void Relay_Set_True_365()
	{
		logic_SubGraph_SaveLoadBool_boolean_365 = local_Question02WrongAnswers_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_365 = local_Question02WrongAnswers_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_365.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_365, logic_SubGraph_SaveLoadBool_boolAsVariable_365, logic_SubGraph_SaveLoadBool_uniqueID_365);
	}

	private void Relay_Set_False_365()
	{
		logic_SubGraph_SaveLoadBool_boolean_365 = local_Question02WrongAnswers_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_365 = local_Question02WrongAnswers_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_365.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_365, logic_SubGraph_SaveLoadBool_boolAsVariable_365, logic_SubGraph_SaveLoadBool_uniqueID_365);
	}

	private void Relay_Save_Out_366()
	{
		Relay_Save_411();
	}

	private void Relay_Load_Out_366()
	{
		Relay_Load_411();
	}

	private void Relay_Restart_Out_366()
	{
		Relay_Set_False_411();
	}

	private void Relay_Save_366()
	{
		logic_SubGraph_SaveLoadBool_boolean_366 = local_Question03WrongAnswers_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_366 = local_Question03WrongAnswers_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_366.Save(ref logic_SubGraph_SaveLoadBool_boolean_366, logic_SubGraph_SaveLoadBool_boolAsVariable_366, logic_SubGraph_SaveLoadBool_uniqueID_366);
	}

	private void Relay_Load_366()
	{
		logic_SubGraph_SaveLoadBool_boolean_366 = local_Question03WrongAnswers_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_366 = local_Question03WrongAnswers_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_366.Load(ref logic_SubGraph_SaveLoadBool_boolean_366, logic_SubGraph_SaveLoadBool_boolAsVariable_366, logic_SubGraph_SaveLoadBool_uniqueID_366);
	}

	private void Relay_Set_True_366()
	{
		logic_SubGraph_SaveLoadBool_boolean_366 = local_Question03WrongAnswers_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_366 = local_Question03WrongAnswers_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_366.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_366, logic_SubGraph_SaveLoadBool_boolAsVariable_366, logic_SubGraph_SaveLoadBool_uniqueID_366);
	}

	private void Relay_Set_False_366()
	{
		logic_SubGraph_SaveLoadBool_boolean_366 = local_Question03WrongAnswers_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_366 = local_Question03WrongAnswers_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_366.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_366, logic_SubGraph_SaveLoadBool_boolAsVariable_366, logic_SubGraph_SaveLoadBool_uniqueID_366);
	}

	private void Relay_In_368()
	{
		logic_uScript_AddMessage_messageData_368 = msgSpawnMinion;
		logic_uScript_AddMessage_speaker_368 = messageSpeaker;
		logic_uScript_AddMessage_Return_368 = logic_uScript_AddMessage_uScript_AddMessage_368.In(logic_uScript_AddMessage_messageData_368, logic_uScript_AddMessage_speaker_368);
		if (logic_uScript_AddMessage_uScript_AddMessage_368.Out)
		{
			Relay_In_297();
		}
	}

	private void Relay_Out_372()
	{
	}

	private void Relay_In_372()
	{
		logic_SubGraph_LoadObjectiveStates_currentObjective_372 = local_Stage_System_Int32;
		logic_SubGraph_LoadObjectiveStates_SubGraph_LoadObjectiveStates_372.In(logic_SubGraph_LoadObjectiveStates_currentObjective_372);
	}

	private void Relay_True_374()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_374.True(out logic_uScriptAct_SetBool_Target_374);
		local_msgIntro_System_Boolean = logic_uScriptAct_SetBool_Target_374;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_374.Out)
		{
			Relay_In_203();
		}
	}

	private void Relay_False_374()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_374.False(out logic_uScriptAct_SetBool_Target_374);
		local_msgIntro_System_Boolean = logic_uScriptAct_SetBool_Target_374;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_374.Out)
		{
			Relay_In_203();
		}
	}

	private void Relay_In_375()
	{
		logic_uScript_IsPlayerInRangeOfTech_tech_375 = local_NPCTech_Tank;
		logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_375.In(logic_uScript_IsPlayerInRangeOfTech_tech_375, logic_uScript_IsPlayerInRangeOfTech_range_375, logic_uScript_IsPlayerInRangeOfTech_techs_375);
		if (logic_uScript_IsPlayerInRangeOfTech_uScript_IsPlayerInRangeOfTech_375.InRange)
		{
			Relay_In_154();
		}
	}

	private void Relay_Save_Out_378()
	{
		Relay_Save_52();
	}

	private void Relay_Load_Out_378()
	{
		Relay_Load_52();
	}

	private void Relay_Restart_Out_378()
	{
		Relay_Set_False_52();
	}

	private void Relay_Save_378()
	{
		logic_SubGraph_SaveLoadBool_boolean_378 = local_msgBaseFoundShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_378 = local_msgBaseFoundShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_378.Save(ref logic_SubGraph_SaveLoadBool_boolean_378, logic_SubGraph_SaveLoadBool_boolAsVariable_378, logic_SubGraph_SaveLoadBool_uniqueID_378);
	}

	private void Relay_Load_378()
	{
		logic_SubGraph_SaveLoadBool_boolean_378 = local_msgBaseFoundShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_378 = local_msgBaseFoundShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_378.Load(ref logic_SubGraph_SaveLoadBool_boolean_378, logic_SubGraph_SaveLoadBool_boolAsVariable_378, logic_SubGraph_SaveLoadBool_uniqueID_378);
	}

	private void Relay_Set_True_378()
	{
		logic_SubGraph_SaveLoadBool_boolean_378 = local_msgBaseFoundShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_378 = local_msgBaseFoundShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_378.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_378, logic_SubGraph_SaveLoadBool_boolAsVariable_378, logic_SubGraph_SaveLoadBool_uniqueID_378);
	}

	private void Relay_Set_False_378()
	{
		logic_SubGraph_SaveLoadBool_boolean_378 = local_msgBaseFoundShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_378 = local_msgBaseFoundShown_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_378.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_378, logic_SubGraph_SaveLoadBool_boolAsVariable_378, logic_SubGraph_SaveLoadBool_uniqueID_378);
	}

	private void Relay_In_380()
	{
		logic_uScriptCon_CompareBool_Bool_380 = local_msgBaseFoundShown_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_380.In(logic_uScriptCon_CompareBool_Bool_380);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_380.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_380.False;
		if (num)
		{
			Relay_In_386();
		}
		if (flag)
		{
			Relay_In_387();
		}
	}

	private void Relay_True_384()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_384.True(out logic_uScriptAct_SetBool_Target_384);
		local_msgBaseFoundShown_System_Boolean = logic_uScriptAct_SetBool_Target_384;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_384.Out)
		{
			Relay_In_386();
		}
	}

	private void Relay_False_384()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_384.False(out logic_uScriptAct_SetBool_Target_384);
		local_msgBaseFoundShown_System_Boolean = logic_uScriptAct_SetBool_Target_384;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_384.Out)
		{
			Relay_In_386();
		}
	}

	private void Relay_Out_386()
	{
	}

	private void Relay_In_386()
	{
		logic_SubGraph_CompleteObjectiveStage_objectiveStage_386 = local_Stage_System_Int32;
		logic_SubGraph_CompleteObjectiveStage_SubGraph_CompleteObjectiveStage_386.In(ref logic_SubGraph_CompleteObjectiveStage_objectiveStage_386, logic_SubGraph_CompleteObjectiveStage_isFinalObjective_386);
	}

	private void Relay_In_387()
	{
		logic_uScript_AddMessage_messageData_387 = msg02BaseFound;
		logic_uScript_AddMessage_speaker_387 = messageSpeaker;
		logic_uScript_AddMessage_Return_387 = logic_uScript_AddMessage_uScript_AddMessage_387.In(logic_uScript_AddMessage_messageData_387, logic_uScript_AddMessage_speaker_387);
		if (logic_uScript_AddMessage_uScript_AddMessage_387.Shown)
		{
			Relay_True_384();
		}
	}

	private void Relay_Output1_389()
	{
		Relay_In_380();
	}

	private void Relay_Output2_389()
	{
		Relay_In_543();
	}

	private void Relay_Output3_389()
	{
	}

	private void Relay_Output4_389()
	{
	}

	private void Relay_Output5_389()
	{
	}

	private void Relay_Output6_389()
	{
	}

	private void Relay_Output7_389()
	{
	}

	private void Relay_Output8_389()
	{
	}

	private void Relay_In_389()
	{
		logic_uScriptCon_ManualSwitch_CurrentOutput_389 = local_Stage_System_Int32;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_389.In(logic_uScriptCon_ManualSwitch_CurrentOutput_389);
	}

	private void Relay_Save_Out_397()
	{
		Relay_Save_399();
	}

	private void Relay_Load_Out_397()
	{
		Relay_Load_399();
	}

	private void Relay_Restart_Out_397()
	{
		Relay_Restart_399();
	}

	private void Relay_Save_397()
	{
		logic_SubGraph_SaveLoadInt_integer_397 = local_QuestionFour_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_397 = local_QuestionFour_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_397.Save(logic_SubGraph_SaveLoadInt_restartValue_397, ref logic_SubGraph_SaveLoadInt_integer_397, logic_SubGraph_SaveLoadInt_intAsVariable_397, logic_SubGraph_SaveLoadInt_uniqueID_397);
	}

	private void Relay_Load_397()
	{
		logic_SubGraph_SaveLoadInt_integer_397 = local_QuestionFour_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_397 = local_QuestionFour_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_397.Load(logic_SubGraph_SaveLoadInt_restartValue_397, ref logic_SubGraph_SaveLoadInt_integer_397, logic_SubGraph_SaveLoadInt_intAsVariable_397, logic_SubGraph_SaveLoadInt_uniqueID_397);
	}

	private void Relay_Restart_397()
	{
		logic_SubGraph_SaveLoadInt_integer_397 = local_QuestionFour_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_397 = local_QuestionFour_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_397.Restart(logic_SubGraph_SaveLoadInt_restartValue_397, ref logic_SubGraph_SaveLoadInt_integer_397, logic_SubGraph_SaveLoadInt_intAsVariable_397, logic_SubGraph_SaveLoadInt_uniqueID_397);
	}

	private void Relay_Save_Out_399()
	{
		Relay_Save_401();
	}

	private void Relay_Load_Out_399()
	{
		Relay_Load_401();
	}

	private void Relay_Restart_Out_399()
	{
		Relay_Restart_401();
	}

	private void Relay_Save_399()
	{
		logic_SubGraph_SaveLoadInt_integer_399 = local_QuestionFive_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_399 = local_QuestionFive_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_399.Save(logic_SubGraph_SaveLoadInt_restartValue_399, ref logic_SubGraph_SaveLoadInt_integer_399, logic_SubGraph_SaveLoadInt_intAsVariable_399, logic_SubGraph_SaveLoadInt_uniqueID_399);
	}

	private void Relay_Load_399()
	{
		logic_SubGraph_SaveLoadInt_integer_399 = local_QuestionFive_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_399 = local_QuestionFive_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_399.Load(logic_SubGraph_SaveLoadInt_restartValue_399, ref logic_SubGraph_SaveLoadInt_integer_399, logic_SubGraph_SaveLoadInt_intAsVariable_399, logic_SubGraph_SaveLoadInt_uniqueID_399);
	}

	private void Relay_Restart_399()
	{
		logic_SubGraph_SaveLoadInt_integer_399 = local_QuestionFive_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_399 = local_QuestionFive_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_399.Restart(logic_SubGraph_SaveLoadInt_restartValue_399, ref logic_SubGraph_SaveLoadInt_integer_399, logic_SubGraph_SaveLoadInt_intAsVariable_399, logic_SubGraph_SaveLoadInt_uniqueID_399);
	}

	private void Relay_Save_Out_401()
	{
		Relay_Save_249();
	}

	private void Relay_Load_Out_401()
	{
		Relay_Load_249();
	}

	private void Relay_Restart_Out_401()
	{
		Relay_Set_False_249();
	}

	private void Relay_Save_401()
	{
		logic_SubGraph_SaveLoadInt_integer_401 = local_QuestionSix_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_401 = local_QuestionSix_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_401.Save(logic_SubGraph_SaveLoadInt_restartValue_401, ref logic_SubGraph_SaveLoadInt_integer_401, logic_SubGraph_SaveLoadInt_intAsVariable_401, logic_SubGraph_SaveLoadInt_uniqueID_401);
	}

	private void Relay_Load_401()
	{
		logic_SubGraph_SaveLoadInt_integer_401 = local_QuestionSix_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_401 = local_QuestionSix_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_401.Load(logic_SubGraph_SaveLoadInt_restartValue_401, ref logic_SubGraph_SaveLoadInt_integer_401, logic_SubGraph_SaveLoadInt_intAsVariable_401, logic_SubGraph_SaveLoadInt_uniqueID_401);
	}

	private void Relay_Restart_401()
	{
		logic_SubGraph_SaveLoadInt_integer_401 = local_QuestionSix_System_Int32;
		logic_SubGraph_SaveLoadInt_intAsVariable_401 = local_QuestionSix_System_Int32;
		logic_SubGraph_SaveLoadInt_SubGraph_SaveLoadInt_401.Restart(logic_SubGraph_SaveLoadInt_restartValue_401, ref logic_SubGraph_SaveLoadInt_integer_401, logic_SubGraph_SaveLoadInt_intAsVariable_401, logic_SubGraph_SaveLoadInt_uniqueID_401);
	}

	private void Relay_Save_Out_403()
	{
		Relay_Save_405();
	}

	private void Relay_Load_Out_403()
	{
		Relay_Load_405();
	}

	private void Relay_Restart_Out_403()
	{
		Relay_Set_False_405();
	}

	private void Relay_Save_403()
	{
		logic_SubGraph_SaveLoadBool_boolean_403 = local_Q4EnemyAlive_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_403 = local_Q4EnemyAlive_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_403.Save(ref logic_SubGraph_SaveLoadBool_boolean_403, logic_SubGraph_SaveLoadBool_boolAsVariable_403, logic_SubGraph_SaveLoadBool_uniqueID_403);
	}

	private void Relay_Load_403()
	{
		logic_SubGraph_SaveLoadBool_boolean_403 = local_Q4EnemyAlive_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_403 = local_Q4EnemyAlive_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_403.Load(ref logic_SubGraph_SaveLoadBool_boolean_403, logic_SubGraph_SaveLoadBool_boolAsVariable_403, logic_SubGraph_SaveLoadBool_uniqueID_403);
	}

	private void Relay_Set_True_403()
	{
		logic_SubGraph_SaveLoadBool_boolean_403 = local_Q4EnemyAlive_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_403 = local_Q4EnemyAlive_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_403.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_403, logic_SubGraph_SaveLoadBool_boolAsVariable_403, logic_SubGraph_SaveLoadBool_uniqueID_403);
	}

	private void Relay_Set_False_403()
	{
		logic_SubGraph_SaveLoadBool_boolean_403 = local_Q4EnemyAlive_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_403 = local_Q4EnemyAlive_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_403.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_403, logic_SubGraph_SaveLoadBool_boolAsVariable_403, logic_SubGraph_SaveLoadBool_uniqueID_403);
	}

	private void Relay_Save_Out_405()
	{
		Relay_Save_407();
	}

	private void Relay_Load_Out_405()
	{
		Relay_Load_407();
	}

	private void Relay_Restart_Out_405()
	{
		Relay_Set_False_407();
	}

	private void Relay_Save_405()
	{
		logic_SubGraph_SaveLoadBool_boolean_405 = local_Q5EnemyAlive_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_405 = local_Q5EnemyAlive_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_405.Save(ref logic_SubGraph_SaveLoadBool_boolean_405, logic_SubGraph_SaveLoadBool_boolAsVariable_405, logic_SubGraph_SaveLoadBool_uniqueID_405);
	}

	private void Relay_Load_405()
	{
		logic_SubGraph_SaveLoadBool_boolean_405 = local_Q5EnemyAlive_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_405 = local_Q5EnemyAlive_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_405.Load(ref logic_SubGraph_SaveLoadBool_boolean_405, logic_SubGraph_SaveLoadBool_boolAsVariable_405, logic_SubGraph_SaveLoadBool_uniqueID_405);
	}

	private void Relay_Set_True_405()
	{
		logic_SubGraph_SaveLoadBool_boolean_405 = local_Q5EnemyAlive_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_405 = local_Q5EnemyAlive_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_405.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_405, logic_SubGraph_SaveLoadBool_boolAsVariable_405, logic_SubGraph_SaveLoadBool_uniqueID_405);
	}

	private void Relay_Set_False_405()
	{
		logic_SubGraph_SaveLoadBool_boolean_405 = local_Q5EnemyAlive_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_405 = local_Q5EnemyAlive_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_405.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_405, logic_SubGraph_SaveLoadBool_boolAsVariable_405, logic_SubGraph_SaveLoadBool_uniqueID_405);
	}

	private void Relay_Save_Out_407()
	{
		Relay_Save_363();
	}

	private void Relay_Load_Out_407()
	{
		Relay_Load_363();
	}

	private void Relay_Restart_Out_407()
	{
		Relay_Set_False_363();
	}

	private void Relay_Save_407()
	{
		logic_SubGraph_SaveLoadBool_boolean_407 = local_Q6EnemyAlive_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_407 = local_Q6EnemyAlive_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_407.Save(ref logic_SubGraph_SaveLoadBool_boolean_407, logic_SubGraph_SaveLoadBool_boolAsVariable_407, logic_SubGraph_SaveLoadBool_uniqueID_407);
	}

	private void Relay_Load_407()
	{
		logic_SubGraph_SaveLoadBool_boolean_407 = local_Q6EnemyAlive_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_407 = local_Q6EnemyAlive_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_407.Load(ref logic_SubGraph_SaveLoadBool_boolean_407, logic_SubGraph_SaveLoadBool_boolAsVariable_407, logic_SubGraph_SaveLoadBool_uniqueID_407);
	}

	private void Relay_Set_True_407()
	{
		logic_SubGraph_SaveLoadBool_boolean_407 = local_Q6EnemyAlive_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_407 = local_Q6EnemyAlive_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_407.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_407, logic_SubGraph_SaveLoadBool_boolAsVariable_407, logic_SubGraph_SaveLoadBool_uniqueID_407);
	}

	private void Relay_Set_False_407()
	{
		logic_SubGraph_SaveLoadBool_boolean_407 = local_Q6EnemyAlive_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_407 = local_Q6EnemyAlive_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_407.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_407, logic_SubGraph_SaveLoadBool_boolAsVariable_407, logic_SubGraph_SaveLoadBool_uniqueID_407);
	}

	private void Relay_Save_Out_411()
	{
		Relay_Save_413();
	}

	private void Relay_Load_Out_411()
	{
		Relay_Load_413();
	}

	private void Relay_Restart_Out_411()
	{
		Relay_Set_False_413();
	}

	private void Relay_Save_411()
	{
		logic_SubGraph_SaveLoadBool_boolean_411 = local_Question04WrongAnswers_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_411 = local_Question04WrongAnswers_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_411.Save(ref logic_SubGraph_SaveLoadBool_boolean_411, logic_SubGraph_SaveLoadBool_boolAsVariable_411, logic_SubGraph_SaveLoadBool_uniqueID_411);
	}

	private void Relay_Load_411()
	{
		logic_SubGraph_SaveLoadBool_boolean_411 = local_Question04WrongAnswers_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_411 = local_Question04WrongAnswers_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_411.Load(ref logic_SubGraph_SaveLoadBool_boolean_411, logic_SubGraph_SaveLoadBool_boolAsVariable_411, logic_SubGraph_SaveLoadBool_uniqueID_411);
	}

	private void Relay_Set_True_411()
	{
		logic_SubGraph_SaveLoadBool_boolean_411 = local_Question04WrongAnswers_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_411 = local_Question04WrongAnswers_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_411.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_411, logic_SubGraph_SaveLoadBool_boolAsVariable_411, logic_SubGraph_SaveLoadBool_uniqueID_411);
	}

	private void Relay_Set_False_411()
	{
		logic_SubGraph_SaveLoadBool_boolean_411 = local_Question04WrongAnswers_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_411 = local_Question04WrongAnswers_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_411.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_411, logic_SubGraph_SaveLoadBool_boolAsVariable_411, logic_SubGraph_SaveLoadBool_uniqueID_411);
	}

	private void Relay_Save_Out_412()
	{
	}

	private void Relay_Load_Out_412()
	{
		Relay_In_372();
	}

	private void Relay_Restart_Out_412()
	{
		Relay_False_305();
	}

	private void Relay_Save_412()
	{
		logic_SubGraph_SaveLoadBool_boolean_412 = local_Question06WrongAnswers_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_412 = local_Question06WrongAnswers_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_412.Save(ref logic_SubGraph_SaveLoadBool_boolean_412, logic_SubGraph_SaveLoadBool_boolAsVariable_412, logic_SubGraph_SaveLoadBool_uniqueID_412);
	}

	private void Relay_Load_412()
	{
		logic_SubGraph_SaveLoadBool_boolean_412 = local_Question06WrongAnswers_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_412 = local_Question06WrongAnswers_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_412.Load(ref logic_SubGraph_SaveLoadBool_boolean_412, logic_SubGraph_SaveLoadBool_boolAsVariable_412, logic_SubGraph_SaveLoadBool_uniqueID_412);
	}

	private void Relay_Set_True_412()
	{
		logic_SubGraph_SaveLoadBool_boolean_412 = local_Question06WrongAnswers_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_412 = local_Question06WrongAnswers_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_412.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_412, logic_SubGraph_SaveLoadBool_boolAsVariable_412, logic_SubGraph_SaveLoadBool_uniqueID_412);
	}

	private void Relay_Set_False_412()
	{
		logic_SubGraph_SaveLoadBool_boolean_412 = local_Question06WrongAnswers_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_412 = local_Question06WrongAnswers_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_412.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_412, logic_SubGraph_SaveLoadBool_boolAsVariable_412, logic_SubGraph_SaveLoadBool_uniqueID_412);
	}

	private void Relay_Save_Out_413()
	{
		Relay_Save_412();
	}

	private void Relay_Load_Out_413()
	{
		Relay_Load_412();
	}

	private void Relay_Restart_Out_413()
	{
		Relay_Set_False_412();
	}

	private void Relay_Save_413()
	{
		logic_SubGraph_SaveLoadBool_boolean_413 = local_Question05WrongAnswers_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_413 = local_Question05WrongAnswers_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_413.Save(ref logic_SubGraph_SaveLoadBool_boolean_413, logic_SubGraph_SaveLoadBool_boolAsVariable_413, logic_SubGraph_SaveLoadBool_uniqueID_413);
	}

	private void Relay_Load_413()
	{
		logic_SubGraph_SaveLoadBool_boolean_413 = local_Question05WrongAnswers_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_413 = local_Question05WrongAnswers_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_413.Load(ref logic_SubGraph_SaveLoadBool_boolean_413, logic_SubGraph_SaveLoadBool_boolAsVariable_413, logic_SubGraph_SaveLoadBool_uniqueID_413);
	}

	private void Relay_Set_True_413()
	{
		logic_SubGraph_SaveLoadBool_boolean_413 = local_Question05WrongAnswers_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_413 = local_Question05WrongAnswers_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_413.Set_True(ref logic_SubGraph_SaveLoadBool_boolean_413, logic_SubGraph_SaveLoadBool_boolAsVariable_413, logic_SubGraph_SaveLoadBool_uniqueID_413);
	}

	private void Relay_Set_False_413()
	{
		logic_SubGraph_SaveLoadBool_boolean_413 = local_Question05WrongAnswers_System_Boolean;
		logic_SubGraph_SaveLoadBool_boolAsVariable_413 = local_Question05WrongAnswers_System_Boolean;
		logic_SubGraph_SaveLoadBool_SubGraph_SaveLoadBool_413.Set_False(ref logic_SubGraph_SaveLoadBool_boolean_413, logic_SubGraph_SaveLoadBool_boolAsVariable_413, logic_SubGraph_SaveLoadBool_uniqueID_413);
	}

	private void Relay_In_416()
	{
		logic_uScriptCon_CompareBool_Bool_416 = local_Q3EnemyAlive_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_416.In(logic_uScriptCon_CompareBool_Bool_416);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_416.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_416.False;
		if (num)
		{
			Relay_In_419();
		}
		if (flag)
		{
			Relay_InitialSpawn_447();
		}
	}

	private void Relay_True_417()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_417.True(out logic_uScriptAct_SetBool_Target_417);
		local_Q3EnemyAlive_System_Boolean = logic_uScriptAct_SetBool_Target_417;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_417.Out)
		{
			Relay_In_449();
		}
	}

	private void Relay_False_417()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_417.False(out logic_uScriptAct_SetBool_Target_417);
		local_Q3EnemyAlive_System_Boolean = logic_uScriptAct_SetBool_Target_417;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_417.Out)
		{
			Relay_In_449();
		}
	}

	private void Relay_In_419()
	{
		logic_uScript_AddMessage_messageData_419 = msgSpawnMinion;
		logic_uScript_AddMessage_speaker_419 = messageSpeaker;
		logic_uScript_AddMessage_Return_419 = logic_uScript_AddMessage_uScript_AddMessage_419.In(logic_uScript_AddMessage_messageData_419, logic_uScript_AddMessage_speaker_419);
		if (logic_uScript_AddMessage_uScript_AddMessage_419.Out)
		{
			Relay_In_449();
		}
	}

	private void Relay_In_424()
	{
		logic_uScript_Wait_seconds_424 = WaitingTime;
		logic_uScript_Wait_uScript_Wait_424.In(logic_uScript_Wait_seconds_424, logic_uScript_Wait_repeat_424);
		if (logic_uScript_Wait_uScript_Wait_424.Waited)
		{
			Relay_In_792();
		}
	}

	private void Relay_In_428()
	{
		logic_uScript_GetCircuitChargeInfo_block_428 = local_ButtonBlock1_TankBlock;
		logic_uScript_GetCircuitChargeInfo_Return_428 = logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_428.In(logic_uScript_GetCircuitChargeInfo_block_428, logic_uScript_GetCircuitChargeInfo_tech_428, logic_uScript_GetCircuitChargeInfo_blockType_428);
		local_Button1Value_System_Int32 = logic_uScript_GetCircuitChargeInfo_Return_428;
		if (logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_428.Out)
		{
			Relay_In_438();
		}
	}

	private void Relay_In_429()
	{
		logic_uScript_AddMessage_messageData_429 = msg09QuestionThree;
		logic_uScript_AddMessage_speaker_429 = messageSpeaker;
		logic_uScript_AddMessage_Return_429 = logic_uScript_AddMessage_uScript_AddMessage_429.In(logic_uScript_AddMessage_messageData_429, logic_uScript_AddMessage_speaker_429);
		if (logic_uScript_AddMessage_uScript_AddMessage_429.Out)
		{
			Relay_In_428();
		}
	}

	private void Relay_In_433()
	{
		logic_uScript_AddMessage_messageData_433 = msgMinionDefeated;
		logic_uScript_AddMessage_speaker_433 = messageSpeaker;
		logic_uScript_AddMessage_Return_433 = logic_uScript_AddMessage_uScript_AddMessage_433.In(logic_uScript_AddMessage_messageData_433, logic_uScript_AddMessage_speaker_433);
		if (logic_uScript_AddMessage_uScript_AddMessage_433.Out)
		{
			Relay_In_424();
		}
	}

	private void Relay_In_434()
	{
		logic_uScript_Wait_seconds_434 = WaitingTime;
		logic_uScript_Wait_uScript_Wait_434.In(logic_uScript_Wait_seconds_434, logic_uScript_Wait_repeat_434);
		if (logic_uScript_Wait_uScript_Wait_434.Waited)
		{
			Relay_False_448();
		}
	}

	private void Relay_In_436()
	{
		logic_uScript_AddMessage_messageData_436 = msg11QuestionThreeWrong;
		logic_uScript_AddMessage_speaker_436 = messageSpeaker;
		logic_uScript_AddMessage_Return_436 = logic_uScript_AddMessage_uScript_AddMessage_436.In(logic_uScript_AddMessage_messageData_436, logic_uScript_AddMessage_speaker_436);
		if (logic_uScript_AddMessage_uScript_AddMessage_436.Out)
		{
			Relay_In_434();
		}
	}

	private void Relay_Output1_437()
	{
		Relay_False_714();
	}

	private void Relay_Output2_437()
	{
		Relay_In_429();
	}

	private void Relay_Output3_437()
	{
		Relay_In_442();
	}

	private void Relay_Output4_437()
	{
		Relay_In_440();
	}

	private void Relay_Output5_437()
	{
		Relay_In_433();
	}

	private void Relay_Output6_437()
	{
	}

	private void Relay_Output7_437()
	{
	}

	private void Relay_Output8_437()
	{
	}

	private void Relay_In_437()
	{
		logic_uScriptCon_ManualSwitch_CurrentOutput_437 = local_QuestionThree_System_Int32;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_437.In(logic_uScriptCon_ManualSwitch_CurrentOutput_437);
	}

	private void Relay_In_438()
	{
		logic_uScriptCon_CompareInt_A_438 = local_Button1Value_System_Int32;
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_438.In(logic_uScriptCon_CompareInt_A_438, logic_uScriptCon_CompareInt_B_438);
		bool greaterThan = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_438.GreaterThan;
		bool lessThanOrEqualTo = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_438.LessThanOrEqualTo;
		if (greaterThan)
		{
			Relay_In_720();
		}
		if (lessThanOrEqualTo)
		{
			Relay_In_751();
		}
	}

	private void Relay_In_440()
	{
		logic_uScriptCon_CompareBool_Bool_440 = local_Question03WrongAnswers_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_440.In(logic_uScriptCon_CompareBool_Bool_440);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_440.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_440.False;
		if (num)
		{
			Relay_In_436();
		}
		if (flag)
		{
			Relay_In_416();
		}
	}

	private void Relay_In_442()
	{
		logic_uScript_AddMessage_messageData_442 = msg10QuestionThreeCorrect;
		logic_uScript_AddMessage_speaker_442 = messageSpeaker;
		logic_uScript_AddMessage_Return_442 = logic_uScript_AddMessage_uScript_AddMessage_442.In(logic_uScript_AddMessage_messageData_442, logic_uScript_AddMessage_speaker_442);
		if (logic_uScript_AddMessage_uScript_AddMessage_442.Out)
		{
			Relay_In_1047();
		}
	}

	private void Relay_InitialSpawn_447()
	{
		int num = 0;
		Array enemyTechData = EnemyTechData;
		if (logic_uScript_SpawnTechsFromData_spawnData_447.Length != num + enemyTechData.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_447, num + enemyTechData.Length);
		}
		Array.Copy(enemyTechData, 0, logic_uScript_SpawnTechsFromData_spawnData_447, num, enemyTechData.Length);
		num += enemyTechData.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_447 = owner_Connection_421;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_447.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_447, logic_uScript_SpawnTechsFromData_ownerNode_447, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_447, logic_uScript_SpawnTechsFromData_allowResurrection_447);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_447.Out)
		{
			Relay_True_417();
		}
	}

	private void Relay_True_448()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_448.True(out logic_uScriptAct_SetBool_Target_448);
		local_Question03WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_448;
	}

	private void Relay_False_448()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_448.False(out logic_uScriptAct_SetBool_Target_448);
		local_Question03WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_448;
	}

	private void Relay_In_449()
	{
		int num = 0;
		Array enemyTechData = EnemyTechData;
		if (logic_uScript_GetAndCheckTechs_techData_449.Length != num + enemyTechData.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_449, num + enemyTechData.Length);
		}
		Array.Copy(enemyTechData, 0, logic_uScript_GetAndCheckTechs_techData_449, num, enemyTechData.Length);
		num += enemyTechData.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_449 = owner_Connection_415;
		int num2 = 0;
		Array array = local_Enemy_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_449.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_449, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techs_449, num2, array.Length);
		num2 += array.Length;
		logic_uScript_GetAndCheckTechs_Return_449 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_449.In(logic_uScript_GetAndCheckTechs_techData_449, logic_uScript_GetAndCheckTechs_ownerNode_449, ref logic_uScript_GetAndCheckTechs_techs_449);
		local_Enemy_TankArray = logic_uScript_GetAndCheckTechs_techs_449;
		if (logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_449.AllDead)
		{
			Relay_In_457();
		}
	}

	private void Relay_In_457()
	{
		logic_uScriptAct_AddInt_v2_A_457 = local_QuestionThree_System_Int32;
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_457.In(logic_uScriptAct_AddInt_v2_A_457, logic_uScriptAct_AddInt_v2_B_457, out logic_uScriptAct_AddInt_v2_IntResult_457, out logic_uScriptAct_AddInt_v2_FloatResult_457);
		local_QuestionThree_System_Int32 = logic_uScriptAct_AddInt_v2_IntResult_457;
	}

	private void Relay_True_462()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_462.True(out logic_uScriptAct_SetBool_Target_462);
		local_Q4EnemyAlive_System_Boolean = logic_uScriptAct_SetBool_Target_462;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_462.Out)
		{
			Relay_In_476();
		}
	}

	private void Relay_False_462()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_462.False(out logic_uScriptAct_SetBool_Target_462);
		local_Q4EnemyAlive_System_Boolean = logic_uScriptAct_SetBool_Target_462;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_462.Out)
		{
			Relay_In_476();
		}
	}

	private void Relay_In_463()
	{
		logic_uScriptCon_CompareBool_Bool_463 = local_Q4EnemyAlive_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_463.In(logic_uScriptCon_CompareBool_Bool_463);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_463.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_463.False;
		if (num)
		{
			Relay_In_496();
		}
		if (flag)
		{
			Relay_InitialSpawn_464();
		}
	}

	private void Relay_InitialSpawn_464()
	{
		int num = 0;
		Array enemyTechData = EnemyTechData;
		if (logic_uScript_SpawnTechsFromData_spawnData_464.Length != num + enemyTechData.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_464, num + enemyTechData.Length);
		}
		Array.Copy(enemyTechData, 0, logic_uScript_SpawnTechsFromData_spawnData_464, num, enemyTechData.Length);
		num += enemyTechData.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_464 = owner_Connection_490;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_464.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_464, logic_uScript_SpawnTechsFromData_ownerNode_464, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_464, logic_uScript_SpawnTechsFromData_allowResurrection_464);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_464.Out)
		{
			Relay_True_462();
		}
	}

	private void Relay_In_465()
	{
		logic_uScriptAct_AddInt_v2_A_465 = local_QuestionFour_System_Int32;
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_465.In(logic_uScriptAct_AddInt_v2_A_465, logic_uScriptAct_AddInt_v2_B_465, out logic_uScriptAct_AddInt_v2_IntResult_465, out logic_uScriptAct_AddInt_v2_FloatResult_465);
		local_QuestionFour_System_Int32 = logic_uScriptAct_AddInt_v2_IntResult_465;
	}

	private void Relay_In_467()
	{
		logic_uScriptCon_CompareInt_A_467 = local_Button1Value_System_Int32;
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_467.In(logic_uScriptCon_CompareInt_A_467, logic_uScriptCon_CompareInt_B_467);
		bool greaterThan = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_467.GreaterThan;
		bool lessThanOrEqualTo = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_467.LessThanOrEqualTo;
		if (greaterThan)
		{
			Relay_In_824();
		}
		if (lessThanOrEqualTo)
		{
			Relay_In_841();
		}
	}

	private void Relay_True_468()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_468.True(out logic_uScriptAct_SetBool_Target_468);
		local_Question04WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_468;
	}

	private void Relay_False_468()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_468.False(out logic_uScriptAct_SetBool_Target_468);
		local_Question04WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_468;
	}

	private void Relay_In_471()
	{
		logic_uScript_Wait_seconds_471 = WaitingTime;
		logic_uScript_Wait_uScript_Wait_471.In(logic_uScript_Wait_seconds_471, logic_uScript_Wait_repeat_471);
		if (logic_uScript_Wait_uScript_Wait_471.Waited)
		{
			Relay_False_468();
		}
	}

	private void Relay_In_472()
	{
		logic_uScript_AddMessage_messageData_472 = msgMinionDefeated;
		logic_uScript_AddMessage_speaker_472 = messageSpeaker;
		logic_uScript_AddMessage_Return_472 = logic_uScript_AddMessage_uScript_AddMessage_472.In(logic_uScript_AddMessage_messageData_472, logic_uScript_AddMessage_speaker_472);
		if (logic_uScript_AddMessage_uScript_AddMessage_472.Out)
		{
			Relay_In_484();
		}
	}

	private void Relay_In_476()
	{
		int num = 0;
		Array enemyTechData = EnemyTechData;
		if (logic_uScript_GetAndCheckTechs_techData_476.Length != num + enemyTechData.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_476, num + enemyTechData.Length);
		}
		Array.Copy(enemyTechData, 0, logic_uScript_GetAndCheckTechs_techData_476, num, enemyTechData.Length);
		num += enemyTechData.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_476 = owner_Connection_485;
		int num2 = 0;
		Array array = local_Enemy_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_476.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_476, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techs_476, num2, array.Length);
		num2 += array.Length;
		logic_uScript_GetAndCheckTechs_Return_476 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_476.In(logic_uScript_GetAndCheckTechs_techData_476, logic_uScript_GetAndCheckTechs_ownerNode_476, ref logic_uScript_GetAndCheckTechs_techs_476);
		local_Enemy_TankArray = logic_uScript_GetAndCheckTechs_techs_476;
		if (logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_476.AllDead)
		{
			Relay_In_465();
		}
	}

	private void Relay_In_478()
	{
		logic_uScript_AddMessage_messageData_478 = msg14QuestionFourWrong;
		logic_uScript_AddMessage_speaker_478 = messageSpeaker;
		logic_uScript_AddMessage_Return_478 = logic_uScript_AddMessage_uScript_AddMessage_478.In(logic_uScript_AddMessage_messageData_478, logic_uScript_AddMessage_speaker_478);
		if (logic_uScript_AddMessage_uScript_AddMessage_478.Out)
		{
			Relay_In_471();
		}
	}

	private void Relay_In_481()
	{
		logic_uScript_AddMessage_messageData_481 = msg13QuestionFourCorrect;
		logic_uScript_AddMessage_speaker_481 = messageSpeaker;
		logic_uScript_AddMessage_Return_481 = logic_uScript_AddMessage_uScript_AddMessage_481.In(logic_uScript_AddMessage_messageData_481, logic_uScript_AddMessage_speaker_481);
		if (logic_uScript_AddMessage_uScript_AddMessage_481.Out)
		{
			Relay_In_1049();
		}
	}

	private void Relay_In_482()
	{
		logic_uScriptCon_CompareBool_Bool_482 = local_Question04WrongAnswers_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_482.In(logic_uScriptCon_CompareBool_Bool_482);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_482.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_482.False;
		if (num)
		{
			Relay_In_478();
		}
		if (flag)
		{
			Relay_In_463();
		}
	}

	private void Relay_In_484()
	{
		logic_uScript_Wait_seconds_484 = WaitingTime;
		logic_uScript_Wait_uScript_Wait_484.In(logic_uScript_Wait_seconds_484, logic_uScript_Wait_repeat_484);
		if (logic_uScript_Wait_uScript_Wait_484.Waited)
		{
			Relay_In_805();
		}
	}

	private void Relay_In_492()
	{
		logic_uScript_AddMessage_messageData_492 = msg12QuestionFour;
		logic_uScript_AddMessage_speaker_492 = messageSpeaker;
		logic_uScript_AddMessage_Return_492 = logic_uScript_AddMessage_uScript_AddMessage_492.In(logic_uScript_AddMessage_messageData_492, logic_uScript_AddMessage_speaker_492);
		if (logic_uScript_AddMessage_uScript_AddMessage_492.Out)
		{
			Relay_In_497();
		}
	}

	private void Relay_Output1_494()
	{
		Relay_False_801();
	}

	private void Relay_Output2_494()
	{
		Relay_In_492();
	}

	private void Relay_Output3_494()
	{
		Relay_In_481();
	}

	private void Relay_Output4_494()
	{
		Relay_In_482();
	}

	private void Relay_Output5_494()
	{
		Relay_In_472();
	}

	private void Relay_Output6_494()
	{
	}

	private void Relay_Output7_494()
	{
	}

	private void Relay_Output8_494()
	{
	}

	private void Relay_In_494()
	{
		logic_uScriptCon_ManualSwitch_CurrentOutput_494 = local_QuestionFour_System_Int32;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_494.In(logic_uScriptCon_ManualSwitch_CurrentOutput_494);
	}

	private void Relay_In_496()
	{
		logic_uScript_AddMessage_messageData_496 = msgSpawnMinion;
		logic_uScript_AddMessage_speaker_496 = messageSpeaker;
		logic_uScript_AddMessage_Return_496 = logic_uScript_AddMessage_uScript_AddMessage_496.In(logic_uScript_AddMessage_messageData_496, logic_uScript_AddMessage_speaker_496);
		if (logic_uScript_AddMessage_uScript_AddMessage_496.Out)
		{
			Relay_In_476();
		}
	}

	private void Relay_In_497()
	{
		logic_uScript_GetCircuitChargeInfo_block_497 = local_ButtonBlock1_TankBlock;
		logic_uScript_GetCircuitChargeInfo_Return_497 = logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_497.In(logic_uScript_GetCircuitChargeInfo_block_497, logic_uScript_GetCircuitChargeInfo_tech_497, logic_uScript_GetCircuitChargeInfo_blockType_497);
		local_Button1Value_System_Int32 = logic_uScript_GetCircuitChargeInfo_Return_497;
		if (logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_497.Out)
		{
			Relay_In_467();
		}
	}

	private void Relay_In_504()
	{
		logic_uScriptCon_CompareInt_A_504 = local_Button1Value_System_Int32;
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_504.In(logic_uScriptCon_CompareInt_A_504, logic_uScriptCon_CompareInt_B_504);
		bool greaterThan = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_504.GreaterThan;
		bool lessThanOrEqualTo = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_504.LessThanOrEqualTo;
		if (greaterThan)
		{
			Relay_In_879();
		}
		if (lessThanOrEqualTo)
		{
			Relay_In_910();
		}
	}

	private void Relay_In_505()
	{
		logic_uScriptCon_CompareBool_Bool_505 = local_Q5EnemyAlive_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_505.In(logic_uScriptCon_CompareBool_Bool_505);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_505.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_505.False;
		if (num)
		{
			Relay_In_523();
		}
		if (flag)
		{
			Relay_InitialSpawn_534();
		}
	}

	private void Relay_In_511()
	{
		logic_uScript_AddMessage_messageData_511 = msg15QuestionFive;
		logic_uScript_AddMessage_speaker_511 = messageSpeaker;
		logic_uScript_AddMessage_Return_511 = logic_uScript_AddMessage_uScript_AddMessage_511.In(logic_uScript_AddMessage_messageData_511, logic_uScript_AddMessage_speaker_511);
		if (logic_uScript_AddMessage_uScript_AddMessage_511.Out)
		{
			Relay_In_512();
		}
	}

	private void Relay_In_512()
	{
		logic_uScript_GetCircuitChargeInfo_block_512 = local_ButtonBlock1_TankBlock;
		logic_uScript_GetCircuitChargeInfo_Return_512 = logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_512.In(logic_uScript_GetCircuitChargeInfo_block_512, logic_uScript_GetCircuitChargeInfo_tech_512, logic_uScript_GetCircuitChargeInfo_blockType_512);
		local_Button1Value_System_Int32 = logic_uScript_GetCircuitChargeInfo_Return_512;
		if (logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_512.Out)
		{
			Relay_In_504();
		}
	}

	private void Relay_In_517()
	{
		logic_uScript_Wait_seconds_517 = WaitingTime;
		logic_uScript_Wait_uScript_Wait_517.In(logic_uScript_Wait_seconds_517, logic_uScript_Wait_repeat_517);
		if (logic_uScript_Wait_uScript_Wait_517.Waited)
		{
			Relay_False_535();
		}
	}

	private void Relay_In_518()
	{
		logic_uScript_AddMessage_messageData_518 = msgMinionDefeated;
		logic_uScript_AddMessage_speaker_518 = messageSpeaker;
		logic_uScript_AddMessage_Return_518 = logic_uScript_AddMessage_uScript_AddMessage_518.In(logic_uScript_AddMessage_messageData_518, logic_uScript_AddMessage_speaker_518);
		if (logic_uScript_AddMessage_uScript_AddMessage_518.Out)
		{
			Relay_In_530();
		}
	}

	private void Relay_In_521()
	{
		logic_uScript_AddMessage_messageData_521 = msg17QuestionFiveWrong;
		logic_uScript_AddMessage_speaker_521 = messageSpeaker;
		logic_uScript_AddMessage_Return_521 = logic_uScript_AddMessage_uScript_AddMessage_521.In(logic_uScript_AddMessage_messageData_521, logic_uScript_AddMessage_speaker_521);
		if (logic_uScript_AddMessage_uScript_AddMessage_521.Out)
		{
			Relay_In_517();
		}
	}

	private void Relay_In_522()
	{
		logic_uScript_AddMessage_messageData_522 = msg16QuestionFiveCorrect;
		logic_uScript_AddMessage_speaker_522 = messageSpeaker;
		logic_uScript_AddMessage_Return_522 = logic_uScript_AddMessage_uScript_AddMessage_522.In(logic_uScript_AddMessage_messageData_522, logic_uScript_AddMessage_speaker_522);
		if (logic_uScript_AddMessage_uScript_AddMessage_522.Out)
		{
			Relay_In_1051();
		}
	}

	private void Relay_In_523()
	{
		logic_uScript_AddMessage_messageData_523 = msgSpawnMinion;
		logic_uScript_AddMessage_speaker_523 = messageSpeaker;
		logic_uScript_AddMessage_Return_523 = logic_uScript_AddMessage_uScript_AddMessage_523.In(logic_uScript_AddMessage_messageData_523, logic_uScript_AddMessage_speaker_523);
		if (logic_uScript_AddMessage_uScript_AddMessage_523.Out)
		{
			Relay_In_526();
		}
	}

	private void Relay_In_526()
	{
		int num = 0;
		Array enemyTechData = EnemyTechData;
		if (logic_uScript_GetAndCheckTechs_techData_526.Length != num + enemyTechData.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techData_526, num + enemyTechData.Length);
		}
		Array.Copy(enemyTechData, 0, logic_uScript_GetAndCheckTechs_techData_526, num, enemyTechData.Length);
		num += enemyTechData.Length;
		logic_uScript_GetAndCheckTechs_ownerNode_526 = owner_Connection_509;
		int num2 = 0;
		Array array = local_Enemy_TankArray;
		if (logic_uScript_GetAndCheckTechs_techs_526.Length != num2 + array.Length)
		{
			Array.Resize(ref logic_uScript_GetAndCheckTechs_techs_526, num2 + array.Length);
		}
		Array.Copy(array, 0, logic_uScript_GetAndCheckTechs_techs_526, num2, array.Length);
		num2 += array.Length;
		logic_uScript_GetAndCheckTechs_Return_526 = logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_526.In(logic_uScript_GetAndCheckTechs_techData_526, logic_uScript_GetAndCheckTechs_ownerNode_526, ref logic_uScript_GetAndCheckTechs_techs_526);
		local_Enemy_TankArray = logic_uScript_GetAndCheckTechs_techs_526;
		if (logic_uScript_GetAndCheckTechs_uScript_GetAndCheckTechs_526.AllDead)
		{
			Relay_In_1055();
		}
	}

	private void Relay_Output1_528()
	{
		Relay_False_954();
	}

	private void Relay_Output2_528()
	{
		Relay_In_511();
	}

	private void Relay_Output3_528()
	{
		Relay_In_522();
	}

	private void Relay_Output4_528()
	{
		Relay_In_532();
	}

	private void Relay_Output5_528()
	{
		Relay_In_518();
	}

	private void Relay_Output6_528()
	{
	}

	private void Relay_Output7_528()
	{
	}

	private void Relay_Output8_528()
	{
	}

	private void Relay_In_528()
	{
		logic_uScriptCon_ManualSwitch_CurrentOutput_528 = local_QuestionFive_System_Int32;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_528.In(logic_uScriptCon_ManualSwitch_CurrentOutput_528);
	}

	private void Relay_True_529()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_529.True(out logic_uScriptAct_SetBool_Target_529);
		local_Q5EnemyAlive_System_Boolean = logic_uScriptAct_SetBool_Target_529;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_529.Out)
		{
			Relay_In_526();
		}
	}

	private void Relay_False_529()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_529.False(out logic_uScriptAct_SetBool_Target_529);
		local_Q5EnemyAlive_System_Boolean = logic_uScriptAct_SetBool_Target_529;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_529.Out)
		{
			Relay_In_526();
		}
	}

	private void Relay_In_530()
	{
		logic_uScript_Wait_seconds_530 = WaitingTime;
		logic_uScript_Wait_uScript_Wait_530.In(logic_uScript_Wait_seconds_530, logic_uScript_Wait_repeat_530);
		if (logic_uScript_Wait_uScript_Wait_530.Waited)
		{
			Relay_In_959();
		}
	}

	private void Relay_In_532()
	{
		logic_uScriptCon_CompareBool_Bool_532 = local_Question05WrongAnswers_System_Boolean;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_532.In(logic_uScriptCon_CompareBool_Bool_532);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_532.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_532.False;
		if (num)
		{
			Relay_In_521();
		}
		if (flag)
		{
			Relay_In_505();
		}
	}

	private void Relay_InitialSpawn_534()
	{
		int num = 0;
		Array enemyTechData = EnemyTechData;
		if (logic_uScript_SpawnTechsFromData_spawnData_534.Length != num + enemyTechData.Length)
		{
			Array.Resize(ref logic_uScript_SpawnTechsFromData_spawnData_534, num + enemyTechData.Length);
		}
		Array.Copy(enemyTechData, 0, logic_uScript_SpawnTechsFromData_spawnData_534, num, enemyTechData.Length);
		num += enemyTechData.Length;
		logic_uScript_SpawnTechsFromData_ownerNode_534 = owner_Connection_527;
		logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_534.InitialSpawn(logic_uScript_SpawnTechsFromData_spawnData_534, logic_uScript_SpawnTechsFromData_ownerNode_534, logic_uScript_SpawnTechsFromData_delayBetweenSpawns_534, logic_uScript_SpawnTechsFromData_allowResurrection_534);
		if (logic_uScript_SpawnTechsFromData_uScript_SpawnTechsFromData_534.Out)
		{
			Relay_True_529();
		}
	}

	private void Relay_True_535()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_535.True(out logic_uScriptAct_SetBool_Target_535);
		local_Question05WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_535;
	}

	private void Relay_False_535()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_535.False(out logic_uScriptAct_SetBool_Target_535);
		local_Question05WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_535;
	}

	private void Relay_Output1_543()
	{
		Relay_In_181();
	}

	private void Relay_Output2_543()
	{
		Relay_In_102();
	}

	private void Relay_Output3_543()
	{
		Relay_In_437();
	}

	private void Relay_Output4_543()
	{
		Relay_In_494();
	}

	private void Relay_Output5_543()
	{
		Relay_In_528();
	}

	private void Relay_Output6_543()
	{
		Relay_In_245();
	}

	private void Relay_Output7_543()
	{
	}

	private void Relay_Output8_543()
	{
	}

	private void Relay_In_543()
	{
		logic_uScriptCon_ManualSwitch_CurrentOutput_543 = local_QuestionsStage_System_Int32;
		logic_uScriptCon_ManualSwitch_uScriptCon_ManualSwitch_543.In(logic_uScriptCon_ManualSwitch_CurrentOutput_543);
	}

	private void Relay_True_544()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_544.True(out logic_uScriptAct_SetBool_Target_544);
		local_Question01WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_544;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_544.Out)
		{
			Relay_True_76();
		}
	}

	private void Relay_False_544()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_544.False(out logic_uScriptAct_SetBool_Target_544);
		local_Question01WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_544;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_544.Out)
		{
			Relay_True_76();
		}
	}

	private void Relay_True_547()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_547.True(out logic_uScriptAct_SetBool_Target_547);
		local_Q1EnemyAlive_System_Boolean = logic_uScriptAct_SetBool_Target_547;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_547.Out)
		{
			Relay_False_544();
		}
	}

	private void Relay_False_547()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_547.False(out logic_uScriptAct_SetBool_Target_547);
		local_Q1EnemyAlive_System_Boolean = logic_uScriptAct_SetBool_Target_547;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_547.Out)
		{
			Relay_False_544();
		}
	}

	private void Relay_In_549()
	{
		logic_uScriptAct_AddInt_v2_A_549 = local_QuestionOne_System_Int32;
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_549.In(logic_uScriptAct_AddInt_v2_A_549, logic_uScriptAct_AddInt_v2_B_549, out logic_uScriptAct_AddInt_v2_IntResult_549, out logic_uScriptAct_AddInt_v2_FloatResult_549);
		local_QuestionOne_System_Int32 = logic_uScriptAct_AddInt_v2_IntResult_549;
	}

	private void Relay_True_551()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_551.True(out logic_uScriptAct_SetBool_Target_551);
		local_Q2EnemyAlive_System_Boolean = logic_uScriptAct_SetBool_Target_551;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_551.Out)
		{
			Relay_False_552();
		}
	}

	private void Relay_False_551()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_551.False(out logic_uScriptAct_SetBool_Target_551);
		local_Q2EnemyAlive_System_Boolean = logic_uScriptAct_SetBool_Target_551;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_551.Out)
		{
			Relay_False_552();
		}
	}

	private void Relay_True_552()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_552.True(out logic_uScriptAct_SetBool_Target_552);
		local_Question02WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_552;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_552.Out)
		{
			Relay_True_223();
		}
	}

	private void Relay_False_552()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_552.False(out logic_uScriptAct_SetBool_Target_552);
		local_Question02WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_552;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_552.Out)
		{
			Relay_True_223();
		}
	}

	private void Relay_In_555()
	{
		logic_uScriptAct_AddInt_v2_A_555 = local_QuestionTwo_System_Int32;
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_555.In(logic_uScriptAct_AddInt_v2_A_555, logic_uScriptAct_AddInt_v2_B_555, out logic_uScriptAct_AddInt_v2_IntResult_555, out logic_uScriptAct_AddInt_v2_FloatResult_555);
		local_QuestionTwo_System_Int32 = logic_uScriptAct_AddInt_v2_IntResult_555;
	}

	private void Relay_In_557()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_557.In(logic_uScriptAct_SetInt_Value_557, out logic_uScriptAct_SetInt_Target_557);
		local_QuestionsStage_System_Int32 = logic_uScriptAct_SetInt_Target_557;
		if (logic_uScriptAct_SetInt_uScriptAct_SetInt_557.Out)
		{
			Relay_In_559();
		}
	}

	private void Relay_In_559()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_559.In(logic_uScriptAct_SetInt_Value_559, out logic_uScriptAct_SetInt_Target_559);
		local_QuestionTwo_System_Int32 = logic_uScriptAct_SetInt_Target_559;
	}

	private void Relay_In_561()
	{
		logic_uScript_Wait_seconds_561 = WaitingTime;
		logic_uScript_Wait_uScript_Wait_561.In(logic_uScript_Wait_seconds_561, logic_uScript_Wait_repeat_561);
		if (logic_uScript_Wait_uScript_Wait_561.Waited)
		{
			Relay_In_557();
		}
	}

	private void Relay_In_564()
	{
		logic_uScriptCon_CompareBool_Bool_564 = Q1Button1;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_564.In(logic_uScriptCon_CompareBool_Bool_564);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_564.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_564.False;
		if (num)
		{
			Relay_False_569();
		}
		if (flag)
		{
			Relay_True_571();
		}
	}

	private void Relay_In_566()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_566.In(logic_uScriptAct_SetInt_Value_566, out logic_uScriptAct_SetInt_Target_566);
		local_QuestionOne_System_Int32 = logic_uScriptAct_SetInt_Target_566;
	}

	private void Relay_True_569()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_569.True(out logic_uScriptAct_SetBool_Target_569);
		local_Question01WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_569;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_569.Out)
		{
			Relay_False_570();
		}
	}

	private void Relay_False_569()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_569.False(out logic_uScriptAct_SetBool_Target_569);
		local_Question01WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_569;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_569.Out)
		{
			Relay_False_570();
		}
	}

	private void Relay_True_570()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_570.True(out logic_uScriptAct_SetBool_Target_570);
		local_CanPushButtons_System_Boolean = logic_uScriptAct_SetBool_Target_570;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_570.Out)
		{
			Relay_In_566();
		}
	}

	private void Relay_False_570()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_570.False(out logic_uScriptAct_SetBool_Target_570);
		local_CanPushButtons_System_Boolean = logic_uScriptAct_SetBool_Target_570;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_570.Out)
		{
			Relay_In_566();
		}
	}

	private void Relay_True_571()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_571.True(out logic_uScriptAct_SetBool_Target_571);
		local_Question01WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_571;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_571.Out)
		{
			Relay_False_572();
		}
	}

	private void Relay_False_571()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_571.False(out logic_uScriptAct_SetBool_Target_571);
		local_Question01WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_571;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_571.Out)
		{
			Relay_False_572();
		}
	}

	private void Relay_True_572()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_572.True(out logic_uScriptAct_SetBool_Target_572);
		local_CanPushButtons_System_Boolean = logic_uScriptAct_SetBool_Target_572;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_572.Out)
		{
			Relay_In_573();
		}
	}

	private void Relay_False_572()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_572.False(out logic_uScriptAct_SetBool_Target_572);
		local_CanPushButtons_System_Boolean = logic_uScriptAct_SetBool_Target_572;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_572.Out)
		{
			Relay_In_573();
		}
	}

	private void Relay_In_573()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_573.In(logic_uScriptAct_SetInt_Value_573, out logic_uScriptAct_SetInt_Target_573);
		local_QuestionOne_System_Int32 = logic_uScriptAct_SetInt_Target_573;
	}

	private void Relay_True_577()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_577.True(out logic_uScriptAct_SetBool_Target_577);
		local_Question01WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_577;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_577.Out)
		{
			Relay_False_586();
		}
	}

	private void Relay_False_577()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_577.False(out logic_uScriptAct_SetBool_Target_577);
		local_Question01WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_577;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_577.Out)
		{
			Relay_False_586();
		}
	}

	private void Relay_In_578()
	{
		logic_uScriptCon_CompareBool_Bool_578 = Q1Button2;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_578.In(logic_uScriptCon_CompareBool_Bool_578);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_578.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_578.False;
		if (num)
		{
			Relay_False_577();
		}
		if (flag)
		{
			Relay_True_587();
		}
	}

	private void Relay_True_586()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_586.True(out logic_uScriptAct_SetBool_Target_586);
		local_CanPushButtons_System_Boolean = logic_uScriptAct_SetBool_Target_586;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_586.Out)
		{
			Relay_In_590();
		}
	}

	private void Relay_False_586()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_586.False(out logic_uScriptAct_SetBool_Target_586);
		local_CanPushButtons_System_Boolean = logic_uScriptAct_SetBool_Target_586;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_586.Out)
		{
			Relay_In_590();
		}
	}

	private void Relay_True_587()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_587.True(out logic_uScriptAct_SetBool_Target_587);
		local_Question01WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_587;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_587.Out)
		{
			Relay_False_588();
		}
	}

	private void Relay_False_587()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_587.False(out logic_uScriptAct_SetBool_Target_587);
		local_Question01WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_587;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_587.Out)
		{
			Relay_False_588();
		}
	}

	private void Relay_True_588()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_588.True(out logic_uScriptAct_SetBool_Target_588);
		local_CanPushButtons_System_Boolean = logic_uScriptAct_SetBool_Target_588;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_588.Out)
		{
			Relay_In_589();
		}
	}

	private void Relay_False_588()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_588.False(out logic_uScriptAct_SetBool_Target_588);
		local_CanPushButtons_System_Boolean = logic_uScriptAct_SetBool_Target_588;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_588.Out)
		{
			Relay_In_589();
		}
	}

	private void Relay_In_589()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_589.In(logic_uScriptAct_SetInt_Value_589, out logic_uScriptAct_SetInt_Target_589);
		local_QuestionOne_System_Int32 = logic_uScriptAct_SetInt_Target_589;
	}

	private void Relay_In_590()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_590.In(logic_uScriptAct_SetInt_Value_590, out logic_uScriptAct_SetInt_Target_590);
		local_QuestionOne_System_Int32 = logic_uScriptAct_SetInt_Target_590;
	}

	private void Relay_True_591()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_591.True(out logic_uScriptAct_SetBool_Target_591);
		local_Question01WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_591;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_591.Out)
		{
			Relay_False_600();
		}
	}

	private void Relay_False_591()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_591.False(out logic_uScriptAct_SetBool_Target_591);
		local_Question01WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_591;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_591.Out)
		{
			Relay_False_600();
		}
	}

	private void Relay_In_592()
	{
		logic_uScriptCon_CompareBool_Bool_592 = Q1Button3;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_592.In(logic_uScriptCon_CompareBool_Bool_592);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_592.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_592.False;
		if (num)
		{
			Relay_False_591();
		}
		if (flag)
		{
			Relay_True_601();
		}
	}

	private void Relay_True_600()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_600.True(out logic_uScriptAct_SetBool_Target_600);
		local_CanPushButtons_System_Boolean = logic_uScriptAct_SetBool_Target_600;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_600.Out)
		{
			Relay_In_604();
		}
	}

	private void Relay_False_600()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_600.False(out logic_uScriptAct_SetBool_Target_600);
		local_CanPushButtons_System_Boolean = logic_uScriptAct_SetBool_Target_600;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_600.Out)
		{
			Relay_In_604();
		}
	}

	private void Relay_True_601()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_601.True(out logic_uScriptAct_SetBool_Target_601);
		local_Question01WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_601;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_601.Out)
		{
			Relay_False_602();
		}
	}

	private void Relay_False_601()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_601.False(out logic_uScriptAct_SetBool_Target_601);
		local_Question01WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_601;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_601.Out)
		{
			Relay_False_602();
		}
	}

	private void Relay_True_602()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_602.True(out logic_uScriptAct_SetBool_Target_602);
		local_CanPushButtons_System_Boolean = logic_uScriptAct_SetBool_Target_602;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_602.Out)
		{
			Relay_In_603();
		}
	}

	private void Relay_False_602()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_602.False(out logic_uScriptAct_SetBool_Target_602);
		local_CanPushButtons_System_Boolean = logic_uScriptAct_SetBool_Target_602;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_602.Out)
		{
			Relay_In_603();
		}
	}

	private void Relay_In_603()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_603.In(logic_uScriptAct_SetInt_Value_603, out logic_uScriptAct_SetInt_Target_603);
		local_QuestionOne_System_Int32 = logic_uScriptAct_SetInt_Target_603;
	}

	private void Relay_In_604()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_604.In(logic_uScriptAct_SetInt_Value_604, out logic_uScriptAct_SetInt_Target_604);
		local_QuestionOne_System_Int32 = logic_uScriptAct_SetInt_Target_604;
	}

	private void Relay_True_606()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_606.True(out logic_uScriptAct_SetBool_Target_606);
		local_CanPushButtons_System_Boolean = logic_uScriptAct_SetBool_Target_606;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_606.Out)
		{
			Relay_In_615();
		}
	}

	private void Relay_False_606()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_606.False(out logic_uScriptAct_SetBool_Target_606);
		local_CanPushButtons_System_Boolean = logic_uScriptAct_SetBool_Target_606;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_606.Out)
		{
			Relay_In_615();
		}
	}

	private void Relay_True_609()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_609.True(out logic_uScriptAct_SetBool_Target_609);
		local_Question01WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_609;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_609.Out)
		{
			Relay_False_606();
		}
	}

	private void Relay_False_609()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_609.False(out logic_uScriptAct_SetBool_Target_609);
		local_Question01WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_609;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_609.Out)
		{
			Relay_False_606();
		}
	}

	private void Relay_In_610()
	{
		logic_uScriptCon_CompareBool_Bool_610 = Q1Button4;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_610.In(logic_uScriptCon_CompareBool_Bool_610);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_610.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_610.False;
		if (num)
		{
			Relay_False_613();
		}
		if (flag)
		{
			Relay_True_609();
		}
	}

	private void Relay_True_613()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_613.True(out logic_uScriptAct_SetBool_Target_613);
		local_Question01WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_613;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_613.Out)
		{
			Relay_False_618();
		}
	}

	private void Relay_False_613()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_613.False(out logic_uScriptAct_SetBool_Target_613);
		local_Question01WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_613;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_613.Out)
		{
			Relay_False_618();
		}
	}

	private void Relay_In_615()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_615.In(logic_uScriptAct_SetInt_Value_615, out logic_uScriptAct_SetInt_Target_615);
		local_QuestionOne_System_Int32 = logic_uScriptAct_SetInt_Target_615;
	}

	private void Relay_In_617()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_617.In(logic_uScriptAct_SetInt_Value_617, out logic_uScriptAct_SetInt_Target_617);
		local_QuestionOne_System_Int32 = logic_uScriptAct_SetInt_Target_617;
	}

	private void Relay_True_618()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_618.True(out logic_uScriptAct_SetBool_Target_618);
		local_CanPushButtons_System_Boolean = logic_uScriptAct_SetBool_Target_618;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_618.Out)
		{
			Relay_In_617();
		}
	}

	private void Relay_False_618()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_618.False(out logic_uScriptAct_SetBool_Target_618);
		local_CanPushButtons_System_Boolean = logic_uScriptAct_SetBool_Target_618;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_618.Out)
		{
			Relay_In_617();
		}
	}

	private void Relay_In_622()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_622.In(logic_uScriptAct_SetInt_Value_622, out logic_uScriptAct_SetInt_Target_622);
		local_QuestionThree_System_Int32 = logic_uScriptAct_SetInt_Target_622;
	}

	private void Relay_In_623()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_623.In(logic_uScriptAct_SetInt_Value_623, out logic_uScriptAct_SetInt_Target_623);
		local_QuestionsStage_System_Int32 = logic_uScriptAct_SetInt_Target_623;
		if (logic_uScriptAct_SetInt_uScriptAct_SetInt_623.Out)
		{
			Relay_In_622();
		}
	}

	private void Relay_True_625()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_625.True(out logic_uScriptAct_SetBool_Target_625);
		local_Question02WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_625;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_625.Out)
		{
			Relay_False_664();
		}
	}

	private void Relay_False_625()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_625.False(out logic_uScriptAct_SetBool_Target_625);
		local_Question02WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_625;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_625.Out)
		{
			Relay_False_664();
		}
	}

	private void Relay_In_626()
	{
		logic_uScriptCon_CompareBool_Bool_626 = Q2Button1;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_626.In(logic_uScriptCon_CompareBool_Bool_626);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_626.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_626.False;
		if (num)
		{
			Relay_False_625();
		}
		if (flag)
		{
			Relay_True_668();
		}
	}

	private void Relay_True_632()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_632.True(out logic_uScriptAct_SetBool_Target_632);
		local_Question02WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_632;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_632.Out)
		{
			Relay_False_663();
		}
	}

	private void Relay_False_632()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_632.False(out logic_uScriptAct_SetBool_Target_632);
		local_Question02WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_632;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_632.Out)
		{
			Relay_False_663();
		}
	}

	private void Relay_True_634()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_634.True(out logic_uScriptAct_SetBool_Target_634);
		local_CanPushButtons_System_Boolean = logic_uScriptAct_SetBool_Target_634;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_634.Out)
		{
			Relay_In_676();
		}
	}

	private void Relay_False_634()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_634.False(out logic_uScriptAct_SetBool_Target_634);
		local_CanPushButtons_System_Boolean = logic_uScriptAct_SetBool_Target_634;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_634.Out)
		{
			Relay_In_676();
		}
	}

	private void Relay_True_637()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_637.True(out logic_uScriptAct_SetBool_Target_637);
		local_Question02WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_637;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_637.Out)
		{
			Relay_False_675();
		}
	}

	private void Relay_False_637()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_637.False(out logic_uScriptAct_SetBool_Target_637);
		local_Question02WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_637;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_637.Out)
		{
			Relay_False_675();
		}
	}

	private void Relay_True_638()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_638.True(out logic_uScriptAct_SetBool_Target_638);
		local_Question02WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_638;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_638.Out)
		{
			Relay_False_667();
		}
	}

	private void Relay_False_638()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_638.False(out logic_uScriptAct_SetBool_Target_638);
		local_Question02WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_638;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_638.Out)
		{
			Relay_False_667();
		}
	}

	private void Relay_In_639()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_639.In(logic_uScriptAct_SetInt_Value_639, out logic_uScriptAct_SetInt_Target_639);
		local_QuestionTwo_System_Int32 = logic_uScriptAct_SetInt_Target_639;
	}

	private void Relay_True_641()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_641.True(out logic_uScriptAct_SetBool_Target_641);
		local_Question02WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_641;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_641.Out)
		{
			Relay_False_656();
		}
	}

	private void Relay_False_641()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_641.False(out logic_uScriptAct_SetBool_Target_641);
		local_Question02WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_641;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_641.Out)
		{
			Relay_False_656();
		}
	}

	private void Relay_In_642()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_642.In(logic_uScriptAct_SetInt_Value_642, out logic_uScriptAct_SetInt_Target_642);
		local_QuestionTwo_System_Int32 = logic_uScriptAct_SetInt_Target_642;
	}

	private void Relay_In_646()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_646.In(logic_uScriptAct_SetInt_Value_646, out logic_uScriptAct_SetInt_Target_646);
		local_QuestionTwo_System_Int32 = logic_uScriptAct_SetInt_Target_646;
	}

	private void Relay_In_648()
	{
		logic_uScriptCon_CompareBool_Bool_648 = Q2Button4;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_648.In(logic_uScriptCon_CompareBool_Bool_648);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_648.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_648.False;
		if (num)
		{
			Relay_False_632();
		}
		if (flag)
		{
			Relay_True_641();
		}
	}

	private void Relay_True_649()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_649.True(out logic_uScriptAct_SetBool_Target_649);
		local_Question02WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_649;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_649.Out)
		{
			Relay_False_634();
		}
	}

	private void Relay_False_649()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_649.False(out logic_uScriptAct_SetBool_Target_649);
		local_Question02WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_649;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_649.Out)
		{
			Relay_False_634();
		}
	}

	private void Relay_In_652()
	{
		logic_uScriptCon_CompareBool_Bool_652 = Q2Button3;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_652.In(logic_uScriptCon_CompareBool_Bool_652);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_652.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_652.False;
		if (num)
		{
			Relay_False_661();
		}
		if (flag)
		{
			Relay_True_649();
		}
	}

	private void Relay_In_654()
	{
		logic_uScriptCon_CompareInt_A_654 = local_Button4Value_System_Int32;
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_654.In(logic_uScriptCon_CompareInt_A_654, logic_uScriptCon_CompareInt_B_654);
		if (logic_uScriptCon_CompareInt_uScriptCon_CompareInt_654.GreaterThan)
		{
			Relay_In_648();
		}
	}

	private void Relay_True_656()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_656.True(out logic_uScriptAct_SetBool_Target_656);
		local_CanPushButtons_System_Boolean = logic_uScriptAct_SetBool_Target_656;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_656.Out)
		{
			Relay_In_639();
		}
	}

	private void Relay_False_656()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_656.False(out logic_uScriptAct_SetBool_Target_656);
		local_CanPushButtons_System_Boolean = logic_uScriptAct_SetBool_Target_656;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_656.Out)
		{
			Relay_In_639();
		}
	}

	private void Relay_In_658()
	{
		logic_uScript_GetCircuitChargeInfo_block_658 = local_ButtonBlock1_TankBlock;
		logic_uScript_GetCircuitChargeInfo_Return_658 = logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_658.In(logic_uScript_GetCircuitChargeInfo_block_658, logic_uScript_GetCircuitChargeInfo_tech_658, logic_uScript_GetCircuitChargeInfo_blockType_658);
		local_Button1Value_System_Int32 = logic_uScript_GetCircuitChargeInfo_Return_658;
		if (logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_658.Out)
		{
			Relay_In_682();
		}
	}

	private void Relay_True_661()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_661.True(out logic_uScriptAct_SetBool_Target_661);
		local_Question02WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_661;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_661.Out)
		{
			Relay_False_689();
		}
	}

	private void Relay_False_661()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_661.False(out logic_uScriptAct_SetBool_Target_661);
		local_Question02WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_661;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_661.Out)
		{
			Relay_False_689();
		}
	}

	private void Relay_True_663()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_663.True(out logic_uScriptAct_SetBool_Target_663);
		local_CanPushButtons_System_Boolean = logic_uScriptAct_SetBool_Target_663;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_663.Out)
		{
			Relay_In_684();
		}
	}

	private void Relay_False_663()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_663.False(out logic_uScriptAct_SetBool_Target_663);
		local_CanPushButtons_System_Boolean = logic_uScriptAct_SetBool_Target_663;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_663.Out)
		{
			Relay_In_684();
		}
	}

	private void Relay_True_664()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_664.True(out logic_uScriptAct_SetBool_Target_664);
		local_CanPushButtons_System_Boolean = logic_uScriptAct_SetBool_Target_664;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_664.Out)
		{
			Relay_In_686();
		}
	}

	private void Relay_False_664()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_664.False(out logic_uScriptAct_SetBool_Target_664);
		local_CanPushButtons_System_Boolean = logic_uScriptAct_SetBool_Target_664;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_664.Out)
		{
			Relay_In_686();
		}
	}

	private void Relay_In_665()
	{
		logic_uScript_GetCircuitChargeInfo_block_665 = local_ButtonBlock2_TankBlock;
		logic_uScript_GetCircuitChargeInfo_Return_665 = logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_665.In(logic_uScript_GetCircuitChargeInfo_block_665, logic_uScript_GetCircuitChargeInfo_tech_665, logic_uScript_GetCircuitChargeInfo_blockType_665);
		local_Button2Value_System_Int32 = logic_uScript_GetCircuitChargeInfo_Return_665;
		if (logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_665.Out)
		{
			Relay_In_683();
		}
	}

	private void Relay_True_667()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_667.True(out logic_uScriptAct_SetBool_Target_667);
		local_CanPushButtons_System_Boolean = logic_uScriptAct_SetBool_Target_667;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_667.Out)
		{
			Relay_In_646();
		}
	}

	private void Relay_False_667()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_667.False(out logic_uScriptAct_SetBool_Target_667);
		local_CanPushButtons_System_Boolean = logic_uScriptAct_SetBool_Target_667;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_667.Out)
		{
			Relay_In_646();
		}
	}

	private void Relay_True_668()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_668.True(out logic_uScriptAct_SetBool_Target_668);
		local_Question02WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_668;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_668.Out)
		{
			Relay_False_674();
		}
	}

	private void Relay_False_668()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_668.False(out logic_uScriptAct_SetBool_Target_668);
		local_Question02WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_668;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_668.Out)
		{
			Relay_False_674();
		}
	}

	private void Relay_In_669()
	{
		logic_uScript_GetCircuitChargeInfo_block_669 = local_ButtonBlock4_TankBlock;
		logic_uScript_GetCircuitChargeInfo_Return_669 = logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_669.In(logic_uScript_GetCircuitChargeInfo_block_669, logic_uScript_GetCircuitChargeInfo_tech_669, logic_uScript_GetCircuitChargeInfo_blockType_669);
		local_Button4Value_System_Int32 = logic_uScript_GetCircuitChargeInfo_Return_669;
		if (logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_669.Out)
		{
			Relay_In_654();
		}
	}

	private void Relay_True_674()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_674.True(out logic_uScriptAct_SetBool_Target_674);
		local_CanPushButtons_System_Boolean = logic_uScriptAct_SetBool_Target_674;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_674.Out)
		{
			Relay_In_678();
		}
	}

	private void Relay_False_674()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_674.False(out logic_uScriptAct_SetBool_Target_674);
		local_CanPushButtons_System_Boolean = logic_uScriptAct_SetBool_Target_674;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_674.Out)
		{
			Relay_In_678();
		}
	}

	private void Relay_True_675()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_675.True(out logic_uScriptAct_SetBool_Target_675);
		local_CanPushButtons_System_Boolean = logic_uScriptAct_SetBool_Target_675;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_675.Out)
		{
			Relay_In_642();
		}
	}

	private void Relay_False_675()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_675.False(out logic_uScriptAct_SetBool_Target_675);
		local_CanPushButtons_System_Boolean = logic_uScriptAct_SetBool_Target_675;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_675.Out)
		{
			Relay_In_642();
		}
	}

	private void Relay_In_676()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_676.In(logic_uScriptAct_SetInt_Value_676, out logic_uScriptAct_SetInt_Target_676);
		local_QuestionTwo_System_Int32 = logic_uScriptAct_SetInt_Target_676;
	}

	private void Relay_In_678()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_678.In(logic_uScriptAct_SetInt_Value_678, out logic_uScriptAct_SetInt_Target_678);
		local_QuestionTwo_System_Int32 = logic_uScriptAct_SetInt_Target_678;
	}

	private void Relay_In_679()
	{
		logic_uScriptCon_CompareBool_Bool_679 = Q2Button2;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_679.In(logic_uScriptCon_CompareBool_Bool_679);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_679.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_679.False;
		if (num)
		{
			Relay_False_637();
		}
		if (flag)
		{
			Relay_True_638();
		}
	}

	private void Relay_In_680()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_680.In(logic_uScriptAct_SetInt_Value_680, out logic_uScriptAct_SetInt_Target_680);
		local_QuestionTwo_System_Int32 = logic_uScriptAct_SetInt_Target_680;
	}

	private void Relay_In_681()
	{
		logic_uScript_GetCircuitChargeInfo_block_681 = local_ButtonBlock3_TankBlock;
		logic_uScript_GetCircuitChargeInfo_Return_681 = logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_681.In(logic_uScript_GetCircuitChargeInfo_block_681, logic_uScript_GetCircuitChargeInfo_tech_681, logic_uScript_GetCircuitChargeInfo_blockType_681);
		local_Button3Value_System_Int32 = logic_uScript_GetCircuitChargeInfo_Return_681;
		if (logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_681.Out)
		{
			Relay_In_688();
		}
	}

	private void Relay_In_682()
	{
		logic_uScriptCon_CompareInt_A_682 = local_Button1Value_System_Int32;
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_682.In(logic_uScriptCon_CompareInt_A_682, logic_uScriptCon_CompareInt_B_682);
		bool greaterThan = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_682.GreaterThan;
		bool lessThanOrEqualTo = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_682.LessThanOrEqualTo;
		if (greaterThan)
		{
			Relay_In_626();
		}
		if (lessThanOrEqualTo)
		{
			Relay_In_665();
		}
	}

	private void Relay_In_683()
	{
		logic_uScriptCon_CompareInt_A_683 = local_Button2Value_System_Int32;
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_683.In(logic_uScriptCon_CompareInt_A_683, logic_uScriptCon_CompareInt_B_683);
		bool greaterThan = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_683.GreaterThan;
		bool lessThanOrEqualTo = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_683.LessThanOrEqualTo;
		if (greaterThan)
		{
			Relay_In_679();
		}
		if (lessThanOrEqualTo)
		{
			Relay_In_681();
		}
	}

	private void Relay_In_684()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_684.In(logic_uScriptAct_SetInt_Value_684, out logic_uScriptAct_SetInt_Target_684);
		local_QuestionTwo_System_Int32 = logic_uScriptAct_SetInt_Target_684;
	}

	private void Relay_In_686()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_686.In(logic_uScriptAct_SetInt_Value_686, out logic_uScriptAct_SetInt_Target_686);
		local_QuestionTwo_System_Int32 = logic_uScriptAct_SetInt_Target_686;
	}

	private void Relay_In_688()
	{
		logic_uScriptCon_CompareInt_A_688 = local_Button3Value_System_Int32;
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_688.In(logic_uScriptCon_CompareInt_A_688, logic_uScriptCon_CompareInt_B_688);
		bool greaterThan = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_688.GreaterThan;
		bool lessThanOrEqualTo = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_688.LessThanOrEqualTo;
		if (greaterThan)
		{
			Relay_In_652();
		}
		if (lessThanOrEqualTo)
		{
			Relay_In_669();
		}
	}

	private void Relay_True_689()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_689.True(out logic_uScriptAct_SetBool_Target_689);
		local_CanPushButtons_System_Boolean = logic_uScriptAct_SetBool_Target_689;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_689.Out)
		{
			Relay_In_680();
		}
	}

	private void Relay_False_689()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_689.False(out logic_uScriptAct_SetBool_Target_689);
		local_CanPushButtons_System_Boolean = logic_uScriptAct_SetBool_Target_689;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_689.Out)
		{
			Relay_In_680();
		}
	}

	private void Relay_In_698()
	{
		logic_uScript_AddMessage_messageData_698 = msgMinionDefeated;
		logic_uScript_AddMessage_speaker_698 = messageSpeaker;
		logic_uScript_AddMessage_Return_698 = logic_uScript_AddMessage_uScript_AddMessage_698.In(logic_uScript_AddMessage_messageData_698, logic_uScript_AddMessage_speaker_698);
		if (logic_uScript_AddMessage_uScript_AddMessage_698.Out)
		{
			Relay_In_699();
		}
	}

	private void Relay_In_699()
	{
		logic_uScript_Wait_seconds_699 = WaitingTime;
		logic_uScript_Wait_uScript_Wait_699.In(logic_uScript_Wait_seconds_699, logic_uScript_Wait_repeat_699);
		if (logic_uScript_Wait_uScript_Wait_699.Waited)
		{
			Relay_In_701();
		}
	}

	private void Relay_In_701()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_701.In(logic_uScriptAct_SetInt_Value_701, out logic_uScriptAct_SetInt_Target_701);
		local_QuestionsStage_System_Int32 = logic_uScriptAct_SetInt_Target_701;
		if (logic_uScriptAct_SetInt_uScriptAct_SetInt_701.Out)
		{
			Relay_In_703();
		}
	}

	private void Relay_In_703()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_703.In(logic_uScriptAct_SetInt_Value_703, out logic_uScriptAct_SetInt_Target_703);
		local_QuestionOne_System_Int32 = logic_uScriptAct_SetInt_Target_703;
	}

	private void Relay_In_706()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_706.In(logic_uScriptAct_SetInt_Value_706, out logic_uScriptAct_SetInt_Target_706);
		local_QuestionsStage_System_Int32 = logic_uScriptAct_SetInt_Target_706;
		if (logic_uScriptAct_SetInt_uScriptAct_SetInt_706.Out)
		{
			Relay_In_708();
		}
	}

	private void Relay_In_708()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_708.In(logic_uScriptAct_SetInt_Value_708, out logic_uScriptAct_SetInt_Target_708);
		local_QuestionOne_System_Int32 = logic_uScriptAct_SetInt_Target_708;
	}

	private void Relay_In_709()
	{
		logic_uScriptAct_AddInt_v2_A_709 = local_QuestionTwo_System_Int32;
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_709.In(logic_uScriptAct_AddInt_v2_A_709, logic_uScriptAct_AddInt_v2_B_709, out logic_uScriptAct_AddInt_v2_IntResult_709, out logic_uScriptAct_AddInt_v2_FloatResult_709);
		local_QuestionTwo_System_Int32 = logic_uScriptAct_AddInt_v2_IntResult_709;
	}

	private void Relay_True_711()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_711.True(out logic_uScriptAct_SetBool_Target_711);
		local_Question03WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_711;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_711.Out)
		{
			Relay_True_716();
		}
	}

	private void Relay_False_711()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_711.False(out logic_uScriptAct_SetBool_Target_711);
		local_Question03WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_711;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_711.Out)
		{
			Relay_True_716();
		}
	}

	private void Relay_In_713()
	{
		logic_uScriptAct_AddInt_v2_A_713 = local_QuestionThree_System_Int32;
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_713.In(logic_uScriptAct_AddInt_v2_A_713, logic_uScriptAct_AddInt_v2_B_713, out logic_uScriptAct_AddInt_v2_IntResult_713, out logic_uScriptAct_AddInt_v2_FloatResult_713);
		local_QuestionThree_System_Int32 = logic_uScriptAct_AddInt_v2_IntResult_713;
	}

	private void Relay_True_714()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_714.True(out logic_uScriptAct_SetBool_Target_714);
		local_Q3EnemyAlive_System_Boolean = logic_uScriptAct_SetBool_Target_714;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_714.Out)
		{
			Relay_False_711();
		}
	}

	private void Relay_False_714()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_714.False(out logic_uScriptAct_SetBool_Target_714);
		local_Q3EnemyAlive_System_Boolean = logic_uScriptAct_SetBool_Target_714;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_714.Out)
		{
			Relay_False_711();
		}
	}

	private void Relay_True_716()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_716.True(out logic_uScriptAct_SetBool_Target_716);
		local_CanPushButtons_System_Boolean = logic_uScriptAct_SetBool_Target_716;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_716.Out)
		{
			Relay_In_713();
		}
	}

	private void Relay_False_716()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_716.False(out logic_uScriptAct_SetBool_Target_716);
		local_CanPushButtons_System_Boolean = logic_uScriptAct_SetBool_Target_716;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_716.Out)
		{
			Relay_In_713();
		}
	}

	private void Relay_In_720()
	{
		logic_uScriptCon_CompareBool_Bool_720 = Q3Button1;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_720.In(logic_uScriptCon_CompareBool_Bool_720);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_720.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_720.False;
		if (num)
		{
			Relay_False_726();
		}
		if (flag)
		{
			Relay_True_723();
		}
	}

	private void Relay_True_721()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_721.True(out logic_uScriptAct_SetBool_Target_721);
		local_CanPushButtons_System_Boolean = logic_uScriptAct_SetBool_Target_721;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_721.Out)
		{
			Relay_In_728();
		}
	}

	private void Relay_False_721()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_721.False(out logic_uScriptAct_SetBool_Target_721);
		local_CanPushButtons_System_Boolean = logic_uScriptAct_SetBool_Target_721;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_721.Out)
		{
			Relay_In_728();
		}
	}

	private void Relay_True_723()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_723.True(out logic_uScriptAct_SetBool_Target_723);
		local_Question03WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_723;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_723.Out)
		{
			Relay_False_721();
		}
	}

	private void Relay_False_723()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_723.False(out logic_uScriptAct_SetBool_Target_723);
		local_Question03WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_723;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_723.Out)
		{
			Relay_False_721();
		}
	}

	private void Relay_True_726()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_726.True(out logic_uScriptAct_SetBool_Target_726);
		local_Question03WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_726;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_726.Out)
		{
			Relay_False_727();
		}
	}

	private void Relay_False_726()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_726.False(out logic_uScriptAct_SetBool_Target_726);
		local_Question03WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_726;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_726.Out)
		{
			Relay_False_727();
		}
	}

	private void Relay_True_727()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_727.True(out logic_uScriptAct_SetBool_Target_727);
		local_CanPushButtons_System_Boolean = logic_uScriptAct_SetBool_Target_727;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_727.Out)
		{
			Relay_In_732();
		}
	}

	private void Relay_False_727()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_727.False(out logic_uScriptAct_SetBool_Target_727);
		local_CanPushButtons_System_Boolean = logic_uScriptAct_SetBool_Target_727;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_727.Out)
		{
			Relay_In_732();
		}
	}

	private void Relay_In_728()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_728.In(logic_uScriptAct_SetInt_Value_728, out logic_uScriptAct_SetInt_Target_728);
		local_QuestionThree_System_Int32 = logic_uScriptAct_SetInt_Target_728;
	}

	private void Relay_In_732()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_732.In(logic_uScriptAct_SetInt_Value_732, out logic_uScriptAct_SetInt_Target_732);
		local_QuestionThree_System_Int32 = logic_uScriptAct_SetInt_Target_732;
	}

	private void Relay_In_733()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_733.In(logic_uScriptAct_SetInt_Value_733, out logic_uScriptAct_SetInt_Target_733);
		local_QuestionFour_System_Int32 = logic_uScriptAct_SetInt_Target_733;
	}

	private void Relay_In_734()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_734.In(logic_uScriptAct_SetInt_Value_734, out logic_uScriptAct_SetInt_Target_734);
		local_QuestionsStage_System_Int32 = logic_uScriptAct_SetInt_Target_734;
		if (logic_uScriptAct_SetInt_uScriptAct_SetInt_734.Out)
		{
			Relay_In_733();
		}
	}

	private void Relay_In_739()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_739.In(logic_uScriptAct_SetInt_Value_739, out logic_uScriptAct_SetInt_Target_739);
		local_QuestionThree_System_Int32 = logic_uScriptAct_SetInt_Target_739;
	}

	private void Relay_In_740()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_740.In(logic_uScriptAct_SetInt_Value_740, out logic_uScriptAct_SetInt_Target_740);
		local_QuestionThree_System_Int32 = logic_uScriptAct_SetInt_Target_740;
	}

	private void Relay_True_742()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_742.True(out logic_uScriptAct_SetBool_Target_742);
		local_CanPushButtons_System_Boolean = logic_uScriptAct_SetBool_Target_742;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_742.Out)
		{
			Relay_In_739();
		}
	}

	private void Relay_False_742()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_742.False(out logic_uScriptAct_SetBool_Target_742);
		local_CanPushButtons_System_Boolean = logic_uScriptAct_SetBool_Target_742;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_742.Out)
		{
			Relay_In_739();
		}
	}

	private void Relay_In_744()
	{
		logic_uScriptCon_CompareInt_A_744 = local_Button2Value_System_Int32;
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_744.In(logic_uScriptCon_CompareInt_A_744, logic_uScriptCon_CompareInt_B_744);
		bool greaterThan = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_744.GreaterThan;
		bool lessThanOrEqualTo = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_744.LessThanOrEqualTo;
		if (greaterThan)
		{
			Relay_In_753();
		}
		if (lessThanOrEqualTo)
		{
			Relay_In_784();
		}
	}

	private void Relay_True_747()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_747.True(out logic_uScriptAct_SetBool_Target_747);
		local_Question03WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_747;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_747.Out)
		{
			Relay_False_748();
		}
	}

	private void Relay_False_747()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_747.False(out logic_uScriptAct_SetBool_Target_747);
		local_Question03WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_747;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_747.Out)
		{
			Relay_False_748();
		}
	}

	private void Relay_True_748()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_748.True(out logic_uScriptAct_SetBool_Target_748);
		local_CanPushButtons_System_Boolean = logic_uScriptAct_SetBool_Target_748;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_748.Out)
		{
			Relay_In_740();
		}
	}

	private void Relay_False_748()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_748.False(out logic_uScriptAct_SetBool_Target_748);
		local_CanPushButtons_System_Boolean = logic_uScriptAct_SetBool_Target_748;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_748.Out)
		{
			Relay_In_740();
		}
	}

	private void Relay_True_750()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_750.True(out logic_uScriptAct_SetBool_Target_750);
		local_Question03WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_750;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_750.Out)
		{
			Relay_False_742();
		}
	}

	private void Relay_False_750()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_750.False(out logic_uScriptAct_SetBool_Target_750);
		local_Question03WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_750;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_750.Out)
		{
			Relay_False_742();
		}
	}

	private void Relay_In_751()
	{
		logic_uScript_GetCircuitChargeInfo_block_751 = local_ButtonBlock2_TankBlock;
		logic_uScript_GetCircuitChargeInfo_Return_751 = logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_751.In(logic_uScript_GetCircuitChargeInfo_block_751, logic_uScript_GetCircuitChargeInfo_tech_751, logic_uScript_GetCircuitChargeInfo_blockType_751);
		local_Button2Value_System_Int32 = logic_uScript_GetCircuitChargeInfo_Return_751;
		if (logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_751.Out)
		{
			Relay_In_744();
		}
	}

	private void Relay_In_753()
	{
		logic_uScriptCon_CompareBool_Bool_753 = Q3Button2;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_753.In(logic_uScriptCon_CompareBool_Bool_753);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_753.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_753.False;
		if (num)
		{
			Relay_False_747();
		}
		if (flag)
		{
			Relay_True_750();
		}
	}

	private void Relay_True_758()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_758.True(out logic_uScriptAct_SetBool_Target_758);
		local_Question03WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_758;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_758.Out)
		{
			Relay_False_759();
		}
	}

	private void Relay_False_758()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_758.False(out logic_uScriptAct_SetBool_Target_758);
		local_Question03WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_758;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_758.Out)
		{
			Relay_False_759();
		}
	}

	private void Relay_True_759()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_759.True(out logic_uScriptAct_SetBool_Target_759);
		local_CanPushButtons_System_Boolean = logic_uScriptAct_SetBool_Target_759;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_759.Out)
		{
			Relay_In_771();
		}
	}

	private void Relay_False_759()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_759.False(out logic_uScriptAct_SetBool_Target_759);
		local_CanPushButtons_System_Boolean = logic_uScriptAct_SetBool_Target_759;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_759.Out)
		{
			Relay_In_771();
		}
	}

	private void Relay_True_760()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_760.True(out logic_uScriptAct_SetBool_Target_760);
		local_CanPushButtons_System_Boolean = logic_uScriptAct_SetBool_Target_760;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_760.Out)
		{
			Relay_In_789();
		}
	}

	private void Relay_False_760()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_760.False(out logic_uScriptAct_SetBool_Target_760);
		local_CanPushButtons_System_Boolean = logic_uScriptAct_SetBool_Target_760;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_760.Out)
		{
			Relay_In_789();
		}
	}

	private void Relay_In_761()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_761.In(logic_uScriptAct_SetInt_Value_761, out logic_uScriptAct_SetInt_Target_761);
		local_QuestionThree_System_Int32 = logic_uScriptAct_SetInt_Target_761;
	}

	private void Relay_In_762()
	{
		logic_uScriptCon_CompareBool_Bool_762 = Q3Button4;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_762.In(logic_uScriptCon_CompareBool_Bool_762);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_762.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_762.False;
		if (num)
		{
			Relay_False_772();
		}
		if (flag)
		{
			Relay_True_758();
		}
	}

	private void Relay_In_763()
	{
		logic_uScript_GetCircuitChargeInfo_block_763 = local_ButtonBlock4_TankBlock;
		logic_uScript_GetCircuitChargeInfo_Return_763 = logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_763.In(logic_uScript_GetCircuitChargeInfo_block_763, logic_uScript_GetCircuitChargeInfo_tech_763, logic_uScript_GetCircuitChargeInfo_blockType_763);
		local_Button4Value_System_Int32 = logic_uScript_GetCircuitChargeInfo_Return_763;
		if (logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_763.Out)
		{
			Relay_In_777();
		}
	}

	private void Relay_In_764()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_764.In(logic_uScriptAct_SetInt_Value_764, out logic_uScriptAct_SetInt_Target_764);
		local_QuestionThree_System_Int32 = logic_uScriptAct_SetInt_Target_764;
	}

	private void Relay_True_767()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_767.True(out logic_uScriptAct_SetBool_Target_767);
		local_CanPushButtons_System_Boolean = logic_uScriptAct_SetBool_Target_767;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_767.Out)
		{
			Relay_In_761();
		}
	}

	private void Relay_False_767()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_767.False(out logic_uScriptAct_SetBool_Target_767);
		local_CanPushButtons_System_Boolean = logic_uScriptAct_SetBool_Target_767;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_767.Out)
		{
			Relay_In_761();
		}
	}

	private void Relay_In_769()
	{
		logic_uScriptCon_CompareInt_A_769 = local_Button3Value_System_Int32;
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_769.In(logic_uScriptCon_CompareInt_A_769, logic_uScriptCon_CompareInt_B_769);
		bool greaterThan = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_769.GreaterThan;
		bool lessThanOrEqualTo = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_769.LessThanOrEqualTo;
		if (greaterThan)
		{
			Relay_In_787();
		}
		if (lessThanOrEqualTo)
		{
			Relay_In_763();
		}
	}

	private void Relay_In_771()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_771.In(logic_uScriptAct_SetInt_Value_771, out logic_uScriptAct_SetInt_Target_771);
		local_QuestionThree_System_Int32 = logic_uScriptAct_SetInt_Target_771;
	}

	private void Relay_True_772()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_772.True(out logic_uScriptAct_SetBool_Target_772);
		local_Question03WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_772;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_772.Out)
		{
			Relay_False_760();
		}
	}

	private void Relay_False_772()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_772.False(out logic_uScriptAct_SetBool_Target_772);
		local_Question03WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_772;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_772.Out)
		{
			Relay_False_760();
		}
	}

	private void Relay_True_776()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_776.True(out logic_uScriptAct_SetBool_Target_776);
		local_Question03WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_776;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_776.Out)
		{
			Relay_False_778();
		}
	}

	private void Relay_False_776()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_776.False(out logic_uScriptAct_SetBool_Target_776);
		local_Question03WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_776;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_776.Out)
		{
			Relay_False_778();
		}
	}

	private void Relay_In_777()
	{
		logic_uScriptCon_CompareInt_A_777 = local_Button4Value_System_Int32;
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_777.In(logic_uScriptCon_CompareInt_A_777, logic_uScriptCon_CompareInt_B_777);
		if (logic_uScriptCon_CompareInt_uScriptCon_CompareInt_777.GreaterThan)
		{
			Relay_In_762();
		}
	}

	private void Relay_True_778()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_778.True(out logic_uScriptAct_SetBool_Target_778);
		local_CanPushButtons_System_Boolean = logic_uScriptAct_SetBool_Target_778;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_778.Out)
		{
			Relay_In_764();
		}
	}

	private void Relay_False_778()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_778.False(out logic_uScriptAct_SetBool_Target_778);
		local_CanPushButtons_System_Boolean = logic_uScriptAct_SetBool_Target_778;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_778.Out)
		{
			Relay_In_764();
		}
	}

	private void Relay_True_781()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_781.True(out logic_uScriptAct_SetBool_Target_781);
		local_Question03WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_781;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_781.Out)
		{
			Relay_False_767();
		}
	}

	private void Relay_False_781()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_781.False(out logic_uScriptAct_SetBool_Target_781);
		local_Question03WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_781;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_781.Out)
		{
			Relay_False_767();
		}
	}

	private void Relay_In_784()
	{
		logic_uScript_GetCircuitChargeInfo_block_784 = local_ButtonBlock3_TankBlock;
		logic_uScript_GetCircuitChargeInfo_Return_784 = logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_784.In(logic_uScript_GetCircuitChargeInfo_block_784, logic_uScript_GetCircuitChargeInfo_tech_784, logic_uScript_GetCircuitChargeInfo_blockType_784);
		local_Button3Value_System_Int32 = logic_uScript_GetCircuitChargeInfo_Return_784;
		if (logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_784.Out)
		{
			Relay_In_769();
		}
	}

	private void Relay_In_787()
	{
		logic_uScriptCon_CompareBool_Bool_787 = Q3Button3;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_787.In(logic_uScriptCon_CompareBool_Bool_787);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_787.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_787.False;
		if (num)
		{
			Relay_False_776();
		}
		if (flag)
		{
			Relay_True_781();
		}
	}

	private void Relay_In_789()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_789.In(logic_uScriptAct_SetInt_Value_789, out logic_uScriptAct_SetInt_Target_789);
		local_QuestionThree_System_Int32 = logic_uScriptAct_SetInt_Target_789;
	}

	private void Relay_In_792()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_792.In(logic_uScriptAct_SetInt_Value_792, out logic_uScriptAct_SetInt_Target_792);
		local_QuestionsStage_System_Int32 = logic_uScriptAct_SetInt_Target_792;
		if (logic_uScriptAct_SetInt_uScriptAct_SetInt_792.Out)
		{
			Relay_In_794();
		}
	}

	private void Relay_In_794()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_794.In(logic_uScriptAct_SetInt_Value_794, out logic_uScriptAct_SetInt_Target_794);
		local_QuestionOne_System_Int32 = logic_uScriptAct_SetInt_Target_794;
	}

	private void Relay_In_795()
	{
		logic_uScriptAct_AddInt_v2_A_795 = local_QuestionFour_System_Int32;
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_795.In(logic_uScriptAct_AddInt_v2_A_795, logic_uScriptAct_AddInt_v2_B_795, out logic_uScriptAct_AddInt_v2_IntResult_795, out logic_uScriptAct_AddInt_v2_FloatResult_795);
		local_QuestionFour_System_Int32 = logic_uScriptAct_AddInt_v2_IntResult_795;
	}

	private void Relay_True_796()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_796.True(out logic_uScriptAct_SetBool_Target_796);
		local_Question04WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_796;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_796.Out)
		{
			Relay_True_800();
		}
	}

	private void Relay_False_796()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_796.False(out logic_uScriptAct_SetBool_Target_796);
		local_Question04WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_796;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_796.Out)
		{
			Relay_True_800();
		}
	}

	private void Relay_True_800()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_800.True(out logic_uScriptAct_SetBool_Target_800);
		local_CanPushButtons_System_Boolean = logic_uScriptAct_SetBool_Target_800;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_800.Out)
		{
			Relay_In_795();
		}
	}

	private void Relay_False_800()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_800.False(out logic_uScriptAct_SetBool_Target_800);
		local_CanPushButtons_System_Boolean = logic_uScriptAct_SetBool_Target_800;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_800.Out)
		{
			Relay_In_795();
		}
	}

	private void Relay_True_801()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_801.True(out logic_uScriptAct_SetBool_Target_801);
		local_Q4EnemyAlive_System_Boolean = logic_uScriptAct_SetBool_Target_801;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_801.Out)
		{
			Relay_False_796();
		}
	}

	private void Relay_False_801()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_801.False(out logic_uScriptAct_SetBool_Target_801);
		local_Q4EnemyAlive_System_Boolean = logic_uScriptAct_SetBool_Target_801;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_801.Out)
		{
			Relay_False_796();
		}
	}

	private void Relay_In_805()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_805.In(logic_uScriptAct_SetInt_Value_805, out logic_uScriptAct_SetInt_Target_805);
		local_QuestionsStage_System_Int32 = logic_uScriptAct_SetInt_Target_805;
		if (logic_uScriptAct_SetInt_uScriptAct_SetInt_805.Out)
		{
			Relay_In_806();
		}
	}

	private void Relay_In_806()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_806.In(logic_uScriptAct_SetInt_Value_806, out logic_uScriptAct_SetInt_Target_806);
		local_QuestionOne_System_Int32 = logic_uScriptAct_SetInt_Target_806;
	}

	private void Relay_In_807()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_807.In(logic_uScriptAct_SetInt_Value_807, out logic_uScriptAct_SetInt_Target_807);
		local_QuestionsStage_System_Int32 = logic_uScriptAct_SetInt_Target_807;
		if (logic_uScriptAct_SetInt_uScriptAct_SetInt_807.Out)
		{
			Relay_In_810();
		}
	}

	private void Relay_In_810()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_810.In(logic_uScriptAct_SetInt_Value_810, out logic_uScriptAct_SetInt_Target_810);
		local_QuestionFive_System_Int32 = logic_uScriptAct_SetInt_Target_810;
	}

	private void Relay_In_813()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_813.In(logic_uScriptAct_SetInt_Value_813, out logic_uScriptAct_SetInt_Target_813);
		local_QuestionFour_System_Int32 = logic_uScriptAct_SetInt_Target_813;
	}

	private void Relay_In_814()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_814.In(logic_uScriptAct_SetInt_Value_814, out logic_uScriptAct_SetInt_Target_814);
		local_QuestionFour_System_Int32 = logic_uScriptAct_SetInt_Target_814;
	}

	private void Relay_True_816()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_816.True(out logic_uScriptAct_SetBool_Target_816);
		local_CanPushButtons_System_Boolean = logic_uScriptAct_SetBool_Target_816;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_816.Out)
		{
			Relay_In_813();
		}
	}

	private void Relay_False_816()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_816.False(out logic_uScriptAct_SetBool_Target_816);
		local_CanPushButtons_System_Boolean = logic_uScriptAct_SetBool_Target_816;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_816.Out)
		{
			Relay_In_813();
		}
	}

	private void Relay_True_819()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_819.True(out logic_uScriptAct_SetBool_Target_819);
		local_Question04WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_819;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_819.Out)
		{
			Relay_False_820();
		}
	}

	private void Relay_False_819()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_819.False(out logic_uScriptAct_SetBool_Target_819);
		local_Question04WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_819;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_819.Out)
		{
			Relay_False_820();
		}
	}

	private void Relay_True_820()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_820.True(out logic_uScriptAct_SetBool_Target_820);
		local_CanPushButtons_System_Boolean = logic_uScriptAct_SetBool_Target_820;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_820.Out)
		{
			Relay_In_814();
		}
	}

	private void Relay_False_820()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_820.False(out logic_uScriptAct_SetBool_Target_820);
		local_CanPushButtons_System_Boolean = logic_uScriptAct_SetBool_Target_820;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_820.Out)
		{
			Relay_In_814();
		}
	}

	private void Relay_True_822()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_822.True(out logic_uScriptAct_SetBool_Target_822);
		local_Question04WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_822;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_822.Out)
		{
			Relay_False_816();
		}
	}

	private void Relay_False_822()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_822.False(out logic_uScriptAct_SetBool_Target_822);
		local_Question04WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_822;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_822.Out)
		{
			Relay_False_816();
		}
	}

	private void Relay_In_824()
	{
		logic_uScriptCon_CompareBool_Bool_824 = Q4Button1;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_824.In(logic_uScriptCon_CompareBool_Bool_824);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_824.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_824.False;
		if (num)
		{
			Relay_False_819();
		}
		if (flag)
		{
			Relay_True_822();
		}
	}

	private void Relay_In_825()
	{
		logic_uScriptCon_CompareBool_Bool_825 = Q4Button2;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_825.In(logic_uScriptCon_CompareBool_Bool_825);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_825.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_825.False;
		if (num)
		{
			Relay_False_832();
		}
		if (flag)
		{
			Relay_True_838();
		}
	}

	private void Relay_In_826()
	{
		logic_uScriptCon_CompareInt_A_826 = local_Button2Value_System_Int32;
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_826.In(logic_uScriptCon_CompareInt_A_826, logic_uScriptCon_CompareInt_B_826);
		bool greaterThan = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_826.GreaterThan;
		bool lessThanOrEqualTo = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_826.LessThanOrEqualTo;
		if (greaterThan)
		{
			Relay_In_825();
		}
		if (lessThanOrEqualTo)
		{
			Relay_In_843();
		}
	}

	private void Relay_In_831()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_831.In(logic_uScriptAct_SetInt_Value_831, out logic_uScriptAct_SetInt_Target_831);
		local_QuestionFour_System_Int32 = logic_uScriptAct_SetInt_Target_831;
	}

	private void Relay_True_832()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_832.True(out logic_uScriptAct_SetBool_Target_832);
		local_Question04WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_832;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_832.Out)
		{
			Relay_False_834();
		}
	}

	private void Relay_False_832()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_832.False(out logic_uScriptAct_SetBool_Target_832);
		local_Question04WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_832;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_832.Out)
		{
			Relay_False_834();
		}
	}

	private void Relay_True_834()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_834.True(out logic_uScriptAct_SetBool_Target_834);
		local_CanPushButtons_System_Boolean = logic_uScriptAct_SetBool_Target_834;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_834.Out)
		{
			Relay_In_836();
		}
	}

	private void Relay_False_834()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_834.False(out logic_uScriptAct_SetBool_Target_834);
		local_CanPushButtons_System_Boolean = logic_uScriptAct_SetBool_Target_834;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_834.Out)
		{
			Relay_In_836();
		}
	}

	private void Relay_In_836()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_836.In(logic_uScriptAct_SetInt_Value_836, out logic_uScriptAct_SetInt_Target_836);
		local_QuestionFour_System_Int32 = logic_uScriptAct_SetInt_Target_836;
	}

	private void Relay_True_838()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_838.True(out logic_uScriptAct_SetBool_Target_838);
		local_Question04WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_838;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_838.Out)
		{
			Relay_False_842();
		}
	}

	private void Relay_False_838()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_838.False(out logic_uScriptAct_SetBool_Target_838);
		local_Question04WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_838;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_838.Out)
		{
			Relay_False_842();
		}
	}

	private void Relay_In_841()
	{
		logic_uScript_GetCircuitChargeInfo_block_841 = local_ButtonBlock2_TankBlock;
		logic_uScript_GetCircuitChargeInfo_Return_841 = logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_841.In(logic_uScript_GetCircuitChargeInfo_block_841, logic_uScript_GetCircuitChargeInfo_tech_841, logic_uScript_GetCircuitChargeInfo_blockType_841);
		local_Button2Value_System_Int32 = logic_uScript_GetCircuitChargeInfo_Return_841;
		if (logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_841.Out)
		{
			Relay_In_826();
		}
	}

	private void Relay_True_842()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_842.True(out logic_uScriptAct_SetBool_Target_842);
		local_CanPushButtons_System_Boolean = logic_uScriptAct_SetBool_Target_842;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_842.Out)
		{
			Relay_In_831();
		}
	}

	private void Relay_False_842()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_842.False(out logic_uScriptAct_SetBool_Target_842);
		local_CanPushButtons_System_Boolean = logic_uScriptAct_SetBool_Target_842;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_842.Out)
		{
			Relay_In_831();
		}
	}

	private void Relay_In_843()
	{
		logic_uScript_GetCircuitChargeInfo_block_843 = local_ButtonBlock3_TankBlock;
		logic_uScript_GetCircuitChargeInfo_Return_843 = logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_843.In(logic_uScript_GetCircuitChargeInfo_block_843, logic_uScript_GetCircuitChargeInfo_tech_843, logic_uScript_GetCircuitChargeInfo_blockType_843);
		local_Button3Value_System_Int32 = logic_uScript_GetCircuitChargeInfo_Return_843;
		if (logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_843.Out)
		{
			Relay_In_858();
		}
	}

	private void Relay_In_844()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_844.In(logic_uScriptAct_SetInt_Value_844, out logic_uScriptAct_SetInt_Target_844);
		local_QuestionFour_System_Int32 = logic_uScriptAct_SetInt_Target_844;
	}

	private void Relay_True_850()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_850.True(out logic_uScriptAct_SetBool_Target_850);
		local_Question04WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_850;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_850.Out)
		{
			Relay_False_856();
		}
	}

	private void Relay_False_850()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_850.False(out logic_uScriptAct_SetBool_Target_850);
		local_Question04WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_850;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_850.Out)
		{
			Relay_False_856();
		}
	}

	private void Relay_In_851()
	{
		logic_uScriptCon_CompareBool_Bool_851 = Q4Button3;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_851.In(logic_uScriptCon_CompareBool_Bool_851);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_851.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_851.False;
		if (num)
		{
			Relay_False_850();
		}
		if (flag)
		{
			Relay_True_859();
		}
	}

	private void Relay_True_852()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_852.True(out logic_uScriptAct_SetBool_Target_852);
		local_CanPushButtons_System_Boolean = logic_uScriptAct_SetBool_Target_852;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_852.Out)
		{
			Relay_In_857();
		}
	}

	private void Relay_False_852()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_852.False(out logic_uScriptAct_SetBool_Target_852);
		local_CanPushButtons_System_Boolean = logic_uScriptAct_SetBool_Target_852;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_852.Out)
		{
			Relay_In_857();
		}
	}

	private void Relay_True_856()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_856.True(out logic_uScriptAct_SetBool_Target_856);
		local_CanPushButtons_System_Boolean = logic_uScriptAct_SetBool_Target_856;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_856.Out)
		{
			Relay_In_844();
		}
	}

	private void Relay_False_856()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_856.False(out logic_uScriptAct_SetBool_Target_856);
		local_CanPushButtons_System_Boolean = logic_uScriptAct_SetBool_Target_856;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_856.Out)
		{
			Relay_In_844();
		}
	}

	private void Relay_In_857()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_857.In(logic_uScriptAct_SetInt_Value_857, out logic_uScriptAct_SetInt_Target_857);
		local_QuestionFour_System_Int32 = logic_uScriptAct_SetInt_Target_857;
	}

	private void Relay_In_858()
	{
		logic_uScriptCon_CompareInt_A_858 = local_Button3Value_System_Int32;
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_858.In(logic_uScriptCon_CompareInt_A_858, logic_uScriptCon_CompareInt_B_858);
		bool greaterThan = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_858.GreaterThan;
		bool lessThanOrEqualTo = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_858.LessThanOrEqualTo;
		if (greaterThan)
		{
			Relay_In_851();
		}
		if (lessThanOrEqualTo)
		{
			Relay_In_877();
		}
	}

	private void Relay_True_859()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_859.True(out logic_uScriptAct_SetBool_Target_859);
		local_Question04WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_859;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_859.Out)
		{
			Relay_False_852();
		}
	}

	private void Relay_False_859()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_859.False(out logic_uScriptAct_SetBool_Target_859);
		local_Question04WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_859;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_859.Out)
		{
			Relay_False_852();
		}
	}

	private void Relay_In_861()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_861.In(logic_uScriptAct_SetInt_Value_861, out logic_uScriptAct_SetInt_Target_861);
		local_QuestionFour_System_Int32 = logic_uScriptAct_SetInt_Target_861;
	}

	private void Relay_In_862()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_862.In(logic_uScriptAct_SetInt_Value_862, out logic_uScriptAct_SetInt_Target_862);
		local_QuestionFour_System_Int32 = logic_uScriptAct_SetInt_Target_862;
	}

	private void Relay_True_863()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_863.True(out logic_uScriptAct_SetBool_Target_863);
		local_CanPushButtons_System_Boolean = logic_uScriptAct_SetBool_Target_863;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_863.Out)
		{
			Relay_In_861();
		}
	}

	private void Relay_False_863()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_863.False(out logic_uScriptAct_SetBool_Target_863);
		local_CanPushButtons_System_Boolean = logic_uScriptAct_SetBool_Target_863;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_863.Out)
		{
			Relay_In_861();
		}
	}

	private void Relay_True_867()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_867.True(out logic_uScriptAct_SetBool_Target_867);
		local_Question04WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_867;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_867.Out)
		{
			Relay_False_868();
		}
	}

	private void Relay_False_867()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_867.False(out logic_uScriptAct_SetBool_Target_867);
		local_Question04WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_867;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_867.Out)
		{
			Relay_False_868();
		}
	}

	private void Relay_True_868()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_868.True(out logic_uScriptAct_SetBool_Target_868);
		local_CanPushButtons_System_Boolean = logic_uScriptAct_SetBool_Target_868;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_868.Out)
		{
			Relay_In_862();
		}
	}

	private void Relay_False_868()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_868.False(out logic_uScriptAct_SetBool_Target_868);
		local_CanPushButtons_System_Boolean = logic_uScriptAct_SetBool_Target_868;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_868.Out)
		{
			Relay_In_862();
		}
	}

	private void Relay_In_871()
	{
		logic_uScriptCon_CompareBool_Bool_871 = Q4Button4;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_871.In(logic_uScriptCon_CompareBool_Bool_871);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_871.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_871.False;
		if (num)
		{
			Relay_False_878();
		}
		if (flag)
		{
			Relay_True_867();
		}
	}

	private void Relay_In_874()
	{
		logic_uScriptCon_CompareInt_A_874 = local_Button4Value_System_Int32;
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_874.In(logic_uScriptCon_CompareInt_A_874, logic_uScriptCon_CompareInt_B_874);
		if (logic_uScriptCon_CompareInt_uScriptCon_CompareInt_874.GreaterThan)
		{
			Relay_In_871();
		}
	}

	private void Relay_In_877()
	{
		logic_uScript_GetCircuitChargeInfo_block_877 = local_ButtonBlock4_TankBlock;
		logic_uScript_GetCircuitChargeInfo_Return_877 = logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_877.In(logic_uScript_GetCircuitChargeInfo_block_877, logic_uScript_GetCircuitChargeInfo_tech_877, logic_uScript_GetCircuitChargeInfo_blockType_877);
		local_Button4Value_System_Int32 = logic_uScript_GetCircuitChargeInfo_Return_877;
		if (logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_877.Out)
		{
			Relay_In_874();
		}
	}

	private void Relay_True_878()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_878.True(out logic_uScriptAct_SetBool_Target_878);
		local_Question04WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_878;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_878.Out)
		{
			Relay_False_863();
		}
	}

	private void Relay_False_878()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_878.False(out logic_uScriptAct_SetBool_Target_878);
		local_Question04WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_878;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_878.Out)
		{
			Relay_False_863();
		}
	}

	private void Relay_In_879()
	{
		logic_uScriptCon_CompareBool_Bool_879 = Q5Button1;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_879.In(logic_uScriptCon_CompareBool_Bool_879);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_879.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_879.False;
		if (num)
		{
			Relay_False_883();
		}
		if (flag)
		{
			Relay_True_889();
		}
	}

	private void Relay_In_882()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_882.In(logic_uScriptAct_SetInt_Value_882, out logic_uScriptAct_SetInt_Target_882);
		local_QuestionFive_System_Int32 = logic_uScriptAct_SetInt_Target_882;
	}

	private void Relay_True_883()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_883.True(out logic_uScriptAct_SetBool_Target_883);
		local_Question05WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_883;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_883.Out)
		{
			Relay_False_885();
		}
	}

	private void Relay_False_883()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_883.False(out logic_uScriptAct_SetBool_Target_883);
		local_Question05WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_883;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_883.Out)
		{
			Relay_False_885();
		}
	}

	private void Relay_True_885()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_885.True(out logic_uScriptAct_SetBool_Target_885);
		local_CanPushButtons_System_Boolean = logic_uScriptAct_SetBool_Target_885;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_885.Out)
		{
			Relay_In_887();
		}
	}

	private void Relay_False_885()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_885.False(out logic_uScriptAct_SetBool_Target_885);
		local_CanPushButtons_System_Boolean = logic_uScriptAct_SetBool_Target_885;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_885.Out)
		{
			Relay_In_887();
		}
	}

	private void Relay_In_887()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_887.In(logic_uScriptAct_SetInt_Value_887, out logic_uScriptAct_SetInt_Target_887);
		local_QuestionFive_System_Int32 = logic_uScriptAct_SetInt_Target_887;
	}

	private void Relay_True_889()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_889.True(out logic_uScriptAct_SetBool_Target_889);
		local_Question05WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_889;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_889.Out)
		{
			Relay_False_892();
		}
	}

	private void Relay_False_889()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_889.False(out logic_uScriptAct_SetBool_Target_889);
		local_Question05WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_889;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_889.Out)
		{
			Relay_False_892();
		}
	}

	private void Relay_True_892()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_892.True(out logic_uScriptAct_SetBool_Target_892);
		local_CanPushButtons_System_Boolean = logic_uScriptAct_SetBool_Target_892;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_892.Out)
		{
			Relay_In_882();
		}
	}

	private void Relay_False_892()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_892.False(out logic_uScriptAct_SetBool_Target_892);
		local_CanPushButtons_System_Boolean = logic_uScriptAct_SetBool_Target_892;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_892.Out)
		{
			Relay_In_882();
		}
	}

	private void Relay_True_899()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_899.True(out logic_uScriptAct_SetBool_Target_899);
		local_CanPushButtons_System_Boolean = logic_uScriptAct_SetBool_Target_899;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_899.Out)
		{
			Relay_In_903();
		}
	}

	private void Relay_False_899()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_899.False(out logic_uScriptAct_SetBool_Target_899);
		local_CanPushButtons_System_Boolean = logic_uScriptAct_SetBool_Target_899;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_899.Out)
		{
			Relay_In_903();
		}
	}

	private void Relay_True_900()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_900.True(out logic_uScriptAct_SetBool_Target_900);
		local_Question05WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_900;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_900.Out)
		{
			Relay_False_909();
		}
	}

	private void Relay_False_900()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_900.False(out logic_uScriptAct_SetBool_Target_900);
		local_Question05WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_900;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_900.Out)
		{
			Relay_False_909();
		}
	}

	private void Relay_In_902()
	{
		logic_uScriptCon_CompareInt_A_902 = local_Button2Value_System_Int32;
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_902.In(logic_uScriptCon_CompareInt_A_902, logic_uScriptCon_CompareInt_B_902);
		bool greaterThan = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_902.GreaterThan;
		bool lessThanOrEqualTo = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_902.LessThanOrEqualTo;
		if (greaterThan)
		{
			Relay_In_905();
		}
		if (lessThanOrEqualTo)
		{
			Relay_In_944();
		}
	}

	private void Relay_In_903()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_903.In(logic_uScriptAct_SetInt_Value_903, out logic_uScriptAct_SetInt_Target_903);
		local_QuestionFive_System_Int32 = logic_uScriptAct_SetInt_Target_903;
	}

	private void Relay_In_904()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_904.In(logic_uScriptAct_SetInt_Value_904, out logic_uScriptAct_SetInt_Target_904);
		local_QuestionFive_System_Int32 = logic_uScriptAct_SetInt_Target_904;
	}

	private void Relay_In_905()
	{
		logic_uScriptCon_CompareBool_Bool_905 = Q5Button2;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_905.In(logic_uScriptCon_CompareBool_Bool_905);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_905.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_905.False;
		if (num)
		{
			Relay_False_900();
		}
		if (flag)
		{
			Relay_True_907();
		}
	}

	private void Relay_True_907()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_907.True(out logic_uScriptAct_SetBool_Target_907);
		local_Question05WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_907;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_907.Out)
		{
			Relay_False_899();
		}
	}

	private void Relay_False_907()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_907.False(out logic_uScriptAct_SetBool_Target_907);
		local_Question05WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_907;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_907.Out)
		{
			Relay_False_899();
		}
	}

	private void Relay_True_909()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_909.True(out logic_uScriptAct_SetBool_Target_909);
		local_CanPushButtons_System_Boolean = logic_uScriptAct_SetBool_Target_909;
	}

	private void Relay_False_909()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_909.False(out logic_uScriptAct_SetBool_Target_909);
		local_CanPushButtons_System_Boolean = logic_uScriptAct_SetBool_Target_909;
	}

	private void Relay_In_910()
	{
		logic_uScript_GetCircuitChargeInfo_block_910 = local_ButtonBlock2_TankBlock;
		logic_uScript_GetCircuitChargeInfo_Return_910 = logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_910.In(logic_uScript_GetCircuitChargeInfo_block_910, logic_uScript_GetCircuitChargeInfo_tech_910, logic_uScript_GetCircuitChargeInfo_blockType_910);
		local_Button2Value_System_Int32 = logic_uScript_GetCircuitChargeInfo_Return_910;
		if (logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_910.Out)
		{
			Relay_In_902();
		}
	}

	private void Relay_In_915()
	{
		logic_uScriptCon_CompareBool_Bool_915 = Q5Button4;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_915.In(logic_uScriptCon_CompareBool_Bool_915);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_915.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_915.False;
		if (num)
		{
			Relay_False_924();
		}
		if (flag)
		{
			Relay_True_941();
		}
	}

	private void Relay_True_921()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_921.True(out logic_uScriptAct_SetBool_Target_921);
		local_CanPushButtons_System_Boolean = logic_uScriptAct_SetBool_Target_921;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_921.Out)
		{
			Relay_In_928();
		}
	}

	private void Relay_False_921()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_921.False(out logic_uScriptAct_SetBool_Target_921);
		local_CanPushButtons_System_Boolean = logic_uScriptAct_SetBool_Target_921;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_921.Out)
		{
			Relay_In_928();
		}
	}

	private void Relay_In_922()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_922.In(logic_uScriptAct_SetInt_Value_922, out logic_uScriptAct_SetInt_Target_922);
		local_QuestionFive_System_Int32 = logic_uScriptAct_SetInt_Target_922;
	}

	private void Relay_True_923()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_923.True(out logic_uScriptAct_SetBool_Target_923);
		local_Question05WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_923;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_923.Out)
		{
			Relay_False_943();
		}
	}

	private void Relay_False_923()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_923.False(out logic_uScriptAct_SetBool_Target_923);
		local_Question05WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_923;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_923.Out)
		{
			Relay_False_943();
		}
	}

	private void Relay_True_924()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_924.True(out logic_uScriptAct_SetBool_Target_924);
		local_Question05WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_924;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_924.Out)
		{
			Relay_False_945();
		}
	}

	private void Relay_False_924()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_924.False(out logic_uScriptAct_SetBool_Target_924);
		local_Question05WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_924;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_924.Out)
		{
			Relay_False_945();
		}
	}

	private void Relay_In_926()
	{
		logic_uScript_GetCircuitChargeInfo_block_926 = local_ButtonBlock4_TankBlock;
		logic_uScript_GetCircuitChargeInfo_Return_926 = logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_926.In(logic_uScript_GetCircuitChargeInfo_block_926, logic_uScript_GetCircuitChargeInfo_tech_926, logic_uScript_GetCircuitChargeInfo_blockType_926);
		local_Button4Value_System_Int32 = logic_uScript_GetCircuitChargeInfo_Return_926;
		if (logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_926.Out)
		{
			Relay_In_936();
		}
	}

	private void Relay_In_927()
	{
		logic_uScriptCon_CompareInt_A_927 = local_Button3Value_System_Int32;
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_927.In(logic_uScriptCon_CompareInt_A_927, logic_uScriptCon_CompareInt_B_927);
		bool greaterThan = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_927.GreaterThan;
		bool lessThanOrEqualTo = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_927.LessThanOrEqualTo;
		if (greaterThan)
		{
			Relay_In_932();
		}
		if (lessThanOrEqualTo)
		{
			Relay_In_926();
		}
	}

	private void Relay_In_928()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_928.In(logic_uScriptAct_SetInt_Value_928, out logic_uScriptAct_SetInt_Target_928);
		local_QuestionFive_System_Int32 = logic_uScriptAct_SetInt_Target_928;
	}

	private void Relay_In_929()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_929.In(logic_uScriptAct_SetInt_Value_929, out logic_uScriptAct_SetInt_Target_929);
		local_QuestionFive_System_Int32 = logic_uScriptAct_SetInt_Target_929;
	}

	private void Relay_In_931()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_931.In(logic_uScriptAct_SetInt_Value_931, out logic_uScriptAct_SetInt_Target_931);
		local_QuestionFive_System_Int32 = logic_uScriptAct_SetInt_Target_931;
	}

	private void Relay_In_932()
	{
		logic_uScriptCon_CompareBool_Bool_932 = Q5Button3;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_932.In(logic_uScriptCon_CompareBool_Bool_932);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_932.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_932.False;
		if (num)
		{
			Relay_False_923();
		}
		if (flag)
		{
			Relay_True_938();
		}
	}

	private void Relay_True_933()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_933.True(out logic_uScriptAct_SetBool_Target_933);
		local_CanPushButtons_System_Boolean = logic_uScriptAct_SetBool_Target_933;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_933.Out)
		{
			Relay_In_931();
		}
	}

	private void Relay_False_933()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_933.False(out logic_uScriptAct_SetBool_Target_933);
		local_CanPushButtons_System_Boolean = logic_uScriptAct_SetBool_Target_933;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_933.Out)
		{
			Relay_In_931();
		}
	}

	private void Relay_In_936()
	{
		logic_uScriptCon_CompareInt_A_936 = local_Button4Value_System_Int32;
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_936.In(logic_uScriptCon_CompareInt_A_936, logic_uScriptCon_CompareInt_B_936);
		if (logic_uScriptCon_CompareInt_uScriptCon_CompareInt_936.GreaterThan)
		{
			Relay_In_915();
		}
	}

	private void Relay_True_938()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_938.True(out logic_uScriptAct_SetBool_Target_938);
		local_Question05WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_938;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_938.Out)
		{
			Relay_False_921();
		}
	}

	private void Relay_False_938()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_938.False(out logic_uScriptAct_SetBool_Target_938);
		local_Question05WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_938;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_938.Out)
		{
			Relay_False_921();
		}
	}

	private void Relay_True_941()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_941.True(out logic_uScriptAct_SetBool_Target_941);
		local_Question05WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_941;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_941.Out)
		{
			Relay_False_933();
		}
	}

	private void Relay_False_941()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_941.False(out logic_uScriptAct_SetBool_Target_941);
		local_Question05WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_941;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_941.Out)
		{
			Relay_False_933();
		}
	}

	private void Relay_True_943()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_943.True(out logic_uScriptAct_SetBool_Target_943);
		local_CanPushButtons_System_Boolean = logic_uScriptAct_SetBool_Target_943;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_943.Out)
		{
			Relay_In_929();
		}
	}

	private void Relay_False_943()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_943.False(out logic_uScriptAct_SetBool_Target_943);
		local_CanPushButtons_System_Boolean = logic_uScriptAct_SetBool_Target_943;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_943.Out)
		{
			Relay_In_929();
		}
	}

	private void Relay_In_944()
	{
		logic_uScript_GetCircuitChargeInfo_block_944 = local_ButtonBlock3_TankBlock;
		logic_uScript_GetCircuitChargeInfo_Return_944 = logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_944.In(logic_uScript_GetCircuitChargeInfo_block_944, logic_uScript_GetCircuitChargeInfo_tech_944, logic_uScript_GetCircuitChargeInfo_blockType_944);
		local_Button3Value_System_Int32 = logic_uScript_GetCircuitChargeInfo_Return_944;
		if (logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_944.Out)
		{
			Relay_In_927();
		}
	}

	private void Relay_True_945()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_945.True(out logic_uScriptAct_SetBool_Target_945);
		local_CanPushButtons_System_Boolean = logic_uScriptAct_SetBool_Target_945;
	}

	private void Relay_False_945()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_945.False(out logic_uScriptAct_SetBool_Target_945);
		local_CanPushButtons_System_Boolean = logic_uScriptAct_SetBool_Target_945;
	}

	private void Relay_In_948()
	{
		logic_uScriptAct_AddInt_v2_A_948 = local_QuestionFive_System_Int32;
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_948.In(logic_uScriptAct_AddInt_v2_A_948, logic_uScriptAct_AddInt_v2_B_948, out logic_uScriptAct_AddInt_v2_IntResult_948, out logic_uScriptAct_AddInt_v2_FloatResult_948);
		local_QuestionFive_System_Int32 = logic_uScriptAct_AddInt_v2_IntResult_948;
	}

	private void Relay_True_949()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_949.True(out logic_uScriptAct_SetBool_Target_949);
		local_CanPushButtons_System_Boolean = logic_uScriptAct_SetBool_Target_949;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_949.Out)
		{
			Relay_In_948();
		}
	}

	private void Relay_False_949()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_949.False(out logic_uScriptAct_SetBool_Target_949);
		local_CanPushButtons_System_Boolean = logic_uScriptAct_SetBool_Target_949;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_949.Out)
		{
			Relay_In_948();
		}
	}

	private void Relay_True_950()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_950.True(out logic_uScriptAct_SetBool_Target_950);
		local_Question05WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_950;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_950.Out)
		{
			Relay_True_949();
		}
	}

	private void Relay_False_950()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_950.False(out logic_uScriptAct_SetBool_Target_950);
		local_Question05WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_950;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_950.Out)
		{
			Relay_True_949();
		}
	}

	private void Relay_True_954()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_954.True(out logic_uScriptAct_SetBool_Target_954);
		local_Q5EnemyAlive_System_Boolean = logic_uScriptAct_SetBool_Target_954;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_954.Out)
		{
			Relay_False_950();
		}
	}

	private void Relay_False_954()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_954.False(out logic_uScriptAct_SetBool_Target_954);
		local_Q5EnemyAlive_System_Boolean = logic_uScriptAct_SetBool_Target_954;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_954.Out)
		{
			Relay_False_950();
		}
	}

	private void Relay_In_957()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_957.In(logic_uScriptAct_SetInt_Value_957, out logic_uScriptAct_SetInt_Target_957);
		local_QuestionSix_System_Int32 = logic_uScriptAct_SetInt_Target_957;
	}

	private void Relay_In_958()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_958.In(logic_uScriptAct_SetInt_Value_958, out logic_uScriptAct_SetInt_Target_958);
		local_QuestionsStage_System_Int32 = logic_uScriptAct_SetInt_Target_958;
		if (logic_uScriptAct_SetInt_uScriptAct_SetInt_958.Out)
		{
			Relay_In_957();
		}
	}

	private void Relay_In_959()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_959.In(logic_uScriptAct_SetInt_Value_959, out logic_uScriptAct_SetInt_Target_959);
		local_QuestionsStage_System_Int32 = logic_uScriptAct_SetInt_Target_959;
		if (logic_uScriptAct_SetInt_uScriptAct_SetInt_959.Out)
		{
			Relay_In_962();
		}
	}

	private void Relay_In_962()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_962.In(logic_uScriptAct_SetInt_Value_962, out logic_uScriptAct_SetInt_Target_962);
		local_QuestionOne_System_Int32 = logic_uScriptAct_SetInt_Target_962;
	}

	private void Relay_True_965()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_965.True(out logic_uScriptAct_SetBool_Target_965);
		local_CanPushButtons_System_Boolean = logic_uScriptAct_SetBool_Target_965;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_965.Out)
		{
			Relay_In_967();
		}
	}

	private void Relay_False_965()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_965.False(out logic_uScriptAct_SetBool_Target_965);
		local_CanPushButtons_System_Boolean = logic_uScriptAct_SetBool_Target_965;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_965.Out)
		{
			Relay_In_967();
		}
	}

	private void Relay_True_966()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_966.True(out logic_uScriptAct_SetBool_Target_966);
		local_Question06WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_966;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_966.Out)
		{
			Relay_True_965();
		}
	}

	private void Relay_False_966()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_966.False(out logic_uScriptAct_SetBool_Target_966);
		local_Question06WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_966;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_966.Out)
		{
			Relay_True_965();
		}
	}

	private void Relay_In_967()
	{
		logic_uScriptAct_AddInt_v2_A_967 = local_QuestionSix_System_Int32;
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_967.In(logic_uScriptAct_AddInt_v2_A_967, logic_uScriptAct_AddInt_v2_B_967, out logic_uScriptAct_AddInt_v2_IntResult_967, out logic_uScriptAct_AddInt_v2_FloatResult_967);
		local_QuestionSix_System_Int32 = logic_uScriptAct_AddInt_v2_IntResult_967;
	}

	private void Relay_True_969()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_969.True(out logic_uScriptAct_SetBool_Target_969);
		local_Q6EnemyAlive_System_Boolean = logic_uScriptAct_SetBool_Target_969;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_969.Out)
		{
			Relay_False_966();
		}
	}

	private void Relay_False_969()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_969.False(out logic_uScriptAct_SetBool_Target_969);
		local_Q6EnemyAlive_System_Boolean = logic_uScriptAct_SetBool_Target_969;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_969.Out)
		{
			Relay_False_966();
		}
	}

	private void Relay_In_972()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_972.In(logic_uScriptAct_SetInt_Value_972, out logic_uScriptAct_SetInt_Target_972);
		local_QuestionsStage_System_Int32 = logic_uScriptAct_SetInt_Target_972;
		if (logic_uScriptAct_SetInt_uScriptAct_SetInt_972.Out)
		{
			Relay_In_974();
		}
	}

	private void Relay_In_974()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_974.In(logic_uScriptAct_SetInt_Value_974, out logic_uScriptAct_SetInt_Target_974);
		local_QuestionOne_System_Int32 = logic_uScriptAct_SetInt_Target_974;
	}

	private void Relay_True_980()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_980.True(out logic_uScriptAct_SetBool_Target_980);
		local_CanPushButtons_System_Boolean = logic_uScriptAct_SetBool_Target_980;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_980.Out)
		{
			Relay_In_982();
		}
	}

	private void Relay_False_980()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_980.False(out logic_uScriptAct_SetBool_Target_980);
		local_CanPushButtons_System_Boolean = logic_uScriptAct_SetBool_Target_980;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_980.Out)
		{
			Relay_In_982();
		}
	}

	private void Relay_True_981()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_981.True(out logic_uScriptAct_SetBool_Target_981);
		local_Question06WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_981;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_981.Out)
		{
			Relay_False_988();
		}
	}

	private void Relay_False_981()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_981.False(out logic_uScriptAct_SetBool_Target_981);
		local_Question06WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_981;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_981.Out)
		{
			Relay_False_988();
		}
	}

	private void Relay_In_982()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_982.In(logic_uScriptAct_SetInt_Value_982, out logic_uScriptAct_SetInt_Target_982);
		local_QuestionSix_System_Int32 = logic_uScriptAct_SetInt_Target_982;
	}

	private void Relay_In_983()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_983.In(logic_uScriptAct_SetInt_Value_983, out logic_uScriptAct_SetInt_Target_983);
		local_QuestionSix_System_Int32 = logic_uScriptAct_SetInt_Target_983;
	}

	private void Relay_In_984()
	{
		logic_uScriptCon_CompareBool_Bool_984 = Q6Button1;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_984.In(logic_uScriptCon_CompareBool_Bool_984);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_984.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_984.False;
		if (num)
		{
			Relay_False_981();
		}
		if (flag)
		{
			Relay_True_986();
		}
	}

	private void Relay_True_986()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_986.True(out logic_uScriptAct_SetBool_Target_986);
		local_Question06WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_986;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_986.Out)
		{
			Relay_False_980();
		}
	}

	private void Relay_False_986()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_986.False(out logic_uScriptAct_SetBool_Target_986);
		local_Question06WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_986;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_986.Out)
		{
			Relay_False_980();
		}
	}

	private void Relay_True_988()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_988.True(out logic_uScriptAct_SetBool_Target_988);
		local_CanPushButtons_System_Boolean = logic_uScriptAct_SetBool_Target_988;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_988.Out)
		{
			Relay_In_983();
		}
	}

	private void Relay_False_988()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_988.False(out logic_uScriptAct_SetBool_Target_988);
		local_CanPushButtons_System_Boolean = logic_uScriptAct_SetBool_Target_988;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_988.Out)
		{
			Relay_In_983();
		}
	}

	private void Relay_True_989()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_989.True(out logic_uScriptAct_SetBool_Target_989);
		local_Question06WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_989;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_989.Out)
		{
			Relay_False_997();
		}
	}

	private void Relay_False_989()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_989.False(out logic_uScriptAct_SetBool_Target_989);
		local_Question06WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_989;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_989.Out)
		{
			Relay_False_997();
		}
	}

	private void Relay_In_992()
	{
		logic_uScript_GetCircuitChargeInfo_block_992 = local_ButtonBlock2_TankBlock;
		logic_uScript_GetCircuitChargeInfo_Return_992 = logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_992.In(logic_uScript_GetCircuitChargeInfo_block_992, logic_uScript_GetCircuitChargeInfo_tech_992, logic_uScript_GetCircuitChargeInfo_blockType_992);
		local_Button2Value_System_Int32 = logic_uScript_GetCircuitChargeInfo_Return_992;
		if (logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_992.Out)
		{
			Relay_In_1004();
		}
	}

	private void Relay_True_996()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_996.True(out logic_uScriptAct_SetBool_Target_996);
		local_Question06WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_996;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_996.Out)
		{
			Relay_False_1005();
		}
	}

	private void Relay_False_996()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_996.False(out logic_uScriptAct_SetBool_Target_996);
		local_Question06WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_996;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_996.Out)
		{
			Relay_False_1005();
		}
	}

	private void Relay_True_997()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_997.True(out logic_uScriptAct_SetBool_Target_997);
		local_CanPushButtons_System_Boolean = logic_uScriptAct_SetBool_Target_997;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_997.Out)
		{
			Relay_In_999();
		}
	}

	private void Relay_False_997()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_997.False(out logic_uScriptAct_SetBool_Target_997);
		local_CanPushButtons_System_Boolean = logic_uScriptAct_SetBool_Target_997;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_997.Out)
		{
			Relay_In_999();
		}
	}

	private void Relay_In_998()
	{
		logic_uScriptCon_CompareBool_Bool_998 = Q6Button2;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_998.In(logic_uScriptCon_CompareBool_Bool_998);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_998.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_998.False;
		if (num)
		{
			Relay_False_996();
		}
		if (flag)
		{
			Relay_True_989();
		}
	}

	private void Relay_In_999()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_999.In(logic_uScriptAct_SetInt_Value_999, out logic_uScriptAct_SetInt_Target_999);
		local_QuestionSix_System_Int32 = logic_uScriptAct_SetInt_Target_999;
	}

	private void Relay_In_1002()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_1002.In(logic_uScriptAct_SetInt_Value_1002, out logic_uScriptAct_SetInt_Target_1002);
		local_QuestionSix_System_Int32 = logic_uScriptAct_SetInt_Target_1002;
	}

	private void Relay_In_1004()
	{
		logic_uScriptCon_CompareInt_A_1004 = local_Button2Value_System_Int32;
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_1004.In(logic_uScriptCon_CompareInt_A_1004, logic_uScriptCon_CompareInt_B_1004);
		bool greaterThan = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_1004.GreaterThan;
		bool lessThanOrEqualTo = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_1004.LessThanOrEqualTo;
		if (greaterThan)
		{
			Relay_In_998();
		}
		if (lessThanOrEqualTo)
		{
			Relay_In_1019();
		}
	}

	private void Relay_True_1005()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_1005.True(out logic_uScriptAct_SetBool_Target_1005);
		local_CanPushButtons_System_Boolean = logic_uScriptAct_SetBool_Target_1005;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_1005.Out)
		{
			Relay_In_1002();
		}
	}

	private void Relay_False_1005()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_1005.False(out logic_uScriptAct_SetBool_Target_1005);
		local_CanPushButtons_System_Boolean = logic_uScriptAct_SetBool_Target_1005;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_1005.Out)
		{
			Relay_In_1002();
		}
	}

	private void Relay_True_1007()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_1007.True(out logic_uScriptAct_SetBool_Target_1007);
		local_Question06WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_1007;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_1007.Out)
		{
			Relay_False_1018();
		}
	}

	private void Relay_False_1007()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_1007.False(out logic_uScriptAct_SetBool_Target_1007);
		local_Question06WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_1007;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_1007.Out)
		{
			Relay_False_1018();
		}
	}

	private void Relay_In_1010()
	{
		logic_uScriptCon_CompareInt_A_1010 = local_Button4Value_System_Int32;
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_1010.In(logic_uScriptCon_CompareInt_A_1010, logic_uScriptCon_CompareInt_B_1010);
		if (logic_uScriptCon_CompareInt_uScriptCon_CompareInt_1010.GreaterThan)
		{
			Relay_In_1030();
		}
	}

	private void Relay_In_1012()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_1012.In(logic_uScriptAct_SetInt_Value_1012, out logic_uScriptAct_SetInt_Target_1012);
		local_QuestionSix_System_Int32 = logic_uScriptAct_SetInt_Target_1012;
	}

	private void Relay_In_1013()
	{
		logic_uScript_GetCircuitChargeInfo_block_1013 = local_ButtonBlock4_TankBlock;
		logic_uScript_GetCircuitChargeInfo_Return_1013 = logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_1013.In(logic_uScript_GetCircuitChargeInfo_block_1013, logic_uScript_GetCircuitChargeInfo_tech_1013, logic_uScript_GetCircuitChargeInfo_blockType_1013);
		local_Button4Value_System_Int32 = logic_uScript_GetCircuitChargeInfo_Return_1013;
		if (logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_1013.Out)
		{
			Relay_In_1010();
		}
	}

	private void Relay_True_1014()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_1014.True(out logic_uScriptAct_SetBool_Target_1014);
		local_Question06WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_1014;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_1014.Out)
		{
			Relay_False_1025();
		}
	}

	private void Relay_False_1014()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_1014.False(out logic_uScriptAct_SetBool_Target_1014);
		local_Question06WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_1014;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_1014.Out)
		{
			Relay_False_1025();
		}
	}

	private void Relay_True_1018()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_1018.True(out logic_uScriptAct_SetBool_Target_1018);
		local_CanPushButtons_System_Boolean = logic_uScriptAct_SetBool_Target_1018;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_1018.Out)
		{
			Relay_In_1040();
		}
	}

	private void Relay_False_1018()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_1018.False(out logic_uScriptAct_SetBool_Target_1018);
		local_CanPushButtons_System_Boolean = logic_uScriptAct_SetBool_Target_1018;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_1018.Out)
		{
			Relay_In_1040();
		}
	}

	private void Relay_In_1019()
	{
		logic_uScript_GetCircuitChargeInfo_block_1019 = local_ButtonBlock3_TankBlock;
		logic_uScript_GetCircuitChargeInfo_Return_1019 = logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_1019.In(logic_uScript_GetCircuitChargeInfo_block_1019, logic_uScript_GetCircuitChargeInfo_tech_1019, logic_uScript_GetCircuitChargeInfo_blockType_1019);
		local_Button3Value_System_Int32 = logic_uScript_GetCircuitChargeInfo_Return_1019;
		if (logic_uScript_GetCircuitChargeInfo_uScript_GetCircuitChargeInfo_1019.Out)
		{
			Relay_In_1039();
		}
	}

	private void Relay_True_1023()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_1023.True(out logic_uScriptAct_SetBool_Target_1023);
		local_Question06WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_1023;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_1023.Out)
		{
			Relay_False_1041();
		}
	}

	private void Relay_False_1023()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_1023.False(out logic_uScriptAct_SetBool_Target_1023);
		local_Question06WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_1023;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_1023.Out)
		{
			Relay_False_1041();
		}
	}

	private void Relay_True_1024()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_1024.True(out logic_uScriptAct_SetBool_Target_1024);
		local_CanPushButtons_System_Boolean = logic_uScriptAct_SetBool_Target_1024;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_1024.Out)
		{
			Relay_In_1012();
		}
	}

	private void Relay_False_1024()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_1024.False(out logic_uScriptAct_SetBool_Target_1024);
		local_CanPushButtons_System_Boolean = logic_uScriptAct_SetBool_Target_1024;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_1024.Out)
		{
			Relay_In_1012();
		}
	}

	private void Relay_True_1025()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_1025.True(out logic_uScriptAct_SetBool_Target_1025);
		local_CanPushButtons_System_Boolean = logic_uScriptAct_SetBool_Target_1025;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_1025.Out)
		{
			Relay_In_1029();
		}
	}

	private void Relay_False_1025()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_1025.False(out logic_uScriptAct_SetBool_Target_1025);
		local_CanPushButtons_System_Boolean = logic_uScriptAct_SetBool_Target_1025;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_1025.Out)
		{
			Relay_In_1029();
		}
	}

	private void Relay_In_1027()
	{
		logic_uScriptCon_CompareBool_Bool_1027 = Q6Button3;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_1027.In(logic_uScriptCon_CompareBool_Bool_1027);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_1027.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_1027.False;
		if (num)
		{
			Relay_False_1023();
		}
		if (flag)
		{
			Relay_True_1014();
		}
	}

	private void Relay_In_1029()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_1029.In(logic_uScriptAct_SetInt_Value_1029, out logic_uScriptAct_SetInt_Target_1029);
		local_QuestionSix_System_Int32 = logic_uScriptAct_SetInt_Target_1029;
	}

	private void Relay_In_1030()
	{
		logic_uScriptCon_CompareBool_Bool_1030 = Q6Button4;
		logic_uScriptCon_CompareBool_uScriptCon_CompareBool_1030.In(logic_uScriptCon_CompareBool_Bool_1030);
		bool num = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_1030.True;
		bool flag = logic_uScriptCon_CompareBool_uScriptCon_CompareBool_1030.False;
		if (num)
		{
			Relay_False_1007();
		}
		if (flag)
		{
			Relay_True_1037();
		}
	}

	private void Relay_In_1034()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_1034.In(logic_uScriptAct_SetInt_Value_1034, out logic_uScriptAct_SetInt_Target_1034);
		local_QuestionSix_System_Int32 = logic_uScriptAct_SetInt_Target_1034;
	}

	private void Relay_True_1037()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_1037.True(out logic_uScriptAct_SetBool_Target_1037);
		local_Question06WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_1037;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_1037.Out)
		{
			Relay_False_1024();
		}
	}

	private void Relay_False_1037()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_1037.False(out logic_uScriptAct_SetBool_Target_1037);
		local_Question06WrongAnswers_System_Boolean = logic_uScriptAct_SetBool_Target_1037;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_1037.Out)
		{
			Relay_False_1024();
		}
	}

	private void Relay_In_1039()
	{
		logic_uScriptCon_CompareInt_A_1039 = local_Button3Value_System_Int32;
		logic_uScriptCon_CompareInt_uScriptCon_CompareInt_1039.In(logic_uScriptCon_CompareInt_A_1039, logic_uScriptCon_CompareInt_B_1039);
		bool greaterThan = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_1039.GreaterThan;
		bool lessThanOrEqualTo = logic_uScriptCon_CompareInt_uScriptCon_CompareInt_1039.LessThanOrEqualTo;
		if (greaterThan)
		{
			Relay_In_1027();
		}
		if (lessThanOrEqualTo)
		{
			Relay_In_1013();
		}
	}

	private void Relay_In_1040()
	{
		logic_uScriptAct_SetInt_uScriptAct_SetInt_1040.In(logic_uScriptAct_SetInt_Value_1040, out logic_uScriptAct_SetInt_Target_1040);
		local_QuestionSix_System_Int32 = logic_uScriptAct_SetInt_Target_1040;
	}

	private void Relay_True_1041()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_1041.True(out logic_uScriptAct_SetBool_Target_1041);
		local_CanPushButtons_System_Boolean = logic_uScriptAct_SetBool_Target_1041;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_1041.Out)
		{
			Relay_In_1034();
		}
	}

	private void Relay_False_1041()
	{
		logic_uScriptAct_SetBool_uScriptAct_SetBool_1041.False(out logic_uScriptAct_SetBool_Target_1041);
		local_CanPushButtons_System_Boolean = logic_uScriptAct_SetBool_Target_1041;
		if (logic_uScriptAct_SetBool_uScriptAct_SetBool_1041.Out)
		{
			Relay_In_1034();
		}
	}

	private void Relay_In_1045()
	{
		logic_uScript_Wait_seconds_1045 = WaitingTime;
		logic_uScript_Wait_uScript_Wait_1045.In(logic_uScript_Wait_seconds_1045, logic_uScript_Wait_repeat_1045);
		if (logic_uScript_Wait_uScript_Wait_1045.Waited)
		{
			Relay_In_623();
		}
	}

	private void Relay_In_1047()
	{
		logic_uScript_Wait_seconds_1047 = WaitingTime;
		logic_uScript_Wait_uScript_Wait_1047.In(logic_uScript_Wait_seconds_1047, logic_uScript_Wait_repeat_1047);
		if (logic_uScript_Wait_uScript_Wait_1047.Waited)
		{
			Relay_In_734();
		}
	}

	private void Relay_In_1049()
	{
		logic_uScript_Wait_seconds_1049 = WaitingTime;
		logic_uScript_Wait_uScript_Wait_1049.In(logic_uScript_Wait_seconds_1049, logic_uScript_Wait_repeat_1049);
		if (logic_uScript_Wait_uScript_Wait_1049.Waited)
		{
			Relay_In_807();
		}
	}

	private void Relay_In_1051()
	{
		logic_uScript_Wait_seconds_1051 = WaitingTime;
		logic_uScript_Wait_uScript_Wait_1051.In(logic_uScript_Wait_seconds_1051, logic_uScript_Wait_repeat_1051);
		if (logic_uScript_Wait_uScript_Wait_1051.Waited)
		{
			Relay_In_958();
		}
	}

	private void Relay_In_1053()
	{
		logic_uScript_Wait_seconds_1053 = WaitingTime;
		logic_uScript_Wait_uScript_Wait_1053.In(logic_uScript_Wait_seconds_1053, logic_uScript_Wait_repeat_1053);
		if (logic_uScript_Wait_uScript_Wait_1053.Waited)
		{
			Relay_In_361();
		}
	}

	private void Relay_In_1055()
	{
		logic_uScriptAct_AddInt_v2_A_1055 = local_QuestionFive_System_Int32;
		logic_uScriptAct_AddInt_v2_uScriptAct_AddInt_v2_1055.In(logic_uScriptAct_AddInt_v2_A_1055, logic_uScriptAct_AddInt_v2_B_1055, out logic_uScriptAct_AddInt_v2_IntResult_1055, out logic_uScriptAct_AddInt_v2_FloatResult_1055);
		local_QuestionFive_System_Int32 = logic_uScriptAct_AddInt_v2_IntResult_1055;
	}
}
